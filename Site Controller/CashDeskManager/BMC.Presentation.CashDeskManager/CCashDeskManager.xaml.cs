using System;
using System.Windows;
using BMC.Common.ExceptionManagement;
using BMC.Common.LogManagement;
using BMC.Transport;
using BMC.Presentation.CashDeskManager;
using Microsoft.Office.Interop.Excel;
using System.Reflection;
using BMC.Business.CashDeskManager;
using System.Collections.Generic;
using System.Windows.Controls;
using System.ComponentModel;
using System.Threading;
using System.Windows.Threading;
using System.Diagnostics;
using System.IO;
using BMC.Presentation.CashDeskManager.UserControls;



namespace BMC.Presentation
{
    public partial class CCashDeskManager
    {
        #region "Declarations"
        TreasuryTransactions busTreasury;
        string RouteNumber = string.Empty;
        string dtFrom = string.Empty;
        string dtTo = string.Empty;
        string TimeFrom = string.Empty;
        string TimeTo = string.Empty;

        string TREASURY_REFILL = "Refill";
        string TREASURY_REFUND = "Refund";
        //string TREASURY_PRIZE_CARD_REFILL = "Prize Card Refill";
        //string TREASURY_HANDPAY_JACKPOT = "Handpay Jackpot";
        string TREASURY_HANDPAY_CREDIT = "Handpay Credit";
        string TREASURY_SHORTPAY = "Shortpay";
        string TREASURY_CASH_DESK_FLOAT = "Cash Desk Float";
        //string TREASURY_FLOAT = "Float";
        string TREASURY_PROGRESSIVE = "Prog";
        string TREASURY_JACKPOT = "Handpay Jackpot";
        string MYFORMAT = "#,##0.00";
        BackgroundWorker _worker = null;
        List<TicketExceptions> lstTicketExcep = null;
        string StartDate = string.Empty;
        string EndDate = string.Empty;
        string StartTime = string.Empty;
        string EndTime = string.Empty;


        #endregion
        public CCashDeskManager()
        {
            InitializeComponent();
            _worker = new BackgroundWorker();
            _worker.WorkerReportsProgress = true;
            _worker.WorkerSupportsCancellation = true;
        }

        private void btnExcepDetails_Click(object sender, RoutedEventArgs e)
        {
            popupgrid.IsOpen = true;
            if (string.IsNullOrEmpty(StartDate) || string.IsNullOrEmpty(EndDate))
            {
                BMC.Presentation.CashDeskManager.MessageBox.showBox("Not a valid Date Range");
            }
            CExceptions objExceptions = new CExceptions("0", StartDate, EndDate,
             StartTime, EndTime);
            exceptiongrid.Children.Add(objExceptions);
            objExceptions.Margin = new Thickness(0);
        }

        private void dtpStartDate_SelectedDateChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            StartDate = Convert.ToDateTime(dtpStartDate.Text).ToString("dd MMM yyyy");
        }

        private void btnDebug_Click(object sender, RoutedEventArgs e)
        {
           
            popupgrid.IsOpen = true;
            if (string.IsNullOrEmpty(StartDate) || string.IsNullOrEmpty(EndDate))
            {
                 BMC.Presentation.CashDeskManager.MessageBox.showBox("Not a valid Date Range");
            }
            CDebug objDebug = new CDebug("0", StartDate, EndDate, StartTime, EndTime);

            exceptiongrid.Children.Add(objDebug);
            objDebug.Margin = new Thickness(0);

        }

        private void btnLiabilityDetails_Click(object sender, RoutedEventArgs e)
        {
         
            popupgrid.IsOpen = true;
            if (string.IsNullOrEmpty(StartDate) || string.IsNullOrEmpty(EndDate))
            {
                BMC.Presentation.CashDeskManager.MessageBox.showBox("Not a valid Date Range");
            }

            CLiability objLiability = new CLiability("0", StartDate, EndDate,
            StartTime, EndTime);


            exceptiongrid.Children.Add(objLiability);
            objLiability.Margin = new Thickness(0);
        }

