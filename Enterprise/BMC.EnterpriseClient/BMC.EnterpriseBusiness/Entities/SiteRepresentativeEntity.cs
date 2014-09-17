using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.EnterpriseBusiness.Entities
{
    public class SiteRepresentativeEntity    
    {
        private int _Staff_ID;

        private System.Nullable<int> _Operator_ID;

        private System.Nullable<int> _Computer_Build_ID;

        private System.Nullable<int> _User_Group_ID;

        private string _Staff_First_Name;

        private string _Staff_Last_Name;

        private string _Staff_Title;

        private string _Staff_Address;

        private string _Staff_Postcode;

        private string _Staff_Phone_No;

        private string _Staff_Extension_No;

        private string _Staff_Mobile_No;

        private string _Staff_Job_Title;

        private string _Staff_Department;

        private System.Nullable<bool> _Staff_IsACollector;

        private System.Nullable<bool> _Staff_IsAnEngineer;

        private System.Nullable<bool> _Staff_IsARepresentative;

        private System.Nullable<bool> _Staff_IsAStockController;

        private string _Staff_Start_Date;

        private string _Staff_End_Date;

        private string _Staff_Modem_Phone_No;

        private string _Staff_Remote_Inbox;

        private string _Staff_Remote_Outbox;

        private string _Staff_Username;

        private string _Staff_Password;

        private System.Nullable<int> _Staff_Analysis_D_R_1_Back_Neg;

        private System.Nullable<int> _Staff_Analysis_D_R_1_Front_Neg;

        private System.Nullable<int> _Staff_Analysis_D_R_1_Back_Pos;

        private System.Nullable<int> _Staff_Analysis_D_R_1_Front_Pos;

        private System.Nullable<int> _Staff_Analysis_D_R_2_Back_Neg;

        private System.Nullable<int> _Staff_Analysis_D_R_2_Front_Neg;

        private System.Nullable<int> _Staff_Analysis_D_R_2_Back_Pos;

        private System.Nullable<int> _Staff_Analysis_D_R_2_Front_Pos;

        private System.Nullable<int> _Staff_Analysis_D_R_3_Back_Neg;

        private System.Nullable<int> _Staff_Analysis_D_R_3_Front_Neg;

        private System.Nullable<int> _Staff_Analysis_D_R_3_Back_Pos;

        private System.Nullable<int> _Staff_Analysis_D_R_3_Front_Pos;

        private System.Nullable<int> _Staff_Analysis_D_R_4_Back_Neg;

        private System.Nullable<int> _Staff_Analysis_D_R_4_Front_Neg;

        private System.Nullable<int> _Staff_Analysis_D_R_4_Back_Pos;

        private System.Nullable<int> _Staff_Analysis_D_R_4_Front_Pos;

        private System.Nullable<int> _Staff_Analysis_D_R_5_Back_Neg;

        private System.Nullable<int> _Staff_Analysis_D_R_5_Front_Neg;

        private System.Nullable<int> _Staff_Analysis_D_R_5_Back_Pos;

        private System.Nullable<int> _Staff_Analysis_D_R_5_Front_Pos;

        private System.Nullable<int> _Staff_Tree_Checked_Back;

        private System.Nullable<int> _Staff_Tree_Checked_Front;

        private System.Nullable<int> _Staff_Tree_UnChecked_Back;

        private System.Nullable<int> _Staff_Tree_UnChecked_Front;

        private System.Nullable<int> _Staff_Tree_Mixed_Back;

        private System.Nullable<int> _Staff_Tree_Mixed_Front;

        private string _Staff_MAN_Number;

        private System.Nullable<int> _Depot_ID;

        private string _Staff_GPS_Location;

        private System.Nullable<int> _Service_Area_ID;

        private System.Nullable<int> _Supplier_ID;

        private string _Staff_Personel_No;

        private System.Nullable<bool> _Staff_Terminated;

        private string _Staff_Collector_Current_Version;

        private string _Staff_Collector_Uploaded_Version;

        private string _Staff_Engineer_Current_Version;

        private string _Staff_Engineer_Uploaded_Version;

        private string _Staff_Notes;

        private string _Staff_Last_Comms_Test_Sent;

        private string _Staff_Last_Comms_Test_Received;

        private string _Email_Address;

        private System.Nullable<int> _UserTableID;

        private int _SecurityUserID;

        private string _WindowsUserName;

        private string _UserName;

        private string _Password;

        private System.Nullable<int> _LanguageID;

        private System.Nullable<int> _CurrencyCulture;

        private System.Nullable<int> _DateCulture;

        private System.Nullable<bool> _ChangePassword;

        private System.Nullable<System.DateTime> _CreatedDate;

        private System.Nullable<System.DateTime> _PasswordChangeDate;

        private System.Nullable<bool> _isReset;

        private bool _isLocked;
        
        public SiteRepresentativeEntity()
        {
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
        
        public System.Nullable<int> Operator_ID
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
        
        public System.Nullable<int> User_Group_ID
        {
            get
            {
                return this._User_Group_ID;
            }
            set
            {
                if ((this._User_Group_ID != value))
                {
                    this._User_Group_ID = value;
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
        
        public string Staff_Title
        {
            get
            {
                return this._Staff_Title;
            }
            set
            {
                if ((this._Staff_Title != value))
                {
                    this._Staff_Title = value;
                }
            }
        }
        
        public string Staff_Address
        {
            get
            {
                return this._Staff_Address;
            }
            set
            {
                if ((this._Staff_Address != value))
                {
                    this._Staff_Address = value;
                }
            }
        }
        
        public string Staff_Postcode
        {
            get
            {
                return this._Staff_Postcode;
            }
            set
            {
                if ((this._Staff_Postcode != value))
                {
                    this._Staff_Postcode = value;
                }
            }
        }
        
        public string Staff_Phone_No
        {
            get
            {
                return this._Staff_Phone_No;
            }
            set
            {
                if ((this._Staff_Phone_No != value))
                {
                    this._Staff_Phone_No = value;
                }
            }
        }
        
        public string Staff_Extension_No
        {
            get
            {
                return this._Staff_Extension_No;
            }
            set
            {
                if ((this._Staff_Extension_No != value))
                {
                    this._Staff_Extension_No = value;
                }
            }
        }
        
        public string Staff_Mobile_No
        {
            get
            {
                return this._Staff_Mobile_No;
            }
            set
            {
                if ((this._Staff_Mobile_No != value))
                {
                    this._Staff_Mobile_No = value;
                }
            }
        }
        
        public string Staff_Job_Title
        {
            get
            {
                return this._Staff_Job_Title;
            }
            set
            {
                if ((this._Staff_Job_Title != value))
                {
                    this._Staff_Job_Title = value;
                }
            }
        }
        
        public string Staff_Department
        {
            get
            {
                return this._Staff_Department;
            }
            set
            {
                if ((this._Staff_Department != value))
                {
                    this._Staff_Department = value;
                }
            }
        }
        
        public System.Nullable<bool> Staff_IsACollector
        {
            get
            {
                return this._Staff_IsACollector;
            }
            set
            {
                if ((this._Staff_IsACollector != value))
                {
                    this._Staff_IsACollector = value;
                }
            }
        }
        
        public System.Nullable<bool> Staff_IsAnEngineer
        {
            get
            {
                return this._Staff_IsAnEngineer;
            }
            set
            {
                if ((this._Staff_IsAnEngineer != value))
                {
                    this._Staff_IsAnEngineer = value;
                }
            }
        }
        
        public System.Nullable<bool> Staff_IsARepresentative
        {
            get
            {
                return this._Staff_IsARepresentative;
            }
            set
            {
                if ((this._Staff_IsARepresentative != value))
                {
                    this._Staff_IsARepresentative = value;
                }
            }
        }

        public System.Nullable<bool> Staff_IsAStockController
        {
            get
            {
                return this._Staff_IsAStockController;
            }
            set
            {
                if ((this._Staff_IsAStockController != value))
                {
                    this._Staff_IsAStockController = value;
                }
            }
        }

        public string Staff_Start_Date
        {
            get
            {
                return this._Staff_Start_Date;
            }
            set
            {
                if ((this._Staff_Start_Date != value))
                {
                    this._Staff_Start_Date = value;
                }
            }
        }

        public string Staff_End_Date
        {
            get
            {
                return this._Staff_End_Date;
            }
            set
            {
                if ((this._Staff_End_Date != value))
                {
                    this._Staff_End_Date = value;
                }
            }
        }

        public string Staff_Modem_Phone_No
        {
            get
            {
                return this._Staff_Modem_Phone_No;
            }
            set
            {
                if ((this._Staff_Modem_Phone_No != value))
                {
                    this._Staff_Modem_Phone_No = value;
                }
            }
        }

        public string Staff_Remote_Inbox
        {
            get
            {
                return this._Staff_Remote_Inbox;
            }
            set
            {
                if ((this._Staff_Remote_Inbox != value))
                {
                    this._Staff_Remote_Inbox = value;
                }
            }
        }

        public string Staff_Remote_Outbox
        {
            get
            {
                return this._Staff_Remote_Outbox;
            }
            set
            {
                if ((this._Staff_Remote_Outbox != value))
                {
                    this._Staff_Remote_Outbox = value;
                }
            }
        }

        public string Staff_Username
        {
            get
            {
                return this._Staff_Username;
            }
            set
            {
                if ((this._Staff_Username != value))
                {
                    this._Staff_Username = value;
                }
            }
        }

        public string Staff_Password
        {
            get
            {
                return this._Staff_Password;
            }
            set
            {
                if ((this._Staff_Password != value))
                {
                    this._Staff_Password = value;
                }
            }
        }
        
        public System.Nullable<int> Staff_Analysis_D_R_1_Back_Neg
        {
            get
            {
                return this._Staff_Analysis_D_R_1_Back_Neg;
            }
            set
            {
                if ((this._Staff_Analysis_D_R_1_Back_Neg != value))
                {
                    this._Staff_Analysis_D_R_1_Back_Neg = value;
                }
            }
        }
                
        public System.Nullable<int> Staff_Analysis_D_R_1_Front_Neg
        {
            get
            {
                return this._Staff_Analysis_D_R_1_Front_Neg;
            }
            set
            {
                if ((this._Staff_Analysis_D_R_1_Front_Neg != value))
                {
                    this._Staff_Analysis_D_R_1_Front_Neg = value;
                }
            }
        }
               
        public System.Nullable<int> Staff_Analysis_D_R_1_Back_Pos
        {
            get
            {
                return this._Staff_Analysis_D_R_1_Back_Pos;
            }
            set
            {
                if ((this._Staff_Analysis_D_R_1_Back_Pos != value))
                {
                    this._Staff_Analysis_D_R_1_Back_Pos = value;
                }
            }
        }
                
        public System.Nullable<int> Staff_Analysis_D_R_1_Front_Pos
        {
            get
            {
                return this._Staff_Analysis_D_R_1_Front_Pos;
            }
            set
            {
                if ((this._Staff_Analysis_D_R_1_Front_Pos != value))
                {
                    this._Staff_Analysis_D_R_1_Front_Pos = value;
                }
            }
        }
        
        public System.Nullable<int> Staff_Analysis_D_R_2_Back_Neg
        {
            get
            {
                return this._Staff_Analysis_D_R_2_Back_Neg;
            }
            set
            {
                if ((this._Staff_Analysis_D_R_2_Back_Neg != value))
                {
                    this._Staff_Analysis_D_R_2_Back_Neg = value;
                }
            }
        }
        
        public System.Nullable<int> Staff_Analysis_D_R_2_Front_Neg
        {
            get
            {
                return this._Staff_Analysis_D_R_2_Front_Neg;
            }
            set
            {
                if ((this._Staff_Analysis_D_R_2_Front_Neg != value))
                {
                    this._Staff_Analysis_D_R_2_Front_Neg = value;
                }
            }
        }
        
        public System.Nullable<int> Staff_Analysis_D_R_2_Back_Pos
        {
            get
            {
                return this._Staff_Analysis_D_R_2_Back_Pos;
            }
            set
            {
                if ((this._Staff_Analysis_D_R_2_Back_Pos != value))
                {
                    this._Staff_Analysis_D_R_2_Back_Pos = value;
                }
            }
        }
                
        public System.Nullable<int> Staff_Analysis_D_R_2_Front_Pos
        {
            get
            {
                return this._Staff_Analysis_D_R_2_Front_Pos;
            }
            set
            {
                if ((this._Staff_Analysis_D_R_2_Front_Pos != value))
                {
                    this._Staff_Analysis_D_R_2_Front_Pos = value;
                }
            }
        }
        
        public System.Nullable<int> Staff_Analysis_D_R_3_Back_Neg
        {
            get
            {
                return this._Staff_Analysis_D_R_3_Back_Neg;
            }
            set
            {
                if ((this._Staff_Analysis_D_R_3_Back_Neg != value))
                {
                    this._Staff_Analysis_D_R_3_Back_Neg = value;
                }
            }
        }
        
        public System.Nullable<int> Staff_Analysis_D_R_3_Front_Neg
        {
            get
            {
                return this._Staff_Analysis_D_R_3_Front_Neg;
            }
            set
            {
                if ((this._Staff_Analysis_D_R_3_Front_Neg != value))
                {
                    this._Staff_Analysis_D_R_3_Front_Neg = value;
                }
            }
        }
        
        public System.Nullable<int> Staff_Analysis_D_R_3_Back_Pos
        {
            get
            {
                return this._Staff_Analysis_D_R_3_Back_Pos;
            }
            set
            {
                if ((this._Staff_Analysis_D_R_3_Back_Pos != value))
                {
                    this._Staff_Analysis_D_R_3_Back_Pos = value;
                }
            }
        }
        
        public System.Nullable<int> Staff_Analysis_D_R_3_Front_Pos
        {
            get
            {
                return this._Staff_Analysis_D_R_3_Front_Pos;
            }
            set
            {
                if ((this._Staff_Analysis_D_R_3_Front_Pos != value))
                {
                    this._Staff_Analysis_D_R_3_Front_Pos = value;
                }
            }
        }
        
        public System.Nullable<int> Staff_Analysis_D_R_4_Back_Neg
        {
            get
            {
                return this._Staff_Analysis_D_R_4_Back_Neg;
            }
            set
            {
                if ((this._Staff_Analysis_D_R_4_Back_Neg != value))
                {
                    this._Staff_Analysis_D_R_4_Back_Neg = value;
                }
            }
        }
        
        public System.Nullable<int> Staff_Analysis_D_R_4_Front_Neg
        {
            get
            {
                return this._Staff_Analysis_D_R_4_Front_Neg;
            }
            set
            {
                if ((this._Staff_Analysis_D_R_4_Front_Neg != value))
                {
                    this._Staff_Analysis_D_R_4_Front_Neg = value;
                }
            }
        }
        
        public System.Nullable<int> Staff_Analysis_D_R_4_Back_Pos
        {
            get
            {
                return this._Staff_Analysis_D_R_4_Back_Pos;
            }
            set
            {
                if ((this._Staff_Analysis_D_R_4_Back_Pos != value))
                {
                    this._Staff_Analysis_D_R_4_Back_Pos = value;
                }
            }
        }
        
        public System.Nullable<int> Staff_Analysis_D_R_4_Front_Pos
        {
            get
            {
                return this._Staff_Analysis_D_R_4_Front_Pos;
            }
            set
            {
                if ((this._Staff_Analysis_D_R_4_Front_Pos != value))
                {
                    this._Staff_Analysis_D_R_4_Front_Pos = value;
                }
            }
        }
        
        public System.Nullable<int> Staff_Analysis_D_R_5_Back_Neg
        {
            get
            {
                return this._Staff_Analysis_D_R_5_Back_Neg;
            }
            set
            {
                if ((this._Staff_Analysis_D_R_5_Back_Neg != value))
                {
                    this._Staff_Analysis_D_R_5_Back_Neg = value;
                }
            }
        }
        
        public System.Nullable<int> Staff_Analysis_D_R_5_Front_Neg
        {
            get
            {
                return this._Staff_Analysis_D_R_5_Front_Neg;
            }
            set
            {
                if ((this._Staff_Analysis_D_R_5_Front_Neg != value))
                {
                    this._Staff_Analysis_D_R_5_Front_Neg = value;
                }
            }
        }
        
        public System.Nullable<int> Staff_Analysis_D_R_5_Back_Pos
        {
            get
            {
                return this._Staff_Analysis_D_R_5_Back_Pos;
            }
            set
            {
                if ((this._Staff_Analysis_D_R_5_Back_Pos != value))
                {
                    this._Staff_Analysis_D_R_5_Back_Pos = value;
                }
            }
        }
        
        public System.Nullable<int> Staff_Analysis_D_R_5_Front_Pos
        {
            get
            {
                return this._Staff_Analysis_D_R_5_Front_Pos;
            }
            set
            {
                if ((this._Staff_Analysis_D_R_5_Front_Pos != value))
                {
                    this._Staff_Analysis_D_R_5_Front_Pos = value;
                }
            }
        }
        
        public System.Nullable<int> Staff_Tree_Checked_Back
        {
            get
            {
                return this._Staff_Tree_Checked_Back;
            }
            set
            {
                if ((this._Staff_Tree_Checked_Back != value))
                {
                    this._Staff_Tree_Checked_Back = value;
                }
            }
        }
        
        public System.Nullable<int> Staff_Tree_Checked_Front
        {
            get
            {
                return this._Staff_Tree_Checked_Front;
            }
            set
            {
                if ((this._Staff_Tree_Checked_Front != value))
                {
                    this._Staff_Tree_Checked_Front = value;
                }
            }
        }
        
        public System.Nullable<int> Staff_Tree_UnChecked_Back
        {
            get
            {
                return this._Staff_Tree_UnChecked_Back;
            }
            set
            {
                if ((this._Staff_Tree_UnChecked_Back != value))
                {
                    this._Staff_Tree_UnChecked_Back = value;
                }
            }
        }
        
        public System.Nullable<int> Staff_Tree_UnChecked_Front
        {
            get
            {
                return this._Staff_Tree_UnChecked_Front;
            }
            set
            {
                if ((this._Staff_Tree_UnChecked_Front != value))
                {
                    this._Staff_Tree_UnChecked_Front = value;
                }
            }
        }
        
        public System.Nullable<int> Staff_Tree_Mixed_Back
        {
            get
            {
                return this._Staff_Tree_Mixed_Back;
            }
            set
            {
                if ((this._Staff_Tree_Mixed_Back != value))
                {
                    this._Staff_Tree_Mixed_Back = value;
                }
            }
        }
        
        public System.Nullable<int> Staff_Tree_Mixed_Front
        {
            get
            {
                return this._Staff_Tree_Mixed_Front;
            }
            set
            {
                if ((this._Staff_Tree_Mixed_Front != value))
                {
                    this._Staff_Tree_Mixed_Front = value;
                }
            }
        }
        
        public string Staff_MAN_Number
        {
            get
            {
                return this._Staff_MAN_Number;
            }
            set
            {
                if ((this._Staff_MAN_Number != value))
                {
                    this._Staff_MAN_Number = value;
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
        
        public string Staff_GPS_Location
        {
            get
            {
                return this._Staff_GPS_Location;
            }
            set
            {
                if ((this._Staff_GPS_Location != value))
                {
                    this._Staff_GPS_Location = value;
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
        
        public string Staff_Personel_No
        {
            get
            {
                return this._Staff_Personel_No;
            }
            set
            {
                if ((this._Staff_Personel_No != value))
                {
                    this._Staff_Personel_No = value;
                }
            }
        }
        
        public System.Nullable<bool> Staff_Terminated
        {
            get
            {
                return this._Staff_Terminated;
            }
            set
            {
                if ((this._Staff_Terminated != value))
                {
                    this._Staff_Terminated = value;
                }
            }
        }
        
        public string Staff_Collector_Current_Version
        {
            get
            {
                return this._Staff_Collector_Current_Version;
            }
            set
            {
                if ((this._Staff_Collector_Current_Version != value))
                {
                    this._Staff_Collector_Current_Version = value;
                }
            }
        }
        
        public string Staff_Collector_Uploaded_Version
        {
            get
            {
                return this._Staff_Collector_Uploaded_Version;
            }
            set
            {
                if ((this._Staff_Collector_Uploaded_Version != value))
                {
                    this._Staff_Collector_Uploaded_Version = value;
                }
            }
        }
        
        public string Staff_Engineer_Current_Version
        {
            get
            {
                return this._Staff_Engineer_Current_Version;
            }
            set
            {
                if ((this._Staff_Engineer_Current_Version != value))
                {
                    this._Staff_Engineer_Current_Version = value;
                }
            }
        }
        
        public string Staff_Engineer_Uploaded_Version
        {
            get
            {
                return this._Staff_Engineer_Uploaded_Version;
            }
            set
            {
                if ((this._Staff_Engineer_Uploaded_Version != value))
                {
                    this._Staff_Engineer_Uploaded_Version = value;
                }
            }
        }
        
        public string Staff_Notes
        {
            get
            {
                return this._Staff_Notes;
            }
            set
            {
                if ((this._Staff_Notes != value))
                {
                    this._Staff_Notes = value;
                }
            }
        }
        
        public string Staff_Last_Comms_Test_Sent
        {
            get
            {
                return this._Staff_Last_Comms_Test_Sent;
            }
            set
            {
                if ((this._Staff_Last_Comms_Test_Sent != value))
                {
                    this._Staff_Last_Comms_Test_Sent = value;
                }
            }
        }
        
        public string Staff_Last_Comms_Test_Received
        {
            get
            {
                return this._Staff_Last_Comms_Test_Received;
            }
            set
            {
                if ((this._Staff_Last_Comms_Test_Received != value))
                {
                    this._Staff_Last_Comms_Test_Received = value;
                }
            }
        }
        
        public string Email_Address
        {
            get
            {
                return this._Email_Address;
            }
            set
            {
                if ((this._Email_Address != value))
                {
                    this._Email_Address = value;
                }
            }
        }
                
        public System.Nullable<int> UserTableID
        {
            get
            {
                return this._UserTableID;
            }
            set
            {
                if ((this._UserTableID != value))
                {
                    this._UserTableID = value;
                }
            }
        }
        
        public int SecurityUserID
        {
            get
            {
                return this._SecurityUserID;
            }
            set
            {
                if ((this._SecurityUserID != value))
                {
                    this._SecurityUserID = value;
                }
            }
        }
        
        public string WindowsUserName
        {
            get
            {
                return this._WindowsUserName;
            }
            set
            {
                if ((this._WindowsUserName != value))
                {
                    this._WindowsUserName = value;
                }
            }
        }
        public string UserName
        {
            get
            {
                return this._UserName;
            }
            set
            {
                if ((this._UserName != value))
                {
                    this._UserName = value;
                }
            }
        }

        public string Password
        {
            get
            {
                return this._Password;
            }
            set
            {
                if ((this._Password != value))
                {
                    this._Password = value;
                }
            }
        }

        public System.Nullable<int> LanguageID
        {
            get
            {
                return this._LanguageID;
            }
            set
            {
                if ((this._LanguageID != value))
                {
                    this._LanguageID = value;
                }
            }
        }

        public System.Nullable<int> CurrencyCulture
        {
            get
            {
                return this._CurrencyCulture;
            }
            set
            {
                if ((this._CurrencyCulture != value))
                {
                    this._CurrencyCulture = value;
                }
            }
        }

        public System.Nullable<int> DateCulture
        {
            get
            {
                return this._DateCulture;
            }
            set
            {
                if ((this._DateCulture != value))
                {
                    this._DateCulture = value;
                }
            }
        }

        public System.Nullable<bool> ChangePassword
        {
            get
            {
                return this._ChangePassword;
            }
            set
            {
                if ((this._ChangePassword != value))
                {
                    this._ChangePassword = value;
                }
            }
        }

        public System.Nullable<System.DateTime> CreatedDate
        {
            get
            {
                return this._CreatedDate;
            }
            set
            {
                if ((this._CreatedDate != value))
                {
                    this._CreatedDate = value;
                }
            }
        }

        public System.Nullable<System.DateTime> PasswordChangeDate
        {
            get
            {
                return this._PasswordChangeDate;
            }
            set
            {
                if ((this._PasswordChangeDate != value))
                {
                    this._PasswordChangeDate = value;
                }
            }
        }

        public System.Nullable<bool> isReset
        {
            get
            {
                return this._isReset;
            }
            set
            {
                if ((this._isReset != value))
                {
                    this._isReset = value;
                }
            }
        }

        public bool isLocked
        {
            get
            {
                return this._isLocked;
            }
            set
            {
                if ((this._isLocked != value))
                {
                    this._isLocked = value;
                }
            }
        }
    }
}
    