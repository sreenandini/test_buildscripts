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
using BMC.Presentation;
using BMC.Common.ExceptionManagement;
using BMC.Business.CashDeskOperator;
using BMC.CashDeskOperator;
using BMC.CashDeskOperator.BusinessObjects;
using BMC.Transport;
using BMC.Common.LogManagement;
using System.Data;
using BMC.Presentation.POS.Views;
using BMC.Presentation.POS.Helper_classes;

namespace BMC.Presentation.POS
{
    /// <summary>
    /// Interaction logic for CollectionBatchReports.xaml
    /// </summary>
    public partial class CollectionBatchReports : Window
    {
        private int _BatchID ;
        private Window _Parent;
        IReports objReports;
        string _ExchangeConnectionString;
        string _TicketingConnectionString;
        string _SiteName;

        bool IsCommonCDO = false;
        public CollectionBatchReports(int BatchId, Window window)
        {
            InitializeComponent();
            this._BatchID = BatchId;
            _Parent = window;
            IsCommonCDO = false;
            objReports = ReportsBusinessObject.CreateInstance();
            

        }

        public CollectionBatchReports(int BatchId, Window window, string ExchangeConnectionString, string TicketingConnectionString,string SiteName )
        {
            InitializeComponent();
            this._BatchID = BatchId;
            _Parent = window;
            _ExchangeConnectionString = ExchangeConnectionString;
            _TicketingConnectionString=TicketingConnectionString;
            _SiteName = SiteName;
            IsCommonCDO = true;
            objReports = ReportsBusinessObject.CreateInstance(ExchangeConnectionString, TicketingConnectionString);
        }

       
        private void btnGenerate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                switch (cboReports.SelectedValue.ToString())
                {
                    case "SGVI Liquidation Report":
                        CreateLiquidationReport();
                        break;
                    case "Exception Summary Report":
                        CreateExceptionSummaryReport();
                        break;
                    case "Collection Report":
                        ShowCollectionReport();
                        break;
                    case "Variance Report":
                        CreateVarianceReport();
                        break;
                    default:
                        ShowBatchWinLossReport();
                        break;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private List<CollectionReports> GetCollectionReports()
        {
            try
            {
                var oDataContext = new InstallationDataContext(oCommonUtilities.CreateInstance().GetConnectionString());
                List<CollectionReports> _collectionReports = new List<CollectionReports>();
                if (Settings.Client.ToUpper() == "SGVI" && Settings.SGVI_Enabled == true)
                {
                    foreach (var machine in oDataContext.GetCollectionReports())
                    {
                        _collectionReports.Add(machine);
                    }
                }
                else
                {
                    _collectionReports.Add(new CollectionReports { Description = "Exception Summary Report", ID = "1" });
                }
                return _collectionReports;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return new List<CollectionReports>();
            }
        }
        
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cboReports.ItemsSource = GetCollectionReports();

            cboReports.ItemsSource = new System.Windows.Forms.BindingSource(GetCollectionReports(), null);
            cboReports.DisplayMemberPath = "Description";
            cboReports.SelectedValuePath = "Description";
            cboReports.SelectedIndex = 0;
        }

