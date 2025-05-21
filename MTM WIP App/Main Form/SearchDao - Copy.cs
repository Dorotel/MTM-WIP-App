using MySql.Data.MySqlClient;
using System.Data;
using System.Diagnostics;
using System.Security.Principal;
using static MTM_WIP_App.SqlVariables;
using System.Reflection;
using MTM_WIP_App.Main_Form;

namespace MTM_WIP_App.Main_Form // Fully Migrated
{
    internal static class SqlHelper
    {
        public static async Task<int> ExecuteNonQuery(
            string commandText,
            Dictionary<string, object>? parameters = null,
            int commandTimeout = 30,
            bool useAsync = false)
        {
            if (useAsync)
            {
                await using var connection = new MySqlConnection(GetConnectionString(null, null, null, null));
                await using var command = new MySqlCommand(commandText, connection)
                {
                    CommandTimeout = commandTimeout
                };
                if (parameters != null)
                {
                    foreach (var param in parameters)
                    {
                        command.Parameters.AddWithValue(param.Key, param.Value ?? DBNull.Value);
                    }
                }

                await connection.OpenAsync();
                return await command.ExecuteNonQueryAsync();
            }
            else
            {
                using var connection = new MySqlConnection(GetConnectionString(null, null, null, null));
                using var command = new MySqlCommand(commandText, connection)
                {
                    CommandTimeout = commandTimeout
                };
                if (parameters != null)
                {
                    foreach (var param in parameters)
                    {
                        command.Parameters.AddWithValue(param.Key, param.Value ?? DBNull.Value);
                    }
                }

                connection.Open();
                return command.ExecuteNonQuery();
            }
        }

        public static async Task<object?> ExecuteScalar(
            string commandText,
            Dictionary<string, object>? parameters = null,
            int commandTimeout = 30,
            bool useAsync = false)
        {
            if (useAsync)
            {
                await using var connection = new MySqlConnection(GetConnectionString(null, null, null, null));
                await using var command = new MySqlCommand(commandText, connection)
                {
                    CommandTimeout = commandTimeout
                };
                if (parameters != null)
                {
                    foreach (var param in parameters)
                    {
                        command.Parameters.AddWithValue(param.Key, param.Value ?? DBNull.Value);
                    }
                }

                await connection.OpenAsync();
                return await command.ExecuteScalarAsync();
            }
            else
            {
                using var connection = new MySqlConnection(GetConnectionString(null, null, null, null));
                using var command = new MySqlCommand(commandText, connection)
                {
                    CommandTimeout = commandTimeout
                };
                if (parameters != null)
                {
                    foreach (var param in parameters)
                    {
                        command.Parameters.AddWithValue(param.Key, param.Value ?? DBNull.Value);
                    }
                }

                connection.Open();
                return command.ExecuteScalar();
            }
        }

        public static async Task<DataTable> ExecuteDataTable(
            string commandText,
            Dictionary<string, object>? parameters = null,
            int commandTimeout = 30,
            bool useAsync = false)
        {
            if (useAsync)
            {
                await using var connection = new MySqlConnection(GetConnectionString(null, null, null, null));
                await using var command = new MySqlCommand(commandText, connection)
                {
                    CommandTimeout = commandTimeout
                };
                if (parameters != null)
                {
                    foreach (var param in parameters)
                    {
                        command.Parameters.AddWithValue(param.Key, param.Value ?? DBNull.Value);
                    }
                }

                await connection.OpenAsync();
                using var reader = await command.ExecuteReaderAsync();
                var table = new DataTable();
                table.Load(reader);
                return table;
            }
            else
            {
                using var connection = new MySqlConnection(GetConnectionString(null, null, null, null));
                using var command = new MySqlCommand(commandText, connection)
                {
                    CommandTimeout = commandTimeout
                };
                if (parameters != null)
                {
                    foreach (var param in parameters)
                    {
                        command.Parameters.AddWithValue(param.Key, param.Value ?? DBNull.Value);
                    }
                }

                using var adapter = new MySqlDataAdapter(command);
                var table = new DataTable();
                adapter.Fill(table);
                return table;
            }
        }

        public static async Task<MySqlDataReader> ExecuteReader(
            string commandText,
            Dictionary<string, object>? parameters = null,
            int commandTimeout = 30,
            bool useAsync = false)
        {
            if (useAsync)
            {
                var connection = new MySqlConnection(GetConnectionString(null, null, null, null));
                var command = new MySqlCommand(commandText, connection)
                {
                    CommandTimeout = commandTimeout
                };
                if (parameters != null)
                {
                    foreach (var param in parameters)
                    {
                        command.Parameters.AddWithValue(param.Key, param.Value ?? DBNull.Value);
                    }
                }

                await connection.OpenAsync();
                return await command.ExecuteReaderAsync(CommandBehavior.CloseConnection);
            }
            else
            {
                var connection = new MySqlConnection(GetConnectionString(null, null, null, null));
                var command = new MySqlCommand(commandText, connection)
                {
                    CommandTimeout = commandTimeout
                };
                if (parameters != null)
                {
                    foreach (var param in parameters)
                    {
                        command.Parameters.AddWithValue(param.Key, param.Value ?? DBNull.Value);
                    }
                }

                connection.Open();
                return command.ExecuteReader(CommandBehavior.CloseConnection);
            }
        }
    }

    internal static class UserDao
    {
        internal static async Task<DataTable> GetVitsUsers(bool useAsync = false)
        {
            try
            {
                return await SqlHelper.ExecuteDataTable(
                    "SELECT `User` FROM `users` WHERE `VitsUser` = TRUE ORDER BY `User` ASC",
                    useAsync: useAsync);
            }
            catch (MySqlException ex)
            {
                AppLogger.LogDatabaseError(ex);
                await ErrorLogDao.HandleException_SQLError_CloseApp(ex);
                return new DataTable();
            }
            catch (Exception ex)
            {
                AppLogger.LogDatabaseError(ex);
                await ErrorLogDao.HandleException_GeneralError_CloseApp(ex);
                return new DataTable();
            }
        }

        internal static async Task<bool> ValidateUserPin(string username, string pin, bool useAsync = false)
        {
            try
            {
                var parameters = new Dictionary<string, object>
                {
                    ["@username"] = username,
                    ["@pin"] = pin
                };
                var result = await SqlHelper.ExecuteScalar(
                    "SELECT COUNT(*) FROM `users` WHERE `User` = @username AND `PIN` = @pin",
                    parameters,
                    useAsync: useAsync);
                return Convert.ToInt32(result) > 0;
            }
            catch (MySqlException ex)
            {
                AppLogger.LogDatabaseError(ex);
                ErrorLogDao.LogErrorWithMethod(ex);
                await ErrorLogDao.HandleException_SQLError_CloseApp(ex, useAsync);
                return false;
            }
            catch (Exception ex)
            {
                AppLogger.LogDatabaseError(ex);
                ErrorLogDao.LogErrorWithMethod(ex);
                await ErrorLogDao.HandleException_GeneralError_CloseApp(ex, useAsync);
                return false;
            }
        }

        internal static async Task<bool> UserExists(string email, bool useAsync = false)
        {
            try
            {
                var parameters = new Dictionary<string, object>
                {
                    ["@Email"] = email
                };
                var result = await SqlHelper.ExecuteScalar(
                    "SELECT COUNT(*) FROM `users` WHERE `User` = @Email",
                    parameters,
                    useAsync: useAsync);
                return Convert.ToInt32(result) > 0;
            }
            catch (MySqlException ex)
            {
                AppLogger.LogDatabaseError(ex);
                ErrorLogDao.HandleException_SQLError_CloseApp(ex);
                return false;
            }
            catch (Exception ex)
            {
                AppLogger.LogDatabaseError(ex);
                ErrorLogDao.HandleException_GeneralError_CloseApp(ex);
                return false;
            }
        }

        internal static async Task InsertUser(string email, string fullName, string shift, string isVitsUser,
            string? pin, bool useAsync = false)
        {
            try
            {
                var parameters = new Dictionary<string, object>
                {
                    ["@Email"] = email,
                    ["@FullName"] = fullName,
                    ["@Shift"] = shift,
                    ["@IsVitsUser"] = isVitsUser,
                    ["@Pin"] = pin ?? (object)DBNull.Value
                };
                await SqlHelper.ExecuteNonQuery(
                    "INSERT INTO `users` (`User`, `Full Name`, `Shift`, `VitsUser`, `Pin`) VALUES (@Email, @FullName, @Shift, @IsVitsUser, @Pin)",
                    parameters,
                    useAsync: useAsync);

                // Update WipAppVariables if the inserted user is the current user
                if (WipAppVariables.User == email)
                {
                    WipAppVariables.PartType = null;
                    WipAppVariables.UserShift = shift;
                }
            }
            catch (MySqlException ex)
            {
                AppLogger.LogDatabaseError(ex);
                ErrorLogDao.HandleException_SQLError_CloseApp(ex);
            }
            catch (Exception ex)
            {
                AppLogger.LogDatabaseError(ex);
                ErrorLogDao.HandleException_GeneralError_CloseApp(ex);
            }
        }

