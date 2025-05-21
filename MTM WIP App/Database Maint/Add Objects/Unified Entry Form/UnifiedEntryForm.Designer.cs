namespace MTM_WIP_App.Database_Maint.Add_Objects.Unified_Entry_Form
{
    partial class UnifiedEntryForm
    {
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            ObjectTypeLabel = new Label();
            ObjectTypeComboBox = new ComboBox();
            UserPanel = new Panel();
            UserFirstName = new TextBox();
            UserLastName = new TextBox();
            UserEmail = new TextBox();
            UserPin = new TextBox();
            VitsCheckBox = new CheckBox();
            ShiftComboBox = new ComboBox();
            AdminCheckBox = new CheckBox();
            ReadOnlyCheckBox = new CheckBox();
            PartPanel = new Panel();
            PartInput = new TextBox();
            PartTypeComboBox = new ComboBox();
            PartTypePanel = new Panel();
            PartTypeInput = new TextBox();
            OpPanel = new Panel();
            OpInput = new TextBox();
            LocationPanel = new Panel();
            LocationInput = new TextBox();
            ZoneInput = new TextBox();
            SubZoneInput = new TextBox();
            StartHeightInput = new TextBox();
            EndHeightInput = new TextBox();
            StartColumnInput = new TextBox();
            EndColumnInput = new TextBox();
            AddRackLocationsButton = new Button();
            SaveButton = new Button();
            ExitButton = new Button();
            panel1 = new Panel();
            UserPanel.SuspendLayout();
            PartPanel.SuspendLayout();
            PartTypePanel.SuspendLayout();
            OpPanel.SuspendLayout();
            LocationPanel.SuspendLayout();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // ObjectTypeLabel
            // 
            ObjectTypeLabel.AutoSize = true;
            ObjectTypeLabel.Location = new Point(20, 20);
            ObjectTypeLabel.Name = "ObjectTypeLabel";
            ObjectTypeLabel.Size = new Size(73, 15);
            ObjectTypeLabel.TabIndex = 0;
            ObjectTypeLabel.Text = "Object Type:";
            // 
            // ObjectTypeComboBox
            // 
            ObjectTypeComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            ObjectTypeComboBox.FormattingEnabled = true;
            ObjectTypeComboBox.Items.AddRange(new object[] { "User", "Part", "Part Type", "Operation", "Location" });
            ObjectTypeComboBox.Location = new Point(130, 15);
            ObjectTypeComboBox.Name = "ObjectTypeComboBox";
            ObjectTypeComboBox.Size = new Size(450, 23);
            ObjectTypeComboBox.TabIndex = 0;
            ObjectTypeComboBox.SelectedIndexChanged += ObjectTypeComboBox_SelectedIndexChanged;
            // 
            // UserPanel
            // 
            UserPanel.BorderStyle = BorderStyle.FixedSingle;
            UserPanel.Controls.Add(UserFirstName);
            UserPanel.Controls.Add(UserLastName);
            UserPanel.Controls.Add(UserEmail);
            UserPanel.Controls.Add(UserPin);
            UserPanel.Controls.Add(VitsCheckBox);
            UserPanel.Controls.Add(ShiftComboBox);
            UserPanel.Controls.Add(AdminCheckBox);
            UserPanel.Controls.Add(ReadOnlyCheckBox);
            UserPanel.Location = new Point(20, 60);
            UserPanel.Name = "UserPanel";
            UserPanel.Size = new Size(560, 200);
            UserPanel.TabIndex = 2;
            UserPanel.Visible = false;
            // 
            // UserFirstName
            // 
            UserFirstName.Location = new Point(20, 20);
            UserFirstName.Name = "UserFirstName";
            UserFirstName.PlaceholderText = "First Name";
            UserFirstName.Size = new Size(200, 23);
            UserFirstName.TabIndex = 0;
            // 
            // UserLastName
            // 
            UserLastName.Location = new Point(20, 60);
            UserLastName.Name = "UserLastName";
            UserLastName.PlaceholderText = "Last Name";
            UserLastName.Size = new Size(200, 23);
            UserLastName.TabIndex = 1;
            // 
            // UserEmail
            // 
            UserEmail.Location = new Point(20, 100);
            UserEmail.Name = "UserEmail";
            UserEmail.PlaceholderText = "Email";
            UserEmail.Size = new Size(200, 23);
            UserEmail.TabIndex = 2;
            // 
            // UserPin
            // 
            UserPin.Location = new Point(20, 140);
            UserPin.Name = "UserPin";
            UserPin.PlaceholderText = "PIN (4 digits)";
            UserPin.Size = new Size(200, 23);
            UserPin.TabIndex = 3;
            // 
            // VitsCheckBox
            // 
            VitsCheckBox.AutoSize = true;
            VitsCheckBox.Location = new Point(240, 20);
            VitsCheckBox.Name = "VitsCheckBox";
            VitsCheckBox.Size = new Size(75, 19);
            VitsCheckBox.TabIndex = 4;
            VitsCheckBox.Text = "VITS User";
            // 
            // ShiftComboBox
            // 
            ShiftComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            ShiftComboBox.FormattingEnabled = true;
            ShiftComboBox.Location = new Point(240, 60);
            ShiftComboBox.Name = "ShiftComboBox";
            ShiftComboBox.Size = new Size(200, 23);
            ShiftComboBox.TabIndex = 5;
            // 
            // AdminCheckBox
            // 
            AdminCheckBox.AutoSize = true;
            AdminCheckBox.Location = new Point(240, 100);
            AdminCheckBox.Name = "AdminCheckBox";
            AdminCheckBox.Size = new Size(101, 19);
            AdminCheckBox.TabIndex = 6;
            AdminCheckBox.Text = "Admin Access";
            // 
            // ReadOnlyCheckBox
            // 
            ReadOnlyCheckBox.AutoSize = true;
            ReadOnlyCheckBox.Location = new Point(240, 140);
            ReadOnlyCheckBox.Name = "ReadOnlyCheckBox";
            ReadOnlyCheckBox.Size = new Size(121, 19);
            ReadOnlyCheckBox.TabIndex = 7;
            ReadOnlyCheckBox.Text = "Read-Only Access";
            // 
            // PartPanel
            // 
            PartPanel.BorderStyle = BorderStyle.FixedSingle;
            PartPanel.Controls.Add(PartInput);
            PartPanel.Controls.Add(PartTypeComboBox);
            PartPanel.Location = new Point(20, 60);
            PartPanel.Name = "PartPanel";
            PartPanel.Size = new Size(560, 100);
            PartPanel.TabIndex = 3;
            PartPanel.Visible = false;
            // 
            // PartInput
            // 
            PartInput.Location = new Point(20, 20);
            PartInput.Name = "PartInput";
            PartInput.PlaceholderText = "Part Number";
            PartInput.Size = new Size(200, 23);
            PartInput.TabIndex = 0;
            // 
            // PartTypeComboBox
            // 
            PartTypeComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            PartTypeComboBox.FormattingEnabled = true;
            PartTypeComboBox.Location = new Point(20, 60);
            PartTypeComboBox.Name = "PartTypeComboBox";
            PartTypeComboBox.Size = new Size(200, 23);
            PartTypeComboBox.TabIndex = 1;
            // 
            // PartTypePanel
            // 
            PartTypePanel.BorderStyle = BorderStyle.FixedSingle;
            PartTypePanel.Controls.Add(PartTypeInput);
            PartTypePanel.Location = new Point(20, 60);
            PartTypePanel.Name = "PartTypePanel";
            PartTypePanel.Size = new Size(560, 50);
            PartTypePanel.TabIndex = 4;
            PartTypePanel.Visible = false;
            // 
            // PartTypeInput
            // 
            PartTypeInput.Location = new Point(20, 20);
            PartTypeInput.Name = "PartTypeInput";
            PartTypeInput.PlaceholderText = "Part Type";
            PartTypeInput.Size = new Size(200, 23);
            PartTypeInput.TabIndex = 0;
            // 
            // OpPanel
            // 
            OpPanel.BorderStyle = BorderStyle.FixedSingle;
            OpPanel.Controls.Add(OpInput);
            OpPanel.Location = new Point(20, 60);
            OpPanel.Name = "OpPanel";
            OpPanel.Size = new Size(560, 50);
            OpPanel.TabIndex = 5;
            OpPanel.Visible = false;
            // 
            // OpInput
            // 
            OpInput.Location = new Point(20, 20);
            OpInput.Name = "OpInput";
            OpInput.PlaceholderText = "Operation Number";
            OpInput.Size = new Size(200, 23);
            OpInput.TabIndex = 0;
            // 
            // LocationPanel
            // 
            LocationPanel.BorderStyle = BorderStyle.FixedSingle;
            LocationPanel.Controls.Add(LocationInput);
            LocationPanel.Controls.Add(ZoneInput);
            LocationPanel.Controls.Add(SubZoneInput);
            LocationPanel.Controls.Add(StartHeightInput);
            LocationPanel.Controls.Add(EndHeightInput);
            LocationPanel.Controls.Add(StartColumnInput);
            LocationPanel.Controls.Add(EndColumnInput);
            LocationPanel.Controls.Add(AddRackLocationsButton);
            LocationPanel.Location = new Point(20, 60);
            LocationPanel.Name = "LocationPanel";
            LocationPanel.Size = new Size(560, 144);
            LocationPanel.TabIndex = 6;
            LocationPanel.Visible = false;
            // 
            // LocationInput
            // 
            LocationInput.Location = new Point(3, 16);
            LocationInput.Name = "LocationInput";
            LocationInput.PlaceholderText = "Single Location";
            LocationInput.Size = new Size(552, 23);
            LocationInput.TabIndex = 0;
            // 
            // ZoneInput
            // 
            ZoneInput.Location = new Point(3, 45);
            ZoneInput.Name = "ZoneInput";
            ZoneInput.PlaceholderText = "Zone (e.g., DD)";
            ZoneInput.Size = new Size(173, 23);
            ZoneInput.TabIndex = 1;
            ZoneInput.Visible = false;
            // 
            // SubZoneInput
            // 
            SubZoneInput.Location = new Point(188, 45);
            SubZoneInput.Name = "SubZoneInput";
            SubZoneInput.PlaceholderText = "SubZone (e.g., A)";
            SubZoneInput.Size = new Size(173, 23);
            SubZoneInput.TabIndex = 2;
            SubZoneInput.Visible = false;
            // 
            // StartHeightInput
            // 
            StartHeightInput.Location = new Point(382, 45);
            StartHeightInput.Name = "StartHeightInput";
            StartHeightInput.PlaceholderText = "Start Height";
            StartHeightInput.Size = new Size(173, 23);
            StartHeightInput.TabIndex = 3;
            StartHeightInput.Visible = false;
            // 
            // EndHeightInput
            // 
            EndHeightInput.Location = new Point(3, 74);
            EndHeightInput.Name = "EndHeightInput";
            EndHeightInput.PlaceholderText = "End Height";
            EndHeightInput.Size = new Size(173, 23);
            EndHeightInput.TabIndex = 4;
            EndHeightInput.Visible = false;
            // 
            // StartColumnInput
            // 
            StartColumnInput.Location = new Point(188, 74);
            StartColumnInput.Name = "StartColumnInput";
            StartColumnInput.PlaceholderText = "Start Column";
            StartColumnInput.Size = new Size(173, 23);
            StartColumnInput.TabIndex = 5;
            StartColumnInput.Visible = false;
            // 
            // EndColumnInput
            // 
            EndColumnInput.Location = new Point(382, 74);
            EndColumnInput.Name = "EndColumnInput";
            EndColumnInput.PlaceholderText = "End Column";
            EndColumnInput.Size = new Size(173, 23);
            EndColumnInput.TabIndex = 6;
            EndColumnInput.Visible = false;
            // 
            // AddRackLocationsButton
            // 
            AddRackLocationsButton.Location = new Point(3, 103);
            AddRackLocationsButton.Name = "AddRackLocationsButton";
            AddRackLocationsButton.Size = new Size(552, 30);
            AddRackLocationsButton.TabIndex = 7;
            AddRackLocationsButton.Text = "Switch to Multiple Entries";
            AddRackLocationsButton.UseVisualStyleBackColor = true;
            AddRackLocationsButton.Click += AddRackLocationsButton_Click;
            // 
            // SaveButton
            // 
            SaveButton.Location = new Point(384, 10);
            SaveButton.Name = "SaveButton";
            SaveButton.Size = new Size(100, 30);
            SaveButton.TabIndex = 8;
            SaveButton.Text = "Save";
            SaveButton.UseVisualStyleBackColor = true;
            SaveButton.Click += SaveButton_Click;
            // 
            // ExitButton
            // 
            ExitButton.Location = new Point(490, 10);
            ExitButton.Name = "ExitButton";
            ExitButton.Size = new Size(100, 30);
            ExitButton.TabIndex = 9;
            ExitButton.Text = "Exit";
            ExitButton.UseVisualStyleBackColor = true;
            ExitButton.Click += ExitButton_Click;
            // 
            // panel1
            // 
            panel1.Controls.Add(SaveButton);
            panel1.Controls.Add(ExitButton);
            panel1.Dock = DockStyle.Bottom;
            panel1.Location = new Point(0, 47);
            panel1.Name = "panel1";
            panel1.Size = new Size(600, 52);
            panel1.TabIndex = 10;
            // 
            // UnifiedEntryForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(600, 99);
            Controls.Add(LocationPanel);
            Controls.Add(ObjectTypeLabel);
            Controls.Add(ObjectTypeComboBox);
            Controls.Add(UserPanel);
            Controls.Add(PartPanel);
            Controls.Add(PartTypePanel);
            Controls.Add(OpPanel);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "UnifiedEntryForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Unified Entry Form";
            UserPanel.ResumeLayout(false);
            UserPanel.PerformLayout();
            PartPanel.ResumeLayout(false);
            PartPanel.PerformLayout();
            PartTypePanel.ResumeLayout(false);
            PartTypePanel.PerformLayout();
            OpPanel.ResumeLayout(false);
            OpPanel.PerformLayout();
            LocationPanel.ResumeLayout(false);
            LocationPanel.PerformLayout();
            panel1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        private System.Windows.Forms.ComboBox ObjectTypeComboBox;
        private System.Windows.Forms.Label ObjectTypeLabel;
        private System.Windows.Forms.Panel UserPanel;
        private System.Windows.Forms.TextBox UserFirstName;
        private System.Windows.Forms.TextBox UserLastName;
        private System.Windows.Forms.TextBox UserEmail;
        private System.Windows.Forms.TextBox UserPin;
        private System.Windows.Forms.CheckBox VitsCheckBox;
        private System.Windows.Forms.ComboBox ShiftComboBox;
        private System.Windows.Forms.CheckBox AdminCheckBox;
        private System.Windows.Forms.CheckBox ReadOnlyCheckBox;
        private System.Windows.Forms.Panel PartPanel;
        private System.Windows.Forms.TextBox PartInput;
        private System.Windows.Forms.ComboBox PartTypeComboBox;
        private System.Windows.Forms.Panel PartTypePanel;
        private System.Windows.Forms.TextBox PartTypeInput;
        private System.Windows.Forms.Panel OpPanel;
        private System.Windows.Forms.TextBox OpInput;
        private System.Windows.Forms.Panel LocationPanel;
        private System.Windows.Forms.TextBox LocationInput;
        private System.Windows.Forms.TextBox ZoneInput;
        private System.Windows.Forms.TextBox SubZoneInput;
        private System.Windows.Forms.TextBox StartHeightInput;
        private System.Windows.Forms.TextBox EndHeightInput;
        private System.Windows.Forms.TextBox StartColumnInput;
        private System.Windows.Forms.TextBox EndColumnInput;
        private System.Windows.Forms.Button AddRackLocationsButton;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.Button ExitButton;
        private Panel panel1;
    }
}