using MTM_WIP_App.Database_Maint.Remove_Objects.Unified_Removal_Form.Classes;
using MTM_WIP_App.Main_Form;

namespace MTM_WIP_App.Database_Maint.Remove_Objects.Unified_Removal_Form
{
    public partial class UnifiedRemovalForm : Form
    {
        private readonly string _connectionString = SqlVariables.GetConnectionString(null, null, null, null);

        public UnifiedRemovalForm()
        {
            InitializeComponent();
            AppLogger.Log(@"UnifiedRemovalForm initialized.");
        }

        private void ObjectTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var selectedObjectType = ObjectTypeComboBox.SelectedItem?.ToString();
                if (string.IsNullOrEmpty(selectedObjectType))
                {
                    return;
                }

                var data = UnifiedRemovalForm_RemovalHelper.GetDataForObjectType(selectedObjectType, _connectionString);
                RemovalDataGridView.DataSource = data;

                if (RemovalDataGridView.Columns.Contains(@"ID"))
                {
                    RemovalDataGridView.Columns[@"ID"].Visible = false;
                }
            }
            catch (Exception ex)
            {
                AppLogger.Log($@"Error in ObjectTypeComboBox_SelectedIndexChanged: {ex.Message}");
                MessageBox.Show($@"An error occurred while loading data:
{ex.Message}", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (RemovalDataGridView.SelectedRows.Count == 0)
                {
                    MessageBox.Show(@"Please select at least one item to delete.", @"Validation Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var selectedIds = UnifiedRemovalForm_RemovalHelper.GetSelectedIds(RemovalDataGridView);
                var selectedObjectType = ObjectTypeComboBox.SelectedItem?.ToString();

                if (string.IsNullOrEmpty(selectedObjectType))
                {
                    return;
                }

                var result = MessageBox.Show($@"Are you sure you want to delete {selectedIds.Count} item(s)?",
                    @"Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    if (selectedObjectType == @"User")
                    {
                        foreach (var userId in selectedIds)
                        {
                            var userEmail =
                                UnifiedRemovalForm_RemovalHelper.GetUserEmailById(userId, _connectionString);

                            UnifiedRemovalForm_RemovalHelper.RemoveUserFromLeads(userEmail, _connectionString);
                            UnifiedRemovalForm_RemovalHelper.RemoveUserFromReadOnly(userEmail, _connectionString);
                            UnifiedRemovalForm_RemovalHelper.RemoveUserFromMySqlServer(userEmail, _connectionString);
                        }
                    }

                    UnifiedRemovalForm_RemovalDatabaseOperations.DeleteEntries(selectedObjectType, selectedIds,
                        _connectionString);

                    MessageBox.Show(@"Selected items deleted successfully.", @"Success", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    ObjectTypeComboBox_SelectedIndexChanged(null, null);
                }
            }
            catch (Exception ex)
            {
                AppLogger.Log($@"Error in DeleteButton_Click: {ex.Message}");
                MessageBox.Show($@"An error occurred while deleting items:
{ex.Message}", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            Close();
            AppLogger.Log(@"UnifiedRemovalForm closed.");
        }
    }
}