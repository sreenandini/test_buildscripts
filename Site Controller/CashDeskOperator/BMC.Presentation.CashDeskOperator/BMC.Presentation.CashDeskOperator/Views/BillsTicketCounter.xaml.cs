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

using BMC.Common.Utilities;
using BMC.CashDeskOperator;
using BMC.Business.CashDeskOperator;
using NoteCumTktScanLib;

using BMC.Presentation.POS.Helper_classes;
using BMC.Common.ExceptionManagement;
using BMC.Common.LogManagement;
using BMC.Transport;
using BMC.Security;

namespace BMC.Presentation.POS.Views
{
    /// <summary>
    /// Interaction logic for BillsTicketCounter.xaml
    /// </summary>
    public partial class BillsTicketCounter : IDisposable
    {
        private bool bStartClicked = false;
        private bool bNoteCounterFinished = false;
        public Dictionary<string, decimal> BillsDict = new Dictionary<string, decimal>();

        public  Int32 _CurrentCollectionIndex = 1;
        CollectionHelper _objCollectionHelper = null;
        ExchangeHelper _objExchangeHelper = null;
        TicketsHelper _objTicketsHelper = null;
        NoteCumTktScanLib.CNoteCumTktScan objNoteCumTktScanLib = new NoteCumTktScanLib.CNoteCumTktScan();
        bool bBillsAvailable = false;
        private string sNotes = string.Empty;
        private string sTickets = string.Empty;
        List<Bills> objBills = new List<Bills>();
        int _CollectionNo = 0;
        int nTotal = 0;
        List<UndeclaredCollectionRecord> lstCollections;
        public IEnumerable<ParentVoucher.ValidVouchers> ValidTicketsHolder;
        public List<ParentVoucher.ValidVouchers> lsValidTicketsHolder;
        BillDenoms oBillDenoms;
        DeclarationFilterBy _filterBy = DeclarationFilterBy.None;

        string _filterValue = string.Empty;
        int _batchNo;
        string _SiteCode;
        private string ExchangeConnectionString;
        private string TicketingConnectionString;
        #region Public Variables
        public int iCollectionNo = 0;
        public string sPosition = string.Empty;
        bool bIsUncommitted = false;

        #endregion

        public BillsTicketCounter()
        {
            InitializeComponent();
            MessageBox.childOwner = this;
            btnExceptionTickets.Visibility = (Settings.HANDLE_EXCEPTIONTICKETS_COUNTER) ? Visibility.Visible : Visibility.Collapsed;
            if (!String.IsNullOrEmpty(ManualCashEntry.sSiteCode))
            {
                txtHeader.Text += "\t\t\t\t" + Application.Current.FindResource("ManualCashEntry_xaml_lblSiteCode").ToString().Trim() + " " + ManualCashEntry.sSiteCode;
            }
            _objCollectionHelper = new CollectionHelper();
            _objExchangeHelper = new ExchangeHelper();
            _objTicketsHelper = new TicketsHelper();
        }

        public BillsTicketCounter(string ExchangeConn, string TicketingConn)
            : this()
        {
            LogManager.WriteLog("BillsTicketCounter:BillsTicketCounter() Entry", LogManager.enumLogLevel.Info);
            LogManager.WriteLog("BillsTicketCounter:BillsTicketCounter() Site Code : " + ManualCashEntry.sSiteCode, LogManager.enumLogLevel.Debug);
            ExchangeConnectionString = ExchangeConn;
            TicketingConnectionString = TicketingConn;
            _objCollectionHelper = new CollectionHelper(ExchangeConn);
            _objExchangeHelper = new ExchangeHelper(ExchangeConn);
            _objTicketsHelper = new TicketsHelper(TicketingConn);
            LogManager.WriteLog("BillsTicketCounter:BillsTicketCounter() Exit", LogManager.enumLogLevel.Info);
        }

        public BillsTicketCounter(int batch, DeclarationFilterBy filterBy, string filterValue, string SiteCode, int CurrentIndex)
        {
            InitializeComponent();
            LogManager.WriteLog("BillsTicketCounter:BillsTicketCounter() Entry", LogManager.enumLogLevel.Info);
            LogManager.WriteLog("BillsTicketCounter:BillsTicketCounter() Site Code : "+SiteCode, LogManager.enumLogLevel.Debug);
            try
            {
                _CurrentCollectionIndex = CurrentIndex;
                //CollectionHelper _collectionHelper = new CollectionHelper();
                this._filterBy = filterBy;
                this._filterValue = filterValue;
                this._batchNo = batch;
                this._SiteCode = SiteCode;
                _objCollectionHelper = new CollectionHelper();
                _objExchangeHelper = new ExchangeHelper();
                _objTicketsHelper = new TicketsHelper();
                btnExceptionTickets.Visibility = (Settings.HANDLE_EXCEPTIONTICKETS_COUNTER) ? Visibility.Visible : Visibility.Collapsed;
                RefreshData(_CurrentCollectionIndex);

                //this.txtAsset.Text = lstCollections[1].AssetNo;
                //this.txtPosition.Text = lstCollections[1].Position;
                //this.iCollectionNo = lstCollections[1].CollectionNo;
                //_CollectionNo = lstCollections[1].CollectionNo;
                //this.sPosition = lstCollections[1].Position;
                //this.txtGame.Text = "";

                MessageBox.childOwner = this;

                if (!String.IsNullOrEmpty(ManualCashEntry.sSiteCode))
                {
                    txtHeader.Text += "\t\t\t\t" + Application.Current.FindResource("ManualCashEntry_xaml_lblSiteCode").ToString().Trim() + " " + _SiteCode;
                }
                bool IsCounterEnabled = _objCollectionHelper.IsNoteCounterVisible();
                if (!IsCounterEnabled)
                {
                    btnApply.Visibility = Visibility.Hidden;
                    btnStart.Visibility = Visibility.Hidden;
                    btnClearAll.Visibility = Visibility.Hidden;
                }

            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
            LogManager.WriteLog("BillsTicketCounter:BillsTicketCounter() Exit", LogManager.enumLogLevel.Info);

        }

        public BillsTicketCounter(int batch, DeclarationFilterBy filterBy, string filterValue, string SiteCode, string ExchangeConn, string TicketingConn, int CollectionIndex)
            : this(batch, filterBy, filterValue, SiteCode, CollectionIndex)
        {
            LogManager.WriteLog("BillsTicketCounter:BillsTicketCounter() Entry", LogManager.enumLogLevel.Info);
            LogManager.WriteLog("BillsTicketCounter:BillsTicketCounter() Site Code : " + SiteCode, LogManager.enumLogLevel.Debug);
            ExchangeConnectionString = ExchangeConn;
            TicketingConnectionString = TicketingConn;
            _objCollectionHelper = new CollectionHelper(ExchangeConn);
            _objExchangeHelper = new ExchangeHelper(ExchangeConn);
            _objTicketsHelper = new TicketsHelper(TicketingConn);
            _CurrentCollectionIndex = CollectionIndex;
            RefreshData(_CurrentCollectionIndex);
            LogManager.WriteLog("BillsTicketCounter:BillsTicketCounter() Exit", LogManager.enumLogLevel.Info);


        }
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (bIsUncommitted)
                {
                    RevertExceptionTickets();
                    bIsUncommitted = false;
                }

                if (bStartClicked)
                    objNoteCumTktScanLib.CloseSerialCom();
                this.DialogResult = false;
                this.Close();
            }
            catch (Exception Ex)
            {

                ExceptionManager.Publish(Ex);
            }
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (bIsUncommitted)
                {
                    RevertExceptionTickets();
                    bIsUncommitted = false;
                }

