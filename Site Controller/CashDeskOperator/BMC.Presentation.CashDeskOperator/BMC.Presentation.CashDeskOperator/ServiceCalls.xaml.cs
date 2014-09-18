using System;
using System.ComponentModel;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using BMC.Common.ExceptionManagement;
using BMC.CashDeskOperator.BusinessObjects;
using Audit.BusinessClasses;
using Audit.Transport;
using BMC.Presentation.POS.Helper_classes;

namespace BMC.Presentation.POS
{
    /// <summary>
    /// Interaction logic for ServiceCalls.xaml
    /// </summary>
    public partial class ServiceCalls : Window, IDisposable
    {
        public int InstallationNo;
        public string Position;
        public string Asset;
        public string Game;
        public string Manufacturer;
        public string SerialNo;
        DataTable dtServiceFaults;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;


        public ServiceCalls()
        {
            InitializeComponent();
            MessageBox.childOwner = this;
            backgroundWorker1 = new BackgroundWorker(); 
            backgroundWorker1.DoWork +=
               new DoWorkEventHandler(backgroundWorker1_DoWork);
            backgroundWorker1.RunWorkerCompleted +=
                new RunWorkerCompletedEventHandler(
            backgroundWorker1_RunWorkerCompleted);
            backgroundWorker1.ProgressChanged +=
                new ProgressChangedEventHandler(
            backgroundWorker1_ProgressChanged);

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            lblAsset.Text  = Asset;
            lblGame.Text  = Game;
            lblManufacturer.Text = Manufacturer;
            lblSerialNo.Text = SerialNo;
            progressBar1.IsIndeterminate = false;
            Duration duration = new Duration(TimeSpan.FromSeconds(10));
            DoubleAnimation doubleanimation = new DoubleAnimation(100.0, duration);
            this.Cursor = Cursors.Wait;
            progressBar1.BeginAnimation(ProgressBar.ValueProperty, doubleanimation);
            
            backgroundWorker1.RunWorkerAsync();
           
        }

        private void btnExit_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// Creates dynamic buttons according to the faults imported from enterprise
        /// </summary>
        private void LoadServiceFaults(DataTable serviceFault)
        {
            try
            {
                if (serviceFault != null)
                {
                    if (serviceFault.Rows.Count > 0)
                    {
                        foreach (DataRow Dr in serviceFault.Rows)
                        {
                            Button B = new Button();
                            B.Name = String.Concat("Fault_", Dr[1].ToString());
                            B.Tag = Dr[1].ToString();
                            grdServiceCalls.RegisterName(String.Concat("Fault_", Dr[1].ToString()), B);
                            B.Margin = new Thickness(10, 0, 0, 0);
                            B.Style = (Style)FindResource("BMC_Button");
                            B.VerticalAlignment = VerticalAlignment.Top;
                            B.HorizontalAlignment = HorizontalAlignment.Right;
                            B.Content = Dr[2].ToString();
                            B.Click += new RoutedEventHandler(LogCashDeskFaultEvent);
                            grdServiceCalls.Children.Add(B);
                        }
                    }
                }
            }
            catch(Exception e)
            {
                ExceptionManager.Publish(e);
            }
        }

        /// <summary>
        /// Gets faults from enterprise
        /// </summary>
        /// <returns></returns>
        private void GetCashDeskServiceFaults()
        {

            IFieldService fieldService = FieldServiceBusinessObject.CreateInstance();
            dtServiceFaults = fieldService.GetCashdeskServiceFaults();
        }


        /// <summary>
        /// Logs fault event
        /// </summary>
        /// <param name="Sender"></param>
        /// <param name="args"></param>
        private void LogCashDeskFaultEvent(object Sender,RoutedEventArgs args)
        {
            string strMsg = string.Empty;
            IFieldService fieldService = FieldServiceBusinessObject.CreateInstance();
            
            strMsg = fieldService.LogSiteEvent(InstallationNo, Int32.Parse(((Button)Sender).Tag.ToString()));

            if (strMsg.Contains("Open service already exists."))
                MessageBox.ShowBox("MessageID139");
            else if (strMsg == string.Empty)
            {
                MessageBox.ShowBox("MessageID140");
                
                AuditViewerBusiness.InsertAuditData(new Audit.Transport.Audit_History
                {
                    
                    AuditModuleName = ModuleName.FieldServices,
                    Audit_Screen_Name = "Position Details|Field Services",
                    Audit_Desc = "An error occured while logging a Service call.",
                    AuditOperationType = OperationType.ADD,
                    Audit_Slot = Asset
                });
            }
            else
            {
                MessageBox.ShowBox("MessageID141", ((Button)Sender).Content.ToString());

                AuditViewerBusiness.InsertAuditData(new Audit.Transport.Audit_History
                {
                    
                    AuditModuleName = ModuleName.FieldServices,
                    Audit_Screen_Name = "Position Details|Field Services",
                    Audit_Desc = "Service Call has been logged in for" + ((Button)Sender).Content.ToString(),
                    AuditOperationType = OperationType.ADD,
                    Audit_Slot = Asset
                });

                this.Close();
            }
        }


        
        # region Background Process

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            GetCashDeskServiceFaults();
        }

        // This event handler deals with the results of the
        // background operation.
        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // First, handle the case where an exception was thrown.
            if (e.Error != null)
                MessageBox.ShowBox(e.Error.Message, BMC_Icon.Error);
            else if (e.Cancelled)
                MessageBox.ShowBox("MessageID142", BMC_Icon.Error);
            else
            {
                lblStatus.Visibility = Visibility.Hidden;
                progressBar1.Visibility = Visibility.Hidden;                
                LoadServiceFaults(dtServiceFaults);
                
            }
            this.Cursor = Cursors.Arrow;
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.progressBar1.Value = e.ProgressPercentage;
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
                        ((BMC.Presentation.POS.ServiceCalls)(this)).Loaded -= (this.Window_Loaded);
                        this.btnExit.Click -= (this.btnExit_Click_1);
                    },
                    (c) =>
                    {
                    });
                    this.WriteLog("ServiceCalls objects are released successfully.");

                }
                disposed = true;
            }
        }

        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="ServiceCalls"/> is reclaimed by garbage collection.
        /// </summary>
        ~ServiceCalls()
        {
            Dispose(false);
        }

        #endregion

    }
}
