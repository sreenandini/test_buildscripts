using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.EnterpriseDataAccess;
using System.Data.Linq;
using System.Collections;
using BMC.EnterpriseBusiness.Entities;
using System.Collections.ObjectModel;
using BMC.Common.LogManagement;
using System.Reflection;
using BMC.Common.ExceptionManagement;
using Audit.Transport;
using System.Xml.Linq;
using System.Threading;

namespace BMC.EnterpriseBusiness.Business
{
    public class SiteDetails
    {
        //EnterpriseDataContext edc = new EnterpriseDataContext();
     //   public void LoadDetailsData()
       // {
            //ISingleResult<GetSiteDetails> result = edc.GetSiteDetailsonLoad(1); 
            //ISingleResult<rsp_GetDetailFieldsonLoadResult> result = edc.GetDetailFieldsonLoad(1); 
   //     }

       
        public AdminSiteEntity GetDetailFields(int SiteID)
        {
            AdminSiteEntity obcoll = null;
            try
            {
                List<rsp_GetDetailFieldsonLoadResult> DetailList;
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    DetailList = DataContext.GetDetailFields(SiteID).ToList();
                }
                obcoll = (from obj in DetailList
                          select new AdminSiteEntity
                          {
                              Site_ID = obj.Site_ID,                           
                              Service_Supplier_ID = obj.Service_Supplier_ID,
                              Service_Area_ID = obj.Service_Area_ID,
                              Service_Depot_ID = obj.Service_Depot_ID,
                              Site_Name = obj.Site_Name,
                              Site_Code = obj.Site_Code,
                              Site_Supplier_Code = obj.Site_Supplier_Code,
                              Site_Company_Code = obj.Site_Company_Code,
                              Site_Phone_No = obj.Site_Phone_No,
                              Site_Fax_No = obj.Site_Fax_No,
                              Site_Email_Address = obj.Site_Email_Address,
                              Site_Address_1 = obj.Site_Address_1,
                              Site_Address_2 = obj.Site_Address_2,
                              Site_Address_3 = obj.Site_Address_3,
                              Site_Address_4 = obj.Site_Address_4,
                              Site_Address_5 = obj.Site_Address_5,
                              Site_Postcode = obj.Site_Postcode,
                              Site_Manager = obj.Site_Manager,
                              Site_Price_Per_Play_Default = obj.Site_Price_Per_Play_Default,
                              Site_Price_Per_Play = obj.Site_Price_Per_Play,
                              Site_Jackpot_Default = obj.Site_Jackpot_Default,
                              Site_Jackpot = obj.Site_Jackpot,
                              Site_Percentage_Payout_Default = obj.Site_Percentage_Payout_Default,
                              Site_Percentage_Payout = obj.Site_Percentage_Payout,
                              Sub_Company_ID = obj.Sub_Company_ID,
                              Company_ID = obj.Company_ID,
                              Terms_Group_ID_Default = obj.Terms_Group_ID_Default,
                              Terms_Group_ID = obj.Terms_Group_ID,
                              Access_Key_ID_Default = obj.Access_Key_ID_Default,
                              Access_Key_ID = obj.Access_Key_ID,
                              Staff_ID_Default = obj.Staff_ID_Default,
                              Staff_ID = obj.Staff_ID,
                              Site_Invoice_Name = obj.Site_Invoice_Name,
                              Site_Invoice_Address = obj.Site_Invoice_Address,
                              Site_Invoice_Postcode = obj.Site_Invoice_Postcode,
                              Sub_Company_Region_ID = obj.Sub_Company_Region_ID,
                              Sub_Company_Area_ID = obj.Sub_Company_Area_ID,
                              Sub_Company_District_ID = obj.Sub_Company_District_ID,
                              Site_Image_Reference = obj.Site_Image_Reference,
                              Site_Image_Reference_2 = obj.Site_Image_Reference_2,
                              Site_Closed = obj.Site_Closed,
                              Depot_ID = obj.Depot_ID,
                              Supplier_ID = obj.Supplier_ID,
                              Site_Classification_ID = obj.Site_Classification_ID,
                              Site_Grade = obj.Site_Grade,
                              Sage_Account_Ref = obj.Sage_Account_Ref,
                              Site_Is_FreeFloat = obj.Site_Is_FreeFloat,
                              Site_Local_Inbox = obj.Site_Local_Inbox,
                              Site_Local_Outbox = obj.Site_Local_Outbox,
                              Site_Memo = obj.Site_Memo,
                              Site_Reference = obj.Site_Reference,
                              Site_Trade_Type = obj.Site_Trade_Type,
                              Site_Non_Trading_Period_From = obj.Site_Non_Trading_Period_From,
                              Site_Non_Trading_Period_To = obj.Site_Non_Trading_Period_To,
                              Site_Supplier_Service_Area = obj.Site_Supplier_Service_Area,
                              Site_Supplier_Area = obj.Site_Supplier_Area,
                              Standard_Opening_Hours_ID = obj.Standard_Opening_Hours_ID,
                              Next_Sub_Company_ID = obj.Next_Sub_Company_ID,
                              Next_Sub_Company_Change_Date = obj.Next_Sub_Company_Change_Date,
                              Site_Previous_Sub_Company_ID = obj.Site_Previous_Sub_Company_ID,
                              Previous_Sub_Company_Change_Date = obj.Previous_Sub_Company_Change_Date,
                              Site_Honeyframe_EDI = obj.Site_Honeyframe_EDI,
                              Site_Datapak_Protocol = obj.Site_Datapak_Protocol,
                              Site_Start_Date = obj.Site_Start_Date,
                              Site_End_Date = obj.Site_End_Date,
                              Site_Licence_Number = obj.Site_Licence_Number,
                              Site_Fiscal_Code = obj.Site_Fiscal_Code, 
                              Site_Street_Number = obj.Site_Street_Number,
                              Site_Province = obj.Site_Province,
                              Site_Municipality = obj.Site_Municipality,
                              Site_Cadastral_Code = obj.Site_Cadastral_Code,
                              Site_Area = obj.Site_Area,
                              Site_Location_Type = obj.Site_Location_Type,
                              Site_Toponym = obj.Site_Toponym,
                              Site_Licensee_Commenced_Date = obj.Site_Licensee_Commenced_Date,
                              Site_Licensee_Agreement_End_Date = obj.Site_Licensee_Agreement_End_Date,
                              Site_Licensee_Agreement_Type = obj.Site_Licensee_Agreement_Type,
                              Site_Application = obj.Site_Application, 
                              Region = obj.Region,
                              WebURL = obj.WebURL,
                              Site_Status_ID = obj.Site_Status_ID,
                              Site_Inactive_Date = obj.Site_Inactive_Date,
                              Site_Connection_IPAddress = obj.Site_Connection_IPAddress,
                              Site_MaxNumber_VLT = obj.Site_MaxNumber_VLT,
                              Site_ZonaRice = obj.Site_ZonaRice,
                              StackerLimitPercentage = obj.StackerLimitPercentage,
                              Site_Closed_Date = obj.Site_Closed_Date

                          }).Single<AdminSiteEntity>();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return obcoll;
        }

