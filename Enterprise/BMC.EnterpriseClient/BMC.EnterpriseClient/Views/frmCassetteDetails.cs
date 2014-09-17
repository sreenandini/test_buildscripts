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
using BMC.Common.ExceptionManagement;
using Audit.Transport;
using BMC.Common.LogManagement;
using BMC.EnterpriseClient.Helpers;
using System.Configuration;
using BMC.Common.Utilities;
using System.Globalization;
using BMC.Common;

namespace BMC.EnterpriseClient.Views
{
    public enum CassetteType
    {
        Cassette = 1,
        Rejection = 2,
        Hopper = 50
    }

    public partial class frmCassetteDetails : Form
    {
        #region Declared Variables

        VaultAdmin VaultBiz = new VaultAdmin();
        readonly int _Vault_id;
        readonly VaultDetails _SelectedVault = null;
        readonly CassetteType _Type = 0;

        int NoofCaassette = 6;
        int NoOfHoppers = 3;
        int NoOfRejection = 1;
        string _strCurrency = "$";

        Vault_GetCassetteDetails _SelectCassette = null;
        List<Vault_GetCassetteDetails> _Cassettes = null;
        bool _bIsEditPermissionEnabled = false;

        #endregion

        #region Costructor

        private frmCassetteDetails()
        {
            InitializeComponent();
            SetPropertyTag();//Externalization changes
        }

