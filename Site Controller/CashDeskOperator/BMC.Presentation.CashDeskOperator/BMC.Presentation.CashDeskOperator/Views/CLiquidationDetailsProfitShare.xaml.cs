using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BMC.CashDeskOperator.BusinessObjects;
using BMC.CashDeskOperator;
using BMC.Transport;
using System.Text.RegularExpressions;
using BMC.Common.ExceptionManagement;
using System.Data;
using BMC.Common.LogManagement;
using BMC.DBInterface.CashDeskOperator;
using BMC.CommonLiquidation.Utilities;
using Audit.BusinessClasses;
using Audit.Transport;
using BMC.Common.Utilities;
using BMC.Business.CashDeskOperator;

namespace BMC.Presentation.POS.Views
{
    /// <summary>
    /// Interaction logic for CLiquidationDetails.xaml
    /// </summary>
    public partial class CLiquidationDetailsProfitShare
    {
        private int _BatchNo;
        private int _iSiteId;
        private CommonLiquidationEntity _entity = null;
        private decimal NetShareValue = 0;

        public CLiquidationDetailsProfitShare()
        {
            InitializeComponent();
        }

        public CLiquidationDetailsProfitShare(int BatchNo)
        {
            _BatchNo = BatchNo;
            InitializeComponent();
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

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            FetchLiquidation();
            LoadLiquidationDetails();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Audit(bool success,int iBatchNo)
        {
            AuditViewerBusiness.InsertAuditData(new Audit.Transport.Audit_History
                            {
                                //Audit_ID = Convert.ToInt32( ModuleID.LiquidationForProfitShare),
                                AuditModuleName = ModuleName.CollectionBasedLiquidation,
                                Audit_Screen_Name = "CollectionBasedLiquidation | CollectionBasedLiquidation",
                                Audit_Field = "CollectionBasedLiquidation",
                                Audit_Desc = "CollectionBasedLiquidation for Batch No: " + iBatchNo + ",Collection Performed Date: " + _entity.Liquidation_Date + " is Completed.",
                                AuditOperationType = OperationType.ADD
                            });
        }

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtAdvanceToRetailer.Text))
                {
                    MessageBox.ShowBox("MessageID370", BMC_Icon.Information);
                    return;
                }

                decimal dAdvanceToRetailer = 0.0M;
                if (!CheckForValidAdvanceToRetailValue(out dAdvanceToRetailer))
                {
                    MessageBox.ShowBox("MessageID434", BMC_Icon.Information);
                    return;
                }

                System.Windows.Forms.DialogResult dr = MessageBox.ShowBox("MessageID371", BMC_Icon.Question, BMC_Button.YesNo);

