using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BMC.Common.ExceptionManagement;
using BMC.Common.LogManagement;
using BMC.EnterpriseBusiness.Entities;
using BMC.EnterpriseBusiness.Business;
using Audit.Transport;
using Audit.BusinessClasses;
using BMC.Common.Utilities;
using BMC.CoreLib.Win32;
using BMC.Common;

namespace BMC.EnterpriseClient.Views
{
    public partial class frmModelType : BMC.EnterpriseClient.Helpers.BMCExtendedForm
    {
        private const string ScreenName = "Model Type  => ";
        BMC.EnterpriseClient.Helpers.Datawatcher objDatawatcher = null;
        

        public frmModelType()
        {
            InitializeComponent();
            SetTagProperty();
            objDatawatcher = new Helpers.Datawatcher(this,
               (w, f) =>
               {
                   w.RemoveControlFromWatcher((f as frmModelType).cmbModelType);
               }); 
        }

        /// <summary>
        /// Assigns the Resource Key names to the controls--Created by kishore sivagnanam
        /// </summary>
        public void SetTagProperty()
        {
            this.btnClose.Tag = "Key_CloseCaption";
            this.btnEdit.Tag = "Key_EditCaption";
            this.btnNew.Tag = "Key_NewCaption";
            this.btnUpdate.Tag = "Key_UpdateCaption";
            this.lblModelDesc.Tag = "Key_CabinetModelDescriptionMandatoryColon";
            this.lblModelName.Tag = "Key_CabinetModelTypeNameMandatory";
            this.Tag = "Key_CabinetModelType";
            this.lblModelType.Tag = "Key_CabinetModelTypeColon";
            this.chkNGA.Tag = "Key_IsNonGamingAsset";

        }
        private void frmModelType_Load(object sender, EventArgs e)
        {
            try
            {
                this.ResolveResources();
                LoadModelType(false, null, false);
                cmbModelType.SelectedIndex = -1;
                btnUpdate.Enabled = false;
                btnEdit.Enabled = false;
                txtModelName.Enabled = false;
                txtModelDesc.Enabled = false;
                objDatawatcher.DataModify = false;
               
            }
            catch (Exception ex)
            {
                Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_UNABLE_TO_LOAD_MODEL"), this.Text);
                ExceptionManager.Publish(ex);
            }
        }

