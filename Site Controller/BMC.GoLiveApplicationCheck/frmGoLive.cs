using System;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;
using BMC.Common.ConfigurationManagement;
using BMC.Common.ExceptionManagement;
using BMC.Common.LogManagement;
using System.Data.SqlClient;
using BMC.DataAccess;
using System.Net;
using BMC.Monitoring;
using System.Messaging;
using System.Net.NetworkInformation;
using System.Data.OleDb;
using BMC.Common.Utilities;

namespace BMC_GoLiveApplication_Check
{
    public partial class frmGoLive : Form
    {
        public frmGoLive()
        {
            
            InitializeComponent();
        }
        public ImageList objImageList;
        private string strTicketingConnection = string.Empty;
        private string strCMPConnection = string.Empty;
        private const string CONST_MESSAGE_HEADER = "GO LIVE test Application";

        private static bool blnOpened = false;
        public static bool GMUFormOpened
        {
            get { return blnOpened; }
            set { blnOpened = value; }
        }

        private void frmGoLive_Load(object sender, EventArgs e)
        {
            try
            {
                //Step 1. Initialize the List View
                InitializeGoLiveListView();

                this.Cursor = Cursors.WaitCursor;
                //Step 2. Check for Exchange Database Connectivity
                CheckExchangeDBConnectivity();

                //Step 3. Check for Ticketing Database Connectivity
                CheckTicketingDBConnectivity();

                //Step 4. Check for CMP Database Connectivity
                CheckCMPDBConnectivity();

                //Step 5. Check for Bind IP and Slot LAN IP
                CheckforBindIP();
                CheckForSlotLANIP();

                //Step 6. Check DHCP Setting
                CheckDHCPSetting();

                //Step 7. Check MSMQ Exists
                CheckMSMQExists();

                //Step 8. Check All services are running
                GetServiceStatus();

                //Step 9. Check Enteprise Connectivity
                CheckEntepriseConnectivity();

                //Step 10. Check Exchange Connectivity
                CheckExchangeWSConnectivity();

                //step11: check GMU status
                CheckGMUStatus();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                MessageBox.Show("Unable to get the details at the moment. Please try again later.", CONST_MESSAGE_HEADER);
            }
            finally
            {
                this.Cursor = Cursors.Arrow;
            }

        }
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            frmGoLive_Load(sender, e);
        }

        private void InitializeGoLiveListView()
        {

            lvGoLive.View = View.Details;
            lvGoLive.Clear();

            objImageList = new ImageList();
            objImageList.ImageSize = new Size(24, 24);
            objImageList.Images.Add(Properties.Resources.Success  );
            objImageList.Images.Add(Properties.Resources.Failure);
            lvGoLive.SmallImageList = objImageList;
            
            lvGoLive.FullRowSelect = true;
            lvGoLive.GridLines = true;
            lvGoLive.Columns.Add("");
            lvGoLive.Columns.Add("Installation Feature");
            lvGoLive.Columns.Add("Remarks");
            lvGoLive.Columns[0].Width = 28;
            lvGoLive.Columns[1].Width = 280;
            lvGoLive.Columns[2].Width = 1200;

            AddItemsToListView("ExchangeDBConnectivity", "Exchange Database Connectivity");
            AddItemsToListView("TicketingDBConnectivity", "Ticketing Database Connectivity");
            AddItemsToListView("CMPDBConnectivity", "CMP Database Connectivity");
            AddItemsToListView("BindIP", "Bind IP");
            AddItemsToListView("SlotLANIP", "Slot LAN IP");
            AddItemsToListView("DHCP", "DHCP Setting in Registry");
            AddItemsToListView("MSMQ", "MSMQ");
            AddItemsToListView("Services", "Services");
            AddItemsToListView("Services1", "");
            AddItemsToListView("EnterpriseConnectivity", "Enterprise Webservice Connectivity");
            AddItemsToListView("ExchangeWebserviceConnectivity", "Exchange Webservice Connectivity");
            AddItemsToListView("GMUStatus", "GMU Status");
            //AddItemsToListView("DBCompare", "DB Compare");
        }


