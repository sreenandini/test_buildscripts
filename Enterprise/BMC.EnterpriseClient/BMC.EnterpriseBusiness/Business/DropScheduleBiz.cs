using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.EnterpriseDataAccess;
using System.Data.Linq;
using System.Collections;
using BMC.EnterpriseBusiness.Entities;
using System.Collections.ObjectModel;
using System.Xml.Linq;
using BMC.Common.LogManagement;

namespace BMC.EnterpriseBusiness.Business
{
    [Flags]
    public enum WeekDays
    {
        Sunday = 1,
        Monday = 2,
        Tuesday = 4,
        Wednesday = 8,
        Thursday = 16,
        Friday = 32,
        Saturday = 64
    }

    public class DropScheduleBiz
    {
        private static DropScheduleBiz _DropScedule;
        //private int WeekDaysValue = 0;
        public DropScheduleEntity ScheduleEntity = new DropScheduleEntity();
        public RegionNameModel RegionEntity = new RegionNameModel();
        private static string rootNode = "BMCRequest";
        private DropScheduleBiz()
        {
        }

        public static DropScheduleBiz CreateInstance()
        {
            if (_DropScedule == null)
                _DropScedule = new DropScheduleBiz();

            return _DropScedule;
        }

        public List<RegionNameModel> GetRegion()
        {
            List<RegionNameModel> objcoll = null;
            try
            {
                List<rsp_GetRegionResult> regList;
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    regList = DataContext.GetRegion().ToList();
                }
                objcoll = (from obj in regList
                           select new RegionNameModel
                           {
                               Sub_Company_Region_ID = obj.Sub_Company_Region_ID,
                               Sub_Company_Region_Name = obj.Sub_Company_Region_Name
                           }).ToList<RegionNameModel>();
               // objcoll.Insert(0, new RegionNameModel { Sub_Company_Region_Name = "All" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objcoll;
        }

        public List<SiteDetailsModel> GetSite(int Region)
        {
            List<SiteDetailsModel> objcoll = null;
            try
            {
                List<rsp_GetSiteOnRegionResult> siteList;
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    siteList = DataContext.GetSiteOnRegion(Region).ToList();
                }
                objcoll = (from obj in siteList
                           select new SiteDetailsModel
                           {
                               Site_ID = obj.Site_ID,
                               Site_Name = obj.Site_Name
                           }).ToList<SiteDetailsModel>();

                //if (Region != 0 && objcoll.Count == 0)
                //{
                //    objcoll.Insert(0, new SiteDetailsModel { Site_Name = "None" });
                //}
                //else
                //{
                //    objcoll.Insert(0, new SiteDetailsModel { Site_Name = "All" });
                //}
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objcoll;
        }

        public void InsertUpdateDropSchedule(DropScheduleEntity objDropSch)
        {
            try
            {
            using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
            {
                int Status;
                int? scheduleIdOut = 0;
                Status = DataContext.InsertUpdateDropSchedule
                    (objDropSch.ScheduleId,
                    objDropSch.ScheduleName,
                    objDropSch.ScheduleTime,
                    objDropSch.StackerLevelPercentage,
                    Convert.ToByte(objDropSch.ScheduleOccurance),
                    objDropSch.StartDate,
                    objDropSch.EndDate,
                    Convert.ToByte(objDropSch.EndOption),
                    objDropSch.TotalOccurances,
                    Convert.ToByte(objDropSch.WeekDays),
                    objDropSch.MonthDuration,
                    Convert.ToByte(objDropSch.DayOfMonth),
                    objDropSch.NextOcc,
                    objDropSch.IsActive,
                    Convert.ToInt32(objDropSch.DropAlertType),
                    objDropSch.RegionId,
                    objDropSch.SiteId,
                    ref scheduleIdOut
                );
                objDropSch.ScheduleId = scheduleIdOut.Value;
            }
        }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error in InsertUpdateDropSchedule Method in DropScheduleBiz" + "-Error Message-" + ex.Message, LogManager.enumLogLevel.Error);  
            }
        }

