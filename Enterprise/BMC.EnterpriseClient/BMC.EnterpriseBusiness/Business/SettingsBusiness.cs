using System;
using System.Collections.Generic;
using System.Linq;
using BMC.Common.ExceptionManagement;
using BMC.EnterpriseBusiness.Entities;
using BMC.EnterpriseDataAccess;

namespace BMC.EnterpriseBusiness.Business
{
    public class SettingsBusiness
    {
        #region Data Member

        private static SettingsBusiness _SettingsBusiness;

        private static object lockobj = new object();
        #endregion //Data Member

        #region Constructor

        public SettingsBusiness()
        {
        }

        #endregion //Constructor

        #region Methods

        #region Static Methods

        public static SettingsBusiness CreateInstance()
        {
            if (_SettingsBusiness == null)
            {
                lock (lockobj)
                {
                    if (_SettingsBusiness == null)
                        _SettingsBusiness = new SettingsBusiness();
                }
            }
            return _SettingsBusiness;
        }

        #endregion //Static Methods

        /// <summary>
        /// To load all Enterprise settings on launch the application
        /// </summary>
        public void GetInitialSettings()
        {
            try
            {
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    List<rsp_GetInitialSettingsResult> lstSettings = DataContext.GetInitialSettings().ToList();
                    if (lstSettings == null || lstSettings.Count <= 0 || lstSettings[0] == null) return;

                    SettingsEntity.BGSAdminWSUserID = lstSettings[0].BGSAdminWSUserID;
                    SettingsEntity.BGSAdminWSPwd = lstSettings[0].BGSAdminWSPwd;
                    SettingsEntity.SGVI_Enabled = Convert.ToBoolean(lstSettings[0].SGVI_Enabled);
                    SettingsEntity.SGVI_Payment_Days = Convert.ToInt32(lstSettings[0].SGVI_Payment_Days);
                    SettingsEntity.SGVI_Statement_Number = Convert.ToInt32(lstSettings[0].SGVI_Statement_Number);
                    SettingsEntity.ReportServerURL = lstSettings[0].ReportServerURL;
                    SettingsEntity.ReportFolder = lstSettings[0].ReportFolder;
                    SettingsEntity.EmptyReportMessage = lstSettings[0].EmptyReportMessage;
                    SettingsEntity.AUTHORIZATION_KEY_EXPIRY_HOURS = Convert.ToInt32(lstSettings[0].AUTHORIZATION_KEY_EXPIRY_HOURS);
                    SettingsEntity.SenderCode = lstSettings[0].SenderCode;
                    SettingsEntity.IsAuditingEnabled = Convert.ToBoolean(lstSettings[0].IsAuditingEnabled);
                    SettingsEntity.Client = lstSettings[0].Client;
                    SettingsEntity.BMC_Reports_Header = lstSettings[0].BMC_Reports_Header;
                    SettingsEntity.BMC_Reports_Language = lstSettings[0].BMC_Reports_Language;
                    SettingsEntity.MaxHandPayAuthRequired = Convert.ToBoolean(lstSettings[0].MaxHandPayAuthRequired);
                    SettingsEntity.ManualEntryTicketValidation = Convert.ToBoolean(lstSettings[0].ManualEntryTicketValidation);
                    SettingsEntity.SlotLifeToDate = Convert.ToBoolean(lstSettings[0].SlotLifeToDate);
                    SettingsEntity.AllowPartNumberEdit = Convert.ToBoolean(lstSettings[0].AllowPartNumberEdit);
                    SettingsEntity.RedeemTicketCustomer_Min = Convert.ToInt32(lstSettings[0].RedeemTicketCustomer_Min);
                    SettingsEntity.RedeemTicketCustomer_Max = Convert.ToInt32(lstSettings[0].RedeemTicketCustomer_Max);
                    SettingsEntity.RedeemTicketCustomer_BankAcctNo = lstSettings[0].RedeemTicketCustomer_BankAcctNo;
                    SettingsEntity.WindowsServices = lstSettings[0].WindowsServices;
                    SettingsEntity.IsAFTEnabledForSite = Convert.ToBoolean(lstSettings[0].IsAFTEnabledForSite);
                    SettingsEntity.IsPowerPromoReportsRequired = Convert.ToBoolean(lstSettings[0].IsPowerPromoReportsRequired);
                    SettingsEntity.MachineMaintenance = Convert.ToBoolean(lstSettings[0].MachineMaintenance);
                    SettingsEntity.CertificateIssuer = lstSettings[0].CertificateIssuer;
                    SettingsEntity.IsCertificateRequired = Convert.ToBoolean(lstSettings[0].IsCertificateRequired);
                    SettingsEntity.ComponentVerification = Convert.ToBoolean(lstSettings[0].ComponentVerification);
                    SettingsEntity.GuardianServerIPAddress = lstSettings[0].GuardianServerIPAddress;
                    SettingsEntity.IsMeterAdjustmentToolRequired = Convert.ToBoolean(lstSettings[0].IsMeterAdjustmentToolRequired);
                    SettingsEntity.LiveMeter = Convert.ToBoolean(lstSettings[0].LiveMeter);
                    SettingsEntity.ClearEventsOnFinalDrop = lstSettings[0].ClearEventsOnFinalDrop;
                    SettingsEntity.Auto_Declare_Monies = Convert.ToBoolean(lstSettings[0].Auto_Declare_Monies);
                    SettingsEntity.IsAFTIncludedInCalculation = Convert.ToBoolean(lstSettings[0].IsAFTIncludedInCalculation);
                    SettingsEntity.TreasuryLimitForMajorPrizes = Convert.ToInt32(lstSettings[0].TreasuryLimitForMajorPrizes);
                    SettingsEntity.SHOWHANDPAYCODE = Convert.ToBoolean(lstSettings[0].SHOWHANDPAYCODE);
                    SettingsEntity.CheckForGamePartNumber = Convert.ToBoolean(lstSettings[0].CheckForGamePartNumber);
                    SettingsEntity.CentralizedDeclaration = Convert.ToBoolean(lstSettings[0].CentralizedDeclaration);
                    SettingsEntity.IsTransmitterEnabled = Convert.ToBoolean(lstSettings[0].IsTransmitterEnabled);
                    SettingsEntity.STMServerIP = lstSettings[0].STMServerIP;
                    SettingsEntity.StackerLevelAlert = Convert.ToBoolean(lstSettings[0].StackerLevelAlert);
                    SettingsEntity.DropScheduleAlert = Convert.ToBoolean(lstSettings[0].DropScheduleAlert);
                    SettingsEntity.AllowOfflineDeclaration = Convert.ToBoolean(lstSettings[0].AllowOfflineDeclaration);
                    SettingsEntity.DeclarationAlert = Convert.ToBoolean(lstSettings[0].DeclarationAlert);
                    SettingsEntity.MinuteThreadCheckinHoursforAutoDrop = Convert.ToInt32(lstSettings[0].MinuteThreadCheckinHoursforAutoDrop);
                    SettingsEntity.RetryMinutesForCheckDB = Convert.ToInt32(lstSettings[0].RetryMinutesForCheckDB);
                    SettingsEntity.StackerFeature = Convert.ToBoolean(lstSettings[0].StackerFeature);
                    SettingsEntity.IsEmployeeCardTrackingEnabled = Convert.ToBoolean(lstSettings[0].IsEmployeeCardTrackingEnabled);
                    SettingsEntity.AddShortpayInVoucherOut = Convert.ToBoolean(lstSettings[0].AddShortpayInVoucherOut);
                    SettingsEntity.IsSiteLicensingEnabled = Convert.ToBoolean(lstSettings[0].IsSiteLicensingEnabled);
                    SettingsEntity.LiquidationProfitShare = Convert.ToBoolean(lstSettings[0].LiquidationProfitShare);
                    SettingsEntity.UseAssetTemplate = Convert.ToBoolean(lstSettings[0].UseAssetTemplate);
                    SettingsEntity.CentralizedReadLiquidation = Convert.ToBoolean(lstSettings[0].CentralizedReadLiquidation);
                    SettingsEntity.AGSSerialNumberAlphaNumeric = Convert.ToBoolean(lstSettings[0].AGSSerialNumberAlphaNumeric);
                    SettingsEntity.AllowDeMerge = Convert.ToBoolean(lstSettings[0].AllowDeMerge);
                    SettingsEntity.IsEnrolmentFlag = Convert.ToBoolean(lstSettings[0].IsEnrolmentFlag);
                    SettingsEntity.Login_Expiry_No_of_Days = Convert.ToInt32(lstSettings[0].Login_Expiry_No_of_Days);
                    SettingsEntity.Login_Max_No_Of_Attempts = Convert.ToInt32(lstSettings[0].Login_Max_No_Of_Attempts);
                    SettingsEntity.PRODUCTVERSION = lstSettings[0].PRODUCTVERSION;
                    SettingsEntity.STMServerPort = Convert.ToInt32(lstSettings[0].STMServerPort);
                    SettingsEntity.IsEnrolmentComplete = Convert.ToBoolean(lstSettings[0].IsEnrolmentComplete);
                    SettingsEntity.AGSValue = Convert.ToInt32(lstSettings[0].AGSValue);
                    SettingsEntity.IsSingleCardEmployee = Convert.ToBoolean(lstSettings[0].IsSingleCardEmployee);
                    SettingsEntity.MaxNoOfCardsForEmployee = Convert.ToInt32(lstSettings[0].MaxNoOfCardsForEmployee);
                    SettingsEntity.MaxNoOfVaultCassettes = Convert.ToInt32(lstSettings[0].MaxNoOfVaultCassettes);
                    SettingsEntity.MaxNoOfVaultHoppers = Convert.ToInt32(lstSettings[0].MaxNoOfVaultHoppers);
                    SettingsEntity.IsVaultEnabled = Convert.ToBoolean(lstSettings[0].IsVaultEnabled);
                    SettingsEntity.IsBillCounterAmountEditable = Convert.ToBoolean(lstSettings[0].IsBillCounterAmountEditable);
                    SettingsEntity.Vault_AutoPopulateDropValues = Convert.ToBoolean(lstSettings[0].Vault_AutoPopulateDropValues);
                    SettingsEntity.Vault_EndDeviceOnTerminate = Convert.ToBoolean(lstSettings[0].Vault_EndDeviceOnTerminate);
                    SettingsEntity.IsCrossTicketingEnabled = Convert.ToBoolean(lstSettings[0].IsCrossTicketingEnabled);
                    SettingsEntity.AllowEnableDisableBarPosition = Convert.ToBoolean(lstSettings[0].AllowEnableDisableBarPosition);
                    SettingsEntity.CustomerName = string.IsNullOrEmpty(lstSettings[0].CustomerName) ? "" : lstSettings[0].CustomerName;
                    SettingsEntity.IsAlertEnabled = Convert.ToBoolean(lstSettings[0].IsAlertEnabled);
                    SettingsEntity.IsEmailAlertEnabled = Convert.ToBoolean(lstSettings[0].IsEmailAlertEnabled);
                    SettingsEntity.IsAutoCalendarEnabled = Convert.ToBoolean(lstSettings[0].IsAutoCalendarEnabled);
                    SettingsEntity.ReportDateFormat = lstSettings[0].ReportDateFormat;
                    SettingsEntity.ReportDateTimeFormat = lstSettings[0].ReportDateTimeFormat;
                    SettingsEntity.ReportDataDateAloneFormat = lstSettings[0].ReportDataDateAloneFormat;
                    SettingsEntity.ReportDataDateNTimeFormat = lstSettings[0].ReportDataDateNTimeFormat;
                    SettingsEntity.ReportPrintDateTimeFormat = lstSettings[0].ReportPrintDateTimeFormat;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        #endregion //Methods
    }
}

