using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.EnterpriseDataAccess;
using BMC.EnterpriseBusiness.Entities;
using Audit.Transport;
using Audit.BusinessClasses;
using BMC.Common.ExceptionManagement;
using BMC.Common.LogManagement;

namespace BMC.EnterpriseBusiness.Business
{
    public class VaultDeclarationBusiness
    {
        public static VaultDeclarationBusiness _Vault;

        #region For Vault Undeclared Drop Screen

        #region Data Load Methods

        public List<Vault_RegionsForDrop> GetRegionsForDrop()
        {
            using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
            {
                List<Vault_RegionsForDrop> objRegionDetails = null;
                List<rsp_Vault_GetAllRegionsResult> Vault_dsRegions = DataContext.Vault_GetAllRegions().ToList();

                objRegionDetails = (from item in Vault_dsRegions
                                    select new Vault_RegionsForDrop()
                                    {
                                        Sub_Company_Region_ID = item.Sub_Company_Region_ID,
                                        Sub_Company_Region_Name = item.Sub_Company_Region_Name
                                    }).ToList();
                return objRegionDetails;
            }
        }

        public List<Vault_SitesForDrop> Vault_GetSitesbasedonRegion(Int32 Region_ID, Int32 User_ID)
        {
            try
            {
                LogManager.WriteLog("Inside VaultDeclarationBusiness-Vault_GetSitesbasedonRegion", LogManager.enumLogLevel.Info);
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    List<Vault_SitesForDrop> objSiteDetails = null;
                    List<rsp_Vault_GetSitesForDropResult> Vault_dsSites = DataContext.Vault_GetSitesbasedonRegion(Region_ID, User_ID).ToList();

                    objSiteDetails = (from item in Vault_dsSites
                                      select new Vault_SitesForDrop()
                                      {
                                          Site_ID = item.Site_ID,
                                          Site_Name = item.Site_Name,
                                          Site_Code = item.Site_Code,
                                      }).ToList();
                    return objSiteDetails;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return null;
            }
        }

        public List<Vault_UndeclaredDrops> GetUndeclaredDrops(Int32 Vault_ID, Int32 Site_ID)
        {
            List<Vault_UndeclaredDrops> _Vault = null;
            using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
            {
                List<rsp_Vault_GetUndeclaredDropsResult> objUndeclaredDrops = DataContext.Vault_GetUndeclaredDrops(Vault_ID, Site_ID).ToList();
                _Vault = (from item in objUndeclaredDrops
                          select new Vault_UndeclaredDrops()
                          {
                              Drop_ID = item.Drop_ID,
                              Vault_ID = item.Vault_ID,
                              FillAmount = item.FillAmount,
                              BleedAmount = item.BleedAmount,
                              AdjustmentAmount = item.AdjustmentAmount,
                              Meter_Balance = item.Meter_Balance,
                              Vault_Balance = item.Vault_Balance,
                              Declared_Balance = item.Declared_Balance,
                              Declared = item.Declared,
                              Freezed = item.Freezed,
                              CreatedDate = item.CreatedDate,
                              CreateUser = item.CreateUser,
                              ModifiedDate = item.ModifiedDate,
                              ModifiedUser = item.ModifiedUser,
                              FreezedDate = item.FreezedDate,
                              FreezeUser = item.FreezeUser,
                              AuditDate = item.AuditDate,
                              AuditUser = item.AuditUser,
                              Site_Drop_Ref = item.Site_Drop_Ref,
                              Site_ID = item.Site_ID,
                              UserName = item.UserName,
                              Name = item.Name,
                              Manufacturer_Name = item.Manufacturer_Name,
                              AuditNote = item.AuditNote,
                              BMCVariance = item.BMCVariance,
                              OpeningBalance = item.OpeningBalance,
                              Type_Prefix = item.Type_Prefix,
                              VaultVariance = item.VaultVariance,
                              IsCentralDeclaration = item.IsCentralDeclaration,
                              IsWebServiceEnabled = item.IsWebServiceEnabled,
                              Capacity = item.Capacity
                          }).ToList();
                return _Vault;
            }
        }

