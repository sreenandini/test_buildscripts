using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using BMC.Business.ExchangeConfig;
using BMC.Common.ExceptionManagement;
using BMC.Common.LogManagement;
using BMC.Common.ConfigurationManagement;

namespace BMC.UI.ExchangeConfig
{
    public partial class frmSQLRestore : Form
    {
        public frmSQLRestore(string Type)
        {
            InitializeComponent();
            PaintGradient();
            this.strType = Type;
        }
        #region Declaration
        const string strBMCConfig = "BMC Exchange Configuration";
        string strType = string.Empty;
        #endregion

        private void frmSQLRestore_Load(object sender, EventArgs e)
        {
            //Retrieve the available servers.
            GetSQLServers();
            txtPassword.Focus();
            SetStatus(false);
            txtDataBases.Text = strType;
        }
        /// <summary>
        /// To set the button and form color
        /// </summary>
        /// <param name=></param>
        /// <returns></returns>      
        /// Author             Date              Description
        /// ---------------------------------------------------
        /// Vineetha Mathew      19-02-2009        Intial Version 
        /// 
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

            btnRestore.BackgroundImage = objbmp;          
            btnRestore.BackgroundImageLayout = ImageLayout.Stretch;

            SsTrip.BackgroundImage = objbmp;
            SsTrip.BackgroundImageLayout = ImageLayout.Stretch;

            this.BackgroundImage = objbmp;
            this.BackgroundImageLayout = ImageLayout.Stretch;

        }

        private void GetSQLServers()
        {
            //Retrieve the available servers.
            List<string> ListResult = DBSettings.GetServers();
            for (int j = 0; j < ListResult.Count; j++)
            {
                cmbServers.Items.Add(ListResult[j]);
            }
            if (this.cmbServers.Items.Count > 0)
                this.cmbServers.SelectedIndex = 0;
            else
                this.cmbServers.Text = "<No available SQL Servers>";
        }

        //private void GetDatabases()
        //{
        //    //Retrieve the available Databases.
        //    List<string> objDBNames = DBSettings.GetDatabases(cmbServers.SelectedItem.ToString(), txtUser.Text, txtPassword.Text);
        //    if (objDBNames != null)
        //    {
        //        for (int i = 0; i < objDBNames.Count; i++)
        //        {
        //            cmbDatabases.Items.Add(objDBNames[i]);
        //        }
        //    }

        //    if (this.cmbDatabases.Items.Count > 0)
        //        this.cmbDatabases.SelectedIndex = 0;
        //    else
        //        this.cmbDatabases.Text = "NorthWind";
        //}

        private void btnRestore_Click(object sender, EventArgs e)
        {
            try
            {
                bool bResult = false;
                btnRestore.Enabled = false;
                string strDataBase = string.Empty;
                Dictionary<string, string> objServer = new Dictionary<string, string>();
                objServer.Add("SERVER", cmbServers.SelectedItem.ToString());
                objServer.Add("UID", txtUser.Text);
                objServer.Add("PASSWORD", txtPassword.Text);

                strDataBase = txtDataBases.Text;
                objServer.Add("DATABASE", strDataBase);
                if (ConfigManager.Read("Startuppath") != null)
                {

                    txtDBFile.Text = ConfigManager.Read("Startuppath");
                    
                }
                objServer.Add("LOCATION", txtDBFile.Text);
                LogManager.WriteLog("LOCATION:" + txtDBFile.Text.ToString(), LogManager.enumLogLevel.Debug);
                bool bDBExists = false;
                string sSQLServerDetails = GetConnectionString();
                bDBExists = DBSettings.CheckDBExists(sSQLServerDetails, strDataBase, 60);
                if (bDBExists == true)
                {
                    MessageBox.Show(strDataBase + " has not been restored. The DB already exists!", "Restore DB", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LogManager.WriteLog(strDataBase + " has not been restored. The DB already exists!", LogManager.enumLogLevel.Info);
                }
                else
                {
                    MessageBox.Show("The Restore Process Running for " + strDataBase + "." + " Wait for the success dialog before doing anything else!", "Restore DB", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    bResult = DBSettings.RestoreDB(strType, objServer);

                    if (bResult)
                    {
                        //MessageBox.Show("The database " + strDataBase + " has been restored.", "Restore DB", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LogManager.WriteLog("The database " + strDataBase + " has been restored.", LogManager.enumLogLevel.Info);
                    }
                    else
                    {
                        MessageBox.Show("The Restore Process failed for " + strDataBase + ".", "Restore DB", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        LogManager.WriteLog("The Restore Process failed for " + strDataBase + ".", LogManager.enumLogLevel.Info);
                    }
                }
            }
                catch(Exception ex)
                {
                    ExceptionManager.Publish(ex);
                    LogManager.WriteLog("btnRestore_Click:" + ex.Message + ex.Source.ToString(), LogManager.enumLogLevel.Error);
                }
            finally
            {
                btnRestore.Enabled = true;
            }
        }

        private string GetConnectionString()
        {
            string strConnectionString = string.Empty;
            if (cmbServers.SelectedItem != null && String.IsNullOrEmpty(txtUser.Text) == false && String.IsNullOrEmpty(txtPassword.Text) == false)
            {
                strConnectionString = "Server = " + cmbServers.SelectedItem.ToString() + ";Uid = " + txtUser.Text + ";pwd = " + txtPassword.Text;
            }
            LogManager.WriteLog("GetConnectionString():" + strConnectionString.Length.ToString(), LogManager.enumLogLevel.Debug);
            return strConnectionString;
        }

        private void lblConnectDB_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                if (ValidateDetails(txtUser, "Username"))
                {
                    if (ValidateDetails(txtPassword, "Password"))
                    {
                        if (cmbServers.Items.Count > 0)
                        {
                            if (cmbServers.SelectedIndex < 0)
                            {
                                errValidate.SetError(cmbServers, "Please select server Name");
                                //toolStripStatus.Text = "Please select server Name";
                            }
                            else
                            {
                                errValidate.SetError(cmbServers, string.Empty);
                                AddServerDetails(cmbServers.SelectedItem.ToString(), txtUser.Text, txtPassword.Text);
                            }
                            LogManager.WriteLog("Combo selection:" +cmbServers.SelectedItem.ToString(), LogManager.enumLogLevel.Debug);
                        }
                    }
                }
            }
            catch( Exception ex)
            {                
                 ExceptionManager.Publish(ex);
                 LogManager.WriteLog("lblConnectDB_LinkClicked:" + ex.Message + ex.Source.ToString(), LogManager.enumLogLevel.Error);
            }
        }


        private void SetStatus(bool status)
        {
            // cmbDatabases.Enabled = status;
            txtDataBases.Enabled = status;
            txtDBFile.Enabled = status;
            //btnDBBrowse.Enabled = status;
            btnRestore.Enabled = status;
        }

        private void txtUser_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ValidateDetails(txtUser, "Password");
        }


