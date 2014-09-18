using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using BMC.Common.ExceptionManagement;
using BMC.DataAccess;
using BMC.Transport;
using BMC.Common.LogManagement;

namespace BMC.DBInterface.CashDeskOperator
{
    public class EnrollmentDataAccess
    {
        

        private SqlTransaction Tran;
        private SqlConnection SqlConn;
        

        

        
        public DataTable GetPositionDetails(string BarPosName)
        {
            try
            {
                SqlParameter oParam = new SqlParameter();
                oParam.ParameterName = "@BarPosName";
                oParam.Value = BarPosName;
                oParam.Direction = ParameterDirection.Input;

                SqlParameter[] oEventParam = new SqlParameter[1];
                oEventParam[0] = oParam;

                return SqlHelper.ExecuteDataset(CommonDataAccess.ExchangeConnectionString, CommandType.StoredProcedure, DBConstants.CONST_SP_RSP_GETPOSITIONDETAILS, oEventParam).Tables[0];
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
                return null;
            }
        }

   
        public DataSet GetMeterLife(int InstallationNo)
        {
            SqlParameter[] objSQlParams = new SqlParameter[1];
            SqlParameter objSQL = new SqlParameter();
            objSQL.ParameterName = "inst_id";
            objSQL.Direction = ParameterDirection.Input;
            objSQL.Value = InstallationNo;
            objSQlParams[0] = objSQL;

            try
            {
                return SqlHelper.ExecuteDataset(CommonDataAccess.ExchangeConnectionString, CommandType.StoredProcedure, DBConstants.CONSTANT_RSP_GETMETERLIFE, objSQlParams);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return null;
            }
        }

        public DataSet GetCurrentDayMeters(int InstallationNo)
        {
            SqlParameter[] objSQlParams = new SqlParameter[1];
            SqlParameter objSQL = new SqlParameter();
            objSQL.ParameterName = "inst_id";
            objSQL.Direction = ParameterDirection.Input;
            objSQL.Value = InstallationNo;
            objSQlParams[0] = objSQL;

            try
            {
                return SqlHelper.ExecuteDataset(CommonDataAccess.ExchangeConnectionString, CommandType.StoredProcedure, DBConstants.CONSTANT_RSP_GETCURRENTDAYMETERS, objSQlParams);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return null;
            }
        }

