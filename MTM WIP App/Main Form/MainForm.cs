using System.Data;
using MTM_WIP_App.Changelog;
using MTM_WIP_App.Database_Maint.Remove_Objects.Unified_Removal_Form;
using MTM_WIP_App.ErrorReporting;
using MTM_WIP_App.Main_Form.Tab_Methods;
using MTM_WIP_App.Settings;
using MySql.Data.MySqlClient;
using Unified_Entry_Form_UnifiedEntryForm = MTM_WIP_App.Database_Maint.Add_Objects.Unified_Entry_Form.UnifiedEntryForm;

namespace MTM_WIP_App.Main_Form
{
    using UnifiedEntryForm = Unified_Entry_Form_UnifiedEntryForm;

    public partial class MainForm : Form
    {
        private bool _isResettingTab3;

        // Add this field to your MainForm class
        private readonly Dictionary<Control, bool> _sqlControlEnabledStates = new();

        // Helper to get all SQL-interactive controls in a single list
        private IEnumerable<Control> GetSqlInteractiveControls()
        {
            yield return InventoryTab_ComboBox_Part;
            yield return InventoryTab_ComboBox_Op;
            yield return InventoryTab_ComboBox_Loc;
            yield return InventoryTab_TextBox_Qty;
            yield return InventoryTab_RichTextBox_Notes;
            yield return InventoryTab_Button_Save;
            yield return InventoryTab_Button_Reset;

            yield return RemoveTab_ComboBox_Part;
            yield return RemoveTab_ComboBox_Op;
            yield return RemoveTab_Button_Search;
            yield return RemoveTab_Button_Delete;
            yield return RemoveTab_Button_Print;
            yield return RemoveTab_Button_Reset;
            yield return RemoveTab_Button_Edit;

            yield return TransferTab_ComboBox_Part;
            yield return TransferTab_ComboBox_Loc;
            yield return TransferTab_Button_Search;
            yield return TransferTab_Button_Save;
            yield return TransferTab_Button_Reset;
            yield return TransferTab_TextBox_Qty;
            // Add any other SQL-interactive controls as needed
        }

        // Create Binding Sources
        private readonly BindingSource _inventoryBindingSource;

        private readonly BindingSource _transferBindingSource = new()
        {
            RaiseListChangedEvents = false,
            AllowNew = false
        };

        private readonly BindingSource _blankBindingSource = new()
        {
            RaiseListChangedEvents = false,
            AllowNew = false
        };

        // Create DataTables
        private readonly DataTable _inventoryTabPartCbDataTable = new();

        private readonly DataTable _inventoryTabOpCbDataTable = new();

        private readonly DataTable _inventoryTabLocationCbDataTable = new();

        private readonly DataTable _removeTabPartCbDataTable = new();

        private readonly DataTable _removeTabComboBoxOpDataTable = new();

        private readonly DataTable _removeTabComboBoxSearchByTypeDataTable = new();

        private readonly DataTable _transferTabLocationCbDataTable = new();

        private readonly DataTable _transferTabPartCbDataTable = new();

        // Create MySqlDataAdapters
        private readonly MySqlDataAdapter _inventoryTabPartCbDataAdapter = new();
        private readonly MySqlDataAdapter _inventoryTabOpCbDataAdapter = new();
        private readonly MySqlDataAdapter _inventoryTabLocationCbDataAdapter = new();
        private readonly MySqlDataAdapter _removeTabPartCbDataAdapter = new();
        private readonly MySqlDataAdapter _removeTabOpCbDataAdapter = new();
        private readonly MySqlDataAdapter _removeTabCBoxSearchByTypeDataAdapter = new();
        private readonly MySqlDataAdapter _transferTabLocationCbDataAdapter = new();
        private readonly MySqlDataAdapter _transferTabPartCbDataAdapter = new();

        /// <summary>
        /// Initializes the MainForm, sets up event handlers, and configures the initial state of the form.
        /// This includes setting the start position, focusing on the InventoryTab_ComboBox_Part, and setting data sources for DataGrids.
        /// </summary>
        internal MainForm()
        {
            _inventoryBindingSource = new BindingSource
            {
                Site = null,
                DataMember = null,
                DataSource = null,
                Position = 0,
                RaiseListChangedEvents = false,
                Sort = null,
                AllowNew = false,
                Filter = null
            };
            try
            {
                InitializeComponent();

                // Adjust font and layout for DPI
                FontScaler.AdjustFontAndLayout(this);

                IsMdiContainer = true;
                Primary_Controls_EventHandlers();

                //ApplicationWideMethods.AdjustFontBasedOnDpi(this);
                StartPosition = FormStartPosition.CenterScreen;
                MainForm_MenuStrip_File_Delete.Visible = false;
                InventoryTab_ComboBox_Part.SelectAll();
                InventoryTab_ComboBox_Part.Focus();
                ActiveControl = InventoryTab_ComboBox_Part;
                RemoveTab_DataGrid.DataSource = _blankBindingSource;
                TransferDataGrid.DataSource = _blankBindingSource;
            }
            catch (MySqlException ex)
            {
                AppLogger.LogDatabaseError(ex);
                ErrorLogDao.LogErrorWithMethod(ex);
                _ = ErrorLogDao.HandleException_SQLError_CloseApp(ex, true);
            }
            catch (Exception ex)
            {
                AppLogger.LogDatabaseError(ex);
                ErrorLogDao.LogErrorWithMethod(ex);
                _ = ErrorLogDao.HandleException_GeneralError_CloseApp(ex, true);
            }
        }


        /// <summary>
        /// Overrides the OnShown method to ensure the form is visible before running the admin check.
        /// </summary>
        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            try
            {
                // Run the admin check after the form is visible
                Startup_AdminCheck();
            }
            catch (MySqlException ex)
            {
                AppLogger.LogDatabaseError(ex);
                ErrorLogDao.LogErrorWithMethod(ex);
                _ = ErrorLogDao.HandleException_SQLError_CloseApp(ex, true);
            }
            catch (Exception ex)
            {
                AppLogger.LogDatabaseError(ex);
                ErrorLogDao.LogErrorWithMethod(ex);
                _ = ErrorLogDao.HandleException_GeneralError_CloseApp(ex, true);
            }
        }

        /// <summary>
        /// Checks if a child window was just closed and, if so, runs the ClearAndResetAllComboBoxes method.
        /// </summary>
        private void CheckChildWindowClosed()
        {
            try
            {
                foreach (var childForm in MdiChildren)
                {
                    if (childForm.IsDisposed)
                    {
                        ClearAndResetAllComboBoxesAsync();
                        break;
                    }
                }
            }
            catch (MySqlException ex)
            {
                AppLogger.LogDatabaseError(ex);
                ErrorLogDao.LogErrorWithMethod(ex);
                _ = ErrorLogDao.HandleException_SQLError_CloseApp(ex, true);
            }
            catch (Exception ex)
            {
                AppLogger.LogDatabaseError(ex);
                ErrorLogDao.LogErrorWithMethod(ex);
                _ = ErrorLogDao.HandleException_GeneralError_CloseApp(ex, true);
            }
        }

        /// <summary>
        /// Overrides the OnMdiChildActivate method to check if a child window was just closed.
        /// </summary>
        protected override void OnMdiChildActivate(EventArgs e)
        {
            base.OnMdiChildActivate(e);
            CheckChildWindowClosed();
        }

        /// <summary>
        /// Loads user settings on form load, including the last shown version of the changelog.
        /// If the last shown version is different from the current version, it updates the changelog visibility and last shown version for the user.
        /// </summary>
        /// <summary>
        /// Loads user settings on form load, including the last shown version of the changelog.
        /// If the last shown version is different from the current version, it updates the changelog visibility and last shown version for the user.
        /// </summary>
        public static async Task Primary_OnLoad_LoadUserSettingsAsync()
        {
            // Retrieve the last version of the changelog shown to the user from the database
            var lastShownVersion = await ChangeLogDao.Primary_ChangeLog_Get_LastShownAsync();

            // If the last shown version is different from the current app version,
            // update the changelog toggle to "false" (show changelog) and set the last shown version for the user
            if (lastShownVersion != WipAppVariables.Version)
            {
                await ChangeLogDao.Primary_ChangeLog_Set_SwitchAsync("false", WipAppVariables.User);
                if (WipAppVariables.Version != null)
                {
                    await ChangeLogDao.Primary_ChangeLog_Set_LastShownAsync(WipAppVariables.Version,
                        WipAppVariables.User);
                }
            }

            // Load user-specific UI settings from the database and assign them to the SQL.Default settings
            // These include theme name, theme text size, visual username, and visual password
            SQL.Default.Theme_Name = await ChangeLogDao.Primary_ChangeLog_Get_Theme_NameAsync() ?? string.Empty;
            SQL.Default.Theme_TextSize = await ChangeLogDao.Primary_ChangeLog_Get_Visual_Theme_FontSizeAsync() ?? 0;
            SQL.Default.VisualUserName = await ChangeLogDao.Primary_ChangeLog_Get_Visual_UserAsync() ?? string.Empty;
            SQL.Default.VisualPassword =
                await ChangeLogDao.Primary_ChangeLog_Get_Visual_PasswordAsync() ?? string.Empty;
        }

        /// <summary>
        /// Saves user settings when the form is closing.
        /// </summary>
        private static void Primary_OnClose_SaveUserSettings()
        {
            SQL.Default.Save();
        }

