using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using BMC.DataAccess;
using BMC.Common.ExceptionManagement;
using System.Linq;
using System.Text;
using BMC.Transport.CashDeskOperatorEntity;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using BMC.Common.LogManagement;
using System.Collections;
using ZipTools;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
namespace BMC.DBInterface.CashDeskOperator
{
 public class FactoryResetDataAccess
    {
        #region "Private Variables"
        CommonDataAccess commonDataAccess = new CommonDataAccess();
        #endregion

        #region "Public Properties"
        #endregion

        #region "Private Function" 

           private int ZipDatabseFile(string srcPath, string destPath,int iSeverity)
              {
                  int iReturn = 0;
                 FileStream fileSource=null;
                 FileStream fileDestination=null;
                 GZipStream CompressedZip=null;
                 byte[] bufferWrite=null;
                try
                {
                    fileSource = new FileStream(srcPath, FileMode.Open, FileAccess.Read, FileShare.Read);
                    LogManager.WriteLog("Source: "+srcPath+" Dest: "+destPath, LogManager.enumLogLevel.Info);                    
                    if (iSeverity==9)
                    {
                        var zipper = new ZipBuilder();
                        zipper.CreatePackage(destPath, srcPath);
                    }
                    else
                    {                        
                        bufferWrite = new byte[fileSource.Length];
                        fileSource.Read(bufferWrite, 0, bufferWrite.Length);
                        fileDestination = new FileStream(destPath, FileMode.OpenOrCreate, FileAccess.Write);
                        CompressedZip = new GZipStream(fileDestination, CompressionMode.Compress, true);
                        CompressedZip.Write(bufferWrite, 0, bufferWrite.Length);
                    }                    
                    iReturn = 1;
                }                    
                catch (OverflowException oex)
                {
                    iReturn = 0;
                    ExceptionManager.Publish(oex);                  
                }
                catch (Exception ex)
                {
                    iReturn = 0;
                    ExceptionManager.Publish(ex);                   
                }
                finally
                {
                    if(fileSource!=null){ fileSource.Close();}
                    if(CompressedZip!=null){CompressedZip.Close();}
                    if(fileDestination!=null){fileDestination.Close();}
                }
                return iReturn;
             }           
        #endregion

        #region "Public Function" 
 
