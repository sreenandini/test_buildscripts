using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.Common.LogManagement;
using BMC.EnterpriseBusiness.Entities;
using BMC.Common.ExceptionManagement;
using BMC.EnterpriseDataAccess;
using System.Data.Linq.Mapping;
using Audit.Transport;
using Audit.BusinessClasses;
using System.Reflection;

namespace BMC.EnterpriseBusiness.Business
{    
    public class AftsettingsBusiness
    {

        private static AftsettingsBusiness _Aft;
        public AftsettingsBusiness() { }
        public static AftsettingsBusiness CreateInstance()
        {
            if (_Aft == null)
                _Aft = new AftsettingsBusiness();
            return _Aft;
        }

        /// <summary>
        /// load denom value from adminsite entity
        /// </summary>
        /// <param name="siteID"></param>
        /// <returns></returns>
        public List<AftsettingsEntity> Aftsettingdenome(int siteID)
        {
            try
            {
                LogManager.WriteLog("Inside Get Denom AftsettingsBusiness", LogManager.enumLogLevel.Info);
                List<AftsettingsEntity> objcoll = new List<AftsettingsEntity>();

                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    foreach (var entity in DataContext.GetAFTSettingsDenom(siteID))
                    {
                        objcoll.Add(new AftsettingsEntity()
                        {
                          Denom=entity.Denom   
                        });
                    }
                    LogManager.WriteLog("End of Get Denom AftsettingsBusiness", LogManager.enumLogLevel.Info);
                    return objcoll;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                throw ex;
            }
        }