        #region Utility Functions
        private void AddItemsToListView(string strName, string strTexttoDisplay)
        {
            ListViewItem objItem = new ListViewItem();
            objItem.SubItems.Add(strTexttoDisplay);
            objItem.Name = strName;
            lvGoLive.Items.Add(objItem);
        }
        private void SetListviewItemStyle(string strItemName, Int32 iImageIndex)
        {
            if (iImageIndex!=-1)
            {
                lvGoLive.Items[strItemName].ImageIndex = iImageIndex;    
            }
            switch (iImageIndex)
            { 
                case 0:
                    lvGoLive.Items[strItemName].ForeColor = Color.Black;
                    lvGoLive.Items[strItemName].BackColor = Color.White;
                    return;

                case 1:
                    lvGoLive.Items[strItemName].ForeColor = Color.White ;
                    lvGoLive.Items[strItemName].BackColor = Color.Red;
                    return;
                case -1:
                    lvGoLive.Items[strItemName].ForeColor = Color.White;
                    lvGoLive.Items[strItemName].BackColor = Color.Red;
                    return;
            }
        }
        #endregion Utility Functions

        private bool CheckDatabaseConnectivity(string strConnectionString)
        {
            SqlConnection objSQLConnection = null;
            try
            {
                objSQLConnection = new SqlConnection();
                objSQLConnection.ConnectionString = strConnectionString;
                objSQLConnection.Open();
                if (objSQLConnection.State == ConnectionState.Open)
                {
                    if (strConnectionString.IndexOf("Exchange") >= 0)
                    {
                        strTicketingConnection = DatabaseHelper.GetTicketingConnectionString();//SqlHelper.ExecuteScalar(objSQLConnection, CommandType.Text, "Select Setting_Value from Setting where Setting_Name ='Ticketing.Connection'").ToString();
                        strCMPConnection = SqlHelper.ExecuteScalar(objSQLConnection, CommandType.Text, "Select Setting_Value from Setting where Setting_Name ='EPIGatewayConnstr'").ToString();
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return false;
            }
            finally
            {
                objSQLConnection.Close();
            }
        }

        private string GetExchangeConnectionString()
        {
            return DatabaseHelper.GetExchangeConnectionString();
        }

        public string[] GetAllLocalIP()
        {
            string[] strIPAddressList = null;
            IPHostEntry objIPEntry = null;
            IPAddress[] objarrIPAddress = null;
            try
            {
                objIPEntry = Dns.GetHostEntry(Dns.GetHostName());

                if (objIPEntry != null)
                {
                    objarrIPAddress = objIPEntry.AddressList;
                }

                if (objarrIPAddress != null)
                {
                    if (objarrIPAddress.Length > 0)
                    {
                        strIPAddressList = new string[objarrIPAddress.Length];
                        strIPAddressList.Initialize();
                        for (int i = 0; i < objarrIPAddress.Length; i++)
                        {
                            strIPAddressList.SetValue(objarrIPAddress[i].ToString(), i);
                        }
                    }
                    else
                    {
                        strIPAddressList = null;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
              return new string[0];
            }
            return strIPAddressList;
        }

 

        #region GoLiveApplication Feature Verifying Functions

        private bool CheckExchangeDBConnectivity()
        {
            string strExchangeConnectionString = string.Empty;
            try
            {
                strExchangeConnectionString = GetExchangeConnectionString();

                if (!CheckDatabaseConnectivity(strExchangeConnectionString))
                {
                    SetListviewItemStyle("ExchangeDBConnectivity", 1);
                    return false;
                }
                else
                {
                    SetListviewItemStyle("ExchangeDBConnectivity", 0);
                    return true;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                SetListviewItemStyle("ExchangeDBConnectivity", 1);
                return false;
            }
        }

        private void CheckTicketingDBConnectivity()
        {
            
            try
            {
                if ((string.IsNullOrEmpty(strTicketingConnection) || (!CheckDatabaseConnectivity(strTicketingConnection))))
                {
                    SetListviewItemStyle("TicketingDBConnectivity", 1);
                }
                else
                {
                    SetListviewItemStyle("TicketingDBConnectivity", 0);
                }
            }
            catch (Exception ex)
            {
                SetListviewItemStyle("TicketingDBConnectivity", 1);
                ExceptionManager.Publish(ex);
            }
        }

        private void CheckCMPDBConnectivity()
        {
            try
            {
                if ((string.IsNullOrEmpty(strCMPConnection) || (!CheckDatabaseConnectivity(strCMPConnection))))
                {
                    SetListviewItemStyle("CMPDBConnectivity", 1);
                }
                else
                {
                    SetListviewItemStyle("CMPDBConnectivity", 0);
                }
            }
            catch (Exception ex)
            {
                SetListviewItemStyle("CMPDBConnectivity", 1);
                ExceptionManager.Publish(ex);
            }
        }

        private void CheckForSlotLANIP()
        {
            string strSlotIP = string.Empty;
            try
            {
                strSlotIP = BMCRegistryHelper.GetRegKeyValue(ConfigManager.Read("RegistryPath") + "\\BMCDHCP", "ServerIP");
                if (string.IsNullOrEmpty(strSlotIP))
                {
                    SetListviewItemStyle("SlotLANIP", 1);  
                    return;
                }
                string[] strarrBindIP = GetAllLocalIP();
                if (strarrBindIP.Length >0)
                {
                    foreach (string strIP in strarrBindIP)
                    {
                        if (strIP.ToUpper().CompareTo(strSlotIP.ToUpper()) == 0)
                        {
                            lvGoLive.Items["SlotLANIP"].SubItems.Add(strIP);
                            SetListviewItemStyle("SlotLANIP", 0);  
                            return;
                        }
                    }
                    SetListviewItemStyle("SlotLANIP", 1);  
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                SetListviewItemStyle("SlotLANIP", 1);  
            }
        }

        private void CheckforBindIP()
        {
            string strBindIP = string.Empty;
            try
            {
                strBindIP = BMCRegistryHelper.GetRegKeyValue(ConfigManager.Read("RegistryPath") + "\\Exchange", "BindIPAddress");
                string[] strarrBindIP = GetAllLocalIP();
                if (string.IsNullOrEmpty(strBindIP))
                {
                    SetListviewItemStyle("BindIP", 1);
                }
                if (strarrBindIP.Length >0 )
                {
                    foreach (string strIP in strarrBindIP)
                    {
                        if (strIP == strBindIP)
                        {
                            lvGoLive.Items["BindIP"].SubItems.Add(strIP);
                            SetListviewItemStyle("BindIP", 0);
                            break;
                        }
                        else
                        {
                            SetListviewItemStyle("BindIP", 1);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                SetListviewItemStyle("BindIP", 1);
            }
        }

        private void CheckDHCPSetting()
        {
            RegistryKey objDHCPSetting = null;
            string strDHCPSettingValue = string.Empty;
            try
            {
                objDHCPSetting = BMCRegistryHelper.GetRegLocalMachine().OpenSubKey(ConfigManager.Read("RegistryPath") + "\\Exchange");
                strDHCPSettingValue = objDHCPSetting.GetValue("EnableDhcp").ToString();
                if (strDHCPSettingValue.Trim() == "1")
                {
                    SetListviewItemStyle("DHCP", 0);
                }
                else
                {
                    SetListviewItemStyle("DHCP", 1);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                SetListviewItemStyle("DHCP", 1);
            }
        }

        public void CheckMSMQExists()
        {
            string strMSMQExchangepath = ".\\private$\\Exchangequeue";
            try
            {
                if (MessageQueue.Exists(strMSMQExchangepath))
                {
                   
                    SetListviewItemStyle("MSMQ", 0);
                }
                else
                {
                    SetListviewItemStyle("MSMQ", 1);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                SetListviewItemStyle("MSMQ", 1);
            }
        }

        private void GetServiceStatus()
        {
            BMCMonitoring objBMCMonitoring = new BMCMonitoring();
            DataTable dtServicesStatus = new DataTable();
            StringBuilder strServicelist = new StringBuilder();
            //Rectangle itemBoundRectangle = new Rectangle(0, 0, 0, 0);
            ListViewItem itemListview = new ListViewItem();
            string[] strListarray = null;
            try
            {
                strListarray = ConfigManager.Read("ServicesList").ToString().Split(',');
                if (strListarray != null)
                {
                    for (int i = 0; i < strListarray.Length; i++)
                    {
                        strServicelist.Append(strListarray[i].ToString() + ",");
                    }
                }
                dtServicesStatus = objBMCMonitoring.GetServiceStatus(strServicelist.ToString(), BMCMonitoring.ServiceTypes.All);
                string strServiceStatus = string.Empty;
                string strServiceStatus1 = string.Empty;
                bool blnFoundNotRunning = false;
                int ServiceCount = dtServicesStatus.Rows.Count / 2;
                int RowCount = 0;
                if (dtServicesStatus.Rows.Count > 0)
                {
                    SetListviewItemStyle("Services", 0);
                    foreach (DataRow dr in dtServicesStatus.Rows)
                    {
                        if (!blnFoundNotRunning && dr["Status"].ToString().ToUpper().Trim() !="RUNNING")
                        {
                            SetListviewItemStyle("Services",1);
                            SetListviewItemStyle("Services1", -1);
                            blnFoundNotRunning = true;
                        }
                        if (RowCount > ServiceCount)
                            strServiceStatus1 += dr["ServiceName"].ToString() + ":" + dr["Status"].ToString() + "; ";    
                        else
                            strServiceStatus+= dr["ServiceName"].ToString()+":" +dr["Status"].ToString()+"; ";
                        RowCount++;
                    }

                    if (lvGoLive.Items["Services"].SubItems.Count > 2)
                        lvGoLive.Items["Services"].SubItems[2].Text = strServiceStatus;
                    else
                        lvGoLive.Items["Services"].SubItems.Add(strServiceStatus);
                    if (!string.IsNullOrEmpty(strServiceStatus1))
                    {
                        if (lvGoLive.Items["Services1"].SubItems.Count > 2)
                            lvGoLive.Items["Services1"].SubItems[2].Text = strServiceStatus1;
                        else
                            lvGoLive.Items["Services1"].SubItems.Add(strServiceStatus1);
                    }
                    else
                    {
                        lvGoLive.Items["Services1"].Remove();
                    }
                    
                }
                #region unused code
                //if (dtServicesStatus.Rows.Count > 0)
                //{
                //    for (int j = 0; j < dtServicesStatus.Columns.Count; j++)
                //    {
                //        for (int i = 0; i < dtServicesStatus.Rows.Count; i++)
                //        {
                //            if (j != dtServicesStatus.Columns.Count - 1)
                //            {
                //                if (dtServicesStatus.Rows[i][j + 1].ToString() == "Stopped")
                //                {
                //                    lvGoLive.Items["Services"].ImageIndex = 1;
                           
                //                    if (lvGoLive.Items["Services"].SubItems.Count > 2)
                //                    {
                //                        lvGoLive.Items["Services"].SubItems[2].Text = lvGoLive.Items["Services"].SubItems[2].Text + ";" + dtServicesStatus.Rows[i][j].ToString() +" Service is in Stopped State";
                //                    }
                //                    else
                //                    {
                //                        lvGoLive.Items["Services"].SubItems.Add(dtServicesStatus.Rows[i][j].ToString() + " Service is in Stopped State");

                //                    }
                //                }
                //                 else if (dtServicesStatus.Rows[i][j + 1].ToString() == "Pending")
                //                {
                //                    lvGoLive.Items["Services"].ImageIndex = 1;

                //                    if (lvGoLive.Items["Services"].SubItems.Count > 2)
                //                    {
                //                        lvGoLive.Items["Services"].SubItems[2].Text = lvGoLive.Items["Services"].SubItems[2].Text + ";" + dtServicesStatus.Rows[i][j].ToString() + " Service is in Pending State";
                //                    }
                //                    else
                //                    {
                //                        lvGoLive.Items["Services"].SubItems.Add(dtServicesStatus.Rows[i][j].ToString() + " Service is in Pending State");

                //                    }

                //                }

                //                else if (dtServicesStatus.Rows[i][j + 1].ToString().ToUpper() == "Service not found".ToUpper())
                //                {
                //                    lvGoLive.Items["Services"].ImageIndex = 1;
                //                    if (lvGoLive.Items["Services"].SubItems.Count > 2)
                //                    {
                //                        lvGoLive.Items["Services"].SubItems[2].Text = lvGoLive.Items["Services"].SubItems[2].Text + ";" + dtServicesStatus.Rows[i][j].ToString() + " Service not found.";
                //                    }
                //                    else
                //                    {
                //                        lvGoLive.Items["Services"].SubItems.Add(dtServicesStatus.Rows[i][j].ToString() + " Service not found");

                //                    }


                //                }

                //            }
                //            else
                //            {
                //                break;
                //            }
                //        }
                //    }
                //}

                //lvGoLive.AutoResizeColumn(2, ColumnHeaderAutoResizeStyle.ColumnContent);
                //if (lvGoLive.Items["Services"].ImageIndex != 1)
                //{
                //    lvGoLive.Items["Services"].ImageIndex = 0;
                //    lvGoLive.Items["Services"].BackColor = Color.White;
                //    lvGoLive.Items["Services"].ForeColor = Color.Black;
                    
                //}
                //else
                //{
                //    lvGoLive.Items["Services"].BackColor = Color.Red;
                //    lvGoLive.Items["Services"].ForeColor = Color.White;
                //}
                #endregion
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);

               
                SetListviewItemStyle("Services", 1);
            }
        }

        public void CheckEntepriseConnectivity()
        {
            string strWebServiceURL = string.Empty;
            try
            {
                strWebServiceURL = BMCRegistryHelper.GetRegKeyValue(ConfigManager.Read("RegistryPath"), "BGSWebService");
                if (!string.IsNullOrEmpty(strWebServiceURL))
                {
                    EnterpriseWebService.EnterpriseWebService objBGSWebProxy = new EnterpriseWebService.EnterpriseWebService(strWebServiceURL);
                    if (objBGSWebProxy.HelloWebService(10) == 10)
                    {
                        SetListviewItemStyle("EnterpriseConnectivity", 0);
                    }
                    else
                    {
                        SetListviewItemStyle("EnterpriseConnectivity", 1);
                    }
                }
                else
                {
                    SetListviewItemStyle("EnterpriseConnectivity", 1);
                    if (lvGoLive.Items["EnterpriseConnectivity"].SubItems.Count > 2)
                        lvGoLive.Items["EnterpriseConnectivity"].SubItems[2].Text = "BGSWebServiceURL Registry Key is Empty";
                    else
                        lvGoLive.Items["EnterpriseConnectivity"].SubItems.Add("BGSWebServiceURL Registry Key is Empty");
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                SetListviewItemStyle("EnterpriseConnectivity", 1);
            }
            
        }

        public void CheckExchangeWSConnectivity()
        {
            string strWebServiceURL = string.Empty;
            try
            {
                strWebServiceURL = ConfigManager.Read("ExchangeWebServicePath");
                if (!string.IsNullOrEmpty(strWebServiceURL))
                {
                    ExchangeWebService.ExchangeWebService objBGSWebProxy = new ExchangeWebService.ExchangeWebService(strWebServiceURL);
                    if (objBGSWebProxy.HelloWebService(101)==101)
                    {
                        SetListviewItemStyle("ExchangeWebserviceConnectivity", 0);
                    }
                    else
                    {
                        SetListviewItemStyle("ExchangeWebserviceConnectivity", 1);
                    }
                }
                else
                {
                    SetListviewItemStyle("ExchangeWebserviceConnectivity", 1);
                    if (lvGoLive.Items["ExchangeWebserviceConnectivity"].SubItems.Count > 2)
                        lvGoLive.Items["ExchangeWebserviceConnectivity"].SubItems[2].Text = "Exchange Webservice URL not found in Config";
                    else
                        lvGoLive.Items["ExchangeWebserviceConnectivity"].SubItems.Add("Exchange Webservice URL not found in Config");
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                SetListviewItemStyle("ExchangeWebserviceConnectivity", 1);
            }
        }

        private void CheckGMUStatus()
        {
            try
            {
                string strIP = string.Empty;
                string strExchangeConnectionString = GetExchangeConnectionString();
                if (CheckExchangeDBConnectivity())
                {
                    DataSet objDs = SqlHelper.ExecuteDataset(strExchangeConnectionString, CommandType.StoredProcedure, "rsp_FetchGMULogin");
                    DataTable dtGMUs;
                    if (objDs.Tables.Count > 0)
                    {
                        dtGMUs = objDs.Tables[0];
                        foreach (DataRow drRow in dtGMUs.Rows)
                        {
                            strIP = drRow[2].ToString().Trim();
                            if (!PingIP(strIP))
                            {
                                SetListviewItemStyle("GMUStatus", 1);
                                return;
                            }
                        }
                        SetListviewItemStyle("GMUStatus", 0);
                    }
                    else
                    {
                        SetListviewItemStyle("GMUStatus", 1);
                    }
                }
                else
                {
                    SetListviewItemStyle("GMUStatus", 1);
                    if (lvGoLive.Items["GMUStatus"].SubItems.Count > 2)
                        lvGoLive.Items["GMUStatus"].SubItems[2].Text = "Unable to connect to Exchange DB to get GMU status";
                    else
                        lvGoLive.Items["GMUStatus"].SubItems.Add("Unable to connect to Exchange DB to get GMU status");
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                SetListviewItemStyle("GMUStatus", 1);
            }
        }

        /// <summary>
        /// Pings the IP
        /// </summary>
        /// <param name="strIP"></param>
        /// <returns>boolean value</returns>
        private bool PingIP(String strIP)
        {
            Ping ping = new Ping();
            PingReply reply;
            try
            {
                reply = ping.Send(strIP, 1000);
                return (reply.Status == IPStatus.Success ? true : false);
                //if (reply.Status == IPStatus.Success)
                //{
                //    return (true);
                //}
                //else return false;
            }
            catch 
            {
                return (false);
            }
            
        }
        # endregion

        private void btnGMUStatus_Click(object sender, EventArgs e)
        {
            try 
	        {
                if (!GMUFormOpened)
                {
                    GMUFormOpened = true;
                    GMU_IP frmGMU = new GMU_IP();
                    frmGMU.StartPosition = FormStartPosition.CenterScreen;
                    frmGMU.Show();
                }
	        }
	        catch (Exception ex)
	        {
                ExceptionManager.Publish(ex);
                MessageBox.Show("Unable to start ping GMU application at the moment.", CONST_MESSAGE_HEADER);                
	        }
        }

        private void DBCompare()
        {
            try
            { 
                int recPos=0;
                OleDbConnection con = new OleDbConnection(GetExchangeConnectionString());
                con.Open();
                OleDbCommand cmd = new OleDbCommand();
                cmd.Connection = con;
                object[] objArrRestrict;
                objArrRestrict = new object[] {null, null, null, "TABLE"};
                DataTable schemaTbl = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables,objArrRestrict);

                OleDbConnection con1 = new OleDbConnection(GetExchangeConnectionString());
                con1.Open();
                OleDbCommand cmd1 = new OleDbCommand();
                cmd1.Connection = con1;
                object[] objArrRestrict1;
                objArrRestrict1 = new object[] {null, null, null, "TABLE"};
                DataTable schemaTbl1 = con1.GetOleDbSchemaTable(OleDbSchemaGuid.Tables,objArrRestrict1);

                if(schemaTbl.Rows.Count!= schemaTbl1.Rows.Count)
                {
                    MessageBox.Show("Both DB don’t have equal number of tables! ");
                }
                else
                {
                    DataView schema1TblView = schemaTbl.DefaultView;
                    schema1TblView.Sort = "TABLE_NAME";
                    foreach (DataRow row in schemaTbl1.Rows)
                    {
                        recPos = schema1TblView.Find("");
                        if(recPos==-1)
                        { 
                            MessageBox.Show("DB’s are different");
                            break;
                        }
                    }
                }
              
            }
            catch (Exception ex)
            { 
                MessageBox.Show("Error:" + ex.Message);
            }
            }
    }

    
}