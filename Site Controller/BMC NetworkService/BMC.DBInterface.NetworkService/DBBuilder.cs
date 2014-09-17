/*-------------------------------------------------------------------------- 
--
-- Description: DBBuilder class - functions related to DB.
-- Revision History
-- 
-- Author               Date                Remarks
-- Anuradha            03 Feb 2009          Initial Version
----------------------------------------------------------------------------- */

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32;
using BMC.Common.ExceptionManagement;
using System.Data.SqlClient;
using BMC.DataAccess;
using System.Data;
using BMC.Transport.NetworkService;
using BMC.Common.LogManagement;
using System.IO;
using BMC.Common.Utilities;

namespace BMC.DBInterface.NetworkService
{
    public static class DBBuilder
    {
        public static string SiteCode { get; set; }

        #region Declarations
         static string strExchangeConnectionString = string.Empty;
        static Dictionary<string, string> objParameters = null;
        #endregion

        #region Get Settings From DB
         /// <summary>
        /// Get Settings from Setting table
        /// </summary>
        /// <param name="SettingName"></param>
        /// <returns>string</returns>
        ///  /// Method Revision History
        ///
        /// Author             Date              Description
        /// ---------------------------------------------------
        /// Anuradha       03-Feb-2009          Intial Version 
        /// 

