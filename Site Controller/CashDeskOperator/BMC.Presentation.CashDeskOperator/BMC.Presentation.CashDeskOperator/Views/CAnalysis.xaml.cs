using System;
using System.ComponentModel;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using BMC.CashDeskOperator.BusinessObjects;
using BMC.Common.ExceptionManagement;
using BMC.Common.LogManagement;
using BMC.Common.Utilities;
using BMC.Presentation.POS.Helper_classes;
using Audit.BusinessClasses;
using Audit.Transport;
using BMC.Transport;
using BMC.Presentation.POS.Views;
using System.Collections.Generic;
using System.Linq;

namespace BMC.Presentation
{
    /// <summary>
    /// Interaction logic for CAnalysis.xaml
    /// </summary>
    public partial class CAnalysis : IDisposable
    {

        decimal m_sngOut;
        decimal m_sngIN;
        decimal m_sngAccCoinIn;
        decimal m_sngAccNotes;
        decimal m_sngAccTicketsIn;
        decimal m_sngAccTicketsOut;
        decimal m_sngAccCoinOut;
        decimal m_sngAccHandpay;
        decimal m_sngAccJackpot;
        decimal m_sngWL;

        decimal m_sngCashableEFTIn;
        decimal m_sngCashableEFTOut;
        decimal m_sngNonCashableEFTIn;
        decimal m_sngNonCashableEFTOut;
        decimal m_sngWATIn;
        decimal m_sngWATOut;

        decimal m_sngAccNonCashableTicketsIn;
        decimal m_sngAccNonCashableTicketsOut;
        decimal m_sngAccProgressiveJacpot;

        int lTicketQty;
        decimal sngTicketValue;
        int m_lTicketQty;
        decimal m_sngTicketValue;

        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private DateTime CheckWeekDate_;
        private DateTime CheckMonthDate_;
        DataTable dtCollection;
        IAnalysis analysisBusinessObject = AnalysisBusinessObject.CreateInstance();
        DataSet dtPrint = null;
        Option selectedOption = new Option();
        System.Threading.CancellationTokenSource c_cancel;
        System.Threading.Tasks.Task t_task;
        object lockObj = new object();
        public CAnalysis()
        {

            this.InitializeComponent();
            //chkDrop.IsChecked = true;
            backgroundWorker1 = new BackgroundWorker();
            backgroundWorker1.DoWork += new DoWorkEventHandler(backgroundWorker1_DoWork);
            backgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backgroundWorker1_RunWorkerCompleted);
        }

        public enum Option
        {
            Day,
            Drop,
            Week,
            Month
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

            //UserControl.pnlButtons.Visibility = Visibility.Visible;
            UserControl.InnerGrid.Visibility = Visibility.Hidden;
            chkDrop.IsChecked = true;

            chkWeekInvoice.Visibility = Visibility.Collapsed;
            chkMonth.Visibility = Settings.EnableMonthToDateTab ? Visibility.Visible : Visibility.Collapsed;
            if (Settings.IsAFTEnabledForSite)
            {
                gvColumnDescription.Width = 364;
                gvColumnValue.Width = 332;
            }
            else
            {
                gvColumnDescription.Width = 354;
                gvColumnValue.Width = 342;
            }
            if (!Settings.WeeklyReport)
            {
                chkWeekInvoice.Visibility = Visibility.Collapsed;
            }
            if (!Settings.ShowSystemCalendar)
            {
                chkMonth.Content = FindResource("CAnalysis_xaml_chkPeriod") as string; 
            }
        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CancelTask(ClearBinding: false);
                btnPrint.IsEnabled = false;
                lblStatus.Content = "";
                progressBar1.Visibility = Visibility.Hidden;
                string sReportName = string.Empty;
                if ((bool)chkDay.IsChecked)
                {
                    sReportName = chkDay.Content.ToString();
                    Audit_Print("Current Day Report ", DateTime.Now);
                }
                else if ((bool)chkDrop.IsChecked)
                {
                    sReportName = chkDrop.Content.ToString();
                    Audit_Print("Current Drop Report ", DateTime.Now);
                }
                else if ((bool)chkWeek.IsChecked)
                {
                    sReportName = chkWeek.Content.ToString();
                    Audit_Print("Current Week Report ", DateTime.Now);
                }
                else if ((bool)chkMonth.IsChecked)
                {
                    sReportName = chkMonth.Content.ToString();
                    if (!Settings.ShowSystemCalendar)
                        Audit_Print("Current Period Report ", DateTime.Now);
                    else
                        Audit_Print("Current Month Report ", DateTime.Now);
                }

