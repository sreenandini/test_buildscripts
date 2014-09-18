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
using BMC.CashDeskOperator;
using BMC.Common.ExceptionManagement;
using BMC.Transport;
using System.Net.NetworkInformation;
using System.Threading;
using System.Globalization;
using System.ComponentModel;
using System.Configuration;

namespace BMC.Presentation
{
    /// <summary>
    /// Interaction logic for CPingGMU.xaml
    /// </summary>



    public partial class CPingGMU : UserControl
    {
        AutoResetEvent _ResetEvent = new AutoResetEvent(false);
        List<GMUListtoPing> _GMUList = null;
        bool _Isprocessing = false;
        int _PingTimeoutMilliseconds = 1000;

        public CPingGMU()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                
                LoadGMUList();
                _PingTimeoutMilliseconds = int.Parse(ConfigurationManager.AppSettings["PingTimeout"].ToString());
            }
            catch (Exception Ex)
            {
                _PingTimeoutMilliseconds = 1000;
                ExceptionManager.Publish(Ex);
            }
        }

        private void UserControl_UnLoaded(object sender, RoutedEventArgs e)
        {
            try
            {
                _ResetEvent.Set();
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
        }

        private void btnPing_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_Isprocessing == false)
                {
                    _Isprocessing = true;
                    Button btn = sender as Button;
                    btn.IsEnabled = false;
                    GMUListtoPing GMU = btn.DataContext as GMUListtoPing;
                    GMU.Status = "Processing";
                    GMU.IsEnabled = false;
                    Action<GMUListtoPing> PingMachine = new Action<GMUListtoPing>(PingIP);
                    PingMachine.BeginInvoke(GMU, null, null);
                    _Isprocessing = false;
                }
            }
            catch (Exception Ex)
            {
                _Isprocessing = false;
                ExceptionManager.Publish(Ex);
            }
            
        }

        private void btnClearStatus_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                ClearStatus();
            }
            catch (Exception Ex)
            {
             ExceptionManager.Publish(Ex);
            }
        }

        public void btnStop_Click(object sender, RoutedEventArgs E)
        {
            try
            {
                _ResetEvent.Set();
                _GMUList.ForEach((x) => { x.IsEnabled = true; });
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
        }

        private void btnPingAll_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!_Isprocessing)
                {
                    ClearStatus();
                    Action PingMachine = new Action(PingAllIPs);
                    PingMachine.BeginInvoke(null, null);
                }
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                LoadGMUList();
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
        }

        private void gv_GmuList_GridViewColumnHeaderClickedHandler(object sender, RoutedEventArgs e)
        {
            if (!_Isprocessing)
            {
                GridViewColumnHeader headerClicked = e.OriginalSource as GridViewColumnHeader;
                this.Sort(headerClicked);
            }
        }

        private void PingIP(GMUListtoPing GmuDetails)
        {
            try
            {
                Ping CheckConnectivity = new Ping();
                PingReply reply = CheckConnectivity.Send(GmuDetails.IPAddress, _PingTimeoutMilliseconds);
                GmuDetails.Status = reply.Status.ToString();
                GmuDetails.IsEnabled = true;
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
                GmuDetails.Status = "Fail";
                GmuDetails.IsEnabled = true;
            }
        }

        private void PingAllIPs()
        {
            try
            {
                _Isprocessing = true;
                _GMUList.ForEach((x) => { x.IsEnabled = false; });
                foreach (GMUListtoPing GmuDetails in _GMUList)
                {
                    try
                    {
                        if (_ResetEvent.WaitOne(200))
                            break;
                        GmuDetails.Status = "Processing";
                        //lv_GmuList.ScrollIntoView(GmuDetails);
                        Application.Current.Dispatcher.Invoke(new Action(() => { lv_GmuList.ScrollIntoView(GmuDetails); lv_GmuList.SelectedItem = GmuDetails; }));
                        GmuDetails.IsEnabled = false;
                        Ping CheckConnectivity = new Ping();
                        PingReply reply = CheckConnectivity.Send(GmuDetails.IPAddress, _PingTimeoutMilliseconds);
                        GmuDetails.Status = reply.Status.ToString();
                        GmuDetails.IsEnabled = true;


                    }
                    catch (Exception Ex)
                    {
                        ExceptionManager.Publish(Ex);
                        GmuDetails.Status = "Fail";
                        GmuDetails.IsEnabled = true;
                    }
                }
                _Isprocessing = false;
            }
            catch (Exception Ex)
            {
                _Isprocessing = false;
                ExceptionManager.Publish(Ex);
                _GMUList.ForEach((x) => { x.IsEnabled = true; });
            }
            finally
            {
                _ResetEvent.Reset();
            }

        }

        private void LoadGMUList()
        {
            if (!_Isprocessing)
            {
                GMUPing objGMUPing = new GMUPing();
                _GMUList = objGMUPing.GetGmuList();
                lv_GmuList.ItemsSource = _GMUList;
                _GMUList.ForEach((x) => { x.IsEnabled = true; });
            }

        }

        private void ClearStatus()
        {
            if (!_Isprocessing)
            {
                _GMUList.ForEach((x) => { x.Status = string.Empty; });
                _ResetEvent.Reset();
            }
        }

        private int Sort(GridViewColumnHeader Header)
        {
            string sSortColumn = "";
            ListSortDirection direction;
            direction = ListSortDirection.Ascending;

            DataTemplate o = Header.Column.CellTemplate as DataTemplate;

            if (Header.Column.HeaderTemplate == null)
            {
                direction = ListSortDirection.Ascending;
                Header.Column.HeaderTemplate = App.Current.Resources["HeaderArrowUp"] as DataTemplate;
            }
            else
            {
                if (Header.Column.HeaderTemplate == App.Current.Resources["HeaderArrowUp"] as DataTemplate)
                {
                    direction = ListSortDirection.Descending;
                    Header.Column.HeaderTemplate = App.Current.Resources["HeaderArrowDown"] as DataTemplate;
                }
                else
                {
                    direction = ListSortDirection.Ascending;
                    Header.Column.HeaderTemplate = App.Current.Resources["HeaderArrowUp"] as DataTemplate;
                }
            }

            if (Header.Tag != null)
                sSortColumn = Header.Tag as string;

            var list = lv_GmuList.View as GridView;
            foreach (var obj in list.Columns)
            {
                if (obj.Header != Header.Column.Header)
                    obj.HeaderTemplate = null;
            }
            if (direction == ListSortDirection.Ascending && sSortColumn.ToUpper() =="STATUS")
            {
              _GMUList=  _GMUList.OrderBy(ol => ol.Status).ToList(); 
            }

            if (direction == ListSortDirection.Descending && sSortColumn.ToUpper() =="STATUS")
            {
                _GMUList = _GMUList.OrderByDescending(old => old.Status).ToList();
            }

            if (direction == ListSortDirection.Ascending && sSortColumn.ToUpper() == "GMUNO")
            {
                _GMUList = _GMUList.OrderBy(olg => double.Parse(olg.GMUNo)).ToList();
            }

            if (direction == ListSortDirection.Descending && sSortColumn.ToUpper() == "GMUNO")
            {
                _GMUList = _GMUList.OrderByDescending(oldg =>double.Parse(oldg.GMUNo)).ToList();
            }

            lv_GmuList.ItemsSource=null;
            lv_GmuList.ItemsSource = _GMUList;

            return 0;
        }

    }
    public class StatusToBackgroundConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture  )
        {
            if (value == null)
            {
                return new SolidColorBrush(System.Windows.Media.Colors.Black);
            }
            if (value.ToString().ToUpper() == "SUCCESS")
            {
                return new SolidColorBrush(System.Windows.Media.Colors.Green);
            }
            if (value.ToString().ToUpper() == "PROCESSING")
            {
                return new SolidColorBrush(System.Windows.Media.Colors.Black);
            }
            else
            {
                return new SolidColorBrush(System.Windows.Media.Colors.Red);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }

}
