
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using System.Threading;
using Microsoft.Reporting.WinForms;
using BMC.Common.LogManagement;
using BMC.Common.ConfigurationManagement;
using System.Collections;
using BMC.EnterpriseReportsBusiness;
using BMC.EnterpriseReportsTransport;
using System.Reflection;
using System.Net;
using BMC.Reports;
using BMC.Common;
using System.IO;
using System.Data.SqlClient;

namespace BMC.ReportViewer
{
    public partial class ReportViewer : Form
    {
        #region Member Variables
        private string ReportName = string.Empty;
        private string ReportArgName = string.Empty;
        private string FormName = string.Empty;
        private clsSPParams SPParams = null;
        private clsResourceParams ResourceParams = null;
        private ReportsBusiness objBusiness = new ReportsBusiness();
        private bool IsRendering = false;
        private DataSet dsReportData = null;
        private bool LoadLocalReport = false;
        private string LocalReportPath = string.Empty;
        private string DataSourceName = string.Empty;
        #endregion  

        #region Constructor
        public ReportViewer()
        {
            InitializeComponent();
            setTagProperty();
            dsReportData = null;
        }

        private void setTagProperty()
        {
            this.Tag = "key_frmReportViewer";
           
        }

        public ReportViewer(string ReportName, string ReportArgName, clsSPParams spParams, clsResourceParams resourceParams)
        {
            InitializeComponent();
            this.LoadLocalReport = false;
            this.DataSourceName = string.Empty; ;
            this.ResolveResources();
            this.ReportName = ReportName;
            this.ReportArgName = ReportArgName;
            this.ResourceParams = resourceParams;
            this.SPParams = spParams;
            SPParams.CurrentDate= DateTime.Now.ToString("dd-MMM-yyyy HH:mm:ss");
            this.dsReportData = null;
        }

        public ReportViewer(string sReportPath, string sDataSourceName, DataSet ReportData, string sReportName, string ReportArgName, clsSPParams spParams, clsResourceParams resourceParams)
        {
            InitializeComponent();
            this.LocalReportPath = sReportPath;
            this.LoadLocalReport = true;
            this.DataSourceName = sDataSourceName;
            this.ResolveResources();
            this.ReportName = sReportName;
            this.ReportArgName = ReportArgName;
            this.ResourceParams = resourceParams;
            this.SPParams = spParams;
            SPParams.CurrentDate = DateTime.Now.ToString("dd-MMM-yyyy HH:mm:ss");
            this.dsReportData = ReportData;
        }

        #endregion Constructor

        private DataTable getData(DataSet ds)
        {
            SqlDataAdapter da = new SqlDataAdapter();
            da.Fill(ds);
            DataTable dt = ds.Tables["ReportTableData"];
            return dt;
        }

