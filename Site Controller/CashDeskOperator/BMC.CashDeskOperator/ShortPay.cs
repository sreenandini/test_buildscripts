using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BMC.Transport.CashDeskOperatorEntity;
using BMC.Business.CashDeskOperator;

namespace BMC.CashDeskOperator.BusinessObjects
{
    public class ShortPayBusinessObject: IShortPay
    {
        #region Private Variables
        private ShortPay shortPay = new ShortPay();
        #endregion

        #region Constructor
        private ShortPayBusinessObject() { }
        #endregion

        #region Public Function

        public int SaveShortpayDetails(Treasury TreasuryEntity)
        {
            return shortPay.SaveShortpayDetails(TreasuryEntity);

        }

        public int SaveReasonDetails(ReasonCode objReason)
        {
            return shortPay.SaveReasonDetails(objReason);
                            
        }

        public int DeleteReasonDetails(ReasonCode objReason)
        {
            return shortPay.DeleteReasonDetails(objReason);
            
        }

        public DataTable GetFailureReasons()
        {
            return shortPay.GetFailureReasons();
        }

        public int UpdateVoidorExpiredTreasury(VoidOrExpiredTreasury ExpiredTreasuryEntity)
        {
            return shortPay.UpdateVoidorExpiredTreasury(ExpiredTreasuryEntity);
        }

        public int UpdateTicketException(int ID, string TicketNumber, string Value)
        {
            return shortPay.UpdateTicketException(ID, TicketNumber, Value);
        }

        public int GetMaxTreasuryID()
        {
            return shortPay.GetMaxTreasuryID();
        }

        public int InsertException(TicketException TicketExceptionEntity)
        {
            return shortPay.InsertException(TicketExceptionEntity);
        }

        public bool CreateShortPayForApproval(Treasury TreasuryEntity, ref int iShortPayID)
        {
            return shortPay.CreateShortPayForApproval(TreasuryEntity, ref iShortPayID);
        }
        
        public void ApproveShortPay(string strIDs, int UserID, int TreasuryID)
        {
            shortPay.ApproveShortPay(strIDs, UserID, TreasuryID);
        }

        public void CancelShortPayForApproval(int iShortPayID)
        {
            shortPay.CancelShortPayForApproval(iShortPayID);
        }

        #endregion

        #region Public Static Function
        
        public static IShortPay CreateInstance()
        {
            return new ShortPayBusinessObject();
        }
        
        #endregion

    }
}
