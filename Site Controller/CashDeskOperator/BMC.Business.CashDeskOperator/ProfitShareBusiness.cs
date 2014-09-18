using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.CommonLiquidation.Utilities;
using BMC.DataAccess;
using System.Data;
using BMC.Common.ExceptionManagement;
using System.Data.Linq;
using BMC.DBInterface.CashDeskOperator;

namespace BMC.Business.CashDeskOperator
{
    public class ProfitShareBusiness
    {

        #region Data Members

        private LiquidationUtility oLiquidationUtility = null;

        #endregion //Data Members

        #region Constructor

        public ProfitShareBusiness()
        {
            oLiquidationUtility = new LiquidationUtility();
        }

        #endregion //Constructor

        #region Public Methods

        public List<ProfitShareGroup> GetProfitShareGroupList()
        {
            try
            {
                List<ProfitShareGroup> lstProfitShareGroup = oLiquidationUtility.GetProfitShareGroupList(CommonDataAccess.ExchangeConnectionString).ToList();
                lstProfitShareGroup.Insert(0, GetNoneItemofProfitShareGroup());
                return lstProfitShareGroup;
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return new List<ProfitShareGroup>();
            }
        }

        public List<ExpenseShareGroup> GetExpenseShareGroupList()
        {
            try
            {
                List<ExpenseShareGroup> lstExpenseShareGroup = oLiquidationUtility.GetExpenseShareGroupList(CommonDataAccess.ExchangeConnectionString).ToList();
            lstExpenseShareGroup.Insert(0, GetNoneItemofExpenseShareGroup());
            return lstExpenseShareGroup;
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return new List<ExpenseShareGroup> ();
            }
        }

        public List<PayPeriods> GetPayPeriods()
        {
            try
            {
                List<PayPeriods> lstPayPeriods = oLiquidationUtility.GetPayPeriods(CommonDataAccess.ExchangeConnectionString, null).ToList();
                lstPayPeriods.Insert(0, GetNoneItemofPayPeriods());
                return lstPayPeriods;
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return new List<PayPeriods>();
            }
        }

        #endregion //Public Methods

        #region Private Methods

        private ProfitShareGroup GetNoneItemofProfitShareGroup()
        {
            try
            {
                ProfitShareGroup objProfitShareGroup = new ProfitShareGroup
                {
                    ProfitShareGroupID = 0,
                    ProfitShareGroupName = "Please Select",
                    ProfitSharePercentage = 0
                };
                return objProfitShareGroup;
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return new ProfitShareGroup();
            }
        }

        private ExpenseShareGroup GetNoneItemofExpenseShareGroup()
        {
            try
            {
                ExpenseShareGroup objExpenseShareGroup = new ExpenseShareGroup
                {
                    ExpenseShareGroupID = 0,
                    ExpenseShareGroupName = "Please Select",
                    ExpenseSharePercentage = 0
                };
                return objExpenseShareGroup;
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return new ExpenseShareGroup();
            }
        }

        private PayPeriods GetNoneItemofPayPeriods()
        {
            try
            {
                PayPeriods objPayPeriods = new PayPeriods
                {
                    Calendar_Period_ID = 0,
                    Calendar_Period = "Please Select",
                };
                return objPayPeriods;
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return new PayPeriods();
            }
        }

        #endregion //Private Methods
    }
}



