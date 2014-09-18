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
        public bool GetTicketInfoForCage(IssueTicketEntity issueTicketEntity, ref CageVoucher voucher)
        {
            bool IsTicketInfoRetrieved = false;
            DataSet GetTicketInfo = new DataSet();

            try
            {
                SqlParameter[] sqlparams = new SqlParameter[5];
                sqlparams[0] = new SqlParameter(DBConstants.CONST_SP_PARAM_STARTDATE, DateTime.Now.AddDays(-5));//.GetUniversalDateTimeFormat()
                sqlparams[1] = new SqlParameter(DBConstants.CONST_SP_PARAM_ENDDATE, DateTime.Now.AddDays(5));//.GetUniversalDateTimeFormat()
                sqlparams[2] = new SqlParameter(DBConstants.CONST_SP_PARAM_TYPE, "A");
                sqlparams[3] = new SqlParameter(DBConstants.CONST_SP_PARAM_BARCODE, issueTicketEntity.BarCode);
                sqlparams[4] = new SqlParameter(DBConstants.CONST_SP_PARAM_ISLIABILITY, 0);

                if (OpenConnection() == true)
                {
                    GetTicketInfo = SqlHelper.ExecuteDataset(CommonDataAccess.TicketingConnectionString, "rsp_BGS_VoucherInformation_CAGE", sqlparams);
                    if (GetTicketInfo.Tables[0].Rows.Count > 0)
                    {
                        voucher.PrintDevice = GetTicketInfo.Tables[0].Rows[0][DBConstants.CONST_SP_RESULT_PRINTDEVICE].ToString();
                        voucher.PayDevice = GetTicketInfo.Tables[0].Rows[0][DBConstants.CONST_SP_RESULT_PAYDEVICE].ToString();
                        voucher.SBarCode = GetTicketInfo.Tables[0].Rows[0][DBConstants.CONST_SP_RESULT_BARCODE].ToString();
                        voucher.Amount = int.Parse((GetTicketInfo.Tables[0].Rows[0][DBConstants.CONST_SP_RESULT_AMOUNT]).ToString());
                        voucher.tktStatus = GetTicketInfo.Tables[0].Rows[0]["strVoucherStatus"].ToString();
                        voucher.tktStatusID = short.Parse(GetTicketInfo.Tables[0].Rows[0]["VoucherStatusID"].ToString());
                        voucher.Datepaid = (GetTicketInfo.Tables[0].Rows[0][DBConstants.CONST_SP_RESULT_DATEPAID]).ToString().ReadDate();
                        voucher.DatePrinted = (GetTicketInfo.Tables[0].Rows[0][DBConstants.CONST_SP_RESULT_DATEPRINTED]).ToString().ReadDate();
                        voucher.DeviceType = GetTicketInfo.Tables[0].Rows[0][DBConstants.CONST_SP_RESULT_DEVICETYPE].ToString();
                        voucher.expireDate = GetTicketInfo.Tables[0].Rows[0]["dtExpire"].ToString().ReadDate();
                        voucher.expiryDays = int.Parse(GetTicketInfo.Tables[0].Rows[0]["expiryDays"].ToString());
                        IsTicketInfoRetrieved = true;
                        voucher.ErrorCode = 0;
                    }
                }

            }
            catch (Exception ex)
            {
                voucher.ErrorCode = -1001;
                ExceptionManager.Publish(ex);
            }
            return IsTicketInfoRetrieved;
        }
        public voucherDTO[] SearchTicketForCage(String partialBarcode, int siteId, long amount, int maxCount)
        {
            DataSet GetTicketInfo = new DataSet();
            voucherDTO[] oVouchers =null;//= new List<voucherDTO>();
            voucherDTO voucher = null;
            try
            {
                SqlParameter[] sqlparams = new SqlParameter[5];
                sqlparams[0] = new SqlParameter("@barcode", partialBarcode);
                sqlparams[1] = new SqlParameter("@siteId", siteId);
                sqlparams[2] = new SqlParameter("@amount", amount);
                sqlparams[3] = new SqlParameter("@maxCount", maxCount);
                sqlparams[4] = new SqlParameter("@ClientSiteCode", Settings.SiteCode);

                if (OpenConnection() == true)
                {
                    GetTicketInfo = SqlHelper.ExecuteDataset(CommonDataAccess.TicketingConnectionString, "rsp_BGS_Search_ByNumber_CAGE", sqlparams);
                    if (GetTicketInfo.Tables[0].Rows.Count > 0)
                    {
                        oVouchers = new voucherDTO[GetTicketInfo.Tables[0].Rows.Count];
                        for (int iCount = 0; iCount < GetTicketInfo.Tables[0].Rows.Count;iCount ++ )
                        {
                            DataRow oDr = GetTicketInfo.Tables[0].Rows[iCount]; 
                            voucher = new voucherDTO();
                            voucher.barcode = oDr[DBConstants.CONST_SP_RESULT_BARCODE].ToString();
                            voucher.errorCodeId = 0;
                            if (oDr["TicketType"].ToString() == "0")
                            {
                                voucher.amountType = amountTypeEnum.CASHABLE;
                              //  voucher.ticketType = ticketTypeEnum.CASHABLE_PROMO;
                              //  voucher.ticketTypeValue = ticketTypeEnum.CASHABLE_PROMO.ToString();
                                voucher.allowRedeem = true;
                            }
                            else if (oDr["TicketType"].ToString() == "1")
                            {
                                voucher.amountType = amountTypeEnum.NONCASHABLE;
                                //     voucher.ticketType = ticketTypeEnum.NON_CASHABLE_PROMO;
                                //     voucher.ticketTypeValue = ticketTypeEnum.NON_CASHABLE_PROMO.ToString();
                                voucher.allowRedeem = false;
                            }
                            else if( oDr["TicketType"].ToString() == "2")
                            {
                                voucher.amountType = amountTypeEnum.CASHABLE_PROMO;
                                voucher.allowRedeem = true;
                            }
                            else
                            {
                                voucher.amountType = amountTypeEnum.NONCASHABLE;
                                voucher.allowRedeem = false;
                            }
                            
                           
                            if (short.Parse(oDr["VoucherStatusID"].ToString()) == -1)
                            {
                                voucher.allowRedeem = false ;
                            }
                            voucher.allowOverride = true;
                            voucher.amount = long.Parse((oDr[DBConstants.CONST_SP_RESULT_AMOUNT]).ToString());
                            voucher.tktStatus = oDr["strVoucherStatus"].ToString(); 
                            voucher.tktStatusId = short.Parse(oDr["VoucherStatusID"].ToString());
                            voucher.effectiveDate = (oDr[DBConstants.CONST_SP_RESULT_DATEPRINTED]).ToString().ReadDate();
                            voucher.expireDate = oDr["dtExpire"].ToString().ReadDate();
                            
                            voucher.expiryDays = int.Parse(oDr["expiryDays"].ToString());
                            
                            voucher.transDateTime = oDr["dtPaid"].ToString().ReadDate();
                            //need to be checked 
                            voucher.playerId = "";
                            voucher.playerCardReqd = "false";
                            voucher.employeeId = "";

                            oVouchers[iCount] = (voucher);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return oVouchers;
        }
        public voucherDTO[] SearchTicketForCage(String partialBarcode)
        {
            DataSet GetTicketInfo = new DataSet();
            voucherDTO[] oVouchers = null;//= new List<voucherDTO>();
            voucherDTO voucher = null;
            try
            {
                SqlParameter[] sqlparams = new SqlParameter[5];
                sqlparams[0] = new SqlParameter("@barcode", partialBarcode);
                sqlparams[1] = new SqlParameter("@siteId", 0);
                sqlparams[2] = new SqlParameter("@amount", 0);
                sqlparams[3] = new SqlParameter("@maxCount", 0);
                sqlparams[4] = new SqlParameter("@ClientSiteCode", Settings.SiteCode);

                if (OpenConnection() == true)
                {
                    GetTicketInfo = SqlHelper.ExecuteDataset(CommonDataAccess.TicketingConnectionString, "rsp_BGS_Search_ByNumber_CAGE", sqlparams);
                    if (GetTicketInfo.Tables[0].Rows.Count > 0)
                    {
                        oVouchers = new voucherDTO[GetTicketInfo.Tables[0].Rows.Count];
                        for (int iCount = 0; iCount < GetTicketInfo.Tables[0].Rows.Count; iCount++)
                        {
                            DataRow oDr = GetTicketInfo.Tables[0].Rows[iCount];
                            voucher = new voucherDTO();
                            voucher.barcode = oDr[DBConstants.CONST_SP_RESULT_BARCODE].ToString();
                            voucher.errorCodeId = 0;
                            if (oDr["TicketType"].ToString() == "0")
                            {
                                voucher.amountType = amountTypeEnum.CASHABLE;
                                //  voucher.ticketType = ticketTypeEnum.CASHABLE_PROMO;
                                //  voucher.ticketTypeValue = ticketTypeEnum.CASHABLE_PROMO.ToString();
                                voucher.allowRedeem = true;
                            }
                            else if (oDr["TicketType"].ToString() == "1")
                            {
                                voucher.amountType = amountTypeEnum.NONCASHABLE;
                                //     voucher.ticketType = ticketTypeEnum.NON_CASHABLE_PROMO;
                                //     voucher.ticketTypeValue = ticketTypeEnum.NON_CASHABLE_PROMO.ToString();
                                voucher.allowRedeem = false;
                            }
                            else if (oDr["TicketType"].ToString() == "2")
                            {
                                voucher.amountType = amountTypeEnum.CASHABLE_PROMO;
                                voucher.allowRedeem = true;
                            }
                            else
                            {
                                voucher.amountType = amountTypeEnum.NONCASHABLE;
                                voucher.allowRedeem = false;
                            }


                            if (short.Parse(oDr["VoucherStatusID"].ToString()) == -1)
                            {
                                voucher.allowRedeem = false;
                            }
                            voucher.allowOverride = true;
                            voucher.amount = long.Parse((oDr[DBConstants.CONST_SP_RESULT_AMOUNT]).ToString());
                            voucher.tktStatus = oDr["strVoucherStatus"].ToString();
                            voucher.tktStatusId = short.Parse(oDr["VoucherStatusID"].ToString());
                            voucher.effectiveDate = (oDr[DBConstants.CONST_SP_RESULT_DATEPRINTED]).ToString().ReadDate();
                            voucher.expireDate = oDr["dtExpire"].ToString().ReadDate();

                            voucher.expiryDays = int.Parse(oDr["expiryDays"].ToString());

                            voucher.transDateTime = oDr["dtPaid"].ToString().ReadDate();
                            //need to be checked 
                            voucher.playerId = "";
                            voucher.playerCardReqd = "false";
                            voucher.employeeId = "";

                            oVouchers[iCount] = (voucher);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return oVouchers;
        }
        public voucherDTO[] SearchTicketForCage(String partialBarcode,string ClientSiteCode)
        {
            DataSet GetTicketInfo = new DataSet();
            voucherDTO[] oVouchers = null;//= new List<voucherDTO>();
            voucherDTO voucher = null;
            try
            {
                SqlParameter[] sqlparams = new SqlParameter[5];
                sqlparams[0] = new SqlParameter("@barcode", partialBarcode);
                sqlparams[1] = new SqlParameter("@siteId", 0);
                sqlparams[2] = new SqlParameter("@amount", 0);  
                sqlparams[3] = new SqlParameter("@maxCount", 0);
                sqlparams[4] = new SqlParameter("@ClientSiteCode", ClientSiteCode);
                if (OpenConnection() == true)
                {
                    GetTicketInfo = SqlHelper.ExecuteDataset(CommonDataAccess.TicketingConnectionString, "rsp_BGS_Search_ByNumber_CAGE", sqlparams);
                    if (GetTicketInfo.Tables[0].Rows.Count > 0)
                    {
                        oVouchers = new voucherDTO[GetTicketInfo.Tables[0].Rows.Count];
                        for (int iCount = 0; iCount < GetTicketInfo.Tables[0].Rows.Count; iCount++)
                        {
                            DataRow oDr = GetTicketInfo.Tables[0].Rows[iCount];
                            voucher = new voucherDTO();
                            voucher.barcode = oDr[DBConstants.CONST_SP_RESULT_BARCODE].ToString();
                            voucher.errorCodeId = 0;
                            if (oDr["TicketType"].ToString() == "0")
                            {
                                voucher.amountType = amountTypeEnum.CASHABLE;
                                //  voucher.ticketType = ticketTypeEnum.CASHABLE_PROMO;
                                //  voucher.ticketTypeValue = ticketTypeEnum.CASHABLE_PROMO.ToString();
                                voucher.allowRedeem = true;
                            }
                            else if (oDr["TicketType"].ToString() == "1")
                            {
                                voucher.amountType = amountTypeEnum.NONCASHABLE;
                                //     voucher.ticketType = ticketTypeEnum.NON_CASHABLE_PROMO;
                                //     voucher.ticketTypeValue = ticketTypeEnum.NON_CASHABLE_PROMO.ToString();
                                voucher.allowRedeem = false;
                            }
                            else if (oDr["TicketType"].ToString() == "2")
                            {
                                voucher.amountType = amountTypeEnum.CASHABLE_PROMO;
                                voucher.allowRedeem = true;
                            }
                            else
                            {
                                voucher.amountType = amountTypeEnum.NONCASHABLE;
                                voucher.allowRedeem = false;
                            }


                            if (short.Parse(oDr["VoucherStatusID"].ToString()) == -1)
                            {
                                voucher.allowRedeem = false;
                            }
                            voucher.allowOverride = true;
                            voucher.amount = long.Parse((oDr[DBConstants.CONST_SP_RESULT_AMOUNT]).ToString());
                            voucher.tktStatus = oDr["strVoucherStatus"].ToString();
                            voucher.tktStatusId = short.Parse(oDr["VoucherStatusID"].ToString());
                            voucher.effectiveDate = (oDr[DBConstants.CONST_SP_RESULT_DATEPRINTED]).ToString().ReadDate();
                            voucher.expireDate = oDr["dtExpire"].ToString().ReadDate();

                            voucher.expiryDays = int.Parse(oDr["expiryDays"].ToString());

                            voucher.transDateTime = oDr["dtPaid"].ToString().ReadDate();
                            //need to be checked 
                            voucher.playerId = "";
                            voucher.playerCardReqd = "false";
                            voucher.employeeId = "";

                            oVouchers[iCount] = (voucher);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return oVouchers;
        }

        public bool GetMachineDetailsFromAssetForCage(ref CageVoucher voucher)
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
        public voucherDTO redeemRequestVoucherForCage(voucherDTO request)
        {
            DataSet GetTicketInfo = new DataSet();
            voucherDTO voucher = new voucherDTO();
            try
            {
                SqlParameter[] sqlparams = new SqlParameter[5];
                sqlparams[0] = new SqlParameter("@barcode", request.barcode);
                sqlparams[1] = new SqlParameter("@siteId", 0);
                sqlparams[2] = new SqlParameter("@amount", 0);
                sqlparams[3] = new SqlParameter("@maxCount", 0);
                sqlparams[4] = new SqlParameter("@ClientSiteCode", Settings.SiteCode);

                if (OpenConnection() == true)
                {
                    GetTicketInfo = SqlHelper.ExecuteDataset(CommonDataAccess.TicketingConnectionString, "rsp_BGS_Search_ByNumber_CAGE", sqlparams);
                    if (GetTicketInfo.Tables[0].Rows.Count > 0)
                    {

                        voucher.barcode = GetTicketInfo.Tables[0].Rows[0][DBConstants.CONST_SP_RESULT_BARCODE].ToString();
                        //voucher.amountType = amountTypeEnum.CASHABLE;
                        voucher.errorCodeId = 0;
                       // voucher.allowRedeem = true;
                        if (GetTicketInfo.Tables[0].Rows[0]["TicketType"].ToString() == "0")
                        {
                            voucher.amountType = amountTypeEnum.CASHABLE;
                            //  voucher.ticketType = ticketTypeEnum.CASHABLE_PROMO;
                            //  voucher.ticketTypeValue = ticketTypeEnum.CASHABLE_PROMO.ToString();
                            voucher.allowRedeem = true;
                        }
                        else if (GetTicketInfo.Tables[0].Rows[0]["TicketType"].ToString() == "1")
                        {
                            voucher.amountType = amountTypeEnum.NONCASHABLE;
                            //     voucher.ticketType = ticketTypeEnum.NON_CASHABLE_PROMO;
                            //     voucher.ticketTypeValue = ticketTypeEnum.NON_CASHABLE_PROMO.ToString();
                            voucher.allowRedeem = false;
                        }
                        else if (GetTicketInfo.Tables[0].Rows[0]["TicketType"].ToString() == "2")
                        {
                            voucher.amountType = amountTypeEnum.CASHABLE_PROMO;
                            voucher.allowRedeem = true;
                        }
                        else
                        {
                            voucher.amountType = amountTypeEnum.NONCASHABLE;
                            voucher.allowRedeem = false;
                        }

                        voucher.allowOverride = false;
                        voucher.amount = long.Parse((GetTicketInfo.Tables[0].Rows[0][DBConstants.CONST_SP_RESULT_AMOUNT]).ToString());
                        voucher.tktStatus = GetTicketInfo.Tables[0].Rows[0]["strVoucherStatus"].ToString();
                        voucher.tktStatusId = short.Parse(GetTicketInfo.Tables[0].Rows[0]["VoucherStatusID"].ToString());
                        voucher.effectiveDate = (GetTicketInfo.Tables[0].Rows[0][DBConstants.CONST_SP_RESULT_DATEPRINTED]).ToString().ReadDate();
                        voucher.expireDate = GetTicketInfo.Tables[0].Rows[0]["dtExpire"].ToString().ReadDate();
                        //voucher.allowRedeem = true;
                        voucher.ticketType = ticketTypeEnum.CASHABLE_PROMO;
                        voucher.ticketTypeValue = ticketTypeEnum.CASHABLE_PROMO.ToString() ;
                        voucher.expiryDays = int.Parse(GetTicketInfo.Tables[0].Rows[0]["expiryDays"].ToString());
                        voucher.transDateTime = GetTicketInfo.Tables[0].Rows[0]["dtPaid"].ToString().ReadDate();
                    }
                    else
                    {
                        voucher.errorCodeId = -3;
                        voucher.tktStatusId = -3;
                        voucher.tktStatus = "NO_DATA_FOUND_ERROR";
                    }
                }

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return voucher;
        }
        public PrintTicketErrorCodes SaveTicketIssueDetailsCage(IssueTicketEntity issueTicketEntity)
        {
            // bool IsTicketIssueDetailsSaved = false;

            try
            {
                SqlParameter[] sqlparams = GetSpParametersSaveTicketIssueDetailsCage(issueTicketEntity);
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
        public SqlParameter[] GetSpParametersSaveTicketIssueDetailsCage(IssueTicketEntity issueTicketEntity)
        {
            SqlParameter[] sp_parames = null;
           // BGSGeneral.cGeneral objBGSGeneral = new BGSGeneral.cGeneral();
            try
            {
                if (issueTicketEntity != null)
                {
                    sp_parames = new SqlParameter[9];
                    sp_parames[0] = new SqlParameter(DBConstants.CONST_SP_PARAM_TICKETNUMBER, issueTicketEntity.BarCode);
                    sp_parames[1] = new SqlParameter(DBConstants.CONST_SP_PARAM_DATEPRINTED, issueTicketEntity.Date);
                    sp_parames[2] = new SqlParameter(DBConstants.CONST_SP_PARAM_WINDOWUSER, System.Environment.UserName.ToString());
                    sp_parames[3] = new SqlParameter(DBConstants.CONST_SP_PARAM_USERID, "userstring");
                    sp_parames[4] = new SqlParameter(DBConstants.CONST_SP_PARAM_MACHINENAME, Environment.MachineName  +"_Cage");
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
        public PrintTicketErrorCodes TicketCreateRequestCage(IssueTicketEntity issueTicketEntity)
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
                        //SDGTicketGen.cSDGTicketGenClass ticketGen = new SDGTicketGen.cSDGTicketGenClass();
                        //retBarCode = ticketGen.StdBarcode(ref retBarCode, ref Amount);
                        SDGTicketGenLib.SDGTicketGenClass ticketGen = new SDGTicketGenLib.SDGTicketGenClass();
                        retBarCode = ticketGen.stdBarcode(retBarCode, Amount);

                        if (LogTitoTicketPrintCage(retBarCode, Amount) > 0)
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

        public int LogTitoTicketPrintCage(string BarCode, int Amount)
        {
            //BGSGeneral.cGeneralClass general = new BGSGeneral.cGeneralClass();

            return DataBaseServiceHandler.ExecuteNonQuery(CommonDataAccess.ExchangeConnectionString, CommandType.StoredProcedure, "usp_LogTITOTicketPrint",
                DataBaseServiceHandler.AddParameter<string>("@TicketNumber", DbType.String, BarCode),
                DataBaseServiceHandler.AddParameter<string>("@Workstation", DbType.String,Environment.MachineName+"_Cage"),
                DataBaseServiceHandler.AddParameter<int>("@Value", DbType.Int32, Amount),
                DataBaseServiceHandler.AddParameter<int>("@Installation_No", DbType.Int32, 0));
        }
        public string CancelTicketCage(string strBarcode)
        {
            //BGSGeneral.cGeneralClass objBGSGeneral = new BGSGeneral.cGeneralClass();
            SqlParameter[] oParams=new SqlParameter[3];
            oParams[0] = new SqlParameter("@InstallationNo", 0);
            oParams[1] = new SqlParameter("@TicketNumber", strBarcode);
            oParams[2]=new SqlParameter("@RetVal",0);
            oParams[2].Direction = ParameterDirection.InputOutput;
            SqlHelper.ExecuteNonQuery(CommonDataAccess.ExchangeConnectionString, CommandType.StoredProcedure, "usp_CancelTicketCompleteMC300", oParams);
            return oParams[2].Value.ToString()  ;
        }

        public bool ValidateUserCage(string UserName, string Password)
        {
            bool retVal = false;
            SqlParameter[] oParams = new SqlParameter[2];
            oParams[0] = new SqlParameter("@UserName", UserName);
            oParams[1] = new SqlParameter("@Password", Password);
            SqlDataReader readUser = SqlHelper.ExecuteReader(CommonDataAccess.ExchangeConnectionString, CommandType.StoredProcedure, "rsp_ValidateUserCage", oParams);
            while (readUser.Read())
            {
                retVal = Convert.ToInt32(readUser["isValid"]) > 0 ? true : false;

            }
            return retVal;
        }
    }
}
