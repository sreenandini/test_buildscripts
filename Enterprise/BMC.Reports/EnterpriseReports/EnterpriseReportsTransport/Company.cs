using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Data;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Linq.Expressions;
using System.ComponentModel;
using System;

namespace BMC.EnterpriseReportsTransport
{
    [Table(Name = "dbo.Company")]
    public partial  class Company : INotifyPropertyChanging, INotifyPropertyChanged
    {

        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);

        private int _Company_ID;

        private System.Nullable<int> _Staff_ID;

        private System.Nullable<int> _Terms_Group_ID;

        private System.Nullable<int> _Access_Key_ID;

        private System.Nullable<int> _Company_Model_Set_ID;

        private string _Company_Name;

        private string _Company_Address;

        private string _Company_Postcode;

        private string _Company_Switchboard_Phone_No;

        private string _Company_Contact_Name;

        private string _Company_Contact_Phone_No;

        private string _Company_Contact_Email_Address;

        private string _Company_Invoice_Address;

        private string _Company_Invoice_Postcode;

        private string _Company_Invoice_Name;

        private string _Company_Price_Per_Play;

        private string _Company_Jackpot;

        private string _Company_Percentage_Payout;

        private string _Company_Start_Date;

        private string _Company_End_Date;

        private string _Company_Memo;

        private string _Company_AMEDIS_Code;

        private string _Company_AMEDIS_Operational_Code;

        private string _Company_ANA_Number;

        private System.Nullable<bool> _Company_AutoCreate_Position;

        private string _Company_Logo_Reference;

        private System.Nullable<char> _Company_Trade_Type;

        private string _Company_Address_1;

        private string _Company_Address_2;

        private string _Company_Address_3;

        private string _Company_Address_4;

        private string _Company_Address_5;

        private System.Nullable<bool> _Company_TX_Collection;

        private System.Nullable<bool> _Company_TX_Movement;

        private System.Nullable<bool> _Company_TX_EDC;

        private System.Nullable<int> _Company_TX_Format;

        private System.Nullable<bool> _Company_RX_Collection;

        private System.Nullable<bool> _Company_RX_Movement;

        private System.Nullable<bool> _Company_RX_EDC;

        private System.Nullable<int> _Company_RX_Format;

        private System.Nullable<bool> _Company_AMEDIS_Variants;

