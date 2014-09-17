using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Documents;
using System.Windows.Forms;
using BMC.Common.ExceptionManagement;
using BMC.CoreLib;
using BMC.CoreLib.Diagnostics;
using BMC.EnterpriseBusiness.Business;
using BMC.EnterpriseBusiness.Entities;
using BMC.EnterpriseClient.Helpers;
using Microsoft.VisualBasic;

namespace BMC.EnterpriseClient.Views
{
    public partial class frmServiceCalls : Form
    {
        string anyText = "--ANY--";
        private string StrCallsType = string.Empty;

        private string _sDepotIDList = string.Empty;
        private string _sStaffIDList = string.Empty;
        private string _sSiteIDList = string.Empty;
        private int? _iSubCompanyID = 0;
        private int? _iJobID = 0;

        private int? _iCallStatusID = 0;
        private int? _iCallGroupID = 0;

        private DateTime _dtStartDate;
        private DateTime _dtEndDate;
        private int? _iCallRemedyID = 0;
        private string _sAssetNo = string.Empty;

        private string sDepotList = string.Empty;
        private string sStaffList = string.Empty;
        private string sSiteList = string.Empty;
        private string sSubCompany = string.Empty;
        private string sJobID = string.Empty;

        private string sCallStatus = string.Empty;
        private string sCallGroup = string.Empty;

        private string sCallRemedy = string.Empty;

        List<ServiceCurrentCallDetailsEntity> lstCurrentCalls = null;
        List<ServiceClosedCallDetailsEntity> lstClosedCalls = null;
        bool isCallStatusMinimal = true;

        public frmServiceCalls(string _strCallsType)
        {
            InitializeComponent();
            StrCallsType = _strCallsType;

            isCallStatusMinimal = !SettingsEntity.IsServiceCallFeatureFull;   // Get from Settings

            if (!AppGlobals.Current.HasUserAccess("HQ_Engineers_CreateCall"))
            {
                btnCreateCall.Visible = false;
            }
        }

        private void uxDockPanel1_Resize(object sender, EventArgs e)
        {
            TableLayoutColumnStyleCollection styles = this.tblInner.ColumnStyles;

            ColumnStyle style = styles[0];
            if (uxDockPanel1.ChildContainer.Visible == false)
            {
                style.Width = 31;
            }
            else
            {
                style.Width = 286;
            }
        }

        private void frmServiceCalls_Load(object sender, EventArgs e)
        {
            if (StrCallsType == "CurrentCalls")
            {
                pnlFrom.Visible = false;
                pnlTo.Visible = false;
                pnlRemedy.Visible = false;
                lblAssetNo.Visible = false;
                txtAssetNo.Visible = false;
                dgvClosedCalls.Visible = false;

                LoadCurrentCallsFilterCriteria();
            }
            else if (StrCallsType == "ClosedCalls")
            {
                pnlCallStatus.Visible = false;
                pnlFaultGroup.Visible = false;
                btnCreateCall.Visible = false;
                btnExportCalls.Visible = false;
                dgvCurrentCalls.Visible = false;

                LoadClosedCallsFilterCriteria();
            }

            LoadCommonFilterCriteria();
        }

