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
    [Table(Name = "dbo.Sub_Company")]
    public partial class SubCompany : INotifyPropertyChanging, INotifyPropertyChanged
    {

        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);

        private int _Sub_Company_ID;

        private System.Nullable<int> _Staff_ID;

        private System.Nullable<bool> _Staff_ID_Default;

        private System.Nullable<int> _Company_ID;

        private System.Nullable<int> _Access_Key_ID;

        private System.Nullable<bool> _Access_Key_ID_Default;

        private System.Nullable<int> _Terms_Group_ID;

        private System.Nullable<bool> _Terms_Group_ID_Default;

        private System.Nullable<int> _Calendar_ID;

        private System.Nullable<int> _Company_Model_Set_ID;

        private string _Sub_Company_Name;

        private string _Sub_Company_Address;

        private string _Sub_Company_Postcode;

        private string _Sub_Company_Switchboard_Phone_No;

        private string _Sub_Company_Contact_Name;

        private string _Sub_Company_Contact_Phone_No;

        private string _Sub_Company_Contact_Email_Address;

        private string _Sub_Company_Invoice_Address;

        private string _Sub_Company_Invoice_Postcode;

        private string _Sub_Company_Invoice_Name;

        private string _Sub_Company_Price_Per_Play;

        private System.Nullable<bool> _Sub_Company_Price_Per_Play_Default;

        private string _Sub_Company_Jackpot;

        private System.Nullable<bool> _Sub_Company_Jackpot_Default;

        private string _Sub_Company_Percentage_Payout;

        private System.Nullable<bool> _Sub_Company_Percentage_Payout_Default;

        private string _Sub_Company_Start_Date;

        private string _Sub_Company_End_Date;

        private string _Sage_Account_Ref;

        private string _Sub_Company_Memo;

        private string _Sub_Company_ANA_Number;

        private System.Nullable<int> _SLA;

        private string _Sub_Company_Logo_Reference;

        private string _Sub_Company_Trade_Type;

        private System.Nullable<bool> _Sub_Company_Use_Split_Rents;

        private string _Sub_Company_Address_1;

        private string _Sub_Company_Address_2;

        private string _Sub_Company_Address_3;

        private string _Sub_Company_Address_4;

        private string _Sub_Company_Address_5;

        private string _Sub_Company_AMEDIS_Code;

        private string _Sub_Company_AMEDIS_Operational_Code;

        private System.Nullable<bool> _Sub_Company_Validate_Terms;

        private System.Nullable<float> _Sub_Company_Validate_Terms_Variance;

        private System.Nullable<bool> _Sub_Company_Suppress_Docket_Print;

        private System.Nullable<bool> _Sub_Company_Post_Print_Dockets;

        private System.Nullable<int> _Sub_Company_Docket_Type;

        private System.Nullable<bool> _Sub_Company_TX_Collection;

        private System.Nullable<bool> _Sub_Company_TX_Collection_Use_Default;

        private System.Nullable<bool> _Sub_Company_TX_Movement;

        private System.Nullable<bool> _Sub_Company_TX_Movement_Use_Default;

        private System.Nullable<bool> _Sub_Company_TX_EDC;

        private System.Nullable<bool> _Sub_Company_TX_EDC_Use_Detault;

        private System.Nullable<int> _Sub_Company_TX_Format;

        private System.Nullable<bool> _Sub_Company_TX_Format_Use_Default;

        private System.Nullable<bool> _Sub_Company_RX_Collection;

        private System.Nullable<bool> _Sub_Company_RX_Collection_Use_Default;

        private System.Nullable<bool> _Sub_Company_RX_Movement;

        private System.Nullable<bool> _Sub_Company_RX_Movement_Use_Default;

        private System.Nullable<bool> _Sub_Company_RX_EDC;

        private System.Nullable<bool> _Sub_Company_RX_EDC_Use_Detault;

        private System.Nullable<int> _Sub_Company_RX_Format;

        private System.Nullable<bool> _Sub_Company_RX_Format_Use_Default;

        private System.Nullable<bool> _Sub_Company_Period_End_Use_Date_Of_Collection;

        private string _Sub_Company_Income_Ledger_Code;

        private string _Sub_Company_Royalty_Ledger_Code;

        private System.Nullable<int> _Sub_Company_Default_Opening_Hours_ID;

        private string _Sub_Company_Account_Name;

        private string _Sub_Company_Sort_Code;

        private string _Sub_Company_Account_No;

        private string _Sub_Company_EDI_Outbox;

        private string _Sub_Company_Leisure_Data_Brewary_Code;

        private System.Nullable<bool> _Sub_Company_Force_Leisure_Data_To_Enterprise;

        #region Extensibility Method Definitions
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
        partial void OnSub_Company_IDChanging(int value);
        partial void OnSub_Company_IDChanged();
        partial void OnStaff_IDChanging(System.Nullable<int> value);
        partial void OnStaff_IDChanged();
        partial void OnStaff_ID_DefaultChanging(System.Nullable<bool> value);
        partial void OnStaff_ID_DefaultChanged();
        partial void OnCompany_IDChanging(System.Nullable<int> value);
        partial void OnCompany_IDChanged();
        partial void OnAccess_Key_IDChanging(System.Nullable<int> value);
        partial void OnAccess_Key_IDChanged();
        partial void OnAccess_Key_ID_DefaultChanging(System.Nullable<bool> value);
        partial void OnAccess_Key_ID_DefaultChanged();
        partial void OnTerms_Group_IDChanging(System.Nullable<int> value);
        partial void OnTerms_Group_IDChanged();
        partial void OnTerms_Group_ID_DefaultChanging(System.Nullable<bool> value);
        partial void OnTerms_Group_ID_DefaultChanged();
        partial void OnCalendar_IDChanging(System.Nullable<int> value);
        partial void OnCalendar_IDChanged();
        partial void OnCompany_Model_Set_IDChanging(System.Nullable<int> value);
        partial void OnCompany_Model_Set_IDChanged();
        partial void OnSub_Company_NameChanging(string value);
        partial void OnSub_Company_NameChanged();
        partial void OnSub_Company_AddressChanging(string value);
        partial void OnSub_Company_AddressChanged();
        partial void OnSub_Company_PostcodeChanging(string value);
        partial void OnSub_Company_PostcodeChanged();
        partial void OnSub_Company_Switchboard_Phone_NoChanging(string value);
        partial void OnSub_Company_Switchboard_Phone_NoChanged();
        partial void OnSub_Company_Contact_NameChanging(string value);
        partial void OnSub_Company_Contact_NameChanged();
        partial void OnSub_Company_Contact_Phone_NoChanging(string value);
        partial void OnSub_Company_Contact_Phone_NoChanged();
        partial void OnSub_Company_Contact_Email_AddressChanging(string value);
        partial void OnSub_Company_Contact_Email_AddressChanged();
        partial void OnSub_Company_Invoice_AddressChanging(string value);
        partial void OnSub_Company_Invoice_AddressChanged();
        partial void OnSub_Company_Invoice_PostcodeChanging(string value);
        partial void OnSub_Company_Invoice_PostcodeChanged();
        partial void OnSub_Company_Invoice_NameChanging(string value);
        partial void OnSub_Company_Invoice_NameChanged();
        partial void OnSub_Company_Price_Per_PlayChanging(string value);
        partial void OnSub_Company_Price_Per_PlayChanged();
        partial void OnSub_Company_Price_Per_Play_DefaultChanging(System.Nullable<bool> value);
        partial void OnSub_Company_Price_Per_Play_DefaultChanged();
        partial void OnSub_Company_JackpotChanging(string value);
        partial void OnSub_Company_JackpotChanged();
        partial void OnSub_Company_Jackpot_DefaultChanging(System.Nullable<bool> value);
        partial void OnSub_Company_Jackpot_DefaultChanged();
        partial void OnSub_Company_Percentage_PayoutChanging(string value);
        partial void OnSub_Company_Percentage_PayoutChanged();
        partial void OnSub_Company_Percentage_Payout_DefaultChanging(System.Nullable<bool> value);
        partial void OnSub_Company_Percentage_Payout_DefaultChanged();
        partial void OnSub_Company_Start_DateChanging(string value);
        partial void OnSub_Company_Start_DateChanged();
        partial void OnSub_Company_End_DateChanging(string value);
        partial void OnSub_Company_End_DateChanged();
        partial void OnSage_Account_RefChanging(string value);
        partial void OnSage_Account_RefChanged();
        partial void OnSub_Company_MemoChanging(string value);
        partial void OnSub_Company_MemoChanged();
        partial void OnSub_Company_ANA_NumberChanging(string value);
        partial void OnSub_Company_ANA_NumberChanged();
        partial void OnSLAChanging(System.Nullable<int> value);
        partial void OnSLAChanged();
        partial void OnSub_Company_Logo_ReferenceChanging(string value);
        partial void OnSub_Company_Logo_ReferenceChanged();
        partial void OnSub_Company_Trade_TypeChanging(string value);
        partial void OnSub_Company_Trade_TypeChanged();
        partial void OnSub_Company_Use_Split_RentsChanging(System.Nullable<bool> value);
        partial void OnSub_Company_Use_Split_RentsChanged();
        partial void OnSub_Company_Address_1Changing(string value);
        partial void OnSub_Company_Address_1Changed();
        partial void OnSub_Company_Address_2Changing(string value);
        partial void OnSub_Company_Address_2Changed();
        partial void OnSub_Company_Address_3Changing(string value);
        partial void OnSub_Company_Address_3Changed();
        partial void OnSub_Company_Address_4Changing(string value);
        partial void OnSub_Company_Address_4Changed();
        partial void OnSub_Company_Address_5Changing(string value);
        partial void OnSub_Company_Address_5Changed();
        partial void OnSub_Company_AMEDIS_CodeChanging(string value);
        partial void OnSub_Company_AMEDIS_CodeChanged();
        partial void OnSub_Company_AMEDIS_Operational_CodeChanging(string value);
        partial void OnSub_Company_AMEDIS_Operational_CodeChanged();
        partial void OnSub_Company_Validate_TermsChanging(System.Nullable<bool> value);
        partial void OnSub_Company_Validate_TermsChanged();
        partial void OnSub_Company_Validate_Terms_VarianceChanging(System.Nullable<float> value);
        partial void OnSub_Company_Validate_Terms_VarianceChanged();
        partial void OnSub_Company_Suppress_Docket_PrintChanging(System.Nullable<bool> value);
        partial void OnSub_Company_Suppress_Docket_PrintChanged();
        partial void OnSub_Company_Post_Print_DocketsChanging(System.Nullable<bool> value);
        partial void OnSub_Company_Post_Print_DocketsChanged();
        partial void OnSub_Company_Docket_TypeChanging(System.Nullable<int> value);
        partial void OnSub_Company_Docket_TypeChanged();
        partial void OnSub_Company_TX_CollectionChanging(System.Nullable<bool> value);
        partial void OnSub_Company_TX_CollectionChanged();
        partial void OnSub_Company_TX_Collection_Use_DefaultChanging(System.Nullable<bool> value);
        partial void OnSub_Company_TX_Collection_Use_DefaultChanged();
        partial void OnSub_Company_TX_MovementChanging(System.Nullable<bool> value);
        partial void OnSub_Company_TX_MovementChanged();
        partial void OnSub_Company_TX_Movement_Use_DefaultChanging(System.Nullable<bool> value);
        partial void OnSub_Company_TX_Movement_Use_DefaultChanged();
        partial void OnSub_Company_TX_EDCChanging(System.Nullable<bool> value);
        partial void OnSub_Company_TX_EDCChanged();
        partial void OnSub_Company_TX_EDC_Use_DetaultChanging(System.Nullable<bool> value);
        partial void OnSub_Company_TX_EDC_Use_DetaultChanged();
        partial void OnSub_Company_TX_FormatChanging(System.Nullable<int> value);
        partial void OnSub_Company_TX_FormatChanged();
        partial void OnSub_Company_TX_Format_Use_DefaultChanging(System.Nullable<bool> value);
        partial void OnSub_Company_TX_Format_Use_DefaultChanged();
        partial void OnSub_Company_RX_CollectionChanging(System.Nullable<bool> value);
        partial void OnSub_Company_RX_CollectionChanged();
        partial void OnSub_Company_RX_Collection_Use_DefaultChanging(System.Nullable<bool> value);
        partial void OnSub_Company_RX_Collection_Use_DefaultChanged();
        partial void OnSub_Company_RX_MovementChanging(System.Nullable<bool> value);
        partial void OnSub_Company_RX_MovementChanged();
        partial void OnSub_Company_RX_Movement_Use_DefaultChanging(System.Nullable<bool> value);
        partial void OnSub_Company_RX_Movement_Use_DefaultChanged();
        partial void OnSub_Company_RX_EDCChanging(System.Nullable<bool> value);
        partial void OnSub_Company_RX_EDCChanged();
        partial void OnSub_Company_RX_EDC_Use_DetaultChanging(System.Nullable<bool> value);
        partial void OnSub_Company_RX_EDC_Use_DetaultChanged();
        partial void OnSub_Company_RX_FormatChanging(System.Nullable<int> value);
        partial void OnSub_Company_RX_FormatChanged();
        partial void OnSub_Company_RX_Format_Use_DefaultChanging(System.Nullable<bool> value);
        partial void OnSub_Company_RX_Format_Use_DefaultChanged();
        partial void OnSub_Company_Period_End_Use_Date_Of_CollectionChanging(System.Nullable<bool> value);
        partial void OnSub_Company_Period_End_Use_Date_Of_CollectionChanged();
        partial void OnSub_Company_Income_Ledger_CodeChanging(string value);
        partial void OnSub_Company_Income_Ledger_CodeChanged();
        partial void OnSub_Company_Royalty_Ledger_CodeChanging(string value);
        partial void OnSub_Company_Royalty_Ledger_CodeChanged();
        partial void OnSub_Company_Default_Opening_Hours_IDChanging(System.Nullable<int> value);
        partial void OnSub_Company_Default_Opening_Hours_IDChanged();
        partial void OnSub_Company_Account_NameChanging(string value);
        partial void OnSub_Company_Account_NameChanged();
        partial void OnSub_Company_Sort_CodeChanging(string value);
        partial void OnSub_Company_Sort_CodeChanged();
        partial void OnSub_Company_Account_NoChanging(string value);
        partial void OnSub_Company_Account_NoChanged();
        partial void OnSub_Company_EDI_OutboxChanging(string value);
        partial void OnSub_Company_EDI_OutboxChanged();
        partial void OnSub_Company_Leisure_Data_Brewary_CodeChanging(string value);
        partial void OnSub_Company_Leisure_Data_Brewary_CodeChanged();
        partial void OnSub_Company_Force_Leisure_Data_To_EnterpriseChanging(System.Nullable<bool> value);
        partial void OnSub_Company_Force_Leisure_Data_To_EnterpriseChanged();
        #endregion

        public SubCompany()
        {
            OnCreated();
        }

        [Column(Storage = "_Sub_Company_ID", AutoSync = AutoSync.OnInsert, DbType = "Int NOT NULL IDENTITY", IsPrimaryKey = true, IsDbGenerated = true)]
        public int Sub_Company_ID
        {
            get
            {
                return this._Sub_Company_ID;
            }
            set
            {
                if ((this._Sub_Company_ID != value))
                {
                    this.OnSub_Company_IDChanging(value);
                    this.SendPropertyChanging();
                    this._Sub_Company_ID = value;
                    this.SendPropertyChanged("Sub_Company_ID");
                    this.OnSub_Company_IDChanged();
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

        [Column(Storage = "_Staff_ID_Default", DbType = "Bit")]
        public System.Nullable<bool> Staff_ID_Default
        {
            get
            {
                return this._Staff_ID_Default;
            }
            set
            {
                if ((this._Staff_ID_Default != value))
                {
                    this.OnStaff_ID_DefaultChanging(value);
                    this.SendPropertyChanging();
                    this._Staff_ID_Default = value;
                    this.SendPropertyChanged("Staff_ID_Default");
                    this.OnStaff_ID_DefaultChanged();
                }
            }
        }

        [Column(Storage = "_Company_ID", DbType = "Int")]
        public System.Nullable<int> Company_ID
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

        [Column(Storage = "_Access_Key_ID_Default", DbType = "Bit")]
        public System.Nullable<bool> Access_Key_ID_Default
        {
            get
            {
                return this._Access_Key_ID_Default;
            }
            set
            {
                if ((this._Access_Key_ID_Default != value))
                {
                    this.OnAccess_Key_ID_DefaultChanging(value);
                    this.SendPropertyChanging();
                    this._Access_Key_ID_Default = value;
                    this.SendPropertyChanged("Access_Key_ID_Default");
                    this.OnAccess_Key_ID_DefaultChanged();
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

        [Column(Storage = "_Terms_Group_ID_Default", DbType = "Bit")]
        public System.Nullable<bool> Terms_Group_ID_Default
        {
            get
            {
                return this._Terms_Group_ID_Default;
            }
            set
            {
                if ((this._Terms_Group_ID_Default != value))
                {
                    this.OnTerms_Group_ID_DefaultChanging(value);
                    this.SendPropertyChanging();
                    this._Terms_Group_ID_Default = value;
                    this.SendPropertyChanged("Terms_Group_ID_Default");
                    this.OnTerms_Group_ID_DefaultChanged();
                }
            }
        }

        [Column(Storage = "_Calendar_ID", DbType = "Int")]
        public System.Nullable<int> Calendar_ID
        {
            get
            {
                return this._Calendar_ID;
            }
            set
            {
                if ((this._Calendar_ID != value))
                {
                    this.OnCalendar_IDChanging(value);
                    this.SendPropertyChanging();
                    this._Calendar_ID = value;
                    this.SendPropertyChanged("Calendar_ID");
                    this.OnCalendar_IDChanged();
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

        [Column(Storage = "_Sub_Company_Name", DbType = "VarChar(50)")]
        public string Sub_Company_Name
        {
            get
            {
                return this._Sub_Company_Name;
            }
            set
            {
                if ((this._Sub_Company_Name != value))
                {
                    this.OnSub_Company_NameChanging(value);
                    this.SendPropertyChanging();
                    this._Sub_Company_Name = value;
                    this.SendPropertyChanged("Sub_Company_Name");
                    this.OnSub_Company_NameChanged();
                }
            }
        }

        [Column(Storage = "_Sub_Company_Address", DbType = "NText", UpdateCheck = UpdateCheck.Never)]
        public string Sub_Company_Address
        {
            get
            {
                return this._Sub_Company_Address;
            }
            set
            {
                if ((this._Sub_Company_Address != value))
                {
                    this.OnSub_Company_AddressChanging(value);
                    this.SendPropertyChanging();
                    this._Sub_Company_Address = value;
                    this.SendPropertyChanged("Sub_Company_Address");
                    this.OnSub_Company_AddressChanged();
                }
            }
        }

        [Column(Storage = "_Sub_Company_Postcode", DbType = "VarChar(15)")]
        public string Sub_Company_Postcode
        {
            get
            {
                return this._Sub_Company_Postcode;
            }
            set
            {
                if ((this._Sub_Company_Postcode != value))
                {
                    this.OnSub_Company_PostcodeChanging(value);
                    this.SendPropertyChanging();
                    this._Sub_Company_Postcode = value;
                    this.SendPropertyChanged("Sub_Company_Postcode");
                    this.OnSub_Company_PostcodeChanged();
                }
            }
        }

        [Column(Storage = "_Sub_Company_Switchboard_Phone_No", DbType = "VarChar(15)")]
        public string Sub_Company_Switchboard_Phone_No
        {
            get
            {
                return this._Sub_Company_Switchboard_Phone_No;
            }
            set
            {
                if ((this._Sub_Company_Switchboard_Phone_No != value))
                {
                    this.OnSub_Company_Switchboard_Phone_NoChanging(value);
                    this.SendPropertyChanging();
                    this._Sub_Company_Switchboard_Phone_No = value;
                    this.SendPropertyChanged("Sub_Company_Switchboard_Phone_No");
                    this.OnSub_Company_Switchboard_Phone_NoChanged();
                }
            }
        }

        [Column(Storage = "_Sub_Company_Contact_Name", DbType = "VarChar(50)")]
        public string Sub_Company_Contact_Name
        {
            get
            {
                return this._Sub_Company_Contact_Name;
            }
            set
            {
                if ((this._Sub_Company_Contact_Name != value))
                {
                    this.OnSub_Company_Contact_NameChanging(value);
                    this.SendPropertyChanging();
                    this._Sub_Company_Contact_Name = value;
                    this.SendPropertyChanged("Sub_Company_Contact_Name");
                    this.OnSub_Company_Contact_NameChanged();
                }
            }
        }

        [Column(Storage = "_Sub_Company_Contact_Phone_No", DbType = "VarChar(15)")]
        public string Sub_Company_Contact_Phone_No
        {
            get
            {
                return this._Sub_Company_Contact_Phone_No;
            }
            set
            {
                if ((this._Sub_Company_Contact_Phone_No != value))
                {
                    this.OnSub_Company_Contact_Phone_NoChanging(value);
                    this.SendPropertyChanging();
                    this._Sub_Company_Contact_Phone_No = value;
                    this.SendPropertyChanged("Sub_Company_Contact_Phone_No");
                    this.OnSub_Company_Contact_Phone_NoChanged();
                }
            }
        }

        [Column(Storage = "_Sub_Company_Contact_Email_Address", DbType = "VarChar(100)")]
        public string Sub_Company_Contact_Email_Address
        {
            get
            {
                return this._Sub_Company_Contact_Email_Address;
            }
            set
            {
                if ((this._Sub_Company_Contact_Email_Address != value))
                {
                    this.OnSub_Company_Contact_Email_AddressChanging(value);
                    this.SendPropertyChanging();
                    this._Sub_Company_Contact_Email_Address = value;
                    this.SendPropertyChanged("Sub_Company_Contact_Email_Address");
                    this.OnSub_Company_Contact_Email_AddressChanged();
                }
            }
        }

        [Column(Storage = "_Sub_Company_Invoice_Address", DbType = "NText", UpdateCheck = UpdateCheck.Never)]
        public string Sub_Company_Invoice_Address
        {
            get
            {
                return this._Sub_Company_Invoice_Address;
            }
            set
            {
                if ((this._Sub_Company_Invoice_Address != value))
                {
                    this.OnSub_Company_Invoice_AddressChanging(value);
                    this.SendPropertyChanging();
                    this._Sub_Company_Invoice_Address = value;
                    this.SendPropertyChanged("Sub_Company_Invoice_Address");
                    this.OnSub_Company_Invoice_AddressChanged();
                }
            }
        }

        [Column(Storage = "_Sub_Company_Invoice_Postcode", DbType = "VarChar(15)")]
        public string Sub_Company_Invoice_Postcode
        {
            get
            {
                return this._Sub_Company_Invoice_Postcode;
            }
            set
            {
                if ((this._Sub_Company_Invoice_Postcode != value))
                {
                    this.OnSub_Company_Invoice_PostcodeChanging(value);
                    this.SendPropertyChanging();
                    this._Sub_Company_Invoice_Postcode = value;
                    this.SendPropertyChanged("Sub_Company_Invoice_Postcode");
                    this.OnSub_Company_Invoice_PostcodeChanged();
                }
            }
        }

        [Column(Storage = "_Sub_Company_Invoice_Name", DbType = "VarChar(50)")]
        public string Sub_Company_Invoice_Name
        {
            get
            {
                return this._Sub_Company_Invoice_Name;
            }
            set
            {
                if ((this._Sub_Company_Invoice_Name != value))
                {
                    this.OnSub_Company_Invoice_NameChanging(value);
                    this.SendPropertyChanging();
                    this._Sub_Company_Invoice_Name = value;
                    this.SendPropertyChanged("Sub_Company_Invoice_Name");
                    this.OnSub_Company_Invoice_NameChanged();
                }
            }
        }

        [Column(Storage = "_Sub_Company_Price_Per_Play", DbType = "VarChar(50)")]
        public string Sub_Company_Price_Per_Play
        {
            get
            {
                return this._Sub_Company_Price_Per_Play;
            }
            set
            {
                if ((this._Sub_Company_Price_Per_Play != value))
                {
                    this.OnSub_Company_Price_Per_PlayChanging(value);
                    this.SendPropertyChanging();
                    this._Sub_Company_Price_Per_Play = value;
                    this.SendPropertyChanged("Sub_Company_Price_Per_Play");
                    this.OnSub_Company_Price_Per_PlayChanged();
                }
            }
        }

        [Column(Storage = "_Sub_Company_Price_Per_Play_Default", DbType = "Bit")]
        public System.Nullable<bool> Sub_Company_Price_Per_Play_Default
        {
            get
            {
                return this._Sub_Company_Price_Per_Play_Default;
            }
            set
            {
                if ((this._Sub_Company_Price_Per_Play_Default != value))
                {
                    this.OnSub_Company_Price_Per_Play_DefaultChanging(value);
                    this.SendPropertyChanging();
                    this._Sub_Company_Price_Per_Play_Default = value;
                    this.SendPropertyChanged("Sub_Company_Price_Per_Play_Default");
                    this.OnSub_Company_Price_Per_Play_DefaultChanged();
                }
            }
        }

        [Column(Storage = "_Sub_Company_Jackpot", DbType = "VarChar(50)")]
        public string Sub_Company_Jackpot
        {
            get
            {
                return this._Sub_Company_Jackpot;
            }
            set
            {
                if ((this._Sub_Company_Jackpot != value))
                {
                    this.OnSub_Company_JackpotChanging(value);
                    this.SendPropertyChanging();
                    this._Sub_Company_Jackpot = value;
                    this.SendPropertyChanged("Sub_Company_Jackpot");
                    this.OnSub_Company_JackpotChanged();
                }
            }
        }

        [Column(Storage = "_Sub_Company_Jackpot_Default", DbType = "Bit")]
        public System.Nullable<bool> Sub_Company_Jackpot_Default
        {
            get
            {
                return this._Sub_Company_Jackpot_Default;
            }
            set
            {
                if ((this._Sub_Company_Jackpot_Default != value))
                {
                    this.OnSub_Company_Jackpot_DefaultChanging(value);
                    this.SendPropertyChanging();
                    this._Sub_Company_Jackpot_Default = value;
                    this.SendPropertyChanged("Sub_Company_Jackpot_Default");
                    this.OnSub_Company_Jackpot_DefaultChanged();
                }
            }
        }

        [Column(Storage = "_Sub_Company_Percentage_Payout", DbType = "VarChar(50)")]
        public string Sub_Company_Percentage_Payout
        {
            get
            {
                return this._Sub_Company_Percentage_Payout;
            }
            set
            {
                if ((this._Sub_Company_Percentage_Payout != value))
                {
                    this.OnSub_Company_Percentage_PayoutChanging(value);
                    this.SendPropertyChanging();
                    this._Sub_Company_Percentage_Payout = value;
                    this.SendPropertyChanged("Sub_Company_Percentage_Payout");
                    this.OnSub_Company_Percentage_PayoutChanged();
                }
            }
        }

        [Column(Storage = "_Sub_Company_Percentage_Payout_Default", DbType = "Bit")]
        public System.Nullable<bool> Sub_Company_Percentage_Payout_Default
        {
            get
            {
                return this._Sub_Company_Percentage_Payout_Default;
            }
            set
            {
                if ((this._Sub_Company_Percentage_Payout_Default != value))
                {
                    this.OnSub_Company_Percentage_Payout_DefaultChanging(value);
                    this.SendPropertyChanging();
                    this._Sub_Company_Percentage_Payout_Default = value;
                    this.SendPropertyChanged("Sub_Company_Percentage_Payout_Default");
                    this.OnSub_Company_Percentage_Payout_DefaultChanged();
                }
            }
        }

        [Column(Storage = "_Sub_Company_Start_Date", DbType = "VarChar(30)")]
        public string Sub_Company_Start_Date
        {
            get
            {
                return this._Sub_Company_Start_Date;
            }
            set
            {
                if ((this._Sub_Company_Start_Date != value))
                {
                    this.OnSub_Company_Start_DateChanging(value);
                    this.SendPropertyChanging();
                    this._Sub_Company_Start_Date = value;
                    this.SendPropertyChanged("Sub_Company_Start_Date");
                    this.OnSub_Company_Start_DateChanged();
                }
            }
        }

        [Column(Storage = "_Sub_Company_End_Date", DbType = "VarChar(30)")]
        public string Sub_Company_End_Date
        {
            get
            {
                return this._Sub_Company_End_Date;
            }
            set
            {
                if ((this._Sub_Company_End_Date != value))
                {
                    this.OnSub_Company_End_DateChanging(value);
                    this.SendPropertyChanging();
                    this._Sub_Company_End_Date = value;
                    this.SendPropertyChanged("Sub_Company_End_Date");
                    this.OnSub_Company_End_DateChanged();
                }
            }
        }

        [Column(Storage = "_Sage_Account_Ref", DbType = "VarChar(50)")]
        public string Sage_Account_Ref
        {
            get
            {
                return this._Sage_Account_Ref;
            }
            set
            {
                if ((this._Sage_Account_Ref != value))
                {
                    this.OnSage_Account_RefChanging(value);
                    this.SendPropertyChanging();
                    this._Sage_Account_Ref = value;
                    this.SendPropertyChanged("Sage_Account_Ref");
                    this.OnSage_Account_RefChanged();
                }
            }
        }

        [Column(Storage = "_Sub_Company_Memo", DbType = "NText", UpdateCheck = UpdateCheck.Never)]
        public string Sub_Company_Memo
        {
            get
            {
                return this._Sub_Company_Memo;
            }
            set
            {
                if ((this._Sub_Company_Memo != value))
                {
                    this.OnSub_Company_MemoChanging(value);
                    this.SendPropertyChanging();
                    this._Sub_Company_Memo = value;
                    this.SendPropertyChanged("Sub_Company_Memo");
                    this.OnSub_Company_MemoChanged();
                }
            }
        }

        [Column(Storage = "_Sub_Company_ANA_Number", DbType = "VarChar(20)")]
        public string Sub_Company_ANA_Number
        {
            get
            {
                return this._Sub_Company_ANA_Number;
            }
            set
            {
                if ((this._Sub_Company_ANA_Number != value))
                {
                    this.OnSub_Company_ANA_NumberChanging(value);
                    this.SendPropertyChanging();
                    this._Sub_Company_ANA_Number = value;
                    this.SendPropertyChanged("Sub_Company_ANA_Number");
                    this.OnSub_Company_ANA_NumberChanged();
                }
            }
        }

        [Column(Storage = "_SLA", DbType = "Int")]
        public System.Nullable<int> SLA
        {
            get
            {
                return this._SLA;
            }
            set
            {
                if ((this._SLA != value))
                {
                    this.OnSLAChanging(value);
                    this.SendPropertyChanging();
                    this._SLA = value;
                    this.SendPropertyChanged("SLA");
                    this.OnSLAChanged();
                }
            }
        }

        [Column(Storage = "_Sub_Company_Logo_Reference", DbType = "VarChar(50)")]
        public string Sub_Company_Logo_Reference
        {
            get
            {
                return this._Sub_Company_Logo_Reference;
            }
            set
            {
                if ((this._Sub_Company_Logo_Reference != value))
                {
                    this.OnSub_Company_Logo_ReferenceChanging(value);
                    this.SendPropertyChanging();
                    this._Sub_Company_Logo_Reference = value;
                    this.SendPropertyChanged("Sub_Company_Logo_Reference");
                    this.OnSub_Company_Logo_ReferenceChanged();
                }
            }
        }

        [Column(Storage = "_Sub_Company_Trade_Type", DbType = "VarChar(50)")]
        public string Sub_Company_Trade_Type
        {
            get
            {
                return this._Sub_Company_Trade_Type;
            }
            set
            {
                if ((this._Sub_Company_Trade_Type != value))
                {
                    this.OnSub_Company_Trade_TypeChanging(value);
                    this.SendPropertyChanging();
                    this._Sub_Company_Trade_Type = value;
                    this.SendPropertyChanged("Sub_Company_Trade_Type");
                    this.OnSub_Company_Trade_TypeChanged();
                }
            }
        }

        [Column(Storage = "_Sub_Company_Use_Split_Rents", DbType = "Bit")]
        public System.Nullable<bool> Sub_Company_Use_Split_Rents
        {
            get
            {
                return this._Sub_Company_Use_Split_Rents;
            }
            set
            {
                if ((this._Sub_Company_Use_Split_Rents != value))
                {
                    this.OnSub_Company_Use_Split_RentsChanging(value);
                    this.SendPropertyChanging();
                    this._Sub_Company_Use_Split_Rents = value;
                    this.SendPropertyChanged("Sub_Company_Use_Split_Rents");
                    this.OnSub_Company_Use_Split_RentsChanged();
                }
            }
        }

        [Column(Storage = "_Sub_Company_Address_1", DbType = "VarChar(50)")]
        public string Sub_Company_Address_1
        {
            get
            {
                return this._Sub_Company_Address_1;
            }
            set
            {
                if ((this._Sub_Company_Address_1 != value))
                {
                    this.OnSub_Company_Address_1Changing(value);
                    this.SendPropertyChanging();
                    this._Sub_Company_Address_1 = value;
                    this.SendPropertyChanged("Sub_Company_Address_1");
                    this.OnSub_Company_Address_1Changed();
                }
            }
        }

        [Column(Storage = "_Sub_Company_Address_2", DbType = "VarChar(50)")]
        public string Sub_Company_Address_2
        {
            get
            {
                return this._Sub_Company_Address_2;
            }
            set
            {
                if ((this._Sub_Company_Address_2 != value))
                {
                    this.OnSub_Company_Address_2Changing(value);
                    this.SendPropertyChanging();
                    this._Sub_Company_Address_2 = value;
                    this.SendPropertyChanged("Sub_Company_Address_2");
                    this.OnSub_Company_Address_2Changed();
                }
            }
        }

        [Column(Storage = "_Sub_Company_Address_3", DbType = "VarChar(50)")]
        public string Sub_Company_Address_3
        {
            get
            {
                return this._Sub_Company_Address_3;
            }
            set
            {
                if ((this._Sub_Company_Address_3 != value))
                {
                    this.OnSub_Company_Address_3Changing(value);
                    this.SendPropertyChanging();
                    this._Sub_Company_Address_3 = value;
                    this.SendPropertyChanged("Sub_Company_Address_3");
                    this.OnSub_Company_Address_3Changed();
                }
            }
        }

        [Column(Storage = "_Sub_Company_Address_4", DbType = "VarChar(50)")]
        public string Sub_Company_Address_4
        {
            get
            {
                return this._Sub_Company_Address_4;
            }
            set
            {
                if ((this._Sub_Company_Address_4 != value))
                {
                    this.OnSub_Company_Address_4Changing(value);
                    this.SendPropertyChanging();
                    this._Sub_Company_Address_4 = value;
                    this.SendPropertyChanged("Sub_Company_Address_4");
                    this.OnSub_Company_Address_4Changed();
                }
            }
        }

        [Column(Storage = "_Sub_Company_Address_5", DbType = "VarChar(50)")]
        public string Sub_Company_Address_5
        {
            get
            {
                return this._Sub_Company_Address_5;
            }
            set
            {
                if ((this._Sub_Company_Address_5 != value))
                {
                    this.OnSub_Company_Address_5Changing(value);
                    this.SendPropertyChanging();
                    this._Sub_Company_Address_5 = value;
                    this.SendPropertyChanged("Sub_Company_Address_5");
                    this.OnSub_Company_Address_5Changed();
                }
            }
        }

        [Column(Storage = "_Sub_Company_AMEDIS_Code", DbType = "VarChar(10)")]
        public string Sub_Company_AMEDIS_Code
        {
            get
            {
                return this._Sub_Company_AMEDIS_Code;
            }
            set
            {
                if ((this._Sub_Company_AMEDIS_Code != value))
                {
                    this.OnSub_Company_AMEDIS_CodeChanging(value);
                    this.SendPropertyChanging();
                    this._Sub_Company_AMEDIS_Code = value;
                    this.SendPropertyChanged("Sub_Company_AMEDIS_Code");
                    this.OnSub_Company_AMEDIS_CodeChanged();
                }
            }
        }

        [Column(Storage = "_Sub_Company_AMEDIS_Operational_Code", DbType = "VarChar(10)")]
        public string Sub_Company_AMEDIS_Operational_Code
        {
            get
            {
                return this._Sub_Company_AMEDIS_Operational_Code;
            }
            set
            {
                if ((this._Sub_Company_AMEDIS_Operational_Code != value))
                {
                    this.OnSub_Company_AMEDIS_Operational_CodeChanging(value);
                    this.SendPropertyChanging();
                    this._Sub_Company_AMEDIS_Operational_Code = value;
                    this.SendPropertyChanged("Sub_Company_AMEDIS_Operational_Code");
                    this.OnSub_Company_AMEDIS_Operational_CodeChanged();
                }
            }
        }

        [Column(Storage = "_Sub_Company_Validate_Terms", DbType = "Bit")]
        public System.Nullable<bool> Sub_Company_Validate_Terms
        {
            get
            {
                return this._Sub_Company_Validate_Terms;
            }
            set
            {
                if ((this._Sub_Company_Validate_Terms != value))
                {
                    this.OnSub_Company_Validate_TermsChanging(value);
                    this.SendPropertyChanging();
                    this._Sub_Company_Validate_Terms = value;
                    this.SendPropertyChanged("Sub_Company_Validate_Terms");
                    this.OnSub_Company_Validate_TermsChanged();
                }
            }
        }

        [Column(Storage = "_Sub_Company_Validate_Terms_Variance", DbType = "Real")]
        public System.Nullable<float> Sub_Company_Validate_Terms_Variance
        {
            get
            {
                return this._Sub_Company_Validate_Terms_Variance;
            }
            set
            {
                if ((this._Sub_Company_Validate_Terms_Variance != value))
                {
                    this.OnSub_Company_Validate_Terms_VarianceChanging(value);
                    this.SendPropertyChanging();
                    this._Sub_Company_Validate_Terms_Variance = value;
                    this.SendPropertyChanged("Sub_Company_Validate_Terms_Variance");
                    this.OnSub_Company_Validate_Terms_VarianceChanged();
                }
            }
        }

        [Column(Storage = "_Sub_Company_Suppress_Docket_Print", DbType = "Bit")]
        public System.Nullable<bool> Sub_Company_Suppress_Docket_Print
        {
            get
            {
                return this._Sub_Company_Suppress_Docket_Print;
            }
            set
            {
                if ((this._Sub_Company_Suppress_Docket_Print != value))
                {
                    this.OnSub_Company_Suppress_Docket_PrintChanging(value);
                    this.SendPropertyChanging();
                    this._Sub_Company_Suppress_Docket_Print = value;
                    this.SendPropertyChanged("Sub_Company_Suppress_Docket_Print");
                    this.OnSub_Company_Suppress_Docket_PrintChanged();
                }
            }
        }

        [Column(Storage = "_Sub_Company_Post_Print_Dockets", DbType = "Bit")]
        public System.Nullable<bool> Sub_Company_Post_Print_Dockets
        {
            get
            {
                return this._Sub_Company_Post_Print_Dockets;
            }
            set
            {
                if ((this._Sub_Company_Post_Print_Dockets != value))
                {
                    this.OnSub_Company_Post_Print_DocketsChanging(value);
                    this.SendPropertyChanging();
                    this._Sub_Company_Post_Print_Dockets = value;
                    this.SendPropertyChanged("Sub_Company_Post_Print_Dockets");
                    this.OnSub_Company_Post_Print_DocketsChanged();
                }
            }
        }

        [Column(Storage = "_Sub_Company_Docket_Type", DbType = "Int")]
        public System.Nullable<int> Sub_Company_Docket_Type
        {
            get
            {
                return this._Sub_Company_Docket_Type;
            }
            set
            {
                if ((this._Sub_Company_Docket_Type != value))
                {
                    this.OnSub_Company_Docket_TypeChanging(value);
                    this.SendPropertyChanging();
                    this._Sub_Company_Docket_Type = value;
                    this.SendPropertyChanged("Sub_Company_Docket_Type");
                    this.OnSub_Company_Docket_TypeChanged();
                }
            }
        }

        [Column(Storage = "_Sub_Company_TX_Collection", DbType = "Bit")]
        public System.Nullable<bool> Sub_Company_TX_Collection
        {
            get
            {
                return this._Sub_Company_TX_Collection;
            }
            set
            {
                if ((this._Sub_Company_TX_Collection != value))
                {
                    this.OnSub_Company_TX_CollectionChanging(value);
                    this.SendPropertyChanging();
                    this._Sub_Company_TX_Collection = value;
                    this.SendPropertyChanged("Sub_Company_TX_Collection");
                    this.OnSub_Company_TX_CollectionChanged();
                }
            }
        }

        [Column(Storage = "_Sub_Company_TX_Collection_Use_Default", DbType = "Bit")]
        public System.Nullable<bool> Sub_Company_TX_Collection_Use_Default
        {
            get
            {
                return this._Sub_Company_TX_Collection_Use_Default;
            }
            set
            {
                if ((this._Sub_Company_TX_Collection_Use_Default != value))
                {
                    this.OnSub_Company_TX_Collection_Use_DefaultChanging(value);
                    this.SendPropertyChanging();
                    this._Sub_Company_TX_Collection_Use_Default = value;
                    this.SendPropertyChanged("Sub_Company_TX_Collection_Use_Default");
                    this.OnSub_Company_TX_Collection_Use_DefaultChanged();
                }
            }
        }

        [Column(Storage = "_Sub_Company_TX_Movement", DbType = "Bit")]
        public System.Nullable<bool> Sub_Company_TX_Movement
        {
            get
            {
                return this._Sub_Company_TX_Movement;
            }
            set
            {
                if ((this._Sub_Company_TX_Movement != value))
                {
                    this.OnSub_Company_TX_MovementChanging(value);
                    this.SendPropertyChanging();
                    this._Sub_Company_TX_Movement = value;
                    this.SendPropertyChanged("Sub_Company_TX_Movement");
                    this.OnSub_Company_TX_MovementChanged();
                }
            }
        }

        [Column(Storage = "_Sub_Company_TX_Movement_Use_Default", DbType = "Bit")]
        public System.Nullable<bool> Sub_Company_TX_Movement_Use_Default
        {
            get
            {
                return this._Sub_Company_TX_Movement_Use_Default;
            }
            set
            {
                if ((this._Sub_Company_TX_Movement_Use_Default != value))
                {
                    this.OnSub_Company_TX_Movement_Use_DefaultChanging(value);
                    this.SendPropertyChanging();
                    this._Sub_Company_TX_Movement_Use_Default = value;
                    this.SendPropertyChanged("Sub_Company_TX_Movement_Use_Default");
                    this.OnSub_Company_TX_Movement_Use_DefaultChanged();
                }
            }
        }

        [Column(Storage = "_Sub_Company_TX_EDC", DbType = "Bit")]
        public System.Nullable<bool> Sub_Company_TX_EDC
        {
            get
            {
                return this._Sub_Company_TX_EDC;
            }
            set
            {
                if ((this._Sub_Company_TX_EDC != value))
                {
                    this.OnSub_Company_TX_EDCChanging(value);
                    this.SendPropertyChanging();
                    this._Sub_Company_TX_EDC = value;
                    this.SendPropertyChanged("Sub_Company_TX_EDC");
                    this.OnSub_Company_TX_EDCChanged();
                }
            }
        }

        [Column(Storage = "_Sub_Company_TX_EDC_Use_Detault", DbType = "Bit")]
        public System.Nullable<bool> Sub_Company_TX_EDC_Use_Detault
        {
            get
            {
                return this._Sub_Company_TX_EDC_Use_Detault;
            }
            set
            {
                if ((this._Sub_Company_TX_EDC_Use_Detault != value))
                {
                    this.OnSub_Company_TX_EDC_Use_DetaultChanging(value);
                    this.SendPropertyChanging();
                    this._Sub_Company_TX_EDC_Use_Detault = value;
                    this.SendPropertyChanged("Sub_Company_TX_EDC_Use_Detault");
                    this.OnSub_Company_TX_EDC_Use_DetaultChanged();
                }
            }
        }

        [Column(Storage = "_Sub_Company_TX_Format", DbType = "Int")]
        public System.Nullable<int> Sub_Company_TX_Format
        {
            get
            {
                return this._Sub_Company_TX_Format;
            }
            set
            {
                if ((this._Sub_Company_TX_Format != value))
                {
                    this.OnSub_Company_TX_FormatChanging(value);
                    this.SendPropertyChanging();
                    this._Sub_Company_TX_Format = value;
                    this.SendPropertyChanged("Sub_Company_TX_Format");
                    this.OnSub_Company_TX_FormatChanged();
                }
            }
        }

        [Column(Storage = "_Sub_Company_TX_Format_Use_Default", DbType = "Bit")]
        public System.Nullable<bool> Sub_Company_TX_Format_Use_Default
        {
            get
            {
                return this._Sub_Company_TX_Format_Use_Default;
            }
            set
            {
                if ((this._Sub_Company_TX_Format_Use_Default != value))
                {
                    this.OnSub_Company_TX_Format_Use_DefaultChanging(value);
                    this.SendPropertyChanging();
                    this._Sub_Company_TX_Format_Use_Default = value;
                    this.SendPropertyChanged("Sub_Company_TX_Format_Use_Default");
                    this.OnSub_Company_TX_Format_Use_DefaultChanged();
                }
            }
        }

        [Column(Storage = "_Sub_Company_RX_Collection", DbType = "Bit")]
        public System.Nullable<bool> Sub_Company_RX_Collection
        {
            get
            {
                return this._Sub_Company_RX_Collection;
            }
            set
            {
                if ((this._Sub_Company_RX_Collection != value))
                {
                    this.OnSub_Company_RX_CollectionChanging(value);
                    this.SendPropertyChanging();
                    this._Sub_Company_RX_Collection = value;
                    this.SendPropertyChanged("Sub_Company_RX_Collection");
                    this.OnSub_Company_RX_CollectionChanged();
                }
            }
        }

        [Column(Storage = "_Sub_Company_RX_Collection_Use_Default", DbType = "Bit")]
        public System.Nullable<bool> Sub_Company_RX_Collection_Use_Default
        {
            get
            {
                return this._Sub_Company_RX_Collection_Use_Default;
            }
            set
            {
                if ((this._Sub_Company_RX_Collection_Use_Default != value))
                {
                    this.OnSub_Company_RX_Collection_Use_DefaultChanging(value);
                    this.SendPropertyChanging();
                    this._Sub_Company_RX_Collection_Use_Default = value;
                    this.SendPropertyChanged("Sub_Company_RX_Collection_Use_Default");
                    this.OnSub_Company_RX_Collection_Use_DefaultChanged();
                }
            }
        }

        [Column(Storage = "_Sub_Company_RX_Movement", DbType = "Bit")]
        public System.Nullable<bool> Sub_Company_RX_Movement
        {
            get
            {
                return this._Sub_Company_RX_Movement;
            }
            set
            {
                if ((this._Sub_Company_RX_Movement != value))
                {
                    this.OnSub_Company_RX_MovementChanging(value);
                    this.SendPropertyChanging();
                    this._Sub_Company_RX_Movement = value;
                    this.SendPropertyChanged("Sub_Company_RX_Movement");
                    this.OnSub_Company_RX_MovementChanged();
                }
            }
        }

        [Column(Storage = "_Sub_Company_RX_Movement_Use_Default", DbType = "Bit")]
        public System.Nullable<bool> Sub_Company_RX_Movement_Use_Default
        {
            get
            {
                return this._Sub_Company_RX_Movement_Use_Default;
            }
            set
            {
                if ((this._Sub_Company_RX_Movement_Use_Default != value))
                {
                    this.OnSub_Company_RX_Movement_Use_DefaultChanging(value);
                    this.SendPropertyChanging();
                    this._Sub_Company_RX_Movement_Use_Default = value;
                    this.SendPropertyChanged("Sub_Company_RX_Movement_Use_Default");
                    this.OnSub_Company_RX_Movement_Use_DefaultChanged();
                }
            }
        }

        [Column(Storage = "_Sub_Company_RX_EDC", DbType = "Bit")]
        public System.Nullable<bool> Sub_Company_RX_EDC
        {
            get
            {
                return this._Sub_Company_RX_EDC;
            }
            set
            {
                if ((this._Sub_Company_RX_EDC != value))
                {
                    this.OnSub_Company_RX_EDCChanging(value);
                    this.SendPropertyChanging();
                    this._Sub_Company_RX_EDC = value;
                    this.SendPropertyChanged("Sub_Company_RX_EDC");
                    this.OnSub_Company_RX_EDCChanged();
                }
            }
        }

        [Column(Storage = "_Sub_Company_RX_EDC_Use_Detault", DbType = "Bit")]
        public System.Nullable<bool> Sub_Company_RX_EDC_Use_Detault
        {
            get
            {
                return this._Sub_Company_RX_EDC_Use_Detault;
            }
            set
            {
                if ((this._Sub_Company_RX_EDC_Use_Detault != value))
                {
                    this.OnSub_Company_RX_EDC_Use_DetaultChanging(value);
                    this.SendPropertyChanging();
                    this._Sub_Company_RX_EDC_Use_Detault = value;
                    this.SendPropertyChanged("Sub_Company_RX_EDC_Use_Detault");
                    this.OnSub_Company_RX_EDC_Use_DetaultChanged();
                }
            }
        }

        [Column(Storage = "_Sub_Company_RX_Format", DbType = "Int")]
        public System.Nullable<int> Sub_Company_RX_Format
        {
            get
            {
                return this._Sub_Company_RX_Format;
            }
            set
            {
                if ((this._Sub_Company_RX_Format != value))
                {
                    this.OnSub_Company_RX_FormatChanging(value);
                    this.SendPropertyChanging();
                    this._Sub_Company_RX_Format = value;
                    this.SendPropertyChanged("Sub_Company_RX_Format");
                    this.OnSub_Company_RX_FormatChanged();
                }
            }
        }

        [Column(Storage = "_Sub_Company_RX_Format_Use_Default", DbType = "Bit")]
        public System.Nullable<bool> Sub_Company_RX_Format_Use_Default
        {
            get
            {
                return this._Sub_Company_RX_Format_Use_Default;
            }
            set
            {
                if ((this._Sub_Company_RX_Format_Use_Default != value))
                {
                    this.OnSub_Company_RX_Format_Use_DefaultChanging(value);
                    this.SendPropertyChanging();
                    this._Sub_Company_RX_Format_Use_Default = value;
                    this.SendPropertyChanged("Sub_Company_RX_Format_Use_Default");
                    this.OnSub_Company_RX_Format_Use_DefaultChanged();
                }
            }
        }

        [Column(Storage = "_Sub_Company_Period_End_Use_Date_Of_Collection", DbType = "Bit")]
        public System.Nullable<bool> Sub_Company_Period_End_Use_Date_Of_Collection
        {
            get
            {
                return this._Sub_Company_Period_End_Use_Date_Of_Collection;
            }
            set
            {
                if ((this._Sub_Company_Period_End_Use_Date_Of_Collection != value))
                {
                    this.OnSub_Company_Period_End_Use_Date_Of_CollectionChanging(value);
                    this.SendPropertyChanging();
                    this._Sub_Company_Period_End_Use_Date_Of_Collection = value;
                    this.SendPropertyChanged("Sub_Company_Period_End_Use_Date_Of_Collection");
                    this.OnSub_Company_Period_End_Use_Date_Of_CollectionChanged();
                }
            }
        }

        [Column(Storage = "_Sub_Company_Income_Ledger_Code", DbType = "VarChar(20)")]
        public string Sub_Company_Income_Ledger_Code
        {
            get
            {
                return this._Sub_Company_Income_Ledger_Code;
            }
            set
            {
                if ((this._Sub_Company_Income_Ledger_Code != value))
                {
                    this.OnSub_Company_Income_Ledger_CodeChanging(value);
                    this.SendPropertyChanging();
                    this._Sub_Company_Income_Ledger_Code = value;
                    this.SendPropertyChanged("Sub_Company_Income_Ledger_Code");
                    this.OnSub_Company_Income_Ledger_CodeChanged();
                }
            }
        }

        [Column(Storage = "_Sub_Company_Royalty_Ledger_Code", DbType = "VarChar(20)")]
        public string Sub_Company_Royalty_Ledger_Code
        {
            get
            {
                return this._Sub_Company_Royalty_Ledger_Code;
            }
            set
            {
                if ((this._Sub_Company_Royalty_Ledger_Code != value))
                {
                    this.OnSub_Company_Royalty_Ledger_CodeChanging(value);
                    this.SendPropertyChanging();
                    this._Sub_Company_Royalty_Ledger_Code = value;
                    this.SendPropertyChanged("Sub_Company_Royalty_Ledger_Code");
                    this.OnSub_Company_Royalty_Ledger_CodeChanged();
                }
            }
        }

        [Column(Storage = "_Sub_Company_Default_Opening_Hours_ID", DbType = "Int")]
        public System.Nullable<int> Sub_Company_Default_Opening_Hours_ID
        {
            get
            {
                return this._Sub_Company_Default_Opening_Hours_ID;
            }
            set
            {
                if ((this._Sub_Company_Default_Opening_Hours_ID != value))
                {
                    this.OnSub_Company_Default_Opening_Hours_IDChanging(value);
                    this.SendPropertyChanging();
                    this._Sub_Company_Default_Opening_Hours_ID = value;
                    this.SendPropertyChanged("Sub_Company_Default_Opening_Hours_ID");
                    this.OnSub_Company_Default_Opening_Hours_IDChanged();
                }
            }
        }

        [Column(Storage = "_Sub_Company_Account_Name", DbType = "VarChar(32)")]
        public string Sub_Company_Account_Name
        {
            get
            {
                return this._Sub_Company_Account_Name;
            }
            set
            {
                if ((this._Sub_Company_Account_Name != value))
                {
                    this.OnSub_Company_Account_NameChanging(value);
                    this.SendPropertyChanging();
                    this._Sub_Company_Account_Name = value;
                    this.SendPropertyChanged("Sub_Company_Account_Name");
                    this.OnSub_Company_Account_NameChanged();
                }
            }
        }

        [Column(Storage = "_Sub_Company_Sort_Code", DbType = "VarChar(8)")]
        public string Sub_Company_Sort_Code
        {
            get
            {
                return this._Sub_Company_Sort_Code;
            }
            set
            {
                if ((this._Sub_Company_Sort_Code != value))
                {
                    this.OnSub_Company_Sort_CodeChanging(value);
                    this.SendPropertyChanging();
                    this._Sub_Company_Sort_Code = value;
                    this.SendPropertyChanged("Sub_Company_Sort_Code");
                    this.OnSub_Company_Sort_CodeChanged();
                }
            }
        }

        [Column(Storage = "_Sub_Company_Account_No", DbType = "VarChar(12)")]
        public string Sub_Company_Account_No
        {
            get
            {
                return this._Sub_Company_Account_No;
            }
            set
            {
                if ((this._Sub_Company_Account_No != value))
                {
                    this.OnSub_Company_Account_NoChanging(value);
                    this.SendPropertyChanging();
                    this._Sub_Company_Account_No = value;
                    this.SendPropertyChanged("Sub_Company_Account_No");
                    this.OnSub_Company_Account_NoChanged();
                }
            }
        }

        [Column(Storage = "_Sub_Company_EDI_Outbox", DbType = "VarChar(50)")]
        public string Sub_Company_EDI_Outbox
        {
            get
            {
                return this._Sub_Company_EDI_Outbox;
            }
            set
            {
                if ((this._Sub_Company_EDI_Outbox != value))
                {
                    this.OnSub_Company_EDI_OutboxChanging(value);
                    this.SendPropertyChanging();
                    this._Sub_Company_EDI_Outbox = value;
                    this.SendPropertyChanged("Sub_Company_EDI_Outbox");
                    this.OnSub_Company_EDI_OutboxChanged();
                }
            }
        }

        [Column(Storage = "_Sub_Company_Leisure_Data_Brewary_Code", DbType = "VarChar(50)")]
        public string Sub_Company_Leisure_Data_Brewary_Code
        {
            get
            {
                return this._Sub_Company_Leisure_Data_Brewary_Code;
            }
            set
            {
                if ((this._Sub_Company_Leisure_Data_Brewary_Code != value))
                {
                    this.OnSub_Company_Leisure_Data_Brewary_CodeChanging(value);
                    this.SendPropertyChanging();
                    this._Sub_Company_Leisure_Data_Brewary_Code = value;
                    this.SendPropertyChanged("Sub_Company_Leisure_Data_Brewary_Code");
                    this.OnSub_Company_Leisure_Data_Brewary_CodeChanged();
                }
            }
        }

        [Column(Storage = "_Sub_Company_Force_Leisure_Data_To_Enterprise", DbType = "Bit")]
        public System.Nullable<bool> Sub_Company_Force_Leisure_Data_To_Enterprise
        {
            get
            {
                return this._Sub_Company_Force_Leisure_Data_To_Enterprise;
            }
            set
            {
                if ((this._Sub_Company_Force_Leisure_Data_To_Enterprise != value))
                {
                    this.OnSub_Company_Force_Leisure_Data_To_EnterpriseChanging(value);
                    this.SendPropertyChanging();
                    this._Sub_Company_Force_Leisure_Data_To_Enterprise = value;
                    this.SendPropertyChanged("Sub_Company_Force_Leisure_Data_To_Enterprise");
                    this.OnSub_Company_Force_Leisure_Data_To_EnterpriseChanged();
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
