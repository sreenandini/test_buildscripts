namespace BMCSiteLicensing
{
    partial class frmSiteLicensing
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSiteLicensing));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.imgltSiteStatus = new System.Windows.Forms.ImageList(this.components);
            this.pnlSiteLicensing = new System.Windows.Forms.Panel();
            this.flowLayoutPanel5 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnAddNew = new System.Windows.Forms.Button();
            this.btnSLUpdate = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.btnCancelLicense = new System.Windows.Forms.Button();
            this.btnActivateLicense = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnLHClear = new System.Windows.Forms.Button();
            this.btn_Refresh = new System.Windows.Forms.Button();
            this.tabPgLicenseGen = new System.Windows.Forms.TabPage();
            this.tabLicenseGen = new System.Windows.Forms.TabControl();
            this.tabPgRuleINfo = new System.Windows.Forms.TabPage();
            this.grpRuleSettings = new System.Windows.Forms.GroupBox();
            this.grpValidationParam = new System.Windows.Forms.GroupBox();
            this.tbllpValidationParam = new System.Windows.Forms.TableLayoutPanel();
            this.chkRSLockSite = new System.Windows.Forms.CheckBox();
            this.chkRSAlertreq = new System.Windows.Forms.CheckBox();
            this.chkRSWarningOnly = new System.Windows.Forms.CheckBox();
            this.chkRSDisableEGMs = new System.Windows.Forms.CheckBox();
            this.txtRSRuleName = new System.Windows.Forms.TextBox();
            this.lblRSRuleName = new System.Windows.Forms.Label();
            this.chkRSValidationReq = new System.Windows.Forms.CheckBox();
            this.grpRuleNames = new System.Windows.Forms.GroupBox();
            this.lstRuleNames = new System.Windows.Forms.ListBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addNewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabPgKeyGeneration = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.grpSiteSelection = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel13 = new System.Windows.Forms.TableLayoutPanel();
            this.lblCompName = new System.Windows.Forms.Label();
            this.lblSiteName = new System.Windows.Forms.Label();
            this.cmbSiteName = new BMC.Common.Utilities.BmcComboBox();
            this.lblSubCompName = new System.Windows.Forms.Label();
            this.cmbCompName = new BMC.Common.Utilities.BmcComboBox();
            this.cmbSubCompName = new BMC.Common.Utilities.BmcComboBox();
            this.grpgenLicense = new System.Windows.Forms.GroupBox();
            this.txtLicenseKey = new System.Windows.Forms.TextBox();
            this.lblLicenseKey = new System.Windows.Forms.Label();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.tableLayoutPanel8 = new System.Windows.Forms.TableLayoutPanel();
            this.grpLicenseSettings = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel10 = new System.Windows.Forms.TableLayoutPanel();
            this.grpValidationParameter = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.chkLSValidationReq = new System.Windows.Forms.CheckBox();
            this.chkLSAlertReq = new System.Windows.Forms.CheckBox();
            this.chkLSWarningOnly = new System.Windows.Forms.CheckBox();
            this.chkLSDisableEGMs = new System.Windows.Forms.CheckBox();
            this.chkLSLockSite = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel11 = new System.Windows.Forms.TableLayoutPanel();
            this.lblStartDate = new System.Windows.Forms.Label();
            this.lblExpiryDate = new System.Windows.Forms.Label();
            this.lblLSRuleName = new System.Windows.Forms.Label();
            this.dtpkStartDate = new System.Windows.Forms.DateTimePicker();
            this.dtpkExpiryDate = new System.Windows.Forms.DateTimePicker();
            this.cmbLSRuleName = new BMC.Common.Utilities.BmcComboBox();
            this.tableLayoutPanel12 = new System.Windows.Forms.TableLayoutPanel();
            this.lblAlertBefor = new System.Windows.Forms.Label();
            this.lblinDays = new System.Windows.Forms.Label();
            this.numUDAlertBefor = new System.Windows.Forms.NumericUpDown();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dtgvAssociatedSites = new System.Windows.Forms.DataGridView();
            this.pnlKeyGeneration = new System.Windows.Forms.Panel();
            this.tabSiteLicensing = new System.Windows.Forms.TabControl();
            this.tabPgViewCancelLicense = new System.Windows.Forms.TabPage();
            this.sC_ViewCancelLicense = new System.Windows.Forms.SplitContainer();
            this.grpSiteList = new System.Windows.Forms.GroupBox();
            this.tvSiteList = new System.Windows.Forms.TreeView();
            this.pnlLicenseDetails = new System.Windows.Forms.Panel();
            this.tableLayoutPanel9 = new System.Windows.Forms.TableLayoutPanel();
            this.grpLicenseDetails = new System.Windows.Forms.GroupBox();
            this.dtgvLicenseDetails = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cancelLicenseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.activateLicenseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabPgLicenseHistory = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.grpSearchCriteria = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel7 = new System.Windows.Forms.TableLayoutPanel();
            this.tbllpSearchcriteria = new System.Windows.Forms.TableLayoutPanel();
            this.dtpkToExpiryDate = new System.Windows.Forms.DateTimePicker();
            this.cmbSCCompanyName = new BMC.Common.Utilities.BmcComboBox();
            this.cmbSCSubCompanyName = new BMC.Common.Utilities.BmcComboBox();
            this.lblSCSubCompName = new System.Windows.Forms.Label();
            this.lblFromExpiryDate = new System.Windows.Forms.Label();
            this.dtpkFromExpiryDate = new System.Windows.Forms.DateTimePicker();
            this.lblToExpiryDate = new System.Windows.Forms.Label();
            this.lblfromStartDate = new System.Windows.Forms.Label();
            this.lblSCSiteName = new System.Windows.Forms.Label();
            this.dtpkFromStartDate = new System.Windows.Forms.DateTimePicker();
            this.cmbSCSiteName = new BMC.Common.Utilities.BmcComboBox();
            this.lblToStartDate = new System.Windows.Forms.Label();
            this.lblSCKeyStatus = new System.Windows.Forms.Label();
            this.dtpkToStartDate = new System.Windows.Forms.DateTimePicker();
            this.cmbSCKeyStatus = new BMC.Common.Utilities.BmcComboBox();
            this.lblSCCompName = new System.Windows.Forms.Label();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.grpSCValidationParam = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.cmbSCLockSite = new BMC.Common.Utilities.BmcComboBox();
            this.lblSCLockSite = new System.Windows.Forms.Label();
            this.cmbSCValidationReq = new BMC.Common.Utilities.BmcComboBox();
            this.lblSCValidationReq = new System.Windows.Forms.Label();
            this.lblSCDisableEGM = new System.Windows.Forms.Label();
            this.cmbSCDisableEGM = new BMC.Common.Utilities.BmcComboBox();
            this.lblSCWarningOnly = new System.Windows.Forms.Label();
            this.cmbSCWarningOnly = new BMC.Common.Utilities.BmcComboBox();
            this.lblAlertRequired = new System.Windows.Forms.Label();
            this.cmbSCAlertRequired = new BMC.Common.Utilities.BmcComboBox();
            this.grpUserFilter = new System.Windows.Forms.GroupBox();
            this.tbluserfilter = new System.Windows.Forms.TableLayoutPanel();
            this.cmbCancelBy = new BMC.Common.Utilities.BmcComboBox();
            this.cmbActivatedBy = new BMC.Common.Utilities.BmcComboBox();
            this.cmbcreateBy = new BMC.Common.Utilities.BmcComboBox();
            this.lblActivatedBy = new System.Windows.Forms.Label();
            this.lblcreate = new System.Windows.Forms.Label();
            this.lblCancelBy = new System.Windows.Forms.Label();
            this.grpSearchResults = new System.Windows.Forms.GroupBox();
            this.dtGVSearchResults = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel14 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pnlSiteLicensing.SuspendLayout();
            this.flowLayoutPanel5.SuspendLayout();
            this.tabPgLicenseGen.SuspendLayout();
            this.tabLicenseGen.SuspendLayout();
            this.tabPgRuleINfo.SuspendLayout();
            this.grpRuleSettings.SuspendLayout();
            this.grpValidationParam.SuspendLayout();
            this.tbllpValidationParam.SuspendLayout();
            this.grpRuleNames.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.tabPgKeyGeneration.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            this.grpSiteSelection.SuspendLayout();
            this.tableLayoutPanel13.SuspendLayout();
            this.grpgenLicense.SuspendLayout();
            this.tableLayoutPanel8.SuspendLayout();
            this.grpLicenseSettings.SuspendLayout();
            this.tableLayoutPanel10.SuspendLayout();
            this.grpValidationParameter.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel11.SuspendLayout();
            this.tableLayoutPanel12.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUDAlertBefor)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvAssociatedSites)).BeginInit();
            this.tabSiteLicensing.SuspendLayout();
            this.tabPgViewCancelLicense.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sC_ViewCancelLicense)).BeginInit();
            this.sC_ViewCancelLicense.Panel1.SuspendLayout();
            this.sC_ViewCancelLicense.Panel2.SuspendLayout();
            this.sC_ViewCancelLicense.SuspendLayout();
            this.grpSiteList.SuspendLayout();
            this.pnlLicenseDetails.SuspendLayout();
            this.tableLayoutPanel9.SuspendLayout();
            this.grpLicenseDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvLicenseDetails)).BeginInit();
            this.contextMenuStrip2.SuspendLayout();
            this.tabPgLicenseHistory.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.grpSearchCriteria.SuspendLayout();
            this.tableLayoutPanel7.SuspendLayout();
            this.tbllpSearchcriteria.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.grpSCValidationParam.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.grpUserFilter.SuspendLayout();
            this.tbluserfilter.SuspendLayout();
            this.grpSearchResults.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtGVSearchResults)).BeginInit();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel14.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // imgltSiteStatus
            // 
            this.imgltSiteStatus.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgltSiteStatus.ImageStream")));
            this.imgltSiteStatus.TransparentColor = System.Drawing.Color.Transparent;
            this.imgltSiteStatus.Images.SetKeyName(0, "empty");
            this.imgltSiteStatus.Images.SetKeyName(1, "Created");
            this.imgltSiteStatus.Images.SetKeyName(2, "Active");
            this.imgltSiteStatus.Images.SetKeyName(3, "Expired");
            this.imgltSiteStatus.Images.SetKeyName(4, "Cancelled");
            this.imgltSiteStatus.Images.SetKeyName(5, "Company.ico");
            this.imgltSiteStatus.Images.SetKeyName(6, "SubCompany.ico");
            this.imgltSiteStatus.Images.SetKeyName(7, "Site.ico");
            this.imgltSiteStatus.Images.SetKeyName(8, "Companies.ico");
            // 
            // pnlSiteLicensing
            // 
            this.pnlSiteLicensing.BackColor = System.Drawing.Color.Transparent;
            this.pnlSiteLicensing.Controls.Add(this.flowLayoutPanel5);
            this.pnlSiteLicensing.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlSiteLicensing.Location = new System.Drawing.Point(172, 0);
            this.pnlSiteLicensing.Margin = new System.Windows.Forms.Padding(0);
            this.pnlSiteLicensing.Name = "pnlSiteLicensing";
            this.pnlSiteLicensing.Size = new System.Drawing.Size(979, 45);
            this.pnlSiteLicensing.TabIndex = 2;
            // 
            // flowLayoutPanel5
            // 
            this.flowLayoutPanel5.Controls.Add(this.btnClose);
            this.flowLayoutPanel5.Controls.Add(this.btnUpdate);
            this.flowLayoutPanel5.Controls.Add(this.btnAddNew);
            this.flowLayoutPanel5.Controls.Add(this.btnSLUpdate);
            this.flowLayoutPanel5.Controls.Add(this.button1);
            this.flowLayoutPanel5.Controls.Add(this.btnCancelLicense);
            this.flowLayoutPanel5.Controls.Add(this.btnActivateLicense);
            this.flowLayoutPanel5.Controls.Add(this.btnSearch);
            this.flowLayoutPanel5.Controls.Add(this.btnLHClear);
            this.flowLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel5.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel5.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel5.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel5.Name = "flowLayoutPanel5";
            this.flowLayoutPanel5.Size = new System.Drawing.Size(979, 45);
            this.flowLayoutPanel5.TabIndex = 0;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.btnClose.Location = new System.Drawing.Point(874, 5);
            this.btnClose.Margin = new System.Windows.Forms.Padding(5);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(100, 28);
            this.btnClose.TabIndex = 5;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnUpdate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.btnUpdate.Location = new System.Drawing.Point(769, 5);
            this.btnUpdate.Margin = new System.Windows.Forms.Padding(5, 5, 0, 5);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(100, 28);
            this.btnUpdate.TabIndex = 4;
            this.btnUpdate.Text = "&Edit";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnAddNew
            // 
            this.btnAddNew.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnAddNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.btnAddNew.Location = new System.Drawing.Point(664, 5);
            this.btnAddNew.Margin = new System.Windows.Forms.Padding(5, 5, 0, 5);
            this.btnAddNew.Name = "btnAddNew";
            this.btnAddNew.Size = new System.Drawing.Size(100, 28);
            this.btnAddNew.TabIndex = 3;
            this.btnAddNew.Text = "Add &New";
            this.btnAddNew.UseVisualStyleBackColor = true;
            this.btnAddNew.Click += new System.EventHandler(this.btnAddNew_Click);
            // 
            // btnSLUpdate
            // 
            this.btnSLUpdate.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnSLUpdate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.btnSLUpdate.Location = new System.Drawing.Point(559, 5);
            this.btnSLUpdate.Margin = new System.Windows.Forms.Padding(5, 5, 0, 5);
            this.btnSLUpdate.Name = "btnSLUpdate";
            this.btnSLUpdate.Size = new System.Drawing.Size(100, 28);
            this.btnSLUpdate.TabIndex = 2;
            this.btnSLUpdate.Text = "&Update";
            this.btnSLUpdate.UseVisualStyleBackColor = true;
            this.btnSLUpdate.Click += new System.EventHandler(this.btnSLUpdate_Click);
            // 
            // button1
            // 
            this.button1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.button1.Location = new System.Drawing.Point(454, 5);
            this.button1.Margin = new System.Windows.Forms.Padding(5, 5, 0, 5);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 28);
            this.button1.TabIndex = 1;
            this.button1.Text = "C&lear";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnCancelLicense
            // 
            this.btnCancelLicense.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.btnCancelLicense.Location = new System.Drawing.Point(349, 5);
            this.btnCancelLicense.Margin = new System.Windows.Forms.Padding(5, 5, 0, 5);
            this.btnCancelLicense.Name = "btnCancelLicense";
            this.btnCancelLicense.Size = new System.Drawing.Size(100, 28);
            this.btnCancelLicense.TabIndex = 0;
            this.btnCancelLicense.Text = "Ca&ncel License";
            this.btnCancelLicense.UseVisualStyleBackColor = true;
            this.btnCancelLicense.Click += new System.EventHandler(this.btnCancelLicense_Click);
            // 
            // btnActivateLicense
            // 
            this.btnActivateLicense.Location = new System.Drawing.Point(235, 5);
            this.btnActivateLicense.Margin = new System.Windows.Forms.Padding(5, 5, 0, 5);
            this.btnActivateLicense.Name = "btnActivateLicense";
            this.btnActivateLicense.Size = new System.Drawing.Size(109, 28);
            this.btnActivateLicense.TabIndex = 9;
            this.btnActivateLicense.Text = "&Activate License";
            this.btnActivateLicense.UseVisualStyleBackColor = true;
            this.btnActivateLicense.Click += new System.EventHandler(this.btnActivateLicense_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.btnSearch.Location = new System.Drawing.Point(130, 5);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(5, 5, 0, 5);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(100, 28);
            this.btnSearch.TabIndex = 8;
            this.btnSearch.Text = " S&earch";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnLHClear
            // 
            this.btnLHClear.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnLHClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.btnLHClear.Location = new System.Drawing.Point(25, 5);
            this.btnLHClear.Margin = new System.Windows.Forms.Padding(5, 5, 0, 5);
            this.btnLHClear.Name = "btnLHClear";
            this.btnLHClear.Size = new System.Drawing.Size(100, 28);
            this.btnLHClear.TabIndex = 7;
            this.btnLHClear.Text = "C&lear";
            this.btnLHClear.UseVisualStyleBackColor = true;
            this.btnLHClear.Click += new System.EventHandler(this.btnLHClear_Click);
            // 
            // btn_Refresh
            // 
            this.btn_Refresh.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btn_Refresh.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.btn_Refresh.Location = new System.Drawing.Point(21, 5);
            this.btn_Refresh.Margin = new System.Windows.Forms.Padding(5, 5, 0, 5);
            this.btn_Refresh.Name = "btn_Refresh";
            this.btn_Refresh.Size = new System.Drawing.Size(100, 28);
            this.btn_Refresh.TabIndex = 6;
            this.btn_Refresh.Text = "&Refresh";
            this.btn_Refresh.UseVisualStyleBackColor = true;
            this.btn_Refresh.Click += new System.EventHandler(this.btn_Refresh_Click);
            // 
            // tabPgLicenseGen
            // 
            this.tabPgLicenseGen.Controls.Add(this.tabLicenseGen);
            this.tabPgLicenseGen.Location = new System.Drawing.Point(4, 22);
            this.tabPgLicenseGen.Name = "tabPgLicenseGen";
            this.tabPgLicenseGen.Padding = new System.Windows.Forms.Padding(3);
            this.tabPgLicenseGen.Size = new System.Drawing.Size(1137, 604);
            this.tabPgLicenseGen.TabIndex = 1;
            this.tabPgLicenseGen.Text = "License Generation";
            this.tabPgLicenseGen.UseVisualStyleBackColor = true;
            // 
            // tabLicenseGen
            // 
            this.tabLicenseGen.Controls.Add(this.tabPgRuleINfo);
            this.tabLicenseGen.Controls.Add(this.tabPgKeyGeneration);
            this.tabLicenseGen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabLicenseGen.Location = new System.Drawing.Point(3, 3);
            this.tabLicenseGen.Name = "tabLicenseGen";
            this.tabLicenseGen.SelectedIndex = 0;
            this.tabLicenseGen.Size = new System.Drawing.Size(1131, 598);
            this.tabLicenseGen.TabIndex = 0;
            this.tabLicenseGen.SelectedIndexChanged += new System.EventHandler(this.tabLicenseGen_SelectedIndexChanged);
            // 
            // tabPgRuleINfo
            // 
            this.tabPgRuleINfo.Controls.Add(this.grpRuleSettings);
            this.tabPgRuleINfo.Controls.Add(this.grpRuleNames);
            this.tabPgRuleINfo.Location = new System.Drawing.Point(4, 22);
            this.tabPgRuleINfo.Name = "tabPgRuleINfo";
            this.tabPgRuleINfo.Padding = new System.Windows.Forms.Padding(3);
            this.tabPgRuleINfo.Size = new System.Drawing.Size(1123, 572);
            this.tabPgRuleINfo.TabIndex = 2;
            this.tabPgRuleINfo.Text = "Rule Information";
            this.tabPgRuleINfo.UseVisualStyleBackColor = true;
            this.tabPgRuleINfo.Leave += new System.EventHandler(this.tabPgRuleINfo_Leave);
            // 
            // grpRuleSettings
            // 
            this.grpRuleSettings.Controls.Add(this.grpValidationParam);
            this.grpRuleSettings.Controls.Add(this.txtRSRuleName);
            this.grpRuleSettings.Controls.Add(this.lblRSRuleName);
            this.grpRuleSettings.Controls.Add(this.chkRSValidationReq);
            this.grpRuleSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpRuleSettings.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold);
            this.grpRuleSettings.Location = new System.Drawing.Point(264, 3);
            this.grpRuleSettings.Name = "grpRuleSettings";
            this.grpRuleSettings.Size = new System.Drawing.Size(856, 566);
            this.grpRuleSettings.TabIndex = 1;
            this.grpRuleSettings.TabStop = false;
            this.grpRuleSettings.Text = "Rule Settings";
            // 
            // grpValidationParam
            // 
            this.grpValidationParam.Controls.Add(this.tbllpValidationParam);
            this.grpValidationParam.Location = new System.Drawing.Point(12, 85);
            this.grpValidationParam.Name = "grpValidationParam";
            this.grpValidationParam.Size = new System.Drawing.Size(369, 162);
            this.grpValidationParam.TabIndex = 3;
            this.grpValidationParam.TabStop = false;
            this.grpValidationParam.Text = "Validation Parameters";
            // 
            // tbllpValidationParam
            // 
            this.tbllpValidationParam.ColumnCount = 1;
            this.tbllpValidationParam.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tbllpValidationParam.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tbllpValidationParam.Controls.Add(this.chkRSLockSite, 0, 0);
            this.tbllpValidationParam.Controls.Add(this.chkRSAlertreq, 0, 3);
            this.tbllpValidationParam.Controls.Add(this.chkRSWarningOnly, 0, 2);
            this.tbllpValidationParam.Controls.Add(this.chkRSDisableEGMs, 0, 1);
            this.tbllpValidationParam.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbllpValidationParam.Location = new System.Drawing.Point(3, 19);
            this.tbllpValidationParam.Name = "tbllpValidationParam";
            this.tbllpValidationParam.Padding = new System.Windows.Forms.Padding(3);
            this.tbllpValidationParam.RowCount = 4;
            this.tbllpValidationParam.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tbllpValidationParam.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tbllpValidationParam.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tbllpValidationParam.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tbllpValidationParam.Size = new System.Drawing.Size(363, 140);
            this.tbllpValidationParam.TabIndex = 0;
            // 
            // chkRSLockSite
            // 
            this.chkRSLockSite.AutoSize = true;
            this.chkRSLockSite.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.chkRSLockSite.Location = new System.Drawing.Point(6, 6);
            this.chkRSLockSite.Name = "chkRSLockSite";
            this.chkRSLockSite.Size = new System.Drawing.Size(78, 17);
            this.chkRSLockSite.TabIndex = 0;
            this.chkRSLockSite.Text = "Lock Site";
            this.chkRSLockSite.UseVisualStyleBackColor = true;
            this.chkRSLockSite.CheckedChanged += new System.EventHandler(this.chkRSLockSite_CheckedChanged);
            // 
            // chkRSAlertreq
            // 
            this.chkRSAlertreq.AutoSize = true;
            this.chkRSAlertreq.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.chkRSAlertreq.Location = new System.Drawing.Point(6, 105);
            this.chkRSAlertreq.Name = "chkRSAlertreq";
            this.chkRSAlertreq.Size = new System.Drawing.Size(108, 17);
            this.chkRSAlertreq.TabIndex = 3;
            this.chkRSAlertreq.Text = "Alert Required";
            this.chkRSAlertreq.UseVisualStyleBackColor = true;
            // 
            // chkRSWarningOnly
            // 
            this.chkRSWarningOnly.AutoSize = true;
            this.chkRSWarningOnly.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.chkRSWarningOnly.Location = new System.Drawing.Point(6, 72);
            this.chkRSWarningOnly.Name = "chkRSWarningOnly";
            this.chkRSWarningOnly.Size = new System.Drawing.Size(103, 17);
            this.chkRSWarningOnly.TabIndex = 2;
            this.chkRSWarningOnly.Text = "Warning Only";
            this.chkRSWarningOnly.UseVisualStyleBackColor = true;
            this.chkRSWarningOnly.CheckedChanged += new System.EventHandler(this.chkRSWarningOnly_CheckedChanged);
            // 
            // chkRSDisableEGMs
            // 
            this.chkRSDisableEGMs.AutoSize = true;
            this.chkRSDisableEGMs.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.chkRSDisableEGMs.Location = new System.Drawing.Point(6, 39);
            this.chkRSDisableEGMs.Name = "chkRSDisableEGMs";
            this.chkRSDisableEGMs.Size = new System.Drawing.Size(103, 17);
            this.chkRSDisableEGMs.TabIndex = 1;
            this.chkRSDisableEGMs.Text = "Disable EGMs";
            this.chkRSDisableEGMs.UseVisualStyleBackColor = true;
            this.chkRSDisableEGMs.CheckedChanged += new System.EventHandler(this.chkRSDisableEGMs_CheckedChanged);
            // 
            // txtRSRuleName
            // 
            this.txtRSRuleName.Enabled = false;
            this.txtRSRuleName.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.txtRSRuleName.Location = new System.Drawing.Point(95, 21);
            this.txtRSRuleName.MaxLength = 15;
            this.txtRSRuleName.Name = "txtRSRuleName";
            this.txtRSRuleName.Size = new System.Drawing.Size(256, 21);
            this.txtRSRuleName.TabIndex = 1;
            this.txtRSRuleName.TabStop = false;
            // 
            // lblRSRuleName
            // 
            this.lblRSRuleName.AutoSize = true;
            this.lblRSRuleName.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.lblRSRuleName.Location = new System.Drawing.Point(15, 24);
            this.lblRSRuleName.Name = "lblRSRuleName";
            this.lblRSRuleName.Size = new System.Drawing.Size(78, 13);
            this.lblRSRuleName.TabIndex = 0;
            this.lblRSRuleName.Text = "Rule Name :";
            // 
            // chkRSValidationReq
            // 
            this.chkRSValidationReq.AutoSize = true;
            this.chkRSValidationReq.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.chkRSValidationReq.Location = new System.Drawing.Point(17, 62);
            this.chkRSValidationReq.Name = "chkRSValidationReq";
            this.chkRSValidationReq.Size = new System.Drawing.Size(137, 17);
            this.chkRSValidationReq.TabIndex = 2;
            this.chkRSValidationReq.Text = "Validation Required";
            this.chkRSValidationReq.UseVisualStyleBackColor = true;
            this.chkRSValidationReq.CheckedChanged += new System.EventHandler(this.chkRSValidationReq_CheckedChanged);
            // 
            // grpRuleNames
            // 
            this.grpRuleNames.Controls.Add(this.lstRuleNames);
            this.grpRuleNames.Dock = System.Windows.Forms.DockStyle.Left;
            this.grpRuleNames.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold);
            this.grpRuleNames.Location = new System.Drawing.Point(3, 3);
            this.grpRuleNames.Name = "grpRuleNames";
            this.grpRuleNames.Size = new System.Drawing.Size(261, 566);
            this.grpRuleNames.TabIndex = 0;
            this.grpRuleNames.TabStop = false;
            this.grpRuleNames.Text = "Rule Names";
            // 
            // lstRuleNames
            // 
            this.lstRuleNames.ContextMenuStrip = this.contextMenuStrip1;
            this.lstRuleNames.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstRuleNames.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstRuleNames.FormattingEnabled = true;
            this.lstRuleNames.HorizontalScrollbar = true;
            this.lstRuleNames.Location = new System.Drawing.Point(3, 19);
            this.lstRuleNames.Name = "lstRuleNames";
            this.lstRuleNames.Size = new System.Drawing.Size(255, 544);
            this.lstRuleNames.TabIndex = 0;
            this.lstRuleNames.SelectedIndexChanged += new System.EventHandler(this.lstRuleNames_SelectedIndexChanged);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addNewToolStripMenuItem,
            this.editToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(124, 48);
            // 
            // addNewToolStripMenuItem
            // 
            this.addNewToolStripMenuItem.Name = "addNewToolStripMenuItem";
            this.addNewToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.addNewToolStripMenuItem.Text = "Add New";
            this.addNewToolStripMenuItem.Click += new System.EventHandler(this.btnAddNew_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.editToolStripMenuItem.Text = "Edit";
            this.editToolStripMenuItem.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // tabPgKeyGeneration
            // 
            this.tabPgKeyGeneration.Controls.Add(this.tableLayoutPanel6);
            this.tabPgKeyGeneration.Controls.Add(this.pnlKeyGeneration);
            this.tabPgKeyGeneration.Location = new System.Drawing.Point(4, 22);
            this.tabPgKeyGeneration.Name = "tabPgKeyGeneration";
            this.tabPgKeyGeneration.Padding = new System.Windows.Forms.Padding(3);
            this.tabPgKeyGeneration.Size = new System.Drawing.Size(1123, 572);
            this.tabPgKeyGeneration.TabIndex = 1;
            this.tabPgKeyGeneration.Text = "Key Generation";
            this.tabPgKeyGeneration.UseVisualStyleBackColor = true;
            this.tabPgKeyGeneration.Enter += new System.EventHandler(this.tabPgKeyGeneration_Enter);
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.ColumnCount = 1;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel6.Controls.Add(this.grpSiteSelection, 0, 0);
            this.tableLayoutPanel6.Controls.Add(this.grpgenLicense, 0, 2);
            this.tableLayoutPanel6.Controls.Add(this.tableLayoutPanel8, 0, 1);
            this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel6.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 3;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 28.78741F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 60.07072F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.14188F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(1117, 566);
            this.tableLayoutPanel6.TabIndex = 1;
            // 
            // grpSiteSelection
            // 
            this.grpSiteSelection.Controls.Add(this.tableLayoutPanel13);
            this.grpSiteSelection.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpSiteSelection.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold);
            this.grpSiteSelection.Location = new System.Drawing.Point(3, 3);
            this.grpSiteSelection.Name = "grpSiteSelection";
            this.grpSiteSelection.Size = new System.Drawing.Size(1111, 156);
            this.grpSiteSelection.TabIndex = 0;
            this.grpSiteSelection.TabStop = false;
            this.grpSiteSelection.Text = "Site Selection";
            // 
            // tableLayoutPanel13
            // 
            this.tableLayoutPanel13.ColumnCount = 3;
            this.tableLayoutPanel13.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 160F));
            this.tableLayoutPanel13.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 400F));
            this.tableLayoutPanel13.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel13.Controls.Add(this.lblCompName, 0, 0);
            this.tableLayoutPanel13.Controls.Add(this.lblSiteName, 0, 2);
            this.tableLayoutPanel13.Controls.Add(this.cmbSiteName, 1, 2);
            this.tableLayoutPanel13.Controls.Add(this.lblSubCompName, 0, 1);
            this.tableLayoutPanel13.Controls.Add(this.cmbCompName, 1, 0);
            this.tableLayoutPanel13.Controls.Add(this.cmbSubCompName, 1, 1);
            this.tableLayoutPanel13.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel13.Location = new System.Drawing.Point(3, 19);
            this.tableLayoutPanel13.Name = "tableLayoutPanel13";
            this.tableLayoutPanel13.RowCount = 3;
            this.tableLayoutPanel13.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33F));
            this.tableLayoutPanel13.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33F));
            this.tableLayoutPanel13.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.34F));
            this.tableLayoutPanel13.Size = new System.Drawing.Size(1105, 134);
            this.tableLayoutPanel13.TabIndex = 0;
            // 
            // lblCompName
            // 
            this.lblCompName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCompName.AutoSize = true;
            this.lblCompName.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.lblCompName.Location = new System.Drawing.Point(3, 15);
            this.lblCompName.Name = "lblCompName";
            this.lblCompName.Size = new System.Drawing.Size(154, 13);
            this.lblCompName.TabIndex = 0;
            this.lblCompName.Text = "* Company Name :";
            // 
            // lblSiteName
            // 
            this.lblSiteName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSiteName.AutoSize = true;
            this.lblSiteName.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.lblSiteName.Location = new System.Drawing.Point(3, 104);
            this.lblSiteName.Name = "lblSiteName";
            this.lblSiteName.Size = new System.Drawing.Size(154, 13);
            this.lblSiteName.TabIndex = 4;
            this.lblSiteName.Text = "* Site Name :";
            // 
            // cmbSiteName
            // 
            this.cmbSiteName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbSiteName.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbSiteName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSiteName.Font = new System.Drawing.Font("Verdana", 8F);
            this.cmbSiteName.FormattingEnabled = true;
            this.cmbSiteName.Location = new System.Drawing.Point(163, 100);
            this.cmbSiteName.Name = "cmbSiteName";
            this.cmbSiteName.Size = new System.Drawing.Size(394, 21);
            this.cmbSiteName.TabIndex = 5;
            // 
            // lblSubCompName
            // 
            this.lblSubCompName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSubCompName.AutoSize = true;
            this.lblSubCompName.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.lblSubCompName.Location = new System.Drawing.Point(3, 59);
            this.lblSubCompName.Name = "lblSubCompName";
            this.lblSubCompName.Size = new System.Drawing.Size(154, 13);
            this.lblSubCompName.TabIndex = 2;
            this.lblSubCompName.Text = "* Sub Company Name :";
            // 
            // cmbCompName
            // 
            this.cmbCompName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbCompName.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbCompName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCompName.Font = new System.Drawing.Font("Verdana", 8F);
            this.cmbCompName.FormattingEnabled = true;
            this.cmbCompName.Location = new System.Drawing.Point(163, 11);
            this.cmbCompName.Name = "cmbCompName";
            this.cmbCompName.Size = new System.Drawing.Size(394, 21);
            this.cmbCompName.TabIndex = 1;
            this.cmbCompName.SelectedIndexChanged += new System.EventHandler(this.cmbCompName_SelectedIndexChanged);
            // 
            // cmbSubCompName
            // 
            this.cmbSubCompName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbSubCompName.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbSubCompName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSubCompName.Font = new System.Drawing.Font("Verdana", 8F);
            this.cmbSubCompName.FormattingEnabled = true;
            this.cmbSubCompName.Location = new System.Drawing.Point(163, 55);
            this.cmbSubCompName.Name = "cmbSubCompName";
            this.cmbSubCompName.Size = new System.Drawing.Size(394, 21);
            this.cmbSubCompName.TabIndex = 3;
            this.cmbSubCompName.SelectedIndexChanged += new System.EventHandler(this.cmbSubCompName_SelectedIndexChanged);
            // 
            // grpgenLicense
            // 
            this.grpgenLicense.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.grpgenLicense.Controls.Add(this.txtLicenseKey);
            this.grpgenLicense.Controls.Add(this.lblLicenseKey);
            this.grpgenLicense.Controls.Add(this.btnGenerate);
            this.grpgenLicense.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold);
            this.grpgenLicense.Location = new System.Drawing.Point(3, 508);
            this.grpgenLicense.Name = "grpgenLicense";
            this.grpgenLicense.Size = new System.Drawing.Size(1111, 51);
            this.grpgenLicense.TabIndex = 1;
            this.grpgenLicense.TabStop = false;
            this.grpgenLicense.Text = "Generate License";
            // 
            // txtLicenseKey
            // 
            this.txtLicenseKey.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtLicenseKey.Font = new System.Drawing.Font("Verdana", 8F);
            this.txtLicenseKey.Location = new System.Drawing.Point(138, 21);
            this.txtLicenseKey.Name = "txtLicenseKey";
            this.txtLicenseKey.ReadOnly = true;
            this.txtLicenseKey.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtLicenseKey.Size = new System.Drawing.Size(298, 20);
            this.txtLicenseKey.TabIndex = 1;
            this.txtLicenseKey.TextChanged += new System.EventHandler(this.txtLicenseKey_TextChanged);
            // 
            // lblLicenseKey
            // 
            this.lblLicenseKey.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblLicenseKey.AutoSize = true;
            this.lblLicenseKey.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.lblLicenseKey.Location = new System.Drawing.Point(12, 25);
            this.lblLicenseKey.Name = "lblLicenseKey";
            this.lblLicenseKey.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblLicenseKey.Size = new System.Drawing.Size(95, 13);
            this.lblLicenseKey.TabIndex = 1;
            this.lblLicenseKey.Text = "* License Key :";
            // 
            // btnGenerate
            // 
            this.btnGenerate.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnGenerate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.btnGenerate.Location = new System.Drawing.Point(466, 17);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnGenerate.Size = new System.Drawing.Size(100, 28);
            this.btnGenerate.TabIndex = 2;
            this.btnGenerate.Text = "&Generate";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // tableLayoutPanel8
            // 
            this.tableLayoutPanel8.ColumnCount = 2;
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel8.Controls.Add(this.grpLicenseSettings, 0, 0);
            this.tableLayoutPanel8.Controls.Add(this.groupBox2, 1, 0);
            this.tableLayoutPanel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel8.Location = new System.Drawing.Point(3, 165);
            this.tableLayoutPanel8.Name = "tableLayoutPanel8";
            this.tableLayoutPanel8.RowCount = 1;
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel8.Size = new System.Drawing.Size(1111, 334);
            this.tableLayoutPanel8.TabIndex = 0;
            // 
            // grpLicenseSettings
            // 
            this.grpLicenseSettings.Controls.Add(this.tableLayoutPanel10);
            this.grpLicenseSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpLicenseSettings.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpLicenseSettings.Location = new System.Drawing.Point(3, 3);
            this.grpLicenseSettings.Name = "grpLicenseSettings";
            this.grpLicenseSettings.Size = new System.Drawing.Size(660, 328);
            this.grpLicenseSettings.TabIndex = 0;
            this.grpLicenseSettings.TabStop = false;
            this.grpLicenseSettings.Text = "License Settings";
            this.grpLicenseSettings.EnabledChanged += new System.EventHandler(this.grpLicenseSettings_EnabledChanged);
            // 
            // tableLayoutPanel10
            // 
            this.tableLayoutPanel10.ColumnCount = 1;
            this.tableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel10.Controls.Add(this.grpValidationParameter, 0, 1);
            this.tableLayoutPanel10.Controls.Add(this.tableLayoutPanel11, 0, 0);
            this.tableLayoutPanel10.Controls.Add(this.tableLayoutPanel12, 0, 2);
            this.tableLayoutPanel10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel10.Location = new System.Drawing.Point(3, 19);
            this.tableLayoutPanel10.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel10.Name = "tableLayoutPanel10";
            this.tableLayoutPanel10.RowCount = 3;
            this.tableLayoutPanel10.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 44F));
            this.tableLayoutPanel10.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 44F));
            this.tableLayoutPanel10.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12F));
            this.tableLayoutPanel10.Size = new System.Drawing.Size(654, 306);
            this.tableLayoutPanel10.TabIndex = 10;
            // 
            // grpValidationParameter
            // 
            this.grpValidationParameter.Controls.Add(this.tableLayoutPanel1);
            this.grpValidationParameter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpValidationParameter.Enabled = false;
            this.grpValidationParameter.Location = new System.Drawing.Point(3, 137);
            this.grpValidationParameter.Name = "grpValidationParameter";
            this.grpValidationParameter.Padding = new System.Windows.Forms.Padding(0);
            this.grpValidationParameter.Size = new System.Drawing.Size(648, 128);
            this.grpValidationParameter.TabIndex = 1;
            this.grpValidationParameter.TabStop = false;
            this.grpValidationParameter.Text = "Validation Parameters";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 43.93939F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 56.06061F));
            this.tableLayoutPanel1.Controls.Add(this.chkLSValidationReq, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.chkLSAlertReq, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.chkLSWarningOnly, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.chkLSDisableEGMs, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.chkLSLockSite, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 16);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.Padding = new System.Windows.Forms.Padding(3);
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(648, 112);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // chkLSValidationReq
            // 
            this.chkLSValidationReq.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.chkLSValidationReq.AutoCheck = false;
            this.chkLSValidationReq.AutoSize = true;
            this.chkLSValidationReq.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.chkLSValidationReq.Location = new System.Drawing.Point(6, 12);
            this.chkLSValidationReq.Name = "chkLSValidationReq";
            this.chkLSValidationReq.Size = new System.Drawing.Size(276, 17);
            this.chkLSValidationReq.TabIndex = 1;
            this.chkLSValidationReq.Text = "Validation Required";
            this.chkLSValidationReq.UseVisualStyleBackColor = true;
            // 
            // chkLSAlertReq
            // 
            this.chkLSAlertReq.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.chkLSAlertReq.AutoCheck = false;
            this.chkLSAlertReq.AutoSize = true;
            this.chkLSAlertReq.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.chkLSAlertReq.Location = new System.Drawing.Point(288, 82);
            this.chkLSAlertReq.Name = "chkLSAlertReq";
            this.chkLSAlertReq.Size = new System.Drawing.Size(354, 17);
            this.chkLSAlertReq.TabIndex = 0;
            this.chkLSAlertReq.Text = "Alert Required";
            this.chkLSAlertReq.UseVisualStyleBackColor = true;
            this.chkLSAlertReq.CheckedChanged += new System.EventHandler(this.chkLSAlertReq_CheckedChanged);
            // 
            // chkLSWarningOnly
            // 
            this.chkLSWarningOnly.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.chkLSWarningOnly.AutoCheck = false;
            this.chkLSWarningOnly.AutoSize = true;
            this.chkLSWarningOnly.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.chkLSWarningOnly.Location = new System.Drawing.Point(6, 82);
            this.chkLSWarningOnly.Name = "chkLSWarningOnly";
            this.chkLSWarningOnly.Size = new System.Drawing.Size(276, 17);
            this.chkLSWarningOnly.TabIndex = 4;
            this.chkLSWarningOnly.Text = "Warning Only";
            this.chkLSWarningOnly.UseVisualStyleBackColor = true;
            // 
            // chkLSDisableEGMs
            // 
            this.chkLSDisableEGMs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.chkLSDisableEGMs.AutoCheck = false;
            this.chkLSDisableEGMs.AutoSize = true;
            this.chkLSDisableEGMs.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.chkLSDisableEGMs.Location = new System.Drawing.Point(288, 47);
            this.chkLSDisableEGMs.Name = "chkLSDisableEGMs";
            this.chkLSDisableEGMs.Size = new System.Drawing.Size(354, 17);
            this.chkLSDisableEGMs.TabIndex = 3;
            this.chkLSDisableEGMs.Text = "Disable EGMs";
            this.chkLSDisableEGMs.UseVisualStyleBackColor = true;
            // 
            // chkLSLockSite
            // 
            this.chkLSLockSite.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.chkLSLockSite.AutoCheck = false;
            this.chkLSLockSite.AutoSize = true;
            this.chkLSLockSite.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.chkLSLockSite.Location = new System.Drawing.Point(6, 47);
            this.chkLSLockSite.Name = "chkLSLockSite";
            this.chkLSLockSite.Size = new System.Drawing.Size(276, 17);
            this.chkLSLockSite.TabIndex = 2;
            this.chkLSLockSite.Text = "Lock Site";
            this.chkLSLockSite.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel11
            // 
            this.tableLayoutPanel11.ColumnCount = 3;
            this.tableLayoutPanel11.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 160F));
            this.tableLayoutPanel11.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tableLayoutPanel11.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel11.Controls.Add(this.lblStartDate, 0, 0);
            this.tableLayoutPanel11.Controls.Add(this.lblExpiryDate, 0, 1);
            this.tableLayoutPanel11.Controls.Add(this.lblLSRuleName, 0, 2);
            this.tableLayoutPanel11.Controls.Add(this.dtpkStartDate, 1, 0);
            this.tableLayoutPanel11.Controls.Add(this.dtpkExpiryDate, 1, 1);
            this.tableLayoutPanel11.Controls.Add(this.cmbLSRuleName, 1, 2);
            this.tableLayoutPanel11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel11.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel11.Name = "tableLayoutPanel11";
            this.tableLayoutPanel11.RowCount = 3;
            this.tableLayoutPanel11.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33F));
            this.tableLayoutPanel11.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33F));
            this.tableLayoutPanel11.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 34F));
            this.tableLayoutPanel11.Size = new System.Drawing.Size(648, 128);
            this.tableLayoutPanel11.TabIndex = 2;
            // 
            // lblStartDate
            // 
            this.lblStartDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStartDate.AutoSize = true;
            this.lblStartDate.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.lblStartDate.Location = new System.Drawing.Point(3, 14);
            this.lblStartDate.Name = "lblStartDate";
            this.lblStartDate.Size = new System.Drawing.Size(154, 13);
            this.lblStartDate.TabIndex = 0;
            this.lblStartDate.Text = "* Start Date :";
            // 
            // lblExpiryDate
            // 
            this.lblExpiryDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblExpiryDate.AutoSize = true;
            this.lblExpiryDate.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.lblExpiryDate.Location = new System.Drawing.Point(3, 56);
            this.lblExpiryDate.Name = "lblExpiryDate";
            this.lblExpiryDate.Size = new System.Drawing.Size(154, 13);
            this.lblExpiryDate.TabIndex = 2;
            this.lblExpiryDate.Text = "* Expiry Date :";
            // 
            // lblLSRuleName
            // 
            this.lblLSRuleName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblLSRuleName.AutoSize = true;
            this.lblLSRuleName.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.lblLSRuleName.Location = new System.Drawing.Point(3, 99);
            this.lblLSRuleName.Name = "lblLSRuleName";
            this.lblLSRuleName.Size = new System.Drawing.Size(154, 13);
            this.lblLSRuleName.TabIndex = 4;
            this.lblLSRuleName.Text = "* Rule Name :";
            // 
            // dtpkStartDate
            // 
            this.dtpkStartDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpkStartDate.Font = new System.Drawing.Font("Verdana", 8F);
            this.dtpkStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpkStartDate.Location = new System.Drawing.Point(163, 11);
            this.dtpkStartDate.MinDate = new System.DateTime(2012, 3, 16, 0, 0, 0, 0);
            this.dtpkStartDate.Name = "dtpkStartDate";
            this.dtpkStartDate.Size = new System.Drawing.Size(194, 20);
            this.dtpkStartDate.TabIndex = 1;
            this.dtpkStartDate.ValueChanged += new System.EventHandler(this.dtpkStartDate_ValueChanged);
            // 
            // dtpkExpiryDate
            // 
            this.dtpkExpiryDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpkExpiryDate.Font = new System.Drawing.Font("Verdana", 8F);
            this.dtpkExpiryDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpkExpiryDate.Location = new System.Drawing.Point(163, 53);
            this.dtpkExpiryDate.MinDate = new System.DateTime(2012, 3, 16, 0, 0, 0, 0);
            this.dtpkExpiryDate.Name = "dtpkExpiryDate";
            this.dtpkExpiryDate.Size = new System.Drawing.Size(194, 20);
            this.dtpkExpiryDate.TabIndex = 3;
            this.dtpkExpiryDate.ValueChanged += new System.EventHandler(this.dtpkExpiryDate_ValueChanged);
            // 
            // cmbLSRuleName
            // 
            this.cmbLSRuleName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbLSRuleName.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbLSRuleName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLSRuleName.Font = new System.Drawing.Font("Verdana", 8F);
            this.cmbLSRuleName.FormattingEnabled = true;
            this.cmbLSRuleName.Location = new System.Drawing.Point(163, 95);
            this.cmbLSRuleName.Name = "cmbLSRuleName";
            this.cmbLSRuleName.Size = new System.Drawing.Size(194, 21);
            this.cmbLSRuleName.TabIndex = 5;
            this.cmbLSRuleName.SelectedIndexChanged += new System.EventHandler(this.cmbLSRuleName_SelectedIndexChanged);
            // 
            // tableLayoutPanel12
            // 
            this.tableLayoutPanel12.ColumnCount = 3;
            this.tableLayoutPanel12.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel12.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 19.43463F));
            this.tableLayoutPanel12.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80.56537F));
            this.tableLayoutPanel12.Controls.Add(this.lblAlertBefor, 0, 0);
            this.tableLayoutPanel12.Controls.Add(this.lblinDays, 2, 0);
            this.tableLayoutPanel12.Controls.Add(this.numUDAlertBefor, 1, 0);
            this.tableLayoutPanel12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel12.Location = new System.Drawing.Point(3, 271);
            this.tableLayoutPanel12.Name = "tableLayoutPanel12";
            this.tableLayoutPanel12.RowCount = 1;
            this.tableLayoutPanel12.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel12.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel12.Size = new System.Drawing.Size(648, 32);
            this.tableLayoutPanel12.TabIndex = 2;
            // 
            // lblAlertBefor
            // 
            this.lblAlertBefor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblAlertBefor.AutoSize = true;
            this.lblAlertBefor.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.lblAlertBefor.Location = new System.Drawing.Point(3, 9);
            this.lblAlertBefor.Name = "lblAlertBefor";
            this.lblAlertBefor.Size = new System.Drawing.Size(114, 13);
            this.lblAlertBefor.TabIndex = 0;
            this.lblAlertBefor.Text = "Alert Before :";
            // 
            // lblinDays
            // 
            this.lblinDays.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblinDays.AutoSize = true;
            this.lblinDays.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.lblinDays.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.lblinDays.Location = new System.Drawing.Point(225, 9);
            this.lblinDays.Name = "lblinDays";
            this.lblinDays.Size = new System.Drawing.Size(420, 13);
            this.lblinDays.TabIndex = 2;
            this.lblinDays.Text = "(in days)";
            // 
            // numUDAlertBefor
            // 
            this.numUDAlertBefor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.numUDAlertBefor.Enabled = false;
            this.numUDAlertBefor.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numUDAlertBefor.Location = new System.Drawing.Point(123, 5);
            this.numUDAlertBefor.Name = "numUDAlertBefor";
            this.numUDAlertBefor.Size = new System.Drawing.Size(96, 21);
            this.numUDAlertBefor.TabIndex = 1;
            this.numUDAlertBefor.EnabledChanged += new System.EventHandler(this.numUDAlertBefor_EnabledChanged);
            this.numUDAlertBefor.KeyDown += new System.Windows.Forms.KeyEventHandler(this.numUDAlertBefor_KeyDown);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dtgvAssociatedSites);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold);
            this.groupBox2.Location = new System.Drawing.Point(669, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(439, 328);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Rule Associated Sites";
            // 
            // dtgvAssociatedSites
            // 
            this.dtgvAssociatedSites.AllowUserToAddRows = false;
            this.dtgvAssociatedSites.AllowUserToDeleteRows = false;
            this.dtgvAssociatedSites.AllowUserToResizeColumns = false;
            this.dtgvAssociatedSites.AllowUserToResizeRows = false;
            this.dtgvAssociatedSites.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dtgvAssociatedSites.DefaultCellStyle = dataGridViewCellStyle1;
            this.dtgvAssociatedSites.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtgvAssociatedSites.Location = new System.Drawing.Point(3, 19);
            this.dtgvAssociatedSites.MultiSelect = false;
            this.dtgvAssociatedSites.Name = "dtgvAssociatedSites";
            this.dtgvAssociatedSites.ReadOnly = true;
            this.dtgvAssociatedSites.RowHeadersVisible = false;
            this.dtgvAssociatedSites.ShowEditingIcon = false;
            this.dtgvAssociatedSites.Size = new System.Drawing.Size(433, 306);
            this.dtgvAssociatedSites.TabIndex = 0;
            // 
            // pnlKeyGeneration
            // 
            this.pnlKeyGeneration.Location = new System.Drawing.Point(207, 6);
            this.pnlKeyGeneration.Name = "pnlKeyGeneration";
            this.pnlKeyGeneration.Size = new System.Drawing.Size(323, 503);
            this.pnlKeyGeneration.TabIndex = 0;
            this.pnlKeyGeneration.Visible = false;
            // 
            // tabSiteLicensing
            // 
            this.tabSiteLicensing.Controls.Add(this.tabPgLicenseGen);
            this.tabSiteLicensing.Controls.Add(this.tabPgViewCancelLicense);
            this.tabSiteLicensing.Controls.Add(this.tabPgLicenseHistory);
            this.tabSiteLicensing.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabSiteLicensing.Location = new System.Drawing.Point(3, 3);
            this.tabSiteLicensing.Name = "tabSiteLicensing";
            this.tabSiteLicensing.SelectedIndex = 0;
            this.tabSiteLicensing.Size = new System.Drawing.Size(1145, 630);
            this.tabSiteLicensing.TabIndex = 0;
            this.tabSiteLicensing.SelectedIndexChanged += new System.EventHandler(this.tabSiteLicensing_SelectedIndexChanged);
            // 
            // tabPgViewCancelLicense
            // 
            this.tabPgViewCancelLicense.Controls.Add(this.sC_ViewCancelLicense);
            this.tabPgViewCancelLicense.Location = new System.Drawing.Point(4, 22);
            this.tabPgViewCancelLicense.Name = "tabPgViewCancelLicense";
            this.tabPgViewCancelLicense.Size = new System.Drawing.Size(1137, 604);
            this.tabPgViewCancelLicense.TabIndex = 3;
            this.tabPgViewCancelLicense.Text = "View/Cancel/Activate License";
            this.tabPgViewCancelLicense.UseVisualStyleBackColor = true;
            // 
            // sC_ViewCancelLicense
            // 
            this.sC_ViewCancelLicense.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sC_ViewCancelLicense.Location = new System.Drawing.Point(0, 0);
            this.sC_ViewCancelLicense.Name = "sC_ViewCancelLicense";
            // 
            // sC_ViewCancelLicense.Panel1
            // 
            this.sC_ViewCancelLicense.Panel1.Controls.Add(this.grpSiteList);
            // 
            // sC_ViewCancelLicense.Panel2
            // 
            this.sC_ViewCancelLicense.Panel2.Controls.Add(this.pnlLicenseDetails);
            this.sC_ViewCancelLicense.Panel2.Resize += new System.EventHandler(this.sC_ViewCancelLicense_Panel2_Resize);
            this.sC_ViewCancelLicense.Size = new System.Drawing.Size(1137, 604);
            this.sC_ViewCancelLicense.SplitterDistance = 295;
            this.sC_ViewCancelLicense.TabIndex = 61;
            // 
            // grpSiteList
            // 
            this.grpSiteList.Controls.Add(this.tvSiteList);
            this.grpSiteList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpSiteList.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold);
            this.grpSiteList.Location = new System.Drawing.Point(0, 0);
            this.grpSiteList.Name = "grpSiteList";
            this.grpSiteList.Size = new System.Drawing.Size(295, 604);
            this.grpSiteList.TabIndex = 0;
            this.grpSiteList.TabStop = false;
            this.grpSiteList.Text = "Site List";
            // 
            // tvSiteList
            // 
            this.tvSiteList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvSiteList.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tvSiteList.HideSelection = false;
            this.tvSiteList.ImageIndex = 0;
            this.tvSiteList.ImageList = this.imgltSiteStatus;
            this.tvSiteList.Location = new System.Drawing.Point(3, 19);
            this.tvSiteList.Name = "tvSiteList";
            this.tvSiteList.SelectedImageIndex = 0;
            this.tvSiteList.Size = new System.Drawing.Size(289, 582);
            this.tvSiteList.TabIndex = 0;
            this.tvSiteList.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvSiteList_AfterSelect);
            this.tvSiteList.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvSiteList_NodeMouseClick);
            // 
            // pnlLicenseDetails
            // 
            this.pnlLicenseDetails.Controls.Add(this.tableLayoutPanel9);
            this.pnlLicenseDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlLicenseDetails.Location = new System.Drawing.Point(0, 0);
            this.pnlLicenseDetails.Name = "pnlLicenseDetails";
            this.pnlLicenseDetails.Size = new System.Drawing.Size(838, 604);
            this.pnlLicenseDetails.TabIndex = 7;
            // 
            // tableLayoutPanel9
            // 
            this.tableLayoutPanel9.ColumnCount = 1;
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel9.Controls.Add(this.grpLicenseDetails, 0, 0);
            this.tableLayoutPanel9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel9.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel9.Name = "tableLayoutPanel9";
            this.tableLayoutPanel9.RowCount = 1;
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 604F));
            this.tableLayoutPanel9.Size = new System.Drawing.Size(838, 604);
            this.tableLayoutPanel9.TabIndex = 1;
            // 
            // grpLicenseDetails
            // 
            this.grpLicenseDetails.Controls.Add(this.dtgvLicenseDetails);
            this.grpLicenseDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpLicenseDetails.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpLicenseDetails.Location = new System.Drawing.Point(3, 3);
            this.grpLicenseDetails.Name = "grpLicenseDetails";
            this.grpLicenseDetails.Size = new System.Drawing.Size(832, 598);
            this.grpLicenseDetails.TabIndex = 0;
            this.grpLicenseDetails.TabStop = false;
            this.grpLicenseDetails.Text = "License Details";
            // 
            // dtgvLicenseDetails
            // 
            this.dtgvLicenseDetails.AllowUserToAddRows = false;
            this.dtgvLicenseDetails.AllowUserToDeleteRows = false;
            this.dtgvLicenseDetails.AllowUserToResizeRows = false;
            this.dtgvLicenseDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgvLicenseDetails.ContextMenuStrip = this.contextMenuStrip2;
            this.dtgvLicenseDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtgvLicenseDetails.Location = new System.Drawing.Point(3, 19);
            this.dtgvLicenseDetails.MultiSelect = false;
            this.dtgvLicenseDetails.Name = "dtgvLicenseDetails";
            this.dtgvLicenseDetails.ReadOnly = true;
            this.dtgvLicenseDetails.RowHeadersVisible = false;
            this.dtgvLicenseDetails.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtgvLicenseDetails.Size = new System.Drawing.Size(826, 576);
            this.dtgvLicenseDetails.TabIndex = 0;
            this.dtgvLicenseDetails.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtgvLicenseDetails_RowEnter);
            this.dtgvLicenseDetails.SelectionChanged += new System.EventHandler(this.dtgvLicenseDetails_SelectionChanged);
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cancelLicenseToolStripMenuItem,
            this.exportToolStripMenuItem,
            this.activateLicenseToolStripMenuItem});
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(160, 70);
            // 
            // cancelLicenseToolStripMenuItem
            // 
            this.cancelLicenseToolStripMenuItem.Name = "cancelLicenseToolStripMenuItem";
            this.cancelLicenseToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.cancelLicenseToolStripMenuItem.Text = "Ca&ncel License";
            this.cancelLicenseToolStripMenuItem.Click += new System.EventHandler(this.btnCancelLicense_Click);
            // 
            // exportToolStripMenuItem
            // 
            this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            this.exportToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.exportToolStripMenuItem.Text = "E&xport";
            this.exportToolStripMenuItem.Click += new System.EventHandler(this.exportToolStripMenuItem_Click);
            // 
            // activateLicenseToolStripMenuItem
            // 
            this.activateLicenseToolStripMenuItem.Name = "activateLicenseToolStripMenuItem";
            this.activateLicenseToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.activateLicenseToolStripMenuItem.Text = "&Activate License";
            this.activateLicenseToolStripMenuItem.Click += new System.EventHandler(this.btnActivateLicense_Click);
            // 
            // tabPgLicenseHistory
            // 
            this.tabPgLicenseHistory.Controls.Add(this.tableLayoutPanel4);
            this.tabPgLicenseHistory.Location = new System.Drawing.Point(4, 22);
            this.tabPgLicenseHistory.Name = "tabPgLicenseHistory";
            this.tabPgLicenseHistory.Padding = new System.Windows.Forms.Padding(3);
            this.tabPgLicenseHistory.Size = new System.Drawing.Size(1137, 604);
            this.tabPgLicenseHistory.TabIndex = 2;
            this.tabPgLicenseHistory.Text = "License History";
            this.tabPgLicenseHistory.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 1;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Controls.Add(this.grpSearchCriteria, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.grpSearchResults, 0, 1);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 2;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40.18865F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 59.81135F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(1131, 598);
            this.tableLayoutPanel4.TabIndex = 5;
            // 
            // grpSearchCriteria
            // 
            this.grpSearchCriteria.Controls.Add(this.tableLayoutPanel7);
            this.grpSearchCriteria.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpSearchCriteria.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold);
            this.grpSearchCriteria.Location = new System.Drawing.Point(3, 3);
            this.grpSearchCriteria.Name = "grpSearchCriteria";
            this.grpSearchCriteria.Size = new System.Drawing.Size(1125, 234);
            this.grpSearchCriteria.TabIndex = 0;
            this.grpSearchCriteria.TabStop = false;
            this.grpSearchCriteria.Text = "Search Criteria";
            // 
            // tableLayoutPanel7
            // 
            this.tableLayoutPanel7.ColumnCount = 1;
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel7.Controls.Add(this.tbllpSearchcriteria, 0, 0);
            this.tableLayoutPanel7.Controls.Add(this.tableLayoutPanel5, 0, 1);
            this.tableLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel7.Location = new System.Drawing.Point(3, 19);
            this.tableLayoutPanel7.Name = "tableLayoutPanel7";
            this.tableLayoutPanel7.RowCount = 2;
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel7.Size = new System.Drawing.Size(1119, 212);
            this.tableLayoutPanel7.TabIndex = 2;
            // 
            // tbllpSearchcriteria
            // 
            this.tbllpSearchcriteria.ColumnCount = 4;
            this.tbllpSearchcriteria.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tbllpSearchcriteria.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 55.31251F));
            this.tbllpSearchcriteria.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 191F));
            this.tbllpSearchcriteria.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 44.6875F));
            this.tbllpSearchcriteria.Controls.Add(this.dtpkToExpiryDate, 3, 3);
            this.tbllpSearchcriteria.Controls.Add(this.cmbSCCompanyName, 1, 0);
            this.tbllpSearchcriteria.Controls.Add(this.cmbSCSubCompanyName, 1, 1);
            this.tbllpSearchcriteria.Controls.Add(this.lblSCSubCompName, 0, 1);
            this.tbllpSearchcriteria.Controls.Add(this.lblFromExpiryDate, 2, 2);
            this.tbllpSearchcriteria.Controls.Add(this.dtpkFromExpiryDate, 3, 2);
            this.tbllpSearchcriteria.Controls.Add(this.lblToExpiryDate, 2, 3);
            this.tbllpSearchcriteria.Controls.Add(this.lblfromStartDate, 2, 0);
            this.tbllpSearchcriteria.Controls.Add(this.lblSCSiteName, 0, 2);
            this.tbllpSearchcriteria.Controls.Add(this.dtpkFromStartDate, 3, 0);
            this.tbllpSearchcriteria.Controls.Add(this.cmbSCSiteName, 1, 2);
            this.tbllpSearchcriteria.Controls.Add(this.lblToStartDate, 2, 1);
            this.tbllpSearchcriteria.Controls.Add(this.lblSCKeyStatus, 0, 3);
            this.tbllpSearchcriteria.Controls.Add(this.dtpkToStartDate, 3, 1);
            this.tbllpSearchcriteria.Controls.Add(this.cmbSCKeyStatus, 1, 3);
            this.tbllpSearchcriteria.Controls.Add(this.lblSCCompName, 0, 0);
            this.tbllpSearchcriteria.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbllpSearchcriteria.Location = new System.Drawing.Point(3, 3);
            this.tbllpSearchcriteria.Name = "tbllpSearchcriteria";
            this.tbllpSearchcriteria.RowCount = 4;
            this.tbllpSearchcriteria.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tbllpSearchcriteria.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tbllpSearchcriteria.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tbllpSearchcriteria.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tbllpSearchcriteria.Size = new System.Drawing.Size(1113, 121);
            this.tbllpSearchcriteria.TabIndex = 0;
            // 
            // dtpkToExpiryDate
            // 
            this.dtpkToExpiryDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpkToExpiryDate.Checked = false;
            this.dtpkToExpiryDate.Font = new System.Drawing.Font("Verdana", 8F);
            this.dtpkToExpiryDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpkToExpiryDate.Location = new System.Drawing.Point(771, 95);
            this.dtpkToExpiryDate.Name = "dtpkToExpiryDate";
            this.dtpkToExpiryDate.Size = new System.Drawing.Size(339, 20);
            this.dtpkToExpiryDate.TabIndex = 15;
            // 
            // cmbSCCompanyName
            // 
            this.cmbSCCompanyName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbSCCompanyName.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbSCCompanyName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSCCompanyName.Font = new System.Drawing.Font("Verdana", 8F);
            this.cmbSCCompanyName.FormattingEnabled = true;
            this.cmbSCCompanyName.Location = new System.Drawing.Point(153, 4);
            this.cmbSCCompanyName.Name = "cmbSCCompanyName";
            this.cmbSCCompanyName.Size = new System.Drawing.Size(421, 21);
            this.cmbSCCompanyName.TabIndex = 1;
            this.cmbSCCompanyName.SelectedIndexChanged += new System.EventHandler(this.cmbSCCompanyName_SelectedIndexChanged);
            // 
            // cmbSCSubCompanyName
            // 
            this.cmbSCSubCompanyName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbSCSubCompanyName.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbSCSubCompanyName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSCSubCompanyName.Font = new System.Drawing.Font("Verdana", 8F);
            this.cmbSCSubCompanyName.FormattingEnabled = true;
            this.cmbSCSubCompanyName.Location = new System.Drawing.Point(153, 34);
            this.cmbSCSubCompanyName.Name = "cmbSCSubCompanyName";
            this.cmbSCSubCompanyName.Size = new System.Drawing.Size(421, 21);
            this.cmbSCSubCompanyName.TabIndex = 3;
            this.cmbSCSubCompanyName.SelectedIndexChanged += new System.EventHandler(this.cmbSCSubCompanyName_SelectedIndexChanged);
            // 
            // lblSCSubCompName
            // 
            this.lblSCSubCompName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSCSubCompName.AutoSize = true;
            this.lblSCSubCompName.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSCSubCompName.Location = new System.Drawing.Point(3, 38);
            this.lblSCSubCompName.Name = "lblSCSubCompName";
            this.lblSCSubCompName.Size = new System.Drawing.Size(144, 13);
            this.lblSCSubCompName.TabIndex = 2;
            this.lblSCSubCompName.Text = "Sub Company Name :";
            // 
            // lblFromExpiryDate
            // 
            this.lblFromExpiryDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFromExpiryDate.AutoSize = true;
            this.lblFromExpiryDate.Font = new System.Drawing.Font("Verdana", 8F);
            this.lblFromExpiryDate.Location = new System.Drawing.Point(580, 68);
            this.lblFromExpiryDate.Name = "lblFromExpiryDate";
            this.lblFromExpiryDate.Size = new System.Drawing.Size(185, 13);
            this.lblFromExpiryDate.TabIndex = 12;
            this.lblFromExpiryDate.Text = "From (License Expired Date) :";
            // 
            // dtpkFromExpiryDate
            // 
            this.dtpkFromExpiryDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpkFromExpiryDate.Checked = false;
            this.dtpkFromExpiryDate.Font = new System.Drawing.Font("Verdana", 8F);
            this.dtpkFromExpiryDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpkFromExpiryDate.Location = new System.Drawing.Point(771, 65);
            this.dtpkFromExpiryDate.Name = "dtpkFromExpiryDate";
            this.dtpkFromExpiryDate.Size = new System.Drawing.Size(339, 20);
            this.dtpkFromExpiryDate.TabIndex = 13;
            this.dtpkFromExpiryDate.ValueChanged += new System.EventHandler(this.dtpkFromExpiryDate_ValueChanged);
            // 
            // lblToExpiryDate
            // 
            this.lblToExpiryDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblToExpiryDate.AutoSize = true;
            this.lblToExpiryDate.Font = new System.Drawing.Font("Verdana", 8F);
            this.lblToExpiryDate.Location = new System.Drawing.Point(580, 99);
            this.lblToExpiryDate.Name = "lblToExpiryDate";
            this.lblToExpiryDate.Size = new System.Drawing.Size(185, 13);
            this.lblToExpiryDate.TabIndex = 14;
            this.lblToExpiryDate.Text = "To (License Expired Date) :";
            // 
            // lblfromStartDate
            // 
            this.lblfromStartDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblfromStartDate.AutoSize = true;
            this.lblfromStartDate.Font = new System.Drawing.Font("Verdana", 8F);
            this.lblfromStartDate.Location = new System.Drawing.Point(580, 8);
            this.lblfromStartDate.Name = "lblfromStartDate";
            this.lblfromStartDate.Size = new System.Drawing.Size(185, 13);
            this.lblfromStartDate.TabIndex = 8;
            this.lblfromStartDate.Text = "From (License Start Date) :";
            // 
            // lblSCSiteName
            // 
            this.lblSCSiteName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSCSiteName.AutoSize = true;
            this.lblSCSiteName.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSCSiteName.Location = new System.Drawing.Point(3, 68);
            this.lblSCSiteName.Name = "lblSCSiteName";
            this.lblSCSiteName.Size = new System.Drawing.Size(144, 13);
            this.lblSCSiteName.TabIndex = 4;
            this.lblSCSiteName.Text = "Site Name :";
            // 
            // dtpkFromStartDate
            // 
            this.dtpkFromStartDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpkFromStartDate.Checked = false;
            this.dtpkFromStartDate.Font = new System.Drawing.Font("Verdana", 8F);
            this.dtpkFromStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpkFromStartDate.Location = new System.Drawing.Point(771, 5);
            this.dtpkFromStartDate.Name = "dtpkFromStartDate";
            this.dtpkFromStartDate.Size = new System.Drawing.Size(339, 20);
            this.dtpkFromStartDate.TabIndex = 9;
            this.dtpkFromStartDate.ValueChanged += new System.EventHandler(this.dtpkFromStartDate_ValueChanged);
            // 
            // cmbSCSiteName
            // 
            this.cmbSCSiteName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbSCSiteName.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbSCSiteName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSCSiteName.Font = new System.Drawing.Font("Verdana", 8F);
            this.cmbSCSiteName.FormattingEnabled = true;
            this.cmbSCSiteName.Location = new System.Drawing.Point(153, 64);
            this.cmbSCSiteName.Name = "cmbSCSiteName";
            this.cmbSCSiteName.Size = new System.Drawing.Size(421, 21);
            this.cmbSCSiteName.TabIndex = 5;
            // 
            // lblToStartDate
            // 
            this.lblToStartDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblToStartDate.AutoSize = true;
            this.lblToStartDate.Font = new System.Drawing.Font("Verdana", 8F);
            this.lblToStartDate.Location = new System.Drawing.Point(580, 38);
            this.lblToStartDate.Name = "lblToStartDate";
            this.lblToStartDate.Size = new System.Drawing.Size(185, 13);
            this.lblToStartDate.TabIndex = 10;
            this.lblToStartDate.Text = "To (License Start Date) :";
            // 
            // lblSCKeyStatus
            // 
            this.lblSCKeyStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSCKeyStatus.AutoSize = true;
            this.lblSCKeyStatus.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSCKeyStatus.Location = new System.Drawing.Point(3, 99);
            this.lblSCKeyStatus.Name = "lblSCKeyStatus";
            this.lblSCKeyStatus.Size = new System.Drawing.Size(144, 13);
            this.lblSCKeyStatus.TabIndex = 6;
            this.lblSCKeyStatus.Text = "Key Status :";
            // 
            // dtpkToStartDate
            // 
            this.dtpkToStartDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpkToStartDate.Checked = false;
            this.dtpkToStartDate.Font = new System.Drawing.Font("Verdana", 8F);
            this.dtpkToStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpkToStartDate.Location = new System.Drawing.Point(771, 35);
            this.dtpkToStartDate.Name = "dtpkToStartDate";
            this.dtpkToStartDate.Size = new System.Drawing.Size(339, 20);
            this.dtpkToStartDate.TabIndex = 11;
            // 
            // cmbSCKeyStatus
            // 
            this.cmbSCKeyStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbSCKeyStatus.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbSCKeyStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSCKeyStatus.Font = new System.Drawing.Font("Verdana", 8F);
            this.cmbSCKeyStatus.FormattingEnabled = true;
            this.cmbSCKeyStatus.Location = new System.Drawing.Point(153, 95);
            this.cmbSCKeyStatus.Name = "cmbSCKeyStatus";
            this.cmbSCKeyStatus.Size = new System.Drawing.Size(421, 21);
            this.cmbSCKeyStatus.TabIndex = 7;
            // 
            // lblSCCompName
            // 
            this.lblSCCompName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSCCompName.AutoSize = true;
            this.lblSCCompName.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSCCompName.Location = new System.Drawing.Point(3, 8);
            this.lblSCCompName.Name = "lblSCCompName";
            this.lblSCCompName.Size = new System.Drawing.Size(144, 13);
            this.lblSCCompName.TabIndex = 0;
            this.lblSCCompName.Text = "Company Name :";
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 2;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.Controls.Add(this.grpSCValidationParam, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.grpUserFilter, 1, 0);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(3, 130);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 1;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 79F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(1113, 79);
            this.tableLayoutPanel5.TabIndex = 1;
            // 
            // grpSCValidationParam
            // 
            this.grpSCValidationParam.Controls.Add(this.tableLayoutPanel2);
            this.grpSCValidationParam.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpSCValidationParam.Location = new System.Drawing.Point(3, 3);
            this.grpSCValidationParam.Name = "grpSCValidationParam";
            this.grpSCValidationParam.Size = new System.Drawing.Size(550, 73);
            this.grpSCValidationParam.TabIndex = 1;
            this.grpSCValidationParam.TabStop = false;
            this.grpSCValidationParam.Text = "Validation Parameters";
            this.grpSCValidationParam.Enter += new System.EventHandler(this.grpSCValidationParam_Enter_1);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 6;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 136F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.3324F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 105F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.3338F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 113F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.3338F));
            this.tableLayoutPanel2.Controls.Add(this.cmbSCLockSite, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.lblSCLockSite, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.cmbSCValidationReq, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.lblSCValidationReq, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.lblSCDisableEGM, 4, 0);
            this.tableLayoutPanel2.Controls.Add(this.cmbSCDisableEGM, 5, 0);
            this.tableLayoutPanel2.Controls.Add(this.lblSCWarningOnly, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.cmbSCWarningOnly, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.lblAlertRequired, 2, 1);
            this.tableLayoutPanel2.Controls.Add(this.cmbSCAlertRequired, 3, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 19);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(544, 51);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // cmbSCLockSite
            // 
            this.cmbSCLockSite.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbSCLockSite.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbSCLockSite.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSCLockSite.Font = new System.Drawing.Font("Verdana", 8F);
            this.cmbSCLockSite.FormattingEnabled = true;
            this.cmbSCLockSite.Location = new System.Drawing.Point(307, 3);
            this.cmbSCLockSite.Name = "cmbSCLockSite";
            this.cmbSCLockSite.Size = new System.Drawing.Size(57, 21);
            this.cmbSCLockSite.TabIndex = 3;
            // 
            // lblSCLockSite
            // 
            this.lblSCLockSite.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSCLockSite.AutoSize = true;
            this.lblSCLockSite.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSCLockSite.Location = new System.Drawing.Point(202, 6);
            this.lblSCLockSite.Name = "lblSCLockSite";
            this.lblSCLockSite.Size = new System.Drawing.Size(99, 13);
            this.lblSCLockSite.TabIndex = 2;
            this.lblSCLockSite.Text = "Lock Site :";
            // 
            // cmbSCValidationReq
            // 
            this.cmbSCValidationReq.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbSCValidationReq.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbSCValidationReq.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSCValidationReq.Font = new System.Drawing.Font("Verdana", 8F);
            this.cmbSCValidationReq.FormattingEnabled = true;
            this.cmbSCValidationReq.Location = new System.Drawing.Point(139, 3);
            this.cmbSCValidationReq.Name = "cmbSCValidationReq";
            this.cmbSCValidationReq.Size = new System.Drawing.Size(57, 21);
            this.cmbSCValidationReq.TabIndex = 1;
            // 
            // lblSCValidationReq
            // 
            this.lblSCValidationReq.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSCValidationReq.AutoSize = true;
            this.lblSCValidationReq.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSCValidationReq.Location = new System.Drawing.Point(3, 6);
            this.lblSCValidationReq.Name = "lblSCValidationReq";
            this.lblSCValidationReq.Size = new System.Drawing.Size(130, 13);
            this.lblSCValidationReq.TabIndex = 0;
            this.lblSCValidationReq.Text = "Validation Required: ";
            // 
            // lblSCDisableEGM
            // 
            this.lblSCDisableEGM.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSCDisableEGM.AutoSize = true;
            this.lblSCDisableEGM.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSCDisableEGM.Location = new System.Drawing.Point(370, 6);
            this.lblSCDisableEGM.Name = "lblSCDisableEGM";
            this.lblSCDisableEGM.Size = new System.Drawing.Size(107, 13);
            this.lblSCDisableEGM.TabIndex = 4;
            this.lblSCDisableEGM.Text = "Disable EGM\'s :";
            // 
            // cmbSCDisableEGM
            // 
            this.cmbSCDisableEGM.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbSCDisableEGM.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbSCDisableEGM.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSCDisableEGM.Font = new System.Drawing.Font("Verdana", 8F);
            this.cmbSCDisableEGM.FormattingEnabled = true;
            this.cmbSCDisableEGM.Location = new System.Drawing.Point(483, 3);
            this.cmbSCDisableEGM.Name = "cmbSCDisableEGM";
            this.cmbSCDisableEGM.Size = new System.Drawing.Size(58, 21);
            this.cmbSCDisableEGM.TabIndex = 5;
            // 
            // lblSCWarningOnly
            // 
            this.lblSCWarningOnly.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSCWarningOnly.AutoSize = true;
            this.lblSCWarningOnly.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSCWarningOnly.Location = new System.Drawing.Point(3, 31);
            this.lblSCWarningOnly.Name = "lblSCWarningOnly";
            this.lblSCWarningOnly.Size = new System.Drawing.Size(130, 13);
            this.lblSCWarningOnly.TabIndex = 6;
            this.lblSCWarningOnly.Text = "Warning Only :";
            // 
            // cmbSCWarningOnly
            // 
            this.cmbSCWarningOnly.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbSCWarningOnly.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbSCWarningOnly.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSCWarningOnly.Font = new System.Drawing.Font("Verdana", 8F);
            this.cmbSCWarningOnly.FormattingEnabled = true;
            this.cmbSCWarningOnly.Location = new System.Drawing.Point(139, 28);
            this.cmbSCWarningOnly.Name = "cmbSCWarningOnly";
            this.cmbSCWarningOnly.Size = new System.Drawing.Size(57, 21);
            this.cmbSCWarningOnly.TabIndex = 7;
            // 
            // lblAlertRequired
            // 
            this.lblAlertRequired.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblAlertRequired.AutoSize = true;
            this.lblAlertRequired.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAlertRequired.Location = new System.Drawing.Point(202, 31);
            this.lblAlertRequired.Name = "lblAlertRequired";
            this.lblAlertRequired.Size = new System.Drawing.Size(99, 13);
            this.lblAlertRequired.TabIndex = 8;
            this.lblAlertRequired.Text = "Alert Required :";
            // 
            // cmbSCAlertRequired
            // 
            this.cmbSCAlertRequired.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbSCAlertRequired.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbSCAlertRequired.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSCAlertRequired.Font = new System.Drawing.Font("Verdana", 8F);
            this.cmbSCAlertRequired.FormattingEnabled = true;
            this.cmbSCAlertRequired.Location = new System.Drawing.Point(307, 28);
            this.cmbSCAlertRequired.Name = "cmbSCAlertRequired";
            this.cmbSCAlertRequired.Size = new System.Drawing.Size(57, 21);
            this.cmbSCAlertRequired.TabIndex = 9;
            // 
            // grpUserFilter
            // 
            this.grpUserFilter.Controls.Add(this.tbluserfilter);
            this.grpUserFilter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpUserFilter.Location = new System.Drawing.Point(559, 3);
            this.grpUserFilter.Name = "grpUserFilter";
            this.grpUserFilter.Size = new System.Drawing.Size(551, 73);
            this.grpUserFilter.TabIndex = 2;
            this.grpUserFilter.TabStop = false;
            this.grpUserFilter.Text = "User Filter";
            // 
            // tbluserfilter
            // 
            this.tbluserfilter.ColumnCount = 3;
            this.tbluserfilter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tbluserfilter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tbluserfilter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tbluserfilter.Controls.Add(this.cmbCancelBy, 2, 1);
            this.tbluserfilter.Controls.Add(this.cmbActivatedBy, 1, 1);
            this.tbluserfilter.Controls.Add(this.cmbcreateBy, 0, 1);
            this.tbluserfilter.Controls.Add(this.lblActivatedBy, 1, 0);
            this.tbluserfilter.Controls.Add(this.lblcreate, 0, 0);
            this.tbluserfilter.Controls.Add(this.lblCancelBy, 2, 0);
            this.tbluserfilter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbluserfilter.Location = new System.Drawing.Point(3, 19);
            this.tbluserfilter.Name = "tbluserfilter";
            this.tbluserfilter.RowCount = 3;
            this.tbluserfilter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tbluserfilter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tbluserfilter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tbluserfilter.Size = new System.Drawing.Size(545, 51);
            this.tbluserfilter.TabIndex = 0;
            // 
            // cmbCancelBy
            // 
            this.cmbCancelBy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbCancelBy.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbCancelBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCancelBy.DropDownWidth = 130;
            this.cmbCancelBy.Font = new System.Drawing.Font("Verdana", 8F);
            this.cmbCancelBy.FormattingEnabled = true;
            this.cmbCancelBy.Location = new System.Drawing.Point(365, 15);
            this.cmbCancelBy.Name = "cmbCancelBy";
            this.cmbCancelBy.Size = new System.Drawing.Size(177, 21);
            this.cmbCancelBy.TabIndex = 12;
            // 
            // cmbActivatedBy
            // 
            this.cmbActivatedBy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbActivatedBy.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbActivatedBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbActivatedBy.DropDownWidth = 130;
            this.cmbActivatedBy.Font = new System.Drawing.Font("Verdana", 8F);
            this.cmbActivatedBy.FormattingEnabled = true;
            this.cmbActivatedBy.Location = new System.Drawing.Point(184, 15);
            this.cmbActivatedBy.Name = "cmbActivatedBy";
            this.cmbActivatedBy.Size = new System.Drawing.Size(175, 21);
            this.cmbActivatedBy.TabIndex = 11;
            // 
            // cmbcreateBy
            // 
            this.cmbcreateBy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbcreateBy.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbcreateBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbcreateBy.DropDownWidth = 130;
            this.cmbcreateBy.Font = new System.Drawing.Font("Verdana", 8F);
            this.cmbcreateBy.FormattingEnabled = true;
            this.cmbcreateBy.Location = new System.Drawing.Point(3, 15);
            this.cmbcreateBy.Name = "cmbcreateBy";
            this.cmbcreateBy.Size = new System.Drawing.Size(175, 21);
            this.cmbcreateBy.TabIndex = 10;
            // 
            // lblActivatedBy
            // 
            this.lblActivatedBy.AutoSize = true;
            this.lblActivatedBy.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.lblActivatedBy.Location = new System.Drawing.Point(184, 0);
            this.lblActivatedBy.Name = "lblActivatedBy";
            this.lblActivatedBy.Size = new System.Drawing.Size(88, 12);
            this.lblActivatedBy.TabIndex = 1;
            this.lblActivatedBy.Text = "Activated By :";
            // 
            // lblcreate
            // 
            this.lblcreate.AutoSize = true;
            this.lblcreate.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.lblcreate.Location = new System.Drawing.Point(3, 0);
            this.lblcreate.Name = "lblcreate";
            this.lblcreate.Size = new System.Drawing.Size(81, 12);
            this.lblcreate.TabIndex = 0;
            this.lblcreate.Text = "Created By :";
            // 
            // lblCancelBy
            // 
            this.lblCancelBy.AutoSize = true;
            this.lblCancelBy.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.lblCancelBy.Location = new System.Drawing.Point(365, 0);
            this.lblCancelBy.Name = "lblCancelBy";
            this.lblCancelBy.Size = new System.Drawing.Size(91, 12);
            this.lblCancelBy.TabIndex = 2;
            this.lblCancelBy.Text = "Cancelled By :";
            // 
            // grpSearchResults
            // 
            this.grpSearchResults.Controls.Add(this.dtGVSearchResults);
            this.grpSearchResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpSearchResults.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpSearchResults.Location = new System.Drawing.Point(3, 243);
            this.grpSearchResults.Name = "grpSearchResults";
            this.grpSearchResults.Size = new System.Drawing.Size(1125, 352);
            this.grpSearchResults.TabIndex = 4;
            this.grpSearchResults.TabStop = false;
            this.grpSearchResults.Text = "Search Results";
            // 
            // dtGVSearchResults
            // 
            this.dtGVSearchResults.AllowUserToAddRows = false;
            this.dtGVSearchResults.AllowUserToDeleteRows = false;
            this.dtGVSearchResults.AllowUserToOrderColumns = true;
            this.dtGVSearchResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtGVSearchResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtGVSearchResults.Location = new System.Drawing.Point(3, 18);
            this.dtGVSearchResults.Name = "dtGVSearchResults";
            this.dtGVSearchResults.ReadOnly = true;
            this.dtGVSearchResults.RowHeadersVisible = false;
            this.dtGVSearchResults.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtGVSearchResults.Size = new System.Drawing.Size(1119, 331);
            this.dtGVSearchResults.TabIndex = 0;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.tabSiteLicensing, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel14, 0, 1);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(1151, 681);
            this.tableLayoutPanel3.TabIndex = 0;
            // 
            // tableLayoutPanel14
            // 
            this.tableLayoutPanel14.ColumnCount = 2;
            this.tableLayoutPanel14.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel14.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 85F));
            this.tableLayoutPanel14.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel14.Controls.Add(this.pnlSiteLicensing, 1, 0);
            this.tableLayoutPanel14.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel14.Location = new System.Drawing.Point(0, 636);
            this.tableLayoutPanel14.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel14.Name = "tableLayoutPanel14";
            this.tableLayoutPanel14.RowCount = 1;
            this.tableLayoutPanel14.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel14.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel14.Size = new System.Drawing.Size(1151, 45);
            this.tableLayoutPanel14.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btn_Refresh);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(172, 45);
            this.panel1.TabIndex = 0;
            // 
            // frmSiteLicensing
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1151, 681);
            this.Controls.Add(this.tableLayoutPanel3);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.Name = "frmSiteLicensing";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Site Licensing ";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmSiteLicensing_Load);
            this.pnlSiteLicensing.ResumeLayout(false);
            this.flowLayoutPanel5.ResumeLayout(false);
            this.tabPgLicenseGen.ResumeLayout(false);
            this.tabLicenseGen.ResumeLayout(false);
            this.tabPgRuleINfo.ResumeLayout(false);
            this.grpRuleSettings.ResumeLayout(false);
            this.grpRuleSettings.PerformLayout();
            this.grpValidationParam.ResumeLayout(false);
            this.tbllpValidationParam.ResumeLayout(false);
            this.tbllpValidationParam.PerformLayout();
            this.grpRuleNames.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.tabPgKeyGeneration.ResumeLayout(false);
            this.tableLayoutPanel6.ResumeLayout(false);
            this.grpSiteSelection.ResumeLayout(false);
            this.tableLayoutPanel13.ResumeLayout(false);
            this.tableLayoutPanel13.PerformLayout();
            this.grpgenLicense.ResumeLayout(false);
            this.grpgenLicense.PerformLayout();
            this.tableLayoutPanel8.ResumeLayout(false);
            this.grpLicenseSettings.ResumeLayout(false);
            this.tableLayoutPanel10.ResumeLayout(false);
            this.grpValidationParameter.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel11.ResumeLayout(false);
            this.tableLayoutPanel11.PerformLayout();
            this.tableLayoutPanel12.ResumeLayout(false);
            this.tableLayoutPanel12.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUDAlertBefor)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtgvAssociatedSites)).EndInit();
            this.tabSiteLicensing.ResumeLayout(false);
            this.tabPgViewCancelLicense.ResumeLayout(false);
            this.sC_ViewCancelLicense.Panel1.ResumeLayout(false);
            this.sC_ViewCancelLicense.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.sC_ViewCancelLicense)).EndInit();
            this.sC_ViewCancelLicense.ResumeLayout(false);
            this.grpSiteList.ResumeLayout(false);
            this.pnlLicenseDetails.ResumeLayout(false);
            this.tableLayoutPanel9.ResumeLayout(false);
            this.grpLicenseDetails.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtgvLicenseDetails)).EndInit();
            this.contextMenuStrip2.ResumeLayout(false);
            this.tabPgLicenseHistory.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.grpSearchCriteria.ResumeLayout(false);
            this.tableLayoutPanel7.ResumeLayout(false);
            this.tbllpSearchcriteria.ResumeLayout(false);
            this.tbllpSearchcriteria.PerformLayout();
            this.tableLayoutPanel5.ResumeLayout(false);
            this.grpSCValidationParam.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.grpUserFilter.ResumeLayout(false);
            this.tbluserfilter.ResumeLayout(false);
            this.tbluserfilter.PerformLayout();
            this.grpSearchResults.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtGVSearchResults)).EndInit();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel14.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ImageList imgltSiteStatus;
        private System.Windows.Forms.Panel pnlSiteLicensing;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.TabPage tabPgLicenseGen;
        private System.Windows.Forms.TabControl tabLicenseGen;
        private System.Windows.Forms.TabPage tabPgKeyGeneration;
        private System.Windows.Forms.Panel pnlKeyGeneration;
        private System.Windows.Forms.GroupBox grpgenLicense;
        private System.Windows.Forms.TextBox txtLicenseKey;
        private System.Windows.Forms.Label lblLicenseKey;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.GroupBox grpSiteSelection;
        private BMC.Common.Utilities.BmcComboBox cmbSiteName;
        private System.Windows.Forms.Label lblSiteName;
        private BMC.Common.Utilities.BmcComboBox cmbSubCompName;
        private System.Windows.Forms.Label lblSubCompName;
        private BMC.Common.Utilities.BmcComboBox cmbCompName;
        private System.Windows.Forms.Label lblCompName;
        private System.Windows.Forms.GroupBox grpLicenseSettings;
        private BMC.Common.Utilities.BmcComboBox cmbLSRuleName;
        private System.Windows.Forms.Label lblLSRuleName;
        private System.Windows.Forms.DateTimePicker dtpkExpiryDate;
        private System.Windows.Forms.Label lblExpiryDate;
        private System.Windows.Forms.DateTimePicker dtpkStartDate;
        private System.Windows.Forms.Label lblStartDate;
        private System.Windows.Forms.TabControl tabSiteLicensing;
        private System.Windows.Forms.TabPage tabPgViewCancelLicense;
        private System.Windows.Forms.NumericUpDown numUDAlertBefor;
        private System.Windows.Forms.Label lblinDays;
        private System.Windows.Forms.Label lblAlertBefor;
        private System.Windows.Forms.CheckBox chkLSWarningOnly;
        private System.Windows.Forms.CheckBox chkLSDisableEGMs;
        private System.Windows.Forms.TabPage tabPgRuleINfo;
        private System.Windows.Forms.GroupBox grpRuleNames;
        private System.Windows.Forms.ListBox lstRuleNames;
        private System.Windows.Forms.GroupBox grpRuleSettings;
        private System.Windows.Forms.GroupBox grpValidationParam;
        private System.Windows.Forms.CheckBox chkRSDisableEGMs;
        private System.Windows.Forms.CheckBox chkRSWarningOnly;
        private System.Windows.Forms.CheckBox chkRSAlertreq;
        private System.Windows.Forms.CheckBox chkRSLockSite;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnAddNew;
        private System.Windows.Forms.TextBox txtRSRuleName;
        private System.Windows.Forms.Label lblRSRuleName;
        private System.Windows.Forms.CheckBox chkRSValidationReq;
        private System.Windows.Forms.TableLayoutPanel tbllpValidationParam;
        private System.Windows.Forms.CheckBox chkLSAlertReq;
        private System.Windows.Forms.GroupBox grpSiteList;
        private System.Windows.Forms.TreeView tvSiteList;
        private System.Windows.Forms.Panel pnlLicenseDetails;
        private System.Windows.Forms.GroupBox grpLicenseDetails;
        private System.Windows.Forms.DataGridView dtgvLicenseDetails;
        private System.Windows.Forms.Button btnCancelLicense;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnSLUpdate;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dtgvAssociatedSites;
        private System.Windows.Forms.TabPage tabPgLicenseHistory;
        private System.Windows.Forms.Button btnLHClear;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.GroupBox grpSearchResults;
        private System.Windows.Forms.DataGridView dtGVSearchResults;
        private System.Windows.Forms.GroupBox grpSearchCriteria;
        private System.Windows.Forms.GroupBox grpSCValidationParam;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private BMC.Common.Utilities.BmcComboBox cmbSCLockSite;
        private System.Windows.Forms.Label lblSCLockSite;
        private BMC.Common.Utilities.BmcComboBox cmbSCValidationReq;
        private System.Windows.Forms.Label lblSCValidationReq;
        private System.Windows.Forms.Label lblSCDisableEGM;
        private BMC.Common.Utilities.BmcComboBox cmbSCDisableEGM;
        private System.Windows.Forms.Label lblAlertRequired;
        private System.Windows.Forms.Label lblSCWarningOnly;
        private BMC.Common.Utilities.BmcComboBox cmbSCAlertRequired;
        private BMC.Common.Utilities.BmcComboBox cmbSCWarningOnly;
        private System.Windows.Forms.TableLayoutPanel tbllpSearchcriteria;
        private System.Windows.Forms.DateTimePicker dtpkToExpiryDate;
        private System.Windows.Forms.DateTimePicker dtpkToStartDate;
        private System.Windows.Forms.Label lblToStartDate;
        private BMC.Common.Utilities.BmcComboBox cmbSCCompanyName;
        private System.Windows.Forms.Label lblSCCompName;
        private BMC.Common.Utilities.BmcComboBox cmbSCSubCompanyName;
        private System.Windows.Forms.Label lblSCSubCompName;
        private System.Windows.Forms.Label lblSCSiteName;
        private System.Windows.Forms.Label lblfromStartDate;
        private System.Windows.Forms.DateTimePicker dtpkFromStartDate;
        private BMC.Common.Utilities.BmcComboBox cmbSCSiteName;
        private System.Windows.Forms.Label lblFromExpiryDate;
        private System.Windows.Forms.Label lblSCKeyStatus;
        private System.Windows.Forms.DateTimePicker dtpkFromExpiryDate;
        private BMC.Common.Utilities.BmcComboBox cmbSCKeyStatus;
        private System.Windows.Forms.Label lblToExpiryDate;
        private System.Windows.Forms.SplitContainer sC_ViewCancelLicense;
        private System.Windows.Forms.GroupBox grpValidationParameter;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.CheckBox chkLSValidationReq;
        private System.Windows.Forms.CheckBox chkLSLockSite;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel7;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel8;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel9;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel10;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel11;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel12;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel13;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel5;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem addNewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.Button btn_Refresh;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem cancelLicenseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
        private System.Windows.Forms.Button btnActivateLicense;
        private System.Windows.Forms.ToolStripMenuItem activateLicenseToolStripMenuItem;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.GroupBox grpUserFilter;
        private System.Windows.Forms.TableLayoutPanel tbluserfilter;
        private System.Windows.Forms.Label lblcreate;
        private System.Windows.Forms.Label lblCancelBy;
        private BMC.Common.Utilities.BmcComboBox cmbCancelBy;
        private BMC.Common.Utilities.BmcComboBox cmbActivatedBy;
        private BMC.Common.Utilities.BmcComboBox cmbcreateBy;
        private System.Windows.Forms.Label lblActivatedBy;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel14;
        private System.Windows.Forms.Panel panel1;
    }
}

