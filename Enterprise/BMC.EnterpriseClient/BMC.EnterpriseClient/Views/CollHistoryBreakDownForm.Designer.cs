namespace BMC.EnterpriseClient.Views
{
    partial class CollHistoryBreakDownForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CollHistoryBreakDownForm));
            BMC.CoreLib.Win32.ComboBoxItem<int> comboBoxItem_11 = new BMC.CoreLib.Win32.ComboBoxItem<int>();
            BMC.CoreLib.Win32.ComboBoxItem<int> comboBoxItem_12 = new BMC.CoreLib.Win32.ComboBoxItem<int>();
            BMC.CoreLib.Win32.ComboBoxItem<int> comboBoxItem_13 = new BMC.CoreLib.Win32.ComboBoxItem<int>();
            BMC.CoreLib.Win32.ComboBoxItem<int> comboBoxItem_14 = new BMC.CoreLib.Win32.ComboBoxItem<int>();
            BMC.CoreLib.Win32.ComboBoxItem<int> comboBoxItem_15 = new BMC.CoreLib.Win32.ComboBoxItem<int>();
            BMC.CoreLib.Win32.ComboBoxItem<int> comboBoxItem_16 = new BMC.CoreLib.Win32.ComboBoxItem<int>();
            BMC.CoreLib.Win32.ComboBoxItem<int> comboBoxItem_17 = new BMC.CoreLib.Win32.ComboBoxItem<int>();
            BMC.CoreLib.Win32.ComboBoxItem<int> comboBoxItem_18 = new BMC.CoreLib.Win32.ComboBoxItem<int>();
            BMC.CoreLib.Win32.ComboBoxItem<int> comboBoxItem_19 = new BMC.CoreLib.Win32.ComboBoxItem<int>();
            BMC.CoreLib.Win32.ComboBoxItem<int> comboBoxItem_110 = new BMC.CoreLib.Win32.ComboBoxItem<int>();
            BMC.CoreLib.Win32.ComboBoxItem<int> comboBoxItem_111 = new BMC.CoreLib.Win32.ComboBoxItem<int>();
            BMC.CoreLib.Win32.ComboBoxItem<int> comboBoxItem_112 = new BMC.CoreLib.Win32.ComboBoxItem<int>();
            this.grpTitle = new System.Windows.Forms.GroupBox();
            this.tblContainer = new System.Windows.Forms.TableLayoutPanel();
            this.tabDetails = new System.Windows.Forms.TabControl();
            this.tbpDropMeterBreakdown = new System.Windows.Forms.TabPage();
            this.tblDropMeterBreakdown = new System.Windows.Forms.TableLayoutPanel();
            this.lvDropBreakDown = new BMC.CoreLib.Win32.ListViewEx();
            this.imglstSmallIcons = new System.Windows.Forms.ImageList(this.components);
            this.btn_ModifyDeclaration = new System.Windows.Forms.Button();
            this.tbpVarianceHistory = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tblHeader = new System.Windows.Forms.TableLayoutPanel();
            this.cmbPeriodCount = new System.Windows.Forms.ComboBox();
            this.lblVarLast = new System.Windows.Forms.Label();
            this.lblVarRecords = new System.Windows.Forms.Label();
            this.lvwVarianceHistory = new BMC.CoreLib.Win32.ListViewEx();
            this.clmCollectionDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmCoinVar = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmBillsVar = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmVoucherInVar = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmVoucherOutVar = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmEFTInVar = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmEFTOutVar = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmAttendantpayVar = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmAttendantpayProgressiveVar = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ClmAsset = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ClmDeclaredBy = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.grpCashDeskTrans = new System.Windows.Forms.GroupBox();
            this.lvwCashDeskTrans = new BMC.CoreLib.Win32.ListViewEx();
            this.clmDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmEnteredBy = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmIssuedBy = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmAmount = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmComment = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.grpDropSummaries = new System.Windows.Forms.GroupBox();
            this.lvwDropSummaries = new BMC.CoreLib.Win32.ListViewEx();
            this.tblDetails = new System.Windows.Forms.TableLayoutPanel();
            this.grpDropDetails = new System.Windows.Forms.GroupBox();
            this.tblDropDetails = new System.Windows.Forms.TableLayoutPanel();
            this.txtCollectionDate = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblGamingDay = new System.Windows.Forms.Label();
            this.txtGamingDay = new System.Windows.Forms.TextBox();
            this.lblUser = new System.Windows.Forms.Label();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.txt_Declaredby = new System.Windows.Forms.TextBox();
            this.lbl_Declaredby = new System.Windows.Forms.Label();
            this.grpInstallationDetails = new System.Windows.Forms.GroupBox();
            this.tblInstallationDetails = new System.Windows.Forms.TableLayoutPanel();
            this.txt_Pos = new System.Windows.Forms.TextBox();
            this.lblPos = new System.Windows.Forms.Label();
            this.lblZone = new System.Windows.Forms.Label();
            this.txtZone = new System.Windows.Forms.TextBox();
            this.lblGame = new System.Windows.Forms.Label();
            this.txt_Game = new System.Windows.Forms.TextBox();
            this.lbl_Asset = new System.Windows.Forms.Label();
            this.txt_Asset = new System.Windows.Forms.TextBox();
            this.tblPosEvents = new System.Windows.Forms.TableLayoutPanel();
            this.btnReturn = new System.Windows.Forms.Button();
            this.grpPositionEvents = new System.Windows.Forms.GroupBox();
            this.lvwPositionEvents = new BMC.CoreLib.Win32.ListViewEx();
            this.clm_Type = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clm_Date = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clm_Time = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clm_Duration = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clm_Description = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.grpTitle.SuspendLayout();
            this.tblContainer.SuspendLayout();
            this.tabDetails.SuspendLayout();
            this.tbpDropMeterBreakdown.SuspendLayout();
            this.tblDropMeterBreakdown.SuspendLayout();
            this.tbpVarianceHistory.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tblHeader.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.grpCashDeskTrans.SuspendLayout();
            this.grpDropSummaries.SuspendLayout();
            this.tblDetails.SuspendLayout();
            this.grpDropDetails.SuspendLayout();
            this.tblDropDetails.SuspendLayout();
            this.grpInstallationDetails.SuspendLayout();
            this.tblInstallationDetails.SuspendLayout();
            this.tblPosEvents.SuspendLayout();
            this.grpPositionEvents.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpTitle
            // 
            this.grpTitle.Controls.Add(this.tblContainer);
            this.grpTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpTitle.Location = new System.Drawing.Point(3, 3);
            this.grpTitle.Name = "grpTitle";
            this.grpTitle.Size = new System.Drawing.Size(1012, 612);
            this.grpTitle.TabIndex = 0;
            this.grpTitle.TabStop = false;
            this.grpTitle.Text = "Drop Breakdown";
            // 
            // tblContainer
            // 
            this.tblContainer.ColumnCount = 1;
            this.tblContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblContainer.Controls.Add(this.tabDetails, 0, 2);
            this.tblContainer.Controls.Add(this.tableLayoutPanel1, 0, 1);
            this.tblContainer.Controls.Add(this.tblDetails, 0, 0);
            this.tblContainer.Controls.Add(this.tblPosEvents, 0, 3);
            this.tblContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblContainer.Location = new System.Drawing.Point(3, 16);
            this.tblContainer.Margin = new System.Windows.Forms.Padding(0);
            this.tblContainer.Name = "tblContainer";
            this.tblContainer.RowCount = 4;
            this.tblContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 98F));
            this.tblContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 127F));
            this.tblContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tblContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tblContainer.Size = new System.Drawing.Size(1006, 593);
            this.tblContainer.TabIndex = 0;
            // 
            // tabDetails
            // 
            this.tabDetails.Controls.Add(this.tbpDropMeterBreakdown);
            this.tabDetails.Controls.Add(this.tbpVarianceHistory);
            this.tabDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabDetails.Location = new System.Drawing.Point(3, 228);
            this.tabDetails.Name = "tabDetails";
            this.tabDetails.SelectedIndex = 0;
            this.tabDetails.Size = new System.Drawing.Size(1000, 251);
            this.tabDetails.TabIndex = 2;
            // 
            // tbpDropMeterBreakdown
            // 
            this.tbpDropMeterBreakdown.Controls.Add(this.tblDropMeterBreakdown);
            this.tbpDropMeterBreakdown.Location = new System.Drawing.Point(4, 22);
            this.tbpDropMeterBreakdown.Name = "tbpDropMeterBreakdown";
            this.tbpDropMeterBreakdown.Padding = new System.Windows.Forms.Padding(3);
            this.tbpDropMeterBreakdown.Size = new System.Drawing.Size(992, 225);
            this.tbpDropMeterBreakdown.TabIndex = 0;
            this.tbpDropMeterBreakdown.Text = "Declared/Metered Breakdown ";
            this.tbpDropMeterBreakdown.UseVisualStyleBackColor = true;
            // 
            // tblDropMeterBreakdown
            // 
            this.tblDropMeterBreakdown.ColumnCount = 1;
            this.tblDropMeterBreakdown.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblDropMeterBreakdown.Controls.Add(this.lvDropBreakDown, 0, 0);
            this.tblDropMeterBreakdown.Controls.Add(this.btn_ModifyDeclaration, 0, 1);
            this.tblDropMeterBreakdown.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblDropMeterBreakdown.Location = new System.Drawing.Point(3, 3);
            this.tblDropMeterBreakdown.Name = "tblDropMeterBreakdown";
            this.tblDropMeterBreakdown.RowCount = 2;
            this.tblDropMeterBreakdown.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblDropMeterBreakdown.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tblDropMeterBreakdown.Size = new System.Drawing.Size(986, 219);
            this.tblDropMeterBreakdown.TabIndex = 0;
            // 
            // lvDropBreakDown
            // 
            this.lvDropBreakDown.ClipboardCopyFormat = BMC.CoreLib.Win32.ListViewClipboardCopyFormat.Default;
            this.lvDropBreakDown.ClipboardCopyMode = BMC.CoreLib.Win32.ListViewClipboardCopyMode.Disable;
            this.lvDropBreakDown.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvDropBreakDown.FullRowSelect = true;
            this.lvDropBreakDown.GridLines = true;
            this.lvDropBreakDown.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lvDropBreakDown.HideSelection = false;
            this.lvDropBreakDown.Location = new System.Drawing.Point(0, 0);
            this.lvDropBreakDown.Margin = new System.Windows.Forms.Padding(0);
            this.lvDropBreakDown.Name = "lvDropBreakDown";
            this.lvDropBreakDown.Size = new System.Drawing.Size(986, 184);
            this.lvDropBreakDown.SmallImageList = this.imglstSmallIcons;
            this.lvDropBreakDown.TabIndex = 3;
            this.lvDropBreakDown.UseCompatibleStateImageBehavior = false;
            this.lvDropBreakDown.View = System.Windows.Forms.View.Details;
            this.lvDropBreakDown.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvDropBreakDown_ColumnClick);
            // 
            // imglstSmallIcons
            // 
            this.imglstSmallIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imglstSmallIcons.ImageStream")));
            this.imglstSmallIcons.TransparentColor = System.Drawing.Color.Transparent;
            this.imglstSmallIcons.Images.SetKeyName(0, "DollarBuilding.ico");
            this.imglstSmallIcons.Images.SetKeyName(1, "MoneyBag.ico");
            this.imglstSmallIcons.Images.SetKeyName(2, "Note.ico");
            this.imglstSmallIcons.Images.SetKeyName(3, "Slot.ico");
            this.imglstSmallIcons.Images.SetKeyName(4, "Variance.ico");
            this.imglstSmallIcons.Images.SetKeyName(5, "Settings.ico");
            this.imglstSmallIcons.Images.SetKeyName(6, "Door.ico");
            this.imglstSmallIcons.Images.SetKeyName(7, "Flash.ico");
            // 
            // btn_ModifyDeclaration
            // 
            this.btn_ModifyDeclaration.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_ModifyDeclaration.Location = new System.Drawing.Point(872, 193);
            this.btn_ModifyDeclaration.Name = "btn_ModifyDeclaration";
            this.btn_ModifyDeclaration.Size = new System.Drawing.Size(111, 23);
            this.btn_ModifyDeclaration.TabIndex = 4;
            this.btn_ModifyDeclaration.Text = "Modify Declaration";
            this.btn_ModifyDeclaration.UseVisualStyleBackColor = true;
            this.btn_ModifyDeclaration.Click += new System.EventHandler(this.btn_ModifyDeclaration_Click);
            // 
            // tbpVarianceHistory
            // 
            this.tbpVarianceHistory.Controls.Add(this.tableLayoutPanel2);
            this.tbpVarianceHistory.Location = new System.Drawing.Point(4, 22);
            this.tbpVarianceHistory.Name = "tbpVarianceHistory";
            this.tbpVarianceHistory.Size = new System.Drawing.Size(992, 225);
            this.tbpVarianceHistory.TabIndex = 3;
            this.tbpVarianceHistory.Text = "Variance History ";
            this.tbpVarianceHistory.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.tblHeader, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.lvwVarianceHistory, 0, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(992, 225);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // tblHeader
            // 
            this.tblHeader.ColumnCount = 4;
            this.tblHeader.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblHeader.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tblHeader.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tblHeader.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tblHeader.Controls.Add(this.cmbPeriodCount, 2, 0);
            this.tblHeader.Controls.Add(this.lblVarLast, 1, 0);
            this.tblHeader.Controls.Add(this.lblVarRecords, 3, 0);
            this.tblHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblHeader.Location = new System.Drawing.Point(3, 3);
            this.tblHeader.Name = "tblHeader";
            this.tblHeader.RowCount = 1;
            this.tblHeader.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblHeader.Size = new System.Drawing.Size(986, 29);
            this.tblHeader.TabIndex = 5;
            // 
            // cmbPeriodCount
            // 
            this.cmbPeriodCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbPeriodCount.DisplayMember = "Text";
            this.cmbPeriodCount.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPeriodCount.FormattingEnabled = true;
            comboBoxItem_11.Text = "-- ALL --";
            comboBoxItem_11.Value = 0;
            comboBoxItem_12.Text = "1";
            comboBoxItem_12.Value = 1;
            comboBoxItem_13.Text = "2";
            comboBoxItem_13.Value = 2;
            comboBoxItem_14.Text = "3";
            comboBoxItem_14.Value = 3;
            comboBoxItem_15.Text = "4";
            comboBoxItem_15.Value = 4;
            comboBoxItem_16.Text = "5";
            comboBoxItem_16.Value = 5;
            comboBoxItem_17.Text = "6";
            comboBoxItem_17.Value = 6;
            comboBoxItem_18.Text = "12";
            comboBoxItem_18.Value = 12;
            comboBoxItem_19.Text = "24";
            comboBoxItem_19.Value = 24;
            comboBoxItem_110.Text = "36";
            comboBoxItem_110.Value = 36;
            comboBoxItem_111.Text = "48";
            comboBoxItem_111.Value = 48;
            comboBoxItem_112.Text = "60";
            comboBoxItem_112.Value = 60;
            this.cmbPeriodCount.Items.AddRange(new object[] {
            comboBoxItem_11,
            comboBoxItem_12,
            comboBoxItem_13,
            comboBoxItem_14,
            comboBoxItem_15,
            comboBoxItem_16,
            comboBoxItem_17,
            comboBoxItem_18,
            comboBoxItem_19,
            comboBoxItem_110,
            comboBoxItem_111,
            comboBoxItem_112});
            this.cmbPeriodCount.Location = new System.Drawing.Point(789, 4);
            this.cmbPeriodCount.Name = "cmbPeriodCount";
            this.cmbPeriodCount.Size = new System.Drawing.Size(114, 21);
            this.cmbPeriodCount.TabIndex = 2;
            this.cmbPeriodCount.ValueMember = "Value";
            this.cmbPeriodCount.SelectedIndexChanged += new System.EventHandler(this.cmbPeriodCount_SelectedIndexChanged);
            // 
            // lblVarLast
            // 
            this.lblVarLast.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblVarLast.AutoSize = true;
            this.lblVarLast.Location = new System.Drawing.Point(756, 8);
            this.lblVarLast.Name = "lblVarLast";
            this.lblVarLast.Size = new System.Drawing.Size(27, 13);
            this.lblVarLast.TabIndex = 3;
            this.lblVarLast.Text = "Last";
            // 
            // lblVarRecords
            // 
            this.lblVarRecords.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblVarRecords.AutoSize = true;
            this.lblVarRecords.Location = new System.Drawing.Point(909, 8);
            this.lblVarRecords.Name = "lblVarRecords";
            this.lblVarRecords.Size = new System.Drawing.Size(47, 13);
            this.lblVarRecords.TabIndex = 4;
            this.lblVarRecords.Text = "Records";
            // 
            // lvwVarianceHistory
            // 
            this.lvwVarianceHistory.ClipboardCopyFormat = BMC.CoreLib.Win32.ListViewClipboardCopyFormat.Default;
            this.lvwVarianceHistory.ClipboardCopyMode = BMC.CoreLib.Win32.ListViewClipboardCopyMode.Disable;
            this.lvwVarianceHistory.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clmCollectionDate,
            this.clmCoinVar,
            this.clmBillsVar,
            this.clmVoucherInVar,
            this.clmVoucherOutVar,
            this.clmEFTInVar,
            this.clmEFTOutVar,
            this.clmAttendantpayVar,
            this.clmAttendantpayProgressiveVar,
            this.ClmAsset,
            this.ClmDeclaredBy});
            this.lvwVarianceHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvwVarianceHistory.FullRowSelect = true;
            this.lvwVarianceHistory.GridLines = true;
            this.lvwVarianceHistory.HideSelection = false;
            this.lvwVarianceHistory.Location = new System.Drawing.Point(0, 35);
            this.lvwVarianceHistory.Margin = new System.Windows.Forms.Padding(0);
            this.lvwVarianceHistory.Name = "lvwVarianceHistory";
            this.lvwVarianceHistory.Size = new System.Drawing.Size(992, 190);
            this.lvwVarianceHistory.TabIndex = 4;
            this.lvwVarianceHistory.UseCompatibleStateImageBehavior = false;
            this.lvwVarianceHistory.View = System.Windows.Forms.View.Details;
            this.lvwVarianceHistory.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvwVarianceHistory_ColumnClick);
            this.lvwVarianceHistory.ColumnWidthChanging += new System.Windows.Forms.ColumnWidthChangingEventHandler(this.lvwVarianceHistory_ColumnWidthChanging);
            // 
            // clmCollectionDate
            // 
            this.clmCollectionDate.Text = "Gaming Day";
            // 
            // clmCoinVar
            // 
            this.clmCoinVar.Text = "Coin Var";
            this.clmCoinVar.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // clmBillsVar
            // 
            this.clmBillsVar.Text = "Bills Var";
            this.clmBillsVar.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // clmVoucherInVar
            // 
            this.clmVoucherInVar.Text = "Voucher In Var";
            this.clmVoucherInVar.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // clmVoucherOutVar
            // 
            this.clmVoucherOutVar.Text = "Voucher Out Var";
            this.clmVoucherOutVar.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // clmEFTInVar
            // 
            this.clmEFTInVar.Text = "EFT In Var";
            this.clmEFTInVar.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // clmEFTOutVar
            // 
            this.clmEFTOutVar.Text = "EFT Out Var";
            this.clmEFTOutVar.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // clmAttendantpayVar
            // 
            this.clmAttendantpayVar.Text = "Attendantpay Var";
            this.clmAttendantpayVar.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // clmAttendantpayProgressiveVar
            // 
            this.clmAttendantpayProgressiveVar.Text = "Attendantpay Progressive Var";
            this.clmAttendantpayProgressiveVar.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // ClmAsset
            // 
            this.ClmAsset.Text = "Asset";
            this.ClmAsset.Width = 0;
            // 
            // ClmDeclaredBy
            // 
            this.ClmDeclaredBy.Width = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45.12922F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 54.87078F));
            this.tableLayoutPanel1.Controls.Add(this.grpCashDeskTrans, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.grpDropSummaries, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 98);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1006, 127);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // grpCashDeskTrans
            // 
            this.grpCashDeskTrans.Controls.Add(this.lvwCashDeskTrans);
            this.grpCashDeskTrans.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpCashDeskTrans.Location = new System.Drawing.Point(456, 3);
            this.grpCashDeskTrans.Name = "grpCashDeskTrans";
            this.grpCashDeskTrans.Padding = new System.Windows.Forms.Padding(5);
            this.grpCashDeskTrans.Size = new System.Drawing.Size(547, 121);
            this.grpCashDeskTrans.TabIndex = 1;
            this.grpCashDeskTrans.TabStop = false;
            this.grpCashDeskTrans.Text = "Cash Desk/Machine Transactions";
            // 
            // lvwCashDeskTrans
            // 
            this.lvwCashDeskTrans.ClipboardCopyFormat = BMC.CoreLib.Win32.ListViewClipboardCopyFormat.Default;
            this.lvwCashDeskTrans.ClipboardCopyMode = BMC.CoreLib.Win32.ListViewClipboardCopyMode.Disable;
            this.lvwCashDeskTrans.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clmDate,
            this.clmTime,
            this.clmType,
            this.clmEnteredBy,
            this.clmIssuedBy,
            this.clmAmount,
            this.clmComment});
            this.lvwCashDeskTrans.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvwCashDeskTrans.FullRowSelect = true;
            this.lvwCashDeskTrans.GridLines = true;
            this.lvwCashDeskTrans.HideSelection = false;
            this.lvwCashDeskTrans.Location = new System.Drawing.Point(5, 18);
            this.lvwCashDeskTrans.Name = "lvwCashDeskTrans";
            this.lvwCashDeskTrans.Size = new System.Drawing.Size(537, 98);
            this.lvwCashDeskTrans.SmallImageList = this.imglstSmallIcons;
            this.lvwCashDeskTrans.TabIndex = 0;
            this.lvwCashDeskTrans.UseCompatibleStateImageBehavior = false;
            this.lvwCashDeskTrans.View = System.Windows.Forms.View.Details;
            this.lvwCashDeskTrans.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvwCashDeskTrans_ColumnClick);
            // 
            // clmDate
            // 
            this.clmDate.Text = "Date";
            // 
            // clmTime
            // 
            this.clmTime.Text = "Time";
            // 
            // clmType
            // 
            this.clmType.Text = "Type";
            // 
            // clmEnteredBy
            // 
            this.clmEnteredBy.Text = "Entered By";
            this.clmEnteredBy.Width = 77;
            // 
            // clmIssuedBy
            // 
            this.clmIssuedBy.Text = "Issued By";
            this.clmIssuedBy.Width = 73;
            // 
            // clmAmount
            // 
            this.clmAmount.Text = "Amount";
            this.clmAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // clmComment
            // 
            this.clmComment.Text = "Comment";
            // 
            // grpDropSummaries
            // 
            this.grpDropSummaries.Controls.Add(this.lvwDropSummaries);
            this.grpDropSummaries.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpDropSummaries.Location = new System.Drawing.Point(3, 3);
            this.grpDropSummaries.Name = "grpDropSummaries";
            this.grpDropSummaries.Padding = new System.Windows.Forms.Padding(5);
            this.grpDropSummaries.Size = new System.Drawing.Size(447, 121);
            this.grpDropSummaries.TabIndex = 0;
            this.grpDropSummaries.TabStop = false;
            this.grpDropSummaries.Text = "Drop Summaries";
            // 
            // lvwDropSummaries
            // 
            this.lvwDropSummaries.ClipboardCopyFormat = BMC.CoreLib.Win32.ListViewClipboardCopyFormat.Default;
            this.lvwDropSummaries.ClipboardCopyMode = BMC.CoreLib.Win32.ListViewClipboardCopyMode.Disable;
            this.lvwDropSummaries.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvwDropSummaries.FullRowSelect = true;
            this.lvwDropSummaries.GridLines = true;
            this.lvwDropSummaries.HideSelection = false;
            this.lvwDropSummaries.Location = new System.Drawing.Point(5, 18);
            this.lvwDropSummaries.MultiSelect = false;
            this.lvwDropSummaries.Name = "lvwDropSummaries";
            this.lvwDropSummaries.Size = new System.Drawing.Size(437, 98);
            this.lvwDropSummaries.SmallImageList = this.imglstSmallIcons;
            this.lvwDropSummaries.TabIndex = 0;
            this.lvwDropSummaries.UseCompatibleStateImageBehavior = false;
            this.lvwDropSummaries.View = System.Windows.Forms.View.Details;
            // 
            // tblDetails
            // 
            this.tblDetails.ColumnCount = 2;
            this.tblDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45F));
            this.tblDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 55F));
            this.tblDetails.Controls.Add(this.grpDropDetails, 1, 0);
            this.tblDetails.Controls.Add(this.grpInstallationDetails, 0, 0);
            this.tblDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblDetails.Location = new System.Drawing.Point(0, 0);
            this.tblDetails.Margin = new System.Windows.Forms.Padding(0);
            this.tblDetails.Name = "tblDetails";
            this.tblDetails.RowCount = 1;
            this.tblDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblDetails.Size = new System.Drawing.Size(1006, 98);
            this.tblDetails.TabIndex = 0;
            // 
            // grpDropDetails
            // 
            this.grpDropDetails.Controls.Add(this.tblDropDetails);
            this.grpDropDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpDropDetails.Location = new System.Drawing.Point(455, 3);
            this.grpDropDetails.Name = "grpDropDetails";
            this.grpDropDetails.Size = new System.Drawing.Size(548, 92);
            this.grpDropDetails.TabIndex = 2;
            this.grpDropDetails.TabStop = false;
            this.grpDropDetails.Text = "Drop Details";
            // 
            // tblDropDetails
            // 
            this.tblDropDetails.ColumnCount = 5;
            this.tblDropDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tblDropDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 138F));
            this.tblDropDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 91F));
            this.tblDropDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 197F));
            this.tblDropDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tblDropDetails.Controls.Add(this.txtCollectionDate, 3, 0);
            this.tblDropDetails.Controls.Add(this.label1, 2, 0);
            this.tblDropDetails.Controls.Add(this.lblGamingDay, 0, 0);
            this.tblDropDetails.Controls.Add(this.txtGamingDay, 1, 0);
            this.tblDropDetails.Controls.Add(this.lblUser, 0, 1);
            this.tblDropDetails.Controls.Add(this.txtUser, 1, 1);
            this.tblDropDetails.Controls.Add(this.txt_Declaredby, 3, 1);
            this.tblDropDetails.Controls.Add(this.lbl_Declaredby, 2, 1);
            this.tblDropDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblDropDetails.Location = new System.Drawing.Point(3, 16);
            this.tblDropDetails.Margin = new System.Windows.Forms.Padding(0);
            this.tblDropDetails.Name = "tblDropDetails";
            this.tblDropDetails.RowCount = 2;
            this.tblDropDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblDropDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tblDropDetails.Size = new System.Drawing.Size(542, 73);
            this.tblDropDetails.TabIndex = 0;
            // 
            // txtCollectionDate
            // 
            this.txtCollectionDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCollectionDate.BackColor = System.Drawing.Color.White;
            this.txtCollectionDate.Location = new System.Drawing.Point(312, 11);
            this.txtCollectionDate.Name = "txtCollectionDate";
            this.txtCollectionDate.ReadOnly = true;
            this.txtCollectionDate.Size = new System.Drawing.Size(191, 20);
            this.txtCollectionDate.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(221, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Batch Date :";
            // 
            // lblGamingDay
            // 
            this.lblGamingDay.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblGamingDay.AutoSize = true;
            this.lblGamingDay.Location = new System.Drawing.Point(3, 15);
            this.lblGamingDay.Name = "lblGamingDay";
            this.lblGamingDay.Size = new System.Drawing.Size(71, 13);
            this.lblGamingDay.TabIndex = 0;
            this.lblGamingDay.Text = "Gaming Day :";
            // 
            // txtGamingDay
            // 
            this.txtGamingDay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtGamingDay.BackColor = System.Drawing.Color.White;
            this.txtGamingDay.Location = new System.Drawing.Point(83, 11);
            this.txtGamingDay.Name = "txtGamingDay";
            this.txtGamingDay.ReadOnly = true;
            this.txtGamingDay.Size = new System.Drawing.Size(132, 20);
            this.txtGamingDay.TabIndex = 1;
            this.txtGamingDay.Text = "31/12/9999 12:12";
            // 
            // lblUser
            // 
            this.lblUser.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblUser.AutoSize = true;
            this.lblUser.Location = new System.Drawing.Point(3, 51);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(35, 13);
            this.lblUser.TabIndex = 4;
            this.lblUser.Text = "User :";
            // 
            // txtUser
            // 
            this.txtUser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtUser.BackColor = System.Drawing.Color.White;
            this.txtUser.Location = new System.Drawing.Point(83, 48);
            this.txtUser.Name = "txtUser";
            this.txtUser.ReadOnly = true;
            this.txtUser.Size = new System.Drawing.Size(132, 20);
            this.txtUser.TabIndex = 5;
            this.txtUser.MouseLeave += new System.EventHandler(this.txtUser_MouseLeave);
            this.txtUser.MouseHover += new System.EventHandler(this.txtUser_MouseHover);
            // 
            // txt_Declaredby
            // 
            this.txt_Declaredby.BackColor = System.Drawing.Color.White;
            this.txt_Declaredby.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_Declaredby.Location = new System.Drawing.Point(312, 46);
            this.txt_Declaredby.Name = "txt_Declaredby";
            this.txt_Declaredby.ReadOnly = true;
            this.txt_Declaredby.Size = new System.Drawing.Size(191, 20);
            this.txt_Declaredby.TabIndex = 7;
            // 
            // lbl_Declaredby
            // 
            this.lbl_Declaredby.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lbl_Declaredby.AutoSize = true;
            this.lbl_Declaredby.Location = new System.Drawing.Point(221, 51);
            this.lbl_Declaredby.Name = "lbl_Declaredby";
            this.lbl_Declaredby.Size = new System.Drawing.Size(68, 13);
            this.lbl_Declaredby.TabIndex = 8;
            this.lbl_Declaredby.Text = "Decalred By:";
            // 
            // grpInstallationDetails
            // 
            this.grpInstallationDetails.Controls.Add(this.tblInstallationDetails);
            this.grpInstallationDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpInstallationDetails.Location = new System.Drawing.Point(3, 3);
            this.grpInstallationDetails.Name = "grpInstallationDetails";
            this.grpInstallationDetails.Size = new System.Drawing.Size(446, 92);
            this.grpInstallationDetails.TabIndex = 1;
            this.grpInstallationDetails.TabStop = false;
            this.grpInstallationDetails.Text = "Installation Details";
            // 
            // tblInstallationDetails
            // 
            this.tblInstallationDetails.ColumnCount = 5;
            this.tblInstallationDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 51F));
            this.tblInstallationDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 156F));
            this.tblInstallationDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 53F));
            this.tblInstallationDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 148F));
            this.tblInstallationDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tblInstallationDetails.Controls.Add(this.txt_Pos, 3, 0);
            this.tblInstallationDetails.Controls.Add(this.lblPos, 2, 0);
            this.tblInstallationDetails.Controls.Add(this.lblZone, 0, 0);
            this.tblInstallationDetails.Controls.Add(this.txtZone, 1, 0);
            this.tblInstallationDetails.Controls.Add(this.lblGame, 0, 1);
            this.tblInstallationDetails.Controls.Add(this.txt_Game, 1, 1);
            this.tblInstallationDetails.Controls.Add(this.lbl_Asset, 2, 1);
            this.tblInstallationDetails.Controls.Add(this.txt_Asset, 3, 1);
            this.tblInstallationDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblInstallationDetails.Location = new System.Drawing.Point(3, 16);
            this.tblInstallationDetails.Margin = new System.Windows.Forms.Padding(0);
            this.tblInstallationDetails.Name = "tblInstallationDetails";
            this.tblInstallationDetails.RowCount = 2;
            this.tblInstallationDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblInstallationDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 38F));
            this.tblInstallationDetails.Size = new System.Drawing.Size(440, 73);
            this.tblInstallationDetails.TabIndex = 0;
            // 
            // txt_Pos
            // 
            this.txt_Pos.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_Pos.BackColor = System.Drawing.Color.White;
            this.txt_Pos.Location = new System.Drawing.Point(263, 7);
            this.txt_Pos.Name = "txt_Pos";
            this.txt_Pos.ReadOnly = true;
            this.txt_Pos.Size = new System.Drawing.Size(142, 20);
            this.txt_Pos.TabIndex = 3;
            // 
            // lblPos
            // 
            this.lblPos.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblPos.AutoSize = true;
            this.lblPos.Location = new System.Drawing.Point(210, 11);
            this.lblPos.Name = "lblPos";
            this.lblPos.Size = new System.Drawing.Size(31, 13);
            this.lblPos.TabIndex = 2;
            this.lblPos.Text = "Pos :";
            // 
            // lblZone
            // 
            this.lblZone.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblZone.AutoSize = true;
            this.lblZone.Location = new System.Drawing.Point(3, 11);
            this.lblZone.Name = "lblZone";
            this.lblZone.Size = new System.Drawing.Size(38, 13);
            this.lblZone.TabIndex = 0;
            this.lblZone.Text = "Zone :";
            // 
            // txtZone
            // 
            this.txtZone.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtZone.BackColor = System.Drawing.Color.White;
            this.txtZone.Location = new System.Drawing.Point(54, 7);
            this.txtZone.Name = "txtZone";
            this.txtZone.ReadOnly = true;
            this.txtZone.Size = new System.Drawing.Size(150, 20);
            this.txtZone.TabIndex = 1;
            // 
            // lblGame
            // 
            this.lblGame.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblGame.AutoSize = true;
            this.lblGame.Location = new System.Drawing.Point(3, 47);
            this.lblGame.Name = "lblGame";
            this.lblGame.Size = new System.Drawing.Size(41, 13);
            this.lblGame.TabIndex = 4;
            this.lblGame.Text = "Game :";
            // 
            // txt_Game
            // 
            this.txt_Game.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_Game.BackColor = System.Drawing.Color.White;
            this.txt_Game.Location = new System.Drawing.Point(54, 44);
            this.txt_Game.Name = "txt_Game";
            this.txt_Game.ReadOnly = true;
            this.txt_Game.Size = new System.Drawing.Size(150, 20);
            this.txt_Game.TabIndex = 5;
            // 
            // lbl_Asset
            // 
            this.lbl_Asset.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lbl_Asset.AutoSize = true;
            this.lbl_Asset.Location = new System.Drawing.Point(210, 47);
            this.lbl_Asset.Name = "lbl_Asset";
            this.lbl_Asset.Size = new System.Drawing.Size(39, 13);
            this.lbl_Asset.TabIndex = 8;
            this.lbl_Asset.Text = "Asset :";
            // 
            // txt_Asset
            // 
            this.txt_Asset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_Asset.BackColor = System.Drawing.Color.White;
            this.txt_Asset.Location = new System.Drawing.Point(263, 44);
            this.txt_Asset.Name = "txt_Asset";
            this.txt_Asset.ReadOnly = true;
            this.txt_Asset.Size = new System.Drawing.Size(142, 20);
            this.txt_Asset.TabIndex = 9;
            // 
            // tblPosEvents
            // 
            this.tblPosEvents.ColumnCount = 2;
            this.tblPosEvents.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblPosEvents.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tblPosEvents.Controls.Add(this.btnReturn, 1, 0);
            this.tblPosEvents.Controls.Add(this.grpPositionEvents, 0, 0);
            this.tblPosEvents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblPosEvents.Location = new System.Drawing.Point(3, 485);
            this.tblPosEvents.Name = "tblPosEvents";
            this.tblPosEvents.RowCount = 1;
            this.tblPosEvents.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblPosEvents.Size = new System.Drawing.Size(1000, 105);
            this.tblPosEvents.TabIndex = 3;
            // 
            // btnReturn
            // 
            this.btnReturn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReturn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnReturn.ImageKey = "MovePrev";
            this.btnReturn.Location = new System.Drawing.Point(883, 78);
            this.btnReturn.Name = "btnReturn";
            this.btnReturn.Size = new System.Drawing.Size(114, 24);
            this.btnReturn.TabIndex = 1;
            this.btnReturn.Text = "Close";
            this.btnReturn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnReturn.UseVisualStyleBackColor = true;
            this.btnReturn.Click += new System.EventHandler(this.btnReturn_Click);
            // 
            // grpPositionEvents
            // 
            this.grpPositionEvents.Controls.Add(this.lvwPositionEvents);
            this.grpPositionEvents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpPositionEvents.Location = new System.Drawing.Point(3, 3);
            this.grpPositionEvents.Name = "grpPositionEvents";
            this.grpPositionEvents.Padding = new System.Windows.Forms.Padding(5);
            this.grpPositionEvents.Size = new System.Drawing.Size(874, 99);
            this.grpPositionEvents.TabIndex = 0;
            this.grpPositionEvents.TabStop = false;
            this.grpPositionEvents.Text = "Position Events";
            // 
            // lvwPositionEvents
            // 
            this.lvwPositionEvents.ClipboardCopyFormat = BMC.CoreLib.Win32.ListViewClipboardCopyFormat.Default;
            this.lvwPositionEvents.ClipboardCopyMode = BMC.CoreLib.Win32.ListViewClipboardCopyMode.Disable;
            this.lvwPositionEvents.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clm_Type,
            this.clm_Date,
            this.clm_Time,
            this.clm_Duration,
            this.clm_Description});
            this.lvwPositionEvents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvwPositionEvents.FullRowSelect = true;
            this.lvwPositionEvents.GridLines = true;
            this.lvwPositionEvents.HideSelection = false;
            this.lvwPositionEvents.Location = new System.Drawing.Point(5, 18);
            this.lvwPositionEvents.Name = "lvwPositionEvents";
            this.lvwPositionEvents.Size = new System.Drawing.Size(864, 76);
            this.lvwPositionEvents.SmallImageList = this.imglstSmallIcons;
            this.lvwPositionEvents.TabIndex = 0;
            this.lvwPositionEvents.UseCompatibleStateImageBehavior = false;
            this.lvwPositionEvents.View = System.Windows.Forms.View.Details;
            this.lvwPositionEvents.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvwPositionEvents_ColumnClick);
            // 
            // clm_Type
            // 
            this.clm_Type.Text = "Type";
            this.clm_Type.Width = 100;
            // 
            // clm_Date
            // 
            this.clm_Date.Text = "Date";
            this.clm_Date.Width = 100;
            // 
            // clm_Time
            // 
            this.clm_Time.Text = "Time";
            this.clm_Time.Width = 100;
            // 
            // clm_Duration
            // 
            this.clm_Duration.Text = "Duration";
            this.clm_Duration.Width = 100;
            // 
            // clm_Description
            // 
            this.clm_Description.Text = "Description";
            this.clm_Description.Width = 100;
            // 
            // CollHistoryBreakDownForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnReturn;
            this.ClientSize = new System.Drawing.Size(1018, 618);
            this.Controls.Add(this.grpTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CollHistoryBreakDownForm";
            this.Padding = new System.Windows.Forms.Padding(3);
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CollHistoryBreakDownForm";
            this.Load += new System.EventHandler(this.CollHistoryBreakDownForm_Load);
            this.grpTitle.ResumeLayout(false);
            this.tblContainer.ResumeLayout(false);
            this.tabDetails.ResumeLayout(false);
            this.tbpDropMeterBreakdown.ResumeLayout(false);
            this.tblDropMeterBreakdown.ResumeLayout(false);
            this.tbpVarianceHistory.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tblHeader.ResumeLayout(false);
            this.tblHeader.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.grpCashDeskTrans.ResumeLayout(false);
            this.grpDropSummaries.ResumeLayout(false);
            this.tblDetails.ResumeLayout(false);
            this.grpDropDetails.ResumeLayout(false);
            this.tblDropDetails.ResumeLayout(false);
            this.tblDropDetails.PerformLayout();
            this.grpInstallationDetails.ResumeLayout(false);
            this.tblInstallationDetails.ResumeLayout(false);
            this.tblInstallationDetails.PerformLayout();
            this.tblPosEvents.ResumeLayout(false);
            this.grpPositionEvents.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpTitle;
        private System.Windows.Forms.TableLayoutPanel tblContainer;
        private System.Windows.Forms.TableLayoutPanel tblDetails;
        private System.Windows.Forms.GroupBox grpDropDetails;
        private System.Windows.Forms.TableLayoutPanel tblDropDetails;
        private System.Windows.Forms.TextBox txtUser;
        private System.Windows.Forms.TextBox txtCollectionDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblGamingDay;
        private System.Windows.Forms.TextBox txtGamingDay;
        private System.Windows.Forms.Label lblUser;
        private System.Windows.Forms.GroupBox grpInstallationDetails;
        private System.Windows.Forms.TableLayoutPanel tblInstallationDetails;
        private System.Windows.Forms.TextBox txt_Game;
        private System.Windows.Forms.TextBox txt_Pos;
        private System.Windows.Forms.Label lblPos;
        private System.Windows.Forms.Label lblZone;
        private System.Windows.Forms.TextBox txtZone;
        private System.Windows.Forms.Label lblGame;
        private System.Windows.Forms.ImageList imglstSmallIcons;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox grpCashDeskTrans;
        private System.Windows.Forms.GroupBox grpDropSummaries;
        private BMC.CoreLib.Win32.ListViewEx lvwDropSummaries;
        private BMC.CoreLib.Win32.ListViewEx lvwCashDeskTrans;
        private System.Windows.Forms.TabControl tabDetails;
        private System.Windows.Forms.TabPage tbpDropMeterBreakdown;
        private System.Windows.Forms.TabPage tbpVarianceHistory;
        private System.Windows.Forms.TableLayoutPanel tblDropMeterBreakdown;
        private BMC.CoreLib.Win32.ListViewEx lvDropBreakDown;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private BMC.CoreLib.Win32.ListViewEx lvwVarianceHistory;
        private System.Windows.Forms.TableLayoutPanel tblHeader;
        private System.Windows.Forms.ComboBox cmbPeriodCount;
        private System.Windows.Forms.Label lblVarLast;
        private System.Windows.Forms.Label lblVarRecords;
        private System.Windows.Forms.TableLayoutPanel tblPosEvents;
        private System.Windows.Forms.GroupBox grpPositionEvents;
        private BMC.CoreLib.Win32.ListViewEx lvwPositionEvents;
        private System.Windows.Forms.ColumnHeader clm_Type;
        private System.Windows.Forms.ColumnHeader clm_Date;
        private System.Windows.Forms.ColumnHeader clm_Time;
        private System.Windows.Forms.ColumnHeader clm_Duration;
        private System.Windows.Forms.ColumnHeader clm_Description;
        private System.Windows.Forms.Button btnReturn;
		private System.Windows.Forms.ColumnHeader clmCollectionDate;
        private System.Windows.Forms.ColumnHeader clmCoinVar;
        private System.Windows.Forms.ColumnHeader clmBillsVar;
        private System.Windows.Forms.ColumnHeader clmVoucherInVar;
        private System.Windows.Forms.ColumnHeader clmVoucherOutVar;
        private System.Windows.Forms.ColumnHeader clmEFTInVar;
        private System.Windows.Forms.ColumnHeader clmEFTOutVar;
        private System.Windows.Forms.ColumnHeader clmAttendantpayVar;
        private System.Windows.Forms.ColumnHeader clmAttendantpayProgressiveVar;
        private System.Windows.Forms.ColumnHeader clmDate;
        private System.Windows.Forms.ColumnHeader clmTime;
        private System.Windows.Forms.ColumnHeader clmType;
        private System.Windows.Forms.ColumnHeader clmEnteredBy;
        private System.Windows.Forms.ColumnHeader clmAmount;
        private System.Windows.Forms.ColumnHeader clmIssuedBy;
        private System.Windows.Forms.ColumnHeader clmComment;
        private System.Windows.Forms.Button btn_ModifyDeclaration;
        private System.Windows.Forms.ColumnHeader ClmAsset;
        private System.Windows.Forms.TextBox txt_Declaredby;
        private System.Windows.Forms.ColumnHeader ClmDeclaredBy;
        private System.Windows.Forms.Label lbl_Asset;
        private System.Windows.Forms.TextBox txt_Asset;
        private System.Windows.Forms.Label lbl_Declaredby;
    }
}