        #region Events
        private void frmReportViewer_Load(object sender, EventArgs e)
        {
            this.Text = ReportName;
            
            txtErrorMsg.Visible = false;
            try
            {
                ReportParameter[] parameters;
                string strURL = string.Empty;
                string strFolder = string.Empty;
                ReportParameterInfoCollection lstParameters = null;

                rdlViewer.ShowCredentialPrompts = false;

                if (LoadLocalReport)
                {
                    rdlViewer.ProcessingMode = ProcessingMode.Local;
                    rdlViewer.LocalReport.ReportPath = LocalReportPath;
                    rdlViewer.LocalReport.DisplayName = ReportName;
                }
                else
                {
                    rdlViewer.ProcessingMode = ProcessingMode.Remote;
                    strURL = objBusiness.GetReportPath();
                    strFolder = objBusiness.GetReportFolder();

                    if (strURL == string.Empty || strURL == null)
                    {
                        MessageBox.Show(this.GetResourceTextByKey(1, "MSG_REP_URL"), Constants.MessageBoxHeader, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                        return;
                    }
                    else
                        strURL = objBusiness.ReportServerURL(strURL);
                    rdlViewer.ServerReport.ReportServerUrl = new Uri(strURL);
                
                    //string modifiedReportPath = "/" + strFolder + "/" + ReportName;
                    string modifiedReportPath = "/" + strFolder + "/" + ReportArgName;
                    if (!Convert.ToBoolean(objBusiness.GetSetting("SGVI_Enabled")) && objBusiness.GetSetting("Client") == "SGVI")
                    {
                        /*if (ReportName.ToUpper() == "LIQUIDATION")
                            modifiedReportPath = "/" + strFolder + "/" + "NonSGVI-Liquidation";
                        else if (ReportName.ToUpper() == "WEEKLYLIQUID")
                            modifiedReportPath = "/" + strFolder + "/" + "NonSGVI-WRL";
                        else if (ReportName.ToUpper() == "MAJORPRIZESREPORT")
                            modifiedReportPath = "/" + strFolder + "/" + "NonSGVI-MajorPrizesReport";*/
                        if (ReportArgName.ToUpper() == "LIQUIDATION")
                            modifiedReportPath = "/" + strFolder + "/" + "NonSGVI-Liquidation";
                        else if (ReportArgName.ToUpper() == "WEEKLYLIQUID")
                            modifiedReportPath = "/" + strFolder + "/" + "NonSGVI-WRL";
                        else if (ReportArgName.ToUpper() == "MAJORPRIZESREPORT")
                            modifiedReportPath = "/" + strFolder + "/" + "NonSGVI-MajorPrizesReport";

                    }
                    rdlViewer.ServerReport.ReportPath = modifiedReportPath;
                    rdlViewer.ServerReport.DisplayName = ReportName;
                    rdlViewer.DocumentMapCollapsed = true;
                }

                if (LoadLocalReport)
                {
                    lstParameters = rdlViewer.LocalReport.GetParameters();
                }
                else
                {
                    lstParameters = rdlViewer.ServerReport.GetParameters();
                }

                parameters = new ReportParameter[lstParameters.Count];
                List<ReportParameter> myReportParams = new List<ReportParameter>();
                ReportParameter parames1 = null;
                foreach (ReportParameterInfo param in lstParameters)
                {
                        parames1 = null;
                        foreach (PropertyInfo info in SPParams.GetType().GetProperties())
                        {
                            if (info.Name.ToUpper() == param.Name.ToUpper())
                            {
                                parames1 = new ReportParameter(param.Name, (info.GetValue(SPParams, null) == null) ? string.Empty : info.GetValue(SPParams, null).ToString());
                                break;
                            }
                        }
                        if (parames1 == null)
                        {
                            foreach (PropertyInfo info in ResourceParams.GetType().GetProperties())
                            {
                                if (info.Name.ToUpper() == param.Name.ToUpper())
                                {
                                    parames1 = new ReportParameter(param.Name, (info.GetValue(ResourceParams, null) == null) ? string.Empty : info.GetValue(ResourceParams, null).ToString());
                                    break;
                                }
                            }
                        }
                        if (parames1 != null)
                        {
                            myReportParams.Add(parames1);
                        }
                }

                if (LoadLocalReport)
                {
                    rdlViewer.LocalReport.SetParameters(myReportParams.ToArray());
                    if (dsReportData != null)
                    {
                        ReportDataSource rds = new ReportDataSource(DataSourceName, dsReportData.Tables[0]);
                        rdlViewer.LocalReport.DataSources.Clear();
                        rdlViewer.LocalReport.DataSources.Add(rds);
                        rdlViewer.LocalReport.Refresh();
                    }
                }
                else
                {
                    rdlViewer.ServerReport.SetParameters(myReportParams.ToArray());
                }
                rdlViewer.SetDisplayMode(DisplayMode.PrintLayout);

                // CR-200959 Setting to open all reports in Page Width mode by default
                rdlViewer.ZoomMode = ZoomMode.PageWidth;
            }

            catch (Exception ex)
            {
                txtErrorMsg.Visible = true;
                txtErrorMsg.Width = this.Width - 100;
                txtErrorMsg.Text = "''Sorry, The report seems to have a problem." + "\r\n";
                txtErrorMsg.Text += "The issue has been logged and will be rectified shortly." + "\r\n";
                txtErrorMsg.Text += "Please contact your administrator if the problem persists''";
                LogManager.WriteLog(ex.Message, LogManager.enumLogLevel.Error);
            }

            this.rdlViewer.RefreshReport();
        }

        private void frmReportViewer_FormClosing(object sender, FormClosingEventArgs e)
        {

            try
            {
                if (IsRendering)
                    if ((MessageBox.Show(this.GetResourceTextByKey(1, "MSG_REP_PROC"), Constants.MessageBoxHeader, MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No))
                    {
                        e.Cancel = true;
                        return;
                    }
                    else
                        rdlViewer.CancelRendering(-1);

            }
            catch (Exception ex)
            {
                LogManager.WriteLog(DateTime.Now + " frmReportViewer_FormClosing:" + " " + ex.ToString(), LogManager.enumLogLevel.Error);
            }
        }


        void ReportViewer_FormClosed(object sender, System.Windows.Forms.FormClosedEventArgs e)
        {
            //if (Program.bExtApp)
            //    Application.Exit();
        }

        private void rvAccWinLoss_RenderingComplete(object sender, RenderingCompleteEventArgs e)
        {
            IsRendering = false;
        }

        private void rvAccWinLoss_RenderingBegin(object sender, CancelEventArgs e)
        {
            IsRendering = true;
        }

        #endregion Events

    }
}