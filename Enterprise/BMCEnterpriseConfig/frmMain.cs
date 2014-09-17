using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics; //Shell and more
using System.Drawing;
using System.IO; //files
using System.Reflection; //Weird stuff
using System.Runtime.InteropServices; //ODBC dll import
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using BMC.Business.EnterpriseConfig;
using BMC.Common.ExceptionManagement;
using BMC.Common.LogManagement;
using BMC.Common.Security;
using BMC.Monitoring;
using Microsoft.Win32; //Registry
using System.Threading;
using BMC.Common.Utilities;
using BMC.Common;
using BMC.CoreLib.Win32;

namespace BMC.UI.EnterpriseConfig
{

    public class Form1 : System.Windows.Forms.Form
    {

        #region variables
        private bool blsRegulatoryEnabled = false;
        private string sRegulatoryType = string.Empty;
        private string ConnectType = "SQL";
        string nameVersion = ResourceExtensions.GetResourceTextByKey(null, "Key_BMCEnterpriseConfig");//"Bally Multi Connect - Enterprise Configuration ";
        private const string ENTERPRISEDBNAME = "Enterprise";
        private string strUrlvalidate = string.Empty;
        string svrName, userName, passWord, dataBase, instName = "", sTimeOut = "";
        string Services = string.Empty;
        string strDataFilePath = string.Empty;
        string strLogFilePath = string.Empty;

        delegate void dUpdateStatus(string sMsg, TextBox txt);
        Thread tProcess = null;
        frmInfiniteProgressBar oFrmInfiniteProgressBar = null;
        #endregion variables

        #region Form generated variables

        private System.Windows.Forms.TextBox txtStatus;
        private System.Windows.Forms.FolderBrowserDialog fldBNet;
        private System.Windows.Forms.MainMenu mainMenu1;
        private System.Windows.Forms.FolderBrowserDialog fldBLocal;
        private GroupBox grpEnterpriseSetup;
        private Label lblExDBName;
        private TextBox txtSvrName;
        private TextBox txtPassword;
        private Label lblPassword;
        private Label lblInstanceName;
        private Label lblDatabaseName;
        private Label lblServerName;
        private TextBox txtInstName;
        private Label lblUsername;
        private TextBox txtUserName;
        private Panel panel1;
        private Label label11;
        private Label label13;
        private ComboBox cmbLocalIPs;
        private Label label15;
        private Button button2;
        private Label label9;
        private Label lblSec;
        private Label lblConnectionTimeOut;
        private TextBox txtSQLTimeout;
        private Button btnTestConn;
        private Button btnEntKey;
        private Button btnStartServices;
        private Button btnSaveConn;
        private Button btnRunScript;
        private Button btnDeployReports;
        private GroupBox gpActions;
        private Button btnRestoreEnterprise;
        private GroupBox grpReportsSetting;
        private Label lblmessage;
        private Label lblRSInstance;
        private TextBox txtRSInstance;
        private TextBox txtReportFolder;
        private Label lblReportFolder;
        private TextBox txtReportServer;
        private Label lblReportServer;
        private GroupBox grpCertificate;
        private TextBox txtCertificateissuer;
        private Label lblCertificateissuer;
        private CheckBox chkCertificateRequired;
        private GroupBox gpSSIS;
        private TextBox txtDatFiles;
        private Label lblDatFiles;
        private Button btnBrowse;
        private GroupBox grpGardianServerIP;
        private Label lblGuardianServerIP;
        private TextBox txtGuardianServerIP;
        private GroupBox gpSTMSetings;
        private CheckBox chkEnableTransmit;
        private Label lblSTMServerURL;
        private TextBox txtEventServer;
        private Label lblCommandTimeout;
        private TextBox txtCommandTimeout;
        private Label lblSeconds;
        private GroupBox EBSgroupbox;
        private Label Ebslableurl;
        private TextBox txtEBSURL;
        private CheckBox chkEnableEBSComm;
        private GroupBox grpGeneralSettings;
        private Button btnBrowseLogPath;
        private TextBox txtLogFilePath;
        private Label lblLogFilePath;
        private IContainer components;
        private TableLayoutPanel tblContainer;
        private TableLayoutPanel tblFooter;
        private Button btnClose;
        private TableLayoutPanel tblHeaderContent;
        private TableLayoutPanel tblEnterpriseSetup;
        private TableLayoutPanel tableLayoutPanel1;
        private TableLayoutPanel tableLayoutPanel2;
        private TableLayoutPanel tblCertificateSettings;
        private TableLayoutPanel tblSTMServerSetUp;
        private TableLayoutPanel tblEBSServerSetup;
        private TableLayoutPanel tblGeneralSetting;
        private FlowLayoutPanel flowLayoutPanel1;
        private FolderBrowserDialog FdbrowserLogPath;

        #endregion Form generated variables

        #region form related/main

        public Form1()
        {
            InitializeComponent();
            oFrmInfiniteProgressBar = new frmInfiniteProgressBar();
            //nameVersion = BMC.CoreLib.Extensions.GetAppSettingValue("EnterpriseConfigCaption", "Bally MultiConnect - Enterprise Config");
            setTagProperty();
        }

