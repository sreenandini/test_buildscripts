using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.Common.ExceptionManagement;
using BMC.EnterpriseDataAccess;
using BMC.EnterpriseBusiness.Entities;

namespace BMC.EnterpriseBusiness.Business
{
    public static class CommonBiz
    {
        #region Static Members

        public static string strUsername = string.Empty;
        public static int iUserId = 0;

        #endregion //Static Members

        #region Static Methods

        public static string EnsureValidString(string sInput)
        {
            try
            {
                char[] input = sInput.ToCharArray();
                StringBuilder sbResult = new StringBuilder();

                foreach (char c in input)
                {
                    int asciiCode = (int)c;
                    if ((asciiCode >= 32 || asciiCode <= 122) && asciiCode != 34 && asciiCode != 37 && asciiCode != 95 && asciiCode != 96)
                    {
                        sbResult.Append(c);
                    }
                }
                return sbResult.ToString();
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return string.Empty;
        }

        public static void GetSiteSetting(int iSiteId, string strSettingMasterName, ref string strSettingValue)
        {
            try
            {
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    DataContext.GetSiteSetting(iSiteId, strSettingMasterName, ref strSettingValue);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        #endregion //Static Methods
    }
}
