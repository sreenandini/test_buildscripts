
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using Microsoft.Win32; //Registry
using System.Reflection; //Weird stuff
using System.Diagnostics; //Shell and more
using System.Drawing.Drawing2D; //GDI+
using System.Data.SqlClient;
using System.Net; //DNS
using System.IO; //files

using System.Text.RegularExpressions;
using System.Threading;
using System.Xml;
using System.Data.Sql;

using BMC.Common.Security;
using System.Configuration;
using System.Collections.Generic;
using BMC.Common.ConfigurationManagement;
using BMC.Common.ExceptionManagement;
using BMC.Common.Utilities;
using BMC.Common;
using BMC.CoreLib;
using BMC.CoreLib.Win32;
using BMC.Common.LogManagement;



namespace BMCEnterpriseClientConfig
{
    /// <summary>
    /// Summary description for Form1.
    /// </summary>
    public class FrmEnterpriseConfig : System.Windows.Forms.Form
    {
        #region Form generated variables

        private System.Windows.Forms.TextBox txtStatus;
        private System.Windows.Forms.FolderBrowserDialog fldBNet;
        private System.Windows.Forms.MainMenu mainMenu1;
        private System.Windows.Forms.FolderBrowserDialog fldBLocal;
        private GroupBox grpDatabaseSettings;
        private Label lblExDBName;
        private Label lblSeperator;
        private TextBox txtSvrName;
        private TextBox txtPassword;
        private Label lblPassword;
        private Label lblInstance;
        private Label lblDatabaseName;
        private Label lblServer;
        private TextBox txtInstName;
        private Label lblUserName;
        private TextBox txtUserName;
        private ComboBox cmbLocalIPs;
        private Label label15;
        private Button button2;
        private Label label9;
        private Label lblSeconds1;
        private Label lblConnectionTimeOut;
        private TextBox txtSQLTimeout;
        private Button btnTestConn;
        private Button btnSaveConn;
        private IContainer components;

        #endregion Form generated variables
        private TextBox txtCommandTimeout;
        private Label lblCommandTimeOut;
        private Label lblSeconds2;
        private FolderBrowserDialog FdbrowserLogPath;
        private GroupBox grpGeneralSetting;
        private Button btnBrowseLogPath;
        private TextBox txtLogFilePath;
        private Label lblLogFilePath;
        private TableLayoutPanel tblGeneralSettings;
        private TableLayoutPanel tblContainer;
        private TableLayoutPanel tblFooter;
        private Button btnClose;
        private TableLayoutPanel tblHeader;
        private TableLayoutPanel tblHeaderContent;
        private TableLayoutPanel tblDatabaseSettings;
        private Label lblRequired;
        private Label lblOptional;
        private Panel pnlHeader;

        bool isEnterpriseClient = false;

        #region form related/main

        public FrmEnterpriseConfig()
        {
            /*RegistryKey regKeyConnectionString = Registry.LocalMachine.OpenSubKey("Software\\Honeyframe");
            if (regKeyConnectionString != null && regKeyConnectionString.GetValue("InstallationType")!=null)
           {                 
               if (regKeyConnectionString.GetValue("InstallationType").ToString() == "EnterpriseClient")
                   isEnterpriseClient = true;
               else
                   isEnterpriseClient = false;
           }
           
           else
               isEnterpriseClient = false;
           */
            isEnterpriseClient = BMCRegistryHelper.IsEnterpriseClient();
            InitializeComponent();
            SetTagProperty();
        }

