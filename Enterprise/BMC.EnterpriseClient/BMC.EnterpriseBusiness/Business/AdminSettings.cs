using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.EnterpriseBusiness.Entities;
using BMC.EnterpriseDataAccess;
using BMC.Common.ExceptionManagement;
using System.Reflection;
using Audit.Transport;



namespace BMC.EnterpriseBusiness.Business
{
    public class AdminSettings
    {
        EnterpriseDataContext dbContext = null;

        public AdminSettings()
        {
            dbContext = EnterpriseDataContextHelper.GetDataContext();
        }

        public AdminSystemSettingsResult GetSystemSettingDetails()
        {
            AdminSystemSettingsResult objAdminSystemSettingsResult = null;
            try
            {
                objAdminSystemSettingsResult = new AdminSystemSettingsResult();
                dbContext = EnterpriseDataContextHelper.GetDataContext();
                var result = dbContext.GetSystemSettings();
                var objSystemSettingsResult = result.GetResult<SystemSettingsResult>();
                var lstMachineTypeInfoResult = result.GetResult<MachineTypeInfoResult>();
                var lstCultureInfoResult = result.GetResult<CultureInfoResult>();

                objAdminSystemSettingsResult.SystemSettings = (from records in objSystemSettingsResult
                                                               select new SystemSettingsEntity
                                                    {
                                                        BarPosition = records.BarPosition,
                                                        Company = records.Company,
                                                        Site = records.Site,
                                                        SubCompany = records.SubCompany,
                                                        Zone = records.Zone,
                                                        AutoGenerateModelCode = records.AutoGenerateModelCode,
                                                        ModelCodePrefix = records.ModelCodePrefix,
                                                        ModelCodeMinLength = records.ModelCodeMinLength,
                                                        AutoGenerateStockCode = records.AutoGenerateStockCode,
                                                        StockCodePrefix = records.StockCodePrefix,
                                                        StockCodeMinLength = records.StockCodeMinLength,
                                                        AllowStockBulkPurchase = records.AllowStockBulkPurchase,
                                                        ForceSiteRepsOnStock = records.ForceSiteRepsOnStock,
                                                        ServiceHandheld = records.ServiceHandheld,
                                                        ImportExportAssetFile = records.ImportExport_AssetFile,
                                                        ServerName = records.ServerName,
                                                        Machine_ID = records.Machine_ID,
                                                        Machine_Class_ID = records.Machine_Class_ID,
                                                        Machine_Type_ID = records.Machine_Type_ID,
                                                        RegionCulture = records.RegionCulture,
                                                        IsSiteLicensingEnabled = records.IsSiteLicensingEnabled,
                                                        IsPowerPromoReportsRequired = records.IsPowerPromoReportsRequired,
                                                        CentralizedDeclaration = records.CentralizedDeclaration,
                                                        IsEmployeecardTrackingEnabled = records.IsEmployeecardTrackingEnabled,
                                                        AllowOfflineDeclaration = records.AllowOfflineDeclaration,
                                                        AddShortpayInVoucherOut = records.AddShortpayInVoucherOut,
                                                        SystemSettingsDisplayTabVisible = records.SystemSettingsDisplayTabVisible,
                                                        SystemSettingsServiceTabVisible = records.SystemSettingsServiceTabVisible,
                                                        IsEnrolmentComplete = records.IsEnrolmentComplete,
                                                        IsEnrolmentFlag = records.IsEnrolmentFlag,
                                                        ValidateAGSForGMU = records.ValidateAGSForGMU,
                                                        IsSitesPartiallyConfiguredEnabled = records.VIEWPARTIALLYCONFIGUREDSITES,
                                                        IsGameCappingEnabled = records.IsGameCappingEnabled,
                                                        IsSuppressZoneEnabled = records.IsSuppressZoneEnabled,
                                                        IsSingleCardEmployee = records.IsSingleCardEmployee,
                                                        AllowEnableDisableBarPosition = records.AllowEnableDisableBarPosition,
                                                        IsAlertEnabled = records.IsAlertEnabled,
                                                        IsEmailAlertEnabled = records.IsEmailAlertEnabled,
                                                        IsAutoCalendarEnabled = records.IsAutoCalendarEnabled,
                                                        SendMailFromEnterprise = records.SendMailFromEnterprise,
                                                        CancelPendingMails=records.CancelPendingMails,
                                                        AllowMutltipleDrops = records.AllowMultipleDrops
                                                    }).FirstOrDefault();



                objAdminSystemSettingsResult.MachineTypeInfoEntities = (from records in lstMachineTypeInfoResult
                                                                        select new MachineTypeInfoEntity
                                                                        {
                                                                            Machine_Type_ID = records.Machine_Type_ID,
                                                                            Machine_Type_Code = records.Machine_Type_Code
                                                                        }).ToList<MachineTypeInfoEntity>();

                objAdminSystemSettingsResult.CultureInfoEntities = (from records in lstCultureInfoResult
                                                                    select new CultureInfoEntity
                                                                    {
                                                                        CultureInfo = records.CultureInfo

                                                                    }).ToList<CultureInfoEntity>();

            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
                objAdminSystemSettingsResult = new AdminSystemSettingsResult();

            }
            finally
            {
                dbContext.Dispose();
            }
            return objAdminSystemSettingsResult;
        }

