using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Audit.BusinessClasses;
using Audit.Transport;
using BMC.CashDeskOperator;
using BMC.CashDeskOperator.BusinessObjects;
using BMC.Common.ExceptionManagement;
using BMC.Common.LogManagement;
using Microsoft.Win32;
using System.Windows.Input;
using BMC.Common.Utilities;
using BMC.CoreLib;
using System.IO;

namespace BMC.Presentation
{
    /// <summary>
    /// Interaction logic for CMachineMaintenanceView.xaml
    /// </summary>
    public partial class CMachineMaintenanceView 
    {
        #region Private Variables

        private string strBarPosName        =   string.Empty;
        private int installationNo;
        
        #endregion Private Variables

        #region Constructor

        public CMachineMaintenanceView()
        {
            InitializeComponent();            
        }

        public CMachineMaintenanceView(string strBarPos, int installationNumber)
        {
            InitializeComponent();
            BarPosName = strBarPos;
            installationNo = installationNumber;
            PopulateSession();                        
        }

        #endregion Constructor

        #region Properties

        public string BarPosName
        {
            get
            {
                return strBarPosName;
            }
            set
            {
                strBarPosName = value;
            }

        }

        #endregion Properties

        #region Events

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                PopulateSession();
                SetSlotPortStatus();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (chkHistory.IsChecked.Value)
                {
                    if (cmbSessions.SelectedIndex > -1)
                    {
                        PopulateEvents(((CMaintenanceSession)cmbSessions.SelectedItem).ID.Value);
                    }
                }
                else if (chkCurrent.IsChecked.Value)
                {
                    PopulateSession();
                    if (txtOpenSession.Tag != null)
                    {
                        PopulateEvents((int)txtOpenSession.Tag);
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
                
        }

        private void btnMainain_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CMachineMaintenance objMachineMaintenance = new CMachineMaintenance();

                //Start 
                if (btnMainain.Content == FindResource("CMachineMaintenanceView_xaml_btnMainain"))
                {
                    if (MessageBox.ShowBox("MessageID256", BMC_Icon.Question, BMC_Button.YesNo) == System.Windows.Forms.DialogResult.No)
                        return;

                    if (objMachineMaintenance.ManageMaintenance(installationNo, 0, Security.SecurityHelper.CurrentUser.SecurityUserID) > 0)
                    {
                        MessageBox.ShowBox("MessageID339", BMC_Icon.Information, BMC_Button.OK);

                        PopulateSession();
                        if (txtOpenSession.Tag != null)
                        {
                            PopulateEvents((int)txtOpenSession.Tag);
                        }
                        return;
                    }

                    //LogManager.WriteLog("Executing Path : " + BMCRegistryHelper.GetRegKeyValue(string.Empty,"InstallationPath").ToString().Trim()  + Common.ConfigurationManagement.ConfigManager.Read(
                    //                "HandpayCommandLinePrompt") + " DisableMachine " + installationNo, LogManager.enumLogLevel.Info);



                    //System.Diagnostics.Process.Start(BMCRegistryHelper.GetRegKeyValue(string.Empty, "InstallationPath").ToString().Trim().ToString().Trim() + Common.ConfigurationManagement.ConfigManager.Read(
                    //            "HandpayCommandLinePrompt"), " DisableMachine " + installationNo);

                    LogManager.WriteLog("Executing Path : " + Path.Combine(Extensions.GetStartupDirectory(), Common.ConfigurationManagement.ConfigManager.Read(
                "HandpayCommandLinePrompt")) + " DisableMachine " + installationNo, LogManager.enumLogLevel.Info);

                    System.Diagnostics.Process.Start(Path.Combine(Extensions.GetStartupDirectory(), Common.ConfigurationManagement.ConfigManager.Read(
                                "HandpayCommandLinePrompt")), " DisableMachine " + installationNo);


                    AuditViewerBusiness.InsertAuditData(new Audit.Transport.Audit_History
                    {
                        
                        AuditModuleName = ModuleName.MachineMaintenance,
                        Audit_Screen_Name = "Machine Maintenance View",
                        Audit_Desc = "Start Maintenance - Installation No: " + installationNo.ToString(),
                        AuditOperationType = OperationType.MODIFY,
                    });

                    MessageBox.ShowBox("MessageID309", BMC_Icon.Information, BMC_Button.OK);
                }
                else//Close 
                {
                    int iMachineEventStatus = objMachineMaintenance.CheckMachineMaintenance(installationNo);
                    if (iMachineEventStatus < 0)
                    {
                        if (MessageBox.ShowBox("MessageID304", BMC_Icon.Question, BMC_Button.YesNo) == System.Windows.Forms.DialogResult.No)
                        {
                            return;
                        }
                        CAuthorize objAuthorize = new CAuthorize("CashdeskOperator.Authorize.cs.OverrideEvents");
                        objAuthorize.User = Security.SecurityHelper.CurrentUser;
                        if (!Security.SecurityHelper.HasAccess("CashdeskOperator.Authorize.cs.OverrideEvents"))
                        {
                            objAuthorize.ShowDialog();
                            if (!objAuthorize.IsAuthorized)
                                return;
                        }
                        else
                        {
                            objAuthorize.IsAuthorized = true;
                        }
                    }

                    if (MessageBox.ShowBox("MessageID257", BMC_Icon.Question, BMC_Button.YesNo) == System.Windows.Forms.DialogResult.No)
                        return;

                    var objReasonCategory = new CReasonCategory();
                    objReasonCategory.ShowDialog();

                    if (objReasonCategory.MaintenanceReasonCategory != null && objReasonCategory.MaintenanceReasonCategory.Length > 0)
                    {
                        objMachineMaintenance.CloseMaintenance(installationNo, Security.SecurityHelper.CurrentUser.SecurityUserID,
                            objReasonCategory.MaintenanceReasonCategory);



                        AuditViewerBusiness.InsertAuditData(new Audit.Transport.Audit_History
                        {
                            
                            AuditModuleName = ModuleName.MachineMaintenance,
                            Audit_Screen_Name = "Machine Maintenance View",
                            Audit_Desc = "Close Maintenance - Installation No: " + installationNo.ToString(),
                            AuditOperationType = OperationType.MODIFY,
                        });

                        MessageBox.ShowBox("MessageID310", BMC_Icon.Information, BMC_Button.OK);
                    }
                    else
                        return;
                }
                PopulateSession();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void cmbSessions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (cmbSessions.SelectedIndex > -1 && chkHistory.IsChecked.Value)
                {
                    PopulateEvents(((CMaintenanceSession)cmbSessions.SelectedItem).ID.Value);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void chkCurrent_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                cmbSessions.Visibility = Visibility.Collapsed;
                txtOpenSession.Visibility = Visibility.Visible;
                btnMainain.Visibility = Visibility.Visible;
                btnRefresh.Visibility = Visibility.Visible;
                
                if (txtOpenSession.Tag != null)
                {
                    PopulateEvents((int)txtOpenSession.Tag);
                    PortBlocking.Visibility = Visibility.Visible;
                    btnBlockPorts.Visibility = Visibility.Visible;
                    Thickness lstSessionEventsThickness = new Thickness(17, 102, 16, 260);
                    lstSessionEvents.Margin = lstSessionEventsThickness; 
                }
                else
                {
                    lstSessionEvents.ItemsSource = null;
                    PortBlocking.Visibility = Visibility.Collapsed;
                    btnBlockPorts.Visibility = Visibility.Collapsed;
                    Thickness lstSessionEventsThickness = new Thickness(17,102,16,160);
                    lstSessionEvents.Margin = lstSessionEventsThickness;                    
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void chkHistory_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                cmbSessions.Visibility = Visibility.Visible;
                txtOpenSession.Visibility = Visibility.Collapsed;
                btnMainain.Visibility = Visibility.Collapsed;
                btnRefresh.Visibility = Visibility.Collapsed;
                PortBlocking.Visibility = Visibility.Collapsed;
                btnBlockPorts.Visibility = Visibility.Collapsed;
                Thickness lstSessionEventsThickness = new Thickness(17, 102, 16, 160);
                lstSessionEvents.Margin = lstSessionEventsThickness;

                if (cmbSessions.SelectedIndex > -1)
                {
                    PopulateEvents(((CMaintenanceSession)cmbSessions.SelectedItem).ID.Value);
                }
                else
                {
                    lstSessionEvents.ItemsSource = null;                    
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        #endregion Events
        
        #region Private Methods

        private void PopulateSession()
        {
            int iOpenSessionIndex = 0;
            CMachineMaintenance objMachineMaintenance = new CMachineMaintenance();
            CMaintenanceSession objCurrentSession = new CMaintenanceSession();
            List<CMaintenanceSession> objlstSession = objMachineMaintenance.GetMaintainSessionForInstallation(installationNo);

            iOpenSessionIndex = GetOpenSessionIndex(objlstSession);
            
            if (iOpenSessionIndex == -1)
            {
                btnMainain.Content = FindResource("CMachineMaintenanceView_xaml_btnMainain");
                txtOpenSession.Text = "";
                txtOpenSession.Tag = null;
                SetScreenStatus("HISTORY");
            }
            else
            {
                btnMainain.Content = FindResource("CMachineMaintenanceView_xaml_btnMainain_Close");
                objCurrentSession = objlstSession.ElementAtOrDefault(iOpenSessionIndex);
                objlstSession.RemoveAt(iOpenSessionIndex);
                txtOpenSession.Text = objCurrentSession.SessionName;
                txtOpenSession.Tag = objCurrentSession.ID;
                SetScreenStatus("CURRENT");
            }
            cmbSessions.ItemsSource = objlstSession;
            cmbSessions.SelectedIndex = 0;
        }

        private int GetOpenSessionIndex(List<CMaintenanceSession> objlstSession)
        {
            int iIndex = 0;
            int iReturn = -1;
            foreach (var obj in objlstSession)
            {
                if (obj.IsSessionOpen.Value)
                {
                    iReturn = iIndex;
                    break;
                }
                iIndex++;
            }
            return iReturn;
        }

        private void PopulateEvents(int SessionID)
        {
            CMachineMaintenance objMachineMaintenance = new CMachineMaintenance();
            List<GetEventsForMaintainSessionResult> objlstMaintenanceHistory = objMachineMaintenance.GetEventsForMaintainSession(SessionID,installationNo);

            lstSessionEvents.ItemsSource = objlstMaintenanceHistory;
        }

        private void SetScreenStatus(string strStatus)
        {
            if (strStatus == "CURRENT")
            {
                chkCurrent.IsChecked = true;
            }
            else
            {
                chkHistory.IsChecked = true;
            }
        }

        private void SetSlotPortStatus()
        {
            try
            {
                LogManager.WriteLog("Inside SetSlotPortStatus method", LogManager.enumLogLevel.Info);

                CMachineMaintenance objMachineMaintenance   =   new CMachineMaintenance();

                CSlotPortStatus cSlotPortStatus             =   objMachineMaintenance.GetSlotPortStatusForInstallation(installationNo).SingleOrDefault();

                rbAuxSerialPortEnabled.IsChecked    =   cSlotPortStatus.AuxSerialPortEnabled == true ? true : false;
                rbAuxSerialPortDisabled.IsChecked   =   cSlotPortStatus.AuxSerialPortEnabled == true ? false : true;

                rbGatSerialPortEnabled.IsChecked    =   cSlotPortStatus.GatSerialPortEnabled == true ? true : false;
                rbGatSerialPortDisabled.IsChecked   =   cSlotPortStatus.GatSerialPortEnabled == true ? false : true;

                rbSlotLinePortEnabled.IsChecked     =   cSlotPortStatus.SlotLinePortEnabled == true ? true : false;
                rbSlotLinePortDisabled.IsChecked    =   cSlotPortStatus.SlotLinePortEnabled == true ? false : true;

                LogManager.WriteLog("Slot Port Status set successfully", LogManager.enumLogLevel.Info);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void ManageSlotPorts()
        {
            LogManager.WriteLog("Inside ManageSlotPorts method", LogManager.enumLogLevel.Info);

            string message              =   string.Empty;

            string auxSerialPort    =   rbAuxSerialPortEnabled.IsChecked == true ? "0" : "1";
            string gatSerialPort    =   rbGatSerialPortEnabled.IsChecked == true ? "0" : "1";
            string slotLinePort     =   rbSlotLinePortEnabled.IsChecked == true ? "0" : "1";

            message = string.Format("{0},{1},{2}", auxSerialPort, gatSerialPort, slotLinePort);

            LogManager.WriteLog(string.Format("{0} - {1}, {2} - {3}, {4} - {5}", "Aux Serial Port Flag", auxSerialPort,
                "GAT Serial Port Flag", gatSerialPort, "Slot Line Port Flag", slotLinePort), LogManager.enumLogLevel.Info);

            CMachineMaintenance cMachineMaintenance = new CMachineMaintenance();

            LogManager.WriteLog("Sending Sector 203 Command to GMU to Enable/Disable Slot Port(s)...", LogManager.enumLogLevel.Info);

            cMachineMaintenance.ManageSlotPorts(installationNo, message);                

            LogManager.WriteLog("Sector 203 Command to Enable/Disable Slot Port(s) sent successfully to GMU.", LogManager.enumLogLevel.Info);
        
        }

        private void UpdateSlotPortStatus()
        {
            LogManager.WriteLog("Inside UpdateSlotPortStatus method", LogManager.enumLogLevel.Info);

            CMachineMaintenance cMachineMaintenance = new CMachineMaintenance();

            bool auxSerialPort  =   rbAuxSerialPortEnabled.IsChecked == true ? true : false;
            bool gatSerialPort  =   rbGatSerialPortEnabled.IsChecked == true ? true: false;                
            bool slotLinePort   =   rbSlotLinePortEnabled.IsChecked == true ? true : false;

            LogManager.WriteLog("Updating database with the Current Slot Port(s) status...", LogManager.enumLogLevel.Info);

            int result = cMachineMaintenance.UpdateSlotPortStatus(installationNo, auxSerialPort, gatSerialPort, slotLinePort);

            LogManager.WriteLog("Database updated successfully with the Current Slot Port(s) status", LogManager.enumLogLevel.Info);           
        }

        #endregion Private Methods

        #region Events

        private void btnBlockPorts_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ManageSlotPorts();
                UpdateSlotPortStatus();               

                try
                {
                    AuditViewerBusiness.InsertAuditData(new Audit.Transport.Audit_History
                    {
                        AuditModuleName = ModuleName.PortBlocking,
                        Audit_Screen_Name = "Enrollment|Enable/Disable Ports",
                        Audit_Desc = "Slot Ports Enabled/Disabled",
                        AuditOperationType = OperationType.MODIFY,
                        Audit_Field = "IsAuxSerialPortEnabled|IsGatSerialPortEnabled|IsSlotLinePortEnabled",
                        Audit_New_Vl = string.Format("{0}|{1}|{2}", Convert.ToInt32(rbAuxSerialPortEnabled.IsChecked).ToString(),
                                    Convert.ToInt32(rbGatSerialPortEnabled.IsChecked).ToString(), Convert.ToInt32(rbSlotLinePortDisabled.IsChecked).ToString()),
                        Audit_Slot = installationNo.ToString()
                    });
                }
                catch (Exception ex)
                {
                    ExceptionManager.Publish(ex);
                }

                MessageBox.ShowBox("MessageID349", BMC_Icon.Information);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                MessageBox.ShowBox("MessageID350", BMC_Icon.Error);
            }
        }

        #endregion Events
    }
}
