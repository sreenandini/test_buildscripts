
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
using BMC.Common;
using BMC.Reports;

namespace BMC.EnterpriseReportsUI
{
    public partial class ReportViewer : Form
    {
        #region Member Variables
        string sReportName, sFormName = string.Empty;
        List<clsSPParams> ReportParams = null;
        ReportsBusiness objBusiness = new ReportsBusiness();
        bool IsRendering = false;
        #endregion

        #region Constructor
        public ReportViewer()
        {
            InitializeComponent();
        }

        public ReportViewer(string ReportName, List<clsSPParams> lstparams)
        {
            InitializeComponent();
            this.sReportName = ReportName;
            this.ReportParams = lstparams;
        }


        #endregion Constructor

        #region Events
        private void frmReportViewer_Load(object sender, EventArgs e)
        {
            txtErrorMsg.Visible = false;

            try
            {

                //this.Text = sReportName;
                string strReport = "";
                ReportParameter[] parameters;

                string strURL = objBusiness.GetReportPath();
                string strFolder = objBusiness.GetReportFolder();
                strReport = sReportName;

                if (strURL == string.Empty || strURL == null)
                {
                    MessageBox.Show("Please verify the Reportserver URL and open the report again", Constants.MessageBoxHeader, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                    return;
                }
                else
                    strURL = objBusiness.ReportServerURL(strURL);

                rvAccWinLoss.ShowCredentialPrompts = false;
                rvAccWinLoss.ProcessingMode = ProcessingMode.Remote;
                rvAccWinLoss.ServerReport.ReportServerUrl = new Uri(strURL);

                if (!Convert.ToBoolean(objBusiness.GetSetting("SGVI_Enabled")) && objBusiness.GetSetting("Client") == "SGVI")
                {
                    if (sReportName.ToUpper() == "LIQUIDATION")
                        rvAccWinLoss.ServerReport.ReportPath = "/" + strFolder + "/" + "NonSGVI-Liquidation";
                    else if (sReportName.ToUpper() == "WEEKLYLIQUID")
                        rvAccWinLoss.ServerReport.ReportPath = "/" + strFolder + "/" + "NonSGVI-WRL";
                    else if (sReportName.ToUpper() == "MAJORPRIZESREPORT")
                        rvAccWinLoss.ServerReport.ReportPath = "/" + strFolder + "/" + "NonSGVI-MajorPrizesReport";
                }
                else
                    rvAccWinLoss.ServerReport.ReportPath = "/" + strFolder + "/" + strReport;

                rvAccWinLoss.DocumentMapCollapsed = true;


                ReportParameterInfoCollection lstParameters = rvAccWinLoss.ServerReport.GetParameters();

                parameters = new ReportParameter[lstParameters.Count];
                List<ReportParameter> myReportParams = new List<ReportParameter>();
                ReportParameter parames1 = null;
                foreach (ReportParameterInfo param in lstParameters)
                {
                    foreach (clsSPParams parems in ReportParams)
                    {
                        foreach (PropertyInfo info in parems.GetType().GetProperties())
                        {
                            if (info.Name.ToUpper() == param.Name.ToUpper())
                            {
                                parames1 = new ReportParameter(param.Name, (info.GetValue(parems, null) == null) ? string.Empty : info.GetValue(parems, null).ToString());
                                break;
                            }
                            
                        }
                        myReportParams.Add(parames1);
                    }
                }


              

                rvAccWinLoss.ServerReport.SetParameters(myReportParams.ToArray());
                rvAccWinLoss.SetDisplayMode(DisplayMode.PrintLayout);
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
            this.rvAccWinLoss.RefreshReport();
        }

        private void frmReportViewer_FormClosing(object sender, FormClosingEventArgs e)
        {

            try
            {
                if (IsRendering)
                    if ((MessageBox.Show("Report still processing,Do you want to close before?", Constants.MessageBoxHeader, MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No))
                    {
                        e.Cancel = true;
                        return;
                    }
                    else
                        rvAccWinLoss.CancelRendering(-1);




            }
            catch (Exception ex)
            {
                LogManager.WriteLog(DateTime.Now + " frmReportViewer_FormClosing:" + " " + ex.ToString(), LogManager.enumLogLevel.Error);
            }
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