        #region Extensibility Method Definitions
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
        partial void OnCompany_IDChanging(int value);
        partial void OnCompany_IDChanged();
        partial void OnStaff_IDChanging(System.Nullable<int> value);
        partial void OnStaff_IDChanged();
        partial void OnTerms_Group_IDChanging(System.Nullable<int> value);
        partial void OnTerms_Group_IDChanged();
        partial void OnAccess_Key_IDChanging(System.Nullable<int> value);
        partial void OnAccess_Key_IDChanged();
        partial void OnCompany_Model_Set_IDChanging(System.Nullable<int> value);
        partial void OnCompany_Model_Set_IDChanged();
        partial void OnCompany_NameChanging(string value);
        partial void OnCompany_NameChanged();
        partial void OnCompany_AddressChanging(string value);
        partial void OnCompany_AddressChanged();
        partial void OnCompany_PostcodeChanging(string value);
        partial void OnCompany_PostcodeChanged();
        partial void OnCompany_Switchboard_Phone_NoChanging(string value);
        partial void OnCompany_Switchboard_Phone_NoChanged();
        partial void OnCompany_Contact_NameChanging(string value);
        partial void OnCompany_Contact_NameChanged();
        partial void OnCompany_Contact_Phone_NoChanging(string value);
        partial void OnCompany_Contact_Phone_NoChanged();
        partial void OnCompany_Contact_Email_AddressChanging(string value);
        partial void OnCompany_Contact_Email_AddressChanged();
        partial void OnCompany_Invoice_AddressChanging(string value);
        partial void OnCompany_Invoice_AddressChanged();
        partial void OnCompany_Invoice_PostcodeChanging(string value);
        partial void OnCompany_Invoice_PostcodeChanged();
        partial void OnCompany_Invoice_NameChanging(string value);
        partial void OnCompany_Invoice_NameChanged();
        partial void OnCompany_Price_Per_PlayChanging(string value);
        partial void OnCompany_Price_Per_PlayChanged();
        partial void OnCompany_JackpotChanging(string value);
        partial void OnCompany_JackpotChanged();
        partial void OnCompany_Percentage_PayoutChanging(string value);
        partial void OnCompany_Percentage_PayoutChanged();
        partial void OnCompany_Start_DateChanging(string value);
        partial void OnCompany_Start_DateChanged();
        partial void OnCompany_End_DateChanging(string value);
        partial void OnCompany_End_DateChanged();
        partial void OnCompany_MemoChanging(string value);
        partial void OnCompany_MemoChanged();
        partial void OnCompany_AMEDIS_CodeChanging(string value);
        partial void OnCompany_AMEDIS_CodeChanged();
        partial void OnCompany_AMEDIS_Operational_CodeChanging(string value);
        partial void OnCompany_AMEDIS_Operational_CodeChanged();
        partial void OnCompany_ANA_NumberChanging(string value);
        partial void OnCompany_ANA_NumberChanged();
        partial void OnCompany_AutoCreate_PositionChanging(System.Nullable<bool> value);
        partial void OnCompany_AutoCreate_PositionChanged();
        partial void OnCompany_Logo_ReferenceChanging(string value);
        partial void OnCompany_Logo_ReferenceChanged();
        partial void OnCompany_Trade_TypeChanging(System.Nullable<char> value);
        partial void OnCompany_Trade_TypeChanged();
        partial void OnCompany_Address_1Changing(string value);
        partial void OnCompany_Address_1Changed();
        partial void OnCompany_Address_2Changing(string value);
        partial void OnCompany_Address_2Changed();
        partial void OnCompany_Address_3Changing(string value);
        partial void OnCompany_Address_3Changed();
        partial void OnCompany_Address_4Changing(string value);
        partial void OnCompany_Address_4Changed();
        partial void OnCompany_Address_5Changing(string value);
        partial void OnCompany_Address_5Changed();
        partial void OnCompany_TX_CollectionChanging(System.Nullable<bool> value);
        partial void OnCompany_TX_CollectionChanged();
        partial void OnCompany_TX_MovementChanging(System.Nullable<bool> value);
        partial void OnCompany_TX_MovementChanged();
        partial void OnCompany_TX_EDCChanging(System.Nullable<bool> value);
        partial void OnCompany_TX_EDCChanged();
        partial void OnCompany_TX_FormatChanging(System.Nullable<int> value);
        partial void OnCompany_TX_FormatChanged();
        partial void OnCompany_RX_CollectionChanging(System.Nullable<bool> value);
        partial void OnCompany_RX_CollectionChanged();
        partial void OnCompany_RX_MovementChanging(System.Nullable<bool> value);
        partial void OnCompany_RX_MovementChanged();
        partial void OnCompany_RX_EDCChanging(System.Nullable<bool> value);
        partial void OnCompany_RX_EDCChanged();
        partial void OnCompany_RX_FormatChanging(System.Nullable<int> value);
        partial void OnCompany_RX_FormatChanged();
        partial void OnCompany_AMEDIS_VariantsChanging(System.Nullable<bool> value);
        partial void OnCompany_AMEDIS_VariantsChanged();
        #endregion

        public Company()
        {
            OnCreated();
        }

        [Column(Storage = "_Company_ID", AutoSync = AutoSync.OnInsert, DbType = "Int NOT NULL IDENTITY", IsPrimaryKey = true, IsDbGenerated = true)]
        public int Company_ID
        {
            get
            {
                return this._Company_ID;
            }
            set
            {
                if ((this._Company_ID != value))
                {
                    this.OnCompany_IDChanging(value);
                    this.SendPropertyChanging();
                    this._Company_ID = value;
                    this.SendPropertyChanged("Company_ID");
                    this.OnCompany_IDChanged();
                }
            }
        }

        [Column(Storage = "_Staff_ID", DbType = "Int")]
        public System.Nullable<int> Staff_ID
        {
            get
            {
                return this._Staff_ID;
            }
            set
            {
                if ((this._Staff_ID != value))
                {
                    this.OnStaff_IDChanging(value);
                    this.SendPropertyChanging();
                    this._Staff_ID = value;
                    this.SendPropertyChanged("Staff_ID");
                    this.OnStaff_IDChanged();
                }
            }
        }

