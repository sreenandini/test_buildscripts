using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ShareHolder.Business;
using BMC.EnterpriseBusiness.Entities;
using BMC.Common.LogManagement;
using Audit.BusinessClasses;
using Audit.Transport;
using BMC.Common.Utilities;
using BMC.Common.ExceptionManagement;
using BMC.EnterpriseClient.Helpers;
using BMC.Common;

namespace BMC.EnterpriseClient.Views
{
    public partial class frmShareHolderAddEdit : Form
    {
        public ShareHolderEntity ShareHolder = new ShareHolderEntity();
        // BMC.EnterpriseClient.Helpers.Datawatcher objDatawatcher = null;



        public frmShareHolderAddEdit(FormEditTypes editTypes, int shareHolderId)
        {
            InitializeComponent();
            SetTagProperty();
            this.ResolveResources();
           // objDatawatcher = new Helpers.Datawatcher(this);
            this.Text = (editTypes == FormEditTypes.Add ? this.GetResourceTextByKey("Key_Add") : this.GetResourceTextByKey("Key_Edit")) + this.GetResourceTextByKey("Key_ShareHolders"); //"Add" : "Edit") + " Share Holder";
            ShareHolder.Id = shareHolderId;
            
        }

        /// <summary>
        /// Assigns the Resource Key names to the controls--Created by kishore sivagnanam
        /// </summary>
        public void SetTagProperty()
        {
            this.btnCancel.Tag = "Key_CancelCaption";
            this.lblDescription.Tag = "Key_DescriptionCaptionColon";
            this.btnSave.Tag = "Key_SaveCaption";
            this.lblShareHolderName.Tag = "Key_NameCaptionMandatoryColon";
            this.Tag = "Key_AddEditShareHolder";

        }

        private bool _IsUpdate = false;

        public bool IsUpdate
        {
            get
            {
                return _IsUpdate;
            }
            set
            {
                _IsUpdate = value;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string ShareHolderName = txtShareHolderName.Text.Trim();
                ShareHolderBusiness objshareHolderDetails = new ShareHolderBusiness();

                if ((String.IsNullOrEmpty(ShareHolderName)) || (ShareHolderName.Length > 50))
                {
                    this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_ENTER_VALID_SHAREHOLDER_NAME"), this.Text);
                    txtShareHolderName.Focus();
                    this.DialogResult = DialogResult.None;
                    return;
                }
                else
                {
                    int? iNameExists = 0;
                    int IsShareHolderNameExists = objshareHolderDetails.IsNameExists(txtShareHolderName.Text, ShareHolder.Id, ref iNameExists);
                    if (iNameExists > 0)
                    {
                        this.ShowWarningMessageBox(this.GetResourceTextByKey(1, "MSG_SHARE_HOLDER_NAME_EXISTS"), this.Text);
                        txtShareHolderName.Focus();
                        txtShareHolderName.Text = string.Empty;
                        return;

                    }
                }

                string ShareHolderDescription = txtShareHolderDescription.Text.Trim();


                if (IsUpdate)
                {
                    LogManager.WriteLog("ShareHolder Edit Starts", LogManager.enumLogLevel.Info);
                    objshareHolderDetails.EditShareHolder(ShareHolder.Id, ShareHolderName, ShareHolderDescription);
                    LogManager.WriteLog("ShareHolder Edit Ends", LogManager.enumLogLevel.Info);
                    try
                    {
                        AuditViewerBusiness business = new AuditViewerBusiness(DatabaseHelper.GetConnectionString());
                        {
                            business.InsertAuditData(new Audit.Transport.Audit_History
                            {
                                EnterpriseModuleName = ModuleNameEnterprise.ShareHolder,
                                Audit_Screen_Name = "ShareHolder|ShareHolderAddEdit",
                                Audit_Desc = "ShareHolder Updated" + "ShareHolder ID: " + ShareHolder.Id + "; ShareHolderName: " + ShareHolderName + ";  ",
                                AuditOperationType = OperationType.MODIFY,
                                Audit_Old_Vl = ShareHolder.Name,
                                Audit_New_Vl = ShareHolderName,
                                Audit_User_ID = AppEntryPoint.Current.UserId,
                                Audit_User_Name = AppEntryPoint.Current.UserName
                            }, false);
                        }
                    }
                    catch (Exception ex)
                    {
                        LogManager.WriteLog("Error While Adding Audit Log for ShareHolder Update: Error Message:" + ex.Message, LogManager.enumLogLevel.Error);
                    }
                }
                else
                {
                    objshareHolderDetails.AddShareHolder(ShareHolderName, ShareHolderDescription);
                    try
                    {
                        AuditViewerBusiness business = new AuditViewerBusiness(DatabaseHelper.GetConnectionString());
                        {
                            business.InsertAuditData(new Audit.Transport.Audit_History
                            {
                                EnterpriseModuleName = ModuleNameEnterprise.ShareHolder,
                                Audit_Screen_Name = "ShareHolder|ShareHolderAddEdit",
                                Audit_Desc = "ShareHolder Added-" + "ShareHolder: " + ShareHolderName + "",
                                AuditOperationType = OperationType.ADD,

                                Audit_User_ID = AppEntryPoint.Current.UserId,
                                Audit_User_Name = AppEntryPoint.Current.UserName
                            }, false);
                        }
                    }
                    catch (Exception ex)
                    {
                        LogManager.WriteLog("Error While Adding Audit Log for ShareHolder Insert: Error Message:" + ex.Message, LogManager.enumLogLevel.Error);
                    }
                }

                this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_SHARE_HOLDER_DETAILS_SAVED"), this.Text);
                //Insert -ShareHolder Name,ShareHolder  Id
                LogManager.WriteLog("ShareHolder Added : " + ShareHolderName + " ", LogManager.enumLogLevel.Info);

                if (!IsUpdate)
                {
                    txtShareHolderName.Text = string.Empty;
                    txtShareHolderDescription.Text = string.Empty;
                    // objDatawatcher.DataModify = false;
                    this.Close();
                    this.DialogResult = DialogResult.OK;
                    txtShareHolderName.Focus();

                }
                else
                {
                    //  objDatawatcher.DataModify = false;
                    this.Close();
                    this.DialogResult = DialogResult.OK;
                }
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

        }

        private void frmAddShareHolder_Load(object sender, EventArgs e)
        {
            try
            {

                ShareHolderBusiness objshareHolderDetails = new ShareHolderBusiness();
                if (IsUpdate)
                {
                    txtShareHolderName.Text = ShareHolder.Name;
                    txtShareHolderDescription.Text = ShareHolder.Description;

                }
                //objDatawatcher.DataModify = false;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}