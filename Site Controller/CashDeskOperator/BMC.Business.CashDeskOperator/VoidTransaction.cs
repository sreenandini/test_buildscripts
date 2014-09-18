using System;
using System.Data;
using BMC.Common.ExceptionManagement;
using BMC.DBInterface.CashDeskOperator;
using BMC.Transport.CashDeskOperatorEntity;

namespace BMC.Business.CashDeskOperator
{
    public class VoidTransaction
    {

        VoidTransactionDataAccess voidTransactionDataAccess = new VoidTransactionDataAccess();

        public int VoidCreate(VoidTranCreate VoidTransactionEntiy)
        {
            return voidTransactionDataAccess.VoidTreasuryNegGen(VoidTransactionEntiy);
        }

        public DataSet FillVoidList()
        {
            return voidTransactionDataAccess.FillVoidTransactionList();
        }
    }
}
