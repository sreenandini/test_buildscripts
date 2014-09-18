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
using BMC.Business.CashDeskOperator;
using BMC.CashDeskOperator;
using BMC.Common.ExceptionManagement;
using BMC.Transport;
using System.Globalization;
namespace BMC.Presentation.POS.Views
{
    /// <summary>
    /// Interaction logic for VoucherDetails.xaml
    /// </summary>
    public partial class VoucherDetails : Window
    {
        private string _strBarCode=string.Empty;
        private bool _IsVoucherFound = false;
        string DateCulture = BMC.Common.ConfigurationManagement.ConfigManager.Read("GetDefaultCultureForDate");
        public static string CurrentSiteCulture = BMC.Common.ConfigurationManagement.ConfigManager.Read("GetDefaultCultureForRegion");
        public static string CurrentCurrenyCulture = BMC.Common.ConfigurationManagement.ConfigManager.Read("GetDefaultCultureForCurrency");
        public string BarCode
        {
            get
            {
                return _strBarCode;
            }
            set
            {
                _strBarCode = value;
            }
        }

        public bool IsVoucherFound
        {
            get
            {
                return _IsVoucherFound;
            }
            set
            {
                _IsVoucherFound = value;
            }
        }

        public VoucherDetails()
        {
            InitializeComponent();

        }
        public VoucherDetails(string strBarCode)
        {
            InitializeComponent();
            this.BarCode = strBarCode;
            txtSiteCode.IsReadOnly = true;
            txtEffectiveDate.IsReadOnly = true;
            txtExpiryDate.IsReadOnly = true;
            txtBarCode.IsReadOnly = true;
            txtVoucherType.IsReadOnly = true;
            txtPrintedDevice.IsReadOnly = true;
            txtRedeemDevice.IsReadOnly = true;
            txtVoidDevice.IsReadOnly = true;
            txtErrorDevice.IsReadOnly = true;
            txtPrintedDate.IsReadOnly = true;
            txtRedeemDate.IsReadOnly = true;
            txtVoidDate.IsReadOnly = true;
            txtAmount.IsReadOnly = true;
            txtStatus.IsReadOnly = true;
            txtErrorDescription.IsReadOnly = true;
            txtIssuedUserName.IsReadOnly = true;
            txtRedeemUser.IsReadOnly = true;
            txtVoidUser.IsReadOnly = true;
            txtRedeemSiteCode.IsReadOnly = true;
            string CurrencySymbol = new RegionInfo(CurrentSiteCulture).CurrencySymbol;
            lblAmount.Content=lblAmount.Content+" in "+CurrencySymbol+" :";
            GetVoucherInformation();
        }
        public void GetVoucherInformation()
        {
            try
            {
               
                
                Promotional objRedeemBusiness = new Promotional();
                
                var lstVoucherInfo= objRedeemBusiness.GetVoucherInfo(BarCode).ToList();

                if (lstVoucherInfo.Count > 0)
                {

                    foreach (var item in lstVoucherInfo)
                    {
                        txtSiteCode.Text = item.SiteCode.ToString();
                        txtBarCode.Text = item.BarCode;
                        txtVoucherType.Text = item.TicketType;
                        txtPrintedDevice.Text = item.PrintDeviceName;
                        txtRedeemDevice.Text = item.PayDeviceName;
                        txtVoidDevice.Text = item.VoidDeviceName;
                        txtErrorDevice.Text = item.ErrorDeviceName;
                        if (item.RedeemSiteCode.ToString() != "0")
                            txtRedeemSiteCode.Text = item.RedeemSiteCode.ToString();
                        else
                            txtRedeemSiteCode.Text = "-";
                        if (item.PrintedDate.Trim() == "-")
                            txtPrintedDate.Text = item.PrintedDate;
                        else
                            txtPrintedDate.Text = Convert.ToDateTime(item.PrintedDate).ToString(CultureInfo.CreateSpecificCulture(DateCulture));

                        if (item.RedeemedDate.Trim() == "-")
                            txtRedeemDate.Text = item.RedeemedDate;
                        else
                            txtRedeemDate.Text = Convert.ToDateTime(item.RedeemedDate).ToString(CultureInfo.CreateSpecificCulture(DateCulture));

                        if (item.VoidedDate.Trim() == "-")
                            txtVoidDate.Text = item.VoidedDate;
                        else
                            txtVoidDate.Text = Convert.ToDateTime(item.VoidedDate).ToString(CultureInfo.CreateSpecificCulture(DateCulture));


                        if (item.ExpiryDate.Trim() == "-")
                            txtExpiryDate.Text = item.ExpiryDate;
                        else
                            txtExpiryDate.Text = Convert.ToDateTime(item.ExpiryDate).ToString(CultureInfo.CreateSpecificCulture(DateCulture));


                        if (item.EffectiveDate.Trim() == "-")
                            txtEffectiveDate.Text = item.EffectiveDate;
                        else
                            txtEffectiveDate.Text = Convert.ToDateTime(item.EffectiveDate).ToString(CultureInfo.CreateSpecificCulture(DateCulture));


                        txtAmount.Text = item.TicketAmount.ToString();
                        txtStatus.Text = item.VoucherStatus;
                        txtErrorDescription.Text = item.ErrorDescription;
                        txtIssuedUserName.Text = item.VoucherIssuedUser;
                        txtRedeemUser.Text = item.VoucherRedeemedUser;
                        txtVoidUser.Text = item.VoucherVoidUser;
                    }


                    this.IsVoucherFound = true;
                }
                else
                    this.IsVoucherFound = false;
            }
            catch (Exception ex)
            {
                this.IsVoucherFound = false;
                ExceptionManager.Publish(ex);
            }
        }

        private void txtSiteCode_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
