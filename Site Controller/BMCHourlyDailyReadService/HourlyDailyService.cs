using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using BMC.HourlyDailyReadJobs;
using BMC.Common.ExceptionManagement;
using BMC.Common.LogManagement;
using BMC.Common.ConfigurationManagement;
using BMC.Common.Utilities;
namespace BMC
{
    public partial class HourlyDailyService : ServiceBase
    {
        #region Declarations

        private static bool IsRunning = false;
        System.Timers.Timer servicetimer;
        int TimerCount = 0;
        HourlyServiceHandle oHourlyServiceHandle = new HourlyServiceHandle();
        DailyReadServiceHandle oDailyReadServiceHandle = new DailyReadServiceHandle();
        StackerServiceHandle oStackerServiceHandle = new StackerServiceHandle();
     
        private System.Timers.Timer deltaTimer;
        private string strAlert = string.Empty;
        #endregion Declarations

        public HourlyDailyService()
        {
            InitializeComponent();
            HourlyDailyEntity.sRegistryPath = ConfigManager.Read("RegistryPath");
        }

        protected override void OnStart(string[] args)
        {

            oHourlyServiceHandle.Init();
            oDailyReadServiceHandle.Init();

            LogManager.WriteLog("Load Initial Settings..", LogManager.enumLogLevel.Info);
            LoadInitialSettings();
            servicetimer.Enabled = true;
            //}
            LogManager.WriteLog("Checking Stacker Level Setting..", LogManager.enumLogLevel.Info);


            try
            {
                strAlert = oStackerServiceHandle.GetSettingDetail("StackerFeature");
            }
            catch (Exception)
            {
                LogManager.WriteLog("Stacker Level Setting not found", LogManager.enumLogLevel.Info);
            }

            if (strAlert != null && strAlert.Trim().ToUpper().Equals("TRUE"))
            {
                oStackerServiceHandle.Init();
                oStackerServiceHandle.DoWork();
            }
            else
            {
                LogManager.WriteLog("Stacker Feature Setting Mode is disabled", LogManager.enumLogLevel.Info);
            }
           
            // Calculating MGMD Delta
            LogManager.WriteLog("Calculating MGMD Delta..", LogManager.enumLogLevel.Info);
            CalculateDelta();

        }

        void servicetimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            RunService();
        }

        protected override void OnStop()
        {
            oHourlyServiceHandle.UnInit();
            oDailyReadServiceHandle.UnInit();
           
            if (strAlert != null && strAlert.Trim().ToUpper().Equals("TRUE"))
            {
                oStackerServiceHandle.UnInit();
            }
            servicetimer.Enabled = false;
            deltaTimer.Stop();
        }

        private void LoadInitialSettings()
        {
            RegistrySetting oRegistrySetting = new RegistrySetting();
            try
            {
                servicetimer = new System.Timers.Timer((HourlyDailyEntity.HourlyReadInterval != 0) ?
                            CheckInterval(HourlyDailyEntity.HourlyReadInterval) : 1000);
                servicetimer.Elapsed += new System.Timers.ElapsedEventHandler(servicetimer_Elapsed);
                DBConnect.InitialSettings();
                HourlyDailyEntity.HasReadRunWithHourly = false;  // Fix for Read run twice for a day
                HourlyDailyEntity.ShouldReadRunWithHourly = HourlyDailyEntity.DailyAutoReadTime.Split(':')[1].ToString() == "00" ? true : false;
                oRegistrySetting.ReadRegistrySettings(HourlyDailyEntity.sRegistryPath);
                LogManager.WriteLog("HourlyReadHour from Registry: " + HourlyDailyEntity.HourlyReadHour.ToString() + " Date format from Registry: " + HourlyDailyEntity.LastAutoRead.ToString() + " Regional setting date format: " + System.Globalization.CultureInfo.CurrentCulture.DisplayName + " Service started", LogManager.enumLogLevel.Info);
                //HourlyDailyEventLog.WriteToEventLog(HourlyDailyEntity.EventLogName,"LoadInitialSettings: ", "Hourly Daily Service initiated", EventLogEntryType.Information);                
            }
            catch (Exception ex)
            {
                //HourlyDailyEventLog.WriteToEventLog(HourlyDailyEntity.EventLogName, "LoadInitialSettings: ", "Message: " + ex.Message + "Source: " + ex.Source, EventLogEntryType.Error);
                ExceptionManager.Publish(ex);
            }
        }