        [Column(Storage = "_Terms_Group_ID", DbType = "Int")]
        public System.Nullable<int> Terms_Group_ID
        {
            get
            {
                return this._Terms_Group_ID;
            }
            set
            {
                if ((this._Terms_Group_ID != value))
                {
                    this.OnTerms_Group_IDChanging(value);
                    this.SendPropertyChanging();
                    this._Terms_Group_ID = value;
                    this.SendPropertyChanged("Terms_Group_ID");
                    this.OnTerms_Group_IDChanged();
                }
            }
        }

        [Column(Storage = "_Access_Key_ID", DbType = "Int")]
        public System.Nullable<int> Access_Key_ID
        {
            get
            {
                return this._Access_Key_ID;
            }
            set
            {
                if ((this._Access_Key_ID != value))
                {
                    this.OnAccess_Key_IDChanging(value);
                    this.SendPropertyChanging();
                    this._Access_Key_ID = value;
                    this.SendPropertyChanged("Access_Key_ID");
                    this.OnAccess_Key_IDChanged();
                }
            }
        }

        [Column(Storage = "_Company_Model_Set_ID", DbType = "Int")]
        public System.Nullable<int> Company_Model_Set_ID
        {
            get
            {
                return this._Company_Model_Set_ID;
            }
            set
            {
                if ((this._Company_Model_Set_ID != value))
                {
                    this.OnCompany_Model_Set_IDChanging(value);
                    this.SendPropertyChanging();
                    this._Company_Model_Set_ID = value;
                    this.SendPropertyChanged("Company_Model_Set_ID");
                    this.OnCompany_Model_Set_IDChanged();
                }
            }
        }

        [Column(Storage = "_Company_Name", DbType = "VarChar(50)")]
        public string Company_Name
        {
            get
            {
                return this._Company_Name;
            }
            set
            {
                if ((this._Company_Name != value))
                {
                    this.OnCompany_NameChanging(value);
                    this.SendPropertyChanging();
                    this._Company_Name = value;
                    this.SendPropertyChanged("Company_Name");
                    this.OnCompany_NameChanged();
                }
            }
        }

        [Column(Storage = "_Company_Address", DbType = "NText", UpdateCheck = UpdateCheck.Never)]
        public string Company_Address
        {
            get
            {
                return this._Company_Address;
            }
            set
            {
                if ((this._Company_Address != value))
                {
                    this.OnCompany_AddressChanging(value);
                    this.SendPropertyChanging();
                    this._Company_Address = value;
                    this.SendPropertyChanged("Company_Address");
                    this.OnCompany_AddressChanged();
                }
            }
        }

        [Column(Storage = "_Company_Postcode", DbType = "VarChar(10)")]
        public string Company_Postcode
        {
            get
            {
                return this._Company_Postcode;
            }
            set
            {
                if ((this._Company_Postcode != value))
                {
                    this.OnCompany_PostcodeChanging(value);
                    this.SendPropertyChanging();
                    this._Company_Postcode = value;
                    this.SendPropertyChanged("Company_Postcode");
                    this.OnCompany_PostcodeChanged();
                }
            }
        }

        [Column(Storage = "_Company_Switchboard_Phone_No", DbType = "VarChar(15)")]
        public string Company_Switchboard_Phone_No
        {
            get
            {
                return this._Company_Switchboard_Phone_No;
            }
            set
            {
                if ((this._Company_Switchboard_Phone_No != value))
                {
                    this.OnCompany_Switchboard_Phone_NoChanging(value);
                    this.SendPropertyChanging();
                    this._Company_Switchboard_Phone_No = value;
                    this.SendPropertyChanged("Company_Switchboard_Phone_No");
                    this.OnCompany_Switchboard_Phone_NoChanged();
                }
            }
        }

        [Column(Storage = "_Company_Contact_Name", DbType = "VarChar(50)")]
        public string Company_Contact_Name
        {
            get
            {
                return this._Company_Contact_Name;
            }
            set
            {
                if ((this._Company_Contact_Name != value))
                {
                    this.OnCompany_Contact_NameChanging(value);
                    this.SendPropertyChanging();
                    this._Company_Contact_Name = value;
                    this.SendPropertyChanged("Company_Contact_Name");
                    this.OnCompany_Contact_NameChanged();
                }
            }
        }

