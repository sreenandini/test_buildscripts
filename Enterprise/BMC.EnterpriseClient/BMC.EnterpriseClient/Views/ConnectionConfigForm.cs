
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
//GDI+
using System.Data.SqlClient;
//Weird stuff
using System.Diagnostics; //Shell and more
//DNS
using System.IO; //files
using System.Text.RegularExpressions;
using System.Windows.Forms;
using BMC.Common.ConfigurationManagement;
using BMC.Common.ExceptionManagement;
using BMC.Common.Security;
using Microsoft.Win32; //Registry
using BMC.CoreLib.Win32;
using BMC.Common.Utilities;
using BMC.Common;

namespace BMC.EnterpriseClient.Views
{
    /// <summary>
    /// Summary description for Form1.
    /// </summary>
    public class ConnectionConfigForm : System.Windows.Forms.Form
    {
        #region Form generated variables

        private System.Windows.Forms.TextBox txtStatus;
        private System.Windows.Forms.FolderBrowserDialog fldBNet;
        private System.Windows.Forms.MainMenu mainMenu1;
        private System.Windows.Forms.FolderBrowserDialog fldBLocal;
        private GroupBox groupBox3;
        private Label lblExDBName;
        private Label label2;
        private TextBox txtSvrName;
        private TextBox txtPassword;
        private Label label5;
        private Label label3;
        private Label label6;
        private Label label1;
        private TextBox txtInstName;
        private Label label4;
        private TextBox txtUserName;
        private Panel panel1;
        private Label label11;
        private Label label13;
        private ComboBox cmbLocalIPs;
        private Label label15;
        private Button button2;
        private Label label9;
        private Label label23;
        private Label label14;
        private TextBox txtSQLTimeout;
        private Button btnTestConn;
        private Button btnSaveConn;
        private IContainer components;

        #endregion Form generated variables
        private TextBox txtCommandTimeout;
        private Label label7;
        private Label label8;

        bool isEnterpriseClient = false;

        #region form related/main

