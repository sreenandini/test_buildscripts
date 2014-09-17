using BMC.EnterpriseClient.Helpers;
namespace BMC.EnterpriseClient.Views
{
    partial class ViewSitesForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ViewSitesForm));
            this.tblContainer = new System.Windows.Forms.TableLayoutPanel();
            this.tblContent = new System.Windows.Forms.TableLayoutPanel();
            this.tblSites = new System.Windows.Forms.TableLayoutPanel();
            this.chkDisplayActiveSites = new System.Windows.Forms.CheckBox();
            this.tblFilter = new System.Windows.Forms.TableLayoutPanel();
            this.cboSiteSearch = new BMC.EnterpriseClient.Helpers.BmcComboBox();
            this.btnFilter = new System.Windows.Forms.Button();
            this.tblDetails = new System.Windows.Forms.TableLayoutPanel();
            this.tabDetails2 = new System.Windows.Forms.TabControl();
            this.tbpDrop = new System.Windows.Forms.TabPage();
            this.tbpHourly = new System.Windows.Forms.TabPage();
            this.tbpAssets = new System.Windows.Forms.TabPage();
            this.tabDetails = new System.Windows.Forms.TabControl();
            this.tbpSiteDetails = new System.Windows.Forms.TabPage();
            this.tbpInstallations = new System.Windows.Forms.TabPage();
            this.tblButtons = new System.Windows.Forms.TableLayoutPanel();
            this.btnMovePrev = new System.Windows.Forms.Button();
            this.imglstSmallIcons = new System.Windows.Forms.ImageList(this.components);
            this.btnMoveFirst = new System.Windows.Forms.Button();
            this.btnTree = new System.Windows.Forms.Button();
            this.btnMoveNext = new System.Windows.Forms.Button();
            this.btnMoveLast = new System.Windows.Forms.Button();
            this.cboSites = new BMC.EnterpriseClient.Helpers.BmcComboBox();
            this.btnExport = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.chkDisplay = new System.Windows.Forms.CheckBox();
            this.btnCashierTransactions = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.ctxMenuCompany = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ctxMenuItemCompany = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxMenuSubCompany = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ctxMenuItemSubCompany = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxMenuSite = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ctxMenuItemSite = new System.Windows.Forms.ToolStripMenuItem();
            this.tblContainer.SuspendLayout();
            this.tblContent.SuspendLayout();
            this.tblSites.SuspendLayout();
            this.tblFilter.SuspendLayout();
            this.tblDetails.SuspendLayout();
            this.tabDetails2.SuspendLayout();
            this.tabDetails.SuspendLayout();
            this.tblButtons.SuspendLayout();
            this.ctxMenuCompany.SuspendLayout();
            this.ctxMenuSubCompany.SuspendLayout();
            this.ctxMenuSite.SuspendLayout();
            this.SuspendLayout();
            // 
            // tblContainer
            // 
            this.tblContainer.ColumnCount = 1;
            this.tblContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblContainer.Controls.Add(this.tblContent, 0, 0);
            this.tblContainer.Controls.Add(this.tblButtons, 0, 1);
            this.tblContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblContainer.Location = new System.Drawing.Point(0, 0);
            this.tblContainer.Name = "tblContainer";
            this.tblContainer.RowCount = 2;
            this.tblContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tblContainer.Size = new System.Drawing.Size(1028, 603);
            this.tblContainer.TabIndex = 0;
            // 
            // tblContent
            // 
            this.tblContent.ColumnCount = 2;
            this.tblContent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 300F));
            this.tblContent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblContent.Controls.Add(this.tblSites, 0, 0);
            this.tblContent.Controls.Add(this.tblDetails, 1, 0);
            this.tblContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblContent.Location = new System.Drawing.Point(0, 3);
            this.tblContent.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.tblContent.Name = "tblContent";
            this.tblContent.RowCount = 1;
            this.tblContent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblContent.Size = new System.Drawing.Size(1025, 557);
            this.tblContent.TabIndex = 0;
            // 
            // tblSites
            // 
            this.tblSites.ColumnCount = 1;
            this.tblSites.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblSites.Controls.Add(this.chkDisplayActiveSites, 0, 1);
            this.tblSites.Controls.Add(this.tblFilter, 0, 2);
            this.tblSites.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblSites.Location = new System.Drawing.Point(0, 0);
            this.tblSites.Margin = new System.Windows.Forms.Padding(0);
            this.tblSites.Name = "tblSites";
            this.tblSites.RowCount = 3;
            this.tblSites.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblSites.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tblSites.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tblSites.Size = new System.Drawing.Size(300, 557);
            this.tblSites.TabIndex = 0;
            // 
            // chkDisplayActiveSites
            // 
            this.chkDisplayActiveSites.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.chkDisplayActiveSites.AutoSize = true;
            this.chkDisplayActiveSites.Location = new System.Drawing.Point(3, 496);
            this.chkDisplayActiveSites.Name = "chkDisplayActiveSites";
            this.chkDisplayActiveSites.Size = new System.Drawing.Size(169, 17);
            this.chkDisplayActiveSites.TabIndex = 0;
            this.chkDisplayActiveSites.Text = "Display Active Sites Only";
            this.chkDisplayActiveSites.UseVisualStyleBackColor = true;
            this.chkDisplayActiveSites.CheckedChanged += new System.EventHandler(this.chkDisplayActiveSites_CheckedChanged);
            // 
            // tblFilter
            // 
            this.tblFilter.ColumnCount = 2;
            this.tblFilter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblFilter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 93F));
            this.tblFilter.Controls.Add(this.cboSiteSearch, 0, 0);
            this.tblFilter.Controls.Add(this.btnFilter, 1, 0);
            this.tblFilter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblFilter.Location = new System.Drawing.Point(0, 522);
            this.tblFilter.Margin = new System.Windows.Forms.Padding(0);
            this.tblFilter.Name = "tblFilter";
            this.tblFilter.RowCount = 1;
            this.tblFilter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblFilter.Size = new System.Drawing.Size(300, 35);
            this.tblFilter.TabIndex = 1;
            // 
            // cboSiteSearch
            // 
            this.cboSiteSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cboSiteSearch.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboSiteSearch.FormattingEnabled = true;
            this.cboSiteSearch.Location = new System.Drawing.Point(3, 7);
            this.cboSiteSearch.Name = "cboSiteSearch";
            this.cboSiteSearch.Size = new System.Drawing.Size(201, 22);
            this.cboSiteSearch.TabIndex = 0;
            this.cboSiteSearch.SelectedIndexChanged += new System.EventHandler(this.cboSiteSearch_SelectedIndexChanged);
            this.cboSiteSearch.KeyUp += new System.Windows.Forms.KeyEventHandler(this.cboSiteSearch_KeyUp);
            // 
            // btnFilter
            // 
            this.btnFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFilter.Location = new System.Drawing.Point(210, 6);
            this.btnFilter.Name = "btnFilter";
            this.btnFilter.Size = new System.Drawing.Size(87, 23);
            this.btnFilter.TabIndex = 1;
            this.btnFilter.Text = "Filter";
            this.btnFilter.UseVisualStyleBackColor = true;
            this.btnFilter.Click += new System.EventHandler(this.btnFilter_Click);
            // 
            // tblDetails
            // 
            this.tblDetails.ColumnCount = 1;
            this.tblDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblDetails.Controls.Add(this.tabDetails2, 0, 1);
            this.tblDetails.Controls.Add(this.tabDetails, 0, 0);
            this.tblDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblDetails.Location = new System.Drawing.Point(303, 3);
            this.tblDetails.Name = "tblDetails";
            this.tblDetails.RowCount = 2;
            this.tblDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 255F));
            this.tblDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblDetails.Size = new System.Drawing.Size(719, 551);
            this.tblDetails.TabIndex = 0;
            // 
            // tabDetails2
            // 
            this.tabDetails2.Appearance = System.Windows.Forms.TabAppearance.Buttons;
            this.tabDetails2.Controls.Add(this.tbpDrop);
            this.tabDetails2.Controls.Add(this.tbpHourly);
            this.tabDetails2.Controls.Add(this.tbpAssets);
            this.tabDetails2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabDetails2.Location = new System.Drawing.Point(0, 255);
            this.tabDetails2.Margin = new System.Windows.Forms.Padding(0);
            this.tabDetails2.Name = "tabDetails2";
            this.tabDetails2.Padding = new System.Drawing.Point(3, 3);
            this.tabDetails2.SelectedIndex = 0;
            this.tabDetails2.Size = new System.Drawing.Size(719, 296);
            this.tabDetails2.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabDetails2.TabIndex = 1;
            this.tabDetails2.SelectedIndexChanged += new System.EventHandler(this.tabDetails2_SelectedIndexChanged);
            // 
            // tbpDrop
            // 
            this.tbpDrop.Location = new System.Drawing.Point(4, 25);
            this.tbpDrop.Margin = new System.Windows.Forms.Padding(0);
            this.tbpDrop.Name = "tbpDrop";
            this.tbpDrop.Size = new System.Drawing.Size(711, 267);
            this.tbpDrop.TabIndex = 0;
            this.tbpDrop.Text = "Drop";
            this.tbpDrop.UseVisualStyleBackColor = true;
            // 
            // tbpHourly
            // 
            this.tbpHourly.Location = new System.Drawing.Point(4, 25);
            this.tbpHourly.Margin = new System.Windows.Forms.Padding(0);
            this.tbpHourly.Name = "tbpHourly";
            this.tbpHourly.Size = new System.Drawing.Size(711, 267);
            this.tbpHourly.TabIndex = 1;
            this.tbpHourly.Text = "Hourly";
            this.tbpHourly.UseVisualStyleBackColor = true;
            // 
            // tbpAssets
            // 
            this.tbpAssets.Location = new System.Drawing.Point(4, 25);
            this.tbpAssets.Margin = new System.Windows.Forms.Padding(0);
            this.tbpAssets.Name = "tbpAssets";
            this.tbpAssets.Size = new System.Drawing.Size(711, 267);
            this.tbpAssets.TabIndex = 2;
            this.tbpAssets.Text = "Assets";
            this.tbpAssets.UseVisualStyleBackColor = true;
            // 
            // tabDetails
            // 
            this.tabDetails.Appearance = System.Windows.Forms.TabAppearance.Buttons;
            this.tabDetails.Controls.Add(this.tbpSiteDetails);
            this.tabDetails.Controls.Add(this.tbpInstallations);
            this.tabDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabDetails.Location = new System.Drawing.Point(0, 0);
            this.tabDetails.Margin = new System.Windows.Forms.Padding(0);
            this.tabDetails.Name = "tabDetails";
            this.tabDetails.Padding = new System.Drawing.Point(3, 3);
            this.tabDetails.SelectedIndex = 0;
            this.tabDetails.Size = new System.Drawing.Size(719, 255);
            this.tabDetails.TabIndex = 0;
            // 
            // tbpSiteDetails
            // 
            this.tbpSiteDetails.Location = new System.Drawing.Point(4, 25);
            this.tbpSiteDetails.Margin = new System.Windows.Forms.Padding(0);
            this.tbpSiteDetails.Name = "tbpSiteDetails";
            this.tbpSiteDetails.Size = new System.Drawing.Size(711, 226);
            this.tbpSiteDetails.TabIndex = 0;
            this.tbpSiteDetails.Text = "Site Details";
            this.tbpSiteDetails.UseVisualStyleBackColor = true;
            // 
            // tbpInstallations
            // 
            this.tbpInstallations.Location = new System.Drawing.Point(4, 25);
            this.tbpInstallations.Margin = new System.Windows.Forms.Padding(0);
            this.tbpInstallations.Name = "tbpInstallations";
            this.tbpInstallations.Size = new System.Drawing.Size(711, 226);
            this.tbpInstallations.TabIndex = 1;
            this.tbpInstallations.Text = "Current Installations";
            this.tbpInstallations.UseVisualStyleBackColor = true;
            // 
            // tblButtons
            // 
            this.tblButtons.ColumnCount = 14;
            this.tblButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tblButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tblButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tblButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tblButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tblButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tblButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 94F));
            this.tblButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 9F));
            this.tblButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 118F));
            this.tblButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tblButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tblButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 110F));
            this.tblButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 110F));
            this.tblButtons.Controls.Add(this.btnMovePrev, 2, 0);
            this.tblButtons.Controls.Add(this.btnMoveFirst, 1, 0);
            this.tblButtons.Controls.Add(this.btnTree, 0, 0);
            this.tblButtons.Controls.Add(this.btnMoveNext, 4, 0);
            this.tblButtons.Controls.Add(this.btnMoveLast, 5, 0);
            this.tblButtons.Controls.Add(this.cboSites, 3, 0);
            this.tblButtons.Controls.Add(this.btnExport, 12, 0);
            this.tblButtons.Controls.Add(this.btnClose, 13, 0);
            this.tblButtons.Controls.Add(this.chkDisplay, 6, 0);
            this.tblButtons.Controls.Add(this.btnCashierTransactions, 11, 0);
            this.tblButtons.Controls.Add(this.label1, 10, 0);
            this.tblButtons.Controls.Add(this.btnRefresh, 8, 0);
            this.tblButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblButtons.Location = new System.Drawing.Point(0, 563);
            this.tblButtons.Margin = new System.Windows.Forms.Padding(0);
            this.tblButtons.Name = "tblButtons";
            this.tblButtons.RowCount = 1;
            this.tblButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblButtons.Size = new System.Drawing.Size(1028, 40);
            this.tblButtons.TabIndex = 0;
            // 
            // btnMovePrev
            // 
            this.btnMovePrev.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMovePrev.ImageKey = "MovePrev.ico";
            this.btnMovePrev.ImageList = this.imglstSmallIcons;
            this.btnMovePrev.Location = new System.Drawing.Point(158, 8);
            this.btnMovePrev.Name = "btnMovePrev";
            this.btnMovePrev.Size = new System.Drawing.Size(29, 24);
            this.btnMovePrev.TabIndex = 2;
            this.btnMovePrev.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnMovePrev.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnMovePrev.UseVisualStyleBackColor = true;
            this.btnMovePrev.Click += new System.EventHandler(this.btnMovePrev_Click);
            // 
            // imglstSmallIcons
            // 
            this.imglstSmallIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imglstSmallIcons.ImageStream")));
            this.imglstSmallIcons.TransparentColor = System.Drawing.Color.Transparent;
            this.imglstSmallIcons.Images.SetKeyName(0, "Company.ico");
            this.imglstSmallIcons.Images.SetKeyName(1, "SubCompany.ico");
            this.imglstSmallIcons.Images.SetKeyName(2, "Site.ico");
            this.imglstSmallIcons.Images.SetKeyName(3, "MovePrev");
            this.imglstSmallIcons.Images.SetKeyName(4, "MoveNext");
            this.imglstSmallIcons.Images.SetKeyName(5, "MoveFirst.ico");
            this.imglstSmallIcons.Images.SetKeyName(6, "MovePrev.ico");
            this.imglstSmallIcons.Images.SetKeyName(7, "MoveNext.ico");
            this.imglstSmallIcons.Images.SetKeyName(8, "MoveLast.ico");
            // 
            // btnMoveFirst
            // 
            this.btnMoveFirst.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMoveFirst.ImageKey = "MoveFirst.ico";
            this.btnMoveFirst.ImageList = this.imglstSmallIcons;
            this.btnMoveFirst.Location = new System.Drawing.Point(123, 8);
            this.btnMoveFirst.Name = "btnMoveFirst";
            this.btnMoveFirst.Size = new System.Drawing.Size(29, 24);
            this.btnMoveFirst.TabIndex = 1;
            this.btnMoveFirst.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnMoveFirst.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnMoveFirst.UseVisualStyleBackColor = true;
            this.btnMoveFirst.Click += new System.EventHandler(this.btnMoveFirst_Click);
            // 
            // btnTree
            // 
            this.btnTree.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTree.ImageKey = "MovePrev";
            this.btnTree.ImageList = this.imglstSmallIcons;
            this.btnTree.Location = new System.Drawing.Point(3, 8);
            this.btnTree.Name = "btnTree";
            this.btnTree.Size = new System.Drawing.Size(114, 24);
            this.btnTree.TabIndex = 1;
            this.btnTree.Text = "Tree";
            this.btnTree.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnTree.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnTree.UseVisualStyleBackColor = true;
            this.btnTree.Click += new System.EventHandler(this.btnTree_Click);
            // 
            // btnMoveNext
            // 
            this.btnMoveNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMoveNext.ImageKey = "MoveNext.ico";
            this.btnMoveNext.ImageList = this.imglstSmallIcons;
            this.btnMoveNext.Location = new System.Drawing.Point(393, 8);
            this.btnMoveNext.Name = "btnMoveNext";
            this.btnMoveNext.Size = new System.Drawing.Size(29, 24);
            this.btnMoveNext.TabIndex = 4;
            this.btnMoveNext.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnMoveNext.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnMoveNext.UseVisualStyleBackColor = true;
            this.btnMoveNext.Click += new System.EventHandler(this.btnMoveNext_Click);
            // 
            // btnMoveLast
            // 
            this.btnMoveLast.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMoveLast.ImageKey = "MoveLast.ico";
            this.btnMoveLast.ImageList = this.imglstSmallIcons;
            this.btnMoveLast.Location = new System.Drawing.Point(428, 8);
            this.btnMoveLast.Name = "btnMoveLast";
            this.btnMoveLast.Size = new System.Drawing.Size(29, 24);
            this.btnMoveLast.TabIndex = 5;
            this.btnMoveLast.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnMoveLast.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnMoveLast.UseVisualStyleBackColor = true;
            this.btnMoveLast.Click += new System.EventHandler(this.btnMoveLast_Click);
            // 
            // cboSites
            // 
            this.cboSites.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cboSites.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboSites.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSites.FormattingEnabled = true;
            this.cboSites.Location = new System.Drawing.Point(193, 9);
            this.cboSites.Name = "cboSites";
            this.cboSites.Size = new System.Drawing.Size(194, 22);
            this.cboSites.TabIndex = 3;
            this.cboSites.SelectedIndexChanged += new System.EventHandler(this.cboSites_SelectedIndexChanged);
            // 
            // btnExport
            // 
            this.btnExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExport.ImageKey = "MovePrev";
            this.btnExport.Location = new System.Drawing.Point(811, 8);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(104, 24);
            this.btnExport.TabIndex = 9;
            this.btnExport.Text = "Export";
            this.btnExport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.ImageKey = "MovePrev";
            this.btnClose.Location = new System.Drawing.Point(921, 8);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(104, 24);
            this.btnClose.TabIndex = 10;
            this.btnClose.Text = "Close";
            this.btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // chkDisplay
            // 
            this.chkDisplay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.chkDisplay.AutoSize = true;
            this.chkDisplay.Location = new System.Drawing.Point(463, 11);
            this.chkDisplay.Name = "chkDisplay";
            this.chkDisplay.Size = new System.Drawing.Size(88, 17);
            this.chkDisplay.TabIndex = 6;
            this.chkDisplay.Text = "Display";
            this.chkDisplay.UseVisualStyleBackColor = true;
            this.chkDisplay.CheckedChanged += new System.EventHandler(this.chkDisplay_CheckedChanged);
            // 
            // btnCashierTransactions
            // 
            this.btnCashierTransactions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCashierTransactions.ImageKey = "MovePrev";
            this.btnCashierTransactions.Location = new System.Drawing.Point(691, 8);
            this.btnCashierTransactions.Name = "btnCashierTransactions";
            this.btnCashierTransactions.Size = new System.Drawing.Size(114, 24);
            this.btnCashierTransactions.TabIndex = 71;
            this.btnCashierTransactions.Text = "Cashier History";
            this.btnCashierTransactions.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCashierTransactions.UseVisualStyleBackColor = true;
            this.btnCashierTransactions.Click += new System.EventHandler(this.btnCashierTransactions_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(686, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1, 13);
            this.label1.TabIndex = 11;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefresh.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefresh.ImageKey = "MovePrev";
            this.btnRefresh.Location = new System.Drawing.Point(566, 3);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(112, 34);
            this.btnRefresh.TabIndex = 7;
            this.btnRefresh.Text = "Load";
            this.btnRefresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // ctxMenuCompany
            // 
            this.ctxMenuCompany.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ctxMenuItemCompany});
            this.ctxMenuCompany.Name = "ctxMenuCompany";
            this.ctxMenuCompany.Size = new System.Drawing.Size(209, 26);
            // 
            // ctxMenuItemCompany
            // 
            this.ctxMenuItemCompany.Name = "ctxMenuItemCompany";
            this.ctxMenuItemCompany.Size = new System.Drawing.Size(208, 22);
            this.ctxMenuItemCompany.Text = "Company Administration";
            this.ctxMenuItemCompany.Click += new System.EventHandler(this.ctxMenuItemCompany_Click);
            // 
            // ctxMenuSubCompany
            // 
            this.ctxMenuSubCompany.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ctxMenuItemSubCompany});
            this.ctxMenuSubCompany.Name = "ctxMnuCompany";
            this.ctxMenuSubCompany.Size = new System.Drawing.Size(232, 26);
            // 
            // ctxMenuItemSubCompany
            // 
            this.ctxMenuItemSubCompany.Name = "ctxMenuItemSubCompany";
            this.ctxMenuItemSubCompany.Size = new System.Drawing.Size(231, 22);
            this.ctxMenuItemSubCompany.Text = "Sub Company Administration";
            this.ctxMenuItemSubCompany.Click += new System.EventHandler(this.ctxMenuItemSubCompany_Click);
            // 
            // ctxMenuSite
            // 
            this.ctxMenuSite.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ctxMenuItemSite});
            this.ctxMenuSite.Name = "ctxMnuCompany";
            this.ctxMenuSite.Size = new System.Drawing.Size(176, 26);
            // 
            // ctxMenuItemSite
            // 
            this.ctxMenuItemSite.Name = "ctxMenuItemSite";
            this.ctxMenuItemSite.Size = new System.Drawing.Size(175, 22);
            this.ctxMenuItemSite.Text = "Site Administration";
            this.ctxMenuItemSite.Click += new System.EventHandler(this.ctxMenuItemSite_Click);
            // 
            // ViewSitesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(1028, 603);
            this.Controls.Add(this.tblContainer);
            this.Name = "ViewSitesForm";
            this.Text = "View Sites";
            this.Load += new System.EventHandler(this.ViewSitesForm_Load);
            this.tblContainer.ResumeLayout(false);
            this.tblContent.ResumeLayout(false);
            this.tblSites.ResumeLayout(false);
            this.tblSites.PerformLayout();
            this.tblFilter.ResumeLayout(false);
            this.tblDetails.ResumeLayout(false);
            this.tabDetails2.ResumeLayout(false);
            this.tabDetails.ResumeLayout(false);
            this.tblButtons.ResumeLayout(false);
            this.tblButtons.PerformLayout();
            this.ctxMenuCompany.ResumeLayout(false);
            this.ctxMenuSubCompany.ResumeLayout(false);
            this.ctxMenuSite.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tblContainer;
        private System.Windows.Forms.TableLayoutPanel tblContent;
        private System.Windows.Forms.TableLayoutPanel tblSites;
        private System.Windows.Forms.CheckBox chkDisplayActiveSites;
        private System.Windows.Forms.TableLayoutPanel tblFilter;
        private System.Windows.Forms.Button btnFilter;   
        private System.Windows.Forms.TableLayoutPanel tblDetails;
        private System.Windows.Forms.TabControl tabDetails;
        private System.Windows.Forms.TabPage tbpSiteDetails;
        private System.Windows.Forms.TabPage tbpInstallations;
        private System.Windows.Forms.TabControl tabDetails2;
        private System.Windows.Forms.TabPage tbpDrop;
        private System.Windows.Forms.TabPage tbpHourly;
        private System.Windows.Forms.TabPage tbpAssets;        
        private System.Windows.Forms.ImageList imglstSmallIcons;
        private System.Windows.Forms.TableLayoutPanel tblButtons;
        private System.Windows.Forms.Button btnTree;
        private System.Windows.Forms.Button btnMovePrev;
        private System.Windows.Forms.Button btnMoveFirst;
        private System.Windows.Forms.Button btnMoveNext;
        private System.Windows.Forms.Button btnMoveLast;
        private BmcComboBox cboSites;
        private System.Windows.Forms.Button btnCashierTransactions;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.CheckBox chkDisplay;
        private BmcComboBox cboSiteSearch;
        private System.Windows.Forms.ContextMenuStrip ctxMenuCompany;
        private System.Windows.Forms.ToolStripMenuItem ctxMenuItemCompany;
        private System.Windows.Forms.ContextMenuStrip ctxMenuSubCompany;
        private System.Windows.Forms.ToolStripMenuItem ctxMenuItemSubCompany;
        private System.Windows.Forms.ContextMenuStrip ctxMenuSite;
        private System.Windows.Forms.ToolStripMenuItem ctxMenuItemSite;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnRefresh;
    }
}