        public void InsertUpdateDropScheduleHistory(int? DropScheduleHistoryID, DropScheduleEntity objDropSch,DateTime? ExecutedTime,int Status)
        {
            using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
            {
                
                Status = DataContext.InsertUpdateDropScheduleHistory
                    (
                    DropScheduleHistoryID,
                    objDropSch.ScheduleId,
                    objDropSch.ScheduleName,
                    objDropSch.ScheduleTime,
                    objDropSch.StackerLevelPercentage,
                    Convert.ToByte(objDropSch.ScheduleOccurance),
                    objDropSch.StartDate,
                    objDropSch.EndDate,
                    Convert.ToByte(objDropSch.EndOption),
                    objDropSch.TotalOccurances,
                    Convert.ToByte(objDropSch.WeekDays),
                    objDropSch.MonthDuration,
                    Convert.ToByte(objDropSch.DayOfMonth),
                    objDropSch.NextOcc,
                    objDropSch.IsActive,
                    ExecutedTime,
                    Status
    
                );
            }
        }

        public void GetSetting(int setting_ID, string setting_Name, string setting_Default, ref string setting_Value)
        {
            using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
            {
                DataContext.GetSetting(setting_ID, setting_Name, setting_Default, ref setting_Value);
            }
        }

        public List<DropScheduleEntity> GetDropSchedule(int ScheduleID,DateTime? currentDate)
        {
            List<DropScheduleEntity> objcoll = null;
            try
            {
                List<rsp_GetDropScheduleAutoResult> DetailList;
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    DetailList = DataContext.GetDropScheduleAuto(ScheduleID, currentDate).ToList();
                }
                objcoll = (from obj in DetailList
                           select new DropScheduleEntity
                           {
                               ScheduleId = obj.ScheduleID,
                               ScheduleName = obj.ScheduleName,
                               ScheduleTime = obj.ScheduleTime,
                               StackerLevelPercentage = Convert.ToByte(obj.StackerLevel),
                               ScheduleOccurance = (ScheduleOccurances)obj.ScheduleType,
                               StartDate = Convert.ToDateTime(obj.StartDate),
                               EndDate = Convert.ToDateTime(obj.EndDate),
                               EndOption = (EndOptions)obj.OccurrenceType,
                               TotalOccurances = Convert.ToInt32(obj.TotalOccurrence),
                               WeekDays = Convert.ToInt32(obj.WeekDays),
                               SelectedWeekDays = SetSelectedWeekDays(Convert.ToInt32(obj.WeekDays)),
                               MonthDuration = Convert.ToInt32(obj.MonthDuration),
                               DayOfMonth = Convert.ToInt32(obj.DateofMonth),
                               NextOcc = Convert.ToDateTime(obj.NextOcc),
                               IsActive = Convert.ToBoolean(obj.IsActive)
                           }).ToList<DropScheduleEntity>();

            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error in GetDropSchedule Method in DropScheduleBiz" + "-Error Message-" + ex.Message, LogManager.enumLogLevel.Error);  
            }
            return objcoll;
        }

        private string SetSelectedWeekDays(int WeekDays)
        {
            if (WeekDays == 0) //In case of new Drop Schedule creation
                return "";

            int result = 0;
            string SelectedDays = string.Empty;
            
            try
            {
                result = WeekDays & 1;
                SelectedDays = (result == 1) ? "Su" : "";

                result = WeekDays & 2;
                SelectedDays += (result == 2) ? ",Mo" : "";

                result = WeekDays & 4;
                SelectedDays += (result == 4) ? ",Tu" : "";

                result = WeekDays & 8;
                SelectedDays += (result == 8) ? ",We" : "";

                result = WeekDays & 16;
                SelectedDays += (result == 16) ? ",Th" : "";

                result = WeekDays & 32;
                SelectedDays += (result == 32) ? ",Fr" : "";

                result = WeekDays & 64;
                SelectedDays += (result == 64) ? ",Sa" : "";

                if (SelectedDays.Trim().StartsWith(","))
                {
                    SelectedDays = SelectedDays.Remove(0,1); 
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error in SelectedWeekDays calculation in DropScheduleBiz" + "-Error Message-" + ex.Message, LogManager.enumLogLevel.Error);  
                return "";
            }
            return SelectedDays;
        }
        public List<DropScheduleEntity> GetDropSchedule(DateTime? currentDate)
        {
            return GetDropSchedule(0, currentDate);
        }

        public bool DeleteDropSchedule(int ScheduleId)
        {
            try
            {
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    DataContext.DeleteDropSchedule(ScheduleId);
                }
                return true;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error in DeleteDropSchedule Method in DropScheduleBiz" + "-Error Message-" + ex.Message, LogManager.enumLogLevel.Error);  
                return false;
            }

        }

        /// <summary>
        /// Overload method for Auto Drop Alert
        /// </summary>
        /// <param name="Region"></param>
        /// <param name="SiteID"></param>
        /// <param name="StackerLevel"></param>
        /// <returns></returns>
        public List<ManualDropScheduleEntity> GetManualDropAlertDetails(int Region, int SiteID, int StackerLevel)
        {
            return GetManualDropAlertDetails(Region,SiteID,StackerLevel,0);
        }


        public List<ManualDropScheduleEntity> GetManualDropAlertDetails(int Region, int SiteID, int StackerLevel, int StaffID)
        {
            List<ManualDropScheduleEntity> objcoll = null;
            try
            {
                List<rsp_GetManualDropAlertDetailsResult> DetailList;
                                
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    DetailList = DataContext.GetManualDropAlertDetails(Region, SiteID, StackerLevel, StaffID).ToList();
                }
                objcoll = (from obj in DetailList
                           select new ManualDropScheduleEntity
                           {
                               INSTALLATION_ID = obj.INSTALLATION_ID,
                               BAR_POSITION_ID = obj.BAR_POSITION_ID,
                               MACHINE_ID = obj.MACHINE_ID,
                               VERSION_NAME= obj.VERSIONNAME,
                               BAR_POSITION_NAME = obj.BAR_POSITION_NAME,
                               SITE_ID = obj.SITE_ID,
                               Sub_Company_Area_Name = obj.SUB_COMPANY_AREA_NAME,
                               SITE_NAME = obj.SITE_NAME,
                               SITE_CODE = obj.SITE_CODE,
                               Sub_Company_Region_Name = obj.SUB_COMPANY_REGION_NAME,
                               SUB_COMPANY_ID = obj.SUB_COMPANY_ID,
                               Sub_Company_Name = obj.Sub_Company_Name,
                               COMPANY_ID = obj.COMPANY_ID,
                               COMPANY_NAME = obj.COMPANY_NAME,
                               AssetNumber = obj.AssetNumber,
                               STACKER_ID = obj.STACKER_ID,
                               STACKERSIZE = obj.STACKERSIZE,
                               TOTALQTY = obj.TOTALQTY,
                               PERCENTAGE = obj.PERCENTAGE,
                               EmployeeName = obj.EmployeeName
                           }).ToList<ManualDropScheduleEntity>();
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error in GetManualDropAlertDetails Method in DropScheduleBiz" + "-Error Message-" + ex.Message, LogManager.enumLogLevel.Error);                  
            }
            return objcoll;
        }

        public List<String> DropAlertToXml(List<ManualDropScheduleEntity> dropScheduleEntites)
        {
            List<String> xmls = new List<string>();
            Dictionary<Int32, String> siteIDInfo = new Dictionary<int, string>();
            Dictionary<Int32, String> moreInfos = new Dictionary<int, string>();

            StringBuilder build = new StringBuilder(string.Empty);
            try
            {
            foreach (ManualDropScheduleEntity item in dropScheduleEntites)
            {
                if (!siteIDInfo.ContainsKey(item.SITE_ID))
                {
                    foreach (var prop in item.GetType().GetProperties())
                    {
                        build.Append(String.Format("<{0}>{1}</{0}>\r\n", prop.Name, prop.GetValue(item, null)));
                    }
                    siteIDInfo.Add(item.SITE_ID, build.ToString());
                    build = new StringBuilder();
                }
                else
                {
                    if (moreInfos.ContainsKey(item.SITE_ID))
                    {
                        moreInfos[item.SITE_ID] += "," + item.BAR_POSITION_NAME;
                    }
                    else
                    {
                        moreInfos.Add(item.SITE_ID, item.BAR_POSITION_NAME);
                    }

                }
            }
            foreach (var item in siteIDInfo)
            {
                String temp = moreInfos.ContainsKey(item.Key) ? String.Format("{2}<{0}>{1}</{0}>\r\n", "MoreInfo", moreInfos[item.Key], item.Value) : siteIDInfo[item.Key];
                xmls.Add(String.Format("<{0}>\r\n{1}</{0}>",rootNode, temp));
            }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error in DropAlertToXml Method in DropScheduleBiz" + "-Error Message-" + ex.Message, LogManager.enumLogLevel.Error);                  
            }
            return xmls;
        }

        public void STM_Export_History(string type, System.Nullable<int> clientID, string site_Code, XElement xmlMessage)
        {
            try
            {
            using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
            {
                DataContext.STM_Export_History(type, clientID, site_Code, xmlMessage);
            }
        }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error in STM_Export_History Method in DropScheduleBiz" + "-Error Message-" + ex.Message, LogManager.enumLogLevel.Error);
    }
}
    }
}
