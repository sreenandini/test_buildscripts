using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.CoreLib;
using BMC.EnterpriseBusiness.Entities;
using BMC.CoreLib.Diagnostics;
using BMC.EnterpriseDataAccess;
using System.Data.Linq;
using BMC.Common.LogManagement;
using BMC.Common.ExceptionManagement;
using System.Windows.Forms;
using System.Reflection;
using System.ComponentModel;
using Audit.Transport;
using Audit.BusinessClasses;
using System.Data.Linq.Mapping;


namespace BMC.EnterpriseBusiness.Business
{
    public class ServiceCallBusiness : DisposableObject
    {
       private static ServiceCallBusiness _Scallbiz;

       public ServiceCallBusiness() { }

       public static ServiceCallBusiness CreateInstance()
        {
            if (_Scallbiz == null)
                _Scallbiz = new ServiceCallBusiness();
            return _Scallbiz;
        }

       public List<CallGroup> LoadCallGroup(string defaultString)
       {
           try
           {
               LogManager.WriteLog("Inside LoadCallGroupDescriptionResult", LogManager.enumLogLevel.Info);
               List<CallGroup> objcoll = new List<CallGroup>();

               using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
               {
                   foreach (var entity in DataContext.LoadCallGroupDescription())
                   {
                       objcoll.Add(new CallGroup()
                       {
                           Description=entity.Call_Group_Description,
                           Id=entity.Call_Group_ID
                       });
                   }
                   LogManager.WriteLog("End of Get CallGroupEntity", LogManager.enumLogLevel.Info);
                   objcoll.Insert(0, new CallGroup() { Id = 0, Description = defaultString });
                   return objcoll;
               }
           }
           catch (Exception ex)
           {
               ExceptionManager.Publish(ex);
               throw ex;
           }
       }

       public List<CallFault> LoadFaultDescription(int groupId, string defaultString)
       {
           try
           {
               LogManager.WriteLog("Inside LoadFaultDescriptionResult", LogManager.enumLogLevel.Info);
               List<CallFault> objcoll = new List<CallFault>();

               using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
               {
                   foreach (var entity in DataContext.rsp_GetCallFaultsByGroupID(groupId))
                   {
                       objcoll.Add(new CallFault()
                       {
                           Description = entity.Call_Fault_Description,
                           Id = entity.Call_Fault_ID
                       });
                   }
                   LogManager.WriteLog("End of LoadFaultDescriptionResult", LogManager.enumLogLevel.Info);
                   objcoll.Insert(0, new CallFault() { Id = 0, Description = defaultString });
                   return objcoll;
               }
           }
           catch (Exception ex)
           {
               ExceptionManager.Publish(ex);
               throw ex;
           }
       }

       public List<CallRemedy> LoadCallRemedy(string defaultString)
       {
           try
           {
               LogManager.WriteLog("Inside LoadCallRemedy", LogManager.enumLogLevel.Info);
               List<CallRemedy> objcoll = new List<CallRemedy>();

               using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
               {
                   foreach (var entity in DataContext.LoadCallRemedyDescription())
                   {
                       objcoll.Add(new CallRemedy()
                       {
                           Description = entity.Call_Remedy_Description,
                           Id = entity.Call_Remedy_ID
                       });
                   }
                   LogManager.WriteLog("End of LoadCallRemedy", LogManager.enumLogLevel.Info);
                   objcoll.Insert(0, new CallRemedy() { Id = 0, Description =  defaultString });
                   return objcoll;
               }
           }
           catch (Exception ex)
           {
               ExceptionManager.Publish(ex);
               throw ex;
           }
       }