        public static string GetSettingFromDB(string SettingName, string SettingDefault)
        {
            string strReturnSetting = string.Empty;
            try
            {
                SqlParameter[] sqlparams = GetSettingParameters(SettingName, SettingDefault);
                SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, DBConstants.RSP_GETSETTING, sqlparams);

                if (sqlparams[3].Value != null || sqlparams[3].Value.ToString() != string.Empty)
                {
                    strReturnSetting = Convert.ToString(sqlparams[3].Value);
                }
                else
                {
                    strReturnSetting = string.Empty;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

            return strReturnSetting;
        }

        private static SqlParameter[] GetSettingParameters(string SettingName, string SettingDefault)
        {
            SqlParameter[] sqlparams = null;
            try
            {

                if (SettingName != null)
                {
                    sqlparams = new SqlParameter[5];

                    sqlparams[0] = new SqlParameter(DBConstants.CONST_SP_PARAM_SETTINGID, string.Empty);
                    sqlparams[1] = new SqlParameter(DBConstants.CONST_SP_PARAM_SETTINGNAME, SettingName.Trim());
                    sqlparams[2] = new SqlParameter(DBConstants.CONST_SP_PARAM_SETTINGDEFAULT, SettingDefault);

                    sqlparams[3] = new SqlParameter();
                    sqlparams[3].ParameterName = DBConstants.CONST_SP_PARAM_SETTINGVALUE;
                    sqlparams[3].Direction = ParameterDirection.Output;
                    sqlparams[3].Value = string.Empty;
                    sqlparams[3].SqlDbType = SqlDbType.VarChar;
                    sqlparams[3].Size = 100;

                    SqlParameter ReturnValue = new SqlParameter();
                    ReturnValue.ParameterName = DBConstants.CONST_SP_PARAM_RETURNVALUE;
                    ReturnValue.Direction = ParameterDirection.ReturnValue;
                    sqlparams[4] = ReturnValue;

                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return sqlparams;
        }

    #endregion

        #region Retrieve Exchange Connection String
        /// <summary>
        /// Get Exchange Connection string
        /// </summary>
        /// <param name="strConnect"></param>
        /// <returns>string</returns>
        ///  /// Method Revision History
        ///
        /// Author             Date              Description
        /// ---------------------------------------------------
        /// Anuradha       03-Feb-2009          Used existing code for retreiving Connection String
        /// 

        public static string GetConnectionString()
        {
            string strConnectionString = string.Empty;
            //BGSGeneral.cConstants objBGSConstants = null;
            //BGSEncryptDecrypt.clsBlowFish objDecrypt = null;
            //bool bUseHex = true;

            try
            {
                //Check if Connection string already exists.
                if (!string.IsNullOrEmpty(strExchangeConnectionString))
                {
                    return strExchangeConnectionString;
                }


                strExchangeConnectionString = BMC.Common.Utilities.DatabaseHelper.GetExchangeConnectionString();


            }
            catch (Exception ex)
            {
                if (ex.Message == "Connectionstring Not Found.")
                {
                    throw ex;
                }
                else
                {
                    ExceptionManager.Publish(ex);
                }
                strExchangeConnectionString = "";
            }
            finally
            {
                //objBGSConstants = null;
                //objDecrypt = null;
            }
            //LogManager.WriteLog("Connection string " + strExchangeConnectionString, LogManager.enumLogLevel.Info);
            //commmented on request-kiruba
            return strExchangeConnectionString;
        }

        public static string GetTicketingConnectionString()
        {
            try
            {
                return DatabaseHelper.GetTicketingConnectionString();

            }
            catch (Exception ex)
            {
                if (ex.Message == "Ticket Connectionstring Not Found.")
                {
                    throw ex;
                }
                else
                {
                    ExceptionManager.Publish(ex);
                }
                return "";
            }
        }
        //
        //private static string GetEnterpriseConnectionString()
        //{
        //    string strConnectionString = string.Empty;
        //    //BGSGeneral.cConstants objBGSConstants = null;
        //    //BGSEncryptDecrypt.clsBlowFish objDecrypt = null;
        //    //bool bUseHex = true;

        //    try
        //    {
        //        //Check if Connection string already exists.
        //        if (!string.IsNullOrEmpty(strExchangeConnectionString))
        //        {
        //            return strExchangeConnectionString;
        //        }


        //        //Get the connection string from registry
        //        RegistryKey regKeyConnectionString = BMCRegistryHelper.GetRegLocalMachine().OpenSubKey("Software\\Honeyframe\\Cashmaster");
        //        strConnectionString = regKeyConnectionString.GetValue("SQLConnect").ToString();

        //        regKeyConnectionString.Close();

        //        //Decrypt the connection string.
        //        if (!strConnectionString.ToUpper().Contains("SERVER"))
        //        {
        //            //objBGSConstants = new BGSGeneral.cConstants();
        //            //objDecrypt = new BGSEncryptDecrypt.clsBlowFish();
        //            //string strKey = objBGSConstants.ENCRYPTIONKEY;
        //            //strConnectionString = objDecrypt.DecryptString(ref strConnectionString, ref strKey, ref bUseHex);
        //            strConnectionString = BMC.Common.Security.CryptEncode.Decrypt(strConnectionString);

        //        }

        //        //Check if the connection string is properly decrypted.
        //        if (string.IsNullOrEmpty(strExchangeConnectionString))
        //        {
        //            if (strConnectionString.ToUpper().Contains("SERVER"))
        //                strExchangeConnectionString = strConnectionString;
        //            else
        //                throw new Exception("Error Decrypting Registry");
        //        }


        //    }
        //    catch (Exception ex)
        //    {
        //        if (ex.Message == "Connectionstring Not Found.")
        //        {
        //            throw ex;
        //        }
        //        else
        //        {
        //            ExceptionManager.Publish(ex);
        //        }
        //        strExchangeConnectionString = "";
        //    }
        //    finally
        //    {
        //        //objBGSConstants = null;
        //        //objDecrypt = null;
        //    }

        //    return strExchangeConnectionString;
        //}

        #endregion

        #region Check If Time falls within Opening Hours
        /// <summary>
        /// Execute Query
        /// </summary>
        /// <returns>success or failure</returns>
        ///  /// Method Revision History
        ///
        /// Author             Date              Description
        /// ---------------------------------------------------
        /// Anuradha       04-Feb-2009          Intial Version 
        /// 

        public static Dictionary<int,bool> ShouldMachineBeEnabled(InstallationEntity[] Installations)
        {
            Dictionary<int, bool> dMachineShouldbeEnabled = null;
            SqlParameter[] sInstallparams=null;
            object objShouldMachineBeEnabled = null;

            try
            {
                dMachineShouldbeEnabled = new Dictionary<int, bool>();

                foreach (InstallationEntity Installation in Installations)
                {
                    //Get the parameters.
                    objParameters = new Dictionary<string, string>();
                    objParameters.Add("@InstallationNo", Installation.InstallationNumber.ToString());
                    sInstallparams = AddParameter(objParameters);

                    //Check if the machine should be enabled or disabled.
                    objShouldMachineBeEnabled = SqlHelper.ExecuteScalar(GetConnectionString(), CommandType.StoredProcedure,
                        DBConstants.RSP_CHECKSHOULDMACHINEBEENABLED, sInstallparams);

                    //Add the machines and their status.
                    dMachineShouldbeEnabled.Add(Installation.InstallationNumber, Convert.ToBoolean(sInstallparams[1].Value));
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            finally
            {
                objParameters = null;
            }
            return dMachineShouldbeEnabled;
        }
        #endregion

        #region Update Time Since last External Connection
        /// <summary>
        /// Update Time Since last External Connection
        /// </summary>
        /// <returns>success or failure</returns>
        ///  /// Method Revision History
        ///
        /// Author             Date              Description
        /// ---------------------------------------------------
        /// Anuradha       04-Feb-2009          Intial Version 
        /// 

        public static bool UpdateTimeSinceLastExternalConn()
        {
            bool bUpdated = false;

            try
            {
                string strCurrentDateTime = DateTime.Now.ToString("MMM dd yyyy HH:mm:ss");
                objParameters = new Dictionary<string, string>();
                objParameters.Add(DBConstants.CONST_SP_PARAM_SETTINGNAME, "TIME_SINCE_LAST_EXTERNAL_CONNECTION");
                objParameters.Add(DBConstants.CONST_SP_PARAM_SETTINGVALUE, strCurrentDateTime);

                object objSetting = SqlHelper.ExecuteScalar(GetConnectionString(), CommandType.StoredProcedure,
                    DBConstants.USP_EDITSETTING, AddParameter(objParameters));

                if (objSetting != null)
                {
                    if (objSetting.ToString().Equals(strCurrentDateTime)) bUpdated = true;
                }
                else
                {
                    bUpdated = false;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            finally
            {
                objParameters = null;
            }
            return bUpdated;
        }
        #endregion

        #region Update External Connection Setting
        /// <summary>
        /// Update External Connection Setting
        /// </summary>
        /// <returns>success or failure</returns>
        ///  /// Method Revision History
        ///
        /// Author             Date              Description
        /// ---------------------------------------------------
        /// Anuradha       04-Feb-2009          Intial Version 
        /// 

        public static bool UpdateExternalConnection(bool ExternalConnection)
        {
            bool bUpdated = false;

            try
            { 
               objParameters = new Dictionary<string, string>();
                objParameters.Add(DBConstants.CONST_SP_PARAM_SETTINGNAME, "External_Connection");
                objParameters.Add(DBConstants.CONST_SP_PARAM_SETTINGVALUE, ExternalConnection.ToString());

                object objSetting = SqlHelper.ExecuteScalar(GetConnectionString(), CommandType.StoredProcedure,
                    DBConstants.USP_EDITSETTING, AddParameter(objParameters));

                if (objSetting != null)
                {
                    if (Convert.ToBoolean(objSetting) == ExternalConnection)
                    {
                        bUpdated = true;
                    }

                    //Call sp to reset the floor controller status.
                    SqlHelper.ExecuteScalar(GetConnectionString(), CommandType.StoredProcedure, "usp_ResetFloorControllerStatus");
                }
                else
                {
                    bUpdated = false;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            finally
            {
                objParameters = null;
            }
            return bUpdated;
        }
        #endregion

        #region Create Fault Events

        /// <summary>
        /// Create Fault Events
        /// </summary>
        /// <returns>success or failure</returns>
        ///  /// Method Revision History
        ///
        /// Author             Date              Description
        /// ---------------------------------------------------
        /// Anuradha       05-Feb-2009          Intial Version 
        /// 

        public static bool CreateFaultEvents(Dictionary<string, string> FaultEventParameters)
        {
            SqlParameter[] sqlparams = null;
            bool bFaultCreated = false;

            try
            {
                //Create a list of FaultEvent parameters.
                sqlparams = AddParameter(FaultEventParameters);

                //check if the fault event already exists. Put an entry only if the connection is not there.

                 int? result = null;

                LogManager.WriteLog("Parameter values " + sqlparams[4].Value + " , " + sqlparams[2].Value + " , " + sqlparams[1].Value + " , " + sqlparams[0].Value, LogManager.enumLogLevel.Info);
                result = Convert.ToInt32(SqlHelper.ExecuteScalar(GetConnectionString(), CommandType.StoredProcedure, DBConstants.RSP_CHECKIFFAULTEXISTS,
                    new SqlParameter[] { sqlparams[4], sqlparams[2], sqlparams[1], sqlparams[0] }));

                LogManager.WriteLog("Result for checking if exists record" + result.Value.ToString(), LogManager.enumLogLevel.Info);
                if (result.HasValue)
                {
                    if (result.Value == 1)
                    {
                        //Insert the Fault Event.
                        SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, DBConstants.USP_INSERTFAULTEVENT, sqlparams);

                        //See if the insertion is successfull.
                        if (int.Parse(sqlparams[6].Value.ToString()) == 0)
                        {
                            bFaultCreated = true;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return bFaultCreated;
        }
        #endregion Create Fault Events

        #region Get avalaible Installations
        /// <summary>
        /// Get Settings from Setting table
        /// </summary>
        /// <returns>list with available installations</returns>
        ///  /// Method Revision History
        ///
        /// Author             Date              Description
        /// ---------------------------------------------------
        /// Anuradha       05-Feb-2009          Intial Version 
        /// 

        public static InstallationEntity[] GetInstallations()
        {
            InstallationEntity[] clsInstallation = null;
            DataTable dtInstallations = null;
            int iCount=0;
            try
            {
                // lInstallations = new List<int>();

                //Get available installations.

                dtInstallations = SqlHelper.ExecuteDataset(GetConnectionString(), CommandType.StoredProcedure,
                    DBConstants.RSP_GETINSTALLATIONDETAILS).Tables[0];

                if (dtInstallations != null)
                {
                    clsInstallation = new InstallationEntity[dtInstallations.Rows.Count];
                    //Add the installation no to the list.
                    foreach (DataRow row in dtInstallations.Rows)
                    {
                        clsInstallation[iCount] = new InstallationEntity();
                        clsInstallation[iCount].InstallationNumber = Convert.ToInt32(row["Installation_No"].ToString());
                        clsInstallation[iCount].BarPositionName = row["Bar_Pos_Name"].ToString();
                        clsInstallation[iCount].BarPositionMachineEnabled = row["BarPositionMachineEnabled"].ToString();
                        clsInstallation[iCount].DataPakNumber = int.Parse(row["Datapak_Serial"].ToString());
                        clsInstallation[iCount].BarPositionNo = int.Parse(row["Bar_Pos_No"].ToString());
                        iCount++;
                    }
                }

                //Get the site code.
                string SiteCode = SqlHelper.ExecuteScalar(GetConnectionString(), CommandType.Text,
                   "Select code from site").ToString();
                if (!String.IsNullOrEmpty(SiteCode))
                {
                    for (int k = 0; k < clsInstallation.Length; k++)
                    {
                        clsInstallation[k].SiteCode = SiteCode;
                    }
                }

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            finally
            {
                if (dtInstallations!=null)
                {
                    dtInstallations.Dispose();
                }
            }
            return clsInstallation;
        }
        #endregion Get avalaible Installations

        #region Update Bar Position Table 
        /// <summary>
        /// Update the status in Bar Position Table.
        /// </summary>
        /// <returns>list with available installations</returns>
        ///  /// Method Revision History
        ///
        /// Author             Date              Description
        /// ---------------------------------------------------
        /// Anuradha       05-Feb-2009          Intial Version 
        /// 

        public static bool UpdateBarPosition(Dictionary<string, string> BarPositions)
        {
            SqlParameter[] sqlparams = null;
            bool bUpdateBarPos = false;

            try
            {
                objParameters = new Dictionary<string, string>();
                objParameters.Add("BarPosNo", BarPositions["BarPosName"]);
                objParameters.Add("isMachine", BarPositions["isMachine"]);
                objParameters.Add("Status", BarPositions["Status"]);

                sqlparams = AddParameter(objParameters);
                SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure,
                    DBConstants.USP_UPDATEBARPOSITIONFORMACHINECONTROL, sqlparams);

                if (int.Parse(sqlparams[3].Value.ToString()) == 0)
                {
                    bUpdateBarPos = true;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
           
            return bUpdateBarPos;
        }
        #endregion Update Bar Position Table

   

        #region AddParameter
        /// <summary>
        /// Add a list of parameters
        /// </summary>
        /// <param name="Parameters"></param>
        /// <returns>list of parameters</returns>
        ///  /// Method Revision History
        ///
        /// Author             Date              Description
        /// ---------------------------------------------------
        /// Anuradha       05-Feb-2009          Intial Version 
        /// 
        private static SqlParameter[] AddParameter(Dictionary<string, string> Parameters)
        {
            SqlParameter[] sqlparams = null;

            try
            {
                if (Parameters.Count > 0)
                {
                    sqlparams = new SqlParameter[Parameters.Count + 1];
                }
                int iCount = 0;

                foreach (KeyValuePair<string, string> KeyValue in Parameters)
                {
                    if (!String.IsNullOrEmpty(KeyValue.Key))
                    {
                        sqlparams[iCount] = new SqlParameter(KeyValue.Key, KeyValue.Value);
                        iCount++;
                    }
                }
                sqlparams[iCount] = new SqlParameter(DBConstants.CONST_SP_PARAM_RETURNVALUE, string.Empty);
                sqlparams[iCount].Direction = ParameterDirection.ReturnValue;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

            return sqlparams;
        }
        #endregion

        #region AddOutputParameter
        /// <summary>
        /// Add a Output Parameter
        /// </summary>
        /// <param name="OutParameterName"></param>
        /// <returns>list of parameters</returns>
        ///  /// Method Revision History
        ///
        /// Author             Date              Description
        /// ---------------------------------------------------
        /// Anuradha       05-Feb-2009          Intial Version 
        /// 
        private static SqlParameter[] AddOutputParameter(string OutParameterName)
        {
            SqlParameter[] sqlparams = null;

            try
            {
                sqlparams = new SqlParameter[1];
                sqlparams[0] = new SqlParameter(OutParameterName, string.Empty);
                sqlparams[0].Direction = ParameterDirection.Output;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

            return sqlparams;
        }
        #endregion

        #region ReturnValue
        /// <summary>
        /// Add a return value parameter
        /// </summary>
        /// <returns>list of return value parameter</returns>
        ///  /// Method Revision History
        ///
        /// Author             Date              Description
        /// ---------------------------------------------------
        /// Anuradha       05-Feb-2009          Intial Version 
        /// 
        private static SqlParameter[] AddReturnParameter()
        {
            SqlParameter[] sqlparams = null;

            try
            {
                sqlparams = new SqlParameter[1];
                sqlparams[0] = new SqlParameter(DBConstants.CONST_SP_PARAM_RETURNVALUE, string.Empty);
                sqlparams[0].Direction = ParameterDirection.ReturnValue;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

            return sqlparams;
        }
        #endregion

        #region Update BarPosition  table in Exchange
        public static void UpdateBarPositionCentralStatus(string strBarposName, bool bStatus)
        {
            SqlParameter[] objParam = new SqlParameter[2];
            string strSPName = "usp_UpdateBarPositionCentralStatus";
            try
            {
                SqlParameter objSQLParam = new SqlParameter();
                objSQLParam.ParameterName = "BarPosName";
                objSQLParam.Value = strBarposName;
                objSQLParam.Direction = ParameterDirection.Input;
                objParam[0] = objSQLParam;

                objSQLParam = new SqlParameter();
                objSQLParam.ParameterName = "Status";
                objSQLParam.Value = bStatus;
                objSQLParam.SqlDbType = SqlDbType.Bit;
                objSQLParam.Direction = ParameterDirection.Input;
                objParam[1] = objSQLParam;

                SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, strSPName, objParam);
            }
            catch (Exception ex)
            {
                LogManager.WriteLog(ex.ToString(), LogManager.enumLogLevel.Error);
            }

        }

        public static bool UpdateMachineStatusinExchange(int strBarposNo, bool bMachine, bool bStatus)
        {
            SqlParameter[] objParam = new SqlParameter[3];
            try
            {
                SqlParameter objSQLParam = new SqlParameter();
                objSQLParam.ParameterName = "BarPosNo";
                objSQLParam.Value = strBarposNo;
                objSQLParam.Direction = ParameterDirection.Input;
                objParam[0] = objSQLParam;

                objSQLParam = new SqlParameter();
                objSQLParam.ParameterName = "isMachine";
                objSQLParam.Value = bMachine;
                objSQLParam.Direction = ParameterDirection.Input;
                objSQLParam.SqlDbType = SqlDbType.Bit;
                objParam[1] = objSQLParam;

                objSQLParam = new SqlParameter();
                objSQLParam.ParameterName = "Status";
                objSQLParam.Value = bStatus;
                objSQLParam.SqlDbType = SqlDbType.Bit;
                objSQLParam.Direction = ParameterDirection.Input;
                objParam[2] = objSQLParam;

                SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, DBConstants.USP_UPDATEBARPOSITIONFORMACHINECONTROL, objParam);
                return true;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog(ex.ToString(), LogManager.enumLogLevel.Error);
                return false;
            }
        }

        #endregion

        public static string GetSiteCode()
        {
            if (string.IsNullOrEmpty(SiteCode))
                SiteCode = (SqlHelper.ExecuteScalar(GetConnectionString(), CommandType.Text, "Select Code as SiteCode from Site").ToString());

            return SiteCode;            
        }

        public static DataTable GetAAMSDetails(int EntityType)
        {
            try
            {
                SqlParameter[] parames = new SqlParameter[1];
                parames[0] = new SqlParameter();
                parames[0].ParameterName = "@EntityType";
                parames[0].Value = EntityType;

                return SqlHelper.ExecuteDataset(GetConnectionString(), CommandType.StoredProcedure, "rsp_GetAAMSMonitoringData", parames).Tables[0];
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
                return new DataTable();
            }
        }

        public static DataTable GetFinalDropInstallations()
        {
            try
            {
                return SqlHelper.ExecuteDataset(GetConnectionString(), CommandType.StoredProcedure, "rsp_GetFinalDropInstallations").Tables[0];
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
                return new DataTable();
            }
        }

        public static void UpdateAAMSStatus(int Reference, string CurrentStatus, string strComments, int iEntityType, int iInstallationNo, DateTime updateDate)
        {
            try
            {
                //LogManager.WriteLog("Current Status - " + CurrentStatus + ". Reference - " + Reference, LogManager.enumLogLevel.Info);
                DataBaseServiceHandler.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "usp_UpdateSuccessfulAAMS",
                    DataBaseServiceHandler.AddParameter<int>("BAD_ID", DbType.Int32, Reference),
                    DataBaseServiceHandler.AddParameter<int>("Current_Status ", DbType.Int32, (CurrentStatus == "1") ? 1 : 0),
                    DataBaseServiceHandler.AddParameter<string>("BAD_Comments", DbType.String, strComments),
                    DataBaseServiceHandler.AddParameter<int>("EntityType", DbType.Int32, iEntityType),
                    DataBaseServiceHandler.AddParameter<int>("InstallationNo", DbType.Int32, iInstallationNo),
                    DataBaseServiceHandler.AddParameter<DateTime>("UpdateDate", DbType.DateTime, updateDate));
                //LogManager.WriteLog("Updated current status successfully.", LogManager.enumLogLevel.Info);
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
        }

        public static string GetEnterpriseKey()
        {
            string EnterpriseKey = string.Empty;

            try
            {
                EnterpriseKey = DataBaseServiceHandler.ExecuteScalar<string>(GetConnectionString(), CommandType.StoredProcedure,
                                                                    "rsp_GetEnterprisePassKeys");
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
            return EnterpriseKey;
        }

        public static string GetExchangeKey()
        {
            string ExchangeKey = string.Empty;

            try
            {
                ExchangeKey = DataBaseServiceHandler.ExecuteScalar<string>(GetConnectionString(), CommandType.StoredProcedure,
                                                                    "rsp_GetExchangePassKeys");
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
            return ExchangeKey;
        }

        public static GameEntity[] GetGameDetails()
        {
            GameEntity[] objGameEntity = null;
            DataTable dtGameDetails = null;
            int iCount = 0;
            try
            {
                    dtGameDetails = SqlHelper.ExecuteDataset(GetConnectionString(), CommandType.StoredProcedure,
                    DBConstants.RSP_GETGAMEENABLEDISABLESTATUS).Tables[0];

                if (dtGameDetails != null)
                {
                    objGameEntity = new GameEntity[dtGameDetails.Rows.Count];
                    //Add the installation no to the list.
                    foreach (DataRow row in dtGameDetails.Rows)
                    {
                        objGameEntity[iCount] = new GameEntity ();
                        objGameEntity[iCount].InstallationNumber  = Convert.ToInt32(row["Installation_No"].ToString());
                        objGameEntity[iCount].GamePosition   =  row["Game_Position"].ToString();
                        objGameEntity[iCount].GameVerificationResult = Convert.ToInt32(row["Game_Verification"].ToString());
                        iCount++;
                    }
                }
                                

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

            return objGameEntity;
        }

        public static bool UpdateCommentsForAAMS(string BADID,string Comment,int EntityType,int iInstallationNo)
        {
            try
            {
                SqlParameter[] parames = new SqlParameter[4];
                parames[0] = new SqlParameter();
                parames[0].ParameterName = "@BADID";
                parames[0].Value = BADID;


                parames[1] = new SqlParameter();
                parames[1].ParameterName = "@BAD_Comments";
                parames[1].Value = Comment;

                parames[2] = new SqlParameter();
                parames[2].ParameterName = "@EntityType";
                parames[2].Value = EntityType;

                parames[3] = new SqlParameter();
                parames[3].ParameterName = "@InstallationNo";
                parames[3].Value = iInstallationNo;


               SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "usp_UpdateCommentsforAAMS", parames);
                return true;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return false;
        }
        
        /// <summary>
        /// Update the floor controller status to process the failed sector 203 command.
        /// </summary>
        /// <param name="p"></param>
        public static bool UpdateFloorControllerStatus(int BADID)
        {
            try
            {
                SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.Text, "Update BMC_AAMS_Details set BAD_Entity_Floor_Controller_Status=0 where bad_id=" + BADID);
                return true;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("UpdateFLoorControllerStatus Failed while sector 203 failure", LogManager.enumLogLevel.Info);
                ExceptionManager.Publish(ex);
            }
            return false;
        }

        /// <summary>
        /// Get the IP based on serial number from GMU login table
        /// </summary>
        /// <param name="p"></param>
        public static string GetGMUIPForSerialNo(int InstallationNo)
        {
            try
            {
                SqlParameter[] sqlparam = new SqlParameter[1];
                sqlparam[0] = new SqlParameter();
                sqlparam[0].ParameterName = "@InstallationNo";
                sqlparam[0].Value = InstallationNo;

                object objIP = SqlHelper.ExecuteScalar(GetConnectionString(), CommandType.StoredProcedure, "rsp_GetGMUIPFromAssetSerial", sqlparam);
                if (objIP != null)
                {
                    return objIP.ToString();
                }
                return string.Empty;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("UpdateFLoorControllerStatus Failed while sector 203 failure", LogManager.enumLogLevel.Info);
                ExceptionManager.Publish(ex);
            }
            return string.Empty;
        }
        /// <summary>
        /// Get all the GMUs
        /// </summary>
        /// <param name="p"></param>
        public static DataTable GetAllGmus(char AllGmus, int TimeDifference, int InstallationNo)
        {
            try
            {
                SqlParameter[] parames = new SqlParameter[3];

                parames[0] = new SqlParameter();
                parames[0].ParameterName = "@AllGmus";
                parames[0].Value = AllGmus;

                parames[1] = new SqlParameter();
                parames[1].ParameterName = "@TimeDifference";
                parames[1].Value = TimeDifference;

                parames[2] = new SqlParameter();
                parames[2].ParameterName = "@InstallationNo";
                parames[2].Value = InstallationNo;

                return SqlHelper.ExecuteDataset(GetConnectionString(), CommandType.StoredProcedure, "rsp_GetAllGmus", parames).Tables[0];
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("GetAllGmus Failed to get data of all GMUs", LogManager.enumLogLevel.Info);
                ExceptionManager.Publish(ex);
                return new DataTable();
            }
        }
        /// <summary>
        /// Get all the installations after meter
        /// </summary>
        /// <param name="p"></param>
        public static DataTable GetInstallationCountAfterMeter()
        {
            try
            {
                return SqlHelper.ExecuteDataset(GetConnectionString(), CommandType.StoredProcedure, "rsp_GetInstallationCountAfterMeter").Tables[0];
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("GetInstallationCountAfterMeter Failed to get data of installations after meter", LogManager.enumLogLevel.Info);
                ExceptionManager.Publish(ex);
                return new DataTable();
            }
        }
        //
        public static void UpdateTicketExpire(int value)
        {
            try
            {
                DataBaseServiceHandler.ExecuteNonQuery(
                    GetTicketingConnectionString(),
                    CommandType.StoredProcedure,
                    "usp_SetTicketExpire",
                    DataBaseServiceHandler.AddParameter<int>("Value", DbType.String, value));

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
        //
        /// <summary>
        /// Get all the installations after meter
        /// </summary>
        /// <param name="p"></param>
        public static DataTable GetTITOConfig()
        {
            try
            {
                return SqlHelper.ExecuteDataset(GetConnectionString(), CommandType.StoredProcedure, "rsp_GetTITOConfigInstallations").Tables[0];
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("GetTITOConfig Failed to get data of installations after meter", LogManager.enumLogLevel.Info);
                ExceptionManager.Publish(ex);
                return new DataTable();
            }
        }
        //
        public static void UpdatedTITOConfig(int Installation_ID, int SiteTITOEnabled, int SiteNonCashEnabled, int MachineTITOEnabled, int MachineNonCashEnabled,int EnableCommand)
        {
            try
            {
                DataBaseServiceHandler.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "usp_UpdatedTITOConfig",
                    DataBaseServiceHandler.AddParameter<int>("@Installation_No", DbType.Int32, Installation_ID),
                    DataBaseServiceHandler.AddParameter<int>("@SiteTITOEnabled", DbType.Int32, SiteTITOEnabled),
                    DataBaseServiceHandler.AddParameter<int>("@SiteNonCashEnabled", DbType.Int32, SiteNonCashEnabled),
                    DataBaseServiceHandler.AddParameter<int>("@MachineTITOEnabled", DbType.Int32, MachineTITOEnabled),
                    DataBaseServiceHandler.AddParameter<int>("@MachineNonCashEnabled", DbType.Int32, MachineNonCashEnabled),
                    DataBaseServiceHandler.AddParameter<int>("@EnableCommand", DbType.Int32, EnableCommand));
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
        }
        //
        public static void UpdateInstallationCountAfterMeter(int Installation_ID)
        {
            try
            {
                DataBaseServiceHandler.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "usp_GetInstallationCountAfterMeter",
                    DataBaseServiceHandler.AddParameter<int>("@Installation_No", DbType.Int32, Installation_ID));
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
        }
        /// <summary>
        /// Get the IP based on serial number from GMU login table
        /// </summary>
        /// <param name="p"></param>
        public static string GetGameAAMSDetails(string strGameName)
        {
            string strGameAAMSCode = string.Empty;

            try
            {
                SqlParameter[] sqlparam = new SqlParameter[1];
                sqlparam[0] = new SqlParameter();
                sqlparam[0].ParameterName = "@Game_Name";
                sqlparam[0].Value = strGameName;

                strGameAAMSCode = SqlHelper.ExecuteScalar(GetConnectionString(), CommandType.StoredProcedure, "rsp_GetGamesAAMSDetails", sqlparam).ToString();
            }
            catch (Exception ex)
            {
                strGameAAMSCode = string.Empty;
                LogManager.WriteLog("GetGameAAMSDetails Failed to get data from database for Game - " + strGameName, LogManager.enumLogLevel.Info);
                ExceptionManager.Publish(ex);
            }
            return strGameAAMSCode;
        }

        public static DataTable GetAFTPollingData()
        {
            try
            {
                return SqlHelper.ExecuteDataset(GetConnectionString(), CommandType.StoredProcedure, "rsp_GetMachinePolling").Tables[0];
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
                return new DataTable();
            }
        }

        public static DataTable GetEmployeeCardPollingData()
        {
            try
            {
                return SqlHelper.ExecuteDataset(GetConnectionString(), CommandType.StoredProcedure, "rsp_GetEmployeeCardPolling").Tables[0];
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
                return new DataTable();
            }
        }

        public static void UpdateAFTPolling(int Installation_ID)
        {
            try
            {
                DataBaseServiceHandler.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "usp_UpdateAFTPolling",
                    DataBaseServiceHandler.AddParameter<int>("@Installation_No", DbType.Int32, Installation_ID));
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
        }

        public static bool GetSiteStatus()
        {
            LogManager.WriteLog("Inside GetSiteStatus method", LogManager.enumLogLevel.Info);

            object result = SqlHelper.ExecuteScalar(GetConnectionString(),
                                                          "rsp_GetSiteStatus");

            return Convert.ToBoolean(result);
    }

        public static void UpdateBarPosition(int iInstallationNo, bool status)
        {
            try
            {
                LogManager.WriteLog("UpdateBarPosition  - started update for machine status", LogManager.enumLogLevel.Info);
                SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "usp_UpdateBarPositionMachineStatus",
                DataBaseServiceHandler.AddParameter<int>("InstallationNo", DbType.Int32, iInstallationNo),
                DataBaseServiceHandler.AddParameter<bool>("Status", DbType.Boolean, status),
                DataBaseServiceHandler.AddParameter<int>("ExportHistory", DbType.Int32, 0));
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Update Bar Position machine status failed", LogManager.enumLogLevel.Info);
                ExceptionManager.Publish(ex);
            }
        }

        public static DataTable GetInstallationsForTicketExpireUpdate()
        {
            try
            {
                return SqlHelper.ExecuteDataset(GetConnectionString(), CommandType.StoredProcedure, "rsp_GetInstallationsForTicketExpireUpdate").Tables[0];
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("GetInstallationsForTicketExpireUpdate Database Hit Failed.", LogManager.enumLogLevel.Info);
                ExceptionManager.Publish(ex);
                return new DataTable();
            }
        }

        public static void UpdateTicketExpireInstallations(int nInstallationNo)
        {
            try
            {
                DataBaseServiceHandler.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "usp_UpdateInstallationsForTicketExpire",
                    DataBaseServiceHandler.AddParameter<int>("@Installation_No", DbType.Int32, nInstallationNo));
            }
            catch (Exception Ex)
            {
                LogManager.WriteLog("usp_UpdateInstallationsForTicketExpire Database Hit Failed.", LogManager.enumLogLevel.Info);
                ExceptionManager.Publish(Ex);
            }
        }

        #region GMU Site Code Update

        public static DataTable GetInstallationsForGMUSiteCodeUpdate()
        {
            try
            {
                return SqlHelper.ExecuteDataset(DBBuilder.GetConnectionString(),
                    CommandType.StoredProcedure, "rsp_GetInstallationsToUpdateSiteCode").Tables[0];
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return new DataTable();
            }
        }

        public static bool UpdateGMUSiteCodeStatus(int iInstallationNo, int iStatus)
        {
            try
            {
                SqlParameter[] sqlparam = new SqlParameter[2];
                sqlparam[0] = new SqlParameter("@InstallationNo", iInstallationNo);
                sqlparam[1] = new SqlParameter("@Status", iStatus);

                SqlHelper.ExecuteNonQuery(DBBuilder.GetConnectionString(), "usp_UpdateGMUSiteCodeStatus", sqlparam);
                return true;
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
                return false;
            }
        }

        #endregion GMU Site Code Update

        #region Instant Periodic Interval

        public static bool IsInstantPeriodicIntervalModified()
        {
            SqlParameter[] sqlparams = null;

            try
            {
                sqlparams = new SqlParameter[1];

                sqlparams[0] = new SqlParameter();
                sqlparams[0].ParameterName = "@IsModified";
                sqlparams[0].Direction = ParameterDirection.Output;
                sqlparams[0].SqlDbType = SqlDbType.Bit;

                SqlHelper.ExecuteNonQuery(DBBuilder.GetConnectionString(), CommandType.StoredProcedure, "rsp_IsInstantPeriodicIntervalModified", (SqlParameter[])sqlparams);

                if (sqlparams[0].Value != DBNull.Value)
                {
                    bool value = false;
                    bool.TryParse(sqlparams[0].Value.ToString(), out value);
                    return value;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return false;
        }

        public static DataTable GetInstallationsForInstantPeriodicInterval()
        {
            try
            {
                return SqlHelper.ExecuteDataset(DBBuilder.GetConnectionString(),
                    CommandType.StoredProcedure, "rsp_GetInstallationsForInstantPeriodicInterval").Tables[0];
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return new DataTable();
            }
        }

        #endregion

        public static void UpdateEmployeeCardPolling(string EmployeecardNo, int InstallationNo)
        {
            try
            {
                DataBaseServiceHandler.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "usp_UpdateEmployeecardPolling",
                    DataBaseServiceHandler.AddParameter<int>("@InstallationNo", DbType.Int32, InstallationNo),
                    DataBaseServiceHandler.AddParameter<string>("@EmpCardNo ", DbType.String, EmployeecardNo));
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
        }
    }
}
