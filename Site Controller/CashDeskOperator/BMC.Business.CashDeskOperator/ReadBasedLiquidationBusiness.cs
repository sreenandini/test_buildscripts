using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.DBInterface.CashDeskOperator;
using BMC.CommonLiquidation.Utilities;

namespace BMC.Business.CashDeskOperator
{
    public class ReadBasedLiquidationBusiness
    {
        #region Data Members

        private LiquidationUtility oLiquidationUtility = null;

        #endregion //Data Members

        #region Constructor

        public ReadBasedLiquidationBusiness()
        {
            oLiquidationUtility = new LiquidationUtility();
        }

        #endregion //Constructor

        #region Methods

        public List<ReadLiquidationEntity> GetReadLiquidationRecords()
        {
            return oLiquidationUtility.GetReadLiquidationRecords(CommonDataAccess.ExchangeConnectionString, null).ToList();
        }

        public List<CommonLiquidationEntity> GetLiquidation(DateTime minDate, DateTime maxDate)
        {
            return oLiquidationUtility.GetReadLiquidation(CommonDataAccess.ExchangeConnectionString, null, minDate, maxDate);
        }

        #endregion //Methods
    }
}
