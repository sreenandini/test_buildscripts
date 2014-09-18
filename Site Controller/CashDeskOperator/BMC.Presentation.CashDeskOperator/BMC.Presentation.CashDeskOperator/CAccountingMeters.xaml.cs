using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BMC.CashDeskOperator.BusinessObjects;
using BMC.Presentation.POS.Helper_classes;

namespace BMC.Presentation.POS
{
    /// <summary>
    /// Interaction logic for CAccountingMeters.xaml
    /// </summary>
    public partial class CAccountingMeters : Window, IDisposable
    {
        private string Display = string.Empty;
        private int Record;
        private int Hour;
        private string actDate = string.Empty;
        IDrop dropBusinessObject = DropBusinessObject.CreateInstance();

        public CAccountingMeters()
        {
            InitializeComponent();
        }

        public CAccountingMeters(string date, string DisplayType, int Record_No, int Hour_No)
        {
            InitializeComponent();
            Display = DisplayType;
            Record = Record_No;
            Hour = Hour_No;
            actDate = date;
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void LoadData(object sender, RoutedEventArgs e)
        {
            DataTable dtMeters;
            dtMeters = dropBusinessObject.GetMeterList(Display, Record, Hour);
            lblRecordType.Content = Display;
            lblType.Content = Display + " ID";
            lblTypeValue.Content = Record;
            lblDate.Content = actDate;
            if ((dtMeters != null) && (dtMeters.Rows.Count > 0))
            {
                lblAsset.Content = dtMeters.Rows[0]["Stock_No"].ToString();
                lblGame.Content = dtMeters.Rows[0]["Name"].ToString();
                lblPos.Content = dtMeters.Rows[0]["Bar_Position"].ToString();
            }
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
                        this.listMeters.Loaded -= (this.LoadData);
                        this.btnExit.Click -= (this.btnExit_Click);
                    },
                    (c) =>
                    {
                    });
                    this.WriteLog("CAccountingMeters objects are released successfully.");

                }
                disposed = true;
            }
        }

        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="CAccountingMeters"/> is reclaimed by garbage collection.
        /// </summary>
        ~CAccountingMeters()
        {
            Dispose(false);
        }

        #endregion
    }
}
