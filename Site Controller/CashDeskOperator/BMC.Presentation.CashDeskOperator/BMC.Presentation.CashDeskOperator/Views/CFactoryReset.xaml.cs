using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BMC.Common.ExceptionManagement;
using BMC.Common.LogManagement;
using BMC.CashDeskOperator.BusinessObjects;
using BMC.Transport.CashDeskOperatorEntity;
using BMC.Common.ConfigurationManagement;
using System.ComponentModel;
using System.Windows.Threading;
using System.Threading;
using System.Windows.Media.Animation;

//Audit
using Audit.BusinessClasses;
using Audit.Transport;
using BMC.Security;
using BMC.Presentation.POS.Helper_classes;

namespace BMC.Presentation.POS.Views
{
    /// <summary>
    /// Interaction logic for CFactoryReset.xaml
    /// </summary>
    public partial class CFactoryReset : UserControl, IDisposable
    {
        #region Declarations
        public static CFactoryReset instance = null;
        private FactoryResetMethods oFactoryReset = FactoryResetMethods.FactoryResetMethodsInstance;
        oCommonUtilities oCommonutilities = oCommonUtilities.CreateInstance();
        FactoryReset oFactoryResetTransport = new FactoryReset();
        private string strRegistryKeyPath = string.Empty;
        private string[] strListarray = null;
        private string strServiceStatus = string.Empty;
        private string strKeyText = string.Empty;
        private BackgroundWorker workerProcess = null;
        private delegate bool delDatabaseBackup(string s);
        private delDatabaseBackup odelDatabaseBackup = null;
        private static bool bDatabaseBackup = false;
        private Grid pnlContent = null;
        private int count = 0;
        private int FRHistoryID = 0;
        
        FactoryResetMode _FactoryResetMode;
        #endregion

        #region Constructor

        protected CFactoryReset()
        {
            InitializeComponent();
        }

