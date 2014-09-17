using System;
using System.Collections.Generic;
using System.Linq;
using BMC.EnterpriseBusiness.Entities;
using BMC.EnterpriseDataAccess;

namespace BMC.EnterpriseBusiness.Business
{
    /// <summary>
    /// MaintenanceBiz
    /// Site Maintenance Tab business logics
    /// </summary>
    public class SiteMaintenanceBiz
    {
        #region Local Declaration

        private static SiteMaintenanceBiz _SiteMaintenance;

        #endregion Local Declaration

        #region Instance Method
        /// <summary>
        /// Creates the instance.
        /// </summary>
        /// <returns>MaintenanceBiz Object</returns>
        public static SiteMaintenanceBiz CreateInstance()
        {
            if (_SiteMaintenance == null)
                _SiteMaintenance = new SiteMaintenanceBiz();

            return _SiteMaintenance;
        }
        #endregion Instance Method

        public List<TransactionKeyStatus> GetTransactionKeyStatus(int siteid)
        {
            List<TransactionKeyStatus> lstRetTransactionKeyStatus = null;
            try
            {
                List<rsp_TransactionKeyStatusResult> lstTransactionKeyStatus;
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    lstTransactionKeyStatus = DataContext.GetTransactionKeyStatus(siteid).ToList();
                }
                lstRetTransactionKeyStatus = (from Records in lstTransactionKeyStatus
                                              select new TransactionKeyStatus
                                   {
                                       TransactionKeyId = Records.TransactionKeyId,
                                       TransactionKey = Records.TransactionKey,
                                       CreatedDate = Records.CreatedDate,
                                       ExpiryDate = Records.ExpiryDate,
                                       Staff_First_Name = Records.Staff_First_Name,
                                       Staff_Last_Name = Records.Staff_Last_Name,
                                       Status = Records.Status,
                                       TransactionFlagName = Records.TransactionFlagName
                                   }).ToList<TransactionKeyStatus>();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lstRetTransactionKeyStatus;
        }

        public int UpdateAuthorizationKey(int siteID, int userId, int type, string transactionKey)
        {
            int? iResult = 0;

            try
            {
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    iResult = DataContext.UpdateAuthorizationKey(siteID, userId, type, transactionKey);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return (int)iResult;
        }

        public int UpdateTransactionKeyStatus(int transactionKeyId, int userID)
        {
            int? iResult = 0;

            try
            {
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    DataContext.UpdateTransactionKeyStatus(transactionKeyId, userID);
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