        internal static async Task UpdateUser(string email, string fullName, string shift, string isVitsUser,
            string? pin, bool useAsync = false)
        {
            try
            {
                var parameters = new Dictionary<string, object>
                {
                    ["@Email"] = email,
                    ["@FullName"] = fullName,
                    ["@Shift"] = shift,
                    ["@IsVitsUser"] = isVitsUser,
                    ["@Pin"] = pin ?? (object)DBNull.Value
                };
                await SqlHelper.ExecuteNonQuery(
                    "UPDATE `users` SET `Full Name` = @FullName, `Shift` = @Shift, `VitsUser` = @IsVitsUser, `Pin` = @Pin WHERE `User` = @Email",
                    parameters,
                    useAsync: useAsync);

                // Update WipAppVariables if the updated user is the current user
                if (WipAppVariables.User == email)
                {
                    WipAppVariables.UserShift = shift;
                }
            }
            catch (MySqlException ex)
            {
                AppLogger.LogDatabaseError(ex);
                ErrorLogDao.HandleException_SQLError_CloseApp(ex);
            }
            catch (Exception ex)
            {
                AppLogger.LogDatabaseError(ex);
                ErrorLogDao.HandleException_GeneralError_CloseApp(ex);
            }
        }

        internal static async Task SetUserAdminStatus(string email, bool isAdmin, bool useAsync = false)
        {
            try
            {
                var parameters = new Dictionary<string, object> { ["@Email"] = email };
                if (isAdmin)
                {
                    var count = Convert.ToInt32(await SqlHelper.ExecuteScalar(
                        "SELECT COUNT(*) FROM `leads` WHERE `User` = @Email", parameters, useAsync: useAsync));
                    if (count == 0)
                    {
                        await SqlHelper.ExecuteNonQuery(
                            "INSERT INTO `leads` (`User`) VALUES (@Email)", parameters, useAsync: useAsync);
                    }
                }
                else
                {
                    await SqlHelper.ExecuteNonQuery(
                        "DELETE FROM `leads` WHERE `User` = @Email", parameters, useAsync: useAsync);
                }

                // Update WipAppVariables if the affected user is the current user
                if (WipAppVariables.User == email)
                {
                    WipAppVariables.userTypeAdmin = isAdmin;
                }
            }
            catch (MySqlException ex)
            {
                AppLogger.LogDatabaseError(ex);
                ErrorLogDao.HandleException_SQLError_CloseApp(ex);
            }
            catch (Exception ex)
            {
                AppLogger.LogDatabaseError(ex);
                ErrorLogDao.HandleException_GeneralError_CloseApp(ex);
            }
        }

        internal static async Task SetUserReadOnlyStatus(string email, bool isReadOnly, bool useAsync = false)
        {
            try
            {
                var parameters = new Dictionary<string, object> { ["@Email"] = email };
                if (isReadOnly)
                {
                    var count = Convert.ToInt32(await SqlHelper.ExecuteScalar(
                        "SELECT COUNT(*) FROM `readonly` WHERE `User` = @Email", parameters, useAsync: useAsync));
                    if (count == 0)
                    {
                        await SqlHelper.ExecuteNonQuery(
                            "INSERT INTO `readonly` (`User`) VALUES (@Email)", parameters, useAsync: useAsync);
                    }
                }
                else
                {
                    await SqlHelper.ExecuteNonQuery(
                        "DELETE FROM `readonly` WHERE `User` = @Email", parameters, useAsync: useAsync);
                }

                // Update WipAppVariables if the affected user is the current user
                if (WipAppVariables.User == email)
                {
                    WipAppVariables.userTypeReadOnly = isReadOnly;
                }
            }
            catch (MySqlException ex)
            {
                AppLogger.LogDatabaseError(ex);
                ErrorLogDao.HandleException_SQLError_CloseApp(ex);
            }
            catch (Exception ex)
            {
                AppLogger.LogDatabaseError(ex);
                ErrorLogDao.HandleException_GeneralError_CloseApp(ex);
            }
        }

        internal static async Task AddUserToMySqlServer(string email, bool useAsync = false)
        {
            try
            {
                var parameters = new Dictionary<string, object> { ["@Email"] = email };
                var count = Convert.ToInt32(await SqlHelper.ExecuteScalar(
                    "SELECT COUNT(*) FROM mysql.user WHERE User = @Email AND Host = '%'", parameters,
                    useAsync: useAsync));
                if (count == 0)
                {
                    // Note: This uses string interpolation for SQL, which is generally not recommended.
                    // If you need to support dynamic user names, ensure proper sanitization.
                    var createUserSql =
                        $"CREATE USER '{email}'@'%' IDENTIFIED WITH mysql_native_password AS ''; " +
                        $"GRANT ALL PRIVILEGES ON *.* TO '{email}'@'%' REQUIRE NONE WITH GRANT OPTION " +
                        $"MAX_QUERIES_PER_HOUR 0 MAX_CONNECTIONS_PER_HOUR 0 MAX_UPDATES_PER_HOUR 0 MAX_USER_CONNECTIONS 0;";
                    await SqlHelper.ExecuteNonQuery(createUserSql, useAsync: useAsync);
                }
            }
            catch (MySqlException ex)
            {
                AppLogger.LogDatabaseError(ex);
                ErrorLogDao.HandleException_SQLError_CloseApp(ex);
            }
            catch (Exception ex)
            {
                AppLogger.LogDatabaseError(ex);
                ErrorLogDao.HandleException_GeneralError_CloseApp(ex);
            }
        }

        internal static async Task<DataRow?> GetUserByEmail(string email, bool useAsync = false)
        {
            try
            {
                var parameters = new Dictionary<string, object> { ["@Email"] = email };
                var table = await SqlHelper.ExecuteDataTable("SELECT * FROM `users` WHERE `User` = @Email", parameters,
                    useAsync: useAsync);
                return table.Rows.Count > 0 ? table.Rows[0] : null;
            }
            catch (MySqlException ex)
            {
                AppLogger.LogDatabaseError(ex);
                ErrorLogDao.HandleException_SQLError_CloseApp(ex);
                return null;
            }
            catch (Exception ex)
            {
                AppLogger.LogDatabaseError(ex);
                ErrorLogDao.HandleException_GeneralError_CloseApp(ex);
                return null;
            }
        }

        internal static async Task<DataTable> GetAllUsers(bool useAsync = false)
        {
            try
            {
                return await SqlHelper.ExecuteDataTable("SELECT * FROM `users`", useAsync: useAsync);
            }
            catch (MySqlException ex)
            {
                AppLogger.LogDatabaseError(ex);
                ErrorLogDao.HandleException_SQLError_CloseApp(ex);
                return new DataTable();
            }
            catch (Exception ex)
            {
                AppLogger.LogDatabaseError(ex);
                ErrorLogDao.HandleException_GeneralError_CloseApp(ex);
                return new DataTable();
            }
        }

        internal static async Task DeleteUser(string email, bool useAsync = false)
        {
            try
            {
                var parameters = new Dictionary<string, object> { ["@Email"] = email };
                await SqlHelper.ExecuteNonQuery("DELETE FROM `users` WHERE `User` = @Email", parameters,
                    useAsync: useAsync);
            }
            catch (MySqlException ex)
            {
                AppLogger.LogDatabaseError(ex);
                ErrorLogDao.HandleException_SQLError_CloseApp(ex);
            }
            catch (Exception ex)
            {
                AppLogger.LogDatabaseError(ex);
                ErrorLogDao.HandleException_GeneralError_CloseApp(ex);
            }
        }

        internal static async Task ChangeUserPin(string email, string newPin, bool useAsync = false)
        {
            try
            {
                var parameters = new Dictionary<string, object>
                {
                    ["@Email"] = email,
                    ["@Pin"] = newPin
                };
                await SqlHelper.ExecuteNonQuery("UPDATE `users` SET `Pin` = @Pin WHERE `User` = @Email", parameters,
                    useAsync: useAsync);

                // Optionally update WipAppVariables if you store the pin (not shown in WipAppVariables)
                if (WipAppVariables.User == email)
                {
                    WipAppVariables.UserPin = newPin;
                }
            }
            catch (MySqlException ex)
            {
                AppLogger.LogDatabaseError(ex);
                ErrorLogDao.HandleException_SQLError_CloseApp(ex);
            }
            catch (Exception ex)
            {
                AppLogger.LogDatabaseError(ex);
                ErrorLogDao.HandleException_GeneralError_CloseApp(ex);
            }
        }

        internal static async Task SetUserShift(string email, string shift, bool useAsync = false)
        {
            try
            {
                var parameters = new Dictionary<string, object>
                {
                    ["@Email"] = email,
                    ["@Shift"] = shift
                };
                await SqlHelper.ExecuteNonQuery("UPDATE `users` SET `Shift` = @Shift WHERE `User` = @Email", parameters,
                    useAsync: useAsync);

                // Update WipAppVariables if the affected user is the current user
                if (WipAppVariables.User == email)
                {
                    WipAppVariables.UserShift = shift;
                }
            }
            catch (MySqlException ex)
            {
                AppLogger.LogDatabaseError(ex);
                ErrorLogDao.HandleException_SQLError_CloseApp(ex);
            }
            catch (Exception ex)
            {
                AppLogger.LogDatabaseError(ex);
                ErrorLogDao.HandleException_GeneralError_CloseApp(ex);
            }
        }


        internal static async Task<DataTable> GetAdmins(bool useAsync = false)
        {
            try
            {
                return await SqlHelper.ExecuteDataTable("SELECT `User` FROM `leads`", useAsync: useAsync);
            }
            catch (MySqlException ex)
            {
                AppLogger.LogDatabaseError(ex);
                ErrorLogDao.HandleException_SQLError_CloseApp(ex);
                return new DataTable();
            }
            catch (Exception ex)
            {
                AppLogger.LogDatabaseError(ex);
                ErrorLogDao.HandleException_GeneralError_CloseApp(ex);
                return new DataTable();
            }
        }

