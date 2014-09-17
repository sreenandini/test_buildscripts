using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BMC.EnterpriseBusiness.Entities;
using BMC.EnterpriseBusiness.Business;
using Audit.Transport;
using BMC.Common.ExceptionManagement;
using BMC.Security;
using BMC.EnterpriseClient.Helpers;
using System.Globalization;
using BMC.Common;
namespace BMC.EnterpriseClient.Views
{

    #region Delegates

    public delegate void delLoadFinanceDetails(int VaultID, VaultDetails objVaultDetails);

    public delegate void AddRemoveControl();

    #endregion

    public partial class frmAddVault : UserControl
    {

        #region Declared Variables
        public AddRemoveControl OnNewVault; //To remove tab pages when new vault is being created
        public AddRemoveControl OnCancelNewVault; //To recreate the removed tab pages
        public TabControl ParentTab;
        public delLoadFinanceDetails loadFinanceDetails;
        VaultAdmin _VaultBiz;
        public VaultDetails _SelectedVault = new VaultDetails();
        bool _bIsEditPermissionEnabled = false; //Edit premission for the form
        List<Vault_GetAllDevices> _Vault; //Master list will not be modified
        List<Vault_GetAllDevices> _lst_SortedVaults = null; //Copy of master list and will be modified for sorting 
        ListViewColumnSorter _lvwColumnSorter = null; //for sorting the listview column
        int NoofCaassette = 6; //Default number of cassetees can be created
        int NoOfHoppers = 3; //Default number of hoppers can be created
        int _SiteCode;
        Vault_GetAllDevices objGetVaultDetails = null;
        bool _isDefaultDataModified = false;
        string _strCurrency = "$";
        bool _bIsTerminateMachineEnabled = false;

        #endregion

        #region Constructor

        public frmAddVault()
        {
            InitializeComponent();
            setTagProperty();
        }

        private void setTagProperty()
        {
            this.btnHoppers.Tag = "Key_HoppersCaption";
            this.btnNew.Tag = "Key_NewCaption";
            this.btnRejection.Tag = "Key_RejectionCassette";
            this.btn_Save.Tag = "Key_SaveCaption";
            this.btn_Terminate.Tag = "Key_Terminate";
            this.label6.Tag = "Key_AlertLevelMandatoryColon";
            this.lblDescription.Tag = "Key_DescriptionMandatory";
            this.lbl_Manaufacturer.Tag = "Key_ManufacturerMandatory";
            this.label4.Tag = "Key_NameMandatory";
            this.label5.Tag = "Key_SerialNumberMandatory";
            this.lbl_StandardFillAmount.Tag = "Key_StandardFillAmountMandatory";
            this.lbl_Type.Tag = "Key_TypeMandatory";
            this.Chk_Active.Tag = "Key_Active";
            this.chkAutoAdjust.Tag = "Key_Autoadjustondrop";
            this.btnCassettes.Tag = "Key_Cassettes";
            this.lbl_Capacity.Tag = "Key_CapacityColon";
            this.mnuSubitemCopy.Tag = "Key_CopyAsset";
            this.chkRejectionFill.Tag = "Key_FillRejection";
            this.lbl_NoofCassettes.Tag = "Key_NoofCassettesColon";
            this.lbl_CoinHoppers.Tag = "Key_NoofCoinHoppersColon";
            this.txt_SearchVault.Tag = "Key_SearchText";
            this.clmSerialNo.Tag = "Key_SerialNumberHeader";
            this.clmSiteCode.Tag = "Key_SiteCode";
            this.clmSiteName.Tag = "Key_SiteName";
            this.clmStatus.Tag = "Key_Status";
            this.lbl_VaultStatus.Tag = "Key_StatusColon";
            this.lbl_Header.Tag = "Key_VaultAdmin";
            this.lbl_VaultDetails.Tag = "Key_VaultDetailsColon";
            this.lblIsWebserviceEnabled.Tag = "Key_VaultInterfaceEnabled";
            this.clmVaultName.Tag = "Key_VaultName";
            this.lbl_Sites.Tag = "Key_VaultsColon";
            this.cbo_Status.Items.Add(this.GetResourceTextByKey("Key_All"));
            this.cbo_Status.Items.Add(this.GetResourceTextByKey("Key_Active"));
            this.cbo_Status.Items.Add(this.GetResourceTextByKey("Key_Assigned"));
            this.cbo_Status.Items.Add(this.GetResourceTextByKey("Key_Unassigned"));

        }

        #endregion

