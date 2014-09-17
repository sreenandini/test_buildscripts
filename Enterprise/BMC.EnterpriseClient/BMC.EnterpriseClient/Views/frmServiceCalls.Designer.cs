namespace BMC.EnterpriseClient.Views
{
    partial class frmServiceCalls
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmServiceCalls));
            this.tblMain = new System.Windows.Forms.TableLayoutPanel();
            this.tblFooter = new System.Windows.Forms.TableLayoutPanel();
            this.flpnlFooter = new System.Windows.Forms.FlowLayoutPanel();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnExportCalls = new System.Windows.Forms.Button();
            this.btnPrintCalls = new System.Windows.Forms.Button();
            this.btnSiteHistory = new System.Windows.Forms.Button();
            this.btnCreateCall = new System.Windows.Forms.Button();
            this.tblInner = new System.Windows.Forms.TableLayoutPanel();
            this.uxDockPanel1 = new BMC.CoreLib.Win32.UxDockPanel();
            this.tblSearchFooter = new System.Windows.Forms.TableLayoutPanel();
            this.btnClearFilters = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.flpnlFilterCriteria = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlCallStatus = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbCallStatus = new System.Windows.Forms.ComboBox();
            this.pnlFaultGroup = new System.Windows.Forms.Panel();
            this.cmbFaultGroup = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.pnlFrom = new System.Windows.Forms.Panel();
            this.dtpFrom = new System.Windows.Forms.DateTimePicker();
            this.label8 = new System.Windows.Forms.Label();
            this.pnlTo = new System.Windows.Forms.Panel();
            this.dtpTo = new System.Windows.Forms.DateTimePicker();
            this.label9 = new System.Windows.Forms.Label();
            this.pnlRemedy = new System.Windows.Forms.Panel();
            this.cmbRemedy = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.pnlDepot = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.lstDepot = new System.Windows.Forms.ListBox();
            this.pnlFilterCriteriaSub = new System.Windows.Forms.Panel();
            this.tblSearchCriteria = new System.Windows.Forms.TableLayoutPanel();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbSubCompany = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtJob = new System.Windows.Forms.TextBox();
            this.txtAssetNo = new System.Windows.Forms.TextBox();
            this.lblAssetNo = new System.Windows.Forms.Label();
            this.lstEngineer = new System.Windows.Forms.ListBox();
            this.lstSite = new System.Windows.Forms.ListBox();
            this.pnlDataGridViews = new System.Windows.Forms.Panel();
            this.dgvClosedCalls = new System.Windows.Forms.DataGridView();
            this.dgvCurrentCalls = new System.Windows.Forms.DataGridView();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.tblMain.SuspendLayout();
            this.tblFooter.SuspendLayout();
            this.flpnlFooter.SuspendLayout();
            this.tblInner.SuspendLayout();
            this.uxDockPanel1.ChildContainer.SuspendLayout();
            this.tblSearchFooter.SuspendLayout();
            this.flpnlFilterCriteria.SuspendLayout();
            this.pnlCallStatus.SuspendLayout();
            this.pnlFaultGroup.SuspendLayout();
            this.pnlFrom.SuspendLayout();
            this.pnlTo.SuspendLayout();
            this.pnlRemedy.SuspendLayout();
            this.pnlDepot.SuspendLayout();
            this.pnlFilterCriteriaSub.SuspendLayout();
            this.tblSearchCriteria.SuspendLayout();
            this.pnlDataGridViews.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvClosedCalls)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCurrentCalls)).BeginInit();
            this.SuspendLayout();
            // 
            // tblMain
            // 
            this.tblMain.ColumnCount = 1;
            this.tblMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblMain.Controls.Add(this.tblFooter, 0, 1);
            this.tblMain.Controls.Add(this.tblInner, 0, 0);
            this.tblMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblMain.Location = new System.Drawing.Point(0, 0);
            this.tblMain.Name = "tblMain";
            this.tblMain.RowCount = 2;
            this.tblMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tblMain.Size = new System.Drawing.Size(988, 673);
            this.tblMain.TabIndex = 0;
            // 
            // tblFooter
            // 
            this.tblFooter.ColumnCount = 2;
            this.tblFooter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblFooter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 507F));
            this.tblFooter.Controls.Add(this.flpnlFooter, 1, 0);
            this.tblFooter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblFooter.Location = new System.Drawing.Point(0, 633);
            this.tblFooter.Margin = new System.Windows.Forms.Padding(0);
            this.tblFooter.Name = "tblFooter";
            this.tblFooter.RowCount = 1;
            this.tblFooter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblFooter.Size = new System.Drawing.Size(988, 40);
            this.tblFooter.TabIndex = 24;
            // 
            // flpnlFooter
            // 
            this.flpnlFooter.Controls.Add(this.btnClose);
            this.flpnlFooter.Controls.Add(this.btnExportCalls);
            this.flpnlFooter.Controls.Add(this.btnPrintCalls);
            this.flpnlFooter.Controls.Add(this.btnSiteHistory);
            this.flpnlFooter.Controls.Add(this.btnCreateCall);
            this.flpnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flpnlFooter.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flpnlFooter.Location = new System.Drawing.Point(484, 3);
            this.flpnlFooter.Name = "flpnlFooter";
            this.flpnlFooter.Size = new System.Drawing.Size(501, 34);
            this.flpnlFooter.TabIndex = 23;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(404, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(94, 28);
            this.btnClose.TabIndex = 22;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnExportCalls
            // 
            this.btnExportCalls.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExportCalls.Location = new System.Drawing.Point(304, 3);
            this.btnExportCalls.Name = "btnExportCalls";
            this.btnExportCalls.Size = new System.Drawing.Size(94, 28);
            this.btnExportCalls.TabIndex = 21;
            this.btnExportCalls.Text = "Export Call(s)";
            this.btnExportCalls.UseVisualStyleBackColor = true;
            this.btnExportCalls.Click += new System.EventHandler(this.btnExportCalls_Click);
            // 
            // btnPrintCalls
            // 
            this.btnPrintCalls.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrintCalls.Location = new System.Drawing.Point(204, 3);
            this.btnPrintCalls.Name = "btnPrintCalls";
            this.btnPrintCalls.Size = new System.Drawing.Size(94, 28);
            this.btnPrintCalls.TabIndex = 20;
            this.btnPrintCalls.Text = "Print Call(s)";
            this.btnPrintCalls.UseVisualStyleBackColor = true;
            this.btnPrintCalls.Click += new System.EventHandler(this.btnPrintCalls_Click);
            // 
            // btnSiteHistory
            // 
            this.btnSiteHistory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSiteHistory.Location = new System.Drawing.Point(104, 3);
            this.btnSiteHistory.Name = "btnSiteHistory";
            this.btnSiteHistory.Size = new System.Drawing.Size(94, 28);
            this.btnSiteHistory.TabIndex = 19;
            this.btnSiteHistory.Text = "Site History";
            this.btnSiteHistory.UseVisualStyleBackColor = true;
            this.btnSiteHistory.Click += new System.EventHandler(this.btnSiteHistory_Click);
            // 
            // btnCreateCall
            // 
            this.btnCreateCall.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCreateCall.Location = new System.Drawing.Point(4, 3);
            this.btnCreateCall.Name = "btnCreateCall";
            this.btnCreateCall.Size = new System.Drawing.Size(94, 28);
            this.btnCreateCall.TabIndex = 18;
            this.btnCreateCall.Text = "Create Call";
            this.btnCreateCall.UseVisualStyleBackColor = true;
            this.btnCreateCall.Click += new System.EventHandler(this.btnCreateCall_Click);
            // 
            // tblInner
            // 
            this.tblInner.ColumnCount = 2;
            this.tblInner.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 283F));
            this.tblInner.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblInner.Controls.Add(this.uxDockPanel1, 0, 0);
            this.tblInner.Controls.Add(this.pnlDataGridViews, 1, 0);
            this.tblInner.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblInner.Location = new System.Drawing.Point(0, 0);
            this.tblInner.Margin = new System.Windows.Forms.Padding(0);
            this.tblInner.Name = "tblInner";
            this.tblInner.RowCount = 1;
            this.tblInner.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblInner.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 633F));
            this.tblInner.Size = new System.Drawing.Size(988, 633);
            this.tblInner.TabIndex = 5;
            // 
            // uxDockPanel1
            // 
            this.uxDockPanel1.ActualWidth = 282;
            // 
            // uxDockPanel1.ChildContainer
            // 
            this.uxDockPanel1.ChildContainer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.uxDockPanel1.ChildContainer.Controls.Add(this.tblSearchFooter);
            this.uxDockPanel1.ChildContainer.Controls.Add(this.flpnlFilterCriteria);
            this.uxDockPanel1.ChildContainer.Dock = System.Windows.Forms.DockStyle.Left;
            this.uxDockPanel1.ChildContainer.Location = new System.Drawing.Point(3, 3);
            this.uxDockPanel1.ChildContainer.Margin = new System.Windows.Forms.Padding(0);
            this.uxDockPanel1.ChildContainer.Name = "ChildContainer";
            this.uxDockPanel1.ChildContainer.Size = new System.Drawing.Size(279, 601);
            this.uxDockPanel1.ChildContainer.TabIndex = 2;
            this.uxDockPanel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.uxDockPanel1.HeaderText = "Search Filter";
            this.uxDockPanel1.IsHidden = false;
            this.uxDockPanel1.Location = new System.Drawing.Point(0, 0);
            this.uxDockPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.uxDockPanel1.Name = "uxDockPanel1";
            this.uxDockPanel1.OwnerForm = this;
            this.uxDockPanel1.Size = new System.Drawing.Size(282, 633);
            this.uxDockPanel1.TabIndex = 0;
            this.uxDockPanel1.Resize += new System.EventHandler(this.uxDockPanel1_Resize);
            // 
            // tblSearchFooter
            // 
            this.tblSearchFooter.ColumnCount = 3;
            this.tblSearchFooter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblSearchFooter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tblSearchFooter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tblSearchFooter.Controls.Add(this.btnClearFilters, 1, 2);
            this.tblSearchFooter.Controls.Add(this.btnSearch, 2, 2);
            this.tblSearchFooter.Controls.Add(this.label11, 0, 1);
            this.tblSearchFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tblSearchFooter.Location = new System.Drawing.Point(0, 533);
            this.tblSearchFooter.Margin = new System.Windows.Forms.Padding(0);
            this.tblSearchFooter.Name = "tblSearchFooter";
            this.tblSearchFooter.RowCount = 3;
            this.tblSearchFooter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblSearchFooter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.tblSearchFooter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.tblSearchFooter.Size = new System.Drawing.Size(277, 66);
            this.tblSearchFooter.TabIndex = 13;
            // 
            // btnClearFilters
            // 
            this.btnClearFilters.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClearFilters.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.btnClearFilters.Location = new System.Drawing.Point(80, 35);
            this.btnClearFilters.Name = "btnClearFilters";
            this.btnClearFilters.Size = new System.Drawing.Size(94, 28);
            this.btnClearFilters.TabIndex = 15;
            this.btnClearFilters.Text = "Clear Filters";
            this.btnClearFilters.UseVisualStyleBackColor = true;
            this.btnClearFilters.Click += new System.EventHandler(this.btnClearFilters_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.btnSearch.Location = new System.Drawing.Point(180, 35);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(94, 28);
            this.btnSearch.TabIndex = 14;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.tblSearchFooter.SetColumnSpan(this.label11, 3);
            this.label11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label11.Location = new System.Drawing.Point(3, -2);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(271, 34);
            this.label11.TabIndex = 16;
            this.label11.Text = "* - Only 3 Entities can be selected. \r\nSelect --ANY-- to include all Entities.";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // flpnlFilterCriteria
            // 
            this.flpnlFilterCriteria.Controls.Add(this.pnlCallStatus);
            this.flpnlFilterCriteria.Controls.Add(this.pnlFaultGroup);
            this.flpnlFilterCriteria.Controls.Add(this.pnlFrom);
            this.flpnlFilterCriteria.Controls.Add(this.pnlTo);
            this.flpnlFilterCriteria.Controls.Add(this.pnlRemedy);
            this.flpnlFilterCriteria.Controls.Add(this.pnlDepot);
            this.flpnlFilterCriteria.Controls.Add(this.pnlFilterCriteriaSub);
            this.flpnlFilterCriteria.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpnlFilterCriteria.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpnlFilterCriteria.Location = new System.Drawing.Point(0, 0);
            this.flpnlFilterCriteria.Margin = new System.Windows.Forms.Padding(0);
            this.flpnlFilterCriteria.Name = "flpnlFilterCriteria";
            this.flpnlFilterCriteria.Size = new System.Drawing.Size(277, 599);
            this.flpnlFilterCriteria.TabIndex = 1;
            // 
            // pnlCallStatus
            // 
            this.pnlCallStatus.Controls.Add(this.label1);
            this.pnlCallStatus.Controls.Add(this.cmbCallStatus);
            this.pnlCallStatus.Location = new System.Drawing.Point(0, 0);
            this.pnlCallStatus.Margin = new System.Windows.Forms.Padding(0);
            this.pnlCallStatus.Name = "pnlCallStatus";
            this.pnlCallStatus.Size = new System.Drawing.Size(273, 35);
            this.pnlCallStatus.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Call Status:";
            // 
            // cmbCallStatus
            // 
            this.cmbCallStatus.FormattingEnabled = true;
            this.cmbCallStatus.Location = new System.Drawing.Point(102, 3);
            this.cmbCallStatus.Name = "cmbCallStatus";
            this.cmbCallStatus.Size = new System.Drawing.Size(168, 21);
            this.cmbCallStatus.TabIndex = 3;
            // 
            // pnlFaultGroup
            // 
            this.pnlFaultGroup.Controls.Add(this.cmbFaultGroup);
            this.pnlFaultGroup.Controls.Add(this.label2);
            this.pnlFaultGroup.Location = new System.Drawing.Point(0, 35);
            this.pnlFaultGroup.Margin = new System.Windows.Forms.Padding(0);
            this.pnlFaultGroup.Name = "pnlFaultGroup";
            this.pnlFaultGroup.Size = new System.Drawing.Size(273, 35);
            this.pnlFaultGroup.TabIndex = 1;
            // 
            // cmbFaultGroup
            // 
            this.cmbFaultGroup.FormattingEnabled = true;
            this.cmbFaultGroup.Location = new System.Drawing.Point(102, 3);
            this.cmbFaultGroup.Name = "cmbFaultGroup";
            this.cmbFaultGroup.Size = new System.Drawing.Size(168, 21);
            this.cmbFaultGroup.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Fault Group:";
            // 
            // pnlFrom
            // 
            this.pnlFrom.Controls.Add(this.dtpFrom);
            this.pnlFrom.Controls.Add(this.label8);
            this.pnlFrom.Location = new System.Drawing.Point(0, 70);
            this.pnlFrom.Margin = new System.Windows.Forms.Padding(0);
            this.pnlFrom.Name = "pnlFrom";
            this.pnlFrom.Size = new System.Drawing.Size(273, 35);
            this.pnlFrom.TabIndex = 2;
            // 
            // dtpFrom
            // 
            this.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFrom.Location = new System.Drawing.Point(102, 3);
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.Size = new System.Drawing.Size(168, 21);
            this.dtpFrom.TabIndex = 5;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(4, 7);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(41, 13);
            this.label8.TabIndex = 0;
            this.label8.Text = "From:";
            // 
            // pnlTo
            // 
            this.pnlTo.Controls.Add(this.dtpTo);
            this.pnlTo.Controls.Add(this.label9);
            this.pnlTo.Location = new System.Drawing.Point(0, 105);
            this.pnlTo.Margin = new System.Windows.Forms.Padding(0);
            this.pnlTo.Name = "pnlTo";
            this.pnlTo.Size = new System.Drawing.Size(273, 35);
            this.pnlTo.TabIndex = 3;
            // 
            // dtpTo
            // 
            this.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpTo.Location = new System.Drawing.Point(103, 4);
            this.dtpTo.Name = "dtpTo";
            this.dtpTo.Size = new System.Drawing.Size(168, 21);
            this.dtpTo.TabIndex = 6;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(4, 7);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(26, 13);
            this.label9.TabIndex = 0;
            this.label9.Text = "To:";
            // 
            // pnlRemedy
            // 
            this.pnlRemedy.Controls.Add(this.cmbRemedy);
            this.pnlRemedy.Controls.Add(this.label10);
            this.pnlRemedy.Location = new System.Drawing.Point(0, 140);
            this.pnlRemedy.Margin = new System.Windows.Forms.Padding(0);
            this.pnlRemedy.Name = "pnlRemedy";
            this.pnlRemedy.Size = new System.Drawing.Size(273, 35);
            this.pnlRemedy.TabIndex = 4;
            // 
            // cmbRemedy
            // 
            this.cmbRemedy.FormattingEnabled = true;
            this.cmbRemedy.Location = new System.Drawing.Point(102, 3);
            this.cmbRemedy.Name = "cmbRemedy";
            this.cmbRemedy.Size = new System.Drawing.Size(168, 21);
            this.cmbRemedy.TabIndex = 6;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(4, 7);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(59, 13);
            this.label10.TabIndex = 0;
            this.label10.Text = "Remedy:";
            // 
            // pnlDepot
            // 
            this.pnlDepot.Controls.Add(this.label3);
            this.pnlDepot.Controls.Add(this.lstDepot);
            this.pnlDepot.Location = new System.Drawing.Point(0, 175);
            this.pnlDepot.Margin = new System.Windows.Forms.Padding(0);
            this.pnlDepot.Name = "pnlDepot";
            this.pnlDepot.Size = new System.Drawing.Size(273, 85);
            this.pnlDepot.TabIndex = 8;
            this.pnlDepot.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 0);
            this.label3.Name = "label3";
            this.label3.Padding = new System.Windows.Forms.Padding(0, 7, 0, 0);
            this.label3.Size = new System.Drawing.Size(57, 20);
            this.label3.TabIndex = 4;
            this.label3.Text = "Depot: *";
            // 
            // lstDepot
            // 
            this.lstDepot.FormattingEnabled = true;
            this.lstDepot.Location = new System.Drawing.Point(102, 3);
            this.lstDepot.Name = "lstDepot";
            this.lstDepot.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.lstDepot.Size = new System.Drawing.Size(168, 69);
            this.lstDepot.TabIndex = 7;
            // 
            // pnlFilterCriteriaSub
            // 
            this.pnlFilterCriteriaSub.Controls.Add(this.tblSearchCriteria);
            this.pnlFilterCriteriaSub.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFilterCriteriaSub.Location = new System.Drawing.Point(0, 260);
            this.pnlFilterCriteriaSub.Margin = new System.Windows.Forms.Padding(0);
            this.pnlFilterCriteriaSub.Name = "pnlFilterCriteriaSub";
            this.pnlFilterCriteriaSub.Size = new System.Drawing.Size(273, 317);
            this.pnlFilterCriteriaSub.TabIndex = 5;
            // 
            // tblSearchCriteria
            // 
            this.tblSearchCriteria.ColumnCount = 2;
            this.tblSearchCriteria.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 99F));
            this.tblSearchCriteria.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 181F));
            this.tblSearchCriteria.Controls.Add(this.label4, 0, 0);
            this.tblSearchCriteria.Controls.Add(this.label5, 0, 1);
            this.tblSearchCriteria.Controls.Add(this.label6, 0, 2);
            this.tblSearchCriteria.Controls.Add(this.cmbSubCompany, 1, 2);
            this.tblSearchCriteria.Controls.Add(this.label7, 0, 3);
            this.tblSearchCriteria.Controls.Add(this.txtJob, 1, 3);
            this.tblSearchCriteria.Controls.Add(this.txtAssetNo, 1, 4);
            this.tblSearchCriteria.Controls.Add(this.lblAssetNo, 0, 4);
            this.tblSearchCriteria.Controls.Add(this.lstEngineer, 1, 0);
            this.tblSearchCriteria.Controls.Add(this.lstSite, 1, 1);
            this.tblSearchCriteria.Location = new System.Drawing.Point(0, 0);
            this.tblSearchCriteria.Margin = new System.Windows.Forms.Padding(0);
            this.tblSearchCriteria.Name = "tblSearchCriteria";
            this.tblSearchCriteria.RowCount = 6;
            this.tblSearchCriteria.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tblSearchCriteria.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tblSearchCriteria.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tblSearchCriteria.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tblSearchCriteria.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tblSearchCriteria.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tblSearchCriteria.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblSearchCriteria.Size = new System.Drawing.Size(273, 388);
            this.tblSearchCriteria.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 0);
            this.label4.Name = "label4";
            this.label4.Padding = new System.Windows.Forms.Padding(0, 7, 0, 0);
            this.label4.Size = new System.Drawing.Size(73, 20);
            this.label4.TabIndex = 6;
            this.label4.Text = "Engineer: *";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 80);
            this.label5.Name = "label5";
            this.label5.Padding = new System.Windows.Forms.Padding(0, 7, 0, 0);
            this.label5.Size = new System.Drawing.Size(45, 20);
            this.label5.TabIndex = 8;
            this.label5.Text = "Site: *";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 160);
            this.label6.Name = "label6";
            this.label6.Padding = new System.Windows.Forms.Padding(0, 7, 0, 0);
            this.label6.Size = new System.Drawing.Size(93, 20);
            this.label6.TabIndex = 10;
            this.label6.Text = "Sub Company:";
            // 
            // cmbSubCompany
            // 
            this.cmbSubCompany.FormattingEnabled = true;
            this.cmbSubCompany.Location = new System.Drawing.Point(102, 163);
            this.cmbSubCompany.Name = "cmbSubCompany";
            this.cmbSubCompany.Size = new System.Drawing.Size(169, 21);
            this.cmbSubCompany.TabIndex = 10;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 195);
            this.label7.Name = "label7";
            this.label7.Padding = new System.Windows.Forms.Padding(0, 7, 0, 0);
            this.label7.Size = new System.Drawing.Size(49, 20);
            this.label7.TabIndex = 12;
            this.label7.Text = "Job ID:";
            // 
            // txtJob
            // 
            this.txtJob.Location = new System.Drawing.Point(102, 198);
            this.txtJob.Name = "txtJob";
            this.txtJob.Size = new System.Drawing.Size(169, 21);
            this.txtJob.TabIndex = 11;
            this.txtJob.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtJob_KeyPress);
            // 
            // txtAssetNo
            // 
            this.txtAssetNo.Location = new System.Drawing.Point(102, 233);
            this.txtAssetNo.Name = "txtAssetNo";
            this.txtAssetNo.Size = new System.Drawing.Size(169, 21);
            this.txtAssetNo.TabIndex = 12;
            // 
            // lblAssetNo
            // 
            this.lblAssetNo.AutoSize = true;
            this.lblAssetNo.Location = new System.Drawing.Point(3, 230);
            this.lblAssetNo.Name = "lblAssetNo";
            this.lblAssetNo.Padding = new System.Windows.Forms.Padding(0, 7, 0, 0);
            this.lblAssetNo.Size = new System.Drawing.Size(62, 20);
            this.lblAssetNo.TabIndex = 15;
            this.lblAssetNo.Text = "Asset No:";
            // 
            // lstEngineer
            // 
            this.lstEngineer.FormattingEnabled = true;
            this.lstEngineer.Location = new System.Drawing.Point(102, 3);
            this.lstEngineer.Name = "lstEngineer";
            this.lstEngineer.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.lstEngineer.Size = new System.Drawing.Size(168, 69);
            this.lstEngineer.TabIndex = 8;
            this.lstEngineer.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lstEngineer_MouseClick);
            this.lstEngineer.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lstEngineer_MouseMove);
            // 
            // lstSite
            // 
            this.lstSite.FormattingEnabled = true;
            this.lstSite.Location = new System.Drawing.Point(102, 83);
            this.lstSite.Name = "lstSite";
            this.lstSite.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.lstSite.Size = new System.Drawing.Size(168, 69);
            this.lstSite.TabIndex = 9;
            this.lstSite.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lstSite_MouseClick);
            this.lstSite.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lstSite_MouseMove);
            // 
            // pnlDataGridViews
            // 
            this.pnlDataGridViews.Controls.Add(this.dgvClosedCalls);
            this.pnlDataGridViews.Controls.Add(this.dgvCurrentCalls);
            this.pnlDataGridViews.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlDataGridViews.Location = new System.Drawing.Point(286, 3);
            this.pnlDataGridViews.Name = "pnlDataGridViews";
            this.pnlDataGridViews.Size = new System.Drawing.Size(699, 627);
            this.pnlDataGridViews.TabIndex = 25;
            // 
            // dgvClosedCalls
            // 
            this.dgvClosedCalls.AllowUserToAddRows = false;
            this.dgvClosedCalls.AllowUserToDeleteRows = false;
            this.dgvClosedCalls.AllowUserToResizeRows = false;
            this.dgvClosedCalls.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dgvClosedCalls.BackgroundColor = System.Drawing.Color.White;
            this.dgvClosedCalls.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvClosedCalls.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvClosedCalls.EnableHeadersVisualStyles = false;
            this.dgvClosedCalls.Location = new System.Drawing.Point(0, 0);
            this.dgvClosedCalls.MultiSelect = false;
            this.dgvClosedCalls.Name = "dgvClosedCalls";
            this.dgvClosedCalls.ReadOnly = true;
            this.dgvClosedCalls.RowHeadersVisible = false;
            this.dgvClosedCalls.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvClosedCalls.Size = new System.Drawing.Size(699, 627);
            this.dgvClosedCalls.TabIndex = 27;
            this.dgvClosedCalls.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvClosedCalls_CellDoubleClick);
            // 
            // dgvCurrentCalls
            // 
            this.dgvCurrentCalls.AllowUserToAddRows = false;
            this.dgvCurrentCalls.AllowUserToDeleteRows = false;
            this.dgvCurrentCalls.AllowUserToResizeRows = false;
            this.dgvCurrentCalls.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dgvCurrentCalls.BackgroundColor = System.Drawing.Color.White;
            this.dgvCurrentCalls.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCurrentCalls.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCurrentCalls.EnableHeadersVisualStyles = false;
            this.dgvCurrentCalls.Location = new System.Drawing.Point(0, 0);
            this.dgvCurrentCalls.MultiSelect = false;
            this.dgvCurrentCalls.Name = "dgvCurrentCalls";
            this.dgvCurrentCalls.ReadOnly = true;
            this.dgvCurrentCalls.RowHeadersVisible = false;
            this.dgvCurrentCalls.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCurrentCalls.Size = new System.Drawing.Size(699, 627);
            this.dgvCurrentCalls.TabIndex = 26;
            this.dgvCurrentCalls.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCurrentCalls_CellDoubleClick);
            // 
            // frmServiceCalls
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(988, 673);
            this.Controls.Add(this.tblMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "frmServiceCalls";
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.frmServiceCalls_Load);
            this.tblMain.ResumeLayout(false);
            this.tblFooter.ResumeLayout(false);
            this.flpnlFooter.ResumeLayout(false);
            this.tblInner.ResumeLayout(false);
            this.uxDockPanel1.ChildContainer.ResumeLayout(false);
            this.tblSearchFooter.ResumeLayout(false);
            this.tblSearchFooter.PerformLayout();
            this.flpnlFilterCriteria.ResumeLayout(false);
            this.pnlCallStatus.ResumeLayout(false);
            this.pnlCallStatus.PerformLayout();
            this.pnlFaultGroup.ResumeLayout(false);
            this.pnlFaultGroup.PerformLayout();
            this.pnlFrom.ResumeLayout(false);
            this.pnlFrom.PerformLayout();
            this.pnlTo.ResumeLayout(false);
            this.pnlTo.PerformLayout();
            this.pnlRemedy.ResumeLayout(false);
            this.pnlRemedy.PerformLayout();
            this.pnlDepot.ResumeLayout(false);
            this.pnlDepot.PerformLayout();
            this.pnlFilterCriteriaSub.ResumeLayout(false);
            this.tblSearchCriteria.ResumeLayout(false);
            this.tblSearchCriteria.PerformLayout();
            this.pnlDataGridViews.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvClosedCalls)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCurrentCalls)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tblMain;
        private System.Windows.Forms.TableLayoutPanel tblFooter;
        private System.Windows.Forms.Button btnCreateCall;
        private System.Windows.Forms.Button btnSiteHistory;
        private System.Windows.Forms.Button btnExportCalls;
        private System.Windows.Forms.Button btnPrintCalls;
        private System.Windows.Forms.Button btnClose;
        private CoreLib.Win32.UxDockPanel uxDockPanel1;
        private System.Windows.Forms.TableLayoutPanel tblSearchCriteria;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbFaultGroup;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmbSubCompany;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtJob;
        private System.Windows.Forms.FlowLayoutPanel flpnlFilterCriteria;
        private System.Windows.Forms.ComboBox cmbCallStatus;
        private System.Windows.Forms.Panel pnlCallStatus;
        private System.Windows.Forms.Panel pnlFaultGroup;
        private System.Windows.Forms.Panel pnlFrom;
        private System.Windows.Forms.DateTimePicker dtpFrom;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Panel pnlTo;
        private System.Windows.Forms.DateTimePicker dtpTo;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Panel pnlRemedy;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cmbRemedy;
        private System.Windows.Forms.Label lblAssetNo;
        private System.Windows.Forms.TextBox txtAssetNo;
        private System.Windows.Forms.TableLayoutPanel tblSearchFooter;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnClearFilters;
        private System.Windows.Forms.TableLayoutPanel tblInner;
        private System.Windows.Forms.Panel pnlFilterCriteriaSub;
        private System.Windows.Forms.FlowLayoutPanel flpnlFooter;
        private System.Windows.Forms.Panel pnlDataGridViews;
        private System.Windows.Forms.DataGridView dgvClosedCalls;
        private System.Windows.Forms.DataGridView dgvCurrentCalls;
        private System.Windows.Forms.ListBox lstEngineer;
        private System.Windows.Forms.ListBox lstSite;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Panel pnlDepot;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox lstDepot;
    }
}