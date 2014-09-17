using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using BMC.Common.LogManagement;
using BMC.EnterpriseReportsTransport;
using BMC.Common.ExceptionManagement;
using BMC.Common;
using BMC.Reports;
using BMC.ReportViewer;

namespace BMC.EnterpriseReportsUI
{
    public partial class ExceptionReport : Form
    {
        #region Member variables       
            private string _strReportArg = "";                   
            public string _FormName="";
            clsSPParams ReportParams = null;
            private string _ParentReportName = string.Empty;
            private string _ParentReportArg = string.Empty;
        #endregion Member variables

        #region Constructor

        public ExceptionReport()
        {
            InitializeComponent();
            setTagProperty();
        }

        private void setTagProperty()
        {
            this.Tag = "Key_Exception_Report_Name";
            this.btnCancel.Tag = "Key_Cancel";
            this.lblMessage5.Tag = "Key_Cancelreport";
            this.lblMessage2.Tag = "Key_Pleaseselectfromthefollowingoptions";
            this.lblMessage1.Tag = "Key_MissingDailyDataMsg";
            this.btnOpen.Tag = "Key_ViewExceptions";
            this.lblMessage3.Tag = "Key_ViewExceptionsReport";
            this.btnContinue.Tag = "Key_ViewReport";
            this.lblMessage4.Tag = "Key_ViewReportDisplay";

        }
       
        public ExceptionReport(string ReportName, clsSPParams lstParams,string ParentReportName,string ParenReportArg)
        {
            InitializeComponent();
            _strReportArg = ReportName;
            ReportParams = lstParams;
            _ParentReportName = ParentReportName;
            _ParentReportArg = ParenReportArg;
        }
        #endregion Constructor

        #region Events

        private void btnOpen_Click(object sender, EventArgs e)
        {
            try
            {
                //RDLReportViewer.Instance.LoadReport(_strReportArg, ReportParams);
                //ReportViewer viewer = new ReportViewer(_strReportArg, ReportParams);
                //viewer.Text = this.Text;
                //viewer.Show();
            }
            catch(Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void frmExceptionReport_Load(object sender, EventArgs e)
        {
            try
            {
                this.ResolveResources();
                this.Cursor = Cursors.WaitCursor;              
                    pnlMessages.Visible = true;
                    if (_FormName.ToUpper().Contains("REPORT"))
                    {
                        int index = _FormName.ToUpper().IndexOf("REPORT");
                        _FormName = _FormName.Remove(index);
                        _FormName = _FormName.Insert(index, "Exception Report"); ;
                    }
                    this.Text = _FormName;
             
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
         
        }

        private void btnContinue_Click(object sender, EventArgs e)
        {
            RDLReportViewer.Instance.LoadReport(_ParentReportArg, ReportParams);
            //ReportViewer viewer = null;
            //try
            //{
            //if (this.Text.ToUpper().Contains("EXCEPTION"))
            //{
            //    int i = this.Text.ToUpper().IndexOf("EXCEPTION");
            //    viewer = new ReportViewer(_ParentReportArg, ReportParams);
            //    viewer.Text = _ParentReportName;               
            //    viewer.Show();  
            //}
            //}
            //catch (Exception ex)
            //{
            //    ExceptionManager.Publish(ex);
            //}
            //finally
            //{
            //    this.Cursor = Cursors.Default;
            //}
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();               
            }
            catch(Exception ex)
            {
                LogManager.WriteLog(DateTime.Now + " btnCancel_Click" + " " + ex.ToString(), LogManager.enumLogLevel.Error);
            }
        }
       
        #endregion Events                     
    }
}