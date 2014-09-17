using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.EnterpriseBusiness.Entities
{
    //class SiteDetailsEntity
    public partial class AdminSiteEntity
    {
        private int _Site_ID;

        private System.Nullable<int> _Service_Supplier_ID;

        private System.Nullable<int> _Service_Area_ID;

        private System.Nullable<int> _Service_Depot_ID;

        private string _Site_Name;

        private string _Site_Code;

        private string _Site_Supplier_Code;

        private string _Site_Company_Code;

        private string _Site_Phone_No;

        private string _Site_Fax_No;

        private string _Site_Email_Address;

        private string _Site_Address_1;

        private string _Site_Address_2;

        private string _Site_Address_3;

        private string _Site_Address_4;

        private string _Site_Address_5;

        private string _Site_Postcode;

        private string _Site_Manager;

        private System.Nullable<bool> _Site_Price_Per_Play_Default;

        private string _Site_Price_Per_Play;

        private System.Nullable<bool> _Site_Jackpot_Default;

        private string _Site_Jackpot;

        private System.Nullable<bool> _Site_Percentage_Payout_Default;

        private string _Site_Percentage_Payout;

        private System.Nullable<int> _Sub_Company_ID;

        private System.Nullable<int> _Company_ID;

        private System.Nullable<bool> _Terms_Group_ID_Default;

        private System.Nullable<int> _Terms_Group_ID;

        private System.Nullable<bool> _Access_Key_ID_Default;

        private System.Nullable<int> _Access_Key_ID;

        private System.Nullable<bool> _Staff_ID_Default;

        private System.Nullable<int> _Staff_ID;

        private string _Site_Invoice_Name;

        private string _Site_Invoice_Address;

        private string _Site_Invoice_Postcode;

        private System.Nullable<int> _Sub_Company_Region_ID;

        private System.Nullable<int> _Sub_Company_Area_ID;

        private System.Nullable<int> _Sub_Company_District_ID;

        private string _Site_Image_Reference;

        private string _Site_Image_Reference_2;

        private System.Nullable<int> _Site_Closed;

        private System.Nullable<int> _Depot_ID;

        private System.Nullable<int> _Supplier_ID;

        private int _Site_Classification_ID;

        private string _Site_Grade;

        private string _Sage_Account_Ref;

        private System.Nullable<bool> _Site_Is_FreeFloat;

        private string _Site_Local_Inbox;

        private string _Site_Local_Outbox;

        private string _Site_Memo;

        private string _Site_Reference;


        private string _Site_Trade_Type;

        private string _Site_Non_Trading_Period_From;

        private string _Site_Non_Trading_Period_To;

        private string _Site_Supplier_Service_Area;

        private string _Site_Supplier_Area;

        private System.Nullable<int> _Standard_Opening_Hours_ID;

        private System.Nullable<int> _Next_Sub_Company_ID;

        private string _Next_Sub_Company_Change_Date;

        private System.Nullable<int> _Site_Previous_Sub_Company_ID;

        private string _Previous_Sub_Company_Change_Date;

        private System.Nullable<int> _Site_Honeyframe_EDI;

        private System.Nullable<int> _Site_Datapak_Protocol;

        private string _Site_Start_Date;

        private string _Site_End_Date;

        private string _Site_Licence_Number;

        private string _Site_Fiscal_Code;

        private string _Site_Street_Number;

        private string _Site_Province;

        private string _Site_Municipality;

        private string _Site_Cadastral_Code;

        private System.Nullable<int> _Site_Area;

        private System.Nullable<int> _Site_Location_Type;

        private System.Nullable<int> _Site_Toponym;

        private System.Nullable<System.DateTime> _Site_Licensee_Commenced_Date;

        private System.Nullable<System.DateTime> _Site_Licensee_Agreement_End_Date;

        private string _Site_Licensee_Agreement_Type;

        private System.Nullable<short> _Site_Application;

        private string _Region;

        private string _WebURL;

        private int _Site_Status_ID;

        private System.Nullable<System.DateTime> _Site_Inactive_Date;

        private string _Site_Connection_IPAddress;

        private System.Nullable<int> _Site_MaxNumber_VLT;

        private string _Site_ZonaRice;

        private System.Nullable<int> _StackerLimitPercentage;
        //************************Combining Entity Classes together*********************************//
        private bool _Site_Enabled;

        private int _IsTITOEnabled;

        private int _IsCrossTicketingEnabled;

        private int _IsNonCashVoucherEnabled;

        private string _TicketingURL;

        private System.Nullable<int> _Bar_Position_ID;

        private string _Operator_Name;

        private int _Operator_ID;

        private string _Depot_Name;

        private string _Site_Classification_Name;

        private string _Standard_Opening_Hours_Description;

        private string _Company_Name;
        
        private string _company_subcompany;

        private string _Service_Area_Name;
        //Aftsetting entity start

        
        //Aftsetting entity End


        //subcompany send value//
        private string _Sub_Company_Region_Name;
        private string _Staff_Last_Name;
        private string _Staff_First_Name;
        public string Sub_Company_Region_Name
        {
            get
            {
                return this._Sub_Company_Region_Name;
            }
            set
            {
                if ((this._Sub_Company_Region_Name != value))
                {
                    this._Sub_Company_Region_Name = value;
                }
            }
        }

        public string Staff_Last_Name
        {
            get
            {
                return this._Staff_Last_Name;
            }
            set
            {
                if ((this._Staff_Last_Name != value))
                {
                    this._Staff_Last_Name = value;
                }
            }
        }
        public string Staff_First_Name
        {
            get
            {
                return this._Staff_First_Name;
            }
            set
            {
                if ((this._Staff_First_Name != value))
                {
                    this._Staff_First_Name = value;
                }
            }
        }


        ///defaault starts here 
            private System.Nullable<int> TermsgroupDefaults_Access_Key_ID;

            private string TermsgroupDefaults_Access_Key_Name;



            public System.Nullable<int> TermsgroupDefaults_Access_Key_IDAccess_Key_ID
            {
                get
                {
                    return this.TermsgroupDefaults_Access_Key_ID;
                }
                set
                {
                    if ((this.TermsgroupDefaults_Access_Key_ID != value))
                    {
                        this.TermsgroupDefaults_Access_Key_ID = value;
                    }
                }
            }
                    
            public string TermsgroupDefaultAccess_Key_Name
            {
                get
                {
                    return this.TermsgroupDefaults_Access_Key_Name;
                }
                set
                {
                    if ((this.TermsgroupDefaults_Access_Key_Name != value))
                    {
                        this.TermsgroupDefaults_Access_Key_Name = value;
                    }
                }
            }
        /// <summary>
        /// ////////
        /// </summary>

            private System.Nullable<int> AcessKeyDefaultResult_Access_Key_ID;

            private string AcessKeyDefaultResult_Access_Key_Name;
                  

            
            public System.Nullable<int> AcessKeyDefaultResultAccess_Key_ID
            {
                get
                {
                    return this.AcessKeyDefaultResult_Access_Key_ID;
                }
                set
                {
                    if ((this.AcessKeyDefaultResult_Access_Key_ID != value))
                    {
                        this.AcessKeyDefaultResult_Access_Key_ID = value;
                    }
                }
            }

            
            public string AcessKeyDefaultResultAccess_Key_Name
            {
                get
                {
                    return this.AcessKeyDefaultResult_Access_Key_Name;
                }
                set
                {
                    if ((this.AcessKeyDefaultResult_Access_Key_Name != value))
                    {
                        this.AcessKeyDefaultResult_Access_Key_Name = value;
                    }
                }
            }

        /// <summary>
        /// ///////
        /// </summary>
            private System.Nullable<int> RepresentitiveDefaultResult_Staff_ID;

            private string RepresentitiveDefaultResult_Staff_Last_Name;

            private string RepresentitiveDefaultResult_Staff_First_Name;
                

            
            public System.Nullable<int> RepresentitiveDefaultResultStaff_ID
            {
                get
                {
                    return this.RepresentitiveDefaultResult_Staff_ID;
                }
                set
                {
                    if ((this.RepresentitiveDefaultResult_Staff_ID != value))
                    {
                        this.RepresentitiveDefaultResult_Staff_ID = value;
                    }
                }
            }

            
            public string RepresentitiveDefaultResultStaff_Last_Name
            {
                get
                {
                    return this.RepresentitiveDefaultResult_Staff_Last_Name;
                }
                set
                {
                    if ((this.RepresentitiveDefaultResult_Staff_Last_Name != value))
                    {
                        this.RepresentitiveDefaultResult_Staff_Last_Name = value;
                    }
                }
            }

            
            public string RepresentitiveDefaultResultStaff_First_Name
            {
                get
                {
                    return this.RepresentitiveDefaultResult_Staff_First_Name;
                }
                set
                {
                    if ((this.RepresentitiveDefaultResult_Staff_First_Name != value))
                    {
                        this.RepresentitiveDefaultResult_Staff_First_Name = value;
                    }
                }
            }
           






        ///default end here



        //subcompany send value end//
        
              
       
        public string company_name
        {

            get
            {
                return _Company_Name;
            }

            set
            {
                _Company_Name = value;

            }
        }
        public string company_subcompany
        {
            get
            {
                return _company_subcompany;
            }

            set
            {
                _company_subcompany = value;

            }
        }
     
        
        private string _Site_Closed_Date;

        /*************************Properties to Apply *******************/

        //private bool _IsCrossTicketingChecked;

        public AdminSiteEntity()
        {
        }
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
                    this._Company_ID = value;
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
        public bool bSiteDetailsUpdated
        {
            get;
            set;
        }
        public System.Nullable<int> Supplier_ID
        {
            get
            {
                return this._Supplier_ID;
            }
            set
            {
                if ((this._Supplier_ID != value))
                {
                    this._Supplier_ID = value;
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

        public int Site_Status_ID
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

        public int IsTITOEnabled
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

        public int IsCrossTicketingEnabled
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

        public int IsNonCashVoucherEnabled
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

        public System.Nullable<int> Bar_Position_ID
        {
            get
            {
                return this._Bar_Position_ID;
            }
            set
            {
                if ((this._Bar_Position_ID != value))
                {
                    this._Bar_Position_ID = value;
                }
            }
        }

        public string Operator_Name
        {
            get
            {
                return this._Operator_Name;
            }
            set
            {
                if ((this._Operator_Name != value))
                {
                    this._Operator_Name = value;
                }
            }
        }
        public int Operator_ID
        {
            get
            {
                return this._Operator_ID;
            }
            set
            {
                if ((this._Operator_ID != value))
                {
                    this._Operator_ID = value;
                }
            }
        }

        public string Depot_Name
        {
            get
            {
                return this._Depot_Name;
            }
            set
            {
                if ((this._Depot_Name != value))
                {
                    this._Depot_Name = value;
                }
            }
        }

        public string Site_Classification_Name
        {
            get
            {
                return this._Site_Classification_Name;
            }
            set
            {
                if ((this._Site_Classification_Name != value))
                {
                    this._Site_Classification_Name = value;
                }
            }
        }

        public string Standard_Opening_Hours_Description
        {
            get
            {
                return this._Standard_Opening_Hours_Description;
            }
            set
            {
                if ((this._Standard_Opening_Hours_Description != value))
                {
                    this._Standard_Opening_Hours_Description = value;
                }
            }
        }

        public string Service_Area_Name
        {
            get
            {
                return this._Service_Area_Name;
            }
            set
            {
                if ((this._Service_Area_Name != value))
                {
                    this._Service_Area_Name = value;
                }
            }
        }
        

        /********************** For Apply properties***********************/

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
        public bool IsCrossTicketingChecked
        {
            get;
            set;
        }
        public bool IsSiteClosed
        {
            get;
            set;
        }
    }
 
	//public partial class SiteStatus
    //{
    //    private bool _Site_Enabled;
		
    //    private int _IsTITOEnabled;
		
    //    private int _IsCrossTicketingEnabled;
		
    //    private int _IsNonCashVoucherEnabled;
		
    //    public SiteStatus()
    //    {
    //    }
				
    //    public bool Site_Enabled
    //    {
    //        get
    //        {
    //            return this._Site_Enabled;
    //        }
    //        set
    //        {
    //            if ((this._Site_Enabled != value))
    //            {
    //                this._Site_Enabled = value;
    //            }
    //        }
    //    }
				
    //    public int IsTITOEnabled
    //    {
    //        get
    //        {
    //            return this._IsTITOEnabled;
    //        }
    //        set
    //        {
    //            if ((this._IsTITOEnabled != value))
    //            {
    //                this._IsTITOEnabled = value;
    //            }
    //        }
    //    }
				
    //    public int IsCrossTicketingEnabled
    //    {
    //        get
    //        {
    //            return this._IsCrossTicketingEnabled;
    //        }
    //        set
    //        {
    //            if ((this._IsCrossTicketingEnabled != value))
    //            {
    //                this._IsCrossTicketingEnabled = value;
    //            }
    //        }
    //    }
				
    //    public int IsNonCashVoucherEnabled
    //    {
    //        get
    //        {
    //            return this._IsNonCashVoucherEnabled;
    //        }
    //        set
    //        {
    //            if ((this._IsNonCashVoucherEnabled != value))
    //            {
    //                this._IsNonCashVoucherEnabled = value;
    //            }
    //        }
    //    }       
    //}
    //public partial class SiteTicketingURL
    //{
    //    private string _TicketingURL;

    //    public SiteTicketingURL()
    //    {
    //    }
				
    //    public string TicketingURL
    //    {
    //        get
    //        {
    //            return this._TicketingURL;
    //        }
    //        set
    //        {
    //            if ((this._TicketingURL != value))
    //            {
    //                this._TicketingURL = value;
    //            }
    //        }
    //    }
    //}
    //public partial class ActiveInstallationsForSite
    //{
    //    private System.Nullable<int> _Bar_Position_ID;

    //    public ActiveInstallationsForSite()
    //    {
    //    }

    //    public System.Nullable<int> Bar_Position_ID
    //    {
    //        get
    //        {
    //            return this._Bar_Position_ID;
    //        }
    //        set
    //        {
    //            if ((this._Bar_Position_ID != value))
    //            {
    //                this._Bar_Position_ID = value;
    //            }
    //        }
    //    }
    //}
    //public partial class CheckSiteAvailablity
    //{
    //    private int _Site_ID;

    //    public CheckSiteAvailablity()
    //    {
    //    }

    //    public int Site_ID
    //    {
    //        get
    //        {
    //            return this._Site_ID;
    //        }
    //        set
    //        {
    //            if ((this._Site_ID != value))
    //            {
    //                this._Site_ID = value;
    //            }
    //        }
    //    }
    //}

    //public partial class GetOperatorInfoResult
    //{

    //    private string _Operator_Name;

    //    private int _Operator_ID;

    //    public GetOperatorInfoResult()
    //    {
    //    }

    //    public string Operator_Name
    //    {
    //        get
    //        {
    //            return this._Operator_Name;
    //        }
    //        set
    //        {
    //            if ((this._Operator_Name != value))
    //            {
    //                this._Operator_Name = value;
    //            }
    //        }
    //    }
    //    public int Operator_ID
    //    {
    //        get
    //        {
    //            return this._Operator_ID;
    //        }
    //        set
    //        {
    //            if ((this._Operator_ID != value))
    //            {
    //                this._Operator_ID = value;
    //            }
    //        }
    //    }
    //}

    //public partial class GetDepotInfo
    //{

    //    private string _Depot_Name;

    //    private int _Depot_ID;

    //    public GetDepotInfo()
    //    {
    //    }
                
    //    public string Depot_Name
    //    {
    //        get
    //        {
    //            return this._Depot_Name;
    //        }
    //        set
    //        {
    //            if ((this._Depot_Name != value))
    //            {
    //                this._Depot_Name = value;
    //            }
    //        }
    //    }
                
    //    public int Depot_ID
    //    {
    //        get
    //        {
    //            return this._Depot_ID;
    //        }
    //        set
    //        {
    //            if ((this._Depot_ID != value))
    //            {
    //                this._Depot_ID = value;
    //            }
    //        }
    //    }
    //}

    //public partial class GetSiteClassification
    //{

    //    private int _Site_Classification_ID;

    //    private string _Site_Classification_Name;

    //    public GetSiteClassification()
    //    {
    //    }

    //    public int Site_Classification_ID
    //    {
    //        get
    //        {
    //            return this._Site_Classification_ID;
    //        }
    //        set
    //        {
    //            if ((this._Site_Classification_ID != value))
    //            {
    //                this._Site_Classification_ID = value;
    //            }
    //        }
    //    }
    //    public string Site_Classification_Name
    //    {
    //        get
    //        {
    //            return this._Site_Classification_Name;
    //        }
    //        set
    //        {
    //            if ((this._Site_Classification_Name != value))
    //            {
    //                this._Site_Classification_Name = value;
    //            }
    //        }
    //    }
    //}

    //public partial class GetSiteClassifNameonID
    //{

    //    private string _Site_Classification_Name;

    //    public GetSiteClassifNameonID()
    //    {
    //    }

    //    public string Site_Classification_Name
    //    {
    //        get
    //        {
    //            return this._Site_Classification_Name;
    //        }
    //        set
    //        {
    //            if ((this._Site_Classification_Name != value))
    //            {
    //                this._Site_Classification_Name = value;
    //            }
    //        }
    //    }
    //}

    //public partial class ServiceOperatorResult
    //{

    //    private string _Operator_Name;

    //    private int _Operator_ID;

    //    public ServiceOperatorResult()
    //    {
    //    }
                
    //    public string Operator_Name
    //    {
    //        get
    //        {
    //            return this._Operator_Name;
    //        }
    //        set
    //        {
    //            if ((this._Operator_Name != value))
    //            {
    //                this._Operator_Name = value;
    //            }
    //        }
    //    }
                
    //    public int Operator_ID
    //    {
    //        get
    //        {
    //            return this._Operator_ID;
    //        }
    //        set
    //        {
    //            if ((this._Operator_ID != value))
    //            {
    //                this._Operator_ID = value;
    //            }
    //        }
    //    }
    //}

    //public partial class GetStdOpeningHrsResult
    //{

    //    private int _Standard_Opening_Hours_ID;

    //    private string _Standard_Opening_Hours_Description;

    //    public GetStdOpeningHrsResult()
    //    {
    //    }
                
    //    public int Standard_Opening_Hours_ID
    //    {
    //        get
    //        {
    //            return this._Standard_Opening_Hours_ID;
    //        }
    //        set
    //        {
    //            if ((this._Standard_Opening_Hours_ID != value))
    //            {
    //                this._Standard_Opening_Hours_ID = value;
    //            }
    //        }
    //    }
                
    //    public string Standard_Opening_Hours_Description
    //    {
    //        get
    //        {
    //            return this._Standard_Opening_Hours_Description;
    //        }
    //        set
    //        {
    //            if ((this._Standard_Opening_Hours_Description != value))
    //            {
    //                this._Standard_Opening_Hours_Description = value;
    //            }
    //        }
    //    }
    //}

    //public partial class SiteCommonEntity
    //{
    //    private bool _IsNumerical;
    //    public SiteCommonEntity()
    //    {
    //    }
    //    public bool IsNumerical
    //    {
    //        get
    //        {
    //            return this._IsNumerical;
    //        }
    //        set
    //        {
    //            this._IsNumerical = value;
    //        }
    //    }
    //}



    public partial class AdminSiteClassEntity
    {
        public int SiteClassID { get; set; }
        public string SiteClassName { get; set; }
    }

    public partial class SiteOpeningHours
    {
        private string _Site_Name;
		
		private string _Site_Open_Monday;
		
		private string _Site_Open_Tuesday;
		
		private string _Site_Open_Wednesday;
		
		private string _Site_Open_Thursday;
		
		private string _Site_Open_Friday;
		
		private string _Site_Open_Saturday;
		
		private string _Site_Open_Sunday;

        private string _TotalHours;        

        private string _Zone_Open_Monday;

        private string _Zone_Open_Tuesday;

        private string _Zone_Open_Wednesday;

        private string _Zone_Open_Thursday;

        private string _Zone_Open_Friday;

        private string _Zone_Open_Saturday;

        private string _Zone_Open_Sunday;

        public SiteOpeningHours()
		{
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

        public string TotalHours
        {
            get
            {
                return this._TotalHours;
            }
            set
            {
                if ((this._TotalHours != value))
                {
                    this._TotalHours = value;
                }
            }
        }
        
        public string Zone_Open_Monday
        {
            get
            {
                return this._Zone_Open_Monday;
            }
            set
            {
                if ((this._Zone_Open_Monday != value))
                {
                    this._Zone_Open_Monday = value;
                }
            }
        }

        public string Zone_Open_Tuesday
        {
            get
            {
                return this._Zone_Open_Tuesday;
            }
            set
            {
                if ((this._Zone_Open_Tuesday != value))
                {
                    this._Zone_Open_Tuesday = value;
                }
            }
        }

        public string Zone_Open_Wednesday
        {
            get
            {
                return this._Zone_Open_Wednesday;
            }
            set
            {
                if ((this._Zone_Open_Wednesday != value))
                {
                    this._Zone_Open_Wednesday = value;
                }
            }
        }

        public string Zone_Open_Thursday
        {
            get
            {
                return this._Zone_Open_Thursday;
            }
            set
            {
                if ((this._Zone_Open_Thursday != value))
                {
                    this._Zone_Open_Thursday = value;
                }
            }
        }

        public string Zone_Open_Friday
        {
            get
            {
                return this._Zone_Open_Friday;
            }
            set
            {
                if ((this._Zone_Open_Friday != value))
                {
                    this._Zone_Open_Friday = value;
                }
            }
        }

        public string Zone_Open_Saturday
        {
            get
            {
                return this._Zone_Open_Saturday;
            }
            set
            {
                if ((this._Zone_Open_Saturday != value))
                {
                    this._Zone_Open_Saturday = value;
                }
            }
        }

        public string Zone_Open_Sunday
        {
            get
            {
                return this._Zone_Open_Sunday;
            }
            set
            {
                if ((this._Zone_Open_Sunday != value))
                {
                    this._Zone_Open_Sunday = value;
                }
            }
        }
    }

    public partial class GetStdOpeningHrsDetailsResult
    {

        private string _Standard_Opening_Hours_Description;

        private string _Standard_Opening_Hours_Open_Monday;

        private string _Standard_Opening_Hours_Open_Tuesday;

        private string _Standard_Opening_Hours_Open_Wednesday;

        private string _Standard_Opening_Hours_Open_Thursday;

        private string _Standard_Opening_Hours_Open_Friday;

        private string _Standard_Opening_Hours_Open_Saturday;

        private string _Standard_Opening_Hours_Open_Sunday;

        private int _Standard_Opening_Hours_Total;

        public GetStdOpeningHrsDetailsResult()
        {
        }       
        
        public string Standard_Opening_Hours_Description
        {
            get
            {
                return this._Standard_Opening_Hours_Description;
            }
            set
            {
                if ((this._Standard_Opening_Hours_Description != value))
                {
                    this._Standard_Opening_Hours_Description = value;
                }
            }
        }

        public string Standard_Opening_Hours_Open_Monday
        {
            get
            {
                return this._Standard_Opening_Hours_Open_Monday;
            }
            set
            {
                if ((this._Standard_Opening_Hours_Open_Monday != value))
                {
                    this._Standard_Opening_Hours_Open_Monday = value;
                }
            }
        }

        public string Standard_Opening_Hours_Open_Tuesday
        {
            get
            {
                return this._Standard_Opening_Hours_Open_Tuesday;
            }
            set
            {
                if ((this._Standard_Opening_Hours_Open_Tuesday != value))
                {
                    this._Standard_Opening_Hours_Open_Tuesday = value;
                }
            }
        }

        public string Standard_Opening_Hours_Open_Wednesday
        {
            get
            {
                return this._Standard_Opening_Hours_Open_Wednesday;
            }
            set
            {
                if ((this._Standard_Opening_Hours_Open_Wednesday != value))
                {
                    this._Standard_Opening_Hours_Open_Wednesday = value;
                }
            }
        }

        public string Standard_Opening_Hours_Open_Thursday
        {
            get
            {
                return this._Standard_Opening_Hours_Open_Thursday;
            }
            set
            {
                if ((this._Standard_Opening_Hours_Open_Thursday != value))
                {
                    this._Standard_Opening_Hours_Open_Thursday = value;
                }
            }
        }

        public string Standard_Opening_Hours_Open_Friday
        {
            get
            {
                return this._Standard_Opening_Hours_Open_Friday;
            }
            set
            {
                if ((this._Standard_Opening_Hours_Open_Friday != value))
                {
                    this._Standard_Opening_Hours_Open_Friday = value;
                }
            }
        }

        public string Standard_Opening_Hours_Open_Saturday
        {
            get
            {
                return this._Standard_Opening_Hours_Open_Saturday;
            }
            set
            {
                if ((this._Standard_Opening_Hours_Open_Saturday != value))
                {
                    this._Standard_Opening_Hours_Open_Saturday = value;
                }
            }
        }

        public string Standard_Opening_Hours_Open_Sunday
        {
            get
            {
                return this._Standard_Opening_Hours_Open_Sunday;
            }
            set
            {
                if ((this._Standard_Opening_Hours_Open_Sunday != value))
                {
                    this._Standard_Opening_Hours_Open_Sunday = value;
                }
            }
        }

        public int Standard_Opening_Hours_Total
        {
            get
            {
                return this._Standard_Opening_Hours_Total;
            }
            set
            {
                if ((this._Standard_Opening_Hours_Total != value))
                {
                    this._Standard_Opening_Hours_Total = value;
                }
            }
        }
    }

    public partial class GetCustomerAccessViewAllDepotsResult
    {

        private System.Nullable<bool> _Customer_Access_View_All_Depots;

        public GetCustomerAccessViewAllDepotsResult()
        {
        }

        public System.Nullable<bool> Customer_Access_View_All_Depots
        {
            get
            {
                return this._Customer_Access_View_All_Depots;
            }
            set
            {
                if ((this._Customer_Access_View_All_Depots != value))
                {
                    this._Customer_Access_View_All_Depots = value;
                }
            }
        }
    }

    public partial class GetCustomerAccessViewAllCompaniesResult
    {

        private System.Nullable<bool> _Customer_Access_View_All_Companies;

        public GetCustomerAccessViewAllCompaniesResult()
        {
        }
                
        public System.Nullable<bool> Customer_Access_View_All_Companies
        {
            get
            {
                return this._Customer_Access_View_All_Companies;
            }
            set
            {
                if ((this._Customer_Access_View_All_Companies != value))
                {
                    this._Customer_Access_View_All_Companies = value;
                }
            }
        }
    }

    public partial class GetZoneOpeningHoursResult
    {

        private string _Zone_Name;

        private string _Zone_Open_Monday;

        private string _Zone_Open_Tuesday;

        private string _Zone_Open_Wednesday;

        private string _Zone_Open_Thursday;

        private string _Zone_Open_Friday;

        private string _Zone_Open_Saturday;

        private string _Zone_Open_Sunday;

        public GetZoneOpeningHoursResult()
        {
        }

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
                    this._Zone_Name = value;
                }
            }
        }

        public string Zone_Open_Monday
        {
            get
            {
                return this._Zone_Open_Monday;
            }
            set
            {
                if ((this._Zone_Open_Monday != value))
                {
                    this._Zone_Open_Monday = value;
                }
            }
        }

        public string Zone_Open_Tuesday
        {
            get
            {
                return this._Zone_Open_Tuesday;
            }
            set
            {
                if ((this._Zone_Open_Tuesday != value))
                {
                    this._Zone_Open_Tuesday = value;
                }
            }
        }

        public string Zone_Open_Wednesday
        {
            get
            {
                return this._Zone_Open_Wednesday;
            }
            set
            {
                if ((this._Zone_Open_Wednesday != value))
                {
                    this._Zone_Open_Wednesday = value;
                }
            }
        }

        public string Zone_Open_Thursday
        {
            get
            {
                return this._Zone_Open_Thursday;
            }
            set
            {
                if ((this._Zone_Open_Thursday != value))
                {
                    this._Zone_Open_Thursday = value;
                }
            }
        }

        public string Zone_Open_Friday
        {
            get
            {
                return this._Zone_Open_Friday;
            }
            set
            {
                if ((this._Zone_Open_Friday != value))
                {
                    this._Zone_Open_Friday = value;
                }
            }
        }

        public string Zone_Open_Saturday
        {
            get
            {
                return this._Zone_Open_Saturday;
            }
            set
            {
                if ((this._Zone_Open_Saturday != value))
                {
                    this._Zone_Open_Saturday = value;
                }
            }
        }

        public string Zone_Open_Sunday
        {
            get
            {
                return this._Zone_Open_Sunday;
            }
            set
            {
                if ((this._Zone_Open_Sunday != value))
                {
                    this._Zone_Open_Sunday = value;
                }
            }
        }
    }

    public partial class GetRepresentativeonSiteResult
    {

        private string _Staff_Last_Name;

        private string _Staff_First_Name;

        private int _Staff_ID;

        public string Staff_Full_Name
        { get; set; }

        public GetRepresentativeonSiteResult()
        {
}
               
        public string Staff_Last_Name
        {
            get
            {
                return this._Staff_Last_Name;
            }
            set
            {
                if ((this._Staff_Last_Name != value))
                {
                    this._Staff_Last_Name = value;
                }
            }
        }
                
        public string Staff_First_Name
        {
            get
            {
                return this._Staff_First_Name;
            }
            set
            {
                if ((this._Staff_First_Name != value))
                {
                    this._Staff_First_Name = value;
                }
            }
        }
                
        public int Staff_ID
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
                
    }

    public partial class SiteKeyExists
    {

        private System.Nullable<bool> _IsExchangeKeyAvailable;

        public SiteKeyExists()
        {
        }

        public System.Nullable<bool> IsExchangeKeyAvailable
        {
            get
            {
                return this._IsExchangeKeyAvailable;
            }
            set
            {
                if ((this._IsExchangeKeyAvailable != value))
                {
                    this._IsExchangeKeyAvailable = value;
                }
            }
        }
    }
}
