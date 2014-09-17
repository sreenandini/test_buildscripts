using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BMC.Common.LogManagement;
using BMC.EnterpriseBusiness.Business;
using BMC.EnterpriseBusiness.Entities;
using BMC.Common.ExceptionManagement;
using BMC.Common.Utilities;
using BMC.CoreLib.Win32;
using Audit.Transport;
using BMC.Common;
namespace BMC.EnterpriseClient.Views
{
    public partial class frmOperator : BMC.EnterpriseClient.Helpers.BMCExtendedForm
    {
        #region Private Variables
        bool ViewOperatorEditPermission = AppGlobals.Current.HasUserAccess("HQ_Admin_Operator_Edit");
        int? _Operator_ID;
        public List<OperatorEntity> lstExistingOperator = null;
        public int iUserID = 0;
        public string strUserName = "";
        BMC.EnterpriseClient.Helpers.Datawatcher objDatawatcher = null;


        #endregion

        #region Constructor

        public frmOperator()
        {
            InitializeComponent();
            SetTagProperty();
            iUserID = AppGlobals.Current.UserId;
            strUserName = AppGlobals.Current.UserName;
            objDatawatcher = new Helpers.Datawatcher(this,
                 (w, f) =>
                 {
                     w.RemoveControlFromWatcher((f as frmOperator).lstOperators);
                    
                 });   
        }

        public frmOperator(int Operator_ID)
        {
            InitializeComponent();
            SetTagProperty();
            _Operator_ID = Operator_ID;
            objDatawatcher = new Helpers.Datawatcher(this,
                (w, f) =>
                {
                    w.RemoveControlFromWatcher((f as frmOperator).lstOperators);

                });   
            objDatawatcher.DataModify = false;
        }
        #endregion

        #region Validation Methods

        private bool bOperatorAlreadyExists(string sOperatorName, int OperatorId)
        {
            bool result;
            try
            {
                LogManager.WriteLog("bOperatorAlreadyExists(),Checking whether The operator name alreasdy exists", LogManager.enumLogLevel.Info);
                result = OperatorBusiness.CreateInstance().IsOperatorExists(sOperatorName, OperatorId);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                result = false;
            }
            return result;
        }

        private bool ValidateControls(ref string ErrorMsg)
        {
            bool retVal = true;

            //if (txt_OperatorPostcode.Text != "")
            //{
            //    if (!txt_OperatorPostcode.Text.IsNumeric())
            //    {
            //        ErrorMsg = "The PostalCode is not in correct format.";
            //        txt_OperatorPostcode.Focus();
            //        retVal = false;
            //    }
            //}
            //if (txt_OperatorInvoicePostcode.Text != "")
            //{
            //    if (!txt_OperatorInvoicePostcode.Text.IsNumeric())
            //    {
            //        ErrorMsg = "The Invoice PostalCode is not in correct format.";
            //        txt_OperatorInvoicePostcode.Focus();
            //        retVal = false;
            //    }
            //}
            if (txt_OperatorEmail.Text != "")
            {
                if (!txt_OperatorEmail.Text.IsValidEmail())
                {
                    ErrorMsg = this.GetResourceTextByKey(1,"MSG_INVALID_OP_EMAILID");
                    txt_OperatorEmail.Focus();
                    retVal = false;
                }
            }
            //if (txt_OperatorPhone.Text != "")
            //{
            //    if (!(txt_OperatorPhone.Text.IsLengthGreaterThanZero()))// && NewUser)
            //    {
            //        ErrorMsg = "Please Enter Valid Operator Phone Number ";
            //        txt_OperatorPhone.Focus();
            //        retVal = false;
            //    }
            //}
            return retVal;
        }

        #endregion

        #region Data Load Methods

