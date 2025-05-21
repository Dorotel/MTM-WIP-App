namespace MTM_WIP_App
{
    sealed partial class PersonalHistory
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
            shiftTB = new TextBox();
            panel4 = new Panel();
            PersonalHistory_Reset_Button = new Button();
            historyPart = new ComboBox();
            sortBox = new ComboBox();
            label4 = new Label();
            sortLabel = new Label();
            inputTab = new TabControl();
            tabPage1 = new TabPage();
            entryTable = new DataGridView();
            tabPage2 = new TabPage();
            removalTable = new DataGridView();
            tabPage3 = new TabPage();
            transferTable = new DataGridView();
            userCB = new ComboBox();
            label1 = new Label();
            panel1 = new Panel();
            label5 = new Label();
            label3 = new Label();
            label2 = new Label();
            userNameTB = new TextBox();
            menuStrip1 = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            printToolStripMenuItem = new ToolStripMenuItem();
            resetToolStripMenuItem = new ToolStripMenuItem();
            panel4.SuspendLayout();
            inputTab.SuspendLayout();
            tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)entryTable).BeginInit();
            tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)removalTable).BeginInit();
            tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)transferTable).BeginInit();
            panel1.SuspendLayout();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // shiftTB
            // 
            shiftTB.Location = new Point(583, 28);
            shiftTB.Name = "shiftTB";
            shiftTB.ReadOnly = true;
            shiftTB.Size = new Size(174, 23);
            shiftTB.TabIndex = 32;
            // 
            // panel4
            // 
            panel4.Controls.Add(PersonalHistory_Reset_Button);
            panel4.Controls.Add(historyPart);
            panel4.Controls.Add(sortBox);
            panel4.Controls.Add(label4);
            panel4.Controls.Add(sortLabel);
            panel4.Dock = DockStyle.Top;
            panel4.Location = new Point(0, 24);
            panel4.Name = "panel4";
            panel4.Size = new Size(804, 36);
            panel4.TabIndex = 24;
            // 
            // PersonalHistory_Reset_Button
            // 
            PersonalHistory_Reset_Button.Dock = DockStyle.Right;
            PersonalHistory_Reset_Button.Location = new Point(729, 0);
            PersonalHistory_Reset_Button.Name = "PersonalHistory_Reset_Button";
            PersonalHistory_Reset_Button.Size = new Size(75, 36);
            PersonalHistory_Reset_Button.TabIndex = 32;
            PersonalHistory_Reset_Button.Text = "Reset";
            PersonalHistory_Reset_Button.UseVisualStyleBackColor = true;
            PersonalHistory_Reset_Button.Click += PersonalHistory_Reset_Button_Click;
            // 
            // historyPart
            // 
            historyPart.AutoCompleteMode = AutoCompleteMode.Suggest;
            historyPart.AutoCompleteSource = AutoCompleteSource.ListItems;
            historyPart.FormattingEnabled = true;
            historyPart.Items.AddRange(new object[] { "Employee", "Part ID", "Location", "Date" });
            historyPart.Location = new Point(381, 6);
            historyPart.Name = "historyPart";
            historyPart.Size = new Size(338, 23);
            historyPart.TabIndex = 31;
            historyPart.SelectedIndexChanged += HistoryPart_SelectedIndexChanged;
            // 
            // sortBox
            // 
            sortBox.AutoCompleteMode = AutoCompleteMode.Suggest;
            sortBox.AutoCompleteSource = AutoCompleteSource.ListItems;
            sortBox.FormattingEnabled = true;
            sortBox.Items.AddRange(new object[] { "Employee", "Part ID", "Location", "Date" });
            sortBox.Location = new Point(69, 6);
            sortBox.Name = "sortBox";
            sortBox.Size = new Size(159, 23);
            sortBox.TabIndex = 27;
            sortBox.SelectedIndexChanged += SortBox_SelectedIndexChanged;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(244, 10);
            label4.Name = "label4";
            label4.Size = new Size(135, 15);
            label4.TabIndex = 30;
            label4.Text = "Search By Part Number :";
            // 
            // sortLabel
            // 
            sortLabel.AutoSize = true;
            sortLabel.Location = new Point(17, 10);
            sortLabel.Name = "sortLabel";
            sortLabel.Size = new Size(50, 15);
            sortLabel.TabIndex = 26;
            sortLabel.Text = "Sort By :";
            // 
            // inputTab
            // 
            inputTab.Controls.Add(tabPage1);
            inputTab.Controls.Add(tabPage2);
            inputTab.Controls.Add(tabPage3);
            inputTab.Dock = DockStyle.Fill;
            inputTab.Location = new Point(0, 60);
            inputTab.Name = "inputTab";
            inputTab.SelectedIndex = 0;
            inputTab.Size = new Size(804, 244);
            inputTab.TabIndex = 25;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(entryTable);
            tabPage1.Location = new Point(4, 24);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(796, 216);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Part Entry";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // entryTable
            // 
            entryTable.AllowUserToAddRows = false;
            entryTable.AllowUserToDeleteRows = false;
            entryTable.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            entryTable.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            entryTable.Dock = DockStyle.Fill;
            entryTable.EditMode = DataGridViewEditMode.EditProgrammatically;
            entryTable.Location = new Point(3, 3);
            entryTable.MultiSelect = false;
            entryTable.Name = "entryTable";
            entryTable.ReadOnly = true;
            entryTable.RowTemplate.Height = 25;
            entryTable.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            entryTable.Size = new Size(790, 210);
            entryTable.TabIndex = 0;
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(removalTable);
            tabPage2.Location = new Point(4, 24);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(796, 216);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Part Removal";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // removalTable
            // 
            removalTable.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            removalTable.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            removalTable.Dock = DockStyle.Fill;
            removalTable.Location = new Point(3, 3);
            removalTable.Name = "removalTable";
            removalTable.RowTemplate.Height = 25;
            removalTable.Size = new Size(790, 210);
            removalTable.TabIndex = 1;
            // 
            // tabPage3
            // 
            tabPage3.Controls.Add(transferTable);
            tabPage3.Location = new Point(4, 24);
            tabPage3.Name = "tabPage3";
            tabPage3.Size = new Size(796, 216);
            tabPage3.TabIndex = 2;
            tabPage3.Text = "Part Transfer";
            tabPage3.UseVisualStyleBackColor = true;
            // 
            // transferTable
            // 
            transferTable.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            transferTable.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            transferTable.Dock = DockStyle.Fill;
            transferTable.Location = new Point(0, 0);
            transferTable.Name = "transferTable";
            transferTable.RowTemplate.Height = 25;
            transferTable.Size = new Size(796, 216);
            transferTable.TabIndex = 1;
            // 
            // userCB
            // 
            userCB.AutoCompleteMode = AutoCompleteMode.Suggest;
            userCB.AutoCompleteSource = AutoCompleteSource.ListItems;
            userCB.FormattingEnabled = true;
            userCB.Location = new Point(45, 28);
            userCB.Name = "userCB";
            userCB.Size = new Size(200, 23);
            userCB.TabIndex = 29;
            userCB.SelectedIndexChanged += UserCB_SelectedIndexChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(3, 32);
            label1.Name = "label1";
            label1.Size = new Size(36, 15);
            label1.TabIndex = 28;
            label1.Text = "User :";
            // 
            // panel1
            // 
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Controls.Add(label5);
            panel1.Controls.Add(userCB);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(shiftTB);
            panel1.Controls.Add(userNameTB);
            panel1.Dock = DockStyle.Bottom;
            panel1.Location = new Point(0, 304);
            panel1.Name = "panel1";
            panel1.Size = new Size(804, 57);
            panel1.TabIndex = 30;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(3, 3);
            label5.Name = "label5";
            label5.Size = new Size(145, 15);
            label5.TabIndex = 31;
            label5.Text = "Sort by User (Admin Only)";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(540, 32);
            label3.Name = "label3";
            label3.Size = new Size(37, 15);
            label3.TabIndex = 30;
            label3.Text = "Shift :";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(257, 32);
            label2.Name = "label2";
            label2.Size = new Size(71, 15);
            label2.TabIndex = 29;
            label2.Text = "User Name :";
            // 
            // userNameTB
            // 
            userNameTB.Location = new Point(334, 28);
            userNameTB.Name = "userNameTB";
            userNameTB.ReadOnly = true;
            userNameTB.Size = new Size(200, 23);
            userNameTB.TabIndex = 31;
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(804, 24);
            menuStrip1.TabIndex = 32;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { printToolStripMenuItem, resetToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(37, 20);
            fileToolStripMenuItem.Text = "File";
            // 
            // printToolStripMenuItem
            // 
            printToolStripMenuItem.Name = "printToolStripMenuItem";
            printToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.P;
            printToolStripMenuItem.Size = new Size(143, 22);
            printToolStripMenuItem.Text = "Print";
            printToolStripMenuItem.Click += PrintToolStripMenuItem_Click;
            // 
            // resetToolStripMenuItem
            // 
            resetToolStripMenuItem.Name = "resetToolStripMenuItem";
            resetToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.R;
            resetToolStripMenuItem.Size = new Size(143, 22);
            resetToolStripMenuItem.Text = "Reset";
            resetToolStripMenuItem.Click += ResetToolStripMenuItem_Click;
            // 
            // PersonalHistory
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = new Size(804, 361);
            Controls.Add(inputTab);
            Controls.Add(panel4);
            Controls.Add(panel1);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "PersonalHistory";
            Text = "Personal History";
            FormClosing += PersonalHistory_FormClosing;
            Load += Form1_Load;
            panel4.ResumeLayout(false);
            panel4.PerformLayout();
            inputTab.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)entryTable).EndInit();
            tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)removalTable).EndInit();
            tabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)transferTable).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panel4;
        private TabControl inputTab;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private TabPage tabPage3;
        private DataGridView removalTable;
        private DataGridView transferTable;
        private Label sortLabel;
        private ComboBox sortBox;
        public DataGridView entryTable;
        private ComboBox userCB;
        private Label label1;
        private Panel panel1;
        private TextBox shiftTB;
        private TextBox userNameTB;
        private Label label3;
        private Label label2;
        private ComboBox historyPart;
        private Label label4;
        private Label label5;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem printToolStripMenuItem;
        private ToolStripMenuItem resetToolStripMenuItem;
        private Button PersonalHistory_Reset_Button;
    }
}