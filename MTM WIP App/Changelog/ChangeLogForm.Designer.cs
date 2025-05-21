namespace MTM_WIP_App.Changelog
{
    partial class ChangeLogForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChangeLogForm));
            ChangeLog_RichTextBox_Log = new RichTextBox();
            ChangeLog_Label_Version = new Label();
            ChangeLog_CheckBox_Hide = new CheckBox();
            ChangeLog_Button_Close = new Button();
            ChangeLog_Button_Update = new Button();
            ChangeLog_ComboBox_Version = new ComboBox();
            SuspendLayout();
            // 
            // ChangeLog_RichTextBox_Log
            // 
            ChangeLog_RichTextBox_Log.Location = new Point(12, 32);
            ChangeLog_RichTextBox_Log.Name = "ChangeLog_RichTextBox_Log";
            ChangeLog_RichTextBox_Log.Size = new Size(776, 381);
            ChangeLog_RichTextBox_Log.TabIndex = 0;
            ChangeLog_RichTextBox_Log.Text = "";
            // 
            // ChangeLog_Label_Version
            // 
            ChangeLog_Label_Version.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            ChangeLog_Label_Version.AutoSize = true;
            ChangeLog_Label_Version.Location = new Point(632, 9);
            ChangeLog_Label_Version.Name = "ChangeLog_Label_Version";
            ChangeLog_Label_Version.Size = new Size(48, 15);
            ChangeLog_Label_Version.TabIndex = 2;
            ChangeLog_Label_Version.Text = "Version:";
            ChangeLog_Label_Version.TextAlign = ContentAlignment.MiddleRight;
            // 
            // ChangeLog_CheckBox_Hide
            // 
            ChangeLog_CheckBox_Hide.AutoSize = true;
            ChangeLog_CheckBox_Hide.Location = new Point(18, 423);
            ChangeLog_CheckBox_Hide.Name = "ChangeLog_CheckBox_Hide";
            ChangeLog_CheckBox_Hide.Size = new Size(174, 19);
            ChangeLog_CheckBox_Hide.TabIndex = 3;
            ChangeLog_CheckBox_Hide.Text = "Don't show until next patch.";
            ChangeLog_CheckBox_Hide.UseVisualStyleBackColor = true;
            ChangeLog_CheckBox_Hide.Click += ChangeLog_CheckBox_Hide_Clicked;
            // 
            // ChangeLog_Button_Close
            // 
            ChangeLog_Button_Close.Location = new Point(713, 420);
            ChangeLog_Button_Close.Name = "ChangeLog_Button_Close";
            ChangeLog_Button_Close.Size = new Size(75, 23);
            ChangeLog_Button_Close.TabIndex = 4;
            ChangeLog_Button_Close.Text = "Close";
            ChangeLog_Button_Close.UseVisualStyleBackColor = true;
            ChangeLog_Button_Close.Click += ChangeLog_Button_Close_Click;
            // 
            // ChangeLog_Button_Update
            // 
            ChangeLog_Button_Update.Location = new Point(632, 420);
            ChangeLog_Button_Update.Name = "ChangeLog_Button_Update";
            ChangeLog_Button_Update.Size = new Size(75, 23);
            ChangeLog_Button_Update.TabIndex = 5;
            ChangeLog_Button_Update.Text = "Update";
            ChangeLog_Button_Update.UseVisualStyleBackColor = true;
            ChangeLog_Button_Update.Click += ChangeLog_Button_Update_Click;
            // 
            // ChangeLog_ComboBox_Version
            // 
            ChangeLog_ComboBox_Version.FormattingEnabled = true;
            ChangeLog_ComboBox_Version.Location = new Point(686, 6);
            ChangeLog_ComboBox_Version.Name = "ChangeLog_ComboBox_Version";
            ChangeLog_ComboBox_Version.Size = new Size(102, 23);
            ChangeLog_ComboBox_Version.TabIndex = 6;
            // 
            // ChangeLogForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(ChangeLog_ComboBox_Version);
            Controls.Add(ChangeLog_Button_Update);
            Controls.Add(ChangeLog_Button_Close);
            Controls.Add(ChangeLog_CheckBox_Hide);
            Controls.Add(ChangeLog_Label_Version);
            Controls.Add(ChangeLog_RichTextBox_Log);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "ChangeLogForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Manitowoc Tool and Manufacturing WIP Application Change Log";
            TopMost = true;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private RichTextBox ChangeLog_RichTextBox_Log;
        private Label ChangeLog_Label_Version;
        private CheckBox ChangeLog_CheckBox_Hide;
        private Button ChangeLog_Button_Close;
        private Button ChangeLog_Button_Update;
        private ComboBox ChangeLog_ComboBox_Version;
    }
}