        //Externalization changes
        private void SetPropertyTag()
        {
            try
            {
                this.btnAdd.Tag = "Key_AddCaption";
                this.btnClose.Tag = "Key_CloseCaption";
                this.btnSave.Tag = "Key_SaveCaption";
                this.lblAlertLevel.Tag = "Key_AlertLevelMandatoryColon";               
                this.lblDescription.Tag = "Key_DescriptionMandatory";              
                this.lblCassetteName.Tag = "Key_NameMandatory";              
                this.clmAlertLevel.Tag = "Key_AlertLevel";
                this.clmDenom.Tag = "Key_Denom";
                this.lbl_IsActive.Tag = "Key_IsActive";
                this.clmIsActive.Tag = "Key_IsActive";
                this.clmMaxFillAmount.Tag = "Key_MaxFillAmount";
                this.clmCasetteName.Tag = "Key_Name";
                this.clmStandardFillAmount.Tag = "Key_StandardFillAmount";
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        public frmCassetteDetails(int Vault_ID, CassetteType Type, VaultDetails Vault)
            : this()
        {
            //Set all initial properties
            try
            {
                _Type = Type;
                _Vault_id = Vault_ID;
                
                //lbl_Header.Text = Type + " " + "Details";
                //this.Text = Type.ToString();

                _SelectedVault = Vault;
                _strCurrency = new RegionInfo(BMC.Common.ConfigurationManagement.ConfigManager.Read("GetDefaultCultureForCurrency")).CurrencySymbol;
                //Get the Cassette and Hopper Settings
                if (_Type == CassetteType.Cassette)
                {
                    NoofCaassette = SettingsEntity.MaxNoOfVaultCassettes;

                    // Set header text
                    this.Text = this.GetResourceTextByKey("Key_CassetteCaption");                    
                }
                else if (_Type == CassetteType.Hopper)
                {
                    NoOfHoppers = SettingsEntity.MaxNoOfVaultHoppers;
                    
                    // Set header text
                    this.Text = this.GetResourceTextByKey("Key_HopperCaption");
                }
                else if (_Type == CassetteType.Rejection)
                {
                    NoOfRejection = 1;
                    txtStandardFillAmount.Text = "0.00";
                    txtStandardFillAmount.Enabled = false;
                    
                    // Set header text
                    this.Text = this.GetResourceTextByKey("Key_RejectionCaption");
                }
                lbl_Header.Text = this.Text + " " + this.GetResourceTextByKey("Key_Details");
            }
            catch (Exception Ex)
            {

                ExceptionManager.Publish(Ex);
            }

        }

        #endregion

        #region Events

        private void frmCassetteDetails_Load(object sender, EventArgs e)
        {
            try
            {
                _bIsEditPermissionEnabled = AppGlobals.Current.HasUserAccess("HQ_Admin_EditCreateVault"); ;

                FormatDisplayControls();

                //load cmbDenom with denoms based on  type (cassette or hopper)
                this.LoadDenomsForCassetteType();

                //load all the cassettes and hoppers for vault
                this.LoadAllCassettesForVault();
                txtCassetteName.Focus();
                if (_Type == CassetteType.Cassette)
                {
                    if (dgvCasetteDetails.Rows.Count == NoofCaassette)
                    {
                        btnAdd.Enabled = false;
                        btnSave.Enabled = true && _bIsEditPermissionEnabled;
                        lbl_Status.Text = string.Format(this.GetResourceTextByKey(1,"MSG_CASSETTE_RESTRICT"), NoofCaassette);
                    }
                }
                else if (_Type == CassetteType.Hopper)
                {
                    if (dgvCasetteDetails.Rows.Count == NoOfHoppers)
                    {
                        btnAdd.Enabled = false;
                        btnSave.Enabled = true && _bIsEditPermissionEnabled;
                        lbl_Status.Text = string.Format(this.GetResourceTextByKey(1,"MSG_CASSETTE_HOPPERS"), NoOfHoppers); 
                    }
                }
                else if (_Type == CassetteType.Rejection)
                {
                    if (dgvCasetteDetails.Rows.Count == NoOfRejection)
                    {
                        btnAdd.Enabled = false;
                        btnSave.Enabled = true && _bIsEditPermissionEnabled;
                        lbl_Status.Text = string.Format(this.GetResourceTextByKey(1,"MSG_CASSETTE_REJECTION"), NoOfRejection);
                    }
                }
                this.ResolveResources(); //Externalization changes
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }   
        }

        private void FormatDisplayControls()
        {
            lblDenom.Tag = string.Format(this.GetResourceTextByKey("Key_DenomMandatory"), _strCurrency);
            lblMaxFillAmount.Text = string.Format(this.GetResourceTextByKey("Key_MaxFillAmountMandatory"), _strCurrency);
            lblStandardFillAmount.Text = string.Format(this.GetResourceTextByKey("Key_StandardFillAmountMandatory"), _strCurrency);
            this.lblDenom.Text = string.Format(this.GetResourceTextByKey("Key_DenomMandatory"), _strCurrency);
            this.lblMaxFillAmount.Text = string.Format(this.GetResourceTextByKey("Key_MaxFillAmountMandatory"), _strCurrency);
            this.lblStandardFillAmount.Text = string.Format(this.GetResourceTextByKey("Key_StandardFillAmountMandatory"), _strCurrency); 
            dgvCasetteDetails.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvCasetteDetails.Columns[2].HeaderText = string.Format("{0} ({1})", dgvCasetteDetails.Columns[2].HeaderText , _strCurrency );
            dgvCasetteDetails.Columns[4].HeaderText = string.Format("{0} ({1})", dgvCasetteDetails.Columns[4].HeaderText, _strCurrency);
            dgvCasetteDetails.Columns[5].HeaderText = string.Format("{0} ({1})", dgvCasetteDetails.Columns[5].HeaderText, _strCurrency);
        }



        private void dgvCasetteDetails_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                this.LoadSelectedCassetteDetails();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                ClearControls();
                _SelectCassette = new Vault_GetCassetteDetails();
                if (btnAdd.Tag.ToString() == "Key_AddCaption")  // Key_AddCaption
                {
                    txtStandardFillAmount.Denom = decimal.Parse(cmbDemon.SelectedValue.ToString());
                    txtMaxFillAmount.Denom = decimal.Parse(cmbDemon.SelectedValue.ToString());

                    btnAdd.Tag = "Key_CancelCaption";
                    btnAdd.Text = this.GetResourceTextByKey("Key_CancelCaption");             //  "&Cancel";
                    
                    btnSave.Enabled = true;
                    EnableDisableControls(true);
                    dgvCasetteDetails.Enabled = false;
                    txtCassetteName.Focus();
                }
                else
                {
                    btnAdd.Tag = "Key_AddCaption";
                    btnAdd.Text = this.GetResourceTextByKey("Key_AddCaption");            // "&Add";
                    EnableDisableControls(false);
                    dgvCasetteDetails.Enabled = true;
                    LoadAllCassettesForVault();
                    btnAdd.Focus();
                }
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
                UpdateCasseteAndHopperDetails();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                Win32Extensions.ShowWarningMessageBox(this, this.GetResourceTextByKey(1, "MSG_CASSETTE_FAILED"), this.Text);
            }
        }