        internal static async Task<DataTable> GetReadOnlyUsers(bool useAsync = false)
        {
            try
            {
                return await SqlHelper.ExecuteDataTable("SELECT `User` FROM `readonly`", useAsync: useAsync);
            }
            catch (MySqlException ex)
            {
                AppLogger.LogDatabaseError(ex);
                ErrorLogDao.HandleException_SQLError_CloseApp(ex);
                return new DataTable();
            }
            catch (Exception ex)
            {
                AppLogger.LogDatabaseError(ex);
                ErrorLogDao.HandleException_GeneralError_CloseApp(ex);
                return new DataTable();
            }
        }
    }

    internal static class PartDao
    {
        internal static async Task<bool> PartExists(string partNumber, bool useAsync = false)
        {
            var parameters = new Dictionary<string, object>
            {
                ["@partNumber"] = partNumber
            };
            var result = await SqlHelper.ExecuteScalar(
                "SELECT COUNT(*) FROM `part_ids` WHERE `Item Number` = @partNumber",
                parameters,
                useAsync: useAsync);
            return Convert.ToInt32(result) > 0;
        }

        internal static async Task InsertPart(string partNumber, string user, string partType, bool useAsync = false)
        {
            var parameters = new Dictionary<string, object>
            {
                ["@partNumber"] = partNumber,
                ["@user"] = user,
                ["@partType"] = partType
            };
            await SqlHelper.ExecuteNonQuery(
                "INSERT INTO `part_ids` (`Item Number`, `ID`, `Issued By`, `Type`) VALUES (@partNumber, NULL, @user, @partType);",
                parameters,
                useAsync: useAsync);
        }

        internal static async Task<DataTable> GetAllParts(bool useAsync = false)
        {
            try
            {
                return await SqlHelper.ExecuteDataTable("SELECT * FROM `part_ids`", useAsync: useAsync);
            }
            catch (MySqlException ex)
            {
                AppLogger.LogDatabaseError(ex);
                ErrorLogDao.HandleException_SQLError_CloseApp(ex);
                return new DataTable();
            }
            catch (Exception ex)
            {
                AppLogger.LogDatabaseError(ex);
                ErrorLogDao.HandleException_GeneralError_CloseApp(ex);
                return new DataTable();
            }
        }

        internal static async Task<DataRow?> GetPartByNumber(string partNumber, bool useAsync = false)
        {
            try
            {
                var parameters = new Dictionary<string, object>
                {
                    ["@partNumber"] = partNumber
                };
                var table = await SqlHelper.ExecuteDataTable(
                    "SELECT * FROM `part_ids` WHERE `Item Number` = @partNumber",
                    parameters,
                    useAsync: useAsync);
                return table.Rows.Count > 0 ? table.Rows[0] : null;
            }
            catch (MySqlException ex)
            {
                AppLogger.LogDatabaseError(ex);
                ErrorLogDao.HandleException_SQLError_CloseApp(ex);
                return null;
            }
            catch (Exception ex)
            {
                AppLogger.LogDatabaseError(ex);
                ErrorLogDao.HandleException_GeneralError_CloseApp(ex);
                return null;
            }
        }

        internal static async Task UpdatePart(string partNumber, string partType, string user, bool useAsync = false)
        {
            try
            {
                var parameters = new Dictionary<string, object>
                {
                    ["@partNumber"] = partNumber,
                    ["@partType"] = partType,
                    ["@user"] = user
                };
                await SqlHelper.ExecuteNonQuery(
                    "UPDATE `part_ids` SET `Type` = @partType, `Issued By` = @user WHERE `Item Number` = @partNumber",
                    parameters,
                    useAsync: useAsync);
            }
            catch (MySqlException ex)
            {
                AppLogger.LogDatabaseError(ex);
                ErrorLogDao.HandleException_SQLError_CloseApp(ex);
            }
            catch (Exception ex)
            {
                AppLogger.LogDatabaseError(ex);
                ErrorLogDao.HandleException_GeneralError_CloseApp(ex);
            }
        }

        internal static async Task DeletePart(string partNumber, bool useAsync = false)
        {
            try
            {
                var parameters = new Dictionary<string, object>
                {
                    ["@partNumber"] = partNumber
                };
                await SqlHelper.ExecuteNonQuery(
                    "DELETE FROM `part_ids` WHERE `Item Number` = @partNumber",
                    parameters,
                    useAsync: useAsync);
            }
            catch (MySqlException ex)
            {
                AppLogger.LogDatabaseError(ex);
                ErrorLogDao.HandleException_SQLError_CloseApp(ex);
            }
            catch (Exception ex)
            {
                AppLogger.LogDatabaseError(ex);
                ErrorLogDao.HandleException_GeneralError_CloseApp(ex);
            }
        }
    }

    internal static class PartTypeDao
    {
        internal static async Task<bool> PartTypeExists(string partType, bool useAsync = false)
        {
            var parameters = new Dictionary<string, object>
            {
                ["@partType"] = partType
            };
            var result = await SqlHelper.ExecuteScalar(
                "SELECT COUNT(*) FROM `item_types` WHERE `Type` = @partType",
                parameters,
                useAsync: useAsync);
            return Convert.ToInt32(result) > 0;
        }

        internal static async Task InsertPartType(string partType, string user, bool useAsync = false)
        {
            var parameters = new Dictionary<string, object>
            {
                ["@partType"] = partType,
                ["@user"] = user
            };
            await SqlHelper.ExecuteNonQuery(
                "INSERT INTO `item_types` (`Type`, `ID`, `Issued By`) VALUES (@partType, NULL, @user);",
                parameters,
                useAsync: useAsync);
        }

        internal static async Task<DataTable> GetAllPartTypes(bool useAsync = false)
        {
            try
            {
                return await SqlHelper.ExecuteDataTable("SELECT * FROM `item_types`", useAsync: useAsync);
            }
            catch (MySqlException ex)
            {
                AppLogger.LogDatabaseError(ex);
                ErrorLogDao.HandleException_SQLError_CloseApp(ex);
                return new DataTable();
            }
            catch (Exception ex)
            {
                AppLogger.LogDatabaseError(ex);
                ErrorLogDao.HandleException_GeneralError_CloseApp(ex);
                return new DataTable();
            }
        }

        internal static async Task<DataRow?> GetPartTypeByName(string partType, bool useAsync = false)
        {
            try
            {
                var parameters = new Dictionary<string, object>
                {
                    ["@partType"] = partType
                };
                var table = await SqlHelper.ExecuteDataTable(
                    "SELECT * FROM `item_types` WHERE `Type` = @partType",
                    parameters,
                    useAsync: useAsync);
                return table.Rows.Count > 0 ? table.Rows[0] : null;
            }
            catch (MySqlException ex)
            {
                AppLogger.LogDatabaseError(ex);
                ErrorLogDao.HandleException_SQLError_CloseApp(ex);
                return null;
            }
            catch (Exception ex)
            {
                AppLogger.LogDatabaseError(ex);
                ErrorLogDao.HandleException_GeneralError_CloseApp(ex);
                return null;
            }
        }

        internal static async Task UpdatePartType(string partType, string newType, string user, bool useAsync = false)
        {
            try
            {
                var parameters = new Dictionary<string, object>
                {
                    ["@partType"] = partType,
                    ["@newType"] = newType,
                    ["@user"] = user
                };
                await SqlHelper.ExecuteNonQuery(
                    "UPDATE `item_types` SET `Type` = @newType, `Issued By` = @user WHERE `Type` = @partType",
                    parameters,
                    useAsync: useAsync);
            }
            catch (MySqlException ex)
            {
                AppLogger.LogDatabaseError(ex);
                ErrorLogDao.HandleException_SQLError_CloseApp(ex);
            }
            catch (Exception ex)
            {
                AppLogger.LogDatabaseError(ex);
                ErrorLogDao.HandleException_GeneralError_CloseApp(ex);
            }
        }

        internal static async Task DeletePartType(string partType, bool useAsync = false)
        {
            try
            {
                var parameters = new Dictionary<string, object>
                {
                    ["@partType"] = partType
                };
                await SqlHelper.ExecuteNonQuery(
                    "DELETE FROM `item_types` WHERE `Type` = @partType",
                    parameters,
                    useAsync: useAsync);
            }
            catch (MySqlException ex)
            {
                AppLogger.LogDatabaseError(ex);
                ErrorLogDao.HandleException_SQLError_CloseApp(ex);
            }
            catch (Exception ex)
            {
                AppLogger.LogDatabaseError(ex);
                ErrorLogDao.HandleException_GeneralError_CloseApp(ex);
            }
        }
    }


    internal static class OperationDao
    {
        internal static async Task<bool> OperationExists(string operationNumber, bool useAsync = false)
        {
            var parameters = new Dictionary<string, object>
            {
                ["@operationNumber"] = operationNumber
            };
            var result = await SqlHelper.ExecuteScalar(
                "SELECT COUNT(*) FROM `operation_numbers` WHERE `Operation` = @operationNumber",
                parameters,
                useAsync: useAsync);
            return Convert.ToInt32(result) > 0;
        }

        internal static async Task InsertOperation(string operationNumber, string user, bool useAsync = false)
        {
            var parameters = new Dictionary<string, object>
            {
                ["@operationNumber"] = operationNumber,
                ["@user"] = user
            };
            await SqlHelper.ExecuteNonQuery(
                "INSERT INTO `operation_numbers` (`Operation`, `ID`, `Issued By`) VALUES (@operationNumber, NULL, @user);",
                parameters,
                useAsync: useAsync);
        }

        internal static async Task<DataTable> GetAllOperations(bool useAsync = false)
        {
            try
            {
                return await SqlHelper.ExecuteDataTable("SELECT * FROM `operation_numbers`", useAsync: useAsync);
            }
            catch (MySqlException ex)
            {
                AppLogger.LogDatabaseError(ex);
                ErrorLogDao.HandleException_SQLError_CloseApp(ex);
                return new DataTable();
            }
            catch (Exception ex)
            {
                AppLogger.LogDatabaseError(ex);
                ErrorLogDao.HandleException_GeneralError_CloseApp(ex);
                return new DataTable();
            }
        }

