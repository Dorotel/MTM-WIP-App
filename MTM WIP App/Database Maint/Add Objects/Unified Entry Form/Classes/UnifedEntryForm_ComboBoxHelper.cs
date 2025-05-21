using MySql.Data.MySqlClient;
using System.Data;
using MTM_WIP_App.Main_Form;

namespace MTM_WIP_App.Database_Maint.Add_Objects.Unified_Entry_Form.Classes
{
    public static class UnifedEntryForm_ComboBoxHelper
    {
        public static void InitializePartTypeComboBox(ComboBox partTypeComboBox, string connectionString)
        {
            try
            {
                using MySqlConnection connection = new(connectionString);
                MySqlDataAdapter adapter = new();
                DataTable dataTable = new();

                MySqlCommand command = new("SELECT * FROM item_types", connection);
                adapter.SelectCommand = command;
                adapter.Fill(dataTable);

                // Remove any rows where Type is "Everything" (case-insensitive)
                for (var i = dataTable.Rows.Count - 1; i >= 0; i--)
                {
                    var typeValue = dataTable.Rows[i]["Type"]?.ToString();
                    if (string.Equals(typeValue, "Everything", StringComparison.OrdinalIgnoreCase))
                    {
                        dataTable.Rows.RemoveAt(i);
                    }
                }

                var defaultRow = dataTable.NewRow();
                defaultRow[0] = "[ Select Part Type ]";
                dataTable.Rows.InsertAt(defaultRow, 0);

                partTypeComboBox.DataSource = dataTable;
                partTypeComboBox.DisplayMember = "Type";
                partTypeComboBox.ValueMember = "ID";
                partTypeComboBox.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                AppLogger.Log($"Error in InitializePartTypeComboBox: {ex.Message}");
                throw;
            }
        }

        public static void InitializeShiftComboBox(ComboBox shiftComboBox)
        {
            try
            {
                shiftComboBox.Items.Clear();
                shiftComboBox.Items.Add("[ Select Shift ]");
                shiftComboBox.Items.Add("Shift 1");
                shiftComboBox.Items.Add("Shift 2");
                shiftComboBox.Items.Add("Shift 3");
                shiftComboBox.Items.Add("Weekend");
                shiftComboBox.Items.Add("Swing Shift");
                shiftComboBox.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                AppLogger.Log($"Error in InitializeShiftComboBox: {ex.Message}");
                throw;
            }
        }
    }
}