                if (bStartClicked)
                    objNoteCumTktScanLib.CloseSerialCom();
                this.DialogResult = false;
                this.Close();
            }
            catch (Exception Ex)
            {

                ExceptionManager.Publish(Ex);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {

                txtGame.Text = _objExchangeHelper.GetGameName(txtAsset.Text);
                btnExceptionTickets.Visibility = Visibility.Collapsed;
            }
            catch (Exception Ex)
            {

                ExceptionManager.Publish(Ex);
            }
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (bStartClicked)
                {

                    bIsUncommitted = true;
                    //string sNotesString = "TICKET:100100000000429685,TICKET:100100000000434979,TICKET:100100000000567875,TICKET:100100000000573265,TICKET:101000010000013362,TICKET:101000011000011440,TICKET:101000011000025478,TICKET:101000011000035880,TICKET:101000011000040730,TICKET:101000011000056618,ONES:1,TWOS:0,FIVES:1,TENS:1,TWENTIES:1,FIFTIES:1,HUNDREDS:1,";
                    //string sNotesString = "TICKET:101200004000214537,ONES:1,TWENTIES:1,FIFTIES:1,";

                    string sNotesString = string.Empty;

                    LogManager.WriteLog("CloseSerialCom() CALL", LogManager.enumLogLevel.Info);
                    objNoteCumTktScanLib.CloseSerialCom();
                    LogManager.WriteLog("CloseSerialCom() ACK", LogManager.enumLogLevel.Info);

                    LogManager.WriteLog("GetString() CALL", LogManager.enumLogLevel.Info);
                    objNoteCumTktScanLib.GetString(out sNotesString);
                    LogManager.WriteLog("GetString() ACK", LogManager.enumLogLevel.Info);

                    LogManager.WriteLog(sNotesString, LogManager.enumLogLevel.Info);

                   //if (_CurrentCollectionIndex==1)
                   // sNotesString = "TICKET:155500502000094416,TICKET:155500509000091859,TICKET:155500512000098672,ONES:1,TWOS:0,FIVES:1,TENS:1,TWENTIES:1,FIFTIES:1,HUNDREDS:1";
                   //else
                   // sNotesString = "TICKET:155500506000096290,TICKET:155500519000134672,TICKET:155500522000090600";
                    //sNotesString = "TICKET:155500554000092150,TICKET:155500559000111809,TICKET:155500904000133427,TICKET:55500560000090821,TICKET:155500754000094164,TICKET:155500776000092048";
                    //sNotesString = "TICKET:555500003000186248,TICKET:555500003000173743,TICKET:555500003000161115,TICKET:555500003000158641,TICKET:555500003000140721,TICKET:555500002000137694,TICKET:555500003000126343,TICKET:555500003000117037,TICKET:555500003000105065,TICKET:555500003000094567";
                    //sNotesString = "TICKET:555500003000042162,TICKET:102000002000084357,TICKET:102000000000141315,TICKET:555500000000083959,TICKET:101200282000074881,TICKET:102000000000132924,TICKET:101200282000069405,TICKET:101200282000059680,TICKET:101200000005945148,TICKET:101200000005930052";
                    bStartClicked = false;
                    bNoteCounterFinished = true;
                    btnStart.Content = Application.Current.FindResource("BillsTicketCounter_xaml_btnStart") as string;//"Start Counter";


                    string[] stemp = sNotesString.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    string[] NameValuePair;

                    string sTemp = string.Empty;
                    string sCurrencySymbol = sTemp.GetCurrencySymbol();


                    foreach (var Entry in stemp)
                    {
                        NameValuePair = Entry.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);

                        switch (NameValuePair[0])
                        {
                            case "TICKET":
                                {
                                    sTickets += "," + NameValuePair[1];
                                    break;
                                }
                            case "ONES":
                                {
                                    if (!BillsDict.ContainsKey("ONES"))
                                        BillsDict.Add("ONES", Convert.ToInt64(NameValuePair[1]));
                                    else
                                        BillsDict["ONES"] = Convert.ToInt64(BillsDict["ONES"]) + Convert.ToInt64(NameValuePair[1]);
                                    break;
                                }
                            case "TWOS":
                                {
                                    if (!BillsDict.ContainsKey("TWOS"))
                                        BillsDict.Add("TWOS", Convert.ToInt64(NameValuePair[1]) * 2);
                                    else
                                        BillsDict["TWOS"] = Convert.ToInt64(BillsDict["TWOS"]) + (Convert.ToInt64(NameValuePair[1]) * 2);

                                    break;
                                }
                            case "FIVES":
                                {
                                    if (!BillsDict.ContainsKey("FIVES"))
                                        BillsDict.Add("FIVES", Convert.ToInt64(NameValuePair[1]) * 5);
                                    else
                                        BillsDict["FIVES"] = Convert.ToInt64(BillsDict["FIVES"]) + (Convert.ToInt64(NameValuePair[1]) * 5);

                                    break;
                                }
                            case "TENS":
                                {
                                    if (!BillsDict.ContainsKey("TENS"))
                                        BillsDict.Add("TENS", Convert.ToInt64(NameValuePair[1]) * 10);
                                    else
                                        BillsDict["TENS"] = Convert.ToInt64(BillsDict["TENS"]) + (Convert.ToInt64(NameValuePair[1]) * 10);
                                    break;
                                }
                            case "TWENTIES":
                                {
                                    if (!BillsDict.ContainsKey("TWENTIES"))
                                        BillsDict.Add("TWENTIES", Convert.ToInt64(NameValuePair[1]) * 20);
                                    else
                                        BillsDict["TWENTIES"] = Convert.ToInt64(BillsDict["TWENTIES"]) + (Convert.ToInt64(NameValuePair[1]) * 20);
                                    break;
                                }
                            case "FIFTIES":
                                {
                                    if (!BillsDict.ContainsKey("FIFTIES"))
                                        BillsDict.Add("FIFTIES", Convert.ToInt64(NameValuePair[1]) * 50);
                                    else
                                        BillsDict["FIFTIES"] = Convert.ToInt64(BillsDict["FIFTIES"]) + (Convert.ToInt64(NameValuePair[1]) * 50);
                                    break;
                                }
                            case "HUNDREDS":
                                {
                                    if (!BillsDict.ContainsKey("HUNDREDS"))
                                        BillsDict.Add("HUNDREDS", Convert.ToInt64(NameValuePair[1]) * 100);
                                    else
                                        BillsDict["HUNDREDS"] = Convert.ToInt64(BillsDict["HUNDREDS"]) + (Convert.ToInt64(NameValuePair[1]) * 100);
                                    break;
                                }
                            case "TWO_HUNDREDS":
                                {
                                    if (!BillsDict.ContainsKey("TWO_HUNDREDS"))
                                        BillsDict.Add("TWO_HUNDREDS", Convert.ToInt64(NameValuePair[1]) * 200);
                                    else
                                        BillsDict["TWO_HUNDREDS"] = Convert.ToInt64(BillsDict["HUNDREDS"]) + (Convert.ToInt64(NameValuePair[1]) * 200);
                                    break;
                                }
                            case "FIVE_HUNDREDS":
                                {
                                    if (!BillsDict.ContainsKey("FIVE_HUNDREDS"))
                                        BillsDict.Add("FIVE_HUNDREDS", Convert.ToInt64(NameValuePair[1]) * 500);
                                    else
                                        BillsDict["FIVE_HUNDREDS"] = Convert.ToInt64(BillsDict["HUNDREDS"]) + (Convert.ToInt64(NameValuePair[1]) * 500);
                                    break;
                                }
                            default:
                                break;
                        }
                    }

                    if (BillsDict.Count > 0)
                    {
                        objBills = new List<Bills>();
                        nTotal = 0;

                        string[] sCurrencyList;

                        //if (ExtensionMethods.CurrentSiteCulture == "it-IT")
                        //    sCurrencyList = new string[] { "FIVES", "TENS", "TWENTIES", "FIFTIES", "HUNDREDS", "TWO_HUNDREDS", "FIVE_HUNDREDS" };
                        //else
                        //    sCurrencyList = new string[] { "ONES", "TWOS", "FIVES", "TENS", "TWENTIES", "FIFTIES", "HUNDREDS" };

                        switch (ExtensionMethods.CurrentSiteCulture)
                        {
                            case "it-IT":
                                sCurrencyList = new string[] { "FIVES", "TENS", "TWENTIES", "FIFTIES", "HUNDREDS", "TWO_HUNDREDS", "FIVE_HUNDREDS" };
                                break;
                            case "es-ar":
                                sCurrencyList = new string[] { "TWOS", "FIVES", "TENS", "TWENTIES", "FIFTIES", "HUNDREDS", "TWO_HUNDREDS", "FIVE_HUNDREDS" };
                                break;
                            default:
                                sCurrencyList = new string[] { "ONES", "TWOS", "FIVES", "TENS", "TWENTIES", "FIFTIES", "HUNDREDS" };
                                break;
                        }

                        oBillDenoms = new BillDenoms();

                        foreach (var Denom in sCurrencyList)
                        {
                            if (BillsDict.ContainsKey(Denom))
                            {
                                nTotal += Convert.ToInt32(BillsDict[Denom]);
                                switch (Denom)
                                {
                                    case "ONES":
                                        {
                                            objBills.Add(new Bills
                                            {
                                                Bill = sCurrencySymbol + " 1",
                                                Count = "",
                                                //Value = sCurrencySymbol + " " + BillsDict[Denom].ToString()
                                                Value = BillsDict[Denom].ToString()

                                            });
                                            oBillDenoms.ONES = BillsDict[Denom].ToString();
                                            break;
                                        }
                                    case "TWOS":
                                        {
                                            objBills.Add(new Bills
                                            {
                                                Bill = sCurrencySymbol + " 2",
                                                Count = "",
                                                Value = BillsDict[Denom].ToString()
                                                //Value = sCurrencySymbol + " " + BillsDict[Denom].ToString()
                                            });
                                            oBillDenoms.TWOS = BillsDict[Denom].ToString();
                                            break;
                                        }
                                    case "FIVES":
                                        {
                                            objBills.Add(new Bills
                                            {
                                                Bill = sCurrencySymbol + " 5",
                                                Count = "",
                                                Value = BillsDict[Denom].ToString()
                                                //Value = sCurrencySymbol + " " + BillsDict[Denom].ToString()
                                            });
                                            oBillDenoms.FIVES = BillsDict[Denom].ToString();
                                            break;
                                        }
                                    case "TENS":
                                        {
                                            objBills.Add(new Bills
                                            {
                                                Bill = sCurrencySymbol + " 10",
                                                Count = "",
                                                Value = BillsDict[Denom].ToString()
                                                //Value = sCurrencySymbol + " " + BillsDict[Denom].ToString()
                                            });
                                            oBillDenoms.TENS = BillsDict[Denom].ToString();
                                            break;
                                        }
                                    case "TWENTIES":
                                        {
                                            objBills.Add(new Bills
                                            {
                                                Bill = sCurrencySymbol + " 20",
                                                Count = "",
                                                Value = BillsDict[Denom].ToString()
                                                //Value = sCurrencySymbol + " " + BillsDict[Denom].ToString()
                                            });
                                            oBillDenoms.TWENTIES = BillsDict[Denom].ToString();
                                            break;
                                        }
                                    case "FIFTIES":
                                        {
                                            objBills.Add(new Bills
                                            {
                                                Bill = sCurrencySymbol + " 50",
                                                Count = "",
                                                Value = BillsDict[Denom].ToString()
                                                //Value = sCurrencySymbol + " " + BillsDict[Denom].ToString()
                                            });
                                            oBillDenoms.FIFTIES = BillsDict[Denom].ToString();
                                            break;
                                        }
                                    case "HUNDREDS":
                                        {
                                            objBills.Add(new Bills
                                            {
                                                Bill = sCurrencySymbol + " 100",
                                                Count = "",
                                                Value = BillsDict[Denom].ToString()
                                                //Value = sCurrencySymbol + " " + BillsDict[Denom].ToString()
                                            });
                                            oBillDenoms.HUNDREDS = BillsDict[Denom].ToString();
                                            break;
                                        }
                                    case "TWO_HUNDREDS":
                                        {
                                            objBills.Add(new Bills
                                            {
                                                Bill = sCurrencySymbol + " 200",
                                                Count = "",
                                                Value = BillsDict[Denom].ToString()
                                                //Value = sCurrencySymbol + " " + BillsDict[Denom].ToString()
                                            });
                                            oBillDenoms.TWO_HUNDREDS = BillsDict[Denom].ToString();
                                            break;
                                        }
                                    case "FIVE_HUNDREDS":
                                        {
                                            objBills.Add(new Bills
                                            {
                                                Bill = sCurrencySymbol + " 500",
                                                Count = "",
                                                Value = BillsDict[Denom].ToString()
                                                //Value = sCurrencySymbol + " " + BillsDict[Denom].ToString()
                                            });
                                            oBillDenoms.FIVE_HUNDREDS = BillsDict[Denom].ToString();
                                            break;
                                        }
                                    default:
                                        break;

                                }
                            }
                        }

                        if (objBills.Count > 0)
                        {
                            bBillsAvailable = true;
                            lvBills.ItemsSource = objBills;
                            if (nTotal > 0)
                                txtTotalBills.Text = sCurrencySymbol + " " + nTotal.ToString();
                            lblcounterWarning.Text = string.Empty;
                        }

                    }
                    else
                        //MessageBox.ShowBox("MessageID357", BMC_Icon.Error);
                        lblcounterWarning.Text = "*" + Application.Current.FindResource("MessageID357") as string;

                    if (!sTickets.IsNullOrEmpty())
                    {
                        this.RefreshTickets();
                    }
                    else
                    {
                        //MessageBox.ShowBox("MessageID347", BMC_Icon.Error);
                        if (bBillsAvailable == false)
                        {
                            lblcounterWarning.Text = lblcounterWarning.Text + ((lblcounterWarning.Text.Length != 0) ? "   *" : string.Empty) + Application.Current.FindResource("MessageID347") as string;
                            return; // Return here to avoid showing save changes message in case no bill are voucher found. 
                        }
                        else
                        {
                            lblcounterWarning.Text = Application.Current.FindResource("MessageID347") as string;
                        }
                        //lblcounterWarning.Visibility = Visibility.Collapsed;
                    }
                    lblcounterWarning.Text = lblcounterWarning.Text + ((lblcounterWarning.Text.Length != 0) ? "   *" : string.Empty) + Application.Current.FindResource("MessageID430") as string;
                }
                else
                {
                    bNoteCounterFinished = false;
                    btnExceptionTickets.Visibility = Visibility.Collapsed;

                    LogManager.WriteLog("OpenSerialComPort() CALL", LogManager.enumLogLevel.Info);
                    if (objNoteCumTktScanLib.OpenSerialComPort(Settings.BillVoucherCounterCOMPort) != 0)
                    {
                        LogManager.WriteLog("OpenSerialComPort() NACK", LogManager.enumLogLevel.Info);
                        LogManager.WriteLog("CloseSerialCom() CALL", LogManager.enumLogLevel.Info);
                        objNoteCumTktScanLib.CloseSerialCom();
                        LogManager.WriteLog("CloseSerialCom() ACK", LogManager.enumLogLevel.Info);
                        MessageBox.ShowBox("MessageID295", BMC_Icon.Error);
                        return;//MessageBox unable to open COM Port-MessageID295

                    }
                    if (ExtensionMethods.CurrentSiteCulture == "en-US")
                    {
                        objNoteCumTktScanLib.SetDenom(1); //en-US-1, Euro-2,
                        LogManager.WriteLog("SetDenom(1) en-US", LogManager.enumLogLevel.Info);
                    }
                    else
                    {
                        objNoteCumTktScanLib.SetDenom(2);
                        LogManager.WriteLog("SetDenom(2) it-IT", LogManager.enumLogLevel.Info);
                    }

                    LogManager.WriteLog("OpenSerialComPort() ACK", LogManager.enumLogLevel.Info);
                    LogManager.WriteLog("StartRead() CALL", LogManager.enumLogLevel.Info);
                    objNoteCumTktScanLib.StartRead();
                    LogManager.WriteLog("StartRead() ACK", LogManager.enumLogLevel.Info);
                    bStartClicked = true;
                    btnStart.Content = Application.Current.FindResource("BillsTicketCounter_xaml_btnStop") as string;//"Stop Counter";
                    //Message:Please remove the Bills & Vouchers from the Currency Counter.
                    //MessageBox.ShowBox("MessageID352", BMC_Icon.Information, BMC_Button.OK);
                    lblcounterWarning.Text = Application.Current.FindResource("MessageID352") as string;

                }
            }
            catch (Exception Ex)
            {
                LogManager.WriteLog("Exception Thrown is Start/Stop Button Click", LogManager.enumLogLevel.Debug);
                //MessageBox.ShowBox("MessageID295", BMC_Icon.Error);
                if (!string.IsNullOrEmpty(Ex.Message))
                {
                    MessageBox.ShowBox(Ex.Message, BMC_Icon.Error, true);
                }
                ExceptionManager.Publish(Ex);
                btnStart.Content = "Start Counter";

            }
            finally
            {
                btnStart.IsEnabled = true;
                this.IsCounterStarted = bStartClicked;
            }
        }

        private void RefreshTickets()
        {
            //Tickets List View
            sTickets = sTickets.TrimStart(new char[] { ',' });


            System.Data.Linq.IMultipleResults Result = _objTicketsHelper.GetVouchers(sTickets, txtAsset.Text);

            ValidTicketsHolder = Result.GetResult<ParentVoucher.ValidVouchers>();
            lsValidTicketsHolder = ValidTicketsHolder.ToList();
            lvTickets.ItemsSource = lsValidTicketsHolder; //objTicketsHelper.GetTicketDetailsList(sTickets);


            List<ParentVoucher.InValidVouchers> invalidVouchersList = Result.GetResult<ParentVoucher.InValidVouchers>().ToList();
            lvInValidTickets.ItemsSource = invalidVouchersList;
            btnExceptionTickets.Visibility = (Settings.HANDLE_EXCEPTIONTICKETS_COUNTER && (invalidVouchersList.Count > 0)) ? Visibility.Visible : Visibility.Collapsed;

            foreach (var value in Result.GetResult<ParentVoucher.ValidVouchersQty>())
            {
                txtQuantity.Text = value.Quantity.ToString();
                txtTotal.Text = (Convert.ToDecimal(value.Total) / 100).GetUniversalCurrencyFormatWithSymbol();
            }

            foreach (var value in Result.GetResult<ParentVoucher.InValidVouchersQty>())
            {
                txtQuantityIn.Text = value.Quantity.ToString();
            }
        }

        private void btnApply_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (bStartClicked)
                {
                    //objNoteCumTktScanLib.CloseSerialCom();
                    MessageBox.ShowBox("MessageID355", BMC_Icon.Error);
                    return;
                }

                if ((lsValidTicketsHolder == null || lsValidTicketsHolder.Count == 0) && (BillsDict == null || BillsDict.Count == 0))
                {
                    lblcounterWarning.Text = Application.Current.FindResource("MessageID356") as string;
                    return;
                }

                if (MessageBox.ShowBox("MessageID354", BMC_Icon.Question, BMC_Button.YesNo) == System.Windows.Forms.DialogResult.No)
                    return;

                //this.DialogResult = true;
                //this.Close();
                SaveAll();
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
        }

        private void btnExceptionTickets_Click(object sender, RoutedEventArgs e)
        {
            try
            {


                CDeclareExceptionTickets tickets = (!Security.SecurityHelper.HasAccess("BMC.Presentation.CommonCDOforDeclaration")) ? new CDeclareExceptionTickets(sPosition, iCollectionNo, sTickets) : new CDeclareExceptionTickets(sPosition, iCollectionNo, sTickets, ExchangeConnectionString, TicketingConnectionString);

                tickets.Owner = this;
                tickets.ShowDialog();
                if (tickets.IsProcessClickedOnce)
                {
                    btnExceptionTickets.Visibility = Visibility.Collapsed;
                    if (bNoteCounterFinished &&
                        !sTickets.IsNullOrEmpty())
                    {
                        this.RefreshTickets();
                    }
                }

            }
            catch (Exception Ex)
            {

                ExceptionManager.Publish(Ex);
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
                        ((BMC.Presentation.POS.Views.BillsTicketCounter)(this)).Loaded -= (this.Window_Loaded);
                        this.btnStart.Click -= (this.btnStart_Click);
                        this.btnApply.Click -= (this.btnApply_Click);
                        this.btnCancel.Click -= (this.btnCancel_Click);
                        this.btnExit.Click -= (this.btnExit_Click);
                    },
                    (c) =>
                    {
                    });
                    this.WriteLog("BillsTicketCounter objects are released successfully.");

                }
                disposed = true;
            }
        }

        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="BillsTicketCounter"/> is reclaimed by garbage collection.
        /// </summary>
        ~BillsTicketCounter()
        {
            Dispose(false);
        }

        #endregion


        private void btnPrevious_Click(object sender, RoutedEventArgs e)
        {
            if (bStartClicked)
            {
                //objNoteCumTktScanLib.CloseSerialCom();
                MessageBox.ShowBox("MessageID355", BMC_Icon.Error);
                return;
            }

            if (bIsUncommitted)
            {
                RevertExceptionTickets();
                bIsUncommitted = false;
            }
            try
            {

                _CurrentCollectionIndex -= 1;

                if (_CurrentCollectionIndex >= 1)
                {
                    SetCurrentCollection(_CurrentCollectionIndex);
                }
                else
                {
                    _CurrentCollectionIndex = 1;
                    SetCurrentCollection(_CurrentCollectionIndex);
                    lblcounterWarning.Text = Application.Current.FindResource("MessageID431") as string;
                }

            }
            catch (Exception Ex)
            {

                ExceptionManager.Publish(Ex);
            }
        }

        private void btnMoveNext_Click(object sender, RoutedEventArgs e)
        {
            if (bStartClicked)
            {
                //objNoteCumTktScanLib.CloseSerialCom();
                MessageBox.ShowBox("MessageID355", BMC_Icon.Error);
                return;
            }

            if (bIsUncommitted)
            {
                RevertExceptionTickets();
                bIsUncommitted = false;
            }
            _CurrentCollectionIndex += 1;
            try
            {

                if (lstCollections.Count > _CurrentCollectionIndex)
                {
                    if (bIsUncommitted)
                        RevertExceptionTickets();

                    SetCurrentCollection(_CurrentCollectionIndex);
                }
                else
                {
                    _CurrentCollectionIndex = lstCollections.Count - 1;
                    SetCurrentCollection(_CurrentCollectionIndex);

                    lblcounterWarning.Text = Application.Current.FindResource("MessageID432") as string;
                }

            }
            catch (Exception Ex)
            {

                ExceptionManager.Publish(Ex);
            }


        }
        private void btnFirst_Click(object sender, RoutedEventArgs e)
        {
            if (bStartClicked)
            {
                //objNoteCumTktScanLib.CloseSerialCom();
                MessageBox.ShowBox("MessageID355", BMC_Icon.Error);
                return;
            }

            if (bIsUncommitted)
            {
                RevertExceptionTickets();
                bIsUncommitted = false;
            }
            try
            {
                _CurrentCollectionIndex = 1;
                SetCurrentCollection(_CurrentCollectionIndex);
                lblcounterWarning.Text = Application.Current.FindResource("MessageID431") as string;
            }
            catch (Exception Ex)
            {

                ExceptionManager.Publish(Ex);
            }


        }

        private void btnLast_Click(object sender, RoutedEventArgs e)
        {
            if (bStartClicked)
            {
                //objNoteCumTktScanLib.CloseSerialCom();
                MessageBox.ShowBox("MessageID355", BMC_Icon.Error);
                return;
            }
            if (bIsUncommitted)
            {
                RevertExceptionTickets();
                bIsUncommitted = false;
            }

            try
            {
                _CurrentCollectionIndex = lstCollections.Count - 1;
                SetCurrentCollection(_CurrentCollectionIndex);
                lblcounterWarning.Text = Application.Current.FindResource("MessageID432") as string;
            }
            catch (Exception Ex)
            {

                ExceptionManager.Publish(Ex);
            }
        }


        void ClearAll()
        {
            try
            {

                LogManager.WriteLog("BillsTicketCounter->ClearAll()", LogManager.enumLogLevel.Debug);

                if (bIsUncommitted)
                {
                    RevertExceptionTickets();
                    bIsUncommitted = false;
                }
                if (oBillDenoms != null)
                    oBillDenoms = null;
                lblcounterWarning.Text = string.Empty;
                objBills.Clear();
                lvBills.ItemsSource = null;
                txtTotalBills.Text = string.Empty;

                if (lsValidTicketsHolder != null)
                    lsValidTicketsHolder.Clear();
                if (BillsDict != null)
                    BillsDict.Clear();

                lvTickets.ItemsSource = null;
                lvInValidTickets.ItemsSource = null;
                sTickets = string.Empty;
                txtTotal.Text = string.Empty;
                txtQuantity.Text = string.Empty;
                txtQuantityIn.Text = string.Empty;
            }
            catch (Exception Ex)
            {

                ExceptionManager.Publish(Ex);
            }
        }
        private void btnClearAll_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (bStartClicked)
                {
                    //objNoteCumTktScanLib.CloseSerialCom();
                    MessageBox.ShowBox("MessageID355", BMC_Icon.Error);
                    return;
                }
                ClearAll();
            }

            catch (Exception Ex)
            {

                ExceptionManager.Publish(Ex);
            }
        }

        private void btnClearBills_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (bStartClicked)
                {
                    //objNoteCumTktScanLib.CloseSerialCom();
                    MessageBox.ShowBox("MessageID355", BMC_Icon.Error);
                    return;
                }
                objBills.Clear();
                BillsDict.Clear();
                if (oBillDenoms != null)
                    oBillDenoms = null;
                lvBills.ItemsSource = null;
                txtTotalBills.Text = string.Empty;
            }
            catch (Exception Ex)
            {

                ExceptionManager.Publish(Ex);
            }
        }

        private void btnClearVouchers_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (bStartClicked)
                {
                    //objNoteCumTktScanLib.CloseSerialCom();
                    MessageBox.ShowBox("MessageID355", BMC_Icon.Error);
                    return;
                }
                if (bIsUncommitted)
                {
                    RevertExceptionTickets();
                    bIsUncommitted = false;
                }

                if (lsValidTicketsHolder != null)
                    lsValidTicketsHolder.Clear();
                lvTickets.ItemsSource = null;
                lvInValidTickets.ItemsSource = null;
                sTickets = string.Empty;
                txtTotal.Text = string.Empty;
                txtQuantity.Text = string.Empty;
                txtQuantityIn.Text = string.Empty;

            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
        }

        void RevertExceptionTickets()
        {
            try
            {
                LogManager.WriteLog("BillsTicketCounter->Skipping save for CollectionNo" + lstCollections[_CurrentCollectionIndex].CollectionNo.ToString(), LogManager.enumLogLevel.Debug);
                if (Settings.HANDLE_EXCEPTIONTICKETS_COUNTER)
                {
                    _objCollectionHelper.DiscardExceptionVoucherChanges(lstCollections[_CurrentCollectionIndex].CollectionNo, lstCollections[_CurrentCollectionIndex].InstallationNo, true);
                }
            }
            catch (Exception Ex)
            {

                ExceptionManager.Publish(Ex);
            }


        }
        void SaveAll()
        {

            LogManager.WriteLog("BillsTicketCounter->SaveAll()" + lstCollections[_CurrentCollectionIndex].CollectionNo.ToString(), LogManager.enumLogLevel.Debug);

            if (Settings.HANDLE_EXCEPTIONTICKETS_COUNTER)
            {
                _objCollectionHelper.DiscardExceptionVoucherChanges(lstCollections[_CurrentCollectionIndex].CollectionNo, lstCollections[_CurrentCollectionIndex].InstallationNo, false);
            }

            if (!this.txtTotal.Text.IsNullOrEmpty())
            {

                foreach (var listitem in this.lsValidTicketsHolder)
                {
                    int? i = 0;
                    _objCollectionHelper.InsertDeclaredTicket(listitem.strBarcode, Convert.ToDecimal(listitem.iAmount) / 100,
                                                                SecurityHelper.CurrentUser.SecurityUserID,
                                                                 0, 0, lstCollections[_CurrentCollectionIndex].InstallationNo, iCollectionNo, ref i);
                    _objCollectionHelper.UpdateVoucherCollection(listitem.strBarcode, "1");
                }
            }

            if (oBillDenoms == null)
            {
                oBillDenoms = new BillDenoms();
                oBillDenoms.FIVE_HUNDREDS = lstCollections[_CurrentCollectionIndex].P50000.ToString();
                oBillDenoms.TWO_HUNDREDS = lstCollections[_CurrentCollectionIndex].P20000.ToString();
                oBillDenoms.HUNDREDS = lstCollections[_CurrentCollectionIndex].P10000.ToString();
                oBillDenoms.FIFTIES = lstCollections[_CurrentCollectionIndex].P5000.ToString();
                oBillDenoms.TWENTIES = lstCollections[_CurrentCollectionIndex].P2000.ToString();
                oBillDenoms.TENS = lstCollections[_CurrentCollectionIndex].P1000.ToString();
                oBillDenoms.FIVES = lstCollections[_CurrentCollectionIndex].P500.ToString();
                oBillDenoms.TWOS = lstCollections[_CurrentCollectionIndex].P200.ToString();
                oBillDenoms.ONES = lstCollections[_CurrentCollectionIndex].P100.ToString();
            }
            _objCollectionHelper.AddCollectionToFullCollection(oBillDenoms.FIVE_HUNDREDS.EmptytoZero().GetCurrencyValueAsString(),
                oBillDenoms.TWO_HUNDREDS.EmptytoZero().GetCurrencyValueAsString(), oBillDenoms.HUNDREDS.EmptytoZero().GetCurrencyValueAsString(), oBillDenoms.FIFTIES.EmptytoZero().GetCurrencyValueAsString(),
                oBillDenoms.TWENTIES.EmptytoZero().GetCurrencyValueAsString(), oBillDenoms.TENS.EmptytoZero().GetCurrencyValueAsString(), oBillDenoms.FIVES.EmptytoZero().GetCurrencyValueAsString(),
               oBillDenoms.TWOS.EmptytoZero().GetCurrencyValueAsString(), oBillDenoms.ONES.EmptytoZero().GetCurrencyValueAsString(), lstCollections[_CurrentCollectionIndex].TotalCoins.GetCurrencyValueAsString(),
               txtTotal.Text.GetCurrencyValueAsString(),
               lstCollections[_CurrentCollectionIndex].Installation_Token_Value,
               lstCollections[_CurrentCollectionIndex].CollectionNo);
            bIsUncommitted = false;
            LogManager.WriteLog("BillsTicketCounter->SaveAll() Complete" + lstCollections[_CurrentCollectionIndex].CollectionNo.ToString(), LogManager.enumLogLevel.Debug);
            RefreshData(_CurrentCollectionIndex);


        }

        void RefreshData(int CurrentCollectionIndex)
        {
            LogManager.WriteLog("BillsTicketCouonter->RefreshData().BatchNo:" + _batchNo.ToString(), LogManager.enumLogLevel.Debug);

            lstCollections = _objCollectionHelper.GetUndeclaredCollectionByBatchNo(_batchNo, _filterBy, _filterValue);
            SetCurrentCollection(CurrentCollectionIndex);
        }
        private void SetCurrentCollection(int CurrentIndex)
        {
         
   
            ClearAll();
            if (lstCollections != null)
            {
                txt_CurrentCollection.Text = string.Format("{0}/{1}", CurrentIndex, lstCollections.Count - 1);
                this.txtAsset.Text = lstCollections[CurrentIndex].AssetNo;
                this.txtPosition.Text = lstCollections[CurrentIndex].Position;
                this.iCollectionNo = lstCollections[CurrentIndex].CollectionNo;
                this.sPosition = lstCollections[CurrentIndex].Position;
                lblcounterWarning.Text = String.Empty;

                txtGame.Text = _objExchangeHelper.GetGameName(txtAsset.Text);
                btnExceptionTickets.Visibility = Visibility.Collapsed;
            }
        }

        private void btnCashEntry_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (bStartClicked)
                {
                    //objNoteCumTktScanLib.CloseSerialCom();
                    MessageBox.ShowBox("MessageID355", BMC_Icon.Error);
                    return;
                }

                int BatchNo = _batchNo;
                //Set Site Code for Title display

                ManualCashEntry.sSiteCode = _SiteCode;

                btnCashEntry.IsEnabled = false;
                var manualCashEntry = Security.SecurityHelper.HasAccess("BMC.Presentation.CommonCDOforDeclaration") ? new ManualCashEntry(CommonUtilities.SiteConnectionString(ExchangeConnectionString), CommonUtilities.TicketingConnectionString(TicketingConnectionString)) : new ManualCashEntry();



                manualCashEntry.Owner = Window.GetWindow(this);
                manualCashEntry.txt500.Text = lstCollections[_CurrentCollectionIndex].P50000.ToString();

                manualCashEntry.txt200.Text = lstCollections[_CurrentCollectionIndex].P20000.ToString();

                manualCashEntry.txt100.Text = lstCollections[_CurrentCollectionIndex].P10000.ToString();

                manualCashEntry.txt50.Text = lstCollections[_CurrentCollectionIndex].P5000.ToString();

                manualCashEntry.txt20.Text = lstCollections[_CurrentCollectionIndex].P2000.ToString();

                manualCashEntry.txt10.Text = lstCollections[_CurrentCollectionIndex].P1000.ToString();

                manualCashEntry.txt5.Text = lstCollections[_CurrentCollectionIndex].P500.ToString();

                manualCashEntry.txt2.Text = lstCollections[_CurrentCollectionIndex].P200.ToString();

                manualCashEntry.txt1.Text = lstCollections[_CurrentCollectionIndex].P100.ToString();

                manualCashEntry.txtTotalCoins.Text = lstCollections[_CurrentCollectionIndex].TotalCoinsValue.GetUniversalCurrencyFormat();


                manualCashEntry.txtTicketsIn.Text = lstCollections[_CurrentCollectionIndex].TicketsInValue.GetUniversalCurrencyFormat();


                //var manualCashEntry = new ManualCashEntry()
                //{

                //    Owner = Window.GetWindow(this),
                //    txt500 =
                //    {
                //        Text = lstCollections[_CurrentCollectionIndex].P50000.ToString()
                //    },
                //    txt200 =
                //    {
                //        Text = lstCollections[_CurrentCollectionIndex].P20000.ToString()
                //    },
                //    txt100 =
                //    {
                //        Text = lstCollections[_CurrentCollectionIndex].P10000.ToString()
                //    },
                //    txt50 =
                //    {
                //        Text = lstCollections[_CurrentCollectionIndex].P5000.ToString()
                //    },
                //    txt20 =
                //    {
                //        Text = lstCollections[_CurrentCollectionIndex].P2000.ToString()
                //    },
                //    txt10 =
                //    {
                //        Text =
                //            lstCollections[_CurrentCollectionIndex].P1000.
                //            ToString()
                //    },
                //    txt5 =
                //    {
                //        Text =
                //            lstCollections[_CurrentCollectionIndex].P500.
                //            ToString()
                //    },
                //    txt2 =
                //    {
                //        Text =
                //            lstCollections[_CurrentCollectionIndex].P200.
                //            ToString()
                //    },
                //    txt1 =
                //    {
                //        Text =
                //          lstCollections[_CurrentCollectionIndex].P100.
                //            ToString()
                //    },
                //    txtTotalCoins =
                //    {
                //        Text =
                //           lstCollections[_CurrentCollectionIndex].TotalCoinsValue.GetUniversalCurrencyFormat()

                //    },
                //    txtTicketsIn =
                //    {
                //        Text =
                //       lstCollections[_CurrentCollectionIndex].TicketsInValue.GetUniversalCurrencyFormat()

                //    },
                //};

                ManualCashEntry.TokenValue =
                    (decimal)lstCollections[_CurrentCollectionIndex].Installation_Token_Value / 100;

                ManualCashEntry.iInstallationNo = lstCollections[_CurrentCollectionIndex].InstallationNo;
                ManualCashEntry.iCollectionNo = lstCollections[_CurrentCollectionIndex].CollectionNo;
                ManualCashEntry.BatchNo = BatchNo;

                //added to pass info to Bill Counter Screen
                ManualCashEntry.sAsset = lstCollections[_CurrentCollectionIndex].AssetNo;
                ManualCashEntry.sPosition = lstCollections[_CurrentCollectionIndex].Position;

                if (ManualCashEntry.TokenValue == 0)
                    manualCashEntry.txtTotalCoins.IsEnabled = false;

                //if (manualCashEntry.ShowDialog() != true) return;
                bool? result2 = manualCashEntry.ShowDialogEx(this);

                if (result2 != null && result2.HasValue && result2.Value == true)
                {
                    try
                    {
                        if (manualCashEntry.txt500.Text == string.Empty)
                            manualCashEntry.txt500.Text = "0";

                        if (manualCashEntry.txt200.Text == string.Empty)
                            manualCashEntry.txt200.Text = "0";

                        if (manualCashEntry.txt100.Text == string.Empty)
                            manualCashEntry.txt100.Text = "0";

                        if (manualCashEntry.txt50.Text == string.Empty)
                            manualCashEntry.txt50.Text = "0";

                        if (manualCashEntry.txt20.Text == string.Empty)
                            manualCashEntry.txt20.Text = "0";

                        if (manualCashEntry.txt10.Text == string.Empty)
                            manualCashEntry.txt10.Text = "0";

                        if (manualCashEntry.txt5.Text == string.Empty)
                            manualCashEntry.txt5.Text = "0";

                        if (manualCashEntry.txt2.Text == string.Empty)
                            manualCashEntry.txt2.Text = "0";

                        if (manualCashEntry.txt1.Text == string.Empty)
                            manualCashEntry.txt1.Text = "0";

                        if (lstCollections[_CurrentCollectionIndex].CollectionBatchNo == 0)
                            AddToPartCollection(manualCashEntry);
                        else
                            AddToFullCollection(manualCashEntry);
                    }
                    catch (Exception ex)
                    {
                        ExceptionManager.Publish(ex);
                        MessageBox.ShowBox(ex.Message, true);
                    }
                    finally
                    {
                        btnCashEntry.IsEnabled = true;
                    }
                }

                RefreshData(_CurrentCollectionIndex);

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            finally
            {
                btnCashEntry.IsEnabled = true;
                //btnAcceptAll.IsEnabled = true;
            }
        }

        private void AddToPartCollection(ManualCashEntry manualCashEntry)
        {
            _objCollectionHelper.AddCollectionToPartCollection(manualCashEntry.txt500.GetCurrencyValueAsString(),
                manualCashEntry.txt200.GetCurrencyValueAsString(), manualCashEntry.txt100.GetCurrencyValueAsString(), manualCashEntry.txt50.GetCurrencyValueAsString(),
                manualCashEntry.txt20.GetCurrencyValueAsString(), manualCashEntry.txt10.GetCurrencyValueAsString(), manualCashEntry.txt5.GetCurrencyValueAsString(),
                manualCashEntry.txt2.GetCurrencyValueAsString(), manualCashEntry.txt1.GetCurrencyValueAsString(), manualCashEntry.txtTotalCoins.GetCurrencyValueAsString(),
                manualCashEntry.txtTicketsIn.GetCurrencyValueAsString(),
               lstCollections[_CurrentCollectionIndex].Installation_Token_Value,
                lstCollections[_CurrentCollectionIndex].CollectionNo);
        }

        private void AddToFullCollection(ManualCashEntry manualCashEntry)
        {
            _objCollectionHelper.AddCollectionToFullCollection(manualCashEntry.txt500.GetCurrencyValueAsString(),
                manualCashEntry.txt200.GetCurrencyValueAsString(), manualCashEntry.txt100.GetCurrencyValueAsString(), manualCashEntry.txt50.GetCurrencyValueAsString(),
                manualCashEntry.txt20.GetCurrencyValueAsString(), manualCashEntry.txt10.GetCurrencyValueAsString(), manualCashEntry.txt5.GetCurrencyValueAsString(),
                manualCashEntry.txt2.GetCurrencyValueAsString(), manualCashEntry.txt1.GetCurrencyValueAsString(), manualCashEntry.txtTotalCoins.GetCurrencyValueAsString(),
                manualCashEntry.txtTicketsIn.GetCurrencyValueAsString(),
                lstCollections[_CurrentCollectionIndex].Installation_Token_Value,
                lstCollections[_CurrentCollectionIndex].CollectionNo);
        }

        private bool _isCounterStarted = false;

        public bool IsCounterStarted
        {
            get { return _isCounterStarted; }
            set
            {
                _isCounterStarted = value;
                btnApply.IsEnabled = !value;
                btnCancel.IsEnabled = !value;
                btnCashEntry.IsEnabled = !value;
                btnClearAll.IsEnabled = !value;
                btnClearBills.IsEnabled = !value;
                btnClearVouchers.IsEnabled = !value;
                btnExit.IsEnabled = !value;
                btnMoveNext.IsEnabled = !value;
                btnPrevious.IsEnabled = !value;
                btnLast.IsEnabled = !value;
                btnFirst.IsEnabled = !value;


            }
        }



    }


    public partial class Bills
    {
        public Bills()
        {
        }
        public string Bill { get; set; }
        public string Count { get; set; }
        public string Value { get; set; }
    }

    public class BillDenoms
    {

        public string ONES;
        public string TWOS;
        public string FIVES;
        public string TENS;
        public string TWENTIES;
        public string FIFTIES;
        public string HUNDREDS;
        public string TWO_HUNDREDS;
        public string FIVE_HUNDREDS;
    }


    public static class FormatString
    {
        public static string EmptytoZero(this string Value)
        {
            try
            {
                if (Value == null || Value == string.Empty)
                {
                    return "0";
                }
                else
                {
                    return Value;
                }
            }
            catch (Exception Ex)
            {

                ExceptionManager.Publish(Ex);
                return "0";
            }

        }
    }
}
