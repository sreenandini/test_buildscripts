namespace BMC.UI.EnterpriseConfig
{
    partial class frmBMCEnterpriseConfig
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBMCEnterpriseConfig));
            this.panel1 = new System.Windows.Forms.Panel();
            this.gpKeysSetup = new System.Windows.Forms.GroupBox();
            this.btnCreateEnterpriseKey = new System.Windows.Forms.Button();
            this.gpReports = new System.Windows.Forms.GroupBox();
            this.lblmessage = new System.Windows.Forms.Label();
            this.lblReportFolder = new System.Windows.Forms.Label();
            this.btnDeployReports = new System.Windows.Forms.Button();
            this.txtReportFolder = new System.Windows.Forms.TextBox();
            this.txtReportServer = new System.Windows.Forms.TextBox();
            this.lblReportServer = new System.Windows.Forms.Label();
            this.gpAAMSSetup = new System.Windows.Forms.GroupBox();
            this.txtHBGSenderCode = new System.Windows.Forms.TextBox();
            this.lblHBGSenderCode = new System.Windows.Forms.Label();
            this.chkEnableAAMS = new System.Windows.Forms.CheckBox();
            this.btnTestURL = new System.Windows.Forms.Button();
            this.txtBASweburl = new System.Windows.Forms.TextBox();
            this.lblWebServiceUrl = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnRunUpgradeScript = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnSaveSettings = new System.Windows.Forms.Button();
            this.gpServiceSetup = new System.Windows.Forms.GroupBox();
            this.gpWindowsService = new System.Windows.Forms.GroupBox();
            this.btnSelectAll = new System.Windows.Forms.Button();
            this.btnRefreshServices = new System.Windows.Forms.Button();
            this.ProgressBarServices = new System.Windows.Forms.ProgressBar();
            this.btnEndService = new System.Windows.Forms.Button();
            this.lvServiceslist = new System.Windows.Forms.ListView();
            this.btnStartService = new System.Windows.Forms.Button();
            this.gpLGESetup = new System.Windows.Forms.GroupBox();
            this.chkEnableLGE = new System.Windows.Forms.CheckBox();
            this.gpLGECredentials = new System.Windows.Forms.GroupBox();
            this.txtLGEDB = new System.Windows.Forms.TextBox();
            this.txtLGEtimeout = new System.Windows.Forms.TextBox();
            this.lblLGETimeout = new System.Windows.Forms.Label();
            this.lblLGEDbname = new System.Windows.Forms.Label();
            this.txtLGEPassword = new System.Windows.Forms.TextBox();
            this.lblLGEPassword = new System.Windows.Forms.Label();
            this.txtLGEInstance = new System.Windows.Forms.TextBox();
            this.lblLGEInstance = new System.Windows.Forms.Label();
            this.txtLGEUsername = new System.Windows.Forms.TextBox();
            this.lblLGEUsername = new System.Windows.Forms.Label();
            this.txtLGEServer = new System.Windows.Forms.TextBox();
            this.lblLGEServer = new System.Windows.Forms.Label();
            this.gpLGEActions = new System.Windows.Forms.GroupBox();
            this.btnLGEGatewayTestConnection = new System.Windows.Forms.Button();
            this.gpMeterAnalysisSetup = new System.Windows.Forms.GroupBox();
            this.gpTktCredentials = new System.Windows.Forms.GroupBox();
            this.chkUseEnterpriseConnection = new System.Windows.Forms.CheckBox();
            this.lblMeterAnalysisdb = new System.Windows.Forms.Label();
            this.lblMeterAnalysisDBname = new System.Windows.Forms.Label();
            this.gpMeterAnalysisActions = new System.Windows.Forms.GroupBox();
            this.btnMeterAnalysisTestConnection = new System.Windows.Forms.Button();
            this.btnMeterAnalysisDBRestore = new System.Windows.Forms.Button();
            this.gpEnterpriseSetup = new System.Windows.Forms.GroupBox();
            this.gpExCredentials = new System.Windows.Forms.GroupBox();
            this.txtEnterpriseTimeOut = new System.Windows.Forms.TextBox();
            this.lblEnterpriseTimeOut = new System.Windows.Forms.Label();
            this.lblexDBName = new System.Windows.Forms.Label();
            this.lblEnterpriseDBname = new System.Windows.Forms.Label();
            this.txtenterprisePassword = new System.Windows.Forms.TextBox();
            this.lblenterprisePassword = new System.Windows.Forms.Label();
            this.txtenterpriseInstance = new System.Windows.Forms.TextBox();
            this.lblenterpriseInstance = new System.Windows.Forms.Label();
            this.txtenterpriseUsername = new System.Windows.Forms.TextBox();
            this.lblenterpriseUsername = new System.Windows.Forms.Label();
            this.txtenterpriseServer = new System.Windows.Forms.TextBox();
            this.lblenterpriseserver = new System.Windows.Forms.Label();
            this.gpenterpriseActions = new System.Windows.Forms.GroupBox();
            this.btnSaveEnterpriseConnection = new System.Windows.Forms.Button();
            this.btnEnterpriseTestConnection = new System.Windows.Forms.Button();
            this.btnenterpriseDBRestore = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.errValidate = new System.Windows.Forms.ErrorProvider(this.components);
            this.panel1.SuspendLayout();
            this.gpKeysSetup.SuspendLayout();
            this.gpReports.SuspendLayout();
            this.gpAAMSSetup.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.gpServiceSetup.SuspendLayout();
            this.gpWindowsService.SuspendLayout();
            this.gpLGESetup.SuspendLayout();
            this.gpLGECredentials.SuspendLayout();
            this.gpLGEActions.SuspendLayout();
            this.gpMeterAnalysisSetup.SuspendLayout();
            this.gpTktCredentials.SuspendLayout();
            this.gpMeterAnalysisActions.SuspendLayout();
            this.gpEnterpriseSetup.SuspendLayout();
            this.gpExCredentials.SuspendLayout();
            this.gpenterpriseActions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errValidate)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.gpKeysSetup);
            this.panel1.Controls.Add(this.gpReports);
            this.panel1.Controls.Add(this.gpAAMSSetup);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.gpServiceSetup);
            this.panel1.Controls.Add(this.gpLGESetup);
            this.panel1.Controls.Add(this.gpMeterAnalysisSetup);
            this.panel1.Controls.Add(this.gpEnterpriseSetup);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(995, 581);
            this.panel1.TabIndex = 0;
            // 
            // gpKeysSetup
            // 
            this.gpKeysSetup.Controls.Add(this.btnCreateEnterpriseKey);
            this.gpKeysSetup.Location = new System.Drawing.Point(4, 419);
            this.gpKeysSetup.Name = "gpKeysSetup";
            this.gpKeysSetup.Size = new System.Drawing.Size(482, 53);
            this.gpKeysSetup.TabIndex = 29;
            this.gpKeysSetup.TabStop = false;
            this.gpKeysSetup.Text = "Keys  Setup";
            // 
            // btnCreateEnterpriseKey
            // 
            this.btnCreateEnterpriseKey.BackColor = System.Drawing.Color.White;
            this.btnCreateEnterpriseKey.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCreateEnterpriseKey.Location = new System.Drawing.Point(87, 19);
            this.btnCreateEnterpriseKey.Name = "btnCreateEnterpriseKey";
            this.btnCreateEnterpriseKey.Size = new System.Drawing.Size(142, 25);
            this.btnCreateEnterpriseKey.TabIndex = 13;
            this.btnCreateEnterpriseKey.Text = "Create Enterprise Key";
            this.btnCreateEnterpriseKey.UseVisualStyleBackColor = false;
            this.btnCreateEnterpriseKey.Click += new System.EventHandler(this.btnCreateEnterpriseKey_Click);
            // 
            // gpReports
            // 
            this.gpReports.Controls.Add(this.lblmessage);
            this.gpReports.Controls.Add(this.lblReportFolder);
            this.gpReports.Controls.Add(this.btnDeployReports);
            this.gpReports.Controls.Add(this.txtReportFolder);
            this.gpReports.Controls.Add(this.txtReportServer);
            this.gpReports.Controls.Add(this.lblReportServer);
            this.gpReports.Location = new System.Drawing.Point(4, 282);
            this.gpReports.Name = "gpReports";
            this.gpReports.Size = new System.Drawing.Size(482, 131);
            this.gpReports.TabIndex = 28;
            this.gpReports.TabStop = false;
            this.gpReports.Text = "Reports Setup";
            // 
            // lblmessage
            // 
            this.lblmessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblmessage.ForeColor = System.Drawing.Color.Red;
            this.lblmessage.Location = new System.Drawing.Point(4, 52);
            this.lblmessage.Name = "lblmessage";
            this.lblmessage.Size = new System.Drawing.Size(397, 20);
            this.lblmessage.TabIndex = 49;
            this.lblmessage.Text = "[Deploy Reports before entering the Reports setting section]";
            // 
            // lblReportFolder
            // 
            this.lblReportFolder.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReportFolder.Location = new System.Drawing.Point(249, 77);
            this.lblReportFolder.Name = "lblReportFolder";
            this.lblReportFolder.Size = new System.Drawing.Size(107, 40);
            this.lblReportFolder.TabIndex = 14;
            this.lblReportFolder.Text = "Reports Deployed Folder";
            // 
            // btnDeployReports
            // 
            this.btnDeployReports.BackColor = System.Drawing.Color.White;
            this.btnDeployReports.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDeployReports.Location = new System.Drawing.Point(86, 19);
            this.btnDeployReports.Name = "btnDeployReports";
            this.btnDeployReports.Size = new System.Drawing.Size(143, 25);
            this.btnDeployReports.TabIndex = 13;
            this.btnDeployReports.Text = "Deploy Reports";
            this.btnDeployReports.UseVisualStyleBackColor = false;
            this.btnDeployReports.Click += new System.EventHandler(this.btnDeployReports_Click);
            // 
            // txtReportFolder
            // 
            this.txtReportFolder.BackColor = System.Drawing.Color.White;
            this.txtReportFolder.Location = new System.Drawing.Point(362, 89);
            this.txtReportFolder.Name = "txtReportFolder";
            this.txtReportFolder.Size = new System.Drawing.Size(115, 20);
            this.txtReportFolder.TabIndex = 1;
            // 
            // txtReportServer
            // 
            this.txtReportServer.BackColor = System.Drawing.Color.White;
            this.txtReportServer.Location = new System.Drawing.Point(95, 89);
            this.txtReportServer.Name = "txtReportServer";
            this.txtReportServer.Size = new System.Drawing.Size(136, 20);
            this.txtReportServer.TabIndex = 0;
            // 
            // lblReportServer
            // 
            this.lblReportServer.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReportServer.Location = new System.Drawing.Point(4, 81);
            this.lblReportServer.Name = "lblReportServer";
            this.lblReportServer.Size = new System.Drawing.Size(94, 36);
            this.lblReportServer.TabIndex = 8;
            this.lblReportServer.Text = "Report Server (IP/Name)";
            // 
            // gpAAMSSetup
            // 
            this.gpAAMSSetup.Controls.Add(this.txtHBGSenderCode);
            this.gpAAMSSetup.Controls.Add(this.lblHBGSenderCode);
            this.gpAAMSSetup.Controls.Add(this.chkEnableAAMS);
            this.gpAAMSSetup.Controls.Add(this.btnTestURL);
            this.gpAAMSSetup.Controls.Add(this.txtBASweburl);
            this.gpAAMSSetup.Controls.Add(this.lblWebServiceUrl);
            this.gpAAMSSetup.Location = new System.Drawing.Point(492, 212);
            this.gpAAMSSetup.Name = "gpAAMSSetup";
            this.gpAAMSSetup.Size = new System.Drawing.Size(482, 105);
            this.gpAAMSSetup.TabIndex = 27;
            this.gpAAMSSetup.TabStop = false;
            this.gpAAMSSetup.Text = "AAMS Connection Setup";
            // 
            // txtHBGSenderCode
            // 
            this.txtHBGSenderCode.BackColor = System.Drawing.Color.White;
            this.txtHBGSenderCode.Location = new System.Drawing.Point(335, 15);
            this.txtHBGSenderCode.Name = "txtHBGSenderCode";
            this.txtHBGSenderCode.Size = new System.Drawing.Size(136, 20);
            this.txtHBGSenderCode.TabIndex = 19;
            // 
            // lblHBGSenderCode
            // 
            this.lblHBGSenderCode.AutoSize = true;
            this.lblHBGSenderCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHBGSenderCode.Location = new System.Drawing.Point(210, 20);
            this.lblHBGSenderCode.Name = "lblHBGSenderCode";
            this.lblHBGSenderCode.Size = new System.Drawing.Size(110, 13);
            this.lblHBGSenderCode.TabIndex = 20;
            this.lblHBGSenderCode.Text = "HBG Sender Code";
            // 
            // chkEnableAAMS
            // 
            this.chkEnableAAMS.AutoSize = true;
            this.chkEnableAAMS.BackColor = System.Drawing.Color.White;
            this.chkEnableAAMS.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkEnableAAMS.ForeColor = System.Drawing.Color.Navy;
            this.chkEnableAAMS.Location = new System.Drawing.Point(9, 19);
            this.chkEnableAAMS.Name = "chkEnableAAMS";
            this.chkEnableAAMS.Size = new System.Drawing.Size(103, 17);
            this.chkEnableAAMS.TabIndex = 18;
            this.chkEnableAAMS.Text = "Enable AAMS";
            this.chkEnableAAMS.UseVisualStyleBackColor = false;
            // 
            // btnTestURL
            // 
            this.btnTestURL.BackColor = System.Drawing.Color.White;
            this.btnTestURL.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTestURL.Location = new System.Drawing.Point(118, 74);
            this.btnTestURL.Name = "btnTestURL";
            this.btnTestURL.Size = new System.Drawing.Size(94, 25);
            this.btnTestURL.TabIndex = 1;
            this.btnTestURL.Text = "Test Url";
            this.btnTestURL.UseVisualStyleBackColor = false;
            // 
            // txtBASweburl
            // 
            this.txtBASweburl.BackColor = System.Drawing.Color.White;
            this.txtBASweburl.Location = new System.Drawing.Point(118, 48);
            this.txtBASweburl.Name = "txtBASweburl";
            this.txtBASweburl.Size = new System.Drawing.Size(351, 20);
            this.txtBASweburl.TabIndex = 0;
            // 
            // lblWebServiceUrl
            // 
            this.lblWebServiceUrl.AutoSize = true;
            this.lblWebServiceUrl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWebServiceUrl.Location = new System.Drawing.Point(6, 51);
            this.lblWebServiceUrl.Name = "lblWebServiceUrl";
            this.lblWebServiceUrl.Size = new System.Drawing.Size(92, 13);
            this.lblWebServiceUrl.TabIndex = 17;
            this.lblWebServiceUrl.Text = "BAS Server Url";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnRunUpgradeScript);
            this.groupBox2.Controls.Add(this.btnExit);
            this.groupBox2.Controls.Add(this.btnSaveSettings);
            this.groupBox2.Location = new System.Drawing.Point(3, 521);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(971, 47);
            this.groupBox2.TabIndex = 25;
            this.groupBox2.TabStop = false;
            // 
            // btnRunUpgradeScript
            // 
            this.btnRunUpgradeScript.BackColor = System.Drawing.Color.White;
            this.btnRunUpgradeScript.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRunUpgradeScript.Location = new System.Drawing.Point(421, 14);
            this.btnRunUpgradeScript.Name = "btnRunUpgradeScript";
            this.btnRunUpgradeScript.Size = new System.Drawing.Size(143, 25);
            this.btnRunUpgradeScript.TabIndex = 25;
            this.btnRunUpgradeScript.Text = "Run Script";
            this.btnRunUpgradeScript.UseVisualStyleBackColor = false;
            this.btnRunUpgradeScript.Click += new System.EventHandler(this.btnRunUpgradeScript_Click);
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.White;
            this.btnExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.Location = new System.Drawing.Point(752, 14);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(143, 25);
            this.btnExit.TabIndex = 26;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnSaveSettings
            // 
            this.btnSaveSettings.BackColor = System.Drawing.Color.White;
            this.btnSaveSettings.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveSettings.Location = new System.Drawing.Point(87, 14);
            this.btnSaveSettings.Name = "btnSaveSettings";
            this.btnSaveSettings.Size = new System.Drawing.Size(143, 25);
            this.btnSaveSettings.TabIndex = 24;
            this.btnSaveSettings.Text = "Save Settings";
            this.btnSaveSettings.UseVisualStyleBackColor = false;
            this.btnSaveSettings.Click += new System.EventHandler(this.btnSaveSettings_Click);
            // 
            // gpServiceSetup
            // 
            this.gpServiceSetup.Controls.Add(this.gpWindowsService);
            this.gpServiceSetup.Location = new System.Drawing.Point(492, 323);
            this.gpServiceSetup.Name = "gpServiceSetup";
            this.gpServiceSetup.Size = new System.Drawing.Size(482, 192);
            this.gpServiceSetup.TabIndex = 6;
            this.gpServiceSetup.TabStop = false;
            this.gpServiceSetup.Text = " Service Setup";
            // 
            // gpWindowsService
            // 
            this.gpWindowsService.Controls.Add(this.btnSelectAll);
            this.gpWindowsService.Controls.Add(this.btnRefreshServices);
            this.gpWindowsService.Controls.Add(this.ProgressBarServices);
            this.gpWindowsService.Controls.Add(this.btnEndService);
            this.gpWindowsService.Controls.Add(this.lvServiceslist);
            this.gpWindowsService.Controls.Add(this.btnStartService);
            this.gpWindowsService.Location = new System.Drawing.Point(6, 19);
            this.gpWindowsService.Name = "gpWindowsService";
            this.gpWindowsService.Size = new System.Drawing.Size(457, 168);
            this.gpWindowsService.TabIndex = 2;
            this.gpWindowsService.TabStop = false;
            this.gpWindowsService.Text = "Windows";
            // 
            // btnSelectAll
            // 
            this.btnSelectAll.BackColor = System.Drawing.Color.White;
            this.btnSelectAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSelectAll.Location = new System.Drawing.Point(327, 108);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new System.Drawing.Size(125, 25);
            this.btnSelectAll.TabIndex = 10;
            this.btnSelectAll.Text = "Select All";
            this.btnSelectAll.UseVisualStyleBackColor = false;
            this.btnSelectAll.Click += new System.EventHandler(this.btnSelectAll_Click);
            // 
            // btnRefreshServices
            // 
            this.btnRefreshServices.BackColor = System.Drawing.Color.White;
            this.btnRefreshServices.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefreshServices.Location = new System.Drawing.Point(328, 77);
            this.btnRefreshServices.Name = "btnRefreshServices";
            this.btnRefreshServices.Size = new System.Drawing.Size(125, 25);
            this.btnRefreshServices.TabIndex = 9;
            this.btnRefreshServices.Text = "Refresh All Services";
            this.btnRefreshServices.UseVisualStyleBackColor = false;
            this.btnRefreshServices.Click += new System.EventHandler(this.btnRefreshServices_Click);
            // 
            // ProgressBarServices
            // 
            this.ProgressBarServices.Location = new System.Drawing.Point(7, 139);
            this.ProgressBarServices.Name = "ProgressBarServices";
            this.ProgressBarServices.Size = new System.Drawing.Size(444, 23);
            this.ProgressBarServices.TabIndex = 8;
            // 
            // btnEndService
            // 
            this.btnEndService.BackColor = System.Drawing.Color.White;
            this.btnEndService.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEndService.Location = new System.Drawing.Point(326, 46);
            this.btnEndService.Name = "btnEndService";
            this.btnEndService.Size = new System.Drawing.Size(125, 25);
            this.btnEndService.TabIndex = 2;
            this.btnEndService.Text = "Stop Service(s)";
            this.btnEndService.UseVisualStyleBackColor = false;
            this.btnEndService.Click += new System.EventHandler(this.btnEndService_Click);
            // 
            // lvServiceslist
            // 
            this.lvServiceslist.BackColor = System.Drawing.Color.GhostWhite;
            this.lvServiceslist.ForeColor = System.Drawing.Color.Red;
            this.lvServiceslist.FullRowSelect = true;
            this.lvServiceslist.Location = new System.Drawing.Point(6, 15);
            this.lvServiceslist.Name = "lvServiceslist";
            this.lvServiceslist.Size = new System.Drawing.Size(314, 118);
            this.lvServiceslist.TabIndex = 3;
            this.lvServiceslist.UseCompatibleStateImageBehavior = false;
            // 
            // btnStartService
            // 
            this.btnStartService.BackColor = System.Drawing.Color.White;
            this.btnStartService.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStartService.Location = new System.Drawing.Point(328, 15);
            this.btnStartService.Name = "btnStartService";
            this.btnStartService.Size = new System.Drawing.Size(125, 25);
            this.btnStartService.TabIndex = 1;
            this.btnStartService.Text = "Start Service(s)";
            this.btnStartService.UseVisualStyleBackColor = false;
            this.btnStartService.Click += new System.EventHandler(this.btnStartService_Click);
            // 
            // gpLGESetup
            // 
            this.gpLGESetup.Controls.Add(this.chkEnableLGE);
            this.gpLGESetup.Controls.Add(this.gpLGECredentials);
            this.gpLGESetup.Controls.Add(this.gpLGEActions);
            this.gpLGESetup.Location = new System.Drawing.Point(492, 8);
            this.gpLGESetup.Name = "gpLGESetup";
            this.gpLGESetup.Size = new System.Drawing.Size(482, 198);
            this.gpLGESetup.TabIndex = 3;
            this.gpLGESetup.TabStop = false;
            this.gpLGESetup.Text = "LGE Connection Setup";
            // 
            // chkEnableLGE
            // 
            this.chkEnableLGE.AutoSize = true;
            this.chkEnableLGE.BackColor = System.Drawing.Color.White;
            this.chkEnableLGE.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkEnableLGE.ForeColor = System.Drawing.Color.Navy;
            this.chkEnableLGE.Location = new System.Drawing.Point(10, 23);
            this.chkEnableLGE.Name = "chkEnableLGE";
            this.chkEnableLGE.Size = new System.Drawing.Size(93, 17);
            this.chkEnableLGE.TabIndex = 16;
            this.chkEnableLGE.Text = "Enable LGE";
            this.chkEnableLGE.UseVisualStyleBackColor = false;
            this.chkEnableLGE.CheckedChanged += new System.EventHandler(this.chkEnableLGE_CheckedChanged);
            // 
            // gpLGECredentials
            // 
            this.gpLGECredentials.Controls.Add(this.txtLGEDB);
            this.gpLGECredentials.Controls.Add(this.txtLGEtimeout);
            this.gpLGECredentials.Controls.Add(this.lblLGETimeout);
            this.gpLGECredentials.Controls.Add(this.lblLGEDbname);
            this.gpLGECredentials.Controls.Add(this.txtLGEPassword);
            this.gpLGECredentials.Controls.Add(this.lblLGEPassword);
            this.gpLGECredentials.Controls.Add(this.txtLGEInstance);
            this.gpLGECredentials.Controls.Add(this.lblLGEInstance);
            this.gpLGECredentials.Controls.Add(this.txtLGEUsername);
            this.gpLGECredentials.Controls.Add(this.lblLGEUsername);
            this.gpLGECredentials.Controls.Add(this.txtLGEServer);
            this.gpLGECredentials.Controls.Add(this.lblLGEServer);
            this.gpLGECredentials.Enabled = false;
            this.gpLGECredentials.Location = new System.Drawing.Point(6, 46);
            this.gpLGECredentials.Name = "gpLGECredentials";
            this.gpLGECredentials.Size = new System.Drawing.Size(471, 103);
            this.gpLGECredentials.TabIndex = 4;
            this.gpLGECredentials.TabStop = false;
            this.gpLGECredentials.Text = "LGE Connection Setup";
            // 
            // txtLGEDB
            // 
            this.txtLGEDB.BackColor = System.Drawing.Color.White;
            this.txtLGEDB.Location = new System.Drawing.Point(70, 72);
            this.txtLGEDB.Name = "txtLGEDB";
            this.txtLGEDB.PasswordChar = '*';
            this.txtLGEDB.Size = new System.Drawing.Size(136, 20);
            this.txtLGEDB.TabIndex = 19;
            // 
            // txtLGEtimeout
            // 
            this.txtLGEtimeout.BackColor = System.Drawing.Color.White;
            this.txtLGEtimeout.Location = new System.Drawing.Point(329, 72);
            this.txtLGEtimeout.Name = "txtLGEtimeout";
            this.txtLGEtimeout.Size = new System.Drawing.Size(64, 20);
            this.txtLGEtimeout.TabIndex = 4;
            this.txtLGEtimeout.Text = "30";
            // 
            // lblLGETimeout
            // 
            this.lblLGETimeout.AutoSize = true;
            this.lblLGETimeout.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLGETimeout.Location = new System.Drawing.Point(212, 77);
            this.lblLGETimeout.Name = "lblLGETimeout";
            this.lblLGETimeout.Size = new System.Drawing.Size(119, 13);
            this.lblLGETimeout.TabIndex = 18;
            this.lblLGETimeout.Text = "Time Out (Seconds)";
            // 
            // lblLGEDbname
            // 
            this.lblLGEDbname.AutoSize = true;
            this.lblLGEDbname.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLGEDbname.Location = new System.Drawing.Point(7, 77);
            this.lblLGEDbname.Name = "lblLGEDbname";
            this.lblLGEDbname.Size = new System.Drawing.Size(60, 13);
            this.lblLGEDbname.TabIndex = 16;
            this.lblLGEDbname.Text = "DB Name";
            // 
            // txtLGEPassword
            // 
            this.txtLGEPassword.BackColor = System.Drawing.Color.White;
            this.txtLGEPassword.Location = new System.Drawing.Point(329, 46);
            this.txtLGEPassword.Name = "txtLGEPassword";
            this.txtLGEPassword.PasswordChar = '*';
            this.txtLGEPassword.Size = new System.Drawing.Size(136, 20);
            this.txtLGEPassword.TabIndex = 3;
            // 
            // lblLGEPassword
            // 
            this.lblLGEPassword.AutoSize = true;
            this.lblLGEPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLGEPassword.Location = new System.Drawing.Point(248, 49);
            this.lblLGEPassword.Name = "lblLGEPassword";
            this.lblLGEPassword.Size = new System.Drawing.Size(61, 13);
            this.lblLGEPassword.TabIndex = 14;
            this.lblLGEPassword.Text = "Password";
            // 
            // txtLGEInstance
            // 
            this.txtLGEInstance.BackColor = System.Drawing.Color.White;
            this.txtLGEInstance.Location = new System.Drawing.Point(329, 17);
            this.txtLGEInstance.Name = "txtLGEInstance";
            this.txtLGEInstance.Size = new System.Drawing.Size(136, 20);
            this.txtLGEInstance.TabIndex = 1;
            // 
            // lblLGEInstance
            // 
            this.lblLGEInstance.AutoSize = true;
            this.lblLGEInstance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLGEInstance.Location = new System.Drawing.Point(253, 17);
            this.lblLGEInstance.Name = "lblLGEInstance";
            this.lblLGEInstance.Size = new System.Drawing.Size(56, 13);
            this.lblLGEInstance.TabIndex = 12;
            this.lblLGEInstance.Text = "Instance";
            // 
            // txtLGEUsername
            // 
            this.txtLGEUsername.BackColor = System.Drawing.Color.White;
            this.txtLGEUsername.Location = new System.Drawing.Point(70, 46);
            this.txtLGEUsername.Name = "txtLGEUsername";
            this.txtLGEUsername.Size = new System.Drawing.Size(136, 20);
            this.txtLGEUsername.TabIndex = 2;
            // 
            // lblLGEUsername
            // 
            this.lblLGEUsername.AutoSize = true;
            this.lblLGEUsername.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLGEUsername.Location = new System.Drawing.Point(4, 49);
            this.lblLGEUsername.Name = "lblLGEUsername";
            this.lblLGEUsername.Size = new System.Drawing.Size(63, 13);
            this.lblLGEUsername.TabIndex = 10;
            this.lblLGEUsername.Text = "Username";
            // 
            // txtLGEServer
            // 
            this.txtLGEServer.BackColor = System.Drawing.Color.White;
            this.txtLGEServer.Location = new System.Drawing.Point(70, 14);
            this.txtLGEServer.Name = "txtLGEServer";
            this.txtLGEServer.Size = new System.Drawing.Size(136, 20);
            this.txtLGEServer.TabIndex = 0;
            // 
            // lblLGEServer
            // 
            this.lblLGEServer.AutoSize = true;
            this.lblLGEServer.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLGEServer.Location = new System.Drawing.Point(4, 17);
            this.lblLGEServer.Name = "lblLGEServer";
            this.lblLGEServer.Size = new System.Drawing.Size(44, 13);
            this.lblLGEServer.TabIndex = 8;
            this.lblLGEServer.Text = "Server";
            // 
            // gpLGEActions
            // 
            this.gpLGEActions.Controls.Add(this.btnLGEGatewayTestConnection);
            this.gpLGEActions.Location = new System.Drawing.Point(6, 155);
            this.gpLGEActions.Name = "gpLGEActions";
            this.gpLGEActions.Size = new System.Drawing.Size(471, 40);
            this.gpLGEActions.TabIndex = 5;
            this.gpLGEActions.TabStop = false;
            this.gpLGEActions.Text = "Actions";
            // 
            // btnLGEGatewayTestConnection
            // 
            this.btnLGEGatewayTestConnection.BackColor = System.Drawing.Color.White;
            this.btnLGEGatewayTestConnection.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLGEGatewayTestConnection.Location = new System.Drawing.Point(70, 9);
            this.btnLGEGatewayTestConnection.Name = "btnLGEGatewayTestConnection";
            this.btnLGEGatewayTestConnection.Size = new System.Drawing.Size(117, 25);
            this.btnLGEGatewayTestConnection.TabIndex = 1;
            this.btnLGEGatewayTestConnection.Text = "Test Connection";
            this.btnLGEGatewayTestConnection.UseVisualStyleBackColor = false;
            this.btnLGEGatewayTestConnection.Click += new System.EventHandler(this.btnLGEGatewayTestConnection_Click);
            // 
            // gpMeterAnalysisSetup
            // 
            this.gpMeterAnalysisSetup.Controls.Add(this.gpTktCredentials);
            this.gpMeterAnalysisSetup.Controls.Add(this.gpMeterAnalysisActions);
            this.gpMeterAnalysisSetup.Location = new System.Drawing.Point(4, 177);
            this.gpMeterAnalysisSetup.Name = "gpMeterAnalysisSetup";
            this.gpMeterAnalysisSetup.Size = new System.Drawing.Size(482, 99);
            this.gpMeterAnalysisSetup.TabIndex = 2;
            this.gpMeterAnalysisSetup.TabStop = false;
            this.gpMeterAnalysisSetup.Text = "MeterAnalysis Connection Setup";
            // 
            // gpTktCredentials
            // 
            this.gpTktCredentials.Controls.Add(this.chkUseEnterpriseConnection);
            this.gpTktCredentials.Controls.Add(this.lblMeterAnalysisdb);
            this.gpTktCredentials.Controls.Add(this.lblMeterAnalysisDBname);
            this.gpTktCredentials.Location = new System.Drawing.Point(6, 14);
            this.gpTktCredentials.Name = "gpTktCredentials";
            this.gpTktCredentials.Size = new System.Drawing.Size(471, 36);
            this.gpTktCredentials.TabIndex = 2;
            this.gpTktCredentials.TabStop = false;
            // 
            // chkUseEnterpriseConnection
            // 
            this.chkUseEnterpriseConnection.AutoSize = true;
            this.chkUseEnterpriseConnection.BackColor = System.Drawing.Color.White;
            this.chkUseEnterpriseConnection.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkUseEnterpriseConnection.ForeColor = System.Drawing.Color.Navy;
            this.chkUseEnterpriseConnection.Location = new System.Drawing.Point(258, 10);
            this.chkUseEnterpriseConnection.Name = "chkUseEnterpriseConnection";
            this.chkUseEnterpriseConnection.Size = new System.Drawing.Size(177, 17);
            this.chkUseEnterpriseConnection.TabIndex = 18;
            this.chkUseEnterpriseConnection.Text = "Use Enterprise Connection";
            this.chkUseEnterpriseConnection.UseVisualStyleBackColor = false;
            this.chkUseEnterpriseConnection.CheckedChanged += new System.EventHandler(this.chkUseEnterpriseConnection_CheckedChanged);
            // 
            // lblMeterAnalysisdb
            // 
            this.lblMeterAnalysisdb.AutoSize = true;
            this.lblMeterAnalysisdb.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMeterAnalysisdb.Location = new System.Drawing.Point(6, 16);
            this.lblMeterAnalysisdb.Name = "lblMeterAnalysisdb";
            this.lblMeterAnalysisdb.Size = new System.Drawing.Size(60, 13);
            this.lblMeterAnalysisdb.TabIndex = 16;
            this.lblMeterAnalysisdb.Text = "DB Name";
            // 
            // lblMeterAnalysisDBname
            // 
            this.lblMeterAnalysisDBname.AutoSize = true;
            this.lblMeterAnalysisDBname.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMeterAnalysisDBname.Location = new System.Drawing.Point(77, 16);
            this.lblMeterAnalysisDBname.Name = "lblMeterAnalysisDBname";
            this.lblMeterAnalysisDBname.Size = new System.Drawing.Size(85, 13);
            this.lblMeterAnalysisDBname.TabIndex = 17;
            this.lblMeterAnalysisDBname.Text = "MeterAnalysis";
            // 
            // gpMeterAnalysisActions
            // 
            this.gpMeterAnalysisActions.Controls.Add(this.btnMeterAnalysisTestConnection);
            this.gpMeterAnalysisActions.Controls.Add(this.btnMeterAnalysisDBRestore);
            this.gpMeterAnalysisActions.Location = new System.Drawing.Point(6, 50);
            this.gpMeterAnalysisActions.Name = "gpMeterAnalysisActions";
            this.gpMeterAnalysisActions.Size = new System.Drawing.Size(471, 40);
            this.gpMeterAnalysisActions.TabIndex = 3;
            this.gpMeterAnalysisActions.TabStop = false;
            this.gpMeterAnalysisActions.Text = "Actions";
            // 
            // btnMeterAnalysisTestConnection
            // 
            this.btnMeterAnalysisTestConnection.BackColor = System.Drawing.Color.White;
            this.btnMeterAnalysisTestConnection.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMeterAnalysisTestConnection.Location = new System.Drawing.Point(200, 10);
            this.btnMeterAnalysisTestConnection.Name = "btnMeterAnalysisTestConnection";
            this.btnMeterAnalysisTestConnection.Size = new System.Drawing.Size(117, 25);
            this.btnMeterAnalysisTestConnection.TabIndex = 1;
            this.btnMeterAnalysisTestConnection.Text = "Test Connection";
            this.btnMeterAnalysisTestConnection.UseVisualStyleBackColor = false;
            this.btnMeterAnalysisTestConnection.Click += new System.EventHandler(this.btnMeterAnalysisTestConnection_Click);
            // 
            // btnMeterAnalysisDBRestore
            // 
            this.btnMeterAnalysisDBRestore.BackColor = System.Drawing.Color.White;
            this.btnMeterAnalysisDBRestore.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMeterAnalysisDBRestore.Location = new System.Drawing.Point(323, 10);
            this.btnMeterAnalysisDBRestore.Name = "btnMeterAnalysisDBRestore";
            this.btnMeterAnalysisDBRestore.Size = new System.Drawing.Size(94, 25);
            this.btnMeterAnalysisDBRestore.TabIndex = 2;
            this.btnMeterAnalysisDBRestore.Text = "Restore DB";
            this.btnMeterAnalysisDBRestore.UseVisualStyleBackColor = false;
            this.btnMeterAnalysisDBRestore.Click += new System.EventHandler(this.btnTicketDBRestore_Click);
            // 
            // gpEnterpriseSetup
            // 
            this.gpEnterpriseSetup.Controls.Add(this.gpExCredentials);
            this.gpEnterpriseSetup.Controls.Add(this.gpenterpriseActions);
            this.gpEnterpriseSetup.Location = new System.Drawing.Point(3, 0);
            this.gpEnterpriseSetup.Name = "gpEnterpriseSetup";
            this.gpEnterpriseSetup.Size = new System.Drawing.Size(482, 168);
            this.gpEnterpriseSetup.TabIndex = 1;
            this.gpEnterpriseSetup.TabStop = false;
            this.gpEnterpriseSetup.Text = "Enterprise Server Connection Setup";
            // 
            // gpExCredentials
            // 
            this.gpExCredentials.Controls.Add(this.txtEnterpriseTimeOut);
            this.gpExCredentials.Controls.Add(this.lblEnterpriseTimeOut);
            this.gpExCredentials.Controls.Add(this.lblexDBName);
            this.gpExCredentials.Controls.Add(this.lblEnterpriseDBname);
            this.gpExCredentials.Controls.Add(this.txtenterprisePassword);
            this.gpExCredentials.Controls.Add(this.lblenterprisePassword);
            this.gpExCredentials.Controls.Add(this.txtenterpriseInstance);
            this.gpExCredentials.Controls.Add(this.lblenterpriseInstance);
            this.gpExCredentials.Controls.Add(this.txtenterpriseUsername);
            this.gpExCredentials.Controls.Add(this.lblenterpriseUsername);
            this.gpExCredentials.Controls.Add(this.txtenterpriseServer);
            this.gpExCredentials.Controls.Add(this.lblenterpriseserver);
            this.gpExCredentials.Location = new System.Drawing.Point(6, 14);
            this.gpExCredentials.Name = "gpExCredentials";
            this.gpExCredentials.Size = new System.Drawing.Size(471, 102);
            this.gpExCredentials.TabIndex = 2;
            this.gpExCredentials.TabStop = false;
            // 
            // txtEnterpriseTimeOut
            // 
            this.txtEnterpriseTimeOut.Location = new System.Drawing.Point(319, 73);
            this.txtEnterpriseTimeOut.Name = "txtEnterpriseTimeOut";
            this.txtEnterpriseTimeOut.Size = new System.Drawing.Size(64, 20);
            this.txtEnterpriseTimeOut.TabIndex = 8;
            this.txtEnterpriseTimeOut.Text = "30";
            // 
            // lblEnterpriseTimeOut
            // 
            this.lblEnterpriseTimeOut.AutoSize = true;
            this.lblEnterpriseTimeOut.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEnterpriseTimeOut.Location = new System.Drawing.Point(198, 76);
            this.lblEnterpriseTimeOut.Name = "lblEnterpriseTimeOut";
            this.lblEnterpriseTimeOut.Size = new System.Drawing.Size(119, 13);
            this.lblEnterpriseTimeOut.TabIndex = 18;
            this.lblEnterpriseTimeOut.Text = "Time Out (Seconds)";
            // 
            // lblexDBName
            // 
            this.lblexDBName.AutoSize = true;
            this.lblexDBName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblexDBName.Location = new System.Drawing.Point(3, 76);
            this.lblexDBName.Name = "lblexDBName";
            this.lblexDBName.Size = new System.Drawing.Size(60, 13);
            this.lblexDBName.TabIndex = 16;
            this.lblexDBName.Text = "DB Name";
            // 
            // lblEnterpriseDBname
            // 
            this.lblEnterpriseDBname.AutoSize = true;
            this.lblEnterpriseDBname.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEnterpriseDBname.Location = new System.Drawing.Point(74, 76);
            this.lblEnterpriseDBname.Name = "lblEnterpriseDBname";
            this.lblEnterpriseDBname.Size = new System.Drawing.Size(64, 13);
            this.lblEnterpriseDBname.TabIndex = 17;
            this.lblEnterpriseDBname.Text = "Enterprise";
            // 
            // txtenterprisePassword
            // 
            this.txtenterprisePassword.BackColor = System.Drawing.Color.White;
            this.txtenterprisePassword.Location = new System.Drawing.Point(319, 45);
            this.txtenterprisePassword.Name = "txtenterprisePassword";
            this.txtenterprisePassword.PasswordChar = '*';
            this.txtenterprisePassword.Size = new System.Drawing.Size(136, 20);
            this.txtenterprisePassword.TabIndex = 7;
            // 
            // lblenterprisePassword
            // 
            this.lblenterprisePassword.AutoSize = true;
            this.lblenterprisePassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblenterprisePassword.Location = new System.Drawing.Point(256, 48);
            this.lblenterprisePassword.Name = "lblenterprisePassword";
            this.lblenterprisePassword.Size = new System.Drawing.Size(61, 13);
            this.lblenterprisePassword.TabIndex = 14;
            this.lblenterprisePassword.Text = "Password";
            // 
            // txtenterpriseInstance
            // 
            this.txtenterpriseInstance.BackColor = System.Drawing.Color.White;
            this.txtenterpriseInstance.Location = new System.Drawing.Point(319, 14);
            this.txtenterpriseInstance.Name = "txtenterpriseInstance";
            this.txtenterpriseInstance.Size = new System.Drawing.Size(136, 20);
            this.txtenterpriseInstance.TabIndex = 5;
            // 
            // lblenterpriseInstance
            // 
            this.lblenterpriseInstance.AutoSize = true;
            this.lblenterpriseInstance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblenterpriseInstance.Location = new System.Drawing.Point(261, 17);
            this.lblenterpriseInstance.Name = "lblenterpriseInstance";
            this.lblenterpriseInstance.Size = new System.Drawing.Size(56, 13);
            this.lblenterpriseInstance.TabIndex = 12;
            this.lblenterpriseInstance.Text = "Instance";
            // 
            // txtenterpriseUsername
            // 
            this.txtenterpriseUsername.BackColor = System.Drawing.Color.White;
            this.txtenterpriseUsername.Location = new System.Drawing.Point(77, 45);
            this.txtenterpriseUsername.Name = "txtenterpriseUsername";
            this.txtenterpriseUsername.Size = new System.Drawing.Size(136, 20);
            this.txtenterpriseUsername.TabIndex = 6;
            // 
            // lblenterpriseUsername
            // 
            this.lblenterpriseUsername.AutoSize = true;
            this.lblenterpriseUsername.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblenterpriseUsername.Location = new System.Drawing.Point(4, 48);
            this.lblenterpriseUsername.Name = "lblenterpriseUsername";
            this.lblenterpriseUsername.Size = new System.Drawing.Size(63, 13);
            this.lblenterpriseUsername.TabIndex = 10;
            this.lblenterpriseUsername.Text = "Username";
            // 
            // txtenterpriseServer
            // 
            this.txtenterpriseServer.BackColor = System.Drawing.Color.White;
            this.txtenterpriseServer.Location = new System.Drawing.Point(77, 14);
            this.txtenterpriseServer.Name = "txtenterpriseServer";
            this.txtenterpriseServer.Size = new System.Drawing.Size(136, 20);
            this.txtenterpriseServer.TabIndex = 4;
            // 
            // lblenterpriseserver
            // 
            this.lblenterpriseserver.AutoSize = true;
            this.lblenterpriseserver.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblenterpriseserver.Location = new System.Drawing.Point(4, 17);
            this.lblenterpriseserver.Name = "lblenterpriseserver";
            this.lblenterpriseserver.Size = new System.Drawing.Size(44, 13);
            this.lblenterpriseserver.TabIndex = 8;
            this.lblenterpriseserver.Text = "Server";
            // 
            // gpenterpriseActions
            // 
            this.gpenterpriseActions.Controls.Add(this.btnSaveEnterpriseConnection);
            this.gpenterpriseActions.Controls.Add(this.btnEnterpriseTestConnection);
            this.gpenterpriseActions.Controls.Add(this.btnenterpriseDBRestore);
            this.gpenterpriseActions.Location = new System.Drawing.Point(5, 120);
            this.gpenterpriseActions.Name = "gpenterpriseActions";
            this.gpenterpriseActions.Size = new System.Drawing.Size(471, 42);
            this.gpenterpriseActions.TabIndex = 3;
            this.gpenterpriseActions.TabStop = false;
            this.gpenterpriseActions.Text = "Actions";
            // 
            // btnSaveEnterpriseConnection
            // 
            this.btnSaveEnterpriseConnection.BackColor = System.Drawing.Color.White;
            this.btnSaveEnterpriseConnection.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveEnterpriseConnection.Location = new System.Drawing.Point(195, 9);
            this.btnSaveEnterpriseConnection.Name = "btnSaveEnterpriseConnection";
            this.btnSaveEnterpriseConnection.Size = new System.Drawing.Size(122, 25);
            this.btnSaveEnterpriseConnection.TabIndex = 1;
            this.btnSaveEnterpriseConnection.Text = "Save  Connection";
            this.btnSaveEnterpriseConnection.UseVisualStyleBackColor = false;
            this.btnSaveEnterpriseConnection.Click += new System.EventHandler(this.btnSaveEnterpriseConnection_Click);
            // 
            // btnEnterpriseTestConnection
            // 
            this.btnEnterpriseTestConnection.BackColor = System.Drawing.Color.White;
            this.btnEnterpriseTestConnection.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEnterpriseTestConnection.Location = new System.Drawing.Point(75, 9);
            this.btnEnterpriseTestConnection.Name = "btnEnterpriseTestConnection";
            this.btnEnterpriseTestConnection.Size = new System.Drawing.Size(114, 25);
            this.btnEnterpriseTestConnection.TabIndex = 0;
            this.btnEnterpriseTestConnection.Text = "Test Connection";
            this.btnEnterpriseTestConnection.UseVisualStyleBackColor = false;
            this.btnEnterpriseTestConnection.Click += new System.EventHandler(this.btnEnterpriseTestConnection_Click);
            // 
            // btnenterpriseDBRestore
            // 
            this.btnenterpriseDBRestore.BackColor = System.Drawing.Color.White;
            this.btnenterpriseDBRestore.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnenterpriseDBRestore.Location = new System.Drawing.Point(323, 9);
            this.btnenterpriseDBRestore.Name = "btnenterpriseDBRestore";
            this.btnenterpriseDBRestore.Size = new System.Drawing.Size(94, 25);
            this.btnenterpriseDBRestore.TabIndex = 2;
            this.btnenterpriseDBRestore.Text = "Restore DB";
            this.btnenterpriseDBRestore.UseVisualStyleBackColor = false;
            this.btnenterpriseDBRestore.Click += new System.EventHandler(this.btnEnterpriseDBRestore_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // errValidate
            // 
            this.errValidate.ContainerControl = this;
            // 
            // frmBMCEnterpriseConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1013, 599);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmBMCEnterpriseConfig";
            this.Text = "BMC Enterprise Configuration";
            this.Load += new System.EventHandler(this.frmBMCEnterpriseConfig_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmBMCEnterpriseConfig_FormClosed);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmBMCEnterpriseConfig_FormClosing);
            this.panel1.ResumeLayout(false);
            this.gpKeysSetup.ResumeLayout(false);
            this.gpReports.ResumeLayout(false);
            this.gpReports.PerformLayout();
            this.gpAAMSSetup.ResumeLayout(false);
            this.gpAAMSSetup.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.gpServiceSetup.ResumeLayout(false);
            this.gpWindowsService.ResumeLayout(false);
            this.gpLGESetup.ResumeLayout(false);
            this.gpLGESetup.PerformLayout();
            this.gpLGECredentials.ResumeLayout(false);
            this.gpLGECredentials.PerformLayout();
            this.gpLGEActions.ResumeLayout(false);
            this.gpMeterAnalysisSetup.ResumeLayout(false);
            this.gpTktCredentials.ResumeLayout(false);
            this.gpTktCredentials.PerformLayout();
            this.gpMeterAnalysisActions.ResumeLayout(false);
            this.gpEnterpriseSetup.ResumeLayout(false);
            this.gpExCredentials.ResumeLayout(false);
            this.gpExCredentials.PerformLayout();
            this.gpenterpriseActions.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errValidate)).EndInit();
            this.ResumeLayout(false);

                    }

                    #endregion

        private System.Windows.Forms.Panel panel1;
                    private System.Windows.Forms.GroupBox gpEnterpriseSetup;
        private System.Windows.Forms.GroupBox gpServiceSetup;
                    private System.Windows.Forms.GroupBox gpExCredentials;
                    private System.Windows.Forms.TextBox txtenterprisePassword;
                    private System.Windows.Forms.Label lblenterprisePassword;
                    private System.Windows.Forms.TextBox txtenterpriseInstance;
                    private System.Windows.Forms.Label lblenterpriseInstance;
                    private System.Windows.Forms.TextBox txtenterpriseUsername;
                    private System.Windows.Forms.Label lblenterpriseUsername;
                    private System.Windows.Forms.TextBox txtenterpriseServer;
        private System.Windows.Forms.Label lblenterpriseserver;
        private System.Windows.Forms.Button btnenterpriseDBRestore;
        private System.Windows.Forms.Button btnEnterpriseTestConnection;
                    private System.Windows.Forms.GroupBox gpWindowsService;
                    private System.Windows.Forms.Button btnStartService;
        private System.Windows.Forms.GroupBox gpMeterAnalysisSetup;
        private System.Windows.Forms.GroupBox gpTktCredentials;
        private System.Windows.Forms.Label lblMeterAnalysisDBname;
        private System.Windows.Forms.GroupBox gpMeterAnalysisActions;
        private System.Windows.Forms.Button btnMeterAnalysisTestConnection;
        private System.Windows.Forms.Button btnMeterAnalysisDBRestore;
        private System.Windows.Forms.TextBox txtEnterpriseTimeOut;
        private System.Windows.Forms.Label lblEnterpriseTimeOut;
        private System.Windows.Forms.Label lblexDBName;
        private System.Windows.Forms.Label lblEnterpriseDBname;
        private System.Windows.Forms.GroupBox gpenterpriseActions;
        private System.Windows.Forms.GroupBox gpLGESetup;
        private System.Windows.Forms.GroupBox gpLGECredentials;
        private System.Windows.Forms.TextBox txtLGEtimeout;
        private System.Windows.Forms.Label lblLGETimeout;
        private System.Windows.Forms.Label lblLGEDbname;
        private System.Windows.Forms.TextBox txtLGEPassword;
        private System.Windows.Forms.Label lblLGEPassword;
        private System.Windows.Forms.TextBox txtLGEInstance;
        private System.Windows.Forms.Label lblLGEInstance;
        private System.Windows.Forms.TextBox txtLGEUsername;
        private System.Windows.Forms.Label lblLGEUsername;
        private System.Windows.Forms.TextBox txtLGEServer;
        private System.Windows.Forms.Label lblLGEServer;
        private System.Windows.Forms.GroupBox gpLGEActions;
        private System.Windows.Forms.Button btnLGEGatewayTestConnection;
        private System.Windows.Forms.ListView lvServiceslist;
        private System.Windows.Forms.Button btnEndService;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ProgressBar ProgressBarServices;
        private System.Windows.Forms.ErrorProvider errValidate;
        private System.Windows.Forms.Button btnSaveEnterpriseConnection;
        private System.Windows.Forms.Button btnRefreshServices;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnRunUpgradeScript;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnSaveSettings;
        private System.Windows.Forms.Button btnSelectAll;
        private System.Windows.Forms.CheckBox chkEnableLGE;
        private System.Windows.Forms.TextBox txtLGEDB;
        private System.Windows.Forms.GroupBox gpAAMSSetup;
        private System.Windows.Forms.CheckBox chkEnableAAMS;
        private System.Windows.Forms.Button btnTestURL;
        private System.Windows.Forms.TextBox txtBASweburl;
        private System.Windows.Forms.Label lblWebServiceUrl;
        private System.Windows.Forms.GroupBox gpReports;
        private System.Windows.Forms.TextBox txtReportFolder;
        private System.Windows.Forms.TextBox txtReportServer;
        private System.Windows.Forms.Label lblReportServer;
        private System.Windows.Forms.Button btnDeployReports;
        private System.Windows.Forms.Label lblReportFolder;
        private System.Windows.Forms.GroupBox gpKeysSetup;
        private System.Windows.Forms.Button btnCreateEnterpriseKey;
        private System.Windows.Forms.TextBox txtHBGSenderCode;
        private System.Windows.Forms.Label lblHBGSenderCode;
        private System.Windows.Forms.CheckBox chkUseEnterpriseConnection;
        private System.Windows.Forms.Label lblMeterAnalysisdb;
        private System.Windows.Forms.Label lblmessage;

        }

     
    }


