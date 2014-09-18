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

namespace BMC.Presentation.POS.Views
{
    /// <summary>
    /// Interaction logic for CLiquidationDetails.xaml
    /// </summary>
    public partial class CLiquidationDetails 
    {
        private int _BatchNo;
        public CLiquidationDetails()
        {
            InitializeComponent();
        }

        public CLiquidationDetails(int BatchNo)
        {
            _BatchNo = BatchNo;
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            LoadLiquidationDetails();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
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
                UpdateAdvanceRetailer();

                //Calculate the retailer negative net

                CalculateRetailerNegative();

                var oReports = new CollectionBatchReports(_BatchNo,this);
                oReports.ShowDialog();
            }
            
        }

        private void LoadLiquidationDetails()
        {
            ILiquidationDetails liquid = LiquidationBusinessObject.CreateInstance();

            List<LiquidationSummary> details = liquid.GetLiquidationSummaryDetails(_BatchNo);

            foreach (LiquidationSummary liquiddetail in details)
            {
                txtDateCollected.Text = liquiddetail.Date_Collected.ToString();
                txtRetailerName.Text = liquiddetail.Retailer_Name;
                txtGross.Text = liquiddetail.Gross.ToString("#,##0.00");
                txtNet.Text = liquiddetail.Net.ToString("#,##0.00");
                tblNetValue.Text = "Net * " + liquiddetail.Percentage_Setting.ToString();
                txtBalanceDue.Text = liquiddetail.Balance_Due.ToString("#,##0.00");
                txtRetailer.Text = liquiddetail.Retailer.ToString("#,##0.00");
                txtRetailernegNet.Text = liquiddetail.Retail_Negative_Net.ToString("#,##0.00");
                txtTicketsExpected.Text = liquiddetail.Tickets_Expected.ToString("#,##0.00");
                txtTicketsPaid.Text = liquiddetail.Tickets_Paid.ToString("#,##0.00");
                txtAdvanceToRetailer.Text = liquiddetail.Advance_To_Retailer.ToString("#,##0.00");
                txtRetailerShare.Text = liquiddetail.Retailer_Share.ToString("#,##0.00");
                txtNetValue.Text=liquiddetail.Net_Percentage.ToString("#,##0.00");
            }
        }

        //Balance Due not calculate based on the AdvanceToRetailer value. The calculation would happen at the time of AdvanceToRetailer LostFocus,
        //but the focus is not get out to other controls from the AdvanceToRetailer control. So changed the event Lostfocus to TextChanged
        //private void txtAdvanceToRetailer_LostFocus(object sender, RoutedEventArgs e)
        //{
        //    UpdateAdvanceRetailer();
        //    LoadLiquidationDetails();
        //}
        
        private void txtAdvanceToRetailer_TextChanged(object sender, RoutedEventArgs e)
        {
            txtAdvanceToRetailer.Text = string.IsNullOrEmpty(txtAdvanceToRetailer.Text) ? "0.00" : txtAdvanceToRetailer.Text;
            txtBalanceDue.Text = (Convert.ToDecimal(txtAdvanceToRetailer.Text) + Convert.ToDecimal(txtNet.Text)).ToString();   
        }

        private void UpdateAdvanceRetailer()
        {
            txtAdvanceToRetailer.Text = string.IsNullOrEmpty(txtAdvanceToRetailer.Text) ? "0.00" : txtAdvanceToRetailer.Text;
            ILiquidationDetails liquid = LiquidationBusinessObject.CreateInstance();

            //On Calculating the values (based on AdvanceToRetailer value), if the AdvanceToRetailer having some invalid value then avoid to update
            //AdvanceToRetail Value to the DB also skip the calculations.
            decimal dAdvanceToRetailer = 0.0M;
            if (CheckForValidAdvanceToRetailValue(out dAdvanceToRetailer))
            {
                liquid.UpdateBatchAdvance(_BatchNo, dAdvanceToRetailer);
                LoadLiquidationDetails();
            }
        }

        private bool CheckForValidAdvanceToRetailValue(out decimal dAdvanceToRetailer)
        {
            return Decimal.TryParse(txtAdvanceToRetailer.Text, out dAdvanceToRetailer);
        }

        private void CalculateRetailerNegative()
        {
            ILiquidationDetails liquid = LiquidationBusinessObject.CreateInstance();
            liquid.CalculateRetailerNegative(_BatchNo);
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

        private void PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (!BMC.Transport.Settings.OnScreenKeyboard)
                return;

            string sValue;
            sValue = DisplayNumberPad(((TextBox)sender).Text);
            ((TextBox)sender).Text = sValue;
        }
    }
}
