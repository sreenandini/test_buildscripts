using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BMC.Common.ExceptionManagement;
using BMC.Common.LogManagement;
using BMC.DBInterface.CashDeskOperator;
using BMC.Business.CashDeskOperator.WebServices;
using BMC.Common.ConfigurationManagement;
using Microsoft.Win32;
using System.Data.SqlClient;
using BMC.Common.Utilities;

namespace BMC.Business.CashDeskOperator
{
    public class SiteSetupBusiness
    {
        #region "Private Variables"
        private SiteSetupDataAccess SiteSetupDataAccess = new SiteSetupDataAccess();
        FactoryResetBusiness oFactoryResetBusiness = new FactoryResetBusiness();
        private Proxy oProxy = null;
        private Proxy oWebService = null;
        private int Xdays = 0;
        private CommonUtilities oCommonUtilities = null;
        private string sVerifyURL = string.Empty;
        #endregion

        public enum ImportStatus
        {
            Failed = -1,
            UpdateFailed = -2,
            Completed = 1,
            EnterpriseNoData = 2
        }

        #region Constructor
        public SiteSetupBusiness()
        {
            ConfigManager.SetConfigurationMode(ConfigManager.ConfigurationMode.AppConfig);
            Xdays = int.Parse(ConfigManager.Read("XDays"));
        }
        #endregion

        #region "Private Functions"

        private string CurrentInstallationDetails()
        {
            return SiteSetupDataAccess.CurrentInstallationDetails(); 
        }

        private int ImportTicketExceptions(int iSiteCode)
        {
            DataTable dtTicketExceptions = new DataTable();
            int iResult = Convert.ToInt32(ImportStatus.Failed);

            try
            {
                dtTicketExceptions = GetTicketExceptionFromServer(iSiteCode);
                LogManager.WriteLog("Import ticket exception details from server count:  " + dtTicketExceptions.Rows.Count.ToString(), LogManager.enumLogLevel.Info);

                if (dtTicketExceptions.Rows.Count > 0)
                {
                    if (SiteSetupDataAccess.ImportTicketExceptionDetails(dtTicketExceptions))
                    {
                        iResult = Convert.ToInt32(ImportStatus.Completed);
                    }
                    else
                    {
                        iResult = Convert.ToInt32(ImportStatus.UpdateFailed);
                    }
                }
                else
                {
                    iResult = Convert.ToInt32(ImportStatus.EnterpriseNoData);
                }
                LogManager.WriteLog("Import voucher details :  " + iResult.ToString(), LogManager.enumLogLevel.Info);
            }
            catch (Exception exImportMachines)
            {
                ExceptionManager.Publish(exImportMachines);
                iResult = Convert.ToInt32(ImportStatus.Failed);
            }

            return iResult;
        }

        private int ImportDeviceDetails(int iSiteCode)
        {
            DataTable dtDeviceDetails = new DataTable();
            int iResult = Convert.ToInt32(ImportStatus.Failed);

            try
            {
                dtDeviceDetails = GetDeviceDetailsFromServer(iSiteCode);
                LogManager.WriteLog("Import device details from server count:  " + dtDeviceDetails.Rows.Count.ToString(), LogManager.enumLogLevel.Info);

                if (dtDeviceDetails.Rows.Count > 0)
                {
                    if (SiteSetupDataAccess.ImportDeviceDetails(dtDeviceDetails))
                        iResult = Convert.ToInt32(ImportStatus.Completed);
                    else
                        iResult = Convert.ToInt32(ImportStatus.UpdateFailed);
                }
                else
                    iResult = Convert.ToInt32(ImportStatus.EnterpriseNoData);

                LogManager.WriteLog("Import device details :  " + iResult.ToString(), LogManager.enumLogLevel.Info);
            }
            catch (Exception exImportMachines)
            {
                ExceptionManager.Publish(exImportMachines);
                iResult = Convert.ToInt32(ImportStatus.Failed);
            }

            return iResult;
        }

        private int ImporWorkstationDetailsDetails(int iSiteCode)
        {
            DataTable dtWorkStationDetails = new DataTable();
            int iResult = Convert.ToInt32(ImportStatus.Failed);

            try
            {
                dtWorkStationDetails = GetWorkstationDetailsFromServer(iSiteCode);
                LogManager.WriteLog("Import work station details from server count:  " + dtWorkStationDetails.Rows.Count.ToString(), LogManager.enumLogLevel.Info);
                if (dtWorkStationDetails.Rows.Count > 0)
                    if (SiteSetupDataAccess.ImportWorkStationDetails(dtWorkStationDetails))
                        iResult = Convert.ToInt32(ImportStatus.Completed);
                    else
                        iResult = Convert.ToInt32(ImportStatus.UpdateFailed);
                else
                    iResult = Convert.ToInt32(ImportStatus.EnterpriseNoData);

                LogManager.WriteLog("Import work station details :  " + iResult.ToString(), LogManager.enumLogLevel.Info);
            }
            catch (Exception exImportMachines)
            {
                ExceptionManager.Publish(exImportMachines);
                iResult = Convert.ToInt32(ImportStatus.Failed);
            }

            return iResult;
        }

        private int ImportVoucherDetails(int iSiteCode)
        {
            //DataTable dtImportTickets = new DataTable();
            int iResult = Convert.ToInt32(ImportStatus.Failed);
            string strResult = string.Empty;
            try
            {
                //dtImportTickets = GetVoucherFromServer(iSiteCode);
                strResult = GetVoucherFromServer(iSiteCode);
                //LogManager.WriteLog("Import voucher details from server count:  " + dtImportTickets.Rows.Count.ToString(), LogManager.enumLogLevel.Info);
                LogManager.WriteLog("Import voucher details from server count:  " + strResult.Length.ToString(), LogManager.enumLogLevel.Info);

                //if (dtImportTickets.Rows.Count > 0)
                if (strResult.Length > 0)
                {
                    //if (SiteSetupDataAccess.ImportVoucherDetails(dtImportTickets))
                    if (SiteSetupDataAccess.ImportVoucherDetails(strResult))
                    {
                        iResult = Convert.ToInt32(ImportStatus.Completed);
                    }
                    else
                    {
                        iResult = Convert.ToInt32(ImportStatus.UpdateFailed);
                    }
                }
                else
                {
                    iResult = Convert.ToInt32(ImportStatus.EnterpriseNoData);
                }
                LogManager.WriteLog("Import voucher details :  " + iResult.ToString(), LogManager.enumLogLevel.Info);
            }
            catch (Exception exImportMachines)
            {
                ExceptionManager.Publish(exImportMachines);
                iResult = Convert.ToInt32(ImportStatus.Failed);
            }

            return iResult;
        }

        private int ImportMachineDetails(int iSiteCode)
        {
            string strResult;
            int iResult = Convert.ToInt32(ImportStatus.Failed);

            try
            {
                strResult = GetMachinesFromServer(iSiteCode);
                LogManager.WriteLog("Import Machines from server:  " + strResult.Length.ToString(), LogManager.enumLogLevel.Info);

                if (string.IsNullOrEmpty(strResult))
                {
                    iResult = Convert.ToInt32(ImportStatus.EnterpriseNoData);
                }
                else
                {
                    if (SiteSetupDataAccess.ImportMachines(strResult))
                    {
                        iResult = Convert.ToInt32(ImportStatus.Completed);
                    }
                    else
                    {
                        iResult = Convert.ToInt32(ImportStatus.UpdateFailed);
                    }
                }
            }
            catch (Exception exImportMachines)
            {
                ExceptionManager.Publish(exImportMachines);
                iResult = Convert.ToInt32(ImportStatus.Failed);
            }

            return iResult;
        }

