using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BMC.CommonLiquidation;
using BMC.Common;
using BMC.Common.Utilities;
using BMC.Common.ExceptionManagement;
using BMC.CommonLiquidation.Utilities;
using BMC.EnterpriseBusiness.Business;
using Audit.BusinessClasses;
using Audit.Transport;
using BMC.CoreLib.Win32;
using BMC.Common;
using BMC.Reports;
using BMC.EnterpriseBusiness.Entities;

namespace BMC.EnterpriseClient.Views
{
    public partial class LiquidationDetails : Form
    {
        private int _iBatchNo = 0;
        private int _iSiteId = 0;
        private int _iSiteCode = 0;
        private int _iSiteBatchId = 0;
        private CommonLiquidationEntity _entity = null;

        private LiquidationDetails()
        {
            InitializeComponent();
            SetTagProperty();
        }

        public CommonLiquidationEntity Entity
        {
            get
            {
                return _entity;
            }
            set
            {
                _entity = value;
            }
        }

        public LiquidationDetails(int iSiteId, int iSiteCode, int iBatchNo, int SiteBatchId):this()
        {
            _iSiteId = iSiteId;
            _iBatchNo = iBatchNo;
            _iSiteCode = iSiteCode;
            _iSiteBatchId = SiteBatchId;
            //InitializeComponent();
        }

        /// <summary>
        /// Assigns the Resource Key names to the controls--Created by kishore sivagnanam
        /// </summary>
        public void SetTagProperty()
        {
            this.lblAdvancetoRetailer.Tag = "Key_AdvanceToRetailerColon";
            this.lblBalanceDue.Tag = "Key_BalanceDueColon";
            this.btnCancel.Tag = "Key_Close";
            this.btnConfirm.Tag = "Key_ConfirmCaption";
            this.lblDateCollected.Tag = "Key_DateCollectedColon";
            this.lblFixedExpense.Tag = "Key_ExpenseShareColon";
            this.lblNet22.Tag = "Key_NetMandatoryColon";
            this.lblNet.Tag = "Key_NetColon";
            this.btnProfitShare.Tag = "Key_ProfitShareCaption";
            this.lblRetailerName.Tag = "Key_RetailerNameColon";
            this.lblRetailerSharebeforeFixedExpense.Tag = "Key_RetailerShareBeforeFixedExpenseColon";
            this.lblRetailerShare.Tag = "Key_RetailerShareColon";
            this.lblRetailer.Tag = "Key_RetailerColon";
            this.Tag = "Key_RetailersLiquidationDetails";
            this.lblRetailersNegativeNet.Tag = "Key_RetailersNegativeNetPriorCollection";
            this.lblTicketsPaid.Tag = "Key_VoucherPaid";
            this.lblGross.Tag = "Key_TotalDeclaredInColon";
            this.lblTicketsExpected.Tag = "Key_TotalDeclaredOutColon";

        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtAdvanceToRetailer.Text))
                {

                    Win32Extensions.ShowWarningMessageBox(this, this.GetResourceTextByKey(1, "MSG_ENTER_ADVANCE_RETAILER_VALUE"), this.Text);
                    return;
                }

                decimal dAdvanceToRetailer = 0.0M;
                if (!CheckForValidAdvanceToRetailValue(out dAdvanceToRetailer))
                {
                    this.ShowWarningMessageBox(this.GetResourceTextByKey(1, "MSG_ENTER_VALID_ADVANCE_RETAILER_VALUE"), this.Text);
                    return;
                }

