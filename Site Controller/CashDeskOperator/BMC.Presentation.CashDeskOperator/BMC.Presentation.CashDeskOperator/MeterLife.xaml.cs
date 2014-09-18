using System;
using System.Windows;
using BMC.Presentation.POS.Helper_classes;

namespace BMC.Presentation.POS
{
    /// <summary>
    /// Interaction logic for MeterLife.xaml
    /// </summary>
    public partial class MeterLife : Window, IDisposable
    {
        public int InstallationNo;
        public string Position;
        public string Asset;
        public string Game;
        public DateTime InstallationStartDate;
        public DateTime InstallationStartTime;

        public MeterLife()
        {
            InitializeComponent();
        }
        public MeterLife(int InstallationNo)
        {
            InitializeComponent();
            MessageBox.childOwner = this;
            this.InstallationNo = InstallationNo;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            lblPosition.Content = Position;
            lblAsset.Content = Asset;
            lblGame.Content = Game;
            lblStartDate.Content = InstallationStartDate.ToLongDateString() + " " + InstallationStartDate.ToShortTimeString();
            lblCurrentDate.Content = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToShortTimeString();
            UCMeterLife.InstallationNo = InstallationNo;
            UCMeterLife.WaitFillListView();
        }



        private void btnExit_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
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
                        ((BMC.Presentation.POS.MeterLife)(this)).Loaded -= (this.Window_Loaded);
                        this.btnExit.Click -= (this.btnExit_Click_1);
                    },
                    (c) =>
                    {
                    });
                    this.WriteLog("MeterLife objects are released successfully.");

                }
                disposed = true;
            }
        }

        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="MeterLife"/> is reclaimed by garbage collection.
        /// </summary>
        ~MeterLife()
        {
            Dispose(false);
        }

        #endregion
    }
}
