using System;
using System.Data;
using System.Windows;
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
using BMC.CashDeskOperator;
using BMC.Business.CashDeskOperator;
using BMC.Presentation.POS.Views;
using System.Windows.Input;
using BMC.DBInterface.CashDeskOperator;
using System.Collections.Generic;

namespace BMC.Presentation
{

    public partial class CVoidTicket
    {
        public delegate void ScannerClick(object sender, RoutedEventArgs e);
        public delegate void ClickClear(object sender);
        private long Custid = 0;
        private string strKeyText = "";
        //private int redeemTicketAmount = 0;
        private bool bisTicketExpired = false;
        private bool isScannerFired = false;

        public CVoidTicket()
        {
            this.InitializeComponent();
            lblVoidAmountValue.Visibility = Visibility.Hidden;
            lblVoidAmount.Visibility = Visibility.Hidden;
        }


        private void button_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                int InstallationNumber = 0;
                int ValidationLength = 0;
                string _Barcode = this.ucValueCalc.txtDisplay.Text.Trim();
                System.Windows.Forms.DialogResult _diagResult;
                btnVerify.IsEnabled = false;
                if (isScannerFired) //check done not to fire the verify event twice while verifying a ticket using scanner
                {
                    isScannerFired = false;
                    return;
                }
                if ((sender is System.Windows.Controls.TextBox))
                    isScannerFired = true;
                else
                    isScannerFired = false;


