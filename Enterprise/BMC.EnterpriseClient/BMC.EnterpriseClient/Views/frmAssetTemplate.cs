using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BMC.EnterpriseBusiness.Business;
using BMC.Common.ExceptionManagement;
using BMC.Common.Utilities;
using BMC.EnterpriseBusiness.Entities;
using BMC.Common.LogManagement;
using BMC.CoreLib.Win32;
using Audit.BusinessClasses;
using Audit.Transport;
using BMC.Common;

namespace BMC.EnterpriseClient.Views
{
    public partial class frmAssetTemplate : Form
    {
        private string _assetNumber = string.Empty;
        private string _OldValue = string.Empty;
        private int index;
        AssetManagementBiz objAssetBiz = null;
        GetAssetNumberForTemplateResult objTemplate = null;
        AuditViewerBusiness business = null;
        BMC.EnterpriseClient.Helpers.Datawatcher objDatawatcher = null;


        public frmAssetTemplate(int IsEdit, string AssetNumber)
        {
            InitializeComponent();
            _assetNumber = AssetNumber;
            if (IsEdit == 1)
                btnEdit.Text = this.GetResourceTextByKey("Key_UpdateCaption");
            else
                btnEdit.Text = this.GetResourceTextByKey("Key_DeleteCaption");

            objAssetBiz = new AssetManagementBiz();
            objTemplate = new GetAssetNumberForTemplateResult();
            business = new AuditViewerBusiness(DatabaseHelper.GetConnectionString());
            SetTagProperty();
        }

        private void SetTagProperty()
        {
            try
            {                
                this.btnCancel.Tag = "Key_CancelCaption";
                this.lblTemplate.Tag = "Key_PleaseSelectaTemplateName";
                this.btnEdit.Tag = "Key_DeleteCaption";
                this.Tag = "Key_AssetTemplate";
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void frmAssetTemplate_Load(object sender, EventArgs e)
        {
            LoadCmbTemplate();
            objDatawatcher = new Helpers.Datawatcher(this);
            this.ResolveResources();
        }

        public void LoadCmbTemplate()
        {
            try
            {
                List<GetAssetTemplateDetailsResult> objTemplate = AssetManagementBiz.CreateInstance().DisplayTemplate();
                cmbEditTemplate.DisplayMember = "TemplateName";
                cmbEditTemplate.ValueMember = "AssetCrTempNumber";
                cmbEditTemplate.SelectedIndex = -1;
                cmbEditTemplate.DataSource = objTemplate;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                index = cmbEditTemplate.SelectedIndex;
                if (btnEdit.Text == this.GetResourceTextByKey("Key_UpdateCaption"))
                {

                    if (index == -1)
                    {
                        this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_ASSETTEMP_NOTEMP"), this.Text);
                    }
                    else
                    {


                        if (this.ShowQuestionMessageBox(this.GetResourceTextByKey(1, "MSG_ASSETTEMP_UPDATE"), this.Text) == DialogResult.Yes)
                        {
                            string TemplateName;
                            TemplateName = cmbEditTemplate.Text;
                            _OldValue = GetAssetNumberForTemplate(TemplateName);
                            UpdateTemplate(true, _assetNumber, TemplateName);
                            LoadCmbTemplate();
                            string _oldAssetNumber = cmbEditTemplate.SelectedItem.ToString();
                            UpdateTemplateAudit(_OldValue, _assetNumber, TemplateName);
                            this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_ASSETTEMP_SUCCESS"), this.Text);
                            this.Close();
                        }
                    }
                }
                else
                {
                    if (index == -1)
                    {
                        this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_ASSETTEMP_DEL"), this.Text);

                    }
                    else
                    {
                        if (this.ShowQuestionMessageBox(this.GetResourceTextByKey(1, "MSG_ASSETTEMP_DELETE"), this.Text) == DialogResult.Yes)
                        {
                            string TemplateName;
                            TemplateName = cmbEditTemplate.Text;
                            UpdateTemplate(false, _assetNumber, TemplateName);
                            DeletetemplateAudit(TemplateName);
                            LoadCmbTemplate();
                            this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_ASSETTEMP_DELSUCCESS"), this.Text);
                            this.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private string GetAssetNumberForTemplate(string TemplateName)
        {
            List<GetAssetNumberForTemplateResult> oAssetDet = null;
            string sAssetNo = string.Empty;
            try
            {
                oAssetDet = objAssetBiz.GetAssetNumber(TemplateName);
                if (oAssetDet.Count > 0)
                {
                    sAssetNo = oAssetDet[0].AssetNumber;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return sAssetNo;
        }

        private void UpdateTemplate(bool IsEdit, string StockNumber, string TemplateName)
        {
            try
            {

                objAssetBiz.UpdateAssetTemplate(IsEdit, StockNumber, TemplateName);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
        private void cmbEditTemplate_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);

            }

        }

        private void UpdateTemplateAudit(string OldValue, string Newvalue, string TemplateName)
        {
            try
            {
                business.InsertAuditData(new Audit.Transport.Audit_History
                {
                    EnterpriseModuleName = ModuleNameEnterprise.AssetTemplate,
                    Audit_Screen_Name = "Template Updated",
                    Audit_Desc = "Update Template:" + TemplateName + " has modified Asset Details " + OldValue + "-->" + Newvalue + "",
                    AuditOperationType = OperationType.MODIFY,
                    Audit_Field = "Asset Number",
                    Audit_New_Vl = Newvalue,
                    Audit_Old_Vl = OldValue,
                    Audit_User_ID = AppEntryPoint.Current.UserId,
                    Audit_User_Name = AppEntryPoint.Current.UserName
                }, false);
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error While Adding Audit Log for Template Update: Error Message:" + ex.Message, LogManager.enumLogLevel.Error);
            }
        }

        private void DeletetemplateAudit(string TemplateName)
        {
            try
            {
                business.InsertAuditData(new Audit.Transport.Audit_History
                {
                    EnterpriseModuleName = ModuleNameEnterprise.AssetTemplate,
                    Audit_Screen_Name = "Template Deleted",
                    Audit_Desc = "Template Name:" + TemplateName + " deleted from Asset Template",
                    AuditOperationType = OperationType.DELETE,
                    Audit_Field = "Template Name",
                    Audit_Old_Vl = TemplateName,
                    Audit_User_ID = AppEntryPoint.Current.UserId,
                    Audit_User_Name = AppEntryPoint.Current.UserName
                }, false);
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error While Adding Audit Log for Template Delete: Error Message:" + ex.Message, LogManager.enumLogLevel.Error);
            }
        }
    }
}


