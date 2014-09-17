using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.EnterpriseDataAccess;
using BMC.EnterpriseBusiness.Entities;
using System.Data;
using BMC.DataAccess;
using BMC.Common.Utilities;
using BMC.Common.ExceptionManagement;
using System.Data.SqlClient;
using System.Globalization;
using System.Threading;
using Audit.Transport;
using Audit.BusinessClasses;

namespace BMC.EnterpriseBusiness.Business
{
    public class AutoCalendar
    {
        private static AutoCalendar _AutoCalendarBiz;
        public static AutoCalendar CreateInstance()
        {
            if (_AutoCalendarBiz == null)
                _AutoCalendarBiz = new AutoCalendar();

            return _AutoCalendarBiz;
        }

        public List<GetAutoCalendarProfiles> GetAutoCalendarProfiles()
        {
            List<GetAutoCalendarProfiles> objCalendarProfiles = null;
            try
            {
                List<rsp_AC_GetAutoCalendarProfilesResult> objProfiles;
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    objProfiles = DataContext.GetAutoCalendarProfiles().ToList();
                }
                objCalendarProfiles = (from obj in objProfiles
                                       select new GetAutoCalendarProfiles
                                       {
                                           AutoCalendarProfile_ID = obj.AutoCalendarProfile_ID,
                                           AutoCalendarProfile_Name = obj.AutoCalendarProfile_Name
                                       }).ToList<GetAutoCalendarProfiles>();

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return objCalendarProfiles;
        }

