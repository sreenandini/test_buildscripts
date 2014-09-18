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
using System.Data;
using BMC.CashDeskOperator.BusinessObjects;
using BMC.Common.ExceptionManagement;
using BMC.Common.LogManagement;
using BMC.Presentation.POS.Helper_classes;
using BMC.CashDeskOperator;
using BMC.Transport;
using BMC.Presentation.POS.Views;

namespace BMC.Presentation
{
    /// <summary>
    /// Interaction logic for CDrop.xaml
    /// </summary>
    public partial class CReadLiquidationMain : IDisposable
    {
        public CReadLiquidationMain()
		{
			InitializeComponent();

            chkReadLiquidation.Visibility = Visibility.Visible;
            chkReport.Visibility = Visibility.Visible;
            chkReadLiquidation.IsChecked = true;

            if ((!Security.SecurityHelper.HasAccess("BMC.Presentation.ReadBasedLiquidationMain.ReadBasedLiquidation")) || Settings.CentralizedReadLiquidation)
            {
                chkReadLiquidation.Visibility = Visibility.Collapsed;
                chkReport.IsChecked = true;
            }

            if (!Security.SecurityHelper.HasAccess("BMC.Presentation.ReadBasedLiquidationMain.ReportLiquidationReport"))
                chkReport.Visibility = Visibility.Collapsed;
		}

        private void chkReadLiquidation_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                chkReadLiquidation.IsEnabled = false;
                pnlReadLiquidationContent.Children.Clear();
                CReadBasedLiquidation objReadBasedLiquidation = new CReadBasedLiquidation();
                pnlReadLiquidationContent.Children.Add(objReadBasedLiquidation);
                objReadBasedLiquidation.Margin = new Thickness(0);
            }
            finally
            {
                chkReadLiquidation.IsEnabled = true;
            }
        }

        private void chkReport_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                chkReport.IsEnabled = false;
                pnlReadLiquidationContent.Children.Clear();
                CReadLiquidationReport objReadLiquidationReport = new CReadLiquidationReport();
                pnlReadLiquidationContent.Children.Add(objReadLiquidationReport);
                objReadLiquidationReport.Margin = new Thickness(0);
            }
            finally
            {
                chkReport.IsEnabled = true;
            }
        }

        #region Dispose Children Members

        private void DisposeChildren()
        {
            try
            {
                int count = pnlReadLiquidationContent.Children.Count;
                for (int i = count - 1; i >= 0; i--)
                {
                    IDisposable element = pnlReadLiquidationContent.Children[i] as IDisposable;
                    if (element != null)
                    {
                        BMC.Presentation.Helper_classes.Common.DisposeObject(ref element);
                    }
                    pnlReadLiquidationContent.Children.RemoveAt(i);
                }
                LogManager.WriteLog("|=> CReadLiquidationMain pnlDropContent Children disposed successfully.", LogManager.enumLogLevel.Info);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
        #endregion

        #region IDisposable Members

        /// <summary>
        /// Variable used to identity whether this object is already disposed or not.
        /// </summary>
        private bool disposed = false;

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        private void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    this.CleanupWPFObjectTopControls((i) =>
                    {
                        // events
                        this.chkReadLiquidation.Checked -= (this.chkReadLiquidation_Checked);
                        this.chkReport.Checked -= (this.chkReport_Checked);
                    },
                    (c) =>
                    {
                    });
                    LogManager.WriteLog("|=> CDrop objects are released successfully.", LogManager.enumLogLevel.Info);
                }
                disposed = true;
            }
        }

        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="CDrop"/> is reclaimed by garbage collection.
        /// </summary>
        ~CReadLiquidationMain()
        {
            Dispose(false);
        }

        #endregion

    }
}
