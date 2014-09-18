using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using BMC.Monitoring;
using BMC.Business.ExchangeConfig;
using BMC.Transport.ExchangeConfig;
using BMC.DBInterface.ExchangeConfig;
using BMC.Common.Utilities;
using BMC.Common.LogManagement;
using BMC.Common.ExceptionManagement;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.IO;
using BMC.Common.ConfigurationManagement;
using System.Xml;
using Microsoft.Win32;
namespace BMC.UI.ExchangeConfig
{
    public partial class frmBMCExchangeConfig : Form
    {
        #region Declarations
        string strConnection = string.Empty;       
        private string ReturnConnectionString=string.Empty;  
        List<PropertyClass> ChangedProperty = new List<PropertyClass>();
        DataTable SettingsTable = null;
        private string strRegistryKeyPath = string.Empty;      
        private string strScriptPath = string.Empty;
        private string strUpgradeVisible = string.Empty;
        private string[] strListarray = null;       
        private string strServiceStatus = string.Empty;
        private PropertyBag PropertyHolder = new PropertyBag();
        private bool bSelectAll = false;
        private string strUrlvalidate = string.Empty;
        private string sProtocol = string.Empty;
        #endregion
        public frmBMCExchangeConfig()
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

                btnExchangeTestConnection.BackgroundImage = objbmp;
                btnSaveExchangeConnection.BackgroundImage = objbmp;
                btnexchangeDBRestore.BackgroundImage = objbmp;
                btnDSNSave.BackgroundImage = objbmp;
                btnTicketingTestConnection.BackgroundImage = objbmp;
                btnTicketDBRestore.BackgroundImage = objbmp;
                btnCMPGatewayTestConnection.BackgroundImage = objbmp;
                btnCMPGatewaySaveChanges.BackgroundImage = objbmp;
                btnTestURL.BackgroundImage = objbmp;
                btnCreateMSMQ.BackgroundImage = objbmp;
                btnStartService.BackgroundImage = objbmp;
                btnEndService.BackgroundImage = objbmp;
                btnRefreshServices.BackgroundImage = objbmp;
                btnSaveSettings.BackgroundImage = objbmp;
                btnRunUpgradeScript.BackgroundImage = objbmp;
                btnExit.BackgroundImage = objbmp;
                btnSelectAll.BackgroundImage = objbmp;
                btnExchangeTestConnection.BackgroundImageLayout = ImageLayout.Stretch;
                btnSaveExchangeConnection.BackgroundImageLayout = ImageLayout.Stretch;
                btnDSNSave.BackgroundImageLayout = ImageLayout.Stretch;
                btnexchangeDBRestore.BackgroundImageLayout = ImageLayout.Stretch;
                btnTicketingTestConnection.BackgroundImageLayout = ImageLayout.Stretch;
                btnTicketDBRestore.BackgroundImageLayout = ImageLayout.Stretch;
                btnCMPGatewayTestConnection.BackgroundImageLayout = ImageLayout.Stretch;
                btnCMPGatewaySaveChanges.BackgroundImageLayout = ImageLayout.Stretch;
                btnTestURL.BackgroundImageLayout = ImageLayout.Stretch;
                btnCreateMSMQ.BackgroundImageLayout = ImageLayout.Stretch;
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
                cmbLanIP.BackColor = Color.White;
                cmbSlotLan.BackColor = Color.White;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("PaintGradient:  " + ex.Message.ToString() + ex.Source.ToString(), LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
            }
        }
        /// <summary>
        /// Settings for the list view             
        /// <param name=""></param>
        /// <returns></returns>
        /// </summary>
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

