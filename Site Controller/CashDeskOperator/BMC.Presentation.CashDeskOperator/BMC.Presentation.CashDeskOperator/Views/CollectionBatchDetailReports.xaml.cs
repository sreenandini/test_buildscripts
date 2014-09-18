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
    public partial class CollectionBatchDetailReports : Window
    {
        #region Member Variables
        private int _BatchID;
        private int _WeekNumber;
        private SiteConfig _SiteConfig;
        private Window _Parent;
        private bool isCommonCDOforDeclaration;
        string ExchangeConst = "";
        string TicketConst = "";
        private bool _ChkWeek;
        List<CollectionReports> _collectionReports = new List<CollectionReports>();
        private bool _Undeclared;
        #endregion

        #region Events
        private void btnGenerate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int selectedreport = Convert.ToInt32(cboReports.SelectedValue);
                switch (selectedreport)
                {
                    case 1:
                        CreateLiquidationReport();
                        break;
                    case 2:
                        if (Settings.SGVI_Enabled == true)
                        {
                            CreateMachineDropReport();
                        }
                        else
                        {
                            CreateBatchWinLossReport();
                        }
                        break;
                    case 3:
                        CreateWeeklyWinLossReport(_WeekNumber);
                        break;
                    case 4:
                        CreateWeeklyExceptionSummaryReport(_WeekNumber);
                        break;
                    case 5:
                        CreateExceptionSummaryReport();
                        break;
					case 6:
                            CreateCollectionReport();
                            break;
                     case 7:
                            CreateVarianceReport();
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
        #endregion

        #region Private Methods
        private List<CollectionReports> GetCollectionReports()
        {

            try
            {
                var oDataContext = new InstallationDataContext(oCommonUtilities.CreateInstance().GetConnectionString());
                foreach (var ReportsInfo in oDataContext.GetCollectionDetailReports())
                {
                    _collectionReports.Add(ReportsInfo);
                }

                if (_ChkWeek == true)
                {
                    cboReports.Items.Add(_collectionReports[2]);//Adding Weekly Win Loss Report.
                    cboReports.Items.Add(_collectionReports[3]);//Weekly Exception Summary Report.
                    cboReports.SelectedIndex = 0;

                }
                else
                {
                    if ((Settings.SGVI_Enabled) ||((Settings.LiquidationType.ToUpper().Equals("COLLECTION") || Settings.LiquidationType.ToUpper().Equals("READ")) && Settings.LiquidationProfitShare))
                        cboReports.Items.Add(_collectionReports[0]);//Adding Liquidation Summary Report.

					if(Settings.ShowCollectionReport)
                        cboReports.Items.Add(_collectionReports[5]);
					
					if(Settings.ShowBatchWinLossOnDeclaration)
                    	cboReports.Items.Add(_collectionReports[1]); //Adding Machine Drop Report. 

					if (Settings.ShowVarianceReport)
                        cboReports.Items.Add(_collectionReports[6]);

                    if (!_Undeclared) //If Declaration is completed Adding Exception Summary Report.
                     cboReports.Items.Add(_collectionReports[4]);   
                    cboReports.SelectedIndex = 0;
                }
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

            return _collectionReports;
        }

        private void CreateCollectionReport()
        {
            try
            {
                //IReports objReports = isCommonCDOforDeclaration ? ReportsBusinessObject.CreateInstance(ExchangeConst, TicketConst) : ReportsBusinessObject.CreateInstance();
                IReports objReports = ReportsBusinessObject.CreateInstance();
                DataSet BatchWinLossDetails = null;
                if (_BatchID > 0)
                {
                    BatchWinLossDetails = objReports.GetBatchWinLoss(_BatchID, 0);
                }
                else if (_WeekNumber > 0)
                {
                    BatchWinLossDetails = objReports.GetBatchWinLoss(_WeekNumber, 1);
                }

                //DataSet BatchWinLossDetails = objReports.GetBatchWinLoss(_BatchID, _WeekNumber);

                if (BatchWinLossDetails.Tables[0].Rows.Count == 0 || BatchWinLossDetails.Tables[0].Rows.Count == 0)
                {
                    LogManager.WriteLog("No Records found for the selected criteria - Return", LogManager.enumLogLevel.Info);

                    MessageBox.ShowBox("MessageID261", BMC_Icon.Information);
                    return;
                }
                using (CReportViewer cReportViewer = isCommonCDOforDeclaration ? new CReportViewer(ExchangeConst, TicketConst) : new CReportViewer())
                {
                    LogManager.WriteLog("Report data fetched successfully from database", LogManager.enumLogLevel.Info);

                    if (_BatchID > 0)
                    {
                        cReportViewer.ShowCollectionReport(_BatchID, 0);
                    }
                    else if (_WeekNumber > 0)
                    {
                        cReportViewer.ShowCollectionReport(_WeekNumber, 1);
                    }

                    cReportViewer.SetOwner(Window.GetWindow(this));
                    cReportViewer.ShowDialog();
                }
                LogManager.WriteLog("CreateCollectionReport Successfull", LogManager.enumLogLevel.Info);
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        void CreateVarianceReport()
        {

            try
            {

                IReports objReports = ReportsBusinessObject.CreateInstance();

                LogManager.WriteLog("Fetching report data from database...", LogManager.enumLogLevel.Info);

                DataSet ExceptionDetails = objReports.GetExceptionSummary(_BatchID);

                if (ExceptionDetails.Tables[0].Rows.Count == 0)
                {
                    LogManager.WriteLog("No Records found for the selected criteria - Return", LogManager.enumLogLevel.Info);

                    MessageBox.ShowBox("MessageID261", BMC_Icon.Information);
                    return;
                }
                using (CReportViewer cReportViewer = new CReportViewer())
                {
                    LogManager.WriteLog("Report data fetched successfully from database", LogManager.enumLogLevel.Info);
                    cReportViewer.ShowVarianceReport(_BatchID);
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


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                GetCollectionReports();

                if (Security.SecurityHelper.HasAccess("BMC.Presentation.CommonCDOforDeclaration"))
                {
                    isCommonCDOforDeclaration = true;
                }
                else
                {
                    isCommonCDOforDeclaration = false;
                }
                ExchangeConst = (isCommonCDOforDeclaration && _SiteConfig != null) ? _SiteConfig.ExchangeConnectionString : "";
                TicketConst = (isCommonCDOforDeclaration && _SiteConfig != null) ? _SiteConfig.TicketConnectionString : "";
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void CreateLiquidationReport()
        {
            try
            {
                if (Settings.SGVI_Enabled)
                {
                    CreateSGVILiquidationReport();
                    return;
                }
                IReports objReports = isCommonCDOforDeclaration ? ReportsBusinessObject.CreateInstance(ExchangeConst, TicketConst) : ReportsBusinessObject.CreateInstance();

                LogManager.WriteLog("Check whether the Liquidation performed for the batch or not-Starts", LogManager.enumLogLevel.Info);
                int? iLiquidationPerformed = 0;
                int isLiquidationPerformedForBatch = objReports.CheckLiquidationPerformed(_BatchID, ref iLiquidationPerformed);

                if (isLiquidationPerformedForBatch == 0)
                {
                    MessageBox.ShowBox("MessageID891", BMC_Icon.Information);
                    return;
                }

                using (CReportViewer cReportViewer = new CReportViewer())
                {
                    LogManager.WriteLog("Launch the liquidation report", LogManager.enumLogLevel.Info);
                    cReportViewer.ShowLiquidationReportForRead(_BatchID, null);
                    cReportViewer.SetOwner(Window.GetWindow(this));
                    cReportViewer.ShowDialog();
                }

                LogManager.WriteLog("ShowLiquidationReport Successfull", LogManager.enumLogLevel.Info);
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void CreateSGVILiquidationReport()
        {
            try
            {
                IReports objReports = isCommonCDOforDeclaration ? ReportsBusinessObject.CreateInstance(ExchangeConst, TicketConst) : ReportsBusinessObject.CreateInstance();
                DataSet LiquidationDetails = objReports.GetLiquidationDetails(_BatchID);

                DataSet LiquidationSummaryDetails = objReports.GetLiquidationSummaryDetails(_BatchID);

                if (LiquidationDetails.Tables[0].Rows.Count == 0 || LiquidationSummaryDetails.Tables[0].Rows.Count == 0)
                {
                    LogManager.WriteLog("No Records found for the selected criteria - Return", LogManager.enumLogLevel.Info);

                    MessageBox.ShowBox("MessageID261", BMC_Icon.Information);
                    return;
                }
                using (CReportViewer cReportViewer = (isCommonCDOforDeclaration && !string.IsNullOrEmpty(ExchangeConst)) ? new CReportViewer(CommonUtilities.SiteConnectionString(ExchangeConst), CommonUtilities.TicketingConnectionString(TicketConst)) : new CReportViewer())
                {
                    LogManager.WriteLog("Report data fetched successfully from database", LogManager.enumLogLevel.Info);
                    cReportViewer.ShowLiquidationReport(LiquidationDetails, LiquidationSummaryDetails, _BatchID);
                    cReportViewer.SetOwner(Window.GetWindow(this));
                    cReportViewer.ShowDialog();
                }
                LogManager.WriteLog("ShowLiquidationReport Successfull", LogManager.enumLogLevel.Info);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void CreateMachineDropReport()
        {
            try
            {
                //IReports objReports = isCommonCDOforDeclaration ? ReportsBusinessObject.CreateInstance(ExchangeConst, TicketConst) : ReportsBusinessObject.CreateInstance();
                IReports objReports = ReportsBusinessObject.CreateInstance();
                DataSet MachineDrop = null;
                if (_BatchID > 0)
                {
                    MachineDrop = objReports.GetMachineDrop(_BatchID, 0);
                }
                else if (_WeekNumber > 0)
                {
                    MachineDrop = objReports.GetMachineDrop(_WeekNumber, 1);
                }

                //DataSet BatchWinLossDetails = objReports.GetBatchWinLoss(_BatchID, _WeekNumber);

                if (MachineDrop.Tables[0].Rows.Count == 0 || MachineDrop.Tables[0].Rows.Count == 0)
                {
                    LogManager.WriteLog("No Records found for the selected criteria - Return", LogManager.enumLogLevel.Info);

                    MessageBox.ShowBox("MessageID261", BMC_Icon.Information);
                    return;
                }
                using (CReportViewer cReportViewer = isCommonCDOforDeclaration ? new CReportViewer(ExchangeConst, TicketConst) : new CReportViewer())
                {
                    LogManager.WriteLog("Report data fetched successfully from database", LogManager.enumLogLevel.Info);

                    if (_BatchID > 0)
                    {
                        cReportViewer.ShowMachineDropReport(_BatchID, 0);
                    }
                    else if (_WeekNumber > 0)
                    {
                        cReportViewer.ShowMachineDropReport(_WeekNumber, 1);
                    }
                    cReportViewer.SetOwner(Window.GetWindow(this));
                    cReportViewer.ShowDialog();
                }
                LogManager.WriteLog("ShowMachine Drop Report Successfull", LogManager.enumLogLevel.Info);
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
                //IReports objReports = isCommonCDOforDeclaration ? ReportsBusinessObject.CreateInstance(ExchangeConst, TicketConst) : ReportsBusinessObject.CreateInstance();
                IReports objReports = ReportsBusinessObject.CreateInstance();
                DataSet BatchWinLossDetails = null;
                if (_BatchID > 0)
                {
                    BatchWinLossDetails = objReports.GetBatchWinLoss(_BatchID, 0);
                }
                else if (_WeekNumber > 0)
                {
                    BatchWinLossDetails = objReports.GetBatchWinLoss(_WeekNumber, 1);
                }

                //DataSet BatchWinLossDetails = objReports.GetBatchWinLoss(_BatchID, _WeekNumber);

                if (BatchWinLossDetails.Tables[0].Rows.Count == 0 || BatchWinLossDetails.Tables[0].Rows.Count == 0)
                {
                    LogManager.WriteLog("No Records found for the selected criteria - Return", LogManager.enumLogLevel.Info);

                    MessageBox.ShowBox("MessageID261", BMC_Icon.Information);
                    return;
                }
                using (CReportViewer cReportViewer = isCommonCDOforDeclaration ? new CReportViewer(ExchangeConst, TicketConst) : new CReportViewer())
                {
                    LogManager.WriteLog("Report data fetched successfully from database", LogManager.enumLogLevel.Info);

                    if (_BatchID > 0)
                    {
                        if (Settings.Region.ToUpper() == "UK")
                            cReportViewer.ShowBatchWinLossReportForUK(_BatchID, 0);
                        else
                        cReportViewer.ShowBatchWinLossReport(_BatchID, 0);
                    }
                    else if (_WeekNumber > 0)
                    {
                        if (Settings.Region.ToUpper() == "UK")
                            cReportViewer.ShowBatchWinLossReportForUK(_WeekNumber, 1);
                        else
                        cReportViewer.ShowBatchWinLossReport(_WeekNumber, 1);
                    }

                    cReportViewer.SetOwner(Window.GetWindow(this));
                    cReportViewer.ShowDialog();
                }
                LogManager.WriteLog("ShowMachine Drop Report Successfull", LogManager.enumLogLevel.Info);
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
        private void CreateWeeklyWinLossReport(int WeekID)
        {
            try
            {
                using (CReportViewer cReportViewer = new CReportViewer())
                {
                    LogManager.WriteLog("Report data fetched successfully from database", LogManager.enumLogLevel.Info);
                    cReportViewer.ShowWeeklyWinLossReport(WeekID);
                    cReportViewer.SetOwner(Window.GetWindow(this));
                    cReportViewer.ShowDialog();
                }

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }


        private void CreateWeeklyExceptionSummaryReport(int WeekID)
        {
            try
            {
                using (CReportViewer cReportViewer = new CReportViewer())
                {
                    LogManager.WriteLog("Report data fetched successfully from database", LogManager.enumLogLevel.Info);
                    cReportViewer.ShowWeeklyExceptionSummary(WeekID);
                    cReportViewer.SetOwner(Window.GetWindow(this));
                    cReportViewer.ShowDialog();
                }

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }


        /// <summary>
        /// Displays the Exception Summary Report for selected collection batch  in a new dialog
        /// </summary>
        private void CreateExceptionSummaryReport()
        {
            try
            {
                using (CReportViewer cReportViewer = new CReportViewer())
                {
                    LogManager.WriteLog("Report data fetched successfully from database", LogManager.enumLogLevel.Info);
                    if (!string.IsNullOrEmpty(ExchangeConst) && !string.IsNullOrEmpty(TicketConst))
                        cReportViewer.ShowExceptionSummaryReport(_BatchID, ExchangeConst, TicketConst);
                    else
                        cReportViewer.ShowExceptionSummaryReport(_BatchID);
                    cReportViewer.SetOwner(Window.GetWindow(this));
                    cReportViewer.ShowDialog();
                }

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
        #endregion

        #region Public Methods
        public CollectionBatchDetailReports(int BatchId, int WeekNumber, SiteConfig SiteConfig, Window window, bool ChkWeek)
        {
            try
            {
                InitializeComponent();
                this._BatchID = BatchId;
                _WeekNumber = WeekNumber;
                _SiteConfig = SiteConfig;
                _Parent = window;
                _ChkWeek = ChkWeek;
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
        public CollectionBatchDetailReports(int BatchId, int WeekNumber, SiteConfig SiteConfig, Window window, bool ChkWeek, bool _Undeclared):this(BatchId, WeekNumber, SiteConfig, window,  ChkWeek)
        {
            try
            {
                this._Undeclared = _Undeclared;
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
        #endregion

        
    }
}
