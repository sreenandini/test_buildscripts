using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BMC.Common.ExceptionManagement;
using BMC.EnterpriseBusiness.Entities;
using BMC.EnterpriseBusiness.Business;
using Audit.Transport;
using System.Xml.Linq;
using BMC.CoreLib.Win32;
using System.Globalization;
using BMC.Common.LogManagement;
using BMC.Common;

namespace BMC.EnterpriseClient.Views
{


    public partial class frmVaultAdmin : Form
    {
        #region Private Variables
        VaultAdmin VaultBiz = new VaultAdmin();
        public VaultDetails _SelectedVault = new VaultDetails();
        public List<VaultDetails> FinancialDetails = null;
        public bool _bVaultAdminEditPermission = false;

        AssignToSiteData objAssignToSiteData = null;
        TabPage _tabFinanceTab = null;
        TabPage _tabSiteTab = null;
        bool _ClosingFromForm = false; //To handle showing extra popup on application close. 
        string _strCurrency = "$";
         BMC.EnterpriseClient.Helpers.Datawatcher objDatawatcher = null;


        #endregion

        #region Constructor

        public frmVaultAdmin()
        {
            InitializeComponent();
            SetTagProperty();
            this.ucfrmAddVault.loadFinanceDetails = new delLoadFinanceDetails(LoadFinanceDetails);
            this.ucfrmAddVault.OnNewVault = new AddRemoveControl(OnNewVaultCreation);
            this.ucfrmAddVault.OnCancelNewVault = new AddRemoveControl(OnCancelVaultCreation);
           objDatawatcher = new Helpers.Datawatcher(this);
        
        }

        #endregion

        /// <summary>
        /// Assigns the Resource Key names to the controls--Created by kishore sivagnanam
        /// </summary>
        public void SetTagProperty()
        {
            this.btnAssignVaultClose.Tag = "Key_CloseCaption";
            this.btnClose.Tag = "Key_CloseCaption";
            this.btnAssignVaultSave.Tag = "Key_SaveCaption";
            this.btnSave.Tag = "Key_SaveCaption";
            this.lblPurchaseInvoiceNumber.Tag = "Key_PurchaseInvoiceNumberMandatory";
            //this.lblPurchasePrice.Tag = "Key_PurchasePriceMandatory";
            this.btnUnassign.Tag = "Key_LessThan";
            this.btnAssign.Tag = "Key_GreaterThan";
            this.tpAssignVaultToSite.Tag = "Key_AssignVaulttoSite";
            this.lbl_AssignToSite.Tag = "Key_AssignVaulttoSite";
            this.lblAssignVault.Tag = "Key_AssigningVaulttoSite";
            this.lblDepreciationStartDate.Tag = "Key_DepreciationStartDate";
            this.lbl_FinanceHeader.Tag = "Key_Finance";
            this.lbl_Prefix.Tag = "Key_PrefixColon";
            this.lblPurchaseData.Tag = "Key_PurchaseDateColon";
            this.txtVaultFilter.Tag = "Key_SearchText";
            this.txtVaultFilter.ForeColor = SystemColors.GrayText;            
            this.txtSitesFilter.Tag = "Key_SearchText";
            this.txtSitesFilter.ForeColor = SystemColors.GrayText;
            this.lbl_SellVault.Tag = "Key_SellVaultColon";
            this.lbl_SerialNo.Tag = "Key_SerialNumberColon";
            this.clmSites.Tag = "Key_Sites";
            this.lblSites.Tag = "Key_SitesColon";
            this.lblSoldDate.Tag = "Key_SoldDateColon";
            this.lblSoldInvoiceNumber.Tag = "Key_SoldInvoiceNumber";
           // this.lblSoldPrice.Tag = "Key_SoldPriceColon";
            this.tpVault.Tag = "Key_Vault";
            this.Tag = "Key_VaultAdmin";
            this.tpVaultFinance.Tag = "Key_VaultFinance";
            this.lbl_VaultName.Tag = "Key_VaultNameColon";
            this.clmVaults.Tag = "Key_Vaults";
            this.lblVaults.Tag = "Key_VaultsColon";
            this.btn_CloseAddVault.Tag = "Key_CloseCaption";
        }

        #region Event Methods

        #region Vault and Cassette Details

