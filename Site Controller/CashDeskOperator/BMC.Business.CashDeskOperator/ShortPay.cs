using System;
using System.Data;
using BMC.DBInterface.CashDeskOperator;
using BMC.Transport.CashDeskOperatorEntity;
using System.Collections.Generic;
using System.Data.Linq;
using BMC.Common.ExceptionManagement;
using System.Linq;
using BMC.Transport;

namespace BMC.Business.CashDeskOperator
{
    public enum ExceptionCodes
    {
        NormalTreasuryEntry = 210,
        HouseKeepingVoid = 211,
        VoidTicketForShortpay = 212,
        VoidTicketForHandpay = 213,
        voidTicketForJackpotHandpay = 214,
        VoidProcessCompletedAlready = 217,
        NoEntryInTicket_Exception = 215,
        NoMatchingEntry = 216
    }

    public class ShortPay
    {
        CommonDataAccess commonDataAccess = new CommonDataAccess();
        ShortPayDataAccess shortPayDataAccess = new ShortPayDataAccess(BMC.Common.Utilities.DatabaseHelper.GetConnectionString());

        public DataTable GetUserRoles(string UserName, string Password)
        {
            return commonDataAccess.GetUserRoles(UserName, Password);            
        }

        public DataTable GetFailureReasons()
        {
            return shortPayDataAccess.GetFailureReasons();
        }

        public DataTable GetInstallationList()
        {
            return (CommonDataAccess.GetInstallationList());
        }

        public int SaveShortpayDetails(Treasury TreasuryEntity)
        {
            return shortPayDataAccess.SaveTreasuryDetails(TreasuryEntity);          
        }

        public int SaveReasonDetails(ReasonCode objReason)
        {
            return shortPayDataAccess.SaveReasonDetails(objReason);
        }

        public int DeleteReasonDetails(ReasonCode objReason)
        {
            return shortPayDataAccess.DeleteReasonDetails(objReason);
            
        }

        public DataTable GetTicketingExceptionTable(string TicketNumber)
        {
            return shortPayDataAccess.GetTicketingExceptionTable(TicketNumber);
        }

        public int UpdateVoidorExpiredTreasury(VoidOrExpiredTreasury VoidTreasuryEntity)
        {
            return shortPayDataAccess.UpdateVoidorExpiredTreasury(VoidTreasuryEntity);
        }

        public int UpdateTicketException(int ID, string TicketNumber, string Value)
        {
            return shortPayDataAccess.UpdateTicketException(ID, TicketNumber, Value);
        }

        public int InsertException(TicketException TicketException)
        {
            return shortPayDataAccess.InsertException(TicketException);
        }

        public int GetMaxTreasuryID()
        {
            return shortPayDataAccess.GetMaxTreasuryID();           
        }

        public bool CreateShortPayForApproval(Treasury TreasuryEntity, ref int iShortPayID)
        {
            return shortPayDataAccess.CreateShortPayForApproval(TreasuryEntity,ref iShortPayID);
        }

        public void ApproveShortPay(string strIDs, int UserID, int TreasuryID)
        {
            shortPayDataAccess.ApproveShortPay(strIDs, UserID, TreasuryID);
        }

        public void CancelShortPayForApproval(int iShortPayID)
        {
            shortPayDataAccess.CancelShortPayForApproval(iShortPayID);
        }
    }
}