        [Column(Storage = "_Company_Contact_Phone_No", DbType = "VarChar(15)")]
        public string Company_Contact_Phone_No
        {
            get
            {
                return this._Company_Contact_Phone_No;
            }
            set
            {
                if ((this._Company_Contact_Phone_No != value))
                {
                    this.OnCompany_Contact_Phone_NoChanging(value);
                    this.SendPropertyChanging();
                    this._Company_Contact_Phone_No = value;
                    this.SendPropertyChanged("Company_Contact_Phone_No");
                    this.OnCompany_Contact_Phone_NoChanged();
                }
            }
        }

        [Column(Storage = "_Company_Contact_Email_Address", DbType = "VarChar(50)")]
        public string Company_Contact_Email_Address
        {
            get
            {
                return this._Company_Contact_Email_Address;
            }
            set
            {
                if ((this._Company_Contact_Email_Address != value))
                {
                    this.OnCompany_Contact_Email_AddressChanging(value);
                    this.SendPropertyChanging();
                    this._Company_Contact_Email_Address = value;
                    this.SendPropertyChanged("Company_Contact_Email_Address");
                    this.OnCompany_Contact_Email_AddressChanged();
                }
            }
        }

        [Column(Storage = "_Company_Invoice_Address", DbType = "NText", UpdateCheck = UpdateCheck.Never)]
        public string Company_Invoice_Address
        {
            get
            {
                return this._Company_Invoice_Address;
            }
            set
            {
                if ((this._Company_Invoice_Address != value))
                {
                    this.OnCompany_Invoice_AddressChanging(value);
                    this.SendPropertyChanging();
                    this._Company_Invoice_Address = value;
                    this.SendPropertyChanged("Company_Invoice_Address");
                    this.OnCompany_Invoice_AddressChanged();
                }
            }
        }

        [Column(Storage = "_Company_Invoice_Postcode", DbType = "VarChar(15)")]
        public string Company_Invoice_Postcode
        {
            get
            {
                return this._Company_Invoice_Postcode;
            }
            set
            {
                if ((this._Company_Invoice_Postcode != value))
                {
                    this.OnCompany_Invoice_PostcodeChanging(value);
                    this.SendPropertyChanging();
                    this._Company_Invoice_Postcode = value;
                    this.SendPropertyChanged("Company_Invoice_Postcode");
                    this.OnCompany_Invoice_PostcodeChanged();
                }
            }
        }

        [Column(Storage = "_Company_Invoice_Name", DbType = "VarChar(50)")]
        public string Company_Invoice_Name
        {
            get
            {
                return this._Company_Invoice_Name;
            }
            set
            {
                if ((this._Company_Invoice_Name != value))
                {
                    this.OnCompany_Invoice_NameChanging(value);
                    this.SendPropertyChanging();
                    this._Company_Invoice_Name = value;
                    this.SendPropertyChanged("Company_Invoice_Name");
                    this.OnCompany_Invoice_NameChanged();
                }
            }
        }

        [Column(Storage = "_Company_Price_Per_Play", DbType = "VarChar(50)")]
        public string Company_Price_Per_Play
        {
            get
            {
                return this._Company_Price_Per_Play;
            }
            set
            {
                if ((this._Company_Price_Per_Play != value))
                {
                    this.OnCompany_Price_Per_PlayChanging(value);
                    this.SendPropertyChanging();
                    this._Company_Price_Per_Play = value;
                    this.SendPropertyChanged("Company_Price_Per_Play");
                    this.OnCompany_Price_Per_PlayChanged();
                }
            }
        }

        [Column(Storage = "_Company_Jackpot", DbType = "VarChar(10)")]
        public string Company_Jackpot
        {
            get
            {
                return this._Company_Jackpot;
            }
            set
            {
                if ((this._Company_Jackpot != value))
                {
                    this.OnCompany_JackpotChanging(value);
                    this.SendPropertyChanging();
                    this._Company_Jackpot = value;
                    this.SendPropertyChanged("Company_Jackpot");
                    this.OnCompany_JackpotChanged();
                }
            }
        }

        [Column(Storage = "_Company_Percentage_Payout", DbType = "VarChar(50)")]
        public string Company_Percentage_Payout
        {
            get
            {
                return this._Company_Percentage_Payout;
            }
            set
            {
                if ((this._Company_Percentage_Payout != value))
                {
                    this.OnCompany_Percentage_PayoutChanging(value);
                    this.SendPropertyChanging();
                    this._Company_Percentage_Payout = value;
                    this.SendPropertyChanged("Company_Percentage_Payout");
                    this.OnCompany_Percentage_PayoutChanged();
                }
            }
        }

