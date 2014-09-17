/* ================================================================================= 
 * Purpose		:	BMC Registry Helper and Database Helper Class
 * File Name	:   DatabaseHelper.cs
 * Author		:	A.Vinod Kumar
 * Modified  	:	08/10/2013
 * ================================================================================= 
 * Revision History :
 * ================================================================================= 
 * 01/01/0001		UNKNOWN         Initial Version
 * 08/10/2013       A.Vinod Kumar   Modified for invoking Enterprise Client and 
 *                                  Exchange Client from same machine.
 * 23/10/2013       A.Vinod Kumar   Modified for saving/loading the data into xml 
 *                                  instead of registry.
 * ===============================================================================*/

using System;
using System.Data.SqlClient;
using System.IO;
using Microsoft.Win32;
using BMC.Common.Persistence;

namespace BMC.Common.Utilities
{
    public enum BMCInstallationType
    {
        None = 0,
        ExchangeServer = 1,
        ExchangeClient = 2,
        EnterpriseServer = 3,
        EnterpriseClient = 4
    }

    public enum BMCCategorizedInstallationTypes
    {
        None = 0,
        Enterprise = 1,
        Exchange = 2
    }

    public enum BMCCategoriedApplicationTypes
    {
        Server = 1,
        Client = 2
    }

    public static class BMCRegistryHelper
    {
        private const string HF = "HONEYFRAME";
        public const string DB_CON_EXC = "Cashmaster";
        public const string DB_CON_ENT = "CashmasterHQ";

        static BMCRegistryHelper()
        {
            InstallationType = GetInstallationType();
        }

        public static BMCInstallationType InstallationType { get; private set; }

        public static BMCCategorizedInstallationTypes ActualInstallationType { get; private set; }

        public static BMCCategorizedInstallationTypes ActiveInstallationType { get; set; }

        public static BMCCategoriedApplicationTypes ActualApplicationType { get; private set; }

        public static BMCInstallationType GetInstallationType()
        {
            BMCInstallationType insType = BMCInstallationType.None;

            try
            {
                string installationType = ConfigApplicationFactory.GetValue("Honeyframe", "InstallationType").ToUpper();
                switch (installationType)
                {
                    case "EXCHANGECLIENT":
                        insType = BMCInstallationType.ExchangeClient;
                        break;

                    case "EXCHANGESERVER":
                        insType = BMCInstallationType.ExchangeServer;
                        break;

                    case "ENTERPRISECLIENT":
                        insType = BMCInstallationType.EnterpriseClient;
                        break;

                    case "ENTERPRISESERVER":
                        insType = BMCInstallationType.EnterpriseServer;
                        break;

                    default:
                        break;
                }

                ActualInstallationType = ((insType == BMCInstallationType.EnterpriseServer) || (insType == BMCInstallationType.EnterpriseClient) ?
                    BMCCategorizedInstallationTypes.Enterprise : BMCCategorizedInstallationTypes.Exchange);
                ActualApplicationType = ((insType == BMCInstallationType.EnterpriseServer) || (insType == BMCInstallationType.ExchangeServer) ?
                    BMCCategoriedApplicationTypes.Server : BMCCategoriedApplicationTypes.Client);

                if (ActiveInstallationType == BMCCategorizedInstallationTypes.None)
                {
                    ActiveInstallationType = ActualInstallationType;
                }
                BMC.Common.LogManagement.LogManager.WriteLog("GetInstallationType returns : " + insType.ToString(), BMC.Common.LogManagement.LogManager.enumLogLevel.Debug);
                BMC.Common.LogManagement.LogManager.WriteLog("ActiveInstallationType  returns : " + ActiveInstallationType.ToString(), BMC.Common.LogManagement.LogManager.enumLogLevel.Debug);
            }
            catch { }

            if (insType == BMCInstallationType.None)
            {
                Environment.FailFast("Installation Type does not available.");
            }
            return insType;
        }

        public static RegistryKey GetRegBaseKey(RegistryHive hive)
        {
            return RegistryKey.OpenBaseKey(hive, (Environment.Is64BitOperatingSystem ? RegistryView.Registry64 : RegistryView.Registry32));
        }

        public static RegistryKey GetRegLocalMachine()
        {
            return GetRegBaseKey(RegistryHive.LocalMachine);
        }

        public static string GetRegKeyValue(string keyName, string valueName)
        {
            return GetRegKeyValue(keyName, valueName, string.Empty);
        }

