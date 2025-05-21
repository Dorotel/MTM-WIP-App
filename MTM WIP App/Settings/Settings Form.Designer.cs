namespace MTM_WIP_App.Settings
{
    sealed partial class SettingsForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            ServerSettings_Label_ServerAddress = new Label();
            ServerSettings_Label_Port = new Label();
            SettingsForm_GroupBox_ServerSettings = new GroupBox();
            ServerSettings_Label_ServerAddressShow = new Label();
            ServerSettings_TextBox_User = new MaskedTextBox();
            ServerSettings_TextBox_Port = new TextBox();
            ServerSettings_TextBox_ServerAddress = new TextBox();
            ServerSettings_Button_Reset = new Button();
            ServerSettings_Button_Update = new Button();
            VisualLogin_GroupBox = new GroupBox();
            VisualLogin_Button_Reset = new Button();
            VisualLogin_Button_Change = new Button();
            VisualLogin_TextBox_Password = new TextBox();
            VisualLogin_Label_Password = new Label();
            VisualLogin_TextBox_UserName = new TextBox();
            VisualLogin_Button_Save = new Button();
            VisualLogin_Label_UserName = new Label();
            SettingsForm_GroupBox_ResultsTheme = new GroupBox();
            label1 = new Label();
            ResultsTheme_NumericUpDown_FontSize = new NumericUpDown();
            ResultsTheme_Label_FontSize = new Label();
            ResultsTheme_Button_Reset = new Button();
            ResultsTheme_Button_Save = new Button();
            ResultsTheme_DataGridView_Preview = new DataGridView();
            ResultsTheme_DataGridView_Preview_Column1 = new DataGridViewTextBoxColumn();
            ResultsTheme_DataGridView_Preview_Column2 = new DataGridViewTextBoxColumn();
            ResultsTheme_TextBox_CurrentTheme = new TextBox();
            ResultsTheme_Label_CurrentTheme = new Label();
            ResultsTheme_ComboBox_Theme = new ComboBox();
            ResultsTheme_Label_Theme = new Label();
            SettingsForm_GroupBox_ServerSettings.SuspendLayout();
            VisualLogin_GroupBox.SuspendLayout();
            SettingsForm_GroupBox_ResultsTheme.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)ResultsTheme_NumericUpDown_FontSize).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ResultsTheme_DataGridView_Preview).BeginInit();
            SuspendLayout();
            // 
            // ServerSettings_Label_ServerAddress
            // 
            ServerSettings_Label_ServerAddress.AutoSize = true;
            ServerSettings_Label_ServerAddress.Location = new Point(6, 23);
            ServerSettings_Label_ServerAddress.Name = "ServerSettings_Label_ServerAddress";
            ServerSettings_Label_ServerAddress.Size = new Size(87, 15);
            ServerSettings_Label_ServerAddress.TabIndex = 2;
            ServerSettings_Label_ServerAddress.Text = "Server Address:";
            // 
            // ServerSettings_Label_Port
            // 
            ServerSettings_Label_Port.AutoSize = true;
            ServerSettings_Label_Port.Location = new Point(388, 23);
            ServerSettings_Label_Port.Name = "ServerSettings_Label_Port";
            ServerSettings_Label_Port.Size = new Size(32, 15);
            ServerSettings_Label_Port.TabIndex = 3;
            ServerSettings_Label_Port.Text = "Port:";
            // 
            // SettingsForm_GroupBox_ServerSettings
            // 
            SettingsForm_GroupBox_ServerSettings.Controls.Add(ServerSettings_Label_ServerAddressShow);
            SettingsForm_GroupBox_ServerSettings.Controls.Add(ServerSettings_TextBox_User);
            SettingsForm_GroupBox_ServerSettings.Controls.Add(ServerSettings_TextBox_Port);
            SettingsForm_GroupBox_ServerSettings.Controls.Add(ServerSettings_TextBox_ServerAddress);
            SettingsForm_GroupBox_ServerSettings.Controls.Add(ServerSettings_Button_Reset);
            SettingsForm_GroupBox_ServerSettings.Controls.Add(ServerSettings_Button_Update);
            SettingsForm_GroupBox_ServerSettings.Controls.Add(ServerSettings_Label_Port);
            SettingsForm_GroupBox_ServerSettings.Controls.Add(ServerSettings_Label_ServerAddress);
            SettingsForm_GroupBox_ServerSettings.Enabled = false;
            SettingsForm_GroupBox_ServerSettings.Location = new Point(12, 150);
            SettingsForm_GroupBox_ServerSettings.Name = "SettingsForm_GroupBox_ServerSettings";
            SettingsForm_GroupBox_ServerSettings.Size = new Size(495, 122);
            SettingsForm_GroupBox_ServerSettings.TabIndex = 4;
            SettingsForm_GroupBox_ServerSettings.TabStop = false;
            SettingsForm_GroupBox_ServerSettings.Text = "MySQL Server Settings";
            // 
            // ServerSettings_Label_ServerAddressShow
            // 
            ServerSettings_Label_ServerAddressShow.AutoSize = true;
            ServerSettings_Label_ServerAddressShow.Location = new Point(8, 46);
            ServerSettings_Label_ServerAddressShow.Name = "ServerSettings_Label_ServerAddressShow";
            ServerSettings_Label_ServerAddressShow.Size = new Size(87, 15);
            ServerSettings_Label_ServerAddressShow.TabIndex = 9;
            ServerSettings_Label_ServerAddressShow.Text = "Server Address:";
            // 
            // ServerSettings_TextBox_User
            // 
            ServerSettings_TextBox_User.BackColor = SystemColors.InactiveCaption;
            ServerSettings_TextBox_User.Location = new Point(99, 93);
            ServerSettings_TextBox_User.Name = "ServerSettings_TextBox_User";
            ServerSettings_TextBox_User.ReadOnly = true;
            ServerSettings_TextBox_User.Size = new Size(298, 23);
            ServerSettings_TextBox_User.TabIndex = 8;
            // 
            // ServerSettings_TextBox_Port
            // 
            ServerSettings_TextBox_Port.Location = new Point(426, 20);
            ServerSettings_TextBox_Port.Name = "ServerSettings_TextBox_Port";
            ServerSettings_TextBox_Port.Size = new Size(62, 23);
            ServerSettings_TextBox_Port.TabIndex = 7;
            ServerSettings_TextBox_Port.TextChanged += ServerButtonEnabler;
            // 
            // ServerSettings_TextBox_ServerAddress
            // 
            ServerSettings_TextBox_ServerAddress.Location = new Point(99, 20);
            ServerSettings_TextBox_ServerAddress.Name = "ServerSettings_TextBox_ServerAddress";
            ServerSettings_TextBox_ServerAddress.Size = new Size(283, 23);
            ServerSettings_TextBox_ServerAddress.TabIndex = 6;
            ServerSettings_TextBox_ServerAddress.TextChanged += ServerButtonEnabler;
            // 
            // ServerSettings_Button_Reset
            // 
            ServerSettings_Button_Reset.Location = new Point(403, 93);
            ServerSettings_Button_Reset.Name = "ServerSettings_Button_Reset";
            ServerSettings_Button_Reset.Size = new Size(86, 25);
            ServerSettings_Button_Reset.TabIndex = 5;
            ServerSettings_Button_Reset.Text = "Reset";
            ServerSettings_Button_Reset.UseVisualStyleBackColor = true;
            ServerSettings_Button_Reset.Click += ResetButton_Click;
            // 
            // ServerSettings_Button_Update
            // 
            ServerSettings_Button_Update.Enabled = false;
            ServerSettings_Button_Update.Location = new Point(8, 93);
            ServerSettings_Button_Update.Name = "ServerSettings_Button_Update";
            ServerSettings_Button_Update.Size = new Size(86, 25);
            ServerSettings_Button_Update.TabIndex = 4;
            ServerSettings_Button_Update.Text = "Update";
            ServerSettings_Button_Update.UseVisualStyleBackColor = true;
            ServerSettings_Button_Update.Click += UpdateButton_Click;
            // 
            // VisualLogin_GroupBox
            // 
            VisualLogin_GroupBox.Controls.Add(VisualLogin_Button_Reset);
            VisualLogin_GroupBox.Controls.Add(VisualLogin_Button_Change);
            VisualLogin_GroupBox.Controls.Add(VisualLogin_TextBox_Password);
            VisualLogin_GroupBox.Controls.Add(VisualLogin_Label_Password);
            VisualLogin_GroupBox.Controls.Add(VisualLogin_TextBox_UserName);
            VisualLogin_GroupBox.Controls.Add(VisualLogin_Button_Save);
            VisualLogin_GroupBox.Controls.Add(VisualLogin_Label_UserName);
            VisualLogin_GroupBox.Location = new Point(12, 12);
            VisualLogin_GroupBox.Name = "VisualLogin_GroupBox";
            VisualLogin_GroupBox.Size = new Size(495, 130);
            VisualLogin_GroupBox.TabIndex = 10;
            VisualLogin_GroupBox.TabStop = false;
            VisualLogin_GroupBox.Text = "Visual Login Info (Needed for Cross Communication)";
            // 
            // VisualLogin_Button_Reset
            // 
            VisualLogin_Button_Reset.Enabled = false;
            VisualLogin_Button_Reset.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            VisualLogin_Button_Reset.Location = new Point(216, 99);
            VisualLogin_Button_Reset.Name = "VisualLogin_Button_Reset";
            VisualLogin_Button_Reset.Size = new Size(86, 25);
            VisualLogin_Button_Reset.TabIndex = 10;
            VisualLogin_Button_Reset.Text = "Reset";
            VisualLogin_Button_Reset.UseVisualStyleBackColor = true;
            VisualLogin_Button_Reset.Click += VisualLogin_Button_Reset_Clicked;
            // 
            // VisualLogin_Button_Change
            // 
            VisualLogin_Button_Change.Location = new Point(6, 99);
            VisualLogin_Button_Change.Name = "VisualLogin_Button_Change";
            VisualLogin_Button_Change.Size = new Size(86, 25);
            VisualLogin_Button_Change.TabIndex = 9;
            VisualLogin_Button_Change.Text = "Change";
            VisualLogin_Button_Change.UseVisualStyleBackColor = true;
            VisualLogin_Button_Change.Click += VisualLogin_Button_Change_Clicked;
            // 
            // VisualLogin_TextBox_Password
            // 
            VisualLogin_TextBox_Password.Enabled = false;
            VisualLogin_TextBox_Password.Location = new Point(87, 49);
            VisualLogin_TextBox_Password.Name = "VisualLogin_TextBox_Password";
            VisualLogin_TextBox_Password.Size = new Size(401, 23);
            VisualLogin_TextBox_Password.TabIndex = 8;
            // 
            // VisualLogin_Label_Password
            // 
            VisualLogin_Label_Password.AutoSize = true;
            VisualLogin_Label_Password.Location = new Point(6, 52);
            VisualLogin_Label_Password.Name = "VisualLogin_Label_Password";
            VisualLogin_Label_Password.Size = new Size(60, 15);
            VisualLogin_Label_Password.TabIndex = 7;
            VisualLogin_Label_Password.Text = "Password:";
            // 
            // VisualLogin_TextBox_UserName
            // 
            VisualLogin_TextBox_UserName.Enabled = false;
            VisualLogin_TextBox_UserName.Location = new Point(87, 20);
            VisualLogin_TextBox_UserName.Name = "VisualLogin_TextBox_UserName";
            VisualLogin_TextBox_UserName.Size = new Size(401, 23);
            VisualLogin_TextBox_UserName.TabIndex = 6;
            // 
            // VisualLogin_Button_Save
            // 
            VisualLogin_Button_Save.Enabled = false;
            VisualLogin_Button_Save.Location = new Point(403, 99);
            VisualLogin_Button_Save.Name = "VisualLogin_Button_Save";
            VisualLogin_Button_Save.Size = new Size(86, 25);
            VisualLogin_Button_Save.TabIndex = 4;
            VisualLogin_Button_Save.Text = "Save";
            VisualLogin_Button_Save.UseVisualStyleBackColor = true;
            VisualLogin_Button_Save.Click += VisualLogin_Button_Save_Clicked;
            // 
            // VisualLogin_Label_UserName
            // 
            VisualLogin_Label_UserName.AutoSize = true;
            VisualLogin_Label_UserName.Location = new Point(6, 23);
            VisualLogin_Label_UserName.Name = "VisualLogin_Label_UserName";
            VisualLogin_Label_UserName.Size = new Size(75, 15);
            VisualLogin_Label_UserName.TabIndex = 2;
            VisualLogin_Label_UserName.Text = "Login Name:";
            // 
            // SettingsForm_GroupBox_ResultsTheme
            // 
            SettingsForm_GroupBox_ResultsTheme.Controls.Add(label1);
            SettingsForm_GroupBox_ResultsTheme.Controls.Add(ResultsTheme_NumericUpDown_FontSize);
            SettingsForm_GroupBox_ResultsTheme.Controls.Add(ResultsTheme_Label_FontSize);
            SettingsForm_GroupBox_ResultsTheme.Controls.Add(ResultsTheme_Button_Reset);
            SettingsForm_GroupBox_ResultsTheme.Controls.Add(ResultsTheme_Button_Save);
            SettingsForm_GroupBox_ResultsTheme.Controls.Add(ResultsTheme_DataGridView_Preview);
            SettingsForm_GroupBox_ResultsTheme.Controls.Add(ResultsTheme_TextBox_CurrentTheme);
            SettingsForm_GroupBox_ResultsTheme.Controls.Add(ResultsTheme_Label_CurrentTheme);
            SettingsForm_GroupBox_ResultsTheme.Controls.Add(ResultsTheme_ComboBox_Theme);
            SettingsForm_GroupBox_ResultsTheme.Controls.Add(ResultsTheme_Label_Theme);
            SettingsForm_GroupBox_ResultsTheme.Location = new Point(513, 12);
            SettingsForm_GroupBox_ResultsTheme.Name = "SettingsForm_GroupBox_ResultsTheme";
            SettingsForm_GroupBox_ResultsTheme.Size = new Size(384, 260);
            SettingsForm_GroupBox_ResultsTheme.TabIndex = 11;
            SettingsForm_GroupBox_ResultsTheme.TabStop = false;
            SettingsForm_GroupBox_ResultsTheme.Text = "Results Theme";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(141, 84);
            label1.Name = "label1";
            label1.Size = new Size(128, 15);
            label1.TabIndex = 9;
            label1.Text = "Don't Set this too high!";
            // 
            // ResultsTheme_NumericUpDown_FontSize
            // 
            ResultsTheme_NumericUpDown_FontSize.Location = new Point(99, 80);
            ResultsTheme_NumericUpDown_FontSize.Maximum = new decimal(new int[] { 30, 0, 0, 0 });
            ResultsTheme_NumericUpDown_FontSize.Minimum = new decimal(new int[] { 9, 0, 0, 0 });
            ResultsTheme_NumericUpDown_FontSize.Name = "ResultsTheme_NumericUpDown_FontSize";
            ResultsTheme_NumericUpDown_FontSize.Size = new Size(41, 23);
            ResultsTheme_NumericUpDown_FontSize.TabIndex = 8;
            ResultsTheme_NumericUpDown_FontSize.Value = new decimal(new int[] { 9, 0, 0, 0 });
            ResultsTheme_NumericUpDown_FontSize.ValueChanged += ResultsTheme_NumericUpDown_FontSize_ValueChanged;
            // 
            // ResultsTheme_Label_FontSize
            // 
            ResultsTheme_Label_FontSize.AutoSize = true;
            ResultsTheme_Label_FontSize.Location = new Point(36, 84);
            ResultsTheme_Label_FontSize.Name = "ResultsTheme_Label_FontSize";
            ResultsTheme_Label_FontSize.Size = new Size(57, 15);
            ResultsTheme_Label_FontSize.TabIndex = 7;
            ResultsTheme_Label_FontSize.Text = "Font Size:";
            // 
            // ResultsTheme_Button_Reset
            // 
            ResultsTheme_Button_Reset.Location = new Point(292, 105);
            ResultsTheme_Button_Reset.Name = "ResultsTheme_Button_Reset";
            ResultsTheme_Button_Reset.Size = new Size(86, 25);
            ResultsTheme_Button_Reset.TabIndex = 6;
            ResultsTheme_Button_Reset.Text = "Default";
            ResultsTheme_Button_Reset.UseVisualStyleBackColor = true;
            ResultsTheme_Button_Reset.Click += ResultsTheme_Button_Reset_Click;
            // 
            // ResultsTheme_Button_Save
            // 
            ResultsTheme_Button_Save.Location = new Point(7, 105);
            ResultsTheme_Button_Save.Name = "ResultsTheme_Button_Save";
            ResultsTheme_Button_Save.Size = new Size(86, 25);
            ResultsTheme_Button_Save.TabIndex = 5;
            ResultsTheme_Button_Save.Text = "Set Theme";
            ResultsTheme_Button_Save.UseVisualStyleBackColor = true;
            ResultsTheme_Button_Save.Click += ResultsTheme_Button_Save_Click;
            // 
            // ResultsTheme_DataGridView_Preview
            // 
            ResultsTheme_DataGridView_Preview.AllowUserToAddRows = false;
            ResultsTheme_DataGridView_Preview.AllowUserToDeleteRows = false;
            ResultsTheme_DataGridView_Preview.AllowUserToResizeColumns = false;
            ResultsTheme_DataGridView_Preview.AllowUserToResizeRows = false;
            ResultsTheme_DataGridView_Preview.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            ResultsTheme_DataGridView_Preview.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
            ResultsTheme_DataGridView_Preview.BorderStyle = BorderStyle.Fixed3D;
            ResultsTheme_DataGridView_Preview.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            ResultsTheme_DataGridView_Preview.Columns.AddRange(new DataGridViewColumn[] { ResultsTheme_DataGridView_Preview_Column1, ResultsTheme_DataGridView_Preview_Column2 });
            ResultsTheme_DataGridView_Preview.Location = new Point(8, 136);
            ResultsTheme_DataGridView_Preview.MultiSelect = false;
            ResultsTheme_DataGridView_Preview.Name = "ResultsTheme_DataGridView_Preview";
            ResultsTheme_DataGridView_Preview.ReadOnly = true;
            ResultsTheme_DataGridView_Preview.RowTemplate.Height = 25;
            ResultsTheme_DataGridView_Preview.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            ResultsTheme_DataGridView_Preview.ShowCellErrors = false;
            ResultsTheme_DataGridView_Preview.ShowCellToolTips = false;
            ResultsTheme_DataGridView_Preview.ShowEditingIcon = false;
            ResultsTheme_DataGridView_Preview.ShowRowErrors = false;
            ResultsTheme_DataGridView_Preview.Size = new Size(370, 116);
            ResultsTheme_DataGridView_Preview.TabIndex = 4;
            // 
            // ResultsTheme_DataGridView_Preview_Column1
            // 
            ResultsTheme_DataGridView_Preview_Column1.Frozen = true;
            ResultsTheme_DataGridView_Preview_Column1.HeaderText = "Column1";
            ResultsTheme_DataGridView_Preview_Column1.Name = "ResultsTheme_DataGridView_Preview_Column1";
            ResultsTheme_DataGridView_Preview_Column1.ReadOnly = true;
            ResultsTheme_DataGridView_Preview_Column1.Width = 81;
            // 
            // ResultsTheme_DataGridView_Preview_Column2
            // 
            ResultsTheme_DataGridView_Preview_Column2.Frozen = true;
            ResultsTheme_DataGridView_Preview_Column2.HeaderText = "Column2";
            ResultsTheme_DataGridView_Preview_Column2.Name = "ResultsTheme_DataGridView_Preview_Column2";
            ResultsTheme_DataGridView_Preview_Column2.ReadOnly = true;
            ResultsTheme_DataGridView_Preview_Column2.Width = 81;
            // 
            // ResultsTheme_TextBox_CurrentTheme
            // 
            ResultsTheme_TextBox_CurrentTheme.Location = new Point(99, 51);
            ResultsTheme_TextBox_CurrentTheme.Name = "ResultsTheme_TextBox_CurrentTheme";
            ResultsTheme_TextBox_CurrentTheme.ReadOnly = true;
            ResultsTheme_TextBox_CurrentTheme.Size = new Size(279, 23);
            ResultsTheme_TextBox_CurrentTheme.TabIndex = 3;
            ResultsTheme_TextBox_CurrentTheme.WordWrap = false;
            // 
            // ResultsTheme_Label_CurrentTheme
            // 
            ResultsTheme_Label_CurrentTheme.AutoSize = true;
            ResultsTheme_Label_CurrentTheme.Location = new Point(7, 55);
            ResultsTheme_Label_CurrentTheme.Name = "ResultsTheme_Label_CurrentTheme";
            ResultsTheme_Label_CurrentTheme.Size = new Size(90, 15);
            ResultsTheme_Label_CurrentTheme.TabIndex = 2;
            ResultsTheme_Label_CurrentTheme.Text = "Current Theme:";
            // 
            // ResultsTheme_ComboBox_Theme
            // 
            ResultsTheme_ComboBox_Theme.FormattingEnabled = true;
            ResultsTheme_ComboBox_Theme.Items.AddRange(new object[] { "[ Pick a Theme ]", "Default (Black and White)", "Light Blue", "Light Red", "Light Grey" });
            ResultsTheme_ComboBox_Theme.Location = new Point(99, 22);
            ResultsTheme_ComboBox_Theme.Name = "ResultsTheme_ComboBox_Theme";
            ResultsTheme_ComboBox_Theme.Size = new Size(279, 23);
            ResultsTheme_ComboBox_Theme.TabIndex = 1;
            ResultsTheme_ComboBox_Theme.SelectedIndexChanged += ResultsTheme_ComboBox_Theme_SelectedIndexChanged;
            // 
            // ResultsTheme_Label_Theme
            // 
            ResultsTheme_Label_Theme.AutoSize = true;
            ResultsTheme_Label_Theme.Location = new Point(8, 26);
            ResultsTheme_Label_Theme.Name = "ResultsTheme_Label_Theme";
            ResultsTheme_Label_Theme.Size = new Size(90, 15);
            ResultsTheme_Label_Theme.TabIndex = 0;
            ResultsTheme_Label_Theme.Text = "Choose Theme:";
            // 
            // SettingsForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(909, 281);
            Controls.Add(SettingsForm_GroupBox_ResultsTheme);
            Controls.Add(VisualLogin_GroupBox);
            Controls.Add(SettingsForm_GroupBox_ServerSettings);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximumSize = new Size(925, 320);
            MinimumSize = new Size(925, 320);
            Name = "SettingsForm";
            Text = "Settings";
            TopMost = true;
            Load += SettingsForm_Load;
            SettingsForm_GroupBox_ServerSettings.ResumeLayout(false);
            SettingsForm_GroupBox_ServerSettings.PerformLayout();
            VisualLogin_GroupBox.ResumeLayout(false);
            VisualLogin_GroupBox.PerformLayout();
            SettingsForm_GroupBox_ResultsTheme.ResumeLayout(false);
            SettingsForm_GroupBox_ResultsTheme.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)ResultsTheme_NumericUpDown_FontSize).EndInit();
            ((System.ComponentModel.ISupportInitialize)ResultsTheme_DataGridView_Preview).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private Label ServerSettings_Label_ServerAddress;
        private Label ServerSettings_Label_Port;
        private GroupBox SettingsForm_GroupBox_ServerSettings;
        private Button ServerSettings_Button_Reset;
        private Button ServerSettings_Button_Update;
        private TextBox ServerSettings_TextBox_Port;
        private TextBox ServerSettings_TextBox_ServerAddress;
        private MaskedTextBox ServerSettings_TextBox_User;
        private Label ServerSettings_Label_ServerAddressShow;
        private GroupBox VisualLogin_GroupBox;
        private TextBox VisualLogin_TextBox_UserName;
        private Button VisualLogin_Button_Save;
        private Label VisualLogin_Label_UserName;
        private GroupBox SettingsForm_GroupBox_ResultsTheme;
        private TextBox ResultsTheme_TextBox_CurrentTheme;
        private Label ResultsTheme_Label_CurrentTheme;
        private ComboBox ResultsTheme_ComboBox_Theme;
        private Label ResultsTheme_Label_Theme;
        private DataGridView ResultsTheme_DataGridView_Preview;
        private DataGridViewTextBoxColumn ResultsTheme_DataGridView_Preview_Column1;
        private DataGridViewTextBoxColumn ResultsTheme_DataGridView_Preview_Column2;
        private Button ResultsTheme_Button_Reset;
        private Button ResultsTheme_Button_Save;
        private Label ResultsTheme_Label_FontSize;
        private NumericUpDown ResultsTheme_NumericUpDown_FontSize;
        private Label label1;
        private TextBox VisualLogin_TextBox_Password;
        private Label VisualLogin_Label_Password;
        private Button VisualLogin_Button_Change;
        private Button VisualLogin_Button_Reset;
    }
}