        /// <summary>
        /// Delete aftsettings value with site id ,denom
        /// </summary>
        /// <param name="siteID"></param>
        /// <param name="denom"></param>
        public void DeleteAftsettingDetails(int siteID, string var)
        {
            try
            {
                LogManager.WriteLog("Inside DeleteAftsettingDetails() in  AftsettingsBusiness", LogManager.enumLogLevel.Info);
                EnterpriseDataContext obj = EnterpriseDataContextHelper.GetDataContext();
                obj.deleteAFTSettings(siteID, var);
                LogManager.WriteLog("End DeleteAftsettingDetails() in  AftsettingsBusiness", LogManager.enumLogLevel.Info);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
        /// <summary>
        /// Load aftsettings details from db 
        /// </summary>
        /// <param name="siteID"></param>
        /// <param name="Denom"></param>
        /// <returns></returns>
        public List<AftsettingsEntity> AftsettingDetails(int siteID, int Denom)
        {
            try
            {
                LogManager.WriteLog("Inside AftsettingDetails in AftsettingsBusiness", LogManager.enumLogLevel.Info);
                List<AftsettingsEntity> objcoll = new List<AftsettingsEntity>();

                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    foreach (var entity in DataContext.GetAFTSetting(siteID, Denom))
                    {
                        objcoll.Add(new AftsettingsEntity()
                        {

                            AFTSetting_NO = entity.AFTSetting_NO,
                            AFTTransactionsAllowed = entity.AFTTransactionsAllowed,
                            AllowCashableDeposits = entity.AllowCashableDeposits,
                            AllowCashWithdrawal = entity.AllowCashWithdrawal,
                            AllowNonCashableDeposits = entity.AllowNonCashableDeposits,
                            AllowPartialTransfers = entity.AllowPartialTransfers,
                            AllowRedeemOffers = entity.AllowRedeemOffers,
                            AllowPointsWithdrawal = entity.AllowPointsWithdrawal,
                            AllowOffers = entity.AllowOffers,
                            AutoDepositCashableCreditsonCardOut = entity.AutoDepositCashableCreditsonCardOut,
                            AutoDepositNonCashableCreditsonCardOut = entity.AutoDepositNonCashableCreditsonCardOut,
                            Denom = entity.Denom,
                            Option1WithdrawalAmount = entity.Option1WithdrawalAmount,
                            Option2WithdrawalAmount = entity.Option2WithdrawalAmount,
                            Option3WithdrawalAmount = entity.Option3WithdrawalAmount,
                            Option4WithdrawalAmount = entity.Option4WithdrawalAmount,
                            Option5WithdrawalAmount = entity.Option5WithdrawalAmount,
                            EFTTimeoutValue = entity.EFTTimeoutValue,
                            MaxWithDrawAmount = entity.MaxWithDrawAmount,
                            MaxDepositAmount = entity.MaxDepositAmount,
                        });
                    }
                    LogManager.WriteLog("End AftsettingDetails() in AftsettingsBusiness", LogManager.enumLogLevel.Info);
                    return objcoll;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                throw ex;

            }
        }

       

        public bool AftValueCompare(List<CompareAFTEntity> OldEntity, List<CompareAFTEntity> currentEntity)
        {
            bool result = false;
            try
            {
                if (OldEntity == null || OldEntity.Count<=0)
                    return true;
                else
                {
                    for (int i = 0; i < currentEntity.Count; i++)
                    {
                        if (OldEntity[i].Value.ToString().ToUpper() != currentEntity[i].Value.ToString().ToUpper())
                        {
                            result = true;
                            return result;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return result;
        }

        /// <summary>
        /// Update and insert Aftsettings .....
        /// </summary>
        /// <param name="siteID"></param>
        /// <param name="denom"></param>
        public void UpdateAftsettingDetails(
                               bool AFTTransactionsAllowed,
                               bool AllowCashableDeposits,
                               bool AllowNonCashableDeposits,
                               bool AllowRedeemOffers,
                               bool AllowPointsWithdrawal,
                               bool AllowCashWithdrawal,
                               bool AllowPartialTransfers,
                               bool AutoDepositNonCashableCreditsonCardOut,
                               bool AutoDepositCashableCreditsonCardOut,
                               bool AllowOffers,
                               int EFTTimeoutValue,
                               int Option1WithdrawalAmount,
                               int Option2WithdrawalAmount,
                               int Option3WithdrawalAmount,
                               int Option4WithdrawalAmount,
                               int Option5WithdrawalAmount,
                               int MaxDepositAmount,
                               int MaxWithDrawAmount,
                               int siteID,
                               int denom
                                           )
        {
            LogManager.WriteLog("Inside UpdateAftsettingDetails() in AftsettingsBusiness", LogManager.enumLogLevel.Info);
            try
            {
                EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext();
                List<CompareAFTEntity> objDBvalue = new List<CompareAFTEntity>();
                


                foreach (var collectionvalue in DataContext.GetAFTSetting(siteID, denom))
                {

                    objDBvalue.Add(new CompareAFTEntity { Name = "AFTTransactionsAllowed", Value = collectionvalue.AFTTransactionsAllowed });
                    objDBvalue.Add(new CompareAFTEntity { Name = "AllowCashableDeposits", Value = collectionvalue.AllowCashableDeposits });
                    objDBvalue.Add(new CompareAFTEntity { Name = "AllowNonCashableDeposits", Value = collectionvalue.AllowNonCashableDeposits });
                    objDBvalue.Add(new CompareAFTEntity { Name = "AllowRedeemOffers", Value = collectionvalue.AllowRedeemOffers });
                    objDBvalue.Add(new CompareAFTEntity { Name = "AllowPointsWithdrawal", Value = collectionvalue.AllowPointsWithdrawal });
                    objDBvalue.Add(new CompareAFTEntity { Name = "AllowCashWithdrawal", Value = collectionvalue.AllowCashWithdrawal });
                    objDBvalue.Add(new CompareAFTEntity { Name = "AllowPartialTransfers", Value = collectionvalue.AllowPartialTransfers });
                    objDBvalue.Add(new CompareAFTEntity { Name = "AutoDepositNonCashableCreditsonCardOut", Value = collectionvalue.AutoDepositNonCashableCreditsonCardOut });
                    objDBvalue.Add(new CompareAFTEntity { Name = "AutoDepositCashableCreditsonCardOut", Value = collectionvalue.AutoDepositCashableCreditsonCardOut });
                    objDBvalue.Add(new CompareAFTEntity { Name = "AllowOffers", Value = collectionvalue.AllowOffers });
                    objDBvalue.Add(new CompareAFTEntity { Name = "EFTTimeoutValue", Value = collectionvalue.EFTTimeoutValue });
                    objDBvalue.Add(new CompareAFTEntity { Name = "Option1WithdrawalAmount", Value = collectionvalue.Option1WithdrawalAmount });
                    objDBvalue.Add(new CompareAFTEntity { Name = "Option2WithdrawalAmount", Value = collectionvalue.Option2WithdrawalAmount });
                    objDBvalue.Add(new CompareAFTEntity { Name = "Option3WithdrawalAmount", Value = collectionvalue.Option3WithdrawalAmount });
                    objDBvalue.Add(new CompareAFTEntity { Name = "Option4WithdrawalAmount", Value = collectionvalue.Option4WithdrawalAmount });
                    objDBvalue.Add(new CompareAFTEntity { Name = "Option5WithdrawalAmount", Value = collectionvalue.Option5WithdrawalAmount });
                    objDBvalue.Add(new CompareAFTEntity { Name = "MaxDepositAmount", Value = collectionvalue.MaxDepositAmount });
                    objDBvalue.Add(new CompareAFTEntity { Name = "MaxWithDrawAmount", Value = collectionvalue.MaxWithDrawAmount });
                    objDBvalue.Add(new CompareAFTEntity { Name = "siteID", Value = siteID });
                    objDBvalue.Add(new CompareAFTEntity { Name = "denom", Value = denom });                  
                }


                if (objDBvalue == null || objDBvalue.Count == 0)
                {

                }
                List<CompareAFTEntity> updatedvalue = new List<CompareAFTEntity>();

                updatedvalue.Add(new CompareAFTEntity { Name = "AFTTransactionsAllowed", Value = AFTTransactionsAllowed });
                updatedvalue.Add(new CompareAFTEntity { Name = "AllowCashableDeposits", Value = AllowCashableDeposits });
                updatedvalue.Add(new CompareAFTEntity { Name = "AllowNonCashableDeposits", Value = AllowNonCashableDeposits });
                updatedvalue.Add(new CompareAFTEntity { Name = "AllowRedeemOffers", Value = AllowRedeemOffers });
                updatedvalue.Add(new CompareAFTEntity { Name = "AllowPointsWithdrawal", Value = AllowPointsWithdrawal });
                updatedvalue.Add(new CompareAFTEntity { Name = "AllowCashWithdrawal", Value = AllowCashWithdrawal });
                updatedvalue.Add(new CompareAFTEntity { Name = "AllowPartialTransfers", Value = AllowPartialTransfers });
                updatedvalue.Add(new CompareAFTEntity { Name = "AutoDepositNonCashableCreditsonCardOut", Value = AutoDepositNonCashableCreditsonCardOut });
                updatedvalue.Add(new CompareAFTEntity { Name = "AutoDepositCashableCreditsonCardOut", Value = AutoDepositCashableCreditsonCardOut });
                updatedvalue.Add(new CompareAFTEntity { Name = "AllowOffers", Value = AllowOffers });
                updatedvalue.Add(new CompareAFTEntity { Name = "EFTTimeoutValue", Value = EFTTimeoutValue });
                updatedvalue.Add(new CompareAFTEntity { Name = "Option1WithdrawalAmount", Value = Option1WithdrawalAmount });
                updatedvalue.Add(new CompareAFTEntity { Name = "Option2WithdrawalAmount", Value = Option2WithdrawalAmount });
                updatedvalue.Add(new CompareAFTEntity { Name = "Option3WithdrawalAmount", Value = Option3WithdrawalAmount });
                updatedvalue.Add(new CompareAFTEntity { Name = "Option4WithdrawalAmount", Value = Option4WithdrawalAmount });
                updatedvalue.Add(new CompareAFTEntity { Name = "Option5WithdrawalAmount", Value = Option5WithdrawalAmount });
                updatedvalue.Add(new CompareAFTEntity { Name = "MaxDepositAmount", Value = MaxDepositAmount });
                updatedvalue.Add(new CompareAFTEntity { Name = "MaxWithDrawAmount", Value = MaxWithDrawAmount });
                updatedvalue.Add(new CompareAFTEntity { Name = "siteID", Value = siteID });
                updatedvalue.Add(new CompareAFTEntity { Name = "denom", Value = denom });     

              
                if (AftValueCompare(objDBvalue, updatedvalue))
                {
                    EnterpriseDataContext objupdate = EnterpriseDataContextHelper.GetDataContext();

                    objupdate.InsertAFTSettings(AFTTransactionsAllowed,
                                           AllowCashableDeposits,
                                           AllowNonCashableDeposits,
                                           AllowRedeemOffers,
                                           AllowPointsWithdrawal,
                                           AllowCashWithdrawal,
                                           AllowPartialTransfers,
                                           AutoDepositNonCashableCreditsonCardOut,
                                           AutoDepositCashableCreditsonCardOut,
                                           AllowOffers,
                                           EFTTimeoutValue,
                                           Option1WithdrawalAmount,
                                           Option2WithdrawalAmount,
                                           Option3WithdrawalAmount,
                                           Option4WithdrawalAmount,
                                           Option5WithdrawalAmount,
                                           MaxDepositAmount,
                                           MaxWithDrawAmount,
                                           siteID,
                                           denom);
                    LogManager.WriteLog("End UpdateAftsettingDetails() in AftsettingsBusiness", LogManager.enumLogLevel.Info);

                }


            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

        }



        public static bool Aftlistvaluecompare<T>(IEnumerable<T> updatedvalue, IEnumerable<T> objDBvalue)
        {
            var cnt = new Dictionary<T, int>();
            foreach (T s in updatedvalue)
            {
                if (cnt.ContainsKey(s))
                {
                    cnt[s]++;
                }
                else
                {
                    cnt.Add(s, 1);
                }
            }
            foreach (T s in objDBvalue)
            {
                if (cnt.ContainsKey(s))
                {
                    cnt[s]--;
                }
                else
                {
                    return false;
                }
            }
            return cnt.Values.All(c => c == 0);
        }

        /// <summary>
        /// Insert into Audit Table .....Create by Kishore sivagnanam
        /// </summary>
        public void AuditNewEntry(ModuleNameEnterprise moduleName, string strScreenName, string strField, string strNewItem, int iUserId, string strUsername, string SiteName)
        {
            try
            {
                //Calling Audit Method
                Audit_History AH = new Audit_History();
                //Populate required Values            
                AH.EnterpriseModuleName = moduleName;
                AH.Audit_Screen_Name = strScreenName;
                AH.Audit_Desc = "Base Denom [" + strNewItem + "] added to " + strScreenName + " for Site: " + SiteName;
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
        public bool AuditModifiedData(ModuleNameEnterprise moduleName, AftsettingsEntity modifiedEntity, AftsettingsEntity originalEntity,int denomvalue, int userID, string userName, string SiteName)
        {
            try
            {
                
                    IDictionary<string, string> dic_getVal = new Dictionary<string, string>();
                    string strFieldName = ""; 
                    string sNewValue = "";
                    string sOldValue = "";
                    string strValue = "";
                    foreach (PropertyInfo prop in modifiedEntity.GetType().GetProperties())
                    {
                        if (!Convert.ToString(prop.GetValue(modifiedEntity, null)).Equals(Convert.ToString(prop.GetValue(originalEntity, null))))
                        {
                            strValue = Convert.ToString(prop.GetValue(originalEntity, null)) + "~" + Convert.ToString(prop.GetValue(modifiedEntity, null));
                            dic_getVal.Add(new KeyValuePair<string, string>(prop.Name, strValue));
                        }
                    }
                    
                    AuditViewerBusiness AVB = new AuditViewerBusiness(Common.Utilities.DatabaseHelper.GetConnectionString());
                    foreach (KeyValuePair<string, string> pair in dic_getVal)
                    {
                        strFieldName=pair.Key;
                        sOldValue=pair.Value.Split('~')[0];
                        sNewValue=pair.Value.Split('~')[1];
                        Audit_History AH = new Audit_History();
                        AH.EnterpriseModuleName = moduleName;
                        AH.Audit_Screen_Name = "AFT Settings";
                        AH.Audit_Desc = strFieldName+" setting for Base Denom [" + denomvalue + "] is  Modified  from " + sOldValue +" to "+sNewValue+ " for Site: " + SiteName;
                        AH.AuditOperationType = OperationType.MODIFY;
                        AH.Audit_Field =strFieldName ;
                        AH.Audit_Old_Vl =sOldValue ;
                        AH.Audit_New_Vl = sNewValue;
                        AH.Audit_Slot = string.Empty;
                        AH.Audit_User_ID = userID;
                        AH.Audit_User_Name = userName;
                        
                        AVB.InsertAuditData(AH, true);

                    }
                
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
                return false;
            }

            return true;
        }
        public void AuditDeleteddata(ModuleNameEnterprise sModuleName, string sScreenName, string sField, string sPrevValue, int iUserId, string strUsername, string SiteName)
        {
            try
            {
                Audit_History AH = new Audit_History();
                AH.EnterpriseModuleName = sModuleName;
                AH.Audit_Screen_Name = sScreenName;
                AH.Audit_Desc =  sField +" " + sPrevValue + "  is deleted  for Site: " + SiteName;
                AH.AuditOperationType = OperationType.DELETE;
                AH.Audit_Field = sField;
                AH.Audit_User_ID = iUserId;
                AH.Audit_User_Name = strUsername;
               AH.Audit_New_Vl = string.Empty; //current value
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

    }

}




