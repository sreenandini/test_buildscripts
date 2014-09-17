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
using BMC.Security;
using System.Globalization;
using BMC.CoreLib.Win32;
using BMC.Common;
using BMC.Reports;

namespace BMC.EnterpriseClient.Views
{
    public partial class frmVaultDropHistory : Form
    {

        #region Declared Variables

        List<Vault_UndeclaredDrops> _lstUndeclaredDrops = null;
        string _SiteCode = "";
        int _RecordNo;
        int _VaultID = 0;
        int VarainceType = 0;
        string _VaultName = string.Empty;
        string _VaultType = string.Empty;
        VaultDeclarationBusiness VaultDeclarationBiz;
        bool _EnterKeyed = false;
        bool _bIsEditPermissionEnabled = false;
        #endregion

        #region Constructor

        public frmVaultDropHistory()
        {
            InitializeComponent();
            SetTagProperty();
        }

        #endregion

        #region Event Menthods

        private void frmVaultDropHistory_Load(object sender, EventArgs e)
        {
            try
            {
                this.ResolveResources();
                LogManager.WriteLog("Inside frmVaultDeclaration_Load and USER ID is -->" + AppGlobals.Current.UserId.ToString(), LogManager.enumLogLevel.Info);
                VaultDeclarationBiz = new VaultDeclarationBusiness();
                this.SetCurrencyRegion();
                LoadSiteCombo();
                cmbVarainceType.SelectedIndex = 0;
                _bIsEditPermissionEnabled = AppGlobals.Current.HasUserAccess("HQ_Admin_EditAuditVaultDeclaration");
                dtpStartDate.Value = DateTime.Now.Subtract(TimeSpan.FromDays(1));
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
        }

        private void cmbSite_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                this.LoadVaultDetails();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            try
            {
                dgvVaultDropHistoryDetails.DataSource = null;
                if (dtpStartDate.Value < System.DateTime.Now)
                {
                    if (dtpEndDate.Value < System.DateTime.Now)
                    {
                        if (dtpStartDate.Value <= dtpEndDate.Value)
                        {
                            PopulateDeclaredVaultDrops();
                        }
                        else
                        {
                            Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_START_DT_SHOULD_LESS_THAN_END_DT"), this.Text);
                        }
                    }
                    else
                    {
                        Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_END_DT_SHOULD_LESS_THAN_SYS_DT"), this.Text);
                    }
                }
                else
                {
                    Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_START_DT_SHOULD_LESS_THAN_SYS_DT"), this.Text);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void dtpDate_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter && !_EnterKeyed)
                {
                    dgvVaultDropHistoryDetails.DataSource = null;
                    if (dtpStartDate.Value < System.DateTime.Now)
                    {
                        if (dtpEndDate.Value < System.DateTime.Now)
                        {
                            if (dtpStartDate.Value < dtpEndDate.Value)
                            {
                                PopulateDeclaredVaultDrops();
                            }
                            else
                            {
                                _EnterKeyed = true;
                                Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_START_DT_SHOULD_LESS_THAN_END_DT"), this.Text);
                            }
                        }
                        else
                        {
                            _EnterKeyed = true;
                            Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_END_DT_SHOULD_LESS_THAN_SYS_DT"), this.Text);
                        }
                    }
                    else
                    {
                        _EnterKeyed = true;
                        Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_START_DT_SHOULD_LESS_THAN_SYS_DT"), this.Text);
                    }
                }
                else
                {
                    _EnterKeyed = false;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void dgvVaultDropHistoryDetails_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (_bIsEditPermissionEnabled)
                {
                    _RecordNo = dgvVaultDropHistoryDetails.CurrentCell.RowIndex;
                    frmAuditAdjustment objAuditAdjustment = new frmAuditAdjustment((Vault_UndeclaredDrops)dgvVaultDropHistoryDetails.CurrentRow.DataBoundItem, cmbSite.Text, txtVaultName.Text, txtTypePrefix.Text, _SiteCode, _RecordNo);
                    objAuditAdjustment.ShowDialog(this);
                    PopulateDeclaredVaultDrops();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btnAdjust_Click(object sender, EventArgs e)
        {
            try
            {
                _RecordNo = dgvVaultDropHistoryDetails.CurrentCell.RowIndex;
                frmAuditAdjustment objAuditAdjustment = new frmAuditAdjustment((Vault_UndeclaredDrops)dgvVaultDropHistoryDetails.CurrentRow.DataBoundItem, cmbSite.Text, txtVaultName.Text, txtTypePrefix.Text, _SiteCode, _RecordNo);
                objAuditAdjustment.ShowDialog(this);
                PopulateDeclaredVaultDrops();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                LogManager.WriteLog("Inside btnPrint_Click()", LogManager.enumLogLevel.Info);
                VarainceType = SelectVarainceType();
                Print(Convert.ToInt32(cmbSite.SelectedValue), _SiteCode, cmbSite.Text, _VaultID, VarainceType, dtpStartDate.Value, dtpEndDate.Value, cmbVarainceType.SelectedValue.ToString());
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        #region Load Methods

        private void LoadSiteCombo()
        {
            LogManager.WriteLog("Inside LoadSiteCombo()", LogManager.enumLogLevel.Info);
            lblStatus.Text = "";
            cmbSite.DisplayMember = "Site_Name";
            cmbSite.ValueMember = "Site_ID";
            cmbSite.DataSource = VaultDeclarationBiz.GetAllSitesDetails(AppGlobals.Current.UserId);
            if (cmbSite.Items.Count > 0) cmbSite.SelectedIndex = 0;
        }

        private void LoadVaultDetails()
        {
            ClearControls();
            LogManager.WriteLog("Inside LoadVaultDetails()", LogManager.enumLogLevel.Info);
            if (cmbSite.SelectedItem != null)
            {
                Vault_SitesForDrop objSite = ((Vault_SitesForDrop)cmbSite.SelectedItem);
                _SiteCode = objSite.Site_Code;
            }
        }

        private void PopulateDeclaredVaultDrops()
        {
            if (cmbSite.SelectedItem != null)
            {
                Vault_SitesForDrop objSite = ((Vault_SitesForDrop)cmbSite.SelectedItem);
                VarainceType = SelectVarainceType();
                dgvVaultDropHistoryDetails.AutoGenerateColumns = false;
                List<Vault_UndeclaredDrops> lstUndeclaredDrops = VaultDeclarationBiz.GetDeclaredDrops(_VaultID, objSite.Site_ID, dtpStartDate.Value, dtpEndDate.Value, VarainceType);
                dgvVaultDropHistoryDetails.DataSource = lstUndeclaredDrops;
                _lstUndeclaredDrops = lstUndeclaredDrops;
                if (lstUndeclaredDrops != null && lstUndeclaredDrops.Count > 0)
                {
                    lblStatus.Text = "";
                    btnPrint.Visible = true;
                    btnDetails.Visible = true;
                    btnAdjust.Visible = true && _bIsEditPermissionEnabled;
                }
                else
                {
                    lblStatus.Text = this.GetResourceTextByKey(1, "MSG_VAULD_DROPNOTFOUND");
                    lblStatus.ForeColor = Color.Red;
                    btnPrint.Visible = false;
                    btnDetails.Visible = false;
                    btnAdjust.Visible = false;
                }
                FormatCollection();
                FormatGridView();
            }
            else
            {
                lblStatus.Text = this.GetResourceTextByKey(1,"MSG_SL_SELECT_SITE");
                lblStatus.ForeColor = Color.Red;
            }
        }

        private void FormatGridView()
        {
            foreach (DataGridViewRow gvRows in dgvVaultDropHistoryDetails.Rows)
            {
                Vault_UndeclaredDrops obj = (Vault_UndeclaredDrops)gvRows.DataBoundItem;
                if (obj.Freezed.Value)
                {
                    gvRows.DefaultCellStyle.BackColor = Color.SkyBlue;
                }
            }
        }

        private int SelectVarainceType()
        {
            int iSelectedValue = 0;
            iSelectedValue = cmbVarainceType.SelectedIndex;
            if (iSelectedValue < 0)
                iSelectedValue = 0;
            return iSelectedValue;
        }

        private void ClearControls()
        {
            try
            {
                dgvVaultDropHistoryDetails.AutoGenerateColumns = false;
                dgvVaultDropHistoryDetails.DataSource = new List<Vault_UndeclaredDrops>();
                txtManufacturer.Text = txtTypePrefix.Text = txtVaultName.Text = string.Empty;
                cmbVarainceType.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                throw;
            }
        }

        #endregion

        #region Update Methods

        #endregion

        #region Miscellaneous Methods

        /// <summary>
        /// Assigns the Resource Key names to the controls--Created by kishore sivagnanam
        /// </summary>
        public void SetTagProperty()
        {
            this.btnAdjust.Tag = "Key_Adjust";
            this.btnClose.Tag = "Key_CloseCaption";
            this.btnGo.Tag = "Key_GoCaption";
            this.btnPrint.Tag = "Key_PrintCaption";
            this.clmActualTotal.Tag = "Key_ActualTotal";
            this.clmAdjustment.Tag = "Key_Adjustment";
            this.clmBmcTotal.Tag = "Key_BMCTotal";
            this.clmBmcVaraince.Tag = "Key_BMCVaraince";
            this.clmDeclarationDateTime.Tag = "Key_DeclarationDateTime";
            this.clmDropDateTime.Tag = "Key_DropDateTime";
            this.clmDropId.Tag = "Key_DropID";
            this.lblEnddate.Tag = "Key_EndDateTime";
            this.clmBleeds.Tag = "Key_Bleeds";
            this.lblManufacturer.Tag = "Key_ManufacturerColon";
            this.btnDetails.Tag = "Key_PrintDetails";
            this.clmFills.Tag = "Key_Fills";
            this.lblSite.Tag = "Key_SiteColon";
            this.lblStartDate.Tag = "Key_StartDateTimeColon";
            this.lblTypePrefix.Tag = "Key_TypePrefixColon";
            this.lblVaraince.Tag = "Key_VarianceColon";
            this.Tag = "Key_VaultDropHistory";
            this.clmVaultTotal.Tag = "Key_VaultTotal";
            this.clmVaultVaraince.Tag = "Key_VaultVariance";
            this.lblVault.Tag = "Key_VaultColon";
            this.clmSiteDropRef.Tag = "Key_SiteDropRef";
            List<ComboBoxItem> lstVarianceTypes = new List<ComboBoxItem>();
            lstVarianceTypes.Add(new ComboBoxItem() { Text = this.GetResourceTextByKey("Key_AllCriteria"), Value = "-- ALL --" });
            lstVarianceTypes.Add(new ComboBoxItem() { Text = this.GetResourceTextByKey("Key_WithVariance"), Value = "With Variance" });
            lstVarianceTypes.Add(new ComboBoxItem() { Text = this.GetResourceTextByKey("Key_ZeroVariance"), Value = "Zero Variance" });
            cmbVarainceType.DataSource = lstVarianceTypes;
            cmbVarainceType.DisplayMember = "Text";
            cmbVarainceType.ValueMember = "Value";
         }

        private void SetCurrencyRegion()
        {
            try
            {
                string strCurrency = new RegionInfo(BMC.Common.ConfigurationManagement.ConfigManager.Read("GetDefaultCultureForCurrency")).CurrencySymbol;
                //lblFillAmount.Text = string.Format("{0} ({1})", lblFillAmount.Text, strCurrency);

                this.clmFills.HeaderText = string.Format("{0} ({1})", clmFills.HeaderText, strCurrency);
                this.clmBleeds.HeaderText = string.Format("{0} ({1})", clmBleeds.HeaderText, strCurrency);
                this.clmAdjustment.HeaderText = string.Format("{0} ({1})", clmAdjustment.HeaderText, strCurrency);
                this.clmBmcTotal.HeaderText = string.Format("{0} ({1})", clmBmcTotal.HeaderText, strCurrency);
                this.clmVaultTotal.HeaderText = string.Format("{0} ({1})", clmVaultTotal.HeaderText, strCurrency);
                this.clmBmcVaraince.HeaderText = string.Format("{0} ({1})", clmBmcVaraince.HeaderText, strCurrency);
                this.clmVaultVaraince.HeaderText = string.Format("{0} ({1})", clmVaultVaraince.HeaderText, strCurrency);
                this.clmActualTotal.HeaderText = string.Format("{0} ({1})", clmActualTotal.HeaderText, strCurrency);
                //dgvVaultDropHistoryDetails.Columns["clmOpeningBalance"].Visible = false;



                foreach (DataGridViewColumn cell in dgvVaultDropHistoryDetails.Columns)
                {
                    cell.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
                this.clmFills.CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                this.clmBleeds.CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                this.clmAdjustment.CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                this.clmBmcTotal.CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                this.clmVaultTotal.CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                this.clmBmcVaraince.CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                this.clmVaultVaraince.CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                this.clmActualTotal.CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleRight;

            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
        }
        private void FormatCollection()
        {
            LogManager.WriteLog("Inside FormatCollection()", LogManager.enumLogLevel.Info);
            Font font = new Font("Verdana", 8, FontStyle.Regular);
            dgvVaultDropHistoryDetails.DefaultCellStyle.Font = font;

            //dgvVaultDropHistoryDetails.Columns["Fills"].Visible = false;
            //dgvVaultDropHistoryDetails.Columns["Site_ID"].Visible = false;
            //dgvVaultDropHistoryDetails.Columns["Declared"].Visible = false;
            //dgvVaultDropHistoryDetails.Columns["Freezed"].Visible = false;
            //dgvVaultDropHistoryDetails.Columns["CreateUser"].Visible = false;
            //dgvVaultDropHistoryDetails.Columns["ModifiedUser"].Visible = false;
            //dgvVaultDropHistoryDetails.Columns["FreezeUser"].Visible = false;
            //dgvVaultDropHistoryDetails.Columns["AuditDate"].Visible = false;
            //dgvVaultDropHistoryDetails.Columns["AuditUser"].Visible = false;
            //dgvVaultDropHistoryDetails.Columns["Site_Drop_Ref"].Visible = false;
            //dgvVaultDropHistoryDetails.Columns["UserName"].Visible = false;
            //dgvVaultDropHistoryDetails.Columns["FreezedDate"].Visible = false;
            //dgvVaultDropHistoryDetails.Columns["Manufacturer_Name"].Visible = false;
            //dgvVaultDropHistoryDetails.Columns["Vault_ID"].Visible = false;
            //dgvVaultDropHistoryDetails.Columns["NAME"].Visible = false;
            //dgvVaultDropHistoryDetails.Columns["Type_Prefix"].Visible = false;
            //dgvVaultDropHistoryDetails.Columns["AuditNote"].Visible = false;
            //dgvVaultDropHistoryDetails.Columns["OpeningBalance"].Visible = false;
            //dgvVaultDropHistoryDetails.Columns["clmBmcVaraince"].Visible = _bIsVaultAudit;
            //dgvVaultDropHistoryDetails.Columns["clmVaultVaraince"].Visible = _bIsVaultAudit;
            //dgvVaultDropHistoryDetails.Columns["CanFreeze"].Visible = false;
            //dgvVaultDropHistoryDetails.Columns["IsCentralDeclaration"].Visible = false;

        }

        private void Print(int Site_id, string SiteCode, string SiteName, int VaultID, int ShowVariance,
                            DateTime StartDate, DateTime EndDate, string Variance)
        {
            LogManager.WriteLog("Inside Print()", LogManager.enumLogLevel.Info);
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                clsSPParams spParams = new clsSPParams();
                spParams.VaultId = VaultID;
                spParams.Site_ID = Site_id;
                spParams.StartDate = StartDate.ToString();
                spParams.EndDate = EndDate.ToString();
                spParams.VarianceName = Variance;
                spParams.SiteCode = SiteCode;
                spParams.SiteName = SiteName;
                spParams.VarianceType = ShowVariance;
                spParams.ReportFilterDateFormat = SettingsEntity.ReportDateTimeFormat;
                spParams.ReportDataDateAloneFormat = SettingsEntity.ReportDataDateAloneFormat;
                spParams.ReportDataDateNTimeFormat = SettingsEntity.ReportDataDateNTimeFormat;
                spParams.ReportPrintDateTimeFormat = SettingsEntity.ReportPrintDateTimeFormat;

                BMC.ReportViewer.RDLReportViewer.Instance.LoadReport("rsp_Vault_GetDeclaredDrops", this.GetResourceTextByKey("Key_Vault_DeclaredVaultDropHistory"), "ENT_DeclaredVaultDropHistory", spParams, false);
               
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                Cursor.Current = Cursors.Default;
            }
        }
        private void PrintDetails(int Site_id, string SiteCode, string SiteName, int VaultID, int ShowVariance,
                            DateTime StartDate, DateTime EndDate, string Variance)
        {
            LogManager.WriteLog("Inside PrintDetails()", LogManager.enumLogLevel.Info);
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                clsSPParams spParams = new clsSPParams();
                spParams.VaultId = VaultID;
                spParams.Site_ID = Site_id;
                spParams.StartDate = StartDate.ToString();
                spParams.EndDate = EndDate.ToString();
                spParams.Variance = ShowVariance;
                spParams.SiteCode = SiteCode;
                spParams.SiteName = SiteName;
                spParams.VarianceType = ShowVariance;
                spParams.VarianceName = Variance;
                spParams.ReportFilterDateFormat = SettingsEntity.ReportDateTimeFormat;
                spParams.ReportDataDateAloneFormat = SettingsEntity.ReportDataDateAloneFormat;
                spParams.ReportDataDateNTimeFormat = SettingsEntity.ReportDataDateNTimeFormat;
                spParams.ReportPrintDateTimeFormat = SettingsEntity.ReportPrintDateTimeFormat;
                BMC.ReportViewer.RDLReportViewer.Instance.LoadReport("rsp_Report_GetDeclaredVaultCassettes", this.GetResourceTextByKey("Key_DeclaredVaultDropsCassettedetails"), "ENT_DeclaredVaultDropsCassetteDetails", spParams, false);              
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                Cursor.Current = Cursors.Default;
            }
        }


        #endregion

        private void dgvVaultDropHistoryDetails_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvVaultDropHistoryDetails.SelectedRows.Count > 0)
            {
                Vault_UndeclaredDrops objDeclaredVault = (Vault_UndeclaredDrops)dgvVaultDropHistoryDetails.SelectedRows[0].DataBoundItem;
                txtVaultName.Text = objDeclaredVault.Name;
                txtTypePrefix.Text = objDeclaredVault.Type_Prefix;
                txtManufacturer.Text = objDeclaredVault.Manufacturer_Name;
            }
        }

        private void frmVaultDropHistory_FormClosing(object sender, FormClosingEventArgs e)
        {
            //try
            //{
            //    if (this.ShowQuestionMessageBox(MessageResources.MSG_CONFIRM_CLOSE) == DialogResult.No)
            //    {
            //        e.Cancel = true; ;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    ExceptionManager.Publish(ex);
            //}
        }

        private void btnDetails_Click(object sender, EventArgs e)
        {
            try
            {
                LogManager.WriteLog("Inside btnPrintDetails_Click()", LogManager.enumLogLevel.Info);
                VarainceType = SelectVarainceType();
                PrintDetails(Convert.ToInt32(cmbSite.SelectedValue), _SiteCode, cmbSite.Text, _VaultID, VarainceType, dtpStartDate.Value, dtpEndDate.Value, cmbVarainceType.SelectedValue.ToString());
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void cmbSite_DropDown(object sender, EventArgs e)
        {
            ComboBox senderComboBox = (ComboBox)sender;
            int width = senderComboBox.DropDownWidth;
            Graphics g = senderComboBox.CreateGraphics();
            Font font = senderComboBox.Font;
            int vertScrollBarWidth =
                (senderComboBox.Items.Count > senderComboBox.MaxDropDownItems)
                ? SystemInformation.VerticalScrollBarWidth : 0;

            int newWidth;
            foreach (Vault_SitesForDrop s in ((ComboBox)sender).Items)
            {
                newWidth = (int)g.MeasureString(s.Site_Name, font).Width
                    + vertScrollBarWidth;
                if (width < newWidth)
                {
                    width = newWidth;
                }
            }
            senderComboBox.DropDownWidth = width;
        }
    }
}
