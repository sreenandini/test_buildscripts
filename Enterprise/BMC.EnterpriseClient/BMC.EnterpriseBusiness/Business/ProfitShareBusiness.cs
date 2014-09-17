using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Text;
using ProfitShare;
using BMC.EnterpriseDataAccess;
using BMC.EnterpriseBusiness.Entities;
using BMC.Common.LogManagement;
using System.Data;
using System.Transactions;

namespace ProfitShare.Business
{
    public class ProfitShareBusiness
    {
        private static ProfitShareBusiness _Profitshare = null;
        public static ProfitShareBusiness CreateInstance()
        {
            if (_Profitshare == null)
            {
                _Profitshare = new ProfitShareBusiness(); ;
            }
            return _Profitshare;

        }
        public static BindingList<GetProfitShareGroupDetailsResult> GetProfitShare(int? ProfitSHID)
        {
            using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
            {

                BindingList<GetProfitShareGroupDetailsResult> result = new BindingList<GetProfitShareGroupDetailsResult>();
                List<rsp_GetProfitShareGroupDetailsResult> dsprofitshare = DataContext.GetProfitShareGroupDetails(ProfitSHID).ToList();

                foreach (rsp_GetProfitShareGroupDetailsResult ProfitShare in dsprofitshare)
                {
                    result.Add(new GetProfitShareGroupDetailsResult()
                    {
                        ProfitShareId = ProfitShare.ProfitShareId,
                        ShareHolderId = ProfitShare.ShareHolderId,
                        ShareHolderName = ProfitShare.ShareHolderName,
                        ProfitSharePercentage = ProfitShare.ProfitSharePercentage,
                        ProfitShareDescription = ProfitShare.ProfitShareDescription
                    });
                }
                return result;
            }
        }

        //public bool DeleteProfitShare(int ProfitShareId)
        //{
        //    bool result = false;
        //    using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
        //    {
        //        int? iResult = 0;
        //        DataContext.usp_DeleteProfitShare(ProfitShareGroupId, ref iResult);
        //        result = (iResult.Value == 1);
        //    }
        //    return result;

        //}


        public static List<ProfitShareEntity> EditProfitShare(int ProfitShareId, int ShareHolderId, float ProfitSharePercentage, string ProfitShareDescription)
        {
            try
            {

                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    DataContext.usp_UpdateProfitShareDetails(ShareHolderId, 0, ProfitShareId, ProfitSharePercentage, ProfitShareDescription);
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error : Edit ProfitShare : " + ex.Message, LogManager.enumLogLevel.Error);
            }
            return null;
        }

        public void AddProfitShare(float ProfitSharePercentage, string ProfitShareDescription)
        {
            try
            {

                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    DataContext.usp_InsertProfitShareDetails(ProfitSharePercentage, ProfitShareDescription);
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error : Adding ProfitShare : " + ex.Message, LogManager.enumLogLevel.Error);
            }
        }

        public int IsNameExists(string ProfitShareGroupName, ref int? blNameExists)
        {
            try
            {
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    return DataContext.rsp_CheckProfitShareGroupNameExists(ProfitShareGroupName, 0, ref blNameExists);
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error : LoadShareHolderPercentage : " + ex.Message, LogManager.enumLogLevel.Error);
            }
            return 0;
        }


        public bool AddProfitSHDetails(int ProfitSHGroupID, List<GetProfitShareGroupDetailsResult> lst_profitSH, ref string ErrorMsg)
        {
            bool retval = true;
            try
            {
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    using (var transaction = new TransactionScope())
                    {
                        foreach (GetProfitShareGroupDetailsResult Profit_SH in lst_profitSH)
                        {
                            ErrorMsg = "ShareHolderId :" + Profit_SH.ShareHolderId + " ;ProfitShareId:" + Profit_SH.ProfitShareId;
                            retval = false;

                            int? ProfitShareId = null;
                            if (Profit_SH.ProfitShareId > 0)
                            {
                                ProfitShareId = Profit_SH.ProfitShareId;
                            }
                            DataContext.usp_AddProfitSHDetails(ProfitSHGroupID, ProfitShareId, Profit_SH.ProfitSharePercentage, Profit_SH.ProfitShareDescription, Profit_SH.ShareHolderId);

                        }
                        transaction.Complete();
                        retval = true;
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error : LoadShareHolderPercentage : " + ex.Message, LogManager.enumLogLevel.Error);
                retval = false;
            }
            return retval;
        }


        public List<rsp_GetShareHolderNameListResult> LoadShareHolderDetails()
        {
            try
            {
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    return DataContext.rsp_GetShareHolderNameList().ToList();
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error : LoadShareHolderDetails : " + ex.Message, LogManager.enumLogLevel.Error);
                return null;
            }
        }

        public List<ShareHolderDetailsResult> GetShareHolderDetails(int? ShareHolderID)
        {

            //call method to db layer
            using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
            {
                List<ShareHolderDetailsResult> result = new List<ShareHolderDetailsResult>();
                List<rsp_GetShareHolderDetailsResult> dsShareHolder = DataContext.GetShareHolderDetails(ShareHolderID).ToList();
                foreach (rsp_GetShareHolderDetailsResult ShareHolder in dsShareHolder)
                {
                    result.Add(new ShareHolderDetailsResult()
                    {
                        ShareHolderId = ShareHolder.ShareHolderId,
                        ShareHolderName = ShareHolder.ShareHolderName,
                        ShareHolderDescription = ShareHolder.ShareHolderDescription,

                    });
                }
                return result;
            }
        }
    }
}
