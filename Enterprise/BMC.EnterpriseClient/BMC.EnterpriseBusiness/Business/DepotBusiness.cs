using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.EnterpriseBusiness.Entities;
using BMC.EnterpriseDataAccess;
using BMC.Common.LogManagement;
using System.Xml.Linq;
using BMC.Common.ExceptionManagement;
using Audit.Transport;
using Audit.BusinessClasses;

namespace BMC.EnterpriseBusiness.Business
{
    public class DepotBusiness
    {
        //singleton object
        private static DepotBusiness _depot;
        public DepotBusiness() { }
        public static DepotBusiness CreateInstance()
        {
            if (_depot == null)
                _depot = new DepotBusiness();

            return _depot;
        }

        #region Data Loading Methods
        //load operator
        public List<OperatorEntity> Depot_LoadOperator()
        {
            try
            {
                LogManager.WriteLog("Inside DepotBusiness-Depot_LoadOperator", LogManager.enumLogLevel.Info);
                List<OperatorEntity> obcoll = new List<OperatorEntity>();
                List<rsp_GetDepotOperatorResult> OperatorList;
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    OperatorList = DataContext.GetDepotOperator().ToList();
                }
                obcoll = (from obj in OperatorList
                          select new OperatorEntity
                          {
                              Operator_ID = obj.Operator_ID,
                              Operator_Name = obj.Operator_Name
                          }).ToList<OperatorEntity>();
                return obcoll;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                throw ex;
            }

        }

