using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BMC.Transport.CashDeskOperatorEntity;
using BMC.Business.CashDeskOperator;
using System.Net;

namespace BMC.CashDeskOperator.BusinessObjects
{
    public partial class HandpayBusinessObject : IHandpay
    {
        #region Private Variables
        private HandPay handPay = new HandPay();
        #endregion

        #region Constructor 
        private HandpayBusinessObject() { }
        #endregion
                
        #region Public Function
        /// <summary>
        /// Functions used to process handpay from events and manual process.
        /// </summary>
        /// <returns></returns>
        /// 
        public DataTable GetUnprocessedHandpays()
        {
            return handPay.GetUnprocessedHandPays();
        }

        public DataTable GetUnprocessedHandpays(int InstallationNo)
        {
            return handPay.GetUnprocessedHandPays(InstallationNo);
        }

        public int ProcessHandPay(Treasury Treasury)
        {
            return handPay.ProcessManualHandpay(Treasury);
        }

        public int ProcessHandPayOnDenomChange(string sBarpos, int userID)
        {
            return handPay.ProcessHandPayOnDenomChange(sBarpos, userID);
        }

        public int ProcessHandPay(Treasury Treasury, int TE_ID)
        {
            LockHandler Lock = new LockHandler();

            int SPResult = TE_ID==0 ? 0: Lock.InsertLockRecord(0, Dns.GetHostName(), "ATTENDANTPAY", "TE_ID", TE_ID.ToString());
            int ReturnValue = -1;

            switch (SPResult)
            {
                case 0: ReturnValue= handPay.ProcessHandpay(Treasury, TE_ID);
                    if (TE_ID == 0) return ReturnValue;
                    break;
                case 1:
                    ReturnValue = -2;//LockExists
                    return ReturnValue;
                case 2:
                    ReturnValue = -3;//LockError
                    return ReturnValue;
                default:
                    ReturnValue = -4;//DatabaseError
                    return ReturnValue;

            }
            Lock.DeleteLockRecord(0, Dns.GetHostName(), "ATTENDANTPAY", "TE_ID", TE_ID.ToString());
            return ReturnValue;
                        
            
        }

        //public DataTable FillMachines()
        //{
        //    return handPay.FillMachines();
        //}
        public DateTime? GetTreasuryDateTime(int Treasury_ID)
        {
            return handPay.GetTreasuryDateTime(Treasury_ID);
        }

        public bool RollbackHandPay(int Ticket_ExceptionID, int Treasury_No)
        {
            return handPay.RollbackProcessHandPay(Ticket_ExceptionID, Treasury_No);
        }

        public bool ExportHandPay(int Treasury_No)
        {
            return handPay.ExportHandPay(Treasury_No);
        }

        public bool SaveVoidExpiredTreasury(VoidOrExpiredTreasury ExpiredTreasury)
        {
            return handPay.SaveVoidorExpiredTreasury(ExpiredTreasury);
        }

        public bool UpdateTicketException(string TicketNumber)
        {
            return handPay.UpdateFinalStatusTicketException(TicketNumber);
        }

        public DataTable GetTicketingExceptionTable(string TicketNumber)
        {
            return handPay.GetTicketExceptions(TicketNumber);
        }

        public IEnumerable<FillTreasuryList> GetHandpays(string BarPos)
        {
            return handPay.GetHandpays(BarPos);
        }

        public int VoidTransaction(VoidTranCreate VoidTransactionEntity)
        {
            return handPay.VoidTreasuryNegativeTrans(VoidTransactionEntity);
        }
        #region IHandpay Members

       
        List<AssetNumberResult> IHandpay.GetAssetNumber(int installation_No)
        {
            return handPay.GetAssetNumber(installation_No);
        }

        List<DenomValueResult> IHandpay.GetDenomValue(string Stock_no)
        {
            return handPay.GetDenomValue(Stock_no);
        }
        public int GetUserID(int? SecurityUserID)
        {
            return handPay.GetUserID(SecurityUserID);
            
         }
            
        
        #endregion


        public string GetEPIDetails(int installation_No)
        {
            return handPay.GetEPIDetails(installation_No);
        }
        public int Clearhandpay(int InstallationNo)
        {
            return handPay.Clearhandpay(InstallationNo);
        }
        #endregion

        #region Public Static Function 
        public static IHandpay CreateInstance()
        {
            return new HandpayBusinessObject();
        }
        #endregion
        
    }
}
