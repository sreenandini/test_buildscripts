using System.ComponentModel;
using System.Data.Linq.Mapping;
using System.Runtime.Serialization;
using System;
using BMC.Common.Utilities;
using System.Text;
using System.Xml.Linq;

namespace BMC.Transport
{
    public class CollectionMachine : INotifyPropertyChanged
    {

        private string _Stock_No;

        private System.Nullable<int> _Installation_Float_Status;

        private string _Zone_Name;

        private int _Zone_No;

        private int _Installation_No;

        private int _Bar_Pos_No;

        private string _Bar_Pos_Name;

        private string _Name;

        private int _IsCollectable;

        private string _Installation_Reference;

        private System.Nullable<bool> _IsSelected;

        private System.Nullable<bool> _IsFocused;

        private string _DISPLAYNAME;

        private byte _FinalCollection_Status;

        private System.Nullable<bool> _IsStackerEventReceived;

        private string _Route_Name;

        private int _Route_No;

        private int _IsDeclared;

        private int _AutoDropSession;


        public CollectionMachine()
        {
        }

        [Column(Storage = "_Stock_No", DbType = "VarChar(50)")]
        public string Stock_No
        {
            get
            {
                return this._Stock_No;
            }
            set
            {
                if ((this._Stock_No != value))
                {
                    this._Stock_No = value ?? "";
                }
                if (PropertyChanged != null)
                {
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Stock_No"));
                }
            }
        }

        [Column(Storage = "_Installation_Float_Status", DbType = "Int")]
        public System.Nullable<int> Installation_Float_Status
        {
            get
            {
                return this._Installation_Float_Status;
            }
            set
            {
                if ((this._Installation_Float_Status != value))
                {
                    this._Installation_Float_Status = value ?? 0;
                }
                if (PropertyChanged != null)
                {
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Installation_Float_Status"));
                }
            }
        }

        [Column(Storage = "_Zone_Name", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
        public string Zone_Name
        {
            get
            {
                return this._Zone_Name;
            }
            set
            {
                if ((this._Zone_Name != value))
                {
                    this._Zone_Name = value ?? "";
                }
                if (PropertyChanged != null)
                {
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Zone_Name"));
                }
            }
        }

        [Column(Storage = "_Zone_No", DbType = "Int NOT NULL")]
        public int Zone_No
        {
            get
            {
                return this._Zone_No;
            }
            set
            {
                if ((this._Zone_No != value))
                {
                    this._Zone_No = value;
                }
                if (PropertyChanged != null)
                {
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Zone_No"));
                }
            }
        }

        [Column(Storage = "_Installation_No", DbType = "Int NOT NULL")]
        public int Installation_No
        {
            get
            {
                return this._Installation_No;
            }
            set
            {
                if ((this._Installation_No != value))
                {
                    this._Installation_No = value;
                }
                if (PropertyChanged != null)
                {
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Installation_No"));
                }
            }
        }

        [Column(Storage = "_Bar_Pos_No", DbType = "Int NOT NULL")]
        public int Bar_Pos_No
        {
            get
            {
                return this._Bar_Pos_No;
            }
            set
            {
                if ((this._Bar_Pos_No != value))
                {
                    this._Bar_Pos_No = value;
                }
                if (PropertyChanged != null)
                {
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Bar_Pos_No"));
                }
            }
        }

        [Column(Storage = "_Bar_Pos_Name", DbType = "VarChar(50)")]
        public string Bar_Pos_Name
        {
            get
            {
                return this._Bar_Pos_Name;
            }
            set
            {
                if ((this._Bar_Pos_Name != value))
                {
                    this._Bar_Pos_Name = value ?? "";
                }
                if (PropertyChanged != null)
                {
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Bar_Pos_Name"));
                }
            }
        }

        [Column(Storage = "_Name", DbType = "VarChar(50)")]
        public string Name
        {
            get
            {
                return this._Name;
            }
            set
            {
                if ((this._Name != value))
                {
                    this._Name = value ?? "";
                }
                if (PropertyChanged != null)
                {
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Name"));
                }
            }
        }

        [Column(Storage = "_IsCollectable", DbType = "Int NOT NULL")]
        public int IsCollectable
        {
            get
            {
                return this._IsCollectable;
            }
            set
            {
                if ((this._IsCollectable != value))
                {
                    this._IsCollectable = value;
                }
                if (PropertyChanged != null)
                {
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("IsCollectable"));
                }
            }
        }

        [Column(Storage = "_Installation_Reference", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
        public string Installation_Reference
        {
            get
            {
                return this._Installation_Reference;
            }
            set
            {
                if ((this._Installation_Reference != value))
                {
                    this._Installation_Reference = value ?? "";
                }
            }
        }

        [Column(Storage = "_IsSelected", DbType = "Bit")]
        public System.Nullable<bool> IsSelected
        {
            get
            {
                return this._IsSelected;
            }
            set
            {
                if ((this._IsSelected != value))
                {
                    this._IsSelected = value ?? false;
                }
                if (PropertyChanged != null)
                {
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("IsSelected"));
                }
            }
        }

        [Column(Storage = "_IsFocused", DbType = "Bit")]
        public System.Nullable<bool> IsFocused
        {
            get
            {
                return this._IsFocused;
            }
            set
            {
                if ((this._IsFocused != value))
                {
                    this._IsFocused = value ?? false;
                }
                if (PropertyChanged != null)
                {
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("IsFocused"));
                }
            }
        }

        [Column(Storage = "_DISPLAYNAME", DbType = "VarChar(103)")]
        public string DISPLAYNAME
        {
            get
            {
                return this._DISPLAYNAME;
            }
            set
            {
                if ((this._DISPLAYNAME != value))
                {
                    this._DISPLAYNAME = value ?? "";
                }
                if (PropertyChanged != null)
                {
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("DISPLAYNAME"));
                }
            }
        }

        [Column(Storage = "_FinalCollection_Status", DbType = "TinyInt NOT NULL")]
        public byte FinalCollection_Status
        {
            get
            {
                return this._FinalCollection_Status;
            }
            set
            {
                if ((this._FinalCollection_Status != value))
                {
                    this._FinalCollection_Status = value;
                }
            }
        }

        [Column(Storage = "_Route_Name", DbType = "VarChar(50)")]
        public string Route_Name
        {
            get
            {
                return this._Route_Name;
            }
            set
            {
                if ((this._Route_Name != value))
                {
                    this._Route_Name = value ?? "";
                }
                if (PropertyChanged != null)
                {
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Route_Name"));
                }
            }
        }

       
        [Column(Storage = "_Route_No", DbType = "Int NOT NULL")]
        public int Route_No
        {
            get
            {
                return this._Route_No;
            }
            set
            {
                if ((this._Route_No != value))
                {
                    this._Route_No = value;
                }
                if (PropertyChanged != null)
                {
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Route_No"));
                }
            }
        }

        [Column(Storage = "_AutoDropSession", DbType = "Int NOT NULL")]
        public int AutoDropSession
        {
            get
            {
                return this._AutoDropSession;
            }
            set
            {
                if ((this._AutoDropSession != value))
                {
                    this._AutoDropSession = value;
                }
                if (PropertyChanged != null)
                {
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("AutoDropSession"));
                }
            }
        }


         [Column(Storage = "_IsDeclared", DbType = "Int NOT NULL")]
        public int IsDeclared
        {
            get
            {
                return this._IsDeclared;
            }
            set
            {
                if ((this._IsDeclared != value))
                {
                    this._IsDeclared = value;
                }
                if (PropertyChanged != null)
                {
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("IsDeclared"));
                }
            }
        }

        public System.Nullable<bool> IsStackerEventReceived
        {
            get
            {
                return this._IsStackerEventReceived;
            }
            set
            {
                if ((this._IsStackerEventReceived != value))
                {
                    this._IsStackerEventReceived = value ?? false;
                }
                if (PropertyChanged != null)
                {
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("IsStackerEventReceived"));
                }
            }
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }

    public partial class rsp_IsUndeclaredPartCollectionResult
    {

        private System.Nullable<bool> _COLLECTED;

        public rsp_IsUndeclaredPartCollectionResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_COLLECTED", DbType = "Bit")]
        public System.Nullable<bool> COLLECTED
        {
            get
            {
                return this._COLLECTED;
            }
            set
            {
                if ((this._COLLECTED != value))
                {
                    this._COLLECTED = value;
                }
            }
        }
    }


    [DataContract()]
    public class CollectionBatchByMachine
    {

        private int _Collection_Batch_No;

        private string _Collection_Batch_Name;

        private string _Collection_Batch_Date;

        public CollectionBatchByMachine()
        {
        }

        [Column(Storage = "_Collection_Batch_No", DbType = "Int NOT NULL")]
        [DataMember(Order = 1)]
        public int Collection_Batch_No
        {
            get
            {
                return this._Collection_Batch_No;
            }
            set
            {
                if ((this._Collection_Batch_No != value))
                {
                    this._Collection_Batch_No = value;
                }
            }
        }

        [Column(Storage = "_Collection_Batch_Name", DbType = "VarChar(50)")]
        [DataMember(Order = 2)]
        public string Collection_Batch_Name
        {
            get
            {
                return this._Collection_Batch_Name;
            }
            set
            {
                if ((this._Collection_Batch_Name != value))
                {
                    this._Collection_Batch_Name = value ?? "";
                }
            }
        }

        [Column(Storage = "_Collection_Batch_Date", DbType = "VarChar(30)")]
        [DataMember(Order = 3)]
        public string Collection_Batch_Date
        {
            get
            {
                return this._Collection_Batch_Date;
            }
            set
            {
                if ((this._Collection_Batch_Date != value))
                {
                    this._Collection_Batch_Date = value ?? "";
                }
            }
        }
    }

    [Table(Name = "dbo.Installation")]
    public partial class Installation : INotifyPropertyChanging, INotifyPropertyChanged
    {

        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);

        private int _Installation_No;

        private System.Nullable<int> _Bar_Pos_No;

        private System.Nullable<int> _Machine_No;

        private System.Nullable<int> _Datapak_No;

        private System.Nullable<int> _Datapak_Variant;

        private System.Nullable<int> _HQ_Installation_No;

        private string _Installation_Reference;

        private System.Nullable<System.DateTime> _Start_Date;

        private System.Nullable<System.DateTime> _End_Date;

        private System.Nullable<float> _Anticipated_Percentage_Payout;

        private System.Nullable<int> _Coins_In_Counter;

        private System.Nullable<int> _Coins_Out_Counter;

        private System.Nullable<int> _Tokens_In_Counter;

        private System.Nullable<int> _Tokens_Out_Counter;

        private System.Nullable<int> _Prize_Counter;

        private System.Nullable<int> _Refill_Counter;

        private System.Nullable<int> _Tournament_Counter;

        private System.Nullable<int> _Jukebox_Counter;

        private System.Nullable<int> _Previous_Installation;

        private System.Nullable<float> _Bagged_Cash_Installation_No;

        private System.Nullable<float> _Bagged_Cash_Amount;

        private System.Nullable<float> _Bagged_Cash_Float;

        private System.Nullable<bool> _Installation_Out_Of_Order;

        private System.Nullable<float> _Float_Issued;

        private string _Float_Issued_By;

        private System.Nullable<int> _Installation_Price_Of_Play;

        private System.Nullable<int> _Installation_Jackpot;

        private System.Nullable<int> _Installation_Meter_1_Initial_Value;

        private System.Nullable<int> _Installation_Meter_1_Final_Value;

        private System.Nullable<int> _Installation_Meter_2_Initial_Value;

        private System.Nullable<int> _Installation_Meter_2_Final_Value;

        private System.Nullable<int> _Installation_Meter_3_Initial_Value;

        private System.Nullable<int> _Installation_Meter_3_Final_Value;

        private System.Nullable<int> _Installation_Meter_4_Initial_Value;

        private System.Nullable<int> _Installation_Meter_4_Final_Value;

        private System.Nullable<int> _Installation_Meter_5_Initial_Value;

        private System.Nullable<int> _Installation_Meter_5_Final_Value;

        private System.Nullable<int> _Installation_Meter_6_Initial_Value;

        private System.Nullable<int> _Installation_Meter_6_Final_Value;

        private System.Nullable<int> _Installation_Meter_7_Initial_Value;

        private System.Nullable<int> _Installation_Meter_7_Final_Value;

        private System.Nullable<int> _Installation_Meter_8_Initial_Value;

        private System.Nullable<int> _Installation_Meter_8_Final_Value;

        private System.Nullable<int> _Installation_Meter_9_Initial_Value;

        private System.Nullable<int> _Installation_Meter_9_Final_Value;

        private System.Nullable<int> _Installation_Meter_10_Initial_Value;

        private System.Nullable<int> _Installation_Meter_10_Final_Value;

        private System.Nullable<int> _Installation_Meter_11_Initial_Value;

        private System.Nullable<int> _Installation_Meter_11_Final_Value;

        private System.Nullable<int> _Installation_Meter_12_Initial_Value;

        private System.Nullable<int> _Installation_Meter_12_Final_Value;

        private System.Nullable<int> _Installation_Meter_13_Initial_Value;

        private System.Nullable<int> _Installation_Meter_13_Final_Value;

        private System.Nullable<int> _Installation_Meter_14_Initial_Value;

        private System.Nullable<int> _Installation_Meter_14_Final_Value;

        private System.Nullable<int> _Installation_Meter_15_Initial_Value;

        private System.Nullable<int> _Installation_Meter_15_Final_Value;

        private System.Nullable<int> _Installation_Meter_16_Initial_Value;

        private System.Nullable<int> _Installation_Meter_16_Final_Value;

        private System.Nullable<int> _Installation_Meter_17_Initial_Value;

        private System.Nullable<int> _Installation_Meter_17_Final_Value;

        private System.Nullable<int> _Installation_Meter_18_Initial_Value;

        private System.Nullable<int> _Installation_Meter_18_Final_Value;

        private System.Nullable<int> _Installation_Meter_19_Initial_Value;

        private System.Nullable<int> _Installation_Meter_19_Final_Value;

        private System.Nullable<int> _Installation_Meter_20_Initial_Value;

        private System.Nullable<int> _Installation_Meter_20_Final_Value;

        private System.Nullable<int> _Installation_Meter_21_Initial_Value;

        private System.Nullable<int> _Installation_Meter_21_Final_Value;

        private System.Nullable<int> _Installation_Meter_22_Initial_Value;

        private System.Nullable<int> _Installation_Meter_22_Final_Value;

        private System.Nullable<int> _Installation_Meter_23_Initial_Value;

        private System.Nullable<int> _Installation_Meter_23_Final_Value;

        private System.Nullable<int> _Installation_Meter_24_Initial_Value;

        private System.Nullable<int> _Installation_Meter_24_Final_Value;

        private System.Nullable<int> _Installation_Meter_25_Initial_Value;

        private System.Nullable<int> _Installation_Meter_25_Final_Value;

        private System.Nullable<int> _Installation_Meter_26_Initial_Value;

        private System.Nullable<int> _Installation_Meter_26_Final_Value;

        private System.Nullable<int> _Installation_Meter_27_Initial_Value;

        private System.Nullable<int> _Installation_Meter_27_Final_Value;

        private System.Nullable<int> _Installation_Meter_28_Initial_Value;

        private System.Nullable<int> _Installation_Meter_28_Final_Value;

        private System.Nullable<int> _Installation_Meter_29_Initial_Value;

        private System.Nullable<int> _Installation_Meter_29_Final_Value;

        private System.Nullable<int> _Installation_Meter_30_Initial_Value;

        private System.Nullable<int> _Installation_Meter_30_Final_Value;

        private System.Nullable<int> _Installation_Meter_31_Initial_Value;

        private System.Nullable<int> _Installation_Meter_31_Final_Value;

        private System.Nullable<int> _Installation_Meter_32_Initial_Value;

        private System.Nullable<int> _Installation_Meter_32_Final_Value;

        private System.Nullable<int> _Installation_Float_Status;

        private System.Nullable<int> _Installation_Initial_Meters_Coins_In;

        private System.Nullable<int> _Installation_Initial_Meters_Coins_Out;

        private System.Nullable<int> _Installation_Initial_Meters_Coin_Drop;

        private System.Nullable<int> _Installation_Initial_Meters_External_Credit;

        private System.Nullable<int> _Installation_Initial_Meters_Games_Bet;

        private System.Nullable<int> _Installation_Initial_Meters_Games_Won;

        private System.Nullable<int> _Installation_Initial_Meters_Notes;

        private System.Nullable<int> _Installation_Initial_Meters_Handpay;

        private string _Anticipated_Removal_Date;

        private string _Rental_Step_Down_Date;

        private System.Nullable<decimal> _Rent1;

        private System.Nullable<decimal> _Rent2;

        private System.Nullable<decimal> _Licence;

        private System.Nullable<bool> _Installation_Out_Order;

        private System.Nullable<int> _Installation_Counter_Cash_In_Units;

        private System.Nullable<int> _Installation_Counter_Cash_Out_Units;

        private System.Nullable<int> _Installation_Counter_Token_In_Units;

        private System.Nullable<int> _Installation_Counter_Token_Out_Units;

        private System.Nullable<int> _Installation_Counter_Refill_Units;

        private System.Nullable<int> _Installation_Counter_Jackpot_Units;

        private System.Nullable<int> _Installation_Counter_Prize_Units;

        private System.Nullable<int> _Installation_Counter_Tournament_Units;

        private System.Nullable<int> _Installation_Counter_Jukebox_Play_Units;

        private System.Nullable<int> _Installation_Counter_Jukebox_Units;

        private System.Nullable<int> _Planned_Movement_ID;

        private string _Installation_RDC_Machine_Code;

        private string _Installation_RDC_Secondary_Machine_Code;

        private int _Installation_Token_Value;

        private System.Nullable<int> _Installation_Games_Count;

        private string _Installation_Status;

        private string _Game_Part_Number;

        private System.Nullable<int> _Installation_MaxBet;

        private bool _IsAuxSerialPortEnabled;

        private bool _IsGatSerialPortEnabled;

        private bool _IsSlotLinePortEnabled;

        private bool _Port_Disabled_Status;

        private System.Nullable<char> _Voucher_Expire_Status;

        private byte _FinalCollection_Status;

        #region Extensibility Method Definitions
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
        partial void OnInstallation_NoChanging(int value);
        partial void OnInstallation_NoChanged();
        partial void OnBar_Pos_NoChanging(System.Nullable<int> value);
        partial void OnBar_Pos_NoChanged();
        partial void OnMachine_NoChanging(System.Nullable<int> value);
        partial void OnMachine_NoChanged();
        partial void OnDatapak_NoChanging(System.Nullable<int> value);
        partial void OnDatapak_NoChanged();
        partial void OnDatapak_VariantChanging(System.Nullable<int> value);
        partial void OnDatapak_VariantChanged();
        partial void OnHQ_Installation_NoChanging(System.Nullable<int> value);
        partial void OnHQ_Installation_NoChanged();
        partial void OnInstallation_ReferenceChanging(string value);
        partial void OnInstallation_ReferenceChanged();
        partial void OnStart_DateChanging(System.Nullable<System.DateTime> value);
        partial void OnStart_DateChanged();
        partial void OnEnd_DateChanging(System.Nullable<System.DateTime> value);
        partial void OnEnd_DateChanged();
        partial void OnAnticipated_Percentage_PayoutChanging(System.Nullable<float> value);
        partial void OnAnticipated_Percentage_PayoutChanged();
        partial void OnCoins_In_CounterChanging(System.Nullable<int> value);
        partial void OnCoins_In_CounterChanged();
        partial void OnCoins_Out_CounterChanging(System.Nullable<int> value);
        partial void OnCoins_Out_CounterChanged();
        partial void OnTokens_In_CounterChanging(System.Nullable<int> value);
        partial void OnTokens_In_CounterChanged();
        partial void OnTokens_Out_CounterChanging(System.Nullable<int> value);
        partial void OnTokens_Out_CounterChanged();
        partial void OnPrize_CounterChanging(System.Nullable<int> value);
        partial void OnPrize_CounterChanged();
        partial void OnRefill_CounterChanging(System.Nullable<int> value);
        partial void OnRefill_CounterChanged();
        partial void OnTournament_CounterChanging(System.Nullable<int> value);
        partial void OnTournament_CounterChanged();
        partial void OnJukebox_CounterChanging(System.Nullable<int> value);
        partial void OnJukebox_CounterChanged();
        partial void OnPrevious_InstallationChanging(System.Nullable<int> value);
        partial void OnPrevious_InstallationChanged();
        partial void OnBagged_Cash_Installation_NoChanging(System.Nullable<float> value);
        partial void OnBagged_Cash_Installation_NoChanged();
        partial void OnBagged_Cash_AmountChanging(System.Nullable<float> value);
        partial void OnBagged_Cash_AmountChanged();
        partial void OnBagged_Cash_FloatChanging(System.Nullable<float> value);
        partial void OnBagged_Cash_FloatChanged();
        partial void OnInstallation_Out_Of_OrderChanging(System.Nullable<bool> value);
        partial void OnInstallation_Out_Of_OrderChanged();
        partial void OnFloat_IssuedChanging(System.Nullable<float> value);
        partial void OnFloat_IssuedChanged();
        partial void OnFloat_Issued_ByChanging(string value);
        partial void OnFloat_Issued_ByChanged();
        partial void OnInstallation_Price_Of_PlayChanging(System.Nullable<int> value);
        partial void OnInstallation_Price_Of_PlayChanged();
        partial void OnInstallation_JackpotChanging(System.Nullable<int> value);
        partial void OnInstallation_JackpotChanged();
        partial void OnInstallation_Meter_1_Initial_ValueChanging(System.Nullable<int> value);
        partial void OnInstallation_Meter_1_Initial_ValueChanged();
        partial void OnInstallation_Meter_1_Final_ValueChanging(System.Nullable<int> value);
        partial void OnInstallation_Meter_1_Final_ValueChanged();
        partial void OnInstallation_Meter_2_Initial_ValueChanging(System.Nullable<int> value);
        partial void OnInstallation_Meter_2_Initial_ValueChanged();
        partial void OnInstallation_Meter_2_Final_ValueChanging(System.Nullable<int> value);
        partial void OnInstallation_Meter_2_Final_ValueChanged();
        partial void OnInstallation_Meter_3_Initial_ValueChanging(System.Nullable<int> value);
        partial void OnInstallation_Meter_3_Initial_ValueChanged();
        partial void OnInstallation_Meter_3_Final_ValueChanging(System.Nullable<int> value);
        partial void OnInstallation_Meter_3_Final_ValueChanged();
        partial void OnInstallation_Meter_4_Initial_ValueChanging(System.Nullable<int> value);
        partial void OnInstallation_Meter_4_Initial_ValueChanged();
        partial void OnInstallation_Meter_4_Final_ValueChanging(System.Nullable<int> value);
        partial void OnInstallation_Meter_4_Final_ValueChanged();
        partial void OnInstallation_Meter_5_Initial_ValueChanging(System.Nullable<int> value);
        partial void OnInstallation_Meter_5_Initial_ValueChanged();
        partial void OnInstallation_Meter_5_Final_ValueChanging(System.Nullable<int> value);
        partial void OnInstallation_Meter_5_Final_ValueChanged();
        partial void OnInstallation_Meter_6_Initial_ValueChanging(System.Nullable<int> value);
        partial void OnInstallation_Meter_6_Initial_ValueChanged();
        partial void OnInstallation_Meter_6_Final_ValueChanging(System.Nullable<int> value);
        partial void OnInstallation_Meter_6_Final_ValueChanged();
        partial void OnInstallation_Meter_7_Initial_ValueChanging(System.Nullable<int> value);
        partial void OnInstallation_Meter_7_Initial_ValueChanged();
        partial void OnInstallation_Meter_7_Final_ValueChanging(System.Nullable<int> value);
        partial void OnInstallation_Meter_7_Final_ValueChanged();
        partial void OnInstallation_Meter_8_Initial_ValueChanging(System.Nullable<int> value);
        partial void OnInstallation_Meter_8_Initial_ValueChanged();
        partial void OnInstallation_Meter_8_Final_ValueChanging(System.Nullable<int> value);
        partial void OnInstallation_Meter_8_Final_ValueChanged();
        partial void OnInstallation_Meter_9_Initial_ValueChanging(System.Nullable<int> value);
        partial void OnInstallation_Meter_9_Initial_ValueChanged();
        partial void OnInstallation_Meter_9_Final_ValueChanging(System.Nullable<int> value);
        partial void OnInstallation_Meter_9_Final_ValueChanged();
        partial void OnInstallation_Meter_10_Initial_ValueChanging(System.Nullable<int> value);
        partial void OnInstallation_Meter_10_Initial_ValueChanged();
        partial void OnInstallation_Meter_10_Final_ValueChanging(System.Nullable<int> value);
        partial void OnInstallation_Meter_10_Final_ValueChanged();
        partial void OnInstallation_Meter_11_Initial_ValueChanging(System.Nullable<int> value);
        partial void OnInstallation_Meter_11_Initial_ValueChanged();
        partial void OnInstallation_Meter_11_Final_ValueChanging(System.Nullable<int> value);
        partial void OnInstallation_Meter_11_Final_ValueChanged();
        partial void OnInstallation_Meter_12_Initial_ValueChanging(System.Nullable<int> value);
        partial void OnInstallation_Meter_12_Initial_ValueChanged();
        partial void OnInstallation_Meter_12_Final_ValueChanging(System.Nullable<int> value);
        partial void OnInstallation_Meter_12_Final_ValueChanged();
        partial void OnInstallation_Meter_13_Initial_ValueChanging(System.Nullable<int> value);
        partial void OnInstallation_Meter_13_Initial_ValueChanged();
        partial void OnInstallation_Meter_13_Final_ValueChanging(System.Nullable<int> value);
        partial void OnInstallation_Meter_13_Final_ValueChanged();
        partial void OnInstallation_Meter_14_Initial_ValueChanging(System.Nullable<int> value);
        partial void OnInstallation_Meter_14_Initial_ValueChanged();
        partial void OnInstallation_Meter_14_Final_ValueChanging(System.Nullable<int> value);
        partial void OnInstallation_Meter_14_Final_ValueChanged();
        partial void OnInstallation_Meter_15_Initial_ValueChanging(System.Nullable<int> value);
        partial void OnInstallation_Meter_15_Initial_ValueChanged();
        partial void OnInstallation_Meter_15_Final_ValueChanging(System.Nullable<int> value);
        partial void OnInstallation_Meter_15_Final_ValueChanged();
        partial void OnInstallation_Meter_16_Initial_ValueChanging(System.Nullable<int> value);
        partial void OnInstallation_Meter_16_Initial_ValueChanged();
        partial void OnInstallation_Meter_16_Final_ValueChanging(System.Nullable<int> value);
        partial void OnInstallation_Meter_16_Final_ValueChanged();
        partial void OnInstallation_Meter_17_Initial_ValueChanging(System.Nullable<int> value);
        partial void OnInstallation_Meter_17_Initial_ValueChanged();
        partial void OnInstallation_Meter_17_Final_ValueChanging(System.Nullable<int> value);
        partial void OnInstallation_Meter_17_Final_ValueChanged();
        partial void OnInstallation_Meter_18_Initial_ValueChanging(System.Nullable<int> value);
        partial void OnInstallation_Meter_18_Initial_ValueChanged();
        partial void OnInstallation_Meter_18_Final_ValueChanging(System.Nullable<int> value);
        partial void OnInstallation_Meter_18_Final_ValueChanged();
        partial void OnInstallation_Meter_19_Initial_ValueChanging(System.Nullable<int> value);
        partial void OnInstallation_Meter_19_Initial_ValueChanged();
        partial void OnInstallation_Meter_19_Final_ValueChanging(System.Nullable<int> value);
        partial void OnInstallation_Meter_19_Final_ValueChanged();
        partial void OnInstallation_Meter_20_Initial_ValueChanging(System.Nullable<int> value);
        partial void OnInstallation_Meter_20_Initial_ValueChanged();
        partial void OnInstallation_Meter_20_Final_ValueChanging(System.Nullable<int> value);
        partial void OnInstallation_Meter_20_Final_ValueChanged();
        partial void OnInstallation_Meter_21_Initial_ValueChanging(System.Nullable<int> value);
        partial void OnInstallation_Meter_21_Initial_ValueChanged();
        partial void OnInstallation_Meter_21_Final_ValueChanging(System.Nullable<int> value);
        partial void OnInstallation_Meter_21_Final_ValueChanged();
        partial void OnInstallation_Meter_22_Initial_ValueChanging(System.Nullable<int> value);
        partial void OnInstallation_Meter_22_Initial_ValueChanged();
        partial void OnInstallation_Meter_22_Final_ValueChanging(System.Nullable<int> value);
        partial void OnInstallation_Meter_22_Final_ValueChanged();
        partial void OnInstallation_Meter_23_Initial_ValueChanging(System.Nullable<int> value);
        partial void OnInstallation_Meter_23_Initial_ValueChanged();
        partial void OnInstallation_Meter_23_Final_ValueChanging(System.Nullable<int> value);
        partial void OnInstallation_Meter_23_Final_ValueChanged();
        partial void OnInstallation_Meter_24_Initial_ValueChanging(System.Nullable<int> value);
        partial void OnInstallation_Meter_24_Initial_ValueChanged();
        partial void OnInstallation_Meter_24_Final_ValueChanging(System.Nullable<int> value);
        partial void OnInstallation_Meter_24_Final_ValueChanged();
        partial void OnInstallation_Meter_25_Initial_ValueChanging(System.Nullable<int> value);
        partial void OnInstallation_Meter_25_Initial_ValueChanged();
        partial void OnInstallation_Meter_25_Final_ValueChanging(System.Nullable<int> value);
        partial void OnInstallation_Meter_25_Final_ValueChanged();
        partial void OnInstallation_Meter_26_Initial_ValueChanging(System.Nullable<int> value);
        partial void OnInstallation_Meter_26_Initial_ValueChanged();
        partial void OnInstallation_Meter_26_Final_ValueChanging(System.Nullable<int> value);
        partial void OnInstallation_Meter_26_Final_ValueChanged();
        partial void OnInstallation_Meter_27_Initial_ValueChanging(System.Nullable<int> value);
        partial void OnInstallation_Meter_27_Initial_ValueChanged();
        partial void OnInstallation_Meter_27_Final_ValueChanging(System.Nullable<int> value);
        partial void OnInstallation_Meter_27_Final_ValueChanged();
        partial void OnInstallation_Meter_28_Initial_ValueChanging(System.Nullable<int> value);
        partial void OnInstallation_Meter_28_Initial_ValueChanged();
        partial void OnInstallation_Meter_28_Final_ValueChanging(System.Nullable<int> value);
        partial void OnInstallation_Meter_28_Final_ValueChanged();
        partial void OnInstallation_Meter_29_Initial_ValueChanging(System.Nullable<int> value);
        partial void OnInstallation_Meter_29_Initial_ValueChanged();
        partial void OnInstallation_Meter_29_Final_ValueChanging(System.Nullable<int> value);
        partial void OnInstallation_Meter_29_Final_ValueChanged();
        partial void OnInstallation_Meter_30_Initial_ValueChanging(System.Nullable<int> value);
        partial void OnInstallation_Meter_30_Initial_ValueChanged();
        partial void OnInstallation_Meter_30_Final_ValueChanging(System.Nullable<int> value);
        partial void OnInstallation_Meter_30_Final_ValueChanged();
        partial void OnInstallation_Meter_31_Initial_ValueChanging(System.Nullable<int> value);
        partial void OnInstallation_Meter_31_Initial_ValueChanged();
        partial void OnInstallation_Meter_31_Final_ValueChanging(System.Nullable<int> value);
        partial void OnInstallation_Meter_31_Final_ValueChanged();
        partial void OnInstallation_Meter_32_Initial_ValueChanging(System.Nullable<int> value);
        partial void OnInstallation_Meter_32_Initial_ValueChanged();
        partial void OnInstallation_Meter_32_Final_ValueChanging(System.Nullable<int> value);
        partial void OnInstallation_Meter_32_Final_ValueChanged();
        partial void OnInstallation_Float_StatusChanging(System.Nullable<int> value);
        partial void OnInstallation_Float_StatusChanged();
        partial void OnInstallation_Initial_Meters_Coins_InChanging(System.Nullable<int> value);
        partial void OnInstallation_Initial_Meters_Coins_InChanged();
        partial void OnInstallation_Initial_Meters_Coins_OutChanging(System.Nullable<int> value);
        partial void OnInstallation_Initial_Meters_Coins_OutChanged();
        partial void OnInstallation_Initial_Meters_Coin_DropChanging(System.Nullable<int> value);
        partial void OnInstallation_Initial_Meters_Coin_DropChanged();
        partial void OnInstallation_Initial_Meters_External_CreditChanging(System.Nullable<int> value);
        partial void OnInstallation_Initial_Meters_External_CreditChanged();
        partial void OnInstallation_Initial_Meters_Games_BetChanging(System.Nullable<int> value);
        partial void OnInstallation_Initial_Meters_Games_BetChanged();
        partial void OnInstallation_Initial_Meters_Games_WonChanging(System.Nullable<int> value);
        partial void OnInstallation_Initial_Meters_Games_WonChanged();
        partial void OnInstallation_Initial_Meters_NotesChanging(System.Nullable<int> value);
        partial void OnInstallation_Initial_Meters_NotesChanged();
        partial void OnInstallation_Initial_Meters_HandpayChanging(System.Nullable<int> value);
        partial void OnInstallation_Initial_Meters_HandpayChanged();
        partial void OnAnticipated_Removal_DateChanging(string value);
        partial void OnAnticipated_Removal_DateChanged();
        partial void OnRental_Step_Down_DateChanging(string value);
        partial void OnRental_Step_Down_DateChanged();
        partial void OnRent1Changing(System.Nullable<decimal> value);
        partial void OnRent1Changed();
        partial void OnRent2Changing(System.Nullable<decimal> value);
        partial void OnRent2Changed();
        partial void OnLicenceChanging(System.Nullable<decimal> value);
        partial void OnLicenceChanged();
        partial void OnInstallation_Out_OrderChanging(System.Nullable<bool> value);
        partial void OnInstallation_Out_OrderChanged();
        partial void OnInstallation_Counter_Cash_In_UnitsChanging(System.Nullable<int> value);
        partial void OnInstallation_Counter_Cash_In_UnitsChanged();
        partial void OnInstallation_Counter_Cash_Out_UnitsChanging(System.Nullable<int> value);
        partial void OnInstallation_Counter_Cash_Out_UnitsChanged();
        partial void OnInstallation_Counter_Token_In_UnitsChanging(System.Nullable<int> value);
        partial void OnInstallation_Counter_Token_In_UnitsChanged();
        partial void OnInstallation_Counter_Token_Out_UnitsChanging(System.Nullable<int> value);
        partial void OnInstallation_Counter_Token_Out_UnitsChanged();
        partial void OnInstallation_Counter_Refill_UnitsChanging(System.Nullable<int> value);
        partial void OnInstallation_Counter_Refill_UnitsChanged();
        partial void OnInstallation_Counter_Jackpot_UnitsChanging(System.Nullable<int> value);
        partial void OnInstallation_Counter_Jackpot_UnitsChanged();
        partial void OnInstallation_Counter_Prize_UnitsChanging(System.Nullable<int> value);
        partial void OnInstallation_Counter_Prize_UnitsChanged();
        partial void OnInstallation_Counter_Tournament_UnitsChanging(System.Nullable<int> value);
        partial void OnInstallation_Counter_Tournament_UnitsChanged();
        partial void OnInstallation_Counter_Jukebox_Play_UnitsChanging(System.Nullable<int> value);
        partial void OnInstallation_Counter_Jukebox_Play_UnitsChanged();
        partial void OnInstallation_Counter_Jukebox_UnitsChanging(System.Nullable<int> value);
        partial void OnInstallation_Counter_Jukebox_UnitsChanged();
        partial void OnPlanned_Movement_IDChanging(System.Nullable<int> value);
        partial void OnPlanned_Movement_IDChanged();
        partial void OnInstallation_RDC_Machine_CodeChanging(string value);
        partial void OnInstallation_RDC_Machine_CodeChanged();
        partial void OnInstallation_RDC_Secondary_Machine_CodeChanging(string value);
        partial void OnInstallation_RDC_Secondary_Machine_CodeChanged();
        partial void OnInstallation_Token_ValueChanging(int value);
        partial void OnInstallation_Token_ValueChanged();
        partial void OnInstallation_Games_CountChanging(System.Nullable<int> value);
        partial void OnInstallation_Games_CountChanged();
        partial void OnInstallation_StatusChanging(string value);
        partial void OnInstallation_StatusChanged();
        partial void OnGame_Part_NumberChanging(string value);
        partial void OnGame_Part_NumberChanged();
        partial void OnInstallation_MaxBetChanging(System.Nullable<int> value);
        partial void OnInstallation_MaxBetChanged();
        partial void OnIsAuxSerialPortEnabledChanging(bool value);
        partial void OnIsAuxSerialPortEnabledChanged();
        partial void OnIsGatSerialPortEnabledChanging(bool value);
        partial void OnIsGatSerialPortEnabledChanged();
        partial void OnIsSlotLinePortEnabledChanging(bool value);
        partial void OnIsSlotLinePortEnabledChanged();
        partial void OnPort_Disabled_StatusChanging(bool value);
        partial void OnPort_Disabled_StatusChanged();
        partial void OnVoucher_Expire_StatusChanging(System.Nullable<char> value);
        partial void OnVoucher_Expire_StatusChanged();
        partial void OnFinalCollection_StatusChanging(byte value);
        partial void OnFinalCollection_StatusChanged();
        #endregion

        public Installation()
        {
            OnCreated();
        }

        [Column(Storage = "_Installation_No", AutoSync = AutoSync.OnInsert, DbType = "Int NOT NULL IDENTITY", IsPrimaryKey = true, IsDbGenerated = true)]
        public int Installation_No
        {
            get
            {
                return this._Installation_No;
            }
            set
            {
                if ((this._Installation_No != value))
                {
                    this.OnInstallation_NoChanging(value);
                    this.SendPropertyChanging();
                    this._Installation_No = value;
                    this.SendPropertyChanged("Installation_No");
                    this.OnInstallation_NoChanged();
                }
            }
        }

        [Column(Storage = "_Bar_Pos_No", DbType = "Int")]
        public System.Nullable<int> Bar_Pos_No
        {
            get
            {
                return this._Bar_Pos_No;
            }
            set
            {
                if ((this._Bar_Pos_No != value))
                {
                    this.OnBar_Pos_NoChanging(value);
                    this.SendPropertyChanging();
                    this._Bar_Pos_No = value;
                    this.SendPropertyChanged("Bar_Pos_No");
                    this.OnBar_Pos_NoChanged();
                }
            }
        }

        [Column(Storage = "_Machine_No", DbType = "Int")]
        public System.Nullable<int> Machine_No
        {
            get
            {
                return this._Machine_No;
            }
            set
            {
                if ((this._Machine_No != value))
                {
                    this.OnMachine_NoChanging(value);
                    this.SendPropertyChanging();
                    this._Machine_No = value;
                    this.SendPropertyChanged("Machine_No");
                    this.OnMachine_NoChanged();
                }
            }
        }

        [Column(Storage = "_Datapak_No", DbType = "Int")]
        public System.Nullable<int> Datapak_No
        {
            get
            {
                return this._Datapak_No;
            }
            set
            {
                if ((this._Datapak_No != value))
                {
                    this.OnDatapak_NoChanging(value);
                    this.SendPropertyChanging();
                    this._Datapak_No = value;
                    this.SendPropertyChanged("Datapak_No");
                    this.OnDatapak_NoChanged();
                }
            }
        }

        [Column(Storage = "_Datapak_Variant", DbType = "Int")]
        public System.Nullable<int> Datapak_Variant
        {
            get
            {
                return this._Datapak_Variant;
            }
            set
            {
                if ((this._Datapak_Variant != value))
                {
                    this.OnDatapak_VariantChanging(value);
                    this.SendPropertyChanging();
                    this._Datapak_Variant = value;
                    this.SendPropertyChanged("Datapak_Variant");
                    this.OnDatapak_VariantChanged();
                }
            }
        }

        [Column(Storage = "_HQ_Installation_No", DbType = "Int")]
        public System.Nullable<int> HQ_Installation_No
        {
            get
            {
                return this._HQ_Installation_No;
            }
            set
            {
                if ((this._HQ_Installation_No != value))
                {
                    this.OnHQ_Installation_NoChanging(value);
                    this.SendPropertyChanging();
                    this._HQ_Installation_No = value;
                    this.SendPropertyChanged("HQ_Installation_No");
                    this.OnHQ_Installation_NoChanged();
                }
            }
        }

        [Column(Storage = "_Installation_Reference", DbType = "VarChar(50)")]
        public string Installation_Reference
        {
            get
            {
                return this._Installation_Reference;
            }
            set
            {
                if ((this._Installation_Reference != value))
                {
                    this.OnInstallation_ReferenceChanging(value);
                    this.SendPropertyChanging();
                    this._Installation_Reference = value;
                    this.SendPropertyChanged("Installation_Reference");
                    this.OnInstallation_ReferenceChanged();
                }
            }
        }

        [Column(Storage = "_Start_Date", DbType = "DateTime")]
        public System.Nullable<System.DateTime> Start_Date
        {
            get
            {
                return this._Start_Date;
            }
            set
            {
                if ((this._Start_Date != value))
                {
                    this.OnStart_DateChanging(value);
                    this.SendPropertyChanging();
                    this._Start_Date = value;
                    this.SendPropertyChanged("Start_Date");
                    this.OnStart_DateChanged();
                }
            }
        }

        [Column(Storage = "_End_Date", DbType = "DateTime")]
        public System.Nullable<System.DateTime> End_Date
        {
            get
            {
                return this._End_Date;
            }
            set
            {
                if ((this._End_Date != value))
                {
                    this.OnEnd_DateChanging(value);
                    this.SendPropertyChanging();
                    this._End_Date = value;
                    this.SendPropertyChanged("End_Date");
                    this.OnEnd_DateChanged();
                }
            }
        }

        [Column(Storage = "_Anticipated_Percentage_Payout", DbType = "Real")]
        public System.Nullable<float> Anticipated_Percentage_Payout
        {
            get
            {
                return this._Anticipated_Percentage_Payout;
            }
            set
            {
                if ((this._Anticipated_Percentage_Payout != value))
                {
                    this.OnAnticipated_Percentage_PayoutChanging(value);
                    this.SendPropertyChanging();
                    this._Anticipated_Percentage_Payout = value;
                    this.SendPropertyChanged("Anticipated_Percentage_Payout");
                    this.OnAnticipated_Percentage_PayoutChanged();
                }
            }
        }

        [Column(Storage = "_Coins_In_Counter", DbType = "Int")]
        public System.Nullable<int> Coins_In_Counter
        {
            get
            {
                return this._Coins_In_Counter;
            }
            set
            {
                if ((this._Coins_In_Counter != value))
                {
                    this.OnCoins_In_CounterChanging(value);
                    this.SendPropertyChanging();
                    this._Coins_In_Counter = value;
                    this.SendPropertyChanged("Coins_In_Counter");
                    this.OnCoins_In_CounterChanged();
                }
            }
        }

        [Column(Storage = "_Coins_Out_Counter", DbType = "Int")]
        public System.Nullable<int> Coins_Out_Counter
        {
            get
            {
                return this._Coins_Out_Counter;
            }
            set
            {
                if ((this._Coins_Out_Counter != value))
                {
                    this.OnCoins_Out_CounterChanging(value);
                    this.SendPropertyChanging();
                    this._Coins_Out_Counter = value;
                    this.SendPropertyChanged("Coins_Out_Counter");
                    this.OnCoins_Out_CounterChanged();
                }
            }
        }

        [Column(Storage = "_Tokens_In_Counter", DbType = "Int")]
        public System.Nullable<int> Tokens_In_Counter
        {
            get
            {
                return this._Tokens_In_Counter;
            }
            set
            {
                if ((this._Tokens_In_Counter != value))
                {
                    this.OnTokens_In_CounterChanging(value);
                    this.SendPropertyChanging();
                    this._Tokens_In_Counter = value;
                    this.SendPropertyChanged("Tokens_In_Counter");
                    this.OnTokens_In_CounterChanged();
                }
            }
        }

        [Column(Storage = "_Tokens_Out_Counter", DbType = "Int")]
        public System.Nullable<int> Tokens_Out_Counter
        {
            get
            {
                return this._Tokens_Out_Counter;
            }
            set
            {
                if ((this._Tokens_Out_Counter != value))
                {
                    this.OnTokens_Out_CounterChanging(value);
                    this.SendPropertyChanging();
                    this._Tokens_Out_Counter = value;
                    this.SendPropertyChanged("Tokens_Out_Counter");
                    this.OnTokens_Out_CounterChanged();
                }
            }
        }

        [Column(Storage = "_Prize_Counter", DbType = "Int")]
        public System.Nullable<int> Prize_Counter
        {
            get
            {
                return this._Prize_Counter;
            }
            set
            {
                if ((this._Prize_Counter != value))
                {
                    this.OnPrize_CounterChanging(value);
                    this.SendPropertyChanging();
                    this._Prize_Counter = value;
                    this.SendPropertyChanged("Prize_Counter");
                    this.OnPrize_CounterChanged();
                }
            }
        }

        [Column(Storage = "_Refill_Counter", DbType = "Int")]
        public System.Nullable<int> Refill_Counter
        {
            get
            {
                return this._Refill_Counter;
            }
            set
            {
                if ((this._Refill_Counter != value))
                {
                    this.OnRefill_CounterChanging(value);
                    this.SendPropertyChanging();
                    this._Refill_Counter = value;
                    this.SendPropertyChanged("Refill_Counter");
                    this.OnRefill_CounterChanged();
                }
            }
        }

        [Column(Storage = "_Tournament_Counter", DbType = "Int")]
        public System.Nullable<int> Tournament_Counter
        {
            get
            {
                return this._Tournament_Counter;
            }
            set
            {
                if ((this._Tournament_Counter != value))
                {
                    this.OnTournament_CounterChanging(value);
                    this.SendPropertyChanging();
                    this._Tournament_Counter = value;
                    this.SendPropertyChanged("Tournament_Counter");
                    this.OnTournament_CounterChanged();
                }
            }
        }

        [Column(Storage = "_Jukebox_Counter", DbType = "Int")]
        public System.Nullable<int> Jukebox_Counter
        {
            get
            {
                return this._Jukebox_Counter;
            }
            set
            {
                if ((this._Jukebox_Counter != value))
                {
                    this.OnJukebox_CounterChanging(value);
                    this.SendPropertyChanging();
                    this._Jukebox_Counter = value;
                    this.SendPropertyChanged("Jukebox_Counter");
                    this.OnJukebox_CounterChanged();
                }
            }
        }

        [Column(Storage = "_Previous_Installation", DbType = "Int")]
        public System.Nullable<int> Previous_Installation
        {
            get
            {
                return this._Previous_Installation;
            }
            set
            {
                if ((this._Previous_Installation != value))
                {
                    this.OnPrevious_InstallationChanging(value);
                    this.SendPropertyChanging();
                    this._Previous_Installation = value;
                    this.SendPropertyChanged("Previous_Installation");
                    this.OnPrevious_InstallationChanged();
                }
            }
        }

        [Column(Storage = "_Bagged_Cash_Installation_No", DbType = "Real")]
        public System.Nullable<float> Bagged_Cash_Installation_No
        {
            get
            {
                return this._Bagged_Cash_Installation_No;
            }
            set
            {
                if ((this._Bagged_Cash_Installation_No != value))
                {
                    this.OnBagged_Cash_Installation_NoChanging(value);
                    this.SendPropertyChanging();
                    this._Bagged_Cash_Installation_No = value;
                    this.SendPropertyChanged("Bagged_Cash_Installation_No");
                    this.OnBagged_Cash_Installation_NoChanged();
                }
            }
        }

        [Column(Storage = "_Bagged_Cash_Amount", DbType = "Real")]
        public System.Nullable<float> Bagged_Cash_Amount
        {
            get
            {
                return this._Bagged_Cash_Amount;
            }
            set
            {
                if ((this._Bagged_Cash_Amount != value))
                {
                    this.OnBagged_Cash_AmountChanging(value);
                    this.SendPropertyChanging();
                    this._Bagged_Cash_Amount = value;
                    this.SendPropertyChanged("Bagged_Cash_Amount");
                    this.OnBagged_Cash_AmountChanged();
                }
            }
        }

        [Column(Storage = "_Bagged_Cash_Float", DbType = "Real")]
        public System.Nullable<float> Bagged_Cash_Float
        {
            get
            {
                return this._Bagged_Cash_Float;
            }
            set
            {
                if ((this._Bagged_Cash_Float != value))
                {
                    this.OnBagged_Cash_FloatChanging(value);
                    this.SendPropertyChanging();
                    this._Bagged_Cash_Float = value;
                    this.SendPropertyChanged("Bagged_Cash_Float");
                    this.OnBagged_Cash_FloatChanged();
                }
            }
        }

        [Column(Storage = "_Installation_Out_Of_Order", DbType = "Bit")]
        public System.Nullable<bool> Installation_Out_Of_Order
        {
            get
            {
                return this._Installation_Out_Of_Order;
            }
            set
            {
                if ((this._Installation_Out_Of_Order != value))
                {
                    this.OnInstallation_Out_Of_OrderChanging(value);
                    this.SendPropertyChanging();
                    this._Installation_Out_Of_Order = value;
                    this.SendPropertyChanged("Installation_Out_Of_Order");
                    this.OnInstallation_Out_Of_OrderChanged();
                }
            }
        }

        [Column(Storage = "_Float_Issued", DbType = "Real")]
        public System.Nullable<float> Float_Issued
        {
            get
            {
                return this._Float_Issued;
            }
            set
            {
                if ((this._Float_Issued != value))
                {
                    this.OnFloat_IssuedChanging(value);
                    this.SendPropertyChanging();
                    this._Float_Issued = value;
                    this.SendPropertyChanged("Float_Issued");
                    this.OnFloat_IssuedChanged();
                }
            }
        }

        [Column(Storage = "_Float_Issued_By", DbType = "VarChar(50)")]
        public string Float_Issued_By
        {
            get
            {
                return this._Float_Issued_By;
            }
            set
            {
                if ((this._Float_Issued_By != value))
                {
                    this.OnFloat_Issued_ByChanging(value);
                    this.SendPropertyChanging();
                    this._Float_Issued_By = value;
                    this.SendPropertyChanged("Float_Issued_By");
                    this.OnFloat_Issued_ByChanged();
                }
            }
        }

        [Column(Storage = "_Installation_Price_Of_Play", DbType = "Int")]
        public System.Nullable<int> Installation_Price_Of_Play
        {
            get
            {
                return this._Installation_Price_Of_Play;
            }
            set
            {
                if ((this._Installation_Price_Of_Play != value))
                {
                    this.OnInstallation_Price_Of_PlayChanging(value);
                    this.SendPropertyChanging();
                    this._Installation_Price_Of_Play = value;
                    this.SendPropertyChanged("Installation_Price_Of_Play");
                    this.OnInstallation_Price_Of_PlayChanged();
                }
            }
        }

        [Column(Storage = "_Installation_Jackpot", DbType = "Int")]
        public System.Nullable<int> Installation_Jackpot
        {
            get
            {
                return this._Installation_Jackpot;
            }
            set
            {
                if ((this._Installation_Jackpot != value))
                {
                    this.OnInstallation_JackpotChanging(value);
                    this.SendPropertyChanging();
                    this._Installation_Jackpot = value;
                    this.SendPropertyChanged("Installation_Jackpot");
                    this.OnInstallation_JackpotChanged();
                }
            }
        }

        [Column(Storage = "_Installation_Meter_1_Initial_Value", DbType = "Int")]
        public System.Nullable<int> Installation_Meter_1_Initial_Value
        {
            get
            {
                return this._Installation_Meter_1_Initial_Value;
            }
            set
            {
                if ((this._Installation_Meter_1_Initial_Value != value))
                {
                    this.OnInstallation_Meter_1_Initial_ValueChanging(value);
                    this.SendPropertyChanging();
                    this._Installation_Meter_1_Initial_Value = value;
                    this.SendPropertyChanged("Installation_Meter_1_Initial_Value");
                    this.OnInstallation_Meter_1_Initial_ValueChanged();
                }
            }
        }

        [Column(Storage = "_Installation_Meter_1_Final_Value", DbType = "Int")]
        public System.Nullable<int> Installation_Meter_1_Final_Value
        {
            get
            {
                return this._Installation_Meter_1_Final_Value;
            }
            set
            {
                if ((this._Installation_Meter_1_Final_Value != value))
                {
                    this.OnInstallation_Meter_1_Final_ValueChanging(value);
                    this.SendPropertyChanging();
                    this._Installation_Meter_1_Final_Value = value;
                    this.SendPropertyChanged("Installation_Meter_1_Final_Value");
                    this.OnInstallation_Meter_1_Final_ValueChanged();
                }
            }
        }

        [Column(Storage = "_Installation_Meter_2_Initial_Value", DbType = "Int")]
        public System.Nullable<int> Installation_Meter_2_Initial_Value
        {
            get
            {
                return this._Installation_Meter_2_Initial_Value;
            }
            set
            {
                if ((this._Installation_Meter_2_Initial_Value != value))
                {
                    this.OnInstallation_Meter_2_Initial_ValueChanging(value);
                    this.SendPropertyChanging();
                    this._Installation_Meter_2_Initial_Value = value;
                    this.SendPropertyChanged("Installation_Meter_2_Initial_Value");
                    this.OnInstallation_Meter_2_Initial_ValueChanged();
                }
            }
        }

        [Column(Storage = "_Installation_Meter_2_Final_Value", DbType = "Int")]
        public System.Nullable<int> Installation_Meter_2_Final_Value
        {
            get
            {
                return this._Installation_Meter_2_Final_Value;
            }
            set
            {
                if ((this._Installation_Meter_2_Final_Value != value))
                {
                    this.OnInstallation_Meter_2_Final_ValueChanging(value);
                    this.SendPropertyChanging();
                    this._Installation_Meter_2_Final_Value = value;
                    this.SendPropertyChanged("Installation_Meter_2_Final_Value");
                    this.OnInstallation_Meter_2_Final_ValueChanged();
                }
            }
        }

        [Column(Storage = "_Installation_Meter_3_Initial_Value", DbType = "Int")]
        public System.Nullable<int> Installation_Meter_3_Initial_Value
        {
            get
            {
                return this._Installation_Meter_3_Initial_Value;
            }
            set
            {
                if ((this._Installation_Meter_3_Initial_Value != value))
                {
                    this.OnInstallation_Meter_3_Initial_ValueChanging(value);
                    this.SendPropertyChanging();
                    this._Installation_Meter_3_Initial_Value = value;
                    this.SendPropertyChanged("Installation_Meter_3_Initial_Value");
                    this.OnInstallation_Meter_3_Initial_ValueChanged();
                }
            }
        }

        [Column(Storage = "_Installation_Meter_3_Final_Value", DbType = "Int")]
        public System.Nullable<int> Installation_Meter_3_Final_Value
        {
            get
            {
                return this._Installation_Meter_3_Final_Value;
            }
            set
            {
                if ((this._Installation_Meter_3_Final_Value != value))
                {
                    this.OnInstallation_Meter_3_Final_ValueChanging(value);
                    this.SendPropertyChanging();
                    this._Installation_Meter_3_Final_Value = value;
                    this.SendPropertyChanged("Installation_Meter_3_Final_Value");
                    this.OnInstallation_Meter_3_Final_ValueChanged();
                }
            }
        }

        [Column(Storage = "_Installation_Meter_4_Initial_Value", DbType = "Int")]
        public System.Nullable<int> Installation_Meter_4_Initial_Value
        {
            get
            {
                return this._Installation_Meter_4_Initial_Value;
            }
            set
            {
                if ((this._Installation_Meter_4_Initial_Value != value))
                {
                    this.OnInstallation_Meter_4_Initial_ValueChanging(value);
                    this.SendPropertyChanging();
                    this._Installation_Meter_4_Initial_Value = value;
                    this.SendPropertyChanged("Installation_Meter_4_Initial_Value");
                    this.OnInstallation_Meter_4_Initial_ValueChanged();
                }
            }
        }

        [Column(Storage = "_Installation_Meter_4_Final_Value", DbType = "Int")]
        public System.Nullable<int> Installation_Meter_4_Final_Value
        {
            get
            {
                return this._Installation_Meter_4_Final_Value;
            }
            set
            {
                if ((this._Installation_Meter_4_Final_Value != value))
                {
                    this.OnInstallation_Meter_4_Final_ValueChanging(value);
                    this.SendPropertyChanging();
                    this._Installation_Meter_4_Final_Value = value;
                    this.SendPropertyChanged("Installation_Meter_4_Final_Value");
                    this.OnInstallation_Meter_4_Final_ValueChanged();
                }
            }
        }

        [Column(Storage = "_Installation_Meter_5_Initial_Value", DbType = "Int")]
        public System.Nullable<int> Installation_Meter_5_Initial_Value
        {
            get
            {
                return this._Installation_Meter_5_Initial_Value;
            }
            set
            {
                if ((this._Installation_Meter_5_Initial_Value != value))
                {
                    this.OnInstallation_Meter_5_Initial_ValueChanging(value);
                    this.SendPropertyChanging();
                    this._Installation_Meter_5_Initial_Value = value;
                    this.SendPropertyChanged("Installation_Meter_5_Initial_Value");
                    this.OnInstallation_Meter_5_Initial_ValueChanged();
                }
            }
        }

        [Column(Storage = "_Installation_Meter_5_Final_Value", DbType = "Int")]
        public System.Nullable<int> Installation_Meter_5_Final_Value
        {
            get
            {
                return this._Installation_Meter_5_Final_Value;
            }
            set
            {
                if ((this._Installation_Meter_5_Final_Value != value))
                {
                    this.OnInstallation_Meter_5_Final_ValueChanging(value);
                    this.SendPropertyChanging();
                    this._Installation_Meter_5_Final_Value = value;
                    this.SendPropertyChanged("Installation_Meter_5_Final_Value");
                    this.OnInstallation_Meter_5_Final_ValueChanged();
                }
            }
        }

        [Column(Storage = "_Installation_Meter_6_Initial_Value", DbType = "Int")]
        public System.Nullable<int> Installation_Meter_6_Initial_Value
        {
            get
            {
                return this._Installation_Meter_6_Initial_Value;
            }
            set
            {
                if ((this._Installation_Meter_6_Initial_Value != value))
                {
                    this.OnInstallation_Meter_6_Initial_ValueChanging(value);
                    this.SendPropertyChanging();
                    this._Installation_Meter_6_Initial_Value = value;
                    this.SendPropertyChanged("Installation_Meter_6_Initial_Value");
                    this.OnInstallation_Meter_6_Initial_ValueChanged();
                }
            }
        }

        [Column(Storage = "_Installation_Meter_6_Final_Value", DbType = "Int")]
        public System.Nullable<int> Installation_Meter_6_Final_Value
        {
            get
            {
                return this._Installation_Meter_6_Final_Value;
            }
            set
            {
                if ((this._Installation_Meter_6_Final_Value != value))
                {
                    this.OnInstallation_Meter_6_Final_ValueChanging(value);
                    this.SendPropertyChanging();
                    this._Installation_Meter_6_Final_Value = value;
                    this.SendPropertyChanged("Installation_Meter_6_Final_Value");
                    this.OnInstallation_Meter_6_Final_ValueChanged();
                }
            }
        }

        [Column(Storage = "_Installation_Meter_7_Initial_Value", DbType = "Int")]
        public System.Nullable<int> Installation_Meter_7_Initial_Value
        {
            get
            {
                return this._Installation_Meter_7_Initial_Value;
            }
            set
            {
                if ((this._Installation_Meter_7_Initial_Value != value))
                {
                    this.OnInstallation_Meter_7_Initial_ValueChanging(value);
                    this.SendPropertyChanging();
                    this._Installation_Meter_7_Initial_Value = value;
                    this.SendPropertyChanged("Installation_Meter_7_Initial_Value");
                    this.OnInstallation_Meter_7_Initial_ValueChanged();
                }
            }
        }

        [Column(Storage = "_Installation_Meter_7_Final_Value", DbType = "Int")]
        public System.Nullable<int> Installation_Meter_7_Final_Value
        {
            get
            {
                return this._Installation_Meter_7_Final_Value;
            }
            set
            {
                if ((this._Installation_Meter_7_Final_Value != value))
                {
                    this.OnInstallation_Meter_7_Final_ValueChanging(value);
                    this.SendPropertyChanging();
                    this._Installation_Meter_7_Final_Value = value;
                    this.SendPropertyChanged("Installation_Meter_7_Final_Value");
                    this.OnInstallation_Meter_7_Final_ValueChanged();
                }
            }
        }

        [Column(Storage = "_Installation_Meter_8_Initial_Value", DbType = "Int")]
        public System.Nullable<int> Installation_Meter_8_Initial_Value
        {
            get
            {
                return this._Installation_Meter_8_Initial_Value;
            }
            set
            {
                if ((this._Installation_Meter_8_Initial_Value != value))
                {
                    this.OnInstallation_Meter_8_Initial_ValueChanging(value);
                    this.SendPropertyChanging();
                    this._Installation_Meter_8_Initial_Value = value;
                    this.SendPropertyChanged("Installation_Meter_8_Initial_Value");
                    this.OnInstallation_Meter_8_Initial_ValueChanged();
                }
            }
        }

        [Column(Storage = "_Installation_Meter_8_Final_Value", DbType = "Int")]
        public System.Nullable<int> Installation_Meter_8_Final_Value
        {
            get
            {
                return this._Installation_Meter_8_Final_Value;
            }
            set
            {
                if ((this._Installation_Meter_8_Final_Value != value))
                {
                    this.OnInstallation_Meter_8_Final_ValueChanging(value);
                    this.SendPropertyChanging();
                    this._Installation_Meter_8_Final_Value = value;
                    this.SendPropertyChanged("Installation_Meter_8_Final_Value");
                    this.OnInstallation_Meter_8_Final_ValueChanged();
                }
            }
        }

        [Column(Storage = "_Installation_Meter_9_Initial_Value", DbType = "Int")]
        public System.Nullable<int> Installation_Meter_9_Initial_Value
        {
            get
            {
                return this._Installation_Meter_9_Initial_Value;
            }
            set
            {
                if ((this._Installation_Meter_9_Initial_Value != value))
                {
                    this.OnInstallation_Meter_9_Initial_ValueChanging(value);
                    this.SendPropertyChanging();
                    this._Installation_Meter_9_Initial_Value = value;
                    this.SendPropertyChanged("Installation_Meter_9_Initial_Value");
                    this.OnInstallation_Meter_9_Initial_ValueChanged();
                }
            }
        }

        [Column(Storage = "_Installation_Meter_9_Final_Value", DbType = "Int")]
        public System.Nullable<int> Installation_Meter_9_Final_Value
        {
            get
            {
                return this._Installation_Meter_9_Final_Value;
            }
            set
            {
                if ((this._Installation_Meter_9_Final_Value != value))
                {
                    this.OnInstallation_Meter_9_Final_ValueChanging(value);
                    this.SendPropertyChanging();
                    this._Installation_Meter_9_Final_Value = value;
                    this.SendPropertyChanged("Installation_Meter_9_Final_Value");
                    this.OnInstallation_Meter_9_Final_ValueChanged();
                }
            }
        }

        [Column(Storage = "_Installation_Meter_10_Initial_Value", DbType = "Int")]
        public System.Nullable<int> Installation_Meter_10_Initial_Value
        {
            get
            {
                return this._Installation_Meter_10_Initial_Value;
            }
            set
            {
                if ((this._Installation_Meter_10_Initial_Value != value))
                {
                    this.OnInstallation_Meter_10_Initial_ValueChanging(value);
                    this.SendPropertyChanging();
                    this._Installation_Meter_10_Initial_Value = value;
                    this.SendPropertyChanged("Installation_Meter_10_Initial_Value");
                    this.OnInstallation_Meter_10_Initial_ValueChanged();
                }
            }
        }

        [Column(Storage = "_Installation_Meter_10_Final_Value", DbType = "Int")]
        public System.Nullable<int> Installation_Meter_10_Final_Value
        {
            get
            {
                return this._Installation_Meter_10_Final_Value;
            }
            set
            {
                if ((this._Installation_Meter_10_Final_Value != value))
                {
                    this.OnInstallation_Meter_10_Final_ValueChanging(value);
                    this.SendPropertyChanging();
                    this._Installation_Meter_10_Final_Value = value;
                    this.SendPropertyChanged("Installation_Meter_10_Final_Value");
                    this.OnInstallation_Meter_10_Final_ValueChanged();
                }
            }
        }

        [Column(Storage = "_Installation_Meter_11_Initial_Value", DbType = "Int")]
        public System.Nullable<int> Installation_Meter_11_Initial_Value
        {
            get
            {
                return this._Installation_Meter_11_Initial_Value;
            }
            set
            {
                if ((this._Installation_Meter_11_Initial_Value != value))
                {
                    this.OnInstallation_Meter_11_Initial_ValueChanging(value);
                    this.SendPropertyChanging();
                    this._Installation_Meter_11_Initial_Value = value;
                    this.SendPropertyChanged("Installation_Meter_11_Initial_Value");
                    this.OnInstallation_Meter_11_Initial_ValueChanged();
                }
            }
        }

        [Column(Storage = "_Installation_Meter_11_Final_Value", DbType = "Int")]
        public System.Nullable<int> Installation_Meter_11_Final_Value
        {
            get
            {
                return this._Installation_Meter_11_Final_Value;
            }
            set
            {
                if ((this._Installation_Meter_11_Final_Value != value))
                {
                    this.OnInstallation_Meter_11_Final_ValueChanging(value);
                    this.SendPropertyChanging();
                    this._Installation_Meter_11_Final_Value = value;
                    this.SendPropertyChanged("Installation_Meter_11_Final_Value");
                    this.OnInstallation_Meter_11_Final_ValueChanged();
                }
            }
        }

        [Column(Storage = "_Installation_Meter_12_Initial_Value", DbType = "Int")]
        public System.Nullable<int> Installation_Meter_12_Initial_Value
        {
            get
            {
                return this._Installation_Meter_12_Initial_Value;
            }
            set
            {
                if ((this._Installation_Meter_12_Initial_Value != value))
                {
                    this.OnInstallation_Meter_12_Initial_ValueChanging(value);
                    this.SendPropertyChanging();
                    this._Installation_Meter_12_Initial_Value = value;
                    this.SendPropertyChanged("Installation_Meter_12_Initial_Value");
                    this.OnInstallation_Meter_12_Initial_ValueChanged();
                }
            }
        }

        [Column(Storage = "_Installation_Meter_12_Final_Value", DbType = "Int")]
        public System.Nullable<int> Installation_Meter_12_Final_Value
        {
            get
            {
                return this._Installation_Meter_12_Final_Value;
            }
            set
            {
                if ((this._Installation_Meter_12_Final_Value != value))
                {
                    this.OnInstallation_Meter_12_Final_ValueChanging(value);
                    this.SendPropertyChanging();
                    this._Installation_Meter_12_Final_Value = value;
                    this.SendPropertyChanged("Installation_Meter_12_Final_Value");
                    this.OnInstallation_Meter_12_Final_ValueChanged();
                }
            }
        }

        [Column(Storage = "_Installation_Meter_13_Initial_Value", DbType = "Int")]
        public System.Nullable<int> Installation_Meter_13_Initial_Value
        {
            get
            {
                return this._Installation_Meter_13_Initial_Value;
            }
            set
            {
                if ((this._Installation_Meter_13_Initial_Value != value))
                {
                    this.OnInstallation_Meter_13_Initial_ValueChanging(value);
                    this.SendPropertyChanging();
                    this._Installation_Meter_13_Initial_Value = value;
                    this.SendPropertyChanged("Installation_Meter_13_Initial_Value");
                    this.OnInstallation_Meter_13_Initial_ValueChanged();
                }
            }
        }

        [Column(Storage = "_Installation_Meter_13_Final_Value", DbType = "Int")]
        public System.Nullable<int> Installation_Meter_13_Final_Value
        {
            get
            {
                return this._Installation_Meter_13_Final_Value;
            }
            set
            {
                if ((this._Installation_Meter_13_Final_Value != value))
                {
                    this.OnInstallation_Meter_13_Final_ValueChanging(value);
                    this.SendPropertyChanging();
                    this._Installation_Meter_13_Final_Value = value;
                    this.SendPropertyChanged("Installation_Meter_13_Final_Value");
                    this.OnInstallation_Meter_13_Final_ValueChanged();
                }
            }
        }

        [Column(Storage = "_Installation_Meter_14_Initial_Value", DbType = "Int")]
        public System.Nullable<int> Installation_Meter_14_Initial_Value
        {
            get
            {
                return this._Installation_Meter_14_Initial_Value;
            }
            set
            {
                if ((this._Installation_Meter_14_Initial_Value != value))
                {
                    this.OnInstallation_Meter_14_Initial_ValueChanging(value);
                    this.SendPropertyChanging();
                    this._Installation_Meter_14_Initial_Value = value;
                    this.SendPropertyChanged("Installation_Meter_14_Initial_Value");
                    this.OnInstallation_Meter_14_Initial_ValueChanged();
                }
            }
        }

        [Column(Storage = "_Installation_Meter_14_Final_Value", DbType = "Int")]
        public System.Nullable<int> Installation_Meter_14_Final_Value
        {
            get
            {
                return this._Installation_Meter_14_Final_Value;
            }
            set
            {
                if ((this._Installation_Meter_14_Final_Value != value))
                {
                    this.OnInstallation_Meter_14_Final_ValueChanging(value);
                    this.SendPropertyChanging();
                    this._Installation_Meter_14_Final_Value = value;
                    this.SendPropertyChanged("Installation_Meter_14_Final_Value");
                    this.OnInstallation_Meter_14_Final_ValueChanged();
                }
            }
        }

        [Column(Storage = "_Installation_Meter_15_Initial_Value", DbType = "Int")]
        public System.Nullable<int> Installation_Meter_15_Initial_Value
        {
            get
            {
                return this._Installation_Meter_15_Initial_Value;
            }
            set
            {
                if ((this._Installation_Meter_15_Initial_Value != value))
                {
                    this.OnInstallation_Meter_15_Initial_ValueChanging(value);
                    this.SendPropertyChanging();
                    this._Installation_Meter_15_Initial_Value = value;
                    this.SendPropertyChanged("Installation_Meter_15_Initial_Value");
                    this.OnInstallation_Meter_15_Initial_ValueChanged();
                }
            }
        }

        [Column(Storage = "_Installation_Meter_15_Final_Value", DbType = "Int")]
        public System.Nullable<int> Installation_Meter_15_Final_Value
        {
            get
            {
                return this._Installation_Meter_15_Final_Value;
            }
            set
            {
                if ((this._Installation_Meter_15_Final_Value != value))
                {
                    this.OnInstallation_Meter_15_Final_ValueChanging(value);
                    this.SendPropertyChanging();
                    this._Installation_Meter_15_Final_Value = value;
                    this.SendPropertyChanged("Installation_Meter_15_Final_Value");
                    this.OnInstallation_Meter_15_Final_ValueChanged();
                }
            }
        }

        [Column(Storage = "_Installation_Meter_16_Initial_Value", DbType = "Int")]
        public System.Nullable<int> Installation_Meter_16_Initial_Value
        {
            get
            {
                return this._Installation_Meter_16_Initial_Value;
            }
            set
            {
                if ((this._Installation_Meter_16_Initial_Value != value))
                {
                    this.OnInstallation_Meter_16_Initial_ValueChanging(value);
                    this.SendPropertyChanging();
                    this._Installation_Meter_16_Initial_Value = value;
                    this.SendPropertyChanged("Installation_Meter_16_Initial_Value");
                    this.OnInstallation_Meter_16_Initial_ValueChanged();
                }
            }
        }

        [Column(Storage = "_Installation_Meter_16_Final_Value", DbType = "Int")]
        public System.Nullable<int> Installation_Meter_16_Final_Value
        {
            get
            {
                return this._Installation_Meter_16_Final_Value;
            }
            set
            {
                if ((this._Installation_Meter_16_Final_Value != value))
                {
                    this.OnInstallation_Meter_16_Final_ValueChanging(value);
                    this.SendPropertyChanging();
                    this._Installation_Meter_16_Final_Value = value;
                    this.SendPropertyChanged("Installation_Meter_16_Final_Value");
                    this.OnInstallation_Meter_16_Final_ValueChanged();
                }
            }
        }

        [Column(Storage = "_Installation_Meter_17_Initial_Value", DbType = "Int")]
        public System.Nullable<int> Installation_Meter_17_Initial_Value
        {
            get
            {
                return this._Installation_Meter_17_Initial_Value;
            }
            set
            {
                if ((this._Installation_Meter_17_Initial_Value != value))
                {
                    this.OnInstallation_Meter_17_Initial_ValueChanging(value);
                    this.SendPropertyChanging();
                    this._Installation_Meter_17_Initial_Value = value;
                    this.SendPropertyChanged("Installation_Meter_17_Initial_Value");
                    this.OnInstallation_Meter_17_Initial_ValueChanged();
                }
            }
        }

        [Column(Storage = "_Installation_Meter_17_Final_Value", DbType = "Int")]
        public System.Nullable<int> Installation_Meter_17_Final_Value
        {
            get
            {
                return this._Installation_Meter_17_Final_Value;
            }
            set
            {
                if ((this._Installation_Meter_17_Final_Value != value))
                {
                    this.OnInstallation_Meter_17_Final_ValueChanging(value);
                    this.SendPropertyChanging();
                    this._Installation_Meter_17_Final_Value = value;
                    this.SendPropertyChanged("Installation_Meter_17_Final_Value");
                    this.OnInstallation_Meter_17_Final_ValueChanged();
                }
            }
        }

        [Column(Storage = "_Installation_Meter_18_Initial_Value", DbType = "Int")]
        public System.Nullable<int> Installation_Meter_18_Initial_Value
        {
            get
            {
                return this._Installation_Meter_18_Initial_Value;
            }
            set
            {
                if ((this._Installation_Meter_18_Initial_Value != value))
                {
                    this.OnInstallation_Meter_18_Initial_ValueChanging(value);
                    this.SendPropertyChanging();
                    this._Installation_Meter_18_Initial_Value = value;
                    this.SendPropertyChanged("Installation_Meter_18_Initial_Value");
                    this.OnInstallation_Meter_18_Initial_ValueChanged();
                }
            }
        }

        [Column(Storage = "_Installation_Meter_18_Final_Value", DbType = "Int")]
        public System.Nullable<int> Installation_Meter_18_Final_Value
        {
            get
            {
                return this._Installation_Meter_18_Final_Value;
            }
            set
            {
                if ((this._Installation_Meter_18_Final_Value != value))
                {
                    this.OnInstallation_Meter_18_Final_ValueChanging(value);
                    this.SendPropertyChanging();
                    this._Installation_Meter_18_Final_Value = value;
                    this.SendPropertyChanged("Installation_Meter_18_Final_Value");
                    this.OnInstallation_Meter_18_Final_ValueChanged();
                }
            }
        }

        [Column(Storage = "_Installation_Meter_19_Initial_Value", DbType = "Int")]
        public System.Nullable<int> Installation_Meter_19_Initial_Value
        {
            get
            {
                return this._Installation_Meter_19_Initial_Value;
            }
            set
            {
                if ((this._Installation_Meter_19_Initial_Value != value))
                {
                    this.OnInstallation_Meter_19_Initial_ValueChanging(value);
                    this.SendPropertyChanging();
                    this._Installation_Meter_19_Initial_Value = value;
                    this.SendPropertyChanged("Installation_Meter_19_Initial_Value");
                    this.OnInstallation_Meter_19_Initial_ValueChanged();
                }
            }
        }

        [Column(Storage = "_Installation_Meter_19_Final_Value", DbType = "Int")]
        public System.Nullable<int> Installation_Meter_19_Final_Value
        {
            get
            {
                return this._Installation_Meter_19_Final_Value;
            }
            set
            {
                if ((this._Installation_Meter_19_Final_Value != value))
                {
                    this.OnInstallation_Meter_19_Final_ValueChanging(value);
                    this.SendPropertyChanging();
                    this._Installation_Meter_19_Final_Value = value;
                    this.SendPropertyChanged("Installation_Meter_19_Final_Value");
                    this.OnInstallation_Meter_19_Final_ValueChanged();
                }
            }
        }

        [Column(Storage = "_Installation_Meter_20_Initial_Value", DbType = "Int")]
        public System.Nullable<int> Installation_Meter_20_Initial_Value
        {
            get
            {
                return this._Installation_Meter_20_Initial_Value;
            }
            set
            {
                if ((this._Installation_Meter_20_Initial_Value != value))
                {
                    this.OnInstallation_Meter_20_Initial_ValueChanging(value);
                    this.SendPropertyChanging();
                    this._Installation_Meter_20_Initial_Value = value;
                    this.SendPropertyChanged("Installation_Meter_20_Initial_Value");
                    this.OnInstallation_Meter_20_Initial_ValueChanged();
                }
            }
        }

        [Column(Storage = "_Installation_Meter_20_Final_Value", DbType = "Int")]
        public System.Nullable<int> Installation_Meter_20_Final_Value
        {
            get
            {
                return this._Installation_Meter_20_Final_Value;
            }
            set
            {
                if ((this._Installation_Meter_20_Final_Value != value))
                {
                    this.OnInstallation_Meter_20_Final_ValueChanging(value);
                    this.SendPropertyChanging();
                    this._Installation_Meter_20_Final_Value = value;
                    this.SendPropertyChanged("Installation_Meter_20_Final_Value");
                    this.OnInstallation_Meter_20_Final_ValueChanged();
                }
            }
        }

        [Column(Storage = "_Installation_Meter_21_Initial_Value", DbType = "Int")]
        public System.Nullable<int> Installation_Meter_21_Initial_Value
        {
            get
            {
                return this._Installation_Meter_21_Initial_Value;
            }
            set
            {
                if ((this._Installation_Meter_21_Initial_Value != value))
                {
                    this.OnInstallation_Meter_21_Initial_ValueChanging(value);
                    this.SendPropertyChanging();
                    this._Installation_Meter_21_Initial_Value = value;
                    this.SendPropertyChanged("Installation_Meter_21_Initial_Value");
                    this.OnInstallation_Meter_21_Initial_ValueChanged();
                }
            }
        }

        [Column(Storage = "_Installation_Meter_21_Final_Value", DbType = "Int")]
        public System.Nullable<int> Installation_Meter_21_Final_Value
        {
            get
            {
                return this._Installation_Meter_21_Final_Value;
            }
            set
            {
                if ((this._Installation_Meter_21_Final_Value != value))
                {
                    this.OnInstallation_Meter_21_Final_ValueChanging(value);
                    this.SendPropertyChanging();
                    this._Installation_Meter_21_Final_Value = value;
                    this.SendPropertyChanged("Installation_Meter_21_Final_Value");
                    this.OnInstallation_Meter_21_Final_ValueChanged();
                }
            }
        }

        [Column(Storage = "_Installation_Meter_22_Initial_Value", DbType = "Int")]
        public System.Nullable<int> Installation_Meter_22_Initial_Value
        {
            get
            {
                return this._Installation_Meter_22_Initial_Value;
            }
            set
            {
                if ((this._Installation_Meter_22_Initial_Value != value))
                {
                    this.OnInstallation_Meter_22_Initial_ValueChanging(value);
                    this.SendPropertyChanging();
                    this._Installation_Meter_22_Initial_Value = value;
                    this.SendPropertyChanged("Installation_Meter_22_Initial_Value");
                    this.OnInstallation_Meter_22_Initial_ValueChanged();
                }
            }
        }

        [Column(Storage = "_Installation_Meter_22_Final_Value", DbType = "Int")]
        public System.Nullable<int> Installation_Meter_22_Final_Value
        {
            get
            {
                return this._Installation_Meter_22_Final_Value;
            }
            set
            {
                if ((this._Installation_Meter_22_Final_Value != value))
                {
                    this.OnInstallation_Meter_22_Final_ValueChanging(value);
                    this.SendPropertyChanging();
                    this._Installation_Meter_22_Final_Value = value;
                    this.SendPropertyChanged("Installation_Meter_22_Final_Value");
                    this.OnInstallation_Meter_22_Final_ValueChanged();
                }
            }
        }

        [Column(Storage = "_Installation_Meter_23_Initial_Value", DbType = "Int")]
        public System.Nullable<int> Installation_Meter_23_Initial_Value
        {
            get
            {
                return this._Installation_Meter_23_Initial_Value;
            }
            set
            {
                if ((this._Installation_Meter_23_Initial_Value != value))
                {
                    this.OnInstallation_Meter_23_Initial_ValueChanging(value);
                    this.SendPropertyChanging();
                    this._Installation_Meter_23_Initial_Value = value;
                    this.SendPropertyChanged("Installation_Meter_23_Initial_Value");
                    this.OnInstallation_Meter_23_Initial_ValueChanged();
                }
            }
        }

        [Column(Storage = "_Installation_Meter_23_Final_Value", DbType = "Int")]
        public System.Nullable<int> Installation_Meter_23_Final_Value
        {
            get
            {
                return this._Installation_Meter_23_Final_Value;
            }
            set
            {
                if ((this._Installation_Meter_23_Final_Value != value))
                {
                    this.OnInstallation_Meter_23_Final_ValueChanging(value);
                    this.SendPropertyChanging();
                    this._Installation_Meter_23_Final_Value = value;
                    this.SendPropertyChanged("Installation_Meter_23_Final_Value");
                    this.OnInstallation_Meter_23_Final_ValueChanged();
                }
            }
        }

        [Column(Storage = "_Installation_Meter_24_Initial_Value", DbType = "Int")]
        public System.Nullable<int> Installation_Meter_24_Initial_Value
        {
            get
            {
                return this._Installation_Meter_24_Initial_Value;
            }
            set
            {
                if ((this._Installation_Meter_24_Initial_Value != value))
                {
                    this.OnInstallation_Meter_24_Initial_ValueChanging(value);
                    this.SendPropertyChanging();
                    this._Installation_Meter_24_Initial_Value = value;
                    this.SendPropertyChanged("Installation_Meter_24_Initial_Value");
                    this.OnInstallation_Meter_24_Initial_ValueChanged();
                }
            }
        }

        [Column(Storage = "_Installation_Meter_24_Final_Value", DbType = "Int")]
        public System.Nullable<int> Installation_Meter_24_Final_Value
        {
            get
            {
                return this._Installation_Meter_24_Final_Value;
            }
            set
            {
                if ((this._Installation_Meter_24_Final_Value != value))
                {
                    this.OnInstallation_Meter_24_Final_ValueChanging(value);
                    this.SendPropertyChanging();
                    this._Installation_Meter_24_Final_Value = value;
                    this.SendPropertyChanged("Installation_Meter_24_Final_Value");
                    this.OnInstallation_Meter_24_Final_ValueChanged();
                }
            }
        }

        [Column(Storage = "_Installation_Meter_25_Initial_Value", DbType = "Int")]
        public System.Nullable<int> Installation_Meter_25_Initial_Value
        {
            get
            {
                return this._Installation_Meter_25_Initial_Value;
            }
            set
            {
                if ((this._Installation_Meter_25_Initial_Value != value))
                {
                    this.OnInstallation_Meter_25_Initial_ValueChanging(value);
                    this.SendPropertyChanging();
                    this._Installation_Meter_25_Initial_Value = value;
                    this.SendPropertyChanged("Installation_Meter_25_Initial_Value");
                    this.OnInstallation_Meter_25_Initial_ValueChanged();
                }
            }
        }

        [Column(Storage = "_Installation_Meter_25_Final_Value", DbType = "Int")]
        public System.Nullable<int> Installation_Meter_25_Final_Value
        {
            get
            {
                return this._Installation_Meter_25_Final_Value;
            }
            set
            {
                if ((this._Installation_Meter_25_Final_Value != value))
                {
                    this.OnInstallation_Meter_25_Final_ValueChanging(value);
                    this.SendPropertyChanging();
                    this._Installation_Meter_25_Final_Value = value;
                    this.SendPropertyChanged("Installation_Meter_25_Final_Value");
                    this.OnInstallation_Meter_25_Final_ValueChanged();
                }
            }
        }

        [Column(Storage = "_Installation_Meter_26_Initial_Value", DbType = "Int")]
        public System.Nullable<int> Installation_Meter_26_Initial_Value
        {
            get
            {
                return this._Installation_Meter_26_Initial_Value;
            }
            set
            {
                if ((this._Installation_Meter_26_Initial_Value != value))
                {
                    this.OnInstallation_Meter_26_Initial_ValueChanging(value);
                    this.SendPropertyChanging();
                    this._Installation_Meter_26_Initial_Value = value;
                    this.SendPropertyChanged("Installation_Meter_26_Initial_Value");
                    this.OnInstallation_Meter_26_Initial_ValueChanged();
                }
            }
        }

        [Column(Storage = "_Installation_Meter_26_Final_Value", DbType = "Int")]
        public System.Nullable<int> Installation_Meter_26_Final_Value
        {
            get
            {
                return this._Installation_Meter_26_Final_Value;
            }
            set
            {
                if ((this._Installation_Meter_26_Final_Value != value))
                {
                    this.OnInstallation_Meter_26_Final_ValueChanging(value);
                    this.SendPropertyChanging();
                    this._Installation_Meter_26_Final_Value = value;
                    this.SendPropertyChanged("Installation_Meter_26_Final_Value");
                    this.OnInstallation_Meter_26_Final_ValueChanged();
                }
            }
        }

        [Column(Storage = "_Installation_Meter_27_Initial_Value", DbType = "Int")]
        public System.Nullable<int> Installation_Meter_27_Initial_Value
        {
            get
            {
                return this._Installation_Meter_27_Initial_Value;
            }
            set
            {
                if ((this._Installation_Meter_27_Initial_Value != value))
                {
                    this.OnInstallation_Meter_27_Initial_ValueChanging(value);
                    this.SendPropertyChanging();
                    this._Installation_Meter_27_Initial_Value = value;
                    this.SendPropertyChanged("Installation_Meter_27_Initial_Value");
                    this.OnInstallation_Meter_27_Initial_ValueChanged();
                }
            }
        }

        [Column(Storage = "_Installation_Meter_27_Final_Value", DbType = "Int")]
        public System.Nullable<int> Installation_Meter_27_Final_Value
        {
            get
            {
                return this._Installation_Meter_27_Final_Value;
            }
            set
            {
                if ((this._Installation_Meter_27_Final_Value != value))
                {
                    this.OnInstallation_Meter_27_Final_ValueChanging(value);
                    this.SendPropertyChanging();
                    this._Installation_Meter_27_Final_Value = value;
                    this.SendPropertyChanged("Installation_Meter_27_Final_Value");
                    this.OnInstallation_Meter_27_Final_ValueChanged();
                }
            }
        }

        [Column(Storage = "_Installation_Meter_28_Initial_Value", DbType = "Int")]
        public System.Nullable<int> Installation_Meter_28_Initial_Value
        {
            get
            {
                return this._Installation_Meter_28_Initial_Value;
            }
            set
            {
                if ((this._Installation_Meter_28_Initial_Value != value))
                {
                    this.OnInstallation_Meter_28_Initial_ValueChanging(value);
                    this.SendPropertyChanging();
                    this._Installation_Meter_28_Initial_Value = value;
                    this.SendPropertyChanged("Installation_Meter_28_Initial_Value");
                    this.OnInstallation_Meter_28_Initial_ValueChanged();
                }
            }
        }

        [Column(Storage = "_Installation_Meter_28_Final_Value", DbType = "Int")]
        public System.Nullable<int> Installation_Meter_28_Final_Value
        {
            get
            {
                return this._Installation_Meter_28_Final_Value;
            }
            set
            {
                if ((this._Installation_Meter_28_Final_Value != value))
                {
                    this.OnInstallation_Meter_28_Final_ValueChanging(value);
                    this.SendPropertyChanging();
                    this._Installation_Meter_28_Final_Value = value;
                    this.SendPropertyChanged("Installation_Meter_28_Final_Value");
                    this.OnInstallation_Meter_28_Final_ValueChanged();
                }
            }
        }

        [Column(Storage = "_Installation_Meter_29_Initial_Value", DbType = "Int")]
        public System.Nullable<int> Installation_Meter_29_Initial_Value
        {
            get
            {
                return this._Installation_Meter_29_Initial_Value;
            }
            set
            {
                if ((this._Installation_Meter_29_Initial_Value != value))
                {
                    this.OnInstallation_Meter_29_Initial_ValueChanging(value);
                    this.SendPropertyChanging();
                    this._Installation_Meter_29_Initial_Value = value;
                    this.SendPropertyChanged("Installation_Meter_29_Initial_Value");
                    this.OnInstallation_Meter_29_Initial_ValueChanged();
                }
            }
        }

        [Column(Storage = "_Installation_Meter_29_Final_Value", DbType = "Int")]
        public System.Nullable<int> Installation_Meter_29_Final_Value
        {
            get
            {
                return this._Installation_Meter_29_Final_Value;
            }
            set
            {
                if ((this._Installation_Meter_29_Final_Value != value))
                {
                    this.OnInstallation_Meter_29_Final_ValueChanging(value);
                    this.SendPropertyChanging();
                    this._Installation_Meter_29_Final_Value = value;
                    this.SendPropertyChanged("Installation_Meter_29_Final_Value");
                    this.OnInstallation_Meter_29_Final_ValueChanged();
                }
            }
        }

        [Column(Storage = "_Installation_Meter_30_Initial_Value", DbType = "Int")]
        public System.Nullable<int> Installation_Meter_30_Initial_Value
        {
            get
            {
                return this._Installation_Meter_30_Initial_Value;
            }
            set
            {
                if ((this._Installation_Meter_30_Initial_Value != value))
                {
                    this.OnInstallation_Meter_30_Initial_ValueChanging(value);
                    this.SendPropertyChanging();
                    this._Installation_Meter_30_Initial_Value = value;
                    this.SendPropertyChanged("Installation_Meter_30_Initial_Value");
                    this.OnInstallation_Meter_30_Initial_ValueChanged();
                }
            }
        }

        [Column(Storage = "_Installation_Meter_30_Final_Value", DbType = "Int")]
        public System.Nullable<int> Installation_Meter_30_Final_Value
        {
            get
            {
                return this._Installation_Meter_30_Final_Value;
            }
            set
            {
                if ((this._Installation_Meter_30_Final_Value != value))
                {
                    this.OnInstallation_Meter_30_Final_ValueChanging(value);
                    this.SendPropertyChanging();
                    this._Installation_Meter_30_Final_Value = value;
                    this.SendPropertyChanged("Installation_Meter_30_Final_Value");
                    this.OnInstallation_Meter_30_Final_ValueChanged();
                }
            }
        }

        [Column(Storage = "_Installation_Meter_31_Initial_Value", DbType = "Int")]
        public System.Nullable<int> Installation_Meter_31_Initial_Value
        {
            get
            {
                return this._Installation_Meter_31_Initial_Value;
            }
            set
            {
                if ((this._Installation_Meter_31_Initial_Value != value))
                {
                    this.OnInstallation_Meter_31_Initial_ValueChanging(value);
                    this.SendPropertyChanging();
                    this._Installation_Meter_31_Initial_Value = value;
                    this.SendPropertyChanged("Installation_Meter_31_Initial_Value");
                    this.OnInstallation_Meter_31_Initial_ValueChanged();
                }
            }
        }

        [Column(Storage = "_Installation_Meter_31_Final_Value", DbType = "Int")]
        public System.Nullable<int> Installation_Meter_31_Final_Value
        {
            get
            {
                return this._Installation_Meter_31_Final_Value;
            }
            set
            {
                if ((this._Installation_Meter_31_Final_Value != value))
                {
                    this.OnInstallation_Meter_31_Final_ValueChanging(value);
                    this.SendPropertyChanging();
                    this._Installation_Meter_31_Final_Value = value;
                    this.SendPropertyChanged("Installation_Meter_31_Final_Value");
                    this.OnInstallation_Meter_31_Final_ValueChanged();
                }
            }
        }

        [Column(Storage = "_Installation_Meter_32_Initial_Value", DbType = "Int")]
        public System.Nullable<int> Installation_Meter_32_Initial_Value
        {
            get
            {
                return this._Installation_Meter_32_Initial_Value;
            }
            set
            {
                if ((this._Installation_Meter_32_Initial_Value != value))
                {
                    this.OnInstallation_Meter_32_Initial_ValueChanging(value);
                    this.SendPropertyChanging();
                    this._Installation_Meter_32_Initial_Value = value;
                    this.SendPropertyChanged("Installation_Meter_32_Initial_Value");
                    this.OnInstallation_Meter_32_Initial_ValueChanged();
                }
            }
        }

        [Column(Storage = "_Installation_Meter_32_Final_Value", DbType = "Int")]
        public System.Nullable<int> Installation_Meter_32_Final_Value
        {
            get
            {
                return this._Installation_Meter_32_Final_Value;
            }
            set
            {
                if ((this._Installation_Meter_32_Final_Value != value))
                {
                    this.OnInstallation_Meter_32_Final_ValueChanging(value);
                    this.SendPropertyChanging();
                    this._Installation_Meter_32_Final_Value = value;
                    this.SendPropertyChanged("Installation_Meter_32_Final_Value");
                    this.OnInstallation_Meter_32_Final_ValueChanged();
                }
            }
        }

        [Column(Storage = "_Installation_Float_Status", DbType = "Int")]
        public System.Nullable<int> Installation_Float_Status
        {
            get
            {
                return this._Installation_Float_Status;
            }
            set
            {
                if ((this._Installation_Float_Status != value))
                {
                    this.OnInstallation_Float_StatusChanging(value);
                    this.SendPropertyChanging();
                    this._Installation_Float_Status = value;
                    this.SendPropertyChanged("Installation_Float_Status");
                    this.OnInstallation_Float_StatusChanged();
                }
            }
        }

        [Column(Storage = "_Installation_Initial_Meters_Coins_In", DbType = "Int")]
        public System.Nullable<int> Installation_Initial_Meters_Coins_In
        {
            get
            {
                return this._Installation_Initial_Meters_Coins_In;
            }
            set
            {
                if ((this._Installation_Initial_Meters_Coins_In != value))
                {
                    this.OnInstallation_Initial_Meters_Coins_InChanging(value);
                    this.SendPropertyChanging();
                    this._Installation_Initial_Meters_Coins_In = value;
                    this.SendPropertyChanged("Installation_Initial_Meters_Coins_In");
                    this.OnInstallation_Initial_Meters_Coins_InChanged();
                }
            }
        }

        [Column(Storage = "_Installation_Initial_Meters_Coins_Out", DbType = "Int")]
        public System.Nullable<int> Installation_Initial_Meters_Coins_Out
        {
            get
            {
                return this._Installation_Initial_Meters_Coins_Out;
            }
            set
            {
                if ((this._Installation_Initial_Meters_Coins_Out != value))
                {
                    this.OnInstallation_Initial_Meters_Coins_OutChanging(value);
                    this.SendPropertyChanging();
                    this._Installation_Initial_Meters_Coins_Out = value;
                    this.SendPropertyChanged("Installation_Initial_Meters_Coins_Out");
                    this.OnInstallation_Initial_Meters_Coins_OutChanged();
                }
            }
        }

        [Column(Storage = "_Installation_Initial_Meters_Coin_Drop", DbType = "Int")]
        public System.Nullable<int> Installation_Initial_Meters_Coin_Drop
        {
            get
            {
                return this._Installation_Initial_Meters_Coin_Drop;
            }
            set
            {
                if ((this._Installation_Initial_Meters_Coin_Drop != value))
                {
                    this.OnInstallation_Initial_Meters_Coin_DropChanging(value);
                    this.SendPropertyChanging();
                    this._Installation_Initial_Meters_Coin_Drop = value;
                    this.SendPropertyChanged("Installation_Initial_Meters_Coin_Drop");
                    this.OnInstallation_Initial_Meters_Coin_DropChanged();
                }
            }
        }

        [Column(Storage = "_Installation_Initial_Meters_External_Credit", DbType = "Int")]
        public System.Nullable<int> Installation_Initial_Meters_External_Credit
        {
            get
            {
                return this._Installation_Initial_Meters_External_Credit;
            }
            set
            {
                if ((this._Installation_Initial_Meters_External_Credit != value))
                {
                    this.OnInstallation_Initial_Meters_External_CreditChanging(value);
                    this.SendPropertyChanging();
                    this._Installation_Initial_Meters_External_Credit = value;
                    this.SendPropertyChanged("Installation_Initial_Meters_External_Credit");
                    this.OnInstallation_Initial_Meters_External_CreditChanged();
                }
            }
        }

        [Column(Storage = "_Installation_Initial_Meters_Games_Bet", DbType = "Int")]
        public System.Nullable<int> Installation_Initial_Meters_Games_Bet
        {
            get
            {
                return this._Installation_Initial_Meters_Games_Bet;
            }
            set
            {
                if ((this._Installation_Initial_Meters_Games_Bet != value))
                {
                    this.OnInstallation_Initial_Meters_Games_BetChanging(value);
                    this.SendPropertyChanging();
                    this._Installation_Initial_Meters_Games_Bet = value;
                    this.SendPropertyChanged("Installation_Initial_Meters_Games_Bet");
                    this.OnInstallation_Initial_Meters_Games_BetChanged();
                }
            }
        }

        [Column(Storage = "_Installation_Initial_Meters_Games_Won", DbType = "Int")]
        public System.Nullable<int> Installation_Initial_Meters_Games_Won
        {
            get
            {
                return this._Installation_Initial_Meters_Games_Won;
            }
            set
            {
                if ((this._Installation_Initial_Meters_Games_Won != value))
                {
                    this.OnInstallation_Initial_Meters_Games_WonChanging(value);
                    this.SendPropertyChanging();
                    this._Installation_Initial_Meters_Games_Won = value;
                    this.SendPropertyChanged("Installation_Initial_Meters_Games_Won");
                    this.OnInstallation_Initial_Meters_Games_WonChanged();
                }
            }
        }

        [Column(Storage = "_Installation_Initial_Meters_Notes", DbType = "Int")]
        public System.Nullable<int> Installation_Initial_Meters_Notes
        {
            get
            {
                return this._Installation_Initial_Meters_Notes;
            }
            set
            {
                if ((this._Installation_Initial_Meters_Notes != value))
                {
                    this.OnInstallation_Initial_Meters_NotesChanging(value);
                    this.SendPropertyChanging();
                    this._Installation_Initial_Meters_Notes = value;
                    this.SendPropertyChanged("Installation_Initial_Meters_Notes");
                    this.OnInstallation_Initial_Meters_NotesChanged();
                }
            }
        }

        [Column(Storage = "_Installation_Initial_Meters_Handpay", DbType = "Int")]
        public System.Nullable<int> Installation_Initial_Meters_Handpay
        {
            get
            {
                return this._Installation_Initial_Meters_Handpay;
            }
            set
            {
                if ((this._Installation_Initial_Meters_Handpay != value))
                {
                    this.OnInstallation_Initial_Meters_HandpayChanging(value);
                    this.SendPropertyChanging();
                    this._Installation_Initial_Meters_Handpay = value;
                    this.SendPropertyChanged("Installation_Initial_Meters_Handpay");
                    this.OnInstallation_Initial_Meters_HandpayChanged();
                }
            }
        }

        [Column(Storage = "_Anticipated_Removal_Date", DbType = "VarChar(30)")]
        public string Anticipated_Removal_Date
        {
            get
            {
                return this._Anticipated_Removal_Date;
            }
            set
            {
                if ((this._Anticipated_Removal_Date != value))
                {
                    this.OnAnticipated_Removal_DateChanging(value);
                    this.SendPropertyChanging();
                    this._Anticipated_Removal_Date = value;
                    this.SendPropertyChanged("Anticipated_Removal_Date");
                    this.OnAnticipated_Removal_DateChanged();
                }
            }
        }

        [Column(Storage = "_Rental_Step_Down_Date", DbType = "VarChar(30)")]
        public string Rental_Step_Down_Date
        {
            get
            {
                return this._Rental_Step_Down_Date;
            }
            set
            {
                if ((this._Rental_Step_Down_Date != value))
                {
                    this.OnRental_Step_Down_DateChanging(value);
                    this.SendPropertyChanging();
                    this._Rental_Step_Down_Date = value;
                    this.SendPropertyChanged("Rental_Step_Down_Date");
                    this.OnRental_Step_Down_DateChanged();
                }
            }
        }

        [Column(Storage = "_Rent1", DbType = "Money")]
        public System.Nullable<decimal> Rent1
        {
            get
            {
                return this._Rent1;
            }
            set
            {
                if ((this._Rent1 != value))
                {
                    this.OnRent1Changing(value);
                    this.SendPropertyChanging();
                    this._Rent1 = value;
                    this.SendPropertyChanged("Rent1");
                    this.OnRent1Changed();
                }
            }
        }

        [Column(Storage = "_Rent2", DbType = "Money")]
        public System.Nullable<decimal> Rent2
        {
            get
            {
                return this._Rent2;
            }
            set
            {
                if ((this._Rent2 != value))
                {
                    this.OnRent2Changing(value);
                    this.SendPropertyChanging();
                    this._Rent2 = value;
                    this.SendPropertyChanged("Rent2");
                    this.OnRent2Changed();
                }
            }
        }

        [Column(Storage = "_Licence", DbType = "Money")]
        public System.Nullable<decimal> Licence
        {
            get
            {
                return this._Licence;
            }
            set
            {
                if ((this._Licence != value))
                {
                    this.OnLicenceChanging(value);
                    this.SendPropertyChanging();
                    this._Licence = value;
                    this.SendPropertyChanged("Licence");
                    this.OnLicenceChanged();
                }
            }
        }

        [Column(Storage = "_Installation_Out_Order", DbType = "Bit")]
        public System.Nullable<bool> Installation_Out_Order
        {
            get
            {
                return this._Installation_Out_Order;
            }
            set
            {
                if ((this._Installation_Out_Order != value))
                {
                    this.OnInstallation_Out_OrderChanging(value);
                    this.SendPropertyChanging();
                    this._Installation_Out_Order = value;
                    this.SendPropertyChanged("Installation_Out_Order");
                    this.OnInstallation_Out_OrderChanged();
                }
            }
        }

        [Column(Storage = "_Installation_Counter_Cash_In_Units", DbType = "Int")]
        public System.Nullable<int> Installation_Counter_Cash_In_Units
        {
            get
            {
                return this._Installation_Counter_Cash_In_Units;
            }
            set
            {
                if ((this._Installation_Counter_Cash_In_Units != value))
                {
                    this.OnInstallation_Counter_Cash_In_UnitsChanging(value);
                    this.SendPropertyChanging();
                    this._Installation_Counter_Cash_In_Units = value;
                    this.SendPropertyChanged("Installation_Counter_Cash_In_Units");
                    this.OnInstallation_Counter_Cash_In_UnitsChanged();
                }
            }
        }

        [Column(Storage = "_Installation_Counter_Cash_Out_Units", DbType = "Int")]
        public System.Nullable<int> Installation_Counter_Cash_Out_Units
        {
            get
            {
                return this._Installation_Counter_Cash_Out_Units;
            }
            set
            {
                if ((this._Installation_Counter_Cash_Out_Units != value))
                {
                    this.OnInstallation_Counter_Cash_Out_UnitsChanging(value);
                    this.SendPropertyChanging();
                    this._Installation_Counter_Cash_Out_Units = value;
                    this.SendPropertyChanged("Installation_Counter_Cash_Out_Units");
                    this.OnInstallation_Counter_Cash_Out_UnitsChanged();
                }
            }
        }

        [Column(Storage = "_Installation_Counter_Token_In_Units", DbType = "Int")]
        public System.Nullable<int> Installation_Counter_Token_In_Units
        {
            get
            {
                return this._Installation_Counter_Token_In_Units;
            }
            set
            {
                if ((this._Installation_Counter_Token_In_Units != value))
                {
                    this.OnInstallation_Counter_Token_In_UnitsChanging(value);
                    this.SendPropertyChanging();
                    this._Installation_Counter_Token_In_Units = value;
                    this.SendPropertyChanged("Installation_Counter_Token_In_Units");
                    this.OnInstallation_Counter_Token_In_UnitsChanged();
                }
            }
        }

        [Column(Storage = "_Installation_Counter_Token_Out_Units", DbType = "Int")]
        public System.Nullable<int> Installation_Counter_Token_Out_Units
        {
            get
            {
                return this._Installation_Counter_Token_Out_Units;
            }
            set
            {
                if ((this._Installation_Counter_Token_Out_Units != value))
                {
                    this.OnInstallation_Counter_Token_Out_UnitsChanging(value);
                    this.SendPropertyChanging();
                    this._Installation_Counter_Token_Out_Units = value;
                    this.SendPropertyChanged("Installation_Counter_Token_Out_Units");
                    this.OnInstallation_Counter_Token_Out_UnitsChanged();
                }
            }
        }

        [Column(Storage = "_Installation_Counter_Refill_Units", DbType = "Int")]
        public System.Nullable<int> Installation_Counter_Refill_Units
        {
            get
            {
                return this._Installation_Counter_Refill_Units;
            }
            set
            {
                if ((this._Installation_Counter_Refill_Units != value))
                {
                    this.OnInstallation_Counter_Refill_UnitsChanging(value);
                    this.SendPropertyChanging();
                    this._Installation_Counter_Refill_Units = value;
                    this.SendPropertyChanged("Installation_Counter_Refill_Units");
                    this.OnInstallation_Counter_Refill_UnitsChanged();
                }
            }
        }

        [Column(Storage = "_Installation_Counter_Jackpot_Units", DbType = "Int")]
        public System.Nullable<int> Installation_Counter_Jackpot_Units
        {
            get
            {
                return this._Installation_Counter_Jackpot_Units;
            }
            set
            {
                if ((this._Installation_Counter_Jackpot_Units != value))
                {
                    this.OnInstallation_Counter_Jackpot_UnitsChanging(value);
                    this.SendPropertyChanging();
                    this._Installation_Counter_Jackpot_Units = value;
                    this.SendPropertyChanged("Installation_Counter_Jackpot_Units");
                    this.OnInstallation_Counter_Jackpot_UnitsChanged();
                }
            }
        }

        [Column(Storage = "_Installation_Counter_Prize_Units", DbType = "Int")]
        public System.Nullable<int> Installation_Counter_Prize_Units
        {
            get
            {
                return this._Installation_Counter_Prize_Units;
            }
            set
            {
                if ((this._Installation_Counter_Prize_Units != value))
                {
                    this.OnInstallation_Counter_Prize_UnitsChanging(value);
                    this.SendPropertyChanging();
                    this._Installation_Counter_Prize_Units = value;
                    this.SendPropertyChanged("Installation_Counter_Prize_Units");
                    this.OnInstallation_Counter_Prize_UnitsChanged();
                }
            }
        }

        [Column(Storage = "_Installation_Counter_Tournament_Units", DbType = "Int")]
        public System.Nullable<int> Installation_Counter_Tournament_Units
        {
            get
            {
                return this._Installation_Counter_Tournament_Units;
            }
            set
            {
                if ((this._Installation_Counter_Tournament_Units != value))
                {
                    this.OnInstallation_Counter_Tournament_UnitsChanging(value);
                    this.SendPropertyChanging();
                    this._Installation_Counter_Tournament_Units = value;
                    this.SendPropertyChanged("Installation_Counter_Tournament_Units");
                    this.OnInstallation_Counter_Tournament_UnitsChanged();
                }
            }
        }

        [Column(Storage = "_Installation_Counter_Jukebox_Play_Units", DbType = "Int")]
        public System.Nullable<int> Installation_Counter_Jukebox_Play_Units
        {
            get
            {
                return this._Installation_Counter_Jukebox_Play_Units;
            }
            set
            {
                if ((this._Installation_Counter_Jukebox_Play_Units != value))
                {
                    this.OnInstallation_Counter_Jukebox_Play_UnitsChanging(value);
                    this.SendPropertyChanging();
                    this._Installation_Counter_Jukebox_Play_Units = value;
                    this.SendPropertyChanged("Installation_Counter_Jukebox_Play_Units");
                    this.OnInstallation_Counter_Jukebox_Play_UnitsChanged();
                }
            }
        }

        [Column(Storage = "_Installation_Counter_Jukebox_Units", DbType = "Int")]
        public System.Nullable<int> Installation_Counter_Jukebox_Units
        {
            get
            {
                return this._Installation_Counter_Jukebox_Units;
            }
            set
            {
                if ((this._Installation_Counter_Jukebox_Units != value))
                {
                    this.OnInstallation_Counter_Jukebox_UnitsChanging(value);
                    this.SendPropertyChanging();
                    this._Installation_Counter_Jukebox_Units = value;
                    this.SendPropertyChanged("Installation_Counter_Jukebox_Units");
                    this.OnInstallation_Counter_Jukebox_UnitsChanged();
                }
            }
        }

        [Column(Storage = "_Planned_Movement_ID", DbType = "Int")]
        public System.Nullable<int> Planned_Movement_ID
        {
            get
            {
                return this._Planned_Movement_ID;
            }
            set
            {
                if ((this._Planned_Movement_ID != value))
                {
                    this.OnPlanned_Movement_IDChanging(value);
                    this.SendPropertyChanging();
                    this._Planned_Movement_ID = value;
                    this.SendPropertyChanged("Planned_Movement_ID");
                    this.OnPlanned_Movement_IDChanged();
                }
            }
        }

        [Column(Storage = "_Installation_RDC_Machine_Code", DbType = "VarChar(10)")]
        public string Installation_RDC_Machine_Code
        {
            get
            {
                return this._Installation_RDC_Machine_Code;
            }
            set
            {
                if ((this._Installation_RDC_Machine_Code != value))
                {
                    this.OnInstallation_RDC_Machine_CodeChanging(value);
                    this.SendPropertyChanging();
                    this._Installation_RDC_Machine_Code = value;
                    this.SendPropertyChanged("Installation_RDC_Machine_Code");
                    this.OnInstallation_RDC_Machine_CodeChanged();
                }
            }
        }

        [Column(Storage = "_Installation_RDC_Secondary_Machine_Code", DbType = "VarChar(20)")]
        public string Installation_RDC_Secondary_Machine_Code
        {
            get
            {
                return this._Installation_RDC_Secondary_Machine_Code;
            }
            set
            {
                if ((this._Installation_RDC_Secondary_Machine_Code != value))
                {
                    this.OnInstallation_RDC_Secondary_Machine_CodeChanging(value);
                    this.SendPropertyChanging();
                    this._Installation_RDC_Secondary_Machine_Code = value;
                    this.SendPropertyChanged("Installation_RDC_Secondary_Machine_Code");
                    this.OnInstallation_RDC_Secondary_Machine_CodeChanged();
                }
            }
        }

        [Column(Storage = "_Installation_Token_Value", DbType = "Int NOT NULL")]
        public int Installation_Token_Value
        {
            get
            {
                return this._Installation_Token_Value;
            }
            set
            {
                if ((this._Installation_Token_Value != value))
                {
                    this.OnInstallation_Token_ValueChanging(value);
                    this.SendPropertyChanging();
                    this._Installation_Token_Value = value;
                    this.SendPropertyChanged("Installation_Token_Value");
                    this.OnInstallation_Token_ValueChanged();
                }
            }
        }

        [Column(Storage = "_Installation_Games_Count", DbType = "Int")]
        public System.Nullable<int> Installation_Games_Count
        {
            get
            {
                return this._Installation_Games_Count;
            }
            set
            {
                if ((this._Installation_Games_Count != value))
                {
                    this.OnInstallation_Games_CountChanging(value);
                    this.SendPropertyChanging();
                    this._Installation_Games_Count = value;
                    this.SendPropertyChanged("Installation_Games_Count");
                    this.OnInstallation_Games_CountChanged();
                }
            }
        }

        [Column(Storage = "_Installation_Status", DbType = "VarChar(50)")]
        public string Installation_Status
        {
            get
            {
                return this._Installation_Status;
            }
            set
            {
                if ((this._Installation_Status != value))
                {
                    this.OnInstallation_StatusChanging(value);
                    this.SendPropertyChanging();
                    this._Installation_Status = value;
                    this.SendPropertyChanged("Installation_Status");
                    this.OnInstallation_StatusChanged();
                }
            }
        }

        [Column(Storage = "_Game_Part_Number", DbType = "VarChar(20)")]
        public string Game_Part_Number
        {
            get
            {
                return this._Game_Part_Number;
            }
            set
            {
                if ((this._Game_Part_Number != value))
                {
                    this.OnGame_Part_NumberChanging(value);
                    this.SendPropertyChanging();
                    this._Game_Part_Number = value;
                    this.SendPropertyChanged("Game_Part_Number");
                    this.OnGame_Part_NumberChanged();
                }
            }
        }

        [Column(Storage = "_Installation_MaxBet", DbType = "Int")]
        public System.Nullable<int> Installation_MaxBet
        {
            get
            {
                return this._Installation_MaxBet;
            }
            set
            {
                if ((this._Installation_MaxBet != value))
                {
                    this.OnInstallation_MaxBetChanging(value);
                    this.SendPropertyChanging();
                    this._Installation_MaxBet = value;
                    this.SendPropertyChanged("Installation_MaxBet");
                    this.OnInstallation_MaxBetChanged();
                }
            }
        }

        [Column(Storage = "_IsAuxSerialPortEnabled", DbType = "Bit NOT NULL")]
        public bool IsAuxSerialPortEnabled
        {
            get
            {
                return this._IsAuxSerialPortEnabled;
            }
            set
            {
                if ((this._IsAuxSerialPortEnabled != value))
                {
                    this.OnIsAuxSerialPortEnabledChanging(value);
                    this.SendPropertyChanging();
                    this._IsAuxSerialPortEnabled = value;
                    this.SendPropertyChanged("IsAuxSerialPortEnabled");
                    this.OnIsAuxSerialPortEnabledChanged();
                }
            }
        }

        [Column(Storage = "_IsGatSerialPortEnabled", DbType = "Bit NOT NULL")]
        public bool IsGatSerialPortEnabled
        {
            get
            {
                return this._IsGatSerialPortEnabled;
            }
            set
            {
                if ((this._IsGatSerialPortEnabled != value))
                {
                    this.OnIsGatSerialPortEnabledChanging(value);
                    this.SendPropertyChanging();
                    this._IsGatSerialPortEnabled = value;
                    this.SendPropertyChanged("IsGatSerialPortEnabled");
                    this.OnIsGatSerialPortEnabledChanged();
                }
            }
        }

        [Column(Storage = "_IsSlotLinePortEnabled", DbType = "Bit NOT NULL")]
        public bool IsSlotLinePortEnabled
        {
            get
            {
                return this._IsSlotLinePortEnabled;
            }
            set
            {
                if ((this._IsSlotLinePortEnabled != value))
                {
                    this.OnIsSlotLinePortEnabledChanging(value);
                    this.SendPropertyChanging();
                    this._IsSlotLinePortEnabled = value;
                    this.SendPropertyChanged("IsSlotLinePortEnabled");
                    this.OnIsSlotLinePortEnabledChanged();
                }
            }
        }

        [Column(Storage = "_Port_Disabled_Status", DbType = "Bit NOT NULL")]
        public bool Port_Disabled_Status
        {
            get
            {
                return this._Port_Disabled_Status;
            }
            set
            {
                if ((this._Port_Disabled_Status != value))
                {
                    this.OnPort_Disabled_StatusChanging(value);
                    this.SendPropertyChanging();
                    this._Port_Disabled_Status = value;
                    this.SendPropertyChanged("Port_Disabled_Status");
                    this.OnPort_Disabled_StatusChanged();
                }
            }
        }

        [Column(Storage = "_Voucher_Expire_Status", DbType = "Char(1)")]
        public System.Nullable<char> Voucher_Expire_Status
        {
            get
            {
                return this._Voucher_Expire_Status;
            }
            set
            {
                if ((this._Voucher_Expire_Status != value))
                {
                    this.OnVoucher_Expire_StatusChanging(value);
                    this.SendPropertyChanging();
                    this._Voucher_Expire_Status = value;
                    this.SendPropertyChanged("Voucher_Expire_Status");
                    this.OnVoucher_Expire_StatusChanged();
                }
            }
        }

        [Column(Storage = "_FinalCollection_Status", DbType = "TinyInt NOT NULL")]
        public byte FinalCollection_Status
        {
            get
            {
                return this._FinalCollection_Status;
            }
            set
            {
                if ((this._FinalCollection_Status != value))
                {
                    this.OnFinalCollection_StatusChanging(value);
                    this.SendPropertyChanging();
                    this._FinalCollection_Status = value;
                    this.SendPropertyChanged("FinalCollection_Status");
                    this.OnFinalCollection_StatusChanged();
                }
            }
        }

        public event PropertyChangingEventHandler PropertyChanging;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void SendPropertyChanging()
        {
            if ((this.PropertyChanging != null))
            {
                this.PropertyChanging(this, emptyChangingEventArgs);
            }
        }

        protected virtual void SendPropertyChanged(String propertyName)
        {
            if ((this.PropertyChanged != null))
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }


    [Table(Name = "dbo.Collection")]
    public partial class Collection : INotifyPropertyChanging, INotifyPropertyChanged
    {

        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);

        private int _Collection_No;

        private System.Nullable<int> _Collection_Batch_No;

        private string _Machine_Serial;

        private string _Collection_RDC_Machine_Code;

        private string _Collection_RDC_Secondary_Machine_Code;

        private System.Nullable<int> _Datapak_Read_Occurrence;

        private System.Nullable<int> _Float_Level;

        private System.Nullable<int> _Period_ID;

        private System.Nullable<int> _Week_ID;

        private System.Nullable<int> _CASH_IN_1P;
        
        private System.Nullable<int> _CASH_IN_2P;

        private System.Nullable<int> _CASH_IN_5P;

        private System.Nullable<int> _CASH_IN_10P;

        private System.Nullable<int> _CASH_IN_20P;

        private System.Nullable<int> _CASH_IN_50P;

        private System.Nullable<int> _CASH_IN_100P;

        private System.Nullable<int> _CASH_IN_200P;

        private System.Nullable<int> _CASH_IN_500P;

        private System.Nullable<int> _CASH_IN_1000P;

        private System.Nullable<int> _CASH_IN_2000P;

        private System.Nullable<int> _CASH_IN_5000P;

        private System.Nullable<int> _CASH_IN_10000P;

        private System.Nullable<int> _CASH_IN_20000P;

        private System.Nullable<int> _CASH_IN_50000P;

        private System.Nullable<int> _CASH_IN_100000P;

        private System.Nullable<int> _TOKEN_IN_5P;

        private System.Nullable<int> _TOKEN_IN_10P;

        private System.Nullable<int> _TOKEN_IN_20P;

        private System.Nullable<int> _TOKEN_IN_50P;

        private System.Nullable<int> _TOKEN_IN_100P;

        private System.Nullable<int> _TOKEN_IN_200P;

        private System.Nullable<int> _TOKEN_IN_500P;

        private System.Nullable<int> _TOKEN_IN_1000P;

        private System.Nullable<int> _CASH_OUT_1P;

        private System.Nullable<int> _CASH_OUT_2P;

        private System.Nullable<int> _CASH_OUT_5P;

        private System.Nullable<int> _CASH_OUT_10P;

        private System.Nullable<int> _CASH_OUT_20P;

        private System.Nullable<int> _CASH_OUT_50P;

        private System.Nullable<int> _CASH_OUT_100P;

        private System.Nullable<int> _CASH_OUT_200P;

        private System.Nullable<int> _CASH_OUT_500P;

        private System.Nullable<int> _CASH_OUT_1000P;

        private System.Nullable<int> _CASH_OUT_2000P;

        private System.Nullable<int> _CASH_OUT_5000P;

        private System.Nullable<int> _CASH_OUT_10000P;

        private System.Nullable<int> _CASH_OUT_20000P;

        private System.Nullable<int> _CASH_OUT_50000P;

        private System.Nullable<int> _CASH_OUT_100000P;

        private System.Nullable<int> _TOKEN_OUT_5P;

        private System.Nullable<int> _TOKEN_OUT_10P;

        private System.Nullable<int> _TOKEN_OUT_20P;

        private System.Nullable<int> _TOKEN_OUT_50P;

        private System.Nullable<int> _TOKEN_OUT_100P;

        private System.Nullable<int> _TOKEN_OUT_200P;

        private System.Nullable<int> _TOKEN_OUT_500P;

        private System.Nullable<int> _TOKEN_OUT_1000P;

        private System.Nullable<int> _CASH_REFILL_5P;

        private System.Nullable<int> _CASH_REFILL_10P;

        private System.Nullable<int> _CASH_REFILL_20P;

        private System.Nullable<int> _CASH_REFILL_50P;

        private System.Nullable<int> _CASH_REFILL_100P;

        private System.Nullable<int> _CASH_REFILL_200P;

        private System.Nullable<int> _CASH_REFILL_500P;

        private System.Nullable<int> _CASH_REFILL_1000P;

        private System.Nullable<int> _CASH_REFILL_2000P;

        private System.Nullable<int> _CASH_REFILL_5000P;

        private System.Nullable<int> _CASH_REFILL_10000P;

        private System.Nullable<int> _CASH_REFILL_20000P;

        private System.Nullable<int> _CASH_REFILL_50000P;

        private System.Nullable<int> _CASH_REFILL_100000P;

        private System.Nullable<int> _TOKEN_REFILL_5P;

        private System.Nullable<int> _TOKEN_REFILL_10P;

        private System.Nullable<int> _TOKEN_REFILL_20P;

        private System.Nullable<int> _TOKEN_REFILL_50P;

        private System.Nullable<int> _TOKEN_REFILL_100P;

        private System.Nullable<int> _TOKEN_REFILL_200P;

        private System.Nullable<int> _TOKEN_REFILL_500P;

        private System.Nullable<int> _TOKEN_REFILL_1000P;

        private System.Nullable<bool> _Declaration;

        private System.Nullable<float> _Treasury_Total;

        private System.Nullable<int> _CounterCashIn;

        private System.Nullable<int> _CounterCashOut;

        private System.Nullable<int> _CounterTokensIn;

        private System.Nullable<int> _CounterTokensOut;

        private System.Nullable<int> _CounterPrize;

        private System.Nullable<int> _CounterTournament;

        private System.Nullable<int> _CounterJukebox;

        private System.Nullable<int> _CounterRefills;

        private System.Nullable<float> _CashCollected;

        private System.Nullable<float> _TokensCollected;

        private System.Nullable<float> _Cash_Collected_1p;

        private System.Nullable<float> _Cash_Collected_2p;

        private System.Nullable<float> _Cash_Collected_5p;

        private System.Nullable<float> _Cash_Collected_10p;

        private System.Nullable<float> _Cash_Collected_20p;

        private System.Nullable<float> _Cash_Collected_50p;

        private System.Nullable<float> _Cash_Collected_100p;

        private System.Nullable<float> _Cash_Collected_200p;

        private System.Nullable<float> _Cash_Collected_500p;

        private System.Nullable<float> _Cash_Collected_1000p;

        private System.Nullable<float> _Cash_Collected_2000p;

        private System.Nullable<float> _Cash_Collected_5000p;

        private System.Nullable<float> _Cash_Collected_10000p;

        private System.Nullable<float> _Cash_Collected_20000p;

        private System.Nullable<float> _Cash_Collected_50000p;

        private System.Nullable<float> _Cash_Collected_100000p;

        private System.Nullable<float> _CashRefills;

        private System.Nullable<float> _TokenRefills;

        private System.Nullable<float> _Cash_Refills_2p;

        private System.Nullable<float> _Cash_Refills_5p;

        private System.Nullable<float> _Cash_Refills_10p;

        private System.Nullable<float> _Cash_Refills_20p;

        private System.Nullable<float> _Cash_Refills_50p;

        private System.Nullable<float> _Cash_Refills_100p;

        private System.Nullable<float> _Cash_Refills_200p;

        private System.Nullable<float> _Cash_Refills_500p;

        private System.Nullable<float> _Cash_Refills_1000p;

        private System.Nullable<float> _Cash_Refills_2000p;

        private System.Nullable<float> _Cash_Refills_5000p;

        private System.Nullable<float> _Cash_Refills_10000p;

        private System.Nullable<float> _Cash_Refills_20000p;

        private System.Nullable<float> _Cash_Refills_50000p;

        private System.Nullable<float> _Cash_Refills_100000p;

        private System.Nullable<int> _CounterCashInElectronic;

        private System.Nullable<int> _CounterCashOutElectronic;

        private System.Nullable<int> _CounterTokensInElectronic;

        private System.Nullable<int> _CounterTokensOutElectronic;

        private System.Nullable<int> _JackpotsOut;

        private System.Nullable<int> _PreviousCounterCashIn;

        private System.Nullable<int> _PreviousCounterCashOut;

        private System.Nullable<int> _PreviousCounterTokensIn;

        private System.Nullable<int> _PreviousCounterTokensOut;

        private System.Nullable<int> _PreviousCounterPrize;

        private System.Nullable<int> _PreviousCounterJackpotsOut;

        private System.Nullable<int> _PreviousCounterTournament;

        private System.Nullable<int> _PreviousCounterJukebox;

        private System.Nullable<int> _PreviousCounterRefills;

        private System.Nullable<int> _PreviousCounterCashInElectronic;

        private System.Nullable<int> _PreviousCounterCashOutElectronic;

        private System.Nullable<int> _PreviousCounterTokensInElectronic;

        private System.Nullable<int> _PreviousCounterTokensOutElectronic;

        private System.Nullable<System.DateTime> _PreviousCollectionDate;

        private System.Nullable<int> _PreviousCollectionNo;

        private System.Nullable<float> _Treasury_Refills;

        private System.Nullable<float> _Treasury_Repayments;

        private System.Nullable<float> _Treasury_Tokens;

        private System.Nullable<float> _ExpectedBaggedCash;

        private System.Nullable<float> _ActualBaggedCash;

        private System.Nullable<int> _Collection_Meters_Coins_In;

        private System.Nullable<int> _Collection_Meters_Coins_Out;

        private System.Nullable<int> _Collection_Meters_Coin_Drop;

        private System.Nullable<int> _Collection_Meters_Handpay;

        private System.Nullable<int> _Collection_Meters_External_Credit;

        private System.Nullable<int> _Collection_Meters_Games_Bet;

        private System.Nullable<int> _Collection_Meters_Games_Won;

        private System.Nullable<int> _Collection_Meters_Notes;

        private System.Nullable<float> _Collection_Treasury_Defloat;

        private System.Nullable<bool> _Collection_Defloat_Collection;

        private System.Nullable<int> _Previous_Meters_Coins_In;

        private System.Nullable<int> _Previous_Meters_Coins_Out;

        private System.Nullable<int> _Previous_Meters_Coin_Drop;

        private System.Nullable<int> _Previous_Meters_Handpay;

        private System.Nullable<int> _Previous_Meters_External_Credit;

        private System.Nullable<int> _Previous_Meters_Games_Bet;

        private System.Nullable<int> _Previous_Meters_Games_Won;

        private System.Nullable<int> _Previous_Meters_Notes;

        private System.Nullable<float> _Treasury_Handpay;

        private System.Nullable<int> _Operator_Week_ID;

        private System.Nullable<int> _Operator_Period_ID;

        private System.Nullable<int> _CollectionHandHeldMetersReceived;

        private System.Nullable<int> _CollectionNoDoorEvents;

        private System.Nullable<int> _CollectionNoPowerEvents;

        private System.Nullable<int> _CollectionNoFaultEvents;

        private System.Nullable<int> _CollectionTotalDurationPower;

        private System.Nullable<int> _CollectionTotalDurationDoor;

        private System.Nullable<int> _COLLECTION_RDC_VTP;

        private System.Nullable<float> _Collection_NetEx;

        private System.Nullable<float> _Collection_VAT_Rate;

        private System.Nullable<int> _COLLECTION_RDC_COINS_IN;

        private System.Nullable<int> _COLLECTION_RDC_COINS_OUT;

        private System.Nullable<int> _COLLECTION_RDC_COIN_DROP;

        private System.Nullable<int> _COLLECTION_RDC_HANDPAY;

        private System.Nullable<int> _COLLECTION_RDC_EXTERNAL_CREDIT;

        private System.Nullable<int> _COLLECTION_RDC_GAMES_BET;

        private System.Nullable<int> _COLLECTION_RDC_GAMES_WON;

        private System.Nullable<int> _COLLECTION_RDC_NOTES;

        private System.Nullable<int> _COLLECTION_RDC_CANCELLED_CREDITS;

        private System.Nullable<int> _COLLECTION_RDC_GAMES_LOST;

        private System.Nullable<int> _COLLECTION_RDC_GAMES_SINCE_POWER_UP;

        private System.Nullable<int> _COLLECTION_RDC_TRUE_COIN_IN;

        private System.Nullable<int> _COLLECTION_RDC_TRUE_COIN_OUT;

        private System.Nullable<int> _COLLECTION_RDC_CURRENT_CREDITS;

        private System.Nullable<int> _Collection_PoP_Actual;

        private System.Nullable<int> _Collection_PoP_Configured;

        private System.Nullable<int> _Collection_EDC_Status;

        private System.Nullable<int> _Collection_Meter_Status;

        private System.Nullable<int> _Collection_Cash_Status;

        private int _Installation_No;

        private System.Nullable<System.DateTime> _Collection_Date;

        private int _CASH_FLOAT_CHANGE_1p;

        private int _CASH_FLOAT_CHANGE_2p;

        private int _CASH_FLOAT_CHANGE_5p;

        private int _CASH_FLOAT_CHANGE_10p;

        private int _CASH_FLOAT_CHANGE_20p;

        private int _CASH_FLOAT_CHANGE_50p;

        private int _CASH_FLOAT_CHANGE_100p;

        private int _CASH_FLOAT_CHANGE_200p;

        private int _CASH_FLOAT_CHANGE_500p;

        private int _CASH_FLOAT_CHANGE_1000p;

        private float _CASH_FLOAT_TOTAL;

        private System.Nullable<int> _DeclaredTicketQty;

        private System.Nullable<float> _DeclaredTicketValue;

        private System.Nullable<int> _COLLECTION_RDC_JACKPOT;

        private int _COLLECTION_RDC_TICKETS_INSERTED_VALUE;

        private int _COLLECTION_RDC_TICKETS_PRINTED_VALUE;

        private System.Nullable<int> _DeclaredTicketPrintedQty;

        private System.Nullable<float> _DeclaredTicketPrintedValue;

        private System.Nullable<System.DateTime> _Collection_Date_Performed;

        private System.Nullable<int> _progressive_win_value;

        private System.Nullable<int> _progressive_win_Handpay_value;

        private System.Nullable<double> _Progressive_Value_Declared;

        #region Extensibility Method Definitions
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
        partial void OnCollection_NoChanging(int value);
        partial void OnCollection_NoChanged();
        partial void OnCollection_Batch_NoChanging(System.Nullable<int> value);
        partial void OnCollection_Batch_NoChanged();
        partial void OnMachine_SerialChanging(string value);
        partial void OnMachine_SerialChanged();
        partial void OnCollection_RDC_Machine_CodeChanging(string value);
        partial void OnCollection_RDC_Machine_CodeChanged();
        partial void OnCollection_RDC_Secondary_Machine_CodeChanging(string value);
        partial void OnCollection_RDC_Secondary_Machine_CodeChanged();
        partial void OnDatapak_Read_OccurrenceChanging(System.Nullable<int> value);
        partial void OnDatapak_Read_OccurrenceChanged();
        partial void OnFloat_LevelChanging(System.Nullable<int> value);
        partial void OnFloat_LevelChanged();
        partial void OnPeriod_IDChanging(System.Nullable<int> value);
        partial void OnPeriod_IDChanged();
        partial void OnWeek_IDChanging(System.Nullable<int> value);
        partial void OnWeek_IDChanged();
        partial void OnCASH_IN_1PChanging(System.Nullable<int> value);
        partial void OnCASH_IN_1PChanged();
        partial void OnCASH_IN_2PChanging(System.Nullable<int> value);
        partial void OnCASH_IN_2PChanged();
        partial void OnCASH_IN_5PChanging(System.Nullable<int> value);
        partial void OnCASH_IN_5PChanged();
        partial void OnCASH_IN_10PChanging(System.Nullable<int> value);
        partial void OnCASH_IN_10PChanged();
        partial void OnCASH_IN_20PChanging(System.Nullable<int> value);
        partial void OnCASH_IN_20PChanged();
        partial void OnCASH_IN_50PChanging(System.Nullable<int> value);
        partial void OnCASH_IN_50PChanged();
        partial void OnCASH_IN_100PChanging(System.Nullable<int> value);
        partial void OnCASH_IN_100PChanged();
        partial void OnCASH_IN_200PChanging(System.Nullable<int> value);
        partial void OnCASH_IN_200PChanged();
        partial void OnCASH_IN_500PChanging(System.Nullable<int> value);
        partial void OnCASH_IN_500PChanged();
        partial void OnCASH_IN_1000PChanging(System.Nullable<int> value);
        partial void OnCASH_IN_1000PChanged();
        partial void OnCASH_IN_2000PChanging(System.Nullable<int> value);
        partial void OnCASH_IN_2000PChanged();
        partial void OnCASH_IN_5000PChanging(System.Nullable<int> value);
        partial void OnCASH_IN_5000PChanged();
        partial void OnCASH_IN_10000PChanging(System.Nullable<int> value);
        partial void OnCASH_IN_10000PChanged();
        partial void OnCASH_IN_20000PChanging(System.Nullable<int> value);
        partial void OnCASH_IN_20000PChanged();
        partial void OnCASH_IN_50000PChanging(System.Nullable<int> value);
        partial void OnCASH_IN_50000PChanged();
        partial void OnCASH_IN_100000PChanging(System.Nullable<int> value);
        partial void OnCASH_IN_100000PChanged();
        partial void OnTOKEN_IN_5PChanging(System.Nullable<int> value);
        partial void OnTOKEN_IN_5PChanged();
        partial void OnTOKEN_IN_10PChanging(System.Nullable<int> value);
        partial void OnTOKEN_IN_10PChanged();
        partial void OnTOKEN_IN_20PChanging(System.Nullable<int> value);
        partial void OnTOKEN_IN_20PChanged();
        partial void OnTOKEN_IN_50PChanging(System.Nullable<int> value);
        partial void OnTOKEN_IN_50PChanged();
        partial void OnTOKEN_IN_100PChanging(System.Nullable<int> value);
        partial void OnTOKEN_IN_100PChanged();
        partial void OnTOKEN_IN_200PChanging(System.Nullable<int> value);
        partial void OnTOKEN_IN_200PChanged();
        partial void OnTOKEN_IN_500PChanging(System.Nullable<int> value);
        partial void OnTOKEN_IN_500PChanged();
        partial void OnTOKEN_IN_1000PChanging(System.Nullable<int> value);
        partial void OnTOKEN_IN_1000PChanged();
        partial void OnCASH_OUT_1PChanging(System.Nullable<int> value);
        partial void OnCASH_OUT_1PChanged();
        partial void OnCASH_OUT_2PChanging(System.Nullable<int> value);
        partial void OnCASH_OUT_2PChanged();
        partial void OnCASH_OUT_5PChanging(System.Nullable<int> value);
        partial void OnCASH_OUT_5PChanged();
        partial void OnCASH_OUT_10PChanging(System.Nullable<int> value);
        partial void OnCASH_OUT_10PChanged();
        partial void OnCASH_OUT_20PChanging(System.Nullable<int> value);
        partial void OnCASH_OUT_20PChanged();
        partial void OnCASH_OUT_50PChanging(System.Nullable<int> value);
        partial void OnCASH_OUT_50PChanged();
        partial void OnCASH_OUT_100PChanging(System.Nullable<int> value);
        partial void OnCASH_OUT_100PChanged();
        partial void OnCASH_OUT_200PChanging(System.Nullable<int> value);
        partial void OnCASH_OUT_200PChanged();
        partial void OnCASH_OUT_500PChanging(System.Nullable<int> value);
        partial void OnCASH_OUT_500PChanged();
        partial void OnCASH_OUT_1000PChanging(System.Nullable<int> value);
        partial void OnCASH_OUT_1000PChanged();
        partial void OnCASH_OUT_2000PChanging(System.Nullable<int> value);
        partial void OnCASH_OUT_2000PChanged();
        partial void OnCASH_OUT_5000PChanging(System.Nullable<int> value);
        partial void OnCASH_OUT_5000PChanged();
        partial void OnCASH_OUT_10000PChanging(System.Nullable<int> value);
        partial void OnCASH_OUT_10000PChanged();
        partial void OnCASH_OUT_20000PChanging(System.Nullable<int> value);
        partial void OnCASH_OUT_20000PChanged();
        partial void OnCASH_OUT_50000PChanging(System.Nullable<int> value);
        partial void OnCASH_OUT_50000PChanged();
        partial void OnCASH_OUT_100000PChanging(System.Nullable<int> value);
        partial void OnCASH_OUT_100000PChanged();
        partial void OnTOKEN_OUT_5PChanging(System.Nullable<int> value);
        partial void OnTOKEN_OUT_5PChanged();
        partial void OnTOKEN_OUT_10PChanging(System.Nullable<int> value);
        partial void OnTOKEN_OUT_10PChanged();
        partial void OnTOKEN_OUT_20PChanging(System.Nullable<int> value);
        partial void OnTOKEN_OUT_20PChanged();
        partial void OnTOKEN_OUT_50PChanging(System.Nullable<int> value);
        partial void OnTOKEN_OUT_50PChanged();
        partial void OnTOKEN_OUT_100PChanging(System.Nullable<int> value);
        partial void OnTOKEN_OUT_100PChanged();
        partial void OnTOKEN_OUT_200PChanging(System.Nullable<int> value);
        partial void OnTOKEN_OUT_200PChanged();
        partial void OnTOKEN_OUT_500PChanging(System.Nullable<int> value);
        partial void OnTOKEN_OUT_500PChanged();
        partial void OnTOKEN_OUT_1000PChanging(System.Nullable<int> value);
        partial void OnTOKEN_OUT_1000PChanged();
        partial void OnCASH_REFILL_5PChanging(System.Nullable<int> value);
        partial void OnCASH_REFILL_5PChanged();
        partial void OnCASH_REFILL_10PChanging(System.Nullable<int> value);
        partial void OnCASH_REFILL_10PChanged();
        partial void OnCASH_REFILL_20PChanging(System.Nullable<int> value);
        partial void OnCASH_REFILL_20PChanged();
        partial void OnCASH_REFILL_50PChanging(System.Nullable<int> value);
        partial void OnCASH_REFILL_50PChanged();
        partial void OnCASH_REFILL_100PChanging(System.Nullable<int> value);
        partial void OnCASH_REFILL_100PChanged();
        partial void OnCASH_REFILL_200PChanging(System.Nullable<int> value);
        partial void OnCASH_REFILL_200PChanged();
        partial void OnCASH_REFILL_500PChanging(System.Nullable<int> value);
        partial void OnCASH_REFILL_500PChanged();
        partial void OnCASH_REFILL_1000PChanging(System.Nullable<int> value);
        partial void OnCASH_REFILL_1000PChanged();
        partial void OnCASH_REFILL_2000PChanging(System.Nullable<int> value);
        partial void OnCASH_REFILL_2000PChanged();
        partial void OnCASH_REFILL_5000PChanging(System.Nullable<int> value);
        partial void OnCASH_REFILL_5000PChanged();
        partial void OnCASH_REFILL_10000PChanging(System.Nullable<int> value);
        partial void OnCASH_REFILL_10000PChanged();
        partial void OnCASH_REFILL_20000PChanging(System.Nullable<int> value);
        partial void OnCASH_REFILL_20000PChanged();
        partial void OnCASH_REFILL_50000PChanging(System.Nullable<int> value);
        partial void OnCASH_REFILL_50000PChanged();
        partial void OnCASH_REFILL_100000PChanging(System.Nullable<int> value);
        partial void OnCASH_REFILL_100000PChanged();
        partial void OnTOKEN_REFILL_5PChanging(System.Nullable<int> value);
        partial void OnTOKEN_REFILL_5PChanged();
        partial void OnTOKEN_REFILL_10PChanging(System.Nullable<int> value);
        partial void OnTOKEN_REFILL_10PChanged();
        partial void OnTOKEN_REFILL_20PChanging(System.Nullable<int> value);
        partial void OnTOKEN_REFILL_20PChanged();
        partial void OnTOKEN_REFILL_50PChanging(System.Nullable<int> value);
        partial void OnTOKEN_REFILL_50PChanged();
        partial void OnTOKEN_REFILL_100PChanging(System.Nullable<int> value);
        partial void OnTOKEN_REFILL_100PChanged();
        partial void OnTOKEN_REFILL_200PChanging(System.Nullable<int> value);
        partial void OnTOKEN_REFILL_200PChanged();
        partial void OnTOKEN_REFILL_500PChanging(System.Nullable<int> value);
        partial void OnTOKEN_REFILL_500PChanged();
        partial void OnTOKEN_REFILL_1000PChanging(System.Nullable<int> value);
        partial void OnTOKEN_REFILL_1000PChanged();
        partial void OnDeclarationChanging(System.Nullable<bool> value);
        partial void OnDeclarationChanged();
        partial void OnTreasury_TotalChanging(System.Nullable<float> value);
        partial void OnTreasury_TotalChanged();
        partial void OnCounterCashInChanging(System.Nullable<int> value);
        partial void OnCounterCashInChanged();
        partial void OnCounterCashOutChanging(System.Nullable<int> value);
        partial void OnCounterCashOutChanged();
        partial void OnCounterTokensInChanging(System.Nullable<int> value);
        partial void OnCounterTokensInChanged();
        partial void OnCounterTokensOutChanging(System.Nullable<int> value);
        partial void OnCounterTokensOutChanged();
        partial void OnCounterPrizeChanging(System.Nullable<int> value);
        partial void OnCounterPrizeChanged();
        partial void OnCounterTournamentChanging(System.Nullable<int> value);
        partial void OnCounterTournamentChanged();
        partial void OnCounterJukeboxChanging(System.Nullable<int> value);
        partial void OnCounterJukeboxChanged();
        partial void OnCounterRefillsChanging(System.Nullable<int> value);
        partial void OnCounterRefillsChanged();
        partial void OnCashCollectedChanging(System.Nullable<float> value);
        partial void OnCashCollectedChanged();
        partial void OnTokensCollectedChanging(System.Nullable<float> value);
        partial void OnTokensCollectedChanged();
        partial void OnCash_Collected_1pChanging(System.Nullable<float> value);
        partial void OnCash_Collected_1pChanged();
        partial void OnCash_Collected_2pChanging(System.Nullable<float> value);
        partial void OnCash_Collected_2pChanged();
        partial void OnCash_Collected_5pChanging(System.Nullable<float> value);
        partial void OnCash_Collected_5pChanged();
        partial void OnCash_Collected_10pChanging(System.Nullable<float> value);
        partial void OnCash_Collected_10pChanged();
        partial void OnCash_Collected_20pChanging(System.Nullable<float> value);
        partial void OnCash_Collected_20pChanged();
        partial void OnCash_Collected_50pChanging(System.Nullable<float> value);
        partial void OnCash_Collected_50pChanged();
        partial void OnCash_Collected_100pChanging(System.Nullable<float> value);
        partial void OnCash_Collected_100pChanged();
        partial void OnCash_Collected_200pChanging(System.Nullable<float> value);
        partial void OnCash_Collected_200pChanged();
        partial void OnCash_Collected_500pChanging(System.Nullable<float> value);
        partial void OnCash_Collected_500pChanged();
        partial void OnCash_Collected_1000pChanging(System.Nullable<float> value);
        partial void OnCash_Collected_1000pChanged();
        partial void OnCash_Collected_2000pChanging(System.Nullable<float> value);
        partial void OnCash_Collected_2000pChanged();
        partial void OnCash_Collected_5000pChanging(System.Nullable<float> value);
        partial void OnCash_Collected_5000pChanged();
        partial void OnCash_Collected_10000pChanging(System.Nullable<float> value);
        partial void OnCash_Collected_10000pChanged();
        partial void OnCash_Collected_20000pChanging(System.Nullable<float> value);
        partial void OnCash_Collected_20000pChanged();
        partial void OnCash_Collected_50000pChanging(System.Nullable<float> value);
        partial void OnCash_Collected_50000pChanged();
        partial void OnCash_Collected_100000pChanging(System.Nullable<float> value);
        partial void OnCash_Collected_100000pChanged();
        partial void OnCashRefillsChanging(System.Nullable<float> value);
        partial void OnCashRefillsChanged();
        partial void OnTokenRefillsChanging(System.Nullable<float> value);
        partial void OnTokenRefillsChanged();
        partial void OnCash_Refills_2pChanging(System.Nullable<float> value);
        partial void OnCash_Refills_2pChanged();
        partial void OnCash_Refills_5pChanging(System.Nullable<float> value);
        partial void OnCash_Refills_5pChanged();
        partial void OnCash_Refills_10pChanging(System.Nullable<float> value);
        partial void OnCash_Refills_10pChanged();
        partial void OnCash_Refills_20pChanging(System.Nullable<float> value);
        partial void OnCash_Refills_20pChanged();
        partial void OnCash_Refills_50pChanging(System.Nullable<float> value);
        partial void OnCash_Refills_50pChanged();
        partial void OnCash_Refills_100pChanging(System.Nullable<float> value);
        partial void OnCash_Refills_100pChanged();
        partial void OnCash_Refills_200pChanging(System.Nullable<float> value);
        partial void OnCash_Refills_200pChanged();
        partial void OnCash_Refills_500pChanging(System.Nullable<float> value);
        partial void OnCash_Refills_500pChanged();
        partial void OnCash_Refills_1000pChanging(System.Nullable<float> value);
        partial void OnCash_Refills_1000pChanged();
        partial void OnCash_Refills_2000pChanging(System.Nullable<float> value);
        partial void OnCash_Refills_2000pChanged();
        partial void OnCash_Refills_5000pChanging(System.Nullable<float> value);
        partial void OnCash_Refills_5000pChanged();
        partial void OnCash_Refills_10000pChanging(System.Nullable<float> value);
        partial void OnCash_Refills_10000pChanged();
        partial void OnCash_Refills_20000pChanging(System.Nullable<float> value);
        partial void OnCash_Refills_20000pChanged();
        partial void OnCash_Refills_50000pChanging(System.Nullable<float> value);
        partial void OnCash_Refills_50000pChanged();
        partial void OnCash_Refills_100000pChanging(System.Nullable<float> value);
        partial void OnCash_Refills_100000pChanged();
        partial void OnCounterCashInElectronicChanging(System.Nullable<int> value);
        partial void OnCounterCashInElectronicChanged();
        partial void OnCounterCashOutElectronicChanging(System.Nullable<int> value);
        partial void OnCounterCashOutElectronicChanged();
        partial void OnCounterTokensInElectronicChanging(System.Nullable<int> value);
        partial void OnCounterTokensInElectronicChanged();
        partial void OnCounterTokensOutElectronicChanging(System.Nullable<int> value);
        partial void OnCounterTokensOutElectronicChanged();
        partial void OnJackpotsOutChanging(System.Nullable<int> value);
        partial void OnJackpotsOutChanged();
        partial void OnPreviousCounterCashInChanging(System.Nullable<int> value);
        partial void OnPreviousCounterCashInChanged();
        partial void OnPreviousCounterCashOutChanging(System.Nullable<int> value);
        partial void OnPreviousCounterCashOutChanged();
        partial void OnPreviousCounterTokensInChanging(System.Nullable<int> value);
        partial void OnPreviousCounterTokensInChanged();
        partial void OnPreviousCounterTokensOutChanging(System.Nullable<int> value);
        partial void OnPreviousCounterTokensOutChanged();
        partial void OnPreviousCounterPrizeChanging(System.Nullable<int> value);
        partial void OnPreviousCounterPrizeChanged();
        partial void OnPreviousCounterJackpotsOutChanging(System.Nullable<int> value);
        partial void OnPreviousCounterJackpotsOutChanged();
        partial void OnPreviousCounterTournamentChanging(System.Nullable<int> value);
        partial void OnPreviousCounterTournamentChanged();
        partial void OnPreviousCounterJukeboxChanging(System.Nullable<int> value);
        partial void OnPreviousCounterJukeboxChanged();
        partial void OnPreviousCounterRefillsChanging(System.Nullable<int> value);
        partial void OnPreviousCounterRefillsChanged();
        partial void OnPreviousCounterCashInElectronicChanging(System.Nullable<int> value);
        partial void OnPreviousCounterCashInElectronicChanged();
        partial void OnPreviousCounterCashOutElectronicChanging(System.Nullable<int> value);
        partial void OnPreviousCounterCashOutElectronicChanged();
        partial void OnPreviousCounterTokensInElectronicChanging(System.Nullable<int> value);
        partial void OnPreviousCounterTokensInElectronicChanged();
        partial void OnPreviousCounterTokensOutElectronicChanging(System.Nullable<int> value);
        partial void OnPreviousCounterTokensOutElectronicChanged();
        partial void OnPreviousCollectionDateChanging(System.Nullable<System.DateTime> value);
        partial void OnPreviousCollectionDateChanged();
        partial void OnPreviousCollectionNoChanging(System.Nullable<int> value);
        partial void OnPreviousCollectionNoChanged();
        partial void OnTreasury_RefillsChanging(System.Nullable<float> value);
        partial void OnTreasury_RefillsChanged();
        partial void OnTreasury_RepaymentsChanging(System.Nullable<float> value);
        partial void OnTreasury_RepaymentsChanged();
        partial void OnTreasury_TokensChanging(System.Nullable<float> value);
        partial void OnTreasury_TokensChanged();
        partial void OnExpectedBaggedCashChanging(System.Nullable<float> value);
        partial void OnExpectedBaggedCashChanged();
        partial void OnActualBaggedCashChanging(System.Nullable<float> value);
        partial void OnActualBaggedCashChanged();
        partial void OnCollection_Meters_Coins_InChanging(System.Nullable<int> value);
        partial void OnCollection_Meters_Coins_InChanged();
        partial void OnCollection_Meters_Coins_OutChanging(System.Nullable<int> value);
        partial void OnCollection_Meters_Coins_OutChanged();
        partial void OnCollection_Meters_Coin_DropChanging(System.Nullable<int> value);
        partial void OnCollection_Meters_Coin_DropChanged();
        partial void OnCollection_Meters_HandpayChanging(System.Nullable<int> value);
        partial void OnCollection_Meters_HandpayChanged();
        partial void OnCollection_Meters_External_CreditChanging(System.Nullable<int> value);
        partial void OnCollection_Meters_External_CreditChanged();
        partial void OnCollection_Meters_Games_BetChanging(System.Nullable<int> value);
        partial void OnCollection_Meters_Games_BetChanged();
        partial void OnCollection_Meters_Games_WonChanging(System.Nullable<int> value);
        partial void OnCollection_Meters_Games_WonChanged();
        partial void OnCollection_Meters_NotesChanging(System.Nullable<int> value);
        partial void OnCollection_Meters_NotesChanged();
        partial void OnCollection_Treasury_DefloatChanging(System.Nullable<float> value);
        partial void OnCollection_Treasury_DefloatChanged();
        partial void OnCollection_Defloat_CollectionChanging(System.Nullable<bool> value);
        partial void OnCollection_Defloat_CollectionChanged();
        partial void OnPrevious_Meters_Coins_InChanging(System.Nullable<int> value);
        partial void OnPrevious_Meters_Coins_InChanged();
        partial void OnPrevious_Meters_Coins_OutChanging(System.Nullable<int> value);
        partial void OnPrevious_Meters_Coins_OutChanged();
        partial void OnPrevious_Meters_Coin_DropChanging(System.Nullable<int> value);
        partial void OnPrevious_Meters_Coin_DropChanged();
        partial void OnPrevious_Meters_HandpayChanging(System.Nullable<int> value);
        partial void OnPrevious_Meters_HandpayChanged();
        partial void OnPrevious_Meters_External_CreditChanging(System.Nullable<int> value);
        partial void OnPrevious_Meters_External_CreditChanged();
        partial void OnPrevious_Meters_Games_BetChanging(System.Nullable<int> value);
        partial void OnPrevious_Meters_Games_BetChanged();
        partial void OnPrevious_Meters_Games_WonChanging(System.Nullable<int> value);
        partial void OnPrevious_Meters_Games_WonChanged();
        partial void OnPrevious_Meters_NotesChanging(System.Nullable<int> value);
        partial void OnPrevious_Meters_NotesChanged();
        partial void OnTreasury_HandpayChanging(System.Nullable<float> value);
        partial void OnTreasury_HandpayChanged();
        partial void OnOperator_Week_IDChanging(System.Nullable<int> value);
        partial void OnOperator_Week_IDChanged();
        partial void OnOperator_Period_IDChanging(System.Nullable<int> value);
        partial void OnOperator_Period_IDChanged();
        partial void OnCollectionHandHeldMetersReceivedChanging(System.Nullable<int> value);
        partial void OnCollectionHandHeldMetersReceivedChanged();
        partial void OnCollectionNoDoorEventsChanging(System.Nullable<int> value);
        partial void OnCollectionNoDoorEventsChanged();
        partial void OnCollectionNoPowerEventsChanging(System.Nullable<int> value);
        partial void OnCollectionNoPowerEventsChanged();
        partial void OnCollectionNoFaultEventsChanging(System.Nullable<int> value);
        partial void OnCollectionNoFaultEventsChanged();
        partial void OnCollectionTotalDurationPowerChanging(System.Nullable<int> value);
        partial void OnCollectionTotalDurationPowerChanged();
        partial void OnCollectionTotalDurationDoorChanging(System.Nullable<int> value);
        partial void OnCollectionTotalDurationDoorChanged();
        partial void OnCOLLECTION_RDC_VTPChanging(System.Nullable<int> value);
        partial void OnCOLLECTION_RDC_VTPChanged();
        partial void OnCollection_NetExChanging(System.Nullable<float> value);
        partial void OnCollection_NetExChanged();
        partial void OnCollection_VAT_RateChanging(System.Nullable<float> value);
        partial void OnCollection_VAT_RateChanged();
        partial void OnCOLLECTION_RDC_COINS_INChanging(System.Nullable<int> value);
        partial void OnCOLLECTION_RDC_COINS_INChanged();
        partial void OnCOLLECTION_RDC_COINS_OUTChanging(System.Nullable<int> value);
        partial void OnCOLLECTION_RDC_COINS_OUTChanged();
        partial void OnCOLLECTION_RDC_COIN_DROPChanging(System.Nullable<int> value);
        partial void OnCOLLECTION_RDC_COIN_DROPChanged();
        partial void OnCOLLECTION_RDC_HANDPAYChanging(System.Nullable<int> value);
        partial void OnCOLLECTION_RDC_HANDPAYChanged();
        partial void OnCOLLECTION_RDC_EXTERNAL_CREDITChanging(System.Nullable<int> value);
        partial void OnCOLLECTION_RDC_EXTERNAL_CREDITChanged();
        partial void OnCOLLECTION_RDC_GAMES_BETChanging(System.Nullable<int> value);
        partial void OnCOLLECTION_RDC_GAMES_BETChanged();
        partial void OnCOLLECTION_RDC_GAMES_WONChanging(System.Nullable<int> value);
        partial void OnCOLLECTION_RDC_GAMES_WONChanged();
        partial void OnCOLLECTION_RDC_NOTESChanging(System.Nullable<int> value);
        partial void OnCOLLECTION_RDC_NOTESChanged();
        partial void OnCOLLECTION_RDC_CANCELLED_CREDITSChanging(System.Nullable<int> value);
        partial void OnCOLLECTION_RDC_CANCELLED_CREDITSChanged();
        partial void OnCOLLECTION_RDC_GAMES_LOSTChanging(System.Nullable<int> value);
        partial void OnCOLLECTION_RDC_GAMES_LOSTChanged();
        partial void OnCOLLECTION_RDC_GAMES_SINCE_POWER_UPChanging(System.Nullable<int> value);
        partial void OnCOLLECTION_RDC_GAMES_SINCE_POWER_UPChanged();
        partial void OnCOLLECTION_RDC_TRUE_COIN_INChanging(System.Nullable<int> value);
        partial void OnCOLLECTION_RDC_TRUE_COIN_INChanged();
        partial void OnCOLLECTION_RDC_TRUE_COIN_OUTChanging(System.Nullable<int> value);
        partial void OnCOLLECTION_RDC_TRUE_COIN_OUTChanged();
        partial void OnCOLLECTION_RDC_CURRENT_CREDITSChanging(System.Nullable<int> value);
        partial void OnCOLLECTION_RDC_CURRENT_CREDITSChanged();
        partial void OnCollection_PoP_ActualChanging(System.Nullable<int> value);
        partial void OnCollection_PoP_ActualChanged();
        partial void OnCollection_PoP_ConfiguredChanging(System.Nullable<int> value);
        partial void OnCollection_PoP_ConfiguredChanged();
        partial void OnCollection_EDC_StatusChanging(System.Nullable<int> value);
        partial void OnCollection_EDC_StatusChanged();
        partial void OnCollection_Meter_StatusChanging(System.Nullable<int> value);
        partial void OnCollection_Meter_StatusChanged();
        partial void OnCollection_Cash_StatusChanging(System.Nullable<int> value);
        partial void OnCollection_Cash_StatusChanged();
        partial void OnInstallation_NoChanging(int value);
        partial void OnInstallation_NoChanged();
        partial void OnCollection_DateChanging(System.Nullable<System.DateTime> value);
        partial void OnCollection_DateChanged();
        partial void OnCASH_FLOAT_CHANGE_1pChanging(int value);
        partial void OnCASH_FLOAT_CHANGE_1pChanged();
        partial void OnCASH_FLOAT_CHANGE_2pChanging(int value);
        partial void OnCASH_FLOAT_CHANGE_2pChanged();
        partial void OnCASH_FLOAT_CHANGE_5pChanging(int value);
        partial void OnCASH_FLOAT_CHANGE_5pChanged();
        partial void OnCASH_FLOAT_CHANGE_10pChanging(int value);
        partial void OnCASH_FLOAT_CHANGE_10pChanged();
        partial void OnCASH_FLOAT_CHANGE_20pChanging(int value);
        partial void OnCASH_FLOAT_CHANGE_20pChanged();
        partial void OnCASH_FLOAT_CHANGE_50pChanging(int value);
        partial void OnCASH_FLOAT_CHANGE_50pChanged();
        partial void OnCASH_FLOAT_CHANGE_100pChanging(int value);
        partial void OnCASH_FLOAT_CHANGE_100pChanged();
        partial void OnCASH_FLOAT_CHANGE_200pChanging(int value);
        partial void OnCASH_FLOAT_CHANGE_200pChanged();
        partial void OnCASH_FLOAT_CHANGE_500pChanging(int value);
        partial void OnCASH_FLOAT_CHANGE_500pChanged();
        partial void OnCASH_FLOAT_CHANGE_1000pChanging(int value);
        partial void OnCASH_FLOAT_CHANGE_1000pChanged();
        partial void OnCASH_FLOAT_TOTALChanging(float value);
        partial void OnCASH_FLOAT_TOTALChanged();
        partial void OnDeclaredTicketQtyChanging(System.Nullable<int> value);
        partial void OnDeclaredTicketQtyChanged();
        partial void OnDeclaredTicketValueChanging(System.Nullable<float> value);
        partial void OnDeclaredTicketValueChanged();
        partial void OnCOLLECTION_RDC_JACKPOTChanging(System.Nullable<int> value);
        partial void OnCOLLECTION_RDC_JACKPOTChanged();
        partial void OnCOLLECTION_RDC_TICKETS_INSERTED_VALUEChanging(int value);
        partial void OnCOLLECTION_RDC_TICKETS_INSERTED_VALUEChanged();
        partial void OnCOLLECTION_RDC_TICKETS_PRINTED_VALUEChanging(int value);
        partial void OnCOLLECTION_RDC_TICKETS_PRINTED_VALUEChanged();
        partial void OnDeclaredTicketPrintedQtyChanging(System.Nullable<int> value);
        partial void OnDeclaredTicketPrintedQtyChanged();
        partial void OnDeclaredTicketPrintedValueChanging(System.Nullable<float> value);
        partial void OnDeclaredTicketPrintedValueChanged();
        partial void OnCollection_Date_PerformedChanging(System.Nullable<System.DateTime> value);
        partial void OnCollection_Date_PerformedChanged();
        partial void Onprogressive_win_valueChanging(System.Nullable<int> value);
        partial void Onprogressive_win_valueChanged();
        partial void Onprogressive_win_Handpay_valueChanging(System.Nullable<int> value);
        partial void Onprogressive_win_Handpay_valueChanged();
        partial void OnProgressive_Value_DeclaredChanging(System.Nullable<double> value);
        partial void OnProgressive_Value_DeclaredChanged();
        #endregion

        public Collection()
        {
            OnCreated();
        }

        [Column(Storage = "_Collection_No", AutoSync = AutoSync.OnInsert, DbType = "Int NOT NULL IDENTITY", IsPrimaryKey = true, IsDbGenerated = true)]
        public int Collection_No
        {
            get
            {
                return this._Collection_No;
            }
            set
            {
                if ((this._Collection_No != value))
                {
                    this.OnCollection_NoChanging(value);
                    this.SendPropertyChanging();
                    this._Collection_No = value;
                    this.SendPropertyChanged("Collection_No");
                    this.OnCollection_NoChanged();
                }
            }
        }

        [Column(Storage = "_Collection_Batch_No", DbType = "Int")]
        public System.Nullable<int> Collection_Batch_No
        {
            get
            {
                return this._Collection_Batch_No;
            }
            set
            {
                if ((this._Collection_Batch_No != value))
                {
                    this.OnCollection_Batch_NoChanging(value);
                    this.SendPropertyChanging();
                    this._Collection_Batch_No = value;
                    this.SendPropertyChanged("Collection_Batch_No");
                    this.OnCollection_Batch_NoChanged();
                }
            }
        }

        [Column(Storage = "_Machine_Serial", DbType = "VarChar(50)")]
        public string Machine_Serial
        {
            get
            {
                return this._Machine_Serial;
            }
            set
            {
                if ((this._Machine_Serial != value))
                {
                    this.OnMachine_SerialChanging(value);
                    this.SendPropertyChanging();
                    this._Machine_Serial = value;
                    this.SendPropertyChanged("Machine_Serial");
                    this.OnMachine_SerialChanged();
                }
            }
        }

        [Column(Storage = "_Collection_RDC_Machine_Code", DbType = "VarChar(10)")]
        public string Collection_RDC_Machine_Code
        {
            get
            {
                return this._Collection_RDC_Machine_Code;
            }
            set
            {
                if ((this._Collection_RDC_Machine_Code != value))
                {
                    this.OnCollection_RDC_Machine_CodeChanging(value);
                    this.SendPropertyChanging();
                    this._Collection_RDC_Machine_Code = value;
                    this.SendPropertyChanged("Collection_RDC_Machine_Code");
                    this.OnCollection_RDC_Machine_CodeChanged();
                }
            }
        }

        [Column(Storage = "_Collection_RDC_Secondary_Machine_Code", DbType = "VarChar(20)")]
        public string Collection_RDC_Secondary_Machine_Code
        {
            get
            {
                return this._Collection_RDC_Secondary_Machine_Code;
            }
            set
            {
                if ((this._Collection_RDC_Secondary_Machine_Code != value))
                {
                    this.OnCollection_RDC_Secondary_Machine_CodeChanging(value);
                    this.SendPropertyChanging();
                    this._Collection_RDC_Secondary_Machine_Code = value;
                    this.SendPropertyChanged("Collection_RDC_Secondary_Machine_Code");
                    this.OnCollection_RDC_Secondary_Machine_CodeChanged();
                }
            }
        }

        [Column(Storage = "_Datapak_Read_Occurrence", DbType = "Int")]
        public System.Nullable<int> Datapak_Read_Occurrence
        {
            get
            {
                return this._Datapak_Read_Occurrence;
            }
            set
            {
                if ((this._Datapak_Read_Occurrence != value))
                {
                    this.OnDatapak_Read_OccurrenceChanging(value);
                    this.SendPropertyChanging();
                    this._Datapak_Read_Occurrence = value;
                    this.SendPropertyChanged("Datapak_Read_Occurrence");
                    this.OnDatapak_Read_OccurrenceChanged();
                }
            }
        }

        [Column(Storage = "_Float_Level", DbType = "Int")]
        public System.Nullable<int> Float_Level
        {
            get
            {
                return this._Float_Level;
            }
            set
            {
                if ((this._Float_Level != value))
                {
                    this.OnFloat_LevelChanging(value);
                    this.SendPropertyChanging();
                    this._Float_Level = value;
                    this.SendPropertyChanged("Float_Level");
                    this.OnFloat_LevelChanged();
                }
            }
        }

        [Column(Storage = "_Period_ID", DbType = "Int")]
        public System.Nullable<int> Period_ID
        {
            get
            {
                return this._Period_ID;
            }
            set
            {
                if ((this._Period_ID != value))
                {
                    this.OnPeriod_IDChanging(value);
                    this.SendPropertyChanging();
                    this._Period_ID = value;
                    this.SendPropertyChanged("Period_ID");
                    this.OnPeriod_IDChanged();
                }
            }
        }

        [Column(Storage = "_Week_ID", DbType = "Int")]
        public System.Nullable<int> Week_ID
        {
            get
            {
                return this._Week_ID;
            }
            set
            {
                if ((this._Week_ID != value))
                {
                    this.OnWeek_IDChanging(value);
                    this.SendPropertyChanging();
                    this._Week_ID = value;
                    this.SendPropertyChanged("Week_ID");
                    this.OnWeek_IDChanged();
                }
            }
        }

        [Column(Storage = "_CASH_IN_1P", DbType = "Int")]
        public System.Nullable<int> CASH_IN_1P
        {
            get
            {
                return this._CASH_IN_1P;
            }
            set
            {
                if ((this._CASH_IN_1P != value))
                {
                    this.OnCASH_IN_1PChanging(value);
                    this.SendPropertyChanging();
                    this._CASH_IN_1P = value;
                    this.SendPropertyChanged("CASH_IN_1P");
                    this.OnCASH_IN_1PChanged();
                }
            }
        }

        [Column(Storage = "_CASH_IN_2P", DbType = "Int")]
        public System.Nullable<int> CASH_IN_2P
        {
            get
            {
                return this._CASH_IN_2P;
            }
            set
            {
                if ((this._CASH_IN_2P != value))
                {
                    this.OnCASH_IN_2PChanging(value);
                    this.SendPropertyChanging();
                    this._CASH_IN_2P = value;
                    this.SendPropertyChanged("CASH_IN_2P");
                    this.OnCASH_IN_2PChanged();
                }
            }
        }

        [Column(Storage = "_CASH_IN_5P", DbType = "Int")]
        public System.Nullable<int> CASH_IN_5P
        {
            get
            {
                return this._CASH_IN_5P;
            }
            set
            {
                if ((this._CASH_IN_5P != value))
                {
                    this.OnCASH_IN_5PChanging(value);
                    this.SendPropertyChanging();
                    this._CASH_IN_5P = value;
                    this.SendPropertyChanged("CASH_IN_5P");
                    this.OnCASH_IN_5PChanged();
                }
            }
        }

        [Column(Storage = "_CASH_IN_10P", DbType = "Int")]
        public System.Nullable<int> CASH_IN_10P
        {
            get
            {
                return this._CASH_IN_10P;
            }
            set
            {
                if ((this._CASH_IN_10P != value))
                {
                    this.OnCASH_IN_10PChanging(value);
                    this.SendPropertyChanging();
                    this._CASH_IN_10P = value;
                    this.SendPropertyChanged("CASH_IN_10P");
                    this.OnCASH_IN_10PChanged();
                }
            }
        }

        [Column(Storage = "_CASH_IN_20P", DbType = "Int")]
        public System.Nullable<int> CASH_IN_20P
        {
            get
            {
                return this._CASH_IN_20P;
            }
            set
            {
                if ((this._CASH_IN_20P != value))
                {
                    this.OnCASH_IN_20PChanging(value);
                    this.SendPropertyChanging();
                    this._CASH_IN_20P = value;
                    this.SendPropertyChanged("CASH_IN_20P");
                    this.OnCASH_IN_20PChanged();
                }
            }
        }

        [Column(Storage = "_CASH_IN_50P", DbType = "Int")]
        public System.Nullable<int> CASH_IN_50P
        {
            get
            {
                return this._CASH_IN_50P;
            }
            set
            {
                if ((this._CASH_IN_50P != value))
                {
                    this.OnCASH_IN_50PChanging(value);
                    this.SendPropertyChanging();
                    this._CASH_IN_50P = value;
                    this.SendPropertyChanged("CASH_IN_50P");
                    this.OnCASH_IN_50PChanged();
                }
            }
        }

        [Column(Storage = "_CASH_IN_100P", DbType = "Int")]
        public System.Nullable<int> CASH_IN_100P
        {
            get
            {
                return this._CASH_IN_100P;
            }
            set
            {
                if ((this._CASH_IN_100P != value))
                {
                    this.OnCASH_IN_100PChanging(value);
                    this.SendPropertyChanging();
                    this._CASH_IN_100P = value;
                    this.SendPropertyChanged("CASH_IN_100P");
                    this.OnCASH_IN_100PChanged();
                }
            }
        }

        [Column(Storage = "_CASH_IN_200P", DbType = "Int")]
        public System.Nullable<int> CASH_IN_200P
        {
            get
            {
                return this._CASH_IN_200P;
            }
            set
            {
                if ((this._CASH_IN_200P != value))
                {
                    this.OnCASH_IN_200PChanging(value);
                    this.SendPropertyChanging();
                    this._CASH_IN_200P = value;
                    this.SendPropertyChanged("CASH_IN_200P");
                    this.OnCASH_IN_200PChanged();
                }
            }
        }

        [Column(Storage = "_CASH_IN_500P", DbType = "Int")]
        public System.Nullable<int> CASH_IN_500P
        {
            get
            {
                return this._CASH_IN_500P;
            }
            set
            {
                if ((this._CASH_IN_500P != value))
                {
                    this.OnCASH_IN_500PChanging(value);
                    this.SendPropertyChanging();
                    this._CASH_IN_500P = value;
                    this.SendPropertyChanged("CASH_IN_500P");
                    this.OnCASH_IN_500PChanged();
                }
            }
        }

        [Column(Storage = "_CASH_IN_1000P", DbType = "Int")]
        public System.Nullable<int> CASH_IN_1000P
        {
            get
            {
                return this._CASH_IN_1000P;
            }
            set
            {
                if ((this._CASH_IN_1000P != value))
                {
                    this.OnCASH_IN_1000PChanging(value);
                    this.SendPropertyChanging();
                    this._CASH_IN_1000P = value;
                    this.SendPropertyChanged("CASH_IN_1000P");
                    this.OnCASH_IN_1000PChanged();
                }
            }
        }

        [Column(Storage = "_CASH_IN_2000P", DbType = "Int")]
        public System.Nullable<int> CASH_IN_2000P
        {
            get
            {
                return this._CASH_IN_2000P;
            }
            set
            {
                if ((this._CASH_IN_2000P != value))
                {
                    this.OnCASH_IN_2000PChanging(value);
                    this.SendPropertyChanging();
                    this._CASH_IN_2000P = value;
                    this.SendPropertyChanged("CASH_IN_2000P");
                    this.OnCASH_IN_2000PChanged();
                }
            }
        }

        [Column(Storage = "_CASH_IN_5000P", DbType = "Int")]
        public System.Nullable<int> CASH_IN_5000P
        {
            get
            {
                return this._CASH_IN_5000P;
            }
            set
            {
                if ((this._CASH_IN_5000P != value))
                {
                    this.OnCASH_IN_5000PChanging(value);
                    this.SendPropertyChanging();
                    this._CASH_IN_5000P = value;
                    this.SendPropertyChanged("CASH_IN_5000P");
                    this.OnCASH_IN_5000PChanged();
                }
            }
        }

        [Column(Storage = "_CASH_IN_10000P", DbType = "Int")]
        public System.Nullable<int> CASH_IN_10000P
        {
            get
            {
                return this._CASH_IN_10000P;
            }
            set
            {
                if ((this._CASH_IN_10000P != value))
                {
                    this.OnCASH_IN_10000PChanging(value);
                    this.SendPropertyChanging();
                    this._CASH_IN_10000P = value;
                    this.SendPropertyChanged("CASH_IN_10000P");
                    this.OnCASH_IN_10000PChanged();
                }
            }
        }

        [Column(Storage = "_CASH_IN_20000P", DbType = "Int")]
        public System.Nullable<int> CASH_IN_20000P
        {
            get
            {
                return this._CASH_IN_20000P;
            }
            set
            {
                if ((this._CASH_IN_20000P != value))
                {
                    this.OnCASH_IN_20000PChanging(value);
                    this.SendPropertyChanging();
                    this._CASH_IN_20000P = value;
                    this.SendPropertyChanged("CASH_IN_20000P");
                    this.OnCASH_IN_20000PChanged();
                }
            }
        }

        [Column(Storage = "_CASH_IN_50000P", DbType = "Int")]
        public System.Nullable<int> CASH_IN_50000P
        {
            get
            {
                return this._CASH_IN_50000P;
            }
            set
            {
                if ((this._CASH_IN_50000P != value))
                {
                    this.OnCASH_IN_50000PChanging(value);
                    this.SendPropertyChanging();
                    this._CASH_IN_50000P = value;
                    this.SendPropertyChanged("CASH_IN_50000P");
                    this.OnCASH_IN_50000PChanged();
                }
            }
        }

        [Column(Storage = "_CASH_IN_100000P", DbType = "Int")]
        public System.Nullable<int> CASH_IN_100000P
        {
            get
            {
                return this._CASH_IN_100000P;
            }
            set
            {
                if ((this._CASH_IN_100000P != value))
                {
                    this.OnCASH_IN_100000PChanging(value);
                    this.SendPropertyChanging();
                    this._CASH_IN_100000P = value;
                    this.SendPropertyChanged("CASH_IN_100000P");
                    this.OnCASH_IN_100000PChanged();
                }
            }
        }

        [Column(Storage = "_TOKEN_IN_5P", DbType = "Int")]
        public System.Nullable<int> TOKEN_IN_5P
        {
            get
            {
                return this._TOKEN_IN_5P;
            }
            set
            {
                if ((this._TOKEN_IN_5P != value))
                {
                    this.OnTOKEN_IN_5PChanging(value);
                    this.SendPropertyChanging();
                    this._TOKEN_IN_5P = value;
                    this.SendPropertyChanged("TOKEN_IN_5P");
                    this.OnTOKEN_IN_5PChanged();
                }
            }
        }

        [Column(Storage = "_TOKEN_IN_10P", DbType = "Int")]
        public System.Nullable<int> TOKEN_IN_10P
        {
            get
            {
                return this._TOKEN_IN_10P;
            }
            set
            {
                if ((this._TOKEN_IN_10P != value))
                {
                    this.OnTOKEN_IN_10PChanging(value);
                    this.SendPropertyChanging();
                    this._TOKEN_IN_10P = value;
                    this.SendPropertyChanged("TOKEN_IN_10P");
                    this.OnTOKEN_IN_10PChanged();
                }
            }
        }

        [Column(Storage = "_TOKEN_IN_20P", DbType = "Int")]
        public System.Nullable<int> TOKEN_IN_20P
        {
            get
            {
                return this._TOKEN_IN_20P;
            }
            set
            {
                if ((this._TOKEN_IN_20P != value))
                {
                    this.OnTOKEN_IN_20PChanging(value);
                    this.SendPropertyChanging();
                    this._TOKEN_IN_20P = value;
                    this.SendPropertyChanged("TOKEN_IN_20P");
                    this.OnTOKEN_IN_20PChanged();
                }
            }
        }

        [Column(Storage = "_TOKEN_IN_50P", DbType = "Int")]
        public System.Nullable<int> TOKEN_IN_50P
        {
            get
            {
                return this._TOKEN_IN_50P;
            }
            set
            {
                if ((this._TOKEN_IN_50P != value))
                {
                    this.OnTOKEN_IN_50PChanging(value);
                    this.SendPropertyChanging();
                    this._TOKEN_IN_50P = value;
                    this.SendPropertyChanged("TOKEN_IN_50P");
                    this.OnTOKEN_IN_50PChanged();
                }
            }
        }

        [Column(Storage = "_TOKEN_IN_100P", DbType = "Int")]
        public System.Nullable<int> TOKEN_IN_100P
        {
            get
            {
                return this._TOKEN_IN_100P;
            }
            set
            {
                if ((this._TOKEN_IN_100P != value))
                {
                    this.OnTOKEN_IN_100PChanging(value);
                    this.SendPropertyChanging();
                    this._TOKEN_IN_100P = value;
                    this.SendPropertyChanged("TOKEN_IN_100P");
                    this.OnTOKEN_IN_100PChanged();
                }
            }
        }

        [Column(Storage = "_TOKEN_IN_200P", DbType = "Int")]
        public System.Nullable<int> TOKEN_IN_200P
        {
            get
            {
                return this._TOKEN_IN_200P;
            }
            set
            {
                if ((this._TOKEN_IN_200P != value))
                {
                    this.OnTOKEN_IN_200PChanging(value);
                    this.SendPropertyChanging();
                    this._TOKEN_IN_200P = value;
                    this.SendPropertyChanged("TOKEN_IN_200P");
                    this.OnTOKEN_IN_200PChanged();
                }
            }
        }

        [Column(Storage = "_TOKEN_IN_500P", DbType = "Int")]
        public System.Nullable<int> TOKEN_IN_500P
        {
            get
            {
                return this._TOKEN_IN_500P;
            }
            set
            {
                if ((this._TOKEN_IN_500P != value))
                {
                    this.OnTOKEN_IN_500PChanging(value);
                    this.SendPropertyChanging();
                    this._TOKEN_IN_500P = value;
                    this.SendPropertyChanged("TOKEN_IN_500P");
                    this.OnTOKEN_IN_500PChanged();
                }
            }
        }

        [Column(Storage = "_TOKEN_IN_1000P", DbType = "Int")]
        public System.Nullable<int> TOKEN_IN_1000P
        {
            get
            {
                return this._TOKEN_IN_1000P;
            }
            set
            {
                if ((this._TOKEN_IN_1000P != value))
                {
                    this.OnTOKEN_IN_1000PChanging(value);
                    this.SendPropertyChanging();
                    this._TOKEN_IN_1000P = value;
                    this.SendPropertyChanged("TOKEN_IN_1000P");
                    this.OnTOKEN_IN_1000PChanged();
                }
            }
        }

        [Column(Storage = "_CASH_OUT_1P", DbType = "Int")]
        public System.Nullable<int> CASH_OUT_1P
        {
            get
            {
                return this._CASH_OUT_1P;
            }
            set
            {
                if ((this._CASH_OUT_1P != value))
                {
                    this.OnCASH_OUT_1PChanging(value);
                    this.SendPropertyChanging();
                    this._CASH_OUT_1P = value;
                    this.SendPropertyChanged("CASH_OUT_1P");
                    this.OnCASH_OUT_1PChanged();
                }
            }
        }

        [Column(Storage = "_CASH_OUT_2P", DbType = "Int")]
        public System.Nullable<int> CASH_OUT_2P
        {
            get
            {
                return this._CASH_OUT_2P;
            }
            set
            {
                if ((this._CASH_OUT_2P != value))
                {
                    this.OnCASH_OUT_2PChanging(value);
                    this.SendPropertyChanging();
                    this._CASH_OUT_2P = value;
                    this.SendPropertyChanged("CASH_OUT_2P");
                    this.OnCASH_OUT_2PChanged();
                }
            }
        }

        [Column(Storage = "_CASH_OUT_5P", DbType = "Int")]
        public System.Nullable<int> CASH_OUT_5P
        {
            get
            {
                return this._CASH_OUT_5P;
            }
            set
            {
                if ((this._CASH_OUT_5P != value))
                {
                    this.OnCASH_OUT_5PChanging(value);
                    this.SendPropertyChanging();
                    this._CASH_OUT_5P = value;
                    this.SendPropertyChanged("CASH_OUT_5P");
                    this.OnCASH_OUT_5PChanged();
                }
            }
        }

        [Column(Storage = "_CASH_OUT_10P", DbType = "Int")]
        public System.Nullable<int> CASH_OUT_10P
        {
            get
            {
                return this._CASH_OUT_10P;
            }
            set
            {
                if ((this._CASH_OUT_10P != value))
                {
                    this.OnCASH_OUT_10PChanging(value);
                    this.SendPropertyChanging();
                    this._CASH_OUT_10P = value;
                    this.SendPropertyChanged("CASH_OUT_10P");
                    this.OnCASH_OUT_10PChanged();
                }
            }
        }

        [Column(Storage = "_CASH_OUT_20P", DbType = "Int")]
        public System.Nullable<int> CASH_OUT_20P
        {
            get
            {
                return this._CASH_OUT_20P;
            }
            set
            {
                if ((this._CASH_OUT_20P != value))
                {
                    this.OnCASH_OUT_20PChanging(value);
                    this.SendPropertyChanging();
                    this._CASH_OUT_20P = value;
                    this.SendPropertyChanged("CASH_OUT_20P");
                    this.OnCASH_OUT_20PChanged();
                }
            }
        }

        [Column(Storage = "_CASH_OUT_50P", DbType = "Int")]
        public System.Nullable<int> CASH_OUT_50P
        {
            get
            {
                return this._CASH_OUT_50P;
            }
            set
            {
                if ((this._CASH_OUT_50P != value))
                {
                    this.OnCASH_OUT_50PChanging(value);
                    this.SendPropertyChanging();
                    this._CASH_OUT_50P = value;
                    this.SendPropertyChanged("CASH_OUT_50P");
                    this.OnCASH_OUT_50PChanged();
                }
            }
        }

        [Column(Storage = "_CASH_OUT_100P", DbType = "Int")]
        public System.Nullable<int> CASH_OUT_100P
        {
            get
            {
                return this._CASH_OUT_100P;
            }
            set
            {
                if ((this._CASH_OUT_100P != value))
                {
                    this.OnCASH_OUT_100PChanging(value);
                    this.SendPropertyChanging();
                    this._CASH_OUT_100P = value;
                    this.SendPropertyChanged("CASH_OUT_100P");
                    this.OnCASH_OUT_100PChanged();
                }
            }
        }

        [Column(Storage = "_CASH_OUT_200P", DbType = "Int")]
        public System.Nullable<int> CASH_OUT_200P
        {
            get
            {
                return this._CASH_OUT_200P;
            }
            set
            {
                if ((this._CASH_OUT_200P != value))
                {
                    this.OnCASH_OUT_200PChanging(value);
                    this.SendPropertyChanging();
                    this._CASH_OUT_200P = value;
                    this.SendPropertyChanged("CASH_OUT_200P");
                    this.OnCASH_OUT_200PChanged();
                }
            }
        }

        [Column(Storage = "_CASH_OUT_500P", DbType = "Int")]
        public System.Nullable<int> CASH_OUT_500P
        {
            get
            {
                return this._CASH_OUT_500P;
            }
            set
            {
                if ((this._CASH_OUT_500P != value))
                {
                    this.OnCASH_OUT_500PChanging(value);
                    this.SendPropertyChanging();
                    this._CASH_OUT_500P = value;
                    this.SendPropertyChanged("CASH_OUT_500P");
                    this.OnCASH_OUT_500PChanged();
                }
            }
        }

        [Column(Storage = "_CASH_OUT_1000P", DbType = "Int")]
        public System.Nullable<int> CASH_OUT_1000P
        {
            get
            {
                return this._CASH_OUT_1000P;
            }
            set
            {
                if ((this._CASH_OUT_1000P != value))
                {
                    this.OnCASH_OUT_1000PChanging(value);
                    this.SendPropertyChanging();
                    this._CASH_OUT_1000P = value;
                    this.SendPropertyChanged("CASH_OUT_1000P");
                    this.OnCASH_OUT_1000PChanged();
                }
            }
        }

        [Column(Storage = "_CASH_OUT_2000P", DbType = "Int")]
        public System.Nullable<int> CASH_OUT_2000P
        {
            get
            {
                return this._CASH_OUT_2000P;
            }
            set
            {
                if ((this._CASH_OUT_2000P != value))
                {
                    this.OnCASH_OUT_2000PChanging(value);
                    this.SendPropertyChanging();
                    this._CASH_OUT_2000P = value;
                    this.SendPropertyChanged("CASH_OUT_2000P");
                    this.OnCASH_OUT_2000PChanged();
                }
            }
        }

        [Column(Storage = "_CASH_OUT_5000P", DbType = "Int")]
        public System.Nullable<int> CASH_OUT_5000P
        {
            get
            {
                return this._CASH_OUT_5000P;
            }
            set
            {
                if ((this._CASH_OUT_5000P != value))
                {
                    this.OnCASH_OUT_5000PChanging(value);
                    this.SendPropertyChanging();
                    this._CASH_OUT_5000P = value;
                    this.SendPropertyChanged("CASH_OUT_5000P");
                    this.OnCASH_OUT_5000PChanged();
                }
            }
        }

        [Column(Storage = "_CASH_OUT_10000P", DbType = "Int")]
        public System.Nullable<int> CASH_OUT_10000P
        {
            get
            {
                return this._CASH_OUT_10000P;
            }
            set
            {
                if ((this._CASH_OUT_10000P != value))
                {
                    this.OnCASH_OUT_10000PChanging(value);
                    this.SendPropertyChanging();
                    this._CASH_OUT_10000P = value;
                    this.SendPropertyChanged("CASH_OUT_10000P");
                    this.OnCASH_OUT_10000PChanged();
                }
            }
        }

        [Column(Storage = "_CASH_OUT_20000P", DbType = "Int")]
        public System.Nullable<int> CASH_OUT_20000P
        {
            get
            {
                return this._CASH_OUT_20000P;
            }
            set
            {
                if ((this._CASH_OUT_20000P != value))
                {
                    this.OnCASH_OUT_20000PChanging(value);
                    this.SendPropertyChanging();
                    this._CASH_OUT_20000P = value;
                    this.SendPropertyChanged("CASH_OUT_20000P");
                    this.OnCASH_OUT_20000PChanged();
                }
            }
        }

        [Column(Storage = "_CASH_OUT_50000P", DbType = "Int")]
        public System.Nullable<int> CASH_OUT_50000P
        {
            get
            {
                return this._CASH_OUT_50000P;
            }
            set
            {
                if ((this._CASH_OUT_50000P != value))
                {
                    this.OnCASH_OUT_50000PChanging(value);
                    this.SendPropertyChanging();
                    this._CASH_OUT_50000P = value;
                    this.SendPropertyChanged("CASH_OUT_50000P");
                    this.OnCASH_OUT_50000PChanged();
                }
            }
        }

        [Column(Storage = "_CASH_OUT_100000P", DbType = "Int")]
        public System.Nullable<int> CASH_OUT_100000P
        {
            get
            {
                return this._CASH_OUT_100000P;
            }
            set
            {
                if ((this._CASH_OUT_100000P != value))
                {
                    this.OnCASH_OUT_100000PChanging(value);
                    this.SendPropertyChanging();
                    this._CASH_OUT_100000P = value;
                    this.SendPropertyChanged("CASH_OUT_100000P");
                    this.OnCASH_OUT_100000PChanged();
                }
            }
        }

        [Column(Storage = "_TOKEN_OUT_5P", DbType = "Int")]
        public System.Nullable<int> TOKEN_OUT_5P
        {
            get
            {
                return this._TOKEN_OUT_5P;
            }
            set
            {
                if ((this._TOKEN_OUT_5P != value))
                {
                    this.OnTOKEN_OUT_5PChanging(value);
                    this.SendPropertyChanging();
                    this._TOKEN_OUT_5P = value;
                    this.SendPropertyChanged("TOKEN_OUT_5P");
                    this.OnTOKEN_OUT_5PChanged();
                }
            }
        }

        [Column(Storage = "_TOKEN_OUT_10P", DbType = "Int")]
        public System.Nullable<int> TOKEN_OUT_10P
        {
            get
            {
                return this._TOKEN_OUT_10P;
            }
            set
            {
                if ((this._TOKEN_OUT_10P != value))
                {
                    this.OnTOKEN_OUT_10PChanging(value);
                    this.SendPropertyChanging();
                    this._TOKEN_OUT_10P = value;
                    this.SendPropertyChanged("TOKEN_OUT_10P");
                    this.OnTOKEN_OUT_10PChanged();
                }
            }
        }

        [Column(Storage = "_TOKEN_OUT_20P", DbType = "Int")]
        public System.Nullable<int> TOKEN_OUT_20P
        {
            get
            {
                return this._TOKEN_OUT_20P;
            }
            set
            {
                if ((this._TOKEN_OUT_20P != value))
                {
                    this.OnTOKEN_OUT_20PChanging(value);
                    this.SendPropertyChanging();
                    this._TOKEN_OUT_20P = value;
                    this.SendPropertyChanged("TOKEN_OUT_20P");
                    this.OnTOKEN_OUT_20PChanged();
                }
            }
        }

        [Column(Storage = "_TOKEN_OUT_50P", DbType = "Int")]
        public System.Nullable<int> TOKEN_OUT_50P
        {
            get
            {
                return this._TOKEN_OUT_50P;
            }
            set
            {
                if ((this._TOKEN_OUT_50P != value))
                {
                    this.OnTOKEN_OUT_50PChanging(value);
                    this.SendPropertyChanging();
                    this._TOKEN_OUT_50P = value;
                    this.SendPropertyChanged("TOKEN_OUT_50P");
                    this.OnTOKEN_OUT_50PChanged();
                }
            }
        }

        [Column(Storage = "_TOKEN_OUT_100P", DbType = "Int")]
        public System.Nullable<int> TOKEN_OUT_100P
        {
            get
            {
                return this._TOKEN_OUT_100P;
            }
            set
            {
                if ((this._TOKEN_OUT_100P != value))
                {
                    this.OnTOKEN_OUT_100PChanging(value);
                    this.SendPropertyChanging();
                    this._TOKEN_OUT_100P = value;
                    this.SendPropertyChanged("TOKEN_OUT_100P");
                    this.OnTOKEN_OUT_100PChanged();
                }
            }
        }

        [Column(Storage = "_TOKEN_OUT_200P", DbType = "Int")]
        public System.Nullable<int> TOKEN_OUT_200P
        {
            get
            {
                return this._TOKEN_OUT_200P;
            }
            set
            {
                if ((this._TOKEN_OUT_200P != value))
                {
                    this.OnTOKEN_OUT_200PChanging(value);
                    this.SendPropertyChanging();
                    this._TOKEN_OUT_200P = value;
                    this.SendPropertyChanged("TOKEN_OUT_200P");
                    this.OnTOKEN_OUT_200PChanged();
                }
            }
        }

        [Column(Storage = "_TOKEN_OUT_500P", DbType = "Int")]
        public System.Nullable<int> TOKEN_OUT_500P
        {
            get
            {
                return this._TOKEN_OUT_500P;
            }
            set
            {
                if ((this._TOKEN_OUT_500P != value))
                {
                    this.OnTOKEN_OUT_500PChanging(value);
                    this.SendPropertyChanging();
                    this._TOKEN_OUT_500P = value;
                    this.SendPropertyChanged("TOKEN_OUT_500P");
                    this.OnTOKEN_OUT_500PChanged();
                }
            }
        }

        [Column(Storage = "_TOKEN_OUT_1000P", DbType = "Int")]
        public System.Nullable<int> TOKEN_OUT_1000P
        {
            get
            {
                return this._TOKEN_OUT_1000P;
            }
            set
            {
                if ((this._TOKEN_OUT_1000P != value))
                {
                    this.OnTOKEN_OUT_1000PChanging(value);
                    this.SendPropertyChanging();
                    this._TOKEN_OUT_1000P = value;
                    this.SendPropertyChanged("TOKEN_OUT_1000P");
                    this.OnTOKEN_OUT_1000PChanged();
                }
            }
        }

        [Column(Storage = "_CASH_REFILL_5P", DbType = "Int")]
        public System.Nullable<int> CASH_REFILL_5P
        {
            get
            {
                return this._CASH_REFILL_5P;
            }
            set
            {
                if ((this._CASH_REFILL_5P != value))
                {
                    this.OnCASH_REFILL_5PChanging(value);
                    this.SendPropertyChanging();
                    this._CASH_REFILL_5P = value;
                    this.SendPropertyChanged("CASH_REFILL_5P");
                    this.OnCASH_REFILL_5PChanged();
                }
            }
        }

        [Column(Storage = "_CASH_REFILL_10P", DbType = "Int")]
        public System.Nullable<int> CASH_REFILL_10P
        {
            get
            {
                return this._CASH_REFILL_10P;
            }
            set
            {
                if ((this._CASH_REFILL_10P != value))
                {
                    this.OnCASH_REFILL_10PChanging(value);
                    this.SendPropertyChanging();
                    this._CASH_REFILL_10P = value;
                    this.SendPropertyChanged("CASH_REFILL_10P");
                    this.OnCASH_REFILL_10PChanged();
                }
            }
        }

        [Column(Storage = "_CASH_REFILL_20P", DbType = "Int")]
        public System.Nullable<int> CASH_REFILL_20P
        {
            get
            {
                return this._CASH_REFILL_20P;
            }
            set
            {
                if ((this._CASH_REFILL_20P != value))
                {
                    this.OnCASH_REFILL_20PChanging(value);
                    this.SendPropertyChanging();
                    this._CASH_REFILL_20P = value;
                    this.SendPropertyChanged("CASH_REFILL_20P");
                    this.OnCASH_REFILL_20PChanged();
                }
            }
        }

        [Column(Storage = "_CASH_REFILL_50P", DbType = "Int")]
        public System.Nullable<int> CASH_REFILL_50P
        {
            get
            {
                return this._CASH_REFILL_50P;
            }
            set
            {
                if ((this._CASH_REFILL_50P != value))
                {
                    this.OnCASH_REFILL_50PChanging(value);
                    this.SendPropertyChanging();
                    this._CASH_REFILL_50P = value;
                    this.SendPropertyChanged("CASH_REFILL_50P");
                    this.OnCASH_REFILL_50PChanged();
                }
            }
        }

        [Column(Storage = "_CASH_REFILL_100P", DbType = "Int")]
        public System.Nullable<int> CASH_REFILL_100P
        {
            get
            {
                return this._CASH_REFILL_100P;
            }
            set
            {
                if ((this._CASH_REFILL_100P != value))
                {
                    this.OnCASH_REFILL_100PChanging(value);
                    this.SendPropertyChanging();
                    this._CASH_REFILL_100P = value;
                    this.SendPropertyChanged("CASH_REFILL_100P");
                    this.OnCASH_REFILL_100PChanged();
                }
            }
        }

        [Column(Storage = "_CASH_REFILL_200P", DbType = "Int")]
        public System.Nullable<int> CASH_REFILL_200P
        {
            get
            {
                return this._CASH_REFILL_200P;
            }
            set
            {
                if ((this._CASH_REFILL_200P != value))
                {
                    this.OnCASH_REFILL_200PChanging(value);
                    this.SendPropertyChanging();
                    this._CASH_REFILL_200P = value;
                    this.SendPropertyChanged("CASH_REFILL_200P");
                    this.OnCASH_REFILL_200PChanged();
                }
            }
        }

        [Column(Storage = "_CASH_REFILL_500P", DbType = "Int")]
        public System.Nullable<int> CASH_REFILL_500P
        {
            get
            {
                return this._CASH_REFILL_500P;
            }
            set
            {
                if ((this._CASH_REFILL_500P != value))
                {
                    this.OnCASH_REFILL_500PChanging(value);
                    this.SendPropertyChanging();
                    this._CASH_REFILL_500P = value;
                    this.SendPropertyChanged("CASH_REFILL_500P");
                    this.OnCASH_REFILL_500PChanged();
                }
            }
        }

        [Column(Storage = "_CASH_REFILL_1000P", DbType = "Int")]
        public System.Nullable<int> CASH_REFILL_1000P
        {
            get
            {
                return this._CASH_REFILL_1000P;
            }
            set
            {
                if ((this._CASH_REFILL_1000P != value))
                {
                    this.OnCASH_REFILL_1000PChanging(value);
                    this.SendPropertyChanging();
                    this._CASH_REFILL_1000P = value;
                    this.SendPropertyChanged("CASH_REFILL_1000P");
                    this.OnCASH_REFILL_1000PChanged();
                }
            }
        }

        [Column(Storage = "_CASH_REFILL_2000P", DbType = "Int")]
        public System.Nullable<int> CASH_REFILL_2000P
        {
            get
            {
                return this._CASH_REFILL_2000P;
            }
            set
            {
                if ((this._CASH_REFILL_2000P != value))
                {
                    this.OnCASH_REFILL_2000PChanging(value);
                    this.SendPropertyChanging();
                    this._CASH_REFILL_2000P = value;
                    this.SendPropertyChanged("CASH_REFILL_2000P");
                    this.OnCASH_REFILL_2000PChanged();
                }
            }
        }

        [Column(Storage = "_CASH_REFILL_5000P", DbType = "Int")]
        public System.Nullable<int> CASH_REFILL_5000P
        {
            get
            {
                return this._CASH_REFILL_5000P;
            }
            set
            {
                if ((this._CASH_REFILL_5000P != value))
                {
                    this.OnCASH_REFILL_5000PChanging(value);
                    this.SendPropertyChanging();
                    this._CASH_REFILL_5000P = value;
                    this.SendPropertyChanged("CASH_REFILL_5000P");
                    this.OnCASH_REFILL_5000PChanged();
                }
            }
        }

        [Column(Storage = "_CASH_REFILL_10000P", DbType = "Int")]
        public System.Nullable<int> CASH_REFILL_10000P
        {
            get
            {
                return this._CASH_REFILL_10000P;
            }
            set
            {
                if ((this._CASH_REFILL_10000P != value))
                {
                    this.OnCASH_REFILL_10000PChanging(value);
                    this.SendPropertyChanging();
                    this._CASH_REFILL_10000P = value;
                    this.SendPropertyChanged("CASH_REFILL_10000P");
                    this.OnCASH_REFILL_10000PChanged();
                }
            }
        }

        [Column(Storage = "_CASH_REFILL_20000P", DbType = "Int")]
        public System.Nullable<int> CASH_REFILL_20000P
        {
            get
            {
                return this._CASH_REFILL_20000P;
            }
            set
            {
                if ((this._CASH_REFILL_20000P != value))
                {
                    this.OnCASH_REFILL_20000PChanging(value);
                    this.SendPropertyChanging();
                    this._CASH_REFILL_20000P = value;
                    this.SendPropertyChanged("CASH_REFILL_20000P");
                    this.OnCASH_REFILL_20000PChanged();
                }
            }
        }

        [Column(Storage = "_CASH_REFILL_50000P", DbType = "Int")]
        public System.Nullable<int> CASH_REFILL_50000P
        {
            get
            {
                return this._CASH_REFILL_50000P;
            }
            set
            {
                if ((this._CASH_REFILL_50000P != value))
                {
                    this.OnCASH_REFILL_50000PChanging(value);
                    this.SendPropertyChanging();
                    this._CASH_REFILL_50000P = value;
                    this.SendPropertyChanged("CASH_REFILL_50000P");
                    this.OnCASH_REFILL_50000PChanged();
                }
            }
        }

        [Column(Storage = "_CASH_REFILL_100000P", DbType = "Int")]
        public System.Nullable<int> CASH_REFILL_100000P
        {
            get
            {
                return this._CASH_REFILL_100000P;
            }
            set
            {
                if ((this._CASH_REFILL_100000P != value))
                {
                    this.OnCASH_REFILL_100000PChanging(value);
                    this.SendPropertyChanging();
                    this._CASH_REFILL_100000P = value;
                    this.SendPropertyChanged("CASH_REFILL_100000P");
                    this.OnCASH_REFILL_100000PChanged();
                }
            }
        }

        [Column(Storage = "_TOKEN_REFILL_5P", DbType = "Int")]
        public System.Nullable<int> TOKEN_REFILL_5P
        {
            get
            {
                return this._TOKEN_REFILL_5P;
            }
            set
            {
                if ((this._TOKEN_REFILL_5P != value))
                {
                    this.OnTOKEN_REFILL_5PChanging(value);
                    this.SendPropertyChanging();
                    this._TOKEN_REFILL_5P = value;
                    this.SendPropertyChanged("TOKEN_REFILL_5P");
                    this.OnTOKEN_REFILL_5PChanged();
                }
            }
        }

        [Column(Storage = "_TOKEN_REFILL_10P", DbType = "Int")]
        public System.Nullable<int> TOKEN_REFILL_10P
        {
            get
            {
                return this._TOKEN_REFILL_10P;
            }
            set
            {
                if ((this._TOKEN_REFILL_10P != value))
                {
                    this.OnTOKEN_REFILL_10PChanging(value);
                    this.SendPropertyChanging();
                    this._TOKEN_REFILL_10P = value;
                    this.SendPropertyChanged("TOKEN_REFILL_10P");
                    this.OnTOKEN_REFILL_10PChanged();
                }
            }
        }

        [Column(Storage = "_TOKEN_REFILL_20P", DbType = "Int")]
        public System.Nullable<int> TOKEN_REFILL_20P
        {
            get
            {
                return this._TOKEN_REFILL_20P;
            }
            set
            {
                if ((this._TOKEN_REFILL_20P != value))
                {
                    this.OnTOKEN_REFILL_20PChanging(value);
                    this.SendPropertyChanging();
                    this._TOKEN_REFILL_20P = value;
                    this.SendPropertyChanged("TOKEN_REFILL_20P");
                    this.OnTOKEN_REFILL_20PChanged();
                }
            }
        }

        [Column(Storage = "_TOKEN_REFILL_50P", DbType = "Int")]
        public System.Nullable<int> TOKEN_REFILL_50P
        {
            get
            {
                return this._TOKEN_REFILL_50P;
            }
            set
            {
                if ((this._TOKEN_REFILL_50P != value))
                {
                    this.OnTOKEN_REFILL_50PChanging(value);
                    this.SendPropertyChanging();
                    this._TOKEN_REFILL_50P = value;
                    this.SendPropertyChanged("TOKEN_REFILL_50P");
                    this.OnTOKEN_REFILL_50PChanged();
                }
            }
        }

        [Column(Storage = "_TOKEN_REFILL_100P", DbType = "Int")]
        public System.Nullable<int> TOKEN_REFILL_100P
        {
            get
            {
                return this._TOKEN_REFILL_100P;
            }
            set
            {
                if ((this._TOKEN_REFILL_100P != value))
                {
                    this.OnTOKEN_REFILL_100PChanging(value);
                    this.SendPropertyChanging();
                    this._TOKEN_REFILL_100P = value;
                    this.SendPropertyChanged("TOKEN_REFILL_100P");
                    this.OnTOKEN_REFILL_100PChanged();
                }
            }
        }

        [Column(Storage = "_TOKEN_REFILL_200P", DbType = "Int")]
        public System.Nullable<int> TOKEN_REFILL_200P
        {
            get
            {
                return this._TOKEN_REFILL_200P;
            }
            set
            {
                if ((this._TOKEN_REFILL_200P != value))
                {
                    this.OnTOKEN_REFILL_200PChanging(value);
                    this.SendPropertyChanging();
                    this._TOKEN_REFILL_200P = value;
                    this.SendPropertyChanged("TOKEN_REFILL_200P");
                    this.OnTOKEN_REFILL_200PChanged();
                }
            }
        }

        [Column(Storage = "_TOKEN_REFILL_500P", DbType = "Int")]
        public System.Nullable<int> TOKEN_REFILL_500P
        {
            get
            {
                return this._TOKEN_REFILL_500P;
            }
            set
            {
                if ((this._TOKEN_REFILL_500P != value))
                {
                    this.OnTOKEN_REFILL_500PChanging(value);
                    this.SendPropertyChanging();
                    this._TOKEN_REFILL_500P = value;
                    this.SendPropertyChanged("TOKEN_REFILL_500P");
                    this.OnTOKEN_REFILL_500PChanged();
                }
            }
        }

        [Column(Storage = "_TOKEN_REFILL_1000P", DbType = "Int")]
        public System.Nullable<int> TOKEN_REFILL_1000P
        {
            get
            {
                return this._TOKEN_REFILL_1000P;
            }
            set
            {
                if ((this._TOKEN_REFILL_1000P != value))
                {
                    this.OnTOKEN_REFILL_1000PChanging(value);
                    this.SendPropertyChanging();
                    this._TOKEN_REFILL_1000P = value;
                    this.SendPropertyChanged("TOKEN_REFILL_1000P");
                    this.OnTOKEN_REFILL_1000PChanged();
                }
            }
        }

        [Column(Storage = "_Declaration", DbType = "Bit")]
        public System.Nullable<bool> Declaration
        {
            get
            {
                return this._Declaration;
            }
            set
            {
                if ((this._Declaration != value))
                {
                    this.OnDeclarationChanging(value);
                    this.SendPropertyChanging();
                    this._Declaration = value;
                    this.SendPropertyChanged("Declaration");
                    this.OnDeclarationChanged();
                }
            }
        }

        [Column(Storage = "_Treasury_Total", DbType = "Real")]
        public System.Nullable<float> Treasury_Total
        {
            get
            {
                return this._Treasury_Total;
            }
            set
            {
                if ((this._Treasury_Total != value))
                {
                    this.OnTreasury_TotalChanging(value);
                    this.SendPropertyChanging();
                    this._Treasury_Total = value;
                    this.SendPropertyChanged("Treasury_Total");
                    this.OnTreasury_TotalChanged();
                }
            }
        }

        [Column(Storage = "_CounterCashIn", DbType = "Int")]
        public System.Nullable<int> CounterCashIn
        {
            get
            {
                return this._CounterCashIn;
            }
            set
            {
                if ((this._CounterCashIn != value))
                {
                    this.OnCounterCashInChanging(value);
                    this.SendPropertyChanging();
                    this._CounterCashIn = value;
                    this.SendPropertyChanged("CounterCashIn");
                    this.OnCounterCashInChanged();
                }
            }
        }

        [Column(Storage = "_CounterCashOut", DbType = "Int")]
        public System.Nullable<int> CounterCashOut
        {
            get
            {
                return this._CounterCashOut;
            }
            set
            {
                if ((this._CounterCashOut != value))
                {
                    this.OnCounterCashOutChanging(value);
                    this.SendPropertyChanging();
                    this._CounterCashOut = value;
                    this.SendPropertyChanged("CounterCashOut");
                    this.OnCounterCashOutChanged();
                }
            }
        }

        [Column(Storage = "_CounterTokensIn", DbType = "Int")]
        public System.Nullable<int> CounterTokensIn
        {
            get
            {
                return this._CounterTokensIn;
            }
            set
            {
                if ((this._CounterTokensIn != value))
                {
                    this.OnCounterTokensInChanging(value);
                    this.SendPropertyChanging();
                    this._CounterTokensIn = value;
                    this.SendPropertyChanged("CounterTokensIn");
                    this.OnCounterTokensInChanged();
                }
            }
        }

        [Column(Storage = "_CounterTokensOut", DbType = "Int")]
        public System.Nullable<int> CounterTokensOut
        {
            get
            {
                return this._CounterTokensOut;
            }
            set
            {
                if ((this._CounterTokensOut != value))
                {
                    this.OnCounterTokensOutChanging(value);
                    this.SendPropertyChanging();
                    this._CounterTokensOut = value;
                    this.SendPropertyChanged("CounterTokensOut");
                    this.OnCounterTokensOutChanged();
                }
            }
        }

        [Column(Storage = "_CounterPrize", DbType = "Int")]
        public System.Nullable<int> CounterPrize
        {
            get
            {
                return this._CounterPrize;
            }
            set
            {
                if ((this._CounterPrize != value))
                {
                    this.OnCounterPrizeChanging(value);
                    this.SendPropertyChanging();
                    this._CounterPrize = value;
                    this.SendPropertyChanged("CounterPrize");
                    this.OnCounterPrizeChanged();
                }
            }
        }

        [Column(Storage = "_CounterTournament", DbType = "Int")]
        public System.Nullable<int> CounterTournament
        {
            get
            {
                return this._CounterTournament;
            }
            set
            {
                if ((this._CounterTournament != value))
                {
                    this.OnCounterTournamentChanging(value);
                    this.SendPropertyChanging();
                    this._CounterTournament = value;
                    this.SendPropertyChanged("CounterTournament");
                    this.OnCounterTournamentChanged();
                }
            }
        }

        [Column(Storage = "_CounterJukebox", DbType = "Int")]
        public System.Nullable<int> CounterJukebox
        {
            get
            {
                return this._CounterJukebox;
            }
            set
            {
                if ((this._CounterJukebox != value))
                {
                    this.OnCounterJukeboxChanging(value);
                    this.SendPropertyChanging();
                    this._CounterJukebox = value;
                    this.SendPropertyChanged("CounterJukebox");
                    this.OnCounterJukeboxChanged();
                }
            }
        }

        [Column(Storage = "_CounterRefills", DbType = "Int")]
        public System.Nullable<int> CounterRefills
        {
            get
            {
                return this._CounterRefills;
            }
            set
            {
                if ((this._CounterRefills != value))
                {
                    this.OnCounterRefillsChanging(value);
                    this.SendPropertyChanging();
                    this._CounterRefills = value;
                    this.SendPropertyChanged("CounterRefills");
                    this.OnCounterRefillsChanged();
                }
            }
        }

        [Column(Storage = "_CashCollected", DbType = "Real")]
        public System.Nullable<float> CashCollected
        {
            get
            {
                return this._CashCollected;
            }
            set
            {
                if ((this._CashCollected != value))
                {
                    this.OnCashCollectedChanging(value);
                    this.SendPropertyChanging();
                    this._CashCollected = value;
                    this.SendPropertyChanged("CashCollected");
                    this.OnCashCollectedChanged();
                }
            }
        }

        [Column(Storage = "_TokensCollected", DbType = "Real")]
        public System.Nullable<float> TokensCollected
        {
            get
            {
                return this._TokensCollected;
            }
            set
            {
                if ((this._TokensCollected != value))
                {
                    this.OnTokensCollectedChanging(value);
                    this.SendPropertyChanging();
                    this._TokensCollected = value;
                    this.SendPropertyChanged("TokensCollected");
                    this.OnTokensCollectedChanged();
                }
            }
        }

        [Column(Storage = "_Cash_Collected_1p", DbType = "Real")]
        public System.Nullable<float> Cash_Collected_1p
        {
            get
            {
                return this._Cash_Collected_1p;
            }
            set
            {
                if ((this._Cash_Collected_1p != value))
                {
                    this.OnCash_Collected_1pChanging(value);
                    this.SendPropertyChanging();
                    this._Cash_Collected_1p = value;
                    this.SendPropertyChanged("Cash_Collected_1p");
                    this.OnCash_Collected_1pChanged();
                }
            }
        }

        [Column(Storage = "_Cash_Collected_2p", DbType = "Real")]
        public System.Nullable<float> Cash_Collected_2p
        {
            get
            {
                return this._Cash_Collected_2p;
            }
            set
            {
                if ((this._Cash_Collected_2p != value))
                {
                    this.OnCash_Collected_2pChanging(value);
                    this.SendPropertyChanging();
                    this._Cash_Collected_2p = value;
                    this.SendPropertyChanged("Cash_Collected_2p");
                    this.OnCash_Collected_2pChanged();
                }
            }
        }

        [Column(Storage = "_Cash_Collected_5p", DbType = "Real")]
        public System.Nullable<float> Cash_Collected_5p
        {
            get
            {
                return this._Cash_Collected_5p;
            }
            set
            {
                if ((this._Cash_Collected_5p != value))
                {
                    this.OnCash_Collected_5pChanging(value);
                    this.SendPropertyChanging();
                    this._Cash_Collected_5p = value;
                    this.SendPropertyChanged("Cash_Collected_5p");
                    this.OnCash_Collected_5pChanged();
                }
            }
        }

        [Column(Storage = "_Cash_Collected_10p", DbType = "Real")]
        public System.Nullable<float> Cash_Collected_10p
        {
            get
            {
                return this._Cash_Collected_10p;
            }
            set
            {
                if ((this._Cash_Collected_10p != value))
                {
                    this.OnCash_Collected_10pChanging(value);
                    this.SendPropertyChanging();
                    this._Cash_Collected_10p = value;
                    this.SendPropertyChanged("Cash_Collected_10p");
                    this.OnCash_Collected_10pChanged();
                }
            }
        }

        [Column(Storage = "_Cash_Collected_20p", DbType = "Real")]
        public System.Nullable<float> Cash_Collected_20p
        {
            get
            {
                return this._Cash_Collected_20p;
            }
            set
            {
                if ((this._Cash_Collected_20p != value))
                {
                    this.OnCash_Collected_20pChanging(value);
                    this.SendPropertyChanging();
                    this._Cash_Collected_20p = value;
                    this.SendPropertyChanged("Cash_Collected_20p");
                    this.OnCash_Collected_20pChanged();
                }
            }
        }

        [Column(Storage = "_Cash_Collected_50p", DbType = "Real")]
        public System.Nullable<float> Cash_Collected_50p
        {
            get
            {
                return this._Cash_Collected_50p;
            }
            set
            {
                if ((this._Cash_Collected_50p != value))
                {
                    this.OnCash_Collected_50pChanging(value);
                    this.SendPropertyChanging();
                    this._Cash_Collected_50p = value;
                    this.SendPropertyChanged("Cash_Collected_50p");
                    this.OnCash_Collected_50pChanged();
                }
            }
        }

        [Column(Storage = "_Cash_Collected_100p", DbType = "Real")]
        public System.Nullable<float> Cash_Collected_100p
        {
            get
            {
                return this._Cash_Collected_100p;
            }
            set
            {
                if ((this._Cash_Collected_100p != value))
                {
                    this.OnCash_Collected_100pChanging(value);
                    this.SendPropertyChanging();
                    this._Cash_Collected_100p = value;
                    this.SendPropertyChanged("Cash_Collected_100p");
                    this.OnCash_Collected_100pChanged();
                }
            }
        }

        [Column(Storage = "_Cash_Collected_200p", DbType = "Real")]
        public System.Nullable<float> Cash_Collected_200p
        {
            get
            {
                return this._Cash_Collected_200p;
            }
            set
            {
                if ((this._Cash_Collected_200p != value))
                {
                    this.OnCash_Collected_200pChanging(value);
                    this.SendPropertyChanging();
                    this._Cash_Collected_200p = value;
                    this.SendPropertyChanged("Cash_Collected_200p");
                    this.OnCash_Collected_200pChanged();
                }
            }
        }

        [Column(Storage = "_Cash_Collected_500p", DbType = "Real")]
        public System.Nullable<float> Cash_Collected_500p
        {
            get
            {
                return this._Cash_Collected_500p;
            }
            set
            {
                if ((this._Cash_Collected_500p != value))
                {
                    this.OnCash_Collected_500pChanging(value);
                    this.SendPropertyChanging();
                    this._Cash_Collected_500p = value;
                    this.SendPropertyChanged("Cash_Collected_500p");
                    this.OnCash_Collected_500pChanged();
                }
            }
        }

        [Column(Storage = "_Cash_Collected_1000p", DbType = "Real")]
        public System.Nullable<float> Cash_Collected_1000p
        {
            get
            {
                return this._Cash_Collected_1000p;
            }
            set
            {
                if ((this._Cash_Collected_1000p != value))
                {
                    this.OnCash_Collected_1000pChanging(value);
                    this.SendPropertyChanging();
                    this._Cash_Collected_1000p = value;
                    this.SendPropertyChanged("Cash_Collected_1000p");
                    this.OnCash_Collected_1000pChanged();
                }
            }
        }

        [Column(Storage = "_Cash_Collected_2000p", DbType = "Real")]
        public System.Nullable<float> Cash_Collected_2000p
        {
            get
            {
                return this._Cash_Collected_2000p;
            }
            set
            {
                if ((this._Cash_Collected_2000p != value))
                {
                    this.OnCash_Collected_2000pChanging(value);
                    this.SendPropertyChanging();
                    this._Cash_Collected_2000p = value;
                    this.SendPropertyChanged("Cash_Collected_2000p");
                    this.OnCash_Collected_2000pChanged();
                }
            }
        }

        [Column(Storage = "_Cash_Collected_5000p", DbType = "Real")]
        public System.Nullable<float> Cash_Collected_5000p
        {
            get
            {
                return this._Cash_Collected_5000p;
            }
            set
            {
                if ((this._Cash_Collected_5000p != value))
                {
                    this.OnCash_Collected_5000pChanging(value);
                    this.SendPropertyChanging();
                    this._Cash_Collected_5000p = value;
                    this.SendPropertyChanged("Cash_Collected_5000p");
                    this.OnCash_Collected_5000pChanged();
                }
            }
        }

        [Column(Storage = "_Cash_Collected_10000p", DbType = "Real")]
        public System.Nullable<float> Cash_Collected_10000p
        {
            get
            {
                return this._Cash_Collected_10000p;
            }
            set
            {
                if ((this._Cash_Collected_10000p != value))
                {
                    this.OnCash_Collected_10000pChanging(value);
                    this.SendPropertyChanging();
                    this._Cash_Collected_10000p = value;
                    this.SendPropertyChanged("Cash_Collected_10000p");
                    this.OnCash_Collected_10000pChanged();
                }
            }
        }

        [Column(Storage = "_Cash_Collected_20000p", DbType = "Real")]
        public System.Nullable<float> Cash_Collected_20000p
        {
            get
            {
                return this._Cash_Collected_20000p;
            }
            set
            {
                if ((this._Cash_Collected_20000p != value))
                {
                    this.OnCash_Collected_20000pChanging(value);
                    this.SendPropertyChanging();
                    this._Cash_Collected_20000p = value;
                    this.SendPropertyChanged("Cash_Collected_20000p");
                    this.OnCash_Collected_20000pChanged();
                }
            }
        }

        [Column(Storage = "_Cash_Collected_50000p", DbType = "Real")]
        public System.Nullable<float> Cash_Collected_50000p
        {
            get
            {
                return this._Cash_Collected_50000p;
            }
            set
            {
                if ((this._Cash_Collected_50000p != value))
                {
                    this.OnCash_Collected_50000pChanging(value);
                    this.SendPropertyChanging();
                    this._Cash_Collected_50000p = value;
                    this.SendPropertyChanged("Cash_Collected_50000p");
                    this.OnCash_Collected_50000pChanged();
                }
            }
        }

        [Column(Storage = "_Cash_Collected_100000p", DbType = "Real")]
        public System.Nullable<float> Cash_Collected_100000p
        {
            get
            {
                return this._Cash_Collected_100000p;
            }
            set
            {
                if ((this._Cash_Collected_100000p != value))
                {
                    this.OnCash_Collected_100000pChanging(value);
                    this.SendPropertyChanging();
                    this._Cash_Collected_100000p = value;
                    this.SendPropertyChanged("Cash_Collected_100000p");
                    this.OnCash_Collected_100000pChanged();
                }
            }
        }

        [Column(Storage = "_CashRefills", DbType = "Real")]
        public System.Nullable<float> CashRefills
        {
            get
            {
                return this._CashRefills;
            }
            set
            {
                if ((this._CashRefills != value))
                {
                    this.OnCashRefillsChanging(value);
                    this.SendPropertyChanging();
                    this._CashRefills = value;
                    this.SendPropertyChanged("CashRefills");
                    this.OnCashRefillsChanged();
                }
            }
        }

        [Column(Storage = "_TokenRefills", DbType = "Real")]
        public System.Nullable<float> TokenRefills
        {
            get
            {
                return this._TokenRefills;
            }
            set
            {
                if ((this._TokenRefills != value))
                {
                    this.OnTokenRefillsChanging(value);
                    this.SendPropertyChanging();
                    this._TokenRefills = value;
                    this.SendPropertyChanged("TokenRefills");
                    this.OnTokenRefillsChanged();
                }
            }
        }

        [Column(Storage = "_Cash_Refills_2p", DbType = "Real")]
        public System.Nullable<float> Cash_Refills_2p
        {
            get
            {
                return this._Cash_Refills_2p;
            }
            set
            {
                if ((this._Cash_Refills_2p != value))
                {
                    this.OnCash_Refills_2pChanging(value);
                    this.SendPropertyChanging();
                    this._Cash_Refills_2p = value;
                    this.SendPropertyChanged("Cash_Refills_2p");
                    this.OnCash_Refills_2pChanged();
                }
            }
        }

        [Column(Storage = "_Cash_Refills_5p", DbType = "Real")]
        public System.Nullable<float> Cash_Refills_5p
        {
            get
            {
                return this._Cash_Refills_5p;
            }
            set
            {
                if ((this._Cash_Refills_5p != value))
                {
                    this.OnCash_Refills_5pChanging(value);
                    this.SendPropertyChanging();
                    this._Cash_Refills_5p = value;
                    this.SendPropertyChanged("Cash_Refills_5p");
                    this.OnCash_Refills_5pChanged();
                }
            }
        }

        [Column(Storage = "_Cash_Refills_10p", DbType = "Real")]
        public System.Nullable<float> Cash_Refills_10p
        {
            get
            {
                return this._Cash_Refills_10p;
            }
            set
            {
                if ((this._Cash_Refills_10p != value))
                {
                    this.OnCash_Refills_10pChanging(value);
                    this.SendPropertyChanging();
                    this._Cash_Refills_10p = value;
                    this.SendPropertyChanged("Cash_Refills_10p");
                    this.OnCash_Refills_10pChanged();
                }
            }
        }

        [Column(Storage = "_Cash_Refills_20p", DbType = "Real")]
        public System.Nullable<float> Cash_Refills_20p
        {
            get
            {
                return this._Cash_Refills_20p;
            }
            set
            {
                if ((this._Cash_Refills_20p != value))
                {
                    this.OnCash_Refills_20pChanging(value);
                    this.SendPropertyChanging();
                    this._Cash_Refills_20p = value;
                    this.SendPropertyChanged("Cash_Refills_20p");
                    this.OnCash_Refills_20pChanged();
                }
            }
        }

        [Column(Storage = "_Cash_Refills_50p", DbType = "Real")]
        public System.Nullable<float> Cash_Refills_50p
        {
            get
            {
                return this._Cash_Refills_50p;
            }
            set
            {
                if ((this._Cash_Refills_50p != value))
                {
                    this.OnCash_Refills_50pChanging(value);
                    this.SendPropertyChanging();
                    this._Cash_Refills_50p = value;
                    this.SendPropertyChanged("Cash_Refills_50p");
                    this.OnCash_Refills_50pChanged();
                }
            }
        }

        [Column(Storage = "_Cash_Refills_100p", DbType = "Real")]
        public System.Nullable<float> Cash_Refills_100p
        {
            get
            {
                return this._Cash_Refills_100p;
            }
            set
            {
                if ((this._Cash_Refills_100p != value))
                {
                    this.OnCash_Refills_100pChanging(value);
                    this.SendPropertyChanging();
                    this._Cash_Refills_100p = value;
                    this.SendPropertyChanged("Cash_Refills_100p");
                    this.OnCash_Refills_100pChanged();
                }
            }
        }

        [Column(Storage = "_Cash_Refills_200p", DbType = "Real")]
        public System.Nullable<float> Cash_Refills_200p
        {
            get
            {
                return this._Cash_Refills_200p;
            }
            set
            {
                if ((this._Cash_Refills_200p != value))
                {
                    this.OnCash_Refills_200pChanging(value);
                    this.SendPropertyChanging();
                    this._Cash_Refills_200p = value;
                    this.SendPropertyChanged("Cash_Refills_200p");
                    this.OnCash_Refills_200pChanged();
                }
            }
        }

        [Column(Storage = "_Cash_Refills_500p", DbType = "Real")]
        public System.Nullable<float> Cash_Refills_500p
        {
            get
            {
                return this._Cash_Refills_500p;
            }
            set
            {
                if ((this._Cash_Refills_500p != value))
                {
                    this.OnCash_Refills_500pChanging(value);
                    this.SendPropertyChanging();
                    this._Cash_Refills_500p = value;
                    this.SendPropertyChanged("Cash_Refills_500p");
                    this.OnCash_Refills_500pChanged();
                }
            }
        }

        [Column(Storage = "_Cash_Refills_1000p", DbType = "Real")]
        public System.Nullable<float> Cash_Refills_1000p
        {
            get
            {
                return this._Cash_Refills_1000p;
            }
            set
            {
                if ((this._Cash_Refills_1000p != value))
                {
                    this.OnCash_Refills_1000pChanging(value);
                    this.SendPropertyChanging();
                    this._Cash_Refills_1000p = value;
                    this.SendPropertyChanged("Cash_Refills_1000p");
                    this.OnCash_Refills_1000pChanged();
                }
            }
        }

        [Column(Storage = "_Cash_Refills_2000p", DbType = "Real")]
        public System.Nullable<float> Cash_Refills_2000p
        {
            get
            {
                return this._Cash_Refills_2000p;
            }
            set
            {
                if ((this._Cash_Refills_2000p != value))
                {
                    this.OnCash_Refills_2000pChanging(value);
                    this.SendPropertyChanging();
                    this._Cash_Refills_2000p = value;
                    this.SendPropertyChanged("Cash_Refills_2000p");
                    this.OnCash_Refills_2000pChanged();
                }
            }
        }

        [Column(Storage = "_Cash_Refills_5000p", DbType = "Real")]
        public System.Nullable<float> Cash_Refills_5000p
        {
            get
            {
                return this._Cash_Refills_5000p;
            }
            set
            {
                if ((this._Cash_Refills_5000p != value))
                {
                    this.OnCash_Refills_5000pChanging(value);
                    this.SendPropertyChanging();
                    this._Cash_Refills_5000p = value;
                    this.SendPropertyChanged("Cash_Refills_5000p");
                    this.OnCash_Refills_5000pChanged();
                }
            }
        }

        [Column(Storage = "_Cash_Refills_10000p", DbType = "Real")]
        public System.Nullable<float> Cash_Refills_10000p
        {
            get
            {
                return this._Cash_Refills_10000p;
            }
            set
            {
                if ((this._Cash_Refills_10000p != value))
                {
                    this.OnCash_Refills_10000pChanging(value);
                    this.SendPropertyChanging();
                    this._Cash_Refills_10000p = value;
                    this.SendPropertyChanged("Cash_Refills_10000p");
                    this.OnCash_Refills_10000pChanged();
                }
            }
        }

        [Column(Storage = "_Cash_Refills_20000p", DbType = "Real")]
        public System.Nullable<float> Cash_Refills_20000p
        {
            get
            {
                return this._Cash_Refills_20000p;
            }
            set
            {
                if ((this._Cash_Refills_20000p != value))
                {
                    this.OnCash_Refills_20000pChanging(value);
                    this.SendPropertyChanging();
                    this._Cash_Refills_20000p = value;
                    this.SendPropertyChanged("Cash_Refills_20000p");
                    this.OnCash_Refills_20000pChanged();
                }
            }
        }

        [Column(Storage = "_Cash_Refills_50000p", DbType = "Real")]
        public System.Nullable<float> Cash_Refills_50000p
        {
            get
            {
                return this._Cash_Refills_50000p;
            }
            set
            {
                if ((this._Cash_Refills_50000p != value))
                {
                    this.OnCash_Refills_50000pChanging(value);
                    this.SendPropertyChanging();
                    this._Cash_Refills_50000p = value;
                    this.SendPropertyChanged("Cash_Refills_50000p");
                    this.OnCash_Refills_50000pChanged();
                }
            }
        }

        [Column(Storage = "_Cash_Refills_100000p", DbType = "Real")]
        public System.Nullable<float> Cash_Refills_100000p
        {
            get
            {
                return this._Cash_Refills_100000p;
            }
            set
            {
                if ((this._Cash_Refills_100000p != value))
                {
                    this.OnCash_Refills_100000pChanging(value);
                    this.SendPropertyChanging();
                    this._Cash_Refills_100000p = value;
                    this.SendPropertyChanged("Cash_Refills_100000p");
                    this.OnCash_Refills_100000pChanged();
                }
            }
        }

        [Column(Storage = "_CounterCashInElectronic", DbType = "Int")]
        public System.Nullable<int> CounterCashInElectronic
        {
            get
            {
                return this._CounterCashInElectronic;
            }
            set
            {
                if ((this._CounterCashInElectronic != value))
                {
                    this.OnCounterCashInElectronicChanging(value);
                    this.SendPropertyChanging();
                    this._CounterCashInElectronic = value;
                    this.SendPropertyChanged("CounterCashInElectronic");
                    this.OnCounterCashInElectronicChanged();
                }
            }
        }

        [Column(Storage = "_CounterCashOutElectronic", DbType = "Int")]
        public System.Nullable<int> CounterCashOutElectronic
        {
            get
            {
                return this._CounterCashOutElectronic;
            }
            set
            {
                if ((this._CounterCashOutElectronic != value))
                {
                    this.OnCounterCashOutElectronicChanging(value);
                    this.SendPropertyChanging();
                    this._CounterCashOutElectronic = value;
                    this.SendPropertyChanged("CounterCashOutElectronic");
                    this.OnCounterCashOutElectronicChanged();
                }
            }
        }

        [Column(Storage = "_CounterTokensInElectronic", DbType = "Int")]
        public System.Nullable<int> CounterTokensInElectronic
        {
            get
            {
                return this._CounterTokensInElectronic;
            }
            set
            {
                if ((this._CounterTokensInElectronic != value))
                {
                    this.OnCounterTokensInElectronicChanging(value);
                    this.SendPropertyChanging();
                    this._CounterTokensInElectronic = value;
                    this.SendPropertyChanged("CounterTokensInElectronic");
                    this.OnCounterTokensInElectronicChanged();
                }
            }
        }

        [Column(Storage = "_CounterTokensOutElectronic", DbType = "Int")]
        public System.Nullable<int> CounterTokensOutElectronic
        {
            get
            {
                return this._CounterTokensOutElectronic;
            }
            set
            {
                if ((this._CounterTokensOutElectronic != value))
                {
                    this.OnCounterTokensOutElectronicChanging(value);
                    this.SendPropertyChanging();
                    this._CounterTokensOutElectronic = value;
                    this.SendPropertyChanged("CounterTokensOutElectronic");
                    this.OnCounterTokensOutElectronicChanged();
                }
            }
        }

        [Column(Storage = "_JackpotsOut", DbType = "Int")]
        public System.Nullable<int> JackpotsOut
        {
            get
            {
                return this._JackpotsOut;
            }
            set
            {
                if ((this._JackpotsOut != value))
                {
                    this.OnJackpotsOutChanging(value);
                    this.SendPropertyChanging();
                    this._JackpotsOut = value;
                    this.SendPropertyChanged("JackpotsOut");
                    this.OnJackpotsOutChanged();
                }
            }
        }

        [Column(Storage = "_PreviousCounterCashIn", DbType = "Int")]
        public System.Nullable<int> PreviousCounterCashIn
        {
            get
            {
                return this._PreviousCounterCashIn;
            }
            set
            {
                if ((this._PreviousCounterCashIn != value))
                {
                    this.OnPreviousCounterCashInChanging(value);
                    this.SendPropertyChanging();
                    this._PreviousCounterCashIn = value;
                    this.SendPropertyChanged("PreviousCounterCashIn");
                    this.OnPreviousCounterCashInChanged();
                }
            }
        }

        [Column(Storage = "_PreviousCounterCashOut", DbType = "Int")]
        public System.Nullable<int> PreviousCounterCashOut
        {
            get
            {
                return this._PreviousCounterCashOut;
            }
            set
            {
                if ((this._PreviousCounterCashOut != value))
                {
                    this.OnPreviousCounterCashOutChanging(value);
                    this.SendPropertyChanging();
                    this._PreviousCounterCashOut = value;
                    this.SendPropertyChanged("PreviousCounterCashOut");
                    this.OnPreviousCounterCashOutChanged();
                }
            }
        }

        [Column(Storage = "_PreviousCounterTokensIn", DbType = "Int")]
        public System.Nullable<int> PreviousCounterTokensIn
        {
            get
            {
                return this._PreviousCounterTokensIn;
            }
            set
            {
                if ((this._PreviousCounterTokensIn != value))
                {
                    this.OnPreviousCounterTokensInChanging(value);
                    this.SendPropertyChanging();
                    this._PreviousCounterTokensIn = value;
                    this.SendPropertyChanged("PreviousCounterTokensIn");
                    this.OnPreviousCounterTokensInChanged();
                }
            }
        }

        [Column(Storage = "_PreviousCounterTokensOut", DbType = "Int")]
        public System.Nullable<int> PreviousCounterTokensOut
        {
            get
            {
                return this._PreviousCounterTokensOut;
            }
            set
            {
                if ((this._PreviousCounterTokensOut != value))
                {
                    this.OnPreviousCounterTokensOutChanging(value);
                    this.SendPropertyChanging();
                    this._PreviousCounterTokensOut = value;
                    this.SendPropertyChanged("PreviousCounterTokensOut");
                    this.OnPreviousCounterTokensOutChanged();
                }
            }
        }

        [Column(Storage = "_PreviousCounterPrize", DbType = "Int")]
        public System.Nullable<int> PreviousCounterPrize
        {
            get
            {
                return this._PreviousCounterPrize;
            }
            set
            {
                if ((this._PreviousCounterPrize != value))
                {
                    this.OnPreviousCounterPrizeChanging(value);
                    this.SendPropertyChanging();
                    this._PreviousCounterPrize = value;
                    this.SendPropertyChanged("PreviousCounterPrize");
                    this.OnPreviousCounterPrizeChanged();
                }
            }
        }

        [Column(Storage = "_PreviousCounterJackpotsOut", DbType = "Int")]
        public System.Nullable<int> PreviousCounterJackpotsOut
        {
            get
            {
                return this._PreviousCounterJackpotsOut;
            }
            set
            {
                if ((this._PreviousCounterJackpotsOut != value))
                {
                    this.OnPreviousCounterJackpotsOutChanging(value);
                    this.SendPropertyChanging();
                    this._PreviousCounterJackpotsOut = value;
                    this.SendPropertyChanged("PreviousCounterJackpotsOut");
                    this.OnPreviousCounterJackpotsOutChanged();
                }
            }
        }

        [Column(Storage = "_PreviousCounterTournament", DbType = "Int")]
        public System.Nullable<int> PreviousCounterTournament
        {
            get
            {
                return this._PreviousCounterTournament;
            }
            set
            {
                if ((this._PreviousCounterTournament != value))
                {
                    this.OnPreviousCounterTournamentChanging(value);
                    this.SendPropertyChanging();
                    this._PreviousCounterTournament = value;
                    this.SendPropertyChanged("PreviousCounterTournament");
                    this.OnPreviousCounterTournamentChanged();
                }
            }
        }

        [Column(Storage = "_PreviousCounterJukebox", DbType = "Int")]
        public System.Nullable<int> PreviousCounterJukebox
        {
            get
            {
                return this._PreviousCounterJukebox;
            }
            set
            {
                if ((this._PreviousCounterJukebox != value))
                {
                    this.OnPreviousCounterJukeboxChanging(value);
                    this.SendPropertyChanging();
                    this._PreviousCounterJukebox = value;
                    this.SendPropertyChanged("PreviousCounterJukebox");
                    this.OnPreviousCounterJukeboxChanged();
                }
            }
        }

        [Column(Storage = "_PreviousCounterRefills", DbType = "Int")]
        public System.Nullable<int> PreviousCounterRefills
        {
            get
            {
                return this._PreviousCounterRefills;
            }
            set
            {
                if ((this._PreviousCounterRefills != value))
                {
                    this.OnPreviousCounterRefillsChanging(value);
                    this.SendPropertyChanging();
                    this._PreviousCounterRefills = value;
                    this.SendPropertyChanged("PreviousCounterRefills");
                    this.OnPreviousCounterRefillsChanged();
                }
            }
        }

        [Column(Storage = "_PreviousCounterCashInElectronic", DbType = "Int")]
        public System.Nullable<int> PreviousCounterCashInElectronic
        {
            get
            {
                return this._PreviousCounterCashInElectronic;
            }
            set
            {
                if ((this._PreviousCounterCashInElectronic != value))
                {
                    this.OnPreviousCounterCashInElectronicChanging(value);
                    this.SendPropertyChanging();
                    this._PreviousCounterCashInElectronic = value;
                    this.SendPropertyChanged("PreviousCounterCashInElectronic");
                    this.OnPreviousCounterCashInElectronicChanged();
                }
            }
        }

        [Column(Storage = "_PreviousCounterCashOutElectronic", DbType = "Int")]
        public System.Nullable<int> PreviousCounterCashOutElectronic
        {
            get
            {
                return this._PreviousCounterCashOutElectronic;
            }
            set
            {
                if ((this._PreviousCounterCashOutElectronic != value))
                {
                    this.OnPreviousCounterCashOutElectronicChanging(value);
                    this.SendPropertyChanging();
                    this._PreviousCounterCashOutElectronic = value;
                    this.SendPropertyChanged("PreviousCounterCashOutElectronic");
                    this.OnPreviousCounterCashOutElectronicChanged();
                }
            }
        }

        [Column(Storage = "_PreviousCounterTokensInElectronic", DbType = "Int")]
        public System.Nullable<int> PreviousCounterTokensInElectronic
        {
            get
            {
                return this._PreviousCounterTokensInElectronic;
            }
            set
            {
                if ((this._PreviousCounterTokensInElectronic != value))
                {
                    this.OnPreviousCounterTokensInElectronicChanging(value);
                    this.SendPropertyChanging();
                    this._PreviousCounterTokensInElectronic = value;
                    this.SendPropertyChanged("PreviousCounterTokensInElectronic");
                    this.OnPreviousCounterTokensInElectronicChanged();
                }
            }
        }

        [Column(Storage = "_PreviousCounterTokensOutElectronic", DbType = "Int")]
        public System.Nullable<int> PreviousCounterTokensOutElectronic
        {
            get
            {
                return this._PreviousCounterTokensOutElectronic;
            }
            set
            {
                if ((this._PreviousCounterTokensOutElectronic != value))
                {
                    this.OnPreviousCounterTokensOutElectronicChanging(value);
                    this.SendPropertyChanging();
                    this._PreviousCounterTokensOutElectronic = value;
                    this.SendPropertyChanged("PreviousCounterTokensOutElectronic");
                    this.OnPreviousCounterTokensOutElectronicChanged();
                }
            }
        }

        [Column(Storage = "_PreviousCollectionDate", DbType = "DateTime")]
        public System.Nullable<System.DateTime> PreviousCollectionDate
        {
            get
            {
                return this._PreviousCollectionDate;
            }
            set
            {
                if ((this._PreviousCollectionDate != value))
                {
                    this.OnPreviousCollectionDateChanging(value);
                    this.SendPropertyChanging();
                    this._PreviousCollectionDate = value;
                    this.SendPropertyChanged("PreviousCollectionDate");
                    this.OnPreviousCollectionDateChanged();
                }
            }
        }

        [Column(Storage = "_PreviousCollectionNo", DbType = "Int")]
        public System.Nullable<int> PreviousCollectionNo
        {
            get
            {
                return this._PreviousCollectionNo;
            }
            set
            {
                if ((this._PreviousCollectionNo != value))
                {
                    this.OnPreviousCollectionNoChanging(value);
                    this.SendPropertyChanging();
                    this._PreviousCollectionNo = value;
                    this.SendPropertyChanged("PreviousCollectionNo");
                    this.OnPreviousCollectionNoChanged();
                }
            }
        }

        [Column(Storage = "_Treasury_Refills", DbType = "Real")]
        public System.Nullable<float> Treasury_Refills
        {
            get
            {
                return this._Treasury_Refills;
            }
            set
            {
                if ((this._Treasury_Refills != value))
                {
                    this.OnTreasury_RefillsChanging(value);
                    this.SendPropertyChanging();
                    this._Treasury_Refills = value;
                    this.SendPropertyChanged("Treasury_Refills");
                    this.OnTreasury_RefillsChanged();
                }
            }
        }

        [Column(Storage = "_Treasury_Repayments", DbType = "Real")]
        public System.Nullable<float> Treasury_Repayments
        {
            get
            {
                return this._Treasury_Repayments;
            }
            set
            {
                if ((this._Treasury_Repayments != value))
                {
                    this.OnTreasury_RepaymentsChanging(value);
                    this.SendPropertyChanging();
                    this._Treasury_Repayments = value;
                    this.SendPropertyChanged("Treasury_Repayments");
                    this.OnTreasury_RepaymentsChanged();
                }
            }
        }

        [Column(Storage = "_Treasury_Tokens", DbType = "Real")]
        public System.Nullable<float> Treasury_Tokens
        {
            get
            {
                return this._Treasury_Tokens;
            }
            set
            {
                if ((this._Treasury_Tokens != value))
                {
                    this.OnTreasury_TokensChanging(value);
                    this.SendPropertyChanging();
                    this._Treasury_Tokens = value;
                    this.SendPropertyChanged("Treasury_Tokens");
                    this.OnTreasury_TokensChanged();
                }
            }
        }

        [Column(Storage = "_ExpectedBaggedCash", DbType = "Real")]
        public System.Nullable<float> ExpectedBaggedCash
        {
            get
            {
                return this._ExpectedBaggedCash;
            }
            set
            {
                if ((this._ExpectedBaggedCash != value))
                {
                    this.OnExpectedBaggedCashChanging(value);
                    this.SendPropertyChanging();
                    this._ExpectedBaggedCash = value;
                    this.SendPropertyChanged("ExpectedBaggedCash");
                    this.OnExpectedBaggedCashChanged();
                }
            }
        }

        [Column(Storage = "_ActualBaggedCash", DbType = "Real")]
        public System.Nullable<float> ActualBaggedCash
        {
            get
            {
                return this._ActualBaggedCash;
            }
            set
            {
                if ((this._ActualBaggedCash != value))
                {
                    this.OnActualBaggedCashChanging(value);
                    this.SendPropertyChanging();
                    this._ActualBaggedCash = value;
                    this.SendPropertyChanged("ActualBaggedCash");
                    this.OnActualBaggedCashChanged();
                }
            }
        }

        [Column(Storage = "_Collection_Meters_Coins_In", DbType = "Int")]
        public System.Nullable<int> Collection_Meters_Coins_In
        {
            get
            {
                return this._Collection_Meters_Coins_In;
            }
            set
            {
                if ((this._Collection_Meters_Coins_In != value))
                {
                    this.OnCollection_Meters_Coins_InChanging(value);
                    this.SendPropertyChanging();
                    this._Collection_Meters_Coins_In = value;
                    this.SendPropertyChanged("Collection_Meters_Coins_In");
                    this.OnCollection_Meters_Coins_InChanged();
                }
            }
        }

        [Column(Storage = "_Collection_Meters_Coins_Out", DbType = "Int")]
        public System.Nullable<int> Collection_Meters_Coins_Out
        {
            get
            {
                return this._Collection_Meters_Coins_Out;
            }
            set
            {
                if ((this._Collection_Meters_Coins_Out != value))
                {
                    this.OnCollection_Meters_Coins_OutChanging(value);
                    this.SendPropertyChanging();
                    this._Collection_Meters_Coins_Out = value;
                    this.SendPropertyChanged("Collection_Meters_Coins_Out");
                    this.OnCollection_Meters_Coins_OutChanged();
                }
            }
        }

        [Column(Storage = "_Collection_Meters_Coin_Drop", DbType = "Int")]
        public System.Nullable<int> Collection_Meters_Coin_Drop
        {
            get
            {
                return this._Collection_Meters_Coin_Drop;
            }
            set
            {
                if ((this._Collection_Meters_Coin_Drop != value))
                {
                    this.OnCollection_Meters_Coin_DropChanging(value);
                    this.SendPropertyChanging();
                    this._Collection_Meters_Coin_Drop = value;
                    this.SendPropertyChanged("Collection_Meters_Coin_Drop");
                    this.OnCollection_Meters_Coin_DropChanged();
                }
            }
        }

        [Column(Storage = "_Collection_Meters_Handpay", DbType = "Int")]
        public System.Nullable<int> Collection_Meters_Handpay
        {
            get
            {
                return this._Collection_Meters_Handpay;
            }
            set
            {
                if ((this._Collection_Meters_Handpay != value))
                {
                    this.OnCollection_Meters_HandpayChanging(value);
                    this.SendPropertyChanging();
                    this._Collection_Meters_Handpay = value;
                    this.SendPropertyChanged("Collection_Meters_Handpay");
                    this.OnCollection_Meters_HandpayChanged();
                }
            }
        }

        [Column(Storage = "_Collection_Meters_External_Credit", DbType = "Int")]
        public System.Nullable<int> Collection_Meters_External_Credit
        {
            get
            {
                return this._Collection_Meters_External_Credit;
            }
            set
            {
                if ((this._Collection_Meters_External_Credit != value))
                {
                    this.OnCollection_Meters_External_CreditChanging(value);
                    this.SendPropertyChanging();
                    this._Collection_Meters_External_Credit = value;
                    this.SendPropertyChanged("Collection_Meters_External_Credit");
                    this.OnCollection_Meters_External_CreditChanged();
                }
            }
        }

        [Column(Storage = "_Collection_Meters_Games_Bet", DbType = "Int")]
        public System.Nullable<int> Collection_Meters_Games_Bet
        {
            get
            {
                return this._Collection_Meters_Games_Bet;
            }
            set
            {
                if ((this._Collection_Meters_Games_Bet != value))
                {
                    this.OnCollection_Meters_Games_BetChanging(value);
                    this.SendPropertyChanging();
                    this._Collection_Meters_Games_Bet = value;
                    this.SendPropertyChanged("Collection_Meters_Games_Bet");
                    this.OnCollection_Meters_Games_BetChanged();
                }
            }
        }

        [Column(Storage = "_Collection_Meters_Games_Won", DbType = "Int")]
        public System.Nullable<int> Collection_Meters_Games_Won
        {
            get
            {
                return this._Collection_Meters_Games_Won;
            }
            set
            {
                if ((this._Collection_Meters_Games_Won != value))
                {
                    this.OnCollection_Meters_Games_WonChanging(value);
                    this.SendPropertyChanging();
                    this._Collection_Meters_Games_Won = value;
                    this.SendPropertyChanged("Collection_Meters_Games_Won");
                    this.OnCollection_Meters_Games_WonChanged();
                }
            }
        }

        [Column(Storage = "_Collection_Meters_Notes", DbType = "Int")]
        public System.Nullable<int> Collection_Meters_Notes
        {
            get
            {
                return this._Collection_Meters_Notes;
            }
            set
            {
                if ((this._Collection_Meters_Notes != value))
                {
                    this.OnCollection_Meters_NotesChanging(value);
                    this.SendPropertyChanging();
                    this._Collection_Meters_Notes = value;
                    this.SendPropertyChanged("Collection_Meters_Notes");
                    this.OnCollection_Meters_NotesChanged();
                }
            }
        }

        [Column(Storage = "_Collection_Treasury_Defloat", DbType = "Real")]
        public System.Nullable<float> Collection_Treasury_Defloat
        {
            get
            {
                return this._Collection_Treasury_Defloat;
            }
            set
            {
                if ((this._Collection_Treasury_Defloat != value))
                {
                    this.OnCollection_Treasury_DefloatChanging(value);
                    this.SendPropertyChanging();
                    this._Collection_Treasury_Defloat = value;
                    this.SendPropertyChanged("Collection_Treasury_Defloat");
                    this.OnCollection_Treasury_DefloatChanged();
                }
            }
        }

        [Column(Storage = "_Collection_Defloat_Collection", DbType = "Bit")]
        public System.Nullable<bool> Collection_Defloat_Collection
        {
            get
            {
                return this._Collection_Defloat_Collection;
            }
            set
            {
                if ((this._Collection_Defloat_Collection != value))
                {
                    this.OnCollection_Defloat_CollectionChanging(value);
                    this.SendPropertyChanging();
                    this._Collection_Defloat_Collection = value;
                    this.SendPropertyChanged("Collection_Defloat_Collection");
                    this.OnCollection_Defloat_CollectionChanged();
                }
            }
        }

        [Column(Storage = "_Previous_Meters_Coins_In", DbType = "Int")]
        public System.Nullable<int> Previous_Meters_Coins_In
        {
            get
            {
                return this._Previous_Meters_Coins_In;
            }
            set
            {
                if ((this._Previous_Meters_Coins_In != value))
                {
                    this.OnPrevious_Meters_Coins_InChanging(value);
                    this.SendPropertyChanging();
                    this._Previous_Meters_Coins_In = value;
                    this.SendPropertyChanged("Previous_Meters_Coins_In");
                    this.OnPrevious_Meters_Coins_InChanged();
                }
            }
        }

        [Column(Storage = "_Previous_Meters_Coins_Out", DbType = "Int")]
        public System.Nullable<int> Previous_Meters_Coins_Out
        {
            get
            {
                return this._Previous_Meters_Coins_Out;
            }
            set
            {
                if ((this._Previous_Meters_Coins_Out != value))
                {
                    this.OnPrevious_Meters_Coins_OutChanging(value);
                    this.SendPropertyChanging();
                    this._Previous_Meters_Coins_Out = value;
                    this.SendPropertyChanged("Previous_Meters_Coins_Out");
                    this.OnPrevious_Meters_Coins_OutChanged();
                }
            }
        }

        [Column(Storage = "_Previous_Meters_Coin_Drop", DbType = "Int")]
        public System.Nullable<int> Previous_Meters_Coin_Drop
        {
            get
            {
                return this._Previous_Meters_Coin_Drop;
            }
            set
            {
                if ((this._Previous_Meters_Coin_Drop != value))
                {
                    this.OnPrevious_Meters_Coin_DropChanging(value);
                    this.SendPropertyChanging();
                    this._Previous_Meters_Coin_Drop = value;
                    this.SendPropertyChanged("Previous_Meters_Coin_Drop");
                    this.OnPrevious_Meters_Coin_DropChanged();
                }
            }
        }

        [Column(Storage = "_Previous_Meters_Handpay", DbType = "Int")]
        public System.Nullable<int> Previous_Meters_Handpay
        {
            get
            {
                return this._Previous_Meters_Handpay;
            }
            set
            {
                if ((this._Previous_Meters_Handpay != value))
                {
                    this.OnPrevious_Meters_HandpayChanging(value);
                    this.SendPropertyChanging();
                    this._Previous_Meters_Handpay = value;
                    this.SendPropertyChanged("Previous_Meters_Handpay");
                    this.OnPrevious_Meters_HandpayChanged();
                }
            }
        }

        [Column(Storage = "_Previous_Meters_External_Credit", DbType = "Int")]
        public System.Nullable<int> Previous_Meters_External_Credit
        {
            get
            {
                return this._Previous_Meters_External_Credit;
            }
            set
            {
                if ((this._Previous_Meters_External_Credit != value))
                {
                    this.OnPrevious_Meters_External_CreditChanging(value);
                    this.SendPropertyChanging();
                    this._Previous_Meters_External_Credit = value;
                    this.SendPropertyChanged("Previous_Meters_External_Credit");
                    this.OnPrevious_Meters_External_CreditChanged();
                }
            }
        }

        [Column(Storage = "_Previous_Meters_Games_Bet", DbType = "Int")]
        public System.Nullable<int> Previous_Meters_Games_Bet
        {
            get
            {
                return this._Previous_Meters_Games_Bet;
            }
            set
            {
                if ((this._Previous_Meters_Games_Bet != value))
                {
                    this.OnPrevious_Meters_Games_BetChanging(value);
                    this.SendPropertyChanging();
                    this._Previous_Meters_Games_Bet = value;
                    this.SendPropertyChanged("Previous_Meters_Games_Bet");
                    this.OnPrevious_Meters_Games_BetChanged();
                }
            }
        }

        [Column(Storage = "_Previous_Meters_Games_Won", DbType = "Int")]
        public System.Nullable<int> Previous_Meters_Games_Won
        {
            get
            {
                return this._Previous_Meters_Games_Won;
            }
            set
            {
                if ((this._Previous_Meters_Games_Won != value))
                {
                    this.OnPrevious_Meters_Games_WonChanging(value);
                    this.SendPropertyChanging();
                    this._Previous_Meters_Games_Won = value;
                    this.SendPropertyChanged("Previous_Meters_Games_Won");
                    this.OnPrevious_Meters_Games_WonChanged();
                }
            }
        }

        [Column(Storage = "_Previous_Meters_Notes", DbType = "Int")]
        public System.Nullable<int> Previous_Meters_Notes
        {
            get
            {
                return this._Previous_Meters_Notes;
            }
            set
            {
                if ((this._Previous_Meters_Notes != value))
                {
                    this.OnPrevious_Meters_NotesChanging(value);
                    this.SendPropertyChanging();
                    this._Previous_Meters_Notes = value;
                    this.SendPropertyChanged("Previous_Meters_Notes");
                    this.OnPrevious_Meters_NotesChanged();
                }
            }
        }

        [Column(Storage = "_Treasury_Handpay", DbType = "Real")]
        public System.Nullable<float> Treasury_Handpay
        {
            get
            {
                return this._Treasury_Handpay;
            }
            set
            {
                if ((this._Treasury_Handpay != value))
                {
                    this.OnTreasury_HandpayChanging(value);
                    this.SendPropertyChanging();
                    this._Treasury_Handpay = value;
                    this.SendPropertyChanged("Treasury_Handpay");
                    this.OnTreasury_HandpayChanged();
                }
            }
        }

        [Column(Storage = "_Operator_Week_ID", DbType = "Int")]
        public System.Nullable<int> Operator_Week_ID
        {
            get
            {
                return this._Operator_Week_ID;
            }
            set
            {
                if ((this._Operator_Week_ID != value))
                {
                    this.OnOperator_Week_IDChanging(value);
                    this.SendPropertyChanging();
                    this._Operator_Week_ID = value;
                    this.SendPropertyChanged("Operator_Week_ID");
                    this.OnOperator_Week_IDChanged();
                }
            }
        }

        [Column(Storage = "_Operator_Period_ID", DbType = "Int")]
        public System.Nullable<int> Operator_Period_ID
        {
            get
            {
                return this._Operator_Period_ID;
            }
            set
            {
                if ((this._Operator_Period_ID != value))
                {
                    this.OnOperator_Period_IDChanging(value);
                    this.SendPropertyChanging();
                    this._Operator_Period_ID = value;
                    this.SendPropertyChanged("Operator_Period_ID");
                    this.OnOperator_Period_IDChanged();
                }
            }
        }

        [Column(Storage = "_CollectionHandHeldMetersReceived", DbType = "Int")]
        public System.Nullable<int> CollectionHandHeldMetersReceived
        {
            get
            {
                return this._CollectionHandHeldMetersReceived;
            }
            set
            {
                if ((this._CollectionHandHeldMetersReceived != value))
                {
                    this.OnCollectionHandHeldMetersReceivedChanging(value);
                    this.SendPropertyChanging();
                    this._CollectionHandHeldMetersReceived = value;
                    this.SendPropertyChanged("CollectionHandHeldMetersReceived");
                    this.OnCollectionHandHeldMetersReceivedChanged();
                }
            }
        }

        [Column(Storage = "_CollectionNoDoorEvents", DbType = "Int")]
        public System.Nullable<int> CollectionNoDoorEvents
        {
            get
            {
                return this._CollectionNoDoorEvents;
            }
            set
            {
                if ((this._CollectionNoDoorEvents != value))
                {
                    this.OnCollectionNoDoorEventsChanging(value);
                    this.SendPropertyChanging();
                    this._CollectionNoDoorEvents = value;
                    this.SendPropertyChanged("CollectionNoDoorEvents");
                    this.OnCollectionNoDoorEventsChanged();
                }
            }
        }

        [Column(Storage = "_CollectionNoPowerEvents", DbType = "Int")]
        public System.Nullable<int> CollectionNoPowerEvents
        {
            get
            {
                return this._CollectionNoPowerEvents;
            }
            set
            {
                if ((this._CollectionNoPowerEvents != value))
                {
                    this.OnCollectionNoPowerEventsChanging(value);
                    this.SendPropertyChanging();
                    this._CollectionNoPowerEvents = value;
                    this.SendPropertyChanged("CollectionNoPowerEvents");
                    this.OnCollectionNoPowerEventsChanged();
                }
            }
        }

        [Column(Storage = "_CollectionNoFaultEvents", DbType = "Int")]
        public System.Nullable<int> CollectionNoFaultEvents
        {
            get
            {
                return this._CollectionNoFaultEvents;
            }
            set
            {
                if ((this._CollectionNoFaultEvents != value))
                {
                    this.OnCollectionNoFaultEventsChanging(value);
                    this.SendPropertyChanging();
                    this._CollectionNoFaultEvents = value;
                    this.SendPropertyChanged("CollectionNoFaultEvents");
                    this.OnCollectionNoFaultEventsChanged();
                }
            }
        }

        [Column(Storage = "_CollectionTotalDurationPower", DbType = "Int")]
        public System.Nullable<int> CollectionTotalDurationPower
        {
            get
            {
                return this._CollectionTotalDurationPower;
            }
            set
            {
                if ((this._CollectionTotalDurationPower != value))
                {
                    this.OnCollectionTotalDurationPowerChanging(value);
                    this.SendPropertyChanging();
                    this._CollectionTotalDurationPower = value;
                    this.SendPropertyChanged("CollectionTotalDurationPower");
                    this.OnCollectionTotalDurationPowerChanged();
                }
            }
        }

        [Column(Storage = "_CollectionTotalDurationDoor", DbType = "Int")]
        public System.Nullable<int> CollectionTotalDurationDoor
        {
            get
            {
                return this._CollectionTotalDurationDoor;
            }
            set
            {
                if ((this._CollectionTotalDurationDoor != value))
                {
                    this.OnCollectionTotalDurationDoorChanging(value);
                    this.SendPropertyChanging();
                    this._CollectionTotalDurationDoor = value;
                    this.SendPropertyChanged("CollectionTotalDurationDoor");
                    this.OnCollectionTotalDurationDoorChanged();
                }
            }
        }

        [Column(Storage = "_COLLECTION_RDC_VTP", DbType = "Int")]
        public System.Nullable<int> COLLECTION_RDC_VTP
        {
            get
            {
                return this._COLLECTION_RDC_VTP;
            }
            set
            {
                if ((this._COLLECTION_RDC_VTP != value))
                {
                    this.OnCOLLECTION_RDC_VTPChanging(value);
                    this.SendPropertyChanging();
                    this._COLLECTION_RDC_VTP = value;
                    this.SendPropertyChanged("COLLECTION_RDC_VTP");
                    this.OnCOLLECTION_RDC_VTPChanged();
                }
            }
        }

        [Column(Storage = "_Collection_NetEx", DbType = "Real")]
        public System.Nullable<float> Collection_NetEx
        {
            get
            {
                return this._Collection_NetEx;
            }
            set
            {
                if ((this._Collection_NetEx != value))
                {
                    this.OnCollection_NetExChanging(value);
                    this.SendPropertyChanging();
                    this._Collection_NetEx = value;
                    this.SendPropertyChanged("Collection_NetEx");
                    this.OnCollection_NetExChanged();
                }
            }
        }

        [Column(Storage = "_Collection_VAT_Rate", DbType = "Real")]
        public System.Nullable<float> Collection_VAT_Rate
        {
            get
            {
                return this._Collection_VAT_Rate;
            }
            set
            {
                if ((this._Collection_VAT_Rate != value))
                {
                    this.OnCollection_VAT_RateChanging(value);
                    this.SendPropertyChanging();
                    this._Collection_VAT_Rate = value;
                    this.SendPropertyChanged("Collection_VAT_Rate");
                    this.OnCollection_VAT_RateChanged();
                }
            }
        }

        [Column(Storage = "_COLLECTION_RDC_COINS_IN", DbType = "Int")]
        public System.Nullable<int> COLLECTION_RDC_COINS_IN
        {
            get
            {
                return this._COLLECTION_RDC_COINS_IN;
            }
            set
            {
                if ((this._COLLECTION_RDC_COINS_IN != value))
                {
                    this.OnCOLLECTION_RDC_COINS_INChanging(value);
                    this.SendPropertyChanging();
                    this._COLLECTION_RDC_COINS_IN = value;
                    this.SendPropertyChanged("COLLECTION_RDC_COINS_IN");
                    this.OnCOLLECTION_RDC_COINS_INChanged();
                }
            }
        }

        [Column(Storage = "_COLLECTION_RDC_COINS_OUT", DbType = "Int")]
        public System.Nullable<int> COLLECTION_RDC_COINS_OUT
        {
            get
            {
                return this._COLLECTION_RDC_COINS_OUT;
            }
            set
            {
                if ((this._COLLECTION_RDC_COINS_OUT != value))
                {
                    this.OnCOLLECTION_RDC_COINS_OUTChanging(value);
                    this.SendPropertyChanging();
                    this._COLLECTION_RDC_COINS_OUT = value;
                    this.SendPropertyChanged("COLLECTION_RDC_COINS_OUT");
                    this.OnCOLLECTION_RDC_COINS_OUTChanged();
                }
            }
        }

        [Column(Storage = "_COLLECTION_RDC_COIN_DROP", DbType = "Int")]
        public System.Nullable<int> COLLECTION_RDC_COIN_DROP
        {
            get
            {
                return this._COLLECTION_RDC_COIN_DROP;
            }
            set
            {
                if ((this._COLLECTION_RDC_COIN_DROP != value))
                {
                    this.OnCOLLECTION_RDC_COIN_DROPChanging(value);
                    this.SendPropertyChanging();
                    this._COLLECTION_RDC_COIN_DROP = value;
                    this.SendPropertyChanged("COLLECTION_RDC_COIN_DROP");
                    this.OnCOLLECTION_RDC_COIN_DROPChanged();
                }
            }
        }

        [Column(Storage = "_COLLECTION_RDC_HANDPAY", DbType = "Int")]
        public System.Nullable<int> COLLECTION_RDC_HANDPAY
        {
            get
            {
                return this._COLLECTION_RDC_HANDPAY;
            }
            set
            {
                if ((this._COLLECTION_RDC_HANDPAY != value))
                {
                    this.OnCOLLECTION_RDC_HANDPAYChanging(value);
                    this.SendPropertyChanging();
                    this._COLLECTION_RDC_HANDPAY = value;
                    this.SendPropertyChanged("COLLECTION_RDC_HANDPAY");
                    this.OnCOLLECTION_RDC_HANDPAYChanged();
                }
            }
        }

        [Column(Storage = "_COLLECTION_RDC_EXTERNAL_CREDIT", DbType = "Int")]
        public System.Nullable<int> COLLECTION_RDC_EXTERNAL_CREDIT
        {
            get
            {
                return this._COLLECTION_RDC_EXTERNAL_CREDIT;
            }
            set
            {
                if ((this._COLLECTION_RDC_EXTERNAL_CREDIT != value))
                {
                    this.OnCOLLECTION_RDC_EXTERNAL_CREDITChanging(value);
                    this.SendPropertyChanging();
                    this._COLLECTION_RDC_EXTERNAL_CREDIT = value;
                    this.SendPropertyChanged("COLLECTION_RDC_EXTERNAL_CREDIT");
                    this.OnCOLLECTION_RDC_EXTERNAL_CREDITChanged();
                }
            }
        }

        [Column(Storage = "_COLLECTION_RDC_GAMES_BET", DbType = "Int")]
        public System.Nullable<int> COLLECTION_RDC_GAMES_BET
        {
            get
            {
                return this._COLLECTION_RDC_GAMES_BET;
            }
            set
            {
                if ((this._COLLECTION_RDC_GAMES_BET != value))
                {
                    this.OnCOLLECTION_RDC_GAMES_BETChanging(value);
                    this.SendPropertyChanging();
                    this._COLLECTION_RDC_GAMES_BET = value;
                    this.SendPropertyChanged("COLLECTION_RDC_GAMES_BET");
                    this.OnCOLLECTION_RDC_GAMES_BETChanged();
                }
            }
        }

        [Column(Storage = "_COLLECTION_RDC_GAMES_WON", DbType = "Int")]
        public System.Nullable<int> COLLECTION_RDC_GAMES_WON
        {
            get
            {
                return this._COLLECTION_RDC_GAMES_WON;
            }
            set
            {
                if ((this._COLLECTION_RDC_GAMES_WON != value))
                {
                    this.OnCOLLECTION_RDC_GAMES_WONChanging(value);
                    this.SendPropertyChanging();
                    this._COLLECTION_RDC_GAMES_WON = value;
                    this.SendPropertyChanged("COLLECTION_RDC_GAMES_WON");
                    this.OnCOLLECTION_RDC_GAMES_WONChanged();
                }
            }
        }

        [Column(Storage = "_COLLECTION_RDC_NOTES", DbType = "Int")]
        public System.Nullable<int> COLLECTION_RDC_NOTES
        {
            get
            {
                return this._COLLECTION_RDC_NOTES;
            }
            set
            {
                if ((this._COLLECTION_RDC_NOTES != value))
                {
                    this.OnCOLLECTION_RDC_NOTESChanging(value);
                    this.SendPropertyChanging();
                    this._COLLECTION_RDC_NOTES = value;
                    this.SendPropertyChanged("COLLECTION_RDC_NOTES");
                    this.OnCOLLECTION_RDC_NOTESChanged();
                }
            }
        }

        [Column(Storage = "_COLLECTION_RDC_CANCELLED_CREDITS", DbType = "Int")]
        public System.Nullable<int> COLLECTION_RDC_CANCELLED_CREDITS
        {
            get
            {
                return this._COLLECTION_RDC_CANCELLED_CREDITS;
            }
            set
            {
                if ((this._COLLECTION_RDC_CANCELLED_CREDITS != value))
                {
                    this.OnCOLLECTION_RDC_CANCELLED_CREDITSChanging(value);
                    this.SendPropertyChanging();
                    this._COLLECTION_RDC_CANCELLED_CREDITS = value;
                    this.SendPropertyChanged("COLLECTION_RDC_CANCELLED_CREDITS");
                    this.OnCOLLECTION_RDC_CANCELLED_CREDITSChanged();
                }
            }
        }

        [Column(Storage = "_COLLECTION_RDC_GAMES_LOST", DbType = "Int")]
        public System.Nullable<int> COLLECTION_RDC_GAMES_LOST
        {
            get
            {
                return this._COLLECTION_RDC_GAMES_LOST;
            }
            set
            {
                if ((this._COLLECTION_RDC_GAMES_LOST != value))
                {
                    this.OnCOLLECTION_RDC_GAMES_LOSTChanging(value);
                    this.SendPropertyChanging();
                    this._COLLECTION_RDC_GAMES_LOST = value;
                    this.SendPropertyChanged("COLLECTION_RDC_GAMES_LOST");
                    this.OnCOLLECTION_RDC_GAMES_LOSTChanged();
                }
            }
        }

        [Column(Storage = "_COLLECTION_RDC_GAMES_SINCE_POWER_UP", DbType = "Int")]
        public System.Nullable<int> COLLECTION_RDC_GAMES_SINCE_POWER_UP
        {
            get
            {
                return this._COLLECTION_RDC_GAMES_SINCE_POWER_UP;
            }
            set
            {
                if ((this._COLLECTION_RDC_GAMES_SINCE_POWER_UP != value))
                {
                    this.OnCOLLECTION_RDC_GAMES_SINCE_POWER_UPChanging(value);
                    this.SendPropertyChanging();
                    this._COLLECTION_RDC_GAMES_SINCE_POWER_UP = value;
                    this.SendPropertyChanged("COLLECTION_RDC_GAMES_SINCE_POWER_UP");
                    this.OnCOLLECTION_RDC_GAMES_SINCE_POWER_UPChanged();
                }
            }
        }

        [Column(Storage = "_COLLECTION_RDC_TRUE_COIN_IN", DbType = "Int")]
        public System.Nullable<int> COLLECTION_RDC_TRUE_COIN_IN
        {
            get
            {
                return this._COLLECTION_RDC_TRUE_COIN_IN;
            }
            set
            {
                if ((this._COLLECTION_RDC_TRUE_COIN_IN != value))
                {
                    this.OnCOLLECTION_RDC_TRUE_COIN_INChanging(value);
                    this.SendPropertyChanging();
                    this._COLLECTION_RDC_TRUE_COIN_IN = value;
                    this.SendPropertyChanged("COLLECTION_RDC_TRUE_COIN_IN");
                    this.OnCOLLECTION_RDC_TRUE_COIN_INChanged();
                }
            }
        }

        [Column(Storage = "_COLLECTION_RDC_TRUE_COIN_OUT", DbType = "Int")]
        public System.Nullable<int> COLLECTION_RDC_TRUE_COIN_OUT
        {
            get
            {
                return this._COLLECTION_RDC_TRUE_COIN_OUT;
            }
            set
            {
                if ((this._COLLECTION_RDC_TRUE_COIN_OUT != value))
                {
                    this.OnCOLLECTION_RDC_TRUE_COIN_OUTChanging(value);
                    this.SendPropertyChanging();
                    this._COLLECTION_RDC_TRUE_COIN_OUT = value;
                    this.SendPropertyChanged("COLLECTION_RDC_TRUE_COIN_OUT");
                    this.OnCOLLECTION_RDC_TRUE_COIN_OUTChanged();
                }
            }
        }

        [Column(Storage = "_COLLECTION_RDC_CURRENT_CREDITS", DbType = "Int")]
        public System.Nullable<int> COLLECTION_RDC_CURRENT_CREDITS
        {
            get
            {
                return this._COLLECTION_RDC_CURRENT_CREDITS;
            }
            set
            {
                if ((this._COLLECTION_RDC_CURRENT_CREDITS != value))
                {
                    this.OnCOLLECTION_RDC_CURRENT_CREDITSChanging(value);
                    this.SendPropertyChanging();
                    this._COLLECTION_RDC_CURRENT_CREDITS = value;
                    this.SendPropertyChanged("COLLECTION_RDC_CURRENT_CREDITS");
                    this.OnCOLLECTION_RDC_CURRENT_CREDITSChanged();
                }
            }
        }

        [Column(Storage = "_Collection_PoP_Actual", DbType = "Int")]
        public System.Nullable<int> Collection_PoP_Actual
        {
            get
            {
                return this._Collection_PoP_Actual;
            }
            set
            {
                if ((this._Collection_PoP_Actual != value))
                {
                    this.OnCollection_PoP_ActualChanging(value);
                    this.SendPropertyChanging();
                    this._Collection_PoP_Actual = value;
                    this.SendPropertyChanged("Collection_PoP_Actual");
                    this.OnCollection_PoP_ActualChanged();
                }
            }
        }

        [Column(Storage = "_Collection_PoP_Configured", DbType = "Int")]
        public System.Nullable<int> Collection_PoP_Configured
        {
            get
            {
                return this._Collection_PoP_Configured;
            }
            set
            {
                if ((this._Collection_PoP_Configured != value))
                {
                    this.OnCollection_PoP_ConfiguredChanging(value);
                    this.SendPropertyChanging();
                    this._Collection_PoP_Configured = value;
                    this.SendPropertyChanged("Collection_PoP_Configured");
                    this.OnCollection_PoP_ConfiguredChanged();
                }
            }
        }

        [Column(Storage = "_Collection_EDC_Status", DbType = "Int")]
        public System.Nullable<int> Collection_EDC_Status
        {
            get
            {
                return this._Collection_EDC_Status;
            }
            set
            {
                if ((this._Collection_EDC_Status != value))
                {
                    this.OnCollection_EDC_StatusChanging(value);
                    this.SendPropertyChanging();
                    this._Collection_EDC_Status = value;
                    this.SendPropertyChanged("Collection_EDC_Status");
                    this.OnCollection_EDC_StatusChanged();
                }
            }
        }

        [Column(Storage = "_Collection_Meter_Status", DbType = "Int")]
        public System.Nullable<int> Collection_Meter_Status
        {
            get
            {
                return this._Collection_Meter_Status;
            }
            set
            {
                if ((this._Collection_Meter_Status != value))
                {
                    this.OnCollection_Meter_StatusChanging(value);
                    this.SendPropertyChanging();
                    this._Collection_Meter_Status = value;
                    this.SendPropertyChanged("Collection_Meter_Status");
                    this.OnCollection_Meter_StatusChanged();
                }
            }
        }

        [Column(Storage = "_Collection_Cash_Status", DbType = "Int")]
        public System.Nullable<int> Collection_Cash_Status
        {
            get
            {
                return this._Collection_Cash_Status;
            }
            set
            {
                if ((this._Collection_Cash_Status != value))
                {
                    this.OnCollection_Cash_StatusChanging(value);
                    this.SendPropertyChanging();
                    this._Collection_Cash_Status = value;
                    this.SendPropertyChanged("Collection_Cash_Status");
                    this.OnCollection_Cash_StatusChanged();
                }
            }
        }

        [Column(Storage = "_Installation_No", DbType = "Int NOT NULL")]
        public int Installation_No
        {
            get
            {
                return this._Installation_No;
            }
            set
            {
                if ((this._Installation_No != value))
                {
                    this.OnInstallation_NoChanging(value);
                    this.SendPropertyChanging();
                    this._Installation_No = value;
                    this.SendPropertyChanged("Installation_No");
                    this.OnInstallation_NoChanged();
                }
            }
        }

        [Column(Storage = "_Collection_Date", DbType = "DateTime")]
        public System.Nullable<System.DateTime> Collection_Date
        {
            get
            {
                return this._Collection_Date;
            }
            set
            {
                if ((this._Collection_Date != value))
                {
                    this.OnCollection_DateChanging(value);
                    this.SendPropertyChanging();
                    this._Collection_Date = value;
                    this.SendPropertyChanged("Collection_Date");
                    this.OnCollection_DateChanged();
                }
            }
        }

        [Column(Storage = "_CASH_FLOAT_CHANGE_1p", DbType = "Int NOT NULL")]
        public int CASH_FLOAT_CHANGE_1p
        {
            get
            {
                return this._CASH_FLOAT_CHANGE_1p;
            }
            set
            {
                if ((this._CASH_FLOAT_CHANGE_1p != value))
                {
                    this.OnCASH_FLOAT_CHANGE_1pChanging(value);
                    this.SendPropertyChanging();
                    this._CASH_FLOAT_CHANGE_1p = value;
                    this.SendPropertyChanged("CASH_FLOAT_CHANGE_1p");
                    this.OnCASH_FLOAT_CHANGE_1pChanged();
                }
            }
        }

        [Column(Storage = "_CASH_FLOAT_CHANGE_2p", DbType = "Int NOT NULL")]
        public int CASH_FLOAT_CHANGE_2p
        {
            get
            {
                return this._CASH_FLOAT_CHANGE_2p;
            }
            set
            {
                if ((this._CASH_FLOAT_CHANGE_2p != value))
                {
                    this.OnCASH_FLOAT_CHANGE_2pChanging(value);
                    this.SendPropertyChanging();
                    this._CASH_FLOAT_CHANGE_2p = value;
                    this.SendPropertyChanged("CASH_FLOAT_CHANGE_2p");
                    this.OnCASH_FLOAT_CHANGE_2pChanged();
                }
            }
        }

        [Column(Storage = "_CASH_FLOAT_CHANGE_5p", DbType = "Int NOT NULL")]
        public int CASH_FLOAT_CHANGE_5p
        {
            get
            {
                return this._CASH_FLOAT_CHANGE_5p;
            }
            set
            {
                if ((this._CASH_FLOAT_CHANGE_5p != value))
                {
                    this.OnCASH_FLOAT_CHANGE_5pChanging(value);
                    this.SendPropertyChanging();
                    this._CASH_FLOAT_CHANGE_5p = value;
                    this.SendPropertyChanged("CASH_FLOAT_CHANGE_5p");
                    this.OnCASH_FLOAT_CHANGE_5pChanged();
                }
            }
        }

        [Column(Storage = "_CASH_FLOAT_CHANGE_10p", DbType = "Int NOT NULL")]
        public int CASH_FLOAT_CHANGE_10p
        {
            get
            {
                return this._CASH_FLOAT_CHANGE_10p;
            }
            set
            {
                if ((this._CASH_FLOAT_CHANGE_10p != value))
                {
                    this.OnCASH_FLOAT_CHANGE_10pChanging(value);
                    this.SendPropertyChanging();
                    this._CASH_FLOAT_CHANGE_10p = value;
                    this.SendPropertyChanged("CASH_FLOAT_CHANGE_10p");
                    this.OnCASH_FLOAT_CHANGE_10pChanged();
                }
            }
        }

        [Column(Storage = "_CASH_FLOAT_CHANGE_20p", DbType = "Int NOT NULL")]
        public int CASH_FLOAT_CHANGE_20p
        {
            get
            {
                return this._CASH_FLOAT_CHANGE_20p;
            }
            set
            {
                if ((this._CASH_FLOAT_CHANGE_20p != value))
                {
                    this.OnCASH_FLOAT_CHANGE_20pChanging(value);
                    this.SendPropertyChanging();
                    this._CASH_FLOAT_CHANGE_20p = value;
                    this.SendPropertyChanged("CASH_FLOAT_CHANGE_20p");
                    this.OnCASH_FLOAT_CHANGE_20pChanged();
                }
            }
        }

        [Column(Storage = "_CASH_FLOAT_CHANGE_50p", DbType = "Int NOT NULL")]
        public int CASH_FLOAT_CHANGE_50p
        {
            get
            {
                return this._CASH_FLOAT_CHANGE_50p;
            }
            set
            {
                if ((this._CASH_FLOAT_CHANGE_50p != value))
                {
                    this.OnCASH_FLOAT_CHANGE_50pChanging(value);
                    this.SendPropertyChanging();
                    this._CASH_FLOAT_CHANGE_50p = value;
                    this.SendPropertyChanged("CASH_FLOAT_CHANGE_50p");
                    this.OnCASH_FLOAT_CHANGE_50pChanged();
                }
            }
        }

        [Column(Storage = "_CASH_FLOAT_CHANGE_100p", DbType = "Int NOT NULL")]
        public int CASH_FLOAT_CHANGE_100p
        {
            get
            {
                return this._CASH_FLOAT_CHANGE_100p;
            }
            set
            {
                if ((this._CASH_FLOAT_CHANGE_100p != value))
                {
                    this.OnCASH_FLOAT_CHANGE_100pChanging(value);
                    this.SendPropertyChanging();
                    this._CASH_FLOAT_CHANGE_100p = value;
                    this.SendPropertyChanged("CASH_FLOAT_CHANGE_100p");
                    this.OnCASH_FLOAT_CHANGE_100pChanged();
                }
            }
        }

        [Column(Storage = "_CASH_FLOAT_CHANGE_200p", DbType = "Int NOT NULL")]
        public int CASH_FLOAT_CHANGE_200p
        {
            get
            {
                return this._CASH_FLOAT_CHANGE_200p;
            }
            set
            {
                if ((this._CASH_FLOAT_CHANGE_200p != value))
                {
                    this.OnCASH_FLOAT_CHANGE_200pChanging(value);
                    this.SendPropertyChanging();
                    this._CASH_FLOAT_CHANGE_200p = value;
                    this.SendPropertyChanged("CASH_FLOAT_CHANGE_200p");
                    this.OnCASH_FLOAT_CHANGE_200pChanged();
                }
            }
        }

        [Column(Storage = "_CASH_FLOAT_CHANGE_500p", DbType = "Int NOT NULL")]
        public int CASH_FLOAT_CHANGE_500p
        {
            get
            {
                return this._CASH_FLOAT_CHANGE_500p;
            }
            set
            {
                if ((this._CASH_FLOAT_CHANGE_500p != value))
                {
                    this.OnCASH_FLOAT_CHANGE_500pChanging(value);
                    this.SendPropertyChanging();
                    this._CASH_FLOAT_CHANGE_500p = value;
                    this.SendPropertyChanged("CASH_FLOAT_CHANGE_500p");
                    this.OnCASH_FLOAT_CHANGE_500pChanged();
                }
            }
        }

        [Column(Storage = "_CASH_FLOAT_CHANGE_1000p", DbType = "Int NOT NULL")]
        public int CASH_FLOAT_CHANGE_1000p
        {
            get
            {
                return this._CASH_FLOAT_CHANGE_1000p;
            }
            set
            {
                if ((this._CASH_FLOAT_CHANGE_1000p != value))
                {
                    this.OnCASH_FLOAT_CHANGE_1000pChanging(value);
                    this.SendPropertyChanging();
                    this._CASH_FLOAT_CHANGE_1000p = value;
                    this.SendPropertyChanged("CASH_FLOAT_CHANGE_1000p");
                    this.OnCASH_FLOAT_CHANGE_1000pChanged();
                }
            }
        }

        [Column(Storage = "_CASH_FLOAT_TOTAL", DbType = "Real NOT NULL")]
        public float CASH_FLOAT_TOTAL
        {
            get
            {
                return this._CASH_FLOAT_TOTAL;
            }
            set
            {
                if ((this._CASH_FLOAT_TOTAL != value))
                {
                    this.OnCASH_FLOAT_TOTALChanging(value);
                    this.SendPropertyChanging();
                    this._CASH_FLOAT_TOTAL = value;
                    this.SendPropertyChanged("CASH_FLOAT_TOTAL");
                    this.OnCASH_FLOAT_TOTALChanged();
                }
            }
        }

        [Column(Storage = "_DeclaredTicketQty", DbType = "Int")]
        public System.Nullable<int> DeclaredTicketQty
        {
            get
            {
                return this._DeclaredTicketQty;
            }
            set
            {
                if ((this._DeclaredTicketQty != value))
                {
                    this.OnDeclaredTicketQtyChanging(value);
                    this.SendPropertyChanging();
                    this._DeclaredTicketQty = value;
                    this.SendPropertyChanged("DeclaredTicketQty");
                    this.OnDeclaredTicketQtyChanged();
                }
            }
        }

        [Column(Storage = "_DeclaredTicketValue", DbType = "Real")]
        public System.Nullable<float> DeclaredTicketValue
        {
            get
            {
                return this._DeclaredTicketValue;
            }
            set
            {
                if ((this._DeclaredTicketValue != value))
                {
                    this.OnDeclaredTicketValueChanging(value);
                    this.SendPropertyChanging();
                    this._DeclaredTicketValue = value;
                    this.SendPropertyChanged("DeclaredTicketValue");
                    this.OnDeclaredTicketValueChanged();
                }
            }
        }

        [Column(Storage = "_COLLECTION_RDC_JACKPOT", DbType = "Int")]
        public System.Nullable<int> COLLECTION_RDC_JACKPOT
        {
            get
            {
                return this._COLLECTION_RDC_JACKPOT;
            }
            set
            {
                if ((this._COLLECTION_RDC_JACKPOT != value))
                {
                    this.OnCOLLECTION_RDC_JACKPOTChanging(value);
                    this.SendPropertyChanging();
                    this._COLLECTION_RDC_JACKPOT = value;
                    this.SendPropertyChanged("COLLECTION_RDC_JACKPOT");
                    this.OnCOLLECTION_RDC_JACKPOTChanged();
                }
            }
        }

        [Column(Storage = "_COLLECTION_RDC_TICKETS_INSERTED_VALUE", DbType = "Int NOT NULL")]
        public int COLLECTION_RDC_TICKETS_INSERTED_VALUE
        {
            get
            {
                return this._COLLECTION_RDC_TICKETS_INSERTED_VALUE;
            }
            set
            {
                if ((this._COLLECTION_RDC_TICKETS_INSERTED_VALUE != value))
                {
                    this.OnCOLLECTION_RDC_TICKETS_INSERTED_VALUEChanging(value);
                    this.SendPropertyChanging();
                    this._COLLECTION_RDC_TICKETS_INSERTED_VALUE = value;
                    this.SendPropertyChanged("COLLECTION_RDC_TICKETS_INSERTED_VALUE");
                    this.OnCOLLECTION_RDC_TICKETS_INSERTED_VALUEChanged();
                }
            }
        }

        [Column(Storage = "_COLLECTION_RDC_TICKETS_PRINTED_VALUE", DbType = "Int NOT NULL")]
        public int COLLECTION_RDC_TICKETS_PRINTED_VALUE
        {
            get
            {
                return this._COLLECTION_RDC_TICKETS_PRINTED_VALUE;
            }
            set
            {
                if ((this._COLLECTION_RDC_TICKETS_PRINTED_VALUE != value))
                {
                    this.OnCOLLECTION_RDC_TICKETS_PRINTED_VALUEChanging(value);
                    this.SendPropertyChanging();
                    this._COLLECTION_RDC_TICKETS_PRINTED_VALUE = value;
                    this.SendPropertyChanged("COLLECTION_RDC_TICKETS_PRINTED_VALUE");
                    this.OnCOLLECTION_RDC_TICKETS_PRINTED_VALUEChanged();
                }
            }
        }

        [Column(Storage = "_DeclaredTicketPrintedQty", DbType = "Int")]
        public System.Nullable<int> DeclaredTicketPrintedQty
        {
            get
            {
                return this._DeclaredTicketPrintedQty;
            }
            set
            {
                if ((this._DeclaredTicketPrintedQty != value))
                {
                    this.OnDeclaredTicketPrintedQtyChanging(value);
                    this.SendPropertyChanging();
                    this._DeclaredTicketPrintedQty = value;
                    this.SendPropertyChanged("DeclaredTicketPrintedQty");
                    this.OnDeclaredTicketPrintedQtyChanged();
                }
            }
        }

        [Column(Storage = "_DeclaredTicketPrintedValue", DbType = "Real")]
        public System.Nullable<float> DeclaredTicketPrintedValue
        {
            get
            {
                return this._DeclaredTicketPrintedValue;
            }
            set
            {
                if ((this._DeclaredTicketPrintedValue != value))
                {
                    this.OnDeclaredTicketPrintedValueChanging(value);
                    this.SendPropertyChanging();
                    this._DeclaredTicketPrintedValue = value;
                    this.SendPropertyChanged("DeclaredTicketPrintedValue");
                    this.OnDeclaredTicketPrintedValueChanged();
                }
            }
        }

        [Column(Storage = "_Collection_Date_Performed", DbType = "DateTime")]
        public System.Nullable<System.DateTime> Collection_Date_Performed
        {
            get
            {
                return this._Collection_Date_Performed;
            }
            set
            {
                if ((this._Collection_Date_Performed != value))
                {
                    this.OnCollection_Date_PerformedChanging(value);
                    this.SendPropertyChanging();
                    this._Collection_Date_Performed = value;
                    this.SendPropertyChanged("Collection_Date_Performed");
                    this.OnCollection_Date_PerformedChanged();
                }
            }
        }

        [Column(Storage = "_progressive_win_value", DbType = "Int")]
        public System.Nullable<int> progressive_win_value
        {
            get
            {
                return this._progressive_win_value;
            }
            set
            {
                if ((this._progressive_win_value != value))
                {
                    this.Onprogressive_win_valueChanging(value);
                    this.SendPropertyChanging();
                    this._progressive_win_value = value;
                    this.SendPropertyChanged("progressive_win_value");
                    this.Onprogressive_win_valueChanged();
                }
            }
        }

        [Column(Storage = "_progressive_win_Handpay_value", DbType = "Int")]
        public System.Nullable<int> progressive_win_Handpay_value
        {
            get
            {
                return this._progressive_win_Handpay_value;
            }
            set
            {
                if ((this._progressive_win_Handpay_value != value))
                {
                    this.Onprogressive_win_Handpay_valueChanging(value);
                    this.SendPropertyChanging();
                    this._progressive_win_Handpay_value = value;
                    this.SendPropertyChanged("progressive_win_Handpay_value");
                    this.Onprogressive_win_Handpay_valueChanged();
                }
            }
        }

        [Column(Storage = "_Progressive_Value_Declared", DbType = "Float")]
        public System.Nullable<double> Progressive_Value_Declared
        {
            get
            {
                return this._Progressive_Value_Declared;
            }
            set
            {
                if ((this._Progressive_Value_Declared != value))
                {
                    this.OnProgressive_Value_DeclaredChanging(value);
                    this.SendPropertyChanging();
                    this._Progressive_Value_Declared = value;
                    this.SendPropertyChanged("Progressive_Value_Declared");
                    this.OnProgressive_Value_DeclaredChanged();
                }
            }
        }

        public event PropertyChangingEventHandler PropertyChanging;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void SendPropertyChanging()
        {
            if ((this.PropertyChanging != null))
            {
                this.PropertyChanging(this, emptyChangingEventArgs);
            }
        }

        protected virtual void SendPropertyChanged(String propertyName)
        {
            if ((this.PropertyChanged != null))
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

    [Table(Name = "dbo.Event")]
    public partial class Event : INotifyPropertyChanging, INotifyPropertyChanged
    {

        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);

        private int _Event_No;

        private System.Nullable<int> _Event_Detail;

        private System.Nullable<int> _Installation_No;

        private System.Nullable<int> _User_No;

        private System.Nullable<int> _Event_Type;

        private System.Nullable<System.DateTime> _Date;

        private System.Nullable<bool> _Event_Last_Collect;

        #region Extensibility Method Definitions
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
        partial void OnEvent_NoChanging(int value);
        partial void OnEvent_NoChanged();
        partial void OnEvent_DetailChanging(System.Nullable<int> value);
        partial void OnEvent_DetailChanged();
        partial void OnInstallation_NoChanging(System.Nullable<int> value);
        partial void OnInstallation_NoChanged();
        partial void OnUser_NoChanging(System.Nullable<int> value);
        partial void OnUser_NoChanged();
        partial void OnEvent_TypeChanging(System.Nullable<int> value);
        partial void OnEvent_TypeChanged();
        partial void OnDateChanging(System.Nullable<System.DateTime> value);
        partial void OnDateChanged();
        partial void OnEvent_Last_CollectChanging(System.Nullable<bool> value);
        partial void OnEvent_Last_CollectChanged();
        #endregion

        public Event()
        {
            OnCreated();
        }

        [Column(Storage = "_Event_No", AutoSync = AutoSync.OnInsert, DbType = "Int NOT NULL IDENTITY", IsPrimaryKey = true, IsDbGenerated = true)]
        public int Event_No
        {
            get
            {
                return this._Event_No;
            }
            set
            {
                if ((this._Event_No != value))
                {
                    this.OnEvent_NoChanging(value);
                    this.SendPropertyChanging();
                    this._Event_No = value;
                    this.SendPropertyChanged("Event_No");
                    this.OnEvent_NoChanged();
                }
            }
        }

        [Column(Storage = "_Event_Detail", DbType = "Int")]
        public System.Nullable<int> Event_Detail
        {
            get
            {
                return this._Event_Detail;
            }
            set
            {
                if ((this._Event_Detail != value))
                {
                    this.OnEvent_DetailChanging(value);
                    this.SendPropertyChanging();
                    this._Event_Detail = value;
                    this.SendPropertyChanged("Event_Detail");
                    this.OnEvent_DetailChanged();
                }
            }
        }

        [Column(Storage = "_Installation_No", DbType = "Int")]
        public System.Nullable<int> Installation_No
        {
            get
            {
                return this._Installation_No;
            }
            set
            {
                if ((this._Installation_No != value))
                {
                    this.OnInstallation_NoChanging(value);
                    this.SendPropertyChanging();
                    this._Installation_No = value;
                    this.SendPropertyChanged("Installation_No");
                    this.OnInstallation_NoChanged();
                }
            }
        }

        [Column(Storage = "_User_No", DbType = "Int")]
        public System.Nullable<int> User_No
        {
            get
            {
                return this._User_No;
            }
            set
            {
                if ((this._User_No != value))
                {
                    this.OnUser_NoChanging(value);
                    this.SendPropertyChanging();
                    this._User_No = value;
                    this.SendPropertyChanged("User_No");
                    this.OnUser_NoChanged();
                }
            }
        }

        [Column(Storage = "_Event_Type", DbType = "Int")]
        public System.Nullable<int> Event_Type
        {
            get
            {
                return this._Event_Type;
            }
            set
            {
                if ((this._Event_Type != value))
                {
                    this.OnEvent_TypeChanging(value);
                    this.SendPropertyChanging();
                    this._Event_Type = value;
                    this.SendPropertyChanged("Event_Type");
                    this.OnEvent_TypeChanged();
                }
            }
        }

        [Column(Storage = "_Date", DbType = "DateTime")]
        public System.Nullable<System.DateTime> Date
        {
            get
            {
                return this._Date;
            }
            set
            {
                if ((this._Date != value))
                {
                    this.OnDateChanging(value);
                    this.SendPropertyChanging();
                    this._Date = value;
                    this.SendPropertyChanged("Date");
                    this.OnDateChanged();
                }
            }
        }

        [Column(Storage = "_Event_Last_Collect", DbType = "Bit")]
        public System.Nullable<bool> Event_Last_Collect
        {
            get
            {
                return this._Event_Last_Collect;
            }
            set
            {
                if ((this._Event_Last_Collect != value))
                {
                    this.OnEvent_Last_CollectChanging(value);
                    this.SendPropertyChanging();
                    this._Event_Last_Collect = value;
                    this.SendPropertyChanged("Event_Last_Collect");
                    this.OnEvent_Last_CollectChanged();
                }
            }
        }

        public event PropertyChangingEventHandler PropertyChanging;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void SendPropertyChanging()
        {
            if ((this.PropertyChanging != null))
            {
                this.PropertyChanging(this, emptyChangingEventArgs);
            }
        }

        protected virtual void SendPropertyChanged(String propertyName)
        {
            if ((this.PropertyChanged != null))
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

    [Table(Name = "dbo.Part_Collection")]
    public partial class Part_Collection : INotifyPropertyChanging, INotifyPropertyChanged
    {

        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);

        private int _Part_Collection_No;

        private System.Nullable<int> _Collection_No;

        private System.Nullable<int> _Installation_No;

        private System.Nullable<int> _User_No;

        private System.Nullable<System.DateTime> _Part_Collection_Date;

        private System.Nullable<bool> _Part_Collection_Declared;

        private System.Nullable<float> _Part_Collection_CashCollected;

        private System.Nullable<float> _Part_Collection_Cash_Collected_1p;

        private System.Nullable<float> _Part_Collection_Cash_Collected_2p;

        private System.Nullable<float> _Part_Collection_Cash_Collected_5p;

        private System.Nullable<float> _Part_Collection_Cash_Collected_10p;

        private System.Nullable<float> _Part_Collection_Cash_Collected_20p;

        private System.Nullable<float> _Part_Collection_Cash_Collected_50p;

        private System.Nullable<float> _Part_Collection_Cash_Collected_100p;

        private System.Nullable<float> _Part_Collection_Cash_Collected_200p;

        private System.Nullable<float> _Part_Collection_Cash_Collected_500p;

        private System.Nullable<float> _Part_Collection_Cash_Collected_1000p;

        private System.Nullable<float> _Part_Collection_Cash_Collected_2000p;

        private System.Nullable<float> _Part_Collection_Cash_Collected_5000p;

        private System.Nullable<float> _Part_Collection_Cash_Collected_10000p;

        private System.Nullable<float> _Part_Collection_Cash_Collected_20000p;

        private System.Nullable<float> _Part_Collection_Cash_Collected_50000p;

        private System.Nullable<float> _Part_Collection_Cash_Collected_100000p;

        private System.Nullable<float> _Part_Collection_CashRefills;

        private System.Nullable<float> _Part_Collection_Cash_Refills_2p;

        private System.Nullable<float> _Part_Collection_Cash_Refills_5p;

        private System.Nullable<float> _Part_Collection_Cash_Refills_10p;

        private System.Nullable<float> _Part_Collection_Cash_Refills_20p;

        private System.Nullable<float> _Part_Collection_Cash_Refills_50p;

        private System.Nullable<float> _Part_Collection_Cash_Refills_100p;

        private System.Nullable<float> _Part_Collection_Cash_Refills_200p;

        private System.Nullable<float> _Part_Collection_Cash_Refills_500p;

        private System.Nullable<float> _Part_Collection_Cash_Refills_1000p;

        private System.Nullable<float> _Part_Collection_Cash_Refills_2000p;

        private System.Nullable<float> _Part_Collection_Cash_Refills_5000p;

        private System.Nullable<float> _Part_Collection_Cash_Refills_10000p;

        private System.Nullable<float> _Part_Collection_Cash_Refills_20000p;

        private System.Nullable<float> _Part_Collection_Cash_Refills_50000p;

        private System.Nullable<float> _Part_Collection_Cash_Refills_100000p;

        private System.Nullable<float> _Part_Collection_TokensCollected;

        private System.Nullable<float> _Part_Collection_TokenRefills;

        private System.Nullable<int> _Part_Collection_CounterCashIn;

        private System.Nullable<int> _Part_Collection_CounterCashOut;

        private System.Nullable<int> _Part_Collection_CounterTokensIn;

        private System.Nullable<int> _Part_Collection_CounterTokensOut;

        private System.Nullable<int> _Part_Collection_CounterJackpots;

        private System.Nullable<int> _Part_Collection_PreviousCounterCashIn;

        private System.Nullable<int> _Part_Collection_PreviousCounterCashOut;

        private System.Nullable<int> _Part_Collection_PreviousCounterTokensIn;

        private System.Nullable<int> _Part_Collection_PreviousCounterTokensOut;

        private System.Nullable<System.DateTime> _Part_Collection_PreviousCollectionDate;

        private string _Part_Collection_Machine;

        private System.Nullable<int> _Part_Collection_QtyTickets;

        private System.Nullable<float> _Part_Collection_ValueTickets;

        private System.Nullable<System.DateTime> _Part_Collection_Date_Performed;

        #region Extensibility Method Definitions
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
        partial void OnPart_Collection_NoChanging(int value);
        partial void OnPart_Collection_NoChanged();
        partial void OnCollection_NoChanging(System.Nullable<int> value);
        partial void OnCollection_NoChanged();
        partial void OnInstallation_NoChanging(System.Nullable<int> value);
        partial void OnInstallation_NoChanged();
        partial void OnUser_NoChanging(System.Nullable<int> value);
        partial void OnUser_NoChanged();
        partial void OnPart_Collection_DateChanging(System.Nullable<System.DateTime> value);
        partial void OnPart_Collection_DateChanged();
        partial void OnPart_Collection_DeclaredChanging(System.Nullable<bool> value);
        partial void OnPart_Collection_DeclaredChanged();
        partial void OnPart_Collection_CashCollectedChanging(System.Nullable<float> value);
        partial void OnPart_Collection_CashCollectedChanged();
        partial void OnPart_Collection_Cash_Collected_1pChanging(System.Nullable<float> value);
        partial void OnPart_Collection_Cash_Collected_1pChanged();
        partial void OnPart_Collection_Cash_Collected_2pChanging(System.Nullable<float> value);
        partial void OnPart_Collection_Cash_Collected_2pChanged();
        partial void OnPart_Collection_Cash_Collected_5pChanging(System.Nullable<float> value);
        partial void OnPart_Collection_Cash_Collected_5pChanged();
        partial void OnPart_Collection_Cash_Collected_10pChanging(System.Nullable<float> value);
        partial void OnPart_Collection_Cash_Collected_10pChanged();
        partial void OnPart_Collection_Cash_Collected_20pChanging(System.Nullable<float> value);
        partial void OnPart_Collection_Cash_Collected_20pChanged();
        partial void OnPart_Collection_Cash_Collected_50pChanging(System.Nullable<float> value);
        partial void OnPart_Collection_Cash_Collected_50pChanged();
        partial void OnPart_Collection_Cash_Collected_100pChanging(System.Nullable<float> value);
        partial void OnPart_Collection_Cash_Collected_100pChanged();
        partial void OnPart_Collection_Cash_Collected_200pChanging(System.Nullable<float> value);
        partial void OnPart_Collection_Cash_Collected_200pChanged();
        partial void OnPart_Collection_Cash_Collected_500pChanging(System.Nullable<float> value);
        partial void OnPart_Collection_Cash_Collected_500pChanged();
        partial void OnPart_Collection_Cash_Collected_1000pChanging(System.Nullable<float> value);
        partial void OnPart_Collection_Cash_Collected_1000pChanged();
        partial void OnPart_Collection_Cash_Collected_2000pChanging(System.Nullable<float> value);
        partial void OnPart_Collection_Cash_Collected_2000pChanged();
        partial void OnPart_Collection_Cash_Collected_5000pChanging(System.Nullable<float> value);
        partial void OnPart_Collection_Cash_Collected_5000pChanged();
        partial void OnPart_Collection_Cash_Collected_10000pChanging(System.Nullable<float> value);
        partial void OnPart_Collection_Cash_Collected_10000pChanged();
        partial void OnPart_Collection_Cash_Collected_20000pChanging(System.Nullable<float> value);
        partial void OnPart_Collection_Cash_Collected_20000pChanged();
        partial void OnPart_Collection_Cash_Collected_50000pChanging(System.Nullable<float> value);
        partial void OnPart_Collection_Cash_Collected_50000pChanged();
        partial void OnPart_Collection_Cash_Collected_100000pChanging(System.Nullable<float> value);
        partial void OnPart_Collection_Cash_Collected_100000pChanged();
        partial void OnPart_Collection_CashRefillsChanging(System.Nullable<float> value);
        partial void OnPart_Collection_CashRefillsChanged();
        partial void OnPart_Collection_Cash_Refills_2pChanging(System.Nullable<float> value);
        partial void OnPart_Collection_Cash_Refills_2pChanged();
        partial void OnPart_Collection_Cash_Refills_5pChanging(System.Nullable<float> value);
        partial void OnPart_Collection_Cash_Refills_5pChanged();
        partial void OnPart_Collection_Cash_Refills_10pChanging(System.Nullable<float> value);
        partial void OnPart_Collection_Cash_Refills_10pChanged();
        partial void OnPart_Collection_Cash_Refills_20pChanging(System.Nullable<float> value);
        partial void OnPart_Collection_Cash_Refills_20pChanged();
        partial void OnPart_Collection_Cash_Refills_50pChanging(System.Nullable<float> value);
        partial void OnPart_Collection_Cash_Refills_50pChanged();
        partial void OnPart_Collection_Cash_Refills_100pChanging(System.Nullable<float> value);
        partial void OnPart_Collection_Cash_Refills_100pChanged();
        partial void OnPart_Collection_Cash_Refills_200pChanging(System.Nullable<float> value);
        partial void OnPart_Collection_Cash_Refills_200pChanged();
        partial void OnPart_Collection_Cash_Refills_500pChanging(System.Nullable<float> value);
        partial void OnPart_Collection_Cash_Refills_500pChanged();
        partial void OnPart_Collection_Cash_Refills_1000pChanging(System.Nullable<float> value);
        partial void OnPart_Collection_Cash_Refills_1000pChanged();
        partial void OnPart_Collection_Cash_Refills_2000pChanging(System.Nullable<float> value);
        partial void OnPart_Collection_Cash_Refills_2000pChanged();
        partial void OnPart_Collection_Cash_Refills_5000pChanging(System.Nullable<float> value);
        partial void OnPart_Collection_Cash_Refills_5000pChanged();
        partial void OnPart_Collection_Cash_Refills_10000pChanging(System.Nullable<float> value);
        partial void OnPart_Collection_Cash_Refills_10000pChanged();
        partial void OnPart_Collection_Cash_Refills_20000pChanging(System.Nullable<float> value);
        partial void OnPart_Collection_Cash_Refills_20000pChanged();
        partial void OnPart_Collection_Cash_Refills_50000pChanging(System.Nullable<float> value);
        partial void OnPart_Collection_Cash_Refills_50000pChanged();
        partial void OnPart_Collection_Cash_Refills_100000pChanging(System.Nullable<float> value);
        partial void OnPart_Collection_Cash_Refills_100000pChanged();
        partial void OnPart_Collection_TokensCollectedChanging(System.Nullable<float> value);
        partial void OnPart_Collection_TokensCollectedChanged();
        partial void OnPart_Collection_TokenRefillsChanging(System.Nullable<float> value);
        partial void OnPart_Collection_TokenRefillsChanged();
        partial void OnPart_Collection_CounterCashInChanging(System.Nullable<int> value);
        partial void OnPart_Collection_CounterCashInChanged();
        partial void OnPart_Collection_CounterCashOutChanging(System.Nullable<int> value);
        partial void OnPart_Collection_CounterCashOutChanged();
        partial void OnPart_Collection_CounterTokensInChanging(System.Nullable<int> value);
        partial void OnPart_Collection_CounterTokensInChanged();
        partial void OnPart_Collection_CounterTokensOutChanging(System.Nullable<int> value);
        partial void OnPart_Collection_CounterTokensOutChanged();
        partial void OnPart_Collection_CounterJackpotsChanging(System.Nullable<int> value);
        partial void OnPart_Collection_CounterJackpotsChanged();
        partial void OnPart_Collection_PreviousCounterCashInChanging(System.Nullable<int> value);
        partial void OnPart_Collection_PreviousCounterCashInChanged();
        partial void OnPart_Collection_PreviousCounterCashOutChanging(System.Nullable<int> value);
        partial void OnPart_Collection_PreviousCounterCashOutChanged();
        partial void OnPart_Collection_PreviousCounterTokensInChanging(System.Nullable<int> value);
        partial void OnPart_Collection_PreviousCounterTokensInChanged();
        partial void OnPart_Collection_PreviousCounterTokensOutChanging(System.Nullable<int> value);
        partial void OnPart_Collection_PreviousCounterTokensOutChanged();
        partial void OnPart_Collection_PreviousCollectionDateChanging(System.Nullable<System.DateTime> value);
        partial void OnPart_Collection_PreviousCollectionDateChanged();
        partial void OnPart_Collection_MachineChanging(string value);
        partial void OnPart_Collection_MachineChanged();
        partial void OnPart_Collection_QtyTicketsChanging(System.Nullable<int> value);
        partial void OnPart_Collection_QtyTicketsChanged();
        partial void OnPart_Collection_ValueTicketsChanging(System.Nullable<float> value);
        partial void OnPart_Collection_ValueTicketsChanged();
        partial void OnPart_Collection_Date_PerformedChanging(System.Nullable<System.DateTime> value);
        partial void OnPart_Collection_Date_PerformedChanged();
        #endregion

        public Part_Collection()
        {
            OnCreated();
        }

        [Column(Storage = "_Part_Collection_No", AutoSync = AutoSync.OnInsert, DbType = "Int NOT NULL IDENTITY", IsPrimaryKey = true, IsDbGenerated = true)]
        public int Part_Collection_No
        {
            get
            {
                return this._Part_Collection_No;
            }
            set
            {
                if ((this._Part_Collection_No != value))
                {
                    this.OnPart_Collection_NoChanging(value);
                    this.SendPropertyChanging();
                    this._Part_Collection_No = value;
                    this.SendPropertyChanged("Part_Collection_No");
                    this.OnPart_Collection_NoChanged();
                }
            }
        }

        [Column(Storage = "_Collection_No", DbType = "Int")]
        public System.Nullable<int> Collection_No
        {
            get
            {
                return this._Collection_No;
            }
            set
            {
                if ((this._Collection_No != value))
                {
                    this.OnCollection_NoChanging(value);
                    this.SendPropertyChanging();
                    this._Collection_No = value;
                    this.SendPropertyChanged("Collection_No");
                    this.OnCollection_NoChanged();
                }
            }
        }

        [Column(Storage = "_Installation_No", DbType = "Int")]
        public System.Nullable<int> Installation_No
        {
            get
            {
                return this._Installation_No;
            }
            set
            {
                if ((this._Installation_No != value))
                {
                    this.OnInstallation_NoChanging(value);
                    this.SendPropertyChanging();
                    this._Installation_No = value;
                    this.SendPropertyChanged("Installation_No");
                    this.OnInstallation_NoChanged();
                }
            }
        }

        [Column(Storage = "_User_No", DbType = "Int")]
        public System.Nullable<int> User_No
        {
            get
            {
                return this._User_No;
            }
            set
            {
                if ((this._User_No != value))
                {
                    this.OnUser_NoChanging(value);
                    this.SendPropertyChanging();
                    this._User_No = value;
                    this.SendPropertyChanged("User_No");
                    this.OnUser_NoChanged();
                }
            }
        }

        [Column(Storage = "_Part_Collection_Date", DbType = "DateTime")]
        public System.Nullable<System.DateTime> Part_Collection_Date
        {
            get
            {
                return this._Part_Collection_Date;
            }
            set
            {
                if ((this._Part_Collection_Date != value))
                {
                    this.OnPart_Collection_DateChanging(value);
                    this.SendPropertyChanging();
                    this._Part_Collection_Date = value;
                    this.SendPropertyChanged("Part_Collection_Date");
                    this.OnPart_Collection_DateChanged();
                }
            }
        }

        [Column(Storage = "_Part_Collection_Declared", DbType = "Bit")]
        public System.Nullable<bool> Part_Collection_Declared
        {
            get
            {
                return this._Part_Collection_Declared;
            }
            set
            {
                if ((this._Part_Collection_Declared != value))
                {
                    this.OnPart_Collection_DeclaredChanging(value);
                    this.SendPropertyChanging();
                    this._Part_Collection_Declared = value;
                    this.SendPropertyChanged("Part_Collection_Declared");
                    this.OnPart_Collection_DeclaredChanged();
                }
            }
        }

        [Column(Storage = "_Part_Collection_CashCollected", DbType = "Real")]
        public System.Nullable<float> Part_Collection_CashCollected
        {
            get
            {
                return this._Part_Collection_CashCollected;
            }
            set
            {
                if ((this._Part_Collection_CashCollected != value))
                {
                    this.OnPart_Collection_CashCollectedChanging(value);
                    this.SendPropertyChanging();
                    this._Part_Collection_CashCollected = value;
                    this.SendPropertyChanged("Part_Collection_CashCollected");
                    this.OnPart_Collection_CashCollectedChanged();
                }
            }
        }

        [Column(Storage = "_Part_Collection_Cash_Collected_1p", DbType = "Real")]
        public System.Nullable<float> Part_Collection_Cash_Collected_1p
        {
            get
            {
                return this._Part_Collection_Cash_Collected_1p;
            }
            set
            {
                if ((this._Part_Collection_Cash_Collected_1p != value))
                {
                    this.OnPart_Collection_Cash_Collected_1pChanging(value);
                    this.SendPropertyChanging();
                    this._Part_Collection_Cash_Collected_1p = value;
                    this.SendPropertyChanged("Part_Collection_Cash_Collected_1p");
                    this.OnPart_Collection_Cash_Collected_1pChanged();
                }
            }
        }

        [Column(Storage = "_Part_Collection_Cash_Collected_2p", DbType = "Real")]
        public System.Nullable<float> Part_Collection_Cash_Collected_2p
        {
            get
            {
                return this._Part_Collection_Cash_Collected_2p;
            }
            set
            {
                if ((this._Part_Collection_Cash_Collected_2p != value))
                {
                    this.OnPart_Collection_Cash_Collected_2pChanging(value);
                    this.SendPropertyChanging();
                    this._Part_Collection_Cash_Collected_2p = value;
                    this.SendPropertyChanged("Part_Collection_Cash_Collected_2p");
                    this.OnPart_Collection_Cash_Collected_2pChanged();
                }
            }
        }

        [Column(Storage = "_Part_Collection_Cash_Collected_5p", DbType = "Real")]
        public System.Nullable<float> Part_Collection_Cash_Collected_5p
        {
            get
            {
                return this._Part_Collection_Cash_Collected_5p;
            }
            set
            {
                if ((this._Part_Collection_Cash_Collected_5p != value))
                {
                    this.OnPart_Collection_Cash_Collected_5pChanging(value);
                    this.SendPropertyChanging();
                    this._Part_Collection_Cash_Collected_5p = value;
                    this.SendPropertyChanged("Part_Collection_Cash_Collected_5p");
                    this.OnPart_Collection_Cash_Collected_5pChanged();
                }
            }
        }

        [Column(Storage = "_Part_Collection_Cash_Collected_10p", DbType = "Real")]
        public System.Nullable<float> Part_Collection_Cash_Collected_10p
        {
            get
            {
                return this._Part_Collection_Cash_Collected_10p;
            }
            set
            {
                if ((this._Part_Collection_Cash_Collected_10p != value))
                {
                    this.OnPart_Collection_Cash_Collected_10pChanging(value);
                    this.SendPropertyChanging();
                    this._Part_Collection_Cash_Collected_10p = value;
                    this.SendPropertyChanged("Part_Collection_Cash_Collected_10p");
                    this.OnPart_Collection_Cash_Collected_10pChanged();
                }
            }
        }

        [Column(Storage = "_Part_Collection_Cash_Collected_20p", DbType = "Real")]
        public System.Nullable<float> Part_Collection_Cash_Collected_20p
        {
            get
            {
                return this._Part_Collection_Cash_Collected_20p;
            }
            set
            {
                if ((this._Part_Collection_Cash_Collected_20p != value))
                {
                    this.OnPart_Collection_Cash_Collected_20pChanging(value);
                    this.SendPropertyChanging();
                    this._Part_Collection_Cash_Collected_20p = value;
                    this.SendPropertyChanged("Part_Collection_Cash_Collected_20p");
                    this.OnPart_Collection_Cash_Collected_20pChanged();
                }
            }
        }

        [Column(Storage = "_Part_Collection_Cash_Collected_50p", DbType = "Real")]
        public System.Nullable<float> Part_Collection_Cash_Collected_50p
        {
            get
            {
                return this._Part_Collection_Cash_Collected_50p;
            }
            set
            {
                if ((this._Part_Collection_Cash_Collected_50p != value))
                {
                    this.OnPart_Collection_Cash_Collected_50pChanging(value);
                    this.SendPropertyChanging();
                    this._Part_Collection_Cash_Collected_50p = value;
                    this.SendPropertyChanged("Part_Collection_Cash_Collected_50p");
                    this.OnPart_Collection_Cash_Collected_50pChanged();
                }
            }
        }

        [Column(Storage = "_Part_Collection_Cash_Collected_100p", DbType = "Real")]
        public System.Nullable<float> Part_Collection_Cash_Collected_100p
        {
            get
            {
                return this._Part_Collection_Cash_Collected_100p;
            }
            set
            {
                if ((this._Part_Collection_Cash_Collected_100p != value))
                {
                    this.OnPart_Collection_Cash_Collected_100pChanging(value);
                    this.SendPropertyChanging();
                    this._Part_Collection_Cash_Collected_100p = value;
                    this.SendPropertyChanged("Part_Collection_Cash_Collected_100p");
                    this.OnPart_Collection_Cash_Collected_100pChanged();
                }
            }
        }

        [Column(Storage = "_Part_Collection_Cash_Collected_200p", DbType = "Real")]
        public System.Nullable<float> Part_Collection_Cash_Collected_200p
        {
            get
            {
                return this._Part_Collection_Cash_Collected_200p;
            }
            set
            {
                if ((this._Part_Collection_Cash_Collected_200p != value))
                {
                    this.OnPart_Collection_Cash_Collected_200pChanging(value);
                    this.SendPropertyChanging();
                    this._Part_Collection_Cash_Collected_200p = value;
                    this.SendPropertyChanged("Part_Collection_Cash_Collected_200p");
                    this.OnPart_Collection_Cash_Collected_200pChanged();
                }
            }
        }

        [Column(Storage = "_Part_Collection_Cash_Collected_500p", DbType = "Real")]
        public System.Nullable<float> Part_Collection_Cash_Collected_500p
        {
            get
            {
                return this._Part_Collection_Cash_Collected_500p;
            }
            set
            {
                if ((this._Part_Collection_Cash_Collected_500p != value))
                {
                    this.OnPart_Collection_Cash_Collected_500pChanging(value);
                    this.SendPropertyChanging();
                    this._Part_Collection_Cash_Collected_500p = value;
                    this.SendPropertyChanged("Part_Collection_Cash_Collected_500p");
                    this.OnPart_Collection_Cash_Collected_500pChanged();
                }
            }
        }

        [Column(Storage = "_Part_Collection_Cash_Collected_1000p", DbType = "Real")]
        public System.Nullable<float> Part_Collection_Cash_Collected_1000p
        {
            get
            {
                return this._Part_Collection_Cash_Collected_1000p;
            }
            set
            {
                if ((this._Part_Collection_Cash_Collected_1000p != value))
                {
                    this.OnPart_Collection_Cash_Collected_1000pChanging(value);
                    this.SendPropertyChanging();
                    this._Part_Collection_Cash_Collected_1000p = value;
                    this.SendPropertyChanged("Part_Collection_Cash_Collected_1000p");
                    this.OnPart_Collection_Cash_Collected_1000pChanged();
                }
            }
        }

        [Column(Storage = "_Part_Collection_Cash_Collected_2000p", DbType = "Real")]
        public System.Nullable<float> Part_Collection_Cash_Collected_2000p
        {
            get
            {
                return this._Part_Collection_Cash_Collected_2000p;
            }
            set
            {
                if ((this._Part_Collection_Cash_Collected_2000p != value))
                {
                    this.OnPart_Collection_Cash_Collected_2000pChanging(value);
                    this.SendPropertyChanging();
                    this._Part_Collection_Cash_Collected_2000p = value;
                    this.SendPropertyChanged("Part_Collection_Cash_Collected_2000p");
                    this.OnPart_Collection_Cash_Collected_2000pChanged();
                }
            }
        }

        [Column(Storage = "_Part_Collection_Cash_Collected_5000p", DbType = "Real")]
        public System.Nullable<float> Part_Collection_Cash_Collected_5000p
        {
            get
            {
                return this._Part_Collection_Cash_Collected_5000p;
            }
            set
            {
                if ((this._Part_Collection_Cash_Collected_5000p != value))
                {
                    this.OnPart_Collection_Cash_Collected_5000pChanging(value);
                    this.SendPropertyChanging();
                    this._Part_Collection_Cash_Collected_5000p = value;
                    this.SendPropertyChanged("Part_Collection_Cash_Collected_5000p");
                    this.OnPart_Collection_Cash_Collected_5000pChanged();
                }
            }
        }

        [Column(Storage = "_Part_Collection_Cash_Collected_10000p", DbType = "Real")]
        public System.Nullable<float> Part_Collection_Cash_Collected_10000p
        {
            get
            {
                return this._Part_Collection_Cash_Collected_10000p;
            }
            set
            {
                if ((this._Part_Collection_Cash_Collected_10000p != value))
                {
                    this.OnPart_Collection_Cash_Collected_10000pChanging(value);
                    this.SendPropertyChanging();
                    this._Part_Collection_Cash_Collected_10000p = value;
                    this.SendPropertyChanged("Part_Collection_Cash_Collected_10000p");
                    this.OnPart_Collection_Cash_Collected_10000pChanged();
                }
            }
        }

        [Column(Storage = "_Part_Collection_Cash_Collected_20000p", DbType = "Real")]
        public System.Nullable<float> Part_Collection_Cash_Collected_20000p
        {
            get
            {
                return this._Part_Collection_Cash_Collected_20000p;
            }
            set
            {
                if ((this._Part_Collection_Cash_Collected_20000p != value))
                {
                    this.OnPart_Collection_Cash_Collected_20000pChanging(value);
                    this.SendPropertyChanging();
                    this._Part_Collection_Cash_Collected_20000p = value;
                    this.SendPropertyChanged("Part_Collection_Cash_Collected_20000p");
                    this.OnPart_Collection_Cash_Collected_20000pChanged();
                }
            }
        }

        [Column(Storage = "_Part_Collection_Cash_Collected_50000p", DbType = "Real")]
        public System.Nullable<float> Part_Collection_Cash_Collected_50000p
        {
            get
            {
                return this._Part_Collection_Cash_Collected_50000p;
            }
            set
            {
                if ((this._Part_Collection_Cash_Collected_50000p != value))
                {
                    this.OnPart_Collection_Cash_Collected_50000pChanging(value);
                    this.SendPropertyChanging();
                    this._Part_Collection_Cash_Collected_50000p = value;
                    this.SendPropertyChanged("Part_Collection_Cash_Collected_50000p");
                    this.OnPart_Collection_Cash_Collected_50000pChanged();
                }
            }
        }

        [Column(Storage = "_Part_Collection_Cash_Collected_100000p", DbType = "Real")]
        public System.Nullable<float> Part_Collection_Cash_Collected_100000p
        {
            get
            {
                return this._Part_Collection_Cash_Collected_100000p;
            }
            set
            {
                if ((this._Part_Collection_Cash_Collected_100000p != value))
                {
                    this.OnPart_Collection_Cash_Collected_100000pChanging(value);
                    this.SendPropertyChanging();
                    this._Part_Collection_Cash_Collected_100000p = value;
                    this.SendPropertyChanged("Part_Collection_Cash_Collected_100000p");
                    this.OnPart_Collection_Cash_Collected_100000pChanged();
                }
            }
        }

        [Column(Storage = "_Part_Collection_CashRefills", DbType = "Real")]
        public System.Nullable<float> Part_Collection_CashRefills
        {
            get
            {
                return this._Part_Collection_CashRefills;
            }
            set
            {
                if ((this._Part_Collection_CashRefills != value))
                {
                    this.OnPart_Collection_CashRefillsChanging(value);
                    this.SendPropertyChanging();
                    this._Part_Collection_CashRefills = value;
                    this.SendPropertyChanged("Part_Collection_CashRefills");
                    this.OnPart_Collection_CashRefillsChanged();
                }
            }
        }

        [Column(Storage = "_Part_Collection_Cash_Refills_2p", DbType = "Real")]
        public System.Nullable<float> Part_Collection_Cash_Refills_2p
        {
            get
            {
                return this._Part_Collection_Cash_Refills_2p;
            }
            set
            {
                if ((this._Part_Collection_Cash_Refills_2p != value))
                {
                    this.OnPart_Collection_Cash_Refills_2pChanging(value);
                    this.SendPropertyChanging();
                    this._Part_Collection_Cash_Refills_2p = value;
                    this.SendPropertyChanged("Part_Collection_Cash_Refills_2p");
                    this.OnPart_Collection_Cash_Refills_2pChanged();
                }
            }
        }

        [Column(Storage = "_Part_Collection_Cash_Refills_5p", DbType = "Real")]
        public System.Nullable<float> Part_Collection_Cash_Refills_5p
        {
            get
            {
                return this._Part_Collection_Cash_Refills_5p;
            }
            set
            {
                if ((this._Part_Collection_Cash_Refills_5p != value))
                {
                    this.OnPart_Collection_Cash_Refills_5pChanging(value);
                    this.SendPropertyChanging();
                    this._Part_Collection_Cash_Refills_5p = value;
                    this.SendPropertyChanged("Part_Collection_Cash_Refills_5p");
                    this.OnPart_Collection_Cash_Refills_5pChanged();
                }
            }
        }

        [Column(Storage = "_Part_Collection_Cash_Refills_10p", DbType = "Real")]
        public System.Nullable<float> Part_Collection_Cash_Refills_10p
        {
            get
            {
                return this._Part_Collection_Cash_Refills_10p;
            }
            set
            {
                if ((this._Part_Collection_Cash_Refills_10p != value))
                {
                    this.OnPart_Collection_Cash_Refills_10pChanging(value);
                    this.SendPropertyChanging();
                    this._Part_Collection_Cash_Refills_10p = value;
                    this.SendPropertyChanged("Part_Collection_Cash_Refills_10p");
                    this.OnPart_Collection_Cash_Refills_10pChanged();
                }
            }
        }

        [Column(Storage = "_Part_Collection_Cash_Refills_20p", DbType = "Real")]
        public System.Nullable<float> Part_Collection_Cash_Refills_20p
        {
            get
            {
                return this._Part_Collection_Cash_Refills_20p;
            }
            set
            {
                if ((this._Part_Collection_Cash_Refills_20p != value))
                {
                    this.OnPart_Collection_Cash_Refills_20pChanging(value);
                    this.SendPropertyChanging();
                    this._Part_Collection_Cash_Refills_20p = value;
                    this.SendPropertyChanged("Part_Collection_Cash_Refills_20p");
                    this.OnPart_Collection_Cash_Refills_20pChanged();
                }
            }
        }

        [Column(Storage = "_Part_Collection_Cash_Refills_50p", DbType = "Real")]
        public System.Nullable<float> Part_Collection_Cash_Refills_50p
        {
            get
            {
                return this._Part_Collection_Cash_Refills_50p;
            }
            set
            {
                if ((this._Part_Collection_Cash_Refills_50p != value))
                {
                    this.OnPart_Collection_Cash_Refills_50pChanging(value);
                    this.SendPropertyChanging();
                    this._Part_Collection_Cash_Refills_50p = value;
                    this.SendPropertyChanged("Part_Collection_Cash_Refills_50p");
                    this.OnPart_Collection_Cash_Refills_50pChanged();
                }
            }
        }

        [Column(Storage = "_Part_Collection_Cash_Refills_100p", DbType = "Real")]
        public System.Nullable<float> Part_Collection_Cash_Refills_100p
        {
            get
            {
                return this._Part_Collection_Cash_Refills_100p;
            }
            set
            {
                if ((this._Part_Collection_Cash_Refills_100p != value))
                {
                    this.OnPart_Collection_Cash_Refills_100pChanging(value);
                    this.SendPropertyChanging();
                    this._Part_Collection_Cash_Refills_100p = value;
                    this.SendPropertyChanged("Part_Collection_Cash_Refills_100p");
                    this.OnPart_Collection_Cash_Refills_100pChanged();
                }
            }
        }

        [Column(Storage = "_Part_Collection_Cash_Refills_200p", DbType = "Real")]
        public System.Nullable<float> Part_Collection_Cash_Refills_200p
        {
            get
            {
                return this._Part_Collection_Cash_Refills_200p;
            }
            set
            {
                if ((this._Part_Collection_Cash_Refills_200p != value))
                {
                    this.OnPart_Collection_Cash_Refills_200pChanging(value);
                    this.SendPropertyChanging();
                    this._Part_Collection_Cash_Refills_200p = value;
                    this.SendPropertyChanged("Part_Collection_Cash_Refills_200p");
                    this.OnPart_Collection_Cash_Refills_200pChanged();
                }
            }
        }

        [Column(Storage = "_Part_Collection_Cash_Refills_500p", DbType = "Real")]
        public System.Nullable<float> Part_Collection_Cash_Refills_500p
        {
            get
            {
                return this._Part_Collection_Cash_Refills_500p;
            }
            set
            {
                if ((this._Part_Collection_Cash_Refills_500p != value))
                {
                    this.OnPart_Collection_Cash_Refills_500pChanging(value);
                    this.SendPropertyChanging();
                    this._Part_Collection_Cash_Refills_500p = value;
                    this.SendPropertyChanged("Part_Collection_Cash_Refills_500p");
                    this.OnPart_Collection_Cash_Refills_500pChanged();
                }
            }
        }

        [Column(Storage = "_Part_Collection_Cash_Refills_1000p", DbType = "Real")]
        public System.Nullable<float> Part_Collection_Cash_Refills_1000p
        {
            get
            {
                return this._Part_Collection_Cash_Refills_1000p;
            }
            set
            {
                if ((this._Part_Collection_Cash_Refills_1000p != value))
                {
                    this.OnPart_Collection_Cash_Refills_1000pChanging(value);
                    this.SendPropertyChanging();
                    this._Part_Collection_Cash_Refills_1000p = value;
                    this.SendPropertyChanged("Part_Collection_Cash_Refills_1000p");
                    this.OnPart_Collection_Cash_Refills_1000pChanged();
                }
            }
        }

        [Column(Storage = "_Part_Collection_Cash_Refills_2000p", DbType = "Real")]
        public System.Nullable<float> Part_Collection_Cash_Refills_2000p
        {
            get
            {
                return this._Part_Collection_Cash_Refills_2000p;
            }
            set
            {
                if ((this._Part_Collection_Cash_Refills_2000p != value))
                {
                    this.OnPart_Collection_Cash_Refills_2000pChanging(value);
                    this.SendPropertyChanging();
                    this._Part_Collection_Cash_Refills_2000p = value;
                    this.SendPropertyChanged("Part_Collection_Cash_Refills_2000p");
                    this.OnPart_Collection_Cash_Refills_2000pChanged();
                }
            }
        }

        [Column(Storage = "_Part_Collection_Cash_Refills_5000p", DbType = "Real")]
        public System.Nullable<float> Part_Collection_Cash_Refills_5000p
        {
            get
            {
                return this._Part_Collection_Cash_Refills_5000p;
            }
            set
            {
                if ((this._Part_Collection_Cash_Refills_5000p != value))
                {
                    this.OnPart_Collection_Cash_Refills_5000pChanging(value);
                    this.SendPropertyChanging();
                    this._Part_Collection_Cash_Refills_5000p = value;
                    this.SendPropertyChanged("Part_Collection_Cash_Refills_5000p");
                    this.OnPart_Collection_Cash_Refills_5000pChanged();
                }
            }
        }

        [Column(Storage = "_Part_Collection_Cash_Refills_10000p", DbType = "Real")]
        public System.Nullable<float> Part_Collection_Cash_Refills_10000p
        {
            get
            {
                return this._Part_Collection_Cash_Refills_10000p;
            }
            set
            {
                if ((this._Part_Collection_Cash_Refills_10000p != value))
                {
                    this.OnPart_Collection_Cash_Refills_10000pChanging(value);
                    this.SendPropertyChanging();
                    this._Part_Collection_Cash_Refills_10000p = value;
                    this.SendPropertyChanged("Part_Collection_Cash_Refills_10000p");
                    this.OnPart_Collection_Cash_Refills_10000pChanged();
                }
            }
        }

        [Column(Storage = "_Part_Collection_Cash_Refills_20000p", DbType = "Real")]
        public System.Nullable<float> Part_Collection_Cash_Refills_20000p
        {
            get
            {
                return this._Part_Collection_Cash_Refills_20000p;
            }
            set
            {
                if ((this._Part_Collection_Cash_Refills_20000p != value))
                {
                    this.OnPart_Collection_Cash_Refills_20000pChanging(value);
                    this.SendPropertyChanging();
                    this._Part_Collection_Cash_Refills_20000p = value;
                    this.SendPropertyChanged("Part_Collection_Cash_Refills_20000p");
                    this.OnPart_Collection_Cash_Refills_20000pChanged();
                }
            }
        }

        [Column(Storage = "_Part_Collection_Cash_Refills_50000p", DbType = "Real")]
        public System.Nullable<float> Part_Collection_Cash_Refills_50000p
        {
            get
            {
                return this._Part_Collection_Cash_Refills_50000p;
            }
            set
            {
                if ((this._Part_Collection_Cash_Refills_50000p != value))
                {
                    this.OnPart_Collection_Cash_Refills_50000pChanging(value);
                    this.SendPropertyChanging();
                    this._Part_Collection_Cash_Refills_50000p = value;
                    this.SendPropertyChanged("Part_Collection_Cash_Refills_50000p");
                    this.OnPart_Collection_Cash_Refills_50000pChanged();
                }
            }
        }

        [Column(Storage = "_Part_Collection_Cash_Refills_100000p", DbType = "Real")]
        public System.Nullable<float> Part_Collection_Cash_Refills_100000p
        {
            get
            {
                return this._Part_Collection_Cash_Refills_100000p;
            }
            set
            {
                if ((this._Part_Collection_Cash_Refills_100000p != value))
                {
                    this.OnPart_Collection_Cash_Refills_100000pChanging(value);
                    this.SendPropertyChanging();
                    this._Part_Collection_Cash_Refills_100000p = value;
                    this.SendPropertyChanged("Part_Collection_Cash_Refills_100000p");
                    this.OnPart_Collection_Cash_Refills_100000pChanged();
                }
            }
        }

        [Column(Storage = "_Part_Collection_TokensCollected", DbType = "Real")]
        public System.Nullable<float> Part_Collection_TokensCollected
        {
            get
            {
                return this._Part_Collection_TokensCollected;
            }
            set
            {
                if ((this._Part_Collection_TokensCollected != value))
                {
                    this.OnPart_Collection_TokensCollectedChanging(value);
                    this.SendPropertyChanging();
                    this._Part_Collection_TokensCollected = value;
                    this.SendPropertyChanged("Part_Collection_TokensCollected");
                    this.OnPart_Collection_TokensCollectedChanged();
                }
            }
        }

        [Column(Storage = "_Part_Collection_TokenRefills", DbType = "Real")]
        public System.Nullable<float> Part_Collection_TokenRefills
        {
            get
            {
                return this._Part_Collection_TokenRefills;
            }
            set
            {
                if ((this._Part_Collection_TokenRefills != value))
                {
                    this.OnPart_Collection_TokenRefillsChanging(value);
                    this.SendPropertyChanging();
                    this._Part_Collection_TokenRefills = value;
                    this.SendPropertyChanged("Part_Collection_TokenRefills");
                    this.OnPart_Collection_TokenRefillsChanged();
                }
            }
        }

        [Column(Storage = "_Part_Collection_CounterCashIn", DbType = "Int")]
        public System.Nullable<int> Part_Collection_CounterCashIn
        {
            get
            {
                return this._Part_Collection_CounterCashIn;
            }
            set
            {
                if ((this._Part_Collection_CounterCashIn != value))
                {
                    this.OnPart_Collection_CounterCashInChanging(value);
                    this.SendPropertyChanging();
                    this._Part_Collection_CounterCashIn = value;
                    this.SendPropertyChanged("Part_Collection_CounterCashIn");
                    this.OnPart_Collection_CounterCashInChanged();
                }
            }
        }

        [Column(Storage = "_Part_Collection_CounterCashOut", DbType = "Int")]
        public System.Nullable<int> Part_Collection_CounterCashOut
        {
            get
            {
                return this._Part_Collection_CounterCashOut;
            }
            set
            {
                if ((this._Part_Collection_CounterCashOut != value))
                {
                    this.OnPart_Collection_CounterCashOutChanging(value);
                    this.SendPropertyChanging();
                    this._Part_Collection_CounterCashOut = value;
                    this.SendPropertyChanged("Part_Collection_CounterCashOut");
                    this.OnPart_Collection_CounterCashOutChanged();
                }
            }
        }

        [Column(Storage = "_Part_Collection_CounterTokensIn", DbType = "Int")]
        public System.Nullable<int> Part_Collection_CounterTokensIn
        {
            get
            {
                return this._Part_Collection_CounterTokensIn;
            }
            set
            {
                if ((this._Part_Collection_CounterTokensIn != value))
                {
                    this.OnPart_Collection_CounterTokensInChanging(value);
                    this.SendPropertyChanging();
                    this._Part_Collection_CounterTokensIn = value;
                    this.SendPropertyChanged("Part_Collection_CounterTokensIn");
                    this.OnPart_Collection_CounterTokensInChanged();
                }
            }
        }

        [Column(Storage = "_Part_Collection_CounterTokensOut", DbType = "Int")]
        public System.Nullable<int> Part_Collection_CounterTokensOut
        {
            get
            {
                return this._Part_Collection_CounterTokensOut;
            }
            set
            {
                if ((this._Part_Collection_CounterTokensOut != value))
                {
                    this.OnPart_Collection_CounterTokensOutChanging(value);
                    this.SendPropertyChanging();
                    this._Part_Collection_CounterTokensOut = value;
                    this.SendPropertyChanged("Part_Collection_CounterTokensOut");
                    this.OnPart_Collection_CounterTokensOutChanged();
                }
            }
        }

        [Column(Storage = "_Part_Collection_CounterJackpots", DbType = "Int")]
        public System.Nullable<int> Part_Collection_CounterJackpots
        {
            get
            {
                return this._Part_Collection_CounterJackpots;
            }
            set
            {
                if ((this._Part_Collection_CounterJackpots != value))
                {
                    this.OnPart_Collection_CounterJackpotsChanging(value);
                    this.SendPropertyChanging();
                    this._Part_Collection_CounterJackpots = value;
                    this.SendPropertyChanged("Part_Collection_CounterJackpots");
                    this.OnPart_Collection_CounterJackpotsChanged();
                }
            }
        }

        [Column(Storage = "_Part_Collection_PreviousCounterCashIn", DbType = "Int")]
        public System.Nullable<int> Part_Collection_PreviousCounterCashIn
        {
            get
            {
                return this._Part_Collection_PreviousCounterCashIn;
            }
            set
            {
                if ((this._Part_Collection_PreviousCounterCashIn != value))
                {
                    this.OnPart_Collection_PreviousCounterCashInChanging(value);
                    this.SendPropertyChanging();
                    this._Part_Collection_PreviousCounterCashIn = value;
                    this.SendPropertyChanged("Part_Collection_PreviousCounterCashIn");
                    this.OnPart_Collection_PreviousCounterCashInChanged();
                }
            }
        }

        [Column(Storage = "_Part_Collection_PreviousCounterCashOut", DbType = "Int")]
        public System.Nullable<int> Part_Collection_PreviousCounterCashOut
        {
            get
            {
                return this._Part_Collection_PreviousCounterCashOut;
            }
            set
            {
                if ((this._Part_Collection_PreviousCounterCashOut != value))
                {
                    this.OnPart_Collection_PreviousCounterCashOutChanging(value);
                    this.SendPropertyChanging();
                    this._Part_Collection_PreviousCounterCashOut = value;
                    this.SendPropertyChanged("Part_Collection_PreviousCounterCashOut");
                    this.OnPart_Collection_PreviousCounterCashOutChanged();
                }
            }
        }

        [Column(Storage = "_Part_Collection_PreviousCounterTokensIn", DbType = "Int")]
        public System.Nullable<int> Part_Collection_PreviousCounterTokensIn
        {
            get
            {
                return this._Part_Collection_PreviousCounterTokensIn;
            }
            set
            {
                if ((this._Part_Collection_PreviousCounterTokensIn != value))
                {
                    this.OnPart_Collection_PreviousCounterTokensInChanging(value);
                    this.SendPropertyChanging();
                    this._Part_Collection_PreviousCounterTokensIn = value;
                    this.SendPropertyChanged("Part_Collection_PreviousCounterTokensIn");
                    this.OnPart_Collection_PreviousCounterTokensInChanged();
                }
            }
        }

        [Column(Storage = "_Part_Collection_PreviousCounterTokensOut", DbType = "Int")]
        public System.Nullable<int> Part_Collection_PreviousCounterTokensOut
        {
            get
            {
                return this._Part_Collection_PreviousCounterTokensOut;
            }
            set
            {
                if ((this._Part_Collection_PreviousCounterTokensOut != value))
                {
                    this.OnPart_Collection_PreviousCounterTokensOutChanging(value);
                    this.SendPropertyChanging();
                    this._Part_Collection_PreviousCounterTokensOut = value;
                    this.SendPropertyChanged("Part_Collection_PreviousCounterTokensOut");
                    this.OnPart_Collection_PreviousCounterTokensOutChanged();
                }
            }
        }

        [Column(Storage = "_Part_Collection_PreviousCollectionDate", DbType = "DateTime")]
        public System.Nullable<System.DateTime> Part_Collection_PreviousCollectionDate
        {
            get
            {
                return this._Part_Collection_PreviousCollectionDate;
            }
            set
            {
                if ((this._Part_Collection_PreviousCollectionDate != value))
                {
                    this.OnPart_Collection_PreviousCollectionDateChanging(value);
                    this.SendPropertyChanging();
                    this._Part_Collection_PreviousCollectionDate = value;
                    this.SendPropertyChanged("Part_Collection_PreviousCollectionDate");
                    this.OnPart_Collection_PreviousCollectionDateChanged();
                }
            }
        }

        [Column(Storage = "_Part_Collection_Machine", DbType = "VarChar(50)")]
        public string Part_Collection_Machine
        {
            get
            {
                return this._Part_Collection_Machine;
            }
            set
            {
                if ((this._Part_Collection_Machine != value))
                {
                    this.OnPart_Collection_MachineChanging(value);
                    this.SendPropertyChanging();
                    this._Part_Collection_Machine = value;
                    this.SendPropertyChanged("Part_Collection_Machine");
                    this.OnPart_Collection_MachineChanged();
                }
            }
        }

        [Column(Storage = "_Part_Collection_QtyTickets", DbType = "Int")]
        public System.Nullable<int> Part_Collection_QtyTickets
        {
            get
            {
                return this._Part_Collection_QtyTickets;
            }
            set
            {
                if ((this._Part_Collection_QtyTickets != value))
                {
                    this.OnPart_Collection_QtyTicketsChanging(value);
                    this.SendPropertyChanging();
                    this._Part_Collection_QtyTickets = value;
                    this.SendPropertyChanged("Part_Collection_QtyTickets");
                    this.OnPart_Collection_QtyTicketsChanged();
                }
            }
        }

        [Column(Storage = "_Part_Collection_ValueTickets", DbType = "Real")]
        public System.Nullable<float> Part_Collection_ValueTickets
        {
            get
            {
                return this._Part_Collection_ValueTickets;
            }
            set
            {
                if ((this._Part_Collection_ValueTickets != value))
                {
                    this.OnPart_Collection_ValueTicketsChanging(value);
                    this.SendPropertyChanging();
                    this._Part_Collection_ValueTickets = value;
                    this.SendPropertyChanged("Part_Collection_ValueTickets");
                    this.OnPart_Collection_ValueTicketsChanged();
                }
            }
        }

        [Column(Storage = "_Part_Collection_Date_Performed", DbType = "DateTime")]
        public System.Nullable<System.DateTime> Part_Collection_Date_Performed
        {
            get
            {
                return this._Part_Collection_Date_Performed;
            }
            set
            {
                if ((this._Part_Collection_Date_Performed != value))
                {
                    this.OnPart_Collection_Date_PerformedChanging(value);
                    this.SendPropertyChanging();
                    this._Part_Collection_Date_Performed = value;
                    this.SendPropertyChanged("Part_Collection_Date_Performed");
                    this.OnPart_Collection_Date_PerformedChanged();
                }
            }
        }


        public event PropertyChangingEventHandler PropertyChanging;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void SendPropertyChanging()
        {
            if ((this.PropertyChanging != null))
            {
                this.PropertyChanging(this, emptyChangingEventArgs);
            }
        }

        protected virtual void SendPropertyChanged(String propertyName)
        {
            if ((this.PropertyChanged != null))
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
    public class UndeclaredCollectionRecord : INotifyPropertyChanged
    {
        // Added bool to handle sort in datagrid
        public bool IsTotalRow { get; set; }
        private string _region = "IT";
        private int _p100000;
        private int _p50000;
        private int _p20000;
        private int _p10000;
        private int _p5000;
        private int _p2000;
        private int _p1000;
        private int _p500;
        private int _p200;
        private int _p100;
        private decimal _p50;
        private decimal _p20;
        private decimal _p10;
        private decimal _p5;
        private decimal _p2;
        private decimal _p1;
        private decimal _Tickets_In;
        private decimal _Tickets_Out;
        private decimal _Coin_Out;
        private decimal _Coin_In;
        private decimal _Jackpots;
        private decimal _Handpays;
        private decimal _CancelledCredits;

        private decimal _WinLossValue;
        private decimal _AttendantPayValue;
        private decimal _EFTIn;
        private decimal _EFTOut;
        private decimal _ShortPayValue;
        private decimal _RefundValue;
        private decimal _RefillsValue;

        public int ReferenceID;
        public string Zone { get; set; }
        public string Position { get; set; }
        public string GameTitle { get; set; }
        public string AssetNo { get; set; }
        public string Collection_Batch_Name { get; set; } 
        public DateTime CollectionDate { get; set; }
        public DateTime CollectionTime { get; set; }
        public string Type { get; set; }
        public string Meters { get; set; }
        public int CollectionNo { get; set; }
        public int CollectionBatchNo { get; set; }
        public int InstallationNo { get; set; }
        public int Installation_Token_Value { get; set; }
        public string Date
        {
            get
            {
                return CollectionDate.Equals(DateTime.MinValue) ? "" : CollectionDate.Date.ToShortDateString();
            }
        }

        public string Time
        {
            get
            {
                return CollectionDate.Equals(DateTime.MinValue) ? "" : CollectionDate.ToString("HH:mm");
            }
        }

        public string Region
        {
            get { return _region; }
            set { _region = value; }
        }

        public decimal TotalAmountValue { get { return TotalBillValue + TotalCoinsValue + TicketsInValue; } }

        public decimal TotalBillValue
        {
            get
            {

                //if (Region.ToUpper().Contains("US"))
                //    return P100000 + P50000 + P20000 + P10000 + P5000 + P2000 + P1000 + P500 + P200 + P100;
                switch (Settings.Region.ToUpper())
                {
                    case "US":
                        return P100000 + P50000 + P20000 + P10000 + P5000 + P2000 + P1000 + P500 + P200 + P100;
                    case "AR":
                        return P100000 + P50000 + P20000 + P10000 + P5000 + P2000 + P1000 + P500 + P200;
                    default:
                        return P100000 + P50000 + P20000 + P10000 + P5000 + P2000 + P1000 + P500;
                }
            }
        }
        public decimal TotalCoinsValue
        {
            get
            {
                //if (Region.ToUpper().Contains("US"))
                //    return P50 + P20 + P10 + P5 + P2;
                switch (Settings.Region.ToUpper())
                {
                    case "US":
                        return P50 + P20 + P10 + P5 + P2 + P1;
                    case "AR":
                        return P100 + P50 + P20 + P10 + P5 + P2 + P1;
                    default:
                        return P200 + P100 + P50 + P20 + P10 + P5 + P2 + P1;
                }


            }
        }
        
        public string P100000S
        {
            get { return  "THOUSANDS";}
        }
        public string P50000S
        {
            get { return "FIVE_HUNDREDS"; }
        }
        public string P20000S
        {
            get { return "TWO_HUNDREDS"; }
        }
        public string P10000S
        {
            get { return  "HUNDREDS";}
        }
        public string P5000S
        {
            get { return "FIFTIES"; }
        }
        public string P2000S
        {
            get { return "TWENTIES";}
        }
        public string P1000S
        {
            get { return "TENS";}
        }
        public string P500S
        {
            get { return "FIVES";}
        }
        public string P200S
        {
            get { return "TWOS";}
        }
        public string P100S
        {
            get { return "ONES"; }
        }
        public string P50S
        {
            get { return "FIFTY_CENTS"; }
        }
        public string P20S
        {
            get { return "TWENTY_CENTS"; }
        }
        public string P10S
        {
            get { return "TEN_CENTS"; }
        }
        public string P5S
        {
            get { return "FIVE_CENTS"; }
        }
        public string P2S
        {
            get { return "TWO_CENTS"; }
        }
        public string P1S
        {
            get { return "ONE_CENT"; }
        }
        
        public string P100000C
        {
            get { return Convert.ToDecimal(P100000).GetUniversalCurrencyFormatWithSymbol(); }
        }
        public string P50000C
        {
            get { return Convert.ToDecimal(P50000).GetUniversalCurrencyFormatWithSymbol(); }
        }
        public string P20000C
        {
            get { return Convert.ToDecimal(P20000).GetUniversalCurrencyFormatWithSymbol(); }
        }
        public string P10000C
        {
            get { return Convert.ToDecimal(P10000).GetUniversalCurrencyFormatWithSymbol(); }
        }
        public string P5000C
        {
            get { return Convert.ToDecimal(P5000).GetUniversalCurrencyFormatWithSymbol(); }
        }
        public string P2000C
        {
            get { return Convert.ToDecimal(P2000).GetUniversalCurrencyFormatWithSymbol(); }
        }
        public string P1000C
        {
            get { return Convert.ToDecimal(P1000).GetUniversalCurrencyFormatWithSymbol(); }
        }
        public string P500C
        {
            get { return Convert.ToDecimal(P500).GetUniversalCurrencyFormatWithSymbol(); }
        }
        public string P200C
        {
            get { return Convert.ToDecimal(P200).GetUniversalCurrencyFormatWithSymbol(); }
        }
        public string P100C
        {
            get { return Convert.ToDecimal(P100).GetUniversalCurrencyFormatWithSymbol(); }
        }
        public string P50C
        {
            get { return Convert.ToDecimal(P50).GetUniversalCurrencyFormatWithSymbol(); }
        }
        public string P20C
        {
            get { return Convert.ToDecimal(P20).GetUniversalCurrencyFormatWithSymbol(); }
        }
        public string P10C
        {
            get { return Convert.ToDecimal(P10).GetUniversalCurrencyFormatWithSymbol(); }
        }
        public string P5C
        {
            get { return Convert.ToDecimal(P5).GetUniversalCurrencyFormatWithSymbol(); }
        }
        public string P2C
        {
            get { return Convert.ToDecimal(P2).GetUniversalCurrencyFormatWithSymbol(); }
        }
        public string P1C
        {
            get { return Convert.ToDecimal(P1).GetUniversalCurrencyFormatWithSymbol(); }
        }

        public int P100000
        {
            get { return _p100000; }
            set
            {
                _p100000 = value;
                if (PropertyChanged != null)
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("P100000"));
            }
        }
        public int P50000
        {
            get { return _p50000; }
            set
            {
                _p50000 = value;
                if (PropertyChanged != null)
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("P50000"));
            }
        }
        public int P20000
        {
            get { return _p20000; }
            set
            {
                _p20000 = value;
                if (PropertyChanged != null)
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("P20000"));
            }
        }
        public int P10000
        {
            get { return _p10000; }
            set
            {
                _p10000 = value;
                if (PropertyChanged != null)
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("P10000"));
            }
        }
        public int P5000
        {
            get { return _p5000; }
            set
            {
                _p5000 = value;
                if (PropertyChanged != null)
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("P5000"));
            }
        }
        public int P2000
        {
            get { return _p2000; }
            set
            {
                _p2000 = value;
                if (PropertyChanged != null)
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("P2000"));
            }
        }
        public int P1000
        {
            get { return _p1000; }
            set
            {
                _p1000 = value;
                if (PropertyChanged != null)
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("P1000"));
            }
        }
        public int P500
        {
            get { return _p500; }
            set
            {
                _p500 = value;
                if (PropertyChanged != null)
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("P500"));
            }
        }
        public int P200
        {
            get { return _p200; }
            set
            {
                _p200 = value;
                if (PropertyChanged != null)
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("P200"));
            }
        }
        public int P100
        {
            get { return _p100; }
            set
            {
                _p100 = value;
                if (PropertyChanged != null)
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("P100"));
            }
        }
        public decimal P50
        {
            get { return _p50; }
            set
            {
                _p50 = value;
                if (PropertyChanged != null)
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("P50"));
            }
        }
        public decimal P20
        {
            get { return _p20; }
            set
            {
                _p20 = value;
                if (PropertyChanged != null)
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("P20"));
            }
        }
        public decimal P10
        {
            get { return _p10; }
            set
            {
                _p10 = value;
                if (PropertyChanged != null)
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("P10"));
            }
        }
        public decimal P5
        {
            get { return _p5; }
            set
            {
                _p5 = value;
                if (PropertyChanged != null)
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("P5"));
            }
        }
        public decimal P2
        {
            get { return _p2; }
            set
            {
                _p2 = value;
                if (PropertyChanged != null)
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("P2"));
            }
        }
        public decimal P1
        {
            get { return _p1; }
            set
            {
                _p1 = value;
                if (PropertyChanged != null)
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("P1"));
            }
        }

        public string TotalBills { get { return TotalBillValue.GetUniversalCurrencyFormatWithSymbolFormattedToZeroDecimal(); } }
        public string TotalCoins { get { return TotalCoinsValue.GetUniversalCurrencyFormatWithSymbol(); } }
        public string TotalAmount { get { return TotalAmountValue.GetUniversalCurrencyFormatWithSymbol(); } }

        public string TicketsIn { get { return TicketsInValue.GetUniversalCurrencyFormatWithSymbol(); } }
        public string TicketsOut { get { return TicketsOutValue.GetUniversalCurrencyFormatWithSymbol(); } }
        public string EFTIn { get { return EFTInValue.GetUniversalCurrencyFormatWithSymbol(); } }
        public string EFTOut { get { return EFTOutValue.GetUniversalCurrencyFormatWithSymbol(); } }
        public string CoinsOut { get { return CoinOutValue.GetUniversalCurrencyFormatWithSymbol(); } }
        
        public string CoinsIn { get { return CoinInValue.GetUniversalCurrencyFormatWithSymbol(); } }

        public bool CoinsOutVisible
        {
            get { return (Settings.Declaration_ShowoutValues); }
        }

        public decimal EFTInValue
        {
            get { return _EFTIn; }
            set
            {
                _EFTIn = value;
                if (PropertyChanged != null)
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("EFTIn"));
            }
        }

        public decimal EFTOutValue
        {
            get { return _EFTOut; }
            set
            {
                _EFTOut = value;
                if (PropertyChanged != null)
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("EFTOut"));
            }
        }

        public decimal TicketsInValue
        {
            get { return _Tickets_In; }
            set
            {
                _Tickets_In = value;
                if (PropertyChanged != null)
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("TicketsIn"));
            }
        }
        public decimal TicketsOutValue
        {
            get { return _Tickets_Out; }
            set
            {
                _Tickets_Out = value;
                if (PropertyChanged != null)
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("TicketsOut"));
            }
        }
        public decimal CoinOutValue
        {
            get { return _Coin_Out; }
            set
            {
                _Coin_Out = value;
                if (PropertyChanged != null)
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("CoinOut"));
            }
        }

        public decimal CoinInValue
        {
            get { return _Coin_In; }
            set
            {
                _Coin_In = value;
                if (PropertyChanged != null)
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("CoinIn"));
            }
        }

        public decimal AttendantPayValue
        {
            get { return _AttendantPayValue; }
            set
            {
                _AttendantPayValue = value;
                if (PropertyChanged != null)
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("AttendantPay"));
            }
        }

        public string AttendantPayValueString
        {
            get { return AttendantPayValue.GetUniversalCurrencyFormatWithSymbol(); }
        }

        public string WinLoss { get { return WinLossValue.GetUniversalCurrencyFormatWithSymbol(); } }
        public string ShortPay { get { return ShortPayValue.GetUniversalCurrencyFormatWithSymbol(); } }
        public string Refund { get { return RefundValue.GetUniversalCurrencyFormatWithSymbol(); } }
        public string Refills { get { return RefillsValue.GetUniversalCurrencyFormatWithSymbol(); } }

        public decimal JackpotValue
        {
            get { return _Jackpots; }
            set
            {
                _Jackpots = value;
                if (PropertyChanged != null)
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Jackpot"));
            }
        }

        public string Jackpot
        {
            get { return JackpotValue.GetUniversalCurrencyFormatWithSymbol(); }
        }

        public string Handpay
        {
            get { return HandpayValue.GetUniversalCurrencyFormatWithSymbol(); }
        }

        public decimal HandpayValue
        {
            get { return _Handpays; }
            set
            {
                _Handpays = value;
                if (PropertyChanged != null)
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Handpay"));
            }
        }

        public decimal WinLossValue
        {
            get { return _WinLossValue; }
            set
            {
                _WinLossValue = value;
                if (PropertyChanged != null)
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("WinLossValue"));
            }
        }
        public decimal ShortPayValue
        {
            get { return _ShortPayValue; }
            set
            {
                _ShortPayValue = value;
                if (PropertyChanged != null)
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("ShortPayValue"));
            }
        }
        public decimal RefundValue
        {
            get { return _RefundValue; }
            set
            {
                _RefundValue = value;
                if (PropertyChanged != null)
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("RefundValue"));
            }
        }
        public decimal RefillsValue
        {
            get { return _RefillsValue; }
            set
            {
                _RefillsValue = value;
                if (PropertyChanged != null)
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("RefillsValue"));
            }
        }

        public string BackColor;
        public int ImageItemDisplay;

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }

    public class UndeclaredCollection
    {

        private int _Collection_Batch_No;

        private string _Collection_Batch_Name;

        private string _DisplayName;

        private DateTime? _BatchTime;

        public UndeclaredCollection()
        {
        }

        [Column(Storage = "_Collection_Batch_No", DbType = "Int NOT NULL")]
        public int Collection_Batch_No
        {
            get
            {
                return this._Collection_Batch_No;
            }
            set
            {
                if ((this._Collection_Batch_No != value))
                {
                    this._Collection_Batch_No = value;
                }
            }
        }

        [Column(Storage = "_Collection_Batch_Name", DbType = "VarChar(50)")]
        public string Collection_Batch_Name
        {
            get
            {
                return this._Collection_Batch_Name;
            }
            set
            {
                if ((this._Collection_Batch_Name != value))
                {
                    this._Collection_Batch_Name = value ?? "";
                }
            }
        }

        [Column(Storage = "_DisplayName", DbType = "VarChar(50)")]
        public string DisplayName
        {
            get
            {
                return this._DisplayName;
            }
            set
            {
                if ((this._DisplayName != value))
                {
                    this._DisplayName = value ?? "";
                }
            }
        }

        [Column(Storage = "_BatchTime", DbType = "DateTime")]
        public System.Nullable<DateTime> BatchTime
        {
            get
            {
                return this._BatchTime;
            }
            set
            {
                if ((this._BatchTime != value))
                {
                    this._BatchTime = value ?? DateTime.MinValue;
                }
            }
        }
    }

    [DataContract()]
    public partial class RouteCollection
    {

        private int _Route_No;

        private string _Route_Name;

        private System.Nullable<bool> _Route_Default;

        public RouteCollection()
        {
        }

        [Column(Storage = "_Route_No", DbType = "Int NOT NULL")]
        [DataMember(Order = 1)]
        public int Route_No
        {
            get
            {
                return this._Route_No;
            }
            set
            {
                if ((this._Route_No != value))
                {
                    this._Route_No = value;
                }
            }
        }

        [Column(Storage = "_Route_Name", DbType = "VarChar(50)")]
        [DataMember(Order = 2)]
        public string Route_Name
        {
            get
            {
                return this._Route_Name;
            }
            set
            {
                if ((this._Route_Name != value))
                {
                    this._Route_Name = value ?? "";
                }
            }
        }

        [Column(Storage = "_Route_Default", DbType = "BIT")]
        [DataMember(Order = 3)]
        public System.Nullable<bool> Route_Default
        {
            get
            {
                return this._Route_Default;
            }
            set
            {
                if ((this._Route_Default != value))
                {
                    this._Route_Default = value ;
                }
            }
        }
    }

    [DataContract()]
    public partial class BarPositionRouteNo
    {

        private int _Bar_Pos_No;

        public BarPositionRouteNo()
        {
        }

        [Column(Storage = "_Bar_Pos_No", DbType = "Int NOT NULL")]
        [DataMember(Order = 1)]
        public int Bar_Pos_No
        {
            get
            {
                return this._Bar_Pos_No;
            }
            set
            {
                if ((this._Bar_Pos_No != value))
                {
                    this._Bar_Pos_No = value;
                }
            }
        }
    }
    public partial class StockNumber
    {

        private string _Stock_No;

        private string _Batch_Type;

        public StockNumber()
        {
        }

        [Column(Storage = "_Stock_No", DbType = "VarChar(50)")]
        public string Stock_No
        {
            get
            {
                return this._Stock_No;
            }
            set
            {
                if ((this._Stock_No != value))
                {
                    this._Stock_No = value;
                }
            }
        }

        [Column(Storage = "_Batch_Type", DbType = "VarChar(50)")]
        public string Batch_Type
        {
            get
            {
                return this._Batch_Type;
            }
            set
            {
                if ((this._Batch_Type != value))
                {
                    this._Batch_Type = value;
                }
            }
        }
    }

    public partial class InstallationData
    {

        private int _Installation_No;

        private long _AutoDropSessionNo;

        private bool _StackerEventReceived;

        private long _BatchNo;

        private int _UserNo;

        private string _Stock_No;

        private bool _FinalDrop;

        private bool _PartCollection;

        private string _Batch_Machine;

        public InstallationData()
        {
        }

        [Column(Storage = "_Installation_No", DbType = "Int")]
        public int Installation_No
        {
            get
            {
                return this._Installation_No;
            }
            set
            {
                if ((this._Installation_No != value))
                {
                    this._Installation_No = value;
                }
            }
        }

        [Column(Storage = "_AutoDropSessionNo", DbType = "BigInt")]
        public long AutoDropSessionNo
        {
            get
            {
                return this._AutoDropSessionNo;
            }
            set
            {
                if ((this._AutoDropSessionNo != value))
                {
                    this._AutoDropSessionNo = value;
                }
            }
        }

        [Column(Storage="_Batch_Machine", DbType="VarChar(50)")]
        public string Batch_Machine
        {
            get
            {
                return this._Batch_Machine;
            }
            set
            {
                if ((this._Batch_Machine != value))
                {
                    this._Batch_Machine = value;
                }
            }
        }

        [Column(Storage = "_StackerEventReceived", DbType = "Bit")]
        public bool StackerEventReceived
        {
            get
            {
                return this._StackerEventReceived;
            }
            set
            {
                if ((this._StackerEventReceived != value))
                {
                    this._StackerEventReceived = value;
                }
            }
        }

        [Column(Storage = "_BatchNo", DbType = "BigInt")]
        public long BatchNo
        {
            get
            {
                return this._BatchNo;
            }
            set
            {
                if ((this._BatchNo != value))
                {
                    this._BatchNo = value;
                }
            }
        }

        [Column(Storage = "_UserNo", DbType = "Int")]
        public int UserNo
        {
            get
            {
                return this._UserNo;
            }
            set
            {
                if ((this._UserNo != value))
                {
                    this._UserNo = value;
                }
            }
        }

        [Column(Storage = "_Stock_No", DbType = "VarChar(50)")]
        public string Stock_No
        {
            get
            {
                return this._Stock_No;
            }
            set
            {
                if ((this._Stock_No != value))
                {
                    this._Stock_No = value;
                }
            }
        }

        [Column(Storage = "_FinalDrop", DbType = "Bit")]
        public bool FinalDrop
        {
            get
            {
                return this._FinalDrop;
            }
            set
            {
                if ((this._FinalDrop != value))
                {
                    this._FinalDrop = value;
                }
            }
        }

        [Column(Storage = "_PartCollection", DbType = "Bit")]
        public bool PartCollection
        {
            get
            {
                return this._PartCollection;
            }
            set
            {
                if ((this._PartCollection != value))
                {
                    this._PartCollection = value;
                }
            }
        }

    }
    public class FullBatchCollectionData
    {
        public int Number { get; set; }
        public string Route { get; set; }
        public DateTime GamingDate { get; set; }
        public DateTime DateCollected { get; set; }
    }

    public class FullWeekCollectionData
    {
        public int WeekNumber { get; set; }
        public string Dates { get; set; }
        public int NoOfMachineDrops { get; set; }
        public string WinLoss { get; set; }
        public string WinLossVariance { get; set; }
        public string Eft { get; set; }
        public int WeekId { get; set; }
        public int BatchId { get; set; }
    }

    public class PartBatchCollectionData
    {
        public string BarPosition { get; set; }
        public string Machine { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string Cash { get; set; }
        public decimal dCash { get; set; }
        public DateTime DateTime { get; set; }
    }

    public class CashCounterCollectionResult
    {

        private int _Collection_No;

        private string _cType;

        private string _Bar_Pos_Name;

        private string _Name;

        private int _Bill100;

        private int _Bill50;

        private int _Bill20;

        private int _Bill10;

        private int _Bill5;

        private int _Bill2;

        private int _Bill1;

        private int _TotalBills;

        private int _TotalCoins;

        private int _TotalCash;

        public CashCounterCollectionResult()
        {
        }

        [Column(Storage = "_Collection_No", DbType = "Int NOT NULL")]
        public int Collection_No
        {
            get
            {
                return this._Collection_No;
            }
            set
            {
                if ((this._Collection_No != value))
                {
                    this._Collection_No = value;
                }
            }
        }

        [Column(Storage = "_cType", DbType = "VarChar(7) NOT NULL", CanBeNull = false)]
        public string cType
        {
            get
            {
                return this._cType;
            }
            set
            {
                if ((this._cType != value))
                {
                    this._cType = value;
                }
            }
        }

        [Column(Storage = "_Bar_Pos_Name", DbType = "VarChar(50)")]
        public string Bar_Pos_Name
        {
            get
            {
                return this._Bar_Pos_Name;
            }
            set
            {
                if ((this._Bar_Pos_Name != value))
                {
                    this._Bar_Pos_Name = value;
                }
            }
        }

        [Column(Storage = "_Name", DbType = "VarChar(50)")]
        public string Name
        {
            get
            {
                return this._Name;
            }
            set
            {
                if ((this._Name != value))
                {
                    this._Name = value;
                }
            }
        }

        [Column(Storage = "_Bill100", DbType = "Int NOT NULL")]
        public int Bill100
        {
            get
            {
                return this._Bill100;
            }
            set
            {
                if ((this._Bill100 != value))
                {
                    this._Bill100 = value;
                }
            }
        }

        [Column(Storage = "_Bill50", DbType = "Int NOT NULL")]
        public int Bill50
        {
            get
            {
                return this._Bill50;
            }
            set
            {
                if ((this._Bill50 != value))
                {
                    this._Bill50 = value;
                }
            }
        }

        [Column(Storage = "_Bill20", DbType = "Int NOT NULL")]
        public int Bill20
        {
            get
            {
                return this._Bill20;
            }
            set
            {
                if ((this._Bill20 != value))
                {
                    this._Bill20 = value;
                }
            }
        }

        [Column(Storage = "_Bill10", DbType = "Int NOT NULL")]
        public int Bill10
        {
            get
            {
                return this._Bill10;
            }
            set
            {
                if ((this._Bill10 != value))
                {
                    this._Bill10 = value;
                }
            }
        }

        [Column(Storage = "_Bill5", DbType = "Int NOT NULL")]
        public int Bill5
        {
            get
            {
                return this._Bill5;
            }
            set
            {
                if ((this._Bill5 != value))
                {
                    this._Bill5 = value;
                }
            }
        }

        [Column(Storage = "_Bill2", DbType = "Int NOT NULL")]
        public int Bill2
        {
            get
            {
                return this._Bill2;
            }
            set
            {
                if ((this._Bill2 != value))
                {
                    this._Bill2 = value;
                }
            }
        }

        [Column(Storage = "_Bill1", DbType = "Int NOT NULL")]
        public int Bill1
        {
            get
            {
                return this._Bill1;
            }
            set
            {
                if ((this._Bill1 != value))
                {
                    this._Bill1 = value;
                }
            }
        }

        [Column(Storage = "_TotalBills", DbType = "Int NOT NULL")]
        public int TotalBills
        {
            get
            {
                return this._TotalBills;
            }
            set
            {
                if ((this._TotalBills != value))
                {
                    this._TotalBills = value;
                }
            }
        }

        [Column(Storage = "_TotalCoins", DbType = "Int NOT NULL")]
        public int TotalCoins
        {
            get
            {
                return this._TotalCoins;
            }
            set
            {
                if ((this._TotalCoins != value))
                {
                    this._TotalCoins = value;
                }
            }
        }

        [Column(Storage = "_TotalCash", DbType = "Int NOT NULL")]
        public int TotalCash
        {
            get
            {
                return this._TotalCash;
            }
            set
            {
                if ((this._TotalCash != value))
                {
                    this._TotalCash = value;
                }
            }
        }
    }

    public enum DeclarationFilterBy
    {
        None = 0,
        Position = 1,
        GameTitle = 2,
        Type = 3
    }

    public class DeclarationFilterColumn
    {
        public DeclarationFilterColumn() { }

        public string DisplayName { get; set; }

        public DeclarationFilterBy ValueName { get; set; }
    }

    public class SiteConfig
    {
        public string SiteCode { get; set; }
        public string ExchangeConnectionString { get; set; }
        public string TicketConnectionString { get; set; }
        public string SiteName { get; set; }
    }

    public partial class GetDropAlertDataResult
    {

        private string _Source;

        private string _BMCVersion;

        private string _ExceptionCode;

        private string _OperatorId;

        private string _SubCode;

        private string _Company;

        private string _Region;

        private string _Area;

        private string _SiteId;

        private string _SiteName;

        private string _Asset;

        private string _Stand;

        private string _DropType;

        private System.Nullable<System.DateTime> _DropDate;

        private string _DropPositionsList;

        private string _DropScheduleId;

        private int _BatchNumber;

        private string _EmployeeCardNumber;

        private string _EmployeeName;

        private System.Nullable<System.DateTime> _MessageDateTime;

        public GetDropAlertDataResult()
        {
        }

        [Column(Storage = "_Source", DbType = "VarChar(4) NOT NULL", CanBeNull = false)]
        public string Source
        {
            get
            {
                return this._Source;
            }
            set
            {
                if ((this._Source != value))
                {
                    this._Source = value;
                }
            }
        }

        [Column(Storage = "_BMCVersion", DbType = "VarChar(20)")]
        public string BMCVersion
        {
            get
            {
                return this._BMCVersion;
            }
            set
            {
                if ((this._BMCVersion != value))
                {
                    this._BMCVersion = value;
                }
            }
        }

        [Column(Storage = "_ExceptionCode", DbType = "VarChar(3) NOT NULL", CanBeNull = false)]
        public string ExceptionCode
        {
            get
            {
                return this._ExceptionCode;
            }
            set
            {
                if ((this._ExceptionCode != value))
                {
                    this._ExceptionCode = value;
                }
            }
        }

        [Column(Storage = "_OperatorId", DbType = "VarChar(3) NOT NULL", CanBeNull = false)]
        public string OperatorId
        {
            get
            {
                return this._OperatorId;
            }
            set
            {
                if ((this._OperatorId != value))
                {
                    this._OperatorId = value;
                }
            }
        }

        [Column(Storage = "_SubCode", DbType = "NVarChar(20)")]
        public string SubCode
        {
            get
            {
                return this._SubCode;
            }
            set
            {
                if ((this._SubCode != value))
                {
                    this._SubCode = value;
                }
            }
        }

        [Column(Storage = "_Company", DbType = "NVarChar(20)")]
        public string Company
        {
            get
            {
                return this._Company;
            }
            set
            {
                if ((this._Company != value))
                {
                    this._Company = value;
                }
            }
        }

        [Column(Storage = "_Region", DbType = "VarChar(10)")]
        public string Region
        {
            get
            {
                return this._Region;
            }
            set
            {
                if ((this._Region != value))
                {
                    this._Region = value;
                }
            }
        }

        [Column(Storage = "_Area", DbType = "NVarChar(20)")]
        public string Area
        {
            get
            {
                return this._Area;
            }
            set
            {
                if ((this._Area != value))
                {
                    this._Area = value;
                }
            }
        }

        [Column(Storage = "_SiteId", DbType = "VarChar(50)")]
        public string SiteId
        {
            get
            {
                return this._SiteId;
            }
            set
            {
                if ((this._SiteId != value))
                {
                    this._SiteId = value;
                }
            }
        }

        [Column(Storage = "_SiteName", DbType = "VarChar(50)")]
        public string SiteName
        {
            get
            {
                return this._SiteName;
            }
            set
            {
                if ((this._SiteName != value))
                {
                    this._SiteName = value;
                }
            }
        }

        [Column(Storage = "_Asset", DbType = "NVarChar(20)")]
        public string Asset
        {
            get
            {
                return this._Asset;
            }
            set
            {
                if ((this._Asset != value))
                {
                    this._Asset = value;
                }
            }
        }

        [Column(Storage = "_Stand", DbType = "NVarChar(20)")]
        public string Stand
        {
            get
            {
                return this._Stand;
            }
            set
            {
                if ((this._Stand != value))
                {
                    this._Stand = value;
                }
            }
        }

        [Column(Storage = "_DropType", DbType = "NVarChar(20)")]
        public string DropType
        {
            get
            {
                return this._DropType;
            }
            set
            {
                if ((this._DropType != value))
                {
                    this._DropType = value;
                }
            }
        }

        [Column(Storage = "_DropDate", DbType = "DateTime")]
        public System.Nullable<System.DateTime> DropDate
        {
            get
            {
                return this._DropDate;
            }
            set
            {
                if ((this._DropDate != value))
                {
                    this._DropDate = value;
                }
            }
        }

        [Column(Storage = "_DropPositionsList", DbType = "NVarChar(2000)")]
        public string DropPositionsList
        {
            get
            {
                return this._DropPositionsList;
            }
            set
            {
                if ((this._DropPositionsList != value))
                {
                    this._DropPositionsList = value;
                }
            }
        }

        [Column(Storage = "_DropScheduleId", DbType = "NVarChar(20)")]
        public string DropScheduleId
        {
            get
            {
                return this._DropScheduleId;
            }
            set
            {
                if ((this._DropScheduleId != value))
                {
                    this._DropScheduleId = value;
                }
            }
        }

        [Column(Storage = "_BatchNumber", DbType = "Int NOT NULL")]
        public int BatchNumber
        {
            get
            {
                return this._BatchNumber;
            }
            set
            {
                if ((this._BatchNumber != value))
                {
                    this._BatchNumber = value;
                }
            }
        }

        [Column(Storage = "_EmployeeCardNumber", DbType = "NVarChar(20)")]
        public string EmployeeCardNumber
        {
            get
            {
                return this._EmployeeCardNumber;
            }
            set
            {
                if ((this._EmployeeCardNumber != value))
                {
                    this._EmployeeCardNumber = value;
                }
            }
        }

        [Column(Storage = "_EmployeeName", DbType = "VarChar(101)")]
        public string EmployeeName
        {
            get
            {
                return this._EmployeeName;
            }
            set
            {
                if ((this._EmployeeName != value))
                {
                    this._EmployeeName = value;
                }
            }
        }

        [Column(Storage = "_MessageDateTime", DbType = "DateTime")]
        public System.Nullable<System.DateTime> MessageDateTime
        {
            get
            {
                return this._MessageDateTime;
            }
            set
            {
                if ((this._MessageDateTime != value))
                {
                    this._MessageDateTime = value;
                }
            }
        }
    }



    public partial class GetDropAlertDataResult
    {
        private static string rootNode = "BMCRequest";

        public override string ToString()
        {
            StringBuilder build = new StringBuilder(string.Empty);

            foreach (var prop in base.GetType().GetProperties())
            {
                if (!prop.Name.ToLower().Contains("date"))
                {
                    build.Append(String.Format("<{0}>{1}</{0}>\r\n", prop.Name, prop.GetValue(this, null)));
                }
                else
                {
                    if (prop.GetValue(this, null) == null)
                    {
                        build.Append(String.Format("<{0}>{1}</{0}>\r\n", prop.Name, ""));
                    }
                    else
                    {
                        build.Append(String.Format("<{0}>{1}</{0}>\r\n", prop.Name, Convert.ToDateTime(prop.GetValue(this, null)).ToString("yyyy-MM-dd HH:mm:ss")));
                    }
                }
            }
            return String.Format("<{0}>\r\n{1}</{0}>", rootNode, build.ToString());
        }

        public XElement GetXml()
        {
            //StringBuilder build = new StringBuilder(string.Empty);
            XElement xml_stack = new XElement(rootNode);
            foreach (var prop in base.GetType().GetProperties())
            {
                if (!prop.Name.ToLower().Contains("date"))
                {
                    //build.Append(String.Format("<{0}>{1}</{0}>\r\n", prop.Name, prop.GetValue(this, null)));
                    xml_stack.Add(new XElement(prop.Name, prop.GetValue(this, null)));
                }
                else
                {
                    if (prop.GetValue(this, null) == null)
                    {
                        // build.Append(String.Format("<{0}>{1}</{0}>\r\n", prop.Name, ""));
                        xml_stack.Add(new XElement(prop.Name, ""));
                    }
                    else
                    {
                        //build.Append(String.Format("<{0}>{1}</{0}>\r\n", prop.Name, Convert.ToDateTime(prop.GetValue(this, null)).ToString("yyyy-MM-dd HH:mm:ss")));
                        xml_stack.Add(new XElement(prop.Name, Convert.ToDateTime(prop.GetValue(this, null)).ToString("yyyy-MM-dd HH:mm:ss")));
                    }
                }
            }
            return xml_stack;
        }

    }

}
