namespace BMC.EnterpriseClient.Views
{
    partial class frmPeriodEndTermsProcessor
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnValidateCollections = new System.Windows.Forms.Button();
            this.btnProcess = new System.Windows.Forms.Button();
            this.btnInterimWeeklyLiquidation = new System.Windows.Forms.Button();
            this.btnConfirmWeeklyLiquidation = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.lvSubCompanyCollectionSummary = new BMC.CoreLib.Win32.ListViewEx();
            this.colTermsGroupId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colSubCompany = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colPeriodStart = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colPeriodEnd = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colTotalNet = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colSubCompanyScheduleId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colSubCompanyScheduleName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colSubCompanySitePercent = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colSubCompanyOperatorPercent = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colSubCompanyPercent = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lblSubCompanyCollectionSummary = new System.Windows.Forms.Label();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.lvAvailableSchedules = new BMC.CoreLib.Win32.ListViewEx();
            this.colScheduleId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colScheduleName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colSitePercent = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colOperatorPercent = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colCompanyPercent = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lblAvailableSchedules = new System.Windows.Forms.Label();
            this.tableLayoutPanel7 = new System.Windows.Forms.TableLayoutPanel();
            this.btnApply = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.tableLayoutPanel8 = new System.Windows.Forms.TableLayoutPanel();
            this.lvCollectionExceptions = new BMC.CoreLib.Win32.ListViewEx();
            this.colSiteId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colSiteName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colSiteCode = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colCashIn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colCashOut = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colNet = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colCollectionNumbers = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colMachineNumbers = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colBills = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colVoucherIn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colVoucherOut = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colAttendantPay = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colBatchesNumbers = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colWeekId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colBatchId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colWeekNumber = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colWeekStartDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colWeekEndDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lblCollectionExceptions = new System.Windows.Forms.Label();
            this.ctxMenuListViewCollections = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.requestBatchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            this.tableLayoutPanel7.SuspendLayout();
            this.tableLayoutPanel8.SuspendLayout();
            this.ctxMenuListViewCollections.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1503, 673);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 7;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel2.Controls.Add(this.btnClose, 6, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnValidateCollections, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnProcess, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnInterimWeeklyLiquidation, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnConfirmWeeklyLiquidation, 4, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnRefresh, 5, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 626);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1497, 44);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(1380, 8);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(114, 28);
            this.btnClose.TabIndex = 4;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnValidateCollections
            // 
            this.btnValidateCollections.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnValidateCollections.Location = new System.Drawing.Point(690, 8);
            this.btnValidateCollections.Name = "btnValidateCollections";
            this.btnValidateCollections.Size = new System.Drawing.Size(144, 28);
            this.btnValidateCollections.TabIndex = 0;
            this.btnValidateCollections.Text = "Validate Collections";
            this.btnValidateCollections.UseVisualStyleBackColor = true;
            this.btnValidateCollections.Click += new System.EventHandler(this.btnValidateCollections_Click);
            // 
            // btnProcess
            // 
            this.btnProcess.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnProcess.Location = new System.Drawing.Point(840, 8);
            this.btnProcess.Name = "btnProcess";
            this.btnProcess.Size = new System.Drawing.Size(114, 28);
            this.btnProcess.TabIndex = 1;
            this.btnProcess.Text = "Process";
            this.btnProcess.UseVisualStyleBackColor = true;
            this.btnProcess.Click += new System.EventHandler(this.btnProcess_Click);
            // 
            // btnInterimWeeklyLiquidation
            // 
            this.btnInterimWeeklyLiquidation.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnInterimWeeklyLiquidation.Location = new System.Drawing.Point(960, 8);
            this.btnInterimWeeklyLiquidation.Name = "btnInterimWeeklyLiquidation";
            this.btnInterimWeeklyLiquidation.Size = new System.Drawing.Size(144, 28);
            this.btnInterimWeeklyLiquidation.TabIndex = 2;
            this.btnInterimWeeklyLiquidation.Text = "Interim Weekly Liquidation";
            this.btnInterimWeeklyLiquidation.UseVisualStyleBackColor = true;
            this.btnInterimWeeklyLiquidation.Click += new System.EventHandler(this.btnInterimWeeklyLiquidation_Click);
            // 
            // btnConfirmWeeklyLiquidation
            // 
            this.btnConfirmWeeklyLiquidation.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConfirmWeeklyLiquidation.Location = new System.Drawing.Point(1110, 8);
            this.btnConfirmWeeklyLiquidation.Name = "btnConfirmWeeklyLiquidation";
            this.btnConfirmWeeklyLiquidation.Size = new System.Drawing.Size(144, 28);
            this.btnConfirmWeeklyLiquidation.TabIndex = 3;
            this.btnConfirmWeeklyLiquidation.Text = "Confirm Weekly Liquidation";
            this.btnConfirmWeeklyLiquidation.UseVisualStyleBackColor = true;
            this.btnConfirmWeeklyLiquidation.Click += new System.EventHandler(this.btnConfirmWeeklyLiquidation_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefresh.Location = new System.Drawing.Point(1260, 8);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(114, 28);
            this.btnRefresh.TabIndex = 5;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel4, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel8, 0, 1);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(1497, 617);
            this.tableLayoutPanel3.TabIndex = 1;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 3;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Controls.Add(this.tableLayoutPanel5, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.tableLayoutPanel6, 2, 0);
            this.tableLayoutPanel4.Controls.Add(this.tableLayoutPanel7, 1, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(1491, 302);
            this.tableLayoutPanel4.TabIndex = 0;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 1;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.Controls.Add(this.lvSubCompanyCollectionSummary, 0, 1);
            this.tableLayoutPanel5.Controls.Add(this.lblSubCompanyCollectionSummary, 0, 0);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 2;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(679, 296);
            this.tableLayoutPanel5.TabIndex = 0;
            // 
            // lvSubCompanyCollectionSummary
            // 
            this.lvSubCompanyCollectionSummary.ClipboardCopyFormat = BMC.CoreLib.Win32.ListViewClipboardCopyFormat.Default;
            this.lvSubCompanyCollectionSummary.ClipboardCopyMode = BMC.CoreLib.Win32.ListViewClipboardCopyMode.Disable;
            this.lvSubCompanyCollectionSummary.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colTermsGroupId,
            this.colSubCompany,
            this.colPeriodStart,
            this.colPeriodEnd,
            this.colTotalNet,
            this.colSubCompanyScheduleId,
            this.colSubCompanyScheduleName,
            this.colSubCompanySitePercent,
            this.colSubCompanyOperatorPercent,
            this.colSubCompanyPercent});
            this.lvSubCompanyCollectionSummary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvSubCompanyCollectionSummary.FullRowSelect = true;
            this.lvSubCompanyCollectionSummary.GridLines = true;
            this.lvSubCompanyCollectionSummary.HideSelection = false;
            this.lvSubCompanyCollectionSummary.Location = new System.Drawing.Point(3, 23);
            this.lvSubCompanyCollectionSummary.Name = "lvSubCompanyCollectionSummary";
            this.lvSubCompanyCollectionSummary.Size = new System.Drawing.Size(673, 270);
            this.lvSubCompanyCollectionSummary.TabIndex = 2;
            this.lvSubCompanyCollectionSummary.UseCompatibleStateImageBehavior = false;
            this.lvSubCompanyCollectionSummary.View = System.Windows.Forms.View.Details;
            this.lvSubCompanyCollectionSummary.SelectedIndexChanged += new System.EventHandler(this.lvSubCompanyCollectionSummary_SelectedIndexChanged);
            // 
            // colTermsGroupId
            // 
            this.colTermsGroupId.Tag = "0|colTermsGroupId";
            this.colTermsGroupId.Text = "Terms Group ID";
            this.colTermsGroupId.Width = 0;
            // 
            // colSubCompany
            // 
            this.colSubCompany.Tag = "2|colSubCompany";
            this.colSubCompany.Text = "Sub Comp";
            // 
            // colPeriodStart
            // 
            this.colPeriodStart.Tag = "1|colPeriodStart";
            this.colPeriodStart.Text = "Per. Start";
            // 
            // colPeriodEnd
            // 
            this.colPeriodEnd.Tag = "1|colPeriodEnd";
            this.colPeriodEnd.Text = "Per. End";
            // 
            // colTotalNet
            // 
            this.colTotalNet.Tag = "1|colTotalNet";
            this.colTotalNet.Text = "Total Net";
            this.colTotalNet.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // colSubCompanyScheduleId
            // 
            this.colSubCompanyScheduleId.Tag = "0|colSubCompanyScheduleId";
            this.colSubCompanyScheduleId.Text = "Sub Company Schedule Id";
            this.colSubCompanyScheduleId.Width = 0;
            // 
            // colSubCompanyScheduleName
            // 
            this.colSubCompanyScheduleName.Tag = "2|colSubCompanyScheduleName";
            this.colSubCompanyScheduleName.Text = "Schedule";
            // 
            // colSubCompanySitePercent
            // 
            this.colSubCompanySitePercent.Tag = "1|colSubCompanySitePercent";
            this.colSubCompanySitePercent.Text = "Site %";
            // 
            // colSubCompanyOperatorPercent
            // 
            this.colSubCompanyOperatorPercent.Tag = "1|colSubCompanyOperatorPercent";
            this.colSubCompanyOperatorPercent.Text = "Oper %";
            // 
            // colSubCompanyPercent
            // 
            this.colSubCompanyPercent.Tag = "1|colSubCompanyPercent";
            this.colSubCompanyPercent.Text = "Comp %";
            // 
            // lblSubCompanyCollectionSummary
            // 
            this.lblSubCompanyCollectionSummary.AutoSize = true;
            this.lblSubCompanyCollectionSummary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSubCompanyCollectionSummary.Location = new System.Drawing.Point(3, 0);
            this.lblSubCompanyCollectionSummary.Name = "lblSubCompanyCollectionSummary";
            this.lblSubCompanyCollectionSummary.Size = new System.Drawing.Size(673, 20);
            this.lblSubCompanyCollectionSummary.TabIndex = 0;
            this.lblSubCompanyCollectionSummary.Text = "Sub Company Collection Summary";
            this.lblSubCompanyCollectionSummary.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.ColumnCount = 1;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel6.Controls.Add(this.lvAvailableSchedules, 0, 1);
            this.tableLayoutPanel6.Controls.Add(this.lblAvailableSchedules, 0, 0);
            this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel6.Location = new System.Drawing.Point(808, 3);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 2;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(680, 296);
            this.tableLayoutPanel6.TabIndex = 1;
            // 
            // lvAvailableSchedules
            // 
            this.lvAvailableSchedules.ClipboardCopyFormat = BMC.CoreLib.Win32.ListViewClipboardCopyFormat.Default;
            this.lvAvailableSchedules.ClipboardCopyMode = BMC.CoreLib.Win32.ListViewClipboardCopyMode.Disable;
            this.lvAvailableSchedules.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colScheduleId,
            this.colScheduleName,
            this.colSitePercent,
            this.colOperatorPercent,
            this.colCompanyPercent});
            this.lvAvailableSchedules.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvAvailableSchedules.FullRowSelect = true;
            this.lvAvailableSchedules.GridLines = true;
            this.lvAvailableSchedules.HideSelection = false;
            this.lvAvailableSchedules.Location = new System.Drawing.Point(3, 23);
            this.lvAvailableSchedules.Name = "lvAvailableSchedules";
            this.lvAvailableSchedules.Size = new System.Drawing.Size(674, 270);
            this.lvAvailableSchedules.TabIndex = 2;
            this.lvAvailableSchedules.UseCompatibleStateImageBehavior = false;
            this.lvAvailableSchedules.View = System.Windows.Forms.View.Details;
            // 
            // colScheduleId
            // 
            this.colScheduleId.Tag = "0|colScheduleId";
            this.colScheduleId.Text = "ScheduleId";
            this.colScheduleId.Width = 0;
            // 
            // colScheduleName
            // 
            this.colScheduleName.Tag = "2|colScheduleName";
            this.colScheduleName.Text = "Schedule Name";
            // 
            // colSitePercent
            // 
            this.colSitePercent.Tag = "1|colSitePercent";
            this.colSitePercent.Text = "Site %";
            // 
            // colOperatorPercent
            // 
            this.colOperatorPercent.Tag = "1|colOperatorPercent";
            this.colOperatorPercent.Text = "Oper %";
            // 
            // colCompanyPercent
            // 
            this.colCompanyPercent.Tag = "1|colCompanyPercent";
            this.colCompanyPercent.Text = "Company %";
            this.colCompanyPercent.Width = 100;
            // 
            // lblAvailableSchedules
            // 
            this.lblAvailableSchedules.AutoSize = true;
            this.lblAvailableSchedules.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblAvailableSchedules.Location = new System.Drawing.Point(3, 0);
            this.lblAvailableSchedules.Name = "lblAvailableSchedules";
            this.lblAvailableSchedules.Size = new System.Drawing.Size(674, 20);
            this.lblAvailableSchedules.TabIndex = 0;
            this.lblAvailableSchedules.Text = "Available Schedules";
            this.lblAvailableSchedules.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel7
            // 
            this.tableLayoutPanel7.ColumnCount = 1;
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel7.Controls.Add(this.btnApply, 0, 1);
            this.tableLayoutPanel7.Controls.Add(this.btnClear, 0, 2);
            this.tableLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel7.Location = new System.Drawing.Point(688, 3);
            this.tableLayoutPanel7.Name = "tableLayoutPanel7";
            this.tableLayoutPanel7.RowCount = 4;
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel7.Size = new System.Drawing.Size(114, 296);
            this.tableLayoutPanel7.TabIndex = 2;
            // 
            // btnApply
            // 
            this.btnApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnApply.Location = new System.Drawing.Point(3, 114);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(108, 28);
            this.btnApply.TabIndex = 0;
            this.btnApply.Text = "<< Apply";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // btnClear
            // 
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClear.Location = new System.Drawing.Point(3, 154);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(108, 28);
            this.btnClear.TabIndex = 1;
            this.btnClear.Text = "Clear >>";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // tableLayoutPanel8
            // 
            this.tableLayoutPanel8.ColumnCount = 1;
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel8.Controls.Add(this.lvCollectionExceptions, 0, 1);
            this.tableLayoutPanel8.Controls.Add(this.lblCollectionExceptions, 0, 0);
            this.tableLayoutPanel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel8.Location = new System.Drawing.Point(3, 311);
            this.tableLayoutPanel8.Name = "tableLayoutPanel8";
            this.tableLayoutPanel8.RowCount = 2;
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel8.Size = new System.Drawing.Size(1491, 303);
            this.tableLayoutPanel8.TabIndex = 1;
            // 
            // lvCollectionExceptions
            // 
            this.lvCollectionExceptions.ClipboardCopyFormat = BMC.CoreLib.Win32.ListViewClipboardCopyFormat.Default;
            this.lvCollectionExceptions.ClipboardCopyMode = BMC.CoreLib.Win32.ListViewClipboardCopyMode.Disable;
            this.lvCollectionExceptions.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colSiteId,
            this.colSiteName,
            this.colSiteCode,
            this.colCashIn,
            this.colCashOut,
            this.colNet,
            this.colCollectionNumbers,
            this.colMachineNumbers,
            this.colBills,
            this.colVoucherIn,
            this.colVoucherOut,
            this.colAttendantPay,
            this.colBatchesNumbers,
            this.colWeekId,
            this.colBatchId,
            this.colWeekNumber,
            this.colWeekStartDate,
            this.colWeekEndDate});
            this.lvCollectionExceptions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvCollectionExceptions.FullRowSelect = true;
            this.lvCollectionExceptions.GridLines = true;
            this.lvCollectionExceptions.HideSelection = false;
            this.lvCollectionExceptions.Location = new System.Drawing.Point(3, 23);
            this.lvCollectionExceptions.Name = "lvCollectionExceptions";
            this.lvCollectionExceptions.Size = new System.Drawing.Size(1485, 277);
            this.lvCollectionExceptions.TabIndex = 2;
            this.lvCollectionExceptions.UseCompatibleStateImageBehavior = false;
            this.lvCollectionExceptions.View = System.Windows.Forms.View.Details;
            this.lvCollectionExceptions.DrawItem += new System.Windows.Forms.DrawListViewItemEventHandler(this.lvCollectionExceptions_DrawItem);
            this.lvCollectionExceptions.DoubleClick += new System.EventHandler(this.lvCollectionExceptions_DoubleClick);
            this.lvCollectionExceptions.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lvCollectionExceptions_MouseClick);
            // 
            // colSiteId
            // 
            this.colSiteId.Tag = "0|colSiteId";
            this.colSiteId.Text = "Site ID";
            this.colSiteId.Width = 0;
            // 
            // colSiteName
            // 
            this.colSiteName.Tag = "2|colSiteName";
            this.colSiteName.Text = "Site";
            // 
            // colSiteCode
            // 
            this.colSiteCode.Tag = "0|colSiteCode";
            this.colSiteCode.Text = "Site Code";
            this.colSiteCode.Width = 0;
            // 
            // colCashIn
            // 
            this.colCashIn.Tag = "1|colCashIn";
            this.colCashIn.Text = "Cash In";
            this.colCashIn.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // colCashOut
            // 
            this.colCashOut.Tag = "1|colCashOut";
            this.colCashOut.Text = "Cash Out";
            this.colCashOut.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // colNet
            // 
            this.colNet.Tag = "1|colNet";
            this.colNet.Text = "Net";
            this.colNet.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // colCollectionNumbers
            // 
            this.colCollectionNumbers.Tag = "1|colCollectionNumbers";
            this.colCollectionNumbers.Text = "# Colls";
            this.colCollectionNumbers.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // colMachineNumbers
            // 
            this.colMachineNumbers.Tag = "1|colMachineNumbers";
            this.colMachineNumbers.Text = "# M/C\'s";
            this.colMachineNumbers.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // colBills
            // 
            this.colBills.Tag = "1|colBills";
            this.colBills.Text = "Bills";
            this.colBills.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // colVoucherIn
            // 
            this.colVoucherIn.Tag = "1|colVoucherIn";
            this.colVoucherIn.Text = "Voucher In";
            this.colVoucherIn.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // colVoucherOut
            // 
            this.colVoucherOut.Tag = "1|colVoucherOut";
            this.colVoucherOut.Text = "Voucher Out";
            this.colVoucherOut.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // colAttendantPay
            // 
            this.colAttendantPay.Tag = "1|colAttendantPay";
            this.colAttendantPay.Text = "AttendantPay";
            this.colAttendantPay.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // colBatchesNumbers
            // 
            this.colBatchesNumbers.Tag = "1|colBatchesNumbers";
            this.colBatchesNumbers.Text = "# Batches";
            this.colBatchesNumbers.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // colWeekId
            // 
            this.colWeekId.Tag = "0|colWeekId";
            this.colWeekId.Text = "Week ID";
            this.colWeekId.Width = 0;
            // 
            // colBatchId
            // 
            this.colBatchId.Tag = "0|colBatchId";
            this.colBatchId.Text = "Batch ID";
            this.colBatchId.Width = 0;
            // 
            // colWeekNumber
            // 
            this.colWeekNumber.Tag = "0|colWeekNumber";
            this.colWeekNumber.Text = "Week No";
            this.colWeekNumber.Width = 0;
            // 
            // colWeekStartDate
            // 
            this.colWeekStartDate.Tag = "0|colWeekStartDate";
            this.colWeekStartDate.Text = "Week Start Date";
            this.colWeekStartDate.Width = 0;
            // 
            // colWeekEndDate
            // 
            this.colWeekEndDate.Tag = "0|colWeekEndDate";
            this.colWeekEndDate.Text = "Week End Date";
            this.colWeekEndDate.Width = 0;
            // 
            // lblCollectionExceptions
            // 
            this.lblCollectionExceptions.AutoSize = true;
            this.lblCollectionExceptions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCollectionExceptions.Location = new System.Drawing.Point(3, 0);
            this.lblCollectionExceptions.Name = "lblCollectionExceptions";
            this.lblCollectionExceptions.Size = new System.Drawing.Size(1485, 20);
            this.lblCollectionExceptions.TabIndex = 0;
            this.lblCollectionExceptions.Text = "Collection Exceptions";
            // 
            // ctxMenuListViewCollections
            // 
            this.ctxMenuListViewCollections.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.requestBatchToolStripMenuItem});
            this.ctxMenuListViewCollections.Name = "ctxMenuListViewCollections";
            this.ctxMenuListViewCollections.Size = new System.Drawing.Size(260, 26);
            // 
            // requestBatchToolStripMenuItem
            // 
            this.requestBatchToolStripMenuItem.Name = "requestBatchToolStripMenuItem";
            this.requestBatchToolStripMenuItem.Size = new System.Drawing.Size(259, 22);
            this.requestBatchToolStripMenuItem.Text = "Request Batch Process for this Item";
            this.requestBatchToolStripMenuItem.Click += new System.EventHandler(this.requestBatchToolStripMenuItem_Click);
            // 
            // frmPeriodEndTermsProcessor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1503, 673);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmPeriodEndTermsProcessor";
            this.Text = "Period End Terms Processor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmPeriodEndTermsProcessor_FormClosing);
            this.Load += new System.EventHandler(this.frmPeriodEndTermsProcessor_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            this.tableLayoutPanel6.ResumeLayout(false);
            this.tableLayoutPanel6.PerformLayout();
            this.tableLayoutPanel7.ResumeLayout(false);
            this.tableLayoutPanel8.ResumeLayout(false);
            this.tableLayoutPanel8.PerformLayout();
            this.ctxMenuListViewCollections.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button btnValidateCollections;
        private System.Windows.Forms.Button btnProcess;
        private System.Windows.Forms.Button btnInterimWeeklyLiquidation;
        private System.Windows.Forms.Button btnConfirmWeeklyLiquidation;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel7;
        private System.Windows.Forms.Label lblSubCompanyCollectionSummary;
        private System.Windows.Forms.Label lblAvailableSchedules;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel8;
        private System.Windows.Forms.Label lblCollectionExceptions;
        private System.Windows.Forms.ContextMenuStrip ctxMenuListViewCollections;
        private System.Windows.Forms.ToolStripMenuItem requestBatchToolStripMenuItem;
        private System.Windows.Forms.Button btnClose;
        private CoreLib.Win32.ListViewEx lvSubCompanyCollectionSummary;
        private System.Windows.Forms.ColumnHeader colTermsGroupId;
        private System.Windows.Forms.ColumnHeader colSubCompany;
        private System.Windows.Forms.ColumnHeader colPeriodStart;
        private System.Windows.Forms.ColumnHeader colPeriodEnd;
        private System.Windows.Forms.ColumnHeader colTotalNet;
        private System.Windows.Forms.ColumnHeader colSubCompanyScheduleId;
        private System.Windows.Forms.ColumnHeader colSubCompanyScheduleName;
        private System.Windows.Forms.ColumnHeader colSubCompanySitePercent;
        private System.Windows.Forms.ColumnHeader colSubCompanyOperatorPercent;
        private System.Windows.Forms.ColumnHeader colSubCompanyPercent;
        private CoreLib.Win32.ListViewEx lvAvailableSchedules;
        private System.Windows.Forms.ColumnHeader colScheduleId;
        private System.Windows.Forms.ColumnHeader colScheduleName;
        private System.Windows.Forms.ColumnHeader colSitePercent;
        private System.Windows.Forms.ColumnHeader colOperatorPercent;
        private System.Windows.Forms.ColumnHeader colCompanyPercent;
        private CoreLib.Win32.ListViewEx lvCollectionExceptions;
        private System.Windows.Forms.ColumnHeader colSiteId;
        private System.Windows.Forms.ColumnHeader colSiteName;
        private System.Windows.Forms.ColumnHeader colSiteCode;
        private System.Windows.Forms.ColumnHeader colCashIn;
        private System.Windows.Forms.ColumnHeader colCashOut;
        private System.Windows.Forms.ColumnHeader colNet;
        private System.Windows.Forms.ColumnHeader colCollectionNumbers;
        private System.Windows.Forms.ColumnHeader colMachineNumbers;
        private System.Windows.Forms.ColumnHeader colBills;
        private System.Windows.Forms.ColumnHeader colVoucherIn;
        private System.Windows.Forms.ColumnHeader colVoucherOut;
        private System.Windows.Forms.ColumnHeader colAttendantPay;
        private System.Windows.Forms.ColumnHeader colBatchesNumbers;
        private System.Windows.Forms.ColumnHeader colWeekId;
        private System.Windows.Forms.ColumnHeader colBatchId;
        private System.Windows.Forms.ColumnHeader colWeekNumber;
        private System.Windows.Forms.ColumnHeader colWeekStartDate;
        private System.Windows.Forms.ColumnHeader colWeekEndDate;
        private System.Windows.Forms.Button btnRefresh;

    }
}