                if (this.ShowQuestionMessageBox(this.GetResourceTextByKey(1, "MSG_WANT_TO_PROCEED"), this.Text) == DialogResult.Yes)
                {
                    //Update advance retailer value
                    UpdateAdvanceRetailer();

                    //Calculate the retailer negative net
                    //CalculateRetailerNegative();
                    //CalculateCarriedForwardAmount();

                    if (_entity.ProfitShareGroupId > 0)
                    {

                        //Sp call for profit share details
                        CommonLiquidation.Utilities.CommonLiquidationDataContext objCommonLiquidation = new BMC.CommonLiquidation.Utilities.CommonLiquidationDataContext(Common.Utilities.DatabaseHelper.GetConnectionString());
                        int LiquidationProfitShareResult = objCommonLiquidation.PerformLiquidationForProfitShare(_iSiteId, 
                                                                                                                    _iBatchNo, 
                                                                                                                    _entity.ProfitShareGroupId, 
                                                                                                                    _entity.ExpenseShareGroupID, 
                                                                                                                    _entity.ExpenseShareAmount, 
                                                                                                                    _entity.WriteOffAmount, 
                                                                                                                    _entity.PayPeriodId, 
                                                                                                                    _entity.Gross,
                                                                                                                    _entity.Tickets_Expected, 
                                                                                                                    _entity.Net_Percentage,
                                                                                                                    _entity.Retailer_Negative_Net,
                                                                                                                    _entity.Net_Percentage, 
                                                                                                                    _entity.Tickets_Paid, 
                                                                                                                    _entity.Advance_To_Retailer, 
                                                                                                                    _entity.Retailer_Share, 
                                                                                                                    _entity.Balance_Due, 
                                                                                                                    _entity.Retailer, 
                                                                                                                    _entity.RetailerShareBeforeFixedExpense,
                                                                                                                    _entity.CarriedForwardExpense, 
                                                                                                                    _entity.RetailerExpenseShareAmount,
                                                                                                                    _entity.FixedExpenseAmount, 
                                                                                                                    _entity.PrevCarriedForwardExpense);


                        this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_LIQUIDATION_COMPLETED"), this.Text);
                        AuditViewerBusiness business = new AuditViewerBusiness(DatabaseHelper.GetConnectionString());
                        {

                            business.InsertAuditData(new Audit.Transport.Audit_History
                            {
                                //collection based liquidation 

                                EnterpriseModuleName = ModuleNameEnterprise.CollectionBasedLiquidation,
                                Audit_Screen_Name = "CollectionBasedLiquidation ",
                                Audit_Desc = "CollectionBasedLiquidation for Site: " + _iSiteCode + ",Site Batch No: " + _iSiteBatchId + ",Collection Performed Date: " + _entity.Liquidation_Date + " is Completed.",
                                Audit_User_ID = AppEntryPoint.Current.UserId,
                                Audit_User_Name = AppEntryPoint.Current.UserName,
                                AuditOperationType = OperationType.ADD,
                            }, false);
                        }
                        if (this.ShowQuestionMessageBox(this.GetResourceTextByKey(1, "MSG_WANT_TO_PRINT_RPT"), this.Text) == DialogResult.Yes)
                        {
                            clsSPParams spParams = new clsSPParams();
                            spParams.BatchId = (_iBatchNo == null) ? 0 : _iBatchNo;
                            spParams.ReadId = 0;
                            spParams.SiteName = txtRetailerName.Text;
                            spParams.ReportFilterDateFormat = SettingsEntity.ReportDateTimeFormat;
                            spParams.ReportDataDateAloneFormat = SettingsEntity.ReportDataDateAloneFormat;
                            spParams.ReportDataDateNTimeFormat = SettingsEntity.ReportDataDateNTimeFormat;
                            spParams.ReportPrintDateTimeFormat = SettingsEntity.ReportPrintDateTimeFormat;
                            BMC.ReportViewer.RDLReportViewer.Instance.LoadReport("rsp_Report_GetLiquidationDetail", this.GetResourceTextByKey("Key_RT_ReportLiquidationSummary_CollectionBased"), "ENT_LiquidationSummary_PS", spParams, false);                            
                        }           
						this.Close();             
                    }
                    else
                    {
                        this.ShowWarningMessageBox(this.GetResourceTextByKey(1, "MSG_SELECT_PROFIT_SHARE_GRP"), this.Text);
                        return;
                    }
                }
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        //private void CalculateCarriedForwardAmount()
        //{
        //    try
        //    {
        //        Entity.CarriedForwardExpense = Entity.Retailer_Share.GetValueOrDefault() >= 0 ? 0 : Math.Abs(Entity.Retailer_Share.GetValueOrDefault());
        //    }

        //    catch (Exception ex)
        //    {
        //        ExceptionManager.Publish(ex);
        //    }
        //}

        private void LiquidationDetails_Load(object sender, EventArgs e)
        {
            this.ResolveResources();
            FetchLiquidation();
            LoadLiquidationDetails();
        }