        public List<Vault_CassetteDropDetailsResult> GetVaultCassetteDropDetails(Int32 DropID)
        {
            List<Vault_CassetteDropDetailsResult> _VaultCassettes = null;
            using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
            {
                List<rsp_Vault_GetCassetteDropDetailsResult> objVaultDropCassette = DataContext.Vault_GetCassetteDropDetails(DropID).ToList();
                _VaultCassettes = (from item in objVaultDropCassette
                                   select new Vault_CassetteDropDetailsResult()
                                   {
                                       AdjustmentAmount = item.AdjustmentAmount.Value,
                                       AuditBalance = item.AuditBalance,
                                       AudtiDate = item.AudtiDate,
                                       BleedAmount = item.BleedAmount.Value,
                                       DeclaredBalance = item.DeclaredBalance.Value,
                                       Cassette_Name = item.Cassette_Name,
                                       Cassette_ID = item.Cassette_ID,
                                       Denom = item.Denom.Value,
                                       Drop_ID = item.Drop_ID,
                                       dtCreated = item.dtCreated,
                                       dtUpdated = item.dtUpdated,
                                       FillAmount = item.FillAmount.Value,
                                       MeterBalance = item.MeterBalance.Value,
                                       Type = item.Type,
                                       VaultBalance = item.VaultBalance.Value,
                                       MaxFillAmount = item.MaxFillAmount
                                   }).ToList();
                return _VaultCassettes;
            }

        }



        #endregion

        #region Update Methods

        public int UpdateVaultDrops(decimal DeclaredValue, bool Declared, long DropID, string SiteCode, int UserId, System.Xml.Linq.XElement cassetteXML)
        {
            int result = 0;
            using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                result = DataContext.Vault_UpdateVaultDrops(DeclaredValue, Declared, DropID, SiteCode, UserId, cassetteXML);
            return result;
        }

        #endregion

        #endregion

        #region For Declared Drop Details

        public List<Vault_SitesForDrop> GetAllSitesDetails(Int32 User_ID)
        {
            using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
            {
                List<Vault_SitesForDrop> objSiteDetails = null;
                List<rsp_Vault_GetSitesForDropResult> Vault_dsSites = DataContext.GetVaultDeclarationSiteDetails(User_ID).ToList();

                objSiteDetails = (from item in Vault_dsSites
                                  select new Vault_SitesForDrop()
                                  {
                                      Site_ID = item.Site_ID,
                                      Site_Name = item.Site_Name,
                                      Site_Code = item.Site_Code,
                                  }).ToList();
                return objSiteDetails;
            }
        }

        public List<Vault_UndeclaredDrops> GetDeclaredDrops(Int32 Vault_ID, Int32 Site_ID, DateTime StartDate, DateTime EndDate, Int32 Varaincetype)
        {
            List<Vault_UndeclaredDrops> _Vault = null;
            using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
            {

                List<rsp_Vault_GetUndeclaredDropsResult> objUndeclaredDrops = DataContext.Vault_GetDeclaredDrops(Vault_ID, Site_ID, StartDate, EndDate, Varaincetype).ToList();
                _Vault = (from item in objUndeclaredDrops
                          select new Vault_UndeclaredDrops()
                          {
                              Drop_ID = item.Drop_ID,
                              Vault_ID = item.Vault_ID,
                              FillAmount = item.FillAmount,
                              BleedAmount = item.BleedAmount,
                              AdjustmentAmount = item.AdjustmentAmount,
                              Meter_Balance = item.Meter_Balance,
                              Vault_Balance = item.Vault_Balance,
                              Declared_Balance = item.Declared_Balance,
                              Declared = item.Declared,
                              Freezed = item.Freezed,
                              CreatedDate = item.CreatedDate,
                              CreateUser = item.CreateUser,
                              ModifiedDate = item.ModifiedDate,
                              ModifiedUser = item.ModifiedUser,
                              FreezedDate = item.FreezedDate,
                              FreezeUser = item.FreezeUser,
                              AuditDate = item.AuditDate,
                              AuditUser = item.AuditUser,
                              Site_Drop_Ref = item.Site_Drop_Ref,
                              Site_ID = item.Site_ID,
                              UserName = item.UserName,
                              AuditNote = item.AuditNote,
                              BMCVariance = item.BMCVariance,
                              VaultVariance = item.VaultVariance,
                              Name = item.Name,
                              Manufacturer_Name = item.Manufacturer_Name,
                              OpeningBalance = item.OpeningBalance,
                              Type_Prefix = item.Type_Prefix,
                              CanFreeze = item.CanFreeze.Value,
                              IsCentralDeclaration = item.IsCentralDeclaration,
                              IsWebServiceEnabled = item.IsWebServiceEnabled,
                              Capacity = item.Capacity
                          }).ToList();
                return _Vault;
            }
        }

