using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
using Audit.BusinessClasses;
using Audit.Transport;
using BMC.CashDeskOperator;
using BMC.CashDeskOperator.BusinessObjects;
using BMC.Common.ConfigurationManagement;
using BMC.Common.ExceptionManagement;
using BMC.Common.LogManagement;
using BMC.Common.Utilities;
using BMC.Presentation.POS.Views;
using BMC.Security;
using BMC.Transport;
using BMC.Business.CashDeskOperator;
using BMC.Presentation.POS.Helper_classes;
using HC = BMC.Presentation.Helper_classes;
using System.Windows.Threading;
using System.Threading;
using System.Windows.Media;
using BMCIPC;
using BMC.CoreLib;

namespace BMC.Presentation
{
    /// <summary>
    /// Interaction logic for PosDetails.xaml
    /// </summary>
    public partial class PosDetails : IDisposable, ICashDispenserStatusParent
    {
        #region Variables
        public event CancelEventHandler Exit;
        private SlotMachine _SlotMachine = null;
        readonly LockHandler _lockHandler = new LockHandler();
        private const string LockappMachineadmin = "MACHINEADMIN";
        private readonly int? _userNo = SecurityHelper.CurrentUser.SecurityUserID;
        private readonly int INSTALLATIONDEFLOAT = 1;
        private readonly int INTRANSITEASSET = 18;
        //PositionDetail positionDetail;
        private System.Windows.Threading.DispatcherTimer Timer;
        IAnalysis analysisBusinessObject = AnalysisBusinessObject.CreateInstance();

        //private int Installation_No;
        //private SlotMachineStatus SlotMachineStatus;
        private bool IsEventUncleared;
        private string sBarPosName;
        private DateTime dtInstallationStartDate;
        private DateTime dtInstallationStartDateforCM;
        private int nInstallationFloatStatus;
        private bool isScreenLoaded = false;
        private bool isFirstTime = true;
        private PositionDetailsScreen? _currentScreen = null;
        CViewHandpay cViewHandpay;
        #endregion

        #region Constructor

        public PosDetails()
        {
            InitializeComponent();
            this.InitCashDispenser();
        }