        private int ImportOtherMachineDetails(int iSiteCode)
        {
            string strResult;
            int iResult = Convert.ToInt32(ImportStatus.Failed);

            try
            {
                strResult = GetOtherMachineDetailsFromServer(iSiteCode);
                LogManager.WriteLog("Import Other Machines Details from server:  " + strResult.Length.ToString(), LogManager.enumLogLevel.Info);

                if (string.IsNullOrEmpty(strResult))
                {
                    iResult = Convert.ToInt32(ImportStatus.EnterpriseNoData);
                }
                else
                {
                    if (SiteSetupDataAccess.ImportOtherMachineDetails(strResult))
                    {
                        iResult = Convert.ToInt32(ImportStatus.Completed);
                    }
                    else
                    {
                        iResult = Convert.ToInt32(ImportStatus.UpdateFailed);
                    }
                }
            }
            catch (Exception exImportMachines)
            {
                ExceptionManager.Publish(exImportMachines);
                iResult = Convert.ToInt32(ImportStatus.Failed);
            }

            return iResult;
        }

        private int ImportUserRolesLinks(int iSiteCode)
        {
            string strResult;
            int iResult = Convert.ToInt32(ImportStatus.Failed);

            try
            {
                strResult = GetUserRolesLinks(iSiteCode);
                LogManager.WriteLog("Import user Roles Links from server:  " + strResult, LogManager.enumLogLevel.Info);

                if (string.IsNullOrEmpty(strResult))
                {
                    iResult = Convert.ToInt32(ImportStatus.EnterpriseNoData);
                }
                else
                {
                    if (SiteSetupDataAccess.ImportUserRolesLinks(strResult))
                    {
                        iResult = Convert.ToInt32(ImportStatus.Completed);
                    }
                    else
                    {
                        iResult = Convert.ToInt32(ImportStatus.UpdateFailed);
                    }
                }
            }
            catch (Exception exImportUserRolesLinks)
            {
                iResult = Convert.ToInt32(ImportStatus.Failed);
                ExceptionManager.Publish(exImportUserRolesLinks);
            }

            return iResult;
        }

        private int ImportObjects(int iSiteCode)
        {
            string strResult = "";
            int iResult = Convert.ToInt32(ImportStatus.Failed);

            try
            {
                strResult = GetAllObjectsFromServer(iSiteCode);
                LogManager.WriteLog("Import objects from server:  " + strResult, LogManager.enumLogLevel.Info);

                if (string.IsNullOrEmpty(strResult))
                    iResult = Convert.ToInt32(ImportStatus.EnterpriseNoData);
                else
                {
                    if (SiteSetupDataAccess.ImportObjects(strResult))
                        iResult = Convert.ToInt32(ImportStatus.Completed);

                    else
                        iResult = Convert.ToInt32(ImportStatus.UpdateFailed);
                }
            }
            catch (Exception exImportobjects)
            {
                iResult = Convert.ToInt32(ImportStatus.Failed);
                ExceptionManager.Publish(exImportobjects);
            }

            return iResult;
        }

        private int ImportRoleAccessObjectRightLnk(int iSiteCode)
        {
            string strResult = "";
            int iResult = Convert.ToInt32(ImportStatus.Failed);

            try
            {
                strResult = GetAllRoleAccessObjectRightLnkFromServer(iSiteCode);
                LogManager.WriteLog("Import RoleAccessObjectRightLnk from server:  " + strResult, LogManager.enumLogLevel.Info);

                if (string.IsNullOrEmpty(strResult))
                    iResult = Convert.ToInt32(ImportStatus.EnterpriseNoData);
                else
                {
                    if (SiteSetupDataAccess.ImportRoleAccessObjectRightLnk(strResult))
                        iResult = Convert.ToInt32(ImportStatus.Completed);

                    else
                        iResult = Convert.ToInt32(ImportStatus.UpdateFailed);
                }
            }
            catch (Exception ex)
            {
                iResult = Convert.ToInt32(ImportStatus.Failed);
                ExceptionManager.Publish(ex);
            }

            return iResult;
        }

        private DataTable GetLatestMeterHistoryFromServer(string strInstallationXML, int iSiteCode)
        {
            if (oWebService == null)
            {
                oWebService = new Proxy(iSiteCode.ToString());
            }
            return oWebService.GetLatestMeterHistory(strInstallationXML);
        }

        private string GetZonesFromServer(int iSiteCode)
        {
            if (oWebService == null)
            {
                oWebService = new Proxy(iSiteCode.ToString());
            }
            return oWebService.GetZonesData(iSiteCode);
        }

        private string GetSiteAllianceFromServer(int iSiteCode)
        {
            if (oWebService == null)
            {
                oWebService = new Proxy(iSiteCode.ToString());
            }
            return oWebService.GetSiteAllianceData(iSiteCode);
        }

        private string GetBarPositionsFromServer(int iSiteCode)
        {
            if (oWebService == null)
            {
                oWebService = new Proxy(iSiteCode.ToString());
            }
            return oWebService.GetBarPositionsData(iSiteCode);
        }

        private string GetMachinesFromServer(int iSiteCode)
        {
            if (oWebService == null)
            {
                oWebService = new Proxy(iSiteCode.ToString());
            }
            return oWebService.GetMachineData(iSiteCode);
        }

        private string GetInstallationFromServer(int iSiteCode)
        {
            if (oWebService == null)
            {
                oWebService = new Proxy(iSiteCode.ToString());
            }
            return oWebService.GetInstallationData(iSiteCode);
        }

        private string GetSiteDetailsFromServer(int iSiteCode)
        {
            if (oWebService == null)
            {
                oWebService = new Proxy(iSiteCode.ToString());
            }
            return oWebService.GetSiteDetails(iSiteCode.ToString());
        }

        private DataTable GetLatestBatchIdFromServer(int iSiteCode)
        {
            if (oWebService == null)
            {
                oWebService = new Proxy(iSiteCode.ToString());
            }

            return oWebService.GetLatestSiteBatchID(iSiteCode, Xdays);
        }