        internal static async Task<DataRow?> GetOperationByNumber(string operationNumber, bool useAsync = false)
        {
            try
            {
                var parameters = new Dictionary<string, object>
                {
                    ["@operationNumber"] = operationNumber
                };
                var table = await SqlHelper.ExecuteDataTable(
                    "SELECT * FROM `operation_numbers` WHERE `Operation` = @operationNumber",
                    parameters,
                    useAsync: useAsync);
                return table.Rows.Count > 0 ? table.Rows[0] : null;
            }
            catch (MySqlException ex)
            {
                AppLogger.LogDatabaseError(ex);
                ErrorLogDao.HandleException_SQLError_CloseApp(ex);
                return null;
            }
            catch (Exception ex)
            {
                AppLogger.LogDatabaseError(ex);
                ErrorLogDao.HandleException_GeneralError_CloseApp(ex);
                return null;
            }
        }

        internal static async Task UpdateOperation(string operationNumber, string newOperationNumber, string user,
            bool useAsync = false)
        {
            try
            {
                var parameters = new Dictionary<string, object>
                {
                    ["@operationNumber"] = operationNumber,
                    ["@newOperationNumber"] = newOperationNumber,
                    ["@user"] = user
                };
                await SqlHelper.ExecuteNonQuery(
                    "UPDATE `operation_numbers` SET `Operation` = @newOperationNumber, `Issued By` = @user WHERE `Operation` = @operationNumber",
                    parameters,
                    useAsync: useAsync);
            }
            catch (MySqlException ex)
            {
                AppLogger.LogDatabaseError(ex);
                ErrorLogDao.HandleException_SQLError_CloseApp(ex);
            }
            catch (Exception ex)
            {
                AppLogger.LogDatabaseError(ex);
                ErrorLogDao.HandleException_GeneralError_CloseApp(ex);
            }
        }

        internal static async Task DeleteOperation(string operationNumber, bool useAsync = false)
        {
            try
            {
                var parameters = new Dictionary<string, object>
                {
                    ["@operationNumber"] = operationNumber
                };
                await SqlHelper.ExecuteNonQuery(
                    "DELETE FROM `operation_numbers` WHERE `Operation` = @operationNumber",
                    parameters,
                    useAsync: useAsync);
            }
            catch (MySqlException ex)
            {
                AppLogger.LogDatabaseError(ex);
                ErrorLogDao.HandleException_SQLError_CloseApp(ex);
            }
            catch (Exception ex)
            {
                AppLogger.LogDatabaseError(ex);
                ErrorLogDao.HandleException_GeneralError_CloseApp(ex);
            }
        }
    }


    internal static class LocationDao
    {
        internal static async Task<bool> LocationExists(string location, bool useAsync = false)
        {
            var parameters = new Dictionary<string, object>
            {
                ["@location"] = location
            };
            var result = await SqlHelper.ExecuteScalar(
                "SELECT COUNT(*) FROM `locations` WHERE `Location` = @location",
                parameters,
                useAsync: useAsync);
            return Convert.ToInt32(result) > 0;
        }

        internal static async Task InsertLocation(string location, string user, bool useAsync = false)
        {
            var parameters = new Dictionary<string, object>
            {
                ["@location"] = location,
                ["@user"] = user
            };
            await SqlHelper.ExecuteNonQuery(
                "INSERT INTO `locations` (`Location`, `ID`, `Issued By`) VALUES (@location, NULL, @user);",
                parameters,
                useAsync: useAsync);
        }

        internal static async Task<DataTable> GetAllLocations(bool useAsync = false)
        {
            try
            {
                return await SqlHelper.ExecuteDataTable("SELECT * FROM `locations`", useAsync: useAsync);
            }
            catch (MySqlException ex)
            {
                AppLogger.LogDatabaseError(ex);
                ErrorLogDao.HandleException_SQLError_CloseApp(ex);
                return new DataTable();
            }
            catch (Exception ex)
            {
                AppLogger.LogDatabaseError(ex);
                ErrorLogDao.HandleException_GeneralError_CloseApp(ex);
                return new DataTable();
            }
        }

        internal static async Task<DataRow?> GetLocationByName(string location, bool useAsync = false)
        {
            try
            {
                var parameters = new Dictionary<string, object>
                {
                    ["@location"] = location
                };
                var table = await SqlHelper.ExecuteDataTable(
                    "SELECT * FROM `locations` WHERE `Location` = @location",
                    parameters,
                    useAsync: useAsync);
                return table.Rows.Count > 0 ? table.Rows[0] : null;
            }
            catch (MySqlException ex)
            {
                AppLogger.LogDatabaseError(ex);
                ErrorLogDao.HandleException_SQLError_CloseApp(ex);
                return null;
            }
            catch (Exception ex)
            {
                AppLogger.LogDatabaseError(ex);
                ErrorLogDao.HandleException_GeneralError_CloseApp(ex);
                return null;
            }
        }

        internal static async Task UpdateLocation(string location, string newLocation, string user,
            bool useAsync = false)
        {
            try
            {
                var parameters = new Dictionary<string, object>
                {
                    ["@location"] = location,
                    ["@newLocation"] = newLocation,
                    ["@user"] = user
                };
                await SqlHelper.ExecuteNonQuery(
                    "UPDATE `locations` SET `Location` = @newLocation, `Issued By` = @user WHERE `Location` = @location",
                    parameters,
                    useAsync: useAsync);
            }
            catch (MySqlException ex)
            {
                AppLogger.LogDatabaseError(ex);
                ErrorLogDao.HandleException_SQLError_CloseApp(ex);
            }
            catch (Exception ex)
            {
                AppLogger.LogDatabaseError(ex);
                ErrorLogDao.HandleException_GeneralError_CloseApp(ex);
            }
        }

        internal static async Task DeleteLocation(string location, bool useAsync = false)
        {
            try
            {
                var parameters = new Dictionary<string, object>
                {
                    ["@location"] = location
                };
                await SqlHelper.ExecuteNonQuery(
                    "DELETE FROM `locations` WHERE `Location` = @location",
                    parameters,
                    useAsync: useAsync);
            }
            catch (MySqlException ex)
            {
                AppLogger.LogDatabaseError(ex);
                ErrorLogDao.HandleException_SQLError_CloseApp(ex);
            }
            catch (Exception ex)
            {
                AppLogger.LogDatabaseError(ex);
                ErrorLogDao.HandleException_GeneralError_CloseApp(ex);
            }
        }
    }


    internal static class ErrorLogDao
    {
        internal static async Task<List<(string Method, string Error)>> GetUniqueErrorsAsync(bool useAsync = false)
        {
            var uniqueErrors = new List<(string Method, string Error)>();
            try
            {
                using var reader = useAsync
                    ? await SqlHelper.ExecuteReader("SELECT DISTINCT `Method`, `Error` FROM `wipapp_errorlog`",
                        useAsync: true)
                    : SqlHelper.ExecuteReader("SELECT DISTINCT `Method`, `Error` FROM `wipapp_errorlog`").Result;

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
                HandleException_SQLError_CloseApp(ex, useAsync);
            }
            catch (Exception ex)
            {
                AppLogger.LogDatabaseError(ex);
                AppLogger.Log("Error in GetUniqueErrors: " + ex.Message);
                HandleException_GeneralError_CloseApp(ex, useAsync);
            }

            return uniqueErrors;
        }

        internal static async Task<DataTable> GetAllErrorsAsync(bool useAsync = false)
        {
            try
            {
                return await SqlHelper.ExecuteDataTable("SELECT * FROM `wipapp_errorlog` ORDER BY `DateTime` DESC",
                    useAsync: useAsync);
            }
            catch (MySqlException ex)
            {
                AppLogger.LogDatabaseError(ex);
                await HandleException_SQLError_CloseApp(ex, useAsync);
                return new DataTable();
            }
            catch (Exception ex)
            {
                AppLogger.LogDatabaseError(ex);
                await HandleException_GeneralError_CloseApp(ex, useAsync);
                return new DataTable();
            }
        }

        internal static async Task<DataTable> GetErrorsByUserAsync(string user, bool useAsync = false)
        {
            try
            {
                var parameters = new Dictionary<string, object>
                {
                    ["@User"] = user
                };
                return await SqlHelper.ExecuteDataTable(
                    "SELECT * FROM `wipapp_errorlog` WHERE `User` = @User ORDER BY `DateTime` DESC",
                    parameters, useAsync: useAsync);
            }
            catch (MySqlException ex)
            {
                AppLogger.LogDatabaseError(ex);
                await HandleException_SQLError_CloseApp(ex, useAsync);
                return new DataTable();
            }
            catch (Exception ex)
            {
                AppLogger.LogDatabaseError(ex);
                await HandleException_GeneralError_CloseApp(ex, useAsync);
                return new DataTable();
            }
        }

        internal static async Task DeleteErrorByIdAsync(int id, bool useAsync = false)
        {
            try
            {
                var parameters = new Dictionary<string, object>
                {
                    ["@Id"] = id
                };
                await SqlHelper.ExecuteNonQuery(
                    "DELETE FROM `wipapp_errorlog` WHERE `ID` = @Id",
                    parameters, useAsync: useAsync);
            }
            catch (MySqlException ex)
            {
                AppLogger.LogDatabaseError(ex);
                await HandleException_SQLError_CloseApp(ex, useAsync);
            }
            catch (Exception ex)
            {
                AppLogger.LogDatabaseError(ex);
                await HandleException_GeneralError_CloseApp(ex, useAsync);
            }
        }

