using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BMC.MeterAdjustmentTool.Helpers;
using System.Data.Linq;
using BMC.Common.ExceptionManagement;
using BMC.Common;
using System.Data.SqlClient;
using BMC.Reports;
using Audit.BusinessClasses;
using Audit.Transport;

namespace BMC.MeterAdjustmentTool
{
    public partial class UxFilterCriteria : UserControl
    {
        //private ProcessEventArgs _processArgs = null;
        private int? _installationID = null;
        private ComboBoxItem<rsp_GetAllInstallationsWithStatusResult> _selItem = null;
        private const string MSG_DATE_CHECK = "End Date should be greater than or equal to Start Date";

        public UxFilterCriteria()
        {
            InitializeComponent();
            //_processArgs = new ProcessEventArgs();

            // Set Tags for controls
            SetTagProperty();
        }

        private void SetTagProperty()
        {
            this.lblEndDate.Tag = "Key_EndDateColon";
            this.uxHeader.Tag = "Key_FilterCriteria";
            this.lblInstallations.Tag = "Key_InstallationsColon";
            this.btnProcess.Tag = "Key_Process";
            this.lblStartDate.Tag = "Key_StartDateColon";
            this.btnViewReport.Tag = "Key_ViewAuditReport";
        }

        public bool ViewReportEnabled
        {
            set
            {
                btnViewReport.Enabled = value;
            }
        }

        private void btnProcess_Click(object sender, EventArgs e)
        {
            if (dtpEndDate.Value.GetDayStart() < dtpStartDate.Value.GetDayStart())
            {
                this.ParentForm.ShowWarningMessageBox(this.GetResourceTextByKey(1, "MSG_MAT_VALIDENDDATE"));
                dtpEndDate.Focus();
                return;
            }

            ProcessEventArgs processArgs = new ProcessEventArgs();
            processArgs.StartDate = dtpStartDate.Value;
            processArgs.EndDate = dtpEndDate.Value;
            processArgs.InstallationNo = _installationID;
            this.OnProcessClicked(processArgs);
        }

        private void UxFilterCriteria_Load(object sender, EventArgs e)
        {
            //btnViewReport.Enabled = false;

            // For externalization
            this.ResolveResources();
        }

        public void HideInstallation()
        {
            lblInstallations.Visible = false;
            cboInstallations.Visible = false;
            btnInstallationSearch.Visible = false;
            tblContent.ColumnStyles[4].Width = 0;
            tblContent.ColumnStyles[5].Width = 0;
            tblContent.ColumnStyles[6].Width = 0;
        }

        public void ShowInstallation()
        {
            lblInstallations.Visible = true;
            cboInstallations.Visible = true;
            btnInstallationSearch.Visible = true;
            tblContent.ColumnStyles[4].Width = 100;
            tblContent.ColumnStyles[5].Width = 180;
            tblContent.ColumnStyles[6].Width = 60;
        }

        public event ProcessClickedEventHandler ProcessClicked = null;

        private void OnProcessClicked(ProcessEventArgs e)
        {
            if (this.ProcessClicked != null)
            {
                this.ProcessClicked(this, e);
            }
        }

        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public DateTime StartDate
        {
            get
            {
                return dtpStartDate.Value;
            }
            set
            {
                dtpStartDate.Value = value;
            }
        }

        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public DateTime EndDate
        {
            get
            {
                return dtpEndDate.Value;
            }
            set
            {
                dtpEndDate.Value = value;
            }
        }

        public IMainForm OwnerForm { get; set; }

        public void ClearInstallations()
        {
            cboInstallations.Items.Clear();
        }

