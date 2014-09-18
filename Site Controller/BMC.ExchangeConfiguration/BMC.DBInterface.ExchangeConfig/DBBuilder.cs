using System;
using System.Collections.Generic;
using System.Text;
using BMC.DataAccess;
using System.Data;
using System.Data.SqlClient;
using BMC.Common.ExceptionManagement;
using BMC.Common.LogManagement;
using System.Collections;
using BMC.Transport.ExchangeConfig;
using System.Diagnostics;
using System.Windows.Forms;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using System.Data.Odbc;
using Microsoft.Win32;
using System.Data.Sql;
using System.Text.RegularExpressions;
using System.IO;
using System.Reflection;
namespace BMC.DBInterface.ExchangeConfig
{
    public static class DBBuilder
    {
                
        /// <summary>
        /// Gets the CMP Gateway Connection String
        /// </summary>
        /// <param name="ExchangeConnectionString"></param>
        /// <returnsCMP Connection string</returns>
        /// Method Revision History
        ///
        /// Author             Date              Description
        /// ---------------------------------------------------
        /// Anuradha        09-Dec-2008        Intial Version 
        /// 
        public static Dictionary<int, string> GetSetting(string ExchangeConnectionString,params SqlParameter[] paramCMPsetting)
        {
           Dictionary<int, string> dCMPSetting=null;
           try
           {
               SqlCommand sqlcommSetting = LoadCommand(DBConstants.RSP_GETSETTING, ExchangeConnectionString, paramCMPsetting);
               dCMPSetting = ExecuteCommand(sqlcommSetting);
           }
           catch (Exception ex)
           {
               LogManager.WriteLog("GetSetting" + ex.Message.ToString() + ex.Source.ToString(), LogManager.enumLogLevel.Error);
               ExceptionManager.Publish(ex);
           }
         
            return dCMPSetting;
        }


        public static DataSet GetInitialSettings(string ExchangeConnectionString)
        {
            try
            {
                return SqlHelper.ExecuteDataset(ExchangeConnectionString, CommandType.StoredProcedure, DBConstants.RSP_GETINITIALSETTING, null);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return null;
            }   
        }