        //load depot based on operator
        public List<DepotEntity> Depot_LoadDepot(int iOperatorId)
        {
            try
            {
                LogManager.WriteLog("Inside DepotBusiness-Depot_LoadDepot", LogManager.enumLogLevel.Info);
                List<DepotEntity> obcoll = null;
                List<rsp_GetDepotResult> DepList;
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    DepList = DataContext.GetDepot(iOperatorId).ToList<rsp_GetDepotResult>();
                }

                obcoll = (from obj in DepList
                          select new DepotEntity
                          {
                              Depot_ID = obj.Depot_ID,
                              Depot_Name = obj.Depot_Name

                          }).ToList<DepotEntity>();
                return obcoll;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Exception occured in DepotBusiness-Depot_LoadOperator,exception" + ex, LogManager.enumLogLevel.Error);
                throw ex;
            }

        }

        //load depot detials based on operator and depot
        public List<DepotEntity> Depot_LoadDepotDetails(int iDepotId, int iOperatorId)
        {
            try
            {
                LogManager.WriteLog("Inside DepotBusiness-Depot_LoadDepotDetails", LogManager.enumLogLevel.Info);
                List<DepotEntity> obcoll = new List<DepotEntity>();
                List<rsp_GetDepotDetailsResult> DepList;
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    DepList = DataContext.GetDepotDetails(iDepotId, iOperatorId).ToList();
                }

                obcoll = (from obj in DepList
                          select new DepotEntity
                          {

                              Depot_ID = obj.Depot_ID,
                              Depot_Name = obj.Depot_Name,
                              Depot_Cadastral_Code = obj.Depot_Cadastral_Code,
                              Depot_Address = obj.Depot_Address,
                              Depot_Postcode = obj.Depot_Postcode,
                              Depot_Area = obj.Depot_Area,
                              Depot_Street_Number = obj.Depot_Street_Number,
                              Depot_Phone_Number = obj.Depot_Phone_Number,
                              Depot_Province = obj.Depot_Province,
                              Depot_Contact_Name = obj.Depot_Contact_Name,
                              Depot_Municipality = obj.Depot_Municipality,
                              Depot_Reference = obj.Depot_Reference,
                              Depot_Service = obj.Depot_Service,
                              Depot_Status = obj.Depot_Status,
                              Supplier_ID = obj.Supplier_ID
                          }).ToList<DepotEntity>();
                return obcoll;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Exception occured in DepotBusiness-Depot_LoadDepotDetails,exception" + ex, LogManager.enumLogLevel.Error);
                throw ex;
            }

        }

        //load service area based on depot details
        public List<ServiceAreaEntity> Depot_LoadServiceArea(int depotid)
        {
            try
            {
                LogManager.WriteLog("Inside DepotBusiness-Depot_loadserviceArea", LogManager.enumLogLevel.Info);
                List<ServiceAreaEntity> obcoll = null;
                List<rsp_GetDepotServiceAreaResult> ServiceAreaList;
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    ServiceAreaList = DataContext.GetDepotServiceArea(depotid).ToList<rsp_GetDepotServiceAreaResult>();
                }
                obcoll = (from obj in ServiceAreaList
                          select new ServiceAreaEntity
                          {
                              Service_Area_Name = obj.Service_Area_Name,
                              Service_Area_ID = obj.Service_Area_ID
                          }).ToList<ServiceAreaEntity>();
                return obcoll;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Exception occured in DepotBusiness-Depot_loadserviceArea,exception" + ex, LogManager.enumLogLevel.Error);
                throw ex;
            }
        }

        //load service area details based on depot ID and service area ID
        public List<ServiceAreaEntity> Depot_LoadServiceAreadetails(int iDepotId, int ServiceArea_ID)
        {
            try
            {
                LogManager.WriteLog("Inside DepotBusiness-Depot_LoadServiceAreadetails", LogManager.enumLogLevel.Info);
                List<ServiceAreaEntity> obcoll = new List<ServiceAreaEntity>();
                List<rsp_GetDepotServiceAreaDetailsResult> areaList;
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    areaList = DataContext.GetDepotServiceAreaDetails(iDepotId, ServiceArea_ID).ToList<rsp_GetDepotServiceAreaDetailsResult>();
                }
                obcoll = (from obj in areaList
                          select new ServiceAreaEntity
                          {
                              Depot_ID = obj.Depot_ID,
                              Service_Area_Name = obj.Service_Area_Name,
                              Service_Area_Description = obj.Service_Area_Description,
                              Service_Area_ID = obj.Service_Area_ID,
                              Service_Area_Notes = obj.Service_Area_Notes
                          }).ToList<ServiceAreaEntity>();
                return obcoll;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Exception occured in DepotBusiness-Depot_LoadServiceAreadetails,exception" + ex, LogManager.enumLogLevel.Error);
                throw ex;
            }
        }

        //Load Site Representative details based on depot ID
        public List<SiteRepresentativeEntity> Depot_LoadSiteRep(int idepotid)
        {
            try
            {
                LogManager.WriteLog("Inside DepotBusiness-Depot_loadSiteRep", LogManager.enumLogLevel.Info);
                List<SiteRepresentativeEntity> obcoll = new List<SiteRepresentativeEntity>();
                List<rsp_GetDepotSiteRepResult> siteRep;
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    siteRep = DataContext.GetDepotSiteRep(idepotid).ToList<rsp_GetDepotSiteRepResult>();
                }
                obcoll = (from obj in siteRep
                          select new SiteRepresentativeEntity
                          {
                              Staff_ID = obj.Staff_ID,
                              UserName = obj.Name,
                              Depot_ID = obj.Depot_ID
                          }).ToList<SiteRepresentativeEntity>();
                return obcoll;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Exception occured in DepotBusiness-Depot_loadSiteRep,exception" + ex, LogManager.enumLogLevel.Error);
                throw ex;
            }

        }

        #endregion Data Loading Methods

        #region Update Methods

        //update depot details
        public int DepotUpdate(string depot_name, string depot_address, string depot_postcode, int depot_supplierid, string depot_Cadastralcode, 
                                string depot_area, string depot_streetnumber, string depot_phonenumber, string depot_province, 
                                string depot_contactname, string depot_municipality, string depot_refererence, bool depot_Service, 
                                int DepotID,string ServiceAreaName,string ServiceAreaDescription,string ServiceAreaNotes,int ServiceAreaID,
                                string StaffId)
        {
            try
            {
                LogManager.WriteLog("Inside DepotBusiness-DepotUpdate", LogManager.enumLogLevel.Info);
                EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext();
                 int result = DataContext.UpdateDepotDetails(depot_name, depot_address, depot_postcode, depot_contactname, depot_supplierid, depot_refererence, depot_Service, depot_phonenumber, depot_streetnumber, depot_province, depot_municipality, depot_Cadastralcode, string.IsNullOrEmpty(depot_area) ? 0 : Convert.ToInt32(depot_area), DepotID, ServiceAreaName, ServiceAreaDescription, ServiceAreaNotes, ServiceAreaID, StaffId);
                LogManager.WriteLog("End of DepotBusiness-Depot_loadSiteRep", LogManager.enumLogLevel.Info);
                return result;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return -1;
            }
        }

        //Check Whether the New Depot Exists
        public bool IsDepotExists(string DepotName, int DepotId, int SupplierId)
        {
            try
            {
                EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext();
                return (bool)DataContext.IsDepotExists(DepotName, DepotId, SupplierId);
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Exception occured in DepotBusiness-IsDepotExists,exception" + ex, LogManager.enumLogLevel.Error);
                throw ex;
            }
        }

        //Check Whether the New Service Area Exists
        public bool IsServiceAreaExists(string ServiceArea,int ServiceAreaID,int DepotID)
        {
            try
            {
                LogManager.WriteLog("Inside bServiceAreaAlreadyExists", LogManager.enumLogLevel.Info);
                EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext();
                return (bool)DataContext.IsDepotServiceAreaExists(ServiceArea, ServiceAreaID, DepotID);
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Exception occured in DepotBusiness-bServiceAreaAlreadyExists,exception" + ex, LogManager.enumLogLevel.Error);
                throw ex;
            }
        }

        #endregion Update Methods

        #region Auditing Depot

        public void InsertNewAuditEntry(ModuleNameEnterprise moduleName, string strScreenName, string strField, string strNewItem, int iUserId, string strUsername)
        {
            try
            {
                //Calling Audit Method
                Audit_History AH = new Audit_History();
                //Populate required Values            
                AH.EnterpriseModuleName = moduleName;
                AH.Audit_Screen_Name = strScreenName;
                AH.Audit_Desc = "Record [" + strNewItem + "] added to " + strScreenName;
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

        public void AuditModifiedDataForDepot(string sDepotName, string sField, string sPrevValue, string sNewValue, int iUserId, string strUsername)
        {
            try
            {
                Audit_History AH = new Audit_History();
                AH.EnterpriseModuleName = ModuleNameEnterprise.Depot;
                AH.Audit_Screen_Name = "Depot";
                AH.Audit_Desc = "Depot " + sDepotName + " modified  ..[" + sField + "] : " + sPrevValue + " -> " + sNewValue;
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
    }
}
