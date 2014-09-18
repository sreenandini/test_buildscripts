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
using BMC.Common.LogManagement;
using BMC.CommonLiquidation.Utilities;
using BMC.Presentation.POS.Views;
using Audit.BusinessClasses;
using Audit.Transport;
using BMC.Presentation.POS.Helper_classes;


namespace BMC.Presentation
{
    /// <summary>
    /// Interaction logic for CLiquidationDetails.xaml
    /// </summary>
    public partial class CReadLiquidation //UserControl
    {
        #region DataMembers
        
        private CommonLiquidationEntity objCommonLiquidation = null;
        private ReadLiquidationConfiguration oReadLiquidationConfiguration = ReadLiquidationConfiguration.ReadLiquidationConfigurationInstance;
        private string sReadDate = string.Empty;
        #endregion //DataMembers

        #region Constructor

        public CReadLiquidation()
        {
            InitializeComponent();
        }

        public CReadLiquidation(List<CommonLiquidationEntity> lstCommonLiquidation,string ReadDate)
        {
            try
            {
                objCommonLiquidation = lstCommonLiquidation[0];
                sReadDate = ReadDate;
                InitializeComponent();
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        #endregion //Constructor

        

        #region Events

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadLiquidationDetails();
        }
        private void Audit(bool success, int? iReadNo)
        {            
            AuditViewerBusiness.InsertAuditData(new Audit.Transport.Audit_History
            {
                AuditModuleName = ModuleName.ReadBasedLiquidation,
                Audit_Screen_Name = "ReadBasedLiquidation | ReadBasedLiquidation",
                Audit_Field = "ReadBasedLiquidation",
                Audit_Desc = "ReadBasedLiquidation for Read No: " + iReadNo + " ,Read Performed Date: " + sReadDate + " is Completed.",
                AuditOperationType = OperationType.ADD
            });
        }
        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (objCommonLiquidation.ProfitShareGroupId <= 0)
                {
                    MessageBox.ShowBox("MessageID490", BMC_Icon.Information);
                    return;
                }

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
                    //Calculate the retailer negative net

                    CalculateRetailerNegative();
                    if (oReadLiquidationConfiguration.SaveLiquidation(objCommonLiquidation) == 0)
                    {
                        MessageBox.ShowBox("MessageID488", BMC_Icon.Information);
                        Audit(true, objCommonLiquidation.Read_No);

                        System.Windows.Forms.DialogResult res = MessageBox.ShowBox("MessageID251", BMC_Icon.Question, BMC_Button.YesNo);
                        if (res == System.Windows.Forms.DialogResult.Yes)
                        {
                            using (CReportViewer cReportViewer = new CReportViewer())
                            {
                                LogManager.WriteLog("Report data fetched successfully from database", LogManager.enumLogLevel.Info);

                                cReportViewer.ShowLiquidationReportForRead(null, objCommonLiquidation.Read_No);
                                cReportViewer.SetOwner(this);
                                

                                cReportViewer.Show();
                            }
                        }
                    }

                    else
                    {
                        MessageBox.ShowBox("MessageID489", BMC_Icon.Information);
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

        private void PreviewMouseUp(object sender, MouseButtonEventArgs e)
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

        private void btnProfitShare_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var objProfitShare = new CProfitShare(objCommonLiquidation);
                objProfitShare.Owner = this;
                objProfitShare.ShowDialog();
                this.objCommonLiquidation = objProfitShare.objCommonLiquidation;
                LoadLiquidationDetails();
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void txtAdvanceToRetailer_TextChanged(object sender, RoutedEventArgs e)
        {
            UpdateAdvanceRetailer();
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

        #endregion //Events

        #region Methods

        private void LoadLiquidationDetails()
        {
            try
            {
                txtDate.Text = objCommonLiquidation.Liquidation_Date.ToString();
                txtRetailerName.Text = objCommonLiquidation.Retailer_Name;
                txtGross.Text = objCommonLiquidation.Gross.GetValueOrDefault().ToString("#,##0.00");
                txtNet.Text = objCommonLiquidation.Net.GetValueOrDefault().ToString("#,##0.00");
                txtblkNetValue.Text = "Net * " + (objCommonLiquidation.Percentage_Setting.GetValueOrDefault()).ToString("#,##0.00");
                txtBalanceDue.Text = objCommonLiquidation.Balance_Due.GetValueOrDefault().ToString("#,##0.00");
                txtRetailer.Text = objCommonLiquidation.Retailer.GetValueOrDefault().ToString("#,##0.00");
                txtRetailernegNet.Text = objCommonLiquidation.Retailer_Negative_Net.GetValueOrDefault().ToString("#,##0.00");
                txtTicketsExpected.Text = objCommonLiquidation.Tickets_Expected.GetValueOrDefault().ToString("#,##0.00");
                txtTicketsPaid.Text = objCommonLiquidation.Tickets_Paid.GetValueOrDefault().ToString("#,##0.00");
                txtAdvanceToRetailer.Text = objCommonLiquidation.Advance_To_Retailer.GetValueOrDefault().ToString("#,##0.00");
                txtFixedExpense.Text = objCommonLiquidation.FixedExpenseAmount.GetValueOrDefault().ToString("#,##0.00");
                txtRetailerShare.Text = objCommonLiquidation.Retailer_Share.GetValueOrDefault().ToString("#,##0.00");
                txtNetValue.Text = objCommonLiquidation.Net_Percentage.GetValueOrDefault().ToString("#,##0.00");
                txtRetailerSharebeforeFixedExpense.Text = objCommonLiquidation.RetailerShareBeforeFixedExpense.GetValueOrDefault().ToString("#,##0.00");
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void UpdateAdvanceRetailer()
        {
            try
            {
                txtAdvanceToRetailer.Text = string.IsNullOrEmpty(txtAdvanceToRetailer.Text) ? "0.00" : txtAdvanceToRetailer.Text;

                decimal dAdvanceToRetailer = 0.0M;
                if (CheckForValidAdvanceToRetailValue(out dAdvanceToRetailer))
                {
                    objCommonLiquidation.Advance_To_Retailer = dAdvanceToRetailer;
                    LoadLiquidationDetails();
                }
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

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

        private void CalculateRetailerNegative()
        {
            try
            {
                objCommonLiquidation.Negative_Net = oReadLiquidationConfiguration.CalculateRetailerNegativeNet(Convert.ToDecimal(objCommonLiquidation.Percentage_Setting));
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

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

        #endregion //Methods
    }
}
