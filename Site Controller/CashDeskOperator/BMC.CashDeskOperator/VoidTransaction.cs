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
    public class VoidTransactionBusinessObject: IVoidTransaction
    {
        #region Private Variables
        private VoidTransaction voidTransaction = new VoidTransaction();
        #endregion

        #region Constructor
        private VoidTransactionBusinessObject() { }
        #endregion

        #region Public Function 
        public int VoidTransaction(VoidTranCreate VoidTransactionEntity)
        {
            LockHandler Lock = new LockHandler();

            int SPResult = Lock.InsertLockRecord(0, Dns.GetHostName(), "VOID", "TreasuryID", VoidTransactionEntity.TreasuryID.ToString());
            int ReturnValue = -1;

            switch (SPResult)
            {
                case 0: ReturnValue = voidTransaction.VoidCreate(VoidTransactionEntity);
                    
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
            Lock.DeleteLockRecord(0, Dns.GetHostName(), "VOID", "TreasuryID", VoidTransactionEntity.TreasuryID.ToString());
            return ReturnValue;
            
            
            
        }

        public DataSet FillVoidList()
        {
            return voidTransaction.FillVoidList();
        }
        #endregion

        #region Public Static Function
        public static IVoidTransaction CreateInstance()
        {
            return new VoidTransactionBusinessObject();
        }
        #endregion
    }
}
