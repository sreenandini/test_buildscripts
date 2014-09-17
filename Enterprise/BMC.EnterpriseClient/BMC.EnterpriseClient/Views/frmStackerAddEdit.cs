using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BMC.EnterpriseBusiness;
using BMC.Common.LogManagement;
using Audit.BusinessClasses;
using Audit.Transport;
using BMC.Common.Utilities;
using BMC.EnterpriseBusiness.Entities;
using Stacker.Business;
using BMC.CoreLib.Win32;
using BMC.Common;

namespace BMC.EnterpriseClient.Views
{
    public partial class frmStackerAddEdit : Form
    {
        public StackerEntity Stacker = new StackerEntity();
        BMC.EnterpriseClient.Helpers.Datawatcher objDatawatcher = null;


        private bool _IsUpdate = false;

        private bool _IsStackerInUse = false;

        private bool OldStackerInUse;

        private string OldStackerDescription;
        private string OldStackerName;
        private int OldstackerValue;
        int StackerID = 0;

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

        public bool IsStackerInUseForEdit
        {
            get
            {
                return _IsStackerInUse;
            }
            set
            {
                _IsStackerInUse = value;
            }
        }


        #region Methods

        #endregion

        #region Events

        public frmStackerAddEdit()
        {
            InitializeComponent();
            SetTagProperty();

        }

        /// <summary>
        /// Assigns the Resource Key names to the controls--Created by kishore sivagnanam
        /// </summary>
        public void SetTagProperty()
        {
            this.btnClose.Tag = "Key_CloseCaption";
            this.btnSave.Tag = "Key_SaveCaption";
            this.lblName.Tag = "Key_NameMandatory";
            this.lblSize.Tag = "Key_SizeMandatory";
            this.rdbActive.Tag = "Key_Active";
            this.lblDescription.Tag = "Key_DescriptionColon";
            this.rdbInActive.Tag = "Key_InActive";
            this.Tag = "Key_StackerAddEdit";
            this.grpStackerType.Tag = "Key_StackerType";
            this.grpStatus.Tag = "Key_Status";

        }

