using Audit.BusinessClasses;
using Audit.Transport;
using BMC.Common.LogManagement;
using BMC.EnterpriseBusiness.Entities;
using BMC.EnterpriseDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.EnterpriseBusiness.Business
{
    public class ServiceAdminBusiness
    {
        private static ServiceAdminBusiness _ServiceAdminBiz;

        public static ServiceAdminBusiness CreateInstance()
        {
            if (_ServiceAdminBiz == null)
                _ServiceAdminBiz = new ServiceAdminBusiness();

            return _ServiceAdminBiz;
        }

        public List<CallGroup> GetCallGroups()
        {
            List<CallGroup> callGroups = null;
            try
            {
                List<rsp_GetCallGroupsResult> groups;
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    groups = DataContext.rsp_GetCallGroups().ToList();
                }

                callGroups = (from grp in groups
                          select new CallGroup
                          {
                              Id = grp.Call_Group_ID,
                              Description = grp.Call_Group_Description,
                              Reference = grp.Call_Group_Reference,
                              Downtime = grp.Call_Group_Downtime.HasValue ?  grp.Call_Group_Downtime.Value :  false,
                              LogEngineerChange = grp.Call_Group_Log_Engineer_Change.HasValue ? grp.Call_Group_Log_Engineer_Change.Value : false,
                              EndDate = grp.Call_Group_End_Date 
                          }).ToList<CallGroup>();
            }
            catch (Exception ex)
            {
                throw;
            }
            return callGroups;
        }

        public List<CallFault> GetCallFaultsByGroupId(int groupID)
        {
            List<CallFault> callFaults = null;
            try
            {
                List<rsp_GetCallFaultsByGroupIDResult> faults;
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    faults = DataContext.rsp_GetCallFaultsByGroupID(groupID).ToList();
                }

                callFaults = (from flt in faults
                              select new CallFault
                              {
                                  Id = flt.Call_Fault_ID,
                                  GroupId = flt.Call_Group_ID,
                                  Description = flt.Call_Fault_Description,
                                  Reference = flt.Call_Fault_Reference,
                                  EndDate = flt.Call_Fault_End_Date
                              }).ToList<CallFault>();
            }
            catch (Exception ex)
            {
                throw;
            }
            return callFaults;
        }

        public List<CallRemedy> GetCallFixCodes()
        {
            List<CallRemedy> fixCodes = null;
            try
            {
                List<rsp_GetCallRemedyResult> codes;
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    codes = DataContext.rsp_GetCallRemedy().ToList();
                }

                fixCodes = (from obj in codes
                            select new CallRemedy
                              {
                                  Id = obj.Call_Remedy_ID,
                                  Description = obj.Call_Remedy_Description,
                                  Reference = obj.Call_Remedy_Reference,
                                  Downtime = obj.Call_Remedy_Attract_Downtime.HasValue ? obj.Call_Remedy_Attract_Downtime.Value : false,
                                  EndDate = obj.Call_Remedy_End_Date
                              }).ToList<CallRemedy>();
            }
            catch (Exception ex)
            {
                throw;
            }
            return fixCodes;
        }


        public List<CallSource> GetCallSources()
        {
            List<CallSource> callSources = null;
            try
            {
                List<rsp_GetCallSourceResult> sources;
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    sources = DataContext.rsp_GetCallSource().ToList();
                }

                callSources = (from src in sources
                              select new CallSource
                              {
                                  Id = src.Call_Source_ID,
                                  Description = src.Call_Source_Description,
                                  Reference = src.Call_Source_Reference,
                              }).ToList<CallSource>();
            }
            catch (Exception ex)
            {
                throw;
            }
            return callSources;
        }


        public bool InsertUpdateCallGroup(CallGroup callGroup)
        {
            bool result = false;
            try
            {                
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                     int? groupIdOut = 0;
                     DataContext.usp_InsertOrUpdateCallGroup(callGroup.Id, callGroup.Description, callGroup.Reference, callGroup.Downtime, callGroup.LogEngineerChange, callGroup.EndDate, ref groupIdOut);

                     if (groupIdOut.Value >= 0)
                         result = true;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error in InsertUpdateCallGroup Method in ServiceAdminBusiness" + "-Error Message-" + ex.Message, LogManager.enumLogLevel.Error);
            }
            return result;
        }

        public bool InsertUpdateCallFault(CallFault callFault)
        {
            bool result = false;
            try
            {
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    int? faultIdOut = 0;
                    DataContext.usp_InsertOrUpdateCallFault(callFault.Id, callFault.GroupId, callFault.Description, callFault.Reference, callFault.EndDate, ref faultIdOut);

                    if (faultIdOut.Value >= 0)
                        result = true;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error in InsertUpdateCallFault Method in ServiceAdminBusiness" + "-Error Message-" + ex.Message, LogManager.enumLogLevel.Error);
            }
            return result;
        }

        public bool InsertUpdateFixCode(CallRemedy callRemedy)
        {
            bool result = false;
            try
            {
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    int? remedyIdOut = 0;
                    DataContext.usp_InsertOrUpdateCallRemedy(callRemedy.Id, callRemedy.Description, callRemedy.Reference, callRemedy.Downtime, callRemedy.EndDate, ref remedyIdOut);

                    if (remedyIdOut.Value >= 0)
                        result = true;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error in InsertUpdateFixCode Method in ServiceAdminBusiness" + "-Error Message-" + ex.Message, LogManager.enumLogLevel.Error);
            }
            return result;
        }

        public bool InsertUpdateCallSource(CallSource callSource)
        {
            bool result = false;
            try
            {
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    int? sourceIdOut = 0;
                    DataContext.usp_InsertOrUpdateCallSource(callSource.Id, callSource.Description, callSource.Reference, ref sourceIdOut);

                    if (sourceIdOut.Value >= 0)
                        result = true;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error in InsertUpdateCallSource Method in ServiceAdminBusiness" + "-Error Message-" + ex.Message, LogManager.enumLogLevel.Error);
            }
            return result;
        }

        public bool CanRemoveCallFaultGroup(int groupId)
        {
            bool result = false;

            try
            {
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    bool? retVal = null;
                    DataContext.rsp_CanRemoveCallGroup(groupId, ref retVal);

                    result = retVal.Value;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error in CanRemoveCallFaultGroup Method in ServiceAdminBusiness" + "-Error Message-" + ex.Message, LogManager.enumLogLevel.Error);
            }
           return result;
        }

        public bool CanRemoveCallFaultDetail(int faultId, int groupId)
        {
            bool result = false;

            try
            {
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    bool? retVal = null;
                    DataContext.rsp_CanRemoveCallFault(faultId, groupId , ref retVal);

                    result = retVal.Value;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error in CanRemoveCallFaultDetail Method in ServiceAdminBusiness" + "-Error Message-" + ex.Message, LogManager.enumLogLevel.Error);
            }
            return result;
        }

        public bool CanRemoveCallRemedy(int remedyId)
        {
            bool result = false;

            try
            {
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    bool? retVal = null;
                    DataContext.rsp_CanRemoveCallRemedy(remedyId, ref retVal);

                    result = retVal.Value;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error in CanRemoveCallRemedy Method in ServiceAdminBusiness" + "-Error Message-" + ex.Message, LogManager.enumLogLevel.Error);
            }
            return result;
        }

        public bool CanRemoveCallSource(int sourceId)
        {
            bool result = false;

            try
            {
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    bool? retVal = null;
                    DataContext.rsp_CanRemoveCallSource(sourceId, ref retVal);

                    result = retVal.Value;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error in CanRemoveCallSource Method in ServiceAdminBusiness" + "-Error Message-" + ex.Message, LogManager.enumLogLevel.Error);
            }
            return result;
        }

        public void InsertNewAuditEntry(ModuleNameEnterprise moduleName, string callType, string desc)
        {
            try
            {
                //Calling Audit Method
                Audit_History AH = new Audit_History();
                //Populate required Values            
                AH.EnterpriseModuleName = moduleName;
                AH.Audit_Screen_Name = "Service Admin";
                AH.Audit_Desc = "Added Service " + callType + " - [" + desc + "]";
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
                LogManager.WriteLog("Error in InsertNewAuditEntry Method in ServiceAdminBusiness" + "-Error Message-" + ex.Message, LogManager.enumLogLevel.Error);
            }
        }

        public void AuditUpdateCallGroup(CallGroup objOldData, CallGroup objNewData)
        {
            try
            {
                if ((objOldData == null) || (objNewData == null)) return;

                string type = "Call Group";
                if (objOldData.EndDate != objNewData.EndDate)
                    AuditModifiedData(type, "Call_Group_Reference", objOldData.Reference.ToString(), objNewData.Reference.ToString(), true);

                else
                {
                    if (objOldData.Reference != objNewData.Reference)
                        AuditModifiedData(type, "Call_Group_Reference", objOldData.Reference.ToString(), objNewData.Reference.ToString());

                    if (objOldData.Description != objNewData.Description)
                        AuditModifiedData(type, "Call_Group_Description", objOldData.Description.ToString(), objNewData.Description.ToString());

                    if (objOldData.Downtime != objNewData.Downtime)
                        AuditModifiedData(type, "Call_Group_Downtime", objOldData.Downtime.ToString(), objNewData.Downtime.ToString());

                    if (objOldData.LogEngineerChange != objNewData.LogEngineerChange)
                        AuditModifiedData(type, "Call_Group_Log_Engineer_Change", objOldData.LogEngineerChange.ToString(), objNewData.LogEngineerChange.ToString());
                }
            }

            catch (Exception ex)
            {
                LogManager.WriteLog("Error in AuditUpdateCallGroup Method in ServiceAdminBusiness" + "-Error Message-" + ex.Message, LogManager.enumLogLevel.Error);
            }
        }

        public void AuditUpdateCallFault(CallFault objOldData, CallFault objNewData)
        {
            try
            {
                if ((objOldData == null) || (objNewData == null)) return;

                string type = "Call Fault";
                if (objOldData.EndDate != objNewData.EndDate)
                    AuditModifiedData(type, "Call_Fault_Reference", objOldData.Reference.ToString(), objNewData.Reference.ToString(), true);

                else
                {
                    if (objOldData.Reference != objNewData.Reference)
                        AuditModifiedData(type, "Call_Fault_Reference", objOldData.Reference.ToString(), objNewData.Reference.ToString());

                    if (objOldData.Description != objNewData.Description)
                        AuditModifiedData(type, "Call_Fault_Description", objOldData.Description.ToString(), objNewData.Description.ToString());
                }
            }

            catch (Exception ex)
            {
                LogManager.WriteLog("Error in AuditUpdateCallFault Method in ServiceAdminBusiness" + "-Error Message-" + ex.Message, LogManager.enumLogLevel.Error);
            }
        }

        public void AuditUpdateCallRemedy(CallRemedy objOldData, CallRemedy objNewData)
        {
            try
            {
                if ((objOldData == null) || (objNewData == null)) return;

                string type = "Call Remedy";
                if (objOldData.EndDate != objNewData.EndDate)
                    AuditModifiedData(type, "Call_Remedy_Reference", objOldData.Reference.ToString(), objNewData.Reference.ToString(), true);

                else
                {
                    if (objOldData.Reference != objNewData.Reference)
                        AuditModifiedData(type, "Call_Remedy_Reference", objOldData.Reference.ToString(), objNewData.Reference.ToString());

                    if (objOldData.Description != objNewData.Description)
                        AuditModifiedData(type, "Call_Remedy_Description", objOldData.Description.ToString(), objNewData.Description.ToString());

                    if (objOldData.Downtime != objNewData.Downtime)
                        AuditModifiedData(type, "Call_Remedy_Attract_Downtime", objOldData.Downtime.ToString(), objNewData.Downtime.ToString());
                }
            }

            catch (Exception ex)
            {
                LogManager.WriteLog("Error in AuditUpdateCallRemedy Method in ServiceAdminBusiness" + "-Error Message-" + ex.Message, LogManager.enumLogLevel.Error);
            }
        }

        public void AuditUpdateCallSource(CallSource objOldData, CallSource objNewData)
        {
            try
            {
                if ((objOldData == null) || (objNewData == null)) return;

                string type = "Call Source";
              
                if (objOldData.Reference != objNewData.Reference)
                    AuditModifiedData(type, "Call_Source_Reference", objOldData.Reference.ToString(), objNewData.Reference.ToString());

                if (objOldData.Description != objNewData.Description)
                    AuditModifiedData(type, "Call_Source_Description", objOldData.Description.ToString(), objNewData.Description.ToString());                
            }

            catch (Exception ex)
            {
                LogManager.WriteLog("Error in AuditUpdateCallSource Method in ServiceAdminBusiness" + "-Error Message-" + ex.Message, LogManager.enumLogLevel.Error);
            }
        }

        public void AuditModifiedData(string type, string sField, string sPrevValue, string sNewValue, bool isSoftDelete = false)
        {
            try
            {
                Audit_History AH = new Audit_History();
                AH.EnterpriseModuleName = ModuleNameEnterprise.ServiceCalls;
                AH.Audit_Screen_Name = "Service Admin";
                if(!isSoftDelete)
                    AH.Audit_Desc = "Modified Service " + type +  " - [" + sField + "] : " + sPrevValue + " -> " + sNewValue;
                else
                    AH.Audit_Desc = "Removed Service " + type + " - [" + sField + "] : " + sPrevValue;
                AH.AuditOperationType = OperationType.MODIFY;
                AH.Audit_Field = isSoftDelete ? string.Empty : sField;
                AH.Audit_User_ID = CommonBiz.iUserId;
                AH.Audit_User_Name = CommonBiz.strUsername;
                AH.Audit_New_Vl = isSoftDelete ? string.Empty : sNewValue;     //current value
                AH.Audit_Old_Vl = isSoftDelete ? string.Empty : sPrevValue;    // previous value
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
