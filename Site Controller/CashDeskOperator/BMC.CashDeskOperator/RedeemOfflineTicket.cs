using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BMC.Transport.CashDeskOperatorEntity;
using BMC.Business.CashDeskOperator;

namespace BMC.CashDeskOperator.BusinessObjects
{
    public class RedeemOfflineTicketBusinessObject: IRedeemOfflineTicket
    {
        #region Private Variables
        private RedeemOfflineTicket redeemOfflineTicket = new RedeemOfflineTicket();
        #endregion

        #region Constructor
        private RedeemOfflineTicketBusinessObject() { }
        #endregion

        #region Public Function 
        public bool IsTicketValid(int InstallationNo, string TicketNumber, int Amount)
        {
            return redeemOfflineTicket.IsTicketValid(InstallationNo, TicketNumber, Amount);
        }

        public bool SaveOfflineTicketDetails(OfflineTicket OfflineTicketEntity, out int treasuryNo)
        {
            return redeemOfflineTicket.SaveOfflineTicketDetails(OfflineTicketEntity, out treasuryNo);
        }
        #endregion

        #region Public Static Function
        public static IRedeemOfflineTicket CreateInstance()
        {
            return new RedeemOfflineTicketBusinessObject();
        }
        #endregion
    }
}
