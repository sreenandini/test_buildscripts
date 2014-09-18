using System;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Threading;
using BMC.Common.Utilities;
using BMC.Transport;
using BMC.Transport.CashDeskOperatorEntity;
using BMC.CashDeskOperator.BusinessObjects;
using BMC.Common.ConfigurationManagement;
using BMC.Common.ExceptionManagement;
using System.Globalization;
using BMC.Presentation.POS.Helper_classes;
using Audit.BusinessClasses;
using Audit.Transport;
using BMC.Common.LogManagement;
using BMC.Presentation.POS.UserControls;
using BMC.Security;
using BMC.Business.CashDeskOperator;
 


namespace BMC.Presentation
{
    /// <summary>
    /// Interaction logic for CPpTicket.xaml
    /// <author> Melvi Miranda</author>
    /// <date>12/03/2012</date>
    /// </summary>
    public partial class CExceptionVoucher  
    {
        DispatcherTimer disptimerRedeem;
        public delegate void ScannerClick(object sender, RoutedEventArgs e);

        public CExceptionVoucher()
        {
            InitializeComponent();
            this.txtStatus.GotFocus += new RoutedEventHandler(txtStatus_GotFocus);
            
        }

        void txtStatus_GotFocus(object sender, RoutedEventArgs e)
        {
            this.ucValueCalc.txtDisplay.Focus();
        }

        void disptimerRedeem_Tick(object sender, EventArgs e)
        {
            disptimerRedeem.Stop();
        }

        private void btnVerify_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                string ticketNumber = this.ucValueCalc.txtDisplay.Text.Trim();

                //check if ticketstring is numeric
                double Num;
                bool isNum = double.TryParse(ticketNumber, out Num);

                
                btnVerify.IsEnabled = false;

                if (isNum && (ticketNumber.IndexOf('.') < 0) && this.ucValueCalc.txtDisplay.Text.Trim().Length > 0)
                {
                    ExceptionVoucher objExceptionVoucher = new ExceptionVoucher();
                    

                    this.txtStatus.Visibility = Visibility.Visible;
                    this.txtStatus.Text = Application.Current.FindResource("MessageID401") as string;


                    //LogManager.WriteLog("Checking Voucher:" + ticketNumber+" is PP Ticket", LogManager.enumLogLevel.Info);
                    if (objExceptionVoucher.IsExceptionVoucher(ticketNumber) == 1)
                    {
                        
                        this.txtStatus.Text = Application.Current.FindResource("MessageID221") as string;

                        
                        if (MessageBox.ShowBox("MessageID395", BMC_Icon.Warning, BMC_Button.YesNo) == System.Windows.Forms.DialogResult.Yes)
                        {
                            disptimerRedeem.IsEnabled = true;
                            disptimerRedeem.Start();

                            //******Check for user access rights.
                            CAuthorize objAuthorize = null;
                            objAuthorize = new CAuthorize("BMC.Presentation.CPpTicket");
                            objAuthorize.User = Security.SecurityHelper.CurrentUser;
                            if (!Security.SecurityHelper.HasAccess("BMC.Presentation.CPpTicket"))
                            {
                                objAuthorize.ShowDialog();
                                if (!objAuthorize.IsAuthorized)
                                {
                                    ClearAll();
                                    return;
                                }
                            }
                            else
                            {
                                objAuthorize.IsAuthorized = true;
                            }

                            //Go ahead and Mark the ticket as active
                            if (objExceptionVoucher.MarkExceptionVoucherActive(ticketNumber) == -1)
                            {
                                //Unsuccessfull in Activating PP ticket :(
                                //LogManager.WriteLog("Unsuccessfull in Activating PP Voucher:" + ticketNumber + "!!!", LogManager.enumLogLevel.Info);                                
                                MessageBox.ShowBox("MessageID397", BMC_Icon.Error, BMC_Button.OK);
                            }
                            else
                            {
                                //Success in Activating PP ticket! :)
                                //LogManager.WriteLog("Success in Activating PP Voucher:" + ticketNumber + "!!!", LogManager.enumLogLevel.Info);                                
                                disptimerRedeem.Stop();
                                this.txtStatus.Text = Application.Current.FindResource("MessageID402") as string;
                                MessageBox.ShowBox("MessageID396", BMC_Icon.Information, BMC_Button.OK);

                                //****Audit code***
                                #region "Audit log for PP ticket"

                                if (objAuthorize != null && objAuthorize.IsAuthorized)
                                {
                                    // Modified description, removed field & new value for Exception Voucher
                                    AuditViewerBusiness.InsertAuditData(new Audit.Transport.Audit_History
                                    {
                                        AuditModuleName = ModuleName.Exception_Voucher,
                                        Audit_Screen_Name = "Exception Voucher",
                                        Audit_Desc = "Voucher Number - " + ticketNumber + " activated by " + objAuthorize.User.DisplayName,
                                        AuditOperationType = OperationType.MODIFY,
                                    });
                                }
                                #endregion

                            }

                            disptimerRedeem.Stop();
                            this.txtStatus.Visibility = Visibility.Hidden;

                        }
                        else
                        {
                            disptimerRedeem.Stop();
                            this.txtStatus.Visibility = Visibility.Hidden;
                        }

                    }
                    else // you will enter here if its not an exception voucher 
                    {
                        disptimerRedeem.Start();
                        this.txtStatus.Text = Application.Current.FindResource("MessageID398") as string;
                        MessageBox.ShowBox("MessageID399", BMC_Icon.Information, BMC_Button.OK, ticketNumber);
                        
                        this.ucValueCalc.txtDisplay.Focus();
                        disptimerRedeem.Stop();
                        this.txtStatus.Visibility = Visibility.Hidden;
                    }
                    

                }//  Outer most IF ends here
                else // enter here if Voucher number has anything other than numerals
                {                    
                    if (ticketNumber.Length > 0)//Show message only if there is something in the textbox
                    {
                        disptimerRedeem.Start();
                        this.txtStatus.Text = Application.Current.FindResource("MessageID403") as string;
                        MessageBox.ShowBox("MessageID400", BMC_Icon.Error, BMC_Button.OK);
                        this.ucValueCalc.txtDisplay.Focus();
                        disptimerRedeem.Stop();
                    }                    
                    this.txtStatus.Visibility = Visibility.Hidden;
                }
                

            }//try ends here
            

