namespace MTM_WIP_App
{
    partial class LoginPromptForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label UsernameLabel;
        private System.Windows.Forms.ComboBox UsernameComboBox;
        private System.Windows.Forms.Label PinLabel;
        private System.Windows.Forms.TextBox PinTextBox;
        private System.Windows.Forms.Button LoginButton;
        private System.Windows.Forms.Button NewUserButton;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginPromptForm));
            UsernameLabel = new Label();
            UsernameComboBox = new ComboBox();
            PinLabel = new Label();
            PinTextBox = new TextBox();
            LoginButton = new Button();
            NewUserButton = new Button();
            SuspendLayout();
            // 
            // UsernameLabel
            // 
            UsernameLabel.AutoSize = true;
            UsernameLabel.Location = new Point(12, 15);
            UsernameLabel.Name = "UsernameLabel";
            UsernameLabel.Size = new Size(63, 15);
            UsernameLabel.TabIndex = 0;
            UsernameLabel.Text = "Username:";
            // 
            // UsernameComboBox
            // 
            UsernameComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            UsernameComboBox.FormattingEnabled = true;
            UsernameComboBox.Location = new Point(86, 12);
            UsernameComboBox.Name = "UsernameComboBox";
            UsernameComboBox.Size = new Size(186, 23);
            UsernameComboBox.TabIndex = 1;
            // 
            // PinLabel
            // 
            PinLabel.AutoSize = true;
            PinLabel.Location = new Point(12, 50);
            PinLabel.Name = "PinLabel";
            PinLabel.Size = new Size(29, 15);
            PinLabel.TabIndex = 2;
            PinLabel.Text = "PIN:";
            // 
            // PinTextBox
            // 
            PinTextBox.Location = new Point(86, 47);
            PinTextBox.Name = "PinTextBox";
            PinTextBox.PasswordChar = '*';
            PinTextBox.Size = new Size(186, 23);
            PinTextBox.TabIndex = 3;
            // 
            // LoginButton
            // 
            LoginButton.Location = new Point(86, 85);
            LoginButton.Name = "LoginButton";
            LoginButton.Size = new Size(75, 23);
            LoginButton.TabIndex = 4;
            LoginButton.Text = "Login";
            LoginButton.UseVisualStyleBackColor = true;
            LoginButton.Click += LoginButton_Click;
            // 
            // NewUserButton
            // 
            NewUserButton.Location = new Point(197, 85);
            NewUserButton.Name = "NewUserButton";
            NewUserButton.Size = new Size(75, 23);
            NewUserButton.TabIndex = 5;
            NewUserButton.Text = "New User";
            NewUserButton.UseVisualStyleBackColor = true;
            NewUserButton.Click += NewUserButton_Click;
            // 
            // LoginPromptForm
            // 
            ClientSize = new Size(284, 121);
            Controls.Add(NewUserButton);
            Controls.Add(LoginButton);
            Controls.Add(PinTextBox);
            Controls.Add(PinLabel);
            Controls.Add(UsernameComboBox);
            Controls.Add(UsernameLabel);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "LoginPromptForm";
            Text = "Login";
            Load += LoginPromptForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }
    }
}