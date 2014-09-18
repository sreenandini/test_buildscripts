using System;
using System.Collections.Generic;
using System.Text;

namespace BMC.Transport.ExchangeConfig
{
    public static class ExchangeConfigRegistryEntities
    {       

        public static string Server
            {get;set;}
        public static string User
            { get; set; }
        public static string Password
            { get; set; }
        public static string RegistryKeyValue
            { get; set; }
        public static string ConnectionTimeOut
            { get; set; }
        public static string DataBase
            { get; set; }
        public static string TicketingConnectionString
            { get; set; }
        public static string ExchangeConnectionString
            { get; set; }
        public static string CMPConnectionString
            { get; set; }
        public static string ODBCRegKeyValue
            { get; set; }
        public static string NetLoggerRegKeyValue
            { get; set; }
        public static string EncryptEnable
            { get; set; }
        public static string DefaultDatabase
            { get; set; }
        public static string DefaultLanguage
            { get; set; }
        public static string ODBCDescription
            { get; set; }
        public static string DataSourceReferenceName
            { get; set; }
        public static string ODBCServer
        { get; set; }
        public static string ODBCServerInstance
        { get; set; }
        public static string ODBCUsername
            { get; set; }
        public static string ODBCPwd
            { get; set; }
        public static string HoneyFrameKeyValue
        { get; set; }
    }
}