            /// <summary>
            /// Check for any Active installations
            /// </summary>
            /// <returns>bool if there is any unclosed installations</returns>
            public bool CheckActiveInstallations()
            {
                bool bReturn = false;
                DataTable dtInstallDetails = null;
                DataSet dsActiveInstallationList =null;
                try
                {

                    dsActiveInstallationList = new DataSet();
                    SqlHelper.FillDataset(CommonDataAccess.ExchangeConnectionString, CommandType.StoredProcedure, DBConstants.SPCHECKINSTALLATIONAVAILABLE, dsActiveInstallationList, new string[] { "CheckInstallation" });

                    if (dsActiveInstallationList.Tables.Count > 0)
                    {
                        dtInstallDetails = new DataTable();
                        dtInstallDetails = dsActiveInstallationList.Tables[0];
                        if (dtInstallDetails != null)
                        {
                            if (dtInstallDetails.Rows.Count > 0)
                            {
                                bReturn = true;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    ExceptionManager.Publish(ex);
                    return bReturn;
                }
                finally
                {
                    if (dtInstallDetails!=null) dtInstallDetails.Dispose();
                    if (dsActiveInstallationList != null) dsActiveInstallationList.Dispose();
                }
                return bReturn;
            }

            /// <summary>
            /// Check for any data to export to enterprise
            /// </summary>
            /// <returns>bool if there is any data yet to export</returns>
            public  bool CheckDataToExport()
            {

                bool bReturn = false;
                DataTable dtCheckExport = null;

                try
                {
                    dtCheckExport =(SqlHelper.ExecuteDataset(CommonDataAccess.ExchangeConnectionString, DBConstants.SPCheckDataToExport).Tables.Count>0)?SqlHelper.ExecuteDataset(CommonDataAccess.ExchangeConnectionString, DBConstants.SPCheckDataToExport).Tables[0]:null;

                    if (dtCheckExport != null)
                    {
                        if (dtCheckExport.Rows.Count > 0)
                        {
                            if(Convert.ToInt16(dtCheckExport.Rows[0][0])>0)
                                    bReturn = true;
                            else
                                bReturn = false;
                        }
                        else
                        {
                            bReturn = false;
                        }
                    }

                }
                catch (Exception ex)
                {
                    bReturn = false;
                    ExceptionManager.Publish(ex);
                }
                finally
                {
                    if (dtCheckExport != null) dtCheckExport.Dispose();
                }

                return bReturn;
            }

            /// <summary>
            /// check if the factory reset scipts ran successfully
            /// </summary>
            /// <returns>bool if the scipts ran successfully</returns>
            public  bool RunScripts(string strScriptToRun)
            {

                //int iReturnValue = 0;
                //SqlConnection SqlConn = null;
                //ServerConnection svrConnServerConnection = null;
                //Server svrServer = null;
                //object objResult = null;
                //bool bReturn = false;

                //try
                //{
                //    SqlConn = new SqlConnection(CommonDataAccess.ExchangeConnectionString);
                //    svrConnServerConnection = new ServerConnection(SqlConn);
                    
                //    svrServer = new Server(svrConnServerConnection);
                //    svrConnServerConnection = svrServer.ConnectionContext;
                //    objResult = svrConnServerConnection.ExecuteNonQuery(strScriptToRun);
                //    iReturnValue=(objResult != null)?int.Parse(objResult.ToString()):0;                
                //    bReturn=(iReturnValue != 0)? true : false;
                //}
                //catch (SqlException sqlEx)
                //{
                //    bReturn = false;                
                //    ExceptionManager.Publish(sqlEx);
                //}
                //catch (Exception Ex)
                //{
                //    bReturn = false;                
                //    ExceptionManager.Publish(Ex);
                //}
                //finally
                //{
                //    if (SqlConn!=null) SqlConn.Close();
                //    if (svrConnServerConnection!=null) svrConnServerConnection=null;
                //    if (svrServer != null) svrServer = null;
                //}
                //return bReturn;
                return true;
            }

            /// <summary>
            /// check if the factory reset scipts ran successfully
            /// </summary>
            /// <returns>bool if the scipts ran successfully</returns>
            public bool RunScripts()
            {                
                int iResult=-2;
                bool bReturn = false;                
                try
                {
                    iResult = SqlHelper.ExecuteNonQuery(CommonDataAccess.ExchangeConnectionString, CommandType.StoredProcedure, DBConstants.SP_RUNSCRIPTS);
                    bReturn = (iResult != -2) ? true : false;
                    LogManager.WriteLog("records affected in runscripts: " + iResult.ToString(), LogManager.enumLogLevel.Info);
                }
               
                catch (Exception Ex)
                {
                    bReturn = false;
                    ExceptionManager.Publish(Ex);
                }               
                return bReturn;
            }

            public bool FactoryResetHistory(bool isCompleted, int ResetModeID, string UserName, ref int FRHistoryID)
            {                
                bool bReturn = false;
                try
                {
                    SqlParameter[] Param = new SqlParameter[4];
                    Param[0] = DataBaseServiceHandler.AddParameter<bool>("@IsCompleted", DbType.Boolean, isCompleted);
                    Param[1] = DataBaseServiceHandler.AddParameter<int>("@Mode_ID", DbType.Int32, ResetModeID);
                    Param[2] = DataBaseServiceHandler.AddParameter<string>("@User_Name", DbType.String, UserName);
                    Param[3] = DataBaseServiceHandler.AddParameter<int>("@Status", DbType.Int32, FRHistoryID, ParameterDirection.InputOutput);
                 
                    SqlHelper.ExecuteScalar(CommonDataAccess.ExchangeConnectionString, CommandType.StoredProcedure, "usp_UpdateFactoryResetHistory", Param);
                    
                    if (Param[3].SqlValue != null)
                        FRHistoryID = Convert.ToInt32(Param[3].SqlValue.ToString());
                    
                    bReturn = (FRHistoryID != -1) ? true : false;
                    LogManager.WriteLog("records affected in FactoryResetHistory: " + FRHistoryID.ToString(), LogManager.enumLogLevel.Info);
                }

                catch (Exception Ex)
                {
                    bReturn = false;
                    ExceptionManager.Publish(Ex);
                }
                return bReturn;
            }

            public bool BackupConstraint(bool BackupConstraint, int ResetModeID)
            {
                int iResult = 0;
                bool bReturn = false;
                try
                {
                    SqlParameter[] Param = new SqlParameter[3];
                    Param[0] = DataBaseServiceHandler.AddParameter<bool>("@BackupConstraint", DbType.Boolean, BackupConstraint);
                    Param[1] = DataBaseServiceHandler.AddParameter<int>("@Mode_ID", DbType.Int32, ResetModeID);
                    Param[2] = DataBaseServiceHandler.AddParameter<int>("@Status", DbType.Int32, iResult, ParameterDirection.Output);
                    
                    SqlHelper.ExecuteNonQuery(CommonDataAccess.ExchangeConnectionString, CommandType.StoredProcedure, "usp_BRForiegnConstraint", Param);
                    
                    if (Param[2].SqlValue != null)
                        iResult = Convert.ToInt32(Param[2].SqlValue.ToString());

                    bReturn = (iResult == 0) ? true : false;
                    LogManager.WriteLog("records affected in BackupConstraint: " + iResult.ToString(), LogManager.enumLogLevel.Info);
                }

                catch (Exception Ex)
                {
                    bReturn = false;
                    ExceptionManager.Publish(Ex);
                }
                return bReturn;
            }

            public bool DeleteAddConstraint(bool DropConstraint)
            {
                int iResult = 0;
                bool bReturn = false;
                try
                {
                    SqlParameter[] Param = new SqlParameter[2];
                    Param[0] = DataBaseServiceHandler.AddParameter<bool>("@DropConstraint", DbType.Boolean, DropConstraint);
                    Param[1] = DataBaseServiceHandler.AddParameter<int>("@Status", DbType.Int32, iResult, ParameterDirection.Output);
                    
                    SqlHelper.ExecuteNonQuery(CommonDataAccess.ExchangeConnectionString, CommandType.StoredProcedure, "usp_AddRemoveForiegnConstraint",Param);
                    
                    if (Param[1].SqlValue != null)
                        iResult = Convert.ToInt32(Param[1].SqlValue.ToString());

                    bReturn = (iResult == 0) ? true : false;
                    LogManager.WriteLog("records affected in DeleteAddConstraint: " + iResult.ToString(), LogManager.enumLogLevel.Info);
                }

                catch (Exception Ex)
                {
                    bReturn = false;
                    ExceptionManager.Publish(Ex);
                }
                return bReturn;
            }

            public bool ResetTable(int Mode_Id)
            {
                int iResult = 0;
                bool bReturn = false;
                try
                {
                    SqlParameter[] Param = new SqlParameter[2];
                    Param[0] = DataBaseServiceHandler.AddParameter<int>("@Mode_ID", DbType.Int32, Mode_Id);
                    Param[1] = DataBaseServiceHandler.AddParameter<int>("@Status", DbType.Int32, iResult,ParameterDirection.Output);
                    
                    SqlHelper.ExecuteNonQuery(CommonDataAccess.ExchangeConnectionString, CommandType.StoredProcedure, "usp_ResetTable", Param);
                    
                    if (Param[1].SqlValue != null)
                        iResult = Convert.ToInt32(Param[1].SqlValue.ToString());

                    bReturn = (iResult == 0) ? true : false;
                    LogManager.WriteLog("records affected in ResetTable: " + iResult.ToString(), LogManager.enumLogLevel.Info);
                }

                catch (Exception Ex)
                {
                    bReturn = false;
                    ExceptionManager.Publish(Ex);
                }
                return bReturn;
            }

            /// <summary>
            /// check able to connect to Db with the given connection string
            /// </summary>
            /// <returns>bool if able to connect to DB successfully</returns>
            public bool TestConnectionToDB(string strTestConnect)
            {
                SqlConnection objSQLConn = null;
                bool bResult = false;

                try
                {
                    if (String.IsNullOrEmpty(strTestConnect) == false)
                    {
                        objSQLConn = new SqlConnection(strTestConnect);
                        objSQLConn.Open();
                        bResult = true;
                    }
                }
                catch (Exception ex)
                {                
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

            /// <summary>
            /// Create database back up in Zip format and kept in path
            /// </summary>
            /// <returns>int 0 -not success, 1- success, 2 - already backkup available</returns>
            public int CreateSqlDatabaseBackUp(FactoryReset oFactoryResetEntity)
            {
                SqlConnection SqlConn = null;
                ServerConnection svrConnServerConnection = null;
                Server SqlSever = null;
                Backup bak = null;
                BackupDeviceItem bkpitem = null;
                DateTime dt = DateTime.Now;
                int iResult = 0;
                String[] format = { "ddMMMyyyyHHmm" };
                string bkpFileName = "";
                string Backuppath = "";
                string date = "";
                string zipFileName = "";

                try
                {
                    date = dt.ToString(format[0], DateTimeFormatInfo.InvariantInfo);
                    Backuppath = oFactoryResetEntity.strBackupPath;
                    bkpFileName = Backuppath + "\\" + "[" + date + "]" + "_BAK_" + "[" +
                        CommonDataAccess.GetSettingValue("TICKET_LOCATION_CODE") + "]" +
                        "[" + BMC.Transport.Settings.SiteName + "]" + "_" +
                        oFactoryResetEntity.BackUpDataBase + ".BAK";
                    oFactoryResetEntity.strBackupFileName = bkpFileName;
                    if (Directory.Exists(Backuppath))
                    {
                        if (File.Exists(bkpFileName) == false)
                        {
                            try
                            {
                                SqlConn = new SqlConnection(CommonDataAccess.ExchangeConnectionString);
                                svrConnServerConnection = new ServerConnection(SqlConn);
                                SqlSever = new Server(svrConnServerConnection);

                                if (SqlSever != null)
                                {
                                    bak = new Microsoft.SqlServer.Management.Smo.Backup();
                                    bak.Action = BackupActionType.Database;
                                    bkpitem = new BackupDeviceItem(bkpFileName, DeviceType.File);
                                    bak.Devices.Add(bkpitem);
                                    bak.Database = oFactoryResetEntity.BackUpDataBase;
                                    bak.SqlBackup(SqlSever);
                                }
                            }
                            catch (SqlException sqlex)
                            {
                                ExceptionManager.Publish(sqlex);
                                iResult = 0;
                            }
                            catch (TimeoutException tex)
                            {
                                ExceptionManager.Publish(tex);
                                iResult = 0;
                            }

                            zipFileName = Backuppath + "\\" + "bkp" + oFactoryResetEntity.BackUpDataBase + date + ".ZIP";                             
                            zipFileName = Backuppath + "\\" + "[" + date + "]" + "_BAK_" + "[" +
                                         CommonDataAccess.GetSettingValue("TICKET_LOCATION_CODE") + "]" +
                                         "[" + BMC.Transport.Settings.SiteName + "]" + "_" +
                                         oFactoryResetEntity.BackUpDataBase + ".ZIP";
                            oFactoryResetEntity.strZipFileName = zipFileName;
                            iResult = 1;
                        }
                        else
                        {
                            iResult = 2;
                        }
                    }
                    else
                    {
                        Directory.CreateDirectory(Backuppath);
                        try
                        {
                            SqlConn = new SqlConnection(CommonDataAccess.ExchangeConnectionString);
                            svrConnServerConnection = new ServerConnection(SqlConn);
                            SqlSever = new Server(svrConnServerConnection);

                            if (SqlSever != null)
                            {
                                bak = new Microsoft.SqlServer.Management.Smo.Backup();
                                bak.Action = BackupActionType.Database;
                                bkpitem = new BackupDeviceItem(bkpFileName, DeviceType.File);
                                bak.Devices.Add(bkpitem);
                                bak.Database = oFactoryResetEntity.BackUpDataBase;
                                bak.SqlBackup(SqlSever);
                            }
                        }
                        catch (SqlException sqlex)
                        {
                            ExceptionManager.Publish(sqlex);
                            iResult = 0;
                        }
                        catch (TimeoutException tex)
                        {
                            ExceptionManager.Publish(tex);
                            iResult = 0;
                        }
                        zipFileName = Backuppath + "bkp" + oFactoryResetEntity.BackUpDataBase + date + ".zip";
                        oFactoryResetEntity.strZipFileName = zipFileName;
                        iResult = 1;

                    }
                }
                catch (FileNotFoundException fex)
                {
                    ExceptionManager.Publish(fex);
                    iResult = 0;
                }
                catch (FileLoadException flx)
                {
                    ExceptionManager.Publish(flx);
                    iResult = 0;
                }
                catch (Exception ex)
                {
                    ExceptionManager.Publish(ex);
                    iResult = 0;
                }
                return iResult;
            }

            /// <summary>
            /// Create  Zip for database back up 
            /// </summary>
            /// <returns>int 0 -not success, 1- success </returns>
            public int CreateDBZip(FactoryReset oFactoryResetEntity)
            {
                if (oFactoryResetEntity.iSeverity == 9)
                {
                return ZipDatabseFile(oFactoryResetEntity.strBackupFileName, oFactoryResetEntity.strZipFileName, oFactoryResetEntity.iSeverity);
                }
                else
                {
                  return  ZipDatabseFile(oFactoryResetEntity.strBackupFileName, oFactoryResetEntity.strZipFileName, oFactoryResetEntity.iSeverity);
                }
            }

            /// <summary>
            /// Get the site code
            /// </summary>
            /// <returns>site code </returns>
            public int  GetSiteCode()
            {
                int  iResult;                
                try
                {
                    iResult =int.Parse(SqlHelper.ExecuteScalar(CommonDataAccess.ExchangeConnectionString, CommandType.Text, "Select Code from Site").ToString());
                  
                }

                catch (Exception Ex)
                {
                    iResult = 0;
                    ExceptionManager.Publish(Ex);
                }
                return iResult;
            }
        #endregion   
                
      
    }   
}