                using (CReportViewer cReportViewer = new CReportViewer())
                    cReportViewer.ShowReportsPrintTab(dtPrint, sReportName, Settings.SiteCode);
            }
            finally
            {
                btnPrint.IsEnabled = true;
            }

        }


        private void Audit_Print(string sReport, DateTime dtPrintDate)
        {
            AuditViewerBusiness.InsertAuditData(new Audit.Transport.Audit_History
            {

                AuditModuleName = ModuleName.PrintReports,
                Audit_Screen_Name = "Reports",
                Audit_Desc = sReport + "is printed.",
                AuditOperationType = OperationType.ADD,
                Audit_Field = "Print Date",
                Audit_New_Vl = dtPrintDate.Date.ToString("d")
            });
        }



        private DataTable GetWTDMTD(DateTime dtNow)
        {
            return analysisBusinessObject.GetWTDMTD();
        }

        private DataTable BuildDataTable(bool IsToday, bool IsCollect, DateTime IsDate, Option reportType)
        {

            try
            {
                GetData(IsToday, IsCollect, IsDate, reportType.ToString());
            }
            catch (Exception ex)
            {
                m_sngOut = 0;
                m_sngIN = 0;
                m_sngAccCoinIn = 0;
                m_sngAccNotes = 0;
                m_sngAccTicketsIn = 0;
                m_sngAccTicketsOut = 0;
                m_sngAccCoinOut = 0;
                m_sngAccHandpay = 0;
                m_sngWL = 0;
                lTicketQty = 0;
                sngTicketValue = 0;
                m_sngAccNonCashableTicketsIn = 0;
                m_sngAccNonCashableTicketsOut = 0;
                m_sngAccProgressiveJacpot = 0;
                m_sngTicketValue = 0;
                m_lTicketQty = 0;
                ExceptionManager.Publish(ex);
                LogManager.WriteLog("BuildDataTable", LogManager.enumLogLevel.Debug);
            }

            DataTable dtSiteSummary = new DataTable();
            dtSiteSummary.Columns.Add("Description");
            dtSiteSummary.Columns.Add("Value");
            DataRow drSiteSummary = dtSiteSummary.NewRow();
            //drSiteSummary[0] = "Coins In";
            drSiteSummary[0] = FindResource("CAnalysis_xaml_Datatable_DataRow_0") as string;
            drSiteSummary[1] = m_sngAccCoinIn.GetUniversalCurrencyFormatWithSymbol();
            dtSiteSummary.Rows.Add(drSiteSummary);

            drSiteSummary = dtSiteSummary.NewRow();
            //drSiteSummary[0] = "Bills In";
            drSiteSummary[0] = FindResource("CAnalysis_xaml_Datatable_DataRow_1") as string;
            drSiteSummary[1] = m_sngAccNotes.GetUniversalCurrencyFormatWithSymbol();
            dtSiteSummary.Rows.Add(drSiteSummary);

            drSiteSummary = dtSiteSummary.NewRow();
            //drSiteSummary[0] = "Cashable Vouchers In";
            drSiteSummary[0] = FindResource("CAnalysis_xaml_Datatable_DataRow_2") as string;
            drSiteSummary[1] = m_sngAccTicketsIn.GetUniversalCurrencyFormatWithSymbol();
            dtSiteSummary.Rows.Add(drSiteSummary);

            drSiteSummary = dtSiteSummary.NewRow();
            //drSiteSummary[0] = "Non Cashable Vouchers In";
            drSiteSummary[0] = FindResource("CAnalysis_xaml_Datatable_DataRow_16") as string;
            drSiteSummary[1] = m_sngAccNonCashableTicketsIn.GetUniversalCurrencyFormatWithSymbol();
            dtSiteSummary.Rows.Add(drSiteSummary);

            if (Settings.IsAFTEnabledForSite)
            {
                drSiteSummary = dtSiteSummary.NewRow();
                //drSiteSummary[0] = "Cashable EFT In";
                drSiteSummary[0] = FindResource("CAnalysis_xaml_Datatable_DataRow_10") as string;
                drSiteSummary[1] = m_sngCashableEFTIn.GetUniversalCurrencyFormatWithSymbol();
                dtSiteSummary.Rows.Add(drSiteSummary);

                drSiteSummary = dtSiteSummary.NewRow();
                //drSiteSummary[0] = "Non Cashable EFT In";
                drSiteSummary[0] = FindResource("CAnalysis_xaml_Datatable_DataRow_12") as string;
                drSiteSummary[1] = m_sngNonCashableEFTIn.GetUniversalCurrencyFormatWithSymbol();
                dtSiteSummary.Rows.Add(drSiteSummary);

                drSiteSummary = dtSiteSummary.NewRow();
                //drSiteSummary[0] = "WAT In";
                drSiteSummary[0] = FindResource("CAnalysis_xaml_Datatable_DataRow_14") as string;
                drSiteSummary[1] = m_sngWATIn.GetUniversalCurrencyFormatWithSymbol();
                dtSiteSummary.Rows.Add(drSiteSummary);
            }

            drSiteSummary = dtSiteSummary.NewRow();
            //drSiteSummary[0] = "Total Cash In";
            drSiteSummary[0] = FindResource("CAnalysis_xaml_Datatable_DataRow_3") as string;
            drSiteSummary[1] = m_sngIN.GetUniversalCurrencyFormatWithSymbol();
            dtSiteSummary.Rows.Add(drSiteSummary);

            //drSiteSummary = dtSiteSummary.NewRow();
            //drSiteSummary[0] = "";
            //drSiteSummary[1] = "";
            //dtSiteSummary.Rows.Add(drSiteSummary);

            drSiteSummary = dtSiteSummary.NewRow();
            //drSiteSummary[0] = "Coins Out";
            drSiteSummary[0] = FindResource("CAnalysis_xaml_Datatable_DataRow_4") as string;
            drSiteSummary[1] = m_sngAccCoinOut.GetUniversalCurrencyFormatWithSymbol();
            dtSiteSummary.Rows.Add(drSiteSummary);

            drSiteSummary = dtSiteSummary.NewRow();
            //drSiteSummary[0] = "Cashable Vouchers Out";
            drSiteSummary[0] = FindResource("CAnalysis_xaml_Datatable_DataRow_5") as string;
            drSiteSummary[1] = m_sngAccTicketsOut.GetUniversalCurrencyFormatWithSymbol();
            dtSiteSummary.Rows.Add(drSiteSummary);

            drSiteSummary = dtSiteSummary.NewRow();
            //drSiteSummary[0] = "Non Cashable Vouchers Out";
            drSiteSummary[0] = FindResource("CAnalysis_xaml_Datatable_DataRow_17") as string;
            drSiteSummary[1] = m_sngAccNonCashableTicketsOut.GetUniversalCurrencyFormatWithSymbol();
            dtSiteSummary.Rows.Add(drSiteSummary);

            drSiteSummary = dtSiteSummary.NewRow();
            //drSiteSummary[0] = "Attendant Pays";
            drSiteSummary[0] = FindResource("CAnalysis_xaml_Datatable_DataRow_6") as string;
            drSiteSummary[1] = m_sngAccHandpay.GetUniversalCurrencyFormatWithSymbol();
            dtSiteSummary.Rows.Add(drSiteSummary);

            drSiteSummary = dtSiteSummary.NewRow();
            //drSiteSummary[0] = "Jackpot Pays";
            drSiteSummary[0] = FindResource("CAnalysis_xaml_Datatable_DataRow_18") as string;
            drSiteSummary[1] = m_sngAccJackpot.GetUniversalCurrencyFormatWithSymbol();
            dtSiteSummary.Rows.Add(drSiteSummary);

            drSiteSummary = dtSiteSummary.NewRow();
            //drSiteSummary[0] = "Progressive Pays";
            drSiteSummary[0] = FindResource("CAnalysis_xaml_Datatable_DataRow_19") as string;
            drSiteSummary[1] = m_sngAccProgressiveJacpot.GetUniversalCurrencyFormatWithSymbol();
            dtSiteSummary.Rows.Add(drSiteSummary);

            if (Settings.IsAFTEnabledForSite)
            {
                drSiteSummary = dtSiteSummary.NewRow();
                //drSiteSummary[0] = "Cashable EFT Out";
                drSiteSummary[0] = FindResource("CAnalysis_xaml_Datatable_DataRow_11") as string;
                drSiteSummary[1] = m_sngCashableEFTOut.GetUniversalCurrencyFormatWithSymbol();
                dtSiteSummary.Rows.Add(drSiteSummary);

                drSiteSummary = dtSiteSummary.NewRow();
                //drSiteSummary[0] = "Non Cashable EFT Out";
                drSiteSummary[0] = FindResource("CAnalysis_xaml_Datatable_DataRow_13") as string;
                drSiteSummary[1] = m_sngNonCashableEFTOut.GetUniversalCurrencyFormatWithSymbol();
                dtSiteSummary.Rows.Add(drSiteSummary);

                drSiteSummary = dtSiteSummary.NewRow();
                //drSiteSummary[0] = "WAT Out";
                drSiteSummary[0] = FindResource("CAnalysis_xaml_Datatable_DataRow_15") as string;
                drSiteSummary[1] = m_sngWATOut.GetUniversalCurrencyFormatWithSymbol();
                dtSiteSummary.Rows.Add(drSiteSummary);
            }

            drSiteSummary = dtSiteSummary.NewRow();
            //drSiteSummary[0] = "Total Cash Out";
            drSiteSummary[0] = FindResource("CAnalysis_xaml_Datatable_DataRow_7") as string;
            drSiteSummary[1] = m_sngOut.GetUniversalCurrencyFormatWithSymbol();
            dtSiteSummary.Rows.Add(drSiteSummary);

            //drSiteSummary = dtSiteSummary.NewRow();
            //drSiteSummary[0] = "";
            //drSiteSummary[1] = "";
            //dtSiteSummary.Rows.Add(drSiteSummary);

            drSiteSummary = dtSiteSummary.NewRow();
            //drSiteSummary[0] = "Net Win";
            drSiteSummary[0] = FindResource("CAnalysis_xaml_Datatable_DataRow_8") as string;
            drSiteSummary[1] = m_sngWL.GetUniversalCurrencyFormatWithSymbol();
            dtSiteSummary.Rows.Add(drSiteSummary);

            //drSiteSummary = dtSiteSummary.NewRow();
            //drSiteSummary[0] = "";
            //drSiteSummary[1] = "";
            //dtSiteSummary.Rows.Add(drSiteSummary);

            drSiteSummary = dtSiteSummary.NewRow();
            //drSiteSummary[0] = "Vouchers Paid                (" + lTicketQty.ToString() + ")";
            drSiteSummary[0] = string.Format("{0}                ({1})", FindResource("CAnalysis_xaml_Datatable_DataRow_9") as string, m_lTicketQty.ToString());
            drSiteSummary[1] = m_sngTicketValue.GetUniversalCurrencyFormatWithSymbol();
            dtSiteSummary.Rows.Add(drSiteSummary);

            this.Dispatcher.BeginInvoke(new Action(() =>
            {
                Binding bind = new Binding();
                bind.Source = dtSiteSummary;
                dgGridView.SetBinding(ListView.ItemsSourceProperty, bind);
            }), null);

            dtPrint = new DataSet();
            dtPrint.Tables.Add(dtSiteSummary);
            dtPrint.Tables[0].TableName = "ReportsTabPrint";

            return new DataTable();


        }

        private void GetData(bool IsToday, bool IsCollect, DateTime IsDate, string reportType)
        {
            decimal sngNotes;
            decimal sngTicketsIn;
            decimal sngTicketsOut;
            decimal sngCoinsOut;
            decimal sngHandpay;
            decimal sngJackpot;
            decimal sngCoinsIn;

            decimal sngCashableEFTIn;
            decimal sngCashableEFTOut;
            decimal sngNonCashableEFTIn;
            decimal sngNonCashableEFTOut;
            decimal sngWATIn;
            decimal sngWATOut;

            decimal sngNonCashableTicketsIn;
            decimal sngNonCashableTicketsOut;
            decimal sngProgressiveJackpot;

            int IToken;
            int lPOP;

            try
            {

                m_sngOut = 0;
                m_sngIN = 0;
                m_sngAccCoinIn = 0;
                m_sngAccNotes = 0;
                m_sngAccTicketsIn = 0;
                m_sngAccTicketsOut = 0;
                m_sngAccCoinOut = 0;
                m_sngAccHandpay = 0;
                m_sngAccJackpot = 0;
                m_sngWL = 0;

                m_sngCashableEFTIn = 0;
                m_sngCashableEFTOut = 0;
                m_sngNonCashableEFTIn = 0;
                m_sngNonCashableEFTOut = 0;
                m_sngWATIn = 0;
                m_sngWATOut = 0;

                m_sngAccNonCashableTicketsIn = 0;
                m_sngAccNonCashableTicketsOut = 0;
                m_sngAccProgressiveJacpot = 0;

                m_sngTicketValue = 0;
                m_lTicketQty = 0;

                sngTicketValue = 0;
                lTicketQty = 0;

                sngNotes = 0;
                sngTicketsIn = 0;
                sngTicketsOut = 0;
                sngCoinsOut = 0;
                sngHandpay = 0;
                sngJackpot = 0;
                sngCoinsIn = 0;

                sngCashableEFTIn = 0;
                sngCashableEFTOut = 0;
                sngNonCashableEFTIn = 0;
                sngNonCashableEFTOut = 0;
                sngWATIn = 0;
                sngWATOut = 0;

                sngNonCashableTicketsIn = 0;
                sngNonCashableTicketsOut = 0;
                sngProgressiveJackpot = 0;

                DataTable dt = new DataTable();
                dt = analysisBusinessObject.GetInstallationsForReports(reportType);


                //Tuple<Func<int[], System.Collections.Concurrent.Partitioner<int>>> t_p = Tuple.Create<Func<int[], System.Collections.Concurrent.Partitioner<int>>>(e => System.Collections.Concurrent.Partitioner.Create(e, true));
                //lblStatus.Content = "Loading Installations (0/"+dt.Rows.Count+")";
                //lock (lockObj)
                {
                    //System.Threading.Tasks.Parallel.ForEach(t_p.Item1(Enumerable.Range(0, dt.Rows.Count).ToArray()), new System.Threading.Tasks.ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount }, (i, state) =>
                    //foreach (DataRow dr in dt.Rows)
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataRow dr = dt.Rows[i];

                        if (c_cancel.IsCancellationRequested)
                        {
                            this.Dispatcher.BeginInvoke(new Action(() =>
                                {
                                    lblStatus.Content = "Cancelling... ";
                                }), null);
                            break;
                        }
                        this.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            lblStatus.Content = "Loading Installations... (" + i + "/" + dt.Rows.Count + ")";
                        }), null);
                        //IToken = int.Parse(dt.Rows[0]["installation_token_value"].ToString());
                        //lPOP = int.Parse(dt.Rows[0]["installation_Price_Of_Play"].ToString());

                        if (dr["installation_token_value"] != DBNull.Value)
                            IToken = int.Parse(Convert.ToString(dr["installation_token_value"]));
                        else
                            IToken = 0;
                        if (dr["installation_Price_Of_Play"] != DBNull.Value)
                            lPOP = int.Parse(Convert.ToString(dr["installation_Price_Of_Play"]));
                        else
                            lPOP = 0;


                        GetDetails(BMC.Presentation.Helper_classes.Common.GetRowValue<int>(dr, "Installation_No"), lPOP, IToken,
                            ref sngCoinsIn, ref  sngNotes, ref sngTicketsIn, ref sngTicketsOut, ref sngHandpay, ref sngJackpot, ref  sngCoinsOut,
                            ref sngCashableEFTIn, ref sngCashableEFTOut, ref sngNonCashableEFTIn, ref sngNonCashableEFTOut, ref sngWATIn, ref sngWATOut,
                            ref sngNonCashableTicketsIn, ref sngNonCashableTicketsOut, ref sngProgressiveJackpot, ref IsDate, IsToday, IsCollect);
                        m_sngAccCoinIn = m_sngAccCoinIn + sngCoinsIn;
                        m_sngAccNotes = m_sngAccNotes + sngNotes;
                        m_sngAccTicketsIn = m_sngAccTicketsIn + sngTicketsIn;
                        m_sngAccTicketsOut = m_sngAccTicketsOut + sngTicketsOut;

                        m_sngAccCoinOut = m_sngAccCoinOut + sngCoinsOut;
                        m_sngAccHandpay = m_sngAccHandpay + sngHandpay;
                        m_sngAccJackpot = m_sngAccJackpot + sngJackpot;

                        m_sngCashableEFTIn = m_sngCashableEFTIn + sngCashableEFTIn;
                        m_sngCashableEFTOut = m_sngCashableEFTOut + sngCashableEFTOut;
                        m_sngNonCashableEFTIn = m_sngNonCashableEFTIn + sngNonCashableEFTIn;
                        m_sngNonCashableEFTOut = m_sngNonCashableEFTOut + sngNonCashableEFTOut;
                        m_sngWATIn = m_sngWATIn + sngWATIn;
                        m_sngWATOut = m_sngWATOut + sngWATOut;

                        m_sngAccNonCashableTicketsIn = m_sngAccNonCashableTicketsIn + sngNonCashableTicketsIn;
                        m_sngAccNonCashableTicketsOut = m_sngAccNonCashableTicketsOut + sngNonCashableTicketsOut;
                        m_sngAccProgressiveJacpot = m_sngAccProgressiveJacpot + sngProgressiveJackpot;
                        //   });
                    }
                    GetTicketsCashedAtCD(ref sngTicketValue, ref lTicketQty, ref IsToday, ref IsCollect, ref IsDate, 0);

                    m_sngTicketValue = sngTicketValue;
                    m_lTicketQty = lTicketQty;

                    m_sngIN = m_sngAccNotes + m_sngAccTicketsIn + m_sngAccNonCashableTicketsIn + m_sngAccCoinIn;
                    m_sngOut = m_sngAccTicketsOut + m_sngAccNonCashableTicketsOut + m_sngAccHandpay + m_sngAccCoinOut + m_sngAccJackpot;

                    if (Settings.IsAFTIncludedInCalculation)
                    {
                        m_sngIN += m_sngCashableEFTIn + m_sngNonCashableEFTIn + m_sngWATIn;
                        m_sngOut += m_sngCashableEFTOut + m_sngNonCashableEFTOut + m_sngWATOut;
                    }

                    m_sngWL = m_sngIN - m_sngOut;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                LogManager.WriteLog("GetData", LogManager.enumLogLevel.Debug);
            }
        }


        private void GetDetails(int lInstallationNo, int lPOP, int lTokenValue, ref decimal sngCoinsIn, ref decimal sngNotes,
            ref decimal sngTicketsIn, ref decimal sngTicketsOut, ref decimal sngHandpay, ref decimal sngJackpot, ref decimal sngCoinsOut,
            ref decimal sngCashableEFTIn, ref decimal sngCashableEFTOut, ref decimal sngNonCashableEFTIn, ref decimal sngNonCashableEFTOut,
            ref decimal sngWATIn, ref decimal sngWATOut, ref decimal sngNonCashableTicketsIn, ref decimal sngNonCashableTicketsOut,
            ref decimal sngProgressiveJackpot, ref DateTime IsDate, bool IsToday, bool IsCollect)
        {
            int SelectDay = 0;

            try
            {
                if (IsToday)
                    SelectDay = 1;

                if (IsCollect)
                    SelectDay = 2;


                if (IsDate == DateTime.Parse("01/01/1753", System.Globalization.CultureInfo.CreateSpecificCulture("en-CA")))
                    IsDate = DateTime.Parse("01/01/1753", System.Globalization.CultureInfo.CreateSpecificCulture("en-CA"));
                else if (IsDate.ToString() == DateTime.MinValue.ToString())
                    IsDate = DateTime.Parse("01/01/1753", System.Globalization.CultureInfo.CreateSpecificCulture("en-CA"));

                DataTable dt = new DataTable();
                dt = analysisBusinessObject.GetSpotCheckDataSAS(lInstallationNo, SelectDay, IsDate);

                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["TRUE_COIN_IN"] == DBNull.Value)
                    {
                        sngCoinsIn = 0;
                    }
                    else
                    {
                        sngCoinsIn = (decimal.Parse(dr["TRUE_COIN_IN"].ToString()) * (lTokenValue / 100));

                    }

                    sngCoinsIn = decimal.Parse(dr["TRUE_COIN_IN"].ToString()) * (decimal.Parse(lTokenValue.ToString()) / 100);
                    if (System.Globalization.CultureInfo.CurrentCulture.Name == "it-IT" || System.Globalization.CultureInfo.CurrentUICulture.Name == "it-IT" ||
                      (Settings.RegulatoryEnabled && Settings.RegulatoryType == "AAMS"))
                        sngNotes = (decimal.Parse(dr["BILL_1"].ToString()) * 1) +
                            (decimal.Parse(dr["BILL_2"].ToString()) * 2) +
                            (decimal.Parse(dr["BILL_5"].ToString()) * 5) +
                            (decimal.Parse(dr["BILL_10"].ToString()) * 10) +
                            (decimal.Parse(dr["BILL_20"].ToString()) * 20) +
                            (decimal.Parse(dr["BILL_50"].ToString()) * 50) +
                            (decimal.Parse(dr["BILL_100"].ToString()) * 100) +
                            ((dr["BILL_200"] != null ? decimal.Parse(dr["BILL_200"].ToString()) * 200 : 0)) +
                            ((dr["BILL_500"] != null ? decimal.Parse(dr["BILL_500"].ToString()) * 500 : 0));
                    else if (Settings.Region == "AR")
                        sngNotes = (decimal.Parse(dr["BILL_2"].ToString()) * 2) +
                            (decimal.Parse(dr["BILL_5"].ToString()) * 5) +
                            (decimal.Parse(dr["BILL_10"].ToString()) * 10) +
                            (decimal.Parse(dr["BILL_20"].ToString()) * 20) +
                            (decimal.Parse(dr["BILL_50"].ToString()) * 50) +
                            (decimal.Parse(dr["BILL_100"].ToString()) * 100);
                    else
                        sngNotes = (decimal.Parse(dr["BILL_1"].ToString()) * 1) +
                            (decimal.Parse(dr["BILL_2"].ToString()) * 2) +
                            (decimal.Parse(dr["BILL_5"].ToString()) * 5) +
                            (decimal.Parse(dr["BILL_10"].ToString()) * 10) +
                            (decimal.Parse(dr["BILL_20"].ToString()) * 20) +
                            (decimal.Parse(dr["BILL_50"].ToString()) * 50) +
                            (decimal.Parse(dr["BILL_100"].ToString()) * 100);

                    sngTicketsIn = (decimal.Parse(long.Parse(dr["Ticktes_Inserted"].ToString()).ToString()) / 100);
                    sngCoinsOut = decimal.Parse(dr["TRUE_COIN_OUT"].ToString()) * (decimal.Parse(lTokenValue.ToString()) / 100);
                    //sngHandpay = ((decimal.Parse(dr["HANDPAY"].ToString()) * lPOP) / 100 +
                    //    ((decimal.Parse(dr["JACKPOT"].ToString()) * lPOP) / 100));
                    sngHandpay = (decimal.Parse(dr["HANDPAY"].ToString()) * lPOP) / 100;
                    sngJackpot = (decimal.Parse(dr["JACKPOT"].ToString()) * lPOP) / 100;
                    sngTicketsOut = (decimal.Parse(dr["Tickets_Printed"].ToString()) / 100);

                    //AFT meters
                    sngCashableEFTIn = (decimal.Parse(dr["CashableAFTIn"].ToString()) / 100);
                    sngCashableEFTOut = (decimal.Parse(dr["CashableAFTOut"].ToString()) / 100);
                    sngNonCashableEFTIn = (decimal.Parse(dr["NonCashableAFTIn"].ToString()) / 100);
                    sngNonCashableEFTOut = (decimal.Parse(dr["NonCashableAFTOut"].ToString()) / 100);
                    sngWATIn = (decimal.Parse(dr["WATIn"].ToString()) / 100);
                    sngWATOut = (decimal.Parse(dr["WATOut"].ToString()) / 100);

                    sngNonCashableTicketsIn = (decimal.Parse(dr["NonCashable_Tickets_Inserted"].ToString()) / 100);
                    sngNonCashableTicketsOut = (decimal.Parse(dr["NonCashable_Tickets_Printed"].ToString()) / 100);
                    sngProgressiveJackpot = (decimal.Parse(dr["ProgHandpay"].ToString()) * lPOP) / 100;

                    return;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                LogManager.WriteLog("GetDetails", LogManager.enumLogLevel.Debug);
            }
        }

        private void GetTicketsCashedAtCD(ref decimal sngTicketsValue, ref int lQty, ref bool IsToday, ref bool IsCollect, ref DateTime IsDate, int MachineNo)
        {

            int SelectDay = 0;

            try
            {
                if (IsToday)
                    SelectDay = 1;

                if (IsCollect)
                    SelectDay = 2;

                if (IsDate == DateTime.Parse("01/01/1753", System.Globalization.CultureInfo.CreateSpecificCulture("en-CA")))
                    IsDate = DateTime.Parse("01/01/1753", System.Globalization.CultureInfo.CreateSpecificCulture("en-CA"));
                else if (IsDate.ToString() == DateTime.MinValue.ToString())
                    IsDate = DateTime.Parse("01/01/1753", System.Globalization.CultureInfo.CreateSpecificCulture("en-CA"));

                DataTable dt = new DataTable();
                dt = analysisBusinessObject.GetTicketsClaimedByCDForPeriod(SelectDay, IsDate, MachineNo);

                foreach (DataRow dr in dt.Rows)
                {
                    try
                    {
                        if (dr["TotalValue"] == DBNull.Value)
                            sngTicketsValue = 0;
                        else
                            sngTicketsValue = BMC.Presentation.Helper_classes.Common.GetRowValue<decimal>(dr, "TotalValue");
                    }
                    catch
                    {
                        try { sngTicketsValue = Convert.ToInt32(BMC.Presentation.Helper_classes.Common.GetRowValue<Decimal>(dr, "TotalValue").ToString()); }
                        catch { }
                    }
                    if (dr["TotalQty"] == DBNull.Value)
                    {
                        sngTicketsValue = 0;
                    }
                    else
                    {
                        lQty = BMC.Presentation.Helper_classes.Common.GetRowValue<int>(dr, "TotalQty");
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                LogManager.WriteLog("GetTicketsCashedAtCD", LogManager.enumLogLevel.Debug);
            }
        }

        private void chkDrop_Checked(object sender, RoutedEventArgs e)
        {
            SetVisibiltyToListviews(Visibility.Hidden, Visibility.Visible);
            dgGridWeek.Visibility = Visibility.Hidden;
            btnDetails.Visibility = Visibility.Visible;
            try
            {
                if (c_cancel != null)
                {
                    c_cancel.Cancel();
                }
                chkDrop.IsEnabled = true;
                chkDay.IsEnabled = false;
                chkWeek.IsEnabled = false;
                chkMonth.IsEnabled = false;
                UserControl.Cursor = Cursors.Wait;
                selectedOption = Option.Drop;
                DateTime dtDate = new DateTime();
                //UserControl.pnlButtons.Visibility = Visibility.Visible;
                UserControl.OuterGrid.Visibility = Visibility.Visible;
                UserControl.InnerGrid.Visibility = Visibility.Hidden;
                this.dgGridView.Visibility = Visibility.Visible;
                this.btnPrint.Visibility = Visibility.Visible;
               // this.btnDetails.Margin = new Thickness(750, 0, 0, 300);
                StartTask(() =>
                    {
                        BuildDataTable(false, true, dtDate, Option.Drop);
                    });




            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                LogManager.WriteLog("chkDrop_Checked", LogManager.enumLogLevel.Debug);

            }
            finally
            {
                UserControl.Cursor = Cursors.Arrow;
            }
        }

        private void StartTask(Action act_task)
        {
            try
            {
                btncancel.Visibility = Visibility.Visible;
                c_cancel = new System.Threading.CancellationTokenSource();

                System.Threading.CancellationToken c_tok = c_cancel.Token;

                t_task = System.Threading.Tasks.Task.Factory.StartNew(obj =>
                {
                    act_task();

                }, c_tok, System.Threading.Tasks.TaskCreationOptions.PreferFairness);
                t_task.ContinueWith(new Action<System.Threading.Tasks.Task>((t) =>
                {
                    this.Dispatcher.BeginInvoke(new Action(() =>
                    {
                        HideLoadingControls();
                    }), null);

                }));



            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);


            }
            //act_task();
            //HideLoadingControls();
        }

        private void HideLoadingControls()
        {
            lblStatus.Visibility = Visibility.Hidden;
            progressBar1.Visibility = Visibility.Hidden;
            chkDrop.IsEnabled = true;
            chkWeek.IsEnabled = true;
            chkDay.IsEnabled = true;
            chkMonth.IsEnabled = true;
            btncancel.Visibility = Visibility.Hidden;
        }
        private void chkDay_Checked(object sender, RoutedEventArgs e)
        {
            SetVisibiltyToListviews(Visibility.Hidden, Visibility.Visible);
            try
            {
				UserControl.OuterGrid.Visibility = Visibility.Visible;                
				if (c_cancel != null)
                {
                    c_cancel.Cancel();
                }
                UserControl.Cursor = Cursors.Wait;
                selectedOption = Option.Day;
                chkDrop.IsEnabled = false;
                chkDay.IsEnabled = true;
                chkWeek.IsEnabled = false;
                chkMonth.IsEnabled = false;
                DateTime dtDate = new DateTime();
                //UserControl.pnlButtons.Visibility = Visibility.Visible;
                UserControl.InnerGrid.Visibility = Visibility.Hidden;
                this.dgGridView.Visibility = Visibility.Visible;
                this.btnPrint.Visibility = Visibility.Visible;
                //this.btnDetails.Margin = new Thickness(750, 0, 0, 300);
                StartTask(() =>
                {
                    BuildDataTable(true, false, dtDate, Option.Day);
                });
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                LogManager.WriteLog("chkDay_Checked", LogManager.enumLogLevel.Debug);

            }
            finally
            {
                UserControl.Cursor = Cursors.Arrow;
            }
        }

        private void chkMonth_Checked(object sender, RoutedEventArgs e)
        {
            SetVisibiltyToListviews(Visibility.Hidden, Visibility.Visible);
            DateTime dtCurrentMonth;
            DataTable dtCheckWeek = null;
            try
            {
                if (c_cancel != null)
                {
                    c_cancel.Cancel();
                }
                chkDrop.IsEnabled = false;
                chkWeek.IsEnabled = false;
                chkDay.IsEnabled = false;
                chkMonth.IsEnabled = true;
				OuterGrid.Visibility = Visibility.Visible;
                UserControl.Cursor = Cursors.Wait;
                selectedOption = Option.Month;
                UserControl.InnerGrid.Visibility = Visibility.Hidden;
                this.dgGridView.Visibility = Visibility.Visible;
                this.btnPrint.Visibility = Visibility.Visible;
                //this.btnDetails.Margin = new Thickness(750, 0, 0, 300);
                dtCheckWeek = GetWTDMTD(DateTime.Now);
                if (dtCheckWeek.Rows.Count > 0)
                {
                    dtCurrentMonth = DateTime.Parse(dtCheckWeek.Rows[0]["MonthStart"].ToString());
                    CheckMonthDate_ = dtCurrentMonth;
                    if (dtCurrentMonth != null)
                    {
                        StartTask(() =>
                        {
                            BuildDataTable(false, false, dtCurrentMonth, Option.Month);
                        });
                    }
                    else
                    {
                        OuterGrid.Visibility = Visibility.Collapsed;
                        MessageBox.ShowBox("MessageID892", BMC_Icon.Warning);
                        

                    }

                }              
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                LogManager.WriteLog("chkMonth_Checked", LogManager.enumLogLevel.Debug);
            }
            finally
            {
                UserControl.Cursor = Cursors.Arrow;
            }
        }

        //private void chkCustom_Checked(object sender, RoutedEventArgs e)
        //{
        //    //MessageBox.showBox(WFHostStartTime.Margin.Left.ToString());
        //    try
        //    {
        //        UserControl.InnerGrid.Visibility = Visibility.Visible; 
        //        System.Windows.Forms.DateTimePicker dtstarttime = new System.Windows.Forms.DateTimePicker();
        //        System.Windows.Forms.DateTimePicker dtEndtime = new System.Windows.Forms.DateTimePicker();
        //        //dtstarttime.Left = 40;
        //        //dtstarttime.Width = 50;            
        //        WFHostStartTime.Child = dtstarttime;               
        //        dtEndtime.CalendarForeColor = System.Drawing.Color.Blue;
        //        //dtEndtime.Left = 315;
        //        //dtEndtime.Width = 50;
        //        WFHostEndTime.Child = dtEndtime;
        //        //BuildDataTable(false, DateTime.Now);

        //    }
        //    catch (Exception ex)
        //    {
        //        ExceptionManager.Publish(ex);
        //        LogManager.WriteLog("chkCustom_Checked", LogManager.enumLogLevel.Debug);

        //    }

        //}

        private void chkWeek_Checked(object sender, RoutedEventArgs e)
        {
            SetVisibiltyToListviews(Visibility.Hidden, Visibility.Visible);
            DateTime dtCurrentWeek;
            DataTable dtCheckWeek = null;
            try
            {
                if (c_cancel != null)
                {
                    c_cancel.Cancel();
                }
                chkDrop.IsEnabled = false;
                chkWeek.IsEnabled = true;
                chkDay.IsEnabled = false;
                chkMonth.IsEnabled = true;
				OuterGrid.Visibility = Visibility.Visible;
                UserControl.Cursor = Cursors.Wait;
                selectedOption = Option.Week;
                this.dgGridView.Visibility = Visibility.Visible;
                UserControl.InnerGrid.Visibility = Visibility.Hidden;
                this.btnPrint.Visibility = Visibility.Visible;
                //this.btnDetails.Margin = new Thickness(750, 0, 0, 300);
                dtCheckWeek = GetWTDMTD(DateTime.Now);
                if (dtCheckWeek.Rows.Count > 0)
                {
                    dtCurrentWeek = DateTime.Parse(dtCheckWeek.Rows[0]["WeekStart"].ToString());
                    CheckWeekDate_ = dtCurrentWeek;
                    if (dtCurrentWeek != null)
                    {
                        StartTask(() =>
                        {
                            BuildDataTable(false, false, dtCurrentWeek, Option.Week);
                        });
                    }
                    else
                    {
                        OuterGrid.Visibility = Visibility.Collapsed;
                        MessageBox.ShowBox("MessageID892", BMC_Icon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                LogManager.WriteLog("chkWeek_Checked", LogManager.enumLogLevel.Debug);
            }
            finally
            {
                UserControl.Cursor = Cursors.Arrow;
            }
        }

        private void btnDetails_Click(object sender, RoutedEventArgs e)
        {
            UserControl.Cursor = Cursors.Wait;
            try
            {
                CancelTask(ClearBinding:false);
                lblStatus.Content = "";
                progressBar1.Visibility = Visibility.Hidden;
                btnDetails.IsEnabled = false;
                POS.Views.CAnalysisDetailsWindow objAnalysisDetails = null;
                if ((bool)chkDay.IsChecked)
                {
                    objAnalysisDetails = new POS.Views.CAnalysisDetailsWindow(this.FindResource("CAanalysisDetails_xaml_txtHeader").ToString(), 1, DateTime.Now, DateTime.Now, 0);
                }
                else if ((bool)chkDrop.IsChecked)
                {
                    objAnalysisDetails = new POS.Views.CAnalysisDetailsWindow(this.FindResource("CAanalysisDetails_xaml_txtHeader").ToString(), 2, DateTime.Now, DateTime.Now, 0);
                }
                else if ((bool)chkWeek.IsChecked)
                {
                    objAnalysisDetails = new POS.Views.CAnalysisDetailsWindow(this.FindResource("CAanalysisDetails_xaml_txtHeader").ToString(), 3, DateTime.Now, DateTime.Now, 0);
                }
                else if ((bool)chkMonth.IsChecked)
                {
                    objAnalysisDetails = new POS.Views.CAnalysisDetailsWindow(this.FindResource("CAanalysisDetails_xaml_txtHeader").ToString(), 4, DateTime.Now, DateTime.Now, 0);
                }
                else
                {
                    int iWeekID = -1;
                    //chkDay.IsChecked = true;
                    // objAnalysisDetails = new POS.Views.CAanalysisDetails(1, DateTime.Now, DateTime.Now, 0);
                    this.Cursor = Cursors.Wait;
                    if (dgGridWeek.SelectedItem != null)
                    {
                        DataRowView dr = (DataRowView)dgGridWeek.SelectedItem;
                        iWeekID = Convert.ToInt32(dr["Week_ID"].ToString());
                        objAnalysisDetails = new POS.Views.CAnalysisDetailsWindow(this.FindResource("CAanalysisDetails_xaml_txtHeader1").ToString(), -99, DateTime.Now, DateTime.Now, iWeekID);
                    }
                    else
                        UserControl.InnerGrid.Visibility = Visibility.Hidden;

                }
                if (objAnalysisDetails != null)
                {
                    objAnalysisDetails.Owner = Window.GetWindow(this);
                    objAnalysisDetails.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            finally
            {
                UserControl.Cursor = Cursors.Arrow;
                btnDetails.IsEnabled = true;
            }
        }

        private void chkWeekInvoice_Checked(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            dgGridWeek.DataContext = new DataTable().DefaultView;
            SetVisibiltyToListviews(Visibility.Visible, Visibility.Hidden);
            UserControl.InnerGrid.Visibility = Visibility.Hidden;
            lblStatus.Visibility = Visibility.Visible;
            progressBar1.Visibility = Visibility.Visible;
            btnDetails.Visibility = Visibility.Visible;
            //btnDetails.Margin = new Thickness(431, 0, 339, 39);
            btnPrint.Visibility = Visibility.Hidden;
            backgroundWorker1.RunWorkerAsync();
        }

        private void GetCollectionData()
        {
            IAnalysis objCashDesk = AnalysisBusinessObject.CreateInstance();
            dtCollection = objCashDesk.GetWeeklyCollectionSummary();
        }

        private void SetVisibiltyToListviews(Visibility WeekViewVisibilty, Visibility MainListviewVisibilty)
        {
            dgGridWeek.Visibility = WeekViewVisibilty;
            dgGridView.Visibility = MainListviewVisibilty;
            lblStatus.Content = "";
            dgGridView.ItemsSource = null;
            btnDetails.Visibility = Visibility.Visible;
            lblStatus.Visibility = Visibility.Visible;
            progressBar1.Visibility = Visibility.Visible;
        }

        //private void dgGridWeek_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        //{
        //    try
        //    {
        //        this.Cursor = Cursors.Wait;
        //        DataRowView dr = (DataRowView)dgGridWeek.SelectedItem;
        //        int iWeekID = Convert.ToInt32(dr[0]);
        //        POS.Views.CAanalysisDetails objAnalysisDetails;
        //        objAnalysisDetails = new POS.Views.CAanalysisDetails(-99, DateTime.Now, DateTime.Now, iWeekID);
        //        objAnalysisDetails.Owner = Window.GetWindow(this);
        //        objAnalysisDetails.ShowDialog();

        //    }
        //    catch (Exception ex)
        //    {
        //        ExceptionManager.Publish(ex);
        //    }
        //    finally
        //    {
        //        this.Cursor = Cursors.Arrow;
        //    }
        //}
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            //System.Threading.Thread.Sleep(5000);
            GetCollectionData();
        }

        private void CancelTask(bool ClearBinding = true)
        {
            if (c_cancel != null)
            {
                c_cancel.Cancel();
            }
            chkDrop.IsEnabled = true;
            chkWeek.IsEnabled = true;
            chkDay.IsEnabled = true;
            chkMonth.IsEnabled = true;

            lblStatus.Visibility = Visibility.Visible;
            lblStatus.Content = "Cancelling... ";
            if (t_task != null)
            {
                t_task.Wait();
            }
            t_task = null;
            if (ClearBinding)
            {
                dgGridView.ItemsSource = null;
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                // First, handle the case where an exception was thrown.
                if (e.Error != null)
                {
                    MessageBox.ShowBox(e.Error.Message, BMC_Icon.Error);
                }
                else if (e.Cancelled)
                {
                    MessageBox.ShowBox("MessageID153", BMC_Icon.Error);
                }
                else
                {
                    dgGridWeek.DataContext = dtCollection.DefaultView;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            finally
            {
                lblStatus.Visibility = Visibility.Hidden;
                progressBar1.Visibility = Visibility.Hidden;
                this.Cursor = Cursors.Arrow;
            }
        }

        private void btncancel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CancelTask();

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
                        if (c_cancel != null)
                        {
                            c_cancel.Cancel();
                        }
                        this.UserControl.Loaded -= (this.UserControl_Loaded);
                        this.chkDrop.Checked -= (this.chkDrop_Checked);
                        this.chkDay.Checked -= (this.chkDay_Checked);
                        this.chkWeek.Checked -= (this.chkWeek_Checked);
                        this.chkMonth.Checked -= (this.chkMonth_Checked);
                        this.chkWeekInvoice.Checked -= (this.chkWeekInvoice_Checked);
                        this.btnPrint.Click -= (this.btnPrint_Click);
                        this.btnDetails.Click -= (this.btnDetails_Click);
                    },
                    (c) =>
                    {
                    });
                    LogManager.WriteLog("|=> CAnalysis objects are released successfully.", LogManager.enumLogLevel.Info);
                }
                disposed = true;
            }
        }

        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="CAnalysis"/> is reclaimed by garbage collection.
        /// </summary>
        ~CAnalysis()
        {
            Dispose(false);
        }

        #endregion
    }
}