using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.Presentation.POS.Helper_classes
{
    public static class SiteLicensing
    {
        #region Enum

        /// <summary>
        /// Enum for License Key Status - should be same as Enterprise.SL_KeyStatus table value
        /// </summary>
        public enum LicenseKeyStatus
        {
            UnDefined = 0,
            Created = 1,
            Active = 2,
            Expired = 3,
            Cancelled = 4
        }

        #endregion //Enum
    }
}