        internal static async Task DeleteAllErrorsAsync(bool useAsync = false)
        {
            try
            {
                await SqlHelper.ExecuteNonQuery("DELETE FROM `wipapp_errorlog`", useAsync: useAsync);
            }
            catch (MySqlException ex)
            {
                AppLogger.LogDatabaseError(ex);
                await HandleException_SQLError_CloseApp(ex, useAsync);
            }
            catch (Exception ex)
            {
                AppLogger.LogDatabaseError(ex);
                await HandleException_GeneralError_CloseApp(ex, useAsync);
            }
        }

        internal static async Task<DataTable> GetErrorsByDateRangeAsync(DateTime start, DateTime end,
            bool useAsync = false)
        {
            try
            {
                var parameters = new Dictionary<string, object>
                {
                    ["@Start"] = start,
                    ["@End"] = end
                };
                return await SqlHelper.ExecuteDataTable(
                    "SELECT * FROM `wipapp_errorlog` WHERE `DateTime` BETWEEN @Start AND @End ORDER BY `DateTime` DESC",
                    parameters, useAsync: useAsync);
            }
            catch (MySqlException ex)
            {
                AppLogger.LogDatabaseError(ex);
                await HandleException_SQLError_CloseApp(ex, useAsync);
                return new DataTable();
            }
            catch (Exception ex)
            {
                AppLogger.LogDatabaseError(ex);
                await HandleException_GeneralError_CloseApp(ex, useAsync);
                return new DataTable();
            }
        }

        internal static async Task HandleException_SQLError_CloseApp(
            Exception ex,
            bool useAsync = false,
            [System.Runtime.CompilerServices.CallerMemberName]
            string callerName = "",
            string controlName = "")
        {
            try
            {
                AppLogger.Log($"SQL Error in method: {callerName}, Control: {controlName}");
                AppLogger.Log($"Exception Message: {ex.Message}");
                AppLogger.Log($"Stack Trace: {ex.StackTrace}");

                if (ex is MySqlException mysqlEx)
                {
                    AppLogger.Log($"MySQL Error Code: {mysqlEx.Number}");
                    AppLogger.Log($"MySQL Error Details: {mysqlEx.Message}");
                }

                var isConnectionError = ex.Message.Contains("Unable to connect to any of the specified MySQL hosts.")
                                        || ex.Message.Contains("Access denied for user")
                                        || ex.Message.Contains("Can't connect to MySQL server on")
                                        || ex.Message.Contains("Unknown MySQL server host")
                                        || ex.Message.Contains("Lost connection to MySQL server")
                                        || ex.Message.Contains("MySQL server has gone away");

                if (isConnectionError)
                {
                    MessageBox.Show(
                        @"Database connection error. The application will now close.",
                        @"Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    Process.GetCurrentProcess().Kill();
                }
                else
                {
                    var parameters = new Dictionary<string, object>
                    {
                        ["@Method"] = callerName,
                        ["@Error"] = ex.Message,
                        ["@User"] = WipAppVariables.User,
                        ["@DateTime"] = DateTime.Now,
                        ["@Control"] = controlName
                    };
                    var sql = "INSERT INTO `wipapp_errorlog` (`Method`, `Error`, `User`, `DateTime`, `Control`) " +
                              "VALUES (@Method, @Error, @User, @DateTime, @Control)";
                    if (useAsync)
                    {
                        await SqlHelper.ExecuteNonQuery(sql, parameters, useAsync: true);
                    }
                    else
                    {
                        SqlHelper.ExecuteNonQuery(sql, parameters).Wait();
                    }
                }
            }
            catch (Exception innerEx)
            {
                AppLogger.Log($"Error while handling exception: {innerEx.Message}");
            }
        }

        internal static async Task HandleException_GeneralError_CloseApp(
            Exception ex,
            bool useAsync = false,
            [System.Runtime.CompilerServices.CallerMemberName]
            string callerName = "",
            string controlName = "")
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

                var message = $"{errorType}\nMethod: {callerName}\nControl: {controlName}\nException:\n{ex.Message}";

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

                var parameters = new Dictionary<string, object>
                {
                    ["@Method"] = callerName,
                    ["@Error"] = ex.Message,
                    ["@User"] = WipAppVariables.User,
                    ["@DateTime"] = DateTime.Now,
                    ["@Control"] = controlName
                };
                var sql = "INSERT INTO `wipapp_errorlog` (`Method`, `Error`, `User`, `DateTime`, `Control`) " +
                          "VALUES (@Method, @Error, @User, @DateTime, @Control)";
                if (useAsync)
                {
                    await SqlHelper.ExecuteNonQuery(sql, parameters, useAsync: true);
                }
                else
                {
                    SqlHelper.ExecuteNonQuery(sql, parameters).Wait();
                }

                if (isCritical)
                {
                    MessageBox.Show(message + "\n\nThe application will now close due to a critical error.",
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
                await HandleException_GeneralError_CloseApp(innerEx, useAsync, controlName: controlName);
            }
        }

        internal static List<(string Method, string Error)> GetUniqueErrors()
        {
            return GetUniqueErrorsAsync(false).GetAwaiter().GetResult();
        }

        public static void HandleException_SQLError_CloseApp(MySqlException mySqlException, bool useAsync,
            string controlName)
        {
            throw new NotImplementedException();
        }

        internal static void LogErrorWithMethod(Exception ex,
            [System.Runtime.CompilerServices.CallerMemberName] string methodName = "")
        {
            AppLogger.Log($"Error in {methodName}: {ex.Message}");
        }
    }

    internal static class InventoryDao
    {
        internal static async Task<List<Search>?> InventoryTab_SaveAsync(bool useAsync = false)
        {
            try
            {
                List<Search> returnThese = [];
                var type = await InventoryTab_GetItemTypeAsync(useAsync);

                var parameters1 = new Dictionary<string, object>
                {
                    ["@Location"] = WipAppVariables.Location,
                    ["@PartID"] = WipAppVariables.partId,
                    ["@Op"] = WipAppVariables.Operation,
                    ["@Notes"] = WipAppVariables.Notes,
                    ["@Quantity"] = WipAppVariables.InventoryQuantity,
                    ["@User"] = WipAppVariables.User,
                    ["@Type"] = type
                };
                await SqlHelper.ExecuteNonQuery(
                    "INSERT INTO `saved_locations` (`ID`, `Location`, `Item Number`, `Op`, `Notes`, `Quantity`, `Date_Time`, `User` , `Item Type`) " +
                    "VALUES (NULL, @Location, @PartID, @Op, @Notes, @Quantity, CURRENT_TIMESTAMP, @User, @Type);",
                    parameters1, useAsync: useAsync);

                var parameters2 = new Dictionary<string, object>
                {
                    ["@User"] = WipAppVariables.User,
                    ["@PartID"] = WipAppVariables.partId,
                    ["@Location"] = WipAppVariables.Location,
                    ["@Type"] = WipAppVariables.PartType,
                    ["@Quantity"] = WipAppVariables.InventoryQuantity
                };
                await SqlHelper.ExecuteNonQuery(
                    "INSERT INTO `input_history` (`User`, `Part ID`, `Location`, `Type`, `Quantity`) " +
                    "VALUES(@User, @PartID, @Location, @Type, @Quantity)",
                    parameters2, useAsync: useAsync);

                AppLogger.Log("InventoryTab_Save executed successfully.");
                return returnThese;
            }
            catch (MySqlException ex)
            {
                AppLogger.LogDatabaseError(ex);
                AppLogger.Log("Error in InventoryTab_Save: " + ex.Message);
                await ErrorLogDao.HandleException_SQLError_CloseApp(ex, useAsync);
                return null;
            }
            catch (Exception ex)
            {
                AppLogger.LogDatabaseError(ex);
                AppLogger.Log("Error in InventoryTab_Save: " + ex.Message);
                await ErrorLogDao.HandleException_GeneralError_CloseApp(ex, useAsync);
                return null;
            }
        }

        internal static async Task<string> InventoryTab_GetItemTypeAsync(bool useAsync = false)
        {
            try
            {
                var parameters = new Dictionary<string, object>
                {
                    ["@PartID"] = WipAppVariables.partId
                };
                using var reader = useAsync
                    ? await SqlHelper.ExecuteReader("SELECT * FROM `part_ids` WHERE `Item Number` = @PartID",
                        parameters, useAsync: true)
                    : SqlHelper.ExecuteReader("SELECT * FROM `part_ids` WHERE `Item Number` = @PartID", parameters)
                        .Result;
                while (reader.Read())
                {
                    var partId = reader.GetString(0);
                    if (partId == WipAppVariables.partId)
                    {
                        return reader.GetString(3);
                    }
                }

                AppLogger.Log("InventoryTab_GetItemType executed successfully for part ID: " + WipAppVariables.partId);
                return "Unknown";
            }
            catch (MySqlException ex)
            {
                AppLogger.LogDatabaseError(ex);
                AppLogger.Log("Error in InventoryTab_GetItemType: " + ex.Message);
                await ErrorLogDao.HandleException_SQLError_CloseApp(ex, useAsync);
                return "Unknown";
            }
            catch (Exception ex)
            {
                AppLogger.LogDatabaseError(ex);
                AppLogger.Log("Error in InventoryTab_GetItemType: " + ex.Message);
                await ErrorLogDao.HandleException_GeneralError_CloseApp(ex, useAsync);
                return "Unknown";
            }
        }

