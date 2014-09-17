using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using BMC.EnterpriseDataAccess;
using System.Collections;
using BMC.EnterpriseBusiness.Entities;
using System.Collections.ObjectModel;
using BMC.Common.ExceptionManagement;
using Audit.BusinessClasses;
using Audit.Transport;
using BMC.Common.LogManagement;
using BMC.EnterpriseDataAccess;

namespace BMC.EnterpriseBusiness.Business
{
    public static class LiquidationBusiness
    {
        #region Methods
       
        public static void UpdateBatchAdvance(int iBatchNo, float dAdvanceToRetailer)
        {
            try
            {
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    DataContext.usp_Insert_Collection_Batch_Advance(iBatchNo, dAdvanceToRetailer);
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error in Liq.BL.UpdateBatchAdvance method" + "-Error Message-" + ex.Message, LogManager.enumLogLevel.Error);
            }
        }

        public static decimal CalculateRetailerNegative(int iSiteId, int iBatchNo, decimal dRetailerPercenage)
        {
            try
            {
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    List<EnterpriseDataContext.esp_Calculate_Collection_Negative_NetResult> lstNegativeNet = new List<EnterpriseDataContext.esp_Calculate_Collection_Negative_NetResult>();
                    lstNegativeNet = DataContext.esp_Calculate_Collection_Negative_Net(iSiteId, iBatchNo, dRetailerPercenage).ToList();

                    if (lstNegativeNet != null && lstNegativeNet.Count > 0)
                        return lstNegativeNet[0].Batch_Negative_Net.GetValueOrDefault();

                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error in Liq.BL.CalculateRetailerNegative method" + "-Error Message-" + ex.Message, LogManager.enumLogLevel.Error);
            }
            return 0.0M;
        }

       
        #endregion //Methods
    }
}
