using System;
using System.Collections.Generic;
using System.Linq;
using BMC.Common;
using BMC.CommonLiquidation.Utilities;
using BMC.EnterpriseDataAccess;
using BMC.Common.ExceptionManagement;

namespace BMC.EnterpriseBusiness.Business
{
    public class ReadBasedLiquidationBiz
    {
        #region DataMembers

        private static ReadBasedLiquidationBiz _ReadBasedLiquidationBiz;
        private LiquidationUtility oLiquidationUtility = null;

        #endregion //DataMembers

        #region Constructor

        public ReadBasedLiquidationBiz()
        {
            oLiquidationUtility = new LiquidationUtility();
        }

        #endregion //Constructor

        #region Static Method

        /// <summary>
        /// To create a new instance for ReadBasedLiquidationBiz and return the created instance
        /// </summary>
        /// <returns></returns>
        public static ReadBasedLiquidationBiz CreateInstance()
        {
            if (_ReadBasedLiquidationBiz == null)
                _ReadBasedLiquidationBiz = new ReadBasedLiquidationBiz();

            return _ReadBasedLiquidationBiz;
        }

        #endregion //Static Method

        #region Read Data

        public List<ReadLiquidationEntity> GetReadLiquidationRecords(int iSiteId)
        {
            try
            {
                return oLiquidationUtility.GetReadLiquidationRecords(EnterpriseDataContextHelper.ConnectionString, iSiteId).ToList();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return new List<ReadLiquidationEntity>();
            }
        }

        public List<rsp_GetActiveSiteDetailsBySettingResult> GetActiveSiteDetails(int iUserId , string defaultText = "")
        {
            string strDisplayValue = string.Empty;
            try
            {
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    List<rsp_GetActiveSiteDetailsBySettingResult> lstactiveSiteDetails = DataContext.GetActiveSiteDetailsBySetting(iUserId, "LiquidationType").ToList();
                    lstactiveSiteDetails.Insert(0, GetNoneItemSite(defaultText));
                    return lstactiveSiteDetails;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return new List<rsp_GetActiveSiteDetailsBySettingResult>();
            }
        }

        public List<rsp_GetActiveSitesForUserResult> GetActiveSitesForReport(int iUserId, string defaultText = "")
        {
            string strDisplayValue = string.Empty;
            try
            {
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    List<rsp_GetActiveSitesForUserResult> lstactiveSiteDetails = DataContext.GetActiveSitesForUser(iUserId).ToList();
                    lstactiveSiteDetails.Insert(0, GetNoneItemSiteForReport(defaultText));
                    return lstactiveSiteDetails;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return new List<rsp_GetActiveSitesForUserResult>();
            }
        }

        public List<ReadLiquidationReportRecords> GetReadLiquidationReportRecords(int iSiteId, bool bOnlyLast20Records)
        {
            try
            {
                return oLiquidationUtility.GetLiquidationDetailForReport(EnterpriseDataContextHelper.ConnectionString, iSiteId, bOnlyLast20Records ? 20 : 3000).ToList();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return new List<ReadLiquidationReportRecords>();
            }
        }

        private rsp_GetActiveSiteDetailsBySettingResult GetNoneItemSite(string defaultText = "")
        {
            try
            {
                rsp_GetActiveSiteDetailsBySettingResult objActiveSiteDetail = new rsp_GetActiveSiteDetailsBySettingResult
                {
                    Site_ID = 0,
                    Site_Code = "",
                    Site_Name = "",
                    DisplayName = (defaultText == string.Empty) ? "Please Select Site" : defaultText                 // "Please Select Site"
                };
                return objActiveSiteDetail;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return new rsp_GetActiveSiteDetailsBySettingResult();
            }
        }

        private rsp_GetActiveSitesForUserResult GetNoneItemSiteForReport(string defaultText = "")
        {
            try
            {
                rsp_GetActiveSitesForUserResult objActiveSiteDetail = new rsp_GetActiveSitesForUserResult
                {
                    Site_ID = 0,
                    Site_Code = "",
                    Site_Name = "",
                    DisplayName = (defaultText == string.Empty) ? "Please Select Site" : defaultText                    //"Please Select Site"
                };
                return objActiveSiteDetail;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return new rsp_GetActiveSitesForUserResult();
            }
        }