        internal static async Task<List<Search>?> RemoveTab_DeleteAsync(bool useAsync = false)
        {
            try
            {
                List<Search> returnThese = [];
                var parameters1 = new Dictionary<string, object>
                {
                    ["@RemoveID"] = WipAppVariables.removeId
                };
                await SqlHelper.ExecuteNonQuery(
                    "DELETE FROM saved_locations WHERE `saved_locations`.`ID` = @RemoveID",
                    parameters1, useAsync: useAsync);

                var parameters2 = new Dictionary<string, object>
                {
                    ["@User"] = WipAppVariables.RemoveUser,
                    ["@PartID"] = WipAppVariables.RemovePartNumber,
                    ["@Location"] = WipAppVariables.RemoveLocation,
                    ["@Quantity"] = WipAppVariables.RemoveQuantity
                };
                await SqlHelper.ExecuteNonQuery(
                    "INSERT INTO `output_history` (`User`, `Part ID`, `Location`, `Quantity`) VALUES(@User, @PartID, @Location, @Quantity)",
                    parameters2, useAsync: useAsync);

                AppLogger.Log("RemoveTab_Delete executed successfully for RemoveID: " + WipAppVariables.removeId);
                return returnThese;
            }
            catch (MySqlException ex)
            {
                AppLogger.LogDatabaseError(ex);
                AppLogger.Log("Error in RemoveTab_Delete: " + ex.Message);
                await ErrorLogDao.HandleException_SQLError_CloseApp(ex, useAsync);
                return null;
            }
            catch (Exception ex)
            {
                AppLogger.LogDatabaseError(ex);
                AppLogger.Log("Error in RemoveTab_Delete: " + ex.Message);
                await ErrorLogDao.HandleException_GeneralError_CloseApp(ex, useAsync);
                return null;
            }
        }

        internal static async Task<DataTable> GetAllInventoryAsync(bool useAsync = false)
        {
            try
            {
                return await SqlHelper.ExecuteDataTable("SELECT * FROM `saved_locations` ORDER BY `Date_Time` DESC",
                    useAsync: useAsync);
            }
            catch (MySqlException ex)
            {
                AppLogger.LogDatabaseError(ex);
                await ErrorLogDao.HandleException_SQLError_CloseApp(ex, useAsync);
                return new DataTable();
            }
            catch (Exception ex)
            {
                AppLogger.LogDatabaseError(ex);
                await ErrorLogDao.HandleException_GeneralError_CloseApp(ex, useAsync);
                return new DataTable();
            }
        }

        internal static async Task<DataTable> GetInventoryByPartIdAsync(string partId, bool useAsync = false)
        {
            try
            {
                var parameters = new Dictionary<string, object>
                {
                    ["@PartID"] = partId
                };
                return await SqlHelper.ExecuteDataTable(
                    "SELECT * FROM `saved_locations` WHERE `Item Number` = @PartID ORDER BY `Date_Time` DESC",
                    parameters, useAsync: useAsync);
            }
            catch (MySqlException ex)
            {
                AppLogger.LogDatabaseError(ex);
                await ErrorLogDao.HandleException_SQLError_CloseApp(ex, useAsync);
                return new DataTable();
            }
            catch (Exception ex)
            {
                AppLogger.LogDatabaseError(ex);
                await ErrorLogDao.HandleException_GeneralError_CloseApp(ex, useAsync);
                return new DataTable();
            }
        }

        internal static async Task<DataTable> GetInventoryByLocationAsync(string location, bool useAsync = false)
        {
            try
            {
                var parameters = new Dictionary<string, object>
                {
                    ["@Location"] = location
                };
                return await SqlHelper.ExecuteDataTable(
                    "SELECT * FROM `saved_locations` WHERE `Location` = @Location ORDER BY `Date_Time` DESC",
                    parameters, useAsync: useAsync);
            }
            catch (MySqlException ex)
            {
                AppLogger.LogDatabaseError(ex);
                await ErrorLogDao.HandleException_SQLError_CloseApp(ex, useAsync);
                return new DataTable();
            }
            catch (Exception ex)
            {
                AppLogger.LogDatabaseError(ex);
                await ErrorLogDao.HandleException_GeneralError_CloseApp(ex, useAsync);
                return new DataTable();
            }
        }

        internal static async Task UpdateInventoryQuantityAsync(int id, int newQuantity, bool useAsync = false)
        {
            try
            {
                var parameters = new Dictionary<string, object>
                {
                    ["@Id"] = id,
                    ["@Quantity"] = newQuantity
                };
                await SqlHelper.ExecuteNonQuery(
                    "UPDATE `saved_locations` SET `Quantity` = @Quantity WHERE `ID` = @Id",
                    parameters, useAsync: useAsync);
            }
            catch (MySqlException ex)
            {
                AppLogger.LogDatabaseError(ex);
                await ErrorLogDao.HandleException_SQLError_CloseApp(ex, useAsync);
            }
            catch (Exception ex)
            {
                AppLogger.LogDatabaseError(ex);
                await ErrorLogDao.HandleException_GeneralError_CloseApp(ex, useAsync);
            }
        }

        internal static async Task DeleteInventoryByIdAsync(int id, bool useAsync = false)
        {
            try
            {
                var parameters = new Dictionary<string, object>
                {
                    ["@Id"] = id
                };
                await SqlHelper.ExecuteNonQuery(
                    "DELETE FROM `saved_locations` WHERE `ID` = @Id",
                    parameters, useAsync: useAsync);
            }
            catch (MySqlException ex)
            {
                AppLogger.LogDatabaseError(ex);
                await ErrorLogDao.HandleException_SQLError_CloseApp(ex, useAsync);
            }
            catch (Exception ex)
            {
                AppLogger.LogDatabaseError(ex);
                await ErrorLogDao.HandleException_GeneralError_CloseApp(ex, useAsync);
            }
        }

        internal static async Task<DataTable> GetInventoryByDateRangeAsync(DateTime start, DateTime end,
            bool useAsync = false)
        {
            try
            {
                var parameters = new Dictionary<string, object>
                {
                    ["@Start"] = start,
                    ["@End"] = end
                };
                return await SqlHelper.ExecuteDataTable(
                    "SELECT * FROM `saved_locations` WHERE `Date_Time` BETWEEN @Start AND @End ORDER BY `Date_Time` DESC",
                    parameters, useAsync: useAsync);
            }
            catch (MySqlException ex)
            {
                AppLogger.LogDatabaseError(ex);
                await ErrorLogDao.HandleException_SQLError_CloseApp(ex, useAsync);
                return new DataTable();
            }
            catch (Exception ex)
            {
                AppLogger.LogDatabaseError(ex);
                await ErrorLogDao.HandleException_GeneralError_CloseApp(ex, useAsync);
                return new DataTable();
            }
        }

        internal static async Task<DataTable> GetInventoryByUserAsync(string user, bool useAsync = false)
        {
            try
            {
                var parameters = new Dictionary<string, object>
                {
                    ["@User"] = user
                };
                return await SqlHelper.ExecuteDataTable(
                    "SELECT * FROM `saved_locations` WHERE `User` = @User ORDER BY `Date_Time` DESC",
                    parameters, useAsync: useAsync);
            }
            catch (MySqlException ex)
            {
                AppLogger.LogDatabaseError(ex);
                await ErrorLogDao.HandleException_SQLError_CloseApp(ex, useAsync);
                return new DataTable();
            }
            catch (Exception ex)
            {
                AppLogger.LogDatabaseError(ex);
                await ErrorLogDao.HandleException_GeneralError_CloseApp(ex, useAsync);
                return new DataTable();
            }
        }

        internal static async Task<DataRow?> GetInventoryByIdAsync(int id, bool useAsync = false)
        {
            try
            {
                var parameters = new Dictionary<string, object>
                {
                    ["@Id"] = id
                };
                var table = await SqlHelper.ExecuteDataTable(
                    "SELECT * FROM `saved_locations` WHERE `ID` = @Id",
                    parameters, useAsync: useAsync);
                return table.Rows.Count > 0 ? table.Rows[0] : null;
            }
            catch (MySqlException ex)
            {
                AppLogger.LogDatabaseError(ex);
                await ErrorLogDao.HandleException_SQLError_CloseApp(ex, useAsync);
                return null;
            }
            catch (Exception ex)
            {
                AppLogger.LogDatabaseError(ex);
                await ErrorLogDao.HandleException_GeneralError_CloseApp(ex, useAsync);
                return null;
            }
        }

        internal static async Task UpdateInventoryNotesAsync(int id, string notes, bool useAsync = false)
        {
            try
            {
                var parameters = new Dictionary<string, object>
                {
                    ["@Id"] = id,
                    ["@Notes"] = notes
                };
                await SqlHelper.ExecuteNonQuery(
                    "UPDATE `saved_locations` SET `Notes` = @Notes WHERE `ID` = @Id",
                    parameters, useAsync: useAsync);
            }
            catch (MySqlException ex)
            {
                AppLogger.LogDatabaseError(ex);
                await ErrorLogDao.HandleException_SQLError_CloseApp(ex, useAsync);
            }
            catch (Exception ex)
            {
                AppLogger.LogDatabaseError(ex);
                await ErrorLogDao.HandleException_GeneralError_CloseApp(ex, useAsync);
            }
        }

        internal static async Task BulkDeleteInventoryByLocationAsync(string location, bool useAsync = false)
        {
            try
            {
                var parameters = new Dictionary<string, object>
                {
                    ["@Location"] = location
                };
                await SqlHelper.ExecuteNonQuery(
                    "DELETE FROM `saved_locations` WHERE `Location` = @Location",
                    parameters, useAsync: useAsync);
            }
            catch (MySqlException ex)
            {
                AppLogger.LogDatabaseError(ex);
                await ErrorLogDao.HandleException_SQLError_CloseApp(ex, useAsync);
            }
            catch (Exception ex)
            {
                AppLogger.LogDatabaseError(ex);
                await ErrorLogDao.HandleException_GeneralError_CloseApp(ex, useAsync);
            }
        }

