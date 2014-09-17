using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.Linq;
using BMC.Common.ExceptionManagement;
using BMC.MeterAdjustmentTool.Helpers;
using System.Data.SqlClient;
using BMC.MeterAdjustmentTool.Properties;
using BMC.MeterAdjustmentTool.Exchange;
using BMC.Common.LogManagement;
using BMC.Common;

namespace BMC.MeterAdjustmentTool
{
    public partial class MainForm : Form, IMainForm
    {
        private bool _isConnected = false;
        private rsp_GetSiteDetailsResult _selectedSite = null;
        private BmcConnectionStringBuilder _connectionStringBuilder = null;
        private string _connectionString = null;

        public static string _userName { get; set; }
        public static int _staffID { get; set; }
        public static int _securityUserID { get; set; }       

        public static MainForm CurrentForm { get; set; }
        
        public MainForm(string userName,int staffID,int securityUserID)
        {
            InitializeComponent();
            this.InitControls();
            CurrentForm = this;
            _userName = userName;
            _staffID = staffID;
            _securityUserID = securityUserID;

            // Set Tags for controls
            SetTagProperty();
        }

        private void SetTagProperty()
        {
            this.Tag = "Key_MeterAdjustmentTool";
            lblSiteStatus.Tag = "Key_SiteDisconnected";
            tbrItemConnect.Tag = "Key_Connectsite";
            tbrItemDisconnect.Tag = "Key_Disconnectsite";
            tbpRead.Tag = "Key_ReadVTP";
            tbpCollections.Tag = "Key_Collections";
            tbpMGMDDelta.Tag = "Key_MGMDSession";

            // Set Header Text
            this.uxBatchDetails.HeaderText = this.GetResourceTextByKey("Key_SuspectedBatchDetails");
            this.uxCollectionDetails.HeaderText = this.GetResourceTextByKey("Key_SuspectedCollectionDetails");
            this.uxDailyRead.HeaderText = this.GetResourceTextByKey("Key_SuspectedDailyReads");
            this.uxHourlyVTP.HeaderText = this.GetResourceTextByKey("Key_SuspectedHourlyData");
            this.uxMGMDDelta.HeaderText = this.GetResourceTextByKey("Key_SuspectedMGMDSessionDeltas");
        }

        private void InitControls()
        {
            this.SuspendLayout();
            sbrItemEntUser.Visible = false;
            pnlContainer.Dock = DockStyle.Fill;
            pnlInfo.Dock = DockStyle.Fill;
            this.IsConnected = false;
            uxBatchDetails.IsEditable = false;
            uxDailyRead.IsEditable = true;
            uxHourlyVTP.IsEditable = true;
            uxCollectionDetails.IsEditable = true;
            uxMGMDDelta.IsEditable = true;
            this.DisplayDateValues();
            this.ResumeLayout(true);
        }

        private void DisplayDateValues()
        {
            uxFilterRead.StartDate = DateTime.Now; // Settings.Default.ReadStartDate.GetValidDateTime();
            uxFilterRead.EndDate = DateTime.Now; // Settings.Default.ReadEndDate.GetValidDateTime();
            uxFilterCollections.StartDate = DateTime.Now; // Settings.Default.CollectionStartDate.GetValidDateTime();
            uxFilterCollections.EndDate = DateTime.Now; // Settings.Default.CollectionEndDate.GetValidDateTime();
            uxFilterMGMDDelta.StartDate = DateTime.Now; // Settings.Default.ReadStartDate.GetValidDateTime();
            uxFilterMGMDDelta.EndDate = DateTime.Now; // Settings.Default.ReadEndDate.GetValidDateTime();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.InitEventHandlers();
            //this.ShowLoginForm();

            // For externalization
            this.ResolveResources();
        }

        private void ShowLoginForm()
        {
            new LoginForm().ShowDialogExResultAndDestroy(this, null,
                (f) =>
                {
                    if (!f.IsFormClosedOK())
                    {
                        this.Close();
                    }
                    else
                    {
                        this.Login = f.Login;
                    }
                });
        }