        public AutoCalendarResult GetAutoCalendarProfilesDetails(int AutoCalendarProfileID)
        {
            AutoCalendarResult objAutoCalendarEntity = new AutoCalendarResult();
            try
            {
                EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext();
                var result = DataContext.GetAutoCalendarProfilesDetails(AutoCalendarProfileID);
                var lstProfilesDetails = result.GetResult<rsp_AC_GetAutoCalendarProfilesDetailsResult>();
                var lstSCDetails = result.GetResult<rsp_AC_GetAutoCalendarSubCompanyDetailsResult>();
                List<GetAutoCalendarProfilesDetails> lstProfilesDetailsEntity = (from records in lstProfilesDetails
                                                                                 select new GetAutoCalendarProfilesDetails
                                                                                 {
                                                                                     AutoCalendarProfile_ID = records.AutoCalendarProfile_ID,
                                                                                     AutoCalendarProfile_Name = records.AutoCalendarProfile_Name,
                                                                                     IsAutoCalendarEnabled = records.IsAutoCalendarEnabled,
                                                                                     CalendarCreateBeforeDays = records.CalendarCreateBeforeDays,
                                                                                     CalendarAlertBefore = records.CalendarAlertBefore,
                                                                                     CalendarAlertRecurrence = records.CalendarAlertRecurrence,
                                                                                     IsCalendarBasedOnDays = records.IsCalendarBasedOnDays,
                                                                                     NewCalendarDayID = records.NewCalendarDayID,
                                                                                     SetNewCalendarActive = records.SetNewCalendarActive
                                                                                 }).ToList<GetAutoCalendarProfilesDetails>();
                List<GetAutoCalendarSubCompanyDetails> lstSCDetailsEntity = (from records in lstSCDetails
                                                                             select new GetAutoCalendarSubCompanyDetails
                                                                             {
                                                                                 Sub_Company_ID = records.Sub_Company_ID,
                                                                                 Sub_Company_Name = records.Sub_Company_Name,
                                                                                 Profilestatus = records.Profilestatus
                                                                             }).ToList<GetAutoCalendarSubCompanyDetails>();
                objAutoCalendarEntity.ProfilesDetails = lstProfilesDetailsEntity;
                objAutoCalendarEntity.SCDetails = lstSCDetailsEntity;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return objAutoCalendarEntity;
        }

        public int VerifyAutoCalendarProfiles(string AutoCalendarProfile_Name, int AutoCalendarProfile_Id)
        {
            using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
            {
                return DataContext.VerifyAutoCalendarProfiles(AutoCalendarProfile_Name, AutoCalendarProfile_Id);
            }
        }

        public int UpdateAutoCalendarProfiles(int AutoCalendarProfile_ID, string AutoCalendarProfile_Name, bool IsAutoCalendarEnabled, int CalendarCreateBeforeDays, int CalendarAlertBefore, int CalendarAlertRecurrence, bool IsCalendarBasedOnDays, int NewCalendarDayID, bool setNewCalendarActive, string AssignProfiles, string OperationType)
        {
            using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
            {
                return DataContext.UpdateAutoCalendarProfiles(AutoCalendarProfile_ID, AutoCalendarProfile_Name, IsAutoCalendarEnabled, CalendarCreateBeforeDays, CalendarAlertBefore, CalendarAlertRecurrence, IsCalendarBasedOnDays, NewCalendarDayID, setNewCalendarActive, AssignProfiles, OperationType);
            }
        }

        #region OperatorAuditing

        public void InsertNewAuditEntry(string strScreenName, string strField, string strNewItem, int iUserId, string strUsername)
        {
            try
            {
                //Calling Audit Method
                Audit_History AH = new Audit_History();
                //Populate required Values            
                AH.EnterpriseModuleName = ModuleNameEnterprise.AutoCalendar;
                AH.Audit_Screen_Name = strScreenName;
                AH.Audit_Desc = "ProfileName [" + strNewItem + "] added to " + strScreenName;
                AH.AuditOperationType = OperationType.ADD;
                AH.Audit_Field = strField;
                AH.Audit_New_Vl = strNewItem;
                AH.Audit_Slot = string.Empty;
                AH.Audit_Old_Vl = string.Empty;

                AH.Audit_User_ID = iUserId;
                AH.Audit_User_Name = strUsername;

                AuditViewerBusiness AVB = new AuditViewerBusiness(Common.Utilities.DatabaseHelper.GetConnectionString());
                AVB.InsertAuditData(AH, true);

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        public void AuditModifiedData(string CalendarProfileName, string sField, string sPrevValue, string sNewValue, int iUserId, string strUsername)
        {
            try
            {
                Audit_History AH = new Audit_History();
                AH.EnterpriseModuleName = ModuleNameEnterprise.AutoCalendar;
                AH.Audit_Screen_Name = "AutoCalendar";
                AH.Audit_Desc = "AutoCalendar " + CalendarProfileName + " modified  ..[" + sField + "] : " + sPrevValue + " -> " + sNewValue;
                AH.AuditOperationType = OperationType.MODIFY;
                AH.Audit_Field = sField;
                AH.Audit_User_ID = iUserId;
                AH.Audit_User_Name = strUsername;
                AH.Audit_New_Vl = sNewValue; //current value
                AH.Audit_Old_Vl = sPrevValue;  // previous value
                AH.Audit_Slot = string.Empty;

                AuditViewerBusiness AVB = new AuditViewerBusiness(Common.Utilities.DatabaseHelper.GetConnectionString());
                AVB.InsertAuditData(AH, true);
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        public void InsertDeteleAuditEntry(string strScreenName, string strField, string strNewItem, int iUserId, string strUsername)
        {
            try
            {
                //Calling Audit Method
                Audit_History AH = new Audit_History();
                //Populate required Values            
                AH.EnterpriseModuleName = ModuleNameEnterprise.AutoCalendar;
                AH.Audit_Screen_Name = strScreenName;
                AH.Audit_Desc = "AutoCalendar [" + strField + "] Deleted ";
                AH.AuditOperationType = OperationType.DELETE;
                AH.Audit_Field = strField;
                AH.Audit_New_Vl = strNewItem;
                AH.Audit_Slot = string.Empty;
                AH.Audit_Old_Vl = string.Empty;

                AH.Audit_User_ID = iUserId;
                AH.Audit_User_Name = strUsername;

                AuditViewerBusiness AVB = new AuditViewerBusiness(Common.Utilities.DatabaseHelper.GetConnectionString());
                AVB.InsertAuditData(AH, true);

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        #endregion
    }
}
