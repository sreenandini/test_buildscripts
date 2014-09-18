using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using BMC.Common.ExceptionManagement;
using BMC.Common.LogManagement;
using BMC.DataAccess;
using BMC.Transport.CashDeskOperatorEntity;
using Microsoft.Win32;
using BMC.Transport;
using BMC.Common.Utilities;

namespace BMC.DBInterface.CashDeskOperator
{
    public class DBBuilder_
    {
        #region Variable Declarations & Object Initializations

        private static string TicketConnection = string.Empty;
        private static string ConnectionString = string.Empty;

        DBConstants objDBconstant = new DBConstants();

        #endregion

        #region Common Functionalities

        public static DataTable GetUserRoles(string UserName, string UserPassword)
        {
            try
            {
                SqlParameter[] objParams = new SqlParameter[2];
                objParams[0] = new SqlParameter(DBConstants.CONST_PARAM_USER_NAME, UserName);
                objParams[1] = new SqlParameter(DBConstants.CONST_PARAM_USER_PASSWORD, UserPassword);
                return SqlHelper.ExecuteDataset(GetExchangeConnectionString(), DBConstants.CONST_SP_GET_USER_ROLE_PROC, objParams).Tables[0];
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
                    return new DataTable();
                }


            }
        }


        /// <summary>
        /// Get the installation details
        /// </summary>
        /// <returns>datatable of installation details</returns>
        public static DataTable GetInstallationDetails(int DatapakSerialNo, int InstallationNumber, bool ShouldIncludeVirtual, bool ShouldSortbyZone)
        {
            try
            {
                DataSet InstallationDetails = new DataSet();
                SqlParameter[] objParams = new SqlParameter[4];
                objParams[0] = new SqlParameter(DBConstants.CONST_PARAM_DATAPAK_SERIAL, DatapakSerialNo);
                objParams[1] = new SqlParameter(DBConstants.CONST_PARAM_INSTALLATION_NUMBER, InstallationNumber);
                objParams[2] = new SqlParameter(DBConstants.CONST_PARAM_INCLUDE_VIRTUAL, ShouldIncludeVirtual);
                objParams[3] = new SqlParameter(DBConstants.CONST_PARAM_SORT_BY_ZONE, ShouldSortbyZone);
                InstallationDetails= SqlHelper.ExecuteDataset(GetExchangeConnectionString(),DBConstants.CONST_SP_GET_INSTALLTION_DETAILS_PROC, objParams);
                if (InstallationDetails.Tables.Count > 0)
                {
                    return InstallationDetails.Tables[0];
                }
                else
                {
                    return new DataTable();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return new DataTable();
            }
        }

        private static string ExchangeConnectionString = string.Empty;
        /// <summary>
        /// Method to get the connection string.
        /// </summary>
        /// <param name="strTicketString"></param>
        /// <returns></returns>
        public static string GetExchangeConnectionString()
        {
            try
            {
                if(string.IsNullOrEmpty(ExchangeConnectionString))
                    ExchangeConnectionString = BMC.Common.Utilities.DatabaseHelper.GetExchangeConnectionString();
            }
            catch (Exception ex)
            {
                ExchangeConnectionString = "";
                if (ex.Message == "Connectionstring Not Found.")
                {
                    throw ex;
                }
                else
                {
                    ExceptionManager.Publish(ex);
                }
            }
            return ExchangeConnectionString;
        }

        /// <summary>
        /// To connect to ticket db 
        /// </summary>
        /// <param name="strConnect"></param>
        /// <returns type=string >strConnect</returns>
        public static string GetTicketingConnectionString(string Connect)
        {
            //try
            //{
            //    SqlParameter[] sqlparams = GetSettingParameterDB(DBConstants.CONST_SP_PARAM_TICKETCONNECTION);
            //    SqlHelper.ExecuteNonQuery(Connect, System.Data.CommandType.StoredProcedure, DBConstants.CONST_SP_GETSETTING, sqlparams);
            //    if (sqlparams[3].Value != null || sqlparams[3].Value.ToString() != string.Empty)
            //        TicketConnection = Convert.ToString(sqlparams[3].Value);
            //    else
            //        TicketConnection = string.Empty;
            //}

            //catch (Exception exTicketConnect)
            //{
            //    ExceptionManager.Publish(exTicketConnect);
            //    TicketConnection = string.Empty;
            //}

            return DatabaseHelper.GetTicketingConnectionString(); 
        }

        /// <summary>
        /// To set parameters for Get Setting SP
        /// </summary>
        /// <param name="strSettingName">string</param>
        /// <returns type=SqlParameter[] >sp_parames</returns>
        public static SqlParameter[] GetSettingParameterDB(string SettingName)
        {
            SqlParameter[] sp_parames = null;
            try
            {

                if (SettingName != null)
                {
                    sp_parames = new SqlParameter[4];

                    sp_parames[0] = new SqlParameter(DBConstants.CONST_SP_PARAM_SETTINGID, 0);
                    sp_parames[1] = new SqlParameter(DBConstants.CONST_SP_PARAM_SETTINGNAME, SettingName.Trim());
                    sp_parames[2] = new SqlParameter(DBConstants.CONST_SP_PARAM_SETTINGDEFAULT, string.Empty);

                    sp_parames[3] = new SqlParameter();
                    sp_parames[3].ParameterName = DBConstants.CONST_SP_PARAM_SETTINGVALUE;
                    sp_parames[3].Direction = ParameterDirection.Output;
                    sp_parames[3].Value = string.Empty;
                    sp_parames[3].SqlDbType = SqlDbType.VarChar;
                    sp_parames[3].Size = 100;

                    SqlParameter ReturnValue = new SqlParameter();
                    ReturnValue.ParameterName = DBConstants.CONST_SP_PARAM_RETURNVALUE;
                    ReturnValue.Direction = ParameterDirection.ReturnValue;
                    sp_parames[4] = ReturnValue;

                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

            return sp_parames;
        }

        public static string GetSettingValue(string SettingName)
        {
            SqlParameter[] sp_parames = null;
            try
            {

                if (SettingName != null)
                {
                    sp_parames = new SqlParameter[4];

                    sp_parames[0] = new SqlParameter(DBConstants.CONST_SP_PARAM_SETTINGID, 0);
                    sp_parames[1] = new SqlParameter(DBConstants.CONST_SP_PARAM_SETTINGNAME, SettingName.Trim());
                    sp_parames[2] = new SqlParameter(DBConstants.CONST_SP_PARAM_SETTINGDEFAULT, string.Empty);

                    sp_parames[3] = new SqlParameter();
                    sp_parames[3].ParameterName = DBConstants.CONST_SP_PARAM_SETTINGVALUE;
                    sp_parames[3].Direction = ParameterDirection.Output;
                    sp_parames[3].Value = string.Empty;
                    sp_parames[3].SqlDbType = SqlDbType.VarChar;
                    sp_parames[3].Size = 100;

                    SqlConnection objcon = new SqlConnection(GetExchangeConnectionString());
                    objcon.Open();
                    SqlCommand objcomd = new SqlCommand(DBConstants.CONST_SP_GETSETTING, objcon);
                    objcomd.CommandType = CommandType.StoredProcedure;
                    objcomd.Parameters.AddRange(sp_parames);
                    objcomd.ExecuteReader();
                    objcon.Close();
                    

                }
                return sp_parames[3].Value.ToString();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return string.Empty;
            }
            
        }

        #endregion Common Functionalities

        #region Issue Ticket

        /// <summary>
        /// To create new ticket for the amount entered
        /// </summary>
        /// <param name="_objCDOEntity"></param>
        /// <returns type=boolean>bTicketCreateReq</returns>
        public bool TicketCreateRequest(IssueTicketEntity issueTicketEntity)
        {

            bool IsTicketCreated = false;
            try
            {
                if (OpenConnection() == true)
                {

                    SqlParameter[] sqlparams = GetSpParametersCreateTicketStart(issueTicketEntity);
                                        
                    SqlHelper.ExecuteNonQuery(GetExchangeConnectionString(), System.Data.CommandType.StoredProcedure, DBConstants.CONST_SP_CREATETICKETSTART, sqlparams);

                    if (sqlparams[4].Value.ToString() != string.Empty)
                    {
                        issueTicketEntity.BarCode = Convert.ToString(sqlparams[4].Value);
                        IsTicketCreated = true;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return IsTicketCreated;

        }
        /// <summary>
        /// To Open  Connection with Ticketing DB and Exchange
        /// </summary>
        /// <param name=""></param>
        /// <returns ></returns>
        public bool OpenConnection()
        {
            string Connection = string.Empty;
            bool IsConnectionOpen = false;

            try
            {
                Connection = GetExchangeConnectionString();

                if (GetTicketingConnectionString(Connection) != string.Empty)
                    IsConnectionOpen = true;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

            return IsConnectionOpen;
        }
        /// <summary>
        /// To set parameters for ticket creation algorithm
        /// </summary>
        /// <param name="objCDOEntity"></param>
        /// <returns type=objectarray >sp_parames</returns>
        public SqlParameter[] GetSpParametersCreateTicketStart(IssueTicketEntity issueTicketEntity)
        {
            SqlParameter[] sp_parames = null;

            try
            {
                BGSGeneral.cGeneral objBGSGeneral = new BGSGeneral.cGeneral();
                if (issueTicketEntity != null)
                {
                    sp_parames = new SqlParameter[7];

                    SqlParameter param0 = new SqlParameter();
                    param0.ParameterName = DBConstants.CONST_SP_PARAM_INSTALLATIONID;
                    param0.Direction = ParameterDirection.Input;
                    param0.Value = 0;
                    sp_parames[0] = param0;

                    SqlParameter param1 = new SqlParameter();
                    param1.ParameterName = DBConstants.CONST_SP_PARAM_DATAPAKSERIALNUMBER;
                    param1.Direction = ParameterDirection.Input;
                    param1.Value = 0;
                    sp_parames[1] = param1;

                    SqlParameter param2 = new SqlParameter();
                    param2.ParameterName = DBConstants.CONST_SP_PARAM_TICKETVALUE;
                    param2.Direction = ParameterDirection.Input;
                    param2.Value = issueTicketEntity.lnglValue;
                    sp_parames[2] = param2;

                    SqlParameter param3 = new SqlParameter();
                    param3.ParameterName = DBConstants.CONST_SP_PARAM_SERVERNAME;
                    param3.Direction = ParameterDirection.Input;
                    param3.Value = objBGSGeneral.GetMachineName();
                    sp_parames[3] = param3;

                    sp_parames[4] = new SqlParameter();
                    sp_parames[4].ParameterName = DBConstants.CONST_SP_PARAM_RETURNTICKETNUMBER;
                    sp_parames[4].Direction = ParameterDirection.Output;
                    sp_parames[4].Value = string.Empty;
                    sp_parames[4].SqlDbType = SqlDbType.VarChar;
                    sp_parames[4].Size = 18;

                    SqlParameter param5 = new SqlParameter();
                    param5.ParameterName = DBConstants.CONST_SP_PARAM_TICKETTYPE;
                    param5.Direction = ParameterDirection.Input;
                    param5.Value = issueTicketEntity.Type;
                    sp_parames[5] = param5;

                    SqlParameter ReturnValue = new SqlParameter();
                    ReturnValue.ParameterName = DBConstants.CONST_SP_PARAM_RETURNVALUE;
                    ReturnValue.Direction = ParameterDirection.ReturnValue;
                    sp_parames[6] = ReturnValue;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return sp_parames;

        }       
        /// <summary>
        /// To Get Region Settings from Setting table in Exchange DB
        /// </summary>
        /// <param name=""></param>
        /// <returns type=string >strRegion</returns>
        [Obsolete]
        public string Region()
        {
            string Region = string.Empty;
           
            try

            {
                SqlParameter[] sqlparams = GetSettingParameterDB(DBConstants.CONST_SP_PARAM_REGION);             
                SqlHelper.ExecuteNonQuery(GetExchangeConnectionString(), CommandType.StoredProcedure, DBConstants.CONST_SP_GETSETTING, sqlparams);
                if (sqlparams[3].Value != null || sqlparams[3].Value.ToString() != string.Empty)
                {
                    Region = Convert.ToString(sqlparams[3].Value);
                }
                else
                {
                    Region = string.Empty;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

            return Region;
        }
        /// <summary>
        /// To Get Voucher Settings from Setting table in Exchange DB
        /// </summary>
        /// <param name=""></param>
        /// <returns type=string >strVoucherSite</returns>
        [Obsolete]
        public string VoucherSite()
        {
            string VoucherSite = string.Empty;

            try
            {
                SqlParameter[] sqlparams = GetSettingParameterDB(DBConstants.CONST_SP_PARAM_VOUCHERSITENAME);                
                SqlHelper.ExecuteNonQuery(GetExchangeConnectionString(), System.Data.CommandType.StoredProcedure, DBConstants.CONST_SP_GETSETTING, sqlparams);

                if (sqlparams[3].Value != null || sqlparams[3].Value.ToString() != string.Empty)
                    VoucherSite = Convert.ToString(sqlparams[3].Value);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

            return VoucherSite;
        }
        /// <summary>
        /// To Get Voucher Settings from Setting table in Exchange DB
        /// </summary>
        /// <param name=""></param>
        /// <returns type=string >strVoucherSite</returns>
        public bool TicketPrintConfirmed(IssueTicketEntity issueTicketEntity)
        {
            bool IsTicketPrinted = false;

            try
            {
                SqlParameter[] sqlparams = GetSpParametersCreateTicketComplete(issueTicketEntity);                
                SqlHelper.ExecuteNonQuery(GetExchangeConnectionString(), System.Data.CommandType.StoredProcedure, DBConstants.CONST_SP_CREATETICKETCOMPLETE, sqlparams);
                if (sqlparams[2].Value != DBNull.Value && Convert.ToString(sqlparams[2].Value) != string.Empty && sqlparams[2].Value != null)
                {
                    if (Convert.ToInt16(sqlparams[2].Value) == 0)
                        IsTicketPrinted = true;
                }
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

            return IsTicketPrinted;

        }
        /// <summary>
        /// To set parameters for completing the create ticket procedure
        /// </summary>
        /// <param name="objCDOEntity"></param>
        /// <returns type=objectarray >sp_parames</returns>
        public SqlParameter[] GetSpParametersCreateTicketComplete(IssueTicketEntity issueTicketEntity)
        {
            SqlParameter[] sp_parames = null;

            try
            {

                if (issueTicketEntity != null)
                {
                    sp_parames = new SqlParameter[3];
                    sp_parames[0] = new SqlParameter(DBConstants.CONST_SP_PARAM_BARCODE, issueTicketEntity.BarCode);

                    sp_parames[1] = new SqlParameter();
                    sp_parames[1].ParameterName = DBConstants.CONST_SP_PARAM_RETRESULT;
                    sp_parames[1].Direction = ParameterDirection.Output;
                    sp_parames[1].Value = 0;
                    sp_parames[1].SqlDbType = SqlDbType.Int;

                    SqlParameter ReturnValue = new SqlParameter();
                    ReturnValue.ParameterName = DBConstants.CONST_SP_PARAM_RETURNVALUE;
                    ReturnValue.Direction = ParameterDirection.ReturnValue;
                    sp_parames[2] = ReturnValue;
                }
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

            return sp_parames;
        }
        /// <summary>
        /// Save the ticket details 
        /// </summary>
        /// <param name="ObjCDOEntity"></param>
        /// <returns></returns>
        public bool SaveTicketIssueDetails(IssueTicketEntity issueTicketEntity)
        {
            bool IsTicketIssueDetailsSaved = false;

            try
            {
                SqlParameter[] sqlparams = GetSpParametersSaveTicketIssueDetails(issueTicketEntity);                
                SqlHelper.ExecuteNonQuery(GetExchangeConnectionString(), System.Data.CommandType.StoredProcedure, DBConstants.CONST_SP_INSERTTICKETISSUE, sqlparams);
                if (sqlparams[8].Value != DBNull.Value && Convert.ToString(sqlparams[8].Value) != string.Empty && sqlparams[8].Value != null)
                {
                    if (int.Parse(sqlparams[8].Value.ToString()) == 0)
                        IsTicketIssueDetailsSaved = true;
                }
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

            return IsTicketIssueDetailsSaved;

        }
        /// <summary>
        /// To set parameters for completing the create ticket procedure
        /// </summary>
        /// <param name="objCDOEntity"></param>
        /// <returns type=objectarray >sp_parames</returns>
        public SqlParameter[] GetSpParametersSaveTicketIssueDetails(IssueTicketEntity issueTicketEntity)
        {
            SqlParameter[] sp_parames = null;
            BGSGeneral.cGeneral objBGSGeneral = new BGSGeneral.cGeneral();
            try
            {
                if (issueTicketEntity != null)
                {
                    sp_parames = new SqlParameter[9];
                    sp_parames[0] = new SqlParameter(DBConstants.CONST_SP_PARAM_TICKETNUMBER, issueTicketEntity.BarCode);
                    sp_parames[1] = new SqlParameter(DBConstants.CONST_SP_PARAM_DATEPRINTED, issueTicketEntity.Date);
                    sp_parames[2] = new SqlParameter(DBConstants.CONST_SP_PARAM_WINDOWUSER, System.Environment.UserName.ToString());
                    sp_parames[3] = new SqlParameter(DBConstants.CONST_SP_PARAM_USERID, "userstring");
                    sp_parames[4] = new SqlParameter(DBConstants.CONST_SP_PARAM_MACHINENAME, objBGSGeneral.GetMachineName());
                    sp_parames[5] = new SqlParameter(DBConstants.CONST_SP_PARAM_TOTALVALUE, issueTicketEntity.dblValue);
                    sp_parames[6] = new SqlParameter(DBConstants.CONST_SP_PARAM_CASHTOTAL, issueTicketEntity.dblValue);
                    sp_parames[7] = new SqlParameter(DBConstants.CONST_SP_PARAM_CASHABLE, false);

                    SqlParameter ReturnValue = new SqlParameter();
                    ReturnValue.ParameterName = DBConstants.CONST_SP_PARAM_RETURNVALUE;
                    ReturnValue.Direction = ParameterDirection.ReturnValue;
                    sp_parames[8] = ReturnValue;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

            return sp_parames;
        }
        /// <summary>
        /// To Get EnableIssueReceipt from Setting table in Exchange DB
        /// </summary>
        /// <param name=""></param>
        /// <returns type=boolean >bCreateTicketIssueReceipt</returns>
        public bool CreateTicketIssueReceipt()
        {
            bool ShouldIssueReceipt = false;

            try
            {
                SqlParameter[] sqlparams = GetSettingParameterDB(DBConstants.CONST_SP_PARAM_ENABLEISSUERECEIPT);
                SqlHelper.ExecuteNonQuery(GetExchangeConnectionString(), System.Data.CommandType.StoredProcedure, DBConstants.CONST_SP_GETSETTING, sqlparams);

                if (sqlparams[3].Value != null && Convert.ToString(sqlparams[3].Value) != string.Empty && sqlparams[3].Value != DBNull.Value)
                    ShouldIssueReceipt = Convert.ToBoolean(sqlparams[3].Value);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

            return ShouldIssueReceipt;
        }
        /// <summary>
        /// To Get the ticket information, to display on receipt.
        /// </summary>
        /// <param name=""></param>
        /// <returns type=boolean >GetTicketInfo</returns>
        public bool GetTicketInfo(IssueTicketEntity issueTicketEntity, ref Voucher voucher)
        {
            bool IsTicketInfoRetrieved = false;
            DataSet GetTicketInfo = new DataSet();

            try
            {
                SqlParameter[] sqlparams = GetSpParametersGetTicketInfo(issueTicketEntity);

                if (OpenConnection() == true)
                {
                    GetTicketInfo = SqlHelper.ExecuteDataset(GetExchangeConnectionString(), DBConstants.CONST_SP_GETMACHINEDETAILSFROMASSET, sqlparams);
                    if (GetTicketInfo.Tables[0].Rows.Count > 0)
                    {
                        voucher.PrintDevice = GetTicketInfo.Tables[0].Rows[0][DBConstants.CONST_SP_RESULT_PRINTDEVICE].ToString();
                        voucher.PayDevice = GetTicketInfo.Tables[0].Rows[0][DBConstants.CONST_SP_RESULT_PAYDEVICE].ToString();
                        voucher.SBarCode = GetTicketInfo.Tables[0].Rows[0][DBConstants.CONST_SP_RESULT_BARCODE].ToString();
                        voucher.Amount = int.Parse((GetTicketInfo.Tables[0].Rows[0][DBConstants.CONST_SP_RESULT_AMOUNT]).ToString());
                        voucher.VoucherStatus = GetTicketInfo.Tables[0].Rows[0][DBConstants.CONST_SP_RESULT_VOUCHERSTATUS].ToString();
                        voucher.Datepaid = Convert.ToDateTime(GetTicketInfo.Tables[0].Rows[0][DBConstants.CONST_SP_RESULT_DATEPAID]);
                        voucher.DatePrinted = Convert.ToDateTime(GetTicketInfo.Tables[0].Rows[0][DBConstants.CONST_SP_RESULT_DATEPRINTED]);
                        voucher.DeviceType = GetTicketInfo.Tables[0].Rows[0][DBConstants.CONST_SP_RESULT_DEVICETYPE].ToString();
                        IsTicketInfoRetrieved = true;
                    }
                }

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

            return IsTicketInfoRetrieved;

        }
        /// <summary>
        /// To Get the ticket information, to display on receipt.
        /// </summary>
        /// <param name=""></param>
        /// <returns type=boolean >GetTicketInfo</returns>
        public bool GetMachineDetailsFromAsset(ref Voucher voucher)
        {
            bool IsMachineDetailsRetrieved = false;
            DataSet dsGetMachineDetailsFromAsset = new DataSet();
            try
            {
                SqlParameter[] sqlparams = new SqlParameter[1];
                sqlparams[0] = new SqlParameter(DBConstants.CONST_SP_PARAM_MACHINENAME, voucher.PrintDevice);

                dsGetMachineDetailsFromAsset = SqlHelper.ExecuteDataset(GetExchangeConnectionString(), DBConstants.CONST_SP_GETMACHINEDETAILSFROMASSET, sqlparams);

                if (dsGetMachineDetailsFromAsset.Tables[0].Rows.Count > 0)
                {
                    voucher.BarPositionName = dsGetMachineDetailsFromAsset.Tables[0].Rows[0][DBConstants.CONST_SP_RESULT_BARPOSITIONNAME].ToString();
                    voucher.InstallationNo = int.Parse((dsGetMachineDetailsFromAsset.Tables[0].Rows[0][DBConstants.CONST_SP_RESULT_INSTALLATIONNUMBER]).ToString());
                    voucher.StockNo = dsGetMachineDetailsFromAsset.Tables[0].Rows[0][DBConstants.CONST_SP_RESULT_STOCKNUMBER].ToString();
                    voucher.PrintDeviceName = dsGetMachineDetailsFromAsset.Tables[0].Rows[0][DBConstants.CONST_SP_RESULT_MACHINENAME].ToString();
                    voucher.PrintDeviceSerial = voucher.PrintDevice;
                    IsMachineDetailsRetrieved = true;
                }
                else
                {
                    voucher.PrintDeviceName = dsGetMachineDetailsFromAsset.Tables[0].Rows[0][DBConstants.CONST_SP_RESULT_PRINTDEVICE].ToString();
                    IsMachineDetailsRetrieved = false;
                }
                
            }
            catch (Exception ex)
            {
                IsMachineDetailsRetrieved = false;
                ExceptionManager.Publish(ex);
            }
            return IsMachineDetailsRetrieved;

        }
        /// <summary>
        /// To set parameters for completing the create ticket procedure
        /// </summary>
        /// <param name="objCDOEntity"></param>
        /// <returns type=objectarray >sp_parames</returns>
        public SqlParameter[] GetSpParametersGetTicketInfo(IssueTicketEntity issueTicketEntity)
        {
            SqlParameter[] sp_parames = null;

            try
            {
                if (issueTicketEntity != null)
                {
                    sp_parames = new SqlParameter[4];
                    sp_parames[0] = new SqlParameter(DBConstants.CONST_SP_PARAM_STARTDATE, DateTime.Now.AddDays(-5).ToString("dd mmm yyyy hh:nn:ss"));
                    sp_parames[1] = new SqlParameter(DBConstants.CONST_SP_PARAM_ENDDATE, DateTime.Now.AddDays(5).ToString("dd mmm yyyy hh:nn:ss"));
                    sp_parames[2] = new SqlParameter(DBConstants.CONST_SP_PARAM_TYPE, "A");
                    sp_parames[3] = new SqlParameter(DBConstants.CONST_SP_PARAM_BARCODE, issueTicketEntity.BarCode);

                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return sp_parames;
        }
        /// <summary>
        /// To set parameters for completing the create ticket procedure
        /// </summary>
        /// <param name="objCDOEntity"></param>
        /// <returns type=objectarray >sp_parames</returns>
        public SqlParameter[] GetSpParametersGetMachineDetailsFromAsset(string MachineName)
        {
            SqlParameter[] sp_parames = null;
            try
            {
                if (MachineName != null || MachineName != string.Empty)
                {
                    sp_parames = new SqlParameter[1];
                    sp_parames[0] = new SqlParameter(DBConstants.CONST_SP_PARAM_MACHINENAME, MachineName);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return sp_parames;
        }
        #endregion Issue Ticket

        #region Redeem Ticket Online

        /// <summary>
        /// Returns the currency symbol.
        /// </summary>
        /// <returns></returns>
        public DataSet GetTicketHistory(RTOnlineWageredDropDetail WagDrop)
        {
            DataSet TicketHistory = new DataSet();

            try
            {

                DataBaseServiceHandler.ConnectionString = GetExchangeConnectionString();
                DataBaseServiceHandler.Fill(QueryType.Procedure, DBConstants.CONST_RSP_GETTICKETHISTORY, TicketHistory, 
                    DataBaseServiceHandler.AddParameter<string>(DBConstants.CONST_PARAM_VOUCHER, DbType.String, WagDrop.TicketString));
            }
            catch (Exception exTicketHistory)
            {
                ExceptionManager.Publish(exTicketHistory);
                TicketHistory = null;
            }
            return TicketHistory;
        }

        /// <summary>
        /// Gets the TITO ticket information.
        /// </summary>
        /// <returns></returns>
        public bool GetTITOTicketInformation(RTOnlineReceiptDetail RTOReceiptDetail)
        {
            bool IsSuccess = false;

            try
            {
                if (RTOReceiptDetail.TicketString.Length == 18)
                {
                    if (GetTicketInfo(ref RTOReceiptDetail))
                        IsSuccess = true;
                }

                if (IsSuccess)
                {
                    if (GetMachineDetailsFromAsset(ref RTOReceiptDetail))
                        RTOReceiptDetail.PrintDevice = RTOReceiptDetail.DeviceName;
                }

                return IsSuccess;
            }
            catch (Exception exGetTITOTicketInformation)
            {
                ExceptionManager.Publish(exGetTITOTicketInformation);
                IsSuccess = false;
                return IsSuccess;
            }
        }

        /// <summary>
        /// Gets the ticket information.
        /// </summary>
        /// <returns></returns>
        public static bool GetTicketInfo(ref RTOnlineReceiptDetail RTOReceiptDetail)
        {
            bool IsSuccess = false;

            try
            {
                DataSet TicketInfo = new DataSet();
                DateTime date = new DateTime();
                string[] strArray = new string[1];
                strArray[0] = "TicketInfo";
                date = DateTime.Now;
                string strcon = "";
                TimeSpan tNegSpan = new TimeSpan(-5);
                TimeSpan tPosSpan = new TimeSpan(5);

                SqlParameter[] objParams = new SqlParameter[4];
                objParams[0] = new SqlParameter(DBConstants.CONST_PARAM_STARTDATE, date.Add(tNegSpan));//Need it in this format --> "dd mmm yyyy hh:nn:ss" Renjish
                objParams[1] = new SqlParameter(DBConstants.CONST_PARAM_ENDDATE, date.Add(tPosSpan));//Need it in this format --> "dd mmm yyyy hh:nn:ss" Renjish
                objParams[2] = new SqlParameter(DBConstants.CONST_PARAM_TYPE, "A");
                objParams[3] = new SqlParameter(DBConstants.CONST_PARAM_BARCODE, RTOReceiptDetail.TicketString);

                SqlHelper.FillDataset(GetTicketingConnectionString(strcon), DBConstants.CONST_RSP_BGS_VOUCHERINFORMATION, TicketInfo, strArray, objParams);

                if (TicketInfo.Tables.Count > 0)
                {
                    if (TicketInfo.Tables["TicketInfo"].Rows.Count > 0)
                    {
                        RTOReceiptDetail.PrintDevice = TicketInfo.Tables["TicketInfo"].Rows[0]["PrintDevice"].ToString();
                        RTOReceiptDetail.DatePrinted = Convert.ToDateTime(TicketInfo.Tables["TicketInfo"].Rows[0]["dtPrinted"]);
                        IsSuccess = true;
                    }
                    else
                        IsSuccess = false;
                }
                else
                    IsSuccess = false;

                return IsSuccess;
            }
            catch (Exception exGetTITOTicketInformation)
            {
                ExceptionManager.Publish(exGetTITOTicketInformation);
                IsSuccess = false;
                return IsSuccess;
            }
        }

        /// <summary>
        /// Gets the machine details from Asset.
        /// </summary>
        /// <returns></returns>
        public static bool GetMachineDetailsFromAsset(ref RTOnlineReceiptDetail RTOReceiptDetail)
        {
            bool IsSuccess = false;

            try
            {
                DataSet MachineDetails = new DataSet();
                DateTime date = new DateTime();
                string[] strArray = new string[1];
                strArray[0] = "MachineDetails";
                date = DateTime.Now;
                string strcon = "";

                SqlParameter[] objParams = new SqlParameter[2];
                objParams[0] = new SqlParameter(DBConstants.CONST_PARAM_MACHINE, RTOReceiptDetail.PrintDevice);

                SqlHelper.FillDataset(GetTicketingConnectionString(strcon), DBConstants.CONST_RSP_GETMACHINEDETAILSFROMASSET, MachineDetails, strArray, objParams);

                if (MachineDetails.Tables.Count > 0)
                {
                    if (MachineDetails.Tables["MachineDetails"].Rows.Count > 0)
                    {
                        RTOReceiptDetail.MachineClassName = MachineDetails.Tables["MachineDetails"].Rows[0]["Name"].ToString();
                        RTOReceiptDetail.DeviceBarPosition = MachineDetails.Tables["MachineDetails"].Rows[0]["Bar_Pos_Name"].ToString();
                        IsSuccess = true;
                    }
                    else
                        IsSuccess = false;
                }
                else
                    IsSuccess = false;

                return IsSuccess;
            }
            catch (Exception exGetMachineDetailsFromAsset)
            {
                ExceptionManager.Publish(exGetMachineDetailsFromAsset);
                IsSuccess = false;
                return IsSuccess;
            }
        }

        /// <summary>
        /// Gets the machine details from TBR Payout.
        /// </summary>
        /// <returns></returns>
        public bool GetMachineDetailsViaTBRPayout(ref RTOnlineReceiptDetail RTOReceiptDetail)
        {
            bool IsSuccess = false;

            try
            {
                DataSet MachineDetails = new DataSet();
                DateTime date = new DateTime();
                string[] strArray = new string[1];
                strArray[0] = "MachineDetails";
                date = DateTime.Now;
                string strcon = "";

                SqlParameter[] objParams = new SqlParameter[2];
                objParams[0] = new SqlParameter(DBConstants.CONST_PARAM_PAYOUT, RTOReceiptDetail.Payout);

                SqlHelper.FillDataset(GetTicketingConnectionString(strcon), DBConstants.CONST_RSP_GETMACHINEDETAILSVIATBRPAYOUT, MachineDetails, strArray, objParams);

                if (MachineDetails.Tables.Count > 0)
                {
                    if (MachineDetails.Tables["MachineDetails"].Rows.Count > 0)
                    {
                        RTOReceiptDetail.DatePrinted = Convert.ToDateTime(MachineDetails.Tables["MachineDetails"].Rows[0]["TBR_Payout_Print_Time"].ToString());
                        RTOReceiptDetail.PrintDevice = "";
                        RTOReceiptDetail.MachineClassName = MachineDetails.Tables["MachineDetails"].Rows[0]["Name"].ToString();
                        RTOReceiptDetail.DeviceBarPosition = MachineDetails.Tables["MachineDetails"].Rows[0]["Bar_Pos_Name"].ToString();
                    }
                    else
                        IsSuccess = false;
                }
                else
                    IsSuccess = false;

                return IsSuccess;
            }
            catch (Exception exGetMachineDetailsFromAsset)
            {
                ExceptionManager.Publish(exGetMachineDetailsFromAsset);
                IsSuccess = false;
                return IsSuccess;
            }
        }


        /// <summary>
        /// Checks if offline ticket exists.
        /// </summary>
        /// <returns></returns>
        public bool DoesOfflineTicketExist(ref RTOnlineTicketDetail RTOTicketDetail)
        {
            bool IsSuccess = false;

            try
            {
                DataSet OfflineTicketDetails = new DataSet();
                DateTime date = new DateTime();
                date = DateTime.Now;
                
                SqlParameter[] objParams = new SqlParameter[3];
                objParams[0] = new SqlParameter(DBConstants.CONST_PARAM_sTICKET_NUMBER, RTOTicketDetail.TicketString);
                SqlParameter ReturnValue = new SqlParameter();
                ReturnValue.ParameterName = DBConstants.CONST_SP_PARAM_RETURNVALUE;
                ReturnValue.Direction = ParameterDirection.ReturnValue;
                objParams[1] = ReturnValue;

                SqlHelper.ExecuteNonQuery(GetExchangeConnectionString(), System.Data.CommandType.StoredProcedure, DBConstants.CONST_RSP_DOESOFFLINETICKETEXIST, objParams);

                if (objParams[1].Value.ToString() == "1")
                    IsSuccess = true;
                else
                    IsSuccess = false;

                return IsSuccess;
            }
            catch (Exception exDoesOfflineTicketExist)
            {
                ExceptionManager.Publish(exDoesOfflineTicketExist);
                IsSuccess = false;
                return IsSuccess;
            }
        }

        /// <summary>
        /// Gets the exception details.
        /// </summary>
        /// <returns></returns>
        public bool GetExceptionDetails(ref RTOnlineTicketDetail RTOTicketDetail)
        {
            bool bSuccess = false;
            string Currency = "£";

            if (Settings.Region.ToUpper() == "US")
                Currency = "$";

            try
            {
                DataSet ExceptionDetails = new DataSet();
                DateTime date = new DateTime();
                string[] strArray = new string[1];
                strArray[0] = "ExceptionDetails";
                date = DateTime.Now;

                SqlParameter[] objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter(DBConstants.CONST_PARAM_BARCODE, RTOTicketDetail.TicketString);

                SqlHelper.FillDataset(GetExchangeConnectionString(), DBConstants.CONST_RSP_GETEXCEPTIONDETAILS, ExceptionDetails, strArray, objParams);

                if (ExceptionDetails.Tables.Count > 0)
                {
                    if (ExceptionDetails.Tables["ExceptionDetails"].Rows.Count > 0)
                    {
                        RTOTicketDetail.RedeemedMachine = ExceptionDetails.Tables["ExceptionDetails"].Rows[0]["Bar_pos_name"].ToString();
                        RTOTicketDetail.RedeemedDevice = ExceptionDetails.Tables["ExceptionDetails"].Rows[0]["TE_Workstation"].ToString();
                        RTOTicketDetail.RedeemedDate = ExceptionDetails.Tables["ExceptionDetails"].Rows[0]["TE_Date"].ToString();
                        RTOTicketDetail.RedeemedAmount = Currency + " " + (Convert.ToDouble(ExceptionDetails.Tables["ExceptionDetails"].Rows[0]["te_value"]) / 100).ToString("N");
                    }
                    else
                        bSuccess = false;
                }
                else
                    bSuccess = false;

                return bSuccess;
            }
            catch (Exception exGetMachineDetailsFromAsset)
            {
                ExceptionManager.Publish(exGetMachineDetailsFromAsset);
                bSuccess = false;
                return bSuccess;
            }
        }

        /// <summary>
        /// Method to check if ticket is valid and to return true/false.
        /// </summary>
        /// <returns></returns>
        [Obsolete]
        public static string GetCurrencySymbol()
        {
            string CurrencySymbol = "£";
            try
            {
                SqlParameter[] objSQLParams = new SqlParameter[5];
                objSQLParams = GetSettingParameterDB(DBConstants.CONST_SP_PARAM_REGION);
                SqlHelper.ExecuteNonQuery(ConnectionString, System.Data.CommandType.StoredProcedure, DBConstants.CONST_SP_GETSETTING, objSQLParams);

                if (objSQLParams[3].Value.ToString().ToLower() == "us")
                    CurrencySymbol = "$";
                else
                    CurrencySymbol = "£";

                return CurrencySymbol;
            }
            catch (Exception exGetCurrencySymbol)
            {
                ExceptionManager.Publish(exGetCurrencySymbol);
                return CurrencySymbol;
            }
        }

        /// <summary>
        /// Method to check if laundering option is enabled.
        /// </summary>
        /// <returns></returns>
        [Obsolete]
        public static bool CheckLaunderingEnabled()
        {
            bool bLaunderingEnabled = false;
            try
            {
                SqlParameter[] objSQLParams = new SqlParameter[5];

                objSQLParams = GetSettingParameterDB(DBConstants.CONST_SP_PARAM_ENABLELAUNDERING);
                SqlHelper.ExecuteNonQuery(GetExchangeConnectionString(), System.Data.CommandType.StoredProcedure, DBConstants.CONST_SP_GETSETTING, objSQLParams);

                if (objSQLParams[3].Value.ToString().ToLower() == "true")
                    bLaunderingEnabled = true;
                else
                    bLaunderingEnabled = false;
            }
            catch (Exception exCheckLaunderingEnabled)
            {
                ExceptionManager.Publish(exCheckLaunderingEnabled);                
            }
            return bLaunderingEnabled;
        }

        /// <summary>
        /// Method to check if allow offline redeem option is enabled.
        /// </summary>
        /// <returns></returns>
        [Obsolete]
        public static bool CheckOfflineRedeemEnabled()
        {
            bool bOfflineRedeemEnabled = false;
            try
            {
                SqlParameter[] objSQLParams = new SqlParameter[5];

                objSQLParams = GetSettingParameterDB(DBConstants.CONST_SP_PARAM_ALLOWOFFLINEREDEEM);             
                SqlHelper.ExecuteNonQuery(GetExchangeConnectionString(), System.Data.CommandType.StoredProcedure, DBConstants.CONST_SP_GETSETTING, objSQLParams);

                if (objSQLParams[3].Value.ToString().ToLower() == "true")
                {
                    bOfflineRedeemEnabled = true;
                }
                else
                {
                    bOfflineRedeemEnabled = false;
                }

                return bOfflineRedeemEnabled;
            }
            catch (Exception exCheckOfflineRedeemEnabled)
            {
                ExceptionManager.Publish(exCheckOfflineRedeemEnabled);
                bOfflineRedeemEnabled = false;
                return bOfflineRedeemEnabled;
            }
        }

        /// <summary>
        /// Method to check if laundering option is enabled.
        /// </summary>
        /// <returns></returns>
        [Obsolete]
        public static bool CheckTicketValidateVoucherEnabled()
        {
            bool TicketValidateVoucherEnabled = false;
            try
            {
                SqlParameter[] objSQLParams = new SqlParameter[5];

                objSQLParams = GetSettingParameterDB(DBConstants.CONST_SP_PARAM_TICKETVALIDATEENABLEVOUCHER);
                SqlHelper.ExecuteNonQuery(GetExchangeConnectionString(), System.Data.CommandType.StoredProcedure, DBConstants.CONST_SP_GETSETTING, objSQLParams);

                if (objSQLParams[3].Value.ToString().ToLower() == "true")
                {
                    TicketValidateVoucherEnabled = true;
                }
                else
                {
                    TicketValidateVoucherEnabled = false;
                }

                return TicketValidateVoucherEnabled;
            }
            catch (Exception exTicketValidateVoucherEnabled)
            {
                ExceptionManager.Publish(exTicketValidateVoucherEnabled);
                TicketValidateVoucherEnabled = false;
                return TicketValidateVoucherEnabled;
            }
        }

        /// <summary>
        /// Returns the Credits Wagered to Cash In value.
        /// </summary>
        /// <returns></returns>
        public double GetAmberCreditsWageredtoCashIn()
        {
            double WageredAmount = 40;
            try
            {
                SqlParameter[] objSQLParams = new SqlParameter[5];

                objSQLParams = GetSettingParameterDB(DBConstants.CONST_SP_PARAM_AMBERCREDITSWAGEREDTOCASHIN);
                SqlHelper.ExecuteNonQuery(GetExchangeConnectionString(), System.Data.CommandType.StoredProcedure, DBConstants.CONST_SP_GETSETTING, objSQLParams);
                WageredAmount = Convert.ToDouble(objSQLParams[3].Value);

                return WageredAmount;
            }
            catch (Exception exmberCreditsWageredtoCashIn)
            {
                ExceptionManager.Publish(exmberCreditsWageredtoCashIn);
                WageredAmount = 40;
                return WageredAmount;
            }
        }

        #endregion Redeem Ticket Online

        #region Redeem Offline Tickets related
        /// <summary>
        /// Get the list of available installation
        /// </summary>
        /// <returns>datatable of list of installation</returns>
        /// Method Revision History
        ///
        /// Author             Date              Description
        /// ---------------------------------------------------
        /// Madhusudhanan      20-Oct-2008      Intial Version 
        public static DataTable GetInstallationList()
        {
            try
            {
                DataSet dsInstallationList = new DataSet();

                SqlHelper.FillDataset(GetExchangeConnectionString(), CommandType.StoredProcedure, DBConstants.CONST_SP_GET_INSTALLTION_LIST_PROC, dsInstallationList, new string[] { "InstallationList" });
                if (dsInstallationList.Tables.Count > 0)
                {
                    return dsInstallationList.Tables[0];
                }
                else
                {
                    return new DataTable();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return new DataTable();
            }
        }


        /// <summary>
        /// saves the ticket deatils to DB
        /// </summary>
        /// <returns>success or failure</returns>
        /// Method Revision History
        ///
        /// Author             Date              Description
        /// ---------------------------------------------------
        /// Madhusudhanan      20-Oct-2008      Intial Version 
        public static bool SaveOfflineTicketDetails(BMC.Transport.CashDeskOperatorEntity.OfflineTicket objOfflineTicket)
        {
            try
            {
                SqlParameter[] objParams = new SqlParameter[8];
                
                objParams[0] = new SqlParameter(DBConstants.CONST_PARAM_iINSTALLATION_NUMBER, objOfflineTicket.InstallationNumber);
                objParams[1] = new SqlParameter(DBConstants.CONST_PARAM_sTICKET_NUMBER, objOfflineTicket.TicketBarCode);
                objParams[2] = new SqlParameter(DBConstants.CONST_PARAM_dDate, objOfflineTicket.RedeemedDateTime);
                objParams[3] = new SqlParameter(DBConstants.CONST_PARAM_sWINDOWS_USER, objOfflineTicket.UserName);
                objParams[4] = new SqlParameter(DBConstants.CONST_PARAM_iUSERID, objOfflineTicket.UserID);
                objParams[5] = new SqlParameter(DBConstants.CONST_PARAM_sWORKSTATION, objOfflineTicket.MachineName);
                objParams[6] = new SqlParameter(DBConstants.CONST_PARAM_sCUSTOMER_DETAILS, objOfflineTicket.CustomerDetails);
                objParams[7] = new SqlParameter(DBConstants.CONST_PARAM_iVALUE, objOfflineTicket.PayableValue);
                 
                object objRetValue = SqlHelper.ExecuteScalar(GetExchangeConnectionString(), CommandType.StoredProcedure, DBConstants.CONST_SP_SAVE_OFFLINE_TICKET_PROC,objParams);
                if (Convert.ToInt32(objRetValue) == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return false;
            }
        }


        #endregion

        #region Shortpay/Handpay related
        /// <summary>
        /// Save Handpay, short pay details in Treasury Table
        /// </summary>
        /// <param name="objTreasury">Treasuries</param>
        /// <returns >int</returns>
        ///  /// Method Revision History
        ///
        /// Author             Date              Description
        /// ---------------------------------------------------
        /// Anuradha       05-Nov-2008    Intial Version 
        public static int SaveTreasuryDetails(Treasury objTreasury)
        {
            try
            {
                SqlParameter[] ObjParams = new SqlParameter[20];
                ObjParams[0] = new SqlParameter(DBConstants.CONST_PARAM_INSTALLATION_NO, objTreasury.InstallationNumber);
                ObjParams[1] = new SqlParameter(DBConstants.CONST_PARAM_COLLECTION_NO, objTreasury.CollectionNumber);
                ObjParams[2] = new SqlParameter(DBConstants.CONST_PARAM_USER_ID, objTreasury.UserID);
                ObjParams[3] = new SqlParameter(DBConstants.CONST_PARAM_TREASURY_TYPE, objTreasury.TreasuryType);
                ObjParams[4] = new SqlParameter(DBConstants.CONST_PARAM_TREASURY_REASON, objTreasury.TreasuryReason);
                ObjParams[5] = new SqlParameter(DBConstants.CONST_PARAM_TREASURY_BREAKDOWN_2P, objTreasury.TreasuryBreakdown2p);
                ObjParams[6] = new SqlParameter(DBConstants.CONST_PARAM_TREASURY_BREAKDOWN_5P, objTreasury.TreasuryBreakdown5p);
                ObjParams[7] = new SqlParameter(DBConstants.CONST_PARAM_TREASURY_BREAKDOWN_10P, objTreasury.TreasuryBreakdown10p);
                ObjParams[8] = new SqlParameter(DBConstants.CONST_PARAM_TREASURY_BREAKDOWN_20P, objTreasury.TreasuryBreakdown20p);
                ObjParams[9] = new SqlParameter(DBConstants.CONST_PARAM_TREASURY_BREAKDOWN_50P, objTreasury.TreasuryBreakdown50p);
                ObjParams[10] = new SqlParameter(DBConstants.CONST_PARAM_TREASURY_BREAKDOWN_100P, objTreasury.TreasuryBreakdown100p);
                ObjParams[11] = new SqlParameter(DBConstants.CONST_PARAM_TREASURY_BREAKDOWN_200P, objTreasury.TreasuryBreakdown200p);
                ObjParams[12] = new SqlParameter(DBConstants.CONST_PARAM_TREASURY_AMOUNT, objTreasury.TreasuryAmount);
                ObjParams[13] = new SqlParameter(DBConstants.CONST_PARAM_TREASURY_ALLOCATED, objTreasury.TreasuryAllocated);
                ObjParams[14] = new SqlParameter(DBConstants.CONST_PARAM_TREASURY_MEMBERSHIP_NO, objTreasury.TreasuryMembershipNo);
                ObjParams[15] = new SqlParameter(DBConstants.CONST_PARAM_TREASURY_REASON_CODE, objTreasury.TreasuryReasonCode);
                ObjParams[16] = new SqlParameter(DBConstants.CONST_PARAM_TREASURY_ISSUER_USER_NO, objTreasury.TreasuryIssuerUserNo);
                ObjParams[17] = new SqlParameter(DBConstants.CONST_PARAM_TREASURY_TEMP, objTreasury.TreasuryTemp);
                ObjParams[18] = new SqlParameter(DBConstants.CONST_PARAM_TREASURY_FLOATISSUEDBY, objTreasury.TreasuryFloatIssuedBy);
                ObjParams[19] = new SqlParameter(DBConstants.CONST_PARAM_TREASURY_NUMBER, 0);
                ObjParams[19].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteNonQuery(GetExchangeConnectionString(), CommandType.StoredProcedure, DBConstants.CONST_SP_SAVE_TREASURY_PROC, ObjParams);
                return (int.Parse(ObjParams[19].Value.ToString()));
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return -1;
            }

        }

        public static DataTable GetTicketingExceptionTable(string strTicketNumber)
        {
            try
            {
                SqlParameter[] ObjParams = new SqlParameter[2];
                ObjParams[0] = new SqlParameter(DBConstants.CONST_PARAM_TE_TICKET_NUMBER, strTicketNumber);
                ObjParams[1] = new SqlParameter(DBConstants.CONST_PARAM_TE_ID, null);
                return SqlHelper.ExecuteDataset(GetExchangeConnectionString(), DBConstants.CONST_SP_GET_TICKETING_EXCEPTIONS_PROC, ObjParams).Tables[0];
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return new DataTable();
            }
        }

        public static DataTable GetFailureReasons()
        {
            try
            {
                return SqlHelper.ExecuteDataset(GetExchangeConnectionString(), CommandType.StoredProcedure, DBConstants.CONST_SP_GET_FAILURE_REASONS).Tables[0];

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return new DataTable();
            }
        }

        /// <summary>
        /// Update Ticket Final Status in Ticket Exception Table
        /// </summary>
        /// <param name="strTicketNumber">string</param>
        /// <returns >bool</returns>
        ///  /// Method Revision History
        ///
        /// Author             Date              Description
        /// ---------------------------------------------------
        /// Anuradha       05-Nov-2008    Intial Version 
        public static bool UpdateTicketException_FinalStatus(string strTicketNumber)
        {
            try
            {
                SqlParameter[] ObjParams = new SqlParameter[1];
                ObjParams[0] = new SqlParameter(DBConstants.CONST_PARAM_TE_TICKET_NUMBER, strTicketNumber);
                if (SqlHelper.ExecuteNonQuery(GetExchangeConnectionString(), DBConstants.CONST_SP_UPDATE_TICKETEXCEPTION, ObjParams) > -1)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return false;
            }
        }

        /// <summary>
        /// Update Ticket Exception Table
        /// </summary>
        /// <param name="objExpiredTreasury">VoidOrExpiredTreasury</param>
        /// <returns >int</returns>
        ///  /// Method Revision History
        ///
        /// Author             Date              Description
        /// ---------------------------------------------------
        /// Anuradha       05-Nov-2008    Intial Version 

        public static int UpdateVoidorExpiredTreasury(VoidOrExpiredTreasury objExpiredTreasury)
        {
            try
            {

                SqlParameter[] ObjParams = new SqlParameter[4];
                ObjParams[0] = new SqlParameter(DBConstants.CONST_PARAM_TRANSACTION_TYPE, objExpiredTreasury.TransactionType);
                ObjParams[1] = new SqlParameter(DBConstants.CONST_PARAM_TICKETNUMBERorID, objExpiredTreasury.TicketNumber);
                ObjParams[2] = new SqlParameter(DBConstants.CONST_PARAM_TREASURYREASON, objExpiredTreasury.TreasuryReason);
                ObjParams[3] = new SqlParameter("@OutVal", 0);
                ObjParams[3].Direction = ParameterDirection.Output;
                return SqlHelper.ExecuteNonQuery(GetExchangeConnectionString(), DBConstants.CONST_SP_UPDATE_VOID_EXPIRED_TREASURY_PROC, ObjParams);

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return -1;
            }
        }
        public static int GetMaxTreasuryID()
        {
            try
            {
                return Convert.ToInt32(SqlHelper.ExecuteScalar(GetExchangeConnectionString(), CommandType.StoredProcedure, DBConstants.CONST_SP_GET_MAX_TREASURYID));

            }
            catch (Exception ex)
            {

                ExceptionManager.Publish(ex);
                return -1;
            }
        }

        public static int InsertException(TicketException objException)
        {
            try
            {
                SqlParameter[] ObjParams = new SqlParameter[5];
                ObjParams[0] = new SqlParameter(DBConstants.CONST_PARAM_INSTALLATION_ID, objException.InstallationNumber);
                ObjParams[1] = new SqlParameter(DBConstants.CONST_PARAM_EXCEPTION_TYPE, objException.ExceptionType);
                ObjParams[2] = new SqlParameter(DBConstants.CONST_PARAM_EXCEPTION_DETAILS, objException.ExceptionDetails);
                ObjParams[3] = new SqlParameter(DBConstants.CONST_PARAM_EXCEPTION_REFERENCE, objException.Reference);
                ObjParams[4] = new SqlParameter(DBConstants.CONST_PARAM_EXCEPTION_USER, objException.User);
                return SqlHelper.ExecuteNonQuery(GetExchangeConnectionString(), DBConstants.CONST_SP_INSERT_EXCEPTION_PROC, ObjParams);

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return -1;
            }
        }

        public static int UpdateTicketException(Int32 iID, string strTicketNo, string strValue)
        {
            try
            {
                SqlParameter[] ObjParams = new SqlParameter[3];
                ObjParams[0] = new SqlParameter(DBConstants.CONST_PARAM_TE_TICKET_NUMBER, strTicketNo);
                ObjParams[1] = new SqlParameter(DBConstants.CONST_PARAM_TE_ID, iID);
                ObjParams[2] = new SqlParameter(DBConstants.CONST_PARAM_TE_STATUS, strValue);

                return SqlHelper.ExecuteNonQuery(GetExchangeConnectionString(), DBConstants.CONST_SP_UPDATE_TICKET_EXCEPTION_PROC, ObjParams);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return -1;
            }
        }

        /// <summary>
        /// Void Treasury Entries by implementing Negative Transaction.
        /// </summary>
        /// <param name="objNegativeTrans">VoidTreasuryNegativeTransaction</param>
        /// <returns >int</returns>
        ///  /// Method Revision History
        ///
        /// Author             Date              Description
        /// ---------------------------------------------------
        /// Anuradha       05-Nov-2008    Intial Version 
        public static int VoidTreasuryNegativeTransaction(VoidTreasuryNegativeTransaction objNegativeTrans)
        {
            try
            {
                SqlParameter[] ObjParams = new SqlParameter[5];
                ObjParams[0] = new SqlParameter(DBConstants.CONST_PARAM_TREASURY_NUMBER, objNegativeTrans.TreasuryNumber);
                ObjParams[1] = new SqlParameter(DBConstants.CONST_PARAM_TREASURY_DATE, objNegativeTrans.TreasuryDate);
                ObjParams[2] = new SqlParameter(DBConstants.CONST_PARAM_TREASURY_TIME, objNegativeTrans.TreasuryTime);
                ObjParams[3] = new SqlParameter(DBConstants.CONST_PARAM_USERNO, objNegativeTrans.UserID);
                ObjParams[4] = new SqlParameter("@OutVal", 0);
                ObjParams[4].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteNonQuery(GetExchangeConnectionString(), CommandType.StoredProcedure, DBConstants.CONST_SP_VOIDTREASURY_CREATENEGTRAN, ObjParams);

                return (int.Parse(ObjParams[4].Value.ToString()));
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return 1;
            }
        }

        #endregion

        #region GetUnProcessedHandpays
        /// <summary>
        /// Get Unprocessed handpays
        /// </summary>
        /// <param name=""></param>
        /// <returns >Datatable</returns>
        ///  /// Method Revision History
        ///
        /// Author             Date              Description
        /// ---------------------------------------------------
        /// Anuradha       05-Nov-2008    Intial Version 
        public static DataTable GetUnProcessedHandPays()
        {
            string strproc = "GetUnprocessedhandpays";
            DataTable dtHandPays = null;
            try
            {
                //Get the list of unprocessed handpays.
                dtHandPays = SqlHelper.ExecuteDataset(GetExchangeConnectionString(), CommandType.StoredProcedure, DBConstants.CONST_SP_RSP_GETHANDPAYS).Tables[0];

                return dtHandPays;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog(strproc + ex.Message, LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
                return null;
            }
        }

        public static DataTable GetUnProcessedHandPays(int InstallationNo)
        {
            string strproc = "GetUnprocessedhandpays";
            DataTable dtHandPays = null;
            try
            {
                //Get the list of unprocessed handpays.

                SqlParameter[] objSQL = new SqlParameter[1];

                SqlParameter objParam1 = new SqlParameter();
                objParam1.ParameterName = "Installation_no";
                objParam1.Direction = ParameterDirection.Input;
                objParam1.SqlDbType = SqlDbType.Int;
                objParam1.Value = InstallationNo;
                objSQL[0] = objParam1;
                dtHandPays = SqlHelper.ExecuteDataset(GetExchangeConnectionString(), CommandType.StoredProcedure, DBConstants.CONST_SP_RSP_GETHANDPAYS, objSQL).Tables[0];

                return dtHandPays;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog(strproc + ex.Message, LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
                return null;
            }
        }
        private static Int64 ToInteger64(object objValue, Int64 iDefaultValue)
        {
            try
            {
                return Convert.ToInt64(objValue);
            }
            catch
            {

                return iDefaultValue;
            }

        }

        #endregion

        #region Void Transaction Methods

        #region To Create a void transaction

        /// <summary>
        /// Method to create a record in the Treasury table for negative amount.
        /// </summary>
        /// <param name="objVoid"></param>
        /// <returns></returns>
        public static int VoidTreasuryNegGen(VoidTranCreate objVoid)
        {
            SqlParameter[] objSQL = new SqlParameter[5];

            SqlParameter objParam1 = new SqlParameter();
            objParam1.ParameterName = "TreasuryNo";
            objParam1.Direction = ParameterDirection.Input;
            objParam1.SqlDbType = SqlDbType.Int;
            objParam1.Value = objVoid.TreasuryID;
            objSQL[0] = objParam1;

            SqlParameter objParam2 = new SqlParameter();
            objParam2.ParameterName = "dDate";
            objParam2.Direction = ParameterDirection.Input;
            objParam2.SqlDbType = SqlDbType.VarChar;
            objParam2.Value = objVoid.Date;
            objSQL[1] = objParam2;

            SqlParameter objParam3 = new SqlParameter();
            objParam3.ParameterName = "dTime";
            objParam3.Direction = ParameterDirection.Input;
            objParam3.SqlDbType = SqlDbType.VarChar;
            objParam3.Value = objVoid.Time;
            objSQL[2] = objParam3;

            SqlParameter objParam4 = new SqlParameter();
            objParam4.ParameterName = "UserNo";
            objParam4.Direction = ParameterDirection.Input;
            objParam4.SqlDbType = SqlDbType.Int;
            objParam4.Value = objVoid.UserNo;
            objSQL[3] = objParam4;

            SqlParameter objParam5 = new SqlParameter();
            objParam5.ParameterName = "OutVal";
            objParam5.Direction = ParameterDirection.Output;
            objParam5.SqlDbType = SqlDbType.Int;
            objSQL[4] = objParam5;

            try
            {
                SqlHelper.ExecuteNonQuery(GetExchangeConnectionString(), CommandType.StoredProcedure, DBConstants.CONSTANT_USP_CREATEVOIDTREASURY, objSQL);
                return int.Parse(objSQL[4].Value.ToString());
            }
            catch (SqlException Ex)
            {
                ExceptionManager.Publish(Ex);
                return 99;
            }
        }
        #endregion

        #region Fill Void Transactions

        /// <summary>
        /// Method to fill records in the Void Transaction screen.
        /// </summary>
        /// <params></param>
        /// <returns></returns>
        /// Method Revision History
        ///
        /// Author             Date              Description
        /// ---------------------------------------------------
        /// Sudarsan S         16-Oct-2008      Intial Version 
        /// 

        public static DataSet FillVoidTransactionList()
        {
            DataSet objDSFill;

            try
            {
                objDSFill = SqlHelper.ExecuteDataset(GetExchangeConnectionString(), CommandType.StoredProcedure, DBConstants.CONSTANT_RSP_GETVOIDTRANSACTIONLIST);
                return objDSFill;
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
                return null;
            }
        }

        #endregion

        #endregion

        #region Get SiteDetails
        /// <summary>
        /// Method to get the Site details.
        /// </summary>
        /// <param name="objVoid"></param>
        /// <returns></returns>
        /// Method Revision History
        ///
        /// Author             Date              Description
        /// ---------------------------------------------------
        /// Sudarsan S         20-Oct-2008      Intial Version 
        /// 

        public static DataSet GetSiteDetails()
        {
            DataSet objDS = new DataSet();

            try
            {
                objDS = SqlHelper.ExecuteDataset(GetExchangeConnectionString(), CommandType.StoredProcedure, DBConstants.CONSTANT_RSP_RSP_GETSITEDETAILS);
                return objDS;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return null;
            }
        }

        #endregion

        #region Get Machine Details via Treasury
        /// <summary>
        /// Method to fetch the details from BP and Machine.
        /// </summary>
        /// <param name="objVoid"></param>
        /// <returns></returns>
        /// Method Revision History
        ///
        /// Author             Date              Description
        /// ---------------------------------------------------
        /// Sudarsan S         16-Oct-2008      Intial Version 
        /// 
        public static DataSet GetMachineDetailsTreasury(string strTreasuryNo)
        {
            SqlParameter[] objSQlParams = new SqlParameter[1];
            SqlParameter objSQL = new SqlParameter();
            objSQL.ParameterName = "Treasury_ID";
            objSQL.Direction = ParameterDirection.Input;
            objSQL.Value = strTreasuryNo;
            objSQlParams[0] = objSQL;

            try
            {
                return SqlHelper.ExecuteDataset(GetExchangeConnectionString(), CommandType.StoredProcedure, DBConstants.CONSTANT_RSP_GETMCDETAILSVIATREASURY, objSQlParams);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return null;
            }
        }

        #endregion

        #region Get Settings from the Setting Table
        /// <summary>
        /// Method to get the Site settings.
        /// </summary>
        /// <param name="objVoid"></param>
        /// <returns></returns>
        /// Method Revision History
        ///
        /// Author             Date              Description
        /// ---------------------------------------------------
        /// Sudarsan S         20-Oct-2008      Intial Version 
        /// 
        public static DataSet GetInitialSettings()
        {
            DataSet objDS = new DataSet();

            try
            {
                objDS = SqlHelper.ExecuteDataset(GetExchangeConnectionString(), CommandType.StoredProcedure, DBConstants.CONSTANT_RSP_GETINITIALSETTINGS);
                return objDS;
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
                return null;
            }
        }

        #endregion

        #region PlayerInfo
        /// <summary>
        /// Retrieve Player Information.
        /// </summary>
        /// <param name=""></param>
        /// <returns >CashDeskOperatorEntity.PlayerInfo</returns>
        ///  /// Method Revision History
        ///
        /// Author             Date              Description
        /// ---------------------------------------------------
        /// Anuradha       13-Nov-2008    Intial Version 
        /// 

        public static PlayerInformation[] PlayerInfo()
        {
            string strproc = "PlayerInfo";
            try
            {
                //Get player details
                return PlayerInformations.CreatePlayerTable();
            }
            catch (Exception ex)
            {
                LogManager.WriteLog(strproc + ex.Message, LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
                return null;
            }
        }
  
        /// <summary>
        /// Retrieve the setting value
        /// </summary>
        /// <param name="strConnect"></param>
        /// <returns>dictionary</returns>
        ///  /// Method Revision History
        ///
        /// Author             Date              Description
        /// ---------------------------------------------------
        /// Anuradha       28-jan-2009          Intial Version 
        /// 
        public Dictionary<string,string> GetCMPCredentials(string strConnect)
        {
            Dictionary<string, string> objDicCMPDetails = new Dictionary<string, string>();
            try
            {
                //Get the CMP Kiosk URL
                SqlParameter[] sqlparams = GetSettingParameterDB(DBConstants.CONSTANT_RSP_GETCMPKIOSKURL);
                string strCMPURL = ExecuteQuery(strConnect, sqlparams);
                if (!String.IsNullOrEmpty(strCMPURL))
                {
                    objDicCMPDetails.Add("CMPURL", strCMPURL);
                }

                //Get the CMP Application USERNAME
                SqlParameter[] sqlparams1 = GetSettingParameterDB(DBConstants.CONSTANT_RSP_GETCMPAPPUSER);
                string strCMPUSER = ExecuteQuery(strConnect, sqlparams1);
                if (!String.IsNullOrEmpty(strCMPUSER))
                {
                    objDicCMPDetails.Add("CMPUSER", strCMPUSER);
                }

                //Get the CMP Application Password
                SqlParameter[] sqlparams2 = GetSettingParameterDB(DBConstants.CONSTANT_RSP_GETCMPAPPPWD);
                string strCMPPWD = ExecuteQuery(strConnect, sqlparams2);
                if (!String.IsNullOrEmpty(strCMPPWD))
                {
                    objDicCMPDetails.Add("CMPPWD", strCMPPWD);
                }
            }


            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

            return objDicCMPDetails;
        }

        /// <summary>
        /// Get the settings for CMP Kiosk
        /// </summary>
        /// <param name="sqlparams"></param>
        /// <param name="strConnect"></param>
        /// <returns >string</returns>
        ///  /// Method Revision History
        ///
        /// Author             Date              Description
        /// ---------------------------------------------------
        /// Anuradha       28-Jan-2009   Intial Version 
        private static string ExecuteQuery(string strConnect,SqlParameter[] sqlparams)
        {
            string strReturnValue = string.Empty;
            try
            {
                SqlHelper.ExecuteNonQuery(strConnect, System.Data.CommandType.StoredProcedure, DBConstants.CONST_SP_GETSETTING, sqlparams);
                if (sqlparams[3].Value != null || sqlparams[3].Value.ToString() != string.Empty)
                {
                    strReturnValue = Convert.ToString(sqlparams[3].Value);
                }
                else
                {
                    strReturnValue = string.Empty;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

            return strReturnValue;
        }

        /// <summary>
        /// Check for prefix and suffix when swipping the card.
        /// </summary>
        /// <param name=""></param>
        /// <returns >boolean</returns>
        ///  /// Method Revision History
        ///
        /// Author             Date              Description
        /// ---------------------------------------------------
        /// Anuradha       13-Nov-2008          Intial Version 
        /// 
        public bool CheckForPrefixSuffixSetting()
        {
            string bHasSuffix = "";
            string bHasPrefix = "";
            bool bResult = false;
            RegistryKey regKeyConnectionString=null;
            try
            {
           
                //regKeyConnectionString = BMCRegistryHelper.GetRegLocalMachine().OpenSubKey("Software\\Honeyframe\\Cashmaster\\Exchange");
                //if (regKeyConnectionString != null)
                //{
                    bHasSuffix = BMCRegistryHelper.GetRegKeyValue("Cashmaster\\Exchange","HasSuffix");

                   // regKeyConnectionString = BMCRegistryHelper.GetRegLocalMachine().OpenSubKey("Software\\Honeyframe\\Cashmaster\\Exchange");
                   // if (regKeyConnectionString != null)
                   // {
                    bHasPrefix = BMCRegistryHelper.GetRegKeyValue("Cashmaster\\Exchange", "HasPrefix");// regKeyConnectionString.GetValue("HasPrefix").ToString();

                        regKeyConnectionString.Close();

                        if (bHasPrefix == "1" && bHasSuffix == "1")
                        {
                            bResult= true;
                        }
                        else
                        {
                            bResult= false;
                        }
                    //}
                    //bResult = false;
                //}
                return bResult;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return false;
            }
        }
        /// <summary>
        /// To check whether Enable Redeem Receipt printer is enabled from the setting table.
        /// </summary>
        /// <param name=""></param>
        /// <returns >true or false</returns>
        ///  /// Method Revision History
        ///
        /// Author             Date              Description
        /// ---------------------------------------------------
        /// Vineetha Mathew        4-March-2009         Intial Version 
        ///         
        public bool CheckEnableRedeemPrintCDODB()
        {
            string strproc = "CheckEnableRedeemPrintCDODB";
            bool bEnableRedeemPrint = false;
            try
            {
                SqlParameter[] sqlparams = GetSettingParameterDB(DBConstants.CONST_SP_PARAM_ENABLEREDEEMPRINTCDO);                
                SqlHelper.ExecuteNonQuery(GetExchangeConnectionString(), System.Data.CommandType.StoredProcedure, DBConstants.CONST_SP_GETSETTING, sqlparams);
                if (sqlparams[3].Value != null || sqlparams[3].Value.ToString() != string.Empty)
                {
                    if (Convert.ToString(sqlparams[3].Value).ToUpper() == "TRUE")
                    { bEnableRedeemPrint = true; }
                    else
                    { bEnableRedeemPrint = false; }
                }
                else
                {
                    bEnableRedeemPrint = false;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog(strproc + ex.Message, LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
                bEnableRedeemPrint = false;                
            }
            return bEnableRedeemPrint;
        }

        /// <summary>
        /// To fetch the common card information from the setting table
        /// </summary>
        /// <param name=""></param>
        /// <returns >true or false</returns>
        public string[] GetCardInformation(string cardNumber)
        {
            string[] returnValue = new string[3];

            SqlParameter SettingValue = DataBaseServiceHandler.AddParameter<string>("@Setting_Value", DbType.String, cardNumber, ParameterDirection.InputOutput);
            SettingValue.Size = 20;

            try
            {
                DataBaseServiceHandler.ExecuteNonQuery(QueryType.Procedure, "rsp_GetSetting",
                    DataBaseServiceHandler.AddParameter<string>("@Setting_Name", DbType.String, "CardNumberFormat"),
                    DataBaseServiceHandler.AddParameter<string>("@Setting_Default", DbType.String, @";([A-Z,a-z,0-9])*\?"),
                    SettingValue);

                returnValue[0] = SettingValue.Value.ToString();

                DataBaseServiceHandler.ExecuteNonQuery(QueryType.Procedure, "rsp_GetSetting",
                    DataBaseServiceHandler.AddParameter<string>("@Setting_Name", DbType.String, "InnerCardNumberFormat"),
                    DataBaseServiceHandler.AddParameter<string>("@Setting_Default", DbType.String, @"([A-Z,a-z,0-9])*"),
                    SettingValue);

                returnValue[1] = SettingValue.Value.ToString();

                DataBaseServiceHandler.ExecuteNonQuery(QueryType.Procedure, "rsp_GetSetting",
                    DataBaseServiceHandler.AddParameter<string>("@Setting_Name", DbType.String, "NumberOfCharToTrim"),
                    DataBaseServiceHandler.AddParameter<string>("@Setting_Default", DbType.String, @"4"),
                    SettingValue);

                returnValue[2] = SettingValue.Value.ToString();
            }

            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }

            return returnValue;
        }

        #endregion

        # region Get Meter Life
        /// <summary>
        /// Gets Meter Life
        /// </summary>
        /// <returns></returns>
        public static DataSet GetMeterLife(int InstallationNo)
        {
            SqlParameter[] objSQlParams = new SqlParameter[1];
            SqlParameter objSQL = new SqlParameter();
            objSQL.ParameterName = "inst_id";
            objSQL.Direction = ParameterDirection.Input;
            objSQL.Value = InstallationNo;
            objSQlParams[0] = objSQL;

            try
            {
                return SqlHelper.ExecuteDataset(GetExchangeConnectionString(), CommandType.StoredProcedure, DBConstants.CONSTANT_RSP_GETMETERLIFE, objSQlParams);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return null;
            }
        }
        #endregion

        #region Analysis Screen

        private static SqlParameter AddOutputparameter(string Paramtername, Object ParamValue)
        {
            SqlParameter objParam = new SqlParameter(Paramtername, ParamValue);
            objParam.Direction = ParameterDirection.Output;
            return objParam;
        }

        public static void GetAnalysisDetails(ref BMC.Transport.CashDeskOperatorEntity.SpotCheck objSpotCheck)
        {
            try
            {
                SqlParameter[] objParams = new SqlParameter[42];

                objParams[0] = new SqlParameter("@Installation_No", objSpotCheck.InstallationNo);
                objParams[1] = AddOutputparameter("@Cash_In ", objSpotCheck.CashIn);
                objParams[2] = AddOutputparameter("@Cash_Out", objSpotCheck.CashOut);
                objParams[3] = AddOutputparameter("@Token_In", objSpotCheck.TokenIn);
                objParams[4] = AddOutputparameter("@Token_Out", objSpotCheck.TokenOut);
                objParams[5] = AddOutputparameter("@Token_Refill", objSpotCheck.TokenRefill);
                objParams[6] = AddOutputparameter("@Cash_Refill", objSpotCheck.CashRefill);
                objParams[7] = AddOutputparameter("@COINS_IN", objSpotCheck.CoinsIn);
                objParams[8] = AddOutputparameter("@COINS_OUT", objSpotCheck.CoinsOut);
                objParams[9] = AddOutputparameter("@COINS_DROP", objSpotCheck.CoinsDrop);
                objParams[10] = AddOutputparameter("@CANCELLED_CREDITS", objSpotCheck.CancelledCredits);
                objParams[11] = AddOutputparameter("@VTP", objSpotCheck.VTP);
                objParams[12] = AddOutputparameter("@Datetime", objSpotCheck.DateTimeStamp);
                objParams[13] = AddOutputparameter("@Jackpot", objSpotCheck.Jackpot);
                objParams[14] = AddOutputparameter("@Handpay", objSpotCheck.HandPay);
                objParams[15] = AddOutputparameter("@BILL_1", objSpotCheck.Bill1);
                objParams[16] = AddOutputparameter("@BILL_2", objSpotCheck.Bill2);
                objParams[17] = AddOutputparameter("@BILL_5", objSpotCheck.Bill5);
                objParams[18] = AddOutputparameter("@BILL_10", objSpotCheck.Bill10);
                objParams[19] = AddOutputparameter("@BILL_20", objSpotCheck.Bill20);
                objParams[20] = AddOutputparameter("@BILL_50", objSpotCheck.Bill50);
                objParams[21] = AddOutputparameter("@BILL_100", objSpotCheck.Bill100);
                objParams[22] = AddOutputparameter("@BILL_250", objSpotCheck.Bill250);
                objParams[23] = AddOutputparameter("@BILL_10000", objSpotCheck.Bill10000);
                objParams[24] = AddOutputparameter("@BILL_20000", objSpotCheck.Bill20000);
                objParams[25] = AddOutputparameter("@BILL_50000", objSpotCheck.Bill50000);
                objParams[26] = AddOutputparameter("@BILL_100000", objSpotCheck.Bill100000);
                objParams[27] = AddOutputparameter("@Ticktes_Inserted", objSpotCheck.TicketsInserted);
                objParams[28] = AddOutputparameter("@TRUE_COIN_IN", objSpotCheck.TrueCoinIn);
                objParams[29] = AddOutputparameter("@TRUE_COIN_OUT", objSpotCheck.TrueCoinOut);
                objParams[30] = AddOutputparameter("@CASH_IN_2P", objSpotCheck.CashIn2p);
                objParams[31] = AddOutputparameter("@CASH_IN_100P", objSpotCheck.CashIn100p);
                objParams[32] = AddOutputparameter("@CASH_IN_200P", objSpotCheck.CashIn200p);
                objParams[33] = AddOutputparameter("@CASH_IN_500P", objSpotCheck.CashIn500p);
                objParams[34] = AddOutputparameter("@CASH_IN_1000P", objSpotCheck.CashIn1000p);
                objParams[35] = AddOutputparameter("@CASH_IN_2000P", objSpotCheck.CashIn2000p);
                objParams[36] = AddOutputparameter("@Tickets_Printed", objSpotCheck.TicketsPrinted);
                objParams[37] = new SqlParameter("@bStartOfDay", objSpotCheck.StartOfDay);
                objParams[38] = new SqlParameter("@bSelectDay", objSpotCheck.SelectDay);
                objParams[39] = new SqlParameter("@Date", objSpotCheck.Date);
                objParams[40] = AddOutputparameter("@NoOfDays", 0);
                objParams[41] = AddOutputparameter("@ProgHandpay", 0);


                SqlConnection objcon = new SqlConnection(GetExchangeConnectionString());
                objcon.Open();
                SqlCommand objcomd = new SqlCommand("usp_GetSpotCheckDataSAS", objcon);
                objcomd.CommandType = CommandType.StoredProcedure;
                objcomd.Parameters.AddRange(objParams);
                objcomd.ExecuteReader();
                objcon.Close();

                objSpotCheck.CashIn = Convert.ToInt32(objParams[1].Value);
                objSpotCheck.CashOut = Convert.ToInt32(objParams[2].Value);
                objSpotCheck.TokenIn = Convert.ToInt32(objParams[3].Value);
                objSpotCheck.TokenOut = Convert.ToInt32(objParams[4].Value);
                objSpotCheck.TokenRefill = Convert.ToInt32(objParams[5].Value);
                objSpotCheck.CashRefill = Convert.ToInt32(objParams[6].Value);
                objSpotCheck.CoinsIn = Convert.ToInt32(objParams[7].Value);
                objSpotCheck.CoinsOut = Convert.ToInt32(objParams[8].Value);
                objSpotCheck.CoinsDrop = Convert.ToDouble(objParams[9].Value);
                objSpotCheck.CancelledCredits = Convert.ToInt32(objParams[10].Value);
                objSpotCheck.VTP = Convert.ToInt32(objParams[11].Value);
                objSpotCheck.DateTimeStamp = Convert.ToDateTime(objParams[12].Value);
                objSpotCheck.Jackpot = Convert.ToInt32(objParams[13].Value);
                objSpotCheck.HandPay = Convert.ToInt32(objParams[14].Value);
                objSpotCheck.TicketsInserted = Convert.ToInt32(objParams[27].Value);
                objSpotCheck.TrueCoinIn = Convert.ToInt32(objParams[28].Value);
                objSpotCheck.TrueCoinOut = Convert.ToInt32(objParams[29].Value);
                objSpotCheck.TicketsPrinted = Convert.ToInt32(objParams[36].Value);

                objSpotCheck.Bill1 = Convert.ToInt32(objParams[15].Value);
                objSpotCheck.Bill2 = Convert.ToInt32(objParams[16].Value);
                objSpotCheck.Bill5 = Convert.ToInt32(objParams[17].Value);
                objSpotCheck.Bill10 = Convert.ToInt32(objParams[18].Value);

                objSpotCheck.Bill20 = Convert.ToInt32(objParams[19].Value);
                objSpotCheck.Bill50 = Convert.ToInt32(objParams[20].Value);
                objSpotCheck.Bill100 = Convert.ToInt32(objParams[21].Value);
                objSpotCheck.Bill250 = Convert.ToInt32(objParams[22].Value);
                objSpotCheck.Bill10000 = Convert.ToInt32(objParams[23].Value);
                objSpotCheck.Bill20000 = Convert.ToInt32(objParams[24].Value);
                objSpotCheck.Bill50000 = Convert.ToInt32(objParams[25].Value);
                objSpotCheck.Bill100000 = Convert.ToInt32(objParams[26].Value);
               
                objSpotCheck.CashIn2p = Convert.ToInt32(objParams[30].Value);
                objSpotCheck.CashIn100p = Convert.ToInt32(objParams[31].Value);
                objSpotCheck.CashIn200p = Convert.ToInt32(objParams[32].Value);
                objSpotCheck.CashIn500p = Convert.ToInt32(objParams[33].Value);
                objSpotCheck.CashIn1000p = Convert.ToInt32(objParams[34].Value);
                objSpotCheck.CashIn2000p = Convert.ToInt32(objParams[35].Value);
                
                objSpotCheck.NumberOfDays = Convert.ToInt32(objParams[40].Value);
                objSpotCheck.ProgHandpay = Convert.ToInt32(objParams[41].Value);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        public static DataTable GetMonthOrWeekStartDate()
        {
            try
            {
                return SqlHelper.ExecuteDataset(GetExchangeConnectionString(), CommandType.StoredProcedure, "rsp_GetWTDMTD").Tables[0];
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return new DataTable();
            }
        }
        #endregion

        #region Field Service
        /// <summary>
        /// Returns the datatable with the list of Current Calls.
        /// </summary>
        /// <returns>Datatable</returns>
        /// Method Revision History
        ///
        /// Author             Date              Description
        /// ---------------------------------------------------
        /// Renjish N          17-Jan-2008      Initial Version 
        public static DataTable GetCurrentServiceCalls(int iSiteCode)
        {
            DataTable dtCurrentServiceCalls = new DataTable();

            try
            {
                SqlParameter[] objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter(DBConstants.CONST_PARAM_SITE_CODE, iSiteCode);

                return SqlHelper.ExecuteDataset(GetExchangeConnectionString(), DBConstants.CONST_SP_RSP_GETCURRENTSERVICECALLS, objParams).Tables[0];
            }
            catch (Exception ex)
            {
                LogManager.WriteLog(DBConstants.CONST_SP_RSP_GETCURRENTSERVICECALLS + ex.Message, LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
                return new DataTable();
            }
        }
        /// <summary>
        /// Returns the site code.
        /// </summary>
        /// <returns>Datatable</returns>
        /// Method Revision History
        ///
        /// Author             Date              Description
        /// ---------------------------------------------------
        /// Renjish N          17-Jan-2008      Initial Version 
        [Obsolete]
        public string GetCurrentSiteCode()
        {
            string strSiteCode = string.Empty;
            DataTable dtCurrentSiteDetails = new DataTable();

            try
            {
                dtCurrentSiteDetails = SqlHelper.ExecuteDataset(GetExchangeConnectionString(), DBConstants.CONST_SP_RSP_GETSITEDETAILS).Tables[0];

                foreach (DataRow dr in dtCurrentSiteDetails.Rows)
                {
                    strSiteCode = dr["Code"].ToString();
                }

                return strSiteCode;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog(DBConstants.CONST_SP_RSP_GETSITEDETAILS + ex.Message, LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
                return string.Empty;
            }
        }
        /// <summary>
        /// Returns the list of Bar Position Names for the site.
        /// </summary>
        /// <returns>Datatable</returns>
        /// Method Revision History
        ///
        /// Author             Date              Description
        /// ---------------------------------------------------
        /// Renjish N          17-Jan-2008      Initial Version 
        [Obsolete]
        public string GetCurrentBarPositionNames()
        {
            DataTable dtCurrentServiceCalls = new DataTable();
            SqlDataReader drReader;
            string strBarPosNames = string.Empty;
            string strSiteCode = string.Empty;

            try
            {
                strSiteCode = Settings.SiteCode; //GetCurrentSiteCode();
                SqlParameter[] objParams = new SqlParameter[2];
                objParams[0] = new SqlParameter();
                objParams[0].ParameterName = DBConstants.CONST_PARAM_SITE_CODE;
                objParams[0].SqlDbType = SqlDbType.VarChar;
                objParams[0].Value = strSiteCode;

                objParams[0] = new SqlParameter("@RETURN_VALUE", SqlDbType.Int);
                objParams[0].Direction = ParameterDirection.ReturnValue;

                drReader = SqlHelper.ExecuteReader(GetExchangeConnectionString(), DBConstants.CONST_SP_RSP_GETCURRENTBARPOSITIONNAMES, objParams);
                while (drReader.Read())
                {
                    strBarPosNames = strBarPosNames + "," + drReader[1].ToString();
                }

                return strBarPosNames;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog(DBConstants.CONST_SP_RSP_GETCURRENTBARPOSITIONNAMES + ex.Message, LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
                return string.Empty;
            }
            finally
            {
                //drReader.Dispose();
            }
        }
        /// <summary>
        /// Returns the number active Bar Positions.
        /// </summary>
        /// <returns>string</returns>
        /// Method Revision History
        ///
        /// Author             Date              Description
        /// ---------------------------------------------------
        /// Renjish N          21-Jan-2008      Initial Version 
        [Obsolete]
        public int GetCurrentBarPosCount()
        {
            int iBarPosCount = 0;

            try
            {
                iBarPosCount = Convert.ToInt32(SqlHelper.ExecuteScalar(GetExchangeConnectionString(), CommandType.Text, "SELECT COUNT(*) FROM Bar_Position WHERE End_Date IS NULL"));
                return iBarPosCount;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog(DBConstants.CONST_SP_RSP_GETCURRENTSERVICECALLS + ex.Message, LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
                return iBarPosCount;
            }
        }

        public DataTable GetPositionList()
        {
            try
            {
                return SqlHelper.ExecuteDataset(GetExchangeConnectionString(), CommandType.StoredProcedure, DBConstants.CONST_SP_RSP_GETCURRENTBARPOSITIONNAMES).Tables[0];
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
                return null;
            }
        }

        public string PrepareCashDeskEvent(int InstallationNo, int FaultType)
        {
            string sEventXML = string.Empty; ;            
            string sOutputXML = string.Empty;

            SqlParameter oParam = new SqlParameter();
            oParam.ParameterName = "@InstallationNo";
            oParam.Value = InstallationNo;
            oParam.Direction = ParameterDirection.Input;

            SqlParameter oParamFaultType = new SqlParameter();
            oParamFaultType.ParameterName = "@FaultType";
            oParamFaultType.Value = FaultType;
            oParamFaultType.Direction = ParameterDirection.Input;

            
            SqlParameter[] oEventParam = new SqlParameter[2];
            oEventParam[0] = oParam;
            oEventParam[1] = oParamFaultType;

            try
            {
                sEventXML = SqlHelper.ExecuteScalar(GetExchangeConnectionString(), DBConstants.CONST_SP_RSP_CASHDESKEVENT, oEventParam).ToString();
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }

            return sEventXML;
        }

        #endregion

        #region GetPositonDetails
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

                return SqlHelper.ExecuteDataset(GetExchangeConnectionString(), CommandType.StoredProcedure, DBConstants.CONST_SP_RSP_GETPOSITIONDETAILS, oEventParam).Tables[0];
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
                return null;
            }
        }
        #endregion GetPositionDetails


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
