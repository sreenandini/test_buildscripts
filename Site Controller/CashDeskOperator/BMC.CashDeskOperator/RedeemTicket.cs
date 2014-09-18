using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BMC.Transport.CashDeskOperatorEntity;
using BMC.Business.CashDeskOperator;

namespace BMC.CashDeskOperator.BusinessObjects
{
    public class RedeemOnlineTicketBusinessObject: IRedeemOnlineTicket
    {
        #region "Private Variables"
        private RedeemTicket redeemTicket = new RedeemTicket();
      
        #endregion

        #region Constructor
        private RedeemOnlineTicketBusinessObject() { }
        #endregion

        #region Public Function

        public RTOnlineTicketDetail RedeemOnlineTicket(RTOnlineTicketDetail TicketDetailEntity)
        {
            
           return redeemTicket.CheckTicket(TicketDetailEntity);
            //return true;
        }

        public bool RedeemOnlineTicketCage(RTOnlineTicketDetail TicketDetailEntity)
        {
            return redeemTicket.CheckTicketCage(TicketDetailEntity);
            
        }
        public bool CheckLaunderingEnabled()
        {
            return RedeemTicket.CheckLaunderingEnabled();
        }

        public int CheckSDGTicket(string TicketString)
        {
            return redeemTicket.CheckSDGTicket(TicketString);
        }

        public int CheckSDGTicketCage(string TicketString)
        {
            return redeemTicket.CheckSDGTicketCage(TicketString);
        }

        
        public RTOnlineTicketDetail GetRedeemTicketAmount(RTOnlineTicketDetail TicketDetailEntity)
        {
            return redeemTicket.GetRedeemTicketAmount(TicketDetailEntity);
        }
        public RTOnlineTicketDetail GetMultiRedeemTicketAmount(RTOnlineTicketDetail TicketDetailEntity)
        {
            return redeemTicket.GetMultiRedeemTicketAmount(TicketDetailEntity);
        }

        public RTOnlineTicketDetail GetRedeemTicketAmountCage(RTOnlineTicketDetail TicketDetailEntity)
        {
            return redeemTicket.GetRedeemTicketAmountCage(TicketDetailEntity);
        }
        public RTOnlineTicketDetail GetVoucherAmountAndStatusForMultipleTicket(RTOnlineTicketDetail TicketDetailEntity)
        {
            return redeemTicket.GetVoucherDetailForMultipleTicketRedeem(TicketDetailEntity);
        }
        public int CheckSDGOfflineTicket(string TicketString)
        {
            return redeemTicket.CheckSDGOfflineTicket(TicketString);
        }
        public string GetTicketPrintDevice(string strbarcode, out DateTime PrintDate)
        {
            return redeemTicket.GetTicketPrintDevice(strbarcode, out PrintDate);
        }

        public bool ValidateClientSiteCode(string sClientSiteCode)
        {
            return redeemTicket.ValidateClientSiteCode(sClientSiteCode);
        }

        public string GetVoucherDetailsToExport(int iVoucherID)
        {
            return redeemTicket.GetVoucherDetailsToExport(iVoucherID);
        }

        public string GetVoucherDetailsForCrossTicketing(string Barcode)
        {
            return redeemTicket.GetVoucherDetailsForCrossTicketing(Barcode);
        }

        public bool ImportVoucherDetails(RTOnlineTicketDetail TicketDetail)
        {
            return redeemTicket.ImportVoucherDetails(TicketDetail);
        }


        public ReedeemTicketDetailsComms RedeemTicketStartComms(ReedeemTicketDetailsComms TicketDetailComms)
        {
            return redeemTicket.RedeemTicketStartComms(TicketDetailComms);
        }

        public void CreatePayDeviceID(string stockNo)
        {
            redeemTicket.CreatePayDeviceID(stockNo);
        }

        public void UpdateLiabilityStatus(string Barcode, string SiteCode, string Status)
        {
            redeemTicket.UpdateLiabilityStatus(Barcode, SiteCode, Status);
        }

        public ReedeemTicketDetailsComms RedeemTicketCompleteComms(ReedeemTicketDetailsComms TicketDetailComms)
        {
            return redeemTicket.RedeemTicketCompleteComms(TicketDetailComms);
        }

        public ReedeemTicketDetailsComms RedeemTicketCancelComms(ReedeemTicketDetailsComms TicketDetailComms)
        {
            return redeemTicket.RedeemTicketCancelComms(TicketDetailComms);
        }

        #region GCD
        public bool RollbackRedeemTicket(string TicketString)
        {
            return redeemTicket.CancelRedeemTicket(TicketString);
        }
        #endregion

        #endregion

        #region Public Static Function
        public static IRedeemOnlineTicket CreateInstance()
        {
            return new RedeemOnlineTicketBusinessObject();
        }
        #endregion
    }
}