        private void btnVoidDetails_Click(object sender, RoutedEventArgs e)
        {
            popupgrid.IsOpen = true;
            if (string.IsNullOrEmpty(StartDate) || string.IsNullOrEmpty(EndDate))
            {

                BMC.Presentation.CashDeskManager.MessageBox.showBox("Not a valid Date Range");
            }

            CVoidAutoCancelledTickets objVoidCancelled = new CVoidAutoCancelledTickets("0", StartDate, EndDate,
            StartTime, EndTime);


            exceptiongrid.Children.Add(objVoidCancelled);
            objVoidCancelled.Margin = new Thickness(0);
        }

        private void btnActiveDetails_Click(object sender, RoutedEventArgs e)
        {
            popupgrid.IsOpen = true;
            if (string.IsNullOrEmpty(StartDate) || string.IsNullOrEmpty(EndDate))
            {
                BMC.Presentation.CashDeskManager.MessageBox.showBox("Not a valid Date Range");
            }

            CActiveTicketsxaml objActiveTickets = new CActiveTicketsxaml("0", StartDate, EndDate,
            StartTime, EndTime);


            exceptiongrid.Children.Add(objActiveTickets);
            //objActiveTickets.Margin = new Thickness(exceptiongrid.Width/8, 50, 0, 0);
            objActiveTickets.Margin = new Thickness(0);
        }

        private void btnPromoCashableDetails_Click(object sender, RoutedEventArgs e)
        {
            popupgrid.IsOpen = true;

            if (string.IsNullOrEmpty(StartDate) || string.IsNullOrEmpty(EndDate))
            {
                BMC.Presentation.CashDeskManager.MessageBox.showBox("Not a valid Date Range");
            }
            CPromoCashableDetails objPromo = new CPromoCashableDetails("0", StartDate, EndDate,
            StartTime, EndTime);


            exceptiongrid.Children.Add(objPromo);
            objPromo.Margin = new Thickness(0);
        }

        private void btnAnomalies_Click(object sender, RoutedEventArgs e)
        {
            popupgrid.IsOpen = true;
            if (string.IsNullOrEmpty(StartDate) || string.IsNullOrEmpty(EndDate))
            {
                BMC.Presentation.CashDeskManager.MessageBox.showBox("Not a valid Date Range");
            }
            CTicketAnomalies objTicketAnomalies = new CTicketAnomalies("0", StartDate, EndDate,
            StartTime, EndTime);

            exceptiongrid.Children.Add(objTicketAnomalies);
            objTicketAnomalies.Margin = new Thickness(0);
        }

        private void btnExpiredDetails_Click(object sender, RoutedEventArgs e)
        {
            popupgrid.IsOpen = true;
            if (string.IsNullOrEmpty(StartDate) || string.IsNullOrEmpty(EndDate))
            {
                BMC.Presentation.CashDeskManager.MessageBox.showBox("Not a valid Date Range");
            }

         
            CVoidExpired objVoidExpired = new CVoidExpired("0", StartDate, EndDate,
            StartTime, EndTime);

            exceptiongrid.Children.Add(objVoidExpired);
            objVoidExpired.Margin = new Thickness(0);
        }

        private void btnProcess_Click(object sender, RoutedEventArgs e)
        {
            if (!_worker.IsBusy)
            {
                loadWorker();
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(dtpStartDate.Text) && !string.IsNullOrEmpty(dtpEndDate.Text))
            {
                StartDate = Convert.ToDateTime(dtpStartDate.Text).ToString("dd MMM yyyy");
                EndDate = Convert.ToDateTime(dtpEndDate.Text).ToString("dd MMM yyyy");
            }
         

             StartTime = tmpStartTime.SelectedHour.ToString() + ":" + tmpStartTime.SelectedMinute.ToString() + ":" + tmpStartTime.SelectedSecond.ToString();
             EndTime = dtpEndtime.SelectedHour.ToString() + ":" + dtpEndtime.SelectedMinute.ToString() + ":" + dtpEndtime.SelectedSecond.ToString();

        }