        private int CheckAuthorizationCodeFromServer(string sAuthCode, int iSiteCode, string TransactionType)
        {
            string sReturnValue = "";
            //string sReturnExcValue = "";
            //string sReturnEntValue = "";
            int iResult = -1;
            //string ExcKeyLocation="";
            //string EntKeyLocation = "";
            //string path = "";
            //int indexToCheckKeys = 0;
            try
            {

                if (oProxy == null)
                    oProxy = new Proxy(sVerifyURL, true);
                sReturnValue = oProxy.CheckTransactionKey(iSiteCode.ToString(), sAuthCode,TransactionType);
                if (!string.IsNullOrEmpty(sReturnValue))
                {
                    iResult = Convert.ToInt32(sReturnValue.Substring(0, 1));
                    //try
                    //{
                    //    //indexToCheckKeys = sReturnValue.IndexOf(',', 3);
                    //if (indexToCheckKeys != 0)
                    //{
                    //    sReturnExcValue = sReturnValue.Substring(2, indexToCheckKeys - 2);
                    //    sReturnEntValue = sReturnValue.Substring(indexToCheckKeys + 1, (sReturnValue.Length - (indexToCheckKeys + 1)));
                    //    Dictionary<string, string> SettoRegistry = new Dictionary<string, string>();
                    //    ConfigManager.SetConfigurationMode(ConfigManager.ConfigurationMode.AppConfig);
                    //    path = ConfigManager.Read("Registrypath");
                    //    if (!string.IsNullOrEmpty(path))
                    //    {
                    //        ExcKeyLocation = ConfigManager.Read("ExchangeKey");
                    //        EntKeyLocation = ConfigManager.Read("EnterpriseKey");
                    //        if (!string.IsNullOrEmpty(ExcKeyLocation) && (!string.IsNullOrEmpty(EntKeyLocation)))
                    //        {
                    //            SettoRegistry.Add(ExcKeyLocation, sReturnExcValue);
                    //            SettoRegistry.Add(EntKeyLocation, sReturnEntValue);
                    //            oCommonUtilities = (oCommonUtilities == null) ? new CommonUtilities() : oCommonUtilities;
                    //            oCommonUtilities.SetRegistryEntries(SettoRegistry, path);

                    //        }
                    //    }
                    //}
                    //}
                    //catch (Exception ex)
                    //{
                    //    LogManager.WriteLog("No Keys returned from Server:" + iResult.ToString(), LogManager.enumLogLevel.Warning);
                    //    indexToCheckKeys = 0;

                    //}
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                iResult = -1;
            }
            return iResult;
        }

        private string GetCollectionsFromServer(int Batch_ID, int iSiteCode)
        {
            if (oWebService == null)
            {
                oWebService = new Proxy(iSiteCode.ToString());
            }
            return oWebService.GetCollections(Batch_ID);
        }

        private string GetTreasuryDetails(int Batch_ID, int iSiteCode)
        {
            if (oWebService == null)
            {
                oWebService = new Proxy(iSiteCode.ToString());
            }
            return oWebService.GetTreasuryDetails(Batch_ID);
        }

        private string GetEventsDetails(int Batch_ID, int iSiteCode)
        {
            if (oWebService == null)
            {
                oWebService = new Proxy(iSiteCode.ToString());
            }
            return oWebService.GetEventsDetails(Batch_ID);
        }
        
        //private DataTable GetVoucherFromServer(int iSiteCode)
        //{
        //    if (oWebService == null)
        //    {
        //        oWebService = new Proxy(iSiteCode.ToString());
        //    }
        //    return oWebService.GetSiteTickets(iSiteCode, Xdays);
        //}

        private string GetVoucherFromServer(int iSiteCode)
        {
            if (oWebService == null)
            {
                oWebService = new Proxy(iSiteCode.ToString());
            }
            return oWebService.GetSiteTickets(iSiteCode, Xdays);
        }

        private DataTable GetDeviceDetailsFromServer(int iSiteCode)
        {
            if (oWebService == null)
            {
                oWebService = new Proxy(iSiteCode.ToString());
            }
            return oWebService.GetSiteDeviceDetails(iSiteCode);
        }

        private DataTable GetWorkstationDetailsFromServer(int iSiteCode)
        {
            if (oWebService == null)
            {
                oWebService = new Proxy(iSiteCode.ToString());
            }
            return oWebService.GetSiteWorkstationDetails(iSiteCode);
        }

        private DataTable GetTicketExceptionFromServer(int iSiteCode)
        {
            if (oWebService == null)
            {
                oWebService = new Proxy(iSiteCode.ToString());
            }
            return oWebService.GetSiteTicketExceptions(iSiteCode, Xdays);
        }

        private string GetSystemSettingsFromServer(int iSiteCode)
        {
            if (oWebService == null)
            {
                oWebService = new Proxy(iSiteCode.ToString());
            }
            return oWebService.GetSystemSettings(iSiteCode.ToString());
        }

        private string GetLookupFromServer(int iSiteCode)
        {
            if (oWebService == null)
            {
                oWebService = new Proxy(iSiteCode.ToString());
            }
            return oWebService.GetLookupMasterSettings();
        }

        private DataTable GetDailyReadFromServer(int iSiteCode)
        {
            if (oWebService == null)
            {
                oWebService = new Proxy(iSiteCode.ToString());
            }
            return oWebService.GetDailyReads(iSiteCode.ToString(), Xdays);
        }

        //private DataTable GetHourlyFromServer(int iSiteCode)
        //{
        //    if (oWebService == null)
        //    {
        //        oWebService = new Proxy(iSiteCode.ToString());
        //    }
        //    return oWebService.GetHourly(iSiteCode.ToString(), Xdays);
        //}

        private string GetHourlyFromServer(int iSiteCode)
        {
            if (oWebService == null)
            {
                oWebService = new Proxy(iSiteCode.ToString());
            }
            return oWebService.GetHourly(iSiteCode.ToString(), Xdays);
        }

        private string GetAFTTransactionsFromServer(int iSiteCode)
        {
            if (oWebService == null)
            {
                oWebService = new Proxy(iSiteCode.ToString());
            }
            return oWebService.GetAFTTransactions(iSiteCode.ToString());
        }

        private string GetAuditHistoryFromServer(int iSiteCode)
        {
            if (oWebService == null)
            {
                oWebService = new Proxy(iSiteCode.ToString());
            }
            return oWebService.GetSiteAuditHistoryDetails(iSiteCode.ToString());
        }

        private string GetAllEventsFromServer(int iSiteCode)
        {
            if (oWebService == null)
            {
                oWebService = new Proxy(iSiteCode.ToString());
            }
            return oWebService.GetAllEvents(iSiteCode.ToString(), Xdays);
        }

        private string GetCashDeskTransactionsFromServer(int iSiteCode, string strXML)
        {
            if (oWebService == null)
            {
                oWebService = new Proxy(iSiteCode.ToString());
            }
            return oWebService.GetCashDeskTransactions(strXML);
        }

        private string GetUserDetails(int iSiteCode)
        {
            if (oWebService == null)
            {
                oWebService = new Proxy(iSiteCode.ToString());
            }
            return oWebService.GetAllUserDetails(iSiteCode.ToString());
        }

        private string GetUserRoles(int iSiteCode)
        {
            if (oWebService == null)
            {
                oWebService = new Proxy(iSiteCode.ToString());
            }
            return oWebService.GetAllUserRoles(iSiteCode.ToString());
        }

        private string GetUserRolesLinks(int iSiteCode)
        {
            if (oWebService == null)
            {
                oWebService = new Proxy(iSiteCode.ToString());
            }
            return oWebService.GetAllUserRolesLinks(iSiteCode.ToString());
        }

        private string GetCalendars(int iSiteCode)
        {
            if (oWebService == null)
            {
                oWebService = new Proxy(iSiteCode.ToString());
            }
            return oWebService.GetCalendars(iSiteCode.ToString());
        }

        private string GetAAMSDetails(int iSiteCode)
        {
            if (oWebService == null)
            {
                oWebService = new Proxy(iSiteCode.ToString());
            }
            return oWebService.GetAAMSDetails(iSiteCode);
        }

        private string GetComponentDetails(int iSiteCode)
        {
            if (oWebService == null)
            {
                oWebService = new Proxy(iSiteCode.ToString());
            }
            return oWebService.GetComponentDetails(iSiteCode.ToString());
        }

        private string GetInstallationGameInfo(int iSiteCode)
        {
            if (oWebService == null)
            {
                oWebService = new Proxy(iSiteCode.ToString());
            }
            return oWebService.GetInstallationGameInfo(iSiteCode);
        }
        
        private string GetSeedValues(int iSiteCode, string Tables)
        {
            if (oWebService == null)
            {
                oWebService = new Proxy(iSiteCode.ToString());
            }
            return oWebService.GetSeedValues(iSiteCode.ToString(), Tables);
            //return "";
        }

        private string GetOtherGameDetails(int iSiteCode)
        {
            if (oWebService == null)
            {
                oWebService = new Proxy(iSiteCode.ToString());
            }
            return oWebService.GetOtherGameDetails(iSiteCode.ToString());
        }

        private string GetGameLibrary(int iSiteCode)
        {
            if (oWebService == null)
            {
                oWebService = new Proxy(iSiteCode.ToString());
            }
            return oWebService.GetGameLibrary(iSiteCode.ToString());
        }

        private string GetPayTable(int iSiteCode)
        {
            if (oWebService == null)
            {
                oWebService = new Proxy(iSiteCode.ToString());
            }
            return oWebService.GetPayTable(iSiteCode.ToString());
        }

        private string GetInstallationGamePayTableInfo(int iSiteCode) 
        {
            if (oWebService == null) {
                oWebService = new Proxy(iSiteCode.ToString());
            }
            return oWebService.GetInstallationGamePayTableInfo(iSiteCode.ToString());
        }

        private bool UpdateSiteStatusFromServer(int iSiteCode, string sUpdate)
        {
            if (oWebService == null)
            {
                oWebService = new Proxy(iSiteCode.ToString());
            }
            return oWebService.SetSiteStatusEnterprise(iSiteCode.ToString(), sUpdate);
        }

        private string GetOtherMachineDetailsFromServer(int iSiteCode)
        {
            if (oWebService == null)
            {
                oWebService = new Proxy(iSiteCode.ToString());
            }
            return oWebService.GetOtherMachineDetailsForRecovery(iSiteCode.ToString());
        }

        private bool ResetEnterpriseTransactionKey(string iAuthCode, int iSiteCode)
        {
            if (oWebService == null)
            {
                oWebService = new Proxy(iSiteCode.ToString());
            }
            return oWebService.ResetTransactionKey(iSiteCode.ToString(), iAuthCode);
        }

        private string CheckNGAinEnterprise(string strMacList, int iSiteCode)
        {
            if (oWebService == null)
            {
                oWebService = new Proxy(iSiteCode.ToString());
            }
            return oWebService.CheckNGA(iSiteCode.ToString(), strMacList);
        }

        private string GetAllObjectsFromServer(int iSiteCode)
        {
            if (oWebService == null)
            {
                oWebService = new Proxy(iSiteCode.ToString());
            }
            return oWebService.GetAllObjects();
        }

        private string GetAllRoleAccessObjectRightLnkFromServer(int iSiteCode)
        {
            if (oWebService == null)
            {
                oWebService = new Proxy(iSiteCode.ToString());
            }
            return oWebService.GetAllRoleAccessObjectRightLnk();
        }

        #endregion

        #region "Public Function"

        public string GetServiceStatus(string strserviceName)
        {
            return oFactoryResetBusiness.GetServiceStatus(strserviceName);
        }

        public bool EndService(string strserviceName)
        {
            return oFactoryResetBusiness.EndService(strserviceName);
        }

        public bool StartService(string strserviceName)
        {
            return oFactoryResetBusiness.StartService(strserviceName);
        }

        public string GetSettingValue(string settingname)
        {
            string servicelist = ((CommonDataAccess.GetSettingValue(settingname)) != null || CommonDataAccess.GetSettingValue(settingname) != string.Empty) ? CommonDataAccess.GetSettingValue("ServiceNames") : string.Empty;
            return servicelist;
        }

        public bool DatabaseIsEmpty()
        {
            return SiteSetupDataAccess.DatabaseIsEmpty();
        }

        public bool IsValidSiteCode(int siteCode)
        {
            return SiteSetupDataAccess.IsValidSiteCode(siteCode);
        }

        public int EnterpriseUrlIsExists(string sServername, string sitecode)
        {
            int iResult = 0;
            ///string sVerifyURL = string.Empty;
            string sWebExtension = string.Empty;
            try
            {
                ConfigManager.SetConfigurationMode(ConfigManager.ConfigurationMode.AppConfig);

                //RegistryKey key = BMCRegistryHelper.GetRegLocalMachine().OpenSubKey(ConfigManager.Read("RegistryPath"));
                //ConfigManager.RegistryString = key.ToString();
                //sVerifyURL = key.GetValue("BGSWebService").ToString();
                sVerifyURL = BMCRegistryHelper.GetRegKeyValue(ConfigManager.Read("RegistryPath"), "BGSWebService");
                /*
                //If registry cleared then manually creating the web url path
                if (sVerifyURL.Length <= 0)
                {
                    sWebExtension = ConfigManager.Read("WebserviceExtension");
                    sVerifyURL = "http://" + sServername + sWebExtension;
                }
                //else if registry has value
                if (!sServername.Contains("http"))
                {
                    sWebExtension = ConfigManager.Read("WebserviceExtension");
                    sServername = "http://" + sServername + sWebExtension;                       
                }
                 * */
                //if (sVerifyURL.ToLower().Equals(sServername.ToLower().Trim()))
                //{
                if (sVerifyURL.Length > 0)
                {
                    oProxy = new Proxy(sVerifyURL, true);
                    if (oProxy.InitiateWebService() > 0)
                        iResult = 1;
                    else
                        iResult = -1;
                }
                else
                {
                    iResult = -2;
                }

                LogManager.WriteLog("Proxy Call Result:  " + iResult.ToString(), LogManager.enumLogLevel.Info);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                iResult = -1;
            }

            return iResult;
        }

        public int ImportSiteDetails(int iSiteCode)
        {
            string strResult;
            int iResult = Convert.ToInt32(ImportStatus.Failed);

            try
            {                
                strResult = GetSiteDetailsFromServer(iSiteCode);
                LogManager.WriteLog("Import SiteDetails from server:  String Length Count - " + strResult.Length.ToString(), LogManager.enumLogLevel.Info);

                if (string.IsNullOrEmpty(strResult))
                {
                    iResult = Convert.ToInt32(ImportStatus.EnterpriseNoData);
                }
                else
                {
                    if (SiteSetupDataAccess.ImportSiteDetails(strResult))
                    {
                        iResult = Convert.ToInt32(ImportStatus.Completed);
                    }
                    else
                    {
                        iResult = Convert.ToInt32(ImportStatus.UpdateFailed);
                    }
                }
            }
            catch (Exception exImportSiteDetails)
            {
                ExceptionManager.Publish(exImportSiteDetails);
                iResult = Convert.ToInt32(ImportStatus.Failed);
            }

            return iResult;
        }

        public int ImportSiteAlliance(int iSiteCode)
        {
            LogManager.WriteLog("Inside ImportSiteAlliance START" , LogManager.enumLogLevel.Info);

            string strResult;
            int iResult = Convert.ToInt32(ImportStatus.Failed);

            try
            {
                LogManager.WriteLog("Before GetSiteAllianceFromServer", LogManager.enumLogLevel.Info);

                strResult = GetSiteAllianceFromServer(iSiteCode);
                LogManager.WriteLog("Import SiteAlliance from server:  " + strResult.Length.ToString(), LogManager.enumLogLevel.Info);

                LogManager.WriteLog("strResult:" + strResult, LogManager.enumLogLevel.Info);

                if (string.IsNullOrEmpty(strResult))
                {
                    iResult = Convert.ToInt32(ImportStatus.EnterpriseNoData);
                }
                else
                {
                    if (SiteSetupDataAccess.ImportSiteAlliance(strResult))
                    {
                        iResult = Convert.ToInt32(ImportStatus.Completed);
                    }
                    else
                    {
                        iResult = Convert.ToInt32(ImportStatus.UpdateFailed);
                    }
                }

                LogManager.WriteLog("iResult:" + iResult.ToString(), LogManager.enumLogLevel.Info);

            }
            catch (Exception exImportSiteAlliance)
            {
                ExceptionManager.Publish(exImportSiteAlliance);
                iResult = Convert.ToInt32(ImportStatus.Failed);
            }

            return iResult;
        }

        public int ImportZones(int iSiteCode)
        {
            string strResult;
            int iResult = Convert.ToInt32(ImportStatus.Failed);

            try
            {
                strResult = GetZonesFromServer(iSiteCode);
                LogManager.WriteLog("Import Zones from server:  " + strResult.Length.ToString(), LogManager.enumLogLevel.Info);

                if (string.IsNullOrEmpty(strResult))
                {
                    iResult = Convert.ToInt32(ImportStatus.EnterpriseNoData);
                }
                else
                {
                    if (SiteSetupDataAccess.ImportZones(strResult))
                    {
                        iResult = Convert.ToInt32(ImportStatus.Completed);
                    }
                    else
                    {
                        iResult = Convert.ToInt32(ImportStatus.UpdateFailed);
                    }
                }
            }
            catch (Exception exImportZones)
            {
                ExceptionManager.Publish(exImportZones);
                iResult = Convert.ToInt32(ImportStatus.Failed);
            }

            return iResult;
        }

        public int ImportInstallations(int iSiteCode)
        {
            string strResult;
            int iResult = Convert.ToInt32(ImportStatus.Failed);

            try
            {
                strResult = GetInstallationFromServer(iSiteCode);
                LogManager.WriteLog("Import Installations from server:  " + strResult, LogManager.enumLogLevel.Info);

                if (string.IsNullOrEmpty(strResult))
                {
                    iResult = Convert.ToInt32(ImportStatus.EnterpriseNoData);
                }
                else
                {
                    if (SiteSetupDataAccess.ImportInstallations(strResult))
                    {
                        iResult = Convert.ToInt32(ImportStatus.Completed);
                    }
                    else
                    {
                        iResult = Convert.ToInt32(ImportStatus.UpdateFailed);
                    }
                }
            }
            catch (Exception exImportInstallations)
            {
                ExceptionManager.Publish(exImportInstallations);
                iResult = Convert.ToInt32(ImportStatus.Failed);
            }

            return iResult;
        }

        public bool ImportMachines(int iSiteCode)
        {
            bool bReturn = false;

            try
            {
                if (ImportOtherMachineDetails(iSiteCode) > 0 && ImportMachineDetails(iSiteCode) > 0)
                {
                    bReturn = true;
                }
            }
            catch (Exception ex)
            {
                bReturn = false;
                ExceptionManager.Publish(ex);
            }

            return bReturn;
        }

        public int ImportBarPositions(int iSiteCode)
        {
            string strResult;
            int iResult = Convert.ToInt32(ImportStatus.Failed);

            try
            {
                strResult = GetBarPositionsFromServer(iSiteCode);
                LogManager.WriteLog("Import BarPositions from server:  " + strResult.Length.ToString(), LogManager.enumLogLevel.Info);

                if (string.IsNullOrEmpty(strResult))
                {
                    iResult = Convert.ToInt32(ImportStatus.EnterpriseNoData);
                }
                else
                {
                    if (SiteSetupDataAccess.ImportBarPositions(strResult))
                    {
                        iResult = Convert.ToInt32(ImportStatus.Completed);
                    }
                    else
                    {
                        iResult = Convert.ToInt32(ImportStatus.UpdateFailed);
                    }
                }
            }
            catch (Exception exImportBarPositions)
            {
                ExceptionManager.Publish(exImportBarPositions);
                iResult = Convert.ToInt32(ImportStatus.Failed);
            }

            return iResult;
        }

        public int ImportLatestMeterHistory(int iSiteCode)
        {
            string strXML = string.Empty;
            DataTable dtLatesMeterHistory = new DataTable();
            int iResult = Convert.ToInt32(ImportStatus.Failed);

            try
            {
                strXML = CurrentInstallationDetails();

                if (string.IsNullOrEmpty(strXML))
                {
                    iResult = Convert.ToInt32(ImportStatus.EnterpriseNoData);
                    LogManager.WriteLog("Import latest meter history from server:  No Installation", LogManager.enumLogLevel.Info);
                }
                else
                {
                    dtLatesMeterHistory = GetLatestMeterHistoryFromServer(strXML, iSiteCode);
                    LogManager.WriteLog("Import latest meter history from server:  " + dtLatesMeterHistory.Rows.Count.ToString(), LogManager.enumLogLevel.Info);

                    if (dtLatesMeterHistory.Rows.Count > 0)
                    {
                        if (SiteSetupDataAccess.ImportLatestMeterHistory(dtLatesMeterHistory))
                        {
                            iResult = Convert.ToInt32(ImportStatus.Completed);
                        }
                        else
                        {
                            iResult = Convert.ToInt32(ImportStatus.UpdateFailed);
                        }
                    }
                    else
                    {
                        iResult = Convert.ToInt32(ImportStatus.EnterpriseNoData);
                    }
                }

            }
            catch (Exception ex)
            {
                iResult = Convert.ToInt32(ImportStatus.Failed);
                ExceptionManager.Publish(ex);
            }

            return iResult;
        }

        public bool ReseedCollectionBatch(int iSiteCode)
        {
            //int iBatchId = 0;
            //LogManager.WriteLog("Import CollectionBatch from server before import:  " + iBatchId.ToString(), LogManager.enumLogLevel.Info);
            ////iBatchId = GetLatestBatchIdFromServer(iSiteCode);
            //LogManager.WriteLog("Import CollectionBatch from server:  " + iBatchId.ToString(), LogManager.enumLogLevel.Info);
            //if (iBatchId > 0)
            //{ return SiteSetupDataAccess.ReseedCollectionBatch(iBatchId); }
            //else { ; }

            return true;
        }

        public bool ImportCollectionBatch(int BatchID, int iSiteCode)
        {
            bool bStatus = false;

            try
            {
                string ReturnXML = GetCollectionsFromServer(BatchID, iSiteCode);
                //LogManager.WriteLog("Import CollectionBatch from server:  " + dtCollectionBatch.Rows.Count.ToString(), LogManager.enumLogLevel.Info);

                if (String.IsNullOrEmpty(ReturnXML))
                {
                    bStatus = false;
                }
                else
                {
                    if (SiteSetupDataAccess.ImportCollectionBatch(ReturnXML))
                    {
                        bStatus = true;
                    }
                    else
                    {
                        bStatus = false;
                    }
                }
            }
            catch (Exception exImportCollectionBatch)
            {
                bStatus = false;
                ExceptionManager.Publish(exImportCollectionBatch);
            }

            return bStatus;
        }

        public bool ImportAFTTransactions(int iSiteCode)
        {
            bool bStatus = false;

            try
            {
                string ReturnXML = GetAFTTransactionsFromServer(iSiteCode);
                
                if (String.IsNullOrEmpty(ReturnXML))
                {
                    bStatus = false;
                }
                else
                {
                    if (SiteSetupDataAccess.ImportAFTTransactions(ReturnXML))
                    {
                        bStatus = true;
                    }
                    else
                    {
                        bStatus = false;
                    }
                }
            }
            catch (Exception exImportAFTTransactions)
            {
                bStatus = false;
                ExceptionManager.Publish(exImportAFTTransactions);
            }

            return bStatus;
        }

        public bool ImportAuditHistory(int iSiteCode)
        {
            bool bStatus = false;

            try
            {
                string ReturnXML = GetAuditHistoryFromServer(iSiteCode);

                if (String.IsNullOrEmpty(ReturnXML))
                {
                    bStatus = false;
                }
                else
                {
                    if (SiteSetupDataAccess.ImportAuditHistory(ReturnXML))
                    {
                        bStatus = true;
                    }
                    else
                    {
                        bStatus = false;
                    }
                }
            }
            catch (Exception exImportAuditHistory)
            {
                bStatus = false;
                ExceptionManager.Publish(exImportAuditHistory);
            }

            return bStatus;
        }
        //
        public bool ImportTreasuryDetails(int nBatchId, int iSiteCode)
        {
            bool bStatus = false;

            try
            {
                string ReturnXML = GetTreasuryDetails(nBatchId, iSiteCode); 

                if (String.IsNullOrEmpty(ReturnXML))
                {
                    bStatus = false;
                }
                else
                {
                    if (SiteSetupDataAccess.ImportTreasuryDetails(ReturnXML))
                    {
                        bStatus = true;
                    }
                    else
                    {
                        bStatus = false;
                    }
                }
            }
            catch (Exception exImportTreasuryDetails)
            {
                bStatus = false;
                ExceptionManager.Publish(exImportTreasuryDetails);
            }

            return bStatus;
        }
        //
        public bool ImportEventsDetails(int nBatchId, int iSiteCode)
        {
            bool bStatus = false;

            try
            {
                string ReturnXML = GetEventsDetails(nBatchId, iSiteCode); 

                if (String.IsNullOrEmpty(ReturnXML))
                {
                    bStatus = false;
                }
                else
                {
                    if (SiteSetupDataAccess.ImportEventsDetails(ReturnXML))
                    {
                        bStatus = true;
                    }
                    else
                    {
                        bStatus = false;
                    }
                }
            }
            catch (Exception exImportEventsDetails)
            {
                bStatus = false;
                ExceptionManager.Publish(exImportEventsDetails);
            }

            return bStatus;
        }
        //
        public int CheckAuthorizationCode(string iAuthCode, int iSiteCode, string TransactionType)
        {
            int iValue = 0;
            iValue = CheckAuthorizationCodeFromServer(iAuthCode, iSiteCode,TransactionType);
            LogManager.WriteLog("CheckAuthorizationCode from server:  " + iValue.ToString(), LogManager.enumLogLevel.Info);
            return iValue;
        }

        public int UpdateSiteStatus(int iSiteCode, string sUpdate)
        {
            bool bServerUpdateStatus = false;
            bool bSiteUpdateStatus = false;

            bServerUpdateStatus = UpdateSiteStatusFromServer(iSiteCode, sUpdate);
            LogManager.WriteLog("Update site status in server:  " + bServerUpdateStatus.ToString(), LogManager.enumLogLevel.Info);
            bSiteUpdateStatus = SiteSetupDataAccess.UpdateSiteStatus(iSiteCode, sUpdate);
            if (bServerUpdateStatus == true && bSiteUpdateStatus == true)
                return 3;
            else if (bServerUpdateStatus == true && bSiteUpdateStatus == false)
                return 2;
            else if (bServerUpdateStatus == false && bSiteUpdateStatus == true)
                return 1;
            else
                return 0;
        }

        public bool UpdateCheckPoints(int iSiteCode, int Value, string sTableName)
        {
            return SiteSetupDataAccess.UpdateCheckPoints(iSiteCode, Value, sTableName);
        }

        public bool UpdateAllCheckPoints(int Value)
        {
            return SiteSetupDataAccess.UpdateAllCheckPoints(Value);
        }

        public bool FlattenSystem()
        {
            return SiteSetupDataAccess.FlattenSystem();
        }

        public bool ResetTransactionKey(string iAuthCode, int iSiteCode)
        {
            return ResetEnterpriseTransactionKey(iAuthCode, iSiteCode);
        }

        public string CheckNGA(string strMACList, int iSiteCode)
        {
            return CheckNGAinEnterprise(strMACList, iSiteCode);
        }

        public DataTable GetTableDetails() { return SiteSetupDataAccess.GetTableDetails(); }

        public Dictionary<int, string> GetCheckPointsStatus(byte iStatus) { return SiteSetupDataAccess.GetCheckPointsStatus(iStatus); }

        public int ImportCollections(int iSiteCode)
        {
            int iResult = Convert.ToInt32(ImportStatus.Failed);
            DataTable dtBatchid = null;

            try
            {
                //Import Audit History
                ImportAuditHistory(iSiteCode);

                dtBatchid = GetLatestBatchIdFromServer(iSiteCode);

                if (dtBatchid.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtBatchid.Rows)
                    {
                        ImportCollectionBatch(Int32.Parse(dr[0].ToString()), iSiteCode);
                        //
                        ImportEventsDetails(Int32.Parse(dr[0].ToString()), iSiteCode);
                        //
                        ImportTreasuryDetails(Int32.Parse(dr[0].ToString()), iSiteCode);
                    }
                    ImportAFTTransactions(iSiteCode);                    

                    iResult = Convert.ToInt32(ImportStatus.Completed);
                }
                else
                {
                    iResult = Convert.ToInt32(ImportStatus.EnterpriseNoData);
                }
            }
            catch (Exception exImportCollections)
            {
                ExceptionManager.Publish(exImportCollections);
                iResult = Convert.ToInt32(ImportStatus.Failed);
            }

            return iResult;
        }