                if (this.ucValueCalc.txtDisplay.Text.Trim().Length > 0)
                {
                    //8-digit ticket validation.
                    //Slot ticket cannot be redeemed via cashdesk.
                    if (this.ucValueCalc.txtDisplay.Text.Trim().Length != 18)
                    {
                        LinqDataAccessDataContext linqDBExchange = new LinqDataAccessDataContext(CommonDataAccess.ExchangeConnectionString);
                        IEnumerable<InstallationFromTicket> InstallationTicket = linqDBExchange.GetInstallationNumber(_Barcode);
                        if (InstallationTicket != null)
                        {
                            foreach (var item in InstallationTicket)
                            {
                                InstallationNumber = item.installation_no.Value;
                                _Barcode = item.strbarcode;

                            }
                            DataTable InstallationDetails = (new CommonDataAccess()).GetInstallationDetails(0, InstallationNumber, false, false);
                            if (InstallationDetails.Rows.Count > 0)
                            {
                                int.TryParse(InstallationDetails.Rows[0]["Validation_length"].ToString(), out ValidationLength);
                                if (ValidationLength != _Barcode.Length)
                                {
                                    MessageBox.ShowBox("MessageID403", BMC_Icon.Error);
                                    return;
                                }
                            }                          
                        }
                    }

                    TicketsHelper objTicketsHelper = new TicketsHelper();

                    int? nResult = -1;
                    decimal dAmount = 0;
                    int iTransactionNo = 0;
                    int iSequenceNo = 0;

                    //foreach (var obj in objTicketsHelper.ValidateVoidVoucher(this.ucValueCalc.txtDisplay.Text.Trim(), ref nResult))
                    //{
                    //    dAmount = Convert.ToDecimal(obj.iAmount)/100;                        
                    //}
                    dAmount = objTicketsHelper.ValidateVoidVoucher(_Barcode, Security.SecurityHelper.CurrentUser.User_No, ref nResult);

                    switch (nResult)
                    {
                        case 0:
                            {
                                if (Convert.ToBoolean(AppSettings.REDEEM_TICKET_POP_UP_ALERT_VISIBILITY))
                                {
                                    //Message: Are you sure you want to Void the Voucher?
                                    _diagResult = MessageBox.ShowBox("MessageID512", BMC_Icon.Question, BMC_Button.YesNo, this.ucValueCalc.txtDisplay.Text.Trim(), Convert.ToString(dAmount));
                                    if (_diagResult == System.Windows.Forms.DialogResult.No)
                                        return;
                                }
                                else
                                {
                                    _diagResult = System.Windows.Forms.DialogResult.Yes;
                                }
                                if (_diagResult == System.Windows.Forms.DialogResult.Yes)
                                {

                                    string barcode = this.ucValueCalc.txtDisplay.Text.Trim();

                                    foreach (var obj in objTicketsHelper.UpdateVoidVoucher(barcode,
                                                           System.Environment.MachineName,
                                                           Security.SecurityHelper.CurrentUser.User_No,
                                                           txtNotes.Text))
                                    {
                                        iTransactionNo = (int)obj.iTransactionNo;
                                        iSequenceNo = obj.TE_ID;
                                    }

                                    // TIS Printed Tickets
                                    if (VoucherHelper.IsTISPrintedTicket(barcode))
                                    {
                                        VoucherHelper.SendTISVoidTicket(barcode, Security.SecurityHelper.CurrentUser.User_No);
                                    }

                                    //Message: "Voucher Voided Successfully."
                                    MessageBox.ShowBox("MessageID513", BMC_Icon.Information);
                                    //Receipt                                
                                    BMC.Business.CashDeskOperator.Reports objReports = new BMC.Business.CashDeskOperator.Reports();
                                    string sCode = BMC.Transport.Settings.SiteCode;
                                    using (CReportViewer objReportViewer = new CReportViewer())
                                    {
                                        objReportViewer.PrintVoidVoucherReceipt(System.Environment.MachineName, this.ucValueCalc.txtDisplay.Text.Trim(),
                                                                                dAmount, iTransactionNo, sCode, iSequenceNo);
                                        //objReportViewer.ShowDialog();
                                    }

                                    Audit(this.ucValueCalc.txtDisplay.Text.Trim(), dAmount, "Voucher Voided Successfully");
                                    lblVoidAmountValue.Visibility = Visibility.Visible;
                                    lblVoidAmount.Visibility = Visibility.Visible;
                                    lblVoidAmountValue.Text = string.Empty;
                                    lblVoidAmountValue.Text = dAmount.ToString("0.00");


                                }
                                break;
                            }


                        case 1:
                            {
                                //Voucher Not Found
                                //Message: "Voucher is not available in System"
                                MessageBox.ShowBox("MessageID514", BMC_Icon.Error);
                                Audit(this.ucValueCalc.txtDisplay.Text.Trim(), dAmount, "Voucher Not Found");
                                lblVoidAmount.Visibility = Visibility.Hidden;
                                lblVoidAmountValue.Visibility = Visibility.Hidden;
                                //Audit
                                break;
                            }
                        case 2:
                            {
                                // Invalid for Slots, only CashDesk is allowed.
                                //Message: "Void is applicable only for Cash Desk Vouchers"
                                MessageBox.ShowBox("MessageID515", BMC_Icon.Error);
                                Audit(this.ucValueCalc.txtDisplay.Text.Trim(), dAmount, "Void is applicable only for Cash Desk Vouchers");
                                //Audit
                                lblVoidAmount.Visibility = Visibility.Hidden;
                                lblVoidAmountValue.Visibility = Visibility.Hidden;
                                break;
                            }
                        case 3:
                            {
                                //PAID Status
                                // Message: "Voucher has already been redeemed."
                                MessageBox.ShowBox("MessageID516", BMC_Icon.Error);
                                Audit(this.ucValueCalc.txtDisplay.Text.Trim(), dAmount, "Voucher has already been redeemed");
                                lblVoidAmount.Visibility = Visibility.Hidden;
                                lblVoidAmountValue.Visibility = Visibility.Hidden;
                                //Audit
                                break;
                            }
                        case 4:
                            {
                                //Already Voided
                                // Message: "Voucher has already been Voided."
                                MessageBox.ShowBox("MessageID517", BMC_Icon.Error);
                                Audit(this.ucValueCalc.txtDisplay.Text.Trim(), dAmount, "Voucher has already been Voided");
                                lblVoidAmount.Visibility = Visibility.Hidden;
                                lblVoidAmountValue.Visibility = Visibility.Hidden;
                                //Audit
                                break;
                            }
                        case 5:
                            {
                                //Expired Date
                                // Message: "Voucher has expired."
                                MessageBox.ShowBox("MessageID518", BMC_Icon.Error);
                                Audit(this.ucValueCalc.txtDisplay.Text.Trim(), dAmount, "Voucher has expired");
                                lblVoidAmount.Visibility = Visibility.Hidden;
                                lblVoidAmountValue.Visibility = Visibility.Hidden;
                                //Audit
                                break;
                            }

                    }


                }
                else
                {
                    //Message: "Please enter or scan a valid Voucher Number
                    MessageBox.ShowBox("MessageID519", BMC_Icon.Warning);
                    this.ucValueCalc.txtDisplay.Focus();
                }

