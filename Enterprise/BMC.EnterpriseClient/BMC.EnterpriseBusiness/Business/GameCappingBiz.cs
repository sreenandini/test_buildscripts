using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.EnterpriseDataAccess;
using BMC.Common.ExceptionManagement;
using BMC.EnterpriseBusiness.Entities;

namespace BMC.EnterpriseBusiness.Business
{
    public class GameCappingBiz
    {
        /// <summary>
        /// Insert GameCapping Settings Details.
        /// </summary>
        /// <param name="bCapRelase"></param>
        /// <param name="bPlayerReserve"></param>
        /// <param name="bEmployeeReserve"></param>
        /// <param name="iMintsToExpire"></param>
        /// <param name="iSite"></param>
        /// <returns></returns>
        public int InsertGameCappingParameters(bool bCapRelase, bool bPlayerReserve, bool bEmployeeReserve, int iMintsToExpire, int iSite,int iStaffId,int iModuleId,string sModuleName,string sScreenName)
        {            
            try
            {
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    return DataContext.InsertGameCappingParameters(bCapRelase, bPlayerReserve, bEmployeeReserve, iMintsToExpire, iSite, iStaffId, iModuleId, sModuleName, sScreenName);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return 0;
            }
            
        }

        /// <summary>
        /// Insert Card Level Details.
        /// </summary>
        /// <param name="iMaxMachinesToCap"></param>
        /// <param name="iCardLevel"></param>
        /// <param name="sMintsToCap"></param>
        /// <param name="sSite"></param>
        /// <returns></returns>
        public int InsertCardLevelSettings(int? iMaxMachinesToCap, int? iCardLevel, string sMintsToCap, string sSite, int iStaffId, int iModuleId, string sModuleName, string sScreenName)
        {            
            try
            {
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    return DataContext.InsertCardLevelSettingsForGameCap(iMaxMachinesToCap, iCardLevel, sMintsToCap, sSite, iStaffId, iModuleId, sModuleName, sScreenName);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return 0;
            }          
        }

        /// <summary>
        /// Insert Card Level Details.
        /// </summary>
        /// <param name="iMaxMachinesToCap"></param>
        /// <param name="iCardLevel"></param>
        /// <param name="sMintsToCap"></param>
        /// <param name="sSite"></param>
        /// <returns></returns>
        public int DeleteGameCappingCardLevel(int? iCardLevel, string sSite, int iStaffId, int iModuleId, string sModuleName, string sScreenName)
        {
            try
            {
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    return DataContext.DeleteGameCappingCardLevel(iCardLevel, sSite, iStaffId, iModuleId, sModuleName, sScreenName);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return 0;
            }
        }
        /// <summary>
        /// Gets CardLevel Details to Load in UI.
        /// </summary>
        /// <param name="iCardLevel"></param>
        /// <param name="iSiteID"></param>
        /// <returns></returns>
        public List<GetCardLevelSettings> GetCardLevelSettings(int iSiteID)
        {
            List<GetCardLevelSettings> objCardLevel = null;
            try
            {
                List<GetCardLevelSettingsResult> objCardLevelResult;
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    objCardLevelResult = DataContext.GetCardLevelSettings(iSiteID).ToList();      
             
                }
                if (objCardLevelResult.Count != 0)
                {
                    objCardLevel = (from obj in objCardLevelResult
                                    select new GetCardLevelSettings
                                    {
                                        CardLevel = obj.CardLevel,
                                        MaxNoofMachinestoCap = obj.MaxNoofMachinestoCap,
                                        MintstoCap = obj.MintstoCap,
                                        SettingID = obj.SettingId

                                    }).ToList<GetCardLevelSettings>();
                }                
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return objCardLevel;
        }

        /// <summary>
        /// Get Game Capping Setting details to Load into UI.
        /// </summary>
        /// <param name="iSiteID"></param>
        /// <returns></returns>
        public GetGameCappingParametersEntity GetGameMappingParameters(int iSiteID)
        {
            GetGameCappingParametersEntity objCardLevel = null;
            try
            {
                List<GetGameCappingParametersResult> objCardLevelResult;
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    objCardLevelResult = DataContext.GetGameCappingParameters(iSiteID).ToList();
                }
                if (objCardLevelResult.Count != 0)
                {
                    objCardLevel = (from obj in objCardLevelResult
                                    select new GetGameCappingParametersEntity
                                    {
                                        CapReleaseOnPlayerCardIn = obj.CapReleaseOnPlayerCardIn,
                                        GameCapID = obj.GameCapID,
                                        MintsToExpire = obj.MintsToExpire,
                                        ReserveGameForEmployee = obj.ReserveGameForEmployee,
                                        ReserveGameForPlayer = obj.ReserveGameForPlayer,
                                        SITE = obj.SITE
                                    }).Single<GetGameCappingParametersEntity>();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return objCardLevel;

        }

        /// <summary>
        /// To Export the Details to Exchange.
        /// </summary>
        /// <param name="Reference"></param>
        /// <param name="Type"></param>
        /// <param name="SiteId"></param>
        /// <returns></returns>
        public int ExportHistory(string Reference, string Type, int SiteId)
        {
            try
            {
                using (EnterpriseDataContext Datacontext = EnterpriseDataContextHelper.GetDataContext())
                {
                    return Datacontext.usp_Export_History(Reference, Type, SiteId);
                }
            }
            catch (Exception ex)
            {                
                ExceptionManager.Publish(ex);
                return 0;
            }
        }
    }
}