        public bool ImportTickets(int iSiteCode)
        {
            bool bReturn = false;

            try
            {
                if (ImportVoucherDetails(iSiteCode) > 0 && ImportDeviceDetails(iSiteCode) > 0
                    && ImportTicketExceptions(iSiteCode) > 0 && ImporWorkstationDetailsDetails(iSiteCode) > 0)
                {
                    bReturn = true;
                }

            }
            catch (Exception ex)
            {
                bReturn = false;
                ExceptionManager.Publish(ex);
            }

            return bReturn;
        }

        public int ImportSystemSettings(int iSiteCode)
        {
            string strResult;
            int iResult = Convert.ToInt32(ImportStatus.Failed);

            try
            {
                strResult = GetSystemSettingsFromServer(iSiteCode);
                LogManager.WriteLog("Import SystemSettings from server:  " + strResult.Length.ToString(), LogManager.enumLogLevel.Debug);

                if (string.IsNullOrEmpty(strResult))
                {
                    iResult = Convert.ToInt32(ImportStatus.EnterpriseNoData);
                }
                else
                {
                    if (SiteSetupDataAccess.ImportSystemSettings(strResult))
                    {
                        iResult = Convert.ToInt32(ImportStatus.Completed);
                    }
                    else
                    {
                        iResult = Convert.ToInt32(ImportStatus.UpdateFailed);
                    }
                }
            }
            catch (Exception exImportSystemSettings)
            {
                ExceptionManager.Publish(exImportSystemSettings);
                iResult = Convert.ToInt32(ImportStatus.Failed);
            }

            if (iResult == -1 || iResult == -2)
                LogManager.WriteLog("Import SystemSettings Failed from server:  ", LogManager.enumLogLevel.Info);
            else
            {
                try
                {
                    strResult = GetLookupFromServer(iSiteCode);
                    LogManager.WriteLog("Import Category And Reason Settings from server", LogManager.enumLogLevel.Debug);

                    if (string.IsNullOrEmpty(strResult))
                    {
                        iResult = Convert.ToInt32(ImportStatus.EnterpriseNoData);
                    }
                    else
                    {
                        if (SiteSetupDataAccess.ImportLookupMaster(strResult))
                        {
                            iResult = Convert.ToInt32(ImportStatus.Completed);
                        }
                        else
                        {
                            iResult = Convert.ToInt32(ImportStatus.UpdateFailed);
                        }
                    }
                }
                catch (Exception exImportSystemSettings)
                {
                    ExceptionManager.Publish(exImportSystemSettings);
                    iResult = Convert.ToInt32(ImportStatus.Failed);
                }
                if (iResult == -1 || iResult == -2)
                    LogManager.WriteLog("Import Category And Reason Settings Failed from server:  ", LogManager.enumLogLevel.Info);

            }

            return iResult;
        }

