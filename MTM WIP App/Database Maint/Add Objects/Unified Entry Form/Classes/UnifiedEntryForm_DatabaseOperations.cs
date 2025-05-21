using MTM_WIP_App.Main_Form;
using MySql.Data.MySqlClient;

namespace MTM_WIP_App.Database_Maint.Add_Objects.Unified_Entry_Form.Classes
{
    public static class UnifiedEntryForm_DatabaseOperations
    {
        public static int TotalLocations { get; private set; }
        public static int SuccessfulLocations { get; private set; }
        public static int FailedLocations { get; private set; }
        public static List<string> FailedLocationDetails { get; private set; } = [];

        public static void ResetCounters()
        {
            TotalLocations = 0;
            SuccessfulLocations = 0;
            FailedLocations = 0;
            FailedLocationDetails.Clear();
        }

        public static void SaveEntry(ComboBox objectTypeComboBox, string connectionString, TextBox userFirstName,
            TextBox userLastName, TextBox userEmail, TextBox userPin, ComboBox shiftComboBox, CheckBox vitsCheckBox,
            CheckBox adminCheckBox, CheckBox readOnlyCheckBox, TextBox partInput, ComboBox partTypeComboBox,
            TextBox partTypeInput, TextBox opInput, TextBox locationInput, bool multiple)
        {
            switch (objectTypeComboBox.SelectedItem.ToString())
            {
                case "User":
                    SaveUser(connectionString, userFirstName, userLastName, userEmail, userPin, shiftComboBox,
                        vitsCheckBox, adminCheckBox, readOnlyCheckBox);
                    break;
                case "Part":
                    SavePart(connectionString, partInput, partTypeComboBox);
                    break;
                case "Part Type":
                    SavePartType(connectionString, partTypeInput);
                    break;
                case "Operation":
                    SaveOperation(connectionString, opInput);
                    break;
                case "Location":
                    SaveLocation(connectionString, locationInput, multiple);
                    break;
            }
        }