        private void frmVaultAdmin_Load(object sender, EventArgs e)
        {
            try
            {
                this.ResolveResources();
                _bVaultAdminEditPermission = AppGlobals.Current.HasUserAccess("HQ_Admin_EditCreateVault");
                _strCurrency = new RegionInfo(BMC.Common.ConfigurationManagement.ConfigManager.Read("GetDefaultCultureForCurrency")).CurrencySymbol;
                lblPurchasePrice.Text = string.Format(this.GetResourceTextByKey("Key_PurchasePrice"), _strCurrency);
                lblSoldPrice.Text = string.Format(this.GetResourceTextByKey("Key_SoldPrice"), _strCurrency);
                _tabFinanceTab = tcVaultAdmin.TabPages[1];
                _tabSiteTab = tcVaultAdmin.TabPages[2];

                ucfrmAddVault.ParentTab = this.tcVaultAdmin;


                lvAssignSite.Items.Clear();

                //Enable and Disable controls based on Edit Permission
                EnableDisableControls();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void EnableDisableControls()
        {
            btnAssignVaultSave.Enabled = _bVaultAdminEditPermission;
            btnSave.Enabled = _bVaultAdminEditPermission;
            btnAssign.Enabled = _bVaultAdminEditPermission;
            btnUnassign.Enabled = _bVaultAdminEditPermission;
            tlpFinanceInnerPanel.Enabled = _bVaultAdminEditPermission;

        }

        private void OnNewVaultCreation()
        {
            if (tcVaultAdmin.TabPages.Contains(_tabFinanceTab))
                tcVaultAdmin.TabPages.Remove(_tabFinanceTab);
            if (tcVaultAdmin.TabPages.Contains(_tabSiteTab))
                tcVaultAdmin.TabPages.Remove(_tabSiteTab);
            btn_CloseAddVault.Enabled = false;
        }
        private void OnCancelVaultCreation()
        {
            if (!tcVaultAdmin.TabPages.Contains(_tabFinanceTab))
                tcVaultAdmin.TabPages.Add(_tabFinanceTab);
            if (!tcVaultAdmin.TabPages.Contains(_tabSiteTab))
                tcVaultAdmin.TabPages.Add(_tabSiteTab);
            btn_CloseAddVault.Enabled = true;
        }

        #endregion

        #region Vault Finance Details

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateControls())
                    return;

                if (UpdateVaultFinanceDetails() == _SelectedVault.Vault_ID)
                {
                    Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_DETAILS_UPDATE_SUCCESS"), this.Text);
                }
                else
                {
                    Win32Extensions.ShowWarningMessageBox(this, this.GetResourceTextByKey(1, "MSG_UPDATE_FAILED"), this.Text);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private bool ValidateControls()
        {
            try
            {
                if (txtPurchasePrice.CTL_IntValue <= 0)
                {
                    Win32Extensions.ShowWarningMessageBox(this, this.GetResourceTextByKey(1, "MSG_VALID_PURCHASE_PRICE"), this.Text);
                    txtPurchasePrice.Focus();
                    return false;
                }
                if (txtInvoiceNumber.Text.Trim() == "")
                {
                    Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_VALID_PURCHASE_INVOICE"), this.Text);
                    txtInvoiceNumber.Focus();
                    return false;
                }
                if (dtpPurchaseDate.Value > System.DateTime.Now)
                {
                    Win32Extensions.ShowErrorMessageBox(this, this.GetResourceTextByKey(1, "MSG_PURCHASE_DT_GREATER_SYS_DT"), this.Text);
                    dtpPurchaseDate.Focus();
                    return false;
                }
                if (dtpDepreciationStartDate.Value < dtpPurchaseDate.Value)
                {
                    Win32Extensions.ShowErrorMessageBox(this, this.GetResourceTextByKey(1, "MSG_DEPRECIATION_START_DT_LESS_THAN_PURCHASE_DT"), this.Text);
                    dtpDepreciationStartDate.Focus();
                    return false;
                }

                if (cb_SellVault.Checked)
                {
                    if (txtSoldPrice.CTL_IntValue <= 0)
                    {
                        Win32Extensions.ShowWarningMessageBox(this, this.GetResourceTextByKey(1, "MSG_VALID_SOLD_PRICE"), this.Text);
                        txtSoldPrice.Focus();
                        return false;
                    }
                    if (txtSoldInvoiceNumber.Text.Trim() == "")
                    {
                        Win32Extensions.ShowWarningMessageBox(this, this.GetResourceTextByKey(1, "MSG_VALID_SOLD_INVOICE_NUMBER"), this.Text);
                        txtSoldInvoiceNumber.Focus();
                        return false;
                    }
                    if (dtpSoldDate.Value < dtpPurchaseDate.Value)
                    {
                        Win32Extensions.ShowErrorMessageBox(this, this.GetResourceTextByKey(1, "MSG_SOLD_DT_GREATER_THAN_PURCHASE_DT"), this.Text);
                        dtpSoldDate.Focus();
                        return false;
                    }

                    if (dtpSoldDate.Value > System.DateTime.Now)
                    {
                        Win32Extensions.ShowErrorMessageBox(this, this.GetResourceTextByKey(1, "MSG_SOLD_DT_GREATER_THAN_SYS_DT"), this.Text);
                        dtpSoldDate.Focus();
                        return false;
                    }

                    if (Win32Extensions.ShowQuestionMessageBox(this, this.GetResourceTextByKey(1, "MSG_VAULT_SOLD_WILL_BE_REMOVED"), this.Text) == System.Windows.Forms.DialogResult.No)
                    {
                        return false;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return false;
            }
        }

        #endregion

        #region Vault Assign Sites Details

        private void txtVaultFilter_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtVaultFilter.Focused || txtVaultFilter.Text != this.GetResourceTextByKey("Key_SearchText"))
                {
                    txtVaultFilter.ForeColor = Color.Black;
                    FilterVault();
                }
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);

            }
        }