        private LoginDetail _login = null;

        public LoginDetail Login
        {
            get { return _login; }
            private set
            {
                _login = value;
                sbrItemEntUser.Visible = (value != null);
                if (value != null)
                {
                    sbrItemEntUser.Text = value.UserName;
                }
            }
        }

        private void InitEventHandlers()
        {
#if DEBUG
            uxFilterRead.StartDate = new DateTime(2009, 01, 01);
            uxFilterRead.EndDate = new DateTime(2011, 10, 16);
            uxFilterCollections.StartDate = new DateTime(2009, 01, 01);
            uxFilterCollections.EndDate = new DateTime(2011, 10, 16);
            uxFilterMGMDDelta.StartDate = new DateTime(2009, 01, 01);
            uxFilterMGMDDelta.EndDate = new DateTime(2011, 10, 16);
#endif
            uxFilterRead.OwnerForm = this;
            uxFilterCollections.OwnerForm = this;
            uxFilterCollections.HideInstallation();
            uxFilterMGMDDelta.OwnerForm = this;

            // Daily Read
            uxDailyRead.DeltaFormClosed += new DeltaDetailsFormClosedHandler(uxDailyRead_DeltaFormClosed);
            uxDailyRead.CreateDataInterface = () =>
            {
                return new DailyRead(_connectionString);
            };
            uxDailyRead.LoadGridItems = (p) =>
            {
                DailyReadSuspectedData s = new DailyReadSuspectedData(_selectedSite);
                s.StartDate = p.StartDate;
                s.EndDate = p.EndDate;
                s.InstallationID = p.InstallationNo;
                s.SiteWebUrl = _selectedSite.WEBUrl;
                return s;
            };
            uxDailyRead.EditItem = (p) =>
            {
                DailyReadSelectedItemData s = new DailyReadSelectedItemData(_selectedSite);
                s.ReadID = Convert.ToInt32(p.SelectedRow["Read_No"].ToString());
                s.SiteWebUrl = _selectedSite.WEBUrl;
                return s;
            };
            uxDailyRead.UpdateItem = (p) =>
            {
                DailyReadUpdateData s = new DailyReadUpdateData(_selectedSite);
                s.ReadID = Convert.ToInt32(p.SelectedRow["Read_No"].ToString());
                s.InstallationID = Convert.ToInt32(p.SelectedRow["Installation_No"].ToString());
                s.UserID = _securityUserID;
                s.UserName = _userName;
                s.SiteWebUrl = _selectedSite.WEBUrl;
                return s;
            };
            uxDailyRead.RowSelected += new GridRowSelectedEventHandler(uxDailyRead_RowSelected);

            // Hourly VTP
            uxHourlyVTP.DeltaFormClosed += new DeltaDetailsFormClosedHandler(uxHourlyVTP_DeltaFormClosed);
            uxHourlyVTP.CreateDataInterface = () =>
            {
                return new HourlyVTP(_connectionString);
            };
            uxHourlyVTP.LoadGridItems = (p) =>
            {
                HourlyVTPSuspectedData s = new HourlyVTPSuspectedData(_selectedSite);
                s.StartDate = this.ReadDateTime.IsValid() ? this.ReadDateTime.Value : p.StartDate;
                s.EndDate = this.ReadDateTime.IsValid() ? this.ReadDateTime.Value : p.EndDate;
                s.InstallationID = this.ReadInstallationID.IsValid() ? this.ReadInstallationID : p.InstallationNo;
                s.SiteWebUrl = _selectedSite.WEBUrl;
                return s;
            };
            uxHourlyVTP.EditItem = (p) =>
            {
                HourlyVTPSelectedItemData s = new HourlyVTPSelectedItemData(_selectedSite);
                s.StartDate = Convert.ToDateTime(p.SelectedRow["HS_Date"].ToString());
                s.Hour = p.SelectedRow["Type"].ToString();
                s.InstallationID = Convert.ToInt32(p.SelectedRow["Installation_No"].ToString());
                s.SiteWebUrl = _selectedSite.WEBUrl;
                return s;
            };
            uxHourlyVTP.UpdateItem = (p) =>
            {
                HourlyVTPUpdateData s = new HourlyVTPUpdateData(_selectedSite);
                s.StartDate = Convert.ToDateTime(p.SelectedRow["HS_Date"].ToString());
                s.Hour = p.SelectedRow["Type"].ToString();
                s.InstallationID = Convert.ToInt32(p.SelectedRow["Installation_No"].ToString());
                s.UserID = _securityUserID;
                s.UserName = _userName;
                s.SiteWebUrl = _selectedSite.WEBUrl;
                return s;
            };

            // Batch Details
            uxBatchDetails.CreateDataInterface = () =>
            {
                return new CollectionDetails(_connectionString);
            };
            uxBatchDetails.LoadGridItems = (p) =>
            {
                CollectionDetailsBatchData s = new CollectionDetailsBatchData(_selectedSite);
                s.StartDate = p.StartDate;
                s.EndDate = p.EndDate;
                s.InstallationID = p.InstallationNo;
                s.SiteWebUrl = _selectedSite.WEBUrl;
                return s;
            };

            // Collection Details
            uxCollectionDetails.CreateDataInterface = () =>
            {
                return new CollectionDetails(_connectionString);
            };
            uxCollectionDetails.LoadGridItems = (p) =>
            {
                CollectionDetailsSuspectedData s = new CollectionDetailsSuspectedData(_selectedSite);
                s.BatchID = (this.BatchID.IsValid() ? this.BatchID.Value : 0);
                s.InstallationID = p.InstallationNo;
                s.SiteWebUrl = _selectedSite.WEBUrl;
                return s;
            };
            uxCollectionDetails.EditItem = (p) =>
            {
                CollectionDetailsSelectedItemData s = new CollectionDetailsSelectedItemData(_selectedSite);
                s.CollectionID = Convert.ToInt32(p.SelectedRow["Collection_No"].ToString());
                s.SiteWebUrl = _selectedSite.WEBUrl;
                return s;
            };
            uxCollectionDetails.UpdateItem = (p) =>
            {
                CollectionDetailsUpdateData s = new CollectionDetailsUpdateData(_selectedSite);
                s.BatchID = (this.BatchID.IsValid() ? this.BatchID.Value : 0);
                s.CollectionID = Convert.ToInt32(p.SelectedRow["Collection_No"].ToString());
                s.UserID = _securityUserID;
                s.UserName = _userName;
                s.SiteWebUrl = _selectedSite.WEBUrl;
                return s;
            };

            // MGMD              
            uxMGMDDelta.CreateDataInterface = () =>
            {
                return new MGMDDelta(_connectionString);
            };
            uxMGMDDelta.LoadGridItems = (p) =>
            {
                MGMDDeltaSuspectedData s = new MGMDDeltaSuspectedData(_selectedSite);
                s.StartDate = p.StartDate;
                s.EndDate = p.EndDate;
                s.InstallationID = p.InstallationNo;
                s.SiteWebUrl = _selectedSite.WEBUrl;
                return s;
            };
            uxMGMDDelta.EditItem = (p) =>
            {
                MGMDDeltaSelectedItemData s = new MGMDDeltaSelectedItemData(_selectedSite);
                s.SessionID = Convert.ToInt32(p.SelectedRow["MGMD_Session_ID"].ToString());
                s.SiteWebUrl = _selectedSite.WEBUrl;
                return s;
            };
            uxMGMDDelta.UpdateItem = (p) =>
            {
                MGMDDeltaUpdateData s = new MGMDDeltaUpdateData(_selectedSite);
                s.SessionID = Convert.ToInt32(p.SelectedRow["MGMD_Session_ID"].ToString());
                s.InstallationID = Convert.ToInt32(p.SelectedRow["MGMD_Installation_No"].ToString());
                s.UserID = _securityUserID;
                s.UserName = _userName;
                s.SiteWebUrl = _selectedSite.WEBUrl;
                return s;
            };
        }

