using MTM_WIP_App.Main_Form;
using MySql.Data.MySqlClient;
using System.Data;

namespace MTM_WIP_App
{
    public sealed partial class PersonalHistory : Form
    {
        private readonly BindingSource _entryBindingSource = new();
        private readonly BindingSource _removalBindingSource = new();
        private readonly BindingSource _transferBindingSource = new();
        private readonly string _connectionString = SqlVariables.GetConnectionString(null, null, null, null);

        /// <summary>
        /// Initializes a new instance of the <see cref="PersonalHistory"/> class.
        /// Sets the font size based on the device DPI and initializes the form components.
        /// If the user is an admin, additional setup can be performed.
        /// Sets the start position of the form to the center of the screen.
        /// </summary>
        public PersonalHistory()
        {
            try
            {
                InitializeComponent();

                // Adjust font and layout for DPI
                FontScaler.AdjustFontAndLayout(this);

                if (WipAppVariables.userTypeAdmin)
                {
                }

                StartPosition = FormStartPosition.CenterScreen;
            }
            catch (MySqlException ex)
            {
                ErrorLogDao.HandleException_SQLError_CloseApp(ex);
            }
            catch (Exception ex)
            {
                ErrorLogDao.HandleException_GeneralError_CloseApp(ex);
            }
        }


        /// <summary>
        /// Loads the form and initializes various components and data sources.
        /// Sets the default sort index, retrieves data from the database, and binds it to the DataGridViews.
        /// Initializes the DataGridViews using the DGV_Designer class.
        /// Populates the historyPart ComboBox with part IDs from the database.
        /// Calls the UserRecall method to set up user-specific data.
        /// </summary>
        public async void Form1_Load(object sender, EventArgs e)
        {
            DataTable table1 = new();

            try
            {
                sortBox.SelectedIndex = 3;

                // Run database queries in parallel off the UI thread
                var entryTask =
                    Task.Run(() => SearchDao.History_InventoryTab("User", PersonalHistoryVariables.user, ""));
                var removalTask =
                    Task.Run(() => SearchDao.History_RemovalTab("User", PersonalHistoryVariables.user, ""));
                var transferTask = Task.Run(() =>
                    SearchDao.History_TransferTab("User", PersonalHistoryVariables.user, ""));

                var entryData = await entryTask;
                var removalData = await removalTask;
                var transferData = await transferTask;

                // Now update UI (on UI thread)
                _entryBindingSource.DataSource = entryData;
                _removalBindingSource.DataSource = removalData;
                _transferBindingSource.DataSource = transferData;
                entryTable.DataSource = _entryBindingSource;
                removalTable.DataSource = _removalBindingSource;
                transferTable.DataSource = _transferBindingSource;

                DgvDesigner.InitializeDataGridView(entryTable, null);
                DgvDesigner.InitializeDataGridView(removalTable, null);
                DgvDesigner.InitializeDataGridView(transferTable, null);

                // Corrected: Open connection before using it
                using (var connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new MySqlCommand("SELECT `Item Number` FROM part_ids", connection))
                    using (var da = new MySqlDataAdapter { SelectCommand = command })
                    {
                        da.Fill(table1);
                    }
                }

                var itemrow = table1.NewRow();
                itemrow[0] = "[ Enter Part ID ]";
                table1.Rows.InsertAt(itemrow, 0);
                historyPart.DataSource = table1;
                historyPart.DisplayMember = "Item Number";
                historyPart.ValueMember = "Item Number";

                UserRecall();
            }
            catch (MySqlException ex)
            {
                ErrorLogDao.HandleException_SQLError_CloseApp(ex);
            }
            catch (Exception ex)
            {
                ErrorLogDao.HandleException_GeneralError_CloseApp(ex);
            }
        }


        /// <summary>
        /// Handles the SelectedIndexChanged event for the sortBox ComboBox.
        /// Updates the history data based on the selected sort criteria (Employee, Part ID, Location, Date).
        /// Calls IndexCheck and PartTBCheck methods to update the relevant variables.
        /// Calls HistoryRecall method to refresh the data displayed in the DataGridViews based on the selected sort criteria.
        /// </summary>
        public void SortBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                IndexCheck();
                PartTbCheck();

