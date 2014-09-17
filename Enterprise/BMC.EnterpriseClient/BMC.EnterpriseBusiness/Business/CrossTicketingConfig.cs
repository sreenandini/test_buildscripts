using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.EnterpriseBusiness.Entities;
using BMC.EnterpriseDataAccess;

namespace BMC.EnterpriseBusiness.Business
{
    public class CrossTicketingConfigBiz
    {
        #region Local Declaration

        private static CrossTicketingConfigBiz _CrossTicketingConfigBiz;

        #endregion Local Declaration

        #region Instance Method
        /// <summary>
        /// Creates the instance.
        /// </summary>
        /// <returns>MaintenanceBiz Object</returns>
        public static CrossTicketingConfigBiz CreateInstance()
        {
            if (_CrossTicketingConfigBiz == null)
                _CrossTicketingConfigBiz = new CrossTicketingConfigBiz();

            return _CrossTicketingConfigBiz;
        }
        #endregion Instance Method

        public List<CrossTicketingSetting> GetCrossTicketConfigSites(string SiteCode)
        {
            List<CrossTicketingSetting> lstRetCrossTicketingSetting = null;
            try
            {
                List<CrossTicketingSettingResult> lstCrossTicketingSetting;
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    lstCrossTicketingSetting = DataContext.GetCrossTicketConfigSites(SiteCode).ToList();
                }
                lstRetCrossTicketingSetting = (from Records in lstCrossTicketingSetting
                                              select new CrossTicketingSetting
                                              {
                                                  ClientSiteCode = Records.ClientSiteCode,
                                                  HostSiteURL = Records.HostSiteURL,
                                                  IsCashableRedeemable = Records.IsCashableRedeemable,
                                                  IsPromoRedeemable = Records.IsPromoRedeemable
                                              }).ToList<CrossTicketingSetting>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lstRetCrossTicketingSetting;
        }

        public int InsertIntoSiteAlliance(string clientSiteCode, string hostSiteCode, string hostSiteURL, bool isCashableRedeemable, bool isPromoRedeemable)
        {
            int? iResult = 0;

            try
            {
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    iResult = DataContext.InsertIntoSiteAlliance(clientSiteCode, hostSiteCode, hostSiteURL, isCashableRedeemable, isPromoRedeemable);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return (int)iResult;
        }
    }
}
