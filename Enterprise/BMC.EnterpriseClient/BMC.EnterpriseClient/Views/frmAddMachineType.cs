using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BMC.EnterpriseBusiness.Business;
using BMC.EnterpriseBusiness.Entities;
using BMC.Common.LogManagement;
using BMC.Common.ExceptionManagement;
using System.Text.RegularExpressions;
using Audit.BusinessClasses;
using Audit.Transport;
using BMC.Common.Utilities;
using BMC.CoreLib.Win32;
using BMC.Common;

namespace BMC.EnterpriseClient.Views
{
    public partial class frmAddMachineType : BMC.EnterpriseClient.Helpers.BMCExtendedForm
    {
        bool NewType = false;
        bool Edited = false;
        int _MachineTypeID = 0;
        private const string ScreenName = "Add MachineType => ";
        private string strAny = "--ANY--";
        List<GetMachineTypeDetailsResult> lst_MCType = null;

        List<GetSiteIconDetailsResult> lst_site = null;
        private int cmb_nameindex = -1;
        bool Addtype = false;

        BMC.EnterpriseClient.Helpers.Datawatcher objDatawatcher = null;


        public frmAddMachineType()
        {
            InitializeComponent();
            setTagProperty();
            objDatawatcher = new Helpers.Datawatcher(this,
                (w, f) =>
                {
                    w.RemoveControlFromWatcher((f as frmAddMachineType).cmbName);
                    w.RemoveControlFromWatcher((f as frmAddMachineType).cmbDepreciation);
                    w.RemoveControlFromWatcher((f as frmAddMachineType).cmbSiteIcon);
                    
                });    
        }

        /// <summary>
        /// 
        /// </summary>
        private void setTagProperty()
        {
            this.btnCancel.Tag = "Key_CancelCaption";
            this.btnEdit.Tag = "Key_EditCaption";
            this.btnNew.Tag = "Key_AddCaption";
            this.lblName.Tag = "Key_NameMandatory";
            this.lblCMPGatewayCode.Tag = "Key_CMPGatewayCode";
            this.lblDepreciationPolicy.Tag = "Key_DepreciationPolicyColon";
            this.lblDescription.Tag = "Key_DescriptionColon";
            this.chkNGA.Tag = "Key_IsNonGamingAsset";
            this.lblLedger.Tag = "Key_LedgerCodeColon";
            this.Tag = "Key_Machinetypeadmin";
            this.lblSiteIcon.Tag = "Key_SiteIconColon";
            this.btnUpdate.Tag = "Key_UpdateCaption";

        }


        public void ShowMe(int MachineTypeID)
        {
            _MachineTypeID = MachineTypeID;
        }

