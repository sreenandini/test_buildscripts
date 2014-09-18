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
using System.Data;
using BMC.Transport;
using System.Data.Linq;
using BMC.Transport.CashDeskOperatorEntity;
using BMC.Common.ExceptionManagement;
using System.Globalization;
using BMC.Common.Utilities;
using BMC.Presentation.POS.Helper_classes;
namespace BMC.Presentation.POS.Views
{
    /// <summary>
    /// Interaction logic for PromoTIS.xaml
    /// </summary>
    public partial class PromoTIS : UserControl
    {
        DateTime StartDate = DateTime.Now;
        public PromoTIS()
        {
            InitializeComponent();
            dtpPromoStartDate.SelectedDate = DateTime.Now.Date;
            tmpPromoStartTime.SelectedHour = 0;
            tmpPromoStartTime.SelectedMinute = 0;
            tmpPromoStartTime.SelectedSecond = 0;
            dtpPromoEndDate.SelectedDate = DateTime.Now.Date;

        }
        private void btnTISSearchOK_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Date Validation

            if((dtpPromoStartDate.SelectedDate==null)||(dtpPromoEndDate.SelectedDate==null))
            {
                MessageBox.ShowBox("MessageID551", BMC_Icon.Warning);//Please Enter the Valid Expiry Date
                return;
            }

                DateTime dtpPromoStartDateVal = DateTime.Now;
                DateTime dtpPromoEndDateVal = DateTime.Now;
                DateTime.TryParse(dtpPromoStartDate.SelectedDate.ToString(),out dtpPromoStartDateVal);
                DateTime.TryParse(dtpPromoEndDate.SelectedDate.ToString(), out dtpPromoEndDateVal);

                if (dtpPromoStartDateVal == DateTime.Today)
                {
                    if (tmpPromoStartTime.SelectedTime > DateTime.Now.TimeOfDay)
                    {
                        MessageBox.ShowBox("MessageID885", BMC_Icon.Warning);
                        return;

                    }
                }

                if (dtpPromoEndDateVal == DateTime.Today)
                {
                    if (tmpPromoEndTime.SelectedTime > DateTime.Now.TimeOfDay)
                    {
                        MessageBox.ShowBox("MessageID886", BMC_Icon.Warning);
                        return;

                    }
                }

                if (dtpPromoStartDateVal == DateTime.Today && dtpPromoEndDateVal == DateTime.Today)
                {
                    if (tmpPromoStartTime.SelectedTime > tmpPromoEndTime.SelectedTime)
                    {
                        MessageBox.ShowBox("MessageID884", BMC_Icon.Warning);
                        return;

                    }
                }
                if (dtpPromoStartDateVal == dtpPromoEndDateVal)
                {
                    if (tmpPromoStartTime.SelectedTime > tmpPromoEndTime.SelectedTime)
                    {
                        MessageBox.ShowBox("MessageID887", BMC_Icon.Warning);
                        return;

                    }
                }


                if (dtpPromoStartDateVal > DateTime.Now)
                {
                    MessageBox.ShowBox("MessageID880", BMC_Icon.Warning);
                    return;
                }

                if (dtpPromoEndDateVal > DateTime.Now)
                {
                    MessageBox.ShowBox("MessageID881", BMC_Icon.Warning);
                    return;
                }

                if (dtpPromoStartDateVal > dtpPromoEndDateVal)
                {
                    MessageBox.ShowBox("MessageID551", BMC_Icon.Warning);
                    return;
                }

                
                

                DateTime ds = dtpPromoStartDate.SelectedDate.Value;
                DateTime dtSCombined = new DateTime(ds.Year, ds.Month, ds.Day, tmpPromoStartTime.SelectedTime.Hours, tmpPromoStartTime.SelectedTime.Minutes, tmpPromoStartTime.SelectedTime.Seconds);

                DateTime de = dtpPromoEndDate.SelectedDate.Value;
                DateTime dtECombined = new DateTime(de.Year, de.Month, de.Day, tmpPromoEndTime.SelectedTime.Hours, tmpPromoEndTime.SelectedTime.Minutes, tmpPromoEndTime.SelectedTime.Seconds);
               
                // TIS Promotional Details screen


                TISPromoDetails objTISPromoDetails = new TISPromoDetails(dtSCombined, dtECombined);
               
                if (objTISPromoDetails.IsTISDataFound)
                {
                 
                    objTISPromoDetails.ShowDialogEx(this);
                }

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
        private void btnTISSearchCancel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
           
                dtpPromoStartDate.SelectedDate = DateTime.Now.Date;
                tmpPromoStartTime.SelectedHour = 0;
                tmpPromoStartTime.SelectedMinute = 0;
                tmpPromoStartTime.SelectedSecond = 0;
                dtpPromoEndDate.SelectedDate = DateTime.Now.Date;
                tmpPromoEndTime.SelectedHour = DateTime.Now.Hour;
                tmpPromoEndTime.SelectedMinute = DateTime.Now.Minute;
                tmpPromoEndTime.SelectedSecond = DateTime.Now.Second;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void txtTISStartDate_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void dtpPromoEndDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                DateTime dt = Convert.ToDateTime(dtpPromoEndDate.SelectedDate);
                StartDate = new DateTime(dt.Year, dt.Month, dt.Day, tmpPromoEndTime.SelectedHour, tmpPromoEndTime.SelectedMinute, tmpPromoEndTime.SelectedSecond);
                txtTISEndDate.Text = Convert.ToDateTime(StartDate).ToString("d", new CultureInfo(ExtensionMethods.CurrentDateCulture));
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void dtpPromoStartDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                DateTime dt = Convert.ToDateTime(dtpPromoStartDate.SelectedDate);
                StartDate = new DateTime(dt.Year, dt.Month, dt.Day, tmpPromoStartTime.SelectedHour, tmpPromoStartTime.SelectedMinute, tmpPromoStartTime.SelectedSecond);
                txtTISStartDate.Text = Convert.ToDateTime(StartDate).ToString("d", new CultureInfo(ExtensionMethods.CurrentDateCulture));
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
        
    }
}
