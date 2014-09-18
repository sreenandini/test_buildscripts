using System;
using System.Data;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using BMC.Common.ConfigurationManagement;
using BMC.Common.ExceptionManagement;
using BMC.Common.LogManagement;
using BMC.CashDeskOperator;
using BMC.CashDeskOperator.BusinessObjects;
using BMC.Transport;
using Microsoft.Win32;
using System.Diagnostics;

namespace BMC.Presentation
{
    /// <summary>
    /// Interaction logic for CrossTicketingSettings.xaml
    /// </summary>
    public partial class CrossTicketingSettings : UserControl
    {
        public CrossTicketingSettings()
        {
            InitializeComponent();
            LoadData();
        }
        public void LoadData()
        {
            try
            {
                using (CrossTicketingSettingDetails objCrossTicketingSettingDetails = new CrossTicketingSettingDetails(oCommonUtilities.CreateInstance().GetConnectionString()))
                {
                    LogManager.WriteLog("Inside LoadData function of Cross Ticketing Settings", LogManager.enumLogLevel.Info);
                    lstCrossTicketingSettingDetails.ItemsSource = objCrossTicketingSettingDetails.GetCrossTicketingSettings(Settings.SiteCode);
                }
            }
            catch (Exception exLoadPropertyGrid)
            {
                ExceptionManager.Publish(exLoadPropertyGrid);
            }
        }

    }
}