        [Column(Storage = "_Company_Start_Date", DbType = "VarChar(30)")]
        public string Company_Start_Date
        {
            get
            {
                return this._Company_Start_Date;
            }
            set
            {
                if ((this._Company_Start_Date != value))
                {
                    this.OnCompany_Start_DateChanging(value);
                    this.SendPropertyChanging();
                    this._Company_Start_Date = value;
                    this.SendPropertyChanged("Company_Start_Date");
                    this.OnCompany_Start_DateChanged();
                }
            }
        }

        [Column(Storage = "_Company_End_Date", DbType = "VarChar(30)")]
        public string Company_End_Date
        {
            get
            {
                return this._Company_End_Date;
            }
            set
            {
                if ((this._Company_End_Date != value))
                {
                    this.OnCompany_End_DateChanging(value);
                    this.SendPropertyChanging();
                    this._Company_End_Date = value;
                    this.SendPropertyChanged("Company_End_Date");
                    this.OnCompany_End_DateChanged();
                }
            }
        }

        [Column(Storage = "_Company_Memo", DbType = "NText", UpdateCheck = UpdateCheck.Never)]
        public string Company_Memo
        {
            get
            {
                return this._Company_Memo;
            }
            set
            {
                if ((this._Company_Memo != value))
                {
                    this.OnCompany_MemoChanging(value);
                    this.SendPropertyChanging();
                    this._Company_Memo = value;
                    this.SendPropertyChanged("Company_Memo");
                    this.OnCompany_MemoChanged();
                }
            }
        }

        [Column(Storage = "_Company_AMEDIS_Code", DbType = "VarChar(4)")]
        public string Company_AMEDIS_Code
        {
            get
            {
                return this._Company_AMEDIS_Code;
            }
            set
            {
                if ((this._Company_AMEDIS_Code != value))
                {
                    this.OnCompany_AMEDIS_CodeChanging(value);
                    this.SendPropertyChanging();
                    this._Company_AMEDIS_Code = value;
                    this.SendPropertyChanged("Company_AMEDIS_Code");
                    this.OnCompany_AMEDIS_CodeChanged();
                }
            }
        }

        [Column(Storage = "_Company_AMEDIS_Operational_Code", DbType = "VarChar(4)")]
        public string Company_AMEDIS_Operational_Code
        {
            get
            {
                return this._Company_AMEDIS_Operational_Code;
            }
            set
            {
                if ((this._Company_AMEDIS_Operational_Code != value))
                {
                    this.OnCompany_AMEDIS_Operational_CodeChanging(value);
                    this.SendPropertyChanging();
                    this._Company_AMEDIS_Operational_Code = value;
                    this.SendPropertyChanged("Company_AMEDIS_Operational_Code");
                    this.OnCompany_AMEDIS_Operational_CodeChanged();
                }
            }
        }

        [Column(Storage = "_Company_ANA_Number", DbType = "VarChar(20)")]
        public string Company_ANA_Number
        {
            get
            {
                return this._Company_ANA_Number;
            }
            set
            {
                if ((this._Company_ANA_Number != value))
                {
                    this.OnCompany_ANA_NumberChanging(value);
                    this.SendPropertyChanging();
                    this._Company_ANA_Number = value;
                    this.SendPropertyChanged("Company_ANA_Number");
                    this.OnCompany_ANA_NumberChanged();
                }
            }
        }

        [Column(Storage = "_Company_AutoCreate_Position", DbType = "Bit")]
        public System.Nullable<bool> Company_AutoCreate_Position
        {
            get
            {
                return this._Company_AutoCreate_Position;
            }
            set
            {
                if ((this._Company_AutoCreate_Position != value))
                {
                    this.OnCompany_AutoCreate_PositionChanging(value);
                    this.SendPropertyChanging();
                    this._Company_AutoCreate_Position = value;
                    this.SendPropertyChanged("Company_AutoCreate_Position");
                    this.OnCompany_AutoCreate_PositionChanged();
                }
            }
        }

