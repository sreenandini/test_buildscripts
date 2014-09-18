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
using BMC.Common.ExceptionManagement;
using BMC.Transport;
using BMC.Presentation.POS.Helper_classes;
using BMC.Common.LogManagement;
using BMC.Security;

namespace BMC.Presentation.POS.Views
{
    /// <summary>
    /// Interaction logic for PromotionVouchers.xaml
    /// </summary>
    public partial class PromotionVouchers : UserControl
    {
        PromoPrint prmPrint;
        PromoHistory prmHistory;
        PromoVoid prmVoid;
        PromoTIS prmTIS;
       

        public PromotionVouchers()
        {
            InitializeComponent();
            LoadDetails();

        }

        public void LoadDetails()
        {

            //if (!CPrintPromotionalTickets.IsPrinting)
            //{
            chkPrint.Visibility = Visibility.Visible;
            chkVoid.Visibility = Visibility.Visible;
            chkHistory.Visibility = Visibility.Visible;
            chkTIS.Visibility = Visibility.Visible;



            if (!SecurityHelper.HasAccess("BMC.Presentation.Promotions.Print"))
            {
                chkPrint.Visibility = Visibility.Collapsed;
                if (SecurityHelper.HasAccess("BMC.Presentation.Promotions.History"))
                    chkHistory.IsChecked = true;
                else
                {
                    if (SecurityHelper.HasAccess("BMC.Presentation.Promotions.Void"))
                    {
                        chkVoid.IsChecked = true;
                    }
                    else
                    {
                        if ((SecurityHelper.HasAccess("BMC.Presentation.Promotions.TIS")) || (!Settings.IsTISEnabled))
                        {
                            chkTIS.IsChecked = true;
                        }
                    }


                }

            }

            else
            {
                chkPrint.IsChecked = true;
                
            }

            if (!SecurityHelper.HasAccess("BMC.Presentation.Promotions.Void"))
            {
                chkVoid.Visibility = Visibility.Collapsed;
                if (SecurityHelper.HasAccess("BMC.Presentation.Promotions.Print"))
                {
                    chkPrint.IsChecked = true;
                }
                else if (SecurityHelper.HasAccess("BMC.Presentation.Promotions.History"))
                {
                    chkHistory.IsChecked = true;

                }

                else if ((SecurityHelper.HasAccess("BMC.Presentation.Promotions.TIS")) && (Settings.IsTISEnabled))
                {
                    chkTIS.IsChecked = true;
                }

            }


            if (!SecurityHelper.HasAccess("BMC.Presentation.Promotions.History"))
            {
                chkHistory.Visibility = Visibility.Collapsed;
                if (SecurityHelper.HasAccess("BMC.Presentation.Promotions.Print"))
                {
                    chkPrint.IsChecked = true;
                }
                else if (SecurityHelper.HasAccess("BMC.Presentation.Promotions.Void"))
                {
                    chkVoid.IsChecked = true;

                }

                else if ((SecurityHelper.HasAccess("BMC.Presentation.Promotions.TIS")) && (Settings.IsTISEnabled))
                {
                    chkTIS.IsChecked = true;
                }
            }
            if ((!SecurityHelper.HasAccess("BMC.Presentation.Promotions.TIS")) || (!Settings.IsTISEnabled))
            {
                chkTIS.Visibility = Visibility.Collapsed;
                if (SecurityHelper.HasAccess("BMC.Presentation.Promotions.Print"))
                {
                    chkPrint.IsChecked = true;
                }

                else if (SecurityHelper.HasAccess("BMC.Presentation.Promotions.History"))
                {
                    chkHistory.IsChecked = true;
                }
                else if (SecurityHelper.HasAccess("BMC.Presentation.Promotions.Void"))
                {
                    chkVoid.IsChecked = true;

                }

            }
            //}
            //else
            //{

            //    chkPrint.Visibility = Visibility.Hidden;
            //    chkVoid.Visibility = Visibility.Hidden;
            //    chkHistory.Visibility = Visibility.Hidden;
            //    chkTIS.Visibility = Visibility.Hidden;
            //}


        }


        private void chkPrint_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                chkPrint.IsEnabled = false;
                //if(!CPrintPromotionalTickets.IsPrinting)
                //     prmPrint = new PromoPrint(objPrintTickets);
                prmPrint = new PromoPrint(this);
                pnlPromoVouchContent.Child = prmPrint;
                prmPrint.Margin = new Thickness(0);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            finally
            {
                chkPrint.IsEnabled = true;
            }
        }

        private void chkHistory_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                chkHistory.IsEnabled = false;

                prmHistory = new PromoHistory();
                pnlPromoVouchContent.Child = prmHistory;
                prmHistory.Margin = new Thickness(0);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            finally
            {
                chkHistory.IsEnabled = true;
            }
        }

        private void chkVoid_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                chkVoid.IsEnabled = false;
                prmVoid = new PromoVoid();
                pnlPromoVouchContent.Child = prmVoid;
                prmVoid.Margin = new Thickness(0);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            finally
            {
                chkVoid.IsEnabled = true;
            }
        }
        private void chkTIS_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                chkTIS.IsEnabled = false;

                prmTIS = new PromoTIS();
                pnlPromoVouchContent.Child = prmTIS;
                prmTIS.Margin = new Thickness(0);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            finally
            {
                chkTIS.IsEnabled = true;
            }
        }
    }



}