        public List<ReadLiquidationDetails> GetReadLiquidationDetails(int iSiteId, DateTime minDateTime, DateTime maxDateTime)
        {
            try
            {
                return oLiquidationUtility.GetReadLiquidationDetails(EnterpriseDataContextHelper.ConnectionString, iSiteId, minDateTime, maxDateTime).ToList();
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return new List<ReadLiquidationDetails>();
            }
        }

        #endregion //Read Data

        #region Liquidation Detail For Read Data

        public List<CommonLiquidationEntity> GetReadLiquidation(int iSiteId, DateTime minDateTime, DateTime maxDateTime)
        {
            try
            {
                return oLiquidationUtility.GetReadLiquidation(EnterpriseDataContextHelper.ConnectionString, iSiteId, minDateTime, maxDateTime).ToList();
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return new List<CommonLiquidationEntity>();
            }
        }

        public decimal CalculateRetailerNegativeNet(int iSiteId, decimal _profitSharePercentage)
        {
            try
            {
                return oLiquidationUtility.CalculateRetailerNegativeNet(EnterpriseDataContextHelper.ConnectionString, iSiteId, _profitSharePercentage);
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return 0.0M;
            }
        }

        public int SaveLiquidation(int iSiteId, CommonLiquidationEntity objCommonLiquidationEntity)
        {
            try
            {
                return oLiquidationUtility.SaveLiquidation(EnterpriseDataContextHelper.ConnectionString, iSiteId, objCommonLiquidationEntity);
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return 0;
            }
        }

        #endregion //Liquidation Detail For Read Data

        #region Profit Share

        public List<ProfitShareGroup> GetProfitShareGroupList(string defaultText = "")
        {
            try
            {
                List<ProfitShareGroup> lstProfitShareGroup = oLiquidationUtility.GetProfitShareGroupList(EnterpriseDataContextHelper.ConnectionString).ToList();
                lstProfitShareGroup.Insert(0, GetNoneItemofProfitShareGroup(defaultText));
                return lstProfitShareGroup;
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return new List<ProfitShareGroup>();
            }
        }

        public List<ExpenseShareGroup> GetExpenseShareGroupList(string defaultText = "")
        {
            try
            {
                List<ExpenseShareGroup> lstExpenseShareGroup = oLiquidationUtility.GetExpenseShareGroupList(EnterpriseDataContextHelper.ConnectionString).ToList();
                lstExpenseShareGroup.Insert(0, GetNoneItemofExpenseShareGroup(defaultText));
                return lstExpenseShareGroup;
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return new List<ExpenseShareGroup>();
            }
        }

        public List<PayPeriods> GetPayPeriods(int? iSiteId, string defaultText = "")
        {
            try
            {
                List<PayPeriods> lstPayPeriods = oLiquidationUtility.GetPayPeriods(EnterpriseDataContextHelper.ConnectionString, iSiteId).ToList();
                lstPayPeriods.Insert(0, GetNoneItemofPayPeriods(defaultText));
                return lstPayPeriods;
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return new List<PayPeriods>();
            }
        }

        #region Private Methods

        private ProfitShareGroup GetNoneItemofProfitShareGroup(string defaultText = "")
        {
            try
            {
                ProfitShareGroup objProfitShareGroup = new ProfitShareGroup
                {
                    ProfitShareGroupID = 0,
                    ProfitShareGroupName = (defaultText == string.Empty) ? "Please Select" : defaultText ,                   //"Please Select",
                    ProfitSharePercentage = 0
                };
                return objProfitShareGroup;
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return new ProfitShareGroup();
            }
        }

        private ExpenseShareGroup GetNoneItemofExpenseShareGroup(string defaultText = "")
        {
            try
            {
                ExpenseShareGroup objExpenseShareGroup = new ExpenseShareGroup
                {
                    ExpenseShareGroupID = 0,
                    ExpenseShareGroupName = (defaultText == string.Empty) ? "Please Select" : defaultText ,              //"Please Select",
                    ExpenseSharePercentage = 0
                };
                return objExpenseShareGroup;
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return new ExpenseShareGroup();
            }
        }

        private PayPeriods GetNoneItemofPayPeriods(string defaultText = "")
        {
            try
            {
                PayPeriods objPayPeriods = new PayPeriods
                {
                    Calendar_Period_ID = 0,
                    Calendar_Period = (defaultText == string.Empty) ? "Please Select" : defaultText                    //"Please Select",
                };
                return objPayPeriods;
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return new PayPeriods();
            }
        }

        #endregion //Private Methods

        #endregion //Profit Share
    }
}
