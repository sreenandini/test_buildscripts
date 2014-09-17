namespace BMC.EnterpriseClient.Views
{
    partial class frmAddShareSchedule
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAddShareSchedule));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tcShareSchedule = new System.Windows.Forms.TabControl();
            this.tpSchedule = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel14 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel11 = new System.Windows.Forms.TableLayoutPanel();
            this.gbSchedule = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel12 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel25 = new System.Windows.Forms.TableLayoutPanel();
            this.rdbAlpha = new System.Windows.Forms.RadioButton();
            this.rdbNumeric = new System.Windows.Forms.RadioButton();
            this.lblName = new System.Windows.Forms.Label();
            this.lblDescription = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.lblNoofBands = new System.Windows.Forms.Label();
            this.txtNoofBands = new System.Windows.Forms.TextBox();
            this.lblBandCountType = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel15 = new System.Windows.Forms.TableLayoutPanel();
            this.btnCancelSchedule = new System.Windows.Forms.Button();
            this.btnUpdateSchedule = new System.Windows.Forms.Button();
            this.btnEditSchedule = new System.Windows.Forms.Button();
            this.btnScheduleClose = new System.Windows.Forms.Button();
            this.tpBands = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel26 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel27 = new System.Windows.Forms.TableLayoutPanel();
            this.btnBandsClose = new System.Windows.Forms.Button();
            this.btnApplyBands = new System.Windows.Forms.Button();
            this.gbBandDates = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel28 = new System.Windows.Forms.TableLayoutPanel();
            this.lblChange = new System.Windows.Forms.Label();
            this.dtpBandsFuture = new System.Windows.Forms.DateTimePicker();
            this.dtpBandsPast = new System.Windows.Forms.DateTimePicker();
            this.btnApplyDates = new System.Windows.Forms.Button();
            this.gbBandShares = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel29 = new System.Windows.Forms.TableLayoutPanel();
            this.lblCompany = new System.Windows.Forms.Label();
            this.lblSupplier = new System.Windows.Forms.Label();
            this.lblSite = new System.Windows.Forms.Label();
            this.txtPastSupplierShare = new System.Windows.Forms.MaskedTextBox();
            this.txtPastSiteshare = new System.Windows.Forms.MaskedTextBox();
            this.chkSupplierShareGuaranteedPast = new System.Windows.Forms.CheckBox();
            this.txtSupplierShare = new System.Windows.Forms.MaskedTextBox();
            this.chkSiteShareGuaranteedPast = new System.Windows.Forms.CheckBox();
            this.txtSiteshare = new System.Windows.Forms.MaskedTextBox();
            this.chkSupplierShareGuaranteed = new System.Windows.Forms.CheckBox();
            this.chkSiteShareGuaranteed = new System.Windows.Forms.CheckBox();
            this.txtFutureSupplierShare = new System.Windows.Forms.MaskedTextBox();
            this.txtFutureCompanyshare = new System.Windows.Forms.MaskedTextBox();
            this.chkSupplierShareGuaranteedFuture = new System.Windows.Forms.CheckBox();
            this.txtCompanyshare = new System.Windows.Forms.MaskedTextBox();
            this.chkSiteShareGuaranteedFuture = new System.Windows.Forms.CheckBox();
            this.chkCompanyShareGuaranteedFuture = new System.Windows.Forms.CheckBox();
            this.chkCompanyShareGuaranteedPast = new System.Windows.Forms.CheckBox();
            this.txtFutureSiteshare = new System.Windows.Forms.MaskedTextBox();
            this.chkCompanyShareGuaranteed = new System.Windows.Forms.CheckBox();
            this.txtPastCompanyshare = new System.Windows.Forms.MaskedTextBox();
            this.lblPastShares = new System.Windows.Forms.Label();
            this.lblCurrentShares = new System.Windows.Forms.Label();
            this.lblPastSharesGuaranteed = new System.Windows.Forms.Label();
            this.lblCurrentSharesGuaranteed = new System.Windows.Forms.Label();
            this.lblFutureSharesGuaranteed = new System.Windows.Forms.Label();
            this.lblFutureShares = new System.Windows.Forms.Label();
            this.btnBandFuturetoCurrent = new System.Windows.Forms.Button();
            this.btnBandsCurrentToPast = new System.Windows.Forms.Button();
            this.gbBandDetails = new System.Windows.Forms.GroupBox();
            this.lvBands = new BMC.CoreLib.Win32.ListViewEx();
            this.chdrBandName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chdrOperatorPast = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chdrSitePast = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chdrCompanyPast = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chdrBandPastDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chdrOperator = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chdrSite = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chdrCompany = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chdrBandFutureDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chdrOperatorFuture = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chdrSiteFuture = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chdrCompanyFuture = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tpMachines = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel9 = new System.Windows.Forms.TableLayoutPanel();
            this.gbSearchGameTitle = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel19 = new System.Windows.Forms.TableLayoutPanel();
            this.lblSearchGame = new System.Windows.Forms.Label();
            this.txtAddMachineSearch = new System.Windows.Forms.TextBox();
            this.cmbMachineSearch = new System.Windows.Forms.ComboBox();
            this.btnAddGameTitle = new System.Windows.Forms.Button();
            this.btnRemoveGameTitle = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel22 = new System.Windows.Forms.TableLayoutPanel();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnApplyMachines = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.gbMachineBands = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel23 = new System.Windows.Forms.TableLayoutPanel();
            this.lblMachineBands = new System.Windows.Forms.Label();
            this.btnMCBandFuturetoCurrent = new System.Windows.Forms.Button();
            this.btnMCBandCurrenttoPast = new System.Windows.Forms.Button();
            this.cmbMachineBandsCurrent = new System.Windows.Forms.ComboBox();
            this.cmbMachineBandsPast = new System.Windows.Forms.ComboBox();
            this.cmbMachineBandsFuture = new System.Windows.Forms.ComboBox();
            this.lblMachineBandsPast = new System.Windows.Forms.Label();
            this.lblMachineBandsCurrent = new System.Windows.Forms.Label();
            this.lblMachineBandsFuture = new System.Windows.Forms.Label();
            this.gbMachineDates = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel24 = new System.Windows.Forms.TableLayoutPanel();
            this.lblMachineChangeDates = new System.Windows.Forms.Label();
            this.DTPickerMachinesFuture = new System.Windows.Forms.DateTimePicker();
            this.DTPickerMachinesPast = new System.Windows.Forms.DateTimePicker();
            this.btnApplyDatesMachines = new System.Windows.Forms.Button();
            this.gbMachineDetails = new System.Windows.Forms.GroupBox();
            this.lvMachines = new BMC.CoreLib.Win32.ListViewEx();
            this.chdrGameTitle = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chdrPastBand = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chdrPastChangeDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chdrCurrentBand = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chdrFutureChangeDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chdrFutureBand = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tableLayoutPanel1.SuspendLayout();
            this.tcShareSchedule.SuspendLayout();
            this.tpSchedule.SuspendLayout();
            this.tableLayoutPanel14.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel11.SuspendLayout();
            this.gbSchedule.SuspendLayout();
            this.tableLayoutPanel12.SuspendLayout();
            this.tableLayoutPanel25.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel15.SuspendLayout();
            this.tpBands.SuspendLayout();
            this.tableLayoutPanel26.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.tableLayoutPanel27.SuspendLayout();
            this.gbBandDates.SuspendLayout();
            this.tableLayoutPanel28.SuspendLayout();
            this.gbBandShares.SuspendLayout();
            this.tableLayoutPanel29.SuspendLayout();
            this.gbBandDetails.SuspendLayout();
            this.tpMachines.SuspendLayout();
            this.tableLayoutPanel9.SuspendLayout();
            this.gbSearchGameTitle.SuspendLayout();
            this.tableLayoutPanel19.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.tableLayoutPanel22.SuspendLayout();
            this.gbMachineBands.SuspendLayout();
            this.tableLayoutPanel23.SuspendLayout();
            this.gbMachineDates.SuspendLayout();
            this.tableLayoutPanel24.SuspendLayout();
            this.gbMachineDetails.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tcShareSchedule, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(977, 636);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tcShareSchedule
            // 
            this.tcShareSchedule.Controls.Add(this.tpSchedule);
            this.tcShareSchedule.Controls.Add(this.tpBands);
            this.tcShareSchedule.Controls.Add(this.tpMachines);
            this.tcShareSchedule.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcShareSchedule.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tcShareSchedule.Location = new System.Drawing.Point(3, 3);
            this.tcShareSchedule.Name = "tcShareSchedule";
            this.tcShareSchedule.SelectedIndex = 0;
            this.tcShareSchedule.Size = new System.Drawing.Size(971, 630);
            this.tcShareSchedule.TabIndex = 0;
            this.tcShareSchedule.TabStop = false;
            this.tcShareSchedule.SelectedIndexChanged += new System.EventHandler(this.tcShareSchedule_SelectedIndexChanged);
            // 
            // tpSchedule
            // 
            this.tpSchedule.BackColor = System.Drawing.SystemColors.Control;
            this.tpSchedule.Controls.Add(this.tableLayoutPanel14);
            this.tpSchedule.Location = new System.Drawing.Point(4, 22);
            this.tpSchedule.Name = "tpSchedule";
            this.tpSchedule.Padding = new System.Windows.Forms.Padding(3);
            this.tpSchedule.Size = new System.Drawing.Size(963, 604);
            this.tpSchedule.TabIndex = 0;
            this.tpSchedule.Text = "Schedule";
            // 
            // tableLayoutPanel14
            // 
            this.tableLayoutPanel14.ColumnCount = 1;
            this.tableLayoutPanel14.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel14.Controls.Add(this.tableLayoutPanel3, 0, 0);
            this.tableLayoutPanel14.Controls.Add(this.tableLayoutPanel2, 0, 1);
            this.tableLayoutPanel14.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel14.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel14.Name = "tableLayoutPanel14";
            this.tableLayoutPanel14.RowCount = 2;
            this.tableLayoutPanel14.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel14.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.tableLayoutPanel14.Size = new System.Drawing.Size(957, 598);
            this.tableLayoutPanel14.TabIndex = 1;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 3;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel11, 1, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(951, 522);
            this.tableLayoutPanel3.TabIndex = 0;
            // 
            // tableLayoutPanel11
            // 
            this.tableLayoutPanel11.ColumnCount = 1;
            this.tableLayoutPanel11.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel11.Controls.Add(this.gbSchedule, 0, 1);
            this.tableLayoutPanel11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel11.Location = new System.Drawing.Point(240, 3);
            this.tableLayoutPanel11.Name = "tableLayoutPanel11";
            this.tableLayoutPanel11.RowCount = 3;
            this.tableLayoutPanel11.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel11.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 185F));
            this.tableLayoutPanel11.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel11.Size = new System.Drawing.Size(469, 516);
            this.tableLayoutPanel11.TabIndex = 0;
            // 
            // gbSchedule
            // 
            this.gbSchedule.Controls.Add(this.tableLayoutPanel12);
            this.gbSchedule.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbSchedule.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbSchedule.Location = new System.Drawing.Point(3, 168);
            this.gbSchedule.Name = "gbSchedule";
            this.gbSchedule.Size = new System.Drawing.Size(463, 179);
            this.gbSchedule.TabIndex = 0;
            this.gbSchedule.TabStop = false;
            this.gbSchedule.Text = "Schedule";
            // 
            // tableLayoutPanel12
            // 
            this.tableLayoutPanel12.ColumnCount = 2;
            this.tableLayoutPanel12.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 44.44444F));
            this.tableLayoutPanel12.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 55.55556F));
            this.tableLayoutPanel12.Controls.Add(this.tableLayoutPanel25, 1, 4);
            this.tableLayoutPanel12.Controls.Add(this.lblName, 0, 1);
            this.tableLayoutPanel12.Controls.Add(this.lblDescription, 0, 2);
            this.tableLayoutPanel12.Controls.Add(this.txtDescription, 1, 2);
            this.tableLayoutPanel12.Controls.Add(this.lblNoofBands, 0, 3);
            this.tableLayoutPanel12.Controls.Add(this.txtNoofBands, 1, 3);
            this.tableLayoutPanel12.Controls.Add(this.lblBandCountType, 0, 4);
            this.tableLayoutPanel12.Controls.Add(this.txtName, 1, 1);
            this.tableLayoutPanel12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel12.Location = new System.Drawing.Point(3, 17);
            this.tableLayoutPanel12.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel12.Name = "tableLayoutPanel12";
            this.tableLayoutPanel12.RowCount = 6;
            this.tableLayoutPanel12.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel12.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel12.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel12.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel12.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel12.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel12.Size = new System.Drawing.Size(457, 159);
            this.tableLayoutPanel12.TabIndex = 0;
            // 
            // tableLayoutPanel25
            // 
            this.tableLayoutPanel25.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tableLayoutPanel25.ColumnCount = 2;
            this.tableLayoutPanel25.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel25.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel25.Controls.Add(this.rdbAlpha, 1, 0);
            this.tableLayoutPanel25.Controls.Add(this.rdbNumeric, 0, 0);
            this.tableLayoutPanel25.Location = new System.Drawing.Point(203, 111);
            this.tableLayoutPanel25.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel25.Name = "tableLayoutPanel25";
            this.tableLayoutPanel25.RowCount = 1;
            this.tableLayoutPanel25.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel25.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel25.Size = new System.Drawing.Size(190, 26);
            this.tableLayoutPanel25.TabIndex = 3;
            // 
            // rdbAlpha
            // 
            this.rdbAlpha.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.rdbAlpha.AutoSize = true;
            this.rdbAlpha.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbAlpha.Location = new System.Drawing.Point(98, 4);
            this.rdbAlpha.Name = "rdbAlpha";
            this.rdbAlpha.Size = new System.Drawing.Size(57, 17);
            this.rdbAlpha.TabIndex = 1;
            this.rdbAlpha.TabStop = true;
            this.rdbAlpha.Text = "Alpha";
            this.rdbAlpha.UseVisualStyleBackColor = true;
            // 
            // rdbNumeric
            // 
            this.rdbNumeric.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.rdbNumeric.AutoSize = true;
            this.rdbNumeric.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbNumeric.Location = new System.Drawing.Point(3, 4);
            this.rdbNumeric.Name = "rdbNumeric";
            this.rdbNumeric.Size = new System.Drawing.Size(72, 17);
            this.rdbNumeric.TabIndex = 0;
            this.rdbNumeric.TabStop = true;
            this.rdbNumeric.Text = "Numeric";
            this.rdbNumeric.UseVisualStyleBackColor = true;
            // 
            // lblName
            // 
            this.lblName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblName.Location = new System.Drawing.Point(3, 22);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(197, 13);
            this.lblName.TabIndex = 8;
            this.lblName.Text = "Name : ";
            // 
            // lblDescription
            // 
            this.lblDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDescription.AutoSize = true;
            this.lblDescription.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescription.Location = new System.Drawing.Point(3, 52);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(197, 13);
            this.lblDescription.TabIndex = 10;
            this.lblDescription.Text = "Description : ";
            // 
            // txtDescription
            // 
            this.txtDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDescription.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDescription.Location = new System.Drawing.Point(206, 48);
            this.txtDescription.MaxLength = 50;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(248, 21);
            this.txtDescription.TabIndex = 1;
            // 
            // lblNoofBands
            // 
            this.lblNoofBands.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblNoofBands.AutoSize = true;
            this.lblNoofBands.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNoofBands.Location = new System.Drawing.Point(3, 82);
            this.lblNoofBands.Name = "lblNoofBands";
            this.lblNoofBands.Size = new System.Drawing.Size(197, 13);
            this.lblNoofBands.TabIndex = 12;
            this.lblNoofBands.Text = "No.of.Bands : ";
            // 
            // txtNoofBands
            // 
            this.txtNoofBands.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNoofBands.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNoofBands.Location = new System.Drawing.Point(206, 78);
            this.txtNoofBands.Name = "txtNoofBands";
            this.txtNoofBands.ReadOnly = true;
            this.txtNoofBands.Size = new System.Drawing.Size(248, 21);
            this.txtNoofBands.TabIndex = 2;
            // 
            // lblBandCountType
            // 
            this.lblBandCountType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblBandCountType.AutoSize = true;
            this.lblBandCountType.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBandCountType.Location = new System.Drawing.Point(3, 117);
            this.lblBandCountType.Name = "lblBandCountType";
            this.lblBandCountType.Size = new System.Drawing.Size(197, 13);
            this.lblBandCountType.TabIndex = 14;
            this.lblBandCountType.Text = "Band Count Type : ";
            // 
            // txtName
            // 
            this.txtName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtName.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtName.Location = new System.Drawing.Point(206, 18);
            this.txtName.MaxLength = 50;
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(248, 21);
            this.txtName.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Controls.Add(this.groupBox1, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 531);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 64F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(951, 64);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tableLayoutPanel15);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(945, 58);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // tableLayoutPanel15
            // 
            this.tableLayoutPanel15.ColumnCount = 5;
            this.tableLayoutPanel15.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel15.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            this.tableLayoutPanel15.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            this.tableLayoutPanel15.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            this.tableLayoutPanel15.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            this.tableLayoutPanel15.Controls.Add(this.btnCancelSchedule, 3, 0);
            this.tableLayoutPanel15.Controls.Add(this.btnUpdateSchedule, 2, 0);
            this.tableLayoutPanel15.Controls.Add(this.btnEditSchedule, 1, 0);
            this.tableLayoutPanel15.Controls.Add(this.btnScheduleClose, 4, 0);
            this.tableLayoutPanel15.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel15.Location = new System.Drawing.Point(3, 17);
            this.tableLayoutPanel15.Name = "tableLayoutPanel15";
            this.tableLayoutPanel15.RowCount = 1;
            this.tableLayoutPanel15.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel15.Size = new System.Drawing.Size(939, 38);
            this.tableLayoutPanel15.TabIndex = 1;
            // 
            // btnCancelSchedule
            // 
            this.btnCancelSchedule.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancelSchedule.Location = new System.Drawing.Point(682, 5);
            this.btnCancelSchedule.Name = "btnCancelSchedule";
            this.btnCancelSchedule.Size = new System.Drawing.Size(124, 28);
            this.btnCancelSchedule.TabIndex = 2;
            this.btnCancelSchedule.Text = "Cancel";
            this.btnCancelSchedule.UseVisualStyleBackColor = true;
            this.btnCancelSchedule.Click += new System.EventHandler(this.btnCancelSchedule_Click);
            // 
            // btnUpdateSchedule
            // 
            this.btnUpdateSchedule.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUpdateSchedule.Location = new System.Drawing.Point(552, 5);
            this.btnUpdateSchedule.Name = "btnUpdateSchedule";
            this.btnUpdateSchedule.Size = new System.Drawing.Size(124, 28);
            this.btnUpdateSchedule.TabIndex = 1;
            this.btnUpdateSchedule.Text = "Update";
            this.btnUpdateSchedule.UseVisualStyleBackColor = true;
            this.btnUpdateSchedule.Click += new System.EventHandler(this.btnUpdateSchedule_Click);
            // 
            // btnEditSchedule
            // 
            this.btnEditSchedule.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEditSchedule.Location = new System.Drawing.Point(422, 5);
            this.btnEditSchedule.Name = "btnEditSchedule";
            this.btnEditSchedule.Size = new System.Drawing.Size(124, 28);
            this.btnEditSchedule.TabIndex = 0;
            this.btnEditSchedule.Text = "Edit";
            this.btnEditSchedule.UseVisualStyleBackColor = true;
            this.btnEditSchedule.Click += new System.EventHandler(this.btnEditSchedule_Click);
            // 
            // btnScheduleClose
            // 
            this.btnScheduleClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnScheduleClose.Location = new System.Drawing.Point(812, 5);
            this.btnScheduleClose.Name = "btnScheduleClose";
            this.btnScheduleClose.Size = new System.Drawing.Size(124, 28);
            this.btnScheduleClose.TabIndex = 3;
            this.btnScheduleClose.Text = "Close";
            this.btnScheduleClose.UseVisualStyleBackColor = true;
            this.btnScheduleClose.Click += new System.EventHandler(this.btnScheduleClose_Click);
            // 
            // tpBands
            // 
            this.tpBands.BackColor = System.Drawing.SystemColors.Control;
            this.tpBands.Controls.Add(this.tableLayoutPanel26);
            this.tpBands.Location = new System.Drawing.Point(4, 22);
            this.tpBands.Name = "tpBands";
            this.tpBands.Padding = new System.Windows.Forms.Padding(3);
            this.tpBands.Size = new System.Drawing.Size(963, 604);
            this.tpBands.TabIndex = 4;
            this.tpBands.Text = "Bands";
            // 
            // tableLayoutPanel26
            // 
            this.tableLayoutPanel26.ColumnCount = 1;
            this.tableLayoutPanel26.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel26.Controls.Add(this.groupBox9, 0, 3);
            this.tableLayoutPanel26.Controls.Add(this.gbBandDates, 0, 2);
            this.tableLayoutPanel26.Controls.Add(this.gbBandShares, 0, 1);
            this.tableLayoutPanel26.Controls.Add(this.gbBandDetails, 0, 0);
            this.tableLayoutPanel26.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel26.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel26.Name = "tableLayoutPanel26";
            this.tableLayoutPanel26.RowCount = 4;
            this.tableLayoutPanel26.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel26.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 210F));
            this.tableLayoutPanel26.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel26.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel26.Size = new System.Drawing.Size(957, 598);
            this.tableLayoutPanel26.TabIndex = 0;
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.tableLayoutPanel27);
            this.groupBox9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox9.Location = new System.Drawing.Point(3, 541);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(951, 54);
            this.groupBox9.TabIndex = 0;
            this.groupBox9.TabStop = false;
            // 
            // tableLayoutPanel27
            // 
            this.tableLayoutPanel27.ColumnCount = 3;
            this.tableLayoutPanel27.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel27.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            this.tableLayoutPanel27.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            this.tableLayoutPanel27.Controls.Add(this.btnBandsClose, 2, 0);
            this.tableLayoutPanel27.Controls.Add(this.btnApplyBands, 1, 0);
            this.tableLayoutPanel27.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel27.Location = new System.Drawing.Point(3, 17);
            this.tableLayoutPanel27.Name = "tableLayoutPanel27";
            this.tableLayoutPanel27.RowCount = 1;
            this.tableLayoutPanel27.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel27.Size = new System.Drawing.Size(945, 34);
            this.tableLayoutPanel27.TabIndex = 0;
            // 
            // btnBandsClose
            // 
            this.btnBandsClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBandsClose.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBandsClose.Location = new System.Drawing.Point(818, 3);
            this.btnBandsClose.Name = "btnBandsClose";
            this.btnBandsClose.Size = new System.Drawing.Size(124, 28);
            this.btnBandsClose.TabIndex = 1;
            this.btnBandsClose.Text = "Close";
            this.btnBandsClose.UseVisualStyleBackColor = true;
            this.btnBandsClose.Click += new System.EventHandler(this.btnScheduleClose_Click);
            // 
            // btnApplyBands
            // 
            this.btnApplyBands.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnApplyBands.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnApplyBands.Location = new System.Drawing.Point(688, 3);
            this.btnApplyBands.Name = "btnApplyBands";
            this.btnApplyBands.Size = new System.Drawing.Size(124, 27);
            this.btnApplyBands.TabIndex = 0;
            this.btnApplyBands.Text = "Apply";
            this.btnApplyBands.UseVisualStyleBackColor = true;
            this.btnApplyBands.Click += new System.EventHandler(this.btnApplyBands_Click);
            // 
            // gbBandDates
            // 
            this.gbBandDates.Controls.Add(this.tableLayoutPanel28);
            this.gbBandDates.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbBandDates.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbBandDates.Location = new System.Drawing.Point(3, 481);
            this.gbBandDates.Name = "gbBandDates";
            this.gbBandDates.Size = new System.Drawing.Size(951, 54);
            this.gbBandDates.TabIndex = 1;
            this.gbBandDates.TabStop = false;
            this.gbBandDates.Text = "Dates";
            // 
            // tableLayoutPanel28
            // 
            this.tableLayoutPanel28.ColumnCount = 5;
            this.tableLayoutPanel28.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel28.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel28.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            this.tableLayoutPanel28.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            this.tableLayoutPanel28.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            this.tableLayoutPanel28.Controls.Add(this.lblChange, 1, 0);
            this.tableLayoutPanel28.Controls.Add(this.dtpBandsFuture, 2, 0);
            this.tableLayoutPanel28.Controls.Add(this.dtpBandsPast, 1, 0);
            this.tableLayoutPanel28.Controls.Add(this.btnApplyDates, 2, 0);
            this.tableLayoutPanel28.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel28.Location = new System.Drawing.Point(3, 17);
            this.tableLayoutPanel28.Name = "tableLayoutPanel28";
            this.tableLayoutPanel28.RowCount = 1;
            this.tableLayoutPanel28.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel28.Size = new System.Drawing.Size(945, 34);
            this.tableLayoutPanel28.TabIndex = 0;
            // 
            // lblChange
            // 
            this.lblChange.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblChange.AutoSize = true;
            this.lblChange.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblChange.Location = new System.Drawing.Point(458, 10);
            this.lblChange.Name = "lblChange";
            this.lblChange.Size = new System.Drawing.Size(94, 13);
            this.lblChange.TabIndex = 66;
            this.lblChange.Text = "Change Dates:";
            // 
            // dtpBandsFuture
            // 
            this.dtpBandsFuture.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpBandsFuture.CustomFormat = "dd/MM/yyyy";
            this.dtpBandsFuture.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpBandsFuture.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpBandsFuture.Location = new System.Drawing.Point(688, 6);
            this.dtpBandsFuture.Name = "dtpBandsFuture";
            this.dtpBandsFuture.Size = new System.Drawing.Size(124, 21);
            this.dtpBandsFuture.TabIndex = 1;
            // 
            // dtpBandsPast
            // 
            this.dtpBandsPast.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpBandsPast.CustomFormat = "dd/MM/yyyy";
            this.dtpBandsPast.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpBandsPast.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpBandsPast.Location = new System.Drawing.Point(558, 6);
            this.dtpBandsPast.Name = "dtpBandsPast";
            this.dtpBandsPast.Size = new System.Drawing.Size(124, 21);
            this.dtpBandsPast.TabIndex = 0;
            // 
            // btnApplyDates
            // 
            this.btnApplyDates.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnApplyDates.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnApplyDates.Location = new System.Drawing.Point(818, 3);
            this.btnApplyDates.Name = "btnApplyDates";
            this.btnApplyDates.Size = new System.Drawing.Size(124, 28);
            this.btnApplyDates.TabIndex = 2;
            this.btnApplyDates.Text = "Apply Dates";
            this.btnApplyDates.UseVisualStyleBackColor = true;
            this.btnApplyDates.Click += new System.EventHandler(this.btnApplyDates_Click);
            // 
            // gbBandShares
            // 
            this.gbBandShares.Controls.Add(this.tableLayoutPanel29);
            this.gbBandShares.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbBandShares.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbBandShares.Location = new System.Drawing.Point(3, 271);
            this.gbBandShares.Name = "gbBandShares";
            this.gbBandShares.Size = new System.Drawing.Size(951, 204);
            this.gbBandShares.TabIndex = 0;
            this.gbBandShares.TabStop = false;
            this.gbBandShares.Text = "Shares";
            // 
            // tableLayoutPanel29
            // 
            this.tableLayoutPanel29.ColumnCount = 8;
            this.tableLayoutPanel29.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel29.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel29.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            this.tableLayoutPanel29.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel29.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            this.tableLayoutPanel29.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel29.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            this.tableLayoutPanel29.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel29.Controls.Add(this.lblCompany, 1, 4);
            this.tableLayoutPanel29.Controls.Add(this.lblSupplier, 1, 2);
            this.tableLayoutPanel29.Controls.Add(this.lblSite, 1, 3);
            this.tableLayoutPanel29.Controls.Add(this.txtPastSupplierShare, 2, 2);
            this.tableLayoutPanel29.Controls.Add(this.txtPastSiteshare, 2, 3);
            this.tableLayoutPanel29.Controls.Add(this.chkSupplierShareGuaranteedPast, 3, 2);
            this.tableLayoutPanel29.Controls.Add(this.txtSupplierShare, 4, 2);
            this.tableLayoutPanel29.Controls.Add(this.chkSiteShareGuaranteedPast, 3, 3);
            this.tableLayoutPanel29.Controls.Add(this.txtSiteshare, 4, 3);
            this.tableLayoutPanel29.Controls.Add(this.chkSupplierShareGuaranteed, 5, 2);
            this.tableLayoutPanel29.Controls.Add(this.chkSiteShareGuaranteed, 5, 3);
            this.tableLayoutPanel29.Controls.Add(this.txtFutureSupplierShare, 6, 2);
            this.tableLayoutPanel29.Controls.Add(this.txtFutureCompanyshare, 6, 4);
            this.tableLayoutPanel29.Controls.Add(this.chkSupplierShareGuaranteedFuture, 7, 2);
            this.tableLayoutPanel29.Controls.Add(this.txtCompanyshare, 4, 4);
            this.tableLayoutPanel29.Controls.Add(this.chkSiteShareGuaranteedFuture, 7, 3);
            this.tableLayoutPanel29.Controls.Add(this.chkCompanyShareGuaranteedFuture, 7, 4);
            this.tableLayoutPanel29.Controls.Add(this.chkCompanyShareGuaranteedPast, 3, 4);
            this.tableLayoutPanel29.Controls.Add(this.txtFutureSiteshare, 6, 3);
            this.tableLayoutPanel29.Controls.Add(this.chkCompanyShareGuaranteed, 5, 4);
            this.tableLayoutPanel29.Controls.Add(this.txtPastCompanyshare, 2, 4);
            this.tableLayoutPanel29.Controls.Add(this.lblPastShares, 2, 1);
            this.tableLayoutPanel29.Controls.Add(this.lblCurrentShares, 4, 1);
            this.tableLayoutPanel29.Controls.Add(this.lblPastSharesGuaranteed, 3, 1);
            this.tableLayoutPanel29.Controls.Add(this.lblCurrentSharesGuaranteed, 5, 1);
            this.tableLayoutPanel29.Controls.Add(this.lblFutureSharesGuaranteed, 7, 1);
            this.tableLayoutPanel29.Controls.Add(this.lblFutureShares, 6, 1);
            this.tableLayoutPanel29.Controls.Add(this.btnBandFuturetoCurrent, 6, 5);
            this.tableLayoutPanel29.Controls.Add(this.btnBandsCurrentToPast, 4, 5);
            this.tableLayoutPanel29.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel29.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tableLayoutPanel29.Location = new System.Drawing.Point(3, 17);
            this.tableLayoutPanel29.Name = "tableLayoutPanel29";
            this.tableLayoutPanel29.RowCount = 6;
            this.tableLayoutPanel29.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel29.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel29.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel29.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel29.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel29.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel29.Size = new System.Drawing.Size(945, 184);
            this.tableLayoutPanel29.TabIndex = 0;
            // 
            // lblCompany
            // 
            this.lblCompany.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCompany.AutoSize = true;
            this.lblCompany.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCompany.Location = new System.Drawing.Point(388, 117);
            this.lblCompany.Name = "lblCompany";
            this.lblCompany.Size = new System.Drawing.Size(74, 13);
            this.lblCompany.TabIndex = 92;
            this.lblCompany.Text = "Company:";
            // 
            // lblSupplier
            // 
            this.lblSupplier.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSupplier.AutoSize = true;
            this.lblSupplier.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSupplier.Location = new System.Drawing.Point(388, 37);
            this.lblSupplier.Name = "lblSupplier";
            this.lblSupplier.Size = new System.Drawing.Size(74, 13);
            this.lblSupplier.TabIndex = 90;
            this.lblSupplier.Text = "Supplier:";
            // 
            // lblSite
            // 
            this.lblSite.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSite.AutoSize = true;
            this.lblSite.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSite.Location = new System.Drawing.Point(388, 77);
            this.lblSite.Name = "lblSite";
            this.lblSite.Size = new System.Drawing.Size(74, 13);
            this.lblSite.TabIndex = 91;
            this.lblSite.Text = "Site:";
            // 
            // txtPastSupplierShare
            // 
            this.txtPastSupplierShare.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPastSupplierShare.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPastSupplierShare.Location = new System.Drawing.Point(468, 33);
            this.txtPastSupplierShare.Mask = "00.00";
            this.txtPastSupplierShare.Name = "txtPastSupplierShare";
            this.txtPastSupplierShare.Size = new System.Drawing.Size(124, 21);
            this.txtPastSupplierShare.TabIndex = 0;
            this.txtPastSupplierShare.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtPastSiteshare
            // 
            this.txtPastSiteshare.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPastSiteshare.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPastSiteshare.Location = new System.Drawing.Point(468, 73);
            this.txtPastSiteshare.Mask = "00.00";
            this.txtPastSiteshare.Name = "txtPastSiteshare";
            this.txtPastSiteshare.Size = new System.Drawing.Size(124, 21);
            this.txtPastSiteshare.TabIndex = 2;
            this.txtPastSiteshare.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // chkSupplierShareGuaranteedPast
            // 
            this.chkSupplierShareGuaranteedPast.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.chkSupplierShareGuaranteedPast.AutoSize = true;
            this.chkSupplierShareGuaranteedPast.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkSupplierShareGuaranteedPast.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkSupplierShareGuaranteedPast.Location = new System.Drawing.Point(598, 37);
            this.chkSupplierShareGuaranteedPast.Name = "chkSupplierShareGuaranteedPast";
            this.chkSupplierShareGuaranteedPast.Size = new System.Drawing.Size(24, 14);
            this.chkSupplierShareGuaranteedPast.TabIndex = 1;
            this.chkSupplierShareGuaranteedPast.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.chkSupplierShareGuaranteedPast.UseVisualStyleBackColor = true;
            // 
            // txtSupplierShare
            // 
            this.txtSupplierShare.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSupplierShare.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSupplierShare.Location = new System.Drawing.Point(628, 33);
            this.txtSupplierShare.Mask = "00.00";
            this.txtSupplierShare.Name = "txtSupplierShare";
            this.txtSupplierShare.Size = new System.Drawing.Size(124, 21);
            this.txtSupplierShare.TabIndex = 6;
            this.txtSupplierShare.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // chkSiteShareGuaranteedPast
            // 
            this.chkSiteShareGuaranteedPast.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.chkSiteShareGuaranteedPast.AutoSize = true;
            this.chkSiteShareGuaranteedPast.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkSiteShareGuaranteedPast.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkSiteShareGuaranteedPast.Location = new System.Drawing.Point(598, 77);
            this.chkSiteShareGuaranteedPast.Name = "chkSiteShareGuaranteedPast";
            this.chkSiteShareGuaranteedPast.Size = new System.Drawing.Size(24, 14);
            this.chkSiteShareGuaranteedPast.TabIndex = 3;
            this.chkSiteShareGuaranteedPast.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.chkSiteShareGuaranteedPast.UseVisualStyleBackColor = true;
            // 
            // txtSiteshare
            // 
            this.txtSiteshare.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSiteshare.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSiteshare.Location = new System.Drawing.Point(628, 73);
            this.txtSiteshare.Mask = "00.00";
            this.txtSiteshare.Name = "txtSiteshare";
            this.txtSiteshare.Size = new System.Drawing.Size(124, 21);
            this.txtSiteshare.TabIndex = 8;
            this.txtSiteshare.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // chkSupplierShareGuaranteed
            // 
            this.chkSupplierShareGuaranteed.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.chkSupplierShareGuaranteed.AutoSize = true;
            this.chkSupplierShareGuaranteed.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkSupplierShareGuaranteed.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkSupplierShareGuaranteed.Location = new System.Drawing.Point(758, 37);
            this.chkSupplierShareGuaranteed.Name = "chkSupplierShareGuaranteed";
            this.chkSupplierShareGuaranteed.Size = new System.Drawing.Size(24, 14);
            this.chkSupplierShareGuaranteed.TabIndex = 7;
            this.chkSupplierShareGuaranteed.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.chkSupplierShareGuaranteed.UseVisualStyleBackColor = true;
            // 
            // chkSiteShareGuaranteed
            // 
            this.chkSiteShareGuaranteed.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.chkSiteShareGuaranteed.AutoSize = true;
            this.chkSiteShareGuaranteed.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkSiteShareGuaranteed.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkSiteShareGuaranteed.Location = new System.Drawing.Point(758, 77);
            this.chkSiteShareGuaranteed.Name = "chkSiteShareGuaranteed";
            this.chkSiteShareGuaranteed.Size = new System.Drawing.Size(24, 14);
            this.chkSiteShareGuaranteed.TabIndex = 9;
            this.chkSiteShareGuaranteed.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.chkSiteShareGuaranteed.UseVisualStyleBackColor = true;
            // 
            // txtFutureSupplierShare
            // 
            this.txtFutureSupplierShare.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFutureSupplierShare.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFutureSupplierShare.Location = new System.Drawing.Point(788, 33);
            this.txtFutureSupplierShare.Mask = "00.00";
            this.txtFutureSupplierShare.Name = "txtFutureSupplierShare";
            this.txtFutureSupplierShare.Size = new System.Drawing.Size(124, 21);
            this.txtFutureSupplierShare.TabIndex = 12;
            this.txtFutureSupplierShare.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtFutureCompanyshare
            // 
            this.txtFutureCompanyshare.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFutureCompanyshare.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFutureCompanyshare.Location = new System.Drawing.Point(788, 113);
            this.txtFutureCompanyshare.Mask = "00.00";
            this.txtFutureCompanyshare.Name = "txtFutureCompanyshare";
            this.txtFutureCompanyshare.Size = new System.Drawing.Size(124, 21);
            this.txtFutureCompanyshare.TabIndex = 16;
            this.txtFutureCompanyshare.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // chkSupplierShareGuaranteedFuture
            // 
            this.chkSupplierShareGuaranteedFuture.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.chkSupplierShareGuaranteedFuture.AutoSize = true;
            this.chkSupplierShareGuaranteedFuture.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkSupplierShareGuaranteedFuture.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkSupplierShareGuaranteedFuture.Location = new System.Drawing.Point(918, 37);
            this.chkSupplierShareGuaranteedFuture.Name = "chkSupplierShareGuaranteedFuture";
            this.chkSupplierShareGuaranteedFuture.Size = new System.Drawing.Size(24, 14);
            this.chkSupplierShareGuaranteedFuture.TabIndex = 13;
            this.chkSupplierShareGuaranteedFuture.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.chkSupplierShareGuaranteedFuture.UseVisualStyleBackColor = true;
            // 
            // txtCompanyshare
            // 
            this.txtCompanyshare.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCompanyshare.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCompanyshare.Location = new System.Drawing.Point(628, 113);
            this.txtCompanyshare.Mask = "00.00";
            this.txtCompanyshare.Name = "txtCompanyshare";
            this.txtCompanyshare.Size = new System.Drawing.Size(124, 21);
            this.txtCompanyshare.TabIndex = 10;
            this.txtCompanyshare.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // chkSiteShareGuaranteedFuture
            // 
            this.chkSiteShareGuaranteedFuture.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.chkSiteShareGuaranteedFuture.AutoSize = true;
            this.chkSiteShareGuaranteedFuture.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkSiteShareGuaranteedFuture.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkSiteShareGuaranteedFuture.Location = new System.Drawing.Point(918, 77);
            this.chkSiteShareGuaranteedFuture.Name = "chkSiteShareGuaranteedFuture";
            this.chkSiteShareGuaranteedFuture.Size = new System.Drawing.Size(24, 14);
            this.chkSiteShareGuaranteedFuture.TabIndex = 15;
            this.chkSiteShareGuaranteedFuture.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.chkSiteShareGuaranteedFuture.UseVisualStyleBackColor = true;
            // 
            // chkCompanyShareGuaranteedFuture
            // 
            this.chkCompanyShareGuaranteedFuture.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.chkCompanyShareGuaranteedFuture.AutoSize = true;
            this.chkCompanyShareGuaranteedFuture.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkCompanyShareGuaranteedFuture.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkCompanyShareGuaranteedFuture.Location = new System.Drawing.Point(918, 117);
            this.chkCompanyShareGuaranteedFuture.Name = "chkCompanyShareGuaranteedFuture";
            this.chkCompanyShareGuaranteedFuture.Size = new System.Drawing.Size(24, 14);
            this.chkCompanyShareGuaranteedFuture.TabIndex = 17;
            this.chkCompanyShareGuaranteedFuture.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.chkCompanyShareGuaranteedFuture.UseVisualStyleBackColor = true;
            // 
            // chkCompanyShareGuaranteedPast
            // 
            this.chkCompanyShareGuaranteedPast.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.chkCompanyShareGuaranteedPast.AutoSize = true;
            this.chkCompanyShareGuaranteedPast.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkCompanyShareGuaranteedPast.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkCompanyShareGuaranteedPast.Location = new System.Drawing.Point(598, 117);
            this.chkCompanyShareGuaranteedPast.Name = "chkCompanyShareGuaranteedPast";
            this.chkCompanyShareGuaranteedPast.Size = new System.Drawing.Size(24, 14);
            this.chkCompanyShareGuaranteedPast.TabIndex = 5;
            this.chkCompanyShareGuaranteedPast.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.chkCompanyShareGuaranteedPast.UseVisualStyleBackColor = true;
            // 
            // txtFutureSiteshare
            // 
            this.txtFutureSiteshare.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFutureSiteshare.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFutureSiteshare.Location = new System.Drawing.Point(788, 73);
            this.txtFutureSiteshare.Mask = "00.00";
            this.txtFutureSiteshare.Name = "txtFutureSiteshare";
            this.txtFutureSiteshare.Size = new System.Drawing.Size(124, 21);
            this.txtFutureSiteshare.TabIndex = 14;
            this.txtFutureSiteshare.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // chkCompanyShareGuaranteed
            // 
            this.chkCompanyShareGuaranteed.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.chkCompanyShareGuaranteed.AutoSize = true;
            this.chkCompanyShareGuaranteed.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkCompanyShareGuaranteed.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkCompanyShareGuaranteed.Location = new System.Drawing.Point(758, 117);
            this.chkCompanyShareGuaranteed.Name = "chkCompanyShareGuaranteed";
            this.chkCompanyShareGuaranteed.Size = new System.Drawing.Size(24, 14);
            this.chkCompanyShareGuaranteed.TabIndex = 11;
            this.chkCompanyShareGuaranteed.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.chkCompanyShareGuaranteed.UseVisualStyleBackColor = true;
            // 
            // txtPastCompanyshare
            // 
            this.txtPastCompanyshare.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPastCompanyshare.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPastCompanyshare.Location = new System.Drawing.Point(468, 113);
            this.txtPastCompanyshare.Mask = "00.00";
            this.txtPastCompanyshare.Name = "txtPastCompanyshare";
            this.txtPastCompanyshare.Size = new System.Drawing.Size(124, 21);
            this.txtPastCompanyshare.TabIndex = 4;
            this.txtPastCompanyshare.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblPastShares
            // 
            this.lblPastShares.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPastShares.AutoSize = true;
            this.lblPastShares.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPastShares.Location = new System.Drawing.Point(468, 5);
            this.lblPastShares.Name = "lblPastShares";
            this.lblPastShares.Size = new System.Drawing.Size(124, 13);
            this.lblPastShares.TabIndex = 111;
            this.lblPastShares.Text = "Past Shares %";
            // 
            // lblCurrentShares
            // 
            this.lblCurrentShares.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCurrentShares.AutoSize = true;
            this.lblCurrentShares.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrentShares.Location = new System.Drawing.Point(628, 5);
            this.lblCurrentShares.Name = "lblCurrentShares";
            this.lblCurrentShares.Size = new System.Drawing.Size(124, 13);
            this.lblCurrentShares.TabIndex = 112;
            this.lblCurrentShares.Text = "Current Shares %";
            // 
            // lblPastSharesGuaranteed
            // 
            this.lblPastSharesGuaranteed.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPastSharesGuaranteed.AutoSize = true;
            this.lblPastSharesGuaranteed.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPastSharesGuaranteed.Location = new System.Drawing.Point(598, 5);
            this.lblPastSharesGuaranteed.Name = "lblPastSharesGuaranteed";
            this.lblPastSharesGuaranteed.Size = new System.Drawing.Size(24, 13);
            this.lblPastSharesGuaranteed.TabIndex = 114;
            this.lblPastSharesGuaranteed.Text = "G?";
            // 
            // lblCurrentSharesGuaranteed
            // 
            this.lblCurrentSharesGuaranteed.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCurrentSharesGuaranteed.AutoSize = true;
            this.lblCurrentSharesGuaranteed.Location = new System.Drawing.Point(758, 5);
            this.lblCurrentSharesGuaranteed.Name = "lblCurrentSharesGuaranteed";
            this.lblCurrentSharesGuaranteed.Size = new System.Drawing.Size(24, 13);
            this.lblCurrentSharesGuaranteed.TabIndex = 115;
            this.lblCurrentSharesGuaranteed.Text = "G?";
            // 
            // lblFutureSharesGuaranteed
            // 
            this.lblFutureSharesGuaranteed.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFutureSharesGuaranteed.AutoSize = true;
            this.lblFutureSharesGuaranteed.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFutureSharesGuaranteed.Location = new System.Drawing.Point(918, 5);
            this.lblFutureSharesGuaranteed.Name = "lblFutureSharesGuaranteed";
            this.lblFutureSharesGuaranteed.Size = new System.Drawing.Size(24, 13);
            this.lblFutureSharesGuaranteed.TabIndex = 116;
            this.lblFutureSharesGuaranteed.Text = "G?";
            // 
            // lblFutureShares
            // 
            this.lblFutureShares.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFutureShares.AutoSize = true;
            this.lblFutureShares.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFutureShares.Location = new System.Drawing.Point(788, 5);
            this.lblFutureShares.Name = "lblFutureShares";
            this.lblFutureShares.Size = new System.Drawing.Size(124, 13);
            this.lblFutureShares.TabIndex = 113;
            this.lblFutureShares.Text = "Future Shares %";
            // 
            // btnBandFuturetoCurrent
            // 
            this.btnBandFuturetoCurrent.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnBandFuturetoCurrent.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBandFuturetoCurrent.Location = new System.Drawing.Point(788, 150);
            this.btnBandFuturetoCurrent.Name = "btnBandFuturetoCurrent";
            this.btnBandFuturetoCurrent.Size = new System.Drawing.Size(124, 28);
            this.btnBandFuturetoCurrent.TabIndex = 19;
            this.btnBandFuturetoCurrent.Text = "Current <-- Future";
            this.btnBandFuturetoCurrent.UseVisualStyleBackColor = true;
            this.btnBandFuturetoCurrent.Click += new System.EventHandler(this.btnBandFuturetoCurrent_Click);
            // 
            // btnBandsCurrentToPast
            // 
            this.btnBandsCurrentToPast.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnBandsCurrentToPast.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBandsCurrentToPast.Location = new System.Drawing.Point(628, 150);
            this.btnBandsCurrentToPast.Name = "btnBandsCurrentToPast";
            this.btnBandsCurrentToPast.Size = new System.Drawing.Size(124, 28);
            this.btnBandsCurrentToPast.TabIndex = 18;
            this.btnBandsCurrentToPast.Text = "Past <-- Current";
            this.btnBandsCurrentToPast.UseVisualStyleBackColor = true;
            this.btnBandsCurrentToPast.Click += new System.EventHandler(this.btnBandsCurrentToPast_Click);
            // 
            // gbBandDetails
            // 
            this.gbBandDetails.Controls.Add(this.lvBands);
            this.gbBandDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbBandDetails.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbBandDetails.Location = new System.Drawing.Point(3, 3);
            this.gbBandDetails.Name = "gbBandDetails";
            this.gbBandDetails.Size = new System.Drawing.Size(951, 262);
            this.gbBandDetails.TabIndex = 3;
            this.gbBandDetails.TabStop = false;
            this.gbBandDetails.Text = "Details";
            // 
            // lvBands
            // 
            this.lvBands.ClipboardCopyFormat = BMC.CoreLib.Win32.ListViewClipboardCopyFormat.Default;
            this.lvBands.ClipboardCopyMode = BMC.CoreLib.Win32.ListViewClipboardCopyMode.Disable;
            this.lvBands.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chdrBandName,
            this.chdrOperatorPast,
            this.chdrSitePast,
            this.chdrCompanyPast,
            this.chdrBandPastDate,
            this.chdrOperator,
            this.chdrSite,
            this.chdrCompany,
            this.chdrBandFutureDate,
            this.chdrOperatorFuture,
            this.chdrSiteFuture,
            this.chdrCompanyFuture});
            this.lvBands.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvBands.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvBands.FullRowSelect = true;
            this.lvBands.GridLines = true;
            this.lvBands.HideSelection = false;
            this.lvBands.Location = new System.Drawing.Point(3, 17);
            this.lvBands.Name = "lvBands";
            this.lvBands.Size = new System.Drawing.Size(945, 242);
            this.lvBands.TabIndex = 2;
            this.lvBands.TabStop = false;
            this.lvBands.UseCompatibleStateImageBehavior = false;
            this.lvBands.View = System.Windows.Forms.View.Details;
            this.lvBands.SelectedIndexChanged += new System.EventHandler(this.lvBands_SelectedIndexChanged);
            this.lvBands.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lvBands_MouseClick);
            // 
            // chdrBandName
            // 
            this.chdrBandName.Text = "Band Name";
            // 
            // chdrOperatorPast
            // 
            this.chdrOperatorPast.Text = "Operator";
            // 
            // chdrSitePast
            // 
            this.chdrSitePast.Text = "Site";
            // 
            // chdrCompanyPast
            // 
            this.chdrCompanyPast.Text = "Company";
            // 
            // chdrBandPastDate
            // 
            this.chdrBandPastDate.Text = "Change Date";
            // 
            // chdrOperator
            // 
            this.chdrOperator.Text = "Operator";
            // 
            // chdrSite
            // 
            this.chdrSite.Text = "Site";
            // 
            // chdrCompany
            // 
            this.chdrCompany.Text = "Company";
            // 
            // chdrBandFutureDate
            // 
            this.chdrBandFutureDate.Text = "Change Date";
            // 
            // chdrOperatorFuture
            // 
            this.chdrOperatorFuture.Text = " Operator";
            // 
            // chdrSiteFuture
            // 
            this.chdrSiteFuture.Text = "Site";
            // 
            // chdrCompanyFuture
            // 
            this.chdrCompanyFuture.Text = "Company";
            // 
            // tpMachines
            // 
            this.tpMachines.BackColor = System.Drawing.SystemColors.Control;
            this.tpMachines.Controls.Add(this.tableLayoutPanel9);
            this.tpMachines.Location = new System.Drawing.Point(4, 22);
            this.tpMachines.Name = "tpMachines";
            this.tpMachines.Size = new System.Drawing.Size(963, 604);
            this.tpMachines.TabIndex = 3;
            this.tpMachines.Text = "Machines";
            // 
            // tableLayoutPanel9
            // 
            this.tableLayoutPanel9.ColumnCount = 1;
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel9.Controls.Add(this.gbSearchGameTitle, 0, 1);
            this.tableLayoutPanel9.Controls.Add(this.groupBox4, 0, 4);
            this.tableLayoutPanel9.Controls.Add(this.gbMachineBands, 0, 2);
            this.tableLayoutPanel9.Controls.Add(this.gbMachineDates, 0, 3);
            this.tableLayoutPanel9.Controls.Add(this.gbMachineDetails, 0, 0);
            this.tableLayoutPanel9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel9.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel9.Name = "tableLayoutPanel9";
            this.tableLayoutPanel9.RowCount = 5;
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel9.Size = new System.Drawing.Size(963, 604);
            this.tableLayoutPanel9.TabIndex = 0;
            // 
            // gbSearchGameTitle
            // 
            this.gbSearchGameTitle.Controls.Add(this.tableLayoutPanel19);
            this.gbSearchGameTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbSearchGameTitle.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbSearchGameTitle.Location = new System.Drawing.Point(3, 347);
            this.gbSearchGameTitle.Name = "gbSearchGameTitle";
            this.gbSearchGameTitle.Size = new System.Drawing.Size(957, 54);
            this.gbSearchGameTitle.TabIndex = 0;
            this.gbSearchGameTitle.TabStop = false;
            this.gbSearchGameTitle.Text = "Search Game Title";
            // 
            // tableLayoutPanel19
            // 
            this.tableLayoutPanel19.ColumnCount = 6;
            this.tableLayoutPanel19.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel19.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 125F));
            this.tableLayoutPanel19.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 140F));
            this.tableLayoutPanel19.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 220F));
            this.tableLayoutPanel19.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            this.tableLayoutPanel19.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 135F));
            this.tableLayoutPanel19.Controls.Add(this.lblSearchGame, 1, 0);
            this.tableLayoutPanel19.Controls.Add(this.txtAddMachineSearch, 2, 0);
            this.tableLayoutPanel19.Controls.Add(this.cmbMachineSearch, 3, 0);
            this.tableLayoutPanel19.Controls.Add(this.btnAddGameTitle, 4, 0);
            this.tableLayoutPanel19.Controls.Add(this.btnRemoveGameTitle, 5, 0);
            this.tableLayoutPanel19.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel19.Location = new System.Drawing.Point(3, 17);
            this.tableLayoutPanel19.Name = "tableLayoutPanel19";
            this.tableLayoutPanel19.RowCount = 1;
            this.tableLayoutPanel19.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel19.Size = new System.Drawing.Size(951, 34);
            this.tableLayoutPanel19.TabIndex = 0;
            // 
            // lblSearchGame
            // 
            this.lblSearchGame.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSearchGame.AutoSize = true;
            this.lblSearchGame.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSearchGame.Location = new System.Drawing.Point(204, 10);
            this.lblSearchGame.Name = "lblSearchGame";
            this.lblSearchGame.Size = new System.Drawing.Size(119, 13);
            this.lblSearchGame.TabIndex = 15;
            this.lblSearchGame.Text = "Search Game Title:";
            // 
            // txtAddMachineSearch
            // 
            this.txtAddMachineSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAddMachineSearch.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAddMachineSearch.Location = new System.Drawing.Point(329, 6);
            this.txtAddMachineSearch.Name = "txtAddMachineSearch";
            this.txtAddMachineSearch.Size = new System.Drawing.Size(134, 21);
            this.txtAddMachineSearch.TabIndex = 0;
            this.txtAddMachineSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtAddMachineSearch_KeyDown);
            // 
            // cmbMachineSearch
            // 
            this.cmbMachineSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbMachineSearch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMachineSearch.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbMachineSearch.FormattingEnabled = true;
            this.cmbMachineSearch.Location = new System.Drawing.Point(469, 6);
            this.cmbMachineSearch.Name = "cmbMachineSearch";
            this.cmbMachineSearch.Size = new System.Drawing.Size(214, 21);
            this.cmbMachineSearch.TabIndex = 1;
            this.cmbMachineSearch.SelectedIndexChanged += new System.EventHandler(this.cmbMachineSearch_SelectedIndexChanged);
            // 
            // btnAddGameTitle
            // 
            this.btnAddGameTitle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddGameTitle.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddGameTitle.Location = new System.Drawing.Point(689, 3);
            this.btnAddGameTitle.Name = "btnAddGameTitle";
            this.btnAddGameTitle.Size = new System.Drawing.Size(124, 28);
            this.btnAddGameTitle.TabIndex = 2;
            this.btnAddGameTitle.Text = "Add Game Title";
            this.btnAddGameTitle.UseVisualStyleBackColor = true;
            this.btnAddGameTitle.Click += new System.EventHandler(this.btnAddGameTitle_Click);
            // 
            // btnRemoveGameTitle
            // 
            this.btnRemoveGameTitle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemoveGameTitle.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRemoveGameTitle.Location = new System.Drawing.Point(819, 3);
            this.btnRemoveGameTitle.Name = "btnRemoveGameTitle";
            this.btnRemoveGameTitle.Size = new System.Drawing.Size(129, 28);
            this.btnRemoveGameTitle.TabIndex = 3;
            this.btnRemoveGameTitle.Text = "Remove Game Title";
            this.btnRemoveGameTitle.UseVisualStyleBackColor = true;
            this.btnRemoveGameTitle.Click += new System.EventHandler(this.btnRemoveGameTitle_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.tableLayoutPanel22);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox4.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.Location = new System.Drawing.Point(3, 547);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(957, 54);
            this.groupBox4.TabIndex = 1;
            this.groupBox4.TabStop = false;
            // 
            // tableLayoutPanel22
            // 
            this.tableLayoutPanel22.ColumnCount = 4;
            this.tableLayoutPanel22.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel22.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            this.tableLayoutPanel22.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            this.tableLayoutPanel22.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            this.tableLayoutPanel22.Controls.Add(this.btnClose, 3, 0);
            this.tableLayoutPanel22.Controls.Add(this.btnApplyMachines, 1, 0);
            this.tableLayoutPanel22.Controls.Add(this.btnExport, 2, 0);
            this.tableLayoutPanel22.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel22.Location = new System.Drawing.Point(3, 17);
            this.tableLayoutPanel22.Name = "tableLayoutPanel22";
            this.tableLayoutPanel22.RowCount = 1;
            this.tableLayoutPanel22.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel22.Size = new System.Drawing.Size(951, 34);
            this.tableLayoutPanel22.TabIndex = 0;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Location = new System.Drawing.Point(824, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(124, 28);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnScheduleClose_Click);
            // 
            // btnApplyMachines
            // 
            this.btnApplyMachines.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnApplyMachines.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnApplyMachines.Location = new System.Drawing.Point(564, 3);
            this.btnApplyMachines.Name = "btnApplyMachines";
            this.btnApplyMachines.Size = new System.Drawing.Size(124, 27);
            this.btnApplyMachines.TabIndex = 0;
            this.btnApplyMachines.Text = "Apply";
            this.btnApplyMachines.UseVisualStyleBackColor = true;
            this.btnApplyMachines.Click += new System.EventHandler(this.btnApplyMachines_Click);
            // 
            // btnExport
            // 
            this.btnExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExport.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExport.Location = new System.Drawing.Point(694, 3);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(124, 28);
            this.btnExport.TabIndex = 1;
            this.btnExport.Text = "Export";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // gbMachineBands
            // 
            this.gbMachineBands.Controls.Add(this.tableLayoutPanel23);
            this.gbMachineBands.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbMachineBands.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbMachineBands.Location = new System.Drawing.Point(3, 407);
            this.gbMachineBands.Name = "gbMachineBands";
            this.gbMachineBands.Size = new System.Drawing.Size(957, 74);
            this.gbMachineBands.TabIndex = 2;
            this.gbMachineBands.TabStop = false;
            this.gbMachineBands.Text = "Bands";
            // 
            // tableLayoutPanel23
            // 
            this.tableLayoutPanel23.ColumnCount = 7;
            this.tableLayoutPanel23.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel23.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 102F));
            this.tableLayoutPanel23.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            this.tableLayoutPanel23.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            this.tableLayoutPanel23.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            this.tableLayoutPanel23.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            this.tableLayoutPanel23.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            this.tableLayoutPanel23.Controls.Add(this.lblMachineBands, 1, 1);
            this.tableLayoutPanel23.Controls.Add(this.btnMCBandFuturetoCurrent, 6, 1);
            this.tableLayoutPanel23.Controls.Add(this.btnMCBandCurrenttoPast, 5, 1);
            this.tableLayoutPanel23.Controls.Add(this.cmbMachineBandsCurrent, 2, 1);
            this.tableLayoutPanel23.Controls.Add(this.cmbMachineBandsPast, 1, 1);
            this.tableLayoutPanel23.Controls.Add(this.cmbMachineBandsFuture, 3, 1);
            this.tableLayoutPanel23.Controls.Add(this.lblMachineBandsPast, 2, 0);
            this.tableLayoutPanel23.Controls.Add(this.lblMachineBandsCurrent, 3, 0);
            this.tableLayoutPanel23.Controls.Add(this.lblMachineBandsFuture, 4, 0);
            this.tableLayoutPanel23.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel23.Location = new System.Drawing.Point(3, 17);
            this.tableLayoutPanel23.Name = "tableLayoutPanel23";
            this.tableLayoutPanel23.RowCount = 2;
            this.tableLayoutPanel23.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel23.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel23.Size = new System.Drawing.Size(951, 54);
            this.tableLayoutPanel23.TabIndex = 0;
            // 
            // lblMachineBands
            // 
            this.lblMachineBands.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMachineBands.AutoSize = true;
            this.lblMachineBands.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMachineBands.Location = new System.Drawing.Point(202, 31);
            this.lblMachineBands.Name = "lblMachineBands";
            this.lblMachineBands.Size = new System.Drawing.Size(96, 13);
            this.lblMachineBands.TabIndex = 14;
            this.lblMachineBands.Text = "Bands:";
            // 
            // btnMCBandFuturetoCurrent
            // 
            this.btnMCBandFuturetoCurrent.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMCBandFuturetoCurrent.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMCBandFuturetoCurrent.Location = new System.Drawing.Point(824, 24);
            this.btnMCBandFuturetoCurrent.Name = "btnMCBandFuturetoCurrent";
            this.btnMCBandFuturetoCurrent.Size = new System.Drawing.Size(124, 27);
            this.btnMCBandFuturetoCurrent.TabIndex = 4;
            this.btnMCBandFuturetoCurrent.Text = "Current <-- Future";
            this.btnMCBandFuturetoCurrent.UseVisualStyleBackColor = true;
            this.btnMCBandFuturetoCurrent.Click += new System.EventHandler(this.btnMCBandFuturetoCurrent_Click);
            // 
            // btnMCBandCurrenttoPast
            // 
            this.btnMCBandCurrenttoPast.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMCBandCurrenttoPast.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMCBandCurrenttoPast.Location = new System.Drawing.Point(694, 24);
            this.btnMCBandCurrenttoPast.Name = "btnMCBandCurrenttoPast";
            this.btnMCBandCurrenttoPast.Size = new System.Drawing.Size(124, 27);
            this.btnMCBandCurrenttoPast.TabIndex = 3;
            this.btnMCBandCurrenttoPast.Text = "Past <-- Current";
            this.btnMCBandCurrenttoPast.UseVisualStyleBackColor = true;
            this.btnMCBandCurrenttoPast.Click += new System.EventHandler(this.btnMCBandCurrenttoPast_Click);
            // 
            // cmbMachineBandsCurrent
            // 
            this.cmbMachineBandsCurrent.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbMachineBandsCurrent.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMachineBandsCurrent.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbMachineBandsCurrent.FormattingEnabled = true;
            this.cmbMachineBandsCurrent.Location = new System.Drawing.Point(434, 27);
            this.cmbMachineBandsCurrent.Name = "cmbMachineBandsCurrent";
            this.cmbMachineBandsCurrent.Size = new System.Drawing.Size(124, 21);
            this.cmbMachineBandsCurrent.TabIndex = 1;
            // 
            // cmbMachineBandsPast
            // 
            this.cmbMachineBandsPast.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbMachineBandsPast.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMachineBandsPast.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbMachineBandsPast.FormattingEnabled = true;
            this.cmbMachineBandsPast.Location = new System.Drawing.Point(304, 27);
            this.cmbMachineBandsPast.Name = "cmbMachineBandsPast";
            this.cmbMachineBandsPast.Size = new System.Drawing.Size(124, 21);
            this.cmbMachineBandsPast.TabIndex = 0;
            // 
            // cmbMachineBandsFuture
            // 
            this.cmbMachineBandsFuture.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbMachineBandsFuture.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMachineBandsFuture.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbMachineBandsFuture.FormattingEnabled = true;
            this.cmbMachineBandsFuture.Location = new System.Drawing.Point(564, 27);
            this.cmbMachineBandsFuture.Name = "cmbMachineBandsFuture";
            this.cmbMachineBandsFuture.Size = new System.Drawing.Size(124, 21);
            this.cmbMachineBandsFuture.TabIndex = 2;
            // 
            // lblMachineBandsPast
            // 
            this.lblMachineBandsPast.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMachineBandsPast.AutoSize = true;
            this.lblMachineBandsPast.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMachineBandsPast.Location = new System.Drawing.Point(304, 4);
            this.lblMachineBandsPast.Name = "lblMachineBandsPast";
            this.lblMachineBandsPast.Size = new System.Drawing.Size(124, 13);
            this.lblMachineBandsPast.TabIndex = 18;
            this.lblMachineBandsPast.Text = "Past";
            // 
            // lblMachineBandsCurrent
            // 
            this.lblMachineBandsCurrent.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMachineBandsCurrent.AutoSize = true;
            this.lblMachineBandsCurrent.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMachineBandsCurrent.Location = new System.Drawing.Point(434, 4);
            this.lblMachineBandsCurrent.Name = "lblMachineBandsCurrent";
            this.lblMachineBandsCurrent.Size = new System.Drawing.Size(124, 13);
            this.lblMachineBandsCurrent.TabIndex = 19;
            this.lblMachineBandsCurrent.Text = "Current";
            // 
            // lblMachineBandsFuture
            // 
            this.lblMachineBandsFuture.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMachineBandsFuture.AutoSize = true;
            this.lblMachineBandsFuture.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMachineBandsFuture.Location = new System.Drawing.Point(564, 4);
            this.lblMachineBandsFuture.Name = "lblMachineBandsFuture";
            this.lblMachineBandsFuture.Size = new System.Drawing.Size(124, 13);
            this.lblMachineBandsFuture.TabIndex = 20;
            this.lblMachineBandsFuture.Text = "Future";
            // 
            // gbMachineDates
            // 
            this.gbMachineDates.Controls.Add(this.tableLayoutPanel24);
            this.gbMachineDates.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbMachineDates.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbMachineDates.Location = new System.Drawing.Point(3, 487);
            this.gbMachineDates.Name = "gbMachineDates";
            this.gbMachineDates.Size = new System.Drawing.Size(957, 54);
            this.gbMachineDates.TabIndex = 3;
            this.gbMachineDates.TabStop = false;
            this.gbMachineDates.Text = "Dates";
            // 
            // tableLayoutPanel24
            // 
            this.tableLayoutPanel24.ColumnCount = 5;
            this.tableLayoutPanel24.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel24.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            this.tableLayoutPanel24.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            this.tableLayoutPanel24.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            this.tableLayoutPanel24.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            this.tableLayoutPanel24.Controls.Add(this.lblMachineChangeDates, 1, 0);
            this.tableLayoutPanel24.Controls.Add(this.DTPickerMachinesFuture, 2, 0);
            this.tableLayoutPanel24.Controls.Add(this.DTPickerMachinesPast, 1, 0);
            this.tableLayoutPanel24.Controls.Add(this.btnApplyDatesMachines, 3, 0);
            this.tableLayoutPanel24.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel24.Location = new System.Drawing.Point(3, 17);
            this.tableLayoutPanel24.Name = "tableLayoutPanel24";
            this.tableLayoutPanel24.RowCount = 1;
            this.tableLayoutPanel24.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel24.Size = new System.Drawing.Size(951, 34);
            this.tableLayoutPanel24.TabIndex = 0;
            // 
            // lblMachineChangeDates
            // 
            this.lblMachineChangeDates.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMachineChangeDates.AutoSize = true;
            this.lblMachineChangeDates.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMachineChangeDates.Location = new System.Drawing.Point(434, 10);
            this.lblMachineChangeDates.Name = "lblMachineChangeDates";
            this.lblMachineChangeDates.Size = new System.Drawing.Size(124, 13);
            this.lblMachineChangeDates.TabIndex = 66;
            this.lblMachineChangeDates.Text = "Change Dates:";
            // 
            // DTPickerMachinesFuture
            // 
            this.DTPickerMachinesFuture.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.DTPickerMachinesFuture.CustomFormat = "dd/MM/yyyy";
            this.DTPickerMachinesFuture.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DTPickerMachinesFuture.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DTPickerMachinesFuture.Location = new System.Drawing.Point(694, 6);
            this.DTPickerMachinesFuture.Name = "DTPickerMachinesFuture";
            this.DTPickerMachinesFuture.Size = new System.Drawing.Size(124, 21);
            this.DTPickerMachinesFuture.TabIndex = 1;
            // 
            // DTPickerMachinesPast
            // 
            this.DTPickerMachinesPast.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.DTPickerMachinesPast.CustomFormat = "dd/MM/yyyy";
            this.DTPickerMachinesPast.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DTPickerMachinesPast.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DTPickerMachinesPast.Location = new System.Drawing.Point(564, 6);
            this.DTPickerMachinesPast.Name = "DTPickerMachinesPast";
            this.DTPickerMachinesPast.Size = new System.Drawing.Size(124, 21);
            this.DTPickerMachinesPast.TabIndex = 0;
            // 
            // btnApplyDatesMachines
            // 
            this.btnApplyDatesMachines.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnApplyDatesMachines.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnApplyDatesMachines.Location = new System.Drawing.Point(824, 3);
            this.btnApplyDatesMachines.Name = "btnApplyDatesMachines";
            this.btnApplyDatesMachines.Size = new System.Drawing.Size(124, 28);
            this.btnApplyDatesMachines.TabIndex = 2;
            this.btnApplyDatesMachines.Text = "Apply Dates";
            this.btnApplyDatesMachines.UseVisualStyleBackColor = true;
            this.btnApplyDatesMachines.Click += new System.EventHandler(this.btnApplyDatesMachines_Click);
            // 
            // gbMachineDetails
            // 
            this.gbMachineDetails.Controls.Add(this.lvMachines);
            this.gbMachineDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbMachineDetails.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbMachineDetails.Location = new System.Drawing.Point(3, 3);
            this.gbMachineDetails.Name = "gbMachineDetails";
            this.gbMachineDetails.Size = new System.Drawing.Size(957, 338);
            this.gbMachineDetails.TabIndex = 4;
            this.gbMachineDetails.TabStop = false;
            this.gbMachineDetails.Text = "Details";
            // 
            // lvMachines
            // 
            this.lvMachines.ClipboardCopyFormat = BMC.CoreLib.Win32.ListViewClipboardCopyFormat.Default;
            this.lvMachines.ClipboardCopyMode = BMC.CoreLib.Win32.ListViewClipboardCopyMode.Disable;
            this.lvMachines.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chdrGameTitle,
            this.chdrPastBand,
            this.chdrPastChangeDate,
            this.chdrCurrentBand,
            this.chdrFutureChangeDate,
            this.chdrFutureBand});
            this.lvMachines.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvMachines.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvMachines.FullRowSelect = true;
            this.lvMachines.GridLines = true;
            this.lvMachines.HideSelection = false;
            this.lvMachines.Location = new System.Drawing.Point(3, 17);
            this.lvMachines.Name = "lvMachines";
            this.lvMachines.Size = new System.Drawing.Size(951, 318);
            this.lvMachines.TabIndex = 0;
            this.lvMachines.TabStop = false;
            this.lvMachines.UseCompatibleStateImageBehavior = false;
            this.lvMachines.View = System.Windows.Forms.View.Details;
            this.lvMachines.SelectedIndexChanged += new System.EventHandler(this.lvMachines_SelectedIndexChanged);
            this.lvMachines.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lvMachines_MouseClick);
            // 
            // chdrGameTitle
            // 
            this.chdrGameTitle.Text = "Game Title";
            // 
            // chdrPastBand
            // 
            this.chdrPastBand.Text = "Past Band";
            // 
            // chdrPastChangeDate
            // 
            this.chdrPastChangeDate.Text = "Change Date";
            // 
            // chdrCurrentBand
            // 
            this.chdrCurrentBand.Text = "Current Band";
            // 
            // chdrFutureChangeDate
            // 
            this.chdrFutureChangeDate.Text = "Change Date";
            // 
            // chdrFutureBand
            // 
            this.chdrFutureBand.Text = "Future Band";
            // 
            // frmAddShareSchedule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(977, 636);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAddShareSchedule";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Share Schedule";
            this.Load += new System.EventHandler(this.frmAddShareSchedule_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tcShareSchedule.ResumeLayout(false);
            this.tpSchedule.ResumeLayout(false);
            this.tableLayoutPanel14.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel11.ResumeLayout(false);
            this.gbSchedule.ResumeLayout(false);
            this.tableLayoutPanel12.ResumeLayout(false);
            this.tableLayoutPanel12.PerformLayout();
            this.tableLayoutPanel25.ResumeLayout(false);
            this.tableLayoutPanel25.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanel15.ResumeLayout(false);
            this.tpBands.ResumeLayout(false);
            this.tableLayoutPanel26.ResumeLayout(false);
            this.groupBox9.ResumeLayout(false);
            this.tableLayoutPanel27.ResumeLayout(false);
            this.gbBandDates.ResumeLayout(false);
            this.tableLayoutPanel28.ResumeLayout(false);
            this.tableLayoutPanel28.PerformLayout();
            this.gbBandShares.ResumeLayout(false);
            this.tableLayoutPanel29.ResumeLayout(false);
            this.tableLayoutPanel29.PerformLayout();
            this.gbBandDetails.ResumeLayout(false);
            this.tpMachines.ResumeLayout(false);
            this.tableLayoutPanel9.ResumeLayout(false);
            this.gbSearchGameTitle.ResumeLayout(false);
            this.tableLayoutPanel19.ResumeLayout(false);
            this.tableLayoutPanel19.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.tableLayoutPanel22.ResumeLayout(false);
            this.gbMachineBands.ResumeLayout(false);
            this.tableLayoutPanel23.ResumeLayout(false);
            this.tableLayoutPanel23.PerformLayout();
            this.gbMachineDates.ResumeLayout(false);
            this.tableLayoutPanel24.ResumeLayout(false);
            this.tableLayoutPanel24.PerformLayout();
            this.gbMachineDetails.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TabControl tcShareSchedule;
        private System.Windows.Forms.TabPage tpSchedule;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel11;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel12;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label lblNoofBands;
        private System.Windows.Forms.TextBox txtNoofBands;
        private System.Windows.Forms.Label lblBandCountType;
        private System.Windows.Forms.RadioButton rdbAlpha;
        private System.Windows.Forms.RadioButton rdbNumeric;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel14;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel15;
        private System.Windows.Forms.Button btnCancelSchedule;
        private System.Windows.Forms.Button btnUpdateSchedule;
        private System.Windows.Forms.Button btnEditSchedule;
        private System.Windows.Forms.TabPage tpMachines;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel9;
        private System.Windows.Forms.GroupBox gbSearchGameTitle;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel19;
        private System.Windows.Forms.Label lblSearchGame;
        private System.Windows.Forms.TextBox txtAddMachineSearch;
        private System.Windows.Forms.ComboBox cmbMachineSearch;
        private System.Windows.Forms.Button btnAddGameTitle;
        private System.Windows.Forms.Button btnRemoveGameTitle;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel22;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.GroupBox gbMachineBands;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel23;
        private System.Windows.Forms.Label lblMachineBands;
        private System.Windows.Forms.Button btnApplyMachines;
        private System.Windows.Forms.Button btnMCBandFuturetoCurrent;
        private System.Windows.Forms.Button btnMCBandCurrenttoPast;
        private System.Windows.Forms.ComboBox cmbMachineBandsCurrent;
        private System.Windows.Forms.ComboBox cmbMachineBandsPast;
        private System.Windows.Forms.ComboBox cmbMachineBandsFuture;
        private System.Windows.Forms.Label lblMachineBandsPast;
        private System.Windows.Forms.Label lblMachineBandsCurrent;
        private System.Windows.Forms.Label lblMachineBandsFuture;
        private System.Windows.Forms.GroupBox gbMachineDates;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel24;
        private System.Windows.Forms.Label lblMachineChangeDates;
        private System.Windows.Forms.DateTimePicker DTPickerMachinesFuture;
        private System.Windows.Forms.DateTimePicker DTPickerMachinesPast;
        private System.Windows.Forms.Button btnApplyDatesMachines;
        private System.Windows.Forms.GroupBox gbMachineDetails;
        private System.Windows.Forms.GroupBox gbSchedule;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel25;
        private System.Windows.Forms.TabPage tpBands;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel26;
        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel27;
        private System.Windows.Forms.Button btnBandsClose;
        private System.Windows.Forms.Button btnApplyBands;
        private System.Windows.Forms.GroupBox gbBandDates;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel28;
        private System.Windows.Forms.Label lblChange;
        private System.Windows.Forms.DateTimePicker dtpBandsFuture;
        private System.Windows.Forms.DateTimePicker dtpBandsPast;
        private System.Windows.Forms.Button btnApplyDates;
        private System.Windows.Forms.GroupBox gbBandShares;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel29;
        private System.Windows.Forms.Label lblCompany;
        private System.Windows.Forms.Label lblSupplier;
        private System.Windows.Forms.Label lblSite;
        private System.Windows.Forms.MaskedTextBox txtPastSupplierShare;
        private System.Windows.Forms.MaskedTextBox txtPastSiteshare;
        private System.Windows.Forms.MaskedTextBox txtSupplierShare;
        private System.Windows.Forms.MaskedTextBox txtPastCompanyshare;
        private System.Windows.Forms.MaskedTextBox txtSiteshare;
        private System.Windows.Forms.MaskedTextBox txtCompanyshare;
        private System.Windows.Forms.MaskedTextBox txtFutureCompanyshare;
        private System.Windows.Forms.MaskedTextBox txtFutureSupplierShare;
        private System.Windows.Forms.MaskedTextBox txtFutureSiteshare;
        private System.Windows.Forms.CheckBox chkSupplierShareGuaranteedPast;
        private System.Windows.Forms.CheckBox chkSiteShareGuaranteedPast;
        private System.Windows.Forms.CheckBox chkSupplierShareGuaranteed;
        private System.Windows.Forms.CheckBox chkSiteShareGuaranteed;
        private System.Windows.Forms.CheckBox chkSupplierShareGuaranteedFuture;
        private System.Windows.Forms.CheckBox chkSiteShareGuaranteedFuture;
        private System.Windows.Forms.CheckBox chkCompanyShareGuaranteedFuture;
        private System.Windows.Forms.CheckBox chkCompanyShareGuaranteedPast;
        private System.Windows.Forms.CheckBox chkCompanyShareGuaranteed;
        private System.Windows.Forms.Label lblPastShares;
        private System.Windows.Forms.Label lblCurrentShares;
        private System.Windows.Forms.Label lblPastSharesGuaranteed;
        private System.Windows.Forms.Label lblCurrentSharesGuaranteed;
        private System.Windows.Forms.Label lblFutureSharesGuaranteed;
        private System.Windows.Forms.Label lblFutureShares;
        private System.Windows.Forms.Button btnBandFuturetoCurrent;
        private System.Windows.Forms.Button btnBandsCurrentToPast;
        private System.Windows.Forms.GroupBox gbBandDetails;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnScheduleClose;
        private CoreLib.Win32.ListViewEx lvBands;
        private System.Windows.Forms.ColumnHeader chdrBandName;
        private System.Windows.Forms.ColumnHeader chdrOperatorPast;
        private System.Windows.Forms.ColumnHeader chdrSitePast;
        private System.Windows.Forms.ColumnHeader chdrCompanyPast;
        private System.Windows.Forms.ColumnHeader chdrBandPastDate;
        private System.Windows.Forms.ColumnHeader chdrOperator;
        private System.Windows.Forms.ColumnHeader chdrSite;
        private System.Windows.Forms.ColumnHeader chdrCompany;
        private System.Windows.Forms.ColumnHeader chdrBandFutureDate;
        private System.Windows.Forms.ColumnHeader chdrOperatorFuture;
        private System.Windows.Forms.ColumnHeader chdrSiteFuture;
        private System.Windows.Forms.ColumnHeader chdrCompanyFuture;
        private CoreLib.Win32.ListViewEx lvMachines;
        private System.Windows.Forms.ColumnHeader chdrGameTitle;
        private System.Windows.Forms.ColumnHeader chdrPastBand;
        private System.Windows.Forms.ColumnHeader chdrPastChangeDate;
        private System.Windows.Forms.ColumnHeader chdrCurrentBand;
        private System.Windows.Forms.ColumnHeader chdrFutureChangeDate;
        private System.Windows.Forms.ColumnHeader chdrFutureBand;
    }
}