        /// <summary>
        /// Model Type
        /// </summary>
        /// <param name="IsNGA"></param>
        private void LoadModelType(bool? IsNGA, int? MT_ID, bool ModelTypeSelectedChange)
        {
            try
            {
                LogManager.WriteLog(ScreenName + "Load Model Type details", LogManager.enumLogLevel.Info);
                List<GetModelTypeResult> lst_ModelType = new List<GetModelTypeResult>();
                if (!ModelTypeSelectedChange)
                {
                    lst_ModelType = AssetManagementBiz.CreateInstance().GetModelTypeDetails(IsNGA, MT_ID);
                    cmbModelType.DisplayMember = "MT_Model_Name";
                    cmbModelType.ValueMember = "MT_ID";
                    cmbModelType.DataSource = lst_ModelType;
                }
                else
                {
                    lst_ModelType.Add(cmbModelType.SelectedItem as GetModelTypeResult);
                }
                if (ModelTypeSelectedChange)
                {
                    if (lst_ModelType != null && lst_ModelType.Count > 0)
                    {
                        txtModelName.Text = lst_ModelType[0].MT_Model_Name;
                        txtModelDesc.Text = lst_ModelType[0].MT_Model_Desc;
                        chkNGA.Checked = lst_ModelType[0].MT_IsNGA;
                    }
                    else
                    {
                        txtModelName.Text = "";
                        txtModelDesc.Text = "";
                        chkNGA.Checked = false;
                    }
                    txtModelName.Enabled = false;
                    txtModelDesc.Enabled = false;
                    btnUpdate.Enabled = false;
                }
               

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void cmbModelType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbModelType.SelectedIndex == -1)
            {
                txtModelName.Text = "";
                txtModelDesc.Text = "";
                btnEdit.Enabled = false;
                return;
                objDatawatcher.DataModify = false;
            }
            try
            {
                btnEdit.Enabled = true;
                LoadModelType(null, (int)cmbModelType.SelectedValue, true);
                btnEdit.Text = this.GetResourceTextByKey("Key_EditCaption");
                objDatawatcher.DataModify = false;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            objDatawatcher.DataModify = false;
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            txtModelName.Enabled = true;
            txtModelDesc.Enabled = true;
            cmbModelType.Enabled = false;
            txtModelName.Text = "";
            txtModelDesc.Text = "";
            cmbModelType.DataSource = null;
            btnNew.Enabled = false;
            btnUpdate.Enabled = true;
            btnEdit.Text = this.GetResourceTextByKey("Key_ClearCaption");
            btnEdit.Enabled = true;
            txtModelName.Focus();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (btnEdit.Text.Equals(this.GetResourceTextByKey("Key_EditCaption")))
            {
                btnEdit.Text = this.GetResourceTextByKey("Key_ClearCaption");
                txtModelName.Enabled = false;
                txtModelDesc.Enabled = true;
                btnNew.Enabled = true;
                btnUpdate.Enabled = true;
                //btnEdit.Enabled = false;
                txtModelName.Focus();
            }
            else
            {
                int selectedIndex = cmbModelType.SelectedIndex;
                btnEdit.Text = this.GetResourceTextByKey("Key_EditCaption");
                EnableControlsUpdate(true);
                LoadModelType(chkNGA.Checked, null, false);
                cmbModelType.SelectedIndex = selectedIndex;

               
            }

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            bool isSuccess = false;
            try
            {
                int selectedIndex = cmbModelType.SelectedIndex;
                string ErrorMsg = "";
                
                if (ValidateControls(ref ErrorMsg))
                {
                    int? ModelTypeID = 0;
                    List<GetModelTypeResult> lst_ModelType = null;
                    if (cmbModelType.DataSource == null)
                    {
                        lst_ModelType = AssetManagementBiz.CreateInstance().GetModelTypeDetails(chkNGA.Checked, null);
                    }

                    if (cmbModelType.DataSource is List<GetModelTypeResult>)
                    {
                        lst_ModelType = cmbModelType.DataSource as List<GetModelTypeResult>;
                    }

                    if (cmbModelType.DataSource == null && (lst_ModelType == null || lst_ModelType.Exists(x => x.MT_Model_Name.Trim().ToUpper() == txtModelName.Text.Trim().ToUpper())
                        && this.ShowQuestionMessageBox(this.GetResourceTextByKey(1, "MSG_MODEL_TYPE_NAME") + txtModelName.Text.Trim() + this.GetResourceTextByKey(1, "MSG_ALREADY_EXISTS"), this.Text) == DialogResult.No))
                    {

                        return;
                    }
                    if (cmbModelType.SelectedItem != null && cmbModelType.SelectedItem is GetModelTypeResult)
                    {
                        ModelTypeID = (cmbModelType.SelectedItem as GetModelTypeResult).MT_ID;
                    }

                    if (AssetManagementBiz.CreateInstance().UpdateModelType(txtModelName.Text.Trim(), txtModelDesc.Text.Trim(), chkNGA.Checked, ref ModelTypeID))
                    {
                        LogManager.WriteLog(ScreenName + "btnUpdate_Click:ModelType details updated successfully; ModelTypeName:" + txtModelName.Text, LogManager.enumLogLevel.Info);
                        bool NewType = (ModelTypeID.HasValue && ModelTypeID.Value > 0);
                        AuditViewerBusiness business = new AuditViewerBusiness(DatabaseHelper.GetConnectionString());
                        business.InsertAuditData(new Audit.Transport.Audit_History
                        {
                            EnterpriseModuleName = ModuleNameEnterprise.AUDIT_MODELTYPE,
                            Audit_Slot = "",
                            Audit_Screen_Name = "ModelType",
                            Audit_Field = "ModelTypeName",
                            Audit_Old_Vl = "",
                            Audit_New_Vl = txtModelName.Text.Trim(),
                            Audit_Desc = "Add ModelType ModelTypeName  " + (NewType ? "added " : "modified ") + txtModelName.Text,
                            AuditOperationType = (NewType ? OperationType.ADD : OperationType.MODIFY),
                            Audit_User_ID = AppEntryPoint.Current.UserId,
                            Audit_User_Name = AppEntryPoint.Current.UserName
                        }, false);

                        isSuccess = true;
                        //btnEdit.Text = "&Edit";
                    }
                    else
                    {
                        LogManager.WriteLog(ScreenName + "btnUpdate_Click:Unable to update ModelTypeName:" + txtModelName.Text, LogManager.enumLogLevel.Info);
                    }
                }
                else
                {
                    Win32Extensions.ShowInfoMessageBox(this, ErrorMsg, this.Text);
                }
                objDatawatcher.DataModify = false;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            finally
            {
                if (isSuccess)
                {
                    btnEdit_Click(sender, e);
                }
                objDatawatcher.DataModify = false;
            }
        }

        void EnableControlsUpdate(bool Enable)
        {
            btnNew.Enabled = Enable;
            btnUpdate.Enabled = !Enable;
            btnEdit.Enabled = !Enable;
            txtModelName.Text = "";
            txtModelDesc.Text = "";
            txtModelName.Enabled = !Enable;
            txtModelDesc.Enabled = !Enable;
            cmbModelType.Enabled = Enable;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            LogManager.WriteLog(ScreenName + "Form Closed", LogManager.enumLogLevel.Info);
            this.Close();
        }

        private void chkNGA_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbModelType.Enabled)
                {
                    LoadModelType(chkNGA.Checked, null, false);
                    cmbModelType.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        bool ValidateControls(ref string ErrorMsg)
        {
            bool retVal = true;
            try
            {
                if (txtModelName.Text.Trim() == "")
                {
                    ErrorMsg = this.GetResourceTextByKey(1,"MSG_CABINET_NAME");
                    txtModelName.Focus();
                    retVal = false;
                }
                else if (txtModelDesc.Text.Trim() == "")
                {
                    ErrorMsg = this.GetResourceTextByKey(1,"MSG_CABINET_DESC");
                    txtModelDesc.Focus();
                    retVal = false;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                retVal = false;
                ErrorMsg = this.GetResourceTextByKey("MSG_ADDMCTYPE_VALIDATEFAILURE");
            }
            return retVal;
        }

    }
}
