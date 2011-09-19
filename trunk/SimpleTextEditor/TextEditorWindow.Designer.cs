namespace SimpleTextEditor
{
    partial class TextEditorWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TextEditorWindow));
            this.toolBar = new System.Windows.Forms.ToolStrip();
            this.btnNew = new System.Windows.Forms.ToolStripButton();
            this.btnOpen = new System.Windows.Forms.ToolStripSplitButton();
            this.mnuOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.separator2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuRecent = new System.Windows.Forms.ToolStripMenuItem();
            this.btnSave = new System.Windows.Forms.ToolStripSplitButton();
            this.mnuSave = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.separator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnPrint = new System.Windows.Forms.ToolStripSplitButton();
            this.mnuPrint = new System.Windows.Forms.ToolStripMenuItem();
            this.separator3 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuPageOptions = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuPrintPreview = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuPrintDialog = new System.Windows.Forms.ToolStripMenuItem();
            this.separator4 = new System.Windows.Forms.ToolStripSeparator();
            this.btnCut = new System.Windows.Forms.ToolStripButton();
            this.btnCopy = new System.Windows.Forms.ToolStripButton();
            this.btnPaste = new System.Windows.Forms.ToolStripButton();
            this.separator5 = new System.Windows.Forms.ToolStripSeparator();
            this.btnUndo = new System.Windows.Forms.ToolStripSplitButton();
            this.btnRedo = new System.Windows.Forms.ToolStripSplitButton();
            this.separator6 = new System.Windows.Forms.ToolStripSeparator();
            this.btnHelp = new System.Windows.Forms.ToolStripButton();
            this.btnInsertTime = new System.Windows.Forms.ToolStripButton();
            this.separator7 = new System.Windows.Forms.ToolStripSeparator();
            this.btnSearch = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
            this.separator9 = new System.Windows.Forms.ToolStripSeparator();
            this.btnOptions = new System.Windows.Forms.ToolStripDropDownButton();
            this.mnuFont = new System.Windows.Forms.ToolStripMenuItem();
            this.backgroundColorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.separator10 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuWordWrap = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuStatusBar = new System.Windows.Forms.ToolStripMenuItem();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.fontDialog1 = new System.Windows.Forms.FontDialog();
            this.pageSetupDialog1 = new System.Windows.Forms.PageSetupDialog();
            this.printDialog1 = new System.Windows.Forms.PrintDialog();
            this.printPreviewDialog1 = new System.Windows.Forms.PrintPreviewDialog();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.statusBar = new System.Windows.Forms.StatusStrip();
            this.lblSpacer = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblPosition = new System.Windows.Forms.ToolStripStatusLabel();
            this.searchBar = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.searchBox = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton5 = new System.Windows.Forms.ToolStripButton();
            this.searchEndLabel = new System.Windows.Forms.ToolStripLabel();
            this.editBox = new System.Windows.Forms.TextBox();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.registerFileAssociationsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolBar.SuspendLayout();
            this.statusBar.SuspendLayout();
            this.searchBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolBar
            // 
            this.toolBar.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNew,
            this.btnOpen,
            this.btnSave,
            this.separator1,
            this.btnPrint,
            this.separator4,
            this.btnCut,
            this.btnCopy,
            this.btnPaste,
            this.separator5,
            this.btnUndo,
            this.btnRedo,
            this.separator6,
            this.btnHelp,
            this.btnInsertTime,
            this.separator7,
            this.btnSearch,
            this.toolStripButton4,
            this.separator9,
            this.btnOptions});
            this.toolBar.Location = new System.Drawing.Point(0, 0);
            this.toolBar.Name = "toolBar";
            this.toolBar.Padding = new System.Windows.Forms.Padding(4, 0, 1, 0);
            this.toolBar.Size = new System.Drawing.Size(788, 25);
            this.toolBar.TabIndex = 0;
            this.toolBar.Text = "Print";
            this.toolBar.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.toolBar_ItemClicked);
            // 
            // btnNew
            // 
            this.btnNew.Image = ((System.Drawing.Image)(resources.GetObject("btnNew.Image")));
            this.btnNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(51, 22);
            this.btnNew.Text = "&New";
            this.btnNew.Click += new System.EventHandler(this.newToolStripButton_Click);
            // 
            // btnOpen
            // 
            this.btnOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnOpen.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuOpen,
            this.separator2,
            this.mnuRecent});
            this.btnOpen.Image = ((System.Drawing.Image)(resources.GetObject("btnOpen.Image")));
            this.btnOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(32, 22);
            this.btnOpen.Text = "&Open";
            this.btnOpen.ButtonClick += new System.EventHandler(this.openToolStripButton_ButtonClick);
            this.btnOpen.Click += new System.EventHandler(this.openToolStripButton_Click);
            // 
            // mnuOpen
            // 
            this.mnuOpen.Image = ((System.Drawing.Image)(resources.GetObject("mnuOpen.Image")));
            this.mnuOpen.Name = "mnuOpen";
            this.mnuOpen.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.mnuOpen.Size = new System.Drawing.Size(195, 22);
            this.mnuOpen.Text = "Open...";
            this.mnuOpen.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // separator2
            // 
            this.separator2.Name = "separator2";
            this.separator2.Size = new System.Drawing.Size(192, 6);
            // 
            // mnuRecent
            // 
            this.mnuRecent.Enabled = false;
            this.mnuRecent.Name = "mnuRecent";
            this.mnuRecent.Size = new System.Drawing.Size(195, 22);
            this.mnuRecent.Text = "(no items in recent list)";
            this.mnuRecent.Click += new System.EventHandler(this.mruToolStripMenuItem_Click);
            // 
            // btnSave
            // 
            this.btnSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSave.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuSave,
            this.mnuSaveAs});
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(32, 22);
            this.btnSave.Text = "&Save";
            this.btnSave.ButtonClick += new System.EventHandler(this.saveToolStripButton_ButtonClick);
            // 
            // mnuSave
            // 
            this.mnuSave.Image = ((System.Drawing.Image)(resources.GetObject("mnuSave.Image")));
            this.mnuSave.Name = "mnuSave";
            this.mnuSave.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.mnuSave.Size = new System.Drawing.Size(138, 22);
            this.mnuSave.Text = "Save";
            // 
            // mnuSaveAs
            // 
            this.mnuSaveAs.Name = "mnuSaveAs";
            this.mnuSaveAs.Size = new System.Drawing.Size(138, 22);
            this.mnuSaveAs.Text = "Save as...";
            this.mnuSaveAs.Click += new System.EventHandler(this.saveAsToolStripMenuItem1_Click);
            // 
            // separator1
            // 
            this.separator1.Name = "separator1";
            this.separator1.Size = new System.Drawing.Size(6, 25);
            // 
            // btnPrint
            // 
            this.btnPrint.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnPrint.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuPrint,
            this.separator3,
            this.mnuPageOptions,
            this.mnuPrintPreview,
            this.mnuPrintDialog});
            this.btnPrint.Image = ((System.Drawing.Image)(resources.GetObject("btnPrint.Image")));
            this.btnPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(32, 22);
            this.btnPrint.Text = "&Print";
            this.btnPrint.ButtonClick += new System.EventHandler(this.printToolStripButton_ButtonClick);
            // 
            // mnuPrint
            // 
            this.mnuPrint.Image = ((System.Drawing.Image)(resources.GetObject("mnuPrint.Image")));
            this.mnuPrint.Name = "mnuPrint";
            this.mnuPrint.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
            this.mnuPrint.Size = new System.Drawing.Size(152, 22);
            this.mnuPrint.Text = "Print";
            this.mnuPrint.Click += new System.EventHandler(this.printToolStripMenuItem_Click);
            // 
            // separator3
            // 
            this.separator3.Name = "separator3";
            this.separator3.Size = new System.Drawing.Size(149, 6);
            // 
            // mnuPageOptions
            // 
            this.mnuPageOptions.Image = ((System.Drawing.Image)(resources.GetObject("mnuPageOptions.Image")));
            this.mnuPageOptions.Name = "mnuPageOptions";
            this.mnuPageOptions.Size = new System.Drawing.Size(152, 22);
            this.mnuPageOptions.Text = "Page options...";
            this.mnuPageOptions.Click += new System.EventHandler(this.pageOptionsToolStripMenuItem_Click);
            // 
            // mnuPrintPreview
            // 
            this.mnuPrintPreview.Image = ((System.Drawing.Image)(resources.GetObject("mnuPrintPreview.Image")));
            this.mnuPrintPreview.Name = "mnuPrintPreview";
            this.mnuPrintPreview.Size = new System.Drawing.Size(152, 22);
            this.mnuPrintPreview.Text = "Print preview...";
            this.mnuPrintPreview.Click += new System.EventHandler(this.printPreviewToolStripMenuItem_Click);
            // 
            // mnuPrintDialog
            // 
            this.mnuPrintDialog.Image = ((System.Drawing.Image)(resources.GetObject("mnuPrintDialog.Image")));
            this.mnuPrintDialog.Name = "mnuPrintDialog";
            this.mnuPrintDialog.Size = new System.Drawing.Size(152, 22);
            this.mnuPrintDialog.Text = "Print dialog...";
            this.mnuPrintDialog.Click += new System.EventHandler(this.printDialogToolStripMenuItem_Click);
            // 
            // separator4
            // 
            this.separator4.Name = "separator4";
            this.separator4.Size = new System.Drawing.Size(6, 25);
            // 
            // btnCut
            // 
            this.btnCut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnCut.Image = ((System.Drawing.Image)(resources.GetObject("btnCut.Image")));
            this.btnCut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCut.Name = "btnCut";
            this.btnCut.Size = new System.Drawing.Size(23, 22);
            this.btnCut.Text = "C&ut";
            this.btnCut.Click += new System.EventHandler(this.cutToolStripButton_Click);
            // 
            // btnCopy
            // 
            this.btnCopy.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnCopy.Image = ((System.Drawing.Image)(resources.GetObject("btnCopy.Image")));
            this.btnCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(23, 22);
            this.btnCopy.Text = "&Copy";
            this.btnCopy.Click += new System.EventHandler(this.copyToolStripButton_Click);
            // 
            // btnPaste
            // 
            this.btnPaste.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnPaste.Image = ((System.Drawing.Image)(resources.GetObject("btnPaste.Image")));
            this.btnPaste.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPaste.Name = "btnPaste";
            this.btnPaste.Size = new System.Drawing.Size(23, 22);
            this.btnPaste.Text = "&Paste";
            this.btnPaste.Click += new System.EventHandler(this.pasteToolStripButton_Click);
            // 
            // separator5
            // 
            this.separator5.Name = "separator5";
            this.separator5.Size = new System.Drawing.Size(6, 25);
            // 
            // btnUndo
            // 
            this.btnUndo.Image = ((System.Drawing.Image)(resources.GetObject("btnUndo.Image")));
            this.btnUndo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnUndo.Name = "btnUndo";
            this.btnUndo.Size = new System.Drawing.Size(68, 22);
            this.btnUndo.Text = "Un&do";
            this.btnUndo.ButtonClick += new System.EventHandler(this.toolStripButton1_Click);
            this.btnUndo.DropDownOpening += new System.EventHandler(this.btnUndo_DropDownOpening);
            // 
            // btnRedo
            // 
            this.btnRedo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnRedo.Image = ((System.Drawing.Image)(resources.GetObject("btnRedo.Image")));
            this.btnRedo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRedo.Name = "btnRedo";
            this.btnRedo.Size = new System.Drawing.Size(32, 22);
            this.btnRedo.Text = "&Redo";
            this.btnRedo.ButtonClick += new System.EventHandler(this.toolStripSplitButton1_ButtonClick);
            this.btnRedo.DropDownOpening += new System.EventHandler(this.toolStripSplitButton1_DropDownOpening);
            // 
            // separator6
            // 
            this.separator6.Name = "separator6";
            this.separator6.Size = new System.Drawing.Size(6, 25);
            // 
            // btnHelp
            // 
            this.btnHelp.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.btnHelp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnHelp.Image = ((System.Drawing.Image)(resources.GetObject("btnHelp.Image")));
            this.btnHelp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(23, 22);
            this.btnHelp.Text = "He&lp";
            this.btnHelp.Click += new System.EventHandler(this.helpToolStripButton_Click);
            // 
            // btnInsertTime
            // 
            this.btnInsertTime.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnInsertTime.Image = ((System.Drawing.Image)(resources.GetObject("btnInsertTime.Image")));
            this.btnInsertTime.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnInsertTime.Name = "btnInsertTime";
            this.btnInsertTime.Size = new System.Drawing.Size(23, 22);
            this.btnInsertTime.Text = "Insert &Date/Time";
            this.btnInsertTime.Click += new System.EventHandler(this.btnInsertTime_Click);
            // 
            // separator7
            // 
            this.separator7.Name = "separator7";
            this.separator7.Size = new System.Drawing.Size(6, 25);
            // 
            // btnSearch
            // 
            this.btnSearch.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSearch.Image = ((System.Drawing.Image)(resources.GetObject("btnSearch.Image")));
            this.btnSearch.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(23, 22);
            this.btnSearch.Text = "Toggle Searchbar (Ctrl+F)";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // toolStripButton4
            // 
            this.toolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton4.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton4.Image")));
            this.toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton4.Name = "toolStripButton4";
            this.toolStripButton4.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton4.Text = "Search & Replace";
            this.toolStripButton4.Click += new System.EventHandler(this.toolStripButton4_Click);
            // 
            // separator9
            // 
            this.separator9.Name = "separator9";
            this.separator9.Size = new System.Drawing.Size(6, 25);
            // 
            // btnOptions
            // 
            this.btnOptions.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFont,
            this.backgroundColorToolStripMenuItem,
            this.separator10,
            this.mnuWordWrap,
            this.mnuStatusBar,
            this.toolStripMenuItem1,
            this.registerFileAssociationsToolStripMenuItem});
            this.btnOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnOptions.Image")));
            this.btnOptions.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnOptions.Name = "btnOptions";
            this.btnOptions.Size = new System.Drawing.Size(78, 22);
            this.btnOptions.Text = "Op&tions";
            // 
            // mnuFont
            // 
            this.mnuFont.Image = ((System.Drawing.Image)(resources.GetObject("mnuFont.Image")));
            this.mnuFont.Name = "mnuFont";
            this.mnuFont.Size = new System.Drawing.Size(206, 22);
            this.mnuFont.Text = "Font...";
            this.mnuFont.Click += new System.EventHandler(this.fontToolStripMenuItem_Click);
            // 
            // backgroundColorToolStripMenuItem
            // 
            this.backgroundColorToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("backgroundColorToolStripMenuItem.Image")));
            this.backgroundColorToolStripMenuItem.Name = "backgroundColorToolStripMenuItem";
            this.backgroundColorToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.backgroundColorToolStripMenuItem.Text = "Background Color...";
            this.backgroundColorToolStripMenuItem.Click += new System.EventHandler(this.backgroundColorToolStripMenuItem_Click);
            // 
            // separator10
            // 
            this.separator10.Name = "separator10";
            this.separator10.Size = new System.Drawing.Size(203, 6);
            // 
            // mnuWordWrap
            // 
            this.mnuWordWrap.Name = "mnuWordWrap";
            this.mnuWordWrap.Size = new System.Drawing.Size(206, 22);
            this.mnuWordWrap.Text = "Word Wrap";
            this.mnuWordWrap.Click += new System.EventHandler(this.wordWrapToolStripMenuItem_Click);
            // 
            // mnuStatusBar
            // 
            this.mnuStatusBar.Name = "mnuStatusBar";
            this.mnuStatusBar.Size = new System.Drawing.Size(206, 22);
            this.mnuStatusBar.Text = "Statusbar";
            this.mnuStatusBar.Click += new System.EventHandler(this.statusbarToolStripMenuItem_Click);
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.DefaultExt = "txt";
            this.saveFileDialog1.Filter = "Text File (*.txt)|*.txt|Text File with UNIX line endings (*.txt)|*.txt|Unicode Te" +
    "xt File (UTF-8) (*.txt)|*.txt|All Files (*.*)|*.*";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.DefaultExt = "txt";
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            // 
            // fontDialog1
            // 
            this.fontDialog1.FontMustExist = true;
            this.fontDialog1.ShowApply = true;
            this.fontDialog1.ShowColor = true;
            this.fontDialog1.Apply += new System.EventHandler(this.fontDialog1_Apply);
            // 
            // printDialog1
            // 
            this.printDialog1.UseEXDialog = true;
            // 
            // printPreviewDialog1
            // 
            this.printPreviewDialog1.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.ClientSize = new System.Drawing.Size(400, 300);
            this.printPreviewDialog1.Enabled = true;
            this.printPreviewDialog1.Icon = ((System.Drawing.Icon)(resources.GetObject("printPreviewDialog1.Icon")));
            this.printPreviewDialog1.Name = "printPreviewDialog1";
            this.printPreviewDialog1.UseAntiAlias = true;
            this.printPreviewDialog1.Visible = false;
            // 
            // printDocument1
            // 
            this.printDocument1.BeginPrint += new System.Drawing.Printing.PrintEventHandler(this.printDocument1_BeginPrint);
            this.printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument1_PrintPage);
            // 
            // statusBar
            // 
            this.statusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblSpacer,
            this.lblPosition});
            this.statusBar.Location = new System.Drawing.Point(0, 542);
            this.statusBar.Name = "statusBar";
            this.statusBar.Size = new System.Drawing.Size(788, 24);
            this.statusBar.TabIndex = 2;
            this.statusBar.Text = "statusStrip1";
            // 
            // lblSpacer
            // 
            this.lblSpacer.Name = "lblSpacer";
            this.lblSpacer.Size = new System.Drawing.Size(704, 19);
            this.lblSpacer.Spring = true;
            // 
            // lblPosition
            // 
            this.lblPosition.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.lblPosition.Name = "lblPosition";
            this.lblPosition.Size = new System.Drawing.Size(69, 19);
            this.lblPosition.Text = "Ln. 3 Col. 5";
            // 
            // searchBar
            // 
            this.searchBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.searchBar.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.searchBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.toolStripLabel1,
            this.searchBox,
            this.toolStripButton2,
            this.toolStripButton3,
            this.toolStripButton5,
            this.searchEndLabel});
            this.searchBar.Location = new System.Drawing.Point(0, 517);
            this.searchBar.Name = "searchBar";
            this.searchBar.Padding = new System.Windows.Forms.Padding(4, 0, 1, 0);
            this.searchBar.Size = new System.Drawing.Size(788, 25);
            this.searchBar.TabIndex = 4;
            this.searchBar.Text = "Searchbar";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton1.Text = "Close Search Bar";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click_1);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(45, 22);
            this.toolStripLabel1.Text = "Search:";
            // 
            // searchBox
            // 
            this.searchBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.searchBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.searchBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.searchBox.Name = "searchBox";
            this.searchBox.Size = new System.Drawing.Size(100, 25);
            this.searchBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.searchBox_KeyDown);
            this.searchBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.searchBox_KeyPress);
            this.searchBox.Click += new System.EventHandler(this.searchBox_Click);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton2.Text = "toolStripButton2";
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton3.Image")));
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(112, 22);
            this.toolStripButton3.Text = "Search Next (F3)";
            this.toolStripButton3.Click += new System.EventHandler(this.toolStripButton3_Click);
            // 
            // toolStripButton5
            // 
            this.toolStripButton5.Checked = true;
            this.toolStripButton5.CheckState = System.Windows.Forms.CheckState.Checked;
            this.toolStripButton5.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton5.Image")));
            this.toolStripButton5.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton5.Name = "toolStripButton5";
            this.toolStripButton5.Size = new System.Drawing.Size(89, 22);
            this.toolStripButton5.Text = "Ignore Case";
            this.toolStripButton5.Click += new System.EventHandler(this.toolStripButton5_Click);
            // 
            // searchEndLabel
            // 
            this.searchEndLabel.Image = ((System.Drawing.Image)(resources.GetObject("searchEndLabel.Image")));
            this.searchEndLabel.Margin = new System.Windows.Forms.Padding(8, 1, 0, 2);
            this.searchEndLabel.Name = "searchEndLabel";
            this.searchEndLabel.Size = new System.Drawing.Size(241, 22);
            this.searchEndLabel.Text = "Search reached the end of the document.";
            this.searchEndLabel.Visible = false;
            // 
            // editBox
            // 
            this.editBox.AcceptsReturn = true;
            this.editBox.AcceptsTab = true;
            this.editBox.AllowDrop = true;
            this.editBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.editBox.HideSelection = false;
            this.editBox.Location = new System.Drawing.Point(0, 25);
            this.editBox.Multiline = true;
            this.editBox.Name = "editBox";
            this.editBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.editBox.Size = new System.Drawing.Size(788, 492);
            this.editBox.TabIndex = 5;
            this.editBox.WordWrap = false;
            this.editBox.Click += new System.EventHandler(this.editBox_Click_1);
            this.editBox.DragDrop += new System.Windows.Forms.DragEventHandler(this.editBox_DragDrop);
            this.editBox.DragEnter += new System.Windows.Forms.DragEventHandler(this.editBox_DragEnter);
            this.editBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.editBox_KeyDown);
            this.editBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.editBox_KeyPress);
            this.editBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.editBox_KeyUp);
            // 
            // colorDialog1
            // 
            this.colorDialog1.AnyColor = true;
            this.colorDialog1.Color = System.Drawing.Color.White;
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(203, 6);
            // 
            // registerFileAssociationsToolStripMenuItem
            // 
            this.registerFileAssociationsToolStripMenuItem.Name = "registerFileAssociationsToolStripMenuItem";
            this.registerFileAssociationsToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.registerFileAssociationsToolStripMenuItem.Text = "Register File Associations";
            this.registerFileAssociationsToolStripMenuItem.Click += new System.EventHandler(this.registerFileAssociationsToolStripMenuItem_Click);
            // 
            // TextEditorWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(788, 566);
            this.Controls.Add(this.editBox);
            this.Controls.Add(this.searchBar);
            this.Controls.Add(this.statusBar);
            this.Controls.Add(this.toolBar);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "TextEditorWindow";
            this.Text = "(untitled) - SimpleTextExitor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TextEditorWindow_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.TextEditorWindow_FormClosed);
            this.Load += new System.EventHandler(this.TextEditorWindow_Load);
            this.Shown += new System.EventHandler(this.TextEditorWindow_Shown);
            this.Resize += new System.EventHandler(this.TextEditorWindow_Resize);
            this.toolBar.ResumeLayout(false);
            this.toolBar.PerformLayout();
            this.statusBar.ResumeLayout(false);
            this.statusBar.PerformLayout();
            this.searchBar.ResumeLayout(false);
            this.searchBar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolBar;
        private System.Windows.Forms.ToolStripButton btnNew;
        private System.Windows.Forms.ToolStripSplitButton btnSave;
        private System.Windows.Forms.ToolStripMenuItem mnuSave;
        private System.Windows.Forms.ToolStripMenuItem mnuSaveAs;
        private System.Windows.Forms.ToolStripSeparator separator4;
        private System.Windows.Forms.ToolStripButton btnCut;
        private System.Windows.Forms.ToolStripButton btnCopy;
        private System.Windows.Forms.ToolStripButton btnPaste;
        private System.Windows.Forms.ToolStripSeparator separator5;
        private System.Windows.Forms.ToolStripButton btnHelp;
        private System.Windows.Forms.ToolStripSeparator separator1;
        private System.Windows.Forms.ToolStripSeparator separator6;
        private System.Windows.Forms.ToolStripButton btnInsertTime;
        private System.Windows.Forms.ToolStripSeparator separator9;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.FontDialog fontDialog1;
        private System.Windows.Forms.ToolStripSplitButton btnPrint;
        private System.Windows.Forms.ToolStripMenuItem mnuPrintDialog;
        private System.Windows.Forms.ToolStripMenuItem mnuPageOptions;
        private System.Windows.Forms.ToolStripMenuItem mnuPrintPreview;
        private System.Windows.Forms.PageSetupDialog pageSetupDialog1;
        private System.Windows.Forms.PrintDialog printDialog1;
        private System.Windows.Forms.PrintPreviewDialog printPreviewDialog1;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.ToolStripDropDownButton btnOptions;
        private System.Windows.Forms.StatusStrip statusBar;
        private System.Windows.Forms.ToolStripStatusLabel lblSpacer;
        private System.Windows.Forms.ToolStripStatusLabel lblPosition;
        private System.Windows.Forms.ToolStripSplitButton btnOpen;
        private System.Windows.Forms.ToolStripMenuItem mnuOpen;
        private System.Windows.Forms.ToolStripSeparator separator2;
        private System.Windows.Forms.ToolStripMenuItem mnuRecent;
        private System.Windows.Forms.ToolStripMenuItem mnuPrint;
        private System.Windows.Forms.ToolStripSeparator separator3;
        private System.Windows.Forms.ToolStripSeparator separator7;
        private System.Windows.Forms.ToolStripSplitButton btnUndo;
        private System.Windows.Forms.ToolStripMenuItem mnuFont;
        private System.Windows.Forms.ToolStripSeparator separator10;
        private System.Windows.Forms.ToolStripMenuItem mnuWordWrap;
        private System.Windows.Forms.ToolStripMenuItem mnuStatusBar;
        private System.Windows.Forms.ToolStripSplitButton btnRedo;
        private System.Windows.Forms.ToolStrip searchBar;
        private System.Windows.Forms.TextBox editBox;
        private System.Windows.Forms.ToolStripTextBox searchBox;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStripButton toolStripButton3;
        private System.Windows.Forms.ToolStripLabel searchEndLabel;
        private System.Windows.Forms.ToolStripButton btnSearch;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.ToolStripMenuItem backgroundColorToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton toolStripButton4;
        private System.Windows.Forms.ToolStripButton toolStripButton5;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem registerFileAssociationsToolStripMenuItem;
    }
}

