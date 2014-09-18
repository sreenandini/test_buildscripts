//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Windows;
//using System.Windows.Controls;
//using System.Windows.Data;
//using System.Windows.Documents;
//using System.Windows.Input;
//using System.Windows.Media;
//using System.Windows.Media.Imaging;
//using System.Windows.Navigation;
//using System.Windows.Shapes;
//using BMC.Business.CashDeskOperator;
//using System.Data;
//using BMC.Transport;
//using System.Data.Linq;
//using BMC.Transport.CashDeskOperatorEntity;
//using BMC.Common.ExceptionManagement;
//using BMC.Presentation.POS;
//using BMC.CashDeskOperator.BusinessObjects;
//using BMC.CashDeskOperator;

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
    
    public partial class TISPromoDetails : Window
    {
        BMC.Business.CashDeskOperator.Promotional objPromotioal = new BMC.Business.CashDeskOperator.Promotional();
        Promotional objprom = new Promotional();
        private bool _IsTISDataFound = false;
        public int RowSelectedTicketId;
        private int iPageNum = 1;
        private int iPageSize = 25;
        private int iLastPage = 1;
        DataTable dtTISDetails=new DataTable();
        DataTable dtTISDetailByPage;
        private bool bRefresh = false;
        private DateTime _StartDate;
        private DateTime _EndDate;
        int NoOfRecordsInPage;
        public bool IsTISDataFound
        {
            get
            {
                return _IsTISDataFound;
            }
            set
            {
                _IsTISDataFound = value;
            }

        }

        public DateTime StartDate
        {
            get
            {
                return _StartDate;
            }
            set
            {
                _StartDate = value;
            }
        }
        public DateTime EndDate
        {
            get
            {
                return _EndDate;
            }
            set
            {
                _EndDate = value;
            }
        }

        public TISPromoDetails(DateTime dtStart, DateTime dtEnd)
        {
            try
            {
                InitializeComponent();
                this.StartDate = dtStart;
                this.EndDate = dtEnd;
                NoOfRecordsInPage = Convert.ToInt32(Common.ConfigurationManagement.ConfigManager.Read("DisplayTISRecords"));
                //ISingleResult<TISPromotionalClassDetails> lstTISDetails = null;
              // var lstTISDetails = objprom.BGetTISPromoDetails(dtStart,dtEnd).ToList();

               // if (lstTISDetails.Count() <= 0)
               // {
               //     MessageBox.ShowBox("MessageID261", BMC_Icon.Warning);//Please Enter the Valid Expiry Date
               //     _IsTISDataFound = false;
               //     return;
               // }
               // lstTISPromoDetailsGridView.DataContext = lstTISDetails;
                //as ISingleResult<TISPromotionalClassDetails>;    
                FillTISData(dtStart,dtEnd);
                PopulateTISListView();
             
               
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
        void FillTISData(DateTime dtStart, DateTime dtEnd)
        {
            txtPageNo.Text = "";
           
         //   var lstTISDetails = objprom.BGetTISPromoDetails(dtStart, dtEnd).ToList();
            dtTISDetails=objprom.BGetTISPromoDetailsDT(dtStart, dtEnd,NoOfRecordsInPage);
            if (dtTISDetails.Rows.Count <= 0)
            {
                MessageBox.ShowBox("MessageID261", BMC_Icon.Warning);//Please Enter the Valid Expiry Date
                _IsTISDataFound = false;
                
                return;
            }

            else if(dtTISDetails.Rows.Count>0)
            {

                iLastPage = Convert.ToInt32(dtTISDetails.Rows[dtTISDetails.Rows.Count - 1]["SrNo"]);
                if (iPageNum > iLastPage)
                    iPageNum = iLastPage;
                PopulateTISListView();
                _IsTISDataFound = true;
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

        private void btnExit_click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnFirst_Click(object sender, RoutedEventArgs e)
        {
            if (iPageNum == 1)
                return;
            else
            {
                iPageNum = 1;
                PopulateTISListView();
            }
        }

        void PopulateTISListView()
        {

            if (dtTISDetailByPage == null)
                dtTISDetailByPage = new DataTable();

            DataView dview = dtTISDetails.DefaultView;
            dview.RowFilter = "SrNo = " + iPageNum;
            dtTISDetailByPage = dview.ToTable();

            if (dtTISDetailByPage.Rows.Count > 0)
            {
                dgTISPromoDetailsGridView.DataContext = dtTISDetailByPage.DefaultView;
                this.DataContext = dgTISPromoDetailsGridView.DataContext = dtTISDetailByPage.DefaultView;

                if(dgTISPromoDetailsGridView.SelectedItem != null)
                    dgTISPromoDetailsGridView.ScrollIntoView(dgTISPromoDetailsGridView.SelectedItem);

                txtPageNo.Text = iPageNum.ToString() + "/" + iLastPage.ToString();
                _IsTISDataFound = true;
            }
            else
            {
                if (bRefresh)
                {
                   // MessageBox.ShowBox("MessageID37", BMC_Icon.Information);
                    bRefresh = false;
                }
            }
        }

        private void btnPrevious_Click(object sender, RoutedEventArgs e)
        {
            if (iPageNum == 1)
                return;

            iPageNum = iPageNum - 1;
            PopulateTISListView();
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
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
                PopulateTISListView();
            }

        }

        private void btnLast_Click(object sender, RoutedEventArgs e)
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
                PopulateTISListView();
            }
        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                IReports objReports = ReportsBusinessObject.CreateInstance();
                using (CReportViewer objReportViewer = new CReportViewer())
                {

                    DataSet dtTISPromotionalDetails= objReports.GetTISPromotionalDetails(this.StartDate,this.EndDate,NoOfRecordsInPage);
                    if (dtTISPromotionalDetails != null)
                    {
                        objReportViewer.PrintTISDetailsReport(dtTISPromotionalDetails, this.StartDate, this.EndDate, NoOfRecordsInPage);
                        objReportViewer.SetOwner(this);

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

        //void lstTISPromoDetailsGridView_GridViewColumnHeaderClickedHandler(object sender, RoutedEventArgs e)
        //{
        //    GridViewColumnHeader headerClicked = e.OriginalSource as GridViewColumnHeader;
        //    dgTISPromoDetailsGridView.Sort(headerClicked);
        //}
     
    }
}