        [Column(Storage = "_Company_Logo_Reference", DbType = "VarChar(50)")]
        public string Company_Logo_Reference
        {
            get
            {
                return this._Company_Logo_Reference;
            }
            set
            {
                if ((this._Company_Logo_Reference != value))
                {
                    this.OnCompany_Logo_ReferenceChanging(value);
                    this.SendPropertyChanging();
                    this._Company_Logo_Reference = value;
                    this.SendPropertyChanged("Company_Logo_Reference");
                    this.OnCompany_Logo_ReferenceChanged();
                }
            }
        }

        [Column(Storage = "_Company_Trade_Type", DbType = "VarChar(1)")]
        public System.Nullable<char> Company_Trade_Type
        {
            get
            {
                return this._Company_Trade_Type;
            }
            set
            {
                if ((this._Company_Trade_Type != value))
                {
                    this.OnCompany_Trade_TypeChanging(value);
                    this.SendPropertyChanging();
                    this._Company_Trade_Type = value;
                    this.SendPropertyChanged("Company_Trade_Type");
                    this.OnCompany_Trade_TypeChanged();
                }
            }
        }

        [Column(Storage = "_Company_Address_1", DbType = "VarChar(50)")]
        public string Company_Address_1
        {
            get
            {
                return this._Company_Address_1;
            }
            set
            {
                if ((this._Company_Address_1 != value))
                {
                    this.OnCompany_Address_1Changing(value);
                    this.SendPropertyChanging();
                    this._Company_Address_1 = value;
                    this.SendPropertyChanged("Company_Address_1");
                    this.OnCompany_Address_1Changed();
                }
            }
        }

        [Column(Storage = "_Company_Address_2", DbType = "VarChar(50)")]
        public string Company_Address_2
        {
            get
            {
                return this._Company_Address_2;
            }
            set
            {
                if ((this._Company_Address_2 != value))
                {
                    this.OnCompany_Address_2Changing(value);
                    this.SendPropertyChanging();
                    this._Company_Address_2 = value;
                    this.SendPropertyChanged("Company_Address_2");
                    this.OnCompany_Address_2Changed();
                }
            }
        }

        [Column(Storage = "_Company_Address_3", DbType = "VarChar(50)")]
        public string Company_Address_3
        {
            get
            {
                return this._Company_Address_3;
            }
            set
            {
                if ((this._Company_Address_3 != value))
                {
                    this.OnCompany_Address_3Changing(value);
                    this.SendPropertyChanging();
                    this._Company_Address_3 = value;
                    this.SendPropertyChanged("Company_Address_3");
                    this.OnCompany_Address_3Changed();
                }
            }
        }

        [Column(Storage = "_Company_Address_4", DbType = "VarChar(50)")]
        public string Company_Address_4
        {
            get
            {
                return this._Company_Address_4;
            }
            set
            {
                if ((this._Company_Address_4 != value))
                {
                    this.OnCompany_Address_4Changing(value);
                    this.SendPropertyChanging();
                    this._Company_Address_4 = value;
                    this.SendPropertyChanged("Company_Address_4");
                    this.OnCompany_Address_4Changed();
                }
            }
        }

        [Column(Storage = "_Company_Address_5", DbType = "VarChar(50)")]
        public string Company_Address_5
        {
            get
            {
                return this._Company_Address_5;
            }
            set
            {
                if ((this._Company_Address_5 != value))
                {
                    this.OnCompany_Address_5Changing(value);
                    this.SendPropertyChanging();
                    this._Company_Address_5 = value;
                    this.SendPropertyChanged("Company_Address_5");
                    this.OnCompany_Address_5Changed();
                }
            }
        }

        [Column(Storage = "_Company_TX_Collection", DbType = "Bit")]
        public System.Nullable<bool> Company_TX_Collection
        {
            get
            {
                return this._Company_TX_Collection;
            }
            set
            {
                if ((this._Company_TX_Collection != value))
                {
                    this.OnCompany_TX_CollectionChanging(value);
                    this.SendPropertyChanging();
                    this._Company_TX_Collection = value;
                    this.SendPropertyChanged("Company_TX_Collection");
                    this.OnCompany_TX_CollectionChanged();
                }
            }
        }

