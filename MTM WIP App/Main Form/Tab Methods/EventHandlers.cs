using MySql.Data.MySqlClient;
using System.Data;

namespace MTM_WIP_App.Main_Form.Tab_Methods
{
    /// <summary>
    /// This class contains event handlers and validation methods for the controls on the Inventory and Removal tabs of the main form.
    /// It includes methods to handle key events, validate control inputs, and enable or disable buttons based on validation results.
    /// The class ensures that user inputs are validated and provides feedback by changing control appearances and enabling or disabling actions.
    /// It handles various controls such as ComboBoxes, TextBoxes, Buttons, and ToolStripMenuItems, and interacts with DataTables to validate selections.
    /// </summary>
    public static class EventHandlers
    {
        /// <summary>
        /// Handles the KeyUp event for a control. If the Enter or Return key is pressed, it moves the focus to the next control.
        /// </summary>
        public static async void Control_KeyUp(object? sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                {
                    var control = sender as Control;
                    if (control != null)
                    {
                        var form = control.FindForm();
                        if (form != null)
                        {
                            form.SelectNextControl(control, true, true, true, true);
                            e.Handled = e.SuppressKeyPress = true;
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                AppLogger.LogDatabaseError(ex);
                // Try to capture the control name from the first relevant control
                var controlName = sender.ToString()
                                  ?? "UnknownControl";
                await ErrorLogDao.HandleException_SQLError_CloseApp(
                    ex, true, nameof(Validate_MethodCaller_InventoryTab), controlName);
            }
            catch (Exception ex)
            {
                AppLogger.LogDatabaseError(ex);
                var controlName = sender.ToString()
                                  ?? "UnknownControl";
                await ErrorLogDao.HandleException_GeneralError_CloseApp(
                    ex, true, nameof(Validate_MethodCaller_InventoryTab), controlName);
            }
        }

        /// <summary>
        /// Validates the controls on the Inventory tab and enables or disables the save button and menu item based on the validation results.
        /// </summary>
        public static async void Validate_MethodCaller_InventoryTab(ComboBox partComboBox, ComboBox opComboBox,
            ComboBox locComboBox, TextBox qtyTextBox, TextBox howTextBox, Button saveButton,
            ToolStripMenuItem saveMenuItem,
            DataTable partDataTable, DataTable opDataTable, DataTable locDataTable)
        {
            try
            {
                AppLogger.Log("Validating Inventory Tab controls.");
                ValidateTabs_InventoryTab_ComboBoxes(partComboBox, partDataTable, opDataTable, locDataTable);
                ValidateTabs_InventoryTab_ComboBoxes(opComboBox, partDataTable, opDataTable, locDataTable);
                ValidateTabs_InventoryTab_ComboBoxes(locComboBox, partDataTable, opDataTable, locDataTable);
                ValidateTabs_InventoryTab_Quantity_TextBox(qtyTextBox);
                ValidateTabs_InventoryTab_HowMany_TextBox(howTextBox);
                ValidateTabs_InventoryTab_ButtonEnabler(partComboBox, opComboBox, locComboBox, qtyTextBox, saveButton,
                    saveMenuItem);

                ValidateTabs_Finisher(partComboBox);
            }
            catch (MySqlException ex)
            {
                AppLogger.LogDatabaseError(ex);
                // Try to capture the control name from the first relevant control
                var controlName = partComboBox?.Name
                                  ?? opComboBox?.Name
                                  ?? locComboBox?.Name
                                  ?? qtyTextBox?.Name
                                  ?? howTextBox?.Name
                                  ?? "UnknownControl";
                await ErrorLogDao.HandleException_SQLError_CloseApp(
                    ex, true, nameof(Validate_MethodCaller_InventoryTab), controlName);
            }
            catch (Exception ex)
            {
                AppLogger.LogDatabaseError(ex);
                var controlName = partComboBox?.Name
                                  ?? opComboBox?.Name
                                  ?? locComboBox?.Name
                                  ?? qtyTextBox?.Name
                                  ?? howTextBox?.Name
                                  ?? "UnknownControl";
                await ErrorLogDao.HandleException_GeneralError_CloseApp(
                    ex, true, nameof(Validate_MethodCaller_InventoryTab), controlName);
            }
        }

        /// <summary>
        /// Validates the controls on the Removal tab and enables or disables the save button and menu item based on the validation results.
        /// </summary>
        public static async void Validate_MethodCaller_RemovalTab(ComboBox partComboBox, ComboBox opComboBox,
            ComboBox showAllComboBox, Button saveButton, ToolStripMenuItem saveMenuItem,
            DataTable partDataTable, DataTable opDataTable)
        {
            try
            {
                AppLogger.Log("Validating Removal Tab controls.");
                ValidateTabs_RemovalTab_ComboBoxes(partComboBox, partDataTable, opDataTable);
                ValidateTabs_RemovalTab_ComboBoxes(opComboBox, partDataTable, opDataTable);
                ValidateTabs_RemovalTab_ComboBoxes(showAllComboBox, partDataTable, opDataTable);

                ValidateTabs_RemovalTab_ButtonEnabler(partComboBox, opComboBox, showAllComboBox, saveButton,
                    saveMenuItem);

                ValidateTabs_Finisher(partComboBox);
            }
            catch (MySqlException ex)
            {
                AppLogger.LogDatabaseError(ex);
                // Try to capture the control name from the first relevant control
                var controlName = partComboBox?.Name
                                  ?? opComboBox?.Name
                                  ?? showAllComboBox?.Name
                                  ?? saveButton?.Name
                                  ?? saveMenuItem?.Name
                                  ?? partDataTable?.TableName
                                  ?? opDataTable?.TableName
                                  ?? "UnknownControl";
                await ErrorLogDao.HandleException_SQLError_CloseApp(
                    ex, true, nameof(Validate_MethodCaller_InventoryTab), controlName);
            }
            catch (Exception ex)
            {
                AppLogger.LogDatabaseError(ex);
                var controlName = partComboBox?.Name
                                  ?? opComboBox?.Name
                                  ?? showAllComboBox?.Name
                                  ?? saveButton?.Name
                                  ?? saveMenuItem?.Name
                                  ?? partDataTable?.TableName
                                  ?? opDataTable?.TableName
                                  ?? "UnknownControl";
                await ErrorLogDao.HandleException_GeneralError_CloseApp(
                    ex, true, nameof(Validate_MethodCaller_InventoryTab), controlName);
            }
        }

        /// <summary>
        /// Finalizes the validation process by selecting all text in the active TextBox control on the form containing the specified ComboBox.
        /// </summary>
        private static void ValidateTabs_Finisher(ComboBox partComboBox)
        {
            var form = partComboBox.FindForm();
            if (form != null)
            {
                var activeControl = form.ActiveControl;
                if (activeControl is TextBoxBase textBoxBase)
                {
                    textBoxBase.SelectAll();
                }
            }
        }

        /// <summary>
        /// Validates the selected item in the specified ComboBox based on the provided DataTables and updates the ComboBox's appearance accordingly.
        /// </summary>
        private static void ValidateTabs_InventoryTab_ComboBoxes(ComboBox comboBox, DataTable partDataTable,
            DataTable opDataTable, DataTable locDataTable)
        {
            DataTable dataTable = null;
            if (comboBox.Name.Contains("Part"))
            {
                dataTable = partDataTable;
            }
            else if (comboBox.Name.Contains("Op"))
            {
                dataTable = opDataTable;
            }
            else if (comboBox.Name.Contains("Loc"))
            {
                dataTable = locDataTable;
            }

            if (comboBox.SelectedIndex < 1 && dataTable != null)
            {
                var current = 0;
                foreach (DataRow row in dataTable.Rows)
                {
                    current++;
                    var value = row.Field<string>(comboBox.DisplayMember);
                    if (value == comboBox.Text && value != "[ Enter Part ID ]" && value != "[ Enter Op # ]" &&
                        value != "[ Enter Location ]")
                    {
                        comboBox.SelectedIndex = dataTable.Rows[current].Field<int>("ID") - 1;
                        comboBox.ForeColor = Color.Black;
                        break;
                    }

                    if (current >= dataTable.Rows.Count)
                    {
                        comboBox.ForeColor = Color.Red;
                        comboBox.SelectedIndex = 0;
                    }
                }
            }
            else
            {
                comboBox.ForeColor = Color.Black;
            }
        }

        /// <summary>
        /// Enables or disables the save button and menu item based on the validation of the selected items in the ComboBoxes and the quantity in the TextBox.
        /// </summary>
        private static void ValidateTabs_InventoryTab_ButtonEnabler(ComboBox partComboBox, ComboBox opComboBox,
            ComboBox locComboBox, TextBox qtyTextBox, Button saveButton, ToolStripMenuItem saveMenuItem)
        {
            var isPartSelected = partComboBox.SelectedIndex >= 1;
            var isOpSelected = opComboBox.SelectedIndex >= 1;
            var isQtyValid = int.TryParse(qtyTextBox.Text, out var qty) && qty > 0;
            var isLocSelected = locComboBox.SelectedIndex >= 1;

            if (isPartSelected && isOpSelected && isLocSelected && isQtyValid)
            {
                saveButton.Enabled = true;
                saveMenuItem.Enabled = true;
            }
            else
            {
                saveButton.Enabled = false;
                saveMenuItem.Enabled = false;
            }
        }

        /// <summary>
        /// Validates the quantity entered in the TextBox. If the quantity is not a valid integer, it sets the TextBox's text to a default error message and changes its text color to red.
        /// </summary>
        private static void ValidateTabs_InventoryTab_Quantity_TextBox(TextBox textBox)
        {
            if (int.TryParse(textBox.Text, out _))
            {
                textBox.ForeColor = Color.Black;
            }
            else
            {
                textBox.ForeColor = Color.Red;
                textBox.Text = @"[ Enter Valid Quantity ]";
            }
        }

        /// <summary>
        /// Validates the "How Many" quantity entered in the TextBox. If the quantity is not a valid integer or exceeds 10, it clears the TextBox, sets its text color to red, and focuses on it.
        /// </summary>
        private static void ValidateTabs_InventoryTab_HowMany_TextBox(TextBox textBox)
        {
            if (!string.IsNullOrEmpty(textBox.Text))
            {
                if (!int.TryParse(textBox.Text, out var converter))
                {
                    textBox.ForeColor = Color.Red;
                    textBox.Clear();
                    textBox.Focus();
                }
                else if (converter > 10)
                {
                    textBox.ForeColor = Color.Red;
                    textBox.Clear();
                    textBox.Focus();
                }
                else
                {
                    textBox.ForeColor = Color.Black;
                }
            }
            else
            {
                textBox.ForeColor = Color.Red;
                textBox.Clear();
                textBox.Focus();
            }
        }

        /// <summary>
        /// Validates the selected item in the specified ComboBox based on the provided DataTables and updates the ComboBox's appearance accordingly.
        /// </summary>
        private static void ValidateTabs_RemovalTab_ComboBoxes(ComboBox comboBox, DataTable partDataTable,
            DataTable opDataTable)
        {
            DataTable dataTable = null;
            if (comboBox.Name.Contains("Part"))
            {
                dataTable = partDataTable;
            }
            else if (comboBox.Name.Contains("Op"))
            {
                dataTable = opDataTable;
            }

            if (comboBox.SelectedIndex < 1 && dataTable != null)
            {
                var current = 0;
                foreach (DataRow row in dataTable.Rows)
                {
                    current++;
                    var value = row.Field<string>(comboBox.DisplayMember);
                    if (value == comboBox.Text && value != "[ Enter Part ID ]" && value != "[ Enter Op # ]")
                    {
                        comboBox.SelectedIndex = dataTable.Rows[current].Field<int>("ID") - 1;
                        comboBox.ForeColor = Color.Black;
                        break;
                    }

                    if (current >= dataTable.Rows.Count)
                    {
                        comboBox.ForeColor = Color.Red;
                        comboBox.SelectedIndex = 0;
                    }
                }
            }
            else
            {
                comboBox.ForeColor = Color.Black;
            }
        }

        /// <summary>
        /// Enables or disables the save button and menu item based on the validation of the selected items in the ComboBoxes on the Removal tab.
        /// </summary>
        private static void ValidateTabs_RemovalTab_ButtonEnabler(ComboBox partComboBox, ComboBox opComboBox,
            ComboBox showAllComboBox, Button saveButton, ToolStripMenuItem saveMenuItem)
        {
            var isPartSelected = partComboBox.SelectedIndex >= 1;

            if (isPartSelected)
            {
                saveButton.Enabled = true;
                saveMenuItem.Enabled = true;
            }
            else
            {
                saveButton.Enabled = false;
                saveMenuItem.Enabled = false;
            }
        }

        /// <summary>
        /// Validates the controls on the Transfer tab and enables or disables the search button, new location combo box, quantity text box, and save button based on the validation results.
        /// </summary>
        public static async void Validate_MethodCaller_TransferTab(ComboBox partComboBox, ComboBox newLocComboBox,
            TextBox qtyTextBox, Button searchButton, Button saveButton, ToolStripMenuItem saveMenuItem,
            DataTable partDataTable, DataTable locDataTable, DataGridView dataGrid)
        {
            try
            {
                AppLogger.Log("Validating Transfer Tab controls.");
                ValidateTabs_TransferTab_ComboBoxes(partComboBox, partDataTable, locDataTable);
                ValidateTabs_TransferTab_ComboBoxes(newLocComboBox, partDataTable, locDataTable);
                ValidateTabs_TransferTab_Quantity_TextBox(qtyTextBox);
                ValidateTabs_TransferTab_ButtonEnabler(partComboBox, newLocComboBox, qtyTextBox, searchButton,
                    saveButton,
                    saveMenuItem, dataGrid);

                ValidateTabs_Finisher(partComboBox);

                if (partComboBox.SelectedIndex >= 1 && newLocComboBox.SelectedIndex < 1 && searchButton.Enabled == true)
                {
                    searchButton.Focus();
                }
            }
            catch (MySqlException ex)
            {
                AppLogger.LogDatabaseError(ex);
                // Try to capture the control name from the first relevant control
                var controlName = partComboBox?.Name
                                  ?? qtyTextBox?.Name
                                  ?? newLocComboBox?.Name
                                  ?? searchButton?.Name
                                  ?? saveButton?.Name
                                  ?? saveMenuItem?.Name
                                  ?? dataGrid?.Name
                                  ?? locDataTable.TableName
                                  ?? "UnknownControl";
                await ErrorLogDao.HandleException_SQLError_CloseApp(
                    ex, true, nameof(Validate_MethodCaller_InventoryTab), controlName);
            }
            catch (Exception ex)
            {
                AppLogger.LogDatabaseError(ex);
                var controlName = partComboBox?.Name
                                  ?? qtyTextBox?.Name
                                  ?? newLocComboBox?.Name
                                  ?? searchButton?.Name
                                  ?? saveButton?.Name
                                  ?? saveMenuItem?.Name
                                  ?? dataGrid?.Name
                                  ?? locDataTable.TableName
                                  ?? "UnknownControl";
                await ErrorLogDao.HandleException_GeneralError_CloseApp(
                    ex, true, nameof(Validate_MethodCaller_InventoryTab), controlName);
            }
        }

        /// <summary>
        /// Validates the selected item in the specified ComboBox based on the provided DataTable and updates the ComboBox's appearance accordingly.
        /// </summary>
        private static void ValidateTabs_TransferTab_ComboBoxes(ComboBox comboBox, DataTable partDataTable,
            DataTable opDataTable)
        {
            DataTable dataTable = null;
            if (comboBox.Name.Contains("Part"))
            {
                dataTable = partDataTable;
            }
            else if (comboBox.Name.Contains("Location"))
            {
                dataTable = opDataTable;
            }

            if (comboBox.SelectedIndex < 1 && dataTable != null)
            {
                var current = 0;
                foreach (DataRow row in dataTable.Rows)
                {
                    current++;
                    var value = row.Field<string>(comboBox.DisplayMember);
                    if (value == comboBox.Text && value != "[ Enter Part ID ]" && value != "[ Enter New Location ]")
                    {
                        comboBox.SelectedIndex = dataTable.Rows[current].Field<int>("ID") - 1;
                        comboBox.ForeColor = Color.Black;
                        break;
                    }

                    if (current >= dataTable.Rows.Count)
                    {
                        comboBox.ForeColor = Color.Red;
                        comboBox.SelectedIndex = 0;
                    }
                }
            }
            else
            {
                comboBox.ForeColor = Color.Black;
            }
        }

        /// <summary>
        /// Validates the quantity entered in the TextBox. If the quantity is not a valid integer, it sets the TextBox's text to a default error message and changes its text color to red.
        /// </summary>
        private static void ValidateTabs_TransferTab_Quantity_TextBox(TextBox textBox)
        {
            if (int.TryParse(textBox.Text, out _))
            {
                textBox.ForeColor = Color.Black;
            }
            else
            {
                textBox.ForeColor = Color.Red;
            }
        }

        /// <summary>
        /// Enables or disables the search button, new location combo box, quantity text box, and save button based on the validation of the selected items in the ComboBoxes and the quantity in the TextBox.
        /// </summary>
        private static void ValidateTabs_TransferTab_ButtonEnabler(ComboBox partComboBox, ComboBox newLocComboBox,
            TextBox qtyTextBox, Button searchButton, Button saveButton, ToolStripMenuItem saveMenuItem,
            DataGridView dataGrid)
        {
            var isPartSelected = partComboBox.SelectedIndex >= 1;
            var isNewLocSelected = newLocComboBox.SelectedIndex >= 1;
            var isQtyValid = int.TryParse(qtyTextBox.Text, out var qty) && qty > 0;

            if (partComboBox.SelectedIndex >= 1 && dataGrid.RowCount == 0)
            {
                searchButton.Enabled = true;
                searchButton.Focus();
            }
            else
            {
                searchButton.Enabled = false;
            }

            qtyTextBox.Enabled = isPartSelected;
            saveButton.Enabled = isPartSelected;

            if (isPartSelected && isNewLocSelected && isQtyValid)
            {
                saveButton.Enabled = true;
                saveMenuItem.Enabled = true;
            }
            else
            {
                saveButton.Enabled = false;
                saveMenuItem.Enabled = false;
            }

            if (!isPartSelected)
            {
                qtyTextBox.Enabled = false;
            }
        }
    }
}