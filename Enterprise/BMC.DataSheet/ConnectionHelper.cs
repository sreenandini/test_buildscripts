using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Data.SqlClient;
using BMC.Common.LogManagement;
using BMC.Common.ExceptionManagement;

namespace CustomReports
{ 
    public static class ConnectionHelper
    {
        static string strConnect = string.Empty;
        static string strServerName = string.Empty;
        static string strUserName = string.Empty;
        static string strPassword = string.Empty;
        static string strDatabaseName = string.Empty;

        public static string EnterpriseConnectionString
        {
            get
            {
                try
                {
                    //string str_TypeOfConnection = ConfigurationManager.AppSettings["ConnectionFlag"];
                    //if (str_TypeOfConnection.ToUpper() == "TRUE")
                    //    strConnect = ConfigurationManager.AppSettings["ConnectionString"];
                    //else
                        strConnect = BMC.Common.Utilities.DatabaseHelper.GetEnterpriseConnectionString();

                    if (strConnect != string.Empty)
                    {
                        try
                        {
                            List<string> databaseInfo = strConnect.Split(';').ToList();

                            ServerName = databaseInfo[0].Split('=')[1].ToString();
                            UserName = databaseInfo[2].Split('=')[1].ToString();
                            Password = databaseInfo[3].Split('=')[1].ToString();
                            DatabaseName = databaseInfo[1].Split('=')[1].ToString();

                            LogManager.WriteLog("Database details retrieved successfully",
                                BMC.Common.LogManagement.LogManager.enumLogLevel.Info);
                        }
                        catch (Exception ex)
                        {
                            LogManager.WriteLog("Error in retrieving the database details",
                                LogManager.enumLogLevel.Error);
                            ExceptionManager.Publish(ex);
                        }
                    }
                    else
                    {
                        LogManager.WriteLog("Error in retrieving the database details",
                            LogManager.enumLogLevel.Error);
                    }
                }

                catch (SqlException ex)
                { ExceptionManager.Publish(ex); }

                catch (Exception ex)
                { ExceptionManager.Publish(ex); }
                
                return strConnect;
            }
        }

        public static string ServerName
        {
            get { return ConnectionHelper.strServerName; }
            set { ConnectionHelper.strServerName = value; }
        }

        public static string UserName
        {
            get { return ConnectionHelper.strUserName; }
            set { ConnectionHelper.strUserName = value; }
        }

        public static string Password
        {
            get { return ConnectionHelper.strPassword; }
            set { ConnectionHelper.strPassword = value; }
        }

        public static string DatabaseName
        {
            get { return ConnectionHelper.strDatabaseName; }
            set { ConnectionHelper.strDatabaseName = value; }
        }
    }

}
