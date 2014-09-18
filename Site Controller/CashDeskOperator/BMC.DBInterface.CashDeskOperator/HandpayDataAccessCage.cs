using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using BMC.DataAccess;
using BMC.Common.ExceptionManagement;
using System.Linq;
using System.Text;
using BMC.Transport.CashDeskOperatorEntity;
using BMC.Transport;
using System.Data.Linq;
using BMC.Common.LogManagement;
 
namespace BMC.DBInterface.CashDeskOperator
{
    public partial class HandpayDataAccess
    {
        #region "Public Static Function"

        public BMC.Transport.jackpotProcessInfoDTO getJackpotStatusAmount(string JackpotSlipNumber, int SiteNo)
        {
            try
            {
                jackpotProcessInfoDTO jpdto = null;
                SqlParameter[] ObjParams = new SqlParameter[2];
                ObjParams[0] = new SqlParameter(DBConstants.CONST_PARAM_TE_Slip_NUMBER, JackpotSlipNumber);
                ObjParams[1] = new SqlParameter(DBConstants.CONST_PARAM_JACKPOT_SITE, SiteNo);
                SqlDataReader reader = SqlHelper.ExecuteReader(CommonDataAccess.ExchangeConnectionString, CommandType.StoredProcedure,
                  "rsp_GetJackpotDetails", ObjParams);
                while (reader.Read())
                {
                    jpdto = new jackpotProcessInfoDTO();
                    jpdto.assetConfigNumber = reader["Asset"].ToString();
                    jpdto.errCode = Convert.ToInt16(reader["Error"]);
                    jpdto.errMessage = reader["ErrorMessage"].ToString();
                    jpdto.hpjpAmount = Convert.ToInt32(reader["Amount"]);
                    //jpdto.hpjpAmountSpecified = true;
                    jpdto.jackpotNetAmount = Convert.ToInt32(reader["Amount"]);
                    //jpdto.jackpotNetAmountSpecified = true;
                    jpdto.jackpotTypeId = Convert.ToInt16(reader["HPType"]);
                    //jpdto.jackpotTypeIdSpecified = true;
                    jpdto.processFlagId = 1; // "Auto";
                    //jpdto.processFlagIdSpecified = true;
                    jpdto.processSuccessful = false; //currently unprocessed
                    jpdto.sequenceNumber = Convert.ToInt32(reader["SlipNo"]);
                    //jpdto.sequenceNumberSpecified = true;
                    jpdto.siteId = Convert.ToInt32(reader["Site"]);
                    //jpdto.siteIdSpecified = true;
                    jpdto.siteNo = reader["Sitename"].ToString();
                    jpdto.slipReferenceID = Convert.ToInt32(reader["SlipNo"]);
                    jpdto.statusFlagId = reader["Error"].ToString() == "8801" ? Convert.ToInt16(4002) : reader["Error"].ToString() == "8802" ? Convert.ToInt16(4003)
                        : reader["Error"].ToString() == "8804" ? Convert.ToInt16(4001) : reader["Error"].ToString() == "8803" ? Convert.ToInt16(4006) : Convert.ToInt16(4014);
                    //jpdto.statusFlagIdSpecified = true;
                }
                return jpdto;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return null;
            }
        }

        #endregion

        #region Common
        const int IntegerNull = int.MinValue;
        DateTime DateUnassigned = DateTime.Parse("12:00:00 AM");
        DateTime DateNull = DateTime.Parse("1/1/1800");
        DateTime DateMin = DateTime.Parse("1/1/1801");
        //DateTime DateMax = DateTime.Parse("12/31/9999");
        DateTime DateTimeBase = DateTime.Parse("1/2/1801");
        const short ShortNull = short.MinValue;
        const decimal DecimalNull = decimal.MinValue;
        const double DoubleNull = double.MinValue;
        const long LongNull = long.MinValue;
        const string StringNull = "";
        #endregion

