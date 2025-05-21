using MTM_WIP_App.Database_Maint.Add_Objects.Unified_Entry_Form.Classes;
using MTM_WIP_App.Main_Form;

namespace MTM_WIP_App.Database_Maint.Add_Objects.Unified_Entry_Form
{
    public partial class UnifiedEntryForm : Form
    {
        private readonly string _connectionString = SqlVariables.GetConnectionString(null, null, null, null);
        private readonly bool _isFromLoginPrompt;

        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility
            .Hidden)]
        public string? NewUser { get; private set; }

        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility
            .Hidden)]
        public string? NewUserPin { get; private set; }

        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility
            .Hidden)]
        private void ObjectTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                UnifiedEntryForm_EventHandling.ObjectTypeComboBox_SelectedIndexChanged(
                    ObjectTypeComboBox,
                    UserPanel,
                    PartPanel,
                    PartTypePanel,
                    OpPanel,
                    LocationPanel,
                    SaveButton
                );

                if (UserPanel.Visible)
                {
                    ClientSize = new Size(ClientSize.Width, UserPanel.Height + 120);
                }
                else if (PartPanel.Visible)
                {
                    ClientSize = new Size(ClientSize.Width, PartPanel.Height + 120);
                }
                else if (PartTypePanel.Visible)
                {
                    ClientSize = new Size(ClientSize.Width, PartTypePanel.Height + 120);
                }
                else if (OpPanel.Visible)
                {
                    ClientSize = new Size(ClientSize.Width, OpPanel.Height + 120);
                }
                else if (LocationPanel.Visible)
                {
                    ClientSize = new Size(ClientSize.Width, LocationPanel.Height + 120);
                }
            }
            catch (Exception ex)
            {
                AppLogger.Log($@"Error in ObjectTypeComboBox_SelectedIndexChanged: {ex.Message}");
                MessageBox.Show($@"An error occurred while changing the panel:
{ex.Message}", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public UnifiedEntryForm(bool fromLoginPrompt = false)
        {
            try
            {
                InitializeComponent();
                _isFromLoginPrompt = fromLoginPrompt;

                AdminCheckBox.CheckedChanged += AdminCheckBox_CheckedChanged;
                ReadOnlyCheckBox.CheckedChanged += ReadOnlyCheckBox_CheckedChanged;


                FontScaler.AdjustFontAndLayout(this);
                StartPosition = FormStartPosition.CenterScreen;

                UnifedEntryForm_ComboBoxHelper.InitializePartTypeComboBox(PartTypeComboBox, _connectionString);
                UnifedEntryForm_ComboBoxHelper.InitializeShiftComboBox(ShiftComboBox);

                if (_isFromLoginPrompt)
                {
                    ObjectTypeComboBox.SelectedIndex = 0;
                    ClientSize = new Size(ClientSize.Width, UserPanel.Height + 120);
                    VitsCheckBox.Checked = true;
                    VitsCheckBox.Visible = false;

                    SaveButton.Enabled = true;
                    AdminCheckBox.Visible = false;
                    ReadOnlyCheckBox.Visible = false;
                    ObjectTypeComboBox.Enabled = false;
                }
                else
                {
                    SaveButton.Enabled = false;
                }

                AppLogger.Log(@"UnifiedEntryForm initialized.");
            }
            catch (Exception ex)
            {
                AppLogger.Log($@"Error in UnifiedEntryForm (Initialize): {ex.Message}");
                MessageBox.Show($@"Error in UnifiedEntryForm (Initialize)
Exception:
{ex.Message}", @"Error");
            }
        }

        private void SaveMultipleLocations(string connectionString, string zone, string subZone, int startHeight,
            int endHeight, int startColumn, int endColumn)
        {
            UnifiedEntryForm_DatabaseOperations.ResetCounters();

            for (var height = startHeight; height <= endHeight; height++)
            {
                for (var column = startColumn; column <= endColumn; column++)
                {
                    var location = $@"{zone}-{subZone}{height:D1}-{column:D2}";
                    var locationInput = new TextBox { Text = location };

                    UnifiedEntryForm_DatabaseOperations.SaveEntry(ObjectTypeComboBox, connectionString, null, null,
                        null, null, null,
                        null, null, null, null, null, null, null, locationInput, true);
                }
            }

            var message = $@"Total Locations: {UnifiedEntryForm_DatabaseOperations.TotalLocations}
Successfully Saved: {UnifiedEntryForm_DatabaseOperations.SuccessfulLocations}
Failed: {UnifiedEntryForm_DatabaseOperations.FailedLocations}";

            if (UnifiedEntryForm_DatabaseOperations.FailedLocations > 0)
            {
                message += $@"

Failed Locations:
{string.Join("\n", UnifiedEntryForm_DatabaseOperations.FailedLocationDetails)}";
            }

            MessageBox.Show(message, @"Save Locations Summary", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void SaveSingleLocation(string connectionString, TextBox locationInput)
        {
            UnifiedEntryForm_DatabaseOperations.ResetCounters();

            UnifiedEntryForm_DatabaseOperations.SaveEntry(
                ObjectTypeComboBox, connectionString, null, null, null, null, null,
                null, null, null, null, null, null, null, locationInput, true);

            var message = $@"Total Locations: {UnifiedEntryForm_DatabaseOperations.TotalLocations}
Successfully Saved: {UnifiedEntryForm_DatabaseOperations.SuccessfulLocations}
Failed: {UnifiedEntryForm_DatabaseOperations.FailedLocations}";

            if (UnifiedEntryForm_DatabaseOperations.FailedLocations > 0)
            {
                message += $@"\n
Failed Locations:
{string.Join("\n", UnifiedEntryForm_DatabaseOperations.FailedLocationDetails)}";
            }

            MessageBox.Show(message, @"Save Locations Summary", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (ObjectTypeComboBox.Text == @"User")
                {
                    if (!UnifiedEntryForm_ValidationHelper.UserValidation(ObjectTypeComboBox, UserFirstName,
                            UserLastName, UserEmail,
                            UserPin, ShiftComboBox))
                    {
                        return;
                    }
                }
                else if (ObjectTypeComboBox.Text == @"Part")
                {
                    if (!UnifiedEntryForm_ValidationHelper.PartValidation(PartInput, PartTypeComboBox))
                    {
                        return;
                    }
                }
                else if (ObjectTypeComboBox.Text == @"Operation")
                {
                    if (!UnifiedEntryForm_ValidationHelper.OperationValidation(OpInput))
                    {
                        return;
                    }
                }
                else if (ObjectTypeComboBox.Text == @"Part Type")
                {
                    if (!UnifiedEntryForm_ValidationHelper.PartTypeValidation(PartTypeInput))
                    {
                        return;
                    }
                }
                else if (ObjectTypeComboBox.Text == @"Location")
                {
                    if (ZoneInput.Visible)
                    {
                        var zone = ZoneInput.Text.Trim();
                        var subZone = SubZoneInput.Text.Trim();
                        if (!int.TryParse(StartHeightInput.Text.Trim(), out var startHeight) ||
                            !int.TryParse(EndHeightInput.Text.Trim(), out var endHeight) ||
                            !int.TryParse(StartColumnInput.Text.Trim(), out var startColumn) ||
                            !int.TryParse(EndColumnInput.Text.Trim(), out var endColumn))
                        {
                            MessageBox.Show(@"Invalid input for height or column range.", @"Validation Error");
                            return;
                        }

                        SaveMultipleLocations(_connectionString, zone, subZone, startHeight, endHeight, startColumn,
                            endColumn);
                        return;
                    }
                    else
                    {
                        if (!UnifiedEntryForm_ValidationHelper.LocationValidation(LocationInput,
                                LocationInput.Text.Trim()))
                        {
                            return;
                        }

                        // Use the new helper for single location
                        SaveSingleLocation(_connectionString, LocationInput);
                        return;
                    }
                }


                UnifiedEntryForm_DatabaseOperations.SaveEntry(ObjectTypeComboBox, _connectionString, UserFirstName,
                    UserLastName,
                    UserEmail, UserPin, ShiftComboBox, VitsCheckBox, AdminCheckBox, ReadOnlyCheckBox, PartInput,
                    PartTypeComboBox, PartTypeInput, OpInput, LocationInput, false);

                if (ObjectTypeComboBox.SelectedItem?.ToString() == @"User")
                {
                    NewUser = UserEmail.Text;
                    NewUserPin = UserPin.Text;
                }

                if (_isFromLoginPrompt)
                {
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    UnifiedEntryForm_FormHelper.ResetForm(UserFirstName, UserLastName, UserEmail, UserPin, VitsCheckBox,
                        AdminCheckBox,
                        ReadOnlyCheckBox, PartInput, PartTypeComboBox, ObjectTypeComboBox, SaveButton);
                }
            }
            catch (Exception ex)
            {
                AppLogger.Log($@"Error in SaveButton_Click: {ex.Message}");
                MessageBox.Show($@"Error in SaveButton_Click
Exception:
{ex.Message}", @"Error");
            }
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            Close();
            AppLogger.Log(@"UnifiedEntryForm closed.");
        }

        private void AddRackLocationsButton_Click(object sender, EventArgs e)
        {
            try
            {
                var isMultipleEntryVisible = ZoneInput.Visible;

                ZoneInput.Visible = !isMultipleEntryVisible;
                SubZoneInput.Visible = !isMultipleEntryVisible;
                StartHeightInput.Visible = !isMultipleEntryVisible;
                EndHeightInput.Visible = !isMultipleEntryVisible;
                StartColumnInput.Visible = !isMultipleEntryVisible;
                EndColumnInput.Visible = !isMultipleEntryVisible;

                AddRackLocationsButton.Text = isMultipleEntryVisible
                    ? @"Switch to Multiple Entries"
                    : @"Switch to Single Entry";

                LocationInput.ReadOnly = !isMultipleEntryVisible;

                if (isMultipleEntryVisible)
                {
                    ZoneInput.Clear();
                    SubZoneInput.Clear();
                    StartHeightInput.Clear();
                    EndHeightInput.Clear();
                    StartColumnInput.Clear();
                    EndColumnInput.Clear();

                    LocationInput.PlaceholderText = @"Single Location";
                    LocationInput.TextAlign = HorizontalAlignment.Left;

                    LocationInput.Text = string.Empty;
                }
                else
                {
                    LocationInput.PlaceholderText = @"Multiple Locations";
                    LocationInput.TextAlign = HorizontalAlignment.Center;
                    ZoneInput.TextChanged += UpdateLocationRange;
                    SubZoneInput.TextChanged += UpdateLocationRange;
                    StartHeightInput.TextChanged += UpdateLocationRange;
                    EndHeightInput.TextChanged += UpdateLocationRange;
                    StartColumnInput.TextChanged += UpdateLocationRange;
                    EndColumnInput.TextChanged += UpdateLocationRange;
                    StartHeightInput.TextChanged += UnifiedEntryForm_ValidationHelper.ValidateSingleDigitInput;
                    EndHeightInput.TextChanged += UnifiedEntryForm_ValidationHelper.ValidateSingleDigitInput;
                }
            }
            catch (Exception ex)
            {
                AppLogger.Log($@"Error in AddRackLocationsButton_Click: {ex.Message}");
                MessageBox.Show($@"An error occurred while toggling entry mode:
{ex.Message}", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateLocationRange(object? sender, EventArgs e)
        {
            try
            {
                var zone = ZoneInput.Text.Trim();
                var subZone = SubZoneInput.Text.Trim();
                var startHeight = StartHeightInput.Text.Trim();
                var endHeight = EndHeightInput.Text.Trim();
                var startColumn = StartColumnInput.Text.Trim();
                var endColumn = EndColumnInput.Text.Trim();

                if (!string.IsNullOrEmpty(zone) &&
                    !string.IsNullOrEmpty(subZone) &&
                    int.TryParse(startHeight, out var startH) &&
                    int.TryParse(endHeight, out var endH) &&
                    int.TryParse(startColumn, out var startC) &&
                    int.TryParse(endColumn, out var endC) &&
                    startH >= 0 && startH <= 9 &&
                    endH >= 0 && endH <= 9 &&
                    startH <= endH &&
                    startC <= endC)
                {
                    LocationInput.Text =
                        $@"{zone}-{subZone}{startH:D1}-{startC:D2} to {zone}-{subZone}{endH:D1}-{endC:D2}";
                }
                else
                {
                    LocationInput.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                AppLogger.Log($@"Error in UpdateLocationRange: {ex.Message}");
                MessageBox.Show($@"An error occurred while updating the location range:
{ex.Message}", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AdminCheckBox_CheckedChanged(object? sender, EventArgs e)
        {
            if (AdminCheckBox.Checked)
            {
                ReadOnlyCheckBox.Checked = false;
            }
        }

        private void ReadOnlyCheckBox_CheckedChanged(object? sender, EventArgs e)
        {
            if (ReadOnlyCheckBox.Checked)
            {
                AdminCheckBox.Checked = false;
            }
        }
    }
}