        private List<TicketExceptions> LoadCashDeskDetails()
        {
            string szErrDesc = string.Empty;
            double cTicketClaimedTotalCashdesk = 0, cTicketPrintedTotalCashdesk = 0, cTicketPrintedTotalEGM = 0,
                cTicketClaimedTotalEGM = 0, cTicketsUnclaimed = 0, cTicketInExceptions = 0, cTicketOutExceptions = 0;
            double currEGM = 0, currCashdesk = 0, cTicketsVoid = 0, cTicketsExpired = 0, cTicketCancelled = 0;

            double cRefundTotal = 0, cRefillTotal = 0, cShortpayTotal = 0, cHandPayTotal = 0, cCashDeskFloatTotal = 0,
                cProgressiveTotal = 0, cJackpotTotal = 0;

            double RefillQty = 0, RefundQty = 0, ShortpayQty = 0, HandpayQty = 0, CashDeskFloatQty = 0,
                ProgressiveQty = 0, JackpotQty = 0, CashDeskClaimedQty = 0, MachineClaimedQty = 0, CashDeskPrintedQty = 0, MachinePrintedQty = 0;


            busTreasury = new TreasuryTransactions();
            RouteNumber = "0";


            List<string> lstPositionstoDisplay = busTreasury.FillListOfFilteredPositions(RouteNumber);

            if (string.IsNullOrEmpty(StartDate) || string.IsNullOrEmpty(EndDate))
            {
                
                System.Windows.Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate
                  {
                      BMC.Presentation.CashDeskManager.MessageBox.showBox("Not a valid Date Range");
                      //prgViewAll.Value = 0;
                  });
                return null;
            }