        public ConnectionConfigForm()
        {
            isEnterpriseClient = BMCRegistryHelper.IsEnterpriseClient();
            InitializeComponent();
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

        private void ConnectionConfigForm_Load(object sender, System.EventArgs e)
        {            
            try
            {

                lblExDBName.Text = ENTERPRISEDBNAME;

                addStatus("Initialising...");
                this.Text = nameVersion;

                addStatus("Done!");
                CreateLogDir();
                GetConnRegSettings(true);

                txtCommandTimeout.Text = txtCommandTimeout.Text = BMCRegistryHelper.GetRegKeyValue("", "SQLCommandTimeOut", "60");

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
                addStatus("Error: " + ex.Message);
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConnectionConfigForm));
            this.txtStatus = new System.Windows.Forms.TextBox();
            this.fldBNet = new System.Windows.Forms.FolderBrowserDialog();
            this.mainMenu1 = new System.Windows.Forms.MainMenu(this.components);
            this.fldBLocal = new System.Windows.Forms.FolderBrowserDialog();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtCommandTimeout = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.btnSaveConn = new System.Windows.Forms.Button();
            this.label23 = new System.Windows.Forms.Label();
            this.btnTestConn = new System.Windows.Forms.Button();
            this.label14 = new System.Windows.Forms.Label();
            this.txtSQLTimeout = new System.Windows.Forms.TextBox();
            this.lblExDBName = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtSvrName = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtInstName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label11 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.cmbLocalIPs = new System.Windows.Forms.ComboBox();
            this.label15 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.groupBox3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtStatus
            // 
            this.txtStatus.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtStatus.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtStatus.ForeColor = System.Drawing.Color.DarkRed;
            this.txtStatus.Location = new System.Drawing.Point(1, 187);
            this.txtStatus.Multiline = true;
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.ReadOnly = true;
            this.txtStatus.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtStatus.Size = new System.Drawing.Size(692, 133);
            this.txtStatus.TabIndex = 2;
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
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.White;
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.txtCommandTimeout);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.btnSaveConn);
            this.groupBox3.Controls.Add(this.label23);
            this.groupBox3.Controls.Add(this.btnTestConn);
            this.groupBox3.Controls.Add(this.label14);
            this.groupBox3.Controls.Add(this.txtSQLTimeout);
            this.groupBox3.Controls.Add(this.lblExDBName);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.txtSvrName);
            this.groupBox3.Controls.Add(this.txtPassword);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.txtInstName);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.txtUserName);
            this.groupBox3.ForeColor = System.Drawing.Color.Coral;
            this.groupBox3.Location = new System.Drawing.Point(1, 27);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(692, 154);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Enterprise Setup";
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.label8.ForeColor = System.Drawing.Color.Red;
            this.label8.Location = new System.Drawing.Point(389, 129);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(63, 16);
            this.label8.TabIndex = 16;
            this.label8.Text = "Seconds";
            // 
            // txtCommandTimeout
            // 
            this.txtCommandTimeout.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.txtCommandTimeout.Location = new System.Drawing.Point(363, 126);
            this.txtCommandTimeout.MaxLength = 3;
            this.txtCommandTimeout.Name = "txtCommandTimeout";
            this.txtCommandTimeout.Size = new System.Drawing.Size(20, 20);
            this.txtCommandTimeout.TabIndex = 15;
            this.txtCommandTimeout.Text = "30";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Red;
            this.label7.Location = new System.Drawing.Point(242, 130);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(110, 13);
            this.label7.TabIndex = 14;
            this.label7.Text = "Command Timeout";
            // 
            // btnSaveConn
            // 
            this.btnSaveConn.BackColor = System.Drawing.SystemColors.Control;
            this.btnSaveConn.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveConn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnSaveConn.Location = new System.Drawing.Point(537, 69);
            this.btnSaveConn.Name = "btnSaveConn";
            this.btnSaveConn.Size = new System.Drawing.Size(137, 27);
            this.btnSaveConn.TabIndex = 18;
            this.btnSaveConn.Text = "Save Settings";
            this.btnSaveConn.UseVisualStyleBackColor = false;
            this.btnSaveConn.Click += new System.EventHandler(this.btnSaveConn_Click);
            // 
            // label23
            // 
            this.label23.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.label23.ForeColor = System.Drawing.Color.Red;
            this.label23.Location = new System.Drawing.Point(389, 105);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(63, 16);
            this.label23.TabIndex = 13;
            this.label23.Text = "Seconds";
            // 
            // btnTestConn
            // 
            this.btnTestConn.BackColor = System.Drawing.SystemColors.Control;
            this.btnTestConn.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTestConn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnTestConn.Location = new System.Drawing.Point(537, 25);
            this.btnTestConn.Name = "btnTestConn";
            this.btnTestConn.Size = new System.Drawing.Size(137, 27);
            this.btnTestConn.TabIndex = 17;
            this.btnTestConn.Text = "Test Connection";
            this.btnTestConn.UseVisualStyleBackColor = false;
            this.btnTestConn.Click += new System.EventHandler(this.btnTestConn_Click);
            // 
            // label14
            // 
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.label14.ForeColor = System.Drawing.Color.Red;
            this.label14.Location = new System.Drawing.Point(242, 105);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(120, 15);
            this.label14.TabIndex = 11;
            this.label14.Text = "Connection Timeout";
            // 
            // txtSQLTimeout
            // 
            this.txtSQLTimeout.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.txtSQLTimeout.Location = new System.Drawing.Point(363, 100);
            this.txtSQLTimeout.MaxLength = 3;
            this.txtSQLTimeout.Name = "txtSQLTimeout";
            this.txtSQLTimeout.Size = new System.Drawing.Size(20, 20);
            this.txtSQLTimeout.TabIndex = 12;
            this.txtSQLTimeout.Text = "30";
            // 
            // lblExDBName
            // 
            this.lblExDBName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblExDBName.ForeColor = System.Drawing.Color.Black;
            this.lblExDBName.Location = new System.Drawing.Point(112, 110);
            this.lblExDBName.Name = "lblExDBName";
            this.lblExDBName.Size = new System.Drawing.Size(71, 19);
            this.lblExDBName.TabIndex = 10;
            this.lblExDBName.Text = "Enterprise";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(192, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(21, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "\\\\";
            // 
            // txtSvrName
            // 
            this.txtSvrName.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSvrName.Location = new System.Drawing.Point(16, 32);
            this.txtSvrName.MaxLength = 50;
            this.txtSvrName.Name = "txtSvrName";
            this.txtSvrName.Size = new System.Drawing.Size(148, 20);
            this.txtSvrName.TabIndex = 1;
            // 
            // txtPassword
            // 
            this.txtPassword.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPassword.Location = new System.Drawing.Point(244, 75);
            this.txtPassword.MaxLength = 50;
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(159, 20);
            this.txtPassword.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.DarkOrange;
            this.label5.Location = new System.Drawing.Point(244, 59);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(72, 16);
            this.label5.TabIndex = 7;
            this.label5.Text = "Password";
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.ForestGreen;
            this.label3.Location = new System.Drawing.Point(244, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 16);
            this.label3.TabIndex = 3;
            this.label3.Text = "Instance Name";
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.DarkOrange;
            this.label6.Location = new System.Drawing.Point(15, 110);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(96, 16);
            this.label6.TabIndex = 9;
            this.label6.Text = "Database Name";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkOrange;
            this.label1.Location = new System.Drawing.Point(15, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Server Name";
            // 
            // txtInstName
            // 
            this.txtInstName.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtInstName.Location = new System.Drawing.Point(247, 32);
            this.txtInstName.MaxLength = 50;
            this.txtInstName.Name = "txtInstName";
            this.txtInstName.Size = new System.Drawing.Size(156, 20);
            this.txtInstName.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.DarkOrange;
            this.label4.Location = new System.Drawing.Point(14, 58);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 16);
            this.label4.TabIndex = 5;
            this.label4.Text = "User Name";
            // 
            // txtUserName
            // 
            this.txtUserName.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUserName.Location = new System.Drawing.Point(16, 75);
            this.txtUserName.MaxLength = 50;
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(150, 20);
            this.txtUserName.TabIndex = 6;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.label13);
            this.panel1.Location = new System.Drawing.Point(1, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(692, 21);
            this.panel1.TabIndex = 0;
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Font = new System.Drawing.Font("Arial", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.DarkOrange;
            this.label11.Location = new System.Drawing.Point(1, 4);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(692, 11);
            this.label11.TabIndex = 0;
            this.label11.Text = "Items REQUIRED ";
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.Color.Transparent;
            this.label13.Font = new System.Drawing.Font("Arial", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.ForestGreen;
            this.label13.Location = new System.Drawing.Point(1, 5);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(692, 16);
            this.label13.TabIndex = 30;
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
            // ConnectionConfigForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(697, 345);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.txtStatus);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Menu = this.mainMenu1;
            this.Name = "ConnectionConfigForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "SQL Connection SetUp Configuration";
            this.Load += new System.EventHandler(this.ConnectionConfigForm_Load);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        #region variables

        private bool blnChangesSaved = false;
        private bool blsRegulatoryEnabled = false;
        private string sRegulatoryType = string.Empty;
        private string ConnectType = "SQL";
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
                addStatus("Checking for Logs Dir...");

                string logPath = ConfigManager.Read("DefaultLogPath");

                if (!Directory.Exists(logPath))
                {
                    if (Directory.Exists(logPath.Substring(0, 2)))
                    {
                        addStatus("Creating Logs Dir...");
                        Directory.CreateDirectory(logPath);
                        addStatus("Done!");
                    }
                    else
                    {
                        addStatus("Logs Dir Not Found!");
                    }
                }
                else
                {
                    addStatus("Logs Dir Found!...");
                }
            }
            catch (Exception ex)
            {
                addStatus("Error Creating Logs Dir!: " + ex.Message);
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
            if (txtInstName.Text.Length > 0)
                returnString += "\\" + txtInstName.Text;
            returnString += ";UID=" + txtUserName.Text + ";PWD=" + txtPassword.Text + ";DATABASE=" + lblExDBName.Text + ";";

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
                addStatus("Error Starting Installer Removal Tool: " + ex.Message);
            }
        }

        private void parseString(object sender, System.EventArgs e)
        {
            //Use regular expressions to ensure that only correct characters are entered
            Control currCtl = (Control)sender;
            currCtl.Text = Regex.Replace(currCtl.Text, @"[^A-Za-z0-9\.\-\(\)\\\:]", "");
        }

        private void RestoreDB(string type)
        {
            //restore the database to the specified location from the backup provided
            try
            {
                addStatus("Launching Database Restore for " + type + "DB" + " ...");
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
                }
                else
                {
                    DBName = "MeterAnalysis";
                    backupFile = "MeterAnalysisBlankDB.bak";
                }
                RestoreArgs = "/Run" + " /File:" + ConfigurationSettings.AppSettings["ApplicationStartupPath"].ToString() + "\\database\\" + backupFile + " /Server:" + svrName;
                if (instName.Length > 0)
                    RestoreArgs += "\\" + instName;
                RestoreArgs += " /DB:" + DBName + " /UserName:" + userName + " /Password:" + passWord;
                ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo();
                psi.FileName = ConfigurationSettings.AppSettings["ApplicationStartupPath"].ToString() + "\\CompanyFilterSQLRestore.exe";
                psi.Arguments = RestoreArgs;
                System.Diagnostics.Process.Start(psi);
                addStatus("Restore Process Running for " + type + "! Wait for the success dialog before doing anything else!");
                addStatus(backupFile);
                addStatus(RestoreArgs);
            }
            catch (Exception ex)
            {
                addStatus("Error Restoring " + type + " Database: " + ex.Message);
            }
        }

        //Attempt to connect to the DB using the provided SQL Connection string
        private void TestDBConnection(string SQLConnection, int timeout)
        {
            try
            {
                addStatus("Testing Database Connection...");

                using (SqlConnection conn = new SqlConnection(SQLConnection))
                {

                    SqlCommand SQLCommand = new SqlCommand();
                    SQLCommand.Connection = conn;
                    SQLCommand.CommandTimeout = timeout;
                    addStatus("Please wait connecting to database..");
                    SQLCommand.Connection.Open();
                    SQLCommand.Connection.Close();
                };
                //GetInitialSettings();
                //if (blsRegulatoryEnabled==true && sRegulatoryType.Length>0)
                //{
                //    ResizeControls();
                //}
                addStatus("Database Connection Successful!");
            }
            catch (Exception ex)
            {
                addStatus("Error Testing Database Connection: " + ex.Message);
            }
        }

        #endregion common functions

        #region config stored in DB

        //overloaded method for displaying the DB name in status
        private void TestDBConnection(string SQLConnection, string sDBName, int timeout)
        {
            string sAdditionalStatsmsg = string.Empty;

            if (sDBName != string.Empty)
                sAdditionalStatsmsg = " for " + sDBName;

            try
            {
                addStatus("Testing Database Connection" + sAdditionalStatsmsg + "...");
                if (sDBName.Length > 0)
                    SQLConnection += ";DATABASE=" + sDBName + ";";
                SQLConnection += "CONNECTION TIMEOUT=" + timeout + ";";
                SqlConnection conn = new SqlConnection(SQLConnection);
                SqlCommand SQLCommand = new SqlCommand();
                SQLCommand.Connection = conn;
                SQLCommand.Connection.Open();
                SQLCommand.Connection.Close();
                addStatus("Database Connection Successful!");
            }
            catch (Exception ex)
            {
                addStatus("Error Testing Database Connection: " + ex.Message);
            }
        }

        private bool CheckifDBExists(string SQLConnection, string sDBName, int timeout)
        {
            bool bDBExisits = false;
            try
            {
                addStatus("Testing Database Connection for " + sDBName + "...");
                SQLConnection += ";DATABASE=" + sDBName + ";";
                SQLConnection += "CONNECTION TIMEOUT=" + timeout + ";";
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
            //bool bUseHex = true;
            //RegistryKey RegKey;
            string SQLConnect = "";
            try
            {
                //RegKey = Registry.LocalMachine.OpenSubKey(BMC.Common.ConfigurationManagement.ConfigManager.Read("RegistryPath"));
                addStatus("Retrieving SQL Connection Info...");
                //SQLConnect = RegKey.GetValue("SQLConnect").ToString();
                SQLConnect = BMCRegistryHelper.GetRegKeyValue(ConfigManager.Read("RegistryPath"), "SQLConnect");

                if (!SQLConnect.ToUpper().Contains("SERVER"))
                {
                    SQLConnect = BMC.Common.Utilities.DatabaseHelper.GetConnectionString();
                }

                addStatus("Done!");
                //RegKey.Close();
                if (blnShouldPopulate)
                {
                    parseSQLConnect(SQLConnect, "Enterprise");
                }
                return SQLConnect;
            }
            catch (Exception ex)
            {
                addStatus("Error Getting Registry Settings: " + ex.Message);
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
                    addStatus("Setting SQL Connection Info...");
                    // BMC.Common.Utilities.DatabaseHelper.StoreConnectionString(txtSvrName.Text, lblExDBName.Text, txtUserName.Text, txtPassword.Text, iSQLTimeOut);                   
                    BMC.Common.Utilities.DatabaseHelper.StoreConnectionString(serverName, lblExDBName.Text, txtUserName.Text, txtPassword.Text, iSQLTimeOut);
                    BMC.Common.Utilities.DatabaseHelper.StoreConnectionString(serverName, "Audit", txtUserName.Text, txtPassword.Text, iSQLTimeOut);

                    addStatus("Done!");
                }
                catch (Exception ex)
                {
                    addStatus("Error Setting Registry Settings:: " + ex.Message);
                }
            }
            else
            {
                addStatus("Server, Username and Database are Mandatory fields!");
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
                    this.ShowInfoMessageBox(this.GetResourceTextByKey(1," MSG_ENTER_SERVER_NAME"), this.Text);
                    return;
                }
                if (string.IsNullOrEmpty(txtUserName.Text))
                {
                    this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_ENTER_USER_NAME"), this.Text);
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
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                if (string.IsNullOrEmpty(txtSvrName.Text))
                {
                    this.ShowInfoMessageBox(this.GetResourceTextByKey(1, " MSG_ENTER_SERVER_NAME"), this.Text);
                    txtSvrName.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtUserName.Text))
                {
                    this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_ENTER_USER_NAME"), this.Text);
                    txtUserName.Focus();
                    return;
                }


                SaveConnSettings();

                //this.SetRegistryString("SQLCommandTimeout", txtCommandTimeout.Text, "Software\\Honeyframe");
                BMCRegistryHelper.SetRegKeyValue("", "SQLCommandTimeout", RegistryValueKind.String, txtCommandTimeout.Text);
                addStatus("Enterprise connection settings saved successfully.:");
            }


            catch (Exception ex)
            {
                addStatus("Error in saving Settings :" + ex.Message);
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
                addStatus("Timeout must be a number!");
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

                if (string.IsNullOrWhiteSpace(EncKey))
                    BMCRegistryHelper.SetRegKeyValue(ConfigManager.Read("RegistryPath"), "EnterpriseKey", RegistryValueKind.String, CryptographyHelper.Encrypt(CryptographyHelper.GetHashString(DateTime.Now.Ticks.ToString())));

                this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_KEY_CREATED"), this.Text);
            }
            catch (Exception Ex)
            {
                this.ShowInfoMessageBox(Ex.Message,this.Text);
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
                this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_INVALID_WEB_SERVER"), this.Text);
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
                    this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_INCORRECT_WEB_URL"), this.Text);
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

        /*
        public bool SetRegistryString(string sKey, string sValue, string sPath)
        {
            Registry.LocalMachine.OpenSubKey(sPath, true).SetValue(sKey, sValue, RegistryValueKind.String);
            return true;
        }
        */
        
        /*
        public string GetRegistryString(string sKey, string sPath, string sDefault)
        {
            try
            {
                return Registry.LocalMachine.OpenSubKey(sPath, true).GetValue(sKey, sDefault).ToString();

            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
                return sDefault;
            }
        }
         */
    }
}