using System;
using System.Data;
using System.ServiceProcess;
using System.Data.SqlClient;
using BMC.Common.ConfigurationManagement;
using BMC.Common.LogManagement;
using BMC.Common.ExceptionManagement;
using BMC.Business.NetworkService;
using BMC.DBInterface.NetworkService;
using System.Diagnostics;
using System.Threading;
using BMC.Transport.NetworkService;



namespace MyService
{
    public partial class NetworkService : ServiceBase
    {
        #region public static variables
        string _strConnectionString = String.Empty;
        public static bool BConnectivityProcessing;
        public static bool BProcessing;
        public static int iEDProcessingCount = 0;
        #endregion


        #region ComObjects

        private EnableDisable _EnableDisable = null;
        private InstantPeriodicIntervalHandler _instantPeriodicInterval = null;
        private AFTEnableDisable _enableDisableAFT = null;
        private EmployeeMasterCard _empMastercard = null;
        private TITO _TITO = null;

        #endregion

        #region Counters
        //Counter for COM Object Reinitialization.
        private int iCOMReInitializeTimerTriggerCountLimit = 0;

        #endregion


        #region Constructor
        public NetworkService()
        {
            InitializeComponent();
        }
        #endregion

        #region Events

        protected override void OnStart(string[] args)
        {
            //System.Diagnostics.Debugger.Break();
            LogManager.WriteLog(string.Format("Starting Service ({0:D}).", Process.GetCurrentProcess().Id), LogManager.enumLogLevel.Info);
            IsStopRequested = false;

            try
            {
                //Get the connection string.
                _strConnectionString = DBBuilder.GetConnectionString();

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

            // Initialize application settings (App.config, setting table, etc...)
            this.InitSettings();

            //ENABLE DISABLE
            if (Convert.ToBoolean(ConfigManager.Read("MachineConfig")) == true)
                _EnableDisable = new EnableDisable();

            //TITO
            if (Convert.ToBoolean(ConfigManager.Read("TITO")) == true)
                _TITO = new TITO();

            //AFTEnableDisable
            if (Convert.ToBoolean(ConfigManager.Read("AFTEnableDisable")) == true)
                _enableDisableAFT = new AFTEnableDisable();

            //Employee Master card broadcast
            if (Convert.ToBoolean(ConfigManager.Read("EmployeecardBroadcast")) == true)
                _empMastercard = new EmployeeMasterCard();

            //Instant Periodic Interval
            if (Convert.ToBoolean(ConfigManager.Read("InstantPeriodic")) == true)
                _instantPeriodicInterval = new InstantPeriodicIntervalHandler();

            //Request/Response Wait Configuration
            NetworkServiceSettings.RequestWaitTime = Convert.ToInt32(ConfigManager.Read("RequestWaitMilliSeconds")); 
            NetworkServiceSettings.DBHitWaitTime = Convert.ToInt32(ConfigManager.Read("DBHitWaitMilliSeconds"));
            


            LogManager.WriteLog("Started Service Successfully.", LogManager.enumLogLevel.Info);
        }

       //public void Start()
       // {
       //     //System.Diagnostics.Debugger.Break();
       //     LogManager.WriteLog(string.Format("Starting Service ({0:D}).", Process.GetCurrentProcess().Id), LogManager.enumLogLevel.Info);
       //     IsStopRequested = false;

       //     try
       //     {
       //         //Get the connection string.
       //         _strConnectionString = DBBuilder.GetConnectionString();

       //     }
       //     catch (Exception ex)
       //     {
       //         ExceptionManager.Publish(ex);
       //     }

       //     // Initialize application settings (App.config, setting table, etc...)
       //     this.InitSettings();

       //     //ENABLE DISABLE
       //     if (Convert.ToBoolean(ConfigManager.Read("MachineConfig")) == true)
       //         _EnableDisable = new EnableDisable();

       //     //TITO
       //     if (Convert.ToBoolean(ConfigManager.Read("TITO")) == true)
       //         _TITO = new TITO();

       //     //AFTEnableDisable
       //     if (Convert.ToBoolean(ConfigManager.Read("AFTEnableDisable")) == true)
       //         _enableDisableAFT = new AFTEnableDisable();

       //     //Employee Master card broadcast
       //     if (Convert.ToBoolean(ConfigManager.Read("EmployeecardBroadcast")) == true)
       //         _empMastercard = new EmployeeMasterCard();

       //     //Instant Periodic Interval
       //     if (Convert.ToBoolean(ConfigManager.Read("InstantPeriodic")) == true)
       //         _instantPeriodicInterval = new InstantPeriodicIntervalHandler();

       //     //Request/Response Wait Configuration
       //     NetworkServiceSettings.RequestWaitTime = Convert.ToInt32(ConfigManager.Read("RequestWaitMilliSeconds"));
       //     NetworkServiceSettings.DBHitWaitTime = Convert.ToInt32(ConfigManager.Read("DBHitWaitMilliSeconds"));



       //     LogManager.WriteLog("Started Service Successfully.", LogManager.enumLogLevel.Info);
       // }
        protected override void OnStop()
        {
            IsStopRequested = true;

            this.DisposeObject(ref _EnableDisable, "_EnableDisable", "COM Object");
            this.DisposeObject(ref _enableDisableAFT, "_enableDisableAFT", "COM Object");
            this.DisposeObject(ref _empMastercard, "_empMastercard", "COM Object");
            this.DisposeObject(ref _TITO, "_TITO", "COM Object");
            this.DisposeObject(ref _instantPeriodicInterval, "_instantPeriodicInterval", "COM Object");

            LogManager.WriteLog(string.Format("Service ({0:D}) was stopped successfully.", Process.GetCurrentProcess().Id), LogManager.enumLogLevel.Info);
            Thread.Sleep(3000);
        }

        #endregion

        #region Methods

        #endregion

        #region Performance Methods
        /*  -----------------------------
         *  PERFORMANCE RELATED CODE
         *  -----------------------------
         *  A.VINOD KUMAR
         *  20/10/2011 18:03:29
         *  -----------------------------
         */
        /// <summary>
        /// Is Stop Requested
        /// </summary>
        private bool _isStopRequested = false;

        /// <summary>
        /// Gets or sets a value indicating whether this instance is stop requested.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is stop requested; otherwise, <c>false</c>.
        /// </value>
        public bool IsStopRequested
        {
            get { return _isStopRequested; }
            set
            {
                _isStopRequested = value;
                ObjectStateNotifier.NotifyObservers(value ?
                    ObjectState.Deactivated :
                    ObjectState.Activated);
            }
        }

        /// <summary>
        /// Enables the timer.
        /// </summary>
        /// <param name="timer">The timer.</param>
        private void EnableTimer(System.Timers.Timer timer, string componentName)
        {
            try
            {
                timer.Enabled = !IsStopRequested;
                if (IsStopRequested)
                {
                    LogManager.WriteLog("|=> EnableTimer() : Stop Service was requested. So Timer is set to disable state.", LogManager.enumLogLevel.Info);
                }
                else
                {
                    LogManager.WriteLog("|=> EnableTimer() : " + componentName + " was enabled.", LogManager.enumLogLevel.Info);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        /// <summary>
        /// Determines whether this instance can process.
        /// </summary>
        /// <returns>
        /// 	<c>true</c> if this instance can process; otherwise, <c>false</c>.
        /// </returns>
        private bool CanProcess()
        {
            bool result = !IsStopRequested;
            try
            {
                if (IsStopRequested)
                {
                    LogManager.WriteLog("|=> CanProcess() : Stop Service was requested. So Timer is set to disable state.", LogManager.enumLogLevel.Info);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return result;
        }

        /// <summary>
        /// Reads the application related setting.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="settingName">Name of the setting.</param>
        /// <param name="defaultValue">The default value.</param>
        private void ReadAppSetting(ref int value, string settingName, int defaultValue)
        {
            try
            {
                value = Int32.Parse(ConfigManager.Read(settingName));
            }
            catch (Exception)
            {
                value = 60;
            }
            LogManager.WriteLog("|=> Setting Name : " + settingName + ", Value : " + value.ToString() + ".", LogManager.enumLogLevel.Info);
        }

        /// <summary>
        /// Initialize application settings (App.config, setting table, etc...)
        /// </summary>
        private void InitSettings()
        {
            ReadAppSetting(ref iCOMReInitializeTimerTriggerCountLimit, "COMReInitializeTimerTriggerCountLimit", 60);
            LogManager.WriteLog("|=> Settings are initialized successfully.", LogManager.enumLogLevel.Info);
        }

        /// <summary>
        /// Reinitializes the COM object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">The obj.</param>
        /// <param name="timerTriggerCount">The timer trigger count.</param>
        /// <param name="componentName">Name of the component.</param>
        /// <param name="create">The create.</param>
        private void ReinitializeCOMObject<T>(ref T obj, ref int timerTriggerCount,
            string componentName, Func<T> create)
            where T : class, IDisposable
        {
            try
            {
                if ((obj == null) ||
                    (timerTriggerCount > iCOMReInitializeTimerTriggerCountLimit))
                {
                    string text = "Reinitialized.";
                    if (obj == null)
                    {
                        text = "Initialized.";
                    }
                    System.Threading.Interlocked.Exchange(ref timerTriggerCount, 1);

                    this.DisposeObject(ref obj, componentName);
                    obj = create();
                    LogManager.WriteLog("|%%> " + componentName + " COM Object " + text, LogManager.enumLogLevel.Info);
                }
                else
                {
                    System.Threading.Interlocked.Increment(ref timerTriggerCount);
                }

                LogManager.WriteLog("|%%> " + componentName + " Timer Trigger Count : " + timerTriggerCount.ToString(), LogManager.enumLogLevel.Info);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        /// <summary>
        /// Disposes the object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">The obj.</param>
        /// <param name="objectName">Name of the object.</param>
        private void DisposeObject<T>(ref T obj, string objectName)
            where T : class, IDisposable
        {
            this.DisposeObject<T>(ref obj, objectName, string.Empty);
        }

        /// <summary>
        /// Disposes the object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">The obj.</param>
        /// <param name="objectName">Name of the object.</param>
        /// <param name="extra">The extra.</param>
        private void DisposeObject<T>(ref T obj, string objectName, string extra)
            where T : class, IDisposable
        {
            try
            {
                if (obj != null)
                {
                    obj.Dispose();
                    obj = null;
                    LogManager.WriteLog("|%%> " + objectName +
                        (!string.IsNullOrEmpty(extra) ? "(" + extra + ")" : "") +
                        " is disposed successfully.", LogManager.enumLogLevel.Info);
                }
            }
            catch { }
        }
        #endregion
    }
}