        /// <summary>
        /// Assigns the Resource Key names to the controls--Created by kishore sivagnanam
        /// </summary>
        public void SetTagProperty()
        {
            this.lblCommandTimeOut.Tag = "Key_CommandTimeoutColon";
            this.lblConnectionTimeOut.Tag = "Key_ConnectionTimeoutColon";
            this.lblDatabaseName.Tag = "Key_DatabaseNameColon";
            this.lblExDBName.Tag = "Key_Enterprise";
            this.grpDatabaseSettings.Tag = "Key_EnterpriseSetup";
            this.lblInstance.Tag = "Key_InstanceColon";
            this.lblRequired.Tag = "Key_ItemsREQUIRED";
            this.lblOptional.Tag = "Key_ItemsthatareOptional";
            this.lblPassword.Tag = "Key_PasswordColon";
            this.btnSaveConn.Tag = "Key_SaveSettings";
            this.lblSeconds2.Tag = "Key_Seconds";
            this.lblSeconds1.Tag = "Key_Seconds";
            this.lblServer.Tag = "Key_ServerColon";
            this.btnTestConn.Tag = "Key_TestConnection";
            this.lblUserName.Tag = "Key_UserName";
            this.Tag = "Key_BMCEnterpriseConfig";
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
            this.ResolveResources();
            if (!isEnterpriseClient)
            {
                //MessageBox.Show(this.GetResourceTextByKey(1, "MSG_ENTERPRISECLIENT_NOT_INSTALLED"));
                Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_ENTERPRISECLIENT_NOT_INSTALLED"), this.Text);
                Application.ExitThread();
            }
            try
            {

                lblExDBName.Text = ENTERPRISEDBNAME;

                addStatus(this.GetResourceTextByKey(1, "MSG_CONFIG_DBINITIALISING"));//
                // this.Text = nameVersion;

                addStatus(this.GetResourceTextByKey(1, "MSG_CONFIG_DONE"));//MSG_CONFIG_DONE
                //CreateLogDir();
                GetConnRegSettings(true);

                txtCommandTimeout.Text = this.GetRegistryString("SQLCommandTimeOut", "", "60");
					
				GetLogPath();
                // GetInitialSettings();

                //if (blsRegulatoryEnabled == true && sRegulatoryType.Length > 0)
                //{
                //    ResizeControls();


                //}
                //else
                //{
                //    sizeControls();
                //}
            }
            catch (Exception ex)
            {
                //addStatus("Error: " + ex.Message);
                ExceptionManager.Publish(ex);
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmEnterpriseConfig));
            this.txtStatus = new System.Windows.Forms.TextBox();
            this.fldBNet = new System.Windows.Forms.FolderBrowserDialog();
            this.mainMenu1 = new System.Windows.Forms.MainMenu(this.components);
            this.fldBLocal = new System.Windows.Forms.FolderBrowserDialog();
            this.grpDatabaseSettings = new System.Windows.Forms.GroupBox();
            this.tblDatabaseSettings = new System.Windows.Forms.TableLayoutPanel();
            this.lblServer = new System.Windows.Forms.Label();
            this.lblInstance = new System.Windows.Forms.Label();
            this.txtSvrName = new System.Windows.Forms.TextBox();
            this.txtInstName = new System.Windows.Forms.TextBox();
            this.lblConnectionTimeOut = new System.Windows.Forms.Label();
            this.txtSQLTimeout = new System.Windows.Forms.TextBox();
            this.lblSeconds1 = new System.Windows.Forms.Label();
            this.lblUserName = new System.Windows.Forms.Label();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.lblCommandTimeOut = new System.Windows.Forms.Label();
            this.txtCommandTimeout = new System.Windows.Forms.TextBox();
            this.lblSeconds2 = new System.Windows.Forms.Label();
            this.lblSeperator = new System.Windows.Forms.Label();
            this.lblDatabaseName = new System.Windows.Forms.Label();
            this.lblExDBName = new System.Windows.Forms.Label();
            this.btnTestConn = new System.Windows.Forms.Button();
            this.btnSaveConn = new System.Windows.Forms.Button();
            this.cmbLocalIPs = new System.Windows.Forms.ComboBox();
            this.label15 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.FdbrowserLogPath = new System.Windows.Forms.FolderBrowserDialog();
            this.grpGeneralSetting = new System.Windows.Forms.GroupBox();
            this.tblGeneralSettings = new System.Windows.Forms.TableLayoutPanel();
            this.txtLogFilePath = new System.Windows.Forms.TextBox();
            this.lblLogFilePath = new System.Windows.Forms.Label();
            this.btnBrowseLogPath = new System.Windows.Forms.Button();
            this.tblContainer = new System.Windows.Forms.TableLayoutPanel();
            this.tblFooter = new System.Windows.Forms.TableLayoutPanel();
            this.btnClose = new System.Windows.Forms.Button();
            this.tblHeader = new System.Windows.Forms.TableLayoutPanel();
            this.tblHeaderContent = new System.Windows.Forms.TableLayoutPanel();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblRequired = new System.Windows.Forms.Label();
            this.lblOptional = new System.Windows.Forms.Label();
            this.grpDatabaseSettings.SuspendLayout();
            this.tblDatabaseSettings.SuspendLayout();
            this.grpGeneralSetting.SuspendLayout();
            this.tblGeneralSettings.SuspendLayout();
            this.tblContainer.SuspendLayout();
            this.tblFooter.SuspendLayout();
            this.tblHeader.SuspendLayout();
            this.tblHeaderContent.SuspendLayout();
            this.pnlHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtStatus
            // 
            this.txtStatus.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtStatus.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtStatus.ForeColor = System.Drawing.Color.DarkRed;
            this.txtStatus.Location = new System.Drawing.Point(3, 208);
            this.txtStatus.Multiline = true;
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.ReadOnly = true;
            this.txtStatus.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtStatus.Size = new System.Drawing.Size(772, 305);
            this.txtStatus.TabIndex = 3;
            this.txtStatus.TabStop = false;
            // 
            // grpDatabaseSettings
            // 
            this.grpDatabaseSettings.BackColor = System.Drawing.Color.White;
            this.grpDatabaseSettings.Controls.Add(this.tblDatabaseSettings);
            this.grpDatabaseSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpDatabaseSettings.ForeColor = System.Drawing.Color.Coral;
            this.grpDatabaseSettings.Location = new System.Drawing.Point(0, 35);
            this.grpDatabaseSettings.Margin = new System.Windows.Forms.Padding(0);
            this.grpDatabaseSettings.Name = "grpDatabaseSettings";
            this.grpDatabaseSettings.Size = new System.Drawing.Size(778, 110);
            this.grpDatabaseSettings.TabIndex = 1;
            this.grpDatabaseSettings.TabStop = false;
            this.grpDatabaseSettings.Text = "Database Settings";
            // 
            // tblDatabaseSettings
            // 
            this.tblDatabaseSettings.ColumnCount = 8;
            this.tblDatabaseSettings.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tblDatabaseSettings.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 42.85714F));
            this.tblDatabaseSettings.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tblDatabaseSettings.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 96F));
            this.tblDatabaseSettings.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 131F));
            this.tblDatabaseSettings.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 42.85715F));
            this.tblDatabaseSettings.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tblDatabaseSettings.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tblDatabaseSettings.Controls.Add(this.lblServer, 0, 0);
            this.tblDatabaseSettings.Controls.Add(this.lblInstance, 3, 0);
            this.tblDatabaseSettings.Controls.Add(this.txtSvrName, 1, 0);
            this.tblDatabaseSettings.Controls.Add(this.txtInstName, 4, 0);
            this.tblDatabaseSettings.Controls.Add(this.lblConnectionTimeOut, 5, 0);
            this.tblDatabaseSettings.Controls.Add(this.txtSQLTimeout, 6, 0);
            this.tblDatabaseSettings.Controls.Add(this.lblSeconds1, 7, 0);
            this.tblDatabaseSettings.Controls.Add(this.lblUserName, 0, 1);
            this.tblDatabaseSettings.Controls.Add(this.txtUserName, 1, 1);
            this.tblDatabaseSettings.Controls.Add(this.lblPassword, 3, 1);
            this.tblDatabaseSettings.Controls.Add(this.txtPassword, 4, 1);
            this.tblDatabaseSettings.Controls.Add(this.lblCommandTimeOut, 5, 1);
            this.tblDatabaseSettings.Controls.Add(this.txtCommandTimeout, 6, 1);
            this.tblDatabaseSettings.Controls.Add(this.lblSeconds2, 7, 1);
            this.tblDatabaseSettings.Controls.Add(this.lblSeperator, 2, 0);
            this.tblDatabaseSettings.Controls.Add(this.lblDatabaseName, 0, 2);
            this.tblDatabaseSettings.Controls.Add(this.lblExDBName, 1, 2);
            this.tblDatabaseSettings.Controls.Add(this.btnTestConn, 7, 2);
            this.tblDatabaseSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblDatabaseSettings.Location = new System.Drawing.Point(3, 16);
            this.tblDatabaseSettings.Name = "tblDatabaseSettings";
            this.tblDatabaseSettings.RowCount = 3;
            this.tblDatabaseSettings.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 59.25926F));
            this.tblDatabaseSettings.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40.74074F));
            this.tblDatabaseSettings.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tblDatabaseSettings.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblDatabaseSettings.Size = new System.Drawing.Size(772, 91);
            this.tblDatabaseSettings.TabIndex = 0;
            // 
            // lblServer
            // 
            this.lblServer.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblServer.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblServer.ForeColor = System.Drawing.Color.DarkOrange;
            this.lblServer.Location = new System.Drawing.Point(3, 10);
            this.lblServer.Name = "lblServer";
            this.lblServer.Size = new System.Drawing.Size(72, 16);
            this.lblServer.TabIndex = 0;
            this.lblServer.Text = "Server Name";
            // 
            // lblInstance
            // 
            this.lblInstance.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblInstance.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInstance.ForeColor = System.Drawing.Color.ForestGreen;
            this.lblInstance.Location = new System.Drawing.Point(256, 9);
            this.lblInstance.Name = "lblInstance";
            this.lblInstance.Size = new System.Drawing.Size(90, 18);
            this.lblInstance.TabIndex = 3;
            this.lblInstance.Text = "Instance Name";
            // 
            // txtSvrName
            // 
            this.txtSvrName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSvrName.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSvrName.Location = new System.Drawing.Point(103, 8);
            this.txtSvrName.MaxLength = 50;
            this.txtSvrName.Name = "txtSvrName";
            this.txtSvrName.Size = new System.Drawing.Size(122, 20);
            this.txtSvrName.TabIndex = 1;
            // 
            // txtInstName
            // 
            this.txtInstName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtInstName.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtInstName.Location = new System.Drawing.Point(352, 8);
            this.txtInstName.MaxLength = 50;
            this.txtInstName.Name = "txtInstName";
            this.txtInstName.Size = new System.Drawing.Size(125, 20);
            this.txtInstName.TabIndex = 4;
            // 
            // lblConnectionTimeOut
            // 
            this.lblConnectionTimeOut.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblConnectionTimeOut.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblConnectionTimeOut.ForeColor = System.Drawing.Color.Red;
            this.lblConnectionTimeOut.Location = new System.Drawing.Point(483, 10);
            this.lblConnectionTimeOut.Name = "lblConnectionTimeOut";
            this.lblConnectionTimeOut.Size = new System.Drawing.Size(122, 15);
            this.lblConnectionTimeOut.TabIndex = 5;
            this.lblConnectionTimeOut.Text = "Connection Timeout";
            // 
            // txtSQLTimeout
            // 
            this.txtSQLTimeout.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.txtSQLTimeout.Location = new System.Drawing.Point(611, 3);
            this.txtSQLTimeout.MaxLength = 3;
            this.txtSQLTimeout.Name = "txtSQLTimeout";
            this.txtSQLTimeout.Size = new System.Drawing.Size(20, 20);
            this.txtSQLTimeout.TabIndex = 6;
            this.txtSQLTimeout.Text = "30";
            // 
            // lblSeconds1
            // 
            this.lblSeconds1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblSeconds1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblSeconds1.ForeColor = System.Drawing.Color.Red;
            this.lblSeconds1.Location = new System.Drawing.Point(653, 10);
            this.lblSeconds1.Name = "lblSeconds1";
            this.lblSeconds1.Size = new System.Drawing.Size(52, 16);
            this.lblSeconds1.TabIndex = 7;
            this.lblSeconds1.Text = "Seconds";
            // 
            // lblUserName
            // 
            this.lblUserName.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblUserName.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUserName.ForeColor = System.Drawing.Color.DarkOrange;
            this.lblUserName.Location = new System.Drawing.Point(3, 40);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(72, 16);
            this.lblUserName.TabIndex = 8;
            this.lblUserName.Text = "User Name";
            // 
            // txtUserName
            // 
            this.txtUserName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtUserName.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUserName.Location = new System.Drawing.Point(103, 39);
            this.txtUserName.MaxLength = 50;
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(122, 20);
            this.txtUserName.TabIndex = 9;
            // 
            // lblPassword
            // 
            this.lblPassword.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblPassword.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPassword.ForeColor = System.Drawing.Color.DarkOrange;
            this.lblPassword.Location = new System.Drawing.Point(256, 40);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(72, 16);
            this.lblPassword.TabIndex = 10;
            this.lblPassword.Text = "Password";
            // 
            // txtPassword
            // 
            this.txtPassword.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPassword.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPassword.Location = new System.Drawing.Point(352, 39);
            this.txtPassword.MaxLength = 50;
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.ShortcutsEnabled = false;
            this.txtPassword.Size = new System.Drawing.Size(125, 20);
            this.txtPassword.TabIndex = 11;
            // 
            // lblCommandTimeOut
            // 
            this.lblCommandTimeOut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCommandTimeOut.AutoSize = true;
            this.lblCommandTimeOut.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCommandTimeOut.ForeColor = System.Drawing.Color.Red;
            this.lblCommandTimeOut.Location = new System.Drawing.Point(483, 41);
            this.lblCommandTimeOut.Name = "lblCommandTimeOut";
            this.lblCommandTimeOut.Size = new System.Drawing.Size(122, 13);
            this.lblCommandTimeOut.TabIndex = 12;
            this.lblCommandTimeOut.Text = "Command Timeout";
            // 
            // txtCommandTimeout
            // 
            this.txtCommandTimeout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCommandTimeout.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.txtCommandTimeout.Location = new System.Drawing.Point(611, 39);
            this.txtCommandTimeout.MaxLength = 3;
            this.txtCommandTimeout.Name = "txtCommandTimeout";
            this.txtCommandTimeout.Size = new System.Drawing.Size(36, 20);
            this.txtCommandTimeout.TabIndex = 13;
            this.txtCommandTimeout.Text = "30";
            // 
            // lblSeconds2
            // 
            this.lblSeconds2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSeconds2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblSeconds2.ForeColor = System.Drawing.Color.Red;
            this.lblSeconds2.Location = new System.Drawing.Point(653, 40);
            this.lblSeconds2.Name = "lblSeconds2";
            this.lblSeconds2.Size = new System.Drawing.Size(116, 16);
            this.lblSeconds2.TabIndex = 14;
            this.lblSeconds2.Text = "Seconds";
            // 
            // lblSeperator
            // 
            this.lblSeperator.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSeperator.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSeperator.Location = new System.Drawing.Point(231, 10);
            this.lblSeperator.Name = "lblSeperator";
            this.lblSeperator.Size = new System.Drawing.Size(19, 16);
            this.lblSeperator.TabIndex = 2;
            this.lblSeperator.Text = "\\\\";
            // 
            // lblDatabaseName
            // 
            this.lblDatabaseName.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblDatabaseName.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDatabaseName.ForeColor = System.Drawing.Color.DarkOrange;
            this.lblDatabaseName.Location = new System.Drawing.Point(3, 67);
            this.lblDatabaseName.Name = "lblDatabaseName";
            this.lblDatabaseName.Size = new System.Drawing.Size(91, 16);
            this.lblDatabaseName.TabIndex = 15;
            this.lblDatabaseName.Text = "Database Name";
            // 
            // lblExDBName
            // 
            this.lblExDBName.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tblDatabaseSettings.SetColumnSpan(this.lblExDBName, 2);
            this.lblExDBName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblExDBName.ForeColor = System.Drawing.Color.Black;
            this.lblExDBName.Location = new System.Drawing.Point(103, 66);
            this.lblExDBName.Name = "lblExDBName";
            this.lblExDBName.Size = new System.Drawing.Size(71, 19);
            this.lblExDBName.TabIndex = 16;
            this.lblExDBName.Text = "Enterprise";
            // 
            // btnTestConn
            // 
            this.btnTestConn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTestConn.BackColor = System.Drawing.SystemColors.Control;
            this.btnTestConn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.btnTestConn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnTestConn.Location = new System.Drawing.Point(653, 63);
            this.btnTestConn.Name = "btnTestConn";
            this.btnTestConn.Size = new System.Drawing.Size(116, 25);
            this.btnTestConn.TabIndex = 17;
            this.btnTestConn.Text = "&Test Connection";
            this.btnTestConn.UseVisualStyleBackColor = true;
            this.btnTestConn.Click += new System.EventHandler(this.btnTestConn_Click);
            // 
            // btnSaveConn
            // 
            this.btnSaveConn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveConn.BackColor = System.Drawing.SystemColors.Control;
            this.btnSaveConn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.btnSaveConn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnSaveConn.Location = new System.Drawing.Point(569, 3);
            this.btnSaveConn.Name = "btnSaveConn";
            this.btnSaveConn.Size = new System.Drawing.Size(100, 28);
            this.btnSaveConn.TabIndex = 0;
            this.btnSaveConn.Text = "Save &All Settings";
            this.btnSaveConn.UseVisualStyleBackColor = true;
            this.btnSaveConn.Click += new System.EventHandler(this.btnSaveConn_Click);
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
            // FdbrowserLogPath
            // 
            this.FdbrowserLogPath.Description = "Please select the path for the log file";
            // 
            // grpGeneralSetting
            // 
            this.grpGeneralSetting.BackColor = System.Drawing.Color.White;
            this.grpGeneralSetting.Controls.Add(this.tblGeneralSettings);
            this.grpGeneralSetting.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpGeneralSetting.ForeColor = System.Drawing.Color.Coral;
            this.grpGeneralSetting.Location = new System.Drawing.Point(3, 148);
            this.grpGeneralSetting.Name = "grpGeneralSetting";
            this.grpGeneralSetting.Size = new System.Drawing.Size(772, 54);
            this.grpGeneralSetting.TabIndex = 2;
            this.grpGeneralSetting.TabStop = false;
            this.grpGeneralSetting.Text = "General Settings";
            // 
            // tblGeneralSettings
            // 
            this.tblGeneralSettings.ColumnCount = 3;
            this.tblGeneralSettings.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tblGeneralSettings.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblGeneralSettings.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tblGeneralSettings.Controls.Add(this.txtLogFilePath, 1, 0);
            this.tblGeneralSettings.Controls.Add(this.lblLogFilePath, 0, 0);
            this.tblGeneralSettings.Controls.Add(this.btnBrowseLogPath, 2, 0);
            this.tblGeneralSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblGeneralSettings.Location = new System.Drawing.Point(3, 16);
            this.tblGeneralSettings.Name = "tblGeneralSettings";
            this.tblGeneralSettings.RowCount = 1;
            this.tblGeneralSettings.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblGeneralSettings.Size = new System.Drawing.Size(766, 35);
            this.tblGeneralSettings.TabIndex = 0;
            // 
            // txtLogFilePath
            // 
            this.txtLogFilePath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLogFilePath.BackColor = System.Drawing.Color.White;
            this.txtLogFilePath.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLogFilePath.Location = new System.Drawing.Point(153, 7);
            this.txtLogFilePath.MaxLength = 50;
            this.txtLogFilePath.Name = "txtLogFilePath";
            this.txtLogFilePath.ReadOnly = true;
            this.txtLogFilePath.Size = new System.Drawing.Size(560, 20);
            this.txtLogFilePath.TabIndex = 1;
            // 
            // lblLogFilePath
            // 
            this.lblLogFilePath.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblLogFilePath.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLogFilePath.ForeColor = System.Drawing.Color.DarkOrange;
            this.lblLogFilePath.Location = new System.Drawing.Point(3, 9);
            this.lblLogFilePath.Name = "lblLogFilePath";
            this.lblLogFilePath.Size = new System.Drawing.Size(82, 16);
            this.lblLogFilePath.TabIndex = 0;
            this.lblLogFilePath.Text = "Log File Path";
            // 
            // btnBrowseLogPath
            // 
            this.btnBrowseLogPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowseLogPath.BackColor = System.Drawing.SystemColors.Control;
            this.btnBrowseLogPath.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBrowseLogPath.ForeColor = System.Drawing.Color.Coral;
            this.btnBrowseLogPath.Location = new System.Drawing.Point(719, 3);
            this.btnBrowseLogPath.Name = "btnBrowseLogPath";
            this.btnBrowseLogPath.Size = new System.Drawing.Size(44, 29);
            this.btnBrowseLogPath.TabIndex = 2;
            this.btnBrowseLogPath.Text = "...";
            this.btnBrowseLogPath.UseVisualStyleBackColor = true;
            this.btnBrowseLogPath.Click += new System.EventHandler(this.btnBrowseLogPath_Click_1);
            // 
            // tblContainer
            // 
            this.tblContainer.ColumnCount = 1;
            this.tblContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblContainer.Controls.Add(this.tblFooter, 0, 1);
            this.tblContainer.Controls.Add(this.tblHeader, 0, 0);
            this.tblContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblContainer.Location = new System.Drawing.Point(0, 0);
            this.tblContainer.Name = "tblContainer";
            this.tblContainer.RowCount = 2;
            this.tblContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tblContainer.Size = new System.Drawing.Size(784, 562);
            this.tblContainer.TabIndex = 0;
            // 
            // tblFooter
            // 
            this.tblFooter.ColumnCount = 3;
            this.tblFooter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblFooter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 106F));
            this.tblFooter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 106F));
            this.tblFooter.Controls.Add(this.btnSaveConn, 1, 0);
            this.tblFooter.Controls.Add(this.btnClose, 2, 0);
            this.tblFooter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblFooter.Location = new System.Drawing.Point(3, 525);
            this.tblFooter.Name = "tblFooter";
            this.tblFooter.RowCount = 1;
            this.tblFooter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblFooter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.tblFooter.Size = new System.Drawing.Size(778, 34);
            this.tblFooter.TabIndex = 1;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(675, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(100, 28);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click_1);
            // 
            // tblHeader
            // 
            this.tblHeader.ColumnCount = 1;
            this.tblHeader.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblHeader.Controls.Add(this.grpDatabaseSettings, 0, 1);
            this.tblHeader.Controls.Add(this.grpGeneralSetting, 0, 2);
            this.tblHeader.Controls.Add(this.txtStatus, 0, 3);
            this.tblHeader.Controls.Add(this.tblHeaderContent, 0, 0);
            this.tblHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblHeader.Location = new System.Drawing.Point(3, 3);
            this.tblHeader.Name = "tblHeader";
            this.tblHeader.RowCount = 4;
            this.tblHeader.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tblHeader.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 110F));
            this.tblHeader.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tblHeader.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblHeader.Size = new System.Drawing.Size(778, 516);
            this.tblHeader.TabIndex = 0;
            // 
            // tblHeaderContent
            // 
            this.tblHeaderContent.ColumnCount = 1;
            this.tblHeaderContent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblHeaderContent.Controls.Add(this.pnlHeader, 0, 0);
            this.tblHeaderContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblHeaderContent.Location = new System.Drawing.Point(3, 3);
            this.tblHeaderContent.Name = "tblHeaderContent";
            this.tblHeaderContent.RowCount = 1;
            this.tblHeaderContent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblHeaderContent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.tblHeaderContent.Size = new System.Drawing.Size(772, 29);
            this.tblHeaderContent.TabIndex = 0;
            // 
            // pnlHeader
            // 
            this.pnlHeader.Controls.Add(this.lblRequired);
            this.pnlHeader.Controls.Add(this.lblOptional);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlHeader.Location = new System.Drawing.Point(3, 3);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(766, 23);
            this.pnlHeader.TabIndex = 0;
            // 
            // lblRequired
            // 
            this.lblRequired.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblRequired.BackColor = System.Drawing.Color.Transparent;
            this.lblRequired.Font = new System.Drawing.Font("Arial", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRequired.ForeColor = System.Drawing.Color.DarkOrange;
            this.lblRequired.Location = new System.Drawing.Point(4, 4);
            this.lblRequired.Name = "lblRequired";
            this.lblRequired.Size = new System.Drawing.Size(111, 17);
            this.lblRequired.TabIndex = 0;
            this.lblRequired.Text = "Items REQUIRED ";
            // 
            // lblOptional
            // 
            this.lblOptional.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblOptional.BackColor = System.Drawing.Color.Transparent;
            this.lblOptional.Font = new System.Drawing.Font("Arial", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOptional.ForeColor = System.Drawing.Color.ForestGreen;
            this.lblOptional.Location = new System.Drawing.Point(137, 5);
            this.lblOptional.Name = "lblOptional";
            this.lblOptional.Size = new System.Drawing.Size(118, 16);
            this.lblOptional.TabIndex = 1;
            this.lblOptional.Text = "Items that are Optional";
            // 
            // FrmEnterpriseConfig
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this.tblContainer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(800, 600);
            this.Menu = this.mainMenu1;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "FrmEnterpriseConfig";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Bally MultiConnect Enterprise Config";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.grpDatabaseSettings.ResumeLayout(false);
            this.tblDatabaseSettings.ResumeLayout(false);
            this.tblDatabaseSettings.PerformLayout();
            this.grpGeneralSetting.ResumeLayout(false);
            this.tblGeneralSettings.ResumeLayout(false);
            this.tblGeneralSettings.PerformLayout();
            this.tblContainer.ResumeLayout(false);
            this.tblFooter.ResumeLayout(false);
            this.tblHeader.ResumeLayout(false);
            this.tblHeader.PerformLayout();
            this.tblHeaderContent.ResumeLayout(false);
            this.pnlHeader.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        #region variables

        //private bool blnChangesSaved = false;
        //private bool blsRegulatoryEnabled= false;
        private string sRegulatoryType = string.Empty;
        //private string ConnectType = "SQL";
        const string nameVersion = "Bally Multi Connect - Enterprise Configuration ";
        private const string ENTERPRISEDBNAME = "Enterprise";
        private string strUrlvalidate = string.Empty;
        string svrName, userName, passWord, dataBase, instName = "", sTimeOut = "";
        #endregion variables

        #region common functions

        private void CreateLogDir()
        {
            try
            {
                addStatus(this.GetResourceTextByKey(1, "MSG_CONFIG_CHEKINGFORLOGDIR"));

                string logPath = ConfigManager.Read(this.GetResourceTextByKey(1, "MSG_CONFIG_DEFAULTLOGPATH"));

                if (!Directory.Exists(logPath))
                {
                    if (Directory.Exists(logPath.Substring(0, 2)))
                    {
                        addStatus(this.GetResourceTextByKey(1, "MSG_CONFIG_CREATELOGDIR"));
                        Directory.CreateDirectory(logPath);
                        addStatus(this.GetResourceTextByKey(1, "MSG_CONFIG_DONE"));
                    }
                    else
                    {
                        addStatus(this.GetResourceTextByKey(1, "MSG_CONFIG_LOGDIRNOTFOUND"));
                    }
                }
                else
                {
                    addStatus(this.GetResourceTextByKey(1, "MSG_CONFIG_LOGDIRNOTFOUND"));
                }
            }
            catch (Exception ex)
            {
                addStatus(this.GetResourceTextByKey(1, "MSG_CONFIG_LOGDIRERROR"));
                ExceptionManager.Publish(ex);
            }
        }

        private void parseSQLConnect(string SQLConnect, string sDBName)
        {
            //format SQL connection string from input info
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
            txtStatus.Text = hour + ":" + minute + "." + second + " ||  " + theString +
                Convert.ToString((char)13) + Convert.ToString((char)10) + txtStatus.Text;
            Application.DoEvents();
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
            returnString = "SERVER=" + txtSvrName.Text;
            //returnString = this.GetResourceTextByKey("Key_Server") + txtSvrName.Text;
            if (txtInstName.Text.Length > 0)
                returnString += "\\" + txtInstName.Text;
                //returnString += this.GetResourceTextByKey("Key_DoubleSlash") + txtInstName.Text;
            returnString += ";UID=" + txtUserName.Text + ";PWD=" + txtPassword.Text + ";DATABASE=" + lblExDBName.Text + ";";
            //returnString += this.GetResourceTextByKey("Key_UID") + txtUserName.Text + this.GetResourceTextByKey("Key_PWD") + txtPassword.Text + this.GetResourceTextByKey("Key_Database") + lblExDBName.Text + ";";

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
            //returnString += this.GetResourceTextByKey("Key_ConnectionTimeOut") + iSQLTimeOut.ToString();

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
            //returnString = "SERVER=" + txtSvrName.Text;
            returnString = this.GetResourceTextByKey("Key_Server") + txtSvrName.Text;
            if (txtInstName.Text.Length > 0)
                //returnString += "\\" + txtInstName.Text;
                returnString += this.GetResourceTextByKey("Key_DoubleSlash") + txtInstName.Text;
            //returnString += ";UID=" + txtUserName.Text + ";PWD=" + txtPassword.Text;
            returnString += this.GetResourceTextByKey("Key_UID") + txtUserName.Text + this.GetResourceTextByKey("Key_PWD") + txtPassword.Text;
            return returnString;
        }

        /*
        private void MakeDSN()
        { //Construct an ODBC DSN so the reports etc can connect successfully
            RegistryKey RegKey;
            try
            {
                addStatus("Setting ODBC...");

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


                addStatus("Done!");
            }
            catch (Exception ex)
            {
                addStatus("Error Setting ODBC: " + ex.Message);
            }
        }    
         */

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
                //addStatus("Error Starting Installer Removal Tool." );
                addStatus(this.GetResourceTextByKey("Key_Client_Config_Error"));
                ExceptionManager.Publish(ex);
            }
        }

        private void parseString(object sender, System.EventArgs e)
        {//Use regular expressions to ensure that only correct characters are entered
            Control currCtl = (Control)sender;
            currCtl.Text = Regex.Replace(currCtl.Text, @"[^A-Za-z0-9\.\-\(\)\\\:]", "");
        }

        private void RestoreDB(string type)
        {//restore the database to the specified location from the backup provided
            try
            {
                addStatus(this.GetResourceTextByKey(1, "MSG_CONFIG_DBCONNECTIONLUNCHING") + type + "DB" + " ...");//MSG_CONFIG_DBCONNECTIONLUNCHING
                string RestoreArgs;
                string backupFile = "";
                string svrName = "", userName = "", passWord = "", DBName = "", instName = "";

                svrName = txtSvrName.Text;
                userName = txtUserName.Text;
                passWord = txtPassword.Text;
                instName = txtInstName.Text;
                if (type == "Enterprise")
                {
                    DBName = ENTERPRISEDBNAME;
                    backupFile = "EnterpriseBlankDB.bak";
                    //backupFile = this.GetResourceTextByKey("Key_EnterpriseDB_bak"); //"EnterpriseBlankDB.bak";
                }
                else
                {
                    DBName = "MeterAnalysis";
                    backupFile = "MeterAnalysisBlankDB.bak";
                    //DBName = this.GetResourceTextByKey("Key_BallyMultiConnectMeterAnalysis"); //"MeterAnalysis";
                    //backupFile = this.GetResourceTextByKey("Key_MeterAnalysisDB_bak"); //"MeterAnalysisBlankDB.bak";
                }
                RestoreArgs = "/Run" + " /File:" + ConfigurationSettings.AppSettings["ApplicationStartupPath"].ToString() + "\\database\\" + backupFile +" /Server:" + svrName;
                //RestoreArgs = this.GetResourceTextByKey("Key_Client_Config_Run") + this.GetResourceTextByKey("Key_Client_Config_File") + ConfigurationSettings.AppSettings["ApplicationStartupPath"].ToString() + this.GetResourceTextByKey("Key_Client_Config_Database") + backupFile + this.GetResourceTextByKey("Key_Client_Config_Server") + svrName;
                if (instName.Length > 0)
                    RestoreArgs += "\\" + instName;
                    //RestoreArgs += this.GetResourceTextByKey("Key_DoubleSlash") + instName;
                RestoreArgs += " /DB:" + DBName + " /UserName:" + userName + " /Password:" + passWord;
                //RestoreArgs += this.GetResourceTextByKey("Key_Client_Config_Db") + DBName + this.GetResourceTextByKey("Key_Client_Config_UserName") + userName + this.GetResourceTextByKey("Key_Client_Config_Password") + passWord;
                ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo();
                psi.FileName = ConfigurationSettings.AppSettings["ApplicationStartupPath"].ToString() + "\\CompanyFilterSQLRestore.exe";
                psi.Arguments = RestoreArgs;
                System.Diagnostics.Process.Start(psi);
                addStatus(this.GetResourceTextByKey(1, "MSG_CONFIG_DBCONNECTIONPROCESS") + type + this.GetResourceTextByKey(1, "MSG_CONFIG_DBCONNECTIONWAITPROCESS"));//MSG_CONFIG_DBCONNECTIONWAITPROCESS
                addStatus(backupFile);
                addStatus(RestoreArgs);
            }
            catch (Exception ex)
            {
                addStatus(this.GetResourceTextByKey(1, "MSG_CONFIG_DBCONNECTIONRESTORE") + type + this.GetResourceTextByKey(1, "MSG_SPLASH_CONNET_TO_DB_FAILURE"));//MSG_SPLASH_CONNET_TO_DB_FAILURE
                ExceptionManager.Publish(ex);
            }
        }

        //Attempt to connect to the DB using the provided SQL Connection string
        private void TestDBConnection(string SQLConnection, int timeout)
        {
            try
            {
                addStatus(this.GetResourceTextByKey(1, "MSG_CONFIG_DATABASEERROR"));//MSG_CONFIG_DATABASEERROR

                using (SqlConnection conn = new SqlConnection(SQLConnection))
                {

                    SqlCommand SQLCommand = new SqlCommand();
                    SQLCommand.Connection = conn;
                    SQLCommand.CommandTimeout = timeout;
                    addStatus(this.GetResourceTextByKey(1, "MSG_CONFIG_DATABASCONNECTING"));//MSG_CONFIG_DATABASCONNECTING
                    SQLCommand.Connection.Open();
                    SQLCommand.Connection.Close();
                };
                //GetInitialSettings();
                //if (blsRegulatoryEnabled==true && sRegulatoryType.Length>0)
                //{
                //    ResizeControls();
                //}
                addStatus(this.GetResourceTextByKey(1, "MSG_CONFIG_DBCONNECTIONSUCCESSFUL"));//MSG_CONFIG_DBCONNECTIONSUCCESSFUL
            }
            catch (Exception ex)
            {
                addStatus(this.GetResourceTextByKey(1, "MSG_CONFIG_DBERROR"));//MSG_CONFIG_DBERROR
                ExceptionManager.Publish(ex);
            }
        }

        #endregion common functions

        #region config stored in DB

        //overloaded method for displaying the DB name in status
        private void TestDBConnection(string SQLConnection, string sDBName, int timeout)
        {
            string sAdditionalStatsmsg = string.Empty;

            if (sDBName != string.Empty)
                //sAdditionalStatsmsg = " for " + sDBName;
                sAdditionalStatsmsg = this.GetResourceTextByKey("Key_For") + sDBName;

            try
            {
                addStatus(this.GetResourceTextByKey(1, "MSG_CONFIG_DBTESTING") + sAdditionalStatsmsg + "...");//MSG_CONFIG_DBTESTING
                if (sDBName.Length > 0)
                    SQLConnection += ";DATABASE=" + sDBName + ";";
                SQLConnection += "CONNECTION TIMEOUT=" + timeout + ";";
                SqlConnection conn = new SqlConnection(SQLConnection);
                SqlCommand SQLCommand = new SqlCommand();
                SQLCommand.Connection = conn;
                SQLCommand.Connection.Open();
                SQLCommand.Connection.Close();
                addStatus(this.GetResourceTextByKey(1, "MSG_CONFIG_DBCONNECTIONSUCCESSFUL"));//MSG_CONFIG_DBCONNECTIONSUCCESSFUL
            }
            catch (Exception ex)
            {
                //addStatus("Failed to test Database Connection.");//
                addStatus(this.GetResourceTextByKey(1, "Key_Client_Config_ConnectionFailed"));
                ExceptionManager.Publish(ex);
            }
        }

        private bool CheckifDBExists(string SQLConnection, string sDBName, int timeout)
        {
            bool bDBExisits = false;
            try
            {
                addStatus(this.GetResourceTextByKey(1, "MSG_CONFIG_DBTESTING") + sDBName + "...");
                SQLConnection += ";DATABASE=" + sDBName + ";";
                SQLConnection += "CONNECTION TIMEOUT=" + timeout + ";";
                //SQLConnection += this.GetResourceTextByKey("Key_Database") + sDBName + ";";
                //SQLConnection += this.GetResourceTextByKey("Key_ConnectionTimeOut") + timeout + ";";
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
            string sKey = string.Empty;
            //bool bUseHex =true;
            //RegistryKey RegKey;
            string SQLConnect = "";
            try
            {
                //RegKey = Registry.LocalMachine.OpenSubKey(BMC.Common.ConfigurationManagement.ConfigManager.Read("RegistryPath"));
                //addStatus("Retrieving SQL Connection Info...");
                //SQLConnect = RegKey.GetValue("SQLConnect").ToString();
                SQLConnect = DatabaseHelper.GetEnterpriseConnectionString();
                //if (!SQLConnect.ToUpper().Contains("SERVER"))
                //{
                //    SQLConnect = BMC.Common.Utilities.DatabaseHelper.GetConnectionString();
                //}

                addStatus(this.GetResourceTextByKey(1, "MSG_CONFIG_DONE"));//MSG_CONFIG_DONE
                //RegKey.Close();
                if (blnShouldPopulate)
                {
                    parseSQLConnect(SQLConnect, "Enterprise");
                }
                return SQLConnect;
            }
            catch (Exception ex)
            {
                addStatus(this.GetResourceTextByKey(1, "MSG_CONFIG_DBCONNECTIONSTRING"));//MSG_CONFIG_DBCONNECTIONSTRING
                ExceptionManager.Publish(ex);
                return string.Empty;
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
                strKeyvalue = this.GetRegistryString("DefaultLogDir", "", "C:\\Logs");
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
                    addStatus(this.GetResourceTextByKey(1, "MSG_CONFIG_DBCONNECTIONSQL"));//MSG_CONFIG_DBCONNECTIONSQL
                    // BMC.Common.Utilities.DatabaseHelper.StoreConnectionString(txtSvrName.Text, lblExDBName.Text, txtUserName.Text, txtPassword.Text, iSQLTimeOut);                   
                    BMC.Common.Utilities.DatabaseHelper.StoreConnectionString(serverName, lblExDBName.Text, txtUserName.Text, txtPassword.Text, iSQLTimeOut);
                    BMC.Common.Utilities.DatabaseHelper.StoreConnectionString(serverName, "Audit", txtUserName.Text, txtPassword.Text, iSQLTimeOut);
                    //addStatus("Done!");
                    this.GetResourceTextByKey("Key_DoneExclamation");
                }
                catch (Exception ex)
                {
                    addStatus(this.GetResourceTextByKey(1, "MSG_CONFIG_DBCONNECTIONFAILED"));//MSG_CONFIG_DBCONNECTIONFAILED
                    ExceptionManager.Publish(ex);
                }
            }
            else
            {
                addStatus(this.GetResourceTextByKey(1, "MSG_CONFIG_DBCONNECTIONMANDATORY"));//MSG_CONFIG_DBCONNECTIONMANDATORY
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
                    //MessageBox.Show(this.GetResourceTextByKey(1, "MSG_ENTER_SERVER_NAME"));
                    Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_ENTER_SERVER_NAME"), this.Text);
                    return;
                }
                if (string.IsNullOrEmpty(txtUserName.Text))
                {
                    //MessageBox.Show(this.GetResourceTextByKey(1, "MSG_ENTER_USER_NAME"));
                    Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_ENTER_USER_NAME"), this.Text);
                    return;
                }
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
            //string Result = "";
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                if (string.IsNullOrEmpty(txtSvrName.Text))
                {
                    //MessageBox.Show(this.GetResourceTextByKey(1, "MSG_ENTER_SERVER_NAME"));
                    Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_ENTER_SERVER_NAME"), this.Text);
                    txtSvrName.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtUserName.Text))
                {
                    //MessageBox.Show(this.GetResourceTextByKey(1, "MSG_ENTER_USER_NAME"));
                    Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_ENTER_USER_NAME"), this.Text);
                    txtUserName.Focus();
                    return;
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

                if (txtLogFilePath.Text.Trim() != string.Empty)
                {
                    if (Directory.Exists(txtLogFilePath.Text.Trim()))
                    {
                        this.SetRegistryString("DefaultLogDir", txtLogFilePath.Text, "");
                        addStatus(this.GetResourceTextByKey("Key_EC_LogsFolderSaved"));//Logs folder detail saved successfully
                    }
                    else
                    {
                        if (Win32Extensions.ShowQuestionMessageBox(this, this.GetResourceTextByKey("Key_EC_CreateLogFolder"), this.Text) == DialogResult.Yes)//Folder does not exists. Do you want to create?
                        {
                            Directory.CreateDirectory(txtLogFilePath.Text.Trim());
                            this.SetRegistryString("DefaultLogDir", txtLogFilePath.Text, "");
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

                SaveConnSettings();

                this.SetRegistryString("SQLCommandTimeout", txtCommandTimeout.Text, "");

                addStatus(this.GetResourceTextByKey(1, "MSG_CONFIG_DBCONNECTIONSUCCESS"));//MSG_CONFIG_DBCONNECTIONSUCCESS
            }


            catch (Exception ex)
            {
                addStatus(this.GetResourceTextByKey(1, "MSG_CONFIG_DBCONNECTIONFAIL"));//MSG_CONFIG_DBCONNECTIONFAIL
                ExceptionManager.Publish(ex);
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
                addStatus(this.GetResourceTextByKey(1, "MSG_CONFIG_DBCONNECTIONNUMBER"));//MSG_CONFIG_DBCONNECTIONNUMBER
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





        #endregion events generated by controls

        private void btnEntKey_Click(object sender, EventArgs e)
        {
            string EncKey = string.Empty;
            //RegistryKey regKey;
            string sConnect = string.Empty;
            try
            {
                /*
                regKey = Registry.LocalMachine.OpenSubKey(BMC.Common.ConfigurationManagement.ConfigManager.Read("RegistryPath"), true);
                if (regKey.GetValue("EnterpriseKey") != null)
                    EncKey = regKey.GetValue("EnterpriseKey").ToString();

                if (EncKey == string.Empty)
                    regKey.SetValue("EnterpriseKey", CryptographyHelper.Encrypt(CryptographyHelper.GetHashString(DateTime.Now.Ticks.ToString())));                                 
               
                regKey.Close();
                */

                EncKey = BMCRegistryHelper.GetRegKeyValue(ConfigManager.Read("RegistryPath"), "EnterpriseKey");
                if (EncKey == string.Empty)
                    BMCRegistryHelper.SetRegKeyValue(ConfigManager.Read("RegistryPath"), "EnterpriseKey", RegistryValueKind.String, CryptographyHelper.Encrypt(CryptographyHelper.GetHashString(DateTime.Now.Ticks.ToString())));

                //MessageBox.Show(this.GetResourceTextByKey(1, "MSG_KEY_CREATED"));
                Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_KEY_CREATED"), this.Text);
            }
            catch (Exception Ex)
            {
                //MessageBox.Show(Ex.Message);
                Win32Extensions.ShowErrorMessageBox(this, Ex.Message, this.Text);
            }

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
                ////Test DB Connection for Enterprise.
                //if (chDatabase == 'L')
                //{
                //     AddServerDetails(strServer, strUsername, strPassword,txtLGEdatabase.Text,strTimeOut);
                //}
                //else 
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
            //bool bResult = false;
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



        private bool ValidateURL(string sUrl)
        {
            bool bReturn = false;
            string sWebExtension = string.Empty;
            Regex objRegexUrlvalidate = new Regex("^(http|ftp)://(www\\.)?.+\\.(com|net|org|asmx)$");
            MatchCollection objMatchCollect;

            if (sUrl.Trim().Length < 0 || sUrl.Trim().Length == 0)
            {
                //MessageBox.Show(this.GetResourceTextByKey(1, "MSG_INVALID_WEB_SERVER"), this.GetResourceTextByKey(1, "MSG_BMC_ENTERPRISE_CONFIGTITLE"), MessageBoxButtons.OK, MessageBoxIcon.Information);                                
                Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_INVALID_WEB_SERVER"), this.Text);
            }
            else
            {
                if (!sUrl.Contains(".asmx"))
                {
                    sWebExtension = ConfigManager.Read("WebserviceExtension");
                    strUrlvalidate = "http://" + sUrl.Trim() + sWebExtension;
                }
                else
                {
                    strUrlvalidate = sUrl.Trim();
                }

                objMatchCollect = objRegexUrlvalidate.Matches(strUrlvalidate);
                if (objMatchCollect.Count > 0)
                {
                    bReturn = true;
                }
                else
                {
                    //MessageBox.Show(this.GetResourceTextByKey(1, "MSG_INCORRECT_WEB_URL"), this.GetResourceTextByKey(1, "MSG_BMC_ENTERPRISE_CONFIGTITLE"), MessageBoxButtons.OK, MessageBoxIcon.Error);                                        
                    Win32Extensions.ShowErrorMessageBox(this, this.GetResourceTextByKey(1, "MSG_INVALID_WEB_SERVER"), this.Text);
                    bReturn = false;
                }
            }
            return bReturn;
        }

        private static string GetDefaultBrowserPath()
        {

            string key = @"htmlfile\shell\open\command";

            RegistryKey registryKey = Registry.ClassesRoot.OpenSubKey(key, false);

            // get default browser path

            return ((string)registryKey.GetValue(null, null)).Split('"')[1];

        }
        public bool SetRegistryString(string sKey, string sValue, string sPath)
        {
            BMC.Common.Utilities.BMCRegistryHelper.SetRegKeyValue(sPath, sKey, RegistryValueKind.String, sValue);
            return true;
        }
        public string GetRegistryString(string sKey, string sPath, string sDefault)
        {
            try
            {
                return BMC.Common.Utilities.BMCRegistryHelper.GetRegKeyValue(sPath, sKey, sDefault);
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
                return sDefault;
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

        private void btnBrowseLogPath_Click_1(object sender, EventArgs e)
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