        private void StackerAddEdit_Load(object sender, EventArgs e)
        {
            try
            {
                this.ResolveResources();
                StackerBusiness objBStackerDetails = new StackerBusiness();
                OldstackerValue = Stacker.StackerSize;
                OldStackerInUse = Stacker.StackerStatus;
                OldStackerDescription = Stacker.StackerDescription;
                OldStackerName = Stacker.StackerName;
                StackerID = Stacker.StackerID;
                if (IsUpdate)
                {
                    txtStackerName.Text = Stacker.StackerName;
                    txtStackerSize.Value = Stacker.StackerSize;
                    txtStackerDescription.Text = Stacker.StackerDescription;
                    rdbActive.Checked = (Stacker.StackerStatus == true);
                    rdbInActive.Checked = (Stacker.StackerStatus == false);
                    if (IsStackerInUseForEdit)
                    {
                        txtStackerName.ReadOnly = true;
                        //txtStackerSize.ReadOnly = true;
                        txtStackerSize.Enabled = false;
                        grpStatus.Enabled = false;
                    }

                    //If stacker is used we should not allow the user to delete the stacker
                    grpStatus.Enabled = objBStackerDetails.IsStackerInUse(Stacker.StackerID) == 0;
                }
                objDatawatcher = new Helpers.Datawatcher(this);
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error : Stacker Add&Edit" + ex.Message, LogManager.enumLogLevel.Error);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string StackerName = txtStackerName.Text.Trim();
                StackerBusiness objBStackerDetails = new StackerBusiness();

                if (String.IsNullOrEmpty(StackerName))
                {
                    this.ShowWarningMessageBox(this.GetResourceTextByKey(1, "MSG_ENTER_STACKER_NAME"), this.Text);
                    txtStackerName.Focus();
                    return;
                }
                if (StackerName.Length > 50)
                {
                    this.ShowWarningMessageBox(this.GetResourceTextByKey(1, "MSG_ENTER_VALID_STACKER_NAME"), this.Text);
                    txtStackerName.Focus();
                    return;
                }
                else
                {
                    //If Stacker Name already Exists
                    int? iNameExists = 0;
                    int IsStackerNameExists = objBStackerDetails.IsNameExists(StackerName, ref iNameExists, StackerID);
                    if (iNameExists > 0)
                    {
                        this.ShowWarningMessageBox(this.GetResourceTextByKey(1, "MSG_STACKER_NAME_EXISTS"), this.Text);
                        txtStackerName.Focus();
                        return;
                    }
                }
                int intStackerSize = Int32.MinValue;
                int.TryParse(txtStackerSize.Value.ToString(), out intStackerSize);
                int StackerSize = intStackerSize;
                if (StackerSize <= 0)
                {
                    this.ShowWarningMessageBox(this.GetResourceTextByKey(1, "MSG_ENTER_STACKER_SIZE_GREATER_THAN_ZERO"), this.Text);
                    txtStackerSize.Focus();
                    return;
                }
                else if (StackerSize > txtStackerSize.Maximum)
                {
                    this.ShowWarningMessageBox(this.GetResourceTextByKey(1, "MSG_STACKER_MAXIMUM_LIMIT"), this.Text);                     
                }

                bool StackerStatus = rdbActive.Checked;

                string StackerDescription = txtStackerDescription.Text.Trim();

                if (IsUpdate)
                {


                    LogManager.WriteLog("Stacker Edit Starts", LogManager.enumLogLevel.Info);
                    objBStackerDetails.EditStackerDetails(Stacker.StackerID, StackerName, StackerSize, StackerStatus, StackerDescription);
                    LogManager.WriteLog("Stacker Edit Ends", LogManager.enumLogLevel.Info);
                    try
                    {
                        if(!Stacker.StackerName.Equals(StackerName)) AuditStackerEdit(StackerName, StackerSize, "StackerName", Stacker.StackerName, StackerName);
                        if (!Stacker.StackerSize.Equals(StackerSize)) AuditStackerEdit(StackerName, StackerSize, "StackerSize", Convert.ToString(Stacker.StackerSize), Convert.ToString(StackerSize));
                        if (!Stacker.StackerStatus.Equals(StackerStatus)) AuditStackerEdit(StackerName, StackerSize, "StackerStatus", Convert.ToString(Stacker.StackerStatus), Convert.ToString(StackerStatus));
                        if (!Stacker.StackerName.Equals(StackerName)) AuditStackerEdit(StackerName, StackerSize, "StackerDescription", Stacker.StackerDescription, StackerDescription);
                        this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_STACKER_UPDATE_SUCCESS"), this.Text);
                        LogManager.WriteLog("Stacker details updated : " + StackerName + " ", LogManager.enumLogLevel.Info);
                        objDatawatcher.DataModify = false;
                        this.Close();
                        return;
                    }
                    catch (Exception ex)
                    {
                        LogManager.WriteLog("Error While Adding Audit Log for Stacker Update: Error Message:" + ex.Message, LogManager.enumLogLevel.Error);
                    }
                }
                else
                {
                    objBStackerDetails.AddStackerDetails(StackerName, StackerSize, StackerStatus, StackerDescription);
                    try
                    {
                        AuditViewerBusiness business = new AuditViewerBusiness(DatabaseHelper.GetConnectionString());
                        {
                            business.InsertAuditData(new Audit.Transport.Audit_History
                            {
                                EnterpriseModuleName = ModuleNameEnterprise.Stacker,
                                Audit_Screen_Name = "Stacker|StackerAddEdit",
                                Audit_Desc = "Stacker Added-" + "StackerName: " + StackerName + "; StackerSize: " + StackerSize,
                                AuditOperationType = OperationType.ADD,
                                Audit_User_ID = AppEntryPoint.Current.UserId,
                                Audit_User_Name = AppEntryPoint.Current.UserName,
                                Audit_Field = "StackerName",
                                Audit_Old_Vl ="",
                                Audit_New_Vl = StackerName

                            }, false);
                        }
                    }
                    catch (Exception ex)
                    {
                        LogManager.WriteLog("Error While Adding Audit Log for Stacker Insert: Error Message:" + ex.Message, LogManager.enumLogLevel.Error);
                    }
                }
                this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_STACKER_SAVE_SUCCESS"), this.Text);
                //Insert - StackerName, Size, User
                //Edit-AH StackerID, StackerName, Size, User               

                LogManager.WriteLog("Stacker Added : " + StackerName + " ", LogManager.enumLogLevel.Info);

                if (!IsUpdate)
                {
                    txtStackerName.Text = string.Empty;
                    txtStackerSize.Value = 1;
                    rdbActive.Checked = true;
                    txtStackerDescription.Text = string.Empty;
                    if (this.ShowQuestionMessageBox(this.GetResourceTextByKey(1, "MSG_WANT_TO_ADD_ANOTHER_STACKER"), this.Text) == DialogResult.No)
                    {
                        this.Close();
                    }
                    else
                    {
                        txtStackerName.Focus();
                    }
                }
                else
                {
                    this.Close();
                }

            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error : Stacker Save" + ex.Message, LogManager.enumLogLevel.Error);
            }
        }

        private void AuditStackerEdit(string StackerName, int StackerSize, string fieldName, string oldValue, string newValue)
        {
            AuditViewerBusiness business = new AuditViewerBusiness(DatabaseHelper.GetConnectionString());
            {
                business.InsertAuditData(new Audit.Transport.Audit_History
                {
                    EnterpriseModuleName = ModuleNameEnterprise.Stacker,
                    Audit_Screen_Name = "Stacker|StackerAddEdit",
                    Audit_Desc = "Stacker Updated-" + "Stacker ID: " + Stacker.StackerID + "; StackerName: " + StackerName + "; StackerSize: " + StackerSize,
                    AuditOperationType = OperationType.MODIFY,
                    Audit_User_ID = AppEntryPoint.Current.UserId,
                    Audit_User_Name = AppEntryPoint.Current.UserName,
                    Audit_Field = fieldName,
                    Audit_Old_Vl = oldValue,
                    Audit_New_Vl = newValue
                }, false);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        private void lblName_Click(object sender, EventArgs e)
        {

        }

        private void frmStackerAddEdit_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void txtStackerSize_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                e.Handled = !char.IsControl(e.KeyChar)
                  && !char.IsDigit(e.KeyChar);
            }
            finally
            {
            }
        }
    }
}