        private void LoadOperator()
        {
            try
            {
                LogManager.WriteLog("LoadOperator(),Get the Operator", LogManager.enumLogLevel.Info);
                lstOperators.DataSource = null;
                lstOperators.Items.Clear();
                lstOperators.DisplayMember = "Operator_Name";
                lstOperators.ValueMember = "Operator_ID";

                List<OperatorEntity> lst_Operator = OperatorBusiness.CreateInstance().Operator_LoadOperator();//
                lstExistingOperator = lst_Operator;
                lstOperators.DataSource = lst_Operator;
                if (lstOperators.Items.Count == 0)
                {
                    Win32Extensions.ShowInfoMessageBox(this,this.GetResourceTextByKey(1, "MSG_ADD_NEW_OPERATOR"), this.Text);
                    btn_Delete.Visible = false;
                    btn_Update.Visible = false;
                    EnableAndDisable(false);
                }
                else
                {
                    btn_Delete.Visible = true;
                    btn_Update.Visible = true;
                    btn_AddNew.Visible = true;
                    lstOperators.SelectedIndex = 0;
                }
                btn_AddNew.Enabled = btn_Update.Enabled = btn_Delete.Enabled = ViewOperatorEditPermission;
                EnableAndDisable(!ViewOperatorEditPermission);
                if (btn_AddNew.Tag == "Key_AddCaption" && btn_Update.Visible == false)
                {
                    EnableAndDisable(true);
                }
                else EnableAndDisable(false);
                LogManager.WriteLog("Loading Operators Completed", LogManager.enumLogLevel.Info);
                objDatawatcher.DataModify = false;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void LoadOperatorDetails(int OperatorId)
        {
            try
            {
                LogManager.WriteLog("LoadOperatorDetails(),Get the Operator details of selected operator", LogManager.enumLogLevel.Info);
                List<OperatorEntity> ltOperator = OperatorBusiness.CreateInstance().Operator_LoadOperatorDetails(OperatorId);
                if (ltOperator.Count > 0)
                {
                    OperatorEntity OperatorDetails = ltOperator[0];
                    txt_OperatorName.Text = OperatorDetails.Operator_Name;
                    txt_OperatorInvoiceName.Text = OperatorDetails.Operator_Invoice_Name;
                    txt_OperatorAddress.Text = OperatorDetails.Operator_Address;
                    txt_OperatorInvoiceAddress.Text = OperatorDetails.Operator_Invoice_Address;
                    txt_OperatorPostcode.Text = OperatorDetails.Operator_PostCode;
                    txt_OperatorInvoicePostcode.Text = OperatorDetails.Operator_Invoice_Postcode;
                    txt_OperatorPhone.Text = OperatorDetails.Operator_Depot_Phone;
                    txt_OperatorEmail.Text = OperatorDetails.Operator_EMail;
                    txt_OperatorFax.Text = OperatorDetails.Operator_Fax;
                    txt_OperatorContactName.Text = OperatorDetails.Operator_Contact;
                    LogManager.WriteLog("Loading Operators details Completed", LogManager.enumLogLevel.Info);
                    objDatawatcher.DataModify = false;
                }
                objDatawatcher.DataModify = false;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

        }

        #endregion

        #region Data Update Methods

        public void UpdateOperatorDetails(bool bnew)
        {
            LogManager.WriteLog("UpdateOperatorDetails(),Updating the Operator Details", LogManager.enumLogLevel.Info);
            OperatorBusiness.CreateInstance().Operator_UpdateOperatorDetails(bnew ? Convert.ToInt32(lstOperators.SelectedValue) : 0, 0, txt_OperatorName.Text.Trim(), txt_OperatorAddress.Text.Trim(), txt_OperatorPostcode.Text.Trim(), txt_OperatorPhone.Text.Trim(), txt_OperatorFax.Text.Trim(), txt_OperatorEmail.Text.Trim(), txt_OperatorContactName.Text.Trim(), txt_OperatorInvoiceAddress.Text.Trim(), txt_OperatorInvoicePostcode.Text.Trim(), txt_OperatorInvoiceName.Text.Trim(), DateTime.Now.ToShortDateString(), "", "", "", "", "", "");
            objDatawatcher.DataModify = false;
        }

        public bool DeleteOperator(int OperatorId)
        {
            bool retVal = false;
            try
            {
                LogManager.WriteLog("DeleteOperator(),Deteting the selected Operator" + OperatorId, LogManager.enumLogLevel.Info);
                if (OperatorBusiness.CreateInstance().Operator_DeleteOperator(OperatorId) >= 0)
                {
                    retVal = true;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return retVal;
        }

        #endregion

        #region Event Methods

        private void Operator_Load(object sender, EventArgs e)
        {
            try
            {
                this.ResolveResources();
                LogManager.WriteLog("Operator_Load(),Load Operator", LogManager.enumLogLevel.Info);
                LoadOperator();
                objDatawatcher.DataModify = false;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void lbOperator_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int OperatorId = Convert.ToInt32(lstOperators.SelectedValue);
                LogManager.WriteLog("lbOperator_SelectedIndexChanged(),Selected OperatorID is" + OperatorId, LogManager.enumLogLevel.Info);
                if (OperatorId != 0)
                {
                    LoadOperatorDetails(OperatorId);
                }
                objDatawatcher.DataModify = false;
            }
            catch (Exception  ex)
            {
                
                 ExceptionManager.Publish(ex);
            }
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            try
            {
                if (btn_AddNew.Tag == "Key_AddCaption")
                {
                    LogManager.WriteLog("btnAddNew_Click(),Adding new Operator", LogManager.enumLogLevel.Info);
                    ClearContents();
                    txt_OperatorName.Focus();
                    lstOperators.Enabled = false;
                    EnableAndDisable(false);
                    btn_Delete.Visible = false;
                    btn_Update.Visible = true;
                    btn_AddNew.Text = this.GetResourceTextByKey("Key_CancelCaption");
                    btn_AddNew.Tag = "Cancel";
                }
                else
                {
                    ClearContents();
                    btn_Delete.Visible = lstOperators.Enabled = true;
                    btn_AddNew.Text = this.GetResourceTextByKey("Key_AddCaption");
                    btn_AddNew.Tag = "Key_AddCaption";
                    LoadOperator();
                    if (lstOperators.Items.Count > 0)
                        lstOperators.SelectedIndex = 0;
                    objDatawatcher.DataModify = false;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btn_Update_Click(object sender, EventArgs e)
        {
            try
            {
                string ErrorMsg = "";
                string strOperator = txt_OperatorName.Text.Trim();
                int operatorid = 0;
                if (strOperator == "")
                {
                    Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_VALID_OPERATOR_NAME"), this.Text);
                    return;
                }
                if (ValidateControls(ref ErrorMsg))//all text box validations are performed
                {
                    if (btn_AddNew.Tag == "Key_AddCaption")
                        operatorid = Convert.ToInt32(lstOperators.SelectedValue);
                    else
                        operatorid = 0;
                    if (bOperatorAlreadyExists(strOperator, operatorid))
                    {
                        Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_OPERATOR_ALREADY_EXISTS"), this.Text);
                        txt_OperatorName.Focus();
                        return;
                    }
                    if (btn_AddNew.Tag == "Key_AddCaption")
                    {
                        UpdateOperatorDetails(true);

                        OperatorEntity objUpdatedOperator = new OperatorEntity();
                        objUpdatedOperator.Operator_ID = operatorid;
                        objUpdatedOperator.Operator_Name = txt_OperatorName.Text.Trim();
                        objUpdatedOperator.Operator_Invoice_Name = txt_OperatorInvoiceName.Text.Trim();
                        objUpdatedOperator.Operator_Address = txt_OperatorAddress.Text.Trim();
                        objUpdatedOperator.Operator_Invoice_Address = txt_OperatorInvoiceAddress.Text.Trim();
                        objUpdatedOperator.Operator_PostCode = txt_OperatorPostcode.Text.Trim();
                        objUpdatedOperator.Operator_Invoice_Postcode = txt_OperatorInvoicePostcode.Text.Trim();
                        objUpdatedOperator.Operator_Depot_Phone = txt_OperatorPhone.Text.Trim();
                        objUpdatedOperator.Operator_EMail = txt_OperatorEmail.Text.Trim();
                        objUpdatedOperator.Operator_Fax = txt_OperatorFax.Text.Trim();
                        objUpdatedOperator.Operator_Contact = txt_OperatorContactName.Text.Trim();

                        AuditUpdatedOperatorDetails(lstExistingOperator[0], objUpdatedOperator);
                    }
                    else
                    {
                        UpdateOperatorDetails(false);
                        OperatorBusiness objOperatorbiz = new OperatorBusiness();
                        objOperatorbiz.InsertNewAuditEntry(ModuleNameEnterprise.Operators, "Operator", "Operator_Name", strOperator, iUserID, strUserName);
                        if (txt_OperatorInvoiceName.Text != "")
                            objOperatorbiz.InsertNewAuditEntry(ModuleNameEnterprise.Operators, "Operator", "Operator_Invoice_Name", txt_OperatorInvoiceName.Text, iUserID, strUserName);
                        if(txt_OperatorAddress.Text !="")
                            objOperatorbiz.InsertNewAuditEntry(ModuleNameEnterprise.Operators, "Operator", "Operator_Address", txt_OperatorAddress.Text, iUserID, strUserName);
                        if (txt_OperatorInvoiceAddress.Text != "")
                            objOperatorbiz.InsertNewAuditEntry(ModuleNameEnterprise.Operators, "Operator", "Operator_Invoice_Address", txt_OperatorInvoiceAddress.Text, iUserID, strUserName);
                        if (txt_OperatorPostcode.Text != "")
                            objOperatorbiz.InsertNewAuditEntry(ModuleNameEnterprise.Operators, "Operator", "Operator_PostCode", txt_OperatorPostcode.Text, iUserID, strUserName);
                        if (txt_OperatorInvoicePostcode.Text != "")
                            objOperatorbiz.InsertNewAuditEntry(ModuleNameEnterprise.Operators, "Operator", "Operator_Invoice_Postcode", txt_OperatorInvoicePostcode.Text, iUserID, strUserName);
                        if (txt_OperatorPhone.Text != "")
                            objOperatorbiz.InsertNewAuditEntry(ModuleNameEnterprise.Operators, "Operator", "Operator_Depot_Phone", txt_OperatorPhone.Text, iUserID, strUserName);
                        if (txt_OperatorEmail.Text != "")
                            objOperatorbiz.InsertNewAuditEntry(ModuleNameEnterprise.Operators, "Operator", "Operator_EMail", txt_OperatorEmail.Text, iUserID, strUserName);
                        if (txt_OperatorFax.Text != "")
                            objOperatorbiz.InsertNewAuditEntry(ModuleNameEnterprise.Operators, "Operator", "Operator_Fax", txt_OperatorFax.Text, iUserID, strUserName);
                        if (txt_OperatorContactName.Text != "")
                            objOperatorbiz.InsertNewAuditEntry(ModuleNameEnterprise.Operators, "Operator", "Operator_Contact", txt_OperatorContactName.Text, iUserID, strUserName);
                    }
                    this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_OPERATOR_UPDATE_SUCCESS"), this.Text);
                }
                else
                {
                    Win32Extensions.ShowWarningMessageBox(this, ErrorMsg, this.Text);
                    return;
                }
                btn_AddNew.Text = this.GetResourceTextByKey("Key_AddCaption");
                btn_AddNew.Tag = "Key_AddCaption";
                LoadOperator();
                lstOperators.Enabled = true;
                objDatawatcher.DataModify = false;
            }
            catch (Exception ex)
            {
                Win32Extensions.ShowErrorMessageBox(this,this.GetResourceTextByKey(1, "MSG_EXCEPTION_OCCURED_WHILE_UPDATE"), this.Text);
                ExceptionManager.Publish(ex);
                lstOperators.Enabled = true;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (lstOperators.Items.Count > 0)
            {
                string OperatorName = lstOperators.SelectedItem.ToString();
                int OperatorId = Convert.ToInt32(lstOperators.SelectedValue);
                try
                {
                    if (Win32Extensions.ShowMessageBox(this, "Do You Wish To Delete " + lstOperators.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    { 
                        LogManager.WriteLog("btnDelete_Click(),Deleting the selected Operatot" + OperatorId, LogManager.enumLogLevel.Info);
                        if (DeleteOperator(OperatorId))
                        {
                            OperatorBusiness objOperatorbiz = new OperatorBusiness();
                            objOperatorbiz.InsertDeteleAuditEntry(ModuleNameEnterprise.Operators, "Operator", "OperatorName", OperatorName,iUserID,strUserName);
                            List<OperatorEntity> lst_Op = (List<OperatorEntity>)lstOperators.DataSource;
                            lst_Op.Remove(lst_Op.Find(o => o.Operator_ID == OperatorId));
                            lstOperators.DataSource = null;
                            lstOperators.DataSource = lst_Op;
                            lstOperators.DisplayMember = "Operator_Name";
                            lstOperators.ValueMember = "Operator_ID";
                            ClearContents();
                            if (lstOperators.Items.Count > 0)
                                lstOperators.SelectedIndex = 0;
                            else
                                lstOperators.SelectedIndex = -1;
                        }
                    }                    
                }
                catch (Exception ex)
                {
                    Win32Extensions.ShowErrorMessageBox(this, this.GetResourceTextByKey(1, "MSG_EXCEPTION_OCCURED_WHILE_DELETE"), this.Text);
                    ExceptionManager.Publish(ex);
                }
            }
            else
            {
                Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_NO_OPERATORS_DELETE"), this.Text);
            }
			LoadOperator();
            objDatawatcher.DataModify = false;
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        #region Miscellaneous Methods
        
        /// <summary>
        /// Assigns the Resource Key names to the controls--Created by kishore sivagnanam
        /// </summary>
        public void SetTagProperty()
        {
            this.btn_AddNew.Tag = "Key_AddCaption";
            //this.btn_AddNew.Text = "@@Key_AddCaption@@";
            this.btn_Close.Tag = "Key_CloseCaption";
            this.btn_Delete.Tag = "Key_DeleteCaption";
            this.btn_Update.Tag = "Key_UpdateCaption";
            this.Tag = "Key_Operators";

        }

        public void ClearContents()
        {
            try
            {
                txt_OperatorName.Text = string.Empty;
                txt_OperatorInvoiceName.Text = string.Empty;
                txt_OperatorAddress.Text = string.Empty;
                txt_OperatorInvoiceAddress.Text = string.Empty;
                txt_OperatorPostcode.Text = string.Empty;
                txt_OperatorInvoicePostcode.Text = string.Empty;
                txt_OperatorPhone.Text = string.Empty;
                txt_OperatorEmail.Text = string.Empty;
                txt_OperatorFax.Text = string.Empty;
                txt_OperatorContactName.Text = string.Empty;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        public void EnableAndDisable(bool value)
        {
            try
            {
                txt_OperatorName.ReadOnly = value;
                txt_OperatorInvoiceName.ReadOnly = value;
                txt_OperatorAddress.ReadOnly = value;
                txt_OperatorInvoiceAddress.ReadOnly = value;
                txt_OperatorPostcode.ReadOnly = value;
                txt_OperatorInvoicePostcode.ReadOnly = value;
                txt_OperatorPhone.ReadOnly = value;
                txt_OperatorEmail.ReadOnly = value;
                txt_OperatorFax.ReadOnly = value;
                txt_OperatorContactName.ReadOnly = value;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        #endregion

        #region Operator Auditing

        public void AuditUpdatedOperatorDetails(OperatorEntity objOldData, OperatorEntity objNewData)
        {
            try
            {
                if ((objOldData == null) || (objNewData == null)) return;
                OperatorBusiness objOperatorbiz = new OperatorBusiness();

                if (objOldData.Operator_Name != objNewData.Operator_Name)
                    objOperatorbiz.AuditModifiedDataForOperator(objNewData.Operator_Name, "Operator_Name", objOldData.Operator_Name, objNewData.Operator_Name,iUserID,strUserName);

                if (objOldData.Operator_Invoice_Name.NullToString() != objNewData.Operator_Invoice_Name)
                    objOperatorbiz.AuditModifiedDataForOperator(objNewData.Operator_Invoice_Name, "Operator_Invoice_Name", objOldData.Operator_Invoice_Name, objNewData.Operator_Invoice_Name, iUserID, strUserName);

                if (objOldData.Operator_Address.NullToString() != objNewData.Operator_Address)
                    objOperatorbiz.AuditModifiedDataForOperator(objNewData.Operator_Address, "Operator_Address", objOldData.Operator_Address, objNewData.Operator_Address, iUserID, strUserName);

                if (objOldData.Operator_Invoice_Address.NullToString() != objNewData.Operator_Invoice_Address)
                    objOperatorbiz.AuditModifiedDataForOperator(objNewData.Operator_Invoice_Address, "Operator_Invoice_Address", objOldData.Operator_Invoice_Address, objNewData.Operator_Invoice_Address, iUserID, strUserName);

                if (objOldData.Operator_PostCode.NullToString() != objNewData.Operator_PostCode)
                    objOperatorbiz.AuditModifiedDataForOperator(objNewData.Operator_PostCode, "Operator_PostCode", objOldData.Operator_PostCode, objNewData.Operator_PostCode, iUserID, strUserName);

                if (objOldData.Operator_Invoice_Postcode.NullToString() != objNewData.Operator_Invoice_Postcode)
                    objOperatorbiz.AuditModifiedDataForOperator(objNewData.Operator_Invoice_Postcode, "Operator_Invoice_Postcode", objOldData.Operator_Invoice_Postcode, objNewData.Operator_Invoice_Postcode, iUserID, strUserName);

                if (objOldData.Operator_Depot_Phone.NullToString() != objNewData.Operator_Depot_Phone)
                    objOperatorbiz.AuditModifiedDataForOperator(objNewData.Operator_Depot_Phone, "Operator_Depot_Phone", objOldData.Operator_Depot_Phone, objNewData.Operator_Depot_Phone, iUserID, strUserName);

                if (objOldData.Operator_EMail.NullToString() != objNewData.Operator_EMail)
                    objOperatorbiz.AuditModifiedDataForOperator(objNewData.Operator_EMail, "Operator_EMail", objOldData.Operator_EMail, objNewData.Operator_EMail, iUserID, strUserName);

                if (objOldData.Operator_Fax.NullToString() != objNewData.Operator_Fax)
                    objOperatorbiz.AuditModifiedDataForOperator(objNewData.Operator_Fax, "Operator_Fax", objOldData.Operator_Fax, objNewData.Operator_Fax, iUserID, strUserName);

                if (objOldData.Operator_Contact.NullToString() != objNewData.Operator_Contact)
                    objOperatorbiz.AuditModifiedDataForOperator(objNewData.Operator_Contact, "Operator_Contact", objOldData.Operator_Contact, objNewData.Operator_Contact, iUserID, strUserName);
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
        #endregion

      
    }
}