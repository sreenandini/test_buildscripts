using System;
using System.Collections.Generic;
using Audit.BusinessClasses;
using Audit.Transport;
using BMC.Common.ExceptionManagement;
using BMC.EnterpriseBusiness.Entities.ServiceCalls;
using BMC.EnterpriseDataAccess;

namespace BMC.EnterpriseBusiness.Business.ServiceCalls
{
    public class GMUFaultBusiness
    {
        EnterpriseDataContext datacontext = null;
        private static GMUFaultBusiness _GMUFault = null;

        public static GMUFaultBusiness CreateInstance()
        {
            if (_GMUFault == null)
                _GMUFault = new GMUFaultBusiness();

            return _GMUFault;
        }

        public int SaveNewFault(string sFaultName, int iIsToMail)
        {
            try
            {
                int result = 0;
                
                EnterpriseDataContext datacontext = EnterpriseDataContextHelper.GetDataContext();
                result = datacontext.UpdateServiceCallDetails(sFaultName, iIsToMail);
                var rr = datacontext.GetAllFaultGroup();
                return result;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return 0;
        }

        public List<FaultGroupEntity> GetAllFaultGroupDescription()
        {
            try
            {
                EnterpriseDataContext datacontext = EnterpriseDataContextHelper.GetDataContext();
                var allFaults = datacontext.GetAllFaultGroup();
                if (allFaults != null)
                {
                    List<FaultGroupEntity> allFaultsInEntity = new List<FaultGroupEntity>();
                    allFaultsInEntity.Add(new FaultGroupEntity { FaultGroup_ID = 0, Fault_Group_Description = "--NONE--" });
                    foreach (rsp_GetAllFaultGroupResult r in allFaults)
                    {
                        allFaultsInEntity.Add(new FaultGroupEntity { Fault_Group_Description = r.Call_Group_Description, FaultGroup_ID = r.Call_Group_ID });
                    }
                    return allFaultsInEntity;
                }
                return null;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return null;
        }

        public List<FaultGroupChildEntity> GetAllFaultsByGroupID(int groupId)
        {
            try
            {
                EnterpriseDataContext datacontext = EnterpriseDataContextHelper.GetDataContext();
                var allFaults = datacontext.GetAllFaultsByGroup(groupId);
                if (allFaults != null)
                {
                    List<FaultGroupChildEntity> allFaultsInEntity = new List<FaultGroupChildEntity>();
                    //allFaultsInEntity.Add(new  FaultGroupEntity { FaultGroup_ID = 0, Fault_Group_Description = "--NONE--" });
                    foreach (rsp_GetAllFaultsByGroupResult r in allFaults)
                    {
                        allFaultsInEntity.Add(new FaultGroupChildEntity { Fault_Description = r.Call_Fault_Description, Fault_ID = r.Call_Fault_ID });
                    }
                    return allFaultsInEntity;
                }
                return null;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return null;
        }

        public List<FaultEventEntity> GetAllFaultEvents()
        {
            try
            {
                List<FaultEventEntity> allFaultEvents = new List<FaultEventEntity>();
                allFaultEvents.Add(new FaultEventEntity { Fault_Event_ID = 1, Fault_Event_Description = "Door" });
                allFaultEvents.Add(new FaultEventEntity { Fault_Event_ID = 2, Fault_Event_Description = "Communications" });
                allFaultEvents.Add(new FaultEventEntity { Fault_Event_ID = 3, Fault_Event_Description = "Power" });
                allFaultEvents.Add(new FaultEventEntity { Fault_Event_ID = 4, Fault_Event_Description = "General Fault" });

                return allFaultEvents;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return null;
        }

        public List<GMUConfigurationEntity> GetAllGMUConfigurations()
        {
            try
            {
                EnterpriseDataContext datacontext = EnterpriseDataContextHelper.GetDataContext();
                var allGMUConfiguration = datacontext.GetAllGMUConfiguration();
                if (allGMUConfiguration != null)
                {
                    List<GMUConfigurationEntity> allGMUConfigurationsInEntity = new List<GMUConfigurationEntity>();

                    foreach (rsp_GetAllGMUConfigurationResult GMUConfiguration in allGMUConfiguration)
                    {
                        allGMUConfigurationsInEntity.Add(new GMUConfigurationEntity
                        {
                            Description = GMUConfiguration.Description,
                            CloseServiceCall = GMUConfiguration.CloseServiceCall,
                            Code = GMUConfiguration.Code,
                            subcode = GMUConfiguration.subcode,
                            CreateServiceCall = GMUConfiguration.CreateServiceCall,
                            Fault = GMUConfiguration.Fault,
                            ToMail = GMUConfiguration.ToMail,
                            SourceID = GMUConfiguration.SourceID,
                            SourceProtocol = GMUConfiguration.SourceProtocol,
                            Type = GMUConfiguration.Type,
                            Call_Fault_ID = GMUConfiguration.Call_Fault_ID,
                            Datapak_Fault_ID = GMUConfiguration.Datapak_Fault_ID,
                            Mail_CC = GMUConfiguration.Mail_CC,
                            Mail_TO = GMUConfiguration.Mail_TO,
                            Call_Group_ID = GMUConfiguration.Call_Group_ID
                        });
                    }
                    return allGMUConfigurationsInEntity;
                }
                
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return null;
        }

        public int UpdateMailConfigurations(int Fault_ID, string To_Address, string CC_Address)
        {
            try
            {
                EnterpriseDataContext datacontext = EnterpriseDataContextHelper.GetDataContext();
                return datacontext.UpdateMailDetailsInGMUConfiguration(Fault_ID, To_Address, CC_Address);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return 0;
        }

        public int UpdateGMUConfiguration(GMUConfigurationEntity objUpdatedEntity)
        {
            try
            {
                EnterpriseDataContext datacontext = EnterpriseDataContextHelper.GetDataContext();
                return datacontext.UpdateGMUConfiguration(objUpdatedEntity.Description, objUpdatedEntity.CreateServiceCall, objUpdatedEntity.CloseServiceCall, objUpdatedEntity.ToMail, objUpdatedEntity.Fault, objUpdatedEntity.Type, objUpdatedEntity.Datapak_Fault_ID, objUpdatedEntity.Call_Fault_ID);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return 0;
        }

        public void InsertNewAuditEntry(ModuleNameEnterprise moduleName, string strScreenName, string strField, string strNewItem)
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

                AH.Audit_User_ID = CommonBiz.iUserId;
                AH.Audit_User_Name = CommonBiz.strUsername;

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