       public List<CallSource> LoadCallSource(string defaultString)
       {
           try
           {
               LogManager.WriteLog("Inside LoadCallSourceDescriptionResult", LogManager.enumLogLevel.Info);
               List<CallSource> objcoll = new List<CallSource>();

               using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
               {
                   foreach (var entity in DataContext.rsp_GetCallSource())
                   {
                       objcoll.Add(new CallSource()
                       {
                           Description  = entity.Call_Source_Description,
                           Id = entity.Call_Source_ID
                       });
                   }
                   LogManager.WriteLog("End of LoadCallSourceDescriptionResult", LogManager.enumLogLevel.Info);
                   objcoll.Insert(0, new CallSource() { Id = 0, Description = defaultString });
                   return objcoll;
               }
           }
           catch (Exception ex)
           {
               ExceptionManager.Publish(ex);
               throw ex;
           }
       }

       public List<MachineTypeEntity> LoadMachineTypes(bool isNewCall)
       {
           try
           {
               LogManager.WriteLog("Inside LoadGetSAdminMachineTypeResult", LogManager.enumLogLevel.Info);
               List<MachineTypeEntity> objcoll = new List<MachineTypeEntity>();

               using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
               {
                   foreach (var entity in DataContext.GetMachineTypesForServiceCall(true))
                   {
                       objcoll.Add(new MachineTypeEntity()
                       {
                           Machine_Type_Description= entity.Machine_Type_Description,
                           Machine_Type_Code=entity.Machine_Type_Code,
                           Machine_Type_ID=entity.Machine_Type_ID                           
                       });
                   }
                   LogManager.WriteLog("End of LoadGetSAdminMachineTypeResult", LogManager.enumLogLevel.Info);
                   return objcoll;
               }
           }
           catch (Exception ex)
           {
               ExceptionManager.Publish(ex);
               throw ex;
           }
       }

       public List<GetMachineDetailsEntity> LoadMachineNames(bool isShowHistory, int siteId, int machineTypeId)
       {
           try
           {
               LogManager.WriteLog("Inside LoadMachineNames", LogManager.enumLogLevel.Info);
               List<GetMachineDetailsEntity> objcoll = new List<GetMachineDetailsEntity>();

               using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
               {
                   foreach (var entity in DataContext.GetMachineNamesForServiceCall(isShowHistory, siteId, machineTypeId))
                   {
                       objcoll.Add(new GetMachineDetailsEntity()
                       {
                            Bar_Position_Name = entity.Bar_Position_Name,
                            Installation_ID = entity.Installation_ID,
                            Machine_Name = entity.Machine_Name,
                            Machine_Stock_No = entity.Machine_Stock_No,
                            Machine_Manufacturers_Serial_No = entity.Machine_Manufacturers_Serial_No,
                             Custom_Machine_Name = entity.Machine_Name + "   [" + entity.Machine_Stock_No + "]  (" + entity.Bar_Position_Name 
                                                    + ") - " + entity.Machine_Manufacturers_Serial_No                            
                       });
                   }
                   LogManager.WriteLog("End of LoadMachineNames", LogManager.enumLogLevel.Info);
                   return objcoll;
               }
           }
           catch (Exception ex)
           {
               ExceptionManager.Publish(ex);
               throw ex;
           }
       }

       public static void LoadCallStatus(ComboBox cbmCallStatus, bool isCurrentCall = false, bool withDefaultValue = true, bool isCallStatusMinimal = false)
       {
           try
           {
               string[] minimalStatusList = new string[] { "Logged" , "Completed" };
    
               var desc = (from CallStatus item in Enum.GetValues(typeof(CallStatus))
                           select new
                           {
                               ID = (int)item,
                               Description = GetEnumDescription(item)
                           }).OrderBy((x) => x.ID).ToList();
              
               if (isCurrentCall == true)
               {
                   // Remove Rejected and Completed Status in Current calls combo list
                   desc.RemoveAt(desc.Count - 1);
                   desc.RemoveAt(desc.Count - 1);
               }

               if (isCallStatusMinimal)
               {
                   desc = desc.FindAll(x =>  x.ID == 0 || minimalStatusList.Contains(x.Description)).ToList();
               }

               if (withDefaultValue == false)
                   desc.RemoveAt(0);

               cbmCallStatus.DataSource = desc;
               cbmCallStatus.DisplayMember = "Description";
               cbmCallStatus.ValueMember = "ID";
           }
           catch (Exception ex)
           {
               ExceptionManager.Publish(ex);
               throw ex;
           }
       }

