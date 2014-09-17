using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BMC.EnterpriseClient.Helpers;
using BMC.CommonLiquidation.Utilities;
using BMC.Common.ExceptionManagement;
using BMC.EnterpriseBusiness.Business;
using Audit.BusinessClasses;
using Audit.Transport;
using BMC.Common.Utilities;
using BMC.Common;
using BMC.Reports;

namespace BMC.EnterpriseClient.Views
{
    public partial class frmReadLiquidation : GenericFormBase
    {
        #region DataMember

        private CommonLiquidationEntity objCommonLiquidation = null;
        private ReadBasedLiquidationBiz objReadBasedLiquidationBiz = new ReadBasedLiquidationBiz();
        private int iSiteId = 0;
        private string sitecode = string.Empty;
        private string ReadDate = string.Empty;
        private string _SiteName = string.Empty;
        #endregion //DataMember

        #region Constructor

        public frmReadLiquidation(int iSiteId, List<CommonLiquidationEntity> lstCommonLiquidation,string Sitecode,string sReadDate,string SiteName)
        {
            try
            {
                this.iSiteId = iSiteId;
                sitecode = Sitecode;
                objCommonLiquidation = lstCommonLiquidation[0];
                ReadDate = sReadDate;
                _SiteName = SiteName;
                InitializeComponent();
                SetTagProperty();
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        #endregion //Constructor

        #region Events

        private void frmReadLiquidation_Load(object sender, EventArgs e)
        {
            this.ResolveResources();
            LoadLiquidationDetails();
            //objDatawatcher = new Helpers.Datawatcher(this);
        }

        private void btnProfitShare_Click(object sender, EventArgs e)
        {
            try
            {
                frmProfitShare objfrmProfitShare = new frmProfitShare(iSiteId, objCommonLiquidation);
                objfrmProfitShare.ShowDialog(this);
                objCommonLiquidation = objfrmProfitShare.objCommonLiquidation;
                LoadLiquidationDetails();
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void Audit(bool success,int? iReadNo,string Liquidationdate)
        {
            AuditViewerBusiness business = new AuditViewerBusiness(DatabaseHelper.GetConnectionString());
            {
                business.InsertAuditData(new Audit.Transport.Audit_History
                {
                    EnterpriseModuleName = ModuleNameEnterprise.ReadBasedLiquidation,
                    Audit_Screen_Name = "ReadBasedLiquidation",
                    Audit_Desc = "ReadBasedLiquidation for Site: " + sitecode + ",Read No: " + iReadNo + ",Read Performed Date: " + ReadDate + " is Completed.",
                    Audit_Field ="ReadBasedLiquidation",
                    Audit_User_ID = AppEntryPoint.Current.UserId,
                    Audit_User_Name = AppEntryPoint.Current.UserName,
                    AuditOperationType = OperationType.ADD,                   
                }, false);
            }
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                if (objCommonLiquidation.ProfitShareGroupId <= 0)
                {
                    this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_SELECT_PROFIT_SHARE_GRP"), this.Text);
                    return;
                }

                if (string.IsNullOrEmpty(txtAdvanceToRetailer.Text))
                {
                    this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_ENTER_ADVANCE_RETAILER_VALUE"), this.Text);
                    return;
                }

                decimal dAdvanceToRetailer = 0.0M;
                if (!CheckForValidAdvanceToRetailValue(out dAdvanceToRetailer))
                {
                    this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_ENTER_VALID_ADVANCE_RETAILER_VALUE"), this.Text);
                    return;
                }

                if (this.ShowQuestionMessageBox(this.GetResourceTextByKey(1, "MSG_WANT_TO_PROCEED"),this.Text) == DialogResult.Yes)
                {

                    //Calculate the retailer negative net
                    //CalculateRetailerNegative();
                    
                    if (objReadBasedLiquidationBiz.SaveLiquidation(iSiteId, objCommonLiquidation) == 0)
                    {
                        this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_LIQUIDATION_COMPLETED"), this.Text);
                        //calling Audit
                        Audit(true,objCommonLiquidation.Read_No,objCommonLiquidation.Liquidation_Date.ToString());
                        if (this.ShowQuestionMessageBox(this.GetResourceTextByKey(1, "MSG_WANT_TO_PRINT_RPT"), this.Text) == DialogResult.Yes)
                        {                           
                            clsSPParams spParams = new clsSPParams();
                            spParams.BatchId = 0;
                            spParams.ReadId = objCommonLiquidation.Read_No.GetValueOrDefault();
                            spParams.SiteName = _SiteName;
                            BMC.ReportViewer.RDLReportViewer.Instance.LoadReport("rsp_Report_GetLiquidationDetail", this.GetResourceTextByKey("Key_RT_ReportLiquidationSummary_ReadBased"), "ENT_LiquidationSummary_PS", spParams, false);                           
                        }
                    }

                    else
                    {
                        this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_ERROR_OCCURED_LIQUIDATION"), this.Text);
                        return;
                    }
                    //.DataModify = false;
                    this.Close();
                }
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void txtAdvanceToRetailer_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtAdvanceToRetailer.Text = string.IsNullOrEmpty(txtAdvanceToRetailer.Text) ? "0.00" : txtAdvanceToRetailer.Text;
                objCommonLiquidation.Advance_To_Retailer = Convert.ToDecimal(txtAdvanceToRetailer.Text);
                txtBalanceDue.Text = Convert.ToDecimal(objCommonLiquidation.Balance_Due).ToString("#,##0.00");
                txtRetailer.Text = Convert.ToDecimal(objCommonLiquidation.Retailer).ToString("#,##0.00");
                txtRetailerShareBeforeFixedExpense.Text = objCommonLiquidation.RetailerShareBeforeFixedExpense.GetValueOrDefault().ToString("#,##0.00");
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        #endregion //Events

        #region Methods

        /// <summary>
        /// Assigns the Resource Key names to the controls--Created by kishore sivagnanam
        /// </summary>
        public void SetTagProperty()
        {
            this.btnCancel.Tag = "Key_CancelCaption";
            this.btnConfirm.Tag = "Key_ConfirmCaption";
            this.btnProfitShare.Tag = "Key_ProfitShareCaption";
            this.lblAdvancedToRetailer.Tag = "Key_AdvanceToRetailerColon";
            this.lblBalanceDue.Tag = "Key_BalanceDueColon";
            this.lblDate.Tag = "Key_DateColon";
            this.lblfixedexpense.Tag = "Key_ExpenseShareColon";
            this.lblNet.Tag = "Key_NetColon";
            this.lblNetValue.Tag = "Key_NetMandatoryColon";
            this.lblRetailer.Tag = "Key_RetailerColon";
            this.lblRetailerName.Tag = "Key_RetailerNameColon";
            this.lblRetailerSharebeforeFixedExpense.Tag = "Key_RetailerShareBeforeFixedExpenseColon";
            this.lblRetailerShare.Tag = "Key_RetailerShareColon";
            this.lblRetailersNegativeNet.Tag = "Key_RetailersNegativeNetPriorRead";
            this.Tag = "Key_RetailersLiquidationDetails";
            this.lblTicketsPaid.Tag = "Key_VouchersPaidColon";
            this.lblGross.Tag = "Key_TotalMeterInColon";
            this.lblTicketsExpected.Tag = "Key_TotalMeterOutColon";

        }


        private void LoadLiquidationDetails()
        {
            try
            {
                txtDate.Text = objCommonLiquidation.Liquidation_Date.ToString();
                txtRetailerName.Text = objCommonLiquidation.Retailer_Name;
                txtGross.Text = objCommonLiquidation.Gross.GetValueOrDefault().ToString("#,##0.00");
                txtNet.Text = objCommonLiquidation.Net.GetValueOrDefault().ToString("#,##0.00");
                lblNetValue.Text = this.GetResourceTextByKey("Key_NetStar") +" "+ (objCommonLiquidation.Percentage_Setting.GetValueOrDefault()).ToString("#,##0.00");
                txtBalanceDue.Text = objCommonLiquidation.Balance_Due.GetValueOrDefault().ToString("#,##0.00");
                txtRetailer.Text = objCommonLiquidation.Retailer.GetValueOrDefault().ToString("#,##0.00");
                txtRetailerNegNet.Text = objCommonLiquidation.Retailer_Negative_Net.GetValueOrDefault().ToString("#,##0.00");
                txtTicketsExpected.Text = objCommonLiquidation.Tickets_Expected.GetValueOrDefault().ToString("#,##0.00");
                txtTicketsPaid.Text = objCommonLiquidation.Tickets_Paid.GetValueOrDefault().ToString("#,##0.00");
                txtAdvanceToRetailer.Text = objCommonLiquidation.Advance_To_Retailer.GetValueOrDefault().ToString("#,##0.00");
                txtRetailerShare.Text = objCommonLiquidation.Retailer_Share.GetValueOrDefault().ToString("#,##0.00");
                txtNetValue.Text = objCommonLiquidation.Net_Percentage.GetValueOrDefault().ToString("#,##0.00");
                txtFixedExpense.Text = objCommonLiquidation.FixedExpenseAmount.GetValueOrDefault().ToString("#,##0.00");
                txtRetailerShareBeforeFixedExpense.Text = objCommonLiquidation.RetailerShareBeforeFixedExpense.GetValueOrDefault().ToString("#,##0.00");
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        //private void UpdateAdvanceRetailer()
        //{
        //    try
        //    {
        //        txtAdvanceToRetailer.Text = string.IsNullOrEmpty(txtAdvanceToRetailer.Text) ? "0.00" : txtAdvanceToRetailer.Text;

        //        decimal dAdvanceToRetailer = 0.0M;
        //        if (CheckForValidAdvanceToRetailValue(out dAdvanceToRetailer))
        //        {
        //            objCommonLiquidation.Advance_To_Retailer = dAdvanceToRetailer;
        //            //CalculateBalanceDue();
        //            LoadLiquidationDetails();
        //        }
        //    }

        //    catch (Exception ex)
        //    {
        //        ExceptionManager.Publish(ex);
        //    }
        //}

        private bool CheckForValidAdvanceToRetailValue(out decimal dAdvanceToRetailer)
        {
            try
            {
                return Decimal.TryParse(txtAdvanceToRetailer.Text, out dAdvanceToRetailer);
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            dAdvanceToRetailer = 0.0M;
            return false;
        }

        //private void CalculateRetailerNegative()
        //{
        //    try
        //    {
        //        objCommonLiquidation.Negative_Net = objReadBasedLiquidationBiz.CalculateRetailerNegativeNet(iSiteId, Convert.ToDecimal(objCommonLiquidation.Percentage_Setting));
        //    }

        //    catch (Exception ex)
        //    {
        //        ExceptionManager.Publish(ex);
        //    }
        //}

        #endregion //Methods

    }
}
