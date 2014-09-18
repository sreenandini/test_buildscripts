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
using BMC.CashDeskOperator;
using System.Data;
using BMC.Common.ExceptionManagement;
using BMC.Transport;
using BMC.Common.LogManagement;

namespace BMC.Presentation.POS.Views
{
    /// <summary>
    /// Interaction logic for CDeclareExceptionTickets.xaml
    /// </summary>
    public partial class CDeclareExceptionTickets : Window
    {
        private Declaration oDeclaration = null ;
        private List<ExceptionVouchers> oVoucherList = null;
        private int CollectionBatchNo { get; set; }
        private int CollectionNo { get; set; }
        private string BarPositionName { get; set; }
        private List<string> _voucherListActual = null;
        private bool _isManualProcess = false;

        private CDeclareExceptionTickets()
        {
            InitializeComponent();
            LogManager.WriteLog("CDeclareExceptionTickets:CDeclareExceptionTickets() Entry", LogManager.enumLogLevel.Info);            
        }

        public CDeclareExceptionTickets(int iCollectionBatchNo)
            : this()
        {
            this.CollectionBatchNo = iCollectionBatchNo;
            oDeclaration=new Declaration();
            FillData();
            if (!String.IsNullOrEmpty(ManualCashEntry.sSiteCode))
            {
                this.tbHeader.Text += "\t      " + Application.Current.FindResource("ManualCashEntry_xaml_lblSiteCode").ToString().Trim() + " " + ManualCashEntry.sSiteCode;
            }
            LogManager.WriteLog("CDeclareExceptionTickets:CDeclareExceptionTickets() Exit", LogManager.enumLogLevel.Info);

        }
        public CDeclareExceptionTickets(int iCollectionBatchNo, string ExchangeConn, string TicketingConn)
            : this()
        {
            LogManager.WriteLog("CDeclareExceptionTickets:CDeclareExceptionTickets() Site Code : " + ManualCashEntry.sSiteCode, LogManager.enumLogLevel.Debug);
            this.CollectionBatchNo = iCollectionBatchNo;
            oDeclaration = new Declaration(ExchangeConn, TicketingConn);
            FillData();
            if (!String.IsNullOrEmpty(ManualCashEntry.sSiteCode))
            {
                this.tbHeader.Text += "\t      " + Application.Current.FindResource("ManualCashEntry_xaml_lblSiteCode").ToString().Trim() + " " + ManualCashEntry.sSiteCode;
            }
            LogManager.WriteLog("CDeclareExceptionTickets:CDeclareExceptionTickets() Exit", LogManager.enumLogLevel.Info);
        }


        public CDeclareExceptionTickets(string barPositionName, int iCollectionNo, string sTickets)
            : this()
        {
            oDeclaration = new Declaration();
            _isManualProcess = true;
            this.CollectionNo = iCollectionNo;
            this.BarPositionName = barPositionName;
            if (!string.IsNullOrEmpty(sTickets))
            {
                _voucherListActual = sTickets.Split(new char[] { ',' }).ToList();
            }
            this.ShowManualProcessItems();
            if (!String.IsNullOrEmpty(ManualCashEntry.sSiteCode))
            {
                this.tbHeader.Text += "\t      " + Application.Current.FindResource("ManualCashEntry_xaml_lblSiteCode").ToString().Trim() + " " + ManualCashEntry.sSiteCode;
            }
        }
        public CDeclareExceptionTickets(string barPositionName, int iCollectionNo, string sTickets, string ExchangeConn, string TicketingConn)
            : this()
        {
            oDeclaration = new Declaration(ExchangeConn, TicketingConn);
            _isManualProcess = true;
            this.CollectionNo = iCollectionNo;
            this.BarPositionName = barPositionName;
            if (!string.IsNullOrEmpty(sTickets))
            {
                _voucherListActual = sTickets.Split(new char[] { ',' }).ToList();
            }
            this.ShowManualProcessItems();
            if (!String.IsNullOrEmpty(ManualCashEntry.sSiteCode))
            {
                this.tbHeader.Text += "\t      " + Application.Current.FindResource("ManualCashEntry_xaml_lblSiteCode").ToString().Trim() + " " + ManualCashEntry.sSiteCode;
            }
        }

