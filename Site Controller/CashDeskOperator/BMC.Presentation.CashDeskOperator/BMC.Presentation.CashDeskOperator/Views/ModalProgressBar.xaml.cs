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
using System.Threading;
using BMC.Common.ExceptionManagement;

namespace BMC.Presentation
{
    /// <summary>
    /// Interaction logic for ModalProgressBar.xaml
    /// </summary>
    public partial class ModalProgressBar : Window
    {
        private Action _DoWork = null;
        private WaitCallback _dAction = null;

        public ModalProgressBar(Action doWork)
        {
            InitializeComponent();
            _DoWork = doWork;
            _dAction = new WaitCallback(this.DoAction);
            this.ResizeMode = System.Windows.ResizeMode.NoResize;
        }

        private void DoAction(object o)
        {
            try
            {
                _DoWork();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            finally
            {
                this.DoClose(o);
            }
        }

        private void DoClose(object o)
        {
            Dispatcher.Invoke(new Action(() =>
            {
                this.Close();
            }));
        }

        private void pgBarMachineDrop_Loaded(object sender, RoutedEventArgs e)
        {
            ThreadPool.QueueUserWorkItem(_dAction, null);
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }

    }
}