       public static void LoadCallStatusOld(ComboBox cmbCallStatus, bool isCurrentCall = false, bool withDefaultValue = true)
       {
           try
           {
                   var desc = (from CallStatus item in Enum.GetValues(typeof(CallStatus))
                               select new
                               {
                                   ID = (int)item,
                                   Description = GetEnumDescription(item)
                               }).OrderBy((x) => x.ID).ToList();

                   if (withDefaultValue == false)
                       desc.RemoveAt(0);

                   if (isCurrentCall == true)
                   {
                       // Remove Rejected and Completed Status in Current calls combo list
                        desc.RemoveAt(desc.Count - 1);
                        desc.RemoveAt(desc.Count - 1);                      
                   }


               cmbCallStatus.DataSource = desc;
               cmbCallStatus.DisplayMember = "Description";
               cmbCallStatus.ValueMember = "ID";
           }
           catch (Exception ex)
           {
               ExceptionManager.Publish(ex);
               throw ex;
           }
       }

       public static string GetEnumDescription(Enum value)
       {
           
               FieldInfo fi = value.GetType().GetField(value.ToString());

               DescriptionAttribute[] attributes =
                   (DescriptionAttribute[])fi.GetCustomAttributes(
                   typeof(DescriptionAttribute),
                   false);

               if (attributes != null &&
                   attributes.Length > 0)
                   return attributes[0].Description;
               else
                   return value.ToString();
           
       
       }

       public List<SiteEntityForService> LoadSiteNames(int _Site_ID, string defaultString = "")
       {
           try
           {
               LogManager.WriteLog("Inside LoadSiteNames on service call admin", LogManager.enumLogLevel.Info);
               List<SiteEntityForService> objcoll = new List<SiteEntityForService>();

               using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
               {
                   foreach (var entity in DataContext.GetsiteNamesForservices(_Site_ID))
                   {
                       objcoll.Add(new SiteEntityForService()
                       {
                          Id=entity.Site_ID,
                          Description=entity.Site_Name

                       });
                   }
                   LogManager.WriteLog("End of LoadSiteNames", LogManager.enumLogLevel.Info);
                   if(defaultString != string.Empty)
                        objcoll.Insert(0, new SiteEntityForService() { Id = 0, Description = defaultString });
                   return objcoll;
               }
           }
           catch (Exception ex)
           {
               ExceptionManager.Publish(ex);
               throw ex;
           }
       }

       public List<DepotEntityForService> LoadDepotNames(string defaultString = "")
       {
           try
           {
               LogManager.WriteLog("Inside LoadDepotNames on service call admin", LogManager.enumLogLevel.Info);
               List<DepotEntityForService> objcoll = new List<DepotEntityForService>();

               using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
               {
                   foreach (var entity in DataContext.GetDepotNamesForservices())
                   {
                       objcoll.Add(new DepotEntityForService()
                       {
                           Id = entity.Depot_ID,
                           Description = entity.Depot_Name

                       });
                   }
                   LogManager.WriteLog("End of LoadDepotNames", LogManager.enumLogLevel.Info);
                   if(defaultString != "")
                         objcoll.Insert(0, new DepotEntityForService() { Id = 0, Description = defaultString });
                   return objcoll;
               }
           }
           catch (Exception ex)
           {
               ExceptionManager.Publish(ex);
               throw ex;
           }
       }

       public List<EngineerEntityForService> LoadEngineerNamesForSite(int SiteId, string defaultString)
       {
           try
           {
               LogManager.WriteLog("Inside LoadEngineerNamesForSite on service call admin", LogManager.enumLogLevel.Info);
               List<EngineerEntityForService> objcoll = new List<EngineerEntityForService>();

               using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
               {
                   foreach (var entity in DataContext.GetEngineerNamesBySiteID(SiteId))
                   {
                       objcoll.Add(new EngineerEntityForService()
                       {
                           Id = entity.Staff_ID.Value,
                           Description = entity.Staff_Name

                       });
                   }
                   LogManager.WriteLog("End of LoadEngineerNamesForSite", LogManager.enumLogLevel.Info);
                   objcoll.Insert(0, new EngineerEntityForService() { Id = 0, Description = defaultString });
                   return objcoll;
               }
           }
           catch (Exception ex)
           {
               ExceptionManager.Publish(ex);
               throw ex;
           }
       }

