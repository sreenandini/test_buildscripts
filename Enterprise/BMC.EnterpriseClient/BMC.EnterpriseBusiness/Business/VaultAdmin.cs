using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.EnterpriseDataAccess;
using BMC.EnterpriseBusiness.Entities;
using System.Data.Linq;
using System.Xml.Linq;

namespace BMC.EnterpriseBusiness.Business
{
    public class VaultAdmin
    {
        #region Load Methods

        public List<Vault_GetAllDevices> LoadVaultDetails(Int32 User_ID)
        {
            using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
            {
                List<Vault_GetAllDevices> lstVaults = new List<Vault_GetAllDevices>();
                List<rsp_Vault_GetAllDevicesResult> dsVaults = DataContext.Vault_GetAllDevices(User_ID).ToList();
                foreach (rsp_Vault_GetAllDevicesResult Vaults in dsVaults)
                {
                    lstVaults.Add(new Vault_GetAllDevices()
                    {
                        Site_Code = Vaults.Site_Code,
                        Site_ID = Vaults.Site_ID,
                        Site_Name = Vaults.Site_Name,
                        Vault = Vaults.Vault,
                        Vault_ID = Vaults.Vault_ID,
                        Serial_No=Vaults.Serial_No,
                        Active=Vaults.Active,
                        Status=Vaults.Status
                    });
                }
                return lstVaults;
            }
        }

        public List<VaultManufacturers> GetAllManufacturers()
        {
            using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
            {

                List<rsp_Vault_GetAllManufacturersResult> lst = DataContext.rsp_Vault_GetAllManufacturers().ToList();

                List<VaultManufacturers> dsManufacturers = (from x in lst
                                                            select new VaultManufacturers()
                                                            {
                                                                Manufacturer_ID = x.Manufacturer_ID,
                                                                Manufacturer_Name = x.Manufacturer_Name
                                                            }).ToList();
                return dsManufacturers;
            }
        }

        public VaultDetails GetVaultDetails(int Vault_ID)
        {
            VaultDetails _Vault = new VaultDetails();
            using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
            {
                List<rsp_Vault_GetVaultDetailsResult> dsDevices = DataContext.Vault_GetVaultDetails(Vault_ID).ToList();
                foreach (rsp_Vault_GetVaultDetailsResult vault in dsDevices)
                {
                    _Vault.Alert_Level = vault.Alert_Level.Value;
                    _Vault.Created_Date = vault.Created_Date.Value;
                    //_Vault.End_Date = vault.End_Date.Value;
                    _Vault.NAME = vault.NAME;
                    _Vault.Serial_NO = vault.Serial_NO;
                    _Vault.Site_ID = vault.Site_ID.Value;
                    _Vault.Vault_ID = vault.Vault_ID;
                    _Vault.Active = vault.Active;
                    _Vault.Type_Prefix = vault.Type_Prefix;
                    _Vault.Manufacturer_ID = vault.Manufacturer_ID;
                    _Vault.Capacity = vault.Capacity;
                    _Vault.NoofCassettes = vault.NoofCassettes;
                    _Vault.NoofCoinHopper = vault.NoofCoinHopper;
                    _Vault.IsWebServiceEnabled = vault.IsWebServiceEnabled;
                    _Vault.PurchasePrice = vault.PurchasePrice;
                    _Vault.PurchaseInvoice = vault.PurchaseInvoice;
                    _Vault.PurchaseDate = vault.PurchaseDate;
                    _Vault.depreciationDate = vault.depreciationDate;
                    _Vault.SoldPrice = vault.SoldPrice;
                    _Vault.SoldInvoice = vault.SoldInvoice;
                    _Vault.SoldDate = vault.SoldDate;
                    _Vault.Description = vault.Description;
                    _Vault.IsAssigned = vault.IsAssigned;
                    _Vault.IsPurchased = vault.IsPurchased;
                    _Vault.IsConfigured = vault.IsConfigured;
                    _Vault.StandaradFillAmount = vault.StandaradFillAmount;
                    _Vault.IsSiteUpdated = vault.IsSiteUpdated;
                    _Vault.FillRejection = vault.FillRejection;
                    _Vault.AutoAdjustEnabled = vault.AutoAdjustEnabled;
                }
                return _Vault;
            }

        }

        public List<Vault_GetCassetteDetails> GetCassetteDetails(int Vault_ID, int CassetteType)
        {
            using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
            {
                List<Vault_GetCassetteDetails> _Cassette = new List<Vault_GetCassetteDetails>();
                List<rsp_Vault_GetCassetteDetailsResult> lstCassettes = DataContext.Vault_GetCassetteDetails(Vault_ID, CassetteType).ToList();
                foreach (rsp_Vault_GetCassetteDetailsResult Cassette in lstCassettes)
                {
                    _Cassette.Add(new Vault_GetCassetteDetails()
                    {
                        AlertLevel = Cassette.AlertLevel,
                        Cassette_ID = Cassette.Cassette_ID,
                        Cassette_Name = Cassette.Cassette_Name,
                        Denom = Cassette.Denom,
                        DESCRIPTION = Cassette.DESCRIPTION,
                        IsActive = Cassette.IsActive,
                        MaxFillAmount = Cassette.MaxFillAmount,
                        MinFillAmount = Cassette.MinFillAmount,
                        StandardFillAmount = Cassette.StandardFillAmount,
                        TYPE = Cassette.TYPE,
                    });

                }
                return _Cassette;
            }
        }

        #endregion

        #region Update Methods