        public AdminSiteEntity GetSiteStatus(int SiteID)
        {
            AdminSiteEntity objcoll = null;
            try
            {
                List<rsp_GetSiteStatusResult> StatusList;
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    StatusList = DataContext.GetSiteStatus(SiteID).ToList();
                }
                objcoll = (from obj in StatusList
                           select new AdminSiteEntity
                           {
                               Site_Enabled = obj.Site_Enabled,
                               IsTITOEnabled = obj.IsTITOEnabled,
                               IsCrossTicketingEnabled = obj.IsCrossTicketingEnabled,
                               IsNonCashVoucherEnabled = obj.IsNonCashVoucherEnabled
                           }).Single<AdminSiteEntity>();                
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return objcoll;

        }
               
        public int InsertExportHistoryCrossTicketing(string Site_Code)
        {
            int result = 0;
            try
            {
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    result = DataContext.InsertExportHistoryCrossTicketing(Site_Code);                    
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return result;           
        }

        public AdminSiteEntity CheckSiteTicketingURL(string Site_Code)
        {
            AdminSiteEntity objcoll = null;
            try
            {
                List<usp_CheckSiteTicketingURLResult> URLList;
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    URLList = DataContext.CheckSiteTicketingURL(Site_Code).ToList();
                }
                objcoll = (from obj in URLList
                           select new AdminSiteEntity
                           {
                               TicketingURL = obj.TicketingURL
                           }).Single<AdminSiteEntity>();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return objcoll;
        }

        public AdminSiteEntity GetActiveInstallationsForSite(int SiteId)
        {
            AdminSiteEntity objcoll = null;
            try
            {
                List<rsp_GetActiveInstallationsForSiteResult> InstallList;
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    InstallList = DataContext.GetActiveInstallationsForSite(SiteId).ToList();
                }
                objcoll = (from obj in InstallList
                           select new AdminSiteEntity
                           {
                               Bar_Position_ID = obj.Bar_Position_ID

                           }).FirstOrDefault<AdminSiteEntity>();
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
            return objcoll;
        }

        public AdminSiteEntity CheckSite(string Site_Code, int SiteID)
        {
            AdminSiteEntity objcoll = null;
            try
            {
                List<rsp_CheckSiteResult> SiteList;
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    SiteList = DataContext.CheckSite(Site_Code,SiteID).ToList();
                }
                
                if (SiteList == null || SiteList.Count == 0) 
                    return null;

                objcoll = (from obj in SiteList
                           select new AdminSiteEntity
                           {
                               Site_ID = obj.Site_ID
                           }).Single<AdminSiteEntity>();
            }
            catch(Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
            return objcoll; 
        }

        public List<AdminSiteEntity> GetOperatorInfo()
        {
            List<AdminSiteEntity> objcol = null;
            try
            {
                List<rsp_GetOperatorInfoResult> OpInfo;
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    OpInfo = DataContext.GetOperatorInfo().ToList();
                }
                objcol = (from obj in OpInfo
                          select new AdminSiteEntity
                          {                              
                              Operator_ID = obj.Operator_ID,
                              Operator_Name = obj.Operator_Name
                          }).ToList<AdminSiteEntity>();
                
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
            return objcol;
        }

        public List<AdminSiteEntity> GetServiceOperator()
        {
            List<AdminSiteEntity> objcol = null;
            try
            {
                List<rsp_ServiceOperatorResult> ServiceOp;
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    ServiceOp = DataContext.ServiceOperator().ToList();
                }
                objcol = (from obj in ServiceOp
                          select new AdminSiteEntity
                          {
                              Operator_ID = obj.Operator_ID,
                              Operator_Name = obj.Operator_Name
                          }).ToList<AdminSiteEntity>();
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
            return objcol;
        }

        public List<AdminSiteEntity> GetDepot(int OperatorID)
        {
            List<AdminSiteEntity> objcol = null;
            try
            {
                List<rsp_GetDepotInfoResult> DepInfo;
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    DepInfo = DataContext.GetDepotInfo(OperatorID).ToList();
                }
                objcol = (from obj in DepInfo
                          select new AdminSiteEntity
                          {
                              Depot_ID = obj.Depot_ID,
                              Depot_Name = obj.Depot_Name
                          }).ToList<AdminSiteEntity>();
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
            return objcol;
        }

        public List<AdminSiteEntity> GetServiceDepot(int OperatorID)
        {
            List<AdminSiteEntity> objcol = null;
            try
            {
                List<rsp_GetDepotInfoResult> DepInfo;
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    DepInfo = DataContext.GetDepotInfoForservice(OperatorID).ToList();
                }
                objcol = (from obj in DepInfo
                          select new AdminSiteEntity
                          {
                              Depot_ID = obj.Depot_ID,
                              Depot_Name = obj.Depot_Name
                          }).ToList<AdminSiteEntity>();
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
            return objcol;
        }


        public List<AdminSiteEntity> GetServiceArea_Depot(int DepotID)
        {
            List<AdminSiteEntity> objcol = null;
            try
            {
                List<rsp_GetServiceAreaonDepotResult> SvcArea;
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    SvcArea = DataContext.GetServiceAreaonDepot(DepotID).ToList();
                }
                objcol = (from obj in SvcArea
                          select new AdminSiteEntity
                          {
                              Service_Area_ID = obj.Service_Area_ID,
                              Service_Area_Name = obj.Service_Area_Name
                          }).ToList<AdminSiteEntity>();
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
            return objcol;
        }

        public List<AdminSiteEntity> GetClassificationinfo()
        {
            List<AdminSiteEntity> objcol = null;
            try
            {
                List<rsp_GetSiteClassificationResult> SiteClassif;
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    SiteClassif = DataContext.GetSiteClassification().ToList();
                }
                objcol = (from obj in SiteClassif
                          select new AdminSiteEntity
                          {
                              Site_Classification_ID = obj.Site_Classification_ID,
                              Site_Classification_Name = obj.Site_Classification_Name
                          }).ToList<AdminSiteEntity>();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }            
            return objcol;
        }