       public List<EngineerEntityForService> LoadEngineerNames(int? _staff_ID,  string defaultText = "")
       {
           try
           {
               LogManager.WriteLog("Inside LoadEngineerNames on service call admin", LogManager.enumLogLevel.Info);
               List<EngineerEntityForService> objcoll = new List<EngineerEntityForService>();

               using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
               {
                   foreach (var entity in DataContext.GetEngineerNamesForservices(_staff_ID))
                   {
                       objcoll.Add(new EngineerEntityForService()
                       {
                           Id = entity.Staff_ID,
                           Description = entity.Staff_Name

                       });
                   }
                   LogManager.WriteLog("End of LoadEngineerNames", LogManager.enumLogLevel.Info);
                   if(defaultText != "")
                       objcoll.Insert(0, new EngineerEntityForService() { Id = 0, Description = defaultText });
                   return objcoll;
               }
           }
           catch (Exception ex)
           {
               ExceptionManager.Publish(ex);
               throw ex;
           }
       }

       public SiteDetailEntity LoadSiteDetails(int SiteId)
       {
           try
           {
               LogManager.WriteLog("Inside LoadSiteDetails on service call admin", LogManager.enumLogLevel.Info);
               List<SiteDetailEntity> objcoll = new List<SiteDetailEntity>();

               using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
               {
                   foreach (var entity in DataContext.GetSiteDetailsForServiceCall(SiteId))
                   {
                       objcoll.Add(new SiteDetailEntity()
                       {
                            Standard_Opening_Hours_Description = entity.Standard_Opening_Hours_Description,
                            Site_Name =  entity.Site_Name,
                            Site_Code  = entity.Site_Code,
                            Site_Address_1= entity.Site_Address_1,
                            Site_Address_2= entity.Site_Address_2,
                            Site_Address_3= entity.Site_Address_3,
                            Site_Address_4= entity.Site_Address_4,
                            Site_Address_5= entity.Site_Address_5,
                            Site_Postcode= entity.Site_Postcode,
                            Site_Manager= entity.Site_Manager,
                            Site_Phone_No= entity.Site_Phone_No,
                            Depot_Name= entity.Depot_Name,
                            Service_Area_Name= entity.Service_Area_Name,
                            Sub_Company_Name= entity.Sub_Company_Name
                       });
                   }
                   LogManager.WriteLog("End of LoadSiteDetails", LogManager.enumLogLevel.Info);
                   return objcoll[0];
               }
           }
           catch (Exception ex)
           {
               ExceptionManager.Publish(ex);
               throw ex;
           }
       }

       public List<SubCompanyEntityForService> LoadSubCompanyNames()
       {
           try
           {
               LogManager.WriteLog("Inside LoadSubCompanyNames on service call admin", LogManager.enumLogLevel.Info);
               List<SubCompanyEntityForService> objcoll = new List<SubCompanyEntityForService>();

               using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
               {
                   foreach (var entity in DataContext.GetSubCompanyNamesForservices())
                   {
                       objcoll.Add(new SubCompanyEntityForService()
                       {
                           Sub_Company_ID = entity.Sub_Company_ID,
                           Sub_Company_Name = entity.Sub_Company_Name

                       });
                   }
                   LogManager.WriteLog("End of LoadSubCompanyNames", LogManager.enumLogLevel.Info);
                   objcoll.Insert(0, new SubCompanyEntityForService() { Sub_Company_ID = 0, Sub_Company_Name = "--ANY--" });
                   return objcoll;
               }
           }
           catch (Exception ex)
           {
               ExceptionManager.Publish(ex);
               throw ex;
           }
       }       