        internal static async Task<int> GetTotalQuantityByPartIdAsync(string partId, bool useAsync = false)
        {
            try
            {
                var parameters = new Dictionary<string, object>
                {
                    ["@PartID"] = partId
                };
                var result = await SqlHelper.ExecuteScalar(
                    "SELECT SUM(`Quantity`) FROM `saved_locations` WHERE `Item Number` = @PartID",
                    parameters, useAsync: useAsync);
                return result != DBNull.Value && result != null ? Convert.ToInt32(result) : 0;
            }
            catch (MySqlException ex)
            {
                AppLogger.LogDatabaseError(ex);
                await ErrorLogDao.HandleException_SQLError_CloseApp(ex, useAsync);
                return 0;
            }
            catch (Exception ex)
            {
                AppLogger.LogDatabaseError(ex);
                await ErrorLogDao.HandleException_GeneralError_CloseApp(ex, useAsync);
                return 0;
            }
        }
    }


    internal static class SystemDao
    {
        // =====================
        // SystemDao
        // =====================


        internal static string System_GetUserName()
        {
            if (Program.enteredUser == "Default User")
            {
                var userIdWithDomain = WindowsIdentity.GetCurrent().Name;
                var posSlash = userIdWithDomain.IndexOf('\\');
                var user = (posSlash == -1 ? userIdWithDomain : userIdWithDomain[(posSlash + 1)..]).ToUpper();
                WipAppVariables.User = user; // Keep WipAppVariables in sync
                return user;
            }
            else
            {
                var userIdWithDomain = Program.enteredUser;
                if (userIdWithDomain == null)
                {
                    throw new InvalidOperationException("User identity could not be retrieved.");
                }

                var posSlash = userIdWithDomain.IndexOf('\\');
                var user = (posSlash == -1 ? userIdWithDomain : userIdWithDomain[(posSlash + 1)..]).ToUpper();
                WipAppVariables.User = user; // Keep WipAppVariables in sync
                return user;
            }
        }

        internal static async Task<List<AdminList>> System_UserAccessTypeAsync(bool useAsync = false)
        {
            var user = WipAppVariables.User;
            List<AdminList> returnThese = [];
            try
            {
                // Reset before checking
                WipAppVariables.userTypeAdmin = false;
                WipAppVariables.userTypeReadOnly = false;

                // Admin check
                using (var reader = useAsync
                           ? await SqlHelper.ExecuteReader("SELECT * FROM `leads`", useAsync: true)
                           : SqlHelper.ExecuteReader("SELECT * FROM `leads`").Result)
                {
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
                        }

                        returnThese.Add(a);
                    }
                }

                // ReadOnly check
                using (var reader2 = useAsync
                           ? await SqlHelper.ExecuteReader("SELECT * FROM `readonly`", useAsync: true)
                           : SqlHelper.ExecuteReader("SELECT * FROM `readonly`").Result)
                {
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
                        }

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
                await ErrorLogDao.HandleException_SQLError_CloseApp(ex, useAsync);
                return [];
            }
            catch (Exception ex)
            {
                AppLogger.LogDatabaseError(ex);
                AppLogger.Log("Error in System_UserAccessType: " + ex.Message);
                await ErrorLogDao.HandleException_GeneralError_CloseApp(ex, useAsync);
                return [];
            }
        }

        internal static async Task SetUserAccessTypeAsync(string email, string accessType, bool useAsync = false)
        {
            try
            {
                // Remove from both tables first
                var parameters = new Dictionary<string, object> { ["@Email"] = email };
                await SqlHelper.ExecuteNonQuery("DELETE FROM `leads` WHERE `User` = @Email", parameters,
                    useAsync: useAsync);
                await SqlHelper.ExecuteNonQuery("DELETE FROM `readonly` WHERE `User` = @Email", parameters,
                    useAsync: useAsync);

                if (accessType == "Admin")
                {
                    await SqlHelper.ExecuteNonQuery("INSERT INTO `leads` (`User`) VALUES (@Email)", parameters,
                        useAsync: useAsync);
                }
                else if (accessType == "ReadOnly")
                {
                    await SqlHelper.ExecuteNonQuery("INSERT INTO `readonly` (`User`) VALUES (@Email)", parameters,
                        useAsync: useAsync);
                }

                // Update WipAppVariables if the affected user is the current user
                if (WipAppVariables.User == email)
                {
                    WipAppVariables.userTypeAdmin = accessType == "Admin";
                    WipAppVariables.userTypeReadOnly = accessType == "ReadOnly";
                }
            }
            catch (MySqlException ex)
            {
                AppLogger.LogDatabaseError(ex);
                await ErrorLogDao.HandleException_SQLError_CloseApp(ex, useAsync);
            }
            catch (Exception ex)
            {
                AppLogger.LogDatabaseError(ex);
                await ErrorLogDao.HandleException_GeneralError_CloseApp(ex, useAsync);
            }
        }

        internal static async Task System_Last10_Buttons_ChangedAsync(bool useAsync = false)
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

                await SqlHelper.ExecuteNonQuery(com1 + com2 + com3 + com4 + com5 + com6 + com7 + com8,
                    useAsync: useAsync);

