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
using BMC.Presentation.CashDeskOperator;
using BMC.Business.CashDeskOperator;
using Audit.Transport;
using System.Threading;
using System.Windows.Threading;
using BMC.CashDeskOperator.BusinessObjects;
using BMC.Common.ExceptionManagement;
using BMC.Presentation.POS.Helper_classes;
using BMC.Common.LogManagement;

namespace BMC.Presentation
{
    /// <summary>
    /// Interaction logic for LoadingWindow.xaml
    /// </summary>
    public partial class LoadingWindow : Window
    {

        private UIElement _parent;
        private ModuleName _ctype;
        private string _ValidationNo { get; set; }
        private string _AssetNo { get; set; }
        private int _Amount { get; set; }
        IList<int> _Installations = null;
        int SleepTime = 1000;
        Action _afterAction = null;
        Action _act_body = null;
        bool _LoadOnlyDB = false;
        
        public LoadingWindow(UIElement parent, ModuleName ctype, string TicketNo, string AssetNo, int Amount)
        {
            _parent = parent;
            _ctype = ctype;
            _ValidationNo = TicketNo;
            _AssetNo = AssetNo;
            _Amount = Amount;
            InitializeComponent();
            ImageBrush myBrush = new ImageBrush();
            myBrush.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Resources/back.jpg", UriKind.Absolute));
            //Window.GetWindow(Parent).   
            this.Owner = Window.GetWindow(parent);
            this.Background = myBrush;
        }

        public LoadingWindow(UIElement parent, ModuleName ctype, IList<int> Installations, Action afterAction)
        {
            _parent = parent;
            _ctype = ctype;
            _Installations = Installations;
            _afterAction = afterAction;
            InitializeComponent();
            lblStatus.SetValue(System.Windows.Controls.Label.ContentProperty, "Current Meters");
            ImageBrush myBrush = new ImageBrush();
            myBrush.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Resources/back.jpg", UriKind.Absolute));
            //Window.GetWindow(Parent).   
            this.Owner = UIExtensionMethods.GetWindowOwner(parent);
            this.Background = myBrush;
            pgBar.IsIndeterminate = false;
            pgBar.Maximum = Installations.Count;
            pgBar.Minimum = 0;
            SleepTime = Convert.ToInt32(BMC.Common.ConfigurationManagement.ConfigManager.Read("DBRefreshInterval"));            
        }
        public LoadingWindow(UIElement parent, ModuleName ctype,Action act_body, bool LoadOnlyDB)
        {
            _parent = parent;
            _ctype = ctype;
            _act_body = act_body;
            _LoadOnlyDB = LoadOnlyDB;
            InitializeComponent();
            lblStatus.SetValue(System.Windows.Controls.Label.ContentProperty, "Retrieving data from database...");
            ImageBrush myBrush = new ImageBrush();
            myBrush.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Resources/back.jpg", UriKind.Absolute));
            //Window.GetWindow(Parent).   
            this.Owner = UIExtensionMethods.GetWindowOwner(parent);
            this.Background = myBrush;
            pgBar.IsIndeterminate = false;
            pgBar.Maximum = 100;
            pgBar.Minimum = 0;
          
        }
              

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Thread tdis = new Thread(delegate()
            {
                switch (_ctype)
                {
                    case ModuleName.AnalysisDetails:
                        LogManager.WriteLog("Loading Form:ModuleName.AnalysisDetails", LogManager.enumLogLevel.Info);
                        if (_LoadOnlyDB)
                        {
                            if (_act_body != null)
                            {
                                _act_body();
                            }
                            GetInstallationFromDB();
                        }
                        else
                        {
                            GetCurrentMeters(_Installations);
                        }
                        break;
                    case ModuleName.SpotCheck:
                        LogManager.WriteLog("Loading Form:ModuleName.SpotCheck", LogManager.enumLogLevel.Info);
                        GetCurrentMeters(_Installations);
                        break;
                    default:
                        Dispense(_ValidationNo, _AssetNo, _Amount);
                        break;
                }
            });
            tdis.Start();

        }

        void Dispense(string TicketNo, string AssetNo, int Amount)
        {
            try
            {
                IBmcCashDispenser obj = BmcCashDispenserFactory.GetDispenser(_parent, _ctype);
                if (obj != null)
                {
                    obj.Finished += new BmcCashDispenserFinishedHandler(obj_Finished);
                    obj.StatusChanged += new BmcCashDispenserStatusChangeHandler(obj_StatusChanged);
                    obj.Dispense(TicketNo, AssetNo, Amount);
                }
                else
                {
                    Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate
                    {
                        Result res = new Result();
                        res.IsSuccess = false;
                        res.error = new Error();
                        res.error.Message = Application.Current.FindResource("MessageID444") as string;
                        res.error.MessageType = "Error";
                        this.Result = res;
                        this.Close();
                    });
                }
            }
            catch (Exception ex)
            {
                Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate
                {
                    this.Close();
                });
            }
        }

        void GetCurrentMeters(IList<int> Installations)
        {
            try
            {
                LogManager.WriteLog("GetCurrentMeters Started", LogManager.enumLogLevel.Info);
                IMeterLife meterLife = MeterLifeBusinessObject.CreateInstance();
                meterLife.GetCurrentMeters(Installations,
                    (i, j, k) =>
                    {
                        this.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate
                        {
                            lblStatus.SetValue(System.Windows.Controls.Label.ContentProperty, string.Format("Processing ... ({0:D} out of {1:D})", j, k));
                            pgBar.Value = j;
                        });
                    });
                this.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate
                {
                    lblStatus.SetValue(System.Windows.Controls.Label.ContentProperty, "Retrieving data from database...");

                });
                Thread.Sleep(SleepTime);
                LogManager.WriteLog("GetCurrentMeters Ended", LogManager.enumLogLevel.Info);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate
                {
                    this.Close();
                });
            }
            finally
            {
                try
                {
                    if (_afterAction != null)
                    {
                        _afterAction();
                    }
                }
                catch { }

                Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate
                {
                    this.Close();
                });
            }
        }

        void GetInstallationFromDB()
        {
            try
            {
                LogManager.WriteLog("InstallationFromDB Started", LogManager.enumLogLevel.Info);               
                this.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate
                {
                    lblStatus.SetValue(System.Windows.Controls.Label.ContentProperty, "Retrieving data from database...");

                });
                Thread.Sleep(SleepTime);
                LogManager.WriteLog("InstallationFromDB Ended", LogManager.enumLogLevel.Info);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate
                {
                    this.Close();
                });
            }
            finally
            {               

                Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate
                {
                    this.Close();
                });
            }
        }

        void obj_StatusChanged(IBmcCashDispenser sender, string Status)
        {
            Dispatcher.BeginInvoke(DispatcherPriority.Background, (SendOrPostCallback)delegate { lblStatus.SetValue(System.Windows.Controls.Label.ContentProperty, Status); }, null);
        }

        public Result Result { get; set; }

        void obj_Finished(IBmcCashDispenser sender, IBmcCashDispenseresult result)
        {
            Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate
            {
                this.Result = result.Result;
                this.Close();
            });
        }
    }
}
