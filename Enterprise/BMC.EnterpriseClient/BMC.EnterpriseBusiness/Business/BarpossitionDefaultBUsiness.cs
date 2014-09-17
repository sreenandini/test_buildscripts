using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.EnterpriseBusiness.Entities;
using BMC.EnterpriseDataAccess;
using BMC.Common.LogManagement;
using BMC.Common.ExceptionManagement;

namespace BMC.EnterpriseBusiness.Business
{
    public class BarpossitionDefaultBUsiness
    {

        private static BarpossitionDefaultBUsiness _BPDefault;
        public BarpossitionDefaultBUsiness() { }
        public static BarpossitionDefaultBUsiness CreateInstance()
        {
            if (_BPDefault == null)
                _BPDefault = new BarpossitionDefaultBUsiness();
            return _BPDefault;
        }
        EnterpriseDataContext dbContextBPdefault = null;

        public List<BPSiteJackpotResult> BPSiteJackpotdetails(int value)
        {
            List<BPSiteJackpotResult> objcoll = new List<BPSiteJackpotResult>();
            try
            {
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    foreach (var entity in DataContext.BPSiteJackpot(value))
                    {
                        objcoll.Add(new BPSiteJackpotResult()
                        {
                            Site_Jackpot = entity.Site_Jackpot
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            LogManager.WriteLog("End of Get DefaultTermsgroupDefaultsResult", LogManager.enumLogLevel.Info);
            return objcoll;
        }

        public List<BPSitePercentagePayoutResult> BPSitePercentagePayoutDetail(int value)
        {
            List<BPSitePercentagePayoutResult> objcoll = new List<BPSitePercentagePayoutResult>();
            try
            {
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    foreach (var entity in DataContext.BPSitePercentagePayout(value))
                    {
                        objcoll.Add(new BPSitePercentagePayoutResult()
                        {
                            Site_Percentage_Payout = entity.Site_Percentage_Payout
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            LogManager.WriteLog("End of Get DefaultTermsgroupDefaultsResult", LogManager.enumLogLevel.Info);
            return objcoll;
        }



        public List<BPSitePricePerPlayResult> BPSitePricePerPlay(int value)
        {
            List<BPSitePricePerPlayResult> objcoll = new List<BPSitePricePerPlayResult>();
            try
            {
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    foreach (var entity in DataContext.BPSitePricePerPlay(value))
                    {
                        objcoll.Add(new BPSitePricePerPlayResult()
                        {
                            Site_Price_Per_Play = entity.Site_Price_Per_Play
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            LogManager.WriteLog("End of Get DefaultTermsgroupDefaultsResult", LogManager.enumLogLevel.Info);
            return objcoll;
        }



        public List<GetBPTermsgroupResult> TermsgroupResult(int value)
        {
            List<GetBPTermsgroupResult> objcoll = new List<GetBPTermsgroupResult>();
            try
            {
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    foreach (var entity in DataContext.GetBPTermsgroup(value))
                    {
                        objcoll.Add(new GetBPTermsgroupResult()
                        {
                            Terms_Group_ID = entity.Terms_Group_ID,
                            Terms_Group_Name = entity.Terms_Group_Name

                        });
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            LogManager.WriteLog("End of Get DefaultTermsgroupDefaultsResult", LogManager.enumLogLevel.Info);
            return objcoll;
        }

        public List<GetBPDefaulloadvalueResult> LoadBPDefaultdetail(int value)
        {
            List<GetBPDefaulloadvalueResult> objcoll = new List<GetBPDefaulloadvalueResult>();
            try
            {
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    foreach (var entity in DataContext.GetBPDefaultDetails(value))
                    {
                        objcoll.Add(new GetBPDefaulloadvalueResult()
                        {
                            Access_Key_ID = entity.Access_Key_ID,
                            Access_Key_ID_Default = entity.Access_Key_ID_Default,
                            Bar_Position_Jackpot = entity.Bar_Position_Jackpot,
                            Bar_Position_Jackpot_Default = entity.Bar_Position_Jackpot_Default,
                            Bar_Position_Percentage_Payout = entity.Bar_Position_Percentage_Payout,
                            Bar_Position_Percentage_Payout_Default = entity.Bar_Position_Percentage_Payout_Default,
                            Bar_Position_Price_Per_Play = entity.Bar_Position_Price_Per_Play,
                            Bar_Position_Price_Per_Play_Default = entity.Bar_Position_Price_Per_Play_Default,
                            Site_ID = entity.Site_ID,
                            Terms_Group_Changeover_Date = entity.Terms_Group_Changeover_Date,
                            Terms_Group_Future_ID = entity.Terms_Group_Future_ID,
                            Terms_Group_ID = entity.Terms_Group_ID,
                            Terms_Group_ID_Default = entity.Terms_Group_ID_Default,
                            Terms_Group_Past_Changeover_Date = entity.Terms_Group_Past_Changeover_Date,
                            Terms_Group_Past_ID = entity.Terms_Group_Past_ID,
                            Zone_ID = entity.Zone_ID
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            LogManager.WriteLog("End of Get DefaultTermsgroupDefaultsResult", LogManager.enumLogLevel.Info);
            return objcoll;
        }

        public void UpdateBarPositionTermsGroup(
            int barPosId,
            int siteId,
            bool isPricePerPlay,
            int pricePerPlay,
            bool isJackpot,
            int jackpot,
            bool isPayout,
            int payout,
            bool isAccessKey,
            int accessKey,
            bool isTermsGroup,
            int termsGroupId,
            int termsGroupPastId,
            int termsGroupFutureId,
            DateTime termsGroupPastChangeOverDate,
            DateTime termsGroupChangeOverDate,
            int userId,
            string userName,
            string moduleName,
            int moduleID)
        {
            using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
            {
                DataContext.UpdateBarPositionTermsGroup(barPosId, siteId, isPricePerPlay, pricePerPlay, isJackpot, jackpot, isPayout, payout, isAccessKey, accessKey, isTermsGroup, termsGroupId, termsGroupPastId, termsGroupFutureId, termsGroupPastChangeOverDate, termsGroupChangeOverDate, userId, userName, moduleName, moduleID);
            }
        }

        public List<getBPAccessKeyResult> BPAccessKeyResult(int value)
        {
            List<getBPAccessKeyResult> objcoll = new List<getBPAccessKeyResult>();
            try
            {
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    foreach (var entity in DataContext.getBPAccessKey(value))
                    {
                        objcoll.Add(new getBPAccessKeyResult()
                        {
                            Access_Key_ID = entity.Access_Key_ID,
                            Access_Key_Name = entity.Access_Key_Name

                        });
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            LogManager.WriteLog("End of Get DefaultTermsgroupDefaultsResult", LogManager.enumLogLevel.Info);
            return objcoll;
        }
    }

}
