using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Xml.Linq;
using System.Windows.Forms;
using BMC.Common.ExceptionManagement;
using BMC.Common.LogManagement;
using BMC.Common;
using System.Reflection;
using BMC.CoreLib.Win32;

namespace CustomReports
{
    public partial class MainPage : Form
    {
        private Dictionary<string, string> _dicReportColumns = new Dictionary<string, string>();
        #region Constructor
        int UserID=0;
        public MainPage(int iUserId)
        {
            LogManager.WriteLog("<BMC DataSheet> Constructor", LogManager.enumLogLevel.Info);
            InitializeComponent();
            setTaskManager();
            UserID = iUserId;

        }

        private void setTaskManager()
        {
          
            this.btnGenerate.Tag = "Key_GenerateCaption";
            this.lblBasedon.Tag = "Key_BasedOnColon";
            this.Tag = "Key_BMCDataSheet";
            this.label2.Tag = "Key_CompanyColon";
            this.groupBox1.Tag = "Key_Criteria";
            this.label7.Tag = "Key_EndDateColon";
            this.label1.Tag = "Key_ReportNameColon";
            this.chkSelect.Tag = "Key_SelectAll";
            this.label4.Tag = "Key_SiteColon";
            this.label6.Tag = "Key_StartDateColon";
            this.label3.Tag = "Key_SubCompanyColon";
            this.label5.Tag = "Key_ZoneColon";

        }

        #endregion //Constructor

        #region Events

        private void MainPage_Load(object sender, EventArgs e)
        {
            this.ResolveResources();
            PopulateReports();
        }

        /// <summary>
        /// Based on the selected report populate the parameter controls
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbReportName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                PopulateHeaderList(cmbReportName.SelectedValue.ToString());
                //Showhide panel based on report selected

                pnlGeneral.Visible = false;
                pnlPUPD.Visible = false;
                cmbBasedOn.Visible = false;

                switch (cmbReportName.SelectedValue.ToString())
                {
                    case "rsp_Report_PerUnitPerDayPerformanceReport":
                        {
                            pnlGeneral.Visible = true;
                            pnlPUPD.Visible = true;
                            PopulateCriteriaControls();
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error in the cmbReportName_SelectedIndexChanged event" + ex.Message, LogManager.enumLogLevel.Error);
            }
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkLstBoxCustom.CheckedItems.Count <= 0)
                {
                    //MessageBox.Show(this.GetResourceTextByKey(1, "MSG_DAS_CHECK_ONE_RESULT"), this.GetResourceTextByKey(1, "MSG_APP_TITLE"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Win32Extensions.ShowWarningMessageBox(this, this.GetResourceTextByKey(1, "MSG_DAS_CHECK_ONE_RESULT"), this.Text);
                    return;
                }
                switch (cmbReportName.SelectedValue.ToString())
                {
                    case "rsp_Report_PerUnitPerDayPerformanceReport":
                        {
                            GetPUPDPerformanceReport();
                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error in the btnGenerate_Click event" + ex.Message, LogManager.enumLogLevel.Error);
            }
        }

