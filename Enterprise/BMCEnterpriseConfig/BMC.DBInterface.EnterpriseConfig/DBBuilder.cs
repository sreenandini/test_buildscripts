using System;
using System.Collections.Generic;
using System.Text;
using BMC.DataAccess;
using System.Data;
using System.Data.SqlClient;
using BMC.Common.ExceptionManagement;
using BMC.Common.LogManagement;
//using SQLDMO;
using System.Collections;
using BMC.Transport.EnterpriseConfig;
using System.Diagnostics;
using System.Windows.Forms;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;

namespace BMC.DBInterface.EnterpriseConfig
{
    public static class DBBuilder
    {
                
        /// <summary>
        /// Gets the CMP Gateway Connection String
        /// </summary>
        /// <param name="EnterpriseConnectionString"></param>
        /// <returnsCMP Connection string</returns>
        /// Method Revision History
        ///
        /// Author             Date              Description
        /// ---------------------------------------------------
        /// Anuradha        09-Dec-2008        Intial Version 
        /// 
        public static Dictionary<int, string> GetSetting(string EnterpriseConnectionString,params SqlParameter[] paramCMPsetting)
        {
           Dictionary<int, string> dCMPSetting=null;
           try
           {
               SqlCommand sqlcommSetting = LoadCommand(DBConstants.RSP_GETSETTING, EnterpriseConnectionString, paramCMPsetting);
               dCMPSetting = ExecuteCommand(sqlcommSetting);
           }
           catch (Exception ex)
           {
               LogManager.WriteLog("GetSetting" + ex.Message.ToString() + ex.Source.ToString(), LogManager.enumLogLevel.Error);
               ExceptionManager.Publish(ex);
           }
         
            return dCMPSetting;
        }


