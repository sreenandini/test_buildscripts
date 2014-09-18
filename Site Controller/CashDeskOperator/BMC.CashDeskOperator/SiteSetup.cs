using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BMC.Business.CashDeskOperator;

namespace BMC.CashDeskOperator.BusinessObjects
{
    public class SiteSetupConfiguration : ISiteRecovery
    {
        private static SiteSetupConfiguration _SiteSetupConfiguration = null;
        private SiteSetupBusiness oSiteSetupbusiness = new SiteSetupBusiness();

        #region Private Constructor
        private SiteSetupConfiguration() { }
        #endregion

        public static SiteSetupConfiguration SiteSetupConfigurationInstance
        {
            get
            {
                if (_SiteSetupConfiguration == null)
                    _SiteSetupConfiguration = new SiteSetupConfiguration();

                return _SiteSetupConfiguration;
            }
        }

        #region ISiteRecovery Members

        public bool IsValidSiteCode(int iSiteCode)
        {
            return oSiteSetupbusiness.IsValidSiteCode(iSiteCode);
        }
        public bool DatabaseIsEmpty() { return oSiteSetupbusiness.DatabaseIsEmpty(); }

        public int EnterpriseUrlIsExists(string sServername, string sitecode) { return oSiteSetupbusiness.EnterpriseUrlIsExists(sServername, sitecode); }

        public int ImportSiteDetails(int iSiteCode) { return oSiteSetupbusiness.ImportSiteDetails(iSiteCode); }

        public int ImportSiteAlliance(int iSiteCode) { return oSiteSetupbusiness.ImportSiteAlliance(iSiteCode); }

        public int ImportInstallations(int iSiteCode) { return oSiteSetupbusiness.ImportInstallations(iSiteCode); }

        public bool ImportMachines(int iSiteCode) { return oSiteSetupbusiness.ImportMachines(iSiteCode); }

        public int ImportBarPositions(int iSiteCode) { return oSiteSetupbusiness.ImportBarPositions(iSiteCode); }

        public int ImportZones(int iSiteCode) { return oSiteSetupbusiness.ImportZones(iSiteCode); }

        public int ImportLatestMeterHistory(int iSiteCode) { return oSiteSetupbusiness.ImportLatestMeterHistory(iSiteCode); }

        public bool ReseedCollectionBatch(int iSiteCode) { return oSiteSetupbusiness.ReseedCollectionBatch(iSiteCode); }

        public int CheckAuthorizationCode(string iAuthCode, int iSiteCode, string TransactionType) { return oSiteSetupbusiness.CheckAuthorizationCode(iAuthCode, iSiteCode, TransactionType); }

        public bool UpdateCheckPoints(int iSiteCode, int Value, string sTableName) { return oSiteSetupbusiness.UpdateCheckPoints(iSiteCode, Value, sTableName); }

        public int UpdateSiteStatus(int iSiteCode, string sUpdate) { return oSiteSetupbusiness.UpdateSiteStatus(iSiteCode, sUpdate); }

        public DataTable GetTableDetails() { return oSiteSetupbusiness.GetTableDetails(); }

        public Dictionary<int, string> GetCheckPointsStatus(byte iStatus) { return oSiteSetupbusiness.GetCheckPointsStatus(iStatus); }

        public int ImportCollections(int iSiteCode) { return oSiteSetupbusiness.ImportCollections(iSiteCode); }

        public bool ImportTickets(int iSiteCode) { return oSiteSetupbusiness.ImportTickets(iSiteCode); }

        public int ImportSystemSettings(int iSiteCode) { return oSiteSetupbusiness.ImportSystemSettings(iSiteCode); }

        public int ImportDailyRead(int iSiteCode) { return oSiteSetupbusiness.ImportDailyRead(iSiteCode); }

        public int ImportHourly(int iSiteCode) { return oSiteSetupbusiness.ImportHourly(iSiteCode); }

        public int ImportAllEvents(int iSiteCode) { return oSiteSetupbusiness.ImportAllEvents(iSiteCode); }

        public int ImportCashDeskTransactions(int iSiteCode) { return oSiteSetupbusiness.ImportCashDeskTransactions(iSiteCode); }

        public int ImportUserDetails(int iSiteCode) { return oSiteSetupbusiness.ImportUserDetails(iSiteCode); }

        public int ImportUserRoles(int iSiteCode) { return oSiteSetupbusiness.ImportUserRoles(iSiteCode); }

        public int ImportCalendars(int iSiteCode) { return oSiteSetupbusiness.ImportCalendars(iSiteCode); }

        public int ImportAAMSDetails(int iSiteCode) { return oSiteSetupbusiness.ImportAAMSDetails(iSiteCode); }

        public int ImportInstallationGameInfo(int iSiteCode) { return oSiteSetupbusiness.ImportInstallationGameInfo(iSiteCode); }

        public bool UpdateAllCheckPoints(int Value) { return oSiteSetupbusiness.UpdateAllCheckPoints(Value); }

        public bool FlattenSystem() { return oSiteSetupbusiness.FlattenSystem(); }

        public bool ResetTransactionKey(string iAuthCode, int iSiteCode) { return oSiteSetupbusiness.ResetTransactionKey(iAuthCode, iSiteCode); }

        public string CheckNGA(string strMACList, int iSiteCode) { return oSiteSetupbusiness.CheckNGA(strMACList, iSiteCode); }

        public string GetSettingValue(string settingname) { return oSiteSetupbusiness.GetSettingValue(settingname); }

        public string GetServiceStatus(string strserviceName) { return oSiteSetupbusiness.GetServiceStatus(strserviceName); }

        public bool EndService(string strserviceName) { return oSiteSetupbusiness.EndService(strserviceName); }

        public bool StartService(string strserviceName) { return oSiteSetupbusiness.StartService(strserviceName); }

        public int ImportComponentDetails(int iSiteCode) { return oSiteSetupbusiness.ImportComponentDetails(iSiteCode); }

        public int ImportOtherGameDetails(int iSiteCode) { return oSiteSetupbusiness.ImportOtherGameDetails(iSiteCode); }

        public int UpdateSeedValues(int iSiteCode) { return oSiteSetupbusiness.UpdateSeedValues(iSiteCode); }


        #endregion
    }
}
