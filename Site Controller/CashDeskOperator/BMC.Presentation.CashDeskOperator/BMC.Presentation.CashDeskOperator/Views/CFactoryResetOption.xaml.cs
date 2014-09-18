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
using BMC.Common.LogManagement;
using BMC.CashDeskOperator.BusinessObjects;
using BMC.Transport.CashDeskOperatorEntity;
using BMC.Common.ConfigurationManagement;
using BMC.Presentation.POS.Helper_classes;

namespace BMC.Presentation.POS.Views
{
    /// <summary>
    /// Interaction logic for CFactoryResetOption.xaml
    /// </summary>
    public partial class CFactoryResetOption : Window
    {
        private CFactoryReset _FactoryReset = null;

        public CFactoryResetOption()
        {
            InitializeComponent();
            _FactoryReset = CFactoryReset.FactoryResetInstance;
            pnlFactoryReset.Child = _FactoryReset;
            _FactoryReset.Margin = new Thickness(0);
            chkResetAccountInfo.IsChecked = true;
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            CFactoryReset.instance = null;
            Hide();
        }

        private void chkResetAccountInfo_Checked(object sender, RoutedEventArgs e)
        {
            _FactoryReset.FactoryReset = FactoryResetMode.ResetAccountInformation;
        }

        private void chkInitialConfig_Checked(object sender, RoutedEventArgs e)
        {
            _FactoryReset.FactoryReset = FactoryResetMode.ResetToInitailConfiguration;
        }

        private void chkMasterReset_Checked(object sender, RoutedEventArgs e)
        {
            _FactoryReset.FactoryReset = FactoryResetMode.MasterReset;
        }        

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
                        this.chkInitialConfig.Checked -= (this.chkInitialConfig_Checked);
                        this.chkMasterReset.Checked -= (this.chkMasterReset_Checked);
                        this.chkResetAccountInfo.Checked -= (this.chkResetAccountInfo_Checked);
                    },
                    (c) =>
                    {
                    });
                    LogManager.WriteLog("|=> CTickets objects are released successfully.", LogManager.enumLogLevel.Info);
                }
                disposed = true;
            }
        }

        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="CTickets"/> is reclaimed by garbage collection.
        /// </summary>
        ~CFactoryResetOption()
        {
            Dispose(false);
        }

        #endregion
   
    }
}