        public void RunService()
        {
            //Dictionary<string, string> dictServiceentries = new Dictionary<string, string>();
            RegistrySetting oRegistrySetting = new RegistrySetting();

            try
            {

#if (DEBUG)
                {
                    if (IsRunning == false)
                    {
                        IsRunning = true;
                        servicetimer.Enabled = true;
                        TimerCount = TimerCount + 1;
                        oRegistrySetting.ReadRegistrySettings(HourlyDailyEntity.sRegistryPath);
                        if (DateTime.Now.Hour != HourlyDailyEntity.HourlyReadHour)
                        {
                            //LogManager.WriteLog("The Hour: " + HourlyDailyEntity.HourlyReadHour.ToString(), LogManager.enumLogLevel.Debug);
                            if (DBConnect.CheckSqlConnectionExists())
                            {
                                DBConnect.InitialSettings();
                                oHourlyServiceHandle.DoWork();
                                HourlyDailyEntity.HourlyReadHour = DateTime.Now.Hour;
                                dictServiceentries.Add(HourlyDailyEntity.sRegistryPath + "\\\\HourlyReadHour", HourlyDailyEntity.HourlyReadHour.ToString());
                                oRegistrySetting.SetRegistryEntries(dictServiceentries, HourlyDailyEntity.sRegistryPath);
                            }
                        }
                        if (TimerCount == 10)
                        {
                            oRegistrySetting.ReadRegistrySettings(HourlyDailyEntity.sRegistryPath);
                            oDailyReadServiceHandle.DoWork();
                            TimerCount = 0;
                        }
                        IsRunning = false;
                    }
                }
#else
                {
                    if (IsRunning == false)
                    {
                        IsRunning = true;
                        servicetimer.Enabled = true;
                        TimerCount = TimerCount + 1;
                        oRegistrySetting.ReadRegistrySettings(HourlyDailyEntity.sRegistryPath);
                        if (DateTime.Now.Hour != HourlyDailyEntity.HourlyReadHour)
                        {
                            //LogManager.WriteLog("The Hour: " + HourlyDailyEntity.HourlyReadHour.ToString(), LogManager.enumLogLevel.Debug);
                            if (DBConnect.CheckSqlConnectionExists())
                            {
                                DBConnect.InitialSettings();
                                oHourlyServiceHandle.DoWork();
                                HourlyDailyEntity.HourlyReadHour = DateTime.Now.Hour;
                                BMCRegistryHelper.SetRegKeyValue(HourlyDailyEntity.sRegistryPath, "HourlyReadHour", Microsoft.Win32.RegistryValueKind.String, HourlyDailyEntity.HourlyReadHour.ToString());
                            }
                        }
                        if (TimerCount == 10)
                        {
                            oRegistrySetting.ReadRegistrySettings(HourlyDailyEntity.sRegistryPath);
                            oDailyReadServiceHandle.DoWork();
                            TimerCount = 0;
                        }
                        IsRunning = false;
                    }
                }

#endif
            }

            catch (Exception ex)
            {
                IsRunning = false;
                //HourlyDailyEventLog.WriteToEventLog(HourlyDailyEntity.EventLogName, "RunService: ", "Message: " + ex.Message + "Source: " + ex.Source, EventLogEntryType.Error);
                ExceptionManager.Publish(ex);
            }
        }

        private int CheckInterval(int iValue)
        {
            int iReturnInterval = 0;
            iReturnInterval = (iValue < 1000) ? 1000 : iValue;
            return iReturnInterval;
        }

        /// <summary>
        /// This method runs as a seperate thread and is responsible for calculating MGMD Delta
        /// </summary>
        private void CalculateDelta()
        {
            try
            {
                if (DBConnect.CalculateMGMDDelta())
                    LogManager.WriteLog("MGMD Delta calculation complete.Going to sleep.", LogManager.enumLogLevel.Info);
                deltaTimer = new System.Timers.Timer();
                ConfigManager.SetConfigurationMode(ConfigManager.ConfigurationMode.AppConfig);
                deltaTimer.Interval = Convert.ToDouble(ConfigManager.Read("MGMDDeltaCalculatorIntervalInMinutes")) * 60 * 1000; // converting it into milli seconds
                deltaTimer.Elapsed += new System.Timers.ElapsedEventHandler(deltaTimer_Elapsed);
                deltaTimer.Start();
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("HourlyDailyService.CalculateDelta()-> Exception:" + ex.Message, LogManager.enumLogLevel.Error);
            }
        }

        void deltaTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                LogManager.WriteLog("Delta Timer Elapsed, Calculating MGMD Delta", LogManager.enumLogLevel.Info);
                deltaTimer.Stop();
                if (DBConnect.CheckSqlConnectionExists())
                {
                    if (DBConnect.CalculateMGMDDelta())
                        LogManager.WriteLog("MGMD Delta calculation complete,Going to sleep.", LogManager.enumLogLevel.Info);
                }
                deltaTimer.Start();
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("HourlyDailyService.deltaTimer_Elapsed()-> Exception:" + ex.Message, LogManager.enumLogLevel.Error);
            }
        }



    }
}
