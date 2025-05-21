using MTM_WIP_App.Main_Form;
using MySql.Data.MySqlClient;
using System.Data;
using Unified_Entry_Form_UnifiedEntryForm = MTM_WIP_App.Database_Maint.Add_Objects.Unified_Entry_Form.UnifiedEntryForm;

namespace MTM_WIP_App
{
    using UnifiedEntryForm = Unified_Entry_Form_UnifiedEntryForm;

    public partial class LoginPromptForm : Form
    {
        private readonly string _connectionString = SqlVariables.GetConnectionString(null, null, null, null);

        public LoginPromptForm()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private async void LoginPromptForm_Load(object sender, EventArgs e)
        {
            try
            {
                // Get users from UserDao
                var table = await UserDao.GetVitsUsers();

                // Add a default row for the dropdown
                var defaultRow = table.NewRow();
                defaultRow["User"] = "[Select Username]";
                table.Rows.InsertAt(defaultRow, 0);

                // Bind the filtered data to the ComboBox
                UsernameComboBox.DataSource = table;
                UsernameComboBox.DisplayMember = "User";
                UsernameComboBox.ValueMember = "User";
            }
            catch (Exception ex)
            {
                MessageBox.Show(@"Error loading usernames: " + ex.Message, @"Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private async void LoginButton_Click(object sender, EventArgs e)
        {
            if (UsernameComboBox.SelectedIndex <= 0 || string.IsNullOrWhiteSpace(PinTextBox.Text))
            {
                MessageBox.Show(@"Please select a username and enter a PIN.", @"Validation Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Validate the username and PIN using UserDao
                var isValid = await UserDao.ValidateUserPin(UsernameComboBox.Text, PinTextBox.Text);

                if (isValid)
                {
                    Program.enteredUser = UsernameComboBox.Text;
                    WipAppVariables.User = SystemDao.System_GetUserName();

                    MessageBox.Show(@"Login successful!", @"Success", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    DialogResult = DialogResult.OK;
                    await SystemDao.System_UserAccessTypeAsync();
                    Close();
                }
                else
                {
                    MessageBox.Show(@"Invalid username or PIN.", @"Login Failed", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(@"Error validating login: " + ex.Message, @"Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void NewUserButton_Click(object sender, EventArgs e)
        {
            // Pass true to indicate the form is opened from LoginPromptForm
            using var unifiedEntryForm = new UnifiedEntryForm(true);
            if (unifiedEntryForm.ShowDialog() == DialogResult.OK) // Check if SaveButton_Click was used
            {
                try
                {
                    // Reset the ComboBox data
                    UsernameComboBox.DataSource = null;
                    UsernameComboBox.Items.Clear();

                    // Rerun the LoginPromptForm_Load method to refresh the username dropdown
                    LoginPromptForm_Load(null, null);

                    // Retrieve the new user data from UnifiedEntryForm
                    var newUser = unifiedEntryForm.NewUser;
                    var newPin = unifiedEntryForm.NewUserPin;

                    if (!string.IsNullOrEmpty(newUser))
                    {
                        // Update the ComboBox and PIN field with the new user data
                        UsernameComboBox.SelectedValue = newUser;
                        PinTextBox.Text = newPin;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(@"Error updating login fields: " + ex.Message, @"Error", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
        }
    }
}