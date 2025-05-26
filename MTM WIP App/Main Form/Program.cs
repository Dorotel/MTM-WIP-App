using System.Diagnostics;
using System.Reflection;
using IWshRuntimeLibrary;
using MySql.Data.MySqlClient;
using File = System.IO.File;

namespace MTM_WIP_App.Main_Form
{
    internal static class Program
    {
        public static string enteredUser = "Default User";
        public static string connectionString = SqlVariables.GetConnectionString(null, null, null, null);
        public static string version = Assembly.GetExecutingAssembly().GetName().Version.ToString();

        [STAThread]
        private static void Main()
        {
            Debug.WriteLine("Main method started.");
            AppLogger.Log("Main method started.");

            try
            {
                AppDataCleaner.WipeAppDataFolders();
                ShortcutManager.EnsureApplicationShortcut();

                Debug.WriteLine("Setting High DPI mode...");
                AppLogger.Log("Setting High DPI mode...");
                Application.SetHighDpiMode(HighDpiMode.PerMonitorV2);

                Debug.WriteLine("Checking DPI scaling...");
                AppLogger.Log("Checking DPI scaling...");
                DpiChecker.CheckDpiScaling();

                ApplicationConfiguration.Initialize();

                Debug.WriteLine("Initializing application...");
                AppLogger.Log("Initializing application...");
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                var mainForm = new MainForm();

                AppLogger.InitializeLogging();
                AppLogger.Log("Application starting...");
                AppLogger.CleanUpOldLogsIfNeeded();

                // Move login prompt to after message loop starts
                mainForm.Shown += async (s, e) =>
                {
                    if (IsDefaultUser())
                    {
                        using var loginForm = new LoginPromptForm();
                        if (loginForm.ShowDialog(mainForm) != DialogResult.OK)
                        {
                            mainForm.Close();
                            return;
                        }
                    }

                    Debug.WriteLine("Retrieving user access type...");
                    AppLogger.Log("Retrieving user access type...");
                    SearchDao.System_UserAccessType();

                    Debug.WriteLine("Loading user settings...");
                    AppLogger.Log("Loading user settings...");

                    await MainForm.Primary_OnLoad_LoadUserSettingsAsync();

                    Debug.WriteLine("Showing changelog...");
                    AppLogger.Log("Showing changelog...");
                    mainForm.Primary_Startup_ShowChangeLog();

                    Debug.WriteLine("Running VersionChecker...");
                    AppLogger.Log("Running VersionChecker...");
                    VersionCheckerService.VersionChecker(null, null);
                };

                Debug.WriteLine("Starting main form...");
                AppLogger.Log("Starting main form...");
                Application.Run(mainForm);

                Debug.WriteLine("Application started.");
                AppLogger.Log("Application started.");
            }
            catch (UnauthorizedAccessException ex)
            {
                ExceptionHandler.HandleUnauthorizedAccessException(ex);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleGeneralException(ex);
            }
        }

        static Program()
        {
            Debug.WriteLine("Initializing Program class...");
            AppLogger.Log("Initializing Program class...");
            VersionCheckerService.Initialize();
        }

        private static bool IsDefaultUser()
        {
            Debug.WriteLine("Checking if user is default...");
            AppLogger.Log("Checking if user is default...");

            if (enteredUser == "Default User")
            {
                enteredUser = WipAppVariables.User;
            }

            return enteredUser is "Default User" or "JOHNK" or "SHOP2" or "MTMDC";
        }
    }

    internal static class AppLogger
    {
        private static string _logFilePath;
        private static readonly Lock LogLock = new();

        private static readonly List<string> LogMessages = [];

        public static void InitializeLogging()
        {
            Debug.WriteLine("Initializing logging...");
            Log("Initializing logging...");

            var server = new MySqlConnectionStringBuilder(Program.connectionString).Server;
            var userName = WipAppVariables.User;
            _logFilePath = SqlVariables.GetLogFilePath(server, userName);

            AppDomain.CurrentDomain.ProcessExit += OnProcessExit;
        }