        public int ImportDailyRead(int iSiteCode)
        {
            DataTable dtImportDaily = new DataTable();
            int iResult = Convert.ToInt32(ImportStatus.Failed);

            try
            {
                dtImportDaily = GetDailyReadFromServer(iSiteCode);
                LogManager.WriteLog("Import daily details from server:  " + dtImportDaily.Rows.Count.ToString(), LogManager.enumLogLevel.Info);

                if (dtImportDaily.Rows.Count > 0)
                {
                    if (SiteSetupDataAccess.ImportDaily(dtImportDaily))
                    {
                        iResult = Convert.ToInt32(ImportStatus.Completed);
                    }
                    else
                    {
                        iResult = Convert.ToInt32(ImportStatus.UpdateFailed);
                    }
                }
                else
                {
                    iResult = Convert.ToInt32(ImportStatus.EnterpriseNoData);
                }
            }
            catch (Exception exImportDailyRead)
            {
                iResult = Convert.ToInt32(ImportStatus.Failed);
                ExceptionManager.Publish(exImportDailyRead);
            }
            finally
            {
                if (dtImportDaily != null) { dtImportDaily.Dispose(); }
            }
            return iResult;
        }

        public int ImportHourly(int iSiteCode)
        {
            //DataTable dtImportHourly = new DataTable();
            string strResult = string.Empty;
            int iResult = Convert.ToInt32(ImportStatus.Failed);

            try
            {
                //dtImportHourly = GetHourlyFromServer(iSiteCode);
                strResult = GetHourlyFromServer(iSiteCode);
                //LogManager.WriteLog("Import hourly details from server:  " + dtImportHourly.Rows.Count.ToString(), LogManager.enumLogLevel.Info);
                LogManager.WriteLog("Import hourly details from server:  " + strResult.Length.ToString(), LogManager.enumLogLevel.Info);

                //if (dtImportHourly.Rows.Count > 0)
                if (strResult.Length > 0)
                {
                    // if (SiteSetupDataAccess.ImportHourly(dtImportHourly))
                    if (SiteSetupDataAccess.ImportHourly(strResult))
                    {
                        iResult = Convert.ToInt32(ImportStatus.Completed);
                    }
                    else
                    {
                        iResult = Convert.ToInt32(ImportStatus.UpdateFailed);
                    }
                }
                else
                {
                    iResult = Convert.ToInt32(ImportStatus.EnterpriseNoData);
                }
            }
            catch (Exception exImportHourly)
            {
                iResult = Convert.ToInt32(ImportStatus.Failed);
                ExceptionManager.Publish(exImportHourly);
            }
            //finally
            //{
            //    if (dtImportHourly != null) { dtImportHourly.Dispose(); }
            //}

            return iResult;
        }

