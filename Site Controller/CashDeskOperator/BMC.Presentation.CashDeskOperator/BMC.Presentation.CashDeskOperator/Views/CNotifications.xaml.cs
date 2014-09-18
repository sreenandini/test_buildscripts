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
using System.Windows.Shapes;
using BMC.Common.ExceptionManagement;
using BMC.CashDeskOperator.BusinessObjects;
using Microsoft.Win32;
using BMC.Common.LogManagement;
using BMC.CashDeskOperator;
using BMC.Presentation.POS.Helper_classes;
using BMC.Transport;

namespace BMC.Presentation.POS.Views
{
    /// <summary>
    /// Interaction logic for CNotifications.xaml
    /// </summary>
    public partial class CNotifications : Window
    {
        public CNotifications()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                GetNotifications();
                dgNotificationsDetails.Columns[0].IsReadOnly = false;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        public void GetNotifications()
        {
            lbl_Status.Content = "";
            dgNotificationsDetails.ItemsSource = null;
            List<rsp_N_GetNotificationsResult> lstNotifications = Notifications.CreateInstance().GetNotifications();
            dgNotificationsDetails.ItemsSource = lstNotifications;
            dgNotificationsDetails.Tag = lstNotifications;
            this.DataContext = lstNotifications;
        }

        private void btn_Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btn_Clear_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string sNotificationId = string.Empty;

                List<rsp_N_GetNotificationsResult> lstReadLiquidation = dgNotificationsDetails.SelectedItems.Cast<rsp_N_GetNotificationsResult>().ToList();

                if (lstReadLiquidation != null)
                {
                    foreach (var v in lstReadLiquidation)
                    {
                        sNotificationId += "," + v.NotificationID.ToString();
                    }

                    if (!String.IsNullOrEmpty(sNotificationId))
                    {
                        sNotificationId = sNotificationId.Remove(0, 1);
                    }

                    if (!String.IsNullOrEmpty(sNotificationId))
                    {
                        int result = Notifications.CreateInstance().UpdateNotifications(sNotificationId, Security.SecurityHelper.CurrentUser.SecurityUserID);
                        cb_SelectAll.IsChecked = false;
                        GetNotifications();
                    }
                    else
                    {
                        lbl_Status.Content = "Select notifications to clear.";
                        return;
                    }
                }
                else
                {
                    lbl_Status.Content = "Select notifications to clear.";
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void cb_SelectAll_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                dgNotificationsDetails.SelectAll();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void cb_SelectAll_Unchecked(object sender, RoutedEventArgs e)
        {
            try
            {
                dgNotificationsDetails.UnselectAll();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btn_Refresh_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                GetNotifications();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

    }
}