        public void FillInstallations()
        {
            try
            {
                cboInstallations.Items.Clear();
                cboInstallations.DisplayMember = "Text";
                cboInstallations.ValueMember = "Value";

                ComboBoxItem<rsp_GetAllInstallationsWithStatusResult> installationAll = new ComboBoxItem<rsp_GetAllInstallationsWithStatusResult>(
                                null, this.GetResourceTextByKey("Key_AllCriteria"));
                cboInstallations.Items.Add(installationAll);
                using (ExchangeDataContext db = new ExchangeDataContext(this.OwnerForm.ConnectionString))
                {
                    ISingleResult<rsp_GetAllInstallationsWithStatusResult> installations = db.FuncGetAllInstallationsWithStatus();

                    if (installations != null)
                    {
                        foreach (rsp_GetAllInstallationsWithStatusResult installation in installations)
                        {
                            string text = string.Format("{0:D} [ {1} / {2} ]{3}",
                                installation.Installation_No,
                                installation.Stock_No,
                                installation.Bar_Pos_Name,
                                (installation.InstallationStatus == "Active" ? "" : " (*)"));
                            ComboBoxItem<rsp_GetAllInstallationsWithStatusResult> installation2 = new ComboBoxItem<rsp_GetAllInstallationsWithStatusResult>(
                                installation, text);
                            cboInstallations.Items.Add(installation2);
                        }
                    }
                }

                cboInstallations.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            finally
            {

            }
        }

        private void cboInstallations_SelectedIndexChanged(object sender, EventArgs e)
        {
            _selItem = null;
            if (cboInstallations.SelectedIndex == 0)
            {
                _installationID = null;
            }
            else
            {
                _selItem = cboInstallations.SelectedItem as ComboBoxItem<rsp_GetAllInstallationsWithStatusResult>;
                if (_selItem != null)
                {
                    _installationID = _selItem.Value.Installation_No;
                }
            }
        }

        public rsp_GetSiteDetailsResult SelectedSite { get; set; }

        public string ConnectionString { get; set; }

        private void btnInstallationSearch_Click(object sender, EventArgs e)
        {
            new InstallationFilter(this.OwnerForm.ConnectionString,
                (_selItem != null ? _selItem.Value : null)).ShowDialogExResultAndDestroy<InstallationFilter>
                (this, null,
                (f) =>
                {
                    if (f.IsFormClosedOK())
                    {
                        cboInstallations.SelectComboBoxItem<rsp_GetAllInstallationsWithStatusResult>(
                            (i) =>
                            {
                                if (i != null)
                                    return i.Installation_No == f.SelectedInstallationNo;
                                else
                                    return false;
                            });
                    }
                });

        }

        private void btnViewReport_Click(object sender, EventArgs e)
        {
            try
            {
                if (dtpEndDate.Value.GetDayStart() < dtpStartDate.Value.GetDayStart())
                {
                    this.ParentForm.ShowWarningMessageBox(this.GetResourceTextByKey(1, "MSG_MAT_VALIDENDDATE"));
                }
                else
                {
                    rsp_GetInitialSettingsResult settings = new AuditViewerBusiness(Common.Utilities.DatabaseHelper.GetConnectionString()).GetSettings();
                    string stagvalue = dtpStartDate.Value.Date.ToString() + "," + dtpEndDate.Value.Date.ToString() + "," + this.SelectedSite.Site_Name;
                    DataSet dsAuditReport = new DataSet();
                    DataAccess da = new DataAccess(this.ConnectionString);
                    string[] sDateRange = Convert.ToString(stagvalue).Split(',');
                    dsAuditReport = da.GetAuditReportDetails(dtpStartDate.Value, dtpEndDate.Value);
                    clsSPParams spParams = new clsSPParams();
                    spParams.SiteName = this.SelectedSite.Site_Name;
                    spParams.AuditStartDate = Convert.ToDateTime(sDateRange[0]);
                    spParams.AuditEndDate = Convert.ToDateTime(sDateRange[1]);
                    spParams.ReportFilterDateFormat = settings.ReportDateTimeFormat;
                    spParams.ReportDataDateAloneFormat = settings.ReportDataDateAloneFormat;
                    spParams.ReportDataDateNTimeFormat = settings.ReportDataDateNTimeFormat;
                    spParams.ReportPrintDateTimeFormat = settings.ReportPrintDateTimeFormat;
                    BMC.ReportViewer.RDLReportViewer.Instance.LoadLocalReport(Application.StartupPath+ "\\BMC.ClientReports\\MAT_AuditReport.rdl", "dsMeterAdjustmentAudit", dsAuditReport, "Audit Trial Report", "MAT_AuditReport", spParams);
                }

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
    }
}
