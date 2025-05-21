using System.Data;
using MTM_WIP_App.Main_Form;
using MySql.Data.MySqlClient;

namespace MTM_WIP_App.Database_Maint.Remove_Objects.Unified_Removal_Form.Classes
{
    public static class UnifiedRemovalForm_RemovalHelper
    {
        public static DataTable GetDataForObjectType(string objectType, string connectionString)
        {
            try
            {
                using var connection = new MySqlConnection(connectionString);
                connection.Open();

                var query = objectType switch
                {
                    "User" => @"
                    SELECT `ID`, `User`, `Full Name`, `Shift`
                    FROM `users`
                    WHERE `User` != @CurrentUser
                    ORDER BY `ID` ASC
                    LIMIT 1, 18446744073709551615", // Skip the first row
                    "Part" => "SELECT `ID`, `Item Number`, `Type` FROM `part_ids`",
                    "Part Type" => "SELECT `ID`, `Type` FROM `item_types`",
                    "Operation" => "SELECT `ID`, `Operation` FROM `operation_numbers`",
                    "Location" => "SELECT `ID`, `Location` FROM `locations`",
                    _ => throw new ArgumentException("Invalid object type.")
                };

                var adapter = new MySqlDataAdapter(query, connection);

                if (objectType == "User")
                {
                    adapter.SelectCommand.Parameters.AddWithValue("@CurrentUser", WipAppVariables.User);
                }

                var dataTable = new DataTable();
                adapter.Fill(dataTable);

                return dataTable;
            }
            catch (Exception ex)
            {
                AppLogger.Log($"Error in GetDataForObjectType: {ex.Message}");
                throw;
            }
        }

        public static List<int> GetSelectedIds(DataGridView dataGridView)
        {
            return dataGridView.SelectedRows
                .Cast<DataGridViewRow>()
                .Select(row => Convert.ToInt32(row.Cells["ID"].Value))
                .ToList();
        }

        public static string GetUserEmailById(int userId, string connectionString)
        {
            try
            {
                using var connection = new MySqlConnection(connectionString);
                connection.Open();

                var query = "SELECT `User` FROM `users` WHERE `ID` = @UserId";
                using var command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@UserId", userId);

                return command.ExecuteScalar()?.ToString() ?? string.Empty;
            }
            catch (Exception ex)
            {
                AppLogger.Log($"Error in GetUserEmailById: {ex.Message}");
                throw;
            }
        }

        public static void RemoveUserFromLeads(string email, string connectionString)
        {
            try
            {
                using var connection = new MySqlConnection(connectionString);
                connection.Open();

                var query = "DELETE FROM `leads` WHERE `User` = @Email";
                using var command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@Email", email);
                command.ExecuteNonQuery();

                AppLogger.Log($"Removed user '{email}' from leads.");
            }
            catch (Exception ex)
            {
                AppLogger.Log($"Error in RemoveUserFromLeads: {ex.Message}");
                throw;
            }
        }

        public static void RemoveUserFromReadOnly(string email, string connectionString)
        {
            try
            {
                using var connection = new MySqlConnection(connectionString);
                connection.Open();

                var query = "DELETE FROM `readonly` WHERE `User` = @Email";
                using var command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@Email", email);
                command.ExecuteNonQuery();

                AppLogger.Log($"Removed user '{email}' from readonly.");
            }
            catch (Exception ex)
            {
                AppLogger.Log($"Error in RemoveUserFromReadOnly: {ex.Message}");
                throw;
            }
        }

        public static void RemoveUserFromMySqlServer(string email, string connectionString)
        {
            try
            {
                using var connection = new MySqlConnection(connectionString);
                connection.Open();

                var query = $"DROP USER IF EXISTS '{email}'@'%'";
                using var command = new MySqlCommand(query, connection);
                command.ExecuteNonQuery();

                AppLogger.Log($"Removed user '{email}' from MySQL server.");
            }
            catch (Exception ex)
            {
                AppLogger.Log($"Error in RemoveUserFromMySQLServer: {ex.Message}");
                throw;
            }
        }
    }
}