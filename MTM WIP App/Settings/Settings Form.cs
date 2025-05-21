using System.Globalization;
using MTM_WIP_App.Main_Form;

namespace MTM_WIP_App.Settings
{
    public sealed partial class SettingsForm : Form
    {
        public static bool settingsChanged;

        public SettingsForm()
        {
            var dpi = DeviceDpi;

            if (dpi == 120)
            {
                Font = new Font(Font.FontFamily, 7.25f);
            }
            else if (dpi == 144)
            {
                Font = new Font(Font.FontFamily, 6f);
            }
            else if (dpi == 192)
            {
                Font = new Font(Font.FontFamily, 4.75f);
            }

            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            var adminpriv = WipAppVariables.userTypeAdmin ? "Admin" : "User";

            CheckDefaults();
            CheckAdmin(adminpriv);
            _ = new DgvDesigner();
            DgvDesigner.InitializeDataGridView(ResultsTheme_DataGridView_Preview, null);
            ResultsTheme_ComboBox_Theme.SelectedIndex = 0;
            ResultsTheme_ComboBox_Theme.ForeColor = Color.Red;
            ResultsTheme_Button_Save.Enabled = false;
            ResultsTheme_DataGridView_Preview.Rows.Add("R1 C1", "R1 C2");
            ResultsTheme_DataGridView_Preview.Rows.Add("R2 C1", "R2 C2");
            ResultsTheme_DataGridView_Preview.Rows.Add("R3 C1", "R3 C2");
            ResultsTheme_DataGridView_Preview.Rows.Add("R4 C1", "R4 C2");
            ResultsTheme_TextBox_CurrentTheme.Text = WipAppVariables.WipDataGridTheme;
            ServerSettings_TextBox_ServerAddress.Text = WipAppVariables.WipServerAddress;
            VisualLogin_TextBox_UserName.Text = WipAppVariables.VisualUserName;
            VisualLogin_TextBox_Password.Text = WipAppVariables.VisualPassword;
            ServerSettings_TextBox_Port.Text = WipAppVariables.WipServerPort;

            var user = string.Format("{0}", WipAppVariables.User);
            ServerSettings_TextBox_User.Text = user + @" | " + adminpriv;
            AppLogger.Log("SettingsForm loaded.");
        }

        private void CheckAdmin(string adminpriv)
        {
            SettingsForm_GroupBox_ServerSettings.Enabled = adminpriv == "Admin";
            AppLogger.Log("Admin check completed. Admin privileges: " + adminpriv);
        }

        private void CheckDefaults()
        {
            if (WipAppVariables.WipServerAddress == "")
            {
                ServerSettings_TextBox_ServerAddress.Text = @"172.16.1.104";
                ServerSettings_TextBox_Port.Text = @"3306";
                WipAppVariables.WipServerAddress = ServerSettings_TextBox_ServerAddress.Text;
                WipAppVariables.WipServerPort = ServerSettings_TextBox_Port.Text;
                // Save to WIP server
                _ = ChangeLogDao.SetWipServerAddressAsync(WipAppVariables.User, WipAppVariables.WipServerAddress);
                _ = ChangeLogDao.SetWipServerPortAsync(WipAppVariables.User, WipAppVariables.WipServerPort);
            }

            if (MTM_WIP_App.SQL.Default.VisualUserName == "")
            {
                WipAppVariables.WipServerAddress = VisualLogin_TextBox_UserName.Text;
                _ = ChangeLogDao.Primary_ChangeLog_Set_Visual_UserAsync(VisualLogin_TextBox_UserName.Text,
                    WipAppVariables.User);
            }

            if (MTM_WIP_App.SQL.Default.VisualPassword == "")
            {
                WipAppVariables.WipServerAddress = VisualLogin_TextBox_Password.Text;
                _ = ChangeLogDao.Primary_ChangeLog_Set_Visual_PasswordAsync(VisualLogin_TextBox_Password.Text,
                    WipAppVariables.User);
            }

            AppLogger.Log("Default settings checked.");
        }