        public int ImportAllEvents(int iSiteCode)
        {
            string strResult;
            int iResult = Convert.ToInt32(ImportStatus.Failed);

            try
            {
                strResult = GetAllEventsFromServer(iSiteCode);
                LogManager.WriteLog("Import AllEvents details from server:  " + strResult.Length.ToString(), LogManager.enumLogLevel.Info);

                if (string.IsNullOrEmpty(strResult))
                {
                    iResult = Convert.ToInt32(ImportStatus.EnterpriseNoData);
                }
                else
                {
                    if (SiteSetupDataAccess.ImportAllEvents(strResult))
                    {
                        iResult = Convert.ToInt32(ImportStatus.Completed);
                    }
                    else
                    {
                        iResult = Convert.ToInt32(ImportStatus.UpdateFailed);
                    }
                }
            }
            catch (Exception exImportHourly)
            {
                iResult = Convert.ToInt32(ImportStatus.Failed);
                ExceptionManager.Publish(exImportHourly);
            }

            return iResult;
        }

        public int ImportCashDeskTransactions(int iSiteCode)
        {
            //DataTable dtCashDeskTransactions = new DataTable();
            string strCashDeskTransactions = string.Empty; 
            int iResult = Convert.ToInt32(ImportStatus.Failed);
            string strXML = string.Empty;

            try
            {
                LogManager.WriteLog("Import Cash Desk Transactions details from server", LogManager.enumLogLevel.Info);
                strXML = CurrentInstallationDetails();

                if (string.IsNullOrEmpty(strXML))
                {
                    iResult = Convert.ToInt32(ImportStatus.EnterpriseNoData);
                    LogManager.WriteLog("Import Cash Desk Transactions details: No Installation.", LogManager.enumLogLevel.Info);
                }
                else
                {
                    strCashDeskTransactions = GetCashDeskTransactionsFromServer(iSiteCode, strXML);
                    if (strCashDeskTransactions!=string.Empty )
                    {
                        if (SiteSetupDataAccess.ImportCashDeskTransactions(strCashDeskTransactions))
                        {
                            iResult = Convert.ToInt32(ImportStatus.Completed);
                            LogManager.WriteLog("Import Cash Desk Transactions details from server: Complete", LogManager.enumLogLevel.Info);
             
                        }
                        else
                        {
                            iResult = Convert.ToInt32(ImportStatus.UpdateFailed);
                            LogManager.WriteLog("Import Cash Desk Transactions details from server: Failed", LogManager.enumLogLevel.Info);
                        }
                    }
                    else
                    {
                        LogManager.WriteLog("Import Cash Desk Transactions details from server: No Enterprise Data", LogManager.enumLogLevel.Info);
                        iResult = Convert.ToInt32(ImportStatus.EnterpriseNoData);
                    }
                }
            }
            catch (Exception exImportCashDeskTransactions)
            {
                iResult = Convert.ToInt32(ImportStatus.Failed);
                ExceptionManager.Publish(exImportCashDeskTransactions);
            }
            finally
            {
                //if (dtCashDeskTransactions != null) { dtCashDeskTransactions.Dispose(); }
                // Update the treasury related data irrespective of the cash desk transaction records
                SiteSetupDataAccess.UpdateTreasuryRelDataForSiteConfig();
            }

            return iResult;
        }