       public List<ServiceCurrentCallDetailsEntity> GetServiceCurrentCallDetails(int? CallStatusID, int? CallGroupID, string DepotIDList, string StaffIDList, string SiteIDList, int? SubCompanyID, int? JobID)
       {
           try
           {
               LogManager.WriteLog("Inside Load GetServiceCurrentCallDetails", LogManager.enumLogLevel.Info);
               List<ServiceCurrentCallDetailsEntity> objcoll = new List<ServiceCurrentCallDetailsEntity>();

               using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
               {
                   foreach (var entity in DataContext.GetServiceCurrentCallDetails(CallStatusID,CallGroupID, DepotIDList,StaffIDList,SiteIDList,SubCompanyID,JobID))
                   {
                       objcoll.Add(new ServiceCurrentCallDetailsEntity()
                       {
                           Bar_Position_Name=entity.Bar_Position_Name,
                           Call_Description=entity.Call_Description,
                           Call_Status_Description=entity.Call_Status_Description,
                           Call_Status_ID=entity.Call_Status_ID,
                           CallOpenDays=entity.CallOpenDays,
                           Engineer_Name=entity.Engineer_Name,
                           Machine_Name=entity.Machine_Name,
                           Machine_Type_ID=entity.Machine_Type_ID,
                           Service_Additional_Work_Req=entity.Service_Additional_Work_Req,
                           Service_Alert_Priority_Machine=entity.Service_Alert_Priority_Machine,
                           Service_Alert_Priority_Site=entity.Service_Alert_Priority_Site,
                           Service_Allocated_Job_No=entity.Service_Allocated_Job_No,
                           Service_ID=entity.Service_ID,
                           Service_Job_Visit_No=entity.Service_Job_Visit_No,
                           Service_Received=entity.Service_Received,
                           Service_SMA=entity.Service_SMA,
                           Site_Address=entity.Site_Address,
                           Site_Code=entity.Site_Code,
                           Site_ID=entity.Site_ID,
                           Site_Postcode=entity.Site_Postcode,
                           SLA_Contract_Description = entity.SLA_Contract_Description,
                           Staff_Name = entity.Staff_Name,
                           Sub_Company_Name = entity.Sub_Company_Name,
                           TimeOpened = entity.TimeOpened
                       });
                   }
                   LogManager.WriteLog("End of GetServiceCurrentCallDetails", LogManager.enumLogLevel.Info);

                   return objcoll;
               }
           }
           catch (Exception ex)
           {
               ExceptionManager.Publish(ex);
               throw ex;
           }

       }
       