        private void UpdateAdvanceRetailer()
        {
            try
            {
                txtAdvanceToRetailer.Text = string.IsNullOrEmpty(txtAdvanceToRetailer.Text) ? "0.00" : txtAdvanceToRetailer.Text;

                decimal dAdvanceToRetailer = 0.0M;
                if (CheckForValidAdvanceToRetailValue(out dAdvanceToRetailer))
                {
                    LiquidationBusiness.UpdateBatchAdvance(_iBatchNo, Convert.ToSingle(txtAdvanceToRetailer.Text));
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        //private void CalculateRetailerNegative()
        //{
        //    try
        //    {
        //        _entity.Negative_Net = LiquidationBusiness.CalculateRetailerNegative(_iSiteId, _iBatchNo, _entity.Percentage_Setting.GetValueOrDefault());
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

        private void LoadLiquidationDetails()
        {
            try
            {
                txtDateCollected.Text = _entity.Liquidation_Date.ToString();
                txtRetailerName.Text = _entity.Retailer_Name;
                txtGross.Text = Convert.ToDecimal(_entity.Gross).ToString("#,##0.00");
                lblNet22.Text = this.GetResourceTextByKey("Key_NetStar") + " " + _entity.Percentage_Setting.GetValueOrDefault().ToString("#,##0.00");
                txtNet.Text = Convert.ToDecimal(_entity.Net).ToString("#,##0.00");
                txtNetValue.Text = Convert.ToDecimal(_entity.Net_Percentage).ToString("#,##0.00");
                txtBalanceDue.Text = Convert.ToDecimal(_entity.Balance_Due).ToString("#,##0.00");
                txtRetailer.Text = Convert.ToDecimal(_entity.Retailer).ToString("#,##0.00");
                txtRetailerNegativeNet.Text = Convert.ToDecimal(_entity.Retailer_Negative_Net).ToString("#,##0.00");
                txtTicketsExpected.Text = Convert.ToDecimal(_entity.Tickets_Expected).ToString("#,##0.00");
                txtTicketsPaid.Text = Convert.ToDecimal(_entity.Tickets_Paid).ToString("#,##0.00");
                txtAdvanceToRetailer.Text = Convert.ToDecimal(_entity.Advance_To_Retailer).ToString("#,##0.00");
                txtRetailerShare.Text = Convert.ToDouble(_entity.Retailer_Share).ToString("#,##0.00");
                txtFixedExpense.Text = Convert.ToDouble(_entity.FixedExpenseAmount).ToString("#,##0.00");
                txtRetailerSharebeforeFixedExpense.Text = Convert.ToDecimal(_entity.RetailerShareBeforeFixedExpense).ToString("#,##0.00");
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

        }

        private void FetchLiquidation()
        {
            try
            {
                CommonLiquidation.Utilities.CommonLiquidationDataContext objCommonLiquidation = new BMC.CommonLiquidation.Utilities.CommonLiquidationDataContext(Common.Utilities.DatabaseHelper.GetConnectionString());

                List<CommonLiquidation.Utilities.CommonCollectionLiquidation> lstLiquidationSummary = objCommonLiquidation.GetLiquidationSummary(_iBatchNo, _iSiteId).ToList();
                foreach (var liquiddetail in lstLiquidationSummary)
                {
                    if (_entity != null)
                    {
                        _entity.Liquidation_Date = liquiddetail.Liquidation_Date;
                        _entity.Retailer_Name = liquiddetail.Retailer_Name;
                        _entity.Net = liquiddetail.Net;
                        _entity.Net_Percentage = liquiddetail.Net_Percentage;
                        _entity.Balance_Due = liquiddetail.Balance_Due;
                        _entity.Retailer = liquiddetail.Retailer;
                        _entity.Retailer_Negative_Net = liquiddetail.Retailer_Negative_Net;
                        _entity.Tickets_Expected = liquiddetail.Tickets_Expected;
                        _entity.Tickets_Paid = liquiddetail.Tickets_Paid;
                        _entity.Advance_To_Retailer = liquiddetail.Advance_To_Retailer;
                        _entity.Retailer_Share = liquiddetail.Retailer_Share;
                        _entity.RetailerExpenseShareAmount = liquiddetail.RetailerExpenseShareAmount;
                        _entity.FixedExpenseAmount = liquiddetail.FixedExpenseAmount;
                        _entity.CarriedForwardExpense = liquiddetail.CarriedForwardExpense;
                        _entity.PrevCarriedForwardExpense = liquiddetail.PrevCarriedForwardExpense;
                        _entity.RetailerShareBeforeFixedExpense = liquiddetail.RetailerShareBeforeFixedExpense;
                    }
                    else
                    {
                        _entity = liquiddetail;
                    }
                    break;
                }
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btnProfitShare_Click(object sender, EventArgs e)
        {
            try
            {
                _entity.Advance_To_Retailer = Convert.ToDecimal(txtAdvanceToRetailer.Text);
                frmProfitShare objProfitShare = new frmProfitShare(_iSiteId, _entity);
                objProfitShare.ShowDialog();
                Entity = objProfitShare.objCommonLiquidation;
                LoadLiquidationDetails();
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

        private void txtAdvanceToRetailer_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtAdvanceToRetailer.Text = string.IsNullOrEmpty(txtAdvanceToRetailer.Text) ? "0.00" : txtAdvanceToRetailer.Text;
                _entity.Advance_To_Retailer = Convert.ToDecimal(txtAdvanceToRetailer.Text);
                txtBalanceDue.Text = Convert.ToDecimal(_entity.Balance_Due).ToString("#,##0.00");
                txtRetailer.Text = Convert.ToDecimal(_entity.Retailer).ToString("#,##0.00");
                txtRetailerSharebeforeFixedExpense.Text = Convert.ToDecimal(_entity.RetailerShareBeforeFixedExpense).ToString("#,##0.00");
            }
            catch (Exception ex)
            {

                ExceptionManager.Publish(ex);
            }
        }
    }
}

