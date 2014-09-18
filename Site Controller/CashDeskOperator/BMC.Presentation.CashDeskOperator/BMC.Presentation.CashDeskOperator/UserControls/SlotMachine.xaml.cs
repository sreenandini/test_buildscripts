using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;
using BMC.Presentation.POS.Helper_classes;

namespace BMC.Presentation
{
    /// <summary>
    /// Interaction logic for SlotMachine.xaml
    /// </summary>
    public partial class SlotMachine: IDisposable
    {
        private bool disposed;
        public static readonly DependencyProperty AssetNumberProperty =
            DependencyProperty.Register("AssetNumber", typeof (string), typeof (SlotMachine), new UIPropertyMetadata(""));

        public static readonly DependencyProperty InnerRoundColorProperty =
            DependencyProperty.Register("InnerRoundColor", typeof (Brush), typeof (SlotMachine),
                                        new UIPropertyMetadata(Brushes.Gray));

        public static readonly DependencyProperty IsCollectableProperty =
            DependencyProperty.Register("IsCollectable", typeof (bool), typeof (SlotMachine),
                                        new UIPropertyMetadata(false,
                                                               IsCollectableValueChange));

        public static readonly DependencyProperty IsEventUnClearedProperty =
            DependencyProperty.Register("IsEventUnCleared", typeof (bool), typeof (SlotMachine),
                                        new UIPropertyMetadata(new PropertyChangedCallback(IsEventUnClearedValueChange)));

        public static readonly DependencyProperty IsHandPayProperty =
            DependencyProperty.Register("IsHandPay", typeof (bool), typeof (SlotMachine),
                                        new UIPropertyMetadata(false, IsHandPayValueChange));

        public static readonly DependencyProperty OuterRoundColorProperty =
            DependencyProperty.Register("OuterRoundColor", typeof (Brush), typeof (SlotMachine),
                                        new UIPropertyMetadata(Brushes.White));

        public static readonly DependencyProperty SlotNumberProperty =
            DependencyProperty.Register("SlotNumber", typeof (string), typeof (SlotMachine),
                                        new UIPropertyMetadata("000"));

        public static readonly DependencyProperty StatusProperty =
            DependencyProperty.Register("Status", typeof (SlotMachineStatus), typeof (SlotMachine),
                                        new UIPropertyMetadata(SlotMachineStatus.EmptyPosition,
                                                               SlotMachineStatusChanged));

        private readonly Storyboard _sbHandPayAnimation = new Storyboard();
        private readonly Storyboard _sbStopAnimation = new Storyboard();

        private readonly Brush _foreColorBlack = Brushes.Black;
        private readonly Brush _foreColorWhite = Brushes.White;

        public SlotMachine()
        {
            InitializeComponent();
            _sbHandPayAnimation = (FindResource("RotateGloss") as Storyboard);
            _sbStopAnimation = (FindResource("StopAnimation") as Storyboard);
        }


