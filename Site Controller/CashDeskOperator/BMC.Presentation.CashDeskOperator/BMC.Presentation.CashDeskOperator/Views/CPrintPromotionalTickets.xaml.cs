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
using BMC.Business.CashDeskOperator;
using System.Data;
using BMC.Transport;
using System.Data.Linq;
using BMC.Transport.CashDeskOperatorEntity;
using BMC.Common.LogManagement;
using BMC.Common.Utilities;
using BMC.CashDeskOperator.BusinessObjects;
using Audit.Transport;
using Audit.BusinessClasses;
using BMC.Common.ExceptionManagement;
using BMC.Presentation.POS.Helper_classes;
using System.Globalization;
using System.Threading;
using System.ComponentModel;
namespace BMC.Presentation.POS.Views
{
    /// <summary>
    /// Interaction logic for PrintPromotionalTickets.xaml
    /// </summary>
    /// 
    public partial class CPrintPromotionalTickets : UserControl
    {

      
        bool blPauseTicketPrinting = false;
        private int _NoOfTickets = 0;
        private int _NoOfPrintedTickets = 0;
        private int _PromotionID = -1;
        private double _PromotionTicketAmount = 0.0;
        string strValue = string.Empty;
        DateTime _ExpiryDate = DateTime.Now;
        static string strProc = string.Empty;
        
        IIssueTicket objCashDeskOperator = IssueTicketBusinessObject.CreateInstance();
        private int _StartPrintTicket = 1;
        private ManualResetEvent _mreResume = null;
        private ManualResetEvent _mreShutdown = null;
      
        private Thread _printThread = null;
        private bool _isClosed = false;
        private int _SucceededTickets = 0;
        private int _FailureTickets = 0;
        private int _PromoTicketType = -1;
        private string _PromoName = string.Empty;
        private string _PromoTicketCount = string.Empty;
        string DisplayTicketPrintedSuccessMsg = string.Empty;
        double PromoTickAmt = 0;
        int PromCount = 0;

        private PromotionVouchers _parentPromo = null;