                ClearAll();

            }
            catch (Exception ex)
            {
                MessageBox.ShowBox("MessageID520", BMC_Icon.Error);
                BMC.Common.ExceptionManagement.ExceptionManager.Publish(ex);
                ClearAll();
            }
            finally
            {
                btnVerify.IsEnabled = true;
            }
        }


        private void Audit(string sVoucherNumber, decimal dAmount, string sDescription)
        {
            AuditViewerBusiness.InsertAuditData(new Audit.Transport.Audit_History
                           {

                               AuditModuleName = ModuleName.VoidVoucher,
                               Audit_Screen_Name = "Vouchers|VoidVoucher",
                               Audit_Desc = sDescription,
                               AuditOperationType = OperationType.MODIFY,
                               Audit_Field = "Voucher Number:CashDesk location:Amount:Reason",
                               Audit_New_Vl = sVoucherNumber + ":" + System.Environment.MachineName + ":" + dAmount.ToString("C", CultureInfo.CurrentCulture) + ":" + this.txtNotes.Text
                           });
        }



        /// <summary>
        /// Clears all the contents of the form.
        /// </summary>
        public void ClearAll()
        {
            this.txtNotes.Text = "";
            this.ucValueCalc.txtDisplay.Text = "";
            this.ucValueCalc.Focus();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

            this.ucValueCalc.vClickClearEvent += new ClickClear(ucValueCalc_ClickClearEvent);

            this.ucValueCalc.vClickEvent += new ScannerClick(button_Click);

            this.ucValueCalc.txtDisplay.Focus();
        }

        void ucValueCalc_ClickClearEvent(object sender)
        {
            ClearAll();
            lblVoidAmountValue.Visibility = Visibility.Collapsed;
            lblVoidAmount.Visibility = Visibility.Collapsed;
        }

        private void txt_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (!Settings.OnScreenKeyboard)
                    return;
                System.Windows.Controls.TextBox txtMouseUp = sender as System.Windows.Controls.TextBox;
                txtMouseUp.Text = DisplayKeyboard(txtMouseUp.Text);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        public string DisplayKeyboard(string keyText)
        {
            strKeyText = "";
            KeyboardInterface objKeyboard = null;

            try
            {
                Window w = Window.GetWindow(this);
                Point pt = default(Point);
                Size sz = default(Size);
                if (w != null)
                {
                    pt = new Point(w.Left, w.Top);
                    sz = new Size(w.Width, w.Height);
                }

                objKeyboard = new KeyboardInterface();
                objKeyboard.Owner = w;
                objKeyboard.Closing += new System.ComponentModel.CancelEventHandler(objKeyboard_Closing);
                objKeyboard.KeyString = keyText;
                objKeyboard.Top = pt.Y + (sz.Height - objKeyboard.Height);
                objKeyboard.Left = pt.X + (sz.Width / 2) - (objKeyboard.Width / 2);
                objKeyboard.ShowInTaskbar = false;
                objKeyboard.ShowDialog();

                if (objKeyboard != null)
                {
                    objKeyboard.Closing -= this.objKeyboard_Closing;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            finally
            {

            }
            return strKeyText;
        }

        void objKeyboard_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                LogManager.WriteLog("Inside objKeyboard_Closing", LogManager.enumLogLevel.Info);

                if (((KeyboardInterface)sender).DialogResult == true)
                {
                    strKeyText = ((KeyboardInterface)sender).KeyString;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
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
                        this.UserControl.Loaded -= (this.UserControl_Loaded);
                        this.btnVerify.Click -= (this.button_Click);
                    },
                    (c) =>
                    {
                    });
                    this.WriteLog("CVoidTicket objects are released successfully.");

                }
                disposed = true;
            }
        }

        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="CRedeemTicket"/> is reclaimed by garbage collection.
        /// </summary>
        ~CVoidTicket()
        {
            Dispose(false);
        }

        #endregion
    }
}