namespace BMC.UI.ExchangeConfig
{
    partial class frmBMCExchangeConfig
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBMCExchangeConfig));
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnRunUpgradeScript = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnSaveSettings = new System.Windows.Forms.Button();
            this.gpDHCPSettings = new System.Windows.Forms.GroupBox();
            this.txtInterfaceIP = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtMultiCastIP = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbSlotLan = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.chkEnableDHCP = new System.Windows.Forms.CheckBox();
            this.gpWebServiceSetup = new System.Windows.Forms.GroupBox();
            this.gpLoccode = new System.Windows.Forms.GroupBox();
            this.chkTrusted = new System.Windows.Forms.CheckBox();
            this.btnTestURL = new System.Windows.Forms.Button();
            this.txtEnterpriseweburl = new System.Windows.Forms.TextBox();
            this.lblWebServiceUrl = new System.Windows.Forms.Label();
            this.gpWindowsService = new System.Windows.Forms.GroupBox();
            this.btnSelectAll = new System.Windows.Forms.Button();
            this.btnRefreshServices = new System.Windows.Forms.Button();
            this.ProgressBarServices = new System.Windows.Forms.ProgressBar();
            this.btnEndService = new System.Windows.Forms.Button();
            this.lvServiceslist = new System.Windows.Forms.ListView();
            this.btnStartService = new System.Windows.Forms.Button();
            this.btnCreateMSMQ = new System.Windows.Forms.Button();
            this.gpCMPSetup = new System.Windows.Forms.GroupBox();
            this.gpCMPCredentials = new System.Windows.Forms.GroupBox();
            this.txtCMPtimeout = new System.Windows.Forms.TextBox();
            this.lblCMPTimeout = new System.Windows.Forms.Label();
            this.lblCMPDbname = new System.Windows.Forms.Label();
            this.lblCMPDB = new System.Windows.Forms.Label();
            this.txtCMPPassword = new System.Windows.Forms.TextBox();
            this.lblCMPPassword = new System.Windows.Forms.Label();
            this.txtCMPInstance = new System.Windows.Forms.TextBox();
            this.lblCMPInstance = new System.Windows.Forms.Label();
            this.txtCMPUsername = new System.Windows.Forms.TextBox();
            this.lblCMPUsername = new System.Windows.Forms.Label();
            this.txtCMPServer = new System.Windows.Forms.TextBox();
            this.lblCMPServer = new System.Windows.Forms.Label();
            this.gpCMPActions = new System.Windows.Forms.GroupBox();
            this.chkUseExchangeConnect = new System.Windows.Forms.CheckBox();
            this.btnCMPGatewayTestConnection = new System.Windows.Forms.Button();
            this.btnCMPGatewaySaveChanges = new System.Windows.Forms.Button();
            this.gpTicketSetup = new System.Windows.Forms.GroupBox();
            this.gpTktCredentials = new System.Windows.Forms.GroupBox();
            this.txtLocCode = new System.Windows.Forms.TextBox();
            this.lblLocCode = new System.Windows.Forms.Label();
            this.txtticketTimeout = new System.Windows.Forms.TextBox();
            this.lblticketTimeOut = new System.Windows.Forms.Label();
            this.lbltkdbname = new System.Windows.Forms.Label();
            this.lblticketDBname = new System.Windows.Forms.Label();
            this.txtticketPassword = new System.Windows.Forms.TextBox();
            this.lbltkPassword = new System.Windows.Forms.Label();
            this.txticketInstance = new System.Windows.Forms.TextBox();
            this.lbltkInstance = new System.Windows.Forms.Label();
            this.txtticketusername = new System.Windows.Forms.TextBox();
            this.lbltkUsername = new System.Windows.Forms.Label();
            this.txtticketserver = new System.Windows.Forms.TextBox();
            this.lbltkServer = new System.Windows.Forms.Label();
            this.gpTKActions = new System.Windows.Forms.GroupBox();
            this.chkUseExchangeConnection = new System.Windows.Forms.CheckBox();
            this.btnTicketingTestConnection = new System.Windows.Forms.Button();
            this.btnTicketDBRestore = new System.Windows.Forms.Button();
            this.gpLocalBindIP = new System.Windows.Forms.GroupBox();
            this.chkEnableRSAEncrypt = new System.Windows.Forms.CheckBox();
            this.chkDisableMachine = new System.Windows.Forms.CheckBox();
            this.chkEnableEncrypt = new System.Windows.Forms.CheckBox();
            this.cmbLanIP = new System.Windows.Forms.ComboBox();
            this.lblLocalIP = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.gpExchangeSetup = new System.Windows.Forms.GroupBox();
            this.gpExCredentials = new System.Windows.Forms.GroupBox();
            this.txtexchangeTimeOut = new System.Windows.Forms.TextBox();
            this.lblexchangeTimeOut = new System.Windows.Forms.Label();
            this.lblexDBName = new System.Windows.Forms.Label();
            this.lblexchangeDBname = new System.Windows.Forms.Label();
            this.txtexchangePassword = new System.Windows.Forms.TextBox();
            this.lblexPassword = new System.Windows.Forms.Label();
            this.txtexchangeInstance = new System.Windows.Forms.TextBox();
            this.lblexInstance = new System.Windows.Forms.Label();
            this.txtexchangeUsername = new System.Windows.Forms.TextBox();
            this.lblexUsername = new System.Windows.Forms.Label();
            this.txtexchangeServer = new System.Windows.Forms.TextBox();
            this.lblexServer = new System.Windows.Forms.Label();
            this.gpexActions = new System.Windows.Forms.GroupBox();
            this.btnDSNSave = new System.Windows.Forms.Button();
            this.btnSaveExchangeConnection = new System.Windows.Forms.Button();
            this.btnExchangeTestConnection = new System.Windows.Forms.Button();
            this.btnexchangeDBRestore = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.errValidate = new System.Windows.Forms.ErrorProvider(this.components);
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.gpDHCPSettings.SuspendLayout();
            this.gpWebServiceSetup.SuspendLayout();
            this.gpLoccode.SuspendLayout();
            this.gpWindowsService.SuspendLayout();
            this.gpCMPSetup.SuspendLayout();
            this.gpCMPCredentials.SuspendLayout();
            this.gpCMPActions.SuspendLayout();
            this.gpTicketSetup.SuspendLayout();
            this.gpTktCredentials.SuspendLayout();
            this.gpTKActions.SuspendLayout();
            this.gpLocalBindIP.SuspendLayout();
            this.gpExchangeSetup.SuspendLayout();
            this.gpExCredentials.SuspendLayout();
            this.gpexActions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errValidate)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.gpDHCPSettings);
            this.panel1.Controls.Add(this.gpWebServiceSetup);
            this.panel1.Controls.Add(this.gpCMPSetup);
            this.panel1.Controls.Add(this.gpTicketSetup);
            this.panel1.Controls.Add(this.gpLocalBindIP);
            this.panel1.Controls.Add(this.gpExchangeSetup);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(971, 594);
            this.panel1.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnRunUpgradeScript);
            this.groupBox2.Controls.Add(this.btnExit);
            this.groupBox2.Controls.Add(this.btnSaveSettings);
            this.groupBox2.Location = new System.Drawing.Point(3, 536);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(959, 47);
            this.groupBox2.TabIndex = 25;
            this.groupBox2.TabStop = false;
            // 
            // btnRunUpgradeScript
            // 
            this.btnRunUpgradeScript.BackColor = System.Drawing.Color.White;
            this.btnRunUpgradeScript.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRunUpgradeScript.Location = new System.Drawing.Point(425, 14);
            this.btnRunUpgradeScript.Name = "btnRunUpgradeScript";
            this.btnRunUpgradeScript.Size = new System.Drawing.Size(112, 25);
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
            this.btnExit.Size = new System.Drawing.Size(112, 25);
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
            this.btnSaveSettings.Size = new System.Drawing.Size(112, 25);
            this.btnSaveSettings.TabIndex = 24;
            this.btnSaveSettings.Text = "Save All Settings";
            this.btnSaveSettings.UseVisualStyleBackColor = false;
            this.btnSaveSettings.Click += new System.EventHandler(this.btnSaveSettings_Click);
            // 
            // gpDHCPSettings
            // 
            this.gpDHCPSettings.Controls.Add(this.txtInterfaceIP);
            this.gpDHCPSettings.Controls.Add(this.label3);
            this.gpDHCPSettings.Controls.Add(this.txtMultiCastIP);
            this.gpDHCPSettings.Controls.Add(this.label4);
            this.gpDHCPSettings.Controls.Add(this.cmbSlotLan);
            this.gpDHCPSettings.Controls.Add(this.label1);
            this.gpDHCPSettings.Controls.Add(this.chkEnableDHCP);
            this.gpDHCPSettings.Location = new System.Drawing.Point(492, 141);
            this.gpDHCPSettings.Name = "gpDHCPSettings";
            this.gpDHCPSettings.Size = new System.Drawing.Size(471, 86);
            this.gpDHCPSettings.TabIndex = 5;
            this.gpDHCPSettings.TabStop = false;
            this.gpDHCPSettings.Text = "DHCP Server Settings";
            // 
            // txtInterfaceIP
            // 
            this.txtInterfaceIP.BackColor = System.Drawing.Color.White;
            this.txtInterfaceIP.Location = new System.Drawing.Point(323, 61);
            this.txtInterfaceIP.MaxLength = 15;
            this.txtInterfaceIP.Name = "txtInterfaceIP";
            this.txtInterfaceIP.Size = new System.Drawing.Size(136, 20);
            this.txtInterfaceIP.TabIndex = 22;
            this.txtInterfaceIP.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtInterfaceIP_KeyDown);
            this.txtInterfaceIP.Leave += new System.EventHandler(this.txtInterfaceIP_Leave);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(179, 63);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(128, 13);
            this.label3.TabIndex = 24;
            this.label3.Text = "Slot LAN Interface IP";
            // 
            // txtMultiCastIP
            // 
            this.txtMultiCastIP.BackColor = System.Drawing.Color.White;
            this.txtMultiCastIP.Location = new System.Drawing.Point(323, 37);
            this.txtMultiCastIP.MaxLength = 15;
            this.txtMultiCastIP.Name = "txtMultiCastIP";
            this.txtMultiCastIP.Size = new System.Drawing.Size(136, 20);
            this.txtMultiCastIP.TabIndex = 21;
            this.txtMultiCastIP.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMultiCastIP_KeyDown);
            this.txtMultiCastIP.Leave += new System.EventHandler(this.txtMultiCastIP_Leave);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(234, 39);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(74, 13);
            this.label4.TabIndex = 23;
            this.label4.Text = "Multicast IP";
            // 
            // cmbSlotLan
            // 
            this.cmbSlotLan.BackColor = System.Drawing.Color.White;
            this.cmbSlotLan.FormattingEnabled = true;
            this.cmbSlotLan.Location = new System.Drawing.Point(323, 12);
            this.cmbSlotLan.Name = "cmbSlotLan";
            this.cmbSlotLan.Size = new System.Drawing.Size(136, 21);
            this.cmbSlotLan.TabIndex = 1;
            this.cmbSlotLan.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbSlotLan_KeyPress);
            this.cmbSlotLan.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbSlotLan_KeyDown);
            this.cmbSlotLan.TabIndexChanged += new System.EventHandler(this.cmbSlotLan_TabIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(250, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 20;
            this.label1.Text = "Slot LAN ";
            // 
            // chkEnableDHCP
            // 
            this.chkEnableDHCP.AutoSize = true;
            this.chkEnableDHCP.BackColor = System.Drawing.Color.White;
            this.chkEnableDHCP.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkEnableDHCP.ForeColor = System.Drawing.Color.Navy;
            this.chkEnableDHCP.Location = new System.Drawing.Point(15, 18);
            this.chkEnableDHCP.Name = "chkEnableDHCP";
            this.chkEnableDHCP.Size = new System.Drawing.Size(103, 17);
            this.chkEnableDHCP.TabIndex = 0;
            this.chkEnableDHCP.Text = "Enable DHCP";
            this.chkEnableDHCP.UseVisualStyleBackColor = false;
            this.chkEnableDHCP.CheckedChanged += new System.EventHandler(this.chkEnableDHCP_CheckedChanged);
            // 
            // gpWebServiceSetup
            // 
            this.gpWebServiceSetup.Controls.Add(this.gpLoccode);
            this.gpWebServiceSetup.Controls.Add(this.gpWindowsService);
            this.gpWebServiceSetup.Location = new System.Drawing.Point(491, 230);
            this.gpWebServiceSetup.Name = "gpWebServiceSetup";
            this.gpWebServiceSetup.Size = new System.Drawing.Size(471, 304);
            this.gpWebServiceSetup.TabIndex = 6;
            this.gpWebServiceSetup.TabStop = false;
            this.gpWebServiceSetup.Text = " Service Setup";
            // 
            // gpLoccode
            // 
            this.gpLoccode.Controls.Add(this.chkTrusted);
            this.gpLoccode.Controls.Add(this.btnTestURL);
            this.gpLoccode.Controls.Add(this.txtEnterpriseweburl);
            this.gpLoccode.Controls.Add(this.lblWebServiceUrl);
            this.gpLoccode.Location = new System.Drawing.Point(8, 13);
            this.gpLoccode.Name = "gpLoccode";
            this.gpLoccode.Size = new System.Drawing.Size(459, 84);
            this.gpLoccode.TabIndex = 1;
            this.gpLoccode.TabStop = false;
            this.gpLoccode.Text = "Web ";
            // 
            // chkTrusted
            // 
            this.chkTrusted.AutoSize = true;
            this.chkTrusted.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.chkTrusted.ForeColor = System.Drawing.Color.Navy;
            this.chkTrusted.Location = new System.Drawing.Point(143, 10);
            this.chkTrusted.Name = "chkTrusted";
            this.chkTrusted.Size = new System.Drawing.Size(95, 17);
            this.chkTrusted.TabIndex = 18;
            this.chkTrusted.Text = "Trusted Site";
            this.chkTrusted.UseVisualStyleBackColor = true;
            this.chkTrusted.CheckedChanged += new System.EventHandler(this.chkTrusted_CheckedChanged);
            // 
            // btnTestURL
            // 
            this.btnTestURL.BackColor = System.Drawing.Color.White;
            this.btnTestURL.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTestURL.Location = new System.Drawing.Point(143, 55);
            this.btnTestURL.Name = "btnTestURL";
            this.btnTestURL.Size = new System.Drawing.Size(94, 25);
            this.btnTestURL.TabIndex = 1;
            this.btnTestURL.Text = "Test Url";
            this.btnTestURL.UseVisualStyleBackColor = false;
            this.btnTestURL.Click += new System.EventHandler(this.btnTestURL_Click);
            // 
            // txtEnterpriseweburl
            // 
            this.txtEnterpriseweburl.BackColor = System.Drawing.Color.White;
            this.txtEnterpriseweburl.Location = new System.Drawing.Point(143, 31);
            this.txtEnterpriseweburl.Name = "txtEnterpriseweburl";
            this.txtEnterpriseweburl.Size = new System.Drawing.Size(301, 20);
            this.txtEnterpriseweburl.TabIndex = 0;
            // 
            // lblWebServiceUrl
            // 
            this.lblWebServiceUrl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWebServiceUrl.Location = new System.Drawing.Point(4, 32);
            this.lblWebServiceUrl.Name = "lblWebServiceUrl";
            this.lblWebServiceUrl.Size = new System.Drawing.Size(94, 29);
            this.lblWebServiceUrl.TabIndex = 17;
            this.lblWebServiceUrl.Text = "Enterprise Server Url / IP ";
            // 
            // gpWindowsService
            // 
            this.gpWindowsService.Controls.Add(this.btnSelectAll);
            this.gpWindowsService.Controls.Add(this.btnRefreshServices);
            this.gpWindowsService.Controls.Add(this.ProgressBarServices);
            this.gpWindowsService.Controls.Add(this.btnEndService);
            this.gpWindowsService.Controls.Add(this.lvServiceslist);
            this.gpWindowsService.Controls.Add(this.btnStartService);
            this.gpWindowsService.Controls.Add(this.btnCreateMSMQ);
            this.gpWindowsService.Location = new System.Drawing.Point(8, 99);
            this.gpWindowsService.Name = "gpWindowsService";
            this.gpWindowsService.Size = new System.Drawing.Size(457, 199);
            this.gpWindowsService.TabIndex = 2;
            this.gpWindowsService.TabStop = false;
            this.gpWindowsService.Text = "Windows";
            // 
            // btnSelectAll
            // 
            this.btnSelectAll.BackColor = System.Drawing.Color.White;
            this.btnSelectAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSelectAll.Location = new System.Drawing.Point(326, 138);
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
            this.btnRefreshServices.Location = new System.Drawing.Point(327, 107);
            this.btnRefreshServices.Name = "btnRefreshServices";
            this.btnRefreshServices.Size = new System.Drawing.Size(125, 25);
            this.btnRefreshServices.TabIndex = 9;
            this.btnRefreshServices.Text = "Refresh All Services";
            this.btnRefreshServices.UseVisualStyleBackColor = false;
            this.btnRefreshServices.Click += new System.EventHandler(this.btnRefreshServices_Click);
            // 
            // ProgressBarServices
            // 
            this.ProgressBarServices.Location = new System.Drawing.Point(7, 170);
            this.ProgressBarServices.Name = "ProgressBarServices";
            this.ProgressBarServices.Size = new System.Drawing.Size(444, 23);
            this.ProgressBarServices.TabIndex = 8;
            // 
            // btnEndService
            // 
            this.btnEndService.BackColor = System.Drawing.Color.White;
            this.btnEndService.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEndService.Location = new System.Drawing.Point(325, 76);
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
            this.lvServiceslist.Location = new System.Drawing.Point(6, 19);
            this.lvServiceslist.Name = "lvServiceslist";
            this.lvServiceslist.Size = new System.Drawing.Size(314, 148);
            this.lvServiceslist.TabIndex = 3;
            this.lvServiceslist.UseCompatibleStateImageBehavior = false;
            // 
            // btnStartService
            // 
            this.btnStartService.BackColor = System.Drawing.Color.White;
            this.btnStartService.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStartService.Location = new System.Drawing.Point(327, 45);
            this.btnStartService.Name = "btnStartService";
            this.btnStartService.Size = new System.Drawing.Size(125, 25);
            this.btnStartService.TabIndex = 1;
            this.btnStartService.Text = "Start Service(s)";
            this.btnStartService.UseVisualStyleBackColor = false;
            this.btnStartService.Click += new System.EventHandler(this.btnStartService_Click);
            // 
            // btnCreateMSMQ
            // 
            this.btnCreateMSMQ.BackColor = System.Drawing.Color.White;
            this.btnCreateMSMQ.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCreateMSMQ.Location = new System.Drawing.Point(327, 14);
            this.btnCreateMSMQ.Name = "btnCreateMSMQ";
            this.btnCreateMSMQ.Size = new System.Drawing.Size(125, 25);
            this.btnCreateMSMQ.TabIndex = 0;
            this.btnCreateMSMQ.Text = "Create MSMQ Queue";
            this.btnCreateMSMQ.UseVisualStyleBackColor = false;
            this.btnCreateMSMQ.Click += new System.EventHandler(this.btnCreateMSMQ_Click);
            // 
            // gpCMPSetup
            // 
            this.gpCMPSetup.Controls.Add(this.gpCMPCredentials);
            this.gpCMPSetup.Controls.Add(this.gpCMPActions);
            this.gpCMPSetup.Location = new System.Drawing.Point(4, 358);
            this.gpCMPSetup.Name = "gpCMPSetup";
            this.gpCMPSetup.Size = new System.Drawing.Size(482, 172);
            this.gpCMPSetup.TabIndex = 3;
            this.gpCMPSetup.TabStop = false;
            this.gpCMPSetup.Text = "CMP Gateway Connection Setup";
            // 
            // gpCMPCredentials
            // 
            this.gpCMPCredentials.Controls.Add(this.txtCMPtimeout);
            this.gpCMPCredentials.Controls.Add(this.lblCMPTimeout);
            this.gpCMPCredentials.Controls.Add(this.lblCMPDbname);
            this.gpCMPCredentials.Controls.Add(this.lblCMPDB);
            this.gpCMPCredentials.Controls.Add(this.txtCMPPassword);
            this.gpCMPCredentials.Controls.Add(this.lblCMPPassword);
            this.gpCMPCredentials.Controls.Add(this.txtCMPInstance);
            this.gpCMPCredentials.Controls.Add(this.lblCMPInstance);
            this.gpCMPCredentials.Controls.Add(this.txtCMPUsername);
            this.gpCMPCredentials.Controls.Add(this.lblCMPUsername);
            this.gpCMPCredentials.Controls.Add(this.txtCMPServer);
            this.gpCMPCredentials.Controls.Add(this.lblCMPServer);
            this.gpCMPCredentials.Location = new System.Drawing.Point(6, 14);
            this.gpCMPCredentials.Name = "gpCMPCredentials";
            this.gpCMPCredentials.Size = new System.Drawing.Size(471, 103);
            this.gpCMPCredentials.TabIndex = 4;
            this.gpCMPCredentials.TabStop = false;
            // 
            // txtCMPtimeout
            // 
            this.txtCMPtimeout.BackColor = System.Drawing.Color.White;
            this.txtCMPtimeout.Location = new System.Drawing.Point(319, 74);
            this.txtCMPtimeout.MaxLength = 3;
            this.txtCMPtimeout.Name = "txtCMPtimeout";
            this.txtCMPtimeout.Size = new System.Drawing.Size(64, 20);
            this.txtCMPtimeout.TabIndex = 4;
            this.txtCMPtimeout.Text = "30";
            this.txtCMPtimeout.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCMPtimeout_KeyPress);
            // 
            // lblCMPTimeout
            // 
            this.lblCMPTimeout.AutoSize = true;
            this.lblCMPTimeout.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCMPTimeout.Location = new System.Drawing.Point(190, 77);
            this.lblCMPTimeout.Name = "lblCMPTimeout";
            this.lblCMPTimeout.Size = new System.Drawing.Size(119, 13);
            this.lblCMPTimeout.TabIndex = 18;
            this.lblCMPTimeout.Text = "Time Out (Seconds)";
            // 
            // lblCMPDbname
            // 
            this.lblCMPDbname.AutoSize = true;
            this.lblCMPDbname.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCMPDbname.Location = new System.Drawing.Point(7, 77);
            this.lblCMPDbname.Name = "lblCMPDbname";
            this.lblCMPDbname.Size = new System.Drawing.Size(60, 13);
            this.lblCMPDbname.TabIndex = 16;
            this.lblCMPDbname.Text = "DB Name";
            // 
            // lblCMPDB
            // 
            this.lblCMPDB.AutoSize = true;
            this.lblCMPDB.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCMPDB.Location = new System.Drawing.Point(77, 77);
            this.lblCMPDB.Name = "lblCMPDB";
            this.lblCMPDB.Size = new System.Drawing.Size(62, 13);
            this.lblCMPDB.TabIndex = 17;
            this.lblCMPDB.Text = "CMktSDG";
            // 
            // txtCMPPassword
            // 
            this.txtCMPPassword.BackColor = System.Drawing.Color.White;
            this.txtCMPPassword.Location = new System.Drawing.Point(319, 46);
            this.txtCMPPassword.Name = "txtCMPPassword";
            this.txtCMPPassword.PasswordChar = '*';
            this.txtCMPPassword.Size = new System.Drawing.Size(136, 20);
            this.txtCMPPassword.TabIndex = 3;
            // 
            // lblCMPPassword
            // 
            this.lblCMPPassword.AutoSize = true;
            this.lblCMPPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCMPPassword.Location = new System.Drawing.Point(248, 49);
            this.lblCMPPassword.Name = "lblCMPPassword";
            this.lblCMPPassword.Size = new System.Drawing.Size(61, 13);
            this.lblCMPPassword.TabIndex = 14;
            this.lblCMPPassword.Text = "Password";
            // 
            // txtCMPInstance
            // 
            this.txtCMPInstance.BackColor = System.Drawing.Color.White;
            this.txtCMPInstance.Location = new System.Drawing.Point(319, 14);
            this.txtCMPInstance.Name = "txtCMPInstance";
            this.txtCMPInstance.Size = new System.Drawing.Size(136, 20);
            this.txtCMPInstance.TabIndex = 1;
            // 
            // lblCMPInstance
            // 
            this.lblCMPInstance.AutoSize = true;
            this.lblCMPInstance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCMPInstance.Location = new System.Drawing.Point(253, 17);
            this.lblCMPInstance.Name = "lblCMPInstance";
            this.lblCMPInstance.Size = new System.Drawing.Size(56, 13);
            this.lblCMPInstance.TabIndex = 12;
            this.lblCMPInstance.Text = "Instance";
            // 
            // txtCMPUsername
            // 
            this.txtCMPUsername.BackColor = System.Drawing.Color.White;
            this.txtCMPUsername.Location = new System.Drawing.Point(77, 46);
            this.txtCMPUsername.Name = "txtCMPUsername";
            this.txtCMPUsername.Size = new System.Drawing.Size(136, 20);
            this.txtCMPUsername.TabIndex = 2;
            // 
            // lblCMPUsername
            // 
            this.lblCMPUsername.AutoSize = true;
            this.lblCMPUsername.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCMPUsername.Location = new System.Drawing.Point(4, 49);
            this.lblCMPUsername.Name = "lblCMPUsername";
            this.lblCMPUsername.Size = new System.Drawing.Size(63, 13);
            this.lblCMPUsername.TabIndex = 10;
            this.lblCMPUsername.Text = "Username";
            // 
            // txtCMPServer
            // 
            this.txtCMPServer.BackColor = System.Drawing.Color.White;
            this.txtCMPServer.Location = new System.Drawing.Point(77, 14);
            this.txtCMPServer.Name = "txtCMPServer";
            this.txtCMPServer.Size = new System.Drawing.Size(136, 20);
            this.txtCMPServer.TabIndex = 0;
            // 
            // lblCMPServer
            // 
            this.lblCMPServer.AutoSize = true;
            this.lblCMPServer.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCMPServer.Location = new System.Drawing.Point(4, 17);
            this.lblCMPServer.Name = "lblCMPServer";
            this.lblCMPServer.Size = new System.Drawing.Size(44, 13);
            this.lblCMPServer.TabIndex = 8;
            this.lblCMPServer.Text = "Server";
            // 
            // gpCMPActions
            // 
            this.gpCMPActions.Controls.Add(this.chkUseExchangeConnect);
            this.gpCMPActions.Controls.Add(this.btnCMPGatewayTestConnection);
            this.gpCMPActions.Controls.Add(this.btnCMPGatewaySaveChanges);
            this.gpCMPActions.Location = new System.Drawing.Point(7, 123);
            this.gpCMPActions.Name = "gpCMPActions";
            this.gpCMPActions.Size = new System.Drawing.Size(471, 43);
            this.gpCMPActions.TabIndex = 5;
            this.gpCMPActions.TabStop = false;
            this.gpCMPActions.Text = "Actions";
            // 
            // chkUseExchangeConnect
            // 
            this.chkUseExchangeConnect.AutoSize = true;
            this.chkUseExchangeConnect.BackColor = System.Drawing.Color.White;
            this.chkUseExchangeConnect.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkUseExchangeConnect.ForeColor = System.Drawing.Color.Navy;
            this.chkUseExchangeConnect.Location = new System.Drawing.Point(6, 19);
            this.chkUseExchangeConnect.Name = "chkUseExchangeConnect";
            this.chkUseExchangeConnect.Size = new System.Drawing.Size(176, 17);
            this.chkUseExchangeConnect.TabIndex = 0;
            this.chkUseExchangeConnect.Text = "Use Exchange Connection";
            this.chkUseExchangeConnect.UseVisualStyleBackColor = false;
            this.chkUseExchangeConnect.CheckedChanged += new System.EventHandler(this.chkUseExchangeConnect_CheckedChanged);
            // 
            // btnCMPGatewayTestConnection
            // 
            this.btnCMPGatewayTestConnection.BackColor = System.Drawing.Color.White;
            this.btnCMPGatewayTestConnection.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCMPGatewayTestConnection.Location = new System.Drawing.Point(200, 11);
            this.btnCMPGatewayTestConnection.Name = "btnCMPGatewayTestConnection";
            this.btnCMPGatewayTestConnection.Size = new System.Drawing.Size(117, 25);
            this.btnCMPGatewayTestConnection.TabIndex = 1;
            this.btnCMPGatewayTestConnection.Text = "Test Connection";
            this.btnCMPGatewayTestConnection.UseVisualStyleBackColor = false;
            this.btnCMPGatewayTestConnection.Click += new System.EventHandler(this.btnCMPGatewayTestConnection_Click);
            // 
            // btnCMPGatewaySaveChanges
            // 
            this.btnCMPGatewaySaveChanges.BackColor = System.Drawing.Color.White;
            this.btnCMPGatewaySaveChanges.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCMPGatewaySaveChanges.Location = new System.Drawing.Point(319, 11);
            this.btnCMPGatewaySaveChanges.Name = "btnCMPGatewaySaveChanges";
            this.btnCMPGatewaySaveChanges.Size = new System.Drawing.Size(94, 25);
            this.btnCMPGatewaySaveChanges.TabIndex = 2;
            this.btnCMPGatewaySaveChanges.Text = "Restore DB";
            this.btnCMPGatewaySaveChanges.UseVisualStyleBackColor = false;
            this.btnCMPGatewaySaveChanges.Click += new System.EventHandler(this.btnCMPGatewaySaveChanges_Click);
            // 
            // gpTicketSetup
            // 
            this.gpTicketSetup.Controls.Add(this.gpTktCredentials);
            this.gpTicketSetup.Controls.Add(this.gpTKActions);
            this.gpTicketSetup.Location = new System.Drawing.Point(4, 177);
            this.gpTicketSetup.Name = "gpTicketSetup";
            this.gpTicketSetup.Size = new System.Drawing.Size(482, 170);
            this.gpTicketSetup.TabIndex = 2;
            this.gpTicketSetup.TabStop = false;
            this.gpTicketSetup.Text = " Ticketing Connection Setup";
            // 
            // gpTktCredentials
            // 
            this.gpTktCredentials.Controls.Add(this.txtLocCode);
            this.gpTktCredentials.Controls.Add(this.lblLocCode);
            this.gpTktCredentials.Controls.Add(this.txtticketTimeout);
            this.gpTktCredentials.Controls.Add(this.lblticketTimeOut);
            this.gpTktCredentials.Controls.Add(this.lbltkdbname);
            this.gpTktCredentials.Controls.Add(this.lblticketDBname);
            this.gpTktCredentials.Controls.Add(this.txtticketPassword);
            this.gpTktCredentials.Controls.Add(this.lbltkPassword);
            this.gpTktCredentials.Controls.Add(this.txticketInstance);
            this.gpTktCredentials.Controls.Add(this.lbltkInstance);
            this.gpTktCredentials.Controls.Add(this.txtticketusername);
            this.gpTktCredentials.Controls.Add(this.lbltkUsername);
            this.gpTktCredentials.Controls.Add(this.txtticketserver);
            this.gpTktCredentials.Controls.Add(this.lbltkServer);
            this.gpTktCredentials.Location = new System.Drawing.Point(6, 14);
            this.gpTktCredentials.Name = "gpTktCredentials";
            this.gpTktCredentials.Size = new System.Drawing.Size(471, 104);
            this.gpTktCredentials.TabIndex = 2;
            this.gpTktCredentials.TabStop = false;
            // 
            // txtLocCode
            // 
            this.txtLocCode.BackColor = System.Drawing.Color.White;
            this.txtLocCode.Location = new System.Drawing.Point(409, 74);
            this.txtLocCode.Name = "txtLocCode";
            this.txtLocCode.Size = new System.Drawing.Size(46, 20);
            this.txtLocCode.TabIndex = 19;
            this.txtLocCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtLocCode_KeyDown);
            // 
            // lblLocCode
            // 
            this.lblLocCode.AutoSize = true;
            this.lblLocCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLocCode.Location = new System.Drawing.Point(319, 80);
            this.lblLocCode.Name = "lblLocCode";
            this.lblLocCode.Size = new System.Drawing.Size(89, 13);
            this.lblLocCode.TabIndex = 20;
            this.lblLocCode.Text = "Location Code";
            // 
            // txtticketTimeout
            // 
            this.txtticketTimeout.BackColor = System.Drawing.Color.White;
            this.txtticketTimeout.Location = new System.Drawing.Point(262, 77);
            this.txtticketTimeout.MaxLength = 3;
            this.txtticketTimeout.Name = "txtticketTimeout";
            this.txtticketTimeout.Size = new System.Drawing.Size(27, 20);
            this.txtticketTimeout.TabIndex = 4;
            this.txtticketTimeout.Text = "30";
            this.txtticketTimeout.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtticketTimeout_KeyPress);
            // 
            // lblticketTimeOut
            // 
            this.lblticketTimeOut.AutoSize = true;
            this.lblticketTimeOut.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblticketTimeOut.Location = new System.Drawing.Point(138, 80);
            this.lblticketTimeOut.Name = "lblticketTimeOut";
            this.lblticketTimeOut.Size = new System.Drawing.Size(119, 13);
            this.lblticketTimeOut.TabIndex = 18;
            this.lblticketTimeOut.Text = "Time Out (Seconds)";
            // 
            // lbltkdbname
            // 
            this.lbltkdbname.AutoSize = true;
            this.lbltkdbname.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbltkdbname.Location = new System.Drawing.Point(6, 80);
            this.lbltkdbname.Name = "lbltkdbname";
            this.lbltkdbname.Size = new System.Drawing.Size(60, 13);
            this.lbltkdbname.TabIndex = 16;
            this.lbltkdbname.Text = "DB Name";
            // 
            // lblticketDBname
            // 
            this.lblticketDBname.AutoSize = true;
            this.lblticketDBname.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblticketDBname.Location = new System.Drawing.Point(72, 80);
            this.lblticketDBname.Name = "lblticketDBname";
            this.lblticketDBname.Size = new System.Drawing.Size(60, 13);
            this.lblticketDBname.TabIndex = 17;
            this.lblticketDBname.Text = "Ticketing";
            // 
            // txtticketPassword
            // 
            this.txtticketPassword.BackColor = System.Drawing.Color.White;
            this.txtticketPassword.Location = new System.Drawing.Point(319, 48);
            this.txtticketPassword.Name = "txtticketPassword";
            this.txtticketPassword.PasswordChar = '*';
            this.txtticketPassword.Size = new System.Drawing.Size(136, 20);
            this.txtticketPassword.TabIndex = 3;
            // 
            // lbltkPassword
            // 
            this.lbltkPassword.AutoSize = true;
            this.lbltkPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbltkPassword.Location = new System.Drawing.Point(251, 50);
            this.lbltkPassword.Name = "lbltkPassword";
            this.lbltkPassword.Size = new System.Drawing.Size(61, 13);
            this.lbltkPassword.TabIndex = 14;
            this.lbltkPassword.Text = "Password";
            // 
            // txticketInstance
            // 
            this.txticketInstance.BackColor = System.Drawing.Color.White;
            this.txticketInstance.Location = new System.Drawing.Point(319, 14);
            this.txticketInstance.Name = "txticketInstance";
            this.txticketInstance.Size = new System.Drawing.Size(136, 20);
            this.txticketInstance.TabIndex = 1;
            // 
            // lbltkInstance
            // 
            this.lbltkInstance.AutoSize = true;
            this.lbltkInstance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbltkInstance.Location = new System.Drawing.Point(256, 16);
            this.lbltkInstance.Name = "lbltkInstance";
            this.lbltkInstance.Size = new System.Drawing.Size(56, 13);
            this.lbltkInstance.TabIndex = 12;
            this.lbltkInstance.Text = "Instance";
            // 
            // txtticketusername
            // 
            this.txtticketusername.BackColor = System.Drawing.Color.White;
            this.txtticketusername.Location = new System.Drawing.Point(77, 48);
            this.txtticketusername.Name = "txtticketusername";
            this.txtticketusername.Size = new System.Drawing.Size(136, 20);
            this.txtticketusername.TabIndex = 2;
            // 
            // lbltkUsername
            // 
            this.lbltkUsername.AutoSize = true;
            this.lbltkUsername.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbltkUsername.Location = new System.Drawing.Point(4, 51);
            this.lbltkUsername.Name = "lbltkUsername";
            this.lbltkUsername.Size = new System.Drawing.Size(63, 13);
            this.lbltkUsername.TabIndex = 10;
            this.lbltkUsername.Text = "Username";
            // 
            // txtticketserver
            // 
            this.txtticketserver.BackColor = System.Drawing.Color.White;
            this.txtticketserver.Location = new System.Drawing.Point(77, 14);
            this.txtticketserver.Name = "txtticketserver";
            this.txtticketserver.Size = new System.Drawing.Size(136, 20);
            this.txtticketserver.TabIndex = 0;
            // 
            // lbltkServer
            // 
            this.lbltkServer.AutoSize = true;
            this.lbltkServer.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbltkServer.Location = new System.Drawing.Point(4, 17);
            this.lbltkServer.Name = "lbltkServer";
            this.lbltkServer.Size = new System.Drawing.Size(44, 13);
            this.lbltkServer.TabIndex = 8;
            this.lbltkServer.Text = "Server";
            // 
            // gpTKActions
            // 
            this.gpTKActions.Controls.Add(this.chkUseExchangeConnection);
            this.gpTKActions.Controls.Add(this.btnTicketingTestConnection);
            this.gpTKActions.Controls.Add(this.btnTicketDBRestore);
            this.gpTKActions.Location = new System.Drawing.Point(6, 124);
            this.gpTKActions.Name = "gpTKActions";
            this.gpTKActions.Size = new System.Drawing.Size(471, 40);
            this.gpTKActions.TabIndex = 3;
            this.gpTKActions.TabStop = false;
            this.gpTKActions.Text = "Actions";
            // 
            // chkUseExchangeConnection
            // 
            this.chkUseExchangeConnection.AutoSize = true;
            this.chkUseExchangeConnection.BackColor = System.Drawing.Color.White;
            this.chkUseExchangeConnection.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkUseExchangeConnection.ForeColor = System.Drawing.Color.Navy;
            this.chkUseExchangeConnection.Location = new System.Drawing.Point(6, 17);
            this.chkUseExchangeConnection.Name = "chkUseExchangeConnection";
            this.chkUseExchangeConnection.Size = new System.Drawing.Size(176, 17);
            this.chkUseExchangeConnection.TabIndex = 0;
            this.chkUseExchangeConnection.Text = "Use Exchange Connection";
            this.chkUseExchangeConnection.UseVisualStyleBackColor = false;
            this.chkUseExchangeConnection.CheckedChanged += new System.EventHandler(this.chkUseExchangeConnection_CheckedChanged);
            // 
            // btnTicketingTestConnection
            // 
            this.btnTicketingTestConnection.BackColor = System.Drawing.Color.White;
            this.btnTicketingTestConnection.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTicketingTestConnection.Location = new System.Drawing.Point(200, 10);
            this.btnTicketingTestConnection.Name = "btnTicketingTestConnection";
            this.btnTicketingTestConnection.Size = new System.Drawing.Size(117, 25);
            this.btnTicketingTestConnection.TabIndex = 1;
            this.btnTicketingTestConnection.Text = "Test Connection";
            this.btnTicketingTestConnection.UseVisualStyleBackColor = false;
            this.btnTicketingTestConnection.Click += new System.EventHandler(this.btnTicketingTestConnection_Click);
            // 
            // btnTicketDBRestore
            // 
            this.btnTicketDBRestore.BackColor = System.Drawing.Color.White;
            this.btnTicketDBRestore.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTicketDBRestore.Location = new System.Drawing.Point(323, 10);
            this.btnTicketDBRestore.Name = "btnTicketDBRestore";
            this.btnTicketDBRestore.Size = new System.Drawing.Size(94, 25);
            this.btnTicketDBRestore.TabIndex = 2;
            this.btnTicketDBRestore.Text = "Restore DB";
            this.btnTicketDBRestore.UseVisualStyleBackColor = false;
            this.btnTicketDBRestore.Click += new System.EventHandler(this.btnTicketDBRestore_Click);
            // 
            // gpLocalBindIP
            // 
            this.gpLocalBindIP.Controls.Add(this.chkEnableRSAEncrypt);
            this.gpLocalBindIP.Controls.Add(this.chkDisableMachine);
            this.gpLocalBindIP.Controls.Add(this.chkEnableEncrypt);
            this.gpLocalBindIP.Controls.Add(this.cmbLanIP);
            this.gpLocalBindIP.Controls.Add(this.lblLocalIP);
            this.gpLocalBindIP.Controls.Add(this.label2);
            this.gpLocalBindIP.Location = new System.Drawing.Point(492, 0);
            this.gpLocalBindIP.Name = "gpLocalBindIP";
            this.gpLocalBindIP.Size = new System.Drawing.Size(472, 135);
            this.gpLocalBindIP.TabIndex = 4;
            this.gpLocalBindIP.TabStop = false;
            this.gpLocalBindIP.Text = "Exchange Server Settings";
            // 
            // chkEnableRSAEncrypt
            // 
            this.chkEnableRSAEncrypt.AutoSize = true;
            this.chkEnableRSAEncrypt.BackColor = System.Drawing.Color.White;
            this.chkEnableRSAEncrypt.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkEnableRSAEncrypt.ForeColor = System.Drawing.Color.Navy;
            this.chkEnableRSAEncrypt.Location = new System.Drawing.Point(14, 99);
            this.chkEnableRSAEncrypt.Name = "chkEnableRSAEncrypt";
            this.chkEnableRSAEncrypt.Size = new System.Drawing.Size(141, 17);
            this.chkEnableRSAEncrypt.TabIndex = 17;
            this.chkEnableRSAEncrypt.Text = "Enable RSA Encrypt";
            this.chkEnableRSAEncrypt.UseVisualStyleBackColor = false;
            // 
            // chkDisableMachine
            // 
            this.chkDisableMachine.AutoSize = true;
            this.chkDisableMachine.BackColor = System.Drawing.Color.White;
            this.chkDisableMachine.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkDisableMachine.ForeColor = System.Drawing.Color.Navy;
            this.chkDisableMachine.Location = new System.Drawing.Point(13, 62);
            this.chkDisableMachine.Name = "chkDisableMachine";
            this.chkDisableMachine.Size = new System.Drawing.Size(191, 17);
            this.chkDisableMachine.TabIndex = 16;
            this.chkDisableMachine.Text = "Disable Machine on Removal";
            this.chkDisableMachine.UseVisualStyleBackColor = false;
            // 
            // chkEnableEncrypt
            // 
            this.chkEnableEncrypt.AutoSize = true;
            this.chkEnableEncrypt.BackColor = System.Drawing.Color.White;
            this.chkEnableEncrypt.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkEnableEncrypt.ForeColor = System.Drawing.Color.Navy;
            this.chkEnableEncrypt.Location = new System.Drawing.Point(13, 30);
            this.chkEnableEncrypt.Name = "chkEnableEncrypt";
            this.chkEnableEncrypt.Size = new System.Drawing.Size(112, 17);
            this.chkEnableEncrypt.TabIndex = 15;
            this.chkEnableEncrypt.Text = "Enable Encrypt";
            this.chkEnableEncrypt.UseVisualStyleBackColor = false;
            // 
            // cmbLanIP
            // 
            this.cmbLanIP.BackColor = System.Drawing.Color.White;
            this.cmbLanIP.FormattingEnabled = true;
            this.cmbLanIP.Location = new System.Drawing.Point(317, 97);
            this.cmbLanIP.Name = "cmbLanIP";
            this.cmbLanIP.Size = new System.Drawing.Size(136, 21);
            this.cmbLanIP.TabIndex = 0;
            // 
            // lblLocalIP
            // 
            this.lblLocalIP.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLocalIP.Location = new System.Drawing.Point(199, 98);
            this.lblLocalIP.Name = "lblLocalIP";
            this.lblLocalIP.Size = new System.Drawing.Size(112, 21);
            this.lblLocalIP.TabIndex = 12;
            this.lblLocalIP.Text = "Corporate LAN";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(234, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(223, 40);
            this.label2.TabIndex = 14;
            this.label2.Text = "(The IP address selected must be part of the same network as the Exchange Server " +
                "or Exchange Client will not function correctly.)";
            // 
            // gpExchangeSetup
            // 
            this.gpExchangeSetup.Controls.Add(this.gpExCredentials);
            this.gpExchangeSetup.Controls.Add(this.gpexActions);
            this.gpExchangeSetup.Location = new System.Drawing.Point(3, 0);
            this.gpExchangeSetup.Name = "gpExchangeSetup";
            this.gpExchangeSetup.Size = new System.Drawing.Size(482, 168);
            this.gpExchangeSetup.TabIndex = 1;
            this.gpExchangeSetup.TabStop = false;
            this.gpExchangeSetup.Text = " Exchange Server Connection Setup";
            // 
            // gpExCredentials
            // 
            this.gpExCredentials.Controls.Add(this.txtexchangeTimeOut);
            this.gpExCredentials.Controls.Add(this.lblexchangeTimeOut);
            this.gpExCredentials.Controls.Add(this.lblexDBName);
            this.gpExCredentials.Controls.Add(this.lblexchangeDBname);
            this.gpExCredentials.Controls.Add(this.txtexchangePassword);
            this.gpExCredentials.Controls.Add(this.lblexPassword);
            this.gpExCredentials.Controls.Add(this.txtexchangeInstance);
            this.gpExCredentials.Controls.Add(this.lblexInstance);
            this.gpExCredentials.Controls.Add(this.txtexchangeUsername);
            this.gpExCredentials.Controls.Add(this.lblexUsername);
            this.gpExCredentials.Controls.Add(this.txtexchangeServer);
            this.gpExCredentials.Controls.Add(this.lblexServer);
            this.gpExCredentials.Location = new System.Drawing.Point(6, 14);
            this.gpExCredentials.Name = "gpExCredentials";
            this.gpExCredentials.Size = new System.Drawing.Size(471, 102);
            this.gpExCredentials.TabIndex = 2;
            this.gpExCredentials.TabStop = false;
            // 
            // txtexchangeTimeOut
            // 
            this.txtexchangeTimeOut.Location = new System.Drawing.Point(319, 73);
            this.txtexchangeTimeOut.MaxLength = 3;
            this.txtexchangeTimeOut.Name = "txtexchangeTimeOut";
            this.txtexchangeTimeOut.Size = new System.Drawing.Size(64, 20);
            this.txtexchangeTimeOut.TabIndex = 8;
            this.txtexchangeTimeOut.Text = "30";
            this.txtexchangeTimeOut.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtexchangeTimeOut_KeyPress);
            // 
            // lblexchangeTimeOut
            // 
            this.lblexchangeTimeOut.AutoSize = true;
            this.lblexchangeTimeOut.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblexchangeTimeOut.Location = new System.Drawing.Point(198, 76);
            this.lblexchangeTimeOut.Name = "lblexchangeTimeOut";
            this.lblexchangeTimeOut.Size = new System.Drawing.Size(119, 13);
            this.lblexchangeTimeOut.TabIndex = 18;
            this.lblexchangeTimeOut.Text = "Time Out (Seconds)";
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
            // lblexchangeDBname
            // 
            this.lblexchangeDBname.AutoSize = true;
            this.lblexchangeDBname.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblexchangeDBname.Location = new System.Drawing.Point(74, 76);
            this.lblexchangeDBname.Name = "lblexchangeDBname";
            this.lblexchangeDBname.Size = new System.Drawing.Size(63, 13);
            this.lblexchangeDBname.TabIndex = 17;
            this.lblexchangeDBname.Text = "Exchange";
            // 
            // txtexchangePassword
            // 
            this.txtexchangePassword.BackColor = System.Drawing.Color.White;
            this.txtexchangePassword.Location = new System.Drawing.Point(319, 45);
            this.txtexchangePassword.Name = "txtexchangePassword";
            this.txtexchangePassword.PasswordChar = '*';
            this.txtexchangePassword.Size = new System.Drawing.Size(136, 20);
            this.txtexchangePassword.TabIndex = 7;
            // 
            // lblexPassword
            // 
            this.lblexPassword.AutoSize = true;
            this.lblexPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblexPassword.Location = new System.Drawing.Point(256, 48);
            this.lblexPassword.Name = "lblexPassword";
            this.lblexPassword.Size = new System.Drawing.Size(61, 13);
            this.lblexPassword.TabIndex = 14;
            this.lblexPassword.Text = "Password";
            // 
            // txtexchangeInstance
            // 
            this.txtexchangeInstance.BackColor = System.Drawing.Color.White;
            this.txtexchangeInstance.Location = new System.Drawing.Point(319, 14);
            this.txtexchangeInstance.Name = "txtexchangeInstance";
            this.txtexchangeInstance.Size = new System.Drawing.Size(136, 20);
            this.txtexchangeInstance.TabIndex = 5;
            // 
            // lblexInstance
            // 
            this.lblexInstance.AutoSize = true;
            this.lblexInstance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblexInstance.Location = new System.Drawing.Point(261, 17);
            this.lblexInstance.Name = "lblexInstance";
            this.lblexInstance.Size = new System.Drawing.Size(56, 13);
            this.lblexInstance.TabIndex = 12;
            this.lblexInstance.Text = "Instance";
            // 
            // txtexchangeUsername
            // 
            this.txtexchangeUsername.BackColor = System.Drawing.Color.White;
            this.txtexchangeUsername.Location = new System.Drawing.Point(77, 45);
            this.txtexchangeUsername.Name = "txtexchangeUsername";
            this.txtexchangeUsername.Size = new System.Drawing.Size(136, 20);
            this.txtexchangeUsername.TabIndex = 6;
            // 
            // lblexUsername
            // 
            this.lblexUsername.AutoSize = true;
            this.lblexUsername.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblexUsername.Location = new System.Drawing.Point(4, 48);
            this.lblexUsername.Name = "lblexUsername";
            this.lblexUsername.Size = new System.Drawing.Size(63, 13);
            this.lblexUsername.TabIndex = 10;
            this.lblexUsername.Text = "Username";
            // 
            // txtexchangeServer
            // 
            this.txtexchangeServer.BackColor = System.Drawing.Color.White;
            this.txtexchangeServer.Location = new System.Drawing.Point(77, 14);
            this.txtexchangeServer.Name = "txtexchangeServer";
            this.txtexchangeServer.Size = new System.Drawing.Size(136, 20);
            this.txtexchangeServer.TabIndex = 4;
            // 
            // lblexServer
            // 
            this.lblexServer.AutoSize = true;
            this.lblexServer.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblexServer.Location = new System.Drawing.Point(4, 17);
            this.lblexServer.Name = "lblexServer";
            this.lblexServer.Size = new System.Drawing.Size(44, 13);
            this.lblexServer.TabIndex = 8;
            this.lblexServer.Text = "Server";
            // 
            // gpexActions
            // 
            this.gpexActions.Controls.Add(this.btnDSNSave);
            this.gpexActions.Controls.Add(this.btnSaveExchangeConnection);
            this.gpexActions.Controls.Add(this.btnExchangeTestConnection);
            this.gpexActions.Controls.Add(this.btnexchangeDBRestore);
            this.gpexActions.Location = new System.Drawing.Point(5, 120);
            this.gpexActions.Name = "gpexActions";
            this.gpexActions.Size = new System.Drawing.Size(471, 42);
            this.gpexActions.TabIndex = 3;
            this.gpexActions.TabStop = false;
            this.gpexActions.Text = "Actions";
            // 
            // btnDSNSave
            // 
            this.btnDSNSave.BackColor = System.Drawing.Color.White;
            this.btnDSNSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDSNSave.Location = new System.Drawing.Point(253, 12);
            this.btnDSNSave.Name = "btnDSNSave";
            this.btnDSNSave.Size = new System.Drawing.Size(122, 25);
            this.btnDSNSave.TabIndex = 3;
            this.btnDSNSave.Text = "Create ODBC DSN ";
            this.btnDSNSave.UseVisualStyleBackColor = false;
            this.btnDSNSave.Click += new System.EventHandler(this.btnDSNSave_Click);
            // 
            // btnSaveExchangeConnection
            // 
            this.btnSaveExchangeConnection.BackColor = System.Drawing.Color.White;
            this.btnSaveExchangeConnection.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveExchangeConnection.Location = new System.Drawing.Point(130, 12);
            this.btnSaveExchangeConnection.Name = "btnSaveExchangeConnection";
            this.btnSaveExchangeConnection.Size = new System.Drawing.Size(122, 25);
            this.btnSaveExchangeConnection.TabIndex = 1;
            this.btnSaveExchangeConnection.Text = "Save  Connection";
            this.btnSaveExchangeConnection.UseVisualStyleBackColor = false;
            this.btnSaveExchangeConnection.Click += new System.EventHandler(this.btnSaveExchangeConnection_Click);
            // 
            // btnExchangeTestConnection
            // 
            this.btnExchangeTestConnection.BackColor = System.Drawing.Color.White;
            this.btnExchangeTestConnection.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExchangeTestConnection.Location = new System.Drawing.Point(10, 12);
            this.btnExchangeTestConnection.Name = "btnExchangeTestConnection";
            this.btnExchangeTestConnection.Size = new System.Drawing.Size(114, 25);
            this.btnExchangeTestConnection.TabIndex = 0;
            this.btnExchangeTestConnection.Text = "Test Connection";
            this.btnExchangeTestConnection.UseVisualStyleBackColor = false;
            this.btnExchangeTestConnection.Click += new System.EventHandler(this.btnExchangeTestConnection_Click);
            // 
            // btnexchangeDBRestore
            // 
            this.btnexchangeDBRestore.BackColor = System.Drawing.Color.White;
            this.btnexchangeDBRestore.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnexchangeDBRestore.Location = new System.Drawing.Point(377, 12);
            this.btnexchangeDBRestore.Name = "btnexchangeDBRestore";
            this.btnexchangeDBRestore.Size = new System.Drawing.Size(94, 25);
            this.btnexchangeDBRestore.TabIndex = 2;
            this.btnexchangeDBRestore.Text = "Restore DB";
            this.btnexchangeDBRestore.UseVisualStyleBackColor = false;
            this.btnexchangeDBRestore.Click += new System.EventHandler(this.btnexchangeDBRestore_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // errValidate
            // 
            this.errValidate.ContainerControl = this;
            // 
            // frmBMCExchangeConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(997, 617);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmBMCExchangeConfig";
            this.Text = "BMC Exchange Configuration";
            this.Load += new System.EventHandler(this.frmBMCExchangeConfig_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmBMCExchangeConfig_FormClosed);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmBMCExchangeConfig_FormClosing);
            this.panel1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.gpDHCPSettings.ResumeLayout(false);
            this.gpDHCPSettings.PerformLayout();
            this.gpWebServiceSetup.ResumeLayout(false);
            this.gpLoccode.ResumeLayout(false);
            this.gpLoccode.PerformLayout();
            this.gpWindowsService.ResumeLayout(false);
            this.gpCMPSetup.ResumeLayout(false);
            this.gpCMPCredentials.ResumeLayout(false);
            this.gpCMPCredentials.PerformLayout();
            this.gpCMPActions.ResumeLayout(false);
            this.gpCMPActions.PerformLayout();
            this.gpTicketSetup.ResumeLayout(false);
            this.gpTktCredentials.ResumeLayout(false);
            this.gpTktCredentials.PerformLayout();
            this.gpTKActions.ResumeLayout(false);
            this.gpTKActions.PerformLayout();
            this.gpLocalBindIP.ResumeLayout(false);
            this.gpLocalBindIP.PerformLayout();
            this.gpExchangeSetup.ResumeLayout(false);
            this.gpExCredentials.ResumeLayout(false);
            this.gpExCredentials.PerformLayout();
            this.gpexActions.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errValidate)).EndInit();
            this.ResumeLayout(false);

                    }

                    #endregion

        private System.Windows.Forms.Panel panel1;
                    private System.Windows.Forms.GroupBox gpExchangeSetup;
        private System.Windows.Forms.GroupBox gpWebServiceSetup;
                    private System.Windows.Forms.GroupBox gpExCredentials;
                    private System.Windows.Forms.TextBox txtexchangePassword;
                    private System.Windows.Forms.Label lblexPassword;
                    private System.Windows.Forms.TextBox txtexchangeInstance;
                    private System.Windows.Forms.Label lblexInstance;
                    private System.Windows.Forms.TextBox txtexchangeUsername;
                    private System.Windows.Forms.Label lblexUsername;
                    private System.Windows.Forms.TextBox txtexchangeServer;
        private System.Windows.Forms.Label lblexServer;
        private System.Windows.Forms.Button btnexchangeDBRestore;
        private System.Windows.Forms.Button btnExchangeTestConnection;
                    private System.Windows.Forms.GroupBox gpWindowsService;
                    private System.Windows.Forms.Button btnStartService;
        private System.Windows.Forms.Button btnCreateMSMQ;
        private System.Windows.Forms.GroupBox gpLoccode;
        private System.Windows.Forms.GroupBox gpLocalBindIP;
        private System.Windows.Forms.ComboBox cmbLanIP;
        private System.Windows.Forms.Label lblLocalIP;
        private System.Windows.Forms.GroupBox gpTicketSetup;
        private System.Windows.Forms.GroupBox gpTktCredentials;
        private System.Windows.Forms.TextBox txtticketTimeout;
        private System.Windows.Forms.Label lblticketTimeOut;
        private System.Windows.Forms.Label lbltkdbname;
        private System.Windows.Forms.Label lblticketDBname;
        private System.Windows.Forms.TextBox txtticketPassword;
        private System.Windows.Forms.Label lbltkPassword;
        private System.Windows.Forms.TextBox txticketInstance;
        private System.Windows.Forms.Label lbltkInstance;
        private System.Windows.Forms.TextBox txtticketusername;
        private System.Windows.Forms.Label lbltkUsername;
        private System.Windows.Forms.TextBox txtticketserver;
        private System.Windows.Forms.Label lbltkServer;
        private System.Windows.Forms.GroupBox gpTKActions;
        private System.Windows.Forms.Button btnTicketingTestConnection;
        private System.Windows.Forms.Button btnTicketDBRestore;
        private System.Windows.Forms.TextBox txtexchangeTimeOut;
        private System.Windows.Forms.Label lblexchangeTimeOut;
        private System.Windows.Forms.Label lblexDBName;
        private System.Windows.Forms.Label lblexchangeDBname;
        private System.Windows.Forms.GroupBox gpexActions;
        private System.Windows.Forms.GroupBox gpCMPSetup;
        private System.Windows.Forms.GroupBox gpCMPCredentials;
        private System.Windows.Forms.TextBox txtCMPtimeout;
        private System.Windows.Forms.Label lblCMPTimeout;
        private System.Windows.Forms.Label lblCMPDbname;
        private System.Windows.Forms.Label lblCMPDB;
        private System.Windows.Forms.TextBox txtCMPPassword;
        private System.Windows.Forms.Label lblCMPPassword;
        private System.Windows.Forms.TextBox txtCMPInstance;
        private System.Windows.Forms.Label lblCMPInstance;
        private System.Windows.Forms.TextBox txtCMPUsername;
        private System.Windows.Forms.Label lblCMPUsername;
        private System.Windows.Forms.TextBox txtCMPServer;
        private System.Windows.Forms.Label lblCMPServer;
        private System.Windows.Forms.GroupBox gpCMPActions;
        private System.Windows.Forms.Button btnCMPGatewayTestConnection;
        private System.Windows.Forms.Button btnCMPGatewaySaveChanges;
        private System.Windows.Forms.CheckBox chkUseExchangeConnection;
        private System.Windows.Forms.CheckBox chkUseExchangeConnect;
        private System.Windows.Forms.TextBox txtEnterpriseweburl;
        private System.Windows.Forms.Label lblWebServiceUrl;
        private System.Windows.Forms.ListView lvServiceslist;
        private System.Windows.Forms.GroupBox gpDHCPSettings;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkEnableDHCP;
        private System.Windows.Forms.Button btnTestURL;
        private System.Windows.Forms.Button btnEndService;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ProgressBar ProgressBarServices;
        private System.Windows.Forms.ErrorProvider errValidate;
        private System.Windows.Forms.ComboBox cmbSlotLan;
        private System.Windows.Forms.TextBox txtLocCode;
        private System.Windows.Forms.Label lblLocCode;
        private System.Windows.Forms.Button btnSaveExchangeConnection;
        private System.Windows.Forms.Button btnRefreshServices;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnRunUpgradeScript;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnSaveSettings;
        private System.Windows.Forms.TextBox txtInterfaceIP;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtMultiCastIP;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnSelectAll;
        private System.Windows.Forms.CheckBox chkEnableEncrypt;
        private System.Windows.Forms.Button btnDSNSave;
        private System.Windows.Forms.CheckBox chkDisableMachine;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chkEnableRSAEncrypt;
        private System.Windows.Forms.CheckBox chkTrusted;

        }

     
    }


