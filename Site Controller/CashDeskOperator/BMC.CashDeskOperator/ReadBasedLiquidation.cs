using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.Business.CashDeskOperator;
using BMC.CommonLiquidation.Utilities;

namespace BMC.CashDeskOperator.BusinessObjects
{
    public class ReadBasedLiquidationConfiguration : IReadBasedLiquidation
    {
        private static ReadBasedLiquidationConfiguration _ReadBasedLiquidationConfiguration = null;
        private ReadBasedLiquidationBusiness oReadBasedLiquidationbusiness = new ReadBasedLiquidationBusiness();

        #region Private Constructor
       
        private ReadBasedLiquidationConfiguration() { }
       
        #endregion

        public static ReadBasedLiquidationConfiguration ReadBasedLiquidationConfigurationInstance
        {
            get
            {
                if (_ReadBasedLiquidationConfiguration == null)
                    _ReadBasedLiquidationConfiguration = new ReadBasedLiquidationConfiguration();

                return _ReadBasedLiquidationConfiguration;
            }
        }

        #region IReadBasedLiquidation Members

        public List<ReadLiquidationEntity> GetReadLiquidationRecords() { return oReadBasedLiquidationbusiness.GetReadLiquidationRecords(); }
        public List<CommonLiquidationEntity> GetReadLiquidation(DateTime minDate, DateTime maxDate) { return oReadBasedLiquidationbusiness.GetLiquidation(minDate, maxDate); }

        #endregion IReadBasedLiquidation Members
    }
}
