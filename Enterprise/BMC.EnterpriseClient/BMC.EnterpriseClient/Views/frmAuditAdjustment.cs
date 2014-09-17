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
using BMC.Security;
using BMC.Common.ExceptionManagement;
using BMC.Common.LogManagement;
using System.Globalization;
using BMC.CoreLib.Win32;
using System.Configuration;
using BMC.Common.Utilities;
using System.Xml.Linq;
using BMC.Common;

namespace BMC.EnterpriseClient.Views
{
    public partial class frmAuditAdjustment : Form
    {

        #region Declared Variables

        int _iDropID;
        Vault_UndeclaredDrops VaultAdjustmentCopy = null;//For cloned data
        int iUserID;
        string strUserName;
        string _SiteCode = "";
        int _RecordNo;
        Vault_UndeclaredDrops objUpdateDeclaration = null;
        string _NegFormat = "#,##0.00;(#,##0.00)";
        bool _bIsEditPermissionEnabled = false;
        bool _IsFreezed = false;
        bool _IsClosingAfterSave = false;
        ucCassette grpCassette = new ucCassette(CassetteType.Cassette);
        ucCassette grpHopper = new ucCassette(CassetteType.Hopper);
        List<Vault_GetDeclaredDropsForAudit> _lstAuditDrops = null;
        private Dictionary<string, NumberTextBox> _dictCassettes = new Dictionary<string, NumberTextBox>();
        private bool _isVaultWebServiceEnabled = false;
        public bool AllowDecimal = true;
        public decimal? _Capacity = 99999999.99M;
        public decimal? _MaxAmount = 999999.99M;
        public string _strCurrency = "$";
        BMC.EnterpriseClient.Helpers.Datawatcher objDatawatcher = null;

        #endregion

        #region Constructor