        /// <summary>
        /// Creates the command object based on procedure name.
        /// </summary>
        /// <param name="EnterpriseConnectionString"></param>
        /// <param name="procedurename"></param>
        /// <returnsCMP Connection string</returns>
        /// Method Revision History
        ///
        /// Author             Date              Description
        /// ---------------------------------------------------
        /// Anuradha        09-Dec-2008        Intial Version 
        /// 
        private static SqlCommand LoadCommand(string procedurename, string EnterpriseConnectionString,params SqlParameter[] paramCMPsetting)
        {
            SqlCommand commCMPSetting = null;
            try
            {
                commCMPSetting = new SqlCommand();
                commCMPSetting.Connection = new SqlConnection(EnterpriseConnectionString);
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

        ///// <summary>
        ///// Restore the DB.
        ///// </summary>
        ///// <param name="ConnectionString"></param>
        ///// <returns>success or failure</returns>
        ///// Method Revision History
        /////
        ///// Author             Date              Description
        ///// ---------------------------------------------------
        ///// Anuradha        11-Dec-2008        Intial Version 
        ///// 


        //public static bool RestoreDB(Dictionary<string,string> DBServerInfo)
        //{
        //     string strServer=string.Empty;
        //     string strUser=string.Empty;
        //      string strPassword=string.Empty;
        //      string strDatabase = string.Empty;
        //      string strFilePath = string.Empty;
        //      bool bResult = false;

        //    try
        //    {
        //        if (DBServerInfo != null)
        //        {

        //            //create an instance of a server class
        //            SQLDMO._SQLServer srv = new SQLDMO.SQLServerClass();
        //            //connect to the server

        //            foreach (KeyValuePair<string, string> objKeyValue in DBServerInfo)
        //            {
        //                if (objKeyValue.Key.ToUpper().Equals("SERVER"))
        //                {
        //                    strServer = objKeyValue.Value;
        //                }
        //                if (objKeyValue.Key.ToUpper().Equals("UID"))
        //                {
        //                    strUser = objKeyValue.Value;
        //                }
        //                if (objKeyValue.Key.ToUpper().Equals("PASSWORD"))
        //                {
        //                    strPassword = objKeyValue.Value;
        //                }
        //                if (objKeyValue.Key.ToUpper().Equals("DATABASE"))
        //                {
        //                    strDatabase = objKeyValue.Value;
        //                }
        //                if (objKeyValue.Key.ToUpper().Equals("FILEPATH"))
        //                {
        //                    strFilePath = objKeyValue.Value;
        //                }


        //            }
        //            srv.Connect(strServer, strUser, strPassword);



        //            //create a restore class instance
        //            SQLDMO.Restore res = new SQLDMO.RestoreClass();
        //            //set the backup device = files property ( easy way )
        //            res.Devices = res.Files;
        //            //set the files property to the File Name text box
        //            res.Files = strDatabase;
        //            //set the database to the chosen database
        //            res.Database = strFilePath;
        //            // Restore the database
        //            res.ReplaceDatabase = true;
        //            res.SQLRestore(srv);
        //            bResult = true;
        //        }
        //    }           
        //    catch (Exception ex)
        //    {
        //        LogManager.WriteLog("RestoreDB" + ex.Message.ToString() + ex.Source.ToString(), LogManager.enumLogLevel.Error);
        //        ExceptionManager.Publish(ex);
        //        bResult = false;
        //    }
        //    return bResult;
        //}


        ///// <summary>
        ///// Restore the DB.
        ///// </summary>
        ///// <param name="ConnectionString"></param>
        ///// <returns>success or failure</returns>
        ///// Method Revision History
        /////
        ///// Author             Date              Description
        ///// ---------------------------------------------------
        ///// Anuradha        11-Dec-2008        Used Existing restore code.
        /////  Renjish			12-Jan-2009			Modified

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

                if (type.ToUpper() == "Enterprise")
                    backupFile = "EnterpriseBlankDB.bak";
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


        //public static List<string> GetServers()
        //{
        //    List<string> objListNames = new List<string>();
        //    try
        //    {
        //        //get all available SQL Servers	
        //        SQLDMO.ApplicationClass sqlApp = new SQLDMO.ApplicationClass();
        //        SQLDMO.NameList sqlServers = sqlApp.ListAvailableSQLServers();

        //        objListNames.Add("(local)");

        //        foreach (string objNames in sqlServers)
        //        {
        //            //IEnumerator objNamesList = objNames.GetEnumerator();
        //            //do
        //            //{
        //            //    objNamesList.MoveNext();
        //            //    objListNames.Add(objNamesList.Current.ToString());
        //            //}

        //           // while (objNamesList.MoveNext());
        //            objListNames.Add(objNames);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        LogManager.WriteLog("GetServers" + ex.Message.ToString() + ex.Source.ToString(), LogManager.enumLogLevel.Error);
        //        ExceptionManager.Publish(ex);                
        //    }
        //    return objListNames;
        //}
        
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
                objResult=SqlHelper.ExecuteScalar(EnterpriseConfigRegistryEntities.EnterpriseConnectionString, DBConstants.SPEDITSETTING,
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
               
                ExceptionManager.Publish(ex);
            }        
           return  bReturn;
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
                objResult = SqlHelper.ExecuteNonQuery(EnterpriseConfigRegistryEntities.MeterAnalysisConnectionString, CommandType.Text, "Update Site set iLocCode=" + iLocationCode);

                if (objResult != null)
                {
                    bReturn = true;
                }
                else
                {
                    bReturn = false;
                }

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
        /// Run the script to clear the data for the Enterprise,Ticketing and CMP tables
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
                SqlConn = new SqlConnection(EnterpriseConfigRegistryEntities.EnterpriseConnectionString);
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

        public static DataTable GetInitialSettings()
        {
            DataTable dtSettings = null;
            using (SqlConnection con = new SqlConnection(EnterpriseConfigRegistryEntities.EnterpriseConnectionString))
            {

                try
                {
                    con.Open();
                    SqlDataAdapter sda = new SqlDataAdapter();
                    DataSet ds = new DataSet();
                    SqlCommand cmdGetDataTable = new SqlCommand(DBConstants.SP_GETENTERPRISEINITIALSETTINGS, con);
                    cmdGetDataTable.CommandType = CommandType.StoredProcedure;
                    cmdGetDataTable.CommandTimeout = 60;
                    //cmdGetDataTable.ExecuteNonQuery();                            
                    sda.SelectCommand = cmdGetDataTable;
                    sda.Fill(ds);                   
                    dtSettings = new DataTable();
                    dtSettings =ds.Tables.Count>0?ds.Tables[0]:dtSettings;                                                        
                }
                catch (Exception ex)
                {
                    dtSettings = null;
                    ExceptionManager.Publish(ex);
                }
                return dtSettings;
            }
        }
    }
}
