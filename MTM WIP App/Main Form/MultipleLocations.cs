using MySql.Data.MySqlClient;
using System.Data;

namespace MTM_WIP_App.Main_Form
{
    public sealed partial class MultipleLocations : Form
    {
        // DataTables
        private readonly DataTable _multipleLocationsLocationDataTable = new();

        // MySqlDataAdapters
        private readonly MySqlDataAdapter _multipleLocationsLocationDataAdapter = new();

        // Other

        public static bool multipleLocationsDisabled;
        public static int multipleLocationsCount;

        public MultipleLocations()
        {
            try
            {
                // Placement
                StartPosition = FormStartPosition.CenterScreen;

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

                // Adjust font and layout for DPI
                FontScaler.AdjustFontAndLayout(this);

                AppLogger.Log("MultipleLocations form initialized.");
            }
            catch (Exception ex)
            {
                AppLogger.LogDatabaseError(ex);
                AppLogger.Log("Error in MultipleLocations (Initialize): " + ex.Message);
                MessageBox.Show(@"Error in MultipleLocations (Initialize)" + """


                                                                             """ + @"Exception:\n" + ex.Message,
                    @"Error");
            }
        }

        private void MultipleLocations_Load(object sender, EventArgs e)
        {
            try
            {
                AppLogger.Log("Loading MultipleLocations form.");
                var connectionString = SqlVariables.GetConnectionString(null, null, null, null);
                MySqlConnection connection = new(connectionString);

                MySqlCommand multipleLocationsLocationCommand = new("SELECT * FROM `locations`", connection);
                _multipleLocationsLocationDataAdapter.SelectCommand = multipleLocationsLocationCommand;
                _multipleLocationsLocationDataAdapter.Fill(_multipleLocationsLocationDataTable);
                var multipleLocationsLocationItemRow = _multipleLocationsLocationDataTable.NewRow();
                multipleLocationsLocationItemRow[0] = "[ Enter Location ]";
                _multipleLocationsLocationDataTable.Rows.InsertAt(multipleLocationsLocationItemRow, 0);
                MultipleLocations_ComboBox_Location.DataSource = _multipleLocationsLocationDataTable;
                MultipleLocations_ComboBox_Location.DisplayMember = "Location";
                MultipleLocations_ComboBox_Location.ValueMember = "Location";
                MultipleLocations_Label_Part.Text = WipAppVariables.partId;
                MultipleLocations_TextBox_Quantity.Text = WipAppVariables.InventoryQuantity.ToString();
            }
            catch (Exception ex)
            {
                AppLogger.LogDatabaseError(ex);
                AppLogger.Log("Error in MultipleLocations_Load: " + ex.Message);
                MessageBox.Show(@"Error in MultipleLocations_Load" + """


                                                                     """ + @"Exception:\n" + ex.Message, @"Error");
            }
        }

        private void MultipleLocations_Button_Exit_Click(object sender, EventArgs e)
        {
            try
            {
                AppLogger.Log("Exiting MultipleLocations form.");
                WipAppVariables.mainFormFormReset = true;

                var form = new MainForm();
                Application.OpenForms[form.Name].Cursor = Cursors.Default;
                Application.OpenForms[form.Name].Enabled = true;
                Application.OpenForms[form.Name].Activate();
                Application.OpenForms[form.Name].Focus();
                multipleLocationsDisabled = true;
                Close();
            }
            catch (Exception ex)
            {
                AppLogger.LogDatabaseError(ex);
                AppLogger.Log("Error in MultipleLocations_Button_Exit_Click: " + ex.Message);
                MessageBox.Show(@"Error in MultipleLocations_Button_Exit_Click" + """


                        """ + @"Exception:\n" + ex.Message,
                    @"Error");
            }
        }

        private void MultipleLocations_ComboBox_Location_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (MultipleLocations_ComboBox_Location.SelectedIndex >= 1)
                {
                    MultipleLocations_ComboBox_Location.ForeColor = Color.Black;
                    if (int.TryParse(MultipleLocations_TextBox_Quantity.Text, out _))
                    {
                        MultipleLocations_TextBox_Quantity.ForeColor = Color.Black;
                        MultipleLocations_Button_Save.Enabled = true;
                    }
                    else
                    {
                        MultipleLocations_TextBox_Quantity.ForeColor = Color.Red;
                        MultipleLocations_TextBox_Quantity.Text = @"[ Enter Valid Quantity ]";
                        MultipleLocations_Button_Save.Enabled = false;
                    }
                }
                else
                {
                    MultipleLocations_ComboBox_Location.ForeColor = Color.Red;
                    MultipleLocations_ComboBox_Location.SelectedIndex = 0;
                    MultipleLocations_Button_Save.Enabled = false;
                }