        public frmAuditAdjustment(Vault_UndeclaredDrops objdeclaredDrops, string SiteName, string VaultName, string VaultType, string SiteCode, int RecordNo)
        {
            try
            {
                InitializeComponent();
                _strCurrency = new RegionInfo(BMC.Common.ConfigurationManagement.ConfigManager.Read("GetDefaultCultureForCurrency")).CurrencySymbol;
                VaultAdjustmentCopy = (Vault_UndeclaredDrops)objdeclaredDrops.Clone();
                objUpdateDeclaration = objdeclaredDrops;
                _iDropID = Convert.ToInt32(objdeclaredDrops.Drop_ID);
                _SiteCode = SiteCode;
                _RecordNo = RecordNo;
                _IsFreezed = objdeclaredDrops.Freezed.Value;
                txtSite.Text = SiteName;
                txtVault.Text = VaultName;
                txtType.Text = VaultType;
                _Capacity = objdeclaredDrops.Capacity;

                iUserID = AppEntryPoint.Current.UserId;
                strUserName = AppEntryPoint.Current.UserName;

                _isVaultWebServiceEnabled = objdeclaredDrops.IsWebServiceEnabled;

                FillDataInControls();
                SetPropertyTag();//Externalization changes

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
        //Externalization changes
        private void SetPropertyTag()
        {
            try
            {
                this.btnClose.Tag = "Key_CloseCaption";
                this.btnSave.Tag = "Key_SaveCaption";
                this.lblNotes.Tag = "Key_NotesMandatory";                
                this.lblBMCTotal.Tag = "Key_BMCTotalColon";
                this.lblBMCVaraince.Tag = "Key_BMCVarainceColon";
                this.gb_Cassettedetails.Tag = "Key_CassetteHopperDetails";
                this.lblDeclaredTotal.Tag = "Key_DeclaredTotalColon";
                this.chkFreeze.Tag = "Key_Freeze";
                this.lblSite.Tag = "Key_SiteColon";
                this.lblType.Tag = "Key_TypeColon";
                this.lblVaultTotal.Tag = "Key_VaultTotalColon";
                this.lblVaultVaraince.Tag = "Key_VaultVarianceColon";
                this.lblVault.Tag = "Key_VaultColon";
                this.Tag = "Key_AuditAdjustment";
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void FillDataInControls()
        {
            txtDeclaredTotal.Text = objUpdateDeclaration.Declared_Balance.ToString();
            txtBmcTotal.Text = objUpdateDeclaration.Meter_Balance.ToString();
            txtVaultValue.Text = objUpdateDeclaration.Vault_Balance.ToString();
            txtBmcVaraince.Text = objUpdateDeclaration.BMCVariance.Value.ToString(_NegFormat);
            txtVaultVaraince.Text = objUpdateDeclaration.VaultVariance.Value.ToString(_NegFormat);
            txtNotes.Text = objUpdateDeclaration.AuditNote.ToString();
            chkFreeze.Checked = objUpdateDeclaration.Freezed.Value;
        }

        public frmAuditAdjustment()
        {
            InitializeComponent();
        }

        #endregion

        #region Event Methods

        private void frmAuditAdjustment_Load(object sender, EventArgs e)
        {
            try
            {
                VaultDeclarationBusiness vaultDeclarationBiz = new VaultDeclarationBusiness();

                if (_isVaultWebServiceEnabled)
                {
                    List<Vault_GetDeclaredDropsForAudit> lstAuditDrops = vaultDeclarationBiz.GetDeclaredDropsForAudit(_iDropID);
                    //lstAuditDrops.CopyTo(
                    _lstAuditDrops = lstAuditDrops;
                    if (lstAuditDrops == null || lstAuditDrops.Count == 0)
                        txtDeclaredTotal.Enabled = true;

                }

                _bIsEditPermissionEnabled = AppGlobals.Current.HasUserAccess("HQ_Admin_EditAuditVaultDeclaration");

                string strCurrency = new RegionInfo(BMC.Common.ConfigurationManagement.ConfigManager.Read("GetDefaultCultureForCurrency")).CurrencySymbol;

                lblBMCTotal.FormateLabelTextWithCurency(strCurrency);
                lblVaultTotal.FormateLabelTextWithCurency(strCurrency);
                lblBMCVaraince.FormateLabelTextWithCurency(strCurrency);
                lblVaultVaraince.FormateLabelTextWithCurency(strCurrency);
                lblDeclaredTotal.FormateLabelTextWithCurency(strCurrency);
                EnableDisable(_bIsEditPermissionEnabled, _IsFreezed);
                ConstructDroppedCassettes(_lstAuditDrops, strCurrency);
                this.ResolveResources();//Externalization changes
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
            objDatawatcher = new Helpers.Datawatcher(this);
        }

        private void Quantity_Changed(object sender, EventArgs e)
        {
            try
            {
                txtDeclaredTotal.Text = (grpCassette.Amount + grpHopper.Amount).ToString();
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
                int result = 0;
                string CassetteDetailsXml = "";
                if (_dictCassettes.Keys.Count > 0)
                {
                    foreach (string key in _dictCassettes.Keys)
                    {
                        if (!_dictCassettes[key].IsValidDenom())
                        {
                            Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_AUDITADJUST_VALID_DENOM") + key, this.Text);
                            _dictCassettes[key].Focus();
                            return;
                        }

                        if (_dictCassettes[key].CapacityExceeded)
                        {
                            lblStatus.Text = string.Format(this.GetResourceTextByKey(1,"MSG_AUDITADJUST_VALID_AMOUNT"), _strCurrency, _dictCassettes[key].MaxVaule);
                            _dictCassettes[key].Focus();
                            return;
                        }
                    }
                    CassetteDetailsXml = ConstructDropDetailsXml();
                }
                else
                {
                    //if (txtDeclaredTotal.Text.Trim() == string.Empty)
                    //{
                    //    Win32Extensions.ShowInfoMessageBox(this, "Please enter valid amount.");
                    //    txtDeclaredTotal.Focus();
                    //    return;
                    //}

                    if (txtDeclaredTotal.CTL_DecimalValue > _Capacity)
                    {
                        Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_AUDITADJUST_DEC"), this.Text);
                        txtDeclaredTotal.Focus();
                        return;
                    }
                }

                if (txtNotes.Text.Trim() == string.Empty)
                {
                    Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_AUDITADJUST_NOTES"), this.Text);
                    txtNotes.Focus();
                    return;
                }

                VaultDeclarationBusiness vaultDeclarationBiz = new VaultDeclarationBusiness();

                if (chkFreeze.Checked == true && !objUpdateDeclaration.CanFreeze)
                {
                    if (Win32Extensions.ShowQuestionMessageBox(this, this.GetResourceTextByKey(1, "MSG_AUDITADJUST_QUE"), this.Text) == System.Windows.Forms.DialogResult.No)
                    {
                        return;
                    }
                }
                if (_dictCassettes.Keys.Count > 0)
                {
                    result = vaultDeclarationBiz.Vault_UpdateAuditData(_iDropID,
                                                                        txtBmcTotal.CTL_DecimalValue,
                                                                        txtVaultValue.CTL_DecimalValue,
                                                                        txtDeclaredTotal.CTL_DecimalValue,
                                                                        txtNotes.Text,
                                                                        chkFreeze.Checked,
                                                                        AppGlobals.Current.UserId,
                                                                        _SiteCode,
                                                                        objUpdateDeclaration.CanFreeze,
                                                                        CassetteDetailsXml);
                }
                else
                {
                    result = vaultDeclarationBiz.Vault_UpdateAuditData(_iDropID,
                                                                        txtBmcTotal.CTL_DecimalValue,
                                                                        txtVaultValue.CTL_DecimalValue,
                                                                        txtDeclaredTotal.CTL_DecimalValue,
                                                                        txtNotes.Text,
                                                                        chkFreeze.Checked,
                                                                        AppGlobals.Current.UserId,
                                                                        _SiteCode,
                                                                        objUpdateDeclaration.CanFreeze,
                                                                        null);
                }
                if (result == 0)
                {
                    decimal dVaultVar = 0;
                    decimal dBmcVar = 0;

                    if (txtVaultVaraince.Text.Contains("("))
                        decimal.TryParse(txtVaultVaraince.Text.Replace("(", "-").Replace(")", ""), out dVaultVar);
                    else
                        decimal.TryParse(txtVaultVaraince.Text, out dVaultVar);

                    if (txtBmcVaraince.Text.Contains("("))
                        decimal.TryParse(txtBmcVaraince.Text.Replace("(", "-").Replace(")", ""), out dBmcVar);
                    else
                        decimal.TryParse(txtBmcVaraince.Text, out dBmcVar);


                    objUpdateDeclaration.Meter_Balance = txtBmcTotal.CTL_DecimalValue;
                    objUpdateDeclaration.Vault_Balance = txtVaultValue.CTL_DecimalValue;
                    objUpdateDeclaration.Declared_Balance = txtDeclaredTotal.CTL_DecimalValue;
                    objUpdateDeclaration.BMCVariance = dBmcVar;
                    objUpdateDeclaration.VaultVariance = dVaultVar;
                    objUpdateDeclaration.AuditNote = txtNotes.Text;
                    objUpdateDeclaration.Freezed = chkFreeze.Checked;
                    AuditVaultAdjustment(VaultAdjustmentCopy, objUpdateDeclaration);
                    Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_CASSETTE_SUCCESS"), this.Text);
                    _IsClosingAfterSave = true;
                    this.Close();
                }
                else if (result == -2)
                {
                    Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_AUDITADJUST_FROZEN"), this.Text);
                    this.Close();
                }
                else
                {
                    Win32Extensions.ShowErrorMessageBox(this, this.GetResourceTextByKey(1, "MSG_CASSETTE_FAILED"), this.Text);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                Win32Extensions.ShowErrorMessageBox(this, this.GetResourceTextByKey(1, "MSG_CASSETTE_FAILED"), this.Text);
            }
        }

        private void txtBmcTotal_TextChanged(object sender, EventArgs e)
        {
            try
            {
                decimal dtxtActualTotal = 0;
                decimal dtxtBmcTotal = 0;
                decimal.TryParse(txtDeclaredTotal.Text, out dtxtActualTotal);
                decimal.TryParse(txtBmcTotal.Text, out dtxtBmcTotal);
                txtBmcVaraince.Text = (dtxtActualTotal - dtxtBmcTotal).ToString(_NegFormat);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void txtVaultValue_TextChanged(object sender, EventArgs e)
        {
            try
            {
                decimal dtxtActualTotal = 0;
                decimal dtxtVaultTotal = 0;
                decimal.TryParse(txtDeclaredTotal.Text, out dtxtActualTotal);
                decimal.TryParse(txtVaultValue.Text, out dtxtVaultTotal);
                txtVaultVaraince.Text = (dtxtActualTotal - dtxtVaultTotal).ToString(_NegFormat);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void txtActualTotal_TextChanged(object sender, EventArgs e)
        {
            try
            {
                decimal dtxtActualTotal = 0;
                decimal dtxtVaultTotal = 0;
                decimal dtxtBmcTotal = 0;

                decimal.TryParse(txtDeclaredTotal.Text, out dtxtActualTotal);
                decimal.TryParse(txtBmcTotal.Text, out dtxtBmcTotal);
                txtBmcVaraince.Text = (dtxtActualTotal - dtxtBmcTotal).ToString(_NegFormat);

                decimal.TryParse(txtDeclaredTotal.Text, out dtxtActualTotal);
                decimal.TryParse(txtVaultValue.Text, out dtxtVaultTotal);
                txtVaultVaraince.Text = (dtxtActualTotal - dtxtVaultTotal).ToString(_NegFormat);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void txtActualTotal_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                    CalcaulateTotal();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void txtAmount_TextChanged(object sender, EventArgs e)
        {
            Decimal val = 0;
            foreach (string key in _dictCassettes.Keys)
            {
                val += _dictCassettes[key].CTL_DecimalValue;
            }
            txtDeclaredTotal.Text = val.ToString();
        }

        private void frmAuditAdjustment_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (!_IsClosingAfterSave)
                {
                    if (this.ShowQuestionMessageBox(this.GetResourceTextByKey(1, "MSG_CONFIRM_CLOSE"), this.Text) == DialogResult.No)
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

        private void dgvDropDetails_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        #region Miscellenous Methods

        private void CalcaulateTotal()
        {
            LogManager.WriteLog("inside CalcaulateTotal()", LogManager.enumLogLevel.Info);
            txtBmcVaraince.Text = (txtDeclaredTotal.CTL_DecimalValue - txtBmcTotal.CTL_DecimalValue).ToString();
            txtVaultVaraince.Text = (txtDeclaredTotal.CTL_DecimalValue - txtVaultValue.CTL_DecimalValue).ToString();
        }

        private void EnableDisable(bool VaultAuditPermission, bool Status)
        {
            if (_bIsEditPermissionEnabled)
            {
                txtNotes.Enabled = true;
                if (!_isVaultWebServiceEnabled)
                    txtDeclaredTotal.Enabled = true;
                chkFreeze.Enabled = true;
                btnSave.Enabled = true;
                if (Status)
                {
                    txtNotes.Enabled = false;
                    txtDeclaredTotal.Enabled = false;
                    chkFreeze.Enabled = false;
                    btnSave.Enabled = false;
                    lblStatus.Text = this.GetResourceTextByKey(1, "Key_ENT_DROP_FROZEN");
                    lblStatus.ForeColor = Color.Red;
                }
            }
            else
            {
                txtDeclaredTotal.Enabled = false;
                txtNotes.Enabled = false;
                chkFreeze.Enabled = false;
                btnSave.Enabled = false;
            }
        }

        private string ConstructDropDetailsXml()
        {
            XElement xml_Cassette = new XElement("CasssetteDetails");

            foreach (string str in _dictCassettes.Keys)
            {
                NumberTextBox txt = _dictCassettes[str];
                xml_Cassette.Add(new XElement("Cassette",
                new XAttribute("Id", ((Vault_GetDeclaredDropsForAudit)txt.Tag).Cassette_ID),
                new XAttribute("Amount", txt.CTL_DecimalValue.ToString())));
            }
            return xml_Cassette.ToString();
        }

        private void ConstructDroppedCassettes(List<Vault_GetDeclaredDropsForAudit> lstCassettes, string strCurrency)
        {
            // 
            // tlp_Cassettes
            // 
            this.tlp_Cassettes.Dock = System.Windows.Forms.DockStyle.Fill;
            tlp_Cassettes.Padding = new Padding(0);
            tlp_Cassettes.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
            this.tlp_Cassettes.Name = "tlp_Cassettes";
            if (lstCassettes != null)
                this.tlp_Cassettes.RowCount = lstCassettes.Count + 2;
            if (_isVaultWebServiceEnabled)
            {
                for (int i = 0; i < lstCassettes.Count + 1; i++)
                {
                    this.tlp_Cassettes.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30));
                }
            }
            this.tlp_Cassettes.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30));


            //this.tlp_Cassettes.Size = new System.Drawing.Size(842, 108);

            Label label1 = new Label();
            Label label2 = new Label();
            Label label3 = new Label();
            Label label4 = new Label();

            this.tlp_Cassettes.Controls.Add(label1, 0, 0);
            this.tlp_Cassettes.Controls.Add(label2, 1, 0);
            this.tlp_Cassettes.Controls.Add(label3, 2, 0);
            this.tlp_Cassettes.Controls.Add(label4, 3, 0);

            label1.Location = new System.Drawing.Point(0, 0);
            label1.Name = "label1";
            label1.TabIndex = 0;
            label1.BackColor = Color.SteelBlue;
            label1.Text = "Cassette/Hopper Name";
            label1.Tag = "Key_CasseteHopperName";
            label1.ForeColor = Color.White;
            label1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            label1.Dock = DockStyle.Fill;
            label1.TextAlign = ContentAlignment.MiddleCenter;

            label2.Location = new System.Drawing.Point(0, 0);
            label2.Name = "lblDenom";
            label2.Dock = DockStyle.Fill;
            label2.TabIndex = 1;
            label2.Text = "Denom";
            label2.Tag = "Key_Denom";
            label2.BackColor = Color.SteelBlue;
            label2.ForeColor = Color.White;
            label2.TextAlign = ContentAlignment.MiddleCenter;
            label2.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            label2.FormateLabelTextWithCurency(strCurrency);

            label3.Location = new System.Drawing.Point(0, 0);
            label3.Name = "lblAmount";
            label3.Dock = DockStyle.Fill;
            label3.TabIndex = 2;
            label3.Text = "Amount";
            label3.Tag = "Key_Amount";
            label3.BackColor = Color.SteelBlue;
            label3.ForeColor = Color.White;
            label3.TextAlign = ContentAlignment.MiddleCenter;
            label3.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            label3.FormateLabelTextWithCurency(strCurrency);

            label4.Location = new System.Drawing.Point(0, 0);
            label4.Dock = DockStyle.Fill;
            label4.Name = "lblVaultAmount";
            label4.TabIndex = 3;
            label4.Text = "Vault Amount";
            label4.Tag = "Key_VaultAmount";
            label4.ForeColor = Color.White;
            label4.BackColor = Color.SteelBlue;
            label4.TextAlign = ContentAlignment.MiddleCenter;
            label4.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            label4.FormateLabelTextWithCurency(strCurrency);

            int it = 1;

            if (_isVaultWebServiceEnabled)
            {
                foreach (Vault_GetDeclaredDropsForAudit item in lstCassettes)
                {
                    _MaxAmount = 0.00M;
                    Label lblCassetteName = new Label();
                    lblCassetteName.Name = "lbl_" + item.Cassette_Name;
                    lblCassetteName.Dock = DockStyle.Fill;
                    lblCassetteName.TabIndex = 3;
                    lblCassetteName.Text = item.Cassette_Name;
                    lblCassetteName.TextAlign = ContentAlignment.MiddleLeft;
                    this.tlp_Cassettes.Controls.Add(lblCassetteName, 0, it);

                    Label lblCassetteDenom = new Label();
                    lblCassetteDenom.Name = "lbl_" + item.Cassette_Name + "_Denom";
                    lblCassetteDenom.Dock = DockStyle.Fill;
                    lblCassetteDenom.TabIndex = 3;
                    lblCassetteDenom.Text = item.Denom.Value.ToString();
                    lblCassetteDenom.TextAlign = ContentAlignment.MiddleRight;
                    this.tlp_Cassettes.Controls.Add(lblCassetteDenom, 1, it);

                    NumberTextBox txtAmount = new NumberTextBox();
                    txtAmount.Name = item.Cassette_Name;
                    txtAmount.Dock = DockStyle.Fill;
                    txtAmount.TabIndex = 3;
                    txtAmount.TextAlign = HorizontalAlignment.Right;
                    this.tlp_Cassettes.Controls.Add(txtAmount, 2, it);
                    _dictCassettes.Add(item.Cassette_Name, txtAmount);
                    txtAmount.TextChanged += txtAmount_TextChanged;

                    if (item.Type == (int)CassetteType.Hopper)
                    {
                        txtAmount.Tag = item;
                        txtAmount.AllowDecimal = true;
                        txtAmount.DecimalLength = 2;
                        txtAmount.MaxLength = 9;
                        txtAmount.Text = item.DeclaredBalance.Value.ToString();
                    }
                    else
                    {
                        txtAmount.Tag = item;
                        txtAmount.AllowDecimal = false;
                        txtAmount.MaxLength = 6;
                        txtAmount.Text = ((int)item.DeclaredBalance.Value).ToString();
                    }
                    txtAmount.MaxVaule = item.MaxFillAmount.Value;
                    txtAmount.Denom = Convert.ToDecimal(item.Denom.Value);

                    Label lblDeclared = new Label();
                    lblDeclared.AutoSize = true;
                    lblDeclared.Name = "lbl_" + item.Cassette_Name + "_vault";
                    lblDeclared.Dock = DockStyle.Fill;
                    lblDeclared.TabIndex = 3;
                    lblDeclared.Text = item.VaultBalance.Value.ToString();
                    lblDeclared.TextAlign = ContentAlignment.MiddleRight;
                    this.tlp_Cassettes.Controls.Add(lblDeclared, 3, it);
                    it++;
                }
            }

            Label label21 = new Label();
            label21.Name = "label21";
            label21.TabIndex = 0;
            label21.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            label21.Dock = DockStyle.Fill;
            this.tlp_Cassettes.SetColumnSpan(label21, 4);
            this.tlp_Cassettes.Controls.Add(label21, 0, it);

        }

        #endregion

        #region Auditing Vault Adjustment

        private void AuditVaultAdjustment(Vault_UndeclaredDrops objOldData, Vault_UndeclaredDrops objNewData)
        {
            if ((objOldData == null) || (objNewData == null)) return;
            VaultDeclarationBusiness objVaultDeclarationbiz = new VaultDeclarationBusiness();

            if (objOldData.Declared_Balance != objNewData.Declared_Balance)
            {
                if (_isVaultWebServiceEnabled)
                {
                    foreach (string str in _dictCassettes.Keys)
                    {
                        NumberTextBox txt = _dictCassettes[str];
                        if (((Vault_GetDeclaredDropsForAudit)txt.Tag).DeclaredBalance.Value != txt.CTL_DecimalValue)
                        {
                            objVaultDeclarationbiz.AuditModifiedDataForVaultAdjustment("DeclaredBalance", "Vault Audit Adjustment", "Vault_Audit : Vault--> [Vault_Id :" + objNewData.Vault_ID.ToString() + "],[Name : " + objNewData.Name.ToString() + "], Cassette--> [Cassette_Id : " + ((Vault_GetDeclaredDropsForAudit)txt.Tag).Cassette_ID.ToString() + "] , [Cassette_Name : " + ((Vault_GetDeclaredDropsForAudit)txt.Tag).Cassette_Name.ToString(), "[Amount :" + ((Vault_GetDeclaredDropsForAudit)txt.Tag).DeclaredBalance.ToString(), " -->" + txt.Text, iUserID, strUserName);
                        }
                    }
                }
                else
                {
                    objVaultDeclarationbiz.AuditModifiedDataForVaultAdjustment(objOldData.Name, "Vault Audit Adjustment", "Declared Balance For Drop ID--> " + objOldData.Drop_ID.ToString(), objOldData.Meter_Balance.ToString(), objNewData.Declared_Balance.ToString(), iUserID, strUserName);
                }
            }
            if (objOldData.AuditNote != objNewData.AuditNote)
            {
                objVaultDeclarationbiz.AuditModifiedDataForVaultAdjustment(objOldData.Name, "Vault Audit Adjustment", "Audit Note For Drop ID--> " + objOldData.Drop_ID.ToString(), objOldData.AuditNote, objNewData.AuditNote, iUserID, strUserName);
            }
            if (objOldData.Freezed != objNewData.Freezed)
            {
                objVaultDeclarationbiz.AuditModifiedDataForVaultAdjustment(objOldData.Name, "Vault Audit Adjustment", "Freezed For Drop ID--> " + objOldData.Drop_ID.ToString(), objOldData.Freezed.ToString(), objNewData.Freezed.ToString(), iUserID, strUserName);
            }
        }

        #endregion

    }

    public static class LabelHelper
    {
        public static void FormateLabelTextWithCurency(this Label label, string RegionCurrencySymbol)
        {
            label.Text = string.Format("{0} ({1}) :", label.Text, RegionCurrencySymbol);
        }
    }
}
