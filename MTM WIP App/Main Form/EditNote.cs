namespace MTM_WIP_App.Main_Form
{
    internal partial class RemovalTabEditNote : Form
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="RemovalTabEditNote" /> class.
        /// </summary>
        /// <param name="changeNotesArray">The array of ChangeNotes objects to be edited.</param>
        internal RemovalTabEditNote(ChangeNotes[] changeNotesArray)
        {
            try
            {
                InitializeComponent();

                // Adjust font and layout for DPI
                FontScaler.AdjustFontAndLayout(this);

                _changeNotesArray = changeNotesArray;
                _max = _changeNotesArray.Length;
                RemovalTab_EditNote_FillControllers();
                if (_max <= 1)
                {
                    RemovalTab_EditNotes_Button_Skip.Visible = false;
                    RemovalTab_EditNotes_Button_Skip.Enabled = false;
                }

                AppLogger.Log("RemovalTab_EditNote initialized with " + _max + " change notes.");
            }
            catch (Exception ex)
            {
                AppLogger.LogDatabaseError(ex);
                AppLogger.Log("Error in RemovalTab_EditNote (Initialize): " + ex.Message);
                SearchDao.HandleException_GeneralError_CloseApp(ex, nameof(RemovalTabEditNote));
            }
        }

        private static int _current = 1;
        private static string _currentNote;
        private static int _max;
        private readonly ChangeNotes[] _changeNotesArray;

        /// <summary>
        ///     Fills the controllers with the current ChangeNotes data.
        /// </summary>
        private void RemovalTab_EditNote_FillControllers()
        {
            try
            {
                AppLogger.Log("Filling controllers for note " + _current + " of " + _max + ".");
                RemovalTab_EditNotes_TransactionNumber2.Text = _current + @" / " + _max;
                if (_current == _max)
                {
                    RemovalTab_EditNotes_Button_Save.Text = @"Save";
                    RemovalTab_EditNotes_Button_Skip.Visible = false;
                }
                else
                {
                    RemovalTab_EditNotes_Button_Save.Text = @"Next";
                    RemovalTab_EditNotes_Button_Skip.Visible = true;
                }

                // Set the notes field in the TextBox to the note field in the current position of the array
                RemovalTab_EditNotes_RichTextBox_Notes.Text = _changeNotesArray[_current - 1].Note;
                RemovalTab_EditNotes_TextBox_Location.Text = _changeNotesArray[_current - 1].Location;
                RemovalTab_EditNotes_TextBox_Operation.Text = _changeNotesArray[_current - 1].Operation;
                RemovalTab_EditNotes_TextBox_PartID.Text = _changeNotesArray[_current - 1].PartId;
                RemovalTab_EditNotes_Button_Save.Enabled = false;
                _currentNote = RemovalTab_EditNotes_RichTextBox_Notes.Text;
                RemovalTab_EditNotes_RichTextBox_Notes.Focus();
                RemovalTab_EditNotes_RichTextBox_Notes.SelectAll();
            }
            catch (Exception ex)
            {
                AppLogger.LogDatabaseError(ex);
                AppLogger.Log("Error in RemovalTab_EditNote_FillControllers: " + ex.Message);
                SearchDao.HandleException_GeneralError_CloseApp(ex);
            }
        }

        /// <summary>
        ///     Handles the click event for the Save button.
        /// </summary>
        private void RemovalTab_EditNotes_Button_Save_Click(object sender, EventArgs e)
        {
            try
            {
                if (RemovalTab_EditNotes_Button_Save.Text == @"Exit")
                {
                    Close();
                }
                else
                {
                    AppLogger.Log("Saving note for part ID: " + _changeNotesArray[_current - 1].PartId);
                    SearchDao.RemovalTab_EditNotes(_changeNotesArray[_current - 1].Id,
                        _changeNotesArray[_current - 1].PartId!,
                        RemovalTab_EditNotes_RichTextBox_Notes.Text);
                    RemovalTab_EditNotes_StatusStrip.Text =
                        @"Updated Notes for " + _changeNotesArray[_current - 1].PartId +
                        @" @ Location: " + _changeNotesArray[_current - 1].Location +
                        @".";
                    if (_current != _max)
                    {
                        _current = _current + 1;
                        RemovalTab_EditNote_FillControllers();
                    }
                    else
                    {
                        RemovalTab_EditNotes_RichTextBox_Notes.Enabled = false;
                        RemovalTab_EditNotes_Button_Save.Text = @"Exit";
                    }
                }
            }
            catch (Exception ex)
            {
                AppLogger.LogDatabaseError(ex);
                AppLogger.Log("Error in RemovalTab_EditNotes_Button_Save_Click: " + ex.Message);
                SearchDao.HandleException_GeneralError_CloseApp(ex);
            }
        }

        /// <summary>
        ///     Handles the click event for the Skip button.
        /// </summary>
        private void RemovalTab_EditNotes_Button_Skip_Click(object sender, EventArgs e)
        {
            try
            {
                AppLogger.Log("Skipping note for part ID: " + _changeNotesArray[_current - 1].PartId);
                if (_current <= _max)
                {
                    _current = _current + 1;
                    RemovalTab_EditNote_FillControllers();
                }
                else
                {
                    RemovalTab_EditNote_FillControllers();
                }
            }
            catch (Exception ex)
            {
                AppLogger.LogDatabaseError(ex);
                AppLogger.Log("Error in RemovalTab_EditNotes_Button_Skip_Click: " + ex.Message);
                SearchDao.HandleException_GeneralError_CloseApp(ex);
            }
        }

        /// <summary>
        ///     Handles the TextChanged event for the Notes RichTextBox.
        /// </summary>
        private void RemovalTab_EditNotes_RichTextBox_Notes_TextChanged(object sender, EventArgs e)
        {
            try
            {
                RemovalTab_EditNotes_StatusStrip.Text = RemovalTab_EditNotes_RichTextBox_Notes.Text;
                if (_currentNote == RemovalTab_EditNotes_RichTextBox_Notes.Text)
                {
                    RemovalTab_EditNotes_Button_Save.Enabled = false;
                }
                else
                {
                    RemovalTab_EditNotes_Button_Save.Enabled = true;
                }

                AppLogger.Log("Notes text changed for part ID: " + _changeNotesArray[_current - 1].PartId);
            }
            catch (Exception ex)
            {
                AppLogger.LogDatabaseError(ex);
                AppLogger.Log("Error in RemovalTab_EditNotes_RichTextBox_Notes_TextChanged: " + ex.Message);
                SearchDao.HandleException_GeneralError_CloseApp(ex);
            }
        }
    }
}