                if (dr.ToString() == "Yes")
                {
                    //Update advance retailer value
                    //UpdateAdvanceRetailer();

                    //Calculate the retailer negative net

                    //CalculateRetailerNegative();
                    //CalculateCarriedForwardAmount();

                    if (Entity.ProfitShareGroupId > 0)
                    {

                        //Sp call for profit share details
                        CommonLiquidationDataContext objCommonLiquidation = new CommonLiquidationDataContext(CommonUtilities.ExchangeConnectionString);
                        int LiquidationProfitShareResult = objCommonLiquidation.PerformLiquidationForProfitShare(null, 
                                                                                                                    _BatchNo, 
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

                        //Report
                        MessageBox.ShowBox("MessageID488", BMC_Icon.Information);
                        //calling Audit Method
                        Audit(true,_BatchNo);
                        System.Windows.Forms.DialogResult res = MessageBox.ShowBox("MessageID491", BMC_Icon.Question, BMC_Button.YesNo);
                        if (res == System.Windows.Forms.DialogResult.Yes)
                        {
                            using (CReportViewer cReportViewer = new CReportViewer())
                            {
                                LogManager.WriteLog("Report data fetched successfully from database", LogManager.enumLogLevel.Info);
                                cReportViewer.ShowLiquidationReportForRead(_BatchNo, null);
                                cReportViewer.ShowDialog();
                            }
                        }
                    }
                    else
                    {
                        MessageBox.ShowBox("MessageID490", BMC_Icon.Information);
                        return;
                    }
                    this.Close();
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

        private void FetchLiquidation()
        {

            CommonLiquidationDataContext objCommonLiquidation = new CommonLiquidationDataContext(CommonUtilities.ExchangeConnectionString);
            List<CommonCollectionLiquidation> details = objCommonLiquidation.GetLiquidationSummary(_BatchNo, null).ToList();

            foreach (var liquiddetail in details)
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
            }
        }

        private void LoadLiquidationDetails()
        {
            try
            {
                txtDateCollected.Text = _entity.Liquidation_Date.ToString();
                txtRetailerName.Text = _entity.Retailer_Name;
                txtGross.Text = Convert.ToDecimal(_entity.Gross).ToString("#,##0.00");
                tblNetValue.Text = "Net * " + _entity.Percentage_Setting.GetValueOrDefault().ToString("#,##0.00");
                txtNet.Text = Convert.ToDecimal(_entity.Net).ToString("#,##0.00");
                txtNetValue.Text = Convert.ToDecimal((_entity.Net_Percentage)).ToString("#,##0.00");
                txtBalanceDue.Text = Convert.ToDecimal(_entity.Balance_Due).ToString("#,##0.00");
                txtRetailer.Text = Convert.ToDecimal(_entity.Retailer).ToString("#,##0.00");
                txtRetailernegNet.Text = Convert.ToDecimal(_entity.Retailer_Negative_Net).ToString("#,##0.00");
                txtTicketsExpected.Text = Convert.ToDecimal(_entity.Tickets_Expected).ToString("#,##0.00");
                txtTicketsPaid.Text = Convert.ToDecimal(_entity.Tickets_Paid).ToString("#,##0.00");
                txtAdvanceToRetailer.Text = Convert.ToDecimal(_entity.Advance_To_Retailer).ToString("#,##0.00");
                txtRetailerShare.Text = Convert.ToDecimal(_entity.Retailer_Share).ToString("#,##0.00");
                txtFixedExpenseShareAmount.Text = Convert.ToDouble(_entity.FixedExpenseAmount).ToString("#,##0.00");
                txtRetailerSharebeforeFixedExpense.Text = Convert.ToDecimal(_entity.RetailerShareBeforeFixedExpense).ToString("#,##0.00");
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void txtAdvanceToRetailer_TextChanged(object sender, RoutedEventArgs e)
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

        private void txtAdvanceToRetailer_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (!BMC.Transport.Settings.OnScreenKeyboard)
                    return;

                string sValue;
                sValue = DisplayNumberPad(((TextBox)sender).Text);
                ((TextBox)sender).Text = sValue;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void txtAdvanceToRetailer_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                e.Handled = !CustomerDetailsConstants.AllowedNumerics.Contains(e.Key);

                //if (e.Key == Key.OemComma)
                //    e.Handled = false;

                //if (e.Key == Key.OemPeriod || e.Key == Key.Decimal)
                //    e.Handled = false;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        //private void UpdateAdvanceRetailer()
        //{
        //    txtAdvanceToRetailer.Text = string.IsNullOrEmpty(txtAdvanceToRetailer.Text) ? "0.00" : txtAdvanceToRetailer.Text;
        //    ILiquidationDetails liquid = LiquidationBusinessObject.CreateInstance();

        //    //On Calculating the values (based on AdvanceToRetailer value), if the AdvanceToRetailer having some invalid value then avoid to update
        //    //AdvanceToRetail Value to the DB also skip the calculations.
        //    decimal dAdvanceToRetailer = 0.0M;
        //    if (CheckForValidAdvanceToRetailValue(out dAdvanceToRetailer))
        //    {
        //        liquid.UpdateBatchAdvance(_BatchNo, dAdvanceToRetailer);                
        //    }
        //}

        private bool CheckForValidAdvanceToRetailValue(out decimal dAdvanceToRetailer)
        {
            return Decimal.TryParse(txtAdvanceToRetailer.Text, out dAdvanceToRetailer);
        }

        //private void CalculateRetailerNegative()
        //{
        //    ILiquidationDetails liquid = LiquidationBusinessObject.CreateInstance();
        //    liquid.CalculateRetailerNegative(_BatchNo);
        //}

        private string DisplayNumberPad(string keytext)
        {
            string strNumberPadText = string.Empty;
            NumberPadWind ObjNumberpadWind = new NumberPadWind(true);
            ObjNumberpadWind.isPlayerClub = true;

            try
            {
                ObjNumberpadWind.ValueText = keytext;

                if (ObjNumberpadWind.ShowDialog() == true)
                {
                    if (ObjNumberpadWind.ValueText == "")
                    {
                        strNumberPadText = "0.00";
                    }
                    else
                    {
                        strNumberPadText = ObjNumberpadWind.ValueText;
                    }
                }
                else
                {
                    strNumberPadText = "0.00";
                }
            }
            catch (Exception ex)
            {
                strNumberPadText = ObjNumberpadWind.ValueText;
                ObjNumberpadWind.Close();
                ExceptionManager.Publish(ex);
            }
            return strNumberPadText;
        }

        private void btnProfitShare_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _entity.Advance_To_Retailer = Convert.ToDecimal(txtAdvanceToRetailer.Text);
                var objProfitShare = new CProfitShare(_entity, _iSiteId, _BatchNo);
                objProfitShare.ShowDialog();
                LoadLiquidationDetails();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
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

    }
}