        public PosDetails(SlotMachine objSlotMachine)
        {
            try
            {
                InitializeComponent();
                SlotMachine = objSlotMachine;
                this.InitCashDispenser();

                LogManager.WriteLog("Inside PosDetails Constructor", LogManager.enumLogLevel.Info);

                if (_SlotMachine.Status == SlotMachineStatus.EmptyPosition || _SlotMachine.Status == SlotMachineStatus.FloatCollection
                    || _SlotMachine.Status == SlotMachineStatus.ForceFinalCollection)
                {
                    DisableControlsIfNoMeters();
                }
                Timer = new System.Windows.Threading.DispatcherTimer();
                Timer.Interval = new TimeSpan(0, 0, Int32.Parse(ConfigManager.Read("RefreshTime").ToString()));
                Timer.IsEnabled = true;
                Timer.Tick += Timer_Tick;
                Timer.Start();
                LogManager.WriteLog("PosDetails Timer started from Constructor", LogManager.enumLogLevel.Info);
                // RefreshSlot();
                // RefreshButtons(_SlotMachine.Status);               

                if (Settings.RegulatoryEnabled && Settings.RegulatoryType == "AAMS" && Settings.MachineMaintenance)
                {
                    rbMachineMaintenance.Visibility = Visibility.Visible;
                    rbMachineMaintenance.Content = FindResource("PosDetails_xaml_rbMachineMaintenance");
                }
                else
                {
                    rbMachineMaintenance.Visibility = Visibility.Collapsed;
                }

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

        }

        private void InitCashDispenser()
        {
            this.UpperDeckText = new DispenserText();
            this.LowerDeckText = new DispenserText();
            this.StatusText = "Loading...";
        }
        #endregion

        #region Events
        void Timer_Tick(object sender, EventArgs e)
        {
            DispatcherTimer timer = sender as DispatcherTimer;
            try
            {
                timer.IsEnabled = false;
                RefreshSlot();
                //RefreshButtons(ucSlotMachine.Status);
                Thread.Sleep(100);
            }
            finally
            {
                timer.IsEnabled = true;
            }
        }
        private void btn_Exit(object sender, RoutedEventArgs e)
        {
            if (Exit != null)
            {
                if (Timer != null)
                {
                    Timer.Stop();
                    Timer.Tick -= Timer_Tick;
                    LogManager.WriteLog("PositionDetails Timer stopped on Exit.", LogManager.enumLogLevel.Info);
                }
                Exit.Invoke(this, new CancelEventArgs());
            }
        }

        private void Handpay_Click(object sender, RoutedEventArgs e)
        {
            LogManager.WriteLog("Handpay_Click", LogManager.enumLogLevel.Info);
            LoadScreen(PositionDetailsScreen.Handpay);
            isScreenLoaded = true;
        }

        private void PlayerClub_Click(object sender, RoutedEventArgs e)
        {
            LogManager.WriteLog("PlayerClub_Click", LogManager.enumLogLevel.Info);
            LoadScreen(PositionDetailsScreen.PlayerClub);
            isScreenLoaded = true;
        }

        private void FldService_Click(object sender, RoutedEventArgs e)
        {
            LogManager.WriteLog("FldService_Click", LogManager.enumLogLevel.Info);
            LoadScreen(PositionDetailsScreen.FieldService);
            isScreenLoaded = true;
        }

        private void Events_Click(object sender, RoutedEventArgs e)
        {
            LogManager.WriteLog("Events_Click", LogManager.enumLogLevel.Info);
            LoadScreen(PositionDetailsScreen.Events);
            isScreenLoaded = true;
        }

        private void MCEnroll_Click(object sender, RoutedEventArgs e)
        {
        }

        private void MCMeters_Click(object sender, RoutedEventArgs e)
        {
            LogManager.WriteLog("MCMeters_Click", LogManager.enumLogLevel.Info);
            LoadScreen(PositionDetailsScreen.MachineMeters);
            isScreenLoaded = true;
        }

        private void FillsCredits_Click(object sender, RoutedEventArgs e)
        {
        }

        private void MCCurrentMeters_Click(object sender, RoutedEventArgs e)
        {
            LogManager.WriteLog("MCCurrentMeters_Click", LogManager.enumLogLevel.Info);
            LoadScreen(PositionDetailsScreen.CurrentMeters);
            isScreenLoaded = true;
        }



        //private void RefreshButtons(SlotMachineStatus eStatus)
        //{
        //    //Remove Machine
        //    CollectionHelper CollectionHelper = new CollectionHelper();

        //    if ((eStatus == SlotMachineStatus.FloatCollection && !CollectionHelper.HasUndeclaredCollecion(_SlotMachine.InstallationNo))
        //        || !Settings.IsFinalDropRequiredForRemoval
        //        || eStatus == SlotMachineStatus.InstallationCompletedNonMetered
        //        || eStatus == SlotMachineStatus.GameInstallationAAMSPending
        //        || eStatus == SlotMachineStatus.VLTInstallationAAMSPending )
        //    {
        //        if (!Security.SecurityHelper.HasAccess("CashdeskOperator.FloorView.cs.RemoveMachine"))
        //            rbRemoveMachine.Visibility = Visibility.Collapsed;
        //        else
        //            rbRemoveMachine.Visibility = Visibility.Visible;
        //    }
        //    else
        //        rbRemoveMachine.Visibility = Visibility.Collapsed;


        //    //Reinstate Machine
        //    if ((eStatus == SlotMachineStatus.FloatCollection && !CollectionHelper.HasUndeclaredCollecion(_SlotMachine.InstallationNo)))
        //    {
        //        rbReInstateMachine.Visibility = Visibility.Visible;
        //        rbReInstateMachine.IsEnabled = true;
        //        rbEvents.Visibility = Visibility.Collapsed;
        //        rbFieldService.Visibility = Visibility.Collapsed;
        //        rbHandpay.Visibility = Visibility.Collapsed;
        //        rbMachineMaintenance.Visibility = Visibility.Collapsed;
        //        rbMachineMeters.Visibility = Visibility.Collapsed;
        //        rbPlayerClub.Visibility = Visibility.Collapsed;

        //        //rbRemoveMachine.IsChecked = true;

        //    }

        //    else
        //        rbReInstateMachine.Visibility = Visibility.Collapsed;

        //} 

        private void SyncTicketExpire_Click(object sender, RoutedEventArgs e)
        {
            LogManager.WriteLog("SyncTicketExpire_Click", LogManager.enumLogLevel.Info);
            MachineManagerLazyInitializer manager = null;

            try
            {
                if (MessageBox.ShowBox("MessageID365", BMC_Icon.Question, BMC_Button.YesNo) == System.Windows.Forms.DialogResult.No) return;
                int InstallationNo = _SlotMachine.InstallationNo;
                oCommonUtilities.CreateInstance().UpdateTicketExpire(Settings.Ticket_Expire);
                manager = new MachineManagerLazyInitializer();
                int nSuccess = manager.GetMachineManager().UpdateTicketConfig(InstallationNo, Settings.Ticket_Expire);
                if (!(nSuccess == 0))
                {
                    MessageBox.ShowBox("MessageID364", BMC_Icon.Warning, _SlotMachine.AssetNumber.ToString());
                    return;
                }
                else
                {
                    oCommonUtilities.CreateInstance().UpdateGMUSiteCodeStatus(InstallationNo, 1);
                }
                MessageBox.ShowBox("MessageID366", BMC_Icon.Information, _SlotMachine.AssetNumber.ToString());
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
            finally
            {
                if (manager != null)
                {
                    manager.ReleaseMachineManager();
                    manager = null;
                }
            }
        }



        private void RemoveMachine_Click(object sender, RoutedEventArgs e)
        {
            LogManager.WriteLog("RemoveMachine_Click", LogManager.enumLogLevel.Info);

            int machineStatusFlag = 0;
            string siteCode = string.Empty;
            bool ShowRemoveConfirmation = true;
            bool canProcess = false;

            try
            {
                Cursor = System.Windows.Input.Cursors.Wait;

                // For Transit Machine - Italy Requirement
                if (Settings.RegulatoryEnabled && Settings.RegulatoryType == "AAMS" && ucSlotMachine.Status != SlotMachineStatus.GameInstallationAAMSPending && ucSlotMachine.Status != SlotMachineStatus.VLTInstallationAAMSPending)
                {
                    if (MessageBox.ShowBox("MessageID344", BMC_Icon.Question, BMC_Button.YesNo) == System.Windows.Forms.DialogResult.Yes)
                    {
                        while (!canProcess)
                        {
                            ActiveSites objActiveSites = new ActiveSites();

                            try
                            {
                                objActiveSites.ShowDialogEx(this);

                                if (objActiveSites.DialogResult == true)
                                {
                                    siteCode = objActiveSites.TransiteSiteCode;
                                    machineStatusFlag = INTRANSITEASSET;
                                    ShowRemoveConfirmation = false;

                                    if (siteCode == string.Empty | siteCode == "Select")
                                    {
                                        MessageBox.ShowBox("MessageID340", BMC_Icon.Information);
                                    }
                                    else
                                    {
                                        canProcess = true;
                                    }
                                }
                                else
                                {
                                    return;
                                }
                            }
                            catch (Exception ex)
                            {
                                ExceptionManager.Publish(ex);
                                return;
                            }
                            finally
                            {
                                if (objActiveSites != null)
                                    objActiveSites = null;
                            }
                        }
                    }
                    else
                    {
                        ShowRemoveConfirmation = true;
                    }
                }

                //Confirmation
                if (ShowRemoveConfirmation)
                {
                    if (MessageBox.ShowBox("MessageID4", BMC_Icon.Question, BMC_Button.YesNo) == System.Windows.Forms.DialogResult.No)
                        return;
                }
                int InstallationNo = _SlotMachine.InstallationNo;
                CollectionHelper CollectionHelper = new CollectionHelper();

                #region Machine Removal

                InstallationDataContext objRemoveContext =
                                        new InstallationDataContext(oCommonUtilities.CreateInstance().GetConnectionString());
                int nDisMachine = 0;

                //When FinalCollection Status is set machine must be already disabled.
                if (_SlotMachine.FinalCollectionStatus == 0)
                    foreach (var IP in objRemoveContext.GetDisableMachine(InstallationNo))
                    {
                        nDisMachine = IP.DisMachine;
                    }


                EnrollmentErrorCodes ErrorCode = EnrollmentBusinessObject.CreateInstance().RemoveMachine(InstallationNo, machineStatusFlag, siteCode, nDisMachine);
                switch (ErrorCode)
                {
                    case EnrollmentErrorCodes.DatabaseError:
                        {
                            MessageBox.ShowBox("MessageID206");
                            Audit_Error("Database Error");
                            break;
                        }
                    case EnrollmentErrorCodes.EnterpriseWebServiceCommunicationFailure:
                        {
                            MessageBox.ShowBox("MessageID207");
                            Audit_Error("Enterprise WebService Communication Failure");
                            break;
                        }
                    case EnrollmentErrorCodes.RemoveFromPollingListFailure:
                        {
                            MessageBox.ShowBox("MessageID208");
                            Audit_Error("Unable to remove from Polling list");
                            break;
                        }
                    case EnrollmentErrorCodes.ExchangeHostServiceNotRunning:
                        {
                            Audit_Error("Unable to remove from Polling list: Timeout occured");
                            if (MessageBox.ShowBox("MessageID359", BMC_Icon.Question, BMC_Button.YesNo) == System.Windows.Forms.DialogResult.No)
                                return;
                            //Calling Remove Machine with Disable Machine command as false, Since first attempt to Disable Failed.
                            ErrorCode = EnrollmentBusinessObject.CreateInstance().RemoveMachine(InstallationNo, machineStatusFlag, siteCode, 0);

                            switch (ErrorCode)
                            {
                                case EnrollmentErrorCodes.DatabaseError:
                                    {
                                        MessageBox.ShowBox("MessageID206");
                                        Audit_Error("Database Error");
                                        break;
                                    }
                                case EnrollmentErrorCodes.EnterpriseWebServiceCommunicationFailure:
                                    {
                                        MessageBox.ShowBox("MessageID207");
                                        Audit_Error("Enterprise WebService Communication Failure");
                                        break;
                                    }
                                case EnrollmentErrorCodes.RemoveFromPollingListFailure:
                                    {
                                        MessageBox.ShowBox("MessageID208");
                                        Audit_Error("Unable to remove from Polling list");
                                        break;
                                    }
                                case EnrollmentErrorCodes.ExchangeHostServiceNotRunning:
                                    {
                                        Audit_Error("Unable to remove from Polling list: Timeout occured");
                                        MessageBox.ShowBox("MessageID360", BMC_Icon.Error);
                                        break;
                                    }
                                case EnrollmentErrorCodes.Success:
                                    MessageBox.ShowBox("MessageID209");

                                    AuditViewerBusiness.InsertAuditData(new Audit.Transport.Audit_History
                                    {

                                        AuditModuleName = ModuleName.RemoveMachine,
                                        Audit_Screen_Name = "Position Details|Remove Machine",
                                        Audit_Desc = "Machine Removed from Position: "
                                                     + Convert.ToInt32(sBarPosName).ToString(),
                                        AuditOperationType = OperationType.MODIFY,
                                        Audit_Slot = lblAsset.Text,
                                        Audit_Field = "Position",
                                        Audit_Old_Vl = Convert.ToInt32(sBarPosName).ToString()
                                    });
                                    break;


                            }
                            return;

                            break;
                        }
                    case EnrollmentErrorCodes.Success:
                        MessageBox.ShowBox("MessageID209");

                        if (machineStatusFlag != 18)
                        {
                            AuditViewerBusiness.InsertAuditData(new Audit.Transport.Audit_History
                            {

                                AuditModuleName = ModuleName.RemoveMachine,
                                Audit_Screen_Name = "Position Details|Remove Machine",
                                Audit_Desc = "Machine Removed from Position: "
                                             + Convert.ToInt32(sBarPosName).ToString(),
                                AuditOperationType = OperationType.MODIFY,
                                Audit_Slot = _SlotMachine.AssetNumber,
                                Audit_Field = "Position",
                                Audit_Old_Vl = Convert.ToInt32(sBarPosName).ToString()
                            });
                        }
                        else
                        {
                            AuditViewerBusiness.InsertAuditData(new Audit.Transport.Audit_History
                            {

                                AuditModuleName = ModuleName.TransitMachine,
                                Audit_Screen_Name = "Position Details|Transit Machine",
                                Audit_Desc = string.Format("{0} - {1}", "Machine is in Transit for Site", siteCode),
                                AuditOperationType = OperationType.MODIFY,
                                Audit_Slot = _SlotMachine.AssetNumber,
                                Audit_Field = "Asset",
                                Audit_Old_Vl = string.Format("{0}|{1}", Settings.SiteCode, Convert.ToInt32(sBarPosName).ToString())
                            });
                        }

                        if (Exit != null)
                        {
                            Timer.Stop();
                            Exit.Invoke(this, new CancelEventArgs());
                        }
                        break;
                }
                #endregion
            }
            finally
            {
                Cursor = System.Windows.Input.Cursors.Arrow;
            }
        }



        private void rbMachineMaintenance_Click(object sender, RoutedEventArgs e)
        {
            LogManager.WriteLog("rbMachineMaintenance_Click", LogManager.enumLogLevel.Info);
            LoadScreen(PositionDetailsScreen.MachineMaintenance);
            isScreenLoaded = true;
        }

        private void rbReInstateMachine_Click(object sender, RoutedEventArgs e)
        {
            LogManager.WriteLog("rbReInstateMachine_Click", LogManager.enumLogLevel.Info);

            MachineManagerLazyInitializer manager = null;
            int nResult;
            try
            {

                var oDataContext =
                           new InstallationDataContext(oCommonUtilities.CreateInstance().GetConnectionString());
                //MachineManagerInterface machineManagerInterface = new MachineManagerInterface();
                manager = new MachineManagerLazyInitializer();

                if (!(Settings.NoWaitForDisableMachine))
                {
                    int nSuccess = manager.GetMachineManager().EnableMachineFromUI(_SlotMachine.InstallationNo);
                    if (nSuccess != 0)
                    {
                        MessageBox.ShowBox("MessageID362", BMC_Icon.Warning, _SlotMachine.AssetNo);
                        return;
                    }
                }

                //
                nResult = oDataContext.InsertReinstateMachine
                     (_SlotMachine.InstallationNo,
                         SecurityHelper.CurrentUser.SecurityUserID,
                         Convert.ToDouble("0"),
                         "Float Issued",
                         "FLOAT",
                         0,
                         0,
                         "SITE",
                         Settings.CD_Not_Use_Hoppers);

                if (nResult == 0)
                {
                    MessageBox.ShowBox("MessageID319", BMC_Icon.Information, BMC_Button.OK);
                    AuditViewerBusiness.InsertAuditData(new Audit.Transport.Audit_History
                    {
                        AuditModuleName = ModuleName.ReinstateMachine,
                        Audit_Screen_Name = "Position Details|Reinstate Machine",
                        Audit_Desc = "Machine Reinstated.",
                        AuditOperationType = OperationType.MODIFY,
                        Audit_Slot = _SlotMachine.AssetNumber
                    });

                }
                else
                {
                    MessageBox.ShowBox("MessageID320", BMC_Icon.Information, BMC_Button.OK);
                    AuditViewerBusiness.InsertAuditData(new Audit.Transport.Audit_History
                    {
                        AuditModuleName = ModuleName.ReinstateMachine,
                        Audit_Screen_Name = "Position Details|Reinstate Machine",
                        Audit_Desc = "Unable to Reinstate Machine.",
                        AuditOperationType = OperationType.MODIFY,
                        Audit_Slot = _SlotMachine.AssetNumber
                    });
                }

                if (Exit != null)
                {
                    Timer.Stop();
                    Exit.Invoke(this, new CancelEventArgs());
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

            finally
            {
                if (manager != null)
                {
                    manager.ReleaseMachineManager();
                    manager = null;
                }
            }

        }
        private void pnlContent_Loaded(object sender, RoutedEventArgs e)
        {
            LogManager.WriteLog("PositionDetails pnlContent_Loaded", LogManager.enumLogLevel.Info);
            var installationDataContext =
                    new InstallationDataContext(oCommonUtilities.CreateInstance().GetConnectionString());
            IList<GetBarPositionDetailsForCashDeskoperatorResult> InstallationDetailsResult;
            InstallationDetailsResult = installationDataContext.GetBarPositionDetailsForCashDeskoperator("", _SlotMachine.InstallationNo).ToList();

            foreach (var InstallationDetails in InstallationDetailsResult)
            {
                lblAsset.Text = InstallationDetails.Asset_No;
                lblGame.Text = InstallationDetails.Game_Name;
                lblBaseDenom.Text = (Convert.ToDecimal(InstallationDetails.Installation_Price_Of_Play) / 100).GetUniversalCurrencyFormatWithSymbol();
                lblPercentagePayout.Text = Math.Round(InstallationDetails.Anticipated_Percentage_Payout, 2).ToString();
                lblMaxBet.Text = InstallationDetails.Installation_MaxBet.ToString();
                lblSerialNo.Text = InstallationDetails.ActSerialNo;
                lblActAsset.Text = InstallationDetails.ActAssetNo;
                lblGMU.Text = InstallationDetails.GMUNo;

                if (InstallationDetails.Manufacturer != null)
                {
                    lblManufacturer.Text = ((InstallationDetails.Manufacturer).Length > 20) ? (InstallationDetails.Manufacturer).Substring(0, 20) : InstallationDetails.Manufacturer;
                    lblManufacturer.ToolTip = InstallationDetails.Manufacturer;
                }
                // lblManufacturer.Text = InstallationDetails.Manufacturer;
                if ((InstallationDetails.Zone_Name) != null)
                {
                    lblZoneName.Text = ((InstallationDetails.Zone_Name).Length > 20) ? (InstallationDetails.Zone_Name).Substring(0, 20) : InstallationDetails.Zone_Name;
                    lblZoneName.ToolTip = InstallationDetails.Zone_Name;
                }


                //lblZoneName.Text = InstallationDetails.Zone_Name;
                lblCreditValue.Text = (Convert.ToDecimal(InstallationDetails.Installation_Token_Value) / 100).GetUniversalCurrencyFormatWithSymbol();
                IsEventUncleared = (bool)InstallationDetails.UnClearedEvent;

                sBarPosName = InstallationDetails.Bar_Pos_Name;
                dtInstallationStartDate = (DateTime)InstallationDetails.InstallationStartDate;
                dtInstallationStartDateforCM = (DateTime)InstallationDetails.Start_Date;
                nInstallationFloatStatus = InstallationDetails.Installation_Float_Status;
                //txtRoute.Text = InstallationDetails.Route;
            }

            //if (Security.SecurityHelper.CurrentUser.UserName.ToUpper() == "BALLY" || Security.SecurityHelper.CurrentUser.UserName.ToUpper() == "CASH")
            //{
            //    rbHandpay.IsChecked = true;
            //    LoadScreen(PositionDetailsScreen.Handpay);
            //    return;
            //} //commented for CR 107,130

            RefreshSlot();
            //if (_SlotMachine.Status == SlotMachineStatus.GMUConnectivity)
            //{
            //    MessageBox.ShowBox("MessageID438");
            //}
            //else if ((rbEvents.Visibility == Visibility.Collapsed) && (rbFieldService.Visibility == Visibility.Collapsed) && (rbHandpay.Visibility == Visibility.Collapsed) && (rbMachineMaintenance.Visibility == Visibility.Collapsed)
            //       && (rbMachineMeters.Visibility == Visibility.Collapsed) && (rbPlayerClub.Visibility == Visibility.Collapsed) && (rbRemoveMachine.Visibility == Visibility.Collapsed) && (rbReInstateMachine.Visibility == Visibility.Collapsed)
            //       && (rbSyncTicketExpire.Visibility == Visibility.Collapsed) && (rbCurrentMeters.Visibility == Visibility.Collapsed) && (_SlotMachine.Status != SlotMachineStatus.FloatCollection))
            //{
            //    MessageBox.ShowBox("MessageID437");
            //    if (Exit != null)
            //    {
            //        Exit.Invoke(this, new CancelEventArgs());
            //    }
            //    return;
            //}

            if ((_SlotMachine.Status == SlotMachineStatus.CardedHandPay) || (_SlotMachine.Status == SlotMachineStatus.EmpCardedPlay) ||
                (_SlotMachine.Status == SlotMachineStatus.NonCardedHandPay) || (_SlotMachine.Status == SlotMachineStatus.CommsDown) ||
                (_SlotMachine.Status == SlotMachineStatus.GameDown))
            {
                if (Security.SecurityHelper.HasAccess("CashdeskOperator.FloorView.cs.Handpay"))
                {
                    rbHandpay.IsChecked = true;
                    LoadScreen(PositionDetailsScreen.Handpay);
                    isScreenLoaded = true;
                }
                else if (Security.SecurityHelper.HasAccess("CashdeskOperator.FloorView.cs.Events"))
                {
                    rbEvents.IsChecked = true;
                    LoadScreen(PositionDetailsScreen.Events);
                    isScreenLoaded = true;
                }
                return;
            }
            else if (IsEventUncleared && rbEvents.Visibility == Visibility.Visible)
            {
                rbEvents.IsChecked = true;
                LoadScreen(PositionDetailsScreen.Events);
                isScreenLoaded = true;
                return;
            }
            
            isFirstTime = false;
        }


        #endregion

        #region Methods

        public void RefreshSlot()
        {
            int oldIndex = 0;
            int _iHandpayCnt = 0;

            try
            {
                if (cViewHandpay != null)
                {
                    oldIndex = cViewHandpay.lstHandpay.SelectedIndex;
                    _iHandpayCnt = cViewHandpay.lstHandpay.Items.Count;
                }

                GetSlotMachineStatus();
                ShowHideControls(ucSlotMachine.Status);
                if (cViewHandpay != null)
                {
                    cViewHandpay.lstHandpay.SelectedIndex = ((oldIndex >= 0 && oldIndex < _iHandpayCnt) ? oldIndex : 0);
                }
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
        }

        public void GetSlotMachineStatus()
        {
            try
            {
                rbEvents.IsEnabled = true;
                rbFieldService.IsEnabled = true;
                rbHandpay.IsEnabled = true;
                rbMachineMeters.IsEnabled = true;
                rbPlayerClub.IsEnabled = true;
                rbRemoveMachine.IsEnabled = true;
                rbMachineMaintenance.IsEnabled = true;
                rbReInstateMachine.IsEnabled = true;
                rbSyncTicketExpire.IsEnabled = true;
                rbCurrentMeters.IsEnabled = true;

                var installationDataContext =
                    new InstallationDataContext(oCommonUtilities.CreateInstance().GetConnectionString());
                IList<FloorStatusData> barPositions;
                barPositions = installationDataContext.GetSlotStatus("", _SlotMachine.InstallationNo);

                if (barPositions.Count > 0)
                {
                    var position = barPositions[0];

                    if (position.Install_No != 0)
                    {
                        ucSlotMachine.FinalCollectionStatus = position.FinalCollectionStatus.HasValue ? position.FinalCollectionStatus.Value : 0;
                        ucSlotMachine.Status = (SlotMachineStatus)Enum.Parse(typeof(SlotMachineStatus), position.Slot_Status);
                        ucSlotMachine.StackerEventReceived = position.StackerEventReceived.HasValue ? position.StackerEventReceived.Value : false;
                        IsEventUncleared = (bool)position.UnClearedEvent;
                        ucSlotMachine.IsCollectable = position.IsCollectable < 1;
                    }
                    else
                        ucSlotMachine.Status = SlotMachineStatus.EmptyPosition;
                }
            }
            catch (Exception exception)
            {
                ExceptionManager.Publish(exception);
            }
            if (ucSlotMachine.StackerEventReceived)
            {
                ucSlotMachine.OuterRoundColor = Brushes.Goldenrod;
                ucSlotMachine.InnerRoundColor = Brushes.IndianRed;
            }
            else
            {
                ucSlotMachine.SetMachineStatus(ucSlotMachine.Status);
            }
        }
        /// <summary>
        /// Common Method for Access rights to buttons
        /// </summary>
        /// <param name="eButton"></param>
        /// <returns></returns>
        private bool HasAccess(PositionDetailsScreen eButton)
        {
            switch (eButton)
            {
                case PositionDetailsScreen.Handpay:
                    return Security.SecurityHelper.HasAccess("CashdeskOperator.FloorView.cs.Handpay");
                case PositionDetailsScreen.PlayerClub:
                    return Security.SecurityHelper.HasAccess("CashdeskOperator.FloorView.cs.PlayerClub");
                case PositionDetailsScreen.FieldService:
                    return Security.SecurityHelper.HasAccess("CashdeskOperator.FloorView.cs.FieldService");
                case PositionDetailsScreen.Events:
                    return Security.SecurityHelper.HasAccess("CashdeskOperator.FloorView.cs.Events");
                case PositionDetailsScreen.MachineMeters:
                    return Security.SecurityHelper.HasAccess("CashdeskOperator.FloorView.cs.MachineMeters");
                case PositionDetailsScreen.MachineMaintenance:
                    return Security.SecurityHelper.HasAccess("CashdeskOperator.Authorize.cs.MachineMaintenance");
                case PositionDetailsScreen.RemoveMachine:
                    return Security.SecurityHelper.HasAccess("CashdeskOperator.FloorView.cs.RemoveMachine");
                case PositionDetailsScreen.SyncTicketExpire:
                    return Security.SecurityHelper.HasAccess("CashdeskOperator.FloorView.cs.SyncTicketExpire");
                case PositionDetailsScreen.CurrentMeters:
                    return Security.SecurityHelper.HasAccess("CashdeskOperator.FloorView.cs.CurrentMeters");
                case PositionDetailsScreen.ReinstateMachine:
                    return Security.SecurityHelper.HasAccess("CashdeskOperator.FloorView.cs.ReinstateMachine");
                default:
                    return false;
            }
        }
        /// <summary>
        /// Common Method to Show Hide Controls based on SlotMachine Status
        /// </summary>
        /// <param name="eStatus"></param>
        private void ShowHideControls(SlotMachineStatus eStatus)
        {


            //Hide All Controls by default and then Toggle Visibility based on the Slot Status
            rbEvents.Visibility = Visibility.Collapsed;
            rbFieldService.Visibility = Visibility.Collapsed;
            rbHandpay.Visibility = Visibility.Collapsed;
            rbMachineMaintenance.Visibility = Visibility.Collapsed;
            rbMachineMeters.Visibility = Visibility.Collapsed;
            rbPlayerClub.Visibility = Visibility.Collapsed;
            rbRemoveMachine.Visibility = Visibility.Collapsed;
            rbReInstateMachine.Visibility = Visibility.Collapsed;
            rbSyncTicketExpire.Visibility = Visibility.Collapsed;
            rbCurrentMeters.Visibility = Visibility.Collapsed;
            if ((Settings.AllowMultipleDrops || Settings.Allow_Machine_Removal) && Settings.IsFinalDropRequiredForRemoval)
            {
                rbRemoveMachine.Visibility = Visibility.Visible;
            }
            if (!Settings.IsFinalDropRequiredForRemoval && (eStatus != SlotMachineStatus.VLTunderMaintenance
                                                                && eStatus != SlotMachineStatus.VLTunderUnauthorizedMaintenance))
            {
                rbRemoveMachine.Visibility = HasAccess(PositionDetailsScreen.RemoveMachine) ? Visibility.Visible : Visibility.Collapsed;
                rbReInstateMachine.Visibility = HasAccess(PositionDetailsScreen.ReinstateMachine) ? Visibility.Visible : Visibility.Collapsed;
                rbSyncTicketExpire.Visibility = Visibility.Collapsed;
                rbCurrentMeters.Visibility = Visibility.Collapsed;
            }

            switch (eStatus)
            {
                case SlotMachineStatus.CardedHandPay:
                case SlotMachineStatus.CardedPlay:
                case SlotMachineStatus.EmpCardedPlay:
                case SlotMachineStatus.NonCardedHandPay:
                case SlotMachineStatus.NonCardedPlay:
                case SlotMachineStatus.MachineInPlay:
                case SlotMachineStatus.NotInPlay:
                case SlotMachineStatus.CommsDown:
                case SlotMachineStatus.GameDown:
                    rbHandpay.Visibility = HasAccess(PositionDetailsScreen.Handpay) ? Visibility.Visible : Visibility.Collapsed;
                    rbPlayerClub.Visibility = HasAccess(PositionDetailsScreen.PlayerClub) ? Visibility.Visible : Visibility.Collapsed;
                    rbEvents.Visibility = HasAccess(PositionDetailsScreen.Events) ? Visibility.Visible : Visibility.Collapsed;
                    rbFieldService.Visibility = HasAccess(PositionDetailsScreen.FieldService) ? Visibility.Visible : Visibility.Collapsed;
                    rbMachineMeters.Visibility = HasAccess(PositionDetailsScreen.MachineMeters) ? Visibility.Visible : Visibility.Collapsed;
                    rbSyncTicketExpire.Visibility = HasAccess(PositionDetailsScreen.SyncTicketExpire) ? Visibility.Visible : Visibility.Collapsed;
                    rbCurrentMeters.Visibility = HasAccess(PositionDetailsScreen.CurrentMeters) ? Visibility.Visible : Visibility.Collapsed;

                    if (!isScreenLoaded)
                    {
                        if (rbHandpay.IsVisible)
                        {
                            rbHandpay.IsChecked = true;
                            LoadScreen(PositionDetailsScreen.Handpay);
                        }
                        else if (rbPlayerClub.IsVisible)
                        {
                            rbPlayerClub.IsChecked = true;
                            LoadScreen(PositionDetailsScreen.PlayerClub);
                        }
                        else if (rbEvents.IsVisible)
                        {
                            rbEvents.IsChecked = true;
                            LoadScreen(PositionDetailsScreen.Events);
                        }
                        else if (rbFieldService.IsVisible)
                        {
                            rbFieldService.IsChecked = true;
                            LoadScreen(PositionDetailsScreen.FieldService);
                        }
                        else if (rbMachineMeters.IsVisible)
                        {
                            rbMachineMeters.IsChecked = true;
                            LoadScreen(PositionDetailsScreen.MachineMeters);
                        }
                        else if (rbCurrentMeters.IsVisible)
                        {
                            rbCurrentMeters.IsChecked = true;
                            LoadScreen(PositionDetailsScreen.CurrentMeters);
                        }
                        else if (rbMachineMaintenance.IsVisible)
                        {
                            rbMachineMaintenance.IsChecked = true;
                            LoadScreen(PositionDetailsScreen.MachineMaintenance);
                        }
                    }
                    else
                    {
                        if (rbHandpay.IsChecked.SafeValue())
                        {
                            LoadScreen(PositionDetailsScreen.Handpay, true);
                        }
                    }

                    break;

                case SlotMachineStatus.FloatCollection:
                    CollectionHelper CollectionHelper = new CollectionHelper();
                    pnlContent.Children.Clear();
                    if (!CollectionHelper.HasUndeclaredCollecion(_SlotMachine.InstallationNo))
                    {
                        

                        rbRemoveMachine.Visibility = HasAccess(PositionDetailsScreen.RemoveMachine) ? Visibility.Visible : Visibility.Collapsed;

                        var installationDataContext =
                            new InstallationDataContext(oCommonUtilities.CreateInstance().GetConnectionString());
                        IList<FloorStatusData> barPositions;
                        barPositions = installationDataContext.GetSlotStatus("", _SlotMachine.InstallationNo);
                        if (barPositions.Count > 0)
                        {
                            var position = barPositions[0];
                            if (position.FinalCollectionStatus == 1)
                            {
                                rbReInstateMachine.Visibility = Visibility.Collapsed;
                            }
                            else
                            {
                                rbReInstateMachine.Visibility = HasAccess(PositionDetailsScreen.ReinstateMachine) ? Visibility.Visible : Visibility.Collapsed;
                            }

                        }
                    }
                    break;

                case SlotMachineStatus.EmptyPosition:
                //case SlotMachineStatus.GameInstallationAAMSPending:
                //case SlotMachineStatus.VLTInstallationAAMSPending:
                case SlotMachineStatus.InstallationCompletedNonMetered:
                    if (Exit != null)
                    {
                        Exit.Invoke(this, new CancelEventArgs());
                    }
                    break;

                case SlotMachineStatus.GoldClubCardedPlay:
                    break;

                //case SlotMachineStatus.VLTunderMaintenance:
                //case SlotMachineStatus.VLTunderUnauthorizedMaintenance:
                //    rbMachineMaintenance.Visibility = HasAccess(PositionDetailsScreen.MachineMaintenance) ? Visibility.Visible : Visibility.Collapsed;
                //    rbMachineMaintenance.IsChecked = rbMachineMaintenance.IsVisible;
                //    isScreenLoaded = true;
                //    if (!isScreenLoaded || isFirstTime)
                //    {
                //        LoadScreen(PositionDetailsScreen.MachineMaintenance);
                //        isFirstTime = false;
                //    }
                //    break;

                default:
                    break;
            }



        }

        private PositionDetailsScreen _selectedScreen = PositionDetailsScreen.FieldService;

        private void LoadScreen(PositionDetailsScreen detail)
        {
            LoadScreen(detail, false);
        }

        private void LoadScreen(PositionDetailsScreen detail, bool isRefresh)
        {
            try
            {
#if SKIP_SAME_PAGE_LOAD
                if (_selectedScreen == detail)
                {
                    LogManager.WriteLog("Same screen was selected again. Skipped loading.", LogManager.enumLogLevel.Info);
                    return;
                }
#endif
                if ((_selectedScreen != detail) &&
                    (detail == PositionDetailsScreen.Handpay))
                {
                    ((ICashDispenserStatusParent)this).ParentLoaded = false;
                }
                else
                {
                    ((ICashDispenserStatusParent)this).ParentLoaded = true;
                }
                LogManager.WriteLog("Inside LoadScreen", LogManager.enumLogLevel.Info);

                this.DisposeChildren(true);
                this.DisposeViewHandPay();
                _currentScreen = null;

                if (_SlotMachine.Status == SlotMachineStatus.EmptyPosition || _SlotMachine.Status == SlotMachineStatus.FloatCollection)
                    return;

                switch (detail)
                {
                    case PositionDetailsScreen.Handpay:
                        //RefreshSlot();
                        ViewHandPayPageCaller(isRefresh);
                        break;

                    case PositionDetailsScreen.PlayerClub:
                        CPositionDetailsPlayerClub PlayerClub = new CPositionDetailsPlayerClub(ucSlotMachine.InstallationNo);
                        pnlContent.Children.Add(PlayerClub);
                        PlayerClub.Margin = new Thickness(0);
                        break;
                    case PositionDetailsScreen.FieldService:

                        CFieldService fieldService = new CFieldService(sBarPosName, lblAsset.Text.ToString(), lblGame.Text.ToString(), lblManufacturer.Text, lblSerialNo.Text);
                        fieldService.Installation = _SlotMachine.InstallationNo;
                        pnlContent.Children.Add(fieldService);
                        fieldService.Margin = new Thickness(0);
                        break;

                    case PositionDetailsScreen.Events:
                        CPositionDetailsEvents posDetailsEvents = new CPositionDetailsEvents(sBarPosName, _SlotMachine.InstallationNo);
                        pnlContent.Children.Add(posDetailsEvents);
                        posDetailsEvents.Margin = new Thickness(0);
                        break;
                    case PositionDetailsScreen.EnrollMachine:
                        break;
                    case PositionDetailsScreen.MachineMeters:

                        CMeterLife meterLife = new CMeterLife(_SlotMachine.InstallationNo, dtInstallationStartDate);
                        pnlContent.Children.Add(meterLife);
                        meterLife.Margin = new Thickness(0);
                        break;
                    case PositionDetailsScreen.FillsCredits:
                        break;
                    case PositionDetailsScreen.CurrentMeters:

                        CCurrentMeters currentMeters = new CCurrentMeters(_SlotMachine.InstallationNo, dtInstallationStartDateforCM);
                        pnlContent.Children.Add(currentMeters);
                        currentMeters.Margin = new Thickness(0);
                        break;
                    //case PositionDetailsScreen.MachineMaintenance:
                    //    CMachineMaintenanceView objMachineMaintenanceView = new CMachineMaintenanceView(sBarPosName, ucSlotMachine.InstallationNo);
                    //    objMachineMaintenanceView.Margin = new Thickness(0);
                    //    pnlContent.Children.Add(objMachineMaintenanceView);
                    //    break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            finally
            {
                _selectedScreen = detail;
            }
        }

        private void DisposeViewHandPay()
        {
            if (!rbHandpay.IsChecked.SafeValue() &&
                cViewHandpay != null)
            {
                cViewHandpay.btnManual.Click -= btnManualClicked;
                cViewHandpay.btnCancel.Click -= btnCancelClicked;
                cViewHandpay = null;
            }
        }

        private void ViewHandPayPageCaller(bool isRefresh)
        {
            LogManager.WriteLog("Inside ViewHandPayPageCaller()", LogManager.enumLogLevel.Info);
            if (cViewHandpay != null)
            {
                if (!isRefresh)
                {
                    this.DisposeViewHandPay();
                }
                else
                {
                    if (ucSlotMachine.Status == SlotMachineStatus.FloatCollection)
                        cViewHandpay.btnManual.Visibility = Visibility.Collapsed;
                    cViewHandpay.Reload(sBarPosName, ucSlotMachine.InstallationNo);
                }
            }

            if (cViewHandpay == null)
            {
                cViewHandpay = new CViewHandpay(sBarPosName, this);
                cViewHandpay.InstallationNumber = ucSlotMachine.InstallationNo;
                if (ucSlotMachine.Status == SlotMachineStatus.FloatCollection)
                    cViewHandpay.btnManual.Visibility = Visibility.Collapsed;
                cViewHandpay.Margin = new Thickness(0);

                cViewHandpay.btnManual.Click += btnManualClicked;
                cViewHandpay.btnCancel.Click += btnCancelClicked;
            }
            pnlContent.Children.Add(cViewHandpay);
        }

        private void btnManualClicked(object sender, EventArgs e)
        {
            if (Timer != null)
                Timer.Stop();
        }

        private void btnCancelClicked(object sender, EventArgs e)
        {
            if (Timer != null)
                Timer.Start();
        }

        public SlotMachine SlotMachine
        {
            get
            {
                return _SlotMachine;
            }
            set
            {
                _SlotMachine = value;
                UpdateDetails();
            }
        }

        private void UpdateDetails()
        {
            ucSlotMachine.SlotNumber = _SlotMachine.SlotNumber;
            ucSlotMachine.FinalCollectionStatus = _SlotMachine.FinalCollectionStatus;
            ucSlotMachine.SetMachineStatus(_SlotMachine.Status);
            ucSlotMachine.IsHandPay = _SlotMachine.IsHandPay;
            ucSlotMachine.StackerEventReceived = _SlotMachine.StackerEventReceived;
            ucSlotMachine.InstallationNo = _SlotMachine.InstallationNo;
        }

        private void Audit_Error(string sDesc)
        {
            AuditViewerBusiness.InsertAuditData(new Audit.Transport.Audit_History
            {

                AuditModuleName = ModuleName.RemoveMachine,
                Audit_Screen_Name = "Position Details|Remove Machine",
                Audit_Desc = sDesc,
                AuditOperationType = OperationType.MODIFY,
                Audit_Slot = _SlotMachine.AssetNumber
            });
        }
        private void DisableControlsIfNoMeters()
        {
            rbEvents.IsEnabled = false;
            rbFieldService.IsEnabled = false;
            rbHandpay.IsEnabled = false;
            rbMachineMaintenance.IsEnabled = false;
            rbMachineMeters.IsEnabled = false;
            rbPlayerClub.IsEnabled = false;
            rbCurrentMeters.IsEnabled = false;
            rbSyncTicketExpire.IsEnabled = false;
            
            rbRemoveMachine.Focus();
            if (_SlotMachine.Status == SlotMachineStatus.ForceFinalCollection)
                rbReInstateMachine.IsEnabled = false;
            

        }
        private bool PerformLockCheck(int InstallationNo)
        {
            LogManager.WriteLog("Inside PerformLockCheck()", LogManager.enumLogLevel.Info);
            if (_lockHandler.GetLoclRecord(0, "", LockappMachineadmin, "POS", SlotMachine.SlotNumberString).Count() > 0)
            {
                MessageBox.ShowBox("MessageID81", BMC_Icon.Error);
                return false;
            }

            var lockRecords = _lockHandler.GetLoclRecord(0, "", "", "INST", InstallationNo.ToString());
            if (!(lockRecords.Count() <= 0))
            {
                MessageBox.ShowBox("MessageID82", BMC_Icon.Error);
                return false;
            }
            return true;
        }
        #endregion

        #region Dispose Children Members

        private void DisposeChildren(bool reclaimMemory)
        {
            try
            {
                int count = pnlContent.Children.Count;
                for (int i = count - 1; i >= 0; i--)
                {
                    IDisposable element = pnlContent.Children[i] as IDisposable;
                    if (element != null)
                    {
                        if (element is CViewHandpay)
                        {
                            LogManager.WriteLog("|=> CViewHandpay screen persisted.", LogManager.enumLogLevel.Info);
                        }
                        else
                        {
                            BMC.Presentation.Helper_classes.Common.DisposeObject(ref element, false);
                        }
                    }
                    pnlContent.Children.RemoveAt(i);
                }
                LogManager.WriteLog("|=> PosDetails pnlContent Children disposed successfully.", LogManager.enumLogLevel.Info);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
        #endregion

        #region IDisposable Members

        private bool disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    this.CleanupWPFObjectTopControls((i) =>
                    {
                        // events
                        this.btnExit.Click -= (this.btn_Exit);
                        this.rbHandpay.Click -= (this.Handpay_Click);
                        this.rbPlayerClub.Click -= (this.PlayerClub_Click);
                        this.rbFieldService.Click -= (this.FldService_Click);
                        this.rbEvents.Click -= (this.Events_Click);
                        this.rbMachineMeters.Click -= (this.MCMeters_Click);
                        this.rbRemoveMachine.Click -= (this.RemoveMachine_Click);
                        this.rbMachineMaintenance.Click -= (this.rbMachineMaintenance_Click);
                        this.rbReInstateMachine.Click -= (this.rbReInstateMachine_Click);
                        this.rbSyncTicketExpire.Click -= (this.SyncTicketExpire_Click);
                        this.pnlContent.Loaded -= (this.pnlContent_Loaded);
                        if (cViewHandpay != null)
                        {
                            cViewHandpay.btnManual.Click -= (this.btnManualClicked);
                            cViewHandpay.btnCancel.Click -= (this.btnCancelClicked);
                        }

                        if (Timer != null)
                        {
                            Timer.Stop();
                            Timer.Tick -= Timer_Tick;
                        }

                        this.DisposeChildren(true);
                    },
                    (c) =>
                    {
                    });
                    LogManager.WriteLog("|=> PosDetails objects are released successfully.", LogManager.enumLogLevel.Info);
                }
                disposed = true;
            }
        }

        ~PosDetails()
        {
            Dispose(false);
        }

        #endregion

        #region ICashDispenserStatusParent Members

        public bool ParentLoaded { get; set; }
        public DispenserText UpperDeckText { get; set; }
        public DispenserText LowerDeckText { get; set; }
        public string StatusText { get; set; }

        #endregion

        private void rbHandpay_Checked(object sender, RoutedEventArgs e)
        {

        }
    }

    #region Enumeration
    public enum PositionDetailsScreen
    {
        Handpay,
        PlayerClub,
        FieldService,
        Events,
        EnrollMachine,
        MachineMeters,
        MachineMaintenance,
        FillsCredits,
        RemoveMachine,
        ReinstateMachine,
        SyncTicketExpire,
        CurrentMeters
    }
    #endregion
}