            catch (Exception ex)
            {
                BMC.Common.ExceptionManagement.ExceptionManager.Publish(ex);
            }
            finally
            {
                ClearAll();
                btnVerify.IsEnabled = true;
            }
        }

        /// <summary>
        /// Clears all the contents of the form.
        /// </summary>
        public void ClearAll()
        {
            this.txtStatus.Text = "";
            this.txtStatus.Visibility = Visibility.Hidden;
            this.txtStatus.Background = Brushes.White;
            this.ucValueCalc.txtDisplay.Text = "";
            this.ucValueCalc.Focus();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.ucValueCalc.txtDisplay.Clear();
            this.txtStatus.Visibility = Visibility.Hidden;

            disptimerRedeem = new DispatcherTimer();
            disptimerRedeem.Interval = new TimeSpan(0, 0, 10);
            disptimerRedeem.Tick += new EventHandler(disptimerRedeem_Tick);
            disptimerRedeem.Stop();
            disptimerRedeem.IsEnabled = false;

            this.ucValueCalc.ClickEvent_ExceptionVoucher += new ScannerClick(btnVerify_Click);
            this.ucValueCalc.txtDisplay.Focus();          
        }

        void ucValueCalc_ClickClearEvent(object sender)
        {
            ClearAll();
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
                        this.UserControl.Loaded -= (this.UserControl_Loaded);
                        this.btnVerify.Click -= (this.btnVerify_Click);                        
                    },
                    (c) =>
                    {
                    });
                    this.WriteLog("CRedeemTicket objects are released successfully.");

                }
                disposed = true;
            }
        }

        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="CRedeemTicket"/> is reclaimed by garbage collection.
        /// </summary>
        ~CExceptionVoucher()
        {
            Dispose(false);
        }

        #endregion

   

    }
}
