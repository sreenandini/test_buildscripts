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
using BMC.CashDeskOperator;
using BMC.Transport;
using System.Timers;
using BMC.Presentation.POS.Helper_classes;

namespace BMC.Presentation
{
    /// <summary>
    /// Interaction logic for CashCounter.xaml
    /// </summary>
    public partial class CashCounter : UserControl, IDisposable
    {
        Timer countTimer = new Timer();
        public bool _IsPartDeclaration = false;
        public CashCounter()
        {
            this.InitializeComponent();
            _IsPartDeclaration = Security.SecurityHelper.HasAccess("BMC.Presentation.CDeclaration.PartCollectionDeclaration");
            SetCombo();
        }

        private void btnBegin_Click(object sender, RoutedEventArgs e)
        {
            countTimer.Elapsed += new ElapsedEventHandler(countTimer_Elapsed);
            countTimer.Start();
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            countTimer.Stop();
        }

        private void btnDeleteZero_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {

        }

        private void countTimer_Elapsed(object sender, ElapsedEventArgs e)
        {

        }

        private void SetCombo()
        {
            var machineTypeSource = (new CollectionHelper()).GetUndeclaredCollection(false, _IsPartDeclaration);

            var enumerator = new List<UndeclaredCollection>();
            var variable = new UndeclaredCollection
            {
                Collection_Batch_Name = "",
                Collection_Batch_No = -1,
                DisplayName = FindResource("PleaseSelectbatch") as string
            };
            enumerator.Add(variable);
            variable = new UndeclaredCollection
            {
                Collection_Batch_Name = "",
                Collection_Batch_No = 0,
                DisplayName = FindResource("PartCollections") as string
            };
            enumerator.Add(variable);

            foreach (var undeclaredCollection in machineTypeSource)
                enumerator.Add(undeclaredCollection);


            cboBatch.ItemsSource = enumerator;

            if (enumerator.Count > 2)
                cboBatch.SelectedIndex = 2;
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
                        this.btnBegin.Click -= (this.btnBegin_Click);
                        this.btnStop.Click -= (this.btnStop_Click);
                        this.btnDelete.Click -= (this.btnDelete_Click);
                        this.btnDeleteZero.Click -= (this.btnDeleteZero_Click);
                        this.btnSave.Click -= (this.btnSave_Click);
                    },
                    (c) =>
                    {
                    });
                    this.WriteLog("CashCounter objects are released successfully.");

                }
                disposed = true;
            }
        }

        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="CashCounter"/> is reclaimed by garbage collection.
        /// </summary>
        ~CashCounter()
        {
            Dispose(false);
        }

        #endregion
    }
}
