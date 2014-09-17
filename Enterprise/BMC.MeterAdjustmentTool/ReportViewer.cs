using CrystalDecisions.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using BMC.Common;
using BMC.Reports;


namespace BMC.MeterAdjustmentTool
{
    public partial class ReportViewer : Form
    {
        public ReportViewer(string connectionString, string webUrl)
        {
            this.webUrl = webUrl;
            this.ConnectionString = connectionString;
            InitializeComponent();
            this.Text = Extensions.AppTitle + this.GetResourceTextByKey("Key_ReportViewerTitle");   // " - [Report Viewer]";
        }

        public string ConnectionString { get; set; }

        public string webUrl { get; set; }

        public void crystalReportViewer1_Load(object sender, EventArgs e)
        {
            try
            {
                DataSet dsAuditReport = new DataSet();
                
                DataAccess da = new DataAccess(this.ConnectionString);
                SqlConnectionStringBuilder _connectionStringBuilder = new SqlConnectionStringBuilder(this.ConnectionString);

                
                string[] sDateRange = Convert.ToString(this.Tag).Split(',');

                dsAuditReport = da.GetAuditReportDetails(Convert.ToDateTime(sDateRange[0]), Convert.ToDateTime(sDateRange[1]));
                
                //CrystalDecisions.CrystalReports.Engine.Database crDatabase;
                //CrystalDecisions.CrystalReports.Engine.Tables crTables;

                //TableLogOnInfo tableLogonInfo = new TableLogOnInfo();
                //ConnectionInfo connectionInfo = new ConnectionInfo();

                //connectionInfo.ServerName = _connectionStringBuilder.DataSource;
                //connectionInfo.UserID = _connectionStringBuilder.UserID;
                //connectionInfo.Password = _connectionStringBuilder.Password;
                //connectionInfo.DatabaseName = _connectionStringBuilder.InitialCatalog;


                clsSPParams spParams = new clsSPParams();
                spParams.SiteName = sDateRange[2].ToString();
                spParams.AuditStartDate=Convert.ToDateTime(sDateRange[0]);
                spParams.AuditEndDate = Convert.ToDateTime(sDateRange[1]);
                BMC.ReportViewer.RDLReportViewer.Instance.LoadLocalReport(Application.StartupPath+"\\BMC.ClientReports\\MAT_AuditReport.rdl", "dsMeterAdjustmentAudit", dsAuditReport, "Audit Trial Report", "MAT_AuditReport", spParams);

                //if (this.webUrl.Contains(MeterGlobals.ExchangeWebUrl114))
                //{
                //    AuditReport_114 crAuditReport = new AuditReport_114();
                //    crAuditReport.SetDataSource(dsAuditReport);
                //    crAuditReport.SetParameterValue("SiteName", sDateRange[2].ToString());
                //    crAuditReport.SetParameterValue("@AuditStartDate", Convert.ToDateTime(sDateRange[0]));
                //    crAuditReport.SetParameterValue("@AuditEndDate", Convert.ToDateTime(sDateRange[1]));
                //    crDatabase = crAuditReport.Database;

                //    crTables = crDatabase.Tables;

                //    foreach (CrystalDecisions.CrystalReports.Engine.Table crTable in crTables)
                //    {
                //        tableLogonInfo = crTable.LogOnInfo;
                //        tableLogonInfo.ConnectionInfo = connectionInfo;
                //        crTable.ApplyLogOnInfo(tableLogonInfo);
                //    }

                //    crystalReportViewer1.ReportSource = crAuditReport;
                //}
                //else
                //{
                //    AuditReport crAuditReport = new AuditReport();
                //    crAuditReport.SetDataSource(dsAuditReport);
                //    crAuditReport.SetParameterValue("SiteName", sDateRange[2].ToString());
                //    crAuditReport.SetParameterValue("@AuditStartDate", Convert.ToDateTime(sDateRange[0]));
                //    crAuditReport.SetParameterValue("@AuditEndDate", Convert.ToDateTime(sDateRange[1]));
                //    crDatabase = crAuditReport.Database;

                //    crTables = crDatabase.Tables;

                //    foreach (CrystalDecisions.CrystalReports.Engine.Table crTable in crTables)
                //    {
                //        tableLogonInfo = crTable.LogOnInfo;
                //        tableLogonInfo.ConnectionInfo = connectionInfo;
                //        crTable.ApplyLogOnInfo(tableLogonInfo);
                //    }

                //    crystalReportViewer1.ReportSource = crAuditReport;
                //}
            }
            catch (Exception ex)
            {
              //
            }
        }        
    }    
}