        //Update Vault Details
        public int UpdateDevice(int Vault_ID, String Name, String Serial_No, bool Active, int SiteID, int AlertLevel, Int32 User_ID, int AuditModule, string AuditModuleName, string ScreenName, int Manufacturer_ID, string Type_Prefix, decimal Capacity, int NoofCoinHopper, int NoofCassettes, bool IsWebServiceEnabled, string NGAtype, string Description, decimal StandardFillAmount, bool IsAutoAdjust, bool IsRejectionFill)
        {
            using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
            {
                List<usp_Vault_UpdateDeviceResult> lstDevices = DataContext.Vault_UpdateDevice(Vault_ID, Name, Serial_No, Active, SiteID, AlertLevel, User_ID, AuditModule, AuditModuleName, ScreenName, Manufacturer_ID, Type_Prefix, Capacity, NoofCoinHopper, NoofCassettes, IsWebServiceEnabled, NGAtype, Description, StandardFillAmount,  IsAutoAdjust,  IsRejectionFill).ToList();
                return lstDevices.Single<usp_Vault_UpdateDeviceResult>().Vault_ID.Value;
            }
        }

        //Update Vault Financial Details

        public int Vault_UpdateFinanceDetails(System.Nullable<int> VaultID, System.Nullable<decimal> PurchasePrice, string PurchaseInvoiceNo, System.Nullable<System.DateTime> PurchaseDate, System.Nullable<System.DateTime> DepreciationDate, System.Nullable<decimal> SoldPrice, string SoldInvoiceNo, System.Nullable<System.DateTime> SoldDate, System.Nullable<int> UserID, System.Nullable<int> ModuleID, string ModuleName, string ScreenName)
        {
            using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
            {
                List<usp_Vault_UpdateFinanceDetailsResult> lstDetails = DataContext.Vault_UpdateFinanceDetails(VaultID, PurchasePrice, PurchaseInvoiceNo, PurchaseDate, DepreciationDate, SoldPrice, SoldInvoiceNo, SoldDate, UserID, ModuleID, ModuleName, ScreenName).ToList();
                return lstDetails.Single<usp_Vault_UpdateFinanceDetailsResult>().Vault_ID.Value;
            }
        }

        //Update Vault Cassette Details
        public int Vault_UpdateCassetteDetails(int Cassette_ID, int Vault_ID, string Cassette_Name, int Type, float Denom, bool IsActive, int AlertLevel, decimal StandardFillAmount, decimal MaxFillAmount,string Description, int User_ID, int Module_ID, string Module_Name, string Screen_Name)
        {
            using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
            {
                int Result = DataContext.Vault_UpdateCassetteDetails(Cassette_ID, Vault_ID, Cassette_Name, Type, Denom, IsActive, AlertLevel, StandardFillAmount, MaxFillAmount, Description, User_ID, Module_ID, Module_Name, Screen_Name);
                return Result;
            }
        }
        #endregion

        public int Vault_Termination(int VaultID, int UserId, int ModuleId, string ModuleName, string ScreenName, string Description)
        {
            using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
            {
                int result = DataContext.Vault_TerminateDevice(VaultID, UserId, ModuleId, ModuleName, ScreenName, Description);
                return result;
            }
        }

        public int UpdateVaultCopy(string Name, string SerialNo, int UserID, int ModuleID, string ModuleName, string ScreenName, int SrcVaultID)
        {
            using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
            {
                int result = DataContext.Vault_CopyDevice(Name, SerialNo, UserID, ModuleID, ModuleName, ScreenName, SrcVaultID);
                return result;
            }

        }

        #region Supporting Methods

        public string GetCurrentCurrenyCulture()
        {
            using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
            {
                string sCurrentCurrenyCulture = string.Empty;
                DataContext.GetSetting(Convert.ToInt32("0"), "BMC_Reports_Language", "en-US", ref sCurrentCurrenyCulture);
                if (sCurrentCurrenyCulture.ToUpper() == "ES-AR")
                {
                    return "it-IT";
                }
                else
                {
                    return "en-US";
                }
            }
        }

        #endregion

        #region Assign To site

        public AssignToSiteData GetDataForAssigningToSite(int User_ID)
        {
             AssignToSiteData objAssignToSiteData = new AssignToSiteData();
             using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
             {
                 IMultipleResults dsDevices = DataContext.rsp_Vault_GetallUnassignedDevices(User_ID);
                 List<rsp_Vault_GetallUnassignedDevices> lstdevices= dsDevices.GetResult<rsp_Vault_GetallUnassignedDevices>().ToList();
                 List<rsp_Vault_GetallUnassignedSites> lstSites = dsDevices.GetResult<rsp_Vault_GetallUnassignedSites>().ToList();

                 objAssignToSiteData.Devices = (from item in lstdevices
                                             select new UnassignedDevice()
                                             {
                                                 Name=item.Name,
                                                 NGADevice_ID = item.NGADevice_ID
                                             } ).ToList();
                 objAssignToSiteData.Sites = (from item in lstSites
                                                select new UnassignedSite()
                                                {
                                                  Site_ID=item.Site_ID,
                                                  Site_Code = item.Site_Code +" -" + item.Site_Name,
                                                  Site_Name = item.Site_Name
                                                }).ToList();
             }

             return objAssignToSiteData;
        }

        public int AssignToSite(XElement xml, int user_ID,int module_ID, string module_Name,  string screen_Name)
        {
            using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
            {
                return DataContext.AssignToSite(xml, user_ID, module_ID, module_Name, screen_Name);
            }
        }
        #endregion

    }
}