        private void ResetButton_Click(object sender, EventArgs e)
        {
            ServerSettings_TextBox_ServerAddress.Text = @"172.16.1.104";
            ServerSettings_TextBox_Port.Text = @"3306";
            WipAppVariables.WipServerAddress = ServerSettings_TextBox_ServerAddress.Text;
            WipAppVariables.WipServerPort = ServerSettings_TextBox_Port.Text;
            // Save to WIP server
            _ = ChangeLogDao.SetWipServerAddressAsync(WipAppVariables.User, WipAppVariables.WipServerAddress);
            _ = ChangeLogDao.SetWipServerPortAsync(WipAppVariables.User, WipAppVariables.WipServerPort);
            settingsChanged = true;
            AppLogger.Log("Server settings reset to default.");
        }

        private void VisualLogin_Button_Reset_Clicked(object sender, EventArgs e)
        {
            _ = ChangeLogDao.Primary_ChangeLog_Set_Visual_UserAsync("User Name", WipAppVariables.User);
            _ = ChangeLogDao.Primary_ChangeLog_Set_Visual_PasswordAsync("Password", WipAppVariables.User);
            VisualLogin_TextBox_UserName.Text = WipAppVariables.VisualPassword;
            VisualLogin_TextBox_Password.Text = WipAppVariables.VisualPassword;
            settingsChanged = true;
            VisualLogin_Button_Save.Enabled = false;
            VisualLogin_Button_Change.Enabled = true;
            VisualLogin_Button_Reset.Enabled = false;
            VisualLogin_TextBox_Password.Enabled = false;
            VisualLogin_TextBox_UserName.Enabled = false;
            AppLogger.Log("Visual login reset to default.");
        }

        private void ServerButtonEnabler(object sender, EventArgs e)
        {
            ServerSettings_Button_Update.Enabled = !string.IsNullOrEmpty(ServerSettings_TextBox_ServerAddress.Text) &&
                                                   !string.IsNullOrEmpty(ServerSettings_TextBox_Port.Text);
        }

        private void UpdateButton_Click(object sender, EventArgs e)
        {
            WipAppVariables.WipServerAddress = ServerSettings_TextBox_ServerAddress.Text;
            WipAppVariables.WipServerPort = ServerSettings_TextBox_Port.Text;
            MTM_WIP_App.SQL.Default.Save();
            settingsChanged = true;
            AppLogger.Log("Server settings updated.");
        }

        private void VisualLogin_Button_Save_Clicked(object sender, EventArgs e)
        {
            MTM_WIP_App.SQL.Default.VisualUserName = VisualLogin_TextBox_UserName.Text;
            MTM_WIP_App.SQL.Default.VisualPassword = VisualLogin_TextBox_Password.Text;
            SearchDao.Primary_ChangeLog_Set_Visual_User(VisualLogin_TextBox_UserName.Text, WipAppVariables.User);
            SearchDao.Primary_ChangeLog_Set_Visual_Password(VisualLogin_TextBox_Password.Text, WipAppVariables.User);
            MTM_WIP_App.SQL.Default.Save();
            VisualLogin_Button_Save.Enabled = false;
            VisualLogin_Button_Change.Enabled = true;
            VisualLogin_Button_Reset.Enabled = false;
            VisualLogin_TextBox_Password.Enabled = false;
            VisualLogin_TextBox_UserName.Enabled = false;
            settingsChanged = true;
            AppLogger.Log("Visual login settings saved.");
        }

        private void ResultsTheme_Button_Save_Click(object sender, EventArgs e)
        {
            if (ResultsTheme_ComboBox_Theme.SelectedIndex >= 1)
            {
                ResultsTheme_TextBox_CurrentTheme.Text = ResultsTheme_ComboBox_Theme.Text;
                MTM_WIP_App.SQL.Default.Theme_Name = ResultsTheme_ComboBox_Theme.Text;
                SearchDao.Primary_ChangeLog_Set_Theme_Name(ResultsTheme_ComboBox_Theme.Text, WipAppVariables.User);
                SearchDao.Primary_ChangeLog_Set_Theme_FontSize(
                    ResultsTheme_NumericUpDown_FontSize.Value.ToString(CultureInfo.InvariantCulture),
                    WipAppVariables.User);
                _ = new DgvDesigner();
                DgvDesigner.InitializeDataGridView(ResultsTheme_DataGridView_Preview, null);
                ResultsTheme_ComboBox_Theme.SelectedIndex = 0;
                MTM_WIP_App.SQL.Default.Save();
                AppLogger.Log("Results theme saved.");
            }
            else
            {
                ResultsTheme_ComboBox_Theme.SelectedIndex = 1;
                MTM_WIP_App.SQL.Default.Save();
            }
        }

