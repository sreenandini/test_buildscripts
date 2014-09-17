using BMC.CoreLib;
using BMC.CoreLib.Diagnostics;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Data.Linq.Mapping;

namespace BMC.ExComms.DataLayer.MSSQL
{
    public partial class ExCommsDataContext
    {
        public List<FloorFinancialsResult> GetFloorFinancials(int? installation_No)
        {
            try
            {
                using (ExCommsSQLDataAccess DataContext = this.GetDataContext())
                {
                    return DataContext.rsp_GetFloorFinancials(null, installation_No).ToList();
                }
            }
            catch (Exception ex)
            {
                Log.Exception(ex);
                return new List<FloorFinancialsResult>();
            }
        }

        public List<FloorFinancialsResult> GetFloorFinancials(int? datapak, int? installation_No)
        {
            try
            {
                using (ExCommsSQLDataAccess DataContext = this.GetDataContext())
                {
                    return DataContext.rsp_GetFloorFinancials(datapak, installation_No).ToList();
                }
            }
            catch (Exception ex)
            {
                Log.Exception(ex);
                return new List<FloorFinancialsResult>();
            }
        }

        public bool CreateTickeException_Handpay(int installation_No, int ticket_Value, string server, bool isHandpayResponse, string hP_Type, DateTime date)
        {
            try
            {
                using (ExCommsSQLDataAccess DataContext = this.GetDataContext())
                {
                    ModuleProc PROC = new ModuleProc("CreateTickeException_Handpay");
                    int? retResult = -1;
                    DataContext.usp_createTickeException_Handpay(installation_No, ticket_Value, server, isHandpayResponse, hP_Type, date, ref retResult);
                    if (retResult != 0)
                    {
                        Log.Info(PROC, "Unable to create handpay for 21/18 event - Installation No " + installation_No.ToString());
                        return false;
                    }

                    Log.Info(PROC, "Handpay Created for 21/18 event - Installation No " + installation_No.ToString());
                    return true;
                }
            }
            catch (Exception ex)
            {
                Log.Exception(ex);
                return false;
            }
        }

        public bool CreateTreasury_Handpay(int installation_No, int ticket_Value, string hP_Type)
        {
            try
            {
                using (ExCommsSQLDataAccess DataContext = this.GetDataContext())
                {
                    ModuleProc PROC = new ModuleProc("CreateTreasury_Handpay");
                    int? retResult = -1;
                    DataContext.usp_createTreasury_Handpay(installation_No, ticket_Value, hP_Type, ref retResult);
                    if (retResult != 0)
                    {
                        Log.Info(PROC, "Unable to create handpay for 21/243 event - Installation No " + installation_No.ToString());
                        return false;
                    }

                    Log.Info(PROC, "Handpay created for 21/243 event - Installation No " + installation_No.ToString());
                    return true;
                }
            }
            catch (Exception ex)
            {
                Log.Exception(ex);
                return false;
            }
        }

        public bool UpdateTicketExceptionResponseMeters(int installation_No, int handpay, int jackpot, int ticketOut)
        {
            try
            {
                using (ExCommsSQLDataAccess DataContext = this.GetDataContext())
                {
                    if (DataContext.usp_UpdateTicketExceptionResponseMeters(installation_No, handpay, jackpot, ticketOut) == 0)
                        return true;
                }
            }
            catch (Exception ex)
            {
                Log.Exception(ex);
            }
            return false;
        }

        public List<FloorFinancialSessionResult> UpdateFloorFinancialSession(int installation_No, string type, string voucher_ID)
        {
            try
            {
                using (ExCommsSQLDataAccess DataContext = this.GetDataContext())
                {
                    return DataContext.usp_UpdateFloorFinancialSession(installation_No, type, voucher_ID).ToList();
                }
            }
            catch (Exception ex)
            {
                Log.Exception(ex);
                return new List<FloorFinancialSessionResult>();
            }
        }

