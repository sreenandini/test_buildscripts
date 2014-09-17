using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.Common.ExceptionManagement;
using BMC.Common.ConfigurationManagement;

namespace BMC.MeterAdjustmentTool
{
    public static class MeterGlobals
    {
        public const string ORIGINAL_TABLE = "Original";
        public const string TRANSLATED_TABLE = "Translated";
        public const string RAMRESET_TABLE = "RamReset";
        public const string INFO_TABLE = "Info";

        public static int ReadVarianceThreshold
        {
            get
            {
                return GetVarianceThreashold("MetAdjReadVarianceThreshold");
            }
        }

        public static int HourlyVarianceThreshold
        {
            get
            {
                return GetVarianceThreashold("MetAdjHourlyVarianceThreshold");
            }
        }

        public static int CollectionVarianceThreshold
        {
            get
            {
                return GetVarianceThreashold("MetAdjCollectionVarianceThreshold");
            }
        }

        public static int MGMDVarianceThreshold
        {
            get
            {
                return GetVarianceThreashold("MetAdjMGMDVarianceThreshold");
            }
        }

        private static int GetVarianceThreashold(string keyName)
        {
            int result = 0;

            try
            {
                result = Convert.ToInt32(ConfigManager.GetConfigurationObject().Read(keyName));
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                result = 10000; //default
            }

            return result;
        }

        public static string ExchangeWebUrl114
        {
            get
            {
                try
                {
                    return ConfigManager.GetConfigurationObject().Read("MetAdj114ExchangeWebUrl");
                }
                catch (Exception ex)
                {
                    ExceptionManager.Publish(ex);
                    return "BGSWSAdmin.asmx";
                }
            }
        }
    }
}