        public void ShowMeNew(int MachineTypeID)
        {
            Addtype = true;
            _MachineTypeID = MachineTypeID;

        }
        /// <summary>
        /// Form Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmAddMachineType_Load(object sender, EventArgs e)
        {
            try
            {
                this.ResolveResources();
                strAny = this.GetResourceTextByKey("Key_Any");
                LogManager.WriteLog(ScreenName + "frmAddMachineType_Load:Started", LogManager.enumLogLevel.Debug);
                NewType = false;
                Edited = false;
                EnableControls(true);
                LoadSiteIconDetails();
                LoadDepreciationDetails();
                LoadMachineTypeDetails();
                if (Addtype)
                {
                    btnNew_Click(this, null);
                    txtName.Focus();
                }

                objDatawatcher.DataModify = false;

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        /// <summary>
        /// Enable controls based on form load
        /// </summary>
        /// <param name="Isload"></param>
        private void EnableControls(bool Isload)
        {
            btnEdit.Visible = Isload;
            btnNew.Visible = Isload;
            btnUpdate.Visible = !Isload;
            btnCancel.Text = Isload ? this.GetResourceTextByKey("Key_CloseCaption") : this.GetResourceTextByKey("Key_CancelCaption"); //"&Close" : "&Cancel";
            btnCancel.Tag = Isload ? "Key_CloseCaption": "Key_CancelCaption"; //"&Close" : "&Cancel";
            this.ResolveResources();
            cmbName.Visible = Isload;
            txtName.Visible = !Isload;
            txtDescription.Enabled = !Isload;
            txtCMPGatewayCode.Enabled = !Isload;
            cmbDepreciation.Enabled = !Isload;
            txtIncomeLedgerCode.Enabled = !Isload;
            chkNGA.Enabled = NewType;
            if (Edited && !Addtype)
            {
                if (cmbName.SelectedItem != null)
                {
                    GetMachineTypeDetailsResult Mtype = cmbName.SelectedItem as GetMachineTypeDetailsResult;
                    txtName.Text = Mtype.Machine_Type_Code;
                    _MachineTypeID = Mtype.Machine_Type_ID;
                }
                btnCancel.Visible = Edited;
            }
            else if (NewType)
            {

                txtDescription.Text = "";
                txtIncomeLedgerCode.Text = "";
                txtName.Text = "";
                chkNGA.Checked = false;
                if (cmbDepreciation.Items.Count > 0)
                {
                    cmbDepreciation.SelectedIndex = 0;
                }
                txtCMPGatewayCode.Text = "";
                btnCancel.Visible = NewType;
            }
        }
        /// <summary>
        /// Depreciation Details
        /// </summary>
        private void LoadDepreciationDetails()
        {
            try
            {
                LogManager.WriteLog(ScreenName + "Load Depreciation details", LogManager.enumLogLevel.Info);
                List<DepreciationEntity> lst_depreciation = DepreciationBusiness.CreateInstance().LoadDepreciationPolicy(null).ToList();
                if (lst_depreciation != null & lst_depreciation.Count > 0)
                {
                    lst_depreciation.Insert(0, (new DepreciationEntity { Depreciation_Policy_ID = -1, Depreciation_Policy_Description = strAny }));
                }
                cmbDepreciation.DataSource = lst_depreciation;
                cmbDepreciation.DisplayMember = "Depreciation_Policy_Description";
                cmbDepreciation.ValueMember = "Depreciation_Policy_ID";
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        /// <summary>
        /// Machine Type Details
        /// </summary>
        private void LoadMachineTypeDetails()
        {
            try
            {
                LogManager.WriteLog(ScreenName + "Load Machine Type details", LogManager.enumLogLevel.Info);
                lst_MCType = BuyMachineBiz.CreateInstance().GetMachineTypeDetails(null);
                cmbName.SelectedIndex = -1;
                cmbName.DataSource = lst_MCType;
                cmbName.DisplayMember = "Machine_Type_Code";
                cmbName.ValueMember = "Machine_Type_ID";
                if (_MachineTypeID > 0)
                {
                    int ind = lst_MCType.FindIndex(se => se.Machine_Type_ID == _MachineTypeID);
                    cmbName.SelectedIndex = ind;
                }



            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        /// <summary>
        /// Site Icon Details
        /// </summary>
        private void LoadSiteIconDetails()
        {
            try
            {
                LogManager.WriteLog(ScreenName + "Load Site Icon details", LogManager.enumLogLevel.Info);

                lst_site = AssetManagementBiz.CreateInstance().GetSiteIconDetails();
                cmbSiteIcon.DisplayMember = "Machine_Site_Icon_Display";
                cmbSiteIcon.ValueMember = "SiteIconID";
                cmbSiteIcon.DataSource = lst_site;


            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void cmbName_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (cmbName.SelectedIndex == -1)
                return;
            try
            {
                if (cmb_nameindex != cmbName.SelectedIndex)
                {
                    GetMachineTypeDetailsResult MType = cmbName.SelectedItem as GetMachineTypeDetailsResult;
                    if (MType != null)
                    {
                        int ind = 0;
                        List<DepreciationEntity> lst_Depreciation = (List<DepreciationEntity>)cmbDepreciation.DataSource;
                        if (lst_Depreciation != null && lst_Depreciation.Count > 0)
                        {
                            ind = lst_Depreciation.FindIndex(se => se.Depreciation_Policy_ID == MType.Depreciation_Policy_ID);
                            cmbDepreciation.SelectedIndex = ind;
                        }

                        List<GetSiteIconDetailsResult> lst_SiteIcon = (List<GetSiteIconDetailsResult>)cmbSiteIcon.DataSource;
                        if (lst_SiteIcon != null && lst_SiteIcon.Count > 0)
                        {
                            ind = lst_SiteIcon.FindIndex(se => se.Machine_Type_Site_Icon == MType.Machine_Type_Site_Icon);
                            cmbSiteIcon.SelectedIndex = ind;
                        }

                        txtDescription.Text = MType.Machine_Type_Description;
                        txtIncomeLedgerCode.Text = MType.Machine_Type_Income_Ledger_Code;
                        txtCMPGatewayCode.Text = MType.Machine_Type_AMEDIS_ID;
                        chkNGA.Checked = (MType.IsNonGamingAssetType == 1);
                    }
                    cmb_nameindex = cmbName.SelectedIndex;
                }
                objDatawatcher.DataModify = false;
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
                NewType = false;
                Edited = true;
                EnableControls(false);
                objDatawatcher.DataModify = false;
                if (cmbSiteIcon.SelectedIndex < 0)
                {
                    cmbSiteIcon.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

        }
        //private void ClearControls()
        //{
        //    txtCMPGatewayCode.Text = "";
        //    txtDescription.Text = "";
        //    txtIncomeLedgerCode.Text = "";
        //    txtName.Text = "";
        //    chkNGA.Checked = false;
        //    if (cmbDepreciation.Items.Count > 0)
        //    {
        //        cmbDepreciation.SelectedIndex = 0;
        //    }
        //}

        private void btnNew_Click(object sender, EventArgs e)
        {
            try
            {
                NewType = true;
                Edited = false;

                if (cmbSiteIcon.Items.Count > 0)
                {
                    cmbSiteIcon.SelectedIndex = 0;
                }

                EnableControls(false);
                objDatawatcher.DataModify = false;
                txtName.Focus();
                txtName.TabIndex = 1;
                txtDescription.TabIndex = 2;
                cmbDepreciation.TabIndex = 3;
                txtIncomeLedgerCode.TabIndex = 4;
                txtCMPGatewayCode.TabIndex = 5;
                chkNGA.TabIndex = 6;
                cmbSiteIcon.TabIndex = 7;
                btnUpdate.TabIndex = 8;
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
                LogManager.WriteLog(ScreenName + "btnCancel_Click:Form Closing", LogManager.enumLogLevel.Debug);
                switch (btnCancel.Tag.ToString())
                {
                    case "Key_CancelCaption": //"&Cancel":
                        //frmAddMachineType_Load(this, null);
                        btnCancel.Text = this.GetResourceTextByKey("Key_CloseCaption");                      
                        break;

                    case "Key_CloseCaption": //"&Close":
                        this.Close();
                        break;
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
                if ((NewType || !NewType) && txtName.Text.Trim() == string.Empty)
                {
                    ErrorMsg = this.GetResourceTextByKey(1,"MSG_ADDMCTYPE_ENTERMCTYPE");// "Please enter a machine type name";
                    retVal = false;
                }
                else if (!NewType && lst_MCType != null)
                {
                    List<GetMachineTypeDetailsResult> lstNew = lst_MCType.FindAll(obj => obj.Machine_Type_ID != _MachineTypeID && obj.Machine_Type_Code.ToUpper() == txtName.Text.Trim().ToUpper());
                    if (lstNew != null && lstNew.Count > 0)
                    {
                        ErrorMsg = this.GetResourceTextByKey(1, "MSG_ADDMCTYPE_TYPE") + "' " + txtName.Text.Trim() + "' " + this.GetResourceTextByKey(1, "MSG_ADDMCTYPE_ALREADYEXISTS");// "Type "+"' "+ txtName.Text.Trim() +"' "+"already exists."+ " Please Enter Another";
                        retVal = false;
                    }
                }
                else if (lst_MCType != null)
                {
                    List<GetMachineTypeDetailsResult> lstNew = lst_MCType.FindAll(obj => obj.Machine_Type_Code.ToUpper() == txtName.Text.Trim().ToUpper());
                    if (lstNew != null && lstNew.Count > 0)
                    {
                        ErrorMsg = this.GetResourceTextByKey(1, "MSG_ADDMCTYPE_TYPE") + "' " + txtName.Text.Trim() + "' " + this.GetResourceTextByKey(1, "MSG_ADDMCTYPE_ALREADYEXISTS");// "Type "+"' " + txtName.Text.Trim()+"' "+"already exists." + " Please Enter Another";
                        retVal = false;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                retVal = false;
                ErrorMsg = this.GetResourceTextByKey(1, "MSG_ADDMCTYPE_VALIDATEFAILURE");//"Unable to Validate Controls";
            }
            return retVal;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            cmbName.Focus();
           
            string ErrorMsg = "";
            try
            {
                if (ValidateControls(ref  ErrorMsg))
                {
                    if (cmbSiteIcon.SelectedIndex < 0)
                    {
                        Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_ENT_MAC_SITE_ICON"),this.Text);
                        cmbSiteIcon.Focus();
                        return;
                    }
                    if (NewType)
                    {
                        _MachineTypeID = 0;
                    }
                    int? MachineTypeID = _MachineTypeID;
                    GetSiteIconDetailsResult SiteIcon = cmbSiteIcon.SelectedItem as GetSiteIconDetailsResult;
                    int? Dep_PolicyID = (int?)cmbDepreciation.SelectedValue;
                    Dep_PolicyID = Dep_PolicyID == -1 ? null : Dep_PolicyID;
                    if (AssetManagementBiz.CreateInstance().UpdateMachineType(ref MachineTypeID, Dep_PolicyID, txtName.Text.Trim(), txtDescription.Text.Trim(),
                        0, SiteIcon.Machine_Type_Site_Icon, txtIncomeLedgerCode.Text.Trim(), txtCMPGatewayCode.Text.Trim(), chkNGA.Checked ? 1 : 0))
                    {
                        _MachineTypeID = MachineTypeID.Value;
                        LogManager.WriteLog(ScreenName + "btnUpdate_Click:Machine Type " + (NewType ? "added" : "modified") + "successfully; MachineID:" + _MachineTypeID, LogManager.enumLogLevel.Info);
                        AuditViewerBusiness business = new AuditViewerBusiness(DatabaseHelper.GetConnectionString());
                        business.InsertAuditData(new Audit.Transport.Audit_History
                        {
                            EnterpriseModuleName = ModuleNameEnterprise.AUDIT_MACHINETYPE,
                            Audit_Slot = "",
                            Audit_Screen_Name = "Add Machine Type",
                            Audit_Field = "Machine_Type_Code",
                            Audit_Old_Vl = "",
                            Audit_New_Vl = txtName.Text,
                            Audit_Desc = "Add MachineType Machine ID " + (NewType ? "added " : "modified ") + _MachineTypeID,
                            AuditOperationType = (NewType ? OperationType.ADD : OperationType.MODIFY),
                            Audit_User_ID = AppEntryPoint.Current.UserId,
                            Audit_User_Name = AppEntryPoint.Current.UserName
                        }, false);
                        frmAddMachineType_Load(this, null);
                    }
                    else
                    {
                        Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_ENT_MAC_UPDATE_MACHINE_DETAILS"),this.Text);
                        LogManager.WriteLog(ScreenName + "btnUpdate_Click:Unable to update Machine Type ID:" + _MachineTypeID, LogManager.enumLogLevel.Info);
                    }
                }
                else
                {
                    Win32Extensions.ShowInfoMessageBox(this, ErrorMsg,this.Text);
                }
            }
            catch (Exception ex)
            {
                Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_ENT_MAC_UPDATE_MACHINE_DETAILS"), this.Text);
                ExceptionManager.Publish(ex);
            }
        }


        private void cmbSiteIcon_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbSiteIcon.SelectedIndex != -1 && (lst_site != null && lst_site.Count > 0))
                {

                    GetSiteIconDetailsResult icon_det = lst_site.Find(se => se.SiteIconID == (int)cmbSiteIcon.SelectedValue);
                    if (icon_det != null)
                    {
                        try
                        {
                            picSlotIcon.Image = Image.FromFile(Environment.CurrentDirectory + System.IO.Path.DirectorySeparatorChar + icon_det.SiteIconPath, true);
                        }
                        catch
                        {
                            picSlotIcon.Image = picSlotIcon.ErrorImage;
                            //MessageBox.Show("Image Not Found", Branding, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
                objDatawatcher.DataModify = false;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void txtCMPGatewayCode_TextChanged(object sender, EventArgs e)
        {
            try
            {

                string strAllowedChar = "[^_a-zA-Z0-9]";
                if ((txtCMPGatewayCode.Text.Trim() != "") && (new Regex(strAllowedChar).IsMatch(txtCMPGatewayCode.Text)))
                {
                    Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_ENT_MAC_APHA_ONLY"), this.Text);
                    txtCMPGatewayCode.Text = "";
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

        }

        

    }
}