        public jackpotProcessInfoDTO payJackpot(string SequenceNumber, int SiteId, string userId, string firstName, string lastName, string cashDeskLocation)
        {
            LinqDataAccessDataContext linqDB = new LinqDataAccessDataContext(CommonDataAccess.ExchangeConnectionString);
            jackpotProcessInfoDTO jpinfo = getJackpotStatusAmount(SequenceNumber, SiteId);
            Treasury objTreasury = new Treasury();
            int? Treasury_No = 0;
            int result;
            int iInstallationNumber = 0;
            ISingleResult<HandpayEntities> lstHpDetails = linqDB.GetHpDetails(Convert.ToInt32(jpinfo.sequenceNumber));

            int IsManualAttendantPay = 0;

            try
            {
                foreach (HandpayEntities ticket in lstHpDetails)
                {

                    IsManualAttendantPay = ticket.IsManualAttendantPay;
                    iInstallationNumber =int.Parse (ticket.installation_no.ToString() );
                    result = linqDB.InsertTreasuryCage(ticket.installation_no, 0, userId, ticket.handpaytype, "", Convert.ToDecimal(ticket.amount)
                        , false, "", 0, 0, false, 0, ticket.TeDate, userId, userId, DateTime.Now, 0,int.Parse( SequenceNumber), ref Treasury_No);
                }

                if (Treasury_No.Value > 0)
                {
                    if (Convert.ToInt32(SequenceNumber) > 0)
                        result = linqDB.UpdateFinalStatusTicketException(Convert.ToInt32(SequenceNumber));
                    jpinfo.processSuccessful = true;
                }

                if (Treasury_No.Value <= 0)
                {
                    jpinfo.errCode = -1;
                }
                //LogManager.WriteLog("Clear Handpay/Jackpot Lock InstallatioNo:[" + iInstallationNumber.ToString() + "Manual:" + IsManualAttendantPay.ToString () + " ]", LogManager.enumLogLevel.Debug);
                ////Clear the lock in the slot
                //if (IsManualAttendantPay == 0)
                //{
                //    Clearhandpay(iInstallationNumber);
                //}
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
                jpinfo.errCode = -1;
		    }
            return jpinfo ;

        }
        public int GetInstallationNo(string SequenceNumber, int SiteId, out int InstallationNo)
        {
            LinqDataAccessDataContext linqDB = new LinqDataAccessDataContext(CommonDataAccess.ExchangeConnectionString);
            InstallationNo = 0;
            try
            {
                ISingleResult<HandpayEntities> lstHpDetails = linqDB.GetHpDetails(int.Parse(SequenceNumber));
                foreach (HandpayEntities ticket in lstHpDetails)
                {
                    InstallationNo = int.Parse(ticket.installation_no.ToString());
                   return  ticket.IsManualAttendantPay;
                }
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
            return 0 ;

        }

        private bool Clearhandpay(int InstallationNo)
        {
            int Datapak;
            DataTable dt = new DataTable();
            dt =GetHandpaystoClear(InstallationNo);
            LogManager.WriteLog("Clear Handpay/Jackpot Lock",LogManager.enumLogLevel.Debug );
            foreach (DataRow dr in dt.Rows)
            {
                LogManager.WriteLog("Clear Handpay/Jackpot Lock Build clear script", LogManager.enumLogLevel.Debug);
                Datapak = GetRowValue<int>(dr, "Datapak_No");
                //return MachineManager.ClearHandpayLock(Datapak);
                System.Diagnostics.Process proc = new System.Diagnostics.Process();
                LogManager.WriteLog("Clear Handpay/Jackpot Lock Script Path :" +  AssemblyDirectory + @"\..\..\CmdLine"  , LogManager.enumLogLevel.Debug);
                proc.StartInfo.FileName =AssemblyDirectory + @"\..\..\CmdLine";
                proc.StartInfo.Arguments = Datapak.ToString();
                proc.Start();
                LogManager.WriteLog("Clear Handpay/Jackpot Lock cleared ", LogManager.enumLogLevel.Debug);
            }
            return false;
        }

        public DataTable GetHandpaystoClear(int InstallationNo)
        {
            DataTable Handpay = new DataTable();
            
            try
            {
                //DataBaseServiceHandler.ConnectionString = CommonDataAccess.ExchangeConnectionString;
                Handpay = DataBaseServiceHandler.Fill(CommonDataAccess.ExchangeConnectionString, CommandType.Text, "Select IsNull(Datapak_No, 0) As Datapak_No From installation Where Installation_no = " + InstallationNo.ToString(), Handpay);
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }

            return Handpay;
        }


        public static T GetRowValue<T>(DataRow Row, string ColumnName)
        {
            Type TypeOfT;
            TypeOfT = typeof(T);
            TypeCode tpCode = Type.GetTypeCode(TypeOfT);
            switch (tpCode)
            {

                case TypeCode.DateTime:
                    if (Row[ColumnName].Equals(DBNull.Value))
                        return (T)(object)DateTime.Parse("1/1/1800");
                    else
                        return (T)Row[ColumnName];
                case TypeCode.Decimal:
                    if (Row[ColumnName].Equals(DBNull.Value))
                        return (T)(object)DecimalNull;
                    else
                        return (T)Row[ColumnName];
                case TypeCode.Double:
                    if (Row[ColumnName].Equals(DBNull.Value))
                        return (T)(object)DoubleNull;
                    else
                        //return (T)Row[ColumnName];
                        return (T)(object)Convert.ToDouble(Row[ColumnName]);
                case TypeCode.Int32:
                    if (Row[ColumnName].Equals(DBNull.Value))
                        return (T)(object)int.Parse("0");
                    else
                        return (T)Row[ColumnName];

                case TypeCode.Int64:
                    if (Row[ColumnName].Equals(DBNull.Value))
                        return (T)(object)int.Parse("0");
                    else
                        return (T)Row[ColumnName];

                case TypeCode.String:
                    if (Row[ColumnName].Equals(DBNull.Value))
                        return (T)(object)string.Empty;
                    else
                        return (T)Row[ColumnName];

                case TypeCode.Boolean:
                    if (Row[ColumnName].Equals(DBNull.Value))
                        return (T)(object)false;
                    else
                        return (T)Row[ColumnName];

                default:
                    return (T)(object)null;
            }

        }
        public DataSet CreateTickeException_HandpayCage(int Bar_Pos, Double  TicketValue, string Server, int isHandpayResponse, string HP_Type)
        {
            try
            {
                LinqDataAccessDataContext linqDB = new LinqDataAccessDataContext();
                return linqDB.createTickeException_HandpayCAGE(Bar_Pos, TicketValue, Server, isHandpayResponse, HP_Type);
            }
            catch (Exception Ex)
            {
               ExceptionManager.Publish(Ex);
            }
            return null;
        }
       private string AssemblyDirectory
        {
            get
            {
                string codeBase = System.Reflection.Assembly.GetExecutingAssembly().CodeBase;
                UriBuilder uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                return System.IO.Path.GetDirectoryName(path);
            }
        }
    }
}
