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
using BMC.CashDeskOperator.BusinessObjects;
using BMC.CashDeskOperator;
using BMC.Common.ExceptionManagement;
using BMC.Common.Utilities;
using BMC.Presentation.POS.Views;
using BMC.Presentation.POS;
using BMC.Presentation.POS.Helper_classes;
namespace BMC.Presentation.POS.Views
{
    /// <summary>
    /// Interaction logic for PromoHistory.xaml
    /// </summary>
    public partial class PromoHistory : UserControl
    {
        public int rowSelectedID;
        public int rowID;
        public string promoName;
        Promotional objprom = new Promotional();
        private bool _IsPromoDetailsFound = false;
        PromoHistDetails promHistDetails;
        private int iPageNum = 1;
        private int iPageSize = 25;
        private int iLastPage = 1;
        DataTable dtPromoHisrotyDetails = new DataTable();
        DataTable dtPromoHistoryByPage;
        private bool bRefresh = false;
        int NoOfRecordsInPage;
        public PromoHistory()
        {
            InitializeComponent();
            NoOfRecordsInPage = Convert.ToInt32(Common.ConfigurationManagement.ConfigManager.Read("DisplayPromoHistory"));
          
            FillPromoHistoryDetails();
            PopulatePromoHistoryListView();

        }
        public bool IsPromoDetailsFound
        {
            get
            {
                return _IsPromoDetailsFound;
            }
            set
            {
                _IsPromoDetailsFound = value;
            }

        }
        private void PopulatePromoHistoryListView()
        {
            try
            {
                if (dtPromoHistoryByPage == null)
                    dtPromoHistoryByPage = new DataTable();

                DataView dview = dtPromoHisrotyDetails.DefaultView;
                dview.RowFilter = "SrNo1 = " + iPageNum;
                dtPromoHistoryByPage = dview.ToTable();

                if (dtPromoHistoryByPage.Rows.Count > 0)
                {
                    dgPromoHistGridView.DataContext = dtPromoHistoryByPage.DefaultView;
                    this.DataContext = dgPromoHistGridView.DataContext = dtPromoHistoryByPage.DefaultView;

                    if(dgPromoHistGridView.SelectedItem != null)
                        dgPromoHistGridView.ScrollIntoView(dgPromoHistGridView.SelectedItem);
                    
                    txtPageNo.Text = iPageNum.ToString() + "/" + iLastPage.ToString();
                    _IsPromoDetailsFound = true;
                }
                else
                {
                    if (bRefresh)
                    {
                      
                        bRefresh = false;
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void FillPromoHistoryDetails()
        {
            try
            {
                txtPageNo.Text = "";
                int a = 0;
                dtPromoHisrotyDetails = objprom.BGetPromoHistoryDetailsDT(a, NoOfRecordsInPage);

                if (dtPromoHisrotyDetails.Rows.Count > 0)
                {

                    iLastPage = Convert.ToInt32(dtPromoHisrotyDetails.Rows[dtPromoHisrotyDetails.Rows.Count - 1]["SrNo1"]);
                    if (iPageNum > iLastPage)
                        iPageNum = iLastPage;
                    PopulatePromoHistoryListView();
                    _IsPromoDetailsFound = true;
                }
                else
                {
                    if (bRefresh)
                    {
                        MessageBox.ShowBox("MessageID37", BMC_Icon.Information);
                        bRefresh = false;
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
      
        private void UserCtrlPromoHist_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {

                int a = 0;
                //ISingleResult<PromotionalClass> lstPromotionalHistory = objprom.BGetPromoHistory(a, NoOfRecordsInPage);
                //lstPromoHistGridView.ItemsSource = lstPromotionalHistory as ISingleResult<PromotionalClass>;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
           // if (lstPromotionalHistory == null) return;
        }

   

        private void btnDetails_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int TicketId=0;
                int index = 0;
                int PageNo = 1;
                if (!string.IsNullOrEmpty(iPageNum.ToString()))
                {
                    PageNo = iPageNum;
                }
                if (dgPromoHistGridView.SelectedIndex >= 0)
                {
                    if (PageNo>1)
                    {
                        index = dgPromoHistGridView.SelectedIndex + (NoOfRecordsInPage * (PageNo - 1));
                    }
                    else
                    {
                        index = dgPromoHistGridView.SelectedIndex;
                    }


                    TicketId = Convert.ToInt32((((System.Data.DataRowView)(dgPromoHistGridView.SelectedItem)).Row)["PromotionalID"]);
                    promoName = Convert.ToString((((System.Data.DataRowView)(dgPromoHistGridView.SelectedItem)).Row)["PromotionalName"]);
                    promHistDetails = new PromoHistDetails(TicketId, promoName);

                    promHistDetails.ShowDialogEx(this);
                }
                else
                {
                    MessageBox.ShowBox("MessageID883", BMC_Icon.Information);
                }
             //   int TicketId = (lstPromoHistGridView.SelectedItem as PromotionalClass).PromotionalID ;
                
                
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                IReports objReports = ReportsBusinessObject.CreateInstance();
                using (CReportViewer objReportViewer = new CReportViewer())
                {

                    DataSet dtPromotionalHistory = objReports.GetPromotionalTicketHistory();
                    if (dtPromotionalHistory != null)
                    {
                        objReportViewer.PrintPromotionalHistoryReport(dtPromotionalHistory);
                        objReportViewer.SetOwner(Window.GetWindow(this));
                        objReportViewer.ShowDialog();
                    }
                    else
                    {
                        MessageBox.ShowBox("MessageID550", BMC_Icon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        //void lstPromoHistGridView_GridViewColumnHeaderClickedHandler(object sender, RoutedEventArgs e)
        //{
        //    GridViewColumnHeader headerClicked = e.OriginalSource as GridViewColumnHeader;
        //    dgPromoHistGridView.Sort(headerClicked);             
        //}

        private void dgPromoHistGridView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnFirstClick_Click(object sender, RoutedEventArgs e)
        {
            if (iPageNum == 1)
                return;
            else
            {
                iPageNum = 1;
                PopulatePromoHistoryListView();
            }
        }

        private void btnPreviousClick_Click(object sender, RoutedEventArgs e)
        {
            if (iPageNum == 1)
                return;

            iPageNum = iPageNum - 1;
            PopulatePromoHistoryListView();
        }

        private void btnNextClick_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtPageNo.Text))
                return;

            string[] sPageSplit = txtPageNo.Text.Split(new char[] { '/' });
            int nLastPage = Convert.ToInt32(sPageSplit[1]);

            if (iPageNum == nLastPage)
                return;
            else
            {
                iPageNum++;
                PopulatePromoHistoryListView();
            }


        }

        private void btnLastClick_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtPageNo.Text))
                return;

            string[] sPageSplit = txtPageNo.Text.Split(new char[] { '/' });
            int nLastPage = Convert.ToInt32(sPageSplit[1]);

            if (iPageNum == nLastPage)
                return;
            else
            {
                iPageNum = nLastPage;
                PopulatePromoHistoryListView();
            }
        }
    }
}