        /// <summary>
        /// Creates the command object based on procedure name.
        /// </summary>
        /// <param name="ExchangeConnectionString"></param>
        /// <param name="procedurename"></param>
        /// <returnsCMP Connection string</returns>
        /// Method Revision History
        ///
        /// Author             Date              Description
        /// ---------------------------------------------------
        /// Anuradha        09-Dec-2008        Intial Version 
        /// 
        private static SqlCommand LoadCommand(string procedurename, string ExchangeConnectionString,params SqlParameter[] paramCMPsetting)
        {
            SqlCommand commCMPSetting = null;
            try
            {
                commCMPSetting = new SqlCommand();
                commCMPSetting.Connection = new SqlConnection(ExchangeConnectionString);
                commCMPSetting.Connection.Open();
                commCMPSetting.CommandText = procedurename;
                commCMPSetting.Parameters.AddRange(paramCMPsetting);
                commCMPSetting.CommandType = CommandType.StoredProcedure;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("LoadCommand" + ex.Message.ToString() + ex.Source.ToString(), LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
            }

            return commCMPSetting;
        }


        /// <summary>
        /// Fill the dataset and add to dictionary
        /// </summary>
        /// <param name="Sqlcommand"></param>
        /// <returnsCMP Connection string</returns>
        /// Method Revision History
        ///
        /// Author             Date              Description
        /// ---------------------------------------------------
        /// Anuradha        09-Dec-2008        Intial Version 
        /// 
        private static Dictionary<int, string> ExecuteCommand(SqlCommand commSetting)
        {
            int ReturnValue = 0;
            Dictionary<int, string> dCMPsetting = new Dictionary<int, string>();
            DataTable dtSetting = new DataTable();

            try
            {
                ReturnValue = commSetting.ExecuteNonQuery();

                dCMPsetting.Add(ReturnValue, commSetting.Parameters["@Setting_Value"].Value.ToString());
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("ExecuteCommand" + ex.Message.ToString() + ex.Source.ToString(), LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
            }
            finally
            {
                commSetting.Connection.Close();
                commSetting.Dispose();
                dtSetting.Dispose();
            }
            return dCMPsetting;
        }


        /// <summary>
        /// Add the input parameters
        /// </summary>
       /// <param name="DataType"></param>
       /// <param name="ParamName"></param>
       /// <param name="Value"></param>
        /// <returnsCMP Connection string</returns>
        /// Method Revision History
        ///
        /// Author             Date              Description
        /// ---------------------------------------------------
        /// Anuradha        09-Dec-2008        Intial Version 
        /// 
        public static SqlParameter AddParameter<T>(string ParamName, DbType DataType, T Value,int Size)
        {
            SqlParameter Param = new SqlParameter();
            Param.DbType = DataType;
            Param.ParameterName = ParamName;
            Param.Value = Value;
            Param.Size = Size;           
            return Param;
        }

        // <summary>
        /// Add the output parameters
        /// </summary>
        /// <param name="DataType"></param>
        /// <param name="ParamName"></param>
        /// <param name="Value"></param>
        /// <returnsCMP Connection string</returns>
        /// Method Revision History
        ///
        /// Author             Date              Description
        /// ---------------------------------------------------
        /// Anuradha        09-Dec-2008        Intial Version 
        /// 
        public static SqlParameter AddOutputParameter<T>(string ParamName, DbType DataType, T Value,int Size)
        {
            SqlParameter Param = new SqlParameter();
            Param.DbType = DataType;
            Param.ParameterName = ParamName;
            Param.Size = Size;
            Param.Value = Value;
            Param.Direction = ParameterDirection.Output;
            return Param;
        }


        /// <summary>
        /// Test the connection with the DB.
        /// </summary>
        /// <param name="ConnectionString"></param>
        /// <returns>success or failure</returns>
        /// Method Revision History
        ///
        /// Author             Date              Description
        /// ---------------------------------------------------
        /// Anuradha        10-Dec-2008        Intial Version 
        /// 

        public static bool TestConnectionDB(string ConnectionString)
        {
            SqlConnection objSQLConn = null;
            bool bResult = false;
            SqlConnection.ClearAllPools(); 
            try
            {
                if (String.IsNullOrEmpty(ConnectionString) == false)
                {
                    objSQLConn = new SqlConnection(ConnectionString);
                    objSQLConn.Open();
                    bResult = true;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("TestConnectionDB" + ex.Message.ToString() + ex.Source.ToString(), LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
            }
            finally
            {
                if (objSQLConn != null)
                {
                    objSQLConn.Close();
                    objSQLConn.Dispose();
                }
            }
            return bResult;
        }

        public static bool TestODBCConnection(string connectionstring)
        {
            bool bResult = false;
            try
            {
                
                using (OdbcConnection myConnection = new OdbcConnection(connectionstring))
                {
                    myConnection.Open();                    
                    bResult = true;                    
                    myConnection.Close();
                };
            }
            catch(Exception ex)
            {
                bResult = false;
                ExceptionManager.Publish(ex);
            }
            return bResult;
        }


        public static bool RestoreDB(string type,Dictionary<string, string> DBServerInfo)
        {
            //restore the database to the specified location from the backup provided
            try
            {
                LogManager.WriteLog("Launching Database Restore for " + type + "DB" + " ...", LogManager.enumLogLevel.Info);
                string RestoreArgs;
                string backupFile = "";
                string strServer = string.Empty, strUser = string.Empty, strPassword = string.Empty,
                    strDatabase = string.Empty, strinstName = string.Empty, strLocation = string.Empty;
                

                foreach (KeyValuePair<string, string> objKeyValue in DBServerInfo)
                {
                    if (objKeyValue.Key.ToUpper().Equals("SERVER"))
                    {
                        strServer = objKeyValue.Value;
                    }
                    if (objKeyValue.Key.ToUpper().Equals("UID"))
                    {
                        strUser = objKeyValue.Value;
                    }
                    if (objKeyValue.Key.ToUpper().Equals("PASSWORD"))
                    {
                        strPassword = objKeyValue.Value;
                    }
                    if (objKeyValue.Key.ToUpper().Equals("DATABASE"))
                    {
                        strDatabase = objKeyValue.Value;
                    }
                    if (objKeyValue.Key.ToUpper().Equals("LOCATION"))
                    {
                        strLocation = objKeyValue.Value;
                    }
                }

                if (type.ToUpper() == "EXCHANGE")
                    backupFile = "ExchangeBlankDB.bak";
                else if (type.ToUpper() == "TICKETING")
                    backupFile = "TicketingBlankDB.bak";
                else if (type.ToUpper() == "CMKTSDG")
                    backupFile = "CMktSDGBlankDB.bak";
                RestoreArgs = "/Run" +
                    " /File:" + Application.StartupPath +@"\Database\" + backupFile +" /Server:" + strServer;
                if (strinstName.Length > 0)
                    RestoreArgs += "\\" + strinstName;
                RestoreArgs += " /DB:" + strDatabase +
                    " /UserName:" + strUser +
                    " /Password:" + strPassword;
                ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo();
                psi.FileName =Application.StartupPath +@"\CompanyFilterSQLRestore.exe";

                LogManager.WriteLog("DB Loc - " + RestoreArgs, LogManager.enumLogLevel.Info);
                LogManager.WriteLog("Process Loc - " + psi.FileName, LogManager.enumLogLevel.Info);

                psi.Arguments = RestoreArgs;
                System.Diagnostics.Process.Start(psi);

                LogManager.WriteLog("Restore Process Running for " + type + "!", LogManager.enumLogLevel.Info);
                LogManager.WriteLog("Wait for the success dialog before doing anything else!", LogManager.enumLogLevel.Info);

                //addStatus(backupFile);
                //addStatus(RestoreArgs);
                return true;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error Restoring " + type + " Database: " + ex.Message,LogManager.enumLogLevel.Info);
                return false;
            }
        }

        /// <summary>
        /// Restore the DB.
        /// </summary>
        /// <param name="ConnectionString"></param>
        /// <returns>success or failure</returns>
        /// Method Revision History
        ///
        /// Author             Date              Description
        /// ---------------------------------------------------
        /// Anuradha        11-Dec-2008       used existing code to check if DB Exists.
        /// 

        public static bool CheckifDBExists(string SQLConnection, string sDBName, int timeout)
        {
            bool bDBExisits = false;
            try
            {
                LogManager.WriteLog("Testing Database Connection for " + sDBName + "...", LogManager.enumLogLevel.Info);
                SQLConnection += ";DATABASE=" + sDBName + ";";
                SQLConnection += "CONNECTION TIMEOUT=" + timeout + ";";
                SqlConnection conn = new SqlConnection(SQLConnection);
                SqlCommand SQLCommand = new SqlCommand();
                SQLCommand.Connection = conn;
                SQLCommand.Connection.Open();
                SQLCommand.Connection.Close();
                bDBExisits = true;
            }
            catch (Exception ex)
            {
                bDBExisits = false;
                LogManager.WriteLog("Database Connection Failed" + ex.Message.ToString() + ex.Source.ToString(), LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
            }

            return bDBExisits;
        }
        //
        public static bool bExecuteRestoreScripts(string SQLConnection)
        {
            SqlConnection sqlConnection = null;
            SqlCommand oCommand = new SqlCommand();
            string scriptFile = string.Empty;
            string sqlcommandText = "";
            bool bExecute = false;
            try
            {
                LogManager.WriteLog("Executing restore scripts in master database.", LogManager.enumLogLevel.Info);

                using (Stream st = Assembly.GetEntryAssembly().GetManifestResourceStream("BMC.ExchangeConfig.Resources.DBRestore.sql"))
                {
                    StreamReader sr = new StreamReader(st);
                    scriptFile = sr.ReadToEnd();
                    sr.Close();
                }

                sqlConnection = new SqlConnection(SQLConnection);
                
                string[] sqlCommands = Regex.Split(scriptFile, @"^\s*GO\s*($|\-\-.*$)", RegexOptions.Multiline | RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace);

                sqlConnection.Open();
                oCommand.Connection = sqlConnection;
                foreach (string sqlCommand in sqlCommands)
                {
                    if (!string.IsNullOrEmpty(sqlCommand))
                    {
                        sqlcommandText = sqlCommand;
                        oCommand.CommandText = sqlCommand;
                        oCommand.ExecuteNonQuery();
                    }
                }
                sqlConnection.Close();
                bExecute = true;
                LogManager.WriteLog(string.Format("Restore scripts executed successfully"), LogManager.enumLogLevel.Info);
            }
            catch (Exception ex)
            {
                LogManager.WriteLog(string.Format("Failed to execute the restore scripts"), LogManager.enumLogLevel.Info);
                throw ex;
            }
            finally
            {
                if (sqlConnection != null)
                {
                    sqlConnection.Close();
                    sqlConnection.Dispose();
                    sqlConnection = null;
                }
            }

            return bExecute;
        }
        //
        public static void ExecuteScripts(string SQLConnection, string scriptFile)
        {
            SqlConnection sqlConnection = null;
            SqlCommand oCommand = new SqlCommand();
            string sqlcommandText = "";
            try
            {
                LogManager.WriteLog("Executing the scripts in database.", LogManager.enumLogLevel.Info);
                sqlConnection = new SqlConnection(SQLConnection);
                string[] sqlCommands = Regex.Split(scriptFile, @"^\s*GO\s*($|\-\-.*$)", RegexOptions.Multiline | RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace);
                sqlConnection.Open();
                oCommand.Connection = sqlConnection;
                foreach (string sqlCommand in sqlCommands)
                {
                    if (!string.IsNullOrEmpty(sqlCommand))
                    {
                        sqlcommandText = sqlCommand;
                        oCommand.CommandText = sqlCommand;
                        oCommand.ExecuteNonQuery();
                    }
                }
                sqlConnection.Close();
                LogManager.WriteLog(string.Format("scripts executed successfully"), LogManager.enumLogLevel.Info);
            }
            catch (Exception ex)
            {
                LogManager.WriteLog(string.Format("Failed to execute the scripts"), LogManager.enumLogLevel.Info);
                throw ex;
            }
            finally
            {
                if (sqlConnection != null)
                {
                    sqlConnection.Close();
                    sqlConnection.Dispose();
                    sqlConnection = null;
                }
            }
        }
        //
        public static bool RestoreDatabase(string SQLConnection, string sBackUPPath, string sRestorePath, string sDatabaseName)
        {
            bool bRestoreDatabase = false;
            try
            {
                if (bExecuteRestoreScripts(SQLConnection))
                {
                    LogManager.WriteLog("Restore database for " + sDatabaseName + "...", LogManager.enumLogLevel.Info);
                    SQLConnection += ";DATABASE=Master;";
                    SQLConnection += "CONNECTION TIMEOUT=0;";
                    SqlConnection conn = new SqlConnection(SQLConnection);
                    SqlCommand SQLCommand = new SqlCommand("RestoreDatabase", conn);
                    SQLCommand.CommandType = CommandType.StoredProcedure;
                    SqlParameter BackUPPath = new SqlParameter("@sBackUPPath", SqlDbType.VarChar);
                    BackUPPath.Value = sBackUPPath;
                    SQLCommand.Parameters.Add(BackUPPath);
                    SqlParameter RestorePath = new SqlParameter("@sRestorePath", SqlDbType.VarChar);
                    RestorePath.Value = sRestorePath;
                    SQLCommand.Parameters.Add(RestorePath);
                    SqlParameter DatabaseName = new SqlParameter("@sDatabaseName", SqlDbType.VarChar);
                    DatabaseName.Value = sDatabaseName;
                    SQLCommand.Parameters.Add(DatabaseName);
                    SQLCommand.Connection.Open();
                    try
                    {
                        SQLCommand.ExecuteNonQuery();
                    }
                    finally
                    {
                        SQLCommand.Connection.Close();
                    }

                    bRestoreDatabase = true;
                }
            }
            catch (Exception ex)
            {
                bRestoreDatabase = false;
                LogManager.WriteLog("Restore database Failed" + ex.Message.ToString() + ex.Source.ToString(), LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
            }
            return bRestoreDatabase;
        }

        public static bool CreateDatabase(string SQLConnection, string _strDataFilePath, string _strLogFilePath, string sDatabaseName)
        {
            bool bCreateDatabase = false;
            try
            {
                if (bExecuteRestoreScripts(SQLConnection))
                {
                    LogManager.WriteLog("Create database for " + sDatabaseName + "...", LogManager.enumLogLevel.Info);
                    //SQLConnection += ";DATABASE=Master;";
                    //SQLConnection += "CONNECTION TIMEOUT=0;";
                    SqlConnection conn = new SqlConnection(SQLConnection);
                    SqlCommand SQLCommand = new SqlCommand("CreateDatabase", conn);
                    SQLCommand.CommandType = CommandType.StoredProcedure;

                    SqlParameter mdfPath = new SqlParameter("@mdf", SqlDbType.VarChar);
                    mdfPath.Value = _strDataFilePath;
                    SQLCommand.Parameters.Add(mdfPath);
                    SqlParameter ldfPath = new SqlParameter("@ldf", SqlDbType.VarChar);
                    ldfPath.Value = _strLogFilePath;
                    SQLCommand.Parameters.Add(ldfPath);
                    SqlParameter DatabaseName = new SqlParameter("@sDatabaseName", SqlDbType.VarChar);
                    DatabaseName.Value = sDatabaseName;
                    SQLCommand.Parameters.Add(DatabaseName);
                    SQLCommand.Connection.Open();
                    try
                    {
                        SQLCommand.ExecuteNonQuery();
                    }
                    finally
                    {
                        SQLCommand.Connection.Close();
                    }

                    bCreateDatabase = true;
                }
            }
            catch (Exception ex)
            {
                bCreateDatabase = false;
                LogManager.WriteLog("Create database Failed" + ex.Message.ToString() + ex.Source.ToString(), LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
            }
            return bCreateDatabase;
        }
        
        public static bool DropDatabase(string SQLConnection, string sDatabaseName)
        {

            bool bDropDatabase = false;

            try
            {
                SqlConnection conn = new SqlConnection(SQLConnection);
                SqlCommand SQLCommand = new SqlCommand("DropDatabase", conn);
                SQLCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter DatabaseName = new SqlParameter("@sDatabaseName", SqlDbType.VarChar);
                DatabaseName.Value = sDatabaseName;
                SQLCommand.Parameters.Add(DatabaseName);
                SQLCommand.Connection.Open();
                try
                {
                    SQLCommand.ExecuteNonQuery();
                }
                finally
                {
                    SQLCommand.Connection.Close();
                }
                bDropDatabase = true;
            }

            catch (Exception ex)
            {
                bDropDatabase = false;
            }
            return bDropDatabase;
        }

        
        /// <summary>
        /// Restore the DB.
        /// </summary>
        /// <param name="ConnectionString"></param>
        /// <returns>success or failure</returns>
        /// Method Revision History
        ///
        /// Author             Date              Description
        /// ---------------------------------------------------
        /// Anuradha        11-Dec-2008        Intial Version 
        /// Renjish			12-Jan-2009			Modified
        /// Bala Rajesh     26-sep-2011         Changed DMO with SMO


        public static List<string> GetServers()
        {
            List<string> objListNames = new List<string>();
            try
            {
                 //Adding local servers
                RegistryKey rk = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Microsoft SQL Server");
                String[] instances = (String[])rk.GetValue("InstalledInstances");
                if (instances.Length > 0)
                {
                    foreach (String element in instances)
                    {
                        String name = "";
                        if (element == "MSSQLSERVER")
                            name = "(local)";
                        else
                            name = System.Environment.MachineName + @"\" + element;

                        if (!objListNames.Contains(name))
                            objListNames.Add(name);
                    }
                }


                // adding remote servers
                DataTable dt = SmoApplication.EnumAvailableSqlServers(false);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        string serverName = dr["Name"].ToString();
                        if (!objListNames.Contains(serverName))
                            objListNames.Add(serverName);
                    }
                }
                objListNames.Sort();
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("GetServers" + ex.Message.ToString() + ex.Source.ToString(), LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);                
            }
            return objListNames;
        }
        
        /// <summary>
        /// Restore the DB.
        /// </summary>
        /// <param name="ConnectionString"></param>
        /// <returns>success or failure</returns>
        /// Method Revision History
        ///
        /// Author             Date              Description
        /// ---------------------------------------------------
        /// Anuradha        11-Dec-2008        Intial Version 
        /// 


        //public static List<string> GetDataBases(string Server,string User,string password)
        //{
        //    List<string> objDataBases = new List<string>();
        //    try
        //    {
        //        SQLDMO.SQLServer srv = new SQLDMO.SQLServerClass();
        //        srv.Connect(Server, User, password);
        //        foreach (SQLDMO.Database db in srv.Databases)
        //        {
        //            if (db.Name != null)
        //                objDataBases.Add(db.Name);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        LogManager.WriteLog("GetDataBases" + ex.Message.ToString() + ex.Source.ToString(), LogManager.enumLogLevel.Error);
        //        ExceptionManager.Publish(ex);
        //    }

        //    return objDataBases;
        //}


        //public static List<string> GetLanguages(string Server, string User, string password)
        //{
        //    List<string> lstLanguages = new List<string>();
        //    try
        //    {
        //        SQLDMO.SQLServer srv = new SQLDMO.SQLServerClass();
        //        srv.Connect(Server, User, password);
        //        foreach (SQLDMO.Language ln in srv.Languages)
        //        {
        //            if (ln.Name != null)
        //                lstLanguages.Add(ln.Name);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        LogManager.WriteLog("GetDataBases" + ex.Message.ToString() + ex.Source.ToString(), LogManager.enumLogLevel.Error);
        //        ExceptionManager.Publish(ex);
        //    }

        //    return lstLanguages;
        //}
             
        /// <summary>        
        /// set ticket connection string to setting table
        /// <Author>Vineetha Mathew</Author>
        /// <DateCreated>Date Created 06-Jan-2009</DateCreated>
        /// <param name=TicketingConnectionString>string</param>
        /// <returns>bool</returns>
        /// </summary>      
        public static bool InsertSettings(string strSettingName,string strSettingValue)
        {  
           string strReturnValue=string.Empty;
           bool bReturn=false;           
           object objResult=null;            
            
            try
            {
                objResult=SqlHelper.ExecuteScalar(ExchangeConfigRegistryEntities.ExchangeConnectionString, DBConstants.SPEDITSETTING,
                    AddParameter<int>(DBConstants.SP_PARAM_SETTINGID, DbType.Int32 ,0,4),
                    AddParameter<string>(DBConstants.SP_PARAM_SETTINGNAME, DbType.String , strSettingName.Trim(),100),
                    AddParameter<string>(DBConstants.SP_PARAM_SETTINGVALUE, DbType.String, strSettingValue.Trim(), 300),
                    AddParameter<string>(DBConstants.SP_PARAM_SETTINGDESCRIPTION, DbType.String, string.Empty, 100));

                if (objResult != null)
                {
                    strReturnValue = objResult.ToString();
                }

                bReturn = true;
                //Renjish - Commented this becoz it was failing even though the data was updated in the database.
                //if (!string.IsNullOrEmpty(strReturnValue))
                //{
                //    bReturn=true;
                //}
                //else
                //{
                //    bReturn=false;
                //}
                             
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("InsertSettings" + ex.Message.ToString() + ex.Source.ToString(), LogManager.enumLogLevel.Error);
                bReturn=false;
               
                //ExceptionManager.Publish(ex);
                throw ex;

            }        
           return  bReturn;
        }

        public static bool InsertTISSettings(string TISConnectionMode, string TISIPAddress, string TISPortNumber, string TISWebServiceURL, string TISTicketPrefix, string TISDataExchangePortNumber, string TISExternalWebServiceURL, string TISExternalCasinoCode)
        {
            bool bReturn = false;
            object objResult = null;
            string strReturnValue = string.Empty;
            try
            {
                

                objResult = SqlHelper.ExecuteScalar(ExchangeConfigRegistryEntities.ExchangeConnectionString, DBConstants.SP_TIS_SETTING,

                    AddParameter<string>(@TISConnectionMode, DbType.String, TISConnectionMode, 8000),
                    AddParameter<string>(@TISIPAddress, DbType.String, TISIPAddress, 8000),
                    AddParameter<string>(@TISPortNumber, DbType.String, TISPortNumber, 8000),
                    AddParameter<string>(@TISWebServiceURL, DbType.String, TISWebServiceURL, 8000),
                    AddParameter<string>(@TISTicketPrefix, DbType.String, TISTicketPrefix, 8000),
                     
                    AddParameter<string>(@TISDataExchangePortNumber, DbType.String, TISDataExchangePortNumber, 8000),
                AddParameter<string>(@TISExternalWebServiceURL, DbType.String, TISExternalWebServiceURL, 8000),
                AddParameter<string>(@TISExternalCasinoCode, DbType.String, TISExternalCasinoCode, 8000));
                
                if (objResult != null)
                {
                    strReturnValue = objResult.ToString();
                }

                bReturn = true;
                

            }
            catch (Exception ex)
            {
                LogManager.WriteLog("InsertSettings" + ex.Message.ToString() + ex.Source.ToString(), LogManager.enumLogLevel.Error);
                bReturn = false;               
                throw ex;

            }
            return bReturn;

        }

        /// <summary>        
        /// Set ticket code to the ticketing DB in site table
        /// <Author>Vineetha Mathew</Author>
        /// <DateCreated>Date Created 19-Feb-2009</DateCreated>
        /// <param name=iLocationCode>int</param>
        /// <returns>bool</returns>
        /// </summary>      
        public static bool SetTicketLocationCodeDB(int iLocationCode)
        {
            bool bReturn = false;
            object objResult = null;

            try
            {
                objResult = SqlHelper.ExecuteNonQuery(ExchangeConfigRegistryEntities.TicketingConnectionString, CommandType.Text, "Update Site set iLocCode=" + iLocationCode);

                if (objResult != null)
                {
                    bReturn = true;
                }
                else
                {
                    bReturn = false;
                }
            }
            catch (System.Data.SqlClient.SqlException sqlEx)
            {
                ExceptionManager.Publish(sqlEx);

                SetTicketLocationCodeDB(iLocationCode);
                bReturn = true;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("SetTicketLocationCode" + ex.Message.ToString() + ex.Source.ToString(), LogManager.enumLogLevel.Error);
                bReturn = false;

                ExceptionManager.Publish(ex);
            }
            return bReturn;
        }
        /// <summary>        
        /// Run the script to clear the data for the Exchange,Ticketing and CMP tables
        /// <Author>Vineetha Mathew</Author>
        /// <DateCreated>Date Created 03-Feb-2009</DateCreated>
        /// <param name=strScriptToRun>string</param>
        /// <returns>bool</returns>
        /// </summary>      
        public static bool RunFactoryResetScriptsDB(string strScriptToRun)
        {
            int iReturnValue = 0;
            SqlConnection SqlConn = null;
            ServerConnection svrConnServerConnection = null;
            Server svrServer = null;
            object objResult = null;
            bool bReturn = false;

            try
            {
                SqlConn = new SqlConnection(ExchangeConfigRegistryEntities.ExchangeConnectionString);
                svrConnServerConnection = new ServerConnection(SqlConn);
                svrServer = new Server(svrConnServerConnection);
                svrConnServerConnection=svrServer.ConnectionContext;
                objResult=svrConnServerConnection.ExecuteNonQuery(strScriptToRun);
                if (objResult != null)
                {
                    iReturnValue = int.Parse(objResult.ToString());
                }
                if (iReturnValue!=0)
                {
                    bReturn = true;
                }
                else
                {
                    bReturn = false;
                }

            }
            catch (SqlException sqlEx)
            {
                bReturn = false;
                LogManager.WriteLog("RunFactoryResetScriptsDB" + sqlEx.Message.ToString() + sqlEx.Source.ToString(), LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(sqlEx);
            }
            catch (Exception Ex)
            {
                bReturn = false;
                LogManager.WriteLog("RunFactoryResetScriptsDB" + Ex.Message.ToString() + Ex.Source.ToString(), LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(Ex);
            }
            finally
            {
                SqlConn.Close();                
            }
            return bReturn;
        }

        public static bool GetSiteCount()
        {   
            SqlConnection SqlConn = null;
            bool result = false;
           
            try
            {
                SqlConn = new SqlConnection(ExchangeConfigRegistryEntities.ExchangeConnectionString);
                SqlConn.Open();
                var Sitecount = SqlHelper.ExecuteScalar(SqlConn, CommandType.Text, "select count(*) from site");

                if (Sitecount != null)
                {   
                    result = (int)Sitecount > 0 ? true : false;
                }                
            }
            catch (SqlException sqlEx)
            {   
                ExceptionManager.Publish(sqlEx);                
            }
            catch (Exception Ex)
            {   
                ExceptionManager.Publish(Ex);                
            }
            finally
            {
                SqlConn.Close();                
            }
            return result;
        }

        public static bool GetUsersCount()
        {
            SqlConnection SqlConn = null;
            bool result = false;

            try
            {
                SqlConn = new SqlConnection(ExchangeConfigRegistryEntities.ExchangeConnectionString);
                SqlConn.Open();
                var userCount = SqlHelper.ExecuteScalar(SqlConn, CommandType.Text, "select count(*) from [user] where securityuserid is not null");

                if (userCount != null)
                {
                    result = (int)userCount > 0 ? true : false;
                }
            }
            catch (SqlException sqlEx)
            {
                ExceptionManager.Publish(sqlEx);
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
            finally
            {
                SqlConn.Close();
            }
            return result;
        }

        public static void InsertExportHistory(string Reference, string Type)
        {
            SqlConnection SqlConn = null;
            try
            {
                SqlConn = new SqlConnection(ExchangeConfigRegistryEntities.ExchangeConnectionString);
                SqlConn.Open();
                var userCount = SqlHelper.ExecuteNonQuery(SqlConn, CommandType.StoredProcedure, "usp_Export_History",
                    DataBaseServiceHandler.AddParameter<string>("@Reference1", DbType.String, Reference),
                    DataBaseServiceHandler.AddParameter<string>("@Reference2", DbType.String, ""),
                    DataBaseServiceHandler.AddParameter<string>("@Type", DbType.String, Type),
                    DataBaseServiceHandler.AddParameter<string>("@Status", DbType.String, ""));
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
            finally
            {
                SqlConn.Close();
            }
        }

        public static int GetExtSiteCode(string ConnectionStr,int ExtSiteCode)
        {
            int Result = 0;
            SqlConnection SqlConn = null;
            SqlCommand SqlCmd = null;
            try
            {
              
                SqlConn = new SqlConnection(ConnectionStr);
                SqlConn.Open();
                SqlCmd = new SqlCommand("rsp_GetExternalSiteCode", SqlConn);

                SqlCmd.CommandText = "dbo.rsp_GetExternalSiteCode";
                SqlCmd.CommandType = CommandType.StoredProcedure;
              

                SqlParameter SPresult = SqlCmd.Parameters.Add("@ExternalSiteCode", SqlDbType.Int);
                SPresult.Direction = ParameterDirection.ReturnValue;

                SqlCmd.ExecuteScalar();
                Result =  Convert.ToInt32(SqlCmd.Parameters["@ExternalSiteCode"].Value);
                SqlConn.Close();
                return Result;
                
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return Result;
            }
        }
        public static string GetExternalCasinoCode(string ConnectionStr, string ExtCasinoCode)
        {
            string  Result = string.Empty;
            SqlConnection SqlConn = null;
            SqlCommand SqlCmd = null;
            try
            {
              
                SqlConn = new SqlConnection(ConnectionStr);
                SqlConn.Open();
                SqlCmd = new SqlCommand("rsp_GetExternalCasinoCode", SqlConn);

                SqlCmd.CommandText = "dbo.rsp_GetExternalCasinoCode";
                SqlCmd.CommandType = CommandType.StoredProcedure;
              

                SqlParameter SPresult = SqlCmd.Parameters.Add("@ExternalCasinoCode", SqlDbType.VarChar);
                SPresult.Direction = ParameterDirection.ReturnValue;

                SqlCmd.ExecuteScalar();
                Result = SqlCmd.Parameters["@ExternalCasinoCode"].Value.ToString();
                SqlConn.Close();
                return Result;
                
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return Result;
            }
        }

        public static DataSet GetGridViewColorRangeDetails(int gvtID, string ExchangeConnectionString)
        {
            try
            {
                SqlParameter[] sqlParamter = new SqlParameter[1];
                sqlParamter[0] = new SqlParameter("@GVTID", gvtID);
                return SqlHelper.ExecuteDataset(ExchangeConnectionString, CommandType.StoredProcedure, DBConstants.RSP_GETGRIDVIEWCOLORRANGEDETAILS, sqlParamter);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return null;
            }
        }

        public static DataSet GetGridViewTypeDetails(string ExchangeConnectionString)
        {
            try
            {
                return SqlHelper.ExecuteDataset(ExchangeConnectionString, CommandType.StoredProcedure, DBConstants.RSP_GETGRIDVIEWTYPEDETAILS, null);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return null;
            }
        }

        public static int InsertOrUpdateGridViewColorRangeDetails(int gvtID, decimal startValue, decimal endValue, string hexValue, string ExchangeConnectionString)
        {
            try
            {
                SqlParameter[] sqlParamter = new SqlParameter[4];

                sqlParamter[0] = new SqlParameter("@GVT_ID", gvtID);
                sqlParamter[1] = new SqlParameter("@Start_Value", startValue);
                sqlParamter[2] = new SqlParameter("@End_Value", endValue);
                sqlParamter[3] = new SqlParameter("@Color_HexValue", hexValue);

                return SqlHelper.ExecuteNonQuery(ExchangeConnectionString, CommandType.StoredProcedure, DBConstants.USP_INSERTORUPDATEGRIDVIEWCOLORRANGEDETAILS, sqlParamter);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return -1;
            }
        }

        public static int DeleteGridViewColorRangeDetails(int gvtID, decimal startValue, decimal endValue, string ExchangeConnectionString)
        {
            try
            {
                SqlParameter[] sqlParamter = new SqlParameter[3];

                sqlParamter[0] = new SqlParameter("@GVT_ID", gvtID);
                sqlParamter[1] = new SqlParameter("@Start_Value", startValue);
                sqlParamter[2] = new SqlParameter("@End_Value", endValue);

                return SqlHelper.ExecuteNonQuery(ExchangeConnectionString, CommandType.StoredProcedure, DBConstants.USP_DELETEGRIDVIEWCOLORRANGEDETAILS, sqlParamter);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return -1;
            }
        }
        
    }
}
