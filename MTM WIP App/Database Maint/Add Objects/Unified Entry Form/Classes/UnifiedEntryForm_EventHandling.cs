using MTM_WIP_App.Main_Form;

namespace MTM_WIP_App.Database_Maint.Add_Objects.Unified_Entry_Form.Classes
{
    public static class UnifiedEntryForm_EventHandling
    {
        public static void ObjectTypeComboBox_SelectedIndexChanged(
            ComboBox objectTypeComboBox,
            Panel userPanel,
            Panel partPanel,
            Panel partTypePanel,
            Panel opPanel,
            Panel locationPanel, // Add LocationPanel parameter
            Button saveButton)
        {
            try
            {
                // Reset visibility of all panels
                userPanel.Visible = false;
                partPanel.Visible = false;
                partTypePanel.Visible = false;
                opPanel.Visible = false;
                locationPanel.Visible = false; // Reset LocationPanel visibility

                // Set visibility based on selected item
                switch (objectTypeComboBox.SelectedItem?.ToString())
                {
                    case "User":
                        userPanel.Visible = true;
                        break;
                    case "Part":
                        partPanel.Visible = true;
                        break;
                    case "Part Type":
                        partTypePanel.Visible = true;
                        break;
                    case "Operation":
                        opPanel.Visible = true;
                        break;
                    case "Location": // Handle LocationPanel visibility
                        locationPanel.Visible = true;
                        break;
                    default:
                        saveButton.Enabled = false;
                        return; // Exit early if no valid selection
                }

                // Enable the save button if a valid selection is made
                saveButton.Enabled = true;
            }
            catch (Exception ex)
            {
                // Log the error and rethrow it
                AppLogger.Log($"Error in ObjectTypeComboBox_SelectedIndexChanged: {ex.Message}");
                throw;
            }
        }
    }
}