        #endregion

        #region For Vault Drop Audited Data

        public int Vault_UpdateAuditData(int DropID, decimal BmcTotal, decimal VaultTotal, decimal DeclaredValue, string AuditNotes, bool isFreexed, int UserID, string SiteCode, bool FreezePrevious, string CassetteDetails)
        {
            int result = 0;
            using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                result = DataContext.Vault_UpdateAuditData(DropID, BmcTotal, VaultTotal, DeclaredValue, AuditNotes, isFreexed, UserID, SiteCode, FreezePrevious, CassetteDetails);
            return result;
        }

        #endregion

        #region Auditing Vault Screen
        public List<Vault_GetDeclaredDropsForAudit> GetDeclaredDropsForAudit(long DropID)
        {
            List<Vault_GetDeclaredDropsForAudit> _Vault = null;
            using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
            {
                List<rsp_Vault_GetDeclaredDropsForAuditResult> objDeclaresDrops = DataContext.Vault_GetDeclaredDropsForAudit(DropID).ToList();
                _Vault = (from item in objDeclaresDrops
                          select new Vault_GetDeclaredDropsForAudit()
                          {
                              AdjustmentAmount = item.AdjustmentAmount,
                              AuditBalance = item.AuditBalance,
                              AuditNote = item.AuditNote,
                              AudtiDate = item.AudtiDate,
                              BleedAmount = item.BleedAmount,
                              Cassette_ID = item.Cassette_ID,
                              Cassette_Name = item.Cassette_Name,
                              Denom = item.Denom,
                              Declared_Balance = item.Declared_Balance,
                              Drop_ID = item.Drop_ID,
                              DeclaredBalance = item.DeclaredBalance,
                              dtCreated = item.dtCreated,
                              dtUpdated = item.dtUpdated,
                              FillAmount = item.FillAmount,
                              FrozeUser = item.FrozeUser,
                              IsFrozen = item.IsFrozen,
                              Meter_Balance = item.Meter_Balance,
                              Vault_Balance = item.Vault_Balance,
                              VaultBalance = item.VaultBalance,
                              Type = item.Type,
                              IsVaultWebServiceEnabled = item.IsVaultWebServiceEnabled,
                              MaxFillAmount = item.MaxFillAmount
                          }).ToList();
                return _Vault;
            }

        }

        public void AuditModifiedDataForVaultAdjustment(string sVaultName, string sScreenName, string sField, string sPrevValue, string sNewValue, int iUserId, string strUsername)
        {
            try
            {
                Audit_History AH = new Audit_History();
                AH.EnterpriseModuleName = ModuleNameEnterprise.VaultManager;
                AH.Audit_Screen_Name = sScreenName;
                AH.Audit_Desc = "Vault" + sVaultName + " modified  ..[" + sField + "] : " + sPrevValue + " -> " + sNewValue;
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

        #endregion


        #region Transaction Reason
        public List<Vault_GetTransactionReason> GetTransactionReasons()
        {
            List<Vault_GetTransactionReason> reason = null;
            using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
            {
                List<rsp_Vault_GetTransactionReasonsResult> reasons = DataContext.rsp_Vault_GetTransactionReasons().ToList();
                int i = 1;
                reason = (from item in reasons
                          select new Vault_GetTransactionReason()
                          {
                              SNo = i++,
                              Reason_ID = item.Reason_ID,
                              ReasonType = item.ReasonType,
                              Reason_Description = item.Reason_Description
                          }
                       ).ToList();
                return reason;
            }
        }

        public bool UpdateTransactionReason(int ReasonID, string ReasonDescription)
        {
            bool retVal = false;
            using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
            {
                retVal = DataContext.usp_Vault_UpdateTransactionReason(ReasonID, ReasonDescription) != -1;

                return retVal;
            }
        }
        #endregion
    }
}