                AppLogger.Log("System_Last10_Buttons_Changed executed successfully.");
            }
            catch (MySqlException ex)
            {
                AppLogger.LogDatabaseError(ex);
                AppLogger.Log("Error in System_Last10_Buttons_Changed: " + ex.Message);
                await ErrorLogDao.HandleException_SQLError_CloseApp(ex, useAsync);
            }
            catch (Exception ex)
            {
                AppLogger.LogDatabaseError(ex);
                AppLogger.Log("Error in System_Last10_Buttons_Changed: " + ex.Message);
                await ErrorLogDao.HandleException_GeneralError_CloseApp(ex, useAsync);
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
    }

    internal static class ChangeLogDao
    {
        internal static async Task<string> GetWipServerAddressAsync(string user, bool useAsync = false)
        {
            try
            {
                var parameters = new Dictionary<string, object>
                {
                    ["@User"] = user
                };
                var result = await SqlHelper.ExecuteScalar(
                    "SELECT `WipServerAddress` FROM `users` WHERE `User` = @User",
                    parameters, useAsync: useAsync);
                return result?.ToString() ?? string.Empty;
            }
            catch (Exception ex)
            {
                AppLogger.LogDatabaseError(ex);
                await ErrorLogDao.HandleException_GeneralError_CloseApp(ex, useAsync);
                return string.Empty;
            }
        }

        internal static async Task<string> GetWipServerPortAsync(string user, bool useAsync = false)
        {
            try
            {
                var parameters = new Dictionary<string, object>
                {
                    ["@User"] = user
                };
                var result = await SqlHelper.ExecuteScalar(
                    "SELECT `WipServerPort` FROM `users` WHERE `User` = @User",
                    parameters, useAsync: useAsync);
                return result?.ToString() ?? string.Empty;
            }
            catch (Exception ex)
            {
                AppLogger.LogDatabaseError(ex);
                await ErrorLogDao.HandleException_GeneralError_CloseApp(ex, useAsync);
                return string.Empty;
            }
        }

        internal static async Task SetWipServerAddressAsync(string user, string address, bool useAsync = false)
        {
            var parameters = new Dictionary<string, object>
            {
                ["@User"] = user,
                ["@WipServerAddress"] = address
            };
            await SqlHelper.ExecuteNonQuery(
                "UPDATE `users` SET `WipServerAddress` = @WipServerAddress WHERE `User` = @User",
                parameters, useAsync: useAsync);
        }

        internal static async Task SetWipServerPortAsync(string user, string port, bool useAsync = false)
        {
            var parameters = new Dictionary<string, object>
            {
                ["@User"] = user,
                ["@WipServerPort"] = port
            };
            await SqlHelper.ExecuteNonQuery(
                "UPDATE `users` SET `WipServerPort` = @WipServerPort WHERE `User` = @User",
                parameters, useAsync: useAsync);
        }

        internal static async Task Primary_ChangeLog_Set_Theme_FontSizeAsync(string value, string user,
            bool useAsync = false)
        {
            var parameters = new Dictionary<string, object>
            {
                ["@Theme_FontSize"] = value,
                ["@User"] = user
            };
            await SqlHelper.ExecuteNonQuery(
                "UPDATE `users` SET `Theme_FontSize` = @Theme_FontSize WHERE `User` = @User",
                parameters, useAsync: useAsync);
        }

        internal static async Task Primary_ChangeLog_Set_Theme_NameAsync(string value, string user,
            bool useAsync = false)
        {
            var parameters = new Dictionary<string, object>
            {
                ["@Theme_Name"] = value,
                ["@User"] = user
            };
            await SqlHelper.ExecuteNonQuery(
                "UPDATE `users` SET `Theme_Name` = @Theme_Name WHERE `User` = @User",
                parameters, useAsync: useAsync);
        }

        internal static async Task<string> Primary_ChangeLog_Get_Theme_NameAsync(bool useAsync = false)
        {
            try
            {
                var parameters = new Dictionary<string, object>
                {
                    ["@User"] = WipAppVariables.User
                };
                var result = await SqlHelper.ExecuteScalar(
                    "SELECT `Theme_Name` FROM `users` WHERE `User` = @User",
                    parameters, useAsync: useAsync);
                return result?.ToString() ?? string.Empty;
            }
            catch (Exception ex)
            {
                AppLogger.LogDatabaseError(ex);
                await ErrorLogDao.HandleException_GeneralError_CloseApp(ex, useAsync);
                return string.Empty;
            }
        }

        internal static async Task<int?> Primary_ChangeLog_Get_Visual_Theme_FontSizeAsync(bool useAsync = false)
        {
            try
            {
                var parameters = new Dictionary<string, object>
                {
                    ["@User"] = WipAppVariables.User
                };
                var result = await SqlHelper.ExecuteScalar(
                    "SELECT `Theme_FontSize` FROM `users` WHERE `User` = @User",
                    parameters, useAsync: useAsync);
                return result != null && int.TryParse(result.ToString(), out var size) ? size : null;
            }
            catch (Exception ex)
            {
                AppLogger.LogDatabaseError(ex);
                await ErrorLogDao.HandleException_GeneralError_CloseApp(ex, useAsync);
                return null;
            }
        }

        internal static async Task<List<string>> Primary_ChangeLog_Get_AllVersionsAsync(bool useAsync = false)
        {
            var versions = new List<string>();
            try
            {
                using var reader = useAsync
                    ? await SqlHelper.ExecuteReader("SELECT `Version` FROM `changelog` ORDER BY `Version` DESC",
                        useAsync: true)
                    : SqlHelper.ExecuteReader("SELECT `Version` FROM `changelog` ORDER BY `Version` DESC").Result;
                while (reader.Read())
                {
                    versions.Add(reader.GetString("Version"));
                }
            }
            catch (Exception ex)
            {
                AppLogger.LogDatabaseError(ex);
                await ErrorLogDao.HandleException_GeneralError_CloseApp(ex, useAsync);
            }

            return versions;
        }

        internal static async Task Primary_ChangeLog_Set_LastShownAsync(string value, string user,
            bool useAsync = false)
        {
            var parameters = new Dictionary<string, object>
            {
                ["@LastShown"] = value,
                ["@User"] = user
            };
            await SqlHelper.ExecuteNonQuery(
                "UPDATE `users` SET `LastShownVersion` = @LastShown WHERE `User` = @User",
                parameters, useAsync: useAsync);
        }

        internal static async Task Primary_ChangeLog_Set_SwitchAsync(string value, string user, bool useAsync = false)
        {
            var parameters = new Dictionary<string, object>
            {
                ["@HideChangeLog"] = value,
                ["@User"] = user
            };
            await SqlHelper.ExecuteNonQuery(
                "UPDATE `users` SET `HideChangeLog` = @HideChangeLog WHERE `User` = @User",
                parameters, useAsync: useAsync);
        }

        internal static async Task Primary_ChangeLog_Set_VersionNotesAsync(string rtfNotes, string version,
            bool useAsync = false)
        {
            var parameters = new Dictionary<string, object>
            {
                ["@Notes"] = rtfNotes,
                ["@Version"] = version
            };
            await SqlHelper.ExecuteNonQuery(
                "UPDATE `changelog` SET `Notes` = @Notes WHERE `Version` = @Version",
                parameters, useAsync: useAsync);
        }

        internal static async Task<string> Primary_ChangeLog_Get_Visual_UserAsync(bool useAsync = false)
        {
            try
            {
                var parameters = new Dictionary<string, object>
                {
                    ["@User"] = WipAppVariables.User
                };
                var result = await SqlHelper.ExecuteScalar(
                    "SELECT `VisualUserName` FROM `users` WHERE `User` = @User",
                    parameters, useAsync: useAsync);
                return result?.ToString() ?? string.Empty;
            }
            catch (Exception ex)
            {
                AppLogger.LogDatabaseError(ex);
                await ErrorLogDao.HandleException_GeneralError_CloseApp(ex, useAsync);
                return string.Empty;
            }
        }


        internal static async Task<string> Primary_ChangeLog_Get_Visual_PasswordAsync(bool useAsync = false)
        {
            try
            {
                var parameters = new Dictionary<string, object>
                {
                    ["@User"] = WipAppVariables.User
                };
                var result = await SqlHelper.ExecuteScalar(
                    "SELECT `VisualPassword` FROM `users` WHERE `User` = @User",
                    parameters, useAsync: useAsync);
                return result?.ToString() ?? string.Empty;
            }
            catch (Exception ex)
            {
                AppLogger.LogDatabaseError(ex);
                await ErrorLogDao.HandleException_GeneralError_CloseApp(ex, useAsync);
                return string.Empty;
            }
        }


        internal static async Task Primary_ChangeLog_Set_Visual_PasswordAsync(string value, string user,
            bool useAsync = false)
        {
            var parameters = new Dictionary<string, object>
            {
                ["@VisualPassword"] = value,
                ["@User"] = user
            };
            await SqlHelper.ExecuteNonQuery(
                "UPDATE `users` SET `VisualPassword` = @VisualPassword WHERE `User` = @User",
                parameters, useAsync: useAsync);
        }

        internal static async Task Primary_ChangeLog_Set_Visual_UserAsync(string value, string user,
            bool useAsync = false)
        {
            var parameters = new Dictionary<string, object>
            {
                ["@VisualUserName"] = value,
                ["@User"] = user
            };
            await SqlHelper.ExecuteNonQuery(
                "UPDATE `users` SET `VisualUserName` = @VisualUserName WHERE `User` = @User",
                parameters, useAsync: useAsync);
        }

        internal static async Task<string> Primary_ChangeLog_Get_ToggleAsync(bool useAsync = false)
        {
            try
            {
                var parameters = new Dictionary<string, object>
                {
                    ["@User"] = WipAppVariables.User
                };
                var result = await SqlHelper.ExecuteScalar(
                    "SELECT `HideChangeLog` FROM `users` WHERE `User` = @User",
                    parameters, useAsync: useAsync);
                return result?.ToString() ?? string.Empty;
            }
            catch (Exception ex)
            {
                AppLogger.LogDatabaseError(ex);
                await ErrorLogDao.HandleException_GeneralError_CloseApp(ex, useAsync);
                return string.Empty;
            }
        }

        internal static async Task<string> Primary_ChangeLog_Get_LastShownAsync(bool useAsync = false)
        {
            try
            {
                var parameters = new Dictionary<string, object>
                {
                    ["@User"] = WipAppVariables.User
                };
                var result = await SqlHelper.ExecuteScalar(
                    "SELECT `LastShownVersion` FROM `users` WHERE `User` = @User",
                    parameters, useAsync: useAsync);
                return result?.ToString() ?? string.Empty;
            }
            catch (Exception ex)
            {
                AppLogger.LogDatabaseError(ex);
                await ErrorLogDao.HandleException_GeneralError_CloseApp(ex, useAsync);
                return string.Empty;
            }
        }

        internal static async Task<string> Primary_ChangeLog_Get_VersionNotesAsync(string? version = null,
            bool useAsync = false)
        {
            try
            {
                var parameters = new Dictionary<string, object>
                {
                    ["@Version"] = version ?? string.Empty
                };
                var result = await SqlHelper.ExecuteScalar(
                    "SELECT `Notes` FROM `changelog` WHERE `Version` = @Version",
                    parameters, useAsync: useAsync);
                return result?.ToString() ?? string.Empty;
            }
            catch (Exception ex)
            {
                AppLogger.LogDatabaseError(ex);
                await ErrorLogDao.HandleException_GeneralError_CloseApp(ex, useAsync);
                return string.Empty;
            }
        }

        internal static async Task<DataTable> GetAllChangeLogEntriesAsync(bool useAsync = false)
        {
            try
            {
                return await SqlHelper.ExecuteDataTable("SELECT * FROM `changelog` ORDER BY `Version` DESC",
                    useAsync: useAsync);
            }
            catch (Exception ex)
            {
                AppLogger.LogDatabaseError(ex);
                await ErrorLogDao.HandleException_GeneralError_CloseApp(ex, useAsync);
                return new DataTable();
            }
        }

        internal static async Task<DataRow?> GetChangeLogEntryByVersionAsync(string version, bool useAsync = false)
        {
            try
            {
                var parameters = new Dictionary<string, object>
                {
                    ["@Version"] = version
                };
                var table = await SqlHelper.ExecuteDataTable(
                    "SELECT * FROM `changelog` WHERE `Version` = @Version",
                    parameters, useAsync: useAsync);
                return table.Rows.Count > 0 ? table.Rows[0] : null;
            }
            catch (Exception ex)
            {
                AppLogger.LogDatabaseError(ex);
                await ErrorLogDao.HandleException_GeneralError_CloseApp(ex, useAsync);
                return null;
            }
        }

        internal static async Task InsertChangeLogEntryAsync(string version, string notes, bool useAsync = false)
        {
            try
            {
                var parameters = new Dictionary<string, object>
                {
                    ["@Version"] = version,
                    ["@Notes"] = notes
                };
                await SqlHelper.ExecuteNonQuery(
                    "INSERT INTO `changelog` (`Version`, `Notes`) VALUES (@Version, @Notes)",
                    parameters, useAsync: useAsync);
            }
            catch (Exception ex)
            {
                AppLogger.LogDatabaseError(ex);
                await ErrorLogDao.HandleException_GeneralError_CloseApp(ex, useAsync);
            }
        }

        internal static async Task DeleteChangeLogEntryAsync(string version, bool useAsync = false)
        {
            try
            {
                var parameters = new Dictionary<string, object>
                {
                    ["@Version"] = version
                };
                await SqlHelper.ExecuteNonQuery(
                    "DELETE FROM `changelog` WHERE `Version` = @Version",
                    parameters, useAsync: useAsync);
            }
            catch (Exception ex)
            {
                AppLogger.LogDatabaseError(ex);
                await ErrorLogDao.HandleException_GeneralError_CloseApp(ex, useAsync);
            }
        }
    }
}