        private void ResultsTheme_NumericUpDown_FontSize_ValueChanged(object sender, EventArgs e)
        {
            var tempTheme = ResultsTheme_Button_Save.Enabled == false
                ? MTM_WIP_App.SQL.Default.Theme_Name
                : ResultsTheme_ComboBox_Theme.Text;

            MTM_WIP_App.SQL.Default.Theme_TextSize = Convert.ToSingle(ResultsTheme_NumericUpDown_FontSize.Value);
            SearchDao.Primary_ChangeLog_Set_Theme_FontSize(
                ResultsTheme_NumericUpDown_FontSize.Value.ToString(CultureInfo.InvariantCulture),
                WipAppVariables.User);

            _ = new DgvDesigner();
            DgvDesigner.InitializeDataGridView(ResultsTheme_DataGridView_Preview, tempTheme);
            MTM_WIP_App.SQL.Default.Save();
            AppLogger.Log("Results theme font size changed.");
        }

        private void ResultsTheme_ComboBox_Theme_SelectedIndexChanged(object sender, EventArgs e)
        {
            string tempTheme;
            if (ResultsTheme_ComboBox_Theme.SelectedIndex <= 0)
            {
                tempTheme = MTM_WIP_App.SQL.Default.Theme_Name;
                ResultsTheme_Button_Save.Enabled = false;
                ResultsTheme_ComboBox_Theme.ForeColor = Color.Red;
            }
            else
            {
                if (ResultsTheme_ComboBox_Theme.Text == ResultsTheme_TextBox_CurrentTheme.Text)
                {
                    ResultsTheme_Button_Save.Enabled = false;
                    ResultsTheme_ComboBox_Theme.ForeColor = Color.Red;
                }
                else
                {
                    ResultsTheme_Button_Save.Enabled = true;
                    ResultsTheme_ComboBox_Theme.ForeColor = Color.Black;
                }

                tempTheme = ResultsTheme_ComboBox_Theme.Text;
            }

            _ = new DgvDesigner();
            DgvDesigner.InitializeDataGridView(ResultsTheme_DataGridView_Preview, tempTheme);
            AppLogger.Log("Results theme combo box selection changed.");
        }

        private void ResultsTheme_Button_Reset_Click(object sender, EventArgs e)
        {
            ResultsTheme_Button_Save.Enabled = false;
            ResultsTheme_TextBox_CurrentTheme.Text = @"Default (Black and White)";
            ResultsTheme_ComboBox_Theme.SelectedIndex = 1;
            ResultsTheme_NumericUpDown_FontSize.Value = 9;
            ResultsTheme_ComboBox_Theme.ForeColor = Color.Red;
            MTM_WIP_App.SQL.Default.Theme_Name = "Default (Black and White)";
            SearchDao.Primary_ChangeLog_Set_Theme_Name("Default (Black and White)", WipAppVariables.User);
            SearchDao.Primary_ChangeLog_Set_Theme_FontSize("9", WipAppVariables.User);

            MTM_WIP_App.SQL.Default.Theme_TextSize = 9;
            _ = new DgvDesigner();
            DgvDesigner.InitializeDataGridView(ResultsTheme_DataGridView_Preview, null);
            MTM_WIP_App.SQL.Default.Save();
            AppLogger.Log("Results theme reset to default.");
        }

        private void VisualLogin_Button_Change_Clicked(object sender, EventArgs e)
        {
            VisualLogin_Button_Save.Enabled = true;
            VisualLogin_Button_Change.Enabled = false;
            VisualLogin_Button_Reset.Enabled = true;
            VisualLogin_TextBox_Password.Enabled = true;
            VisualLogin_TextBox_UserName.Enabled = true;
            AppLogger.Log("Visual login change enabled.");
        }
    }
}