       public List<ServiceClosedCallDetailsEntity> GetServiceClosedCallDetails(DateTime? StartDate, DateTime? EndDate, int? CallRemedyID, string DepotIDList, string StaffIDList, string SiteIDList, int? SubCompanyID, int? JobID, string AssetNo)
       {
           try
           {
               LogManager.WriteLog("Inside Load GetServiceClosedCallDetails", LogManager.enumLogLevel.Info);
               List<ServiceClosedCallDetailsEntity> objcoll = new List<ServiceClosedCallDetailsEntity>();

               using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
               {
                   foreach (var entity in DataContext.GetServiceClosedCallDetails(StartDate, EndDate, CallRemedyID, DepotIDList, StaffIDList, SiteIDList, SubCompanyID, JobID, AssetNo))
                   {
                       objcoll.Add(new ServiceClosedCallDetailsEntity()
                       {
                           Service_Received=entity.Service_Received,
                           Staff_Name_Received = entity.Staff_Name_Received,
                           Staff_Name_Cleared = entity.Staff_Name_Cleared,
                           Service_DownTime_HH_mm = entity.Service_DownTime_HH_mm,
                           Service_Cleared = entity.Service_Cleared,
                           Service_Job_Visit_No = entity.Service_Job_Visit_No,
                           Site_Name_Address = entity.Site_Name_Address,
                           Site_Code = entity.Site_Code,
                           Machine_Name_Stock_No = entity.Machine_Name_Stock_No,
                           Machine_Type_ID = entity.Machine_Type_ID,
                           Engineer_Name = entity.Engineer_Name,
                           SLA_Contract_Description = entity.SLA_Contract_Description,
                           Call_Description = entity.Call_Description,
                           Call_Remedy_Description = entity.Call_Remedy_Description,
                           Bar_Position_Name=entity.Bar_Position_Name,
                           Site_Postcode=entity.Site_Postcode,
                           TimeOpened = entity.TimeOpened,
                           Service_DownTime = entity.Service_DownTime,
                           Call_Remedy_ID = entity.Call_Remedy_ID,
                           Service_Allocated_Job_No = entity.Service_Allocated_Job_No,
                           Service_ID = entity.Service_ID,
                           Call_Status_ID = entity.Call_Status_ID,
                           Zone_ID = entity.Zone_ID,
                           Site_ID = entity.Site_ID,
                           Staff_ID = entity.Staff_ID,
                           Call_Group_ID = entity.Call_Group_ID,
                           Depot_ID = entity.Depot_ID
                       });
                   }
                   LogManager.WriteLog("End of GetServiceClosedCallDetails", LogManager.enumLogLevel.Info);

                   return objcoll;
               }
           }
           catch (Exception ex)
           {
               ExceptionManager.Publish(ex);
               throw ex;
           }

       }

       public ServiceEntity GetEditServiceDetail(int ServiceID, bool IsCallClosed)
       {
           try
           {
               LogManager.WriteLog("Inside LoadEditServiceDetail on service call edit", LogManager.enumLogLevel.Info);
               List<ServiceEntity> objcoll = new List<ServiceEntity>();

               using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
               {
                   foreach (var entity in DataContext.GetServiceCurrentCallDetailsByServiceID(ServiceID, IsCallClosed))
                   {
                       objcoll.Add(new ServiceEntity()
                       {
                           Service_ID = ServiceID,
                           Call_Fault_Additional_Notes=entity.Call_Fault_Additional_Notes,
                           Call_Fault_ID=entity.Call_Fault_ID,
                           Call_Group_ID=entity.Call_Group_ID,
                           Call_Remedy_Additional_Description=entity.Call_Remedy_Additional_Description,
                           Call_Remedy_ID=entity.Call_Remedy_ID,
                           Call_Source_ID=entity.Call_Source_ID,
                           Call_Status_ID=entity.Call_Status_ID,
                           Installation_ID=entity.Installation_ID,
                           Machine_Type_ID=entity.Machine_Type_ID,
                           Service_Acknowledged=entity.Service_Acknowledged,
                           Service_Additional_Work_Req=entity.Service_Additional_Work_Req,
                           Service_Allocated_Job_No=entity.Service_Allocated_Job_No,
                           Service_Arrived_At_Site=entity.Service_Arrived_At_Site,
                           Service_Cleared=entity.Service_Cleared,
                           Service_Issued=entity.Service_Issued,
                           Service_Issued_To_Staff_ID=entity.Service_Issued_To_Staff_ID,
                           Service_Received=entity.Service_Received,
                           Service_Visit_No=entity.Service_Visit_No,
                           Site_ID=entity.Site_ID,
                           SLA_Contract_ID = entity.SLA_Contract_ID,
                           Service_Received_Staff_ID = entity.Service_Received_Staff_ID,
                           Service_Issued_By_Staff_ID = entity.Service_Issued_By_Staff_ID,
                           Service_Cleared_Staff_ID = entity.Service_Cleared_Staff_ID
                       });
                   }
                   LogManager.WriteLog("End of LoadEditServiceDetail", LogManager.enumLogLevel.Info);
                   return objcoll[0];
               }
           }
           catch (Exception ex)
           {
               ExceptionManager.Publish(ex);
               throw ex;
           }
       }