        /// <summary>
        /// To get all the service settings when the form loaded       
        /// <param name=""></param>
        /// <returns></returns>
        /// </summary>      
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
                        if (objKeys.Key.ToString().ToUpper() == "REGISTRYPATH")
                        {
                            strRegistryKeyPath = objKeys.Value.ToString();
                            if (!string.IsNullOrEmpty(strRegistryKeyPath))
                            {
                                ExchangeConfigRegistryEntities.RegistryKeyValue = strRegistryKeyPath;
                            }

                        }
                        else if (objKeys.Key.ToString().ToUpper() == "NETLOGGERPATH")
                        {
                            strRegistryKeyPath = objKeys.Value.ToString();
                            if (!string.IsNullOrEmpty(strRegistryKeyPath))
                            {
                                ExchangeConfigRegistryEntities.NetLoggerRegKeyValue = strRegistryKeyPath;
                            }
                        }
                        else if (objKeys.Key.ToString().ToUpper() == "UPGRADESCRIPT")
                        {
                            strScriptPath = objKeys.Value.ToString();
                        }
                        else if (objKeys.Key.ToString().ToUpper() == "VISIBLEUPGRADESCRIPT")
                        {
                            strUpgradeVisible = objKeys.Value.ToString();
                        }
                        else if (objKeys.Key.ToString().ToUpper() == "PROTOCOL")
                        {
                            if (objKeys.Value.ToString().ToUpper() == "HTTPS://")
                                chkTrusted.Checked = true;
                            else
                                chkTrusted.Checked = false;
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
        /// <summary>
        /// To get all the status of the services on form load
        /// <Author>Vineetha Mathew</Author>      
        /// <param name=""></param>
        /// <returns></returns>
        /// </summary>      
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
        /// <summary>
        /// To get the status of a particular service
        /// <Author>Vineetha Mathew</Author>       
        /// <param name="strServiceName">string</param>
        /// <returns></returns>
        /// </summary>       
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
        /// <summary>
        /// To start the selected service and change the color accordingly
        ///  Color.Blue- Successfull running of service
        ///  Color.Red- Failure while running a service
        ///  Color.HotPink- Pending status while running a service       
        /// <param >button_nsender</param>
        /// <returns></returns>
        /// </summary>            
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
        /// <summary>
        /// To start the selected service  
        /// <param name="strServicename">string</param>
        /// <returns name="service status">bool</returns>
        /// </summary>              
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
        /// <summary>
        /// To End the selected service and change the color accordingly
        ///  Color.Blue- Successfull running of service
        ///  Color.Red- Failure while running a service
        ///  Color.HotPink- Pending status while running a service  
        /// <param >button_nsender</param>
        /// <returns></returns>
        /// </summary>      
        /// Method Revision History
        ///
        /// Author             Date              Description
        /// ---------------------------------------------------
        ///Vineetha Mathew      08-12-2008      Created
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
        /// <summary>
        /// To End the selected service 
        /// <param name="strServicename">string</param>
        /// <returns name="service status">bool</returns>
        /// </summary>     
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
        /// <summary>
        /// Done all initial settings in form load  
        /// <param ></param>
        /// <returns></returns>
        /// </summary>         
   
        private void frmBMCExchangeConfig_Load(object sender, EventArgs e)
        {
            try
            {               
                //get the settings from the config file      
                GetInitialSettings();

                //Get all the binding IP's
                GetBindingIPS();

                //Retrieve the server settings for Exchange,Ticketing and CMP
                GetSettings();

                //Get the property grid settings
                GetSettingsInfo();                

                //Call refresh to save exchange connection when the registry is not having data.
                RefreshControls();

                //Get all the service settings
                GetServiceStatusToListView();

                //Get DHCP Settings.
                GetDHCPSettings();

                //Set DHCP Controls
                RefreshDHCPControls();

                //Check the visibility of the button upgrade script based on the config setting
                if (strUpgradeVisible.ToUpper() == "FALSE")
                {
                    btnRunUpgradeScript.Enabled = false;
                }

            }
            catch (Exception ex)
            {
                LogManager.WriteLog("frmBMCExchangeConfig_Load" + ex.Message.ToString() + ex.Source.ToString(), LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
            }
          
        }
        /// <summary>
        /// Get all the binding IP's         
        /// <param name=""></param>
        /// <returns></returns>
        /// </summary>
        private void GetBindingIPS()
        {
          string[] strIPAddressList=null;

          try
          {
              strIPAddressList = ReadServicesSettings.GetAllLocalIP();
              if (strIPAddressList.Length > 0)
              {
                  cmbLanIP.Items.Add("--Select--");
                  cmbSlotLan.Items.Add("--Select--");

                  if (strIPAddressList.Length == 1)
                  {
                      cmbLanIP.DropDownStyle = ComboBoxStyle.DropDownList;
                      cmbLanIP.Items.Add(strIPAddressList[0].ToString());
                      if (cmbLanIP.Items.Count > 1)
                      {
                          cmbLanIP.SelectedItem = strIPAddressList[0].ToString();
                      }
                      else
                      {
                          cmbLanIP.SelectedIndex = 0;
                      }


                      cmbSlotLan.Items.Add(strIPAddressList[0].ToString());
                      if (cmbSlotLan.Items.Count > 1)
                      {
                          cmbSlotLan.SelectedItem = strIPAddressList[0].ToString();
                      }
                      else
                      {
                          cmbSlotLan.SelectedIndex = 0;
                      }
                  }
                  else if (strIPAddressList.Length > 1)
                  {
                      for (int i = 0; i < strIPAddressList.Length; i++)
                      {
                          if (strIPAddressList[i] != null && strIPAddressList[i] != string.Empty)
                          {
                              if (ValidateIP(strIPAddressList[i].ToString()) == true)
                              {
                                  cmbLanIP.DropDownStyle = ComboBoxStyle.DropDownList;
                                  cmbLanIP.Items.Add(strIPAddressList[i].ToString());
                              }

                              if (ValidateIP(strIPAddressList[i].ToString()) == true)
                              {
                                  cmbSlotLan.DropDownStyle = ComboBoxStyle.DropDown;
                                  cmbSlotLan.Items.Add(strIPAddressList[i].ToString());
                              }
                          }
                      }
                  }

                  //cmbLanIP.SelectedIndex = 0;
                  //cmbSlotLan.SelectedIndex = 0;
              }

          }
          catch (Exception ex)
          {
              LogManager.WriteLog("GetBindingIPS" + ex.Message.ToString() + ex.Source.ToString(), LogManager.enumLogLevel.Error);
              ExceptionManager.Publish(ex);
          }
      
        }

        private void GetDHCPSettings()
        {
            string strKeyvalue = string.Empty;

            try
            {

                if (!String.IsNullOrEmpty(strConnection))
                {
                    Dictionary<string, string> ExchangeRegistryEntries = RegistrySettings.GetRegistryEntries(UIConstants.strMulticastip);
                    
                    foreach (KeyValuePair<string, string> KVPServer in ExchangeRegistryEntries)
                    {
                        strKeyvalue = KVPServer.Key.Substring(KVPServer.Key.LastIndexOf('\\') + 1);

                        if (strKeyvalue.ToLower() == "multicastip")
                        {
                            if (!string.IsNullOrEmpty(KVPServer.Value.ToString()))
                            {
                                txtMultiCastIP.Text = KVPServer.Value.ToString();
                            }
                            else
                            {
                                txtMultiCastIP.Text = "239.192.1.1";
                                Dictionary<string, string> dictSetregistryentries; 
                                dictSetregistryentries = new Dictionary<string, string>();
                                dictSetregistryentries.Add(UIConstants.strMulticastip.ToString(), txtMultiCastIP.Text.ToString().Trim() + "+" + "REG_SZ");

                                //Save all Registry Settings under cash master
                                RegistrySettings.SetRegistryEntries(dictSetregistryentries, ExchangeConfigRegistryEntities.RegistryKeyValue);
                                Application.DoEvents();
                            }
                        }

                        if (strKeyvalue.ToLower() == "interface")
                        {
                            if (!string.IsNullOrEmpty(KVPServer.Value.ToString()))
                            {
                                txtInterfaceIP.Text = KVPServer.Value.ToString(); 
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("GetDHCPSettings" + ex.Message.ToString() + ex.Source.ToString(), LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
            }
        }

        /// <summary>
        /// Enable or Disable control based on the registry entry for the first time   
        /// <param name=""></param>
        /// <returns></returns>
        /// </summary>
        private void RefreshControls()
        {
           // if ((txtexchangeServer.Text == string.Empty) && (txtexchangeUsername.Text == string.Empty) && (txtexchangePassword.Text == string.Empty))
            if ((txtexchangeServer.Text == string.Empty) || (txtexchangeUsername.Text == string.Empty) || (txtexchangePassword.Text == string.Empty))
            {
                
                gpTicketSetup.Enabled = false;
                gpCMPSetup.Enabled = false;
                gpLocalBindIP.Enabled = false;
                gpDHCPSettings.Enabled = false;
                gpWebServiceSetup.Enabled = false;
                //gpSystemSettings.Enabled = false;
                btnSaveSettings.Enabled = false;
                if (strUpgradeVisible.ToUpper() == "FALSE")
                {
                    btnRunUpgradeScript.Enabled = false;
                }
            }
            else
            {                
                gpTicketSetup.Enabled = true;
                gpCMPSetup.Enabled = true;
                gpLocalBindIP.Enabled = true;
                gpDHCPSettings.Enabled = true;
                gpWebServiceSetup.Enabled = true;
                //gpSystemSettings.Enabled = true;
                btnSaveSettings.Enabled = true;
                if (strUpgradeVisible.ToUpper() == "TRUE")
                {
                    btnRunUpgradeScript.Enabled = true;
                }               
            }
        }

        private void RefreshDHCPControls()
        {
            if (chkEnableDHCP.Checked)
            {
                txtMultiCastIP.Enabled = false;
                txtInterfaceIP.Enabled = false;

                //txtMultiCastIP.Text = 
            }
            else
            {
                txtMultiCastIP.Enabled = true;
                txtInterfaceIP.Enabled = true;
            }
        } 

        private void GetSettingsInfo()
        {
            bool bTestExchangeConnection=false;
            try
            {
                LogManager.WriteLog("Getting Exchange Connection String" , LogManager.enumLogLevel.Debug);

                if (ValidateText(txtexchangeServer, "Server"))
                {
                    if (ValidateText(txtexchangeUsername, "UserName"))
                    {
                        if (ValidateText(txtexchangePassword, "Password"))
                        {
                            if (ValidateText(txtexchangeTimeOut, "Connection Timeout"))
                            {
                                bTestExchangeConnection = TestConnection(txtexchangeServer.Text, txtexchangeUsername.Text, txtexchangePassword.Text, txtexchangeTimeOut.Text, txtexchangeInstance.Text, 'E');
                            }
                        }
                    }
                }
                  if (bTestExchangeConnection == true)
                  {
                      if (!string.IsNullOrEmpty(ReturnConnectionString))
                          DataBaseServiceHandler.ConnectionString = ReturnConnectionString;
                  }
                //DataBaseServiceHandler.ConnectionString = RegistrySettings.ExchangeConnectionString();

                LogManager.WriteLog("Exchange Connection String" + DataBaseServiceHandler.ConnectionString.Length.ToString(),LogManager.enumLogLevel.Debug);
                SettingsTable = new DataTable();
                DataBaseServiceHandler.Fill(QueryType.Text, "Select [ID] = Setting_ID , [Name] = Setting_Name, [Value] = Isnull(Setting_Value,'') From Setting", SettingsTable);
                PropertyBag pbSetting = new PropertyBag();

                if (SettingsTable != null && SettingsTable.Rows.Count > 0)
                {
                    LogManager.WriteLog("Setting Table Rows Count   " + SettingsTable.Rows.Count.ToString(),LogManager.enumLogLevel.Debug);
                }
                else
                {
                    LogManager.WriteLog("Could not populate Settings Table", LogManager.enumLogLevel.Debug);
                }

                pbSetting.GetValue += new PropertySpecEventHandler(pbSetting_GetValue);
                pbSetting.SetValue += new PropertySpecEventHandler(pbSetting_SetValue);

                foreach (DataRow dr in SettingsTable.Rows)
                {
                    if (BMC.Common.Utilities.Common.GetRowValue<string>(dr, "Value").ToUpper() == "TRUE" || BMC.Common.Utilities.Common.GetRowValue<string>(dr, "Value").ToUpper() == "FALSE")
                        if (BMC.Common.Utilities.Common.GetRowValue<string>(dr, "Value").ToUpper() == "TRUE")
                            pbSetting.Properties.Add(new PropertySpec(BMC.Common.Utilities.Common.GetRowValue<string>(dr, "Name"), typeof(bool), "BMC Category", null, true));
                        else
                            pbSetting.Properties.Add(new PropertySpec(BMC.Common.Utilities.Common.GetRowValue<string>(dr, "Name"), typeof(bool), "BMC Category", null, false));
                    else
                        pbSetting.Properties.Add(new PropertySpec(BMC.Common.Utilities.Common.GetRowValue<string>(dr, "Name"), typeof(string), "BMC Category", null, BMC.Common.Utilities.Common.GetRowValue<string>(dr, "Value")));

                }
                if (pbSetting != null && pbSetting.Properties.Count > 0)
                {
                    LogManager.WriteLog("PBSetting Properties Count    " + pbSetting.Properties.Count.ToString(), LogManager.enumLogLevel.Debug);
                }
                //propertyGridBag1.PropertyGridContorl.SelectedObject = pbSetting;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("GetSettingsInfo" + ex.Message.ToString() + ex.Source.ToString(), LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
            }
        }
        private void pbSetting_GetValue(object sender, PropertySpecEventArgs e)
        {
            e.Value = BMC.Common.Utilities.Common.GetRowValue<string>(SettingsTable.Select("Name = '" + e.Property.Name + "'")[0], "Value");
        }
        private void pbSetting_SetValue(object sender, PropertySpecEventArgs e)
        {
            PropertyClass Pb = new PropertyClass();
            Pb.Name = e.Property.Name;
            Pb.Value = e.Value.ToString();
            ChangedProperty.Add(Pb);
            SettingsTable.Select("Name = '" + e.Property.Name + "'")[0]["Value"] = e.Value;
        }
        /// <summary>
        /// Get all the settings value from registry & database   
        /// <param name=""></param>
        /// <returns></returns>
        /// </summary>
        private void GetSettings()
        {
            string strKeyvalue = string.Empty;            
            string strWebUrl = string.Empty;
            string strTicketingLocCode = string.Empty;

            try
            {

                strConnection = RegistrySettings.ExchangeConnectionString();

                if (!String.IsNullOrEmpty(strConnection))
                {
                    Dictionary<string, string> ServerEntries = Credentials.RetrieveServerDetails(strConnection);
                    //Loading services from DB
                    ConfigManager.SetConfigurationMode(ConfigManager.ConfigurationMode.AppConfig);
                    if (ConfigManager.Read("ServicesListFromDB") != null)
                    {
                        if (ConfigManager.Read("ServicesListFromDB").ToUpper() == "TRUE")
                        {
                            strListarray = null;
                            strListarray =DBSettings.GetSettingValue(strConnection,"ServiceNames").Split(',');
                            LoadServicesToListView(strListarray);
                        }
                    }
                    GetExchangeServerSettings(ServerEntries);

                    string strCMPConnectionString = DBSettings.CMPConnectionString(strConnection);
                    Dictionary<string, string> CMPServerEntries = Credentials.RetrieveServerDetails(strCMPConnectionString);
                    GetCMPServerSettings(CMPServerEntries);

                    string strTicketingConnectionString = DBSettings.TicketingConnectionString(strConnection);
                    Dictionary<string, string> TicketingServerEntries = Credentials.RetrieveServerDetails(strTicketingConnectionString);
                    GetTicketingServerSettings(TicketingServerEntries);

                    strTicketingLocCode = DBSettings.TicketingLocCodeString(strConnection);
                    txtLocCode.Text = strTicketingLocCode;

                    Dictionary<string, string> ExchangeRegistryEntries = RegistrySettings.GetRegistryEntries(ExchangeConfigRegistryEntities.RegistryKeyValue);
                    foreach (KeyValuePair<string, string> KVPServer in ExchangeRegistryEntries)
                    {
                        strKeyvalue = KVPServer.Key.Substring(KVPServer.Key.LastIndexOf('\\') + 1);

                        if (strKeyvalue.ToLower() == "serverip")
                        {
                            if (!string.IsNullOrEmpty(KVPServer.Value.ToString()))
                            {
                                if (cmbSlotLan.Items.IndexOf(KVPServer.Value.ToString()) == -1)
                                {
                                    cmbSlotLan.Items.Add(KVPServer.Value.ToString());
                                }
                                cmbSlotLan.SelectedIndex = cmbSlotLan.Items.IndexOf(KVPServer.Value.ToString());
                            }
                            else
                            {
                                cmbSlotLan.DropDownStyle = ComboBoxStyle.DropDownList;
                                cmbSlotLan.SelectedIndex = 0;
                            }
                        }

                        if (strKeyvalue.ToLower() == "enabledhcp")
                        {
                            if (!string.IsNullOrEmpty(KVPServer.Value.ToString()))
                            {
                                chkEnableDHCP.Checked = Convert.ToBoolean(int.Parse(KVPServer.Value));
                            }
                        }
                        if (strKeyvalue.ToLower() == "encryptenable")
                        {
                            if (!string.IsNullOrEmpty(KVPServer.Value.ToString()))
                            {
                                 chkEnableEncrypt.Checked = Convert.ToBoolean(int.Parse(KVPServer.Value));
                            }
                        }
                        if (strKeyvalue.ToLower() == "rsaenable")
                        {
                            if (!string.IsNullOrEmpty(KVPServer.Value.ToString()))
                            {
                                chkEnableRSAEncrypt.Checked = Convert.ToBoolean(int.Parse(KVPServer.Value));
                            }
                        }
                        if (strKeyvalue.ToLower() == "dismachinewhenremove")
                        {
                            if (!string.IsNullOrEmpty(KVPServer.Value.ToString()))
                            {
                                chkDisableMachine.Checked = Convert.ToBoolean(int.Parse(KVPServer.Value));
                            }
                        }
                        if (strKeyvalue.ToLower() == "bindipaddress")
                        {
                            if (!string.IsNullOrEmpty(KVPServer.Value.ToString()))
                            {
                                if (cmbLanIP.Items.IndexOf(KVPServer.Value.ToString()) == -1)
                                {
                                    cmbLanIP.Items.Add(KVPServer.Value.ToString());
                                }
                                cmbLanIP.SelectedIndex = cmbLanIP.Items.IndexOf(KVPServer.Value.ToString());
                            }
                            else
                            {
                                cmbLanIP.DropDownStyle = ComboBoxStyle.DropDownList;
                                cmbLanIP.SelectedIndex = 0;
                            }
                        }

                        if (strKeyvalue.ToLower() == "bgswebservice")
                        {
                            if (!string.IsNullOrEmpty(KVPServer.Value.ToString()))
                            {
                                strWebUrl = KVPServer.Value.ToString();
                                txtEnterpriseweburl.Text = strWebUrl;
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
        /// <summary>
        /// Display the exchange server details.
        /// <param name="ServerEntries"></param>
        /// <returns></returns>
        /// </summary>       
        private void GetExchangeServerSettings(Dictionary<string, string> ServerEntries)
        {
            try
            {
                if (ServerEntries != null)
                {
                    foreach (KeyValuePair<string, string> objKeyValue in ServerEntries)
                    {
                        if (objKeyValue.Key.ToUpper() == "SERVER")
                        {
                            txtexchangeServer.Text = objKeyValue.Value;
                        }
                        else if (objKeyValue.Key.ToUpper() == "UID")
                        {
                            txtexchangeUsername.Text = objKeyValue.Value;
                        }
                        else if (objKeyValue.Key.ToUpper() == "PASSWORD")
                        {
                            txtexchangePassword.Text = objKeyValue.Value;
                        }
                        else if (objKeyValue.Key.ToUpper() == "DATABASE")
                        {
                            lblexchangeDBname.Text = objKeyValue.Value;
                        }
                        else if (objKeyValue.Key.ToUpper() == "TIMEOUT")
                        {
                            txtexchangeTimeOut.Text = objKeyValue.Value;
                        }
                        else if (objKeyValue.Key.ToUpper() == "INSTANCE")
                        {
                            txtexchangeInstance.Text = objKeyValue.Value;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("GetExchangeServerSettings" + ex.Message.ToString() + ex.Source.ToString(), LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
            }
        }
        /// <summary>
        /// Display the CMP server details.
        /// <param name="CMPServerEntries"></param>
        /// <returns></returns>
        /// </summary>        
        private void GetCMPServerSettings(Dictionary<string, string> CMPServerEntries)
        {
            try
            {
                if (CMPServerEntries != null)
                {
                    foreach (KeyValuePair<string, string> objKeyValue in CMPServerEntries)
                    {
                        if (objKeyValue.Key.ToUpper() == "SERVER")
                        {
                            txtCMPServer.Text = objKeyValue.Value;
                        }
                        else if (objKeyValue.Key.ToUpper() == "UID")
                        {
                            txtCMPUsername.Text = objKeyValue.Value;
                        }
                        else if (objKeyValue.Key.ToUpper() == "PASSWORD")
                        {
                            txtCMPPassword.Text = objKeyValue.Value;
                        }
                        else if (objKeyValue.Key.ToUpper() == "DATABASE")
                        {
                            lblCMPDB.Text = objKeyValue.Value;
                        }
                        else if (objKeyValue.Key.ToUpper() == "TIMEOUT")
                        {
                            txtCMPtimeout.Text = objKeyValue.Value;
                        }
                        else if (objKeyValue.Key.ToUpper() == "INSTANCE")
                        {
                            txtCMPInstance.Text = objKeyValue.Value;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("GetCMPServerSettings" + ex.Message.ToString() + ex.Source.ToString(), LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
            }
        }
        /// <summary>
        /// Display the Ticketing server details.
        /// <param name="CMPServerEntries"></param>
        /// <returns></returns>
        /// </summary>        
        private void GetTicketingServerSettings(Dictionary<string, string> TicketingServerSettings)
        {
            try
            {
                if (TicketingServerSettings != null)
                {
                    foreach (KeyValuePair<string, string> objKeyValue in TicketingServerSettings)
                    {
                        if (objKeyValue.Key.ToUpper() == "SERVER")
                        {
                            txtticketserver.Text = objKeyValue.Value;
                        }
                        else if (objKeyValue.Key.ToUpper() == "UID")
                        {
                            txtticketusername.Text = objKeyValue.Value;
                        }
                        else if (objKeyValue.Key.ToUpper() == "PASSWORD")
                        {
                            txtticketPassword.Text = objKeyValue.Value;
                        }
                        else if (objKeyValue.Key.ToUpper() == "DATABASE")
                        {
                            lblticketDBname.Text = objKeyValue.Value;
                        }
                        else if (objKeyValue.Key.ToUpper() == "TIMEOUT")
                        {
                            txtticketTimeout.Text = objKeyValue.Value;
                        }
                        else if (objKeyValue.Key.ToUpper() == "INSTANCE")
                        {
                            txticketInstance.Text = objKeyValue.Value;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("GetTicketingServerSettings" + ex.Message.ToString() + ex.Source.ToString(), LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
            }
        }
        /// <summary>
        /// Check Message Queue for Exchange is created if not creating
        /// <param >button_sender</param>
        /// <returns></returns>
        /// </summary>        
        private void btnCreateMSMQ_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                ProgressBarServices.Value = 0;
                if (ReadServicesSettings.CheckMSMQExists(UIConstants.strExchangeQueuePath) == false)
                {
                    ProgressBarServices.Value = 20;
                    MessageBox.Show("Message Queue Already Exists.", UIConstants.strBMCConfig, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ProgressBarServices.Value = 0;
                }
                else
                {
                    //tSStatus.Text = "Processing.....please wait..";
                    ProgressBarServices.Value = 20;
                    if (ReadServicesSettings.CreateMSMQ(UIConstants.strExchangeQueuePath) == true)
                    {
                        ProgressBarServices.Value = 100;
                        //tSStatus.Text = "Created MSMQ successfully... ";
                        MessageBox.Show("Message Queue Created Successfully.", UIConstants.strBMCConfig, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ProgressBarServices.Value = 0;
                    }
                    else
                    {
                        ProgressBarServices.Value = 20;
                        //tSStatus.Text = "MSMQ creation failed.. ";
                        MessageBox.Show("Message Queue Creation Failed.", UIConstants.strBMCConfig, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        ProgressBarServices.Value = 0;
                    }
                }

            }
            catch (Exception ex)
            {
                if (ex.Message.Equals("Message Queuing has not been installed on this computer."))
                {
                    MessageBox.Show("Message Queue is not installed.", UIConstants.strBMCConfig, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    LogManager.WriteLog("btnCreateMSMQ_Click" + ex.Message.ToString() + ex.Source.ToString(), LogManager.enumLogLevel.Error);
                    ExceptionManager.Publish(ex);

                }
                else
                {
                    MessageBox.Show("Creation of MSMQ Failed.", UIConstants.strBMCConfig, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    LogManager.WriteLog("btnCreateMSMQ_Click" + ex.Message.ToString() + ex.Source.ToString(), LogManager.enumLogLevel.Error);
                    ExceptionManager.Publish(ex);
                }
            }
            finally
            {
                Cursor = Cursors.Default;
            }

        }
        /// <summary>        
        /// Testing enterprise web url to check web services is working  
        /// <param name=iSendValue>button sender</param>
        /// <returns></returns>
        /// </summary>        
        private void btnTestURL_Click(object sender, EventArgs e)
        {            
            try
            {
                Cursor = Cursors.WaitCursor;

                CheckWebService(txtEnterpriseweburl.Text);

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
            Regex objRegexUrlvalidate = new Regex("^(http|ftp|https)://(www\\.)?.+\\.(com|net|org|asmx)$");
            MatchCollection objMatchCollect;

            if (sUrl.Trim().Length < 0 || sUrl.Trim().Length == 0)
            {
                MessageBox.Show("You must supply a valid Web server/URL name.", UIConstants.strBMCConfig, MessageBoxButtons.OK, MessageBoxIcon.Information);
                //tSStatus.Text = "You must supply a valid server name";
                errValidate.SetError(txtEnterpriseweburl, "You must supply a valid Web server/URL name");
            }
            else
            {
                if (!sUrl.Contains(".asmx"))
                {
                    sWebExtension = ConfigManager.Read("WebserviceExtension");
                    LoadConfig();
                    if (!sUrl.Contains(sProtocol))
                         strUrlvalidate = sProtocol.Trim() + sUrl.Trim() + sWebExtension.Trim();
                    else
                        strUrlvalidate = sUrl.Trim() +sWebExtension.Trim();
                    txtEnterpriseweburl.Text = strUrlvalidate;
                }
                else
                {
                    LoadConfig();
                    if (!sUrl.Contains(sProtocol))
                    {
                        MessageBox.Show("Enterprise Web Url not in correct format.", UIConstants.strBMCConfig, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtEnterpriseweburl.Focus();
                        bReturn = false;
                    }
                    else
                    {
                        strUrlvalidate = sUrl.Trim();
                        objMatchCollect = objRegexUrlvalidate.Matches(strUrlvalidate);
                        if (objMatchCollect.Count > 0)
                           bReturn = true;                        
                        else
                        {
                            MessageBox.Show("Enterprise Web Url not in correct format.", UIConstants.strBMCConfig, MessageBoxButtons.OK, MessageBoxIcon.Error);                            
                            errValidate.SetError(txtEnterpriseweburl, "You must supply a valid Url");
                            bReturn = false;
                        }
                    }
                }

               
            }
            return bReturn;
        }

        /// <summary>
        /// Check Web service hitting  
        /// <param name="strURL">string</param>
        /// <returns>integer</returns>
        /// </summary>
        private int CheckWebService(string strURL)
        {  
            int iReceiveValue=-1;  
           
            try
            {
                   if (ValidateURL(strURL))
                    {
                        iReceiveValue = ReadServicesSettings.TestWebUrl(strUrlvalidate);
                        if (iReceiveValue==0)
                        {
                            MessageBox.Show("Trusted Site Found.", UIConstants.strBMCConfig, MessageBoxButtons.OK, MessageBoxIcon.Information);                            
                            errValidate.Clear();
                        }
                        else if (iReceiveValue > 0)
                        {
                            MessageBox.Show("Enterprise Web Service Test Successful.", UIConstants.strBMCConfig, MessageBoxButtons.OK, MessageBoxIcon.Information);                            
                            errValidate.Clear();
                        }
                        else                        
                            MessageBox.Show("Enterprise Web Service Test Failed.", UIConstants.strBMCConfig, MessageBoxButtons.OK, MessageBoxIcon.Error);                            
                        
                    }
                    else
                    {
                        iReceiveValue = -1;
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
        /// <summary>
        /// Validate the textbox details.
        /// </summary>
        /// <returns>success or failure</returns>
        /// Method Revision History
     
        private  bool ValidateText(TextBox tBox, string Message)
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
        /// <summary>
        /// For restorring the exchange Database  
        /// <param name=""></param>
        /// <returns></returns>
        /// </summary>
        private void btnexchangeDBRestore_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                frmSQLRestore frmRestoreDB = new frmSQLRestore("Exchange");
                frmRestoreDB.ShowDialog();
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("btnexchangeDBRestore_Click" + ex.Message.ToString() + ex.Source.ToString(), LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }
        /// <summary>
        /// testing ticketing Db connection       
        /// <param name=""></param>
        /// <returns></returns>
        /// </summary>
        private void btnTicketingTestConnection_Click(object sender, EventArgs e)
        {  
            bool bTestConnection = false;
            try
            {
                Cursor = Cursors.WaitCursor;
                if (ValidateText(txtticketserver, "Server"))
                {
                    if (ValidateText(txtticketusername, "UserName"))
                    {
                        if (ValidateText(txtticketPassword, "Password"))
                        {
                            if (ValidateText(txtticketTimeout, "Connection Timeout"))
                            {
                                bTestConnection = TestConnection(txtticketserver.Text, txtticketusername.Text, txtticketPassword.Text, txtticketTimeout.Text, txticketInstance.Text, 'T');
                            }
                        }
                    }
                }
                if (bTestConnection == true)
                {
                    //tSStatus.Text = "Connection To Ticketing Database Successfull.";
                    MessageBox.Show("Connection To Ticketing Database Successful.", UIConstants.strBMCConfig, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    //tSStatus.Text = "Connection To Ticketing Database Failed.";
                    MessageBox.Show("Connection To Ticketing Database Failed.", UIConstants.strBMCConfig, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        /// <summary>
        /// Test the DB Connection with the credentials entered
        /// </summary>
        /// <param name="Server"></param>
        /// <param name="UserName"></param>
        /// <param name="Password"></param>
        /// <param name="DataBase"></param>
        ///<param name="ConnectionTimeout"></param> 
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
        private void btnExchangeTestConnection_Click(object sender, EventArgs e)
        {
            bool bTestConnection = false;
            try
            {
                Cursor = Cursors.WaitCursor;

                if (ValidateText(txtexchangeServer, "Server"))
                {
                    if (ValidateText(txtexchangeUsername, "UserName"))
                    {
                        if (ValidateText(txtexchangePassword, "Password"))
                        {
                            if (ValidateText(txtexchangeTimeOut, "Connection Timeout"))
                            {
                                bTestConnection = TestConnection(txtexchangeServer.Text, txtexchangeUsername.Text, txtexchangePassword.Text, txtexchangeTimeOut.Text, txtexchangeInstance.Text, 'E');
                            }
                        }
                    }
                }
                if (bTestConnection == true)
                {
                    //tSStatus.Text = "Connection to Exchange Database Successfull.";
                    MessageBox.Show("Connection to Exchange Database Successful.", UIConstants.strBMCConfig, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    //tSStatus.Text = "Connection to Exchange Database Failed.";
                    MessageBox.Show("Connection to Exchange Database Failed.", UIConstants.strBMCConfig, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("btnExchangeTestConnection_Click" + ex.Message.ToString() + ex.Source.ToString(), LogManager.enumLogLevel.Error);
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
                //Test DB Connection for Exchange.
                if (chDatabase=='E')
                {
                    bTestConnection = AddServerDetails(strServer, strUsername, strPassword, UIConstants.strExchangeDBName, strTimeOut);
                }
                else if (chDatabase == 'T')
                {
                    bTestConnection = AddServerDetails(strServer, strUsername, strPassword, UIConstants.strTicketingDBName, strTimeOut);
                }
                else if (chDatabase == 'C')
                {
                    bTestConnection = AddServerDetails(strServer, strUsername, strPassword, UIConstants.strCMPDBName, strTimeOut);
                }
                
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("ExchangeTestConnection" + ex.Message.ToString() + ex.Source.ToString(), LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
            }
            return bTestConnection;
        }
        private void btnCMPGatewayTestConnection_Click(object sender, EventArgs e)
        {   
            bool bTestConnection = false;
            try
            {
                Cursor = Cursors.WaitCursor;
                if (ValidateText(txtCMPServer, "Server"))
                {
                    if (ValidateText(txtCMPUsername, "UserName"))
                    {
                        if (ValidateText(txtCMPPassword, "Password"))
                        {
                            if (ValidateText(txtCMPtimeout, "Connection Timeout"))
                            {
                                bTestConnection = TestConnection(txtCMPServer.Text, txtCMPUsername.Text, txtCMPPassword.Text, txtCMPtimeout.Text, txtCMPInstance.Text, 'C');
                            }
                        }
                    }
                }
                if (bTestConnection == true)
                {
                    //tSStatus.Text = "Connection to CMP Database Successfull.";
                    MessageBox.Show("Connection to CMP Database Successful.", UIConstants.strBMCConfig, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    //tSStatus.Text = "Connection to CMP Database Failed.";
                    MessageBox.Show("Connection to CMP Database Failed.", UIConstants.strBMCConfig, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("btnCMPGatewayTestConnection_Click" + ex.Message.ToString() + ex.Source.ToString(), LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }       
        private void chkUseExchangeConnection_CheckedChanged(object sender, EventArgs e)
        {

            //Use exchange credentials if checked.
            if (chkUseExchangeConnection.Checked)
            {
                txtticketserver.Text = txtexchangeServer.Text;
                txtticketusername.Text = txtexchangeUsername.Text;
                txtticketPassword.Text = txtexchangePassword.Text;
                txtticketTimeout.Text = txtexchangeTimeOut.Text;
            }
            else
            {
                //Enter new credentials
                txtticketserver.Text = string.Empty;
                txtticketusername.Text = string.Empty;
                txtticketPassword.Text = string.Empty;
                txtticketTimeout.Text = string.Empty;
            }
        }
        private void chkUseExchangeConnect_CheckedChanged(object sender, EventArgs e)
        {
            //Use exchange credentials if checked.
            if (chkUseExchangeConnect.Checked)
            {
                txtCMPServer.Text = txtexchangeServer.Text;
                txtCMPUsername.Text = txtexchangeUsername.Text;
                txtCMPPassword.Text = txtexchangePassword.Text;
                txtCMPtimeout.Text = txtexchangeTimeOut.Text;
            }
            else
            {
                //Enter new credentials
                txtCMPServer.Text = string.Empty;
                txtCMPUsername.Text = string.Empty;
                txtCMPPassword.Text = string.Empty;
                txtCMPtimeout.Text = string.Empty;
            }
        }
        private void btnSaveSettings_Click(object sender, EventArgs e)
        {
            Regex objRegexUrlvalidate = new Regex("^(http|ftp|https)://(www\\.)?.+\\.(com|net|org|asmx)$");
            MatchCollection objMatchCollect;
            int iEnableDHCP = 0;
            int iEnableEncrypt = 0;
            int iDisableMachine= 0;
            string strSlotLanIPAddress = string.Empty;
            string strEncryptExchangeConnection = string.Empty;
            string strEncryptExchangeConnectionHex = string.Empty;
            bool bTestExchangeConnection = false;
            bool bTestTicketingConnection = false;
            bool bTestCMPConnection = false;
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
                    //Exchange Server save 
                    if (ValidateText(txtexchangeServer, "Server"))
                    {
                        if (ValidateText(txtexchangeUsername, "UserName"))
                        {
                            if (ValidateText(txtexchangePassword, "Password"))
                            {
                                if (ValidateText(txtexchangeTimeOut, "Connection Timeout"))
                                {
                                    bTestExchangeConnection = TestConnection(txtexchangeServer.Text, txtexchangeUsername.Text, txtexchangePassword.Text, txtexchangeTimeOut.Text, txtexchangeInstance.Text,'E');
                                }
                            }
                        }
                    }                   
                    if (bTestExchangeConnection == true)
                    {
                        if (!string.IsNullOrEmpty(ReturnConnectionString))                            
                        ExchangeConfigRegistryEntities.ExchangeConnectionString = ReturnConnectionString;                        
                        strEncryptExchangeConnection = RegistrySettings.EncryptExchangeConnection();
                        if (!string.IsNullOrEmpty(strEncryptExchangeConnection))
                        {  
                            dictSetregistryentries.Add(UIConstants.strSQLConnect, strEncryptExchangeConnection+ "+" + "REG_SZ");
                           
                        }
                        strEncryptExchangeConnectionHex = RegistrySettings.EncryptExchangeConnectionHex();
                        if (!string.IsNullOrEmpty(strEncryptExchangeConnectionHex))
                        {
                            dictSetregistryentries.Add(UIConstants.strSQLConnectEx, strEncryptExchangeConnectionHex + "+" + "REG_SZ");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Exchange Connection Failed.", UIConstants.strBMCConfig, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    Application.DoEvents();
                    //Ticketing server save 
                    if (ValidateText(txtticketserver, "Server"))
                    {
                        if (ValidateText(txtticketusername, "UserName"))
                        {
                            if (ValidateText(txtticketPassword, "Password"))
                            {
                                if (ValidateText(txtticketTimeout, "Connection Timeout"))
                                {
                                    bTestTicketingConnection = TestConnection(txtticketserver.Text, txtticketusername.Text, txtticketPassword.Text, txtticketTimeout.Text, txticketInstance.Text, 'T');
                                }
                            }
                        }
                    }                   
                    if (bTestTicketingConnection == true)
                    {
                        if (!string.IsNullOrEmpty(ReturnConnectionString))
                        {
                            ExchangeConfigRegistryEntities.TicketingConnectionString = ReturnConnectionString;
                        }
                        if (!string.IsNullOrEmpty(ExchangeConfigRegistryEntities.TicketingConnectionString))
                        {
                           bInsertSetting=DBSettings.InsertSettings(UIConstants.TICKETINGCONNECTIONSETTING, ExchangeConfigRegistryEntities.TicketingConnectionString);
                           
                           if (bInsertSetting==false)
                            {
                                MessageBox.Show("Error in saving Ticketing Connection details.", UIConstants.strBMCConfig, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            if (txtLocCode.Text.Length > 0)
                            {
                                bInsertSetting = DBSettings.InsertSettings(UIConstants.TICKETLOCATIONCODENAME, txtLocCode.Text);
                                if (bInsertSetting == false)
                                {
                                    MessageBox.Show("Error in saving Ticket Location Code.", UIConstants.strBMCConfig, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                                bSetLocCode = DBSettings.SetTicketLocationCode(int.Parse(txtLocCode.Text));
                                if (bSetLocCode == false)
                                {
                                    MessageBox.Show("Error in saving Ticket Location Code in Ticketing database.", UIConstants.strBMCConfig, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                            else
                            {
                                MessageBox.Show("Ticket Location Code is empty.", UIConstants.strBMCConfig, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Ticketing Connection Failed.", UIConstants.strBMCConfig, MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                    Application.DoEvents();
                    //CMP Gateway server save
                    if (ValidateText(txtCMPServer, "Server"))
                    {
                        if (ValidateText(txtCMPUsername, "UserName"))
                        {
                            if (ValidateText(txtCMPPassword, "Password"))
                            {
                                if (ValidateText(txtCMPtimeout, "Connection Timeout"))
                                {
                                    bTestCMPConnection = TestConnection(txtCMPServer.Text, txtCMPUsername.Text, txtCMPPassword.Text, txtCMPtimeout.Text, txtCMPInstance.Text, 'C');
                                }
                            }
                        }
                    }                   
                    if (bTestCMPConnection == true)
                    {
                        if (!string.IsNullOrEmpty(ReturnConnectionString))
                        {
                            ExchangeConfigRegistryEntities.CMPConnectionString = ReturnConnectionString;
                        }
                        if (!string.IsNullOrEmpty(ExchangeConfigRegistryEntities.CMPConnectionString))
                        {
                            bInsertSetting = DBSettings.InsertSettings(UIConstants.CMPCONNECTIONSETTING, ExchangeConfigRegistryEntities.CMPConnectionString);
                            if (bInsertSetting == false)
                            {
                                MessageBox.Show("Error in saving CMP details.", UIConstants.strBMCConfig, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                      }
                    else
                    {
                        MessageBox.Show("CMP Connection Failed.", UIConstants.strBMCConfig, MessageBoxButtons.OK, MessageBoxIcon.Error);
                       
                    }
                    Application.DoEvents();                      
                    //Bind IP address
                    if (cmbLanIP.SelectedItem != null) //&& (cmbLanIP.SelectedItem.ToString() != "--Select--"))
                    {
                        dictSetregistryentries.Add(UIConstants.strBindIP.ToString(), cmbLanIP.SelectedItem.ToString() + "+" + "REG_SZ");
                        dictSetregistryentries.Add(UIConstants.strDefaultServerIP.ToString(), cmbLanIP.SelectedItem.ToString() + "+" + "REG_SZ");
                        dictSetNetLoggerRegistryEntry.Add(UIConstants.strNetLogger.ToString(), cmbLanIP.SelectedItem.ToString() + "+" + "REG_SZ");
                    }
                    else
                    {
                        MessageBox.Show("Error in saving Bind IP details.", UIConstants.strBMCConfig, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    Application.DoEvents();

                    //DHCP Server settings
                    if (chkEnableDHCP.Checked == true)
                    {
                        iEnableDHCP = 1;                      
                    }
                    else
                    {
                        iEnableDHCP = 0;
                      
                    }
                    if (cmbSlotLan.SelectedItem != null)// && (cmbSlotLan.SelectedItem.ToString() != "--Select--"))
                    {
                        strSlotLanIPAddress = cmbSlotLan.SelectedItem.ToString();
                        dictSetregistryentries.Add(UIConstants.strDHCPServerIP.ToString(), strSlotLanIPAddress.ToString() + "+" + "REG_SZ");
                        dictSetregistryentries.Add(UIConstants.strDHCPEnabled.ToString(), iEnableDHCP.ToString() + "+" + "REG_DWORD");
                        dictSetregistryentries.Add(UIConstants.strMulticastip.ToString(), txtMultiCastIP.Text.ToString().Trim() + "+" + "REG_SZ");
                        dictSetregistryentries.Add(UIConstants.strInterface.ToString(), txtInterfaceIP.Text.ToString().Trim() + "+" + "REG_SZ");
                    }
                    else
                    {
                        MessageBox.Show("Error in saving Slot LAN details.", UIConstants.strBMCConfig, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    Application.DoEvents();

                    //Encrypt enable settings                    
                    if (chkEnableEncrypt.Checked == false)
                        if (MessageBox.Show("Do you want to check Enable encrypt ?", UIConstants.strBMCConfig, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            chkEnableEncrypt.Checked = true;
                    if (chkEnableEncrypt.Checked == true)
                        iEnableEncrypt = 1;
                    else
                        iEnableEncrypt = 0;

                    dictSetregistryentries.Add(UIConstants.strEncryptEnable, iEnableEncrypt.ToString() + "+" + "REG_DWORD");
                    Application.DoEvents();
                    //Encrypt enable settings                    
                    if (chkEnableRSAEncrypt.Checked == false)
                        if (MessageBox.Show("Do you want to check Enable encrypt RSA ?", UIConstants.strBMCConfig, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            chkEnableRSAEncrypt.Checked = true;
                    if (chkEnableRSAEncrypt.Checked == true)
                        iEnableEncrypt = 1;
                    else
                        iEnableEncrypt = 0;

                    dictSetregistryentries.Add(UIConstants.strEnableRSAEncrypt, iEnableEncrypt.ToString() + "+" + "REG_DWORD");

                    //machine disable settings                    
                    if (chkDisableMachine.Checked == false)
                        if (MessageBox.Show("Do you want to check Machine Disable on uninstallation", UIConstants.strBMCConfig, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            chkDisableMachine.Checked = true;
                    if (chkDisableMachine.Checked == true)
                        iDisableMachine = 1;
                    else
                        iDisableMachine = 0;

                    dictSetregistryentries.Add(UIConstants.strDisablemachine, iDisableMachine.ToString() + "+" + "REG_DWORD");
                    Application.DoEvents();

                    //Web service                     
                    if ((txtEnterpriseweburl.Text.Trim().Length) > 0 )
                        if (strUrlvalidate==string.Empty)//If test functionality not used 
                            dictSetregistryentries.Add(UIConstants.strBGSWebservice.ToString(), txtEnterpriseweburl.Text.Trim() + "+" + "REG_SZ");                       
                        else
                            dictSetregistryentries.Add(UIConstants.strBGSWebservice.ToString(), strUrlvalidate + "+" + "REG_SZ");                       
                    else                                           
                        MessageBox.Show("Error in saving Enterprise Web Service details.", UIConstants.strBMCConfig, MessageBoxButtons.OK, MessageBoxIcon.Error);                    
                    

                    //DSN settings
                    //ExchangeConfigRegistryEntities.ODBCRegKeyValue = UIConstants.strODBCRegPath;
                    //ReadServicesSettings.DSNSettings(lblexchangeDBname.Text.ToString(), txtexchangeInstance.Text);
                    //Application.DoEvents();
                                       

                    //Save all Registry Settings under cash master
                    RegistrySettings.SetRegistryEntries(dictSetregistryentries,ExchangeConfigRegistryEntities.RegistryKeyValue);
                    Application.DoEvents();

                    //Save all Registry Settings under NetLogger                    
                    RegistrySettings.SetRegistryEntries(dictSetNetLoggerRegistryEntry, ExchangeConfigRegistryEntities.NetLoggerRegKeyValue);
                    Application.DoEvents();

                    //Save System settings for settings DB
                    foreach (PropertyClass pc in ChangedProperty)
                        DataBaseServiceHandler.ExecuteNonQuery(QueryType.Text, "Update Setting Set Setting_Value = '" + pc.Value.Trim() + "' Where Setting_Name = '" + pc.Name.Trim() + "'");

                    ChangedProperty = new List<PropertyClass>();
                    Application.DoEvents();
                    MessageBox.Show("Please  start/restart windows services once db connection updated.", UIConstants.strBMCConfig, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    MessageBox.Show("Saved Settings Successfully.", UIConstants.strBMCConfig, MessageBoxButtons.OK,MessageBoxIcon.Information);
                    //sStrip.Text = "Done.";                    
                    //tSStatus.Text = "Done";
                    GetSettingsInfo();  
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
        /// <summary>
        /// Validations done for the IP address
        /// </summary>
        /// <param name="strCheckIP"></param>        
           
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

                frmSQLRestore frmRestoreDB = new frmSQLRestore("Ticketing");
                frmRestoreDB.ShowDialog();
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("btnTicketDBRestore_Click" + ex.Message.ToString() + ex.Source.ToString(), LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
            }
        }
        private void btnCMPGatewaySaveChanges_Click(object sender, EventArgs e)
        {
            try
            {
                frmSQLRestore frmRestoreDB = new frmSQLRestore("CMktSDG");
                frmRestoreDB.ShowDialog();
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("btnCMPGatewaySaveChanges_Click" + ex.Message.ToString() + ex.Source.ToString(), LogManager.enumLogLevel.Error);
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
        private void cmbSlotLan_TabIndexChanged(object sender, EventArgs e)
        {
            //if (e.KeyChar == Convert.ToChar(Keys.Enter))
            //{
            try
            {
                if (!string.IsNullOrEmpty(cmbSlotLan.Text.ToString()))
                {
                    if (cmbSlotLan.Text.CompareTo(cmbSlotLan.SelectedText.ToString()) > 0)
                    {
                        if (!ValidateIP(cmbSlotLan.Text.ToString()) == true)
                        {
                            //tSStatus.Text = "Please enter only numerics between 0-255";
                            MessageBox.Show("Please enter only numerics between 0-255", UIConstants.strBMCConfig, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            cmbSlotLan.Text = "";
                        }
                        else
                        {
                            cmbSlotLan.Items.Add(cmbSlotLan.Text.ToString());
                            return;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("cmbSlotLan_TabIndexChanged" + ex.Message.ToString() + ex.Source.ToString(), LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
            }
        }
        private void cmbSlotLan_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == Convert.ToChar(Keys.Enter))
                {
                    if (!string.IsNullOrEmpty(cmbSlotLan.Text.ToString()))
                    {
                        if (CompareIP(cmbSlotLan.Text.ToString()))
                        {
                            if (!ValidateIP(cmbSlotLan.Text.ToString()) == true)
                            {
                                //tSStatus.Text = "Please enter only numerics between 0-255";
                                MessageBox.Show("Please enter only numerics between 0-255", UIConstants.strBMCConfig, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                cmbSlotLan.Text = "";
                            }
                            else
                            {
                                cmbSlotLan.Items.Add(cmbSlotLan.Text.ToString());                              
                                return;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("cmbSlotLan_KeyPress" + ex.Message.ToString() + ex.Source.ToString(), LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
            }
        }
        /// <summary>
        /// CompareIP with the combo box
        /// </summary>
        /// <param name="strNewText"></param>                                           
        private bool CompareIP(string strNewText)
        {
            bool bReturn = false;
            try
            {
                for (int i = 0; i < cmbSlotLan.Items.Count; i++)
                {
                    if (cmbSlotLan.Items[i] != null && cmbSlotLan.Items[i].ToString() != string.Empty)
                    {                    
                        if (strNewText != cmbSlotLan.Items[i].ToString())
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
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("CompareIP" + ex.Message.ToString() + ex.Source.ToString(), LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
            }
            return bReturn;

        }
        private void frmBMCExchangeConfig_FormClosed(object sender, FormClosedEventArgs e)
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
        private void frmBMCExchangeConfig_FormClosing(object sender, FormClosingEventArgs e)
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
                //if (ValidateText(txtexchangeServer, "Server"))
                //{
                //    if (ValidateText(txtexchangeUsername, "UserName"))
                //    {
                //        if (ValidateText(txtexchangePassword, "Password"))
                //        {
                //            if (ValidateText(txtexchangeTimeOut, "Connection Timeout"))
                //            {
                //                bTestConnection = TestConnection(txtexchangeServer.Text, txtexchangeUsername.Text, txtexchangePassword.Text, txtexchangeTimeOut.Text, txtexchangeInstance.Text, 'E');
                //            }
                //        }
                //    }
                //}                
                //if (bTestConnection == false)                
                //{
                //    //tSStatus.Text = "Connection to Exchange Database Failed..Please fix it first and upgrade";
                //    MessageBox.Show("Connection to Exchange Database Failed..Please fix it first and upgrade", UIConstants.strBMCConfig, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                     psi.FileName = startuppath + "\\Exchange Server\\DeployScripts.exe";
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
        private void btnSaveExchangeConnection_Click(object sender, EventArgs e)
        {
            bool bTestExchangeConnection = false;
            Dictionary<string,string> dictSetregistryentries;
            string strEncryptExchangeConnection = string.Empty;
            string strEncryptExchangeConnectionHex = string.Empty;
            try
            {
                //tSStatus.Text = "Processing...";
                dictSetregistryentries = new Dictionary<string, string>();
                Cursor = Cursors.WaitCursor;
                //Exchange Server save 
                if (ValidateText(txtexchangeServer, "Server"))
                {
                    if (ValidateText(txtexchangeUsername, "UserName"))
                    {
                        if (ValidateText(txtexchangePassword, "Password"))
                        {
                            if (ValidateText(txtexchangeTimeOut, "Connection Timeout"))
                            {
                                bTestExchangeConnection = TestConnection(txtexchangeServer.Text, txtexchangeUsername.Text, txtexchangePassword.Text, txtexchangeTimeOut.Text, txtexchangeInstance.Text, 'E');
                            }
                        }
                    }
                }                
                if (bTestExchangeConnection == true)
                {
                    if (!string.IsNullOrEmpty(ReturnConnectionString))
                        ExchangeConfigRegistryEntities.ExchangeConnectionString = ReturnConnectionString;
                    strEncryptExchangeConnection = RegistrySettings.EncryptExchangeConnection();
                    if (!string.IsNullOrEmpty(strEncryptExchangeConnection))
                    {
                        dictSetregistryentries.Add(UIConstants.strSQLConnect, strEncryptExchangeConnection + "+" + "REG_SZ");
                        strEncryptExchangeConnectionHex = RegistrySettings.EncryptExchangeConnectionHex();
                        if (!string.IsNullOrEmpty(strEncryptExchangeConnectionHex))
                        {
                            dictSetregistryentries.Add(UIConstants.strSQLConnectEx, strEncryptExchangeConnectionHex + "+" + "REG_SZ");
                        }                        
                        //Save all Registry Settings under cash master
                        RegistrySettings.SetRegistryEntries(dictSetregistryentries, ExchangeConfigRegistryEntities.RegistryKeyValue);
                        Application.DoEvents();
                        MessageBox.Show("Exchange Connection Saved Successfully.", UIConstants.strBMCConfig, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //tSStatus.Text = "Done.";                        
                        //GetSettingsInfo();
                        GetSettings();
                        GetServiceStatusToListView();
                        RefreshControls();
                    }
                }
                else
                {
                    MessageBox.Show("Connection to Exchange Database Failed.", UIConstants.strBMCConfig, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    btnSaveSettings.Enabled = true;
                    //tSStatus.Text = "Connection failed.";
                }
                Application.DoEvents();
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("btnSaveExchangeConnection_Click" + ex.Message.ToString() + ex.Source.ToString(), LogManager.enumLogLevel.Error);
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

        private void chkEnableDHCP_CheckedChanged(object sender, EventArgs e)
        {
            RefreshDHCPControls();
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

        private void btnDSNSave_Click(object sender, EventArgs e)
        {          

            frmSetupODBCDsn ofrmSetupODBCDsn =new frmSetupODBCDsn(txtexchangeServer.Text, txtexchangeUsername.Text, txtexchangePassword.Text);
            ofrmSetupODBCDsn.ShowDialog();
        }
                   

        private void txtInterfaceIP_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (!((e.KeyCode >= Keys.NumPad0 && e.KeyCode <= Keys.NumPad9) ||
                    ((e.KeyCode >= Keys.D0 && e.KeyCode <= Keys.D9) && (!e.Shift)) ||
                    e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete ||
                    e.KeyCode == Keys.Decimal || e.KeyCode == Keys.Enter || e.KeyCode == Keys.OemPeriod))

                    e.SuppressKeyPress = true;                
                
            }
            catch(Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void txtInterfaceIP_Leave(object sender, EventArgs e)
        {
            try{
                if (txtInterfaceIP.Text.ToString() == string.Empty)
                    return;
            if (!ValidateIP(txtInterfaceIP.Text.ToString()) == true)
            {
                txtInterfaceIP.Text = "";
                MessageBox.Show("Please enter only numerics between 0-255", UIConstants.strBMCConfig, MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtInterfaceIP.Focus();
            }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void txtMultiCastIP_Leave(object sender, EventArgs e)
        {
            try{
                if (txtMultiCastIP.Text.ToString() == string.Empty)
                    return;
            if (!ValidateIP(txtMultiCastIP.Text.ToString()) == true)
            {
                txtMultiCastIP.Text = "";
                MessageBox.Show("Please enter only numerics between 0-255", UIConstants.strBMCConfig, MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtMultiCastIP.Focus();
            }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void txtMultiCastIP_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (!((e.KeyCode >= Keys.NumPad0 && e.KeyCode <= Keys.NumPad9) ||
                    ((e.KeyCode >= Keys.D0 && e.KeyCode <= Keys.D9) && (!e.Shift)) ||
                    e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete ||
                    e.KeyCode == Keys.Decimal || e.KeyCode == Keys.Enter || e.KeyCode == Keys.OemPeriod))

                    e.SuppressKeyPress = true;

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void txtexchangeTimeOut_KeyPress(object sender, KeyPressEventArgs e)
        {            
            if (!(e.KeyChar >= 48 && e.KeyChar <= 57))            
                e.Handled = true;            

        }

        private void txtticketTimeout_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(e.KeyChar >= 48 && e.KeyChar <= 57))
                e.Handled = true;        

        }

        private void txtCMPtimeout_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(e.KeyChar >= 48 && e.KeyChar <= 57))
                e.Handled = true;        
        }

        private void chkTrusted_CheckedChanged(object sender, EventArgs e)
        {
            XmlDocument xDom;
            XmlNode myNode;
            string executableName;
            FileInfo executableFileInfo=null;
            try
            {
              xDom = new XmlDocument();
              var regkey = Registry.LocalMachine.OpenSubKey(UIConstants.StartUpPath);
              if (regkey != null) 
                  executableName = regkey.GetValue("InstallationPath").ToString();
              else             
                  executableName = Application.StartupPath;

                executableFileInfo = new FileInfo(executableName + "\\BMC Exchange Config.exe.config");
                xDom.Load(executableFileInfo.FullName);
                myNode = xDom.DocumentElement.SelectSingleNode("/configuration/appSettings");
                if (myNode != null)
                {
                    foreach (XmlNode oNode in myNode.ChildNodes)
                    {
                        if (oNode.Attributes["key"].Value.ToString().ToUpper() == "PROTOCOL")                                                 
                            oNode.Attributes["value"].Value = chkTrusted.Checked?"https://":"http://";
                                 
                    }
                    xDom.Save(executableFileInfo.FullName);
           
                }
            }
            catch (Exception ex)
            {
               ExceptionManager.Publish(ex);
            }
        }   

       void LoadConfig()
        {
            XmlDocument xDom;
            XmlNode myNode;
            string executableName;
            FileInfo executableFileInfo = null;
            try
            {
                xDom = new XmlDocument();
                var regkey = Registry.LocalMachine.OpenSubKey(UIConstants.StartUpPath);
                if (regkey != null)
                    executableName = regkey.GetValue("InstallationPath").ToString();
                else
                    executableName = Application.StartupPath;

                executableFileInfo = new FileInfo(executableName + "\\BMC Exchange Config.exe.config");
                xDom.Load(executableFileInfo.FullName);
                myNode = xDom.DocumentElement.SelectSingleNode("/configuration/appSettings");
                if (myNode != null)
                {
                    foreach (XmlNode oNode in myNode.ChildNodes)
                    {
                        if (oNode.Attributes["key"].Value.ToString().ToUpper() == "PROTOCOL")
                             sProtocol = oNode.Attributes["value"].Value;
                      
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
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