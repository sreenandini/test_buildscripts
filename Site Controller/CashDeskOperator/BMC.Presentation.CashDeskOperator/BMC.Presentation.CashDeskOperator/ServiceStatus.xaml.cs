using BMC.Common.LogManagement;
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

namespace BMC.Presentation
{
    /// <summary>
    /// Interaction logic for ServiceStatus.xaml
    /// </summary>
    public partial class ServiceStatus : Window
    {

        public ServiceStatus(IDictionary<string, BMC.Presentation.MainScreen.ServiceDetail> dic_servicedetail, bool isDBRunning, bool isGuardianRunning)
        {
            InitializeComponent();
            LoadServicesDetails(dic_servicedetail, isDBRunning, isGuardianRunning);

        }



        public void LoadServicesDetails(IDictionary<string, BMC.Presentation.MainScreen.ServiceDetail> dic_servicedetail, bool isDBRunning, bool isGuardianRunning)
        {
            this.Dispatcher.Invoke((Action)(() =>
                {
                    try
                    {
                        var redcolor = new System.Windows.Media.SolidColorBrush(Colors.Red);
                        var greencolor = new System.Windows.Media.SolidColorBrush(Colors.Green);
                        List<BMC.Presentation.MainScreen.ServiceDetail> lst_service = dic_servicedetail.Values.ToList();
                        int i = 1;
                        foreach (BMC.Presentation.MainScreen.ServiceDetail sd in lst_service)
                        {
                            sd.SNo = i++;
                            if (!isDBRunning)
                            {
                                sd.ServiceStatus = "Unknown";
                            }
                            else if ((!isGuardianRunning) && sd.ServiceName.ToUpper() != "BMCGUARDIANSERVICE")
                            {
                                sd.ServiceStatus = "Unknown";
                            }
                            sd.ForeColor = (string.Compare(sd.ServiceStatus, "Running", true) != 0) ? redcolor : greencolor;
                        }
                        if (BMC.Presentation.MainScreen.ServiceDetail.LastUpdateDateTime.HasValue)
                        {
                            txt_lastupdated.Text = "Last Updated Time : " + BMC.Presentation.MainScreen.ServiceDetail.LastUpdateDateTime.Value.ToString("dd-MMM-yyyy HH:mm:ss");
                        }
                        lst_ServiceStatus.ItemsSource = null;
                        lst_ServiceStatus.ItemsSource = lst_service;

                    }
                    catch (Exception ex)
                    {
                        LogManager.WriteLog("LoadServicesDetails::" + ex.Message, LogManager.enumLogLevel.Error);

                    }
                }));

        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
