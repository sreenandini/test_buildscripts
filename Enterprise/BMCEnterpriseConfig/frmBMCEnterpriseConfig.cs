using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using BMC.Monitoring;
using BMC.Common.Utilities;
using BMC.Common.LogManagement;
using BMC.Common.ExceptionManagement;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.IO;
using BMC.Common.ConfigurationManagement;
using BMC.Business.EnterpriseConfig;
using BMC.Transport.EnterpriseConfig;
using Microsoft.Win32;
using BMC.Common.Security;
namespace BMC.UI.EnterpriseConfig
{
    public partial class frmBMCEnterpriseConfig : Form
    {
        #region Declarations
        string strConnection = string.Empty;       
        private string ReturnConnectionString=string.Empty;  
        List<PropertyClass> ChangedProperty = new List<PropertyClass>();        
        private string strRegistryKeyPath = string.Empty;      
        private string strScriptPath = string.Empty;
        private string strUpgradeVisible = string.Empty;
        private string[] strListarray = null;       
        private string strServiceStatus = string.Empty;        
        private bool bSelectAll = false;
        private string strUrlvalidate = string.Empty;        
        private bool blsRegulatoryEnabled = false;
        private string sRegulatoryType = string.Empty;
        #endregion
        public frmBMCEnterpriseConfig()
        {
            InitializeComponent();
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
            try
            {
                Color[] clrSet = new Color[4]{   
                                    //Color.Silver,                                                                        
                                    //Color.SeaShell,
                                    //Color.WhiteSmoke,
                                    //Color.White};
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
                       Point(0, 0), new Point(this.Width, this.Height), Color.FromArgb(217,230,255), Color.White);
                gradBrushButton.InterpolationColors = clrblend;
                objrect = new Rectangle(0, 0, this.Width, this.Height);               
                objbmp = new Bitmap(this.Width, this.Height);
                //}

                grObject = Graphics.FromImage(objbmp);
                grObject.FillRectangle(gradBrushButton, objrect);

                btnEnterpriseTestConnection.BackgroundImage = objbmp;
                btnSaveEnterpriseConnection.BackgroundImage = objbmp;
                btnenterpriseDBRestore.BackgroundImage = objbmp;
                btnMeterAnalysisTestConnection.BackgroundImage = objbmp;
                btnMeterAnalysisDBRestore.BackgroundImage = objbmp;
                btnLGEGatewayTestConnection.BackgroundImage = objbmp;
                
                btnTestURL.BackgroundImage = objbmp;
                
                btnStartService.BackgroundImage = objbmp;
                btnEndService.BackgroundImage = objbmp;
                btnRefreshServices.BackgroundImage = objbmp;
                btnSaveSettings.BackgroundImage = objbmp;
                btnRunUpgradeScript.BackgroundImage = objbmp;
                btnExit.BackgroundImage = objbmp;
                btnSelectAll.BackgroundImage = objbmp;
                btnEnterpriseTestConnection.BackgroundImageLayout = ImageLayout.Stretch;
                btnSaveEnterpriseConnection.BackgroundImageLayout = ImageLayout.Stretch;
                btnenterpriseDBRestore.BackgroundImageLayout = ImageLayout.Stretch;
                btnMeterAnalysisTestConnection.BackgroundImageLayout = ImageLayout.Stretch;
                btnMeterAnalysisDBRestore.BackgroundImageLayout = ImageLayout.Stretch;
                btnLGEGatewayTestConnection.BackgroundImageLayout = ImageLayout.Stretch;
                
                btnTestURL.BackgroundImageLayout = ImageLayout.Stretch;
                
                btnStartService.BackgroundImageLayout = ImageLayout.Stretch;
                btnEndService.BackgroundImageLayout = ImageLayout.Stretch;
                btnRefreshServices.BackgroundImageLayout = ImageLayout.Stretch;
                btnSaveSettings.BackgroundImageLayout = ImageLayout.Stretch;
                btnRunUpgradeScript.BackgroundImageLayout = ImageLayout.Stretch;
                btnExit.BackgroundImageLayout = ImageLayout.Stretch;
                btnSelectAll.BackgroundImageLayout = ImageLayout.Stretch; ;
                panel1.BackgroundImage = objbmp;
                panel1.BackgroundImageLayout = ImageLayout.Stretch;

                //lvServiceslist.BackgroundImage = objbmp;
                //lvServiceslist.BackgroundImageLayout = ImageLayout.Stretch;
                lvServiceslist.BackColor = Color.White;                
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("PaintGradient:  " + ex.Message.ToString() + ex.Source.ToString(), LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
            }
        }
        
        private void CreateHeadersAndFillListView()
        {
            ColumnHeader colHead;

            colHead = new ColumnHeader();
            colHead.Text = "Services";
            colHead.Width = 200;            
            this.lvServiceslist.Columns.Add(colHead);

            colHead = new ColumnHeader();
            colHead.Text = "Status";
            colHead .Width= 100;
            this.lvServiceslist.Columns.Add(colHead);


            lvServiceslist.LabelEdit = true;
            lvServiceslist.CheckBoxes = true;
            lvServiceslist.Font = new Font("Arial", 8, FontStyle.Bold);
            lvServiceslist.Scrollable = true;
            lvServiceslist.View = View.Details;
            lvServiceslist.Sorting = SortOrder.Ascending;
            lvServiceslist.MultiSelect = false;            
           
        }

        private void LoadServicesToListView(string[] strListarray)
        {
            ListViewItem itemListView = null;
            ListViewItem.ListViewSubItem subItemListView = null;
            lvServiceslist.Items.Clear();
            lvServiceslist.BeginUpdate();            
            for (int i = 0; i < strListarray.Length; i++)
            {
                if (strListarray[i] != null && strListarray[i] != string.Empty)
                {
                    itemListView = new ListViewItem();
                    itemListView.Text = strListarray[i];

                    subItemListView = new ListViewItem.ListViewSubItem();
                    subItemListView.Text = "";
                    itemListView.SubItems.Add(subItemListView);
                    this.lvServiceslist.Items.Add(itemListView);
                }
            }
            lvServiceslist.EndUpdate();
        }
      


        private void GetInitialSettings()
        {

            try
            {
                //tSStatus.Text = "";

                lvServiceslist.BeginUpdate();
                CreateHeadersAndFillListView();

                Dictionary<string, string> objKeycCollections = ReadServicesSettings.GetKeys("appSettings");

                if (objKeycCollections != null)
                {
                    foreach (KeyValuePair<string, string> objKeys in objKeycCollections)
                        //if (objKeys.Key.ToString() == "ServicesList")
                        //{
                        //    {
                        //        strListarray = objKeys.Value.ToString().Split(',');
                        //        LoadServicesToListView(strListarray);                               
                        //    }
                        //}
                        if (objKeys.Key.ToString() == "RegistryPath")
                        {
                            strRegistryKeyPath = objKeys.Value.ToString();
                            if (!string.IsNullOrEmpty(strRegistryKeyPath))
                            {
                                EnterpriseConfigRegistryEntities.RegistryKeyValue = strRegistryKeyPath;
                            }

                        }
                        else if (objKeys.Key.ToString() == "NetLoggerPath")
                        {
                            strRegistryKeyPath = objKeys.Value.ToString();
                            if (!string.IsNullOrEmpty(strRegistryKeyPath))
                            {
                                EnterpriseConfigRegistryEntities.NetLoggerRegKeyValue = strRegistryKeyPath;
                            }
                        }
                        else if (objKeys.Key.ToString() == "UpgradeScript")
                        {
                            strScriptPath = objKeys.Value.ToString();
                        }
                        else if (objKeys.Key.ToString() == "VisibleUpgradeScript")
                        {
                            strUpgradeVisible = objKeys.Value.ToString();
                        }

                }

                else
                {
                    MessageBox.Show("Config file not found.", UIConstants.strBMCConfig, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
               

            }
            catch (ConfigurationException confiex)
            {
                LogManager.WriteLog("GetInitialSettings" + confiex.Message.ToString() + confiex.Source.ToString(), LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(confiex);
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("GetInitialSettings" + ex.Message.ToString() + ex.Source.ToString(), LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
            }
 			finally
            {
                lvServiceslist.EndUpdate();
            }
               
        }
        
        private void GetServiceStatusToListView()
        {
            BMCMonitoring objBMCMonitoring = new BMCMonitoring();
            DataTable dtServicesStatus = new DataTable();
            StringBuilder strServicelist = new StringBuilder();           
            try
            {
                lvServiceslist.BeginUpdate();

                for (int i = 0; i < lvServiceslist.Items.Count; i++)
                {                                     
                    strServicelist.Append(lvServiceslist.Items[i].Text + ",");
                }
                dtServicesStatus = objBMCMonitoring.GetServiceStatus(strServicelist.ToString(), BMCMonitoring.ServiceTypes.All);
                Application.DoEvents();
                if (dtServicesStatus.Rows.Count > 0)
                {
                    for (int j = 0; j < dtServicesStatus.Columns.Count; j++)
                    {
                        for (int i = 0; i < dtServicesStatus.Rows.Count; i++)
                        {
                            if (j != dtServicesStatus.Columns.Count - 1)
                            {
                                if (dtServicesStatus.Rows[i][j].ToString() == lvServiceslist.Items[i].Text.ToString() && dtServicesStatus.Rows[i][j + 1].ToString() == "Stopped")
                                {   
                                    lvServiceslist.Items[i].ForeColor = Color.Red;
                                    lvServiceslist.Items[i].SubItems[1].Text = "Stopped";                                    
                                }
                                else if (dtServicesStatus.Rows[i][j].ToString() == lvServiceslist.Items[i].Text.ToString() && dtServicesStatus.Rows[i][j + 1].ToString() == "Running")
                                {                                   
                                    lvServiceslist.Items[i].ForeColor = Color.Blue;                                   
                                    lvServiceslist.Items[i].SubItems[1].Text = "Running";                                    

                                }
                                else if (dtServicesStatus.Rows[i][j].ToString() == lvServiceslist.Items[i].Text.ToString() && dtServicesStatus.Rows[i][j + 1].ToString() == "Pending")
                                {   
                                    lvServiceslist.Items[i].ForeColor = Color.MediumSeaGreen;                                   
                                    lvServiceslist.Items[i].SubItems[1].Text = "Pending";                                    
                                }
                                else if (dtServicesStatus.Rows[i][j].ToString() == lvServiceslist.Items[i].Text.ToString() && dtServicesStatus.Rows[i][j + 1].ToString() == "Service not found")
                                {
                                    lvServiceslist.Items[i].ForeColor = Color.Green;
                                    lvServiceslist.Items[i].SubItems[1].Text = "Service not found";                                    
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                }
               
            }
            catch (IndexOutOfRangeException iex)
            {
                LogManager.WriteLog("GetServiceStatus" + iex.Message.ToString() + iex.Source.ToString(), LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(iex);
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("GetServiceStatus" + ex.Message.ToString() + ex.Source.ToString(), LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
            }
            finally
            {
                lvServiceslist.EndUpdate();
            }
        }
        
        private string GetServiceStatus(string strServiceName)
        {
            BMCMonitoring objBMCMonitoring = new BMCMonitoring();
            DataTable dtServicesStatus = new DataTable();
            StringBuilder strServicelist = new StringBuilder();
            try
            {
                dtServicesStatus = objBMCMonitoring.GetServiceStatus(strServiceName, BMCMonitoring.ServiceTypes.All);
                Application.DoEvents();
                if (dtServicesStatus.Rows.Count > 0)
                {
                    for (int j = 0; j < dtServicesStatus.Columns.Count; j++)
                    {
                        for (int i = 0; i < dtServicesStatus.Rows.Count; i++)
                        {
                            if (j != dtServicesStatus.Columns.Count - 1)
                            {
                                if (dtServicesStatus.Rows[i][j].ToString() == strServiceName && dtServicesStatus.Rows[i][j + 1].ToString() == "Stopped")
                                {
                                    strServiceStatus = "STOP";
                                }
                                else if (dtServicesStatus.Rows[i][j].ToString() == strServiceName && dtServicesStatus.Rows[i][j + 1].ToString() == "Running")
                                {
                                    strServiceStatus = "RUN";

                                }
                                else if (dtServicesStatus.Rows[i][j].ToString() == strServiceName && dtServicesStatus.Rows[i][j + 1].ToString() == "Pending")
                                {
                                    strServiceStatus = "PEND";
                                }
                                else if (dtServicesStatus.Rows[i][j].ToString() == strServiceName && dtServicesStatus.Rows[i][j + 1].ToString() == "Service not found")
                                {
                                    strServiceStatus = "NOSERVICE";
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                }

            }
            catch (IndexOutOfRangeException iex)
            {
                LogManager.WriteLog("GetServiceStatus" + iex.Message.ToString() + iex.Source.ToString(), LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(iex);
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("GetServiceStatus" + ex.Message.ToString() + ex.Source.ToString(), LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
            }
            finally
            {
                dtServicesStatus.Dispose();
            }
            return strServiceStatus;
        }
        
        private void btnStartService_Click(object sender, EventArgs e)
        {
            bool bServiceStatus = false;
            int i = 0;
            bool bChecked = false;             
            ProgressBarServices.Value = 10;

            try
            {
                for (i = 0; i < lvServiceslist.Items.Count; i++)
                {
                    if (lvServiceslist.Items[i].Checked == true)
                    {                        
                        bChecked = true; 

                        bServiceStatus = StartService(lvServiceslist.Items[i].Text.ToString().Trim());
                        Application.DoEvents();
                        ProgressBarServices.Value = 20;
                        if (bServiceStatus)
                        {                            
                            lvServiceslist.BeginUpdate();
                            lvServiceslist.Items[i].ForeColor = Color.Blue;
                            //lvServiceslist.Items[i].Checked = Convert.ToBoolean(CheckState.Checked);
                            lvServiceslist.Items[i].SubItems[1].Text = "Running";
                            ProgressBarServices.Value = 100;
                            //tSStatus.Text = "Process started Successfully";
                            
                            lvServiceslist.EndUpdate();
                            MessageBox.Show("Service - " + lvServiceslist.Items[i].Text + " Started Successfully.", UIConstants.strBMCConfig, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ProgressBarServices.Value = 0;
                        }
                        else
                        {
                            MessageBox.Show("Starting Service - " + lvServiceslist.Items[i].Text + " failed.", UIConstants.strBMCConfig, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            lvServiceslist.BeginUpdate();
                            lvServiceslist.Items[i].ForeColor = Color.Red;
                            //lvServiceslist.Items[i].Checked = Convert.ToBoolean(CheckState.Unchecked);
                            lvServiceslist.Items[i].SubItems[1].Text = "Start Failed";
                            //tSStatus.Text = "Process failed to start";
                            ProgressBarServices.Value = 0;
                            lvServiceslist.EndUpdate();
                        }

                        lvServiceslist.Items[i].Checked = Convert.ToBoolean(CheckState.Unchecked);
                    }
                }

                if (!bChecked)
                {
                    MessageBox.Show("Please Select A Service.", UIConstants.strBMCConfig, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (IndexOutOfRangeException iex)
            {
                MessageBox.Show("Starting Service - " + lvServiceslist.Items[i].Text + " failed.", UIConstants.strBMCConfig, MessageBoxButtons.OK, MessageBoxIcon.Error);
                lvServiceslist.Items[i].Checked = Convert.ToBoolean(CheckState.Unchecked);
                LogManager.WriteLog("btnStartService_Click" + iex.Message.ToString() + iex.Source.ToString(), LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(iex);
            }

            catch (Exception ex)
            {
                MessageBox.Show("Starting Service - " + lvServiceslist.Items[i].Text + " failed.", UIConstants.strBMCConfig, MessageBoxButtons.OK, MessageBoxIcon.Error);
                lvServiceslist.Items[i].Checked = Convert.ToBoolean(CheckState.Unchecked);
                LogManager.WriteLog("btnStartService_Click" + ex.Message.ToString() + ex.Source.ToString(), LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
            }
            finally
            {
                ProgressBarServices.Value = 0;
            }

        }
        
        private bool StartService(string strServicename)
        {
            try
            {
                BMCMonitoring objBMCMonitoring = new BMCMonitoring();
                return objBMCMonitoring.StartService(strServicename);
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("StartService" + ex.Message.ToString() + ex.Source.ToString(), LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
                return false;
            }
        }
        
         private void btnEndService_Click(object sender, EventArgs e)
        {
            bool bServiceStatus = false;
            int i = 0;
            bool bChecked = false;             
            ProgressBarServices.Value = 10;
            try
            {                
                for (i = 0; i < lvServiceslist.Items.Count; i++)
                {
                    if (lvServiceslist.Items[i].Checked == true)
                    {
                        bChecked = true;
                        bServiceStatus = EndService(lvServiceslist.Items[i].Text);
                        Application.DoEvents();
                        ProgressBarServices.Value = 20;

                        if (bServiceStatus)
                        {                            
                            lvServiceslist.BeginUpdate();
                            lvServiceslist.Items[i].Checked = Convert.ToBoolean(CheckState.Unchecked);
                            lvServiceslist.Items[i].ForeColor = Color.Red;
                            lvServiceslist.Items[i].SubItems[1].Text = "Stopped"; 
                            //tSStatus.Text = "Process Stopped successfully";
                            lvServiceslist.EndUpdate();
                            ProgressBarServices.Value = 100;
                            MessageBox.Show("Service - " + lvServiceslist.Items[i].Text + " Stopped Succesfully.", UIConstants.strBMCConfig, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ProgressBarServices.Value = 0;
                            
                        }
                        else
                        {
                            MessageBox.Show("Ending Service - " + lvServiceslist.Items[i].Text + " Failed.", UIConstants.strBMCConfig, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            lvServiceslist.BeginUpdate();
                            lvServiceslist.Items[i].Checked = Convert.ToBoolean(CheckState.Unchecked);
                            lvServiceslist.Items[i].ForeColor = Color.MediumSeaGreen;
                            lvServiceslist.Items[i].SubItems[1].Text = "Pending Stopped"; 
                            //tSStatus.Text = "Pending process...please try again later";
                            ProgressBarServices.Value = 0;
                            lvServiceslist.EndUpdate();
                        }
                    }
                }

                if (!bChecked)
                {
                    MessageBox.Show("Please select a service.", UIConstants.strBMCConfig, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Ending Service - " + lvServiceslist.Items[i].Text + " Failed.", UIConstants.strBMCConfig, MessageBoxButtons.OK, MessageBoxIcon.Error); 
                LogManager.WriteLog("btnEndService" + ex.Message.ToString() + ex.Source.ToString(), LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
            }
            finally
            {
                ProgressBarServices.Value = 0;
            }
        }
        
        private bool EndService(string strServicename)
        {
            try
            {
                BMCMonitoring objBMCMonitoring = new BMCMonitoring();
                return objBMCMonitoring.EndService(strServicename);                
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("EndService" + ex.Message.ToString() + ex.Source.ToString(), LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
                return false;
            }
        }
        
   
        private void frmBMCEnterpriseConfig_Load(object sender, EventArgs e)
        {
            try
            {               
                //get the settings from the config file      
                GetInitialSettings();               

                //Retrieve the server settings for Enterprise,Ticketing and CMP
                GetSettings();                          

                //Call refresh to save Enterprise connection when the registry is not having data.
                RefreshControls();

                //Get all the service settings
                GetServiceStatusToListView();                

                //Check the visibility of the button upgrade script based on the config setting
                if (strUpgradeVisible.ToUpper() == "FALSE")
                {
                    btnRunUpgradeScript.Enabled = false;
                }
                if (blsRegulatoryEnabled == true && sRegulatoryType.Length > 0)
                {
                    //Get BAS Settings.
                    GetBASSetting();

                    GetLGEServerSetting();
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("frmBMCEnterpriseConfig_Load" + ex.Message.ToString() + ex.Source.ToString(), LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
            }
          
        }       

        private void RefreshControls()
        {
           // if ((txtEnterpriseServer.Text == string.Empty) && (txtEnterpriseUsername.Text == string.Empty) && (txtEnterprisePassword.Text == string.Empty))
            if ((txtenterpriseServer.Text == string.Empty) || (txtenterpriseUsername.Text == string.Empty) || (txtenterprisePassword.Text == string.Empty))
            {
                
                gpMeterAnalysisSetup.Enabled = false;
                gpLGESetup.Enabled = false;                
                gpServiceSetup.Enabled = false;
                //gpSystemSettings.Enabled = false;
                btnSaveSettings.Enabled = false;
                if (strUpgradeVisible.ToUpper() == "FALSE")
                {
                    btnRunUpgradeScript.Enabled = false;
                }
            }
            else
            {                
                gpMeterAnalysisSetup.Enabled = true;
                gpLGESetup.Enabled = true;                
                gpServiceSetup.Enabled = true;
                //gpSystemSettings.Enabled = true;
                btnSaveSettings.Enabled = true;
                if (strUpgradeVisible.ToUpper() == "TRUE")
                {
                    btnRunUpgradeScript.Enabled = true;
                }
                if (chkEnableLGE.Checked==true)
                {
                    gpLGECredentials.Enabled = true;
                }
            }
        }                
        
        private void GetSettings()
        {
            string strKeyvalue = string.Empty;            
            string strWebUrl = string.Empty;
            string strTicketingLocCode = string.Empty;
            DataTable dtSettings = null;
            try
            {

                strConnection = RegistrySettings.EnterpriseConnectionString();

                if (!String.IsNullOrEmpty(strConnection))
                {
                    Dictionary<string, string> ServerEntries = Credentials.RetrieveServerDetails(strConnection);                   
                    GetEnterpriseServerSettings(ServerEntries);
                    
                    string strMeterAnalysisConnectionString = DBSettings.MeterAnalysisConnectionString(strConnection);
                    if (string.IsNullOrEmpty(strMeterAnalysisConnectionString))
                    {
                        chkUseEnterpriseConnection.Checked = false;
                    }
                    else
                    {
                        Dictionary<string, string> MeterAnalysisServerEntries = Credentials.RetrieveServerDetails(strMeterAnalysisConnectionString);
                        GetMeterAnalysisServerSettings(MeterAnalysisServerEntries);
                    }
                    dtSettings = new DataTable();
                    dtSettings = DBSettings.GetInitialSettings();
                    if (dtSettings != null)
                    {
                        if (dtSettings.Rows.Count > 0)
                        {
                            blsRegulatoryEnabled = Convert.ToBoolean(dtSettings.Rows[0]["IsRegulatoryEnabled"].ToString());
                            sRegulatoryType = dtSettings.Rows[0]["RegulatoryType"].ToString();

                        }
                    }

                    Dictionary<string, string> EnterpriseRegistryEntries = RegistrySettings.GetRegistryEntries(EnterpriseConfigRegistryEntities.RegistryKeyValue);
                    foreach (KeyValuePair<string, string> KVPServer in EnterpriseRegistryEntries)
                    {
                        strKeyvalue = KVPServer.Key.Substring(KVPServer.Key.LastIndexOf('\\') + 1);


                        if (strKeyvalue.ToLower() == "enterprisekey")
                        {
                            if (!string.IsNullOrEmpty(KVPServer.Value.ToString()))
                            {
                                // chkEnableLGE.Checked = Convert.ToBoolean(int.Parse(KVPServer.Value));
                            }
                        }

                        if (strKeyvalue.ToLower() == "sqlconnect")
                        {
                            if (!string.IsNullOrEmpty(KVPServer.Value.ToString()))
                            {
                                //strWebUrl = KVPServer.Value.ToString();
                                //txtBASweburl.Text = strWebUrl;
                                //strWebUrl = strWebUrl.Substring(strWebUrl.IndexOf("//") + 2);
                                //txtEnterpriseweburl.Text = strWebUrl.Substring(0, strWebUrl.IndexOf("/"));
                            }

                        }

                    }                  
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("GetSettings" + ex.Message.ToString() + ex.Source.ToString(), LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
            }
        }
        
        private void GetEnterpriseServerSettings(Dictionary<string, string> ServerEntries)
        {
            try
            {
                if (ServerEntries != null)
                {
                    foreach (KeyValuePair<string, string> objKeyValue in ServerEntries)
                    {
                        if (objKeyValue.Key.ToUpper() == "SERVER")
                        {
                            txtenterpriseServer.Text = objKeyValue.Value;
                        }
                        else if (objKeyValue.Key.ToUpper() == "UID")
                        {
                            txtenterpriseUsername.Text = objKeyValue.Value;
                        }
                        else if (objKeyValue.Key.ToUpper() == "PASSWORD")
                        {
                            txtenterprisePassword.Text = objKeyValue.Value;
                        }
                        else if (objKeyValue.Key.ToUpper() == "DATABASE")
                        {
                            lblEnterpriseDBname.Text = objKeyValue.Value;
                        }
                        else if (objKeyValue.Key.ToUpper() == "TIMEOUT")
                        {
                            txtEnterpriseTimeOut.Text = objKeyValue.Value;
                        }
                        else if (objKeyValue.Key.ToUpper() == "INSTANCE")
                        {
                            txtenterpriseInstance.Text = objKeyValue.Value;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("GetEnterpriseServerSettings" + ex.Message.ToString() + ex.Source.ToString(), LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
            }
        }

        private void GetMeterAnalysisServerSettings(Dictionary<string, string> MeterAnalysisServerEntries)
        {
            try
            {
                if (MeterAnalysisServerEntries != null)
                {
                    foreach (KeyValuePair<string, string> objKeyValue in MeterAnalysisServerEntries)
                    {
                        if (objKeyValue.Key.ToUpper() == "SERVER")
                        {
                            txtLGEServer.Text = objKeyValue.Value;
                        }
                        else if (objKeyValue.Key.ToUpper() == "UID")
                        {
                            txtLGEUsername.Text = objKeyValue.Value;
                        }
                        else if (objKeyValue.Key.ToUpper() == "PASSWORD")
                        {
                            txtLGEPassword.Text = objKeyValue.Value;
                        }
                        else if (objKeyValue.Key.ToUpper() == "DATABASE")
                        {
                            //lblCMPDB.Text = objKeyValue.Value;
                        }
                        else if (objKeyValue.Key.ToUpper() == "TIMEOUT")
                        {
                            txtLGEtimeout.Text = objKeyValue.Value;
                        }
                        else if (objKeyValue.Key.ToUpper() == "INSTANCE")
                        {
                            txtLGEInstance.Text = objKeyValue.Value;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("GetMeterAnalysisServerSettings" + ex.Message.ToString() + ex.Source.ToString(), LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
            }
        }     
                       
        private void btnTestURL_Click(object sender, EventArgs e)
        {            
            try
            {
                Cursor = Cursors.WaitCursor;

                CheckWebService(txtBASweburl.Text);

            }

            catch (Exception ex)
            {                
                ExceptionManager.Publish(ex);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private bool ValidateURL(string sUrl)
        {
            bool bReturn = false;            
            string sWebExtension = string.Empty;
            Regex objRegexUrlvalidate = new Regex("^(http|ftp)://(www\\.)?.+\\.(com|net|org|asmx)$");
            MatchCollection objMatchCollect;

            if (sUrl.Trim().Length < 0 || sUrl.Trim().Length == 0)
            {
                MessageBox.Show("You must supply a valid Web server/URL name.", UIConstants.strBMCConfig, MessageBoxButtons.OK, MessageBoxIcon.Information);
                //tSStatus.Text = "You must supply a valid server name";
                errValidate.SetError(txtBASweburl, "You must supply a valid Web server/URL name");
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
                    MessageBox.Show("Enterprise Web Url not in correct format.", UIConstants.strBMCConfig, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //tSStatus.Text = "Enterprise Web Url not in correct format.";
                    errValidate.SetError(txtBASweburl, "You must supply a valid Url");
                    bReturn = false;
                }
            }
            return bReturn;
        }
              
        private int CheckWebService(string strURL)
        {  
            int iReceiveValue=0;  
           
            try
            {
                   if (ValidateURL(strURL))
                    {
                        iReceiveValue = ReadServicesSettings.TestWebUrl(strUrlvalidate);
                        if (iReceiveValue > 0)
                        {
                            MessageBox.Show("Enterprise Web Service Test Successfull.", UIConstants.strBMCConfig, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //tSStatus.Text = "Enterprise Web Service Test Successfull.";
                            errValidate.Clear();
                        }
                        else
                        {
                            MessageBox.Show("Enterprise Web Service Test Failed.", UIConstants.strBMCConfig, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            //tSStatus.Text = "Enterprise Web Service Test Failed.";
                        }
                    }
                    else
                    {
                        iReceiveValue = 0;
                    }                
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("CheckWebService" + ex.Message.ToString() + ex.Source.ToString(), LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
            }
            return iReceiveValue;
    }
               
        private void btnExit_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("btnExit_Click" + ex.Message.ToString() + ex.Source.ToString(), LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
            }
        }
            
        private bool ValidateText(TextBox tBox, string Message)
        {
            bool bStatus = true;
            if (tBox.Text.Length == 0)
            {
                //tSStatus.Text = "Please enter " + Message;
                errValidate.SetError(tBox, "Please enter " + Message);
                bStatus = false;
            }
            else
                errValidate.SetError(tBox, "");
            return bStatus;
        }
       
        private void btnEnterpriseDBRestore_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Do you want to really restore the database?", UIConstants.strBMCConfig, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Cursor = Cursors.WaitCursor;
                    //frmSQLRestore frmRestoreDB = new frmSQLRestore("Enterprise");
                    //frmRestoreDB.ShowDialog();
                }
                
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("btnEnterpriseDBRestore_Click" + ex.Message.ToString() + ex.Source.ToString(), LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void GetBASSetting()
        {
            try
            {
                //Update the app.config also.
                XmlDocument xDom;
                XmlNode myNode;
                xDom = new XmlDocument();
                xDom.Load("C:\\Program Files\\Bally Technologies\\Enterprise Server\\Bally Audit System\\WindowsService\\BMC.BAS.ExportImportService.exe.config");
                myNode = xDom.DocumentElement.SelectSingleNode("/configuration/applicationSettings/BMC.BAS.ExportImportService.Properties.Settings/setting");
                if (myNode != null)
                {
                    foreach (XmlNode oNode in myNode.ChildNodes)
                    {
                        if (oNode.Name == "value")
                        {
                            txtbasWebService.Text = oNode.InnerText;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //addStatus("Error Getting Report Server Setting: " + ex.Message);
            }
        }

        //Save the report server setting
        private void GetLGEServerSetting()
        {
            string SQLConnect;

            try
            {
                SQLConnect = MakeSQLConnectionString();
                SqlConnection sqlConn = new SqlConnection(SQLConnect);

                SqlCommand sqlCMD = new SqlCommand();
                sqlCMD.Connection = sqlConn;
                sqlCMD.CommandText = "SELECT Setting_Value FROM Setting WHERE Setting_Name = 'LGEConnectionDetails'";
                sqlCMD.CommandType = CommandType.Text;

                sqlConn.Open();

                string strConnString = sqlCMD.ExecuteScalar().ToString();

                String[] strArray = strConnString.Split(';');

                foreach (string strArr in strArray)
                {
                    string[] strValue = strArr.Split('=');

                    if (strValue.Length > 1)
                    {
                        if (strValue[0].ToString().Trim() == "server")
                        {
                            txtLGEServer.Text = strValue[1].ToString().Trim();
                        }
                        if (strValue[0].ToString().Trim() == "database")
                        {
                            txtLGEdatabase.Text = strValue[1].ToString().Trim();
                        }
                        if (strValue[0].ToString().Trim() == "username")
                        {
                            txtLGEUser.Text = strValue[1].ToString().Trim();
                        }
                        if (strValue[0].ToString().Trim() == "password")
                        {
                            txtLGEPwd.Text = strValue[1].ToString().Trim();
                        }
                    }
                }

                sqlConn.Close();

            }
            catch (Exception ex)
            {
                addStatus("Error Getting LGE Server Settings: " + ex.Message);
            }
        }
        
        private bool AddServerDetails(string Server, string UserName, string Password, string DataBase, string ConnectionTimeout)
        {
            bool bResult = false;
            Dictionary<string, string> objServerDetails = new Dictionary<string, string>();
            try
            {
                objServerDetails.Add("SERVER", Server);
                objServerDetails.Add("UID", UserName);
                objServerDetails.Add("PWD", Password);
                objServerDetails.Add("DATABASE", DataBase);
                objServerDetails.Add("CONNECTION TIMEOUT", ConnectionTimeout);

                ReturnConnectionString = Credentials.MakeConnectionString(objServerDetails);    
                if (!String.IsNullOrEmpty(ReturnConnectionString))
                {
                    bResult = Credentials.TestConnectionDB(ReturnConnectionString);
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("AddServerDetails" + ex.Message.ToString() + ex.Source.ToString(), LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
            }
            return bResult;
        }

        private void btnEnterpriseTestConnection_Click(object sender, EventArgs e)
        {
            bool bTestConnection = false;
            try
            {
                Cursor = Cursors.WaitCursor;

                if (ValidateText(txtenterpriseServer, "Server"))
                {
                    if (ValidateText(txtenterpriseUsername, "UserName"))
                    {
                        if (ValidateText(txtenterprisePassword, "Password"))
                        {
                            if (ValidateText(txtEnterpriseTimeOut, "Connection Timeout"))
                            {
                                bTestConnection = TestConnection(txtenterpriseServer.Text, txtenterpriseUsername.Text, txtenterprisePassword.Text, txtEnterpriseTimeOut.Text, txtenterpriseInstance.Text, 'E');
                            }
                        }
                    }
                }
                if (bTestConnection == true)
                {
                    //tSStatus.Text = "Connection to Enterprise Database Successfull.";
                    MessageBox.Show("Connection to Enterprise Database Successfull.", UIConstants.strBMCConfig, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    //tSStatus.Text = "Connection to Enterprise Database Failed.";
                    MessageBox.Show("Connection to Enterprise Database Failed.", UIConstants.strBMCConfig, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("btnEnterpriseTestConnection_Click" + ex.Message.ToString() + ex.Source.ToString(), LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }       

        private bool TestConnection(string strServer,string strUsername,string strPassword,string strTimeOut,string strInstance,char chDatabase)
        {
            bool bTestConnection = false;
            string strServerName = string.Empty;
            try
            {
                if (strInstance.Trim().Length > 0)
                {
                    strServer = strServer + "\\" + strInstance;
                }
                //Test DB Connection for Enterprise.
                if (chDatabase=='E')
                {
                    bTestConnection = AddServerDetails(strServer, strUsername, strPassword, UIConstants.strEnterpriseDBName, strTimeOut);
                }
                else if (chDatabase == 'M')
                {
                    bTestConnection = AddServerDetails(strServer, strUsername, strPassword, UIConstants.strMeterAnalysisDBName, strTimeOut);
                }
                else if (chDatabase == 'L')
                {
                    bTestConnection = AddServerDetails(strServer, strUsername, strPassword, txtLGEDB.Text.Trim(), strTimeOut);
                }  
                
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("EnterpriseTestConnection" + ex.Message.ToString() + ex.Source.ToString(), LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
            }
            return bTestConnection;
        }

       
        private void chkUseEnterpriseConnection_CheckedChanged(object sender, EventArgs e)
        {

            //Use Enterprise credentials if checked.
            if (chkUseEnterpriseConnection.Checked)
            {
               
            }
            else
            {
                //Enter new credentials               
            }
        }

        private void chkUseEnterpriseConnect_CheckedChanged(object sender, EventArgs e)
        {
            //Use Enterprise credentials if checked.
            if (chkUseEnterpriseConnection.Checked)
            {
                txtLGEServer.Text = txtenterpriseServer.Text;
                txtLGEUsername.Text = txtenterpriseUsername.Text;
                txtLGEPassword.Text = txtenterprisePassword.Text;
                txtLGEtimeout.Text = txtEnterpriseTimeOut.Text;
            }
            else
            {
                //Enter new credentials
                txtLGEServer.Text = string.Empty;
                txtLGEUsername.Text = string.Empty;
                txtLGEPassword.Text = string.Empty;
                txtLGEtimeout.Text = string.Empty;
            }
        }

        private void btnSaveSettings_Click(object sender, EventArgs e)
        {
            Regex objRegexUrlvalidate = new Regex("^(http|ftp)://(www\\.)?.+\\.(com|net|org|asmx)$");
            MatchCollection objMatchCollect;
            int iEnableDHCP = 0;
            int iEnableEncrypt = 0;
            string strSlotLanIPAddress = string.Empty;
            string strEncryptEnterpriseConnection = string.Empty;
            string strEncryptEnterpriseConnectionHex = string.Empty;
            bool bTestEnterpriseConnection = false;
            bool bTestTicketingConnection = false;
            bool bTestLGEConnection = false;
            bool bInsertSetting = false;
            bool bSetLocCode = false;
            string strIPPath = string.Empty;
            Dictionary<string, string> dictSetregistryentries;
            Dictionary<string, string> dictSetNetLoggerRegistryEntry;
            try
            {
                if (MessageBox.Show("Do you want to save all the settings?", UIConstants.strBMCConfig, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Cursor = Cursors.WaitCursor;
                    //sStrip.Text = "Processing....";
                    //tSStatus.Text = "Processing...";
                    dictSetregistryentries = new Dictionary<string, string>();
                    dictSetNetLoggerRegistryEntry = new Dictionary<string, string>();
                    //Enterprise Server save 
                    if (ValidateText(txtenterpriseServer, "Server"))
                    {
                        if (ValidateText(txtenterpriseUsername, "UserName"))
                        {
                            if (ValidateText(txtenterprisePassword, "Password"))
                            {
                                if (ValidateText(txtEnterpriseTimeOut, "Connection Timeout"))
                                {
                                    bTestEnterpriseConnection = TestConnection(txtenterpriseServer.Text, txtenterpriseUsername.Text, txtenterprisePassword.Text, txtEnterpriseTimeOut.Text, txtenterpriseInstance.Text,'E');
                                }
                            }
                        }
                    }                   
                    if (bTestEnterpriseConnection == true)
                    {
                        if (!string.IsNullOrEmpty(ReturnConnectionString))                            
                        EnterpriseConfigRegistryEntities.EnterpriseConnectionString = ReturnConnectionString;                        
                        strEncryptEnterpriseConnection = RegistrySettings.EncryptEnterpriseConnection();
                        if (!string.IsNullOrEmpty(strEncryptEnterpriseConnection))
                        {  
                            dictSetregistryentries.Add(UIConstants.strSQLConnect, strEncryptEnterpriseConnection+ "+" + "REG_SZ");
                           
                        }                        
                    }
                    else
                    {
                        MessageBox.Show("Enterprise Connection Failed.", UIConstants.strBMCConfig, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    Application.DoEvents();
                    bTestTicketingConnection =TestConnection(txtenterpriseServer.Text, txtenterpriseUsername.Text, txtenterprisePassword.Text, txtEnterpriseTimeOut.Text, txtenterpriseInstance.Text,'M');
                              
                    if (bTestTicketingConnection == true)
                    {
                        if (!string.IsNullOrEmpty(ReturnConnectionString))
                        {
                            EnterpriseConfigRegistryEntities.MeterAnalysisConnectionString = ReturnConnectionString;
                        }                        
                    }
                    else
                    {
                        MessageBox.Show("Ticketing Connection Failed.", UIConstants.strBMCConfig, MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                    Application.DoEvents();
                    //LGE  server save
                    if (ValidateText(txtLGEServer, "Server"))
                    {
                        if (ValidateText(txtLGEUsername, "UserName"))
                        {
                            if (ValidateText(txtLGEPassword, "Password"))
                            {
                                if (ValidateText(txtLGEtimeout, "Connection Timeout"))
                                {
                                    bTestLGEConnection = TestConnection(txtLGEServer.Text, txtLGEUsername.Text, txtLGEPassword.Text, txtLGEtimeout.Text, txtLGEInstance.Text, 'C');
                                }
                            }
                        }
                    }

                    if (bTestLGEConnection == true)
                    {
                        if (!string.IsNullOrEmpty(ReturnConnectionString))
                        {
                            EnterpriseConfigRegistryEntities.LGEConnectionString = ReturnConnectionString;
                        }
                        if (!string.IsNullOrEmpty(EnterpriseConfigRegistryEntities.LGEConnectionString))
                        {
                            bInsertSetting = DBSettings.InsertSettings(UIConstants.strLGEConnection, EnterpriseConfigRegistryEntities.LGEConnectionString);
                            if (bInsertSetting == false)
                            {
                                MessageBox.Show("Error in saving LGE details.", UIConstants.strBMCConfig, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("CMP Connection Failed.", UIConstants.strBMCConfig, MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }

                    //Encrypt enable settings                    
                    if (chkEnableLGE.Checked == false)
                        if (MessageBox.Show("Do you want to check Enable encrypt ?", UIConstants.strBMCConfig, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            return;
                    if (chkEnableLGE.Checked == true)
                        iEnableEncrypt = 1;
                    else
                        iEnableEncrypt = 0;

                    //dictSetregistryentries.Add(UIConstants.strEncryptEnable, iEnableEncrypt.ToString() + "+" + "REG_DWORD");
                    Application.DoEvents();
                  

                    //Save all Registry Settings under cash master
                    RegistrySettings.SetRegistryEntries(dictSetregistryentries,EnterpriseConfigRegistryEntities.RegistryKeyValue);
                    Application.DoEvents();

                    //Save all Registry Settings under NetLogger                    
                    RegistrySettings.SetRegistryEntries(dictSetNetLoggerRegistryEntry, EnterpriseConfigRegistryEntities.NetLoggerRegKeyValue);
                    Application.DoEvents();
                  
                    Application.DoEvents();
                    MessageBox.Show("Saved Settings Successfully.", UIConstants.strBMCConfig, MessageBoxButtons.OK,MessageBoxIcon.Information);
                    //sStrip.Text = "Done.";                    
                    //tSStatus.Text = "Done";                   
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Save Button Settings" + ex.Message.ToString() + ex.Source.ToString(), LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }
                   
        public bool ValidateIP(string strCheckIP )
        {    string[] strIParray;        
            bool bReturn = false;
            int iCheckValue = 0;

            try
            {
                if (!string.IsNullOrEmpty(strCheckIP))
                {
                    if (strCheckIP.IndexOf(".", 0) > 0)
                    {
                        strIParray = strCheckIP.ToString().Split('.');
                       
                        if (strIParray.LongLength==4)
                        {
                            for (int i = 0; i <= strIParray.Length - 1; i++)
                            {
                                if (strIParray[i] != null && strIParray[i] != string.Empty)
                                {
                                    iCheckValue = int.Parse(strIParray[i].ToString());
                                    if (iCheckValue > 0)
                                    {
                                        if (iCheckValue <= 255 && iCheckValue >= 0)
                                        {
                                            bReturn = true;
                                        }
                                        else
                                        {
                                            bReturn = false;
                                            break;

                                        }
                                    }
                                }
                                else
                                {
                                    bReturn = false;
                                }
                            }
                        }

                    }
                    else
                    {
                        bReturn = false;
                    }
                }
                else
                {
                    bReturn = false;
                }
            }
            catch (IndexOutOfRangeException iex)
            {
                LogManager.WriteLog("ValidateIP" + iex.Message.ToString() + iex.Source.ToString(), LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(iex);
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("ValidateIP" + ex.Message.ToString() + ex.Source.ToString(), LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
            }
            return bReturn;
        }       

        private void btnTicketDBRestore_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Do you want to really restore the database?", UIConstants.strBMCConfig, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Cursor = Cursors.WaitCursor;
                    //frmSQLRestore frmRestoreDB = new frmSQLRestore("Ticketing");
                    //frmRestoreDB.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("btnTicketDBRestore_Click" + ex.Message.ToString() + ex.Source.ToString(), LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
            }
        }        

        private void cmbSlotLan_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (!((e.KeyCode >= Keys.NumPad0 && e.KeyCode <= Keys.NumPad9) ||
                    ((e.KeyCode >= Keys.D0 && e.KeyCode <= Keys.D9) && (!e.Shift)) ||
                    e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete || e.KeyCode ==
                    Keys.Alt || e.KeyCode == Keys.Left || e.KeyCode == Keys.Right ||
                    e.KeyCode == Keys.Shift || e.KeyCode == Keys.Home || e.KeyCode ==
                    Keys.End || e.KeyCode == Keys.Decimal || e.KeyCode == Keys.Enter))

                    e.SuppressKeyPress = true;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("cmbSlotLan_KeyDown" + ex.Message.ToString() + ex.Source.ToString(), LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
            }

        }
       
        private void frmBMCEnterpriseConfig_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {     
                    Application.Exit();                   
               
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("FormClosed" + ex.Message.ToString() + ex.Source.ToString(), LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
            }
        }

        private void frmBMCEnterpriseConfig_FormClosing(object sender, FormClosingEventArgs e)
        {   
             DialogResult dr;
             try
             {
                 dr = MessageBox.Show("Are you sure you want to exit the application?", UIConstants.strBMCConfig, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                 if (dr.ToString() == "Yes")
                 {
                     e.Cancel = false;

                 }
                 else
                 {
                     e.Cancel = true;
                 }
             }
             catch (Exception ex)
             {
                 LogManager.WriteLog("FormClosing" + ex.Message.ToString() + ex.Source.ToString(), LogManager.enumLogLevel.Error);
                 ExceptionManager.Publish(ex);
             }

        }

        private void btnRunUpgradeScript_Click(object sender, EventArgs e)
        {
            string sArgToCmdShell=string.Empty;
            bool bTestConnection = false;
            try
            {
                Cursor = Cursors.WaitCursor;
                //if (ValidateText(txtEnterpriseServer, "Server"))
                //{
                //    if (ValidateText(txtEnterpriseUsername, "UserName"))
                //    {
                //        if (ValidateText(txtEnterprisePassword, "Password"))
                //        {
                //            if (ValidateText(txtEnterpriseTimeOut, "Connection Timeout"))
                //            {
                //                bTestConnection = TestConnection(txtEnterpriseServer.Text, txtEnterpriseUsername.Text, txtEnterprisePassword.Text, txtEnterpriseTimeOut.Text, txtEnterpriseInstance.Text, 'E');
                //            }
                //        }
                //    }
                //}                
                //if (bTestConnection == false)                
                //{
                //    //tSStatus.Text = "Connection to Enterprise Database Failed..Please fix it first and upgrade";
                //    MessageBox.Show("Connection to Enterprise Database Failed..Please fix it first and upgrade", UIConstants.strBMCConfig, MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    return;
                //}
                //if (ValidateText(txtticketserver, "Server"))
                //{
                //    if (ValidateText(txtticketusername, "UserName"))
                //    {
                //        if (ValidateText(txtticketPassword, "Password"))
                //        {
                //            if (ValidateText(txtticketTimeout, "Connection Timeout"))
                //            {
                //                bTestConnection = TestConnection(txtticketserver.Text, txtticketusername.Text, txtticketPassword.Text, txtticketTimeout.Text, txticketInstance.Text, 'T');
                //            }
                //        }
                //    }
                //}                
                //if (bTestConnection == false)
                //{
                //    //tSStatus.Text = "Connection to Ticketing Database Failed.Please fix it first and upgrade";
                //    MessageBox.Show("Connection to Ticketing Database Failed..Please fix it first and upgrade", UIConstants.strBMCConfig, MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    return;
                //}              

                //frmDeployScripts frmDeployScripts = new frmDeployScripts(strScriptPath);
                //frmDeployScripts.ShowDialog();
                 ConfigManager.SetConfigurationMode(ConfigManager.ConfigurationMode.AppConfig);
                 if (ConfigManager.Read("Startuppath") != null)
                 {
                     string startuppath = ConfigManager.Read("Startuppath");
                     ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo();
                     psi.FileName = startuppath + "\\Enterprise Server\\DeployScripts.exe";
                     if (!File.Exists(psi.FileName))
                     {
                         MessageBox.Show("Please check DeployScripts.exe exists in the path.", UIConstants.strBMCConfig, MessageBoxButtons.OK, MessageBoxIcon.Error);
                         return;
                     }
                     System.Diagnostics.Process.Start(psi);
                     Application.DoEvents();
                     System.Threading.Thread.Sleep(10000);
                     Application.DoEvents();

                 }
              

            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Upgrade Script Run Failed." + ex.Message.ToString() + ex.Source.ToString(), LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
                //MessageBox.Show("An error has occured in the Upgrade Script Run.", UIConstants.strBMCConfig, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void txtLocCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (!((e.KeyCode >= Keys.NumPad0 && e.KeyCode <= Keys.NumPad9) ||
                    ((e.KeyCode >= Keys.D0 && e.KeyCode <= Keys.D9) && (!e.Shift)) ||
                    e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete || e.KeyCode ==
                    Keys.Alt || e.KeyCode == Keys.Left || e.KeyCode == Keys.Right ||
                    e.KeyCode == Keys.Shift || e.KeyCode == Keys.Home || e.KeyCode ==
                    Keys.End || e.KeyCode == Keys.Decimal || e.KeyCode == Keys.Enter))

                    e.SuppressKeyPress = true;
            }


            catch (Exception ex)
            {
                LogManager.WriteLog("txtLocCode_KeyDown" + ex.Message.ToString() + ex.Source.ToString(), LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
            }
        }

        private void btnSaveEnterpriseConnection_Click(object sender, EventArgs e)
        {
            bool bTestEnterpriseConnection = false;
            Dictionary<string,string> dictSetregistryentries;
            string strEncryptEnterpriseConnection = string.Empty;
            string strEncryptEnterpriseConnectionHex = string.Empty;
            try
            {
                //tSStatus.Text = "Processing...";
                dictSetregistryentries = new Dictionary<string, string>();
                Cursor = Cursors.WaitCursor;
                //Enterprise Server save 
                if (ValidateText(txtenterpriseServer, "Server"))
                {
                    if (ValidateText(txtenterpriseUsername, "UserName"))
                    {
                        if (ValidateText(txtenterprisePassword, "Password"))
                        {
                            if (ValidateText(txtEnterpriseTimeOut, "Connection Timeout"))
                            {
                                bTestEnterpriseConnection = TestConnection(txtenterpriseServer.Text, txtenterpriseUsername.Text, txtenterprisePassword.Text, txtEnterpriseTimeOut.Text, txtenterpriseInstance.Text, 'E');
                            }
                        }
                    }
                }                
                if (bTestEnterpriseConnection == true)
                {
                    if (!string.IsNullOrEmpty(ReturnConnectionString))
                        EnterpriseConfigRegistryEntities.EnterpriseConnectionString = ReturnConnectionString;
                    strEncryptEnterpriseConnection = RegistrySettings.EncryptEnterpriseConnection();
                    if (!string.IsNullOrEmpty(strEncryptEnterpriseConnection))
                    {
                        dictSetregistryentries.Add(UIConstants.strSQLConnect, strEncryptEnterpriseConnection + "+" + "REG_SZ");                                              
                        //Save all Registry Settings under cash master
                        RegistrySettings.SetRegistryEntries(dictSetregistryentries, EnterpriseConfigRegistryEntities.RegistryKeyValue);
                        Application.DoEvents();
                        MessageBox.Show("Enterprise Connection Saved Successfully.", UIConstants.strBMCConfig, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //tSStatus.Text = "Done.";                        
                        //GetSettingsInfo();
                        GetSettings();
                        GetServiceStatusToListView();
                        RefreshControls();
                    }
                }
                else
                {
                    MessageBox.Show("Connection to Enterprise Database Failed.", UIConstants.strBMCConfig, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    btnSaveSettings.Enabled = true;
                    //tSStatus.Text = "Connection failed.";
                }
                Application.DoEvents();
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("btnSaveEnterpriseConnection_Click" + ex.Message.ToString() + ex.Source.ToString(), LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void btnRefreshServices_Click(object sender, EventArgs e)
        {           
            //Get all the service settings
            if (btnSelectAll.Text.ToString().ToUpper().Equals("DESELECT ALL"))
            {
                btnSelectAll.Text = "Select All";
            }
            GetServiceStatusToListView();
        }
      

        private void btnSelectAll_Click(object sender, EventArgs e)
        {

            if (bSelectAll == false)
            {
                btnSelectAll.Text = "DeSelect All";
                bSelectAll = true;
            }
            else
            {
                btnSelectAll.Text = "Select All";
                bSelectAll = false;
            }

            if (lvServiceslist.Items.Count > 0)
            {
                for (int i = 0; i < lvServiceslist.Items.Count; i++)
                {
                    if (lvServiceslist.Items[i].Checked == true)
                    {
                        lvServiceslist.Items[i].Checked = false;
                        Application.DoEvents();
                        // ProgressBarServices.Value = 20;
                    }
                    else
                    {
                        lvServiceslist.Items[i].Checked = true;
                    }
                }
            }
          
        }

        private void btnMeterAnalysisTestConnection_Click(object sender, EventArgs e)
        {
            bool bTestConnection = false;
            try
            {
                Cursor = Cursors.WaitCursor;
                if (chkUseEnterpriseConnection.Checked == false)
                {
                    MessageBox.Show("Please check the Use Enterprise connection string!", UIConstants.strBMCConfig, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                bTestConnection = TestConnection(txtenterpriseServer.Text, txtenterpriseUsername.Text, txtenterprisePassword.Text, txtEnterpriseTimeOut.Text, txtenterpriseInstance.Text, 'M');

                if (bTestConnection == true)
                {
                    //tSStatus.Text = "Connection To Ticketing Database Successfull.";
                    MessageBox.Show("Connection To Meter Analysis Database Successfull.", UIConstants.strBMCConfig, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    //tSStatus.Text = "Connection To Ticketing Database Failed.";
                    MessageBox.Show("Connection To Meter Analysis Database Failed.", UIConstants.strBMCConfig, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("btnTicketingTestConnection_Click" + ex.Message.ToString() + ex.Source.ToString(), LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void btnDeployReports_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(strConnection))
            {
                Process psi = new System.Diagnostics.Process();
                psi.StartInfo.FileName = ConfigurationSettings.AppSettings["ApplicationStartupPath"].ToString() + "\\BMC DEPLOY REPORTS\\RunDeployReport.bat";
                if (File.Exists(psi.StartInfo.FileName))
                {
                    psi.Start();
                }
                else
                {
                    MessageBox.Show("File"+psi.StartInfo.FileName.ToString()+" Not Found.", UIConstants.strBMCConfig, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }
            else
            {
                MessageBox.Show("Save the connection settings before Deploying the reports1", UIConstants.strBMCConfig, MessageBoxButtons.OK, MessageBoxIcon.Warning);                
            }
        }

        private void btnCreateEnterpriseKey_Click(object sender, EventArgs e)
        {
            string EncKey = string.Empty;
            RegistryKey regKey;
            string sConnect = string.Empty;
            try
            {
                
               
                //Save  Registry Settings under cash master HQ
               
                Application.DoEvents();
                
                //regKey = Registry.LocalMachine.OpenSubKey(BMC.Common.ConfigurationManagement.ConfigManager.Read("RegistryPath"), true);
                //if (regKey.GetValue("EnterpriseKey") != null)
                //    EncKey = regKey.GetValue("EnterpriseKey").ToString();

                //if (EncKey == string.Empty)
                //    regKey.SetValue("EnterpriseKey", CryptographyHelper.Encrypt(CryptographyHelper.GetHashString(DateTime.Now.Ticks.ToString())));
                //regKey.Close();

                EncKey = BMCRegistryHelper.GetRegKeyValue(ConfigManager.Read("RegistryPath"), "EnterpriseKey");
                //if (EncKey == string.Empty)
                //    regKey.SetValue("EnterpriseKey", CryptographyHelper.Encrypt(CryptographyHelper.GetHashString(DateTime.Now.Ticks.ToString())));

                MessageBox.Show("Enterprise Key created successfully.");
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        private void chkEnableLGE_CheckedChanged(object sender, EventArgs e)
        {
            if (chkEnableLGE.Checked == true)
                gpLGECredentials.Enabled = true;
        }

        private void btnLGEGatewayTestConnection_Click(object sender, EventArgs e)
        {
            if (ValidateText(txtLGEServer, "Server"))
            {
                if (ValidateText(txtLGEUsername, "UserName"))
                {
                    if (ValidateText(txtLGEPassword, "Password"))
                    {
                        if (ValidateText(txtLGEtimeout, "Connection Timeout"))
                        {
                            bTestCMPConnection = TestConnection(txtLGEServer.Text, txtLGEUsername.Text, txtLGEPassword.Text, txtLGEtimeout.Text, txtLGEInstance.Text, 'C');
                        }
                    }
                }
            }      
        }   
       
    }
    public class PropertyClass
    {
        public string ID;
        public string Name;
        public string Value;
    }

}