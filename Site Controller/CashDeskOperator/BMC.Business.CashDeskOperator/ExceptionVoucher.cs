using System;
using System.Data;
using BMC.DBInterface.CashDeskOperator;
using BMC.Transport.CashDeskOperatorEntity;

namespace BMC.Business.CashDeskOperator
{
    public class ExceptionVoucher
    {
        ExceptionVoucherDataAccess exceptionVoucher = new ExceptionVoucherDataAccess();

        #region "Public Functions"
        public int IsExceptionVoucher(string TicketString)
        {
            return exceptionVoucher.IsExceptionVoucher(TicketString);
        }

        public int MarkExceptionVoucherActive(string TicketString)
        {
            return exceptionVoucher.MarkExceptionVoucherActive(TicketString);
        }

        #endregion
    }
}