        #region Events
        /// <summary>
        /// Check Edit Permission for Controls
        /// Load Manufacturers in the Combo box
        /// Load Vault devices in the Listview
        /// Get the number of cassettes and hoppers from settings
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmFillVault_Load(object sender, EventArgs e)
        {
            try
            {
                this.ResolveResources();
                _strCurrency = new RegionInfo(BMC.Common.ConfigurationManagement.ConfigManager.Read("GetDefaultCultureForCurrency")).CurrencySymbol;
                lbl_Capacity.Text = this.GetResourceTextByKey("Key_CapacityColon").Replace(":","")+ "(" + _strCurrency + ") :";
                lbl_StandardFillAmount.Text = this.GetResourceTextByKey("Key_StandardFillAmount")+ "(" + _strCurrency + ") :";
                cbo_Status.SelectedIndex = 0;
                _VaultBiz = new VaultAdmin();
                _lvwColumnSorter = new ListViewColumnSorter();
                lvVaultdetails.ListViewItemSorter = _lvwColumnSorter;

                _bIsEditPermissionEnabled = AppGlobals.Current.HasUserAccess("HQ_Admin_EditCreateVault");
                _bIsTerminateMachineEnabled = AppGlobals.Current.HasUserAccess("HQ_Admin_TerminateVault");
                //Load all Manufacturers
                LoadManufacturers();
                btn_Terminate.Enabled = _bIsTerminateMachineEnabled;
                //initially load the vault details in the listview
                this.LoadVaultDetails();

                //Get Cassette and Hopper Details
                GetCassetteAndHopperDetails();

                if (System.Configuration.ConfigurationManager.AppSettings["ShowCopyVault"].NullToString().ToUpper() == "TRUE")
                {
                    lvVaultdetails.ContextMenuStrip = mnuCopyAsset;
                }

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        //Get the number of cassettes and hoppers from settings
        private void GetCassetteAndHopperDetails()
        {
            NoofCaassette = SettingsEntity.MaxNoOfVaultCassettes;
            NoOfHoppers = SettingsEntity.MaxNoOfVaultHoppers;
        }

        private void lvVaultdetails_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //load the selected vault data details
                LoadSelectedVaultDetails();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnNew.Tag.ToString() == "Key_NewCaption")
                {
                    ClearControls();
                    btnNew.Tag = "Key_CancelCaption";
                    btnNew.Text = this.GetResourceTextByKey("Key_CancelCaption");
                    btn_Terminate.Enabled = false;
                    btn_Save.Enabled = true && _bIsEditPermissionEnabled;
                    lvVaultdetails.Enabled = false;
                    txt_SearchVault.Enabled = false;

                    _SelectedVault = new VaultDetails() { Vault_ID = 0 };
                    objGetVaultDetails = null;
                    this.EnableDisableAllControls(true);
                    this.CustomiseLabelsForWebService(_SelectedVault.IsWebServiceEnabled);
                    if (OnNewVault != null)
                        this.OnNewVault();
                    txt_DeviceName.Focus();
                }
                else if (btnNew.Tag.ToString() == "Key_CancelCaption")
                {
                    lvVaultdetails.Enabled = true;
                    txt_SearchVault.Enabled = true;
                    btn_Terminate.Enabled = true && _bIsTerminateMachineEnabled;
                    btnNew.Tag = "Key_NewCaption";
                    btnNew.Text = this.GetResourceTextByKey("Key_NewCaption");
                    this.CustomiseLabelsForWebService(true);
                    LoadVaultDetails();
                    if (OnCancelNewVault != null)
                        this.OnCancelNewVault();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            try
            {
                //Will be validating the controls and updating the vault details
                UpdateVaultDetails();
            }
            catch (Exception Ex)
            {
                btnNew.Tag = "Key_NewCaption";
                btnNew.Text = this.GetResourceTextByKey("Key_NewCaption");
                lvVaultdetails.Enabled = true;
                ExceptionManager.Publish(Ex);
                btn_Terminate.Enabled = true && _bIsTerminateMachineEnabled;
                EnableDisableAllControls(false);
                LoadVaultDetails();
            }
        }

        private void chk_WebserviceEnabled_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (!chk_WebserviceEnabled.Checked)
                {
                    txt_Capacity.Enabled = true && _bIsEditPermissionEnabled;
                    txt_Coinhoppers.Enabled = true && _bIsEditPermissionEnabled;
                    txt_NoofCassettes.Enabled = true && _bIsEditPermissionEnabled;
                    txt_StandardFillAmount.Enabled = true && _bIsEditPermissionEnabled;
                    btnCassettes.Enabled = false;
                    btnHoppers.Enabled = false;
                    btnRejection.Enabled = false;
                    lblStatus.Text = string.Empty;
                    this.CustomiseLabelsForWebService(false);
                }
                else if (chk_WebserviceEnabled.Checked && _SelectedVault.IsWebServiceEnabled)
                {
                    txt_Capacity.Enabled = false;
                    txt_Coinhoppers.Enabled = false;
                    txt_NoofCassettes.Enabled = false;
                    txt_StandardFillAmount.Enabled = false;
                    btnCassettes.Enabled = true;
                    btnHoppers.Enabled = true;
                    btnRejection.Enabled = true;
                    this.CustomiseLabelsForWebService(true);
                    lblStatus.Text = string.Empty;
                }
                else if (chk_WebserviceEnabled.Checked && !_SelectedVault.IsWebServiceEnabled)
                {
                    txt_Capacity.Enabled = false;
                    txt_Coinhoppers.Enabled = false;
                    txt_NoofCassettes.Enabled = false;
                    txt_StandardFillAmount.Enabled = false;
                    btnCassettes.Enabled = false;
                    btnHoppers.Enabled = false;
                    btnRejection.Enabled = false;
                    this.CustomiseLabelsForWebService(false);
                    lblStatus.Text = this.GetResourceTextByKey(1, "MSG_SAVE_VAULTDETAILS");
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btnCassettes_Click(object sender, EventArgs e)
        {
            try
            {
                this.ShowCassetteHopperDef(CassetteType.Cassette, _SelectedVault.IsAssigned);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btnHoppers_Click(object sender, EventArgs e)
        {
            try
            {
                this.ShowCassetteHopperDef(CassetteType.Hopper, _SelectedVault.IsAssigned);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
        private void btnRejection_Click(object sender, EventArgs e)
        {
            try
            {
                this.ShowCassetteHopperDef(CassetteType.Rejection, _SelectedVault.IsAssigned);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btn_Terminate_Click(object sender, EventArgs e)
        {
            try
            {
                if (objGetVaultDetails != null)
                {
                    frmTerminateVault objTerminate = new frmTerminateVault(objGetVaultDetails);
                    objTerminate.ShowDialog();
                    if (objTerminate.DialogResult == DialogResult.Yes)
                    {
                        LoadVaultDetails();
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void cbo_Status_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                FilterWithOrdering();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void lvVaultdetails_ColumnClick(object sender, ColumnClickEventArgs e)
        {

            if (e.Column == _lvwColumnSorter.SortColumn)
            {
                // Reverse the current sort direction for this column.
                if (_lvwColumnSorter.Order == System.Windows.Forms.SortOrder.Ascending)
                {
                    _lvwColumnSorter.Order = System.Windows.Forms.SortOrder.Descending;
                }
                else
                {
                    _lvwColumnSorter.Order = System.Windows.Forms.SortOrder.Ascending;
                }
            }
            else
            {
                // Set the column number that is to be sorted; default to ascending.
                _lvwColumnSorter.SortColumn = e.Column;
                _lvwColumnSorter.Order = System.Windows.Forms.SortOrder.Ascending;
            }
            ListView lst_view = sender as ListView;
            lst_view.Sort();
        }

        private void txt_SearchSites_TextChanged(object sender, EventArgs e)
        {
            if (txt_SearchVault.Tag.ToString() != "0")
            {
                if (_Vault.Count > 0 && txt_SearchVault.Text != "")
                {
                    FilterVault();
                }
                else
                {
                    FilterWithOrdering();
                }
            }
        }

        private void txt_SearchSites_Enter(object sender, EventArgs e)
        {
            try
            {
                if (txt_SearchVault.Text == this.GetResourceTextByKey("Key_Search") && txt_SearchVault.ForeColor == SystemColors.GrayText)
                {
                    txt_SearchVault.Tag = "1";
                    txt_SearchVault.Text = string.Empty;
                    txt_SearchVault.ForeColor = Color.Black;
                }
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
                Win32Extensions.ShowErrorMessageBox(this, this.GetResourceTextByKey(1, "MSG_ENT_VAU_ERROR"), this.ParentForm.Text);
            }
        }

        private void txt_SearchSites_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txt_SearchVault.Text.Trim() == "")
                {
                    txt_SearchVault.Tag = "0";
                    txt_SearchVault.Text = this.GetResourceTextByKey("Key_Search");
                    txt_SearchVault.ForeColor = SystemColors.GrayText;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                Win32Extensions.ShowErrorMessageBox(this, this.GetResourceTextByKey(1, "MSG_ENT_VAU_ERROR"), this.ParentForm.Text);
            }
        }

        #region Additional Event Methods

        private void txt_AlertLevel_Enter(object sender, EventArgs e)
        {
            lblStatus.Text = this.GetResourceTextByKey(1,"MSG_ALERTLEVEL");
        }

        private void txt_AlertLevel_Leave(object sender, EventArgs e)
        {
            lblStatus.Text = "";
        }

        private void txt_Capacity_Enter(object sender, EventArgs e)
        {
            //lblStatus.Text = "Enter capacity level between 1 to 99999999.99";
        }

        private void txt_Capacity_Leave(object sender, EventArgs e)
        {
            //blStatus.Text = "";
        }

        private void txt_SerialNo_Enter(object sender, EventArgs e)
        {

            //lblStatus.Text = "Serial number should not be more than 20 digits";
        }

        private void txt_SerialNo_Leave(object sender, EventArgs e)
        {
            //lblStatus.Text = "";
        }

        private void txt_DeviceName_Enter(object sender, EventArgs e)
        {
            //lblStatus.Text = "Device name should not be more than 50 characters";
        }

        private void txt_DeviceName_Leave(object sender, EventArgs e)
        {
            //lblStatus.Text = "";
        }

        private void txt_NoofCassettes_Enter(object sender, EventArgs e)
        {
            lblStatus.Text =this.GetResourceTextByKey("Key_CasseteLevel") + NoofCaassette;
        }

        private void txt_NoofCassettes_Leave(object sender, EventArgs e)
        {
            lblStatus.Text = "";
        }

        private void txt_Coinhoppers_Enter(object sender, EventArgs e)
        {
            lblStatus.Text =this.GetResourceTextByKey("Key_HoppersLevel") + NoOfHoppers;
        }

        private void txt_Coinhoppers_Leave(object sender, EventArgs e)
        {
            lblStatus.Text = "";
        }

        #endregion

        #endregion

        #region Load Methods

        /// <summary>
        /// Load Manufacturers in the Manufacturers Combo Box
        /// </summary>
        private void LoadManufacturers()
        {
            try
            {
                List<VaultManufacturers> lstManufacturers = _VaultBiz.GetAllManufacturers();
                cmb_Manufacturer.DataSource = lstManufacturers;
                cmb_Manufacturer.DisplayMember = "Manufacturer_Name";
                cmb_Manufacturer.ValueMember = "Manufacturer_ID";
                cmb_Manufacturer.SelectedIndex = 0;
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
        }

        public void LoadVaultDetails()
        {
            try
            {
                ClearControls();
                int SelectedVaultIndex = 0;
                lvVaultdetails.Items.Clear();
                _lst_SortedVaults = _VaultBiz.LoadVaultDetails(AppGlobals.Current.UserId);
                _Vault = _lst_SortedVaults;

                if (_lst_SortedVaults.Count != 0)
                {
                    foreach (Vault_GetAllDevices items in _lst_SortedVaults)
                    {
                        ListViewItem lvItem = new ListViewItem(items.Vault.ToString());
                        lvItem.Tag = items;
                        lvItem.SubItems.Add(new ListViewItem.ListViewSubItem(lvItem, items.Serial_No.ToString()));
                        lvItem.SubItems.Add(new ListViewItem.ListViewSubItem(lvItem, items.Site_Code.ToString()));
                        lvItem.SubItems.Add(new ListViewItem.ListViewSubItem(lvItem, items.Site_Name.ToString()));
                        if (items.Status.ToUpper() == "ACTIVE")
                        {
                            lvItem.SubItems.Add(new ListViewItem.ListViewSubItem(lvItem, "Active"));
                        }
                        else if (items.Status.ToUpper() == "ASSIGNED")
                        {
                            lvItem.SubItems.Add(new ListViewItem.ListViewSubItem(lvItem, "Assigned"));
                        }
                        else
                        {
                            lvItem.SubItems.Add(new ListViewItem.ListViewSubItem(lvItem, "Unassigned"));
                        }
                        lvVaultdetails.Items.Add(lvItem);

                        if (_SelectedVault.Vault_ID == items.Vault_ID)
                        {
                            SelectedVaultIndex = lvVaultdetails.Items.Count - 1;
                            btn_Terminate.Enabled = true && _bIsTerminateMachineEnabled;
                            btn_Save.Enabled = true && _bIsEditPermissionEnabled;
                        }
                        if (items.Active)
                        {
                            lvItem.ForeColor = Color.Red;
                        }

                    }
                    lvVaultdetails.Items[SelectedVaultIndex].Selected = true;
                    CustomiseLabelsForWebService(_SelectedVault.IsWebServiceEnabled);
                    lvVaultdetails.Columns[lvVaultdetails.Columns.Count - 1].Width = -2;
                }

                if (lvVaultdetails.Items.Count > 0)
                {
                    //this.EnableDisableAllControls(true);
                    txt_DeviceName.Focus();
                    lblStatus.Text = "";
                    btn_Terminate.Enabled = true && _bIsTerminateMachineEnabled;
                    btn_Save.Enabled = true && _bIsEditPermissionEnabled;
                    btnNew.Enabled = true && _bIsEditPermissionEnabled;
                }
                else
                {
                    this.EnableDisableAllControls(false);
                    txt_Capacity.Enabled = false;
                    txt_Coinhoppers.Enabled = false;
                    txt_NoofCassettes.Enabled = false;
                    txt_StandardFillAmount.Enabled = false;

                    btn_Terminate.Enabled = false;
                    btn_Save.Enabled = false;
                    btnNew.Enabled = true && _bIsEditPermissionEnabled;
                    if (_bIsEditPermissionEnabled)
                        btnNew.Focus();
                    lblStatus.Text = this.GetResourceTextByKey("Key_VaultsNotFound");
                    _SelectedVault = null;
                    if (loadFinanceDetails != null)
                    {
                        loadFinanceDetails(0, null);
                    }
                }
                FilterWithOrdering();
                txt_SearchVault.Enabled = true;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void LoadSelectedVaultDetails()
        {
            try
            {
                if (lvVaultdetails.SelectedItems.Count > 0)
                {
                    objGetVaultDetails = (Vault_GetAllDevices)lvVaultdetails.SelectedItems[0].Tag;
                    _SelectedVault = _VaultBiz.GetVaultDetails(objGetVaultDetails.Vault_ID);

                    //Assigning datas to the controls
                    txt_DeviceName.Text = _SelectedVault.NAME;
                    txt_SerialNo.Text = _SelectedVault.Serial_NO;
                    txt_AlertLevel.Text = _SelectedVault.Alert_Level.ToString();
                    txt_Capacity.Text = _SelectedVault.Capacity.ToString();
                    txt_Type.Text = _SelectedVault.Type_Prefix;
                    txt_NoofCassettes.Text = _SelectedVault.NoofCassettes.ToString();
                    txt_Coinhoppers.Text = _SelectedVault.NoofCoinHopper.ToString();
                    chk_WebserviceEnabled.Checked = _SelectedVault.IsWebServiceEnabled;
                    Chk_Active.Checked = _SelectedVault.Active;
                    cmb_Manufacturer.SelectedValue = _SelectedVault.Manufacturer_ID;
                    txt_StandardFillAmount.Text = _SelectedVault.StandaradFillAmount.ToString();
                    txtDescription.Text = _SelectedVault.Description;
                    _SiteCode = _SelectedVault.Site_ID;
                    chkAutoAdjust.Checked = _SelectedVault.AutoAdjustEnabled;
                    chkRejectionFill.Checked = _SelectedVault.FillRejection;

                    if (loadFinanceDetails != null)
                        loadFinanceDetails(objGetVaultDetails.Vault_ID, _SelectedVault);

                    _bIsEditPermissionEnabled = AppGlobals.Current.HasUserAccess("HQ_Admin_EditCreateVault") ;
                    _bIsTerminateMachineEnabled = AppGlobals.Current.HasUserAccess("HQ_Admin_TerminateVault") && !_SelectedVault.IsSiteUpdated;

                    if (_SelectedVault.IsAssigned)
                    {
                        EnableDisableAllControls(false);
                        btn_Save.Enabled = false && _bIsEditPermissionEnabled;

                    }
                    else
                    {
                        EnableDisableAllControls(true);
                        btn_Save.Enabled = true && _bIsEditPermissionEnabled;
                    }

                    if (!_SelectedVault.IsConfigured)
                    {
                        lblStatus.Text = this.GetResourceTextByKey("Key_HopperConfig");
                    }
                    else
                    {
                        lblStatus.Text = "";
                    }
                    btn_Terminate.Enabled = _bIsTerminateMachineEnabled;
                    CustomiseLabelsForWebService(_SelectedVault.IsWebServiceEnabled);

                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void ShowCassetteHopperDef(CassetteType Type, bool bIsAssigned)
        {
            try
            {
                if (_SelectedVault!=null)
                {
                    //int iListIndex = lvVaultdetails.SelectedItems[0].Index;
                    frmCassetteDetails Hoppers = new frmCassetteDetails(_SelectedVault.Vault_ID, Type, _SelectedVault);
                    Hoppers.ShowDialog();
                    LoadVaultDetails();
                    lvVaultdetails.Focus();
                    //lvVaultdetails.Items[iListIndex].Selected = true;
                    LoadSelectedVaultDetails(); 
                }
            }
            catch (Exception Ex)
            {

                ExceptionManager.Publish(Ex);
            }
        }

        #endregion

        #region UpdateMethods

        private void UpdateVaultDetails()
        {
            if (ValidateControls())
            {
                int tempVaultID = _SelectedVault.Vault_ID;
                _SelectedVault.Vault_ID = _VaultBiz.UpdateDevice(_SelectedVault.Vault_ID,
                                                               txt_DeviceName.Text.Trim(),
                                                               txt_SerialNo.Text.Trim(),
                                                               Chk_Active.Checked,
                                                               _SiteCode,
                                                               txt_AlertLevel.CTL_IntValue,
                                                               AppGlobals.Current.UserId,
                                                               (int)ModuleIDEnterprise.VaultManager,
                                                               ModuleIDEnterprise.VaultManager.ToString(),
                                                               "Vault Device Admin",
                                                               (int)cmb_Manufacturer.SelectedValue,
                                                               txt_Type.Text.Trim(),
                                                               txt_Capacity.CTL_DecimalValue,
                                                               txt_Coinhoppers.CTL_IntValue,
                                                               txt_NoofCassettes.CTL_IntValue,
                                                               chk_WebserviceEnabled.Checked,
                                                               "VAULT",
                                                               txtDescription.Text,
                                                               txt_StandardFillAmount.CTL_DecimalValue,
                                                               chkAutoAdjust.Checked,
                                                               chkRejectionFill.Checked);

                if (_SelectedVault.Vault_ID == -10)
                {
                    Win32Extensions.ShowWarningMessageBox(this, this.GetResourceTextByKey(1, "MSG_ENT_VAU_DEVICE_EXIST"), this.ParentForm.Text);
                    _SelectedVault.Vault_ID = tempVaultID;
                    txt_DeviceName.Focus();
                    return;
                }
                if (_SelectedVault.Vault_ID == -11)
                {
                    Win32Extensions.ShowWarningMessageBox(this, this.GetResourceTextByKey(1, "MSG_ENT_VAU_SERIAL_EXIST"), this.ParentForm.Text);
                    txt_SerialNo.Focus();
                    _SelectedVault.Vault_ID = tempVaultID;
                    return;
                }

                if (_SelectedVault.Vault_ID > 0)
                {
                    btnCassettes.Enabled = true;
                    btnHoppers.Enabled = true;
                    btnRejection.Enabled = true;
                    Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_ENT_VAU_SAVE_SUCCESS"), this.ParentForm.Text);
                }

                if (OnCancelNewVault != null)
                    this.OnCancelNewVault();
                LoadVaultDetails();
                btnNew.Tag = "Key_NewCaption";
                btnNew.Text = this.GetResourceTextByKey("Key_NewCaption");
                lvVaultdetails.Enabled = true;

                //Call main form method to load finance details 
                if (loadFinanceDetails != null)
                {
                    loadFinanceDetails(_SelectedVault.Vault_ID, _SelectedVault);
                }
            }
        }

        #endregion

        #region Supporting Methods

        private void ClearControls()
        {
            txt_DeviceName.Text = string.Empty;
            txt_SerialNo.Text = string.Empty;
            txt_AlertLevel.Text = string.Empty;
            txt_Capacity.Text = string.Empty;
            txt_Type.Text = string.Empty;
            txt_NoofCassettes.Text = string.Empty; ;
            txt_StandardFillAmount.Text = string.Empty;
            txt_Coinhoppers.Text = string.Empty;
            txtDescription.Text = string.Empty;
            chk_WebserviceEnabled.Checked = false;
            Chk_Active.Checked = false;
            cmb_Manufacturer.SelectedValue = -1;
            chkAutoAdjust.Checked =false;
            chkRejectionFill.Checked = false;
        }

        private bool ValidateControls()
        {
            if (txt_DeviceName.Text.Trim() == string.Empty)
            {
                Win32Extensions.ShowWarningMessageBox(this, this.GetResourceTextByKey(1, "MSG_ENT_VAU_VALID_DEVICE"), this.ParentForm.Text);
                txt_DeviceName.Focus();
                return false;
            }

            if (txt_SerialNo.Text.Trim() == string.Empty)
            {
                Win32Extensions.ShowWarningMessageBox(this, this.GetResourceTextByKey(1, "MSG_ENT_VAU_VALID_SERIAL"), this.ParentForm.Text);
                txt_SerialNo.Focus();
                return false;
            }
            if (txt_AlertLevel.CTL_IntValue < 1 || txt_AlertLevel.CTL_IntValue > 100)
            {
                Win32Extensions.ShowWarningMessageBox(this, this.GetResourceTextByKey(1, "MSG_ENT_VAU_ALERT"), this.ParentForm.Text);
                txt_AlertLevel.Focus();
                return false;
            }

            if (txt_Type.Text.Trim() == string.Empty)
            {
                Win32Extensions.ShowWarningMessageBox(this, this.GetResourceTextByKey(1, "MSG_ENT_VAU_TYPE"), this.ParentForm.Text);
                txt_Type.Focus();
                return false;
            }
            if (cmb_Manufacturer.SelectedIndex < 0)
            {
                Win32Extensions.ShowWarningMessageBox(this, this.GetResourceTextByKey(1, "MSG_ENT_VAU_MANU"), this.ParentForm.Text);
                cmb_Manufacturer.Focus();
                return false;
            }

            if (!chk_WebserviceEnabled.Checked)
            {
                if (txt_Capacity.CTL_DecimalValue <= 0)
                {
                    Win32Extensions.ShowWarningMessageBox(this, this.GetResourceTextByKey(1, "MSG_ENT_VAU_CAPACITY"), this.ParentForm.Text);
                    txt_Capacity.Focus();
                    return false;
                }
                if (Convert.ToInt32(txt_NoofCassettes.CTL_IntValue) <= 0 || (Convert.ToInt32(txt_NoofCassettes.CTL_IntValue) > NoofCaassette))
                {
                    Win32Extensions.ShowWarningMessageBox(this, string.Format(this.GetResourceTextByKey(1, "MSG_NO_CASSETTES_BETWEEN"), NoofCaassette), this.ParentForm.Text);
                    txt_NoofCassettes.Focus();
                    return false;
                }
                if (Convert.ToInt32(txt_Coinhoppers.CTL_IntValue) <= 0 || (Convert.ToInt32(txt_Coinhoppers.CTL_IntValue) > NoOfHoppers))
                {
                    Win32Extensions.ShowWarningMessageBox(this, string.Format(this.GetResourceTextByKey(1, "MSG_NO_HOPPERS_BETWEEN"), NoOfHoppers), this.ParentForm.Text);
                    txt_Coinhoppers.Focus();
                    return false;
                }
                if (txt_StandardFillAmount.CTL_DecimalValue <= 0)
                {
                    Win32Extensions.ShowWarningMessageBox(this, this.GetResourceTextByKey(1, "MSG_ENT_VAU_AMOUNT"), this.ParentForm.Text);
                    txt_StandardFillAmount.Focus();
                    return false;
                }
                if (txt_StandardFillAmount.CTL_DecimalValue > txt_Capacity.CTL_DecimalValue)
                {
                    Win32Extensions.ShowWarningMessageBox(this, this.GetResourceTextByKey(1, "MSG_ENT_VAU_FILL"), this.ParentForm.Text);
                    txt_StandardFillAmount.Focus();
                    return false;
                }
            }

            if (txtDescription.Text.Trim() == string.Empty)
            {
                Win32Extensions.ShowWarningMessageBox(this, this.GetResourceTextByKey(1, "MSG_ENT_VAU_DESC"), this.ParentForm.Text);
                txtDescription.Focus();
                return false;
            }

            return true;
        }

        private void FilterVault()
        {
            try
            {
                lvVaultdetails.Items.Clear();

                if (!String.IsNullOrEmpty(txt_SearchVault.Text) && (txt_SearchVault.Text !=this.GetResourceTextByKey("Key_Search") && txt_SearchVault.ForeColor != SystemColors.GrayText))
                {
                    if (_lst_SortedVaults != null)
                    {
                        List<Vault_GetAllDevices> lst_dev = _lst_SortedVaults.FindAll(obj =>

                                ((obj.Vault.IndexOf(txt_SearchVault.Text, StringComparison.CurrentCultureIgnoreCase) != -1)
                                || (obj.Serial_No.IndexOf(txt_SearchVault.Text, StringComparison.CurrentCultureIgnoreCase) != -1)
                                || (obj.Site_Code.IndexOf(txt_SearchVault.Text, StringComparison.CurrentCultureIgnoreCase) != -1)
                                || (obj.Site_Name.IndexOf(txt_SearchVault.Text, StringComparison.CurrentCultureIgnoreCase) != -1)));
                        foreach (Vault_GetAllDevices items in lst_dev)
                        {
                            ListViewItem lvItem = new ListViewItem(items.Vault.ToString());
                            lvItem.Tag = items;
                            lvItem.SubItems.Add(new ListViewItem.ListViewSubItem(lvItem, items.Serial_No.ToString()));
                            lvItem.SubItems.Add(new ListViewItem.ListViewSubItem(lvItem, items.Site_Code.ToString()));
                            lvItem.SubItems.Add(new ListViewItem.ListViewSubItem(lvItem, items.Site_Name.ToString()));
                            if (items.Status.ToUpper() == "ACTIVE")
                            {
                                lvItem.SubItems.Add(new ListViewItem.ListViewSubItem(lvItem, "Active"));
                            }
                            else if (items.Status.ToUpper() == "ASSIGNED")
                            {
                                lvItem.SubItems.Add(new ListViewItem.ListViewSubItem(lvItem, "Assigned"));
                            }
                            else
                            {
                                lvItem.SubItems.Add(new ListViewItem.ListViewSubItem(lvItem, "Unassigned"));
                            }
                            lvVaultdetails.Items.Add(lvItem);
                            if (items.Active)
                            {
                                lvItem.ForeColor = Color.Red;
                            }
                        }
                    }
                }
                else
                {
                    if (_lst_SortedVaults != null)
                    {
                        foreach (Vault_GetAllDevices items in _lst_SortedVaults)
                        {
                            ListViewItem lvItem = new ListViewItem(items.Vault.ToString());
                            lvItem.Tag = items;
                            lvItem.SubItems.Add(new ListViewItem.ListViewSubItem(lvItem, items.Serial_No.ToString()));
                            lvItem.SubItems.Add(new ListViewItem.ListViewSubItem(lvItem, items.Site_Code.ToString()));
                            lvItem.SubItems.Add(new ListViewItem.ListViewSubItem(lvItem, items.Site_Name.ToString()));
                            if (items.Status.ToUpper() == "ACTIVE")
                            {
                                lvItem.SubItems.Add(new ListViewItem.ListViewSubItem(lvItem, "Active"));
                            }
                            else if (items.Status.ToUpper() == "ASSIGNED")
                            {
                                lvItem.SubItems.Add(new ListViewItem.ListViewSubItem(lvItem, "Assigned"));
                            }
                            else
                            {
                                lvItem.SubItems.Add(new ListViewItem.ListViewSubItem(lvItem, "Unassigned"));
                            }
                            lvVaultdetails.Items.Add(lvItem);
                            if (items.Active)
                            {
                                lvItem.ForeColor = Color.Red;
                            }
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
                Win32Extensions.ShowErrorMessageBox(this, this.GetResourceTextByKey(1, "MSG_ENT_VAU_ERROR"), this.ParentForm.Text);
            }
        }

        private void FilterWithOrdering()
        {
            if (cbo_Status.Text.ToUpper() == this.GetResourceTextByKey("Key_Active").ToUpper())
            {
                _lst_SortedVaults = _Vault.FindAll(obj =>
                      (obj.Status == "Active")
                   );
            }
            else if (cbo_Status.Text.ToUpper() == this.GetResourceTextByKey("Key_Assigned").ToUpper())
            {
                _lst_SortedVaults = _Vault.FindAll(obj =>
                      (obj.Status == "Assigned")
                   );
            }
            else if (cbo_Status.Text.ToUpper() == this.GetResourceTextByKey("Key_Unassigned").ToUpper())
            {
                _lst_SortedVaults = _Vault.FindAll(obj =>
                         (obj.Status == "Unassigned")
                        );
            }
            else
            {
                _lst_SortedVaults = _Vault;
            }
            FilterVault();
        }

        private void CustomiseLabelsForWebService(bool IsWebserviceEnabled)
        {
            if (IsWebserviceEnabled)
            {
                lbl_Capacity.Text = this.GetResourceTextByKey("Key_CapacityColon").Replace(":","")+ "(" + _strCurrency + ") :";
                lbl_NoofCassettes.Text = this.GetResourceTextByKey("Key_NoofCassettesColon");//"No of Cassettes :";
                lbl_CoinHoppers.Text = this.GetResourceTextByKey("Key_NoofCoinHoppersColon");//"No of Coin Hoppers :";
                lbl_StandardFillAmount.Text = this.GetResourceTextByKey("Key_StandardFillAmount")+ "(" + _strCurrency + ") :";
            }
            else
            {
                lbl_Capacity.Text = "*"+" "+this.GetResourceTextByKey("Key_CapacityColon").Replace(":","")+ "(" + _strCurrency + ") :";//"* Capacity (" + _strCurrency + ") :";
                lbl_NoofCassettes.Text ="*"+" "+ this.GetResourceTextByKey("Key_NoofCassettesColon");//"* No of Cassettes :";
                lbl_CoinHoppers.Text = "*"+" "+this.GetResourceTextByKey("Key_NoofCoinHoppersColon");//"* No of Coin Hoppers :";
                lbl_StandardFillAmount.Text = "*"+" "+this.GetResourceTextByKey("Key_StandardFillAmount") + "(" + _strCurrency + ") :";//"* Standard Fill Amount  (" + _strCurrency + ") :";
            }
        }

        #endregion

        #region Managing Controls

        //Enables and Disables Controls based on manual operations
        private void EnableDisableAllControls(bool IsEnabled)
        {
            try
            {
                txt_DeviceName.Enabled = IsEnabled && _bIsEditPermissionEnabled;
                txt_SerialNo.Enabled = IsEnabled && _bIsEditPermissionEnabled;
                txt_AlertLevel.Enabled = IsEnabled && _bIsEditPermissionEnabled;
                txt_Type.Enabled = IsEnabled && _bIsEditPermissionEnabled;
                txtDescription.Enabled = IsEnabled && _bIsEditPermissionEnabled;
                cmb_Manufacturer.Enabled = IsEnabled && _bIsEditPermissionEnabled;
                chk_WebserviceEnabled.Enabled = IsEnabled && _bIsEditPermissionEnabled;
                txtDescription.Enabled = IsEnabled && _bIsEditPermissionEnabled;
                chkAutoAdjust.Enabled = IsEnabled && _bIsEditPermissionEnabled;
                chkRejectionFill.Enabled = IsEnabled && _bIsEditPermissionEnabled;
                if (_SelectedVault.IsWebServiceEnabled)
                {
                    txt_Capacity.Enabled = false && _bIsEditPermissionEnabled;
                    txt_Coinhoppers.Enabled = false && _bIsEditPermissionEnabled;
                    txt_NoofCassettes.Enabled = false && _bIsEditPermissionEnabled;
                    txt_StandardFillAmount.Enabled = false && _bIsEditPermissionEnabled;
                }
                else
                {
                    txt_Capacity.Enabled = IsEnabled && _bIsEditPermissionEnabled;
                    txt_Coinhoppers.Enabled = IsEnabled && _bIsEditPermissionEnabled;
                    txt_NoofCassettes.Enabled = IsEnabled && _bIsEditPermissionEnabled;
                    txt_StandardFillAmount.Enabled = IsEnabled && _bIsEditPermissionEnabled;
                }

                btnCassettes.Enabled = _SelectedVault.IsWebServiceEnabled;
                btnHoppers.Enabled = _SelectedVault.IsWebServiceEnabled;
                btnRejection.Enabled = _SelectedVault.IsWebServiceEnabled;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        //Enables and Disables Controls based on Edit Permission
        private void EditPermissionForControls()
        {
            try
            {
                txt_DeviceName.Enabled = _bIsEditPermissionEnabled;
                txt_SerialNo.Enabled = _bIsEditPermissionEnabled;
                txt_AlertLevel.Enabled = _bIsEditPermissionEnabled;
                txt_Type.Enabled = _bIsEditPermissionEnabled;
                txt_Capacity.Enabled = _bIsEditPermissionEnabled;
                txtDescription.Enabled = _bIsEditPermissionEnabled;
                txt_StandardFillAmount.Enabled = _bIsEditPermissionEnabled;
                chk_WebserviceEnabled.Enabled = _bIsEditPermissionEnabled;
                cmb_Manufacturer.Enabled = _bIsEditPermissionEnabled;
                btnNew.Enabled = _bIsEditPermissionEnabled;
                btn_Save.Enabled = _bIsEditPermissionEnabled;
                btn_Terminate.Enabled = _bIsTerminateMachineEnabled;
                chkAutoAdjust.Enabled = _bIsTerminateMachineEnabled;
                chkRejectionFill.Enabled = _bIsTerminateMachineEnabled;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
        #endregion

        #region Unused Methods

        private void frmAddVault_Leave(object sender, EventArgs e)
        {
            try
            {
                CompareOldAndNewData();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private bool IsDefaultDataModified(VaultDetails OldData, VaultDetails NewData)
        {
            try
            {
                _isDefaultDataModified = false;
                if (OldData.ToString() != NewData.ToString())
                {
                    _isDefaultDataModified = true;
                }
                return _isDefaultDataModified;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return _isDefaultDataModified;
            }
        }

        private void CompareOldAndNewData()
        {
            VaultDetails NewData = (VaultDetails)_SelectedVault.Clone();

            if (txt_DeviceName.Text != "")
            {
                NewData.NAME = txt_DeviceName.Text;
                NewData.Serial_NO = txt_SerialNo.Text;
                NewData.Alert_Level = Convert.ToInt32(txt_AlertLevel.Text);
                NewData.Type_Prefix = txt_Type.Text;
                NewData.Manufacturer_ID = Convert.ToInt32(cmb_Manufacturer.SelectedValue);
                NewData.IsWebServiceEnabled = chk_WebserviceEnabled.Checked;
                NewData.Capacity = Decimal.Parse(txt_Capacity.Text);
                NewData.NoofCassettes = Convert.ToInt32(txt_NoofCassettes.Text);
                NewData.NoofCoinHopper = Convert.ToInt32(txt_Coinhoppers.Text);
                NewData.AutoAdjustEnabled = chkAutoAdjust.Checked;
                NewData.FillRejection = chkRejectionFill.Checked;
            }

            if (IsDefaultDataModified(_SelectedVault, NewData))
            {
                if (Win32Extensions.ShowQuestionMessageBox(this, this.GetResourceTextByKey(1, "MSG_CONFIRM_CLOSE"), this.ParentForm.Text) == System.Windows.Forms.DialogResult.Yes)
                {
                    UpdateVaultDetails();
                }
                else
                {
                    txt_DeviceName.Text = _SelectedVault.NAME;
                    txt_SerialNo.Text = _SelectedVault.Serial_NO;
                    txt_AlertLevel.Text = _SelectedVault.Alert_Level.ToString();
                    txt_Capacity.Text = _SelectedVault.Capacity.ToString();
                    txt_Type.Text = _SelectedVault.Type_Prefix;
                    txt_NoofCassettes.Text = _SelectedVault.NoofCassettes.ToString();
                    txt_Coinhoppers.Text = _SelectedVault.NoofCoinHopper.ToString();
                    chk_WebserviceEnabled.Checked = _SelectedVault.IsWebServiceEnabled;
                    Chk_Active.Checked = _SelectedVault.Active;
                    cmb_Manufacturer.SelectedValue = _SelectedVault.Manufacturer_ID;
                    txtDescription.Text = _SelectedVault.Description;
                    chkAutoAdjust.Checked = _SelectedVault.AutoAdjustEnabled;
                    chkRejectionFill.Checked = _SelectedVault.FillRejection;
                }
            }
            else
            {
                LoadSelectedVaultDetails();
            }
        }

        private void mnuSubitemCopy_Click(object sender, EventArgs e)
        {
            if (lvVaultdetails.Items.Count > 0)
            {
                if (_SelectedVault.Vault_ID > 0)
                {
                    frmVaultCopy frm = new frmVaultCopy(_SelectedVault.Vault_ID, _SelectedVault.NAME);
                    frm.ShowDialog();
                    LoadVaultDetails();
                    lvVaultdetails.Focus();
                    LoadSelectedVaultDetails();
                }
            }
            else
            {
                Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_ENT_VAU_CAOPY"), this.ParentForm.Text);
            }


        }

        #endregion

    }
}