        public bool CreateTicketCompleteMC300(int installation_No, string barcode, int value, DateTime date, int seqNumber, int ticket_Type, DateTime printedDate)
        {
            try
            {
                using (ExCommsSQLDataAccess DataContext = this.GetDataContext())
                {
                    ModuleProc PROC = new ModuleProc("CreateTicketCompleteMC300");
                    int? retResult = -1;
                    DataContext.esp_CreateTicketCompleteMC300(installation_No, barcode, value, date, seqNumber, ticket_Type, printedDate, ref retResult);
                    if (retResult != 0)
                    {
                        Log.Info(PROC, "Unable to create ticket complete MC300 - Installation No " + installation_No.ToString());
                        return false;
                    }

                    Log.Info(PROC, "Created ticket complete MC300- Installation No " + installation_No.ToString());
                    return true;
                }
            }
            catch (Exception ex)
            {
                Log.Exception(ex);
                return false;
            }
        }

        public bool CreateTicketComplete(string barCode, string issuedUser, string redeemedUser)
        {
            try
            {
                using (ExCommsSQLDataAccess DataContext = this.GetDataContext())
                {
                    ModuleProc PROC = new ModuleProc("CreateTicketComplete");
                    int? retResult = -1;
                    DataContext.esp_CreateTicketComplete(barCode, issuedUser, redeemedUser, ref retResult);
                    if (retResult == 0)
                        return true;

                    Log.Info(PROC, "CreateTicketComplete returns " + retResult.ToString() + " for the barcode " + barCode.ToString());
                    return true;
                }
            }
            catch (Exception ex)
            {
                Log.Exception(ex);
                return false;
            }
        }

        public bool RedeemTicketComplete(string barCode, string deviceID, ref int? voucherID, string clientSiteCode, string playerCardNo)
        {
            try
            {
                using (ExCommsSQLDataAccess DataContext = this.GetDataContext())
                {
                    ModuleProc PROC = new ModuleProc("RedeemTicketComplete");
                    int? retResult = -1;
                    int result = DataContext.esp_RedeemTicketComplete(barCode, deviceID, ref retResult, ref voucherID, clientSiteCode, playerCardNo);
                    if (retResult == 0)
                        return true;

                    Log.Info(PROC, "RedeemTicketComplete returns " + retResult.ToString());
                    return true;
                }
            }
            catch (Exception ex)
            {
                Log.Exception(ex);
                return false;
            }
        }

        public bool CheckInstallationDetailsForComms(int installationNo)
        {
            bool bRet = false;

            try
            {
                InstallationDetailsForComms oInstallationDetailsForComms = GetInstallationDetailsForComms(installationNo);
                if (oInstallationDetailsForComms != null)
                    bRet = true;
            }
            catch (Exception ex)
            {
                Log.Exception(ex);
            }

            return bRet;
        }

