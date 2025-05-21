using MTM_WIP_App.Main_Form;

namespace MTM_WIP_App.Database_Maint.Add_Objects.Unified_Entry_Form.Classes
{
    public static class UnifiedEntryForm_FormHelper
    {
        public static void ResetForm(TextBox userFirstName, TextBox userLastName, TextBox userEmail, TextBox userPin,
            CheckBox vitsCheckBox, CheckBox adminCheckBox, CheckBox readOnlyCheckBox, TextBox partInput,
            ComboBox partTypeComboBox, ComboBox objectTypeComboBox, Button saveButton)
        {
            userFirstName.Clear();
            userLastName.Clear();
            userEmail.Clear();
            userPin.Clear();
            vitsCheckBox.Checked = false;
            adminCheckBox.Checked = false;
            readOnlyCheckBox.Checked = false;
            partInput.Clear();
            partTypeComboBox.SelectedIndex = 0;
            objectTypeComboBox.SelectedIndex = -1;
            saveButton.Enabled = false;

            AppLogger.Log("Form reset.");
        }
    }
}