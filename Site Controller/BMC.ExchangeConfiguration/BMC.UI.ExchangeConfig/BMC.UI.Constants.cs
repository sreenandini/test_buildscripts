using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace BMC.UI.ExchangeConfig
{
   public static class UIConstants
    {
       //MSMQ related
       public  const string strMSMQservice = "MSMQ";
       public const string strExchangeQueuePath = ".\\private$\\Exchangequeue";

       //Exchange related
       //public const string strDHCPServerIP = "Honeyframe\\Cashmaster\\BMCDHCP\\ServerIP";       
       //public const string strDHCPEnabled = "Honeyframe\\Cashmaster\\Exchange\\EnableDhcp";
       //public const string strMulticastip = "Honeyframe\\Cashmaster\\Exchange\\multicastip";
       //public const string strInterface = "Honeyframe\\Cashmaster\\Exchange\\interface";
       //public const string strDefaultServerIP = "Honeyframe\\Cashmaster\\Exchange\\Default_Server_IP";
       //public const string strBindIP = "Honeyframe\\Cashmaster\\Exchange\\BindIPAddress";
       //public const string strEncryptEnable = "Honeyframe\\Cashmaster\\Exchange\\Events\\EncryptEnable";
       //public const string strBGSWebservice = "Honeyframe\\Cashmaster\\BGSWebService";
       //public const string strSQLConnect = "Honeyframe\\Cashmaster\\SQLConnect";
       //public const string strSQLConnectEx = "Honeyframe\\Cashmaster\\SQLConnectEx";
       //public const string strNetLogger = "Honeyframe\\NetLogger\\ServerIP";
       public const string StartUpPath = "Software\\Honeyframe";
       public const string strDHCPServerIP = "\\BMCDHCP\\ServerIP";
       public const string strDHCPEnabled = "\\Exchange\\EnableDhcp";
       public const string strMulticastip = "\\Exchange\\multicastip";
       public const string strInterface = "\\Exchange\\interface";
       public const string strDefaultServerIP = "\\Exchange\\Default_Server_IP";
       public const string strBindIP = "\\Exchange\\BindIPAddress";
       public const string strEncryptEnable = "\\Exchange\\Events\\EncryptEnable";
       public const string strEnableRSAEncrypt = "\\Exchange\\Events\\RSAEnable";
       public const string strDisablemachine = "\\Exchange\\Events\\DisMachineWhenRemove";
       public const string strBGSWebservice = "\\Cashmaster\\BGSWebService";
       public const string strSQLConnect = "\\Cashmaster\\SQLConnect";
       public const string strSQLConnectEx = "\\Cashmaster\\SQLConnectEx";
       public const string strNetLogger = "Honeyframe\\NetLogger\\ServerIP";

       public const string strODBCRegPath = "Software\\ODBC\\ODBC.INI";       

       //setting table related
       public const string CMPCONNECTIONSETTING = "EPIGatewayConnstr";
       public const string TICKETINGCONNECTIONSETTING = "Ticketing.Connection";
       public const string TICKETLOCATIONCODENAME = "TICKET_LOCATION_CODE";
       
       //Captions for messageboxes related
       public const string strBMCConfig = "BMC Exchange Configuration";
       public const string strExchangeDBName = "Exchange";
       public const string strTicketingDBName = "Ticketing";
       public const string strCMPDBName = "CMKTSDG";
    }
}