        private void txtPassword_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ValidateDetails(txtPassword, "Password");
        }


        private bool ValidateDetails(TextBox tBox, string Message)
        {
            bool bResult = false;
            try
            {
                if (tBox.Text.Length == 0)
                {
                    errValidate.SetError(tBox, "Please enter DB " + Message);
                    //toolStripStatus.Text = "Please enter DB " + Message;
                    bResult = false;
                }
                else
                {
                    errValidate.SetError(tBox, string.Empty);
                    bResult = true;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

            return bResult;
        }

        /// <summary>
        /// Test the DB Connection with the credentials entered
        /// </summary>
        /// <param name="Server"></param>
        /// <param name="UserName"></param>
        /// <param name="Password"></param>
        /// <param name="DataBase"></param>
        ///<param name="ConnectionTimeout"></param>
        /// Method Revision History
        ///
        /// Author             Date              Description
        /// ---------------------------------------------------
        /// Anuradha        12-Dec-2008        Intial Version 
        /// 


        private void AddServerDetails(string Server, string UserName, string Password)
        {
            bool bResult = false;
            try
            {
                Dictionary<string, string> objServerDetails = new Dictionary<string, string>();
                objServerDetails.Add("SERVER", Server);
                objServerDetails.Add("UID", UserName);
                objServerDetails.Add("PWD", Password);
                objServerDetails.Add("TIMEOUT", "60");

                string ReturnConnectionString = Credentials.MakeConnectionString(objServerDetails);
                LogManager.WriteLog("ReturnConnectionString:" + ReturnConnectionString.Length.ToString(), LogManager.enumLogLevel.Debug);
                if (!String.IsNullOrEmpty(ReturnConnectionString))
                {
                    bResult = Credentials.TestConnectionDB(ReturnConnectionString);
                    if (bResult == true)
                    {
                        MessageBox.Show("Test Connection  is successfull", strBMCConfig, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //toolStripStatus.Text = "Test Connection  is successfull";
                        SetStatus(true);
                        //Retrieve the available Databases.
                        // GetDatabases();
                    }
                    else
                    {
                        MessageBox.Show("Could not establish connection to server.Try Again ... ", strBMCConfig, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        //toolStripStatus.Text = "Could not establish connection to server.Try Again ... ";
                    }
                }
            }
            catch(Exception ex)
            {
                ExceptionManager.Publish(ex);
                LogManager.WriteLog("AddServerDetails"+ex.Message+ex.Source.ToString(),LogManager.enumLogLevel.Error);
            }
        }

        private void btnDBBrowse_Click(object sender, EventArgs e)
        {
            ofdSelectDB.ShowDialog();
            if (!String.IsNullOrEmpty(ofdSelectDB.FileName))
            {
                txtDBFile.Text = ofdSelectDB.FileName;
            }
            else
            {
                MessageBox.Show("Please select the path of DB to restore", strBMCConfig, MessageBoxButtons.OK, MessageBoxIcon.Information);
                //toolStripStatus.Text = "Please select the path of DB to restore";
            }
        }

        private void lblConnectDB_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }
        
       
    }
}