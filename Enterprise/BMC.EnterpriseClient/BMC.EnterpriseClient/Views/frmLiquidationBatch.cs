using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BMC.EnterpriseBusiness.Business;
using BMC.Common.ExceptionManagement;
using BMC.Common.LogManagement;
using BMC.CoreLib.Win32;
using BMC.Common;

namespace BMC.EnterpriseClient.Views
{
    public partial class frmLiquidationBatch : Form
    {
        public frmLiquidationBatch()
        {
            LogManager.WriteLog("Liquidation Batch Starts", LogManager.enumLogLevel.Info);
            InitializeComponent();
            LogManager.WriteLog("Liquidation Batch-Loading Site Combo Starts", LogManager.enumLogLevel.Info);
            LoadSiteCombo();
            LoadBatchNo();
            LogManager.WriteLog("Liquidation Batch-Loading Site Combo Ends", LogManager.enumLogLevel.Info);
            
            //Resource file changes
            SetPropertyTag();
        }
       
        //Resource file changes
        private void SetPropertyTag()
        {
            try
            {
                this.lblBatchNo.Tag = "Key_BatchNoMandatoryColon";
                this.lblSiteCode.Tag = "Key_SiteCodeMandatory";
                this.btnClose.Tag = "Key_Close";
                this.btnPerformLiquidation.Tag = "Key_PerformLiquidation";
            }
            catch (Exception ex)
            {                
                ExceptionManager.Publish(ex);
            }
           
        }
        
        private void LoadBatchNo()
        {
            CollectionBasedLiquidation objLiquidationBiz = new CollectionBasedLiquidation();
            try
            {
                int SiteId =Convert.ToInt32(cboSiteCode.SelectedValue.ToString());
                DataTable dtBatch = objLiquidationBiz.GetBatchNoForLiquidation(SiteId);
                DataRow dr = dtBatch.NewRow();
                dr["Collection_Batch_Name"] = this.GetResourceTextByKey("Key_PleaseSelectBatch");
                dr["Batch_Id"] = -1;
                dtBatch.Rows.InsertAt(dr, 0);
                if (dtBatch.Rows.Count > 0)
                {
                    cboBatchNo.DataSource = dtBatch;
                }
                cboBatchNo.DisplayMember = "Collection_Batch_Name";
                cboBatchNo.ValueMember = "Batch_Id";
                cboBatchNo.SelectedValue = -1;
               
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void LoadSiteCombo()
        {
            CollectionBasedLiquidation objLiquidationBiz = new CollectionBasedLiquidation();
            try
            {
                DataTable dtSiteCode = objLiquidationBiz.GetSiteCodeForLiquidation(AppEntryPoint.Current.UserId, "LiquidationType");
                DataRow dr = dtSiteCode.NewRow();
                dr["DisplayName"] = this.GetResourceTextByKey("Key_PleaseSelectSite");
                dr["Site_Code"] = -1;
                dtSiteCode.Rows.InsertAt(dr, 0);
                if (dtSiteCode.Rows.Count > 0)
                {
                    cboSiteCode.DataSource = dtSiteCode;
                }
                cboSiteCode.DisplayMember = "DisplayName";
                cboSiteCode.ValueMember = "Site_Code";
                cboSiteCode.SelectedValue = -1;
             

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }


        private void btnPerformLiquidation_Click(object sender, EventArgs e)
        {
            try
            {
                int Batch_Id = 0;
                int Site_Id = 0;
                int Site_Code = 0;
                if (cboSiteCode.SelectedIndex > 0 && cboBatchNo.SelectedIndex > 0)
                {
                    Batch_Id = Convert.ToInt32(cboBatchNo.SelectedValue.ToString());
                    Site_Code = Convert.ToInt32(cboSiteCode.SelectedValue.ToString());
                    string[] site=cboBatchNo.Text.Split('-');
                    int iSiteBatchId = Convert.ToInt32(site[0]);
                    DataRowView drView = cboSiteCode.SelectedItem as DataRowView;
                    if (drView != null)
                        Site_Id = Convert.ToInt32(drView["Site_ID"]);

                    if (Site_Code > 0 && Batch_Id > 0)
                    {
                        if (cboBatchNo.SelectedIndex == 1)
                        {
                            LiquidationDetails liquidationDetailsPS = new LiquidationDetails(Site_Id, Site_Code, Batch_Id, iSiteBatchId);
                            liquidationDetailsPS.Owner = this;
                            liquidationDetailsPS.ShowDialog();
                            LoadSiteCombo();
                            LoadBatchNo();
                        }
                        else
                        {
                            this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_LIQUIDATION_PERFORM_SEQUENTIALLY"), this.Text);                         
                        }
                    }
                    else
                    {
                        this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_LIQUIDATION_VALID_BATCH"), this.Text);                        
                        return;
                    }
                }
                else
                {
                    this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_LIQUIDATION_VALID_BATCH"), this.Text);
                    return;
                }

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

        private void frmLiquidationBatch_Load(object sender, EventArgs e)
        {
            this.ResolveResources(); 
        }

        private void cboSiteCode_SelectedIndexChanged(object sender, EventArgs e)
        {

           LogManager.WriteLog("Liquidation Batch-Loading Batch Combo Starts", LogManager.enumLogLevel.Info);
           int SelectedSiteValue = 0;
           bool val = Int32.TryParse(cboSiteCode.SelectedValue.ToString(),out SelectedSiteValue);
           LoadBatchNo();
           LogManager.WriteLog("Liquidation Batch-Loading Batch Combo Ends", LogManager.enumLogLevel.Info);
        }

    }
}