        private void txtSitesFilter_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtSitesFilter.Focused || txtSitesFilter.Text != this.GetResourceTextByKey("Key_SearchText"))
                {
                    txtSitesFilter.ForeColor = Color.Black;
                    FilterSites();

                }
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
        }

        private void txtSitesFilter_Enter(object sender, EventArgs e)
        {
            try
            {
                if (txtSitesFilter.Text == this.GetResourceTextByKey("Key_SearchText") && txtSitesFilter.ForeColor == SystemColors.GrayText)
                {
                    txtSitesFilter.Text = string.Empty;
                }
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
        }

        private void txtVaultFilter_Enter(object sender, EventArgs e)
        {
            try
            {
                if (txtVaultFilter.Text == this.GetResourceTextByKey("Key_SearchText") && txtVaultFilter.ForeColor == SystemColors.GrayText)
                {
                    txtVaultFilter.Text = string.Empty;
                }
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);

            }
        }

        private void txtVaultFilter_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtVaultFilter.Text.Trim() == string.Empty)
                {
                    txtVaultFilter.ForeColor = SystemColors.GrayText;
                    txtVaultFilter.Text = this.GetResourceTextByKey("Key_SearchText");
                    txtVaultFilter.ForeColor = SystemColors.GrayText;

                }
                FilterVault();
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
        }

        private void txtSitesFilter_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtSitesFilter.Text.Trim() == string.Empty)
                {
                    txtSitesFilter.ForeColor = SystemColors.GrayText;
                    txtSitesFilter.Text = this.GetResourceTextByKey("Key_SearchText");
                    txtSitesFilter.ForeColor = SystemColors.GrayText;

                }
                FilterSites();
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);

            }
        }

        private void btnAssign_Click(object sender, EventArgs e)
        {

            try
            {
                if (lbSites.SelectedItem != null && lbVaults.SelectedItem != null)
                {
                    ((UnassignedSite)lbSites.SelectedItem).IsMapped = true;
                    ((UnassignedDevice)lbVaults.SelectedItem).IsMapped = true;
                    ((UnassignedSite)lbSites.SelectedItem).Mapped_Vault = ((UnassignedDevice)lbVaults.SelectedItem);
                    ListViewItem lvItem = new ListViewItem();
                    lvItem.Text = ((UnassignedDevice)lbVaults.SelectedItem).Name;
                    lvItem.Tag = ((UnassignedSite)lbSites.SelectedItem);
                    lvItem.SubItems.Add(new ListViewItem.ListViewSubItem(lvItem, ((UnassignedSite)lbSites.SelectedItem).Site_Code));
                    lvAssignSite.Items.Add(lvItem);
                    FilterSites();
                    FilterVault();
                }
                else
                {
                    Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_SELECT_DEVICE_SITE"), this.Text);
                }

            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
        }

        private void btnUnassign_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (ListViewItem item in lvAssignSite.SelectedItems)
                {
                    UnassignedSite tempUnassignedSites = (UnassignedSite)item.Tag;
                    tempUnassignedSites.IsMapped = false;
                    tempUnassignedSites.Mapped_Vault.IsMapped = false;
                    lvAssignSite.Items.Remove(item);
                }
                FilterSites();
                FilterVault();
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
        }

        private void btnAssignVaultSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (lvAssignSite.Items.Count <= 0)
                {
                    Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_NO_VAULT"), this.Text);
                    return;
                }
                if (Win32Extensions.ShowQuestionMessageBox(this, this.GetResourceTextByKey(1, "MSG_VAULT_ASSIGNED_CANNOT_MODIFIED"), this.Text) == System.Windows.Forms.DialogResult.Yes)
                {
                    XElement xElement = new XElement("Vaults");
                    foreach (ListViewItem item in lvAssignSite.Items)
                    {
                        UnassignedSite tempUnassignedSite = (UnassignedSite)item.Tag;
                        XElement xmlVault = new XElement("Vault", new XAttribute("NGADevice_ID", tempUnassignedSite.Mapped_Vault.NGADevice_ID), new XAttribute("Site_ID", tempUnassignedSite.Site_ID));
                        xElement.Add(xmlVault);
                    }
                    VaultBiz.AssignToSite(xElement, AppGlobals.Current.UserId, (int)ModuleIDEnterprise.VaultManager, ModuleIDEnterprise.VaultManager.ToString(), "Assign Vault To Site");
                    Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_VAULT_ASSIGN_SUCCESS"), this.Text);
                    BindAllUnassigned();
                    LogManager.WriteLog("btnAssignVaultSave->Refreshing Vault details" , LogManager.enumLogLevel.Info );
                    ucfrmAddVault.LoadVaultDetails();
                }
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
        }

        #endregion

        private void tcVaultAdmin_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tcVaultAdmin.SelectedTab.Tag.ToString() == "Vault")
            {
                //load/Refresh vault related data
                ucfrmAddVault.LoadVaultDetails();
            }
            else if (tcVaultAdmin.SelectedTab.Tag.ToString() == "Vault Finance")
            {
                //load Vault Finance Related Data
            }
            else
            {
                this.BindAllUnassigned();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            try
            {
                _ClosingFromForm = true;
                this.Close();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void tcVaultAdmin_Click(object sender, EventArgs e)
        {
            try
            {
                if (((TabControl)sender).SelectedIndex == 2)
                {
                    this.BindAllUnassigned();
                }
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
        }

        #endregion

        #region Load Methods

        private void LoadFinanceDetails(int VaultID, VaultDetails SelectedVault)
        {
            try
            {
                if (VaultID > 0)
                {
                    _bVaultAdminEditPermission = AppGlobals.Current.HasUserAccess("HQ_Admin_EditCreateVault");
                    tlpVaultFinance.Enabled = true;
                    lbl_VaultText.Text = SelectedVault.NAME;
                    lbl_SerialNoTxt.Text = SelectedVault.Serial_NO;
                    lbl_PrefixText.Text = SelectedVault.Type_Prefix;
                    cb_SellVault.Checked = false;
                    _SelectedVault = SelectedVault;
                    txtPurchasePrice.Text = SelectedVault.PurchasePrice.ToString();
                    txtInvoiceNumber.Text = SelectedVault.PurchaseInvoice.ToString();
                    dtpPurchaseDate.Value = SelectedVault.PurchaseDate.Value;
                    dtpDepreciationStartDate.Value = SelectedVault.depreciationDate.Value;
                    txtSoldPrice.Text = SelectedVault.SoldPrice.ToString();
                    txtSoldInvoiceNumber.Text = SelectedVault.SoldInvoice.ToString();
                    dtpSoldDate.Value = SelectedVault.SoldDate.Value;
                    if (SelectedVault.IsAssigned)
                    {
                        cb_SellVault.Enabled = false;
                    }
                    else
                    {
                        cb_SellVault.Enabled = true && _bVaultAdminEditPermission;
                    }
                }
                else
                {
                    tlpVaultFinance.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        #endregion

        #region Update Methods

        private int UpdateVaultFinanceDetails()
        {
            try
            {
                int Result = 0;
                DateTime? dtSoldDate = null;
                decimal? SoldPrice = null;
                string SoldInvoiceNo = "";

                if (cb_SellVault.Checked)
                {
                    dtSoldDate = dtpSoldDate.Value;
                    SoldPrice = decimal.Parse(txtSoldPrice.Text);
                    SoldInvoiceNo = txtSoldInvoiceNumber.Text;
                }

                if (cb_SellVault.Checked)
                {
                    Result = VaultBiz.Vault_UpdateFinanceDetails(_SelectedVault.Vault_ID,
                                                                             decimal.Parse(txtPurchasePrice.Text),
                                                                             txtInvoiceNumber.Text,
                                                                             dtpPurchaseDate.Value,
                                                                             dtpDepreciationStartDate.Value,
                                                                             SoldPrice,
                                                                             SoldInvoiceNo,
                                                                             dtSoldDate,
                                                                             AppGlobals.Current.UserId,
                                                                             (int)ModuleIDEnterprise.VaultManager,
                                                                             ModuleIDEnterprise.VaultManager.ToString(),
                                                                             "Vault Device Admin");
                }
                else
                {
                    Result = VaultBiz.Vault_UpdateFinanceDetails(_SelectedVault.Vault_ID,
                                                                 decimal.Parse(txtPurchasePrice.Text),
                                                                 txtInvoiceNumber.Text,
                                                                 dtpPurchaseDate.Value,
                                                                 dtpDepreciationStartDate.Value,
                                                                 0,
                                                                 "",
                                                                 null,
                                                                 AppGlobals.Current.UserId,
                                                                 (int)ModuleIDEnterprise.VaultManager,
                                                                 ModuleIDEnterprise.VaultManager.ToString(),
                                                                 "Vault Device Admin");
                }
                return Result;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return -1;
            }
        }

        #endregion

        #region Miscellaneous Methods

        private void BindAllUnassigned()
        {

            objAssignToSiteData = VaultBiz.GetDataForAssigningToSite(AppGlobals.Current.UserId);
            lvAssignSite.Items.Clear();
            FilterVault();
            FilterSites();
        }

        private void FilterSites()
        {
            try
            {
                lbSites.Items.Clear();
                lbSites.DisplayMember = "Site_Code";
                lbSites.ValueMember = "Site_ID";

                if (txtSitesFilter.Text.Trim() != string.Empty && txtSitesFilter.ForeColor == Color.Black)
                {
                    List<UnassignedSite> tempList = objAssignToSiteData.Sites.FindAll((x) => x.Site_Code.ToUpper().IndexOf(txtSitesFilter.Text.Trim().ToUpper()) > -1 && x.IsMapped == false);
                    foreach (UnassignedSite site in tempList)
                    {
                        lbSites.Items.Add(site);
                    }
                }
                else
                {
                    foreach (UnassignedSite site in objAssignToSiteData.Sites.FindAll((x) => x.IsMapped == false))
                    {
                        lbSites.Items.Add(site);
                    }

                }


            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }

        }

        private void FilterVault()
        {
            try
            {
                lbVaults.Items.Clear();
                lbVaults.DisplayMember = "Name";
                lbVaults.ValueMember = "Vault_ID";

                if (txtVaultFilter.Text.Trim() != string.Empty && txtVaultFilter.ForeColor == Color.Black)
                {
                    List<UnassignedDevice> tempList = objAssignToSiteData.Devices.FindAll((x) => x.Name.ToUpper().IndexOf(txtVaultFilter.Text.Trim().ToUpper()) > -1 && x.IsMapped == false);
                    foreach (UnassignedDevice device in tempList)
                    {
                        lbVaults.Items.Add(device);
                    }
                }
                else
                {
                    foreach (UnassignedDevice device in objAssignToSiteData.Devices.FindAll((x) => x.IsMapped == false))
                    {
                        lbVaults.Items.Add(device);
                    }

                }
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }

        }

        #endregion

        private void cb_SellVault_CheckedChanged(object sender, EventArgs e)
        {
            if (cb_SellVault.Checked)
            {
                lblSoldPrice.Text = "* Sold Price " +"("+_strCurrency + ")"+" :";
                lblSoldInvoiceNumber.Text = "* Sold Invoice Number :";
                txtSoldPrice.Enabled = true;
                txtSoldInvoiceNumber.Enabled = true;
                dtpSoldDate.Enabled = true;
            }
            else
            {
                lblSoldPrice.Text = "Sold Price " + "(" + _strCurrency + ")" + " :";
                lblSoldInvoiceNumber.Text = "Sold Invoice Number :";
                txtSoldPrice.Enabled = false;
                txtSoldInvoiceNumber.Enabled = false;
                dtpSoldDate.Enabled = false;
            }
        }

        private void frmVaultAdmin_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        private void btn_CloseAddVault_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}