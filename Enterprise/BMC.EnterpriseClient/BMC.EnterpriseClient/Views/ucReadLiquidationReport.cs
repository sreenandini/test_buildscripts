using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BMC.EnterpriseBusiness.Business;
using BMC.Common.ExceptionManagement;
using BMC.CoreLib.Win32;
using BMC.Common;
using BMC.Reports;
using BMC.EnterpriseBusiness.Entities;

namespace BMC.EnterpriseClient.Views
{
    public partial class ucReadLiquidationReport : UserControl
    {
        #region Data Members

        private ReadBasedLiquidationBiz objReadBasedLiquidationBiz = new ReadBasedLiquidationBiz();
        string cmbDefaultText = string.Empty;
        #endregion //Data Members

        #region Properties

        public DataGridView ReadDataGrid
        {
            get
            {
                return dtgvReadData;
            }
        }

        public ComboBox SiteCombo
        {
            get
            {
                return cboSite;
            }
        }

        public CheckBox ChkOnlyLast20Records
        {
            get
            {
                return chkOnlyLast20;
            }
        }

        public int ReadDataListCount
        {
            get
            {
                if (dtgvReadData.Rows == null) return 0;
                return dtgvReadData.Rows.Count;
            }
        }

        public int SiteId
        {
            get
            {
                return Convert.ToInt32(cboSite.SelectedValue);
            }
        }

        #endregion //Properties

        #region Constructor

        public ucReadLiquidationReport()
        {
            InitializeComponent();

            // Set Tags for controls
            SetTagProperty();
        }

        private void SetTagProperty()
        {
            lblSite.Tag = "Key_SiteMandatory";
            chkOnlyLast20.Tag = "Key_Showonlylast20Records";
            cmbDefaultText = this.GetResourceTextByKey(1, "MSG_SITE_SELECT");
        }
        #endregion //Constructor

        #region Events

        private void ucReadBasedLiquidation_Load(object sender, EventArgs e)
        {
            // For externalization
            this.ResolveResources();

            try
            {
                chkOnlyLast20.Checked = true;
                LoadSites();
                FillData();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void cboSite_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillData();
        }

        private void chkOnlyLast20_CheckedChanged(object sender, EventArgs e)
        {
            if (cboSite.SelectedIndex > 0)
                FillData();
        }

        #endregion //Events

        #region Methods

        private void LoadSites()
        {
            try
            {
                cboSite.DisplayMember = "DisplayName";
                cboSite.ValueMember = "Site_ID";
                cboSite.DataSource = new BindingSource(objReadBasedLiquidationBiz.GetActiveSitesForReport(AppGlobals.Current.LoggedinUser.SecurityUserID, cmbDefaultText), null);
                cboSite.SelectedItem = 0;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        public void FillData()
        {
            try
            {
                dtgvReadData.DataSource = objReadBasedLiquidationBiz.GetReadLiquidationReportRecords(SiteId, chkOnlyLast20.Checked);
                FormatReadDataGrid();
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        public void ShowLiquidationReport()
        {
            try
            {
                if (ReadDataListCount <= 0)
                {
                    Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_NO_READRECORDS"), this.ParentForm.Text);        // "There are no Read records found."
                    return;
                }

                if (ReadDataGrid.SelectedRows.Count <= 0)
                {
                    Win32Extensions.ShowWarningMessageBox(this, this.GetResourceTextByKey(1, "MSG_SELECTROW_TOPRINT"), this.ParentForm.Text);       // ">Please select any record to Print"
                    return;
                }
                string SiteName = ((BMC.EnterpriseDataAccess.rsp_GetActiveSitesForUserResult)(cboSite.SelectedItem)).Site_Name;
                clsSPParams spParams = new clsSPParams();
                spParams.BatchId = 0;
                spParams.ReadId = Convert.ToInt32(ReadDataGrid.SelectedRows[0].Cells["ReadId"].Value);
                spParams.SiteName = SiteName;
                spParams.ReportFilterDateFormat = SettingsEntity.ReportDateTimeFormat;
                spParams.ReportDataDateAloneFormat = SettingsEntity.ReportDataDateAloneFormat;
                spParams.ReportDataDateNTimeFormat = SettingsEntity.ReportDataDateNTimeFormat;
                spParams.ReportPrintDateTimeFormat = SettingsEntity.ReportPrintDateTimeFormat;
                BMC.ReportViewer.RDLReportViewer.Instance.LoadReport("rsp_Report_GetLiquidationDetail", this.GetResourceTextByKey("Key_RT_ReportLiquidationSummary_ReadBased"), "ENT_LiquidationSummary_PS", spParams, false);

                //frmCrystalReportViewer frmCrystalReportViewer = new frmCrystalReportViewer();
                //frmCrystalReportViewer.ShowLiquidationSummaryReport(null, Convert.ToInt32(ReadDataGrid.SelectedRows[0].Cells["ReadId"].Value), SiteName);
                //frmCrystalReportViewer.ShowDialog(this);
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        public int GetReadDataListCount()
        {
            return dtgvReadData.Rows.Count;
        }

        public int GetSiteId()
        {
            return Convert.ToInt32(cboSite.SelectedValue);
        }

        private void FormatReadDataGrid()
        {
            try
            {
                dtgvReadData.Columns["ReadId"].Visible = false;
                dtgvReadData.Columns["Read_Date"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

                dtgvReadData.Columns["Read_Date"].Tag = "Key_ReadDate";               //"Read Date";
                dtgvReadData.Columns["LiquidationId"].Tag = "Key_LiquidationId";      //"Liquidation Id";
                dtgvReadData.Columns["SiteLiquidationId"].Tag = "Key_SiteLiquidationId";  //"Site Liquidation Id";
                dtgvReadData.Columns["LiquidationDate"].Tag = "Key_LiquidationDate";      //"Liquidation Date";
                dtgvReadData.Columns["Calendar_Period"].Tag = "Key_CalendarPeriod";       //"Calendar Period";
                dtgvReadData.Columns["Gross"].Tag = "Key_TotalMeterIn";               //"Total Meter In";
                dtgvReadData.Columns["TicketsExpected"].Tag = "Key_TotalMeterOut";    //"Total Meter Out";
                dtgvReadData.Columns["Net"].Tag = "Key_Net";  
                dtgvReadData.Columns["RetailerNegativeNet"].Tag = "Key_RetailerNegativeNet";   //"Retailer Negative Net";
                dtgvReadData.Columns["TicketPaid"].Tag = "Key_VoucherPaid";                    //"Ticket Paid";
                dtgvReadData.Columns["AdvanceToRetailer"].Tag = "Key_AdvanceToRetailer";      //"Advance To Retailer";
                dtgvReadData.Columns["Retailer"].Tag = "Key_Retailer";                        //"Retailer";
                dtgvReadData.Columns["BalanceDue"].Tag = "Key_BalanceDue";                    //"Balance Due";
                dtgvReadData.Columns["RetailerNetRevenue"].Tag = "Key_RetailerNetRevenue";    //"Retailer Net Revenue";

                // Resolve once the tags are set
                dtgvReadData.ResolveResources();
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
        #endregion //Methods

    }
}
