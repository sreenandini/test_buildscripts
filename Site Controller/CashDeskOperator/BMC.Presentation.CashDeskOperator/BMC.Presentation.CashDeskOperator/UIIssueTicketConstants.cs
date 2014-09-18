using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace  BMC.Presentation
{
    public class UIIssueTicketConstants
    {
        /// <summary>
        /// Constants & Variables used in Issue Ticket functions
        /// </summary>
        ///  ///  /// Method Revision History
        ///
        /// Author             Date              Description
        /// ---------------------------------------------------
        /// Vineetha Mathew         15-Oct-2008      Intial Version 
        public  const string STANDARDTICKET="1";
        public  const string PROMOTICKET="7";        
    }
    public static class FactoryResetConstants
    {
        public static List<Key> AllowedKeys = new List<Key> 
        {
            Key.D0, 
            Key.D1,
            Key.D2,
            Key.D3,
            Key.D4,
            Key.D5,
            Key.D6,
            Key.D7,
            Key.D8,
            Key.D9,

            Key.NumPad0,
            Key.NumPad1,
            Key.NumPad2,
            Key.NumPad3,
            Key.NumPad4,
            Key.NumPad5,
            Key.NumPad6,
            Key.NumPad7,
            Key.NumPad8,
            Key.NumPad9,

            Key.Enter,
            Key.Back,
            Key.Delete,
            Key.LeftAlt,
            Key.RightAlt,
            Key.Left,
            Key.Right,
            Key.LeftShift,
            Key.RightShift,
            Key.Decimal,
            Key.Home,
            Key.End,
            Key.Tab

        };
            //MSMQ related
            public const string strMSMQservice = "MSMQ";
            public const string strExchangeQueuePath = ".\\private$\\Exchangequeue";

            //Exchange related
            public const string strDHCPServerIP = "Honeyframe\\Cashmaster\\Exchange\\Default_Server_IP";
            public const string strDHCPEnabled = "Honeyframe\\Cashmaster\\Exchange\\EnableDhcp";
            public const string strBindIP = "Honeyframe\\Cashmaster\\Exchange\\BindIPAddress";
            public const string strBGSWebservice = "Honeyframe\\Cashmaster\\BGSWebService";
            public const string strSQLConnect = "Honeyframe\\Cashmaster\\SQLConnect";
            public const string strSQLConnectEx = "Honeyframe\\Cashmaster\\SQLConnectEx";
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

            //Config Constants

            public const string strConfigServicelist = "ServicesList";
            public const string strConfigRegistrypath = "RegistryPath";
            public const string strConfigNetloggerpath = "NetLoggerPath";

            //Application Constants
            public const string strClearDbScript = "\\Exchange Server\\Database\\Scripts\\Clearprelivedata(Exchange).sql";
            public const string strStartUpFolder="Exchange Server";
            public const string strDataArchive="\\Exchange Server\\Database\\DataArchiveBackup";



        
    }

    public static class CustomerDetailsConstants
    {
        public static List<Key> AllowedAlphabets = new List<Key>         
            {    
                Key.A,
                Key.B,
                Key.C,
                Key.D,
                Key.E,
                Key.F,
                Key.G,
                Key.H,
                Key.I,
                Key.J,
                Key.K,
                Key.L,
                Key.M,
                Key.N,
                Key.O,
                Key.P,
                Key.Q,
                Key.R,
                Key.S,
                Key.T,
                Key.U,
                Key.V,
                Key.W,
                Key.X,
                Key.Y,
                Key.Z,         
                

                Key.Enter,
                Key.Back,
                Key.Delete,
                Key.LeftAlt,
                Key.RightAlt,
                Key.Left,
                Key.Right,
                Key.LeftShift,
                Key.RightShift,                
                Key.Home,
                Key.End,
                Key.Tab,
                Key.Space

        };
        public static List<Key> AllowedNumerics = new List<Key>         
            {    
                Key.D0, 
                Key.D1,
                Key.D2,
                Key.D3,
                Key.D4,
                Key.D5,
                Key.D6,
                Key.D7,
                Key.D8,
                Key.D9,

                Key.NumPad0,
                Key.NumPad1,
                Key.NumPad2,
                Key.NumPad3,
                Key.NumPad4,
                Key.NumPad5,
                Key.NumPad6,
                Key.NumPad7,
                Key.NumPad8,
                Key.NumPad9,               
                
                Key.Enter,
                Key.Back,
                Key.Delete,
                //Key.LeftAlt,
                //Key.RightAlt,
                Key.Left,
                Key.Right,
                //Key.LeftShift,
                //Key.RightShift,                
                Key.Home,
                Key.End,
                Key.Tab


        };

        public static List<Key> AllowedSpecialCharacters = new List<Key> 
        {
            Key.OemQuestion,
            Key.OemComma
        };
    }
}
