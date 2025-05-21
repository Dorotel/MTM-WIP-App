using MySql.Data.MySqlClient;
using MTM_WIP_App.Main_Form;
using Timer = System.Windows.Forms.Timer;

namespace MTM_WIP_App.Changelog
{
    public partial class ChangeLogForm : Form
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ChangeLogForm" /> class and calls the Primary_OnStartUp method.
        /// </summary>
        internal ChangeLogForm()
        {
            InitializeComponent();

            ChangeLog_ComboBox_Version.SelectedIndexChanged += ChangeLog_ComboBox_Version_SelectedIndexChanged;


            // Adjust font and layout for DPI
            FontScaler.AdjustFontAndLayout(this);

            ChangeLog_CheckBox_Hide.Click += ChangeLog_CheckBox_Hide_Clicked!;

            // Simplified object initialization for Timer
            _timer = new Timer
            {
                Interval = 500 // Set the interval to 500 milliseconds
            };
            _timer.Tick += Timer_Tick!;

            Primary_OnStartUp();
        }

        private void ChangeLog_ComboBox_Version_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (ChangeLog_ComboBox_Version.SelectedItem is string selectedVersion)
            {
                var versionNotes = SearchDao.Primary_ChangeLog_Get_VersionNotes(selectedVersion);
                if (IsValidRtf(versionNotes))
                {
                    ChangeLog_RichTextBox_Log.Rtf = versionNotes;
                }
                else
                {
                    ChangeLog_RichTextBox_Log.Text = @"Invalid RTF content.";
                }
            }
        }

        private bool _isRed = true;
        private readonly Timer _timer;

        /// <summary>
        ///     Handles the Click event of the ChangeLog_Button_Close control. Closes the form when the button is clicked.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void ChangeLog_Button_Close_Click(object sender, EventArgs e)
        {
            AppLogger.Log("ChangeLog_Button_Close clicked.");
            Close();
        }

        /// <summary>
        ///     Handles the Click event of the ChangeLog_Button_Update control. Updates the version notes in the database with the
        ///     current text from the changelog rich text box.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void ChangeLog_Button_Update_Click(object sender, EventArgs e)
        {
            AppLogger.Log("ChangeLog_Button_Update clicked.");
            if (WipAppVariables.Version != null)
            {
                var rtfNotes = ChangeLog_RichTextBox_Log.Rtf;
                if (!string.IsNullOrEmpty(rtfNotes))
                {
                    SearchDao.Primary_ChangeLog_Set_VersionNotes(rtfNotes,
                        WipAppVariables.Version); // Save the RTF content
                    AppLogger.Log("Version notes updated.");
                }
                else
                {
                    AppLogger.Log("ChangeLog_RichTextBox_Log.Rtf is null or empty. Update aborted.");
                }
            }
        }

        /// <summary>
        ///     Handles the Click event of the ChangeLog_CheckBox_Hide control. Toggles the changelog visibility status for the
        ///     current user.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void ChangeLog_CheckBox_Hide_Clicked(object sender, EventArgs e)
        {
            AppLogger.Log("ChangeLog_CheckBox_Hide clicked.");
            var shown = SearchDao.Primary_ChangeLog_Get_Toggle();
            if (ChangeLog_CheckBox_Hide.Checked)
            {
                if (shown == "false")
                {
                    SearchDao.Primary_ChangeLog_Set_Switch("true", WipAppVariables.User);
                    _timer.Stop();
                    ChangeLog_CheckBox_Hide.ForeColor = Color.Black; // Reset to default color
                    ChangeLog_CheckBox_Hide.Font = new Font(ChangeLog_CheckBox_Hide.Font, FontStyle.Regular);
                    AppLogger.Log("ChangeLog_CheckBox_Hide checked.");
                }
            }
            else
            {
                if (shown == "true")
                {
                    SearchDao.Primary_ChangeLog_Set_Switch("false", WipAppVariables.User);
                    _timer.Start();
                    AppLogger.Log("ChangeLog_CheckBox_Hide unchecked.");
                }
            }
        }

        /// <summary>
        ///     Validates if the provided string is a valid RTF content.
        /// </summary>
        /// <param name="rtf">The RTF content to validate.</param>
        /// <returns>True if the content is valid RTF; otherwise, false.</returns>
        private static bool IsValidRtf(string rtf)
        {
            try
            {
                using var richTextBox = new RichTextBox();
                richTextBox.Rtf = rtf;

                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        ///     Initializes the form on startup by retrieving and displaying the changelog status and version notes.
        ///     Sets the visibility and state of the update button and the changelog text box based on the user's role.
        /// </summary>
        private void Primary_OnStartUp()
        {
            AppLogger.Log("Primary_OnStartUp started.");
            try
            {
                var status = SearchDao.Primary_ChangeLog_Get_Toggle();
                var versionNotes = SearchDao.Primary_ChangeLog_Get_VersionNotes();

                // Validate and set the RTF content
                if (IsValidRtf(versionNotes))
                {
                    ChangeLog_RichTextBox_Log.Rtf = versionNotes;
                }
                else
                {
                    ChangeLog_RichTextBox_Log.Text = @"Invalid RTF content.";
                }

                // Fill ComboBox with all versions
                var versions = SearchDao.Primary_ChangeLog_Get_AllVersions();
                ChangeLog_ComboBox_Version.BeginUpdate();
                ChangeLog_ComboBox_Version.Items.Clear();
                ChangeLog_ComboBox_Version.Items.AddRange(versions.ToArray());
                ChangeLog_ComboBox_Version.EndUpdate();

                // Select the current version if available, else select the first
                if (!string.IsNullOrEmpty(WipAppVariables.Version) && versions.Contains(WipAppVariables.Version))
                {
                    ChangeLog_ComboBox_Version.SelectedItem = WipAppVariables.Version;
                }
                else if (versions.Count > 0)
                {
                    ChangeLog_ComboBox_Version.SelectedIndex = 0;
                }


                if (status == "true")
                {
                    SearchDao.Primary_ChangeLog_Set_Switch("true", WipAppVariables.User);
                    ChangeLog_CheckBox_Hide.Checked = true;
                    _timer.Stop();
                    ChangeLog_ComboBox_Version.ForeColor = Color.Black; // Reset to default color
                }
                else
                {
                    SearchDao.Primary_ChangeLog_Set_Switch("false", WipAppVariables.User);
                    ChangeLog_CheckBox_Hide.Checked = false;
                    _timer.Start();
                }

                if (WipAppVariables.User == "JOHNK" || WipAppVariables.User == "JKOLL")
                {
                    ChangeLog_Button_Update.Show();
                    ChangeLog_Button_Update.Enabled = true;
                    ChangeLog_RichTextBox_Log.ReadOnly = false;
                }
                else
                {
                    ChangeLog_Button_Update.Hide();
                    ChangeLog_Button_Update.Enabled = false;
                    ChangeLog_RichTextBox_Log.ReadOnly = true;
                }

                AppLogger.Log("Primary_OnStartUp completed.");
            }
            catch (MySqlException ex)
            {
                AppLogger.LogDatabaseError(ex);
                SearchDao.HandleException_SQLError_CloseApp(ex);
            }
            catch (Exception ex)
            {
                AppLogger.LogDatabaseError(ex);
                SearchDao.HandleException_GeneralError_CloseApp(ex);
            }
        }

        /// <summary>
        ///     Handles the Timer Tick event to change the text color of the ChangeLog_TextBox_Version.
        /// </summary>
        private void Timer_Tick(object sender, EventArgs e)
        {
            ChangeLog_CheckBox_Hide.ForeColor = _isRed ? Color.Red : Color.Black;
            ChangeLog_CheckBox_Hide.Font = _isRed
                ? new Font(ChangeLog_CheckBox_Hide.Font, FontStyle.Bold)
                : new Font(ChangeLog_CheckBox_Hide.Font, FontStyle.Regular);

            _isRed = !_isRed;
        }
    }
}