namespace MTM_WIP_App.Main_Form
{
    sealed partial class MultipleLocations
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MultipleLocations));
            MultipleLocations_ComboBox_Location = new ComboBox();
            MultipleLocations_Label_Location = new Label();
            MultipleLocations_ToolTip_Location = new ToolTip(components);
            MultipleLocations_Button_Exit = new Button();
            MultipleLocations_Button_Save = new Button();
            MultipleLocations_Panel_Location = new Panel();
            MultipleLocations_TextBox_Quantity = new TextBox();
            MultipleLocations_Label_Quantity = new Label();
            MultipleLocations_Label_Count = new Label();
            MultipleLocations_Panel_Title = new Panel();
            MultipleLocations_Label_Part = new Label();
            MultipleLocations_Panel_Location.SuspendLayout();
            MultipleLocations_Panel_Title.SuspendLayout();
            SuspendLayout();
            // 
            // MultipleLocations_ComboBox_Location
            // 
            MultipleLocations_ComboBox_Location.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            MultipleLocations_ComboBox_Location.AutoCompleteSource = AutoCompleteSource.ListItems;
            MultipleLocations_ComboBox_Location.FormattingEnabled = true;
            MultipleLocations_ComboBox_Location.Location = new Point(65, 4);
            MultipleLocations_ComboBox_Location.Name = "MultipleLocations_ComboBox_Location";
            MultipleLocations_ComboBox_Location.Size = new Size(246, 23);
            MultipleLocations_ComboBox_Location.TabIndex = 0;
            MultipleLocations_ComboBox_Location.SelectedIndexChanged += MultipleLocations_ComboBox_Location_SelectedIndexChanged;
            MultipleLocations_ComboBox_Location.KeyDown += MultipleLocations_ComboBox_Location_KeyDown;
            MultipleLocations_ComboBox_Location.Leave += MultipleLocations_ComboBox_Location_Leave;
            // 
            // MultipleLocations_Label_Location
            // 
            MultipleLocations_Label_Location.AutoSize = true;
            MultipleLocations_Label_Location.Location = new Point(4, 8);
            MultipleLocations_Label_Location.Name = "MultipleLocations_Label_Location";
            MultipleLocations_Label_Location.Size = new Size(56, 15);
            MultipleLocations_Label_Location.TabIndex = 1;
            MultipleLocations_Label_Location.Text = "Location:";
            // 
            // MultipleLocations_ToolTip_Location
            // 
            MultipleLocations_ToolTip_Location.IsBalloon = true;
            MultipleLocations_ToolTip_Location.ToolTipIcon = ToolTipIcon.Info;
            MultipleLocations_ToolTip_Location.ToolTipTitle = "Tooltip";
            // 
            // MultipleLocations_Button_Exit
            // 
            MultipleLocations_Button_Exit.Location = new Point(240, 62);
            MultipleLocations_Button_Exit.Name = "MultipleLocations_Button_Exit";
            MultipleLocations_Button_Exit.Size = new Size(75, 23);
            MultipleLocations_Button_Exit.TabIndex = 3;
            MultipleLocations_Button_Exit.Text = "Exit";
            MultipleLocations_ToolTip_Location.SetToolTip(MultipleLocations_Button_Exit, "Click to finish adding parts.");
            MultipleLocations_Button_Exit.UseVisualStyleBackColor = true;
            MultipleLocations_Button_Exit.Click += MultipleLocations_Button_Exit_Click;
            // 
            // MultipleLocations_Button_Save
            // 
            MultipleLocations_Button_Save.Enabled = false;
            MultipleLocations_Button_Save.Location = new Point(4, 62);
            MultipleLocations_Button_Save.Name = "MultipleLocations_Button_Save";
            MultipleLocations_Button_Save.Size = new Size(75, 23);
            MultipleLocations_Button_Save.TabIndex = 2;
            MultipleLocations_Button_Save.Text = "Save / Next";
            MultipleLocations_ToolTip_Location.SetToolTip(MultipleLocations_Button_Save, "Click to save and add another part.\r\nNote: Click Exit after clicking \"Save / Next\"\r\nif this is your last location.");
            MultipleLocations_Button_Save.UseVisualStyleBackColor = true;
            MultipleLocations_Button_Save.Click += MultipleLocations_Button_Save_Click;
            // 
            // MultipleLocations_Panel_Location
            // 
            MultipleLocations_Panel_Location.AutoSize = true;
            MultipleLocations_Panel_Location.BorderStyle = BorderStyle.FixedSingle;
            MultipleLocations_Panel_Location.Controls.Add(MultipleLocations_TextBox_Quantity);
            MultipleLocations_Panel_Location.Controls.Add(MultipleLocations_Label_Quantity);
            MultipleLocations_Panel_Location.Controls.Add(MultipleLocations_Button_Save);
            MultipleLocations_Panel_Location.Controls.Add(MultipleLocations_Button_Exit);
            MultipleLocations_Panel_Location.Controls.Add(MultipleLocations_Label_Location);
            MultipleLocations_Panel_Location.Controls.Add(MultipleLocations_ComboBox_Location);
            MultipleLocations_Panel_Location.Controls.Add(MultipleLocations_Label_Count);
            MultipleLocations_Panel_Location.Dock = DockStyle.Bottom;
            MultipleLocations_Panel_Location.Location = new Point(0, 34);
            MultipleLocations_Panel_Location.Name = "MultipleLocations_Panel_Location";
            MultipleLocations_Panel_Location.Size = new Size(320, 90);
            MultipleLocations_Panel_Location.TabIndex = 2;
            // 
            // MultipleLocations_TextBox_Quantity
            // 
            MultipleLocations_TextBox_Quantity.Location = new Point(65, 33);
            MultipleLocations_TextBox_Quantity.Name = "MultipleLocations_TextBox_Quantity";
            MultipleLocations_TextBox_Quantity.Size = new Size(246, 23);
            MultipleLocations_TextBox_Quantity.TabIndex = 1;
            MultipleLocations_TextBox_Quantity.KeyDown += MultipleLocations_TextBox_Quantity_KeyDown;
            MultipleLocations_TextBox_Quantity.Leave += MultipleLocations_Quantity_Leave;
            // 
            // MultipleLocations_Label_Quantity
            // 
            MultipleLocations_Label_Quantity.AutoSize = true;
            MultipleLocations_Label_Quantity.Location = new Point(4, 37);
            MultipleLocations_Label_Quantity.Name = "MultipleLocations_Label_Quantity";
            MultipleLocations_Label_Quantity.Size = new Size(56, 15);
            MultipleLocations_Label_Quantity.TabIndex = 5;
            MultipleLocations_Label_Quantity.Text = "Quantity:";
            // 
            // MultipleLocations_Label_Count
            // 
            MultipleLocations_Label_Count.Dock = DockStyle.Bottom;
            MultipleLocations_Label_Count.Location = new Point(0, 65);
            MultipleLocations_Label_Count.Name = "MultipleLocations_Label_Count";
            MultipleLocations_Label_Count.Size = new Size(318, 23);
            MultipleLocations_Label_Count.TabIndex = 7;
            MultipleLocations_Label_Count.Text = "Transactions: 0";
            MultipleLocations_Label_Count.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // MultipleLocations_Panel_Title
            // 
            MultipleLocations_Panel_Title.BorderStyle = BorderStyle.FixedSingle;
            MultipleLocations_Panel_Title.Controls.Add(MultipleLocations_Label_Part);
            MultipleLocations_Panel_Title.Dock = DockStyle.Top;
            MultipleLocations_Panel_Title.Location = new Point(0, 0);
            MultipleLocations_Panel_Title.Name = "MultipleLocations_Panel_Title";
            MultipleLocations_Panel_Title.Size = new Size(320, 35);
            MultipleLocations_Panel_Title.TabIndex = 3;
            // 
            // MultipleLocations_Label_Part
            // 
            MultipleLocations_Label_Part.Dock = DockStyle.Fill;
            MultipleLocations_Label_Part.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point);
            MultipleLocations_Label_Part.Location = new Point(0, 0);
            MultipleLocations_Label_Part.Name = "MultipleLocations_Label_Part";
            MultipleLocations_Label_Part.Size = new Size(318, 33);
            MultipleLocations_Label_Part.TabIndex = 0;
            MultipleLocations_Label_Part.Text = "Part Number Label";
            MultipleLocations_Label_Part.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // MultipleLocations
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(320, 124);
            Controls.Add(MultipleLocations_Panel_Title);
            Controls.Add(MultipleLocations_Panel_Location);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "MultipleLocations";
            Text = "Multiple Location Entry";
            FormClosed += MultipleLocations_FormClosed;
            Load += MultipleLocations_Load;
            MultipleLocations_Panel_Location.ResumeLayout(false);
            MultipleLocations_Panel_Location.PerformLayout();
            MultipleLocations_Panel_Title.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox MultipleLocations_ComboBox_Location;
        private Label MultipleLocations_Label_Location;
        private ToolTip MultipleLocations_ToolTip_Location;
        private Panel MultipleLocations_Panel_Location;
        private Button MultipleLocations_Button_Save;
        private Button MultipleLocations_Button_Exit;
        private Panel MultipleLocations_Panel_Title;
        private Label MultipleLocations_Label_Part;
        private TextBox MultipleLocations_TextBox_Quantity;
        private Label MultipleLocations_Label_Quantity;
        private Label MultipleLocations_Label_Count;
    }
}