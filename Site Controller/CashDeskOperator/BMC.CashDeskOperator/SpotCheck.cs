using System;
using System.Collections.Generic;
using System.Linq;
using BMC.Business.CashDeskOperator;
using BMC.Transport.CashDeskOperatorEntity;

namespace BMC.CashDeskOperator.BusinessObjects
{
    public class SpotCheckConfiguration : ISpotCheck
    {
        private static SpotCheckConfiguration _SpotCheckConfiguration = null;
        private SpotCheckBusiness oSpotCheckbusiness = new SpotCheckBusiness();

        #region Private Constructor

        private SpotCheckConfiguration() { }
       
        #endregion //Private Constructor

        public static SpotCheckConfiguration SpotCheckConfigurationInstance
        {
            get
            {
                if (_SpotCheckConfiguration == null)
                    _SpotCheckConfiguration = new SpotCheckConfiguration();

                return _SpotCheckConfiguration;
            }
        }

        #region ISpotCheck Members

        public List<Installations> GetInstallationDetails() { return oSpotCheckbusiness.GetInstallationDetails(); }
        public List<SpotCheck> GetSpotCheckSummaryDetails(int iInstallation_No, int? iPop) { return oSpotCheckbusiness.GetSpotCheckSummaryDetails(iInstallation_No, iPop); }

        #endregion //ISpotCheck Members
    }
}
