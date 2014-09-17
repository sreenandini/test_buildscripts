using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BMC.EnterpriseClient.Helpers;
using BMC.EnterpriseBusiness.Business;
using BMC.Common.ExceptionManagement;
using BMC.CommonLiquidation.Utilities;
using BMC.Common;
namespace BMC.EnterpriseClient.Views
{
    public partial class frmReadBasedLiquidation : GenericFormBase
    {
        #region Data Members

        private List<CommonLiquidationEntity> lstCommonLiquidation = null;
        private ReadBasedLiquidationBiz objReadBasedLiquidationBiz = new ReadBasedLiquidationBiz();

        #endregion //Data Members

        #region Constructor

        public frmReadBasedLiquidation()
        {
            try
            {
                InitializeComponent();
                SetTagProperty();
                if (!AppGlobals.Current.HasUserAccess("HQ_Financial_ReadLiquidationReport"))
                    tcReadBasedLiquidation.TabPages.Remove(tpReport);
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
        }

        private void SetTagProperty()
        {            
            this.btnClose.Tag = "Key_CloseCaption";
            this.btnDetails.Tag = "Key_DetailsCaption";
            this.btnPerform.Tag = "Key_PerformCaption";
            this.btnPrint.Tag = "Key_PrintCaption";
            this.btnRefresh.Tag = "Key_Refresh";
            this.Tag = "Key_ReadBasedLiquidation";
            this.tpReadLiquidation.Tag = "Key_ReadLiquidation";
            this.tpReport.Tag = "Key_Report";
        }

        #endregion //Constructor

        #region Events

        private void btnPerform_Click(object sender, EventArgs e)
        {
            LoadLiquidation();
        }

        private void btnDetails_Click(object sender, EventArgs e)
        {
            ucReadBasedLiquidation.ShowLiquidationDetails(1, 1);
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            ucReadBasedLiquidation.FillData();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            ucReadLiquidationReport.ShowLiquidationReport();
        }

        private void tcReadBasedLiquidation_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (((TabControl)sender).SelectedIndex == 0)
                {
                    ucReadBasedLiquidation.SiteCombo.SelectedIndex = 0;
                    return;
                }
                if (((TabControl)sender).SelectedIndex == 1)
                {
                    ucReadLiquidationReport.SiteCombo.SelectedIndex = 0;
                    return;
                }
            }

            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
        }

        #endregion //Events

        #region Methods

        private void LoadLiquidation()
        {
            try
            {
                if (!ValidateRead()) return;
                string ReadDate = Convert.ToDateTime(ucReadBasedLiquidation.ReadDataGrid.SelectedRows[0].Cells[1].Value).ToString("dd-MM-yyyy");
                string SiteName = ((BMC.EnterpriseDataAccess.rsp_GetActiveSiteDetailsBySettingResult)(ucReadBasedLiquidation.SiteCombo.SelectedItem)).Site_Name;
                frmReadLiquidation objfrmReadLiquidation = new frmReadLiquidation(ucReadBasedLiquidation.SiteId, lstCommonLiquidation, ucReadBasedLiquidation.SiteCombo.Text, ReadDate, SiteName);
                objfrmReadLiquidation.ShowDialog();
                ucReadBasedLiquidation.FillData();
            }

            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
        }


        private bool ValidateRead()
        {
            if (ucReadBasedLiquidation.ReadDataListCount <= 0)
            {
                this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_ENT_RBL_NO_RECORDS"), this.Text);
                return false;
            }

            for (int i = 0; i < ucReadBasedLiquidation.ReadDataGrid.SelectedRows.Count; i++)
            {
                if (ucReadBasedLiquidation.ReadDataGrid.Rows[i].Selected) continue;
                else
                {
                    this.ShowWarningMessageBox(this.GetResourceTextByKey(1, "MSG_ENT_RBL_SEQ"), this.Text);
                    return false;
                }
            }

            List<DateTime> lstReadDate = ucReadBasedLiquidation.ReadDataGrid.SelectedRows.Cast<DataGridViewRow>().Select(item => Convert.ToDateTime(item.Cells["Read_Date"].Value)).ToList();
            lstCommonLiquidation = objReadBasedLiquidationBiz.GetReadLiquidation(ucReadBasedLiquidation.SiteId, lstReadDate.Min(), lstReadDate.Max());
            
            if (lstCommonLiquidation == null || lstCommonLiquidation.Count <= 0)
            {
                this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_ENT_RBL_NO_RECORDS"), this.Text);
                return false;
            }

            return true;
        }

        #endregion //Methods

        private void frmReadBasedLiquidation_Load(object sender, EventArgs e)
        {
            this.ResolveResources();
        }

    }
}
