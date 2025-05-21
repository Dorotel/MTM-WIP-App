namespace MTM_WIP_App.Main_Form
{
    public partial class MainForm
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            MainForm_MenuStrip = new MenuStrip();
            MainForm_MenuStrip_File = new ToolStripMenuItem();
            MainForm_MenuStrip_File_Save = new ToolStripMenuItem();
            MainForm_MenuStrip_File_Delete = new ToolStripMenuItem();
            MainForm_MenuStrip_File_Print = new ToolStripMenuItem();
            MainForm_MenuStrip_File_Settings = new ToolStripMenuItem();
            MainForm_MenuStrip_Exit = new ToolStripMenuItem();
            MainForm_MenuStrip_Edit = new ToolStripMenuItem();
            MainToolStrip_New_Object = new ToolStripMenuItem();
            CallUnifiedRemovalForm = new ToolStripMenuItem();
            toolStripSeparator4 = new ToolStripSeparator();
            resetLast10ButtonsToolStripMenuItem = new ToolStripMenuItem();
            MainForm_MenuStrip_View = new ToolStripMenuItem();
            personalHistoryToolStripMenuItem = new ToolStripMenuItem();
            MainForm_MenuStrip_View_Reset = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            addToInventoryToolStripMenuItem = new ToolStripMenuItem();
            removeFromInventoryToolStripMenuItem = new ToolStripMenuItem();
            locationToLocationToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator2 = new ToolStripSeparator();
            viewAllWIPToolStripMenuItem = new ToolStripMenuItem();
            viewOutsideServiceToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator3 = new ToolStripSeparator();
            viewChangelogToolStripMenuItem = new ToolStripMenuItem();
            RemoveTab_GroupBox_Main = new GroupBox();
            panel3 = new Panel();
            RemoveTab_ComboBox_Part = new ComboBox();
            RemoveTab_Label_Part = new Label();
            RemoveTab_Label_Op = new Label();
            RemoveTab_ComboBox_Op = new ComboBox();
            RemoveTab_Panel_DataGrid = new Panel();
            RemoveTab_Image_NothingFound = new PictureBox();
            RemoveTab_DataGrid = new DataGridView();
            panel1 = new Panel();
            RemoveTab_Button_Edit = new Button();
            RemoveTab_Panel_SeachByType = new Panel();
            RemoveTab_Label_SearchByType = new Label();
            RemoveTab_CBox_ShowAll = new ComboBox();
            RemoveTab_Button_Print = new Button();
            RemoveTab_Button_Reset = new Button();
            RemoveTab_Button_Delete = new Button();
            RemoveTab_Button_Search = new Button();
            MainForm_TabControl = new TabControl();
            MainForm_TabControl_Inventory = new TabPage();
            InventoryTab_Group_Main = new GroupBox();
            InventoryBottomGroup = new Panel();
            InventoryTab_Label_Version = new Label();
            InventoryTab_Button_Reset = new Button();
            InventoryTab_Button_Save = new Button();
            InventoryTab_Group_Top = new Panel();
            MainForm_Button_ShowHideLast10 = new Button();
            InventoryTab_TextBox_HowMany = new TextBox();
            InventoryTab_ComboBox_Loc = new ComboBox();
            InventoryTab_TextBox_Qty = new TextBox();
            InventoryTab_ComboBox_Op = new ComboBox();
            InventoryTab_ComboBox_Part = new ComboBox();
            InventoryTab_CheckBox_Multi_Different = new CheckBox();
            InventoryTab_Label_Op = new Label();
            InventoryTab_Label_HowMany = new Label();
            InventoryTab_RichTextBox_Notes = new RichTextBox();
            InventoryTab_Label_Notes = new Label();
            InventoryTab_CheckBox_Multi = new CheckBox();
            InventoryTab_Label_Part = new Label();
            InventoryTab_Label_Qty = new Label();
            InventoryTab_Label_Loc = new Label();
            MainForm_TabControl_Remove = new TabPage();
            MainForm_TabControl_Transfer = new TabPage();
            TransferTab_Group_Main = new GroupBox();
            panel4 = new Panel();
            TransferTab_Button_Search = new Button();
            TransferTab_ComboBox_Part = new ComboBox();
            TransferTab_Label_Part = new Label();
            TransferTab_Label_Loc = new Label();
            TransferTab_ComboBox_Loc = new ComboBox();
            TransferTab_Panel_DataGrid = new Panel();
            TransferTab_Image_Nothing = new PictureBox();
            TransferDataGrid = new DataGridView();
            panel5 = new Panel();
            TransferTab_TextBox_Qty = new TextBox();
            TransferTab_Button_Save = new Button();
            TransferTab_Label_Qty = new Label();
            TransferTab_Button_Reset = new Button();
            MainForm_StatusStrip = new StatusStrip();
            MainForm_StatusStrip_SavedStatus = new ToolStripStatusLabel();
            MainForm_StatusStrip_Disconnected = new ToolStripStatusLabel();
            Inventory_PrintDocument = new System.Drawing.Printing.PrintDocument();
            Inventory_PrintDialog = new PrintPreviewDialog();
            MainForm_ToolTip = new ToolTip(components);
            MainForm_GroupBox_Last10 = new GroupBox();
            MainForm_Last10_Button_10 = new Button();
            MainForm_Last10_Button_09 = new Button();
            MainForm_Last10_Button_08 = new Button();
            MainForm_Last10_Button_07 = new Button();
            MainForm_Last10_Button_06 = new Button();
            MainForm_Last10_Button_05 = new Button();
            MainForm_Last10_Button_04 = new Button();
            MainForm_Last10_Button_03 = new Button();
            MainForm_Last10_Button_02 = new Button();
            MainForm_Last10_Button_01 = new Button();
            Last10Timer = new System.Windows.Forms.Timer(components);
            MainForm_MenuStrip.SuspendLayout();
            RemoveTab_GroupBox_Main.SuspendLayout();
            panel3.SuspendLayout();
            RemoveTab_Panel_DataGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)RemoveTab_Image_NothingFound).BeginInit();
            ((System.ComponentModel.ISupportInitialize)RemoveTab_DataGrid).BeginInit();
            panel1.SuspendLayout();
            RemoveTab_Panel_SeachByType.SuspendLayout();
            MainForm_TabControl.SuspendLayout();
            MainForm_TabControl_Inventory.SuspendLayout();
            InventoryTab_Group_Main.SuspendLayout();
            InventoryBottomGroup.SuspendLayout();
            InventoryTab_Group_Top.SuspendLayout();
            MainForm_TabControl_Remove.SuspendLayout();
            MainForm_TabControl_Transfer.SuspendLayout();
            TransferTab_Group_Main.SuspendLayout();
            panel4.SuspendLayout();
            TransferTab_Panel_DataGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)TransferTab_Image_Nothing).BeginInit();
            ((System.ComponentModel.ISupportInitialize)TransferDataGrid).BeginInit();
            panel5.SuspendLayout();
            MainForm_StatusStrip.SuspendLayout();
            MainForm_GroupBox_Last10.SuspendLayout();
            SuspendLayout();
            // 
            // MainForm_MenuStrip
            // 
            MainForm_MenuStrip.ImageScalingSize = new Size(24, 24);
            MainForm_MenuStrip.Items.AddRange(new ToolStripItem[] { MainForm_MenuStrip_File, MainForm_MenuStrip_Edit, MainForm_MenuStrip_View });
            MainForm_MenuStrip.Location = new Point(0, 0);
            MainForm_MenuStrip.Name = "MainForm_MenuStrip";
            MainForm_MenuStrip.Size = new Size(984, 24);
            MainForm_MenuStrip.TabIndex = 1;
            MainForm_MenuStrip.Text = "menuStrip1";
            // 
            // MainForm_MenuStrip_File
            // 
            MainForm_MenuStrip_File.DropDownItems.AddRange(new ToolStripItem[] { MainForm_MenuStrip_File_Save, MainForm_MenuStrip_File_Delete, MainForm_MenuStrip_File_Print, MainForm_MenuStrip_File_Settings, MainForm_MenuStrip_Exit });
            MainForm_MenuStrip_File.Name = "MainForm_MenuStrip_File";
            MainForm_MenuStrip_File.Size = new Size(37, 20);
            MainForm_MenuStrip_File.Text = "File";
            // 
            // MainForm_MenuStrip_File_Save
            // 
            MainForm_MenuStrip_File_Save.Name = "MainForm_MenuStrip_File_Save";
            MainForm_MenuStrip_File_Save.ShortcutKeys = Keys.Control | Keys.S;
            MainForm_MenuStrip_File_Save.Size = new Size(188, 22);
            MainForm_MenuStrip_File_Save.Text = "Save";
            MainForm_MenuStrip_File_Save.Click += ToolStrip_Save_Clicked;
            // 
            // MainForm_MenuStrip_File_Delete
            // 
            MainForm_MenuStrip_File_Delete.Name = "MainForm_MenuStrip_File_Delete";
            MainForm_MenuStrip_File_Delete.ShortcutKeys = Keys.Delete;
            MainForm_MenuStrip_File_Delete.Size = new Size(188, 22);
            MainForm_MenuStrip_File_Delete.Text = "Delete";
            MainForm_MenuStrip_File_Delete.Click += ToolStrip_Delete_Clicked;
            // 
            // MainForm_MenuStrip_File_Print
            // 
            MainForm_MenuStrip_File_Print.Name = "MainForm_MenuStrip_File_Print";
            MainForm_MenuStrip_File_Print.ShortcutKeys = Keys.Control | Keys.P;
            MainForm_MenuStrip_File_Print.Size = new Size(188, 22);
            MainForm_MenuStrip_File_Print.Text = "Print";
            MainForm_MenuStrip_File_Print.Click += RemoveTab_Button_Print_Clicked;
            // 
            // MainForm_MenuStrip_File_Settings
            // 
            MainForm_MenuStrip_File_Settings.Name = "MainForm_MenuStrip_File_Settings";
            MainForm_MenuStrip_File_Settings.ShortcutKeys = Keys.Control | Keys.Shift | Keys.S;
            MainForm_MenuStrip_File_Settings.Size = new Size(188, 22);
            MainForm_MenuStrip_File_Settings.Text = "Settings";
            MainForm_MenuStrip_File_Settings.Click += ToolStrip_Settings_Clicked;
            // 
            // MainForm_MenuStrip_Exit
            // 
            MainForm_MenuStrip_Exit.Name = "MainForm_MenuStrip_Exit";
            MainForm_MenuStrip_Exit.ShortcutKeys = Keys.Alt | Keys.F4;
            MainForm_MenuStrip_Exit.Size = new Size(188, 22);
            MainForm_MenuStrip_Exit.Text = "Exit";
            MainForm_MenuStrip_Exit.Click += MainForm_MenuStrip_Exit_Click;
            // 
            // MainForm_MenuStrip_Edit
            // 
            MainForm_MenuStrip_Edit.DropDownItems.AddRange(new ToolStripItem[] { MainToolStrip_New_Object, CallUnifiedRemovalForm, toolStripSeparator4, resetLast10ButtonsToolStripMenuItem });
            MainForm_MenuStrip_Edit.Name = "MainForm_MenuStrip_Edit";
            MainForm_MenuStrip_Edit.Size = new Size(39, 20);
            MainForm_MenuStrip_Edit.Text = "Edit";
            // 
            // MainToolStrip_New_Object
            // 
            MainToolStrip_New_Object.Name = "MainToolStrip_New_Object";
            MainToolStrip_New_Object.Size = new Size(185, 22);
            MainToolStrip_New_Object.Text = "New Object";
            MainToolStrip_New_Object.Click += MainToolStrip_New_Object_Click;
            // 
            // CallUnifiedRemovalForm
            // 
            CallUnifiedRemovalForm.Name = "CallUnifiedRemovalForm";
            CallUnifiedRemovalForm.Size = new Size(185, 22);
            CallUnifiedRemovalForm.Text = "Remove Object";
            CallUnifiedRemovalForm.Click += CallUnifiedRemovalForm_Click;
            // 
            // toolStripSeparator4
            // 
            toolStripSeparator4.Name = "toolStripSeparator4";
            toolStripSeparator4.Size = new Size(182, 6);
            // 
            // resetLast10ButtonsToolStripMenuItem
            // 
            resetLast10ButtonsToolStripMenuItem.Name = "resetLast10ButtonsToolStripMenuItem";
            resetLast10ButtonsToolStripMenuItem.Size = new Size(185, 22);
            resetLast10ButtonsToolStripMenuItem.Text = "Reset Last 10 Buttons";
            resetLast10ButtonsToolStripMenuItem.Click += ToolStrip_ResetLast10_Clicked;
            // 
            // MainForm_MenuStrip_View
            // 
            MainForm_MenuStrip_View.DropDownItems.AddRange(new ToolStripItem[] { personalHistoryToolStripMenuItem, MainForm_MenuStrip_View_Reset, toolStripSeparator1, addToInventoryToolStripMenuItem, removeFromInventoryToolStripMenuItem, locationToLocationToolStripMenuItem, toolStripSeparator2, viewAllWIPToolStripMenuItem, viewOutsideServiceToolStripMenuItem, toolStripSeparator3, viewChangelogToolStripMenuItem });
            MainForm_MenuStrip_View.Name = "MainForm_MenuStrip_View";
            MainForm_MenuStrip_View.Size = new Size(44, 20);
            MainForm_MenuStrip_View.Text = "View";
            // 
            // personalHistoryToolStripMenuItem
            // 
            personalHistoryToolStripMenuItem.Name = "personalHistoryToolStripMenuItem";
            personalHistoryToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.H;
            personalHistoryToolStripMenuItem.Size = new Size(234, 22);
            personalHistoryToolStripMenuItem.Text = "Personal History";
            personalHistoryToolStripMenuItem.Click += ToolStrip_History_Clicked;
            // 
            // MainForm_MenuStrip_View_Reset
            // 
            MainForm_MenuStrip_View_Reset.Name = "MainForm_MenuStrip_View_Reset";
            MainForm_MenuStrip_View_Reset.ShortcutKeys = Keys.Control | Keys.R;
            MainForm_MenuStrip_View_Reset.Size = new Size(234, 22);
            MainForm_MenuStrip_View_Reset.Text = "Reset New Transaction";
            MainForm_MenuStrip_View_Reset.Click += ToolStrip_ResetAllTabs_Clicked;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(231, 6);
            // 
            // addToInventoryToolStripMenuItem
            // 
            addToInventoryToolStripMenuItem.Name = "addToInventoryToolStripMenuItem";
            addToInventoryToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.D1;
            addToInventoryToolStripMenuItem.Size = new Size(234, 22);
            addToInventoryToolStripMenuItem.Text = "New Transaction";
            addToInventoryToolStripMenuItem.Click += ToolStrip_View_Tab1;
            // 
            // removeFromInventoryToolStripMenuItem
            // 
            removeFromInventoryToolStripMenuItem.Name = "removeFromInventoryToolStripMenuItem";
            removeFromInventoryToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.D2;
            removeFromInventoryToolStripMenuItem.Size = new Size(234, 22);
            removeFromInventoryToolStripMenuItem.Text = "Remove";
            removeFromInventoryToolStripMenuItem.Click += ToolStrip_View_Tab2;
            // 
            // locationToLocationToolStripMenuItem
            // 
            locationToLocationToolStripMenuItem.Name = "locationToLocationToolStripMenuItem";
            locationToLocationToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.D3;
            locationToLocationToolStripMenuItem.Size = new Size(234, 22);
            locationToLocationToolStripMenuItem.Text = "Transfer";
            locationToLocationToolStripMenuItem.Click += ToolStrip_View_Tab3;
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(231, 6);
            toolStripSeparator2.Click += ToolStrip_View_Tab3;
            // 
            // viewAllWIPToolStripMenuItem
            // 
            viewAllWIPToolStripMenuItem.Name = "viewAllWIPToolStripMenuItem";
            viewAllWIPToolStripMenuItem.Size = new Size(234, 22);
            viewAllWIPToolStripMenuItem.Text = "View All WIP";
            viewAllWIPToolStripMenuItem.Click += ToolStrip_ViewAllWIP_Clicked;
            // 
            // viewOutsideServiceToolStripMenuItem
            // 
            viewOutsideServiceToolStripMenuItem.Name = "viewOutsideServiceToolStripMenuItem";
            viewOutsideServiceToolStripMenuItem.Size = new Size(234, 22);
            viewOutsideServiceToolStripMenuItem.Text = "View Outside Service";
            viewOutsideServiceToolStripMenuItem.Click += ToolStrip_OutsideService_Clicked;
            // 
            // toolStripSeparator3
            // 
            toolStripSeparator3.Name = "toolStripSeparator3";
            toolStripSeparator3.Size = new Size(231, 6);
            // 
            // viewChangelogToolStripMenuItem
            // 
            viewChangelogToolStripMenuItem.Name = "viewChangelogToolStripMenuItem";
            viewChangelogToolStripMenuItem.Size = new Size(234, 22);
            viewChangelogToolStripMenuItem.Text = "View Changelog";
            viewChangelogToolStripMenuItem.Click += ToolStrip_ViewChangeLog_Clicked;
            // 
            // RemoveTab_GroupBox_Main
            // 
            RemoveTab_GroupBox_Main.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            RemoveTab_GroupBox_Main.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            RemoveTab_GroupBox_Main.Controls.Add(panel3);
            RemoveTab_GroupBox_Main.Controls.Add(RemoveTab_Panel_DataGrid);
            RemoveTab_GroupBox_Main.Controls.Add(panel1);
            RemoveTab_GroupBox_Main.FlatStyle = FlatStyle.Flat;
            RemoveTab_GroupBox_Main.Location = new Point(3, 3);
            RemoveTab_GroupBox_Main.Name = "RemoveTab_GroupBox_Main";
            RemoveTab_GroupBox_Main.Size = new Size(825, 398);
            RemoveTab_GroupBox_Main.TabIndex = 16;
            RemoveTab_GroupBox_Main.TabStop = false;
            RemoveTab_GroupBox_Main.Text = "Part Lookup and Remove";
            // 
            // panel3
            // 
            panel3.Controls.Add(RemoveTab_ComboBox_Part);
            panel3.Controls.Add(RemoveTab_Label_Part);
            panel3.Controls.Add(RemoveTab_Label_Op);
            panel3.Controls.Add(RemoveTab_ComboBox_Op);
            panel3.Dock = DockStyle.Top;
            panel3.Location = new Point(3, 19);
            panel3.Name = "panel3";
            panel3.Size = new Size(819, 36);
            panel3.TabIndex = 22;
            // 
            // RemoveTab_ComboBox_Part
            // 
            RemoveTab_ComboBox_Part.AutoCompleteMode = AutoCompleteMode.Suggest;
            RemoveTab_ComboBox_Part.AutoCompleteSource = AutoCompleteSource.ListItems;
            RemoveTab_ComboBox_Part.FormattingEnabled = true;
            RemoveTab_ComboBox_Part.Location = new Point(86, 7);
            RemoveTab_ComboBox_Part.Name = "RemoveTab_ComboBox_Part";
            RemoveTab_ComboBox_Part.Size = new Size(564, 23);
            RemoveTab_ComboBox_Part.TabIndex = 1;
            // 
            // RemoveTab_Label_Part
            // 
            RemoveTab_Label_Part.AutoSize = true;
            RemoveTab_Label_Part.Location = new Point(3, 11);
            RemoveTab_Label_Part.Name = "RemoveTab_Label_Part";
            RemoveTab_Label_Part.Size = new Size(78, 15);
            RemoveTab_Label_Part.TabIndex = 4;
            RemoveTab_Label_Part.Text = "Part Number:";
            // 
            // RemoveTab_Label_Op
            // 
            RemoveTab_Label_Op.AutoSize = true;
            RemoveTab_Label_Op.Location = new Point(656, 11);
            RemoveTab_Label_Op.Name = "RemoveTab_Label_Op";
            RemoveTab_Label_Op.Size = new Size(26, 15);
            RemoveTab_Label_Op.TabIndex = 5;
            RemoveTab_Label_Op.Text = "Op:";
            // 
            // RemoveTab_ComboBox_Op
            // 
            RemoveTab_ComboBox_Op.AutoCompleteMode = AutoCompleteMode.Suggest;
            RemoveTab_ComboBox_Op.AutoCompleteSource = AutoCompleteSource.ListItems;
            RemoveTab_ComboBox_Op.FormattingEnabled = true;
            RemoveTab_ComboBox_Op.Location = new Point(688, 7);
            RemoveTab_ComboBox_Op.Name = "RemoveTab_ComboBox_Op";
            RemoveTab_ComboBox_Op.Size = new Size(128, 23);
            RemoveTab_ComboBox_Op.TabIndex = 2;
            // 
            // RemoveTab_Panel_DataGrid
            // 
            RemoveTab_Panel_DataGrid.Controls.Add(RemoveTab_Image_NothingFound);
            RemoveTab_Panel_DataGrid.Controls.Add(RemoveTab_DataGrid);
            RemoveTab_Panel_DataGrid.Location = new Point(3, 55);
            RemoveTab_Panel_DataGrid.Name = "RemoveTab_Panel_DataGrid";
            RemoveTab_Panel_DataGrid.Size = new Size(819, 299);
            RemoveTab_Panel_DataGrid.TabIndex = 21;
            // 
            // RemoveTab_Image_NothingFound
            // 
            RemoveTab_Image_NothingFound.Dock = DockStyle.Fill;
            RemoveTab_Image_NothingFound.ErrorImage = null;
            RemoveTab_Image_NothingFound.Image = Properties.Resources._4041;
            RemoveTab_Image_NothingFound.InitialImage = null;
            RemoveTab_Image_NothingFound.Location = new Point(0, 0);
            RemoveTab_Image_NothingFound.Name = "RemoveTab_Image_NothingFound";
            RemoveTab_Image_NothingFound.Size = new Size(819, 299);
            RemoveTab_Image_NothingFound.SizeMode = PictureBoxSizeMode.CenterImage;
            RemoveTab_Image_NothingFound.TabIndex = 6;
            RemoveTab_Image_NothingFound.TabStop = false;
            // 
            // RemoveTab_DataGrid
            // 
            RemoveTab_DataGrid.AllowUserToAddRows = false;
            RemoveTab_DataGrid.AllowUserToDeleteRows = false;
            RemoveTab_DataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            RemoveTab_DataGrid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            RemoveTab_DataGrid.BorderStyle = BorderStyle.Fixed3D;
            RemoveTab_DataGrid.CellBorderStyle = DataGridViewCellBorderStyle.Sunken;
            RemoveTab_DataGrid.ClipboardCopyMode = DataGridViewClipboardCopyMode.Disable;
            RemoveTab_DataGrid.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Sunken;
            RemoveTab_DataGrid.ColumnHeadersHeight = 34;
            RemoveTab_DataGrid.Dock = DockStyle.Fill;
            RemoveTab_DataGrid.EditMode = DataGridViewEditMode.EditProgrammatically;
            RemoveTab_DataGrid.Location = new Point(0, 0);
            RemoveTab_DataGrid.Name = "RemoveTab_DataGrid";
            RemoveTab_DataGrid.ReadOnly = true;
            RemoveTab_DataGrid.RowHeadersWidth = 62;
            RemoveTab_DataGrid.RowTemplate.ReadOnly = true;
            RemoveTab_DataGrid.RowTemplate.Resizable = DataGridViewTriState.True;
            RemoveTab_DataGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            RemoveTab_DataGrid.ShowCellErrors = false;
            RemoveTab_DataGrid.ShowCellToolTips = false;
            RemoveTab_DataGrid.ShowEditingIcon = false;
            RemoveTab_DataGrid.ShowRowErrors = false;
            RemoveTab_DataGrid.Size = new Size(819, 299);
            RemoveTab_DataGrid.StandardTab = true;
            RemoveTab_DataGrid.TabIndex = 4;
            RemoveTab_DataGrid.SelectionChanged += RemoveTab_DataGrid_SelectionChanged;
            // 
            // panel1
            // 
            panel1.Controls.Add(RemoveTab_Button_Edit);
            panel1.Controls.Add(RemoveTab_Panel_SeachByType);
            panel1.Controls.Add(RemoveTab_Button_Print);
            panel1.Controls.Add(RemoveTab_Button_Reset);
            panel1.Controls.Add(RemoveTab_Button_Delete);
            panel1.Controls.Add(RemoveTab_Button_Search);
            panel1.Dock = DockStyle.Bottom;
            panel1.Location = new Point(3, 354);
            panel1.Name = "panel1";
            panel1.Padding = new Padding(3);
            panel1.Size = new Size(819, 41);
            panel1.TabIndex = 20;
            // 
            // RemoveTab_Button_Edit
            // 
            RemoveTab_Button_Edit.Dock = DockStyle.Right;
            RemoveTab_Button_Edit.Location = new Point(555, 3);
            RemoveTab_Button_Edit.Name = "RemoveTab_Button_Edit";
            RemoveTab_Button_Edit.Size = new Size(87, 35);
            RemoveTab_Button_Edit.TabIndex = 14;
            RemoveTab_Button_Edit.TabStop = false;
            RemoveTab_Button_Edit.Text = "Edit Note";
            MainForm_ToolTip.SetToolTip(RemoveTab_Button_Edit, "Shortcut: Ctrl+P");
            RemoveTab_Button_Edit.UseVisualStyleBackColor = true;
            RemoveTab_Button_Edit.Click += RemoveTab_Button_Edit_Click;
            // 
            // RemoveTab_Panel_SeachByType
            // 
            RemoveTab_Panel_SeachByType.Controls.Add(RemoveTab_Label_SearchByType);
            RemoveTab_Panel_SeachByType.Controls.Add(RemoveTab_CBox_ShowAll);
            RemoveTab_Panel_SeachByType.Location = new Point(294, 3);
            RemoveTab_Panel_SeachByType.Name = "RemoveTab_Panel_SeachByType";
            RemoveTab_Panel_SeachByType.Padding = new Padding(3);
            RemoveTab_Panel_SeachByType.Size = new Size(255, 35);
            RemoveTab_Panel_SeachByType.TabIndex = 12;
            // 
            // RemoveTab_Label_SearchByType
            // 
            RemoveTab_Label_SearchByType.AutoSize = true;
            RemoveTab_Label_SearchByType.Location = new Point(6, 10);
            RemoveTab_Label_SearchByType.Name = "RemoveTab_Label_SearchByType";
            RemoveTab_Label_SearchByType.Size = new Size(56, 15);
            RemoveTab_Label_SearchByType.TabIndex = 16;
            RemoveTab_Label_SearchByType.Text = "Show All:";
            // 
            // RemoveTab_CBox_ShowAll
            // 
            RemoveTab_CBox_ShowAll.FormattingEnabled = true;
            RemoveTab_CBox_ShowAll.Location = new Point(68, 6);
            RemoveTab_CBox_ShowAll.Name = "RemoveTab_CBox_ShowAll";
            RemoveTab_CBox_ShowAll.Size = new Size(181, 23);
            RemoveTab_CBox_ShowAll.TabIndex = 5;
            RemoveTab_CBox_ShowAll.TabStop = false;
            RemoveTab_CBox_ShowAll.SelectedIndexChanged += RemoveTab_ComboBox_SearchByType_SelectedIndexChanged;
            // 
            // RemoveTab_Button_Print
            // 
            RemoveTab_Button_Print.Dock = DockStyle.Right;
            RemoveTab_Button_Print.Location = new Point(642, 3);
            RemoveTab_Button_Print.Name = "RemoveTab_Button_Print";
            RemoveTab_Button_Print.Size = new Size(87, 35);
            RemoveTab_Button_Print.TabIndex = 7;
            RemoveTab_Button_Print.TabStop = false;
            RemoveTab_Button_Print.Text = "Print";
            MainForm_ToolTip.SetToolTip(RemoveTab_Button_Print, "Shortcut: Ctrl+P");
            RemoveTab_Button_Print.UseVisualStyleBackColor = true;
            RemoveTab_Button_Print.Click += InventoryTab_Button_Print_Clicked;
            // 
            // RemoveTab_Button_Reset
            // 
            RemoveTab_Button_Reset.Dock = DockStyle.Left;
            RemoveTab_Button_Reset.Location = new Point(90, 3);
            RemoveTab_Button_Reset.Name = "RemoveTab_Button_Reset";
            RemoveTab_Button_Reset.Size = new Size(100, 35);
            RemoveTab_Button_Reset.TabIndex = 5;
            RemoveTab_Button_Reset.TabStop = false;
            RemoveTab_Button_Reset.Text = "Reset";
            MainForm_ToolTip.SetToolTip(RemoveTab_Button_Reset, "Shortcut: Ctrl+R");
            RemoveTab_Button_Reset.UseVisualStyleBackColor = true;
            RemoveTab_Button_Reset.Click += RemoveTab_Button_Reset_Clicked;
            // 
            // RemoveTab_Button_Delete
            // 
            RemoveTab_Button_Delete.Dock = DockStyle.Right;
            RemoveTab_Button_Delete.Location = new Point(729, 3);
            RemoveTab_Button_Delete.Name = "RemoveTab_Button_Delete";
            RemoveTab_Button_Delete.Size = new Size(87, 35);
            RemoveTab_Button_Delete.TabIndex = 8;
            RemoveTab_Button_Delete.Text = "Delete";
            MainForm_ToolTip.SetToolTip(RemoveTab_Button_Delete, "Shortcut: Del");
            RemoveTab_Button_Delete.UseVisualStyleBackColor = true;
            RemoveTab_Button_Delete.Click += RemoveTab_Button_Delete_Clicked;
            // 
            // RemoveTab_Button_Search
            // 
            RemoveTab_Button_Search.Dock = DockStyle.Left;
            RemoveTab_Button_Search.Location = new Point(3, 3);
            RemoveTab_Button_Search.Name = "RemoveTab_Button_Search";
            RemoveTab_Button_Search.Size = new Size(87, 35);
            RemoveTab_Button_Search.TabIndex = 3;
            RemoveTab_Button_Search.Text = "Search";
            MainForm_ToolTip.SetToolTip(RemoveTab_Button_Search, "Shortcut: Ctrl+S");
            RemoveTab_Button_Search.UseVisualStyleBackColor = true;
            RemoveTab_Button_Search.Click += RemoveTab_Button_Search_Clicked;
            // 
            // MainForm_TabControl
            // 
            MainForm_TabControl.Controls.Add(MainForm_TabControl_Inventory);
            MainForm_TabControl.Controls.Add(MainForm_TabControl_Remove);
            MainForm_TabControl.Controls.Add(MainForm_TabControl_Transfer);
            MainForm_TabControl.Location = new Point(0, 27);
            MainForm_TabControl.Multiline = true;
            MainForm_TabControl.Name = "MainForm_TabControl";
            MainForm_TabControl.SelectedIndex = 0;
            MainForm_TabControl.ShowToolTips = true;
            MainForm_TabControl.Size = new Size(839, 432);
            MainForm_TabControl.SizeMode = TabSizeMode.FillToRight;
            MainForm_TabControl.TabIndex = 90;
            MainForm_TabControl.TabStop = false;
            MainForm_TabControl.SelectedIndexChanged += Primary_TabControl_SelectedIndexChanged;
            // 
            // MainForm_TabControl_Inventory
            // 
            MainForm_TabControl_Inventory.Controls.Add(InventoryTab_Group_Main);
            MainForm_TabControl_Inventory.Location = new Point(4, 24);
            MainForm_TabControl_Inventory.Name = "MainForm_TabControl_Inventory";
            MainForm_TabControl_Inventory.Padding = new Padding(3);
            MainForm_TabControl_Inventory.Size = new Size(831, 404);
            MainForm_TabControl_Inventory.TabIndex = 0;
            MainForm_TabControl_Inventory.Text = "New Transaction (Ctrl+1)";
            MainForm_TabControl_Inventory.ToolTipText = "Shortcut: Ctrl+1";
            MainForm_TabControl_Inventory.UseVisualStyleBackColor = true;
            // 
            // InventoryTab_Group_Main
            // 
            InventoryTab_Group_Main.Controls.Add(InventoryBottomGroup);
            InventoryTab_Group_Main.Controls.Add(InventoryTab_Group_Top);
            InventoryTab_Group_Main.Dock = DockStyle.Fill;
            InventoryTab_Group_Main.Location = new Point(3, 3);
            InventoryTab_Group_Main.Name = "InventoryTab_Group_Main";
            InventoryTab_Group_Main.Size = new Size(825, 398);
            InventoryTab_Group_Main.TabIndex = 0;
            InventoryTab_Group_Main.TabStop = false;
            InventoryTab_Group_Main.Text = "New Transaction";
            // 
            // InventoryBottomGroup
            // 
            InventoryBottomGroup.Controls.Add(InventoryTab_Label_Version);
            InventoryBottomGroup.Controls.Add(InventoryTab_Button_Reset);
            InventoryBottomGroup.Controls.Add(InventoryTab_Button_Save);
            InventoryBottomGroup.Dock = DockStyle.Bottom;
            InventoryBottomGroup.Location = new Point(3, 355);
            InventoryBottomGroup.Name = "InventoryBottomGroup";
            InventoryBottomGroup.Size = new Size(819, 40);
            InventoryBottomGroup.TabIndex = 25;
            // 
            // InventoryTab_Label_Version
            // 
            InventoryTab_Label_Version.Dock = DockStyle.Fill;
            InventoryTab_Label_Version.Font = new Font("Segoe UI", 7F);
            InventoryTab_Label_Version.Location = new Point(87, 0);
            InventoryTab_Label_Version.Name = "InventoryTab_Label_Version";
            InventoryTab_Label_Version.Size = new Size(645, 40);
            InventoryTab_Label_Version.TabIndex = 8;
            InventoryTab_Label_Version.Text = "Version: ";
            InventoryTab_Label_Version.TextAlign = ContentAlignment.BottomCenter;
            // 
            // InventoryTab_Button_Reset
            // 
            InventoryTab_Button_Reset.Dock = DockStyle.Right;
            InventoryTab_Button_Reset.Font = new Font("Segoe UI", 8F);
            InventoryTab_Button_Reset.Location = new Point(732, 0);
            InventoryTab_Button_Reset.Name = "InventoryTab_Button_Reset";
            InventoryTab_Button_Reset.Size = new Size(87, 40);
            InventoryTab_Button_Reset.TabIndex = 7;
            InventoryTab_Button_Reset.TabStop = false;
            InventoryTab_Button_Reset.Text = "Reset";
            MainForm_ToolTip.SetToolTip(InventoryTab_Button_Reset, "Shortcut: Ctrl+R");
            InventoryTab_Button_Reset.UseVisualStyleBackColor = true;
            InventoryTab_Button_Reset.Click += InventoryTab_Button_Reset_Clicked;
            // 
            // InventoryTab_Button_Save
            // 
            InventoryTab_Button_Save.Dock = DockStyle.Left;
            InventoryTab_Button_Save.Font = new Font("Segoe UI", 8F);
            InventoryTab_Button_Save.Location = new Point(0, 0);
            InventoryTab_Button_Save.Name = "InventoryTab_Button_Save";
            InventoryTab_Button_Save.Size = new Size(87, 40);
            InventoryTab_Button_Save.TabIndex = 6;
            InventoryTab_Button_Save.Text = "Save";
            MainForm_ToolTip.SetToolTip(InventoryTab_Button_Save, "Shortcut: Ctrl+S");
            InventoryTab_Button_Save.UseVisualStyleBackColor = true;
            InventoryTab_Button_Save.Click += InventoryTab_Button_Save_Clicked;
            // 
            // InventoryTab_Group_Top
            // 
            InventoryTab_Group_Top.AutoSize = true;
            InventoryTab_Group_Top.Controls.Add(MainForm_Button_ShowHideLast10);
            InventoryTab_Group_Top.Controls.Add(InventoryTab_TextBox_HowMany);
            InventoryTab_Group_Top.Controls.Add(InventoryTab_ComboBox_Loc);
            InventoryTab_Group_Top.Controls.Add(InventoryTab_TextBox_Qty);
            InventoryTab_Group_Top.Controls.Add(InventoryTab_ComboBox_Op);
            InventoryTab_Group_Top.Controls.Add(InventoryTab_ComboBox_Part);
            InventoryTab_Group_Top.Controls.Add(InventoryTab_CheckBox_Multi_Different);
            InventoryTab_Group_Top.Controls.Add(InventoryTab_Label_Op);
            InventoryTab_Group_Top.Controls.Add(InventoryTab_Label_HowMany);
            InventoryTab_Group_Top.Controls.Add(InventoryTab_RichTextBox_Notes);
            InventoryTab_Group_Top.Controls.Add(InventoryTab_Label_Notes);
            InventoryTab_Group_Top.Controls.Add(InventoryTab_CheckBox_Multi);
            InventoryTab_Group_Top.Controls.Add(InventoryTab_Label_Part);
            InventoryTab_Group_Top.Controls.Add(InventoryTab_Label_Qty);
            InventoryTab_Group_Top.Controls.Add(InventoryTab_Label_Loc);
            InventoryTab_Group_Top.Dock = DockStyle.Top;
            InventoryTab_Group_Top.Location = new Point(3, 19);
            InventoryTab_Group_Top.Name = "InventoryTab_Group_Top";
            InventoryTab_Group_Top.Size = new Size(819, 1265);
            InventoryTab_Group_Top.TabIndex = 26;
            // 
            // MainForm_Button_ShowHideLast10
            // 
            MainForm_Button_ShowHideLast10.Location = new Point(623, 127);
            MainForm_Button_ShowHideLast10.Name = "MainForm_Button_ShowHideLast10";
            MainForm_Button_ShowHideLast10.Size = new Size(193, 23);
            MainForm_Button_ShowHideLast10.TabIndex = 18;
            MainForm_Button_ShowHideLast10.TabStop = false;
            MainForm_Button_ShowHideLast10.Text = "Hide Last 10";
            MainForm_Button_ShowHideLast10.UseVisualStyleBackColor = true;
            MainForm_Button_ShowHideLast10.Click += Primary_Button_ShowHideLast10_Clicked;
            // 
            // InventoryTab_TextBox_HowMany
            // 
            InventoryTab_TextBox_HowMany.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            InventoryTab_TextBox_HowMany.Enabled = false;
            InventoryTab_TextBox_HowMany.Location = new Point(421, 127);
            InventoryTab_TextBox_HowMany.Name = "InventoryTab_TextBox_HowMany";
            InventoryTab_TextBox_HowMany.Size = new Size(196, 23);
            InventoryTab_TextBox_HowMany.TabIndex = 9;
            // 
            // InventoryTab_ComboBox_Loc
            // 
            InventoryTab_ComboBox_Loc.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            InventoryTab_ComboBox_Loc.AutoCompleteMode = AutoCompleteMode.Suggest;
            InventoryTab_ComboBox_Loc.AutoCompleteSource = AutoCompleteSource.ListItems;
            InventoryTab_ComboBox_Loc.FormattingEnabled = true;
            InventoryTab_ComboBox_Loc.Location = new Point(101, 97);
            InventoryTab_ComboBox_Loc.Name = "InventoryTab_ComboBox_Loc";
            InventoryTab_ComboBox_Loc.Size = new Size(715, 23);
            InventoryTab_ComboBox_Loc.TabIndex = 4;
            // 
            // InventoryTab_TextBox_Qty
            // 
            InventoryTab_TextBox_Qty.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            InventoryTab_TextBox_Qty.Location = new Point(101, 66);
            InventoryTab_TextBox_Qty.MaxLength = 6;
            InventoryTab_TextBox_Qty.Name = "InventoryTab_TextBox_Qty";
            InventoryTab_TextBox_Qty.Size = new Size(715, 23);
            InventoryTab_TextBox_Qty.TabIndex = 3;
            // 
            // InventoryTab_ComboBox_Op
            // 
            InventoryTab_ComboBox_Op.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            InventoryTab_ComboBox_Op.AutoCompleteMode = AutoCompleteMode.Suggest;
            InventoryTab_ComboBox_Op.AutoCompleteSource = AutoCompleteSource.ListItems;
            InventoryTab_ComboBox_Op.FormattingEnabled = true;
            InventoryTab_ComboBox_Op.Location = new Point(101, 35);
            InventoryTab_ComboBox_Op.Name = "InventoryTab_ComboBox_Op";
            InventoryTab_ComboBox_Op.Size = new Size(715, 23);
            InventoryTab_ComboBox_Op.TabIndex = 2;
            // 
            // InventoryTab_ComboBox_Part
            // 
            InventoryTab_ComboBox_Part.AutoCompleteMode = AutoCompleteMode.Suggest;
            InventoryTab_ComboBox_Part.AutoCompleteSource = AutoCompleteSource.ListItems;
            InventoryTab_ComboBox_Part.FormattingEnabled = true;
            InventoryTab_ComboBox_Part.Location = new Point(101, 4);
            InventoryTab_ComboBox_Part.MaxDropDownItems = 4;
            InventoryTab_ComboBox_Part.MaxLength = 500;
            InventoryTab_ComboBox_Part.Name = "InventoryTab_ComboBox_Part";
            InventoryTab_ComboBox_Part.Size = new Size(715, 23);
            InventoryTab_ComboBox_Part.TabIndex = 1;
            InventoryTab_ComboBox_Part.SelectedIndexChanged += InventoryTab_ComboBox_Part_DunnageCheck;
            // 
            // InventoryTab_CheckBox_Multi_Different
            // 
            InventoryTab_CheckBox_Multi_Different.AutoSize = true;
            InventoryTab_CheckBox_Multi_Different.Location = new Point(215, 130);
            InventoryTab_CheckBox_Multi_Different.Name = "InventoryTab_CheckBox_Multi_Different";
            InventoryTab_CheckBox_Multi_Different.Size = new Size(126, 19);
            InventoryTab_CheckBox_Multi_Different.TabIndex = 8;
            InventoryTab_CheckBox_Multi_Different.TabStop = false;
            InventoryTab_CheckBox_Multi_Different.Text = "Different Locations";
            InventoryTab_CheckBox_Multi_Different.UseVisualStyleBackColor = true;
            InventoryTab_CheckBox_Multi_Different.CheckedChanged += InventoryTab_CheckBox_Multi_Different_CheckedChanged;
            // 
            // InventoryTab_Label_Op
            // 
            InventoryTab_Label_Op.AutoSize = true;
            InventoryTab_Label_Op.Location = new Point(3, 39);
            InventoryTab_Label_Op.Name = "InventoryTab_Label_Op";
            InventoryTab_Label_Op.Size = new Size(87, 15);
            InventoryTab_Label_Op.TabIndex = 5;
            InventoryTab_Label_Op.Text = "Next Operaton:";
            // 
            // InventoryTab_Label_HowMany
            // 
            InventoryTab_Label_HowMany.AutoSize = true;
            InventoryTab_Label_HowMany.Location = new Point(347, 131);
            InventoryTab_Label_HowMany.Name = "InventoryTab_Label_HowMany";
            InventoryTab_Label_HowMany.Size = new Size(68, 15);
            InventoryTab_Label_HowMany.TabIndex = 16;
            InventoryTab_Label_HowMany.Text = "How Many:";
            // 
            // InventoryTab_RichTextBox_Notes
            // 
            InventoryTab_RichTextBox_Notes.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            InventoryTab_RichTextBox_Notes.Location = new Point(6, 157);
            InventoryTab_RichTextBox_Notes.Name = "InventoryTab_RichTextBox_Notes";
            InventoryTab_RichTextBox_Notes.Size = new Size(810, 176);
            InventoryTab_RichTextBox_Notes.TabIndex = 5;
            InventoryTab_RichTextBox_Notes.Text = "";
            // 
            // InventoryTab_Label_Notes
            // 
            InventoryTab_Label_Notes.AutoSize = true;
            InventoryTab_Label_Notes.Location = new Point(3, 132);
            InventoryTab_Label_Notes.Name = "InventoryTab_Label_Notes";
            InventoryTab_Label_Notes.Size = new Size(41, 15);
            InventoryTab_Label_Notes.TabIndex = 10;
            InventoryTab_Label_Notes.Text = "Notes:";
            // 
            // InventoryTab_CheckBox_Multi
            // 
            InventoryTab_CheckBox_Multi.AutoSize = true;
            InventoryTab_CheckBox_Multi.Location = new Point(101, 130);
            InventoryTab_CheckBox_Multi.Name = "InventoryTab_CheckBox_Multi";
            InventoryTab_CheckBox_Multi.Size = new Size(108, 19);
            InventoryTab_CheckBox_Multi.TabIndex = 7;
            InventoryTab_CheckBox_Multi.TabStop = false;
            InventoryTab_CheckBox_Multi.Text = "Multiple Entries";
            InventoryTab_CheckBox_Multi.UseVisualStyleBackColor = true;
            InventoryTab_CheckBox_Multi.CheckedChanged += InventoryTab_CheckBox_Multiple_Changed;
            // 
            // InventoryTab_Label_Part
            // 
            InventoryTab_Label_Part.AutoSize = true;
            InventoryTab_Label_Part.Location = new Point(3, 8);
            InventoryTab_Label_Part.Name = "InventoryTab_Label_Part";
            InventoryTab_Label_Part.Size = new Size(78, 15);
            InventoryTab_Label_Part.TabIndex = 4;
            InventoryTab_Label_Part.Text = "Part Number:";
            // 
            // InventoryTab_Label_Qty
            // 
            InventoryTab_Label_Qty.AutoSize = true;
            InventoryTab_Label_Qty.Location = new Point(3, 70);
            InventoryTab_Label_Qty.Name = "InventoryTab_Label_Qty";
            InventoryTab_Label_Qty.Size = new Size(56, 15);
            InventoryTab_Label_Qty.TabIndex = 6;
            InventoryTab_Label_Qty.Text = "Quantity:";
            // 
            // InventoryTab_Label_Loc
            // 
            InventoryTab_Label_Loc.AutoSize = true;
            InventoryTab_Label_Loc.Location = new Point(3, 101);
            InventoryTab_Label_Loc.Name = "InventoryTab_Label_Loc";
            InventoryTab_Label_Loc.Size = new Size(56, 15);
            InventoryTab_Label_Loc.TabIndex = 8;
            InventoryTab_Label_Loc.Text = "Location:";
            // 
            // MainForm_TabControl_Remove
            // 
            MainForm_TabControl_Remove.Controls.Add(RemoveTab_GroupBox_Main);
            MainForm_TabControl_Remove.Location = new Point(4, 24);
            MainForm_TabControl_Remove.Name = "MainForm_TabControl_Remove";
            MainForm_TabControl_Remove.Padding = new Padding(3);
            MainForm_TabControl_Remove.Size = new Size(831, 404);
            MainForm_TabControl_Remove.TabIndex = 1;
            MainForm_TabControl_Remove.Text = "Remove (Ctrl + 2)";
            MainForm_TabControl_Remove.ToolTipText = "Shortcut: Ctrl+2";
            MainForm_TabControl_Remove.UseVisualStyleBackColor = true;
            // 
            // MainForm_TabControl_Transfer
            // 
            MainForm_TabControl_Transfer.Controls.Add(TransferTab_Group_Main);
            MainForm_TabControl_Transfer.Location = new Point(4, 24);
            MainForm_TabControl_Transfer.Name = "MainForm_TabControl_Transfer";
            MainForm_TabControl_Transfer.Padding = new Padding(3);
            MainForm_TabControl_Transfer.Size = new Size(831, 404);
            MainForm_TabControl_Transfer.TabIndex = 2;
            MainForm_TabControl_Transfer.Text = "Transfer (Ctrl+3)";
            MainForm_TabControl_Transfer.ToolTipText = "Shortcut: Ctrl+3";
            MainForm_TabControl_Transfer.UseVisualStyleBackColor = true;
            // 
            // TransferTab_Group_Main
            // 
            TransferTab_Group_Main.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            TransferTab_Group_Main.Controls.Add(panel4);
            TransferTab_Group_Main.Controls.Add(TransferTab_Panel_DataGrid);
            TransferTab_Group_Main.Controls.Add(panel5);
            TransferTab_Group_Main.Location = new Point(3, 3);
            TransferTab_Group_Main.Name = "TransferTab_Group_Main";
            TransferTab_Group_Main.Size = new Size(825, 398);
            TransferTab_Group_Main.TabIndex = 0;
            TransferTab_Group_Main.TabStop = false;
            TransferTab_Group_Main.Text = "Location Change";
            // 
            // panel4
            // 
            panel4.Controls.Add(TransferTab_Button_Search);
            panel4.Controls.Add(TransferTab_ComboBox_Part);
            panel4.Controls.Add(TransferTab_Label_Part);
            panel4.Controls.Add(TransferTab_Label_Loc);
            panel4.Controls.Add(TransferTab_ComboBox_Loc);
            panel4.Dock = DockStyle.Top;
            panel4.Location = new Point(3, 19);
            panel4.Name = "panel4";
            panel4.Size = new Size(819, 36);
            panel4.TabIndex = 23;
            // 
            // TransferTab_Button_Search
            // 
            TransferTab_Button_Search.Dock = DockStyle.Right;
            TransferTab_Button_Search.FlatStyle = FlatStyle.System;
            TransferTab_Button_Search.Location = new Point(732, 0);
            TransferTab_Button_Search.Name = "TransferTab_Button_Search";
            TransferTab_Button_Search.Size = new Size(87, 36);
            TransferTab_Button_Search.TabIndex = 2;
            TransferTab_Button_Search.Text = "Search";
            MainForm_ToolTip.SetToolTip(TransferTab_Button_Search, "Shortcut: None");
            TransferTab_Button_Search.UseVisualStyleBackColor = true;
            TransferTab_Button_Search.Click += TransferTab_Button_Search_Clicked;
            // 
            // TransferTab_ComboBox_Part
            // 
            TransferTab_ComboBox_Part.AutoCompleteMode = AutoCompleteMode.Suggest;
            TransferTab_ComboBox_Part.AutoCompleteSource = AutoCompleteSource.ListItems;
            TransferTab_ComboBox_Part.FormattingEnabled = true;
            TransferTab_ComboBox_Part.Location = new Point(86, 7);
            TransferTab_ComboBox_Part.Name = "TransferTab_ComboBox_Part";
            TransferTab_ComboBox_Part.Size = new Size(282, 23);
            TransferTab_ComboBox_Part.TabIndex = 1;
            TransferTab_ComboBox_Part.SelectedIndexChanged += TransferTab_ComboBox_Part_SelectedIndexChanged;
            // 
            // TransferTab_Label_Part
            // 
            TransferTab_Label_Part.AutoSize = true;
            TransferTab_Label_Part.Location = new Point(3, 11);
            TransferTab_Label_Part.Name = "TransferTab_Label_Part";
            TransferTab_Label_Part.Size = new Size(78, 15);
            TransferTab_Label_Part.TabIndex = 4;
            TransferTab_Label_Part.Text = "Part Number:";
            // 
            // TransferTab_Label_Loc
            // 
            TransferTab_Label_Loc.AutoSize = true;
            TransferTab_Label_Loc.Location = new Point(374, 10);
            TransferTab_Label_Loc.Name = "TransferTab_Label_Loc";
            TransferTab_Label_Loc.Size = new Size(83, 15);
            TransferTab_Label_Loc.TabIndex = 18;
            TransferTab_Label_Loc.Text = "New Location:";
            // 
            // TransferTab_ComboBox_Loc
            // 
            TransferTab_ComboBox_Loc.AutoCompleteMode = AutoCompleteMode.Suggest;
            TransferTab_ComboBox_Loc.AutoCompleteSource = AutoCompleteSource.ListItems;
            TransferTab_ComboBox_Loc.FormattingEnabled = true;
            TransferTab_ComboBox_Loc.Location = new Point(463, 7);
            TransferTab_ComboBox_Loc.Name = "TransferTab_ComboBox_Loc";
            TransferTab_ComboBox_Loc.Size = new Size(263, 23);
            TransferTab_ComboBox_Loc.TabIndex = 4;
            TransferTab_ComboBox_Loc.SelectedIndexChanged += TransferTab_ComboBox_Location_SelectedIndexChanged;
            // 
            // TransferTab_Panel_DataGrid
            // 
            TransferTab_Panel_DataGrid.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            TransferTab_Panel_DataGrid.Controls.Add(TransferTab_Image_Nothing);
            TransferTab_Panel_DataGrid.Controls.Add(TransferDataGrid);
            TransferTab_Panel_DataGrid.Location = new Point(3, 55);
            TransferTab_Panel_DataGrid.Name = "TransferTab_Panel_DataGrid";
            TransferTab_Panel_DataGrid.Size = new Size(819, 305);
            TransferTab_Panel_DataGrid.TabIndex = 25;
            // 
            // TransferTab_Image_Nothing
            // 
            TransferTab_Image_Nothing.Dock = DockStyle.Fill;
            TransferTab_Image_Nothing.ErrorImage = null;
            TransferTab_Image_Nothing.Image = Properties.Resources._4041;
            TransferTab_Image_Nothing.InitialImage = null;
            TransferTab_Image_Nothing.Location = new Point(0, 0);
            TransferTab_Image_Nothing.Name = "TransferTab_Image_Nothing";
            TransferTab_Image_Nothing.Size = new Size(819, 305);
            TransferTab_Image_Nothing.SizeMode = PictureBoxSizeMode.CenterImage;
            TransferTab_Image_Nothing.TabIndex = 19;
            TransferTab_Image_Nothing.TabStop = false;
            TransferTab_Image_Nothing.Visible = false;
            // 
            // TransferDataGrid
            // 
            TransferDataGrid.AllowUserToAddRows = false;
            TransferDataGrid.AllowUserToDeleteRows = false;
            TransferDataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            TransferDataGrid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            TransferDataGrid.BorderStyle = BorderStyle.Fixed3D;
            TransferDataGrid.CellBorderStyle = DataGridViewCellBorderStyle.Sunken;
            TransferDataGrid.ClipboardCopyMode = DataGridViewClipboardCopyMode.Disable;
            TransferDataGrid.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Sunken;
            TransferDataGrid.ColumnHeadersHeight = 34;
            TransferDataGrid.Dock = DockStyle.Fill;
            TransferDataGrid.EditMode = DataGridViewEditMode.EditProgrammatically;
            TransferDataGrid.Location = new Point(0, 0);
            TransferDataGrid.MultiSelect = false;
            TransferDataGrid.Name = "TransferDataGrid";
            TransferDataGrid.ReadOnly = true;
            TransferDataGrid.RowHeadersWidth = 62;
            TransferDataGrid.RowTemplate.ReadOnly = true;
            TransferDataGrid.RowTemplate.Resizable = DataGridViewTriState.True;
            TransferDataGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            TransferDataGrid.ShowCellErrors = false;
            TransferDataGrid.ShowCellToolTips = false;
            TransferDataGrid.ShowEditingIcon = false;
            TransferDataGrid.ShowRowErrors = false;
            TransferDataGrid.Size = new Size(819, 305);
            TransferDataGrid.StandardTab = true;
            TransferDataGrid.TabIndex = 5;
            TransferDataGrid.TabStop = false;
            TransferDataGrid.SelectionChanged += TransferTab_DataGrid_SelectionChanged;
            // 
            // panel5
            // 
            panel5.Controls.Add(TransferTab_TextBox_Qty);
            panel5.Controls.Add(TransferTab_Button_Save);
            panel5.Controls.Add(TransferTab_Label_Qty);
            panel5.Controls.Add(TransferTab_Button_Reset);
            panel5.Dock = DockStyle.Bottom;
            panel5.Location = new Point(3, 360);
            panel5.Name = "panel5";
            panel5.Size = new Size(819, 35);
            panel5.TabIndex = 24;
            // 
            // TransferTab_TextBox_Qty
            // 
            TransferTab_TextBox_Qty.Location = new Point(65, 6);
            TransferTab_TextBox_Qty.Name = "TransferTab_TextBox_Qty";
            TransferTab_TextBox_Qty.Size = new Size(187, 23);
            TransferTab_TextBox_Qty.TabIndex = 3;
            TransferTab_TextBox_Qty.TextChanged += TransferTab_TextBox_Qty_TextChanged;
            TransferTab_TextBox_Qty.KeyPress += TransferTab_TextBox_Qty_KeyPress;
            // 
            // TransferTab_Button_Save
            // 
            TransferTab_Button_Save.Dock = DockStyle.Right;
            TransferTab_Button_Save.FlatStyle = FlatStyle.System;
            TransferTab_Button_Save.Location = new Point(647, 0);
            TransferTab_Button_Save.Name = "TransferTab_Button_Save";
            TransferTab_Button_Save.Size = new Size(86, 35);
            TransferTab_Button_Save.TabIndex = 6;
            TransferTab_Button_Save.Text = "Save";
            MainForm_ToolTip.SetToolTip(TransferTab_Button_Save, "Shortcut: Ctrl+S");
            TransferTab_Button_Save.UseVisualStyleBackColor = true;
            TransferTab_Button_Save.Click += TransferTab_Button_Save_Clicked;
            // 
            // TransferTab_Label_Qty
            // 
            TransferTab_Label_Qty.AutoSize = true;
            TransferTab_Label_Qty.Location = new Point(3, 10);
            TransferTab_Label_Qty.Name = "TransferTab_Label_Qty";
            TransferTab_Label_Qty.Size = new Size(56, 15);
            TransferTab_Label_Qty.TabIndex = 20;
            TransferTab_Label_Qty.Text = "Quantity:";
            MainForm_ToolTip.SetToolTip(TransferTab_Label_Qty, "Enter an amount to have the application subtract\r\nthat amount from your selected location.\r\n\r\nLeave this blank to transfer the entire amount.");
            // 
            // TransferTab_Button_Reset
            // 
            TransferTab_Button_Reset.Dock = DockStyle.Right;
            TransferTab_Button_Reset.FlatStyle = FlatStyle.System;
            TransferTab_Button_Reset.Location = new Point(733, 0);
            TransferTab_Button_Reset.Name = "TransferTab_Button_Reset";
            TransferTab_Button_Reset.Size = new Size(86, 35);
            TransferTab_Button_Reset.TabIndex = 19;
            TransferTab_Button_Reset.Text = "Reset";
            MainForm_ToolTip.SetToolTip(TransferTab_Button_Reset, "Shortcut: Ctrl+R");
            TransferTab_Button_Reset.UseVisualStyleBackColor = true;
            TransferTab_Button_Reset.Click += TransferTab_Button_Reset_Clicked;
            // 
            // MainForm_StatusStrip
            // 
            MainForm_StatusStrip.ImageScalingSize = new Size(24, 24);
            MainForm_StatusStrip.Items.AddRange(new ToolStripItem[] { MainForm_StatusStrip_SavedStatus, MainForm_StatusStrip_Disconnected });
            MainForm_StatusStrip.Location = new Point(0, 459);
            MainForm_StatusStrip.Name = "MainForm_StatusStrip";
            MainForm_StatusStrip.Size = new Size(984, 22);
            MainForm_StatusStrip.SizingGrip = false;
            MainForm_StatusStrip.TabIndex = 18;
            // 
            // MainForm_StatusStrip_SavedStatus
            // 
            MainForm_StatusStrip_SavedStatus.Name = "MainForm_StatusStrip_SavedStatus";
            MainForm_StatusStrip_SavedStatus.Size = new Size(0, 17);
            // 
            // MainForm_StatusStrip_Disconnected
            // 
            MainForm_StatusStrip_Disconnected.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold | FontStyle.Italic);
            MainForm_StatusStrip_Disconnected.ForeColor = Color.Red;
            MainForm_StatusStrip_Disconnected.Name = "MainForm_StatusStrip_Disconnected";
            MainForm_StatusStrip_Disconnected.Size = new Size(240, 17);
            MainForm_StatusStrip_Disconnected.Text = "Disconnected from Server, please standby...";
            MainForm_StatusStrip_Disconnected.Visible = false;
            // 
            // Inventory_PrintDialog
            // 
            Inventory_PrintDialog.AutoScrollMargin = new Size(0, 0);
            Inventory_PrintDialog.AutoScrollMinSize = new Size(0, 0);
            Inventory_PrintDialog.ClientSize = new Size(400, 300);
            Inventory_PrintDialog.Enabled = true;
            Inventory_PrintDialog.Icon = (Icon)resources.GetObject("Inventory_PrintDialog.Icon");
            Inventory_PrintDialog.Name = "Inventory_PrintDialog";
            Inventory_PrintDialog.Visible = false;
            // 
            // MainForm_GroupBox_Last10
            // 
            MainForm_GroupBox_Last10.Controls.Add(MainForm_Last10_Button_10);
            MainForm_GroupBox_Last10.Controls.Add(MainForm_Last10_Button_09);
            MainForm_GroupBox_Last10.Controls.Add(MainForm_Last10_Button_08);
            MainForm_GroupBox_Last10.Controls.Add(MainForm_Last10_Button_07);
            MainForm_GroupBox_Last10.Controls.Add(MainForm_Last10_Button_06);
            MainForm_GroupBox_Last10.Controls.Add(MainForm_Last10_Button_05);
            MainForm_GroupBox_Last10.Controls.Add(MainForm_Last10_Button_04);
            MainForm_GroupBox_Last10.Controls.Add(MainForm_Last10_Button_03);
            MainForm_GroupBox_Last10.Controls.Add(MainForm_Last10_Button_02);
            MainForm_GroupBox_Last10.Controls.Add(MainForm_Last10_Button_01);
            MainForm_GroupBox_Last10.Dock = DockStyle.Right;
            MainForm_GroupBox_Last10.Location = new Point(839, 24);
            MainForm_GroupBox_Last10.Name = "MainForm_GroupBox_Last10";
            MainForm_GroupBox_Last10.Size = new Size(145, 435);
            MainForm_GroupBox_Last10.TabIndex = 91;
            MainForm_GroupBox_Last10.TabStop = false;
            MainForm_GroupBox_Last10.Text = "Quick Keys";
            MainForm_ToolTip.SetToolTip(MainForm_GroupBox_Last10, resources.GetString("MainForm_GroupBox_Last10.ToolTip"));
            // 
            // MainForm_Last10_Button_10
            // 
            MainForm_Last10_Button_10.Enabled = false;
            MainForm_Last10_Button_10.Location = new Point(6, 384);
            MainForm_Last10_Button_10.Name = "MainForm_Last10_Button_10";
            MainForm_Last10_Button_10.Size = new Size(133, 40);
            MainForm_Last10_Button_10.TabIndex = 9;
            MainForm_Last10_Button_10.TabStop = false;
            MainForm_Last10_Button_10.Text = "[ Nothing Yet ]";
            MainForm_Last10_Button_10.UseVisualStyleBackColor = true;
            MainForm_Last10_Button_10.TextChanged += Primary_Button_Last10_TextChanged;
            MainForm_Last10_Button_10.Click += Primary_Button_Last10_10_Clicked;
            // 
            // MainForm_Last10_Button_09
            // 
            MainForm_Last10_Button_09.Enabled = false;
            MainForm_Last10_Button_09.Location = new Point(6, 344);
            MainForm_Last10_Button_09.Name = "MainForm_Last10_Button_09";
            MainForm_Last10_Button_09.Size = new Size(133, 40);
            MainForm_Last10_Button_09.TabIndex = 8;
            MainForm_Last10_Button_09.TabStop = false;
            MainForm_Last10_Button_09.Text = "[ Nothing Yet ]";
            MainForm_Last10_Button_09.UseVisualStyleBackColor = true;
            MainForm_Last10_Button_09.TextChanged += Primary_Button_Last10_TextChanged;
            MainForm_Last10_Button_09.Click += Primary_Button_Last10_09_Clicked;
            // 
            // MainForm_Last10_Button_08
            // 
            MainForm_Last10_Button_08.Enabled = false;
            MainForm_Last10_Button_08.Location = new Point(6, 304);
            MainForm_Last10_Button_08.Name = "MainForm_Last10_Button_08";
            MainForm_Last10_Button_08.Size = new Size(133, 40);
            MainForm_Last10_Button_08.TabIndex = 7;
            MainForm_Last10_Button_08.TabStop = false;
            MainForm_Last10_Button_08.Text = "[ Nothing Yet ]";
            MainForm_Last10_Button_08.UseVisualStyleBackColor = true;
            MainForm_Last10_Button_08.TextChanged += Primary_Button_Last10_TextChanged;
            MainForm_Last10_Button_08.Click += Primary_Button_Last10_08_Clicked;
            // 
            // MainForm_Last10_Button_07
            // 
            MainForm_Last10_Button_07.Enabled = false;
            MainForm_Last10_Button_07.Location = new Point(6, 264);
            MainForm_Last10_Button_07.Name = "MainForm_Last10_Button_07";
            MainForm_Last10_Button_07.Size = new Size(133, 40);
            MainForm_Last10_Button_07.TabIndex = 6;
            MainForm_Last10_Button_07.TabStop = false;
            MainForm_Last10_Button_07.Text = "[ Nothing Yet ]";
            MainForm_Last10_Button_07.UseVisualStyleBackColor = true;
            MainForm_Last10_Button_07.TextChanged += Primary_Button_Last10_TextChanged;
            MainForm_Last10_Button_07.Click += Primary_Button_Last10_07_Clicked;
            // 
            // MainForm_Last10_Button_06
            // 
            MainForm_Last10_Button_06.Enabled = false;
            MainForm_Last10_Button_06.Location = new Point(6, 224);
            MainForm_Last10_Button_06.Name = "MainForm_Last10_Button_06";
            MainForm_Last10_Button_06.Size = new Size(133, 40);
            MainForm_Last10_Button_06.TabIndex = 5;
            MainForm_Last10_Button_06.TabStop = false;
            MainForm_Last10_Button_06.Text = "[ Nothing Yet ]";
            MainForm_Last10_Button_06.UseVisualStyleBackColor = true;
            MainForm_Last10_Button_06.TextChanged += Primary_Button_Last10_TextChanged;
            MainForm_Last10_Button_06.Click += Primary_Button_Last10_06_Clicked;
            // 
            // MainForm_Last10_Button_05
            // 
            MainForm_Last10_Button_05.Enabled = false;
            MainForm_Last10_Button_05.Location = new Point(6, 184);
            MainForm_Last10_Button_05.Name = "MainForm_Last10_Button_05";
            MainForm_Last10_Button_05.Size = new Size(133, 40);
            MainForm_Last10_Button_05.TabIndex = 4;
            MainForm_Last10_Button_05.TabStop = false;
            MainForm_Last10_Button_05.Text = "[ Nothing Yet ]";
            MainForm_Last10_Button_05.UseVisualStyleBackColor = true;
            MainForm_Last10_Button_05.TextChanged += Primary_Button_Last10_TextChanged;
            MainForm_Last10_Button_05.Click += Primary_Button_Last10_05_Clicked;
            // 
            // MainForm_Last10_Button_04
            // 
            MainForm_Last10_Button_04.Enabled = false;
            MainForm_Last10_Button_04.Location = new Point(6, 144);
            MainForm_Last10_Button_04.Name = "MainForm_Last10_Button_04";
            MainForm_Last10_Button_04.Size = new Size(133, 40);
            MainForm_Last10_Button_04.TabIndex = 3;
            MainForm_Last10_Button_04.TabStop = false;
            MainForm_Last10_Button_04.Text = "[ Nothing Yet ]";
            MainForm_Last10_Button_04.UseVisualStyleBackColor = true;
            MainForm_Last10_Button_04.TextChanged += Primary_Button_Last10_TextChanged;
            MainForm_Last10_Button_04.Click += Primary_Button_Last10_04_Clicked;
            // 
            // MainForm_Last10_Button_03
            // 
            MainForm_Last10_Button_03.Enabled = false;
            MainForm_Last10_Button_03.Location = new Point(6, 104);
            MainForm_Last10_Button_03.Name = "MainForm_Last10_Button_03";
            MainForm_Last10_Button_03.Size = new Size(133, 40);
            MainForm_Last10_Button_03.TabIndex = 2;
            MainForm_Last10_Button_03.TabStop = false;
            MainForm_Last10_Button_03.Text = "[ Nothing Yet ]";
            MainForm_Last10_Button_03.UseVisualStyleBackColor = true;
            MainForm_Last10_Button_03.TextChanged += Primary_Button_Last10_TextChanged;
            MainForm_Last10_Button_03.Click += Primary_Button_Last10_03_Clicked;
            // 
            // MainForm_Last10_Button_02
            // 
            MainForm_Last10_Button_02.Enabled = false;
            MainForm_Last10_Button_02.Location = new Point(6, 64);
            MainForm_Last10_Button_02.Name = "MainForm_Last10_Button_02";
            MainForm_Last10_Button_02.Size = new Size(133, 40);
            MainForm_Last10_Button_02.TabIndex = 1;
            MainForm_Last10_Button_02.TabStop = false;
            MainForm_Last10_Button_02.Text = "[ Nothing Yet ]";
            MainForm_Last10_Button_02.UseVisualStyleBackColor = true;
            MainForm_Last10_Button_02.TextChanged += Primary_Button_Last10_TextChanged;
            MainForm_Last10_Button_02.Click += Primary_Button_Last10_02_Clicked;
            // 
            // MainForm_Last10_Button_01
            // 
            MainForm_Last10_Button_01.Location = new Point(6, 24);
            MainForm_Last10_Button_01.Name = "MainForm_Last10_Button_01";
            MainForm_Last10_Button_01.Size = new Size(133, 40);
            MainForm_Last10_Button_01.TabIndex = 0;
            MainForm_Last10_Button_01.TabStop = false;
            MainForm_Last10_Button_01.Text = "[ Nothing Yet ]";
            MainForm_Last10_Button_01.UseVisualStyleBackColor = true;
            MainForm_Last10_Button_01.TextChanged += Primary_Button_Last10_TextChanged;
            MainForm_Last10_Button_01.Click += Primary_Button_Last10_01_Clicked;
            // 
            // Last10Timer
            // 
            Last10Timer.Enabled = true;
            Last10Timer.Interval = 15000;
            Last10Timer.Tick += Primary_Timer_Last10_Tick;
            // 
            // MainForm
            // 
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new Size(984, 481);
            Controls.Add(MainForm_TabControl);
            Controls.Add(MainForm_GroupBox_Last10);
            Controls.Add(MainForm_StatusStrip);
            Controls.Add(MainForm_MenuStrip);
            HelpButton = true;
            Icon = (Icon)resources.GetObject("$this.Icon");
            IsMdiContainer = true;
            KeyPreview = true;
            MainMenuStrip = MainForm_MenuStrip;
            MaximizeBox = false;
            MaximumSize = new Size(1000, 520);
            MinimumSize = new Size(1000, 520);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Manitowoc Tool and Manufacturing | WIP Inventory System |";
            Activated += Helper_MainForm_RegainFocus;
            Load += Primary_MainForm_LoadAsync;
            Enter += Helper_MainForm_RegainFocus;
            MainForm_MenuStrip.ResumeLayout(false);
            MainForm_MenuStrip.PerformLayout();
            RemoveTab_GroupBox_Main.ResumeLayout(false);
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            RemoveTab_Panel_DataGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)RemoveTab_Image_NothingFound).EndInit();
            ((System.ComponentModel.ISupportInitialize)RemoveTab_DataGrid).EndInit();
            panel1.ResumeLayout(false);
            RemoveTab_Panel_SeachByType.ResumeLayout(false);
            RemoveTab_Panel_SeachByType.PerformLayout();
            MainForm_TabControl.ResumeLayout(false);
            MainForm_TabControl_Inventory.ResumeLayout(false);
            InventoryTab_Group_Main.ResumeLayout(false);
            InventoryTab_Group_Main.PerformLayout();
            InventoryBottomGroup.ResumeLayout(false);
            InventoryTab_Group_Top.ResumeLayout(false);
            InventoryTab_Group_Top.PerformLayout();
            MainForm_TabControl_Remove.ResumeLayout(false);
            MainForm_TabControl_Transfer.ResumeLayout(false);
            TransferTab_Group_Main.ResumeLayout(false);
            panel4.ResumeLayout(false);
            panel4.PerformLayout();
            TransferTab_Panel_DataGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)TransferTab_Image_Nothing).EndInit();
            ((System.ComponentModel.ISupportInitialize)TransferDataGrid).EndInit();
            panel5.ResumeLayout(false);
            panel5.PerformLayout();
            MainForm_StatusStrip.ResumeLayout(false);
            MainForm_StatusStrip.PerformLayout();
            MainForm_GroupBox_Last10.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private MenuStrip MainForm_MenuStrip;
        private ToolStripMenuItem MainForm_MenuStrip_File;
        private ToolStripMenuItem MainForm_MenuStrip_Edit;
        private ToolStripMenuItem MainForm_MenuStrip_View;
        private GroupBox RemoveTab_GroupBox_Main;
        private Button RemoveTab_Button_Delete;
        private Label RemoveTab_Label_Op;
        private Label RemoveTab_Label_Part;
        private Button RemoveTab_Button_Search;
        private ToolStripMenuItem MainForm_MenuStrip_File_Settings;
        public ComboBox RemoveTab_ComboBox_Part;
        private ToolStripMenuItem viewAllWIPToolStripMenuItem;
        public DataGridView RemoveTab_DataGrid;
        public ComboBox RemoveTab_ComboBox_Op;
        private Panel panel1;
        private Panel panel3;
        private Panel RemoveTab_Panel_DataGrid;
        private TabPage MainForm_TabControl_Remove;
        internal TabPage MainForm_TabControl_Inventory;
        private TabPage MainForm_TabControl_Transfer;
        private Panel panel5;
        private Panel panel4;
        private Label TransferTab_Label_Part;
        private Panel TransferTab_Panel_DataGrid;
        private Label TransferTab_Label_Loc;
        private ToolStripMenuItem personalHistoryToolStripMenuItem;
        public StatusStrip MainForm_StatusStrip;
        public ToolStripStatusLabel MainForm_StatusStrip_SavedStatus;
        internal TabControl MainForm_TabControl;
        internal ComboBox TransferTab_ComboBox_Part;
        internal DataGridView TransferDataGrid;
        internal ComboBox TransferTab_ComboBox_Loc;
        internal Button TransferTab_Button_Save;
        internal Button TransferTab_Button_Search;
        private Button RemoveTab_Button_Reset;
        private Button RemoveTab_Button_Print;
        private System.Drawing.Printing.PrintDocument Inventory_PrintDocument;
        private PrintPreviewDialog Inventory_PrintDialog;
        private ToolStripMenuItem MainForm_MenuStrip_File_Save;
        private ToolStripMenuItem MainForm_MenuStrip_File_Print;
        private ToolStripMenuItem MainForm_MenuStrip_View_Reset;
        private GroupBox TransferTab_Group_Main;
        private GroupBox InventoryTab_Group_Main;
        private Panel InventoryBottomGroup;
        internal Button InventoryTab_Button_Reset;
        internal Button InventoryTab_Button_Save;
        private Panel InventoryTab_Group_Top;
        private Label InventoryTab_Label_Op;
        private Label InventoryTab_Label_Notes;
        internal ComboBox InventoryTab_ComboBox_Loc;
        internal RichTextBox InventoryTab_RichTextBox_Notes;
        internal TextBox InventoryTab_TextBox_Qty;
        internal ComboBox InventoryTab_ComboBox_Op;
        private Label InventoryTab_Label_Part;
        internal ComboBox InventoryTab_ComboBox_Part;
        private Label InventoryTab_Label_Qty;
        private Label InventoryTab_Label_Loc;
        private TextBox InventoryTab_TextBox_HowMany;
        private Label InventoryTab_Label_HowMany;
        internal CheckBox InventoryTab_CheckBox_Multi;
        internal Button TransferTab_Button_Reset;
        private ToolStripMenuItem MainForm_MenuStrip_Exit;
        private ToolStripMenuItem viewOutsideServiceToolStripMenuItem;
        internal CheckBox InventoryTab_CheckBox_Multi_Different;
        private ToolStripMenuItem MainForm_MenuStrip_File_Delete;
        private Label InventoryTab_Label_Version;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem addToInventoryToolStripMenuItem;
        private ToolStripMenuItem removeFromInventoryToolStripMenuItem;
        private ToolStripMenuItem locationToLocationToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator2;
        private ToolTip MainForm_ToolTip;
        private GroupBox MainForm_GroupBox_Last10;
        internal Button MainForm_Last10_Button_01;
        internal Button MainForm_Last10_Button_10;
        internal Button MainForm_Last10_Button_09;
        internal Button MainForm_Last10_Button_08;
        internal Button MainForm_Last10_Button_07;
        internal Button MainForm_Last10_Button_06;
        internal Button MainForm_Last10_Button_05;
        internal Button MainForm_Last10_Button_04;
        internal Button MainForm_Last10_Button_03;
        internal Button MainForm_Last10_Button_02;
        internal System.Windows.Forms.Timer Last10Timer;
        internal ToolStripStatusLabel MainForm_StatusStrip_Disconnected;
        private Button MainForm_Button_ShowHideLast10;
        private Panel RemoveTab_Panel_SeachByType;
        private Label RemoveTab_Label_SearchByType;
        private ComboBox RemoveTab_CBox_ShowAll;
        private ToolStripMenuItem resetLast10ButtonsToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator3;
        private Label TransferTab_Label_Qty;
        private TextBox TransferTab_TextBox_Qty;
        private ToolStripMenuItem viewChangelogToolStripMenuItem;
        private Button RemoveTab_Button_Edit;
        private PictureBox RemoveTab_Image_NothingFound;
        private PictureBox TransferTab_Image_Nothing;
        private ToolStripMenuItem MainToolStrip_New_Object;
        private ToolStripMenuItem CallUnifiedRemovalForm;
        private ToolStripSeparator toolStripSeparator4;
    }
}