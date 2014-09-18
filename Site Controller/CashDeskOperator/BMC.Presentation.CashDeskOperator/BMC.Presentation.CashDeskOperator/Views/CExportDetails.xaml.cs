using System.Windows;
using System.Windows.Controls;
using BMC.Common.LogManagement;
using BMC.Transport;
using BMC.CashDeskOperator.BusinessObjects;
using System.Collections.Generic;
using System;
using BMC.Common.ExceptionManagement;
using BMC.Security;
using Audit.BusinessClasses;
using Audit.Transport;
using System.ServiceProcess;
using System.Threading;

namespace BMC.Presentation.POS.Views
{
    /// <summary>
    /// Interaction logic for CExportDetails.xaml
    /// </summary>
    /// 
    public delegate void delExecute();
    public partial class CExportDetails : UserControl
    {
        ExportDetailBO oExportDetailObject = null;
        List<UnExportedData> oUnExportedData = null;
        public string svcStatus = "";
      
        public CExportDetails()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                LogManager.WriteLog("Export Detail :Loading Export Detail UI", LogManager.enumLogLevel.Debug);
                oExportDetailObject = new ExportDetailBO();

                List<StatusType> StatusTypes = oExportDetailObject.GetStatusTypes();
                cmbStatusType.ItemsSource = StatusTypes;

                cmbStatusType.DisplayMemberPath = "Description";
                cmbStatusType.SelectedValuePath = "Type";
                cmbStatusType.SelectedIndex = 0;

                RefreshUnExportedList();
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
        }

        private void btn_Refresh_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                RefreshUnExportedList();
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
        }

        private void RefreshUnExportedList()
        {
            try
            {
                pgBarExportDetails.Visibility = Visibility.Collapsed;
                txtPGStatusExportDetails.Visibility = Visibility.Collapsed;
                oUnExportedData = oExportDetailObject.ReadUnExportedData(string.Empty);
                lvUnExportData.ItemsSource = oUnExportedData;
                chk_CheckAllDetails.IsChecked = false;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public bool ServiceStatus(bool bStopService)
        {
            try
            {
                bool bStatus = false;
                int iStopAttemptCounts = 5;

                ServiceController myService = new ServiceController();
                myService.ServiceName = "BMCExchangeImportExport";
                ServiceControllerStatus svcStatus = myService.Status;

                if (bStopService)
                {
                    if (svcStatus != ServiceControllerStatus.Stopped)
                    {
                        myService.Stop();
                        while (svcStatus != ServiceControllerStatus.Stopped )
                        {
                            if (iStopAttemptCounts <= 0)
                                throw new Exception("Maximum attempts to stop service reached");
                            myService.Refresh();
                            svcStatus = myService.Status;
                            Thread.Sleep(200);
                            iStopAttemptCounts--;
                            LogManager.WriteLog(string.Format("CExportDetails->ServiceStatus: Trying to stop BMCExchangeImportExport Attempt : {0}" , iStopAttemptCounts),LogManager.enumLogLevel.Debug);
                        }
                    }

                    if (svcStatus == ServiceControllerStatus.Stopped)
                        bStatus = true;
                }
                else
                {
                    delExecute dExecute = new delExecute(myService.Start);
                    if (svcStatus != ServiceControllerStatus.Running)
                    {
                        dExecute.Invoke();
                    }
                    bStatus = true;
                }

                return bStatus;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                if (bStopService)
                    MessageBox.ShowBox("MessageID536", BMC_Icon.Error, BMC_Button.OK);
                else
                    MessageBox.ShowBox("MessageID537", BMC_Icon.Error, BMC_Button.OK);
                return false;
            }
        }

        private void UpdateProgressStatus(string progress, int iProMac)
        {
            /* 
             * Used dispatcher as the UI thread will be accessed from another thread
             */
            System.Windows.Application.Current.Dispatcher.Invoke((ThreadStart)delegate
            {
                txtPGStatusExportDetails.Text = Convert.ToString(Application.Current.FindResource(progress));
                pgBarExportDetails.Value = iProMac;
            });
        }

        private void btn_Update_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string strIDs = string.Empty;
                btn_Update.IsEnabled = false;
                pgBarExportDetails.Visibility = Visibility.Visible;
                txtPGStatusExportDetails.Visibility = Visibility.Visible;
                if (oUnExportedData != null)
                {
                    oUnExportedData.ForEach(x =>
                    {
                        if (x.IsSelected == true)
                            strIDs = strIDs + "," + x.ID.ToString();
                    });
                }

                strIDs = strIDs.TrimStart(',');
                if (strIDs.Length > 0)
                {
                    UpdateProgressStatus("MessageID539", 5);
                    if (ServiceStatus(true))
                    {
                        try
                        {
                            UpdateProgressStatus("MessageID540", 33);
                            if (strIDs.Length > 0)
                            {
                                int iStatus = ((StatusType)cmbStatusType.SelectedItem).Type;

                                if (iStatus == 100)
                                    if (MessageBox.ShowBox("MessageID501", BMC_Icon.Information, BMC_Button.YesNo) == System.Windows.Forms.DialogResult.No)
                                        return;

                                LogManager.WriteLog("Export Details: btn_Update_Click Updating Export Status of : " + strIDs + " to " + iStatus.ToString(), LogManager.enumLogLevel.Debug);
                                UpdateProgressStatus("MessageID541", 38);
                                oExportDetailObject.UpdateUnExportedData(iStatus, strIDs);
                                UpdateProgressStatus("MessageID542", 66);
                                AuditViewerBusiness.InsertAuditData(new Audit.Transport.Audit_History
                                {
                                    AuditModuleName = ModuleName.ExportDetail,
                                    Audit_Screen_Name = "Export Detail",
                                    Audit_Desc = "Export status for EH_ID : " + strIDs + " has be modified",
                                    AuditOperationType = OperationType.MODIFY,
                                    Audit_Field = "EH_Status",
                                    Audit_Old_Vl = "NULL OR -1",
                                    Audit_New_Vl = iStatus.ToString()
                                });

                                RefreshUnExportedList();
                            }

                            
                        }
                        catch (Exception ex)
                        {
                            ExceptionManager.Publish(ex);
                        }
                        finally
                        {
							UpdateProgressStatus("MessageID543", 71);
                            ServiceStatus(false);
                            UpdateProgressStatus("MessageID544", 100);
                        }
                    }
                    else
                    {
                        UpdateProgressStatus("MessageID545", 0);
                    }
                }
                else
                    MessageBox.ShowBox("MessageID502", BMC_Icon.Information, BMC_Button.OK);
            }
            catch (Exception Ex)
            {
                MessageBox.ShowBox("MessageID538", BMC_Icon.Error, BMC_Button.OK);
                ExceptionManager.Publish(Ex);
            }
            finally
            {
                btn_Update.IsEnabled = true;
                pgBarExportDetails.Visibility = Visibility.Collapsed;
                txtPGStatusExportDetails.Visibility = Visibility.Collapsed;
            }
        }

        private void chk_CheckAllDetails_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                SelectAllUnExportedDetails(chk_CheckAllDetails.IsChecked.Value);
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
        }

        void SelectAllUnExportedDetails(bool IsSelected)
        {
            if (oUnExportedData != null && oUnExportedData.Count > 0)
            {
                foreach (var item in oUnExportedData)
                {
                    item.IsSelected = IsSelected;
                }
            }
        }
    }
}