using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using BMC.Common.ConfigurationManagement;
using System.Data.SqlClient;
namespace BMC.EnterpriseReportsDataAccess
{
    public static class DbConnection
    {
      static string strConnect      =   string.Empty;
      static string serverName      =   string.Empty;
      static string userName        =   string.Empty;
      static string password        =   string.Empty;
      static string databaseName    =   string.Empty;


        public static string EnterpriseConnectionString
        {
            get
            {
                try
                {
                    string str_TypeOfConnection = ConfigurationManager.AppSettings["ConnectionFlag"];
                    if (str_TypeOfConnection.ToUpper() == "TRUE")
                        strConnect = ConfigManager.Read("ConnectionString");
                    else
                        strConnect = BMC.Common.Utilities.DatabaseHelper.GetConnectionString();

                    if (strConnect != string.Empty)
                    {
                        try
                        {
                            List<string> databaseInfo = strConnect.Split(';').ToList();

                            ServerName      =   databaseInfo[0].Split('=')[1].ToString();
                            UserName        =   databaseInfo[2].Split('=')[1].ToString();
                            Password        =   databaseInfo[3].Split('=')[1].ToString();
                            DatabaseName    =   databaseInfo[1].Split('=')[1].ToString();

                            BMC.Common.LogManagement.LogManager.WriteLog("Database details retrieved successfully",
                                BMC.Common.LogManagement.LogManager.enumLogLevel.Info);
                        }
                        catch (Exception ex)
                        {
                            BMC.Common.LogManagement.LogManager.WriteLog("Error in retrieving the database details",
                                BMC.Common.LogManagement.LogManager.enumLogLevel.Error);
                            BMC.Common.ExceptionManagement.ExceptionManager.Publish(ex);                            
                        }
                    }
                    else
                    {
                        BMC.Common.LogManagement.LogManager.WriteLog("Error in retrieving the database details", 
                            BMC.Common.LogManagement.LogManager.enumLogLevel.Error);                        
                    }
                }

                catch (SqlException ex)
                { BMC.Common.ExceptionManagement.ExceptionManager.Publish(ex); }

                catch (Exception ex)
                { BMC.Common.ExceptionManagement.ExceptionManager.Publish(ex); }
                return strConnect;

            }
        }

        public static string ServerName
        {
            get { return DbConnection.serverName; }
            set { DbConnection.serverName = value; }
        }

        public static string UserName
        {
            get { return DbConnection.userName; }
            set { DbConnection.userName = value; }
        }

        public static string Password
        {
            get { return DbConnection.password; }
            set { DbConnection.password = value; }
        }

        public static string DatabaseName
        {
            get { return DbConnection.databaseName; }
            set { DbConnection.databaseName = value; }
        }
    }
}