        private void cmbDemon_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (_SelectCassette != null)
                {
                    txtStandardFillAmount.Denom = decimal.Parse(cmbDemon.SelectedValue.ToString());
                    txtMaxFillAmount.Denom = decimal.Parse(cmbDemon.SelectedValue.ToString());
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
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

        private void txtAlertLevel_Enter(object sender, EventArgs e)
        {
            lbl_Status.Text = this.GetResourceTextByKey(1,"MSG_CASSETTE_LEVEL");
        }

        private void txtAlertLevel_Leave(object sender, EventArgs e)
        {
            lbl_Status.Text = string.Empty;
        }

        private void txtStandardFillAmount_Enter(object sender, EventArgs e)
        {
            lbl_Status.Text = this.GetResourceTextByKey(1,"MSG_CASSETTE_VALIDATION");
        }

        private void txtStandardFillAmount_Leave(object sender, EventArgs e)
        {
            lbl_Status.Text = "";
        }

        private void frmCassetteDetails_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {

                if (this.ShowQuestionMessageBox(this.GetResourceTextByKey(1, "MSG_CONFIRM_CLOSE"), this.Text) == DialogResult.No)
                {
                    e.Cancel = true; ;
                }

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        #endregion

        #region Load Methods

        private void LoadDenomsForCassetteType()
        {

            this.UpdateControlsForCassetteType(_Type);


        }

        private void LoadAllCassettesForVault()
        {
            ClearControls();
            List<Vault_GetCassetteDetails> Cassettes = null;
            Cassettes = VaultBiz.GetCassetteDetails(_Vault_id, (int)_Type);
            dgvCasetteDetails.AutoGenerateColumns = false;
            dgvCasetteDetails.DataSource = Cassettes;
            _Cassettes = Cassettes;

            foreach (DataGridViewRow gvrows in dgvCasetteDetails.Rows)
            {
                Vault_GetCassetteDetails objCassettes = (Vault_GetCassetteDetails)gvrows.DataBoundItem;
                if (objCassettes.IsActive)
                {
                    gvrows.DefaultCellStyle.BackColor = Color.SkyBlue;
                }
            }

            if (dgvCasetteDetails.Rows.Count > 0 && _bIsEditPermissionEnabled)
            {
                EnableDisableControls(true);
                if (_Type == CassetteType.Cassette)
                {
                    if (dgvCasetteDetails.Rows.Count == NoofCaassette)
                    {
                        btnAdd.Enabled = false;
                        btnSave.Enabled = true && _bIsEditPermissionEnabled;
                        lbl_Status.Text = string.Format(this.GetResourceTextByKey(1, "MSG_CASSETTE_RESTRICT"), NoofCaassette);
                    }
                }
                else if (_Type == CassetteType.Hopper)
                {
                    if (dgvCasetteDetails.Rows.Count == NoOfHoppers)
                    {
                        btnAdd.Enabled = false;
                        btnSave.Enabled = true && _bIsEditPermissionEnabled;
                        lbl_Status.Text = string.Format(this.GetResourceTextByKey(1, "MSG_CASSETTE_HOPPERS"), NoofCaassette);
                    }
                }
                else if (_Type == CassetteType.Rejection)
                {
                    if (dgvCasetteDetails.Rows.Count == NoOfRejection)
                    {
                        btnAdd.Enabled = false;
                        btnSave.Enabled = true && _bIsEditPermissionEnabled;
                        lbl_Status.Text = string.Format(this.GetResourceTextByKey(1, "MSG_CASSETTE_REJECTION"), NoofCaassette);
                    }
                }
            }
            else
            {
                EnableDisableControls(false);
                btnAdd.Enabled = true && _bIsEditPermissionEnabled && !_SelectedVault.IsAssigned;
                btnSave.Enabled = false;
            }
        }

        private void LoadSelectedCassetteDetails()
        {
            if (dgvCasetteDetails.SelectedRows.Count > 0)
            {
                Vault_GetCassetteDetails CasstetteDetails = (Vault_GetCassetteDetails)dgvCasetteDetails.SelectedRows[0].DataBoundItem;
                _SelectCassette = CasstetteDetails;
                txtCassetteName.Text = CasstetteDetails.Cassette_Name;
                txtAlertLevel.Text = CasstetteDetails.AlertLevel.ToString();
                txtStandardFillAmount.Text = CasstetteDetails.StandardFillAmount.ToString();
                txtMaxFillAmount.Text = CasstetteDetails.MaxFillAmount.ToString();
                txtDescription.Text = CasstetteDetails.DESCRIPTION;
                cbIsActive.Checked = CasstetteDetails.IsActive;

                if (_Type == CassetteType.Cassette)
                {
                    cmbDemon.SelectedValue = (int)CasstetteDetails.Denom;
                    txtMaxFillAmount.Denom = (int)CasstetteDetails.Denom;
                    txtStandardFillAmount.Denom = (int)CasstetteDetails.Denom;

                    if (dgvCasetteDetails.Rows.Count == NoofCaassette)
                    {
                        btnAdd.Enabled = false;
                        btnSave.Enabled = true && _bIsEditPermissionEnabled;
                        lbl_Status.Text = string.Format(this.GetResourceTextByKey(1, "MSG_CASSETTE_RESTRICT"), NoofCaassette);
                    }
                }
                else if (_Type == CassetteType.Hopper)
                {
                    txtMaxFillAmount.Denom = (decimal)CasstetteDetails.Denom;
                    txtStandardFillAmount.Denom = (decimal)CasstetteDetails.Denom;
                    cmbDemon.SelectedValue = CasstetteDetails.Denom.ToString("n2");

                    if (dgvCasetteDetails.Rows.Count == NoOfHoppers)
                    {
                        btnAdd.Enabled = false;
                        btnSave.Enabled = true && _bIsEditPermissionEnabled;
                        lbl_Status.Text = string.Format(this.GetResourceTextByKey(1, "MSG_CASSETTE_HOPPERS"), NoofCaassette);
                    }
                }
                else if (_Type == CassetteType.Rejection)
                {
                    txtMaxFillAmount.Denom = (decimal)CasstetteDetails.Denom;
                    txtStandardFillAmount.Denom = (decimal)CasstetteDetails.Denom;
                    cmbDemon.SelectedValue = (int)CasstetteDetails.Denom;

                    if (dgvCasetteDetails.Rows.Count == NoOfRejection)
                    {
                        btnAdd.Enabled = false;
                        btnSave.Enabled = true && _bIsEditPermissionEnabled;
                        lbl_Status.Text = string.Format(this.GetResourceTextByKey(1, "MSG_CASSETTE_REJECTION"), NoofCaassette);
                    }
                }
                txtCassetteName.Focus();
            }
        }

        #endregion

        #region Update Methods

        private void UpdateCasseteAndHopperDetails()
        {
            int CassettedID = 0;
            int Result = 0;

            if (_SelectCassette.Cassette_ID != 0)
            {
                CassettedID = _SelectCassette.Cassette_ID;
            }
            if (txtCassetteName.Text.Trim() == "")
            {
                this.ShowInfoMessageBox(string.Format(this.GetResourceTextByKey(1, "MSG_CASSETTE_VALID_NAME"), _Type.ToString()), this.Text);                                            
                txtCassetteName.Focus();
                return;
            }
            if (txtAlertLevel.CTL_IntValue <= 0 || txtAlertLevel.CTL_IntValue > 100)
            {
                Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_CASSETTE_VALID_ALERT"), this.Text);
                txtAlertLevel.Focus();
                return;
            }

            if (_Type != CassetteType.Rejection)
            {
                if (txtStandardFillAmount.CTL_DecimalValue <= 0)
                {
                    Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_CASSETTE_VALID_FILL"), this.Text);
                    txtStandardFillAmount.Focus();
                    return;
                }
                if (!txtStandardFillAmount.IsValidDenom())
                {
                    Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_CASSETTE_VALID_DENOM"), this.Text);
                    txtStandardFillAmount.Focus();
                    return;
                }
                if (txtMaxFillAmount.CTL_DecimalValue <= 0)
                {
                    Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_CASSETTE_VALID_MAXFILL"), this.Text);
                    txtMaxFillAmount.Focus();
                    return;
                }

                if (!txtMaxFillAmount.IsValidDenom())
                {
                    Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_CASSETTE_VALID_MAXDENOM"), this.Text);
                    txtMaxFillAmount.Focus();
                    return;
                }
            }
            else
            {
                if (txtMaxFillAmount.CTL_DecimalValue <= 0)
                {
                    Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_CASSETTE_VALID_MAXFILL"), this.Text);
                    txtMaxFillAmount.Focus();
                    return;
                }
                if (!txtMaxFillAmount.IsValidDenom())
                {
                    Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_CASSETTE_VALID_MAXDENOM"), this.Text);
                    txtMaxFillAmount.Focus();
                    return;
                }
                txtStandardFillAmount.Text = "0.00";
            }


            if (txtDescription.Text.Trim() == "")
            {
                Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_CASSETTE_VALID_DESC"), this.Text);
                txtDescription.Focus();
                return;
            }

            if (txtStandardFillAmount.CTL_DecimalValue > txtMaxFillAmount.CTL_DecimalValue)
            {
                Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_CASSETTE_STANDARDFILL"), this.Text);
                txtStandardFillAmount.Focus();
                return;
            }

            Result = VaultBiz.Vault_UpdateCassetteDetails(CassettedID,
                                                               _Vault_id, txtCassetteName.Text,
                                                               (int)_Type,
                                                               float.Parse(cmbDemon.Text),
                                                               cbIsActive.Checked,
                                                               Convert.ToInt32(txtAlertLevel.Text),
                                                               decimal.Parse(txtStandardFillAmount.Text),
                                                               decimal.Parse(txtMaxFillAmount.Text),
                                                               txtDescription.Text,
                                                               AppGlobals.Current.UserId,
                                                               (int)ModuleIDEnterprise.VaultManager,
                                                               ModuleIDEnterprise.VaultManager.ToString(),
                                                               "Vault Cassette Details");

            if (Result == 0)
            {
                Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_CASSETTE_SUCCESS"),this.Text);
            }
            else if (Result == -2)
            {
                Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_CASSETTE_NAMEEXISTS"), this.Text);
                txtCassetteName.Focus();
                return;
            }
            else if (Result == -3)
            {
                Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_CASSETTE_ENABLE_WEBSERVICE"), this.Text);
                return;
            }
            else if (Result == -4)
            {
                Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_CASSETTE_MAX_LEVEL"), this.Text);
            }
            else
            {
                LogManager.WriteLog("Error while saving Cassette : Return Code " + Result.ToString(), LogManager.enumLogLevel.Error);
                Win32Extensions.ShowWarningMessageBox(this, this.GetResourceTextByKey(1, "MSG_CASSETTE_FAILED"), this.Text);
            }
            btnAdd.Tag = "Key_AddCaption";
            btnAdd.Text = this.GetResourceTextByKey("Key_AddCaption");
            dgvCasetteDetails.Enabled = true;
            LoadAllCassettesForVault();
        }

        #endregion

        #region View Control Methods

        private void UpdateControlsForCassetteType(CassetteType Type)
        {
            txtStandardFillAmount.AllowDecimal = true;
            txtMaxFillAmount.AllowDecimal = true;
            txtStandardFillAmount.MaxLength = 9;
            txtMaxFillAmount.MaxLength = 9;
            txtStandardFillAmount.DecimalLength = 2;
            txtMaxFillAmount.DecimalLength = 2;

            DataTable DtDenoms = new DataTable();
            DtDenoms.Columns.Add("Value", typeof(decimal));
            DataRow denom;

            string strDenoms = ConfigurationManager.AppSettings[ExtensionMethods.CurrentSiteCulture + "_Denom"].ToString();

            if (Type == CassetteType.Cassette)
            {
                foreach (string strDenom in strDenoms.Split(':')[0].Split(','))
                {
                    int itemp;
                    int.TryParse(strDenom, out itemp);
                    if (itemp != 0)
                    {
                        denom = DtDenoms.NewRow();
                        denom[0] = itemp.ToString();
                        DtDenoms.Rows.Add(denom);
                    }
                }

            }
            else if (Type == CassetteType.Hopper)
            {
                foreach (string strDenom in strDenoms.Split(':')[1].Split(','))
                {
                    decimal dtemp;
                    decimal.TryParse(strDenom, out dtemp);
                    if (dtemp != 0)
                    {
                        denom = DtDenoms.NewRow();
                        denom[0] = dtemp.ToString();
                        DtDenoms.Rows.Add(denom);
                    }
                }
            }
            else if (Type == CassetteType.Rejection)
            {
                denom = DtDenoms.NewRow();
                denom[0] = "1";
                DtDenoms.Rows.Add(denom);
            }
            cmbDemon.DataSource = DtDenoms;
            cmbDemon.ValueMember = "Value";
            cmbDemon.DisplayMember = "Value";

        }

        private void ClearControls()
        {
            try
            {
                txtCassetteName.Text = "";
                txtAlertLevel.Text = "";
                txtStandardFillAmount.Text = "";
                txtMaxFillAmount.Text = "";
                txtDescription.Text = "";
                cbIsActive.Checked = false;
                cmbDemon.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void EnableDisableControls(bool bStatus)
        {
            try
            {
                if (bStatus)
                {
                    txtAlertLevel.Enabled = bStatus && _bIsEditPermissionEnabled;
                    txtStandardFillAmount.Enabled = bStatus && _bIsEditPermissionEnabled;
                    txtMaxFillAmount.Enabled = bStatus && _bIsEditPermissionEnabled;
                    txtDescription.Enabled = bStatus && _bIsEditPermissionEnabled;

                    if (_SelectedVault.IsAssigned)
                    {
                        cbIsActive.Enabled = false;
                        cmbDemon.Enabled = false;
                        txtCassetteName.Enabled = false;
                        txtStandardFillAmount.Enabled = false;
                        btnAdd.Enabled = false;
                    }
                    else
                    {
                        cbIsActive.Enabled = bStatus && _bIsEditPermissionEnabled;
                        cmbDemon.Enabled = bStatus && _bIsEditPermissionEnabled;
                        txtCassetteName.Enabled = bStatus && _bIsEditPermissionEnabled;
                        txtStandardFillAmount.Enabled = bStatus && _bIsEditPermissionEnabled;
                        btnAdd.Enabled = true && _bIsEditPermissionEnabled;
                    }
                }
                else
                {
                    txtCassetteName.Enabled = bStatus;
                    txtAlertLevel.Enabled = bStatus;
                    cmbDemon.Enabled = bStatus;
                    txtStandardFillAmount.Enabled = bStatus;
                    txtMaxFillAmount.Enabled = bStatus;
                    txtDescription.Enabled = bStatus;
                    cbIsActive.Enabled = bStatus;
                }
                if (_Type == CassetteType.Rejection)
                {
                    txtStandardFillAmount.Enabled = false;
                    txtStandardFillAmount.Text = "0.00";
                }

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        #endregion


    }
}
