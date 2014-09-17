using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BMC.Common.Utilities;
using BMC.Common.LogManagement;
using BMC.Common.ExceptionManagement;
using BMC.EnterpriseBusiness.Business;
using BMC.EnterpriseBusiness.Entities;
using System.Globalization;
using BMC.Security;
using System.Configuration;
using System.Xml.Linq;
using BMC.CoreLib.Win32;
using BMC.Common;

namespace BMC.EnterpriseClient.Views
{
    public partial class frmVaultDeclaration : Form
    {
        #region Declared Variables
        ucCassette grpCassette = new ucCassette(CassetteType.Cassette);
        ucCassette grpHopper = new ucCassette(CassetteType.Hopper);
        VaultDeclarationBusiness VaultDeclarationBiz;
        bool bStartClicked;
        bool bUncommitedDataFound = false;
        int _iSiteID;
        List<Vault_UndeclaredDrops> _lstUndeclaredDrops = null;
        private NoteCumTktScanLib.CNoteCumTktScan objNoteCumTktScanLib = null;
        private string _strCurrency = "$";
        private List<decimal> _CashDenom = new List<decimal>();
        private List<decimal> _CoinDenom = new List<decimal>();
        private bool _IsCassettesDefined;
        private bool _bIsEditPermissionEnabled = false;
        bool _IsAmountEditable = false;
        bool _bIsAutoPopulateEnabled = true;
        Vault_UndeclaredDrops objUndeclaredDrop = null;
        string _sTotalBills = string.Empty;
        string _sTotalCoins = string.Empty;
        #endregion

        #region Constructor