            if (!Business.CashDeskManager.Common.isValidDateRange((StartDate + " " + StartTime), (EndDate + " " + EndTime)))
            {
                System.Windows.Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate
                {
                    BMC.Presentation.CashDeskManager.MessageBox.showBox("Not a valid Date Range");
                   // prgViewAll.Value = 0;
                });
                return null;
            }

            TicketsClaimed oTicketsClaimed = new TicketsClaimed();
            oTicketsClaimed.TicketsClaimedTo = EndDate + " " + EndTime;
            oTicketsClaimed.TicketsClaimedFrom = StartDate + " " + StartTime;

            szErrDesc = "pre tickets claimed";
            List<TicketExceptions> lstTicketsClaimed = busTreasury.TicketsClaimed(oTicketsClaimed, lstPositionstoDisplay);
            if (lstTicketsClaimed != null)
            {
                foreach (TicketExceptions exep in lstTicketsClaimed)
                {
                    cTicketClaimedTotalCashdesk += (float)exep.Value;
                }
            }
            else
            {
                lstTicketsClaimed = new List<TicketExceptions>();
            }


            szErrDesc = "pre tickets printed";
            List<TicketExceptions> lstTicketsPrinted = busTreasury.TicketsPrinted(oTicketsClaimed, lstPositionstoDisplay);
            if (lstTicketsPrinted != null)
            {
                foreach (TicketExceptions exep in lstTicketsPrinted)
                {
                    cTicketPrintedTotalEGM += (float)exep.Value;
                }
                foreach (TicketExceptions item in lstTicketsPrinted)
                {
                    lstTicketsClaimed.Add(item);
                }
            }
            

            szErrDesc = "pre TiTo claimed";

            Tickets oTickets = new Tickets();
            oTickets.EndDate = EndDate + " " + EndTime;
            oTickets.StartDate = StartDate + " " + StartTime;
            oTickets.IsLiability = false;
            oTickets.BarCode = "%";
            oTickets.Type = "C";
            System.Windows.Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate
               {
                   oTickets.IsClaimedInCashDesk = (bool)chkCashDeskTicketIn.IsChecked;
                   oTickets.IsClaimedInMachine = (bool)chkTicketIn.IsChecked;
                   prgViewAll.Value += 20;

               }
              
           );
           
           
                List<TicketExceptions> lstTitoClaimed = busTreasury.TitoTicketsClaimed(oTickets, lstPositionstoDisplay);

                if (lstTitoClaimed != null)
                {
                    foreach (TicketExceptions exep in lstTitoClaimed)
                    {
                        currEGM += exep.currEGM;
                        currCashdesk += exep.CurrentCashDesk;
                        CashDeskClaimedQty += exep.CashDeskClaimedQty;
                        MachineClaimedQty += exep.MachineClaimedQty;
                    }
                    System.Windows.Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate
                      {
                          if ((bool)chkCashDeskTicketIn.IsChecked && (bool)chkTicketIn.IsChecked)
                          {
                              foreach (TicketExceptions item in lstTitoClaimed)
                              {
                                  lstTicketsClaimed.Add(item);
                              }
                          }
                          prgViewAll.Value += 20;
                      });
                    cTicketClaimedTotalCashdesk = cTicketClaimedTotalCashdesk + currCashdesk;
                    cTicketClaimedTotalEGM = cTicketClaimedTotalEGM + currEGM;
                }
           

            oTickets.Type = "U";
            List<TicketExceptions> lstTicketsUnclaimed = busTreasury.TitoTicketsUnclaimed(oTickets, lstPositionstoDisplay);
            if (lstTicketsUnclaimed != null)
            {
                foreach (TicketExceptions exep in lstTicketsUnclaimed)
                {
                    cTicketsUnclaimed += (float)exep.Value;
                    lstTicketsClaimed.Add(exep);
                }
            }

            oTickets.Type = "E";
            List<TicketExceptions> lstTicketInExceptions = busTreasury.TITOTicketInExceptions(oTickets, lstPositionstoDisplay);
            if (lstTicketInExceptions != null)
            {
                foreach (TicketExceptions exep in lstTicketInExceptions)
                {
                    cTicketInExceptions += (double)exep.currValue;
                    lstTicketsClaimed.Add(exep);
                }
            }

            oTickets.Type = "E";
            List<TicketExceptions> lstTicketOutExceptions = busTreasury.TitoTicketOutExceptions(oTickets, lstPositionstoDisplay);
            if (lstTicketOutExceptions != null)
            {
                foreach (TicketExceptions exep in lstTicketOutExceptions)
                {
                    cTicketOutExceptions += (float)exep.cTicketTotal;
                   // lstTicketsClaimed.Add(exep);
                }
            }

            //cTicketOutExceptions = lstTicketOutExceptions[0].cTicketTotal;

            oTickets.Type = "V";
            List<TicketExceptions> lstTicketsVoid = busTreasury.GetTicket_VoidnExpired(oTickets, lstPositionstoDisplay);
            if (lstTicketsVoid != null)
            {
                foreach (TicketExceptions exep in lstTicketsVoid)
                {
                    lstTicketsClaimed.Add(exep);
                    cTicketsVoid += (float)exep.cTicketTotal;
                }
            }
            //cTicketsVoid = lstTicketsVoid[0].cTicketTotal;

            oTickets.Type = "D";
            List<TicketExceptions> lstTicketsExpired = busTreasury.GetTicket_VoidnExpired(oTickets, lstPositionstoDisplay);
            if (lstTicketsExpired != null)
            {
                foreach (TicketExceptions exep in lstTicketsExpired)
                {
                    lstTicketsClaimed.Add(exep);
                    cTicketsExpired += (float)exep.currValue;
                }
            }


            oTickets.Type = "B";
            List<TicketExceptions> lstTicketsCancelled = busTreasury.GetTicket_VoidnExpired(oTickets, lstPositionstoDisplay);
            if (lstTicketsCancelled != null)
            {
                foreach (TicketExceptions exep in lstTicketsCancelled)
                {
                    lstTicketsClaimed.Add(exep);
                    cTicketCancelled += (float)exep.cTicketTotal;
                }
            }



            oTickets.Type = "P";
            System.Windows.Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate
               {

                   oTickets.IsPrintedInCashDesk = (bool)chkCashDeskTicketOut.IsChecked;
                   oTickets.IsPrintedInMachine = (bool)chkTicketOut.IsChecked;
                   prgViewAll.Value += 20;
               });
            
                List<TicketExceptions> lstTitoTicketsPrinted = busTreasury.TitoTicketsPrinted(oTickets, lstPositionstoDisplay);
                if (lstTitoTicketsPrinted != null)
                {
                    foreach (TicketExceptions exep in lstTitoTicketsPrinted)
                    {
                        System.Windows.Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate
                          {
                              if ((bool)chkCashDeskTicketOut.IsChecked && (bool)chkTicketOut.IsChecked)
                              {
                                  lstTicketsClaimed.Add(exep);
                              }
                          });

                       
                        CashDeskPrintedQty += exep.CashDeskPrintedQty;
                        MachinePrintedQty += exep.MachinePrintedQty;
                    }
                    if (lstTitoTicketsPrinted != null && lstTitoTicketsPrinted.Count > 0)
                    {
                        currEGM = lstTitoTicketsPrinted[0].currEGM;
                        currCashdesk = lstTitoTicketsPrinted[0].CurrentCashDesk;
                        cTicketPrintedTotalCashdesk += currCashdesk;
                        cTicketPrintedTotalEGM += currEGM;
                    }
                }
            

            szErrDesc = "pre refunds";
            oTickets.Type = TREASURY_REFUND;
            System.Windows.Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate
              {
                  oTickets.IsLiability = (bool)chkRefunds.IsChecked;
                  prgViewAll.Value += 20;
              });
            List<TicketExceptions> RefundItems = busTreasury.GetTreasuryItems(oTickets, lstPositionstoDisplay);
            if (RefundItems != null)
            {
                foreach (TicketExceptions item in RefundItems)
                {
                    lstTicketsClaimed.Add(item);
                    cRefundTotal += item.TreasuryAmount;
                    RefundQty += item.HandpayQty;
                }
            }

            szErrDesc = "pre refill";
            oTickets.Type = TREASURY_REFILL;
            System.Windows.Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate
              {
                  oTickets.IsLiability = (bool)chkRefills.IsChecked;
              });
            List<TicketExceptions> RefillItems = busTreasury.GetTreasuryItems(oTickets, lstPositionstoDisplay);
            if (RefillItems != null)
            {
                foreach (TicketExceptions item in RefillItems)
                {
                    lstTicketsClaimed.Add(item);
                    cRefillTotal += item.TreasuryAmount;
                    RefillQty += item.HandpayQty;
                }
            }

            szErrDesc = "pre shortpay";
            oTickets.Type = TREASURY_SHORTPAY;
            System.Windows.Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate
              {
                  oTickets.IsLiability = (bool)chkShortPays.IsChecked;
              });
            List<TicketExceptions> ShortpayItems = busTreasury.GetTreasuryItems(oTickets, lstPositionstoDisplay);
            if (ShortpayItems != null)
            {
                foreach (TicketExceptions item in ShortpayItems)
                {
                    lstTicketsClaimed.Add(item);
                    cShortpayTotal += item.TreasuryAmount;
                    ShortpayQty += item.HandpayQty;
                }
            }

            szErrDesc = "Handpays";
            oTickets.Type = TREASURY_HANDPAY_CREDIT;
            System.Windows.Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate
              {
                  oTickets.IsLiability = (bool)chkHandpays.IsChecked;
                  prgViewAll.Value += 20;
              });
            List<TicketExceptions> Handpays = busTreasury.GetTreasuryItems(oTickets, lstPositionstoDisplay);
            if (Handpays != null)
            {
                foreach (TicketExceptions item in Handpays)
                {
                    lstTicketsClaimed.Add(item);
                    cHandPayTotal += Convert.ToDouble(item.Amount);
                    HandpayQty += item.HandpayQty;
                }
            }

            szErrDesc = "pre cash desk float";
            oTickets.Type = TREASURY_CASH_DESK_FLOAT;
            System.Windows.Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate
              {
                  oTickets.IsLiability = (bool)chkCashDeskFloat.IsChecked;
              });
            List<TicketExceptions> CashDeskFloatItems = busTreasury.GetTreasuryItems(oTickets, lstPositionstoDisplay);
            if (CashDeskFloatItems != null)
            {
                foreach (TicketExceptions item in CashDeskFloatItems)
                {
                    lstTicketsClaimed.Add(item);
                    cCashDeskFloatTotal += item.TreasuryAmount;
                    CashDeskFloatQty += item.HandpayQty;
                }
            }

            szErrDesc = "Progressive";
            oTickets.Type = TREASURY_PROGRESSIVE;
            System.Windows.Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate
              {
                  oTickets.IsLiability = (bool)chkProghandpays.IsChecked;
              });
            List<TicketExceptions> ProgItems = busTreasury.GetTreasuryItems(oTickets, lstPositionstoDisplay);
            if (ProgItems != null)
            {
                foreach (TicketExceptions item in ProgItems)
                {
                    lstTicketsClaimed.Add(item);
                    cProgressiveTotal += item.TreasuryAmount;
                    ProgressiveQty += item.HandpayQty;
                }
            }

            szErrDesc = "Jackpot";
            oTickets.Type = TREASURY_JACKPOT;
            System.Windows.Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate
              {
                  oTickets.IsLiability = (bool)chkjackpot.IsChecked;
              });
            List<TicketExceptions> JackPotItems = busTreasury.GetTreasuryItems(oTickets, lstPositionstoDisplay);
            if (JackPotItems != null)
            {
                foreach (TicketExceptions item in JackPotItems)
                {
                    lstTicketsClaimed.Add(item);
                    cJackpotTotal += item.TreasuryAmount;
                    JackpotQty += item.HandpayQty;
                }
            }

                System.Windows.Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate
                  {
                      prgViewAll.Value += 20;
                      txtCashDeskTicketInVal.Text = cTicketClaimedTotalCashdesk.ToString(MYFORMAT);
                      txtMachineTicketInVal.Text = cTicketClaimedTotalEGM.ToString(MYFORMAT);
                      txtMachineTicketOutVal.Text = cTicketPrintedTotalEGM.ToString(MYFORMAT);
                      txtCashDeskTicketOutVal.Text = cTicketPrintedTotalCashdesk.ToString(MYFORMAT);
                      txtLiabilityVal.Text = ((cTicketPrintedTotalEGM + cTicketPrintedTotalCashdesk) -
                          (cTicketClaimedTotalEGM + cTicketClaimedTotalCashdesk)).ToString(MYFORMAT);

                      txtExcepVal.Text = cTicketInExceptions.ToString(MYFORMAT) + "/" + cTicketOutExceptions.ToString(MYFORMAT);
                      txtActiveVal.Text = cTicketsUnclaimed.ToString(MYFORMAT);
                      txtVoidval.Text = cTicketsVoid.ToString(MYFORMAT) + "/" + cTicketCancelled.ToString(MYFORMAT);
                      txtExpiredVal.Text = cTicketsExpired.ToString(MYFORMAT);

                      txtRefundVal.Text = cRefundTotal.ToString(MYFORMAT);
                      txtFillsVal.Text = cRefillTotal.ToString(MYFORMAT);
                      txtCashDeskShortPaysVal.Text = cShortpayTotal.ToString(MYFORMAT);
                      txtHandpayVal.Text = cHandPayTotal.ToString(MYFORMAT);
                      txtCashDeskFloatVal.Text = cCashDeskFloatTotal.ToString(MYFORMAT);
                      txtProgHandpayVal.Text = cProgressiveTotal.ToString(MYFORMAT);
                      txtJackpotVal.Text = cJackpotTotal.ToString(MYFORMAT);
                  });
                  double promoTotal = 0;
                  List<TicketExceptions> lstPromo = busTreasury.GetPromoCashableTickets(oTicketsClaimed, lstPositionstoDisplay);
                  if (lstPromo != null)
                  {
                      foreach (TicketExceptions item in lstPromo)
                      {
                          promoTotal += item.Value;
                      }
                  }
                  System.Windows.Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate
                  {
                      txtPromoCashableVal.Text = promoTotal.ToString(MYFORMAT);

                      txtHandpayQty.Text = HandpayQty.ToString();
                      txtProgHandpayQty.Text = ProgressiveQty.ToString();
                      txtJackPotQty.Text = JackpotQty.ToString();
                      txtRefundQty.Text = RefundQty.ToString();
                      txtFillsQty.Text = RefillQty.ToString();
                      txtCashDeskShortPayQty.Text = ShortpayQty.ToString();
                      txtCashDeskFloatQty.Text = CashDeskFloatQty.ToString();
                      txtCashDeskTicketInQty.Text = CashDeskClaimedQty.ToString();
                      txtMachineTicketInQty.Text = MachineClaimedQty.ToString();
                      txtCashDeskTicketOutQty.Text = CashDeskPrintedQty.ToString();
                      txtMachineTicketOutQty.Text = MachinePrintedQty.ToString();
                  });
            HandpayQty = 0;
            ProgressiveQty = 0;
            JackpotQty = 0;
            RefundQty = 0;
            RefillQty = 0;
            ShortpayQty = 0;
            CashDeskFloatQty = 0;
            CashDeskClaimedQty = 0;
            CashDeskPrintedQty = 0;
            MachineClaimedQty = 0;
            MachinePrintedQty = 0;

            return lstTicketsClaimed;
        }


        private void loadWorker()
        {
            int count = 1;


            _worker.DoWork += (s, args) =>
            {
                BackgroundWorker worker = s as BackgroundWorker;

                worker.ReportProgress(count * 10);

                if (worker.CancellationPending)
                {
                    args.Cancel = true;
                    return;
                }

                args.Result = LoadCashDeskDetails();
                System.Threading.Thread.Sleep(5);
            };

            _worker.ProgressChanged += new ProgressChangedEventHandler(_worker_ProgressChanged);

            _worker.RunWorkerCompleted += (s, args) =>
            {
                System.Windows.Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate
                {
                    lstTicketExcep = (List<TicketExceptions>) args.Result;
                });
            };

            _worker.RunWorkerAsync();
        }

        void _worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            int i = e.ProgressPercentage;
            prgViewAll.Value = i;
        }

        private void btnPrint_Copy_Click(object sender, RoutedEventArgs e)
        {
         
            popupgrid.IsOpen = true;
            //CashDeskManagerAllDetails objViewAll = new CashDeskManagerAllDetails("0", "01 Sep 2008", "02 Sep 2008",
            // StartTime, EndTime, (bool)chkRefills.IsChecked, (bool)chkRefunds.IsChecked, (bool)chkCashDeskFloat.IsChecked, (bool)chkjackpot.IsChecked,
            // (bool)chkProghandpays.IsChecked, (bool)chkShortPays.IsChecked);
            if (lstTicketExcep != null)
            {

                CashDeskManagerAllDetails objViewAll = new CashDeskManagerAllDetails(lstTicketExcep);
                exceptiongrid.Children.Add(objViewAll);
                objViewAll.Margin = new Thickness(0);
                objViewAll.BringIntoView();
            }
        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {

            //MessageBox.showBox("Printing Started");
            //Thread.Sleep(5000);
            //MessageBox.showBox("Printing Failed");
        }

        private void btnReconciliation_Click(object sender, RoutedEventArgs e)
        {

            if (string.IsNullOrEmpty(StartDate) || string.IsNullOrEmpty(EndDate))
            {
                BMC.Presentation.CashDeskManager.MessageBox.showBox("Not a valid Date Range");
                return;
            }
            if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\CashdeskReports.exe"))
            {
                Process proc = new System.Diagnostics.Process();
                proc.EnableRaisingEvents = false;
                proc.StartInfo.FileName = "CashdeskReports.exe";
                proc.StartInfo.Arguments = "/TYPE=CDRECON /STARTDATE=" + StartDate + " " + StartTime + " /ENDDATE=" + EndDate + " " + EndTime;
                proc.Start();
            }
            else
            {
                BMC.Presentation.CashDeskManager.MessageBox.showBox("Missing cashdeskreport module");
            }
        }

        private void btnMovement_Click(object sender, RoutedEventArgs e)
        {

            if (string.IsNullOrEmpty(StartDate) || string.IsNullOrEmpty(EndDate))
            {
                BMC.Presentation.CashDeskManager.MessageBox.showBox("Not a valid Date Range");
                return;
            }
            if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\CashdeskReports.exe"))
            {
                Process proc = new System.Diagnostics.Process();
                proc.EnableRaisingEvents = false;
                proc.StartInfo.FileName = "CashdeskReports.exe";
                busTreasury = new TreasuryTransactions();
                if (busTreasury.GetRegionFromSite().Equals("US"))
                {
                    proc.StartInfo.Arguments = "/TYPE=CDRESULTUS /STARTDATE=" + StartDate + " " + StartTime + " /ENDDATE=" + EndDate + " " + EndTime;
                }
                else if (busTreasury.GetRegionFromSite().Equals("UK"))
                {
                    proc.StartInfo.Arguments = "/TYPE=CDRESULT /STARTDATE=" + StartDate + " " + StartTime + " /ENDDATE=" + EndDate + " " + EndTime;
                }
                proc.Start();
            }
            else
            {
                BMC.Presentation.CashDeskManager.MessageBox.showBox("Missing cashdeskreport module");
            }
        }

        private void btnBalancing_Click(object sender, RoutedEventArgs e)
        {

            if (string.IsNullOrEmpty(StartDate) || string.IsNullOrEmpty(EndDate))
            {
                BMC.Presentation.CashDeskManager.MessageBox.showBox("Not a valid Date Range");
                return;
            }

            if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\CashdeskReports.exe"))
            {
                Process proc = new System.Diagnostics.Process();
                proc.EnableRaisingEvents = false;
                proc.StartInfo.FileName = "CashdeskReports.exe";
                proc.StartInfo.Arguments = "/TYPE=CDRESULTCOLL /STARTDATE=" + StartDate + " " + StartTime + " /ENDDATE=" + EndDate + " " + EndTime;
                proc.Start();
            }
            else
            {
                 BMC.Presentation.CashDeskManager.MessageBox.showBox("Missing cashdeskreport module");
            }
        }

        private void dtpEndDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            EndDate = Convert.ToDateTime(dtpEndDate.Text).ToString("dd MMM yyyy");
        }

        private void dtpEndtime_SelectedTimeChanged(object sender, AC.AvalonControlsLibrary.Controls.TimeSelectedChangedRoutedEventArgs e)
        {
            EndTime = dtpEndtime.SelectedHour.ToString() + ":" + dtpEndtime.SelectedMinute.ToString() + ":" + dtpEndtime.SelectedSecond.ToString();
        }

        private void tmpStartTime_SelectedTimeChanged(object sender, AC.AvalonControlsLibrary.Controls.TimeSelectedChangedRoutedEventArgs e)
        {
            StartTime = tmpStartTime.SelectedHour.ToString() + ":" + tmpStartTime.SelectedMinute.ToString() + ":" + tmpStartTime.SelectedSecond.ToString();
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            Business.CashDeskManager.Common.CloseExcel();
        }
    }
}