        public int ImportUserDetails(int iSiteCode)
        {
            string strResult;
            int iResult = Convert.ToInt32(ImportStatus.Failed);

            try
            {
                strResult = GetUserDetails(iSiteCode);
                LogManager.WriteLog("Import user details from server:  " + strResult, LogManager.enumLogLevel.Info);

                if (string.IsNullOrEmpty(strResult))
                {
                    iResult = Convert.ToInt32(ImportStatus.EnterpriseNoData);
                }
                else
                {
                    if (SiteSetupDataAccess.ImportUserDetails(strResult))
                    {
                        iResult = Convert.ToInt32(ImportStatus.Completed);
                    }
                    else
                    {
                        iResult = Convert.ToInt32(ImportStatus.UpdateFailed);
                    }
                }
            }
            catch (Exception exImportuserdetails)
            {
                iResult = Convert.ToInt32(ImportStatus.Failed);
                ExceptionManager.Publish(exImportuserdetails);
            }

            return iResult;
        }

        public int ImportUserRoles(int iSiteCode)
        {
            string strResult;
            int iResult = Convert.ToInt32(ImportStatus.Failed);

            try
            {
                strResult = GetUserRoles(iSiteCode);
                LogManager.WriteLog("Import user roles from server:  " + strResult, LogManager.enumLogLevel.Info);

                if (string.IsNullOrEmpty(strResult))
                {
                    iResult = Convert.ToInt32(ImportStatus.EnterpriseNoData);
                }
                else
                {
                    if (SiteSetupDataAccess.ImportUserRoles(strResult))
                    {
                        if (ImportUserRolesLinks(iSiteCode) > 0 && ImportObjects(iSiteCode) > 0
                            && ImportRoleAccessObjectRightLnk(iSiteCode) > 0)
                        {
                            iResult = Convert.ToInt32(ImportStatus.Completed);
                        }
                        else
                        {
                            iResult = Convert.ToInt32(ImportStatus.UpdateFailed);
                        }
                    }
                    else
                    {
                        iResult = Convert.ToInt32(ImportStatus.UpdateFailed);
                    }
                }
            }
            catch (Exception exImportUserRoles)
            {
                iResult = Convert.ToInt32(ImportStatus.Failed);
                ExceptionManager.Publish(exImportUserRoles);
            }

            return iResult;
        }

