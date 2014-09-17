using BMC.Common;
using BMC.Common.ConfigurationManagement;
using BMC.Common.ExceptionManagement;
using BMC.Common.LogManagement;
using BMC.CoreLib.Concurrent;
using BMC.CoreLib.Win32;
using BMC.EnterpriseBusiness.Business;
using BMC.EnterpriseBusiness.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace BMC.EnterpriseClient.Views
{
    public partial class TicketingConfig : Form
    {
        CrossTicketingConfigBiz oCrossTicketingConfigBiz = null;
        List<CrossTicketingSetting> lstCrossTicketingSetting = null;

        private string _HostSiteCode;
        
        public TicketingConfig(string SiteCode)
        {
            InitializeComponent();
            oCrossTicketingConfigBiz = CrossTicketingConfigBiz.CreateInstance();
            _HostSiteCode = SiteCode;
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
            LoadFullyConfiguredSites();
        }

        private void LoadFullyConfiguredSites()
        {
            try
            {
                LogManager.WriteLog("Inside LoadFullyConfiguredSites method", LogManager.enumLogLevel.Info);
                lstCrossTicketingSetting = oCrossTicketingConfigBiz.GetCrossTicketConfigSites(HostSiteCode);

                if (lstCrossTicketingSetting.Count > 0)
                {
                    grdGetSites.DataSource = lstCrossTicketingSetting;

                    grdGetSites.Columns[0].ReadOnly = true;
                    grdGetSites.Columns[0].Tag= "Key_SiteCode";
                    grdGetSites.Columns[1].Tag = "Key_IsCashableRedeemable";
                    grdGetSites.Columns[1].Width = 140;
                    grdGetSites.Columns[2].Tag = "Key_IsPromoRedeemable";
                    grdGetSites.Columns[2].Width = 125;
                    grdGetSites.Columns[3].Visible = false;
                    grdGetSites.ResolveResources();

                    txtTicketingURL.Text = lstCrossTicketingSetting[0].HostSiteURL;
                    LogManager.WriteLog("Sites successfully loaded", LogManager.enumLogLevel.Info);
                }
                else if (lstCrossTicketingSetting.Count <= 0)
                {
                    Win32Extensions.ShowWarningMessageBox(this, this.GetResourceTextByKey(1,"MSG_TC_NOSITES"), this.Text);
                    this.Close();
                }

            }
            catch (Exception Ex)
            {
                LogManager.WriteLog("LoadFullyConfiguredSites Failed", LogManager.enumLogLevel.Info);
                Win32Extensions.ShowErrorMessageBox(this, Ex.Message, this.Text);
                this.Close();
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

                    Win32Extensions.ShowInfoMessageBox(this,this.GetResourceTextByKey(1,"MSG_TC_TICKETINGURL"), this.Text);
                    txtTicketingURL.Focus();
                }
                else
                {
                    CrossTicketingSetting oCrossTicketingSetting = new CrossTicketingSetting();
                    if (DialogResult.Yes == Win32Extensions.ShowQuestionMessageBox(this, this.GetResourceTextByKey(1,"MSG_TC_APPLYCONFIRMATION"), this.Text))
                    {
                        foreach (DataGridViewRow row in grdGetSites.Rows)
                        {
                            oCrossTicketingSetting.ClientSiteCode = row.Cells[0].Value.ToString();
                            oCrossTicketingSetting.IsCashableRedeemable = Convert.ToBoolean(row.Cells[1].Value);
                            oCrossTicketingSetting.IsPromoRedeemable = Convert.ToBoolean(row.Cells[2].Value);

                            int result = oCrossTicketingConfigBiz.InsertIntoSiteAlliance(oCrossTicketingSetting.ClientSiteCode, HostSiteCode, txtTicketingURL.Text, oCrossTicketingSetting.IsCashableRedeemable, oCrossTicketingSetting.IsPromoRedeemable);
                            
                            if (result == 1)
                            {
                                Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1,"MSG_TC_ALREADYCONFIGURED"), this.Text);
                                btnOk.Enabled = true;
                                return;
                            }
                        }
                        
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
