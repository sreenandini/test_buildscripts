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

namespace BMC.DBInterface.NetworkService
{
    public class VerificationDBBuilder
    {
        //1
        public static void InsertVLTDetails()
        {
            try
            {
                SqlHelper.ExecuteNonQuery(DBBuilder.GetConnectionString(), CommandType.StoredProcedure, "usp_insertAAMSVerification");
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        //2
        public static DataTable GetUnprocessedVLTDetails()
        {
            try
            {
                return SqlHelper.ExecuteDataset(DBBuilder.GetConnectionString(), CommandType.StoredProcedure, "rsp_GetUnprocessedVLTRecords").Tables[0];
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return new DataTable();
            }
        }

        //3
        public static bool UpdateVLTVerificationStatus(string Serial, int Status)
        {
            SqlParameter[] sqlparam = new SqlParameter[2];
            sqlparam[0] = new SqlParameter("@Serial", Serial);
            sqlparam[1] = new SqlParameter("@Status", Status);
            try
            {
                SqlHelper.ExecuteNonQuery(DBBuilder.GetConnectionString(), CommandType.StoredProcedure, "usp_UpdateVLTVerificationStatus", sqlparam);
                return true;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return false;
            }
        }

        //4
        public static string GetVLTDetailsinXML(string Serial, string MessageAAMSID)
        {
            SqlParameter[] sqlparam = new SqlParameter[3];
            sqlparam[0] = new SqlParameter("@Serial", Serial);
            sqlparam[1] = new SqlParameter("@MessageAAMSID", MessageAAMSID);
            sqlparam[2] = new SqlParameter("@Type", "");
            try
            {
                return SqlHelper.ExecuteScalar(DBBuilder.GetConnectionString(), CommandType.StoredProcedure, "rsp_GetVLTDetailsinXML", sqlparam).ToString();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return string.Empty;
            }
        }

        //5
        public static bool UpdateAAMSVerificationStatus(int IHID, int Status)
        {
            SqlParameter[] sqlparam = new SqlParameter[2];
            sqlparam[0] = new SqlParameter("@IH_ID", IHID);
            sqlparam[1] = new SqlParameter("@IH_Status", Status);
            try
            {
                SqlHelper.ExecuteNonQuery(DBBuilder.GetConnectionString(), CommandType.StoredProcedure, "usp_UpdateAAMSVerificationStatus", sqlparam);
                return true;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return false;
            }
        }


        //6
        public static void ResetAAMSVerificationRecords()
        {
            try
            {
                SqlHelper.ExecuteNonQuery(DBBuilder.GetConnectionString(), CommandType.StoredProcedure, "usp_ResetAAMSVerificationRecords");
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        //7
        public static bool UpdateLastRuninSetting(string date)
        {
            SqlParameter[] sqlparam = new SqlParameter[1];
            sqlparam[0] = new SqlParameter("@date", date.ToString());

            try
            {
                SqlHelper.ExecuteNonQuery(DBBuilder.GetConnectionString(), CommandType.StoredProcedure, "usp_UpdateLastRuninSetting", sqlparam);
                return true;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return false;
            }
        }

        public static DataTable GetInstallationsToUpdateOptionFileParams()
        {

            try
            {
                return SqlHelper.ExecuteDataset(DBBuilder.GetConnectionString(), CommandType.StoredProcedure, "rsp_GetInstallationsToUpdateOptionFileParams").Tables[0];
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return new DataTable();
            }
        }

        public static DataTable GetLP21Details()
        {
            try
            {
                return SqlHelper.ExecuteDataset(DBBuilder.GetConnectionString(), CommandType.StoredProcedure, "rsp_GetDataforLP21").Tables[0];
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
                return new DataTable();
            }
        }

        public static DataTable GetB5ReqDetails()
        {
            try
            {
                return SqlHelper.ExecuteDataset(DBBuilder.GetConnectionString(), CommandType.StoredProcedure, "rsp_GetDataforB5Req").Tables[0];
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
                return new DataTable();
            }
        }

        public static DataTable GetNoSyncInstallations(char syncStatus)
        {
            SqlParameter[] sqlparam = new SqlParameter[1];
            sqlparam[0] = new SqlParameter("@SyncStatus", syncStatus);
            try
            {
                return SqlHelper.ExecuteDataset(DBBuilder.GetConnectionString(), CommandType.StoredProcedure, "rsp_GetNoSyncInstallations", sqlparam).Tables[0];
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
                return new DataTable();
            }
        }

        public static bool SyncInstallationStatus(int Installation_No, char syncStatus)
        {
            try
            {
                DataBaseServiceHandler.ExecuteNonQuery(DBBuilder.GetConnectionString(), CommandType.StoredProcedure, "usp_SyncInstallationStatus",
                    DataBaseServiceHandler.AddParameter<int>("@Installation_No", DbType.Int32, Installation_No), DataBaseServiceHandler.AddParameter<char>("@SyncStatus", DbType.String, syncStatus));

                return true;
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
                return false;
            }
        }

        public static bool UpdateB5ReqStatus(int RefID)
        {
            try
            {
                DataBaseServiceHandler.ExecuteNonQuery(DBBuilder.GetConnectionString(), CommandType.StoredProcedure, "usp_UpdateB5ReqStatus",
                    DataBaseServiceHandler.AddParameter<int>("@BAD_ID", DbType.Int32, RefID));

                return true;
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
                return false;
            }
        }

        public static void UpdateLP21Status(int ReferenceID, string Command, string UpdateType)
        {
            try
            {
                DataBaseServiceHandler.ExecuteNonQuery(DBBuilder.GetConnectionString(), CommandType.StoredProcedure, "usp_UpdateLP21Status",
                    DataBaseServiceHandler.AddParameter<int>("@Reference_ID", DbType.Int32, ReferenceID),
                    DataBaseServiceHandler.AddParameter<string>("@EntityCommand", DbType.String, Command),
                    DataBaseServiceHandler.AddParameter<string>("@UpdateType", DbType.String, UpdateType));
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
        }

        public static void UpdateAAMSVerification(int ReferenceID, int IH_Status)
        {
            try
            {
                DataBaseServiceHandler.ExecuteNonQuery(DBBuilder.GetConnectionString(), CommandType.StoredProcedure, "usp_UpdateAAMSIHVerification",
                    DataBaseServiceHandler.AddParameter<int>("@Reference_ID", DbType.Int32, ReferenceID),
                    DataBaseServiceHandler.AddParameter<int>("@IH_Status", DbType.Int32, IH_Status));
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
        }

        public static void UpdateSector203(int Installation_No, string cRC, string updateType)
        {
            try
            {
                //var output = DataBaseServiceHandler.AddParameter<bool>("@Verified", DbType.Boolean, false);
                DataBaseServiceHandler.ExecuteNonQuery(DBBuilder.GetConnectionString(), CommandType.StoredProcedure, "usp_UpdateSector203UpdateforCRC",
                    DataBaseServiceHandler.AddParameter<int>("@Installation_No", DbType.Int32, Installation_No),
                    DataBaseServiceHandler.AddParameter<string>("@cRC", DbType.String, cRC),
                    DataBaseServiceHandler.AddParameter<string>("@UpdateType", DbType.String, updateType));

                //return Convert.ToBoolean(output.Value);
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
                //return false;
            }
            
        }

        public static bool UpdateInstallationcRC(int Installation_No, string cRC)
        {
            try
            {
                var output = DataBaseServiceHandler.AddParameter<bool>("@Out", DbType.Boolean, false);
                DataBaseServiceHandler.ExecuteNonQuery(DBBuilder.GetConnectionString(), CommandType.StoredProcedure, "usp_UpdateInstallationcRC",
                    DataBaseServiceHandler.AddParameter<int>("@Installation_No", DbType.Int32, Installation_No),
                    DataBaseServiceHandler.AddParameter<string>("@cRC", DbType.String, cRC), output);

                return Convert.ToBoolean(output.Value);
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
                return false;
            }
        }

        public static void UpdatePendingRecords()
        {
            try
            {
                DataBaseServiceHandler.ExecuteNonQuery(DBBuilder.GetConnectionString(), CommandType.StoredProcedure, "usp_UpdateInProgressVerificationRecords");
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
        }

        public static void InsertExportHistory(string Type, string Reference)
        {
            try
            {
                DataBaseServiceHandler.ExecuteNonQuery(DBBuilder.GetConnectionString(), CommandType.StoredProcedure, "usp_Export_History",
                    DataBaseServiceHandler.AddParameter<string>("@Reference1", DbType.String, Reference),
                    DataBaseServiceHandler.AddParameter<string>("@Reference2", DbType.String, ""),
                    DataBaseServiceHandler.AddParameter<string>("@Type", DbType.String, Type),
                    DataBaseServiceHandler.AddParameter<string>("@Status", DbType.String, ""));
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
        }

        public static DataTable GetInstallationDetailsForPortBlocking()
        {
            try
            {
                return SqlHelper.ExecuteDataset(DBBuilder.GetConnectionString(), CommandType.StoredProcedure, "rsp_GetInstallationDetailsForPortBlocking").Tables[0];
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
                return new DataTable();
            }
        }

        public static bool UpdatePortDisabledStatusForPortBlocking(int installationNo)
        {
            try
            {
                DataBaseServiceHandler.ExecuteNonQuery(DBBuilder.GetConnectionString(), CommandType.StoredProcedure, "usp_UpdatePortDisabledStatus",
                    DataBaseServiceHandler.AddParameter<int>("@InstallationNo", DbType.Int32, installationNo));

                return true;
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
                return false;
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

        #region Component Verification

        public static DataTable GetUnprocessedCompVerDetails()
        {
            try
            {
                return SqlHelper.ExecuteDataset(DBBuilder.GetConnectionString(), CommandType.StoredProcedure, "rsp_GetUnProcessedCompVerificationRecords").Tables[0];
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return new DataTable();
            }
        }

        public static string UpdateCompVerStatus(int iInstallationNo, int iCompIndex)
        {
            try
            {
                SqlParameter[] sqlparam = new SqlParameter[3];
                sqlparam[0] = new SqlParameter("@InstallationID", iInstallationNo);
                sqlparam[1] = new SqlParameter("@ComponentID", iCompIndex);
                SqlParameter param = new SqlParameter();
                param.DbType = DbType.Int32;
                param.Direction = ParameterDirection.Output;
                param.TypeName = "@Status";

                sqlparam[2] = param;

                SqlHelper.ExecuteNonQuery(DBBuilder.GetConnectionString(), "usp_UpdateComponentVerificationData", sqlparam);

                return sqlparam[2].Value.ToString();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return string.Empty;
            }
        }

        public static DataTable GetCVInstallationDetails(int iType)
        {
            try
            {
                SqlParameter[] sqlparam = new SqlParameter[1];
                sqlparam[0] = new SqlParameter("@Type", iType);

                return SqlHelper.ExecuteDataset(DBBuilder.GetConnectionString(), CommandType.StoredProcedure, "rsp_GetCVInstallationDetails", sqlparam).Tables[0];
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return new DataTable();
            }
        }

        public static DataTable GetComponentTypes(int iInstallationID)
        {
            try
            {
                SqlParameter[] sqlparam = new SqlParameter[1];
                sqlparam[0] = new SqlParameter("@iInstallationID", iInstallationID);

                return SqlHelper.ExecuteDataset(DBBuilder.GetConnectionString(), CommandType.StoredProcedure, "rsp_GetComponentTypesForCompDetailsRequest", sqlparam).Tables[0];
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return new DataTable();
            }
        }

        public static string UpdateComponentRequestedStatus(string strSerialNo)
        {
            try
            {
                SqlParameter[] sqlparam = new SqlParameter[2];
                sqlparam[0] = new SqlParameter("@SerialNo", strSerialNo);
                SqlParameter param = new SqlParameter();
                param.DbType = DbType.String;
                param.Direction = ParameterDirection.Output;
                param.TypeName = "@Status";

                sqlparam[1] = param;

                SqlHelper.ExecuteDataset(DBBuilder.GetConnectionString(), CommandType.StoredProcedure, "usp_UpdateComponentRequestedData");

                return sqlparam[1].Value.ToString();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return string.Empty;
            }
        }

        public static string UpdateComponentData(int iInstallationNo, int iCompIndex)
        {
            try
            {
                SqlParameter[] sqlparam = new SqlParameter[3];
                sqlparam[0] = new SqlParameter("@InstallationID", iInstallationNo);
                sqlparam[1] = new SqlParameter("@ComponentID", iCompIndex);
                SqlParameter param = new SqlParameter();
                param.DbType = DbType.Int32;
                param.Direction = ParameterDirection.Output;
                param.TypeName = "@Status";

                sqlparam[2] = param;

                SqlHelper.ExecuteNonQuery(DBBuilder.GetConnectionString(), "usp_UpdateComponentVerificationData", sqlparam);

                return sqlparam[2].Value.ToString();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return string.Empty;
            }
        }

        public static DataTable GetUnprocessedMC300VerDetails()
        {
            try
            {
                return SqlHelper.ExecuteDataset(DBBuilder.GetConnectionString(), CommandType.StoredProcedure, "rsp_GetUnprocessedMC300VerificationRecords").Tables[0];
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return new DataTable();
            }
        }

        public static void UpdateDailyVerificationData(string strSerialNo)
        {
            try
            {
                SqlParameter[] sqlparam = new SqlParameter[1];
                sqlparam[0] = new SqlParameter("@SerialNo", strSerialNo);                              

                SqlHelper.ExecuteNonQuery(DBBuilder.GetConnectionString(), "usp_UpdateDailyComponentVerificationData", sqlparam);                
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                
            }
        }


        public static DataTable GetActiveMachinesForCompVerification()
        {
            try
            {
                return SqlHelper.ExecuteDataset(DBBuilder.GetConnectionString(), CommandType.StoredProcedure, "rsp_GetActiveMachinesForComponentVerification").Tables[0];
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return new DataTable();
            }
        }

        #endregion Component Verification

    }
}
