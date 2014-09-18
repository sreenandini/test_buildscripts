using System.Windows;
using BMC.CashDeskOperator.BusinessObjects;
using BMC.Presentation.POS.Helper_classes;
using System;
using BMC.Common.LogManagement;

namespace BMC.Presentation
{
    /// <summary>
    /// Interaction logic for CPositionCurrentStatus.xaml
    /// </summary>
    public partial class CPositionCurrentStatus : IDisposable
    {
        readonly IEnrollment _iPosition = EnrollmentBusinessObject.CreateInstance();

        public CPositionCurrentStatus()
        {
            InitializeComponent();
            GetCurrentPositionDetails();
        }

        public void GetCurrentPositionDetails()
        {
            var resultSet = _iPosition.GetPositionCurrentStatus(chkAll.IsChecked.Value, chkVLTAAMSStatus.IsChecked.Value, chkVLTAAMSVerification.IsChecked.Value, chkGameInstallAAMS.IsChecked.Value, chkGameVerification.IsChecked.Value, chkGameEnableAAMS.IsChecked.Value, chkAAMSEnableDiable.IsChecked.Value, chkBMCEnterpriseStatus.IsChecked.Value);
            Helper_classes.Common.BindListView(resultSet, lstPosCurrent);
        }

        private void CheckBoxes_Checked(object sender, RoutedEventArgs e)
        {
            GetCurrentPositionDetails();
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
                        // events
                        this.chkAAMSEnableDiable.Click -= (this.CheckBoxes_Checked);
                        this.chkBMCEnterpriseStatus.Click -= (this.CheckBoxes_Checked);
                        this.chkVLTAAMSStatus.Click -= (this.CheckBoxes_Checked);
                        this.chkVLTAAMSVerification.Click -= (this.CheckBoxes_Checked);
                        this.chkGameInstallAAMS.Click -= (this.CheckBoxes_Checked);
                        this.chkGameVerification.Click -= (this.CheckBoxes_Checked);
                        this.chkAll.Click -= (this.CheckBoxes_Checked);
                    },
                    (c) =>
                    {
                    });
                    LogManager.WriteLog("|=> CPositionCurrentStatus objects are released successfully.", LogManager.enumLogLevel.Info);
                }
                disposed = true;
            }
        }

        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="CPositionCurrentStatus"/> is reclaimed by garbage collection.
        /// </summary>
        ~CPositionCurrentStatus()
        {
            Dispose(false);
        }

        #endregion
    }
}
