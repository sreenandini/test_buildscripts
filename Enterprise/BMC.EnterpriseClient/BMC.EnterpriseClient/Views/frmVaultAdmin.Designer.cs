namespace BMC.EnterpriseClient.Views
{
    partial class frmVaultAdmin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmVaultAdmin));
            this.tlpMainPanel = new System.Windows.Forms.TableLayoutPanel();
            this.tcVaultAdmin = new System.Windows.Forms.TabControl();
            this.tpVault = new System.Windows.Forms.TabPage();
            this.tlpVault = new System.Windows.Forms.TableLayoutPanel();
            this.btn_CloseAddVault = new System.Windows.Forms.Button();
            this.tpVaultFinance = new System.Windows.Forms.TabPage();
            this.tlpVaultFinance = new System.Windows.Forms.TableLayoutPanel();
            this.tlpFinanceInnerPanel = new System.Windows.Forms.TableLayoutPanel();
            this.dtpSoldDate = new System.Windows.Forms.DateTimePicker();
            this.dtpDepreciationStartDate = new System.Windows.Forms.DateTimePicker();
            this.txtSoldInvoiceNumber = new System.Windows.Forms.TextBox();
            this.txtInvoiceNumber = new System.Windows.Forms.TextBox();
            this.lblPurchasePrice = new System.Windows.Forms.Label();
            this.lblSoldDate = new System.Windows.Forms.Label();
            this.lblSoldInvoiceNumber = new System.Windows.Forms.Label();
            this.lblSoldPrice = new System.Windows.Forms.Label();
            this.lblDepreciationStartDate = new System.Windows.Forms.Label();
            this.lblPurchaseData = new System.Windows.Forms.Label();
            this.lblPurchaseInvoiceNumber = new System.Windows.Forms.Label();
            this.dtpPurchaseDate = new System.Windows.Forms.DateTimePicker();
            this.lbl_SellVault = new System.Windows.Forms.Label();
            this.cb_SellVault = new System.Windows.Forms.CheckBox();
            this.lbl_VaultName = new System.Windows.Forms.Label();
            this.lbl_SerialNo = new System.Windows.Forms.Label();
            this.lbl_Prefix = new System.Windows.Forms.Label();
            this.lbl_PrefixText = new System.Windows.Forms.Label();
            this.lbl_SerialNoTxt = new System.Windows.Forms.Label();
            this.lbl_VaultText = new System.Windows.Forms.Label();
            this.tlpButtons = new System.Windows.Forms.TableLayoutPanel();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.tlp_FinanceHeader = new System.Windows.Forms.TableLayoutPanel();
            this.lbl_FinanceHeader = new System.Windows.Forms.Label();
            this.tpAssignVaultToSite = new System.Windows.Forms.TabPage();
            this.tlpAssignVaultMainPanel = new System.Windows.Forms.TableLayoutPanel();
            this.tlpAssignVaultInnerPanel = new System.Windows.Forms.TableLayoutPanel();
            this.tlpVaults = new System.Windows.Forms.TableLayoutPanel();
            this.txtVaultFilter = new System.Windows.Forms.TextBox();
            this.lbVaults = new System.Windows.Forms.ListBox();
            this.lblVaults = new System.Windows.Forms.Label();
            this.lblSites = new System.Windows.Forms.Label();
            this.lblAssignVault = new System.Windows.Forms.Label();
            this.tlpSites = new System.Windows.Forms.TableLayoutPanel();
            this.txtSitesFilter = new System.Windows.Forms.TextBox();
            this.lbSites = new System.Windows.Forms.ListBox();
            this.lvAssignSite = new System.Windows.Forms.ListView();
            this.clmVaults = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmSites = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tlpAssigningButtons = new System.Windows.Forms.TableLayoutPanel();
            this.btnAssign = new System.Windows.Forms.Button();
            this.btnUnassign = new System.Windows.Forms.Button();
            this.tlpAssignVaultButtons = new System.Windows.Forms.TableLayoutPanel();
            this.btnAssignVaultSave = new System.Windows.Forms.Button();
            this.btnAssignVaultClose = new System.Windows.Forms.Button();
            this.tlp_AssignToSite = new System.Windows.Forms.TableLayoutPanel();
            this.lbl_AssignToSite = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.ucfrmAddVault = new BMC.EnterpriseClient.Views.frmAddVault();
            this.txtSoldPrice = new BMC.EnterpriseClient.Views.NumberTextBox();
            this.txtPurchasePrice = new BMC.EnterpriseClient.Views.NumberTextBox();
            this.tlpMainPanel.SuspendLayout();
            this.tcVaultAdmin.SuspendLayout();
            this.tpVault.SuspendLayout();
            this.tlpVault.SuspendLayout();
            this.tpVaultFinance.SuspendLayout();
            this.tlpVaultFinance.SuspendLayout();
            this.tlpFinanceInnerPanel.SuspendLayout();
            this.tlpButtons.SuspendLayout();
            this.tlp_FinanceHeader.SuspendLayout();
            this.tpAssignVaultToSite.SuspendLayout();
            this.tlpAssignVaultMainPanel.SuspendLayout();
            this.tlpAssignVaultInnerPanel.SuspendLayout();
            this.tlpVaults.SuspendLayout();
            this.tlpSites.SuspendLayout();
            this.tlpAssigningButtons.SuspendLayout();
            this.tlpAssignVaultButtons.SuspendLayout();
            this.tlp_AssignToSite.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpMainPanel
            // 
            this.tlpMainPanel.ColumnCount = 1;
            this.tlpMainPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMainPanel.Controls.Add(this.tcVaultAdmin, 0, 1);
            this.tlpMainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMainPanel.Location = new System.Drawing.Point(0, 0);
            this.tlpMainPanel.Name = "tlpMainPanel";
            this.tlpMainPanel.RowCount = 3;
            this.tlpMainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tlpMainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tlpMainPanel.Size = new System.Drawing.Size(820, 632);
            this.tlpMainPanel.TabIndex = 0;
            // 
            // tcVaultAdmin
            // 
            this.tcVaultAdmin.Controls.Add(this.tpVault);
            this.tcVaultAdmin.Controls.Add(this.tpVaultFinance);
            this.tcVaultAdmin.Controls.Add(this.tpAssignVaultToSite);
            this.tcVaultAdmin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcVaultAdmin.Location = new System.Drawing.Point(3, 28);
            this.tcVaultAdmin.Name = "tcVaultAdmin";
            this.tcVaultAdmin.SelectedIndex = 0;
            this.tcVaultAdmin.Size = new System.Drawing.Size(814, 576);
            this.tcVaultAdmin.TabIndex = 0;
            this.tcVaultAdmin.SelectedIndexChanged += new System.EventHandler(this.tcVaultAdmin_SelectedIndexChanged);
            // 
            // tpVault
            // 
            this.tpVault.AutoScroll = true;
            this.tpVault.Controls.Add(this.tlpVault);
            this.tpVault.Location = new System.Drawing.Point(4, 22);
            this.tpVault.Name = "tpVault";
            this.tpVault.Padding = new System.Windows.Forms.Padding(3);
            this.tpVault.Size = new System.Drawing.Size(806, 550);
            this.tpVault.TabIndex = 0;
            this.tpVault.Tag = "Vault";
            this.tpVault.Text = "Vault";
            this.tpVault.UseVisualStyleBackColor = true;
            // 
            // tlpVault
            // 
            this.tlpVault.AutoScroll = true;
            this.tlpVault.ColumnCount = 1;
            this.tlpVault.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpVault.Controls.Add(this.ucfrmAddVault, 0, 0);
            this.tlpVault.Controls.Add(this.btn_CloseAddVault, 0, 1);
            this.tlpVault.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpVault.Location = new System.Drawing.Point(3, 3);
            this.tlpVault.Name = "tlpVault";
            this.tlpVault.RowCount = 2;
            this.tlpVault.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpVault.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlpVault.Size = new System.Drawing.Size(800, 544);
            this.tlpVault.TabIndex = 0;
            // 
            // btn_CloseAddVault
            // 
            this.btn_CloseAddVault.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_CloseAddVault.Location = new System.Drawing.Point(722, 517);
            this.btn_CloseAddVault.Name = "btn_CloseAddVault";
            this.btn_CloseAddVault.Size = new System.Drawing.Size(75, 24);
            this.btn_CloseAddVault.TabIndex = 1;
            this.btn_CloseAddVault.Text = "&Close";
            this.btn_CloseAddVault.UseVisualStyleBackColor = true;
            this.btn_CloseAddVault.Click += new System.EventHandler(this.btn_CloseAddVault_Click);
            // 
            // tpVaultFinance
            // 
            this.tpVaultFinance.Controls.Add(this.tlpVaultFinance);
            this.tpVaultFinance.Location = new System.Drawing.Point(4, 22);
            this.tpVaultFinance.Name = "tpVaultFinance";
            this.tpVaultFinance.Padding = new System.Windows.Forms.Padding(3);
            this.tpVaultFinance.Size = new System.Drawing.Size(806, 550);
            this.tpVaultFinance.TabIndex = 1;
            this.tpVaultFinance.Tag = "Vault Finance";
            this.tpVaultFinance.Text = "Vault Finance";
            this.tpVaultFinance.UseVisualStyleBackColor = true;
            // 
            // tlpVaultFinance
            // 
            this.tlpVaultFinance.ColumnCount = 1;
            this.tlpVaultFinance.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpVaultFinance.Controls.Add(this.tlpFinanceInnerPanel, 0, 1);
            this.tlpVaultFinance.Controls.Add(this.tlpButtons, 0, 2);
            this.tlpVaultFinance.Controls.Add(this.tlp_FinanceHeader, 0, 0);
            this.tlpVaultFinance.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpVaultFinance.Location = new System.Drawing.Point(3, 3);
            this.tlpVaultFinance.Name = "tlpVaultFinance";
            this.tlpVaultFinance.RowCount = 3;
            this.tlpVaultFinance.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tlpVaultFinance.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpVaultFinance.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tlpVaultFinance.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpVaultFinance.Size = new System.Drawing.Size(800, 544);
            this.tlpVaultFinance.TabIndex = 0;
            // 
            // tlpFinanceInnerPanel
            // 
            this.tlpFinanceInnerPanel.ColumnCount = 2;
            this.tlpFinanceInnerPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tlpFinanceInnerPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpFinanceInnerPanel.Controls.Add(this.dtpSoldDate, 1, 10);
            this.tlpFinanceInnerPanel.Controls.Add(this.dtpDepreciationStartDate, 1, 6);
            this.tlpFinanceInnerPanel.Controls.Add(this.txtSoldInvoiceNumber, 1, 9);
            this.tlpFinanceInnerPanel.Controls.Add(this.txtSoldPrice, 1, 8);
            this.tlpFinanceInnerPanel.Controls.Add(this.txtInvoiceNumber, 1, 4);
            this.tlpFinanceInnerPanel.Controls.Add(this.lblPurchasePrice, 0, 3);
            this.tlpFinanceInnerPanel.Controls.Add(this.lblSoldDate, 0, 10);
            this.tlpFinanceInnerPanel.Controls.Add(this.lblSoldInvoiceNumber, 0, 9);
            this.tlpFinanceInnerPanel.Controls.Add(this.lblSoldPrice, 0, 8);
            this.tlpFinanceInnerPanel.Controls.Add(this.lblDepreciationStartDate, 0, 6);
            this.tlpFinanceInnerPanel.Controls.Add(this.lblPurchaseData, 0, 5);
            this.tlpFinanceInnerPanel.Controls.Add(this.lblPurchaseInvoiceNumber, 0, 4);
            this.tlpFinanceInnerPanel.Controls.Add(this.txtPurchasePrice, 1, 3);
            this.tlpFinanceInnerPanel.Controls.Add(this.dtpPurchaseDate, 1, 5);
            this.tlpFinanceInnerPanel.Controls.Add(this.lbl_SellVault, 0, 7);
            this.tlpFinanceInnerPanel.Controls.Add(this.cb_SellVault, 1, 7);
            this.tlpFinanceInnerPanel.Controls.Add(this.lbl_VaultName, 0, 0);
            this.tlpFinanceInnerPanel.Controls.Add(this.lbl_SerialNo, 0, 1);
            this.tlpFinanceInnerPanel.Controls.Add(this.lbl_Prefix, 0, 2);
            this.tlpFinanceInnerPanel.Controls.Add(this.lbl_PrefixText, 1, 2);
            this.tlpFinanceInnerPanel.Controls.Add(this.lbl_SerialNoTxt, 1, 1);
            this.tlpFinanceInnerPanel.Controls.Add(this.lbl_VaultText, 1, 0);
            this.tlpFinanceInnerPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpFinanceInnerPanel.Location = new System.Drawing.Point(3, 38);
            this.tlpFinanceInnerPanel.Name = "tlpFinanceInnerPanel";
            this.tlpFinanceInnerPanel.RowCount = 11;
            this.tlpFinanceInnerPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.090908F));
            this.tlpFinanceInnerPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.090908F));
            this.tlpFinanceInnerPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.090908F));
            this.tlpFinanceInnerPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.090908F));
            this.tlpFinanceInnerPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.090908F));
            this.tlpFinanceInnerPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.090908F));
            this.tlpFinanceInnerPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.090908F));
            this.tlpFinanceInnerPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.090908F));
            this.tlpFinanceInnerPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.090908F));
            this.tlpFinanceInnerPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.090908F));
            this.tlpFinanceInnerPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.090908F));
            this.tlpFinanceInnerPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpFinanceInnerPanel.Size = new System.Drawing.Size(794, 468);
            this.tlpFinanceInnerPanel.TabIndex = 1;
            // 
            // dtpSoldDate
            // 
            this.dtpSoldDate.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.dtpSoldDate.CustomFormat = "dd-MMM-yyyy hh:mm:ss tt";
            this.dtpSoldDate.Enabled = false;
            this.dtpSoldDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpSoldDate.Location = new System.Drawing.Point(203, 434);
            this.dtpSoldDate.Name = "dtpSoldDate";
            this.dtpSoldDate.Size = new System.Drawing.Size(200, 20);
            this.dtpSoldDate.TabIndex = 15;
            // 
            // dtpDepreciationStartDate
            // 
            this.dtpDepreciationStartDate.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.dtpDepreciationStartDate.CustomFormat = "dd-MMM-yyyy hh:mm:ss tt";
            this.dtpDepreciationStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDepreciationStartDate.Location = new System.Drawing.Point(203, 263);
            this.dtpDepreciationStartDate.Name = "dtpDepreciationStartDate";
            this.dtpDepreciationStartDate.Size = new System.Drawing.Size(200, 20);
            this.dtpDepreciationStartDate.TabIndex = 7;
            // 
            // txtSoldInvoiceNumber
            // 
            this.txtSoldInvoiceNumber.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtSoldInvoiceNumber.Enabled = false;
            this.txtSoldInvoiceNumber.Location = new System.Drawing.Point(203, 389);
            this.txtSoldInvoiceNumber.MaxLength = 50;
            this.txtSoldInvoiceNumber.Name = "txtSoldInvoiceNumber";
            this.txtSoldInvoiceNumber.Size = new System.Drawing.Size(206, 20);
            this.txtSoldInvoiceNumber.TabIndex = 13;
            // 
            // txtInvoiceNumber
            // 
            this.txtInvoiceNumber.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtInvoiceNumber.Location = new System.Drawing.Point(203, 179);
            this.txtInvoiceNumber.MaxLength = 50;
            this.txtInvoiceNumber.Name = "txtInvoiceNumber";
            this.txtInvoiceNumber.Size = new System.Drawing.Size(206, 20);
            this.txtInvoiceNumber.TabIndex = 3;
            // 
            // lblPurchasePrice
            // 
            this.lblPurchasePrice.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lblPurchasePrice.AutoSize = true;
            this.lblPurchasePrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPurchasePrice.Location = new System.Drawing.Point(3, 126);
            this.lblPurchasePrice.Name = "lblPurchasePrice";
            this.lblPurchasePrice.Size = new System.Drawing.Size(104, 42);
            this.lblPurchasePrice.TabIndex = 0;
            this.lblPurchasePrice.Text = "* Purchase Price :";
            this.lblPurchasePrice.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblSoldDate
            // 
            this.lblSoldDate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lblSoldDate.AutoSize = true;
            this.lblSoldDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSoldDate.Location = new System.Drawing.Point(3, 420);
            this.lblSoldDate.Name = "lblSoldDate";
            this.lblSoldDate.Size = new System.Drawing.Size(67, 48);
            this.lblSoldDate.TabIndex = 14;
            this.lblSoldDate.Text = "Sold Date :";
            this.lblSoldDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblSoldInvoiceNumber
            // 
            this.lblSoldInvoiceNumber.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lblSoldInvoiceNumber.AutoSize = true;
            this.lblSoldInvoiceNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSoldInvoiceNumber.Location = new System.Drawing.Point(3, 378);
            this.lblSoldInvoiceNumber.Name = "lblSoldInvoiceNumber";
            this.lblSoldInvoiceNumber.Size = new System.Drawing.Size(127, 42);
            this.lblSoldInvoiceNumber.TabIndex = 12;
            this.lblSoldInvoiceNumber.Text = "Sold Invoice Number :";
            this.lblSoldInvoiceNumber.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblSoldPrice
            // 
            this.lblSoldPrice.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lblSoldPrice.AutoSize = true;
            this.lblSoldPrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSoldPrice.Location = new System.Drawing.Point(3, 336);
            this.lblSoldPrice.Name = "lblSoldPrice";
            this.lblSoldPrice.Size = new System.Drawing.Size(69, 42);
            this.lblSoldPrice.TabIndex = 10;
            this.lblSoldPrice.Text = "Sold Price :";
            this.lblSoldPrice.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblDepreciationStartDate
            // 
            this.lblDepreciationStartDate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lblDepreciationStartDate.AutoSize = true;
            this.lblDepreciationStartDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDepreciationStartDate.Location = new System.Drawing.Point(3, 252);
            this.lblDepreciationStartDate.Name = "lblDepreciationStartDate";
            this.lblDepreciationStartDate.Size = new System.Drawing.Size(140, 42);
            this.lblDepreciationStartDate.TabIndex = 6;
            this.lblDepreciationStartDate.Text = "Depreciation Start Date :";
            this.lblDepreciationStartDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblPurchaseData
            // 
            this.lblPurchaseData.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lblPurchaseData.AutoSize = true;
            this.lblPurchaseData.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPurchaseData.Location = new System.Drawing.Point(3, 210);
            this.lblPurchaseData.Name = "lblPurchaseData";
            this.lblPurchaseData.Size = new System.Drawing.Size(94, 42);
            this.lblPurchaseData.TabIndex = 4;
            this.lblPurchaseData.Text = "Purchase Date :";
            this.lblPurchaseData.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblPurchaseInvoiceNumber
            // 
            this.lblPurchaseInvoiceNumber.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lblPurchaseInvoiceNumber.AutoSize = true;
            this.lblPurchaseInvoiceNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPurchaseInvoiceNumber.Location = new System.Drawing.Point(3, 168);
            this.lblPurchaseInvoiceNumber.Name = "lblPurchaseInvoiceNumber";
            this.lblPurchaseInvoiceNumber.Size = new System.Drawing.Size(162, 42);
            this.lblPurchaseInvoiceNumber.TabIndex = 2;
            this.lblPurchaseInvoiceNumber.Text = "* Purchase Invoice Number :";
            this.lblPurchaseInvoiceNumber.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dtpPurchaseDate
            // 
            this.dtpPurchaseDate.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.dtpPurchaseDate.CustomFormat = "dd-MMM-yyyy hh:mm:ss tt";
            this.dtpPurchaseDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpPurchaseDate.Location = new System.Drawing.Point(203, 221);
            this.dtpPurchaseDate.Name = "dtpPurchaseDate";
            this.dtpPurchaseDate.Size = new System.Drawing.Size(200, 20);
            this.dtpPurchaseDate.TabIndex = 5;
            // 
            // lbl_SellVault
            // 
            this.lbl_SellVault.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_SellVault.AutoSize = true;
            this.lbl_SellVault.Location = new System.Drawing.Point(3, 308);
            this.lbl_SellVault.Name = "lbl_SellVault";
            this.lbl_SellVault.Size = new System.Drawing.Size(194, 13);
            this.lbl_SellVault.TabIndex = 8;
            this.lbl_SellVault.Text = "Sell Vault :";
            // 
            // cb_SellVault
            // 
            this.cb_SellVault.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cb_SellVault.AutoSize = true;
            this.cb_SellVault.Location = new System.Drawing.Point(203, 308);
            this.cb_SellVault.Name = "cb_SellVault";
            this.cb_SellVault.Size = new System.Drawing.Size(15, 14);
            this.cb_SellVault.TabIndex = 9;
            this.cb_SellVault.UseVisualStyleBackColor = true;
            this.cb_SellVault.CheckedChanged += new System.EventHandler(this.cb_SellVault_CheckedChanged);
            // 
            // lbl_VaultName
            // 
            this.lbl_VaultName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lbl_VaultName.AutoSize = true;
            this.lbl_VaultName.Location = new System.Drawing.Point(3, 0);
            this.lbl_VaultName.Name = "lbl_VaultName";
            this.lbl_VaultName.Size = new System.Drawing.Size(68, 42);
            this.lbl_VaultName.TabIndex = 16;
            this.lbl_VaultName.Text = "Vault Name :";
            this.lbl_VaultName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_SerialNo
            // 
            this.lbl_SerialNo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lbl_SerialNo.AutoSize = true;
            this.lbl_SerialNo.Location = new System.Drawing.Point(3, 42);
            this.lbl_SerialNo.Name = "lbl_SerialNo";
            this.lbl_SerialNo.Size = new System.Drawing.Size(79, 42);
            this.lbl_SerialNo.TabIndex = 17;
            this.lbl_SerialNo.Text = "Serial Number :";
            this.lbl_SerialNo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_Prefix
            // 
            this.lbl_Prefix.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lbl_Prefix.AutoSize = true;
            this.lbl_Prefix.Location = new System.Drawing.Point(3, 84);
            this.lbl_Prefix.Name = "lbl_Prefix";
            this.lbl_Prefix.Size = new System.Drawing.Size(39, 42);
            this.lbl_Prefix.TabIndex = 18;
            this.lbl_Prefix.Text = "Prefix :";
            this.lbl_Prefix.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_PrefixText
            // 
            this.lbl_PrefixText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lbl_PrefixText.AutoSize = true;
            this.lbl_PrefixText.Location = new System.Drawing.Point(203, 84);
            this.lbl_PrefixText.Name = "lbl_PrefixText";
            this.lbl_PrefixText.Size = new System.Drawing.Size(19, 42);
            this.lbl_PrefixText.TabIndex = 21;
            this.lbl_PrefixText.Text = "***";
            this.lbl_PrefixText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_SerialNoTxt
            // 
            this.lbl_SerialNoTxt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lbl_SerialNoTxt.AutoSize = true;
            this.lbl_SerialNoTxt.Location = new System.Drawing.Point(203, 42);
            this.lbl_SerialNoTxt.Name = "lbl_SerialNoTxt";
            this.lbl_SerialNoTxt.Size = new System.Drawing.Size(19, 42);
            this.lbl_SerialNoTxt.TabIndex = 20;
            this.lbl_SerialNoTxt.Text = "***";
            this.lbl_SerialNoTxt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_VaultText
            // 
            this.lbl_VaultText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lbl_VaultText.AutoSize = true;
            this.lbl_VaultText.Location = new System.Drawing.Point(203, 0);
            this.lbl_VaultText.Name = "lbl_VaultText";
            this.lbl_VaultText.Size = new System.Drawing.Size(19, 42);
            this.lbl_VaultText.TabIndex = 19;
            this.lbl_VaultText.Text = "***";
            this.lbl_VaultText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tlpButtons
            // 
            this.tlpButtons.ColumnCount = 4;
            this.tlpButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tlpButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tlpButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tlpButtons.Controls.Add(this.btnClose, 3, 0);
            this.tlpButtons.Controls.Add(this.btnSave, 2, 0);
            this.tlpButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpButtons.Location = new System.Drawing.Point(3, 512);
            this.tlpButtons.Name = "tlpButtons";
            this.tlpButtons.RowCount = 1;
            this.tlpButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpButtons.Size = new System.Drawing.Size(794, 29);
            this.tlpButtons.TabIndex = 2;
            // 
            // btnClose
            // 
            this.btnClose.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnClose.Location = new System.Drawing.Point(697, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(94, 23);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSave
            // 
            this.btnSave.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSave.Location = new System.Drawing.Point(597, 3);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(94, 23);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "&Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // tlp_FinanceHeader
            // 
            this.tlp_FinanceHeader.BackColor = System.Drawing.Color.SteelBlue;
            this.tlp_FinanceHeader.ColumnCount = 1;
            this.tlp_FinanceHeader.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlp_FinanceHeader.Controls.Add(this.lbl_FinanceHeader, 0, 0);
            this.tlp_FinanceHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_FinanceHeader.Location = new System.Drawing.Point(3, 3);
            this.tlp_FinanceHeader.Name = "tlp_FinanceHeader";
            this.tlp_FinanceHeader.RowCount = 1;
            this.tlp_FinanceHeader.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlp_FinanceHeader.Size = new System.Drawing.Size(794, 29);
            this.tlp_FinanceHeader.TabIndex = 3;
            // 
            // lbl_FinanceHeader
            // 
            this.lbl_FinanceHeader.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_FinanceHeader.AutoSize = true;
            this.lbl_FinanceHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_FinanceHeader.ForeColor = System.Drawing.Color.White;
            this.lbl_FinanceHeader.Location = new System.Drawing.Point(3, 6);
            this.lbl_FinanceHeader.Name = "lbl_FinanceHeader";
            this.lbl_FinanceHeader.Size = new System.Drawing.Size(788, 17);
            this.lbl_FinanceHeader.TabIndex = 0;
            this.lbl_FinanceHeader.Text = "Finance";
            // 
            // tpAssignVaultToSite
            // 
            this.tpAssignVaultToSite.Controls.Add(this.tlpAssignVaultMainPanel);
            this.tpAssignVaultToSite.Location = new System.Drawing.Point(4, 22);
            this.tpAssignVaultToSite.Name = "tpAssignVaultToSite";
            this.tpAssignVaultToSite.Size = new System.Drawing.Size(806, 550);
            this.tpAssignVaultToSite.TabIndex = 2;
            this.tpAssignVaultToSite.Tag = "Assign Vault to Site";
            this.tpAssignVaultToSite.Text = "Assign Vault to Site";
            this.tpAssignVaultToSite.UseVisualStyleBackColor = true;
            // 
            // tlpAssignVaultMainPanel
            // 
            this.tlpAssignVaultMainPanel.ColumnCount = 1;
            this.tlpAssignVaultMainPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpAssignVaultMainPanel.Controls.Add(this.tlpAssignVaultInnerPanel, 0, 1);
            this.tlpAssignVaultMainPanel.Controls.Add(this.tlpAssignVaultButtons, 0, 2);
            this.tlpAssignVaultMainPanel.Controls.Add(this.tlp_AssignToSite, 0, 0);
            this.tlpAssignVaultMainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpAssignVaultMainPanel.Location = new System.Drawing.Point(0, 0);
            this.tlpAssignVaultMainPanel.Name = "tlpAssignVaultMainPanel";
            this.tlpAssignVaultMainPanel.RowCount = 3;
            this.tlpAssignVaultMainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tlpAssignVaultMainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpAssignVaultMainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tlpAssignVaultMainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpAssignVaultMainPanel.Size = new System.Drawing.Size(806, 550);
            this.tlpAssignVaultMainPanel.TabIndex = 0;
            // 
            // tlpAssignVaultInnerPanel
            // 
            this.tlpAssignVaultInnerPanel.ColumnCount = 4;
            this.tlpAssignVaultInnerPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tlpAssignVaultInnerPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tlpAssignVaultInnerPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tlpAssignVaultInnerPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tlpAssignVaultInnerPanel.Controls.Add(this.tlpVaults, 0, 1);
            this.tlpAssignVaultInnerPanel.Controls.Add(this.lblVaults, 0, 0);
            this.tlpAssignVaultInnerPanel.Controls.Add(this.lblSites, 1, 0);
            this.tlpAssignVaultInnerPanel.Controls.Add(this.lblAssignVault, 3, 0);
            this.tlpAssignVaultInnerPanel.Controls.Add(this.tlpSites, 1, 1);
            this.tlpAssignVaultInnerPanel.Controls.Add(this.lvAssignSite, 3, 1);
            this.tlpAssignVaultInnerPanel.Controls.Add(this.tlpAssigningButtons, 2, 1);
            this.tlpAssignVaultInnerPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpAssignVaultInnerPanel.Location = new System.Drawing.Point(3, 38);
            this.tlpAssignVaultInnerPanel.Name = "tlpAssignVaultInnerPanel";
            this.tlpAssignVaultInnerPanel.RowCount = 2;
            this.tlpAssignVaultInnerPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tlpAssignVaultInnerPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpAssignVaultInnerPanel.Size = new System.Drawing.Size(800, 474);
            this.tlpAssignVaultInnerPanel.TabIndex = 0;
            // 
            // tlpVaults
            // 
            this.tlpVaults.ColumnCount = 1;
            this.tlpVaults.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpVaults.Controls.Add(this.txtVaultFilter, 0, 0);
            this.tlpVaults.Controls.Add(this.lbVaults, 0, 1);
            this.tlpVaults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpVaults.Location = new System.Drawing.Point(3, 28);
            this.tlpVaults.Name = "tlpVaults";
            this.tlpVaults.RowCount = 2;
            this.tlpVaults.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tlpVaults.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpVaults.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpVaults.Size = new System.Drawing.Size(222, 443);
            this.tlpVaults.TabIndex = 0;
            // 
            // txtVaultFilter
            // 
            this.txtVaultFilter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtVaultFilter.ForeColor = System.Drawing.SystemColors.GrayText;
            this.txtVaultFilter.Location = new System.Drawing.Point(3, 3);
            this.txtVaultFilter.Name = "txtVaultFilter";
            this.txtVaultFilter.Size = new System.Drawing.Size(216, 20);
            this.txtVaultFilter.TabIndex = 0;
            this.txtVaultFilter.Text = "Search";
            this.txtVaultFilter.TextChanged += new System.EventHandler(this.txtVaultFilter_TextChanged);
            this.txtVaultFilter.Enter += new System.EventHandler(this.txtVaultFilter_Enter);
            this.txtVaultFilter.Leave += new System.EventHandler(this.txtVaultFilter_Leave);
            // 
            // lbVaults
            // 
            this.lbVaults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbVaults.FormattingEnabled = true;
            this.lbVaults.Location = new System.Drawing.Point(3, 28);
            this.lbVaults.Name = "lbVaults";
            this.lbVaults.Size = new System.Drawing.Size(216, 412);
            this.lbVaults.TabIndex = 1;
            // 
            // lblVaults
            // 
            this.lblVaults.AutoSize = true;
            this.lblVaults.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVaults.Location = new System.Drawing.Point(3, 0);
            this.lblVaults.Name = "lblVaults";
            this.lblVaults.Size = new System.Drawing.Size(54, 15);
            this.lblVaults.TabIndex = 1;
            this.lblVaults.Text = "Vaults :";
            // 
            // lblSites
            // 
            this.lblSites.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblSites.AutoSize = true;
            this.lblSites.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSites.Location = new System.Drawing.Point(231, 5);
            this.lblSites.Name = "lblSites";
            this.lblSites.Size = new System.Drawing.Size(47, 15);
            this.lblSites.TabIndex = 2;
            this.lblSites.Text = "Sites :";
            // 
            // lblAssignVault
            // 
            this.lblAssignVault.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblAssignVault.AutoSize = true;
            this.lblAssignVault.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAssignVault.Location = new System.Drawing.Point(499, 5);
            this.lblAssignVault.Name = "lblAssignVault";
            this.lblAssignVault.Size = new System.Drawing.Size(158, 15);
            this.lblAssignVault.TabIndex = 3;
            this.lblAssignVault.Text = "Assigning Vault to Site :";
            // 
            // tlpSites
            // 
            this.tlpSites.ColumnCount = 1;
            this.tlpSites.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpSites.Controls.Add(this.txtSitesFilter, 0, 0);
            this.tlpSites.Controls.Add(this.lbSites, 0, 1);
            this.tlpSites.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpSites.Location = new System.Drawing.Point(231, 28);
            this.tlpSites.Name = "tlpSites";
            this.tlpSites.RowCount = 2;
            this.tlpSites.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tlpSites.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpSites.Size = new System.Drawing.Size(222, 443);
            this.tlpSites.TabIndex = 4;
            // 
            // txtSitesFilter
            // 
            this.txtSitesFilter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSitesFilter.ForeColor = System.Drawing.SystemColors.GrayText;
            this.txtSitesFilter.Location = new System.Drawing.Point(3, 3);
            this.txtSitesFilter.Name = "txtSitesFilter";
            this.txtSitesFilter.Size = new System.Drawing.Size(216, 20);
            this.txtSitesFilter.TabIndex = 0;
            this.txtSitesFilter.Text = "Search";
            this.txtSitesFilter.TextChanged += new System.EventHandler(this.txtSitesFilter_TextChanged);
            this.txtSitesFilter.Enter += new System.EventHandler(this.txtSitesFilter_Enter);
            this.txtSitesFilter.Leave += new System.EventHandler(this.txtSitesFilter_Leave);
            // 
            // lbSites
            // 
            this.lbSites.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbSites.FormattingEnabled = true;
            this.lbSites.Location = new System.Drawing.Point(3, 28);
            this.lbSites.Name = "lbSites";
            this.lbSites.Size = new System.Drawing.Size(216, 412);
            this.lbSites.TabIndex = 1;
            // 
            // lvAssignSite
            // 
            this.lvAssignSite.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clmVaults,
            this.clmSites});
            this.lvAssignSite.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvAssignSite.FullRowSelect = true;
            this.lvAssignSite.GridLines = true;
            this.lvAssignSite.Location = new System.Drawing.Point(499, 28);
            this.lvAssignSite.Margin = new System.Windows.Forms.Padding(3, 3, 3, 12);
            this.lvAssignSite.Name = "lvAssignSite";
            this.lvAssignSite.Size = new System.Drawing.Size(298, 434);
            this.lvAssignSite.TabIndex = 5;
            this.lvAssignSite.UseCompatibleStateImageBehavior = false;
            this.lvAssignSite.View = System.Windows.Forms.View.Details;
            // 
            // clmVaults
            // 
            this.clmVaults.Text = "Vaults";
            this.clmVaults.Width = 137;
            // 
            // clmSites
            // 
            this.clmSites.Text = "Sites";
            this.clmSites.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.clmSites.Width = 150;
            // 
            // tlpAssigningButtons
            // 
            this.tlpAssigningButtons.ColumnCount = 1;
            this.tlpAssigningButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpAssigningButtons.Controls.Add(this.btnAssign, 0, 0);
            this.tlpAssigningButtons.Controls.Add(this.btnUnassign, 0, 1);
            this.tlpAssigningButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpAssigningButtons.Location = new System.Drawing.Point(459, 28);
            this.tlpAssigningButtons.Name = "tlpAssigningButtons";
            this.tlpAssigningButtons.RowCount = 2;
            this.tlpAssigningButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpAssigningButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpAssigningButtons.Size = new System.Drawing.Size(34, 443);
            this.tlpAssigningButtons.TabIndex = 6;
            // 
            // btnAssign
            // 
            this.btnAssign.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAssign.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAssign.Location = new System.Drawing.Point(3, 188);
            this.btnAssign.Name = "btnAssign";
            this.btnAssign.Size = new System.Drawing.Size(28, 30);
            this.btnAssign.TabIndex = 0;
            this.btnAssign.Text = ">";
            this.btnAssign.UseVisualStyleBackColor = true;
            this.btnAssign.Click += new System.EventHandler(this.btnAssign_Click);
            // 
            // btnUnassign
            // 
            this.btnUnassign.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUnassign.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUnassign.Location = new System.Drawing.Point(3, 224);
            this.btnUnassign.Name = "btnUnassign";
            this.btnUnassign.Size = new System.Drawing.Size(28, 30);
            this.btnUnassign.TabIndex = 1;
            this.btnUnassign.Text = "<";
            this.btnUnassign.UseVisualStyleBackColor = true;
            this.btnUnassign.Click += new System.EventHandler(this.btnUnassign_Click);
            // 
            // tlpAssignVaultButtons
            // 
            this.tlpAssignVaultButtons.ColumnCount = 4;
            this.tlpAssignVaultButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpAssignVaultButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpAssignVaultButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tlpAssignVaultButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tlpAssignVaultButtons.Controls.Add(this.btnAssignVaultSave, 2, 0);
            this.tlpAssignVaultButtons.Controls.Add(this.btnAssignVaultClose, 3, 0);
            this.tlpAssignVaultButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpAssignVaultButtons.Location = new System.Drawing.Point(3, 518);
            this.tlpAssignVaultButtons.Name = "tlpAssignVaultButtons";
            this.tlpAssignVaultButtons.RowCount = 1;
            this.tlpAssignVaultButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpAssignVaultButtons.Size = new System.Drawing.Size(800, 29);
            this.tlpAssignVaultButtons.TabIndex = 2;
            // 
            // btnAssignVaultSave
            // 
            this.btnAssignVaultSave.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnAssignVaultSave.Location = new System.Drawing.Point(603, 3);
            this.btnAssignVaultSave.Name = "btnAssignVaultSave";
            this.btnAssignVaultSave.Size = new System.Drawing.Size(94, 23);
            this.btnAssignVaultSave.TabIndex = 0;
            this.btnAssignVaultSave.Text = "&Save";
            this.btnAssignVaultSave.UseVisualStyleBackColor = true;
            this.btnAssignVaultSave.Click += new System.EventHandler(this.btnAssignVaultSave_Click);
            // 
            // btnAssignVaultClose
            // 
            this.btnAssignVaultClose.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnAssignVaultClose.Location = new System.Drawing.Point(703, 3);
            this.btnAssignVaultClose.Name = "btnAssignVaultClose";
            this.btnAssignVaultClose.Size = new System.Drawing.Size(94, 23);
            this.btnAssignVaultClose.TabIndex = 1;
            this.btnAssignVaultClose.Text = "&Close";
            this.btnAssignVaultClose.UseVisualStyleBackColor = true;
            this.btnAssignVaultClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // tlp_AssignToSite
            // 
            this.tlp_AssignToSite.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tlp_AssignToSite.BackColor = System.Drawing.Color.SteelBlue;
            this.tlp_AssignToSite.ColumnCount = 1;
            this.tlp_AssignToSite.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlp_AssignToSite.Controls.Add(this.lbl_AssignToSite, 0, 0);
            this.tlp_AssignToSite.Location = new System.Drawing.Point(3, 3);
            this.tlp_AssignToSite.Name = "tlp_AssignToSite";
            this.tlp_AssignToSite.RowCount = 1;
            this.tlp_AssignToSite.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlp_AssignToSite.Size = new System.Drawing.Size(800, 29);
            this.tlp_AssignToSite.TabIndex = 3;
            // 
            // lbl_AssignToSite
            // 
            this.lbl_AssignToSite.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_AssignToSite.AutoSize = true;
            this.lbl_AssignToSite.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_AssignToSite.ForeColor = System.Drawing.Color.White;
            this.lbl_AssignToSite.Location = new System.Drawing.Point(3, 6);
            this.lbl_AssignToSite.Name = "lbl_AssignToSite";
            this.lbl_AssignToSite.Size = new System.Drawing.Size(794, 17);
            this.lbl_AssignToSite.TabIndex = 0;
            this.lbl_AssignToSite.Text = "Assign Vault to Site";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(200, 100);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // ucfrmAddVault
            // 
            this.ucfrmAddVault.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucfrmAddVault.Location = new System.Drawing.Point(3, 3);
            this.ucfrmAddVault.Name = "ucfrmAddVault";
            this.ucfrmAddVault.Size = new System.Drawing.Size(794, 508);
            this.ucfrmAddVault.TabIndex = 0;
            // 
            // txtSoldPrice
            // 
            this.txtSoldPrice.AllowDecimal = true;
            this.txtSoldPrice.AllowNegative = false;
            this.txtSoldPrice.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtSoldPrice.DecimalLength = 2;
            this.txtSoldPrice.Denom = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtSoldPrice.Enabled = false;
            this.txtSoldPrice.Location = new System.Drawing.Point(203, 347);
            this.txtSoldPrice.MaxLength = 9;
            this.txtSoldPrice.MaxVaule = new decimal(new int[] {
            99999999,
            0,
            0,
            131072});
            this.txtSoldPrice.Name = "txtSoldPrice";
            this.txtSoldPrice.ShortcutsEnabled = false;
            this.txtSoldPrice.Size = new System.Drawing.Size(206, 20);
            this.txtSoldPrice.TabIndex = 11;
            // 
            // txtPurchasePrice
            // 
            this.txtPurchasePrice.AllowDecimal = true;
            this.txtPurchasePrice.AllowNegative = false;
            this.txtPurchasePrice.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtPurchasePrice.DecimalLength = 2;
            this.txtPurchasePrice.Denom = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtPurchasePrice.Location = new System.Drawing.Point(203, 137);
            this.txtPurchasePrice.MaxLength = 9;
            this.txtPurchasePrice.MaxVaule = new decimal(new int[] {
            99999999,
            0,
            0,
            131072});
            this.txtPurchasePrice.Name = "txtPurchasePrice";
            this.txtPurchasePrice.ShortcutsEnabled = false;
            this.txtPurchasePrice.Size = new System.Drawing.Size(206, 20);
            this.txtPurchasePrice.TabIndex = 1;
            // 
            // frmVaultAdmin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(820, 632);
            this.Controls.Add(this.tlpMainPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmVaultAdmin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Vault Admin";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmVaultAdmin_FormClosing);
            this.Load += new System.EventHandler(this.frmVaultAdmin_Load);
            this.tlpMainPanel.ResumeLayout(false);
            this.tcVaultAdmin.ResumeLayout(false);
            this.tpVault.ResumeLayout(false);
            this.tlpVault.ResumeLayout(false);
            this.tpVaultFinance.ResumeLayout(false);
            this.tlpVaultFinance.ResumeLayout(false);
            this.tlpFinanceInnerPanel.ResumeLayout(false);
            this.tlpFinanceInnerPanel.PerformLayout();
            this.tlpButtons.ResumeLayout(false);
            this.tlp_FinanceHeader.ResumeLayout(false);
            this.tlp_FinanceHeader.PerformLayout();
            this.tpAssignVaultToSite.ResumeLayout(false);
            this.tlpAssignVaultMainPanel.ResumeLayout(false);
            this.tlpAssignVaultInnerPanel.ResumeLayout(false);
            this.tlpAssignVaultInnerPanel.PerformLayout();
            this.tlpVaults.ResumeLayout(false);
            this.tlpVaults.PerformLayout();
            this.tlpSites.ResumeLayout(false);
            this.tlpSites.PerformLayout();
            this.tlpAssigningButtons.ResumeLayout(false);
            this.tlpAssignVaultButtons.ResumeLayout(false);
            this.tlp_AssignToSite.ResumeLayout(false);
            this.tlp_AssignToSite.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpMainPanel;
        private System.Windows.Forms.TabControl tcVaultAdmin;
        private System.Windows.Forms.TabPage tpVault;
        private System.Windows.Forms.TabPage tpVaultFinance;
        private System.Windows.Forms.TabPage tpAssignVaultToSite;
        private System.Windows.Forms.TableLayoutPanel tlpVaultFinance;
        private System.Windows.Forms.TableLayoutPanel tlpFinanceInnerPanel;
        private System.Windows.Forms.TableLayoutPanel tlpButtons;
        private System.Windows.Forms.TextBox txtSoldInvoiceNumber;
        private NumberTextBox txtSoldPrice;
        private System.Windows.Forms.TextBox txtInvoiceNumber;
        private System.Windows.Forms.Label lblPurchasePrice;
        private System.Windows.Forms.Label lblSoldDate;
        private System.Windows.Forms.Label lblSoldInvoiceNumber;
        private System.Windows.Forms.Label lblSoldPrice;
        private System.Windows.Forms.Label lblDepreciationStartDate;
        private System.Windows.Forms.Label lblPurchaseData;
        private System.Windows.Forms.Label lblPurchaseInvoiceNumber;
        private NumberTextBox txtPurchasePrice;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.DateTimePicker dtpSoldDate;
        private System.Windows.Forms.DateTimePicker dtpDepreciationStartDate;
        private System.Windows.Forms.DateTimePicker dtpPurchaseDate;
        private System.Windows.Forms.TableLayoutPanel tlpAssignVaultMainPanel;
        private System.Windows.Forms.TableLayoutPanel tlpAssignVaultInnerPanel;
        private System.Windows.Forms.TableLayoutPanel tlpAssignVaultButtons;
        private System.Windows.Forms.Button btnAssignVaultSave;
        private System.Windows.Forms.Button btnAssignVaultClose;
        private System.Windows.Forms.Label lblVaults;
        private System.Windows.Forms.Label lblSites;
        private System.Windows.Forms.Label lblAssignVault;
        private System.Windows.Forms.TableLayoutPanel tlpVaults;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TextBox txtVaultFilter;
        private System.Windows.Forms.TableLayoutPanel tlpSites;
        private System.Windows.Forms.TextBox txtSitesFilter;
        private System.Windows.Forms.ListBox lbSites;
        private System.Windows.Forms.ListView lvAssignSite;
        private System.Windows.Forms.ColumnHeader clmVaults;
        private System.Windows.Forms.ColumnHeader clmSites;
        private System.Windows.Forms.ListBox lbVaults;
        private System.Windows.Forms.TableLayoutPanel tlpAssigningButtons;
        private System.Windows.Forms.Button btnAssign;
        private System.Windows.Forms.Button btnUnassign;
        private System.Windows.Forms.TableLayoutPanel tlpVault;
        private frmAddVault ucfrmAddVault;
        private System.Windows.Forms.Label lbl_SellVault;
        private System.Windows.Forms.CheckBox cb_SellVault;
        private System.Windows.Forms.TableLayoutPanel tlp_FinanceHeader;
        private System.Windows.Forms.Label lbl_FinanceHeader;
        private System.Windows.Forms.TableLayoutPanel tlp_AssignToSite;
        private System.Windows.Forms.Label lbl_AssignToSite;
        private System.Windows.Forms.Button btn_CloseAddVault;
        private System.Windows.Forms.Label lbl_VaultName;
        private System.Windows.Forms.Label lbl_SerialNo;
        private System.Windows.Forms.Label lbl_Prefix;
        private System.Windows.Forms.Label lbl_PrefixText;
        private System.Windows.Forms.Label lbl_SerialNoTxt;
        private System.Windows.Forms.Label lbl_VaultText;
       
    }
}