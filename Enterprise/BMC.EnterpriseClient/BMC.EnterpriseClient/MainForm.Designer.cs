namespace BMC.EnterpriseClient
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.sbarMain = new System.Windows.Forms.StatusStrip();
            this.sbrItemUserDetails = new System.Windows.Forms.ToolStripStatusLabel();
            this.sbrItemSpring = new System.Windows.Forms.ToolStripStatusLabel();
            this.sbr_Notitfications = new System.Windows.Forms.ToolStripStatusLabel();
            this.sbrCaps = new System.Windows.Forms.ToolStripStatusLabel();
            this.sbrNum = new System.Windows.Forms.ToolStripStatusLabel();
            this.sbrDateTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.sbrItemServices = new System.Windows.Forms.ToolStripDropDownButton();
            this.sbrItemDatabase = new System.Windows.Forms.ToolStripStatusLabel();
            this.sbrItemNetwork = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel4 = new System.Windows.Forms.ToolStripStatusLabel();
            this.mbarMain = new System.Windows.Forms.MenuStrip();
            this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileCloseAll = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuFileExit = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuView = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuViewToolbar = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuViewStatusbar = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHelpAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.uxActiveItems = new BMC.CoreLib.Win32.UxDockPanel();
            this.dgvActiveItems = new System.Windows.Forms.DataGridView();
            this.chdrActiveClose = new System.Windows.Forms.DataGridViewButtonColumn();
            this.chdrActiveImage = new System.Windows.Forms.DataGridViewImageColumn();
            this.chdrActiveItem = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.tbrChildServiceCalls = new System.Windows.Forms.ToolStrip();
            this.tbrItemCreateServiceCall = new System.Windows.Forms.ToolStripButton();
            this.tbrItemCurrentServiceCalls = new System.Windows.Forms.ToolStripButton();
            this.tbrItemClosedServiceCalls = new System.Windows.Forms.ToolStripButton();
            this.tbrItemServiceAdmin = new System.Windows.Forms.ToolStripButton();
            this.tbrItemGMUEvents = new System.Windows.Forms.ToolStripButton();
            this.tbrItemEventViewer = new System.Windows.Forms.ToolStripButton();
            this.tbrItemAlerts = new System.Windows.Forms.ToolStripButton();
            this.tbrItemShowAlerts = new System.Windows.Forms.ToolStripButton();
            this.tbrChildVault = new System.Windows.Forms.ToolStrip();
            this.tbrItemAddVault = new System.Windows.Forms.ToolStripButton();
            this.tbrItemVaultDeclaration = new System.Windows.Forms.ToolStripButton();
            this.tbrItemVaultAudit = new System.Windows.Forms.ToolStripButton();
            this.tbrChildFinancial = new System.Windows.Forms.ToolStrip();
            this.tbrItemShareHolders = new System.Windows.Forms.ToolStripButton();
            this.tbrItemProfitShareGroup = new System.Windows.Forms.ToolStripButton();
            this.tbrItemExpenseShareGroup = new System.Windows.Forms.ToolStripButton();
            this.tbrItemReadLiquidation = new System.Windows.Forms.ToolStripButton();
            this.tsbCollectionLiquidation = new System.Windows.Forms.ToolStripButton();
            this.tbrChildMonitoring = new System.Windows.Forms.ToolStrip();
            this.tbrItemSystemAudit = new System.Windows.Forms.ToolStripButton();
            this.tbrItemSystemMonitoring = new System.Windows.Forms.ToolStripButton();
            this.tbrItemDataCommsAudit = new System.Windows.Forms.ToolStripButton();
            this.tbrChildAdmin = new System.Windows.Forms.ToolStrip();
            this.tbrItemOrganisation = new System.Windows.Forms.ToolStripButton();
            this.tbrItemUserAdmin = new System.Windows.Forms.ToolStripButton();
            this.tbrItemSettings = new System.Windows.Forms.ToolStripButton();
            this.tbrItemDepot = new System.Windows.Forms.ToolStripButton();
            this.tbrItemOperators = new System.Windows.Forms.ToolStripButton();
            this.tbrItemCalendars = new System.Windows.Forms.ToolStripButton();
            this.tbrItemOpenHours = new System.Windows.Forms.ToolStripButton();
            this.tbrItemSiteSettings = new System.Windows.Forms.ToolStripButton();
            this.tbrItemDeclaration = new System.Windows.Forms.ToolStripButton();
            this.tbrItemStacker = new System.Windows.Forms.ToolStripButton();
            this.tbrItemDropSchedule = new System.Windows.Forms.ToolStripButton();
            this.tbrItemSiteLicensing = new System.Windows.Forms.ToolStripButton();
            this.tsbAGSCombination = new System.Windows.Forms.ToolStripButton();
            this.tbrItemEmployee = new System.Windows.Forms.ToolStripButton();
            this.tbrNoLogin = new System.Windows.Forms.ToolStrip();
            this.tbrItemLogin = new System.Windows.Forms.ToolStripButton();
            this.tbrMain = new System.Windows.Forms.ToolStrip();
            this.tbrItemAdmin = new System.Windows.Forms.ToolStripButton();
            this.tbrItemViewSites = new System.Windows.Forms.ToolStripButton();
            this.tbrItemServiceCalls = new System.Windows.Forms.ToolStripButton();
            this.tbrItemAssets = new System.Windows.Forms.ToolStripButton();
            this.tbrItemGameLibrary = new System.Windows.Forms.ToolStripButton();
            this.tbrItemFinancial = new System.Windows.Forms.ToolStripButton();
            this.tbrItemReports = new System.Windows.Forms.ToolStripButton();
            this.tbrItemDataSheet = new System.Windows.Forms.ToolStripButton();
            this.tbrItemAnalysis = new System.Windows.Forms.ToolStripButton();
            this.tbrItemMeterAdjustment = new System.Windows.Forms.ToolStripButton();
            this.tbrItemVault = new System.Windows.Forms.ToolStripButton();
            this.tbrItemSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.tbrItemMonitoring = new System.Windows.Forms.ToolStripButton();
            this.tbrItemChangePwd = new System.Windows.Forms.ToolStripButton();
            this.tbrItemLogout = new System.Windows.Forms.ToolStripButton();
            this.tbrChildFinancialSGVI = new System.Windows.Forms.ToolStrip();
            this.tbrItemTerms = new System.Windows.Forms.ToolStripButton();
            this.tbrItemShares = new System.Windows.Forms.ToolStripButton();
            this.tbrItemTermsSummary = new System.Windows.Forms.ToolStripButton();
            this.tbrItemPeriodEnd = new System.Windows.Forms.ToolStripButton();
            this.sbarMain.SuspendLayout();
            this.mbarMain.SuspendLayout();
            this.uxActiveItems.ChildContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvActiveItems)).BeginInit();
            this.tbrChildServiceCalls.SuspendLayout();
            this.tbrChildVault.SuspendLayout();
            this.tbrChildFinancial.SuspendLayout();
            this.tbrChildMonitoring.SuspendLayout();
            this.tbrChildAdmin.SuspendLayout();
            this.tbrNoLogin.SuspendLayout();
            this.tbrMain.SuspendLayout();
            this.tbrChildFinancialSGVI.SuspendLayout();
            this.SuspendLayout();
            // 
            // sbarMain
            // 
            this.sbarMain.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sbarMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sbrItemUserDetails,
            this.sbrItemSpring,
            this.sbr_Notitfications,
            this.sbrCaps,
            this.sbrNum,
            this.sbrDateTime,
            this.sbrItemServices,
            this.sbrItemDatabase,
            this.sbrItemNetwork});
            this.sbarMain.Location = new System.Drawing.Point(0, 548);
            this.sbarMain.Name = "sbarMain";
            this.sbarMain.Size = new System.Drawing.Size(996, 25);
            this.sbarMain.TabIndex = 4;
            this.sbarMain.Text = "statusStrip1";
            // 
            // sbrItemUserDetails
            // 
            this.sbrItemUserDetails.Image = global::BMC.EnterpriseClient.Properties.Resources.User;
            this.sbrItemUserDetails.ImageTransparentColor = System.Drawing.Color.Black;
            this.sbrItemUserDetails.Name = "sbrItemUserDetails";
            this.sbrItemUserDetails.Padding = new System.Windows.Forms.Padding(2);
            this.sbrItemUserDetails.Size = new System.Drawing.Size(80, 20);
            this.sbrItemUserDetails.Text = "User Name";
            // 
            // sbrItemSpring
            // 
            this.sbrItemSpring.Name = "sbrItemSpring";
            this.sbrItemSpring.Size = new System.Drawing.Size(686, 20);
            this.sbrItemSpring.Spring = true;
            // 
            // sbr_Notitfications
            // 
            this.sbr_Notitfications.AutoToolTip = true;
            this.sbr_Notitfications.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.sbr_Notitfications.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.sbr_Notitfications.Image = global::BMC.EnterpriseClient.Properties.Resources.Notification;
            this.sbr_Notitfications.ImageTransparentColor = System.Drawing.Color.White;
            this.sbr_Notitfications.Name = "sbr_Notitfications";
            this.sbr_Notitfications.Size = new System.Drawing.Size(20, 20);
            this.sbr_Notitfications.ToolTipText = "Notifications";
            this.sbr_Notitfications.Click += new System.EventHandler(this.sbr_Notitfications_Click);
            // 
            // sbrCaps
            // 
            this.sbrCaps.Name = "sbrCaps";
            this.sbrCaps.Size = new System.Drawing.Size(35, 20);
            this.sbrCaps.Text = "CAPS";
            // 
            // sbrNum
            // 
            this.sbrNum.Name = "sbrNum";
            this.sbrNum.Size = new System.Drawing.Size(32, 20);
            this.sbrNum.Text = "NUM";
            // 
            // sbrDateTime
            // 
            this.sbrDateTime.Name = "sbrDateTime";
            this.sbrDateTime.Size = new System.Drawing.Size(53, 20);
            this.sbrDateTime.Text = "DateTime";
            // 
            // sbrItemServices
            // 
            this.sbrItemServices.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.sbrItemServices.Image = global::BMC.EnterpriseClient.Properties.Resources.ICO_SRV_OK;
            this.sbrItemServices.ImageTransparentColor = System.Drawing.Color.Black;
            this.sbrItemServices.Name = "sbrItemServices";
            this.sbrItemServices.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.sbrItemServices.Size = new System.Drawing.Size(39, 23);
            this.sbrItemServices.Text = "toolStripDropDownButton1";
            // 
            // sbrItemDatabase
            // 
            this.sbrItemDatabase.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.sbrItemDatabase.ForeColor = System.Drawing.Color.Transparent;
            this.sbrItemDatabase.ImageTransparentColor = System.Drawing.Color.Black;
            this.sbrItemDatabase.Name = "sbrItemDatabase";
            this.sbrItemDatabase.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.sbrItemDatabase.Size = new System.Drawing.Size(10, 20);
            this.sbrItemDatabase.Text = "toolStripDropDownButton1";
            // 
            // sbrItemNetwork
            // 
            this.sbrItemNetwork.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.sbrItemNetwork.Image = global::BMC.EnterpriseClient.Properties.Resources.ICO_NET_OK;
            this.sbrItemNetwork.ImageTransparentColor = System.Drawing.Color.Black;
            this.sbrItemNetwork.Name = "sbrItemNetwork";
            this.sbrItemNetwork.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.sbrItemNetwork.Size = new System.Drawing.Size(26, 20);
            this.sbrItemNetwork.Text = "toolStripDropDownButton1";
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(109, 20);
            this.toolStripStatusLabel3.Text = "toolStripStatusLabel3";
            // 
            // toolStripStatusLabel4
            // 
            this.toolStripStatusLabel4.Name = "toolStripStatusLabel4";
            this.toolStripStatusLabel4.Size = new System.Drawing.Size(109, 20);
            this.toolStripStatusLabel4.Text = "toolStripStatusLabel4";
            // 
            // mbarMain
            // 
            this.mbarMain.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.mbarMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile,
            this.mnuView,
            this.helpToolStripMenuItem});
            this.mbarMain.Location = new System.Drawing.Point(0, 0);
            this.mbarMain.Name = "mbarMain";
            this.mbarMain.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
            this.mbarMain.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.mbarMain.Size = new System.Drawing.Size(996, 24);
            this.mbarMain.TabIndex = 0;
            this.mbarMain.Text = "menuStrip1";
            // 
            // mnuFile
            // 
            this.mnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFileCloseAll,
            this.mnuFileSep1,
            this.mnuFileExit});
            this.mnuFile.Name = "mnuFile";
            this.mnuFile.Size = new System.Drawing.Size(35, 20);
            this.mnuFile.Text = "&File";
            // 
            // mnuFileCloseAll
            // 
            this.mnuFileCloseAll.Image = ((System.Drawing.Image)(resources.GetObject("mnuFileCloseAll.Image")));
            this.mnuFileCloseAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuFileCloseAll.Name = "mnuFileCloseAll";
            this.mnuFileCloseAll.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.L)));
            this.mnuFileCloseAll.Size = new System.Drawing.Size(175, 22);
            this.mnuFileCloseAll.Text = "Close &All";
            this.mnuFileCloseAll.Click += new System.EventHandler(this.mnuFileCloseAll_Click);
            // 
            // mnuFileSep1
            // 
            this.mnuFileSep1.Name = "mnuFileSep1";
            this.mnuFileSep1.Size = new System.Drawing.Size(172, 6);
            // 
            // mnuFileExit
            // 
            this.mnuFileExit.Name = "mnuFileExit";
            this.mnuFileExit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.mnuFileExit.Size = new System.Drawing.Size(175, 22);
            this.mnuFileExit.Text = "E&xit";
            this.mnuFileExit.Click += new System.EventHandler(this.mnuFileExit_Click);
            // 
            // mnuView
            // 
            this.mnuView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuViewToolbar,
            this.mnuViewStatusbar});
            this.mnuView.Name = "mnuView";
            this.mnuView.Size = new System.Drawing.Size(42, 20);
            this.mnuView.Text = "&View";
            // 
            // mnuViewToolbar
            // 
            this.mnuViewToolbar.CheckOnClick = true;
            this.mnuViewToolbar.Name = "mnuViewToolbar";
            this.mnuViewToolbar.Size = new System.Drawing.Size(119, 22);
            this.mnuViewToolbar.Text = "&Toolbar";
            this.mnuViewToolbar.CheckedChanged += new System.EventHandler(this.mnuViewToolbar_CheckedChanged);
            // 
            // mnuViewStatusbar
            // 
            this.mnuViewStatusbar.CheckOnClick = true;
            this.mnuViewStatusbar.Name = "mnuViewStatusbar";
            this.mnuViewStatusbar.Size = new System.Drawing.Size(119, 22);
            this.mnuViewStatusbar.Text = "&Statusbar";
            this.mnuViewStatusbar.CheckedChanged += new System.EventHandler(this.mnuViewStatusbar_CheckedChanged);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuHelpAbout});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(41, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // mnuHelpAbout
            // 
            this.mnuHelpAbout.Name = "mnuHelpAbout";
            this.mnuHelpAbout.Size = new System.Drawing.Size(111, 22);
            this.mnuHelpAbout.Text = "&About...";
            this.mnuHelpAbout.Click += new System.EventHandler(this.mnuHelpAbout_Click);
            // 
            // uxActiveItems
            // 
            this.uxActiveItems.ActualWidth = 250;
            // 
            // uxActiveItems.ChildContainer
            // 
            this.uxActiveItems.ChildContainer.Controls.Add(this.dgvActiveItems);
            this.uxActiveItems.ChildContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uxActiveItems.ChildContainer.Location = new System.Drawing.Point(3, 3);
            this.uxActiveItems.ChildContainer.Margin = new System.Windows.Forms.Padding(0);
            this.uxActiveItems.ChildContainer.Name = "ChildContainer";
            this.uxActiveItems.ChildContainer.Padding = new System.Windows.Forms.Padding(3);
            this.uxActiveItems.ChildContainer.Size = new System.Drawing.Size(215, 475);
            this.uxActiveItems.ChildContainer.TabIndex = 0;
            this.uxActiveItems.Dock = System.Windows.Forms.DockStyle.Left;
            this.uxActiveItems.HeaderText = "Active Items";
            this.uxActiveItems.IsHidden = true;
            this.uxActiveItems.Location = new System.Drawing.Point(0, 210);
            this.uxActiveItems.Name = "uxActiveItems";
            this.uxActiveItems.OwnerForm = this;
            this.uxActiveItems.Size = new System.Drawing.Size(31, 338);
            this.uxActiveItems.TabIndex = 3;
            // 
            // dgvActiveItems
            // 
            this.dgvActiveItems.AllowUserToAddRows = false;
            this.dgvActiveItems.AllowUserToDeleteRows = false;
            this.dgvActiveItems.AllowUserToOrderColumns = true;
            this.dgvActiveItems.AllowUserToResizeColumns = false;
            this.dgvActiveItems.AllowUserToResizeRows = false;
            this.dgvActiveItems.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.dgvActiveItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvActiveItems.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.chdrActiveClose,
            this.chdrActiveImage,
            this.chdrActiveItem});
            this.dgvActiveItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvActiveItems.Location = new System.Drawing.Point(3, 3);
            this.dgvActiveItems.Name = "dgvActiveItems";
            this.dgvActiveItems.RowHeadersVisible = false;
            this.dgvActiveItems.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvActiveItems.Size = new System.Drawing.Size(209, 469);
            this.dgvActiveItems.TabIndex = 1;
            this.dgvActiveItems.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvActiveItems_CellClick);
            // 
            // chdrActiveClose
            // 
            this.chdrActiveClose.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.chdrActiveClose.Frozen = true;
            this.chdrActiveClose.HeaderText = "";
            this.chdrActiveClose.Name = "chdrActiveClose";
            this.chdrActiveClose.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.chdrActiveClose.Text = "X";
            this.chdrActiveClose.Width = 32;
            // 
            // chdrActiveImage
            // 
            this.chdrActiveImage.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.chdrActiveImage.Frozen = true;
            this.chdrActiveImage.HeaderText = "";
            this.chdrActiveImage.Name = "chdrActiveImage";
            this.chdrActiveImage.ReadOnly = true;
            this.chdrActiveImage.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.chdrActiveImage.Width = 32;
            // 
            // chdrActiveItem
            // 
            this.chdrActiveItem.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.chdrActiveItem.HeaderText = "Items";
            this.chdrActiveItem.Name = "chdrActiveItem";
            this.chdrActiveItem.ReadOnly = true;
            this.chdrActiveItem.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.chdrActiveItem.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // tbrChildServiceCalls
            // 
            this.tbrChildServiceCalls.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.tbrChildServiceCalls.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tbrChildServiceCalls.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tbrItemCreateServiceCall,
            this.tbrItemCurrentServiceCalls,
            this.tbrItemClosedServiceCalls,
            this.tbrItemServiceAdmin,
            this.tbrItemGMUEvents,
            this.tbrItemEventViewer,
            this.tbrItemAlerts,
            this.tbrItemShowAlerts});
            this.tbrChildServiceCalls.Location = new System.Drawing.Point(0, 210);
            this.tbrChildServiceCalls.Name = "tbrChildServiceCalls";
            this.tbrChildServiceCalls.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.tbrChildServiceCalls.Size = new System.Drawing.Size(996, 62);
            this.tbrChildServiceCalls.TabIndex = 14;
            this.tbrChildServiceCalls.Text = "toolStrip1";
            this.tbrChildServiceCalls.Visible = false;
            // 
            // tbrItemCreateServiceCall
            // 
            this.tbrItemCreateServiceCall.Image = ((System.Drawing.Image)(resources.GetObject("tbrItemCreateServiceCall.Image")));
            this.tbrItemCreateServiceCall.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbrItemCreateServiceCall.Name = "tbrItemCreateServiceCall";
            this.tbrItemCreateServiceCall.Padding = new System.Windows.Forms.Padding(5);
            this.tbrItemCreateServiceCall.Size = new System.Drawing.Size(72, 59);
            this.tbrItemCreateServiceCall.Tag = "MAIN_TBAR_CREATECALL";
            this.tbrItemCreateServiceCall.Text = "Create Call";
            this.tbrItemCreateServiceCall.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tbrItemCurrentServiceCalls
            // 
            this.tbrItemCurrentServiceCalls.Image = ((System.Drawing.Image)(resources.GetObject("tbrItemCurrentServiceCalls.Image")));
            this.tbrItemCurrentServiceCalls.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbrItemCurrentServiceCalls.Name = "tbrItemCurrentServiceCalls";
            this.tbrItemCurrentServiceCalls.Padding = new System.Windows.Forms.Padding(5);
            this.tbrItemCurrentServiceCalls.Size = new System.Drawing.Size(80, 59);
            this.tbrItemCurrentServiceCalls.Tag = "MAIN_TBAR_CURRENTCALLS";
            this.tbrItemCurrentServiceCalls.Text = "Current Calls";
            this.tbrItemCurrentServiceCalls.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tbrItemClosedServiceCalls
            // 
            this.tbrItemClosedServiceCalls.Image = ((System.Drawing.Image)(resources.GetObject("tbrItemClosedServiceCalls.Image")));
            this.tbrItemClosedServiceCalls.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbrItemClosedServiceCalls.Name = "tbrItemClosedServiceCalls";
            this.tbrItemClosedServiceCalls.Padding = new System.Windows.Forms.Padding(5);
            this.tbrItemClosedServiceCalls.Size = new System.Drawing.Size(78, 59);
            this.tbrItemClosedServiceCalls.Tag = "MAIN_TBAR_CLOSEDCALLS";
            this.tbrItemClosedServiceCalls.Text = "Closed Calls";
            this.tbrItemClosedServiceCalls.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tbrItemServiceAdmin
            // 
            this.tbrItemServiceAdmin.Image = ((System.Drawing.Image)(resources.GetObject("tbrItemServiceAdmin.Image")));
            this.tbrItemServiceAdmin.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbrItemServiceAdmin.Name = "tbrItemServiceAdmin";
            this.tbrItemServiceAdmin.Padding = new System.Windows.Forms.Padding(5);
            this.tbrItemServiceAdmin.Size = new System.Drawing.Size(89, 59);
            this.tbrItemServiceAdmin.Tag = "MAIN_TBAR_SERVICEADMIN";
            this.tbrItemServiceAdmin.Text = "Service Admin";
            this.tbrItemServiceAdmin.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tbrItemGMUEvents
            // 
            this.tbrItemGMUEvents.Image = ((System.Drawing.Image)(resources.GetObject("tbrItemGMUEvents.Image")));
            this.tbrItemGMUEvents.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbrItemGMUEvents.Name = "tbrItemGMUEvents";
            this.tbrItemGMUEvents.Padding = new System.Windows.Forms.Padding(5);
            this.tbrItemGMUEvents.Size = new System.Drawing.Size(82, 59);
            this.tbrItemGMUEvents.Tag = "MAIN_TBAR_GMUEVENTS";
            this.tbrItemGMUEvents.Text = "GMU Events";
            this.tbrItemGMUEvents.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tbrItemEventViewer
            // 
            this.tbrItemEventViewer.Image = ((System.Drawing.Image)(resources.GetObject("tbrItemEventViewer.Image")));
            this.tbrItemEventViewer.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbrItemEventViewer.Name = "tbrItemEventViewer";
            this.tbrItemEventViewer.Padding = new System.Windows.Forms.Padding(5);
            this.tbrItemEventViewer.Size = new System.Drawing.Size(84, 59);
            this.tbrItemEventViewer.Tag = "MAIN_TBAR_EVENTVIEWER";
            this.tbrItemEventViewer.Text = "Event Viewer";
            this.tbrItemEventViewer.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tbrItemAlerts
            // 
            this.tbrItemAlerts.Image = ((System.Drawing.Image)(resources.GetObject("tbrItemAlerts.Image")));
            this.tbrItemAlerts.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbrItemAlerts.Name = "tbrItemAlerts";
            this.tbrItemAlerts.Size = new System.Drawing.Size(37, 59);
            this.tbrItemAlerts.Text = "Alerts";
            this.tbrItemAlerts.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tbrItemShowAlerts
            // 
            this.tbrItemShowAlerts.Image = ((System.Drawing.Image)(resources.GetObject("tbrItemShowAlerts.Image")));
            this.tbrItemShowAlerts.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbrItemShowAlerts.Name = "tbrItemShowAlerts";
            this.tbrItemShowAlerts.Size = new System.Drawing.Size(67, 59);
            this.tbrItemShowAlerts.Text = "Show Alerts";
            this.tbrItemShowAlerts.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tbrChildVault
            // 
            this.tbrChildVault.CanOverflow = false;
            this.tbrChildVault.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.tbrChildVault.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tbrChildVault.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tbrItemAddVault,
            this.tbrItemVaultDeclaration,
            this.tbrItemVaultAudit});
            this.tbrChildVault.Location = new System.Drawing.Point(0, 210);
            this.tbrChildVault.Name = "tbrChildVault";
            this.tbrChildVault.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.tbrChildVault.Size = new System.Drawing.Size(996, 62);
            this.tbrChildVault.TabIndex = 13;
            this.tbrChildVault.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical90;
            this.tbrChildVault.Visible = false;
            // 
            // tbrItemAddVault
            // 
            this.tbrItemAddVault.AutoSize = false;
            this.tbrItemAddVault.Image = ((System.Drawing.Image)(resources.GetObject("tbrItemAddVault.Image")));
            this.tbrItemAddVault.ImageTransparentColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(254)))), ((int)(((byte)(1)))), ((int)(((byte)(254)))));
            this.tbrItemAddVault.Name = "tbrItemAddVault";
            this.tbrItemAddVault.Size = new System.Drawing.Size(59, 59);
            this.tbrItemAddVault.Tag = "MAIN_TBAR_ADDVAULT";
            this.tbrItemAddVault.Text = "Add Vault";
            this.tbrItemAddVault.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.tbrItemAddVault.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tbrItemVaultDeclaration
            // 
            this.tbrItemVaultDeclaration.AutoSize = false;
            this.tbrItemVaultDeclaration.Image = ((System.Drawing.Image)(resources.GetObject("tbrItemVaultDeclaration.Image")));
            this.tbrItemVaultDeclaration.ImageTransparentColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(254)))), ((int)(((byte)(1)))), ((int)(((byte)(254)))));
            this.tbrItemVaultDeclaration.Name = "tbrItemVaultDeclaration";
            this.tbrItemVaultDeclaration.Size = new System.Drawing.Size(59, 59);
            this.tbrItemVaultDeclaration.Tag = "MAIN_TBAR_VAULT_DECLARATION";
            this.tbrItemVaultDeclaration.Text = "Declaration";
            this.tbrItemVaultDeclaration.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.tbrItemVaultDeclaration.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tbrItemVaultAudit
            // 
            this.tbrItemVaultAudit.AutoSize = false;
            this.tbrItemVaultAudit.Image = ((System.Drawing.Image)(resources.GetObject("tbrItemVaultAudit.Image")));
            this.tbrItemVaultAudit.ImageTransparentColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(254)))), ((int)(((byte)(1)))), ((int)(((byte)(254)))));
            this.tbrItemVaultAudit.Name = "tbrItemVaultAudit";
            this.tbrItemVaultAudit.Size = new System.Drawing.Size(59, 59);
            this.tbrItemVaultAudit.Text = "Audit";
            this.tbrItemVaultAudit.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.tbrItemVaultAudit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tbrChildFinancial
            // 
            this.tbrChildFinancial.CanOverflow = false;
            this.tbrChildFinancial.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.tbrChildFinancial.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tbrChildFinancial.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tbrItemShareHolders,
            this.tbrItemProfitShareGroup,
            this.tbrItemExpenseShareGroup,
            this.tbrItemReadLiquidation,
            this.tsbCollectionLiquidation});
            this.tbrChildFinancial.Location = new System.Drawing.Point(0, 210);
            this.tbrChildFinancial.Name = "tbrChildFinancial";
            this.tbrChildFinancial.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.tbrChildFinancial.Size = new System.Drawing.Size(996, 62);
            this.tbrChildFinancial.TabIndex = 11;
            this.tbrChildFinancial.Text = "toolStrip1";
            this.tbrChildFinancial.Visible = false;
            // 
            // tbrItemShareHolders
            // 
            this.tbrItemShareHolders.Image = ((System.Drawing.Image)(resources.GetObject("tbrItemShareHolders.Image")));
            this.tbrItemShareHolders.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbrItemShareHolders.Name = "tbrItemShareHolders";
            this.tbrItemShareHolders.Padding = new System.Windows.Forms.Padding(5);
            this.tbrItemShareHolders.Size = new System.Drawing.Size(88, 59);
            this.tbrItemShareHolders.Tag = "MAIN_TBAR_SHAREHOLDERS";
            this.tbrItemShareHolders.Text = "Share Holders";
            this.tbrItemShareHolders.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tbrItemShareHolders.Visible = false;
            // 
            // tbrItemProfitShareGroup
            // 
            this.tbrItemProfitShareGroup.Image = ((System.Drawing.Image)(resources.GetObject("tbrItemProfitShareGroup.Image")));
            this.tbrItemProfitShareGroup.ImageTransparentColor = System.Drawing.Color.White;
            this.tbrItemProfitShareGroup.Name = "tbrItemProfitShareGroup";
            this.tbrItemProfitShareGroup.Padding = new System.Windows.Forms.Padding(5);
            this.tbrItemProfitShareGroup.Size = new System.Drawing.Size(113, 59);
            this.tbrItemProfitShareGroup.Tag = "MAIN_TBAR_PROFITSHAREGROUPS";
            this.tbrItemProfitShareGroup.Text = "Profit Share Groups";
            this.tbrItemProfitShareGroup.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tbrItemProfitShareGroup.Visible = false;
            // 
            // tbrItemExpenseShareGroup
            // 
            this.tbrItemExpenseShareGroup.Image = ((System.Drawing.Image)(resources.GetObject("tbrItemExpenseShareGroup.Image")));
            this.tbrItemExpenseShareGroup.ImageTransparentColor = System.Drawing.Color.Black;
            this.tbrItemExpenseShareGroup.Name = "tbrItemExpenseShareGroup";
            this.tbrItemExpenseShareGroup.Padding = new System.Windows.Forms.Padding(5);
            this.tbrItemExpenseShareGroup.Size = new System.Drawing.Size(130, 59);
            this.tbrItemExpenseShareGroup.Tag = "MAIN_TBAR_EXPENSESHAREGROUPS";
            this.tbrItemExpenseShareGroup.Text = "Expense Share Groups";
            this.tbrItemExpenseShareGroup.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tbrItemExpenseShareGroup.Visible = false;
            // 
            // tbrItemReadLiquidation
            // 
            this.tbrItemReadLiquidation.Image = ((System.Drawing.Image)(resources.GetObject("tbrItemReadLiquidation.Image")));
            this.tbrItemReadLiquidation.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbrItemReadLiquidation.Name = "tbrItemReadLiquidation";
            this.tbrItemReadLiquidation.Size = new System.Drawing.Size(124, 59);
            this.tbrItemReadLiquidation.Tag = "MAIN_TBAR_READLIQUIDATION";
            this.tbrItemReadLiquidation.Text = "Read Based Liquidation";
            this.tbrItemReadLiquidation.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tbrItemReadLiquidation.Visible = false;
            // 
            // tsbCollectionLiquidation
            // 
            this.tsbCollectionLiquidation.Image = ((System.Drawing.Image)(resources.GetObject("tsbCollectionLiquidation.Image")));
            this.tsbCollectionLiquidation.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCollectionLiquidation.Name = "tsbCollectionLiquidation";
            this.tsbCollectionLiquidation.Size = new System.Drawing.Size(144, 59);
            this.tsbCollectionLiquidation.Text = "Collection Based Liquidation";
            this.tsbCollectionLiquidation.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbCollectionLiquidation.Visible = false;
            // 
            // tbrChildMonitoring
            // 
            this.tbrChildMonitoring.CanOverflow = false;
            this.tbrChildMonitoring.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.tbrChildMonitoring.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tbrChildMonitoring.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tbrItemSystemAudit,
            this.tbrItemSystemMonitoring,
            this.tbrItemDataCommsAudit});
            this.tbrChildMonitoring.Location = new System.Drawing.Point(0, 210);
            this.tbrChildMonitoring.Name = "tbrChildMonitoring";
            this.tbrChildMonitoring.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.tbrChildMonitoring.Size = new System.Drawing.Size(996, 62);
            this.tbrChildMonitoring.TabIndex = 8;
            this.tbrChildMonitoring.Text = "toolStrip1";
            this.tbrChildMonitoring.Visible = false;
            // 
            // tbrItemSystemAudit
            // 
            this.tbrItemSystemAudit.Image = ((System.Drawing.Image)(resources.GetObject("tbrItemSystemAudit.Image")));
            this.tbrItemSystemAudit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbrItemSystemAudit.Name = "tbrItemSystemAudit";
            this.tbrItemSystemAudit.Padding = new System.Windows.Forms.Padding(5);
            this.tbrItemSystemAudit.Size = new System.Drawing.Size(82, 59);
            this.tbrItemSystemAudit.Tag = "MAIN_TBAR_SYSTEMAUDIT";
            this.tbrItemSystemAudit.Text = "System Audit";
            this.tbrItemSystemAudit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tbrItemSystemMonitoring
            // 
            this.tbrItemSystemMonitoring.Image = ((System.Drawing.Image)(resources.GetObject("tbrItemSystemMonitoring.Image")));
            this.tbrItemSystemMonitoring.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbrItemSystemMonitoring.Name = "tbrItemSystemMonitoring";
            this.tbrItemSystemMonitoring.Padding = new System.Windows.Forms.Padding(5);
            this.tbrItemSystemMonitoring.Size = new System.Drawing.Size(107, 59);
            this.tbrItemSystemMonitoring.Tag = "MAIN_TBAR_SYSMON";
            this.tbrItemSystemMonitoring.Text = "System Monitoring";
            this.tbrItemSystemMonitoring.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tbrItemDataCommsAudit
            // 
            this.tbrItemDataCommsAudit.Image = ((System.Drawing.Image)(resources.GetObject("tbrItemDataCommsAudit.Image")));
            this.tbrItemDataCommsAudit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbrItemDataCommsAudit.Name = "tbrItemDataCommsAudit";
            this.tbrItemDataCommsAudit.Padding = new System.Windows.Forms.Padding(5);
            this.tbrItemDataCommsAudit.Size = new System.Drawing.Size(108, 59);
            this.tbrItemDataCommsAudit.Tag = "MAIN_TBAR_DATACOMMSAUDIT";
            this.tbrItemDataCommsAudit.Text = "Data Comms Audit";
            this.tbrItemDataCommsAudit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tbrChildAdmin
            // 
            this.tbrChildAdmin.CanOverflow = false;
            this.tbrChildAdmin.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.tbrChildAdmin.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tbrChildAdmin.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tbrItemOrganisation,
            this.tbrItemUserAdmin,
            this.tbrItemSettings,
            this.tbrItemDepot,
            this.tbrItemOperators,
            this.tbrItemCalendars,
            this.tbrItemOpenHours,
            this.tbrItemSiteSettings,
            this.tbrItemDeclaration,
            this.tbrItemStacker,
            this.tbrItemDropSchedule,
            this.tbrItemSiteLicensing,
            this.tsbAGSCombination,
            this.tbrItemEmployee});
            this.tbrChildAdmin.Location = new System.Drawing.Point(0, 210);
            this.tbrChildAdmin.Name = "tbrChildAdmin";
            this.tbrChildAdmin.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.tbrChildAdmin.Size = new System.Drawing.Size(996, 62);
            this.tbrChildAdmin.TabIndex = 4;
            this.tbrChildAdmin.Text = "toolStrip1";
            this.tbrChildAdmin.Visible = false;
            // 
            // tbrItemOrganisation
            // 
            this.tbrItemOrganisation.Image = ((System.Drawing.Image)(resources.GetObject("tbrItemOrganisation.Image")));
            this.tbrItemOrganisation.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbrItemOrganisation.Name = "tbrItemOrganisation";
            this.tbrItemOrganisation.Padding = new System.Windows.Forms.Padding(5);
            this.tbrItemOrganisation.Size = new System.Drawing.Size(80, 59);
            this.tbrItemOrganisation.Tag = "MAIN_TBAR_ORGANISATION";
            this.tbrItemOrganisation.Text = "Organisation";
            this.tbrItemOrganisation.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tbrItemUserAdmin
            // 
            this.tbrItemUserAdmin.Image = ((System.Drawing.Image)(resources.GetObject("tbrItemUserAdmin.Image")));
            this.tbrItemUserAdmin.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbrItemUserAdmin.Name = "tbrItemUserAdmin";
            this.tbrItemUserAdmin.Padding = new System.Windows.Forms.Padding(5);
            this.tbrItemUserAdmin.Size = new System.Drawing.Size(111, 59);
            this.tbrItemUserAdmin.Tag = "MAIN_TBAR_USERADMINISTRATION";
            this.tbrItemUserAdmin.Text = "User Administration";
            this.tbrItemUserAdmin.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tbrItemSettings
            // 
            this.tbrItemSettings.Image = ((System.Drawing.Image)(resources.GetObject("tbrItemSettings.Image")));
            this.tbrItemSettings.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbrItemSettings.Name = "tbrItemSettings";
            this.tbrItemSettings.Padding = new System.Windows.Forms.Padding(5);
            this.tbrItemSettings.Size = new System.Drawing.Size(59, 59);
            this.tbrItemSettings.Tag = "MAIN_TBAR_SETTINGS";
            this.tbrItemSettings.Text = "Settings";
            this.tbrItemSettings.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tbrItemDepot
            // 
            this.tbrItemDepot.Image = ((System.Drawing.Image)(resources.GetObject("tbrItemDepot.Image")));
            this.tbrItemDepot.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbrItemDepot.Name = "tbrItemDepot";
            this.tbrItemDepot.Padding = new System.Windows.Forms.Padding(5);
            this.tbrItemDepot.Size = new System.Drawing.Size(50, 59);
            this.tbrItemDepot.Tag = "MAIN_TBAR_DEPOT";
            this.tbrItemDepot.Text = "Depot";
            this.tbrItemDepot.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tbrItemOperators
            // 
            this.tbrItemOperators.Image = ((System.Drawing.Image)(resources.GetObject("tbrItemOperators.Image")));
            this.tbrItemOperators.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbrItemOperators.Name = "tbrItemOperators";
            this.tbrItemOperators.Padding = new System.Windows.Forms.Padding(5);
            this.tbrItemOperators.Size = new System.Drawing.Size(67, 59);
            this.tbrItemOperators.Tag = "MAIN_TBAR_OPERATORS";
            this.tbrItemOperators.Text = "Operators";
            this.tbrItemOperators.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tbrItemCalendars
            // 
            this.tbrItemCalendars.Image = ((System.Drawing.Image)(resources.GetObject("tbrItemCalendars.Image")));
            this.tbrItemCalendars.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbrItemCalendars.Name = "tbrItemCalendars";
            this.tbrItemCalendars.Padding = new System.Windows.Forms.Padding(5);
            this.tbrItemCalendars.Size = new System.Drawing.Size(68, 59);
            this.tbrItemCalendars.Tag = "MAIN_TBAR_CALENDARS";
            this.tbrItemCalendars.Text = "Calendars";
            this.tbrItemCalendars.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tbrItemOpenHours
            // 
            this.tbrItemOpenHours.Image = ((System.Drawing.Image)(resources.GetObject("tbrItemOpenHours.Image")));
            this.tbrItemOpenHours.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbrItemOpenHours.Name = "tbrItemOpenHours";
            this.tbrItemOpenHours.Padding = new System.Windows.Forms.Padding(5);
            this.tbrItemOpenHours.Size = new System.Drawing.Size(78, 59);
            this.tbrItemOpenHours.Tag = "MAIN_TBAR_OPENHOURS";
            this.tbrItemOpenHours.Text = "Open Hours";
            this.tbrItemOpenHours.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tbrItemSiteSettings
            // 
            this.tbrItemSiteSettings.Image = ((System.Drawing.Image)(resources.GetObject("tbrItemSiteSettings.Image")));
            this.tbrItemSiteSettings.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbrItemSiteSettings.Name = "tbrItemSiteSettings";
            this.tbrItemSiteSettings.Padding = new System.Windows.Forms.Padding(5);
            this.tbrItemSiteSettings.Size = new System.Drawing.Size(80, 59);
            this.tbrItemSiteSettings.Tag = "MAIN_TBAR_SITESETTINGS";
            this.tbrItemSiteSettings.Text = "Site Settings";
            this.tbrItemSiteSettings.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tbrItemDeclaration
            // 
            this.tbrItemDeclaration.Image = ((System.Drawing.Image)(resources.GetObject("tbrItemDeclaration.Image")));
            this.tbrItemDeclaration.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbrItemDeclaration.Name = "tbrItemDeclaration";
            this.tbrItemDeclaration.Padding = new System.Windows.Forms.Padding(5);
            this.tbrItemDeclaration.Size = new System.Drawing.Size(75, 59);
            this.tbrItemDeclaration.Tag = "MAIN_TBAR_DECLARATION";
            this.tbrItemDeclaration.Text = "Declaration";
            this.tbrItemDeclaration.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tbrItemStacker
            // 
            this.tbrItemStacker.Image = ((System.Drawing.Image)(resources.GetObject("tbrItemStacker.Image")));
            this.tbrItemStacker.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbrItemStacker.Name = "tbrItemStacker";
            this.tbrItemStacker.Padding = new System.Windows.Forms.Padding(5);
            this.tbrItemStacker.Size = new System.Drawing.Size(58, 59);
            this.tbrItemStacker.Tag = "MAIN_TBAR_STACKER";
            this.tbrItemStacker.Text = "Stacker";
            this.tbrItemStacker.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tbrItemDropSchedule
            // 
            this.tbrItemDropSchedule.Image = ((System.Drawing.Image)(resources.GetObject("tbrItemDropSchedule.Image")));
            this.tbrItemDropSchedule.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbrItemDropSchedule.Name = "tbrItemDropSchedule";
            this.tbrItemDropSchedule.Padding = new System.Windows.Forms.Padding(5);
            this.tbrItemDropSchedule.Size = new System.Drawing.Size(92, 59);
            this.tbrItemDropSchedule.Tag = "MAIN_TBAR_DROPSCHEDULE";
            this.tbrItemDropSchedule.Text = "Drop Schedule";
            this.tbrItemDropSchedule.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tbrItemSiteLicensing
            // 
            this.tbrItemSiteLicensing.Image = ((System.Drawing.Image)(resources.GetObject("tbrItemSiteLicensing.Image")));
            this.tbrItemSiteLicensing.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbrItemSiteLicensing.Name = "tbrItemSiteLicensing";
            this.tbrItemSiteLicensing.Padding = new System.Windows.Forms.Padding(5);
            this.tbrItemSiteLicensing.Size = new System.Drawing.Size(87, 59);
            this.tbrItemSiteLicensing.Tag = "MAIN_TBAR_SITELICENSING";
            this.tbrItemSiteLicensing.Text = "Site Licensing";
            this.tbrItemSiteLicensing.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tsbAGSCombination
            // 
            this.tsbAGSCombination.Image = ((System.Drawing.Image)(resources.GetObject("tsbAGSCombination.Image")));
            this.tsbAGSCombination.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAGSCombination.Name = "tsbAGSCombination";
            this.tsbAGSCombination.Size = new System.Drawing.Size(94, 59);
            this.tsbAGSCombination.Tag = "MAIN_TBAR_AGS";
            this.tsbAGSCombination.Text = "AGS Combination";
            this.tsbAGSCombination.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tbrItemEmployee
            // 
            this.tbrItemEmployee.Image = ((System.Drawing.Image)(resources.GetObject("tbrItemEmployee.Image")));
            this.tbrItemEmployee.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbrItemEmployee.Name = "tbrItemEmployee";
            this.tbrItemEmployee.Size = new System.Drawing.Size(117, 59);
            this.tbrItemEmployee.Tag = "MAIN_TBAR_EMPLOYEE";
            this.tbrItemEmployee.Text = "Employee Card Details";
            this.tbrItemEmployee.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tbrNoLogin
            // 
            this.tbrNoLogin.CanOverflow = false;
            this.tbrNoLogin.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.tbrNoLogin.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tbrNoLogin.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tbrItemLogin});
            this.tbrNoLogin.Location = new System.Drawing.Point(0, 148);
            this.tbrNoLogin.Name = "tbrNoLogin";
            this.tbrNoLogin.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.tbrNoLogin.Size = new System.Drawing.Size(996, 62);
            this.tbrNoLogin.TabIndex = 3;
            this.tbrNoLogin.Text = "toolStrip1";
            // 
            // tbrItemLogin
            // 
            this.tbrItemLogin.Image = ((System.Drawing.Image)(resources.GetObject("tbrItemLogin.Image")));
            this.tbrItemLogin.ImageTransparentColor = System.Drawing.Color.Fuchsia;
            this.tbrItemLogin.Name = "tbrItemLogin";
            this.tbrItemLogin.Padding = new System.Windows.Forms.Padding(5);
            this.tbrItemLogin.Size = new System.Drawing.Size(47, 59);
            this.tbrItemLogin.Tag = "MAIN_TBAR_LOGIN";
            this.tbrItemLogin.Text = "Login";
            this.tbrItemLogin.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tbrItemLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // tbrMain
            // 
            this.tbrMain.CanOverflow = false;
            this.tbrMain.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.tbrMain.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tbrMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tbrItemAdmin,
            this.tbrItemViewSites,
            this.tbrItemServiceCalls,
            this.tbrItemAssets,
            this.tbrItemGameLibrary,
            this.tbrItemFinancial,
            this.tbrItemReports,
            this.tbrItemDataSheet,
            this.tbrItemAnalysis,
            this.tbrItemMeterAdjustment,
            this.tbrItemVault,
            this.tbrItemSep1,
            this.tbrItemMonitoring,
            this.tbrItemChangePwd,
            this.tbrItemLogout});
            this.tbrMain.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.tbrMain.Location = new System.Drawing.Point(0, 24);
            this.tbrMain.Name = "tbrMain";
            this.tbrMain.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.tbrMain.Size = new System.Drawing.Size(996, 124);
            this.tbrMain.TabIndex = 2;
            // 
            // tbrItemAdmin
            // 
            this.tbrItemAdmin.Image = ((System.Drawing.Image)(resources.GetObject("tbrItemAdmin.Image")));
            this.tbrItemAdmin.ImageTransparentColor = System.Drawing.Color.Fuchsia;
            this.tbrItemAdmin.Name = "tbrItemAdmin";
            this.tbrItemAdmin.Padding = new System.Windows.Forms.Padding(5);
            this.tbrItemAdmin.Size = new System.Drawing.Size(50, 59);
            this.tbrItemAdmin.Tag = "MAIN_TBAR_ADMIN";
            this.tbrItemAdmin.Text = "Admin";
            this.tbrItemAdmin.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tbrItemViewSites
            // 
            this.tbrItemViewSites.Image = ((System.Drawing.Image)(resources.GetObject("tbrItemViewSites.Image")));
            this.tbrItemViewSites.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbrItemViewSites.Name = "tbrItemViewSites";
            this.tbrItemViewSites.Padding = new System.Windows.Forms.Padding(5);
            this.tbrItemViewSites.Size = new System.Drawing.Size(70, 59);
            this.tbrItemViewSites.Tag = "MAIN_TBAR_VIEWSITES";
            this.tbrItemViewSites.Text = "View Sites";
            this.tbrItemViewSites.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tbrItemServiceCalls
            // 
            this.tbrItemServiceCalls.Image = ((System.Drawing.Image)(resources.GetObject("tbrItemServiceCalls.Image")));
            this.tbrItemServiceCalls.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbrItemServiceCalls.Name = "tbrItemServiceCalls";
            this.tbrItemServiceCalls.Padding = new System.Windows.Forms.Padding(5);
            this.tbrItemServiceCalls.Size = new System.Drawing.Size(82, 59);
            this.tbrItemServiceCalls.Tag = "MAIN_TBAR_SERVICECALLS";
            this.tbrItemServiceCalls.Text = "Service Calls";
            this.tbrItemServiceCalls.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tbrItemAssets
            // 
            this.tbrItemAssets.Image = ((System.Drawing.Image)(resources.GetObject("tbrItemAssets.Image")));
            this.tbrItemAssets.ImageTransparentColor = System.Drawing.Color.Fuchsia;
            this.tbrItemAssets.Name = "tbrItemAssets";
            this.tbrItemAssets.Padding = new System.Windows.Forms.Padding(5);
            this.tbrItemAssets.Size = new System.Drawing.Size(52, 59);
            this.tbrItemAssets.Tag = "MAIN_TBAR_ASSETS";
            this.tbrItemAssets.Text = "Assets";
            this.tbrItemAssets.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tbrItemGameLibrary
            // 
            this.tbrItemGameLibrary.Image = ((System.Drawing.Image)(resources.GetObject("tbrItemGameLibrary.Image")));
            this.tbrItemGameLibrary.ImageTransparentColor = System.Drawing.Color.Fuchsia;
            this.tbrItemGameLibrary.Name = "tbrItemGameLibrary";
            this.tbrItemGameLibrary.Padding = new System.Windows.Forms.Padding(5);
            this.tbrItemGameLibrary.Size = new System.Drawing.Size(83, 59);
            this.tbrItemGameLibrary.Tag = "MAIN_TBAR_GAMELIBRARY";
            this.tbrItemGameLibrary.Text = "Game Library";
            this.tbrItemGameLibrary.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tbrItemFinancial
            // 
            this.tbrItemFinancial.Image = ((System.Drawing.Image)(resources.GetObject("tbrItemFinancial.Image")));
            this.tbrItemFinancial.ImageTransparentColor = System.Drawing.Color.Fuchsia;
            this.tbrItemFinancial.Name = "tbrItemFinancial";
            this.tbrItemFinancial.Padding = new System.Windows.Forms.Padding(5);
            this.tbrItemFinancial.Size = new System.Drawing.Size(57, 59);
            this.tbrItemFinancial.Tag = "MAIN_TBAR_FINANCIAL";
            this.tbrItemFinancial.Text = "Fiancial";
            this.tbrItemFinancial.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tbrItemReports
            // 
            this.tbrItemReports.Image = ((System.Drawing.Image)(resources.GetObject("tbrItemReports.Image")));
            this.tbrItemReports.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbrItemReports.Name = "tbrItemReports";
            this.tbrItemReports.Padding = new System.Windows.Forms.Padding(5);
            this.tbrItemReports.Size = new System.Drawing.Size(58, 59);
            this.tbrItemReports.Tag = "MAIN_TBAR_REPORTS";
            this.tbrItemReports.Text = "Reports";
            this.tbrItemReports.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tbrItemDataSheet
            // 
            this.tbrItemDataSheet.Image = ((System.Drawing.Image)(resources.GetObject("tbrItemDataSheet.Image")));
            this.tbrItemDataSheet.ImageTransparentColor = System.Drawing.Color.Fuchsia;
            this.tbrItemDataSheet.Name = "tbrItemDataSheet";
            this.tbrItemDataSheet.Padding = new System.Windows.Forms.Padding(5);
            this.tbrItemDataSheet.Size = new System.Drawing.Size(75, 59);
            this.tbrItemDataSheet.Tag = "MAIN_TBAR_DATASHEET";
            this.tbrItemDataSheet.Text = "Data Sheet";
            this.tbrItemDataSheet.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tbrItemAnalysis
            // 
            this.tbrItemAnalysis.Image = ((System.Drawing.Image)(resources.GetObject("tbrItemAnalysis.Image")));
            this.tbrItemAnalysis.ImageTransparentColor = System.Drawing.Color.Fuchsia;
            this.tbrItemAnalysis.Name = "tbrItemAnalysis";
            this.tbrItemAnalysis.Padding = new System.Windows.Forms.Padding(5);
            this.tbrItemAnalysis.Size = new System.Drawing.Size(59, 59);
            this.tbrItemAnalysis.Tag = "MAIN_TBAR_ANALYSIS";
            this.tbrItemAnalysis.Text = "Analysis";
            this.tbrItemAnalysis.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tbrItemMeterAdjustment
            // 
            this.tbrItemMeterAdjustment.Image = ((System.Drawing.Image)(resources.GetObject("tbrItemMeterAdjustment.Image")));
            this.tbrItemMeterAdjustment.ImageTransparentColor = System.Drawing.Color.Fuchsia;
            this.tbrItemMeterAdjustment.Name = "tbrItemMeterAdjustment";
            this.tbrItemMeterAdjustment.Padding = new System.Windows.Forms.Padding(5);
            this.tbrItemMeterAdjustment.Size = new System.Drawing.Size(103, 59);
            this.tbrItemMeterAdjustment.Tag = "MAIN_TBAR_METERADJUSTMENT";
            this.tbrItemMeterAdjustment.Text = "Meter Adjustment";
            this.tbrItemMeterAdjustment.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tbrItemVault
            // 
            this.tbrItemVault.AutoSize = false;
            this.tbrItemVault.Image = ((System.Drawing.Image)(resources.GetObject("tbrItemVault.Image")));
            this.tbrItemVault.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbrItemVault.Name = "tbrItemVault";
            this.tbrItemVault.Size = new System.Drawing.Size(59, 59);
            this.tbrItemVault.Text = "Vault ";
            this.tbrItemVault.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tbrItemSep1
            // 
            this.tbrItemSep1.AutoSize = false;
            this.tbrItemSep1.Name = "tbrItemSep1";
            this.tbrItemSep1.Size = new System.Drawing.Size(6, 59);
            // 
            // tbrItemMonitoring
            // 
            this.tbrItemMonitoring.Image = ((System.Drawing.Image)(resources.GetObject("tbrItemMonitoring.Image")));
            this.tbrItemMonitoring.ImageTransparentColor = System.Drawing.Color.Fuchsia;
            this.tbrItemMonitoring.Name = "tbrItemMonitoring";
            this.tbrItemMonitoring.Padding = new System.Windows.Forms.Padding(5);
            this.tbrItemMonitoring.Size = new System.Drawing.Size(106, 59);
            this.tbrItemMonitoring.Tag = "MAIN_TBAR_AUDITMONITOR";
            this.tbrItemMonitoring.Text = "Audit && Monitoring";
            this.tbrItemMonitoring.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tbrItemChangePwd
            // 
            this.tbrItemChangePwd.Image = ((System.Drawing.Image)(resources.GetObject("tbrItemChangePwd.Image")));
            this.tbrItemChangePwd.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tbrItemChangePwd.ImageTransparentColor = System.Drawing.Color.Fuchsia;
            this.tbrItemChangePwd.Name = "tbrItemChangePwd";
            this.tbrItemChangePwd.Padding = new System.Windows.Forms.Padding(5);
            this.tbrItemChangePwd.Size = new System.Drawing.Size(107, 59);
            this.tbrItemChangePwd.Tag = "MAIN_TBAR_CHANGEPWD";
            this.tbrItemChangePwd.Text = "Change Password";
            this.tbrItemChangePwd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tbrItemLogout
            // 
            this.tbrItemLogout.Image = ((System.Drawing.Image)(resources.GetObject("tbrItemLogout.Image")));
            this.tbrItemLogout.ImageTransparentColor = System.Drawing.Color.Fuchsia;
            this.tbrItemLogout.Name = "tbrItemLogout";
            this.tbrItemLogout.Padding = new System.Windows.Forms.Padding(5);
            this.tbrItemLogout.Size = new System.Drawing.Size(54, 59);
            this.tbrItemLogout.Tag = "MAIN_TBAR_LOGOUT";
            this.tbrItemLogout.Text = "Logout";
            this.tbrItemLogout.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tbrItemLogout.Click += new System.EventHandler(this.tbrItemLogout_Click);
            // 
            // tbrChildFinancialSGVI
            // 
            this.tbrChildFinancialSGVI.CanOverflow = false;
            this.tbrChildFinancialSGVI.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.tbrChildFinancialSGVI.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tbrChildFinancialSGVI.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tbrItemTerms,
            this.tbrItemShares,
            this.tbrItemTermsSummary,
            this.tbrItemPeriodEnd});
            this.tbrChildFinancialSGVI.Location = new System.Drawing.Point(31, 210);
            this.tbrChildFinancialSGVI.Name = "tbrChildFinancialSGVI";
            this.tbrChildFinancialSGVI.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.tbrChildFinancialSGVI.Size = new System.Drawing.Size(965, 62);
            this.tbrChildFinancialSGVI.TabIndex = 16;
            this.tbrChildFinancialSGVI.Text = "toolStrip1";
            this.tbrChildFinancialSGVI.Visible = false;
            // 
            // tbrItemTerms
            // 
            this.tbrItemTerms.Image = ((System.Drawing.Image)(resources.GetObject("tbrItemTerms.Image")));
            this.tbrItemTerms.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbrItemTerms.Name = "tbrItemTerms";
            this.tbrItemTerms.Padding = new System.Windows.Forms.Padding(5);
            this.tbrItemTerms.Size = new System.Drawing.Size(50, 59);
            this.tbrItemTerms.Tag = "MAIN_TBAR_TERMS";
            this.tbrItemTerms.Text = "Terms";
            this.tbrItemTerms.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tbrItemTerms.Visible = false;
            // 
            // tbrItemShares
            // 
            this.tbrItemShares.Image = ((System.Drawing.Image)(resources.GetObject("tbrItemShares.Image")));
            this.tbrItemShares.ImageTransparentColor = System.Drawing.Color.White;
            this.tbrItemShares.Name = "tbrItemShares";
            this.tbrItemShares.Padding = new System.Windows.Forms.Padding(5);
            this.tbrItemShares.Size = new System.Drawing.Size(54, 59);
            this.tbrItemShares.Tag = "MAIN_TBAR_SHARES";
            this.tbrItemShares.Text = "Shares";
            this.tbrItemShares.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tbrItemShares.Visible = false;
            // 
            // tbrItemTermsSummary
            // 
            this.tbrItemTermsSummary.Image = ((System.Drawing.Image)(resources.GetObject("tbrItemTermsSummary.Image")));
            this.tbrItemTermsSummary.ImageTransparentColor = System.Drawing.Color.Black;
            this.tbrItemTermsSummary.Name = "tbrItemTermsSummary";
            this.tbrItemTermsSummary.Padding = new System.Windows.Forms.Padding(5);
            this.tbrItemTermsSummary.Size = new System.Drawing.Size(96, 59);
            this.tbrItemTermsSummary.Tag = "MAIN_TBAR_TERMSSUMMARY";
            this.tbrItemTermsSummary.Text = "Terms Summary";
            this.tbrItemTermsSummary.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tbrItemTermsSummary.Visible = false;
            // 
            // tbrItemPeriodEnd
            // 
            this.tbrItemPeriodEnd.Image = ((System.Drawing.Image)(resources.GetObject("tbrItemPeriodEnd.Image")));
            this.tbrItemPeriodEnd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbrItemPeriodEnd.Name = "tbrItemPeriodEnd";
            this.tbrItemPeriodEnd.Size = new System.Drawing.Size(63, 59);
            this.tbrItemPeriodEnd.Tag = "MAIN_TBAR_PERIODEND";
            this.tbrItemPeriodEnd.Text = "Period End";
            this.tbrItemPeriodEnd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tbrItemPeriodEnd.Visible = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::BMC.EnterpriseClient.Properties.Resources.BMC_Enterprise_Transparent2;
            this.ClientSize = new System.Drawing.Size(996, 573);
            this.Controls.Add(this.uxActiveItems);
            this.Controls.Add(this.tbrChildAdmin);
            this.Controls.Add(this.tbrChildServiceCalls);
            this.Controls.Add(this.tbrChildVault);
            this.Controls.Add(this.tbrChildFinancial);
            this.Controls.Add(this.tbrChildMonitoring);
            this.Controls.Add(this.tbrChildFinancialSGVI);
            this.Controls.Add(this.tbrNoLogin);
            this.Controls.Add(this.tbrMain);
            this.Controls.Add(this.sbarMain);
            this.Controls.Add(this.mbarMain);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.mbarMain;
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "MainForm";
            this.Text = "BMC Enterprise Client";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.sbarMain.ResumeLayout(false);
            this.sbarMain.PerformLayout();
            this.mbarMain.ResumeLayout(false);
            this.mbarMain.PerformLayout();
            this.uxActiveItems.ChildContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvActiveItems)).EndInit();
            this.tbrChildServiceCalls.ResumeLayout(false);
            this.tbrChildServiceCalls.PerformLayout();
            this.tbrChildVault.ResumeLayout(false);
            this.tbrChildVault.PerformLayout();
            this.tbrChildFinancial.ResumeLayout(false);
            this.tbrChildFinancial.PerformLayout();
            this.tbrChildMonitoring.ResumeLayout(false);
            this.tbrChildMonitoring.PerformLayout();
            this.tbrChildAdmin.ResumeLayout(false);
            this.tbrChildAdmin.PerformLayout();
            this.tbrNoLogin.ResumeLayout(false);
            this.tbrNoLogin.PerformLayout();
            this.tbrMain.ResumeLayout(false);
            this.tbrMain.PerformLayout();
            this.tbrChildFinancialSGVI.ResumeLayout(false);
            this.tbrChildFinancialSGVI.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip sbarMain;
        private System.Windows.Forms.ToolStripStatusLabel sbrItemUserDetails;
        private System.Windows.Forms.ToolStripStatusLabel sbrItemSpring;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel4;
        //private System.Windows.Forms.ToolStripButton tbrItemGroups;
        //private System.Windows.Forms.ToolStripButton tbrItemAccess;
        // private System.Windows.Forms.ToolStripButton tbrItemUsers;
        private System.Windows.Forms.ToolStripDropDownButton sbrItemServices;
        private System.Windows.Forms.ToolStripStatusLabel sbrItemDatabase;
        private System.Windows.Forms.ToolStripStatusLabel sbrItemNetwork;
        private System.Windows.Forms.MenuStrip mbarMain;
        private System.Windows.Forms.ToolStripMenuItem mnuFile;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuHelpAbout;
        private System.Windows.Forms.ToolStripMenuItem mnuFileCloseAll;
        private System.Windows.Forms.ToolStripMenuItem mnuFileExit;
        private BMC.CoreLib.Win32.UxDockPanel uxActiveItems;
        private System.Windows.Forms.DataGridView dgvActiveItems;
        private System.Windows.Forms.DataGridViewButtonColumn chdrActiveClose;
        private System.Windows.Forms.DataGridViewImageColumn chdrActiveImage;
        private System.Windows.Forms.DataGridViewTextBoxColumn chdrActiveItem;
        private System.Windows.Forms.ToolStripMenuItem mnuView;
        private System.Windows.Forms.ToolStripMenuItem mnuViewToolbar;
        private System.Windows.Forms.ToolStripMenuItem mnuViewStatusbar;
        private System.Windows.Forms.ToolStripSeparator mnuFileSep1;
        private System.Windows.Forms.ToolStripStatusLabel sbrDateTime;
        private System.Windows.Forms.ToolStripStatusLabel sbrCaps;
        private System.Windows.Forms.ToolStripStatusLabel sbrNum;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolStrip tbrChildServiceCalls;
        private System.Windows.Forms.ToolStripButton tbrItemCreateServiceCall;
        private System.Windows.Forms.ToolStripButton tbrItemCurrentServiceCalls;
        private System.Windows.Forms.ToolStripButton tbrItemClosedServiceCalls;
        private System.Windows.Forms.ToolStripButton tbrItemServiceAdmin;
        private System.Windows.Forms.ToolStripButton tbrItemGMUEvents;
        private System.Windows.Forms.ToolStripButton tbrItemEventViewer;
        private System.Windows.Forms.ToolStrip tbrChildVault;
        private System.Windows.Forms.ToolStripButton tbrItemAddVault;
        private System.Windows.Forms.ToolStripButton tbrItemVaultDeclaration;
        private System.Windows.Forms.ToolStripButton tbrItemVaultAudit;
        private System.Windows.Forms.ToolStrip tbrChildFinancial;
        private System.Windows.Forms.ToolStripButton tbrItemShareHolders;
        private System.Windows.Forms.ToolStripButton tbrItemProfitShareGroup;
        private System.Windows.Forms.ToolStripButton tbrItemExpenseShareGroup;
        private System.Windows.Forms.ToolStripButton tbrItemReadLiquidation;
        private System.Windows.Forms.ToolStripButton tsbCollectionLiquidation;
        private System.Windows.Forms.ToolStrip tbrChildMonitoring;
        private System.Windows.Forms.ToolStripButton tbrItemSystemAudit;
        private System.Windows.Forms.ToolStripButton tbrItemSystemMonitoring;
        private System.Windows.Forms.ToolStripButton tbrItemDataCommsAudit;
        private System.Windows.Forms.ToolStrip tbrChildAdmin;
        private System.Windows.Forms.ToolStripButton tbrItemOrganisation;
        private System.Windows.Forms.ToolStripButton tbrItemUserAdmin;
        private System.Windows.Forms.ToolStripButton tbrItemSettings;
        private System.Windows.Forms.ToolStripButton tbrItemDepot;
        private System.Windows.Forms.ToolStripButton tbrItemOperators;
        private System.Windows.Forms.ToolStripButton tbrItemCalendars;
        private System.Windows.Forms.ToolStripButton tbrItemOpenHours;
        private System.Windows.Forms.ToolStripButton tbrItemSiteSettings;
        private System.Windows.Forms.ToolStripButton tbrItemDeclaration;
        private System.Windows.Forms.ToolStripButton tbrItemStacker;
        private System.Windows.Forms.ToolStripButton tbrItemDropSchedule;
        private System.Windows.Forms.ToolStripButton tbrItemSiteLicensing;
        private System.Windows.Forms.ToolStripButton tsbAGSCombination;
        private System.Windows.Forms.ToolStrip tbrNoLogin;
        private System.Windows.Forms.ToolStripButton tbrItemLogin;
        private System.Windows.Forms.ToolStrip tbrMain;
        private System.Windows.Forms.ToolStripButton tbrItemAdmin;
        private System.Windows.Forms.ToolStripButton tbrItemViewSites;
        private System.Windows.Forms.ToolStripButton tbrItemServiceCalls;
        private System.Windows.Forms.ToolStripButton tbrItemAssets;
        private System.Windows.Forms.ToolStripButton tbrItemGameLibrary;
        private System.Windows.Forms.ToolStripButton tbrItemFinancial;
        private System.Windows.Forms.ToolStripButton tbrItemReports;
        private System.Windows.Forms.ToolStripButton tbrItemDataSheet;
        private System.Windows.Forms.ToolStripButton tbrItemAnalysis;
        private System.Windows.Forms.ToolStripButton tbrItemMeterAdjustment;
        private System.Windows.Forms.ToolStripButton tbrItemVault;
        private System.Windows.Forms.ToolStripSeparator tbrItemSep1;
        private System.Windows.Forms.ToolStripButton tbrItemMonitoring;
        private System.Windows.Forms.ToolStripButton tbrItemChangePwd;
        private System.Windows.Forms.ToolStripButton tbrItemLogout;
 		private System.Windows.Forms.ToolStrip tbrChildFinancialSGVI;
        private System.Windows.Forms.ToolStripButton tbrItemTerms;
        private System.Windows.Forms.ToolStripButton tbrItemShares;
        private System.Windows.Forms.ToolStripButton tbrItemTermsSummary;
        private System.Windows.Forms.ToolStripButton tbrItemPeriodEnd;
        private System.Windows.Forms.ToolStripButton tbrItemEmployee;
		private System.Windows.Forms.ToolStripStatusLabel sbr_Notitfications;
        private System.Windows.Forms.ToolStripButton tbrItemAlerts;
        private System.Windows.Forms.ToolStripButton tbrItemShowAlerts;
        
    }
}