        public bool CloseInstallation_Old(int InstallationNo, int MachineStatusFlag, string MachineTransitSiteCode)
        {
            try
            {
                //Call the SP to close the installation, clear FF and close the machine.
                SqlParameter[] objSQlParams = new SqlParameter[4];

                SqlParameter objSQL1 = new SqlParameter();
                objSQL1.ParameterName = "InstallationNo";
                objSQL1.Direction = ParameterDirection.Input;
                objSQL1.Value = InstallationNo;
                objSQlParams[0] = objSQL1;

                SqlParameter objSQL2 = new SqlParameter();
                objSQL2.ParameterName = "UserID";
                objSQL2.Direction = ParameterDirection.Input;
                objSQL2.Value = Security.SecurityHelper.CurrentUser.SecurityUserID;
                objSQlParams[1] = objSQL2;

                SqlParameter objSQL3 = new SqlParameter();
                objSQL3.ParameterName = "MachineStatusFlag";
                objSQL3.Direction = ParameterDirection.Input;
                objSQL3.Value = MachineStatusFlag;
                objSQlParams[2] = objSQL3;

                SqlParameter objSQL4 = new SqlParameter();
                objSQL4.ParameterName = "MachineTransitSiteCode";
                objSQL4.Direction = ParameterDirection.Input;
                objSQL4.Value = MachineTransitSiteCode;
                objSQlParams[3] = objSQL4;

                if (SqlHelper.ExecuteNonQuery(CommonDataAccess.ExchangeConnectionString, CommandType.StoredProcedure, "esp_CloseInstallation", objSQlParams) > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return false;
            }

        }

        #region Close Installation (esp_CloseInstallation stored procedure is splitted into multiple stored procedures)
        private class CloseInstallationArg
        {
            public int InstallationNo { get; set; }
            public int MachineStatusFlag { get; set; }
            public string MachineTransitSiteCode { get; set; }
            public string SerialNo { get; set; }
        }

        private delegate bool CloseInstallationHandler(SqlConnection conn, CloseInstallationArg argument);

        public bool CloseInstallation(int InstallationNo, int MachineStatusFlag, string MachineTransitSiteCode)
        {
            bool result = true;
            using (SqlConnection conn = new SqlConnection(CommonDataAccess.ExchangeConnectionString))
            {
                try
                {
                    conn.Open();
                    if (System.Transactions.Transaction.Current != null)
                    {
                        conn.EnlistTransaction(System.Transactions.Transaction.Current);
                    }

                    CloseInstallationArg argument = new CloseInstallationArg()
                    {
                        InstallationNo = InstallationNo,
                        MachineStatusFlag = MachineStatusFlag,
                        MachineTransitSiteCode = MachineTransitSiteCode
                    };
                    CloseInstallationHandler[] handlers = new CloseInstallationHandler[] 
                    {
                        CloseInstallation_Machine, CloseInstallation_Read, CloseInstallation_VTP,
                        CloseInstallation_MeterHistory, CloseInstallation_MGMD, 
                        CloseInstallation_FloorFinancials, CloseInstallation_BAD_IGI
                    };

                    foreach (CloseInstallationHandler handler in handlers)
                    {
                        try
                        {
                            result &= handler(conn, argument);
                        }
                        catch (Exception ex2)
                        {
                            ExceptionManager.Publish(ex2);
                            result &= false;
                        }
                        if (!result) break;
                    }
                }
                catch (Exception ex)
                {
                    ExceptionManager.Publish(ex);
                    result = false;
                }
            }
            return result;
        }

        private bool ExecuteQueryWithCommandTimeout(SqlConnection conn, string procedureName, SqlParameter[] parameters)
        {
            return this.ExecuteQueryWithCommandTimeout(conn, procedureName, parameters, null);
        }

        private bool ExecuteQueryWithCommandTimeout(SqlConnection conn, string procedureName, SqlParameter[] parameters,
            Action afterExecute)
        {
            bool result = false;
            try
            {
                SqlParameter[] newParameters = new SqlParameter[parameters.Length + 1];
                Array.Copy(parameters, newParameters, parameters.Length);
                SqlParameter returnValue = new SqlParameter("@ReturnValue", SqlDbType.Int);
                returnValue.Direction = ParameterDirection.ReturnValue;
                newParameters[newParameters.Length - 1] = returnValue;

                int queryResult = SqlHelper.ExecuteNonQueryWithCommandTimeOut(conn, CommandType.StoredProcedure,
                    procedureName, 300, newParameters);
                if (afterExecute != null)
                {
                    afterExecute();
                }
                if (returnValue.Value != DBNull.Value)
                {
                    result = ((int)returnValue.Value == 0);
                }
            }
            finally { }
            return result;
        }

        //Call the installation and machine
        private bool CloseInstallation_Machine(SqlConnection conn, CloseInstallationArg argument)
        {
            SqlParameter[] objSQlParams = new SqlParameter[5];

            SqlParameter objSQL1 = new SqlParameter();
            objSQL1.ParameterName = "InstallationNo";
            objSQL1.Direction = ParameterDirection.Input;
            objSQL1.Value = argument.InstallationNo;
            objSQlParams[0] = objSQL1;

            SqlParameter objSQL2 = new SqlParameter();
            objSQL2.ParameterName = "UserID";
            objSQL2.Direction = ParameterDirection.Input;
            objSQL2.Value = Security.SecurityHelper.CurrentUser.SecurityUserID;
            objSQlParams[1] = objSQL2;

            SqlParameter objSQL3 = new SqlParameter();
            objSQL3.ParameterName = "MachineStatusFlag";
            objSQL3.Direction = ParameterDirection.Input;
            objSQL3.Value = argument.MachineStatusFlag;
            objSQlParams[2] = objSQL3;

            SqlParameter objSQL4 = new SqlParameter();
            objSQL4.ParameterName = "MachineTransitSiteCode";
            objSQL4.Direction = ParameterDirection.Input;
            objSQL4.Value = argument.MachineTransitSiteCode;
            objSQlParams[3] = objSQL4;

            SqlParameter objSQL5 = new SqlParameter();
            objSQL5.ParameterName = "SerialNo";
            objSQL5.Direction = ParameterDirection.Output;
            objSQL5.Size = 50;
            objSQlParams[4] = objSQL5;

            return this.ExecuteQueryWithCommandTimeout(conn, "dbo.usp_CloseInstallation_Machine", objSQlParams,
                () =>
                {
                    if (objSQL5.Value != DBNull.Value)
                    {
                        argument.SerialNo = objSQL5.Value.ToString();
                    }
                });
        }

        private bool CloseInstallation_Read(SqlConnection conn, CloseInstallationArg argument)
        {
            SqlParameter[] objSQlParams = new SqlParameter[1];

            SqlParameter objSQL1 = new SqlParameter();
            objSQL1.ParameterName = "InstallationNo";
            objSQL1.Direction = ParameterDirection.Input;
            objSQL1.Value = argument.InstallationNo;
            objSQlParams[0] = objSQL1;

            return this.ExecuteQueryWithCommandTimeout(conn, "dbo.usp_CloseInstallation_Read", objSQlParams);
        }

        private bool CloseInstallation_VTP(SqlConnection conn, CloseInstallationArg argument)
        {
            SqlParameter[] objSQlParams = new SqlParameter[1];

            SqlParameter objSQL1 = new SqlParameter();
            objSQL1.ParameterName = "InstallationNo";
            objSQL1.Direction = ParameterDirection.Input;
            objSQL1.Value = argument.InstallationNo;
            objSQlParams[0] = objSQL1;

            return this.ExecuteQueryWithCommandTimeout(conn, "dbo.usp_CloseInstallation_VTP", objSQlParams);
        }

        private bool CloseInstallation_MeterHistory(SqlConnection conn, CloseInstallationArg argument)
        {
            SqlParameter[] objSQlParams = new SqlParameter[1];

            SqlParameter objSQL1 = new SqlParameter();
            objSQL1.ParameterName = "InstallationNo";
            objSQL1.Direction = ParameterDirection.Input;
            objSQL1.Value = argument.InstallationNo;
            objSQlParams[0] = objSQL1;

            return this.ExecuteQueryWithCommandTimeout(conn, "dbo.usp_CloseInstallation_MeterHistory", objSQlParams);
        }

        private bool CloseInstallation_MGMD(SqlConnection conn, CloseInstallationArg argument)
        {
            SqlParameter[] objSQlParams = new SqlParameter[1];

            SqlParameter objSQL1 = new SqlParameter();
            objSQL1.ParameterName = "InstallationNo";
            objSQL1.Direction = ParameterDirection.Input;
            objSQL1.Value = argument.InstallationNo;
            objSQlParams[0] = objSQL1;

            return this.ExecuteQueryWithCommandTimeout(conn, "dbo.usp_CloseInstallation_MGMD", objSQlParams);
        }

        private bool CloseInstallation_FloorFinancials(SqlConnection conn, CloseInstallationArg argument)
        {
            SqlParameter[] objSQlParams = new SqlParameter[1];

            SqlParameter objSQL1 = new SqlParameter();
            objSQL1.ParameterName = "InstallationNo";
            objSQL1.Direction = ParameterDirection.Input;
            objSQL1.Value = argument.InstallationNo;
            objSQlParams[0] = objSQL1;

            return this.ExecuteQueryWithCommandTimeout(conn, "dbo.usp_CloseInstallation_FloorFinancials", objSQlParams);
        }

        private bool CloseInstallation_BAD_IGI(SqlConnection conn, CloseInstallationArg argument)
        {
            SqlParameter[] objSQlParams = new SqlParameter[2];

            SqlParameter objSQL1 = new SqlParameter();
            objSQL1.ParameterName = "InstallationNo";
            objSQL1.Direction = ParameterDirection.Input;
            objSQL1.Value = argument.InstallationNo;
            objSQlParams[0] = objSQL1;

            SqlParameter objSQL2 = new SqlParameter();
            objSQL2.ParameterName = "SerialNo";
            objSQL2.Direction = ParameterDirection.Input;
            objSQL2.Value = argument.SerialNo;
            objSQlParams[1] = objSQL2;

            return this.ExecuteQueryWithCommandTimeout(conn, "dbo.usp_CloseInstallation_BAD_IGI", objSQlParams);
        }

        #endregion

        public bool UpdateMachineStatusForClosedInstallation(int InstallationNo)
        {
            try
            {   
                SqlParameter[] objSQlParams = new SqlParameter[1];

                SqlParameter objSQL1 = new SqlParameter();
                objSQL1.ParameterName = "Installation_No";
                objSQL1.Direction = ParameterDirection.Input;
                objSQL1.Value = InstallationNo;
                objSQlParams[0] = objSQL1;                

                if (SqlHelper.ExecuteNonQuery(CommonDataAccess.ExchangeConnectionString, CommandType.StoredProcedure, "usp_UpdateMachineStatusForClosedInstallation", objSQlParams) > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return false;
            }

        }

        public string GetClosedInstallationXML(int InstallationNo)
        {
            SqlParameter[] objSQlParams = new SqlParameter[1];
            SqlParameter objSQL1 = new SqlParameter();
            objSQL1.ParameterName = "Installation_No";
            objSQL1.Direction = ParameterDirection.Input;
            objSQL1.Value = InstallationNo;
            objSQlParams[0] = objSQL1;

            try
            {

                return SqlHelper.ExecuteScalar(CommonDataAccess.ExchangeConnectionString, CommandType.StoredProcedure, "rsp_GetRemoveInstallationXML", objSQlParams).ToString();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return null;
            }
        }

        public bool ExportRemoveInstallation(int InstallationNo)
        {
            try
            {
                //Call the SP to close the installation, clear FF and close the machine.
                SqlParameter[] objSQlParams = new SqlParameter[2];
                SqlParameter objSQL1 = new SqlParameter();
                objSQL1.ParameterName = "@Reference1";
                objSQL1.Direction = ParameterDirection.Input;
                objSQL1.Value = InstallationNo.ToString();
                objSQlParams[0] = objSQL1;

                SqlParameter objSQL2 = new SqlParameter();
                objSQL2.ParameterName = "@Type";
                objSQL2.Direction = ParameterDirection.Input;
                objSQL2.Value = "REMOVEINSTALLATION";
                objSQlParams[1] = objSQL2;

                if (SqlHelper.ExecuteNonQuery(CommonDataAccess.ExchangeConnectionString, CommandType.StoredProcedure, "usp_Export_History", objSQlParams) > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return false;
            }
 
        }

        public int CreateInstallation(PositionDetails PosDetails)
        {
            LogManager.WriteLog("CreateInstallation:", LogManager.enumLogLevel.Info);
            SqlDataReader Reader = null;
            SqlConn = new SqlConnection(CommonDataAccess.ExchangeConnectionString);
            int InstallationNO=0;
            SqlConn.Open();
            Tran = SqlConn.BeginTransaction(IsolationLevel.ReadUncommitted, "Installation");
            try
            {
                ///Store the installation data in database
                var oArrayParam = new SqlParameter[24];
                var oParam1 = new SqlParameter
                {
                    ParameterName = "@Position",
                    Value = PosDetails.Position,
                    Direction = ParameterDirection.Input
                };
                oArrayParam[0] = oParam1;
                var oParam2 = new SqlParameter
                {
                    ParameterName = "@Machine_Stock_No",
                    Value = PosDetails.AssetNo,
                    Direction = ParameterDirection.Input
                };
                oArrayParam[1] = oParam2;
                var oParam3 = new SqlParameter
                {
                    ParameterName = "@MachineTypeCode",
                    Value = PosDetails.GameCode,
                    Direction = ParameterDirection.Input
                };
                oArrayParam[2] = oParam3;
                var oParam4 = new SqlParameter
                {
                    ParameterName = "@MachineName",
                    Value = PosDetails.Game,
                    Direction = ParameterDirection.Input
                };
                oArrayParam[3] = oParam4;
                var oParam5 = new SqlParameter
                {
                    ParameterName = "@SerialNo",
                    Value = PosDetails.SerialNo,
                    Direction = ParameterDirection.Input
                };
                oArrayParam[4] = oParam5;
                var oParam6 = new SqlParameter
                {
                    ParameterName = "@AltSerialNo",
                    Value = PosDetails.AltSerialNo,
                    Direction = ParameterDirection.Input
                };
                oArrayParam[5] = oParam6;
                var oParam7 = new SqlParameter
                {
                    ParameterName = "@BaseDenom",
                    Value = PosDetails.BaseDenom,
                    Direction = ParameterDirection.Input
                };
                oArrayParam[6] = oParam7;
                var oParam8 = new SqlParameter
                {
                    ParameterName = "@CreditValue",
                    Value = PosDetails.CreditValue,
                    Direction = ParameterDirection.Input
                };
                oArrayParam[7] = oParam8;
                var oParam9 = new SqlParameter
                {
                    ParameterName = "@Installation_Percentage_Payout",
                    Value = PosDetails.PercentagePayout,
                    Direction = ParameterDirection.Input
                };
                oArrayParam[8] = oParam9;
                var oParam10 = new SqlParameter
                {
                    ParameterName = "@Installation_Jackpot_Value",
                    Value = PosDetails.Jackpot,
                    Direction = ParameterDirection.Input
                };
                oArrayParam[9] = oParam10;

                var oParam11 = new SqlParameter
                {
                    ParameterName = "@Manufacturer_Name",
                    Value = PosDetails.Manufacturer,
                    Direction = ParameterDirection.Input
                };
                oArrayParam[10] = oParam11;

                var outParam = new SqlParameter
                {
                    ParameterName = "@Installation_No",
                    Value = 0,
                    Direction = ParameterDirection.Output
                };

                oArrayParam[11] = outParam;

                var oParam12 = new SqlParameter
                {
                    ParameterName = "@ActAssetNo",
                    Value = PosDetails.ActAssetNo,
                    Direction = ParameterDirection.Input
                };
                oArrayParam[12] = oParam12;

                var oParam13 = new SqlParameter
                {
                    ParameterName = "@GMUNo",
                    Value = PosDetails.GMUNo,
                    Direction = ParameterDirection.Input
                };
                oArrayParam[13] = oParam13;

                var oParam14 = new SqlParameter
                {
                    ParameterName = "@ActSerialNo",
                    Value = PosDetails.ActSerialNo,
                    Direction = ParameterDirection.Input
                };
                oArrayParam[14] = oParam14;

                var oParam15 = new SqlParameter
                {
                    ParameterName = "@EnrolmentFlag ",
                    Value = PosDetails.EnrolmentFlag,
                    Direction = ParameterDirection.Input
                };
                oArrayParam[15] = oParam15;

                var oParam16 = new SqlParameter
                {
                    ParameterName = "@CMPGameType",
                    Value = PosDetails.CMPGameType,
                    Direction = ParameterDirection.Input
                };
                oArrayParam[16] = oParam16;

                var oParam17 = new SqlParameter
                {
                    ParameterName = "@OperatorName",
                    Value = PosDetails.OperatorName,
                    Direction = ParameterDirection.Input
                };
                oArrayParam[17] = oParam17;

                var oParam18 = new SqlParameter
                {
                    ParameterName = "@IsMultiGame",
                    Value = PosDetails.isMultiGame,
                    Direction = ParameterDirection.Input
                };
                oArrayParam[18] = oParam18;

			    var oParam19 = new SqlParameter
                {
                    ParameterName = "@GetGameDetails",
                    Value = PosDetails.GetGameDetails,
                    Direction = ParameterDirection.Input
                };
                oArrayParam[19] = oParam19;

                var oParam20 = new SqlParameter
                {
                    ParameterName = "@IsDefaultAssetDetail",
                    Value = PosDetails.IsDefaultAssetDetail,
                    Direction = ParameterDirection.Input
                };
                oArrayParam[20] = oParam20;

                var oParam21 = new SqlParameter
                {
                    ParameterName = "@OccupanyHour",
                    Value = PosDetails.OccupancyHour,
                    Direction = ParameterDirection.Input
                };
                oArrayParam[21] = oParam21;
                
                var oParam22 = new SqlParameter
                {
                    ParameterName = "@AssetDisplayName",
                    Value = PosDetails.AssetDisplayName,
                    Direction = ParameterDirection.Input
                };
 				oArrayParam[22] = oParam22;
                var oParam23 = new SqlParameter
                               {
                                   ParameterName = "@GameTypeCode",
                                   Value = PosDetails.GameTypeCode,
                                   Direction = ParameterDirection.Input
                               };
                oArrayParam[23] = oParam23;


                Reader = SqlHelper.ExecuteReader(SqlConn, Tran, CommandType.StoredProcedure, "usp_CreateSimpleInstallation", oArrayParam, SqlHelper.SqlConnectionOwnership.External);


                InstallationNO = Int32.Parse(oArrayParam[11].Value.ToString());
                if (Reader != null) { if (!Reader.IsClosed) Reader.Close(); }
                return InstallationNO;
            }
            catch (Exception ex)
            {
                if (Reader != null)
                {
                    if (!Reader.IsClosed) Reader.Close();
                }

                RollbackTransaction(InstallationNO);
                ExceptionManager.Publish(ex);
                return 0;
            }
            finally
            {
                if (Reader != null)
                {
                    if (!Reader.IsClosed) Reader.Close();
                    Reader = null;
                }
            }
            
        }

        public string GetInstallationXML(int InstallationNo)
        {
            try
            {

                var oArrayParam = new SqlParameter[1];
                var oParam = new SqlParameter
                {
                    ParameterName = "@Installation_No",
                    Value = InstallationNo,
                    Direction = ParameterDirection.Input
                };
                oArrayParam[0] = oParam;
                return (string)SqlHelper.ExecuteScalar(CommonDataAccess.ExchangeConnectionString, CommandType.StoredProcedure, "rsp_GetInstallationXML", oArrayParam);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return "";
            }
        }
       
        public bool UpdateHQID(int intstallationNo, int HQInstallationNo)
        {
            try
            {
                //Call the SP to close the installation, clear FF and close the machine.
                SqlParameter[] objSQlParams = new SqlParameter[2];

                SqlParameter objSQL1 = new SqlParameter();
                objSQL1.ParameterName = "@InstallationNo";
                objSQL1.Direction = ParameterDirection.Input;
                objSQL1.Value = intstallationNo;
                objSQlParams[0] = objSQL1;

                SqlParameter objSQL2 = new SqlParameter();
                objSQL2.ParameterName = "@HQInstallationNo";
                objSQL2.Direction = ParameterDirection.Input;
                objSQL2.Value = HQInstallationNo;
                objSQlParams[1] = objSQL2;

                if (SqlHelper.ExecuteNonQuery(CommonDataAccess.ExchangeConnectionString, CommandType.StoredProcedure, "usp_UpdateHQID", objSQlParams) > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return false;
            }            
        }

        public bool UpdateGMUSiteCodeStatus(int iInstallationNo, int iStatus)
        {
            try
            {
                SqlParameter[] sqlparam = new SqlParameter[2];
                sqlparam[0] = new SqlParameter("@InstallationNo", iInstallationNo);
                sqlparam[1] = new SqlParameter("@Status", iStatus);

                SqlHelper.ExecuteNonQuery(CommonDataAccess.ExchangeConnectionString, "usp_UpdateGMUSiteCodeStatus", sqlparam);
                return true;
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
                return false;
            }
        }

        public bool CommitTransaction()
        {
           
            if (SqlConn.State == ConnectionState.Open)
            {
                Tran.Commit();
                SqlConn.Close();
            }
            return true;
        }
        public bool RollbackTransaction(int Installation_No)
        {
            
            
            if (SqlConn.State == ConnectionState.Open)
            {
                Tran.Rollback("Installation");
                SqlConn.Close();
                //TO DO: Call SP to delete Installation_Game_Info
                if (Installation_No > 0)
                {
                    //Call the SP to close the installation, clear FF and close the machine.
                    SqlParameter[] objSQlParams = new SqlParameter[1];

                    SqlParameter objSQL1 = new SqlParameter();
                    objSQL1.ParameterName = DBConstants.SP_PARAM_REMOVEINSTALLATIONREFERENCES;
                    objSQL1.Direction = ParameterDirection.Input;
                    objSQL1.Value = Installation_No;
                    objSQlParams[0] = objSQL1;

                    SqlHelper.ExecuteNonQuery(CommonDataAccess.ExchangeConnectionString, CommandType.StoredProcedure, DBConstants.SP_USP_REMOVEINSTALLATIONREFERENCES, objSQlParams);
                   

                }
            }
            return true;
            
        }

        public int GetBarPosPort(int InstallationNo)
        {
            
            try
            {
                var oArrayParamInstall = new SqlParameter[1];
                var oParamInstall = new SqlParameter
                {
                    ParameterName = "@Installation_No",
                    Value = InstallationNo,
                    Direction = ParameterDirection.Input
                };
                oArrayParamInstall[0] = oParamInstall;

                
                DataSet dsInstallation = SqlHelper.ExecuteDataset(CommonDataAccess.ExchangeConnectionString, CommandType.StoredProcedure, "rsp_GetInstallationdetails", oArrayParamInstall);
                return Int32.Parse(dsInstallation.Tables[0].Rows[0]["Bar_Pos_Port"].ToString());
            }
            catch (Exception ex)
            {
                return 0;
            }
            
                    
        }

        public bool SetMachineMaintenanceState(int installationNo)
        {
            int errorCode = 0;
            string errorMessage = string.Empty;

            try
            {
                LogManager.WriteLog("Inside Method", LogManager.enumLogLevel.Info);

                DataBaseServiceHandler.ExecuteNonQuery(CommonDataAccess.ExchangeConnectionString,
                    CommandType.StoredProcedure, DBConstants.CONST_SP_USP_INSERTMACHINEMAINTENANCEDETAILS,
                    DataBaseServiceHandler.AddParameter<int>(DBConstants.CONST_PARAM_USER_ID, DbType.Int32, BMC.Security.SecurityHelper.CurrentUser.SecurityUserID, ParameterDirection.Input),
                    DataBaseServiceHandler.AddParameter<int>(DBConstants.CONST_PARAM_INSTALATION_NO, DbType.Int32, installationNo, ParameterDirection.Input),
                    DataBaseServiceHandler.AddParameter<int>(DBConstants.CONST_PARAM_ERROR_CODE, DbType.Int32, errorCode, ParameterDirection.Output),
                    DataBaseServiceHandler.AddParameter<string>(DBConstants.CONST_PARAM_ERROR_MESSAGE, DbType.String, errorMessage, ParameterDirection.Output));

                LogManager.WriteLog(string.Format("{0} - {1}", "Error Code", errorCode.ToString()), LogManager.enumLogLevel.Info);
                LogManager.WriteLog(string.Format("{0} - {1}", "Error Message", errorMessage), LogManager.enumLogLevel.Info);

                return errorCode == 0 ? true : false;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return false;
            }
        }

        public bool SetMachinePreviousState(int installationNo)
        {
            int errorCode = 0;
            string errorMessage = string.Empty;

            try
            {
                LogManager.WriteLog("Inside Method", LogManager.enumLogLevel.Info);

                DataBaseServiceHandler.ExecuteNonQuery(CommonDataAccess.ExchangeConnectionString,
                    CommandType.StoredProcedure, DBConstants.CONST_SP_USP_UPDATEMACHINEMAINTENANCEDETAILS,                    
                    DataBaseServiceHandler.AddParameter<int>(DBConstants.CONST_PARAM_INSTALATION_NO, DbType.Int32, installationNo, ParameterDirection.Input),
                    DataBaseServiceHandler.AddParameter<int>(DBConstants.CONST_PARAM_ERROR_CODE, DbType.Int32, errorCode, ParameterDirection.Output),
                    DataBaseServiceHandler.AddParameter<string>(DBConstants.CONST_PARAM_ERROR_MESSAGE, DbType.String, errorMessage, ParameterDirection.Output));

                LogManager.WriteLog(string.Format("{0} - {1}", "Error Code", errorCode.ToString()), LogManager.enumLogLevel.Info);
                LogManager.WriteLog(string.Format("{0} - {1}", "Error Message", errorMessage), LogManager.enumLogLevel.Info);

                return errorCode == 0 ? true : false;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return false;
            }
        }


        public int GetInstallationNo(string strSerialNo)
        {
            try
            {
                var oArrayParam = new SqlParameter[1];
                var oParam = new SqlParameter
                {
                    ParameterName = "@SerialNo",
                    Value = strSerialNo,
                    Direction = ParameterDirection.Input
                };
                oArrayParam[0] = oParam;
                return (int)SqlHelper.ExecuteScalar(CommonDataAccess.ExchangeConnectionString, CommandType.StoredProcedure, "rsp_GetInstallationNoForMcRemoval", oArrayParam);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return 0;
            }
        }

        public int ExecuteHourlyVTP(int InstallationNumber, DateTime dtTheDate, int iTheHour, bool isRead)
        {
            int iResult = -1;
            int iValue = 0;
            try
            {

                var ReturnValue = DataBaseServiceHandler.AddParameter<int>(DBConstants.CONST_PARAM_RETURNVALUE, DbType.Int32, 0, ParameterDirection.ReturnValue);
                iValue = SqlHelper.ExecuteNonQuery(CommonDataAccess.ExchangeConnectionString, System.Data.CommandType.StoredProcedure, DBConstants.CONST_SP_USP_HOURLY_VTP,
                    DataBaseServiceHandler.AddParameter<int>(DBConstants.CONST_PARAM_THEINSTALLATION, DbType.Int32, InstallationNumber),
                    DataBaseServiceHandler.AddParameter<DateTime>(DBConstants.CONST_PARAM_THEDATETIME, DbType.DateTime, dtTheDate),
                    DataBaseServiceHandler.AddParameter<int>(DBConstants.CONST_PARAM_THEHOUR, DbType.Int16, iTheHour),
                    DataBaseServiceHandler.AddParameter<bool>(DBConstants.CONST_PARAM_ISREAD, DbType.Boolean, isRead),
                    ReturnValue);
                if (iValue > 0)
                {
                    if (ReturnValue != null)
                    {
                        if (ReturnValue.Value != DBNull.Value || ReturnValue.Value.ToString() != string.Empty)
                        {
                            iResult = int.Parse(ReturnValue.Value.ToString());
                        }
                    }

                }

                LogManager.WriteLog("SP Excecution for HourlyVTP: " + iValue, LogManager.enumLogLevel.Info);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                iResult = -1;
            }
            return iResult;
        }

        public bool UpdateHourlyStatsGamingday(string Installations)
        {
            int iValue = 0;
            bool bReturn = false;
            try
            {

                DateTime dtLastRundate = DateTime.Now;
                SqlParameter[] oParams = new SqlParameter[2];
                oParams[0] = new SqlParameter("@Installations", Installations);
                oParams[1] = new SqlParameter("@MaxDate", dtLastRundate);
                iValue = SqlHelper.ExecuteNonQuery(CommonDataAccess.ExchangeConnectionString, System.Data.CommandType.StoredProcedure, DBConstants.CONST_SP_ESP_COLLATEHSGAMINGDAY, oParams);

                if (iValue > 0)
                    bReturn = true;
                LogManager.WriteLog("Hourly Statistics updated successfully " + iValue.ToString() + "!", LogManager.enumLogLevel.Info);
            }
            catch (Exception ex)
            {
                bReturn = false;
                ExceptionManager.Publish(ex);
            }
            return bReturn;
        }

        public SqlDataReader GetAssetDetailsForPosition(int iInstallationNo)
        {
            SqlDataReader reader = null;
       
            try
            {
                var oArrayParam = new SqlParameter[1];
                var oParam = new SqlParameter
                {
                    ParameterName = "@InstallatioNo",
                    Value = iInstallationNo,
                    Direction = ParameterDirection.Input
                };
                oArrayParam[0] = oParam;
                reader = SqlHelper.ExecuteReader(CommonDataAccess.ExchangeConnectionString, System.Data.CommandType.StoredProcedure, DBConstants.CONSTANT_RSP_GETASSETDETAILS, oArrayParam);

            }
            catch (Exception ex)
            {
       
                ExceptionManager.Publish(ex);
            }
            return reader;
        }

        public void GetAssetDetails(int iInstallationNo, ref string SiteCode, ref string AssetNumber, ref string PosNumber)
        {
            DataSet AssetDetails = new DataSet();

            try
            {
                //DataBaseServiceHandler.ConnectionString = ExchangeConnectionString;
                AssetDetails = DataBaseServiceHandler.Fill(CommonDataAccess.ExchangeConnectionString, CommandType.StoredProcedure, DBConstants.CONSTANT_RSP_GETASSETPOSDETAILS, AssetDetails,
                    DataBaseServiceHandler.AddParameter<int>("Installation_No", DbType.Int32, iInstallationNo));
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
           
            PosNumber = AssetDetails.Tables[0].Rows[0]["Bar_Pos_Name"].ToString();
            SiteCode = AssetDetails.Tables[0].Rows[0]["Code"].ToString();
            AssetNumber = AssetDetails.Tables[0].Rows[0]["Stock_No"].ToString();
              
        }

        #region "Public Static Function"
        #endregion


        public void InsertIntoExportHistory(int InstallationNumber)
        {            
            try
            {
                
                SqlParameter[] oParams = new SqlParameter[4];

                oParams[0] = new SqlParameter("@Reference1", InstallationNumber.ToString());
                oParams[1] = new SqlParameter("@Reference2", null);
                oParams[2] = new SqlParameter("@Type", "COMMONDATA");
                oParams[3] = new SqlParameter("@Status", null);

                SqlHelper.ExecuteNonQuery(CommonDataAccess.ExchangeConnectionString, System.Data.CommandType.StoredProcedure, DBConstants.CONST_SP_EXPORT_HISTORY, oParams);

                LogManager.WriteLog("Requested for HQ_Installation number successfully ", LogManager.enumLogLevel.Info);
            }
            catch (Exception ex)
            {                
                ExceptionManager.Publish(ex);
            }
        }
    }
}
