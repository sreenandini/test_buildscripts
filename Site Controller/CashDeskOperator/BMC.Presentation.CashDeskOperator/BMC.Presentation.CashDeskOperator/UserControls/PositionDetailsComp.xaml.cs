using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using BMC.Common.ExceptionManagement;
using BMC.Presentation.POS;
using BMC.Transport.CashDeskOperatorEntity;
using BMC.CashDeskOperator.BusinessObjects;
using BMC.Presentation.POS.Helper_classes;

namespace BMC.Presentation
{
    /// <summary>
    /// Interaction logic for PositionDetailsComp.xaml
    /// </summary>
    public partial class PositionDetailsComp : IDisposable
    {
        //BGSMachineManagerNET.MachineManager MachineManager = new BGSMachineManagerNET.MachineManager();

        IAnalysis analysisBusinessObject = AnalysisBusinessObject.CreateInstance();
        public event CancelEventHandler Exit;
        private PositionDetail _POSDetail;

        public const string TREASURY_HANDPAY_JACKPOT = "Handpay Jackpot";
        public const string TREASURY_HANDPAY_CREDIT = "Handpay Credit";
        public const string TREASURY_SHORTPAY = "Shortpay";
        public const string TREASURY_CASH_DESK_FLOAT = "Cash Desk Float";
        public const string TREASURY_FLOAT = "Float";
        public const string TREASURY_PROG = "Prog";
        public const string TREASURY_VOID = "VOID";

        int iTreasuryID = 0;

        public PositionDetail POSDetail
        {
            get { return _POSDetail; }
            set
            {
                _POSDetail = value;
                lblAsset.Content = _POSDetail.AssetNumber;
                lblDate.Content = _POSDetail.HandPayDate;
                lblGame.Content = _POSDetail.GameName;
                lblPosition.Content = _POSDetail.PostionNumber;
                lblTime.Content = _POSDetail.HandPayTime;
                lblHandPayType.Content = _POSDetail.HandPayType;
                if (string.IsNullOrEmpty(_POSDetail.HandPayValue))
                {
                    lblHandPayValue.Content = "None";
                    lblDate.Content = "None";
                    lblTime.Content = "None";
                    lblHandPayType.Content = "None";
                    btnHandPayProcess.IsEnabled = false;
                }
                else
                {
                    lblHandPayValue.Content = CommonBusinessObjects.GetCurrency(Convert.ToDouble(_POSDetail.HandPayValue));
                }

                DataTable dt = new DataTable();
                dt = analysisBusinessObject.GetEPIDetails(_POSDetail.InstallationNo);

                foreach (DataRow dr in dt.Rows)
                {
                    if (BMC.Presentation.Helper_classes.Common.GetRowValue<string>(dr, "IsEPIAvailable").ToString() == "1")
                    {
                        _POSDetail.IsEPIAvailable = true;
                        _POSDetail.PlayerAccountNumber = BMC.Presentation.Helper_classes.Common.GetRowValue<string>(dr, "EPIDetails").ToString();
                        Dictionary<string, string> PlayerInfo = (PlayerInformationBusinessObject.CreateInstance()).GetPlayerInfo(_POSDetail.PlayerAccountNumber);
                        if (PlayerInfo != null)
                        {
                            string PlayerName;
                            string PlayerStatus;
                            TimeSpan CardTimeIn = new TimeSpan(0, 0, 0);
                            if (PlayerInfo.TryGetValue("DisplayName", out PlayerName))
                                _POSDetail.PlayerName = PlayerName;

                            if (PlayerInfo.TryGetValue("ClubState", out PlayerStatus))
                                _POSDetail.PlayerClubStatus = PlayerStatus;

                            DateTime CardedTime;
                            if (DateTime.TryParse(BMC.Presentation.Helper_classes.Common.GetRowValue<string>(dr, "CardinTime").ToString(), out CardedTime))
                                CardTimeIn = DateTime.Now.Subtract(CardedTime);

                            _POSDetail.PlayerTimeOfPlay += ((CardTimeIn.Hours / 60) + CardTimeIn.Minutes).ToString() + " : " + CardTimeIn.Seconds.ToString();

                        }
                    }
                    else
                        _POSDetail.IsEPIAvailable = false;
                    break;
                }


                if (_POSDetail.IsEPIAvailable)
                {
                    lblPlayerAccount.Content = _POSDetail.PlayerAccountNumber;
                    lblPlayerName.Content = _POSDetail.PlayerName;
                    lblPlayerStatus.Content = _POSDetail.PlayerClubStatus;
                    lblPlayerTimeOfPlay.Content = _POSDetail.PlayerTimeOfPlay;
                }
                else
                {
                    lblPlayerAccount.Content = "None";
                    lblPlayerName.Content = "None";
                    lblPlayerStatus.Content = "None";
                    lblPlayerTimeOfPlay.Content = "None";
                }

                if (_POSDetail.GameName.ToString().Trim() == String.Empty)
                    btnEnrol.Content = "Install Machine";
                else
                    btnEnrol.Content = "Remove Machine";

            }
        }

