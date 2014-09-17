using System;
using System.Collections.Generic;
using System.Text;

namespace BMC.Transport.EnterpriseConfig
{
    public static class EnterpriseConfigRegistryEntities
    {
        private static string strServer = string.Empty;
        private static string strUser = string.Empty;
        private static string strPassword = string.Empty;
        private static string strRegistryKey = string.Empty;
        private static string strDataBase = string.Empty;
        private static string strConnectionTimeOut = string.Empty;
        private static string strTicketConnect = string.Empty;
        private static string strEnterpriseConnect = string.Empty;
        private static string strCMPConnect = string.Empty;
        private static string strODBCRegkey = string.Empty;
        private static string strNetLogRegkey = string.Empty;
        private static string strEncryptEnable= string.Empty;
        private static string strLGEConnect = string.Empty;
        public static string Server
        {
            get
            {
                return strServer;
            }
            set
            {
                strServer = value;
            }
        }

        public static string User
        {
            get
            {
                return strUser;
            }
            set
            {
                strUser = value;
            }
        }

        public static string Password
        {
            get
            {
                return strPassword;
            }
            set
            {
                strPassword = value;
            }
        }


        public static string RegistryKeyValue
        {
            get
            {
                return strRegistryKey;
            }
            set
            {
                strRegistryKey = value;
            }
        }

        public static string ConnectionTimeOut
        {
            get
            {
                return strConnectionTimeOut;
            }
            set
            {
                strConnectionTimeOut = value;
            }
        }

        public static string DataBase
        {
            get
            {
                return strDataBase;
            }
            set
            {
                strDataBase = value;
            }
        }
        public static string MeterAnalysisConnectionString
        {
            get
            {
                return strTicketConnect;
            }
            set
            {
                strTicketConnect = value;
            }
        }
        public static string EnterpriseConnectionString
        {
            get
            {
                return strEnterpriseConnect;
            }
            set
            {
                strEnterpriseConnect = value;
            }
        }
      
        public static string ODBCRegKeyValue
        {
            get
            {
                return strODBCRegkey;
            }
            set
            {
                strODBCRegkey = value;
            }
        }
        public static string NetLoggerRegKeyValue
        {
            get
            {
                return strNetLogRegkey;
            }
            set
            {
                strNetLogRegkey = value;
            }
        }
        public static string LGEConnectionString
        {
            get
            {
                return strLGEConnect;
            }
            set
            {
                strLGEConnect = value;
            }
        }
    }
}
