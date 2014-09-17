using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BMC.EnterpriseBusiness.Business;
using BMC.Common.LogManagement;
using BMC.EnterpriseBusiness.Entities;
using BMC.Common.ExceptionManagement;
using Audit.BusinessClasses;
using Audit.Transport;
using BMC.CoreLib.Win32;
using BMC.Common;

namespace BMC.EnterpriseClient.Views
{
    public partial class frmMachineModelAdminNGA : BMC.EnterpriseClient.Helpers.BMCExtendedForm
    {
        public int MachineClassID
        {
            get;
            set;
        }
        public int MachineTypeID
        {
            get;
            set;
        }

        private const string ScreenName = "MachineModelAdmin NGA => ";
         BMC.EnterpriseClient.Helpers.Datawatcher objDatawatcher = null;


        private bool RefreshFlag = false;
        private bool _NewCategory = false;
        Action act_Refresh = null;
        public frmMachineModelAdminNGA(Action act_ref)
        {
            InitializeComponent();
            SetTagProperty();
            act_Refresh = act_ref;
            objDatawatcher = new Helpers.Datawatcher(this,
                (w, f) =>
                {
                    w.RemoveControlFromWatcher((f as frmMachineModelAdminNGA).cmbManufacturerID);
                    w.RemoveControlFromWatcher((f as frmMachineModelAdminNGA).cmbManufacturerID);
                    w.RemoveControlFromWatcher((f as frmMachineModelAdminNGA).cmbMachineTypeID);
                    w.RemoveControlFromWatcher((f as frmMachineModelAdminNGA).cmbCategory);
                    w.RemoveControlFromWatcher((f as frmMachineModelAdminNGA).cmbDepreciationPolicy);


                });
        }

        /// <summary>
        /// Assigns the Resource Key names to the controls--Created by kishore sivagnanam
        /// </summary>
        public void SetTagProperty()
        {
            this.btnCancel.Tag = "Key_CancelCaption";
            this.btnSave.Tag = "Key_SaveCaption";
            this.lblCategory.Tag = "Key_CategoryColon";
            this.chkDeListedModel.Tag = "Key_DeListedModel";
            this.lblDepreciationPolicy.Tag = "Key_DepreciationPolicyColon";
            this.Tag = "Key_MachineModelAdministration";
            this.lblManufacturer.Tag = "Key_ManufacturerColon";
            this.lblModelCode.Tag = "Key_ModelCodeColon";
            this.lblName.Tag = "Key_NameColon";
            this.gpNewMachineModel.Tag = "Key_NewMachineModelDetails";
            this.lblReleaseDate.Tag = "Key_ReleaseDateColon";
            this.lblSubCategory.Tag = "Key_SubCategoryColon";
            this.chkTestMachine.Tag = "Key_TestMachine";
            this.lblType.Tag = "Key_TypeColon";
            this.chkUseDepreciationDefault.Tag = "Key_UseDepreciationDefault";

        }

        private void frmMachineModelAdminNGA_Load(object sender, EventArgs e)
        {
            this.ResolveResources();
            _NewCategory = false;
            try
            {
                if (AssetManagementBiz.CreateInstance().CheckAutoModelCodeExists())
                {
                    txtModelCode.Enabled = false;
                    LogManager.WriteLog(ScreenName + "Check Auto_Generate_Model_Codes: TRUE", LogManager.enumLogLevel.Info);
                }
                else
                {
                    LogManager.WriteLog(ScreenName + "Check Auto_Generate_Model_Codes: FALSE", LogManager.enumLogLevel.Info);
                }
                if (MachineClassID == 0)
                {
                    int? MachineClass_ID = 0;            
                    AssetManagementBiz.CreateInstance().GetMachineClassID(ref MachineClass_ID, false);
                    MachineClassID = MachineClass_ID ?? 0;
                    LogManager.WriteLog(ScreenName + "frmMachineModelAdminNGA_Load:Created Temp MachineClassID:" + MachineClassID, LogManager.enumLogLevel.Info);
                    _NewCategory = (MachineClassID > 0);                    
                }
                LoadManufacturer();
                LoadDepreciationDetails();
                LoadMachineTypeDetails();
                //assign M\C model to controls
                AssignMachineModeltoCtrl();
                cmbMachineTypeID.Enabled = false;
                cmbCategory.Enabled = false;
                objDatawatcher.DataModify = false;
               // objDatawatcher = new Helpers.Datawatcher(this);
      

            }
            catch (Exception ex)
            {
                Win32Extensions.ShowInfoMessageBox(this,this.GetResourceTextByKey(1, "MSG_UNABLE_TO_RETRIEVE_MCMODEL"), this.Text);
                ExceptionManager.Publish(ex);
            }
        }

