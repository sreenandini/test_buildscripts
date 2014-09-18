using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC
{
    /// <summary>
    /// 
    /// </summary>
    interface ICashDeskManager
    {
        bool CashDeskMovement();
        bool CashDeskReconsilation();
        bool Debug();
        bool SystemBalancing();
        bool TreasuryTransactions();
    }
    /// <summary>
    /// 
    /// </summary>
    public class CashDeskManager : ICashDeskManager
    {
        # region ICashDeskManager Members

        public bool CashDeskMovement()
        {
            throw new NotImplementedException();
        }
        public bool CashDeskReconsilation()
        {
            throw new NotImplementedException();
        }
        public bool Debug()
        {
            throw new NotImplementedException();
        }
        public bool SystemBalancing()
        {
            throw new NotImplementedException();
        }
        public bool TreasuryTransactions()
        {
            throw new NotImplementedException();
        }

        # endregion
    }
}
