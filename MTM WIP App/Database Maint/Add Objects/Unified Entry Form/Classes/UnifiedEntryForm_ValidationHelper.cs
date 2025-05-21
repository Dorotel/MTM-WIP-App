using System.Text.RegularExpressions;
using MTM_WIP_App.Main_Form;

namespace MTM_WIP_App.Database_Maint.Add_Objects.Unified_Entry_Form.Classes
{
    public static class UnifiedEntryForm_ValidationHelper
    {
        public static bool UserValidation(ComboBox objectTypeComboBox, TextBox userFirstName, TextBox userLastName,
            TextBox userEmail, TextBox userPin, ComboBox shiftComboBox)
        {
            if (string.IsNullOrWhiteSpace(userFirstName.Text) ||
                string.IsNullOrWhiteSpace(userLastName.Text) ||
                string.IsNullOrWhiteSpace(userEmail.Text) ||
                shiftComboBox.SelectedIndex == 0)
            {
                MessageBox.Show(@"Please fill out all required fields and select a valid shift.", @"Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(userPin.Text) || Regex.IsMatch(userPin.Text, @"^\d{4}$"))
            {
                return true;
            }

            MessageBox.Show(@"Please enter a valid 4-digit PIN or leave it empty.", @"Validation Error",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return false;
        }

        public static bool PartValidation(TextBox partInput, ComboBox partTypeComboBox)
        {
            if (string.IsNullOrWhiteSpace(partInput.Text))
            {
                MessageBox.Show(@"Please enter a valid Part Number.", @"Validation Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                partInput.Focus();
                return false;
            }

            if (partTypeComboBox.SelectedIndex <= 0 || string.IsNullOrWhiteSpace(partTypeComboBox.Text))
            {
                MessageBox.Show(@"Please select a valid Part Type.", @"Validation Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                partTypeComboBox.Focus();
                return false;
            }

            return true;
        }

        public static bool OperationValidation(TextBox opInput)
        {
            if (string.IsNullOrWhiteSpace(opInput.Text))
            {
                MessageBox.Show(@"Please enter a valid Operation Number.", @"Validation Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                opInput.Focus();
                return false;
            }

            return true;
        }

        public static bool PartTypeValidation(TextBox partTypeInput)
        {
            if (string.IsNullOrWhiteSpace(partTypeInput.Text))
            {
                MessageBox.Show(@"Please enter a valid Part Type.", @"Validation Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                partTypeInput.Focus();
                return false;
            }

            return true;
        }

        public static bool LocationValidation(TextBox locationInput, string location)
        {
            if (!string.IsNullOrWhiteSpace(location))
            {
                return true;
            }

            MessageBox.Show(@"Please enter a valid Location.", @"Validation Error", MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            locationInput.Focus();
            return false;
        }

        public static void ValidateSingleDigitInput(object? sender, EventArgs e)
        {
            try
            {
                if (sender is TextBox textBox)
                {
                    if (!string.IsNullOrEmpty(textBox.Text) &&
                        (!int.TryParse(textBox.Text, out var value) || value < 0 || value > 9))
                    {
                        textBox.Text = string.Empty;
                    }
                }
            }
            catch (Exception ex)
            {
                AppLogger.Log($@"Error in ValidateSingleDigitInput: {ex.Message}");
                MessageBox.Show($@"An error occurred while validating input:
{ex.Message}", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}