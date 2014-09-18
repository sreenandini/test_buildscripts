using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.Business.CashDeskOperator;
using BMC.CommonLiquidation.Utilities;

namespace BMC.CashDeskOperator.BusinessObjects
{
    public class ProfitShareConfiguration : IProfitShare
    {
        private static ProfitShareConfiguration _ProfitShareConfiguration = null;
        private ProfitShareBusiness oProfitShareBusiness = new ProfitShareBusiness();

        #region Private Constructor
       
        private ProfitShareConfiguration() { }
       
        #endregion

        public static ProfitShareConfiguration ProfitShareConfigurationInstance
        {
            get
            {
                if (_ProfitShareConfiguration == null)
                    _ProfitShareConfiguration = new ProfitShareConfiguration();

                return _ProfitShareConfiguration;
            }
        }

        #region IReadBasedLiquidation Members

        public List<ProfitShareGroup> GetProfitShareGroupList() { return oProfitShareBusiness.GetProfitShareGroupList(); }
        public List<ExpenseShareGroup> GetExpenseShareGroupList() { return oProfitShareBusiness.GetExpenseShareGroupList(); }
        public List<PayPeriods> GetPayPeriods() { return oProfitShareBusiness.GetPayPeriods(); }

        #endregion IReadBasedLiquidation Members
    }
}