       public List<ServiceNotesDisplayEntity> GetServiceNotesDisplay(int JobID)
       {
           try
           {
               LogManager.WriteLog("Inside GetServiceNotesDisplay on service call edit", LogManager.enumLogLevel.Info);
               List<ServiceNotesDisplayEntity> objcoll = new List<ServiceNotesDisplayEntity>();

               using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
               {
                   foreach (var entity in DataContext.GetServiceNotesByJobNo(JobID))
                   {
                       objcoll.Add(new ServiceNotesDisplayEntity()
                       {
                           Staff_Name=entity.Staff_Name,
                           Service_Notes_Notes=entity.Service_Notes_Notes,
                           Service_Notes_Date=entity.Service_Notes_Date,
                           Service_Notes_ID=entity.Service_Notes_ID
                       });
                   }
                   LogManager.WriteLog("End of GetServiceNotesDisplay", LogManager.enumLogLevel.Info);
                   return objcoll;
               }
           }
           catch (Exception ex)
           {
               ExceptionManager.Publish(ex);
               throw ex;
           }
       }

       public int InsertOrUpdateServiceCall(int serviceId, int siteId, int CallSourceID, int CallFaultID, int CallGroupID, int CallRemedyID,
           string MachineTypeID, int InstallationID, int ServiceVisitNo, int ServiceReceivedStaffID, 
           int ServiceIssuedToStaffID, int ServiceIssuedByStaffID, int CallStatusID,
          string CallFaultAdditionalNotes, string CallRemedyAdditionalDescription, DateTime ServiceReceived, int ServiceAllocatedJobNo = -1, bool IsCallClosed = false,
         DateTime? ServiceIssued = null, DateTime? ServiceAcknowledged = null, DateTime? ServiceArrivedAtSite = null, DateTime? ServiceCleared = null,
           int ServiceClearedStaffID = 0)
       {
           int result = 0;
                   // @ServiceId INT = 0,
                   // @SiteID INT,
                   // @CallSourceID INT,
                   // @CallFaultID INT,
                   // @CallGroupID INT,
                   // @CallRemedyID INT,
                   // @MachineTypeID INT,
                   // @InstallationID INT,
                   // @ServiceVisitNo INT = 1,
                   // @ServiceReceivedStaffID INT,
                   // @ServiceIssuedToStaffID INT,
                   // @ServiceIssuedByStaffID INT,
                   // @CallStatusID INT,
                   // @ServiceIssued DateTime,
                   // @ServiceReceived DateTime,
                   // @CallFaultAdditionalNotes Nvarchar,
                   // @CallRemedyAdditionalDescription Nvarchar,
                   // @BarPositionID INT = 0,
                   // @MachineID INT,
                   // @ServiceAllocatedJobNo INT

           try
           {
               LogManager.WriteLog("Inside InsertUpdateServiceCall on service call", LogManager.enumLogLevel.Info);

                   using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                   {
                       result = DataContext.InsertOrUpdateServiceCall(serviceId, siteId, CallSourceID, CallFaultID, CallGroupID, CallRemedyID,
                            MachineTypeID, InstallationID, ServiceVisitNo, ServiceReceivedStaffID, ServiceIssuedToStaffID, ServiceIssuedByStaffID,
                           CallStatusID, CallFaultAdditionalNotes, CallRemedyAdditionalDescription, ServiceAllocatedJobNo, IsCallClosed,
                           ServiceReceived, ServiceIssued, ServiceAcknowledged, ServiceArrivedAtSite, ServiceCleared, ServiceClearedStaffID);
                   }

               LogManager.WriteLog("End of InsertUpdateServiceCall", LogManager.enumLogLevel.Info);
           }
           catch (Exception ex)
           {
               ExceptionManager.Publish(ex);
               throw ex;
           }

           return result;
       }