        public SlotMachineStatus Status
        {
            get { return (SlotMachineStatus) GetValue(StatusProperty); }
            set { SetValue(StatusProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Status.  This enables animation, styling, binding, etc...

        public double Top { get; set; }
        public double Left { get; set; }

        public BMCIPC.FloorPositionDataDto PositionData { get; set; }

        public Brush OuterRoundColor
        {
            get { return (Brush) GetValue(OuterRoundColorProperty); }
            set { SetValue(OuterRoundColorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for OuterRoundColor.  This enables animation, styling, binding, etc...


        public Brush InnerRoundColor
        {
            get { return (Brush) GetValue(InnerRoundColorProperty); }
            set { SetValue(InnerRoundColorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for InnerRoundColor.  This enables animation, styling, binding, etc...


        public string SlotNumber
        {
            get { return (string) GetValue(SlotNumberProperty); }
            set { SetValue(SlotNumberProperty, value); }
        }

        public string AssetNumber
        {
            get { return (string) GetValue(AssetNumberProperty); }
            set { SetValue(AssetNumberProperty, value); }
        }

        public Brush ForeColorBrush
        {
            get
            {
                return this.Status == SlotMachineStatus.ForceFinalCollection ||
                      this.Status == SlotMachineStatus.FloatCollection ? _foreColorWhite : _foreColorBlack;
            }
        }

        //New Variable for assigning Slot Number at time of installation
        public string SlotNumberString { get; set; }


        public int InstallationNo { get; set; }
        public int FFInstallationNo { get; set; }
        public string AssetNo { get; set; }
        public string GameName { get; set; }
        public DateTime InstallationStartDate { get; set; }
        public DateTime InstallationStartTime { get; set; }
        public int InstallationFloatStatus { get; set; }
        public int SlotID { get; set; }
        public string SerialNumber { get; set; }
        public string Manufacturer { get; set; }
        public int MaxBet { get; set; }
        public int BaseDenom { get; set; }
        public int CoinValue { get; set; }
        public double PercentagePayout { get; set; }
        public int FinalCollectionStatus { get; set; }
        // Using a DependencyProperty as the backing store for SlotNumber.  This enables animation, styling, binding, etc...

        public bool StackerEventReceived
        {
            get { return (bool)GetValue(StackerEventReceivedProperty); }
            set { SetValue(StackerEventReceivedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for StackerEventReceived.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StackerEventReceivedProperty =
            DependencyProperty.Register("StackerEventReceived", typeof(bool), typeof(SlotMachine), new UIPropertyMetadata(false, HasStackerRemoveEventReceived));


        public bool IsHandPay
        {
            get { return (bool) GetValue(IsHandPayProperty); }
            set { SetValue(IsHandPayProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsHandPay.  This enables animation, styling, binding, etc...

        public bool IsEventUnCleared
        {
            get { return (bool) GetValue(IsEventUnClearedProperty); }
            set { SetValue(IsEventUnClearedProperty, value); }
        }

        public bool IsCollectable
        {
            get { return (bool) GetValue(IsCollectableProperty); }
            set { SetValue(IsCollectableProperty, value); }
        }

        public string SlotStatusString { get; set; }

        private static void SlotMachineStatusChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            ((SlotMachine) (source)).SetMachineStatus((SlotMachineStatus) e.NewValue);
        }

        public void SetMachineStatus(SlotMachineStatus machineStatus)
        {
            IsHandPay = false;
            switch (machineStatus)
            {
                case SlotMachineStatus.EmptyPosition:
                    InnerRoundColor = Brushes.Gray;
                    OuterRoundColor = Brushes.White;
                    // other fixes
                    {
                        try
                        {
                            if (!string.IsNullOrEmpty(this.AssetNo))
                                this.AssetNo = string.Empty;
                            if (this.IsCollectable) this.IsCollectable = false;
                            if (this.IsEventUnCleared) this.IsEventUnCleared = false;
                            if (this.IsHandPay) this.IsHandPay = false;

                            UnclearedImageEvent.Visibility = Visibility.Hidden;
                            ClearedImageEvent.Visibility = Visibility.Hidden;
                            FloatCollectionImage.Visibility = Visibility.Hidden;
                        }
                        catch { }
                    }
                    break;
                case SlotMachineStatus.NotInPlay:
                    InnerRoundColor = FindResource("NotInPlayInnerColor") as Brush;
                    OuterRoundColor = FindResource("NotInPlayOuterColor") as Brush;
                    break;
                case SlotMachineStatus.NonCardedHandPay:
                    InnerRoundColor = FindResource("HandPayColorInnerColor") as Brush;
                    OuterRoundColor = FindResource("HandPayColorOuterColor") as Brush;
                    IsHandPay = true;     
                    break;
                case SlotMachineStatus.CardedHandPay:
                    InnerRoundColor = FindResource("HandPayColorInnerColor") as Brush;
                    OuterRoundColor = FindResource("CardedPlayOuterColor") as Brush;
                    IsHandPay = true;
                    break;
                case SlotMachineStatus.MachineInPlay:
                    InnerRoundColor = FindResource("MachineInPlayInnerColor") as Brush;
                    OuterRoundColor = FindResource("MachineInPlayOuterColor") as Brush;
                    break;
                case SlotMachineStatus.CardedPlay:
                    InnerRoundColor = FindResource("CardedPlayInnerColor") as Brush;
                    OuterRoundColor = FindResource("CardedPlayOuterColor") as Brush;
                    break;

                case SlotMachineStatus.EmpCardedPlay:
                    InnerRoundColor = FindResource("EmpCardedPlayInnerColor") as Brush;
                    OuterRoundColor = FindResource("EmpCardedPlayOuterColor") as Brush;
                    break;

                case SlotMachineStatus.GMUConnectivity:
                    InnerRoundColor = FindResource("GMUConnectivityInnerColor") as Brush;
                    OuterRoundColor = FindResource("GMUConnectivityOuterColor") as Brush;
                    break;

                case SlotMachineStatus.GoldClubCardedPlay:
                    InnerRoundColor = FindResource("GoldClubCardedPlayInnerColor") as Brush;
                    OuterRoundColor = FindResource("GoldClubCardedPlayOuterColor") as Brush;
                    break;
                case SlotMachineStatus.VLTInstallationAAMSPending:
                    InnerRoundColor = FindResource("VLTInstallationAAMSPendingInnerColor") as Brush;
                    OuterRoundColor = FindResource("VLTInstallationAAMSPendingOuterColor") as Brush;
                    break;
                case SlotMachineStatus.GameInstallationAAMSPending:
                    InnerRoundColor = FindResource("GameInstallationAAMSPendingInnerColor") as Brush;
                    OuterRoundColor = FindResource("GameInstallationAAMSPendingOuterColor") as Brush;
                    break;
                case SlotMachineStatus.VLTunderMaintenance:
                    InnerRoundColor = FindResource("VLTunderMaintenanceInnerColor") as Brush;
                    OuterRoundColor = FindResource("VLTunderMaintenanceOuterColor") as Brush;
                    break;
                case SlotMachineStatus.VLTunderUnauthorizedMaintenance:
                    InnerRoundColor = FindResource("MachineInPlayOuterColor") as Brush;
                    OuterRoundColor = FindResource("VLTunderMaintenanceOuterColor") as Brush;

                    break;
                case SlotMachineStatus.FloatCollection:
                    InnerRoundColor = FindResource("NotInPlayInnerColor") as Brush;
                    OuterRoundColor = Brushes.Black;
                    break;
                case SlotMachineStatus.ForceFinalCollection:
                    InnerRoundColor = Brushes.Pink;
                    OuterRoundColor = Brushes.Black;

                    break;

                case SlotMachineStatus.InstallationCompletedNonMetered:
                    InnerRoundColor = Brushes.White;
                    OuterRoundColor = Brushes.YellowGreen;
                    break;

                case SlotMachineStatus.CommsDown:
                    InnerRoundColor = FindResource("ComsDownInnerColor") as Brush;
                    OuterRoundColor = FindResource("ComsDownOuterColor") as Brush;
                    break;

                case SlotMachineStatus.GameDown:
                    InnerRoundColor = FindResource("GameDownInnerColor") as Brush;
                    OuterRoundColor = FindResource("GameDownOuterColor") as Brush;
                    break;

                default:
                    break;
            }
        }

        private static void IsHandPayValueChange(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            //Don't Animate the HandPay if ForceFinalCollection Flag is Set after RamReset and Denom Change.
            if (((SlotMachine)(source)).FinalCollectionStatus == 0)
            {
                ((SlotMachine)source).AnimateHandPay((bool)e.NewValue);
            }
        }

        private void AnimateHandPay(bool beginAnimation)
        {
            if (beginAnimation)
            {
                RED.Visibility = Visibility.Visible;
            }
            else
            {
                RED.Visibility = Visibility.Hidden;
            }
        }


        private static void IsEventUnClearedValueChange(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            ((SlotMachine) source).ShowEventImage((bool) e.NewValue);
        }

        private static void IsCollectableValueChange(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            ((SlotMachine) source).ShowGoldBagImage((bool) e.NewValue);
        }

        public void ShowGoldBagImage(bool shouldDisplay)
        {
            FloatCollectionImage.Visibility = shouldDisplay ? Visibility.Visible : Visibility.Hidden;
        }

        private static void HasStackerRemoveEventReceived(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            if (((SlotMachine)source).StackerEventReceived)
            {
                ((SlotMachine)source).OuterRoundColor = Brushes.Goldenrod;
                ((SlotMachine)source).InnerRoundColor = Brushes.IndianRed;
            }
            else
            {
                ((SlotMachine)source).SetMachineStatus(((SlotMachine)source).Status);
            }
        }

        public void ShowEventImage(bool shouldDisplay)
        {
            if (shouldDisplay)
            {
                UnclearedImageEvent.Visibility = Visibility.Visible;
                ClearedImageEvent.Visibility = Visibility.Hidden;
            }
            else
            {
                UnclearedImageEvent.Visibility = Visibility.Hidden;
                ClearedImageEvent.Visibility = Visibility.Visible;
            }
        }

        public const int SlotMachineGap = 10;
        public const int SlotMachineWidth = 62;
        public const int SlotMachineHeight = 72;
        public static readonly int SlotMachineWidthGap = SlotMachineWidth + SlotMachineGap;
        public static readonly int SlotMachineHeightGap = SlotMachineHeight + SlotMachineGap;        

        #region IDisposable Members

        public void Dispose()
        {
            //throw new NotImplementedException();
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    AssetNo = null;
                    this.ClearedImageEvent = null;
                    this.ellipse = null;
                    this.FloatCollectionImage = null;
                    this.GameName = null;
                    this.Glass_Icon_Big = null;
                    this.Glass_Icon_Small = null;
                    this.Gloss = null;
                    this.Group_618 = null;
                    this.InnerBG = null;
                    this.InnerRoundColor = null;
                    this.OuterRoundBG = null;
                    this.OuterRoundColor = null;
                    this.Path_610 = null;
                    this.Path_612 = null;
                    this.Path_613 = null;
                    this.SlotNumber = null;
                    this.SlotNumberString = null;
                    this.UnclearedImageEvent = null;
                    this.viewbox1 = null;
                }
                disposed = true;
            }
        }

        ~SlotMachine()
        {
            Dispose(false);
        }

        #endregion
    }

    public enum SlotMachineStatus
    {
        EmptyPosition = 0,
        NotInPlay,
        InstallationCompletedNonMetered,
        NonCardedPlay,
        NonCardedHandPay,
        CardedPlay,
        EmpCardedPlay,
        CardedHandPay,
        MachineInPlay,
        GoldClubCardedPlay,
        VLTInstallationAAMSPending,
        GameInstallationAAMSPending,
        VLTunderMaintenance,
        VLTunderUnauthorizedMaintenance,
        FloatCollection,
        ForceFinalCollection,
        GMUConnectivity,
        CommsDown,
        GameDown,
        StackerRemoved
    }
}