using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using BMC.Common.Utilities;
using BMC.DataAccess;
using BMC.Common.ExceptionManagement;
using BMC.Transport.CashDeskOperatorEntity;
using BMC.Transport;
using BMC.Common.LogManagement;


namespace BMC.DBInterface.CashDeskOperator
{
    public class RedeemTicketDataAccess
    {
        #region "Private Variables"
        #endregion

        #region "Public Properties"
        #endregion

        #region "Private Function"
        #endregion

        #region "Public Function"

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

                objSQLParams = CommonDataAccess.GetSettingParameterDB(DBConstants.CONST_SP_PARAM_AMBERCREDITSWAGEREDTOCASHIN);
                SqlHelper.ExecuteNonQuery(CommonDataAccess.ExchangeConnectionString, System.Data.CommandType.StoredProcedure, DBConstants.CONST_SP_GETSETTING, objSQLParams);
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

        /// <summary>
        /// Returns the currency symbol.
        /// </summary>
        /// <returns></returns>
        public DataSet GetTicketHistory(RTOnlineWageredDropDetail WagDrop)
        {
            DataSet TicketHistory = new DataSet();

            try
            {

                //DataBaseServiceHandler.ConnectionString = CommonDataAccess.ExchangeConnectionString;
                TicketHistory = DataBaseServiceHandler.Fill(CommonDataAccess.ExchangeConnectionString, CommandType.StoredProcedure, DBConstants.CONST_RSP_GETTICKETHISTORY, TicketHistory,
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

                SqlHelper.FillDataset(CommonDataAccess.GetTicketingConnectionString(strcon), DBConstants.CONST_RSP_GETMACHINEDETAILSVIATBRPAYOUT, MachineDetails, strArray, objParams);

                if (MachineDetails.Tables.Count > 0)
                {
                    if (MachineDetails.Tables["MachineDetails"].Rows.Count > 0)
                    {
                        RTOReceiptDetail.DatePrinted =
                            (MachineDetails.Tables["MachineDetails"].Rows[0]["TBR_Payout_Print_Time"]).ToString().
                                ReadDate();
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

                SqlHelper.ExecuteNonQuery(CommonDataAccess.ExchangeConnectionString, System.Data.CommandType.StoredProcedure, DBConstants.CONST_RSP_DOESOFFLINETICKETEXIST, objParams);

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
            string Currency = "".GetCurrencySymbol();
            try
            {
                DataSet ExceptionDetails = new DataSet();
                DateTime date = new DateTime();
                string[] strArray = new string[1];
                strArray[0] = "ExceptionDetails";
                date = DateTime.Now;

                SqlParameter[] objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter(DBConstants.CONST_PARAM_BARCODE, RTOTicketDetail.TicketString);

                SqlHelper.FillDataset(CommonDataAccess.ExchangeConnectionString, DBConstants.CONST_RSP_GETEXCEPTIONDETAILS, ExceptionDetails, strArray, objParams);

                if (ExceptionDetails.Tables.Count > 0)
                {
                    if (ExceptionDetails.Tables["ExceptionDetails"].Rows.Count > 0)
                    {
                        RTOTicketDetail.RedeemedMachine = ExceptionDetails.Tables["ExceptionDetails"].Rows[0]["Bar_pos_name"].ToString();
                        RTOTicketDetail.RedeemedDevice = ExceptionDetails.Tables["ExceptionDetails"].Rows[0]["TE_Workstation"].ToString();
                        RTOTicketDetail.RedeemedDate =
                            Common.Utilities.Common.GetRowValue<DateTime>(
                                ExceptionDetails.Tables["ExceptionDetails"].Rows[0], "TE_Date");// ExceptionDetails.Tables["ExceptionDetails"].Rows[0]["TE_Date"].ToString();

                        RTOTicketDetail.RedeemedAmount =
                            Convert.ToDecimal(
                                Common.Utilities.Common.GetRowValue<int>(
                                    ExceptionDetails.Tables["ExceptionDetails"].Rows[0], "te_value"))/100;
                        // RTOTicketDetail.RedeemedAmount = Currency + " " + (Convert.ToDouble(ExceptionDetails.Tables["ExceptionDetails"].Rows[0]["te_value"]) / 100).ToString("N");
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

        public int PaySDGTicket(string BarCode, bool RedeemExpiredTicket, ref RTOnlineTicketDetail TicketDetail)
        {
            try
            {
                var iAmount = DataBaseServiceHandler.AddParameter<int>("iAmount", DbType.Int32, 0, ParameterDirection.Output);
                var iResult = DataBaseServiceHandler.AddParameter<int>("iResult", DbType.Int32, 0, ParameterDirection.Output);
                var sBarcode = DataBaseServiceHandler.AddParameter<string>("sBarcode", DbType.String, "", ParameterDirection.Output);
                var iTicketType = DataBaseServiceHandler.AddParameter<int>("iTicketType", DbType.Int32, 0, ParameterDirection.Output);
                var strHashed = DataBaseServiceHandler.AddParameter<byte>("strHashed", DbType.Byte, 0x0000002D, ParameterDirection.Output);
                var dPrinted = DataBaseServiceHandler.AddParameter<DateTime>("dPrinted", DbType.DateTime, DateTime.Now, ParameterDirection.Output);

                DataBaseServiceHandler.ExecuteNonQuery(CommonDataAccess.TicketingConnectionString, CommandType.StoredProcedure, "pPaySDGTicket",
                    DataBaseServiceHandler.AddParameter<string>("strBarcode", DbType.String, BarCode),
                    DataBaseServiceHandler.AddParameter<bool>("RedeemExpiredTicket", DbType.Boolean, RedeemExpiredTicket),
                    DataBaseServiceHandler.AddParameter<string>("strDeviceID", DbType.String, TicketDetail.RedeemedMachine),
                    DataBaseServiceHandler.AddParameter<int>("@AuthorizedUser_No", DbType.Int16, TicketDetail.AuthorizedUser_No),
                    DataBaseServiceHandler.AddParameter<DateTime>("@Authorized_Date", DbType.DateTime, TicketDetail.Authorized_Date == DateTime.MinValue? DateTime.Parse("1800-1-1 0:0:0"): TicketDetail.Authorized_Date),
                    DataBaseServiceHandler.AddParameter<Int64>("@Customer_Id", DbType.Int64, TicketDetail.CustomerId),
                    DataBaseServiceHandler.AddParameter<string>("@ClientSiteCode", DbType.String, TicketDetail.ClientSiteCode),
                    iAmount, iResult, sBarcode, iTicketType, strHashed, dPrinted);

                TicketDetail.TicketValue = Convert.ToInt32(iAmount.Value);
                return Convert.ToInt32(iResult.Value);
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
                return -99;
            }
        }
        public int PaySDGTicketCage(string BarCode, bool RedeemExpiredTicket, ref RTOnlineTicketDetail TicketDetail)
        {
            try
            {
                var iAmount = DataBaseServiceHandler.AddParameter<int>("iAmount", DbType.Int32, 0, ParameterDirection.Output);
                var iResult = DataBaseServiceHandler.AddParameter<int>("iResult", DbType.Int32, 0, ParameterDirection.Output);
                var sBarcode = DataBaseServiceHandler.AddParameter<string>("sBarcode", DbType.String, "", ParameterDirection.Output);
                var iTicketType = DataBaseServiceHandler.AddParameter<int>("iTicketType", DbType.Int32, 0, ParameterDirection.Output);
                var strHashed = DataBaseServiceHandler.AddParameter<byte>("strHashed", DbType.Byte, 0x0000002D, ParameterDirection.Output);
                var dPrinted = DataBaseServiceHandler.AddParameter<DateTime>("dPrinted", DbType.DateTime, DateTime.Now, ParameterDirection.Output);

                DataBaseServiceHandler.ExecuteNonQuery(CommonDataAccess.TicketingConnectionString, CommandType.StoredProcedure, "pPaySDGTicket",
                    DataBaseServiceHandler.AddParameter<string>("strBarcode", DbType.String, BarCode),
                    DataBaseServiceHandler.AddParameter<bool>("RedeemExpiredTicket", DbType.Boolean, RedeemExpiredTicket),
                    DataBaseServiceHandler.AddParameter<string>("strDeviceID", DbType.String, System.Environment.MachineName+"_Cage"),
                    DataBaseServiceHandler.AddParameter<int>("@AuthorizedUser_No", DbType.Int16, TicketDetail.AuthorizedUser_No),
                    DataBaseServiceHandler.AddParameter<DateTime>("@Authorized_Date", DbType.DateTime, TicketDetail.Authorized_Date == DateTime.MinValue ? DateTime.Now.DBMinValue() : TicketDetail.Authorized_Date),
                    DataBaseServiceHandler.AddParameter<Int64>("@Customer_Id", DbType.Int64, TicketDetail.CustomerId),
                    DataBaseServiceHandler.AddParameter<string>("@ClientSiteCode", DbType.String, Settings.SiteCode),
                    iAmount, iResult, sBarcode, iTicketType, strHashed, dPrinted);

                TicketDetail.TicketValue = Convert.ToInt32(iAmount.Value);
                return Convert.ToInt32(iResult.Value);
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
                return -99;
            }
        }

        public int pCloseSDGTicket(string Barcode,RTOnlineTicketDetail TicketDetail)// string BarCode,string sClientSiteCode)
        {
            try
            {
                var iResult = DataBaseServiceHandler.AddParameter<int>("iResult", DbType.Int32, 0, ParameterDirection.Output);
                //New parameter to get the ivoucherid
                var iVoucherID = DataBaseServiceHandler.AddParameter<int>("VoucherID", DbType.Int32, 0, ParameterDirection.Output);

                DataBaseServiceHandler.ExecuteNonQuery(CommonDataAccess.TicketingConnectionString, CommandType.StoredProcedure, "pCloseSDGTicket",
                    DataBaseServiceHandler.AddParameter<string>("strBarcode", DbType.String, Barcode),
                    DataBaseServiceHandler.AddParameter<string>("strDeviceID", DbType.String, TicketDetail.RedeemedMachine),//System.Environment.MachineName),
                    DataBaseServiceHandler.AddParameter<string>("@RedeemedUser", DbType.String, (Security.SecurityHelper.CurrentUser == null) ? "NONE" : Security.SecurityHelper.CurrentUser.UserName),
                    DataBaseServiceHandler.AddParameter<int>("iStatus", DbType.Int32, 1),
                    DataBaseServiceHandler.AddParameter<int>("Error_Code", DbType.Int32, 0),
                    DataBaseServiceHandler.AddParameter<string>("ClientSiteCode", DbType.String, TicketDetail.ClientSiteCode)//sClientSiteCode)
                    , iResult, iVoucherID);


                if (Convert.ToInt32(iResult.Value) == 0)
                    DataBaseServiceHandler.ExecuteNonQuery(CommonDataAccess.ExchangeConnectionString, CommandType.StoredProcedure, "usp_UpdateTicketExceptionOnCloseSDG",
                        DataBaseServiceHandler.AddParameter<string>("TE_TicketNumber", DbType.String, Barcode),
                        DataBaseServiceHandler.AddParameter<int>("TE_ID", DbType.Int32, 0),
                        DataBaseServiceHandler.AddParameter<string>("TE_Status_Final_Actual", DbType.String, "CLAIMED"));
                      
                
                return Convert.ToInt32(iResult.Value);
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
                return -99;
            }
        }
        public int pCloseSDGTicketCage(string BarCode)
        {
            try
            {
                var iResult = DataBaseServiceHandler.AddParameter<int>("iResult", DbType.Int32, 0, ParameterDirection.Output);
                var iVoucherID = DataBaseServiceHandler.AddParameter<int>("VoucherID", DbType.Int32, 0, ParameterDirection.Output);

                DataBaseServiceHandler.ExecuteNonQuery(CommonDataAccess.TicketingConnectionString, CommandType.StoredProcedure, "pCloseSDGTicket",
                    DataBaseServiceHandler.AddParameter<string>("strBarcode", DbType.String, BarCode),
                    DataBaseServiceHandler.AddParameter<string>("strDeviceID", DbType.String, System.Environment.MachineName+"_Cage"),
                    DataBaseServiceHandler.AddParameter<string>("@RedeemedUser", DbType.String, Security.SecurityHelper.CurrentUser.UserName),
                    DataBaseServiceHandler.AddParameter<int>("iStatus", DbType.Int32, 1),
                    DataBaseServiceHandler.AddParameter<int>("Error_Code", DbType.Int32, 0),
                    DataBaseServiceHandler.AddParameter<string>("ClientSiteCode", DbType.String, Settings.SiteCode)//sClientSiteCode)
                    , iResult, iVoucherID);


                if (Convert.ToInt32(iResult.Value) == 0)
                    SqlHelper.ExecuteNonQuery(CommonDataAccess.ExchangeConnectionString, CommandType.Text,
                        "UPDATE dbo.Ticket_Exception SET TE_Status_Final_Actual = 'CLAIMED' WHERE TE_TicketNumber = " + BarCode);

                return Convert.ToInt32(iResult.Value);
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
                return -99;
            }
        }
        public bool CheckforTicketException(string BarCode)
        {
            try
            {
                SqlParameter[] oparams = new SqlParameter[2];

                SqlParameter param = new SqlParameter("Barcode", SqlDbType.VarChar, 32);
                param.Value = BarCode;
                oparams[0] = param;

                SqlParameter paramRet = new SqlParameter();
                paramRet.ParameterName = "@RETURN_VALUE";
                paramRet.Direction = ParameterDirection.ReturnValue;
                oparams[1] = paramRet;

                SqlHelper.ExecuteNonQuery(CommonDataAccess.ExchangeConnectionString, CommandType.StoredProcedure, "rsp_CheckForTicketException", oparams);

                if (Convert.ToInt32(oparams[1].Value) == 1)
                    return true;

                return false;
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
                return false;
            }
        }

        #endregion

        #region "Public Static Function"

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

                SqlParameter[] objParams = new SqlParameter[5];
                objParams[0] = new SqlParameter(DBConstants.CONST_PARAM_STARTDATE, date.Add(tNegSpan));//Need it in this format --> "dd mmm yyyy hh:nn:ss" Renjish
                objParams[1] = new SqlParameter(DBConstants.CONST_PARAM_ENDDATE, date.Add(tPosSpan));//Need it in this format --> "dd mmm yyyy hh:nn:ss" Renjish
                objParams[2] = new SqlParameter(DBConstants.CONST_PARAM_TYPE, "A");
                objParams[3] = new SqlParameter(DBConstants.CONST_PARAM_BARCODE, RTOReceiptDetail.TicketString);
                objParams[4] = new SqlParameter(DBConstants.CONST_PARAM_LIABILITY, 0);

                SqlHelper.FillDataset(CommonDataAccess.GetTicketingConnectionString(strcon), DBConstants.CONST_RSP_BGS_VOUCHERINFORMATION, TicketInfo, strArray, objParams);

                if (TicketInfo.Tables.Count > 0)
                {
                    if (TicketInfo.Tables["TicketInfo"].Rows.Count > 0)
                    {
                        RTOReceiptDetail.PrintDevice = TicketInfo.Tables["TicketInfo"].Rows[0]["PrintDevice"].ToString();
                        RTOReceiptDetail.DatePrinted = (TicketInfo.Tables["TicketInfo"].Rows[0]["dtPrinted"]).ToString().ReadDate();
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
                //string strcon = "";

                SqlParameter[] objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter(DBConstants.CONST_PARAM_MACHINE, RTOReceiptDetail.PrintDevice);

                SqlHelper.FillDataset(CommonDataAccess.ExchangeConnectionString, DBConstants.CONST_RSP_GETMACHINEDETAILSFROMASSET, MachineDetails, strArray, objParams);

                if (MachineDetails.Tables.Count > 0)
                {
                    if (MachineDetails.Tables["MachineDetails"].Rows.Count > 0)
                    {
                        RTOReceiptDetail.MachineClassName = MachineDetails.Tables["MachineDetails"].Rows[0]["Name"].ToString();
                        RTOReceiptDetail.DeviceBarPosition = MachineDetails.Tables["MachineDetails"].Rows[0]["Bar_Pos_Name"].ToString();
                        RTOReceiptDetail.DeviceName = MachineDetails.Tables["MachineDetails"].Rows[0]["Stock_No"].ToString();
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

        //public int GetRedeemTicketAmount(string ticketString, out int ticketStatus)
        //{
        //    int ticketAmount = 0;
        //    ticketStatus = 0;

        //    try
        //    {
        //        SqlParameter[] sqlParam = new SqlParameter[4];
        //        sqlParam[0] = new SqlParameter(DBConstants.CONST_PARAM_BARCODE, ticketString);
        //        sqlParam[1] = new SqlParameter(DBConstants.CONST_PARAM_DEVICEID, System.Environment.MachineName);
        //        sqlParam[2] = new SqlParameter(DBConstants.CONST_PARAM_TICKETSTATUS, ticketStatus);
        //        sqlParam[2].Direction = ParameterDirection.Output;
        //        sqlParam[3] = new SqlParameter(DBConstants.CONST_PARAM_TICKETAMOUNT, ticketAmount);
        //        sqlParam[3].Direction = ParameterDirection.Output;

        //        SqlHelper.ExecuteNonQuery(CommonDataAccess.TicketingConnectionString, CommandType.StoredProcedure, DBConstants.CONST_RSP_GETREDEEMTICKETAMOUNT, sqlParam);                
        //        ticketStatus = Convert.ToInt32(sqlParam[2].Value);
        //        ticketAmount = Convert.ToInt32(sqlParam[3].Value);
        //    }
        //    catch (Exception ex)
        //    {
        //        ExceptionManager.Publish(ex);                
        //    }

        //    return ticketAmount;
        //}

        public RTOnlineTicketDetail GetRedeemTicketAmount(RTOnlineTicketDetail TicketDetailEntity)
        {
            int ticketAmount = 0;
            TicketDetailEntity.TicketStatusCode = 0;

            try
            {
                SqlParameter[] sqlParam = new SqlParameter[8];
                sqlParam[0] = new SqlParameter(DBConstants.CONST_PARAM_BARCODE, TicketDetailEntity.TicketString);
                sqlParam[1] = new SqlParameter(DBConstants.CONST_PARAM_DEVICEID, System.Environment.MachineName);
                sqlParam[2] = new SqlParameter("@ClientSiteCode", TicketDetailEntity.ClientSiteCode);
                sqlParam[3] = new SqlParameter(DBConstants.CONST_PARAM_TICKETSTATUS, TicketDetailEntity.TicketStatusCode);
                sqlParam[3].Direction = ParameterDirection.Output;
                sqlParam[4] = new SqlParameter(DBConstants.CONST_PARAM_TICKETAMOUNT, ticketAmount);
                sqlParam[4].Direction = ParameterDirection.Output;
                sqlParam[5] = new SqlParameter("@PrintedDevice", SqlDbType.VarChar,50);
                sqlParam[5].Direction = ParameterDirection.Output;
                sqlParam[6] = new SqlParameter("@PrintedDate", TicketDetailEntity.PrintedDate);
                sqlParam[6].Direction = ParameterDirection.Output;
                sqlParam[7] = new SqlParameter("@iVoucherID", TicketDetailEntity.iVoucherid);
                sqlParam[7].Direction = ParameterDirection.Output;
                

                SqlHelper.ExecuteNonQuery(CommonDataAccess.TicketingConnectionString, CommandType.StoredProcedure, DBConstants.CONST_RSP_GETREDEEMTICKETAMOUNT, sqlParam);
                TicketDetailEntity.TicketStatusCode = Convert.ToInt32(sqlParam[3].Value);
                if (!DBNull.Value.Equals(sqlParam[4].Value))
                {
                    ticketAmount = Convert.ToInt32(sqlParam[4].Value);
                }
                else if (DBNull.Value.Equals(sqlParam[4].Value))
                {
                    ticketAmount = 0;
                }
                TicketDetailEntity.RedeemedAmount = Convert.ToDecimal(ticketAmount);
                TicketDetailEntity.PrintedDevice=Convert.ToString(sqlParam[5].Value);
                if (!DBNull.Value.Equals(sqlParam[6].Value))
                {
                    TicketDetailEntity.PrintedDate = Convert.ToDateTime(sqlParam[6].Value);
                }
                else if (DBNull.Value.Equals(sqlParam[6].Value))
                {
                    TicketDetailEntity.PrintedDate = DateTime.MinValue;
                }
                if (!DBNull.Value.Equals(sqlParam[7].Value))
                {
                    TicketDetailEntity.iVoucherid = Convert.ToInt32(sqlParam[7].Value);
                }
                else if (DBNull.Value.Equals(sqlParam[7].Value))
                {
                    TicketDetailEntity.iVoucherid = 0;
                }
                
              

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

            //return ticketAmount;
            return TicketDetailEntity;
        }

        public int CheckSDGOfflineTicket(string ticketString)
        {
            int ticketStatus = 0;

            try
            {
                SqlParameter[] sqlParam = new SqlParameter[2];
                sqlParam[0] = new SqlParameter("strBarcode", ticketString);
                sqlParam[1] = new SqlParameter("iStatus", ticketStatus);
                sqlParam[1].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteNonQuery(CommonDataAccess.ExchangeConnectionString, CommandType.StoredProcedure, DBConstants.CONST_USP_CHECKSITECODEFOROFFLINEVOUCHERREDEEM, sqlParam);
                ticketStatus = Convert.ToInt32(sqlParam[1].Value);
                
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);                
            }

            return ticketStatus;
        }

        public string GetTicketPrintDevice(string strbarcode,out DateTime PrintDate )
        {
            PrintDate = DateTime.Now; 
            string strBar_Pos=string.Empty ; 
            try
            {
                SqlParameter[] sqlParam = new SqlParameter[2];
                sqlParam[0] = new SqlParameter("strBarcode", strbarcode);
                sqlParam[1] = new SqlParameter("PrintDate", PrintDate);
                sqlParam[1].Direction = ParameterDirection.Output;
                strBar_Pos=  SqlHelper.ExecuteScalar(CommonDataAccess.ExchangeConnectionString, CommandType.StoredProcedure, "rsp_GetTicketPrintDevice", sqlParam).ToString() ;
                PrintDate = DateTime.Parse(sqlParam[1].Value.ToString() );
            }
            catch (Exception ex)
            {
                
                ExceptionManager.Publish(ex);                
            }
            return strBar_Pos;
            
        }


        public string GetVoucherDetailsToExport(int iVoucherID)
        {
            string strStoredProcedureName = "rsp_GetVoucherDetailsToExportToSite";
            string strXMLData = string.Empty;

            try
            {
                SqlParameter[] oSQLParam = new SqlParameter[1];
                SqlParameter oParam = new SqlParameter();
                oParam.ParameterName = "VoucherID";
                oParam.Value = iVoucherID;
                oParam.Direction = ParameterDirection.Input;
                oSQLParam[0] = oParam;

                strXMLData = SqlHelper.ExecuteScalar(CommonDataAccess.TicketingConnectionString, strStoredProcedureName, oSQLParam).ToString();
            }
            catch (Exception exCheckDataToExport)
            {
                strXMLData = string.Empty;
                ExceptionManager.Publish(exCheckDataToExport);
            }

            return strXMLData;
        }

        public string GetVoucherDetailsForCrossTicketing(string Barcode)
        {
            string strStoredProcedureName = "rsp_GetVoucherForCrossTicketing";
            string strXMLData = string.Empty;

            try
            {
                SqlParameter[] oSQLParam = new SqlParameter[1];
                SqlParameter oParam = new SqlParameter();
                oParam.ParameterName = "BarCode";
                oParam.Value = Barcode;
                oParam.Direction = ParameterDirection.Input;
                oSQLParam[0] = oParam;

                strXMLData = SqlHelper.ExecuteScalar(CommonDataAccess.TicketingConnectionString, strStoredProcedureName, oSQLParam).ToString();
                LogManager.WriteLog("GetVoucherDetailsForCrossTicketing : " + Convert.ToString(strXMLData), LogManager.enumLogLevel.Info);
            }
            catch (Exception ex)
            {
                strXMLData = string.Empty;
                LogManager.WriteLog("GetVoucherDetailsForCrossTicketing : " + ex.Message, LogManager.enumLogLevel.Info);
            }

            return strXMLData;
        }

        #endregion

        #region CrossTicketing Methods

        public void UpdateLiabilityStatus(string Barcode, string SiteCode, string Status)
        {
            string strStoredProcedureName = "usp_UpdateVoucherStatus";
            try
            {
                SqlParameter[] oSQLParam = new SqlParameter[3];
                SqlParameter oParam = new SqlParameter();
                oParam.ParameterName = "Barcode";
                oParam.Value = Barcode;
                oParam.Direction = ParameterDirection.Input;
                oSQLParam[0] = oParam;

                oParam = new SqlParameter();
                oParam.ParameterName = "Status";
                oParam.Value = Status;
                oParam.Direction = ParameterDirection.Input;
                oSQLParam[1] = oParam;

                oParam = new SqlParameter();
                oParam.ParameterName = "SiteCode";
                oParam.Value = SiteCode;
                oParam.Direction = ParameterDirection.Input;
                oSQLParam[2] = oParam;

                SqlHelper.ExecuteNonQuery(CommonDataAccess.TicketingConnectionString, CommandType.StoredProcedure, strStoredProcedureName, oSQLParam);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

        }

        public void CreatePayDeviceID(string StockNo)
        {
            string strStoredProcedureName = "usp_CreateDeviceID";

            try
            {
                SqlParameter[] oSQLParam = new SqlParameter[1];
                SqlParameter oParam = new SqlParameter();
                oParam.ParameterName = "StockNo";
                oParam.Value = StockNo;
                oParam.Direction = ParameterDirection.Input;
                oSQLParam[0] = oParam;

                SqlHelper.ExecuteNonQuery(CommonDataAccess.TicketingConnectionString, CommandType.StoredProcedure, strStoredProcedureName, oSQLParam);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        public bool ImportVoucherDetails(RTOnlineTicketDetail TicketDetail)
        {
            bool bSuccess = false;
            try
            {
                SqlParameter[] oSQLParams = new SqlParameter[5];

                SqlParameter objSQLParam = new SqlParameter();
                objSQLParam.ParameterName = "doc";
                objSQLParam.Value = TicketDetail.VoucherXMLData;
                objSQLParam.Direction = ParameterDirection.Input;
                oSQLParams[0] = objSQLParam;

                objSQLParam = new SqlParameter();
                objSQLParam.ParameterName = "DeviceName";
                objSQLParam.Value = System.Environment.MachineName + ((Settings.CAGE_ENABLED)?"_Cage" : string.Empty ) ;
                objSQLParam.Direction = ParameterDirection.Input;
                oSQLParams[1] = objSQLParam;

                objSQLParam = new SqlParameter();
                objSQLParam.ParameterName = "PrintedDeviceName";
                objSQLParam.Value = TicketDetail.PrintedDevice;
                objSQLParam.Direction = ParameterDirection.Input;
                oSQLParams[2] = objSQLParam;

                objSQLParam = new SqlParameter();
                objSQLParam.ParameterName = "RedeemUserName";
                objSQLParam.Value = TicketDetail.RedeemedUser;
                objSQLParam.Direction = ParameterDirection.Input;
                oSQLParams[3] = objSQLParam;

                objSQLParam = new SqlParameter();
                objSQLParam.ParameterName = "IsSuccess";
                objSQLParam.Direction = ParameterDirection.Output;
                objSQLParam.SqlDbType = SqlDbType.Int;
                oSQLParams[4] = objSQLParam;

                LogManager.WriteLog("<CrossTicketing> ImportVoucherDetails XML:" + TicketDetail.VoucherXMLData + "\n Printed Device: " + TicketDetail.PrintedDevice, LogManager.enumLogLevel.Info);

                SqlHelper.ExecuteNonQuery(CommonDataAccess.TicketingConnectionString, CommandType.StoredProcedure, "RSP_IMPORTVOUCHERDETAILS_CAGE", oSQLParams);

                if (int.Parse(oSQLParams[4].Value.ToString()) == 0)
                {
                    bSuccess = true;
                    LogManager.WriteLog("<CrossTicketing>   ImportVoucherDetails Call Success", LogManager.enumLogLevel.Info);
                }
                else
                {
                    bSuccess = false;
                    LogManager.WriteLog("<CrossTicketing>   ImportVoucherDetails Call " + "  failed due to " + oSQLParams[1].Value.ToString(), LogManager.enumLogLevel.Info);
                }
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                bSuccess = false;
            }

            return bSuccess;
        }

        public ReedeemTicketDetailsComms RedeemTicketStartComms(ReedeemTicketDetailsComms TicketDetailComms)
        {

            try
            {
                SqlParameter[] oSQLParams = new SqlParameter[7];

                SqlParameter objSQLParam = new SqlParameter();
                objSQLParam.ParameterName = "Barcode";
                objSQLParam.Value = TicketDetailComms.Barcode;
                objSQLParam.Direction = ParameterDirection.Input;
                oSQLParams[0] = objSQLParam;

                LogManager.WriteLog("<CrossTicketing>   Esp_redeemticketstart Barcode: " + TicketDetailComms.Barcode, LogManager.enumLogLevel.Info);

                objSQLParam = new SqlParameter();
                objSQLParam.ParameterName = "DeviceID";
                objSQLParam.Value = TicketDetailComms.DeviceId;
                objSQLParam.Direction = ParameterDirection.Input;
                oSQLParams[1] = objSQLParam;

                LogManager.WriteLog("<CrossTicketing>   Esp_redeemticketstart DeviceID: " + TicketDetailComms.DeviceId, LogManager.enumLogLevel.Info);

                objSQLParam = new SqlParameter();
                objSQLParam.ParameterName = "ClSiteCode";
                objSQLParam.Value = TicketDetailComms.ClientSiteCode;
                objSQLParam.Direction = ParameterDirection.Input;
                oSQLParams[2] = objSQLParam;

                LogManager.WriteLog("<CrossTicketing>   Esp_redeemticketstart ClSiteCode: " + TicketDetailComms.ClientSiteCode, LogManager.enumLogLevel.Info);

                objSQLParam = new SqlParameter();
                objSQLParam.ParameterName = "retAmount";
                objSQLParam.Value = TicketDetailComms.retAmount;
                objSQLParam.Direction = ParameterDirection.Output;
                objSQLParam.SqlDbType = SqlDbType.Int;
                oSQLParams[3] = objSQLParam;

                LogManager.WriteLog("<CrossTicketing>   Esp_redeemticketstart retAmount: " + TicketDetailComms.retAmount, LogManager.enumLogLevel.Info);

                objSQLParam = new SqlParameter();
                objSQLParam.ParameterName = "retResult";
                objSQLParam.Direction = ParameterDirection.Output;
                objSQLParam.SqlDbType = SqlDbType.Int;
                oSQLParams[4] = objSQLParam;

                objSQLParam = new SqlParameter();
                objSQLParam.ParameterName = "retBarcode";
                objSQLParam.Value = string.Empty;
                objSQLParam.Direction = ParameterDirection.Output;
                objSQLParam.SqlDbType = SqlDbType.VarChar;
                oSQLParams[5] = objSQLParam;

                objSQLParam = new SqlParameter();
                objSQLParam.ParameterName = "retTicketType";
                objSQLParam.Direction = ParameterDirection.Output;
                objSQLParam.SqlDbType = SqlDbType.Int;
                oSQLParams[6] = objSQLParam;

                SqlHelper.ExecuteNonQuery(CommonDataAccess.ExchangeConnectionString, CommandType.StoredProcedure, "Esp_redeemticketstart", oSQLParams);

                bool bResult;
                int nretAmount, nretResult, nretTicketType;

                
                bResult = int.TryParse(Convert.ToString(oSQLParams[3].Value), out nretAmount);
                LogManager.WriteLog("<CrossTicketing>   Esp_redeemticketstart returns nretAmount: " + nretAmount.ToString(), LogManager.enumLogLevel.Info);
                bResult = int.TryParse(Convert.ToString(oSQLParams[4].Value), out nretResult);
                LogManager.WriteLog("<CrossTicketing>   Esp_redeemticketstart returns nretResult: " + nretResult.ToString(), LogManager.enumLogLevel.Info);

                TicketDetailComms.retAmount=  nretAmount;
                TicketDetailComms.retResult = nretResult;
                //TicketDetailComms.retBarcode = Convert.ToString(oSQLParams[5].Value);
                //LogManager.WriteLog("<CrossTicketing>   Esp_redeemticketstart returns retBarcode: " + TicketDetailComms.retBarcode, LogManager.enumLogLevel.Info);
                //TicketDetailComms.retTicketType = int.Parse(oSQLParams[6].Value.ToString());
                bResult = int.TryParse(Convert.ToString(oSQLParams[6].Value), out nretTicketType);
                TicketDetailComms.retTicketType = bResult ? nretTicketType : 0;
                LogManager.WriteLog("<CrossTicketing>   Esp_redeemticketstart returns retTicketType: " + TicketDetailComms.retTicketType, LogManager.enumLogLevel.Info);

                LogManager.WriteLog("<CrossTicketing>   Esp_redeemticketstart returns: " + TicketDetailComms.retResult.ToString(), LogManager.enumLogLevel.Info);

            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

            return TicketDetailComms;
        }


        public ReedeemTicketDetailsComms RedeemTicketCompleteComms(ReedeemTicketDetailsComms TicketDetailComms)
        {
            //Add additional parameter to update clientsitecode from client

            try
            {
                SqlParameter[] oSQLParams = new SqlParameter[5];

                SqlParameter objSQLParam = new SqlParameter();
                objSQLParam.ParameterName = "Barcode";
                objSQLParam.Value = TicketDetailComms.Barcode;
                objSQLParam.Direction = ParameterDirection.Input;
                oSQLParams[0] = objSQLParam;

                objSQLParam = new SqlParameter();
                objSQLParam.ParameterName = "DeviceID";
                objSQLParam.Value = TicketDetailComms.DeviceId;
                objSQLParam.Direction = ParameterDirection.Input;
                oSQLParams[1] = objSQLParam;

                objSQLParam = new SqlParameter();
                objSQLParam.ParameterName = "retResult";
                objSQLParam.Direction = ParameterDirection.Output;
                objSQLParam.SqlDbType = SqlDbType.Int;
                oSQLParams[2] = objSQLParam;


                objSQLParam = new SqlParameter();
                objSQLParam.ParameterName = "iVoucherID";
                objSQLParam.Direction = ParameterDirection.Output;
                objSQLParam.SqlDbType = SqlDbType.Int;
                oSQLParams[3] = objSQLParam;

                objSQLParam = new SqlParameter();
                objSQLParam.ParameterName = "iClientSiteCode";
                objSQLParam.Value = TicketDetailComms.ClientSiteCode;
                objSQLParam.Direction = ParameterDirection.Input;
                objSQLParam.SqlDbType = SqlDbType.VarChar;
                oSQLParams[4] = objSQLParam;

                SqlHelper.ExecuteNonQuery(CommonDataAccess.ExchangeConnectionString, CommandType.StoredProcedure, "esp_RedeemTicketComplete", oSQLParams);

                LogManager.WriteLog("<CrossTicketing>   esp_RedeemTicketComplete after SP CALL", LogManager.enumLogLevel.Info);

                int nResult, nVoucherid;
                bool bResult = int.TryParse(Convert.ToString(oSQLParams[2].Value),out nResult);
                bResult= int.TryParse(Convert.ToString(oSQLParams[3].Value), out nVoucherid);

                TicketDetailComms.retResult = nResult;
                TicketDetailComms.iVoucherid = nVoucherid;

                LogManager.WriteLog("<CrossTicketing>   esp_RedeemTicketComplete returns: " + TicketDetailComms.retResult.ToString() + " | nVoucherid" + nVoucherid.ToString(), LogManager.enumLogLevel.Info);

            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

            return TicketDetailComms;
        }


        public ReedeemTicketDetailsComms RedeemTicketCancelComms(ReedeemTicketDetailsComms TicketDetailComms)
        {

            try
            {
                SqlParameter[] oSQLParams = new SqlParameter[5];

                SqlParameter objSQLParam = new SqlParameter();
                objSQLParam.ParameterName = "Barcode";
                objSQLParam.Value = TicketDetailComms.Barcode;
                objSQLParam.Direction = ParameterDirection.Input;
                oSQLParams[0] = objSQLParam;

                objSQLParam = new SqlParameter();
                objSQLParam.ParameterName = "DeviceID";
                objSQLParam.Value = TicketDetailComms.DeviceId;
                objSQLParam.Direction = ParameterDirection.Input;
                oSQLParams[1] = objSQLParam;

                objSQLParam = new SqlParameter();
                objSQLParam.ParameterName = "iError_Code";
                objSQLParam.Value = TicketDetailComms.iErrorCode;
                objSQLParam.Direction = ParameterDirection.Input;
                objSQLParam.SqlDbType = SqlDbType.Int;
                oSQLParams[2] = objSQLParam;

                objSQLParam = new SqlParameter();
                objSQLParam.ParameterName = "retResult";
                objSQLParam.Direction = ParameterDirection.Output;
                objSQLParam.SqlDbType = SqlDbType.Int;
                oSQLParams[3] = objSQLParam;

                objSQLParam = new SqlParameter();
                objSQLParam.ParameterName = "iClientSiteCode";
                objSQLParam.Value = TicketDetailComms.ClientSiteCode;
                objSQLParam.Direction = ParameterDirection.Input;
                objSQLParam.SqlDbType = SqlDbType.VarChar;
                oSQLParams[4] = objSQLParam;


                SqlHelper.ExecuteNonQuery(CommonDataAccess.ExchangeConnectionString, CommandType.StoredProcedure, "esp_RedeemTicketCancel", oSQLParams);

                TicketDetailComms.retResult = int.Parse(oSQLParams[3].Value.ToString());

                LogManager.WriteLog("<CrossTicketing>   esp_RedeemTicketCancel returns: " + TicketDetailComms.retResult.ToString(), LogManager.enumLogLevel.Info);

            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

            return TicketDetailComms;
        }

        public bool ImportVoucherDetailsComms(ReedeemTicketDetailsComms TicketDetail)
        {
            bool bSuccess = false;
            try
            {
                SqlParameter[] oSQLParams = new SqlParameter[4];

                SqlParameter objSQLParam = new SqlParameter();
                objSQLParam.ParameterName = "doc";
                objSQLParam.Value = TicketDetail.VoucherXMLData;
                objSQLParam.Direction = ParameterDirection.Input;
                oSQLParams[0] = objSQLParam;

                objSQLParam = new SqlParameter();
                objSQLParam.ParameterName = "DeviceName";
                objSQLParam.Value = TicketDetail.DeviceId;
                objSQLParam.Direction = ParameterDirection.Input;
                oSQLParams[1] = objSQLParam;


                objSQLParam = new SqlParameter();
                objSQLParam.ParameterName = "RedeemUserName";
                objSQLParam.Value = "";
                objSQLParam.Direction = ParameterDirection.Input;
                oSQLParams[2] = objSQLParam;

                objSQLParam = new SqlParameter();
                objSQLParam.ParameterName = "IsSuccess";
                objSQLParam.Direction = ParameterDirection.Output;
                objSQLParam.SqlDbType = SqlDbType.Int;
                oSQLParams[3] = objSQLParam;

         

                SqlHelper.ExecuteNonQuery(CommonDataAccess.TicketingConnectionString, CommandType.StoredProcedure, "RSP_IMPORTVOUCHERDETAILS", oSQLParams);

                if (int.Parse(oSQLParams[3].Value.ToString()) == 0)
                {
                    bSuccess = true;
                    LogManager.WriteLog("<CrossTicketing>   ImportVoucherDetails Call Success", LogManager.enumLogLevel.Info);
                }
                else
                {
                    bSuccess = false;
                    LogManager.WriteLog("<CrossTicketing>   ImportVoucherDetails Call " + "  failed due to " + oSQLParams[1].Value.ToString(), LogManager.enumLogLevel.Info);
                }
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                bSuccess = false;
            }

            return bSuccess;
        }
        #endregion

        #region GCD
        public bool CancelRedeemTicket(string BarCode)
        {
            bool RetVal = true;
            try
            {
                if (DataBaseServiceHandler.ExecuteNonQuery(CommonDataAccess.TicketingConnectionString, CommandType.StoredProcedure, "pCancelSDGTicket",
                     DataBaseServiceHandler.AddParameter<string>("strBarcode", DbType.String, BarCode)) < 0)
                {
                    RetVal = false;
                }

            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
                RetVal = false;
            }
            return RetVal;
        }
        #endregion

        #region MultipleVouchersRedeem

        

        public RTOnlineTicketDetail GetVoucherAmountAndStatusForMultipleTicket(RTOnlineTicketDetail TicketDetailEntity)
        {
            try
            {
                LogManager.WriteLog("Inside GetVoucherAmountAndStatusForMultipleTicket", LogManager.enumLogLevel.Info);
                LinqDataAccessDataContext _LinqDB = new LinqDataAccessDataContext(CommonDataAccess.TicketingConnectionString);
                List<rsp_ValidateVoucherForMultipleVoucherRedemptionResult> lstValidatedVoucherDetails = null;
                lstValidatedVoucherDetails = _LinqDB.ValidateVoucherForMultipleVoucherRedemption(TicketDetailEntity.TicketString).ToList();
                if (lstValidatedVoucherDetails != null)
                {
                    TicketDetailEntity.TicketStatusCode = lstValidatedVoucherDetails[0].iStatus;
                    TicketDetailEntity.TicketValue = Convert.ToDouble(lstValidatedVoucherDetails[0].Amount);
                    TicketDetailEntity.RedeemedAmount = lstValidatedVoucherDetails[0].Amount;
                    TicketDetailEntity.PrintedDate = lstValidatedVoucherDetails[0].PrintDate.Value;
                    TicketDetailEntity.iVoucherid = lstValidatedVoucherDetails[0].VoucherID;
                    LogManager.WriteLog("The Ticket details for the barcode :-- " + TicketDetailEntity.TicketString + "are" +
                                        " TicketStatusCode:-" + TicketDetailEntity.TicketStatusCode +
                                        "Ticket Amount :--" + TicketDetailEntity.TicketValue +
                                        "Voucher Id :--" + TicketDetailEntity.iVoucherid, LogManager.enumLogLevel.Info);
                       
                                

                }
                
            }
            catch (Exception ex)
            {

                ExceptionManager.Publish(ex);
            }
            return TicketDetailEntity;
        }


        

        #endregion MultipleVouchersRedeem

    }
}