        /// <summary>
        /// select/unselect the all the list of columns
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkSelect_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < chkLstBoxCustom.Items.Count; i++)
                    chkLstBoxCustom.SetItemChecked(i, chkSelect.Checked);
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error in the chkSelect_CheckedChanged event" + ex.Message, LogManager.enumLogLevel.Error);
            }
        }

        private void cmbCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                cmbSubCompany.DataSource = null;
                PopulateSubCompany(Convert.ToInt32(cmbCompany.SelectedValue));
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error in the cmbCompany_SelectedIndexChanged event" + ex.Message, LogManager.enumLogLevel.Error);
            }
        }

        private void cmbSubCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                cmbSite.DataSource = null;
                PopulateSites(Convert.ToInt32(cmbSubCompany.SelectedValue), Convert.ToInt32(cmbCompany.SelectedValue));
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error in the cmbSubCompany_SelectedIndexChanged event" + ex.Message, LogManager.enumLogLevel.Error);
            }
        }

        private void cmbSite_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int subCompanyID = Convert.ToInt32(cmbSubCompany.SelectedValue);
                int CompanyID = Convert.ToInt32(cmbCompany.SelectedValue);
                cmbZone.DataSource = null;
                PopulateZones(CompanyID, subCompanyID, Convert.ToInt32(cmbSite.SelectedValue));
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error in the cmbSite_SelectedIndexChanged event" + ex.Message, LogManager.enumLogLevel.Error);
            }
        }

        private void dtpPUPDStartDate_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                DateTimePicker dt = (DateTimePicker)sender;
                Control[] dtEnd = Controls.Find(dtpPUPDEndDate.Name, true);
                if (dtEnd.Length <= 0) return;
                DateTimePicker child = dtEnd[0] as DateTimePicker;

                if (dt.Value > child.Value)
                {
                    dt.MaxDate = child.Value;
                    return;
                }
                if (dt.Value > DateTime.Now)
                {
                    dt.MaxDate = DateTime.Now;
                    return;
                }

                dt.MaxDate = child.Value;
                child.MinDate = dt.Value;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error in the dtpPUPDStartDate_ValueChanged event" + ex.Message, LogManager.enumLogLevel.Error);
            }
        }

        private void dtpPUPDEndDate_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                DateTimePicker dt = (DateTimePicker)sender;
                Control[] dtStart = Controls.Find(dtpPUPDStartDate.Name, true);
                if (dtStart.Length <= 0) return;

                DateTimePicker child = dtStart[0] as DateTimePicker;
                if (dt.Value < child.Value)
                {
                    dt.MinDate = child.Value;
                    return;
                }
                if (dt.Value > DateTime.Now)
                {
                    dt.MaxDate = DateTime.Now;
                    return;
                }

                dt.MinDate = child.Value;
                child.MaxDate = dt.Value;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error in the dtpPUPDEndDate_ValueChanged event" + ex.Message, LogManager.enumLogLevel.Error);
            }
        }

        #endregion //Events

        #region Methods

        /// <summary>
        /// To populate reports in the Reportname combo
        /// </summary>
        private void PopulateReports()
        {
            try
            {
                cmbReportName.Items.Clear();

                CustomReportsBusiness objCustomReportsBusiness = new CustomReportsBusiness();
                cmbReportName.DataSource = new BindingSource(objCustomReportsBusiness.GetReports(@Application.StartupPath + "\\ReportColumnList.xml"), null);
                cmbReportName.DisplayMember = "Key";
                cmbReportName.ValueMember = "Value";
                cmbReportName.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error in the PopulateReports method" + ex.Message, LogManager.enumLogLevel.Error);
            }
        }

        private void PopulateCriteriaControls()
        {
            try
            {
                switch (cmbReportName.SelectedValue.ToString())
                {
                    case "rsp_Report_PerUnitPerDayPerformanceReport":
                        {
                            LoadandDisplayBasedOnCriteria();
                            PopulateCompany();
                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error in the PopulateCriteriaControls method" + ex.Message, LogManager.enumLogLevel.Error);
            }
        }

        private void PopulateCompany()
        {
            try
            {
                CustomReportsBusiness objCustomReportsBusiness = new CustomReportsBusiness();
                DataSet objData = objCustomReportsBusiness.PopulateCompany(UserID);
                cmbCompany.DisplayMember = "Company_Name";
                cmbCompany.ValueMember = "Company_ID";
                cmbCompany.DataSource = objData.Tables[0];

                if (cmbCompany.Items.Count > 0)
                    PopulateSubCompany(Convert.ToInt32(cmbCompany.SelectedValue));
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error in the PopulateCompany method" + ex.Message, LogManager.enumLogLevel.Error);
            }
        }

        private void PopulateSubCompany(int nCompanyID)
        {
            try
            {
                CustomReportsBusiness objCustomReportsBusiness = new CustomReportsBusiness();
                DataSet objData = objCustomReportsBusiness.PopulateSubCompany(nCompanyID,UserID);
                cmbSubCompany.DisplayMember = "Sub_Company_Name";
                cmbSubCompany.ValueMember = "Sub_Company_ID";
                cmbSubCompany.DataSource = objData.Tables[0];

                if (cmbSubCompany.Items.Count > 0)
                    PopulateSites(Convert.ToInt32(cmbSubCompany.SelectedValue), Convert.ToInt32(cmbCompany.SelectedValue));
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error in the PopulateSubCompany method" + ex.Message, LogManager.enumLogLevel.Error);
            }
        }

        private void PopulateSites(int nSubCompanyID, int nCompanyID)
        {
            try
            {
                CustomReportsBusiness objCustomReportsBusiness = new CustomReportsBusiness();
                DataSet objData = objCustomReportsBusiness.PopulateSites(nSubCompanyID, nCompanyID, UserID);
                cmbSite.DisplayMember = "Site_Name";
                cmbSite.ValueMember = "Site_ID";
                cmbSite.DataSource = objData.Tables[0];
                int subCompanyID = Convert.ToInt32(cmbSubCompany.SelectedValue);
                int CompanyID = Convert.ToInt32(cmbCompany.SelectedValue);
                if (cmbSite.Items.Count > 0)
                    PopulateZones(CompanyID, subCompanyID, Convert.ToInt32(cmbSite.SelectedValue));
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error in the PopulateSites method" + ex.Message, LogManager.enumLogLevel.Error);
            }
        }

        private void PopulateZones(int CompanyId, int nSubCompanyId, int nSiteID)
        {
            try
            {
                DataSet objData = new DataSet();
                if (cmbSite.SelectedIndex > 0 )
                {
                    CustomReportsBusiness objCustomReportsBusiness = new CustomReportsBusiness();
                    objData = objCustomReportsBusiness.PopulateZones(CompanyId, nSubCompanyId, nSiteID);
                    cmbZone.DataSource = objData.Tables[0];
                    cmbZone.DisplayMember = "Zone_Name";
                    cmbZone.ValueMember = "Zone_ID";
                }
                else
                {
                    cmbZone.Items.Clear();
                    //cmbZone.Items.Add("All");
                    cmbZone.Items.Add(this.GetResourceTextByKey("Key_All"));
                    cmbZone.SelectedIndex = 0;                    
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error in the PopulateZones method" + ex.Message, LogManager.enumLogLevel.Error);
            }
        }

        /// <summary>
        /// Based on the Report Name selection fill the list of custom columns from the XML to the check list box
        /// </summary>
        /// <param name="SPName"></param>
        private void PopulateHeaderList(string SPName)
        {
            try
            {
                chkLstBoxCustom.Items.Clear();

                XDocument xmlDoc = XDocument.Load(@Application.StartupPath + "\\ReportColumnList.xml");
                var q = from c in xmlDoc.Descendants("Report")
                        where c.Attribute("Value").Value == SPName
                        select c;
                _dicReportColumns.Clear();
                foreach (var item in q)
                {
                    foreach (var h in item.Elements())
                    {
                        string columnText=this.GetResourceTextByKey("Key_DS_"+h.Value);
                        if (!_dicReportColumns.ContainsKey(h.Value))
                        {
                            _dicReportColumns.Add(h.Value, columnText);
                            chkLstBoxCustom.Items.Add(columnText, true);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error in the PopulateHeaderList method" + ex.Message, LogManager.enumLogLevel.Error);
            }
        }

        /// <summary>
        /// Based on the search criteria fetch and display the results to the ResultDataGrid form
        /// </summary>
        private void GetPUPDPerformanceReport()
        {
            try
            {
                CustomReportsBusiness objCustomReportsBusiness = new CustomReportsBusiness();
                DataSet objDataSet = objCustomReportsBusiness.GetPUPDPerformanceReport
                                                            (Convert.ToInt32(cmbBasedOn.SelectedValue)
                                                            ,Convert.ToInt32(cmbCompany.SelectedValue)
                                                            , Convert.ToInt32(cmbSubCompany.SelectedValue)
                                                            , Convert.ToInt32(cmbSite.SelectedValue)
                                                            , Convert.ToInt32(cmbZone.SelectedValue)
                                                            , dtpPUPDStartDate.Value, dtpPUPDEndDate.Value);

                if (objDataSet != null && objDataSet.Tables.Count > 0 && objDataSet.Tables[0].Rows.Count > 0)
                {
                    List<string> lstItemsToRemove = new List<string>();
                    foreach (KeyValuePair<string, string> entry in _dicReportColumns)
                    {
                        if (!chkLstBoxCustom.CheckedItems.Contains(entry.Value))
                        {
                            lstItemsToRemove.Add(entry.Key);
                        }
                     }
                    ResultDataGrid objResultGrid = new ResultDataGrid(cmbReportName.SelectedValue.ToString(), objDataSet, lstItemsToRemove);
                    objResultGrid.ShowDialog(this);
                }
                else
                    //MessageBox.Show(this.GetResourceTextByKey(1, "MSG_CDM_NO_RECORDS"), this.GetResourceTextByKey(1, "MSG_APP_TITLE"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_CDM_NO_RECORDS"), this.Text);
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error in the GetPUPDPerformanceReport method" + ex.Message, LogManager.enumLogLevel.Error);
            }
        }

        private void LoadandDisplayBasedOnCriteria()
        {
            try
            {
                CustomReportsBusiness objCustomReportsBusiness = new CustomReportsBusiness();
                cmbBasedOn.Visible = true;
                cmbBasedOn.DataSource = new BindingSource(objCustomReportsBusiness.GetBasedOnFilter(), null);
                cmbBasedOn.DisplayMember = "Value";
                cmbBasedOn.ValueMember = "Key";
                cmbBasedOn.SelectedIndex = 0;
            }

            catch (Exception ex)
            {
                LogManager.WriteLog("Error in the DisplayBasedOnCriteria method" + ex.Message, LogManager.enumLogLevel.Error);
            }
        }

        #endregion Methods

    }
}
