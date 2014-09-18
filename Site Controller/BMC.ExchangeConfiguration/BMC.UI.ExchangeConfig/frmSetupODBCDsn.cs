using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using BMC.Business.ExchangeConfig;
using BMC.Transport.ExchangeConfig;
using BMC.Common.ExceptionManagement;


namespace BMC.UI.ExchangeConfig
{
    public partial class frmSetupODBCDsn : Form
    {
        const string strBMCConfig = "BMC Exchange Configuration";

        private frmSetupODBCDsn()
        {         
       
            InitializeComponent();
            PaintGradient();
        }

        public frmSetupODBCDsn(string sServer, string sUserName, string sPwd)
        {
            try
            {
                ExchangeConfigRegistryEntities.ODBCServer = sServer;
                ExchangeConfigRegistryEntities.ODBCUsername = sUserName;
                ExchangeConfigRegistryEntities.ODBCPwd = sPwd;
                InitializeComponent();
                PaintGradient();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private bool ValidateText(TextBox tBox, string Message)
        {
            bool bStatus = false;
            if (tBox.Text.Length == 0)
            {               
                errValidate.SetError(tBox, "Please enter " + Message);
                bStatus = false;
            }
            else
            {
                errValidate.SetError(tBox, "");
                bStatus = true;
            }
            return bStatus;
        }

        private bool ValidateText(ComboBox tBox, string Message)
        {
            bool bStatus = false;
            if (tBox.Text == string.Empty ||tBox.Text.ToUpper()== "<NO AVAILABLE SQL DATABASES>")
            {
                errValidate.SetError(tBox, "Please enter " + Message);
                bStatus = false;
            }
            else
            {
                errValidate.SetError(tBox, "");
                bStatus = true;
            }
            return bStatus;
        }

        private void PaintGradient()
        {
            string strBMPath = string.Empty;
            System.Drawing.Drawing2D.LinearGradientBrush gradBrushButton;
            Graphics grObject;
            System.Drawing.Drawing2D.ColorBlend clrblend = null;
            Rectangle objrect;
            Bitmap objbmp = null;

            Color[] clrSet = new Color[4]{                                      
                                    Color.FromArgb(119,187,255),                                                                        
                                     Color.FromArgb(210,232,255),
                                     Color.FromArgb(232,244,255),
                                    Color.FromArgb(255,255,255)};
            clrblend = new System.Drawing.Drawing2D.ColorBlend();
            clrblend.Colors = clrSet;
            Single[] bPts = new Single[4]{
                                            0,                                          
                                            0.5F,
                                            0.8F,                                          
                                            1};
            clrblend.Positions = bPts;
            gradBrushButton = new System.Drawing.Drawing2D.LinearGradientBrush(new
                   Point(0, 0), new Point(this.Width, this.Height), Color.FromArgb(217, 230, 255), Color.White);
            gradBrushButton.InterpolationColors = clrblend;
            objrect = new Rectangle(0, 0, this.Width, this.Height);
            objbmp = new Bitmap(this.Width, this.Height);

            grObject = Graphics.FromImage(objbmp);
            grObject.FillRectangle(gradBrushButton, objrect);

            btnCancel.BackgroundImage = objbmp;
            btnCancel.BackgroundImageLayout = ImageLayout.Stretch;

            btnOK.BackgroundImage = objbmp;
            btnOK.BackgroundImageLayout = ImageLayout.Stretch;

            btnTestConnection.BackgroundImage = objbmp;
            btnTestConnection.BackgroundImageLayout = ImageLayout.Stretch;

            SsTrip.BackgroundImage = objbmp;
            SsTrip.BackgroundImageLayout = ImageLayout.Stretch;

            this.BackgroundImage = objbmp;
            this.BackgroundImageLayout = ImageLayout.Stretch;

        }

        void LoadServers()
        {
            try
            {
                //Retrieve the available servers.
                List<string> ListResult = DBSettings.GetServers();
                for (int j = 0; j < ListResult.Count; j++)
                {
                    cmbServer.Items.Add(ListResult[j]);
                }
                if (this.cmbServer.Items.Count > 0)
                    this.cmbServer.SelectedIndex = 0;
                else
                    this.cmbServer.Text = "<No available SQL Servers>";
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        void LoadDatabases()
        {
            try
            {
                //Retrieve the available Databases.
                List<string> ListResult = DBSettings.GetDatabases(ExchangeConfigRegistryEntities.ODBCServer,
                ExchangeConfigRegistryEntities.ODBCUsername, ExchangeConfigRegistryEntities.ODBCPwd);
                for (int j = 0; j < ListResult.Count; j++)
                {
                    cmbDefaultDB.Items.Add(ListResult[j]);
                }
                if (this.cmbDefaultDB.Items.Count > 0)
                    this.cmbDefaultDB.SelectedIndex = 0;
                else
                    this.cmbDefaultDB.Text = "<No available SQL Databases>";
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        void LoadLanguages()
        {
            //Retrieve the available Languages.
            try
            {
                List<string> ListResult = DBSettings.GetLanguages(ExchangeConfigRegistryEntities.ODBCServer,
                ExchangeConfigRegistryEntities.ODBCUsername, ExchangeConfigRegistryEntities.ODBCPwd);
                for (int j = 0; j < ListResult.Count; j++)
                {
                    cmbDefaultLang.Items.Add(ListResult[j]);
                }
                if (this.cmbDefaultLang.Items.Count > 0)
                    this.cmbDefaultLang.SelectedIndex = 0;
                else
                    this.cmbDefaultLang.Text = "<No available SQL languages>";
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void frmSetupODBCDsn_Load(object sender, EventArgs e)
        {
            try
            {
                LoadServers();
                LoadDatabases();
                LoadLanguages();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void rbSqlAuthentication_CheckedChanged(object sender, EventArgs e)
        {
            pnlLoginCredentials.Enabled = true;
            txtLoginName.Text = ExchangeConfigRegistryEntities.ODBCUsername;
            txtpwd.Text = ExchangeConfigRegistryEntities.ODBCPwd;
        }

        private void rbWindowsAuthentication_CheckedChanged(object sender, EventArgs e)
        {
            pnlLoginCredentials.Enabled = false;
            txtLoginName.Text = string.Empty;
            txtpwd.Text = string.Empty;
        }

        private void btnTestConnection_Click(object sender, EventArgs e)
        {
            try
            {
                string connectionstring = string.Empty;
                if (rbSqlAuthentication.Checked == true)
                {
                    if (ValidateText(txtDSReferName, "Data Source Reference Name"))
                    {
                        if (ValidateText(cmbServer, "Server"))
                        {
                            if (ValidateText(txtLoginName, "UserName"))
                            {
                                if (ValidateText(txtpwd, "Password"))
                                {
                                    if (ValidateText(cmbDefaultDB, "Default Database"))
                                    {
                                        if (ValidateText(cmbDefaultLang, "Default Language"))
                                        {
                                            connectionstring = "DRIVER={SQL Server};" +
                                                                        "SERVER=" + cmbServer.Text.ToString() +
                                                                        ";Trusted_connection=No" +
                                                                        ";DATABASE=" + cmbDefaultDB.Text.ToString() +
                                                                        ";Uid=" + txtLoginName.Text +
                                                                        ";Pwd=" + txtpwd.Text + ";";
                                            if (BMC.DBInterface.ExchangeConfig.DBBuilder.TestODBCConnection(connectionstring))
                                                MessageBox.Show("Test Data Source Successful", strBMCConfig, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (ValidateText(txtDSReferName, "Data Source Reference Name"))
                    {
                        if (ValidateText(cmbServer, "Server"))
                        {
                            if (ValidateText(cmbDefaultDB, "Default Database"))
                            {
                                if (ValidateText(cmbDefaultLang, "Default Language"))
                                {
                                    connectionstring = "DRIVER={SQL Server};" +
                                                                   "SERVER=" + cmbServer.Text.ToString() +
                                                                   ";Trusted_connection=YES" +
                                                                   ";DATABASE=" + cmbDefaultDB.Text.ToString();
                                    if (BMC.DBInterface.ExchangeConfig.DBBuilder.TestODBCConnection(connectionstring))
                                        MessageBox.Show("Test Data Source Successful", strBMCConfig, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                ExchangeConfigRegistryEntities.ODBCRegKeyValue = UIConstants.strODBCRegPath;
                if (txtDSReferName.Text != string.Empty)
                {
                    ExchangeConfigRegistryEntities.DataSourceReferenceName = (txtDSReferName.Text != string.Empty ? txtDSReferName.Text : "Leisure SQL");
                    ExchangeConfigRegistryEntities.ODBCDescription = txtDescription.Text;
                    if (cmbServer.Text != string.Empty || cmbServer.Text != null)
                    {
                        ExchangeConfigRegistryEntities.ODBCServer = (cmbServer.Text != null ? cmbServer.Text.ToString() : "(local)");
                        ExchangeConfigRegistryEntities.DefaultDatabase = (cmbDefaultDB.Text != null ? cmbDefaultDB.Text.ToString() : "Exchange");
                        ExchangeConfigRegistryEntities.DefaultLanguage = (cmbDefaultLang.Text != null ? cmbDefaultLang.Text.ToString() : "british");
                        if (rbSqlAuthentication.Checked == true)
                        {
                            if (ValidateText(txtLoginName, "Login name"))
                            {
                                ExchangeConfigRegistryEntities.ODBCUsername = txtLoginName.Text;
                                if (ValidateText(txtpwd, "Password"))
                                {
                                    ExchangeConfigRegistryEntities.ODBCPwd = txtpwd.Text;
                                    if (ReadServicesSettings.DSNSettings(true))
                                        MessageBox.Show("Saved Data Source Successfully", strBMCConfig, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }
                        }
                        else
                        {
                            if (ReadServicesSettings.DSNSettings(false))
                            {
                                MessageBox.Show("Saved Data Source Successfully", strBMCConfig, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                    else
                        MessageBox.Show("Please enter Server", strBMCConfig, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                    MessageBox.Show("Please enter DSN Reference Name", strBMCConfig, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch(Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void cmbServer_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == Convert.ToChar(Keys.Enter))
                {
                    if (!string.IsNullOrEmpty(cmbServer.Text.ToString()))
                    {
                        cmbServer.Items.Add(cmbServer.Text.ToString());
                        return;

                    }
                }
            }
            catch (Exception ex)
            { ExceptionManager.Publish(ex); }
        }

        private void cmbDefaultDB_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == Convert.ToChar(Keys.Enter))
                {
                    if (!string.IsNullOrEmpty(cmbDefaultDB.Text.ToString()))
                    {
                        cmbDefaultDB.Items.Add(cmbDefaultDB.Text.ToString());
                        return;

                    }
                }
            }
            catch (Exception ex)
            {  ExceptionManager.Publish(ex);}
        }

        private void cmbDefaultLang_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == Convert.ToChar(Keys.Enter))
                {
                    if (!string.IsNullOrEmpty(cmbDefaultLang.Text.ToString()))
                    {
                        cmbDefaultLang.Items.Add(cmbDefaultLang.Text.ToString());
                        return;

                    }
                }
            }
            catch (Exception ex)
                {  ExceptionManager.Publish(ex);}
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            //txtDSReferName.Text = string.Empty;
            txtDescription.Text = string.Empty;
            //txtLoginName.Text = string.Empty;
            //txtpwd.Text = string.Empty;
            //cmbDefaultDB.Text = string.Empty;
            //cmbDefaultLang.Text = string.Empty;
            //cmbServer.Text = string.Empty;
            rbWindowsAuthentication.Checked = true;
            rbSqlAuthentication.Checked = false;

            LoadServers();
            LoadDatabases();
            LoadLanguages();
        }      

    }
}