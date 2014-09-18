using System.ComponentModel;
using System.Windows;
using System.Windows.Threading;
using System.Threading;
using System.Windows.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BMC.CashDeskOperator.BusinessObjects;
using BMC.Transport;
using System.Data;
using BMC.Transport;
using BMC.Common.LogManagement;
using BMC.Common.ExceptionManagement;
using BMC.Presentation.POS.Helper_classes;
using Audit.BusinessClasses;
using Audit.Transport;
using BMC.Common.Utilities;
using BMC.CashDeskOperator;
using System.Globalization;

namespace BMC.Presentation.POS
{
    /// <summary>
    /// Interaction logic for EnrollmentWnd.xaml
    /// </summary>
    public partial class EnrollmentWnd : IDisposable
    {
        private string strKeyText = "";
        public event CancelEventHandler Exit;
        PositionDetails objPosDetails;
        bool EnterpriseSuccess;
        private BackgroundWorker bw = new BackgroundWorker();
        private BackgroundWorker bwGetAssetDetails = new BackgroundWorker();
        private String[] format = { "dd MMM yyyy ", "dd MMM yyyy HH:m:ss" };

        public EnrollmentWnd(string PositionName)
        {
            InitializeComponent();

            LogManager.WriteLog("Inside Constructor", LogManager.enumLogLevel.Info);

            objPosDetails = new PositionDetails() { Position = PositionName };
            lblPosition.Content = PositionName;
            lblStatus.Visibility = Visibility.Hidden;
            progressBar1.Visibility = Visibility.Hidden;
            if (Settings.RegulatoryEnabled && Settings.RegulatoryType == "AAMS")
            {
                chkInTransitAsset.Visibility = Visibility.Visible;
            }
            //txtBaseDenom.Text = "1";

            //if (BMC.Common.ConfigurationManagement.ConfigManager.Read("EnableDenomChange").ToUpper() == "FALSE")
            //    txtBaseDenom.IsEnabled = false;

            lblCoinType.Text = lblCoinType.Text + "(" + ExtensionMethods.GetCurrencyCoinSymbol() + "):";

            using (InstallationDataContext objCoinType = new InstallationDataContext(oCommonUtilities.CreateInstance().GetConnectionString()))
            {
                var vCoinType = (from CoinType in objCoinType.GetCoinType(ExtensionMethods.CurrentSiteCulture)
                                 select CoinType);

                cmbCoinType.DisplayMemberPath = "CoinType";
                cmbCoinType.SelectedValuePath = "CoinType";
                cmbCoinType.ItemsSource = vCoinType;

                if (cmbCoinType.Items.Count > 0)
                    cmbCoinType.SelectedIndex = 0;
            }

            bw.WorkerReportsProgress = true;
            bw.WorkerSupportsCancellation = true;
            bw.DoWork += new DoWorkEventHandler(bw_DoWork);
            bw.ProgressChanged += new ProgressChangedEventHandler(bw_ProgressChanged);
            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);

            bwGetAssetDetails.DoWork += new DoWorkEventHandler(bwGetAssetDetails_DoWork);
            bwGetAssetDetails.ProgressChanged += new ProgressChangedEventHandler(bwGetAssetDetails_ProgressChanged);
            bwGetAssetDetails.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwGetAssetDetails_RunWorkerCompleted);

