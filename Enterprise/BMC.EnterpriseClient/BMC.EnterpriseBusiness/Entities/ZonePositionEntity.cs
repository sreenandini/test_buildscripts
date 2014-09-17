using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.EnterpriseBusiness.Entities
{
    public partial class SiteDetailsEntity
    {
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

            private System.Nullable<int> _Site_Toponym;

            private string _Site_Closed_Date;

            private System.Nullable<bool> _AFT_Settings_Enabled;

            private bool _Site_Enabled;

            private string _Site_Region_Code;

            private System.Nullable<int> _Site_Connection_Type;

            private System.Nullable<int> _Site_MaxNumber_VLT;

            private string _Site_Connection_IPAddress;

            private System.Nullable<int> _Site_AAMS_Status;

            private System.Nullable<System.DateTime> _Site_Modified_Date;

            private string _Site_Termination_Status;

            private string _Site_ZonaRice;

            private System.Nullable<int> _IsTITOEnabled;

            private System.Nullable<int> _IsNonCashVoucherEnabled;

            private System.Nullable<int> _IsCrossTicketingEnabled;

            private string _TicketingURL;

            private System.Nullable<int> _StackerLimitPercentage;

        

            
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
                        this._Site_ID = value;
                    }
                }
            }

            
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
                        this._Site_Reference = value;
                    }
                }
            }

           
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
                        this._Sub_Company_ID = value;
                    }
                }
            }

            
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
                        this._Staff_ID = value;
                    }
                }
            }

           
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
                        this._Staff_ID_Default = value;
                    }
                }
            }

           
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
                        this._Access_Key_ID = value;
                    }
                }
            }

            
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
                        this._Access_Key_ID_Default = value;
                    }
                }
            }

           
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
                        this._Terms_Group_ID = value;
                    }
                }
            }

        
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
                        this._Terms_Group_ID_Default = value;
                    }
                }
            }

            
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
                        this._Computer_Build_ID = value;
                    }
                }
            }

            
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
                        this._Standard_Opening_Hours_ID = value;
                    }
                }
            }

           
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
                        this._Secondary_Sub_Company_ID = value;
                    }
                }
            }

            
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
                        this._Site_London_Rent = value;
                    }
                }
            }

           
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
                        this._Site_Supplier_Area = value;
                    }
                }
            }

            
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
                        this._Site_Supplier_Service_Area = value;
                    }
                }
            }

           
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
                        this._Site_Grade = value;
                    }
                }
            }

            
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
                        this._Site_Permit_Needed = value;
                    }
                }
            }

           
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
                        this._Site_Code = value;
                    }
                }
            }

           
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
                        this._Site_Supplier_Code = value;
                    }
                }
            }

           
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
                        this._Site_Name = value;
                    }
                }
            }

            
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
                        this._Site_Address = value;
                    }
                }
            }

            
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
                        this._Site_Postcode = value;
                    }
                }
            }

           
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
                        this._Site_Phone_No = value;
                    }
                }
            }

            
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
                        this._Site_Fax_No = value;
                    }
                }
            }

           
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
                        this._Site_Email_Address = value;
                    }
                }
            }

           
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
                        this._Site_Manager = value;
                    }
                }
            }

            
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
                        this._Site_Computer_Name = value;
                    }
                }
            }

            
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
                        this._Site_Open_Monday = value;
                    }
                }
            }

           
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
                        this._Site_Open_Tuesday = value;
                    }
                }
            }

            
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
                        this._Site_Open_Wednesday = value;
                    }
                }
            }

           
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
                        this._Site_Open_Thursday = value;
                    }
                }
            }

            
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
                        this._Site_Open_Friday = value;
                    }
                }
            }

            
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
                        this._Site_Open_Saturday = value;
                    }
                }
            }

            
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
                        this._Site_Open_Sunday = value;
                    }
                }
            }

           
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
                        this._Site_Invoice_Address = value;
                    }
                }
            }

            
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
                        this._Site_Invoice_Postcode = value;
                    }
                }
            }

           
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
                        this._Site_Invoice_Name = value;
                    }
                }
            }

          
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
                        this._Site_Dial_Up_Number = value;
                    }
                }
            }

            
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
                        this._Site_Username = value;
                    }
                }
            }

           
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
                        this._Site_Password = value;
                    }
                }
            }

            
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
                        this._Site_Domain = value;
                    }
                }
            }

           
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
                        this._Site_Local_Inbox = value;
                    }
                }
            }

            
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
                        this._Site_Local_Outbox = value;
                    }
                }
            }

           
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
                        this._Site_Remote_Inbox = value;
                    }
                }
            }

            
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
                        this._Site_Remote_Outbox = value;
                    }
                }
            }

            
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
                        this._Site_FTPServerAddress = value;
                    }
                }
            }

           
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
                        this._Site_ConnType = value;
                    }
                }
            }

           
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
                        this._Site_Price_Per_Play = value;
                    }
                }
            }

            
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
                        this._Site_Price_Per_Play_Default = value;
                    }
                }
            }

           
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
                        this._Site_Jackpot = value;
                    }
                }
            }

           
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
                        this._Site_Jackpot_Default = value;
                    }
                }
            }

           
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
                        this._Site_Percentage_Payout = value;
                    }
                }
            }

            
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
                        this._Site_Percentage_Payout_Default = value;
                    }
                }
            }

           
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
                        this._Site_Start_Date = value;
                    }
                }
            }

            
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
                        this._Site_End_Date = value;
                    }
                }
            }

           
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
                        this._Sage_Account_Ref = value;
                    }
                }
            }

           
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
                        this._Site_Memo = value;
                    }
                }
            }

         
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
                        this._Site_Company_Code = value;
                    }
                }
            }

          
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
                        this._Site_Previous_Sub_Company_ID = value;
                    }
                }
            }

          
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
                        this._Site_Licensee_Commenced_Date = value;
                    }
                }
            }

         
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
                        this._Site_Licensee_Agreement_Type = value;
                    }
                }
            }

          
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
                        this._Depot_ID = value;
                    }
                }
            }

           
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
                        this._Service_Depot_ID = value;
                    }
                }
            }

           
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
                        this._Site_VAT_Exempt_Flag = value;
                    }
                }
            }

          
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
                        this._Site_Company_Target = value;
                    }
                }
            }

           
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
                        this._Site_Company_Barrellage = value;
                    }
                }
            }

          
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
                        this._Site_Image_Reference = value;
                    }
                }
            }

           
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
                        this._Site_Image_Reference_2 = value;
                    }
                }
            }

           
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
                        this._Site_Trade_Type = value;
                    }
                }
            }

           
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
                        this._Sub_Company_Region_ID = value;
                    }
                }
            }

            
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
                        this._Sub_Company_Area_ID = value;
                    }
                }
            }

          
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
                        this._Sub_Company_District_ID = value;
                    }
                }
            }

            
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
                        this._Site_Address_1 = value;
                    }
                }
            }

          
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
                        this._Site_Address_2 = value;
                    }
                }
            }

           
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
                        this._Site_Address_3 = value;
                    }
                }
            }

           
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
                        this._Site_Address_4 = value;
                    }
                }
            }

           
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
                        this._Site_Address_5 = value;
                    }
                }
            }

           
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
                        this._Site_TX_Collection = value;
                    }
                }
            }

          
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
                        this._Site_TX_Collection_Use_Default = value;
                    }
                }
            }

           
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
                        this._Site_TX_Movement = value;
                    }
                }
            }

        
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
                        this._Site_TX_Movement_Use_Default = value;
                    }
                }
            }

           
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
                        this._Site_TX_EDC = value;
                    }
                }
            }

           
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
                        this._Site_TX_EDC_Use_Detault = value;
                    }
                }
            }

           
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
                        this._Site_TX_Format = value;
                    }
                }
            }

           
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
                        this._Site_TX_Format_Use_Default = value;
                    }
                }
            }

          
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
                        this._Site_RX_Collection = value;
                    }
                }
            }

           
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
                        this._Site_RX_Collection_Use_Default = value;
                    }
                }
            }

          
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
                        this._Site_RX_Movement = value;
                    }
                }
            }

           
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
                        this._Site_RX_Movement_Use_Default = value;
                    }
                }
            }

           
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
                        this._Site_RX_EDC = value;
                    }
                }
            }

           
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
                        this._Site_RX_EDC_Use_Detault = value;
                    }
                }
            }

          
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
                        this._Site_RX_Format = value;
                    }
                }
            }

           
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
                        this._Site_RX_Format_Use_Default = value;
                    }
                }
            }

          
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
                        this._NT_Phone_Book_Entry = value;
                    }
                }
            }

         
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
                        this._Next_Secondary_Sub_Company_ID = value;
                    }
                }
            }

           
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
                        this._Site_Secondary_Sub_Company_Changeover = value;
                    }
                }
            }

            
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
                        this._Site_GPS_Location = value;
                    }
                }
            }

           
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
                        this._Site_Stop_Importing_EDI_On = value;
                    }
                }
            }

            
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
                        this._Site_Non_Trading_Period_From = value;
                    }
                }
            }

           
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
                        this._Site_Non_Trading_Period_To = value;
                    }
                }
            }

           
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
                        this._Service_Area_ID = value;
                    }
                }
            }

          
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
                        this._Service_Supplier_ID = value;
                    }
                }
            }

           
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
                        this._Next_Sub_Company_ID = value;
                    }
                }
            }

           
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
                        this._Next_Sub_Company_Change_Date = value;
                    }
                }
            }

           
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
                        this._Previous_Sub_Company_Change_Date = value;
                    }
                }
            }

          
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
                        this._Previous_Secondary_Sub_Company_ID = value;
                    }
                }
            }

        
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
                        this._Previous_Secondary_Sub_Company_Change_Date = value;
                    }
                }
            }

           
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
                        this._Site_Honeyframe_EDI = value;
                    }
                }
            }

           
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
                        this._Site_Datapak_Protocol = value;
                    }
                }
            }

           
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
                        this._Site_Is_FreeFloat = value;
                    }
                }
            }

           
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
                        this._Site_Classification_ID = value;
                    }
                }
            }

            
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
                        this._Site_Licensee_Agreement_End_Date = value;
                    }
                }
            }

           
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
                        this._Site_Licence_Number = value;
                    }
                }
            }

           
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
                        this._Site_Application = value;
                    }
                }
            }

           
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
                        this._WebURL = value;
                    }
                }
            }

           
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
                        this._ConnectionString = value;
                    }
                }
            }

           
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
                        this._Site_Status = value;
                    }
                }
            }

          
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
                        this._Last_Updated_Time = value;
                    }
                }
            }

           
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
                        this._Apply_Retailer_Share = value;
                    }
                }
            }

           
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
                        this._Site_Status_ID = value;
                    }
                }
            }

           
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
                        this._Site_Inactive_Date = value;
                    }
                }
            }

           
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
                        this._NGA_Machine_ID = value;
                    }
                }
            }

           
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
                        this._Site_Setting_Profile_ID = value;
                    }
                }
            }

         
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
                        this._SiteStatus = value;
                    }
                }
            }

           
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
                        this._ExchangeKey = value;
                    }
                }
            }

         
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
                        this._Site_Fiscal_Code = value;
                    }
                }
            }

          
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
                        this._Site_Street_Number = value;
                    }
                }
            }

         
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
                        this._Site_Province = value;
                    }
                }
            }

         
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
                        this._Site_Municipality = value;
                    }
                }
            }

          
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
                        this._Site_Cadastral_Code = value;
                    }
                }
            }

         
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
                        this._Site_Area = value;
                    }
                }
            }

         
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
                        this._Site_Location_Type = value;
                    }
                }
            }

          
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
                        this._Site_Closed = value;
                    }
                }
            }

          
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
                        this._Site_Workstation = value;
                    }
                }
            }

         
            public System.Nullable<int> Site_Toponym
            {
                get
                {
                    return this._Site_Toponym;
                }
                set
                {
                    if ((this._Site_Toponym != value))
                    {
                        this._Site_Toponym = value;
                    }
                }
            }

           
            public string Site_Closed_Date
            {
                get
                {
                    return this._Site_Closed_Date;
                }
                set
                {
                    if ((this._Site_Closed_Date != value))
                    {
                        this._Site_Closed_Date = value;
                    }
                }
            }

           
            public System.Nullable<bool> AFT_Settings_Enabled
            {
                get
                {
                    return this._AFT_Settings_Enabled;
                }
                set
                {
                    if ((this._AFT_Settings_Enabled != value))
                    {
                        this._AFT_Settings_Enabled = value;
                    }
                }
            }

          
            public bool Site_Enabled
            {
                get
                {
                    return this._Site_Enabled;
                }
                set
                {
                    if ((this._Site_Enabled != value))
                    {
                        this._Site_Enabled = value;
                    }
                }
            }

          
            public string Site_Region_Code
            {
                get
                {
                    return this._Site_Region_Code;
                }
                set
                {
                    if ((this._Site_Region_Code != value))
                    {
                        this._Site_Region_Code = value;
                    }
                }
            }

          
            public System.Nullable<int> Site_Connection_Type
            {
                get
                {
                    return this._Site_Connection_Type;
                }
                set
                {
                    if ((this._Site_Connection_Type != value))
                    {
                        this._Site_Connection_Type = value;
                    }
                }
            }

          
            public System.Nullable<int> Site_MaxNumber_VLT
            {
                get
                {
                    return this._Site_MaxNumber_VLT;
                }
                set
                {
                    if ((this._Site_MaxNumber_VLT != value))
                    {
                        this._Site_MaxNumber_VLT = value;
                    }
                }
            }

           
            public string Site_Connection_IPAddress
            {
                get
                {
                    return this._Site_Connection_IPAddress;
                }
                set
                {
                    if ((this._Site_Connection_IPAddress != value))
                    {
                        this._Site_Connection_IPAddress = value;
                    }
                }
            }

            
            public System.Nullable<int> Site_AAMS_Status
            {
                get
                {
                    return this._Site_AAMS_Status;
                }
                set
                {
                    if ((this._Site_AAMS_Status != value))
                    {
                        this._Site_AAMS_Status = value;
                    }
                }
            }

          
            public System.Nullable<System.DateTime> Site_Modified_Date
            {
                get
                {
                    return this._Site_Modified_Date;
                }
                set
                {
                    if ((this._Site_Modified_Date != value))
                    {
                        this._Site_Modified_Date = value;
                    }
                }
            }

           
            public string Site_Termination_Status
            {
                get
                {
                    return this._Site_Termination_Status;
                }
                set
                {
                    if ((this._Site_Termination_Status != value))
                    {
                        this._Site_Termination_Status = value;
                    }
                }
            }

         
            public string Site_ZonaRice
            {
                get
                {
                    return this._Site_ZonaRice;
                }
                set
                {
                    if ((this._Site_ZonaRice != value))
                    {
                        this._Site_ZonaRice = value;
                    }
                }
            }

           
            public System.Nullable<int> IsTITOEnabled
            {
                get
                {
                    return this._IsTITOEnabled;
                }
                set
                {
                    if ((this._IsTITOEnabled != value))
                    {
                        this._IsTITOEnabled = value;
                    }
                }
            }

           
            public System.Nullable<int> IsNonCashVoucherEnabled
            {
                get
                {
                    return this._IsNonCashVoucherEnabled;
                }
                set
                {
                    if ((this._IsNonCashVoucherEnabled != value))
                    {
                        this._IsNonCashVoucherEnabled = value;
                    }
                }
            }

          
            public System.Nullable<int> IsCrossTicketingEnabled
            {
                get
                {
                    return this._IsCrossTicketingEnabled;
                }
                set
                {
                    if ((this._IsCrossTicketingEnabled != value))
                    {
                        this._IsCrossTicketingEnabled = value;
                    }
                }
            }

           
            public string TicketingURL
            {
                get
                {
                    return this._TicketingURL;
                }
                set
                {
                    if ((this._TicketingURL != value))
                    {
                        this._TicketingURL = value;
                    }
                }
            }

           
            public System.Nullable<int> StackerLimitPercentage
            {
                get
                {
                    return this._StackerLimitPercentage;
                }
                set
                {
                    if ((this._StackerLimitPercentage != value))
                    {
                        this._StackerLimitPercentage = value;
                    }
                }
            }
    }

    public class BarPositionInfoEntity
    {

        public int Bar_Position_Invoice_Period;

        public string Site_Code;

        public string Site_Name;

        public System.Nullable<int> Site_ID;

        public string Bar_Position_Name;

        public string Bar_Position_Company_Position_Code;

        public string Bar_Position_End_Date;

        public System.Nullable<int> Machine_Type_ID;

        public string Bar_Position_Location;

        public System.Nullable<int> Zone_ID;

        public string Bar_Position_Supplier_Site_Code;

        public string Bar_Position_Supplier_Position_Code;

        public string Bar_Position_Image_Reference;

        public System.Nullable<bool> Bar_Position_Price_Per_Play_Default;

        public string Bar_Position_Price_Per_Play;

        public System.Nullable<bool> Bar_Position_Jackpot_Default;

        public string Bar_Position_Jackpot;

        public System.Nullable<bool> Bar_Position_Percentage_Payout_Default;

        public string Bar_Position_Percentage_Payout;

        public System.Nullable<bool> Terms_Group_ID_Default;

        public System.Nullable<int> Terms_Group_ID;

        public System.Nullable<bool> Access_Key_ID_Default;

        public System.Nullable<int> Access_Key_ID;

        public System.Nullable<int> Depot_ID;

        public string Depot_Name;

        public System.Nullable<int> Operator_ID;

        public string Operator_Name;

        public string Machine_Name;

        public string Machine_BACTA_Code;

        public string Machine_Stock_No;

        public string Installation_End_Date;

        public System.Nullable<int> Bar_Position_Category;

        public System.Nullable<bool> Bar_Position_Override_Licence;

        public System.Nullable<bool> Bar_Position_Override_Shares;

        public System.Nullable<bool> Bar_Position_Override_Rent;

        public System.Nullable<float> Bar_Position_Rent;

        public System.Nullable<float> Bar_Position_Rent_Previous;

        public System.Nullable<float> Bar_Position_Rent_Future;

        public string Bar_Position_Rent_Past_Date;

        public string Bar_Position_Rent_Future_Date;

        public System.Nullable<float> Bar_Position_Supplier_Share;

        public System.Nullable<float> Bar_Position_Site_Share;

        public System.Nullable<float> Bar_Position_Owners_Share;

        public System.Nullable<float> Bar_Position_Secondary_Owners_Share;

        public System.Nullable<float> Bar_Position_Supplier_Share_Previous;

        public System.Nullable<float> Bar_Position_Site_Share_Previous;

        public System.Nullable<float> Bar_Position_Owners_Share_Previous;

        public System.Nullable<float> Bar_Position_Secondary_Owners_Share_Previous;

        public System.Nullable<int> Bar_Position_Collection_Period;

        public string Terms_Group_Past_Changeover_Date;

        public System.Nullable<int> Terms_Group_Past_ID;

        public System.Nullable<float> Bar_Position_Supplier_Share_Future;

        public System.Nullable<float> Bar_Position_Site_Share_Future;

        public System.Nullable<float> Bar_Position_Owners_Share_Future;

        public System.Nullable<float> Bar_Position_Secondary_Owners_Share_Future;

        public string Bar_Position_Share_Past_Date;

        public string Bar_Position_Share_Future_Date;

        public System.Nullable<float> Bar_Position_Licence_Charge;

        public System.Nullable<float> Bar_Position_Licence_Previous;

        public System.Nullable<float> Bar_Position_Licence_Future;

        public string Bar_Position_Licence_Past_Date;

        public string Bar_Position_Licence_Future_Date;

        public System.Nullable<bool> Bar_Position_Use_Terms;

        public string Bar_Position_Collection_Day;

        public System.Nullable<bool> Bar_Position_Use_Site_Share_For_Secondary_Brewery;

        public string Terms_Group_Changeover_Date;

        public System.Nullable<int> Terms_Group_Future_ID;

        public System.Nullable<bool> Bar_Position_Prize_LOS;

        public System.Nullable<int> Bar_Position_Rent_Schedule_ID;

        public System.Nullable<int> Bar_Position_Share_Schedule_ID;

        public System.Nullable<bool> Bar_Position_Override_Rent_Schedule;

        public System.Nullable<bool> Bar_Position_Override_Share_Schedule;

        public System.Nullable<bool> Bar_Position_Override_Rent_From_Schedule_To_Rent;

        public System.Nullable<bool> Bar_Position_Override_Rent_From_Rent_To_Schedule;

        public string Bar_Position_Override_Rent_From_Schedule_To_Rent_Date;

        public string Bar_Position_Override_Rent_From_Rent_To_Schedule_Date;

        public System.Nullable<int> Bar_Position_Rent_Schedule_ID_From;

        public System.Nullable<bool> Bar_Position_Disable_EDI_Export;

        public bool Bar_Position_IsEnable;
    }

    public partial class BarPositionEntity
    {
        public int Bar_Position_ID ;
        public System.Nullable<int> Site_ID ;
        public System.Nullable<int> Zone_ID ;
        public System.Nullable<int> Access_Key_ID ;
        public System.Nullable<bool> Access_Key_ID_Default ;
        public System.Nullable<int> Terms_Group_ID ;
        public string Terms_Group_Changeover_Date ;
        public System.Nullable<int> Terms_Group_Future_ID;
        public string Terms_Group_Past_Changeover_Date;
        public System.Nullable<int> Terms_Group_Past_ID;
        public System.Nullable<bool> Terms_Group_ID_Default;
        public System.Nullable<int> Duty_ID;
        public System.Nullable<int> Depot_ID;
        public System.Nullable<int> Machine_Type_ID;
        public string Bar_Position_Name;
        public string Bar_Position_Location;
        public string Bar_Position_Start_Date;
        public string Bar_Position_End_Date;
        public string Bar_Position_Collection_Day;
        public string Bar_Position_Price_Per_Play;
        public System.Nullable<bool> Bar_Position_Price_Per_Play_Default;
        public string Bar_Position_Jackpot;
        public System.Nullable<bool> Bar_Position_Jackpot_Default;
        public string Bar_Position_Percentage_Payout;
        public System.Nullable<bool> Bar_Position_Percentage_Payout_Default;
        public string Bar_Position_Last_Collection_Date;
        public string Bar_Position_Collection_Rent_Paid_Until;
        public System.Nullable<int> Bar_Position_Collection_Period;
        public string Bar_Position_Supplier_AMEDIS_Code;
        public string Bar_Position_Supplier_Depot_AMEDIS_Code;
        public string Bar_Position_Supplier_Site_Code;
        public string Bar_Position_Supplier_Position_Code;
        public string Bar_Position_Supplier_Area;
        public string Bar_Position_Supplier_Service_Area;
        public string Bar_Position_Company_Position_Code;
        public System.Nullable<float> Bar_Position_Company_Target;
        public System.Nullable<int> Bar_Position_Collection_Frequency;
        public string Bar_Position_Image_Reference;
        public System.Nullable<int> Bar_Position_Machine_Type_AMEDIS_Code;
        public System.Nullable<float> Bar_Position_Rent;
        public System.Nullable<float> Bar_Position_Rent_Previous;
        public System.Nullable<float> Bar_Position_Rent_Future;
        public string Bar_Position_Rent_Past_Date;
        public string Bar_Position_Rent_Future_Date;
        public System.Nullable<float> Bar_Position_Supplier_Share;
        public System.Nullable<float> Bar_Position_Site_Share;
        public System.Nullable<float> Bar_Position_Owners_Share;
        public System.Nullable<float> Bar_Position_Secondary_Owners_Share;
        public System.Nullable<float> Bar_Position_Supplier_Share_Previous;
        public System.Nullable<float> Bar_Position_Site_Share_Previous;
        public System.Nullable<float> Bar_Position_Owners_Share_Previous;
        public System.Nullable<float> Bar_Position_Secondary_Owners_Share_Previous;
        public System.Nullable<float> Bar_Position_Supplier_Share_Future;
        public System.Nullable<float> Bar_Position_Site_Share_Future;
        public System.Nullable<float> Bar_Position_Owners_Share_Future;
        public System.Nullable<float> Bar_Position_Secondary_Owners_Share_Future;
        public string Bar_Position_Share_Past_Date;
        public string Bar_Position_Share_Future_Date;
        public System.Nullable<float> Bar_Position_Licence_Charge;
        public System.Nullable<float> Bar_Position_Licence_Previous;
        public System.Nullable<float> Bar_Position_Licence_Future;
        public string Bar_Position_Licence_Past_Date;
        public string Bar_Position_Licence_Future_Date;
        public System.Nullable<bool> Bar_Position_Use_Terms;
        public System.Nullable<bool> Bar_Position_TX_Collection;
        public System.Nullable<bool> Bar_Position_TX_Collection_Use_Default;
        public System.Nullable<bool> Bar_Position_TX_Movement;
        public System.Nullable<bool> Bar_Position_TX_Movement_Use_Default;
        public System.Nullable<bool> Bar_Position_TX_EDC;
        public System.Nullable<bool> Bar_Position_TX_EDC_Use_Detault;
        public System.Nullable<int> Bar_Position_TX_Format;
        public System.Nullable<bool> Bar_Position_TX_Format_Use_Default;
        public System.Nullable<bool> Bar_Position_RX_Collection;
        public System.Nullable<bool> Bar_Position_RX_Collection_Use_Default;
        public System.Nullable<bool> Bar_Position_RX_Movement;
        public System.Nullable<bool> Bar_Position_RX_Movement_Use_Default;
        public System.Nullable<bool> Bar_Position_RX_EDC;
        public System.Nullable<bool> Bar_Position_RX_EDC_Use_Detault;
        public System.Nullable<int> Bar_Position_RX_Format;
        public System.Nullable<bool> Bar_Position_RX_Format_Use_Default;
        public System.Nullable<float> Bar_Position_Net_Target;
        public System.Nullable<int> Bar_Position_Below_Net_Target_Counter;
        public System.Nullable<int> Bar_Position_Below_Company_Target_Counter;
        public System.Nullable<bool> Bar_Position_Security_Required;
        public System.Nullable<bool> Bar_Position_Site_Has_Cashbox_Keys;
        public System.Nullable<bool> Bar_Position_Site_Has_FreePlay_Access;
        public System.Nullable<bool> Bar_Position_Override_Rent;
        public System.Nullable<bool> Bar_Position_Override_Shares;
        public System.Nullable<bool> Bar_Position_Override_Licence;
        public System.Nullable<int> Bar_Position_Category;
        public System.Nullable<float> Bar_Position_PPL_Charge;
        public System.Nullable<float> Bar_Position_PPL_Previous;
        public System.Nullable<float> Bar_Position_PPL_Future;
        public string Bar_Position_PPL_Past_Date;
        public string Bar_Position_PPL_Future_Date;
        public System.Nullable<int> Bar_Position_Float_Issued;
        public System.Nullable<int> Bar_Position_Float_Recovered;
        public System.Nullable<bool> Bar_Position_Use_Site_Share_For_Secondary_Brewery;
        public System.Nullable<bool> Bar_Position_Prize_LOS;
        public System.Nullable<int> Bar_Position_Rent_Schedule_ID;
        public System.Nullable<int> Bar_Position_Share_Schedule_ID;
        public System.Nullable<bool> Bar_Position_Override_Rent_Schedule;
        public System.Nullable<bool> Bar_Position_Override_Share_Schedule;
        public System.Nullable<int> Bar_Position_Last_Collection_ID;
        public System.Nullable<bool> Bar_Position_Override_Rent_From_Schedule_To_Rent;
        public System.Nullable<bool> Bar_Position_Override_Rent_From_Rent_To_Schedule;
        public string Bar_Position_Override_Rent_From_Schedule_To_Rent_Date;
        public string Bar_Position_Override_Rent_From_Rent_To_Schedule_Date;
        public System.Nullable<int> Bar_Position_Rent_Schedule_ID_From;
        public System.Nullable<bool> Bar_Position_Disable_EDI_Export;
        public int Bar_Position_Invoice_Period;
        public System.Nullable<int> Bar_Position_Machine_Enabled;
        public System.Nullable<int> Bar_Position_Note_Acceptor_Enabled;
        public System.Nullable<System.DateTime> Bar_Position_Machine_Enabled_Date;
        public bool Bar_Position_IsEnable;
    }
    

    public partial class ZoneEntity
    {

        public int Zone_ID{get;set;}

        public string Zone_Name { get; set; }

        public int AssignedZones { get; set; }

        public bool PromotionEnabled { get; set; }

        public int? OpenHours { get; set; }
    }

    public class BarPositionExtensionEntity
    {
        public int Bar_Position_ID;
        public System.Data.Linq.Binary Bar_Position_Image;
    }


}
