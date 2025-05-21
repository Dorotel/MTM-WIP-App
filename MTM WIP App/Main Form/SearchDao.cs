using MySql.Data.MySqlClient;
using System.Data;
using System.Diagnostics;
using System.Security.Principal;
using static MTM_WIP_App.SqlVariables;

namespace MTM_WIP_App.Main_Form
{
    internal class SearchDao
    {
        internal static DataTable GetVitsUsers()
        {
            var table = new DataTable();
            try
            {
                using var connection = new MySqlConnection(GetConnectionString(null, null, null, null));
                connection.Open();
                var command = new MySqlCommand("SELECT `User` FROM `users` WHERE `VitsUser` = TRUE ORDER BY `User` ASC",
                    connection);
                var adapter = new MySqlDataAdapter(command);
                adapter.Fill(table);
            }
            catch (Exception ex)
            {
                AppLogger.LogDatabaseError(ex);
            }

            return table;
        }

        internal static bool ValidateUserPin(string username, string pin)
        {
            try
            {
                using var connection = new MySqlConnection(GetConnectionString(null, null, null, null));
                connection.Open();
                var command = new MySqlCommand("SELECT COUNT(*) FROM `users` WHERE `User` = @username AND `PIN` = @pin",
                    connection);
                command.Parameters.AddWithValue("@username", username);
                command.Parameters.AddWithValue("@pin", pin);
                var result = Convert.ToInt32(command.ExecuteScalar());
                return result > 0;
            }
            catch (Exception ex)
            {
                AppLogger.LogDatabaseError(ex);
                return false;
            }
        }

        // --- USER MANAGEMENT ---

        internal static bool UserExists(string email)
        {
            using var connection = new MySqlConnection(GetConnectionString(null, null, null, null));
            connection.Open();
            var cmd = new MySqlCommand("SELECT COUNT(*) FROM `users` WHERE `User` = @Email", connection);
            cmd.Parameters.AddWithValue("@Email", email);
            return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
        }

        internal static void InsertUser(string email, string fullName, string shift, string isVitsUser, string? pin)
        {
            using var connection = new MySqlConnection(GetConnectionString(null, null, null, null));
            connection.Open();
            var cmd = new MySqlCommand(
                "INSERT INTO `users` (`User`, `Full Name`, `Shift`, `VitsUser`, `Pin`) VALUES (@Email, @FullName, @Shift, @IsVitsUser, @Pin)",
                connection);
            cmd.Parameters.AddWithValue("@Email", email);
            cmd.Parameters.AddWithValue("@FullName", fullName);
            cmd.Parameters.AddWithValue("@Shift", shift);
            cmd.Parameters.AddWithValue("@IsVitsUser", isVitsUser);
            cmd.Parameters.AddWithValue("@Pin", pin ?? (object)DBNull.Value);
            cmd.ExecuteNonQuery();
        }

        internal static void UpdateUser(string email, string fullName, string shift, string isVitsUser, string? pin)
        {
            using var connection = new MySqlConnection(GetConnectionString(null, null, null, null));
            connection.Open();
            var cmd = new MySqlCommand(
                "UPDATE `users` SET `Full Name` = @FullName, `Shift` = @Shift, `VitsUser` = @IsVitsUser, `Pin` = @Pin WHERE `User` = @Email",
                connection);
            cmd.Parameters.AddWithValue("@Email", email);
            cmd.Parameters.AddWithValue("@FullName", fullName);
            cmd.Parameters.AddWithValue("@Shift", shift);
            cmd.Parameters.AddWithValue("@IsVitsUser", isVitsUser);
            cmd.Parameters.AddWithValue("@Pin", pin ?? (object)DBNull.Value);
            cmd.ExecuteNonQuery();
        }

        internal static void SetUserAdminStatus(string email, bool isAdmin)
        {
            using var connection = new MySqlConnection(GetConnectionString(null, null, null, null));
            connection.Open();
            if (isAdmin)
            {
                var checkCmd = new MySqlCommand("SELECT COUNT(*) FROM `leads` WHERE `User` = @Email", connection);
                checkCmd.Parameters.AddWithValue("@Email", email);
                if (Convert.ToInt32(checkCmd.ExecuteScalar()) == 0)
                {
                    var insertCmd = new MySqlCommand("INSERT INTO `leads` (`User`) VALUES (@Email)", connection);
                    insertCmd.Parameters.AddWithValue("@Email", email);
                    insertCmd.ExecuteNonQuery();
                }
            }
            else
            {
                var deleteCmd = new MySqlCommand("DELETE FROM `leads` WHERE `User` = @Email", connection);
                deleteCmd.Parameters.AddWithValue("@Email", email);
                deleteCmd.ExecuteNonQuery();
            }
        }

        internal static void SetUserReadOnlyStatus(string email, bool isReadOnly)
        {
            using var connection = new MySqlConnection(GetConnectionString(null, null, null, null));
            connection.Open();
            if (isReadOnly)
            {
                var checkCmd = new MySqlCommand("SELECT COUNT(*) FROM `readonly` WHERE `User` = @Email", connection);
                checkCmd.Parameters.AddWithValue("@Email", email);
                if (Convert.ToInt32(checkCmd.ExecuteScalar()) == 0)
                {
                    var insertCmd = new MySqlCommand("INSERT INTO `readonly` (`User`) VALUES (@Email)", connection);
                    insertCmd.Parameters.AddWithValue("@Email", email);
                    insertCmd.ExecuteNonQuery();
                }
            }
            else
            {
                var deleteCmd = new MySqlCommand("DELETE FROM `readonly` WHERE `User` = @Email", connection);
                deleteCmd.Parameters.AddWithValue("@Email", email);
                deleteCmd.ExecuteNonQuery();
            }
        }

        internal static void AddUserToMySqlServer(string email)
        {
            using var connection = new MySqlConnection(GetConnectionString(null, null, null, null));
            connection.Open();
            var checkCmd = new MySqlCommand("SELECT COUNT(*) FROM mysql.user WHERE User = @Email AND Host = '%'",
                connection);
            checkCmd.Parameters.AddWithValue("@Email", email);
            if (Convert.ToInt32(checkCmd.ExecuteScalar()) == 0)
            {
                var createCmd = new MySqlCommand(
                    $"CREATE USER '{email}'@'%' IDENTIFIED WITH mysql_native_password AS ''; " +
                    $"GRANT ALL PRIVILEGES ON *.* TO '{email}'@'%' REQUIRE NONE WITH GRANT OPTION " +
                    $"MAX_QUERIES_PER_HOUR 0 MAX_CONNECTIONS_PER_HOUR 0 MAX_UPDATES_PER_HOUR 0 MAX_USER_CONNECTIONS 0;",
                    connection);
                createCmd.ExecuteNonQuery();
            }
        }

        // --- PART MANAGEMENT ---

        internal static bool PartExists(string partNumber)
        {
            using var connection = new MySqlConnection(GetConnectionString(null, null, null, null));
            connection.Open();
            var cmd = new MySqlCommand("SELECT COUNT(*) FROM `part_ids` WHERE `Item Number` = @partNumber", connection);
            cmd.Parameters.AddWithValue("@partNumber", partNumber);
            return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
        }

        internal static void InsertPart(string partNumber, string user, string partType)
        {
            using var connection = new MySqlConnection(GetConnectionString(null, null, null, null));
            connection.Open();
            var cmd = new MySqlCommand(
                "INSERT INTO `part_ids` (`Item Number`, `ID`, `Issued By`, `Type`) VALUES (@partNumber, NULL, @user, @partType);",
                connection);
            cmd.Parameters.AddWithValue("@partNumber", partNumber);
            cmd.Parameters.AddWithValue("@user", user);
            cmd.Parameters.AddWithValue("@partType", partType);
            cmd.ExecuteNonQuery();
        }

        // --- PART TYPE MANAGEMENT ---

        internal static bool PartTypeExists(string partType)
        {
            using var connection = new MySqlConnection(GetConnectionString(null, null, null, null));
            connection.Open();
            var cmd = new MySqlCommand("SELECT COUNT(*) FROM `item_types` WHERE `Type` = @partType", connection);
            cmd.Parameters.AddWithValue("@partType", partType);
            return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
        }

        internal static void InsertPartType(string partType, string user)
        {
            using var connection = new MySqlConnection(GetConnectionString(null, null, null, null));
            connection.Open();
            var cmd = new MySqlCommand(
                "INSERT INTO `item_types` (`Type`, `ID`, `Issued By`) VALUES (@partType, NULL, @user);",
                connection);
            cmd.Parameters.AddWithValue("@partType", partType);
            cmd.Parameters.AddWithValue("@user", user);
            cmd.ExecuteNonQuery();
        }

