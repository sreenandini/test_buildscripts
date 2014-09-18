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
using System.ComponentModel;

namespace BMC.Presentation
{
    /// <summary>
    /// Interaction logic for CDrop.xaml
    /// </summary>
    public partial class CDrop : IDisposable//, INotifyPropertyChanged
    {

        
        public CDrop()
		{
			InitializeComponent();

            chkHistory.Visibility = Visibility.Visible;
            chkDeclaration.Visibility = Visibility.Visible;
            chkLiquidation.Visibility = Visibility.Collapsed;

            if ((!Security.SecurityHelper.HasAccess("BMC.Presentation.CDeclaration")))         
                chkDeclaration.Visibility = Visibility.Collapsed;          

            if (!Security.SecurityHelper.HasAccess("BMC.Presentation.CPerformDrop")) 
            {
                chkDrop.Visibility = Visibility.Collapsed;
                if (chkDeclaration.Visibility != Visibility.Collapsed)
                chkDeclaration.IsChecked = true;                
            }
            else
                chkDrop.IsChecked = true;                         

            if (chkDrop.Visibility == Visibility.Collapsed && chkDeclaration.Visibility == Visibility.Collapsed)
                chkHistory.IsChecked = true;

            if ((Settings.LiquidationProfitShare) && (Settings.LiquidationType.ToUpper() == "COLLECTION") && (!Settings.CentralizedDeclaration) && (Security.SecurityHelper.HasAccess("BMC.Presentation.CBatchLiquidation")))
            {
                chkLiquidation.Visibility = Visibility.Visible;
            }

            CPerformDrop.BusyPropertyChanged += new PropertyChangedEventHandler(propChangedEventHandler);
            CDeclaration.BusyPropertyChanged += new PropertyChangedEventHandler(propChangedEventHandler);

		}

        private void propChangedEventHandler(object sender, PropertyChangedEventArgs e)
        {
            DisableAllControls(sender);
        }

        private void DisableAllControls(object sender)
        {
            if (sender is CPerformDrop)
            {
                chkDeclaration.IsEnabled = !CPerformDrop.IsBusy;
                chkHistory.IsEnabled = !CPerformDrop.IsBusy;
                chkLiquidation.IsEnabled = !CPerformDrop.IsBusy;
            }
            else
            {
                chkDrop.IsEnabled = !CDeclaration.IsBusy;
                chkHistory.IsEnabled = !CDeclaration.IsBusy;
                chkLiquidation.IsEnabled = !CDeclaration.IsBusy;
            }
            
        }


        private void chkDeclaration_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (CPerformDrop.IsBusy || CDeclaration.IsBusy)
                {
                    if (CPerformDrop.IsBusy)
                    {
                       MessageBox.ShowBox("MessageID368", BMC_Icon.Information);
                       chkDeclaration.IsChecked = false;
                    }
                    e.Handled = false;
                    return;
                }
                chkDeclaration.IsEnabled = false;
                pnlDropContent.Children.Clear();
                CDeclaration declaration = new CDeclaration();
                pnlDropContent.Children.Add(declaration);
                declaration.Margin = new Thickness(0);
            }
            finally
            {
                if (!CPerformDrop.IsBusy)
                {
                    chkDeclaration.IsEnabled = true;
                    chkDeclaration.IsChecked = true;
                }
                else
                {
                    chkDeclaration.IsEnabled = false;
                    chkDeclaration.IsChecked = false;
                }
            }
        }

        private void chkDrop_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (CPerformDrop.IsBusy || CDeclaration.IsBusy)
                {
                    if (CDeclaration.IsBusy)
                    {
                        chkDrop.IsChecked = false;
                        MessageBox.ShowBox("MessageID368", BMC_Icon.Information);
                    }
                    e.Handled = false;
                    return;
                }
                chkDrop.IsEnabled = false;
                pnlDropContent.Children.Clear();
                CPerformDrop performDrop = new CPerformDrop();
                pnlDropContent.Children.Add(performDrop);
                performDrop.Margin = new Thickness(0);
            }
            finally
            {
                if (!CDeclaration.IsBusy)
                {
                    chkDrop.IsEnabled = true;
                    chkDrop.IsChecked = true;
                }
            }
        }

        private void chkHistory_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (CDeclaration.IsBusy || CPerformDrop.IsBusy)
                {
                    MessageBox.ShowBox("MessageID368", BMC_Icon.Information);
                    e.Handled = false;
                    return;
                }
                chkHistory.IsEnabled = false;
                pnlDropContent.Children.Clear();
                CDropHistory history = new CDropHistory();
                pnlDropContent.Children.Add(history);
                history.Margin = new Thickness(0);
            }
            finally
            {
                chkHistory.IsEnabled = true;
            }
        }

        #region Dispose Children Members

        private void DisposeChildren()
        {
            try
            {
                int count = pnlDropContent.Children.Count;
                for (int i = count - 1; i >= 0; i--)
                {
                    IDisposable element = pnlDropContent.Children[i] as IDisposable;
                    if (element != null)
                    {
                        BMC.Presentation.Helper_classes.Common.DisposeObject(ref element);
                    }
                    pnlDropContent.Children.RemoveAt(i);
                }
                LogManager.WriteLog("|=> CDrop pnlDropContent Children disposed successfully.", LogManager.enumLogLevel.Info);
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
                        this.chkDrop.Checked -= (this.chkDrop_Checked);
                        this.chkDeclaration.Checked -= (this.chkDeclaration_Checked);
                        this.chkHistory.Checked -= (this.chkHistory_Checked);
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
        ~CDrop()
        {
            Dispose(false);
        }

        #endregion

        private void chkLiquidation_Checked(object sender, RoutedEventArgs e)
        {
            
            try
            {
                if (CDeclaration.IsBusy || CPerformDrop.IsBusy)
                {
                    MessageBox.ShowBox("MessageID368", BMC_Icon.Information);
                    e.Handled = false;
                    return;
                }
               chkLiquidation.IsEnabled = false;
                pnlDropContent.Children.Clear();
                CBatchLiquidation BatchLiquidation = new CBatchLiquidation();
                pnlDropContent.Children.Add(BatchLiquidation);
                BatchLiquidation.Margin = new Thickness(0);
                 
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error in Liquidation Tab Checked", LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
            }
            finally
            {
                chkLiquidation.IsEnabled = true;
            }
        }
    }
}