        public frmVaultDeclaration(int SiteID, string SiteName)
        {
            try
            {
                _strCurrency = new RegionInfo(BMC.Common.ConfigurationManagement.ConfigManager.Read("GetDefaultCultureForCurrency")).CurrencySymbol;
                LogManager.WriteLog("Inside Constructor Region", LogManager.enumLogLevel.Info);
                InitializeComponent();
                SetTagProperty();
                _iSiteID = SiteID;
                txtSiteName.Text = SiteName;
                this.LoadCurrencyByRegion();
                _sTotalBills = this.GetResourceTextByKey("Key_TotalBillsColon");
                _sTotalCoins = this.GetResourceTextByKey("Key_TotalCoinsColon");

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        #endregion

        #region Event Methods

        private void frmVaultDeclaration_Load(object sender, EventArgs e)
        {
            try
            {
                this.ResolveResources();
                _bIsEditPermissionEnabled = AppGlobals.Current.HasUserAccess("HQ_Admin_EditVaultDeclaration");
                LogManager.WriteLog("Inside frmVaultDeclaration_Load", LogManager.enumLogLevel.Info);
                VaultDeclarationBiz = new VaultDeclarationBusiness();
                _IsAmountEditable = SettingsEntity.IsBillCounterAmountEditable;
                _bIsAutoPopulateEnabled = SettingsEntity.Vault_AutoPopulateDropValues;
                btnSave.Visible = _bIsEditPermissionEnabled;
                LoadVaultDetails();
                objNoteCumTktScanLib = new NoteCumTktScanLib.CNoteCumTktScan();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void Quantity_Changed(object sender, EventArgs e)
        {
            SetDeclaredAmount();
        }

        private void SetDeclaredAmount()
        {
            try
            {
                grpCassette.SumGroupsDeclared();
                grpHopper.SumGroupsDeclared();
                txtDeclaredBalance.Text = (grpCassette.Amount + grpHopper.Amount).ToString();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void lvDropDetails_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                lblStatus.Text = "";
                LogManager.WriteLog("Inside lvDropDetails_SelectedIndexChanged", LogManager.enumLogLevel.Info);
                if (lvDropDetails.SelectedItems.Count > 0)
                {

                    if (btnSave.Enabled && (grpCassette.IsChildQuantityModified || grpHopper.IsChildQuantityModified) && lvDropDetails.Items[lvDropDetails.Items.Count - 1].Selected == false)
                    {
                        if (Win32Extensions.ShowQuestionMessageBox(this, this.GetResourceTextByKey(1, "MSG_UNCOMMITED_DATA_FOUND"), this.Text) == System.Windows.Forms.DialogResult.Yes)
                        {
                            if (!SaveDeclaration())
                            {
                                return;
                            }
                        }
                        else
                        {
                            txtDeclaredBalance.Text = "0.00";
                        }
                    }
                    else
                    {
                        txtDeclaredBalance.Text = "0.00";
                    }

                    bUncommitedDataFound = false;
                    objUndeclaredDrop = (Vault_UndeclaredDrops)lvDropDetails.SelectedItems[0].Tag;
                    List<Vault_CassetteDropDetailsResult> _lstCassetteDrops = VaultDeclarationBiz.GetVaultCassetteDropDetails(Convert.ToInt32(objUndeclaredDrop.Drop_ID));

                    txtFillAmount.Text = objUndeclaredDrop.FillAmount.ToString();
                    txtBleedAmount.Text = objUndeclaredDrop.BleedAmount.ToString();
                    txtAdjustmentAmount.Text = objUndeclaredDrop.AdjustmentAmount.ToString();
                    txtMeterBalance.Text = objUndeclaredDrop.Meter_Balance.ToString();
                    txtVaultBalance.Text = objUndeclaredDrop.Vault_Balance.ToString();
                    txtDeclaredBalance.Text = objUndeclaredDrop.Declared_Balance.ToString();
                    txtVaultName.Text = objUndeclaredDrop.Name;
                    txtManufacturer.Text = objUndeclaredDrop.Manufacturer_Name;
                    txtTyprPrefix.Text = objUndeclaredDrop.Type_Prefix;

                    if (_lstCassetteDrops.Count > 0)
                    {
                        _IsCassettesDefined = true;
                        ConstructDroppedCassettes(_lstCassetteDrops);
                    }
                    else
                    {
                        _IsCassettesDefined = false;
                        ConstructDefaultCassetteDetails();
                    }

                    if (lvDropDetails.SelectedItems[0].Index == lvDropDetails.Items.Count - 1)
                    {
                        btnSave.Enabled = _bIsEditPermissionEnabled;
                        this.SetStatus(string.Empty);
                    }
                    else
                    {
                        btnSave.Enabled = false;
                        this.SetStatus(this.GetResourceTextByKey(1,"MSG_VALUT_DEC_PREV_DROPS"));//"Please declare the previous drops before proceeding");
                    }
                    SetDeclaredAmount();
                    btnStartStop.Focus();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void ConstructDroppedCassettes(List<Vault_CassetteDropDetailsResult> _lstCassetteDrops)
        {
            this.ClearCassetteControls();
            grpCassette.ResetChildren();
            grpHopper.ResetChildren();

            grpCassette.Height = 25;
            grpCassette.OnQuantityChange += new EventHandler(Quantity_Changed);
            grpCassette.Width = flpDeclaration.ClientSize.Width - 30;
            grpCassette.Name = "grpCassette";
            grpCassette.CassetteName = _sTotalBills;// "Total Bills:";


            grpHopper.Height = 25;
            grpHopper.OnQuantityChange += new EventHandler(Quantity_Changed);
            grpHopper.Width = flpDeclaration.ClientSize.Width - 30;
            grpHopper.Name = "grpHopper";
            grpHopper.CassetteName = _sTotalCoins; //"Total Coins:";

            foreach (Vault_CassetteDropDetailsResult Cassette in _lstCassetteDrops.FindAll((x) => x.Type == (int)CassetteType.Cassette))
            {

                ucCassette uc = new ucCassette(CassetteType.Cassette, _IsAmountEditable);
                uc.OnInvalidDenom = OnInvalidDenomMethod;
                //uc.Size=new Size(300,20);
                uc.Height = 25;
                uc.OnQuantityChange += new EventHandler(Quantity_Changed);
                uc.Width = flpDeclaration.ClientSize.Width - 30;
                uc.Name = "txtQuantity_" + Cassette.Cassette_Name;
                uc.Denom = Cassette.Denom;
                uc.Quantity = 0;
                if (_bIsAutoPopulateEnabled)
                {
                    uc.Amount = Convert.ToInt32(Cassette.VaultBalance);
                    uc.Quantity = Convert.ToInt32(Cassette.VaultBalance / Cassette.Denom);
                }
                uc.CassetteName = Cassette.Cassette_Name;
                uc.DropAmount = Cassette.VaultBalance;
                uc.Cassette_ID = Cassette.Cassette_ID;
                uc.MaxFillAmount = Cassette.MaxFillAmount;
                grpCassette.SubscribeAsChild(uc);
                flpDeclaration.Controls.Add(uc);
            }
            foreach (Vault_CassetteDropDetailsResult Cassette in _lstCassetteDrops.FindAll((x) => x.Type == (int)CassetteType.Rejection))
            {

                ucCassette uc = new ucCassette(CassetteType.Cassette, _IsAmountEditable);
                uc.OnInvalidDenom = OnInvalidDenomMethod;
                //uc.Size=new Size(300,20);
                uc.Height = 25;
                uc.OnQuantityChange += new EventHandler(Quantity_Changed);
                uc.Width = flpDeclaration.ClientSize.Width - 30;
                uc.Name = "txtQuantity_" + Cassette.Cassette_Name;
                uc.Denom = Cassette.Denom;
                uc.Quantity = 0;
                if (_bIsAutoPopulateEnabled)
                {
                    uc.Amount = Convert.ToInt32(Cassette.VaultBalance);
                    uc.Quantity = Convert.ToInt32(Cassette.VaultBalance / Cassette.Denom);
                }
                uc.CassetteName = Cassette.Cassette_Name;
                uc.DropAmount = Cassette.VaultBalance;
                uc.Cassette_ID = Cassette.Cassette_ID;
                uc.MaxFillAmount = Cassette.MaxFillAmount;
                grpCassette.SubscribeAsChild(uc);
                flpDeclaration.Controls.Add(uc);
            }

            if (grpCassette.GroupMembers.Count > 0)
            {
                grpCassette.SumGroupsDropped();
                flpDeclaration.Controls.Add(grpCassette);
            }
            foreach (Vault_CassetteDropDetailsResult Cassette in _lstCassetteDrops.FindAll((x) => x.Type == (int)CassetteType.Hopper))
            {
                ucCassette uc = new ucCassette(CassetteType.Hopper, _IsAmountEditable);
				uc.OnInvalidDenom = OnInvalidDenomMethod;
                //uc.Size=new Size(300,20);
                uc.Height = 25;
                uc.OnQuantityChange += new EventHandler(Quantity_Changed);
                uc.Width = flpDeclaration.ClientSize.Width - 30;
                uc.Name = "txtQuantity_" + Cassette.Cassette_Name;
                uc.Denom = Cassette.Denom;
                uc.Quantity = 0;
                if (_bIsAutoPopulateEnabled)
                {
                    uc.Amount = Cassette.VaultBalance;
                    uc.Quantity = Convert.ToInt32(Cassette.VaultBalance / Cassette.Denom);
                }
                uc.CassetteName = Cassette.Cassette_Name;
                uc.DropAmount = Cassette.VaultBalance;
                uc.Cassette_ID = Cassette.Cassette_ID;
                uc.MaxFillAmount = Cassette.MaxFillAmount;
                grpHopper.SubscribeAsChild(uc);
                flpDeclaration.Controls.Add(uc);
            }

            if (grpHopper.GroupMembers.Count > 0)
            {
                grpHopper.SumGroupsDropped();
                flpDeclaration.Controls.Add(grpHopper);
            }

            if (grpCassette.GroupMembers.Count > 0)
            {
                btnStartStop.Enabled = true && _bIsEditPermissionEnabled;
            }
            else
            {
                btnStartStop.Enabled = false;
            }
        }

        private void btnStartStop_Click(object sender, EventArgs e)
        {
            try
            {
                LogManager.WriteLog("Inside btnStartStop_Click", LogManager.enumLogLevel.Info);
                if (btnStartStop.Text == this.GetResourceTextByKey("Key_StartCounterCaption"))
                {
                    grpCassette.DisableChildren();
                    grpHopper.DisableChildren();
                    gbDropDetails.Enabled = false;
                    btnStartStop.Text = this.GetResourceTextByKey("Key_StopCounterCaption");
                    btnSave.Enabled = false;
                    btnClear.Enabled = false;
                }
                else
                {
                    grpCassette.EnabledChildren();
                    grpHopper.EnabledChildren();
                    gbDropDetails.Enabled = true;
                    btnStartStop.Text = this.GetResourceTextByKey("Key_StartCounterCaption");
                    if (lvDropDetails.SelectedItems[0].Index == lvDropDetails.Items.Count - 1)
                    {
                        btnSave.Enabled = _bIsEditPermissionEnabled;
                    }
                    btnClear.Enabled = true;
                }
                if (bStartClicked)
                {
                    string sNotesString = string.Empty;
                    LogManager.WriteLog("CloseSerialCom() CALL", LogManager.enumLogLevel.Info);
                    objNoteCumTktScanLib.CloseSerialCom();
                    LogManager.WriteLog("CloseSerialCom() ACK", LogManager.enumLogLevel.Info);

                    LogManager.WriteLog("GetString() CALL", LogManager.enumLogLevel.Info);
                    objNoteCumTktScanLib.GetString(out sNotesString);
                    LogManager.WriteLog("GetString() ACK", LogManager.enumLogLevel.Info);

                    LogManager.WriteLog(sNotesString, LogManager.enumLogLevel.Info);
                    bStartClicked = false;

                    //sNotesString = "ONES:10,TWOS:15,FIVES:5,TENS:3,TWENTIES:2,FIFTIES:4,HUNDREDS:3";
                    btnStartStop.Text = this.GetResourceTextByKey("Key_StartCounterCaption");


                    if (string.IsNullOrEmpty(sNotesString))
                    {
                        LogManager.WriteLog("No bills/Tickets data is available to process.", LogManager.enumLogLevel.Info);
                        this.SetStatus(this.GetResourceTextByKey(1,"MSG_VALUT_DEC_NOBILLS"));//"No bills data is available to process.");
                    }
                    else
                    {
                        LogManager.WriteLog(sNotesString, LogManager.enumLogLevel.Info);
                        this.FillCountedCash(sNotesString);
                    }


                    //this.SetStatus("Please save before closing or moving to the next position.");
                    this.Cursor = Cursors.Default;

                }
                else
                {
                    LogManager.WriteLog("OpenSerialComPort() CALL", LogManager.enumLogLevel.Info);
                    if (objNoteCumTktScanLib.OpenSerialComPort(ConfigurationManager.AppSettings["BillVoucherCounterCOMPort"]) != 0)
                    {
                        LogManager.WriteLog("OpenSerialComPort() NACK", LogManager.enumLogLevel.Info);
                        LogManager.WriteLog("CloseSerialCom() CALL", LogManager.enumLogLevel.Info);
                        objNoteCumTktScanLib.CloseSerialCom();
                        LogManager.WriteLog("CloseSerialCom() ACK", LogManager.enumLogLevel.Info);
                        this.SetStatus(this.GetResourceTextByKey(1,"MSG_VALUT_DEC_COMPORT"));//"Unable to open COM Port. Please check connectivity");

                        return;
                    }
                    if (ExtensionMethods.CurrentSiteCulture == "en-US")
                    {
                        objNoteCumTktScanLib.SetDenom(1);
                        LogManager.WriteLog("SetDenom(1) en-US", LogManager.enumLogLevel.Info);
                    }
                    else
                    {
                        objNoteCumTktScanLib.SetDenom(2);
                        LogManager.WriteLog("SetDenom(2) it-IT", LogManager.enumLogLevel.Info);
                    }
                    LogManager.WriteLog("OpenSerialComPort() ACK", LogManager.enumLogLevel.Info);
                    LogManager.WriteLog("StartRead() CALL", LogManager.enumLogLevel.Info);
                    objNoteCumTktScanLib.StartRead();
                    LogManager.WriteLog("StartRead() ACK", LogManager.enumLogLevel.Info);
                    bStartClicked = true;
                    btnStartStop.Text = this.GetResourceTextByKey("Key_StopCounterCaption");
                }

            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Exception Thrown is Start/Stop Button Click", LogManager.enumLogLevel.Debug);
                ExceptionManager.Publish(ex);
                this.SetStatus(this.GetResourceTextByKey(1,"MSG_VALUT_DEC_COMPORT"));//"Unable to open COM Port. Please check connectivity");
                btnStartStop.Text = this.GetResourceTextByKey("Key_StartCounterCaption");

            }
            finally
            {
                btnStartStop.Enabled = true && _bIsEditPermissionEnabled;
                this.Cursor = Cursors.Default;
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                LogManager.WriteLog("Inside btnClear_Click", LogManager.enumLogLevel.Info);
                grpCassette.ClearGroup();
                grpHopper.ClearGroup();
                txtDeclaredBalance.Text = "0.00";
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                lblStatus.Text = "";
                if (_IsCassettesDefined)
                {
                    foreach (ucCassette cassette in grpCassette.GroupMembers)
                    {
                        if (!cassette.IsValidDenom())
                        {
                            lblStatus.Text = this.GetResourceTextByKey(1, "MSG_VAULT_INVALIDAMOUNT") + cassette.CassetteName;
                            cassette.SetFocus();
                            return;
                        }

                        if (cassette.CapacityExceeded)
                        {
                            lblStatus.Text = string.Format(this.GetResourceTextByKey(1,"MSG_VAULT_CASSETTEAMOUNT"), _strCurrency, cassette.MaxFillAmount, cassette.CassetteName);
                            cassette.SetFocus();
                            return;
                        }
                    }

                    foreach (ucCassette cassette in grpHopper.GroupMembers)
                    {
                        if (!cassette.IsValidDenom())
                        {
                            lblStatus.Text = this.GetResourceTextByKey(1, "MSG_VAULT_INVALIDAMOUNT") + cassette.CassetteName;
                            cassette.SetFocus();
                            return;
                        }

                        if (cassette.CapacityExceeded)
                        {
                            lblStatus.Text = string.Format(this.GetResourceTextByKey(1,"MSG_VAULT_HOPPER"), _strCurrency, cassette.MaxFillAmount, cassette.CassetteName);
                            cassette.SetFocus();
                            return;
                        }
                    }
                }
                else
                {
                    decimal Amount = 0.00M;
                    foreach (ucCassette cassette in grpCassette.GroupMembers)
                    {
                        if (!cassette.IsValidDenom())
                        {
                            lblStatus.Text = this.GetResourceTextByKey(1, "MSG_VAULT_INVALIDAMOUNT") + cassette.CassetteName;
                            cassette.SetFocus();
                            return;
                        }

                        Amount += cassette.Amount;
                    }
                    foreach (ucCassette cassette in grpHopper.GroupMembers)
                    {
                        if (!cassette.IsValidDenom())
                        {
                            lblStatus.Text = this.GetResourceTextByKey(1, "MSG_VAULT_INVALIDAMOUNT") + cassette.CassetteName;
                            cassette.SetFocus();
                            return;
                        }
                        Amount += cassette.Amount;
                    }
                    if (objUndeclaredDrop.Capacity < Amount)
                    {
                        lblStatus.ForeColor = Color.Red;
                        lblStatus.Text = this.GetResourceTextByKey(1, "MSG_VAULT_CAPACITY");
                        return;
                    }
                }

                Decimal dValue;
                Decimal.TryParse(txtDeclaredBalance.Text, out dValue);
                if (dValue == 0)
                {
                    if (Win32Extensions.ShowQuestionMessageBox(this, this.GetResourceTextByKey(1, "MSG_NO_CASH_ENTERED"), this.Text) == System.Windows.Forms.DialogResult.No)
                    {
                        return;
                    }
                    else
                    {
                        if (SaveDeclaration())
                        {
                            return;
                        }
                    }
                }
                if (Win32Extensions.ShowQuestionMessageBox(this, this.GetResourceTextByKey(1, "MSG_SAVE_DECLARATION"), this.Text) == System.Windows.Forms.DialogResult.Yes)
                {
                    if (SaveDeclaration())
                    {
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                Win32Extensions.ShowErrorMessageBox(this, this.GetResourceTextByKey(1, "MSG_UPDATE_FAILED"), this.Text);
            }
        }

        private void textBoxNumbersOnly_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                LogManager.WriteLog("Inside textBoxNumbersOnly_KeyPress", LogManager.enumLogLevel.Info);
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
                {
                    e.Handled = true;
                }
                if ((sender as TextBox).Name == "txtCoinsValue")
                {
                    if (e.KeyChar == '.'
                        && (sender as TextBox).Text.IndexOf('.') > -1)
                    {
                        e.Handled = true;
                    }
                }

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void frmVaultDeclaration_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                if (btnSave.Enabled && (grpCassette.IsChildQuantityModified || grpHopper.IsChildQuantityModified) && lvDropDetails.Items[lvDropDetails.Items.Count - 1].Selected == true)
                {
                    if (Win32Extensions.ShowQuestionMessageBox(this, this.GetResourceTextByKey(1, "MSG_UNCOMMITED_DATA_FOUND"), this.Text) == System.Windows.Forms.DialogResult.Yes)
                    {
                        if (!SaveDeclaration())
                            return;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        #endregion

        #region Load Methods

        //private void LoadSiteCombo()
        //{
        //    LogManager.WriteLog("Inside LoadSiteCombo()", LogManager.enumLogLevel.Info);
        //    cmbSite.DisplayMember = "Site_Name";
        //    cmbSite.ValueMember = "Site_ID";
        //    cmbSite.DataSource = VaultDeclarationBiz.GetAllSitesDetails(AppGlobals.Current.UserId);
        //    cmbSite.SelectedValue = _iSiteID;
        //    lvDropDetails.Focus();
        //}

        private void LoadVaultDetails()
        {
            LogManager.WriteLog("Inside LoadVaultDetails()", LogManager.enumLogLevel.Info);

            ClearControls();
            lvDropDetails.Items.Clear();

            List<Vault_UndeclaredDrops> lstUndeclaredDrops = VaultDeclarationBiz.GetUndeclaredDrops(0, _iSiteID);
            _lstUndeclaredDrops = lstUndeclaredDrops;
            if (lstUndeclaredDrops != null && lstUndeclaredDrops.Count > 0)
            {
                foreach (Vault_UndeclaredDrops item in lstUndeclaredDrops)
                {

                    ListViewItem lvItem = new ListViewItem(item.Drop_ID.ToString());
                    lvItem.Tag = item;
                    lvItem.SubItems.Add(new ListViewItem.ListViewSubItem(lvItem, item.CreatedDate.ToString()));
                    lvItem.SubItems.Add(new ListViewItem.ListViewSubItem(lvItem, item.Site_Drop_Ref.ToString()));
                    lvDropDetails.Items.Add(lvItem);
                    if (lvDropDetails.Items.Count != lstUndeclaredDrops.Count)
                        lvItem.BackColor = Color.LightGray;

                }
                lvDropDetails.Items[lvDropDetails.Items.Count - 1].Selected = true;
                lvDropDetails.Items[lvDropDetails.Items.Count - 1].EnsureVisible();
                if (grpCassette.GroupMembers.Count > 0)
                {
                    btnStartStop.Enabled = true && _bIsEditPermissionEnabled;
                }
                else
                {
                    btnStartStop.Enabled = false;
                }
                btnClear.Enabled = true;
                btnSave.Enabled = _bIsEditPermissionEnabled;
                btnStartStop.Focus();
            }
            else
            {
                this.Close();
            }
        }

        #endregion

        #region Update Methods

        void ClearDropControls()
        {
            try
            {
                txtFillAmount.Text = decimal.Zero.ToString();
                txtBleedAmount.Text = decimal.Zero.ToString();
                txtAdjustmentAmount.Text = decimal.Zero.ToString();
                txtMeterBalance.Text = decimal.Zero.ToString();
                txtVaultBalance.Text = decimal.Zero.ToString();
                txtDeclaredBalance.Text = decimal.Zero.ToString();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private bool SaveDeclaration()
        {
            XElement _xmlCassette = null;
            bool bStatus = true;
            if (_IsCassettesDefined)
            {
                _xmlCassette = new XElement("CassetteDetails");
                foreach (ucCassette cassette in grpCassette.GroupMembers)
                {
                    if (!cassette.IsValidDenom())
                    {
                        bStatus = false;
                        lblStatus.Text = this.GetResourceTextByKey(1, "MSG_VAULT_INVALIDAMOUNT") + cassette.CassetteName;
                        cassette.SetFocus();
                        return bStatus;
                    }

                    if (objUndeclaredDrop.IsWebServiceEnabled)
                    {
                        if (cassette.CapacityExceeded)
                        {
                            bStatus = false;
                            lblStatus.Text = string.Format(this.GetResourceTextByKey(1, "MSG_VAULT_AMOUNTCAPA"), _strCurrency, cassette.MaxFillAmount);
                            cassette.SetFocus();
                            return bStatus;
                        }
                    }
                    _xmlCassette.Add(cassette.CasseteXML);
                }

                foreach (ucCassette cassette in grpHopper.GroupMembers)
                {
                    if (!cassette.IsValidDenom())
                    {
                        bStatus = false;
                        lblStatus.Text = this.GetResourceTextByKey(1, "MSG_VAULT_INVALIDAMOUNT") + cassette.CassetteName;
                        cassette.SetFocus();
                        return bStatus;
                    }

                    if (objUndeclaredDrop.IsWebServiceEnabled)
                    {
                        if (cassette.CapacityExceeded)
                        {
                            bStatus = false;
                            lblStatus.Text = string.Format(this.GetResourceTextByKey(1, "MSG_VAULT_AMOUNTCAPA"), _strCurrency, cassette.MaxFillAmount);
                            cassette.SetFocus();
                            return bStatus;
                        }
                    }
                    _xmlCassette.Add(cassette.CasseteXML);
                }
            }
            else
            {
                decimal Amount = 0.00M;
                foreach (ucCassette cassette in grpCassette.GroupMembers)
                {
                    Amount += cassette.Amount;
                }
                foreach (ucCassette cassette in grpHopper.GroupMembers)
                {
                    Amount += cassette.Amount;
                }
                if (objUndeclaredDrop.Capacity < Amount)
                {
                    bStatus = false;
                    lblStatus.Text = this.GetResourceTextByKey(1, "MSG_VAULT_CAPACITY");
                    lblStatus.ForeColor = Color.Red;
                    return bStatus;
                }
            }

            LogManager.WriteLog("Inside SaveDeclaration()", LogManager.enumLogLevel.Info);
            decimal temp = decimal.Parse(txtDeclaredBalance.Text);
            Vault_UndeclaredDrops objDrop = (Vault_UndeclaredDrops)lvDropDetails.Items[lvDropDetails.Items.Count - 1].Tag;

            int result = VaultDeclarationBiz.UpdateVaultDrops(temp, true, objDrop.Drop_ID, _iSiteID.ToString(), AppGlobals.Current.UserId, _xmlCassette);
            if (result == 0)
            {
                Vault_UndeclaredDrops objUndeclaredDrops = new Vault_UndeclaredDrops();
                objUndeclaredDrops.Declared_Balance = decimal.Parse(txtDeclaredBalance.Text);

                AuditVaultDeclaration(_lstUndeclaredDrops.Last(), objUndeclaredDrops);
                Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_DETAILS_UPDATE_SUCCESS"), this.Text);
                this.ClearCassetteControls();
            }
            else if (result == -2)
            {
                Win32Extensions.ShowWarningMessageBox(this, this.GetResourceTextByKey(1, "MSG_DROP_DECLARED"), this.Text);
            }
            else
                Win32Extensions.ShowErrorMessageBox(this, this.GetResourceTextByKey(1, "MSG_UPDATE_FAILED"), this.Text);

            txtFillAmount.Text = txtBleedAmount.Text = txtAdjustmentAmount.Text = txtMeterBalance.Text = txtVaultBalance.Text = txtDeclaredBalance.Text = "0";

            this.LoadVaultDetails();

            return bStatus;
        }

        #endregion

        #region Miscellaneous Methods

        /// <summary>
        /// Assigns the Resource Key names to the controls--Created by kishore sivagnanam
        /// </summary>
        public void SetTagProperty()
        {
            this.btnClear.Tag = "Key_ClearCaption";
            this.btnSave.Tag = "Key_SaveCaption";
            this.lblAdjustmentAmount.Tag = "Key_AdjustmentAmountColon";
            this.lblBleedAmount.Tag = "Key_BleedAmountColon";
            this.lblDeclarationDetails.Tag = "Key_DeclarationDetails";
            this.lblDeclaredBalance.Tag = "Key_DeclaredBalanceColon";
            this.clm_Drop.Tag = "Key_DropCreated";
            this.lblDropDetails.Tag = "Key_DropDetails";
            this.clm_DropID.Tag = "Key_DropID";
            this.lblFillAmount.Tag = "Key_FillAmountColon";
            this.lblManufacturer.Tag = "Key_ManufacturerColon";
            this.lblMeterBalance.Tag = "Key_MeterBalanceColon";
            this.btnStartStop.Tag = "Key_StartCounterCaption";
            this.clmDropSiteRef.Tag = "Key_SiteDropRef";
            this.lblSite.Tag = "Key_SiteColon";
            this.label3.Tag = "Key_TypeColon";
            this.lblVaultBalance.Tag = "Key_VaultBalance";
            this.lblVaultDetails.Tag = "Key_VaultDetails";
            this.lblVault.Tag = "Key_VaultColon";
            this.Tag = "Key_VaultDeclaration";

        }

        private void LoadCurrencyByRegion()
        {
            try
            {

                lblFillAmount.Text = string.Format("{0} ({1})", lblFillAmount.Text, _strCurrency);
                lblBleedAmount.Text = string.Format("{0} ({1})", lblBleedAmount.Text, _strCurrency);
                lblAdjustmentAmount.Text = string.Format("{0} ({1})", lblAdjustmentAmount.Text, _strCurrency);
                lblMeterBalance.Text = string.Format("{0} ({1})", lblMeterBalance.Text, _strCurrency);
                lblVaultBalance.Text = string.Format("{0} ({1})", lblVaultBalance.Text, _strCurrency);
                lblDeclaredBalance.Text = string.Format("{0} ({1})", lblDeclaredBalance.Text, _strCurrency);
                //lblTotalAmount.Text = string.Format("{0} ({1})", lblTotalAmount.Text, _strCurrency);
                //lblTotalBills.Text = string.Format("{0} ({1})", lblTotalBills.Text, _strCurrency);
                //lblCoins.Text = string.Format("{0} ({1})", lblCoins.Text, _strCurrency);

                string strDenoms = ConfigurationManager.AppSettings[ExtensionMethods.CurrentSiteCulture + "_Denom"].ToString();

                foreach (string strDenom in strDenoms.Split(':')[0].Split(','))
                {
                    int itemp;
                    int.TryParse(strDenom, out itemp);
                    if (itemp != 0)
                        _CashDenom.Add(itemp);
                }

                foreach (string strDenom in strDenoms.Split(':')[1].Split(','))
                {
                    decimal dtemp;
                    decimal.TryParse(strDenom, out dtemp);
                    if (dtemp != 0)
                        _CoinDenom.Add(dtemp);
                }



            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void FillCountedCash(string sNotesString)
        {
            LogManager.WriteLog("Inside FillCountedCash()", LogManager.enumLogLevel.Info);
            string[] sDenoms = sNotesString.Split(',');
            foreach (string item in sDenoms)
            {
                string[] BillDenom = item.Split(':');
                List<ucCassette> _denomCassetteList = null;
                try
                {
                    switch (BillDenom[0])
                    {
                        //ONES:10,TWOS:15,FIVES:5,TENS:3,TWENTIES:2,FIFTIES:4,HUNDREDS:3"
                        case "ONES":
                            _denomCassetteList = grpCassette[1];
                            foreach (ucCassette objucCassette in _denomCassetteList)
                            {
                                if (objucCassette.IsSelected)
                                {

                                    objucCassette.Quantity = objucCassette.Quantity + int.Parse(BillDenom[1]);
                                    if (_IsAmountEditable)
                                        objucCassette.Amount = objucCassette.Quantity * objucCassette.Denom;
                                    break;
                                }
                            }
                            break;
                        case "TWOS":
                            _denomCassetteList = grpCassette[2];
                            foreach (ucCassette objucCassette in _denomCassetteList)
                            {
                                if (objucCassette.IsSelected)
                                {
                                    objucCassette.Quantity = objucCassette.Quantity + int.Parse(BillDenom[1]);
                                    if (_IsAmountEditable)
                                        objucCassette.Amount = objucCassette.Quantity * objucCassette.Denom;
                                    break;
                                }
                            }

                            break;
                        case "FIVES":
                            _denomCassetteList = grpCassette[5];
                            foreach (ucCassette objucCassette in _denomCassetteList)
                            {
                                if (objucCassette.IsSelected)
                                {
                                    objucCassette.Quantity = objucCassette.Quantity + int.Parse(BillDenom[1]);
                                    if (_IsAmountEditable)
                                        objucCassette.Amount = objucCassette.Quantity * objucCassette.Denom;
                                    break;
                                }
                            }
                            break;
                        case "TENS":
                            _denomCassetteList = grpCassette[10];
                            foreach (ucCassette objucCassette in _denomCassetteList)
                            {
                                if (objucCassette.IsSelected)
                                {
                                    objucCassette.Quantity = objucCassette.Quantity + int.Parse(BillDenom[1]);
                                    if (_IsAmountEditable)
                                        objucCassette.Amount = objucCassette.Quantity * objucCassette.Denom;
                                    break;
                                }
                            }
                            break;
                        case "TWENTIES":
                            _denomCassetteList = grpCassette[20];
                            foreach (ucCassette objucCassette in _denomCassetteList)
                            {
                                if (objucCassette.IsSelected)
                                {
                                    objucCassette.Quantity = objucCassette.Quantity + int.Parse(BillDenom[1]);
                                    if (_IsAmountEditable)
                                        objucCassette.Amount = objucCassette.Quantity * objucCassette.Denom;
                                    break;
                                }
                            }
                            break;
                        case "FIFTIES":
                            _denomCassetteList = grpCassette[50];
                            foreach (ucCassette objucCassette in _denomCassetteList)
                            {
                                if (objucCassette.IsSelected)
                                {
                                    objucCassette.Quantity = objucCassette.Quantity + int.Parse(BillDenom[1]);
                                    if (_IsAmountEditable)
                                        objucCassette.Amount = objucCassette.Quantity * objucCassette.Denom;
                                    break;
                                }
                            }
                            break;
                        case "HUNDREDS":
                            _denomCassetteList = grpCassette[100];
                            foreach (ucCassette objucCassette in _denomCassetteList)
                            {
                                if (objucCassette.IsSelected)
                                {
                                    objucCassette.Quantity = objucCassette.Quantity + int.Parse(BillDenom[1]);
                                    if (_IsAmountEditable)
                                        objucCassette.Amount = objucCassette.Quantity * objucCassette.Denom;
                                    break;
                                }
                            }
                            //txt100billQty.Text = BillDenom[1].ToString();
                            break;
                        default:
                            //log here
                            break;
                    }
                    grpHopper.SumGroupsDropped();
                    grpCassette.SumGroupsDropped();
                    this.SetDeclaredAmount();
                }
                catch (Exception Ex)
                {

                    ExceptionManager.Publish(Ex);
                }
            }
        }

        private void ClearControls()
        {
            //txt100billQty.Text = txt50billQty.Text = txt20billQty.Text = txt10billQty.Text = txt5billQty.Text = txt2billQty.Text = txt1billQty.Text = txtCoinsValue.Text = "0";
            //txtDeclaredBalance.Text = "0";
        }

        private void SetStatus(string strMessage)
        {
            lblStatus.Text = strMessage;
        }

        void ClearCassetteControls()
        {
            flpDeclaration.Controls.Clear();
            ucCassetteHeader ucHeader = new ucCassetteHeader();
            ucHeader.Height = 25;
            ucHeader.Width = flpDeclaration.ClientSize.Width - 30;
            flpDeclaration.Controls.Add(ucHeader);
            ucHeader.setLabels();
        }

        void ConstructDefaultCassetteDetails()
        {

            this.ClearCassetteControls();
            grpCassette.ResetChildren();
            grpHopper.ResetChildren();
            grpCassette.Height = 25;
            grpCassette.OnQuantityChange += new EventHandler(Quantity_Changed);
            grpCassette.Width = flpDeclaration.ClientSize.Width - 30;
            grpCassette.Name = "grpCassette";
            grpCassette.CassetteName = _sTotalBills;// "Total Bills:";


            grpHopper.Height = 25;
            grpHopper.OnQuantityChange += new EventHandler(Quantity_Changed);
            grpHopper.Width = flpDeclaration.ClientSize.Width - 30;
            grpHopper.Name = "grpHopper";
            grpHopper.CassetteName = _sTotalCoins; //"Total Coins:";

            //foreach()
            //{
            //    //if(iterator,tyoe=caseetetype.casset
            //        //grpCassette.SubscribeAsChild()
            //    else 
            //    //grpHopperSubscribeAsChild
            //}

            foreach (int iDenom in _CashDenom)
            {
                ucCassette uc = new ucCassette(CassetteType.Cassette, _IsAmountEditable);
                uc.OnInvalidDenom = OnInvalidDenomMethod;                
                //uc.Size=new Size(300,20);
                uc.Height = 25;
                uc.OnQuantityChange += new EventHandler(Quantity_Changed);
                uc.Width = flpDeclaration.ClientSize.Width - 30;
                uc.Name = "txtQuantity_" + iDenom;
                uc.Denom = iDenom;
                uc.Quantity = 0;
                uc.CassetteName = string.Format("Cassette {0} ", iDenom.ToString());
                grpCassette.SubscribeAsChild(uc);
                flpDeclaration.Controls.Add(uc);
            }

            //Rejection cassette 
            ucCassette RC = new ucCassette(CassetteType.Cassette, _IsAmountEditable);
            RC.OnInvalidDenom = OnInvalidDenomMethod;
            //uc.Size=new Size(300,20);
            RC.Height = 25;
            RC.OnQuantityChange += new EventHandler(Quantity_Changed);
            RC.Width = flpDeclaration.ClientSize.Width - 30;
            RC.Name = "txtQuantity_" + "1";
            RC.Denom = 1;
            RC.Quantity = 0;
            RC.CassetteName = "Rejection";
            grpCassette.SubscribeAsChild(RC);
            flpDeclaration.Controls.Add(RC);

            grpCassette.SumGroupsDropped();
            flpDeclaration.Controls.Add(grpCassette);

            foreach (decimal dDenom in _CoinDenom)
            {
                ucCassette uc = new ucCassette(CassetteType.Hopper, _IsAmountEditable);
				uc.OnInvalidDenom = OnInvalidDenomMethod;
                //uc.Size=new Size(300,20);
                uc.Height = 25;
                uc.OnQuantityChange += new EventHandler(Quantity_Changed);
                uc.Width = flpDeclaration.ClientSize.Width - 30;
                uc.Name = "txtQuantity_" + dDenom.ToString();
                uc.Denom = dDenom;
                uc.Quantity = 0;
                uc.CassetteName = string.Format("Hopper {0} ", dDenom.ToString());
                grpHopper.SubscribeAsChild(uc);
                flpDeclaration.Controls.Add(uc);
            }

            grpHopper.SumGroupsDropped();
            flpDeclaration.Controls.Add(grpHopper);
        }

        void OnInvalidDenomMethod(string Source,string Message)
        {
            lblStatus.Text = this.GetResourceTextByKey("MSG_VAULT_INVALIDAMOUNT") + Source;
        }

        #endregion

        #region Vault Declaration Auditing

        private void AuditVaultDeclaration(Vault_UndeclaredDrops objOldData, Vault_UndeclaredDrops objNewData)
        {
            if ((objOldData == null) || (objNewData == null)) return;
            VaultDeclarationBusiness objVaultDeclarationbiz = new VaultDeclarationBusiness();

            if (objOldData.Declared_Balance != objNewData.Declared_Balance)
                objVaultDeclarationbiz.AuditModifiedDataForVaultAdjustment(objOldData.Name.ToString(), "Vault Declaration", "Declared Balance for Drop ID " + objOldData.Drop_ID.ToString(), objOldData.Declared_Balance.ToString(), objNewData.Declared_Balance.ToString(), AppEntryPoint.Current.UserId, AppEntryPoint.Current.UserName);

        }

        #endregion

        private void frmVaultDeclaration_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (_lstUndeclaredDrops.Count > 0)
                {
                    if (Win32Extensions.ShowQuestionMessageBox(this, this.GetResourceTextByKey(1, "MSG_CONFIRM_CLOSE"), this.Text) == DialogResult.No)
                    {
                        e.Cancel = true; ;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void txtSiteName_MouseHover(object sender, EventArgs e)
        {
            ToolTip tp = new ToolTip();
            tp.SetToolTip(txtSiteName, txtSiteName.Text);
        }

    }
}