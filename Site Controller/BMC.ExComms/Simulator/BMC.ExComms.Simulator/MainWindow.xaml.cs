using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BMC.CoreLib;
using BMC.CoreLib.Concurrent;
using BMC.CoreLib.Net;
using BMC.CoreLib.Win32;
using BMC.ExComms.Contracts.DTO.Freeform;
using BMC.ExComms.Simulator.Handlers;
using BMC.ExComms.Simulator.Models;

namespace BMC.ExComms.Simulator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //private readonly IExecutorService _executorService = ExecutorServiceFactory.CreateExecutorService();
        //private static string IPADDR = Extensions.GetIpAddressString(-1);// "192.168.1.10";
        ////private static string IPADDR = "192.168.10.11";

        //private SocketTransceiver _socket = null;

        public MainWindow()
        {
            InitializeComponent();
            App.MainWindow = this;
            axConsole.Output = App.SbLog.ToString();
            axConsole.ScrollToEnd();
            Log.GlobalWriteToExternalLog += Log_GlobalWriteToExternalLog;
            App.ScreenInitialized = true;
        }

        void Log_GlobalWriteToExternalLog(string formattedMessage, CoreLib.Diagnostics.LogEntryType type, object extra)
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "Log_GlobalWriteToExternalLog"))
            {
                try
                {
                    Dispatcher.Invoke(new Action(() =>
                    {
                        if (type == CoreLib.Diagnostics.LogEntryType.Exception)
                        {
                            if (axConsole.Exceptions.Count >= 32767)
                                axConsole.Exceptions.Clear();

                            ExceptionModel item = new ExceptionModel()
                            {
                                SNo = axConsole.Exceptions.Count + 1,
                                ReceivedTime = DateTime.Now,
                                Exception = formattedMessage,
                            };
                            axConsole.Exceptions.Add(item);
                        }

                        if (axConsole.Output.Length > 32767)
                        {
                            axConsole.Output = string.Empty;
                        }

                        axConsole.Output += DateTime.Now.ToFullString() + "\t" + formattedMessage + Environment.NewLine;
                        axConsole.ScrollToEnd();
                    }));
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "Method"))
            {
                try
                {
                    //this.InitSocket();
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }
            }
        }

        protected virtual string DYN_MODULE_NAME
        {
            get
            {
                return this.GetType().Name;
            }
        }

        //private void InitSocket()
        //{
        //    _socket = new SocketTransceiver(_executorService, IPADDR, 1031);
        //    _socket.ReceiveUdpEntityData += OnReceiveUdpEntityData;
        //    _socket.Start();
        //}

        //private void OnReceiveUdpEntityData(UdpFreeformEntity udpEntity)
        //{
        //    using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "Method"))
        //    {
        //        try
        //        {
        //            string text = FreeformHelper.GetConvertBytesToHexString(udpEntity.RawData, string.Empty);
        //            Dispatcher.Invoke(new Action(() =>
        //            {
        //                if (axResponse.ResponseItems.Count >= 32767)
        //                    axResponse.ResponseItems.Clear();

        //                UdpResponseItemModel item = new UdpResponseItemModel()
        //                {
        //                    IPAddress = udpEntity.AddressString,
        //                    SNo = axResponse.ResponseItems.Count + 1,
        //                    ReceivedTime = udpEntity.ProcessDate,
        //                    RawDataInHex = text,
        //                };
        //                axResponse.ResponseItems.Add(item);
        //            }));
        //        }
        //        catch (Exception ex)
        //        {
        //            method.Exception(ex);
        //        }
        //    }
        //}
    }
}