        private void setTagProperty()
        {
           
            this.grpEnterpriseSetup.Tag = "Key_EnterpriseSetup";
            this.lblSeconds.Tag = "Key_Seconds";
            this.lblCommandTimeout.Tag = "Key_CommandTimeoutColon";
            this.btnRestoreEnterprise.Tag = "Key_CreateDatabase";
            this.lblSec.Tag = "Key_Seconds";
            this.btnTestConn.Tag = "Key_TestConnection";
            this.lblConnectionTimeOut.Tag = "Key_ConnectionTimeoutColon";
            //this.lblExDBName.Tag = "Key_EnterpriseColon";
            this.lblPassword.Tag = "Key_PasswordColon";
            this.lblInstanceName.Tag = "Key_InstanceNameColon";
            this.lblDatabaseName.Tag = "Key_DatabaseNameColon";
            this.lblServerName.Tag = "Key_ServerNameColon";
            this.lblUsername.Tag = "Key_UserName";
            this.label11.Tag = "Key_ItemsREQUIRED";
            this.label13.Tag = "Key_ItemsthatareOptional";
            this.label15.Tag = "Key_IPAddress_Part_of_SameNetwork";
            this.button2.Tag = "Key_RestoreDatabase";
            this.label9.Tag = "Key_RestoreBlankDB";
            this.btnEntKey.Tag = "Key_CreateEnterpriseKey";
            this.btnStartServices.Tag = "Key_StartServices";
            this.btnSaveConn.Tag = "Key_SaveAllSettings";
            this.btnRunScript.Tag = "Key_RunScripts";
            this.btnDeployReports.Tag = "Key_DeployReports";
            this.grpReportsSetting.Tag = "Key_ReportsSetting";
            this.lblmessage.Tag = "Key_RegionBasedFolders";
            this.lblRSInstance.Tag = "Key_InstanceName";
            this.lblReportFolder.Tag = "Key_ReportsFolder";
            this.lblReportServer.Tag = "Key_ReportServer";
            this.grpCertificate.Tag = "Key_CertificateSettings";
            this.chkCertificateRequired.Tag = "Key_CertificateEnabled";
            this.lblCertificateissuer.Tag = "Key_IssuerName";
            this.gpSSIS.Tag = "Key_SSISSettings";
            this.lblDatFiles.Tag = "Key_DatFilesFolder";
            this.lblGuardianServerIP.Tag = "Key_GuardianServerIP";
            this.gpSTMSetings.Tag = "Key_STMServerSetup";
            this.chkEnableTransmit.Tag = "Key_EnableTransmitter";
            this.lblSTMServerURL.Tag = "Key_ServerURLColon";
            this.EBSgroupbox.Tag = "Key_EBSServerSetup";
            this.chkEnableEBSComm.Tag = "Key_EnableEBSCommunication";
            this.Ebslableurl.Tag = "Key_EBSURLColon";

        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        private void Form1_Load(object sender, System.EventArgs e)
        {
            try
            {
                chkEnableEBSComm_CheckedChanged(sender, e);

                chkEnableTransmit_CheckedChanged(sender, e);

                lblExDBName.Text = ENTERPRISEDBNAME;

                //addStatus("Initialising...");
                addStatus(this.GetResourceTextByKey("Key_EC_Initialising"));
                this.Text = nameVersion;

                //addStatus("Done!");
                addStatus(this.GetResourceTextByKey("Key_EC_Done_Exclamation"));

                CreateLogDir();

                GetConnRegSettings(true);

                GetInitialSettings();

                GetLogPath();

                sizeControls();

                this.ResolveResources();
            }
            catch (Exception ex)
            {
                //addStatus("Error: " + ex.Message);
                addStatus(this.GetResourceTextByKey("Key_EC_Errror") + ex.Message);
            }
        }

        #endregion form related/main

        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.txtStatus = new System.Windows.Forms.TextBox();
            this.fldBNet = new System.Windows.Forms.FolderBrowserDialog();
            this.mainMenu1 = new System.Windows.Forms.MainMenu(this.components);
            this.fldBLocal = new System.Windows.Forms.FolderBrowserDialog();
            this.grpEnterpriseSetup = new System.Windows.Forms.GroupBox();
            this.tblEnterpriseSetup = new System.Windows.Forms.TableLayoutPanel();
            this.lblServerName = new System.Windows.Forms.Label();
            this.txtSvrName = new System.Windows.Forms.TextBox();
            this.lblUsername = new System.Windows.Forms.Label();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.lblInstanceName = new System.Windows.Forms.Label();
            this.txtInstName = new System.Windows.Forms.TextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.lblConnectionTimeOut = new System.Windows.Forms.Label();
            this.lblCommandTimeout = new System.Windows.Forms.Label();
            this.txtSQLTimeout = new System.Windows.Forms.TextBox();
            this.txtCommandTimeout = new System.Windows.Forms.TextBox();
            this.lblSec = new System.Windows.Forms.Label();
            this.lblSeconds = new System.Windows.Forms.Label();
            this.lblDatabaseName = new System.Windows.Forms.Label();
            this.btnRestoreEnterprise = new System.Windows.Forms.Button();
            this.btnTestConn = new System.Windows.Forms.Button();
            this.lblExDBName = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label11 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.cmbLocalIPs = new System.Windows.Forms.ComboBox();
            this.label15 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.btnEntKey = new System.Windows.Forms.Button();
            this.btnStartServices = new System.Windows.Forms.Button();
            this.btnSaveConn = new System.Windows.Forms.Button();
            this.btnRunScript = new System.Windows.Forms.Button();
            this.btnDeployReports = new System.Windows.Forms.Button();
            this.gpActions = new System.Windows.Forms.GroupBox();
            this.grpReportsSetting = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lblmessage = new System.Windows.Forms.Label();
            this.lblReportServer = new System.Windows.Forms.Label();
            this.txtReportFolder = new System.Windows.Forms.TextBox();
            this.txtRSInstance = new System.Windows.Forms.TextBox();
            this.lblRSInstance = new System.Windows.Forms.Label();
            this.txtReportServer = new System.Windows.Forms.TextBox();
            this.lblReportFolder = new System.Windows.Forms.Label();
            this.grpCertificate = new System.Windows.Forms.GroupBox();
            this.tblCertificateSettings = new System.Windows.Forms.TableLayoutPanel();
            this.chkCertificateRequired = new System.Windows.Forms.CheckBox();
            this.txtCertificateissuer = new System.Windows.Forms.TextBox();
            this.lblCertificateissuer = new System.Windows.Forms.Label();
            this.gpSSIS = new System.Windows.Forms.GroupBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.txtDatFiles = new System.Windows.Forms.TextBox();
            this.lblDatFiles = new System.Windows.Forms.Label();
            this.grpGardianServerIP = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.txtGuardianServerIP = new System.Windows.Forms.TextBox();
            this.lblGuardianServerIP = new System.Windows.Forms.Label();
            this.gpSTMSetings = new System.Windows.Forms.GroupBox();
            this.tblSTMServerSetUp = new System.Windows.Forms.TableLayoutPanel();
            this.chkEnableTransmit = new System.Windows.Forms.CheckBox();
            this.txtEventServer = new System.Windows.Forms.TextBox();
            this.lblSTMServerURL = new System.Windows.Forms.Label();
            this.EBSgroupbox = new System.Windows.Forms.GroupBox();
            this.tblEBSServerSetup = new System.Windows.Forms.TableLayoutPanel();
            this.chkEnableEBSComm = new System.Windows.Forms.CheckBox();
            this.txtEBSURL = new System.Windows.Forms.TextBox();
            this.Ebslableurl = new System.Windows.Forms.Label();
            this.grpGeneralSettings = new System.Windows.Forms.GroupBox();
            this.tblGeneralSetting = new System.Windows.Forms.TableLayoutPanel();
            this.lblLogFilePath = new System.Windows.Forms.Label();
            this.txtLogFilePath = new System.Windows.Forms.TextBox();
            this.btnBrowseLogPath = new System.Windows.Forms.Button();
            this.FdbrowserLogPath = new System.Windows.Forms.FolderBrowserDialog();
            this.tblContainer = new System.Windows.Forms.TableLayoutPanel();
            this.tblFooter = new System.Windows.Forms.TableLayoutPanel();
            this.btnClose = new System.Windows.Forms.Button();
            this.tblHeaderContent = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.grpEnterpriseSetup.SuspendLayout();
            this.tblEnterpriseSetup.SuspendLayout();
            this.panel1.SuspendLayout();
            this.grpReportsSetting.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.grpCertificate.SuspendLayout();
            this.tblCertificateSettings.SuspendLayout();
            this.gpSSIS.SuspendLayout();
            this.grpGardianServerIP.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.gpSTMSetings.SuspendLayout();
            this.tblSTMServerSetUp.SuspendLayout();
            this.EBSgroupbox.SuspendLayout();
            this.tblEBSServerSetup.SuspendLayout();
            this.grpGeneralSettings.SuspendLayout();
            this.tblGeneralSetting.SuspendLayout();
            this.tblContainer.SuspendLayout();
            this.tblFooter.SuspendLayout();
            this.tblHeaderContent.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtStatus
            // 
            this.txtStatus.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtStatus.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtStatus.ForeColor = System.Drawing.Color.DarkRed;
            this.txtStatus.Location = new System.Drawing.Point(3, 626);
            this.txtStatus.Multiline = true;
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.ReadOnly = true;
            this.txtStatus.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtStatus.Size = new System.Drawing.Size(772, 87);
            this.txtStatus.TabIndex = 9;
            this.txtStatus.TabStop = false;
            // 
            // fldBNet
            // 
            this.fldBNet.Description = "Please browse to the relevant network folder";
            // 
            // fldBLocal
            // 
            this.fldBLocal.Description = "Please browse to the relevant local folder";
            // 
            // grpEnterpriseSetup
            // 
            this.grpEnterpriseSetup.BackColor = System.Drawing.Color.White;
            this.grpEnterpriseSetup.Controls.Add(this.tblEnterpriseSetup);
            this.grpEnterpriseSetup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpEnterpriseSetup.ForeColor = System.Drawing.Color.Coral;
            this.grpEnterpriseSetup.Location = new System.Drawing.Point(0, 30);
            this.grpEnterpriseSetup.Margin = new System.Windows.Forms.Padding(0);
            this.grpEnterpriseSetup.Name = "grpEnterpriseSetup";
            this.grpEnterpriseSetup.Size = new System.Drawing.Size(778, 150);
            this.grpEnterpriseSetup.TabIndex = 1;
            this.grpEnterpriseSetup.TabStop = false;
            this.grpEnterpriseSetup.Text = "Enterprise Setup";
            // 
            // tblEnterpriseSetup
            // 
            this.tblEnterpriseSetup.ColumnCount = 7;
            this.tblEnterpriseSetup.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tblEnterpriseSetup.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblEnterpriseSetup.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tblEnterpriseSetup.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tblEnterpriseSetup.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            this.tblEnterpriseSetup.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tblEnterpriseSetup.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tblEnterpriseSetup.Controls.Add(this.lblServerName, 0, 0);
            this.tblEnterpriseSetup.Controls.Add(this.txtSvrName, 1, 0);
            this.tblEnterpriseSetup.Controls.Add(this.lblUsername, 0, 1);
            this.tblEnterpriseSetup.Controls.Add(this.txtUserName, 1, 1);
            this.tblEnterpriseSetup.Controls.Add(this.lblInstanceName, 2, 0);
            this.tblEnterpriseSetup.Controls.Add(this.txtInstName, 3, 0);
            this.tblEnterpriseSetup.Controls.Add(this.lblPassword, 2, 1);
            this.tblEnterpriseSetup.Controls.Add(this.txtPassword, 3, 1);
            this.tblEnterpriseSetup.Controls.Add(this.lblConnectionTimeOut, 4, 0);
            this.tblEnterpriseSetup.Controls.Add(this.lblCommandTimeout, 4, 1);
            this.tblEnterpriseSetup.Controls.Add(this.txtSQLTimeout, 5, 0);
            this.tblEnterpriseSetup.Controls.Add(this.txtCommandTimeout, 5, 1);
            this.tblEnterpriseSetup.Controls.Add(this.lblSec, 6, 0);
            this.tblEnterpriseSetup.Controls.Add(this.lblSeconds, 6, 1);
            this.tblEnterpriseSetup.Controls.Add(this.lblDatabaseName, 0, 2);
            this.tblEnterpriseSetup.Controls.Add(this.lblExDBName, 1, 2);
            this.tblEnterpriseSetup.Controls.Add(this.flowLayoutPanel1, 3, 2);
            this.tblEnterpriseSetup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblEnterpriseSetup.Location = new System.Drawing.Point(3, 16);
            this.tblEnterpriseSetup.Margin = new System.Windows.Forms.Padding(0);
            this.tblEnterpriseSetup.Name = "tblEnterpriseSetup";
            this.tblEnterpriseSetup.RowCount = 3;
            this.tblEnterpriseSetup.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblEnterpriseSetup.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblEnterpriseSetup.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 41F));
            this.tblEnterpriseSetup.Size = new System.Drawing.Size(772, 131);
            this.tblEnterpriseSetup.TabIndex = 0;
            // 
            // lblServerName
            // 
            this.lblServerName.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblServerName.AutoSize = true;
            this.lblServerName.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblServerName.ForeColor = System.Drawing.Color.DarkOrange;
            this.lblServerName.Location = new System.Drawing.Point(3, 15);
            this.lblServerName.Name = "lblServerName";
            this.lblServerName.Size = new System.Drawing.Size(78, 14);
            this.lblServerName.TabIndex = 0;
            this.lblServerName.Text = "Server Name";
            // 
            // txtSvrName
            // 
            this.txtSvrName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSvrName.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSvrName.Location = new System.Drawing.Point(103, 12);
            this.txtSvrName.MaxLength = 50;
            this.txtSvrName.Name = "txtSvrName";
            this.txtSvrName.Size = new System.Drawing.Size(200, 20);
            this.txtSvrName.TabIndex = 1;
            // 
            // lblUsername
            // 
            this.lblUsername.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblUsername.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUsername.ForeColor = System.Drawing.Color.DarkOrange;
            this.lblUsername.Location = new System.Drawing.Point(3, 59);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(72, 16);
            this.lblUsername.TabIndex = 7;
            this.lblUsername.Text = "User Name";
            // 
            // txtUserName
            // 
            this.txtUserName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtUserName.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUserName.Location = new System.Drawing.Point(103, 57);
            this.txtUserName.MaxLength = 50;
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(200, 20);
            this.txtUserName.TabIndex = 8;
            // 
            // lblInstanceName
            // 
            this.lblInstanceName.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblInstanceName.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInstanceName.ForeColor = System.Drawing.Color.ForestGreen;
            this.lblInstanceName.Location = new System.Drawing.Point(309, 14);
            this.lblInstanceName.Name = "lblInstanceName";
            this.lblInstanceName.Size = new System.Drawing.Size(94, 16);
            this.lblInstanceName.TabIndex = 2;
            this.lblInstanceName.Text = "Instance Name";
            // 
            // txtInstName
            // 
            this.txtInstName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtInstName.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtInstName.Location = new System.Drawing.Point(409, 12);
            this.txtInstName.MaxLength = 50;
            this.txtInstName.Name = "txtInstName";
            this.txtInstName.Size = new System.Drawing.Size(158, 20);
            this.txtInstName.TabIndex = 3;
            // 
            // lblPassword
            // 
            this.lblPassword.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblPassword.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPassword.ForeColor = System.Drawing.Color.DarkOrange;
            this.lblPassword.Location = new System.Drawing.Point(331, 59);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(72, 16);
            this.lblPassword.TabIndex = 9;
            this.lblPassword.Text = "Password";
            // 
            // txtPassword
            // 
            this.txtPassword.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPassword.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPassword.Location = new System.Drawing.Point(409, 57);
            this.txtPassword.MaxLength = 50;
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.ShortcutsEnabled = false;
            this.txtPassword.Size = new System.Drawing.Size(158, 20);
            this.txtPassword.TabIndex = 10;
            // 
            // lblConnectionTimeOut
            // 
            this.lblConnectionTimeOut.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblConnectionTimeOut.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblConnectionTimeOut.ForeColor = System.Drawing.Color.Red;
            this.lblConnectionTimeOut.Location = new System.Drawing.Point(575, 13);
            this.lblConnectionTimeOut.Name = "lblConnectionTimeOut";
            this.lblConnectionTimeOut.Size = new System.Drawing.Size(122, 19);
            this.lblConnectionTimeOut.TabIndex = 4;
            this.lblConnectionTimeOut.Text = "Connection Timeout";
            // 
            // lblCommandTimeout
            // 
            this.lblCommandTimeout.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblCommandTimeout.AutoSize = true;
            this.lblCommandTimeout.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCommandTimeout.ForeColor = System.Drawing.Color.Red;
            this.lblCommandTimeout.Location = new System.Drawing.Point(587, 61);
            this.lblCommandTimeout.Name = "lblCommandTimeout";
            this.lblCommandTimeout.Size = new System.Drawing.Size(110, 13);
            this.lblCommandTimeout.TabIndex = 11;
            this.lblCommandTimeout.Text = "Command Timeout";
            // 
            // txtSQLTimeout
            // 
            this.txtSQLTimeout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSQLTimeout.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.txtSQLTimeout.Location = new System.Drawing.Point(703, 12);
            this.txtSQLTimeout.MaxLength = 3;
            this.txtSQLTimeout.Name = "txtSQLTimeout";
            this.txtSQLTimeout.Size = new System.Drawing.Size(24, 20);
            this.txtSQLTimeout.TabIndex = 5;
            this.txtSQLTimeout.Text = "30";
            // 
            // txtCommandTimeout
            // 
            this.txtCommandTimeout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCommandTimeout.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.txtCommandTimeout.Location = new System.Drawing.Point(703, 57);
            this.txtCommandTimeout.MaxLength = 3;
            this.txtCommandTimeout.Name = "txtCommandTimeout";
            this.txtCommandTimeout.Size = new System.Drawing.Size(24, 20);
            this.txtCommandTimeout.TabIndex = 12;
            this.txtCommandTimeout.Text = "30";
            // 
            // lblSec
            // 
            this.lblSec.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblSec.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblSec.ForeColor = System.Drawing.Color.Red;
            this.lblSec.Location = new System.Drawing.Point(733, 14);
            this.lblSec.Name = "lblSec";
            this.lblSec.Size = new System.Drawing.Size(32, 16);
            this.lblSec.TabIndex = 6;
            this.lblSec.Text = "Seconds";
            // 
            // lblSeconds
            // 
            this.lblSeconds.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblSeconds.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblSeconds.ForeColor = System.Drawing.Color.Red;
            this.lblSeconds.Location = new System.Drawing.Point(733, 59);
            this.lblSeconds.Name = "lblSeconds";
            this.lblSeconds.Size = new System.Drawing.Size(32, 16);
            this.lblSeconds.TabIndex = 13;
            this.lblSeconds.Text = "Seconds";
            // 
            // lblDatabaseName
            // 
            this.lblDatabaseName.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblDatabaseName.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDatabaseName.ForeColor = System.Drawing.Color.DarkOrange;
            this.lblDatabaseName.Location = new System.Drawing.Point(3, 102);
            this.lblDatabaseName.Name = "lblDatabaseName";
            this.lblDatabaseName.Size = new System.Drawing.Size(94, 16);
            this.lblDatabaseName.TabIndex = 14;
            this.lblDatabaseName.Text = "Database Name";
            // 
            // btnRestoreEnterprise
            // 
            this.btnRestoreEnterprise.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnRestoreEnterprise.BackColor = System.Drawing.SystemColors.Control;
            this.btnRestoreEnterprise.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.btnRestoreEnterprise.ForeColor = System.Drawing.Color.Black;
            this.btnRestoreEnterprise.Location = new System.Drawing.Point(3, 3);
            this.btnRestoreEnterprise.Name = "btnRestoreEnterprise";
            this.btnRestoreEnterprise.Size = new System.Drawing.Size(100, 28);
            this.btnRestoreEnterprise.TabIndex = 17;
            this.btnRestoreEnterprise.Text = "Cre&ate Database";
            this.btnRestoreEnterprise.UseVisualStyleBackColor = true;
            this.btnRestoreEnterprise.Click += new System.EventHandler(this.btnRestoreEnterprise_Click);
            // 
            // btnTestConn
            // 
            this.btnTestConn.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnTestConn.BackColor = System.Drawing.SystemColors.Control;
            this.btnTestConn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.btnTestConn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnTestConn.Location = new System.Drawing.Point(109, 3);
            this.btnTestConn.Name = "btnTestConn";
            this.btnTestConn.Size = new System.Drawing.Size(110, 28);
            this.btnTestConn.TabIndex = 16;
            this.btnTestConn.Text = "&Test Connection";
            this.btnTestConn.UseVisualStyleBackColor = true;
            this.btnTestConn.Click += new System.EventHandler(this.btnTestConn_Click);
            // 
            // lblExDBName
            // 
            this.lblExDBName.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblExDBName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblExDBName.ForeColor = System.Drawing.Color.Black;
            this.lblExDBName.Location = new System.Drawing.Point(103, 101);
            this.lblExDBName.Name = "lblExDBName";
            this.lblExDBName.Size = new System.Drawing.Size(71, 19);
            this.lblExDBName.TabIndex = 15;
            this.lblExDBName.Text = "Enterprise";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.label13);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(772, 24);
            this.panel1.TabIndex = 0;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Font = new System.Drawing.Font("Arial", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.DarkOrange;
            this.label11.Location = new System.Drawing.Point(1, 4);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(85, 11);
            this.label11.TabIndex = 0;
            this.label11.Text = "Items REQUIRED ";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.BackColor = System.Drawing.Color.Transparent;
            this.label13.Font = new System.Drawing.Font("Arial", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.ForestGreen;
            this.label13.Location = new System.Drawing.Point(104, 5);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(108, 11);
            this.label13.TabIndex = 1;
            this.label13.Text = "Items that are Optional";
            // 
            // cmbLocalIPs
            // 
            this.cmbLocalIPs.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLocalIPs.Location = new System.Drawing.Point(4, 16);
            this.cmbLocalIPs.Name = "cmbLocalIPs";
            this.cmbLocalIPs.Size = new System.Drawing.Size(116, 21);
            this.cmbLocalIPs.TabIndex = 1;
            // 
            // label15
            // 
            this.label15.Font = new System.Drawing.Font("Arial", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.ForeColor = System.Drawing.Color.Red;
            this.label15.Location = new System.Drawing.Point(4, 40);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(112, 64);
            this.label15.TabIndex = 0;
            this.label15.Text = "The IP address selected MUST be part of the same network as the Enterprise Server" +
    " or Enterprise Client will not function correctly.";
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.SystemColors.Control;
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(357, 16);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(104, 37);
            this.button2.TabIndex = 7;
            this.button2.Text = "Restore Database";
            this.button2.UseVisualStyleBackColor = false;
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.Blue;
            this.label9.Location = new System.Drawing.Point(3, 20);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(337, 33);
            this.label9.TabIndex = 9;
            this.label9.Text = "Restore BLANK Databases for Enterprise and MeterAnalysis DBs (These DBs should no" +
    "t exist already)";
            // 
            // btnEntKey
            // 
            this.btnEntKey.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEntKey.BackColor = System.Drawing.SystemColors.Control;
            this.btnEntKey.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.btnEntKey.ForeColor = System.Drawing.Color.Black;
            this.btnEntKey.Location = new System.Drawing.Point(399, 6);
            this.btnEntKey.Name = "btnEntKey";
            this.btnEntKey.Size = new System.Drawing.Size(140, 27);
            this.btnEntKey.TabIndex = 3;
            this.btnEntKey.Text = "Create &Enterprise Key";
            this.btnEntKey.UseVisualStyleBackColor = true;
            this.btnEntKey.Click += new System.EventHandler(this.btnEntKey_Click);
            // 
            // btnStartServices
            // 
            this.btnStartServices.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStartServices.BackColor = System.Drawing.SystemColors.Control;
            this.btnStartServices.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.btnStartServices.ForeColor = System.Drawing.Color.Black;
            this.btnStartServices.Location = new System.Drawing.Point(545, 6);
            this.btnStartServices.Name = "btnStartServices";
            this.btnStartServices.Size = new System.Drawing.Size(130, 27);
            this.btnStartServices.TabIndex = 4;
            this.btnStartServices.Text = "Start Ser&vices";
            this.btnStartServices.UseVisualStyleBackColor = true;
            this.btnStartServices.Click += new System.EventHandler(this.btnStartServices_Click);
            // 
            // btnSaveConn
            // 
            this.btnSaveConn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveConn.BackColor = System.Drawing.SystemColors.Control;
            this.btnSaveConn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.btnSaveConn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnSaveConn.Location = new System.Drawing.Point(21, 6);
            this.btnSaveConn.Name = "btnSaveConn";
            this.btnSaveConn.Size = new System.Drawing.Size(120, 27);
            this.btnSaveConn.TabIndex = 0;
            this.btnSaveConn.Text = "&Save All Settings";
            this.btnSaveConn.UseVisualStyleBackColor = true;
            this.btnSaveConn.Click += new System.EventHandler(this.btnSaveConn_Click);
            // 
            // btnRunScript
            // 
            this.btnRunScript.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRunScript.BackColor = System.Drawing.SystemColors.Control;
            this.btnRunScript.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.btnRunScript.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnRunScript.Location = new System.Drawing.Point(147, 6);
            this.btnRunScript.Name = "btnRunScript";
            this.btnRunScript.Size = new System.Drawing.Size(120, 27);
            this.btnRunScript.TabIndex = 1;
            this.btnRunScript.Text = "&Run Scripts";
            this.btnRunScript.UseVisualStyleBackColor = true;
            this.btnRunScript.Click += new System.EventHandler(this.btnRunScript_Click);
            // 
            // btnDeployReports
            // 
            this.btnDeployReports.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDeployReports.BackColor = System.Drawing.SystemColors.Control;
            this.btnDeployReports.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.btnDeployReports.ForeColor = System.Drawing.Color.Black;
            this.btnDeployReports.Location = new System.Drawing.Point(273, 6);
            this.btnDeployReports.Name = "btnDeployReports";
            this.btnDeployReports.Size = new System.Drawing.Size(120, 27);
            this.btnDeployReports.TabIndex = 2;
            this.btnDeployReports.Text = "&Deploy Reports";
            this.btnDeployReports.UseVisualStyleBackColor = true;
            this.btnDeployReports.Click += new System.EventHandler(this.btnDeployReports_Click);
            // 
            // gpActions
            // 
            this.gpActions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gpActions.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gpActions.ForeColor = System.Drawing.Color.Coral;
            this.gpActions.Location = new System.Drawing.Point(3, 533);
            this.gpActions.Name = "gpActions";
            this.gpActions.Size = new System.Drawing.Size(772, 87);
            this.gpActions.TabIndex = 8;
            this.gpActions.TabStop = false;
            // 
            // grpReportsSetting
            // 
            this.grpReportsSetting.Controls.Add(this.tableLayoutPanel1);
            this.grpReportsSetting.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpReportsSetting.ForeColor = System.Drawing.Color.Coral;
            this.grpReportsSetting.Location = new System.Drawing.Point(0, 180);
            this.grpReportsSetting.Margin = new System.Windows.Forms.Padding(0);
            this.grpReportsSetting.Name = "grpReportsSetting";
            this.grpReportsSetting.Size = new System.Drawing.Size(778, 78);
            this.grpReportsSetting.TabIndex = 2;
            this.grpReportsSetting.TabStop = false;
            this.grpReportsSetting.Text = "Reports Setting";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 6;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.Controls.Add(this.lblmessage, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblReportServer, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.txtReportFolder, 5, 1);
            this.tableLayoutPanel1.Controls.Add(this.txtRSInstance, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblRSInstance, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.txtReportServer, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblReportFolder, 4, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(772, 59);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // lblmessage
            // 
            this.lblmessage.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tableLayoutPanel1.SetColumnSpan(this.lblmessage, 6);
            this.lblmessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblmessage.ForeColor = System.Drawing.Color.Red;
            this.lblmessage.Location = new System.Drawing.Point(3, 5);
            this.lblmessage.Name = "lblmessage";
            this.lblmessage.Size = new System.Drawing.Size(566, 20);
            this.lblmessage.TabIndex = 0;
            this.lblmessage.Text = "[  If region is US  please give Reports folder name as BMCUSREPORTS else BMCUKREP" +
    "ORTS   ]";
            // 
            // lblReportServer
            // 
            this.lblReportServer.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblReportServer.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReportServer.ForeColor = System.Drawing.Color.DarkOrange;
            this.lblReportServer.Location = new System.Drawing.Point(3, 33);
            this.lblReportServer.Name = "lblReportServer";
            this.lblReportServer.Size = new System.Drawing.Size(91, 23);
            this.lblReportServer.TabIndex = 1;
            this.lblReportServer.Text = "Report Server (IP/Name)";
            // 
            // txtReportFolder
            // 
            this.txtReportFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtReportFolder.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReportFolder.Location = new System.Drawing.Point(633, 34);
            this.txtReportFolder.MaxLength = 50;
            this.txtReportFolder.Name = "txtReportFolder";
            this.txtReportFolder.Size = new System.Drawing.Size(136, 20);
            this.txtReportFolder.TabIndex = 6;
            // 
            // txtRSInstance
            // 
            this.txtRSInstance.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRSInstance.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRSInstance.Location = new System.Drawing.Point(343, 34);
            this.txtRSInstance.MaxLength = 50;
            this.txtRSInstance.Name = "txtRSInstance";
            this.txtRSInstance.Size = new System.Drawing.Size(134, 20);
            this.txtRSInstance.TabIndex = 4;
            // 
            // lblRSInstance
            // 
            this.lblRSInstance.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblRSInstance.AutoSize = true;
            this.lblRSInstance.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRSInstance.ForeColor = System.Drawing.Color.ForestGreen;
            this.lblRSInstance.Location = new System.Drawing.Point(246, 37);
            this.lblRSInstance.Name = "lblRSInstance";
            this.lblRSInstance.Size = new System.Drawing.Size(91, 14);
            this.lblRSInstance.TabIndex = 3;
            this.lblRSInstance.Text = "Instance Name:";
            // 
            // txtReportServer
            // 
            this.txtReportServer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtReportServer.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReportServer.Location = new System.Drawing.Point(103, 34);
            this.txtReportServer.MaxLength = 50;
            this.txtReportServer.Name = "txtReportServer";
            this.txtReportServer.Size = new System.Drawing.Size(134, 20);
            this.txtReportServer.TabIndex = 2;
            // 
            // lblReportFolder
            // 
            this.lblReportFolder.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblReportFolder.AutoSize = true;
            this.lblReportFolder.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReportFolder.ForeColor = System.Drawing.Color.DarkOrange;
            this.lblReportFolder.Location = new System.Drawing.Point(532, 37);
            this.lblReportFolder.Name = "lblReportFolder";
            this.lblReportFolder.Size = new System.Drawing.Size(95, 14);
            this.lblReportFolder.TabIndex = 5;
            this.lblReportFolder.Text = "Reports  Folder:";
            // 
            // grpCertificate
            // 
            this.grpCertificate.Controls.Add(this.tblCertificateSettings);
            this.grpCertificate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpCertificate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpCertificate.ForeColor = System.Drawing.Color.Coral;
            this.grpCertificate.Location = new System.Drawing.Point(0, 316);
            this.grpCertificate.Margin = new System.Windows.Forms.Padding(0);
            this.grpCertificate.Name = "grpCertificate";
            this.grpCertificate.Size = new System.Drawing.Size(778, 54);
            this.grpCertificate.TabIndex = 4;
            this.grpCertificate.TabStop = false;
            this.grpCertificate.Text = "Certificate Settings";
            // 
            // tblCertificateSettings
            // 
            this.tblCertificateSettings.ColumnCount = 3;
            this.tblCertificateSettings.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tblCertificateSettings.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tblCertificateSettings.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblCertificateSettings.Controls.Add(this.chkCertificateRequired, 0, 0);
            this.tblCertificateSettings.Controls.Add(this.txtCertificateissuer, 2, 0);
            this.tblCertificateSettings.Controls.Add(this.lblCertificateissuer, 1, 0);
            this.tblCertificateSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblCertificateSettings.Location = new System.Drawing.Point(3, 16);
            this.tblCertificateSettings.Name = "tblCertificateSettings";
            this.tblCertificateSettings.RowCount = 1;
            this.tblCertificateSettings.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblCertificateSettings.Size = new System.Drawing.Size(772, 35);
            this.tblCertificateSettings.TabIndex = 0;
            // 
            // chkCertificateRequired
            // 
            this.chkCertificateRequired.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.chkCertificateRequired.AutoSize = true;
            this.chkCertificateRequired.Location = new System.Drawing.Point(3, 9);
            this.chkCertificateRequired.Name = "chkCertificateRequired";
            this.chkCertificateRequired.Size = new System.Drawing.Size(134, 17);
            this.chkCertificateRequired.TabIndex = 0;
            this.chkCertificateRequired.Text = "Certificate Enabled";
            this.chkCertificateRequired.UseVisualStyleBackColor = true;
            this.chkCertificateRequired.CheckedChanged += new System.EventHandler(this.chkCertificateRequired_CheckedChanged);
            // 
            // txtCertificateissuer
            // 
            this.txtCertificateissuer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCertificateissuer.Enabled = false;
            this.txtCertificateissuer.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCertificateissuer.Location = new System.Drawing.Point(388, 7);
            this.txtCertificateissuer.MaxLength = 20;
            this.txtCertificateissuer.Name = "txtCertificateissuer";
            this.txtCertificateissuer.Size = new System.Drawing.Size(381, 20);
            this.txtCertificateissuer.TabIndex = 2;
            // 
            // lblCertificateissuer
            // 
            this.lblCertificateissuer.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblCertificateissuer.AutoSize = true;
            this.lblCertificateissuer.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCertificateissuer.ForeColor = System.Drawing.Color.DarkOrange;
            this.lblCertificateissuer.Location = new System.Drawing.Point(234, 10);
            this.lblCertificateissuer.Name = "lblCertificateissuer";
            this.lblCertificateissuer.Size = new System.Drawing.Size(80, 14);
            this.lblCertificateissuer.TabIndex = 1;
            this.lblCertificateissuer.Text = "Issuer Name:";
            // 
            // gpSSIS
            // 
            this.gpSSIS.Controls.Add(this.btnBrowse);
            this.gpSSIS.Controls.Add(this.txtDatFiles);
            this.gpSSIS.Controls.Add(this.lblDatFiles);
            this.gpSSIS.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gpSSIS.ForeColor = System.Drawing.Color.Coral;
            this.gpSSIS.Location = new System.Drawing.Point(57, 70);
            this.gpSSIS.Name = "gpSSIS";
            this.gpSSIS.Size = new System.Drawing.Size(292, 52);
            this.gpSSIS.TabIndex = 1;
            this.gpSSIS.TabStop = false;
            this.gpSSIS.Text = "SSIS Settings";
            this.gpSSIS.Visible = false;
            // 
            // btnBrowse
            // 
            this.btnBrowse.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnBrowse.Location = new System.Drawing.Point(257, 13);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(28, 30);
            this.btnBrowse.TabIndex = 0;
            this.btnBrowse.Text = "...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // txtDatFiles
            // 
            this.txtDatFiles.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDatFiles.Location = new System.Drawing.Point(103, 19);
            this.txtDatFiles.MaxLength = 20;
            this.txtDatFiles.Name = "txtDatFiles";
            this.txtDatFiles.Size = new System.Drawing.Size(148, 20);
            this.txtDatFiles.TabIndex = 2;
            // 
            // lblDatFiles
            // 
            this.lblDatFiles.AutoSize = true;
            this.lblDatFiles.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDatFiles.ForeColor = System.Drawing.Color.DarkOrange;
            this.lblDatFiles.Location = new System.Drawing.Point(6, 23);
            this.lblDatFiles.Name = "lblDatFiles";
            this.lblDatFiles.Size = new System.Drawing.Size(91, 14);
            this.lblDatFiles.TabIndex = 1;
            this.lblDatFiles.Text = "Dat Files Folder";
            // 
            // grpGardianServerIP
            // 
            this.grpGardianServerIP.Controls.Add(this.tableLayoutPanel2);
            this.grpGardianServerIP.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpGardianServerIP.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpGardianServerIP.ForeColor = System.Drawing.Color.Coral;
            this.grpGardianServerIP.Location = new System.Drawing.Point(0, 258);
            this.grpGardianServerIP.Margin = new System.Windows.Forms.Padding(0);
            this.grpGardianServerIP.Name = "grpGardianServerIP";
            this.grpGardianServerIP.Size = new System.Drawing.Size(778, 58);
            this.grpGardianServerIP.TabIndex = 3;
            this.grpGardianServerIP.TabStop = false;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tableLayoutPanel2.Controls.Add(this.txtGuardianServerIP, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.lblGuardianServerIP, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(772, 39);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // txtGuardianServerIP
            // 
            this.txtGuardianServerIP.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtGuardianServerIP.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGuardianServerIP.Location = new System.Drawing.Point(157, 9);
            this.txtGuardianServerIP.Name = "txtGuardianServerIP";
            this.txtGuardianServerIP.Size = new System.Drawing.Size(158, 20);
            this.txtGuardianServerIP.TabIndex = 1;
            // 
            // lblGuardianServerIP
            // 
            this.lblGuardianServerIP.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblGuardianServerIP.AutoSize = true;
            this.lblGuardianServerIP.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGuardianServerIP.ForeColor = System.Drawing.Color.DarkOrange;
            this.lblGuardianServerIP.Location = new System.Drawing.Point(3, 13);
            this.lblGuardianServerIP.Name = "lblGuardianServerIP";
            this.lblGuardianServerIP.Size = new System.Drawing.Size(115, 13);
            this.lblGuardianServerIP.TabIndex = 0;
            this.lblGuardianServerIP.Text = "Guardian Server IP";
            // 
            // gpSTMSetings
            // 
            this.gpSTMSetings.Controls.Add(this.tblSTMServerSetUp);
            this.gpSTMSetings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gpSTMSetings.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gpSTMSetings.ForeColor = System.Drawing.Color.Coral;
            this.gpSTMSetings.Location = new System.Drawing.Point(0, 370);
            this.gpSTMSetings.Margin = new System.Windows.Forms.Padding(0);
            this.gpSTMSetings.Name = "gpSTMSetings";
            this.gpSTMSetings.Size = new System.Drawing.Size(778, 50);
            this.gpSTMSetings.TabIndex = 5;
            this.gpSTMSetings.TabStop = false;
            this.gpSTMSetings.Text = "STM Server Setup";
            // 
            // tblSTMServerSetUp
            // 
            this.tblSTMServerSetUp.ColumnCount = 3;
            this.tblSTMServerSetUp.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tblSTMServerSetUp.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tblSTMServerSetUp.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblSTMServerSetUp.Controls.Add(this.chkEnableTransmit, 0, 0);
            this.tblSTMServerSetUp.Controls.Add(this.txtEventServer, 2, 0);
            this.tblSTMServerSetUp.Controls.Add(this.lblSTMServerURL, 1, 0);
            this.tblSTMServerSetUp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblSTMServerSetUp.Location = new System.Drawing.Point(3, 16);
            this.tblSTMServerSetUp.Name = "tblSTMServerSetUp";
            this.tblSTMServerSetUp.RowCount = 1;
            this.tblSTMServerSetUp.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblSTMServerSetUp.Size = new System.Drawing.Size(772, 31);
            this.tblSTMServerSetUp.TabIndex = 0;
            // 
            // chkEnableTransmit
            // 
            this.chkEnableTransmit.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.chkEnableTransmit.AutoSize = true;
            this.chkEnableTransmit.Location = new System.Drawing.Point(3, 7);
            this.chkEnableTransmit.Name = "chkEnableTransmit";
            this.chkEnableTransmit.Size = new System.Drawing.Size(132, 17);
            this.chkEnableTransmit.TabIndex = 0;
            this.chkEnableTransmit.Text = "Enable Transmitter";
            this.chkEnableTransmit.UseVisualStyleBackColor = true;
            this.chkEnableTransmit.CheckedChanged += new System.EventHandler(this.chkEnableTransmit_CheckedChanged);
            // 
            // txtEventServer
            // 
            this.txtEventServer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtEventServer.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEventServer.Location = new System.Drawing.Point(388, 5);
            this.txtEventServer.MaxLength = 80;
            this.txtEventServer.Name = "txtEventServer";
            this.txtEventServer.Size = new System.Drawing.Size(381, 20);
            this.txtEventServer.TabIndex = 2;
            // 
            // lblSTMServerURL
            // 
            this.lblSTMServerURL.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblSTMServerURL.AutoSize = true;
            this.lblSTMServerURL.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSTMServerURL.ForeColor = System.Drawing.Color.DarkOrange;
            this.lblSTMServerURL.Location = new System.Drawing.Point(311, 8);
            this.lblSTMServerURL.Name = "lblSTMServerURL";
            this.lblSTMServerURL.Size = new System.Drawing.Size(71, 14);
            this.lblSTMServerURL.TabIndex = 1;
            this.lblSTMServerURL.Text = "Server URL:";
            // 
            // EBSgroupbox
            // 
            this.EBSgroupbox.Controls.Add(this.tblEBSServerSetup);
            this.EBSgroupbox.Controls.Add(this.gpSSIS);
            this.EBSgroupbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.EBSgroupbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.EBSgroupbox.ForeColor = System.Drawing.Color.Coral;
            this.EBSgroupbox.Location = new System.Drawing.Point(0, 420);
            this.EBSgroupbox.Margin = new System.Windows.Forms.Padding(0);
            this.EBSgroupbox.Name = "EBSgroupbox";
            this.EBSgroupbox.Size = new System.Drawing.Size(778, 50);
            this.EBSgroupbox.TabIndex = 6;
            this.EBSgroupbox.TabStop = false;
            this.EBSgroupbox.Text = "EBS Server Setup";
            // 
            // tblEBSServerSetup
            // 
            this.tblEBSServerSetup.ColumnCount = 3;
            this.tblEBSServerSetup.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tblEBSServerSetup.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tblEBSServerSetup.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblEBSServerSetup.Controls.Add(this.chkEnableEBSComm, 0, 0);
            this.tblEBSServerSetup.Controls.Add(this.txtEBSURL, 2, 0);
            this.tblEBSServerSetup.Controls.Add(this.Ebslableurl, 1, 0);
            this.tblEBSServerSetup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblEBSServerSetup.Location = new System.Drawing.Point(3, 16);
            this.tblEBSServerSetup.Name = "tblEBSServerSetup";
            this.tblEBSServerSetup.RowCount = 1;
            this.tblEBSServerSetup.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblEBSServerSetup.Size = new System.Drawing.Size(772, 31);
            this.tblEBSServerSetup.TabIndex = 0;
            // 
            // chkEnableEBSComm
            // 
            this.chkEnableEBSComm.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.chkEnableEBSComm.AutoSize = true;
            this.chkEnableEBSComm.Location = new System.Drawing.Point(3, 7);
            this.chkEnableEBSComm.Name = "chkEnableEBSComm";
            this.chkEnableEBSComm.Size = new System.Drawing.Size(182, 17);
            this.chkEnableEBSComm.TabIndex = 0;
            this.chkEnableEBSComm.Text = "Enable EBS Communication";
            this.chkEnableEBSComm.UseVisualStyleBackColor = true;
            this.chkEnableEBSComm.CheckedChanged += new System.EventHandler(this.chkEnableEBSComm_CheckedChanged);
            // 
            // txtEBSURL
            // 
            this.txtEBSURL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtEBSURL.Location = new System.Drawing.Point(388, 5);
            this.txtEBSURL.Name = "txtEBSURL";
            this.txtEBSURL.Size = new System.Drawing.Size(381, 20);
            this.txtEBSURL.TabIndex = 2;
            // 
            // Ebslableurl
            // 
            this.Ebslableurl.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.Ebslableurl.AutoSize = true;
            this.Ebslableurl.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            this.Ebslableurl.Location = new System.Drawing.Point(325, 8);
            this.Ebslableurl.Name = "Ebslableurl";
            this.Ebslableurl.Size = new System.Drawing.Size(57, 14);
            this.Ebslableurl.TabIndex = 1;
            this.Ebslableurl.Text = "EBS URL: ";
            // 
            // grpGeneralSettings
            // 
            this.grpGeneralSettings.Controls.Add(this.tblGeneralSetting);
            this.grpGeneralSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpGeneralSettings.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpGeneralSettings.ForeColor = System.Drawing.Color.Coral;
            this.grpGeneralSettings.Location = new System.Drawing.Point(3, 473);
            this.grpGeneralSettings.Name = "grpGeneralSettings";
            this.grpGeneralSettings.Size = new System.Drawing.Size(772, 54);
            this.grpGeneralSettings.TabIndex = 7;
            this.grpGeneralSettings.TabStop = false;
            this.grpGeneralSettings.Text = "General Settings";
            // 
            // tblGeneralSetting
            // 
            this.tblGeneralSetting.ColumnCount = 3;
            this.tblGeneralSetting.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tblGeneralSetting.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tblGeneralSetting.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.tblGeneralSetting.Controls.Add(this.lblLogFilePath, 0, 0);
            this.tblGeneralSetting.Controls.Add(this.txtLogFilePath, 1, 0);
            this.tblGeneralSetting.Controls.Add(this.btnBrowseLogPath, 2, 0);
            this.tblGeneralSetting.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblGeneralSetting.Location = new System.Drawing.Point(3, 16);
            this.tblGeneralSetting.Margin = new System.Windows.Forms.Padding(0);
            this.tblGeneralSetting.Name = "tblGeneralSetting";
            this.tblGeneralSetting.RowCount = 1;
            this.tblGeneralSetting.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblGeneralSetting.Size = new System.Drawing.Size(766, 35);
            this.tblGeneralSetting.TabIndex = 0;
            // 
            // lblLogFilePath
            // 
            this.lblLogFilePath.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblLogFilePath.AutoSize = true;
            this.lblLogFilePath.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLogFilePath.ForeColor = System.Drawing.Color.DarkOrange;
            this.lblLogFilePath.Location = new System.Drawing.Point(3, 10);
            this.lblLogFilePath.Name = "lblLogFilePath";
            this.lblLogFilePath.Size = new System.Drawing.Size(80, 14);
            this.lblLogFilePath.TabIndex = 2;
            this.lblLogFilePath.Text = "Log File Path:";
            // 
            // txtLogFilePath
            // 
            this.txtLogFilePath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLogFilePath.BackColor = System.Drawing.Color.White;
            this.txtLogFilePath.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLogFilePath.Location = new System.Drawing.Point(222, 7);
            this.txtLogFilePath.MaxLength = 20;
            this.txtLogFilePath.Name = "txtLogFilePath";
            this.txtLogFilePath.ReadOnly = true;
            this.txtLogFilePath.Size = new System.Drawing.Size(507, 20);
            this.txtLogFilePath.TabIndex = 0;
            // 
            // btnBrowseLogPath
            // 
            this.btnBrowseLogPath.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnBrowseLogPath.Location = new System.Drawing.Point(735, 5);
            this.btnBrowseLogPath.Name = "btnBrowseLogPath";
            this.btnBrowseLogPath.Size = new System.Drawing.Size(25, 25);
            this.btnBrowseLogPath.TabIndex = 1;
            this.btnBrowseLogPath.Text = "...";
            this.btnBrowseLogPath.UseVisualStyleBackColor = true;
            this.btnBrowseLogPath.Click += new System.EventHandler(this.btnBrowseLogPath_Click);
            // 
            // FdbrowserLogPath
            // 
            this.FdbrowserLogPath.Description = "Please select the path for the log file";
            // 
            // tblContainer
            // 
            this.tblContainer.ColumnCount = 1;
            this.tblContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblContainer.Controls.Add(this.tblFooter, 0, 1);
            this.tblContainer.Controls.Add(this.tblHeaderContent, 0, 0);
            this.tblContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblContainer.Location = new System.Drawing.Point(0, 0);
            this.tblContainer.Name = "tblContainer";
            this.tblContainer.RowCount = 2;
            this.tblContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tblContainer.Size = new System.Drawing.Size(784, 762);
            this.tblContainer.TabIndex = 0;
            // 
            // tblFooter
            // 
            this.tblFooter.ColumnCount = 7;
            this.tblFooter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblFooter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 126F));
            this.tblFooter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 126F));
            this.tblFooter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 126F));
            this.tblFooter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 146F));
            this.tblFooter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 136F));
            this.tblFooter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 106F));
            this.tblFooter.Controls.Add(this.btnStartServices, 5, 0);
            this.tblFooter.Controls.Add(this.btnEntKey, 4, 0);
            this.tblFooter.Controls.Add(this.btnSaveConn, 1, 0);
            this.tblFooter.Controls.Add(this.btnRunScript, 2, 0);
            this.tblFooter.Controls.Add(this.btnDeployReports, 3, 0);
            this.tblFooter.Controls.Add(this.btnClose, 6, 0);
            this.tblFooter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblFooter.Location = new System.Drawing.Point(0, 722);
            this.tblFooter.Margin = new System.Windows.Forms.Padding(0);
            this.tblFooter.Name = "tblFooter";
            this.tblFooter.RowCount = 1;
            this.tblFooter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblFooter.Size = new System.Drawing.Size(784, 40);
            this.tblFooter.TabIndex = 1;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(681, 6);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(100, 28);
            this.btnClose.TabIndex = 5;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click_1);
            // 
            // tblHeaderContent
            // 
            this.tblHeaderContent.ColumnCount = 1;
            this.tblHeaderContent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblHeaderContent.Controls.Add(this.grpEnterpriseSetup, 0, 1);
            this.tblHeaderContent.Controls.Add(this.panel1, 0, 0);
            this.tblHeaderContent.Controls.Add(this.grpGeneralSettings, 0, 7);
            this.tblHeaderContent.Controls.Add(this.gpActions, 0, 8);
            this.tblHeaderContent.Controls.Add(this.EBSgroupbox, 0, 6);
            this.tblHeaderContent.Controls.Add(this.grpReportsSetting, 0, 2);
            this.tblHeaderContent.Controls.Add(this.gpSTMSetings, 0, 5);
            this.tblHeaderContent.Controls.Add(this.grpGardianServerIP, 0, 3);
            this.tblHeaderContent.Controls.Add(this.grpCertificate, 0, 4);
            this.tblHeaderContent.Controls.Add(this.txtStatus, 0, 9);
            this.tblHeaderContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblHeaderContent.Location = new System.Drawing.Point(3, 3);
            this.tblHeaderContent.Name = "tblHeaderContent";
            this.tblHeaderContent.RowCount = 10;
            this.tblHeaderContent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tblHeaderContent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tblHeaderContent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 78F));
            this.tblHeaderContent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 58F));
            this.tblHeaderContent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 54F));
            this.tblHeaderContent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tblHeaderContent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tblHeaderContent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tblHeaderContent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblHeaderContent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblHeaderContent.Size = new System.Drawing.Size(778, 716);
            this.tblHeaderContent.TabIndex = 0;
            // 
            // flowLayoutPanel1
            // 
            this.tblEnterpriseSetup.SetColumnSpan(this.flowLayoutPanel1, 4);
            this.flowLayoutPanel1.Controls.Add(this.btnRestoreEnterprise);
            this.flowLayoutPanel1.Controls.Add(this.btnTestConn);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(546, 90);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(226, 41);
            this.flowLayoutPanel1.TabIndex = 18;
            // 
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(784, 762);
            this.Controls.Add(this.tblContainer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Menu = this.mainMenu1;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Bally MultiConnect Enterprise Config";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.grpEnterpriseSetup.ResumeLayout(false);
            this.tblEnterpriseSetup.ResumeLayout(false);
            this.tblEnterpriseSetup.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.grpReportsSetting.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.grpCertificate.ResumeLayout(false);
            this.tblCertificateSettings.ResumeLayout(false);
            this.tblCertificateSettings.PerformLayout();
            this.gpSSIS.ResumeLayout(false);
            this.gpSSIS.PerformLayout();
            this.grpGardianServerIP.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.gpSTMSetings.ResumeLayout(false);
            this.tblSTMServerSetUp.ResumeLayout(false);
            this.tblSTMServerSetUp.PerformLayout();
            this.EBSgroupbox.ResumeLayout(false);
            this.tblEBSServerSetup.ResumeLayout(false);
            this.tblEBSServerSetup.PerformLayout();
            this.grpGeneralSettings.ResumeLayout(false);
            this.tblGeneralSetting.ResumeLayout(false);
            this.tblGeneralSetting.PerformLayout();
            this.tblContainer.ResumeLayout(false);
            this.tblFooter.ResumeLayout(false);
            this.tblHeaderContent.ResumeLayout(false);
            this.tblHeaderContent.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion


        #region common functions

        private void CreateLogDir()
        {
            try
            {
                //addStatus("Checking for Logs Dir...");
                addStatus(this.GetResourceTextByKey("Key_EC_CheckingforLogsDir"));

                string logPath = ConfigurationManager.AppSettings.Get("DefaultLogPath");

                if (!Directory.Exists(logPath))
                {
                    if (Directory.Exists(logPath.Substring(0, 2)))
                    {
                        //addStatus("Creating Logs Dir...");
                        addStatus(this.GetResourceTextByKey("Key_EC_CreatingLogsDir"));
                        Directory.CreateDirectory(logPath);
                        //addStatus("Done!");
                        addStatus(this.GetResourceTextByKey("Key_EC_Done_Exclamation"));
                    }
                    else
                    {
                        //addStatus("Logs Dir Not Found!");
                        addStatus(this.GetResourceTextByKey("Key_EC_LogsDirNotFound"));
                    }
                }
                else
                {
                    //addStatus("Logs Dir Found!...");
                    addStatus(this.GetResourceTextByKey("Key_EC_LogsDirFound"));
                }
            }
            catch (Exception ex)
            {
                //addStatus("Error Creating Logs Dir!: " + ex.Message);
                addStatus(this.GetResourceTextByKey("Key_EC_ErrorCreatingLogsDir") + ex.Message);
            }
        }

        private void parseSQLConnect(string SQLConnect, string sDBName)
        { //format SQL connection string from input info
            int startPos, endPos;

            SQLConnect = SQLConnect.Replace("\\", "\\\\");
            startPos = SQLConnect.IndexOf("Server=") + 7;
            endPos = SQLConnect.IndexOf(";", startPos);
            svrName = SQLConnect.Substring(startPos, endPos - startPos);
            startPos = SQLConnect.IndexOf("UID=") + 4;
            endPos = SQLConnect.IndexOf(";", startPos);
            userName = SQLConnect.Substring(startPos, endPos - startPos);
            startPos = SQLConnect.IndexOf("PWD=") + 4;
            endPos = SQLConnect.IndexOf(";", startPos);
            passWord = SQLConnect.Substring(startPos, endPos - startPos);
            startPos = SQLConnect.IndexOf("Initial Catalog=") + 16;
            endPos = SQLConnect.IndexOf(";", startPos);
            dataBase = SQLConnect.Substring(startPos, endPos - startPos);
            if (SQLConnect.Contains("Timeout="))
            {
                startPos = SQLConnect.IndexOf("Timeout=") + 8;
                sTimeOut = SQLConnect.Substring(startPos, SQLConnect.Length - (startPos + 1));
            }
            else
            {
                sTimeOut = "30";
            }

            if (svrName.IndexOf("\\\\") != -1)
            {
                instName = svrName.Substring(svrName.IndexOf("\\\\") + 2);
                svrName = svrName.Substring(0, svrName.IndexOf("\\\\"));
            }
            if (dataBase.IndexOf(";") != -1)
                dataBase = dataBase.Substring(0, dataBase.Length - 1);

            txtSvrName.Text = svrName;
            txtInstName.Text = instName;
            txtUserName.Text = userName;
            txtPassword.Text = passWord;
            txtSQLTimeout.Text = sTimeOut;


        }

        //Add an entry to the log, timestamp it etc
        private void addStatus(string theString)
        {
            DateTime now = DateTime.Now;
            string sMsg = string.Empty;
            string hour, minute, second;
            if (now.Hour < 10)
                hour = "0" + now.Hour.ToString();
            else
                hour = now.Hour.ToString();
            if (now.Minute < 10)
                minute = "0" + now.Minute.ToString();
            else
                minute = now.Minute.ToString();
            if (now.Second < 10)
                second = "0" + now.Second.ToString();
            else
                second = now.Second.ToString();

            sMsg = hour + ":" + minute + "." + second + " ||  " + theString +
                Convert.ToString((char)13) + Convert.ToString((char)10) + txtStatus.Text;

            UpdateStatus(sMsg, txtStatus);

            Application.DoEvents();
        }

        private void UpdateStatus(string sMsg, TextBox txt)
        {
            if (txt.InvokeRequired)
            {
                txt.Invoke(new dUpdateStatus(UpdateStatus), sMsg, txt);
            }

            txt.Text = sMsg;
        }

        //Construct a SQL connection string
        private string MakeSQLConnectionString()
        {
            string returnString;
            int iSQLTimeOut = 0;
            if (txtSvrName.Text.Length < 1)
            {
                GetConnRegSettings(true);
            }

            if (txtSvrName.Text.Trim() != string.Empty)
                returnString = "SERVER=" + txtSvrName.Text;
            else
                return string.Empty;

            if (txtInstName.Text.Trim().Length > 0)
                returnString += "\\" + txtInstName.Text;

            if (txtUserName.Text.Trim() != string.Empty)
                returnString += ";UID=" + txtUserName.Text;
            else
                return string.Empty;
            if (txtPassword.Text.Trim() != string.Empty)
                returnString += ";PWD=" + txtPassword.Text;
            else
                return string.Empty;

            returnString += ";DATABASE=" + lblExDBName.Text + ";";

            try
            {
                iSQLTimeOut = int.Parse(txtSQLTimeout.Text);
            }
            catch
            {
                iSQLTimeOut = 60;
            }

            if (iSQLTimeOut == 0)
                iSQLTimeOut = 60;

            returnString += "Connection Timeout = " + iSQLTimeOut.ToString();

            return returnString;
        }

        //Construct a SQL connection string
        private string GetSQLServerDetails()
        {
            string returnString;
            if (txtSvrName.Text.Length < 1)
            {
                GetConnRegSettings(true);
            }
            returnString = "SERVER=" + txtSvrName.Text;
            if (txtInstName.Text.Length > 0)
                returnString += "\\" + txtInstName.Text;
            returnString += ";UID=" + txtUserName.Text + ";PWD=" + txtPassword.Text;
            return returnString;
        }

        private void MakeDSN()
        { //Construct an ODBC DSN so the reports etc can connect successfully
            RegistryKey RegKey;
            try
            {
                //addStatus("Setting ODBC...");
                addStatus(this.GetResourceTextByKey("Key_EC_SettingODBC"));

                //for  CashMasterHQ 
                RegKey = Registry.LocalMachine.OpenSubKey("Software\\ODBC\\ODBC.INI", true);
                Registry.LocalMachine.CreateSubKey("Software\\ODBC\\ODBC.INI\\CashMasterHQ");
                RegKey = Registry.LocalMachine.OpenSubKey("Software\\ODBC\\ODBC.INI\\CashMasterHQ", true);
                RegKey.SetValue("Database", lblExDBName.Text);
                RegKey.SetValue("Description", "");
                RegKey.SetValue("Driver", "C:\\WINDOWS\\System32\\SQLSRV32.dll");
                RegKey.SetValue("Language", "british");//"us_english");
                RegKey.SetValue("Lastuser", txtUserName.Text);
                string serverString;
                serverString = txtSvrName.Text;
                if (txtInstName.Text != "")
                    serverString += "\\" + txtInstName.Text;
                RegKey.SetValue("Server", serverString);
                RegKey = Registry.LocalMachine.CreateSubKey("Software\\ODBC\\ODBC.INI\\ODBC Data Sources");
                RegKey = Registry.LocalMachine.OpenSubKey("Software\\ODBC\\ODBC.INI\\ODBC Data Sources", true);
                RegKey.SetValue("CashMasterHQ", "SQL Server");
                RegKey.Close();
                //for  CashMasterHQ SQL
                RegKey = Registry.LocalMachine.OpenSubKey("Software\\ODBC\\ODBC.INI", true);
                Registry.LocalMachine.CreateSubKey("Software\\ODBC\\ODBC.INI\\CashMasterHQ SQL");
                RegKey = Registry.LocalMachine.OpenSubKey("Software\\ODBC\\ODBC.INI\\CashMasterHQ SQL", true);
                RegKey.SetValue("Database", lblExDBName.Text);
                RegKey.SetValue("Description", "");
                RegKey.SetValue("Driver", "C:\\WINDOWS\\System32\\SQLSRV32.dll");
                RegKey.SetValue("Language", "british");//"us_english");
                RegKey.SetValue("Lastuser", txtUserName.Text);

                serverString = txtSvrName.Text;
                if (txtInstName.Text != "")
                    serverString += "\\" + txtInstName.Text;
                RegKey.SetValue("Server", serverString);
                RegKey = Registry.LocalMachine.CreateSubKey("Software\\ODBC\\ODBC.INI\\ODBC Data Sources");
                RegKey = Registry.LocalMachine.OpenSubKey("Software\\ODBC\\ODBC.INI\\ODBC Data Sources", true);
                RegKey.SetValue("CashMasterHQ", "SQL Server");
                RegKey.Close();


                //addStatus("Done!");
                addStatus(this.GetResourceTextByKey("Key_EC_Done_Exclamation"));
            }
            catch (Exception ex)
            {
                //addStatus("Error Setting ODBC: " + ex.Message);
                addStatus(this.GetResourceTextByKey("Key_EC_ErrorSettingODBC") + ex.Message);
            }
        }

        //Execute tool to remove windows installer for an application
        private void autoRemoveInstaller(string whatToRemove)
        {
            try
            {
                ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo();
                psi.FileName = "WindowsInstallerDisabler.exe";
                psi.Arguments = "/remove:" + whatToRemove;
                System.Diagnostics.Process.Start(psi);
            }
            catch (Exception ex)
            {
                //addStatus("Error Starting Installer Removal Tool: " + ex.Message);
                addStatus(this.GetResourceTextByKey("Key_EC_ErrorStartingInstallerRemovalTool") + ex.Message);
            }
        }

        private void parseString(object sender, System.EventArgs e)
        {//Use regular expressions to ensure that only correct characters are entered
            Control currCtl = (Control)sender;
            currCtl.Text = Regex.Replace(currCtl.Text, @"[^A-Za-z0-9\.\-\(\)\\\:]", "");
        }

        private bool RestoreDB(string strType)
        {
            string sSQLServerDetails = string.Empty;

            try
            {
                sSQLServerDetails = GetSQLServerDetails();	//+S002
                //addStatus("Launching Database creation for " + strType + "DB" + " ...");
                addStatus(this.GetResourceTextByKey("Key_EC_LaunchingDatabasecreation") + strType + "DB" + " ...");

                if (RestoreDatabase(sSQLServerDetails, strDataFilePath, strLogFilePath, strType))                            //+S002
                {
                    //addStatus("Creation Process Running for " + strType + "! Wait for the success status before doing anything else!");
                    addStatus(this.GetResourceTextByKey("Key_EC_CreationProcess") + strType + this.GetResourceTextByKey("Key_EC_EntConfig_Message"));

                    #region +S002 START
                    try
                    {
                        if (bLoadDatabase(sSQLServerDetails, strType))
                        {
                            //addStatus(strType + " - Database has been loaded successfully");
                            addStatus(strType + this.GetResourceTextByKey("Key_EC_Databasehasbeenloadedsuccessfully"));
                        }
                        else
                        {
                            //addStatus(strType + " - Database load failed");
                            addStatus(strType + this.GetResourceTextByKey("Key_EC_Databaseloadfailed"));
                            return false;
                        }
                    }
                    catch (Exception ex)
                    {
                        ExceptionManager.Publish(ex);
                        return false;
                    }
                    #endregion +S002 END
                    LogManager.WriteLog(string.Format("{0} - {1}", "Database Creation Complete.", txtSvrName.Text), LogManager.enumLogLevel.Info);
                }
                else
                {
                    return false;
                }
                return true;

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                //addStatus(ex.Message);
                addStatus(this.GetResourceTextByKey("Key_EC_Errror") + ex.Message);
                return false;
            }
        }


        #region +S002 START
        public static bool RestoreDatabase(string SQLConnection, string _strDataFilePath, string _strLogFilePath, string sDatabaseName)
        {
            bool bRestoreDatabase = false;
            try
            {
                if (bExecuteRestoreScripts(SQLConnection))
                {
                    LogManager.WriteLog("Creating database for " + sDatabaseName + "...", LogManager.enumLogLevel.Info);
                    SQLConnection += ";DATABASE=Master;";
                    SQLConnection += "CONNECTION TIMEOUT=0;";
                    SqlConnection conn = new SqlConnection(SQLConnection);

                    SqlCommand SQLCommand = new SqlCommand("CreateDatabase", conn);     //+S002
                    SQLCommand.CommandType = CommandType.StoredProcedure;

                    SqlParameter mdfPath = new SqlParameter("@mdf", SqlDbType.VarChar);
                    mdfPath.Value = _strDataFilePath;
                    SQLCommand.Parameters.Add(mdfPath);
                    SqlParameter ldfPath = new SqlParameter("@ldf", SqlDbType.VarChar);
                    ldfPath.Value = _strLogFilePath;
                    SQLCommand.Parameters.Add(ldfPath);
                    SqlParameter DatabaseName = new SqlParameter("@sDatabaseName", SqlDbType.VarChar);
                    DatabaseName.Value = sDatabaseName;
                    SQLCommand.Parameters.Add(DatabaseName);
                    SQLCommand.Connection.Open();
                    try
                    {
                        SQLCommand.ExecuteNonQuery();
                    }
                    finally
                    {
                        SQLCommand.Connection.Close();
                    }

                    bRestoreDatabase = true;
                }
            }
            catch (Exception ex)
            {
                bRestoreDatabase = false;
                LogManager.WriteLog("Database creation Failed" + ex.Message.ToString() + ex.Source.ToString(), LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
            }
            return bRestoreDatabase;
        }

        public static bool bExecuteRestoreScripts(string SQLConnection)
        {
            SqlConnection sqlConnection = null;
            SqlCommand oCommand = new SqlCommand();
            string scriptFile = string.Empty;
            string sqlcommandText = "";
            bool bExecute = false;
            try
            {
                LogManager.WriteLog("Executing creation scripts in master database.", LogManager.enumLogLevel.Info);

                using (Stream st = Assembly.GetExecutingAssembly().GetManifestResourceStream("BMC.DBRestore.sql"))
                {
                    StreamReader sr = new StreamReader(st);
                    scriptFile = sr.ReadToEnd();
                }

                sqlConnection = new SqlConnection(SQLConnection);
                string[] sqlCommands = Regex.Split(scriptFile, @"^\s*GO\s*($|\-\-.*$)", RegexOptions.Multiline | RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace);
                sqlConnection.Open();
                oCommand.Connection = sqlConnection;
                foreach (string sqlCommand in sqlCommands)
                {
                    if (!string.IsNullOrEmpty(sqlCommand))
                    {
                        sqlcommandText = sqlCommand;
                        oCommand.CommandText = sqlCommand;
                        oCommand.ExecuteNonQuery();
                    }
                }
                sqlConnection.Close();
                bExecute = true;
                LogManager.WriteLog(string.Format("Creation scripts executed successfully"), LogManager.enumLogLevel.Info);
            }
            catch (Exception ex)
            {
                LogManager.WriteLog(string.Format("Failed to execute the creation scripts"), LogManager.enumLogLevel.Info);
                throw ex;
            }
            finally
            {
                if (sqlConnection != null)
                {
                    sqlConnection.Close();
                    sqlConnection.Dispose();
                    sqlConnection = null;
                }
            }

            return bExecute;
        }

        public static bool DropDatabase(string SQLConnection, string sDatabaseName)
        {

            bool bDropDatabase = false;

            try
            {
                SQLConnection += ";DATABASE=Master;";
                SQLConnection += "CONNECTION TIMEOUT=0;";
                SqlConnection conn = new SqlConnection(SQLConnection);
                SqlCommand SQLCommand = new SqlCommand("DropDatabase", conn);
                SQLCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter DatabaseName = new SqlParameter("@sDatabaseName", SqlDbType.VarChar);
                DatabaseName.Value = sDatabaseName;
                SQLCommand.Parameters.Add(DatabaseName);
                SQLCommand.Connection.Open();
                try
                {
                    SQLCommand.ExecuteNonQuery();
                }
                finally
                {
                    SQLCommand.Connection.Close();
                }
                bDropDatabase = true;
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                bDropDatabase = false;
            }
            return bDropDatabase;
        }

        private bool bLoadDatabase(string SQLConnection, string sDatabaseName)
        {
            bool bLoadDatabase = false;

            string scriptFile = string.Empty;

            try
            {
                LogManager.WriteLog("Inside bLoadDatabase", LogManager.enumLogLevel.Info);

                string updgradeScriptDefaultPath = Application.StartupPath + "\\" + ConfigurationManager.AppSettings.Get("UpdgradeScriptDefaultPath");
                string upgradeScriptFileName = ConfigurationManager.AppSettings.Get("UpgradeScriptFileName");
                string upgradeScriptPath = string.Format("{0}\\{1}", updgradeScriptDefaultPath, upgradeScriptFileName);

                XmlDocument xdoc = new XmlDocument();
                xdoc.Load(updgradeScriptDefaultPath + Path.DirectorySeparatorChar + upgradeScriptFileName);
                XmlNodeList xlist = xdoc.SelectNodes("//Order/name");

                foreach (XmlNode xscriptFName in xlist)
                {
                    string scriptFileNameSql = null;
                    string scriptFileNameEnpt = null;

                    if (File.Exists(updgradeScriptDefaultPath + Path.DirectorySeparatorChar + sDatabaseName + Path.DirectorySeparatorChar + xscriptFName.InnerText + ".sql"))
                        scriptFileNameSql = updgradeScriptDefaultPath + Path.DirectorySeparatorChar + sDatabaseName + Path.DirectorySeparatorChar + xscriptFName.InnerText + ".sql";

                    if (File.Exists(updgradeScriptDefaultPath + Path.DirectorySeparatorChar + sDatabaseName + Path.DirectorySeparatorChar + xscriptFName.InnerText + ".enpt"))
                        scriptFileNameEnpt = updgradeScriptDefaultPath + Path.DirectorySeparatorChar + sDatabaseName + Path.DirectorySeparatorChar + xscriptFName.InnerText + ".enpt";

                    if (string.IsNullOrEmpty(scriptFileNameSql) && string.IsNullOrEmpty(scriptFileNameEnpt))
                        continue;

                    try
                    {
                        if (!string.IsNullOrEmpty(scriptFileNameSql))
                        {
                            //addStatus("Creating the " + xscriptFName.InnerText);
                            addStatus(this.GetResourceTextByKey("Key_EC_ScriptCreating") + xscriptFName.InnerText);
                            StreamReader sr = File.OpenText(scriptFileNameSql);
                            scriptFile = sr.ReadToEnd();
                            sr.Close();

                            if (xscriptFName.InnerText.ToUpper() == "JOB")
                                ExecuteDirect(SQLConnection, scriptFile);
                            else
                                ExecuteScripts(SQLConnection, scriptFile);

                            LogManager.WriteLog(string.Format("{0}-{1}", scriptFileNameSql, "executed successfully"), LogManager.enumLogLevel.Info);
                        }

                        if (!string.IsNullOrEmpty(scriptFileNameEnpt))
                        {
                            //addStatus("Creating the " + xscriptFName.InnerText);
                            addStatus(this.GetResourceTextByKey("Key_EC_ScriptCreating") + xscriptFName.InnerText);
                            scriptFile = DecryptFile(scriptFileNameEnpt);

                            if (xscriptFName.InnerText.ToUpper() == "JOB")
                                ExecuteDirect(SQLConnection, scriptFile);
                            else
                                ExecuteScripts(SQLConnection, scriptFile);

                            LogManager.WriteLog(string.Format("{0}-{1}", scriptFileNameEnpt, "executed successfully"), LogManager.enumLogLevel.Info);
                        }
                    }
                    catch (Exception ex)
                    {
                        //addStatus("Error in creating the " + xscriptFName.InnerText);
                        addStatus(this.GetResourceTextByKey("Key_EC_ErrorinScriptCreation") + xscriptFName.InnerText);
                        LogManager.WriteLog("Load database Failed" + ex.Message.ToString() + ex.Source.ToString(), LogManager.enumLogLevel.Error);
                        ExceptionManager.Publish(ex);
                        if (xscriptFName.InnerText.ToUpper() != "JOB")
                            return false;
                    }
                }
                bLoadDatabase = true;
            }
            catch (Exception ex)
            {
                bLoadDatabase = false;
                LogManager.WriteLog("Load database Failed" + ex.Message.ToString() + ex.Source.ToString(), LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
            }

            return bLoadDatabase;
        }

        private string DecryptFile(string inputFile)
        {
            try
            {
                string sExcrytionKey = @"!@#$%^&*";
                string sqlscript = "";
                System.Text.UnicodeEncoding UE = new System.Text.UnicodeEncoding();
                byte[] key = UE.GetBytes(sExcrytionKey);

                using (FileStream fsCrypt = new FileStream(inputFile, FileMode.Open))
                {
                    System.Security.Cryptography.RijndaelManaged RMCrypto = new System.Security.Cryptography.RijndaelManaged();

                    using (System.Security.Cryptography.CryptoStream cs = new System.Security.Cryptography.CryptoStream(fsCrypt, RMCrypto.CreateDecryptor(key, key), System.Security.Cryptography.CryptoStreamMode.Read))
                    {

                        FileStream fsOut = new FileStream(Path.GetDirectoryName(inputFile) + "\\Temp.sql", FileMode.Create);

                        int data;
                        while ((data = cs.ReadByte()) != -1)
                            fsOut.WriteByte((byte)data);
                        fsOut.Flush();
                        fsOut.Dispose();
                        fsOut.Close();
                    }
                }
                StreamReader sr = File.OpenText(Path.GetDirectoryName(inputFile) + "\\Temp.sql");
                sqlscript = sr.ReadToEnd();
                sr.Close();
                File.Delete(Path.GetDirectoryName(inputFile) + "\\Temp.sql");
                return sqlscript;

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return "";
            }
        }
        #endregion +S002 END

        //Attempt to connect to the DB using the provided SQL Connection string
        private void TestDBConnection(string SQLConnection, int timeout)
        {
            try
            {
                //addStatus("Testing Database Connection...");
                addStatus(this.GetResourceTextByKey("Key_EC_TestDataBase"));

                using (SqlConnection conn = new SqlConnection(SQLConnection))
                {
                    SqlCommand SQLCommand = new SqlCommand();
                    SQLCommand.Connection = conn;
                    SQLCommand.Connection.Open();
                    SQLCommand.Connection.Close();
                };

                GetInitialSettings();

                //addStatus("Database Connection Successful!");
                addStatus(this.GetResourceTextByKey("Key_EC_DBConnectionSuccessful"));
            }
            catch (Exception ex)
            {
                //addStatus("Error Testing Database Connection: " + ex.Message);
                addStatus(this.GetResourceTextByKey("Key_EC_ErrorinTestDatabase") + ex.Message);
            }
        }

        #endregion common functions

        #region config stored in DB

        //overloaded method for displaying the DB name in status
        private bool TestDBConnection(string SQLConnection, string sDBName, int timeout)        //+S002
        {
            bool bConSuccess = false;   //+S002

            string sAdditionalStatsmsg = string.Empty;

            if (sDBName != string.Empty)
                sAdditionalStatsmsg = " for " + sDBName;

            try
            {
                //addStatus("Testing Database Connection" + sAdditionalStatsmsg + "...");
                addStatus(this.GetResourceTextByKey("Key_EC_TestingDatabaseConnection") + sAdditionalStatsmsg + "...");
                if (sDBName.Length > 0)
                    SQLConnection += ";DATABASE=" + sDBName + ";";
                SQLConnection += "CONNECTION TIMEOUT=" + timeout + ";";
                SqlConnection conn = new SqlConnection(SQLConnection);
                SqlCommand SQLCommand = new SqlCommand();
                SQLCommand.Connection = conn;
                SQLCommand.Connection.Open();
                SQLCommand.Connection.Close();
                //addStatus("Database Connection Successful!");
                addStatus(this.GetResourceTextByKey("Key_EC_DBConnectionSuccessful"));
                bConSuccess = true;     //+S002
            }
            catch (Exception ex)
            {
                bConSuccess = false;    //+S002
                //addStatus("Error Testing Database Connection: " + ex.Message);
                addStatus(this.GetResourceTextByKey("Key_EC_ErrorinTestDatabase") + ex.Message);
            }

            return bConSuccess;         //+S002
        }

        private bool CheckifDBExists(string SQLConnection, string sDBName, int timeout)
        {
            bool bDBExisits = false;
            try
            {
                //addStatus("Testing Database Connection for " + sDBName + "...");
                addStatus(this.GetResourceTextByKey("Key_EC_TestingDatabaseConnectionFor") + sDBName + "...");
                SQLConnection += ";DATABASE=" + sDBName + ";";
                SQLConnection += "CONNECTION TIMEOUT=" + timeout + ";";

                SqlConnection.ClearAllPools();		//+S002

                SqlConnection conn = new SqlConnection(SQLConnection);
                SqlCommand SQLCommand = new SqlCommand();
                SQLCommand.Connection = conn;
                SQLCommand.Connection.Open();
                SQLCommand.Connection.Close();
                bDBExisits = true;
            }
            catch
            {
                bDBExisits = false;
            }

            return bDBExisits;
        }

        #endregion config stored in DB

        #region config in registry

        private string GetConnRegSettings(bool blnShouldPopulate) //Check registry for SQl connection settings
        {

            //string sKey = string.Empty;            
            ////RegistryKey RegKey;

            try
            {
                string SQLConnect = DatabaseHelper.GetEnterpriseConnectionString();
                if (blnShouldPopulate)
                {
                    parseSQLConnect(SQLConnect, "Enterprise");
                }
                return SQLConnect;
            }
            catch (Exception ex)
            {
                //addStatus("Connection String does not exist.");
                addStatus(this.GetResourceTextByKey("Key_EC_ConnectionNotExists"));
                ExceptionManager.Publish(ex);
                return string.Empty;
            }
        }


        //Save connection settings, for the current pane
        private void SaveConnSettings()
        {
            Int16 iSQLTimeOut = 30;
            try
            {
                iSQLTimeOut = Int16.Parse(txtSQLTimeout.Text);
            }
            catch
            {
                iSQLTimeOut = 60;
            }

            if (iSQLTimeOut == 0)
                iSQLTimeOut = 60;
            if (txtSvrName.Text.Length != 0 && txtUserName.Text.Length != 0)
            {
                string sKey = string.Empty;
                string SQLConnect;
                SQLConnect = MakeSQLConnectionString();
                string serverName = txtSvrName.Text;
                if (txtInstName.Text.Trim().Length > 0)
                {
                    serverName += "\\" + txtInstName.Text;
                }
                try
                {
                    //addStatus("Setting SQL Connection Info...");
                    addStatus(this.GetResourceTextByKey("Key_EC_SettingSQLConnectionInfo"));
                    //BMC.Common.Utilities.DatabaseHelper.StoreConnectionString(txtSvrName.Text, lblExDBName.Text, txtUserName.Text, txtPassword.Text, iSQLTimeOut);
                    BMC.Common.Utilities.DatabaseHelper.StoreConnectionString(serverName, lblExDBName.Text, txtUserName.Text, txtPassword.Text, iSQLTimeOut);
                    BMC.Common.Utilities.DatabaseHelper.StoreConnectionString(serverName, "Audit", txtUserName.Text, txtPassword.Text, iSQLTimeOut);
                    //addStatus("Done!");
                    addStatus(this.GetResourceTextByKey("Key_EC_Done_Exclamation"));
                }
                catch (Exception ex)
                {
                    //addStatus("Unable to save the Information");
                    addStatus(this.GetResourceTextByKey("Key_EC_SaveInformationFail"));
                    ExceptionManager.Publish(ex);
                }
            }
            else
            {
                //addStatus("Server, Username and Database are Mandatory fields!");
                addStatus(this.GetResourceTextByKey("Key_EC_MandatoryFields"));
            }
        }

        //Save the report server setting
        private void SaveToSetting(string SettingValue, string SettingName)
        {
            string SQLConnect;
            try
            {
                SQLConnect = MakeSQLConnectionString();
                SqlConnection sqlConn = new SqlConnection(SQLConnect);
                SqlCommand sqlCMD = new SqlCommand();
                sqlCMD.Connection = sqlConn;
                sqlCMD.CommandText = "UPDATE Setting SET Setting_Value ='" + SettingValue.Trim() + "' WHERE Setting_Name = '" + SettingName.Trim() + "'";
                sqlCMD.CommandType = CommandType.Text;

                sqlConn.Open();
                int iCount = sqlCMD.ExecuteNonQuery();
                sqlConn.Close();
                if (iCount > 0)
                    //addStatus("Updated Setting " + SettingName + " Successfully: ");
                    addStatus(this.GetResourceTextByKey("Key_EC_UpdatedSetting") + SettingName + this.GetResourceTextByKey("Key_EC_EntConfig_Successfully"));
            }
            catch (Exception ex)
            {
                //addStatus("Error Setting " + SettingName + " : " + ex.Message);
                addStatus(this.GetResourceTextByKey("Key_EC_SettingError") + SettingName + " : " + ex.Message);
            }
        }

        #endregion config in registry

        private Int32 ToInteger(Object objValue)
        {
            try
            {
                return Convert.ToInt32(objValue);
            }
            catch
            {
                return 0;
            }
        }

        #region events generated by controls

        private void btnTestConn_Click(object sender, System.EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                if (string.IsNullOrEmpty(txtSvrName.Text))
                {
                    //MessageBox.Show("Please enter value for server Name.", nameVersion);
                    Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_EC_ENTER_SERVERNAME"), this.Text);
                    return;
                }
                if (string.IsNullOrEmpty(txtUserName.Text))
                {
                    //MessageBox.Show("Please enter value for User Name.", nameVersion);
                    Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_EC_ENTER_USERNAME"), this.Text);
                    return;
                }

                SqlConnection.ClearAllPools();

                TestDBConnection(MakeSQLConnectionString(), ToInteger(txtSQLTimeout.Text));

            }
            catch
            {

            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void btnSaveConn_Click(object sender, System.EventArgs e)
        {
            string Result = "";
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                if (string.IsNullOrEmpty(txtSvrName.Text))
                {
                    //MessageBox.Show("Please enter value for server Name.", nameVersion);
                    Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_EC_ENTER_SERVERNAME"), this.Text);
                    txtSvrName.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtUserName.Text))
                {
                    //MessageBox.Show("Please enter value for User Name.", nameVersion);
                    Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_EC_ENTER_USERNAME"), this.Text);
                    txtUserName.Focus();
                    return;
                }
                SaveConnSettings();
                if (string.IsNullOrEmpty(txtReportServer.Text))
                {
                    //Result = MessageBox.Show("Do you want to save Report server details later?", nameVersion, MessageBoxButtons.YesNo).ToString();
                    Result = Win32Extensions.ShowQuestionMessageBox(this, this.GetResourceTextByKey(1, "MSG_EC_REPORT_SERVERDETAILSCONFIRM"), this.Text).ToString();
                    if (Result.ToUpper() != "YES")
                        return;
                    if (string.IsNullOrEmpty(txtReportFolder.Text))
                    {
                        //Result = MessageBox.Show("Do you want to save Report folder details later?", nameVersion, MessageBoxButtons.YesNo).ToString();
                        Result = Win32Extensions.ShowQuestionMessageBox(this, this.GetResourceTextByKey(1, "MSG_EC_REPORT_FOLDERDETAILSCONFIRM"), this.Text).ToString();
                        if (Result.ToUpper() != "YES")
                            return;
                    }
                }
                if (txtReportServer.Text.Length != 0)
                {
                    string ReportServer = string.Empty;
                    ReportServer = txtReportServer.Text;

                    SaveToSetting(ReportServer, "ReportServerURL");
                    SaveToSetting(txtReportFolder.Text, "ReportFolder");
                    SaveToSetting(txtRSInstance.Text, "ReportServerInstance");
                }
                else
                {
                    //addStatus("Report Server is a Mandatory field!");
                    addStatus(this.GetResourceTextByKey("Key_EC_ReportServerisaMandatoryfield"));
                }

                if (chkCertificateRequired.Checked)
                {
                    if (string.IsNullOrEmpty(txtCertificateissuer.Text))
                    {
                        //MessageBox.Show("Please enter value for Certificate issuer.", nameVersion);
                        Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_EC_ENTER_CERT_ISSUER"), this.Text);
                        txtCertificateissuer.Focus();
                        return;
                    }
                    else
                    {
                        //addStatus("Certificate Issuer is a Mandatory field!");
                        addStatus(this.GetResourceTextByKey("Key_EC_CertificateIssuerisaMandatoryfield"));
                    }
                }

                int itempConnectionTimeOut;
                if (!(int.TryParse(txtSQLTimeout.Text, out itempConnectionTimeOut)))
                {
                    //MessageBox.Show("Please enter a numeric value for command timeout.", nameVersion, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_EC_ENTER_CONNECTIONTIMEOUT"), this.Text);
                    txtSQLTimeout.Clear();
                    txtSQLTimeout.Focus();
                    return;

                }

                int itempTimeOut;
                if (!(int.TryParse(txtCommandTimeout.Text, out itempTimeOut)))
                {
                    //MessageBox.Show("Please enter a numeric value for command timeout.", nameVersion, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_EC_ENTER_COMMANDTIMEOUT"), this.Text);
                    txtCommandTimeout.Clear();
                    txtCommandTimeout.Focus();
                    return;

                }
                SaveToSetting(chkCertificateRequired.Checked ? "true" : "false", "IsCertificateRequired");
                SaveToSetting(txtCertificateissuer.Text.Trim(), "CertificateIssuer");

                if (string.IsNullOrEmpty(txtGuardianServerIP.Text) || txtGuardianServerIP.Text == ".")
                {
                    //MessageBox.Show("Please Enter Valid Guardian Server IP.", nameVersion);
                    Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_EC_ENTER_GUARDIAN_IP"), this.Text);
                    txtGuardianServerIP.Focus();
                    return;
                }
                SaveToSetting(txtGuardianServerIP.Text.Trim(), "GuardianServerIPAddress");

                if (txtLogFilePath.Text.Trim() != string.Empty)
                {
                    if (Directory.Exists(txtLogFilePath.Text.Trim()))
                    {
                        BMCRegistryHelper.SetRegKeyValue(string.Empty, UIConstants.DefaultLogDir, RegistryValueKind.String, txtLogFilePath.Text.Trim());
                        addStatus(this.GetResourceTextByKey("Key_EC_LogsFolderSaved"));//Logs folder detail saved successfully
                    }
                    else
                    {
                        if (Win32Extensions.ShowQuestionMessageBox(this, this.GetResourceTextByKey("Key_EC_CreateLogFolder"), this.Text) == DialogResult.Yes)//Folder does not exists. Do you want to create?
                        {
                            Directory.CreateDirectory(txtLogFilePath.Text.Trim());
                            BMCRegistryHelper.SetRegKeyValue(string.Empty, UIConstants.DefaultLogDir, RegistryValueKind.String, txtLogFilePath.Text.Trim());
                            addStatus(this.GetResourceTextByKey("Key_EC_FolderCreationSuccessful"));//Logs folder created successfully
                        }
                        else
                        {
                            addStatus(this.GetResourceTextByKey("Key_EC_FolderDoesNotExist"));//Folder does not exists.
                            txtLogFilePath.Focus();
                            return;
                        }
                    }
                }
                else
                {
                    addStatus(this.GetResourceTextByKey("Key_EC_ValidLogFolder"));//Please enter a valid logfile path.
                    txtLogFilePath.Focus();
                    return;
                }


                #region +S001 START
                string strSTMEnabled;
                strSTMEnabled = (chkEnableTransmit.Checked) ? "1" : "0";
                SaveToSetting(strSTMEnabled, "IsTransmitterEnabled");

                if (strSTMEnabled == "1")
                {
                    if (txtEventServer.Text.Trim() != string.Empty)
                    {
                        SaveToSetting(txtEventServer.Text.Trim(), "STMServerIP");
                    }
                    else
                    {
                        //MessageBox.Show("Please enter value for event transmitter server URL.", nameVersion);
                        Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_EC_ENTER_TRANSMITTER_URL"), this.Text);
                        txtEventServer.Focus();
                        return;
                    }
                }
                else
                {
                    SaveToSetting(txtEventServer.Text.Trim(), "STMServerIP");
                }


               
                if ((chkEnableEBSComm.Checked) && (string.IsNullOrEmpty(txtEBSURL.Text)))
                {
                    //MessageBox.Show("Please enter value for EBS URL.", nameVersion);
                    Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_EC_ENTER_EBS_URL"), this.Text);
                    txtEBSURL.Focus();
                    return;
                }
                else
                {
                    SaveToSetting(chkEnableEBSComm.Checked.ToString(), "ISEBSENABLED");
                    SaveToSetting(txtEBSURL.Text.Trim(), "EBSEndPointURL");
                }
              
                //EBS SERVER URL

                #endregion +S001 END

                try
                {
                    XmlDocument xDom;
                    XmlNode myNode;
                    xDom = new XmlDocument();
                    xDom.Load(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), @"BMC DEPLOY REPORTS\DeployReport.exe.config"));
                    myNode = xDom.DocumentElement.SelectSingleNode("/configuration/appSettings");
                    if (myNode != null)
                    {
                        foreach (XmlNode oNode in myNode.ChildNodes)
                        {
                            if (oNode.Attributes["key"].Value.ToString().ToUpper() == "SERVERNAME")
                            {
                                oNode.Attributes["value"].Value = txtSvrName.Text;
                            }
                            else if (oNode.Attributes["key"].Value.ToString().ToUpper() == "UID")
                            {
                                oNode.Attributes["value"].Value = txtUserName.Text;
                            }
                            else if (oNode.Attributes["key"].Value.ToString().ToUpper() == "PWD")
                            {
                                oNode.Attributes["value"].Value = txtPassword.Text;
                            }
                            if (oNode.Attributes["key"].Value.ToString().ToUpper() == "WEBSERVICEPATH")
                            {
                                oNode.Attributes["value"].Value = txtReportServer.Text;
                            }
                            if (oNode.Attributes["key"].Value.ToString().ToUpper() == "WEBSERVICENAME")
                            {
                                if (!string.IsNullOrEmpty(txtRSInstance.Text))
                                    oNode.Attributes["value"].Value = "/ReportServer$" + txtRSInstance.Text + "/ReportService2005.asmx";
                                else
                                    oNode.Attributes["value"].Value = "/ReportServer/ReportService2005.asmx";
                            }
                        }
                        xDom.Save(Application.StartupPath + "\\BMC DEPLOY REPORTS\\DeployReport.exe.config");
                    }

                    //New code to update Enterprise Reports config file
                    try
                    {
                        XmlDocument xDoc;
                        XmlNode Node;
                        xDoc = new XmlDocument();
                        xDoc.Load(Application.StartupPath + "\\BMC.EnterpriseReports.exe.config");
                        Node = xDoc.DocumentElement.SelectSingleNode("/configuration/appSettings");
                        if (Node != null)
                        {
                            foreach (XmlNode oNode in Node.ChildNodes)
                            {
                                if (oNode.Attributes["key"].Value.ToString().ToLower() == "reportserverinstance")
                                {
                                    if (!string.IsNullOrEmpty(txtRSInstance.Text))
                                        oNode.Attributes["value"].Value = "/ReportServer$" + txtRSInstance.Text;
                                    else
                                        oNode.Attributes["value"].Value = "/ReportServer";
                                }
                            }
                        }

                        xDoc.Save(Application.StartupPath + "\\BMC.EnterpriseReports.exe.config");
                    }
                    catch (Exception ex)
                    {
                        ExceptionManager.Publish(ex);
                    }

                }
                catch (FileNotFoundException ex)
                {
                    //addStatus("File not found :" + ex.Message);
                    addStatus(this.GetResourceTextByKey("Key_EC_Filenotfound") + ex.Message);
                }
                catch (DirectoryNotFoundException ex)
                {
                    //addStatus("Directory not found :" + ex.Message);
                    addStatus(this.GetResourceTextByKey("Key_EC_Directorynotfound") + ex.Message);
                }
                catch (Exception ex)
                {
                    //addStatus("Error in saving deploy report settings to config :" + ex.Message);
                    addStatus(this.GetResourceTextByKey("Key_EC_Errorinsavingdeployreport") + ex.Message);
                }

                if (ConnectType == "SQL")
                    MakeDSN();

                //RegistrySettings.SetRegistryString(UIConstants.SQLCommandTimeOut, txtCommandTimeout.Text, UIConstants.StartUpPath);

                BMCRegistryHelper.SetRegKeyValue("", UIConstants.SQLCommandTimeOut, RegistryValueKind.String, txtCommandTimeout.Text);
                txtCommandTimeout.Text = BMCRegistryHelper.GetRegKeyValue("", UIConstants.SQLCommandTimeOut, "60");
                //RegistrySettings.GetRegistryString(UIConstants.SQLCommandTimeOut, txtCommandTimeout.Text, UIConstants.StartUpPath);
                SetRegistryValueForDependOnServiceKey();

                //addStatus("Enterprise connection settings saved successfully.:");
                addStatus(this.GetResourceTextByKey("Key_EC_SavedSuccessfully"));
            }
            catch (Exception ex)
            {
                //addStatus("Error in saving Settings :" + ex.Message);
                addStatus(this.GetResourceTextByKey("Key_EC_ErrorinsavingSettings") + ex.Message);
            }

            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void txtSQLTimeout_Leave(object sender, System.EventArgs e)
        {
            try
            {
                Int32.Parse(txtSQLTimeout.Text);
            }
            catch
            {
                txtSQLTimeout.Text = "15"; //clear on error cant parse to number because im lazy.
                //addStatus("Timeout must be a number!");
                addStatus(this.GetResourceTextByKey("Key_EC_TimeOutWarning"));
                if (txtCommandTimeout.Text == string.Empty)
                {
                    txtCommandTimeout.Text = BMCRegistryHelper.GetRegKeyValue("", UIConstants.SQLCommandTimeOut, "60");
                    //txtCommandTimeout.Text = RegistrySettings.GetRegistryString(UIConstants.SQLCommandTimeOut, UIConstants.StartUpPath, "60");

                }

            }
        }

        private void txtSvrName_Leave(object sender, System.EventArgs e)
        {
            txtSvrName.Text = txtSvrName.Text.Replace("\\", "");
            if (txtSvrName.Text.ToLower() == "local" || txtSvrName.Text.ToLower() == "localhost" ||
                txtSvrName.Text.ToLower() == "127.0.0.1")
                txtSvrName.Text = "(local)";
            parseString(sender, e);
        }

        private void txtInstName_Leave(object sender, System.EventArgs e)
        {
            txtInstName.Text = txtInstName.Text.Replace("\\", "");
            parseString(sender, e);
        }

        private void btnClose_Click(object sender, System.EventArgs e)
        {
            Application.Exit();
        }

        private void btnDeployReports_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(GetConnRegSettings(false)))
                {
                    Process psi = new System.Diagnostics.Process();
                    psi.StartInfo.FileName = Application.StartupPath + "\\BMC DEPLOY REPORTS\\RunDeployReport.bat";
                    if (File.Exists(psi.StartInfo.FileName))
                    {
                        psi.Start();
                    }
                    else
                    {
                        //addStatus("File not found");
                        addStatus(this.GetResourceTextByKey("Key_EC_Filenotfound"));
                    }

                }
                else
                {
                    //addStatus("Save the connection settings before Deploying the reports");
                    addStatus(this.GetResourceTextByKey("Key_EC_Saveconnection"));
                }
            }
            catch (Exception ex)
            {
                //addStatus("Error in deploying reports :" + ex.Message);
                addStatus(this.GetResourceTextByKey("Key_EC_Errorindeployingreports") + ex.Message);
            }
        }

        private void btnStartServices_Click(object sender, EventArgs e)
        {
            string[] strListarray = null;
            bool bServiceStatus = false;
            int i = 0;
            try
            {
                BMCMonitoring objMonitoring = new BMCMonitoring();
                if (!string.IsNullOrEmpty(Services))
                    strListarray = Services.Split(',');
                else
                {
                    //addStatus(" Sorry no services found.");
                    addStatus(this.GetResourceTextByKey("Key_EC_Sorrynoservicesfound"));
                    return;
                }
                for (i = 0; i < strListarray.Length; i++)
                {
                    if (!string.IsNullOrEmpty(strListarray[i]))
                        bServiceStatus = objMonitoring.RestartService(strListarray[i].ToString());
                    if (bServiceStatus)
                        //addStatus(strListarray[i].ToString() + " service started successfully.");
                        addStatus(strListarray[i].ToString() + this.GetResourceTextByKey("Key_EC_servicestartedsuccessfully"));
                    else
                        //addStatus("Unable to Start the " + strListarray[i].ToString() + " service at the moment. Please try later or try a manual start.");
                        addStatus(this.GetResourceTextByKey("Key_EC_Sorrynoservicesfound") + strListarray[i].ToString() + this.GetResourceTextByKey("Key_EC_EntConfig_ServiceStart"));
                }

            }
            catch (Exception ex)
            {
                addStatus(ex.Message);
                ExceptionManager.Publish(ex);
            }
        }

        private void btnRunScript_Click(object sender, EventArgs e)
        {
            try
            {
                //Open the DeployScript exe to run the SQL Scripts.
                ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo();
                psi.FileName = Application.StartupPath + "\\DeployScripts.exe";
                System.Diagnostics.Process.Start(psi);
                Application.DoEvents();
                Thread.Sleep(10000);
                Application.DoEvents();
                //addStatus("The required SQL Scripts have been run on the database.");
                addStatus(this.GetResourceTextByKey("Key_EC_SQLScriptInfo"));
            }
            catch (Exception ex)
            {
                //addStatus("The sql scripts failed to execute:" + ex.Message);
                addStatus(this.GetResourceTextByKey("Key_EC_Thesqlscriptsfailed") + ex.Message);
            }
        }

        #endregion events generated by controls


        public bool TestProcess()
        {
            try
            {
                if (TestDBConnection(MakeSQLConnectionString(), "Enterprise", Convert.ToInt32(txtSQLTimeout.Text)))
                    return true;
                else
                {
                    //MessageBox.Show("Please make sure that database is available!", nameVersion);
                    Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_EC_CONFIRM_DATABASE"), this.Text);
                    return false;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return false;
            }
        }


        private void btnEntKey_Click(object sender, EventArgs e)
        {
            try
            {
                if (TestProcess())
                {
                    CreatePassKey();
                    //MessageBox.Show("Enterprise Key created successfully.", nameVersion);
                    Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_EC_KEY_CREATION_SUCCESSFULL"), this.Text);
                }
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
                //MessageBox.Show("Enterprise Key creation failed.", nameVersion);
                Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_EC_KEY_CREATION_FAILED"), this.Text);
            }

        }

        private void SaveEnterprisePasskeyToDB(string passkey)
        {
            try
            {
                string SQLConnect = MakeSQLConnectionString();
                using (SqlConnection sqlConn = new SqlConnection(SQLConnect))
                {
                    SqlCommand sqlCMD = new SqlCommand();
                    sqlCMD.Connection = sqlConn;
                    sqlCMD.CommandText = "EXEC dbo.usp_EditSetting @Setting_Name = 'EnterprisePassKey', @Setting_Value = '" + passkey.Replace("'", "''") + "'";
                    sqlCMD.CommandType = CommandType.Text;
                    sqlConn.Open();
                    sqlCMD.ExecuteNonQuery();
                    sqlConn.Close();
                }
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
        }

        private void SaveToSettingProfileItems(string sKey, string sValue)
        {
            try
            {
                string SQLConnect = MakeSQLConnectionString();
                using (SqlConnection sqlConn = new SqlConnection(SQLConnect))
                {
                    SqlCommand sqlCMD = new SqlCommand();
                    sqlCMD.Connection = sqlConn;
                    sqlCMD.CommandText = "UPDATE SettingsProfileItems SET SettingsProfileItems_SettingsMaster_Values=" + " '" + sValue + "'" + "FROM  SettingsProfileItems spi join SettingsMaster sm ON sm.SettingsMaster_ID=spi.SettingsProfileItems_SettingsMaster_ID WHERE sm.SettingsMaster_Name=" + "'" + sKey + "'";
                    sqlCMD.CommandType = CommandType.Text;
                    sqlConn.Open();
                    sqlCMD.ExecuteNonQuery();
                    sqlConn.Close();
                }
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
        }

        private void btnRestoreEnterprise_Click(object sender, EventArgs e)
        {
            DialogResult dr;
            string sSQLServeDetails = string.Empty;

            bool bEnterpriseDBExistis = false;
            bool bMeterAnalysisDBExists = false;
            bool bAuditDBExists = false;
            strDataFilePath = string.Empty;
            strLogFilePath = string.Empty;

            try
            {
                if (txtSvrName.Text == "")
                {
                    //MessageBox.Show("Please make sure that server name have the value entered!", nameVersion);
                    Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_EC_CONFIRM_SERVERNAME"), this.Text);
                    return;
                }

                if (string.IsNullOrEmpty(txtUserName.Text.Trim()))
                {
                    //MessageBox.Show("Please enter value for User Name.", nameVersion);
                    Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_EC_ENTER_USERNAME"), this.Text);
                    return;
                }

                if (string.IsNullOrEmpty(txtPassword.Text.Trim()))
                {
                    //MessageBox.Show("Please enter value for password.", nameVersion);
                    Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_EC_ENTER_PASSWORD"), this.Text);
                    return;
                }

                sSQLServeDetails = GetSQLServerDetails();

                if (!TestDBConnection(sSQLServeDetails, "master", ToInteger(txtSQLTimeout.Text)))
                {
                    return;
                }


                ConnectType = "SQL";

                    bEnterpriseDBExistis = CheckifDBExists(sSQLServeDetails, ENTERPRISEDBNAME, 60);
                    if (bEnterpriseDBExistis == true)
                    {
                        //addStatus("Enterprise DB not created. The DB already exists!");
                        addStatus(this.GetResourceTextByKey("Key_EC_EnterpriseDBalreadyexists"));
                    }

                    bMeterAnalysisDBExists = CheckifDBExists(sSQLServeDetails, "MeterAnalysis", 60);
                    if (bMeterAnalysisDBExists == true)
                    {
                        //addStatus("MeterAnalysis DB not created. The DB already exists!");
                        addStatus(this.GetResourceTextByKey("Key_EC_MeterAnalysisDBalreadyexists"));
                    }

                    bAuditDBExists = CheckifDBExists(sSQLServeDetails, "Audit", 60);
                    if (bAuditDBExists == true)
                    {
                        //addStatus("Audit DB not created. The DB already exists!");
                        addStatus(this.GetResourceTextByKey("Key_EC_AuditDBalreadyexists"));
                    }

                    if (bEnterpriseDBExistis && bMeterAnalysisDBExists && bAuditDBExists)
                    {
                        return;
                    }

                    using (frmSelectFiles _frmSelectFiles = new frmSelectFiles(bEnterpriseDBExistis, bAuditDBExists, bMeterAnalysisDBExists))
                    {
                        _frmSelectFiles.ShowDialog();
                        strDataFilePath = _frmSelectFiles.StrDataFilePath;
                        strLogFilePath = _frmSelectFiles.StrLogFilePath;
                    }

                    if (string.IsNullOrEmpty(strDataFilePath) || string.IsNullOrEmpty(strLogFilePath))
                    {
                        return;
                    }


                    //dr = MessageBox.Show("This process will take more than 45 seconds! Do you want to create database now?", nameVersion, MessageBoxButtons.YesNo);
                    dr = Win32Extensions.ShowQuestionMessageBox(this, this.GetResourceTextByKey(1, "MSG_EC_DB_CREATION_MSG"), this.Text);
                    if (dr.ToString().ToUpper() == "NO")
                    {
                        return;
                    }

                    tProcess = new Thread(() => CreateDatabase(bEnterpriseDBExistis, bMeterAnalysisDBExists, bAuditDBExists));
                tProcess.Name = "Enterprise DB Creation";
                btnRestoreEnterprise.Enabled = false;

                tProcess.Start();

                oFrmInfiniteProgressBar.ShowDialog();
            }
            catch (Exception ex)
            {
                //addStatus("Error in creating database :" + ex.Message);
                addStatus(this.GetResourceTextByKey("Key_EC_ErrorincreatingDB") + ex.Message);
            }
        }

        private void CreateDatabase(bool bEnterpriseDBExistis, bool bMeterAnalysisDBExists, bool bAuditDBExists)
        {
            string sSQLServeDetails = string.Empty;
            ConnectType = "SQL";
            int _bStatus = 0;

            sSQLServeDetails = GetSQLServerDetails();

            bool bDropDB = false;
            try
            {
                // Create Enterprise Blank DB
                if (!bEnterpriseDBExistis)
                {
                    if (RestoreDB("Enterprise"))
                    {
                        //addStatus("Enterprise DB created successfully.");
                        addStatus(this.GetResourceTextByKey("Key_EC_EnterpriseDBcreatedsuccessfully"));
                        _bStatus |= 0x01;
                    }
                    else
                    {
                        bDropDB = true;
                        //addStatus("Error in creating Enterprise DB.");
                        addStatus(this.GetResourceTextByKey("Key_EC_ErrorincreatingEnterpriseDB"));
                    }
                }

                if (bDropDB)
                {
                    DropDatabase(sSQLServeDetails, "Enterprise");
                    bDropDB = false;
                }

                // Create Meter Analysis Blank DB
                if (!bMeterAnalysisDBExists)
                {
                    if (RestoreDB("MeterAnalysis"))
                    {
                        //addStatus("MeterAnalysis DB created successfully.");
                        addStatus(this.GetResourceTextByKey("Key_EC_MeterAnalysisDBcreatedsuccessfully"));
                        _bStatus |= 0x02;
                    }
                    else
                    {
                        bDropDB = true;
                        //addStatus("Error in creating MeterAnalysis DB.");
                        addStatus(this.GetResourceTextByKey("Key_EC_ErrorincreatingMeterAnalysisD"));
                    }
                }

                if (bDropDB)
                {
                    DropDatabase(sSQLServeDetails, "MeterAnalysis");
                    bDropDB = false;
                }

                // Create Audit DB
                if (!bAuditDBExists)
                {
                    if (RestoreDB("Audit"))
                    {
                        //addStatus("Audit DB Created successfully.");
                        addStatus(this.GetResourceTextByKey("Key_EC_AuditDBCreatedsuccessfully"));
                        _bStatus |= 0x04;
                    }
                    else
                    {
                        bDropDB = true;
                        //addStatus("Error in creating Audit DB.");
                        addStatus(this.GetResourceTextByKey("Key_EC_ErrorincreatingAuditDB"));
                    }
                }

                if (bDropDB)
                {
                    DropDatabase(sSQLServeDetails, "Audit");
                }

                if (_bStatus == 0x7)
                {
                    CreatePassKey();
                    //addStatus("Enterprise Key created successfully.");
                    addStatus(this.GetResourceTextByKey("Key_EC_EnterpriseKeycreatedsuccessfully"));
                }
                //MessageBox.Show("Process completed successfully.", nameVersion);
                this.Invoke((MethodInvoker)delegate { Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_EC_PROCESS_COMPLETED"), this.Text); });
                Application.DoEvents();
            }
            catch (Exception ex)
            {
                //addStatus("Error in creating database :" + ex.Message);
                addStatus(this.GetResourceTextByKey("Key_EC_Errorincreatingdatabase") + ex.Message);
            }
            finally
            {
                btnRestoreEnterprise.Invoke((MethodInvoker)delegate { this.btnRestoreEnterprise.Enabled = true; });
                oFrmInfiniteProgressBar.Invoke((MethodInvoker)delegate { this.oFrmInfiniteProgressBar.Close(); });
            }
        }

        public void CreatePassKey()
        {
            string EncKey = string.Empty;
            //RegistryKey regKey;
            string sConnect = string.Empty;

            //regKey = Registry.LocalMachine.OpenSubKey(BMC.Common.ConfigurationManagement.ConfigManager.Read("RegistryPath"), true);
            //if (regKey.GetValue("EnterpriseKey") != null)
            //    EncKey = regKey.GetValue("EnterpriseKey").ToString();

            EncKey = BMCRegistryHelper.GetRegKeyValue(BMC.Common.ConfigurationManagement.ConfigManager.Read("RegistryPath"), "EnterpriseKey");


            if (EncKey == string.Empty)
            {
                string passkey = CryptographyHelper.Encrypt(CryptographyHelper.GetHashString(DateTime.Now.Ticks.ToString()));
                BMCRegistryHelper.SetRegKeyValue(BMC.Common.ConfigurationManagement.ConfigManager.Read("RegistryPath"), "EnterpriseKey", RegistryValueKind.String, passkey);
                //regKey.SetValue("EnterpriseKey", passkey);
                this.SaveEnterprisePasskeyToDB(passkey);
            }

            string sCopyrightsfromConfig = ConfigurationManager.AppSettings.Get("CopyRights");
            this.SaveToSettingProfileItems("COPYRIGTINFO", sCopyrightsfromConfig);
            //regKey.Close();
            //if "CreateEnterpriseKey button is clicked then following message will be shown.

        }

        private void TestConnection(string strServer, string strUsername, string strPassword, string strTimeOut, string strInstance, char chDatabase)
        {

            string strServerName = string.Empty;
            try
            {
                if (strInstance.Trim().Length > 0)
                {
                    strServer = strServer + "\\" + strInstance;
                }

                if (chDatabase == 'E')
                {
                    AddServerDetails(strServer, strUsername, strPassword, ENTERPRISEDBNAME, strTimeOut);
                }

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void AddServerDetails(string Server, string UserName, string Password, string DataBase, string ConnectionTimeout)
        {
            string ReturnConnectionString = string.Empty;
            Dictionary<string, string> objServerDetails = new Dictionary<string, string>();
            try
            {
                objServerDetails.Add("SERVER", Server);
                objServerDetails.Add("UID", UserName);
                objServerDetails.Add("PWD", Password);
                objServerDetails.Add("DATABASE", DataBase);
                objServerDetails.Add("CONNECTION TIMEOUT", ConnectionTimeout);

                ReturnConnectionString = MakeConnectionString(objServerDetails);

                if (!String.IsNullOrEmpty(ReturnConnectionString))
                {
                    TestDBConnection(ReturnConnectionString, DataBase, int.Parse(ConnectionTimeout));
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

        }

        private string MakeConnectionString(Dictionary<string, string> Credentials)
        {
            string strConnectionstring = string.Empty;
            try
            {
                if (Credentials != null)
                {
                    foreach (KeyValuePair<string, string> objKeyValue in Credentials)
                    {
                        strConnectionstring += objKeyValue.Key + "=" + objKeyValue.Value + ";";
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return strConnectionstring;
        }

        private void GetInitialSettings()
        {
            string SQLConnect = string.Empty;
            string strSPName = "rsp_GetEnterpriseInitialSettings";
            SQLConnect = MakeSQLConnectionString();
            string Reportserver = string.Empty;
            string clientName = string.Empty;
            string ReportServerInstance = string.Empty;
            if (!String.IsNullOrEmpty(SQLConnect.Trim()))
            {
                using (SqlConnection con = new SqlConnection(SQLConnect))
                {

                    try
                    {
                        con.Open();
                        SqlDataAdapter sda = new SqlDataAdapter();
                        DataSet ds = new DataSet();
                        SqlCommand cmdGetDataTable = new SqlCommand(strSPName, con);
                        cmdGetDataTable.CommandType = CommandType.StoredProcedure;
                        cmdGetDataTable.CommandTimeout = 60;
                        sda.SelectCommand = cmdGetDataTable;
                        sda.Fill(ds);
                        if (ds.Tables.Count > 0)
                        {
                            DataTable dtSettings = new DataTable();
                            dtSettings = ds.Tables[0];
                            if (dtSettings.Rows.Count > 0)
                            {
                                blsRegulatoryEnabled = (dtSettings.Rows[0]["RegulatoryType"] != string.Empty) ? Convert.ToBoolean(dtSettings.Rows[0]["IsRegulatoryEnabled"].ToString()) : false;
                                sRegulatoryType = (dtSettings.Rows[0]["RegulatoryType"] != string.Empty) ? dtSettings.Rows[0]["RegulatoryType"].ToString() : string.Empty;
                                Reportserver = (dtSettings.Rows[0]["ReportServerURL"] != string.Empty) ? dtSettings.Rows[0]["ReportServerURL"].ToString() : string.Empty;
                                ReportServerInstance = (dtSettings.Rows[0]["ReportServerInstance"] != string.Empty) ? dtSettings.Rows[0]["ReportServerInstance"].ToString() : string.Empty;

                                if (Reportserver != string.Empty)
                                {
                                    txtReportServer.Text = Reportserver;
                                }

                                if (ReportServerInstance != string.Empty)
                                {
                                    txtRSInstance.Text = ReportServerInstance;
                                }

                                if (txtReportFolder.Text.Trim().Length == 0)
                                {
                                    txtReportFolder.Text = "BMCUKReports";
                                }
                                txtReportFolder.Text = (dtSettings.Rows[0]["ReportFolder"] != string.Empty) ? dtSettings.Rows[0]["ReportFolder"].ToString() : string.Empty;
                                Services = (dtSettings.Rows[0]["WindowsServices"] != string.Empty) ? dtSettings.Rows[0]["WindowsServices"].ToString() : string.Empty;

                                txtCertificateissuer.Text = (dtSettings.Rows[0]["CertificateIssuer"] != string.Empty) ? dtSettings.Rows[0]["CertificateIssuer"].ToString() : string.Empty;
                                chkCertificateRequired.Checked = (dtSettings.Rows[0]["IsCertificateRequired"] != string.Empty) ? Convert.ToBoolean(dtSettings.Rows[0]["IsCertificateRequired"].ToString()) : false;
                                clientName = (dtSettings.Rows[0]["Client"] != string.Empty) ? dtSettings.Rows[0]["Client"].ToString().ToUpper() : string.Empty;
                                txtGuardianServerIP.Text = (dtSettings.Rows[0]["GuardianServerIPAddress"] != string.Empty) ? dtSettings.Rows[0]["GuardianServerIPAddress"].ToString() : string.Empty;

                                #region +S001 START
                                chkEnableTransmit.Checked = (dtSettings.Rows[0]["IsTransmitterEnabled"] != string.Empty) ? dtSettings.Rows[0]["IsTransmitterEnabled"].ToString() == "1" ? true : false : false;
                                txtEventServer.Text = (dtSettings.Rows[0]["STMServerIP"] != string.Empty) ? dtSettings.Rows[0]["STMServerIP"].ToString() : string.Empty;
                                #endregion +S001 END

                                //txtCommandTimeout.Text = RegistrySettings.GetRegistryString(UIConstants.SQLCommandTimeOut, UIConstants.StartUpPath, "60");
                                txtCommandTimeout.Text = BMCRegistryHelper.GetRegKeyValue("", UIConstants.SQLCommandTimeOut, "60");

                                chkEnableEBSComm.Checked =  (dtSettings.Rows[0]["ISEBSENABLED"] != string.Empty) ? Convert.ToBoolean(dtSettings.Rows[0]["ISEBSENABLED"]) : false;    
                                txtEBSURL.Text = (dtSettings.Rows[0]["EBSEndPointURL"] != string.Empty) ? dtSettings.Rows[0]["EBSEndPointURL"].ToString() : string.Empty;
                                if (!chkEnableEBSComm.Checked)
                                {
                                    txtEBSURL.Text = string.Empty;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        ExceptionManager.Publish(ex);
                    }
                }
            }
        }

        /// <summary>
        /// Get Log path details
        /// </summary>
        private void GetLogPath()
        {
            string strKeyvalue = string.Empty;
            try
            {
                strKeyvalue = BMCRegistryHelper.GetRegKeyValue(string.Empty, UIConstants.DefaultLogDir, "C:\\Logs");
                if (!string.IsNullOrWhiteSpace(strKeyvalue))
                {
                    txtLogFilePath.Text = strKeyvalue;
                }
                else
                {
                    txtLogFilePath.Text = "C:\\Logs";
                }
            }
            catch (Exception ex)
            {
                txtLogFilePath.Text = "C:\\Logs";
                ExceptionManager.Publish(ex);
                addStatus(this.GetResourceTextByKey("Key_EC_ErrorLoadingLogPath"));//Error while loading logs file path.
            }
        }

        private void sizeControls()
        {
            //+S001 START
            gpSTMSetings.Location = new Point(4, 268);
            gpActions.Location = new Point(4, 341);
            txtStatus.Location = new Point(4, 438);
            //+S001 END
            txtStatus.Height = 380;
        }

        public static void ExecuteScripts(string SQLConnection, string scriptFile)
        {
            SqlConnection sqlConnection = null;
            SqlCommand oCommand = new SqlCommand();
            string sqlcommandText = "";
            try
            {
                LogManager.WriteLog("Executing the scripts in database.", LogManager.enumLogLevel.Info);
                sqlConnection = new SqlConnection(SQLConnection);
                string[] sqlCommands = Regex.Split(scriptFile, @"^\s*GO\s*($|\-\-.*$)", RegexOptions.Multiline | RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace);
                sqlConnection.Open();
                oCommand.Connection = sqlConnection;
                foreach (string sqlCommand in sqlCommands)
                {
                    if (!string.IsNullOrEmpty(sqlCommand))
                    {
                        sqlcommandText = sqlCommand;
                        oCommand.CommandText = sqlCommand;
                        oCommand.ExecuteNonQuery();
                    }
                }
                sqlConnection.Close();
                LogManager.WriteLog(string.Format("scripts executed successfully"), LogManager.enumLogLevel.Info);
            }
            catch (Exception ex)
            {
                LogManager.WriteLog(string.Format("Failed to execute the scripts"), LogManager.enumLogLevel.Info);
                throw ex;
            }
            finally
            {
                if (sqlConnection != null)
                {
                    sqlConnection.Close();
                    sqlConnection.Dispose();
                    sqlConnection = null;
                }
            }
        }
        //
        public static void ExecuteDirect(string SQLConnection, string scriptFile)
        {
            SqlConnection sqlConnection = null;
            SqlCommand oCommand = new SqlCommand();
            string sqlcommandText = "";
            try
            {
                LogManager.WriteLog("Executing the scripts in database.", LogManager.enumLogLevel.Info);
                sqlConnection = new SqlConnection(SQLConnection);
                sqlConnection.Open();
                oCommand.Connection = sqlConnection;
                if (!string.IsNullOrEmpty(scriptFile))
                {
                    sqlcommandText = scriptFile;
                    oCommand.CommandText = scriptFile;
                    oCommand.ExecuteNonQuery();
                }
                sqlConnection.Close();
                LogManager.WriteLog(string.Format("scripts executed successfully"), LogManager.enumLogLevel.Info);
            }
            catch (Exception ex)
            {
                LogManager.WriteLog(string.Format("Failed to execute the scripts"), LogManager.enumLogLevel.Info);
                throw ex;
            }
            finally
            {
                if (sqlConnection != null)
                {
                    sqlConnection.Close();
                    sqlConnection.Dispose();
                    sqlConnection = null;
                }
            }
        }

        private void chkCertificateRequired_CheckedChanged(object sender, EventArgs e)
        {

            if (chkCertificateRequired.Checked)
                txtCertificateissuer.Enabled = true;
            else
                txtCertificateissuer.Enabled = false;
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            try
            {
                fldBLocal.ShowDialog();
                if (!string.IsNullOrEmpty(fldBLocal.SelectedPath))
                    txtDatFiles.Text = fldBLocal.SelectedPath;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void SetRegistryValueForDependOnServiceKey()
        {
            RegistryKey RegKey = null;

            try
            {
                LogManager.WriteLog("Inside SetRegistryValueForDependOnServiceKey method", LogManager.enumLogLevel.Info);

                string dependOnServiceKey = ConfigurationManager.AppSettings.Get("DependOnServiceKey");
                string dependOnServiceValue = txtInstName.Text.Trim() == string.Empty ? "SQLServerAgent"
                                        : string.Format("{0}{1}{2}", "SQLServerAgent", "$", txtInstName.Text);

                string[] strServices = null;

                if (!string.IsNullOrEmpty(Services))
                    strServices = Services.Split(',');
                else
                    return;

                string registryPathControlSet001 = ConfigurationManager.AppSettings.Get("RegistryPathControlSet001");

                foreach (string serviceName in strServices)
                {
                    try
                    {
                        RegKey = Registry.LocalMachine.OpenSubKey(string.Format("{0}\\{1}", registryPathControlSet001, serviceName), true);
                        object RegSubKey = RegKey.GetValue(dependOnServiceKey);
                        if (RegSubKey == null)
                            RegKey.CreateSubKey(dependOnServiceKey);
                        RegKey.SetValue(dependOnServiceKey, dependOnServiceValue);
                        RegSubKey = null;
                    }
                    catch (Exception ex)
                    {
                        ExceptionManager.Publish(ex);
                    }
                    finally
                    {
                        if (RegKey != null) { RegKey.Close(); RegKey = null; }
                    }
                }

                string registryPathControlSet002 = ConfigurationManager.AppSettings.Get("RegistryPathControlSet002");

                foreach (string serviceName in strServices)
                {
                    try
                    {
                        RegKey = Registry.LocalMachine.OpenSubKey(string.Format("{0}\\{1}", registryPathControlSet002, serviceName), true);
                        object RegSubKey = RegKey.GetValue(dependOnServiceKey);
                        if (RegSubKey == null)
                            RegKey.CreateSubKey(dependOnServiceKey);
                        RegKey.SetValue(dependOnServiceKey, dependOnServiceValue);
                        RegSubKey = null;
                    }
                    catch (Exception ex)
                    {
                        ExceptionManager.Publish(ex);
                    }
                    finally
                    {
                        if (RegKey != null) { RegKey.Close(); RegKey = null; }
                    }
                }

                string registryPathCurrentControlSet = ConfigurationManager.AppSettings.Get("RegistryPathCurrentControlSet");

                foreach (string serviceName in strServices)
                {
                    try
                    {
                        RegKey = Registry.LocalMachine.OpenSubKey(string.Format("{0}\\{1}", registryPathCurrentControlSet, serviceName), true);
                        object RegSubKey = RegKey.GetValue(dependOnServiceKey);
                        if (RegSubKey == null)
                            RegKey.CreateSubKey(dependOnServiceKey);
                        RegKey.SetValue(dependOnServiceKey, dependOnServiceValue);
                        RegSubKey = null;
                    }
                    catch (Exception ex)
                    {
                        ExceptionManager.Publish(ex);
                    }
                    finally
                    {
                        if (RegKey != null) { RegKey.Close(); RegKey = null; }
                    }
                }

                LogManager.WriteLog("RegistryValue set successfully for DependOnService Key.", LogManager.enumLogLevel.Info);

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void chkEnableTransmit_CheckedChanged(object sender, EventArgs e)
        {
            txtEventServer.Enabled = chkEnableTransmit.Checked;
            if (!txtEventServer.Enabled)
            {
                txtEventServer.Text = string.Empty;
            }
        }

        private void chkEnableEBSComm_CheckedChanged(object sender, EventArgs e)
        {
            txtEBSURL.Enabled = chkEnableEBSComm.Checked;
            if (!txtEBSURL.Enabled)
            {
                txtEBSURL.Text = string.Empty;
            }
        }

        private void btnBrowseLogPath_Click(object sender, EventArgs e)
        {
            try
            {
                LogManager.WriteLog("Inside btnBrowseLogPath_Click", LogManager.enumLogLevel.Info);
                string FolderPath = null;

                if (FdbrowserLogPath.ShowDialog() == DialogResult.OK)
                {
                    FolderPath = FdbrowserLogPath.SelectedPath;
                    if (FolderPath != null)
                    {
                        txtLogFilePath.Clear();
                        txtLogFilePath.Text = FolderPath;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btnClose_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

       

       
    }
}
