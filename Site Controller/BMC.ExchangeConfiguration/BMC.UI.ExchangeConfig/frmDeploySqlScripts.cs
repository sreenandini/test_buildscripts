using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Collections;
using System.Threading;
using Microsoft.Win32;
using System.IO;
using System.Data.SqlClient;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using BMC.Common.LogManagement;
using BMC.Common.ExceptionManagement;
using BMC.Business.ExchangeConfig;

namespace BMC.UI.ExchangeConfig
{
    public partial class frmDeployScripts : Form
    {
        string strScriptPath = string.Empty;
        string strSqlfilename = string.Empty;
        //System.Threading.Timer tmrDeploy;

        public frmDeployScripts()
        {
            InitializeComponent();
            PaintGradient();
        }

        public frmDeployScripts(string strPath)
        {
            InitializeComponent();
            strScriptPath = strPath;
            lblScriptPath.Text = strScriptPath;
            PaintGradient();
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

            btnRunScripts.BackgroundImage = objbmp;
            btnExit.BackgroundImage = objbmp;           
            btnRunScripts.BackgroundImageLayout = ImageLayout.Stretch;            
            btnExit.BackgroundImageLayout = ImageLayout.Stretch;

            this.BackgroundImage = objbmp;
            this.BackgroundImageLayout = ImageLayout.Stretch;

        }
  
        private void RunScripts()
        {
           
            string strScript=string.Empty;
            try
            {
                String strServer = "";
                String strUserName = "";
                String strPassword = "";
                String strArgs = "";

                String strConnect = "";
                ArrayList arrayListReg = new ArrayList();
                lblstatus.Text = "Processing.....";
                // tmrDeploy = new System.Threading.Timer(tmrDeploy_Tick,"Tick", 1000, 2000);
                //tmrDeploy.Enabled = true;
                //tmrDeploy.Tick += new EventHandler(tmrDeploy_Tick);
                strConnect = RegistrySettings.ExchangeConnectionString();
                //LogManager.WriteLog("strConnectExchange :" + strConnect, LogManager.enumLogLevel.Debug);


                arrayListReg.AddRange(strConnect.Trim().Split(';'));
                arrayListReg.TrimToSize();

                foreach (Object ar in arrayListReg)
                {
                    string[] arrKeys = new string[2];
                    arrKeys = ar.ToString().Trim().Split('=');
                    switch (arrKeys[0].Trim().ToString())
                    {
                        case "SERVER":
                            strServer = arrKeys[1].Trim().ToString();
                            break;
                        case "UID":
                            strUserName = arrKeys[1].Trim().ToString();
                            break;
                        case "PWD":
                            strPassword = arrKeys[1].Trim().ToString();
                            break;
                        default:
                            break;
                    }
                }


                strArgs = strServer + " " + strUserName + " " + strPassword;


                //LogManager.WriteLog("Credentials :" + strArgs, LogManager.enumLogLevel.Debug);
                //strMasterScriptPath = System.Configuration.ConfigurationManager.AppSettings["MasterScriptPath"].ToString().Trim();
                //strMeterAnalysisScriptPath = System.Configuration.ConfigurationManager.AppSettings["MeterAnalysisScriptPath"].ToString().Trim();
                //strEnterpriseScriptPath = System.Configuration.ConfigurationManager.AppSettings["EnterpriseScriptPath"].ToString().Trim();
                //strExchangeScriptPath = System.Configuration.ConfigurationManager.AppSettings["ExchangeScriptPath"].ToString().Trim();
                //strTicketingScriptPath = System.Configuration.ConfigurationManager.AppSettings["TicketingScriptPath"].ToString().Trim();
                //strCMktSDGScriptPath = System.Configuration.ConfigurationManager.AppSettings["CMktSDGScriptPath"].ToString().Trim();

                //DirectoryInfo obj = new DirectoryInfo(strScriptPath);
                //// FileInfo[] strFiles = obj.GetFiles();
                //FileInfo[] strFiles = ;
                //foreach (FileInfo sFile in strFiles)
                //{

                //LogManager.WriteLog("Before fileinfo :", LogManager.enumLogLevel.Debug);
                FileInfo sFile = new FileInfo(strScriptPath);
                //LogManager.WriteLog("strScriptPath again :" + strScriptPath, LogManager.enumLogLevel.Debug);
                strSqlfilename = sFile.Name;


                //LogManager.WriteLog("sFile  :" + sFile.FullName.ToString(), LogManager.enumLogLevel.Debug);
                strScript = sFile.OpenText().ReadToEnd();
                //LogManager.WriteLog("strScript  :" + strScript, LogManager.enumLogLevel.Debug);

                RunSql(strConnect, strScript);

                lblstatus.Text = "SQL Script Run Successfully";
                MessageBox.Show("SQL Script Run Successfully.", UIConstants.strBMCConfig, MessageBoxButtons.OK, MessageBoxIcon.Information);
              
                return;

            }
            catch (Exception ex)
            {
                lblstatus.Text = "An error has occured in the SQL Script Run";
                MessageBox.Show("An error has occured in the SQL Script Run.", UIConstants.strBMCConfig, MessageBoxButtons.OK, MessageBoxIcon.Error);
                ExceptionManager.Publish(ex);
                LogManager.WriteLog("Exception occured in  : " + strSqlfilename, LogManager.enumLogLevel.Info);
                return;
            }
            finally
            {
                btnRunScripts.Enabled = true;
            }
        }

        public void RunSql(string strConnect, string strScript)
        {
            SqlConnection SqlConn = null;
            Server Server = null;

            try
            {
                SqlConn = new SqlConnection(strConnect);
                Server = new Server(new ServerConnection(SqlConn));
                Server.ConnectionContext.ExecuteNonQuery(strScript);
            }
            finally
            {
                SqlConn.Close();
                SqlConn.Dispose();
            }
            
        }
        //void tmrDeploy_Tick(object str)
        //{
        //    if (pgBStatus.Value < 100)
        //    {
        //        if (this.InvokeRequired)
        //        {
        //            MethodInvoker objHelper = delegate { pgBStatus.Value = pgBStatus.Value + 1; };
        //            Invoke(objHelper);
        //        }
        //    }
        //} 

        private void btnRunScripts_Click(object sender, EventArgs e)
        {
            btnRunScripts.Enabled = false;
            RunScripts();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }        
    }
}