        // --- OPERATION MANAGEMENT ---

        internal static bool OperationExists(string operationNumber)
        {
            using var connection = new MySqlConnection(GetConnectionString(null, null, null, null));
            connection.Open();
            var cmd = new MySqlCommand("SELECT COUNT(*) FROM `operation_numbers` WHERE `Operation` = @operationNumber",
                connection);
            cmd.Parameters.AddWithValue("@operationNumber", operationNumber);
            return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
        }

        internal static void InsertOperation(string operationNumber, string user)
        {
            using var connection = new MySqlConnection(GetConnectionString(null, null, null, null));
            connection.Open();
            var cmd = new MySqlCommand(
                "INSERT INTO `operation_numbers` (`Operation`, `ID`, `Issued By`) VALUES (@operationNumber, NULL, @user);",
                connection);
            cmd.Parameters.AddWithValue("@operationNumber", operationNumber);
            cmd.Parameters.AddWithValue("@user", user);
            cmd.ExecuteNonQuery();
        }

        // --- LOCATION MANAGEMENT ---

        internal static bool LocationExists(string location)
        {
            using var connection = new MySqlConnection(GetConnectionString(null, null, null, null));
            connection.Open();
            var cmd = new MySqlCommand("SELECT COUNT(*) FROM `locations` WHERE `Location` = @location", connection);
            cmd.Parameters.AddWithValue("@location", location);
            return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
        }

        internal static void InsertLocation(string location, string user)
        {
            using var connection = new MySqlConnection(GetConnectionString(null, null, null, null));
            connection.Open();
            var cmd = new MySqlCommand(
                "INSERT INTO `locations` (`Location`, `ID`, `Issued By`) VALUES (@location, NULL, @user);",
                connection);
            cmd.Parameters.AddWithValue("@location", location);
            cmd.Parameters.AddWithValue("@user", user);
            cmd.ExecuteNonQuery();
        }

        internal static List<(string Method, string Error)> GetUniqueErrors()
        {
            MySqlConnection connection = null;
            MySqlCommand command = null;
            MySqlDataReader reader = null;
            List<(string Method, string Error)> uniqueErrors = [];

            try
            {
                connection = new MySqlConnection(GetConnectionString(null, null, null, null));
                connection.Open();

                command = new MySqlCommand("SELECT DISTINCT `Method`, `Error` FROM `wipapp_errorlog`", connection);
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var method = reader.GetString("Method");
                    var error = reader.GetString("Error");
                    uniqueErrors.Add((method, error));
                }

                AppLogger.Log("GetUniqueErrors executed successfully.");
            }
            catch (MySqlException ex)
            {
                AppLogger.LogDatabaseError(ex);
                AppLogger.Log("Error in GetUniqueErrors: " + ex.Message);
                HandleException_SQLError_CloseApp(ex);
            }
            catch (Exception ex)
            {
                AppLogger.LogDatabaseError(ex);
                AppLogger.Log("Error in GetUniqueErrors: " + ex.Message);
                HandleException_GeneralError_CloseApp(ex);
            }
            finally
            {
                reader?.Close();
                command?.Dispose();
                connection?.Close();
                connection?.Dispose();
            }

            return uniqueErrors;
        }

        internal static List<string> Primary_ChangeLog_Get_AllVersions()
        {
            var versions = new List<string>();
            try
            {
                var connectionString = GetConnectionString(null, null, null, null);
                using var connection = new MySqlConnection(connectionString);
                connection.Open();
                using var command =
                    new MySqlCommand("SELECT `Version` FROM `wipapp_version_history` ORDER BY `Version` DESC",
                        connection);
                using var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    versions.Add(reader.GetString(0));
                }

                AppLogger.Log("Primary_ChangeLog_Get_AllVersions executed successfully.");
            }
            catch (MySqlException ex)
            {
                AppLogger.LogDatabaseError(ex);
                HandleException_SQLError_CloseApp(ex);
            }
            catch (Exception ex)
            {
                AppLogger.LogDatabaseError(ex);
                HandleException_GeneralError_CloseApp(ex);
            }