        public bool IsPrintedCompleted
        {
            get { return (bool)GetValue(IsPrintedCompletedProperty); }
            set { SetValue(IsPrintedCompletedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsPrintedCompleted.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsPrintedCompletedProperty =
            DependencyProperty.Register("IsPrintedCompleted", typeof(bool), typeof(CPrintPromotionalTickets),
            new UIPropertyMetadata(new PropertyChangedCallback(OnIsPrintedCompletedChanged)));


        private static void OnIsPrintedCompletedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            try
            {
                CPrintPromotionalTickets source = d as CPrintPromotionalTickets;
                if ((bool)e.OldValue != (bool)e.NewValue &&
                    (bool)e.NewValue)
                {
                    MainScreen.ActiveInstance.PromotionActiveElement = null;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        public int NoOfTickets
        {
            get
            {
                return _NoOfTickets;
            }
            set
            {
                _NoOfTickets = value;
            }
        }

        public int NoOfPrintedTickets
        {
            get
            {
                return _NoOfPrintedTickets;
            }
            set
            {
                _NoOfPrintedTickets = value;
            }
        }

        public int PromotionID
        {
            get
            {
                return _PromotionID;
            }
            set
            {
                _PromotionID = value;
            }
        }
        public double PromotionTicketAmount
        {
            get
            {
                return _PromotionTicketAmount;
            }
            set
            {
                _PromotionTicketAmount = value;
            }
        }

        public DateTime ExpiryDate
        {
            get
            {
                return _ExpiryDate;
            }
            set
            {
                _ExpiryDate = value;
            }
        }
        public int StartPrintTicket
        {
            get
            {
                return _StartPrintTicket;
            }
            set
            {
                _StartPrintTicket = value;
            }
        }

        public bool IsEverythingOK
        {
            get
            {
                return (_NoOfTickets == _NoOfPrintedTickets);
            }
        }
        public int SucceededTickets
        {
            get
            {
                return _SucceededTickets;
            }
            set
            {
                _SucceededTickets = value;
            }

        }

        public int FailureTickets
        {
            get
            {
                return _FailureTickets;
            }
            set
            {
                _FailureTickets = value;
            }

        }
        public int PromoTicketType
        {
            get
            {
                return _PromoTicketType;
            }
            set
            {
                _PromoTicketType = value;
            }

        }
        public string PromoName
        {
            get
            {
                return _PromoName;
            }
            set
            {
                _PromoName = value;
            }
        }
        public string PromoTicketCount
        {
            get
            {
                return _PromoTicketCount;
            }
            set
            {
                _PromoTicketCount = value;
            }
        }

       

        public CPrintPromotionalTickets()
        {
            InitializeComponent();
            _mreResume = new ManualResetEvent(true);
            _mreShutdown = new ManualResetEvent(false);

        }

        public CPrintPromotionalTickets(PromotionVouchers parentPromo)
            : this()
        {
            _parentPromo = parentPromo;
        }

        public void PrintValues(int iPromotionID, double dblPromoTicketAmount, DateTime dtPromoTickExp, int PromoTicketType, int TicketCount)
        {
            IssueTicketEntity issueTicketEntity = new IssueTicketEntity();
            this.PromotionID = iPromotionID;
            this.PromotionTicketAmount = dblPromoTicketAmount;
            this.ExpiryDate = dtPromoTickExp;
            this.PromoTicketType = PromoTicketType;
            this.PromCount = TicketCount;
            _mreShutdown.Reset();
            this.DisplayTicketPrintedSuccessMsg = Common.ConfigurationManagement.ConfigManager.Read("DisplayPromoTicketPrintMessage").ToString();
            LoadPromotionDetails(iPromotionID);
        }



        public void LoadPromotionDetails(int PromotionalID)
        {
            Promotional objPromotional = new Promotional();
            _NoOfPrintedTickets = 0;
            pbPrint.Value = _NoOfPrintedTickets;
            _NoOfTickets = objPromotional.BPromotionalTicketCount(PromotionalID);
            IsPrintedCompleted = false;
            _printThread = new Thread(new ThreadStart(this.PrintTickets));
            _printThread.Name = "PROMO_PRINT";
            _printThread.Start();
        }
        public void PrintTickets()
        {
            try
            {
                App application = App.Current as App;
                Thread.CurrentThread.CurrentUICulture = application.CurrentUICulture;
                Thread.CurrentThread.CurrentCulture = application.CurrentCulture;

                this.Dispatcher.Invoke(new Action(() =>
                {
                    pbPrint.Value = _NoOfPrintedTickets;
                    this.lblErrorMsg.Content = string.Empty;
                    this.lblPrintedTicket.Content = _NoOfPrintedTickets;
                    this.lblTotalTicket.Content = _NoOfTickets;
                    pbPrint.Minimum = _NoOfPrintedTickets;
                    pbPrint.Maximum = _NoOfTickets;
                }), System.Windows.Threading.DispatcherPriority.Normal);


                long lValue = 0;
                if (strValue != null && strValue != string.Empty)
                    lValue = Convert.ToInt64(strValue.GetSingleFromString() * 100);
                else
                    lValue = 0;

                this.Dispatcher.Invoke(new Action(() =>
                {
                    pbPrint.Value = _NoOfPrintedTickets;
                }), System.Windows.Threading.DispatcherPriority.Normal);

                Promotional objPromotional = new Promotional();

                try
                {
                    while ((_NoOfPrintedTickets < _NoOfTickets) && !_mreShutdown.WaitOne(1))
                    {
                        if (_mreResume.WaitOne())
                        {
                            if (_mreShutdown.WaitOne(10))
                            {
                                LogManager.WriteLog("Print screen was closed. So exiting now.", LogManager.enumLogLevel.Info);
                                return;
                            }
                        }

                        this.Dispatcher.Invoke(new Action(() =>
                        {

                            lblPrintedTicket.Content = _NoOfPrintedTickets.ToString();
                            lblTotalTicket.Content = _NoOfTickets.ToString();
                        }), System.Windows.Threading.DispatcherPriority.Normal);


                        if (true)
                        {

                            string strBarcode;
                            this.Dispatcher.Invoke(new Action(() =>
                            {
                                pbPrint.Value = _NoOfPrintedTickets;
                            }), System.Windows.Threading.DispatcherPriority.Normal);
                            
                            IssueTicketEntity issueTicketEntity = new IssueTicketEntity();
                            issueTicketEntity.Type = UIIssueTicketConstants.STANDARDTICKET;
                            if (PromoTicketType == 1)
                                issueTicketEntity.TicketHeader = "PLAYABLE VOUCHER";
                            else if (PromoTicketType == 0)
                                issueTicketEntity.TicketHeader = "CASHABLE PROMO VOUCHER";
                            else
                                issueTicketEntity.TicketHeader = "CASH DESK VOUCHER";

                            LogManager.WriteLog("Ticket Header : " + issueTicketEntity.TicketHeader, LogManager.enumLogLevel.Info);

                         //   issueTicketEntity.VoidDate = DateTime.Now.ToString();// (Convert.ToDateTime(this.ExpiryDate, new CultureInfo(ExtensionMethods.CurrentDateCulture))).ToString();
                            issueTicketEntity.VoidDate = (Convert.ToDateTime(this.ExpiryDate, new CultureInfo(ExtensionMethods.CurrentDateCulture))).ToString();
                            issueTicketEntity.dblValue = Convert.ToDouble(this.PromotionTicketAmount, new CultureInfo(ExtensionMethods.CurrentCurrenyCulture)); ;
                            issueTicketEntity.lnglValue = long.Parse((this.PromotionTicketAmount * 100).ToString());
                            System.TimeSpan ts = (ExpiryDate).Subtract(DateTime.Now);
                            issueTicketEntity.NumberOfDays = ts.Days;
                            // issueTicketEntity.Date = DateTime.Now;

                            string PrintedDateTime = string.Empty;
                            //PrintedDateTime = (Convert.ToDateTime(System.DateTime.Now, new CultureInfo(ExtensionMethods.CurrentDateCulture))).ToString();

                            issueTicketEntity.Date = DateTime.Now;//Convert.ToDateTime(PrintedDateTime);
                            PrintTicketErrorCodes PrintResult = objCashDeskOperator.IssueTicket(issueTicketEntity);
                            strBarcode = issueTicketEntity.BarCode;
                            this.Dispatcher.Invoke(new Action(() =>
                            {
                                this.lblErrorMsg.Visibility = Visibility.Visible;
                                this.pbPrint.Visibility = Visibility.Visible;
                                this.lblPrintedTicket.Visibility = Visibility.Visible;
                                this.lblTotalTicket.Visibility = Visibility.Visible;
                                this.btnCancel.Visibility = Visibility.Visible;
                                this.btnPause.Visibility = Visibility.Visible;


                                switch (PrintResult)
                                {
                                    case PrintTicketErrorCodes.OpenCOMPortFailure:
                                        {
                                            _FailureTickets++;
                                            //    MessageBox.ShowBox("MessageID295", BMC_Icon.Warning); //Unable to open COM Port. Please check connectivity.
                                            lblErrorMsg.Content = "Unable to open COM Port. Please check connectivity";
                                            return;
                                        }
                                    case PrintTicketErrorCodes.OutofPaper:
                                        {
                                            _FailureTickets++;
                                            //  MessageBox.ShowBox("MessageID294", BMC_Icon.Warning); //out of Paper
                                            lblErrorMsg.Content = "Out of Paper";
                                            return;
                                        }
                                    case PrintTicketErrorCodes.Success:
                                        {

                                            lblErrorMsg.Content = string.Empty;
                                            _SucceededTickets++;
                                            pbPrint.Value = _NoOfPrintedTickets;


                                            if (!String.IsNullOrEmpty(DisplayTicketPrintedSuccessMsg))
                                            {
                                                if (DisplayTicketPrintedSuccessMsg.ToUpper().Trim() == "TRUE")
                                                {
                                                    lblErrorMsg.Content = " Voucher printed successfully";
                                                }
                                            }
                                            _NoOfPrintedTickets += 1;


                                            pbPrint.Value = _NoOfPrintedTickets;
                                            lblPrintedTicket.Content = _NoOfPrintedTickets.ToString();
                                            int UpdateVoucherPromotionID = objPromotional.BUpdateVoucherPromotion(this.PromotionID, strBarcode);//Update PromotionalID in Voucher Table

                                            break;
                                        }
                                    case PrintTicketErrorCodes.eVoltErr:
                                        {
                                            _FailureTickets++;
                                            // MessageBox.ShowBox("MsgPromErr1", BMC_Icon.Error);
                                            lblErrorMsg.Content = "Voltage Error";

                                            return;
                                        }
                                    case PrintTicketErrorCodes.eHeadErr:
                                        {
                                            _FailureTickets++;
                                            //  MessageBox.ShowBox("MsgPromErr2", BMC_Icon.Error);
                                            lblErrorMsg.Content = " Printer Head Error";
                                            return;
                                        }
                                    case PrintTicketErrorCodes.ePaperOut:
                                        {
                                            _FailureTickets++;
                                            // MessageBox.ShowBox("MsgPromErr3", BMC_Icon.Error);
                                            lblErrorMsg.Content = "Out of Paper";
                                            return;
                                        }
                                    case PrintTicketErrorCodes.ePlatenUP:
                                        {
                                            _FailureTickets++;
                                            //  MessageBox.ShowBox("MsgPromErr4", BMC_Icon.Error);
                                            lblErrorMsg.Content = "Platen Up";
                                            return;
                                        }
                                    case PrintTicketErrorCodes.eSysErr:
                                        {
                                            _FailureTickets++;
                                            // MessageBox.ShowBox("MsgPromErr5", BMC_Icon.Error);
                                            lblErrorMsg.Content = "System Error";
                                            return;
                                        }
                                    case PrintTicketErrorCodes.eBusy:
                                        {
                                            _FailureTickets++;
                                            //   MessageBox.ShowBox("MsgPromErr6", BMC_Icon.Error);
                                            lblErrorMsg.Content = "Printer is Busy";
                                            return;
                                        }
                                    case PrintTicketErrorCodes.eJobMemOF:
                                        {
                                            _FailureTickets++;
                                            //  MessageBox.ShowBox("MsgPromErr7", BMC_Icon.Error);
                                            lblErrorMsg.Content = "Job Memory off";
                                            return;
                                        }
                                    case PrintTicketErrorCodes.eBufOF:
                                        {
                                            _FailureTickets++;
                                            // MessageBox.ShowBox("MsgPromErr8", BMC_Icon.Error);
                                            lblErrorMsg.Content = "Buffer off";
                                            return;
                                        }
                                    case PrintTicketErrorCodes.eLibLoadErr:
                                        {
                                            _FailureTickets++;
                                            //  MessageBox.ShowBox("MsgPromErr9", BMC_Icon.Error);
                                            lblErrorMsg.Content = "Library Load Error";
                                            return;
                                        }
                                    case PrintTicketErrorCodes.ePRDataErr:
                                        {
                                            _FailureTickets++;
                                            //   MessageBox.ShowBox("MsgPromErr10", BMC_Icon.Error);
                                            lblErrorMsg.Content = "Printer Data Error";
                                            return;
                                        }
                                    case PrintTicketErrorCodes.eLibRefErr:
                                        {
                                            _FailureTickets++;
                                            //  MessageBox.ShowBox("MsgPromErr11", BMC_Icon.Error);
                                            lblErrorMsg.Content = "Library Reference Error";
                                            return;
                                        }
                                    case PrintTicketErrorCodes.eTempErr:
                                        {
                                            _FailureTickets++;
                                            //   MessageBox.ShowBox("MsgPromErr12", BMC_Icon.Error);
                                            lblErrorMsg.Content = "Temp Error";
                                            return;
                                        }
                                    case PrintTicketErrorCodes.eMissingSupplyIndex:
                                        {
                                            _FailureTickets++;
                                            //   MessageBox.ShowBox("MsgPromErr13", BMC_Icon.Error);
                                            lblErrorMsg.Content = "Supply Index is Missing";
                                            return;
                                        }
                                    case PrintTicketErrorCodes.eFlashProgErr:
                                        {
                                            _FailureTickets++;
                                            //   MessageBox.ShowBox("MsgPromErr14", BMC_Icon.Error);
                                            lblErrorMsg.Content = "Flash Program Error";
                                            return;
                                        }
                                    case PrintTicketErrorCodes.ePaperInChute:
                                        {
                                            _FailureTickets++;
                                            //   MessageBox.ShowBox("MsgPromErr15", BMC_Icon.Error);
                                            lblErrorMsg.Content = "Paper in Chute";
                                            return;
                                        }
                                    case PrintTicketErrorCodes.ePrintLibCorr:
                                        {
                                            _FailureTickets++;
                                            //   MessageBox.ShowBox("MsgPromErr16", BMC_Icon.Error);
                                            lblErrorMsg.Content = "Print library is corrupted.";
                                            return;
                                        }
                                    case PrintTicketErrorCodes.eCmdErr:
                                        {
                                            _FailureTickets++;
                                            // MessageBox.ShowBox("MsgPromErr17", BMC_Icon.Error);
                                            lblErrorMsg.Content = "Command Error";
                                            return;
                                        }
                                    case PrintTicketErrorCodes.ePaperLow:
                                        {
                                            _FailureTickets++;
                                            //  MessageBox.ShowBox("MsgPromErr18", BMC_Icon.Error);
                                            lblErrorMsg.Content = "Paper low.";
                                            return;
                                        }
                                    case PrintTicketErrorCodes.ePaperJam:
                                        {
                                            _FailureTickets++;
                                            //   MessageBox.ShowBox("MsgPromErr19", BMC_Icon.Error);
                                            lblErrorMsg.Content = "Paper jammed.";
                                            return;
                                        }
                                    case PrintTicketErrorCodes.eCurrentErr:
                                        {
                                            _FailureTickets++;
                                            // MessageBox.ShowBox("MsgPromErr20", BMC_Icon.Error);
                                            lblErrorMsg.Content = "Current Error";
                                            return;
                                        }
                                    case PrintTicketErrorCodes.eJournalPrint:
                                        {
                                            _FailureTickets++;
                                            //  MessageBox.ShowBox("MsgPromErr21", BMC_Icon.Error);
                                            lblErrorMsg.Content = "Journal Print Error";
                                            return;
                                        }
                                    default:
                                        {
                                            _FailureTickets++;
                                            //   MessageBox.ShowBox("MessageID102", BMC_Icon.Warning);
                                            lblErrorMsg.Content = "Unable to Print Voucher";
                                            return;
                                        }
                                }



                            }), System.Windows.Threading.DispatcherPriority.Normal);


                            this.Dispatcher.Invoke(new Action(() =>
                            {
                                this.lblPrintedTicket.Content = _NoOfPrintedTickets.ToString();
                                this.lblTotalTicket.Content = _NoOfTickets.ToString();
                                UpdateProgressBarDelegate updatePbDelegate = new UpdateProgressBarDelegate(pbPrint.SetValue);
                            }), System.Windows.Threading.DispatcherPriority.Normal);


                            this.Dispatcher.Invoke(new Action(() =>
                            {
                                btnCancel.IsEnabled = true;

                            }), System.Windows.Threading.DispatcherPriority.Normal);
                            System.Threading.Thread.Sleep(1000);
                        }
                        PromoTickAmt = Convert.ToDouble(_PromotionTicketAmount);
                        PromCount = Convert.ToInt32(_NoOfTickets);

                        double TotalAmoount = PromCount * PromoTickAmt;
                        if (_NoOfPrintedTickets == _NoOfTickets)
                        {
                            int UpdateVoucherPrintSuccess = objPromotional.MarkPromotionalTicketsAsValid(this.PromotionID, 1);
                            this.Dispatcher.Invoke(new Action(() =>
                            {
                                this.lblErrorMsg.Content = string.Empty;
                                pbPrint.Value = _NoOfPrintedTickets;
                                MessageBox.ShowBox("MessageID435", BMC_Icon.Information);

                            }), System.Windows.Threading.DispatcherPriority.Normal);

                            AuditViewerBusiness.InsertAuditData(new Audit.Transport.Audit_History
                            {
                                AuditModuleName = ModuleName.Promotion,
                                Audit_Screen_Name = "Promo Print",
                                Audit_Desc = "Promo Name: " + PromoName + "; TicketAmount: " + _PromotionTicketAmount.ToString() + "; Number of Tickets: " + PromCount.ToString() + "; Total Promotion Amount: " + TotalAmoount.ToString() + "; Expiry Date: " + ExpiryDate,
                                AuditOperationType = OperationType.ADD,
                                Audit_New_Vl = PromoName
                            });


                            this.Dispatcher.Invoke(new Action(() =>
                                                   {
                                                       IsPrintedCompleted = true;
                                                   }), System.Windows.Threading.DispatcherPriority.Normal);
                        }

                    }

                }
                catch (Exception ex)
                {
                    ExceptionManager.Publish(ex);
                    LogManager.WriteLog("Promotional Print Tickets : " + ex.Message, LogManager.enumLogLevel.Error);

                }
                finally
                {

                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
        private void ucValueCalc_ValueChanged(object sender, RoutedEventArgs e)
        {
            try
            {
                strValue = Convert.ToString(((ValueCalcComp)sender).txtDisplay.Text);

            }
            catch (Exception ex)
            {
                LogManager.WriteLog(strProc + ex.Message, LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
            }

        }
        private void Audit(string sDesc)
        {
            AuditViewerBusiness.InsertAuditData(new Audit.Transport.Audit_History
            {
                AuditModuleName = ModuleName.Promotion,
                Audit_Screen_Name = "Vouchers|PrintVoucher",
                Audit_Desc = sDesc,
                AuditOperationType = OperationType.ADD
            });
        }


        private void btnPause_Click(object sender, RoutedEventArgs e)
        {
            blPauseTicketPrinting = !blPauseTicketPrinting;

            if (blPauseTicketPrinting == true)
            {

                btnPause.Content = "Resume";
                _mreResume.Reset();
            }
            else
            {
                btnPause.Content = "Pause";
                _mreResume.Set();
            }
            if (btnPause.Content == "Pause")
                btnCancel.IsEnabled = true;
            else if (btnPause.Content == "Resume")
                btnCancel.IsEnabled = false;

            pbPrint.Value = _NoOfPrintedTickets;


            
        }
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                System.Windows.Forms.DialogResult dr;
                dr = MessageBox.ShowBox("MessageID413", BMC_Icon.Question, BMC_Button.YesNo);

                if (dr.ToString().ToUpper() == "NO")
                {
                    //continue printing  
                    _mreResume.Set();
                }
                else
                {

                    _mreShutdown.Set();
                    _isClosed = true;

                    double TotalAmoount = PromCount * PromoTickAmt;
                    AuditViewerBusiness.InsertAuditData(new Audit.Transport.Audit_History
                    {
                        AuditModuleName = ModuleName.Promotion,
                        Audit_Screen_Name = "Promo Print Cancelled",
                        Audit_Desc = "Promo Name: " + PromoName + "; TicketAmount: " + _PromotionTicketAmount + "; Number of Tickets: " + _PromoTicketCount + "; Total Promotion Amount: " + TotalAmoount + "; Expiry Date: " + ExpiryDate,
                        AuditOperationType = OperationType.ADD,
                        Audit_New_Vl = PromoName

                    });
                   
                    IsPrintedCompleted = true;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        void CPrintPromotionalTickets_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

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
                        // this.Closing -= (CPrintPromotionalTickets_Closing);
                    },
                    (c) =>
                    {
                    });
                    this.WriteLog("ClientMessage objects are released successfully.");

                }
                disposed = true;
            }
        }

        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="CPrintPromotionalTickets"/> is reclaimed by garbage collection.
        /// </summary>
        ~CPrintPromotionalTickets()
        {
            Dispose(false);
        }

        #endregion


        
    }


}


