using System;
using System.Collections.Generic;
using System.Linq;
using BMC.Business.CashDeskOperator;
using BMC.CommonLiquidation.Utilities;
using BMC.CashDeskOperator.BusinessObjects;

namespace BMC.CashDeskOperator
{
    public class ReadLiquidationDetailsConfiguration : IReadLiquidationDetails
    {
        private static ReadLiquidationDetailsConfiguration _ReadLiquidationDetailsConfiguration = null;
        private ReadLiquidationDetailsBusiness oReadLiquidationDetailsBusiness = new ReadLiquidationDetailsBusiness();

        #region Private Constructor
       
        private ReadLiquidationDetailsConfiguration() { }
       
        #endregion

        public static ReadLiquidationDetailsConfiguration ReadLiquidationDetailsConfigurationInstance
        {
            get
            {
                if (_ReadLiquidationDetailsConfiguration == null)
                    _ReadLiquidationDetailsConfiguration= new ReadLiquidationDetailsConfiguration();

                return _ReadLiquidationDetailsConfiguration;
            }
        }

        #region IReadLiquidationDetails Members

        public List<ReadLiquidationDetails> GetReadLiquidationDetails(DateTime _minDateTime, DateTime _maxDateTime) { return oReadLiquidationDetailsBusiness.GetReadLiquidationDetails(_minDateTime, _maxDateTime); }

        #endregion //IReadLiquidationDetails Members
    }
}