        public int ImportCalendars(int iSiteCode)
        {
            string strResult;
            int iResult = Convert.ToInt32(ImportStatus.Failed);

            try
            {
                strResult = GetCalendars(iSiteCode);
                LogManager.WriteLog("Import Calendars from server:  " + strResult, LogManager.enumLogLevel.Info);

                if (string.IsNullOrEmpty(strResult))
                {
                    iResult = Convert.ToInt32(ImportStatus.EnterpriseNoData);
                }
                else
                {
                    if (SiteSetupDataAccess.ImportCalendars(strResult))
                    {
                        iResult = Convert.ToInt32(ImportStatus.Completed);
                    }
                    else
                    {
                        iResult = Convert.ToInt32(ImportStatus.UpdateFailed);
                    }
                }
            }
            catch (Exception exImportCalendars)
            {
                iResult = Convert.ToInt32(ImportStatus.Failed);
                ExceptionManager.Publish(exImportCalendars);
            }

            return iResult;
        }

        public int ImportAAMSDetails(int iSiteCode)
        {
            string strResult;
            int iResult = Convert.ToInt32(ImportStatus.Failed);

            try
            {
                strResult = GetAAMSDetails(iSiteCode);
                LogManager.WriteLog("Import AAMS Details from server:  " + strResult, LogManager.enumLogLevel.Info);

                if (string.IsNullOrEmpty(strResult))
                {
                    iResult = Convert.ToInt32(ImportStatus.EnterpriseNoData);
                }
                else
                {
                    if (SiteSetupDataAccess.ImportAAMSDetails(strResult))
                    {
                        iResult = Convert.ToInt32(ImportStatus.Completed);
                    }
                    else
                    {
                        iResult = Convert.ToInt32(ImportStatus.UpdateFailed);
                    }
                }
            }
            catch (Exception exImportAAMSDetails)
            {
                iResult = Convert.ToInt32(ImportStatus.Failed);
                ExceptionManager.Publish(exImportAAMSDetails);
            }

            return iResult;
        }

        public int ImportInstallationGameInfo(int iSiteCode)
        {
            string strResult;
            int iResult = Convert.ToInt32(ImportStatus.Failed);

            try
            {
                strResult = GetInstallationGameInfo(iSiteCode);
                LogManager.WriteLog("Import ComponentDetails from server:  " + strResult, LogManager.enumLogLevel.Info);

                if (string.IsNullOrEmpty(strResult))
                {
                    iResult = Convert.ToInt32(ImportStatus.EnterpriseNoData);
                }
                else
                {
                    if (SiteSetupDataAccess.ImportInstallationGameInfo(strResult))
                    {
                        iResult = Convert.ToInt32(ImportStatus.Completed);
                    }
                    else
                    {
                        iResult = Convert.ToInt32(ImportStatus.UpdateFailed);
                    }
                }
            }
            catch (Exception exImportInstallationGameInfo)
            {
                iResult = Convert.ToInt32(ImportStatus.Failed);
                ExceptionManager.Publish(exImportInstallationGameInfo);
            }

            return iResult;
        }

        /// <summary>
        /// Method to import the component details for the site.
        /// </summary>
        /// <param name="iSiteCode"></param>
        /// <returns></returns>
        public int ImportComponentDetails(int iSiteCode)
        {
            string strResult;
            int iResult = Convert.ToInt32(ImportStatus.Failed);

            try
            {
                strResult = GetComponentDetails(iSiteCode);
                LogManager.WriteLog("Import ImportComponentDetails from server:  " + strResult, LogManager.enumLogLevel.Info);

                if (string.IsNullOrEmpty(strResult))
                {
                    iResult = Convert.ToInt32(ImportStatus.EnterpriseNoData);
                }
                else
                {
                    if (SiteSetupDataAccess.ImportComponentDetails(strResult))
                    {
                        iResult = Convert.ToInt32(ImportStatus.Completed);
                    }
                    else
                    {
                        iResult = Convert.ToInt32(ImportStatus.UpdateFailed);
                    }
                }
            }
            catch (Exception exImportInstallationGameInfo)
            {
                iResult = Convert.ToInt32(ImportStatus.Failed);
                ExceptionManager.Publish(exImportInstallationGameInfo);
            }

            return iResult;
        }

        /// <summary>
        /// Method to import the other game details for the site.
        /// </summary>
        /// <param name="iSiteCode"></param>
        /// <returns></returns>
        public int ImportOtherGameDetails(int iSiteCode)
        {
            string strResult;
            int iResult = Convert.ToInt32(ImportStatus.Failed);

            try
            {
                strResult = GetOtherGameDetails(iSiteCode);
                LogManager.WriteLog("Import ImportOtherGameDetails from server:  " + strResult, LogManager.enumLogLevel.Info);

                if (string.IsNullOrEmpty(strResult))
                {
                    iResult = Convert.ToInt32(ImportStatus.EnterpriseNoData);
                }
                else
                {
                    string[] strData = strResult.Split('|');

                    if (strData.Length > 0)
                    {
                        if (SiteSetupDataAccess.ImportGameCategoryDetails(strData[0]))
                        {
                            iResult = Convert.ToInt32(ImportStatus.Completed);
                        }
                        else
                        {
                            iResult = Convert.ToInt32(ImportStatus.UpdateFailed);
                        }
                    }

                    if (strData.Length > 1)
                    {
                        if (SiteSetupDataAccess.ImportGameTitleDetails(strData[1]))
                        {
                            iResult = Convert.ToInt32(ImportStatus.Completed);
                        }
                        else
                        {
                            iResult = Convert.ToInt32(ImportStatus.UpdateFailed);
                        }
                    }
                }
                //
                strResult = GetGameLibrary(iSiteCode);
                LogManager.WriteLog("Import ImportGameLibrary from server:  " + strResult, LogManager.enumLogLevel.Info);
                if (strResult.Length > 0)
                {
                    SiteSetupDataAccess.ImportGameLibraryDetails(strResult);
                }
                //
                strResult = GetPayTable(iSiteCode);
                LogManager.WriteLog("Import GetPaytable from server:  " + strResult, LogManager.enumLogLevel.Info);
                if (strResult.Length > 0)
                {
                    SiteSetupDataAccess.ImportPayTableDetails(strResult);

                }
                //
                strResult = GetInstallationGamePayTableInfo(iSiteCode);
                LogManager.WriteLog("Import InstallationGamePayTableInfo from server:  " + strResult, LogManager.enumLogLevel.Info);
                if (strResult.Length > 0) {
                    SiteSetupDataAccess.ImportInstallationGamePayTableInfoDetails(strResult);

                }
                iResult = Convert.ToInt32(ImportStatus.Completed);
            }
            catch (Exception exImportInstallationGameInfo)
            {
                iResult = Convert.ToInt32(ImportStatus.Failed);
                ExceptionManager.Publish(exImportInstallationGameInfo);
            }

            return iResult;
        }

        public int UpdateSeedValues(int iSiteCode)
        {
            string strResult;
            int iResult = Convert.ToInt32(ImportStatus.Failed);

            try
            {
                string sValue = CommonDataAccess.GetSettingValue("SiteRecover-Tables");
                LogManager.WriteLog("Tables :  " + sValue, LogManager.enumLogLevel.Info);
                strResult = GetSeedValues(iSiteCode, sValue);
                LogManager.WriteLog("Import Seed Values from server:  " + strResult, LogManager.enumLogLevel.Info);

                if (string.IsNullOrEmpty(strResult))
                {
                    iResult = Convert.ToInt32(ImportStatus.EnterpriseNoData);
                }
                else
                {
                    if (SiteSetupDataAccess.ImportSeedValues(strResult))
                    {
                        iResult = Convert.ToInt32(ImportStatus.Completed);
                    }
                    else
                    {
                        iResult = Convert.ToInt32(ImportStatus.UpdateFailed);
                    }
                }
            }
            catch (Exception exSeedValues)
            {
                iResult = Convert.ToInt32(ImportStatus.Failed);
                ExceptionManager.Publish(exSeedValues);
            }

            return iResult;
        }

        #endregion
    }
}