        private void ShowManualProcessItems()
        {
            oVoucherList = oDeclaration.GetExceptionTicketsByPos(this.BarPositionName, this.CollectionNo);

            try
            {
                if (_voucherListActual != null &&
                    oVoucherList != null)
                {
                    oVoucherList = (from v in oVoucherList
                                    join a in _voucherListActual
                                    on v.strBarcode equals a
                                    select v).ToList();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

            if (oVoucherList == null) oVoucherList = new List<ExceptionVouchers>();
            Binding bind = new Binding();
            bind.Source = oVoucherList;
            lstVouchers.SetBinding(ListView.ItemsSourceProperty, bind);
            this.ShowVoucherScreen();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();

        }
        private void FillData()
        {

            try
            {
                this.CloseVoucherScreen();
                DataSet oTicketVarList = oDeclaration.GetTicketVarianceByPos(this.CollectionBatchNo);
                Binding bind = new Binding();
                bind.Source = oTicketVarList.Tables[0];
                lstPos.SetBinding(ListView.ItemsSourceProperty, bind);

            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }


        }

        private void lstPos_MouseUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (lstPos.SelectedIndex > -1)
                {
                    oVoucherList = oDeclaration.GetExceptionTicketsByPos((((System.Data.DataRowView)(lstPos.SelectedItem))).Row.ItemArray[1].ToString(),
                        int.Parse((((System.Data.DataRowView)(lstPos.SelectedItem))).Row.ItemArray[6].ToString()));
                    Binding bind = new Binding();
                    bind.Source = oVoucherList;
                    lstVouchers.SetBinding(ListView.ItemsSourceProperty, bind);
                    this.ShowVoucherScreen();
                }
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);

            }
        }

        private void lstPos_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // CR# 115943 (sub item 2) : Code was moved to lstPos_MouseUp (By A.Vinod Kumar 05/11/2011 16:10:29)
        }

        private void btnProcess_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                foreach (ExceptionVouchers Voucher in oVoucherList)
                {
                    if (Voucher.IsChecked)
                    {
                        if (!_isManualProcess)
                        {
                            oDeclaration.DeclareExceptionTicket(Voucher.Collection_No, Voucher.strBarcode, Voucher.Installation_no, BMC.Security.SecurityHelper.CurrentUser.SecurityUserID, Voucher.iAmount);
                        }
                        else
                        {
                            oDeclaration.DeclareExceptionTicketAsPaid(Voucher.Collection_No, Voucher.strBarcode, Voucher.Installation_no, BMC.Security.SecurityHelper.CurrentUser.SecurityUserID, Voucher.iAmount);
                        }
                    }
                }
                if (!_isManualProcess)
                {
                    this.FillData();
                    lstVouchers.Visibility = Visibility.Collapsed;
                }
                else
                {
                    this.ShowManualProcessItems();
                }
                this.IsProcessClickedOnce = true;
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);

            }
        }

        private void btnVoucherClose_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (lstVouchers.Visibility == Visibility.Visible)
                {
                    this.CloseVoucherScreen();
                    if (_isManualProcess)
                    {
                        this.Close();
                    }
                }
                else
                {
                    if (lstPos.Items.Count > 0)
                    {
                        if (MessageBox.ShowBox("MessageID386", BMC_Icon.Question, BMC_Button.YesNo, "") == System.Windows.Forms.DialogResult.Yes)
                        {
                            this.Close();
                        }
                    }
                    else
                    {
                        this.Close();
                    }

                }
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);

            }
        }
        void CloseVoucherScreen()
        {
            chk_CheckAll.Visibility = Visibility.Collapsed;
            btnExit.Visibility = Visibility.Collapsed;
            btnProcess.Visibility = Visibility.Collapsed;
            //btnVoucherClose.Visibility = Visibility.Visible;
            lstVouchers.Visibility = Visibility.Collapsed;
            btnVoucherClose.Content = Application.Current.Resources["CDeclareExceptionTickets_Done"].ToString();
        }
        void ShowVoucherScreen()
        {
            chk_CheckAll.Visibility = Visibility.Visible;
            chk_CheckAll.IsChecked = false;
            btnExit.Visibility = Visibility.Collapsed;
            btnProcess.Visibility = ((oVoucherList != null && oVoucherList.Count > 0) ? Visibility.Visible : Visibility.Hidden);
            // btnVoucherClose.Visibility = Visibility.Visible;
            lstVouchers.Visibility = Visibility.Visible;
            btnVoucherClose.Content = Application.Current.Resources["CDeclareExceptionTickets_Close"].ToString();
        }

        public bool IsProcessClickedOnce { get; private set; }


        private void chk_CheckAll_Checked(object sender, RoutedEventArgs e)
        {
            foreach (ExceptionVouchers Voucher in oVoucherList)
            {
                Voucher.IsChecked = chk_CheckAll.IsChecked.Value;
            }
        }

        private void chk_CheckAll_Unchecked(object sender, RoutedEventArgs e)
        {
            chk_CheckAll_Checked(sender, e);
        }
    }
}
