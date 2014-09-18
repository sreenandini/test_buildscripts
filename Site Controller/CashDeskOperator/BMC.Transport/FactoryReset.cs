using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.Transport.CashDeskOperatorEntity
{
    public enum FactoryResetMode
    {
        MasterReset = 1,
        ResetToInitailConfiguration = 2,
        ResetAccountInformation = 3
    }

   public class FactoryReset
    {
       public FactoryReset()
       { }
        public string Server
        {
            get;
            set;

        }
        public string UserID
        {
            get;
            set;
        }
        public string Password
        {
            get;
            set;
        }
        public string ServerInstance
        {
            get;
            set;
        }
        public string RegistryKeyValue
        {
            get;
            set;
        }
        public string ConnectionTimeOut
        {
            get;
            set;
        }
        public string DataBase
        {
            get;
            set;
        }
        public string TicketLocationCode
        {
            get;
            set;
        }
        public string TicketingConnectionString
        {
            get;
            set;
        }
        public string ExchangeConnectionString
        {
            get;
            set;
        }
        public string CMPConnectionString
        {
            get;
            set;
        }
        public string ODBCRegKeyValue
        {
            get;
            set;
        }
        public string NetLoggerRegKeyValue
        {
            get;
            set;
        }        
        public string strBackupPath
        {
            get ;
            set ;
        }
        public string strBackupFileName
        {
            get;
            set;
        }
        public string strZipFileName
        {
            get;
            set;
        }
        public int iSeverity
        {
            get;
            set;
        }
        public string BackUpDataBase
        {
            get;
            set;
        }
        
    }
  
}