        private void LoadCommonFilterCriteria()
        {
            try
            {
                lstDepot.DataSource = ServiceCallBusiness.CreateInstance().LoadDepotNames(anyText);
                lstDepot.DisplayMember = "Description";
                lstDepot.ValueMember = "Id";
                lstDepot.SelectedIndex = 0;

                lstEngineer.DataSource = ServiceCallBusiness.CreateInstance().LoadEngineerNames(0, anyText);
                lstEngineer.DisplayMember = "Description";
                lstEngineer.ValueMember = "Id";
                lstEngineer.SelectedIndex = 0;

                lstSite.DataSource = ServiceCallBusiness.CreateInstance().LoadSiteNames(0, anyText);
                lstSite.DisplayMember = "Description";
                lstSite.ValueMember = "Id";
                lstSite.SelectedIndex = 0;

                cmbSubCompany.DataSource = ServiceCallBusiness.CreateInstance().LoadSubCompanyNames();
                cmbSubCompany.DisplayMember = "Sub_Company_Name";
                cmbSubCompany.ValueMember = "Sub_Company_ID";
                cmbSubCompany.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void LoadCurrentCallsFilterCriteria()
        {
            try
            {
                ServiceCallBusiness.LoadCallStatus(cmbCallStatus, true, true, isCallStatusMinimal);
                cmbCallStatus.SelectedIndex = 0;

                cmbFaultGroup.DataSource = ServiceCallBusiness.CreateInstance().LoadCallGroup(anyText);
                cmbFaultGroup.DisplayMember = "Description";
                cmbFaultGroup.ValueMember = "Id";
                cmbFaultGroup.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void LoadClosedCallsFilterCriteria()
        {
            try
            {
                dtpFrom.Format = DateTimePickerFormat.Custom;
                dtpFrom.CustomFormat = "dd/MM/yyyy HH:mm";
                dtpTo.Format = DateTimePickerFormat.Custom;
                dtpTo.CustomFormat = "dd/MM/yyyy HH:mm";

                cmbRemedy.DataSource = ServiceCallBusiness.CreateInstance().LoadCallRemedy(anyText);
                cmbRemedy.DisplayMember = "Description";
                cmbRemedy.ValueMember = "Id";
                cmbRemedy.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btnClearFilters_Click(object sender, EventArgs e)
        {
            try
            {
                if (StrCallsType == "CurrentCalls")
                {
                    cmbCallStatus.SelectedIndex = 0;
                    cmbFaultGroup.SelectedIndex = 0;
                }
                else
                {
                    dtpFrom.ResetText();
                    dtpTo.ResetText();
                    cmbRemedy.SelectedIndex = 0;
                    txtAssetNo.ResetText();
                }

                lstDepot.ClearSelected();
                lstEngineer.ClearSelected();
                lstSite.ClearSelected();

                lstDepot.SelectedIndex = 0;
                lstEngineer.SelectedIndex = 0;
                lstSite.SelectedIndex = 0;
                cmbSubCompany.SelectedIndex = 0;
                txtJob.ResetText();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                _sDepotIDList = string.Join(",", from item in lstDepot.SelectedItems.OfType<DepotEntityForService>().ToList() select item.Id);
                _sStaffIDList = string.Join(",", from item in lstEngineer.SelectedItems.OfType<EngineerEntityForService>().ToList() select item.Id);
                _sSiteIDList = string.Join(",", from item in lstSite.SelectedItems.OfType<SiteEntityForService>().ToList() select item.Id);
                _iSubCompanyID = Convert.ToInt32(cmbSubCompany.SelectedValue.ToString());
                _iJobID = string.IsNullOrEmpty(txtJob.Text.Trim()) ? 0 : Convert.ToInt32(txtJob.Text.Trim());

                sDepotList = string.Join(",", from item in lstDepot.SelectedItems.OfType<DepotEntityForService>().ToList() select item.Description);
                sStaffList = string.Join(",", from item in lstEngineer.SelectedItems.OfType<EngineerEntityForService>().ToList() select item.Description);
                sSiteList = string.Join(",", from item in lstSite.SelectedItems.OfType<SiteEntityForService>().ToList() select item.Description);
                sSubCompany = ((SubCompanyEntityForService)cmbSubCompany.SelectedItem).Sub_Company_Name;
                sJobID = txtJob.Text.Trim();

                if (StrCallsType == "CurrentCalls")
                {
                    dgvCurrentCalls.DataSource = null;

                    _iCallStatusID = Convert.ToInt32(cmbCallStatus.SelectedValue.ToString());
                    _iCallGroupID = Convert.ToInt32(cmbFaultGroup.SelectedValue.ToString());

                    sCallStatus = ServiceCallBusiness.GetEnumDescription((Enum)((CallStatus)_iCallStatusID));
                    sCallGroup = ((CallGroup)cmbFaultGroup.SelectedItem).Description;

                    lstCurrentCalls = ServiceCallBusiness.CreateInstance().GetServiceCurrentCallDetails(_iCallStatusID, _iCallGroupID, _sDepotIDList, _sStaffIDList, _sSiteIDList, _iSubCompanyID, _iJobID);

                    if (lstCurrentCalls.Count == 0)
                    {
                        this.ShowMessageBox("No Open Calls found for the selected criteria", MessageBoxButtons.OK, MessageBoxIcon.None);
                        return;
                    }

                    dgvCurrentCalls.DataSource = lstCurrentCalls;


                    dgvCurrentCalls.Columns[15].Visible = false;
                    dgvCurrentCalls.Columns[16].Visible = false;
                    dgvCurrentCalls.Columns[17].Visible = false;
                    dgvCurrentCalls.Columns[18].Visible = false;
                    dgvCurrentCalls.Columns[19].Visible = false;
                    dgvCurrentCalls.Columns[20].Visible = false;
                    dgvCurrentCalls.Columns[21].Visible = false;
                    dgvCurrentCalls.Columns[22].Visible = false;
                    dgvCurrentCalls.Columns[23].Visible = false;

                    dgvCurrentCalls.Columns[0].HeaderText = "Job ID\\Service Visit No.";
                    dgvCurrentCalls.Columns[1].HeaderText = "Call Status";
                    dgvCurrentCalls.Columns[2].HeaderText = "Date Logged";
                    dgvCurrentCalls.Columns[3].HeaderText = "Fault";
                    dgvCurrentCalls.Columns[4].HeaderText = "Status";
                    dgvCurrentCalls.Columns[4].Visible = false;
                    dgvCurrentCalls.Columns[5].HeaderText = "Downtime";
                    dgvCurrentCalls.Columns[6].HeaderText = "Logged By";
                    dgvCurrentCalls.Columns[7].HeaderText = "Site Name, Address";
                    dgvCurrentCalls.Columns[8].HeaderText = "Site Code";
                    dgvCurrentCalls.Columns[9].HeaderText = "Machine Type";
                    dgvCurrentCalls.Columns[10].HeaderText = "Machine";
                    dgvCurrentCalls.Columns[11].HeaderText = "Engineer";
                    dgvCurrentCalls.Columns[12].HeaderText = "Sub Company";
                    dgvCurrentCalls.Columns[13].HeaderText = "Post/Zip Code";
                    dgvCurrentCalls.Columns[14].HeaderText = "SLA";
                    dgvCurrentCalls.Columns[14].Visible = false;

                    dgvCurrentCalls.AutoResizeColumns();
                }
                else
                {
                    if (dtpFrom.Value > dtpTo.Value)
                    {
                        this.ShowMessageBox("From Date must be lesser than To Date", "Service - Closed Calls", MessageBoxButtons.OK, MessageBoxIcon.None);
                        return;
                    }

                    dgvClosedCalls.DataSource = null;

                    _dtStartDate = dtpFrom.Value;
                    _dtEndDate = dtpTo.Value;
                    _iCallRemedyID = Convert.ToInt32(cmbRemedy.SelectedValue.ToString());
                    _sAssetNo = txtAssetNo.Text.Trim().ToString();

                    sCallRemedy = ((CallRemedy)cmbRemedy.SelectedItem).Description;

                    lstClosedCalls = ServiceCallBusiness.CreateInstance().GetServiceClosedCallDetails(_dtStartDate, _dtEndDate, _iCallRemedyID, _sDepotIDList, _sStaffIDList, _sSiteIDList, _iSubCompanyID, _iJobID, _sAssetNo);

                    if (lstClosedCalls.Count == 0)
                    {
                        this.ShowMessageBox("No Closed Calls found for the selected criteria", MessageBoxButtons.OK, MessageBoxIcon.None);
                        return;
                    }

                    dgvClosedCalls.DataSource = lstClosedCalls;

                    dgvClosedCalls.Columns[14].Visible = false;
                    dgvClosedCalls.Columns[15].Visible = false;
                    dgvClosedCalls.Columns[16].Visible = false;
                    dgvClosedCalls.Columns[17].Visible = false;
                    dgvClosedCalls.Columns[18].Visible = false;
                    dgvClosedCalls.Columns[19].Visible = false;
                    dgvClosedCalls.Columns[20].Visible = false;
                    dgvClosedCalls.Columns[21].Visible = false;
                    dgvClosedCalls.Columns[22].Visible = false;
                    dgvClosedCalls.Columns[23].Visible = false;
                    dgvClosedCalls.Columns[24].Visible = false;
                    dgvClosedCalls.Columns[25].Visible = false;
                    dgvClosedCalls.Columns[26].Visible = false;

                    dgvClosedCalls.Columns[0].HeaderText = "Job ID\\Service Visit No.";
                    dgvClosedCalls.Columns[1].HeaderText = "Date Logged";
                    dgvClosedCalls.Columns[2].HeaderText = "Logged By";
                    dgvClosedCalls.Columns[3].HeaderText = "Closed By";
                    dgvClosedCalls.Columns[4].HeaderText = "Downtime";
                    dgvClosedCalls.Columns[5].HeaderText = "Date Closed";
                    dgvClosedCalls.Columns[6].HeaderText = "Site Name, Address";
                    dgvClosedCalls.Columns[7].HeaderText = "Site Code";
                    dgvClosedCalls.Columns[8].HeaderText = "Machine Type";
                    dgvClosedCalls.Columns[9].HeaderText = "Machine";
                    dgvClosedCalls.Columns[10].HeaderText = "Engineer";
                    dgvClosedCalls.Columns[11].HeaderText = "SLA";
                    dgvClosedCalls.Columns[11].Visible = false;
                    dgvClosedCalls.Columns[12].HeaderText = "Fault";
                    dgvClosedCalls.Columns[13].HeaderText = "Remedy";

                    dgvClosedCalls.AutoResizeColumns();
                }

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void txtJob_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar)
    && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnExportCalls_Click(object sender, EventArgs e)
        {
            if (StrCallsType == "CurrentCalls")
            {
                GlobalHelper.ExportTocsv(this.dgvCurrentCalls);
            }
        }

        private void btnPrintCalls_Click(object sender, EventArgs e)
        {
            if (dgvCurrentCalls.SelectedRows.Count > 0)
            {
                Cursor.Current = Cursors.WaitCursor;
                try
                {
                    // TODO: To use rdl report
                    //using (frmCrystalReportViewer cReportViewer = new frmCrystalReportViewer())
                    //{
                    //    cReportViewer.showServiceCurrentCalls(_iCallStatusID, _iCallGroupID, _sDepotIDList, _sStaffIDList, _sSiteIDList, _iSubCompanyID, _iJobID,
                    //                                            sCallStatus, sCallGroup, sDepotList, sStaffList, sSiteList, sSubCompany, sJobID);
                    //    cReportViewer.ShowDialog();
                    //}
                    //Cursor.Current = Cursors.Default;
                }
                catch (Exception ex)
                {
                    ExceptionManager.Publish(ex);
                    Cursor.Current = Cursors.Default;
                }
            }
            if (dgvClosedCalls.SelectedRows.Count > 0)
            {
                Cursor.Current = Cursors.WaitCursor;
                try
                {
                    // TODO: To use rdl report
                    //using (frmCrystalReportViewer cReportViewer = new frmCrystalReportViewer())
                    //{
                    //    cReportViewer.showServiceClosedCalls(_dtStartDate, _dtEndDate, _iCallRemedyID, _sDepotIDList, _sStaffIDList, _sSiteIDList, _iSubCompanyID, _iJobID,
                    //                                            sCallRemedy, sDepotList, sStaffList, sSiteList, sSubCompany, sJobID, _sAssetNo);
                    //    cReportViewer.ShowDialog();
                    //}
                    //Cursor.Current = Cursors.Default;
                }
                catch (Exception ex)
                {
                    ExceptionManager.Publish(ex);
                    Cursor.Current = Cursors.Default;
                }
            }
        }

        private void btnSiteHistory_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvCurrentCalls.SelectedRows.Count > 0 || dgvClosedCalls.SelectedRows.Count > 0)
                {
                    string _sSite_ID = string.Empty;

                    if (StrCallsType == "CurrentCalls")
                    {
                        _sSite_ID = Convert.ToString(dgvCurrentCalls.SelectedRows[0].Cells["Site_ID"].Value);
                    }
                    else
                    {
                        _sSite_ID = Convert.ToString(dgvClosedCalls.SelectedRows[0].Cells["Site_ID"].Value);
                    }

                    List<ServiceCurrentCallDetailsEntity> lstSiteOpenCalls = ServiceCallBusiness.CreateInstance().GetServiceCurrentCallDetails(0, 0, string.Empty, string.Empty, _sSite_ID, 0, 0);

                    List<ServiceClosedCallDetailsEntity> lstSiteClosedCalls = new List<ServiceClosedCallDetailsEntity>();
                    lstSiteClosedCalls = ServiceCallBusiness.CreateInstance().GetServiceClosedCallDetails(null, null, 0, string.Empty, string.Empty, _sSite_ID, 0, 0, string.Empty);

                    frmServiceSiteHistory objfrmSH = new frmServiceSiteHistory(StrCallsType, lstSiteOpenCalls, lstSiteClosedCalls);
                    objfrmSH.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void lstDepot_MouseMove(object sender, MouseEventArgs e)
        {
            ListBox lb = (ListBox)sender;
            int index = lb.IndexFromPoint(e.Location);

            if (index >= 0 && index < lb.Items.Count)
            {
                string toolTipString = ((DepotEntityForService)lb.Items[index]).Description;

                if (toolTip1.GetToolTip(lb) != toolTipString)
                    toolTip1.SetToolTip(lb, toolTipString);
            }
            else
                toolTip1.Hide(lb);
        }


        private void lstEngineer_MouseMove(object sender, MouseEventArgs e)
        {
            ListBox lb = (ListBox)sender;
            int index = lb.IndexFromPoint(e.Location);

            if (index >= 0 && index < lb.Items.Count)
            {
                string toolTipString = ((EngineerEntityForService)lb.Items[index]).Description;

                if (toolTip1.GetToolTip(lb) != toolTipString)
                    toolTip1.SetToolTip(lb, toolTipString);
            }
            else
                toolTip1.Hide(lb);
        }

        private void lstSite_MouseMove(object sender, MouseEventArgs e)
        {
            ListBox lb = (ListBox)sender;
            int index = lb.IndexFromPoint(e.Location);

            if (index >= 0 && index < lb.Items.Count)
            {
                string toolTipString = ((SiteEntityForService)lb.Items[index]).Description;

                if (toolTip1.GetToolTip(lb) != toolTipString)
                    toolTip1.SetToolTip(lb, toolTipString);
            }
            else
                toolTip1.Hide(lb);
        }

        private void dgvCurrentCalls_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                frmServiceCallCreate objScc = new frmServiceCallCreate(Convert.ToInt32(dgvCurrentCalls.Rows[e.RowIndex].Cells["Service_ID"].Value), Convert.ToInt32(dgvCurrentCalls.Rows[e.RowIndex].Cells["Site_ID"].Value), false);
                objScc.BringToFront();
                objScc.ShowDialog();
                btnSearch.PerformClick();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void dgvClosedCalls_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                frmServiceCallCreate objScc = new frmServiceCallCreate(Convert.ToInt32(dgvClosedCalls.Rows[e.RowIndex].Cells["Service_ID"].Value), Convert.ToInt32(dgvClosedCalls.Rows[e.RowIndex].Cells["Site_ID"].Value), true);
                objScc.ShowDialog();
                btnSearch.PerformClick();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btnCreateCall_Click(object sender, EventArgs e)
        {
            frmServiceCallCreate obj = new frmServiceCallCreate();
            obj.ShowDialog();
        }

        private void lstDepot_MouseClick(object sender, MouseEventArgs e)
        {
            SetListBoxLimitation(sender, e);
        }

        private void lstEngineer_MouseClick(object sender, MouseEventArgs e)
        {
            SetListBoxLimitation(sender, e);
        }

        private void lstSite_MouseClick(object sender, MouseEventArgs e)
        {
            SetListBoxLimitation(sender, e);
        }

        private void SetListBoxLimitation(object sender, MouseEventArgs e)
        {
            ListBox lb = (ListBox)sender;
            int index = lb.IndexFromPoint(e.Location);

            if (lb.SelectedItems.Count == 4)
            {
                lb.SelectedItems.Remove(lb.Items[index]);
            }

            if (lb.SelectedValue != null && lb.SelectedItems.Count > 0 && lb.Items[index] == lb.Items[0])
            {
                if (lb.SelectedItems.Count > 1)
                {
                    lb.ClearSelected();
                    lb.SetSelected(0, true);
                }
            }
            else if (lb.SelectedValue != null && lb.SelectedItems.Count > 0 && lb.Items[index] != lb.Items[0])
            {
                lb.SetSelected(0, false);
            }
        }
    }
}
