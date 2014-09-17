using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;

namespace BMC.Common.Utilities
{
    [Database(Name = "Exchange")]
    internal partial class LookupManagerDataContext : DataContext
    {
        private static MappingSource mappingSource = new AttributeMappingSource();

        #region Extensibility Method Definitions

        partial void OnCreated();
        partial void InsertLookupMaster(LookupMaster instance);
        partial void UpdateLookupMaster(LookupMaster instance);
        partial void DeleteLookupMaster(LookupMaster instance);

        #endregion

        public LookupManagerDataContext(string connection) :
            base(connection, mappingSource)
        {
            OnCreated();
        }

        public Table<LookupMaster> LookupMasters
        {
            get { return GetTable<LookupMaster>(); }
        }

        public Table<LanguageLookup> LanguageLookups
        {
            get { return GetTable<LanguageLookup>(); }
        }

        public Table<CodeMaster> CodeMasters
        {
            get { return GetTable<CodeMaster>(); }
        }

        public Table<Site> Sites
        {
            get { return GetTable<Site>(); }
        }

        [Function(Name = "dbo.usp_Export_History")]
        public int usp_Export_History([Parameter(Name = "Reference1", DbType = "VarChar(50)")] string reference1,
                                      [Parameter(Name = "Type", DbType = "VarChar(30)")] string type,
                                      [Parameter(Name = "Site_id", DbType = "Int")] Nullable<int> site_id)
        {
            IExecuteResult result = ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), reference1,
                                                      type, site_id);
            return ((int)(result.ReturnValue));
        }
    }

    [Table(Name = "dbo.Site")]
    internal partial class Site : INotifyPropertyChanging, INotifyPropertyChanged
    {
        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);

        private Nullable<int> _Access_Key_ID;

        private Nullable<bool> _Access_Key_ID_Default;
        private bool _Apply_Retailer_Share;

        private Nullable<int> _Computer_Build_ID;
        private string _ConnectionString;
        private Nullable<int> _Depot_ID;
        private string _ExchangeKey;
        private Nullable<DateTime> _Last_Updated_Time;
        private Nullable<int> _Next_Secondary_Sub_Company_ID;
        private string _Next_Sub_Company_Change_Date;
        private Nullable<int> _Next_Sub_Company_ID;
        private Nullable<int> _NGA_Machine_ID;
        private string _NT_Phone_Book_Entry;
        private string _Previous_Secondary_Sub_Company_Change_Date;
        private Nullable<int> _Previous_Secondary_Sub_Company_ID;
        private string _Previous_Sub_Company_Change_Date;
        private string _Region;
        private string _Sage_Account_Ref;

        private Nullable<int> _Secondary_Sub_Company_ID;
        private Nullable<int> _Service_Area_ID;
        private Nullable<int> _Service_Depot_ID;
        private Nullable<int> _Service_Supplier_ID;

        private string _Site_Address;
        private string _Site_Address_1;

        private string _Site_Address_2;

        private string _Site_Address_3;

        private string _Site_Address_4;

        private string _Site_Address_5;
        private Nullable<short> _Site_Application;
        private Nullable<int> _Site_Area;
        private string _Site_Cadastral_Code;
        private int _Site_Classification_ID;
        private Nullable<int> _Site_Closed;
        private string _Site_Closed_Date;
        private string _Site_Code;
        private Nullable<int> _Site_Company_Barrellage;
        private string _Site_Company_Code;
        private Nullable<float> _Site_Company_Target;

        private string _Site_Computer_Name;
        private Nullable<int> _Site_ConnType;
        private Nullable<int> _Site_Datapak_Protocol;

        private string _Site_Dial_Up_Number;

        private string _Site_Domain;
        private string _Site_Email_Address;
        private string _Site_End_Date;
        private string _Site_Fax_No;
        private string _Site_Fiscal_Code;

        private string _Site_FTPServerAddress;
        private string _Site_GPS_Location;
        private string _Site_Grade;
        private Nullable<int> _Site_Honeyframe_EDI;
        private int _Site_ID;
        private string _Site_Image_Reference;

        private string _Site_Image_Reference_2;
        private Nullable<DateTime> _Site_Inactive_Date;
        private string _Site_Invoice_Address;
        private string _Site_Invoice_Name;
        private string _Site_Invoice_Postcode;
        private Nullable<bool> _Site_Is_FreeFloat;

        private string _Site_Jackpot;

        private Nullable<bool> _Site_Jackpot_Default;
        private string _Site_Licence_Number;
        private Nullable<DateTime> _Site_Licensee_Agreement_End_Date;
        private string _Site_Licensee_Agreement_Type;
        private Nullable<DateTime> _Site_Licensee_Commenced_Date;
        private string _Site_Local_Inbox;

        private string _Site_Local_Outbox;
        private Nullable<int> _Site_Location_Type;
        private Nullable<bool> _Site_London_Rent;
        private string _Site_Manager;
        private string _Site_Memo;
        private string _Site_Municipality;
        private string _Site_Name;
        private string _Site_Non_Trading_Period_From;

        private string _Site_Non_Trading_Period_To;
        private string _Site_Open_Friday;
        private string _Site_Open_Monday;
        private string _Site_Open_Saturday;

        private string _Site_Open_Sunday;
        private string _Site_Open_Thursday;
        private string _Site_Open_Tuesday;

        private string _Site_Open_Wednesday;
        private string _Site_Password;

        private string _Site_Percentage_Payout;

        private Nullable<bool> _Site_Percentage_Payout_Default;
        private Nullable<int> _Site_Permit_Needed;
        private string _Site_Phone_No;
        private string _Site_Postcode;

        private Nullable<int> _Site_Previous_Sub_Company_ID;
        private string _Site_Price_Per_Play;

        private Nullable<bool> _Site_Price_Per_Play_Default;
        private string _Site_Province;
        private string _Site_Reference;
        private string _Site_Remote_Inbox;

        private string _Site_Remote_Outbox;

        private Nullable<bool> _Site_RX_Collection;

        private Nullable<bool> _Site_RX_Collection_Use_Default;

        private Nullable<bool> _Site_RX_EDC;

        private Nullable<bool> _Site_RX_EDC_Use_Detault;

        private Nullable<int> _Site_RX_Format;

        private Nullable<bool> _Site_RX_Format_Use_Default;
        private Nullable<bool> _Site_RX_Movement;

        private Nullable<bool> _Site_RX_Movement_Use_Default;

        private string _Site_Secondary_Sub_Company_Changeover;
        private Nullable<int> _Site_Setting_Profile_ID;
        private string _Site_Start_Date;

        private XElement _Site_Status;

        private Nullable<int> _Site_Status_ID;
        private string _Site_Stop_Importing_EDI_On;

        private string _Site_Street_Number;
        private string _Site_Supplier_Area;
        private string _Site_Supplier_Code;
        private string _Site_Supplier_Service_Area;

        private Nullable<int> _Site_Toponym;
        private string _Site_Trade_Type;
        private Nullable<bool> _Site_TX_Collection;

        private Nullable<bool> _Site_TX_Collection_Use_Default;
        private Nullable<bool> _Site_TX_EDC;

        private Nullable<bool> _Site_TX_EDC_Use_Detault;

        private Nullable<int> _Site_TX_Format;

        private Nullable<bool> _Site_TX_Format_Use_Default;
        private Nullable<bool> _Site_TX_Movement;

        private Nullable<bool> _Site_TX_Movement_Use_Default;
        private string _Site_Username;
        private Nullable<bool> _Site_VAT_Exempt_Flag;
        private string _Site_Workstation;
        private string _SiteStatus;
        private Nullable<int> _Staff_ID;

        private Nullable<bool> _Staff_ID_Default;
        private Nullable<int> _Standard_Opening_Hours_ID;
        private Nullable<int> _Sub_Company_Area_ID;

        private Nullable<int> _Sub_Company_District_ID;
        private Nullable<int> _Sub_Company_ID;
        private Nullable<int> _Sub_Company_Region_ID;
        private Nullable<int> _Terms_Group_ID;

        private Nullable<bool> _Terms_Group_ID_Default;
        private string _WebURL;

        #region Extensibility Method Definitions

        partial void OnLoaded();
        partial void OnValidate(ChangeAction action);
        partial void OnCreated();
        partial void OnSite_IDChanging(int value);
        partial void OnSite_IDChanged();
        partial void OnSite_ReferenceChanging(string value);
        partial void OnSite_ReferenceChanged();
        partial void OnSub_Company_IDChanging(Nullable<int> value);
        partial void OnSub_Company_IDChanged();
        partial void OnStaff_IDChanging(Nullable<int> value);
        partial void OnStaff_IDChanged();
        partial void OnStaff_ID_DefaultChanging(Nullable<bool> value);
        partial void OnStaff_ID_DefaultChanged();
        partial void OnAccess_Key_IDChanging(Nullable<int> value);
        partial void OnAccess_Key_IDChanged();
        partial void OnAccess_Key_ID_DefaultChanging(Nullable<bool> value);
        partial void OnAccess_Key_ID_DefaultChanged();
        partial void OnTerms_Group_IDChanging(Nullable<int> value);
        partial void OnTerms_Group_IDChanged();
        partial void OnTerms_Group_ID_DefaultChanging(Nullable<bool> value);
        partial void OnTerms_Group_ID_DefaultChanged();
        partial void OnComputer_Build_IDChanging(Nullable<int> value);
        partial void OnComputer_Build_IDChanged();
        partial void OnStandard_Opening_Hours_IDChanging(Nullable<int> value);
        partial void OnStandard_Opening_Hours_IDChanged();
        partial void OnSecondary_Sub_Company_IDChanging(Nullable<int> value);
        partial void OnSecondary_Sub_Company_IDChanged();
        partial void OnSite_London_RentChanging(Nullable<bool> value);
        partial void OnSite_London_RentChanged();
        partial void OnSite_Supplier_AreaChanging(string value);
        partial void OnSite_Supplier_AreaChanged();
        partial void OnSite_Supplier_Service_AreaChanging(string value);
        partial void OnSite_Supplier_Service_AreaChanged();
        partial void OnSite_GradeChanging(string value);
        partial void OnSite_GradeChanged();
        partial void OnSite_Permit_NeededChanging(Nullable<int> value);
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
        partial void OnSite_ConnTypeChanging(Nullable<int> value);
        partial void OnSite_ConnTypeChanged();
        partial void OnSite_Price_Per_PlayChanging(string value);
        partial void OnSite_Price_Per_PlayChanged();
        partial void OnSite_Price_Per_Play_DefaultChanging(Nullable<bool> value);
        partial void OnSite_Price_Per_Play_DefaultChanged();
        partial void OnSite_JackpotChanging(string value);
        partial void OnSite_JackpotChanged();
        partial void OnSite_Jackpot_DefaultChanging(Nullable<bool> value);
        partial void OnSite_Jackpot_DefaultChanged();
        partial void OnSite_Percentage_PayoutChanging(string value);
        partial void OnSite_Percentage_PayoutChanged();
        partial void OnSite_Percentage_Payout_DefaultChanging(Nullable<bool> value);
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
        partial void OnSite_Previous_Sub_Company_IDChanging(Nullable<int> value);
        partial void OnSite_Previous_Sub_Company_IDChanged();
        partial void OnSite_Licensee_Commenced_DateChanging(Nullable<DateTime> value);
        partial void OnSite_Licensee_Commenced_DateChanged();
        partial void OnSite_Licensee_Agreement_TypeChanging(string value);
        partial void OnSite_Licensee_Agreement_TypeChanged();
        partial void OnDepot_IDChanging(Nullable<int> value);
        partial void OnDepot_IDChanged();
        partial void OnService_Depot_IDChanging(Nullable<int> value);
        partial void OnService_Depot_IDChanged();
        partial void OnSite_VAT_Exempt_FlagChanging(Nullable<bool> value);
        partial void OnSite_VAT_Exempt_FlagChanged();
        partial void OnSite_Company_TargetChanging(Nullable<float> value);
        partial void OnSite_Company_TargetChanged();
        partial void OnSite_Company_BarrellageChanging(Nullable<int> value);
        partial void OnSite_Company_BarrellageChanged();
        partial void OnSite_Image_ReferenceChanging(string value);
        partial void OnSite_Image_ReferenceChanged();
        partial void OnSite_Image_Reference_2Changing(string value);
        partial void OnSite_Image_Reference_2Changed();
        partial void OnSite_Trade_TypeChanging(string value);
        partial void OnSite_Trade_TypeChanged();
        partial void OnSub_Company_Region_IDChanging(Nullable<int> value);
        partial void OnSub_Company_Region_IDChanged();
        partial void OnSub_Company_Area_IDChanging(Nullable<int> value);
        partial void OnSub_Company_Area_IDChanged();
        partial void OnSub_Company_District_IDChanging(Nullable<int> value);
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
        partial void OnSite_TX_CollectionChanging(Nullable<bool> value);
        partial void OnSite_TX_CollectionChanged();
        partial void OnSite_TX_Collection_Use_DefaultChanging(Nullable<bool> value);
        partial void OnSite_TX_Collection_Use_DefaultChanged();
        partial void OnSite_TX_MovementChanging(Nullable<bool> value);
        partial void OnSite_TX_MovementChanged();
        partial void OnSite_TX_Movement_Use_DefaultChanging(Nullable<bool> value);
        partial void OnSite_TX_Movement_Use_DefaultChanged();
        partial void OnSite_TX_EDCChanging(Nullable<bool> value);
        partial void OnSite_TX_EDCChanged();
        partial void OnSite_TX_EDC_Use_DetaultChanging(Nullable<bool> value);
        partial void OnSite_TX_EDC_Use_DetaultChanged();
        partial void OnSite_TX_FormatChanging(Nullable<int> value);
        partial void OnSite_TX_FormatChanged();
        partial void OnSite_TX_Format_Use_DefaultChanging(Nullable<bool> value);
        partial void OnSite_TX_Format_Use_DefaultChanged();
        partial void OnSite_RX_CollectionChanging(Nullable<bool> value);
        partial void OnSite_RX_CollectionChanged();
        partial void OnSite_RX_Collection_Use_DefaultChanging(Nullable<bool> value);
        partial void OnSite_RX_Collection_Use_DefaultChanged();
        partial void OnSite_RX_MovementChanging(Nullable<bool> value);
        partial void OnSite_RX_MovementChanged();
        partial void OnSite_RX_Movement_Use_DefaultChanging(Nullable<bool> value);
        partial void OnSite_RX_Movement_Use_DefaultChanged();
        partial void OnSite_RX_EDCChanging(Nullable<bool> value);
        partial void OnSite_RX_EDCChanged();
        partial void OnSite_RX_EDC_Use_DetaultChanging(Nullable<bool> value);
        partial void OnSite_RX_EDC_Use_DetaultChanged();
        partial void OnSite_RX_FormatChanging(Nullable<int> value);
        partial void OnSite_RX_FormatChanged();
        partial void OnSite_RX_Format_Use_DefaultChanging(Nullable<bool> value);
        partial void OnSite_RX_Format_Use_DefaultChanged();
        partial void OnNT_Phone_Book_EntryChanging(string value);
        partial void OnNT_Phone_Book_EntryChanged();
        partial void OnNext_Secondary_Sub_Company_IDChanging(Nullable<int> value);
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
        partial void OnService_Area_IDChanging(Nullable<int> value);
        partial void OnService_Area_IDChanged();
        partial void OnService_Supplier_IDChanging(Nullable<int> value);
        partial void OnService_Supplier_IDChanged();
        partial void OnNext_Sub_Company_IDChanging(Nullable<int> value);
        partial void OnNext_Sub_Company_IDChanged();
        partial void OnNext_Sub_Company_Change_DateChanging(string value);
        partial void OnNext_Sub_Company_Change_DateChanged();
        partial void OnPrevious_Sub_Company_Change_DateChanging(string value);
        partial void OnPrevious_Sub_Company_Change_DateChanged();
        partial void OnPrevious_Secondary_Sub_Company_IDChanging(Nullable<int> value);
        partial void OnPrevious_Secondary_Sub_Company_IDChanged();
        partial void OnPrevious_Secondary_Sub_Company_Change_DateChanging(string value);
        partial void OnPrevious_Secondary_Sub_Company_Change_DateChanged();
        partial void OnSite_Honeyframe_EDIChanging(Nullable<int> value);
        partial void OnSite_Honeyframe_EDIChanged();
        partial void OnSite_Datapak_ProtocolChanging(Nullable<int> value);
        partial void OnSite_Datapak_ProtocolChanged();
        partial void OnSite_Is_FreeFloatChanging(Nullable<bool> value);
        partial void OnSite_Is_FreeFloatChanged();
        partial void OnSite_Classification_IDChanging(int value);
        partial void OnSite_Classification_IDChanged();
        partial void OnSite_Licensee_Agreement_End_DateChanging(Nullable<DateTime> value);
        partial void OnSite_Licensee_Agreement_End_DateChanged();
        partial void OnSite_Licence_NumberChanging(string value);
        partial void OnSite_Licence_NumberChanged();
        partial void OnSite_ApplicationChanging(Nullable<short> value);
        partial void OnSite_ApplicationChanged();
        partial void OnRegionChanging(string value);
        partial void OnRegionChanged();
        partial void OnWebURLChanging(string value);
        partial void OnWebURLChanged();
        partial void OnConnectionStringChanging(string value);
        partial void OnConnectionStringChanged();
        partial void OnSite_StatusChanging(XElement value);
        partial void OnSite_StatusChanged();
        partial void OnLast_Updated_TimeChanging(Nullable<DateTime> value);
        partial void OnLast_Updated_TimeChanged();
        partial void OnApply_Retailer_ShareChanging(bool value);
        partial void OnApply_Retailer_ShareChanged();
        partial void OnSite_Status_IDChanging(Nullable<int> value);
        partial void OnSite_Status_IDChanged();
        partial void OnSite_Inactive_DateChanging(Nullable<DateTime> value);
        partial void OnSite_Inactive_DateChanged();
        partial void OnNGA_Machine_IDChanging(Nullable<int> value);
        partial void OnNGA_Machine_IDChanged();
        partial void OnSite_Setting_Profile_IDChanging(Nullable<int> value);
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
        partial void OnSite_AreaChanging(Nullable<int> value);
        partial void OnSite_AreaChanged();
        partial void OnSite_Location_TypeChanging(Nullable<int> value);
        partial void OnSite_Location_TypeChanged();
        partial void OnSite_ClosedChanging(Nullable<int> value);
        partial void OnSite_ClosedChanged();
        partial void OnSite_WorkstationChanging(string value);
        partial void OnSite_WorkstationChanged();
        partial void OnSite_ToponymChanging(Nullable<int> value);
        partial void OnSite_ToponymChanged();
        partial void OnSite_Closed_DateChanging(string value);
        partial void OnSite_Closed_DateChanged();

        #endregion

        public Site()
        {
            OnCreated();
        }

        [Column(Storage = "_Site_ID", AutoSync = AutoSync.OnInsert, DbType = "Int NOT NULL IDENTITY",
            IsPrimaryKey = true, IsDbGenerated = true)]
        public int Site_ID
        {
            get { return _Site_ID; }
            set
            {
                if ((_Site_ID != value))
                {
                    OnSite_IDChanging(value);
                    SendPropertyChanging();
                    _Site_ID = value;
                    SendPropertyChanged("Site_ID");
                    OnSite_IDChanged();
                }
            }
        }

        [Column(Storage = "_Site_Reference", DbType = "VarChar(50)")]
        public string Site_Reference
        {
            get { return _Site_Reference; }
            set
            {
                if ((_Site_Reference != value))
                {
                    OnSite_ReferenceChanging(value);
                    SendPropertyChanging();
                    _Site_Reference = value;
                    SendPropertyChanged("Site_Reference");
                    OnSite_ReferenceChanged();
                }
            }
        }

        [Column(Storage = "_Sub_Company_ID", DbType = "Int")]
        public Nullable<int> Sub_Company_ID
        {
            get { return _Sub_Company_ID; }
            set
            {
                if ((_Sub_Company_ID != value))
                {
                    OnSub_Company_IDChanging(value);
                    SendPropertyChanging();
                    _Sub_Company_ID = value;
                    SendPropertyChanged("Sub_Company_ID");
                    OnSub_Company_IDChanged();
                }
            }
        }

        [Column(Storage = "_Staff_ID", DbType = "Int")]
        public Nullable<int> Staff_ID
        {
            get { return _Staff_ID; }
            set
            {
                if ((_Staff_ID != value))
                {
                    OnStaff_IDChanging(value);
                    SendPropertyChanging();
                    _Staff_ID = value;
                    SendPropertyChanged("Staff_ID");
                    OnStaff_IDChanged();
                }
            }
        }

        [Column(Storage = "_Staff_ID_Default", DbType = "Bit")]
        public Nullable<bool> Staff_ID_Default
        {
            get { return _Staff_ID_Default; }
            set
            {
                if ((_Staff_ID_Default != value))
                {
                    OnStaff_ID_DefaultChanging(value);
                    SendPropertyChanging();
                    _Staff_ID_Default = value;
                    SendPropertyChanged("Staff_ID_Default");
                    OnStaff_ID_DefaultChanged();
                }
            }
        }

        [Column(Storage = "_Access_Key_ID", DbType = "Int")]
        public Nullable<int> Access_Key_ID
        {
            get { return _Access_Key_ID; }
            set
            {
                if ((_Access_Key_ID != value))
                {
                    OnAccess_Key_IDChanging(value);
                    SendPropertyChanging();
                    _Access_Key_ID = value;
                    SendPropertyChanged("Access_Key_ID");
                    OnAccess_Key_IDChanged();
                }
            }
        }

        [Column(Storage = "_Access_Key_ID_Default", DbType = "Bit")]
        public Nullable<bool> Access_Key_ID_Default
        {
            get { return _Access_Key_ID_Default; }
            set
            {
                if ((_Access_Key_ID_Default != value))
                {
                    OnAccess_Key_ID_DefaultChanging(value);
                    SendPropertyChanging();
                    _Access_Key_ID_Default = value;
                    SendPropertyChanged("Access_Key_ID_Default");
                    OnAccess_Key_ID_DefaultChanged();
                }
            }
        }

        [Column(Storage = "_Terms_Group_ID", DbType = "Int")]
        public Nullable<int> Terms_Group_ID
        {
            get { return _Terms_Group_ID; }
            set
            {
                if ((_Terms_Group_ID != value))
                {
                    OnTerms_Group_IDChanging(value);
                    SendPropertyChanging();
                    _Terms_Group_ID = value;
                    SendPropertyChanged("Terms_Group_ID");
                    OnTerms_Group_IDChanged();
                }
            }
        }

        [Column(Storage = "_Terms_Group_ID_Default", DbType = "Bit")]
        public Nullable<bool> Terms_Group_ID_Default
        {
            get { return _Terms_Group_ID_Default; }
            set
            {
                if ((_Terms_Group_ID_Default != value))
                {
                    OnTerms_Group_ID_DefaultChanging(value);
                    SendPropertyChanging();
                    _Terms_Group_ID_Default = value;
                    SendPropertyChanged("Terms_Group_ID_Default");
                    OnTerms_Group_ID_DefaultChanged();
                }
            }
        }

        [Column(Storage = "_Computer_Build_ID", DbType = "Int")]
        public Nullable<int> Computer_Build_ID
        {
            get { return _Computer_Build_ID; }
            set
            {
                if ((_Computer_Build_ID != value))
                {
                    OnComputer_Build_IDChanging(value);
                    SendPropertyChanging();
                    _Computer_Build_ID = value;
                    SendPropertyChanged("Computer_Build_ID");
                    OnComputer_Build_IDChanged();
                }
            }
        }

        [Column(Storage = "_Standard_Opening_Hours_ID", DbType = "Int")]
        public Nullable<int> Standard_Opening_Hours_ID
        {
            get { return _Standard_Opening_Hours_ID; }
            set
            {
                if ((_Standard_Opening_Hours_ID != value))
                {
                    OnStandard_Opening_Hours_IDChanging(value);
                    SendPropertyChanging();
                    _Standard_Opening_Hours_ID = value;
                    SendPropertyChanged("Standard_Opening_Hours_ID");
                    OnStandard_Opening_Hours_IDChanged();
                }
            }
        }

        [Column(Storage = "_Secondary_Sub_Company_ID", DbType = "Int")]
        public Nullable<int> Secondary_Sub_Company_ID
        {
            get { return _Secondary_Sub_Company_ID; }
            set
            {
                if ((_Secondary_Sub_Company_ID != value))
                {
                    OnSecondary_Sub_Company_IDChanging(value);
                    SendPropertyChanging();
                    _Secondary_Sub_Company_ID = value;
                    SendPropertyChanged("Secondary_Sub_Company_ID");
                    OnSecondary_Sub_Company_IDChanged();
                }
            }
        }

        [Column(Storage = "_Site_London_Rent", DbType = "Bit")]
        public Nullable<bool> Site_London_Rent
        {
            get { return _Site_London_Rent; }
            set
            {
                if ((_Site_London_Rent != value))
                {
                    OnSite_London_RentChanging(value);
                    SendPropertyChanging();
                    _Site_London_Rent = value;
                    SendPropertyChanged("Site_London_Rent");
                    OnSite_London_RentChanged();
                }
            }
        }

        [Column(Storage = "_Site_Supplier_Area", DbType = "VarChar(50)")]
        public string Site_Supplier_Area
        {
            get { return _Site_Supplier_Area; }
            set
            {
                if ((_Site_Supplier_Area != value))
                {
                    OnSite_Supplier_AreaChanging(value);
                    SendPropertyChanging();
                    _Site_Supplier_Area = value;
                    SendPropertyChanged("Site_Supplier_Area");
                    OnSite_Supplier_AreaChanged();
                }
            }
        }

        [Column(Storage = "_Site_Supplier_Service_Area", DbType = "VarChar(50)")]
        public string Site_Supplier_Service_Area
        {
            get { return _Site_Supplier_Service_Area; }
            set
            {
                if ((_Site_Supplier_Service_Area != value))
                {
                    OnSite_Supplier_Service_AreaChanging(value);
                    SendPropertyChanging();
                    _Site_Supplier_Service_Area = value;
                    SendPropertyChanged("Site_Supplier_Service_Area");
                    OnSite_Supplier_Service_AreaChanged();
                }
            }
        }

        [Column(Storage = "_Site_Grade", DbType = "VarChar(10)")]
        public string Site_Grade
        {
            get { return _Site_Grade; }
            set
            {
                if ((_Site_Grade != value))
                {
                    OnSite_GradeChanging(value);
                    SendPropertyChanging();
                    _Site_Grade = value;
                    SendPropertyChanged("Site_Grade");
                    OnSite_GradeChanged();
                }
            }
        }

        [Column(Storage = "_Site_Permit_Needed", DbType = "Int")]
        public Nullable<int> Site_Permit_Needed
        {
            get { return _Site_Permit_Needed; }
            set
            {
                if ((_Site_Permit_Needed != value))
                {
                    OnSite_Permit_NeededChanging(value);
                    SendPropertyChanging();
                    _Site_Permit_Needed = value;
                    SendPropertyChanged("Site_Permit_Needed");
                    OnSite_Permit_NeededChanged();
                }
            }
        }

        [Column(Storage = "_Site_Code", DbType = "VarChar(50)")]
        public string Site_Code
        {
            get { return _Site_Code; }
            set
            {
                if ((_Site_Code != value))
                {
                    OnSite_CodeChanging(value);
                    SendPropertyChanging();
                    _Site_Code = value;
                    SendPropertyChanged("Site_Code");
                    OnSite_CodeChanged();
                }
            }
        }

        [Column(Storage = "_Site_Supplier_Code", DbType = "VarChar(50)")]
        public string Site_Supplier_Code
        {
            get { return _Site_Supplier_Code; }
            set
            {
                if ((_Site_Supplier_Code != value))
                {
                    OnSite_Supplier_CodeChanging(value);
                    SendPropertyChanging();
                    _Site_Supplier_Code = value;
                    SendPropertyChanged("Site_Supplier_Code");
                    OnSite_Supplier_CodeChanged();
                }
            }
        }

        [Column(Storage = "_Site_Name", DbType = "VarChar(50)")]
        public string Site_Name
        {
            get { return _Site_Name; }
            set
            {
                if ((_Site_Name != value))
                {
                    OnSite_NameChanging(value);
                    SendPropertyChanging();
                    _Site_Name = value;
                    SendPropertyChanged("Site_Name");
                    OnSite_NameChanged();
                }
            }
        }

        [Column(Storage = "_Site_Address", DbType = "NText", UpdateCheck = UpdateCheck.Never)]
        public string Site_Address
        {
            get { return _Site_Address; }
            set
            {
                if ((_Site_Address != value))
                {
                    OnSite_AddressChanging(value);
                    SendPropertyChanging();
                    _Site_Address = value;
                    SendPropertyChanged("Site_Address");
                    OnSite_AddressChanged();
                }
            }
        }

        [Column(Storage = "_Site_Postcode", DbType = "VarChar(15)")]
        public string Site_Postcode
        {
            get { return _Site_Postcode; }
            set
            {
                if ((_Site_Postcode != value))
                {
                    OnSite_PostcodeChanging(value);
                    SendPropertyChanging();
                    _Site_Postcode = value;
                    SendPropertyChanged("Site_Postcode");
                    OnSite_PostcodeChanged();
                }
            }
        }

        [Column(Storage = "_Site_Phone_No", DbType = "VarChar(15)")]
        public string Site_Phone_No
        {
            get { return _Site_Phone_No; }
            set
            {
                if ((_Site_Phone_No != value))
                {
                    OnSite_Phone_NoChanging(value);
                    SendPropertyChanging();
                    _Site_Phone_No = value;
                    SendPropertyChanged("Site_Phone_No");
                    OnSite_Phone_NoChanged();
                }
            }
        }

        [Column(Storage = "_Site_Fax_No", DbType = "VarChar(15)")]
        public string Site_Fax_No
        {
            get { return _Site_Fax_No; }
            set
            {
                if ((_Site_Fax_No != value))
                {
                    OnSite_Fax_NoChanging(value);
                    SendPropertyChanging();
                    _Site_Fax_No = value;
                    SendPropertyChanged("Site_Fax_No");
                    OnSite_Fax_NoChanged();
                }
            }
        }

        [Column(Storage = "_Site_Email_Address", DbType = "VarChar(100)")]
        public string Site_Email_Address
        {
            get { return _Site_Email_Address; }
            set
            {
                if ((_Site_Email_Address != value))
                {
                    OnSite_Email_AddressChanging(value);
                    SendPropertyChanging();
                    _Site_Email_Address = value;
                    SendPropertyChanged("Site_Email_Address");
                    OnSite_Email_AddressChanged();
                }
            }
        }

        [Column(Storage = "_Site_Manager", DbType = "VarChar(50)")]
        public string Site_Manager
        {
            get { return _Site_Manager; }
            set
            {
                if ((_Site_Manager != value))
                {
                    OnSite_ManagerChanging(value);
                    SendPropertyChanging();
                    _Site_Manager = value;
                    SendPropertyChanged("Site_Manager");
                    OnSite_ManagerChanged();
                }
            }
        }

        [Column(Storage = "_Site_Computer_Name", DbType = "VarChar(50)")]
        public string Site_Computer_Name
        {
            get { return _Site_Computer_Name; }
            set
            {
                if ((_Site_Computer_Name != value))
                {
                    OnSite_Computer_NameChanging(value);
                    SendPropertyChanging();
                    _Site_Computer_Name = value;
                    SendPropertyChanged("Site_Computer_Name");
                    OnSite_Computer_NameChanged();
                }
            }
        }

        [Column(Storage = "_Site_Open_Monday", DbType = "VarChar(96)")]
        public string Site_Open_Monday
        {
            get { return _Site_Open_Monday; }
            set
            {
                if ((_Site_Open_Monday != value))
                {
                    OnSite_Open_MondayChanging(value);
                    SendPropertyChanging();
                    _Site_Open_Monday = value;
                    SendPropertyChanged("Site_Open_Monday");
                    OnSite_Open_MondayChanged();
                }
            }
        }

        [Column(Storage = "_Site_Open_Tuesday", DbType = "VarChar(96)")]
        public string Site_Open_Tuesday
        {
            get { return _Site_Open_Tuesday; }
            set
            {
                if ((_Site_Open_Tuesday != value))
                {
                    OnSite_Open_TuesdayChanging(value);
                    SendPropertyChanging();
                    _Site_Open_Tuesday = value;
                    SendPropertyChanged("Site_Open_Tuesday");
                    OnSite_Open_TuesdayChanged();
                }
            }
        }

        [Column(Storage = "_Site_Open_Wednesday", DbType = "VarChar(96)")]
        public string Site_Open_Wednesday
        {
            get { return _Site_Open_Wednesday; }
            set
            {
                if ((_Site_Open_Wednesday != value))
                {
                    OnSite_Open_WednesdayChanging(value);
                    SendPropertyChanging();
                    _Site_Open_Wednesday = value;
                    SendPropertyChanged("Site_Open_Wednesday");
                    OnSite_Open_WednesdayChanged();
                }
            }
        }

        [Column(Storage = "_Site_Open_Thursday", DbType = "VarChar(96)")]
        public string Site_Open_Thursday
        {
            get { return _Site_Open_Thursday; }
            set
            {
                if ((_Site_Open_Thursday != value))
                {
                    OnSite_Open_ThursdayChanging(value);
                    SendPropertyChanging();
                    _Site_Open_Thursday = value;
                    SendPropertyChanged("Site_Open_Thursday");
                    OnSite_Open_ThursdayChanged();
                }
            }
        }

        [Column(Storage = "_Site_Open_Friday", DbType = "VarChar(96)")]
        public string Site_Open_Friday
        {
            get { return _Site_Open_Friday; }
            set
            {
                if ((_Site_Open_Friday != value))
                {
                    OnSite_Open_FridayChanging(value);
                    SendPropertyChanging();
                    _Site_Open_Friday = value;
                    SendPropertyChanged("Site_Open_Friday");
                    OnSite_Open_FridayChanged();
                }
            }
        }

        [Column(Storage = "_Site_Open_Saturday", DbType = "VarChar(96)")]
        public string Site_Open_Saturday
        {
            get { return _Site_Open_Saturday; }
            set
            {
                if ((_Site_Open_Saturday != value))
                {
                    OnSite_Open_SaturdayChanging(value);
                    SendPropertyChanging();
                    _Site_Open_Saturday = value;
                    SendPropertyChanged("Site_Open_Saturday");
                    OnSite_Open_SaturdayChanged();
                }
            }
        }

        [Column(Storage = "_Site_Open_Sunday", DbType = "VarChar(96)")]
        public string Site_Open_Sunday
        {
            get { return _Site_Open_Sunday; }
            set
            {
                if ((_Site_Open_Sunday != value))
                {
                    OnSite_Open_SundayChanging(value);
                    SendPropertyChanging();
                    _Site_Open_Sunday = value;
                    SendPropertyChanged("Site_Open_Sunday");
                    OnSite_Open_SundayChanged();
                }
            }
        }

        [Column(Storage = "_Site_Invoice_Address", DbType = "NText", UpdateCheck = UpdateCheck.Never)]
        public string Site_Invoice_Address
        {
            get { return _Site_Invoice_Address; }
            set
            {
                if ((_Site_Invoice_Address != value))
                {
                    OnSite_Invoice_AddressChanging(value);
                    SendPropertyChanging();
                    _Site_Invoice_Address = value;
                    SendPropertyChanged("Site_Invoice_Address");
                    OnSite_Invoice_AddressChanged();
                }
            }
        }

        [Column(Storage = "_Site_Invoice_Postcode", DbType = "VarChar(15)")]
        public string Site_Invoice_Postcode
        {
            get { return _Site_Invoice_Postcode; }
            set
            {
                if ((_Site_Invoice_Postcode != value))
                {
                    OnSite_Invoice_PostcodeChanging(value);
                    SendPropertyChanging();
                    _Site_Invoice_Postcode = value;
                    SendPropertyChanged("Site_Invoice_Postcode");
                    OnSite_Invoice_PostcodeChanged();
                }
            }
        }

        [Column(Storage = "_Site_Invoice_Name", DbType = "VarChar(50)")]
        public string Site_Invoice_Name
        {
            get { return _Site_Invoice_Name; }
            set
            {
                if ((_Site_Invoice_Name != value))
                {
                    OnSite_Invoice_NameChanging(value);
                    SendPropertyChanging();
                    _Site_Invoice_Name = value;
                    SendPropertyChanged("Site_Invoice_Name");
                    OnSite_Invoice_NameChanged();
                }
            }
        }

        [Column(Storage = "_Site_Dial_Up_Number", DbType = "VarChar(50)")]
        public string Site_Dial_Up_Number
        {
            get { return _Site_Dial_Up_Number; }
            set
            {
                if ((_Site_Dial_Up_Number != value))
                {
                    OnSite_Dial_Up_NumberChanging(value);
                    SendPropertyChanging();
                    _Site_Dial_Up_Number = value;
                    SendPropertyChanged("Site_Dial_Up_Number");
                    OnSite_Dial_Up_NumberChanged();
                }
            }
        }

        [Column(Storage = "_Site_Username", DbType = "VarChar(50)")]
        public string Site_Username
        {
            get { return _Site_Username; }
            set
            {
                if ((_Site_Username != value))
                {
                    OnSite_UsernameChanging(value);
                    SendPropertyChanging();
                    _Site_Username = value;
                    SendPropertyChanged("Site_Username");
                    OnSite_UsernameChanged();
                }
            }
        }

        [Column(Storage = "_Site_Password", DbType = "VarChar(50)")]
        public string Site_Password
        {
            get { return _Site_Password; }
            set
            {
                if ((_Site_Password != value))
                {
                    OnSite_PasswordChanging(value);
                    SendPropertyChanging();
                    _Site_Password = value;
                    SendPropertyChanged("Site_Password");
                    OnSite_PasswordChanged();
                }
            }
        }

        [Column(Storage = "_Site_Domain", DbType = "VarChar(50)")]
        public string Site_Domain
        {
            get { return _Site_Domain; }
            set
            {
                if ((_Site_Domain != value))
                {
                    OnSite_DomainChanging(value);
                    SendPropertyChanging();
                    _Site_Domain = value;
                    SendPropertyChanged("Site_Domain");
                    OnSite_DomainChanged();
                }
            }
        }

        [Column(Storage = "_Site_Local_Inbox", DbType = "VarChar(100)")]
        public string Site_Local_Inbox
        {
            get { return _Site_Local_Inbox; }
            set
            {
                if ((_Site_Local_Inbox != value))
                {
                    OnSite_Local_InboxChanging(value);
                    SendPropertyChanging();
                    _Site_Local_Inbox = value;
                    SendPropertyChanged("Site_Local_Inbox");
                    OnSite_Local_InboxChanged();
                }
            }
        }

        [Column(Storage = "_Site_Local_Outbox", DbType = "VarChar(100)")]
        public string Site_Local_Outbox
        {
            get { return _Site_Local_Outbox; }
            set
            {
                if ((_Site_Local_Outbox != value))
                {
                    OnSite_Local_OutboxChanging(value);
                    SendPropertyChanging();
                    _Site_Local_Outbox = value;
                    SendPropertyChanged("Site_Local_Outbox");
                    OnSite_Local_OutboxChanged();
                }
            }
        }

        [Column(Storage = "_Site_Remote_Inbox", DbType = "VarChar(100)")]
        public string Site_Remote_Inbox
        {
            get { return _Site_Remote_Inbox; }
            set
            {
                if ((_Site_Remote_Inbox != value))
                {
                    OnSite_Remote_InboxChanging(value);
                    SendPropertyChanging();
                    _Site_Remote_Inbox = value;
                    SendPropertyChanged("Site_Remote_Inbox");
                    OnSite_Remote_InboxChanged();
                }
            }
        }

        [Column(Storage = "_Site_Remote_Outbox", DbType = "VarChar(100)")]
        public string Site_Remote_Outbox
        {
            get { return _Site_Remote_Outbox; }
            set
            {
                if ((_Site_Remote_Outbox != value))
                {
                    OnSite_Remote_OutboxChanging(value);
                    SendPropertyChanging();
                    _Site_Remote_Outbox = value;
                    SendPropertyChanged("Site_Remote_Outbox");
                    OnSite_Remote_OutboxChanged();
                }
            }
        }

        [Column(Storage = "_Site_FTPServerAddress", DbType = "VarChar(100)")]
        public string Site_FTPServerAddress
        {
            get { return _Site_FTPServerAddress; }
            set
            {
                if ((_Site_FTPServerAddress != value))
                {
                    OnSite_FTPServerAddressChanging(value);
                    SendPropertyChanging();
                    _Site_FTPServerAddress = value;
                    SendPropertyChanged("Site_FTPServerAddress");
                    OnSite_FTPServerAddressChanged();
                }
            }
        }

        [Column(Storage = "_Site_ConnType", DbType = "Int")]
        public Nullable<int> Site_ConnType
        {
            get { return _Site_ConnType; }
            set
            {
                if ((_Site_ConnType != value))
                {
                    OnSite_ConnTypeChanging(value);
                    SendPropertyChanging();
                    _Site_ConnType = value;
                    SendPropertyChanged("Site_ConnType");
                    OnSite_ConnTypeChanged();
                }
            }
        }

        [Column(Storage = "_Site_Price_Per_Play", DbType = "VarChar(50)")]
        public string Site_Price_Per_Play
        {
            get { return _Site_Price_Per_Play; }
            set
            {
                if ((_Site_Price_Per_Play != value))
                {
                    OnSite_Price_Per_PlayChanging(value);
                    SendPropertyChanging();
                    _Site_Price_Per_Play = value;
                    SendPropertyChanged("Site_Price_Per_Play");
                    OnSite_Price_Per_PlayChanged();
                }
            }
        }

        [Column(Storage = "_Site_Price_Per_Play_Default", DbType = "Bit")]
        public Nullable<bool> Site_Price_Per_Play_Default
        {
            get { return _Site_Price_Per_Play_Default; }
            set
            {
                if ((_Site_Price_Per_Play_Default != value))
                {
                    OnSite_Price_Per_Play_DefaultChanging(value);
                    SendPropertyChanging();
                    _Site_Price_Per_Play_Default = value;
                    SendPropertyChanged("Site_Price_Per_Play_Default");
                    OnSite_Price_Per_Play_DefaultChanged();
                }
            }
        }

        [Column(Storage = "_Site_Jackpot", DbType = "VarChar(50)")]
        public string Site_Jackpot
        {
            get { return _Site_Jackpot; }
            set
            {
                if ((_Site_Jackpot != value))
                {
                    OnSite_JackpotChanging(value);
                    SendPropertyChanging();
                    _Site_Jackpot = value;
                    SendPropertyChanged("Site_Jackpot");
                    OnSite_JackpotChanged();
                }
            }
        }

        [Column(Storage = "_Site_Jackpot_Default", DbType = "Bit")]
        public Nullable<bool> Site_Jackpot_Default
        {
            get { return _Site_Jackpot_Default; }
            set
            {
                if ((_Site_Jackpot_Default != value))
                {
                    OnSite_Jackpot_DefaultChanging(value);
                    SendPropertyChanging();
                    _Site_Jackpot_Default = value;
                    SendPropertyChanged("Site_Jackpot_Default");
                    OnSite_Jackpot_DefaultChanged();
                }
            }
        }

        [Column(Storage = "_Site_Percentage_Payout", DbType = "VarChar(50)")]
        public string Site_Percentage_Payout
        {
            get { return _Site_Percentage_Payout; }
            set
            {
                if ((_Site_Percentage_Payout != value))
                {
                    OnSite_Percentage_PayoutChanging(value);
                    SendPropertyChanging();
                    _Site_Percentage_Payout = value;
                    SendPropertyChanged("Site_Percentage_Payout");
                    OnSite_Percentage_PayoutChanged();
                }
            }
        }

        [Column(Storage = "_Site_Percentage_Payout_Default", DbType = "Bit")]
        public Nullable<bool> Site_Percentage_Payout_Default
        {
            get { return _Site_Percentage_Payout_Default; }
            set
            {
                if ((_Site_Percentage_Payout_Default != value))
                {
                    OnSite_Percentage_Payout_DefaultChanging(value);
                    SendPropertyChanging();
                    _Site_Percentage_Payout_Default = value;
                    SendPropertyChanged("Site_Percentage_Payout_Default");
                    OnSite_Percentage_Payout_DefaultChanged();
                }
            }
        }

        [Column(Storage = "_Site_Start_Date", DbType = "VarChar(30)")]
        public string Site_Start_Date
        {
            get { return _Site_Start_Date; }
            set
            {
                if ((_Site_Start_Date != value))
                {
                    OnSite_Start_DateChanging(value);
                    SendPropertyChanging();
                    _Site_Start_Date = value;
                    SendPropertyChanged("Site_Start_Date");
                    OnSite_Start_DateChanged();
                }
            }
        }

        [Column(Storage = "_Site_End_Date", DbType = "VarChar(30)")]
        public string Site_End_Date
        {
            get { return _Site_End_Date; }
            set
            {
                if ((_Site_End_Date != value))
                {
                    OnSite_End_DateChanging(value);
                    SendPropertyChanging();
                    _Site_End_Date = value;
                    SendPropertyChanged("Site_End_Date");
                    OnSite_End_DateChanged();
                }
            }
        }

        [Column(Storage = "_Sage_Account_Ref", DbType = "VarChar(50)")]
        public string Sage_Account_Ref
        {
            get { return _Sage_Account_Ref; }
            set
            {
                if ((_Sage_Account_Ref != value))
                {
                    OnSage_Account_RefChanging(value);
                    SendPropertyChanging();
                    _Sage_Account_Ref = value;
                    SendPropertyChanged("Sage_Account_Ref");
                    OnSage_Account_RefChanged();
                }
            }
        }

        [Column(Storage = "_Site_Memo", DbType = "NText", UpdateCheck = UpdateCheck.Never)]
        public string Site_Memo
        {
            get { return _Site_Memo; }
            set
            {
                if ((_Site_Memo != value))
                {
                    OnSite_MemoChanging(value);
                    SendPropertyChanging();
                    _Site_Memo = value;
                    SendPropertyChanged("Site_Memo");
                    OnSite_MemoChanged();
                }
            }
        }

        [Column(Storage = "_Site_Company_Code", DbType = "VarChar(50)")]
        public string Site_Company_Code
        {
            get { return _Site_Company_Code; }
            set
            {
                if ((_Site_Company_Code != value))
                {
                    OnSite_Company_CodeChanging(value);
                    SendPropertyChanging();
                    _Site_Company_Code = value;
                    SendPropertyChanged("Site_Company_Code");
                    OnSite_Company_CodeChanged();
                }
            }
        }

        [Column(Storage = "_Site_Previous_Sub_Company_ID", DbType = "Int")]
        public Nullable<int> Site_Previous_Sub_Company_ID
        {
            get { return _Site_Previous_Sub_Company_ID; }
            set
            {
                if ((_Site_Previous_Sub_Company_ID != value))
                {
                    OnSite_Previous_Sub_Company_IDChanging(value);
                    SendPropertyChanging();
                    _Site_Previous_Sub_Company_ID = value;
                    SendPropertyChanged("Site_Previous_Sub_Company_ID");
                    OnSite_Previous_Sub_Company_IDChanged();
                }
            }
        }

        [Column(Storage = "_Site_Licensee_Commenced_Date", DbType = "SmallDateTime")]
        public Nullable<DateTime> Site_Licensee_Commenced_Date
        {
            get { return _Site_Licensee_Commenced_Date; }
            set
            {
                if ((_Site_Licensee_Commenced_Date != value))
                {
                    OnSite_Licensee_Commenced_DateChanging(value);
                    SendPropertyChanging();
                    _Site_Licensee_Commenced_Date = value;
                    SendPropertyChanged("Site_Licensee_Commenced_Date");
                    OnSite_Licensee_Commenced_DateChanged();
                }
            }
        }

        [Column(Storage = "_Site_Licensee_Agreement_Type", DbType = "VarChar(50)")]
        public string Site_Licensee_Agreement_Type
        {
            get { return _Site_Licensee_Agreement_Type; }
            set
            {
                if ((_Site_Licensee_Agreement_Type != value))
                {
                    OnSite_Licensee_Agreement_TypeChanging(value);
                    SendPropertyChanging();
                    _Site_Licensee_Agreement_Type = value;
                    SendPropertyChanged("Site_Licensee_Agreement_Type");
                    OnSite_Licensee_Agreement_TypeChanged();
                }
            }
        }

        [Column(Storage = "_Depot_ID", DbType = "Int")]
        public Nullable<int> Depot_ID
        {
            get { return _Depot_ID; }
            set
            {
                if ((_Depot_ID != value))
                {
                    OnDepot_IDChanging(value);
                    SendPropertyChanging();
                    _Depot_ID = value;
                    SendPropertyChanged("Depot_ID");
                    OnDepot_IDChanged();
                }
            }
        }

        [Column(Storage = "_Service_Depot_ID", DbType = "Int")]
        public Nullable<int> Service_Depot_ID
        {
            get { return _Service_Depot_ID; }
            set
            {
                if ((_Service_Depot_ID != value))
                {
                    OnService_Depot_IDChanging(value);
                    SendPropertyChanging();
                    _Service_Depot_ID = value;
                    SendPropertyChanged("Service_Depot_ID");
                    OnService_Depot_IDChanged();
                }
            }
        }

        [Column(Storage = "_Site_VAT_Exempt_Flag", DbType = "Bit")]
        public Nullable<bool> Site_VAT_Exempt_Flag
        {
            get { return _Site_VAT_Exempt_Flag; }
            set
            {
                if ((_Site_VAT_Exempt_Flag != value))
                {
                    OnSite_VAT_Exempt_FlagChanging(value);
                    SendPropertyChanging();
                    _Site_VAT_Exempt_Flag = value;
                    SendPropertyChanged("Site_VAT_Exempt_Flag");
                    OnSite_VAT_Exempt_FlagChanged();
                }
            }
        }

        [Column(Storage = "_Site_Company_Target", DbType = "Real")]
        public Nullable<float> Site_Company_Target
        {
            get { return _Site_Company_Target; }
            set
            {
                if ((_Site_Company_Target != value))
                {
                    OnSite_Company_TargetChanging(value);
                    SendPropertyChanging();
                    _Site_Company_Target = value;
                    SendPropertyChanged("Site_Company_Target");
                    OnSite_Company_TargetChanged();
                }
            }
        }

        [Column(Storage = "_Site_Company_Barrellage", DbType = "Int")]
        public Nullable<int> Site_Company_Barrellage
        {
            get { return _Site_Company_Barrellage; }
            set
            {
                if ((_Site_Company_Barrellage != value))
                {
                    OnSite_Company_BarrellageChanging(value);
                    SendPropertyChanging();
                    _Site_Company_Barrellage = value;
                    SendPropertyChanged("Site_Company_Barrellage");
                    OnSite_Company_BarrellageChanged();
                }
            }
        }

        [Column(Storage = "_Site_Image_Reference", DbType = "VarChar(255)")]
        public string Site_Image_Reference
        {
            get { return _Site_Image_Reference; }
            set
            {
                if ((_Site_Image_Reference != value))
                {
                    OnSite_Image_ReferenceChanging(value);
                    SendPropertyChanging();
                    _Site_Image_Reference = value;
                    SendPropertyChanged("Site_Image_Reference");
                    OnSite_Image_ReferenceChanged();
                }
            }
        }

        [Column(Storage = "_Site_Image_Reference_2", DbType = "VarChar(255)")]
        public string Site_Image_Reference_2
        {
            get { return _Site_Image_Reference_2; }
            set
            {
                if ((_Site_Image_Reference_2 != value))
                {
                    OnSite_Image_Reference_2Changing(value);
                    SendPropertyChanging();
                    _Site_Image_Reference_2 = value;
                    SendPropertyChanged("Site_Image_Reference_2");
                    OnSite_Image_Reference_2Changed();
                }
            }
        }

        [Column(Storage = "_Site_Trade_Type", DbType = "VarChar(50)")]
        public string Site_Trade_Type
        {
            get { return _Site_Trade_Type; }
            set
            {
                if ((_Site_Trade_Type != value))
                {
                    OnSite_Trade_TypeChanging(value);
                    SendPropertyChanging();
                    _Site_Trade_Type = value;
                    SendPropertyChanged("Site_Trade_Type");
                    OnSite_Trade_TypeChanged();
                }
            }
        }

        [Column(Storage = "_Sub_Company_Region_ID", DbType = "Int")]
        public Nullable<int> Sub_Company_Region_ID
        {
            get { return _Sub_Company_Region_ID; }
            set
            {
                if ((_Sub_Company_Region_ID != value))
                {
                    OnSub_Company_Region_IDChanging(value);
                    SendPropertyChanging();
                    _Sub_Company_Region_ID = value;
                    SendPropertyChanged("Sub_Company_Region_ID");
                    OnSub_Company_Region_IDChanged();
                }
            }
        }

        [Column(Storage = "_Sub_Company_Area_ID", DbType = "Int")]
        public Nullable<int> Sub_Company_Area_ID
        {
            get { return _Sub_Company_Area_ID; }
            set
            {
                if ((_Sub_Company_Area_ID != value))
                {
                    OnSub_Company_Area_IDChanging(value);
                    SendPropertyChanging();
                    _Sub_Company_Area_ID = value;
                    SendPropertyChanged("Sub_Company_Area_ID");
                    OnSub_Company_Area_IDChanged();
                }
            }
        }

        [Column(Storage = "_Sub_Company_District_ID", DbType = "Int")]
        public Nullable<int> Sub_Company_District_ID
        {
            get { return _Sub_Company_District_ID; }
            set
            {
                if ((_Sub_Company_District_ID != value))
                {
                    OnSub_Company_District_IDChanging(value);
                    SendPropertyChanging();
                    _Sub_Company_District_ID = value;
                    SendPropertyChanged("Sub_Company_District_ID");
                    OnSub_Company_District_IDChanged();
                }
            }
        }

        [Column(Storage = "_Site_Address_1", DbType = "VarChar(50)")]
        public string Site_Address_1
        {
            get { return _Site_Address_1; }
            set
            {
                if ((_Site_Address_1 != value))
                {
                    OnSite_Address_1Changing(value);
                    SendPropertyChanging();
                    _Site_Address_1 = value;
                    SendPropertyChanged("Site_Address_1");
                    OnSite_Address_1Changed();
                }
            }
        }

        [Column(Storage = "_Site_Address_2", DbType = "VarChar(50)")]
        public string Site_Address_2
        {
            get { return _Site_Address_2; }
            set
            {
                if ((_Site_Address_2 != value))
                {
                    OnSite_Address_2Changing(value);
                    SendPropertyChanging();
                    _Site_Address_2 = value;
                    SendPropertyChanged("Site_Address_2");
                    OnSite_Address_2Changed();
                }
            }
        }

        [Column(Storage = "_Site_Address_3", DbType = "VarChar(50)")]
        public string Site_Address_3
        {
            get { return _Site_Address_3; }
            set
            {
                if ((_Site_Address_3 != value))
                {
                    OnSite_Address_3Changing(value);
                    SendPropertyChanging();
                    _Site_Address_3 = value;
                    SendPropertyChanged("Site_Address_3");
                    OnSite_Address_3Changed();
                }
            }
        }

        [Column(Storage = "_Site_Address_4", DbType = "VarChar(50)")]
        public string Site_Address_4
        {
            get { return _Site_Address_4; }
            set
            {
                if ((_Site_Address_4 != value))
                {
                    OnSite_Address_4Changing(value);
                    SendPropertyChanging();
                    _Site_Address_4 = value;
                    SendPropertyChanged("Site_Address_4");
                    OnSite_Address_4Changed();
                }
            }
        }

        [Column(Storage = "_Site_Address_5", DbType = "VarChar(50)")]
        public string Site_Address_5
        {
            get { return _Site_Address_5; }
            set
            {
                if ((_Site_Address_5 != value))
                {
                    OnSite_Address_5Changing(value);
                    SendPropertyChanging();
                    _Site_Address_5 = value;
                    SendPropertyChanged("Site_Address_5");
                    OnSite_Address_5Changed();
                }
            }
        }

        [Column(Storage = "_Site_TX_Collection", DbType = "Bit")]
        public Nullable<bool> Site_TX_Collection
        {
            get { return _Site_TX_Collection; }
            set
            {
                if ((_Site_TX_Collection != value))
                {
                    OnSite_TX_CollectionChanging(value);
                    SendPropertyChanging();
                    _Site_TX_Collection = value;
                    SendPropertyChanged("Site_TX_Collection");
                    OnSite_TX_CollectionChanged();
                }
            }
        }

        [Column(Storage = "_Site_TX_Collection_Use_Default", DbType = "Bit")]
        public Nullable<bool> Site_TX_Collection_Use_Default
        {
            get { return _Site_TX_Collection_Use_Default; }
            set
            {
                if ((_Site_TX_Collection_Use_Default != value))
                {
                    OnSite_TX_Collection_Use_DefaultChanging(value);
                    SendPropertyChanging();
                    _Site_TX_Collection_Use_Default = value;
                    SendPropertyChanged("Site_TX_Collection_Use_Default");
                    OnSite_TX_Collection_Use_DefaultChanged();
                }
            }
        }

        [Column(Storage = "_Site_TX_Movement", DbType = "Bit")]
        public Nullable<bool> Site_TX_Movement
        {
            get { return _Site_TX_Movement; }
            set
            {
                if ((_Site_TX_Movement != value))
                {
                    OnSite_TX_MovementChanging(value);
                    SendPropertyChanging();
                    _Site_TX_Movement = value;
                    SendPropertyChanged("Site_TX_Movement");
                    OnSite_TX_MovementChanged();
                }
            }
        }

        [Column(Storage = "_Site_TX_Movement_Use_Default", DbType = "Bit")]
        public Nullable<bool> Site_TX_Movement_Use_Default
        {
            get { return _Site_TX_Movement_Use_Default; }
            set
            {
                if ((_Site_TX_Movement_Use_Default != value))
                {
                    OnSite_TX_Movement_Use_DefaultChanging(value);
                    SendPropertyChanging();
                    _Site_TX_Movement_Use_Default = value;
                    SendPropertyChanged("Site_TX_Movement_Use_Default");
                    OnSite_TX_Movement_Use_DefaultChanged();
                }
            }
        }

        [Column(Storage = "_Site_TX_EDC", DbType = "Bit")]
        public Nullable<bool> Site_TX_EDC
        {
            get { return _Site_TX_EDC; }
            set
            {
                if ((_Site_TX_EDC != value))
                {
                    OnSite_TX_EDCChanging(value);
                    SendPropertyChanging();
                    _Site_TX_EDC = value;
                    SendPropertyChanged("Site_TX_EDC");
                    OnSite_TX_EDCChanged();
                }
            }
        }

        [Column(Storage = "_Site_TX_EDC_Use_Detault", DbType = "Bit")]
        public Nullable<bool> Site_TX_EDC_Use_Detault
        {
            get { return _Site_TX_EDC_Use_Detault; }
            set
            {
                if ((_Site_TX_EDC_Use_Detault != value))
                {
                    OnSite_TX_EDC_Use_DetaultChanging(value);
                    SendPropertyChanging();
                    _Site_TX_EDC_Use_Detault = value;
                    SendPropertyChanged("Site_TX_EDC_Use_Detault");
                    OnSite_TX_EDC_Use_DetaultChanged();
                }
            }
        }

        [Column(Storage = "_Site_TX_Format", DbType = "Int")]
        public Nullable<int> Site_TX_Format
        {
            get { return _Site_TX_Format; }
            set
            {
                if ((_Site_TX_Format != value))
                {
                    OnSite_TX_FormatChanging(value);
                    SendPropertyChanging();
                    _Site_TX_Format = value;
                    SendPropertyChanged("Site_TX_Format");
                    OnSite_TX_FormatChanged();
                }
            }
        }

        [Column(Storage = "_Site_TX_Format_Use_Default", DbType = "Bit")]
        public Nullable<bool> Site_TX_Format_Use_Default
        {
            get { return _Site_TX_Format_Use_Default; }
            set
            {
                if ((_Site_TX_Format_Use_Default != value))
                {
                    OnSite_TX_Format_Use_DefaultChanging(value);
                    SendPropertyChanging();
                    _Site_TX_Format_Use_Default = value;
                    SendPropertyChanged("Site_TX_Format_Use_Default");
                    OnSite_TX_Format_Use_DefaultChanged();
                }
            }
        }

        [Column(Storage = "_Site_RX_Collection", DbType = "Bit")]
        public Nullable<bool> Site_RX_Collection
        {
            get { return _Site_RX_Collection; }
            set
            {
                if ((_Site_RX_Collection != value))
                {
                    OnSite_RX_CollectionChanging(value);
                    SendPropertyChanging();
                    _Site_RX_Collection = value;
                    SendPropertyChanged("Site_RX_Collection");
                    OnSite_RX_CollectionChanged();
                }
            }
        }

        [Column(Storage = "_Site_RX_Collection_Use_Default", DbType = "Bit")]
        public Nullable<bool> Site_RX_Collection_Use_Default
        {
            get { return _Site_RX_Collection_Use_Default; }
            set
            {
                if ((_Site_RX_Collection_Use_Default != value))
                {
                    OnSite_RX_Collection_Use_DefaultChanging(value);
                    SendPropertyChanging();
                    _Site_RX_Collection_Use_Default = value;
                    SendPropertyChanged("Site_RX_Collection_Use_Default");
                    OnSite_RX_Collection_Use_DefaultChanged();
                }
            }
        }

        [Column(Storage = "_Site_RX_Movement", DbType = "Bit")]
        public Nullable<bool> Site_RX_Movement
        {
            get { return _Site_RX_Movement; }
            set
            {
                if ((_Site_RX_Movement != value))
                {
                    OnSite_RX_MovementChanging(value);
                    SendPropertyChanging();
                    _Site_RX_Movement = value;
                    SendPropertyChanged("Site_RX_Movement");
                    OnSite_RX_MovementChanged();
                }
            }
        }

        [Column(Storage = "_Site_RX_Movement_Use_Default", DbType = "Bit")]
        public Nullable<bool> Site_RX_Movement_Use_Default
        {
            get { return _Site_RX_Movement_Use_Default; }
            set
            {
                if ((_Site_RX_Movement_Use_Default != value))
                {
                    OnSite_RX_Movement_Use_DefaultChanging(value);
                    SendPropertyChanging();
                    _Site_RX_Movement_Use_Default = value;
                    SendPropertyChanged("Site_RX_Movement_Use_Default");
                    OnSite_RX_Movement_Use_DefaultChanged();
                }
            }
        }

        [Column(Storage = "_Site_RX_EDC", DbType = "Bit")]
        public Nullable<bool> Site_RX_EDC
        {
            get { return _Site_RX_EDC; }
            set
            {
                if ((_Site_RX_EDC != value))
                {
                    OnSite_RX_EDCChanging(value);
                    SendPropertyChanging();
                    _Site_RX_EDC = value;
                    SendPropertyChanged("Site_RX_EDC");
                    OnSite_RX_EDCChanged();
                }
            }
        }

        [Column(Storage = "_Site_RX_EDC_Use_Detault", DbType = "Bit")]
        public Nullable<bool> Site_RX_EDC_Use_Detault
        {
            get { return _Site_RX_EDC_Use_Detault; }
            set
            {
                if ((_Site_RX_EDC_Use_Detault != value))
                {
                    OnSite_RX_EDC_Use_DetaultChanging(value);
                    SendPropertyChanging();
                    _Site_RX_EDC_Use_Detault = value;
                    SendPropertyChanged("Site_RX_EDC_Use_Detault");
                    OnSite_RX_EDC_Use_DetaultChanged();
                }
            }
        }

        [Column(Storage = "_Site_RX_Format", DbType = "Int")]
        public Nullable<int> Site_RX_Format
        {
            get { return _Site_RX_Format; }
            set
            {
                if ((_Site_RX_Format != value))
                {
                    OnSite_RX_FormatChanging(value);
                    SendPropertyChanging();
                    _Site_RX_Format = value;
                    SendPropertyChanged("Site_RX_Format");
                    OnSite_RX_FormatChanged();
                }
            }
        }

        [Column(Storage = "_Site_RX_Format_Use_Default", DbType = "Bit")]
        public Nullable<bool> Site_RX_Format_Use_Default
        {
            get { return _Site_RX_Format_Use_Default; }
            set
            {
                if ((_Site_RX_Format_Use_Default != value))
                {
                    OnSite_RX_Format_Use_DefaultChanging(value);
                    SendPropertyChanging();
                    _Site_RX_Format_Use_Default = value;
                    SendPropertyChanged("Site_RX_Format_Use_Default");
                    OnSite_RX_Format_Use_DefaultChanged();
                }
            }
        }

        [Column(Storage = "_NT_Phone_Book_Entry", DbType = "VarChar(100)")]
        public string NT_Phone_Book_Entry
        {
            get { return _NT_Phone_Book_Entry; }
            set
            {
                if ((_NT_Phone_Book_Entry != value))
                {
                    OnNT_Phone_Book_EntryChanging(value);
                    SendPropertyChanging();
                    _NT_Phone_Book_Entry = value;
                    SendPropertyChanged("NT_Phone_Book_Entry");
                    OnNT_Phone_Book_EntryChanged();
                }
            }
        }

        [Column(Storage = "_Next_Secondary_Sub_Company_ID", DbType = "Int")]
        public Nullable<int> Next_Secondary_Sub_Company_ID
        {
            get { return _Next_Secondary_Sub_Company_ID; }
            set
            {
                if ((_Next_Secondary_Sub_Company_ID != value))
                {
                    OnNext_Secondary_Sub_Company_IDChanging(value);
                    SendPropertyChanging();
                    _Next_Secondary_Sub_Company_ID = value;
                    SendPropertyChanged("Next_Secondary_Sub_Company_ID");
                    OnNext_Secondary_Sub_Company_IDChanged();
                }
            }
        }

        [Column(Storage = "_Site_Secondary_Sub_Company_Changeover", DbType = "VarChar(10)")]
        public string Site_Secondary_Sub_Company_Changeover
        {
            get { return _Site_Secondary_Sub_Company_Changeover; }
            set
            {
                if ((_Site_Secondary_Sub_Company_Changeover != value))
                {
                    OnSite_Secondary_Sub_Company_ChangeoverChanging(value);
                    SendPropertyChanging();
                    _Site_Secondary_Sub_Company_Changeover = value;
                    SendPropertyChanged("Site_Secondary_Sub_Company_Changeover");
                    OnSite_Secondary_Sub_Company_ChangeoverChanged();
                }
            }
        }

        [Column(Storage = "_Site_GPS_Location", DbType = "VarChar(50)")]
        public string Site_GPS_Location
        {
            get { return _Site_GPS_Location; }
            set
            {
                if ((_Site_GPS_Location != value))
                {
                    OnSite_GPS_LocationChanging(value);
                    SendPropertyChanging();
                    _Site_GPS_Location = value;
                    SendPropertyChanged("Site_GPS_Location");
                    OnSite_GPS_LocationChanged();
                }
            }
        }

        [Column(Storage = "_Site_Stop_Importing_EDI_On", DbType = "VarChar(10)")]
        public string Site_Stop_Importing_EDI_On
        {
            get { return _Site_Stop_Importing_EDI_On; }
            set
            {
                if ((_Site_Stop_Importing_EDI_On != value))
                {
                    OnSite_Stop_Importing_EDI_OnChanging(value);
                    SendPropertyChanging();
                    _Site_Stop_Importing_EDI_On = value;
                    SendPropertyChanged("Site_Stop_Importing_EDI_On");
                    OnSite_Stop_Importing_EDI_OnChanged();
                }
            }
        }

        [Column(Storage = "_Site_Non_Trading_Period_From", DbType = "VarChar(30)")]
        public string Site_Non_Trading_Period_From
        {
            get { return _Site_Non_Trading_Period_From; }
            set
            {
                if ((_Site_Non_Trading_Period_From != value))
                {
                    OnSite_Non_Trading_Period_FromChanging(value);
                    SendPropertyChanging();
                    _Site_Non_Trading_Period_From = value;
                    SendPropertyChanged("Site_Non_Trading_Period_From");
                    OnSite_Non_Trading_Period_FromChanged();
                }
            }
        }

        [Column(Storage = "_Site_Non_Trading_Period_To", DbType = "VarChar(30)")]
        public string Site_Non_Trading_Period_To
        {
            get { return _Site_Non_Trading_Period_To; }
            set
            {
                if ((_Site_Non_Trading_Period_To != value))
                {
                    OnSite_Non_Trading_Period_ToChanging(value);
                    SendPropertyChanging();
                    _Site_Non_Trading_Period_To = value;
                    SendPropertyChanged("Site_Non_Trading_Period_To");
                    OnSite_Non_Trading_Period_ToChanged();
                }
            }
        }

        [Column(Storage = "_Service_Area_ID", DbType = "Int")]
        public Nullable<int> Service_Area_ID
        {
            get { return _Service_Area_ID; }
            set
            {
                if ((_Service_Area_ID != value))
                {
                    OnService_Area_IDChanging(value);
                    SendPropertyChanging();
                    _Service_Area_ID = value;
                    SendPropertyChanged("Service_Area_ID");
                    OnService_Area_IDChanged();
                }
            }
        }

        [Column(Storage = "_Service_Supplier_ID", DbType = "Int")]
        public Nullable<int> Service_Supplier_ID
        {
            get { return _Service_Supplier_ID; }
            set
            {
                if ((_Service_Supplier_ID != value))
                {
                    OnService_Supplier_IDChanging(value);
                    SendPropertyChanging();
                    _Service_Supplier_ID = value;
                    SendPropertyChanged("Service_Supplier_ID");
                    OnService_Supplier_IDChanged();
                }
            }
        }

        [Column(Storage = "_Next_Sub_Company_ID", DbType = "Int")]
        public Nullable<int> Next_Sub_Company_ID
        {
            get { return _Next_Sub_Company_ID; }
            set
            {
                if ((_Next_Sub_Company_ID != value))
                {
                    OnNext_Sub_Company_IDChanging(value);
                    SendPropertyChanging();
                    _Next_Sub_Company_ID = value;
                    SendPropertyChanged("Next_Sub_Company_ID");
                    OnNext_Sub_Company_IDChanged();
                }
            }
        }

        [Column(Storage = "_Next_Sub_Company_Change_Date", DbType = "VarChar(30)")]
        public string Next_Sub_Company_Change_Date
        {
            get { return _Next_Sub_Company_Change_Date; }
            set
            {
                if ((_Next_Sub_Company_Change_Date != value))
                {
                    OnNext_Sub_Company_Change_DateChanging(value);
                    SendPropertyChanging();
                    _Next_Sub_Company_Change_Date = value;
                    SendPropertyChanged("Next_Sub_Company_Change_Date");
                    OnNext_Sub_Company_Change_DateChanged();
                }
            }
        }

        [Column(Storage = "_Previous_Sub_Company_Change_Date", DbType = "VarChar(30)")]
        public string Previous_Sub_Company_Change_Date
        {
            get { return _Previous_Sub_Company_Change_Date; }
            set
            {
                if ((_Previous_Sub_Company_Change_Date != value))
                {
                    OnPrevious_Sub_Company_Change_DateChanging(value);
                    SendPropertyChanging();
                    _Previous_Sub_Company_Change_Date = value;
                    SendPropertyChanged("Previous_Sub_Company_Change_Date");
                    OnPrevious_Sub_Company_Change_DateChanged();
                }
            }
        }

        [Column(Storage = "_Previous_Secondary_Sub_Company_ID", DbType = "Int")]
        public Nullable<int> Previous_Secondary_Sub_Company_ID
        {
            get { return _Previous_Secondary_Sub_Company_ID; }
            set
            {
                if ((_Previous_Secondary_Sub_Company_ID != value))
                {
                    OnPrevious_Secondary_Sub_Company_IDChanging(value);
                    SendPropertyChanging();
                    _Previous_Secondary_Sub_Company_ID = value;
                    SendPropertyChanged("Previous_Secondary_Sub_Company_ID");
                    OnPrevious_Secondary_Sub_Company_IDChanged();
                }
            }
        }

        [Column(Storage = "_Previous_Secondary_Sub_Company_Change_Date", DbType = "VarChar(30)")]
        public string Previous_Secondary_Sub_Company_Change_Date
        {
            get { return _Previous_Secondary_Sub_Company_Change_Date; }
            set
            {
                if ((_Previous_Secondary_Sub_Company_Change_Date != value))
                {
                    OnPrevious_Secondary_Sub_Company_Change_DateChanging(value);
                    SendPropertyChanging();
                    _Previous_Secondary_Sub_Company_Change_Date = value;
                    SendPropertyChanged("Previous_Secondary_Sub_Company_Change_Date");
                    OnPrevious_Secondary_Sub_Company_Change_DateChanged();
                }
            }
        }

        [Column(Storage = "_Site_Honeyframe_EDI", DbType = "Int")]
        public Nullable<int> Site_Honeyframe_EDI
        {
            get { return _Site_Honeyframe_EDI; }
            set
            {
                if ((_Site_Honeyframe_EDI != value))
                {
                    OnSite_Honeyframe_EDIChanging(value);
                    SendPropertyChanging();
                    _Site_Honeyframe_EDI = value;
                    SendPropertyChanged("Site_Honeyframe_EDI");
                    OnSite_Honeyframe_EDIChanged();
                }
            }
        }

        [Column(Storage = "_Site_Datapak_Protocol", DbType = "Int")]
        public Nullable<int> Site_Datapak_Protocol
        {
            get { return _Site_Datapak_Protocol; }
            set
            {
                if ((_Site_Datapak_Protocol != value))
                {
                    OnSite_Datapak_ProtocolChanging(value);
                    SendPropertyChanging();
                    _Site_Datapak_Protocol = value;
                    SendPropertyChanged("Site_Datapak_Protocol");
                    OnSite_Datapak_ProtocolChanged();
                }
            }
        }

        [Column(Storage = "_Site_Is_FreeFloat", DbType = "Bit")]
        public Nullable<bool> Site_Is_FreeFloat
        {
            get { return _Site_Is_FreeFloat; }
            set
            {
                if ((_Site_Is_FreeFloat != value))
                {
                    OnSite_Is_FreeFloatChanging(value);
                    SendPropertyChanging();
                    _Site_Is_FreeFloat = value;
                    SendPropertyChanged("Site_Is_FreeFloat");
                    OnSite_Is_FreeFloatChanged();
                }
            }
        }

        [Column(Storage = "_Site_Classification_ID", DbType = "Int NOT NULL")]
        public int Site_Classification_ID
        {
            get { return _Site_Classification_ID; }
            set
            {
                if ((_Site_Classification_ID != value))
                {
                    OnSite_Classification_IDChanging(value);
                    SendPropertyChanging();
                    _Site_Classification_ID = value;
                    SendPropertyChanged("Site_Classification_ID");
                    OnSite_Classification_IDChanged();
                }
            }
        }

        [Column(Storage = "_Site_Licensee_Agreement_End_Date", DbType = "DateTime")]
        public Nullable<DateTime> Site_Licensee_Agreement_End_Date
        {
            get { return _Site_Licensee_Agreement_End_Date; }
            set
            {
                if ((_Site_Licensee_Agreement_End_Date != value))
                {
                    OnSite_Licensee_Agreement_End_DateChanging(value);
                    SendPropertyChanging();
                    _Site_Licensee_Agreement_End_Date = value;
                    SendPropertyChanged("Site_Licensee_Agreement_End_Date");
                    OnSite_Licensee_Agreement_End_DateChanged();
                }
            }
        }

        [Column(Storage = "_Site_Licence_Number", DbType = "VarChar(25)")]
        public string Site_Licence_Number
        {
            get { return _Site_Licence_Number; }
            set
            {
                if ((_Site_Licence_Number != value))
                {
                    OnSite_Licence_NumberChanging(value);
                    SendPropertyChanging();
                    _Site_Licence_Number = value;
                    SendPropertyChanged("Site_Licence_Number");
                    OnSite_Licence_NumberChanged();
                }
            }
        }

        [Column(Storage = "_Site_Application", DbType = "SmallInt")]
        public Nullable<short> Site_Application
        {
            get { return _Site_Application; }
            set
            {
                if ((_Site_Application != value))
                {
                    OnSite_ApplicationChanging(value);
                    SendPropertyChanging();
                    _Site_Application = value;
                    SendPropertyChanged("Site_Application");
                    OnSite_ApplicationChanged();
                }
            }
        }

        [Column(Storage = "_Region", DbType = "VarChar(10)")]
        public string Region
        {
            get { return _Region; }
            set
            {
                if ((_Region != value))
                {
                    OnRegionChanging(value);
                    SendPropertyChanging();
                    _Region = value;
                    SendPropertyChanged("Region");
                    OnRegionChanged();
                }
            }
        }

        [Column(Storage = "_WebURL", DbType = "VarChar(2000)")]
        public string WebURL
        {
            get { return _WebURL; }
            set
            {
                if ((_WebURL != value))
                {
                    OnWebURLChanging(value);
                    SendPropertyChanging();
                    _WebURL = value;
                    SendPropertyChanged("WebURL");
                    OnWebURLChanged();
                }
            }
        }

        [Column(Storage = "_ConnectionString", DbType = "VarChar(200)")]
        public string ConnectionString
        {
            get { return _ConnectionString; }
            set
            {
                if ((_ConnectionString != value))
                {
                    OnConnectionStringChanging(value);
                    SendPropertyChanging();
                    _ConnectionString = value;
                    SendPropertyChanged("ConnectionString");
                    OnConnectionStringChanged();
                }
            }
        }

        [Column(Storage = "_Site_Status", DbType = "Xml", UpdateCheck = UpdateCheck.Never)]
        public XElement Site_Status
        {
            get { return _Site_Status; }
            set
            {
                if ((_Site_Status != value))
                {
                    OnSite_StatusChanging(value);
                    SendPropertyChanging();
                    _Site_Status = value;
                    SendPropertyChanged("Site_Status");
                    OnSite_StatusChanged();
                }
            }
        }

        [Column(Storage = "_Last_Updated_Time", DbType = "DateTime")]
        public Nullable<DateTime> Last_Updated_Time
        {
            get { return _Last_Updated_Time; }
            set
            {
                if ((_Last_Updated_Time != value))
                {
                    OnLast_Updated_TimeChanging(value);
                    SendPropertyChanging();
                    _Last_Updated_Time = value;
                    SendPropertyChanged("Last_Updated_Time");
                    OnLast_Updated_TimeChanged();
                }
            }
        }

        [Column(Storage = "_Apply_Retailer_Share", DbType = "Bit NOT NULL")]
        public bool Apply_Retailer_Share
        {
            get { return _Apply_Retailer_Share; }
            set
            {
                if ((_Apply_Retailer_Share != value))
                {
                    OnApply_Retailer_ShareChanging(value);
                    SendPropertyChanging();
                    _Apply_Retailer_Share = value;
                    SendPropertyChanged("Apply_Retailer_Share");
                    OnApply_Retailer_ShareChanged();
                }
            }
        }

        [Column(Storage = "_Site_Status_ID", DbType = "Int")]
        public Nullable<int> Site_Status_ID
        {
            get { return _Site_Status_ID; }
            set
            {
                if ((_Site_Status_ID != value))
                {
                    OnSite_Status_IDChanging(value);
                    SendPropertyChanging();
                    _Site_Status_ID = value;
                    SendPropertyChanged("Site_Status_ID");
                    OnSite_Status_IDChanged();
                }
            }
        }

        [Column(Storage = "_Site_Inactive_Date", DbType = "DateTime")]
        public Nullable<DateTime> Site_Inactive_Date
        {
            get { return _Site_Inactive_Date; }
            set
            {
                if ((_Site_Inactive_Date != value))
                {
                    OnSite_Inactive_DateChanging(value);
                    SendPropertyChanging();
                    _Site_Inactive_Date = value;
                    SendPropertyChanged("Site_Inactive_Date");
                    OnSite_Inactive_DateChanged();
                }
            }
        }

        [Column(Storage = "_NGA_Machine_ID", DbType = "Int")]
        public Nullable<int> NGA_Machine_ID
        {
            get { return _NGA_Machine_ID; }
            set
            {
                if ((_NGA_Machine_ID != value))
                {
                    OnNGA_Machine_IDChanging(value);
                    SendPropertyChanging();
                    _NGA_Machine_ID = value;
                    SendPropertyChanged("NGA_Machine_ID");
                    OnNGA_Machine_IDChanged();
                }
            }
        }

        [Column(Storage = "_Site_Setting_Profile_ID", DbType = "Int")]
        public Nullable<int> Site_Setting_Profile_ID
        {
            get { return _Site_Setting_Profile_ID; }
            set
            {
                if ((_Site_Setting_Profile_ID != value))
                {
                    OnSite_Setting_Profile_IDChanging(value);
                    SendPropertyChanging();
                    _Site_Setting_Profile_ID = value;
                    SendPropertyChanged("Site_Setting_Profile_ID");
                    OnSite_Setting_Profile_IDChanged();
                }
            }
        }

        [Column(Storage = "_SiteStatus", DbType = "VarChar(300)")]
        public string SiteStatus
        {
            get { return _SiteStatus; }
            set
            {
                if ((_SiteStatus != value))
                {
                    OnSiteStatusChanging(value);
                    SendPropertyChanging();
                    _SiteStatus = value;
                    SendPropertyChanged("SiteStatus");
                    OnSiteStatusChanged();
                }
            }
        }

        [Column(Storage = "_ExchangeKey", DbType = "VarChar(300)")]
        public string ExchangeKey
        {
            get { return _ExchangeKey; }
            set
            {
                if ((_ExchangeKey != value))
                {
                    OnExchangeKeyChanging(value);
                    SendPropertyChanging();
                    _ExchangeKey = value;
                    SendPropertyChanged("ExchangeKey");
                    OnExchangeKeyChanged();
                }
            }
        }

        [Column(Storage = "_Site_Fiscal_Code", DbType = "VarChar(16)")]
        public string Site_Fiscal_Code
        {
            get { return _Site_Fiscal_Code; }
            set
            {
                if ((_Site_Fiscal_Code != value))
                {
                    OnSite_Fiscal_CodeChanging(value);
                    SendPropertyChanging();
                    _Site_Fiscal_Code = value;
                    SendPropertyChanged("Site_Fiscal_Code");
                    OnSite_Fiscal_CodeChanged();
                }
            }
        }

        [Column(Storage = "_Site_Street_Number", DbType = "VarChar(15)")]
        public string Site_Street_Number
        {
            get { return _Site_Street_Number; }
            set
            {
                if ((_Site_Street_Number != value))
                {
                    OnSite_Street_NumberChanging(value);
                    SendPropertyChanging();
                    _Site_Street_Number = value;
                    SendPropertyChanged("Site_Street_Number");
                    OnSite_Street_NumberChanged();
                }
            }
        }

        [Column(Storage = "_Site_Province", DbType = "VarChar(15)")]
        public string Site_Province
        {
            get { return _Site_Province; }
            set
            {
                if ((_Site_Province != value))
                {
                    OnSite_ProvinceChanging(value);
                    SendPropertyChanging();
                    _Site_Province = value;
                    SendPropertyChanged("Site_Province");
                    OnSite_ProvinceChanged();
                }
            }
        }

        [Column(Storage = "_Site_Municipality", DbType = "VarChar(40)")]
        public string Site_Municipality
        {
            get { return _Site_Municipality; }
            set
            {
                if ((_Site_Municipality != value))
                {
                    OnSite_MunicipalityChanging(value);
                    SendPropertyChanging();
                    _Site_Municipality = value;
                    SendPropertyChanged("Site_Municipality");
                    OnSite_MunicipalityChanged();
                }
            }
        }

        [Column(Storage = "_Site_Cadastral_Code", DbType = "VarChar(15)")]
        public string Site_Cadastral_Code
        {
            get { return _Site_Cadastral_Code; }
            set
            {
                if ((_Site_Cadastral_Code != value))
                {
                    OnSite_Cadastral_CodeChanging(value);
                    SendPropertyChanging();
                    _Site_Cadastral_Code = value;
                    SendPropertyChanged("Site_Cadastral_Code");
                    OnSite_Cadastral_CodeChanged();
                }
            }
        }

        [Column(Storage = "_Site_Area", DbType = "Int")]
        public Nullable<int> Site_Area
        {
            get { return _Site_Area; }
            set
            {
                if ((_Site_Area != value))
                {
                    OnSite_AreaChanging(value);
                    SendPropertyChanging();
                    _Site_Area = value;
                    SendPropertyChanged("Site_Area");
                    OnSite_AreaChanged();
                }
            }
        }

        [Column(Storage = "_Site_Location_Type", DbType = "Int")]
        public Nullable<int> Site_Location_Type
        {
            get { return _Site_Location_Type; }
            set
            {
                if ((_Site_Location_Type != value))
                {
                    OnSite_Location_TypeChanging(value);
                    SendPropertyChanging();
                    _Site_Location_Type = value;
                    SendPropertyChanged("Site_Location_Type");
                    OnSite_Location_TypeChanged();
                }
            }
        }

        [Column(Storage = "_Site_Closed", DbType = "Int")]
        public Nullable<int> Site_Closed
        {
            get { return _Site_Closed; }
            set
            {
                if ((_Site_Closed != value))
                {
                    OnSite_ClosedChanging(value);
                    SendPropertyChanging();
                    _Site_Closed = value;
                    SendPropertyChanged("Site_Closed");
                    OnSite_ClosedChanged();
                }
            }
        }

        [Column(Storage = "_Site_Workstation", DbType = "VarChar(100)")]
        public string Site_Workstation
        {
            get { return _Site_Workstation; }
            set
            {
                if ((_Site_Workstation != value))
                {
                    OnSite_WorkstationChanging(value);
                    SendPropertyChanging();
                    _Site_Workstation = value;
                    SendPropertyChanged("Site_Workstation");
                    OnSite_WorkstationChanged();
                }
            }
        }

        [Column(Storage = "_Site_Toponym", DbType = "Int")]
        public Nullable<int> Site_Toponym
        {
            get { return _Site_Toponym; }
            set
            {
                if ((_Site_Toponym != value))
                {
                    OnSite_ToponymChanging(value);
                    SendPropertyChanging();
                    _Site_Toponym = value;
                    SendPropertyChanged("Site_Toponym");
                    OnSite_ToponymChanged();
                }
            }
        }

        [Column(Storage = "_Site_Closed_Date", DbType = "VarChar(30)")]
        public string Site_Closed_Date
        {
            get { return _Site_Closed_Date; }
            set
            {
                if ((_Site_Closed_Date != value))
                {
                    OnSite_Closed_DateChanging(value);
                    SendPropertyChanging();
                    _Site_Closed_Date = value;
                    SendPropertyChanged("Site_Closed_Date");
                    OnSite_Closed_DateChanged();
                }
            }
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region INotifyPropertyChanging Members

        public event PropertyChangingEventHandler PropertyChanging;

        #endregion

        protected virtual void SendPropertyChanging()
        {
            if ((PropertyChanging != null))
            {
                PropertyChanging(this, emptyChangingEventArgs);
            }
        }

        protected virtual void SendPropertyChanged(String propertyName)
        {
            if ((PropertyChanged != null))
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

    [Table(Name = "dbo.LookupMaster")]
    public partial class LookupMaster : INotifyPropertyChanging, INotifyPropertyChanged
    {

        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);

        private int _ID;

        private string _Code;

        private string _DisplayText;

        private string _Description;

        private System.Nullable<int> _ParentId;

        #region Extensibility Method Definitions
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
        partial void OnIDChanging(int value);
        partial void OnIDChanged();
        partial void OnCodeChanging(string value);
        partial void OnCodeChanged();
        partial void OnDisplayTextChanging(string value);
        partial void OnDisplayTextChanged();
        partial void OnDescriptionChanging(string value);
        partial void OnDescriptionChanged();
        partial void OnParentIdChanging(System.Nullable<int> value);
        partial void OnParentIdChanged();
        #endregion

        public LookupMaster()
        {
            OnCreated();
        }

        [Column(Storage = "_ID", AutoSync = AutoSync.OnInsert, DbType = "Int NOT NULL IDENTITY", IsPrimaryKey = true, IsDbGenerated = true)]
        public int ID
        {
            get
            {
                return this._ID;
            }
            set
            {
                if ((this._ID != value))
                {
                    this.OnIDChanging(value);
                    this.SendPropertyChanging();
                    this._ID = value;
                    this.SendPropertyChanged("ID");
                    this.OnIDChanged();
                }
            }
        }

        [Column(Storage = "_Code", DbType = "Char(6) NOT NULL", CanBeNull = false)]
        public string Code
        {
            get
            {
                return this._Code;
            }
            set
            {
                if ((this._Code != value))
                {
                    this.OnCodeChanging(value);
                    this.SendPropertyChanging();
                    this._Code = value;
                    this.SendPropertyChanged("Code");
                    this.OnCodeChanged();
                }
            }
        }

        [Column(Storage = "_DisplayText", DbType = "VarChar(100) NOT NULL", CanBeNull = false)]
        public string DisplayText
        {
            get
            {
                return this._DisplayText;
            }
            set
            {
                if ((this._DisplayText != value))
                {
                    this.OnDisplayTextChanging(value);
                    this.SendPropertyChanging();
                    this._DisplayText = value;
                    this.SendPropertyChanged("DisplayText");
                    this.OnDisplayTextChanged();
                }
            }
        }

        [Column(Storage = "_Description", DbType = "VarChar(100)")]
        public string Description
        {
            get
            {
                return this._Description;
            }
            set
            {
                if ((this._Description != value))
                {
                    this.OnDescriptionChanging(value);
                    this.SendPropertyChanging();
                    this._Description = value;
                    this.SendPropertyChanged("Description");
                    this.OnDescriptionChanged();
                }
            }
        }

        [Column(Storage = "_ParentId", DbType = "Int")]
        public System.Nullable<int> ParentId
        {
            get
            {
                return this._ParentId;
            }
            set
            {
                if ((this._ParentId != value))
                {
                    this.OnParentIdChanging(value);
                    this.SendPropertyChanging();
                    this._ParentId = value;
                    this.SendPropertyChanged("ParentId");
                    this.OnParentIdChanged();
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

    [Table(Name = "dbo.CodeMaster")]
    public partial class CodeMaster : INotifyPropertyChanging, INotifyPropertyChanged
    {

        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);

        private int _ID;

        private string _Code;

        private string _Description;

        private System.Nullable<int> _ParentID;

        #region Extensibility Method Definitions
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
        partial void OnIDChanging(int value);
        partial void OnIDChanged();
        partial void OnCodeChanging(string value);
        partial void OnCodeChanged();
        partial void OnDescriptionChanging(string value);
        partial void OnDescriptionChanged();
        partial void OnParentIDChanging(System.Nullable<int> value);
        partial void OnParentIDChanged();
        #endregion

        public CodeMaster()
        {
            OnCreated();
        }

        [Column(Storage = "_ID", AutoSync = AutoSync.Always, DbType = "Int NOT NULL IDENTITY", IsDbGenerated = true)]
        public int ID
        {
            get
            {
                return this._ID;
            }
            set
            {
                if ((this._ID != value))
                {
                    this.OnIDChanging(value);
                    this.SendPropertyChanging();
                    this._ID = value;
                    this.SendPropertyChanged("ID");
                    this.OnIDChanged();
                }
            }
        }

        [Column(Storage = "_Code", DbType = "Char(6) NOT NULL", CanBeNull = false, IsPrimaryKey = true)]
        public string Code
        {
            get
            {
                return this._Code;
            }
            set
            {
                if ((this._Code != value))
                {
                    this.OnCodeChanging(value);
                    this.SendPropertyChanging();
                    this._Code = value;
                    this.SendPropertyChanged("Code");
                    this.OnCodeChanged();
                }
            }
        }

        [Column(Storage = "_Description", DbType = "VarChar(100)")]
        public string Description
        {
            get
            {
                return this._Description;
            }
            set
            {
                if ((this._Description != value))
                {
                    this.OnDescriptionChanging(value);
                    this.SendPropertyChanging();
                    this._Description = value;
                    this.SendPropertyChanged("Description");
                    this.OnDescriptionChanged();
                }
            }
        }

        [Column(Storage = "_ParentID", DbType = "Int")]
        public System.Nullable<int> ParentID
        {
            get
            {
                return this._ParentID;
            }
            set
            {
                if ((this._ParentID != value))
                {
                    this.OnParentIDChanging(value);
                    this.SendPropertyChanging();
                    this._ParentID = value;
                    this.SendPropertyChanged("ParentID");
                    this.OnParentIDChanged();
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

    [Table(Name = "dbo.LanguageLookup")]
    internal partial class LanguageLookup
    {
        private string _ForeignText;
        private int _ID;

        private string _LanguageCode;
        private string _LookupMasterID;

        public LanguageLookup()
        {
        }

        [Column(Storage = "_ID", DbType = "Int NOT NULL IDENTITY", IsDbGenerated = true, IsPrimaryKey = true)]
        public int ID
        {
            get { return _ID; }
            set
            {
                if ((_ID != value))
                {
                    _ID = value;
                }
            }
        }

        [Column(Storage = "_LookupMasterID", DbType = "VarChar(100)")]
        public string LookupMasterID
        {
            get { return _LookupMasterID; }
            set
            {
                if ((_LookupMasterID != value))
                {
                    _LookupMasterID = value;
                }
            }
        }

        [Column(Storage = "_LanguageCode", DbType = "Char(5)")]
        public string LanguageCode
        {
            get { return _LanguageCode; }
            set
            {
                if ((_LanguageCode != value))
                {
                    _LanguageCode = value;
                }
            }
        }

        [Column(Storage = "_ForeignText", DbType = "VarChar(100)")]
        public string ForeignText
        {
            get { return _ForeignText; }
            set
            {
                if ((_ForeignText != value))
                {
                    _ForeignText = value;
                }
            }
        }
    }

}
