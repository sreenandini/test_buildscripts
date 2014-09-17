using BMC.CentralisedSiteSettings.Business;
using BMC.Common.ExceptionManagement;
using BMC.Common.LogManagement;
using BMC.EnterpriseDataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BMC.EnterpriseBusiness.Business
{
    public class CentralizedSiteSettingBiz
    {
        #region Local Declaration

        private static CentralizedSiteSettingBiz _CentralizedSiteSetting;

        #endregion Local Declaration

        #region Instance Method
        /// <summary>
        /// Creates the instance.
        /// </summary>
        /// <returns>MaintenanceBiz Object</returns>
        public static CentralizedSiteSettingBiz CreateInstance()
        {
            if (_CentralizedSiteSetting == null)
                _CentralizedSiteSetting = new CentralizedSiteSettingBiz();

            return _CentralizedSiteSetting;
        }
        #endregion Instance Method

        public DataTable GetSiteList()
        {
            try
            {
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    return DataContext.GetSiteList();
                }
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetSiteList(string SiteName, int SiteStatusID)
        {
            try
            {
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    return DataContext.GetSiteList(SiteName, SiteStatusID);
                }
            }
            catch
            {
                return new DataTable();
            }
        }

        /// <summary>
        /// Method to get the Profiles List.
        /// </summary>
        /// <returns></returns>
        public DataTable GetProfileList()
        {
            try
            {
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    return DataContext.GetProfileList();
                }
            }
            catch
            {
                return new DataTable();
            }
        }

        /// <summary>
        /// Method to get the Settings List.
        /// </summary>
        /// <returns></returns>
        public DataTable GetSettingsList(string strProfileName, bool GetAllSettings)
        {
            try
            {
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    return DataContext.GetSettingsList(strProfileName, GetAllSettings);
                }
            }
            catch
            {
                return new DataTable();
            }
        }

        /// <summary>
        /// Method to update the Individual Settings.
        /// </summary>
        /// <param name="strSettingName"></param>
        /// <param name="strSettingValue"></param>
        /// <param name="strProfileName"></param>
        public void UpdateSetting(string strSettingName, string strSettingValue, string strProfileName)
        {
            try
            {
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    DataContext.UpdateSetting(strSettingName, strSettingValue, strProfileName);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        /// <summary>
        /// Method to insert the Individual Settings.
        /// </summary>
        /// <param name="strSettingName"></param>
        /// <param name="strSettingValue"></param>
        /// <param name="strProfileName"></param>
        public void InsertSetting(string strSettingName, string strSettingValue, string strProfileName)
        {
            try
            {
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    DataContext.InsertSetting(strSettingName, strSettingValue, strProfileName);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        public int InsertProfile(string strProfileName)
        {
            int iRetVal = 0;
            try
            {
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    iRetVal = DataContext.InsertProfile(strProfileName);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return iRetVal;
        }

        public void InsertAllSetting(string strProfileName, bool GetAllSettings)
        {
            DataTable dtSettings = null;
            try
            {
                dtSettings = new DataTable();
                
                dtSettings = GetSettingsList("Default Profile", GetAllSettings);

                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    foreach (DataRow dr in dtSettings.Rows)
                    {
                        DataContext.InsertSetting(strProfileName, dr["Name"].ToString(), dr["Value"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            finally
            {
                dtSettings.Dispose();
            }
        }

        public string GetProfileNameForSite(string strSiteName)
        {
            string strProfileName = string.Empty;
            try
            {
                List<rsp_GetProfileNameForSiteResult> ResultSet = null;

                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    ResultSet = DataContext.GetProfileNameForSite(strSiteName).ToList();
                    List<SiteSettingProfileName> lstProfileName = (from Records in ResultSet
                                                                   select new SiteSettingProfileName
                                                        {
                                                            ProfileName = Records.ProfileName
                                                        }).ToList<SiteSettingProfileName>();
                    
                    strProfileName = lstProfileName.FirstOrDefault().ProfileName;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return strProfileName;
        }

        public void UpdateProfileForSite(string strSiteCode, string strProfileName)
        {
            try
            {
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    DataContext.UpdateProfileForSite(strSiteCode, strProfileName);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        public void InsertExportHistory(string iSiteID, string Site_Code)
        {
            try
            {
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    DataContext.Insert_ExportHistory(iSiteID.ToString(), "SITESETTINGS", 0, Site_Code);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

        }

        public DataTable GetSitesForProfile(string strProfileName)
        {
            try
            {
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    return DataContext.GetSitesForProfile(strProfileName);
                }
            }
            catch
            {
                return new DataTable();
            }
        }
    }
}
