using System;
using System.Collections.Generic;
using System.Linq;
using BMC.DBInterface.CashDeskOperator;
using BMC.CommonLiquidation.Utilities;

namespace BMC.Business.CashDeskOperator
{
    public class ReadLiquidationDetailsBusiness
    {
        #region Data Members

        private LiquidationUtility oLiquidationUtility = null;

        #endregion //Data Members

        #region Constructor

        public ReadLiquidationDetailsBusiness()
        {
            oLiquidationUtility = new LiquidationUtility();
        }

        #endregion //Constructor

        #region Methods

        public List<ReadLiquidationDetails> GetReadLiquidationDetails(DateTime _minDateTime, DateTime _maxDateTime)
        {
            return oLiquidationUtility.GetReadLiquidationDetails(CommonDataAccess.ExchangeConnectionString, null, _minDateTime, _maxDateTime).ToList();
        }

        #endregion //Methods
    }
}