            return versions;
        }

        internal static void Primary_ChangeLog_Set_LastShown(string value, string user)
        {
            MySqlConnection connection = null;
            MySqlCommand command = null;
            try
            {
                connection = new MySqlConnection(GetConnectionString(null, null, null, null));
                command = new MySqlCommand
                {
                    CommandText =
                        $@"UPDATE `users` SET `LastShownVersion` = '{value}' WHERE `users`.`user` = '{user}';",
                    Connection = connection
                };
                connection.Open();
                command.ExecuteNonQuery();
                AppLogger.Log("Primary_ChangeLog_Set_LastShown executed successfully for user: " + user);
            }
            catch (MySqlException ex)
            {
                AppLogger.LogDatabaseError(ex);
                AppLogger.Log("Error in Primary_ChangeLog_Set_LastShown: " + ex.Message);
                HandleException_SQLError_CloseApp(ex);
            }
            catch (Exception ex)
            {
                AppLogger.LogDatabaseError(ex);
                AppLogger.Log("Error in Primary_ChangeLog_Set_LastShown: " + ex.Message);
                HandleException_GeneralError_CloseApp(ex);
            }
            finally
            {
                command?.Dispose();
                connection?.Close();
                connection?.Dispose();
            }
        }

        internal static void Primary_ChangeLog_Set_Switch(string value, string user)
        {
            MySqlConnection connection = null;
            MySqlCommand command = null;
            try
            {
                connection = new MySqlConnection(GetConnectionString(null, null, null, null));
                command = new MySqlCommand
                {
                    CommandText = $@"UPDATE `users` SET `HideChangeLog` = '{value}' WHERE `users`.`user` = '{user}';",
                    Connection = connection
                };
                connection.Open();
                command.ExecuteNonQuery();
                AppLogger.Log("Primary_ChangeLog_Set_Switch executed successfully for user: " + user);
            }
            catch (MySqlException ex)
            {
                AppLogger.LogDatabaseError(ex);
                AppLogger.Log("Error in Primary_ChangeLog_Set_Switch: " + ex.Message);
                HandleException_SQLError_CloseApp(ex);
            }
            catch (Exception ex)
            {
                AppLogger.LogDatabaseError(ex);
                AppLogger.Log("Error in Primary_ChangeLog_Set_Switch: " + ex.Message);
                HandleException_GeneralError_CloseApp(ex);
            }
            finally
            {
                command?.Dispose();
                connection?.Close();
                connection?.Dispose();
            }
        }

        internal static void Primary_ChangeLog_Set_VersionNotes(string rtfNotes, string version)
        {
            MySqlConnection connection = null;
            MySqlCommand command = null;
            try
            {
                connection = new MySqlConnection(GetConnectionString(null, null, null, null));
                connection.Open();
                command = new MySqlCommand
                {
                    CommandText = "UPDATE `wipapp_version_history` SET `Notes` = @notes WHERE `Version` = @version",
                    Connection = connection
                };
                command.Parameters.AddWithValue("@notes", rtfNotes);
                command.Parameters.AddWithValue("@version", version);
                command.ExecuteNonQuery();
                AppLogger.Log("Primary_ChangeLog_Set_VersionNotes executed successfully for version: " + version);
            }
            catch (MySqlException ex)
            {
                AppLogger.LogDatabaseError(ex);
                AppLogger.Log("Error in Primary_ChangeLog_Set_VersionNotes: " + ex.Message);
                HandleException_SQLError_CloseApp(ex);
            }
            catch (Exception ex)
            {
                AppLogger.LogDatabaseError(ex);
                AppLogger.Log("Error in Primary_ChangeLog_Set_VersionNotes: " + ex.Message);
                HandleException_GeneralError_CloseApp(ex);
            }
            finally
            {
                command?.Dispose();
                connection?.Close();
                connection?.Dispose();
            }
        }

        internal static string Primary_ChangeLog_Get_Visual_User()
        {
            MySqlConnection connection = null;
            MySqlCommand command = null;
            MySqlDataReader reader = null;
            var user = WipAppVariables.User;
            var returnThis = "UserName";
            try
            {
                connection = new MySqlConnection(GetConnectionString(null, null, null, null));
                connection.Open();
                command = new MySqlCommand
                {
                    CommandText = "SELECT * FROM `users`",
                    Connection = connection
                };
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    AdminList a = new()
                    {
                        Id = reader.GetInt32(0),
                        User = reader.GetString(1),
                        VisualUserName = reader.GetString(6)
                    };
                    if (a.User == user)
                    {
                        returnThis = a.VisualUserName;
                    }
                }

                AppLogger.Log("Primary_ChangeLog_Get_Visual_User executed successfully for user: " + user);
            }
            catch (MySqlException ex)
            {
                AppLogger.LogDatabaseError(ex);
                AppLogger.Log("Error in Primary_ChangeLog_Get_Visual_User: " + ex.Message);
                HandleException_SQLError_CloseApp(ex);
                return returnThis;
            }
            catch (Exception ex)
            {
                AppLogger.LogDatabaseError(ex);
                AppLogger.Log("Error in Primary_ChangeLog_Get_Visual_User: " + ex.Message);
                HandleException_GeneralError_CloseApp(ex);
                return returnThis;
            }
            finally
            {
                reader?.Close();
                reader?.Dispose();
                command?.Dispose();
                connection?.Close();
                connection?.Dispose();
            }

            return returnThis;
        }

        internal static string Primary_ChangeLog_Get_Theme_Name()
        {
            MySqlConnection connection = null;
            MySqlCommand command = null;
            MySqlDataReader reader = null;
            var user = WipAppVariables.User;
            var returnThis = "false";
            try
            {
                connection = new MySqlConnection(GetConnectionString(null, null, null, null));
                connection.Open();
                command = new MySqlCommand
                {
                    CommandText = "SELECT * FROM `users`",
                    Connection = connection
                };
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    AdminList a = new()
                    {
                        Id = reader.GetInt32(0),
                        User = reader.GetString(1),
                        ThemeName = reader.GetString(8)
                    };
                    if (a.User == user)
                    {
                        returnThis = a.ThemeName;
                    }
                }

                AppLogger.Log("Primary_ChangeLog_Get_Theme_Name executed successfully for user: " + user);
            }
            catch (MySqlException ex)
            {
                AppLogger.LogDatabaseError(ex);
                AppLogger.Log("Error in Primary_ChangeLog_Get_Theme_Name: " + ex.Message);
                HandleException_SQLError_CloseApp(ex);
                return returnThis;
            }
            catch (Exception ex)
            {
                AppLogger.LogDatabaseError(ex);
                AppLogger.Log("Error in Primary_ChangeLog_Get_Theme_Name: " + ex.Message);
                HandleException_GeneralError_CloseApp(ex);
                return returnThis;
            }
            finally
            {
                reader?.Close();
                reader?.Dispose();
                command?.Dispose();
                connection?.Close();
                connection?.Dispose();
            }

            return returnThis;
        }

        internal static string Primary_ChangeLog_Get_Visual_Password()
        {
            MySqlConnection connection = null;
            MySqlCommand command = null;
            MySqlDataReader reader = null;
            var user = WipAppVariables.User;
            var returnThis = "false";
            try
            {
                connection = new MySqlConnection(GetConnectionString(null, null, null, null));
                connection.Open();
                command = new MySqlCommand
                {
                    CommandText = "SELECT * FROM `users`",
                    Connection = connection
                };
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    AdminList a = new()
                    {
                        Id = reader.GetInt32(0),
                        User = reader.GetString(1),
                        VisualPassword = reader.GetString(7)
                    };
                    if (a.User == user)
                    {
                        returnThis = a.VisualPassword;
                    }
                }

                AppLogger.Log("Primary_ChangeLog_Get_Visual_Password executed successfully for user: " + user);
            }
            catch (MySqlException ex)
            {
                AppLogger.LogDatabaseError(ex);
                AppLogger.Log("Error in Primary_ChangeLog_Get_Visual_Password: " + ex.Message);
                HandleException_SQLError_CloseApp(ex);
                return returnThis;
            }
            catch (Exception ex)
            {
                AppLogger.LogDatabaseError(ex);
                AppLogger.Log("Error in Primary_ChangeLog_Get_Visual_Password: " + ex.Message);
                HandleException_GeneralError_CloseApp(ex);
                return returnThis;
            }
            finally
            {
                reader?.Close();
                reader?.Dispose();
                command?.Dispose();
                connection?.Close();
                connection?.Dispose();
            }

            return returnThis;
        }

        internal static int? Primary_ChangeLog_Get_Visual_Theme_FontSize()
        {
            MySqlConnection connection = null;
            MySqlCommand command = null;
            MySqlDataReader reader = null;
            var user = WipAppVariables.User;
            int? returnThis = 9;
            try
            {
                connection = new MySqlConnection(GetConnectionString(null, null, null, null));
                connection.Open();
                command = new MySqlCommand
                {
                    CommandText = "SELECT * FROM `users`",
                    Connection = connection
                };
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    AdminList a = new()
                    {
                        Id = reader.GetInt32(0),
                        User = reader.GetString(1),
                        ThemeFontSize = reader.GetInt16(9)
                    };
                    if (a.User == user)
                    {
                        returnThis = a.ThemeFontSize;
                    }
                }

                AppLogger.Log("Primary_ChangeLog_Get_Visual_Theme_FontSize executed successfully for user: " + user);
            }
            catch (MySqlException ex)
            {
                AppLogger.LogDatabaseError(ex);
                AppLogger.Log("Error in Primary_ChangeLog_Get_Visual_Theme_FontSize: " + ex.Message);
                HandleException_SQLError_CloseApp(ex);
                return returnThis;
            }
            catch (Exception ex)
            {
                AppLogger.LogDatabaseError(ex);
                AppLogger.Log("Error in Primary_ChangeLog_Get_Visual_Theme_FontSize: " + ex.Message);
                HandleException_GeneralError_CloseApp(ex);
                return returnThis;
            }
            finally
            {
                reader?.Close();
                reader?.Dispose();
                command?.Dispose();
                connection?.Close();
                connection?.Dispose();
            }

            return returnThis;
        }

        internal static void Primary_ChangeLog_Set_Theme_FontSize(string value, string user)
        {
            MySqlConnection connection = null;
            MySqlCommand command = null;
            try
            {
                connection = new MySqlConnection(GetConnectionString(null, null, null, null));
                command = new MySqlCommand
                {
                    CommandText = $@"UPDATE `users` SET `Theme_FontSize` = '{value}' WHERE `users`.`user` = '{user}';",
                    Connection = connection
                };
                connection.Open();
                command.ExecuteNonQuery();
                AppLogger.Log("Primary_ChangeLog_Set_Theme_FontSize executed successfully for user: " + user);
            }
            catch (MySqlException ex)
            {
                AppLogger.LogDatabaseError(ex);
                AppLogger.Log("Error in Primary_ChangeLog_Set_Theme_FontSize: " + ex.Message);
                HandleException_SQLError_CloseApp(ex);
            }
            catch (Exception ex)
            {
                AppLogger.LogDatabaseError(ex);
                AppLogger.Log("Error in Primary_ChangeLog_Set_Theme_FontSize: " + ex.Message);
                HandleException_GeneralError_CloseApp(ex);
            }
            finally
            {
                command?.Dispose();
                connection?.Close();
                connection?.Dispose();
            }
        }

        internal static void Primary_ChangeLog_Set_Theme_Name(string value, string user)
        {
            MySqlConnection connection = null;
            MySqlCommand command = null;
            try
            {
                connection = new MySqlConnection(GetConnectionString(null, null, null, null));
                command = new MySqlCommand
                {
                    CommandText = $@"UPDATE `users` SET `Theme_Name` = '{value}' WHERE `users`.`user` = '{user}';",
                    Connection = connection
                };
                connection.Open();
                command.ExecuteNonQuery();
                AppLogger.Log("Primary_ChangeLog_Set_Theme_Name executed successfully for user: " + user);
            }
            catch (MySqlException ex)
            {
                AppLogger.LogDatabaseError(ex);
                AppLogger.Log("Error in Primary_ChangeLog_Set_Theme_Name: " + ex.Message);
                HandleException_SQLError_CloseApp(ex);
            }
            catch (Exception ex)
            {
                AppLogger.LogDatabaseError(ex);
                AppLogger.Log("Error in Primary_ChangeLog_Set_Theme_Name: " + ex.Message);
                HandleException_GeneralError_CloseApp(ex);
            }
            finally
            {
                command?.Dispose();
                connection?.Close();
                connection?.Dispose();
            }
        }

        internal static void Primary_ChangeLog_Set_Visual_Password(string value, string user)
        {
            MySqlConnection connection = null;
            MySqlCommand command = null;
            try
            {
                connection = new MySqlConnection(GetConnectionString(null, null, null, null));
                command = new MySqlCommand
                {
                    CommandText = $@"UPDATE `users` SET `VisualPassword` = '{value}' WHERE `users`.`user` = '{user}';",
                    Connection = connection
                };
                connection.Open();
                command.ExecuteNonQuery();
                AppLogger.Log("Primary_ChangeLog_Set_Visual_Password executed successfully for user: " + user);
            }
            catch (MySqlException ex)
            {
                AppLogger.LogDatabaseError(ex);
                AppLogger.Log("Error in Primary_ChangeLog_Set_Visual_Password: " + ex.Message);
                HandleException_SQLError_CloseApp(ex);
            }
            catch (Exception ex)
            {
                AppLogger.LogDatabaseError(ex);
                AppLogger.Log("Error in Primary_ChangeLog_Set_Visual_Password: " + ex.Message);
                HandleException_GeneralError_CloseApp(ex);
            }
            finally
            {
                command?.Dispose();
                connection?.Close();
                connection?.Dispose();
            }
        }

        internal static void Primary_ChangeLog_Set_Visual_User(string value, string user)
        {
            MySqlConnection connection = null;
            MySqlCommand command = null;
            try
            {
                connection = new MySqlConnection(GetConnectionString(null, null, null, null));
                command = new MySqlCommand
                {
                    CommandText = $@"UPDATE `users` SET `VisualUserName` = '{value}' WHERE `users`.`user` = '{user}';",
                    Connection = connection
                };
                connection.Open();
                command.ExecuteNonQuery();
                AppLogger.Log("Primary_ChangeLog_Set_Visual_User executed successfully for user: " + user);
            }
            catch (MySqlException ex)
            {
                AppLogger.LogDatabaseError(ex);
                AppLogger.Log("Error in Primary_ChangeLog_Set_Visual_User: " + ex.Message);
                HandleException_SQLError_CloseApp(ex);
            }
            catch (Exception ex)
            {
                AppLogger.LogDatabaseError(ex);
                AppLogger.Log("Error in Primary_ChangeLog_Set_Visual_User: " + ex.Message);
                HandleException_GeneralError_CloseApp(ex);
            }
            finally
            {
                command?.Dispose();
                connection?.Close();
                connection?.Dispose();
            }
        }

        internal static string Primary_ChangeLog_Get_Toggle()
        {
            MySqlConnection connection = null;
            MySqlCommand command = null;
            MySqlDataReader reader = null;
            var user = WipAppVariables.User;
            var returnThis = "false";
            try
            {
                connection = new MySqlConnection(GetConnectionString(null, null, null, null));
                connection.Open();
                command = new MySqlCommand
                {
                    CommandText = "SELECT * FROM `users`",
                    Connection = connection
                };
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    AdminList a = new()
                    {
                        Id = reader.GetInt32(0),
                        User = reader.GetString(1),
                        HideChangeLog = reader.GetString(4)
                    };
                    if (a.User == user)
                    {
                        returnThis = a.HideChangeLog;
                    }
                }

                AppLogger.Log("Primary_ChangeLog_Get_Toggle executed successfully for user: " + user);
            }
            catch (MySqlException ex)
            {
                AppLogger.LogDatabaseError(ex);
                AppLogger.Log("Error in Primary_ChangeLog_Get_Toggle: " + ex.Message);
                HandleException_SQLError_CloseApp(ex);
                return returnThis;
            }
            catch (Exception ex)
            {
                AppLogger.LogDatabaseError(ex);
                AppLogger.Log("Error in Primary_ChangeLog_Get_Toggle: " + ex.Message);
                HandleException_GeneralError_CloseApp(ex);
                return returnThis;
            }
            finally
            {
                reader?.Close();
                reader?.Dispose();
                command?.Dispose();
                connection?.Close();
                connection?.Dispose();
            }

            return returnThis;
        }

        internal static string Primary_ChangeLog_Get_LastShown()
        {
            MySqlConnection connection = null;
            MySqlCommand command = null;
            MySqlDataReader reader = null;
            var user = WipAppVariables.User;
            var returnThis = "0.0.0.0";
            try
            {
                connection = new MySqlConnection(GetConnectionString(null, null, null, null));
                connection.Open();
                command = new MySqlCommand
                {
                    CommandText = "SELECT * FROM `users`",
                    Connection = connection
                };
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    AdminList a = new()
                    {
                        Id = reader.GetInt32(0),
                        User = reader.GetString(1),
                        LastShownVersion = reader.GetString(5)
                    };
                    if (a.User == user)
                    {
                        returnThis = a.LastShownVersion;
                    }
                }

                AppLogger.Log("Primary_ChangeLog_Get_LastShown executed successfully for user: " + user);
            }
            catch (MySqlException ex)
            {
                AppLogger.LogDatabaseError(ex);
                AppLogger.Log("Error in Primary_ChangeLog_Get_LastShown: " + ex.Message);
                HandleException_SQLError_CloseApp(ex);
                return returnThis;
            }
            catch (Exception ex)
            {
                AppLogger.LogDatabaseError(ex);
                AppLogger.Log("Error in Primary_ChangeLog_Get_LastShown: " + ex.Message);
                HandleException_GeneralError_CloseApp(ex);
                return returnThis;
            }
            finally
            {
                reader?.Close();
                reader?.Dispose();
                command?.Dispose();
                connection?.Close();
                connection?.Dispose();
            }

            return returnThis;
        }

        internal static string Primary_ChangeLog_Get_VersionNotes(string? version = null)
        {
            MySqlConnection connection = null;
            MySqlCommand command = null;
            MySqlDataReader reader = null;
            var v = version ?? WipAppVariables.Version;
            var returnThis = "";
            try
            {
                connection = new MySqlConnection(GetConnectionString(null, null, null, null));
                connection.Open();
                command = new MySqlCommand
                {
                    CommandText = "SELECT `Notes` FROM `wipapp_version_history` WHERE `Version` = @version",
                    Connection = connection
                };
                command.Parameters.AddWithValue("@version", v);
                reader = command.ExecuteReader();
                if (reader.Read())
                {
                    returnThis = reader.GetString(0);
                }

                AppLogger.Log("Primary_ChangeLog_Get_VersionNotes executed successfully for version: " + v);
            }
            catch (MySqlException ex)
            {
                AppLogger.LogDatabaseError(ex);
                AppLogger.Log("Error in Primary_ChangeLog_Get_VersionNotes: " + ex.Message);
                HandleException_SQLError_CloseApp(ex);
                return returnThis;
            }
            catch (Exception ex)
            {
                AppLogger.LogDatabaseError(ex);
                AppLogger.Log("Error in Primary_ChangeLog_Get_VersionNotes: " + ex.Message);
                HandleException_GeneralError_CloseApp(ex);
                return returnThis;
            }
            finally
            {
                reader?.Close();
                reader?.Dispose();
                command?.Dispose();
                connection?.Close();
                connection?.Dispose();
            }

            return returnThis;
        }

        internal static void HandleException_SQLError_CloseApp(Exception ex,
            [System.Runtime.CompilerServices.CallerMemberName]
            string callerName = "")
        {
            try
            {
                // Log the error details
                AppLogger.Log($"SQL Error in method: {callerName}");
                AppLogger.Log($"Exception Message: {ex.Message}");
                AppLogger.Log($"Stack Trace: {ex.StackTrace}");

                if (ex is MySqlException mysqlEx)
                {
                    AppLogger.Log($"MySQL Error Code: {mysqlEx.Number}");
                    AppLogger.Log($"MySQL Error Details: {mysqlEx.Message}");
                }

                // Check if the error is related to connection issues
                var isConnectionError = ex.Message.Contains("Unable to connect to any of the specified MySQL hosts.") ||
                                        ex.Message.Contains("Access denied for user") ||
                                        ex.Message.Contains("Can't connect to MySQL server on") ||
                                        ex.Message.Contains("Unknown MySQL server host") ||
                                        ex.Message.Contains("Lost connection to MySQL server") ||
                                        ex.Message.Contains("MySQL server has gone away");

                if (isConnectionError)
                {
                    // Notify the user and close the application
                    MessageBox.Show(@"Database connection error. The application will now close.", @"Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Process.GetCurrentProcess().Kill();
                }
                else
                {
                    // Log the error to the database
                    var connectionString = GetConnectionString(null, null, null, null);
                    using var connection = new MySqlConnection(connectionString);
                    connection.Open();
                    using var command = new MySqlCommand(
                        "INSERT INTO `wipapp_errorlog` (`Method`, `Error`, `User`, `DateTime`) VALUES (@Method, @Error, @User, @DateTime)",
                        connection);
                    command.Parameters.AddWithValue("@Method", callerName);
                    command.Parameters.AddWithValue("@Error", ex.Message);
                    command.Parameters.AddWithValue("@User", WipAppVariables.User);
                    command.Parameters.AddWithValue("@DateTime", DateTime.Now);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception innerEx)
            {
                AppLogger.Log($"Error while handling exception: {innerEx.Message}");
            }
        }


        internal static void HandleException_GeneralError_CloseApp(Exception ex,
            [System.Runtime.CompilerServices.CallerMemberName]
            string callerName = "")
        {
            try
            {
                var errorType = ex switch
                {
                    ArgumentNullException => "A required argument was null.",
                    ArgumentOutOfRangeException => "An argument was out of range.",
                    InvalidOperationException => "An invalid operation occurred.",
                    FormatException => "A format error occurred.",
                    NullReferenceException => "A null reference occurred.",
                    OutOfMemoryException => "The application ran out of memory.",
                    StackOverflowException => "A stack overflow occurred.",
                    AccessViolationException => "An access violation occurred.",
                    _ => "An unexpected error occurred."
                };

                var message = $"{errorType}\nMethod: {callerName}\nException:\n{ex.Message}";

                var isCritical = ex is OutOfMemoryException || ex is StackOverflowException ||
                                 ex is AccessViolationException;

                if (Application.OpenForms.OfType<MainForm>().Any())
                {
                    var mainForm = Application.OpenForms.OfType<MainForm>().First();
                    mainForm.Invoke(() =>
                    {
                        mainForm.MainForm_StatusStrip_Disconnected.Visible = true;
                        mainForm.MainForm_StatusStrip_SavedStatus.Visible = false;
                        foreach (Control c in mainForm.Controls)
                        {
                            c.Enabled = false;
                        }
                    });
                }

                var connectionString = GetConnectionString(null, null, null, null);
                using (var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    using (var command = new MySqlCommand(
                               "INSERT INTO `wipapp_errorlog` (`Method`, `Error`, `User`, `DateTime`) VALUES (@Method, @Error, @User, @DateTime)",
                               connection))
                    {
                        command.Parameters.AddWithValue("@Method", callerName);
                        command.Parameters.AddWithValue("@Error", ex.Message);
                        command.Parameters.AddWithValue("@User", WipAppVariables.User);
                        command.Parameters.AddWithValue("@DateTime", DateTime.Now);
                        command.ExecuteNonQuery();
                    }
                }

                if (isCritical)
                {
                    MessageBox.Show(message + """

                                              The application will now close due to a critical error.
                                              """,
                        @"Critical Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Process.GetCurrentProcess().Kill();
                }
                else
                {
                    MessageBox.Show(message, @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                AppLogger.Log("HandleException_GeneralError_CloseApp executed successfully.");
            }
            catch (Exception innerEx)
            {
                HandleException_GeneralError_CloseApp(innerEx);
            }
        }

        internal static string System_GetUserName()
        {
            if (Program.enteredUser == "Default User")
            {
                var userIdWithDomain = WindowsIdentity.GetCurrent().Name;
                string result;
                var posSlash = userIdWithDomain.IndexOf('\\');

                if (posSlash == -1)
                {
                    result = userIdWithDomain;
                }
                else
                {
                    result = userIdWithDomain.Substring(posSlash + 1, userIdWithDomain.Length - (posSlash + 1));
                }

                return result.ToUpper();
            }
            else
            {
                var userIdWithDomain = Program.enteredUser;
                string result;

                if (userIdWithDomain == null)
                {
                    throw new InvalidOperationException("User identity could not be retrieved.");
                }

                var posSlash = userIdWithDomain.IndexOf('\\');


                if (posSlash == -1)
                {
                    result = userIdWithDomain;
                }
                else
                {
                    result = userIdWithDomain.Substring(posSlash + 1, userIdWithDomain.Length - (posSlash + 1));
                }

                return result.ToUpper();
            }
        }

        internal static List<AdminList> System_UserAccessType()
        {
            MySqlConnection connection = null;
            MySqlCommand command = null;
            MySqlDataReader reader = null;
            MySqlConnection connection2 = null;
            MySqlCommand command2 = null;
            MySqlDataReader reader2 = null;
            try
            {
                var user = WipAppVariables.User;
                List<AdminList> returnThese = [];

                connection = new MySqlConnection(GetConnectionString(null, null, null, null));
                connection.Open();
                command = new MySqlCommand
                {
                    CommandText = "SELECT * FROM `leads`",
                    Connection = connection
                };
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    AdminList a = new()
                    {
                        Id = reader.GetInt32(0),
                        User = reader.GetString(1)
                    };
                    if (a.User == user)
                    {
                        WipAppVariables.userTypeAdmin = true;
                        returnThese.Add(a);
                    }
                    else
                    {
                        returnThese.Add(a);
                    }
                }

                connection2 = new MySqlConnection(GetConnectionString(null, null, null, null));
                connection2.Open();
                command2 = new MySqlCommand
                {
                    CommandText = "SELECT * FROM `readonly`",
                    Connection = connection2
                };
                reader2 = command2.ExecuteReader();
                while (reader2.Read())
                {
                    AdminList a = new()
                    {
                        Id = reader2.GetInt32(0),
                        User = reader2.GetString(1)
                    };
                    if (a.User == user)
                    {
                        WipAppVariables.userTypeReadOnly = true;
                        returnThese.Add(a);
                    }
                    else
                    {
                        returnThese.Add(a);
                    }
                }

                AppLogger.Log("System_UserAccessType executed successfully for user: " + user);
                return returnThese;
            }
            catch (MySqlException ex)
            {
                AppLogger.LogDatabaseError(ex);
                AppLogger.Log("Error in System_UserAccessType: " + ex.Message);
                HandleException_SQLError_CloseApp(ex);
                return [];
            }
            catch (Exception ex)
            {
                AppLogger.LogDatabaseError(ex);
                AppLogger.Log("Error in System_UserAccessType: " + ex.Message);
                HandleException_GeneralError_CloseApp(ex);
                return [];
            }
            finally
            {
                reader2?.Close();
                reader2?.Dispose();
                command2?.Dispose();
                connection2?.Close();
                connection2?.Dispose();
                reader?.Close();
                reader?.Dispose();
                command?.Dispose();
                connection?.Close();
                connection?.Dispose();
            }
        }

        internal static void System_Last10_Buttons_Changed()
        {
            try
            {
                var com1 = "INSERT INTO `last_10_transactions`(`ID`, `PartID`, `Op`, `Quantity`) VALUES(11, '" +
                           WipAppVariables.partId + "', '" + WipAppVariables.Operation + "', '" +
                           WipAppVariables.InventoryQuantity + "');";
                var com2 = "DELETE FROM `last_10_transactions` WHERE `ID` = 10;";
                var com3 = "ALTER TABLE  `mtm database`.`last_10_transactions` MODIFY COLUMN `ID` INT;";
                var com4 = "ALTER TABLE  `mtm database`.`last_10_transactions` DROP PRIMARY KEY;";
                var com5 =
                    "UPDATE `mtm database`.`last_10_transactions` SET `mtm database`.`last_10_transactions`.`ID` = `ID` +1 LIMIT 9;";
                var com6 = "ALTER TABLE  `mtm database`.`last_10_transactions` ADD PRIMARY KEY(id);";
                var com7 = "ALTER TABLE  `mtm database`.`last_10_transactions` MODIFY COLUMN `ID` INT AUTO_INCREMENT;";
                var com8 = "UPDATE `last_10_transactions` SET `ID`= 1 WHERE `ID` = 11;";
                var connectionString = GetConnectionString(null, null, null, null);

                using (var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    using (var command = new MySqlCommand(com1 + com2 + com3 + com4 + com5 + com6 + com7 + com8,
                               connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }

                AppLogger.Log("System_Last10_Buttons_Changed executed successfully.");
            }
            catch (MySqlException ex)
            {
                AppLogger.LogDatabaseError(ex);
                AppLogger.Log("Error in System_Last10_Buttons_Changed: " + ex.Message);
                HandleException_SQLError_CloseApp(ex);
            }
            catch (Exception ex)
            {
                AppLogger.LogDatabaseError(ex);
                AppLogger.Log("Error in System_Last10_Buttons_Changed: " + ex.Message);
                HandleException_GeneralError_CloseApp(ex);
            }
            finally
            {
                if (Application.OpenForms.OfType<MainForm>().Any())
                {
                    var mainForm = Application.OpenForms.OfType<MainForm>().First();
                    mainForm.Invoke(() =>
                    {
                        mainForm.MainForm_StatusStrip_Disconnected.Visible = false;
                        mainForm.MainForm_StatusStrip_SavedStatus.Visible = true;
                        mainForm.Enabled = true;
                    });
                }
            }
        }

        internal static List<Search>? InventoryTab_Save()
        {
            try
            {
                List<Search> returnThese = [];
                var type = InventoryTab_GetItemType();
                var connectionString = GetConnectionString(null, null, null, null);

                using (var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    using (var command = new MySqlCommand(
                               "INSERT INTO `saved_locations` (`ID`, `Location`, `Item Number`, `Op`, `Notes`, `Quantity`, `Date_Time`, `User` , `Item Type`) VALUES (NULL, '" +
                               WipAppVariables.Location + "', '" + WipAppVariables.partId + "', '" +
                               WipAppVariables.Operation + "', '" + WipAppVariables.Notes + "', '" +
                               WipAppVariables.InventoryQuantity + "', CURRENT_TIMESTAMP, '" +
                               WipAppVariables.User + "','" +
                               type + "');", connection))
                    {
                        command.ExecuteNonQuery();
                    }

                    using (var command2 = new MySqlCommand(
                               "INSERT INTO `input_history` (`User`, `Part ID`, `Location`, `Type`, `Quantity`) VALUES('" +
                               WipAppVariables.User + "', '" + WipAppVariables.partId + "', '" +
                               WipAppVariables.Location + "', '" + WipAppVariables.PartType + "', '" +
                               WipAppVariables.InventoryQuantity + "')", connection))
                    {
                        command2.ExecuteNonQuery();
                    }
                }

                AppLogger.Log("InventoryTab_Save executed successfully.");
                return returnThese;
            }
            catch (MySqlException ex)
            {
                AppLogger.LogDatabaseError(ex);
                AppLogger.Log("Error in InventoryTab_Save: " + ex.Message);
                HandleException_SQLError_CloseApp(ex);
                return null;
            }
            catch (Exception ex)
            {
                AppLogger.LogDatabaseError(ex);
                AppLogger.Log("Error in InventoryTab_Save: " + ex.Message);
                HandleException_GeneralError_CloseApp(ex);
                return null;
            }
        }

        internal static string InventoryTab_GetItemType()
        {
            try
            {
                var connectionString = GetConnectionString(null, null, null, null);
                using (var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    using (var command = new MySqlCommand("SELECT * FROM `part_ids` WHERE `Item Number` = @PartID",
                               connection))
                    {
                        command.Parameters.AddWithValue("@PartID", WipAppVariables.partId);
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var partId = reader.GetString(0);
                                if (partId == WipAppVariables.partId)
                                {
                                    return reader.GetString(3);
                                }
                            }
                        }
                    }
                }

                AppLogger.Log("InventoryTab_GetItemType executed successfully for part ID: " + WipAppVariables.partId);
                return "Unknown";
            }
            catch (MySqlException ex)
            {
                AppLogger.LogDatabaseError(ex);
                AppLogger.Log("Error in InventoryTab_GetItemType: " + ex.Message);
                HandleException_SQLError_CloseApp(ex);
                return "Unknown";
            }
            catch (Exception ex)
            {
                AppLogger.LogDatabaseError(ex);
                AppLogger.Log("Error in InventoryTab_GetItemType: " + ex.Message);
                HandleException_GeneralError_CloseApp(ex);
                return "Unknown";
            }
        }

        internal static async Task<List<Search>> RemoveTab_SearchAsync(string searchTerm, string opTerm, string opIndex)
        {
            List<Search> returnThese = [];
            try
            {
                _ = new MainForm();
                WipAppVariables.PrintTitle = opIndex == "0"
                    ? $"{searchTerm} | Operation : All"
                    : $"{searchTerm} : Operation : {WipAppVariables.PrintOp}";

                var connectionString = GetConnectionString(null, null, null, null);
                using var connection = new MySqlConnection(connectionString);
                await connection.OpenAsync();

                var searchWildPhase = "%" + searchTerm + "%";
                var opWildPhase = "%" + opTerm + "%";

                if (searchWildPhase == "%%")
                {
                    searchWildPhase = "% %";
                }

                if (opWildPhase == "%%")
                {
                    opWildPhase = "%[ Enter Op # ]%";
                }

                using var command = new MySqlCommand();
                command.Connection = connection;
                command.CommandText = opWildPhase != "%[ Enter Op # ]%"
                    ? "SELECT * FROM `saved_locations` WHERE `Item Number` LIKE @search AND `Op` LIKE @op ORDER BY `Location` ASC, `Op` ASC"
                    : "SELECT * FROM `saved_locations` WHERE `Item Number` LIKE @search ORDER BY `Location` ASC, `Op` ASC";
                command.Parameters.AddWithValue("@search", searchWildPhase);
                command.Parameters.AddWithValue("@op", opWildPhase);

                using var reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    Search a = new()
                    {
                        Id = reader.GetInt32(0),
                        Location = reader.GetString(1),
                        PartId = reader.GetString(2),
                        Op = reader.GetString(3),
                        Notes = reader.GetString(4),
                        Quantity = reader.GetInt32(5),
                        Date = reader.GetDateTime(6),
                        User = reader.GetString(7),
                        Type = reader.GetString(8)
                    };
                    returnThese.Add(a);
                }

                AppLogger.Log("RemoveTab_SearchAsync executed successfully for searchTerm: " + searchTerm);
            }
            catch (MySqlException ex)
            {
                AppLogger.LogDatabaseError(ex);
                AppLogger.Log("Error in RemoveTab_SearchAsync: " + ex.Message);
                HandleException_SQLError_CloseApp(ex);
            }

            return returnThese;
        }

        internal static List<Search>? RemoveTab_Delete()
        {
            try
            {
                List<Search> returnThese = [];
                var connectionString = GetConnectionString(null, null, null, null);
                using (var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    using (var command = new MySqlCommand(
                               "DELETE FROM saved_locations WHERE `saved_locations`.`ID` = @RemoveID", connection))
                    {
                        command.Parameters.AddWithValue("@RemoveID", WipAppVariables.removeId);
                        command.ExecuteNonQuery();
                    }

                    using (var command2 = new MySqlCommand(
                               "INSERT INTO `output_history` (`User`, `Part ID`, `Location`, `Quantity`) VALUES(@User, @PartID, @Location, @Quantity)",
                               connection))
                    {
                        command2.Parameters.AddWithValue("@User", WipAppVariables.RemoveUser);
                        command2.Parameters.AddWithValue("@PartID", WipAppVariables.RemovePartNumber);
                        command2.Parameters.AddWithValue("@Location", WipAppVariables.RemoveLocation);
                        command2.Parameters.AddWithValue("@Quantity", WipAppVariables.RemoveQuantity);
                        command2.ExecuteNonQuery();
                    }
                }

                AppLogger.Log("RemoveTab_Delete executed successfully for RemoveID: " + WipAppVariables.removeId);
                return returnThese;
            }
            catch (MySqlException ex)
            {
                AppLogger.LogDatabaseError(ex);
                AppLogger.Log("Error in RemoveTab_Delete: " + ex.Message);
                HandleException_SQLError_CloseApp(ex);
                return null;
            }
            catch (Exception ex)
            {
                AppLogger.LogDatabaseError(ex);
                AppLogger.Log("Error in RemoveTab_Delete: " + ex.Message);
                HandleException_GeneralError_CloseApp(ex);
                return null;
            }
        }

        internal static async Task<List<Search>> RemoveTab_GetAllLocationsAsync()
        {
            WipAppVariables.PrintTitle = "All Part Numbers";
            List<Search> returnThese = [];
            try
            {
                var connectionString = GetConnectionString(null, null, null, null);
                using var connection = new MySqlConnection(connectionString);
                await connection.OpenAsync();
                using var command = new MySqlCommand("SELECT * FROM saved_locations ORDER BY `Location` ASC, `Op` ASC",
                    connection);
                using var reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    Search a = new()
                    {
                        Id = reader.GetInt32(0),
                        Location = reader.GetString(1),
                        PartId = reader.GetString(2),
                        Op = reader.GetString(3),
                        Notes = reader.GetString(4),
                        Quantity = reader.GetInt32(5),
                        Date = reader.GetDateTime(6),
                        User = reader.GetString(7),
                        Type = reader.GetString(8)
                    };
                    returnThese.Add(a);
                }

                AppLogger.Log("RemoveTab_GetAllLocationsAsync executed successfully.");
            }
            catch (MySqlException ex)
            {
                AppLogger.LogDatabaseError(ex);
                AppLogger.Log("Error in RemoveTab_GetAllLocationsAsync: " + ex.Message);
                HandleException_SQLError_CloseApp(ex);
            }

            return returnThese;
        }

        internal static async Task<List<Search>> RemoveTab_SearchByTypeAsync(string searchTerm)
        {
            List<Search> returnThese = [];
            _ = new MainForm();
            WipAppVariables.PrintTitle = "Search By Type: " + searchTerm;
            try
            {
                var connectionString = GetConnectionString(null, null, null, null);
                using var connection = new MySqlConnection(connectionString);
                await connection.OpenAsync();
                var searchWildPhase = "%" + searchTerm + "%";
                if (searchWildPhase == "%%")
                {
                    searchWildPhase = "% %";
                }

                using var command = new MySqlCommand(
                    "SELECT * FROM `saved_locations` WHERE `Item Type` LIKE @search ORDER BY `Location` ASC, `Op` ASC",
                    connection);
                command.Parameters.AddWithValue("@search", searchWildPhase);
                using var reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    Search a = new()
                    {
                        Id = reader.GetInt32(0),
                        Location = reader.GetString(1),
                        PartId = reader.GetString(2),
                        Op = reader.GetString(3),
                        Notes = reader.GetString(4),
                        Quantity = reader.GetInt32(5),
                        Date = reader.GetDateTime(6),
                        User = reader.GetString(7),
                        Type = reader.GetString(8)
                    };
                    returnThese.Add(a);
                }

                AppLogger.Log("RemoveTab_SearchByTypeAsync executed successfully for searchTerm: " + searchTerm);
            }
            catch (MySqlException ex)
            {
                AppLogger.LogDatabaseError(ex);
                AppLogger.Log("Error in RemoveTab_SearchByTypeAsync: " + ex.Message);
                HandleException_SQLError_CloseApp(ex);
            }

            return returnThese;
        }

        internal static async Task<List<Search>> RemovalTab_GetOutsideServiceAsync()
        {
            List<Search> returnThese = [];
            try
            {
                WipAppVariables.PrintTitle = "All Outside Service";
                var connectionString = GetConnectionString(null, null, null, null);
                using var connection = new MySqlConnection(connectionString);
                await connection.OpenAsync();
                using var command = new MySqlCommand(
                    "SELECT * FROM saved_locations WHERE `Op` = 900 ORDER BY `Location` ASC, `Op` ASC",
                    connection);
                using var reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    Search a = new()
                    {
                        Id = reader.GetInt32(0),
                        Location = reader.GetString(1),
                        PartId = reader.GetString(2),
                        Op = reader.GetString(3),
                        Notes = reader.GetString(4),
                        Quantity = reader.GetInt32(5),
                        Date = reader.GetDateTime(6),
                        User = reader.GetString(7)
                    };
                    returnThese.Add(a);
                }

                AppLogger.Log("RemovalTab_GetOutsideServiceAsync executed successfully.");
            }
            catch (MySqlException ex)
            {
                AppLogger.LogDatabaseError(ex);
                AppLogger.Log("Error in RemovalTab_GetOutsideServiceAsync: " + ex.Message);
                HandleException_SQLError_CloseApp(ex);
            }

            return returnThese;
        }

        internal static void RemovalTab_EditNotes(int id, string partId, string notes)
        {
            MySqlConnection connection = null;
            MySqlCommand command = null;
            try
            {
                var connectionString = GetConnectionString(null, null, null, null);
                connection = new MySqlConnection(connectionString);
                connection.Open();
                command = new MySqlCommand(
                    "UPDATE `saved_locations` SET `Notes` = @notes WHERE `saved_locations`.`ID` = @id AND `Item Number` = @partId",
                    connection);
                command.Parameters.AddWithValue("@notes", notes);
                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@partId", partId);
                command.ExecuteNonQuery();
                AppLogger.Log("RemovalTab_EditNotes executed successfully for ID: " + id);
            }
            catch (MySqlException ex)
            {
                AppLogger.LogDatabaseError(ex);
                AppLogger.Log("Error in RemovalTab_EditNotes: " + ex.Message);
                HandleException_SQLError_CloseApp(ex);
            }
            catch (Exception ex)
            {
                AppLogger.LogDatabaseError(ex);
                AppLogger.Log("Error in RemovalTab_EditNotes: " + ex.Message);
                HandleException_GeneralError_CloseApp(ex);
            }
            finally
            {
                command?.Dispose();
                connection?.Close();
                connection?.Dispose();
            }
        }

        internal static async Task<List<Search>> TransferTab_SearchAsync(string searchTerm)
        {
            WipAppVariables.PrintTitle = searchTerm;
            List<Search> returnThese = [];
            try
            {
                var connectionString = GetConnectionString(null, null, null, null);
                using var connection = new MySqlConnection(connectionString);
                await connection.OpenAsync();
                var searchWildPhase = "%" + searchTerm + "%";
                using var command = new MySqlCommand(
                    "SELECT * FROM `saved_locations` WHERE `Item Number` LIKE @search ORDER BY `Location` ASC, `Op` ASC",
                    connection);
                command.Parameters.AddWithValue("@search", searchWildPhase);
                using var reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    Search a = new()
                    {
                        Id = reader.GetInt32(0),
                        Location = reader.GetString(1),
                        PartId = reader.GetString(2),
                        Op = reader.GetString(3),
                        Notes = reader.GetString(4),
                        Quantity = reader.GetInt32(5),
                        Date = reader.GetDateTime(6),
                        User = reader.GetString(7),
                        Type = reader.GetString(8)
                    };
                    returnThese.Add(a);
                }

                AppLogger.Log("TransferTab_SearchAsync executed successfully for searchTerm: " + searchTerm);
            }
            catch (MySqlException ex)
            {
                AppLogger.LogDatabaseError(ex);
                AppLogger.Log("Error in TransferTab_SearchAsync: " + ex.Message);
                HandleException_SQLError_CloseApp(ex);
            }

            return returnThese;
        }

        public static void TransferTab_Save_NonSplit(string newLocation, string id,
            string oldLocation, string partId, string quantity, string type)
        {
            try
            {
                var user = WipAppVariables.User;
                var connectionString = GetConnectionString(null, null, null, null);
                using (var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    using (var command = new MySqlCommand(
                               "UPDATE `saved_locations` SET `Location` = @newLocation, `User` = @User, `Date_Time` = CURRENT_TIMESTAMP WHERE `Item Number` = @partId AND `Location` = @oldLocation LIMIT 1;",
                               connection))
                    {
                        command.Parameters.AddWithValue("@newLocation", newLocation);
                        command.Parameters.AddWithValue("@User", user);
                        command.Parameters.AddWithValue("@partId", partId);
                        command.Parameters.AddWithValue("@oldLocation", oldLocation);
                        command.ExecuteNonQuery();
                    }

                    using (var command2 = new MySqlCommand(
                               "INSERT INTO `input_history` (`User`, `Part ID`, `Location`, `Type`, `Quantity`) VALUES(@User, @partId, @newLocation, @type, @quantity);",
                               connection))
                    {
                        command2.Parameters.AddWithValue("@User", user);
                        command2.Parameters.AddWithValue("@partId", partId);
                        command2.Parameters.AddWithValue("@newLocation", newLocation);
                        command2.Parameters.AddWithValue("@type", type);
                        command2.Parameters.AddWithValue("@quantity", quantity);
                        command2.ExecuteNonQuery();
                    }
                }

                AppLogger.Log("TransferTab_Save_NonSplit executed successfully for partId: " + partId);
                WipAppVariables.mainFormFormReset = true;
            }
            catch (MySqlException ex)
            {
                AppLogger.LogDatabaseError(ex);
                AppLogger.Log("Error in TransferTab_Save_NonSplit: " + ex.Message);
                HandleException_SQLError_CloseApp(ex);
            }
            catch (Exception ex)
            {
                AppLogger.LogDatabaseError(ex);
                AppLogger.Log("Error in TransferTab_Save_NonSplit: " + ex.Message);
                HandleException_GeneralError_CloseApp(ex);
            }
        }

        public static void TransferTab_Save_Split(string newLocation, string inventoryId, string oldLocation,
            string partId, string quantity, string type, string op, string notes, string diff, string partType)
        {
            try
            {
                var user = WipAppVariables.User;
                var connectionString = GetConnectionString(null, null, null, null);
                using (var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    using (var command = new MySqlCommand(
                               "UPDATE `saved_locations` SET `Location` = @oldLocation, `quantity` = @quantity, `Date_Time` = CURRENT_TIMESTAMP WHERE `ID` LIKE @inventoryId LIMIT 1;",
                               connection))
                    {
                        command.Parameters.AddWithValue("@oldLocation", oldLocation);
                        command.Parameters.AddWithValue("@quantity", diff);
                        command.Parameters.AddWithValue("@inventoryId", inventoryId);
                        command.ExecuteNonQuery();
                    }

                    using (var command2 = new MySqlCommand(
                               "INSERT INTO `input_history` (`User`, `Part ID`, `Location`, `Type`, `Quantity`) VALUES(@User, @partId, @newLocation, @type, @diff)",
                               connection))
                    {
                        command2.Parameters.AddWithValue("@User", user);
                        command2.Parameters.AddWithValue("@partId", partId);
                        command2.Parameters.AddWithValue("@newLocation", newLocation);
                        command2.Parameters.AddWithValue("@type", type);
                        command2.Parameters.AddWithValue("@diff", quantity);
                        command2.ExecuteNonQuery();
                    }

                    using (var command3 = new MySqlCommand(
                               "INSERT INTO `saved_locations` (`ID`, `Location`, `Item Number`, `op`, `Notes`, `quantity`, `Date_Time`, `User` , `Item type`) VALUES (NULL, @newLocation, @partId, @op, @notes, @diff, CURRENT_TIMESTAMP, @User, @type)",
                               connection))
                    {
                        command3.Parameters.AddWithValue("@newLocation", newLocation);
                        command3.Parameters.AddWithValue("@partId", partId);
                        command3.Parameters.AddWithValue("@op", op);
                        command3.Parameters.AddWithValue("@notes", notes);
                        command3.Parameters.AddWithValue("@diff", quantity);
                        command3.Parameters.AddWithValue("@User", user);
                        command3.Parameters.AddWithValue("@type", partType);
                        command3.ExecuteNonQuery();
                    }
                }

                AppLogger.Log("TransferTab_Save_Split executed successfully for partId: " + partId);
                WipAppVariables.mainFormFormReset = true;
            }
            catch (MySqlException ex)
            {
                AppLogger.LogDatabaseError(ex);
                AppLogger.Log("Error in TransferTab_Save_Split: " + ex.Message);
                HandleException_SQLError_CloseApp(ex);
            }
            catch (Exception ex)
            {
                AppLogger.LogDatabaseError(ex);
                AppLogger.Log("Error in TransferTab_Save_Split: " + ex.Message);
                HandleException_GeneralError_CloseApp(ex);
            }
        }

        internal static List<HistoryB> History_InventoryTab(string sort, string user, string part)
        {
            List<HistoryB> returnThese = [];
            try
            {
                var connectionString = GetConnectionString(null, null, null, null);
                using (var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    var searchWildPhase = "%transfer%";
                    var userWildPhrase = "%" + user + "%";
                    var partWildPhrase = "%" + part + "%";
                    using (var command = new MySqlCommand())
                    {
                        command.Connection = connection;
                        if (sort == "Time")
                        {
                            command.CommandText =
                                "SELECT * FROM `input_history` WHERE `User` LIKE @user AND `Type` NOT LIKE @search AND `Part ID` LIKE @part ORDER BY `" +
                                sort + "` DESC";
                        }
                        else
                        {
                            command.CommandText =
                                "SELECT * FROM `input_history` WHERE `User` LIKE @user AND `Type` NOT LIKE @search AND `Part ID` LIKE @part ORDER BY `" +
                                sort + "` ASC";
                        }

                        command.Parameters.AddWithValue("@search", searchWildPhase);
                        command.Parameters.AddWithValue("@user", userWildPhrase);
                        command.Parameters.AddWithValue("@part", partWildPhrase);

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                HistoryB a = new()
                                {
                                    Id = reader.GetInt32(0),
                                    User = reader.GetString(1),
                                    PartId = reader.GetString(2),
                                    Location = reader.GetString(3),
                                    Type = reader.GetString(4),
                                    Quantity = reader.GetInt32(5),
                                    Date = reader.GetDateTime(6)
                                };
                                returnThese.Add(a);
                            }
                        }
                    }
                }

                AppLogger.Log("History_InventoryTab executed successfully for user: " + user);
                return returnThese;
            }
            catch (MySqlException ex)
            {
                AppLogger.LogDatabaseError(ex);
                AppLogger.Log("Error in History_InventoryTab: " + ex.Message);
                HandleException_SQLError_CloseApp(ex);
                return [];
            }
            catch (Exception ex)
            {
                AppLogger.LogDatabaseError(ex);
                AppLogger.Log("Error in History_InventoryTab: " + ex.Message);
                HandleException_GeneralError_CloseApp(ex);
                return [];
            }
        }

        internal static List<History> History_RemovalTab(string sort, string user, string part)
        {
            List<History> returnThese = [];
            try
            {
                var connectionString = GetConnectionString(null, null, null, null);
                using (var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    var userWildPhrase = "%" + user + "%";
                    var partWildPhrase = "%" + part + "%";
                    using (var command = new MySqlCommand())
                    {
                        command.Connection = connection;
                        if (sort == "Time")
                        {
                            command.CommandText =
                                "SELECT * FROM `output_history` WHERE `User` LIKE @user AND `Part ID` LIKE @part ORDER BY `" +
                                sort + "` DESC";
                        }
                        else
                        {
                            command.CommandText =
                                "SELECT * FROM `output_history` WHERE `User` LIKE @user AND `Part ID` LIKE @part ORDER BY `" +
                                sort + "` ASC";
                        }

                        command.Parameters.AddWithValue("@user", userWildPhrase);
                        command.Parameters.AddWithValue("@part", partWildPhrase);

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                History a = new()
                                {
                                    Id = reader.GetInt32(0),
                                    User = reader.GetString(1),
                                    PartId = reader.GetString(2),
                                    Location = reader.GetString(3),
                                    Quantity = reader.GetInt32(4),
                                    Date = reader.GetDateTime(5)
                                };
                                returnThese.Add(a);
                            }
                        }
                    }
                }

                AppLogger.Log("History_RemovalTab executed successfully for user: " + user);
                return returnThese;
            }
            catch (MySqlException ex)
            {
                AppLogger.LogDatabaseError(ex);
                AppLogger.Log("Error in History_RemovalTab: " + ex.Message);
                HandleException_SQLError_CloseApp(ex);
                return [];
            }
            catch (Exception ex)
            {
                AppLogger.LogDatabaseError(ex);
                AppLogger.Log("Error in History_RemovalTab: " + ex.Message);
                HandleException_GeneralError_CloseApp(ex);
                return [];
            }
        }

        internal static List<HistoryB> History_TransferTab(string sort, string user, string part)
        {
            MySqlConnection connection = null;
            MySqlCommand command = null;
            MySqlDataReader reader = null;
            try
            {
                List<HistoryB> returnThese = [];
                connection = new MySqlConnection(GetConnectionString(null, null, null, null));
                connection.Open();

                var searchWildPhase = "%transfer%";
                var userWildPhrase = "%" + user + "%";
                var partWildPhrase = "%" + part + "%";

                command = new MySqlCommand
                {
                    Connection = connection
                };

                if (sort == "Time")
                {
                    command.CommandText =
                        "SELECT * FROM `input_history` WHERE `User` LIKE @user AND `Type` LIKE @search AND `Part ID` LIKE @part ORDER BY `" +
                        sort + "` DESC";
                }
                else
                {
                    command.CommandText =
                        "SELECT * FROM `input_history` WHERE `User` LIKE @user AND `Type` LIKE @search AND `Part ID` LIKE @part ORDER BY `" +
                        sort + "` ASC";
                }

                command.Parameters.AddWithValue("@search", searchWildPhase);
                command.Parameters.AddWithValue("@user", userWildPhrase);
                command.Parameters.AddWithValue("@part", partWildPhrase);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    HistoryB a = new()
                    {
                        Id = reader.GetInt32(0),
                        User = reader.GetString(1),
                        PartId = reader.GetString(2),
                        Location = reader.GetString(3),
                        Type = reader.GetString(4),
                        Quantity = reader.GetInt32(5),
                        Date = reader.GetDateTime(6)
                    };
                    returnThese.Add(a);
                }

                AppLogger.Log("History_TransferTab executed successfully for user: " + user);
                return returnThese;
            }
            catch (MySqlException ex)
            {
                AppLogger.LogDatabaseError(ex);
                AppLogger.Log("Error in History_TransferTab: " + ex.Message);
                HandleException_SQLError_CloseApp(ex);
                return [];
            }
            catch (Exception ex)
            {
                AppLogger.LogDatabaseError(ex);
                AppLogger.Log("Error in History_TransferTab: " + ex.Message);
                HandleException_GeneralError_CloseApp(ex);
                return [];
            }
            finally
            {
                reader?.Close();
                reader?.Dispose();
                command?.Dispose();
                connection?.Close();
                connection?.Dispose();
            }
        }
    }
}