            txtAsset.Focus();
        }

        void bwGetAssetDetails_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate
                    {
                        lblStatus.Visibility = Visibility.Hidden;
                        progressBar1.Visibility = Visibility.Hidden;
                        this.Cursor = Cursors.Arrow;
                        GetAccDetails.IsEnabled = true;
                    });
        }

        void bwGetAssetDetails_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

        }

        void bwGetAssetDetails_DoWork(object sender, DoWorkEventArgs e)
        {

            GetAssetDetailsFromEnterprise();

            Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate
                    {
                        GetAccDetails.IsEnabled = false;
                        lblStatus.Visibility = Visibility.Hidden;
                        progressBar1.Visibility = Visibility.Hidden;
                        this.Cursor = Cursors.Arrow;
                    });
        }



        void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate
                    {
                        lblStatus.Visibility = Visibility.Hidden;
                        progressBar1.Visibility = Visibility.Hidden;
                        this.Cursor = Cursors.Arrow;

                    });


        }

        void bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

        }

        void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            EnrollMachine();
            Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate
                    {
                        lblStatus.Visibility = Visibility.Hidden;
                        progressBar1.Visibility = Visibility.Hidden;
                        this.Cursor = Cursors.Arrow;
                    });
        }




        private void Enroll_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Enroll.IsEnabled = false;
                if (lblSerialNo.Content.ToString() == string.Empty)
                {
                    MessageBox.ShowBox("MessageID300", BMC_Icon.Information);
                    return;
                }

                if (isPositionLocked(objPosDetails.Position))
                    MessageBox.ShowBox("MessageID240", BMC_Icon.Error, BMC_Button.OK);
                else
                {
                    lblStatus.Visibility = Visibility.Visible;
                    progressBar1.Visibility = Visibility.Visible;
                    this.Cursor = Cursors.Wait;
                    bw.RunWorkerAsync();
                }
            }
            finally
            {
                Enroll.IsEnabled = true;
            }
        }



        private bool isPositionLocked(string sPosition)
        {
            var oDataContext =
                        new InstallationDataContext(oCommonUtilities.CreateInstance().GetConnectionString());
            LogManager.WriteLog(objPosDetails.Position, LogManager.enumLogLevel.Info);

            foreach (var Result in oDataContext.isPositionLocked(sPosition))
            {
                if (Result.Result == "LOCKED")
                    return true;
            }

            return false;
        }


        private void EnrollMachine()
        {
            Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate
            {
                try
                {
                    LogManager.WriteLog("Inside Enroll_Click", LogManager.enumLogLevel.Info);

                    int installationNo = 0;
                    if (Settings.ValidateGMUInSite && Settings.AGSValue.Trim() == Settings.DefaultGMUValue.Trim())
                    {
                        if (UpdateGMUNo.CreateInstance().CheckAGSCombination("", lblGMUNo.Content.ToString().Trim(), ""))
                        {
                            MessageBox.ShowBox("MessageID510", BMC_Icon.Information);
                            return;
                        }
                    }
					int agsValue = Convert.ToInt32(Settings.AGSValue.Trim());
                    if (UpdateGMUNo.CreateInstance().CheckAGSCombination(((agsValue & 8) == 8) ?  objPosDetails.ActAssetNo :"", ((agsValue & 16) == 16) ?  lblGMUNo.Content.ToString().Trim() : "", ((agsValue & 4) == 4) ?  objPosDetails.ActSerialNo : ""))
                    {
                         MessageBox.ShowBox("Enrollment_MessageID511", BMC_Icon.Information);
                         return;
                    }

                    GetAssetDetailsFromEnterprise();

                    if (EnterpriseSuccess)
                    {
                        if (lblSerialNo.Content.ToString() == string.Empty)
                        {
                            MessageBox.ShowBox("MessageID300", BMC_Icon.Information);
                            return;
                        }

                        LogManager.WriteLog("Inside If - EnterpriseSuccess", LogManager.enumLogLevel.Info);

                        //if (!ValidateControls())
                        //    return;

                        LogManager.WriteLog("Assigning values to object...", LogManager.enumLogLevel.Info);

                        //objPosDetails.CreditValue = Int32.Parse(txtCreditValue.Text.Trim());
                        objPosDetails.CreditValue = Convert.ToInt32(cmbCoinType.SelectedValue);
                        LogManager.WriteLog("CreditValue assigned", LogManager.enumLogLevel.Info);

                        objPosDetails.Jackpot = 0;// Int32.Parse(txtJackpot.Text.Trim()); ;
                        //LogManager.WriteLog("Jackpot value assigned", LogManager.enumLogLevel.Info);

                        LogManager.WriteLog("Invoking InstallMachine....", LogManager.enumLogLevel.Info);

                        EnrollmentErrorCodes ReturnValue = EnrollmentBusinessObject.CreateInstance().InstallMachine(objPosDetails, Security.SecurityHelper.CurrentUser.SecurityUserID, out installationNo);

                        LogManager.WriteLog(string.Format("Install Machine Completed with ReturnCode - {0}", ReturnValue), LogManager.enumLogLevel.Info);

                        switch (ReturnValue)
                        {
                            case EnrollmentErrorCodes.UpdateToOptionFileParameterFailure:
                                {
                                    try
                                    {
                                        LogManager.WriteLog(string.Format("Machine Removal due to  UpdateToOptionFileParameterFailure : Serial No:{0}, Installation No: (1) ", objPosDetails.SerialNo, objPosDetails.InstallationNo), LogManager.enumLogLevel.Info);
                                        //EnrollmentErrorCodes ErrorCode = EnrollmentBusinessObject.CreateInstance().RemoveMachine(objPosDetails.InstallationNo, 0, string.Empty);
                                    }
                                    catch (Exception ex)
                                    {
                                        LogManager.WriteLog("Machine Removal due to UpdateToOptionFileParameterFailure", LogManager.enumLogLevel.Info);
                                        ExceptionManager.Publish(ex);
                                    }
                                    ShowMessage("MessageID351");
                                    Audit_Error("Unable to update the Option File Parameter.");
                                    break;
                                }
                            case EnrollmentErrorCodes.AddToPollingListFailure:
                                {
                                    try
                                    {
                                        LogManager.WriteLog(string.Format("Machine Removal due to  AddToPollingListFailure : Serial No:{0}, Installation No: (1) ", objPosDetails.SerialNo, objPosDetails.InstallationNo), LogManager.enumLogLevel.Info);
                                        //EnrollmentErrorCodes ErrorCode = EnrollmentBusinessObject.CreateInstance().RemoveMachine(objPosDetails.InstallationNo, 0, string.Empty);
                                    }
                                    catch (Exception ex)
                                    {
                                        LogManager.WriteLog("Machine Removal due to  AddToPollingListFailure", LogManager.enumLogLevel.Info);
                                        ExceptionManager.Publish(ex);
                                    }
                                    ShowMessage("MessageID234");
                                    Audit_Error("Unable to Add to Polling list.");
                                    break;
                                }
                            case EnrollmentErrorCodes.ExchangeHostServiceNotRunning:
                                {
                                    try
                                    {
                                        LogManager.WriteLog(string.Format("Machine Removal due to  AddToPollingList TimeOut : Serial No:{0}, Installation No: (1) ", objPosDetails.SerialNo, objPosDetails.InstallationNo), LogManager.enumLogLevel.Info);
                                        //EnrollmentErrorCodes ErrorCode = EnrollmentBusinessObject.CreateInstance().RemoveMachine(objPosDetails.InstallationNo, 0, string.Empty);
                                    }
                                    catch (Exception ex)
                                    {
                                        LogManager.WriteLog("Machine Removal due to  AddToPollingList TimeOut ", LogManager.enumLogLevel.Info);
                                        ExceptionManager.Publish(ex);
                                    }
                                    ShowMessage("MessageID358");
                                    Audit_Error("Unable to Add to Polling list. Time Out Occured");
                                    break;
                                }
                            case EnrollmentErrorCodes.DatabaseError:
                                {
                                    ShowMessage("MessageID235");
                                    Audit_Error("Database Error.");
                                    break;
                                }
                            case EnrollmentErrorCodes.EnterpriseDatabaseError:
                                {
                                    ShowMessage("MessageID236");
                                    Audit_Error("Enterprise Database Error.");
                                    break;
                                }
                            case EnrollmentErrorCodes.EnterpriseWebServiceCommunicationFailure:
                                {
                                    ShowMessage("MessageID237");
                                    Audit_Error("Enterprise WebService Communication Failure.");
                                    break;
                                }
                            case EnrollmentErrorCodes.EnterpriseAssetInUse:
                                {
                                    ShowMessage("MessageID238");
                                    Audit_Error("Enterprise Asset in Use.");
                                    break;
                                }
                            case EnrollmentErrorCodes.EnterpriseAssetNotAvailable:
                                {
                                    ShowMessage("MessageID239");
                                    Audit_Error("Enterprise Asset not Availble.");
                                    break;
                                }
                            case EnrollmentErrorCodes.PositionLocked:
                                {
                                    ShowMessage("MessageID240");
                                    Audit_Error("Postion is Locked.");
                                    break;
                                }
                            case EnrollmentErrorCodes.LockExists:
                                {
                                    ShowMessage("MessageID240");
                                    Audit_Error("Lock Exists");
                                    break;
                                }
                            case EnrollmentErrorCodes.LockError:
                                {
                                    ShowMessage("MessageID240");
                                    Audit_Error("Lock Error.");
                                    break;
                                }
                            case EnrollmentErrorCodes.Success:
                                {
                                    MessageBox.ShowBox("MessageID241", BMC_Icon.Information);
                                    AuditViewerBusiness.InsertAuditData(new Audit.Transport.Audit_History
                                    {
                                        AuditModuleName = ModuleName.Enrollment,
                                        Audit_Screen_Name = "Enrollment|MachineInstallation",
                                        Audit_Desc = chkInTransitAsset.IsChecked == false ? "Machine Installed" : "Transit Machine Installed",
                                        AuditOperationType = OperationType.ADD,
                                        Audit_Field = "Position",
                                        Audit_New_Vl = objPosDetails.Position,
                                        Audit_Slot = objPosDetails.AssetNo
                                    });

                                    // Insert Hourly Records if the machine installation is successful
                                    ExecuteHourlyVTP(installationNo);
                                    EnrollmentBusinessObject.CreateInstance().UpdateHourlyStatsGamingday(installationNo.ToString());
                                    EnrollmentBusinessObject.CreateInstance().InsertIntoExportHistory(installationNo);
                                    CloseWindow();

                                }
                                break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    ExceptionManager.Publish(ex);
                }
            });
        }


        private void Audit_Error(string sDesc)
        {
            AuditViewerBusiness.InsertAuditData(new Audit.Transport.Audit_History
            {

                AuditModuleName = ModuleName.Enrollment,
                Audit_Screen_Name = "Enrollment|MachineInstallation",
                Audit_Desc = sDesc,
                AuditOperationType = OperationType.ADD,
                Audit_Field = "Position",
                Audit_New_Vl = objPosDetails.Position,
                Audit_Slot = objPosDetails.AssetNo
            });
        }


        private bool ValidateControls()
        {
            LogManager.WriteLog("Inside ValidateControls", LogManager.enumLogLevel.Info);

            LogManager.WriteLog("ValidateControls Successfull", LogManager.enumLogLevel.Info);

            return true;
        }

        private void ShowMessage(string MessageID)
        {
            LogManager.WriteLog("Inside ShowMessage", LogManager.enumLogLevel.Info);

            MessageBox.ShowBox(MessageID, BMC_Icon.Warning);

        }

        private void GetAccDetails_Click(object sender, RoutedEventArgs e)
        {
            if (txtAsset.Text.Trim().Length > 0)
            {
                lblStatus.Visibility = Visibility.Visible;
                progressBar1.Visibility = Visibility.Visible;
                this.Cursor = Cursors.Wait;
                GetAccDetails.IsEnabled = false;
                bwGetAssetDetails.RunWorkerAsync();
            }
            else
            {
                MessageBox.ShowBox("MessageID246", BMC_Icon.Error);
            }
        }

        private void GetAssetDetailsFromEnterprise()
        {
            Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate
            {
                try
                {
                    LogManager.WriteLog("Inside GetAccDetails_Click", LogManager.enumLogLevel.Info);

                    // Get details from Enterprise for the asset
                    if (txtAsset.Text.Trim().Length > 0)
                    {
                        LogManager.WriteLog("Invoking GetAssetDetails method", LogManager.enumLogLevel.Info);

                        DataTable dtAsset = EnrollmentBusinessObject.CreateInstance().GetAssetDetails(txtAsset.Text.Trim(), Settings.SiteCode);

                        LogManager.WriteLog("GetAssetDetails Success", LogManager.enumLogLevel.Info);

                        if (dtAsset != null)
                        {
                            LogManager.WriteLog("Asset details available", LogManager.enumLogLevel.Info);

                            LogManager.WriteLog("Asset details - " + dtAsset.Rows.Count.ToString(), LogManager.enumLogLevel.Info);
                            if (dtAsset.Rows.Count > 0)
                                switch (Int32.Parse(dtAsset.Rows[0][0].ToString()))
                                {
                                    case -1: //asset not exists
                                        LogManager.WriteLog("Inside Case -1", LogManager.enumLogLevel.Info);
                                        ShowMessage("MessageID239");
                                        EnterpriseSuccess = false;
                                        txtAsset.Text = string.Empty;
                                        //TODO: Message Box  
                                        lblSerialNo.Content = "";
                                        lblGameTitle.Content = "";
                                        tbMachineType.Text = "";
                                        lblAltSrialNo.Content = "";
                                        lblGMUNo.Content = "";
                                        return;
                                    case -2: //asset is IN USE
                                        LogManager.WriteLog("Inside Case -2", LogManager.enumLogLevel.Info);
                                        ShowMessage("MessageID238");
                                        EnterpriseSuccess = false;
                                        txtAsset.Text = string.Empty;
                                        //TODO: Message Box
                                        lblSerialNo.Content = "";
                                        lblGameTitle.Content = "";
                                        tbMachineType.Text = "";
                                        lblAltSrialNo.Content = "";
                                        lblGMUNo.Content = "";
                                        return;
                                    case -3: //asset is in Transit
                                        LogManager.WriteLog("Inside Case -3", LogManager.enumLogLevel.Info);
                                        ShowMessage("MessageID341");
                                        EnterpriseSuccess = false;
                                        txtAsset.Text = string.Empty;
                                        //TODO: Message Box
                                        lblSerialNo.Content = "";
                                        lblGameTitle.Content = "";
                                        tbMachineType.Text = "";
                                        lblAltSrialNo.Content = "";
                                        lblGMUNo.Content = "";
                                        return;
                                    case 0:
                                        LogManager.WriteLog("Inside Case 0", LogManager.enumLogLevel.Info);
                                        string AGSSerial = string.Empty;
                                        EnterpriseSuccess = true;
                                        objPosDetails.AssetNo = txtAsset.Text.Trim();

                                        lblSerialNo.Content = dtAsset.Rows[0]["SerialNo"].ToString();
                                        objPosDetails.SerialNo = dtAsset.Rows[0]["SerialNo"].ToString();
                                        lblGameTitle.Content = objPosDetails.Game = dtAsset.Rows[0]["Game"].ToString(); //MachineName
                                        tbMachineType.Text = objPosDetails.GameCode = dtAsset.Rows[0]["GameCode"].ToString();//MachineTypeCode
                                        lblAltSrialNo.Content = objPosDetails.AltSerialNo = dtAsset.Rows[0]["AltSerialNo"].ToString();
                                        objPosDetails.Manufacturer = dtAsset.Rows[0]["Manufacturer_Name"].ToString();
                                        objPosDetails.ActAssetNo = dtAsset.Rows[0]["ActAssetNo"].ToString();
                                        objPosDetails.GMUNo = dtAsset.Rows[0]["GMUNo"].ToString();
                                        lblGMUNo.Content = objPosDetails.GMUNo;
                                        objPosDetails.ActSerialNo = dtAsset.Rows[0]["ActSerialNo"].ToString();
                                        objPosDetails.EnrolmentFlag = int.Parse(dtAsset.Rows[0]["EnrolmentFlag"].ToString());
                                        objPosDetails.CMPGameType = dtAsset.Rows[0]["CMPGameType"].ToString();
                                        objPosDetails.isMultiGame = Convert.ToBoolean(dtAsset.Rows[0]["isMultiGame"].ToString()) ? 1 : 0;
                                        objPosDetails.GetGameDetails = Convert.ToBoolean(dtAsset.Rows[0]["GetGameDetails"].ToString());
                                        objPosDetails.IsDefaultAssetDetail = Convert.ToBoolean(dtAsset.Rows[0]["IsDefaultAssetDetail"]);
                                        objPosDetails.BaseDenom = int.Parse(dtAsset.Rows[0]["Base_Denom"].ToString());
                                        objPosDetails.PercentagePayout = Convert.ToSingle(dtAsset.Rows[0]["Percentage_Payout"].ToString());
                                        objPosDetails.OccupancyHour = Convert.ToInt32(dtAsset.Rows[0]["OccupanyHour"].ToString());
                                        objPosDetails.AssetDisplayName = Convert.ToString(dtAsset.Rows[0]["AssetDisplayName"].ToString());
                                        objPosDetails.GameTypeCode = Convert.ToString(dtAsset.Rows[0]["GameTypeCode"].ToString());

                                        if (objPosDetails.EnrolmentFlag == 1)
                                        {
                                            tb_AssetNo.Text = Application.Current.FindResource("PosDetails_xaml_TextBlock_3").ToString();
                                        }
                                        else if (objPosDetails.EnrolmentFlag == 2)
                                        {

                                            tb_AssetNo.Text = Application.Current.FindResource("MachineRemoval_xaml_TextBlock_4").ToString();
                                        }
                                        else
                                        {
                                            tb_AssetNo.Text = Application.Current.FindResource("EnrollmentWnd_xaml_SerialNo").ToString();
                                        }
                                        break;
                                }
                            else
                                ShowMessage("MessageID237");
                        }
                        else
                        {
                            LogManager.WriteLog("Asset details not available", LogManager.enumLogLevel.Error);
                        }
                    }
                    else
                    {
                        EnterpriseSuccess = false;
                        lblSerialNo.Content = "";
                        lblGameTitle.Content = "";
                        tbMachineType.Text = "";
                        lblAltSrialNo.Content = "";
                        lblGMUNo.Content = "";
                    }
                }
                catch (Exception ex)
                {
                    ExceptionManager.Publish(ex);
                    GetAccDetails.IsEnabled = true;
                }
            });
        }

        private void ExecuteHourlyVTP(int installationNo)
        {
            DateTime dtTheDateTime;
            int iTheHour = 0;
            int iReturnValue = -1;

            try
            {
                LogManager.WriteLog("Inside ExecuteHourlyVTP", LogManager.enumLogLevel.Info);

                if (DateTime.Now.Hour == 0)
                {
                    dtTheDateTime = DateTime.Today.Date.AddDays(-1);
                    dtTheDateTime.Date.ToString(format[0], DateTimeFormatInfo.InvariantInfo);
                    iTheHour = 24;
                    LogManager.WriteLog("Started hourly for datetime= " + dtTheDateTime.ToString() + " Started hourly for Hour= " + iTheHour.ToString(), LogManager.enumLogLevel.Info);
                }
                else
                {
                    dtTheDateTime = DateTime.Now;
                    dtTheDateTime.ToString(format[1], DateTimeFormatInfo.InvariantInfo);
                    iTheHour = DateTime.Now.Hour;
                    LogManager.WriteLog("Started hourly for datetime= " + dtTheDateTime.ToString() + " Started hourly for Hour= " + iTheHour.ToString(), LogManager.enumLogLevel.Info);
                }

                iReturnValue = EnrollmentBusinessObject.CreateInstance().ExecuteHourlyVTP(installationNo, dtTheDateTime, iTheHour, false);

                switch (iReturnValue)
                {
                    case 0:
                        LogManager.WriteLog("Hourly for DPNo=" + installationNo + " -Passed!", LogManager.enumLogLevel.Info);
                        break;
                    case 1:
                        LogManager.WriteLog("Hourly for  DPNo= " + installationNo + " -Invalid Installation!", LogManager.enumLogLevel.Info);
                        break;
                    case 2:
                        LogManager.WriteLog("Hourly for DPNo= " + installationNo + " -Invalid Installation!", LogManager.enumLogLevel.Info);
                        break;
                    default:
                        LogManager.WriteLog("Hourly for DPNo= " + installationNo + " -'Other' Error-", LogManager.enumLogLevel.Info);
                        break;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btnExit_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                LogManager.WriteLog("Inside btnExit_Click_1", LogManager.enumLogLevel.Info);

                if (Exit != null)
                {
                    Exit.Invoke(this, new CancelEventArgs());
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
        private void CloseWindow()
        {
            if (Exit != null)
            {
                Exit.Invoke(this, new CancelEventArgs());
            }

        }
        #region Keyboard events

        void objKeyboard_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                LogManager.WriteLog("Inside objKeyboard_Closing", LogManager.enumLogLevel.Info);

                if (((KeyboardInterface)sender).DialogResult == true)
                {
                    strKeyText = ((KeyboardInterface)sender).KeyString;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private string DisplayKeyboard(string KeyText, string type)
        {
            try
            {
                LogManager.WriteLog("Inside DisplayKeyboard", LogManager.enumLogLevel.Info);
                strKeyText = "";
                KeyboardInterface objKeyboard = new KeyboardInterface();
                if (type == "Pwd")
                {
                    objKeyboard.IsPwd = true;
                }
                objKeyboard.Closing += new System.ComponentModel.CancelEventHandler(objKeyboard_Closing);
                objKeyboard.KeyString = KeyText;
                Point locationFromScreen = this.PointToScreen(new Point(0, 0));
                PresentationSource source = PresentationSource.FromVisual(this);
                System.Windows.Point targetPoints = source.CompositionTarget.TransformFromDevice.Transform(locationFromScreen);
                objKeyboard.Top = targetPoints.Y + this.Height / 2;
                objKeyboard.Left = targetPoints.X;
                objKeyboard.ShowDialogEx(this);
                return strKeyText;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return string.Empty;
            }
        }

        #endregion

        private void txtAsset_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                LogManager.WriteLog("Inside txtAsset_PreviewMouseUp", LogManager.enumLogLevel.Info);
                if (!BMC.Transport.Settings.OnScreenKeyboard)
                    return;
                txtAsset.Text = DisplayKeyboard(string.Empty, string.Empty);
                txtAsset.SelectAll();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void txtBaseDenom_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            //try
            //{
            //    LogManager.WriteLog("Inside txtBaseDenom_PreviewMouseUp", LogManager.enumLogLevel.Info);
            //    if (!BMC.Transport.Settings.OnScreenKeyboard)
            //        return;
            //    txtBaseDenom.Text = DisplayKeyboard(string.Empty, string.Empty);
            //    txtBaseDenom.SelectAll();
            //}
            //catch (Exception ex)
            //{
            //    ExceptionManager.Publish(ex);
            //}
        }

        private void txtCreditValue_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                LogManager.WriteLog("Inside txtCreditValue_PreviewMouseUp", LogManager.enumLogLevel.Info);
                if (!BMC.Transport.Settings.OnScreenKeyboard)
                    return;
                txtCreditValue.Text = DisplayKeyboard(string.Empty, string.Empty);
                txtCreditValue.SelectAll();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void txtPercentagePayout_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                LogManager.WriteLog("Inside txtPercentagePayout_PreviewMouseUp", LogManager.enumLogLevel.Info);
                if (!BMC.Transport.Settings.OnScreenKeyboard)
                    return;
                txtPercentagePayout.Text = DisplayKeyboard(string.Empty, string.Empty);
                txtPercentagePayout.SelectAll();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

        }

        private void txtJackpot_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                LogManager.WriteLog("Inside txtJackpot_PreviewMouseUp", LogManager.enumLogLevel.Info);
                if (!BMC.Transport.Settings.OnScreenKeyboard)
                    return;
                txtJackpot.Text = DisplayKeyboard(string.Empty, string.Empty);
                txtJackpot.SelectAll();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void chkInTransitAsset_Checked(object sender, RoutedEventArgs e)
        {
            string assetNo = string.Empty;

            try
            {
                this.Cursor = System.Windows.Input.Cursors.Wait;

                LogManager.WriteLog("Inside chkInTransitAsset_Checked", LogManager.enumLogLevel.Info);

                CTransitAsset cTransitAsset = new CTransitAsset();
                cTransitAsset.ShowDialogEx(this);

                txtAsset.Text = cTransitAsset.TransitAssetNo;

                if (txtAsset.Text == string.Empty)
                    chkInTransitAsset.IsChecked = false;
                else
                    GetAccDetails_Click(sender, e);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return;
            }
            finally
            {
                this.Cursor = System.Windows.Input.Cursors.Arrow;
            }
        }

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
                        this.btnExit.Click -= (this.btnExit_Click_1);
                        this.txtAsset.PreviewMouseUp -= (this.txtAsset_PreviewMouseUp);
                        this.chkInTransitAsset.Checked -= (this.chkInTransitAsset_Checked);
                        this.GetAccDetails.Click -= (this.GetAccDetails_Click);
                        this.txtBaseDenom.PreviewMouseUp -= (this.txtBaseDenom_PreviewMouseUp);
                        this.txtCreditValue.PreviewMouseUp -= (this.txtCreditValue_PreviewMouseUp);
                        this.txtJackpot.PreviewMouseUp -= (this.txtJackpot_PreviewMouseUp);
                        this.Enroll.Click -= (this.Enroll_Click);

                        // others
                        progressBar1.ClearTriggers();
                        WPFPerfMethods.Cleanup_TextBoxStyle1(
                            txtGMUNo, textBox1,
                            txtAsset, txtBaseDenom,
                            txtCreditValue, txtPercentagePayout,
                            txtJackpot, txtMaxBet);
                    },
                    (c) =>
                    {
                    });
                    LogManager.WriteLog("|=> EnrollmentWnd objects are released successfully.", LogManager.enumLogLevel.Info);
                }
                disposed = true;
            }
        }

        ~EnrollmentWnd()
        {
            Dispose(false);
        }

        #endregion

        private void ucEnrollmentWnd_Loaded(object sender, RoutedEventArgs e)
        {
            txtAsset.Focus();
        }
    }
}