        private static string GetFullKeyName(string keyName)
        {
            string fullKeyName = !string.IsNullOrEmpty(keyName) ? HF + "\\" + keyName : HF;
            fullKeyName = fullKeyName.Replace(@"\\", @"\");
            return fullKeyName;
        }

        public static string GetRegKeyValue(string keyName, string valueName, object defaultValue)
        {
            string defaultValue2 = string.Empty;
            if (defaultValue != null) defaultValue2 = defaultValue.ToString();
            return ConfigApplicationFactory.GetValue(GetFullKeyName(keyName), valueName, defaultValue2);
        }

        public static void SetRegKeyValue(string keyName, string valueName, RegistryValueKind valueType, object value)
        {
            ConfigApplicationFactory.SetValue(GetFullKeyName(keyName), valueName, value.ToString());
        }

        public static void SetRegKeyValue(BMCInstallationType installationType,
                                            string keyName, string valueName,
                                            RegistryValueKind valueType, object value)
        {
            string fullKeyName = string.Empty;

            switch (ActiveInstallationType)
            {
                case BMCCategorizedInstallationTypes.Exchange:
                    fullKeyName = DB_CON_EXC;
                    break;

                case BMCCategorizedInstallationTypes.Enterprise:
                    fullKeyName = DB_CON_ENT;
                    break;

                default:
                    throw new InvalidDataException("Registry key does not Exist.  Please check if Registry key is set.");
            }

            if (!string.IsNullOrEmpty(fullKeyName) &&
                !string.IsNullOrEmpty(keyName))
            {
                fullKeyName += "\\" + keyName;
            }
            SetRegKeyValue(fullKeyName, valueName, valueType, value);
        }

        public static string GetRegKeyValue(BMCInstallationType installationType,
                                            string keyName, string valueName, object defaultValue)
        {
            string fullKeyName = string.Empty;

            switch (installationType)
            {
                case BMCInstallationType.ExchangeServer:
                case BMCInstallationType.ExchangeClient:
                    fullKeyName = DB_CON_EXC;
                    break;

                case BMCInstallationType.EnterpriseServer:
                case BMCInstallationType.EnterpriseClient:
                    fullKeyName = DB_CON_ENT;
                    break;

                default:
                    throw new InvalidDataException("Registry key does not Exist.  Please check if Registry key is set.");
            }

            if (!string.IsNullOrEmpty(fullKeyName) &&
                !string.IsNullOrEmpty(keyName))
            {
                fullKeyName += "\\" + keyName;
            }
            return GetRegKeyValue(fullKeyName, valueName, defaultValue);
        }

        public static void SetRegKeyValueEncrypt(BMCInstallationType installationType,
                                                     string keyName, string valueName,
                                                    RegistryValueKind valueType, object value)
        {
            string encrypted = Security.CryptEncode.Encrypt(value.ToString());
            SetRegKeyValue(installationType, keyName, valueName, valueType, encrypted);
        }

        public static string GetRegKeyValueDecrypt(BMCInstallationType installationType,
                                                    string keyName, string valueName)
        {
            try
            {
                return Security.CryptEncode.Decrypt(GetRegKeyValue(installationType, keyName, valueName, string.Empty));
            }
            catch
            {
                return string.Empty;
            }
        }

        public static string GetExchangeRegKeyValue(string keyName, string valueName)
        {
            return GetRegKeyValue(BMCInstallationType.ExchangeClient, keyName, valueName, string.Empty);
        }

        public static string GetExchangeRegKeyValueDecrypt(string keyName, string valueName)
        {
            try
            {
                return Security.CryptEncode.Decrypt(GetExchangeRegKeyValue(keyName, valueName));
            }
            catch
            {
                return string.Empty;
            }
        }

        public static string GetEnterpriseRegKeyValue(string keyName, string valueName)
        {
            return GetRegKeyValue(BMCInstallationType.EnterpriseClient, keyName, valueName, string.Empty);
        }

        public static string GetEnterpriseRegKeyValueDecrypt(string keyName, string valueName)
        {
            try
            {
                return Security.CryptEncode.Decrypt(GetEnterpriseRegKeyValue(keyName, valueName));
            }
            catch
            {
                return string.Empty;
            }
        }

        public static string GetRegKeyValueByInstallationType(string keyName, string defaultValue)
        {
            switch (ActiveInstallationType)
            {
                //case BMCInstallationType.ExchangeServer:
                //case BMCInstallationType.ExchangeClient:
                case BMCCategorizedInstallationTypes.Exchange:
                    return BMCRegistryHelper.GetRegKeyValue(BMCInstallationType.ExchangeClient, string.Empty, keyName, defaultValue);

                //case BMCInstallationType.EnterpriseServer:
                //case BMCInstallationType.EnterpriseClient:
                case BMCCategorizedInstallationTypes.Enterprise:
                    return BMCRegistryHelper.GetRegKeyValue(BMCInstallationType.EnterpriseClient, "Settings", keyName, defaultValue);

                default:
                    throw new InvalidDataException("Registry key does not Exist.  Please check if Registry key is set.");
            }
        }

        public static bool IsExchange()
        {
            return ActiveInstallationType == BMCCategorizedInstallationTypes.Exchange;
        }

        public static bool IsEnterprise()
        {
            return ActiveInstallationType == BMCCategorizedInstallationTypes.Enterprise;
        }

        private static bool IsServer()
        {
            return ActualApplicationType == BMCCategoriedApplicationTypes.Server;
        }

        private static bool IsClient()
        {
            return ActualApplicationType == BMCCategoriedApplicationTypes.Client;
        }

        public static bool IsExchangeServer()
        {
            return IsExchange() && IsServer();
        }

        public static bool IsExchangeClient()
        {
            return IsExchange() && IsClient();
        }

        public static bool IsEnterpriseServer()
        {
            return IsEnterprise() && IsServer();
        }

        public static bool IsEnterpriseClient()
        {
            return IsEnterprise() && IsClient();
        }

        public static bool IsLoggingDisabled
        {
            get
            {
                bool result = false;
                Boolean.TryParse(GetRegKeyValue(string.Empty, "DisableLogging"), out result);
                return result;
            }
        }
    }

