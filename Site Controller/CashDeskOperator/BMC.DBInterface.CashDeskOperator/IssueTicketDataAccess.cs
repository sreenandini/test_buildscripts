using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using BMC.Common.ExceptionManagement;
using BMC.Common.Utilities;
using BMC.DataAccess;
using BMC.Transport;
using BMC.Transport.CashDeskOperatorEntity;

namespace BMC.DBInterface.CashDeskOperator
{
    public partial class IssueTicketDataAccess
    {

        CommonDataAccess commonDataAccess = new CommonDataAccess();

        #region "Private Variables"
        #endregion

        #region "Public Properties"
        #endregion

        #region "Private Function"
        #endregion

        #region "Public Function"

        #region Issue Ticket

        /// <summary>
        /// To create new ticket for the amount entered
        /// </summary>
        /// <param name="_objCDOEntity"></param>
        /// <returns type=boolean>bTicketCreateReq</returns>
        public PrintTicketErrorCodes TicketCreateRequest(IssueTicketEntity issueTicketEntity)
        {
            string retBarCode = string.Empty;
            int Amount;
            
          //  bool IsTicketCreated = false;
            try
            {
                if (OpenConnection() == true)
                {

                    SqlParameter[] sqlparams = GetSpParametersCreateTicketStart(issueTicketEntity);

                    SqlHelper.ExecuteNonQuery(CommonDataAccess.ExchangeConnectionString, System.Data.CommandType.StoredProcedure, DBConstants.CONST_SP_CREATETICKETSTART, sqlparams);

                    if (sqlparams[2].Value.ToString() != string.Empty)
                    {
                        retBarCode = sqlparams[2].Value.ToString();
                        Amount = Convert.ToInt32(issueTicketEntity.lnglValue);
                        //SDGTicketGenLib.cSDGTicketGenClass ticketGen = new SDGTicketGen.cSDGTicketGenClass();
                        //retBarCode = ticketGen.StdBarcode(ref retBarCode, ref Amount);
                        SDGTicketGenLib.SDGTicketGenClass ticketGen = new SDGTicketGenLib.SDGTicketGenClass();
                        retBarCode = ticketGen.stdBarcode(retBarCode, Amount);

                        if (LogTitoTicketPrint(retBarCode, Amount) > 0)
                        {
                            issueTicketEntity.BarCode = retBarCode;// Convert.ToString(sqlparams[4].Value);
                           // IsTicketCreated = true;   
                        return PrintTicketErrorCodes.Success;
                    }
                }
            }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            //return IsTicketCreated;
            return PrintTicketErrorCodes.TicketCreateRequestFailure;

        }

        public int LogTitoTicketPrint(string BarCode, int Amount)
        {
           

            return DataBaseServiceHandler.ExecuteNonQuery(CommonDataAccess.ExchangeConnectionString, CommandType.StoredProcedure, "usp_LogTITOTicketPrint",
                DataBaseServiceHandler.AddParameter<string>("@TicketNumber", DbType.String, BarCode),
                DataBaseServiceHandler.AddParameter<string>("@Workstation", DbType.String, Environment.MachineName),
                DataBaseServiceHandler.AddParameter<int>("@Value", DbType.Int32, Amount),
                DataBaseServiceHandler.AddParameter<int>("@Installation_No", DbType.Int32, 0));

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
                Connection = CommonDataAccess.ExchangeConnectionString;

                if (CommonDataAccess.GetTicketingConnectionString(Connection) != string.Empty)
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
                //BGSGeneral.cGeneral objBGSGeneral = new BGSGeneral.cGeneral();
                if (issueTicketEntity != null)
                {
                    sp_parames = new SqlParameter[5];

                    SqlParameter param0 = new SqlParameter();
                    param0.ParameterName = DBConstants.CONST_SP_PARAM_INSTALLATIONID;
                    param0.Direction = ParameterDirection.Input;
                    param0.Value = 0;
                    sp_parames[0] = param0;

                    //SqlParameter param1 = new SqlParameter();
                    //param1.ParameterName = DBConstants.CONST_SP_PARAM_DATAPAKSERIALNUMBER;
                    //param1.Direction = ParameterDirection.Input;
                    //param1.Value = 0;
                    //sp_parames[1] = param1;

                    SqlParameter param2 = new SqlParameter();
                    param2.ParameterName = DBConstants.CONST_SP_PARAM_TICKETVALUE;
                    param2.Direction = ParameterDirection.Input;
                    param2.Value = issueTicketEntity.lnglValue;
                    sp_parames[1] = param2;

                    //SqlParameter param3 = new SqlParameter();
                    //param3.ParameterName = DBConstants.CONST_SP_PARAM_SERVERNAME;
                    //param3.Direction = ParameterDirection.Input;
                    //param3.Value = objBGSGeneral.GetMachineName();
                    //sp_parames[3] = param3;

                    sp_parames[2] = new SqlParameter();
                    sp_parames[2].ParameterName = DBConstants.CONST_SP_PARAM_RETURNTICKETNUMBER;
                    sp_parames[2].Direction = ParameterDirection.Output;
                    sp_parames[2].Value = string.Empty;
                    sp_parames[2].SqlDbType = SqlDbType.VarChar;
                    sp_parames[2].Size = 18;

                    SqlParameter param5 = new SqlParameter();
                    param5.ParameterName = DBConstants.CONST_SP_PARAM_TICKETTYPE;
                    param5.Direction = ParameterDirection.Input;
                    param5.Value = issueTicketEntity.Type;
                    sp_parames[3] = param5;

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
                SqlParameter[] sqlparams = CommonDataAccess.GetSettingParameterDB(DBConstants.CONST_SP_PARAM_REGION);
                SqlHelper.ExecuteNonQuery(CommonDataAccess.ExchangeConnectionString, CommandType.StoredProcedure, DBConstants.CONST_SP_GETSETTING, sqlparams);
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
                SqlParameter[] sqlparams = CommonDataAccess.GetSettingParameterDB(DBConstants.CONST_SP_PARAM_VOUCHERSITENAME);
                SqlHelper.ExecuteNonQuery(CommonDataAccess.ExchangeConnectionString, System.Data.CommandType.StoredProcedure, DBConstants.CONST_SP_GETSETTING, sqlparams);

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
        public PrintTicketErrorCodes TicketPrintConfirmed(IssueTicketEntity issueTicketEntity)
        {
           // bool IsTicketPrinted = false;

            try
            {
                SqlParameter[] sqlparams = GetSpParametersCreateTicketComplete(issueTicketEntity);
                SqlHelper.ExecuteNonQuery(CommonDataAccess.ExchangeConnectionString, System.Data.CommandType.StoredProcedure, DBConstants.CONST_SP_CREATETICKETCOMPLETE, sqlparams);
                if (sqlparams[4].Value != DBNull.Value && Convert.ToString(sqlparams[4].Value) != string.Empty && sqlparams[4].Value != null)
                {
                    if (Convert.ToInt16(sqlparams[4].Value) == 0)
                        //  IsTicketPrinted = true;
                        return PrintTicketErrorCodes.Success;
                }
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

            //return IsTicketPrinted;
            return PrintTicketErrorCodes.TicketPrintConfirmationFailure;

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
                    sp_parames = new SqlParameter[5];
                    sp_parames[0] = new SqlParameter(DBConstants.CONST_SP_PARAM_BARCODE, issueTicketEntity.BarCode);

                    sp_parames[1] = new SqlParameter("@IssuedUser", Security.SecurityHelper.CurrentUser.UserName);
                    sp_parames[2] = new SqlParameter("@RedeemedUser", string.Empty);

                    sp_parames[3] = new SqlParameter();
                    sp_parames[3].ParameterName = DBConstants.CONST_SP_PARAM_RETRESULT;
                    sp_parames[3].Direction = ParameterDirection.Output;
                    sp_parames[3].Value = 0;
                    sp_parames[3].SqlDbType = SqlDbType.Int;

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
        /// <summary>
        /// Save the ticket details 
        /// </summary>
        /// <param name="ObjCDOEntity"></param>
        /// <returns></returns>
        public PrintTicketErrorCodes SaveTicketIssueDetails(IssueTicketEntity issueTicketEntity)
        {
           // bool IsTicketIssueDetailsSaved = false;

            try
            {
                SqlParameter[] sqlparams = GetSpParametersSaveTicketIssueDetails(issueTicketEntity);
                SqlHelper.ExecuteNonQuery(CommonDataAccess.ExchangeConnectionString, System.Data.CommandType.StoredProcedure, DBConstants.CONST_SP_INSERTTICKETISSUE, sqlparams);
                if (sqlparams[8].Value != DBNull.Value && Convert.ToString(sqlparams[8].Value) != string.Empty && sqlparams[8].Value != null)
                {
                    if (int.Parse(sqlparams[8].Value.ToString()) == 0)
                        //IsTicketIssueDetailsSaved = true;
                        return PrintTicketErrorCodes.Success;
                }
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

           // return IsTicketIssueDetailsSaved;
            return PrintTicketErrorCodes.SaveTicketIssueDetailsFailure;

        }
        /// <summary>
        /// To set parameters for completing the create ticket procedure
        /// </summary>
        /// <param name="objCDOEntity"></param>
        /// <returns type=objectarray >sp_parames</returns>
        public SqlParameter[] GetSpParametersSaveTicketIssueDetails(IssueTicketEntity issueTicketEntity)
        {
            SqlParameter[] sp_parames = null;
         
            try
            {
                if (issueTicketEntity != null)
                {
                    sp_parames = new SqlParameter[9];
                    sp_parames[0] = new SqlParameter(DBConstants.CONST_SP_PARAM_TICKETNUMBER, issueTicketEntity.BarCode);
                    sp_parames[1] = new SqlParameter(DBConstants.CONST_SP_PARAM_DATEPRINTED, issueTicketEntity.Date);
                    sp_parames[2] = new SqlParameter(DBConstants.CONST_SP_PARAM_WINDOWUSER, System.Environment.UserName.ToString());
                    sp_parames[3] = new SqlParameter(DBConstants.CONST_SP_PARAM_USERID, "userstring");
                    sp_parames[4] = new SqlParameter(DBConstants.CONST_SP_PARAM_MACHINENAME, Environment.MachineName);
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
                SqlParameter[] sqlparams = CommonDataAccess.GetSettingParameterDB(DBConstants.CONST_SP_PARAM_ENABLEISSUERECEIPT);
                SqlHelper.ExecuteNonQuery(CommonDataAccess.ExchangeConnectionString, System.Data.CommandType.StoredProcedure, DBConstants.CONST_SP_GETSETTING, sqlparams);

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
                    GetTicketInfo = SqlHelper.ExecuteDataset(CommonDataAccess.TicketingConnectionString, DBConstants.CONST_SP_BGSVOUCHERINFORMATION, sqlparams);
                    if (GetTicketInfo.Tables[0].Rows.Count > 0)
                    {
                        voucher.PrintDevice = GetTicketInfo.Tables[0].Rows[0][DBConstants.CONST_SP_RESULT_PRINTDEVICE].ToString();
                        voucher.PayDevice = GetTicketInfo.Tables[0].Rows[0][DBConstants.CONST_SP_RESULT_PAYDEVICE].ToString();
                        voucher.SBarCode = GetTicketInfo.Tables[0].Rows[0][DBConstants.CONST_SP_RESULT_BARCODE].ToString();
                        voucher.Amount = int.Parse((GetTicketInfo.Tables[0].Rows[0][DBConstants.CONST_SP_RESULT_AMOUNT]).ToString());
                        voucher.VoucherStatus = GetTicketInfo.Tables[0].Rows[0][DBConstants.CONST_SP_RESULT_VOUCHERSTATUS].ToString();
                        voucher.Datepaid = (GetTicketInfo.Tables[0].Rows[0][DBConstants.CONST_SP_RESULT_DATEPAID]).ToString().ReadDate();
                        voucher.DatePrinted = (GetTicketInfo.Tables[0].Rows[0][DBConstants.CONST_SP_RESULT_DATEPRINTED]).ToString().ReadDate();
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

                dsGetMachineDetailsFromAsset = SqlHelper.ExecuteDataset(CommonDataAccess.ExchangeConnectionString, DBConstants.CONST_SP_GETMACHINEDETAILSFROMASSET, sqlparams);

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
                    voucher.PrintDeviceName = string.Empty;
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
                    sp_parames = new SqlParameter[6];
                    sp_parames[0] = new SqlParameter(DBConstants.CONST_SP_PARAM_STARTDATE, DateTime.Now.AddDays(-5));//.GetUniversalDateTimeFormat()
                    sp_parames[1] = new SqlParameter(DBConstants.CONST_SP_PARAM_ENDDATE, DateTime.Now.AddDays(5));//.GetUniversalDateTimeFormat()
                    sp_parames[2] = new SqlParameter(DBConstants.CONST_SP_PARAM_TYPE, "A");
                    sp_parames[3] = new SqlParameter(DBConstants.CONST_SP_PARAM_BARCODE, issueTicketEntity.BarCode);
                    sp_parames[4] = new SqlParameter(DBConstants.CONST_SP_PARAM_ISLIABILITY, 0);
                    sp_parames[5] = new SqlParameter("@User", 0);

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

        #endregion

        #region "Public Static Function"
        #endregion

    }
}
