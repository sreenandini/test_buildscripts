using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BMC.Common.Utilities;
using BMC.Common.LogManagement;
using BMC.CoreLib.Win32;
using BMC.Common;

namespace BMCTicketingConfig
{
    public partial class TicketingConfig : Form
    {


        DBClass business = new DBClass(DatabaseHelper.GetConnectionString());
        GetSitesResult sitesResult = new GetSitesResult();
        private string _HostSiteCode;
        public TicketingConfig(string SiteCode)
        {
            InitializeComponent();
            _HostSiteCode = SiteCode;
            LoadFullyConfiguredSites();
            setTagProperty();
        }

        private void setTagProperty()
        {
            this.btnCancel.Tag = "Key_CancelCaption";
            this.btnOk.Tag = "Key_OkCaption";
            this.label1.Tag = "Key_TicketingURL";
            this.label2.Tag = "Key_SiteAlliance";
            this.Tag = "Key_TicketingConfig";


        }

        private void TicketingConfig_Load(object sender, EventArgs e)
        {
            this.ResolveResources();
        }
        private void LoadFullyConfiguredSites()
        {
            IList<GetSitesResult> resultSet;

            try
            {
                LogManager.WriteLog("Inside LoadFullyConfiguredSites method", LogManager.enumLogLevel.Info);
                resultSet = business.GetSites(HostSiteCode).ToList();
                if (resultSet.Count > 0)
                {
                    grdGetSites.DataSource = resultSet;
                    grdGetSites.Columns[0].ReadOnly = true;
                    //grdGetSites.Columns[0].HeaderText = "Site Code";
                    grdGetSites.Columns[0].Tag= "Key_SiteCode";
                    //grdGetSites.Columns[1].HeaderText = "Is Cashable Redeemable";
                    grdGetSites.Columns[1].Tag = "Key_IsCashableRedeemable";
                    grdGetSites.Columns[1].Width = 140;
                   // grdGetSites.Columns[2].HeaderText = "Is Promo Redeemable";
                    grdGetSites.Columns[2].Tag = "Key_IsPromoRedeemable";
                    grdGetSites.Columns[2].Width = 125;
                    grdGetSites.Columns[3].Visible = false;
                    grdGetSites.ResolveResources();

                    txtTicketingURL.Text = resultSet[0].HostSiteURL;
                    LogManager.WriteLog("Sites successfully loaded", LogManager.enumLogLevel.Info);
                }
                else if (resultSet.Count <= 0)
                {
                    //MessageBox.Show("No sites found for cross ticketing", "BMC Enterprise", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Win32Extensions.ShowWarningMessageBox(this, this.GetResourceTextByKey(1,"MSG_TC_NOSITES"), this.Text);
                    //this.Close();
                    Environment.Exit(0);
                }

            }
            catch (Exception Ex)
            {
                LogManager.WriteLog("LoadFullyConfiguredSites Failed", LogManager.enumLogLevel.Info);
                //MessageBox.Show(Ex.Message);
                Win32Extensions.ShowErrorMessageBox(this, Ex.Message, this.Text);
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                LogManager.WriteLog("Inside button click ok", LogManager.enumLogLevel.Info);
                btnOk.Enabled = false;
                if (txtTicketingURL.Text.Trim() == string.Empty)
                {
                    //MessageBox.Show("Please enter the Ticketing URL", "BMC Enterprise", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Win32Extensions.ShowInfoMessageBox(this,this.GetResourceTextByKey(1,"MSG_TC_TICKETINGURL"), this.Text);
                    txtTicketingURL.Focus();
                }
                else
                {
                    //if (DialogResult.Yes == MessageBox.Show("Are you sure you want to apply these settings?", "BMC Enterprise", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                    if (DialogResult.Yes == Win32Extensions.ShowQuestionMessageBox(this, this.GetResourceTextByKey(1,"MSG_TC_APPLYCONFIRMATION"), this.Text))
                    {

                        foreach (DataGridViewRow row in grdGetSites.Rows)
                        {

                            sitesResult.ClientSiteCode = row.Cells[0].Value.ToString();
                            sitesResult.IsCashableRedeemable = Convert.ToBoolean(row.Cells[1].Value);
                            sitesResult.IsPromoRedeemable = Convert.ToBoolean(row.Cells[2].Value);

                            int result = business.InsertIntoSiteAlliance(sitesResult.ClientSiteCode, HostSiteCode, txtTicketingURL.Text, sitesResult.IsCashableRedeemable, sitesResult.IsPromoRedeemable);
                            if (result == 1)
                            {
                                //MessageBox.Show("Ticketing URL has been Already Configured", "BMC Enterprise", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1,"MSG_TC_ALREADYCONFIGURED"), this.Text);
                                btnOk.Enabled = true;
                                return;
                            }                            
                        }
                        //MessageBox.Show("Cross ticketing details updated successfully", "BMC Enterprise", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1,"MSG_TC_UPDATE"), this.Text);
                        LogManager.WriteLog("Cross ticketing details updated successfully for Site Code [" + HostSiteCode + "]", LogManager.enumLogLevel.Info);
                        this.Close();
                    }
                }
                btnOk.Enabled = true;
            }
            catch (Exception Ex)
            {
                LogManager.WriteLog("Failed to update cross ticketing details for Site Code [" + HostSiteCode + "]", LogManager.enumLogLevel.Info);
                //MessageBox.Show(Ex.Message);
                Win32Extensions.ShowErrorMessageBox(this, Ex.Message, this.Text);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #region Properties
        public string HostSiteCode
        {
            get
            {
                return this._HostSiteCode;
            }
            set
            {
                if ((this._HostSiteCode != value))
                {
                    this._HostSiteCode = value;
                }
            }
        }
        #endregion

    }
}