                AppLogger.Log("Location selected: " + MultipleLocations_ComboBox_Location.Text);
            }
            catch (Exception ex)
            {
                AppLogger.LogDatabaseError(ex);
                AppLogger.Log("Error in MultipleLocations_ComboBox_Location_SelectedIndexChanged: " + ex.Message);
                MessageBox.Show(
                    @"Error in MultipleLocations_ComboBox_Location_SelectedIndexChanged" + """


                        """ + @"Exception:\n" +
                    ex.Message, @"Error");
            }
        }

        private void MultipleLocations_Button_Save_Click(object sender, EventArgs e)
        {
            try
            {
                AppLogger.Log("Saving location and quantity.");
                _ = new SearchDao();
                multipleLocationsCount++;
                MultipleLocations_Label_Count.Text = @"Transactions: " + multipleLocationsCount;
                WipAppVariables.PartType = "From Press";
                WipAppVariables.Location = MultipleLocations_ComboBox_Location.Text;
                WipAppVariables.InventoryQuantity = Convert.ToInt32(MultipleLocations_TextBox_Quantity.Text);
                SearchDao.InventoryTab_Save();
                MultipleLocations_ComboBox_Location.Focus();
            }
            catch (Exception ex)
            {
                AppLogger.LogDatabaseError(ex);
                AppLogger.Log("Error in MultipleLocations_Button_Save_Click: " + ex.Message);
                MessageBox.Show(@"Error in MultipleLocations_Button_Save_Click" + """


                        """ + @"Exception:" + ex.Message,
                    @"Error");
            }
        }

        private void MultipleLocations_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                AppLogger.Log("MultipleLocations form closed.");
                var form = new MainForm();
                Application.OpenForms[form.Name].Cursor = Cursors.Default;
                Application.OpenForms[form.Name].Enabled = true;
                Application.OpenForms[form.Name].Activate();
                multipleLocationsDisabled = true;
                Application.OpenForms[form.Name].Focus();
            }
            catch (Exception ex)
            {
                AppLogger.LogDatabaseError(ex);
                AppLogger.Log("Error in MultipleLocations_FormClosed: " + ex.Message);
                MessageBox.Show(@"Error in MultipleLocations_FormClosed" + """


                                                                           """ + """
                    Exception:

                    """ + ex.Message, @"Error");
            }
        }

        private void MultipleLocations_Quantity_Leave(object sender, EventArgs e)
        {
            try
            {
                if (int.TryParse(MultipleLocations_TextBox_Quantity.Text, out _))
                {
                    MultipleLocations_TextBox_Quantity.ForeColor = Color.Black;
                    if (MultipleLocations_ComboBox_Location.SelectedIndex >= 1)
                    {
                        MultipleLocations_ComboBox_Location.ForeColor = Color.Black;
                        MultipleLocations_Button_Save.Enabled = true;
                    }
                    else
                    {
                        MultipleLocations_ComboBox_Location.ForeColor = Color.Red;
                        MultipleLocations_ComboBox_Location.SelectedIndex = 0;
                        MultipleLocations_Button_Save.Enabled = false;
                    }
                }
                else
                {
                    MultipleLocations_TextBox_Quantity.ForeColor = Color.Red;
                    MultipleLocations_TextBox_Quantity.Text = @"[ Enter Valid Quantity ]";
                    MultipleLocations_Button_Save.Enabled = false;
                }

                AppLogger.Log("Quantity leave event processed.");
            }
            catch (Exception ex)
            {
                AppLogger.LogDatabaseError(ex);
                AppLogger.Log("Error in MultipleLocations_Quantity_Leave: " + ex.Message);
                MessageBox.Show(@"Error in MultipleLocations_Quantity_Leave" + """


                                                                               """ + @"Exception:
" + ex.Message,
                    @"Error");
            }
        }

        private void MultipleLocations_ComboBox_Location_Leave(object sender, EventArgs e)
        {
            try
            {
                if (MultipleLocations_ComboBox_Location.SelectedIndex >= 1)
                {
                    MultipleLocations_ComboBox_Location.ForeColor = Color.Black;
                    if (int.TryParse(MultipleLocations_TextBox_Quantity.Text, out _))
                    {
                        MultipleLocations_TextBox_Quantity.ForeColor = Color.Black;
                        MultipleLocations_Button_Save.Enabled = true;
                    }
                    else
                    {
                        MultipleLocations_TextBox_Quantity.ForeColor = Color.Red;
                        MultipleLocations_TextBox_Quantity.Text = @"[ Enter Valid Quantity ]";
                        MultipleLocations_Button_Save.Enabled = false;
                    }
                }
                else
                {
                    MultipleLocations_ComboBox_Location.ForeColor = Color.Red;
                    MultipleLocations_ComboBox_Location.SelectedIndex = 0;
                    MultipleLocations_Button_Save.Enabled = false;
                }

                AppLogger.Log("Location leave event processed.");
            }
            catch (Exception ex)
            {
                AppLogger.LogDatabaseError(ex);
                AppLogger.Log("Error in MultipleLocations_ComboBox_Location_Leave: " + ex.Message);
                MessageBox.Show(
                    @"Error in MultipleLocations_ComboBox_Location_Leave" + """


                                                                            """ + """
                        Exception:

                        """ + ex.Message, @"Error");
            }
        }

        private void MultipleLocations_ComboBox_Location_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                {
                    SelectNextControl((Control)sender, true, true, true, true);
                    e.Handled = e.SuppressKeyPress = true;
                }

                AppLogger.Log("Location key down event processed.");
            }
            catch (Exception ex)
            {
                AppLogger.LogDatabaseError(ex);
                AppLogger.Log("Error in MultipleLocations_ComboBox_Location_KeyDown: " + ex.Message);
                MessageBox.Show(
                    @"Error in MultipleLocations_ComboBox_Location_KeyDown" + """


                                                                              """ + """
                        Exception:

                        """ + ex.Message,
                    @"Error");
            }
        }

        private void MultipleLocations_TextBox_Quantity_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                {
                    SelectNextControl((Control)sender, true, true, true, true);
                    e.Handled = e.SuppressKeyPress = true;
                }

                AppLogger.Log("Quantity key down event processed.");
            }
            catch (Exception ex)
            {
                AppLogger.LogDatabaseError(ex);
                AppLogger.Log("Error in MultipleLocations_TextBox_Quantity_KeyDown: " + ex.Message);
                MessageBox.Show(
                    @"Error in MultipleLocations_TextBox_Quantity_KeyDown" + """


                                                                             """ + """
                        Exception:

                        """ + ex.Message,
                    @"Error");
            }
        }
    }
}