       public int InsertServiceNotes(int jobId, int staffId, int engineerId, string subject, string notes, DateTime notesDate, 
           int notesInOut, int closedId = 0)
       {
           int result = 0;
           try
           {
               LogManager.WriteLog("Inside InsertServiceNotes on service call", LogManager.enumLogLevel.Info);

               using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
               {
                   result = DataContext.InsertServiceNotes(jobId, staffId, engineerId, subject, notes, notesDate, notesInOut, closedId);
               }

               LogManager.WriteLog("End of InsertServiceNotes", LogManager.enumLogLevel.Info);
           }
           catch (Exception ex)
           {
               ExceptionManager.Publish(ex);
               throw ex;
           }

           return result;
       }


       public void InsertNewAuditEntry(ModuleNameEnterprise moduleName, string desc, string Audit_Screen_Name)
       {
           try
           {
               //Calling Audit Method
               Audit_History AH = new Audit_History();
               //Populate required Values            
               AH.EnterpriseModuleName = moduleName;
               AH.Audit_Screen_Name = Audit_Screen_Name;
               AH.Audit_Desc = desc;
               AH.AuditOperationType = OperationType.ADD;
               AH.Audit_Field = string.Empty;
               AH.Audit_New_Vl = string.Empty;
               AH.Audit_Slot = string.Empty;
               AH.Audit_Old_Vl = string.Empty;

               AH.Audit_User_ID = CommonBiz.iUserId;
               AH.Audit_User_Name = CommonBiz.strUsername;

               AuditViewerBusiness AVB = new AuditViewerBusiness(Common.Utilities.DatabaseHelper.GetConnectionString());
               AVB.InsertAuditData(AH, true);

           }
           catch (Exception ex)
           {
               LogManager.WriteLog("Error in InsertNewAuditEntry Method" + "-Error Message-" + ex.Message, LogManager.enumLogLevel.Error);
           }
       }


       public void AuditUpdateServiceCall(ServiceEntity objOldData, ServiceEntity objNewData, string screenName)
       {
           try
           {
               Type type = typeof(ServiceEntity);
               PropertyInfo[] properties = type.GetProperties();
               foreach (PropertyInfo property in properties)
               {
                   string prevValue = (property.GetValue(objOldData, null) == null) ? "" : property.GetValue(objOldData, null).ToString();
                   string currValue = (property.GetValue(objNewData, null) == null) ? "" : property.GetValue(objNewData, null).ToString();

                   if (currValue != prevValue)
                   {
                       string fieldName = "";
                       ColumnAttribute colAttr = (ColumnAttribute) Attribute.GetCustomAttribute(property, typeof (ColumnAttribute));

                       if (colAttr != null)
                       {
                           fieldName = colAttr.Storage;
                           AuditModifiedData(objNewData.Service_Allocated_Job_No.Value, fieldName, prevValue, currValue, screenName);
                       }
                   }
               }
           }
           catch (Exception ex)
           {
               LogManager.WriteLog("Error in AuditUpdateServiceCall Method" + "-Error Message-" + ex.Message, LogManager.enumLogLevel.Error);
           }
       }

       public void AuditModifiedData(int jobId, string sField, string sPrevValue, string sNewValue,string screenName)
       {
           try
           {
               string ss = "sss";
               Audit_History AH = new Audit_History();
               AH.EnterpriseModuleName = ModuleNameEnterprise.ServiceCalls;
               AH.Audit_Screen_Name = screenName;
               AH.Audit_Desc = "Call Details modified for call : " + jobId.ToString() +" [" + sField + "] : " + sPrevValue + " -> " + sNewValue;
               AH.AuditOperationType = OperationType.MODIFY;
               AH.Audit_Field = sField;
               AH.Audit_User_ID = CommonBiz.iUserId;
               AH.Audit_User_Name = CommonBiz.strUsername;
               AH.Audit_New_Vl =  sNewValue;     //current value
               AH.Audit_Old_Vl =  sPrevValue;    // previous value
               AH.Audit_Slot = string.Empty;

               AuditViewerBusiness AVB = new AuditViewerBusiness(Common.Utilities.DatabaseHelper.GetConnectionString());
               AVB.InsertAuditData(AH, true);
           }

           catch (Exception ex)
           {
               throw;
           }
       }

    }

}
