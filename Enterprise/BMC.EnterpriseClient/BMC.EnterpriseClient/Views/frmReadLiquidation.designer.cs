namespace BMC.EnterpriseClient.Views
{
    partial class frmReadLiquidation
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
            this.grpLiquidationDetails = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lblDate = new System.Windows.Forms.Label();
            this.lblRetailerName = new System.Windows.Forms.Label();
            this.lblGross = new System.Windows.Forms.Label();
            this.lblTicketsExpected = new System.Windows.Forms.Label();
            this.lblNet = new System.Windows.Forms.Label();
            this.lblNetValue = new System.Windows.Forms.Label();
            this.lblRetailersNegativeNet = new System.Windows.Forms.Label();
            this.lblRetailerShare = new System.Windows.Forms.Label();
            this.lblTicketsPaid = new System.Windows.Forms.Label();
            this.lblRetailer = new System.Windows.Forms.Label();
            this.txtDate = new System.Windows.Forms.TextBox();
            this.txtRetailerName = new System.Windows.Forms.TextBox();
            this.txtGross = new System.Windows.Forms.TextBox();
            this.txtTicketsExpected = new System.Windows.Forms.TextBox();
            this.txtNet = new System.Windows.Forms.TextBox();
            this.txtNetValue = new System.Windows.Forms.TextBox();
            this.txtRetailerNegNet = new System.Windows.Forms.TextBox();
            this.txtRetailerShare = new System.Windows.Forms.TextBox();
            this.txtTicketsPaid = new System.Windows.Forms.TextBox();
            this.txtRetailer = new System.Windows.Forms.TextBox();
            this.txtBalanceDue = new System.Windows.Forms.TextBox();
            this.lblAdvancedToRetailer = new System.Windows.Forms.Label();
            this.lblfixedexpense = new System.Windows.Forms.Label();
            this.txtFixedExpense = new System.Windows.Forms.TextBox();
            this.lblBalanceDue = new System.Windows.Forms.Label();
            this.txtAdvanceToRetailer = new BMC.EnterpriseClient.Views.NumberTextBox();
            this.lblRetailerSharebeforeFixedExpense = new System.Windows.Forms.Label();
            this.txtRetailerShareBeforeFixedExpense = new System.Windows.Forms.TextBox();
            this.btnProfitShare = new System.Windows.Forms.Button();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.grpLiquidationDetails.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpLiquidationDetails
            // 
            this.grpLiquidationDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpLiquidationDetails.Controls.Add(this.tableLayoutPanel1);
            this.grpLiquidationDetails.Location = new System.Drawing.Point(6, 0);
            this.grpLiquidationDetails.Name = "grpLiquidationDetails";
            this.grpLiquidationDetails.Size = new System.Drawing.Size(620, 423);
            this.grpLiquidationDetails.TabIndex = 0;
            this.grpLiquidationDetails.TabStop = false;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 27.70054F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 23.6113F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 48.68816F));
            this.tableLayoutPanel1.Controls.Add(this.lblDate, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblRetailerName, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblGross, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.lblTicketsExpected, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.lblNet, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.lblNetValue, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.lblRetailersNegativeNet, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.lblRetailerShare, 0, 7);
            this.tableLayoutPanel1.Controls.Add(this.lblTicketsPaid, 0, 8);
            this.tableLayoutPanel1.Controls.Add(this.lblRetailer, 0, 12);
            this.tableLayoutPanel1.Controls.Add(this.txtDate, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtRetailerName, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.txtGross, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.txtTicketsExpected, 2, 3);
            this.tableLayoutPanel1.Controls.Add(this.txtNet, 2, 4);
            this.tableLayoutPanel1.Controls.Add(this.txtNetValue, 2, 5);
            this.tableLayoutPanel1.Controls.Add(this.txtRetailerNegNet, 2, 6);
            this.tableLayoutPanel1.Controls.Add(this.txtRetailerShare, 2, 7);
            this.tableLayoutPanel1.Controls.Add(this.txtTicketsPaid, 2, 8);
            this.tableLayoutPanel1.Controls.Add(this.txtRetailer, 2, 12);
            this.tableLayoutPanel1.Controls.Add(this.txtBalanceDue, 2, 13);
            this.tableLayoutPanel1.Controls.Add(this.lblAdvancedToRetailer, 0, 9);
            this.tableLayoutPanel1.Controls.Add(this.lblfixedexpense, 0, 11);
            this.tableLayoutPanel1.Controls.Add(this.txtFixedExpense, 2, 11);
            this.tableLayoutPanel1.Controls.Add(this.lblBalanceDue, 0, 13);
            this.tableLayoutPanel1.Controls.Add(this.txtAdvanceToRetailer, 2, 9);
            this.tableLayoutPanel1.Controls.Add(this.lblRetailerSharebeforeFixedExpense, 0, 10);
            this.tableLayoutPanel1.Controls.Add(this.txtRetailerShareBeforeFixedExpense, 2, 10);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 17);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 14;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(614, 403);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.lblDate, 2);
            this.lblDate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDate.Location = new System.Drawing.Point(3, 0);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(308, 30);
            this.lblDate.TabIndex = 0;
            this.lblDate.Text = "Date:";
            // 
            // lblRetailerName
            // 
            this.lblRetailerName.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.lblRetailerName, 2);
            this.lblRetailerName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblRetailerName.Location = new System.Drawing.Point(3, 30);
            this.lblRetailerName.Name = "lblRetailerName";
            this.lblRetailerName.Size = new System.Drawing.Size(308, 30);
            this.lblRetailerName.TabIndex = 2;
            this.lblRetailerName.Text = "Retailer Name:";
            // 
            // lblGross
            // 
            this.lblGross.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.lblGross, 2);
            this.lblGross.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblGross.Location = new System.Drawing.Point(3, 60);
            this.lblGross.Name = "lblGross";
            this.lblGross.Size = new System.Drawing.Size(308, 30);
            this.lblGross.TabIndex = 4;
            this.lblGross.Text = "Total Meter In:";
            // 
            // lblTicketsExpected
            // 
            this.lblTicketsExpected.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.lblTicketsExpected, 2);
            this.lblTicketsExpected.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTicketsExpected.Location = new System.Drawing.Point(3, 90);
            this.lblTicketsExpected.Name = "lblTicketsExpected";
            this.lblTicketsExpected.Size = new System.Drawing.Size(308, 30);
            this.lblTicketsExpected.TabIndex = 6;
            this.lblTicketsExpected.Text = "Total Meter Out:";
            // 
            // lblNet
            // 
            this.lblNet.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.lblNet, 2);
            this.lblNet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblNet.Location = new System.Drawing.Point(3, 120);
            this.lblNet.Name = "lblNet";
            this.lblNet.Size = new System.Drawing.Size(308, 30);
            this.lblNet.TabIndex = 8;
            this.lblNet.Text = "Net";
            // 
            // lblNetValue
            // 
            this.lblNetValue.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.lblNetValue, 2);
            this.lblNetValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblNetValue.Location = new System.Drawing.Point(3, 150);
            this.lblNetValue.Name = "lblNetValue";
            this.lblNetValue.Size = new System.Drawing.Size(308, 30);
            this.lblNetValue.TabIndex = 10;
            this.lblNetValue.Text = "Net *";
            // 
            // lblRetailersNegativeNet
            // 
            this.lblRetailersNegativeNet.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.lblRetailersNegativeNet, 2);
            this.lblRetailersNegativeNet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblRetailersNegativeNet.Location = new System.Drawing.Point(3, 180);
            this.lblRetailersNegativeNet.Name = "lblRetailersNegativeNet";
            this.lblRetailersNegativeNet.Size = new System.Drawing.Size(308, 30);
            this.lblRetailersNegativeNet.TabIndex = 12;
            this.lblRetailersNegativeNet.Text = "Retailer\'s Negative Net: (Prior Read) :";
            // 
            // lblRetailerShare
            // 
            this.lblRetailerShare.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.lblRetailerShare, 2);
            this.lblRetailerShare.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblRetailerShare.Location = new System.Drawing.Point(3, 210);
            this.lblRetailerShare.Name = "lblRetailerShare";
            this.lblRetailerShare.Size = new System.Drawing.Size(308, 30);
            this.lblRetailerShare.TabIndex = 14;
            this.lblRetailerShare.Text = "Retailer Share:";
            // 
            // lblTicketsPaid
            // 
            this.lblTicketsPaid.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.lblTicketsPaid, 2);
            this.lblTicketsPaid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTicketsPaid.Location = new System.Drawing.Point(3, 240);
            this.lblTicketsPaid.Name = "lblTicketsPaid";
            this.lblTicketsPaid.Size = new System.Drawing.Size(308, 30);
            this.lblTicketsPaid.TabIndex = 16;
            // 
            // lblRetailer
            // 
            this.lblRetailer.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.lblRetailer, 2);
            this.lblRetailer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblRetailer.Location = new System.Drawing.Point(3, 345);
            this.lblRetailer.Name = "lblRetailer";
            this.lblRetailer.Size = new System.Drawing.Size(308, 30);
            this.lblRetailer.TabIndex = 24;
            this.lblRetailer.Text = "Retailer";
            // 
            // txtDate
            // 
            this.txtDate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDate.Location = new System.Drawing.Point(317, 3);
            this.txtDate.Name = "txtDate";
            this.txtDate.ReadOnly = true;
            this.txtDate.Size = new System.Drawing.Size(294, 21);
            this.txtDate.TabIndex = 1;
            // 
            // txtRetailerName
            // 
            this.txtRetailerName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtRetailerName.Location = new System.Drawing.Point(317, 33);
            this.txtRetailerName.Name = "txtRetailerName";
            this.txtRetailerName.ReadOnly = true;
            this.txtRetailerName.Size = new System.Drawing.Size(294, 21);
            this.txtRetailerName.TabIndex = 3;
            // 
            // txtGross
            // 
            this.txtGross.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtGross.Location = new System.Drawing.Point(317, 63);
            this.txtGross.Name = "txtGross";
            this.txtGross.ReadOnly = true;
            this.txtGross.Size = new System.Drawing.Size(294, 21);
            this.txtGross.TabIndex = 5;
            // 
            // txtTicketsExpected
            // 
            this.txtTicketsExpected.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtTicketsExpected.Location = new System.Drawing.Point(317, 93);
            this.txtTicketsExpected.Name = "txtTicketsExpected";
            this.txtTicketsExpected.ReadOnly = true;
            this.txtTicketsExpected.Size = new System.Drawing.Size(294, 21);
            this.txtTicketsExpected.TabIndex = 7;
            // 
            // txtNet
            // 
            this.txtNet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtNet.Location = new System.Drawing.Point(317, 123);
            this.txtNet.Name = "txtNet";
            this.txtNet.ReadOnly = true;
            this.txtNet.Size = new System.Drawing.Size(294, 21);
            this.txtNet.TabIndex = 9;
            // 
            // txtNetValue
            // 
            this.txtNetValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtNetValue.Location = new System.Drawing.Point(317, 153);
            this.txtNetValue.Name = "txtNetValue";
            this.txtNetValue.ReadOnly = true;
            this.txtNetValue.Size = new System.Drawing.Size(294, 21);
            this.txtNetValue.TabIndex = 11;
            // 
            // txtRetailerNegNet
            // 
            this.txtRetailerNegNet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtRetailerNegNet.Location = new System.Drawing.Point(317, 183);
            this.txtRetailerNegNet.Name = "txtRetailerNegNet";
            this.txtRetailerNegNet.ReadOnly = true;
            this.txtRetailerNegNet.Size = new System.Drawing.Size(294, 21);
            this.txtRetailerNegNet.TabIndex = 13;
            // 
            // txtRetailerShare
            // 
            this.txtRetailerShare.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtRetailerShare.Location = new System.Drawing.Point(317, 213);
            this.txtRetailerShare.Name = "txtRetailerShare";
            this.txtRetailerShare.ReadOnly = true;
            this.txtRetailerShare.Size = new System.Drawing.Size(294, 21);
            this.txtRetailerShare.TabIndex = 15;
            // 
            // txtTicketsPaid
            // 
            this.txtTicketsPaid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtTicketsPaid.Location = new System.Drawing.Point(317, 243);
            this.txtTicketsPaid.Name = "txtTicketsPaid";
            this.txtTicketsPaid.ReadOnly = true;
            this.txtTicketsPaid.Size = new System.Drawing.Size(294, 21);
            this.txtTicketsPaid.TabIndex = 17;
            // 
            // txtRetailer
            // 
            this.txtRetailer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtRetailer.Location = new System.Drawing.Point(317, 348);
            this.txtRetailer.Name = "txtRetailer";
            this.txtRetailer.ReadOnly = true;
            this.txtRetailer.Size = new System.Drawing.Size(294, 21);
            this.txtRetailer.TabIndex = 25;
            // 
            // txtBalanceDue
            // 
            this.txtBalanceDue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtBalanceDue.Location = new System.Drawing.Point(317, 378);
            this.txtBalanceDue.Name = "txtBalanceDue";
            this.txtBalanceDue.ReadOnly = true;
            this.txtBalanceDue.Size = new System.Drawing.Size(294, 21);
            this.txtBalanceDue.TabIndex = 27;
            // 
            // lblAdvancedToRetailer
            // 
            this.lblAdvancedToRetailer.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.lblAdvancedToRetailer, 2);
            this.lblAdvancedToRetailer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblAdvancedToRetailer.Location = new System.Drawing.Point(3, 270);
            this.lblAdvancedToRetailer.Name = "lblAdvancedToRetailer";
            this.lblAdvancedToRetailer.Size = new System.Drawing.Size(308, 25);
            this.lblAdvancedToRetailer.TabIndex = 18;
            this.lblAdvancedToRetailer.Text = "Advance To Retailer:";
            // 
            // lblfixedexpense
            // 
            this.lblfixedexpense.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.lblfixedexpense, 2);
            this.lblfixedexpense.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblfixedexpense.Location = new System.Drawing.Point(3, 320);
            this.lblfixedexpense.Name = "lblfixedexpense";
            this.lblfixedexpense.Size = new System.Drawing.Size(308, 25);
            this.lblfixedexpense.TabIndex = 22;
            this.lblfixedexpense.Text = "Expense Share:";
            // 
            // txtFixedExpense
            // 
            this.txtFixedExpense.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtFixedExpense.Location = new System.Drawing.Point(317, 323);
            this.txtFixedExpense.Name = "txtFixedExpense";
            this.txtFixedExpense.ReadOnly = true;
            this.txtFixedExpense.Size = new System.Drawing.Size(294, 21);
            this.txtFixedExpense.TabIndex = 23;
            // 
            // lblBalanceDue
            // 
            this.lblBalanceDue.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.lblBalanceDue, 2);
            this.lblBalanceDue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblBalanceDue.Location = new System.Drawing.Point(3, 375);
            this.lblBalanceDue.Name = "lblBalanceDue";
            this.lblBalanceDue.Size = new System.Drawing.Size(308, 28);
            this.lblBalanceDue.TabIndex = 26;
            this.lblBalanceDue.Text = "Balance Due";
            // 
            // txtAdvanceToRetailer
            // 
            this.txtAdvanceToRetailer.AllowDecimal = true;
            this.txtAdvanceToRetailer.AllowNegative = false;
            this.txtAdvanceToRetailer.DecimalLength = 2;
            this.txtAdvanceToRetailer.Denom = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtAdvanceToRetailer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtAdvanceToRetailer.Location = new System.Drawing.Point(317, 273);
            this.txtAdvanceToRetailer.MaxLength = 12;
            this.txtAdvanceToRetailer.MaxVaule = new decimal(new int[] {
            1215752191,
            23,
            0,
            131072});
            this.txtAdvanceToRetailer.Name = "txtAdvanceToRetailer";
            this.txtAdvanceToRetailer.ShortcutsEnabled = false;
            this.txtAdvanceToRetailer.Size = new System.Drawing.Size(294, 21);
            this.txtAdvanceToRetailer.TabIndex = 19;
            this.txtAdvanceToRetailer.TextChanged += new System.EventHandler(this.txtAdvanceToRetailer_TextChanged);
            // 
            // lblRetailerSharebeforeFixedExpense
            // 
            this.lblRetailerSharebeforeFixedExpense.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.lblRetailerSharebeforeFixedExpense, 2);
            this.lblRetailerSharebeforeFixedExpense.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblRetailerSharebeforeFixedExpense.Location = new System.Drawing.Point(3, 295);
            this.lblRetailerSharebeforeFixedExpense.Name = "lblRetailerSharebeforeFixedExpense";
            this.lblRetailerSharebeforeFixedExpense.Size = new System.Drawing.Size(308, 25);
            this.lblRetailerSharebeforeFixedExpense.TabIndex = 20;
            this.lblRetailerSharebeforeFixedExpense.Text = "Retailer Share Before Fixed Expense:";
            // 
            // txtRetailerShareBeforeFixedExpense
            // 
            this.txtRetailerShareBeforeFixedExpense.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRetailerShareBeforeFixedExpense.Location = new System.Drawing.Point(317, 298);
            this.txtRetailerShareBeforeFixedExpense.Name = "txtRetailerShareBeforeFixedExpense";
            this.txtRetailerShareBeforeFixedExpense.ReadOnly = true;
            this.txtRetailerShareBeforeFixedExpense.Size = new System.Drawing.Size(294, 21);
            this.txtRetailerShareBeforeFixedExpense.TabIndex = 21;
            // 
            // btnProfitShare
            // 
            this.btnProfitShare.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnProfitShare.Location = new System.Drawing.Point(323, 429);
            this.btnProfitShare.Name = "btnProfitShare";
            this.btnProfitShare.Size = new System.Drawing.Size(86, 29);
            this.btnProfitShare.TabIndex = 1;
            this.btnProfitShare.Text = "&Profit Share";
            this.btnProfitShare.UseVisualStyleBackColor = true;
            this.btnProfitShare.Click += new System.EventHandler(this.btnProfitShare_Click);
            // 
            // btnConfirm
            // 
            this.btnConfirm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConfirm.Location = new System.Drawing.Point(432, 429);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(86, 29);
            this.btnConfirm.TabIndex = 2;
            this.btnConfirm.Text = "&Confirm";
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(540, 429);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(86, 29);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // frmReadLiquidation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(633, 468);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnConfirm);
            this.Controls.Add(this.btnProfitShare);
            this.Controls.Add(this.grpLiquidationDetails);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmReadLiquidation";
            this.Text = "Retailer\'s Liquidation Details:";
            this.Load += new System.EventHandler(this.frmReadLiquidation_Load);
            this.grpLiquidationDetails.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpLiquidationDetails;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.Label lblRetailerName;
        private System.Windows.Forms.Label lblGross;
        private System.Windows.Forms.Label lblTicketsExpected;
        private System.Windows.Forms.Label lblNet;
        private System.Windows.Forms.Label lblNetValue;
        private System.Windows.Forms.Label lblRetailersNegativeNet;
        private System.Windows.Forms.Label lblRetailerShare;
        private System.Windows.Forms.Label lblTicketsPaid;
        private System.Windows.Forms.Label lblRetailer;
        private System.Windows.Forms.Label lblBalanceDue;
        private System.Windows.Forms.TextBox txtDate;
        private System.Windows.Forms.TextBox txtRetailerName;
        private System.Windows.Forms.TextBox txtGross;
        private System.Windows.Forms.TextBox txtTicketsExpected;
        private System.Windows.Forms.TextBox txtNet;
        private System.Windows.Forms.TextBox txtNetValue;
        private System.Windows.Forms.TextBox txtRetailerNegNet;
        private System.Windows.Forms.TextBox txtRetailerShare;
        private System.Windows.Forms.TextBox txtTicketsPaid;
        private System.Windows.Forms.TextBox txtRetailer;
        private System.Windows.Forms.TextBox txtBalanceDue;
        private System.Windows.Forms.Button btnProfitShare;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblAdvancedToRetailer;
        private System.Windows.Forms.Label lblfixedexpense;
        private System.Windows.Forms.TextBox txtFixedExpense;
        private NumberTextBox txtAdvanceToRetailer;
        private System.Windows.Forms.Label lblRetailerSharebeforeFixedExpense;
        private System.Windows.Forms.TextBox txtRetailerShareBeforeFixedExpense;
    }
}