        public bool CancelTicketCompleteMC300(int? installationNo, string ticketNumber)
        {
            bool bRet = false;

            try
            {
                using (ExCommsSQLDataAccess DataContext = this.GetDataContext())
                {
                    int? iRetResult = 0;
                    DataContext.CancelTicketCompleteMC300(installationNo, ticketNumber, ref iRetResult);
                    Log.Info("CancelTicketCompleteMC300 returns : " + iRetResult.SafeValue());

                    if (iRetResult > 0)
                    {
                        bRet = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Exception(ex);
            }

            return bRet;
        }

        public bool RedeemTicketStart(ref string barcode, string stock_No, string siteCode, int isTISPrintedTicket, string playerCardNo, ref int? retAmount, ref int? retResult, ref int? TicketType)
        {
            bool bRet = false;
            string retBarcode = string.Empty;

            try
            {
                using (ExCommsSQLDataAccess DataContext = this.GetDataContext())
                {
                    int? iRetResult = 0;
                    iRetResult = DataContext.RedeemTicketStart(barcode, stock_No, siteCode, isTISPrintedTicket, playerCardNo, ref retAmount, ref retResult, ref retBarcode, ref TicketType);

                    barcode = retBarcode;
                    bRet = (retResult == 0 && retAmount > 0);
                }
            }
            catch (Exception ex)
            {
                Log.Exception(ex);
                bRet = false;
            }

            return bRet;
        }

        public bool CreateTicketException(int? installationNo, int? ticketValue, string server, bool? isHandpayResponse, string hpType, DateTime? date, ref int? retResult)
        {
            bool bRet = false;

            try
            {
                using (ExCommsSQLDataAccess DataContext = this.GetDataContext())
                {
                    int? iRetResult = 0;

                    DataContext.usp_createTickeException_Handpay(installationNo, ticketValue, server, isHandpayResponse, hpType, date, ref retResult);

                    if ((iRetResult != 0) || (iRetResult != -1))
                    {
                        bRet = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Exception(ex);
            }

            return bRet;
        }

        public int GetLastTicketNumber(int installationNo)
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "GetLastTicketNumber"))
            {
                int? result = default(int?);

                try
                {
                    using (ExCommsSQLDataAccess dbContext = this.GetDataContext())
                    {
                        dbContext.rsp_GetLastTicketNumber(installationNo, ref result);
                    }
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }

                return result.SafeValue();
            }
        }

        public bool InsertTicket(int installation_ID, int? machine_Serial, int? datapak_Serial,
            int? ticket_Value, int? ticket_Number, DateTime? print_Time)
        {
            try
            {
                using (ExCommsSQLDataAccess DataContext = this.GetDataContext())
                {
                    ModuleProc PROC = new ModuleProc("InsertTicket");
                    int retResult = DataContext.usp_InsertTicket(installation_ID, machine_Serial, datapak_Serial, ticket_Value, ticket_Number, print_Time);
                    if (retResult != 0)
                    {
                        Log.Info(PROC, "Unable to create NOVO ticket complete MC300 - Installation No " + installation_ID.ToString());
                        return false;
                    }

                    Log.Info(PROC, "Created ticket complete NOVO - Installation No " + installation_ID.ToString());
                    return true;
                }
            }
            catch (Exception ex)
            {
                Log.Exception(ex);
                return false;
            }
        }

        public int UpdateTicketExceptionRequestMeters(int installationNo, int? handpay, int? jackpot, int? ticketOut)
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "UpdateTicketExceptionRequestMeters"))
            {
                int result = 0;

                try
                {
                    using (ExCommsSQLDataAccess dbContext = this.GetDataContext())
                    {
                        result = dbContext.usp_UpdateTicketExceptionRequestMeters(installationNo, handpay, jackpot, ticketOut);
                    }
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }

                return result;
            }
        }

        public int UpdateTicketExceptionResponseMeters(int installationNo, int? handpay, int? jackpot, int? ticketOut)
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "UpdateTicketExceptionResponseMeters"))
            {
                int result = 0;

                try
                {
                    using (ExCommsSQLDataAccess dbContext = this.GetDataContext())
                    {
                        result = dbContext.usp_UpdateTicketExceptionResponseMeters(installationNo, handpay, jackpot, ticketOut);
                    }
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }

                return result;
            }
        }
    }

    public partial class ExCommsSQLDataAccess : System.Data.Linq.DataContext
    {
        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.rsp_GetFloorFinancials")]
        public ISingleResult<FloorFinancialsResult> rsp_GetFloorFinancials([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Datapak_Serial", DbType = "Int")] System.Nullable<int> datapak_Serial, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Installation_No", DbType = "Int")] System.Nullable<int> installation_No)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), datapak_Serial, installation_No);
            return ((ISingleResult<FloorFinancialsResult>)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.usp_createTickeException_Handpay")]
        public int usp_createTickeException_Handpay([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Installation_No", DbType = "Int")] System.Nullable<int> installation_No, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Ticket_Value", DbType = "Int")] System.Nullable<int> ticket_Value, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType = "VarChar(50)")] string server, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType = "Bit")] System.Nullable<bool> isHandpayResponse, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "HP_Type", DbType = "VarChar(50)")] string hP_Type, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType = "DateTime")] System.Nullable<System.DateTime> date, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType = "Int")] ref System.Nullable<int> retResult)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), installation_No, ticket_Value, server, isHandpayResponse, hP_Type, date, retResult);
            retResult = ((System.Nullable<int>)(result.GetParameterValue(6)));
            return ((int)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.usp_createTreasury_Handpay")]
        public int usp_createTreasury_Handpay([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Installation_No", DbType = "Int")] System.Nullable<int> installation_No, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Ticket_Value", DbType = "Int")] System.Nullable<int> ticket_Value, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "HP_Type", DbType = "VarChar(50)")] string hP_Type, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType = "Int")] ref System.Nullable<int> retResult)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), installation_No, ticket_Value, hP_Type, retResult);
            retResult = ((System.Nullable<int>)(result.GetParameterValue(3)));
            return ((int)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.usp_UpdateFloorFinancialSession")]
        public ISingleResult<FloorFinancialSessionResult> usp_UpdateFloorFinancialSession([global::System.Data.Linq.Mapping.ParameterAttribute(DbType = "Int")] System.Nullable<int> installation_no, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Type", DbType = "VarChar(10)")] string type, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Voucher_ID", DbType = "VarChar(20)")] string voucher_ID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), installation_no, type, voucher_ID);
            return ((ISingleResult<FloorFinancialSessionResult>)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.esp_CreateTicketCompleteMC300")]
        public ISingleResult<TicketCompleteMC300Result> esp_CreateTicketCompleteMC300([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Installation_No", DbType = "Int")] System.Nullable<int> installation_No, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Barcode", DbType = "Char(32)")] string barcode, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Value", DbType = "Int")] System.Nullable<int> value, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Date", DbType = "DateTime")] System.Nullable<System.DateTime> date, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "SeqNumber", DbType = "Int")] System.Nullable<int> seqNumber, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Ticket_Type", DbType = "Int")] System.Nullable<int> ticket_Type, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "PrintedDate", DbType = "DateTime")] System.Nullable<System.DateTime> printedDate, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType = "Int")] ref System.Nullable<int> retResult)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), installation_No, barcode, value, date, seqNumber, ticket_Type, printedDate, retResult);
            retResult = ((System.Nullable<int>)(result.GetParameterValue(7)));
            return ((ISingleResult<TicketCompleteMC300Result>)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.esp_CreateTicketComplete")]
        public int esp_CreateTicketComplete([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Barcode", DbType = "VarChar(32)")] string barcode, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "IssuedUser", DbType = "Varchar(50)")] string issuedUser, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "RedeemedUser", DbType = "Varchar(50)")] string redeemedUser, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "RetResult", DbType = "Int")] ref System.Nullable<int> retResult)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), barcode, issuedUser, redeemedUser, retResult);
            return ((int)(result.ReturnValue));
        }

        //[global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.esp_RedeemTicketComplete")]
        //public void esp_RedeemTicketComplete([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Barcode", DbType = "VarChar(32)")] string barcode, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "DeviceID", DbType = "VarChar(50)")] string deviceID, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType = "Int")] ref System.Nullable<int> retResult, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType = "Int")] ref System.Nullable<int> iVoucherID, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType = "VarChar(10)")] string iClientSiteCode, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "PlayerCardNo", DbType = "VarChar(20)")] string playerCardNo)
        //{
        //    IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), barcode, deviceID, retResult, iVoucherID, iClientSiteCode, playerCardNo);
        //    retResult = ((System.Nullable<int>)(result.GetParameterValue(2)));
        //    iVoucherID = ((System.Nullable<int>)(result.GetParameterValue(3)));
        //}

        [Function(Name = "dbo.esp_RedeemTicketComplete")]
        public int esp_RedeemTicketComplete([Parameter(Name = "Barcode", DbType = "VarChar(32)")] string barcode, [Parameter(Name = "DeviceID", DbType = "VarChar(50)")] string deviceID, [Parameter(DbType = "Int")] ref System.Nullable<int> retResult, [Parameter(DbType = "Int")] ref System.Nullable<int> iVoucherID, [Parameter(DbType = "VarChar(10)")] string iClientSiteCode, [Parameter(Name = "PlayerCardNo", DbType = "VarChar(20)")] string playerCardNo)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), barcode, deviceID, retResult, iVoucherID, iClientSiteCode, playerCardNo);
            retResult = ((System.Nullable<int>)(result.GetParameterValue(2)));
            iVoucherID = ((System.Nullable<int>)(result.GetParameterValue(3)));
            return ((int)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.usp_CancelTicketCompleteMC300")]
        public int CancelTicketCompleteMC300([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "InstallationNo", DbType = "Int")] System.Nullable<int> installationNo, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "TicketNumber", DbType = "VarChar(18)")] string ticketNumber, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "RetVal", DbType = "Int")] ref System.Nullable<int> iRetResult)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), installationNo, ticketNumber, iRetResult);
            iRetResult = ((System.Nullable<int>)(result.GetParameterValue(2)));
            return ((int)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.esp_RedeemTicketStart")]
        public int RedeemTicketStart([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Barcode", DbType = "VarChar(32)")] string barcode, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "DeviceID", DbType = "VarChar(50)")] string deviceID, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "ClSiteCode", DbType = "VarChar(50)")] string clSiteCode, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "IsTISPrintedTicket", DbType = "Int")] System.Nullable<int> isTISPrintedTicket, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "PlayerCardNo", DbType = "VarChar(20)")] string playerCardNo, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType = "Int")] ref System.Nullable<int> retAmount, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType = "Int")] ref System.Nullable<int> retResult, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType = "VarChar(32)")] ref string retBarcode, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType = "Int")] ref System.Nullable<int> retTicketType)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), barcode, deviceID, clSiteCode, isTISPrintedTicket, playerCardNo, retAmount, retResult, retBarcode, retTicketType);
            retAmount = ((System.Nullable<int>)(result.GetParameterValue(5)));
            retResult = ((System.Nullable<int>)(result.GetParameterValue(6)));
            retBarcode = ((string)(result.GetParameterValue(7)));
            retTicketType = ((System.Nullable<int>)(result.GetParameterValue(8)));
            return ((int)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetLastTicketNumber")]
        public int rsp_GetLastTicketNumber([Parameter(Name = "GMUNo", DbType = "Int")] System.Nullable<int> gMUNo, [Parameter(Name = "LastTicketNo", DbType = "Int")] ref System.Nullable<int> lastTicketNo)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), gMUNo, lastTicketNo);
            lastTicketNo = ((System.Nullable<int>)(result.GetParameterValue(1)));
            return ((int)(result.ReturnValue));
        }

        [Function(Name = "dbo.usp_InsertTicket")]
        public int usp_InsertTicket([Parameter(Name = "Installation_ID", DbType = "Int")] System.Nullable<int> installation_ID, [Parameter(Name = "Machine_Serial", DbType = "Int")] System.Nullable<int> machine_Serial, [Parameter(Name = "Datapak_Serial", DbType = "Int")] System.Nullable<int> datapak_Serial, [Parameter(Name = "Ticket_Value", DbType = "Int")] System.Nullable<int> ticket_Value, [Parameter(Name = "Ticket_Number", DbType = "Int")] System.Nullable<int> ticket_Number, [Parameter(Name = "Print_Time", DbType = "DateTime")] System.Nullable<System.DateTime> print_Time)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), installation_ID, machine_Serial, datapak_Serial, ticket_Value, ticket_Number, print_Time);
            return ((int)(result.ReturnValue));
        }

        [Function(Name = "dbo.usp_UpdateTicketExceptionRequestMeters")]
        public int usp_UpdateTicketExceptionRequestMeters([Parameter(Name = "Installation", DbType = "Int")] System.Nullable<int> installation, [Parameter(Name = "Handpay", DbType = "Int")] System.Nullable<int> handpay, [Parameter(Name = "Jackpot", DbType = "Int")] System.Nullable<int> jackpot, [Parameter(Name = "TicketOut", DbType = "Int")] System.Nullable<int> ticketOut)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), installation, handpay, jackpot, ticketOut);
            return ((int)(result.ReturnValue));
        }

        [Function(Name = "dbo.usp_UpdateTicketExceptionResponseMeters")]
        public int usp_UpdateTicketExceptionResponseMeters([Parameter(Name = "Installation", DbType = "Int")] System.Nullable<int> installation, [Parameter(Name = "Handpay", DbType = "Int")] System.Nullable<int> handpay, [Parameter(Name = "Jackpot", DbType = "Int")] System.Nullable<int> jackpot, [Parameter(Name = "TicketOut", DbType = "Int")] System.Nullable<int> ticketOut)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), installation, handpay, jackpot, ticketOut);
            return ((int)(result.ReturnValue));
        }
    }

    public partial class FloorFinancialsResult
    {

        private System.Nullable<int> _Coins_In;

        private System.Nullable<int> _Coins_Out;

        private System.Nullable<int> _Games_Bet;

        private System.Nullable<int> _Games_Won;

        private System.Nullable<int> _Games_Lost;

        private System.Nullable<int> _Jackpot;

        private System.Nullable<int> _TicketOut;

        private System.Nullable<int> _Handpay;

        private System.Nullable<int> _BillsIn;

        private System.Nullable<int> _VouchersIn;

        private System.Nullable<int> _NoncashableEFTIN;

        private System.Nullable<int> _NonCashableEFTOut;

        private System.Nullable<int> _CashableEFTIn;

        private System.Nullable<int> _CashableEFTOut;

        public FloorFinancialsResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Coins_In", DbType = "Int")]
        public System.Nullable<int> Coins_In
        {
            get
            {
                return this._Coins_In;
            }
            set
            {
                if ((this._Coins_In != value))
                {
                    this._Coins_In = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Coins_Out", DbType = "Int")]
        public System.Nullable<int> Coins_Out
        {
            get
            {
                return this._Coins_Out;
            }
            set
            {
                if ((this._Coins_Out != value))
                {
                    this._Coins_Out = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Games_Bet", DbType = "Int")]
        public System.Nullable<int> Games_Bet
        {
            get
            {
                return this._Games_Bet;
            }
            set
            {
                if ((this._Games_Bet != value))
                {
                    this._Games_Bet = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Games_Won", DbType = "Int")]
        public System.Nullable<int> Games_Won
        {
            get
            {
                return this._Games_Won;
            }
            set
            {
                if ((this._Games_Won != value))
                {
                    this._Games_Won = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Games_Lost", DbType = "Int")]
        public System.Nullable<int> Games_Lost
        {
            get
            {
                return this._Games_Lost;
            }
            set
            {
                if ((this._Games_Lost != value))
                {
                    this._Games_Lost = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Jackpot", DbType = "Int")]
        public System.Nullable<int> Jackpot
        {
            get
            {
                return this._Jackpot;
            }
            set
            {
                if ((this._Jackpot != value))
                {
                    this._Jackpot = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_TicketOut", DbType = "Int")]
        public System.Nullable<int> TicketOut
        {
            get
            {
                return this._TicketOut;
            }
            set
            {
                if ((this._TicketOut != value))
                {
                    this._TicketOut = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Handpay", DbType = "Int")]
        public System.Nullable<int> Handpay
        {
            get
            {
                return this._Handpay;
            }
            set
            {
                if ((this._Handpay != value))
                {
                    this._Handpay = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_BillsIn", DbType = "Int")]
        public System.Nullable<int> BillsIn
        {
            get
            {
                return this._BillsIn;
            }
            set
            {
                if ((this._BillsIn != value))
                {
                    this._BillsIn = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_VouchersIn", DbType = "Int")]
        public System.Nullable<int> VouchersIn
        {
            get
            {
                return this._VouchersIn;
            }
            set
            {
                if ((this._VouchersIn != value))
                {
                    this._VouchersIn = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_NoncashableEFTIN", DbType = "Int")]
        public System.Nullable<int> NoncashableEFTIN
        {
            get
            {
                return this._NoncashableEFTIN;
            }
            set
            {
                if ((this._NoncashableEFTIN != value))
                {
                    this._NoncashableEFTIN = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_NonCashableEFTOut", DbType = "Int")]
        public System.Nullable<int> NonCashableEFTOut
        {
            get
            {
                return this._NonCashableEFTOut;
            }
            set
            {
                if ((this._NonCashableEFTOut != value))
                {
                    this._NonCashableEFTOut = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_CashableEFTIn", DbType = "Int")]
        public System.Nullable<int> CashableEFTIn
        {
            get
            {
                return this._CashableEFTIn;
            }
            set
            {
                if ((this._CashableEFTIn != value))
                {
                    this._CashableEFTIn = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_CashableEFTOut", DbType = "Int")]
        public System.Nullable<int> CashableEFTOut
        {
            get
            {
                return this._CashableEFTOut;
            }
            set
            {
                if ((this._CashableEFTOut != value))
                {
                    this._CashableEFTOut = value;
                }
            }
        }
    }

    public partial class FloorFinancialSessionResult
    {

        private string _Setting_Value;

        public FloorFinancialSessionResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Setting_Value", DbType = "VarChar(8000)")]
        public string Setting_Value
        {
            get
            {
                return this._Setting_Value;
            }
            set
            {
                if ((this._Setting_Value != value))
                {
                    this._Setting_Value = value;
                }
            }
        }
    }

    public partial class TicketCompleteMC300Result
    {

        private string _Setting_Value;

        public TicketCompleteMC300Result()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Setting_Value", DbType = "VarChar(8000)")]
        public string Setting_Value
        {
            get
            {
                return this._Setting_Value;
            }
            set
            {
                if ((this._Setting_Value != value))
                {
                    this._Setting_Value = value;
                }
            }
        }
    }
}