        public static CFactoryReset FactoryResetInstance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CFactoryReset();
                }
                return instance;
            }
        }

        public FactoryResetMode FactoryReset { get { return this._FactoryResetMode; } set { this._FactoryResetMode = value; } }
        
        public Grid InitializepnlContent
        {
            get
            {
                return pnlContent;
            }
            set
            {
                pnlContent = value;
            }
        }


        #endregion

        #region Class level Static methods

        static void BackupComplete(IAsyncResult ar)
        {
            System.Runtime.Remoting.Messaging.AsyncResult iar = (System.Runtime.Remoting.Messaging.AsyncResult)ar;
            delDatabaseBackup state = (delDatabaseBackup)iar.AsyncDelegate;
            bDatabaseBackup = state.EndInvoke(ar);
            ar.AsyncWaitHandle.Close();
        }

        #endregion Class level Static methods

        #region Class level Methods

        private void Refresh()
        {
            txtAutCode.Text = "";
            Cursor = System.Windows.Input.Cursors.Arrow;
            pbFactory.Visibility = Visibility.Hidden;
            LayoutRoot.Visibility = Visibility.Hidden;
            btnCancel.Visibility = Visibility.Visible;
            btnOK.Visibility = Visibility.Visible;
            lblStatus.Visibility = Visibility.Hidden;
            txtAutCode.IsEnabled = true;
            btnOK.IsEnabled = true;
        }

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
                            oFactoryResetTransport.Server = objKeyValue.Value;
                        }
                        else if (objKeyValue.Key.ToUpper() == "UID")
                        {
                            oFactoryResetTransport.UserID = objKeyValue.Value;
                        }
                        else if (objKeyValue.Key.ToUpper() == "PASSWORD")
                        {
                            oFactoryResetTransport.Password = objKeyValue.Value;
                        }
                        else if (objKeyValue.Key.ToUpper() == "DATABASE")
                        {
                            oFactoryResetTransport.DataBase = objKeyValue.Value;
                        }
                        else if (objKeyValue.Key.ToUpper() == "TIMEOUT")
                        {
                            oFactoryResetTransport.ConnectionTimeOut = objKeyValue.Value;
                        }
                        else if (objKeyValue.Key.ToUpper() == "INSTANCE")
                        {
                            oFactoryResetTransport.ServerInstance = objKeyValue.Value;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void GetConfigSetting()
        {
            Dictionary<string, string> objKeycCollections = oFactoryReset.GetKeys("appSettings");

            if (objKeycCollections != null)
            {
                foreach (KeyValuePair<string, string> objKeys in objKeycCollections)
                    if (objKeys.Key.ToString() == FactoryResetConstants.strConfigServicelist)
                    {
                        {
                            strListarray = objKeys.Value.ToString().Split(',');
                        }
                    }
                    else if (objKeys.Key.ToString() == FactoryResetConstants.strConfigRegistrypath)
                    {
                        strRegistryKeyPath = objKeys.Value.ToString();
                        oFactoryResetTransport.RegistryKeyValue = (!string.IsNullOrEmpty(strRegistryKeyPath)) ? strRegistryKeyPath : string.Empty;


                    }
                    else if (objKeys.Key.ToString() == FactoryResetConstants.strConfigNetloggerpath)
                    {
                        strRegistryKeyPath = objKeys.Value.ToString();
                        oFactoryResetTransport.NetLoggerRegKeyValue = (!string.IsNullOrEmpty(strRegistryKeyPath)) ? strRegistryKeyPath : string.Empty;
                    }
            }

            else
            {
                MessageBox.ShowBox("MessageID154", BMC_Icon.Warning, BMC_Button.OK);
            }
        }

        private void GetDbSettings()
        {
            string strKeyvalue = string.Empty;
            string strWebUrl = string.Empty;

            try
            {
                oFactoryResetTransport.ExchangeConnectionString = oCommonutilities.GetConnectionString();
                oFactoryResetTransport.TicketingConnectionString = oCommonutilities.GetTicketConnectionString();
                oFactoryResetTransport.CMPConnectionString = oCommonutilities.GetCMPConnectionString();
                oFactoryResetTransport.TicketLocationCode = oCommonutilities.GetTicketLocationCode();
                ConfigManager.SetConfigurationMode(ConfigManager.ConfigurationMode.AppConfig);
                if (ConfigManager.Read("ServicesListFromDB") != null)
                {
                    if (ConfigManager.Read("ServicesListFromDB").ToUpper() == "TRUE")
                    {
                        strListarray = null;
                        strListarray = oFactoryReset.GetSettingValue("ServiceNames").Split(',');
                    }
                }
                GetExchangeServerSettings(oFactoryReset.RetrieveServerDetails(oFactoryResetTransport.ExchangeConnectionString));
                //Dictionary<string, string> ExchangeRegistryEntries = oFactoryReset.GetRegistryEntries(oFactoryResetTransport.RegistryKeyValue);
                //foreach (KeyValuePair<string, string> KVPServer in ExchangeRegistryEntries)
                //{
                //    strKeyvalue = KVPServer.Key.Substring(KVPServer.Key.LastIndexOf('\\') + 1);

                //    if (strKeyvalue == "Default_Server_IP")
                //    {
                //        if (!string.IsNullOrEmpty(KVPServer.Value.ToString()))
                //        {

                //        }
                //    }

                //    if (strKeyvalue == "EnableDhcp")
                //    {
                //        if (!string.IsNullOrEmpty(KVPServer.Value.ToString()))
                //        {

                //        }
                //    }
                //    if (strKeyvalue == "BindIPAddress")
                //    {
                //        if (!string.IsNullOrEmpty(KVPServer.Value.ToString()))
                //        {

                //        }
                //    }

                //    if (strKeyvalue == "BGSWebService")
                //    {
                //        if (!string.IsNullOrEmpty(KVPServer.Value.ToString()))
                //        {
                //            strWebUrl = KVPServer.Value.ToString();
                //            strWebUrl = strWebUrl.Substring(strWebUrl.IndexOf("//") + 2);
                //            //txtEnterpriseweburl.Text = strWebUrl.Substring(0, strWebUrl.IndexOf("/"));
                //        }

                //    }

                //}
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

        }

        private void StopAllServices()
        {
            StringBuilder strServicelist = new StringBuilder();
            bool bServiceStatus = false;
            //Stop all serivces before doing clearing data
            if (strListarray != null)
            {
                for (int i = 0; i < strListarray.Length; i++)
                {
                    if (strListarray[i] != null && strListarray[i] != string.Empty)
                    {
                        strServicelist.Append(strListarray[i].ToString() + ",");
                        strServiceStatus = oFactoryReset.GetServiceStatus(strListarray[i]);
                        if (strServiceStatus.ToUpper() == "NOSERVICE")
                        {
                            LogManager.WriteLog(strListarray[i] + "Service not found", LogManager.enumLogLevel.Info);
                        }
                        else
                        {
                            lblStatus.Text = "Status:\nStopping services...";
                            bServiceStatus = oFactoryReset.EndService(strListarray[i].ToString());
                            System.Windows.Forms.Application.DoEvents();
                            if (bServiceStatus)
                            {
                                LogManager.WriteLog("Status of  " + strListarray[i] + "is" + bServiceStatus.ToString(), LogManager.enumLogLevel.Info);
                            }
                            else
                            {
                                LogManager.WriteLog("Status of  " + strListarray[i] + "is" + bServiceStatus.ToString(), LogManager.enumLogLevel.Info);
                            }
                        }
                    }
                }
            }
        }

        private void StartAllServices()
        {
            StringBuilder strServicelist = new StringBuilder();
            bool bServiceStatus = false;
            //Stop all serivces before doing clearing data
            if (strListarray != null)
            {
                for (int i = 0; i < strListarray.Length; i++)
                {
                    if (strListarray[i] != null && strListarray[i] != string.Empty)
                    {
                        strServicelist.Append(strListarray[i].ToString() + ",");
                        strServiceStatus = oFactoryReset.GetServiceStatus(strListarray[i]);
                        if (strServiceStatus.ToUpper() == "NOSERVICE")
                        {
                            LogManager.WriteLog(strListarray[i] + "Service not found", LogManager.enumLogLevel.Info);
                        }
                        else
                        {
                            Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate
                            { lblStatus.Text = "Status:\nStarting services..."; });
                            bServiceStatus = oFactoryReset.StartService(strListarray[i].ToString());
                            System.Windows.Forms.Application.DoEvents();
                            if (bServiceStatus)
                            {
                                LogManager.WriteLog("Status of  " + strListarray[i] + "is" + bServiceStatus.ToString(), LogManager.enumLogLevel.Info);
                            }
                            else
                            {
                                LogManager.WriteLog("Status of  " + strListarray[i] + "is" + bServiceStatus.ToString(), LogManager.enumLogLevel.Info);
                            }
                        }
                    }
                }
            }
        }

        //private bool ClearRegistrySettings()
        //{
        //    bool bClearRegistry = false;
        //    try
        //    {
        //        Dictionary<string, string> dictSetregistryentries;
        //        Dictionary<string, string> dictSetNetLoggerRegistryEntry;
        //        //clearing Registry
        //        dictSetregistryentries = new Dictionary<string, string>();
                
        //        dictSetregistryentries.Add("BGSWebService", "");
        //        dictSetregistryentries.Add("SQLConnect", "");
        //        dictSetregistryentries.Add("SQLConnectEx", "");
        //        dictSetregistryentries.Add("TicketingSQLConnect", "");

        //        dictSetNetLoggerRegistryEntry = new Dictionary<string, string>();
        //        dictSetNetLoggerRegistryEntry.Add(FactoryResetConstants.strNetLogger.ToString(), "127.0.0.1");

        //        //Save all Registry Settings under cash master
        //        oFactoryReset.SetRegistryEntries(dictSetregistryentries, oFactoryResetTransport.RegistryKeyValue);
        //        //Save all Registry Settings under NetLogger   
        //        oFactoryReset.SetRegistryEntries(dictSetNetLoggerRegistryEntry, oFactoryResetTransport.NetLoggerRegKeyValue);
        //        bClearRegistry = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        bClearRegistry = false;
        //        ExceptionManager.Publish(ex);
        //    }
        //    return bClearRegistry;
        //}

        private bool TestConnectionToDB(Dictionary<string, string> ServerEntries, string strDbname)
        {
            try
            {
                oFactoryReset.MakeConnectionString(ServerEntries);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return false;
        }

        private bool TestConnectionToDB(string Connectionstring)
        {
            try
            {
                return oFactoryReset.TestConnectionDB(Connectionstring);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return false;
            }
        }

        private bool CreateSqlDatabaseBackUp(string sDatabase)
        {
            int iDatabaseZip = 0;
            bool bDatabaseZip = false;
            Application.Current.Dispatcher.Invoke(DispatcherPriority.Background, new ThreadStart(delegate {
                try
                {
                    ConfigManager.SetConfigurationMode(ConfigManager.ConfigurationMode.AppConfig);
                    oFactoryResetTransport.strBackupPath = ConfigManager.Read("ZipPath") != null ? ConfigManager.Read("ZipPath") : "";
                    if (!string.IsNullOrEmpty(oFactoryResetTransport.strBackupPath))
                    {
                        iDatabaseZip = oFactoryReset.CreateSqlDatabaseBackUp(oFactoryResetTransport);
                        switch (iDatabaseZip)
                        {
                            case (1):
                                {
                                    LogManager.WriteLog(sDatabase + " backup is completed and available in the path " + oFactoryResetTransport.strBackupPath + " is failed.", LogManager.enumLogLevel.Info);
                                    MessageBox.ShowBox("MessageID155", BMC_Icon.Information, BMC_Button.OK, sDatabase, oFactoryResetTransport.strBackupPath);
                                    bDatabaseZip = true;
                                    break;
                                }
                            case (2):
                                {
                                    LogManager.WriteLog("The backup file for " + sDatabase + " already exists.", LogManager.enumLogLevel.Info);
                                    MessageBox.ShowBox("MessageID156", BMC_Icon.Information, BMC_Button.OK, sDatabase);
                                    bDatabaseZip = false;
                                    break;
                                }
                            case (0):
                                {
                                    LogManager.WriteLog("The backup process for " + sDatabase + "  failed.", LogManager.enumLogLevel.Info);
                                    MessageBox.ShowBox("MessageID157", BMC_Icon.Error, BMC_Button.OK, sDatabase);
                                    bDatabaseZip = false;
                                    break;
                                }
                        }
                    }
                    else
                    {
                        MessageBox.ShowBox("MessageID158", BMC_Icon.Error, BMC_Button.OK, sDatabase);
                        bDatabaseZip = false;
                    }

                }
                catch (Exception ex)
                {
                    ExceptionManager.Publish(ex);
            } }));
            return bDatabaseZip;

        }

        private bool CreateZip()
        {
            System.Windows.Forms.DialogResult dr;
            System.IO.FileStream fileSource = null;
            int iDatabaseZip = 0;
            bool bDatabaseZip = false;
            string srcFileName = "";
              Application.Current.Dispatcher.Invoke(DispatcherPriority.Background, new ThreadStart(delegate {
                try
                {
                    lblStatus.Text = "Status:\nCompressing DB backup...";
                    srcFileName = oFactoryResetTransport.strBackupFileName;

                    if (!string.IsNullOrEmpty(srcFileName))
                    {
                        fileSource = new System.IO.FileStream(srcFileName, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.Read);
                        if (fileSource != null)
                        {
                            LogManager.WriteLog("Db FileSize: " + fileSource.Length.ToString(), LogManager.enumLogLevel.Info);
                            if (fileSource.Length > Int32.MaxValue)
                            {
                                dr = MessageBox.ShowBox("MessageID159", BMC_Icon.Question, BMC_Button.YesNo);

                                if (dr.ToString().ToUpper() == "YES")
                                {
                                    oFactoryResetTransport.iSeverity = 9;
                                }
                                else
                                { bDatabaseZip = false; }
                            }
                            else
                            {
                                oFactoryResetTransport.iSeverity = 5;
                            }
                        }
                        iDatabaseZip = oFactoryReset.CreateDBZip(oFactoryResetTransport);
                        switch (iDatabaseZip)
                        {
                            case (1):
                                {
                                    LogManager.WriteLog(oFactoryResetTransport.BackUpDataBase + " zip is completed and available in the path" + oFactoryResetTransport.strBackupPath + " is failed.", LogManager.enumLogLevel.Info);
                                    MessageBox.ShowBox("MessageID160", BMC_Icon.Information, BMC_Button.OK, oFactoryResetTransport.BackUpDataBase, oFactoryResetTransport.strBackupPath);
                                    bDatabaseZip = true;
                                    break;
                                }
                            case (0):
                                {
                                    LogManager.WriteLog("The zip process for " + oFactoryResetTransport.BackUpDataBase + " is failed.", LogManager.enumLogLevel.Info);
                                    MessageBox.ShowBox("MessageID161", BMC_Icon.Error, BMC_Button.OK, oFactoryResetTransport.BackUpDataBase);
                                    bDatabaseZip = false;
                                    break;
                                }
                        }
                    }
                    else
                    {
                        MessageBox.ShowBox("MessageID162", BMC_Icon.Error, BMC_Button.OK);
                        bDatabaseZip = false;
                    }

                }
                catch (Exception ex)
                {
                    ExceptionManager.Publish(ex);
                }
            }));
            return bDatabaseZip;
        }

        private int CheckStartUpPath(string Path)
        {
            if (System.Windows.Forms.Application.StartupPath.IndexOf(Path, 0) > 0)
            {
                return System.Windows.Forms.Application.StartupPath.IndexOf(Path, 0);
            }
            else
            {
                return 0;
            }
        }

        private void FactoryResetProcess()
        {
            System.Windows.Forms.DialogResult dr;
            string strSqlFilePath = string.Empty;
            string strScriptToRun = string.Empty;
            int iAuthResult = 0;
            string sTextAutCode = string.Empty;
            try
            {
                LogManager.WriteLog("Started FactoryResetProcess: ", LogManager.enumLogLevel.Debug);

                LayoutRoot.Visibility = Visibility.Visible;
                pbFactory.Visibility = Visibility.Visible;
                sTextAutCode = txtAutCode.Text.Trim();
                txtAutCode.IsEnabled = false;
                pbFactory.Value = 5;

                if (sTextAutCode == string.Empty)
                {
                    MessageBox.ShowBox("MessageID163", BMC_Icon.Warning, BMC_Button.OK);
                    txtAutCode.Focus();
                    pbFactory.Value = 0;
                    MakeVisible();
                    Refresh();
                    return;
                }
                else
                {
                    lblStatus.Text = "Status:\nVerifying Authorization code...";
                    pbFactory.Value = 10;
                    iAuthResult = oFactoryReset.CheckAuthorizationCode(sTextAutCode);
                    switch (iAuthResult)
                    {

                        case -1:
                            {
                                MessageBox.ShowBox("MessageID164", BMC_Icon.Warning, BMC_Button.OK);
                                pbFactory.Value = 0;
                                MakeVisible();
                                Refresh();
                                return;

                            }
                        case 0:
                            {
                                MessageBox.ShowBox("MessageID164", BMC_Icon.Warning, BMC_Button.OK);
                                pbFactory.Value = 0;
                                MakeVisible();
                                Refresh();
                                return;

                            }
                        case 1: break;
                        case 2:
                            {
                                bool bUpdateStatus = oFactoryReset.ResetTransactionKey(sTextAutCode);
                                MessageBox.ShowBox("MessageID25", BMC_Icon.Warning, BMC_Button.OK);
                                pbFactory.Value = 0;
                                MakeVisible();
                                Refresh();
                                return;
                            }
                        case 3:
                            {
                                bool bUpdateStatus = oFactoryReset.ResetTransactionKey(sTextAutCode);
                                MessageBox.ShowBox("MessageID25", BMC_Icon.Warning, BMC_Button.OK);
                                pbFactory.Value = 0;
                                MakeVisible();
                                Refresh();
                                return;
                            }
                        case 4:
                            {
                                MessageBox.ShowBox("MessageID167", BMC_Icon.Warning, BMC_Button.OK);
                                pbFactory.Value = 0;
                                MakeVisible();
                                Refresh();
                                return;
                            }
                        case 6:
                            {
                                MessageBox.ShowBox("MessageID168", BMC_Icon.Warning, BMC_Button.OK);
                                pbFactory.Value = 0;
                                MakeVisible();
                                Refresh();
                                return;
                            }
                        case 7:
                            {
                                MessageBox.ShowBox("MessageID199", BMC_Icon.Warning, BMC_Button.OK);
                                pbFactory.Value = 0;
                                MakeVisible();
                                Refresh();
                                return;

                            }
                        case 8:
                            {
                                MessageBox.ShowBox("MessageID200", BMC_Icon.Warning, BMC_Button.OK);
                                pbFactory.Value = 0;
                                MakeVisible();
                                Refresh();
                                return;
                            }
                    }
                    pbFactory.Value = 20;

                    lblStatus.Text = "Status:\nVerified Authorization code...";

                    if (!string.IsNullOrEmpty(oFactoryResetTransport.ExchangeConnectionString))
                    {
                        if (TestConnectionToDB(oFactoryResetTransport.ExchangeConnectionString))
                        {
                            pbFactory.Value = 30;

                            if (strListarray != null)
                            {                                
                                StopAllServices();
                            }
                            else
                            {
                                MessageBox.ShowBox("MessageID172", BMC_Icon.Warning, BMC_Button.OK);
                                pbFactory.Value = 0;
                                MakeVisible();
                                Refresh();
                                return;
                            }

                            pbFactory.Value = 40;

                            ResumeFactoryProcess();
                        }
                        else
                        {
                            MessageBox.ShowBox("MessageID174", BMC_Icon.Warning, BMC_Button.OK);
                            pbFactory.Value = 0;
                            MakeVisible();
                            Refresh();
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.ShowBox("MessageID174", BMC_Icon.Warning, BMC_Button.OK);
                        pbFactory.Value = 0;
                        MakeVisible();
                        Refresh();
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                pbFactory.Value = 0;
                MakeVisible();
                Refresh();
            }
        }

        private void MakeVisible()
        {
            btnOK.Visibility = Visibility.Visible;
            btnCancel.Visibility = Visibility.Visible;
        }

        private void DBBackup_ZipWorker(int i)
        {
            System.Windows.Forms.DialogResult dr;
            string sDr = string.Empty;
            try
            {
                workerProcess = new BackgroundWorker();
                workerProcess.WorkerReportsProgress = true;
                workerProcess.WorkerSupportsCancellation = true;
                if (workerProcess.IsBusy == false)
                {
                    if (i == 0) { oFactoryResetTransport.BackUpDataBase = FactoryResetConstants.strExchangeDBName; }
                    else if (i == 1) { oFactoryResetTransport.BackUpDataBase = FactoryResetConstants.strTicketingDBName; }

                    workerProcess.DoWork += (s, args) =>
                    {
                        Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate
                    { lblStatus.Text = "Status:\nCreating DB backup..."; });
                        odelDatabaseBackup = new delDatabaseBackup(CreateSqlDatabaseBackUp);
                        IAsyncResult ar = odelDatabaseBackup.BeginInvoke(oFactoryResetTransport.BackUpDataBase, new AsyncCallback(BackupComplete), new object());
                        while (!ar.IsCompleted)
                        {
                            Thread.Sleep(10);
                            ar.AsyncWaitHandle.WaitOne();
                            workerProcess.ReportProgress(DateTime.Now.Second);
                        }
                        workerProcess.ReportProgress(DateTime.Now.Second);
                        if (workerProcess.CancellationPending)
                        {
                            args.Cancel = true;
                            MakeVisible();
                            Refresh();
                            return;
                        }
                    };
                    workerProcess.ProgressChanged += (s, args) =>
                    {
                        pbFactory.Value = 50 + args.ProgressPercentage;
                        if (Spin.IsFrozen)
                            LayoutRoot.BeginAnimation(Border.OpacityProperty, Spin, HandoffBehavior.SnapshotAndReplace);
                    };
                    workerProcess.RunWorkerCompleted += (s, args) =>
                    {
                        if (args.Cancelled == true)
                        {
                            pbFactory.Value = 0;
                        }
                        else
                        {
                            if (bDatabaseBackup == false)
                            {
                                pbFactory.Value = 60;
                                dr = MessageBox.ShowBox("MessageID176", BMC_Icon.Question, BMC_Button.YesNo);
                                sDr = dr.ToString().ToUpper();
                                if (sDr == "NO")
                                {
                                    pbFactory.Value = 0;
                                    MakeVisible();
                                    Refresh();
                                    workerProcess.CancelAsync();
                                    workerProcess.Dispose();
                                    return;
                                }
                            }
                            else
                            {
                                pbFactory.Value = 65;
                                //dr = MessageBox.ShowBox("MessageID177", BMC_Icon.Question, BMC_Button.YesNo);
                                //    sDr = dr.ToString().ToUpper();
                                //    if (sDr == "YES")
                                //    {                                                                                              
                                pbFactory.Value = 70;
                                if (CreateZip() == false)
                                {
                                    pbFactory.Value = 80;
                                    dr = MessageBox.ShowBox("MessageID178", BMC_Icon.Question, BMC_Button.YesNo);
                                    sDr = dr.ToString().ToUpper();
                                    if (sDr == "NO")
                                    {
                                        pbFactory.Value = 0;
                                        MakeVisible();
                                        Refresh();
                                        workerProcess.CancelAsync();
                                        workerProcess.Dispose();
                                        return;
                                    }
                                }
                                //}

                            }
                            i = i + 1;
                            if (i == 1 && args.Cancelled == false)
                                DBBackup_ZipWorker(i);
                            if (i > 1 && args.Cancelled == false)
                                ResumeFactoryProcess();
                        }

                    };
                    workerProcess.RunWorkerAsync();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                pbFactory.Value = 0;
            }
        }

        private void ResumeFactoryProcess()
        {
            try
            {
                int iMode_Id = Convert.ToInt32(_FactoryResetMode);

                int iCounter = 3;

                lblStatus.Text = "Status:\nReset process started...";

                if (oFactoryReset.FactoryResetHistory(false, iMode_Id, SecurityHelper.CurrentUser.UserName, ref FRHistoryID) == false)
                {
                    LogManager.WriteLog("FactoryResetHistory: ", LogManager.enumLogLevel.Debug);
                    Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate
                    { MessageBox.ShowBox("MessageID179", BMC_Icon.Warning, BMC_Button.OK); });
                    pbFactory.Value = 0;
                    Refresh();
                    return;
                }

                while (iCounter>=iMode_Id)
                {
                    if (_FactoryResetMode == FactoryResetMode.ResetAccountInformation)
                        lblStatus.Text = "Status:\nDeleting account information...";
                    
                    if (_FactoryResetMode == FactoryResetMode.ResetToInitailConfiguration)
                        lblStatus.Text = "Status:\nDeleting all installation information...";
                    
                    if (_FactoryResetMode == FactoryResetMode.MasterReset)
                        lblStatus.Text = "Status:\nDeleting user information...";

                    if (!Reset(iCounter--))
                    {
                        if (oFactoryReset.FactoryResetHistory(false, 0, "", ref FRHistoryID) == false)
                        {
                            LogManager.WriteLog("FactoryResetHistory: ", LogManager.enumLogLevel.Debug);
                            Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate
                            { MessageBox.ShowBox("MessageID179", BMC_Icon.Warning, BMC_Button.OK); });
                            pbFactory.Value = 0;
                            Refresh();                            
                        }
						return;
                    }
                }

                if (oFactoryReset.DeleteAddConstraint(false) == false)
                {
                    LogManager.WriteLog("DeleteAddConstraint: ", LogManager.enumLogLevel.Debug);
                    Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate
                    { MessageBox.ShowBox("MessageID179", BMC_Icon.Warning, BMC_Button.OK); });
                    pbFactory.Value = 0;
                    Refresh();
                    return;
                }

                if (oFactoryReset.BackupConstraint(false, 0) == false)
                {
                    LogManager.WriteLog("BackupConstraint: ", LogManager.enumLogLevel.Debug);
                    Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate
                    { MessageBox.ShowBox("MessageID179", BMC_Icon.Warning, BMC_Button.OK); });
                    pbFactory.Value = 0;
                    Refresh();
                    return;
                }

                if (oFactoryReset.FactoryResetHistory(true, iMode_Id, SecurityHelper.CurrentUser.UserName, ref FRHistoryID) == false)
                {
                    LogManager.WriteLog("FactoryResetHistory: ", LogManager.enumLogLevel.Debug);
                    Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate
                    { MessageBox.ShowBox("MessageID179", BMC_Icon.Warning, BMC_Button.OK); });
                    pbFactory.Value = 0;
                    Refresh();
                    return;
                }

                lblStatus.Text = "Status:\nReset process completed...";

                pbFactory.Value = 98;

                StartAllServices();

                LogManager.WriteLog("Factory Reset completed successfully.", LogManager.enumLogLevel.Debug);

                Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate
                {
                    MessageBox.ShowBox("MessageID181", BMC_Icon.Information, BMC_Button.OK);
                    pbFactory.Value = 100;
                    Cursor = Cursors.Arrow;
                    txtAutCode.PreviewMouseUp -= new MouseButtonEventHandler(txtAutCode_PreviewMouseUp);
                    txtAutCode.KeyDown -= new KeyEventHandler(txtAutCode_KeyDown);
                    Refresh();
                    Application.Current.Shutdown();
                });
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                pbFactory.Value = 0;
            }
        }

        private bool Reset(int Mode_Id)
        {
            if (oFactoryReset.BackupConstraint(true, Mode_Id) == false)
            {
                LogManager.WriteLog("RunScripts: ", LogManager.enumLogLevel.Debug);
                Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate
                { MessageBox.ShowBox("MessageID179", BMC_Icon.Warning, BMC_Button.OK); });
                pbFactory.Value = 0;
                Refresh();
                return false;
            }

            if (oFactoryReset.DeleteAddConstraint(true) == false)
            {
                LogManager.WriteLog("RunScripts: ", LogManager.enumLogLevel.Debug);
                Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate
                { MessageBox.ShowBox("MessageID179", BMC_Icon.Warning, BMC_Button.OK); });
                pbFactory.Value = 0;
                Refresh();
                return false;
            }

            if (oFactoryReset.ResetTable(Mode_Id) == false)
            {
                LogManager.WriteLog("RunScripts: ", LogManager.enumLogLevel.Debug);
                Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate
                { MessageBox.ShowBox("MessageID179", BMC_Icon.Warning, BMC_Button.OK); });
                pbFactory.Value = 0;
                Refresh();
                return false;
            }

            return true;
        }

        #endregion Class level Methods

        #region Class level Events

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Refresh();            
            GetConfigSetting();
            GetDbSettings();
            txtAutCode.Focus();
            LayoutRoot.Visibility = Visibility.Hidden;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                btnCancel.IsEnabled = false;
                Refresh();
                txtAutCode.Focus();
            }
            finally
            {
                btnCancel.IsEnabled = true;
            }
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                txtAutCode.Focus();
                btnOK.IsEnabled = false;
                Cursor = Cursors.Wait;
                txtAutCode.IsEnabled = false;
                LayoutRoot.Visibility = Visibility.Visible;
                btnOK.Visibility = Visibility.Hidden;
                btnCancel.Visibility = Visibility.Hidden;
                lblStatus.Visibility = Visibility.Visible;
                FactoryResetProcess();
            }
            finally
            {
                btnOK.IsEnabled = true;
            }
        }

        private void txtAutCode_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            try
            {
                if (!FactoryResetConstants.AllowedKeys.Contains(e.Key)) { e.Handled = true; }

            }


            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

        }

        private void txtAutCode_PreviewMouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (!BMC.Transport.Settings.OnScreenKeyboard)
                return;
            txtAutCode.Text = DisplayKeyboard(string.Empty, string.Empty);
            txtAutCode.SelectAll();
        }

        #endregion Class level Events

        #region Keyboard events
        void objKeyboard_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (((KeyboardInterface)sender).DialogResult == true)
            {
                strKeyText = ((KeyboardInterface)sender).KeyString;
            }
        }

        private string DisplayKeyboard(string KeyText, string type)
        {
            try
            {
                this.Cursor = Cursors.Wait;
                strKeyText = "";
                KeyboardInterface objKeyboard = new KeyboardInterface();
                if (type == "Pwd")
                {
                    objKeyboard.IsPwd = true;
                }
                objKeyboard.Closing += new System.ComponentModel.CancelEventHandler(objKeyboard_Closing);
                objKeyboard.KeyString = KeyText;
                Point locationFromScreen = CFactoryReset.FactoryResetInstance.PointToScreen(new Point(0, 0));
                PresentationSource source = PresentationSource.FromVisual(this);
                System.Windows.Point targetPoints = source.CompositionTarget.TransformFromDevice.Transform(locationFromScreen);
                objKeyboard.Top = targetPoints.Y + CFactoryReset.FactoryResetInstance.Height / 2;
                objKeyboard.Left = targetPoints.X - 100;
                objKeyboard.ShowDialog();
                return strKeyText;
            }
            finally
            {
                this.Cursor = Cursors.Arrow;
            }
        }

        #endregion

        #region IDisposable Members

        /// <summary>
        /// Variable used to identity whether this object is already disposed or not.
        /// </summary>
        private bool disposed = false;

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        private void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    this.CleanupWPFObjectTopControls((i) =>
                    {
                        // events
                        ((BMC.Presentation.POS.Views.CFactoryReset)(this)).Loaded -= (this.Window_Loaded);                        
                        this.txtAutCode.KeyDown -= (this.txtAutCode_KeyDown);
                        this.txtAutCode.PreviewMouseUp -= (this.txtAutCode_PreviewMouseUp);
                        this.btnOK.Click -= (this.btnOK_Click);
                        this.btnCancel.Click -= (this.btnCancel_Click);
                    },
                    (c) =>
                    {
                    });
                    this.WriteLog("CFactoryReset objects are released successfully.");

                }
                disposed = true;
            }
        }

        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="CFactoryReset"/> is reclaimed by garbage collection.
        /// </summary>
        ~CFactoryReset()
        {
            Dispose(false);
        }

        #endregion

    }
}