        [Column(Storage = "_Company_TX_Movement", DbType = "Bit")]
        public System.Nullable<bool> Company_TX_Movement
        {
            get
            {
                return this._Company_TX_Movement;
            }
            set
            {
                if ((this._Company_TX_Movement != value))
                {
                    this.OnCompany_TX_MovementChanging(value);
                    this.SendPropertyChanging();
                    this._Company_TX_Movement = value;
                    this.SendPropertyChanged("Company_TX_Movement");
                    this.OnCompany_TX_MovementChanged();
                }
            }
        }

        [Column(Storage = "_Company_TX_EDC", DbType = "Bit")]
        public System.Nullable<bool> Company_TX_EDC
        {
            get
            {
                return this._Company_TX_EDC;
            }
            set
            {
                if ((this._Company_TX_EDC != value))
                {
                    this.OnCompany_TX_EDCChanging(value);
                    this.SendPropertyChanging();
                    this._Company_TX_EDC = value;
                    this.SendPropertyChanged("Company_TX_EDC");
                    this.OnCompany_TX_EDCChanged();
                }
            }
        }

        [Column(Storage = "_Company_TX_Format", DbType = "Int")]
        public System.Nullable<int> Company_TX_Format
        {
            get
            {
                return this._Company_TX_Format;
            }
            set
            {
                if ((this._Company_TX_Format != value))
                {
                    this.OnCompany_TX_FormatChanging(value);
                    this.SendPropertyChanging();
                    this._Company_TX_Format = value;
                    this.SendPropertyChanged("Company_TX_Format");
                    this.OnCompany_TX_FormatChanged();
                }
            }
        }

        [Column(Storage = "_Company_RX_Collection", DbType = "Bit")]
        public System.Nullable<bool> Company_RX_Collection
        {
            get
            {
                return this._Company_RX_Collection;
            }
            set
            {
                if ((this._Company_RX_Collection != value))
                {
                    this.OnCompany_RX_CollectionChanging(value);
                    this.SendPropertyChanging();
                    this._Company_RX_Collection = value;
                    this.SendPropertyChanged("Company_RX_Collection");
                    this.OnCompany_RX_CollectionChanged();
                }
            }
        }

        [Column(Storage = "_Company_RX_Movement", DbType = "Bit")]
        public System.Nullable<bool> Company_RX_Movement
        {
            get
            {
                return this._Company_RX_Movement;
            }
            set
            {
                if ((this._Company_RX_Movement != value))
                {
                    this.OnCompany_RX_MovementChanging(value);
                    this.SendPropertyChanging();
                    this._Company_RX_Movement = value;
                    this.SendPropertyChanged("Company_RX_Movement");
                    this.OnCompany_RX_MovementChanged();
                }
            }
        }

        [Column(Storage = "_Company_RX_EDC", DbType = "Bit")]
        public System.Nullable<bool> Company_RX_EDC
        {
            get
            {
                return this._Company_RX_EDC;
            }
            set
            {
                if ((this._Company_RX_EDC != value))
                {
                    this.OnCompany_RX_EDCChanging(value);
                    this.SendPropertyChanging();
                    this._Company_RX_EDC = value;
                    this.SendPropertyChanged("Company_RX_EDC");
                    this.OnCompany_RX_EDCChanged();
                }
            }
        }

        [Column(Storage = "_Company_RX_Format", DbType = "Int")]
        public System.Nullable<int> Company_RX_Format
        {
            get
            {
                return this._Company_RX_Format;
            }
            set
            {
                if ((this._Company_RX_Format != value))
                {
                    this.OnCompany_RX_FormatChanging(value);
                    this.SendPropertyChanging();
                    this._Company_RX_Format = value;
                    this.SendPropertyChanged("Company_RX_Format");
                    this.OnCompany_RX_FormatChanged();
                }
            }
        }

        [Column(Storage = "_Company_AMEDIS_Variants", DbType = "Bit")]
        public System.Nullable<bool> Company_AMEDIS_Variants
        {
            get
            {
                return this._Company_AMEDIS_Variants;
            }
            set
            {
                if ((this._Company_AMEDIS_Variants != value))
                {
                    this.OnCompany_AMEDIS_VariantsChanging(value);
                    this.SendPropertyChanging();
                    this._Company_AMEDIS_Variants = value;
                    this.SendPropertyChanged("Company_AMEDIS_Variants");
                    this.OnCompany_AMEDIS_VariantsChanged();
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
}