        /// <summary>
        /// Overrides the OnFormClosing method to save user settings before the form closes.
        /// </summary>
        /// <param name="e">The <see cref="FormClosingEventArgs"/> instance containing the event data.</param>
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            Primary_OnClose_SaveUserSettings();
        }

        /// <summary>
        /// Shows the changelog on startup if the changelog visibility toggle is set to false for the current user.
        /// </summary>
        internal void Primary_Startup_ShowChangeLog()
        {
            var show = SearchDao.Primary_ChangeLog_Get_Toggle();

            if (show == "false")
            {
                var change = new ChangeLogForm();
                Enabled = false;
                change.FormClosed +=
                    (_, _) => Enabled = true;
                change.Show();
            }
        }

        /// <summary>
        /// Updates the event handlers for various controls on the form. This includes setting up KeyUp and Leave event handlers for ComboBoxes, TextBoxes, and DataGrids.
        /// The event handlers are used to handle user interactions and validate inputs on the Inventory and Removal tabs.
        /// If the method cannot be run due to an inability to connect to the SQL server, it will call HandleException_SQLError_CloseApp.
        /// </summary>
        private void Primary_Controls_EventHandlers()
        {
            try
            {
                InventoryTab_ComboBox_Part.KeyUp += EventHandlers.Control_KeyUp;
                InventoryTab_ComboBox_Op.KeyUp += EventHandlers.Control_KeyUp;
                InventoryTab_TextBox_Qty.KeyUp += EventHandlers.Control_KeyUp;
                InventoryTab_ComboBox_Loc.KeyUp += EventHandlers.Control_KeyUp;
                InventoryTab_RichTextBox_Notes.KeyUp += EventHandlers.Control_KeyUp;
                InventoryTab_TextBox_HowMany.KeyUp += EventHandlers.Control_KeyUp;

                RemoveTab_ComboBox_Part.KeyUp += EventHandlers.Control_KeyUp;
                RemoveTab_ComboBox_Op.KeyUp += EventHandlers.Control_KeyUp;
                RemoveTab_DataGrid.KeyUp += EventHandlers.Control_KeyUp;

                TransferTab_ComboBox_Part.KeyUp += EventHandlers.Control_KeyUp;
                TransferTab_ComboBox_Loc.KeyUp += EventHandlers.Control_KeyUp;
                TransferDataGrid.KeyUp += EventHandlers.Control_KeyUp;

                InventoryTab_ComboBox_Part.Leave += (sender, _) =>
                {
                    if (sender != null)
                    {
                        EventHandlers.Validate_MethodCaller_InventoryTab(InventoryTab_ComboBox_Part,
                            InventoryTab_ComboBox_Op, InventoryTab_ComboBox_Loc, InventoryTab_TextBox_Qty,
                            InventoryTab_TextBox_HowMany,
                            InventoryTab_Button_Save, MainForm_MenuStrip_File_Save, _inventoryTabPartCbDataTable,
                            _inventoryTabOpCbDataTable, _inventoryTabLocationCbDataTable);
                    }
                };
                InventoryTab_ComboBox_Op.Leave += (sender, _) =>
                {
                    if (sender != null)
                    {
                        EventHandlers.Validate_MethodCaller_InventoryTab(InventoryTab_ComboBox_Part,
                            InventoryTab_ComboBox_Op, InventoryTab_ComboBox_Loc, InventoryTab_TextBox_Qty,
                            InventoryTab_TextBox_HowMany,
                            InventoryTab_Button_Save, MainForm_MenuStrip_File_Save, _inventoryTabPartCbDataTable,
                            _inventoryTabOpCbDataTable, _inventoryTabLocationCbDataTable);
                    }
                };
                InventoryTab_TextBox_Qty.Leave += (sender, _) =>
                {
                    if (sender != null)
                    {
                        EventHandlers.Validate_MethodCaller_InventoryTab(InventoryTab_ComboBox_Part,
                            InventoryTab_ComboBox_Op, InventoryTab_ComboBox_Loc, InventoryTab_TextBox_Qty,
                            InventoryTab_TextBox_HowMany,
                            InventoryTab_Button_Save, MainForm_MenuStrip_File_Save, _inventoryTabPartCbDataTable,
                            _inventoryTabOpCbDataTable, _inventoryTabLocationCbDataTable);
                    }
                };
                InventoryTab_ComboBox_Loc.Leave += (sender, _) =>
                {
                    if (sender != null)
                    {
                        EventHandlers.Validate_MethodCaller_InventoryTab(InventoryTab_ComboBox_Part,
                            InventoryTab_ComboBox_Op, InventoryTab_ComboBox_Loc, InventoryTab_TextBox_Qty,
                            InventoryTab_TextBox_HowMany,
                            InventoryTab_Button_Save, MainForm_MenuStrip_File_Save, _inventoryTabPartCbDataTable,
                            _inventoryTabOpCbDataTable, _inventoryTabLocationCbDataTable);
                    }
                };
                InventoryTab_RichTextBox_Notes.Leave += (sender, _) =>
                {
                    if (sender != null)
                    {
                        EventHandlers.Validate_MethodCaller_InventoryTab(InventoryTab_ComboBox_Part,
                            InventoryTab_ComboBox_Op, InventoryTab_ComboBox_Loc, InventoryTab_TextBox_Qty,
                            InventoryTab_TextBox_HowMany,
                            InventoryTab_Button_Save, MainForm_MenuStrip_File_Save, _inventoryTabPartCbDataTable,
                            _inventoryTabOpCbDataTable, _inventoryTabLocationCbDataTable);
                    }
                };
                InventoryTab_TextBox_HowMany.Leave += (sender, _) =>
                {
                    if (sender != null)
                    {
                        EventHandlers.Validate_MethodCaller_InventoryTab(InventoryTab_ComboBox_Part,
                            InventoryTab_ComboBox_Op, InventoryTab_ComboBox_Loc, InventoryTab_TextBox_Qty,
                            InventoryTab_TextBox_HowMany,
                            InventoryTab_Button_Save, MainForm_MenuStrip_File_Save, _inventoryTabPartCbDataTable,
                            _inventoryTabOpCbDataTable, _inventoryTabLocationCbDataTable);
                    }
                };

                RemoveTab_ComboBox_Part.Leave += (sender, _) =>
                {
                    if (sender != null)
                    {
                        EventHandlers.Validate_MethodCaller_RemovalTab(RemoveTab_ComboBox_Part,
                            RemoveTab_ComboBox_Op, RemoveTab_CBox_ShowAll, RemoveTab_Button_Search,
                            MainForm_MenuStrip_File_Save,
                            _inventoryTabPartCbDataTable,
                            _inventoryTabOpCbDataTable);
                    }
                };

                RemoveTab_ComboBox_Op.Leave += (sender, _) =>
                {
                    if (sender != null)
                    {
                        EventHandlers.Validate_MethodCaller_RemovalTab(RemoveTab_ComboBox_Part,
                            RemoveTab_ComboBox_Op, RemoveTab_CBox_ShowAll, RemoveTab_Button_Search,
                            MainForm_MenuStrip_File_Save,
                            _inventoryTabPartCbDataTable,
                            _inventoryTabOpCbDataTable);
                    }
                };

                TransferTab_ComboBox_Part.Leave += (sender, _) =>
                {
                    if (sender != null)
                    {
                        EventHandlers.Validate_MethodCaller_TransferTab(TransferTab_ComboBox_Part,
                            TransferTab_ComboBox_Loc, TransferTab_TextBox_Qty, TransferTab_Button_Search,
                            TransferTab_Button_Save,
                            MainForm_MenuStrip_File_Save,
                            _transferTabPartCbDataTable, _transferTabLocationCbDataTable, TransferDataGrid);
                    }
                };
                TransferTab_ComboBox_Loc.Leave += (sender, _) =>
                {
                    if (sender != null)
                    {
                        EventHandlers.Validate_MethodCaller_TransferTab(TransferTab_ComboBox_Part,
                            TransferTab_ComboBox_Loc, TransferTab_TextBox_Qty, TransferTab_Button_Search,
                            TransferTab_Button_Save,
                            MainForm_MenuStrip_File_Save,
                            _transferTabPartCbDataTable, _transferTabLocationCbDataTable, TransferDataGrid);
                    }
                };
                TransferTab_TextBox_Qty.Leave += (sender, _) =>
                {
                    if (sender != null)
                    {
                        EventHandlers.Validate_MethodCaller_TransferTab(TransferTab_ComboBox_Part,
                            TransferTab_ComboBox_Loc, TransferTab_TextBox_Qty, TransferTab_Button_Search,
                            TransferTab_Button_Save,
                            MainForm_MenuStrip_File_Save,
                            _transferTabPartCbDataTable, _transferTabLocationCbDataTable, TransferDataGrid);
                    }
                };
            }
            catch (MySqlException ex)
            {
                SearchDao.HandleException_SQLError_CloseApp(ex);
            }
            catch (Exception ex)
            {
                SearchDao.HandleException_GeneralError_CloseApp(ex);
            }
        }

        /// <summary>
        /// Overrides the OnPaint method to adjust the font size based on the DPI of the Graphics object and draw a "Hello, world!" message.
        /// </summary>
        protected override void OnPaint(PaintEventArgs e)
        {
            try
            {
                base.OnPaint(e);

                // Get the DPI of the Graphics object
                var dpiX = e.Graphics.DpiX;

                // Calculate the font size based on the DPI
                var fontSize = dpiX switch
                {
                    120 => 7f,
                    144 => 6f,
                    192 => 4.75f,
                    _ => Font.Size
                };

                // Create a new Font object with the adjusted size
                using var font = new Font(Font.FontFamily, fontSize);

                // Draw the text
                e.Graphics.DrawString("Hello, world!", font, Brushes.Black, new PointF(10, 10));
            }
            catch (MySqlException ex)
            {
                SearchDao.HandleException_SQLError_CloseApp(ex);
            }
            catch (Exception ex)
            {
                SearchDao.HandleException_GeneralError_CloseApp(ex);
            }
        }

        /// <summary>
        /// Loads the primary MainForm, initializes the form's state, and sets up the initial configuration.
        /// This includes filling the "Last 10 Transactions" list, resetting form controls, and setting up combo boxes and data grids.
        /// </summary>
        private async void Primary_MainForm_LoadAsync(object sender, EventArgs e)
        {
            SetSqlInteractiveControlsEnabled(false); // Disable SQL-interactive controls

            try
            {
                await Last10FillAsync();

                var isFormReset = WipAppVariables.mainFormFormReset == false;
                var areComboBoxesUnselected = TransferTab_ComboBox_Loc.SelectedIndex < 1 &&
                                              TransferTab_ComboBox_Part.SelectedIndex < 1 &&
                                              RemoveTab_ComboBox_Part.SelectedIndex < 1 &&
                                              RemoveTab_ComboBox_Op.SelectedIndex < 1 &&
                                              InventoryTab_ComboBox_Loc.SelectedIndex < 1 &&
                                              InventoryTab_ComboBox_Op.SelectedIndex < 1 &&
                                              InventoryTab_ComboBox_Part.SelectedIndex < 1;

                if (isFormReset && areComboBoxesUnselected)
                {
                    var version = WipAppVariables.Version;
                    InventoryTab_Label_Version.Text = @$"Version : {version}";
                    _inventoryBindingSource.DataSource = null;
                    RemoveTab_DataGrid.DataSource = _blankBindingSource;
                    TransferDataGrid.DataSource = _blankBindingSource;
                    ActiveControl = InventoryTab_ComboBox_Part;
                    await Startup_FillComboBoxesAsync();
                    Startup_SQLReset();
                    Helper_TabControl_ResetTab3();
                    Helper_TabControl_ResetTab2();
                    Helper_TabControl_ResetTab1();
                    InventoryTab_ComboBox_Part.SelectAll();
                    InventoryTab_ComboBox_Part.Focus();
                }
                else
                {
                    SettingsForm.settingsChanged = false;
                    await Startup_FillComboBoxesAsync();
                    Startup_SQLReset();
                    Helper_TabControl_ResetTab3();
                    Helper_TabControl_ResetTab2();
                    Helper_TabControl_ResetTab1();
                }
            }
            catch (MySqlException ex)
            {
                SearchDao.HandleException_SQLError_CloseApp(ex);
            }
            catch (Exception ex)
            {
                SearchDao.HandleException_GeneralError_CloseApp(ex);
            }
            finally
            {
                SetSqlInteractiveControlsEnabled(true); // Re-enable SQL-interactive controls
            }
        }

        private void SetSqlInteractiveControlsEnabled(bool enabled)
        {
            foreach (var control in GetSqlInteractiveControls())
            {
                if (enabled)
                {
                    // Restore original state if we have it
                    if (_sqlControlEnabledStates.TryGetValue(control, out var originalEnabled))
                    {
                        control.Enabled = originalEnabled;
                    }
                }
                else
                {
                    // Store original state if not already stored, then disable
                    if (!_sqlControlEnabledStates.ContainsKey(control))
                    {
                        _sqlControlEnabledStates[control] = control.Enabled;
                    }

                    control.Enabled = false;
                }
            }

            // When re-enabling, clear the state dictionary
            if (enabled)
            {
                _sqlControlEnabledStates.Clear();
            }
        }

        /// <summary>
        /// Resets various SQL tables in the database by updating their IDs and setting the AUTO_INCREMENT value.
        /// This method is typically used during the startup process to ensure that the database tables are in a consistent state.
        /// </summary>
        private static void Startup_SQLReset()
        {
            MySqlConnection connection = null;
            MySqlCommand command = null;
            MySqlDataReader reader = null;
            try
            {
                var connectionString = SqlVariables.GetConnectionString(null, null, null, null);
                connection = new MySqlConnection(connectionString);
                connection.Open();

                // Get all tables in the current database
                var getTablesQuery =
                    "SELECT TABLE_NAME FROM information_schema.TABLES WHERE TABLE_SCHEMA = DATABASE();";
                command = new MySqlCommand(getTablesQuery, connection);
                var tables = new List<string>();
                using (reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        tables.Add(reader.GetString(0));
                    }
                }

                command.Dispose();

                foreach (var table in tables)
                {
                    // 1. Check if the table has an 'ID' column and get its type and extra info
                    var checkIdColumnQuery = $@"
                SELECT COLUMN_NAME, COLUMN_TYPE, DATA_TYPE, EXTRA
                FROM information_schema.COLUMNS
                WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = '{table}' AND COLUMN_NAME = 'ID';";
                    command = new MySqlCommand(checkIdColumnQuery, connection);
                    string idExtra = null;
                    string idDataType = null;
                    string idColumnType = null;
                    using (reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            idExtra = reader.GetString("EXTRA");
                            idDataType = reader.GetString("DATA_TYPE");
                            idColumnType = reader.GetString("COLUMN_TYPE");
                        }
                    }

                    command.Dispose();

                    if (idExtra == null)
                    {
                        continue; // No ID column, skip
                    }

                    // 2. Check for composite primary key
                    var checkCompositeKeyQuery = $@"
                SELECT COUNT(*)
                FROM information_schema.KEY_COLUMN_USAGE
                WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = '{table}' AND CONSTRAINT_NAME = 'PRIMARY';";
                    command = new MySqlCommand(checkCompositeKeyQuery, connection);
                    var primaryKeyCount = Convert.ToInt32(command.ExecuteScalar());
                    command.Dispose();

                    if (primaryKeyCount > 1)
                    {
                        // Composite primary key, skip this table
                        continue;
                    }

                    // 3. Check if any other column is already AUTO_INCREMENT
                    var checkOtherAutoIncQuery = $@"
                SELECT COLUMN_NAME
                FROM information_schema.COLUMNS
                WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = '{table}' AND EXTRA LIKE '%auto_increment%' AND COLUMN_NAME != 'ID';";
                    command = new MySqlCommand(checkOtherAutoIncQuery, connection);
                    bool otherAutoIncExists;
                    using (reader = command.ExecuteReader())
                    {
                        otherAutoIncExists = reader.HasRows;
                    }

                    command.Dispose();

                    if (otherAutoIncExists)
                    {
                        // Skip this table, as another column is already AUTO_INCREMENT
                        continue;
                    }

                    // 4. Check if 'ID' is already the primary key
                    var checkPrimaryKeyQuery = $@"
                SELECT COUNT(*)
                FROM information_schema.KEY_COLUMN_USAGE
                WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = '{table}' AND COLUMN_NAME = 'ID' AND CONSTRAINT_NAME = 'PRIMARY';";
                    command = new MySqlCommand(checkPrimaryKeyQuery, connection);
                    var isPrimaryKey = Convert.ToInt32(command.ExecuteScalar()) > 0;
                    command.Dispose();

                    // 5. If not INT type, skip (MySQL only allows AUTO_INCREMENT on integer columns)
                    if (!idDataType.Equals("int", StringComparison.OrdinalIgnoreCase) &&
                        !idDataType.Equals("bigint", StringComparison.OrdinalIgnoreCase) &&
                        !idDataType.Equals("mediumint", StringComparison.OrdinalIgnoreCase) &&
                        !idDataType.Equals("smallint", StringComparison.OrdinalIgnoreCase) &&
                        !idDataType.Equals("tinyint", StringComparison.OrdinalIgnoreCase))
                    {
                        continue;
                    }

                    // 6. Drop existing primary key if not already on ID
                    if (!isPrimaryKey && primaryKeyCount == 1)
                    {
                        var dropPrimaryKeyQuery = $"ALTER TABLE `{table}` DROP PRIMARY KEY;";
                        try
                        {
                            command = new MySqlCommand(dropPrimaryKeyQuery, connection);
                            command.ExecuteNonQuery();
                        }
                        catch (MySqlException ex)
                        {
                            // Ignore if there was no primary key to drop
                            if (ex.Number != 1091) // ER_CANT_DROP_FIELD_OR_KEY
                            {
                                throw;
                            }
                        }
                        finally
                        {
                            command?.Dispose();
                        }
                    }

                    // 7. Set ID column to INT NOT NULL AUTO_INCREMENT if not already
                    if (!idExtra.Contains("auto_increment", StringComparison.OrdinalIgnoreCase))
                    {
                        var alterIdColumnQuery =
                            $"ALTER TABLE `{table}` MODIFY COLUMN `ID` {idColumnType} NOT NULL AUTO_INCREMENT;";
                        command = new MySqlCommand(alterIdColumnQuery, connection);
                        command.ExecuteNonQuery();
                        command.Dispose();
                    }

                    // 8. Add primary key on 'ID' if not already
                    if (!isPrimaryKey)
                    {
                        var addPrimaryKeyQuery = $"ALTER TABLE `{table}` ADD PRIMARY KEY (`ID`);";
                        command = new MySqlCommand(addPrimaryKeyQuery, connection);
                        command.ExecuteNonQuery();
                        command.Dispose();
                    }
                }
            }
            catch (MySqlException ex)
            {
                AppLogger.Log($"MySQL Exception: {ex.Message}/n{ex.StackTrace}");
                SearchDao.HandleException_SQLError_CloseApp(ex);
            }
            catch (Exception ex)
            {
                SearchDao.HandleException_GeneralError_CloseApp(ex);
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

        /// <summary>
        /// Fills the combo boxes on the MainForm with data from the database during startup.
        /// This includes part numbers, operation numbers, locations, and item types for various tabs.
        /// </summary>
        private async Task Startup_FillComboBoxesAsync()
        {
            MySqlConnection connection = null;
            try
            {
                var connectionString = SqlVariables.GetConnectionString(null, null, null, null);
                connection = new MySqlConnection(connectionString);
                await connection.OpenAsync();

                await Startup_FillComboBoxAsync("SELECT * FROM part_ids", connection, _inventoryTabPartCbDataAdapter,
                    _inventoryTabPartCbDataTable, InventoryTab_ComboBox_Part, "Item Number", "ID", "[ Enter Part ID ]");

                await Startup_FillComboBoxAsync("SELECT * FROM `operation_numbers`", connection,
                    _inventoryTabOpCbDataAdapter,
                    _inventoryTabOpCbDataTable, InventoryTab_ComboBox_Op, "Operation", "Operation", "[ Enter Op # ]");

                await Startup_FillComboBoxAsync("SELECT * FROM `locations`", connection,
                    _inventoryTabLocationCbDataAdapter,
                    _inventoryTabLocationCbDataTable, InventoryTab_ComboBox_Loc, "Location", "Location",
                    "[ Enter Location ]");

                await Startup_FillComboBoxAsync("SELECT * FROM part_ids", connection, _removeTabPartCbDataAdapter,
                    _removeTabPartCbDataTable, RemoveTab_ComboBox_Part, "Item Number", "Item Number",
                    "[ Enter Part ID ]");

                await Startup_FillComboBoxAsync("SELECT * FROM `operation_numbers`", connection,
                    _removeTabOpCbDataAdapter,
                    _removeTabComboBoxOpDataTable, RemoveTab_ComboBox_Op, "Operation", "Operation", "[ Enter Op # ]");

                await Startup_FillComboBoxAsync("SELECT * FROM `item_types`", connection,
                    _removeTabCBoxSearchByTypeDataAdapter,
                    _removeTabComboBoxSearchByTypeDataTable, RemoveTab_CBox_ShowAll, "Type", "Type", "[ Select Type ]");

                await Startup_FillComboBoxAsync("SELECT * FROM `locations`", connection,
                    _transferTabLocationCbDataAdapter,
                    _transferTabLocationCbDataTable, TransferTab_ComboBox_Loc, "Location", "Location",
                    "[ Enter New Location ]");

                await Startup_FillComboBoxAsync("SELECT * FROM part_ids", connection, _transferTabPartCbDataAdapter,
                    _transferTabPartCbDataTable, TransferTab_ComboBox_Part, "Item Number", "Item Number",
                    "[ Enter Part ID ]");
            }
            catch (MySqlException ex)
            {
                SearchDao.HandleException_SQLError_CloseApp(ex);
            }
            catch (Exception ex)
            {
                SearchDao.HandleException_GeneralError_CloseApp(ex);
            }
            finally
            {
                connection?.Close();
                connection?.Dispose();
            }
        }

        /// <summary>
        /// Fills a ComboBox with data from the database based on the provided query.
        /// This includes setting the data source, display member, and value member for the ComboBox.
        /// </summary>
        private static async Task Startup_FillComboBoxAsync(
            string query,
            MySqlConnection connection,
            MySqlDataAdapter dataAdapter,
            DataTable dataTable,
            ComboBox comboBox,
            string displayMember,
            string valueMember,
            string defaultText)
        {
            MySqlCommand command = null;
            try
            {
                command = new MySqlCommand(query, connection);
                dataAdapter.SelectCommand = command;
                await Task.Run(() =>
                    dataAdapter.Fill(dataTable)); // MySqlDataAdapter.FillAsync is not available, so use Task.Run
                var itemRow = dataTable.NewRow();
                itemRow[0] = defaultText;
                dataTable.Rows.InsertAt(itemRow, 0);

                if (comboBox.InvokeRequired)
                {
                    comboBox.Invoke(new Action(() =>
                    {
                        comboBox.DataSource = dataTable;
                        comboBox.DisplayMember = displayMember;
                        comboBox.ValueMember = valueMember;
                    }));
                }
                else
                {
                    comboBox.DataSource = dataTable;
                    comboBox.DisplayMember = displayMember;
                    comboBox.ValueMember = valueMember;
                }
            }
            catch (MySqlException ex)
            {
                SearchDao.HandleException_SQLError_CloseApp(ex);
            }
            catch (Exception ex)
            {
                SearchDao.HandleException_GeneralError_CloseApp(ex);
            }
            finally
            {
                command?.Dispose();
            }
        }


        /// <summary>
        /// Checks the user's access level and updates the UI controls accordingly.
        /// Disables certain controls if the user is not an admin or has read-only access.
        /// </summary>
        /// <summary>
        /// Checks the user's access level and updates the UI controls accordingly.
        /// Disables certain controls if the user is not an admin or has read-only access.
        /// </summary>
        private void Startup_AdminCheck()
        {
            try
            {
                // Safely check and update UI controls based on user type
                var isAdmin = WipAppVariables.userTypeAdmin;
                var isReadOnly = WipAppVariables.userTypeReadOnly;
                var user = WipAppVariables.User;

                CallUnifiedRemovalForm.Enabled = isAdmin;
                MainToolStrip_New_Object.Enabled = isAdmin;

                // Set user type variable
                var userType = isAdmin ? "Administrator" : "Normal User";
                if (isReadOnly)
                {
                    userType = "Read Only";
                }

                Text = $@"{Text} User: {user} | {userType}";

                if (isReadOnly)
                {
                    // Disable Inventory and Transfer tabs
                    MainForm_TabControl.SelectedIndex = 1;

                    var inventoryTab = MainForm_TabControl.TabPages["MainForm_TabControl_Inventory"];
                    if (inventoryTab != null)
                    {
                        MainForm_TabControl.TabPages.Remove(inventoryTab);
                    }

                    var transferTab = MainForm_TabControl.TabPages["MainForm_TabControl_Transfer"];
                    if (transferTab != null)
                    {
                        MainForm_TabControl.TabPages.Remove(transferTab);
                    }

                    Primary_Button_ShowHideLast10_Clicked(null, null);

                    // Hide the Save button in the Remove tab
                    RemoveTab_Button_Delete.Visible = false;

                    // Disable menu options
                    MainForm_MenuStrip_File.Enabled = false;
                    MainForm_MenuStrip_Edit.Enabled = false;
                    MainForm_MenuStrip_View.Enabled = false;

                    // Hide Delete button
                    RemoveTab_Button_Delete.Hide();
                }
                else
                {
                    // Enable menu options for non-read-only users
                    MainForm_MenuStrip_File.Enabled = true;
                    MainForm_MenuStrip_Edit.Enabled = true;
                    MainForm_MenuStrip_View.Enabled = true;

                    // Show Delete button
                    RemoveTab_Button_Delete.Show();

                    // Add Inventory and Transfer tabs back
                    var inventoryTab = MainForm_TabControl.TabPages["MainForm_TabControl_Inventory"];
                    if (inventoryTab == null)
                    {
                        var newInventoryTab = new TabPage("MainForm_TabControl_Inventory");
                        MainForm_TabControl.TabPages.Add(newInventoryTab);
                    }

                    var transferTab = MainForm_TabControl.TabPages["MainForm_TabControl_Transfer"];
                    if (transferTab == null)
                    {
                        var newTransferTab = new TabPage("MainForm_TabControl_Transfer");
                        MainForm_TabControl.TabPages.Add(newTransferTab);
                    }
                }
            }
            catch (MySqlException ex)
            {
                SearchDao.HandleException_SQLError_CloseApp(ex);
            }
            catch (Exception ex)
            {
                SearchDao.HandleException_GeneralError_CloseApp(ex);
            }
        }

        /// <summary>
        /// Clears all ComboBoxes and resets them to their default state by first clearing all data and then filling them with data from the database.
        /// Disposes of any data after making the sources null.
        /// </summary>
        private async Task ClearAndResetAllComboBoxesAsync()
        {
            try
            {
                if (InvokeRequired)
                {
                    await InvokeAsync(() => ClearAndResetAllComboBoxesAsync().Wait());
                    return;
                }

                InventoryTab_ComboBox_Part.DataSource = null;
                _inventoryTabPartCbDataTable.Clear();
                _inventoryTabPartCbDataTable.Dispose();

                InventoryTab_ComboBox_Op.DataSource = null;
                _inventoryTabOpCbDataTable.Clear();
                _inventoryTabOpCbDataTable.Dispose();

                InventoryTab_ComboBox_Loc.DataSource = null;
                _inventoryTabLocationCbDataTable.Clear();
                _inventoryTabLocationCbDataTable.Dispose();

                RemoveTab_ComboBox_Part.DataSource = null;
                _removeTabPartCbDataTable.Clear();
                _removeTabPartCbDataTable.Dispose();

                RemoveTab_ComboBox_Op.DataSource = null;
                _removeTabComboBoxOpDataTable.Clear();
                _removeTabComboBoxOpDataTable.Dispose();

                RemoveTab_CBox_ShowAll.DataSource = null;
                _removeTabComboBoxSearchByTypeDataTable.Clear();
                _removeTabComboBoxSearchByTypeDataTable.Dispose();

                TransferTab_ComboBox_Part.DataSource = null;
                _transferTabPartCbDataTable.Clear();
                _transferTabPartCbDataTable.Dispose();

                TransferTab_ComboBox_Loc.DataSource = null;
                _transferTabLocationCbDataTable.Clear();
                _transferTabLocationCbDataTable.Dispose();

                await Startup_FillComboBoxesAsync();

                Helper_TabControl_ResetTab1();
                Helper_TabControl_ResetTab2();
                Helper_TabControl_ResetTab3();
                if (MainForm_TabControl.SelectedIndex == 0)
                {
                    InventoryTab_ComboBox_Part.Focus();
                }

                if (MainForm_TabControl.SelectedIndex == 1)
                {
                    RemoveTab_ComboBox_Part.Focus();
                }

                if (MainForm_TabControl.SelectedIndex == 2)
                {
                    TransferTab_ComboBox_Part.Focus();
                }
            }
            catch (MySqlException ex)
            {
                SearchDao.HandleException_SQLError_CloseApp(ex);
            }
            catch (Exception ex)
            {
                SearchDao.HandleException_GeneralError_CloseApp(ex);
            }
        }

        // Helper for async invoke
        private Task InvokeAsync(Action action)
        {
            return Task.Factory.StartNew(() => Invoke(action), CancellationToken.None, TaskCreationOptions.None,
                TaskScheduler.Default);
        }


        /// <summary>
        /// Resets the controls on the Inventory tab to their default state.
        /// This includes resetting ComboBoxes, TextBoxes, and CheckBoxes, and disabling the Save button.
        /// </summary>
        internal void Helper_TabControl_ResetTab1()
        {
            try
            {
                Helper_Reset_ComboBox(InventoryTab_ComboBox_Loc, Color.Red, 0);
                Helper_Reset_ComboBox(InventoryTab_ComboBox_Op, Color.Red, 0);
                Helper_Reset_ComboBox(InventoryTab_ComboBox_Part, Color.Red, 0);

                InventoryTab_CheckBox_Multi.Checked = false;
                InventoryTab_CheckBox_Multi_Different.Checked = false;
                InventoryTab_TextBox_HowMany.Clear();
                InventoryTab_TextBox_HowMany.Enabled = false;
                InventoryTab_CheckBox_Multi_Different.Enabled = false;

                Helper_Reset_TextBox(InventoryTab_TextBox_Qty, Color.Red, "[ Enter Valid Quantity ]");
                InventoryTab_RichTextBox_Notes.Text = string.Empty;

                Helper_SetActiveControl(InventoryTab_ComboBox_Part);
                InventoryTab_Button_Save.Enabled = false;
                MainForm_MenuStrip_File_Save.Enabled = false;
            }
            catch (MySqlException ex)
            {
                SearchDao.HandleException_SQLError_CloseApp(ex);
            }
            catch (Exception ex)
            {
                SearchDao.HandleException_GeneralError_CloseApp(ex);
            }
        }

        /// <summary>
        /// Resets the controls on the Remove tab to their default state.
        /// This includes resetting ComboBoxes, disabling buttons, and updating the DataGrid.
        /// </summary>
        internal void Helper_TabControl_ResetTab2()
        {
            try
            {
                RemoveTab_DataGrid.DataSource = _blankBindingSource;
                RemoveTab_DataGrid.Update();

                Helper_Reset_ComboBox(RemoveTab_ComboBox_Part, Color.Red, 0);
                Helper_Reset_ComboBox(RemoveTab_ComboBox_Op, Color.Red, 0);
                Helper_Reset_ComboBox(RemoveTab_CBox_ShowAll, Color.Red, 0);

                RemoveTab_Button_Search.Enabled = false;
                RemoveTab_Button_Delete.Enabled = false;
                RemoveTab_Button_Print.Enabled = false;
                RemoveTab_Button_Edit.Visible = false;
                RemoveTab_Image_NothingFound.Visible = false;

                MainForm_MenuStrip_File_Print.Enabled = false;
                MainForm_MenuStrip_File_Save.Enabled = false;

                Helper_EnableComboBoxes(RemoveTab_ComboBox_Part, RemoveTab_ComboBox_Op, RemoveTab_CBox_ShowAll);

                Helper_SetActiveControl(RemoveTab_ComboBox_Part);
            }
            catch (MySqlException ex)
            {
                SearchDao.HandleException_SQLError_CloseApp(ex);
            }
            catch (Exception ex)
            {
                SearchDao.HandleException_GeneralError_CloseApp(ex);
            }
        }

        /// <summary>
        /// Resets the controls on the Transfer tab to their default state.
        /// This includes resetting ComboBoxes, disabling buttons, clearing the DataGrid, and updating the UI.
        /// </summary>
        internal void Helper_TabControl_ResetTab3()
        {
            try
            {
                _isResettingTab3 = true; // Set the flag to true

                Helper_Reset_ComboBox(TransferTab_ComboBox_Part, Color.Red, 0);
                Helper_Reset_ComboBox(TransferTab_ComboBox_Loc, Color.Red, 0);

                TransferDataGrid.DataSource = _blankBindingSource;
                TransferDataGrid.Update();

                TransferTab_ComboBox_Part.Enabled = true;
                TransferTab_Button_Search.Enabled = false;
                TransferTab_Button_Save.Enabled = false;
                MainForm_MenuStrip_File_Save.Enabled = false;

                TransferTab_TextBox_Qty.Clear();
                TransferTab_TextBox_Qty.Enabled = false;

                TransferTab_Image_Nothing.Visible = false;
            }
            catch (MySqlException ex)
            {
                SearchDao.HandleException_SQLError_CloseApp(ex);
            }
            catch (Exception ex)
            {
                SearchDao.HandleException_GeneralError_CloseApp(ex);
            }
            finally
            {
                _isResettingTab3 = false; // Set the flag back to false
            }
        }

        /// <summary>
        /// Resets the specified ComboBox to its default state by setting its foreground color and selected index.
        /// </summary>
        /// <param name="comboBox">The ComboBox to reset.</param>
        /// <param name="color">The color to set for the ComboBox's foreground.</param>
        /// <param name="selectedIndex">The index to select in the ComboBox.</param>
        private static void Helper_Reset_ComboBox(ComboBox comboBox, Color color, int selectedIndex)
        {
            try
            {
                comboBox.ForeColor = color;
                comboBox.SelectedIndex = selectedIndex;
            }
            catch (MySqlException ex)
            {
                SearchDao.HandleException_SQLError_CloseApp(ex);
            }
            catch (Exception ex)
            {
                SearchDao.HandleException_GeneralError_CloseApp(ex);
            }
        }

        /// <summary>
        /// Resets the specified TextBox to its default state by setting its foreground color and text.
        /// </summary>
        /// <param name="textBox">The TextBox to reset.</param>
        /// <param name="color">The color to set for the TextBox's foreground.</param>
        /// <param name="text">The text to set for the TextBox.</param>
        private static void Helper_Reset_TextBox(TextBox textBox, Color color, string text)
        {
            try
            {
                textBox.ForeColor = color;
                textBox.Text = text;
            }
            catch (MySqlException ex)
            {
                SearchDao.HandleException_SQLError_CloseApp(ex);
            }
            catch (Exception ex)
            {
                SearchDao.HandleException_GeneralError_CloseApp(ex);
            }
        }

        /// <summary>
        /// Sets the specified control as the active control and focuses on it.
        /// If the control is a TextBoxBase, it selects all text within the control.
        /// </summary>
        /// <param name="control">The control to set as active and focus on.</param>
        private void Helper_SetActiveControl(Control control)
        {
            try
            {
                ActiveControl = control;
                if (control is TextBoxBase textBoxBase)
                {
                    textBoxBase.SelectAll();
                }

                control.Focus();
            }
            catch (MySqlException ex)
            {
                SearchDao.HandleException_SQLError_CloseApp(ex);
            }
            catch (Exception ex)
            {
                SearchDao.HandleException_GeneralError_CloseApp(ex);
            }
        }

        /// <summary>
        /// Enables the specified ComboBoxes by setting their Enabled property to true.
        /// </summary>
        /// <param name="comboBoxes">The ComboBoxes to enable.</param>
        private static void Helper_EnableComboBoxes(params ComboBox[] comboBoxes)
        {
            try
            {
                foreach (var comboBox in comboBoxes)
                {
                    comboBox.Enabled = true;
                }
            }
            catch (MySqlException ex)
            {
                SearchDao.HandleException_SQLError_CloseApp(ex);
            }
            catch (Exception ex)
            {
                SearchDao.HandleException_GeneralError_CloseApp(ex);
            }
        }

        /// <summary>
        /// Regains focus on the MainForm and updates the UI based on settings changes and user type.
        /// This includes resetting tabs, updating the status strip, and enabling/disabling controls based on user permissions.
        /// </summary>
        private void Helper_MainForm_RegainFocus(object sender, EventArgs e)
        {
            try
            {
                if (SettingsForm.settingsChanged || MultipleLocations.multipleLocationsDisabled)
                {
                    var usertype = WipAppVariables.userTypeAdmin ? "Administrator" : "Normal User";
                    if (WipAppVariables.userTypeReadOnly)
                    {
                        usertype = "Read Only";
                    }

                    Text =
                        @$"Manitowoc Tool and Manufacturing | WIP Inventory System | User: {WipAppVariables.User} | {usertype}";
                    Helper_TabControl_ResetTab1();
                    Helper_TabControl_ResetTab2();
                    Helper_TabControl_ResetTab3();
                    InventoryTab_ComboBox_Part.Focus();
                    SettingsForm.settingsChanged = false;
                    Enabled = true;
                    Cursor = Cursors.Default;
                    MultipleLocations.multipleLocationsDisabled = false;

                    if (MultipleLocations.multipleLocationsDisabled)
                    {
                        MainForm_StatusStrip_SavedStatus.Text =
                            @$"{MultipleLocations.multipleLocationsCount} Transactions of Part ID {WipAppVariables.partId} made to multiple locations.";
                        MultipleLocations.multipleLocationsCount = 0;
                    }

                    MultipleLocations.multipleLocationsCount = 0;
                }

                if (WipAppVariables.userTypeReadOnly)
                {
                    TransferTab_Button_Save.Hide();
                    TransferTab_ComboBox_Part.Enabled = false;
                    InventoryTab_ComboBox_Part.Enabled = false;
                    InventoryTab_ComboBox_Op.Enabled = false;
                    InventoryTab_TextBox_Qty.Enabled = false;
                    InventoryTab_RichTextBox_Notes.Enabled = false;
                    InventoryTab_ComboBox_Loc.Enabled = false;
                    InventoryTab_CheckBox_Multi.Enabled = false;
                    InventoryTab_TextBox_HowMany.Enabled = false;
                    InventoryTab_Button_Save.Enabled = false;
                    InventoryTab_Button_Reset.Enabled = false;
                    MainForm_MenuStrip_File.Enabled = false;
                    MainForm_MenuStrip_Edit.Enabled = false;
                    MainForm_MenuStrip_View.Enabled = false;
                    RemoveTab_Button_Delete.Hide();
                    MainForm_TabControl_Remove.Focus();
                }
            }
            catch (MySqlException ex)
            {
                SearchDao.HandleException_SQLError_CloseApp(ex);
            }
            catch (Exception ex)
            {
                SearchDao.HandleException_GeneralError_CloseApp(ex);
            }
        }

        /// <summary>
        /// Checks the selected part in the Transfer tab ComboBox and updates the UI accordingly.
        /// This includes enabling/disabling the Save button and setting the ComboBox's foreground color.
        /// </summary>
        private void Helper_TransferTab_ButtonEnabler()
        {
            try
            {
                if (TransferTab_ComboBox_Part.SelectedIndex <= 0)
                {
                    var current = 0;
                    foreach (DataRow row in _transferTabPartCbDataTable.Rows)
                    {
                        current++;
                        var partId = row.Field<string>("Item Number");
                        if (partId == TransferTab_ComboBox_Part.Text && partId != "[ Enter Part ID ]")
                        {
                            TransferTab_ComboBox_Part.SelectedIndex =
                                _transferTabPartCbDataTable.Rows[current].Field<int>("ID") - 1;
                            TransferTab_ComboBox_Part.ForeColor = Color.Black;
                            if (TransferTab_ComboBox_Part.SelectedIndex != 0 &&
                                TransferTab_ComboBox_Loc.SelectedIndex != 0)
                            {
                                TransferTab_Button_Save.Enabled = true;
                                MainForm_MenuStrip_File_Save.Enabled = true;
                                TransferTab_Button_Search.Enabled = false;
                            }
                            else
                            {
                                TransferTab_Button_Save.Enabled = false;
                                MainForm_MenuStrip_File_Save.Enabled = false;
                                TransferTab_Button_Search.Enabled = true;
                            }

                            break;
                        }

                        if (current >= _transferTabPartCbDataTable.Rows.Count)
                        {
                            TransferTab_ComboBox_Part.SelectedIndex = 0;
                            TransferTab_Button_Save.Enabled = false;
                            MainForm_MenuStrip_File_Save.Enabled = false;
                            TransferTab_ComboBox_Part.ForeColor = Color.Red;
                        }
                    }
                }
                else
                {
                    TransferTab_ComboBox_Part.ForeColor = Color.Black;
                    if (TransferDataGrid.Rows.Count > 0)
                    {
                        if (TransferTab_ComboBox_Part.SelectedIndex > 0 && TransferTab_ComboBox_Loc.SelectedIndex > 0)
                        {
                            TransferTab_Button_Save.Enabled = true;
                            MainForm_MenuStrip_File_Save.Enabled = true;
                        }
                        else
                        {
                            TransferTab_Button_Save.Enabled = false;
                            MainForm_MenuStrip_File_Save.Enabled = false;
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                SearchDao.HandleException_SQLError_CloseApp(ex);
            }
            catch (Exception ex)
            {
                SearchDao.HandleException_GeneralError_CloseApp(ex);
            }
        }

        /// <summary>
        /// Handles the CheckedChanged event for the InventoryTab_CheckBox_Multi_Different control.
        /// This method enables or disables the InventoryTab_TextBox_HowMany and InventoryTab_ComboBox_Loc controls based on the state of the checkbox.
        /// It also validates the input fields and enables or disables the save button accordingly.
        /// </summary>
        private void InventoryTab_CheckBox_Multi_Different_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (InventoryTab_CheckBox_Multi_Different.Checked)
                {
                    InventoryTab_TextBox_HowMany.Enabled = false;
                    InventoryTab_ComboBox_Loc.SelectedIndex = InventoryTab_ComboBox_Loc.FindStringExact("N/A");
                    InventoryTab_ComboBox_Loc.ForeColor = Color.Black;
                    InventoryTab_ComboBox_Loc.Enabled = false;
                }
                else
                {
                    InventoryTab_TextBox_HowMany.Enabled = true;
                    InventoryTab_ComboBox_Loc.SelectedIndex = 0;
                    InventoryTab_ComboBox_Loc.Enabled = true;
                    InventoryTab_ComboBox_Loc.ForeColor = Color.Red;
                }

                if (InventoryTab_ComboBox_Part.SelectedIndex != 0 &&
                    InventoryTab_ComboBox_Op.SelectedIndex != 0 &&
                    InventoryTab_ComboBox_Loc.SelectedIndex != 0 &&
                    InventoryTab_TextBox_Qty.Text.Length != 0 &&
                    int.TryParse(InventoryTab_TextBox_Qty.Text, out _))
                {
                    InventoryTab_Button_Save.Enabled = true;
                    MainForm_MenuStrip_File_Save.Enabled = true;
                }
                else
                {
                    InventoryTab_Button_Save.Enabled = false;
                    MainForm_MenuStrip_File_Save.Enabled = false;
                }
            }
            catch (MySqlException ex)
            {
                SearchDao.HandleException_SQLError_CloseApp(ex);
            }
            catch (Exception ex)
            {
                SearchDao.HandleException_GeneralError_CloseApp(ex);
            }
        }

        /// <summary>
        /// Handles the click event for the InventoryTab_Reset button.
        /// This method resets the controls on the Inventory tab to their default state by calling the Helper_TabControl_ResetTab1 method.
        /// </summary>
        private async void InventoryTab_Button_Reset_Clicked(object sender, EventArgs e)
        {
            var button = InventoryTab_Button_Reset;
            if (!button.Enabled)
            {
                return;
            }

            button.Enabled = false;
            SetSqlInteractiveControlsEnabled(false);
            try
            {
                await ClearAndResetAllComboBoxesAsync();
            }
            catch (MySqlException ex)
            {
                SearchDao.HandleException_SQLError_CloseApp(ex);
            }
            catch (Exception ex)
            {
                SearchDao.HandleException_GeneralError_CloseApp(ex);
            }
            finally
            {
                SetSqlInteractiveControlsEnabled(true);
                button.Enabled = true;
            }
        }

        /// <summary>
        /// Handles the click event for the InventoryTab_Print button.
        /// This method prints the data displayed in the RemoveTab_DataGrid using the DGVPrinter library.
        /// It temporarily changes the theme to "Default (Black and White)" for printing and then restores the original theme.
        /// </summary>
        private void InventoryTab_Button_Print_Clicked(object sender, EventArgs e)
        {
            try
            {
                var temptheme = SQL.Default.Theme_Name;
                SQL.Default.Theme_Name = "Default (Black and White)";
                _ = new DgvDesigner();
                DgvDesigner.InitializeDataGridView(RemoveTab_DataGrid, null);
                DgvPrinter printer = new()
                {
                    Title = WipAppVariables.PrintTitle,
                    SubTitle = "Work In Process Locations for Part Number: " + WipAppVariables.PrintTitle,
                    SubTitleFormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.NoClip,
                    HideColumns = { "ID", "Notes", "Date", "Type", "User" },
                    PageNumbers = true,
                    PageNumberInHeader = false,
                    PorportionalColumns = true,
                    HeaderCellAlignment = StringAlignment.Near,
                    Footer = "Manitowoc Tool and Manufacturing",
                    FooterSpacing = 15
                };
                printer.PrintDataGridView(RemoveTab_DataGrid);
                SQL.Default.Theme_Name = temptheme;
                DgvDesigner.InitializeDataGridView(RemoveTab_DataGrid, null);
            }
            catch (MySqlException ex)
            {
                SearchDao.HandleException_SQLError_CloseApp(ex);
            }
            catch (Exception ex)
            {
                SearchDao.HandleException_GeneralError_CloseApp(ex);
            }
        }

        /// <summary>
        /// Handles the click event for the InventoryTab_Save button.
        /// This method validates the selected operation number and prompts the user with a warning if the operation number is "900" (Outside Service).
        /// If the user confirms, it sets the notes to "Outside Service" and proceeds with saving the data.
        /// Otherwise, it resets the operation ComboBox and focuses on it.
        /// If the "Multi" and "Multi Different" checkboxes are checked, it sets the necessary variables and opens the MultipleLocations form.
        /// If the "Multi" checkbox is checked and the "How Many" textbox has a value, it performs multiple save operations.
        /// Otherwise, it performs a single save operation.
        /// </summary>
        private void InventoryTab_Button_Save_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (InventoryTab_ComboBox_Op.Text == @"900")
                {
                    var message =
                        "WARNING!\nThe Operation Number you have chosen is for Outside Service Only!\nAre you sure this is correct?";
                    var caption = "OUTSIDE SERVICE WARNING";
                    var buttons = MessageBoxButtons.YesNo;
                    var result = MessageBox.Show(this, message, caption, buttons, MessageBoxIcon.Warning,
                        MessageBoxDefaultButton.Button1);

                    if (result == DialogResult.Yes)
                    {
                        InventoryTab_RichTextBox_Notes.Text = @"Outside Service";
                    }
                    else
                    {
                        InventoryTab_ComboBox_Op.SelectedIndex = 0;
                        InventoryTab_ComboBox_Op.ForeColor = Color.Red;
                        InventoryTab_ComboBox_Op.SelectAll();
                        InventoryTab_ComboBox_Op.Focus();
                        ActiveControl = InventoryTab_ComboBox_Op;
                        return;
                    }
                }

                if (InventoryTab_CheckBox_Multi.Checked && InventoryTab_CheckBox_Multi_Different.Checked)
                {
                    WipAppVariables.PartType = "From Press";
                    WipAppVariables.partId = InventoryTab_ComboBox_Part.Text;
                    WipAppVariables.Operation = InventoryTab_ComboBox_Op.Text;
                    WipAppVariables.InventoryQuantity = Convert.ToInt32(InventoryTab_TextBox_Qty.Text);
                    WipAppVariables.Notes = InventoryTab_RichTextBox_Notes.Text;

                    Primary_DuplicationCheck_Last10();

                    var change = new MultipleLocations();
                    Enabled = false;
                    change.FormClosed +=
                        (s, _) => Enabled = true;
                    change.FormClosed += (_, _) => ClearAndResetAllComboBoxesAsync();
                    change.Show();
                }
                else
                {
                    WipAppVariables.PartType = "From Press";
                    WipAppVariables.partId = InventoryTab_ComboBox_Part.Text;
                    WipAppVariables.Operation = InventoryTab_ComboBox_Op.Text;
                    WipAppVariables.Location = InventoryTab_ComboBox_Loc.Text;
                    WipAppVariables.InventoryQuantity = Convert.ToInt32(InventoryTab_TextBox_Qty.Text);
                    WipAppVariables.Notes = InventoryTab_RichTextBox_Notes.Text;
                    MainForm_StatusStrip_SavedStatus.Text =
                        @$"Part: {WipAppVariables.partId} | Op: {WipAppVariables.Operation} | Qty: {WipAppVariables.InventoryQuantity} | Saved to: {WipAppVariables.Location} | At: {DateTime.Now:t}";
                    Primary_DuplicationCheck_Last10();
                    if (InventoryTab_CheckBox_Multi.Checked && InventoryTab_TextBox_HowMany.Text.Length > 0)
                    {
                        MainForm_StatusStrip_SavedStatus.Text =
                            @$"Part: {WipAppVariables.partId} | Op: {WipAppVariables.Operation} | Qty: {WipAppVariables.InventoryQuantity} | Saved to: {WipAppVariables.Location} | At: {DateTime.Now:t} | {Convert.ToInt32(InventoryTab_TextBox_HowMany.Text)} Transactions made.";

                        for (var i = 1; i <= Convert.ToInt32(InventoryTab_TextBox_HowMany.Text); i++)
                        {
                            SearchDao.InventoryTab_Save();
                        }
                    }
                    else
                    {
                        SearchDao.InventoryTab_Save();
                    }

                    Helper_TabControl_ResetTab1();
                }
            }
            catch (MySqlException ex)
            {
                SearchDao.HandleException_SQLError_CloseApp(ex);
            }
            catch (Exception ex)
            {
                SearchDao.HandleException_GeneralError_CloseApp(ex);
            }
        }

        /// <summary>
        /// Handles the click event for the RemoveTab_Print button.
        /// This method prints the data displayed in the RemoveTab_DataGrid using the DGVPrinter library.
        /// It temporarily changes the theme to "Default (Black and White)" for printing and then restores the original theme.
        /// </summary>
        private void RemoveTab_Button_Print_Clicked(object sender, EventArgs e)
        {
            try
            {
                var temptheme = SQL.Default.Theme_Name;
                SQL.Default.Theme_Name = "Default (Black and White)";
                _ = new DgvDesigner();
                DgvDesigner.InitializeDataGridView(RemoveTab_DataGrid, null);
                DgvPrinter printer = new()
                {
                    Title = WipAppVariables.PrintTitle,
                    SubTitle = "Work In Process Locations for Part Number: " + WipAppVariables.PrintTitle,
                    SubTitleFormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.NoClip,
                    HideColumns = { "ID", "Notes", "Date", "Type", "User" },
                    PageNumbers = true,
                    PageNumberInHeader = false,
                    PorportionalColumns = true,
                    HeaderCellAlignment = StringAlignment.Near,
                    Footer = "Manitowoc Tool and Manufacturing",
                    FooterSpacing = 15
                };

                // Show the print dialog and handle the result
                var result = printer.DisplayPrintDialog();
                if (result == DialogResult.OK)
                {
                    printer.PrintDataGridView(RemoveTab_DataGrid);
                }

                SQL.Default.Theme_Name = temptheme;
                DgvDesigner.InitializeDataGridView(RemoveTab_DataGrid, null);
            }
            catch (MySqlException ex)
            {
                SearchDao.HandleException_SQLError_CloseApp(ex);
            }
            catch (Exception ex)
            {
                SearchDao.HandleException_GeneralError_CloseApp(ex);
            }
            finally
            {
                // Ensure the theme is reset even if an exception occurs
                SQL.Default.Theme_Name = SQL.Default.Theme_Name ?? "Default";
            }
        }


        /// <summary>
        /// Handles the click event for the RemoveTab_Reset button.
        /// This method resets the controls on the Remove tab to their default state by calling the Helper_TabControl_ResetTab2 method.
        /// </summary>
        private async void RemoveTab_Button_Reset_Clicked(object sender, EventArgs e)
        {
            var button = RemoveTab_Button_Reset;
            if (!button.Enabled)
            {
                return;
            }

            button.Enabled = false;
            SetSqlInteractiveControlsEnabled(false);
            try
            {
                await ClearAndResetAllComboBoxesAsync();
                RemoveTab_Image_NothingFound.Visible = false;
            }
            catch (MySqlException ex)
            {
                SearchDao.HandleException_SQLError_CloseApp(ex);
            }
            catch (Exception ex)
            {
                SearchDao.HandleException_GeneralError_CloseApp(ex);
            }
            finally
            {
                SetSqlInteractiveControlsEnabled(true);
                button.Enabled = true;
            }
        }

        /// <summary>
        /// Handles the click event for the RemoveTab_Search button.
        /// This method searches for records in the database based on the selected part and operation number,
        /// and updates the RemoveTab_DataGrid with the search results.
        /// </summary>
        private async void RemoveTab_Button_Search_Clicked(object sender, EventArgs e)
        {
            try
            {
                RemoveTab_ComboBox_Part.Enabled = false;
                RemoveTab_ComboBox_Op.Enabled = false;
                WipAppVariables.PrintOp = RemoveTab_ComboBox_Op.Text;
                var data = await SearchDao.RemoveTab_SearchAsync(
                    RemoveTab_ComboBox_Part.Text,
                    RemoveTab_ComboBox_Op.Text,
                    RemoveTab_ComboBox_Op.SelectedIndex.ToString());
                _inventoryBindingSource.DataSource = data;
                RemoveTab_DataGrid.DataSource = _inventoryBindingSource;
                for (var i = 0; i < RemoveTab_DataGrid.Columns.Count; i++)
                {
                    RemoveTab_DataGrid.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                }

                if (RemoveTab_DataGrid.Columns.Contains("ID"))
                {
                    RemoveTab_DataGrid.Columns["ID"].Visible = false;
                }

                DgvDesigner.InitializeDataGridView(RemoveTab_DataGrid, null);
                RemoveTab_Image_NothingFound.Visible = RemoveTab_DataGrid.RowCount == 0;
            }
            catch (MySqlException ex)
            {
                SearchDao.HandleException_SQLError_CloseApp(ex);
            }
            catch (Exception ex)
            {
                SearchDao.HandleException_GeneralError_CloseApp(ex);
            }
            finally
            {
                RemoveTab_ComboBox_Part.Enabled = true;
                RemoveTab_ComboBox_Op.Enabled = true;
            }
        }


        /// <summary>
        /// Handles the click event for the RemoveTab_Delete button.
        /// This method deletes the selected rows from the RemoveTab_DataGrid and updates the database accordingly.
        /// It prompts the user for confirmation before performing the deletion.
        /// </summary>
        private void RemoveTab_Button_Delete_Clicked(object sender, EventArgs e)
        {
            System.Text.StringBuilder sb = new();

            if (RemoveTab_DataGrid.SelectedRows.Count <= 0)
            {
                return;
            }

            var selectedRowCount = RemoveTab_DataGrid.Rows.GetRowCount(DataGridViewElementStates.Selected);

            if (selectedRowCount <= 0)
            {
                return;
            }

            var moreThan1Trigger = selectedRowCount > 1;

            foreach (DataGridViewRow row in RemoveTab_DataGrid.SelectedRows)
            {
                var sri = row.Index;
                sb.Append("PartID: " + RemoveTab_DataGrid.Rows[sri].Cells["PartID"].Value +
                          " | Location : " + RemoveTab_DataGrid.Rows[sri].Cells["Location"].Value);
                sb.Append(Environment.NewLine);
            }

            var message = selectedRowCount > 5
                ? "Multiple PartIDs (more than 5) selected for removal.\nClick YES to remove them."
                : sb.ToString();

            const string caption = "Inventory Removal Alert";
            const MessageBoxButtons buttons = MessageBoxButtons.YesNo;

            var result = MessageBox.Show(this, message, caption, buttons, MessageBoxIcon.Warning,
                MessageBoxDefaultButton.Button1);

            if (result == DialogResult.Yes)
            {
                foreach (DataGridViewRow row in RemoveTab_DataGrid.SelectedRows)
                {
                    try
                    {
                        WipAppVariables.removeId = row.Cells[0].Value.ToString();
                        WipAppVariables.RemoveLocation = row.Cells[1].Value.ToString();
                        WipAppVariables.RemovePartNumber = row.Cells[2].Value.ToString();
                        WipAppVariables.RemoveOperation = row.Cells[3].Value.ToString();
                        WipAppVariables.RemoveQuantity = Convert.ToInt32(row.Cells[5].Value);
                        WipAppVariables.RemoveUser = WipAppVariables.User;
                        SearchDao.RemoveTab_Delete();
                        if (moreThan1Trigger)
                        {
                            MainForm_StatusStrip_SavedStatus.Text =
                                @$"Part: {WipAppVariables.RemovePartNumber} | Deleted From: MULTIPLE LOCATIONS | Transactions Made: {selectedRowCount} | At: {DateTime.Now:t}";
                        }
                        else
                        {
                            MainForm_StatusStrip_SavedStatus.Text =
                                @$"Part: {WipAppVariables.RemovePartNumber} | Op: {WipAppVariables.RemoveOperation} | Qty: {WipAppVariables.RemoveQuantity} | Deleted From: {WipAppVariables.RemoveLocation} | At: {DateTime.Now:t}";
                        }

                        RemoveTab_DataGrid.Rows.Remove(row);
                    }
                    catch (MySqlException ex)
                    {
                        SearchDao.HandleException_SQLError_CloseApp(ex);
                    }
                    catch (Exception ex)
                    {
                        SearchDao.HandleException_GeneralError_CloseApp(ex);
                    }
                }
            }
            else
            {
                MessageBox.Show(@"Nothing Removed", @"Inventory Removal Alert");
            }
        }

        /// <summary>
        /// Handles the SelectionChanged event for the RemoveTab_DataGrid.
        /// This method updates the visibility of the "ID" column and enables or disables buttons based on the selection count.
        /// </summary>
        private void RemoveTab_DataGrid_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                // Hide the "ID" column if it exists
                if (RemoveTab_DataGrid.Columns.Contains("ID"))
                {
                    RemoveTab_DataGrid.Columns["ID"].Visible = false;
                }

                // Enable or disable buttons based on the selection count
                var hasSelectedCells = RemoveTab_DataGrid.SelectedCells.Count > 0;
                RemoveTab_Button_Edit.Visible = hasSelectedCells;
                RemoveTab_Button_Delete.Enabled = hasSelectedCells;
                RemoveTab_Button_Print.Enabled = hasSelectedCells;
                MainForm_MenuStrip_File_Print.Enabled = hasSelectedCells;

                var success = RemoveTab_DataGrid.RowCount > 0;
                RemoveTab_Image_NothingFound.Visible = !success;
            }
            catch (MySqlException ex)
            {
                AppLogger.LogDatabaseError(ex);
                // Automatically capture the control name from sender if possible
                var controlName = (sender as Control)?.Name ?? "RemoveTab_DataGrid";
                ErrorLogDao.HandleException_SQLError_CloseApp(ex, true, nameof(RemoveTab_DataGrid_SelectionChanged),
                    controlName);
            }
            catch (Exception ex)
            {
                AppLogger.LogDatabaseError(ex);
                var controlName = (sender as Control)?.Name ?? "RemoveTab_DataGrid";
                ErrorLogDao.HandleException_GeneralError_CloseApp(ex, true, nameof(RemoveTab_DataGrid_SelectionChanged),
                    controlName);
            }
        }

        /// <summary>
        /// Handles the click event for the TransferTab_Search button.
        /// This method searches for records in the database based on the selected part number,
        /// and updates the TransferDataGrid with the search results.
        /// It also enables/disables relevant controls based on the search results.
        /// </summary>
        private async void TransferTab_Button_Search_Clicked(object sender, EventArgs e)
        {
            try
            {
                var data = await SearchDao.TransferTab_SearchAsync(TransferTab_ComboBox_Part.Text);
                _transferBindingSource.DataSource = data;
                TransferDataGrid.DataSource = _transferBindingSource;
                Helper_TransferTab_ButtonEnabler();
                if (TransferDataGrid.Columns.Contains("ID"))
                {
                    TransferDataGrid.Columns["ID"].Visible = false;
                }

                DgvDesigner.InitializeDataGridView(TransferDataGrid, null);
                TransferTab_TextBox_Qty.Enabled = true;
                TransferTab_ComboBox_Part.Enabled = false;
                TransferTab_Panel_DataGrid.Focus();
                TransferTab_Image_Nothing.Visible = TransferDataGrid.RowCount == 0;
            }
            catch (MySqlException ex)
            {
                SearchDao.HandleException_SQLError_CloseApp(ex);
            }
            catch (Exception ex)
            {
                SearchDao.HandleException_GeneralError_CloseApp(ex);
            }
            finally
            {
                TransferTab_ComboBox_Part.Enabled = true;
            }
        }

        /// <summary>
        /// Handles the click event for the TransferTab_Save button.
        /// This method saves the transfer details to the database based on the selected part and location.
        /// It handles both non-split and split transfers, and updates the UI accordingly.
        /// </summary>
        private void TransferTab_Button_Save_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (TransferTab_ComboBox_Part.SelectedIndex != 0)
                {
                    if (TransferDataGrid.SelectedRows.Count == 0)
                    {
                        MessageBox.Show(@"No row selected in the Transfer Data Grid.", @"Error", MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        return;
                    }

                    var selectedrowindex = TransferDataGrid.SelectedRows[0].Index;
                    var selectedRow = TransferDataGrid.Rows[selectedrowindex];

                    var savedQty = selectedRow.Cells["Quantity"].Value as int? ?? 0;

                    if (string.IsNullOrEmpty(TransferTab_TextBox_Qty.Text))
                    {
                        TransferTab_TextBox_Qty.Text = savedQty.ToString();
                    }

                    if (!int.TryParse(TransferTab_TextBox_Qty.Text, out var enteredQty))
                    {
                        MessageBox.Show(@"Invalid quantity entered.", @"Error", MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        return;
                    }

                    var remaining = savedQty - enteredQty;

                    WipAppVariables.partId = selectedRow.Cells["PartID"].Value?.ToString() ?? string.Empty;
                    WipAppVariables.id = selectedRow.Cells["ID"].Value?.ToString() ?? string.Empty;
                    WipAppVariables.Operation = selectedRow.Cells["Op"].Value?.ToString() ?? string.Empty;
                    WipAppVariables.OldLocation = selectedRow.Cells["Location"].Value?.ToString() ?? string.Empty;
                    WipAppVariables.NewLocation = TransferTab_ComboBox_Loc.SelectedValue?.ToString() ?? string.Empty;
                    WipAppVariables.Notes = selectedRow.Cells["Notes"].Value?.ToString() ?? string.Empty;
                    WipAppVariables.PartType = selectedRow.Cells["Type"].Value?.ToString() ?? string.Empty;

                    MainForm_StatusStrip_SavedStatus.Text =
                        $@"Part Number: {WipAppVariables.partId} | From: {WipAppVariables.OldLocation} | Moved to: {WipAppVariables.NewLocation}";

                    if (savedQty == enteredQty)
                    {
                        WipAppVariables.TransferType =
                            $"Transfer: {enteredQty} moved to {WipAppVariables.NewLocation} from {WipAppVariables.OldLocation}.";
                        SearchDao.TransferTab_Save_NonSplit(
                            WipAppVariables.NewLocation,
                            WipAppVariables.id,
                            WipAppVariables.OldLocation,
                            WipAppVariables.partId,
                            savedQty.ToString(),
                            WipAppVariables.TransferType);
                    }
                    else
                    {
                        WipAppVariables.TransferType =
                            $"Transfer: {enteredQty} moved to {WipAppVariables.NewLocation} from {WipAppVariables.OldLocation}.";
                        SearchDao.TransferTab_Save_Split(
                            WipAppVariables.NewLocation,
                            WipAppVariables.id,
                            WipAppVariables.OldLocation,
                            WipAppVariables.partId,
                            enteredQty.ToString(),
                            WipAppVariables.TransferType,
                            WipAppVariables.Operation,
                            WipAppVariables.Notes,
                            remaining.ToString(),
                            WipAppVariables.PartType);
                    }

                    Helper_TabControl_ResetTab3();
                }
                else
                {
                    MessageBox.Show(@"Please select a valid part.", @"Error", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            catch (MySqlException ex)
            {
                SearchDao.HandleException_SQLError_CloseApp(ex);
            }
            catch (Exception ex)
            {
                SearchDao.HandleException_GeneralError_CloseApp(ex);
            }
        }

        /// <summary>
        /// Handles the click event for the TransferTab_Reset button.
        /// This method resets the controls on the Transfer tab to their default state by calling the Helper_TabControl_ResetTab3 method.
        /// </summary>
        private async void TransferTab_Button_Reset_Clicked(object sender, EventArgs e)
        {
            var button = TransferTab_Button_Reset;
            if (!button.Enabled)
            {
                return;
            }

            button.Enabled = false;
            SetSqlInteractiveControlsEnabled(false);
            try
            {
                await ClearAndResetAllComboBoxesAsync();
                TransferTab_ComboBox_Part.Focus();
                TransferTab_Image_Nothing.Visible = false;
            }
            catch (MySqlException ex)
            {
                SearchDao.HandleException_SQLError_CloseApp(ex);
            }
            catch (Exception ex)
            {
                SearchDao.HandleException_GeneralError_CloseApp(ex);
            }
            finally
            {
                SetSqlInteractiveControlsEnabled(true);
                button.Enabled = true;
            }
        }

        /// <summary>
        /// Handles the click event for the "View All WIP" tool strip menu item.
        /// This method retrieves all WIP locations from the database and updates the RemoveTab_DataGrid with the results.
        /// It also disables certain controls and sets the selected tab to the Remove tab.
        /// </summary>
        private async void ToolStrip_ViewAllWIP_Clicked(object sender, EventArgs e)
        {
            try
            {
                RemoveTab_ComboBox_Part.Enabled = false;
                RemoveTab_ComboBox_Op.Enabled = false;
                RemoveTab_Button_Search.Enabled = false;
                MainForm_TabControl.SelectedIndex = 1;
                var data = await SearchDao.RemoveTab_GetAllLocationsAsync();
                _inventoryBindingSource.DataSource = data;
                RemoveTab_DataGrid.DataSource = _inventoryBindingSource;
                if (RemoveTab_DataGrid.Columns.Contains("ID"))
                {
                    RemoveTab_DataGrid.Columns["ID"].Visible = false;
                }

                DgvDesigner.InitializeDataGridView(RemoveTab_DataGrid, null);
            }
            catch (MySqlException ex)
            {
                SearchDao.HandleException_SQLError_CloseApp(ex);
            }
            catch (Exception ex)
            {
                SearchDao.HandleException_GeneralError_CloseApp(ex);
            }
        }

        /// <summary>
        /// Handles the click event for the "Settings" tool strip menu item.
        /// This method opens the SettingsForm.
        /// </summary>
        private void ToolStrip_Settings_Clicked(object sender, EventArgs e)
        {
            try
            {
                var change = new SettingsForm();
                Enabled = false;
                change.FormClosed +=
                    (_, _) => Enabled = true;
                change.Show();
            }
            catch (MySqlException ex)
            {
                SearchDao.HandleException_SQLError_CloseApp(ex);
            }
            catch (Exception ex)
            {
                SearchDao.HandleException_GeneralError_CloseApp(ex);
            }
        }

        /// <summary>
        /// Handles the click event for the "History" tool strip menu item.
        /// This method opens the PersonalHistory form.
        /// </summary>
        private void ToolStrip_History_Clicked(object sender, EventArgs e)
        {
            try
            {
                var change = new PersonalHistory();
                Enabled = false;
                change.FormClosed +=
                    (_, _) => Enabled = true;
                change.Show();
            }
            catch (MySqlException ex)
            {
                SearchDao.HandleException_SQLError_CloseApp(ex);
            }
            catch (Exception ex)
            {
                SearchDao.HandleException_GeneralError_CloseApp(ex);
            }
        }


        /// <summary>
        /// Handles the click event for the "Save" tool strip menu item.
        /// This method triggers the save action for the currently selected tab.
        /// </summary>
        private void ToolStrip_Save_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (MainForm_TabControl.SelectedTab == MainForm_TabControl.TabPages["MainForm_TabControl_Inventory"])
                {
                    if (InventoryTab_Button_Save.Enabled)
                    {
                        InventoryTab_Button_Save_Clicked(null, null);
                    }
                }

                if (MainForm_TabControl.SelectedTab == MainForm_TabControl.TabPages["MainForm_TabControl_Transfer"])
                {
                    if (TransferTab_Button_Save.Enabled)
                    {
                        TransferTab_Button_Save_Clicked(null, null);
                    }
                }

                if (MainForm_TabControl.SelectedTab == MainForm_TabControl.TabPages["MainForm_TabControl_Remove"])
                {
                    if (RemoveTab_Button_Search.Enabled)
                    {
                        RemoveTab_Button_Search_Clicked(null, null);
                    }
                }
            }
            catch (MySqlException ex)
            {
                SearchDao.HandleException_SQLError_CloseApp(ex);
            }
            catch (Exception ex)
            {
                SearchDao.HandleException_GeneralError_CloseApp(ex);
            }
        }

        /// <summary>
        /// Handles the click event for the "Reset All Tabs" tool strip menu item.
        /// This method resets all tabs to their default state.
        /// </summary>
        private async void ToolStrip_ResetAllTabs_Clicked(object sender, EventArgs e)
        {
            try
            {
                Helper_TabControl_ResetTab1();
                await ClearAndResetAllComboBoxesAsync();
                Helper_TabControl_ResetTab3();
                Primary_TabControl_SelectedIndexChanged(null, null);
            }
            catch (MySqlException ex)
            {
                SearchDao.HandleException_SQLError_CloseApp(ex);
            }
            catch (Exception ex)
            {
                SearchDao.HandleException_GeneralError_CloseApp(ex);
            }
        }

        /// <summary>
        /// Handles the click event for the "Outside Service" tool strip menu item.
        /// This method retrieves all outside service records from the database and updates the RemoveTab_DataGrid with the results.
        /// It also disables certain controls and sets the selected tab to the Remove tab.
        /// </summary>
        private async void ToolStrip_OutsideService_Clicked(object sender, EventArgs e)
        {
            try
            {
                RemoveTab_ComboBox_Part.Enabled = false;
                RemoveTab_ComboBox_Op.Enabled = false;
                RemoveTab_Button_Search.Enabled = false;
                MainForm_TabControl.SelectedIndex = 1;
                var data = await SearchDao.RemovalTab_GetOutsideServiceAsync();
                _inventoryBindingSource.DataSource = data;
                RemoveTab_DataGrid.DataSource = _inventoryBindingSource;
                if (RemoveTab_DataGrid.Columns.Contains("ID"))
                {
                    RemoveTab_DataGrid.Columns["ID"].Visible = false;
                }

                DgvDesigner.InitializeDataGridView(RemoveTab_DataGrid, null);
            }
            catch (MySqlException ex)
            {
                SearchDao.HandleException_SQLError_CloseApp(ex);
            }
            catch (Exception ex)
            {
                SearchDao.HandleException_GeneralError_CloseApp(ex);
            }
        }

        /// <summary>
        /// Handles the click event for the "Delete" tool strip menu item.
        /// This method triggers the delete action for the Remove tab if it is selected and the delete button is enabled.
        /// </summary>
        private void ToolStrip_Delete_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (MainForm_TabControl.SelectedIndex == 1)
                {
                    if (RemoveTab_Button_Delete.Enabled)
                    {
                        RemoveTab_Button_Delete_Clicked(null, null);
                    }
                }
            }
            catch (MySqlException ex)
            {
                SearchDao.HandleException_SQLError_CloseApp(ex);
            }
            catch (Exception ex)
            {
                SearchDao.HandleException_GeneralError_CloseApp(ex);
            }
        }

        /// <summary>
        /// Handles the event when the selected part in the InventoryTab_ComboBox_Part changes.
        /// This method checks if the selected part is of type "Dunnage" and updates the UI accordingly.
        /// If the part is "Dunnage", it sets the operation to "N/A" and quantity to 1, and disables the respective controls.
        /// Otherwise, it enables the operation and quantity controls.
        /// </summary>
        private void InventoryTab_ComboBox_Part_DunnageCheck(object sender, EventArgs e)
        {
            try
            {
                if (InventoryTab_ComboBox_Part.SelectedIndex >= 1)
                {
                    foreach (DataRow row in _inventoryTabPartCbDataTable.Rows)
                    {
                        var partId = row.Field<string>("Item Number");
                        var partType = row.Field<string>("Type");
                        if (partId == InventoryTab_ComboBox_Part.Text && partId != "[ Enter Part ID ]")
                        {
                            if (partType == "Dunnage")
                            {
                                InventoryTab_ComboBox_Op.Text = @"N/A";
                                InventoryTab_ComboBox_Op.Enabled = false;
                                InventoryTab_TextBox_Qty.Text = @"1";
                                InventoryTab_TextBox_Qty.Enabled = false;
                                InventoryTab_ComboBox_Loc.Focus();
                            }
                            else
                            {
                                if (InventoryTab_ComboBox_Op.Text == @"N/A")
                                {
                                    InventoryTab_ComboBox_Op.SelectedIndex = 0;
                                }

                                if (InventoryTab_TextBox_Qty.Text == @"1")
                                {
                                    InventoryTab_TextBox_Qty.Clear();
                                }

                                InventoryTab_ComboBox_Op.Enabled = true;
                                InventoryTab_TextBox_Qty.Enabled = true;
                            }

                            break;
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                SearchDao.HandleException_SQLError_CloseApp(ex);
            }
            catch (Exception ex)
            {
                SearchDao.HandleException_GeneralError_CloseApp(ex);
            }
        }

        /// <summary>
        /// Handles the CheckedChanged event for the InventoryTab_CheckBox_Multi control.
        /// This method enables or disables the InventoryTab_TextBox_HowMany and InventoryTab_CheckBox_Multi_Different controls based on the state of the checkbox.
        /// It also validates the input fields and enables or disables the save button accordingly.
        /// </summary>
        private void InventoryTab_CheckBox_Multiple_Changed(object sender, EventArgs e)
        {
            try
            {
                if (InventoryTab_CheckBox_Multi.Checked)
                {
                    InventoryTab_TextBox_HowMany.Enabled = true;
                    InventoryTab_CheckBox_Multi_Different.Enabled = true;
                }
                else
                {
                    InventoryTab_CheckBox_Multi_Different.Enabled = false;
                    InventoryTab_CheckBox_Multi_Different.Checked = false;
                    InventoryTab_TextBox_HowMany.Enabled = false;
                }

                if (InventoryTab_ComboBox_Part.SelectedIndex != 0 &&
                    InventoryTab_ComboBox_Op.SelectedIndex != 0 &&
                    InventoryTab_ComboBox_Loc.SelectedIndex != 0 &&
                    InventoryTab_TextBox_Qty.Text.Length != 0 &&
                    int.TryParse(InventoryTab_TextBox_Qty.Text, out _))
                {
                    InventoryTab_Button_Save.Enabled = true;
                    MainForm_MenuStrip_File_Save.Enabled = true;
                }
                else
                {
                    InventoryTab_Button_Save.Enabled = false;
                    MainForm_MenuStrip_File_Save.Enabled = false;
                }
            }
            catch (MySqlException ex)
            {
                SearchDao.HandleException_SQLError_CloseApp(ex);
            }
            catch (Exception ex)
            {
                SearchDao.HandleException_GeneralError_CloseApp(ex);
            }
        }


        /// <summary>
        /// Handles the SelectedIndexChanged event for the MainForm_TabControl.
        /// This method updates the active control, menu strip text, and visibility of the print option based on the selected tab.
        /// It also resets the controls on all tabs to their default state.
        /// </summary>
        private void Primary_TabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (MainForm_TabControl.SelectedIndex == 0)
                {
                    ActiveControl = InventoryTab_ComboBox_Part;
                    MainForm_MenuStrip_File_Save.Text = @"Save";
                    MainForm_MenuStrip_View_Reset.Text = @"Reset New Transaction";
                    MainForm_MenuStrip_File_Print.Visible = false;
                    Helper_TabControl_ResetTab1();
                    Helper_TabControl_ResetTab2();
                    Helper_TabControl_ResetTab3();

                    InventoryTab_ComboBox_Loc.Select(InventoryTab_ComboBox_Loc.Text.Length, 0);
                    InventoryTab_ComboBox_Op.Select(InventoryTab_ComboBox_Op.Text.Length, 0);
                    InventoryTab_TextBox_Qty.Select(InventoryTab_TextBox_Qty.Text.Length, 0);
                    InventoryTab_RichTextBox_Notes.Select(InventoryTab_RichTextBox_Notes.Text.Length, 0);
                    InventoryTab_ComboBox_Part.SelectAll();
                    InventoryTab_ComboBox_Part.Focus();
                }
                else if (MainForm_TabControl.SelectedIndex == 1)
                {
                    ActiveControl = RemoveTab_ComboBox_Part;
                    MainForm_MenuStrip_File_Save.Text = @"Search";
                    MainForm_MenuStrip_View_Reset.Text = @"Reset Remove";
                    MainForm_MenuStrip_File_Print.Visible = true;
                    Helper_TabControl_ResetTab1();
                    Helper_TabControl_ResetTab2();
                    Helper_TabControl_ResetTab3();

                    RemoveTab_ComboBox_Op.Select(0, 0);
                    RemoveTab_ComboBox_Part.SelectAll();
                    RemoveTab_ComboBox_Part.Focus();
                }
                else if (MainForm_TabControl.SelectedIndex == 2)
                {
                    ActiveControl = TransferTab_ComboBox_Part;
                    MainForm_MenuStrip_File_Save.Text = @"Save";
                    MainForm_MenuStrip_View_Reset.Text = @"Reset Transfer";
                    MainForm_MenuStrip_File_Print.Visible = false;
                    Helper_TabControl_ResetTab1();
                    Helper_TabControl_ResetTab2();
                    Helper_TabControl_ResetTab3();

                    TransferTab_ComboBox_Loc.Select(0, 0);
                    TransferTab_ComboBox_Part.SelectAll();
                    TransferTab_ComboBox_Part.Focus();
                }
            }
            catch (MySqlException ex)
            {
                SearchDao.HandleException_SQLError_CloseApp(ex);
            }
            catch (Exception ex)
            {
                SearchDao.HandleException_GeneralError_CloseApp(ex);
            }
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event for the TransferTab_ComboBox_Loc control.
        /// This method updates the foreground color of the ComboBox and enables or disables the save button based on the selected index.
        /// </summary>
        private void TransferTab_ComboBox_Location_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (TransferTab_ComboBox_Loc.SelectedIndex < 0)
                {
                    TransferTab_ComboBox_Loc.SelectedIndex = 0;
                    TransferTab_ComboBox_Loc.ForeColor = Color.Red;
                    TransferTab_Button_Save.Enabled = false;
                    MainForm_MenuStrip_File_Save.Enabled = false;
                }
                else if (TransferTab_ComboBox_Part.SelectedIndex > 0 && TransferTab_ComboBox_Loc.SelectedIndex > 0)
                {
                    TransferTab_Button_Save.Enabled = true;
                    MainForm_MenuStrip_File_Save.Enabled = true;
                }
                else
                {
                    TransferTab_Button_Save.Enabled = false;
                    MainForm_MenuStrip_File_Save.Enabled = false;
                }
            }
            catch (MySqlException ex)
            {
                SearchDao.HandleException_SQLError_CloseApp(ex);
            }
            catch (Exception ex)
            {
                SearchDao.HandleException_GeneralError_CloseApp(ex);
            }
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event for the TransferTab_ComboBox_Part control.
        /// This method enables or disables the save button based on the selected part and location.
        /// </summary>
        private void TransferTab_ComboBox_Part_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Helper_TransferTab_ButtonEnabler();
            }
            catch (MySqlException ex)
            {
                SearchDao.HandleException_SQLError_CloseApp(ex);
            }
            catch (Exception ex)
            {
                SearchDao.HandleException_GeneralError_CloseApp(ex);
            }
        }

        /// <summary>
        /// Handles the click event for the "View Tab 1" tool strip menu item.
        /// This method sets the selected tab to the Inventory tab.
        /// </summary>
        private void ToolStrip_View_Tab1(object sender, EventArgs e)
        {
            try
            {
                MainForm_TabControl.SelectedIndex = 0;
            }
            catch (MySqlException ex)
            {
                SearchDao.HandleException_SQLError_CloseApp(ex);
            }
            catch (Exception ex)
            {
                SearchDao.HandleException_GeneralError_CloseApp(ex);
            }
        }

        /// <summary>
        /// Handles the click event for the "View Tab 2" tool strip menu item.
        /// This method sets the selected tab to the Remove tab.
        /// </summary>
        private void ToolStrip_View_Tab2(object sender, EventArgs e)
        {
            try
            {
                MainForm_TabControl.SelectedIndex = 1;
            }
            catch (MySqlException ex)
            {
                SearchDao.HandleException_SQLError_CloseApp(ex);
            }
            catch (Exception ex)
            {
                SearchDao.HandleException_GeneralError_CloseApp(ex);
            }
        }

        /// <summary>
        /// Handles the click event for the "View Tab 3" tool strip menu item.
        /// This method sets the selected tab to the Transfer tab.
        /// </summary>
        private void ToolStrip_View_Tab3(object sender, EventArgs e)
        {
            try
            {
                MainForm_TabControl.SelectedIndex = 2;
            }
            catch (MySqlException ex)
            {
                SearchDao.HandleException_SQLError_CloseApp(ex);
            }
            catch (Exception ex)
            {
                SearchDao.HandleException_GeneralError_CloseApp(ex);
            }
        }


        /// <summary>
        /// Fills the "Last 10 Transactions" list with data from the database.
        /// This method retrieves the last 10 transactions from the database and updates the WipApp_Variables.list.
        /// It also updates the UI controls to reflect the retrieved data.
        /// </summary>
        internal async Task Last10FillAsync()
        {
            WipAppVariables.list.Clear();
            MySqlConnection connection = null;
            MySqlCommand command = null;
            MySqlDataReader reader = null;
            try
            {
                connection = new MySqlConnection(WipAppVariables.connectionString);
                await connection.OpenAsync();
                command = new MySqlCommand("SELECT * FROM last_10_transactions;", connection);

                reader = (MySqlDataReader)await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    Last10 a = new()
                    {
                        Id = reader.GetInt32(0),
                        PartId = reader.GetString(1),
                        Op = reader.GetString(2),
                        Quantity = reader.GetInt32(3)
                    };
                    WipAppVariables.list.Add(a);
                }
            }
            catch (MySqlException ex)
            {
                SearchDao.HandleException_SQLError_CloseApp(ex);
            }
            catch (Exception ex)
            {
                SearchDao.HandleException_GeneralError_CloseApp(ex);
            }
            finally
            {
                reader?.Close();
                reader?.Dispose();
                command?.Dispose();
                connection?.Close();
                connection?.Dispose();

                if (WipAppVariables.list.Count > 0)
                {
                    if (InvokeRequired)
                    {
                        Invoke(new Action(UpdateLast10Buttons));
                    }
                    else
                    {
                        UpdateLast10Buttons();
                    }
                }
            }
        }

        private void UpdateLast10Buttons()
        {
            MainForm_StatusStrip_Disconnected.Visible = false;
            MainForm_StatusStrip_SavedStatus.Visible = true;
            foreach (Control c in Controls)
            {
                c.Enabled = true;
            }

            MainForm_Last10_Button_01.Text = $@"{WipAppVariables.list[0].PartId} ({WipAppVariables.list[0].Op})";
            MainForm_Last10_Button_02.Text = $@"{WipAppVariables.list[1].PartId} ({WipAppVariables.list[1].Op})";
            MainForm_Last10_Button_03.Text = $@"{WipAppVariables.list[2].PartId} ({WipAppVariables.list[2].Op})";
            MainForm_Last10_Button_04.Text = $@"{WipAppVariables.list[3].PartId} ({WipAppVariables.list[3].Op})";
            MainForm_Last10_Button_05.Text = $@"{WipAppVariables.list[4].PartId} ({WipAppVariables.list[4].Op})";
            MainForm_Last10_Button_06.Text = $@"{WipAppVariables.list[5].PartId} ({WipAppVariables.list[5].Op})";
            MainForm_Last10_Button_07.Text = $@"{WipAppVariables.list[6].PartId} ({WipAppVariables.list[6].Op})";
            MainForm_Last10_Button_08.Text = $@"{WipAppVariables.list[7].PartId} ({WipAppVariables.list[7].Op})";
            MainForm_Last10_Button_09.Text = $@"{WipAppVariables.list[8].PartId} ({WipAppVariables.list[8].Op})";
            MainForm_Last10_Button_10.Text = $@"{WipAppVariables.list[9].PartId} ({WipAppVariables.list[9].Op})";
        }


        /// <summary>
        /// Handles the click event for the MainForm_Last10_Button_01 button.
        /// This method resets the tabs and updates the controls based on the first item in the "Last 10 Transactions" list.
        /// </summary>
        private void Primary_Button_Last10_01_Clicked(object sender, EventArgs e)
        {
            try
            {
                Helper_TabControl_ResetTab1();
                Helper_TabControl_ResetTab2();
                Helper_TabControl_ResetTab3();

                if (MainForm_TabControl.SelectedIndex == 0)
                {
                    if (WipAppVariables.list[0].PartId != "[ Blank ]")
                    {
                        MainForm_Last10_Button_01.Enabled = true;
                        InventoryTab_ComboBox_Part.Text = WipAppVariables.list[0].PartId;
                        InventoryTab_ComboBox_Op.Text = WipAppVariables.list[0].Op;
                        InventoryTab_TextBox_Qty.Text = WipAppVariables.list[0].Quantity.ToString();
                        InventoryTab_ComboBox_Part.ForeColor = Color.Black;
                        InventoryTab_ComboBox_Op.ForeColor = Color.Black;
                        InventoryTab_TextBox_Qty.ForeColor = Color.Black;
                        InventoryTab_ComboBox_Loc.Focus();
                    }
                    else
                    {
                        MainForm_Last10_Button_01.Enabled = false;
                    }
                }

                if (MainForm_TabControl.SelectedIndex == 1)
                {
                    if (WipAppVariables.list[0].PartId != "[ Blank ]")
                    {
                        MainForm_Last10_Button_01.Enabled = true;
                        RemoveTab_ComboBox_Part.Text = WipAppVariables.list[0].PartId;
                        RemoveTab_ComboBox_Op.Text = WipAppVariables.list[0].Op;
                        RemoveTab_ComboBox_Part.ForeColor = Color.Black;
                        RemoveTab_ComboBox_Op.ForeColor = Color.Black;
                        RemoveTab_Button_Search_Clicked(null, null);
                    }
                    else
                    {
                        MainForm_Last10_Button_01.Enabled = false;
                    }
                }

                if (MainForm_TabControl.SelectedIndex == 2)
                {
                    if (WipAppVariables.list[0].PartId != "[ Blank ]")
                    {
                        MainForm_Last10_Button_01.Enabled = true;
                        TransferTab_ComboBox_Part.Text = WipAppVariables.list[0].PartId;
                        TransferTab_ComboBox_Part.ForeColor = Color.Black;
                        TransferTab_Button_Search_Clicked(null, null);
                    }
                    else
                    {
                        MainForm_Last10_Button_01.Enabled = false;
                    }
                }
            }
            catch (MySqlException ex)
            {
                SearchDao.HandleException_SQLError_CloseApp(ex);
            }
            catch (Exception ex)
            {
                SearchDao.HandleException_GeneralError_CloseApp(ex);
            }
        }

        /// <summary>
        /// Handles the click event for the MainForm_Last10_Button_02 button.
        /// This method resets the tabs and updates the controls based on the second item in the "Last 10 Transactions" list.
        /// </summary>
        private void Primary_Button_Last10_02_Clicked(object sender, EventArgs e)
        {
            try
            {
                Helper_TabControl_ResetTab1();
                Helper_TabControl_ResetTab2();
                Helper_TabControl_ResetTab3();

                if (MainForm_TabControl.SelectedIndex == 0)
                {
                    if (WipAppVariables.list[1].PartId != "[ Blank ]")
                    {
                        MainForm_Last10_Button_02.Enabled = true;
                        InventoryTab_ComboBox_Part.Text = WipAppVariables.list[1].PartId;
                        InventoryTab_ComboBox_Op.Text = WipAppVariables.list[1].Op;
                        InventoryTab_TextBox_Qty.Text = WipAppVariables.list[1].Quantity.ToString();
                        InventoryTab_ComboBox_Part.ForeColor = Color.Black;
                        InventoryTab_ComboBox_Op.ForeColor = Color.Black;
                        InventoryTab_TextBox_Qty.ForeColor = Color.Black;
                        InventoryTab_ComboBox_Loc.Focus();
                    }
                    else
                    {
                        MainForm_Last10_Button_02.Enabled = false;
                    }
                }

                if (MainForm_TabControl.SelectedIndex == 1)
                {
                    if (WipAppVariables.list[1].PartId != "[ Blank ]")
                    {
                        MainForm_Last10_Button_02.Enabled = true;
                        RemoveTab_ComboBox_Part.Text = WipAppVariables.list[1].PartId;
                        RemoveTab_ComboBox_Op.Text = WipAppVariables.list[1].Op;
                        RemoveTab_ComboBox_Part.ForeColor = Color.Black;
                        RemoveTab_ComboBox_Op.ForeColor = Color.Black;
                        RemoveTab_Button_Search_Clicked(null, null);
                    }
                    else
                    {
                        MainForm_Last10_Button_02.Enabled = false;
                    }
                }

                if (MainForm_TabControl.SelectedIndex == 2)
                {
                    if (WipAppVariables.list[1].PartId != "[ Blank ]")
                    {
                        MainForm_Last10_Button_02.Enabled = true;
                        TransferTab_ComboBox_Part.Text = WipAppVariables.list[1].PartId;
                        TransferTab_ComboBox_Part.ForeColor = Color.Black;
                        TransferTab_Button_Search_Clicked(null, null);
                    }
                    else
                    {
                        MainForm_Last10_Button_02.Enabled = false;
                    }
                }
            }
            catch (MySqlException ex)
            {
                SearchDao.HandleException_SQLError_CloseApp(ex);
            }
            catch (Exception ex)
            {
                SearchDao.HandleException_GeneralError_CloseApp(ex);
            }
        }

        /// <summary>
        /// Handles the click event for the MainForm_Last10_Button_03 button.
        /// This method resets the tabs and updates the controls based on the third item in the "Last 10 Transactions" list.
        /// </summary>
        private void Primary_Button_Last10_03_Clicked(object sender, EventArgs e)
        {
            try
            {
                Helper_TabControl_ResetTab1();
                Helper_TabControl_ResetTab2();
                Helper_TabControl_ResetTab3();

                if (MainForm_TabControl.SelectedIndex == 0)
                {
                    if (WipAppVariables.list[2].PartId != "[ Blank ]")
                    {
                        MainForm_Last10_Button_03.Enabled = true;
                        InventoryTab_ComboBox_Part.Text = WipAppVariables.list[2].PartId;
                        InventoryTab_ComboBox_Op.Text = WipAppVariables.list[2].Op;
                        InventoryTab_TextBox_Qty.Text = WipAppVariables.list[2].Quantity.ToString();
                        InventoryTab_ComboBox_Part.ForeColor = Color.Black;
                        InventoryTab_ComboBox_Op.ForeColor = Color.Black;
                        InventoryTab_TextBox_Qty.ForeColor = Color.Black;
                        InventoryTab_ComboBox_Loc.Focus();
                    }
                    else
                    {
                        MainForm_Last10_Button_03.Enabled = false;
                    }
                }

                if (MainForm_TabControl.SelectedIndex == 1)
                {
                    if (WipAppVariables.list[2].PartId != "[ Blank ]")
                    {
                        MainForm_Last10_Button_03.Enabled = true;
                        RemoveTab_ComboBox_Part.Text = WipAppVariables.list[2].PartId;
                        RemoveTab_ComboBox_Op.Text = WipAppVariables.list[2].Op;
                        RemoveTab_ComboBox_Part.ForeColor = Color.Black;
                        RemoveTab_ComboBox_Op.ForeColor = Color.Black;
                        RemoveTab_Button_Search_Clicked(null, null);
                    }
                    else
                    {
                        MainForm_Last10_Button_03.Enabled = false;
                    }
                }

                if (MainForm_TabControl.SelectedIndex == 2)
                {
                    if (WipAppVariables.list[2].PartId != "[ Blank ]")
                    {
                        MainForm_Last10_Button_03.Enabled = true;
                        TransferTab_ComboBox_Part.Text = WipAppVariables.list[2].PartId;
                        TransferTab_ComboBox_Part.ForeColor = Color.Black;
                        TransferTab_Button_Search_Clicked(null, null);
                    }
                    else
                    {
                        MainForm_Last10_Button_03.Enabled = false;
                    }
                }
            }
            catch (MySqlException ex)
            {
                SearchDao.HandleException_SQLError_CloseApp(ex);
            }
            catch (Exception ex)
            {
                SearchDao.HandleException_GeneralError_CloseApp(ex);
            }
        }

        /// <summary>
        /// Handles the click event for the MainForm_Last10_Button_04 button.
        /// This method resets the tabs and updates the controls based on the fourth item in the "Last 10 Transactions" list.
        /// </summary>
        private void Primary_Button_Last10_04_Clicked(object sender, EventArgs e)
        {
            try
            {
                Helper_TabControl_ResetTab1();
                Helper_TabControl_ResetTab2();
                Helper_TabControl_ResetTab3();

                if (MainForm_TabControl.SelectedIndex == 0)
                {
                    if (WipAppVariables.list[3].PartId != "[ Blank ]")
                    {
                        MainForm_Last10_Button_04.Enabled = true;
                        InventoryTab_ComboBox_Part.Text = WipAppVariables.list[3].PartId;
                        InventoryTab_ComboBox_Op.Text = WipAppVariables.list[3].Op;
                        InventoryTab_TextBox_Qty.Text = WipAppVariables.list[3].Quantity.ToString();
                        InventoryTab_ComboBox_Part.ForeColor = Color.Black;
                        InventoryTab_ComboBox_Op.ForeColor = Color.Black;
                        InventoryTab_TextBox_Qty.ForeColor = Color.Black;
                        InventoryTab_ComboBox_Loc.Focus();
                    }
                    else
                    {
                        MainForm_Last10_Button_04.Enabled = false;
                    }
                }

                if (MainForm_TabControl.SelectedIndex == 1)
                {
                    if (WipAppVariables.list[3].PartId != "[ Blank ]")
                    {
                        MainForm_Last10_Button_04.Enabled = true;
                        RemoveTab_ComboBox_Part.Text = WipAppVariables.list[3].PartId;
                        RemoveTab_ComboBox_Op.Text = WipAppVariables.list[3].Op;
                        RemoveTab_ComboBox_Part.ForeColor = Color.Black;
                        RemoveTab_ComboBox_Op.ForeColor = Color.Black;
                        RemoveTab_Button_Search_Clicked(null, null);
                    }
                    else
                    {
                        MainForm_Last10_Button_04.Enabled = false;
                    }
                }

                if (MainForm_TabControl.SelectedIndex == 2)
                {
                    if (WipAppVariables.list[3].PartId != "[ Blank ]")
                    {
                        MainForm_Last10_Button_04.Enabled = true;
                        TransferTab_ComboBox_Part.Text = WipAppVariables.list[3].PartId;
                        TransferTab_ComboBox_Part.ForeColor = Color.Black;
                        TransferTab_Button_Search_Clicked(null, null);
                    }
                    else
                    {
                        MainForm_Last10_Button_04.Enabled = false;
                    }
                }
            }
            catch (MySqlException ex)
            {
                SearchDao.HandleException_SQLError_CloseApp(ex);
            }
            catch (Exception ex)
            {
                SearchDao.HandleException_GeneralError_CloseApp(ex);
            }
        }

        /// <summary>
        /// Handles the click event for the MainForm_Last10_Button_05 button.
        /// This method resets the tabs and updates the controls based on the fifth item in the "Last 10 Transactions" list.
        /// </summary>
        private void Primary_Button_Last10_05_Clicked(object sender, EventArgs e)
        {
            try
            {
                Helper_TabControl_ResetTab1();
                Helper_TabControl_ResetTab2();
                Helper_TabControl_ResetTab3();

                if (MainForm_TabControl.SelectedIndex == 0)
                {
                    if (WipAppVariables.list[4].PartId != "[ Blank ]")
                    {
                        MainForm_Last10_Button_05.Enabled = true;
                        InventoryTab_ComboBox_Part.Text = WipAppVariables.list[4].PartId;
                        InventoryTab_ComboBox_Op.Text = WipAppVariables.list[4].Op;
                        InventoryTab_TextBox_Qty.Text = WipAppVariables.list[4].Quantity.ToString();
                        InventoryTab_ComboBox_Part.ForeColor = Color.Black;
                        InventoryTab_ComboBox_Op.ForeColor = Color.Black;
                        InventoryTab_TextBox_Qty.ForeColor = Color.Black;
                        InventoryTab_ComboBox_Loc.Focus();
                    }
                    else
                    {
                        MainForm_Last10_Button_05.Enabled = false;
                    }
                }

                if (MainForm_TabControl.SelectedIndex == 1)
                {
                    if (WipAppVariables.list[4].PartId != "[ Blank ]")
                    {
                        MainForm_Last10_Button_05.Enabled = true;
                        RemoveTab_ComboBox_Part.Text = WipAppVariables.list[4].PartId;
                        RemoveTab_ComboBox_Op.Text = WipAppVariables.list[4].Op;
                        RemoveTab_ComboBox_Part.ForeColor = Color.Black;
                        RemoveTab_ComboBox_Op.ForeColor = Color.Black;
                        RemoveTab_Button_Search_Clicked(null, null);
                    }
                    else
                    {
                        MainForm_Last10_Button_05.Enabled = false;
                    }
                }

                if (MainForm_TabControl.SelectedIndex == 2)
                {
                    if (WipAppVariables.list[4].PartId != "[ Blank ]")
                    {
                        MainForm_Last10_Button_05.Enabled = true;
                        TransferTab_ComboBox_Part.Text = WipAppVariables.list[4].PartId;
                        TransferTab_ComboBox_Part.ForeColor = Color.Black;
                        TransferTab_Button_Search_Clicked(null, null);
                    }
                    else
                    {
                        MainForm_Last10_Button_05.Enabled = false;
                    }
                }
            }
            catch (MySqlException ex)
            {
                SearchDao.HandleException_SQLError_CloseApp(ex);
            }
            catch (Exception ex)
            {
                SearchDao.HandleException_GeneralError_CloseApp(ex);
            }
        }

        /// <summary>
        /// Handles the click event for the MainForm_Last10_Button_06 button.
        /// This method resets the tabs and updates the controls based on the sixth item in the "Last 10 Transactions" list.
        /// </summary>
        private void Primary_Button_Last10_06_Clicked(object sender, EventArgs e)
        {
            try
            {
                Helper_TabControl_ResetTab1();
                Helper_TabControl_ResetTab2();
                Helper_TabControl_ResetTab3();

                if (MainForm_TabControl.SelectedIndex == 0)
                {
                    if (WipAppVariables.list[5].PartId != "[ Blank ]")
                    {
                        MainForm_Last10_Button_06.Enabled = true;
                        InventoryTab_ComboBox_Part.Text = WipAppVariables.list[5].PartId;
                        InventoryTab_ComboBox_Op.Text = WipAppVariables.list[5].Op;
                        InventoryTab_TextBox_Qty.Text = WipAppVariables.list[5].Quantity.ToString();
                        InventoryTab_ComboBox_Part.ForeColor = Color.Black;
                        InventoryTab_ComboBox_Op.ForeColor = Color.Black;
                        InventoryTab_TextBox_Qty.ForeColor = Color.Black;
                        InventoryTab_ComboBox_Loc.Focus();
                    }
                    else
                    {
                        MainForm_Last10_Button_06.Enabled = false;
                    }
                }

                if (MainForm_TabControl.SelectedIndex == 1)
                {
                    if (WipAppVariables.list[5].PartId != "[ Blank ]")
                    {
                        MainForm_Last10_Button_06.Enabled = true;
                        RemoveTab_ComboBox_Part.Text = WipAppVariables.list[5].PartId;
                        RemoveTab_ComboBox_Op.Text = WipAppVariables.list[5].Op;
                        RemoveTab_ComboBox_Part.ForeColor = Color.Black;
                        RemoveTab_ComboBox_Op.ForeColor = Color.Black;
                        RemoveTab_Button_Search_Clicked(null, null);
                    }
                    else
                    {
                        MainForm_Last10_Button_06.Enabled = false;
                    }
                }

                if (MainForm_TabControl.SelectedIndex == 2)
                {
                    if (WipAppVariables.list[5].PartId != "[ Blank ]")
                    {
                        MainForm_Last10_Button_06.Enabled = true;
                        TransferTab_ComboBox_Part.Text = WipAppVariables.list[5].PartId;
                        TransferTab_ComboBox_Part.ForeColor = Color.Black;
                        TransferTab_Button_Search_Clicked(null, null);
                    }
                    else
                    {
                        MainForm_Last10_Button_06.Enabled = false;
                    }
                }
            }
            catch (MySqlException ex)
            {
                SearchDao.HandleException_SQLError_CloseApp(ex);
            }
            catch (Exception ex)
            {
                SearchDao.HandleException_GeneralError_CloseApp(ex);
            }
        }

        /// <summary>
        /// Handles the click event for the MainForm_Last10_Button_07 button.
        /// This method resets the tabs and updates the controls based on the seventh item in the "Last 10 Transactions" list.
        /// </summary>
        private void Primary_Button_Last10_07_Clicked(object sender, EventArgs e)
        {
            try
            {
                Helper_TabControl_ResetTab1();
                Helper_TabControl_ResetTab2();
                Helper_TabControl_ResetTab3();

                if (MainForm_TabControl.SelectedIndex == 0)
                {
                    if (WipAppVariables.list[6].PartId != "[ Blank ]")
                    {
                        MainForm_Last10_Button_07.Enabled = true;
                        InventoryTab_ComboBox_Part.Text = WipAppVariables.list[6].PartId;
                        InventoryTab_ComboBox_Op.Text = WipAppVariables.list[6].Op;
                        InventoryTab_TextBox_Qty.Text = WipAppVariables.list[6].Quantity.ToString();
                        InventoryTab_ComboBox_Part.ForeColor = Color.Black;
                        InventoryTab_ComboBox_Op.ForeColor = Color.Black;
                        InventoryTab_TextBox_Qty.ForeColor = Color.Black;
                        InventoryTab_ComboBox_Loc.Focus();
                    }
                    else
                    {
                        MainForm_Last10_Button_07.Enabled = false;
                    }
                }

                if (MainForm_TabControl.SelectedIndex == 1)
                {
                    if (WipAppVariables.list[6].PartId != "[ Blank ]")
                    {
                        MainForm_Last10_Button_07.Enabled = true;
                        RemoveTab_ComboBox_Part.Text = WipAppVariables.list[6].PartId;
                        RemoveTab_ComboBox_Op.Text = WipAppVariables.list[6].Op;
                        RemoveTab_ComboBox_Part.ForeColor = Color.Black;
                        RemoveTab_ComboBox_Op.ForeColor = Color.Black;
                        RemoveTab_Button_Search_Clicked(null, null);
                    }
                    else
                    {
                        MainForm_Last10_Button_07.Enabled = false;
                    }
                }

                if (MainForm_TabControl.SelectedIndex == 2)
                {
                    if (WipAppVariables.list[6].PartId != "[ Blank ]")
                    {
                        MainForm_Last10_Button_07.Enabled = true;
                        TransferTab_ComboBox_Part.Text = WipAppVariables.list[6].PartId;
                        TransferTab_ComboBox_Part.ForeColor = Color.Black;
                        TransferTab_Button_Search_Clicked(null, null);
                    }
                    else
                    {
                        MainForm_Last10_Button_07.Enabled = false;
                    }
                }
            }
            catch (MySqlException ex)
            {
                SearchDao.HandleException_SQLError_CloseApp(ex);
            }
            catch (Exception ex)
            {
                SearchDao.HandleException_GeneralError_CloseApp(ex);
            }
        }

        /// <summary>
        /// Handles the click event for the MainForm_Last10_Button_08 button.
        /// This method resets the tabs and updates the controls based on the eighth item in the "Last 10 Transactions" list.
        /// </summary>
        private void Primary_Button_Last10_08_Clicked(object sender, EventArgs e)
        {
            try
            {
                Helper_TabControl_ResetTab1();
                Helper_TabControl_ResetTab2();
                Helper_TabControl_ResetTab3();

                if (MainForm_TabControl.SelectedIndex == 0)
                {
                    if (WipAppVariables.list[7].PartId != "[ Blank ]")
                    {
                        MainForm_Last10_Button_08.Enabled = true;
                        InventoryTab_ComboBox_Part.Text = WipAppVariables.list[7].PartId;
                        InventoryTab_ComboBox_Op.Text = WipAppVariables.list[7].Op;
                        InventoryTab_TextBox_Qty.Text = WipAppVariables.list[7].Quantity.ToString();
                        InventoryTab_ComboBox_Part.ForeColor = Color.Black;
                        InventoryTab_ComboBox_Op.ForeColor = Color.Black;
                        InventoryTab_TextBox_Qty.ForeColor = Color.Black;
                        InventoryTab_ComboBox_Loc.Focus();
                    }
                    else
                    {
                        MainForm_Last10_Button_08.Enabled = false;
                    }
                }

                if (MainForm_TabControl.SelectedIndex == 1)
                {
                    if (WipAppVariables.list[7].PartId != "[ Blank ]")
                    {
                        MainForm_Last10_Button_08.Enabled = true;
                        RemoveTab_ComboBox_Part.Text = WipAppVariables.list[7].PartId;
                        RemoveTab_ComboBox_Op.Text = WipAppVariables.list[7].Op;
                        RemoveTab_ComboBox_Part.ForeColor = Color.Black;
                        RemoveTab_ComboBox_Op.ForeColor = Color.Black;
                        RemoveTab_Button_Search_Clicked(null, null);
                    }
                    else
                    {
                        MainForm_Last10_Button_08.Enabled = false;
                    }
                }

                if (MainForm_TabControl.SelectedIndex == 2)
                {
                    if (WipAppVariables.list[7].PartId != "[ Blank ]")
                    {
                        MainForm_Last10_Button_08.Enabled = true;
                        TransferTab_ComboBox_Part.Text = WipAppVariables.list[7].PartId;
                        TransferTab_ComboBox_Part.ForeColor = Color.Black;
                        TransferTab_Button_Search_Clicked(null, null);
                    }
                    else
                    {
                        MainForm_Last10_Button_08.Enabled = false;
                    }
                }
            }
            catch (MySqlException ex)
            {
                SearchDao.HandleException_SQLError_CloseApp(ex);
            }
            catch (Exception ex)
            {
                SearchDao.HandleException_GeneralError_CloseApp(ex);
            }
        }

        /// <summary>
        /// Handles the click event for the MainForm_Last10_Button_09 button.
        /// This method resets the tabs and updates the controls based on the ninth item in the "Last 10 Transactions" list.
        /// </summary>
        private void Primary_Button_Last10_09_Clicked(object sender, EventArgs e)
        {
            try
            {
                Helper_TabControl_ResetTab1();
                Helper_TabControl_ResetTab2();
                Helper_TabControl_ResetTab3();

                if (MainForm_TabControl.SelectedIndex == 0)
                {
                    if (WipAppVariables.list[8].PartId != "[ Blank ]")
                    {
                        MainForm_Last10_Button_09.Enabled = true;
                        InventoryTab_ComboBox_Part.Text = WipAppVariables.list[8].PartId;
                        InventoryTab_ComboBox_Op.Text = WipAppVariables.list[8].Op;
                        InventoryTab_TextBox_Qty.Text = WipAppVariables.list[8].Quantity.ToString();
                        InventoryTab_ComboBox_Part.ForeColor = Color.Black;
                        InventoryTab_ComboBox_Op.ForeColor = Color.Black;
                        InventoryTab_TextBox_Qty.ForeColor = Color.Black;
                        InventoryTab_ComboBox_Loc.Focus();
                    }
                    else
                    {
                        MainForm_Last10_Button_09.Enabled = false;
                    }
                }

                if (MainForm_TabControl.SelectedIndex == 1)
                {
                    if (WipAppVariables.list[8].PartId != "[ Blank ]")
                    {
                        MainForm_Last10_Button_09.Enabled = true;
                        RemoveTab_ComboBox_Part.Text = WipAppVariables.list[8].PartId;
                        RemoveTab_ComboBox_Op.Text = WipAppVariables.list[8].Op;
                        RemoveTab_ComboBox_Part.ForeColor = Color.Black;
                        RemoveTab_ComboBox_Op.ForeColor = Color.Black;
                        RemoveTab_Button_Search_Clicked(null, null);
                    }
                    else
                    {
                        MainForm_Last10_Button_09.Enabled = false;
                    }
                }

                if (MainForm_TabControl.SelectedIndex == 2)
                {
                    if (WipAppVariables.list[8].PartId != "[ Blank ]")
                    {
                        MainForm_Last10_Button_09.Enabled = true;
                        TransferTab_ComboBox_Part.Text = WipAppVariables.list[8].PartId;
                        TransferTab_ComboBox_Part.ForeColor = Color.Black;
                        TransferTab_Button_Search_Clicked(null, null);
                    }
                    else
                    {
                        MainForm_Last10_Button_09.Enabled = false;
                    }
                }
            }
            catch (MySqlException ex)
            {
                SearchDao.HandleException_SQLError_CloseApp(ex);
            }
            catch (Exception ex)
            {
                SearchDao.HandleException_GeneralError_CloseApp(ex);
            }
        }

        /// <summary>
        /// Handles the click event for the MainForm_Last10_Button_10 button.
        /// This method resets the tabs and updates the controls based on the tenth item in the "Last 10 Transactions" list.
        /// </summary>
        private void Primary_Button_Last10_10_Clicked(object sender, EventArgs e)
        {
            try
            {
                Helper_TabControl_ResetTab1();
                Helper_TabControl_ResetTab2();
                Helper_TabControl_ResetTab3();

                if (MainForm_TabControl.SelectedIndex == 0)
                {
                    if (WipAppVariables.list[9].PartId != "[ Blank ]")
                    {
                        MainForm_Last10_Button_10.Enabled = true;
                        InventoryTab_ComboBox_Part.Text = WipAppVariables.list[9].PartId;
                        InventoryTab_ComboBox_Op.Text = WipAppVariables.list[9].Op;
                        InventoryTab_TextBox_Qty.Text = WipAppVariables.list[9].Quantity.ToString();
                        InventoryTab_ComboBox_Part.ForeColor = Color.Black;
                        InventoryTab_ComboBox_Op.ForeColor = Color.Black;
                        InventoryTab_TextBox_Qty.ForeColor = Color.Black;
                        InventoryTab_ComboBox_Loc.Focus();
                    }
                    else
                    {
                        MainForm_Last10_Button_10.Enabled = false;
                    }
                }

                if (MainForm_TabControl.SelectedIndex == 1)
                {
                    if (WipAppVariables.list[9].PartId != "[ Blank ]")
                    {
                        MainForm_Last10_Button_10.Enabled = true;
                        RemoveTab_ComboBox_Part.Text = WipAppVariables.list[9].PartId;
                        RemoveTab_ComboBox_Op.Text = WipAppVariables.list[9].Op;
                        RemoveTab_ComboBox_Part.ForeColor = Color.Black;
                        RemoveTab_ComboBox_Op.ForeColor = Color.Black;
                        RemoveTab_Button_Search_Clicked(null, null);
                    }
                    else
                    {
                        MainForm_Last10_Button_10.Enabled = false;
                    }
                }

                if (MainForm_TabControl.SelectedIndex == 2)
                {
                    if (WipAppVariables.list[9].PartId != "[ Blank ]")
                    {
                        MainForm_Last10_Button_10.Enabled = true;
                        TransferTab_ComboBox_Part.Text = WipAppVariables.list[9].PartId;
                        TransferTab_ComboBox_Part.ForeColor = Color.Black;
                        TransferTab_Button_Search_Clicked(null, null);
                    }
                    else
                    {
                        MainForm_Last10_Button_10.Enabled = false;
                    }
                }
            }
            catch (MySqlException ex)
            {
                SearchDao.HandleException_SQLError_CloseApp(ex);
            }
            catch (Exception ex)
            {
                SearchDao.HandleException_GeneralError_CloseApp(ex);
            }
        }

        /// <summary>
        /// Handles the TextChanged event for the buttons in the MainForm_GroupBox_Last10.
        /// This method updates the enabled state of the buttons based on the "Last 10 Transactions" list.
        /// </summary>
        private void Primary_Button_Last10_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string y;
                var x = 9;
                foreach (var button in MainForm_GroupBox_Last10.Controls.OfType<Button>())
                {
                    y = WipAppVariables.list[x].PartId;
                    x--;
                    button.Enabled = y != "[ Blank ]";
                }
            }
            catch (MySqlException ex)
            {
                SearchDao.HandleException_SQLError_CloseApp(ex);
            }
            catch (Exception ex)
            {
                SearchDao.HandleException_GeneralError_CloseApp(ex);
            }
        }

        /// <summary>
        /// Handles the Tick event for the Last10Timer.
        /// This method refills the "Last 10 Transactions" list and restarts the timer.
        /// </summary>
        private void Primary_Timer_Last10_Tick(object sender, EventArgs e)
        {
            try
            {
                Last10FillAsync();
                Last10Timer.Start();
            }
            catch (MySqlException ex)
            {
                SearchDao.HandleException_SQLError_CloseApp(ex);
            }
            catch (Exception ex)
            {
                SearchDao.HandleException_GeneralError_CloseApp(ex);
            }
        }

        /// <summary>
        /// Checks for duplication in the "Last 10 Transactions" list.
        /// This method ensures that the current part and operation are not already in the list.
        /// If they are not, it updates the list.
        /// </summary>
        private void Primary_DuplicationCheck_Last10()
        {
            try
            {
                var x = 0;
                for (var i = 0; i < WipAppVariables.list.Count; i++)
                {
                    if (WipAppVariables.list[i].PartId == InventoryTab_ComboBox_Part.Text &&
                        WipAppVariables.list[i].Op == InventoryTab_ComboBox_Op.Text)
                    {
                        break;
                    }
                    else
                    {
                        x++;
                    }
                }

                if (x == 10)
                {
                    SearchDao.System_Last10_Buttons_Changed();
                }
            }
            catch (MySqlException ex)
            {
                SearchDao.HandleException_SQLError_CloseApp(ex);
            }
            catch (Exception ex)
            {
                SearchDao.HandleException_GeneralError_CloseApp(ex);
            }
        }

        /// <summary>
        /// Handles the click event for the MainForm_Button_ShowHideLast10 button.
        /// This method toggles the visibility of the MainForm_GroupBox_Last10 and adjusts the form's width accordingly.
        /// </summary>
        private void Primary_Button_ShowHideLast10_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (MainForm_GroupBox_Last10.Visible)
                {
                    MainForm_GroupBox_Last10.Visible = false;
                    Width -= 145;
                    MinimumSize = new Size(Width - 145, Height);
                    MaximumSize = new Size(Width - 145, Height);
                    MainForm_Button_ShowHideLast10.Text = @"Show Last 10";
                }
                else
                {
                    MainForm_GroupBox_Last10.Visible = true;
                    Width += 145;
                    MinimumSize = new Size(Width + 145, Height);
                    MaximumSize = new Size(Width + 145, Height);
                    MainForm_Button_ShowHideLast10.Text = @"Hide Last 10";
                }
            }
            catch (MySqlException ex)
            {
                SearchDao.HandleException_SQLError_CloseApp(ex);
            }
            catch (Exception ex)
            {
                SearchDao.HandleException_GeneralError_CloseApp(ex);
            }
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event for the RemoveTab_ComboBox_SearchByType control.
        /// This method updates the RemoveTab_DataGrid based on the selected search type and disables/enables relevant controls.
        /// </summary>
        private async void RemoveTab_ComboBox_SearchByType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (MainForm_TabControl.SelectedIndex == 1)
                {
                    if (RemoveTab_CBox_ShowAll.SelectedIndex == 0)
                    {
                        RemoveTab_ComboBox_Part.DataSource = _removeTabPartCbDataTable;
                        RemoveTab_ComboBox_Part.DisplayMember = "Item Number";
                        RemoveTab_ComboBox_Part.ValueMember = "ID";
                        RemoveTab_ComboBox_Part.SelectedIndex = 0;
                        RemoveTab_ComboBox_Part.ForeColor = Color.Red;
                        RemoveTab_ComboBox_Part.Select(0, 0);
                        RemoveTab_ComboBox_Part.Focus();
                    }
                    else if (RemoveTab_CBox_ShowAll.Text == @"Everything")
                    {
                        RemoveTab_ComboBox_Part.Enabled = false;
                        RemoveTab_ComboBox_Op.Enabled = false;
                        RemoveTab_Button_Search.Enabled = false;

                        var data = await SearchDao.RemoveTab_GetAllLocationsAsync();
                        _inventoryBindingSource.DataSource = data;
                        RemoveTab_DataGrid.DataSource = _inventoryBindingSource;
                        for (var i = 0; i < RemoveTab_DataGrid.Columns.Count; i++)
                        {
                            RemoveTab_DataGrid.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                        }

                        if (RemoveTab_DataGrid.Columns.Contains("ID"))
                        {
                            RemoveTab_DataGrid.Columns["ID"].Visible = false;
                        }

                        DgvDesigner.InitializeDataGridView(RemoveTab_DataGrid, null);
                    }
                    else
                    {
                        RemoveTab_ComboBox_Part.Enabled = false;
                        RemoveTab_ComboBox_Op.Enabled = false;
                        RemoveTab_Button_Search.Enabled = false;

                        var data = await SearchDao.RemoveTab_SearchByTypeAsync(RemoveTab_CBox_ShowAll.Text);
                        _inventoryBindingSource.DataSource = data;
                        RemoveTab_DataGrid.DataSource = _inventoryBindingSource;
                        for (var i = 0; i < RemoveTab_DataGrid.Columns.Count; i++)
                        {
                            RemoveTab_DataGrid.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                        }

                        if (RemoveTab_DataGrid.Columns.Contains("ID"))
                        {
                            RemoveTab_DataGrid.Columns["ID"].Visible = false;
                        }

                        DgvDesigner.InitializeDataGridView(RemoveTab_DataGrid, null);
                    }

                    var success = RemoveTab_DataGrid.RowCount > 0;
                    RemoveTab_Image_NothingFound.Visible = !success;
                }
            }
            catch (MySqlException ex)
            {
                SearchDao.HandleException_SQLError_CloseApp(ex);
            }
            catch (Exception ex)
            {
                SearchDao.HandleException_GeneralError_CloseApp(ex);
            }
        }

        /// <summary>
        /// Resets the "Last 10 Transactions" list in the database.
        /// This method truncates the table, resets the AUTO_INCREMENT value, and inserts blank entries.
        /// </summary>
        private static async Task Primary_Last10_ResetAsync()
        {
            MySqlConnection connection = null;
            MySqlCommand command = null;
            try
            {
                WipAppVariables.list.Clear();
                connection = new MySqlConnection(WipAppVariables.connectionString);
                await connection.OpenAsync();
                command = new MySqlCommand("TRUNCATE `last_10_transactions`;" +
                                           "ALTER TABLE `mtm database`.`last_10_transactions`" +
                                           "AUTO_INCREMENT = 100," +
                                           "CHANGE COLUMN `ID` `ID` INT(11) NOT NULL AUTO_INCREMENT;" +
                                           "INSERT INTO `last_10_transactions` (`PartID`, `Op`, `Quantity`) VALUES ('[ Blank ]', 'N/A', '0');" +
                                           "INSERT INTO `last_10_transactions` (`PartID`, `Op`, `Quantity`) VALUES ('[ Blank ]', 'N/A', '0');" +
                                           "INSERT INTO `last_10_transactions` (`PartID`, `Op`, `Quantity`) VALUES ('[ Blank ]', 'N/A', '0');" +
                                           "INSERT INTO `last_10_transactions` (`PartID`, `Op`, `Quantity`) VALUES ('[ Blank ]', 'N/A', '0');" +
                                           "INSERT INTO `last_10_transactions` (`PartID`, `Op`, `Quantity`) VALUES ('[ Blank ]', 'N/A', '0');" +
                                           "INSERT INTO `last_10_transactions` (`PartID`, `Op`, `Quantity`) VALUES ('[ Blank ]', 'N/A', '0');" +
                                           "INSERT INTO `last_10_transactions` (`PartID`, `Op`, `Quantity`) VALUES ('[ Blank ]', 'N/A', '0');" +
                                           "INSERT INTO `last_10_transactions` (`PartID`, `Op`, `Quantity`) VALUES ('[ Blank ]', 'N/A', '0');" +
                                           "INSERT INTO `last_10_transactions` (`PartID`, `Op`, `Quantity`) VALUES ('[ Blank ]', 'N/A', '0');" +
                                           "INSERT INTO `last_10_transactions` (`PartID`, `Op`, `Quantity`) VALUES ('[ Blank ]', 'N/A', '0');",
                    connection);
                await command.ExecuteNonQueryAsync();
                MessageBox.Show(@"Last 10 Reset", @"Success");
                Application.Restart();
            }
            catch (MySqlException ex)
            {
                SearchDao.HandleException_SQLError_CloseApp(ex);
            }
            catch (Exception ex)
            {
                SearchDao.HandleException_GeneralError_CloseApp(ex);
            }
            finally
            {
                command?.Dispose();
                connection?.Close();
                connection?.Dispose();
            }
        }

        /// <summary>
        /// Handles the click event for the "Reset Last 10" tool strip menu item.
        /// This method resets the "Last 10 Transactions" list if the user has admin permissions.
        /// </summary>
        private void ToolStrip_ResetLast10_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (WipAppVariables.userTypeAdmin)
                {
                    var dialogResult = MessageBox.Show(@"Are you sure you want to reset the Last 10 Buttons?",
                        @"Confirm Reset", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        Primary_Last10_ResetAsync();
                    }
                    else if (dialogResult == DialogResult.No)
                    {
                        MessageBox.Show(@"Reset Cancelled", @"Cancelled");
                    }
                }
                else
                {
                    MessageBox.Show(@"You do not have permission to reset the Last 10 Buttons", @"Permission Denied");
                }
            }
            catch (MySqlException ex)
            {
                SearchDao.HandleException_SQLError_CloseApp(ex);
            }
            catch (Exception ex)
            {
                SearchDao.HandleException_GeneralError_CloseApp(ex);
            }
        }

        /// <summary>
        /// Handles the SelectionChanged event for the TransferDataGrid.
        /// This method updates the TransferTab_TextBox_Qty with the quantity of the selected row.
        /// </summary>
        private void TransferTab_DataGrid_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                // Only proceed if Helper_TabControl_ResetTab3 is not being executed
                if (!_isResettingTab3 && TransferDataGrid.Rows.Count > 0)
                {
                    var selectedRow = TransferDataGrid.SelectedRows[0];
                    TransferTab_TextBox_Qty.Text = selectedRow.Cells["Quantity"].Value.ToString();
                }

                var success = TransferDataGrid.RowCount > 0;
                TransferTab_Image_Nothing.Visible = !success;
            }
            catch (MySqlException ex)
            {
                SearchDao.HandleException_SQLError_CloseApp(ex);
            }
            catch (Exception ex)
            {
                SearchDao.HandleException_GeneralError_CloseApp(ex);
            }
        }

        /// <summary>
        /// Handles the TextChanged event for the TransferTab_TextBox_Qty.
        /// This method validates the entered quantity and ensures it does not exceed the available quantity.
        /// </summary>
        private void TransferTab_TextBox_Qty_TextChanged(object sender, EventArgs e)
        {
            try
            {
                // Check if the TransferDataGrid has rows
                if (TransferDataGrid.Rows.Count == 0)
                {
                    TransferTab_TextBox_Qty.Clear();
                    return;
                }

                // Ensure there is a selected row
                if (TransferDataGrid.SelectedRows.Count == 0)
                {
                    TransferTab_TextBox_Qty.Clear();
                    return;
                }

                var selectedRow = TransferDataGrid.SelectedRows[0];

                // Ensure the "Quantity" cell is not null and contains a valid integer
                if (selectedRow.Cells["Quantity"].Value == null ||
                    !int.TryParse(selectedRow.Cells["Quantity"].Value.ToString(), out var availableQty))
                {
                    TransferTab_TextBox_Qty.Clear();
                    return;
                }

                // Check if the TextBox is empty
                if (string.IsNullOrEmpty(TransferTab_TextBox_Qty.Text))
                {
                    return;
                }

                // Validate the entered quantity
                if (int.TryParse(TransferTab_TextBox_Qty.Text, out var enteredQty) && enteredQty > availableQty)
                {
                    TransferTab_TextBox_Qty.Text = availableQty.ToString();
                }
            }
            catch (FormatException ex)
            {
                MessageBox.Show(
                    @"Invalid number format in TransferTab_TextBox_Qty_TextChanged\nException:\n" + ex.Message,
                    @"Error");
            }
            catch (InvalidCastException ex)
            {
                MessageBox.Show(@"Invalid cast in TransferTab_TextBox_Qty_TextChanged\nException:\n" + ex.Message,
                    @"Error");
            }
            catch (MySqlException ex)
            {
                SearchDao.HandleException_SQLError_CloseApp(ex);
            }
            catch (Exception ex)
            {
                SearchDao.HandleException_GeneralError_CloseApp(ex);
            }
        }


        /// <summary>
        /// Handles the KeyPress event for the TransferTab_TextBox_Qty.
        /// This method ensures that only numeric input and a single decimal point are allowed.
        /// </summary>
        private void TransferTab_TextBox_Qty_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
                {
                    e.Handled = true;
                }

                // only allow one decimal point
                if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
                {
                    e.Handled = true;
                }
            }
            catch (MySqlException ex)
            {
                SearchDao.HandleException_SQLError_CloseApp(ex);
            }
            catch (Exception ex)
            {
                SearchDao.HandleException_GeneralError_CloseApp(ex);
            }
        }

        private void ToolStrip_ViewChangeLog_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (IsHandleCreated && !IsDisposed)
                {
                    var change = new ChangeLogForm();
                    Enabled = false;
                    change.FormClosed +=
                        (_, _) => Enabled = true;
                    change.Show();
                }
            }
            catch (Exception ex)
            {
                SearchDao.HandleException_GeneralError_CloseApp(ex);
            }
        }

        /// <summary>
        /// Handles the click event for the RemoveTab_Button_Edit button.
        /// This method reads the entire document, creates an array of ChangeNotes objects based on the selected rows in the RemoveTab_DataGrid,
        /// and sets each field to the matching DataGridView column's selected index. If there are more than one selected index, it adds to the array.
        /// Then, it opens the EditNote window using the array it just created.
        /// </summary>
        private void RemoveTab_Button_Edit_Click(object sender, EventArgs e)
        {
            try
            {
                // Ensure there are selected rows
                if (RemoveTab_DataGrid.SelectedRows.Count == 0)
                {
                    MessageBox.Show(@"No rows selected.", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    RemoveTab_Button_Edit.Visible = false;
                    return;
                }

                // Create a list to hold ChangeNotes objects
                List<ChangeNotes> changeNotesList = [];

                // Iterate through each selected row
                foreach (DataGridViewRow row in RemoveTab_DataGrid.SelectedRows)
                {
                    // Create a new ChangeNotes object and set its fields based on the DataGridView columns
                    ChangeNotes changeNote = new()
                    {
                        Id = Convert.ToInt32(row.Cells["ID"].Value),
                        Location = row.Cells["Location"].Value?.ToString(),
                        PartId = row.Cells["PartID"].Value?.ToString(),
                        Operation = row.Cells["Op"].Value?.ToString(),
                        Note = row.Cells["Notes"].Value?.ToString()
                    };

                    // Add the ChangeNotes object to the list
                    changeNotesList.Add(changeNote);
                }

                // Convert the list to an array
                ChangeNotes[] changeNotesArray = changeNotesList?.ToArray();

                // Open the EditNote window using the array
                if (changeNotesArray != null)
                {
                    var editNoteWindow = new RemovalTabEditNote(changeNotesArray);
                    editNoteWindow.FormClosed += (_, _) => Helper_TabControl_ResetTab2();
                    editNoteWindow.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                SearchDao.HandleException_GeneralError_CloseApp(ex);
            }
        }

        private void MainToolStrip_New_Object_Click(object sender, EventArgs e)
        {
            try
            {
                var unifiedEntryForm = new UnifiedEntryForm();
                Enabled = false;
                unifiedEntryForm.FormClosed += (_, _) => Enabled = true;
                unifiedEntryForm.Show();
            }
            catch (MySqlException ex)
            {
                SearchDao.HandleException_SQLError_CloseApp(ex);
            }
            catch (Exception ex)
            {
                SearchDao.HandleException_GeneralError_CloseApp(ex);
            }
        }

        private async void CallUnifiedRemovalForm_Click(object sender, EventArgs e)
        {
            try
            {
                var change = new UnifiedRemovalForm();
                Enabled = false;
                change.FormClosed += async (_, _) =>
                {
                    Enabled = true;
                    await ClearAndResetAllComboBoxesAsync();
                };
                change.Show();
            }
            catch (MySqlException ex)
            {
                SearchDao.HandleException_SQLError_CloseApp(ex);
            }
            catch (Exception ex)
            {
                SearchDao.HandleException_GeneralError_CloseApp(ex);
            }
        }

        private void MainForm_MenuStrip_Exit_Click(object sender, EventArgs e)
        {
            try
            {
                // Log the exit action
                AppLogger.Log("Application is closing via MainForm_MenuStrip_Exit_Click.");

                // Close the application
                Application.Exit();
            }
            catch (Exception ex)
            {
                // Handle any unexpected errors
                MessageBox.Show($@"An error occurred while closing the application: {ex.Message}", @"Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                AppLogger.Log($"Error while closing the application: {ex.Message}");
            }
        }
    }
}