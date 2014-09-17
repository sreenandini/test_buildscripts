using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using BMC.EventsTransmitter.DataAccess;

namespace BMC.EventsTransmitter.Utils
{

    public class Settings
    {
        public static string STMServerIP;
        public static string EventTransmitterSourceQ;
        public static int STMServerPort;
        public static int EventTransmit_RecoverOnError;
        public static int EventTransmit_RetryRepeat;
        public static int iMaxTransmissionRetry;
        public static int IsTransmitterEnabled;
        public static string AllowedMessages;
        public static string Version;
        //public static int LogMode=0;
        public static bool IsInitialized { get; private set; }
        public static int DeleteMessageOnParseErr;
        public static int RefreshSiteDetailsInMinutes;
        static  Log Logger = Log.GetInstance(); 
        public static void Initialize()
        {
            try
            {
                ModuleSettings();
                Logger.Info(string.Format("TRANSMITTER SETTINGS [Mode:{0}]", ConfigurationManager.AppSettings["EventTrasnmitter_SettingsMode"].NulltoString("DEFAULT")));
                switch (ConfigurationManager.AppSettings["EventTrasnmitter_SettingsMode"].NulltoString("APP"))
                {
                    case "APP":
                        LoadSettingsFromConfig();
                        break;
                    case "DB":
                        LoadSettingsFromDB();
                        break;
                    case "REG":
                        LoadSettingFromReg();
                        break;
                    default:
                        LoadSettingsFromConfig();
                        break;
                }
                Logger.Info(string.Format("SOURCE QUEUE       : {0}", Settings.EventTransmitterSourceQ));
                Logger.Info(string.Format("SERVER IP          : {0}", Settings.STMServerIP));
                Logger.Info(string.Format("SERVER PORT        : {0}", Settings.STMServerPort));
                Logger.Info(string.Format("ALLOWED MESSAGES   : {0}", Settings.AllowedMessages));
                IsInitialized = true;
            }
            catch (Exception Ex)
            {
                Logger.Error("Settings","Initialize",Ex);
                IsInitialized = false;
                throw Ex;
            }
        }

        private static void LoadSettingFromReg()
        {
            Logger.Info("LOADING INITIAL SETTINGS [Mode:REGISTRY]...");
            throw new NotImplementedException();
        }

        private static void LoadSettingsFromDB()
        {
            Logger.Info("LOADING INITIAL SETTINGS [Mode:DB]...");
            DataAdapter oDB = new DataAdapter();
            Settings.IsTransmitterEnabled = oDB.GetSetting("IsTransmitterEnabled").NullToInt(1);
            Settings.STMServerIP = oDB.GetSetting("STMServerIP").NulltoString("");
            Settings.STMServerPort = oDB.GetSetting("STMServerPort").NullToInt(5555);
            Settings.Version = oDB.GetSetting("PRODUCTVERSION").NulltoString("");
        }

        private static void LoadSettingsFromConfig()
        {
            Logger.Info("LOADING INITIAL SETTINGS [Mode:APPCONFIG]...");
            Settings.IsTransmitterEnabled = ConfigurationManager.AppSettings["IsTransmitterEnabled"].NullToInt(1);
            Settings.STMServerIP = ConfigurationManager.AppSettings["STMServerIP"].NulltoString("127.0.0.1");
            Settings.STMServerPort = ConfigurationManager.AppSettings["STMServerPort"].NullToInt(5555);
            Settings.Version = AssemblyDetails.VersionAsNumber;
         }
        private static void ModuleSettings()
        {
            Settings.EventTransmit_RetryRepeat = ConfigurationManager.AppSettings["EventTransmit_RetryRepeat"].NullToInt(1);
            Settings.EventTransmit_RecoverOnError = ConfigurationManager.AppSettings["EventTransmit_RecoverOnError"].NullToInt(1);
            Settings.iMaxTransmissionRetry = ConfigurationManager.AppSettings["iMaxTransmissionRetry"].NullToInt(3);
            if (Settings.iMaxTransmissionRetry < 3) Settings.iMaxTransmissionRetry = 3;
            Settings.EventTransmitterSourceQ = ConfigurationManager.AppSettings["EventTrasnmitterSrcQ"].NulltoString(@".\private$\STMQueue");
            Settings.DeleteMessageOnParseErr = ConfigurationManager.AppSettings["DeleteMessageOnParseErr"].NullToInt(0);
            Settings.AllowedMessages = ConfigurationManager.AppSettings["STMAllowedMessages"].NulltoString();
            Settings.RefreshSiteDetailsInMinutes = ConfigurationManager.AppSettings["RefreshSiteDetailsInMinutes"].NullToInt(60);
            //switch (ConfigurationManager.AppSettings["LogMode"].NulltoString("DEBUG"))
            //{
            //    case "DEBUG":
            //        Settings.LogMode = 0;
            //        break;
            //    case "INFO":
            //        Settings.LogMode = 1;
            //        break;
            //    case "WARNING":
            //        Settings.LogMode = 2;
            //        break;
            //    case "ERROR":
            //        Settings.LogMode = 3;
            //        break;
            //    default:
            //        Settings.LogMode = 0;
            //        break;
            //}

        }
    }
}