    public static class DatabaseHelper
    {
        public static string GetConnectionString()
        {
            return GetConnectionString(BMCRegistryHelper.ActiveInstallationType);
        }

        public static string GetConnectionString(BMCCategorizedInstallationTypes installationType)
        {
            switch (installationType)
            {
                case BMCCategorizedInstallationTypes.Exchange:
                    return BMCRegistryHelper.GetRegKeyValueDecrypt(BMCInstallationType.ExchangeClient, string.Empty, "SQLConnect");

                case BMCCategorizedInstallationTypes.Enterprise:
                    return BMCRegistryHelper.GetRegKeyValueDecrypt(BMCInstallationType.EnterpriseClient, "Settings", "SQLConnect");

                default:
                    throw new InvalidDataException("Registry key does not Exist.  Please check if Registry key is set.");
            }
        }

        public static string GetExchangeConnectionString()
        {
            return BMCRegistryHelper.GetExchangeRegKeyValueDecrypt(string.Empty, "SQLConnect");
        }

        public static string GetEnterpriseConnectionString()
        {
            return BMCRegistryHelper.GetEnterpriseRegKeyValueDecrypt("Settings", "SQLConnect");
        }

        public static string GetAuditConnectionString()
        {
            return BMCRegistryHelper.GetEnterpriseRegKeyValueDecrypt("Settings", "AuditSQLConnect");
        }

        public static string GetTicketingConnectionString()
        {
            return BMCRegistryHelper.GetExchangeRegKeyValueDecrypt(string.Empty, "TicketingSQLConnect");
        }

        public static bool StoreConnectionString(string serverName, string database, string loginName, string password, int timeOutInSeconds)
        {
            var connectionString = "Server=" + serverName.Trim() + ";Initial Catalog=" + database.Trim() + ";UID=" + loginName.Trim() + ";PWD=" + password + ";Timeout=" + timeOutInSeconds.ToString() + ";";
            return _StoreConnectionString(connectionString, database);
        }

        public static bool StoreConnectionString(string connectionString)
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(connectionString);
            string database = builder.InitialCatalog;
            return _StoreConnectionString(connectionString, database);
        }

        private static bool _StoreConnectionString(string connectionString, string database)
        {
            //BMCInstallationType installationType = BMCRegistryHelper.GetInstallationType();

            switch (BMCRegistryHelper.ActiveInstallationType)
            {
                //case BMCInstallationType.ExchangeServer:
                //case BMCInstallationType.ExchangeClient:
                case BMCCategorizedInstallationTypes.Exchange:
                    {
                        if (database.ToLower() == "ticketing")
                        {
                            BMCRegistryHelper.SetRegKeyValueEncrypt(BMCInstallationType.ExchangeClient, string.Empty, "TicketingSQLConnect", RegistryValueKind.String, connectionString);
                        }
                        else
                        {
                            BMCRegistryHelper.SetRegKeyValueEncrypt(BMCInstallationType.ExchangeClient, string.Empty, "SQLConnect", RegistryValueKind.String, connectionString);
                        }
                    }
                    break;

                case BMCCategorizedInstallationTypes.Enterprise:
                    {
                        if (database == "Audit")
                        {
                            BMCRegistryHelper.SetRegKeyValueEncrypt(BMCInstallationType.EnterpriseClient, "Settings", "AuditSQLConnect", RegistryValueKind.String, connectionString);
                        }
                        else
                        {
                            BMCRegistryHelper.SetRegKeyValueEncrypt(BMCInstallationType.EnterpriseClient, "Settings", "SQLConnect", RegistryValueKind.String, connectionString);
                        }
                    }
                    break;

                default:
                    break;
            }

            return true;
        }
    }
}