        public static void Log(string message)
        {
            using (LogLock.EnterScope())
            {
                LogMessages.Add($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}");
            }
        }

        public static void LogDatabaseError(Exception ex)
        {
            using (LogLock.EnterScope())
            {
                LogMessages.Add($"{DateTime.Now}: Database Error - {ex.Message}");
                LogMessages.Add($"{DateTime.Now}: Stack Trace - {ex.StackTrace}");
            }
        }

        public static void CleanUpOldLogsIfNeeded()
        {
            Debug.WriteLine("Cleaning up old logs if needed...");
            Log("Cleaning up old logs if needed...");

            var logDirectory = Path.GetDirectoryName(_logFilePath);
            if (!string.IsNullOrEmpty(logDirectory))
            {
                CleanUpOldLogs(logDirectory, 20);
            }
        }

        private static void OnProcessExit(object? sender, EventArgs e)
        {
            Debug.WriteLine("OnProcessExit triggered. Writing logs to file...");
            Log("OnProcessExit triggered. Writing logs to file...");

            try
            {
                using var writer = new StreamWriter(_logFilePath, true);
                lock (LogLock)
                {
                    foreach (var logMessage in LogMessages)
                    {
                        writer.WriteLine(logMessage);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Failed to write to log file on exit: {ex.Message}");
                Log($"Failed to write to log file on exit: {ex.Message}");
            }
        }

        private static void CleanUpOldLogs(string logDirectory, int maxLogs)
        {
            Debug.WriteLine($"Cleaning up old logs in directory: {logDirectory}");
            Log($"Cleaning up old logs in directory: {logDirectory}");

            try
            {
                var logFiles = Directory.GetFiles(logDirectory, "*.log")
                    .OrderByDescending(File.GetCreationTime)
                    .ToList();

                if (logFiles.Count > maxLogs)
                {
                    var filesToDelete = logFiles.Skip(maxLogs).ToList();
                    foreach (var logFile in filesToDelete)
                    {
                        Debug.WriteLine($"Deleting old log file: {logFile}");
                        Log($"Deleting old log file: {logFile}");
                        File.Delete(logFile);
                    }
                }

                if (Debugger.IsAttached)
                {
                    Debug.WriteLine("Skipping cleaning of %AppData% and %LocalAppData% in debug mode.");
                    Log("Skipping cleaning of %AppData% and %LocalAppData% in debug mode.");
                    return;
                }

                var appDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                    "MTM_WIP_APP");
                var localAppDataPath =
                    Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                        "MTM_WIP_APP");

                AppDataCleaner.DeleteDirectoryContents(appDataPath);
                AppDataCleaner.DeleteDirectoryContents(localAppDataPath);

                Log("Cleaned up application data folders in %AppData% and %LocalAppData%.");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Failed to clean up old log files or application data: {ex.Message}");
                Log($"Failed to clean up old log files or application data: {ex.Message}");
            }
        }
    }//done

    internal static class VersionCheckerService
    {
        private static readonly System.Timers.Timer VersionTimer = new(30000);

        public static void Initialize()
        {
            try
            {
                VersionTimer.Elapsed += VersionChecker;
                VersionTimer.Enabled = true;
                VersionTimer.AutoReset = true;
                VersionTimer.Start();

                Debug.WriteLine("VersionTimer initialized and started.");
                AppLogger.Log("VersionTimer initialized and started.");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error initializing VersionTimer: {ex.Message}");
                AppLogger.Log($"Error initializing VersionTimer: {ex.Message}");
            }
        }