                if (sortBox.Text == @"Employee")
                {
                    HistoryRecall("User", PersonalHistoryVariables.huser, PersonalHistoryVariables.part);
                }
                else if (sortBox.Text == @"Part ID")
                {
                    HistoryRecall("Part ID", PersonalHistoryVariables.huser, PersonalHistoryVariables.part);
                }
                else if (sortBox.Text == @"Location")
                {
                    HistoryRecall("Location", PersonalHistoryVariables.huser, PersonalHistoryVariables.part);
                }
                else if (sortBox.Text == @"Date")
                {
                    HistoryRecall("Time", PersonalHistoryVariables.huser, PersonalHistoryVariables.part);
                }
            }
            catch (MySqlException ex)
            {
                ErrorLogDao.HandleException_SQLError_CloseApp(ex);
            }
            catch (Exception ex)
            {
                ErrorLogDao.HandleException_GeneralError_CloseApp(ex);
            }
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event for the userCB ComboBox.
        /// Calls the UserRecall method to update the user-specific data based on the selected user.
        /// </summary>
        public void UserCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                UserRecall();
            }
            catch (MySqlException ex)
            {
                ErrorLogDao.HandleException_SQLError_CloseApp(ex);
            }
            catch (Exception ex)
            {
                ErrorLogDao.HandleException_GeneralError_CloseApp(ex);
            }
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event for the historyPart ComboBox.
        /// Updates the history data based on the selected part ID and the current sort criteria (Employee, Part ID, Location, Date).
        /// Calls IndexCheck and PartTBCheck methods to update the relevant variables.
        /// Calls HistoryRecall method to refresh the data displayed in the DataGridViews based on the selected part ID and sort criteria.
        /// </summary>
        public void HistoryPart_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (historyPart.SelectedIndex > 0)
                {
                    IndexCheck();
                    PartTbCheck();

                    if (sortBox.Text == @"Employee")
                    {
                        HistoryRecall("User", PersonalHistoryVariables.huser, PersonalHistoryVariables.part);
                    }
                    else if (sortBox.Text == @"Part ID")
                    {
                        HistoryRecall("Part ID", PersonalHistoryVariables.huser, PersonalHistoryVariables.part);
                    }
                    else if (sortBox.Text == @"Location")
                    {
                        HistoryRecall("Location", PersonalHistoryVariables.huser, PersonalHistoryVariables.part);
                    }
                    else if (sortBox.Text == @"Date")
                    {
                        HistoryRecall("Time", PersonalHistoryVariables.huser, PersonalHistoryVariables.part);
                    }
                }
            }
            catch (MySqlException ex)
            {
                ErrorLogDao.HandleException_SQLError_CloseApp(ex);
            }
            catch (Exception ex)
            {
                ErrorLogDao.HandleException_GeneralError_CloseApp(ex);
            }
        }

        /// <summary>
        /// Retrieves user data from the database and updates the userCB ComboBox with the retrieved data.
        /// If the initial load is true, sets the data source, display member, and value member for the userCB ComboBox.
        /// If the user is an admin, selects the last user in the list and updates the shiftTB and userNameTB TextBoxes.
        /// If the user is not an admin, selects the current user in the list and updates the shiftTB and userNameTB TextBoxes.
        /// Calls PartTBCheck and IndexCheck methods to update the relevant variables.
        /// Calls HistoryRecall method to refresh the data displayed in the DataGridViews based on the selected user and sort criteria.
        /// </summary>
        private void UserRecall()
        {
            MySqlConnection connection = null;
            MySqlCommand command8 = null;
            MySqlDataAdapter da8 = null;
            DataTable table8 = new();

            try
            {
                connection = new MySqlConnection(_connectionString);
                connection.Open();

                command8 = new MySqlCommand("SELECT * FROM `users` ORDER BY Shift ASC", connection);
                da8 = new MySqlDataAdapter
                {
                    SelectCommand = command8
                };
                da8.Fill(table8);

                if (PersonalHistoryVariables.initialLoad)
                {
                    userCB.DataSource = table8;
                    userCB.DisplayMember = "Full Name";
                    userCB.ValueMember = "Full Name";

                    if (WipAppVariables.userTypeAdmin)
                    {
                        var currentRow = table8.Rows[^1];
                        userCB.SelectedIndex = userCB.Items.Count - 1;
                        shiftTB.Text = currentRow["Shift"].ToString();
                        userNameTB.Text = currentRow["User"].ToString();
                    }
                    else
                    {
                        for (var i = 0; i < table8.Rows.Count; i++)
                        {
                            var currentRow = table8.Rows[i];
                            if (currentRow["User"].ToString() == PersonalHistoryVariables.user)
                            {
                                userCB.SelectedIndex = i;
                                shiftTB.Text = currentRow["Shift"].ToString();
                                userNameTB.Text = currentRow["User"].ToString();
                                break;
                            }
                        }

                        userCB.Enabled = false;
                    }

                    PersonalHistoryVariables.initialLoad = false;
                }

                PartTbCheck();

                var selectedrowindex = userCB.SelectedIndex;
                var selectedRow = table8.Rows[selectedrowindex];
                userNameTB.Text = $@"{selectedRow["User"]}";
                shiftTB.Text = $@"{selectedRow["Shift"]}";

                IndexCheck();

                if (sortBox.Text == @"Employee")
                {
                    HistoryRecall("User", PersonalHistoryVariables.huser, PersonalHistoryVariables.part);
                }
                else if (sortBox.Text == @"Part ID")
                {
                    HistoryRecall("Part ID", PersonalHistoryVariables.huser, PersonalHistoryVariables.part);
                }
                else if (sortBox.Text == @"Location")
                {
                    HistoryRecall("Location", PersonalHistoryVariables.huser, PersonalHistoryVariables.part);
                }
                else if (sortBox.Text == @"Date")
                {
                    HistoryRecall("Time", PersonalHistoryVariables.huser, PersonalHistoryVariables.part);
                }
            }
            catch (MySqlException ex)
            {
                ErrorLogDao.HandleException_SQLError_CloseApp(ex);
            }
            catch (Exception ex)
            {
                ErrorLogDao.HandleException_GeneralError_CloseApp(ex);
            }
            finally
            {
                da8?.Dispose();
                command8?.Dispose();
                connection?.Close();
                connection?.Dispose();
            }
        }

        /// <summary>
        /// Resets the design of the DataGridViews for entryTable, removalTable, and transferTable.
        /// Initializes the DataGridViews using the DGV_Designer class.
        /// </summary>
        private void ResetDesigner()
        {
            try
            {
                DgvDesigner.InitializeDataGridView(entryTable, null);
                DgvDesigner.InitializeDataGridView(removalTable, null);
                DgvDesigner.InitializeDataGridView(transferTable, null);
            }
            catch (MySqlException ex)
            {
                ErrorLogDao.HandleException_SQLError_CloseApp(ex);
            }
            catch (Exception ex)
            {
                ErrorLogDao.HandleException_GeneralError_CloseApp(ex);
            }
        }

        /// <summary>
        /// Retrieves history data from the database based on the specified type, user, and part.
        /// Updates the data sources for the entry, removal, and transfer DataGridViews.
        /// Calls the ResetDesigner method to reset the design of the DataGridViews.
        /// Hides the "ID" column in the DataGridViews if it exists.
        /// </summary>
        private void HistoryRecall(string type, string huser, string part)
        {
            try
            {
                _entryBindingSource.DataSource = SearchDao.History_InventoryTab(type, huser, part);
                _removalBindingSource.DataSource = SearchDao.History_RemovalTab(type, huser, part);
                _transferBindingSource.DataSource = SearchDao.History_TransferTab(type, huser, part);
                entryTable.Update();
                removalTable.Update();
                transferTable.Update();

                ResetDesigner();

                if (entryTable.Columns.Contains("ID") && removalTable.Columns.Contains("ID") &&
                    transferTable.Columns.Contains("ID"))
                {
                    entryTable.Columns["ID"].Visible = false;
                    removalTable.Columns["ID"].Visible = false;
                    transferTable.Columns["ID"].Visible = false;
                }
            }
            catch (MySqlException ex)
            {
                ErrorLogDao.HandleException_SQLError_CloseApp(ex);
            }
            catch (Exception ex)
            {
                ErrorLogDao.HandleException_GeneralError_CloseApp(ex);
            }
        }

        /// <summary>
        /// Checks the selected user in the userCB ComboBox and updates the PersonalHistory_Variables.huser variable accordingly.
        /// If the selected user is "[ All Users ]", sets PersonalHistory_Variables.huser to null.
        /// Otherwise, sets PersonalHistory_Variables.huser to the text in the userNameTB TextBox.
        /// </summary>
        private void IndexCheck()
        {
            try
            {
                if (userCB.Text == @"[ All Users ]")
                {
                    PersonalHistoryVariables.huser = null;
                }
                else
                {
                    PersonalHistoryVariables.huser = userNameTB.Text;
                }
            }
            catch (MySqlException ex)
            {
                ErrorLogDao.HandleException_SQLError_CloseApp(ex);
            }
            catch (Exception ex)
            {
                ErrorLogDao.HandleException_GeneralError_CloseApp(ex);
            }
        }

        /// <summary>
        /// Checks the selected part in the historyPart ComboBox and updates the PersonalHistory_Variables.part variable accordingly.
        /// If a part is selected, sets PersonalHistory_Variables.part to the selected part's text.
        /// Otherwise, sets PersonalHistory_Variables.part to an empty string.
        /// </summary>
        private void PartTbCheck()
        {
            try
            {
                if (historyPart.SelectedIndex > 0)
                {
                    PersonalHistoryVariables.part = historyPart.Text;
                }
                else
                {
                    PersonalHistoryVariables.part = "";
                }
            }
            catch (MySqlException ex)
            {
                ErrorLogDao.HandleException_SQLError_CloseApp(ex);
            }
            catch (Exception ex)
            {
                ErrorLogDao.HandleException_GeneralError_CloseApp(ex);
            }
        }

        /// <summary>
        /// Handles the FormClosing event for the PersonalHistory form.
        /// Sets the PersonalHistory_Variables.initialLoad variable to true to indicate that the form is closing.
        /// </summary>
        private void PersonalHistory_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                PersonalHistoryVariables.initialLoad = true;
            }
            catch (MySqlException ex)
            {
                ErrorLogDao.HandleException_SQLError_CloseApp(ex);
            }
            catch (Exception ex)
            {
                ErrorLogDao.HandleException_GeneralError_CloseApp(ex);
            }
        }

        /// <summary>
        /// Handles the click event for the PrintToolStripMenuItem.
        /// Configures the DGVPrinter with the appropriate settings and prints the DataGridView based on the selected tab.
        /// Sets the subtitle, title, and other print settings for the DGVPrinter.
        /// </summary>
        private void PrintToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                DgvPrinter printer = new()
                {
                    SubTitle = "Report of transactions made by : " + userCB.Text,
                    SubTitleFormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.NoClip,
                    PageNumbers = true,
                    HideColumns = { "ID", "Type" },
                    PageNumberInHeader = false,
                    PorportionalColumns = true,
                    HeaderCellAlignment = StringAlignment.Near,
                    Footer = "Manitowoc Tool and Manufacturing",
                    FooterSpacing = 15
                };

                if (inputTab.SelectedIndex == 0)
                {
                    PersonalHistoryVariables.currentTab = "Inventoried Items (New Transactions)";
                    printer.Title = "Personal History Report | " + PersonalHistoryVariables.currentTab;
                    printer.PrintDataGridView(entryTable);
                }
                else if (inputTab.SelectedIndex == 1)
                {
                    PersonalHistoryVariables.currentTab = "Removed Items (Remove)";
                    printer.Title = "Personal History Report | " + PersonalHistoryVariables.currentTab;
                    printer.PrintDataGridView(removalTable);
                }
                else if (inputTab.SelectedIndex == 2)
                {
                    PersonalHistoryVariables.currentTab = "Transferred Items (Transfer)";
                    printer.Title = "Personal History Report | " + PersonalHistoryVariables.currentTab;
                    printer.PrintDataGridView(transferTable);
                }
            }
            catch (MySqlException ex)
            {
                ErrorLogDao.HandleException_SQLError_CloseApp(ex);
            }
            catch (Exception ex)
            {
                ErrorLogDao.HandleException_GeneralError_CloseApp(ex);
            }
        }

        /// <summary>
        /// Handles the click event for the ResetToolStripMenuItem.
        /// Resets the form by clearing all controls, reinitializing the components, and reloading the form data.
        /// Sets the PersonalHistory_Variables.initialLoad variable to true to indicate that the form is being reset.
        /// </summary>
        private void ResetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                PersonalHistoryVariables.initialLoad = true;
                Controls.Clear();
                InitializeComponent();

                // Adjust font and layout for DPI
                FontScaler.AdjustFontAndLayout(this);

                Form1_Load(null, null);
            }
            catch (MySqlException ex)
            {
                ErrorLogDao.HandleException_SQLError_CloseApp(ex);
            }
            catch (Exception ex)
            {
                ErrorLogDao.HandleException_GeneralError_CloseApp(ex);
            }
        }

        /// <summary>
        /// Handles the click event for the PersonalHistory_Reset_Button.
        /// Calls the ResetToolStripMenuItem_Click method to reset the form by clearing all controls, reinitializing the components, and reloading the form data.
        /// </summary>
        private void PersonalHistory_Reset_Button_Click(object sender, EventArgs e)
        {
            try
            {
                ResetToolStripMenuItem_Click(null, null);
            }
            catch (MySqlException ex)
            {
                ErrorLogDao.HandleException_SQLError_CloseApp(ex);
            }
            catch (Exception ex)
            {
                ErrorLogDao.HandleException_GeneralError_CloseApp(ex);
            }
        }
    }
}