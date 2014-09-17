namespace BMC.EnterpriseClient.Views
{
    partial class frmAdminSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAdminSettings));
            this.tblMain = new System.Windows.Forms.TableLayoutPanel();
            this.tblStock = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tblInner = new System.Windows.Forms.TableLayoutPanel();
            this.grpOtherSettings = new System.Windows.Forms.GroupBox();
            this.tblOtherSettings = new System.Windows.Forms.TableLayoutPanel();
            this.chkCancelPendingMails = new System.Windows.Forms.CheckBox();
            this.chkPowerPomoReportsRequired = new System.Windows.Forms.CheckBox();
            this.tblOtherSettingsRegion = new System.Windows.Forms.TableLayoutPanel();
            this.cmbCulture = new System.Windows.Forms.ComboBox();
            this.lblRegion = new System.Windows.Forms.Label();
            this.chkCentralizedDeclaration = new System.Windows.Forms.CheckBox();
            this.chkOfflineDeclaration = new System.Windows.Forms.CheckBox();
            this.chkAllowBPEnable = new System.Windows.Forms.CheckBox();
            this.chkSiteLicensingRequired = new System.Windows.Forms.CheckBox();
            this.chkEmployeecardTrackingRequired = new System.Windows.Forms.CheckBox();
            this.chkImportExportAssetFile = new System.Windows.Forms.CheckBox();
            this.chkValidateAGS = new System.Windows.Forms.CheckBox();
            this.chkPartiallyConfigured = new System.Windows.Forms.CheckBox();
            this.chkGameCappingEnabled = new System.Windows.Forms.CheckBox();
            this.chkSuppressGroupByZone = new System.Windows.Forms.CheckBox();
            this.chkIsSingleEmpCard = new System.Windows.Forms.CheckBox();
            this.chkMailAlert = new System.Windows.Forms.CheckBox();
            this.chkIsAlertEnabled = new System.Windows.Forms.CheckBox();
            this.cb_SendMailFromEnterprise = new System.Windows.Forms.CheckBox();
            this.cb_IsAutoCalendarEnabled = new System.Windows.Forms.CheckBox();
            this.chkAddSPinVout = new System.Windows.Forms.CheckBox();
            this.chkAllowMultipleDrops = new System.Windows.Forms.CheckBox();
            this.grpAssetSettings = new System.Windows.Forms.GroupBox();
            this.tblAssetDetails = new System.Windows.Forms.TableLayoutPanel();
            this.cmbUnallocatedMachine = new System.Windows.Forms.ComboBox();
            this.cmbUnallocatedGame = new System.Windows.Forms.ComboBox();
            this.cmbUnallocatedType = new System.Windows.Forms.ComboBox();
            this.txtAssetNumberMinLength = new System.Windows.Forms.TextBox();
            this.txtAssetNumberPrefix = new System.Windows.Forms.TextBox();
            this.chkAutoGenAssetNumber = new System.Windows.Forms.CheckBox();
            this.chkAllowBulkAssetPurchase = new System.Windows.Forms.CheckBox();
            this.chkForceSiteRepforAsset = new System.Windows.Forms.CheckBox();
            this.chkForceCodeToSite = new System.Windows.Forms.CheckBox();
            this.txtGameCodeMinLength = new System.Windows.Forms.TextBox();
            this.txtGameCodePrefix = new System.Windows.Forms.TextBox();
            this.lblGameCodePrefix = new System.Windows.Forms.Label();
            this.chkAutoGenGameCode = new System.Windows.Forms.CheckBox();
            this.lblAutoGenGameCode = new System.Windows.Forms.Label();
            this.lblGameCodeLen = new System.Windows.Forms.Label();
            this.lblAutoGenAssetNo = new System.Windows.Forms.Label();
            this.lblAssetNoPrefix = new System.Windows.Forms.Label();
            this.lblAssetNoLen = new System.Windows.Forms.Label();
            this.lblUnallocatedType = new System.Windows.Forms.Label();
            this.lblUnallocatedGame = new System.Windows.Forms.Label();
            this.lblUnallocatedMachine = new System.Windows.Forms.Label();
            this.lblForceAsset = new System.Windows.Forms.Label();
            this.lblAllowBulkPurchase = new System.Windows.Forms.Label();
            this.lblEnforceCodes = new System.Windows.Forms.Label();
            this.grpAGSCombination = new System.Windows.Forms.GroupBox();
            this.tblAGSCombination = new System.Windows.Forms.TableLayoutPanel();
            this.chkSerial = new System.Windows.Forms.CheckBox();
            this.chkAsset = new System.Windows.Forms.CheckBox();
            this.chkGMU = new System.Windows.Forms.CheckBox();
            this.gp_VaultSettings = new System.Windows.Forms.GroupBox();
            this.btnEditTransactionReason = new System.Windows.Forms.Button();
            this.tblFooter = new System.Windows.Forms.TableLayoutPanel();
            this.btnClose = new System.Windows.Forms.Button();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.tblMain.SuspendLayout();
            this.tblStock.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tblInner.SuspendLayout();
            this.grpOtherSettings.SuspendLayout();
            this.tblOtherSettings.SuspendLayout();
            this.tblOtherSettingsRegion.SuspendLayout();
            this.grpAssetSettings.SuspendLayout();
            this.tblAssetDetails.SuspendLayout();
            this.grpAGSCombination.SuspendLayout();
            this.tblAGSCombination.SuspendLayout();
            this.gp_VaultSettings.SuspendLayout();
            this.tblFooter.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tblMain
            // 
            this.tblMain.ColumnCount = 1;
            this.tblMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblMain.Controls.Add(this.tblStock, 0, 0);
            this.tblMain.Controls.Add(this.tblFooter, 0, 1);
            this.tblMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblMain.Location = new System.Drawing.Point(0, 0);
            this.tblMain.Name = "tblMain";
            this.tblMain.RowCount = 2;
            this.tblMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tblMain.Size = new System.Drawing.Size(786, 692);
            this.tblMain.TabIndex = 0;
            // 
            // tblStock
            // 
            this.tblStock.ColumnCount = 3;
            this.tblStock.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 1F));
            this.tblStock.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 98F));
            this.tblStock.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 1F));
            this.tblStock.Controls.Add(this.panel1, 1, 1);
            this.tblStock.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblStock.Location = new System.Drawing.Point(0, 0);
            this.tblStock.Margin = new System.Windows.Forms.Padding(0);
            this.tblStock.Name = "tblStock";
            this.tblStock.RowCount = 3;
            this.tblStock.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 1F));
            this.tblStock.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 98F));
            this.tblStock.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 1F));
            this.tblStock.Size = new System.Drawing.Size(786, 647);
            this.tblStock.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.tblInner);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(10, 9);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(764, 628);
            this.panel1.TabIndex = 2;
            // 
            // tblInner
            // 
            this.tblInner.ColumnCount = 1;
            this.tblInner.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblInner.Controls.Add(this.grpOtherSettings, 0, 1);
            this.tblInner.Controls.Add(this.grpAssetSettings, 0, 0);
            this.tblInner.Controls.Add(this.grpAGSCombination, 0, 2);
            this.tblInner.Controls.Add(this.gp_VaultSettings, 0, 3);
            this.tblInner.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblInner.Location = new System.Drawing.Point(0, 0);
            this.tblInner.Margin = new System.Windows.Forms.Padding(0);
            this.tblInner.Name = "tblInner";
            this.tblInner.RowCount = 4;
            this.tblInner.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 215F));
            this.tblInner.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 302F));
            this.tblInner.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.tblInner.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tblInner.Size = new System.Drawing.Size(764, 628);
            this.tblInner.TabIndex = 1;
            // 
            // grpOtherSettings
            // 
            this.grpOtherSettings.Controls.Add(this.tblOtherSettings);
            this.grpOtherSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpOtherSettings.Location = new System.Drawing.Point(3, 218);
            this.grpOtherSettings.Name = "grpOtherSettings";
            this.grpOtherSettings.Size = new System.Drawing.Size(758, 296);
            this.grpOtherSettings.TabIndex = 1;
            this.grpOtherSettings.TabStop = false;
            this.grpOtherSettings.Text = "Other Settings";
            // 
            // tblOtherSettings
            // 
            this.tblOtherSettings.ColumnCount = 4;
            this.tblOtherSettings.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 27.47F));
            this.tblOtherSettings.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22.55F));
            this.tblOtherSettings.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.47F));
            this.tblOtherSettings.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 24.51F));
            this.tblOtherSettings.Controls.Add(this.chkCancelPendingMails, 3, 8);
            this.tblOtherSettings.Controls.Add(this.chkPowerPomoReportsRequired, 0, 1);
            this.tblOtherSettings.Controls.Add(this.tblOtherSettingsRegion, 2, 1);
            this.tblOtherSettings.Controls.Add(this.chkCentralizedDeclaration, 0, 2);
            this.tblOtherSettings.Controls.Add(this.chkOfflineDeclaration, 2, 2);
            this.tblOtherSettings.Controls.Add(this.chkAllowBPEnable, 0, 3);
            this.tblOtherSettings.Controls.Add(this.chkSiteLicensingRequired, 2, 3);
            this.tblOtherSettings.Controls.Add(this.chkEmployeecardTrackingRequired, 0, 4);
            this.tblOtherSettings.Controls.Add(this.chkImportExportAssetFile, 2, 4);
            this.tblOtherSettings.Controls.Add(this.chkValidateAGS, 0, 5);
            this.tblOtherSettings.Controls.Add(this.chkPartiallyConfigured, 2, 5);
            this.tblOtherSettings.Controls.Add(this.chkGameCappingEnabled, 0, 6);
            this.tblOtherSettings.Controls.Add(this.chkSuppressGroupByZone, 2, 6);
            this.tblOtherSettings.Controls.Add(this.chkIsSingleEmpCard, 0, 7);
            this.tblOtherSettings.Controls.Add(this.chkMailAlert, 2, 7);
            this.tblOtherSettings.Controls.Add(this.chkIsAlertEnabled, 0, 8);
            this.tblOtherSettings.Controls.Add(this.cb_SendMailFromEnterprise, 2, 8);
            this.tblOtherSettings.Controls.Add(this.cb_IsAutoCalendarEnabled, 0, 9);
            this.tblOtherSettings.Controls.Add(this.chkAddSPinVout, 3, 9);
            this.tblOtherSettings.Controls.Add(this.chkAllowMultipleDrops, 2, 9);
            this.tblOtherSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblOtherSettings.Location = new System.Drawing.Point(3, 16);
            this.tblOtherSettings.Name = "tblOtherSettings";
            this.tblOtherSettings.RowCount = 11;
            this.tblOtherSettings.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tblOtherSettings.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.tblOtherSettings.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.tblOtherSettings.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.tblOtherSettings.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.tblOtherSettings.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.tblOtherSettings.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.tblOtherSettings.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.tblOtherSettings.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.tblOtherSettings.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.tblOtherSettings.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tblOtherSettings.Size = new System.Drawing.Size(752, 277);
            this.tblOtherSettings.TabIndex = 0;
            // 
            // chkCancelPendingMails
            // 
            this.chkCancelPendingMails.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.chkCancelPendingMails.AutoSize = true;
            this.chkCancelPendingMails.Location = new System.Drawing.Point(569, 214);
            this.chkCancelPendingMails.Name = "chkCancelPendingMails";
            this.chkCancelPendingMails.Size = new System.Drawing.Size(134, 17);
            this.chkCancelPendingMails.TabIndex = 20;
            this.chkCancelPendingMails.Text = "Cancel Pending Emails";
            this.chkCancelPendingMails.UseVisualStyleBackColor = true;
            // 
            // chkPowerPomoReportsRequired
            // 
            this.chkPowerPomoReportsRequired.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.chkPowerPomoReportsRequired.AutoSize = true;
            this.chkPowerPomoReportsRequired.Location = new System.Drawing.Point(3, 11);
            this.chkPowerPomoReportsRequired.Name = "chkPowerPomoReportsRequired";
            this.chkPowerPomoReportsRequired.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.chkPowerPomoReportsRequired.Size = new System.Drawing.Size(132, 17);
            this.chkPowerPomoReportsRequired.TabIndex = 0;
            this.chkPowerPomoReportsRequired.Text = "Power Promo Reports ";
            this.chkPowerPomoReportsRequired.UseVisualStyleBackColor = true;
            // 
            // tblOtherSettingsRegion
            // 
            this.tblOtherSettingsRegion.ColumnCount = 2;
            this.tblOtherSettingsRegion.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tblOtherSettingsRegion.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblOtherSettingsRegion.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblOtherSettingsRegion.Controls.Add(this.cmbCulture, 1, 0);
            this.tblOtherSettingsRegion.Controls.Add(this.lblRegion, 0, 0);
            this.tblOtherSettingsRegion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblOtherSettingsRegion.Location = new System.Drawing.Point(375, 5);
            this.tblOtherSettingsRegion.Margin = new System.Windows.Forms.Padding(0);
            this.tblOtherSettingsRegion.Name = "tblOtherSettingsRegion";
            this.tblOtherSettingsRegion.RowCount = 1;
            this.tblOtherSettingsRegion.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblOtherSettingsRegion.Size = new System.Drawing.Size(191, 29);
            this.tblOtherSettingsRegion.TabIndex = 7;
            // 
            // cmbCulture
            // 
            this.cmbCulture.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbCulture.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCulture.FormattingEnabled = true;
            this.cmbCulture.Location = new System.Drawing.Point(63, 3);
            this.cmbCulture.Name = "cmbCulture";
            this.cmbCulture.Size = new System.Drawing.Size(125, 21);
            this.cmbCulture.TabIndex = 8;
            // 
            // lblRegion
            // 
            this.lblRegion.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblRegion.AutoSize = true;
            this.lblRegion.Location = new System.Drawing.Point(3, 8);
            this.lblRegion.Name = "lblRegion";
            this.lblRegion.Size = new System.Drawing.Size(44, 13);
            this.lblRegion.TabIndex = 0;
            this.lblRegion.Text = "Region:";
            // 
            // chkCentralizedDeclaration
            // 
            this.chkCentralizedDeclaration.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.chkCentralizedDeclaration.AutoSize = true;
            this.chkCentralizedDeclaration.Location = new System.Drawing.Point(3, 40);
            this.chkCentralizedDeclaration.Name = "chkCentralizedDeclaration";
            this.chkCentralizedDeclaration.Size = new System.Drawing.Size(135, 17);
            this.chkCentralizedDeclaration.TabIndex = 1;
            this.chkCentralizedDeclaration.Text = "Centralized Declaration";
            this.chkCentralizedDeclaration.UseVisualStyleBackColor = true;
            // 
            // chkOfflineDeclaration
            // 
            this.chkOfflineDeclaration.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.chkOfflineDeclaration.AutoSize = true;
            this.chkOfflineDeclaration.Location = new System.Drawing.Point(378, 40);
            this.chkOfflineDeclaration.Name = "chkOfflineDeclaration";
            this.chkOfflineDeclaration.Size = new System.Drawing.Size(141, 17);
            this.chkOfflineDeclaration.TabIndex = 9;
            this.chkOfflineDeclaration.Text = "Allow Offline Declaration";
            this.chkOfflineDeclaration.UseVisualStyleBackColor = true;
            // 
            // chkAllowBPEnable
            // 
            this.chkAllowBPEnable.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.chkAllowBPEnable.AutoSize = true;
            this.chkAllowBPEnable.Location = new System.Drawing.Point(3, 69);
            this.chkAllowBPEnable.Name = "chkAllowBPEnable";
            this.chkAllowBPEnable.Size = new System.Drawing.Size(172, 17);
            this.chkAllowBPEnable.TabIndex = 2;
            this.chkAllowBPEnable.Text = "AllowEnableDisableBarPosition";
            this.chkAllowBPEnable.UseVisualStyleBackColor = true;
            // 
            // chkSiteLicensingRequired
            // 
            this.chkSiteLicensingRequired.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.chkSiteLicensingRequired.AutoSize = true;
            this.chkSiteLicensingRequired.Location = new System.Drawing.Point(378, 69);
            this.chkSiteLicensingRequired.Name = "chkSiteLicensingRequired";
            this.chkSiteLicensingRequired.Size = new System.Drawing.Size(134, 17);
            this.chkSiteLicensingRequired.TabIndex = 10;
            this.chkSiteLicensingRequired.Text = "Site Licensing Enabled";
            this.chkSiteLicensingRequired.UseVisualStyleBackColor = true;
            // 
            // chkEmployeecardTrackingRequired
            // 
            this.chkEmployeecardTrackingRequired.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.chkEmployeecardTrackingRequired.AutoSize = true;
            this.chkEmployeecardTrackingRequired.Location = new System.Drawing.Point(3, 98);
            this.chkEmployeecardTrackingRequired.Name = "chkEmployeecardTrackingRequired";
            this.chkEmployeecardTrackingRequired.Size = new System.Drawing.Size(195, 17);
            this.chkEmployeecardTrackingRequired.TabIndex = 3;
            this.chkEmployeecardTrackingRequired.Text = "Is Employee Card Tracking Enabled";
            this.chkEmployeecardTrackingRequired.UseVisualStyleBackColor = true;
            // 
            // chkImportExportAssetFile
            // 
            this.chkImportExportAssetFile.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.chkImportExportAssetFile.AutoSize = true;
            this.chkImportExportAssetFile.Location = new System.Drawing.Point(378, 98);
            this.chkImportExportAssetFile.Name = "chkImportExportAssetFile";
            this.chkImportExportAssetFile.Size = new System.Drawing.Size(138, 17);
            this.chkImportExportAssetFile.TabIndex = 11;
            this.chkImportExportAssetFile.Text = "Import/Export Asset File";
            this.chkImportExportAssetFile.UseVisualStyleBackColor = true;
            // 
            // chkValidateAGS
            // 
            this.chkValidateAGS.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.chkValidateAGS.AutoSize = true;
            this.chkValidateAGS.Location = new System.Drawing.Point(3, 127);
            this.chkValidateAGS.Name = "chkValidateAGS";
            this.chkValidateAGS.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.chkValidateAGS.Size = new System.Drawing.Size(200, 17);
            this.chkValidateAGS.TabIndex = 4;
            this.chkValidateAGS.Text = "Validate AGS For GMU No Updation ";
            this.chkValidateAGS.UseVisualStyleBackColor = true;
            // 
            // chkPartiallyConfigured
            // 
            this.chkPartiallyConfigured.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.chkPartiallyConfigured.AutoSize = true;
            this.chkPartiallyConfigured.Location = new System.Drawing.Point(378, 127);
            this.chkPartiallyConfigured.Name = "chkPartiallyConfigured";
            this.chkPartiallyConfigured.Size = new System.Drawing.Size(168, 17);
            this.chkPartiallyConfigured.TabIndex = 12;
            this.chkPartiallyConfigured.Text = "View Sites Partially Configured";
            this.chkPartiallyConfigured.UseVisualStyleBackColor = true;
            // 
            // chkGameCappingEnabled
            // 
            this.chkGameCappingEnabled.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.chkGameCappingEnabled.AutoSize = true;
            this.chkGameCappingEnabled.Location = new System.Drawing.Point(3, 156);
            this.chkGameCappingEnabled.Name = "chkGameCappingEnabled";
            this.chkGameCappingEnabled.Size = new System.Drawing.Size(138, 17);
            this.chkGameCappingEnabled.TabIndex = 5;
            this.chkGameCappingEnabled.Text = "Game Capping Enabled";
            this.chkGameCappingEnabled.UseVisualStyleBackColor = true;
            // 
            // chkSuppressGroupByZone
            // 
            this.chkSuppressGroupByZone.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.chkSuppressGroupByZone.AutoSize = true;
            this.chkSuppressGroupByZone.Location = new System.Drawing.Point(378, 156);
            this.chkSuppressGroupByZone.Name = "chkSuppressGroupByZone";
            this.chkSuppressGroupByZone.Size = new System.Drawing.Size(142, 17);
            this.chkSuppressGroupByZone.TabIndex = 13;
            this.chkSuppressGroupByZone.Text = "Suppress GroupBy Zone";
            this.chkSuppressGroupByZone.UseVisualStyleBackColor = true;
            // 
            // chkIsSingleEmpCard
            // 
            this.chkIsSingleEmpCard.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.chkIsSingleEmpCard.AutoSize = true;
            this.chkIsSingleEmpCard.Location = new System.Drawing.Point(3, 185);
            this.chkIsSingleEmpCard.Name = "chkIsSingleEmpCard";
            this.chkIsSingleEmpCard.Size = new System.Drawing.Size(140, 17);
            this.chkIsSingleEmpCard.TabIndex = 6;
            this.chkIsSingleEmpCard.Text = "Is Single Card Employee";
            this.chkIsSingleEmpCard.UseVisualStyleBackColor = true;
            // 
            // chkMailAlert
            // 
            this.chkMailAlert.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.chkMailAlert.AutoSize = true;
            this.chkMailAlert.Location = new System.Drawing.Point(378, 185);
            this.chkMailAlert.Name = "chkMailAlert";
            this.chkMailAlert.Size = new System.Drawing.Size(100, 17);
            this.chkMailAlert.TabIndex = 15;
            this.chkMailAlert.Text = "Is Alert Enabled";
            this.chkMailAlert.UseVisualStyleBackColor = true;
            // 
            // chkIsAlertEnabled
            // 
            this.chkIsAlertEnabled.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.chkIsAlertEnabled.AutoSize = true;
            this.chkIsAlertEnabled.Location = new System.Drawing.Point(3, 214);
            this.chkIsAlertEnabled.Name = "chkIsAlertEnabled";
            this.chkIsAlertEnabled.Size = new System.Drawing.Size(128, 17);
            this.chkIsAlertEnabled.TabIndex = 16;
            this.chkIsAlertEnabled.Text = "Is Email Alert Enabled";
            this.chkIsAlertEnabled.UseVisualStyleBackColor = true;
            // 
            // cb_SendMailFromEnterprise
            // 
            this.cb_SendMailFromEnterprise.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cb_SendMailFromEnterprise.AutoSize = true;
            this.cb_SendMailFromEnterprise.Location = new System.Drawing.Point(378, 214);
            this.cb_SendMailFromEnterprise.Name = "cb_SendMailFromEnterprise";
            this.cb_SendMailFromEnterprise.Size = new System.Drawing.Size(145, 17);
            this.cb_SendMailFromEnterprise.TabIndex = 17;
            this.cb_SendMailFromEnterprise.Text = "Send mail from Enterprise";
            this.cb_SendMailFromEnterprise.UseVisualStyleBackColor = true;
            // 
            // cb_IsAutoCalendarEnabled
            // 
            this.cb_IsAutoCalendarEnabled.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cb_IsAutoCalendarEnabled.AutoSize = true;
            this.cb_IsAutoCalendarEnabled.Location = new System.Drawing.Point(3, 243);
            this.cb_IsAutoCalendarEnabled.Name = "cb_IsAutoCalendarEnabled";
            this.cb_IsAutoCalendarEnabled.Size = new System.Drawing.Size(143, 17);
            this.cb_IsAutoCalendarEnabled.TabIndex = 18;
            this.cb_IsAutoCalendarEnabled.Text = "Is AutoCalendar Enabled";
            this.cb_IsAutoCalendarEnabled.UseVisualStyleBackColor = true;
            // 
            // chkAddSPinVout
            // 
            this.chkAddSPinVout.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.chkAddSPinVout.AutoSize = true;
            this.chkAddSPinVout.Location = new System.Drawing.Point(569, 243);
            this.chkAddSPinVout.Name = "chkAddSPinVout";
            this.chkAddSPinVout.Size = new System.Drawing.Size(164, 17);
            this.chkAddSPinVout.TabIndex = 14;
            this.chkAddSPinVout.TabStop = false;
            this.chkAddSPinVout.Text = "Add Shortpay in Voucher Out";
            this.chkAddSPinVout.UseVisualStyleBackColor = true;
            this.chkAddSPinVout.Visible = false;
            // 
            // chkAllowMultipleDrops
            // 
            this.chkAllowMultipleDrops.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.chkAllowMultipleDrops.AutoSize = true;
            this.chkAllowMultipleDrops.Location = new System.Drawing.Point(378, 243);
            this.chkAllowMultipleDrops.Name = "chkAllowMultipleDrops";
            this.chkAllowMultipleDrops.Size = new System.Drawing.Size(121, 17);
            this.chkAllowMultipleDrops.TabIndex = 19;
            this.chkAllowMultipleDrops.Text = "Allow Multiple Drops";
            this.chkAllowMultipleDrops.UseVisualStyleBackColor = true;
            // 
            // grpAssetSettings
            // 
            this.grpAssetSettings.Controls.Add(this.tblAssetDetails);
            this.grpAssetSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpAssetSettings.Location = new System.Drawing.Point(3, 3);
            this.grpAssetSettings.Name = "grpAssetSettings";
            this.grpAssetSettings.Size = new System.Drawing.Size(758, 209);
            this.grpAssetSettings.TabIndex = 0;
            this.grpAssetSettings.TabStop = false;
            this.grpAssetSettings.Text = "Asset Settings";
            // 
            // tblAssetDetails
            // 
            this.tblAssetDetails.ColumnCount = 5;
            this.tblAssetDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 27.47032F));
            this.tblAssetDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.70605F));
            this.tblAssetDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7.843233F));
            this.tblAssetDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.4703F));
            this.tblAssetDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 24.51009F));
            this.tblAssetDetails.Controls.Add(this.cmbUnallocatedMachine, 4, 6);
            this.tblAssetDetails.Controls.Add(this.cmbUnallocatedGame, 4, 5);
            this.tblAssetDetails.Controls.Add(this.cmbUnallocatedType, 4, 4);
            this.tblAssetDetails.Controls.Add(this.txtAssetNumberMinLength, 4, 3);
            this.tblAssetDetails.Controls.Add(this.txtAssetNumberPrefix, 4, 2);
            this.tblAssetDetails.Controls.Add(this.chkAutoGenAssetNumber, 4, 1);
            this.tblAssetDetails.Controls.Add(this.chkAllowBulkAssetPurchase, 1, 5);
            this.tblAssetDetails.Controls.Add(this.chkForceSiteRepforAsset, 1, 4);
            this.tblAssetDetails.Controls.Add(this.chkForceCodeToSite, 1, 6);
            this.tblAssetDetails.Controls.Add(this.txtGameCodeMinLength, 1, 3);
            this.tblAssetDetails.Controls.Add(this.txtGameCodePrefix, 1, 2);
            this.tblAssetDetails.Controls.Add(this.lblGameCodePrefix, 0, 2);
            this.tblAssetDetails.Controls.Add(this.chkAutoGenGameCode, 1, 1);
            this.tblAssetDetails.Controls.Add(this.lblAutoGenGameCode, 0, 1);
            this.tblAssetDetails.Controls.Add(this.lblGameCodeLen, 0, 3);
            this.tblAssetDetails.Controls.Add(this.lblAutoGenAssetNo, 3, 1);
            this.tblAssetDetails.Controls.Add(this.lblAssetNoPrefix, 3, 2);
            this.tblAssetDetails.Controls.Add(this.lblAssetNoLen, 3, 3);
            this.tblAssetDetails.Controls.Add(this.lblUnallocatedType, 3, 4);
            this.tblAssetDetails.Controls.Add(this.lblUnallocatedGame, 3, 5);
            this.tblAssetDetails.Controls.Add(this.lblUnallocatedMachine, 3, 6);
            this.tblAssetDetails.Controls.Add(this.lblForceAsset, 0, 4);
            this.tblAssetDetails.Controls.Add(this.lblAllowBulkPurchase, 0, 5);
            this.tblAssetDetails.Controls.Add(this.lblEnforceCodes, 0, 6);
            this.tblAssetDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblAssetDetails.Location = new System.Drawing.Point(3, 16);
            this.tblAssetDetails.Name = "tblAssetDetails";
            this.tblAssetDetails.RowCount = 8;
            this.tblAssetDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tblAssetDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.tblAssetDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.tblAssetDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.tblAssetDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.tblAssetDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.tblAssetDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.tblAssetDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tblAssetDetails.Size = new System.Drawing.Size(752, 190);
            this.tblAssetDetails.TabIndex = 0;
            // 
            // cmbUnallocatedMachine
            // 
            this.cmbUnallocatedMachine.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbUnallocatedMachine.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbUnallocatedMachine.FormattingEnabled = true;
            this.cmbUnallocatedMachine.Location = new System.Drawing.Point(568, 153);
            this.cmbUnallocatedMachine.Name = "cmbUnallocatedMachine";
            this.cmbUnallocatedMachine.Size = new System.Drawing.Size(181, 21);
            this.cmbUnallocatedMachine.TabIndex = 11;
            // 
            // cmbUnallocatedGame
            // 
            this.cmbUnallocatedGame.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbUnallocatedGame.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbUnallocatedGame.FormattingEnabled = true;
            this.cmbUnallocatedGame.Location = new System.Drawing.Point(568, 124);
            this.cmbUnallocatedGame.Name = "cmbUnallocatedGame";
            this.cmbUnallocatedGame.Size = new System.Drawing.Size(181, 21);
            this.cmbUnallocatedGame.TabIndex = 10;
            // 
            // cmbUnallocatedType
            // 
            this.cmbUnallocatedType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbUnallocatedType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbUnallocatedType.FormattingEnabled = true;
            this.cmbUnallocatedType.Location = new System.Drawing.Point(568, 95);
            this.cmbUnallocatedType.Name = "cmbUnallocatedType";
            this.cmbUnallocatedType.Size = new System.Drawing.Size(181, 21);
            this.cmbUnallocatedType.TabIndex = 9;
            // 
            // txtAssetNumberMinLength
            // 
            this.txtAssetNumberMinLength.Location = new System.Drawing.Point(568, 66);
            this.txtAssetNumberMinLength.MaxLength = 10;
            this.txtAssetNumberMinLength.Name = "txtAssetNumberMinLength";
            this.txtAssetNumberMinLength.Size = new System.Drawing.Size(52, 20);
            this.txtAssetNumberMinLength.TabIndex = 8;
            // 
            // txtAssetNumberPrefix
            // 
            this.txtAssetNumberPrefix.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtAssetNumberPrefix.Location = new System.Drawing.Point(568, 37);
            this.txtAssetNumberPrefix.MaxLength = 10;
            this.txtAssetNumberPrefix.Name = "txtAssetNumberPrefix";
            this.txtAssetNumberPrefix.Size = new System.Drawing.Size(181, 20);
            this.txtAssetNumberPrefix.TabIndex = 7;
            // 
            // chkAutoGenAssetNumber
            // 
            this.chkAutoGenAssetNumber.AutoSize = true;
            this.chkAutoGenAssetNumber.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkAutoGenAssetNumber.Location = new System.Drawing.Point(568, 8);
            this.chkAutoGenAssetNumber.Name = "chkAutoGenAssetNumber";
            this.chkAutoGenAssetNumber.Size = new System.Drawing.Size(181, 23);
            this.chkAutoGenAssetNumber.TabIndex = 6;
            this.chkAutoGenAssetNumber.UseVisualStyleBackColor = true;
            // 
            // chkAllowBulkAssetPurchase
            // 
            this.chkAllowBulkAssetPurchase.AutoSize = true;
            this.chkAllowBulkAssetPurchase.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkAllowBulkAssetPurchase.Location = new System.Drawing.Point(209, 124);
            this.chkAllowBulkAssetPurchase.Name = "chkAllowBulkAssetPurchase";
            this.chkAllowBulkAssetPurchase.Size = new System.Drawing.Size(104, 23);
            this.chkAllowBulkAssetPurchase.TabIndex = 4;
            this.chkAllowBulkAssetPurchase.UseVisualStyleBackColor = true;
            // 
            // chkForceSiteRepforAsset
            // 
            this.chkForceSiteRepforAsset.AutoSize = true;
            this.chkForceSiteRepforAsset.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkForceSiteRepforAsset.Location = new System.Drawing.Point(209, 95);
            this.chkForceSiteRepforAsset.Name = "chkForceSiteRepforAsset";
            this.chkForceSiteRepforAsset.Size = new System.Drawing.Size(104, 23);
            this.chkForceSiteRepforAsset.TabIndex = 3;
            this.chkForceSiteRepforAsset.UseVisualStyleBackColor = true;
            // 
            // chkForceCodeToSite
            // 
            this.chkForceCodeToSite.AutoSize = true;
            this.chkForceCodeToSite.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkForceCodeToSite.Location = new System.Drawing.Point(209, 153);
            this.chkForceCodeToSite.Name = "chkForceCodeToSite";
            this.chkForceCodeToSite.Size = new System.Drawing.Size(104, 23);
            this.chkForceCodeToSite.TabIndex = 5;
            this.chkForceCodeToSite.UseVisualStyleBackColor = true;
            this.chkForceCodeToSite.Visible = false;
            // 
            // txtGameCodeMinLength
            // 
            this.txtGameCodeMinLength.Location = new System.Drawing.Point(209, 66);
            this.txtGameCodeMinLength.MaxLength = 10;
            this.txtGameCodeMinLength.Name = "txtGameCodeMinLength";
            this.txtGameCodeMinLength.Size = new System.Drawing.Size(52, 20);
            this.txtGameCodeMinLength.TabIndex = 2;
            this.txtGameCodeMinLength.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtGameCodeMinLength_KeyPress);
            // 
            // txtGameCodePrefix
            // 
            this.txtGameCodePrefix.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtGameCodePrefix.Location = new System.Drawing.Point(209, 37);
            this.txtGameCodePrefix.MaxLength = 10;
            this.txtGameCodePrefix.Name = "txtGameCodePrefix";
            this.txtGameCodePrefix.Size = new System.Drawing.Size(104, 20);
            this.txtGameCodePrefix.TabIndex = 1;
            // 
            // lblGameCodePrefix
            // 
            this.lblGameCodePrefix.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblGameCodePrefix.AutoSize = true;
            this.lblGameCodePrefix.Location = new System.Drawing.Point(3, 42);
            this.lblGameCodePrefix.Name = "lblGameCodePrefix";
            this.lblGameCodePrefix.Size = new System.Drawing.Size(98, 13);
            this.lblGameCodePrefix.TabIndex = 2;
            this.lblGameCodePrefix.Text = "Game Code Prefix :";
            // 
            // chkAutoGenGameCode
            // 
            this.chkAutoGenGameCode.AutoSize = true;
            this.chkAutoGenGameCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkAutoGenGameCode.Location = new System.Drawing.Point(209, 8);
            this.chkAutoGenGameCode.Name = "chkAutoGenGameCode";
            this.chkAutoGenGameCode.Size = new System.Drawing.Size(104, 23);
            this.chkAutoGenGameCode.TabIndex = 0;
            this.chkAutoGenGameCode.UseVisualStyleBackColor = true;
            // 
            // lblAutoGenGameCode
            // 
            this.lblAutoGenGameCode.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblAutoGenGameCode.AutoSize = true;
            this.lblAutoGenGameCode.Location = new System.Drawing.Point(3, 13);
            this.lblAutoGenGameCode.Name = "lblAutoGenGameCode";
            this.lblAutoGenGameCode.Size = new System.Drawing.Size(146, 13);
            this.lblAutoGenGameCode.TabIndex = 0;
            this.lblAutoGenGameCode.Text = "Auto Generate Game Codes :";
            // 
            // lblGameCodeLen
            // 
            this.lblGameCodeLen.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblGameCodeLen.AutoSize = true;
            this.lblGameCodeLen.Location = new System.Drawing.Point(3, 71);
            this.lblGameCodeLen.Name = "lblGameCodeLen";
            this.lblGameCodeLen.Size = new System.Drawing.Size(125, 13);
            this.lblGameCodeLen.TabIndex = 4;
            this.lblGameCodeLen.Text = "Game Code Min Length :";
            // 
            // lblAutoGenAssetNo
            // 
            this.lblAutoGenAssetNo.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblAutoGenAssetNo.AutoSize = true;
            this.lblAutoGenAssetNo.Location = new System.Drawing.Point(377, 13);
            this.lblAutoGenAssetNo.Name = "lblAutoGenAssetNo";
            this.lblAutoGenAssetNo.Size = new System.Drawing.Size(151, 13);
            this.lblAutoGenAssetNo.TabIndex = 6;
            this.lblAutoGenAssetNo.Text = "Auto Generate Asset Number :";
            // 
            // lblAssetNoPrefix
            // 
            this.lblAssetNoPrefix.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblAssetNoPrefix.AutoSize = true;
            this.lblAssetNoPrefix.Location = new System.Drawing.Point(377, 42);
            this.lblAssetNoPrefix.Name = "lblAssetNoPrefix";
            this.lblAssetNoPrefix.Size = new System.Drawing.Size(108, 13);
            this.lblAssetNoPrefix.TabIndex = 14;
            this.lblAssetNoPrefix.Text = "Asset Number Prefix :";
            // 
            // lblAssetNoLen
            // 
            this.lblAssetNoLen.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblAssetNoLen.AutoSize = true;
            this.lblAssetNoLen.Location = new System.Drawing.Point(377, 71);
            this.lblAssetNoLen.Name = "lblAssetNoLen";
            this.lblAssetNoLen.Size = new System.Drawing.Size(135, 13);
            this.lblAssetNoLen.TabIndex = 16;
            this.lblAssetNoLen.Text = "Asset Number Min Length :";
            // 
            // lblUnallocatedType
            // 
            this.lblUnallocatedType.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblUnallocatedType.AutoSize = true;
            this.lblUnallocatedType.Location = new System.Drawing.Point(377, 100);
            this.lblUnallocatedType.Name = "lblUnallocatedType";
            this.lblUnallocatedType.Size = new System.Drawing.Size(97, 13);
            this.lblUnallocatedType.TabIndex = 18;
            this.lblUnallocatedType.Text = "Unallocated Type :";
            // 
            // lblUnallocatedGame
            // 
            this.lblUnallocatedGame.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblUnallocatedGame.AutoSize = true;
            this.lblUnallocatedGame.Location = new System.Drawing.Point(377, 129);
            this.lblUnallocatedGame.Name = "lblUnallocatedGame";
            this.lblUnallocatedGame.Size = new System.Drawing.Size(101, 13);
            this.lblUnallocatedGame.TabIndex = 20;
            this.lblUnallocatedGame.Text = "Unallocated Game :";
            // 
            // lblUnallocatedMachine
            // 
            this.lblUnallocatedMachine.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblUnallocatedMachine.AutoSize = true;
            this.lblUnallocatedMachine.Location = new System.Drawing.Point(377, 158);
            this.lblUnallocatedMachine.Name = "lblUnallocatedMachine";
            this.lblUnallocatedMachine.Size = new System.Drawing.Size(114, 13);
            this.lblUnallocatedMachine.TabIndex = 22;
            this.lblUnallocatedMachine.Text = "Unallocated Machine :";
            // 
            // lblForceAsset
            // 
            this.lblForceAsset.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblForceAsset.AutoSize = true;
            this.lblForceAsset.Location = new System.Drawing.Point(3, 100);
            this.lblForceAsset.Name = "lblForceAsset";
            this.lblForceAsset.Size = new System.Drawing.Size(181, 13);
            this.lblForceAsset.TabIndex = 6;
            this.lblForceAsset.Text = "Force Asset items to have a site rep :";
            // 
            // lblAllowBulkPurchase
            // 
            this.lblAllowBulkPurchase.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblAllowBulkPurchase.AutoSize = true;
            this.lblAllowBulkPurchase.Location = new System.Drawing.Point(3, 129);
            this.lblAllowBulkPurchase.Name = "lblAllowBulkPurchase";
            this.lblAllowBulkPurchase.Size = new System.Drawing.Size(175, 13);
            this.lblAllowBulkPurchase.TabIndex = 8;
            this.lblAllowBulkPurchase.Text = "Allow bulk purchase of asset items :";
            // 
            // lblEnforceCodes
            // 
            this.lblEnforceCodes.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblEnforceCodes.AutoSize = true;
            this.lblEnforceCodes.Location = new System.Drawing.Point(3, 158);
            this.lblEnforceCodes.Name = "lblEnforceCodes";
            this.lblEnforceCodes.Size = new System.Drawing.Size(120, 13);
            this.lblEnforceCodes.TabIndex = 10;
            this.lblEnforceCodes.Text = "Enforce Codes To Site :";
            this.lblEnforceCodes.Visible = false;
            // 
            // grpAGSCombination
            // 
            this.grpAGSCombination.Controls.Add(this.tblAGSCombination);
            this.grpAGSCombination.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpAGSCombination.Location = new System.Drawing.Point(3, 520);
            this.grpAGSCombination.Name = "grpAGSCombination";
            this.grpAGSCombination.Size = new System.Drawing.Size(758, 42);
            this.grpAGSCombination.TabIndex = 0;
            this.grpAGSCombination.TabStop = false;
            this.grpAGSCombination.Text = "AGS Combination";
            // 
            // tblAGSCombination
            // 
            this.tblAGSCombination.ColumnCount = 4;
            this.tblAGSCombination.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tblAGSCombination.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tblAGSCombination.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tblAGSCombination.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 55F));
            this.tblAGSCombination.Controls.Add(this.chkSerial, 0, 0);
            this.tblAGSCombination.Controls.Add(this.chkAsset, 1, 0);
            this.tblAGSCombination.Controls.Add(this.chkGMU, 2, 0);
            this.tblAGSCombination.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblAGSCombination.Location = new System.Drawing.Point(3, 16);
            this.tblAGSCombination.Name = "tblAGSCombination";
            this.tblAGSCombination.RowCount = 1;
            this.tblAGSCombination.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblAGSCombination.Size = new System.Drawing.Size(752, 23);
            this.tblAGSCombination.TabIndex = 0;
            // 
            // chkSerial
            // 
            this.chkSerial.AutoSize = true;
            this.chkSerial.Location = new System.Drawing.Point(3, 3);
            this.chkSerial.Name = "chkSerial";
            this.chkSerial.Size = new System.Drawing.Size(52, 17);
            this.chkSerial.TabIndex = 17;
            this.chkSerial.Text = "Serial";
            this.chkSerial.UseVisualStyleBackColor = true;
            // 
            // chkAsset
            // 
            this.chkAsset.AutoSize = true;
            this.chkAsset.Location = new System.Drawing.Point(115, 3);
            this.chkAsset.Name = "chkAsset";
            this.chkAsset.Size = new System.Drawing.Size(52, 17);
            this.chkAsset.TabIndex = 18;
            this.chkAsset.Text = "Asset";
            this.chkAsset.UseVisualStyleBackColor = true;
            // 
            // chkGMU
            // 
            this.chkGMU.AutoSize = true;
            this.chkGMU.Location = new System.Drawing.Point(227, 3);
            this.chkGMU.Name = "chkGMU";
            this.chkGMU.Size = new System.Drawing.Size(51, 17);
            this.chkGMU.TabIndex = 19;
            this.chkGMU.Text = "GMU";
            this.chkGMU.UseVisualStyleBackColor = true;
            // 
            // gp_VaultSettings
            // 
            this.gp_VaultSettings.Controls.Add(this.btnEditTransactionReason);
            this.gp_VaultSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gp_VaultSettings.Location = new System.Drawing.Point(3, 568);
            this.gp_VaultSettings.Name = "gp_VaultSettings";
            this.gp_VaultSettings.Size = new System.Drawing.Size(758, 57);
            this.gp_VaultSettings.TabIndex = 2;
            this.gp_VaultSettings.TabStop = false;
            this.gp_VaultSettings.Text = "Vault Settings";
            // 
            // btnEditTransactionReason
            // 
            this.btnEditTransactionReason.Location = new System.Drawing.Point(15, 20);
            this.btnEditTransactionReason.Name = "btnEditTransactionReason";
            this.btnEditTransactionReason.Size = new System.Drawing.Size(182, 29);
            this.btnEditTransactionReason.TabIndex = 0;
            this.btnEditTransactionReason.Text = "Edit Transaction Reason";
            this.btnEditTransactionReason.UseVisualStyleBackColor = true;
            this.btnEditTransactionReason.Click += new System.EventHandler(this.btnEditTransactionReason_Click);
            // 
            // tblFooter
            // 
            this.tblFooter.ColumnCount = 3;
            this.tblFooter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblFooter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 306F));
            this.tblFooter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tblFooter.Controls.Add(this.btnClose, 2, 0);
            this.tblFooter.Controls.Add(this.flowLayoutPanel1, 1, 0);
            this.tblFooter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblFooter.Location = new System.Drawing.Point(3, 650);
            this.tblFooter.Name = "tblFooter";
            this.tblFooter.RowCount = 1;
            this.tblFooter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblFooter.Size = new System.Drawing.Size(780, 39);
            this.tblFooter.TabIndex = 1;
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnClose.Location = new System.Drawing.Point(683, 8);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(94, 28);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.btnEdit);
            this.flowLayoutPanel1.Controls.Add(this.btnCancel);
            this.flowLayoutPanel1.Controls.Add(this.btnUpdate);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(377, 5);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(300, 31);
            this.flowLayoutPanel1.TabIndex = 1;
            // 
            // btnEdit
            // 
            this.btnEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEdit.Location = new System.Drawing.Point(203, 3);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(94, 28);
            this.btnEdit.TabIndex = 2;
            this.btnEdit.Text = "Edit";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(103, 3);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(94, 28);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Visible = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUpdate.Location = new System.Drawing.Point(3, 3);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(94, 28);
            this.btnUpdate.TabIndex = 0;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Visible = false;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // frmAdminSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(786, 692);
            this.ControlBox = false;
            this.Controls.Add(this.tblMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "frmAdminSettings";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "System Settings";
            this.tblMain.ResumeLayout(false);
            this.tblStock.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.tblInner.ResumeLayout(false);
            this.grpOtherSettings.ResumeLayout(false);
            this.tblOtherSettings.ResumeLayout(false);
            this.tblOtherSettings.PerformLayout();
            this.tblOtherSettingsRegion.ResumeLayout(false);
            this.tblOtherSettingsRegion.PerformLayout();
            this.grpAssetSettings.ResumeLayout(false);
            this.tblAssetDetails.ResumeLayout(false);
            this.tblAssetDetails.PerformLayout();
            this.grpAGSCombination.ResumeLayout(false);
            this.tblAGSCombination.ResumeLayout(false);
            this.tblAGSCombination.PerformLayout();
            this.gp_VaultSettings.ResumeLayout(false);
            this.tblFooter.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tblMain;
        private System.Windows.Forms.TableLayoutPanel tblFooter;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.TableLayoutPanel tblStock;
        private System.Windows.Forms.TableLayoutPanel tblInner;
        private System.Windows.Forms.GroupBox grpOtherSettings;
        private System.Windows.Forms.TableLayoutPanel tblOtherSettings;
        private System.Windows.Forms.CheckBox chkPowerPomoReportsRequired;
        private System.Windows.Forms.TableLayoutPanel tblOtherSettingsRegion;
        private System.Windows.Forms.ComboBox cmbCulture;
        private System.Windows.Forms.Label lblRegion;
        private System.Windows.Forms.CheckBox chkCentralizedDeclaration;
        private System.Windows.Forms.CheckBox chkOfflineDeclaration;
        private System.Windows.Forms.CheckBox chkAllowBPEnable;
        private System.Windows.Forms.CheckBox chkSiteLicensingRequired;
        private System.Windows.Forms.CheckBox chkEmployeecardTrackingRequired;
        private System.Windows.Forms.CheckBox chkImportExportAssetFile;
        private System.Windows.Forms.CheckBox chkValidateAGS;
        private System.Windows.Forms.CheckBox chkPartiallyConfigured;
        private System.Windows.Forms.CheckBox chkGameCappingEnabled;
        private System.Windows.Forms.CheckBox chkSuppressGroupByZone;
        private System.Windows.Forms.CheckBox chkIsSingleEmpCard;
        private System.Windows.Forms.CheckBox chkAddSPinVout;
        private System.Windows.Forms.GroupBox grpAssetSettings;
        private System.Windows.Forms.TableLayoutPanel tblAssetDetails;
        private System.Windows.Forms.ComboBox cmbUnallocatedMachine;
        private System.Windows.Forms.ComboBox cmbUnallocatedGame;
        private System.Windows.Forms.ComboBox cmbUnallocatedType;
        private System.Windows.Forms.TextBox txtAssetNumberMinLength;
        private System.Windows.Forms.TextBox txtAssetNumberPrefix;
        private System.Windows.Forms.CheckBox chkAutoGenAssetNumber;
        private System.Windows.Forms.CheckBox chkAllowBulkAssetPurchase;
        private System.Windows.Forms.CheckBox chkForceSiteRepforAsset;
        private System.Windows.Forms.CheckBox chkForceCodeToSite;
        private System.Windows.Forms.TextBox txtGameCodeMinLength;
        private System.Windows.Forms.TextBox txtGameCodePrefix;
        private System.Windows.Forms.Label lblGameCodePrefix;
        private System.Windows.Forms.CheckBox chkAutoGenGameCode;
        private System.Windows.Forms.Label lblAutoGenGameCode;
        private System.Windows.Forms.Label lblGameCodeLen;
        private System.Windows.Forms.Label lblAutoGenAssetNo;
        private System.Windows.Forms.Label lblAssetNoPrefix;
        private System.Windows.Forms.Label lblAssetNoLen;
        private System.Windows.Forms.Label lblUnallocatedType;
        private System.Windows.Forms.Label lblUnallocatedGame;
        private System.Windows.Forms.Label lblUnallocatedMachine;
        private System.Windows.Forms.Label lblForceAsset;
        private System.Windows.Forms.Label lblAllowBulkPurchase;
        private System.Windows.Forms.Label lblEnforceCodes;
        private System.Windows.Forms.GroupBox grpAGSCombination;
        private System.Windows.Forms.TableLayoutPanel tblAGSCombination;
        private System.Windows.Forms.CheckBox chkSerial;
        private System.Windows.Forms.CheckBox chkAsset;
        private System.Windows.Forms.CheckBox chkGMU;
        private System.Windows.Forms.CheckBox chkMailAlert;
        private System.Windows.Forms.CheckBox chkIsAlertEnabled;
        private System.Windows.Forms.CheckBox cb_SendMailFromEnterprise;
        private System.Windows.Forms.CheckBox cb_IsAutoCalendarEnabled;
        private System.Windows.Forms.CheckBox chkAllowMultipleDrops;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox gp_VaultSettings;
        private System.Windows.Forms.Button btnEditTransactionReason;
		 private System.Windows.Forms.CheckBox chkCancelPendingMails;
    }
}