        private void CreateLiquidationReport()
        {
            try
            {
                //IReports objReports = ReportsBusinessObject.CreateInstance();

                LogManager.WriteLog("Fetching report data from database...", LogManager.enumLogLevel.Info);

                DataSet LiquidationDetails = objReports.GetLiquidationDetails(_BatchID);

                DataSet LiquidationSummaryDetails = objReports.GetLiquidationSummaryDetails(_BatchID);

                if (LiquidationDetails.Tables[0].Rows.Count == 0 || LiquidationSummaryDetails.Tables[0].Rows.Count == 0)
                {
                    LogManager.WriteLog("No Records found for the selected criteria - Return", LogManager.enumLogLevel.Info);

                    MessageBox.ShowBox("MessageID261", BMC_Icon.Information);
                    return;
                }

                using (CReportViewer cReportViewer = new CReportViewer())
                {
                    LogManager.WriteLog("Report data fetched successfully from database", LogManager.enumLogLevel.Info);

                    cReportViewer.ShowLiquidationReport(LiquidationDetails, LiquidationSummaryDetails, _BatchID);
                    cReportViewer.SetOwner(System.Windows.Window.GetWindow(this));

                    cReportViewer.Show();
                }


                LogManager.WriteLog("ShowLiquidationReport Successfull", LogManager.enumLogLevel.Info);
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Report" + ex.Message, LogManager.enumLogLevel.Info);
            }
        }
        //
        private void CreateExceptionSummaryReport()
        {
            try
            {
                //IReports objReports = ReportsBusinessObject.CreateInstance();

                LogManager.WriteLog("Fetching report data from database...", LogManager.enumLogLevel.Info);

                DataSet ExceptionDetails = objReports.GetExceptionSummary(_BatchID);

                if (ExceptionDetails.Tables[0].Rows.Count == 0)
                {
                    LogManager.WriteLog("No Records found for the selected criteria - Return", LogManager.enumLogLevel.Info);

                    MessageBox.ShowBox("MessageID261", BMC_Icon.Information);
                    return;
                }

                if(IsCommonCDO)
                {
                    using (CReportViewer cReportViewer = new CReportViewer(_ExchangeConnectionString, _TicketingConnectionString))
                    {
                        LogManager.WriteLog("Report data fetched successfully from database", LogManager.enumLogLevel.Info);
                        if(Settings.Region.ToUpper()=="UK")
                          cReportViewer.ShowExceptionSummaryReportUK(_BatchID, _ExchangeConnectionString, _TicketingConnectionString);
                        else 
                            cReportViewer.ShowExceptionSummaryReport(_BatchID,_ExchangeConnectionString,_TicketingConnectionString);
                        
                        cReportViewer.SetOwner(System.Windows.Window.GetWindow(this));
                        cReportViewer.Show();
                    }
                }
                else
                {
                    using (CReportViewer cReportViewer = new CReportViewer())
                    {
                        LogManager.WriteLog("Report data fetched successfully from database", LogManager.enumLogLevel.Info);
                        if (Settings.Region.ToUpper() == "UK")
                            cReportViewer.ShowExceptionSummaryReportUK(_BatchID);
                        else
                            cReportViewer.ShowExceptionSummaryReport(_BatchID);
                        cReportViewer.SetOwner(System.Windows.Window.GetWindow(this));

                        cReportViewer.Show();
                    }
                }


                LogManager.WriteLog("ShowExceptionSummaryReport Successfull", LogManager.enumLogLevel.Info);
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Report" + ex.Message, LogManager.enumLogLevel.Info);
            }
        }

        private void CreateVarianceReport()
        {
            try
            {
                //IReports objReports = ReportsBusinessObject.CreateInstance();

                LogManager.WriteLog("Fetching report data from database...", LogManager.enumLogLevel.Info);

                DataSet ExceptionDetails = objReports.GetExceptionSummary(_BatchID);

                if (ExceptionDetails.Tables[0].Rows.Count == 0)
                {
                    LogManager.WriteLog("No Records found for the selected criteria - Return", LogManager.enumLogLevel.Info);

                    MessageBox.ShowBox("MessageID261", BMC_Icon.Information);
                    return;
                }

                if(IsCommonCDO)
                {
                    using (CReportViewer cReportViewer = new CReportViewer(_ExchangeConnectionString, _TicketingConnectionString))
                    {
                        LogManager.WriteLog("Report data fetched successfully from database", LogManager.enumLogLevel.Info);
                        cReportViewer.ShowVarianceReport(_BatchID, _ExchangeConnectionString, _TicketingConnectionString);
                        cReportViewer.SetOwner(System.Windows.Window.GetWindow(this));

                        cReportViewer.Show();
                    }
                }
                else
                {
                    using (CReportViewer cReportViewer = new CReportViewer())
                    {
                        LogManager.WriteLog("Report data fetched successfully from database", LogManager.enumLogLevel.Info);
                        cReportViewer.ShowVarianceReport(_BatchID);
                        cReportViewer.SetOwner(System.Windows.Window.GetWindow(this));

                        cReportViewer.Show();
                    }
                }


                LogManager.WriteLog("ShowExceptionSummaryReport Successfull", LogManager.enumLogLevel.Info);
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Report" + ex.Message, LogManager.enumLogLevel.Info);
            }
        }