        public string PositionText
        {
            get { return (string)GetValue(PositionTextProperty); }
            set { SetValue(PositionTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PositionText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PositionTextProperty =
            DependencyProperty.Register("PositionText", typeof(string), typeof(PositionDetailsComp), new UIPropertyMetadata(string.Empty));


        public bool IsHandPay
        {
            get { return (bool)GetValue(IsHandPayProperty); }
            set { SetValue(IsHandPayProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsHandPay.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsHandPayProperty =
            DependencyProperty.Register("IsHandPay", typeof(bool), typeof(PositionDetailsComp), new UIPropertyMetadata(false, new PropertyChangedCallback(IsHandPayCallback)));



        public Brush StatusColor
        {
            get { return (Brush)GetValue(StatusColorProperty); }
            set { SetValue(StatusColorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for StatusColor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StatusColorProperty =
            DependencyProperty.Register("StatusColor", typeof(Brush), typeof(PositionDetailsComp), new UIPropertyMetadata(Brushes.Red));


        public PositionDetailsComp()
        {
            this.InitializeComponent();

        }


        public PositionDetailsComp(PositionDetail PositionDetail)
        {
            this.Loaded += new RoutedEventHandler(PositionDetailsComp_Loaded);
            this.InitializeComponent();
            _POSDetail = PositionDetail;




        }

        void PositionDetailsComp_Loaded(object sender, RoutedEventArgs e)
        {
            lblAsset.Content = _POSDetail.AssetNumber;
            lblDate.Content = _POSDetail.HandPayDate;
            lblGame.Content = _POSDetail.GameName;
            lblPosition.Content = _POSDetail.PostionNumber;
            lblTime.Content = _POSDetail.HandPayTime;
            lblHandPayType.Content = _POSDetail.HandPayType;
            if (string.IsNullOrEmpty(_POSDetail.HandPayValue))
            {
                lblHandPayValue.Content = "";
            }
            else
            {
                lblHandPayValue.Content = CommonBusinessObjects.GetCurrency(Convert.ToDouble(_POSDetail.HandPayValue));
            }
            lblPlayerAccount.Content = _POSDetail.PlayerAccountNumber;
            lblPlayerName.Content = _POSDetail.PlayerName;
            lblPlayerStatus.Content = _POSDetail.PlayerClubStatus;
            lblPlayerTimeOfPlay.Content = _POSDetail.PlayerTimeOfPlay;

            if (_POSDetail.GameName.ToString().Trim() == String.Empty)
                btnEnrol.Content = "Install Machine";
            else
                btnEnrol.Content = "Remove Machine";
        }

        private void btn_Exit(object sender, RoutedEventArgs e)
        {
            if (Exit != null)
            {
                Exit.Invoke(this, new CancelEventArgs());
            }
        }

        private static void IsHandPayCallback(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            if ((bool)e.NewValue == false)
            {
                ((PositionDetailsComp)source).pnlHandpay.Visibility = Visibility.Collapsed;
            }
        }

        private void btnHandPayProcess_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                btnHandPayProcess.IsEnabled = false;
                //Clearhandpay(_POSDetail.InstallationNo);
                this.Cursor = Cursors.Wait;
                //System.Windows.Forms.Application.DoEvents();
                SaveDetails_EventsFromConnexus(false);
                this.Cursor = Cursors.Arrow;
            }
            finally
            {
                btnHandPayProcess.IsEnabled = true;
            }
        }

        private void SaveDetails_EventsFromConnexus(bool IsVoid)
        {
            string strTEID = string.Empty;
            string strTreasuryType = string.Empty;
            //float fTreasuryAmount = 0.00F;
            double fTreasuryAmount;

            Clearhandpay(_POSDetail.InstallationNo);
            try
            {
                Treasury objHandpayEntity = new Treasury();
                IHandpay objCashDeskOperator = HandpayBusinessObject.CreateInstance();

                strTreasuryType = TREASURY_HANDPAY_CREDIT;

                //Get the type of treasury event
                if (_POSDetail.HandPayType == "HandPay")
                {
                    strTreasuryType = TREASURY_HANDPAY_CREDIT; ;
                }
                else if (_POSDetail.HandPayType == "Jackpot")
                {
                    strTreasuryType = TREASURY_HANDPAY_JACKPOT;
                }
                else if (_POSDetail.HandPayType == "Progressive")
                {
                    strTreasuryType = TREASURY_PROG;
                }

                //Save the treasury details in treasury table.
                objHandpayEntity.TreasuryType = strTreasuryType;

                objHandpayEntity.CollectionNumber = 0;
                objHandpayEntity.InstallationNumber = _POSDetail.InstallationNo;
                objHandpayEntity.UserID = Security.SecurityHelper.CurrentUser.SecurityUserID;
                objHandpayEntity.TreasuryReason = "";
                objHandpayEntity.TreasuryReasonCode = 0;
                objHandpayEntity.TreasuryAllocated = 0;
                objHandpayEntity.TreasuryBreakdown100p = 0;
                objHandpayEntity.TreasuryBreakdown10p = 0;
                objHandpayEntity.TreasuryBreakdown200p = 0;
                objHandpayEntity.TreasuryBreakdown20p = 0;
                objHandpayEntity.TreasuryBreakdown2p = 0;
                objHandpayEntity.TreasuryBreakdown50p = 0;
                objHandpayEntity.TreasuryBreakdown5p = 0;
                objHandpayEntity.TreasuryIssuerUserNo = Security.SecurityHelper.CurrentUser.SecurityUserID;
                objHandpayEntity.TreasuryMembershipNo = "0";

                objHandpayEntity.TreasuryAmount = double.Parse(_POSDetail.HandPayValue.ToString());
                fTreasuryAmount = objHandpayEntity.TreasuryAmount;

                iTreasuryID = objCashDeskOperator.ProcessHandPay(objHandpayEntity);

                DataTable dt = new DataTable();
                dt = analysisBusinessObject.GetTicketException(_POSDetail.InstallationNo);

                foreach (DataRow dr in dt.Rows)
                {
                    strTEID = BMC.Presentation.Helper_classes.Common.GetRowValue<int>(dr, "TE_ID").ToString();
                    break;
                }


                if (objCashDeskOperator.UpdateTicketException(strTEID) == false)
                {
                    BMC.Common.LogManagement.LogManager.WriteLog("Error while updating ticket exception table", BMC.Common.LogManagement.LogManager.enumLogLevel.Info);
                }

                //Create a receipt with -ve amount incase of a void transaction
                if (IsVoid == true) { fTreasuryAmount = -fTreasuryAmount; }
                //Print the receipt for handpay.

                if (strTreasuryType == TREASURY_HANDPAY_CREDIT)
                {
                    strTreasuryType = "Handpay";
                }
                (oCommonUtilities.CreateInstance()).PrintCommonReceipt(false, strTreasuryType, "");

                if (IsVoid == false)
                {
                    MessageBox.ShowBox("MessageID57", BMC_Icon.Information);
                }

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

        private bool Clearhandpay(int InstallationNo)
        {
            int Datapak;
            DataTable dt = new DataTable();
            dt = analysisBusinessObject.GetHandpaystoClear(InstallationNo);
            
            foreach (DataRow dr in dt.Rows)
            {
                Datapak = BMC.Presentation.Helper_classes.Common.GetRowValue<int>(dr, "Datapak_No");
                //return MachineManager.ClearHandpayLock(Datapak);
                System.Diagnostics.Process proc = new System.Diagnostics.Process();
                proc.StartInfo.FileName="CmdLine";
                proc.StartInfo.Arguments = Datapak.ToString();
                proc.Start();
            }
            return false;
        }

        void EXClas_ACK(ComExchangeLib.MessageAck MessageAck)
        {
            if (MessageAck.ACK)
            {
                bool IsVoid = false;
                string strTEID = string.Empty;
                string strTreasuryType = string.Empty;
                //float fTreasuryAmount = 0.00F;
                double fTreasuryAmount;
                try
                {
                    Treasury objHandpayEntity = new Treasury();
                    IHandpay objCashDeskOperator = HandpayBusinessObject.CreateInstance();

                    strTreasuryType = TREASURY_HANDPAY_CREDIT;

                    //Get the type of treasury event
                    if (_POSDetail.HandPayType == "HandPay")
                    {
                        strTreasuryType = TREASURY_HANDPAY_CREDIT; ;
                    }
                    else if (_POSDetail.HandPayType == "Jackpot")
                    {
                        strTreasuryType = TREASURY_HANDPAY_JACKPOT;
                    }
                    else if (_POSDetail.HandPayType == "Progressive")
                    {
                        strTreasuryType = TREASURY_PROG;
                    }

                    //Save the treasury details in treasury table.
                    objHandpayEntity.TreasuryType = strTreasuryType;

                    objHandpayEntity.CollectionNumber = 0;
                    objHandpayEntity.InstallationNumber = _POSDetail.InstallationNo;
                    objHandpayEntity.UserID = Security.SecurityHelper.CurrentUser.SecurityUserID;
                    objHandpayEntity.TreasuryReason = "";
                    objHandpayEntity.TreasuryReasonCode = 0;
                    objHandpayEntity.TreasuryAllocated = 0;
                    objHandpayEntity.TreasuryBreakdown100p = 0;
                    objHandpayEntity.TreasuryBreakdown10p = 0;
                    objHandpayEntity.TreasuryBreakdown200p = 0;
                    objHandpayEntity.TreasuryBreakdown20p = 0;
                    objHandpayEntity.TreasuryBreakdown2p = 0;
                    objHandpayEntity.TreasuryBreakdown50p = 0;
                    objHandpayEntity.TreasuryBreakdown5p = 0;
                    objHandpayEntity.TreasuryIssuerUserNo = Security.SecurityHelper.CurrentUser.SecurityUserID;
                    objHandpayEntity.TreasuryMembershipNo = "0";

                    objHandpayEntity.TreasuryAmount = double.Parse(_POSDetail.HandPayValue.ToString());
                    fTreasuryAmount = objHandpayEntity.TreasuryAmount;

                    iTreasuryID = objCashDeskOperator.ProcessHandPay(objHandpayEntity);

                    DataTable dt = new DataTable();
                    dt = analysisBusinessObject.GetTicketException(_POSDetail.InstallationNo);

                    foreach (DataRow dr in dt.Rows)
                    {
                        strTEID = BMC.Presentation.Helper_classes.Common.GetRowValue<int>(dr, "TE_ID").ToString();
                        break;
                    }


                    if (objCashDeskOperator.UpdateTicketException(strTEID) == false)
                    {
                        BMC.Common.LogManagement.LogManager.WriteLog("Error while updating ticket exception table", BMC.Common.LogManagement.LogManager.enumLogLevel.Info);
                    }

                    //Create a receipt with -ve amount incase of a void transaction
                    if (IsVoid == true) { fTreasuryAmount = -fTreasuryAmount; }
                    //Print the receipt for handpay.


                    //MachineDetails.TreasuryNo = iTreasuryID.ToString();
                    //MachineDetails.Value = objHandpayEntity.TreasuryAmount.ToString();

                    if (strTreasuryType == TREASURY_HANDPAY_CREDIT)
                    {
                        strTreasuryType = "Handpay";
                    }
                    (oCommonUtilities.CreateInstance()).PrintCommonReceipt(false, strTreasuryType, "");
                    if (IsVoid == false)
                    {
                        MessageBox.ShowBox("MessageID57", BMC_Icon.Information);
                    }

                    this.Visibility = Visibility.Hidden;
                }
                catch (Exception ex)
                {
                    MessageBox.ShowBox("MessageID59", BMC_Icon.Error);
                    ExceptionManager.Publish(ex);
                }
            }
            else
            {
                MessageBox.ShowBox("MessageID59", BMC_Icon.Error);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //this.Visibility = Visibility.Hidden;
            //CHandpay objHandpay = new CHandpay();
            //pnlContent.Children.Add(objHandpay);
            //objHandpay.Margin = new Thickness(0);
            //LoadmachineList();
            //objHandpayo


        }




        private void btnMeterLife_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                btnMeterLife.IsEnabled = false;
                if (_POSDetail.GameName.ToString().Trim() == String.Empty)
                    return;
                MeterLife objMeterLife = new MeterLife(_POSDetail.InstallationNo);
                objMeterLife.Position = _POSDetail.PostionNumber;
                objMeterLife.Asset = _POSDetail.AssetNumber;
                objMeterLife.Game = _POSDetail.GameName;
                objMeterLife.InstallationStartDate = _POSDetail.InstallationStartDate;
                objMeterLife.InstallationStartTime = _POSDetail.InstallationStartTime;
                objMeterLife.Owner = Window.GetWindow(this);
                objMeterLife.ShowDialog();
            }
            finally
            {
                btnMeterLife.IsEnabled = true;
            }
        }

        private void btnServiceRequest_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                btnServiceRequest.IsEnabled = false;
                ServiceCalls objServiceCalls = new ServiceCalls();
                objServiceCalls.InstallationNo = _POSDetail.InstallationNo;
                objServiceCalls.Position = _POSDetail.PostionNumber;
                objServiceCalls.Asset = _POSDetail.AssetNumber;
                objServiceCalls.Game = _POSDetail.GameName;
                objServiceCalls.Manufacturer = _POSDetail.Manufacturer;
                objServiceCalls.SerialNo = _POSDetail.SerialNumber;
                objServiceCalls.Owner = Window.GetWindow(this);
                objServiceCalls.ShowDialog();
            }
            finally
            {
                btnServiceRequest.IsEnabled = true;
            }
        }

        private void btnEnrol_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                btnEnrol.IsEnabled = false;
                MachineEnrolment objMachineEnrolment = new MachineEnrolment(lblGame.Content.ToString().Trim() == String.Empty, _POSDetail.PostionNumber);
                objMachineEnrolment.Owner = Window.GetWindow(this);
                objMachineEnrolment.ShowDialog();
            }
            finally
            {
                btnEnrol.IsEnabled = true;
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
                        this.btnExit.Click -= (this.btn_Exit);
                        this.btnHandPayProcess.Click -= (this.btnHandPayProcess_Click);
                        this.btnMeterLife.Click -= (this.btnMeterLife_Click);
                        this.btnServiceRequest.Click -= (this.btnServiceRequest_Click);
                        this.btnEnrol.Click -= (this.btnEnrol_Click);
                    },
                    (c) =>
                    {
                    });
                    this.WriteLog("PositionDetailsComp objects are released successfully.");

                }
                disposed = true;
            }
        }

        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="PositionDetailsComp"/> is reclaimed by garbage collection.
        /// </summary>
        ~PositionDetailsComp()
        {
            Dispose(false);
        }

        #endregion
    }



    public class PositionDetail
    {
        public string PostionNumber = string.Empty;
        public string GameName = string.Empty;
        public string AssetNumber = string.Empty;
        public string SerialNumber = string.Empty;
        public string Manufacturer = string.Empty;
        public string HandPayDate = string.Empty;
        public string HandPayTime = string.Empty;
        public string HandPayType = string.Empty;
        public string HandPayValue = string.Empty;
        public bool IsEPIAvailable = false;
        public string PlayerName = "";
        public string PlayerAccountNumber = "";
        public string PlayerClubStatus = "";
        public string PlayerTimeOfPlay = "";
        public int InstallationNo = 0;
        public DateTime InstallationStartDate;
        public DateTime InstallationStartTime;
        public int InstallationFloatStatus;
        public SlotMachineStatus Status;        
		public bool IsEventUncleared = false;
        public int FFInstallationNo;
        public int MaxBet;
        public int BaseDenom;
        public int CoinValue;
        public double PercentagePayout;


    }
}