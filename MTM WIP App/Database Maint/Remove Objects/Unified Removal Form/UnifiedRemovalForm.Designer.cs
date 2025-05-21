using System.Net.Mime;

namespace MTM_WIP_App.Database_Maint.Remove_Objects.Unified_Removal_Form
{
    partial class UnifiedRemovalForm
    {
        private System.ComponentModel.IContainer components = null;

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
            ObjectTypeLabel = new Label();
            ObjectTypeComboBox = new ComboBox();
            DataGridViewPanel = new Panel();
            RemovalDataGridView = new DataGridView();
            DeleteButton = new Button();
            ExitButton = new Button();
            panel1 = new Panel();
            DataGridViewPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)RemovalDataGridView).BeginInit();
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
            ObjectTypeComboBox.Size = new Size(649, 23);
            ObjectTypeComboBox.TabIndex = 0;
            ObjectTypeComboBox.SelectedIndexChanged += ObjectTypeComboBox_SelectedIndexChanged;
            // 
            // DataGridViewPanel
            // 
            DataGridViewPanel.BorderStyle = BorderStyle.FixedSingle;
            DataGridViewPanel.Controls.Add(RemovalDataGridView);
            DataGridViewPanel.Location = new Point(20, 60);
            DataGridViewPanel.Name = "DataGridViewPanel";
            DataGridViewPanel.Size = new Size(760, 400);
            DataGridViewPanel.TabIndex = 1;
            // 
            // RemovalDataGridView
            // 
            RemovalDataGridView.AllowUserToAddRows = false;
            RemovalDataGridView.AllowUserToDeleteRows = false;
            RemovalDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            RemovalDataGridView.Dock = DockStyle.Fill;
            RemovalDataGridView.Location = new Point(0, 0);
            RemovalDataGridView.Name = "RemovalDataGridView";
            RemovalDataGridView.ReadOnly = true;
            RemovalDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            RemovalDataGridView.Size = new Size(758, 398);
            RemovalDataGridView.TabIndex = 0;
            // 
            // DeleteButton
            // 
            DeleteButton.Location = new Point(580, 10);
            DeleteButton.Name = "DeleteButton";
            DeleteButton.Size = new Size(100, 30);
            DeleteButton.TabIndex = 2;
            DeleteButton.Text = "Delete";
            DeleteButton.UseVisualStyleBackColor = true;
            DeleteButton.Click += DeleteButton_Click;
            // 
            // ExitButton
            // 
            ExitButton.Location = new Point(690, 10);
            ExitButton.Name = "ExitButton";
            ExitButton.Size = new Size(100, 30);
            ExitButton.TabIndex = 3;
            ExitButton.Text = "Exit";
            ExitButton.UseVisualStyleBackColor = true;
            ExitButton.Click += ExitButton_Click;
            // 
            // panel1
            // 
            panel1.Controls.Add(DeleteButton);
            panel1.Controls.Add(ExitButton);
            panel1.Dock = DockStyle.Bottom;
            panel1.Location = new Point(0, 470);
            panel1.Name = "panel1";
            panel1.Size = new Size(800, 50);
            panel1.TabIndex = 4;
            // 
            // UnifiedRemovalForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 520);
            Controls.Add(DataGridViewPanel);
            Controls.Add(ObjectTypeLabel);
            Controls.Add(ObjectTypeComboBox);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "UnifiedRemovalForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Unified Removal Form";
            DataGridViewPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)RemovalDataGridView).EndInit();
            panel1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        private Label ObjectTypeLabel;
        private ComboBox ObjectTypeComboBox;
        private Panel DataGridViewPanel;
        private DataGridView RemovalDataGridView;
        private Button DeleteButton;
        private Button ExitButton;
        private Panel panel1;
    }
}