        private void ShowBatchWinLossReport()
        {
            try
            {
                LogManager.WriteLog("ShowBatchWinLossReport  Start", LogManager.enumLogLevel.Info);
                
                if (IsCommonCDO)
                {
                    using (CReportViewer cReportViewer = new CReportViewer(_ExchangeConnectionString, _TicketingConnectionString))
                    {
                        LogManager.WriteLog("Report data fetched successfully from database", LogManager.enumLogLevel.Info);

                        if (_BatchID > 0)
                        {
                            if(Settings.Region.ToUpper() == "UK")
                                cReportViewer.ShowBatchWinLossReportForUK(_BatchID, 0);
                            else
                            cReportViewer.ShowBatchWinLossReport(_BatchID, 0);
                        }
                        cReportViewer.Show();
                    }
                }
                else
                {
                    using (CReportViewer cReportViewer = new CReportViewer())
                    {
                        LogManager.WriteLog("Report data fetched successfully from database", LogManager.enumLogLevel.Info);
                        if (_BatchID > 0)
                        {
                            if (Settings.Region.ToUpper() == "UK")
                                cReportViewer.ShowBatchWinLossReportForUK(_BatchID, 0);
                            else
                            cReportViewer.ShowBatchWinLossReport(_BatchID, 0);
                        }
                        cReportViewer.Show();
                    }
                }

                LogManager.WriteLog("ShowBatchWinLossReport  Successfull", LogManager.enumLogLevel.Info);
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
       
        private void ShowCollectionReport()
        {
            try
            {
                LogManager.WriteLog("ShowCollectionReport  Start", LogManager.enumLogLevel.Info);

                if (IsCommonCDO)
                {
                    using (CReportViewer cReportViewer = new CReportViewer(_ExchangeConnectionString, _TicketingConnectionString))
                    {
                        LogManager.WriteLog("Report data fetched successfully from database", LogManager.enumLogLevel.Info);

                        if (_BatchID > 0)
                        {
                            cReportViewer.ShowCollectionReport(_BatchID, 0);
                        }
                        cReportViewer.Show();
                    }
                }
                else
                {
                    using (CReportViewer cReportViewer = new CReportViewer())
                    {
                        LogManager.WriteLog("Report data fetched successfully from database", LogManager.enumLogLevel.Info);
                        if (_BatchID > 0)
                        {
                            cReportViewer.ShowCollectionReport(_BatchID, 0);
                        }
                        cReportViewer.Show();
                    }
                }

                LogManager.WriteLog("ShowCollectionReport  Successfull", LogManager.enumLogLevel.Info);
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void CreateBatchWinLossReport()
        {
            try
            {
                //IReports objReports = ReportsBusinessObject.CreateInstance();

                LogManager.WriteLog("Fetching report data from database...", LogManager.enumLogLevel.Info);





                using (CReportViewer cReportViewer = new CReportViewer())
                {
                    LogManager.WriteLog("Report data fetched successfully from database", LogManager.enumLogLevel.Info);

                    cReportViewer.ShowExceptionSummaryReport(_BatchID);
                    cReportViewer.SetOwner(System.Windows.Window.GetWindow(this));

                    cReportViewer.Show();
                }


                LogManager.WriteLog("ShowExceptionSummaryReport Successfull", LogManager.enumLogLevel.Info);
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Report" + ex.Message, LogManager.enumLogLevel.Info);
            }
        }
    }
}
