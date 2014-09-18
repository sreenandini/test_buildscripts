using System;
using System.Collections.Generic;
using System.Linq;
using BMC.Common;
using BMC.CommonLiquidation.Utilities;
//using BMC.EnterpriseDataAccess;
using BMC.Common.ExceptionManagement;
using BMC.DataAccess;
using BMC.Common.Utilities;
using BMC.DBInterface.CashDeskOperator;

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

        //public List<ReadLiquidationEntity> GetReadLiquidationRecords(int iSiteId)
        //{
        //    try
        //    {
        //        return oLiquidationUtility.GetReadLiquidationRecords(EnterpriseDataContextHelper.ConnectionString, iSiteId).ToList();
        //    }
        //    catch (Exception ex)
        //    {
        //        ExceptionManager.Publish(ex);
        //        return new List<ReadLiquidationEntity>();
        //    }
        //}

        //public List<rsp_GetActiveSiteDetailsBySettingResult> GetActiveSiteDetails(int iUserId)
        //{
        //    string strDisplayValue = string.Empty;
        //    try
        //    {
        //        using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
        //        {
        //            List<rsp_GetActiveSiteDetailsBySettingResult> lstactiveSiteDetails = DataContext.GetActiveSiteDetailsBySetting(iUserId, "LiquidationType").ToList();
        //            lstactiveSiteDetails.Insert(0, GetNoneItemSite());
        //            return lstactiveSiteDetails;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ExceptionManager.Publish(ex);
        //        return new List<rsp_GetActiveSiteDetailsBySettingResult>();
        //    }
        //}

        //private rsp_GetActiveSiteDetailsBySettingResult GetNoneItemSite()
        //{
        //    try
        //    {
        //        rsp_GetActiveSiteDetailsBySettingResult objActiveSiteDetail = new rsp_GetActiveSiteDetailsBySettingResult
        //        {
        //            Site_ID = 0,
        //            Site_Code = "",
        //            Site_Name = "",
        //            DisplayName = "Please Select Site"
        //        };
        //        return objActiveSiteDetail;
        //    }
        //    catch (Exception ex)
        //    {
        //        ExceptionManager.Publish(ex);
        //        return new rsp_GetActiveSiteDetailsBySettingResult();
        //    }
        //}

        #endregion //Read Data

        #region Liquidation Detail For Read Data

        //public List<CommonLiquidationEntity> GetReadLiquidation(int iSiteId)
        //{
        //    return oLiquidationUtility.GetReadLiquidation(EnterpriseDataContextHelper.ConnectionString, iSiteId).ToList();
        //}

        //public decimal CalculateRetailerNegativeNet(int iSiteId, decimal _profitSharePercentage)
        //{
        //    return oLiquidationUtility.CalculateRetailerNegativeNet(EnterpriseDataContextHelper.ConnectionString, iSiteId, _profitSharePercentage);
        //}

        //public int SaveLiquidation(int iSiteId, CommonLiquidationEntity objCommonLiquidationEntity)
        //{
        //    return oLiquidationUtility.SaveLiquidation(EnterpriseDataContextHelper.ConnectionString, iSiteId, objCommonLiquidationEntity);
        //}

        #endregion //Liquidation Detail For Read Data

        #region Profit Share

        public List<ProfitShareGroup> GetProfitShareGroupList()
        {
            try
            {
                List<ProfitShareGroup> lstProfitShareGroup = oLiquidationUtility.GetProfitShareGroupList(CommonDataAccess.ExchangeConnectionString).ToList();
                lstProfitShareGroup.Insert(0, GetNoneItemofProfitShareGroup());
                return lstProfitShareGroup;
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return new List<ProfitShareGroup>();
            }
        }

        public List<ExpenseShareGroup> GetExpenseShareGroupList()
        {
            try
            {
                List<ExpenseShareGroup> lstExpenseShareGroup = oLiquidationUtility.GetExpenseShareGroupList(CommonDataAccess.ExchangeConnectionString).ToList();
                lstExpenseShareGroup.Insert(0, GetNoneItemofExpenseShareGroup());
                return lstExpenseShareGroup;
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return new List<ExpenseShareGroup>();
            }
        }

        public List<PayPeriods> GetPayPeriods(int? iSiteId)
        {
            try
            {
                List<PayPeriods> lstPayPeriods = oLiquidationUtility.GetPayPeriods(CommonDataAccess.ExchangeConnectionString,iSiteId).ToList();
                lstPayPeriods.Insert(0, GetNoneItemofPayPeriods());
                return lstPayPeriods;
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return new List<PayPeriods>();
            }
        }

        #region Private Methods

        private ProfitShareGroup GetNoneItemofProfitShareGroup()
        {
            try
            {
                ProfitShareGroup objProfitShareGroup = new ProfitShareGroup
                {
                    ProfitShareGroupID = 0,
                    ProfitShareGroupName = "Please Select",
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

        private ExpenseShareGroup GetNoneItemofExpenseShareGroup()
        {
            try
            {
                ExpenseShareGroup objExpenseShareGroup = new ExpenseShareGroup
                {
                    ExpenseShareGroupID = 0,
                    ExpenseShareGroupName = "Please Select",
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

        private PayPeriods GetNoneItemofPayPeriods()
        {
            try
            {
                PayPeriods objPayPeriods = new PayPeriods
                {
                    Calendar_Period_ID = 0,
                    Date = "Please Select",
                    Calendar_Period_Start_Date = DateTime.MinValue,
                    Calendar_Period_End_Date = DateTime.MinValue
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
