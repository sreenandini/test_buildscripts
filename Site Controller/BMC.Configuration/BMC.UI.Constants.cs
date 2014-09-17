/*******************************************************************************************************
 *  Revision History
 *  Name            TrackCode   Modified Date   Change Description
 *  Selva Kumar S   S001        27th Jul 2012   Registry path to store information cash dispenser
 *                                              
 * ****************************************************************************************************/

using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace BMC.ExchangeConfig
{
    public static class UIConstants
    {
        //MSMQ related
        public const string strMSMQservice = "MSMQ";
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
        public const string StartUpPath = "";
        public const string strDHCPServerIP = "Cashmaster\\BMCDHCP\\ServerIP";
        public const string strDHCPEnabled = "Cashmaster\\Exchange\\EnableDhcp";
        public const string strMulticastip = "Cashmaster\\Exchange\\multicastip";
        public const string strInterface = "Cashmaster\\Exchange\\interface";
        public const string strDefaultServerIP = "Cashmaster\\Exchange\\Default_Server_IP";
        public const string strBindIP = "Cashmaster\\Exchange\\BindIPAddress";
        public const string strEncryptEnable = "Cashmaster\\Exchange\\Events\\EncryptEnable";
        public const string strEnableRSAEncrypt = "Cashmaster\\Exchange\\Events\\RSAEnable";
        public const string strDisablemachine = "Cashmaster\\Exchange\\Events\\DisMachineWhenRemove";
        public const string strBGSWebservice = "Cashmaster\\BGSWebService";
        public const string strLFMWebservice = "Cashmaster\\LFMWebService";
        public const string strSQLConnect = "Cashmaster\\SQLConnect";
        public const string strPCConnect = "Cashmaster\\PCConnect";
        public const string strSQLConnectEx = "Cashmaster\\SQLConnectEx";
        public const string strNetLogger = "NetLogger\\ServerIP";
        public const string SQLCommandTimeOut = "SQLCommandTimeout";
        public const string strCMPWebserviceURL = "Cashmaster\\CMPWebserviceURL";

        public const string strODBCRegPath = "Software\\ODBC\\ODBC.INI";
        public const string DefaultLogDir = "DefaultLogDir";

        #region +S001 START
        public const string strCDServerInfo = "\\Cashmaster\\CDServerInfo";
        public const string strCDServerPwd = "\\Cashmaster\\CDServerPwd";
        #endregion +S001 END

        //setting table related
        public const string CMPCONNECTIONSETTING = "EPIGatewayConnstr";
        public const string TICKETINGCONNECTIONSETTING = "Ticketing.Connection";
        public const string TICKETLOCATIONCODENAME = "TICKET_LOCATION_CODE";
        public const string IsCertificateRequired = "IsCertificateRequired";
        public const string CertificateIssuer = "CertificateIssuer";
        public const string PTGatewayIP = "PT_GATEWAY_IP";
        public const string PTGatewayPortNo = "PT_GATEWAY_PORT_NO";
        public const string PTGatewayMsgRspTimeOut = "PT_GATEWAY_MSG_RESP_TIMEOUT";
        public const string CMPAppUserName = "CMPAPP_UNAME";
        public const string CMPAppPassword = "CMPAPP_PWD";
        public const string CMPKioskURL = "CMP_KIOSKURL";         
        public const string DisableMachineOnRemoval = "DISABLE_MACHINE_ON_REMOVAL";
        public const string CMPMode = "CMPMode";

        //Captions for messageboxes related
        public const string strBMCConfig = "BMC Exchange Configuration";
        public const string strExchangeDBName = "Exchange";
        public const string strTicketingDBName = "Ticketing";
        public const string strCMPDBName = "EXTSYSMSG";

        //SDT Setting
        public const string SDTSendPTPortNo = "SDT_SendPTPortNo";
        public const string SDTReceivePTPortNo = "SDT_ReceivePTPortNo";
        public const string SDTSendCAPortNo = "SDT_SendCAPortNo";
        public const string SDTReceiveCAPortNo = "SDT_ReceiveCAPortNo";
    }
}