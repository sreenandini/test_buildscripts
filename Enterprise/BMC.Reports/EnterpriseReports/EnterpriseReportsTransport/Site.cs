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
    [Table(Name = "dbo.Site")]
    public partial class Site : INotifyPropertyChanging, INotifyPropertyChanged
    {

        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);

        private int _Site_ID;

        private string _Site_Reference;

        private System.Nullable<int> _Sub_Company_ID;

        private System.Nullable<int> _Staff_ID;

        private System.Nullable<bool> _Staff_ID_Default;

        private System.Nullable<int> _Access_Key_ID;

        private System.Nullable<bool> _Access_Key_ID_Default;

        private System.Nullable<int> _Terms_Group_ID;

        private System.Nullable<bool> _Terms_Group_ID_Default;

        private System.Nullable<int> _Computer_Build_ID;

        private System.Nullable<int> _Standard_Opening_Hours_ID;

        private System.Nullable<int> _Secondary_Sub_Company_ID;

        private System.Nullable<bool> _Site_London_Rent;

        private string _Site_Supplier_Area;

        private string _Site_Supplier_Service_Area;

        private string _Site_Grade;

        private System.Nullable<int> _Site_Permit_Needed;

        private string _Site_Code;

        private string _Site_Supplier_Code;

        private string _Site_Name;

        private string _Site_Address;

        private string _Site_Postcode;

        private string _Site_Phone_No;

        private string _Site_Fax_No;

        private string _Site_Email_Address;

        private string _Site_Manager;

        private string _Site_Computer_Name;

        private string _Site_Open_Monday;

        private string _Site_Open_Tuesday;

        private string _Site_Open_Wednesday;

        private string _Site_Open_Thursday;

        private string _Site_Open_Friday;

        private string _Site_Open_Saturday;

        private string _Site_Open_Sunday;

        private string _Site_Invoice_Address;

        private string _Site_Invoice_Postcode;

        private string _Site_Invoice_Name;

        private string _Site_Dial_Up_Number;

        private string _Site_Username;

        private string _Site_Password;

        private string _Site_Domain;

        private string _Site_Local_Inbox;

        private string _Site_Local_Outbox;

        private string _Site_Remote_Inbox;

        private string _Site_Remote_Outbox;

        private string _Site_FTPServerAddress;

        private System.Nullable<int> _Site_ConnType;

        private string _Site_Price_Per_Play;

        private System.Nullable<bool> _Site_Price_Per_Play_Default;

        private string _Site_Jackpot;

        private System.Nullable<bool> _Site_Jackpot_Default;

        private string _Site_Percentage_Payout;

        private System.Nullable<bool> _Site_Percentage_Payout_Default;

        private string _Site_Start_Date;

        private string _Site_End_Date;

        private string _Sage_Account_Ref;

        private string _Site_Memo;

        private string _Site_Company_Code;

        private System.Nullable<int> _Site_Previous_Sub_Company_ID;

        private System.Nullable<System.DateTime> _Site_Licensee_Commenced_Date;

        private string _Site_Licensee_Agreement_Type;

        private System.Nullable<int> _Depot_ID;

        private System.Nullable<int> _Service_Depot_ID;

        private System.Nullable<bool> _Site_VAT_Exempt_Flag;

        private System.Nullable<float> _Site_Company_Target;

        private System.Nullable<int> _Site_Company_Barrellage;

        private string _Site_Image_Reference;

        private string _Site_Image_Reference_2;

        private string _Site_Trade_Type;

        private System.Nullable<int> _Sub_Company_Region_ID;

        private System.Nullable<int> _Sub_Company_Area_ID;

        private System.Nullable<int> _Sub_Company_District_ID;

        private string _Site_Address_1;

        private string _Site_Address_2;

        private string _Site_Address_3;

        private string _Site_Address_4;

        private string _Site_Address_5;

        private System.Nullable<bool> _Site_TX_Collection;

        private System.Nullable<bool> _Site_TX_Collection_Use_Default;

        private System.Nullable<bool> _Site_TX_Movement;

        private System.Nullable<bool> _Site_TX_Movement_Use_Default;

        private System.Nullable<bool> _Site_TX_EDC;

        private System.Nullable<bool> _Site_TX_EDC_Use_Detault;

        private System.Nullable<int> _Site_TX_Format;

        private System.Nullable<bool> _Site_TX_Format_Use_Default;

        private System.Nullable<bool> _Site_RX_Collection;

        private System.Nullable<bool> _Site_RX_Collection_Use_Default;

        private System.Nullable<bool> _Site_RX_Movement;

        private System.Nullable<bool> _Site_RX_Movement_Use_Default;

        private System.Nullable<bool> _Site_RX_EDC;

        private System.Nullable<bool> _Site_RX_EDC_Use_Detault;

        private System.Nullable<int> _Site_RX_Format;

        private System.Nullable<bool> _Site_RX_Format_Use_Default;

        private string _NT_Phone_Book_Entry;

        private System.Nullable<int> _Next_Secondary_Sub_Company_ID;

        private string _Site_Secondary_Sub_Company_Changeover;

        private string _Site_GPS_Location;

        private string _Site_Stop_Importing_EDI_On;

        private string _Site_Non_Trading_Period_From;

        private string _Site_Non_Trading_Period_To;

        private System.Nullable<int> _Service_Area_ID;

        private System.Nullable<int> _Service_Supplier_ID;

        private System.Nullable<int> _Next_Sub_Company_ID;

        private string _Next_Sub_Company_Change_Date;

        private string _Previous_Sub_Company_Change_Date;

        private System.Nullable<int> _Previous_Secondary_Sub_Company_ID;

        private string _Previous_Secondary_Sub_Company_Change_Date;

        private System.Nullable<int> _Site_Honeyframe_EDI;

        private System.Nullable<int> _Site_Datapak_Protocol;

        private System.Nullable<bool> _Site_Is_FreeFloat;

        private int _Site_Classification_ID;

        private System.Nullable<System.DateTime> _Site_Licensee_Agreement_End_Date;

        private string _Site_Licence_Number;

        private System.Nullable<short> _Site_Application;

        private string _Region;

        private string _WebURL;

        private string _ConnectionString;

        private System.Xml.Linq.XElement _Site_Status;

        private System.Nullable<System.DateTime> _Last_Updated_Time;

        private bool _Apply_Retailer_Share;

        private System.Nullable<int> _Site_Status_ID;

        private System.Nullable<System.DateTime> _Site_Inactive_Date;

        private System.Nullable<int> _NGA_Machine_ID;

        private System.Nullable<int> _Site_Setting_Profile_ID;

        private string _SiteStatus;

        private string _ExchangeKey;

        private string _Site_Fiscal_Code;

        private string _Site_Street_Number;

        private string _Site_Province;

        private string _Site_Municipality;

        private string _Site_Cadastral_Code;

        private System.Nullable<int> _Site_Area;

        private System.Nullable<int> _Site_Location_Type;

        private System.Nullable<int> _Site_Closed;

        private string _Site_Workstation;

        #region Extensibility Method Definitions
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
        partial void OnSite_IDChanging(int value);
        partial void OnSite_IDChanged();
        partial void OnSite_ReferenceChanging(string value);
        partial void OnSite_ReferenceChanged();
        partial void OnSub_Company_IDChanging(System.Nullable<int> value);
        partial void OnSub_Company_IDChanged();
        partial void OnStaff_IDChanging(System.Nullable<int> value);
        partial void OnStaff_IDChanged();
        partial void OnStaff_ID_DefaultChanging(System.Nullable<bool> value);
        partial void OnStaff_ID_DefaultChanged();
        partial void OnAccess_Key_IDChanging(System.Nullable<int> value);
        partial void OnAccess_Key_IDChanged();
        partial void OnAccess_Key_ID_DefaultChanging(System.Nullable<bool> value);
        partial void OnAccess_Key_ID_DefaultChanged();
        partial void OnTerms_Group_IDChanging(System.Nullable<int> value);
        partial void OnTerms_Group_IDChanged();
        partial void OnTerms_Group_ID_DefaultChanging(System.Nullable<bool> value);
        partial void OnTerms_Group_ID_DefaultChanged();
        partial void OnComputer_Build_IDChanging(System.Nullable<int> value);
        partial void OnComputer_Build_IDChanged();
        partial void OnStandard_Opening_Hours_IDChanging(System.Nullable<int> value);
        partial void OnStandard_Opening_Hours_IDChanged();
        partial void OnSecondary_Sub_Company_IDChanging(System.Nullable<int> value);
        partial void OnSecondary_Sub_Company_IDChanged();
        partial void OnSite_London_RentChanging(System.Nullable<bool> value);
        partial void OnSite_London_RentChanged();
        partial void OnSite_Supplier_AreaChanging(string value);
        partial void OnSite_Supplier_AreaChanged();
        partial void OnSite_Supplier_Service_AreaChanging(string value);
        partial void OnSite_Supplier_Service_AreaChanged();
        partial void OnSite_GradeChanging(string value);
        partial void OnSite_GradeChanged();
        partial void OnSite_Permit_NeededChanging(System.Nullable<int> value);
        partial void OnSite_Permit_NeededChanged();
        partial void OnSite_CodeChanging(string value);
        partial void OnSite_CodeChanged();
        partial void OnSite_Supplier_CodeChanging(string value);
        partial void OnSite_Supplier_CodeChanged();
        partial void OnSite_NameChanging(string value);
        partial void OnSite_NameChanged();
        partial void OnSite_AddressChanging(string value);
        partial void OnSite_AddressChanged();
        partial void OnSite_PostcodeChanging(string value);
        partial void OnSite_PostcodeChanged();
        partial void OnSite_Phone_NoChanging(string value);
        partial void OnSite_Phone_NoChanged();
        partial void OnSite_Fax_NoChanging(string value);
        partial void OnSite_Fax_NoChanged();
        partial void OnSite_Email_AddressChanging(string value);
        partial void OnSite_Email_AddressChanged();
        partial void OnSite_ManagerChanging(string value);
        partial void OnSite_ManagerChanged();
        partial void OnSite_Computer_NameChanging(string value);
        partial void OnSite_Computer_NameChanged();
        partial void OnSite_Open_MondayChanging(string value);
        partial void OnSite_Open_MondayChanged();
        partial void OnSite_Open_TuesdayChanging(string value);
        partial void OnSite_Open_TuesdayChanged();
        partial void OnSite_Open_WednesdayChanging(string value);
        partial void OnSite_Open_WednesdayChanged();
        partial void OnSite_Open_ThursdayChanging(string value);
        partial void OnSite_Open_ThursdayChanged();
        partial void OnSite_Open_FridayChanging(string value);
        partial void OnSite_Open_FridayChanged();
        partial void OnSite_Open_SaturdayChanging(string value);
        partial void OnSite_Open_SaturdayChanged();
        partial void OnSite_Open_SundayChanging(string value);
        partial void OnSite_Open_SundayChanged();
        partial void OnSite_Invoice_AddressChanging(string value);
        partial void OnSite_Invoice_AddressChanged();
        partial void OnSite_Invoice_PostcodeChanging(string value);
        partial void OnSite_Invoice_PostcodeChanged();
        partial void OnSite_Invoice_NameChanging(string value);
        partial void OnSite_Invoice_NameChanged();
        partial void OnSite_Dial_Up_NumberChanging(string value);
        partial void OnSite_Dial_Up_NumberChanged();
        partial void OnSite_UsernameChanging(string value);
        partial void OnSite_UsernameChanged();
        partial void OnSite_PasswordChanging(string value);
        partial void OnSite_PasswordChanged();
        partial void OnSite_DomainChanging(string value);
        partial void OnSite_DomainChanged();
        partial void OnSite_Local_InboxChanging(string value);
        partial void OnSite_Local_InboxChanged();
        partial void OnSite_Local_OutboxChanging(string value);
        partial void OnSite_Local_OutboxChanged();
        partial void OnSite_Remote_InboxChanging(string value);
        partial void OnSite_Remote_InboxChanged();
        partial void OnSite_Remote_OutboxChanging(string value);
        partial void OnSite_Remote_OutboxChanged();
        partial void OnSite_FTPServerAddressChanging(string value);
        partial void OnSite_FTPServerAddressChanged();
        partial void OnSite_ConnTypeChanging(System.Nullable<int> value);
        partial void OnSite_ConnTypeChanged();
        partial void OnSite_Price_Per_PlayChanging(string value);
        partial void OnSite_Price_Per_PlayChanged();
        partial void OnSite_Price_Per_Play_DefaultChanging(System.Nullable<bool> value);
        partial void OnSite_Price_Per_Play_DefaultChanged();
        partial void OnSite_JackpotChanging(string value);
        partial void OnSite_JackpotChanged();
        partial void OnSite_Jackpot_DefaultChanging(System.Nullable<bool> value);
        partial void OnSite_Jackpot_DefaultChanged();
        partial void OnSite_Percentage_PayoutChanging(string value);
        partial void OnSite_Percentage_PayoutChanged();
        partial void OnSite_Percentage_Payout_DefaultChanging(System.Nullable<bool> value);
        partial void OnSite_Percentage_Payout_DefaultChanged();
        partial void OnSite_Start_DateChanging(string value);
        partial void OnSite_Start_DateChanged();
        partial void OnSite_End_DateChanging(string value);
        partial void OnSite_End_DateChanged();
        partial void OnSage_Account_RefChanging(string value);
        partial void OnSage_Account_RefChanged();
        partial void OnSite_MemoChanging(string value);
        partial void OnSite_MemoChanged();
        partial void OnSite_Company_CodeChanging(string value);
        partial void OnSite_Company_CodeChanged();
        partial void OnSite_Previous_Sub_Company_IDChanging(System.Nullable<int> value);
        partial void OnSite_Previous_Sub_Company_IDChanged();
        partial void OnSite_Licensee_Commenced_DateChanging(System.Nullable<System.DateTime> value);
        partial void OnSite_Licensee_Commenced_DateChanged();
        partial void OnSite_Licensee_Agreement_TypeChanging(string value);
        partial void OnSite_Licensee_Agreement_TypeChanged();
        partial void OnDepot_IDChanging(System.Nullable<int> value);
        partial void OnDepot_IDChanged();
        partial void OnService_Depot_IDChanging(System.Nullable<int> value);
        partial void OnService_Depot_IDChanged();
        partial void OnSite_VAT_Exempt_FlagChanging(System.Nullable<bool> value);
        partial void OnSite_VAT_Exempt_FlagChanged();
        partial void OnSite_Company_TargetChanging(System.Nullable<float> value);
        partial void OnSite_Company_TargetChanged();
        partial void OnSite_Company_BarrellageChanging(System.Nullable<int> value);
        partial void OnSite_Company_BarrellageChanged();
        partial void OnSite_Image_ReferenceChanging(string value);
        partial void OnSite_Image_ReferenceChanged();
        partial void OnSite_Image_Reference_2Changing(string value);
        partial void OnSite_Image_Reference_2Changed();
        partial void OnSite_Trade_TypeChanging(string value);
        partial void OnSite_Trade_TypeChanged();
        partial void OnSub_Company_Region_IDChanging(System.Nullable<int> value);
        partial void OnSub_Company_Region_IDChanged();
        partial void OnSub_Company_Area_IDChanging(System.Nullable<int> value);
        partial void OnSub_Company_Area_IDChanged();
        partial void OnSub_Company_District_IDChanging(System.Nullable<int> value);
        partial void OnSub_Company_District_IDChanged();
        partial void OnSite_Address_1Changing(string value);
        partial void OnSite_Address_1Changed();
        partial void OnSite_Address_2Changing(string value);
        partial void OnSite_Address_2Changed();
        partial void OnSite_Address_3Changing(string value);
        partial void OnSite_Address_3Changed();
        partial void OnSite_Address_4Changing(string value);
        partial void OnSite_Address_4Changed();
        partial void OnSite_Address_5Changing(string value);
        partial void OnSite_Address_5Changed();
        partial void OnSite_TX_CollectionChanging(System.Nullable<bool> value);
        partial void OnSite_TX_CollectionChanged();
        partial void OnSite_TX_Collection_Use_DefaultChanging(System.Nullable<bool> value);
        partial void OnSite_TX_Collection_Use_DefaultChanged();
        partial void OnSite_TX_MovementChanging(System.Nullable<bool> value);
        partial void OnSite_TX_MovementChanged();
        partial void OnSite_TX_Movement_Use_DefaultChanging(System.Nullable<bool> value);
        partial void OnSite_TX_Movement_Use_DefaultChanged();
        partial void OnSite_TX_EDCChanging(System.Nullable<bool> value);
        partial void OnSite_TX_EDCChanged();
        partial void OnSite_TX_EDC_Use_DetaultChanging(System.Nullable<bool> value);
        partial void OnSite_TX_EDC_Use_DetaultChanged();
        partial void OnSite_TX_FormatChanging(System.Nullable<int> value);
        partial void OnSite_TX_FormatChanged();
        partial void OnSite_TX_Format_Use_DefaultChanging(System.Nullable<bool> value);
        partial void OnSite_TX_Format_Use_DefaultChanged();
        partial void OnSite_RX_CollectionChanging(System.Nullable<bool> value);
        partial void OnSite_RX_CollectionChanged();
        partial void OnSite_RX_Collection_Use_DefaultChanging(System.Nullable<bool> value);
        partial void OnSite_RX_Collection_Use_DefaultChanged();
        partial void OnSite_RX_MovementChanging(System.Nullable<bool> value);
        partial void OnSite_RX_MovementChanged();
        partial void OnSite_RX_Movement_Use_DefaultChanging(System.Nullable<bool> value);
        partial void OnSite_RX_Movement_Use_DefaultChanged();
        partial void OnSite_RX_EDCChanging(System.Nullable<bool> value);
        partial void OnSite_RX_EDCChanged();
        partial void OnSite_RX_EDC_Use_DetaultChanging(System.Nullable<bool> value);
        partial void OnSite_RX_EDC_Use_DetaultChanged();
        partial void OnSite_RX_FormatChanging(System.Nullable<int> value);
        partial void OnSite_RX_FormatChanged();
        partial void OnSite_RX_Format_Use_DefaultChanging(System.Nullable<bool> value);
        partial void OnSite_RX_Format_Use_DefaultChanged();
        partial void OnNT_Phone_Book_EntryChanging(string value);
        partial void OnNT_Phone_Book_EntryChanged();
        partial void OnNext_Secondary_Sub_Company_IDChanging(System.Nullable<int> value);
        partial void OnNext_Secondary_Sub_Company_IDChanged();
        partial void OnSite_Secondary_Sub_Company_ChangeoverChanging(string value);
        partial void OnSite_Secondary_Sub_Company_ChangeoverChanged();
        partial void OnSite_GPS_LocationChanging(string value);
        partial void OnSite_GPS_LocationChanged();
        partial void OnSite_Stop_Importing_EDI_OnChanging(string value);
        partial void OnSite_Stop_Importing_EDI_OnChanged();
        partial void OnSite_Non_Trading_Period_FromChanging(string value);
        partial void OnSite_Non_Trading_Period_FromChanged();
        partial void OnSite_Non_Trading_Period_ToChanging(string value);
        partial void OnSite_Non_Trading_Period_ToChanged();
        partial void OnService_Area_IDChanging(System.Nullable<int> value);
        partial void OnService_Area_IDChanged();
        partial void OnService_Supplier_IDChanging(System.Nullable<int> value);
        partial void OnService_Supplier_IDChanged();
        partial void OnNext_Sub_Company_IDChanging(System.Nullable<int> value);
        partial void OnNext_Sub_Company_IDChanged();
        partial void OnNext_Sub_Company_Change_DateChanging(string value);
        partial void OnNext_Sub_Company_Change_DateChanged();
        partial void OnPrevious_Sub_Company_Change_DateChanging(string value);
        partial void OnPrevious_Sub_Company_Change_DateChanged();
        partial void OnPrevious_Secondary_Sub_Company_IDChanging(System.Nullable<int> value);
        partial void OnPrevious_Secondary_Sub_Company_IDChanged();
        partial void OnPrevious_Secondary_Sub_Company_Change_DateChanging(string value);
        partial void OnPrevious_Secondary_Sub_Company_Change_DateChanged();
        partial void OnSite_Honeyframe_EDIChanging(System.Nullable<int> value);
        partial void OnSite_Honeyframe_EDIChanged();
        partial void OnSite_Datapak_ProtocolChanging(System.Nullable<int> value);
        partial void OnSite_Datapak_ProtocolChanged();
        partial void OnSite_Is_FreeFloatChanging(System.Nullable<bool> value);
        partial void OnSite_Is_FreeFloatChanged();
        partial void OnSite_Classification_IDChanging(int value);
        partial void OnSite_Classification_IDChanged();
        partial void OnSite_Licensee_Agreement_End_DateChanging(System.Nullable<System.DateTime> value);
        partial void OnSite_Licensee_Agreement_End_DateChanged();
        partial void OnSite_Licence_NumberChanging(string value);
        partial void OnSite_Licence_NumberChanged();
        partial void OnSite_ApplicationChanging(System.Nullable<short> value);
        partial void OnSite_ApplicationChanged();
        partial void OnRegionChanging(string value);
        partial void OnRegionChanged();
        partial void OnWebURLChanging(string value);
        partial void OnWebURLChanged();
        partial void OnConnectionStringChanging(string value);
        partial void OnConnectionStringChanged();
        partial void OnSite_StatusChanging(System.Xml.Linq.XElement value);
        partial void OnSite_StatusChanged();
        partial void OnLast_Updated_TimeChanging(System.Nullable<System.DateTime> value);
        partial void OnLast_Updated_TimeChanged();
        partial void OnApply_Retailer_ShareChanging(bool value);
        partial void OnApply_Retailer_ShareChanged();
        partial void OnSite_Status_IDChanging(System.Nullable<int> value);
        partial void OnSite_Status_IDChanged();
        partial void OnSite_Inactive_DateChanging(System.Nullable<System.DateTime> value);
        partial void OnSite_Inactive_DateChanged();
        partial void OnNGA_Machine_IDChanging(System.Nullable<int> value);
        partial void OnNGA_Machine_IDChanged();
        partial void OnSite_Setting_Profile_IDChanging(System.Nullable<int> value);
        partial void OnSite_Setting_Profile_IDChanged();
        partial void OnSiteStatusChanging(string value);
        partial void OnSiteStatusChanged();
        partial void OnExchangeKeyChanging(string value);
        partial void OnExchangeKeyChanged();
        partial void OnSite_Fiscal_CodeChanging(string value);
        partial void OnSite_Fiscal_CodeChanged();
        partial void OnSite_Street_NumberChanging(string value);
        partial void OnSite_Street_NumberChanged();
        partial void OnSite_ProvinceChanging(string value);
        partial void OnSite_ProvinceChanged();
        partial void OnSite_MunicipalityChanging(string value);
        partial void OnSite_MunicipalityChanged();
        partial void OnSite_Cadastral_CodeChanging(string value);
        partial void OnSite_Cadastral_CodeChanged();
        partial void OnSite_AreaChanging(System.Nullable<int> value);
        partial void OnSite_AreaChanged();
        partial void OnSite_Location_TypeChanging(System.Nullable<int> value);
        partial void OnSite_Location_TypeChanged();
        partial void OnSite_ClosedChanging(System.Nullable<int> value);
        partial void OnSite_ClosedChanged();
        partial void OnSite_WorkstationChanging(string value);
        partial void OnSite_WorkstationChanged();
        #endregion

        public Site()
        {
            OnCreated();
        }

        [Column(Storage = "_Site_ID", AutoSync = AutoSync.OnInsert, DbType = "Int NOT NULL IDENTITY", IsPrimaryKey = true, IsDbGenerated = true)]
        public int Site_ID
        {
            get
            {
                return this._Site_ID;
            }
            set
            {
                if ((this._Site_ID != value))
                {
                    this.OnSite_IDChanging(value);
                    this.SendPropertyChanging();
                    this._Site_ID = value;
                    this.SendPropertyChanged("Site_ID");
                    this.OnSite_IDChanged();
                }
            }
        }

        [Column(Storage = "_Site_Reference", DbType = "VarChar(50)")]
        public string Site_Reference
        {
            get
            {
                return this._Site_Reference;
            }
            set
            {
                if ((this._Site_Reference != value))
                {
                    this.OnSite_ReferenceChanging(value);
                    this.SendPropertyChanging();
                    this._Site_Reference = value;
                    this.SendPropertyChanged("Site_Reference");
                    this.OnSite_ReferenceChanged();
                }
            }
        }

        [Column(Storage = "_Sub_Company_ID", DbType = "Int")]
        public System.Nullable<int> Sub_Company_ID
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

        [Column(Storage = "_Computer_Build_ID", DbType = "Int")]
        public System.Nullable<int> Computer_Build_ID
        {
            get
            {
                return this._Computer_Build_ID;
            }
            set
            {
                if ((this._Computer_Build_ID != value))
                {
                    this.OnComputer_Build_IDChanging(value);
                    this.SendPropertyChanging();
                    this._Computer_Build_ID = value;
                    this.SendPropertyChanged("Computer_Build_ID");
                    this.OnComputer_Build_IDChanged();
                }
            }
        }

        [Column(Storage = "_Standard_Opening_Hours_ID", DbType = "Int")]
        public System.Nullable<int> Standard_Opening_Hours_ID
        {
            get
            {
                return this._Standard_Opening_Hours_ID;
            }
            set
            {
                if ((this._Standard_Opening_Hours_ID != value))
                {
                    this.OnStandard_Opening_Hours_IDChanging(value);
                    this.SendPropertyChanging();
                    this._Standard_Opening_Hours_ID = value;
                    this.SendPropertyChanged("Standard_Opening_Hours_ID");
                    this.OnStandard_Opening_Hours_IDChanged();
                }
            }
        }

        [Column(Storage = "_Secondary_Sub_Company_ID", DbType = "Int")]
        public System.Nullable<int> Secondary_Sub_Company_ID
        {
            get
            {
                return this._Secondary_Sub_Company_ID;
            }
            set
            {
                if ((this._Secondary_Sub_Company_ID != value))
                {
                    this.OnSecondary_Sub_Company_IDChanging(value);
                    this.SendPropertyChanging();
                    this._Secondary_Sub_Company_ID = value;
                    this.SendPropertyChanged("Secondary_Sub_Company_ID");
                    this.OnSecondary_Sub_Company_IDChanged();
                }
            }
        }

        [Column(Storage = "_Site_London_Rent", DbType = "Bit")]
        public System.Nullable<bool> Site_London_Rent
        {
            get
            {
                return this._Site_London_Rent;
            }
            set
            {
                if ((this._Site_London_Rent != value))
                {
                    this.OnSite_London_RentChanging(value);
                    this.SendPropertyChanging();
                    this._Site_London_Rent = value;
                    this.SendPropertyChanged("Site_London_Rent");
                    this.OnSite_London_RentChanged();
                }
            }
        }

        [Column(Storage = "_Site_Supplier_Area", DbType = "VarChar(50)")]
        public string Site_Supplier_Area
        {
            get
            {
                return this._Site_Supplier_Area;
            }
            set
            {
                if ((this._Site_Supplier_Area != value))
                {
                    this.OnSite_Supplier_AreaChanging(value);
                    this.SendPropertyChanging();
                    this._Site_Supplier_Area = value;
                    this.SendPropertyChanged("Site_Supplier_Area");
                    this.OnSite_Supplier_AreaChanged();
                }
            }
        }

        [Column(Storage = "_Site_Supplier_Service_Area", DbType = "VarChar(50)")]
        public string Site_Supplier_Service_Area
        {
            get
            {
                return this._Site_Supplier_Service_Area;
            }
            set
            {
                if ((this._Site_Supplier_Service_Area != value))
                {
                    this.OnSite_Supplier_Service_AreaChanging(value);
                    this.SendPropertyChanging();
                    this._Site_Supplier_Service_Area = value;
                    this.SendPropertyChanged("Site_Supplier_Service_Area");
                    this.OnSite_Supplier_Service_AreaChanged();
                }
            }
        }

        [Column(Storage = "_Site_Grade", DbType = "VarChar(10)")]
        public string Site_Grade
        {
            get
            {
                return this._Site_Grade;
            }
            set
            {
                if ((this._Site_Grade != value))
                {
                    this.OnSite_GradeChanging(value);
                    this.SendPropertyChanging();
                    this._Site_Grade = value;
                    this.SendPropertyChanged("Site_Grade");
                    this.OnSite_GradeChanged();
                }
            }
        }

        [Column(Storage = "_Site_Permit_Needed", DbType = "Int")]
        public System.Nullable<int> Site_Permit_Needed
        {
            get
            {
                return this._Site_Permit_Needed;
            }
            set
            {
                if ((this._Site_Permit_Needed != value))
                {
                    this.OnSite_Permit_NeededChanging(value);
                    this.SendPropertyChanging();
                    this._Site_Permit_Needed = value;
                    this.SendPropertyChanged("Site_Permit_Needed");
                    this.OnSite_Permit_NeededChanged();
                }
            }
        }

        [Column(Storage = "_Site_Code", DbType = "VarChar(50)")]
        public string Site_Code
        {
            get
            {
                return this._Site_Code;
            }
            set
            {
                if ((this._Site_Code != value))
                {
                    this.OnSite_CodeChanging(value);
                    this.SendPropertyChanging();
                    this._Site_Code = value;
                    this.SendPropertyChanged("Site_Code");
                    this.OnSite_CodeChanged();
                }
            }
        }

        [Column(Storage = "_Site_Supplier_Code", DbType = "VarChar(50)")]
        public string Site_Supplier_Code
        {
            get
            {
                return this._Site_Supplier_Code;
            }
            set
            {
                if ((this._Site_Supplier_Code != value))
                {
                    this.OnSite_Supplier_CodeChanging(value);
                    this.SendPropertyChanging();
                    this._Site_Supplier_Code = value;
                    this.SendPropertyChanged("Site_Supplier_Code");
                    this.OnSite_Supplier_CodeChanged();
                }
            }
        }

        [Column(Storage = "_Site_Name", DbType = "VarChar(50)")]
        public string Site_Name
        {
            get
            {
                return this._Site_Name;
            }
            set
            {
                if ((this._Site_Name != value))
                {
                    this.OnSite_NameChanging(value);
                    this.SendPropertyChanging();
                    this._Site_Name = value;
                    this.SendPropertyChanged("Site_Name");
                    this.OnSite_NameChanged();
                }
            }
        }

        [Column(Storage = "_Site_Address", DbType = "NText", UpdateCheck = UpdateCheck.Never)]
        public string Site_Address
        {
            get
            {
                return this._Site_Address;
            }
            set
            {
                if ((this._Site_Address != value))
                {
                    this.OnSite_AddressChanging(value);
                    this.SendPropertyChanging();
                    this._Site_Address = value;
                    this.SendPropertyChanged("Site_Address");
                    this.OnSite_AddressChanged();
                }
            }
        }

        [Column(Storage = "_Site_Postcode", DbType = "VarChar(15)")]
        public string Site_Postcode
        {
            get
            {
                return this._Site_Postcode;
            }
            set
            {
                if ((this._Site_Postcode != value))
                {
                    this.OnSite_PostcodeChanging(value);
                    this.SendPropertyChanging();
                    this._Site_Postcode = value;
                    this.SendPropertyChanged("Site_Postcode");
                    this.OnSite_PostcodeChanged();
                }
            }
        }

        [Column(Storage = "_Site_Phone_No", DbType = "VarChar(15)")]
        public string Site_Phone_No
        {
            get
            {
                return this._Site_Phone_No;
            }
            set
            {
                if ((this._Site_Phone_No != value))
                {
                    this.OnSite_Phone_NoChanging(value);
                    this.SendPropertyChanging();
                    this._Site_Phone_No = value;
                    this.SendPropertyChanged("Site_Phone_No");
                    this.OnSite_Phone_NoChanged();
                }
            }
        }

        [Column(Storage = "_Site_Fax_No", DbType = "VarChar(15)")]
        public string Site_Fax_No
        {
            get
            {
                return this._Site_Fax_No;
            }
            set
            {
                if ((this._Site_Fax_No != value))
                {
                    this.OnSite_Fax_NoChanging(value);
                    this.SendPropertyChanging();
                    this._Site_Fax_No = value;
                    this.SendPropertyChanged("Site_Fax_No");
                    this.OnSite_Fax_NoChanged();
                }
            }
        }

        [Column(Storage = "_Site_Email_Address", DbType = "VarChar(100)")]
        public string Site_Email_Address
        {
            get
            {
                return this._Site_Email_Address;
            }
            set
            {
                if ((this._Site_Email_Address != value))
                {
                    this.OnSite_Email_AddressChanging(value);
                    this.SendPropertyChanging();
                    this._Site_Email_Address = value;
                    this.SendPropertyChanged("Site_Email_Address");
                    this.OnSite_Email_AddressChanged();
                }
            }
        }

        [Column(Storage = "_Site_Manager", DbType = "VarChar(50)")]
        public string Site_Manager
        {
            get
            {
                return this._Site_Manager;
            }
            set
            {
                if ((this._Site_Manager != value))
                {
                    this.OnSite_ManagerChanging(value);
                    this.SendPropertyChanging();
                    this._Site_Manager = value;
                    this.SendPropertyChanged("Site_Manager");
                    this.OnSite_ManagerChanged();
                }
            }
        }

        [Column(Storage = "_Site_Computer_Name", DbType = "VarChar(50)")]
        public string Site_Computer_Name
        {
            get
            {
                return this._Site_Computer_Name;
            }
            set
            {
                if ((this._Site_Computer_Name != value))
                {
                    this.OnSite_Computer_NameChanging(value);
                    this.SendPropertyChanging();
                    this._Site_Computer_Name = value;
                    this.SendPropertyChanged("Site_Computer_Name");
                    this.OnSite_Computer_NameChanged();
                }
            }
        }

        [Column(Storage = "_Site_Open_Monday", DbType = "VarChar(96)")]
        public string Site_Open_Monday
        {
            get
            {
                return this._Site_Open_Monday;
            }
            set
            {
                if ((this._Site_Open_Monday != value))
                {
                    this.OnSite_Open_MondayChanging(value);
                    this.SendPropertyChanging();
                    this._Site_Open_Monday = value;
                    this.SendPropertyChanged("Site_Open_Monday");
                    this.OnSite_Open_MondayChanged();
                }
            }
        }

        [Column(Storage = "_Site_Open_Tuesday", DbType = "VarChar(96)")]
        public string Site_Open_Tuesday
        {
            get
            {
                return this._Site_Open_Tuesday;
            }
            set
            {
                if ((this._Site_Open_Tuesday != value))
                {
                    this.OnSite_Open_TuesdayChanging(value);
                    this.SendPropertyChanging();
                    this._Site_Open_Tuesday = value;
                    this.SendPropertyChanged("Site_Open_Tuesday");
                    this.OnSite_Open_TuesdayChanged();
                }
            }
        }

        [Column(Storage = "_Site_Open_Wednesday", DbType = "VarChar(96)")]
        public string Site_Open_Wednesday
        {
            get
            {
                return this._Site_Open_Wednesday;
            }
            set
            {
                if ((this._Site_Open_Wednesday != value))
                {
                    this.OnSite_Open_WednesdayChanging(value);
                    this.SendPropertyChanging();
                    this._Site_Open_Wednesday = value;
                    this.SendPropertyChanged("Site_Open_Wednesday");
                    this.OnSite_Open_WednesdayChanged();
                }
            }
        }

        [Column(Storage = "_Site_Open_Thursday", DbType = "VarChar(96)")]
        public string Site_Open_Thursday
        {
            get
            {
                return this._Site_Open_Thursday;
            }
            set
            {
                if ((this._Site_Open_Thursday != value))
                {
                    this.OnSite_Open_ThursdayChanging(value);
                    this.SendPropertyChanging();
                    this._Site_Open_Thursday = value;
                    this.SendPropertyChanged("Site_Open_Thursday");
                    this.OnSite_Open_ThursdayChanged();
                }
            }
        }

        [Column(Storage = "_Site_Open_Friday", DbType = "VarChar(96)")]
        public string Site_Open_Friday
        {
            get
            {
                return this._Site_Open_Friday;
            }
            set
            {
                if ((this._Site_Open_Friday != value))
                {
                    this.OnSite_Open_FridayChanging(value);
                    this.SendPropertyChanging();
                    this._Site_Open_Friday = value;
                    this.SendPropertyChanged("Site_Open_Friday");
                    this.OnSite_Open_FridayChanged();
                }
            }
        }

        [Column(Storage = "_Site_Open_Saturday", DbType = "VarChar(96)")]
        public string Site_Open_Saturday
        {
            get
            {
                return this._Site_Open_Saturday;
            }
            set
            {
                if ((this._Site_Open_Saturday != value))
                {
                    this.OnSite_Open_SaturdayChanging(value);
                    this.SendPropertyChanging();
                    this._Site_Open_Saturday = value;
                    this.SendPropertyChanged("Site_Open_Saturday");
                    this.OnSite_Open_SaturdayChanged();
                }
            }
        }

        [Column(Storage = "_Site_Open_Sunday", DbType = "VarChar(96)")]
        public string Site_Open_Sunday
        {
            get
            {
                return this._Site_Open_Sunday;
            }
            set
            {
                if ((this._Site_Open_Sunday != value))
                {
                    this.OnSite_Open_SundayChanging(value);
                    this.SendPropertyChanging();
                    this._Site_Open_Sunday = value;
                    this.SendPropertyChanged("Site_Open_Sunday");
                    this.OnSite_Open_SundayChanged();
                }
            }
        }

        [Column(Storage = "_Site_Invoice_Address", DbType = "NText", UpdateCheck = UpdateCheck.Never)]
        public string Site_Invoice_Address
        {
            get
            {
                return this._Site_Invoice_Address;
            }
            set
            {
                if ((this._Site_Invoice_Address != value))
                {
                    this.OnSite_Invoice_AddressChanging(value);
                    this.SendPropertyChanging();
                    this._Site_Invoice_Address = value;
                    this.SendPropertyChanged("Site_Invoice_Address");
                    this.OnSite_Invoice_AddressChanged();
                }
            }
        }

        [Column(Storage = "_Site_Invoice_Postcode", DbType = "VarChar(15)")]
        public string Site_Invoice_Postcode
        {
            get
            {
                return this._Site_Invoice_Postcode;
            }
            set
            {
                if ((this._Site_Invoice_Postcode != value))
                {
                    this.OnSite_Invoice_PostcodeChanging(value);
                    this.SendPropertyChanging();
                    this._Site_Invoice_Postcode = value;
                    this.SendPropertyChanged("Site_Invoice_Postcode");
                    this.OnSite_Invoice_PostcodeChanged();
                }
            }
        }

        [Column(Storage = "_Site_Invoice_Name", DbType = "VarChar(50)")]
        public string Site_Invoice_Name
        {
            get
            {
                return this._Site_Invoice_Name;
            }
            set
            {
                if ((this._Site_Invoice_Name != value))
                {
                    this.OnSite_Invoice_NameChanging(value);
                    this.SendPropertyChanging();
                    this._Site_Invoice_Name = value;
                    this.SendPropertyChanged("Site_Invoice_Name");
                    this.OnSite_Invoice_NameChanged();
                }
            }
        }

        [Column(Storage = "_Site_Dial_Up_Number", DbType = "VarChar(50)")]
        public string Site_Dial_Up_Number
        {
            get
            {
                return this._Site_Dial_Up_Number;
            }
            set
            {
                if ((this._Site_Dial_Up_Number != value))
                {
                    this.OnSite_Dial_Up_NumberChanging(value);
                    this.SendPropertyChanging();
                    this._Site_Dial_Up_Number = value;
                    this.SendPropertyChanged("Site_Dial_Up_Number");
                    this.OnSite_Dial_Up_NumberChanged();
                }
            }
        }

        [Column(Storage = "_Site_Username", DbType = "VarChar(50)")]
        public string Site_Username
        {
            get
            {
                return this._Site_Username;
            }
            set
            {
                if ((this._Site_Username != value))
                {
                    this.OnSite_UsernameChanging(value);
                    this.SendPropertyChanging();
                    this._Site_Username = value;
                    this.SendPropertyChanged("Site_Username");
                    this.OnSite_UsernameChanged();
                }
            }
        }

        [Column(Storage = "_Site_Password", DbType = "VarChar(50)")]
        public string Site_Password
        {
            get
            {
                return this._Site_Password;
            }
            set
            {
                if ((this._Site_Password != value))
                {
                    this.OnSite_PasswordChanging(value);
                    this.SendPropertyChanging();
                    this._Site_Password = value;
                    this.SendPropertyChanged("Site_Password");
                    this.OnSite_PasswordChanged();
                }
            }
        }

        [Column(Storage = "_Site_Domain", DbType = "VarChar(50)")]
        public string Site_Domain
        {
            get
            {
                return this._Site_Domain;
            }
            set
            {
                if ((this._Site_Domain != value))
                {
                    this.OnSite_DomainChanging(value);
                    this.SendPropertyChanging();
                    this._Site_Domain = value;
                    this.SendPropertyChanged("Site_Domain");
                    this.OnSite_DomainChanged();
                }
            }
        }

        [Column(Storage = "_Site_Local_Inbox", DbType = "VarChar(100)")]
        public string Site_Local_Inbox
        {
            get
            {
                return this._Site_Local_Inbox;
            }
            set
            {
                if ((this._Site_Local_Inbox != value))
                {
                    this.OnSite_Local_InboxChanging(value);
                    this.SendPropertyChanging();
                    this._Site_Local_Inbox = value;
                    this.SendPropertyChanged("Site_Local_Inbox");
                    this.OnSite_Local_InboxChanged();
                }
            }
        }

        [Column(Storage = "_Site_Local_Outbox", DbType = "VarChar(100)")]
        public string Site_Local_Outbox
        {
            get
            {
                return this._Site_Local_Outbox;
            }
            set
            {
                if ((this._Site_Local_Outbox != value))
                {
                    this.OnSite_Local_OutboxChanging(value);
                    this.SendPropertyChanging();
                    this._Site_Local_Outbox = value;
                    this.SendPropertyChanged("Site_Local_Outbox");
                    this.OnSite_Local_OutboxChanged();
                }
            }
        }

        [Column(Storage = "_Site_Remote_Inbox", DbType = "VarChar(100)")]
        public string Site_Remote_Inbox
        {
            get
            {
                return this._Site_Remote_Inbox;
            }
            set
            {
                if ((this._Site_Remote_Inbox != value))
                {
                    this.OnSite_Remote_InboxChanging(value);
                    this.SendPropertyChanging();
                    this._Site_Remote_Inbox = value;
                    this.SendPropertyChanged("Site_Remote_Inbox");
                    this.OnSite_Remote_InboxChanged();
                }
            }
        }

        [Column(Storage = "_Site_Remote_Outbox", DbType = "VarChar(100)")]
        public string Site_Remote_Outbox
        {
            get
            {
                return this._Site_Remote_Outbox;
            }
            set
            {
                if ((this._Site_Remote_Outbox != value))
                {
                    this.OnSite_Remote_OutboxChanging(value);
                    this.SendPropertyChanging();
                    this._Site_Remote_Outbox = value;
                    this.SendPropertyChanged("Site_Remote_Outbox");
                    this.OnSite_Remote_OutboxChanged();
                }
            }
        }

        [Column(Storage = "_Site_FTPServerAddress", DbType = "VarChar(100)")]
        public string Site_FTPServerAddress
        {
            get
            {
                return this._Site_FTPServerAddress;
            }
            set
            {
                if ((this._Site_FTPServerAddress != value))
                {
                    this.OnSite_FTPServerAddressChanging(value);
                    this.SendPropertyChanging();
                    this._Site_FTPServerAddress = value;
                    this.SendPropertyChanged("Site_FTPServerAddress");
                    this.OnSite_FTPServerAddressChanged();
                }
            }
        }

        [Column(Storage = "_Site_ConnType", DbType = "Int")]
        public System.Nullable<int> Site_ConnType
        {
            get
            {
                return this._Site_ConnType;
            }
            set
            {
                if ((this._Site_ConnType != value))
                {
                    this.OnSite_ConnTypeChanging(value);
                    this.SendPropertyChanging();
                    this._Site_ConnType = value;
                    this.SendPropertyChanged("Site_ConnType");
                    this.OnSite_ConnTypeChanged();
                }
            }
        }

        [Column(Storage = "_Site_Price_Per_Play", DbType = "VarChar(50)")]
        public string Site_Price_Per_Play
        {
            get
            {
                return this._Site_Price_Per_Play;
            }
            set
            {
                if ((this._Site_Price_Per_Play != value))
                {
                    this.OnSite_Price_Per_PlayChanging(value);
                    this.SendPropertyChanging();
                    this._Site_Price_Per_Play = value;
                    this.SendPropertyChanged("Site_Price_Per_Play");
                    this.OnSite_Price_Per_PlayChanged();
                }
            }
        }

        [Column(Storage = "_Site_Price_Per_Play_Default", DbType = "Bit")]
        public System.Nullable<bool> Site_Price_Per_Play_Default
        {
            get
            {
                return this._Site_Price_Per_Play_Default;
            }
            set
            {
                if ((this._Site_Price_Per_Play_Default != value))
                {
                    this.OnSite_Price_Per_Play_DefaultChanging(value);
                    this.SendPropertyChanging();
                    this._Site_Price_Per_Play_Default = value;
                    this.SendPropertyChanged("Site_Price_Per_Play_Default");
                    this.OnSite_Price_Per_Play_DefaultChanged();
                }
            }
        }

        [Column(Storage = "_Site_Jackpot", DbType = "VarChar(50)")]
        public string Site_Jackpot
        {
            get
            {
                return this._Site_Jackpot;
            }
            set
            {
                if ((this._Site_Jackpot != value))
                {
                    this.OnSite_JackpotChanging(value);
                    this.SendPropertyChanging();
                    this._Site_Jackpot = value;
                    this.SendPropertyChanged("Site_Jackpot");
                    this.OnSite_JackpotChanged();
                }
            }
        }

        [Column(Storage = "_Site_Jackpot_Default", DbType = "Bit")]
        public System.Nullable<bool> Site_Jackpot_Default
        {
            get
            {
                return this._Site_Jackpot_Default;
            }
            set
            {
                if ((this._Site_Jackpot_Default != value))
                {
                    this.OnSite_Jackpot_DefaultChanging(value);
                    this.SendPropertyChanging();
                    this._Site_Jackpot_Default = value;
                    this.SendPropertyChanged("Site_Jackpot_Default");
                    this.OnSite_Jackpot_DefaultChanged();
                }
            }
        }

        [Column(Storage = "_Site_Percentage_Payout", DbType = "VarChar(50)")]
        public string Site_Percentage_Payout
        {
            get
            {
                return this._Site_Percentage_Payout;
            }
            set
            {
                if ((this._Site_Percentage_Payout != value))
                {
                    this.OnSite_Percentage_PayoutChanging(value);
                    this.SendPropertyChanging();
                    this._Site_Percentage_Payout = value;
                    this.SendPropertyChanged("Site_Percentage_Payout");
                    this.OnSite_Percentage_PayoutChanged();
                }
            }
        }

        [Column(Storage = "_Site_Percentage_Payout_Default", DbType = "Bit")]
        public System.Nullable<bool> Site_Percentage_Payout_Default
        {
            get
            {
                return this._Site_Percentage_Payout_Default;
            }
            set
            {
                if ((this._Site_Percentage_Payout_Default != value))
                {
                    this.OnSite_Percentage_Payout_DefaultChanging(value);
                    this.SendPropertyChanging();
                    this._Site_Percentage_Payout_Default = value;
                    this.SendPropertyChanged("Site_Percentage_Payout_Default");
                    this.OnSite_Percentage_Payout_DefaultChanged();
                }
            }
        }

        [Column(Storage = "_Site_Start_Date", DbType = "VarChar(30)")]
        public string Site_Start_Date
        {
            get
            {
                return this._Site_Start_Date;
            }
            set
            {
                if ((this._Site_Start_Date != value))
                {
                    this.OnSite_Start_DateChanging(value);
                    this.SendPropertyChanging();
                    this._Site_Start_Date = value;
                    this.SendPropertyChanged("Site_Start_Date");
                    this.OnSite_Start_DateChanged();
                }
            }
        }

        [Column(Storage = "_Site_End_Date", DbType = "VarChar(30)")]
        public string Site_End_Date
        {
            get
            {
                return this._Site_End_Date;
            }
            set
            {
                if ((this._Site_End_Date != value))
                {
                    this.OnSite_End_DateChanging(value);
                    this.SendPropertyChanging();
                    this._Site_End_Date = value;
                    this.SendPropertyChanged("Site_End_Date");
                    this.OnSite_End_DateChanged();
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

        [Column(Storage = "_Site_Memo", DbType = "NText", UpdateCheck = UpdateCheck.Never)]
        public string Site_Memo
        {
            get
            {
                return this._Site_Memo;
            }
            set
            {
                if ((this._Site_Memo != value))
                {
                    this.OnSite_MemoChanging(value);
                    this.SendPropertyChanging();
                    this._Site_Memo = value;
                    this.SendPropertyChanged("Site_Memo");
                    this.OnSite_MemoChanged();
                }
            }
        }

        [Column(Storage = "_Site_Company_Code", DbType = "VarChar(50)")]
        public string Site_Company_Code
        {
            get
            {
                return this._Site_Company_Code;
            }
            set
            {
                if ((this._Site_Company_Code != value))
                {
                    this.OnSite_Company_CodeChanging(value);
                    this.SendPropertyChanging();
                    this._Site_Company_Code = value;
                    this.SendPropertyChanged("Site_Company_Code");
                    this.OnSite_Company_CodeChanged();
                }
            }
        }

        [Column(Storage = "_Site_Previous_Sub_Company_ID", DbType = "Int")]
        public System.Nullable<int> Site_Previous_Sub_Company_ID
        {
            get
            {
                return this._Site_Previous_Sub_Company_ID;
            }
            set
            {
                if ((this._Site_Previous_Sub_Company_ID != value))
                {
                    this.OnSite_Previous_Sub_Company_IDChanging(value);
                    this.SendPropertyChanging();
                    this._Site_Previous_Sub_Company_ID = value;
                    this.SendPropertyChanged("Site_Previous_Sub_Company_ID");
                    this.OnSite_Previous_Sub_Company_IDChanged();
                }
            }
        }

        [Column(Storage = "_Site_Licensee_Commenced_Date", DbType = "SmallDateTime")]
        public System.Nullable<System.DateTime> Site_Licensee_Commenced_Date
        {
            get
            {
                return this._Site_Licensee_Commenced_Date;
            }
            set
            {
                if ((this._Site_Licensee_Commenced_Date != value))
                {
                    this.OnSite_Licensee_Commenced_DateChanging(value);
                    this.SendPropertyChanging();
                    this._Site_Licensee_Commenced_Date = value;
                    this.SendPropertyChanged("Site_Licensee_Commenced_Date");
                    this.OnSite_Licensee_Commenced_DateChanged();
                }
            }
        }

        [Column(Storage = "_Site_Licensee_Agreement_Type", DbType = "VarChar(50)")]
        public string Site_Licensee_Agreement_Type
        {
            get
            {
                return this._Site_Licensee_Agreement_Type;
            }
            set
            {
                if ((this._Site_Licensee_Agreement_Type != value))
                {
                    this.OnSite_Licensee_Agreement_TypeChanging(value);
                    this.SendPropertyChanging();
                    this._Site_Licensee_Agreement_Type = value;
                    this.SendPropertyChanged("Site_Licensee_Agreement_Type");
                    this.OnSite_Licensee_Agreement_TypeChanged();
                }
            }
        }

        [Column(Storage = "_Depot_ID", DbType = "Int")]
        public System.Nullable<int> Depot_ID
        {
            get
            {
                return this._Depot_ID;
            }
            set
            {
                if ((this._Depot_ID != value))
                {
                    this.OnDepot_IDChanging(value);
                    this.SendPropertyChanging();
                    this._Depot_ID = value;
                    this.SendPropertyChanged("Depot_ID");
                    this.OnDepot_IDChanged();
                }
            }
        }

        [Column(Storage = "_Service_Depot_ID", DbType = "Int")]
        public System.Nullable<int> Service_Depot_ID
        {
            get
            {
                return this._Service_Depot_ID;
            }
            set
            {
                if ((this._Service_Depot_ID != value))
                {
                    this.OnService_Depot_IDChanging(value);
                    this.SendPropertyChanging();
                    this._Service_Depot_ID = value;
                    this.SendPropertyChanged("Service_Depot_ID");
                    this.OnService_Depot_IDChanged();
                }
            }
        }

        [Column(Storage = "_Site_VAT_Exempt_Flag", DbType = "Bit")]
        public System.Nullable<bool> Site_VAT_Exempt_Flag
        {
            get
            {
                return this._Site_VAT_Exempt_Flag;
            }
            set
            {
                if ((this._Site_VAT_Exempt_Flag != value))
                {
                    this.OnSite_VAT_Exempt_FlagChanging(value);
                    this.SendPropertyChanging();
                    this._Site_VAT_Exempt_Flag = value;
                    this.SendPropertyChanged("Site_VAT_Exempt_Flag");
                    this.OnSite_VAT_Exempt_FlagChanged();
                }
            }
        }

        [Column(Storage = "_Site_Company_Target", DbType = "Real")]
        public System.Nullable<float> Site_Company_Target
        {
            get
            {
                return this._Site_Company_Target;
            }
            set
            {
                if ((this._Site_Company_Target != value))
                {
                    this.OnSite_Company_TargetChanging(value);
                    this.SendPropertyChanging();
                    this._Site_Company_Target = value;
                    this.SendPropertyChanged("Site_Company_Target");
                    this.OnSite_Company_TargetChanged();
                }
            }
        }

        [Column(Storage = "_Site_Company_Barrellage", DbType = "Int")]
        public System.Nullable<int> Site_Company_Barrellage
        {
            get
            {
                return this._Site_Company_Barrellage;
            }
            set
            {
                if ((this._Site_Company_Barrellage != value))
                {
                    this.OnSite_Company_BarrellageChanging(value);
                    this.SendPropertyChanging();
                    this._Site_Company_Barrellage = value;
                    this.SendPropertyChanged("Site_Company_Barrellage");
                    this.OnSite_Company_BarrellageChanged();
                }
            }
        }

        [Column(Storage = "_Site_Image_Reference", DbType = "VarChar(255)")]
        public string Site_Image_Reference
        {
            get
            {
                return this._Site_Image_Reference;
            }
            set
            {
                if ((this._Site_Image_Reference != value))
                {
                    this.OnSite_Image_ReferenceChanging(value);
                    this.SendPropertyChanging();
                    this._Site_Image_Reference = value;
                    this.SendPropertyChanged("Site_Image_Reference");
                    this.OnSite_Image_ReferenceChanged();
                }
            }
        }

        [Column(Storage = "_Site_Image_Reference_2", DbType = "VarChar(255)")]
        public string Site_Image_Reference_2
        {
            get
            {
                return this._Site_Image_Reference_2;
            }
            set
            {
                if ((this._Site_Image_Reference_2 != value))
                {
                    this.OnSite_Image_Reference_2Changing(value);
                    this.SendPropertyChanging();
                    this._Site_Image_Reference_2 = value;
                    this.SendPropertyChanged("Site_Image_Reference_2");
                    this.OnSite_Image_Reference_2Changed();
                }
            }
        }

        [Column(Storage = "_Site_Trade_Type", DbType = "VarChar(50)")]
        public string Site_Trade_Type
        {
            get
            {
                return this._Site_Trade_Type;
            }
            set
            {
                if ((this._Site_Trade_Type != value))
                {
                    this.OnSite_Trade_TypeChanging(value);
                    this.SendPropertyChanging();
                    this._Site_Trade_Type = value;
                    this.SendPropertyChanged("Site_Trade_Type");
                    this.OnSite_Trade_TypeChanged();
                }
            }
        }

        [Column(Storage = "_Sub_Company_Region_ID", DbType = "Int")]
        public System.Nullable<int> Sub_Company_Region_ID
        {
            get
            {
                return this._Sub_Company_Region_ID;
            }
            set
            {
                if ((this._Sub_Company_Region_ID != value))
                {
                    this.OnSub_Company_Region_IDChanging(value);
                    this.SendPropertyChanging();
                    this._Sub_Company_Region_ID = value;
                    this.SendPropertyChanged("Sub_Company_Region_ID");
                    this.OnSub_Company_Region_IDChanged();
                }
            }
        }

        [Column(Storage = "_Sub_Company_Area_ID", DbType = "Int")]
        public System.Nullable<int> Sub_Company_Area_ID
        {
            get
            {
                return this._Sub_Company_Area_ID;
            }
            set
            {
                if ((this._Sub_Company_Area_ID != value))
                {
                    this.OnSub_Company_Area_IDChanging(value);
                    this.SendPropertyChanging();
                    this._Sub_Company_Area_ID = value;
                    this.SendPropertyChanged("Sub_Company_Area_ID");
                    this.OnSub_Company_Area_IDChanged();
                }
            }
        }

        [Column(Storage = "_Sub_Company_District_ID", DbType = "Int")]
        public System.Nullable<int> Sub_Company_District_ID
        {
            get
            {
                return this._Sub_Company_District_ID;
            }
            set
            {
                if ((this._Sub_Company_District_ID != value))
                {
                    this.OnSub_Company_District_IDChanging(value);
                    this.SendPropertyChanging();
                    this._Sub_Company_District_ID = value;
                    this.SendPropertyChanged("Sub_Company_District_ID");
                    this.OnSub_Company_District_IDChanged();
                }
            }
        }

        [Column(Storage = "_Site_Address_1", DbType = "VarChar(50)")]
        public string Site_Address_1
        {
            get
            {
                return this._Site_Address_1;
            }
            set
            {
                if ((this._Site_Address_1 != value))
                {
                    this.OnSite_Address_1Changing(value);
                    this.SendPropertyChanging();
                    this._Site_Address_1 = value;
                    this.SendPropertyChanged("Site_Address_1");
                    this.OnSite_Address_1Changed();
                }
            }
        }

        [Column(Storage = "_Site_Address_2", DbType = "VarChar(50)")]
        public string Site_Address_2
        {
            get
            {
                return this._Site_Address_2;
            }
            set
            {
                if ((this._Site_Address_2 != value))
                {
                    this.OnSite_Address_2Changing(value);
                    this.SendPropertyChanging();
                    this._Site_Address_2 = value;
                    this.SendPropertyChanged("Site_Address_2");
                    this.OnSite_Address_2Changed();
                }
            }
        }

        [Column(Storage = "_Site_Address_3", DbType = "VarChar(50)")]
        public string Site_Address_3
        {
            get
            {
                return this._Site_Address_3;
            }
            set
            {
                if ((this._Site_Address_3 != value))
                {
                    this.OnSite_Address_3Changing(value);
                    this.SendPropertyChanging();
                    this._Site_Address_3 = value;
                    this.SendPropertyChanged("Site_Address_3");
                    this.OnSite_Address_3Changed();
                }
            }
        }

        [Column(Storage = "_Site_Address_4", DbType = "VarChar(50)")]
        public string Site_Address_4
        {
            get
            {
                return this._Site_Address_4;
            }
            set
            {
                if ((this._Site_Address_4 != value))
                {
                    this.OnSite_Address_4Changing(value);
                    this.SendPropertyChanging();
                    this._Site_Address_4 = value;
                    this.SendPropertyChanged("Site_Address_4");
                    this.OnSite_Address_4Changed();
                }
            }
        }

        [Column(Storage = "_Site_Address_5", DbType = "VarChar(50)")]
        public string Site_Address_5
        {
            get
            {
                return this._Site_Address_5;
            }
            set
            {
                if ((this._Site_Address_5 != value))
                {
                    this.OnSite_Address_5Changing(value);
                    this.SendPropertyChanging();
                    this._Site_Address_5 = value;
                    this.SendPropertyChanged("Site_Address_5");
                    this.OnSite_Address_5Changed();
                }
            }
        }

        [Column(Storage = "_Site_TX_Collection", DbType = "Bit")]
        public System.Nullable<bool> Site_TX_Collection
        {
            get
            {
                return this._Site_TX_Collection;
            }
            set
            {
                if ((this._Site_TX_Collection != value))
                {
                    this.OnSite_TX_CollectionChanging(value);
                    this.SendPropertyChanging();
                    this._Site_TX_Collection = value;
                    this.SendPropertyChanged("Site_TX_Collection");
                    this.OnSite_TX_CollectionChanged();
                }
            }
        }

        [Column(Storage = "_Site_TX_Collection_Use_Default", DbType = "Bit")]
        public System.Nullable<bool> Site_TX_Collection_Use_Default
        {
            get
            {
                return this._Site_TX_Collection_Use_Default;
            }
            set
            {
                if ((this._Site_TX_Collection_Use_Default != value))
                {
                    this.OnSite_TX_Collection_Use_DefaultChanging(value);
                    this.SendPropertyChanging();
                    this._Site_TX_Collection_Use_Default = value;
                    this.SendPropertyChanged("Site_TX_Collection_Use_Default");
                    this.OnSite_TX_Collection_Use_DefaultChanged();
                }
            }
        }

        [Column(Storage = "_Site_TX_Movement", DbType = "Bit")]
        public System.Nullable<bool> Site_TX_Movement
        {
            get
            {
                return this._Site_TX_Movement;
            }
            set
            {
                if ((this._Site_TX_Movement != value))
                {
                    this.OnSite_TX_MovementChanging(value);
                    this.SendPropertyChanging();
                    this._Site_TX_Movement = value;
                    this.SendPropertyChanged("Site_TX_Movement");
                    this.OnSite_TX_MovementChanged();
                }
            }
        }

        [Column(Storage = "_Site_TX_Movement_Use_Default", DbType = "Bit")]
        public System.Nullable<bool> Site_TX_Movement_Use_Default
        {
            get
            {
                return this._Site_TX_Movement_Use_Default;
            }
            set
            {
                if ((this._Site_TX_Movement_Use_Default != value))
                {
                    this.OnSite_TX_Movement_Use_DefaultChanging(value);
                    this.SendPropertyChanging();
                    this._Site_TX_Movement_Use_Default = value;
                    this.SendPropertyChanged("Site_TX_Movement_Use_Default");
                    this.OnSite_TX_Movement_Use_DefaultChanged();
                }
            }
        }

        [Column(Storage = "_Site_TX_EDC", DbType = "Bit")]
        public System.Nullable<bool> Site_TX_EDC
        {
            get
            {
                return this._Site_TX_EDC;
            }
            set
            {
                if ((this._Site_TX_EDC != value))
                {
                    this.OnSite_TX_EDCChanging(value);
                    this.SendPropertyChanging();
                    this._Site_TX_EDC = value;
                    this.SendPropertyChanged("Site_TX_EDC");
                    this.OnSite_TX_EDCChanged();
                }
            }
        }

        [Column(Storage = "_Site_TX_EDC_Use_Detault", DbType = "Bit")]
        public System.Nullable<bool> Site_TX_EDC_Use_Detault
        {
            get
            {
                return this._Site_TX_EDC_Use_Detault;
            }
            set
            {
                if ((this._Site_TX_EDC_Use_Detault != value))
                {
                    this.OnSite_TX_EDC_Use_DetaultChanging(value);
                    this.SendPropertyChanging();
                    this._Site_TX_EDC_Use_Detault = value;
                    this.SendPropertyChanged("Site_TX_EDC_Use_Detault");
                    this.OnSite_TX_EDC_Use_DetaultChanged();
                }
            }
        }

        [Column(Storage = "_Site_TX_Format", DbType = "Int")]
        public System.Nullable<int> Site_TX_Format
        {
            get
            {
                return this._Site_TX_Format;
            }
            set
            {
                if ((this._Site_TX_Format != value))
                {
                    this.OnSite_TX_FormatChanging(value);
                    this.SendPropertyChanging();
                    this._Site_TX_Format = value;
                    this.SendPropertyChanged("Site_TX_Format");
                    this.OnSite_TX_FormatChanged();
                }
            }
        }

        [Column(Storage = "_Site_TX_Format_Use_Default", DbType = "Bit")]
        public System.Nullable<bool> Site_TX_Format_Use_Default
        {
            get
            {
                return this._Site_TX_Format_Use_Default;
            }
            set
            {
                if ((this._Site_TX_Format_Use_Default != value))
                {
                    this.OnSite_TX_Format_Use_DefaultChanging(value);
                    this.SendPropertyChanging();
                    this._Site_TX_Format_Use_Default = value;
                    this.SendPropertyChanged("Site_TX_Format_Use_Default");
                    this.OnSite_TX_Format_Use_DefaultChanged();
                }
            }
        }

        [Column(Storage = "_Site_RX_Collection", DbType = "Bit")]
        public System.Nullable<bool> Site_RX_Collection
        {
            get
            {
                return this._Site_RX_Collection;
            }
            set
            {
                if ((this._Site_RX_Collection != value))
                {
                    this.OnSite_RX_CollectionChanging(value);
                    this.SendPropertyChanging();
                    this._Site_RX_Collection = value;
                    this.SendPropertyChanged("Site_RX_Collection");
                    this.OnSite_RX_CollectionChanged();
                }
            }
        }

        [Column(Storage = "_Site_RX_Collection_Use_Default", DbType = "Bit")]
        public System.Nullable<bool> Site_RX_Collection_Use_Default
        {
            get
            {
                return this._Site_RX_Collection_Use_Default;
            }
            set
            {
                if ((this._Site_RX_Collection_Use_Default != value))
                {
                    this.OnSite_RX_Collection_Use_DefaultChanging(value);
                    this.SendPropertyChanging();
                    this._Site_RX_Collection_Use_Default = value;
                    this.SendPropertyChanged("Site_RX_Collection_Use_Default");
                    this.OnSite_RX_Collection_Use_DefaultChanged();
                }
            }
        }

        [Column(Storage = "_Site_RX_Movement", DbType = "Bit")]
        public System.Nullable<bool> Site_RX_Movement
        {
            get
            {
                return this._Site_RX_Movement;
            }
            set
            {
                if ((this._Site_RX_Movement != value))
                {
                    this.OnSite_RX_MovementChanging(value);
                    this.SendPropertyChanging();
                    this._Site_RX_Movement = value;
                    this.SendPropertyChanged("Site_RX_Movement");
                    this.OnSite_RX_MovementChanged();
                }
            }
        }

        [Column(Storage = "_Site_RX_Movement_Use_Default", DbType = "Bit")]
        public System.Nullable<bool> Site_RX_Movement_Use_Default
        {
            get
            {
                return this._Site_RX_Movement_Use_Default;
            }
            set
            {
                if ((this._Site_RX_Movement_Use_Default != value))
                {
                    this.OnSite_RX_Movement_Use_DefaultChanging(value);
                    this.SendPropertyChanging();
                    this._Site_RX_Movement_Use_Default = value;
                    this.SendPropertyChanged("Site_RX_Movement_Use_Default");
                    this.OnSite_RX_Movement_Use_DefaultChanged();
                }
            }
        }

        [Column(Storage = "_Site_RX_EDC", DbType = "Bit")]
        public System.Nullable<bool> Site_RX_EDC
        {
            get
            {
                return this._Site_RX_EDC;
            }
            set
            {
                if ((this._Site_RX_EDC != value))
                {
                    this.OnSite_RX_EDCChanging(value);
                    this.SendPropertyChanging();
                    this._Site_RX_EDC = value;
                    this.SendPropertyChanged("Site_RX_EDC");
                    this.OnSite_RX_EDCChanged();
                }
            }
        }

        [Column(Storage = "_Site_RX_EDC_Use_Detault", DbType = "Bit")]
        public System.Nullable<bool> Site_RX_EDC_Use_Detault
        {
            get
            {
                return this._Site_RX_EDC_Use_Detault;
            }
            set
            {
                if ((this._Site_RX_EDC_Use_Detault != value))
                {
                    this.OnSite_RX_EDC_Use_DetaultChanging(value);
                    this.SendPropertyChanging();
                    this._Site_RX_EDC_Use_Detault = value;
                    this.SendPropertyChanged("Site_RX_EDC_Use_Detault");
                    this.OnSite_RX_EDC_Use_DetaultChanged();
                }
            }
        }

        [Column(Storage = "_Site_RX_Format", DbType = "Int")]
        public System.Nullable<int> Site_RX_Format
        {
            get
            {
                return this._Site_RX_Format;
            }
            set
            {
                if ((this._Site_RX_Format != value))
                {
                    this.OnSite_RX_FormatChanging(value);
                    this.SendPropertyChanging();
                    this._Site_RX_Format = value;
                    this.SendPropertyChanged("Site_RX_Format");
                    this.OnSite_RX_FormatChanged();
                }
            }
        }

        [Column(Storage = "_Site_RX_Format_Use_Default", DbType = "Bit")]
        public System.Nullable<bool> Site_RX_Format_Use_Default
        {
            get
            {
                return this._Site_RX_Format_Use_Default;
            }
            set
            {
                if ((this._Site_RX_Format_Use_Default != value))
                {
                    this.OnSite_RX_Format_Use_DefaultChanging(value);
                    this.SendPropertyChanging();
                    this._Site_RX_Format_Use_Default = value;
                    this.SendPropertyChanged("Site_RX_Format_Use_Default");
                    this.OnSite_RX_Format_Use_DefaultChanged();
                }
            }
        }

        [Column(Storage = "_NT_Phone_Book_Entry", DbType = "VarChar(100)")]
        public string NT_Phone_Book_Entry
        {
            get
            {
                return this._NT_Phone_Book_Entry;
            }
            set
            {
                if ((this._NT_Phone_Book_Entry != value))
                {
                    this.OnNT_Phone_Book_EntryChanging(value);
                    this.SendPropertyChanging();
                    this._NT_Phone_Book_Entry = value;
                    this.SendPropertyChanged("NT_Phone_Book_Entry");
                    this.OnNT_Phone_Book_EntryChanged();
                }
            }
        }

        [Column(Storage = "_Next_Secondary_Sub_Company_ID", DbType = "Int")]
        public System.Nullable<int> Next_Secondary_Sub_Company_ID
        {
            get
            {
                return this._Next_Secondary_Sub_Company_ID;
            }
            set
            {
                if ((this._Next_Secondary_Sub_Company_ID != value))
                {
                    this.OnNext_Secondary_Sub_Company_IDChanging(value);
                    this.SendPropertyChanging();
                    this._Next_Secondary_Sub_Company_ID = value;
                    this.SendPropertyChanged("Next_Secondary_Sub_Company_ID");
                    this.OnNext_Secondary_Sub_Company_IDChanged();
                }
            }
        }

        [Column(Storage = "_Site_Secondary_Sub_Company_Changeover", DbType = "VarChar(10)")]
        public string Site_Secondary_Sub_Company_Changeover
        {
            get
            {
                return this._Site_Secondary_Sub_Company_Changeover;
            }
            set
            {
                if ((this._Site_Secondary_Sub_Company_Changeover != value))
                {
                    this.OnSite_Secondary_Sub_Company_ChangeoverChanging(value);
                    this.SendPropertyChanging();
                    this._Site_Secondary_Sub_Company_Changeover = value;
                    this.SendPropertyChanged("Site_Secondary_Sub_Company_Changeover");
                    this.OnSite_Secondary_Sub_Company_ChangeoverChanged();
                }
            }
        }

        [Column(Storage = "_Site_GPS_Location", DbType = "VarChar(50)")]
        public string Site_GPS_Location
        {
            get
            {
                return this._Site_GPS_Location;
            }
            set
            {
                if ((this._Site_GPS_Location != value))
                {
                    this.OnSite_GPS_LocationChanging(value);
                    this.SendPropertyChanging();
                    this._Site_GPS_Location = value;
                    this.SendPropertyChanged("Site_GPS_Location");
                    this.OnSite_GPS_LocationChanged();
                }
            }
        }

        [Column(Storage = "_Site_Stop_Importing_EDI_On", DbType = "VarChar(10)")]
        public string Site_Stop_Importing_EDI_On
        {
            get
            {
                return this._Site_Stop_Importing_EDI_On;
            }
            set
            {
                if ((this._Site_Stop_Importing_EDI_On != value))
                {
                    this.OnSite_Stop_Importing_EDI_OnChanging(value);
                    this.SendPropertyChanging();
                    this._Site_Stop_Importing_EDI_On = value;
                    this.SendPropertyChanged("Site_Stop_Importing_EDI_On");
                    this.OnSite_Stop_Importing_EDI_OnChanged();
                }
            }
        }

        [Column(Storage = "_Site_Non_Trading_Period_From", DbType = "VarChar(30)")]
        public string Site_Non_Trading_Period_From
        {
            get
            {
                return this._Site_Non_Trading_Period_From;
            }
            set
            {
                if ((this._Site_Non_Trading_Period_From != value))
                {
                    this.OnSite_Non_Trading_Period_FromChanging(value);
                    this.SendPropertyChanging();
                    this._Site_Non_Trading_Period_From = value;
                    this.SendPropertyChanged("Site_Non_Trading_Period_From");
                    this.OnSite_Non_Trading_Period_FromChanged();
                }
            }
        }

        [Column(Storage = "_Site_Non_Trading_Period_To", DbType = "VarChar(30)")]
        public string Site_Non_Trading_Period_To
        {
            get
            {
                return this._Site_Non_Trading_Period_To;
            }
            set
            {
                if ((this._Site_Non_Trading_Period_To != value))
                {
                    this.OnSite_Non_Trading_Period_ToChanging(value);
                    this.SendPropertyChanging();
                    this._Site_Non_Trading_Period_To = value;
                    this.SendPropertyChanged("Site_Non_Trading_Period_To");
                    this.OnSite_Non_Trading_Period_ToChanged();
                }
            }
        }

        [Column(Storage = "_Service_Area_ID", DbType = "Int")]
        public System.Nullable<int> Service_Area_ID
        {
            get
            {
                return this._Service_Area_ID;
            }
            set
            {
                if ((this._Service_Area_ID != value))
                {
                    this.OnService_Area_IDChanging(value);
                    this.SendPropertyChanging();
                    this._Service_Area_ID = value;
                    this.SendPropertyChanged("Service_Area_ID");
                    this.OnService_Area_IDChanged();
                }
            }
        }

        [Column(Storage = "_Service_Supplier_ID", DbType = "Int")]
        public System.Nullable<int> Service_Supplier_ID
        {
            get
            {
                return this._Service_Supplier_ID;
            }
            set
            {
                if ((this._Service_Supplier_ID != value))
                {
                    this.OnService_Supplier_IDChanging(value);
                    this.SendPropertyChanging();
                    this._Service_Supplier_ID = value;
                    this.SendPropertyChanged("Service_Supplier_ID");
                    this.OnService_Supplier_IDChanged();
                }
            }
        }

        [Column(Storage = "_Next_Sub_Company_ID", DbType = "Int")]
        public System.Nullable<int> Next_Sub_Company_ID
        {
            get
            {
                return this._Next_Sub_Company_ID;
            }
            set
            {
                if ((this._Next_Sub_Company_ID != value))
                {
                    this.OnNext_Sub_Company_IDChanging(value);
                    this.SendPropertyChanging();
                    this._Next_Sub_Company_ID = value;
                    this.SendPropertyChanged("Next_Sub_Company_ID");
                    this.OnNext_Sub_Company_IDChanged();
                }
            }
        }

        [Column(Storage = "_Next_Sub_Company_Change_Date", DbType = "VarChar(30)")]
        public string Next_Sub_Company_Change_Date
        {
            get
            {
                return this._Next_Sub_Company_Change_Date;
            }
            set
            {
                if ((this._Next_Sub_Company_Change_Date != value))
                {
                    this.OnNext_Sub_Company_Change_DateChanging(value);
                    this.SendPropertyChanging();
                    this._Next_Sub_Company_Change_Date = value;
                    this.SendPropertyChanged("Next_Sub_Company_Change_Date");
                    this.OnNext_Sub_Company_Change_DateChanged();
                }
            }
        }

        [Column(Storage = "_Previous_Sub_Company_Change_Date", DbType = "VarChar(30)")]
        public string Previous_Sub_Company_Change_Date
        {
            get
            {
                return this._Previous_Sub_Company_Change_Date;
            }
            set
            {
                if ((this._Previous_Sub_Company_Change_Date != value))
                {
                    this.OnPrevious_Sub_Company_Change_DateChanging(value);
                    this.SendPropertyChanging();
                    this._Previous_Sub_Company_Change_Date = value;
                    this.SendPropertyChanged("Previous_Sub_Company_Change_Date");
                    this.OnPrevious_Sub_Company_Change_DateChanged();
                }
            }
        }

        [Column(Storage = "_Previous_Secondary_Sub_Company_ID", DbType = "Int")]
        public System.Nullable<int> Previous_Secondary_Sub_Company_ID
        {
            get
            {
                return this._Previous_Secondary_Sub_Company_ID;
            }
            set
            {
                if ((this._Previous_Secondary_Sub_Company_ID != value))
                {
                    this.OnPrevious_Secondary_Sub_Company_IDChanging(value);
                    this.SendPropertyChanging();
                    this._Previous_Secondary_Sub_Company_ID = value;
                    this.SendPropertyChanged("Previous_Secondary_Sub_Company_ID");
                    this.OnPrevious_Secondary_Sub_Company_IDChanged();
                }
            }
        }

        [Column(Storage = "_Previous_Secondary_Sub_Company_Change_Date", DbType = "VarChar(30)")]
        public string Previous_Secondary_Sub_Company_Change_Date
        {
            get
            {
                return this._Previous_Secondary_Sub_Company_Change_Date;
            }
            set
            {
                if ((this._Previous_Secondary_Sub_Company_Change_Date != value))
                {
                    this.OnPrevious_Secondary_Sub_Company_Change_DateChanging(value);
                    this.SendPropertyChanging();
                    this._Previous_Secondary_Sub_Company_Change_Date = value;
                    this.SendPropertyChanged("Previous_Secondary_Sub_Company_Change_Date");
                    this.OnPrevious_Secondary_Sub_Company_Change_DateChanged();
                }
            }
        }

        [Column(Storage = "_Site_Honeyframe_EDI", DbType = "Int")]
        public System.Nullable<int> Site_Honeyframe_EDI
        {
            get
            {
                return this._Site_Honeyframe_EDI;
            }
            set
            {
                if ((this._Site_Honeyframe_EDI != value))
                {
                    this.OnSite_Honeyframe_EDIChanging(value);
                    this.SendPropertyChanging();
                    this._Site_Honeyframe_EDI = value;
                    this.SendPropertyChanged("Site_Honeyframe_EDI");
                    this.OnSite_Honeyframe_EDIChanged();
                }
            }
        }

        [Column(Storage = "_Site_Datapak_Protocol", DbType = "Int")]
        public System.Nullable<int> Site_Datapak_Protocol
        {
            get
            {
                return this._Site_Datapak_Protocol;
            }
            set
            {
                if ((this._Site_Datapak_Protocol != value))
                {
                    this.OnSite_Datapak_ProtocolChanging(value);
                    this.SendPropertyChanging();
                    this._Site_Datapak_Protocol = value;
                    this.SendPropertyChanged("Site_Datapak_Protocol");
                    this.OnSite_Datapak_ProtocolChanged();
                }
            }
        }

        [Column(Storage = "_Site_Is_FreeFloat", DbType = "Bit")]
        public System.Nullable<bool> Site_Is_FreeFloat
        {
            get
            {
                return this._Site_Is_FreeFloat;
            }
            set
            {
                if ((this._Site_Is_FreeFloat != value))
                {
                    this.OnSite_Is_FreeFloatChanging(value);
                    this.SendPropertyChanging();
                    this._Site_Is_FreeFloat = value;
                    this.SendPropertyChanged("Site_Is_FreeFloat");
                    this.OnSite_Is_FreeFloatChanged();
                }
            }
        }

        [Column(Storage = "_Site_Classification_ID", DbType = "Int NOT NULL")]
        public int Site_Classification_ID
        {
            get
            {
                return this._Site_Classification_ID;
            }
            set
            {
                if ((this._Site_Classification_ID != value))
                {
                    this.OnSite_Classification_IDChanging(value);
                    this.SendPropertyChanging();
                    this._Site_Classification_ID = value;
                    this.SendPropertyChanged("Site_Classification_ID");
                    this.OnSite_Classification_IDChanged();
                }
            }
        }

        [Column(Storage = "_Site_Licensee_Agreement_End_Date", DbType = "DateTime")]
        public System.Nullable<System.DateTime> Site_Licensee_Agreement_End_Date
        {
            get
            {
                return this._Site_Licensee_Agreement_End_Date;
            }
            set
            {
                if ((this._Site_Licensee_Agreement_End_Date != value))
                {
                    this.OnSite_Licensee_Agreement_End_DateChanging(value);
                    this.SendPropertyChanging();
                    this._Site_Licensee_Agreement_End_Date = value;
                    this.SendPropertyChanged("Site_Licensee_Agreement_End_Date");
                    this.OnSite_Licensee_Agreement_End_DateChanged();
                }
            }
        }

        [Column(Storage = "_Site_Licence_Number", DbType = "VarChar(25)")]
        public string Site_Licence_Number
        {
            get
            {
                return this._Site_Licence_Number;
            }
            set
            {
                if ((this._Site_Licence_Number != value))
                {
                    this.OnSite_Licence_NumberChanging(value);
                    this.SendPropertyChanging();
                    this._Site_Licence_Number = value;
                    this.SendPropertyChanged("Site_Licence_Number");
                    this.OnSite_Licence_NumberChanged();
                }
            }
        }

        [Column(Storage = "_Site_Application", DbType = "SmallInt")]
        public System.Nullable<short> Site_Application
        {
            get
            {
                return this._Site_Application;
            }
            set
            {
                if ((this._Site_Application != value))
                {
                    this.OnSite_ApplicationChanging(value);
                    this.SendPropertyChanging();
                    this._Site_Application = value;
                    this.SendPropertyChanged("Site_Application");
                    this.OnSite_ApplicationChanged();
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
                    this.OnRegionChanging(value);
                    this.SendPropertyChanging();
                    this._Region = value;
                    this.SendPropertyChanged("Region");
                    this.OnRegionChanged();
                }
            }
        }

        [Column(Storage = "_WebURL", DbType = "VarChar(2000)")]
        public string WebURL
        {
            get
            {
                return this._WebURL;
            }
            set
            {
                if ((this._WebURL != value))
                {
                    this.OnWebURLChanging(value);
                    this.SendPropertyChanging();
                    this._WebURL = value;
                    this.SendPropertyChanged("WebURL");
                    this.OnWebURLChanged();
                }
            }
        }

        [Column(Storage = "_ConnectionString", DbType = "VarChar(200)")]
        public string ConnectionString
        {
            get
            {
                return this._ConnectionString;
            }
            set
            {
                if ((this._ConnectionString != value))
                {
                    this.OnConnectionStringChanging(value);
                    this.SendPropertyChanging();
                    this._ConnectionString = value;
                    this.SendPropertyChanged("ConnectionString");
                    this.OnConnectionStringChanged();
                }
            }
        }

        [Column(Storage = "_Site_Status", DbType = "Xml", UpdateCheck = UpdateCheck.Never)]
        public System.Xml.Linq.XElement Site_Status
        {
            get
            {
                return this._Site_Status;
            }
            set
            {
                if ((this._Site_Status != value))
                {
                    this.OnSite_StatusChanging(value);
                    this.SendPropertyChanging();
                    this._Site_Status = value;
                    this.SendPropertyChanged("Site_Status");
                    this.OnSite_StatusChanged();
                }
            }
        }

        [Column(Storage = "_Last_Updated_Time", DbType = "DateTime")]
        public System.Nullable<System.DateTime> Last_Updated_Time
        {
            get
            {
                return this._Last_Updated_Time;
            }
            set
            {
                if ((this._Last_Updated_Time != value))
                {
                    this.OnLast_Updated_TimeChanging(value);
                    this.SendPropertyChanging();
                    this._Last_Updated_Time = value;
                    this.SendPropertyChanged("Last_Updated_Time");
                    this.OnLast_Updated_TimeChanged();
                }
            }
        }

        [Column(Storage = "_Apply_Retailer_Share", DbType = "Bit NOT NULL")]
        public bool Apply_Retailer_Share
        {
            get
            {
                return this._Apply_Retailer_Share;
            }
            set
            {
                if ((this._Apply_Retailer_Share != value))
                {
                    this.OnApply_Retailer_ShareChanging(value);
                    this.SendPropertyChanging();
                    this._Apply_Retailer_Share = value;
                    this.SendPropertyChanged("Apply_Retailer_Share");
                    this.OnApply_Retailer_ShareChanged();
                }
            }
        }

        [Column(Storage = "_Site_Status_ID", DbType = "Int")]
        public System.Nullable<int> Site_Status_ID
        {
            get
            {
                return this._Site_Status_ID;
            }
            set
            {
                if ((this._Site_Status_ID != value))
                {
                    this.OnSite_Status_IDChanging(value);
                    this.SendPropertyChanging();
                    this._Site_Status_ID = value;
                    this.SendPropertyChanged("Site_Status_ID");
                    this.OnSite_Status_IDChanged();
                }
            }
        }

        [Column(Storage = "_Site_Inactive_Date", DbType = "DateTime")]
        public System.Nullable<System.DateTime> Site_Inactive_Date
        {
            get
            {
                return this._Site_Inactive_Date;
            }
            set
            {
                if ((this._Site_Inactive_Date != value))
                {
                    this.OnSite_Inactive_DateChanging(value);
                    this.SendPropertyChanging();
                    this._Site_Inactive_Date = value;
                    this.SendPropertyChanged("Site_Inactive_Date");
                    this.OnSite_Inactive_DateChanged();
                }
            }
        }

        [Column(Storage = "_NGA_Machine_ID", DbType = "Int")]
        public System.Nullable<int> NGA_Machine_ID
        {
            get
            {
                return this._NGA_Machine_ID;
            }
            set
            {
                if ((this._NGA_Machine_ID != value))
                {
                    this.OnNGA_Machine_IDChanging(value);
                    this.SendPropertyChanging();
                    this._NGA_Machine_ID = value;
                    this.SendPropertyChanged("NGA_Machine_ID");
                    this.OnNGA_Machine_IDChanged();
                }
            }
        }

        [Column(Storage = "_Site_Setting_Profile_ID", DbType = "Int")]
        public System.Nullable<int> Site_Setting_Profile_ID
        {
            get
            {
                return this._Site_Setting_Profile_ID;
            }
            set
            {
                if ((this._Site_Setting_Profile_ID != value))
                {
                    this.OnSite_Setting_Profile_IDChanging(value);
                    this.SendPropertyChanging();
                    this._Site_Setting_Profile_ID = value;
                    this.SendPropertyChanged("Site_Setting_Profile_ID");
                    this.OnSite_Setting_Profile_IDChanged();
                }
            }
        }

        [Column(Storage = "_SiteStatus", DbType = "VarChar(300)")]
        public string SiteStatus
        {
            get
            {
                return this._SiteStatus;
            }
            set
            {
                if ((this._SiteStatus != value))
                {
                    this.OnSiteStatusChanging(value);
                    this.SendPropertyChanging();
                    this._SiteStatus = value;
                    this.SendPropertyChanged("SiteStatus");
                    this.OnSiteStatusChanged();
                }
            }
        }

        [Column(Storage = "_ExchangeKey", DbType = "VarChar(300)")]
        public string ExchangeKey
        {
            get
            {
                return this._ExchangeKey;
            }
            set
            {
                if ((this._ExchangeKey != value))
                {
                    this.OnExchangeKeyChanging(value);
                    this.SendPropertyChanging();
                    this._ExchangeKey = value;
                    this.SendPropertyChanged("ExchangeKey");
                    this.OnExchangeKeyChanged();
                }
            }
        }

        [Column(Storage = "_Site_Fiscal_Code", DbType = "VarChar(16)")]
        public string Site_Fiscal_Code
        {
            get
            {
                return this._Site_Fiscal_Code;
            }
            set
            {
                if ((this._Site_Fiscal_Code != value))
                {
                    this.OnSite_Fiscal_CodeChanging(value);
                    this.SendPropertyChanging();
                    this._Site_Fiscal_Code = value;
                    this.SendPropertyChanged("Site_Fiscal_Code");
                    this.OnSite_Fiscal_CodeChanged();
                }
            }
        }

        [Column(Storage = "_Site_Street_Number", DbType = "VarChar(15)")]
        public string Site_Street_Number
        {
            get
            {
                return this._Site_Street_Number;
            }
            set
            {
                if ((this._Site_Street_Number != value))
                {
                    this.OnSite_Street_NumberChanging(value);
                    this.SendPropertyChanging();
                    this._Site_Street_Number = value;
                    this.SendPropertyChanged("Site_Street_Number");
                    this.OnSite_Street_NumberChanged();
                }
            }
        }

        [Column(Storage = "_Site_Province", DbType = "VarChar(15)")]
        public string Site_Province
        {
            get
            {
                return this._Site_Province;
            }
            set
            {
                if ((this._Site_Province != value))
                {
                    this.OnSite_ProvinceChanging(value);
                    this.SendPropertyChanging();
                    this._Site_Province = value;
                    this.SendPropertyChanged("Site_Province");
                    this.OnSite_ProvinceChanged();
                }
            }
        }

        [Column(Storage = "_Site_Municipality", DbType = "VarChar(40)")]
        public string Site_Municipality
        {
            get
            {
                return this._Site_Municipality;
            }
            set
            {
                if ((this._Site_Municipality != value))
                {
                    this.OnSite_MunicipalityChanging(value);
                    this.SendPropertyChanging();
                    this._Site_Municipality = value;
                    this.SendPropertyChanged("Site_Municipality");
                    this.OnSite_MunicipalityChanged();
                }
            }
        }

        [Column(Storage = "_Site_Cadastral_Code", DbType = "VarChar(15)")]
        public string Site_Cadastral_Code
        {
            get
            {
                return this._Site_Cadastral_Code;
            }
            set
            {
                if ((this._Site_Cadastral_Code != value))
                {
                    this.OnSite_Cadastral_CodeChanging(value);
                    this.SendPropertyChanging();
                    this._Site_Cadastral_Code = value;
                    this.SendPropertyChanged("Site_Cadastral_Code");
                    this.OnSite_Cadastral_CodeChanged();
                }
            }
        }

        [Column(Storage = "_Site_Area", DbType = "Int")]
        public System.Nullable<int> Site_Area
        {
            get
            {
                return this._Site_Area;
            }
            set
            {
                if ((this._Site_Area != value))
                {
                    this.OnSite_AreaChanging(value);
                    this.SendPropertyChanging();
                    this._Site_Area = value;
                    this.SendPropertyChanged("Site_Area");
                    this.OnSite_AreaChanged();
                }
            }
        }

        [Column(Storage = "_Site_Location_Type", DbType = "Int")]
        public System.Nullable<int> Site_Location_Type
        {
            get
            {
                return this._Site_Location_Type;
            }
            set
            {
                if ((this._Site_Location_Type != value))
                {
                    this.OnSite_Location_TypeChanging(value);
                    this.SendPropertyChanging();
                    this._Site_Location_Type = value;
                    this.SendPropertyChanged("Site_Location_Type");
                    this.OnSite_Location_TypeChanged();
                }
            }
        }

        [Column(Storage = "_Site_Closed", DbType = "Int")]
        public System.Nullable<int> Site_Closed
        {
            get
            {
                return this._Site_Closed;
            }
            set
            {
                if ((this._Site_Closed != value))
                {
                    this.OnSite_ClosedChanging(value);
                    this.SendPropertyChanging();
                    this._Site_Closed = value;
                    this.SendPropertyChanged("Site_Closed");
                    this.OnSite_ClosedChanged();
                }
            }
        }

        [Column(Storage = "_Site_Workstation", DbType = "VarChar(100)")]
        public string Site_Workstation
        {
            get
            {
                return this._Site_Workstation;
            }
            set
            {
                if ((this._Site_Workstation != value))
                {
                    this.OnSite_WorkstationChanging(value);
                    this.SendPropertyChanging();
                    this._Site_Workstation = value;
                    this.SendPropertyChanged("Site_Workstation");
                    this.OnSite_WorkstationChanged();
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
