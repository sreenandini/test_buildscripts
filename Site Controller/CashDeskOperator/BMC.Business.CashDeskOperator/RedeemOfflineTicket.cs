using System;
using System.Data;
using BMC.Common.ExceptionManagement;
using BMC.Common.LogManagement;
using BMC.DBInterface.CashDeskOperator;
using BMC.Transport.CashDeskOperatorEntity;
using BMC.Common.Utilities;

namespace BMC.Business.CashDeskOperator
{
    public class RedeemOfflineTicket
    {
        private int CONST_DEFAULT_TICKET_LENGTH = 18;
        private RedeemOfflineDataAccess RedeemOfflineTicketDBAccess = new RedeemOfflineDataAccess();

        public bool CheckCRC(string sBarcode, long lValue, string SiteCode, string Datapak)
        {
            try
            {
                if (sBarcode.Length == 8)
                {
                    Datapak = string.Concat(Datapak, "0000");
                    sBarcode = "1" + SiteCode.Substring(SiteCode.Length - 3, 3) + Datapak.Substring(0, 5) + "0" + sBarcode.Substring(0, 4) + sBarcode.Substring(sBarcode.Length - 4, 4);
                }

                SDGTicketGenLib.SDGTicketGenClass oticketGen = new SDGTicketGenLib.SDGTicketGenClass();
                string sCRCTicket = oticketGen.stdBarcode(sBarcode.Substring(0, 14), (int)lValue);
                LogManager.WriteLog("CheckCRC Voucher:" + sCRCTicket, LogManager.enumLogLevel.Error);
                if (sCRCTicket == sBarcode)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("CheckCRC Message:" + ex.Message, LogManager.enumLogLevel.Error);
                return false;
            }
        }

        public bool IsTicketValid(int InstallationNo, string TicketNumber, int Amount)
        {
            try
            {
                DataTable InstallationDetails = (new CommonDataAccess()).GetInstallationDetails(0, InstallationNo, false, false);
                int ValidationLength =0;
                
                string SiteCode = BMC.Transport.Settings.SiteCode;
                if (InstallationDetails.Rows.Count > 0)
                {
                    LogManager.WriteLog("IsTicketValid::Barcode - " + TicketNumber + ", SiteCode - " + SiteCode + " , Installation - " + InstallationNo.ToString(), LogManager.enumLogLevel.Info);
                    try
                    {
                        int.TryParse( InstallationDetails.Rows[0]["Validation_length"].ToString(), out ValidationLength);
                        if (ValidationLength == 0)
                            ValidationLength = CONST_DEFAULT_TICKET_LENGTH;
                    }
                    catch
                    {   
                        ValidationLength = CONST_DEFAULT_TICKET_LENGTH;
                    }
                    if (TicketNumber.Trim().Length == ValidationLength)
                    {
                        string Installation = InstallationNo.ToString();
                        if (CheckCRC( TicketNumber,  Amount,  SiteCode,  Installation))
                            return true;
                        else
                        {
                            LogManager.WriteLog("IsVoucherValid::CRC not matching for offline Voucher: " + TicketNumber, LogManager.enumLogLevel.Info);
                            return false;
                        }
                    }
                    else
                    {
                        LogManager.WriteLog("IsTicketValid::Voucher Length not equal to machines validation Length for :" + TicketNumber, LogManager.enumLogLevel.Error);
                        return false;
                    }
                }
                else
                {
                    LogManager.WriteLog("IsVoucherValid::Unable to find Installation for " +InstallationNo.ToString(), LogManager.enumLogLevel.Error);
                    return false;
                }

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);                
            }
            return false;
        }

        public DataTable GetInstallationList()
        {
            return ( CommonDataAccess.GetInstallationList());        
        }

        public bool SaveOfflineTicketDetails(OfflineTicket objOfflineTicketDetails, out int treasuryNo)
        {
            return RedeemOfflineTicketDBAccess.SaveOfflineTicketDetails(objOfflineTicketDetails, out treasuryNo);
        }

    }
}