        void uxHourlyVTP_DeltaFormClosed(object sender, DeltaDetailsCloseEventArgs e)
        {
            try
            {
                if (e.HasChanged &&
                    e.ViewInfo.Rows.Count>0)
                {
                    // "Kindly change the corresponding read (Bar Position : {0}, Read Date : {1}) record."
                    string barposCol = this.GetResourceTextByKey("Key_HrlyRdSch_Bar_Pos_Name");
                    string dateCol = this.GetResourceTextByKey("Key_HrlyRdSch_HS_Date");

                    // "BarPosition" "Date"
                    string message = string.Format(this.GetResourceTextByKey(1, "MSG_MAT_CHANGE_READRECORD"),
                        e.ViewInfo.Rows[0][barposCol].ToString(),
                        Convert.ToDateTime(e.ViewInfo.Rows[0][dateCol].ToString()).ToString("dd/MMM/yyyy"));
                    this.ShowInfoMessageBox(message);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        void uxDailyRead_DeltaFormClosed(object sender, DeltaDetailsCloseEventArgs e)
        {
            try
            {
                if (e.HasChanged &&
                    e.ViewInfo.Rows.Count > 0)
                {
                    // "Kindly change the corresponding hourly (Bar Position : {0}, Hourly Date : {1}) records."
                    string posCol = this.GetResourceTextByKey("Key_DailyRdSch_Bar_Pos_Name");
                    string readDateCol = this.GetResourceTextByKey("Key_DailyRdSch_CollectedDateTime");

                    // "Pos" "Read DateTime"
                    string message = string.Format(this.GetResourceTextByKey(1, "MSG_MAT_CHANGE_HOURLYRECORD"),
                        e.ViewInfo.Rows[0][posCol].ToString(),
                        Convert.ToDateTime(e.ViewInfo.Rows[0][readDateCol].ToString()).ToString("dd/MMM/yyyy"));
                    this.ShowInfoMessageBox(message);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        void uxDailyRead_RowSelected(object sender, GridRowSelectedEventArgs e)
        {
            this.ReadInstallationID = Convert.ToInt32(e.SelectedRow["Installation_No"].ToString());
            DateTime dtFormatted = e.SelectedRow["CollectedDateTime"].ToString().ParseDateTime();
            if (dtFormatted != DateTime.MinValue)
            {
                this.ReadDateTime = dtFormatted;
            }
            uxHourlyVTP.LoadGrid();
        }

        private void uxBatchDetails_RowSelected(object sender, GridRowSelectedEventArgs e)
        {
            this.BatchID = Convert.ToInt32(e.SelectedRow["Site Batch No"]);
            uxCollectionDetails.LoadGrid();
        }

        private void tbrItemConnect_Click(object sender, EventArgs e)
        {
            try
            {

                string str = string.Empty;
                SqlConnectDialog dlg = new SqlConnectDialog(_securityUserID);
                dlg.ShowDialogExResultAndDestroy(this,
                    (f) => { },
                    (f) =>
                    {
                        if (f.IsFormClosedOK())
                        {
                            this.SelectedSite = f.SelectedSite;
                            this.ConnectionStringBuilder = f.ConnectionStringBuilder;
                            this.ConnectionString = f.ConnectionString;
                            bool isCancelled = false;

                            if (this.SelectedSite.WEBUrl.Contains(MeterGlobals.ExchangeWebUrl114))
                            {
                                uxFilterRead.HideInstallation();
                                tabContainer.TabPages.Remove(tbpMGMDDelta);

                                if (!CheckSiteDBConnection())
                                {
                                    new SiteDBConnectDialog().ShowDialogExResultAndDestroy(this,
                                    (a) =>
                                        {   
                                            a.SiteId = f.SelectedSite.Site_ID;
                                            a.SiteCode = f.SelectedSite.Site_code;
                                            a.SiteName = f.SelectedSite.Site_Name;
                                            a.SecurityUserID = _securityUserID;
                                        },
                                    (g) =>
                                    {
                                        if (!g.IsFormClosedOK())
                                        {
                                            this.tbrItemDisconnect_Click(null, EventArgs.Empty);
                                            isCancelled = true;
                                        }
                                        else
                                        {
                                            this.SelectedSite = f.SelectedSite;
                                            this.ConnectionStringBuilder = g.ConnectionStringBuilder;
                                            this.ConnectionString = g.ConnectionString;                                            
                                        }
                                    });
                                }
                            }
                            else
                            {
                                if (!tabContainer.TabPages.Contains(tbpMGMDDelta))
                                {
                                    tabContainer.TabPages.Add(tbpMGMDDelta);
                                }
                            }

                            if (isCancelled) return;

                            if (!this.SelectedSite.WEBUrl.Contains(MeterGlobals.ExchangeWebUrl114))
                            {
                                uxFilterRead.FillInstallations();
                                uxFilterCollections.FillInstallations();
                                uxFilterMGMDDelta.FillInstallations();
                            }

                            uxFilterRead.SelectedSite = this.SelectedSite;
                            uxFilterRead.ConnectionString = this.ConnectionString;

                            uxFilterCollections.SelectedSite = this.SelectedSite;
                            uxFilterCollections.ConnectionString = this.ConnectionString;

                            uxFilterMGMDDelta.SelectedSite = this.SelectedSite;
                            uxFilterMGMDDelta.ConnectionString = this.ConnectionString;

                            this.DisplayDateValues();
                            this.IsConnected = true;
                        }
                    });
            }

            catch (Exception ex)
            {
                LogManager.WriteLog("Entering Meter Adjustment EXE", LogManager.enumLogLevel.Debug);
                ExceptionManager.Publish(ex);
            }
        }

        public bool IsConnected
        {
            get { return _isConnected; }
            set
            {
                _isConnected = value;
                pnlInfo.Visible = !value;
                pnlContainer.Visible = value;
                tbrItemConnect.Enabled = !value;
                tbrItemDisconnect.Enabled = value;
                sbrItemConnectStatus.Text = this.GetResourceTextByKey("Key_Disconnected");   // "Disconnected.";
                sbrItemConnectStatus.Image = BMC.MeterAdjustmentTool.Properties.Resources.DisconnectServer;
                sbrItemSite.Visible = value;
                sbrItemDBServer.Visible = value;
                sbrItemDBUserName.Visible = value;
                tabContainer.SelectedIndex = 0;
                this.ReadInstallationID = null;
                this.BatchID = null;

                if (value)
                {
                    sbrItemConnectStatus.Text = this.GetResourceTextByKey("Key_Connected");    // "Connected.";
                    sbrItemConnectStatus.Image =BMC.MeterAdjustmentTool.Properties.Resources.ConnectServer;
                    sbrItemSite.Text = _selectedSite.Site_Name;
                    sbrItemDBServer.Text = _connectionStringBuilder.Server;
                    sbrItemDBUserName.Text = _connectionStringBuilder.UserID;
                    
                    uxFilterRead.ViewReportEnabled = true;
                }
            }
        }

        private bool ValidateConnection(string SiteConnString)
        {
            bool result = false;
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = SiteConnString;
                
                try
                {
                    connection.Open();
                    if (connection.State == ConnectionState.Open)
                    {
                        bool siteCodeMatched = false;
                        string availSite = string.Empty;

                        try
                        {
                            SqlCommand cmd = new SqlCommand("SELECT TOP 1 Code FROM dbo.SITE", connection);
                            object siteCode = cmd.ExecuteScalar();
                            if (siteCode != null)
                            {
                                availSite = siteCode.ToString();
                                siteCodeMatched = (string.Compare(_selectedSite.Site_code, availSite, true) == 0);
                            }
                        }
                        catch { }

                        if (siteCodeMatched)
                        {
                            result = true;
                        }
                        else
                        {
                            this.ShowWarningMessageBox(string.Format(this.GetResourceTextByKey(1, "MSG_MAT_SITECODEMISMATCH"), _selectedSite.Site_code, availSite));
                            //  "Selected site's code ({0}) does not match with the actual site's code ({1})."
                        }
                    }
                    else
                    {
                        this.ShowErrorMessageBox(string.Format(this.GetResourceTextByKey(1, "MSG_MAT_CANNOTCONNECTSERVER") , _connectionStringBuilder.Server));
                        // "Unable to connect to the server "
                    }
                    return result;
                }
                catch (Exception ex)
                {
                    LogManager.WriteLog("Error in ValidateConnection - " + SiteConnString+ ".", LogManager.enumLogLevel.Info);
                    ExceptionManager.Publish(ex);
                    return result;
                }
            }
       

        private void tbrItemDisconnect_Click(object sender, EventArgs e)
        {
            this.DisposeSiteDetails();
            uxFilterRead.ClearInstallations();
            uxFilterCollections.ClearInstallations();
            uxFilterMGMDDelta.ClearInstallations();
            uxDailyRead.Clear();
            uxHourlyVTP.Clear();
            uxBatchDetails.Clear();
            uxCollectionDetails.Clear();
            uxMGMDDelta.Clear();
            uxFilterRead.ShowInstallation();
            this.IsConnected = false;
        }

        private void DisposeSiteDetails()
        {
            if (_connectionStringBuilder != null)
            {
                Extensions.DisposeObject(ref _connectionStringBuilder);
            }
            _selectedSite = null;
            this.ConnectionString = string.Empty;
        }

        private void uxFilterRead_ProcessClicked(object sender, ProcessEventArgs e)
        {
            try
            {
                this.ReadInstallationID = null;
                this.ReadDateTime = null;
                uxDailyRead.ProcessedArgs = e;
                uxHourlyVTP.ProcessedArgs = e;
                uxDailyRead.LoadGrid();
                uxHourlyVTP.LoadGrid();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void uxFilterCollections_ProcessClicked(object sender, ProcessEventArgs e)
        {
            try
            {
                this.BatchID = null;
                uxBatchDetails.ProcessedArgs = e;
                uxCollectionDetails.ProcessedArgs = e;
                uxBatchDetails.LoadGrid();
                // Set Header text for columns
                uxBatchDetails.SetBatchDetailsHeader();
                uxCollectionDetails.LoadGrid();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void uxFilterMGMDDelta_ProcessClicked(object sender, ProcessEventArgs e)
        {
            try
            {
                uxMGMDDelta.ProcessedArgs = e;
                uxMGMDDelta.LoadGrid();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private bool CheckSiteDBConnection()
        {
            bool DBConnection = false;
            rsp_GetSiteDetailsResult site = null;
            
            try
            {
                using (EnterpriseDataContextHelper db = new EnterpriseDataContextHelper())
                {
                    ISingleResult<rsp_GetSiteDetailsResult> sites = db.FuncGetSiteDetails(SelectedSite.Site_Name, null,_securityUserID);
                    if (sites != null)
                    {
                        site = sites.FirstOrDefault();
                        {
                            Binary binData = site.Site_DB_ConnectionString;
                           
                            if (binData != null)
                            {
                                byte[] bytData = binData.ToArray();
                                if (bytData != null)
                                {
                                    if (ValidateConnection(TripleDESEncryption.DecryptFromBytes(bytData)))
                                    {
                                        DBConnection = true;
                                    }
                                    else
                                    {
                                        DBConnection = false;
                                    }
                                }
                            }
                            else
                            {
                                DBConnection = false;
                            }
                        }
                    }
                    else
                    {
                        DBConnection = false;
                    }
                }
                return DBConnection;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return DBConnection;
            }  
        }
        
      	public rsp_GetSiteDetailsResult SelectedSite
        {
            get { return _selectedSite; }
            set { _selectedSite = value; }
        }

        public BmcConnectionStringBuilder ConnectionStringBuilder
        {
            get { return _connectionStringBuilder; }
            set { _connectionStringBuilder = value; }
        }

        public string ConnectionString
        {
            get { return _connectionString; }
            set { _connectionString = value; }
        }

        public int? ReadInstallationID { get; set; }

        public DateTime? ReadDateTime { get; set; }

        public int? BatchID { get; set; }
    }
}
