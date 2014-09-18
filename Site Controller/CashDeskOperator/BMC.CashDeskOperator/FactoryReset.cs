using System;
using System.Linq;
using System.Text;
using Microsoft.Win32;
using System.Collections.Generic;
using System.Configuration;
using BMC.Business.CashDeskOperator;
using BMC.Transport.CashDeskOperatorEntity;
namespace BMC.CashDeskOperator.BusinessObjects
{
    public class FactoryResetMethods:IFactoryReset
    {
        
        private static FactoryResetMethods _FactoryResetMethods = null;
        FactoryResetBusiness oFactoryResetBusiness = new FactoryResetBusiness();
        FactoryResetBusiness.ReadRegistrySettings oFRBReadRegistry = FactoryResetBusiness.ReadRegistrySettings.ReadRegistrySettinginstance;
        FactoryResetBusiness.DatabaseCredentials oFRBDatabaseCredentials = FactoryResetBusiness.DatabaseCredentials.DatabaseCredentialsinstance;
         
         #region Private Constructor
                private FactoryResetMethods() { }
            #endregion
         
        public static FactoryResetMethods FactoryResetMethodsInstance
            {

                get
                {
                    if (_FactoryResetMethods == null)
                        _FactoryResetMethods = new FactoryResetMethods();

                    return _FactoryResetMethods;
                }
            }

        #region IFactoryReset Members

           public Dictionary<string, string> GetKeys(string sectionname)
                
            {
                return FactoryResetBusiness.ReadConfigSettings<string>.GetKeys(sectionname);
               
            }

           //public Dictionary<string, string> GetRegistryEntries(string Registrypath)
           // {
           //     return oFRBReadRegistry.GetRegistryEntries(Registrypath);
             
           // }           

           //public bool SetRegistryEntries(Dictionary<string, string> dictSetregistry, string strPath)
           // {
           //     return oFRBReadRegistry.SetRegistryEntries(dictSetregistry, strPath);
           // }

           public Dictionary<string, string> RetrieveServerDetails(string ConnectionString)
            {
                return oFRBDatabaseCredentials.RetrieveServerDetails(ConnectionString);
            }

           public string MakeConnectionString(Dictionary<string, string> Credentials)
            {
                return oFRBDatabaseCredentials.MakeConnectionString(Credentials);
            }

           public bool GetServerDetails(string ConnectionString)
            {
                return oFRBDatabaseCredentials.GetServerDetails(ConnectionString);
            }
           public int CreateSqlDatabaseBackUp(FactoryReset objFactoryReset)
           {
               return oFactoryResetBusiness.CreateSqlDatabaseBackUp(objFactoryReset);
           }

           public int CreateDBZip(FactoryReset objFactoryReset)
           {
               return oFactoryResetBusiness.CreateDBZip(objFactoryReset);
           }

           public bool TestConnectionDB(Dictionary<string, string> Credentials)
            {
                return oFactoryResetBusiness.TestConnectionToDB(Credentials);
            }
           public bool TestConnectionDB(string Connectionstring)
           {
               return oFactoryResetBusiness.TestConnectionToDB(Connectionstring);
           }
           public bool CheckInstallation()
           {
               return oFactoryResetBusiness.CheckInstallation();
           }

           public bool CheckDataToExport()
           {
               return oFactoryResetBusiness.CheckDataToExport();
           }

           public bool RunScripts()
           {
               return oFactoryResetBusiness.RunScripts();
           }

            public bool FactoryResetHistory(bool isCompleted, int ResetModeID, string UserName, ref int FRHistoryID)
            {
                return oFactoryResetBusiness.FactoryResetHistory(isCompleted, ResetModeID, UserName, ref FRHistoryID);
            }

           public bool BackupConstraint(bool BackupConstraint, int ResetModeID)
           {
               return oFactoryResetBusiness.BackupConstraint(BackupConstraint, ResetModeID);
           }

           public bool DeleteAddConstraint(bool DropConstraint)
           {
               return oFactoryResetBusiness.DeleteAddConstraint(DropConstraint);
           }

           public bool ResetTable(int Mode_Id)
           {
               return oFactoryResetBusiness.ResetTable(Mode_Id);
           }

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
          public int CheckAuthorizationCode(string iAuthCode) { return oFactoryResetBusiness.CheckAuthorizationCode(iAuthCode); }
          public string GetSettingValue(string settingname) { return oFRBDatabaseCredentials.GetSettingValue(settingname); }
          public bool ResetTransactionKey(string iAuthCode) { return oFactoryResetBusiness.ResetTransactionKey(iAuthCode); }
        
        #endregion
     
        
    }

   
}
