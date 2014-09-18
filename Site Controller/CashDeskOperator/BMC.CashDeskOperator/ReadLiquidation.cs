using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.CommonLiquidation.Utilities;
using BMC.Business.CashDeskOperator;

namespace BMC.CashDeskOperator.BusinessObjects
{
    public class ReadLiquidationConfiguration : IReadLiquidation
    {
        private static ReadLiquidationConfiguration _ReadLiquidationConfiguration = null;
        private ReadLiquidationBusiness oReadLiquidationbusiness = new ReadLiquidationBusiness();

        #region Private Constructor

        private ReadLiquidationConfiguration() { }
       
        #endregion

        public static ReadLiquidationConfiguration ReadLiquidationConfigurationInstance
        {
            get
            {
                if (_ReadLiquidationConfiguration == null)
                    _ReadLiquidationConfiguration = new ReadLiquidationConfiguration();

                return _ReadLiquidationConfiguration;
            }
        }

        #region IReadBasedLiquidation Members

        public decimal CalculateRetailerNegativeNet(decimal _profitSharePercentage) { return oReadLiquidationbusiness.CalculateRetailerNegativeNet(_profitSharePercentage); }
        public int SaveLiquidation(CommonLiquidationEntity objCommonLiquidationEntity) { return oReadLiquidationbusiness.SaveLiquidation(objCommonLiquidationEntity); }

        #endregion IReadBasedLiquidation Members
    }
}
