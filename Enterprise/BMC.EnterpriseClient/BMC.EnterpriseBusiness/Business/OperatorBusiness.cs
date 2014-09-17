using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.Common.LogManagement;
using BMC.EnterpriseDataAccess;
using BMC.Common.ExceptionManagement;
using Audit.BusinessClasses;
using Audit.Transport;


namespace BMC.EnterpriseBusiness.Business
{
    public class OperatorBusiness
    {
        private static OperatorBusiness _operator;

        #region Constructor
        public OperatorBusiness()
        {

        }
        #endregion

        #region 

        public static OperatorBusiness CreateInstance()
        {
            if (_operator == null)
                _operator = new OperatorBusiness();

            return _operator;
        }
        
        #endregion 

        #region Data Loading Methods

        public List<OperatorEntity> Operator_LoadOperator()
        {
            try
            {
                LogManager.WriteLog("Loading the Operators", LogManager.enumLogLevel.Info);
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
                return null;
            }
        }
        
        public List<OperatorEntity> Operator_LoadOperatorDetails(int? OperatorId)
        {
            try
            {
                LogManager.WriteLog("Loading Operator details", LogManager.enumLogLevel.Info);
                List<OperatorEntity> obcoll = new List<OperatorEntity>();
                List<rsp_GetOperatorDetailsResult> operatorlist;
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    operatorlist = DataContext.GetOperatorDetails(OperatorId).ToList();
                }
                obcoll = (from obj in operatorlist
                          select new OperatorEntity
                          {
                              Operator_ID = obj.Operator_ID,
                              Operator_Name = obj.Operator_Name,
                              Operator_Invoice_Name = obj.Operator_Invoice_Name,
                              Operator_Address = obj.Operator_Address,
                              Operator_Invoice_Address = obj.Operator_Invoice_Address,
                              Operator_PostCode = obj.Operator_PostCode,
                              Operator_Invoice_Postcode = obj.Operator_Invoice_Postcode,
                              Operator_Depot_Phone = obj.Operator_Depot_Phone,
                              Operator_EMail = obj.Operator_EMail,
                              Operator_Fax = obj.Operator_Fax,
                              Operator_Contact = obj.Operator_Contact
                          }).ToList<OperatorEntity>();
                return obcoll;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return null;
            }
        }

        public bool IsOperatorExists(string OperatorName, int OperatorId)
        {
            try
            {
                LogManager.WriteLog("BIZ-IsOperatorExists()_ Checking for the same operator", LogManager.enumLogLevel.Info);
                EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext();
				return (bool)DataContext.IsOperatorExists(OperatorName, OperatorId);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return false;
            }
        }
        
        #endregion

        #region Data Update Methods

        public void Operator_UpdateOperatorDetails(int OperatorID, int CalendarID, string OperatorName, string OperatorAddress, string OperatorPostcode, string OperatorPhoneNo, string OperatorFax, string OperatorEmail, string OperatorContact, string OperatorInvoiceAddress, string OperatorInvoicePostcode, string OperatorInvoiceName, string OperatorStartDate, string OperatorEndDate, string OperatorAMEDIS_Code, string OperatorLogoReference, string OperatorAccountName, string OperatorSortCode, string OperatorAccountNo)
        {
            try
            {
                LogManager.WriteLog("Updating the Operator Details" + OperatorID + OperatorName, LogManager.enumLogLevel.Info);
                EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext();
                DataContext.UpdateOperatorDetails(OperatorID, CalendarID, OperatorName, OperatorAddress, OperatorPostcode, OperatorPhoneNo, OperatorFax, OperatorEmail, OperatorContact, OperatorInvoiceAddress, OperatorInvoicePostcode, OperatorInvoiceName, OperatorStartDate, OperatorEndDate, OperatorAMEDIS_Code, OperatorLogoReference, OperatorAccountName, OperatorSortCode, OperatorAccountNo);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        public int Operator_DeleteOperator(int OperatorId)
        {
            int retval = -1;
            try
            {
                LogManager.WriteLog("Deleting Operator" + OperatorId, LogManager.enumLogLevel.Info);
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    retval = DataContext.DeleteOperatorDetails(OperatorId);
                }

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return -1;
            }
            return retval;
        }
        
        #endregion

        #region OperatorAuditing

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

        public void AuditModifiedDataForOperator(string sOperatorName, string sField, string sPrevValue, string sNewValue, int iUserId, string strUsername)
        {
            try
            {
                Audit_History AH = new Audit_History();
                AH.EnterpriseModuleName = ModuleNameEnterprise.Operators;
                AH.Audit_Screen_Name = "Operator";
                AH.Audit_Desc = "Operator " + sOperatorName + " modified  ..[" + sField + "] : " + sPrevValue + " -> " + sNewValue;
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

        public void InsertDeteleAuditEntry(ModuleNameEnterprise moduleName, string strScreenName, string strField, string strNewItem, int iUserId, string strUsername)
        {
            try
            {
                //Calling Audit Method
                Audit_History AH = new Audit_History();
                //Populate required Values            
                AH.EnterpriseModuleName = moduleName;
                AH.Audit_Screen_Name = strScreenName;
                AH.Audit_Desc = "Record [" + strNewItem + "] added to " + strScreenName;
                AH.AuditOperationType = OperationType.DELETE;
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
        
        #endregion
    }
}
