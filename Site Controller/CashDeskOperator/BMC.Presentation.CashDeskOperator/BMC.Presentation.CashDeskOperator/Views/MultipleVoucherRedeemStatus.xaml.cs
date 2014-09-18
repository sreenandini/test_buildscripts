using BMC.Common.ExceptionManagement;
using BMC.Common.LogManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BMC.Presentation.POS.Views
{
    /// <summary>
    /// Interaction logic for MultipleVoucherRedeemStatus.xaml
    /// </summary>
    public partial class MultipleVoucherRedeemStatus : IDisposable
    {

        #region variables
        public bool disposed;
        RedeemMultipleTickets _redeemMultipleTickets = null;
        private Action _DoWork = null;
        private WaitCallback _dAction = null;

        #endregion variables
        public MultipleVoucherRedeemStatus(RedeemMultipleTickets objR,Action doWork)
        {
            InitializeComponent();
            _redeemMultipleTickets = objR;
            _DoWork = doWork;
            _dAction = new WaitCallback(this.DoAction);
            this.ResizeMode = System.Windows.ResizeMode.NoResize;
        }

       

        #region Events

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            ThreadPool.QueueUserWorkItem(_dAction, null);            
           
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {

            try
            {
               
                _redeemMultipleTickets.btnStopRedeem_Click(sender,e);
                this.Close();    
           
                
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
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

        //private void pgBarMachineDrop_Loaded(object sender, RoutedEventArgs e)
        //{
        //    ThreadPool.QueueUserWorkItem(_dAction, null);
        //}

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }

        #endregion Events


        #region Methods

        public void Dispose()
        {

            try
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }
            catch (Exception ex)
            {


            }
        }

        private void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _redeemMultipleTickets = null;

                }
                disposed = true;
            }
        }

        #endregion Methods


    }
}
