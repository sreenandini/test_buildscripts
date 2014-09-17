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
using BMC.Common.ExceptionManagement;
using System.Data.SqlClient;
using BMC.Common.LogManagement;
using BMC.Security;
using System.Globalization;
using BMC.CoreLib.Win32;
using BMC.Common;
using BMC.Reports;

namespace BMC.EnterpriseClient.Views
{
    public partial class frmOutstandingVaultDrops : Form
    {
        #region Declared Variables

        VaultDeclarationBusiness VaultDeclarationBiz;
        int _VaultID = 0;        
        string _SiteCode = string.Empty;
        private bool _bIsEditPermissionEnabled = false;
        #endregion

        #region Constructor

        public frmOutstandingVaultDrops()
        {
            try
            {
                LogManager.WriteLog("Inside frmOutstandingVaultDrops()", LogManager.enumLogLevel.Info);
                InitializeComponent();
                SetTagProperty();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        #endregion
        /// <summary>
        /// Assigns the Resource Key names to the controls--Created by kishore sivagnanam
        /// </summary>
        public void SetTagProperty()
        {
            this.btnClose.Tag = "Key_CloseCaption";
            this.btnNotesCounter.Tag = "Key_Declare";
            this.btnPrint.Tag = "Key_PrintCaption";
            this.btnRefresh.Tag = "Key_Refresh";
            this.clmAdjustments.Tag = "Key_Adjustments";
            this.clmBleed.Tag = "Key_Bleeds";
            this.clmBmcTotal.Tag = "Key_BMCTotal";
            this.clmDropDataTime.Tag = "Key_DropDateTime";
            this.clmDropID.Tag = "Key_DropID";
            this.clmFills.Tag = "Key_Fills";
            this.Tag = "Key_OutstandingVaultDrops";
            this.btnDetails.Tag = "Key_PrintDetails";
            this.lblRegion.Tag = "Key_RegionColon";
            this.clmSiteDropRef.Tag = "Key_SiteDropRef";
            this.lblSite.Tag = "Key_SiteColon";
            this.lblTypePrefix.Tag = "Key_TypePrefixColon";
            this.txtDeclarationLabel.Tag = "Key_UndeclaredVaultDrops";
            this.clmVaultTotal.Tag = "Key_VaultTotal";
            this.lblVault.Tag = "Key_VaultColon";
        }

        #region Event Methods

        private void frmOutstandingVaultDrops_Load(object sender, EventArgs e)
        {
            try
            {
                this.ResolveResources();
                tsslStatus.Text = "";
                _bIsEditPermissionEnabled = AppGlobals.Current.HasUserAccess("HQ_Admin_EditVaultDeclaration");
                LogManager.WriteLog("Inside frmOutstandingVaultDrops_Load()", LogManager.enumLogLevel.Info);
                this.SetCurrencyRegion();
                VaultDeclarationBiz = new VaultDeclarationBusiness();
                LoadRegionCombo();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void cmbRegion_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                LogManager.WriteLog("cmbRegion_SelectedIndexChanged", LogManager.enumLogLevel.Info);
                LoadSiteCombo();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void cbSite_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                LogManager.WriteLog("cbSite_SelectedIndexChanged", LogManager.enumLogLevel.Info);
                this.LoadVaultDetails();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                LogManager.WriteLog("Inside btnRefresh_Click()", LogManager.enumLogLevel.Info);
                LoadVaultDetails();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btnNotesCounter_Click(object sender, EventArgs e)
        {
            try
            {
                LogManager.WriteLog("Inside btnNotesCounter_Click()", LogManager.enumLogLevel.Info);
                if (cmbSite.SelectedValue != null)
                {
                    frmVaultDeclaration objVaultDeclaration = new frmVaultDeclaration(Convert.ToInt32(cmbSite.SelectedValue),cmbSite.Text);
                    objVaultDeclaration.ShowDialog();
                    LoadVaultDetails();
                }                    
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void gvOutstandingVaultDrops_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (btnNotesCounter.Enabled == true && _bIsEditPermissionEnabled)
                {
                    LogManager.WriteLog("Inside btnNotesCounter_Click()", LogManager.enumLogLevel.Info);
                    frmVaultDeclaration objVaultDeclaration = new frmVaultDeclaration(Convert.ToInt32(cmbSite.SelectedValue), cmbSite.Text);
                    objVaultDeclaration.ShowDialog();
                    LoadVaultDetails();
                }
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
                Print(cmbRegion.Text, Convert.ToInt32(cmbSite.SelectedValue), _SiteCode, cmbSite.Text, _VaultID);
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

        private void LoadRegionCombo()
        {
            try
            {
                LogManager.WriteLog("Inside LoadRegionCombo()", LogManager.enumLogLevel.Info);
                cmbRegion.DisplayMember = "Sub_Company_Region_Name";
                cmbRegion.ValueMember = "Sub_Company_Region_ID";
                List<Vault_RegionsForDrop> lstVaultDrops = VaultDeclarationBiz.GetRegionsForDrop();
                lstVaultDrops.Insert(0, new Vault_RegionsForDrop() { Sub_Company_Region_ID = 0, Sub_Company_Region_Name = this.GetResourceTextByKey("Key_AllCriteria") });
                cmbRegion.DataSource = lstVaultDrops;
                if (cmbRegion.Items.Count > 0) cmbRegion.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void LoadSiteCombo()
        {
            try
            {
                this.ClearAllControls();
                LogManager.WriteLog("Inside LoadSiteCombo()", LogManager.enumLogLevel.Info);
                cmbSite.DisplayMember = "Site_Name";
                cmbSite.ValueMember = "Site_ID";
                cmbSite.DataSource = VaultDeclarationBiz.Vault_GetSitesbasedonRegion(Convert.ToInt32(cmbRegion.SelectedValue),AppGlobals.Current.UserId);
                if (cmbSite.Items.Count > 0)
                {
                    cmbSite.SelectedIndex = 0;
                }
                else
                {
                    btnNotesCounter.Enabled = false;
                    btnPrint.Enabled = false;
                    btnDetails.Enabled = false;
                }
                LogManager.WriteLog("EXITING LoadSiteCombo()", LogManager.enumLogLevel.Info);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void ClearAllControls()
        {
            gvOutstandingVaultDrops.AutoGenerateColumns = false;
            gvOutstandingVaultDrops.DataSource = new List<Vault_UndeclaredDrops>();
            txtTypePrefix.Text = string.Empty;
            txtVaultName.Text = string.Empty;

        }

        private void LoadVaultDetails()
        {
            try
            {
                LogManager.WriteLog("Inside LoadVaultDetails()", LogManager.enumLogLevel.Info);
                if (cmbSite.SelectedItem != null)
                {
                    Vault_SitesForDrop objSite = ((Vault_SitesForDrop)cmbSite.SelectedItem);
                    _SiteCode = objSite.Site_Code;
                    tsslStatus.Text = "";
                    gvOutstandingVaultDrops.AutoGenerateColumns = false;
                    List<Vault_UndeclaredDrops> lstUndeclaredDrops = VaultDeclarationBiz.GetUndeclaredDrops(_VaultID, objSite.Site_ID);
                    gvOutstandingVaultDrops.DataSource = lstUndeclaredDrops;
                    if (lstUndeclaredDrops != null && lstUndeclaredDrops.Count > 0)
                    {
                        btnPrint.Enabled = true;
                        btnDetails.Enabled = true;
                        if (lstUndeclaredDrops[0].IsCentralDeclaration.ToUpper() == "TRUE")
                        {
                            if(_bIsEditPermissionEnabled)
                            {
                                btnNotesCounter.Enabled = true ;
                            }
                            else
                            {
                                btnNotesCounter.Enabled = false;
                                tsslStatus.Text = this.GetResourceTextByKey("MSG_VAULT_PERMISSION");
                                tsslStatus.ForeColor = Color.Black;
                            }
                        }
                        else
                        {
                            tsslStatus.Text = this.GetResourceTextByKey("MSG_VAULT_DECLARATION");
                            tsslStatus.ForeColor = Color.Red;
                            this.gvOutstandingVaultDrops.CellDoubleClick -= new System.Windows.Forms.DataGridViewCellEventHandler(this.gvOutstandingVaultDrops_CellDoubleClick);
                            btnNotesCounter.Enabled = false;
                        }
                    }
                    else
                    {
                        btnNotesCounter.Enabled = false; 
                        btnPrint.Enabled = false;
                        btnDetails.Enabled = false;
                        tsslStatus.Text = this.GetResourceTextByKey("MSG_VAULT_NORECORDS");
                        tsslStatus.ForeColor = Color.Black;
                    }
                    FormatCollection();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        #endregion

        #region Miscellenous Methods

        private void FormatCollection()
        {
            try
            {
                LogManager.WriteLog("Inside FormatCollection()", LogManager.enumLogLevel.Info);
                Font font = new Font("Verdana", 8, FontStyle.Regular);
                gvOutstandingVaultDrops.DefaultCellStyle.Font = font;

                //gvOutstandingVaultDrops.Columns["Site_ID"].Visible = false;
                //gvOutstandingVaultDrops.Columns["Declared_Balance"].Visible = false;
                //gvOutstandingVaultDrops.Columns["Declared"].Visible = false;
                //gvOutstandingVaultDrops.Columns["Freezed"].Visible = false;
                //gvOutstandingVaultDrops.Columns["CreateUser"].Visible = false;
                //gvOutstandingVaultDrops.Columns["ModifiedDate"].Visible = false;
                //gvOutstandingVaultDrops.Columns["ModifiedUser"].Visible = false;
                //gvOutstandingVaultDrops.Columns["FreezeUser"].Visible = false;
                //gvOutstandingVaultDrops.Columns["AuditDate"].Visible = false;
                //gvOutstandingVaultDrops.Columns["AuditUser"].Visible = false;
                //gvOutstandingVaultDrops.Columns["Site_Drop_Ref"].Visible = false;
                //gvOutstandingVaultDrops.Columns["UserName"].Visible = false;
                //gvOutstandingVaultDrops.Columns["BMCVariance"].Visible = false;
                //gvOutstandingVaultDrops.Columns["VaultVariance"].Visible = false;
                //gvOutstandingVaultDrops.Columns["FreezedDate"].Visible = false;
                //gvOutstandingVaultDrops.Columns["Manufacturer_Name"].Visible = false;
                //gvOutstandingVaultDrops.Columns["Vault_ID"].Visible = false;
                //gvOutstandingVaultDrops.Columns["OpeningBalance"].Visible = false;
                //gvOutstandingVaultDrops.Columns["NAME"].Visible = false;
                //gvOutstandingVaultDrops.Columns["clmOpeningBalance"].Visible = false;
                //gvOutstandingVaultDrops.Columns["Type_Prefix"].Visible = false;
                //gvOutstandingVaultDrops.Columns["CanFreeze"].Visible = false;
                //gvOutstandingVaultDrops.Columns["IsCentralDeclaration"].Visible = false;
                //gvOutstandingVaultDrops.Columns["AuditNote"].Visible = false;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void Print(string RegionName, int Site_id, string SiteCode, string SiteName, int VaultID)
        {
            LogManager.WriteLog("Inside Print()", LogManager.enumLogLevel.Info);
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                clsSPParams spParams = new clsSPParams();
                spParams.Vault_Id = VaultID;
                spParams.Site_ID = Site_id;
                spParams.SiteCode = SiteCode;
                spParams.SiteName = SiteName;
                spParams.RegionName = RegionName;
                spParams.ReportFilterDateFormat = SettingsEntity.ReportDateTimeFormat;
                spParams.ReportDataDateAloneFormat = SettingsEntity.ReportDataDateAloneFormat;
                spParams.ReportDataDateNTimeFormat = SettingsEntity.ReportDataDateNTimeFormat;
                spParams.ReportPrintDateTimeFormat = SettingsEntity.ReportPrintDateTimeFormat;
                BMC.ReportViewer.RDLReportViewer.Instance.LoadReport("rsp_Vault_GetUndeclaredDrops", this.txtDeclarationLabel.Text, "ENT_UndeclaredVaultDrops", spParams, false);                
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                Cursor.Current = Cursors.Default;
            }
        }

        private void PrintDropDetails(string RegionName, int Site_id, string SiteCode, string SiteName, int VaultID)
        {
            LogManager.WriteLog("Inside PrintDropDetails()", LogManager.enumLogLevel.Info);
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                clsSPParams spParams = new clsSPParams();
                spParams.VaultId = VaultID;
                spParams.Site_ID = Site_id;
                spParams.RegionName = RegionName;
                spParams.SiteCode = SiteCode;
                spParams.SiteName = SiteName;
                spParams.ReportFilterDateFormat = SettingsEntity.ReportDateTimeFormat;
                spParams.ReportDataDateAloneFormat = SettingsEntity.ReportDataDateAloneFormat;
                spParams.ReportDataDateNTimeFormat = SettingsEntity.ReportDataDateNTimeFormat;
                spParams.ReportPrintDateTimeFormat = SettingsEntity.ReportPrintDateTimeFormat;
                BMC.ReportViewer.RDLReportViewer.Instance.LoadReport("rsp_Report_GetUndeclaredVaultCassettes", this.GetResourceTextByKey("Key_Vault_UndeclaredVaultsCassettes"), "ENT_UndeclaredVaultCassettes", spParams, false);              
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                Cursor.Current = Cursors.Default;
            }
        }

        private void SetCurrencyRegion()
        {
            try
            {
                string strCurrency = new RegionInfo(BMC.Common.ConfigurationManagement.ConfigManager.Read("GetDefaultCultureForCurrency")).CurrencySymbol;
                //lblFillAmount.Text = string.Format("{0} ({1})", lblFillAmount.Text, strCurrency);

                this.clmFills.HeaderText = string.Format("{0} ({1})", clmFills.HeaderText, strCurrency);
                this.clmBleed.HeaderText = string.Format("{0} ({1})", clmBleed.HeaderText, strCurrency);
                this.clmBmcTotal.HeaderText = string.Format("{0} ({1})", clmBmcTotal.HeaderText, strCurrency);
                this.clmVaultTotal.HeaderText = string.Format("{0} ({1})", clmVaultTotal.HeaderText, strCurrency);
                this.clmAdjustments.HeaderText = string.Format("{0} ({1})", clmAdjustments.HeaderText, strCurrency);
                foreach (DataGridViewColumn cell in gvOutstandingVaultDrops.Columns)
                {
                    cell.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
                this.clmFills.CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                this.clmBleed.CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                this.clmBmcTotal.CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                this.clmVaultTotal.CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                this.clmAdjustments.CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleRight;

            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
        }

        #endregion

        private void gvOutstandingVaultDrops_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (gvOutstandingVaultDrops.SelectedRows.Count > 0)
                {
                    Vault_UndeclaredDrops objVault_UndeclaredDrops = (Vault_UndeclaredDrops)gvOutstandingVaultDrops.SelectedRows[0].DataBoundItem;
                    txtVaultName.Text = objVault_UndeclaredDrops.Name;
                    txtTypePrefix.Text = objVault_UndeclaredDrops.Type_Prefix;
                }
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
        }

        private void frmOutstandingVaultDrops_FormClosing(object sender, FormClosingEventArgs e)
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
                LogManager.WriteLog("Inside btnPrint_Click()", LogManager.enumLogLevel.Info);
                PrintDropDetails(cmbRegion.Text, Convert.ToInt32(cmbSite.SelectedValue), _SiteCode, cmbSite.Text, _VaultID);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void cmbSite_DropDown(object sender, EventArgs e)
        {
            try
            {
                ComboBox senderComboBox = (ComboBox)sender;
                int width = senderComboBox.DropDownWidth;
                Graphics g = senderComboBox.CreateGraphics();
                Font font = senderComboBox.Font;
                int vertScrollBarWidth =
                    (senderComboBox.Items.Count > senderComboBox.MaxDropDownItems)
                    ? SystemInformation.VerticalScrollBarWidth : 0;

                int newWidth;
                foreach (Vault_SitesForDrop s in senderComboBox.Items)
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
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void cmbRegion_DropDown(object sender, EventArgs e)
        {
            try
            {
                ComboBox senderComboBox = (ComboBox)sender;
                int width = senderComboBox.DropDownWidth;
                Graphics g = senderComboBox.CreateGraphics();
                Font font = senderComboBox.Font;
                int vertScrollBarWidth =
                    (senderComboBox.Items.Count > senderComboBox.MaxDropDownItems)
                    ? SystemInformation.VerticalScrollBarWidth : 0;

                int newWidth;
                foreach (Vault_RegionsForDrop s in senderComboBox.Items)
                {
                    newWidth = (int)g.MeasureString(s.Sub_Company_Region_Name, font).Width
                        + vertScrollBarWidth;
                    if (width < newWidth)
                    {
                        width = newWidth;
                    }
                }
                senderComboBox.DropDownWidth = width;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
    }
}