        public List<AdminSiteEntity> GetSiteClassificationName(int SiteClassifID)
        {
            List<AdminSiteEntity> objcol = null;
            try
            {
                List<rsp_GetSiteClassifNameonIDResult> SiteClassifName;
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    SiteClassifName = DataContext.GetSiteClassifNameonID(SiteClassifID).ToList();
                }
                objcol = (from obj in SiteClassifName
                          select new AdminSiteEntity
                          {
                              Site_Classification_Name = obj.Site_Classification_Name
                          }).ToList<AdminSiteEntity>();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return objcol;
        }

        public List<AdminSiteEntity> GetStdOpeningHours()
        {
            List<AdminSiteEntity> objcol = null;
            try
            {
                List<rsp_GetStdOpeningHrsResult> StdOpnHrs;
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    StdOpnHrs = DataContext.GetStdOpeningHrs().ToList();
                }
                objcol = (from obj in StdOpnHrs
                          select new AdminSiteEntity
                          {
                              Standard_Opening_Hours_ID = obj.Standard_Opening_Hours_ID,
                              Standard_Opening_Hours_Description = obj.Standard_Opening_Hours_Description
                          }).ToList<AdminSiteEntity>();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return objcol;
        }

        public void InsertUpdateSiteClassification(AdminSiteClassEntity objAdminSiteClass)
        {
            try
            {
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    int Status;
                    int? SiteClassifIDOut = 0;
                    Status = DataContext.InsertUpdateSiteClassification(objAdminSiteClass.SiteClassID, objAdminSiteClass.SiteClassName, ref SiteClassifIDOut);
                    objAdminSiteClass.SiteClassID = SiteClassifIDOut.Value;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }       

        public int DeleteSiteClassifonSite(int SiteClassifID)
        {       
            int? SiteClassifStatusOUT = 0;
            try
            {
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {                    
                    
                    DataContext.RemoveSiteClassifonSiteStatus(SiteClassifID, ref SiteClassifStatusOUT);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return Convert.ToInt32(SiteClassifStatusOUT);
        }

        public void InsertNTPonSite(int SiteID, string NTPFrom, string NTPTo)
        {
            try
            {
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    DataContext.InsertNTPonSite(SiteID, NTPFrom, NTPTo);                    
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        public void NTPZeroCollection(int SiteID, string NTPFrom, string NTPTo, int CollDays, string CollTime , string PrevCollTime, char CollType, string Remarks)
        {
            try
            {
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    DataContext.NTPZeroCollection(SiteID, NTPFrom, NTPTo, CollDays, CollTime, PrevCollTime, CollType, Remarks);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        public SiteOpeningHours OpeningHours(int SiteID)
        {
            SiteOpeningHours objcol = null;
            try
            {
                List<rsp_GetSiteOpeningHoursResult> SiteOpnHrs;

                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    SiteOpnHrs = DataContext.GetSiteOpeningHours(SiteID).ToList();
                }
                objcol = (from obj in SiteOpnHrs
                          select new SiteOpeningHours
                          {
                              Site_Name = obj.Site_Name,
                              Site_Open_Monday = obj.Site_Open_Monday,
                              Site_Open_Tuesday = obj.Site_Open_Tuesday,
                              Site_Open_Wednesday = obj.Site_Open_Wednesday,
                              Site_Open_Thursday = obj.Site_Open_Thursday,
                              Site_Open_Friday = obj.Site_Open_Friday,
                              Site_Open_Saturday = obj.Site_Open_Saturday,
                              Site_Open_Sunday = obj.Site_Open_Sunday
                          }).Single< SiteOpeningHours>();               
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return objcol;
        }

        public GetZoneOpeningHoursResult ZoneOpenHours(int ZoneID)
        {
            GetZoneOpeningHoursResult objcol = null;
            try
            {
                List<rsp_GetZoneOpeningHoursResult> ZoneOpnHrs;

                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    ZoneOpnHrs = DataContext.GetZoneOpeningHours(ZoneID).ToList();
                }
                objcol = (from obj in ZoneOpnHrs
                          select new GetZoneOpeningHoursResult
                          {
                              Zone_Name = obj.Zone_Name,
                              Zone_Open_Monday = obj.Zone_Open_Monday,
                              Zone_Open_Tuesday = obj.Zone_Open_Tuesday,
                              Zone_Open_Wednesday = obj.Zone_Open_Wednesday,
                              Zone_Open_Thursday = obj.Zone_Open_Thursday,
                              Zone_Open_Friday = obj.Zone_Open_Friday,
                              Zone_Open_Saturday = obj.Zone_Open_Saturday,
                              Zone_Open_Sunday = obj.Zone_Open_Sunday
                          }).Single<GetZoneOpeningHoursResult>();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return objcol;
        }

        public bool CheckandUpdateSiteOpeningHours(int SiteID, SiteOpeningHours OpenHrs)
        {
            bool SiteStatus = false;
            try
            {
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {                    
                    int? SiteDetailStatusOUT = 0;
                    DataContext.UpdateSiteOpeningHours(SiteID, OpenHrs.Site_Open_Monday, OpenHrs.Site_Open_Tuesday, OpenHrs.Site_Open_Wednesday, 
                                                        OpenHrs.Site_Open_Thursday, OpenHrs.Site_Open_Friday, OpenHrs.Site_Open_Saturday, 
                                                        OpenHrs.Site_Open_Sunday, ref SiteDetailStatusOUT);
                    if (SiteDetailStatusOUT == 1)
                    {
                        SiteStatus = true;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return SiteStatus;
        }

        public bool CheckandUpdateZoneOpeningHours(int ZoneID, GetZoneOpeningHoursResult ZnOpenHrs)
        {
            bool UpdateZoneStatus = false;
            try
            {
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    int? ZoneStatusOut = 0;
                    DataContext.UpdateZoneOpeningHoursonZone(ZoneID, ZnOpenHrs.Zone_Open_Monday, ZnOpenHrs.Zone_Open_Tuesday, ZnOpenHrs.Zone_Open_Wednesday,
                                                                ZnOpenHrs.Zone_Open_Thursday, ZnOpenHrs.Zone_Open_Friday, ZnOpenHrs.Zone_Open_Saturday,
                                                                ZnOpenHrs.Zone_Open_Sunday, ref ZoneStatusOut);
                    if (ZoneStatusOut == 1)
                    {
                        UpdateZoneStatus = true;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return UpdateZoneStatus;
        }

        public bool CheckandUpdateZoneOpeningSiteTime(int ZoneID)
        {
            bool UpdateZoneStatus = false;
            try
            {
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    int? ZoneStatusOut = 0;
                    DataContext.UpdateZoneOpeningHoursSiteTime(ZoneID, ref ZoneStatusOut);
                    if (ZoneStatusOut == 1)
                    {
                        UpdateZoneStatus = true;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return UpdateZoneStatus;
        }



        public long CheckInsertStdOpeningHrsDesc(string StdHrDesc)
        {      
            int? StdOpenHRIDOUT = 0;
            try
            {              
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {                    
                    DataContext.InsertStdOpeningHrsDesc(StdHrDesc, ref StdOpenHRIDOUT);
                }                
            }
            catch(Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return Convert.ToInt64(StdOpenHRIDOUT);
        }

        public bool CheckZoneinSite(int SiteID)
        {
            bool ZoneStatus = false;
            try
            {
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {                    
                    int? ZoneStatusOUT = 0;
                    DataContext.CheckZoneonSite(SiteID, ref ZoneStatusOUT);
                    if (ZoneStatusOUT == 1)
                    {
                        ZoneStatus = true;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return ZoneStatus;
        }

        public void UpdateZoneOpeningHours(int SiteID, SiteOpeningHours OpenHrs)
        {
            try
            {
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    DataContext.UpdateZoneOpeningHours(SiteID, OpenHrs.Zone_Open_Monday, OpenHrs.Zone_Open_Tuesday, OpenHrs.Zone_Open_Wednesday, OpenHrs.Zone_Open_Thursday, OpenHrs.Zone_Open_Friday, OpenHrs.Zone_Open_Saturday, OpenHrs.Zone_Open_Sunday);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

        }

        public GetStdOpeningHrsDetailsResult GetStdOpeningHrsDetails(long StdHRID)
        {
            GetStdOpeningHrsDetailsResult objColl = null;
            try
            {
                List<rsp_GetStdOpeningHrsDetailsResult> StdOpenHRDet;

                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    StdOpenHRDet = DataContext.GetStdOpeningHrsDetails(Convert.ToInt32(StdHRID)).ToList();
                }
                objColl = (from obj in StdOpenHRDet
                           select new GetStdOpeningHrsDetailsResult
                           {
                               Standard_Opening_Hours_Description = obj.Standard_Opening_Hours_Description,
                               Standard_Opening_Hours_Open_Monday = obj.Standard_Opening_Hours_Open_Monday,
                               Standard_Opening_Hours_Open_Tuesday = obj.Standard_Opening_Hours_Open_Tuesday,
                               Standard_Opening_Hours_Open_Wednesday = obj.Standard_Opening_Hours_Open_Wednesday,
                               Standard_Opening_Hours_Open_Thursday = obj.Standard_Opening_Hours_Open_Thursday,
                               Standard_Opening_Hours_Open_Friday = obj.Standard_Opening_Hours_Open_Friday,
                               Standard_Opening_Hours_Open_Saturday = obj.Standard_Opening_Hours_Open_Saturday,
                               Standard_Opening_Hours_Open_Sunday = obj.Standard_Opening_Hours_Open_Sunday
                           }).Single<GetStdOpeningHrsDetailsResult>();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return objColl;
        }

        public bool CheckandUpdateStdOpeningHrs(int StdHrID, GetStdOpeningHrsDetailsResult StdOpenHRs)
        {
            bool UpdateStatus = false;
            try
            {
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    int? UpdateStatusOUT = 0;
                    DataContext.UpdateStdOpeningHours(StdHrID, StdOpenHRs.Standard_Opening_Hours_Open_Monday, StdOpenHRs.Standard_Opening_Hours_Open_Tuesday,
                                            StdOpenHRs.Standard_Opening_Hours_Open_Wednesday, StdOpenHRs.Standard_Opening_Hours_Open_Thursday,
                                            StdOpenHRs.Standard_Opening_Hours_Open_Friday, StdOpenHRs.Standard_Opening_Hours_Open_Saturday,
                                            StdOpenHRs.Standard_Opening_Hours_Open_Sunday, StdOpenHRs.Standard_Opening_Hours_Total, ref UpdateStatusOUT);

                    if (UpdateStatusOUT == 1)
                    {
                        UpdateStatus = true;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return UpdateStatus;
        }
       
        public List<AdminSiteEntity> GetStaffCustomerAccessOperator(int StaffID)
        {
            List<AdminSiteEntity> objcol = null;
            try
            {
                List<rsp_GetStaffCustomerAccessOperatorResult> OpInfo;
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    OpInfo = DataContext.GetStaffCustomerAccessOperator(StaffID).ToList();
                }
                objcol = (from obj in OpInfo
                          select new AdminSiteEntity
                          {
                              Operator_ID = obj.Operator_ID,
                              Operator_Name = obj.Operator_Name
                          }).ToList<AdminSiteEntity>();
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
            return objcol;        
        }

        public List<AdminSiteEntity> GetCustomerAccessDepot(int OperatorID, int StaffID)
        {
            List<AdminSiteEntity> objcol = null;
            try
            {
                List<rsp_GetCustomerAccessDepotonOperatorResult> DepInfo;
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    DepInfo = DataContext.GetCustomerAccessDepotonOperator(OperatorID, StaffID).ToList();
                }
                objcol = (from obj in DepInfo
                          select new AdminSiteEntity
                          {
                              Depot_ID = obj.Depot_ID,
                              Depot_Name = obj.Depot_Name
                          }).ToList<AdminSiteEntity>();
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
            return objcol;
        }
        public List<AdminSiteEntity> GetStaffCustomerAccessServiceOperator(int StaffID)
        {
            List<AdminSiteEntity> objcol = null;
            try
            {
                List<rsp_GetStaffCustomerAccessServiceOperatorResult> ServiceOp;
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    ServiceOp = DataContext.GetStaffCustomerAccessServiceOperator(StaffID).ToList();
                }
                objcol = (from obj in ServiceOp
                          select new AdminSiteEntity
                          {
                              Operator_ID = obj.Operator_ID,
                              Operator_Name = obj.Operator_Name
                          }).ToList<AdminSiteEntity>();
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
            return objcol;
        }
        /// <summary>
        public List<GetRepresentativeonSiteResult> GetRepresentativeonSite(int SiteId)
        {
            List<GetRepresentativeonSiteResult> objcolRep = new List<GetRepresentativeonSiteResult>();
            try
            {
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    foreach (var entity in DataContext.GetRepresentativeonSite(SiteId))
                    {
                        objcolRep.Add(new GetRepresentativeonSiteResult()
                        {        
                            Staff_ID = entity.Staff_ID,
                            Staff_First_Name = entity.Staff_First_Name,
                            Staff_Last_Name = entity.Staff_Last_Name,
                            Staff_Full_Name = entity.Staff_Last_Name + ", " + entity.Staff_First_Name
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            LogManager.WriteLog("End of Get DefaultTermsgroupDefaultsResult", LogManager.enumLogLevel.Info);
            return objcolRep;
        }
        /// </summary>
		/// Export Machine Info to the client
        /// Make the entry in Export History
        /// <param name="reference1"></param>
        /// <param name="site_id"></param>
        /// <returns></returns>
        public int ExportModel(string reference1, System.Nullable<int> site_id, ref string errorMessage)
        {
            System.Nullable<int> result = -10;
            using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
            {
                DataContext.usp_Export_Model(reference1, site_id, ref result, ref errorMessage);
            }
            return Convert.ToInt32(result);
        }

        /// <summary>
        /// Export Calendar Info to the client
        /// Make the entry in Export History
        /// </summary>
        /// <param name="reference1"></param>
        /// <param name="site_id"></param>
        /// <returns></returns>
        public int ExportCalendar(string reference1, System.Nullable<int> site_id, ref string errorMessage)
        {
            System.Nullable<int> result = -10;
            using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
            {
                DataContext.usp_Export_Calendar(reference1, site_id, ref result, ref errorMessage);
            }
            return Convert.ToInt32(result);
        }

        /// <summary>
        /// Export Site Info to the site
        /// Make the entry in Export History
        /// </summary>
        /// <param name="reference1"></param>
        /// <param name="site_id"></param>
        /// <returns></returns>
        public int ExportSiteSetup(string reference1, System.Nullable<int> site_id, ref string errorMessage)
        {
            System.Nullable<int> result = -10;
            using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
            {
                DataContext.usp_Export_SiteSetup(reference1, site_id, ref result, ref errorMessage);
            }
            return Convert.ToInt32(result);
        }

        /// <summary>
        /// Export Game Info to the Site
        /// By Making the entry in Export History
        /// </summary>
        /// <param name="site_id"></param>
        /// <returns></returns>
        public int ExportGameLibrary(System.Nullable<int> site_id, ref string errorMessage)
        {
            System.Nullable<int> result = -10;
            using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
            {
                DataContext.usp_Export_GameLibrary(site_id, ref result, ref errorMessage);
            }
            return Convert.ToInt32(result);
        }

        /// <summary>
        /// Update Exchange Passkey in Enterprise DB
        /// </summary>
        /// <param name="site_id"></param>
        /// <param name="passKey"></param>
        /// <returns></returns>
        public int UpdatePasskey(System.Nullable<int> site_id, string passKey, ref string errorMessage)
        {
            System.Nullable<int> result = -10;
            using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
            {
                DataContext.UpdatePasskey(site_id,passKey, ref result, ref errorMessage);
            }
            return Convert.ToInt32(result);
        }

        /// <summary>
        /// Enable/Disable the Site
        /// isEnable = true => Enable
        /// isEnable = false => Disable
        /// </summary>
        /// <param name="site_id"></param>
        /// <param name="isEnable"></param>
        /// <returns></returns>
        public int EnableorDisableSite(System.Nullable<int> site_id, System.Nullable<bool> isEnable, ref string errorMessage)
        {
            System.Nullable<int> result = -10;
            using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
            {
                DataContext.EnableorDisableSite(site_id, isEnable, ref result, ref errorMessage);
            }
            return Convert.ToInt32(result);
        }
        //public SiteCommonEntity IsNumericalCheck(string sInput)
        //{
        //    Char a;
        //    int temp = 0, nCount, nLength = 0;
        //    bool Numerical = true;
        //    nLength = sInput.Length;
        //    for (nCount = 0; nCount < nLength; nCount++)
        //    {
        //        a = Convert.ToChar(sInput.Substring(nCount, 1));
        //        temp = Convert.ToInt32(a);
        //        //MessageBox.Show(x.ToString());
        //        //if(x > 47 && x < 58)
        //        if (temp > 47 && temp < 58)
        //        {
        //            // IsNumerical = true;
        //        }
        //        else
        //        {
        //            Numerical = false;
        //        }
        //    }
        //    SiteCommonEntity Obj=null;
        //    Obj.IsNumerical = Numerical;

        //    return Obj;
        //}
        public void GetSetting(int? setting_ID, string setting_Name, string setting_Default, ref string setting_Value)
        {
            using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
            {
                DataContext.GetSetting(setting_ID, setting_Name, setting_Default, ref setting_Value);
            }
        }
        //private XElement InsertOrUpdateSite(AdminSiteEntity modifiedEntity)
        //{
        //    try
        //    {
        //        if (modifiedEntity == null) return null;
        //        XElement element = new XElement("SiteDetaills");
        //        foreach (PropertyInfo prop in modifiedEntity.GetType().GetProperties())
        //        {
        //            element.Add(new XElement(prop.Name, prop.GetValue(modifiedEntity, null) == null ? "" : prop.GetValue(modifiedEntity, null)));

        //            // !prop.GetValue(modifiedEntity, null).Equals(prop.GetValue(originalEntity, null))

        //        }
        //        string list = string.Empty;
        //        foreach (PropertyInfo prop in modifiedEntity.GetType().GetProperties())
        //        {
        //           // element.Add(new XElement(prop.Name, prop.GetValue(modifiedEntity, null) == null ? "" : prop.GetValue(modifiedEntity, null)));
        //            list += prop.Name +"\r\n";
        //            // !prop.GetValue(modifiedEntity, null).Equals(prop.GetValue(originalEntity, null))

        //        }
        //        return element;
        //    }
        //    catch (Exception ex)
        //    {
        //        ExceptionManager.Publish(ex);
        //        return null;
        //    }
        //}


        public void InsertOrUpdatSite(AdminSiteEntity _modifiedEntity)
        {
           // InsertOrUpdateSite(_modifiedEntity);
            using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
            {
                System.Nullable<int> siteID = 0;
                siteID = _modifiedEntity.Site_ID;
                DataContext.InsertorUpdateSiteInfo(
                    ref siteID,
_modifiedEntity.Service_Supplier_ID,
_modifiedEntity.Service_Area_ID,
_modifiedEntity.Service_Depot_ID,
_modifiedEntity.Site_Name,
_modifiedEntity.Site_Code,
_modifiedEntity.Site_Supplier_Code,
_modifiedEntity.Site_Company_Code,
_modifiedEntity.Site_Phone_No,
_modifiedEntity.Site_Fax_No,
_modifiedEntity.Site_Email_Address,
_modifiedEntity.Site_Address_1,
_modifiedEntity.Site_Address_2,
_modifiedEntity.Site_Address_3,
_modifiedEntity.Site_Address_4,
_modifiedEntity.Site_Address_5,
_modifiedEntity.Site_Postcode,
_modifiedEntity.Site_Manager,
_modifiedEntity.Site_Price_Per_Play_Default,
_modifiedEntity.Site_Price_Per_Play,
_modifiedEntity.Site_Jackpot_Default,
_modifiedEntity.Site_Jackpot,
_modifiedEntity.Site_Percentage_Payout_Default,
_modifiedEntity.Site_Percentage_Payout,
_modifiedEntity.Sub_Company_ID,
_modifiedEntity.Terms_Group_ID_Default,
_modifiedEntity.Terms_Group_ID,
_modifiedEntity.Access_Key_ID_Default,
_modifiedEntity.Access_Key_ID,
_modifiedEntity.Staff_ID_Default,
_modifiedEntity.Staff_ID,
_modifiedEntity.Site_Invoice_Name,
_modifiedEntity.Site_Invoice_Address,
_modifiedEntity.Site_Invoice_Postcode,
_modifiedEntity.Sub_Company_Region_ID,
_modifiedEntity.Sub_Company_Area_ID,
_modifiedEntity.Sub_Company_District_ID,
_modifiedEntity.Site_Image_Reference,
_modifiedEntity.Site_Image_Reference_2,
_modifiedEntity.Site_Closed,
_modifiedEntity.Depot_ID,
_modifiedEntity.Site_Classification_ID,
_modifiedEntity.Site_Grade,
_modifiedEntity.Sage_Account_Ref,
_modifiedEntity.Site_Is_FreeFloat,
_modifiedEntity.Site_Local_Inbox,
_modifiedEntity.Site_Local_Outbox,
_modifiedEntity.Site_Memo,
_modifiedEntity.Site_Reference,
_modifiedEntity.Site_Trade_Type,
_modifiedEntity.Site_Non_Trading_Period_From,
_modifiedEntity.Site_Non_Trading_Period_To,
_modifiedEntity.Site_Supplier_Service_Area,
_modifiedEntity.Site_Supplier_Area,
_modifiedEntity.Standard_Opening_Hours_ID,
_modifiedEntity.Next_Sub_Company_ID,
_modifiedEntity.Next_Sub_Company_Change_Date,
_modifiedEntity.Site_Previous_Sub_Company_ID,
_modifiedEntity.Previous_Sub_Company_Change_Date,
_modifiedEntity.Site_Honeyframe_EDI,
_modifiedEntity.Site_Datapak_Protocol,
_modifiedEntity.Site_Start_Date,
_modifiedEntity.Site_End_Date,
_modifiedEntity.Site_Licence_Number,
_modifiedEntity.Site_Fiscal_Code,
_modifiedEntity.Site_Street_Number,
_modifiedEntity.Site_Province,
_modifiedEntity.Site_Municipality,
_modifiedEntity.Site_Cadastral_Code,
_modifiedEntity.Site_Area,
_modifiedEntity.Site_Location_Type,
_modifiedEntity.Site_Toponym,
_modifiedEntity.Site_Licensee_Commenced_Date,
_modifiedEntity.Site_Licensee_Agreement_End_Date,
_modifiedEntity.Site_Licensee_Agreement_Type,
_modifiedEntity.Site_Application,
_modifiedEntity.Region,
_modifiedEntity.WebURL,
_modifiedEntity.Site_Status_ID,
_modifiedEntity.Site_Inactive_Date,
_modifiedEntity.Site_Connection_IPAddress,
_modifiedEntity.Site_MaxNumber_VLT,
_modifiedEntity.Site_ZonaRice,
_modifiedEntity.StackerLimitPercentage,
_modifiedEntity.Site_Enabled,
_modifiedEntity.IsTITOEnabled,
_modifiedEntity.IsCrossTicketingEnabled,
_modifiedEntity.IsNonCashVoucherEnabled


);

                _modifiedEntity.Site_ID = Convert.ToInt32(siteID);
    }
}

        public bool AuditData(AdminSiteEntity modifiedEntity, AdminSiteEntity originalEntity, int userID, string userName, string SiteName)
        {
            try
            {
                string addOrModify = (originalEntity == null || originalEntity.Site_ID == 0) ? "added" : "modified";
                string AddOrModify = (originalEntity == null || originalEntity.Site_ID == 0) ? "ADD" : "MODIFY";

                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    IDictionary<string, string> dic_getVal = new Dictionary<string, string>();
                    string strName = ""; string strValue = ""; string[] sValue; string sNewValue = ""; string sOldValue = "";
                    foreach (PropertyInfo prop in modifiedEntity.GetType().GetProperties())
                    {
                        if (!Convert.ToString(prop.GetValue(modifiedEntity, null)).Equals(Convert.ToString(prop.GetValue(originalEntity, null))))
                        {
                            strValue = Convert.ToString(prop.GetValue(originalEntity, null)) + "~" + Convert.ToString(prop.GetValue(modifiedEntity, null));
                            dic_getVal.Add(new KeyValuePair<string, string>(prop.Name, strValue));
                        }
                    }
                    foreach (string obj in dic_getVal.Keys)
                    {
                        strName += obj + ",";
                    }
                    if (dic_getVal.Count > 1 && addOrModify == "modified")
                    {
                        DataContext.InsertAuditData(userID, userName, (int)ModuleNameEnterprise.AUDIT_SITE, ModuleNameEnterprise.AUDIT_SITE.ToString(), "Site Admin", "",
                           strName.ToString(), "", "", "Site Admin [" + strName + "] Modified for Site: " + SiteName, AddOrModify);
                        Thread.Sleep(10);
                    }
                    else if (dic_getVal.Count > 0 && addOrModify == "added")
                    {
                        DataContext.InsertAuditData(userID, userName, (int)ModuleNameEnterprise.AUDIT_SITE, ModuleNameEnterprise.AUDIT_SITE.ToString(), "Site Admin", "", strName.ToString(),
                          "", "", string.Format("Site [{0}] Added ..", SiteName, strName.ToString(), "", SiteName), AddOrModify);
                        Thread.Sleep(10);
                    }
                    else if(dic_getVal.Count!=0)
                    {
                        sValue = strValue.Split('~');
                        sOldValue = sValue[0].ToString();
                        sNewValue = sValue[1].ToString();

                        DataContext.InsertAuditData(userID, userName, (int)ModuleNameEnterprise.AUDIT_SITE, ModuleNameEnterprise.AUDIT_SITE.ToString(), "Site Admin", "", strName.ToString(),
                          sOldValue, sNewValue, string.Format("Site [{0}] Modified ..[{1}]:{2}-->{3}", SiteName, strName.ToString(), sOldValue, sNewValue), AddOrModify);
                        Thread.Sleep(10);
                    }
                }
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
                return false;
            }
        
            return true;
        }

        public int CheckWebURLExists(int _SiteID, string WebURL, int? Result)
        {
            int ResultVal = -1;
            try
            {
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    DataContext.rsp_CheckWebURLExists(_SiteID, WebURL, ref Result);
                    ResultVal = Convert.ToInt32(Result);
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error in Business-SiteDetailsCheckWebURLExists", LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
            }
            return ResultVal;
        }
        public void UpdateDepotIDForBarPosition(int? iDepotID,int iSiteID)
        {
            try
            {
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    DataContext.UpdateDepotIDForBarPosition(iDepotID,iSiteID);
    }
}
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

        }

        public SiteKeyExists IsSiteKeyExists(int SiteID)
        {
            SiteKeyExists objSiteKeyExists = null;
            try
            {
                List<rsp_CheckIsExchangeKeyExistsResult> StatusList;
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    StatusList = DataContext.CheckIsExchangeKeyExists(SiteID).ToList();
                }

                objSiteKeyExists = (from obj in StatusList
                                    select new SiteKeyExists
                                   {
                                       IsExchangeKeyAvailable = obj.IsExchangeKeyAvailable
                                   }).Single<SiteKeyExists>();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return objSiteKeyExists;
        }
    }
}
