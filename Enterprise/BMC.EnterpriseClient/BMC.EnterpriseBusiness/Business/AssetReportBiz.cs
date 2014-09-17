using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.EnterpriseBusiness.Entities;
using BMC.EnterpriseDataAccess;
using System.Linq;
using System.Data.Linq;
using BMC.Common.ExceptionManagement;

namespace BMC.EnterpriseBusiness.Business
{
    public class AssetReportBiz
    {
        private static AssetReportBiz _buy;

        /// <summary>
        /// create an instance for the class.
        /// </summary>
        /// <returns></returns>
        public static AssetReportBiz CreateInstance()
        {
            if (_buy == null)
            {
                _buy = new AssetReportBiz();
            }
            return _buy;
        }

        /// <summary>
        /// Get the asset report details from DB.
        /// </summary>
        /// <param name="asset"></param>
        /// <returns>List of the asset report details</returns>
        public List<AssetReportResult> LoadAssetReportDetails(AssetParams asset)
        {
            try
            {
                ISingleResult<EnterpriseDataContext.rsp_AssetReportResult> lstAssetReport = null;
                List<AssetReportResult> lstAssetRepResult = null;

                //get the details
                using (EnterpriseDataContext context = EnterpriseDataContextHelper.GetDataContext())
                {
                    lstAssetReport = context.AssetReport(Common.Utilities.Common.GetUniversalDate(asset.StartDate),
                       Common.Utilities.Common.GetUniversalDate(asset.EndDate), asset.SiteID, asset.Category);

                    //Create a new record based on the return parameter
                    lstAssetRepResult = (from Records in lstAssetReport
                                         select new AssetReportResult
                                         {
                                             ActPerc = Records.ActPerc,
                                             asset = Records.asset,
                                             CasinoWin = Records.CasinoWin,
                                             Category = Records.Category,
                                             DailyWin = Records.DailyWin,
                                             GameName = Records.GameName,
                                             Handle = Records.Handle,
                                             Machine_ID = Records.Machine_ID,
                                             Manu = Records.Manu,
                                             Model = Records.Model,
                                             PercVar = Records.PercVar,
                                             Position = Records.Position,
                                             RDCCashOut = Records.RDCCashOut,
                                             rn = Records.rn,
                                             Site_Name = Records.Site_Name,
                                             TheoPerc = Records.TheoPerc,
                                             Type = Records.Type,
                                             Zone_Name = Records.Zone_Name

                                         }).ToList<AssetReportResult>();

                }
                return lstAssetRepResult;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return new List<AssetReportResult>();
            }
           
        }

        /// <summary>
        /// Get the  asset history details from DB.
        /// </summary>
        /// <param name="asset"></param>
        /// <returns>List of the asset history details</returns>
        /// 
        public List<DetailedHistoryResult> LoadAssetHistoryDetails(AssetHistoryParams history)
        {
            try
            {
                ISingleResult<EnterpriseDataContext.rsp_GetDetailedHistoryResult> lstAssetHistory = null;
                List<DetailedHistoryResult> lstAssetRepResult = null;

                //get the details
                using (EnterpriseDataContext context = EnterpriseDataContextHelper.GetDataContext())
                {
                    lstAssetHistory = context.DetailedHistory(history.BarPositionID, history.IsDetailed, history.SiteID);

                    //Create a new record based on the return parameter
                    lstAssetRepResult = (from Records in lstAssetHistory
                                         select new DetailedHistoryResult
                                         {
                                             AvgDailyWin = Records.AvgDailyWin,
                                             Collection_CashTake = Records.Collection_CashTake,
                                             Collection_Company_Share = Records.Collection_Company_Share,
                                             Collection_Days = Records.Collection_Days,
                                             Collection_Gross = Records.Collection_Gross,
                                             Collection_ID = Records.Collection_ID,
                                             Collection_Location_Share = Records.Collection_Location_Share,
                                             Collection_Other_Share = Records.Collection_Other_Share,
                                             Collection_Supplier_Share = Records.Collection_Supplier_Share,
                                             Depot_Name = Records.Depot_Name,
                                             GameName = Records.GameName,
                                             Installation_End_Date = Records.Installation_End_Date,
                                             Installation_ID = Records.Installation_ID,
                                             Installation_Jackpot_Value = Records.Installation_Jackpot_Value,
                                             Installation_Price_Per_Play = Records.Installation_Price_Per_Play,
                                             Installation_Start_Date = Records.Installation_Start_Date,
                                             Machine_Alternative_Serial_Numbers = Records.Machine_Alternative_Serial_Numbers,
                                             Machine_BACTA_Code = Records.Machine_BACTA_Code,
                                             Machine_Class_ID = Records.Machine_Class_ID,
                                             Machine_Class_Model_Code = Records.Machine_Class_Model_Code,
                                             Machine_Manufacturers_Serial_No = Records.Machine_Manufacturers_Serial_No,
                                             Machine_Name = Records.Machine_Name,
                                             Machine_Stock_No = Records.Machine_Stock_No,
                                             Machine_Type_Code = Records.Machine_Type_Code,
                                             Operator_Name = Records.Operator_Name

                                         }).ToList<DetailedHistoryResult>();

                }
                return lstAssetRepResult;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return new List<DetailedHistoryResult>();
            }
        }

        /// <summary>
        /// Get the current BMC Version
        /// </summary>
        /// <returns></returns>
        public string GetBMCVersion()
        {
            try
            {
                //if version is null return 12.4 by default
                using (EnterpriseDataContext context = EnterpriseDataContextHelper.GetDataContext())
                {
                    List<EnterpriseDataContext.SProcResult> l_res = context.GetBMCVersion().ToList();
                    if (l_res.Count > 0)
                    {
                        return l_res[0].Result;
                    }
                    else
                    {
                        return "12.4.3";
                    }
                }             

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return string.Empty;
            }
        }

    }
}
