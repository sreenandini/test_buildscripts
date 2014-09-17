using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stacker;
using BMC.EnterpriseDataAccess;
using BMC.EnterpriseBusiness.Entities;
using BMC.Common.LogManagement;
using BMC.Common.ExceptionManagement;

namespace BMC.EnterpriseBusiness.Business
{
    public class AGSBusiness
    {
        public int InsertOrUpdateAGSSetting(string SettingValue)
        {
            int res = -1;
            try
            {
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    res = DataContext.usp_InsertOrUpdateAGSSetting(SettingValue);
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("InsertOrUpdateAGSSetting", LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
            }
            return res;
        }

        public int InsertOrUpdateSetting(string SettingName, string SettingValue)
        {
            int res1 = -1;
            try
            {
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    res1 = DataContext.usp_InsertOrUpdateSetting(SettingName, ref SettingValue);
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("InsertOrUpdateSetting", LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
            }
            return res1;
        }
        public int CheckEnrolmentTypeBiz(string Serial, string Asset, string GMU, int? Result, int? Machine_ID)
        {
            int ResultVal = -1;
            try
            {
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    ResultVal = DataContext.rsp_CheckEnrolmentType(Serial, Asset, GMU, ref Result, Machine_ID);
                   // ResultVal = Result;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("CheckEnrolmentTypeBiz", LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
            }
            return ResultVal;
        }

    }
}