        private static void SaveUser(string connectionString, TextBox userFirstName, TextBox userLastName,
            TextBox userEmail, TextBox userPin, ComboBox shiftComboBox, CheckBox vitsCheckBox, CheckBox adminCheckBox,
            CheckBox readOnlyCheckBox)
        {
            try
            {
                // Prepare user data
                var fullName = $"{userFirstName.Text} {userLastName.Text}".Trim();
                var email = userEmail.Text.ToUpper();
                var shift = shiftComboBox.Text;
                var isVitsUser = vitsCheckBox.Checked ? "1" : "0";
                var pin = string.IsNullOrWhiteSpace(userPin.Text) ? null : userPin.Text;

                // Validate required fields
                if (string.IsNullOrWhiteSpace(fullName) || string.IsNullOrWhiteSpace(email) ||
                    string.IsNullOrWhiteSpace(shift))
                {
                    MessageBox.Show(@"Please fill out all required fields.", @"Validation Error", MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                // Check if PIN is required and validate
                if (vitsCheckBox.Checked && string.IsNullOrWhiteSpace(pin))
                {
                    MessageBox.Show(@"Please enter a 4-digit PIN.", @"Validation Error", MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    userPin.Focus();
                    return;
                }

                var userExists = SearchDao.UserExists(email);

                if (userExists)
                {
                    // Prompt to overwrite
                    var result = MessageBox.Show($@"The user {email} already exists. Do you want to overwrite?",
                        @"User Exists", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.No)
                    {
                        return;
                    }

                    SearchDao.UpdateUser(email, fullName, shift, isVitsUser, pin);
                }
                else
                {
                    SearchDao.InsertUser(email, fullName, shift, isVitsUser, pin);
                }

                SearchDao.SetUserAdminStatus(email, adminCheckBox.Checked);
                SearchDao.SetUserReadOnlyStatus(email, readOnlyCheckBox.Checked);
                SearchDao.AddUserToMySqlServer(email);

                AppLogger.Log($"User saved: {email}");
                MessageBox.Show(@"User saved successfully!", @"Success", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                AppLogger.Log($"Error in SaveUser: {ex.Message}");
                MessageBox.Show(@$"Error saving user:\n{ex.Message}", @"Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private static void SavePart(string connectionString, TextBox partInput, ComboBox partTypeComboBox)
        {
            try
            {
                // Get the part number and type
                var partNumber = partInput.Text.Trim().ToUpper();
                var partType = partTypeComboBox.Text;

                // Validate input
                if (string.IsNullOrWhiteSpace(partNumber) || partTypeComboBox.SelectedIndex <= 0)
                {
                    MessageBox.Show(@"Please enter a valid Part Number and select a Part Type.", @"Validation Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    partInput.Focus();
                    return;
                }

                var partExists = SearchDao.PartExists(partNumber);

                if (partExists)
                {
                    MessageBox.Show(
                        @$"The Part ID [{partNumber}] already exists! If you're having issues, try restarting the app.",
                        @"Duplicate Part", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    partInput.ForeColor = Color.Red;
                    partInput.Focus();
                }
                else
                {
                    SearchDao.InsertPart(partNumber, WipAppVariables.User, partType);

                    MessageBox.Show(
                        @$"New Part ID [{partNumber}] entered successfully! You can add another or close the form.",
                        @"Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    partInput.ForeColor = Color.Black;
                    partInput.Clear();
                    partInput.Focus();
                }
            }
            catch (MySqlException ex)
            {
                AppLogger.LogDatabaseError(ex);
                MessageBox.Show(@$"Database error while saving the part:\n{ex.Message}", @"Database Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                AppLogger.Log($"Error in SavePart: {ex.Message}");
                MessageBox.Show(@$"An error occurred while saving the part:\n{ex.Message}", @"Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static void SavePartType(string connectionString, TextBox partTypeInput)
        {
            try
            {
                // Get the part type
                var partType = partTypeInput.Text.Trim();

                // Validate input
                if (string.IsNullOrWhiteSpace(partType))
                {
                    MessageBox.Show(@"Please enter a valid Part Type.", @"Validation Error", MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    partTypeInput.Focus();
                    return;
                }

                var partTypeExists = SearchDao.PartTypeExists(partType);

                if (partTypeExists)
                {
                    MessageBox.Show(
                        @$"The Part Type [{partType}] already exists! If you're having issues, try restarting the app.",
                        @"Duplicate Part Type", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    partTypeInput.ForeColor = Color.Red;
                    partTypeInput.Focus();
                }
                else
                {
                    SearchDao.InsertPartType(partType, WipAppVariables.User);

                    MessageBox.Show(
                        @$"New Part Type [{partType}] entered successfully! You can add another or close the form.",
                        @"Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    partTypeInput.ForeColor = Color.Black;
                    partTypeInput.Clear();
                    partTypeInput.Focus();
                }
            }
            catch (MySqlException ex)
            {
                AppLogger.LogDatabaseError(ex);
                MessageBox.Show(@$"Database error while saving the part type:\n{ex.Message}", @"Database Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                AppLogger.Log($"Error in SavePartType: {ex.Message}");
                MessageBox.Show(@$"An error occurred while saving the part type:\n{ex.Message}", @"Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static void SaveOperation(string connectionString, TextBox opInput)
        {
            try
            {
                // Get the operation number
                var operationNumber = opInput.Text.Trim().ToUpper();

                // Validate input
                if (string.IsNullOrWhiteSpace(operationNumber))
                {
                    MessageBox.Show(@"Please enter a valid Operation Number.", @"Validation Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    opInput.Focus();
                    return;
                }

                var operationExists = SearchDao.OperationExists(operationNumber);

                if (operationExists)
                {
                    MessageBox.Show(
                        @$"The Operation Number [{operationNumber}] already exists! If you're having issues, try restarting the app.",
                        @"Duplicate Operation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    opInput.ForeColor = Color.Red;
                    opInput.Focus();
                }
                else
                {
                    SearchDao.InsertOperation(operationNumber, WipAppVariables.User);

                    MessageBox.Show(
                        @$"New Operation Number [{operationNumber}] entered successfully! You can add another or close the form.",
                        @"Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    opInput.ForeColor = Color.Black;
                    opInput.Clear();
                    opInput.Focus();
                }
            }
            catch (MySqlException ex)
            {
                AppLogger.LogDatabaseError(ex);
                MessageBox.Show(@$"Database error while saving the operation:\n{ex.Message}", @"Database Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                AppLogger.Log($"Error in SaveOperation: {ex.Message}");
                MessageBox.Show($@"An error occurred while saving the operation:\n{ex.Message}", @"Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static void SaveLocation(string connectionString, TextBox locationInput, bool multiple)
        {
            try
            {
                TotalLocations++; // Increment TotalLocations for every location processed

                var location = locationInput.Text.Trim().ToUpper();

                // Validate input
                if (string.IsNullOrWhiteSpace(location))
                {
                    FailedLocations++;
                    FailedLocationDetails.Add($"Invalid input: {locationInput.Text}");
                    return;
                }

                var locationExists = SearchDao.LocationExists(location);

                if (locationExists)
                {
                    FailedLocations++;
                    FailedLocationDetails.Add($"Duplicate: {location}");
                }
                else
                {
                    SearchDao.InsertLocation(location, WipAppVariables.User);
                    SuccessfulLocations++;
                }
            }
            catch (Exception ex)
            {
                FailedLocations++;
                FailedLocationDetails.Add($"Error: {locationInput.Text} - {ex.Message}");
                AppLogger.Log($"Error in SaveLocation: {ex.Message}");
            }
        }
    }
}