        public List<MachineClassInfoEntity> GetMachineClassInfo(int typeID)
        {
            List<MachineClassInfoEntity> obj = null;
            try
            {
                dbContext = EnterpriseDataContextHelper.GetDataContext();
                var result = dbContext.GetMachineClassNames(typeID);
                obj = (from records in result
                       select new MachineClassInfoEntity
                                       {
                                           Machine_Class_ID = records.Machine_Class_ID,
                                           Machine_Name = records.Machine_Name

                                       }).ToList<MachineClassInfoEntity>();
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
            finally
            {
                dbContext.Dispose();
            }
            return obj;
        }

        public List<MachineInfoEntity> GetMachineInfo(int machineClassID)
        {
            List<MachineInfoEntity> obj = null;
            try
            {
                dbContext = EnterpriseDataContextHelper.GetDataContext();
                var result = dbContext.GetMachineInfo(machineClassID);
                obj = (from records in result
                       select new MachineInfoEntity
                       {
                           Machine_ID = records.Machine_ID,
                           Machine_Stock_No = records.Machine_Stock_No

                       }).ToList<MachineInfoEntity>();
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
            finally
            {
                dbContext.Dispose();
            }
            return obj;
        }
        public bool UpdateSystemSettings(SystemSettingsEntity modifiedEntity, SystemSettingsEntity originalEntity, int userID, string userName)
        {
            try
            {
                dbContext = EnterpriseDataContextHelper.GetDataContext();
                dbContext.UpdateSystemSettings(modifiedEntity.BarPosition, modifiedEntity.Company, modifiedEntity.SubCompany,
                    modifiedEntity.Site, modifiedEntity.Zone, modifiedEntity.AutoGenerateModelCode, modifiedEntity.ModelCodePrefix, modifiedEntity.ModelCodeMinLength,
                    modifiedEntity.AutoGenerateStockCode, modifiedEntity.StockCodePrefix, modifiedEntity.StockCodeMinLength,
                    modifiedEntity.AllowStockBulkPurchase, modifiedEntity.ForceSiteRepsOnStock, modifiedEntity.ServiceHandheld,
                    modifiedEntity.ServerName, modifiedEntity.RegionCulture,
                    (Convert.ToBoolean(modifiedEntity.IsSiteLicensingEnabled)) ? "true" : "false",
                    (Convert.ToBoolean(modifiedEntity.ImportExportAssetFile)) ? "true" : "false",
                    (Convert.ToBoolean(modifiedEntity.IsEnrolmentFlag)) ? "true" : "false",
                    (Convert.ToBoolean(modifiedEntity.IsEnrolmentComplete)) ? "true" : "false",
                    (Convert.ToBoolean(modifiedEntity.IsPowerPromoReportsRequired)) ? "true" : "false",
                    (Convert.ToBoolean(modifiedEntity.CentralizedDeclaration)) ? "true" : "false",
                    (Convert.ToBoolean(modifiedEntity.IsEmployeecardTrackingEnabled)) ? "true" : "false",
                    (Convert.ToBoolean(modifiedEntity.AllowOfflineDeclaration)) ? "true" : "false",
                    (Convert.ToBoolean(modifiedEntity.AddShortpayInVoucherOut)) ? "true" : "false",
                    (Convert.ToBoolean(modifiedEntity.SystemSettingsDisplayTabVisible)) ? "true" : "false",
                    (Convert.ToBoolean(modifiedEntity.SystemSettingsServiceTabVisible)) ? "true" : "false",
                    (Convert.ToBoolean(modifiedEntity.ValidateAGSForGMU)) ? "true" : "false",
                    (Convert.ToBoolean(modifiedEntity.IsSitesPartiallyConfiguredEnabled)) ? "true" : "false",
                    (Convert.ToBoolean(modifiedEntity.IsGameCappingEnabled)) ? "true" : "false",
                    (Convert.ToBoolean(modifiedEntity.IsSuppressZoneEnabled)) ? "true" : "false",
                    (Convert.ToBoolean(modifiedEntity.IsSingleCardEmployee)) ? "true" : "false",
                    (Convert.ToBoolean(modifiedEntity.AllowEnableDisableBarPosition)) ? "true" : "false",
                    (Convert.ToBoolean(modifiedEntity.IsAlertEnabled)) ? "true" : "false",
                    (Convert.ToBoolean(modifiedEntity.IsEmailAlertEnabled)) ? "true" : "false",
                    (Convert.ToBoolean(modifiedEntity.IsAutoCalendarEnabled)) ? "true" : "false",
                    (Convert.ToBoolean(modifiedEntity.SendMailFromEnterprise)) ? "true" : "false",
                    (Convert.ToBoolean(modifiedEntity.CancelPendingMails)) ? "true" : "false",
                    (Convert.ToBoolean(modifiedEntity.AllowMutltipleDrops)) ? "true" : "false");

                return AuditData(modifiedEntity, originalEntity, userID, userName);

            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
                return false;
            }
            finally
            {
                dbContext.Dispose();
            }
        }

        private bool AuditData(SystemSettingsEntity modifiedEntity, SystemSettingsEntity originalEntity, int userID, string userName)
        {
            try
            {

                dbContext = EnterpriseDataContextHelper.GetDataContext();

                foreach (PropertyInfo prop in modifiedEntity.GetType().GetProperties())
                {
                    if (prop.GetValue(modifiedEntity,null) != null && !(prop.GetValue(modifiedEntity, null)).Equals(prop.GetValue(originalEntity, null)))
                        dbContext.InsertAuditData(userID, userName, (int)ModuleNameEnterprise.Settings, "System Settings", "System Settings", "", prop.Name, Convert.ToString(prop.GetValue(originalEntity, null)), Convert.ToString(prop.GetValue(modifiedEntity, null)), "System Settings modified    ..[" + prop.Name + "]: '" + prop.GetValue(originalEntity, null) + "' --> '" + prop.GetValue(modifiedEntity, null) + "'", "MODIFY");

                }
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
                return false;
            }
            finally
            {
                dbContext.Dispose();
            }
            return true;
        }

        public bool IsMultiCardAssociatedToUser()
        {
            bool _bStatus = false;

            try
            {
                dbContext = EnterpriseDataContextHelper.GetDataContext();
                var result = dbContext.IsMultiCardAssociatedToUser();
                _bStatus = Convert.ToBoolean((from records in result
                                              select new IsMultiCardAssociatedToUser
                                              {
                                                  MultiCardAssociatedToUser = records.IsMultiCardAssociatedToUser,
                                              }).FirstOrDefault().MultiCardAssociatedToUser);
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
            finally
            {
                dbContext.Dispose();
            }
            return _bStatus;
        }

    }
}