        public static void VersionChecker(object? sender, System.Timers.ElapsedEventArgs e)
        {
            Debug.WriteLine("Running VersionChecker...");
            AppLogger.Log("Running VersionChecker...");

            MySqlConnection connection = null;
            MySqlCommand command = null;
            MySqlDataReader reader = null;

            try
            {
                connection = new MySqlConnection(Program.connectionString);
                connection.Open();

                command = new MySqlCommand("SELECT * FROM `program_information`", connection);
                using (reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var databaseVersion = reader.GetString(1);

                        if (databaseVersion != Program.version)
                        {
                            AppLogger.Log(
                                $"Version mismatch detected. Current: {Program.version}, Expected: {databaseVersion}");
                            Debug.WriteLine(
                                $"Version mismatch detected. Current: {Program.version}, Expected: {databaseVersion}");

                            Task.Run(() =>
                            {
                                var message = "You are using an older version of the WIP Application.\n" +
                                              "This normally means a newer version is just about to be released.\n" +
                                              "The program will close in 30 seconds, or by clicking OK.";
                                var caption = $"Version Conflict Error ({Program.version}/{databaseVersion})";
                                MessageBox.Show(message, caption, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

                                Application.Exit();
                            });

                            break;
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                AppLogger.LogDatabaseError(ex);
                ExceptionHandler.HandleDatabaseError();
            }
            catch (Exception ex)
            {
                AppLogger.LogDatabaseError(ex);
                MessageBox.Show(@"An error occurred in VersionChecker:
" + ex.Message, @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                reader?.Close();
                command?.Dispose();
                connection?.Close();
            }
        }
    }//done

    internal static class ShortcutManager//done
    {
        public static void EnsureApplicationShortcut()
        {
            Debug.WriteLine("Ensuring application shortcut...");
            AppLogger.Log("Ensuring application shortcut...");

            try
            {
                var desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                var shortcutName = "MTM WIP App (Live).lnk";
                var shortcutPath = Path.Combine(desktopPath, shortcutName);
                var applicationPath = Application.ExecutablePath;

                Debug.WriteLine("Checking for existing shortcuts...");
                AppLogger.Log("Checking for existing shortcuts...");
                var existingShortcuts = Directory.GetFiles(desktopPath, "*.lnk")
                    .Where(file => GetShortcutTarget(file) == applicationPath)
                    .ToList();

                foreach (var shortcut in existingShortcuts)
                {
                    if (!shortcut.Equals(shortcutPath, StringComparison.OrdinalIgnoreCase))
                    {
                        Debug.WriteLine($"Deleting existing shortcut: {shortcut}");
                        File.Delete(shortcut);
                        AppLogger.Log($"Deleted existing shortcut: {shortcut}");
                    }
                }

                if (!File.Exists(shortcutPath))
                {
                    Debug.WriteLine($"Creating new shortcut: {shortcutPath}");
                    CreateShortcut(shortcutPath, applicationPath, "MTM WIP Application");
                    AppLogger.Log($"Created new shortcut: {shortcutPath}");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error ensuring application shortcut: {ex.Message}");
                AppLogger.Log($"Error ensuring application shortcut: {ex.Message}");
            }
        }

        private static void CreateShortcut(string shortcutPath, string targetPath, string description)
        {
            Debug.WriteLine($"Creating shortcut at {shortcutPath}...");
            AppLogger.Log($"Creating shortcut at {shortcutPath}...");

            try
            {
                var shell = new WshShell();
                var shortcut = (IWshShortcut)shell.CreateShortcut(shortcutPath);
                shortcut.TargetPath = targetPath;
                shortcut.Description = description;
                shortcut.Save();

                Debug.WriteLine($"Shortcut created at {shortcutPath}");
                AppLogger.Log($"Shortcut created at {shortcutPath}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error creating shortcut: {ex.Message}");
                AppLogger.Log($"Error creating shortcut: {ex.Message}");
            }
        }

        private static string GetShortcutTarget(string shortcutPath)
        {
            Debug.WriteLine($"Getting shortcut target for {shortcutPath}...");
            AppLogger.Log($"Getting shortcut target for {shortcutPath}...");

            try
            {
                var shell = new WshShell();
                var shortcut = (IWshShortcut)shell.CreateShortcut(shortcutPath);
                return shortcut.TargetPath;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error getting shortcut target: {ex.Message}");
                AppLogger.Log($"Error getting shortcut target: {ex.Message}");
                return string.Empty;
            }
        }
    }

    internal static class AppDataCleaner
    {
        public static void WipeAppDataFolders()
        {
            Debug.WriteLine("Wiping AppData folders...");
            AppLogger.Log("Wiping AppData folders...");

            try
            {
                var appDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                    "MTM_WIP_APP");
                var localAppDataPath =
                    Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                        "MTM_WIP_APP");

                DeleteDirectoryIfExists(appDataPath);
                DeleteDirectoryIfExists(localAppDataPath);

                AppLogger.Log("MTM_WIP_APP folders wiped from AppData and LocalAppData.");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error wiping MTM_WIP_APP folders: {ex.Message}");
                AppLogger.Log($"Error wiping MTM_WIP_APP folders: {ex.Message}");
            }
        }

        private static void DeleteDirectoryIfExists(string path)
        {
            Debug.WriteLine($"Checking if directory exists: {path}");
            AppLogger.Log($"Checking if directory exists: {path}");

            try
            {
                if (Directory.Exists(path))
                {
                    Debug.WriteLine($"Directory exists. Deleting: {path}");
                    Directory.Delete(path, true);
                    AppLogger.Log($"Deleted directory: {path}");
                }
                else
                {
                    Debug.WriteLine($"Directory does not exist: {path}");
                    AppLogger.Log($"Directory does not exist: {path}");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error deleting directory {path}: {ex.Message}");
                AppLogger.Log($"Error deleting directory {path}: {ex.Message}");
            }
        }

        public static void DeleteDirectoryContents(string directoryPath)
        {
            try
            {
                if (Directory.Exists(directoryPath))
                {
                    Debug.WriteLine($"Deleting contents of directory: {directoryPath}");
                    AppLogger.Log($"Deleting contents of directory: {directoryPath}");

                    foreach (var file in Directory.GetFiles(directoryPath))
                    {
                        Debug.WriteLine($"Deleting file: {file}");
                        AppLogger.Log($"Deleting file: {file}");
                        File.Delete(file);
                    }

                    foreach (var subDirectory in Directory.GetDirectories(directoryPath))
                    {
                        Debug.WriteLine($"Deleting subdirectory: {subDirectory}");
                        AppLogger.Log($"Deleting subdirectory: {subDirectory}");
                        Directory.Delete(subDirectory, true);
                    }
                }
                else
                {
                    Debug.WriteLine($"Directory does not exist: {directoryPath}");
                    AppLogger.Log($"Directory does not exist: {directoryPath}");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error deleting contents of directory {directoryPath}: {ex.Message}");
                AppLogger.Log($"Error deleting contents of directory {directoryPath}: {ex.Message}");
            }
        }
    }//done

    internal static class ExceptionHandler
    {
        public static void HandleUnauthorizedAccessException(UnauthorizedAccessException ex)
        {
            Debug.WriteLine($"UnauthorizedAccessException: {ex.Message}");
            AppLogger.Log($"UnauthorizedAccessException: {ex.Message}");

            MessageBox.Show(
                @"You do not have the necessary permissions to run this application. Please run as administrator.",
                @"Permission Denied",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static void HandleGeneralException(Exception ex)
        {
            Debug.WriteLine($"Exception: {ex.Message}");
            AppLogger.Log($"Exception: {ex.Message}");

            MessageBox.Show(@"An unexpected error occurred: " + ex.Message, @"Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static void HandleDatabaseError()
        {
            Debug.WriteLine("Handling database error...");
            AppLogger.Log("Handling database error...");

            try
            {
                if (Application.OpenForms.OfType<MainForm>().Any())
                {
                    var mainForm = Application.OpenForms.OfType<MainForm>().First();
                    mainForm.Invoke(new Action(() =>
                    {
                        mainForm.MainForm_StatusStrip_Disconnected.Visible = true;
                        mainForm.MainForm_StatusStrip_SavedStatus.Visible = false;
                        mainForm.Enabled = false;
                    }));
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error handling database error: {ex.Message}");
                AppLogger.Log($"Error handling database error: {ex.Message}");
            }
        }
    }//done
}