        private void AssignMachineModeltoCtrl()
        {
            try
            {
                LogManager.WriteLog(ScreenName + "Getting machine class details ID:" + MachineClassID, LogManager.enumLogLevel.Info);
                List<GetMachineClassListResult> lst_MCDetails = AssetManagementBiz.CreateInstance().GetMachineClassDetails(MachineClassID);
                if (lst_MCDetails != null && lst_MCDetails.Count > 0)
                {
                    GetMachineClassListResult MC = lst_MCDetails[0];
                    txtMachineName.Text = MC.Machine_Name;
                    int ind = -1;
                    if (!_NewCategory)
                    {

                        List<GetMachineTypeDetailsResult> lst_MCType = (List<GetMachineTypeDetailsResult>)cmbMachineTypeID.DataSource;
                        if (lst_MCType != null && lst_MCType.Count > 0)
                        {
                            ind = lst_MCType.FindIndex(obj => obj.Machine_Type_ID == MC.Machine_Type_ID);
                            cmbMachineTypeID.SelectedIndex = (ind >= 0) ? ind : 0;
                            cmbCategory.SelectedIndex = ind;
                        }
                    }

                    List<Manufacturer> lst_manf = (List<Manufacturer>)cmbManufacturerID.DataSource;
                    if (lst_manf != null && lst_manf.Count > 0)
                    {
                        ind = lst_manf.FindIndex(obj => obj.Manufacturer_ID == MC.Manufacturer_ID);
                        cmbManufacturerID.SelectedIndex = (ind >= 0) ? ind : 0;
                    }
                    txtModelCode.Text = MC.Machine_Class_Model_Code;
                    txtMachineCategory.Text = MC.Machine_Class_Category;

                    chkDeListedModel.Checked = MC.Machine_Class_DeListed ?? false;
                    chkTestMachine.Checked = MC.Machine_Class_Test_Machine ?? false;
                    chkUseDepreciationDefault.Checked = MC.Depreciation_Policy_Use_Default ?? false;

                    List<DepreciationEntity> lst_depreciation = (List<DepreciationEntity>)cmbDepreciationPolicy.DataSource;
                    if (lst_depreciation != null && lst_depreciation.Count > 0)
                    {
                        ind = lst_depreciation.FindIndex(obj => obj.Depreciation_Policy_ID == MC.Depreciation_Policy_ID);
                        cmbDepreciationPolicy.SelectedIndex = (ind >= 0) ? ind : 0;
                    }
                    else
                    {
                        cmbDepreciationPolicy.Enabled = false;
                    }
                    if (MC.Machine_Class_Release_Date != null && MC.Machine_Class_Release_Date.Length > 0)
                    {
                        dtReleaseDate.Value = Convert.ToDateTime(MC.Machine_Class_Release_Date);
                    }

                }
                
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        #region DBMethods
        /// <summary>
        /// Manufacturer Details
        /// </summary>
        private void LoadManufacturer()
        {
            try
            {

                cmbManufacturerID.Items.Clear();
                LogManager.WriteLog(ScreenName + "Load Manufacturer details", LogManager.enumLogLevel.Info);
                List<Manufacturer> lst_manf = BuyMachineBiz.CreateInstance().GetManufacturers();
                Manufacturer m_negative = lst_manf.Find(o => o.Manufacturer_ID == -1);
                if (m_negative != null)
                {
                    lst_manf.Remove(m_negative);
                }
                cmbManufacturerID.DataSource = lst_manf;
                cmbManufacturerID.DisplayMember = "Manufacturer_Name";
                cmbManufacturerID.ValueMember = "Manufacturer_ID";
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
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
                if (lst_depreciation == null)
                    return;
                cmbDepreciationPolicy.DataSource = lst_depreciation;
                cmbDepreciationPolicy.DisplayMember = "Depreciation_Policy_Description";
                cmbDepreciationPolicy.ValueMember = "Depreciation_Policy_ID";

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
                List<GetMachineTypeDetailsResult> lst_MCType = BuyMachineBiz.CreateInstance().GetMachineTypeDetails((MachineTypeID == 0 ? (int?)null : MachineTypeID));

                cmbMachineTypeID.DataSource = lst_MCType;
                cmbMachineTypeID.DisplayMember = "MCTypeDescription_NGA";
                cmbMachineTypeID.ValueMember = "Machine_Type_ID";

                cmbCategory.DataSource = lst_MCType;
                cmbCategory.DisplayMember = "MCTypeDescription_NGA";
                cmbCategory.ValueMember = "Machine_Type_ID";
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        } 
        #endregion

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string ErrorMsg = "";
                _NewCategory = false;
                RefreshFlag = true;
                if (Validate(ref ErrorMsg))
                {
                    LogManager.WriteLog(ScreenName + " btnSave_Click:", LogManager.enumLogLevel.Info);
                    int? MC_ClassID = MachineClassID;
                    if (AssetManagementBiz.CreateInstance().UpdateMachineClassDetails(txtMachineName.Text, GetIntValue(cmbManufacturerID.SelectedValue), txtModelCode.Text, chkDeListedModel.Checked, chkTestMachine.Checked,
                        txtMachineCategory.Text, GetIntValue(cmbDepreciationPolicy.SelectedValue), chkUseDepreciationDefault.Checked, BMC.Common.Utilities.Common.GetUniversalDate(dtReleaseDate.Value),
                        GetIntValue(cmbMachineTypeID.SelectedValue), AppEntryPoint.Current.UserId, (int)ModuleIDEnterprise.AUDIT_MACHINEMODEL, ModuleNameEnterprise.AUDIT_MACHINEMODEL.ToString(), ref MC_ClassID))
                    {
                        LogManager.WriteLog(ScreenName + "Machine Class Successfully Saved ID:" + MachineClassID, LogManager.enumLogLevel.Info);
                        MachineClassID = MC_ClassID.Value;
                        Win32Extensions.ShowInfoMessageBox(this,this.GetResourceTextByKey(1, "MSG_MODEL_FILE_EXP_COMPLETE"), this.Text);
                        objDatawatcher.DataModify = false;
                        this.Close();
                    }
                    else
                    {
                        LogManager.WriteLog(ScreenName + "Unable to save machine class details", LogManager.enumLogLevel.Error);
                        Win32Extensions.ShowInfoMessageBox(this,this.GetResourceTextByKey(1, "MSG_UNABLE_TO_UPDATE_MODEL"), this.Text);
                    }

                }
                else
                {
                    Win32Extensions.ShowInfoMessageBox(this,ErrorMsg, this.Text);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

        }

        int? GetIntValue(object obj)
        {
            int returnVal = 0;
            int? Actval = null;

            if (obj != null && int.TryParse(obj.ToString(), out returnVal))
            {
                Actval = returnVal;
            }
            return Actval;
        }

        /// <summary>
        /// Validate all controls and returns true if condition is Statsified
        /// </summary>
        /// <param name="ErrorMsg">returns Error Message</param>
        /// <returns></returns>
        bool Validate(ref string ErrorMsg)
        {
            bool retVal = true;
            try
            {
                char[] strKeyBoard = { '\'', '"', '%', '_', '`' };
                if (txtMachineName.Text.Trim() == "")
                {
                    ErrorMsg = this.GetResourceTextByKey(1,"MSG_MCMODEL_NAMENOTEMPTY");// "Machine name cannot be empty.";
                    retVal = false;
                }
                else if (txtMachineName.Text.IndexOfAny(strKeyBoard) != -1)
                {
                    ErrorMsg = this.GetResourceTextByKey(1,"MSG_MCMODEL_INVALIDNAME");//"Invalid data entered [Machine Name]";
                    retVal = false;
                }
                else if (txtModelCode.Text.IndexOfAny(strKeyBoard) != -1)
                {
                    ErrorMsg = this.GetResourceTextByKey(1,"MSG_MCMODEL_INVALIDMODELCODE");//"Invalid data entered [Model Code]";
                    retVal = false;
                }
                else if (txtMachineCategory.Text.IndexOfAny(strKeyBoard) != -1)
                {
                    ErrorMsg = this.GetResourceTextByKey(1,"MSG_MCMODEL_INVALIDSUBCATEGORY");//"Invalid data entered [Sub Category]";
                    retVal = false;
                }
                else if ((int)cmbManufacturerID.SelectedValue <= 0)
                {
                    ErrorMsg = this.GetResourceTextByKey(1,"MSG_MCMODEL_SELECTMANUFACTURER");//"Manufacturer should be selected";
                    retVal = false;
                }

                if (retVal)
                {
                    bool? ModelCodeExists = false;
                    bool? MachineNameExists = false;
                    AssetManagementBiz.CreateInstance().CheckModelCodeAndMachineExists(MachineClassID, txtModelCode.Text.Trim(), txtMachineName.Text.Trim(), ref ModelCodeExists, ref MachineNameExists);
                    if (ModelCodeExists ?? false)
                    {
                        ErrorMsg = this.GetResourceTextByKey(1,"MSG_MCMODEL_MODELCODE") + txtModelCode.Text.Trim() + this.GetResourceTextByKey(1,"MSG_MCMODEL_ALREADYEXISTS");//"Model Code [" + txtModelCode.Text.Trim() + "] already exists.";
                        retVal = false;
                    }
                    if (MachineNameExists ?? false)
                    {
                        ErrorMsg = this.GetResourceTextByKey(1,"MSG_MCMODEL_MACHINENAME") + txtModelCode.Text.Trim() + this.GetResourceTextByKey(1,"MSG_MCMODEL_ALREADYEXISTS");// "Machine Name [" +txtMachineName.Text.Trim()+ "] already exists.";
                        retVal = false;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                ErrorMsg = ex.Message;
                retVal = false;
            }
            return retVal;

        }

        private void chkUseDepreciationDefault_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                cmbDepreciationPolicy.Enabled = !chkUseDepreciationDefault.Checked;
                if (!chkUseDepreciationDefault.Checked)
                {

                    if (cmbMachineTypeID.SelectedIndex >= 0)
                    {
                        //List<GetMachineTypeDetailsResult> lst_MCType = (List<GetMachineTypeDetailsResult>)cmbMachineTypeID.DataSource;
                        //GetMachineTypeDetailsResult Mtype = lst_MCType.Find(obj => obj.Machine_Type_ID == MC.Machine_Type_ID);
                        GetMachineTypeDetailsResult Mtype = (GetMachineTypeDetailsResult)cmbMachineTypeID.SelectedItem;
                        // GetMachineTypeDetailsResult Mtype = lst_MCType.Find(obj => obj.Machine_Type_ID == MC.Machine_Type_ID);
                        List<DepreciationEntity> lst_depreciation = (List<DepreciationEntity>)cmbDepreciationPolicy.DataSource;
                        if (lst_depreciation != null && lst_depreciation.Count > 0)
                        {
                            int ind = lst_depreciation.FindIndex(obj => obj.Depreciation_Policy_ID == Mtype.Depreciation_Policy_ID);
                            cmbDepreciationPolicy.SelectedIndex = (ind >= 0) ? ind : 0;
                        }
                        else
                        {
                            cmbDepreciationPolicy.SelectedIndex = -1;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void txtModelCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsLetter(e.KeyChar))
            {
                e.KeyChar = Char.ToUpper(e.KeyChar);
            }
        }

        private void frmMachineModelAdminNGA_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (_NewCategory)
                {
                    int? MachineClass_ID = MachineClassID;
                    AssetManagementBiz.CreateInstance().GetMachineClassID(ref MachineClass_ID, true);
                    LogManager.WriteLog(ScreenName + "frmMachineModelAdminNGA_FormClosing:Delete Temp MachineClassID:" + MachineClassID, LogManager.enumLogLevel.Info);

                }
                _NewCategory = false;
                LogManager.WriteLog(ScreenName + "btnCancel_Click: Form Closing...", LogManager.enumLogLevel.Debug);
                if (act_Refresh != null && RefreshFlag)
                {
                    act_Refresh();
                }
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
