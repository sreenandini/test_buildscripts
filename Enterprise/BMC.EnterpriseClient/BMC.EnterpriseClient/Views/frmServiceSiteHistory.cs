using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BMC.Common.ExceptionManagement;
using BMC.EnterpriseBusiness.Entities;

namespace BMC.EnterpriseClient.Views
{
    public partial class frmServiceSiteHistory : Form
    {
        public frmServiceSiteHistory()
        {
            InitializeComponent();
        }

        public frmServiceSiteHistory(string StrCallsType, List<ServiceCurrentCallDetailsEntity> lstSiteOpenCalls, List<ServiceClosedCallDetailsEntity> lstSiteClosedCalls)
        {
            InitializeComponent();
            try
            {
                if (StrCallsType == "CurrentCalls")
                {
                    lblSite.Text = lstSiteOpenCalls[0].Site_Code + ", " + lstSiteOpenCalls[0].Site_Address + ", " + lstSiteOpenCalls[0].Site_Postcode;
                }
                else
                {
                    lblSite.Text = lstSiteClosedCalls[0].Site_Code + ", " + lstSiteClosedCalls[0].Site_Name_Address + ", " + lstSiteClosedCalls[0].Site_Postcode;
                }
                LoadSiteOpenCallsGRV(lstSiteOpenCalls);
                LoadSiteClosedCallsGRV(lstSiteClosedCalls);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void LoadSiteOpenCallsGRV(List<ServiceCurrentCallDetailsEntity> lstCurrentCalls)
        {
            try
            {
                dgvSiteOpenCalls.DataSource = lstCurrentCalls;

                dgvSiteOpenCalls.Columns[15].Visible = false;
                dgvSiteOpenCalls.Columns[16].Visible = false;
                dgvSiteOpenCalls.Columns[17].Visible = false;
                dgvSiteOpenCalls.Columns[18].Visible = false;
                dgvSiteOpenCalls.Columns[19].Visible = false;
                dgvSiteOpenCalls.Columns[20].Visible = false;
                dgvSiteOpenCalls.Columns[21].Visible = false;
                dgvSiteOpenCalls.Columns[22].Visible = false;
                dgvSiteOpenCalls.Columns[23].Visible = false;

                dgvSiteOpenCalls.Columns[0].HeaderText = "Job ID\\Service Visit No.";
                dgvSiteOpenCalls.Columns[1].HeaderText = "Call Status";
                dgvSiteOpenCalls.Columns[2].HeaderText = "Date Logged";
                dgvSiteOpenCalls.Columns[3].HeaderText = "Fault";
                dgvSiteOpenCalls.Columns[4].HeaderText = "Status";
                dgvSiteOpenCalls.Columns[4].Visible = false;
                dgvSiteOpenCalls.Columns[5].HeaderText = "Downtime";
                dgvSiteOpenCalls.Columns[6].HeaderText = "Logged By";
                dgvSiteOpenCalls.Columns[7].HeaderText = "Site Name, Address";
                dgvSiteOpenCalls.Columns[7].Visible = false;
                dgvSiteOpenCalls.Columns[8].HeaderText = "Site Code";
                dgvSiteOpenCalls.Columns[8].Visible = false;
                dgvSiteOpenCalls.Columns[9].HeaderText = "Machine Type";
                dgvSiteOpenCalls.Columns[10].HeaderText = "Machine";
                dgvSiteOpenCalls.Columns[11].HeaderText = "Engineer";
                dgvSiteOpenCalls.Columns[12].HeaderText = "Sub Company";
                dgvSiteOpenCalls.Columns[13].HeaderText = "Post/Zip Code";
                dgvSiteOpenCalls.Columns[13].Visible = false;
                dgvSiteOpenCalls.Columns[14].HeaderText = "SLA";
                dgvSiteOpenCalls.Columns[14].Visible = false;

                dgvSiteOpenCalls.AutoResizeColumns();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void LoadSiteClosedCallsGRV(List<ServiceClosedCallDetailsEntity> lstClosedCalls)
        {
            try
            {
                dgvSiteClosedCalls.DataSource = lstClosedCalls;

                dgvSiteClosedCalls.Columns[14].Visible = false;
                dgvSiteClosedCalls.Columns[15].Visible = false;
                dgvSiteClosedCalls.Columns[16].Visible = false;
                dgvSiteClosedCalls.Columns[17].Visible = false;
                dgvSiteClosedCalls.Columns[18].Visible = false;
                dgvSiteClosedCalls.Columns[19].Visible = false;
                dgvSiteClosedCalls.Columns[20].Visible = false;
                dgvSiteClosedCalls.Columns[21].Visible = false;
                dgvSiteClosedCalls.Columns[22].Visible = false;
                dgvSiteClosedCalls.Columns[23].Visible = false;
                dgvSiteClosedCalls.Columns[24].Visible = false;
                dgvSiteClosedCalls.Columns[25].Visible = false;
                dgvSiteClosedCalls.Columns[26].Visible = false;

                dgvSiteClosedCalls.Columns[0].HeaderText = "Job ID\\Service Visit No.";
                dgvSiteClosedCalls.Columns[1].HeaderText = "Date Logged";
                dgvSiteClosedCalls.Columns[2].HeaderText = "Logged By";
                dgvSiteClosedCalls.Columns[3].HeaderText = "Closed By";
                dgvSiteClosedCalls.Columns[4].HeaderText = "Downtime";
                dgvSiteClosedCalls.Columns[5].HeaderText = "Date Closed";
                dgvSiteClosedCalls.Columns[6].HeaderText = "Site Name, Address";
                dgvSiteClosedCalls.Columns[6].Visible = false;
                dgvSiteClosedCalls.Columns[7].HeaderText = "Site Code";
                dgvSiteClosedCalls.Columns[7].Visible = false;
                dgvSiteClosedCalls.Columns[8].HeaderText = "Machine Type";
                dgvSiteClosedCalls.Columns[9].HeaderText = "Machine";
                dgvSiteClosedCalls.Columns[10].HeaderText = "Engineer";
                dgvSiteClosedCalls.Columns[11].HeaderText = "SLA";
                dgvSiteClosedCalls.Columns[11].Visible = false;
                dgvSiteClosedCalls.Columns[12].HeaderText = "Fault";
                dgvSiteClosedCalls.Columns[13].HeaderText = "Remedy";

                dgvSiteClosedCalls.AutoResizeColumns();

                if(dgvSiteClosedCalls.CurrentCell != null)
                    dgvSiteClosedCalls.CurrentCell.Selected = false;
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

    }
}
