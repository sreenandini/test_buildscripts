using BMC.Common.ExceptionManagement;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;

namespace CustomReports
{
    public partial class frmDataSheet : Form
    {
        int UserID = 0;
        public frmDataSheet(string sUserName,int iUserId)
        {
            InitializeComponent();
             UserID = iUserId;
        }

        private void frmDataSheet_Load(object sender, EventArgs e)
        {
            lblMessage.Text = string.Empty;
            PopulateReports();
        }
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
                ExceptionManager.Publish(ex);
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
                ExceptionManager.Publish(ex);
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
                ExceptionManager.Publish(ex);
            }
        }

        private void cmbReportName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                PopulateHeaderList(cmbReportName.SelectedValue.ToString());
                //Showhide panel based on report selected

               
                cmbBasedOn.Visible = false;

                switch (cmbReportName.SelectedValue.ToString())
                {
                    case "rsp_Report_PerUnitPerDayPerformanceReport":
                        {                           
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
                ExceptionManager.Publish(ex);
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

                foreach (var item in q)
                {
                    foreach (var h in item.Elements())
                        chkLstBoxCustom.Items.Add(h.Value, true);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkLstBoxCustom.CheckedItems.Count <= 0)
                {
                    MessageBox.Show("Please check atleast one result field to generate.", "Bally MultiConnect - Enterprise", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                ExceptionManager.Publish(ex);
            }
        }
        /// <summary>
        /// Based on the search criteria fetch and display the results to the ResultDataGrid form
        /// </summary>
        private void GetPUPDPerformanceReport()
        {
            try
            {
                lblMessage.Text = string.Empty;
                CustomReportsBusiness objCustomReportsBusiness = new CustomReportsBusiness();                
                DataSet objDataSet = objCustomReportsBusiness.GetPUPDPerformanceReport
                                                            (Convert.ToInt32(cmbBasedOn.SelectedValue)
                                                            , Convert.ToInt32(cmbCompany.SelectedValue)
                                                            , Convert.ToInt32(cmbSubCompany.SelectedValue)
                                                            , Convert.ToInt32(cmbSite.SelectedValue)
                                                            , Convert.ToInt32(cmbZone.SelectedValue)
                                                            , dtpPUPDStartDate.Value, dtpPUPDEndDate.Value);

                if (objDataSet != null && objDataSet.Tables.Count > 0 && objDataSet.Tables[0].Rows.Count > 0)
                {
                    List<string> lstItemsToRemove = new List<string>();
                    DataView objDataView = objDataSet.Tables[0].DefaultView;
                    DataTable dt = objDataSet.Tables[0];
                    foreach (DataColumn col in objDataSet.Tables[0].Columns)
                    {
                        if (!chkLstBoxCustom.CheckedItems.Contains(col.ColumnName))
                            lstItemsToRemove.Add(col.ColumnName);
                    }
                    foreach (string item in lstItemsToRemove)
                    {
                        dt.Columns.Remove(item);
                    }
                    dgvPreview.DataSource = dt;                    
                }
                else
                {
                    lblMessage.Text = "There are no records to display.";
                    lblMessage.ForeColor = Color.Red;
                     
                }
                //    MessageBox.Show("There are no records to display.", "Bally MultiConnect - Enterprise", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
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
                ExceptionManager.Publish(ex);
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
                ExceptionManager.Publish(ex);
            }
        }

        private void PopulateSites(int nSubCompanyID, int nCompanyID)
        {
            try
            {
                CustomReportsBusiness objCustomReportsBusiness = new CustomReportsBusiness();
                DataSet objData = objCustomReportsBusiness.PopulateSites(nSubCompanyID, nCompanyID);
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
                ExceptionManager.Publish(ex);
            }
        }

        private void PopulateZones(int CompanyId, int nSubCompanyId, int nSiteID)
        {
            try
            {
                DataSet objData = new DataSet();
                if (cmbSite.SelectedIndex > 0)
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
                    cmbZone.Items.Add("All");
                    cmbZone.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            string sFileName = string.Empty;
            try
            {
                if (dgvPreview.RowCount != 0)
                {
                    BMC.CoreLib.Win32.Win32Extensions.ExportControlDataToExcel<object>(this, dgvPreview, null, true, false, true);
                    lblMessage.Text = string.Empty;
                }
                else
                    lblMessage.Text = "No Records to Export.";
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < chkLstBoxCustom.Items.Count; i++)
                    chkLstBoxCustom.SetItemChecked(i, chkSelectAll.Checked);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
    }
}
