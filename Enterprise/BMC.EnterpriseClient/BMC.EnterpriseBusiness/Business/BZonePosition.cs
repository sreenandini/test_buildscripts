using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.EnterpriseDataAccess;
using BMC.Common.ExceptionManagement;
using BMC.Common.LogManagement;
using BMC.EnterpriseBusiness.Entities;
using System.Data;
using BMC.Common.Utilities;
using BMC.EnterpriseDataAccess;
using System.Reflection;
using System.Data.Linq;
using Audit.Transport;
using Audit.BusinessClasses;

namespace BMC.EnterpriseBusiness.Business
{
    public class BZonePosition
    {
        public int BInsertBarPosition(string BarPositionName,
                                      int SiteId,
                                      string Bar_Position_Start_Date,
                                      int Depot_ID,
                                      string Bar_Position_Rent_Past_Date,
                                      string Bar_Position_Share_Past_Date,
                                      string Bar_Position_Licence_Past_Date,
                                      string Bar_Position_Rent_Future_Date,
                                      string Bar_Position_Share_Future_Date,
                                      string Bar_Position_Licence_Future_Date,
                                      bool Bar_Position_Use_Terms,
                                      string Bar_Position_Last_Collection_Date,
                                      string Bar_Position_Collection_Rent_Paid_Until,
                                      string Bar_Position_Price_Per_Play,
                                      bool Bar_Position_Price_Per_Play_Default,
                                      string Bar_Position_Jackpot,
                                      bool Bar_Position_Jackpot_Default,
                                      string Bar_Position_Percentage_Payout,
                                      bool Bar_Position_Percentage_Payout_Default,
                                      int Access_Key_ID,
                                      bool Access_Key_ID_Default,
                                      int Terms_Group_ID,
                                      bool Terms_Group_ID_Default,
                                      ref int? BarPositionId)
        {

            try
            {
                using (EnterpriseDataContext Datacontext = EnterpriseDataContextHelper.GetDataContext())
                {
                    return Datacontext.USP_InsertBarPosition(BarPositionName,
                                                                     SiteId,
                                                                     Bar_Position_Start_Date,
                                                                     Depot_ID,
                                                                     Bar_Position_Rent_Past_Date,
                                                                     Bar_Position_Share_Past_Date,
                                                                     Bar_Position_Licence_Past_Date,
                                                                     Bar_Position_Rent_Future_Date,
                                                                     Bar_Position_Share_Future_Date,
                                                                     Bar_Position_Licence_Future_Date,
                                                                     Bar_Position_Use_Terms,
                                                                     Bar_Position_Last_Collection_Date,
                                                                     Bar_Position_Collection_Rent_Paid_Until,
                                                                     Bar_Position_Price_Per_Play,
                                                                     Bar_Position_Price_Per_Play_Default,
                                                                     Bar_Position_Jackpot,
                                                                     Bar_Position_Jackpot_Default,
                                                                     Bar_Position_Percentage_Payout,
                                                                     Bar_Position_Percentage_Payout_Default,
                                                                     Access_Key_ID,
                                                                     Access_Key_ID_Default,
                                                                     Terms_Group_ID,
                                                                     Terms_Group_ID_Default,
                                                                     ref BarPositionId);

                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("BZonePosition-Insert new BarPosition", LogManager.enumLogLevel.Info);
                ExceptionManager.Publish(ex);
                return 0;
            }
        }

        public int BCheckNameExist(int SiteId, string BarPositionName, ref int? count)
        {
            try
            {
                using (EnterpriseDataContext Datacontext = EnterpriseDataContextHelper.GetDataContext())
                {

                    return Datacontext.rsp_GetBarPosition(SiteId, BarPositionName, ref count,0);

                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("BZonePosition-BarPosition Name Exist", LogManager.enumLogLevel.Info);
                ExceptionManager.Publish(ex);
                return 0;
            }
        }

        public int BCheckNameExist(int SiteId, string BarPositionName, ref int? count, int barPositionID)
        {
            try
            {
                using (EnterpriseDataContext Datacontext = EnterpriseDataContextHelper.GetDataContext())
                {

                    return Datacontext.rsp_GetBarPosition(SiteId, BarPositionName, ref count, barPositionID);

                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("BZonePosition-BarPosition Name Exist", LogManager.enumLogLevel.Info);
                ExceptionManager.Publish(ex);
                return 0;
            }
        }

        public void Audit(ModuleNameEnterprise sModuleName, string sDesc, string sScreenName, string sZoneName, string sField, string sPrevValue, string sNewValue, int iUserId, string strUsername, string SiteName)
        {
            try
            {
                Audit_History AH = new Audit_History();
                AH.EnterpriseModuleName = sModuleName;
                AH.Audit_Screen_Name = sScreenName;
                AH.Audit_Desc = sDesc + sZoneName + " modified  ..[" + sField + "] : " + sPrevValue + " -> " + sNewValue + " for Site: " + SiteName;
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
        public void InsertNewAuditEntry(ModuleNameEnterprise moduleName, string strScreenName, string strField, string strNewItem, int iUserId, string strUsername, string SiteName)
        {
            try
            {
                //Calling Audit Method
                Audit_History AH = new Audit_History();
                //Populate required Values            
                AH.EnterpriseModuleName = moduleName;
                AH.Audit_Screen_Name = strScreenName;
                AH.Audit_Desc = "Record [" + strNewItem + "] added to " + strScreenName + " for Site: " + SiteName;
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

        public BarPositionEntity BGetBarPositionDetails(int _BarPositionID)
        {
            try
            {
                using (EnterpriseDataContext Datacontext = EnterpriseDataContextHelper.GetDataContext())
                {
                    BarPositionEntity entity = null;
                    entity = (from result in Datacontext.rsp_GetBarPositionDetailsById(_BarPositionID)
                              select new BarPositionEntity
                              {
                                  Bar_Position_ID = result.Bar_Position_ID,
                                  Site_ID = result.Site_ID,
                                  Zone_ID = result.Zone_ID,
                                  Access_Key_ID = result.Access_Key_ID,
                                  Access_Key_ID_Default = result.Access_Key_ID_Default,
                                  Terms_Group_ID = result.Terms_Group_ID,
                                  Terms_Group_Changeover_Date = result.Terms_Group_Changeover_Date,
                                  Terms_Group_Future_ID = result.Terms_Group_Future_ID,
                                  Terms_Group_Past_Changeover_Date = result.Terms_Group_Past_Changeover_Date,
                                  Terms_Group_Past_ID = result.Terms_Group_Past_ID,
                                  Terms_Group_ID_Default = result.Terms_Group_ID_Default,
                                  Duty_ID = result.Duty_ID,
                                  Depot_ID = result.Depot_ID,
                                  Machine_Type_ID = result.Machine_Type_ID,
                                  Bar_Position_Name = result.Bar_Position_Name,
                                  Bar_Position_Location = result.Bar_Position_Location,
                                  Bar_Position_Start_Date = result.Bar_Position_Start_Date,
                                  Bar_Position_End_Date = result.Bar_Position_End_Date,
                                  Bar_Position_Collection_Day = result.Bar_Position_Collection_Day,
                                  Bar_Position_Price_Per_Play = result.Bar_Position_Price_Per_Play,
                                  Bar_Position_Price_Per_Play_Default = result.Bar_Position_Price_Per_Play_Default,
                                  Bar_Position_Jackpot = result.Bar_Position_Jackpot,
                                  Bar_Position_Jackpot_Default = result.Bar_Position_Jackpot_Default,
                                  Bar_Position_Percentage_Payout = result.Bar_Position_Percentage_Payout,
                                  Bar_Position_Percentage_Payout_Default = result.Bar_Position_Percentage_Payout_Default,
                                  Bar_Position_Last_Collection_Date = result.Bar_Position_Last_Collection_Date,
                                  Bar_Position_Collection_Rent_Paid_Until = result.Bar_Position_Collection_Rent_Paid_Until,
                                  Bar_Position_Collection_Period = result.Bar_Position_Collection_Period,
                                  Bar_Position_Supplier_AMEDIS_Code = result.Bar_Position_Supplier_AMEDIS_Code,
                                  Bar_Position_Supplier_Depot_AMEDIS_Code = result.Bar_Position_Supplier_Depot_AMEDIS_Code,
                                  Bar_Position_Supplier_Site_Code = result.Bar_Position_Supplier_Site_Code,
                                  Bar_Position_Supplier_Position_Code = result.Bar_Position_Supplier_Position_Code,
                                  Bar_Position_Supplier_Area = result.Bar_Position_Supplier_Area,
                                  Bar_Position_Supplier_Service_Area = result.Bar_Position_Supplier_Service_Area,
                                  Bar_Position_Company_Position_Code = result.Bar_Position_Company_Position_Code,
                                  Bar_Position_Company_Target = result.Bar_Position_Company_Target,
                                  Bar_Position_Collection_Frequency = result.Bar_Position_Collection_Frequency,
                                  Bar_Position_Image_Reference = result.Bar_Position_Image_Reference,
                                  Bar_Position_Machine_Type_AMEDIS_Code = result.Bar_Position_Machine_Type_AMEDIS_Code,
                                  Bar_Position_Rent = result.Bar_Position_Rent,
                                  Bar_Position_Rent_Previous = result.Bar_Position_Rent_Previous,
                                  Bar_Position_Rent_Future = result.Bar_Position_Rent_Future,
                                  Bar_Position_Rent_Past_Date = result.Bar_Position_Rent_Past_Date,
                                  Bar_Position_Rent_Future_Date = result.Bar_Position_Rent_Future_Date,
                                  Bar_Position_Supplier_Share = result.Bar_Position_Supplier_Share,
                                  Bar_Position_Site_Share = result.Bar_Position_Site_Share,
                                  Bar_Position_Owners_Share = result.Bar_Position_Owners_Share,
                                  Bar_Position_Secondary_Owners_Share = result.Bar_Position_Secondary_Owners_Share,
                                  Bar_Position_Supplier_Share_Previous = result.Bar_Position_Supplier_Share_Previous,
                                  Bar_Position_Site_Share_Previous = result.Bar_Position_Site_Share_Previous,
                                  Bar_Position_Owners_Share_Previous = result.Bar_Position_Owners_Share_Previous,
                                  Bar_Position_Secondary_Owners_Share_Previous = result.Bar_Position_Secondary_Owners_Share_Previous,
                                  Bar_Position_Supplier_Share_Future = result.Bar_Position_Supplier_Share_Future,
                                  Bar_Position_Site_Share_Future = result.Bar_Position_Site_Share_Future,
                                  Bar_Position_Owners_Share_Future = result.Bar_Position_Owners_Share_Future,
                                  Bar_Position_Secondary_Owners_Share_Future = result.Bar_Position_Secondary_Owners_Share_Future,
                                  Bar_Position_Share_Past_Date = result.Bar_Position_Share_Past_Date,
                                  Bar_Position_Share_Future_Date = result.Bar_Position_Share_Future_Date,
                                  Bar_Position_Licence_Charge = result.Bar_Position_Licence_Charge,
                                  Bar_Position_Licence_Previous = result.Bar_Position_Licence_Previous,
                                  Bar_Position_Licence_Future = result.Bar_Position_Licence_Future,
                                  Bar_Position_Licence_Past_Date = result.Bar_Position_Licence_Past_Date,
                                  Bar_Position_Licence_Future_Date = result.Bar_Position_Licence_Future_Date,
                                  Bar_Position_Use_Terms = result.Bar_Position_Use_Terms,
                                  Bar_Position_TX_Collection = result.Bar_Position_TX_Collection,
                                  Bar_Position_TX_Collection_Use_Default = result.Bar_Position_TX_Collection_Use_Default,
                                  Bar_Position_TX_Movement = result.Bar_Position_TX_Movement,
                                  Bar_Position_TX_Movement_Use_Default = result.Bar_Position_TX_Movement_Use_Default,
                                  Bar_Position_TX_EDC = result.Bar_Position_TX_EDC,
                                  Bar_Position_TX_EDC_Use_Detault = result.Bar_Position_TX_EDC_Use_Detault,
                                  Bar_Position_TX_Format = result.Bar_Position_TX_Format,
                                  Bar_Position_TX_Format_Use_Default = result.Bar_Position_TX_Format_Use_Default,
                                  Bar_Position_RX_Collection = result.Bar_Position_RX_Collection,
                                  Bar_Position_RX_Collection_Use_Default = result.Bar_Position_RX_Collection_Use_Default,
                                  Bar_Position_RX_Movement = result.Bar_Position_RX_Movement,
                                  Bar_Position_RX_Movement_Use_Default = result.Bar_Position_RX_Movement_Use_Default,
                                  Bar_Position_RX_EDC = result.Bar_Position_RX_EDC,
                                  Bar_Position_RX_EDC_Use_Detault = result.Bar_Position_RX_EDC_Use_Detault,
                                  Bar_Position_RX_Format = result.Bar_Position_RX_Format,
                                  Bar_Position_RX_Format_Use_Default = result.Bar_Position_RX_Format_Use_Default,
                                  Bar_Position_Net_Target = result.Bar_Position_Net_Target,
                                  Bar_Position_Below_Net_Target_Counter = result.Bar_Position_Below_Net_Target_Counter,
                                  Bar_Position_Below_Company_Target_Counter = result.Bar_Position_Below_Company_Target_Counter,
                                  Bar_Position_Security_Required = result.Bar_Position_Security_Required,
                                  Bar_Position_Site_Has_Cashbox_Keys = result.Bar_Position_Site_Has_Cashbox_Keys,
                                  Bar_Position_Site_Has_FreePlay_Access = result.Bar_Position_Site_Has_FreePlay_Access,
                                  Bar_Position_Override_Rent = result.Bar_Position_Override_Rent,
                                  Bar_Position_Override_Shares = result.Bar_Position_Override_Shares,
                                  Bar_Position_Override_Licence = result.Bar_Position_Override_Licence,
                                  Bar_Position_Category = result.Bar_Position_Category,
                                  Bar_Position_PPL_Charge = result.Bar_Position_PPL_Charge,
                                  Bar_Position_PPL_Previous = result.Bar_Position_PPL_Previous,
                                  Bar_Position_PPL_Future = result.Bar_Position_PPL_Future,
                                  Bar_Position_PPL_Past_Date = result.Bar_Position_PPL_Past_Date,
                                  Bar_Position_PPL_Future_Date = result.Bar_Position_PPL_Future_Date,
                                  Bar_Position_Float_Issued = result.Bar_Position_Float_Issued,
                                  Bar_Position_Float_Recovered = result.Bar_Position_Float_Recovered,
                                  Bar_Position_Use_Site_Share_For_Secondary_Brewery = result.Bar_Position_Use_Site_Share_For_Secondary_Brewery,
                                  Bar_Position_Prize_LOS = result.Bar_Position_Prize_LOS,
                                  Bar_Position_Rent_Schedule_ID = result.Bar_Position_Rent_Schedule_ID,
                                  Bar_Position_Share_Schedule_ID = result.Bar_Position_Share_Schedule_ID,
                                  Bar_Position_Override_Rent_Schedule = result.Bar_Position_Override_Rent_Schedule,
                                  Bar_Position_Override_Share_Schedule = result.Bar_Position_Override_Share_Schedule,
                                  Bar_Position_Last_Collection_ID = result.Bar_Position_Last_Collection_ID,
                                  Bar_Position_Override_Rent_From_Schedule_To_Rent = result.Bar_Position_Override_Rent_From_Schedule_To_Rent,
                                  Bar_Position_Override_Rent_From_Rent_To_Schedule = result.Bar_Position_Override_Rent_From_Rent_To_Schedule,
                                  Bar_Position_Override_Rent_From_Schedule_To_Rent_Date = result.Bar_Position_Override_Rent_From_Schedule_To_Rent_Date,
                                  Bar_Position_Override_Rent_From_Rent_To_Schedule_Date = result.Bar_Position_Override_Rent_From_Rent_To_Schedule_Date,
                                  Bar_Position_Rent_Schedule_ID_From = result.Bar_Position_Rent_Schedule_ID_From,
                                  Bar_Position_Disable_EDI_Export = result.Bar_Position_Disable_EDI_Export,
                                  Bar_Position_Invoice_Period = result.Bar_Position_Invoice_Period,
                                  Bar_Position_Machine_Enabled = result.Bar_Position_Machine_Enabled,
                                  Bar_Position_Note_Acceptor_Enabled = result.Bar_Position_Note_Acceptor_Enabled,
                                  Bar_Position_Machine_Enabled_Date = result.Bar_Position_Machine_Enabled_Date,
                                  Bar_Position_IsEnable = result.Bar_Position_IsEnable
                              }).FirstOrDefault();
                    return entity;

                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("BGetBarPositionDetails", LogManager.enumLogLevel.Info);
                ExceptionManager.Publish(ex);
                return null;
            }
        }

        public List<BMC.EnterpriseDataAccess.EnterpriseDataContext.rsp_CreateBarPositionTemplateResult> BBulkCopyPosition(int BarPositionTemplateID, string StartBarPositionName, string LastBarPositionName)
        {
            try
            {
                using (EnterpriseDataContext Datacontext = EnterpriseDataContextHelper.GetDataContext())
                {

                    return Datacontext.rsp_CreateBarPositionTemplate(BarPositionTemplateID, StartBarPositionName, LastBarPositionName).ToList();

                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("BZonePosition-BBulkCopyPosition Insert", LogManager.enumLogLevel.Info);
                ExceptionManager.Publish(ex);
                return null;
            }
        }

        public int BExportHistory(string Reference, string Type, int SiteId)
        {
            try
            {
                using (EnterpriseDataContext Datacontext = EnterpriseDataContextHelper.GetDataContext())
                {

                    return Datacontext.usp_Export_History(Reference, Type, SiteId);

                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("BZonePosition-BExportHistory Insert", LogManager.enumLogLevel.Info);
                ExceptionManager.Publish(ex);
                return 0;
            }
        }


        public int BInsertZone(int SiteID, int? OpeningHoursID, string ZoneName,bool PromotionEnabled, ref int? ZoneID)
        {
            try
            {
                using (EnterpriseDataContext Datacontext = EnterpriseDataContextHelper.GetDataContext())
                {

                    return Datacontext.usp_InsertNewZone(SiteID, OpeningHoursID, ZoneName,PromotionEnabled, ref ZoneID);

                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("BZonePosition-New Zone Insert", LogManager.enumLogLevel.Info);
                ExceptionManager.Publish(ex);
                return 0;
            }
        }


        public int BInsertZoneName(string ZoneName, int ZoneID, int SiteID,bool PromotionEnabled, int? OpenHourID)
        {
            try
            {
                using (EnterpriseDataContext Datacontext = EnterpriseDataContextHelper.GetDataContext())
                {

                    return Datacontext.usp_UpdateZoneName(ZoneName, ZoneID, SiteID,PromotionEnabled, OpenHourID);

                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("BZonePosition-Zone Name Insert", LogManager.enumLogLevel.Info);
                ExceptionManager.Publish(ex);
                return 0;
            }
        }

        public List<BMC.EnterpriseDataAccess.EnterpriseDataContext.rsp_GetBarPositionDetailsBySiteIDResult> BGetBarPositionDetailsBySiteID(int Site_ID)
        {
            try
            {
                using (EnterpriseDataContext Datacontext = EnterpriseDataContextHelper.GetDataContext())
                {

                    return Datacontext.rsp_GetBarPositionDetailsBySiteID(Site_ID).ToList();

                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("BGetBarPositionDetailsBySiteID-Get Bar Position Details By SiteID", LogManager.enumLogLevel.Info);
                ExceptionManager.Publish(ex);
                return null;
            }
        }
        public int BDeleteZone(int ZoneID)
        {
            try
            {
                using (EnterpriseDataContext Datacontext = EnterpriseDataContextHelper.GetDataContext())
                {

                    return Datacontext.usp_DeleteZone(ZoneID);

                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("BDeleteZone-Delete Zone", LogManager.enumLogLevel.Info);
                ExceptionManager.Publish(ex);
                return 0;
            }
        }

        public List<BMC.EnterpriseDataAccess.EnterpriseDataContext.rsp_GetLatestBarPositionIDResult> GetBarPositionID()
        {
            try
            {
                using (EnterpriseDataContext Datacontext = EnterpriseDataContextHelper.GetDataContext())
                {

                    return Datacontext.rsp_GetLatestBarPositionID().ToList();

                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("GetBarPositionID-Get Bar Position ID", LogManager.enumLogLevel.Info);
                ExceptionManager.Publish(ex);
                return null;
            }
        }

        public DataTable LoadOpeningHours()
        {
            try
            {
                using (EnterpriseDataContext Datacontext = EnterpriseDataContextHelper.GetDataContext())
                {

                    return Datacontext.rsp_GetOpeningHours();

                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("LoadOpeningHours", LogManager.enumLogLevel.Info);
                ExceptionManager.Publish(ex);
                return null;
            }
        }

        public List<ZoneEntity> GetZones(int SiteID)
        {
            try
            {
                using (EnterpriseDataContext Datacontext = EnterpriseDataContextHelper.GetDataContext())
                {

                    var result = Datacontext.SelectZones(SiteID);
                    if (result == null) return null;
                    List<ZoneEntity> list = (from obj in result
                                             select new ZoneEntity
                                             {
                                                 Zone_ID = obj.Zone_ID,
                                                 Zone_Name = obj.Zone_Name,
                                                 AssignedZones = obj.AssignedZones,
                                                 PromotionEnabled=obj.PromotionEnabled,
                                                 OpenHours = obj.Standard_Opening_Hours_ID
                                             }).ToList<ZoneEntity>();
                    return list;

                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("GetZones", LogManager.enumLogLevel.Info);
                ExceptionManager.Publish(ex);
                return null;
            }
        }

        public DataTable GetZoneDetailsBySiteID(int SiteID)
        {
            using (EnterpriseDataContext ZonePositionDataAccess = EnterpriseDataContextHelper.GetDataContext())
            {
                try
                {
                    return ZonePositionDataAccess.GetZoneDetailsBySiteID(SiteID);
                }
                catch (Exception ex)
                {
                    ExceptionManager.Publish(ex);
                    return null;
                }
            }

        }
        public DataTable GetTermsGroup()
        {
            using (EnterpriseDataContext ZonePositionDataAccess = EnterpriseDataContextHelper.GetDataContext())
            {
                try
                {
                    return ZonePositionDataAccess.GetTermsGroupData();
                }
                catch (Exception ex)
                {
                    ExceptionManager.Publish(ex);
                    return null;
                }
            }

        }
        public DataTable GetDepotListForPosition(int supplierid)
        {
            using (EnterpriseDataContext ZonePositionDataAccess = EnterpriseDataContextHelper.GetDataContext())
            {
                try
                {
                    return ZonePositionDataAccess.GetDepotListForPosition(supplierid);
                }
                catch (Exception ex)
                {
                    ExceptionManager.Publish(ex);
                    return null;
                }
            }
        }
        public DataTable GetOperatorForBarPosition()
        {
            using (EnterpriseDataContext ZonePositionDataAccess = EnterpriseDataContextHelper.GetDataContext())
            {
                try
                {
                    return ZonePositionDataAccess.GetOperatorForPosition();
                }
                catch (Exception ex)
                {
                    ExceptionManager.Publish(ex);
                    return null;
                }
            }
        }
        public DataTable GetMachineType()
        {
            using (EnterpriseDataContext ZonePositionDataAccess = EnterpriseDataContextHelper.GetDataContext())
            {
                try
                {
                    return ZonePositionDataAccess.GetMachineType();
                }
                catch (Exception ex)
                {
                    ExceptionManager.Publish(ex);
                    return null;
                }
            }
        }
        public int BInsertOrUpdateBarPosition(BarPositionEntity newEntity, BarPositionEntity oldEntity)
        {
            try
            {
                using (EnterpriseDataContext Datacontext = EnterpriseDataContextHelper.GetDataContext())
                {
                    return Datacontext.InsertOrUpdateBarPosition(newEntity.Bar_Position_ID,newEntity.Site_ID,
newEntity.Zone_ID,
newEntity.Access_Key_ID,
newEntity.Access_Key_ID_Default,
newEntity.Terms_Group_ID,
newEntity.Terms_Group_Changeover_Date,
newEntity.Terms_Group_Future_ID,
newEntity.Terms_Group_Past_Changeover_Date,
newEntity.Terms_Group_Past_ID,
newEntity.Terms_Group_ID_Default,
newEntity.Duty_ID,
newEntity.Depot_ID,
newEntity.Machine_Type_ID,
newEntity.Bar_Position_Name,
newEntity.Bar_Position_Location,
newEntity.Bar_Position_Start_Date,
newEntity.Bar_Position_End_Date,
newEntity.Bar_Position_Collection_Day,
newEntity.Bar_Position_Price_Per_Play,
newEntity.Bar_Position_Price_Per_Play_Default,
newEntity.Bar_Position_Jackpot,
newEntity.Bar_Position_Jackpot_Default,
newEntity.Bar_Position_Percentage_Payout,
newEntity.Bar_Position_Percentage_Payout_Default,
newEntity.Bar_Position_Last_Collection_Date,
newEntity.Bar_Position_Collection_Rent_Paid_Until,
newEntity.Bar_Position_Collection_Period,
newEntity.Bar_Position_Supplier_AMEDIS_Code,
newEntity.Bar_Position_Supplier_Depot_AMEDIS_Code,
newEntity.Bar_Position_Supplier_Site_Code,
newEntity.Bar_Position_Supplier_Position_Code,
newEntity.Bar_Position_Supplier_Area,
newEntity.Bar_Position_Supplier_Service_Area,
newEntity.Bar_Position_Company_Position_Code,
newEntity.Bar_Position_Company_Target,
newEntity.Bar_Position_Collection_Frequency,
newEntity.Bar_Position_Image_Reference,
newEntity.Bar_Position_Machine_Type_AMEDIS_Code,
newEntity.Bar_Position_Rent,
newEntity.Bar_Position_Rent_Previous,
newEntity.Bar_Position_Rent_Future,
newEntity.Bar_Position_Rent_Past_Date,
newEntity.Bar_Position_Rent_Future_Date,
newEntity.Bar_Position_Supplier_Share,
newEntity.Bar_Position_Site_Share,
newEntity.Bar_Position_Owners_Share,
newEntity.Bar_Position_Secondary_Owners_Share,
newEntity.Bar_Position_Supplier_Share_Previous,
newEntity.Bar_Position_Site_Share_Previous,
newEntity.Bar_Position_Owners_Share_Previous,
newEntity.Bar_Position_Secondary_Owners_Share_Previous,
newEntity.Bar_Position_Supplier_Share_Future,
newEntity.Bar_Position_Site_Share_Future,
newEntity.Bar_Position_Owners_Share_Future,
newEntity.Bar_Position_Secondary_Owners_Share_Future,
newEntity.Bar_Position_Share_Past_Date,
newEntity.Bar_Position_Share_Future_Date,
newEntity.Bar_Position_Licence_Charge,
newEntity.Bar_Position_Licence_Previous,
newEntity.Bar_Position_Licence_Future,
newEntity.Bar_Position_Licence_Past_Date,
newEntity.Bar_Position_Licence_Future_Date,
newEntity.Bar_Position_Use_Terms,
newEntity.Bar_Position_TX_Collection,
newEntity.Bar_Position_TX_Collection_Use_Default,
newEntity.Bar_Position_TX_Movement,
newEntity.Bar_Position_TX_Movement_Use_Default,
newEntity.Bar_Position_TX_EDC,
newEntity.Bar_Position_TX_EDC_Use_Detault,
newEntity.Bar_Position_TX_Format,
newEntity.Bar_Position_TX_Format_Use_Default,
newEntity.Bar_Position_RX_Collection,
newEntity.Bar_Position_RX_Collection_Use_Default,
newEntity.Bar_Position_RX_Movement,
newEntity.Bar_Position_RX_Movement_Use_Default,
newEntity.Bar_Position_RX_EDC,
newEntity.Bar_Position_RX_EDC_Use_Detault,
newEntity.Bar_Position_RX_Format,
newEntity.Bar_Position_RX_Format_Use_Default,
newEntity.Bar_Position_Net_Target,
newEntity.Bar_Position_Below_Net_Target_Counter,
newEntity.Bar_Position_Below_Company_Target_Counter,
newEntity.Bar_Position_Security_Required,
newEntity.Bar_Position_Site_Has_Cashbox_Keys,
newEntity.Bar_Position_Site_Has_FreePlay_Access,
newEntity.Bar_Position_Override_Rent,
newEntity.Bar_Position_Override_Shares,
newEntity.Bar_Position_Override_Licence,
newEntity.Bar_Position_Category,
newEntity.Bar_Position_PPL_Charge,
newEntity.Bar_Position_PPL_Previous,
newEntity.Bar_Position_PPL_Future,
newEntity.Bar_Position_PPL_Past_Date,
newEntity.Bar_Position_PPL_Future_Date,
newEntity.Bar_Position_Float_Issued,
newEntity.Bar_Position_Float_Recovered,
newEntity.Bar_Position_Use_Site_Share_For_Secondary_Brewery,
newEntity.Bar_Position_Prize_LOS,
newEntity.Bar_Position_Rent_Schedule_ID,
newEntity.Bar_Position_IsEnable
);
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("BUpdateBarPosition-Zone Name Insert", LogManager.enumLogLevel.Info);
                ExceptionManager.Publish(ex);
                return 0;
            }
        }


        public BarPositionInfoEntity GetBarPositionInfo(Int32 barPositionID)
        {
            try
            {
                using (EnterpriseDataContext Datacontext = EnterpriseDataContextHelper.GetDataContext())
                {
                    var result = Datacontext.GetBarPositionInfo(barPositionID);
                    BarPositionInfoEntity entity = (from n in result
                                                    select new BarPositionInfoEntity
                                                    {
                                                        Bar_Position_Invoice_Period = n.Bar_Position_Invoice_Period,
                                                        Site_Code = n.Site_Code,
                                                        Site_Name = n.Site_Name,
                                                        Site_ID = n.Site_ID,
                                                        Bar_Position_Name = n.Bar_Position_Name,
                                                        Bar_Position_Company_Position_Code = n.Bar_Position_Company_Position_Code,
                                                        Bar_Position_End_Date = n.Bar_Position_End_Date,
                                                        Machine_Type_ID = n.Machine_Type_ID,
                                                        Bar_Position_Location = n.Bar_Position_Location,
                                                        Zone_ID = n.Zone_ID,
                                                        Bar_Position_Supplier_Site_Code = n.Bar_Position_Supplier_Site_Code,
                                                        Bar_Position_Supplier_Position_Code = n.Bar_Position_Supplier_Position_Code,
                                                        Bar_Position_Image_Reference = n.Bar_Position_Image_Reference,
                                                        Bar_Position_Price_Per_Play_Default = n.Bar_Position_Price_Per_Play_Default,
                                                        Bar_Position_Price_Per_Play = n.Bar_Position_Price_Per_Play,
                                                        Bar_Position_Jackpot_Default = n.Bar_Position_Jackpot_Default,
                                                        Bar_Position_Jackpot = n.Bar_Position_Jackpot,
                                                        Bar_Position_Percentage_Payout_Default = n.Bar_Position_Percentage_Payout_Default,
                                                        Bar_Position_Percentage_Payout = n.Bar_Position_Percentage_Payout,
                                                        Terms_Group_ID_Default = n.Terms_Group_ID_Default,
                                                        Terms_Group_ID = n.Terms_Group_ID,
                                                        Access_Key_ID_Default = n.Access_Key_ID_Default,
                                                        Access_Key_ID = n.Access_Key_ID,
                                                        Depot_ID = n.Depot_ID,
                                                        Depot_Name = n.Depot_Name,
                                                        Operator_ID = n.Operator_ID,
                                                        Operator_Name = n.Operator_Name,
                                                        Machine_Name = n.Machine_Name,
                                                        Machine_BACTA_Code = n.Machine_BACTA_Code,
                                                        Machine_Stock_No = n.Machine_Stock_No,
                                                        Installation_End_Date = n.Installation_End_Date,
                                                        Bar_Position_Category = n.Bar_Position_Category,
                                                        Bar_Position_Override_Licence = n.Bar_Position_Override_Licence,
                                                        Bar_Position_Override_Shares = n.Bar_Position_Override_Shares,
                                                        Bar_Position_Override_Rent = n.Bar_Position_Override_Rent,
                                                        Bar_Position_Rent = n.Bar_Position_Rent,
                                                        Bar_Position_Rent_Previous = n.Bar_Position_Rent_Previous,
                                                        Bar_Position_Rent_Future = n.Bar_Position_Rent_Future,
                                                        Bar_Position_Rent_Past_Date = n.Bar_Position_Rent_Past_Date,
                                                        Bar_Position_Rent_Future_Date = n.Bar_Position_Rent_Future_Date,
                                                        Bar_Position_Supplier_Share = n.Bar_Position_Supplier_Share,
                                                        Bar_Position_Site_Share = n.Bar_Position_Site_Share,
                                                        Bar_Position_Owners_Share = n.Bar_Position_Owners_Share,
                                                        Bar_Position_Secondary_Owners_Share = n.Bar_Position_Secondary_Owners_Share,
                                                        Bar_Position_Supplier_Share_Previous = n.Bar_Position_Supplier_Share_Previous,
                                                        Bar_Position_Site_Share_Previous = n.Bar_Position_Site_Share_Previous,
                                                        Bar_Position_Owners_Share_Previous = n.Bar_Position_Owners_Share_Previous,
                                                        Bar_Position_Secondary_Owners_Share_Previous = n.Bar_Position_Secondary_Owners_Share_Previous,
                                                        Bar_Position_Collection_Period = n.Bar_Position_Collection_Period,
                                                        Terms_Group_Past_Changeover_Date = n.Terms_Group_Past_Changeover_Date,
                                                        Terms_Group_Past_ID = n.Terms_Group_Past_ID,
                                                        Bar_Position_Supplier_Share_Future = n.Bar_Position_Supplier_Share_Future,
                                                        Bar_Position_Site_Share_Future = n.Bar_Position_Site_Share_Future,
                                                        Bar_Position_Owners_Share_Future = n.Bar_Position_Owners_Share_Future,
                                                        Bar_Position_Secondary_Owners_Share_Future = n.Bar_Position_Secondary_Owners_Share_Future,
                                                        Bar_Position_Share_Past_Date = n.Bar_Position_Share_Past_Date,
                                                        Bar_Position_Share_Future_Date = n.Bar_Position_Share_Future_Date,
                                                        Bar_Position_Licence_Charge = n.Bar_Position_Licence_Charge,
                                                        Bar_Position_Licence_Previous = n.Bar_Position_Licence_Previous,
                                                        Bar_Position_Licence_Future = n.Bar_Position_Licence_Future,
                                                        Bar_Position_Licence_Past_Date = n.Bar_Position_Licence_Past_Date,
                                                        Bar_Position_Licence_Future_Date = n.Bar_Position_Licence_Future_Date,
                                                        Bar_Position_Use_Terms = n.Bar_Position_Use_Terms,
                                                        Bar_Position_Collection_Day = n.Bar_Position_Collection_Day,
                                                        Bar_Position_Use_Site_Share_For_Secondary_Brewery = n.Bar_Position_Use_Site_Share_For_Secondary_Brewery,
                                                        Terms_Group_Changeover_Date = n.Terms_Group_Changeover_Date,
                                                        Terms_Group_Future_ID = n.Terms_Group_Future_ID,
                                                        Bar_Position_Prize_LOS = n.Bar_Position_Prize_LOS,
                                                        Bar_Position_Rent_Schedule_ID = n.Bar_Position_Rent_Schedule_ID,
                                                        Bar_Position_Share_Schedule_ID = n.Bar_Position_Share_Schedule_ID,
                                                        Bar_Position_Override_Rent_Schedule = n.Bar_Position_Override_Rent_Schedule,
                                                        Bar_Position_Override_Share_Schedule = n.Bar_Position_Override_Share_Schedule,
                                                        Bar_Position_Override_Rent_From_Schedule_To_Rent = n.Bar_Position_Override_Rent_From_Schedule_To_Rent,
                                                        Bar_Position_Override_Rent_From_Rent_To_Schedule = n.Bar_Position_Override_Rent_From_Rent_To_Schedule,
                                                        Bar_Position_Override_Rent_From_Schedule_To_Rent_Date = n.Bar_Position_Override_Rent_From_Schedule_To_Rent_Date,
                                                        Bar_Position_Override_Rent_From_Rent_To_Schedule_Date = n.Bar_Position_Override_Rent_From_Rent_To_Schedule_Date,
                                                        Bar_Position_Rent_Schedule_ID_From = n.Bar_Position_Rent_Schedule_ID_From,
                                                        Bar_Position_Disable_EDI_Export = n.Bar_Position_Disable_EDI_Export,
                                                        Bar_Position_IsEnable = n.Bar_Position_IsEnable
                                                    }).FirstOrDefault();
                    return entity;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return null;
        }


        public BarPositionExtensionEntity GetBarPositionExtension(int barPositionID)
        {
            try
            {
                using (EnterpriseDataContext Datacontext = EnterpriseDataContextHelper.GetDataContext())
                {
                    var result = Datacontext.rsp_GetBarPositionExtension(barPositionID);
                    BarPositionExtensionEntity entity = (from n in result
                                                         select new BarPositionExtensionEntity
                                                         {
                                                             Bar_Position_ID = n.Bar_Position_ID,
                                                             Bar_Position_Image = n.Bar_Position_Image
                                                         }).FirstOrDefault();
                    return entity;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return null;
        }

        public bool InsertOrUpdateBarPositionExtension(int barPositionID, byte[] image, bool isDelete)
        {
            try
            {
                using (EnterpriseDataContext Datacontext = EnterpriseDataContextHelper.GetDataContext())
                {
                    Datacontext.InsertOrUpdateBarPositionExtension(barPositionID, image, isDelete);
                }
                return true;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return false;
        }
    }


}

