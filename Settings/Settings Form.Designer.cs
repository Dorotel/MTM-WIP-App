﻿namespace Settings
{
    partial class SettingsForm
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
            label1 = new Label();
            label2 = new Label();
            groupBox1 = new GroupBox();
            PortBox = new TextBox();
            ServerAddressBox = new TextBox();
            button2 = new Button();
            button1 = new Button();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(6, 23);
            label1.Name = "label1";
            label1.Size = new Size(87, 15);
            label1.TabIndex = 2;
            label1.Text = "Server Address:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(388, 23);
            label2.Name = "label2";
            label2.Size = new Size(32, 15);
            label2.TabIndex = 3;
            label2.Text = "Port:";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(PortBox);
            groupBox1.Controls.Add(ServerAddressBox);
            groupBox1.Controls.Add(button2);
            groupBox1.Controls.Add(button1);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(label1);
            groupBox1.Location = new Point(11, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(495, 87);
            groupBox1.TabIndex = 4;
            groupBox1.TabStop = false;
            groupBox1.Text = "MySQL Server Settings";
            // 
            // PortBox
            // 
            PortBox.Location = new Point(426, 20);
            PortBox.Name = "PortBox";
            PortBox.Size = new Size(62, 23);
            PortBox.TabIndex = 7;
            // 
            // ServerAddressBox
            // 
            ServerAddressBox.Location = new Point(99, 20);
            ServerAddressBox.Name = "ServerAddressBox";
            ServerAddressBox.Size = new Size(283, 23);
            ServerAddressBox.TabIndex = 6;
            // 
            // button2
            // 
            button2.Location = new Point(402, 55);
            button2.Name = "button2";
            button2.Size = new Size(86, 25);
            button2.TabIndex = 5;
            button2.Text = "Reset";
            button2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            button1.Location = new Point(7, 55);
            button1.Name = "button1";
            button1.Size = new Size(86, 25);
            button1.TabIndex = 4;
            button1.Text = "Update";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // SettingsForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(518, 105);
            Controls.Add(groupBox1);
            Name = "SettingsForm";
            Text = "Settings";
            TopMost = true;
            Load += SettingsForm_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private Label label1;
        private Label label2;
        private GroupBox groupBox1;
        private Button button2;
        private Button button1;
        private TextBox PortBox;
        private TextBox ServerAddressBox;
    }
}