using MTM_WIP_App.Main_Form;
using MySql.Data.MySqlClient;

namespace MTM_WIP_App.Database_Maint.Remove_Objects.Unified_Removal_Form.Classes
{
    public static class UnifiedRemovalForm_RemovalDatabaseOperations
    {
        public static void DeleteEntries(string objectType, List<int> ids, string connectionString)
        {
            try
            {
                using var connection = new MySqlConnection(connectionString);
                connection.Open();

                var tableName = objectType switch
                {
                    "User" => "users",
                    "Part" => "part_ids",
                    "Part Type" => "item_types",
                    "Operation" => "operation_numbers",
                    "Location" => "locations",
                    _ => throw new ArgumentException("Invalid object type.")
                };

                var idList = string.Join(",", ids);
                var query = $"DELETE FROM `{tableName}` WHERE `ID` IN ({idList})";

                using var command = new MySqlCommand(query, connection);
                command.ExecuteNonQuery();

                AppLogger.Log($"Deleted {ids.Count} entries from {tableName}.");
            }
            catch (Exception ex)
            {
                AppLogger.Log($"Error in DeleteEntries: {ex.Message}");
                throw;
            }
        }
    }
}