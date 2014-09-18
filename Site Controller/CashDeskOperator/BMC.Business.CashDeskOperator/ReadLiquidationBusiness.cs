using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.CommonLiquidation.Utilities;
using BMC.DBInterface.CashDeskOperator;

namespace BMC.Business.CashDeskOperator
{
    public class ReadLiquidationBusiness
    {
        #region Data Members

        private LiquidationUtility oLiquidationUtility = null;

        #endregion //Data Members

        #region Constructor

        public ReadLiquidationBusiness()
        {
            oLiquidationUtility = new LiquidationUtility();
        }

        #endregion //Constructor

        #region Methods

        public decimal CalculateRetailerNegativeNet(decimal _profitSharePercentage)
        {
            return oLiquidationUtility.CalculateRetailerNegativeNet(CommonDataAccess.ExchangeConnectionString, null, _profitSharePercentage);
        }

        public int SaveLiquidation(CommonLiquidationEntity objCommonLiquidationEntity)
        {
            return oLiquidationUtility.SaveLiquidation(CommonDataAccess.ExchangeConnectionString, null, objCommonLiquidationEntity);
        }

        #endregion //Methods
    }
}
