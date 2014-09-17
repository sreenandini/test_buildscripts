using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.CoreLib;
using System.ComponentModel;
using System.Data.Linq.Mapping;

namespace BMC.EnterpriseBusiness.Entities
{
    

        public partial class GetDepotSCResult
        {

            private int _Service_Area_ID;

            private System.Nullable<int> _Depot_ID;

            private string _Service_Area_Name;

            private string _Service_Area_Description;

            private string _Service_Area_Notes;

            public GetDepotSCResult()
            {
            }

            public int Service_Area_ID
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


            public string Service_Area_Description
            {
                get
                {
                    return this._Service_Area_Description;
                }
                set
                {
                    if ((this._Service_Area_Description != value))
                    {
                        this._Service_Area_Description = value;
                    }
                }
            }


            public string Service_Area_Notes
            {
                get
                {
                    return this._Service_Area_Notes;
                }
                set
                {
                    if ((this._Service_Area_Notes != value))
                    {
                        this._Service_Area_Notes = value;
                    }
                }
            }
        }        

        public partial class MachineTypeEntity
        {

            private string _Machine_Type_Code;

            private string _Machine_Type_Description;

            private System.Nullable<int> _Machine_Type_ID;

            public MachineTypeEntity()
            {
            }

            public string Machine_Type_Code
            {
                get
                {
                    return this._Machine_Type_Code;
                }
                set
                {
                    if ((this._Machine_Type_Code != value))
                    {
                        this._Machine_Type_Code = value;
                    }
                }
            }

            public string Machine_Type_Description
            {
                get
                {
                    return this._Machine_Type_Description;
                }
                set
                {
                    if ((this._Machine_Type_Description != value))
                    {
                        this._Machine_Type_Description = value;
                    }
                }
            }

            public System.Nullable<int> Machine_Type_ID
            {
                get
                {
                    return this._Machine_Type_ID;
                }
                set
                {
                    if ((this._Machine_Type_ID != value))
                    {
                        this._Machine_Type_ID = value;
                    }
                }
            }
        }

        public partial class GetServiceDetailsResult
        {

            private int _Service_ID;

            private System.Nullable<int> _Call_Source_ID;

            private System.Nullable<int> _Call_Fault_ID;

            private System.Nullable<int> _Call_Status_ID;

            private System.Nullable<int> _Call_Remedy_ID;

            private string _Call_Remedy_Additional_Description;

            private System.Nullable<int> _Site_ID;

            private System.Nullable<int> _Bar_Position_ID;

            private System.Nullable<int> _Zone_ID;

            private System.Nullable<int> _Machine_ID;

            private string _Machine_Type_ID;

            private System.Nullable<int> _Installation_ID;

            private System.Nullable<int> _SLA_Contract_ID;

            private System.Nullable<int> _Service_DownTime;

            private string _Service_Received;

            private System.Nullable<int> _Service_Received_Staff_ID;

            private string _Service_Issued;

            private System.Nullable<int> _Service_Issued_By_Staff_ID;

            private System.Nullable<int> _Service_Issued_To_Staff_ID;

            private string _Service_Arrived_At_Site;

            private string _Service_Cleared;

            private System.Nullable<int> _Service_Cleared_Staff_ID;

            private System.Nullable<int> _Service_Closed_ID;

            private System.Nullable<int> _Service_Message_ID;

            private System.Nullable<int> _Service_Sequence_No;

            private System.Nullable<int> _Call_Group_ID;

            private string _Call_Fault_Additional_Notes;

            private System.Nullable<bool> _Service_Additional_Work_Req;

            private System.Nullable<bool> _Service_Alert_Priority_Site;

            private System.Nullable<bool> _Service_Alert_Priority_Machine;

            private System.Nullable<int> _Service_Visit_No;

            private System.Nullable<int> _Service_Allocated_Job_No;

            private string _Service_Acknowledged;

            private System.Nullable<int> _Service_GMU_Type_ID;

            private System.Nullable<int> _Service_GMU_Source_ID;

            private System.Nullable<bool> _IsEscalated;

            public GetServiceDetailsResult()
            {
            }

            public int Service_ID
            {
                get
                {
                    return this._Service_ID;
                }
                set
                {
                    if ((this._Service_ID != value))
                    {
                        this._Service_ID = value;
                    }
                }
            }

         
            public System.Nullable<int> Call_Source_ID
            {
                get
                {
                    return this._Call_Source_ID;
                }
                set
                {
                    if ((this._Call_Source_ID != value))
                    {
                        this._Call_Source_ID = value;
                    }
                }
            }


            public System.Nullable<int> Call_Fault_ID
            {
                get
                {
                    return this._Call_Fault_ID;
                }
                set
                {
                    if ((this._Call_Fault_ID != value))
                    {
                        this._Call_Fault_ID = value;
                    }
                }
            }

       
            public System.Nullable<int> Call_Status_ID
            {
                get
                {
                    return this._Call_Status_ID;
                }
                set
                {
                    if ((this._Call_Status_ID != value))
                    {
                        this._Call_Status_ID = value;
                    }
                }
            }

     
            public System.Nullable<int> Call_Remedy_ID
            {
                get
                {
                    return this._Call_Remedy_ID;
                }
                set
                {
                    if ((this._Call_Remedy_ID != value))
                    {
                        this._Call_Remedy_ID = value;
                    }
                }
            }

           public string Call_Remedy_Additional_Description
            {
                get
                {
                    return this._Call_Remedy_Additional_Description;
                }
                set
                {
                    if ((this._Call_Remedy_Additional_Description != value))
                    {
                        this._Call_Remedy_Additional_Description = value;
                    }
                }
            }

            public System.Nullable<int> Site_ID
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

          
            public System.Nullable<int> Zone_ID
            {
                get
                {
                    return this._Zone_ID;
                }
                set
                {
                    if ((this._Zone_ID != value))
                    {
                        this._Zone_ID = value;
                    }
                }
            }

            
            public System.Nullable<int> Machine_ID
            {
                get
                {
                    return this._Machine_ID;
                }
                set
                {
                    if ((this._Machine_ID != value))
                    {
                        this._Machine_ID = value;
                    }
                }
            }

            
            public string Machine_Type_ID
            {
                get
                {
                    return this._Machine_Type_ID;
                }
                set
                {
                    if ((this._Machine_Type_ID != value))
                    {
                        this._Machine_Type_ID = value;
                    }
                }
            }

           
            public System.Nullable<int> Installation_ID
            {
                get
                {
                    return this._Installation_ID;
                }
                set
                {
                    if ((this._Installation_ID != value))
                    {
                        this._Installation_ID = value;
                    }
                }
            }

         
            public System.Nullable<int> SLA_Contract_ID
            {
                get
                {
                    return this._SLA_Contract_ID;
                }
                set
                {
                    if ((this._SLA_Contract_ID != value))
                    {
                        this._SLA_Contract_ID = value;
                    }
                }
            }

           
            public System.Nullable<int> Service_DownTime
            {
                get
                {
                    return this._Service_DownTime;
                }
                set
                {
                    if ((this._Service_DownTime != value))
                    {
                        this._Service_DownTime = value;
                    }
                }
            }

            
            public string Service_Received
            {
                get
                {
                    return this._Service_Received;
                }
                set
                {
                    if ((this._Service_Received != value))
                    {
                        this._Service_Received = value;
                    }
                }
            }

            public System.Nullable<int> Service_Received_Staff_ID
            {
                get
                {
                    return this._Service_Received_Staff_ID;
                }
                set
                {
                    if ((this._Service_Received_Staff_ID != value))
                    {
                        this._Service_Received_Staff_ID = value;
                    }
                }
            }

           
            public string Service_Issued
            {
                get
                {
                    return this._Service_Issued;
                }
                set
                {
                    if ((this._Service_Issued != value))
                    {
                        this._Service_Issued = value;
                    }
                }
            }

  
            public System.Nullable<int> Service_Issued_By_Staff_ID
            {
                get
                {
                    return this._Service_Issued_By_Staff_ID;
                }
                set
                {
                    if ((this._Service_Issued_By_Staff_ID != value))
                    {
                        this._Service_Issued_By_Staff_ID = value;
                    }
                }
            }

            public System.Nullable<int> Service_Issued_To_Staff_ID
            {
                get
                {
                    return this._Service_Issued_To_Staff_ID;
                }
                set
                {
                    if ((this._Service_Issued_To_Staff_ID != value))
                    {
                        this._Service_Issued_To_Staff_ID = value;
                    }
                }
            }

            
            public string Service_Arrived_At_Site
            {
                get
                {
                    return this._Service_Arrived_At_Site;
                }
                set
                {
                    if ((this._Service_Arrived_At_Site != value))
                    {
                        this._Service_Arrived_At_Site = value;
                    }
                }
            }

            public string Service_Cleared
            {
                get
                {
                    return this._Service_Cleared;
                }
                set
                {
                    if ((this._Service_Cleared != value))
                    {
                        this._Service_Cleared = value;
                    }
                }
            }

            public System.Nullable<int> Service_Cleared_Staff_ID
            {
                get
                {
                    return this._Service_Cleared_Staff_ID;
                }
                set
                {
                    if ((this._Service_Cleared_Staff_ID != value))
                    {
                        this._Service_Cleared_Staff_ID = value;
                    }
                }
            }

            public System.Nullable<int> Service_Closed_ID
            {
                get
                {
                    return this._Service_Closed_ID;
                }
                set
                {
                    if ((this._Service_Closed_ID != value))
                    {
                        this._Service_Closed_ID = value;
                    }
                }
            }

           
            public System.Nullable<int> Service_Message_ID
            {
                get
                {
                    return this._Service_Message_ID;
                }
                set
                {
                    if ((this._Service_Message_ID != value))
                    {
                        this._Service_Message_ID = value;
                    }
                }
            }

            public System.Nullable<int> Service_Sequence_No
            {
                get
                {
                    return this._Service_Sequence_No;
                }
                set
                {
                    if ((this._Service_Sequence_No != value))
                    {
                        this._Service_Sequence_No = value;
                    }
                }
            }

        
            public System.Nullable<int> Call_Group_ID
            {
                get
                {
                    return this._Call_Group_ID;
                }
                set
                {
                    if ((this._Call_Group_ID != value))
                    {
                        this._Call_Group_ID = value;
                    }
                }
            }

          
            public string Call_Fault_Additional_Notes
            {
                get
                {
                    return this._Call_Fault_Additional_Notes;
                }
                set
                {
                    if ((this._Call_Fault_Additional_Notes != value))
                    {
                        this._Call_Fault_Additional_Notes = value;
                    }
                }
            }

           
            public System.Nullable<bool> Service_Additional_Work_Req
            {
                get
                {
                    return this._Service_Additional_Work_Req;
                }
                set
                {
                    if ((this._Service_Additional_Work_Req != value))
                    {
                        this._Service_Additional_Work_Req = value;
                    }
                }
            }

           
            public System.Nullable<bool> Service_Alert_Priority_Site
            {
                get
                {
                    return this._Service_Alert_Priority_Site;
                }
                set
                {
                    if ((this._Service_Alert_Priority_Site != value))
                    {
                        this._Service_Alert_Priority_Site = value;
                    }
                }
            }

           
            public System.Nullable<bool> Service_Alert_Priority_Machine
            {
                get
                {
                    return this._Service_Alert_Priority_Machine;
                }
                set
                {
                    if ((this._Service_Alert_Priority_Machine != value))
                    {
                        this._Service_Alert_Priority_Machine = value;
                    }
                }
            }

           
            public System.Nullable<int> Service_Visit_No
            {
                get
                {
                    return this._Service_Visit_No;
                }
                set
                {
                    if ((this._Service_Visit_No != value))
                    {
                        this._Service_Visit_No = value;
                    }
                }
            }

          
            public System.Nullable<int> Service_Allocated_Job_No
            {
                get
                {
                    return this._Service_Allocated_Job_No;
                }
                set
                {
                    if ((this._Service_Allocated_Job_No != value))
                    {
                        this._Service_Allocated_Job_No = value;
                    }
                }
            }

            
            public string Service_Acknowledged
            {
                get
                {
                    return this._Service_Acknowledged;
                }
                set
                {
                    if ((this._Service_Acknowledged != value))
                    {
                        this._Service_Acknowledged = value;
                    }
                }
            }

            
            public System.Nullable<int> Service_GMU_Type_ID
            {
                get
                {
                    return this._Service_GMU_Type_ID;
                }
                set
                {
                    if ((this._Service_GMU_Type_ID != value))
                    {
                        this._Service_GMU_Type_ID = value;
                    }
                }
            }

            
            public System.Nullable<int> Service_GMU_Source_ID
            {
                get
                {
                    return this._Service_GMU_Source_ID;
                }
                set
                {
                    if ((this._Service_GMU_Source_ID != value))
                    {
                        this._Service_GMU_Source_ID = value;
                    }
                }
            }

            
            public System.Nullable<bool> IsEscalated
            {
                get
                {
                    return this._IsEscalated;
                }
                set
                {
                    if ((this._IsEscalated != value))
                    {
                        this._IsEscalated = value;
                    }
                }
            }
        }

        public partial class GetSLA_ContractResult
        {

            private int _SLA_Contract_ID;

            private string _SLA_Contract_Description;

            private System.Nullable<int> _SLA_Contract_Response;

            private System.Nullable<int> _SLA_Contract_Site_Days;

            private System.Nullable<int> _SLA_Contract_Site_Calls;

            private System.Nullable<int> _SLA_Contract_Machine_Days;

            private System.Nullable<int> _SLA_Contract_Machine_Calls;

            private System.Nullable<int> _SLA_Contract_Order;

            private System.Nullable<bool> _SLA_Contract_Default;

            private System.Nullable<int> _SLA_ID;

            public GetSLA_ContractResult()
            {
            }

            
            public int SLA_Contract_ID
            {
                get
                {
                    return this._SLA_Contract_ID;
                }
                set
                {
                    if ((this._SLA_Contract_ID != value))
                    {
                        this._SLA_Contract_ID = value;
                    }
                }
            }

            
            public string SLA_Contract_Description
            {
                get
                {
                    return this._SLA_Contract_Description;
                }
                set
                {
                    if ((this._SLA_Contract_Description != value))
                    {
                        this._SLA_Contract_Description = value;
                    }
                }
            }

       
            public System.Nullable<int> SLA_Contract_Response
            {
                get
                {
                    return this._SLA_Contract_Response;
                }
                set
                {
                    if ((this._SLA_Contract_Response != value))
                    {
                        this._SLA_Contract_Response = value;
                    }
                }
            }

            public System.Nullable<int> SLA_Contract_Site_Days
            {
                get
                {
                    return this._SLA_Contract_Site_Days;
                }
                set
                {
                    if ((this._SLA_Contract_Site_Days != value))
                    {
                        this._SLA_Contract_Site_Days = value;
                    }
                }
            }

            public System.Nullable<int> SLA_Contract_Site_Calls
            {
                get
                {
                    return this._SLA_Contract_Site_Calls;
                }
                set
                {
                    if ((this._SLA_Contract_Site_Calls != value))
                    {
                        this._SLA_Contract_Site_Calls = value;
                    }
                }
            }

            public System.Nullable<int> SLA_Contract_Machine_Days
            {
                get
                {
                    return this._SLA_Contract_Machine_Days;
                }
                set
                {
                    if ((this._SLA_Contract_Machine_Days != value))
                    {
                        this._SLA_Contract_Machine_Days = value;
                    }
                }
            }

         
            public System.Nullable<int> SLA_Contract_Machine_Calls
            {
                get
                {
                    return this._SLA_Contract_Machine_Calls;
                }
                set
                {
                    if ((this._SLA_Contract_Machine_Calls != value))
                    {
                        this._SLA_Contract_Machine_Calls = value;
                    }
                }
            }

         
            public System.Nullable<int> SLA_Contract_Order
            {
                get
                {
                    return this._SLA_Contract_Order;
                }
                set
                {
                    if ((this._SLA_Contract_Order != value))
                    {
                        this._SLA_Contract_Order = value;
                    }
                }
            }

           
            public System.Nullable<bool> SLA_Contract_Default
            {
                get
                {
                    return this._SLA_Contract_Default;
                }
                set
                {
                    if ((this._SLA_Contract_Default != value))
                    {
                        this._SLA_Contract_Default = value;
                    }
                }
            }

            public System.Nullable<int> SLA_ID
            {
                get
                {
                    return this._SLA_ID;
                }
                set
                {
                    if ((this._SLA_ID != value))
                    {
                        this._SLA_ID = value;
                    }
                }
            }
        }

  
        public enum CallStatus
        {
            // NOTE: If the order of this changed, Please change the related text in 
            // SP - rsp_GetServiceCurrentCallDetails & rsp_FetchOpenServiceCalls
            // And in GetCurrentServiceCalls() method in FieldService.cs in Exchange

            [Description("--ANY--")]
            NONE = 0,
            [Description("Logged")]
            CALL_STATUS_LOGGED = 1,
            [Description("Viewed")]
            CALL_STATUS_VIEWED = 2,
            [Description("Despatched")]
            CALL_STATUS_AWAITING_RECIEPT =3,
            [Description("Accepted")]
            CALL_STATUS_ENGINEER_CONFIRMED = 4,
            [Description("Enroute")]
            CALL_STATUS_ENROUTE = 5,
            [Description("At Site")]
            CALL_STATUS_AT_SITE = 6,
            [Description("Received")]
            CALL_STATUS_RECEIVED = 7,
            [Description("Completed")]
            CALL_STATUS_COMPLETED = 8,
            [Description("Rejected")]
            CALL_STATUS_REFUSED = 9,            
        }

        public partial class SiteEntityForService
        {
            public int Id { get; set; }
            public string Description { get; set; }
        }

        public class DepotEntityForService
        {
            public int Id { get; set; }
            public string Description { get; set; }
        }

        public partial class EngineerEntityForService
        {
            public int Id { get; set; }
            public string Description { get; set; }
        }

        public partial class SubCompanyEntityForService
        {

            private int _Sub_Company_ID;

            private string _Sub_Company_Name;

            public SubCompanyEntityForService()
            {
            }


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
                        this._Sub_Company_ID = value;
                    }
                }
            }


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
                        this._Sub_Company_Name = value;
                    }
                }
            }
        }

        public partial class GetsiteserviceDetailResult
        {

            private string _Standard_Opening_Hours_Description;

            private string _Bar_Position_Name;

            private string _Machine_Name;

            private string _Site_Name;

            private string _Site_Code;

            private string _Site_Address_1;

            private string _Site_Address_2;

            private string _Site_Address_3;

            private string _Site_Address_4;

            private string _Site_Address_5;

            private string _Site_Postcode;

            private string _Site_Manager;

            private string _Site_Phone_No;

            private string _Machine_Stock_No;

            private System.Nullable<int> _Installation_ID;

            private string _Machine_Name1;

            private string _Bar_Position_Name1;

            private string _Depot_Name;

            private string _Service_Area_Name;

            private string _Sub_Company_Name;

            public GetsiteserviceDetailResult()
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

       
            public string Bar_Position_Name
            {
                get
                {
                    return this._Bar_Position_Name;
                }
                set
                {
                    if ((this._Bar_Position_Name != value))
                    {
                        this._Bar_Position_Name = value;
                    }
                }
            }

            public string Machine_Name
            {
                get
                {
                    return this._Machine_Name;
                }
                set
                {
                    if ((this._Machine_Name != value))
                    {
                        this._Machine_Name = value;
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

            
            public string Machine_Stock_No
            {
                get
                {
                    return this._Machine_Stock_No;
                }
                set
                {
                    if ((this._Machine_Stock_No != value))
                    {
                        this._Machine_Stock_No = value;
                    }
                }
            }

           
            public System.Nullable<int> Installation_ID
            {
                get
                {
                    return this._Installation_ID;
                }
                set
                {
                    if ((this._Installation_ID != value))
                    {
                        this._Installation_ID = value;
                    }
                }
            }

            public string Machine_Name1
            {
                get
                {
                    return this._Machine_Name1;
                }
                set
                {
                    if ((this._Machine_Name1 != value))
                    {
                        this._Machine_Name1 = value;
                    }
                }
            }

            public string Bar_Position_Name1
            {
                get
                {
                    return this._Bar_Position_Name1;
                }
                set
                {
                    if ((this._Bar_Position_Name1 != value))
                    {
                        this._Bar_Position_Name1 = value;
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
                        this._Sub_Company_Name = value;
                    }
                }
            }
        }

        public partial class GetMachineDetailsEntity
        {

            private string _Bar_Position_Name;

            private string _Machine_Name;

            private string _Site_Name;

            private string _Site_Code;

            private string _Site_Address_1;

            private string _Site_Address_2;

            private string _Site_Address_3;

            private string _Site_Address_4;

            private string _Site_Address_5;

            private string _Site_Postcode;

            private string _Site_Manager;

            private string _Site_Phone_No;

            private System.Nullable<int> _Installation_ID;

            private string _Machine_Stock_No;

            private string _Machine_Manufacturers_Serial_No;

            private string _Depot_Name;

            private string _Service_Area_Name;

            private string _Sub_Company_Name;

            public GetMachineDetailsEntity()
            {
            }

            public string  Custom_Machine_Name { get; set; }
         
            public string Bar_Position_Name
            {
                get
                {
                    return this._Bar_Position_Name;
                }
                set
                {
                    if ((this._Bar_Position_Name != value))
                    {
                        this._Bar_Position_Name = value;
                    }
                }
            }

           
            public string Machine_Name
            {
                get
                {
                    return this._Machine_Name;
                }
                set
                {
                    if ((this._Machine_Name != value))
                    {
                        this._Machine_Name = value;
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

            
            public System.Nullable<int> Installation_ID
            {
                get
                {
                    return this._Installation_ID;
                }
                set
                {
                    if ((this._Installation_ID != value))
                    {
                        this._Installation_ID = value;
                    }
                }
            }

            public string Machine_Stock_No
            {
                get
                {
                    return this._Machine_Stock_No;
                }
                set
                {
                    if ((this._Machine_Stock_No != value))
                    {
                        this._Machine_Stock_No = value;
                    }
                }
            }

      
            public string Machine_Manufacturers_Serial_No
            {
                get
                {
                    return this._Machine_Manufacturers_Serial_No;
                }
                set
                {
                    if ((this._Machine_Manufacturers_Serial_No != value))
                    {
                        this._Machine_Manufacturers_Serial_No = value;
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
                        this._Sub_Company_Name = value;
                    }
                }
            }
        }

        public partial class GetAllEngineerResult
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

            private System.Nullable<int> _UserTableID;

            private string _Email_Address;

            private string _Staff_Last_Comms_Test_Received;

            private string _Staff_Last_Comms_Test_Sent;

            private string _Staff_Notes;

            private string _Staff_Engineer_Uploaded_Version;

            private string _Staff_Engineer_Current_Version;

            private string _Staff_Collector_Uploaded_Version;

            private string _Staff_Collector_Current_Version;

            private System.Nullable<bool> _Staff_Terminated;

            private string _Staff_Personel_No;

            private System.Nullable<int> _Supplier_ID;

            private System.Nullable<int> _Service_Area_ID;

            private string _Staff_GPS_Location;

            private System.Nullable<int> _Depot_ID;

            private string _Staff_MAN_Number;

            private System.Nullable<int> _Staff_Tree_Mixed_Front;

            private System.Nullable<int> _Staff_Tree_Mixed_Back;

            private System.Nullable<int> _Staff_Tree_UnChecked_Front;

            private System.Nullable<int> _Staff_Tree_UnChecked_Back;

            private System.Nullable<int> _Staff_Tree_Checked_Front;

            private System.Nullable<int> _Staff_Tree_Checked_Back;

            private System.Nullable<int> _Staff_Analysis_D_R_5_Front_Pos;

            private System.Nullable<int> _Staff_Analysis_D_R_5_Back_Pos;

            private System.Nullable<int> _Staff_Analysis_D_R_5_Front_Neg;

            private System.Nullable<int> _Staff_Analysis_D_R_5_Back_Neg;

            private System.Nullable<int> _Staff_Analysis_D_R_4_Front_Pos;

            private System.Nullable<int> _Staff_Analysis_D_R_4_Back_Pos;

            private System.Nullable<int> _Staff_Analysis_D_R_4_Front_Neg;

            private System.Nullable<int> _Staff_Analysis_D_R_3_Front_Pos;

            private System.Nullable<int> _Staff_Analysis_D_R_4_Back_Neg;

            private System.Nullable<int> _Staff_Analysis_D_R_3_Back_Pos;

            private System.Nullable<int> _Staff_Analysis_D_R_3_Front_Neg;

            private System.Nullable<int> _Staff_Analysis_D_R_3_Back_Neg;

            private System.Nullable<int> _Staff_Analysis_D_R_2_Front_Pos;

            private System.Nullable<int> _Staff_Analysis_D_R_2_Back_Pos;

            private System.Nullable<int> _Staff_Analysis_D_R_2_Front_Neg;

            private System.Nullable<int> _Staff_Analysis_D_R_2_Back_Neg;

            private System.Nullable<int> _Staff_Analysis_D_R_1_Front_Pos;

            private System.Nullable<int> _Staff_Analysis_D_R_1_Back_Pos;

            private System.Nullable<int> _Staff_Analysis_D_R_1_Front_Neg;

            private System.Nullable<int> _Staff_Analysis_D_R_1_Back_Neg;

            public GetAllEngineerResult()
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
        }

        public partial class GetEngineerdetailSS
	    {
		
		    private int _Site_ID;
		
		    private string _Staff_First_Name;
		
		    private string _Staff_Last_Name;
		
		    private System.Nullable<int> _Staff_ID;
		
		    private System.Nullable<int> _Service_Area_ID;
		
		    public GetEngineerdetailSS()
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
	    }
       
        public partial class SLA_ContractDetailasResult
            {

                private string _SLA_Contract_Description;

                private System.Nullable<int> _SLA_Contract_ID;

                public SLA_ContractDetailasResult()
                {
                }

            
                public string SLA_Contract_Description
                {
                    get
                    {
                        return this._SLA_Contract_Description;
                    }
                    set
                    {
                        if ((this._SLA_Contract_Description != value))
                        {
                            this._SLA_Contract_Description = value;
                        }
                    }
                }

            
                public System.Nullable<int> SLA_Contract_ID
                {
                    get
                    {
                        return this._SLA_Contract_ID;
                    }
                    set
                    {
                        if ((this._SLA_Contract_ID != value))
                        {
                            this._SLA_Contract_ID = value;
                        }
                    }
                }
            }

        public partial class ServiceCallFaultDetailsResult
	    {
		
		    private int _Service_ID;
		
		    private System.Nullable<int> _Call_Source_ID;
		
		    private System.Nullable<int> _Call_Fault_ID;
		
		    private System.Nullable<int> _Call_Status_ID;
		
		    private System.Nullable<int> _Call_Remedy_ID;
		
		    private string _Call_Remedy_Additional_Description;
		
		    private System.Nullable<int> _Site_ID;
		
		    private System.Nullable<int> _Bar_Position_ID;
		
		    private System.Nullable<int> _Zone_ID;
		
		    private System.Nullable<int> _Machine_ID;
		
		    private string _Machine_Type_ID;
		
		    private System.Nullable<int> _Installation_ID;
		
		    private System.Nullable<int> _SLA_Contract_ID;
		
		    private System.Nullable<int> _Service_DownTime;
		
		    private string _Service_Received;
		
		    private System.Nullable<int> _Service_Received_Staff_ID;
		
		    private string _Service_Issued;
		
		    private System.Nullable<int> _Service_Issued_By_Staff_ID;
		
		    private System.Nullable<int> _Service_Issued_To_Staff_ID;
		
		    private string _Service_Cleared;
		
		    private string _Service_Arrived_At_Site;
		
		    private System.Nullable<int> _Service_Cleared_Staff_ID;
		
		    private System.Nullable<int> _Service_Closed_ID;
		
		    private System.Nullable<int> _Service_Message_ID;
		
		    private System.Nullable<int> _Service_Sequence_No;
		
		    private System.Nullable<int> _Call_Group_ID;
		
		    private string _Call_Fault_Additional_Notes;
		
		    private System.Nullable<bool> _Service_Alert_Priority_Site;
		
		    private System.Nullable<bool> _Service_Additional_Work_Req;
		
		    private System.Nullable<bool> _Service_Alert_Priority_Machine;
		
		    private System.Nullable<int> _Service_Visit_No;
		
		    private System.Nullable<int> _Service_Allocated_Job_No;
		
		    private string _Service_Acknowledged;
		
		    private System.Nullable<int> _Service_GMU_Source_ID;
		
		    private System.Nullable<int> _Service_GMU_Type_ID;
		
		    private System.Nullable<bool> _IsEscalated;
		
		    public ServiceCallFaultDetailsResult()
		    {
		    }
		
		    public int Service_ID
		    {
			    get
			    {
				    return this._Service_ID;
			    }
			    set
			    {
				    if ((this._Service_ID != value))
				    {
					    this._Service_ID = value;
				    }
			    }
		    }
	
		    public System.Nullable<int> Call_Source_ID
		    {
			    get
			    {
				    return this._Call_Source_ID;
			    }
			    set
			    {
				    if ((this._Call_Source_ID != value))
				    {
					    this._Call_Source_ID = value;
				    }
			    }
		    }
		

		    public System.Nullable<int> Call_Fault_ID
		    {
			    get
			    {
				    return this._Call_Fault_ID;
			    }
			    set
			    {
				    if ((this._Call_Fault_ID != value))
				    {
					    this._Call_Fault_ID = value;
				    }
			    }
		    }
		
	
		    public System.Nullable<int> Call_Status_ID
		    {
			    get
			    {
				    return this._Call_Status_ID;
			    }
			    set
			    {
				    if ((this._Call_Status_ID != value))
				    {
					    this._Call_Status_ID = value;
				    }
			    }
		    }
		
		
		    public System.Nullable<int> Call_Remedy_ID
		    {
			    get
			    {
				    return this._Call_Remedy_ID;
			    }
			    set
			    {
				    if ((this._Call_Remedy_ID != value))
				    {
					    this._Call_Remedy_ID = value;
				    }
			    }
		    }
		
		
		    public string Call_Remedy_Additional_Description
		    {
			    get
			    {
				    return this._Call_Remedy_Additional_Description;
			    }
			    set
			    {
				    if ((this._Call_Remedy_Additional_Description != value))
				    {
					    this._Call_Remedy_Additional_Description = value;
				    }
			    }
		    }
		
		
		    public System.Nullable<int> Site_ID
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
		
	
		    public System.Nullable<int> Zone_ID
		    {
			    get
			    {
				    return this._Zone_ID;
			    }
			    set
			    {
				    if ((this._Zone_ID != value))
				    {
					    this._Zone_ID = value;
				    }
			    }
		    }
		
		
		    public System.Nullable<int> Machine_ID
		    {
			    get
			    {
				    return this._Machine_ID;
			    }
			    set
			    {
				    if ((this._Machine_ID != value))
				    {
					    this._Machine_ID = value;
				    }
			    }
		    }
		
		
		    public string Machine_Type_ID
		    {
			    get
			    {
				    return this._Machine_Type_ID;
			    }
			    set
			    {
				    if ((this._Machine_Type_ID != value))
				    {
					    this._Machine_Type_ID = value;
				    }
			    }
		    }
		
	
		    public System.Nullable<int> Installation_ID
		    {
			    get
			    {
				    return this._Installation_ID;
			    }
			    set
			    {
				    if ((this._Installation_ID != value))
				    {
					    this._Installation_ID = value;
				    }
			    }
		    }
		
	
		    public System.Nullable<int> SLA_Contract_ID
		    {
			    get
			    {
				    return this._SLA_Contract_ID;
			    }
			    set
			    {
				    if ((this._SLA_Contract_ID != value))
				    {
					    this._SLA_Contract_ID = value;
				    }
			    }
		    }
		
	
		    public System.Nullable<int> Service_DownTime
		    {
			    get
			    {
				    return this._Service_DownTime;
			    }
			    set
			    {
				    if ((this._Service_DownTime != value))
				    {
					    this._Service_DownTime = value;
				    }
			    }
		    }
		
		
		    public string Service_Received
		    {
			    get
			    {
				    return this._Service_Received;
			    }
			    set
			    {
				    if ((this._Service_Received != value))
				    {
					    this._Service_Received = value;
				    }
			    }
		    }
		
		
		    public System.Nullable<int> Service_Received_Staff_ID
		    {
			    get
			    {
				    return this._Service_Received_Staff_ID;
			    }
			    set
			    {
				    if ((this._Service_Received_Staff_ID != value))
				    {
					    this._Service_Received_Staff_ID = value;
				    }
			    }
		    }
		
	
		    public string Service_Issued
		    {
			    get
			    {
				    return this._Service_Issued;
			    }
			    set
			    {
				    if ((this._Service_Issued != value))
				    {
					    this._Service_Issued = value;
				    }
			    }
		    }
		
		
		    public System.Nullable<int> Service_Issued_By_Staff_ID
		    {
			    get
			    {
				    return this._Service_Issued_By_Staff_ID;
			    }
			    set
			    {
				    if ((this._Service_Issued_By_Staff_ID != value))
				    {
					    this._Service_Issued_By_Staff_ID = value;
				    }
			    }
		    }
		
		
		    public System.Nullable<int> Service_Issued_To_Staff_ID
		    {
			    get
			    {
				    return this._Service_Issued_To_Staff_ID;
			    }
			    set
			    {
				    if ((this._Service_Issued_To_Staff_ID != value))
				    {
					    this._Service_Issued_To_Staff_ID = value;
				    }
			    }
		    }
		
		
		    public string Service_Cleared
		    {
			    get
			    {
				    return this._Service_Cleared;
			    }
			    set
			    {
				    if ((this._Service_Cleared != value))
				    {
					    this._Service_Cleared = value;
				    }
			    }
		    }
		
	
		    public string Service_Arrived_At_Site
		    {
			    get
			    {
				    return this._Service_Arrived_At_Site;
			    }
			    set
			    {
				    if ((this._Service_Arrived_At_Site != value))
				    {
					    this._Service_Arrived_At_Site = value;
				    }
			    }
		    }
		
	
		    public System.Nullable<int> Service_Cleared_Staff_ID
		    {
			    get
			    {
				    return this._Service_Cleared_Staff_ID;
			    }
			    set
			    {
				    if ((this._Service_Cleared_Staff_ID != value))
				    {
					    this._Service_Cleared_Staff_ID = value;
				    }
			    }
		    }
		
		
		    public System.Nullable<int> Service_Closed_ID
		    {
			    get
			    {
				    return this._Service_Closed_ID;
			    }
			    set
			    {
				    if ((this._Service_Closed_ID != value))
				    {
					    this._Service_Closed_ID = value;
				    }
			    }
		    }
		
		
		    public System.Nullable<int> Service_Message_ID
		    {
			    get
			    {
				    return this._Service_Message_ID;
			    }
			    set
			    {
				    if ((this._Service_Message_ID != value))
				    {
					    this._Service_Message_ID = value;
				    }
			    }
		    }
		
		
		    public System.Nullable<int> Service_Sequence_No
		    {
			    get
			    {
				    return this._Service_Sequence_No;
			    }
			    set
			    {
				    if ((this._Service_Sequence_No != value))
				    {
					    this._Service_Sequence_No = value;
				    }
			    }
		    }
		
		
		    public System.Nullable<int> Call_Group_ID
		    {
			    get
			    {
				    return this._Call_Group_ID;
			    }
			    set
			    {
				    if ((this._Call_Group_ID != value))
				    {
					    this._Call_Group_ID = value;
				    }
			    }
		    }
		
		
		    public string Call_Fault_Additional_Notes
		    {
			    get
			    {
				    return this._Call_Fault_Additional_Notes;
			    }
			    set
			    {
				    if ((this._Call_Fault_Additional_Notes != value))
				    {
					    this._Call_Fault_Additional_Notes = value;
				    }
			    }
		    }
		
		
		    public System.Nullable<bool> Service_Alert_Priority_Site
		    {
			    get
			    {
				    return this._Service_Alert_Priority_Site;
			    }
			    set
			    {
				    if ((this._Service_Alert_Priority_Site != value))
				    {
					    this._Service_Alert_Priority_Site = value;
				    }
			    }
		    }
		
		
		    public System.Nullable<bool> Service_Additional_Work_Req
		    {
			    get
			    {
				    return this._Service_Additional_Work_Req;
			    }
			    set
			    {
				    if ((this._Service_Additional_Work_Req != value))
				    {
					    this._Service_Additional_Work_Req = value;
				    }
			    }
		    }
		
		
		    public System.Nullable<bool> Service_Alert_Priority_Machine
		    {
			    get
			    {
				    return this._Service_Alert_Priority_Machine;
			    }
			    set
			    {
				    if ((this._Service_Alert_Priority_Machine != value))
				    {
					    this._Service_Alert_Priority_Machine = value;
				    }
			    }
		    }
		
		
		    public System.Nullable<int> Service_Visit_No
		    {
			    get
			    {
				    return this._Service_Visit_No;
			    }
			    set
			    {
				    if ((this._Service_Visit_No != value))
				    {
					    this._Service_Visit_No = value;
				    }
			    }
		    }
	
		    public System.Nullable<int> Service_Allocated_Job_No
		    {
			    get
			    {
				    return this._Service_Allocated_Job_No;
			    }
			    set
			    {
				    if ((this._Service_Allocated_Job_No != value))
				    {
					    this._Service_Allocated_Job_No = value;
				    }
			    }
		    }
		
		
		    public string Service_Acknowledged
		    {
			    get
			    {
				    return this._Service_Acknowledged;
			    }
			    set
			    {
				    if ((this._Service_Acknowledged != value))
				    {
					    this._Service_Acknowledged = value;
				    }
			    }
		    }
		
		
		    public System.Nullable<int> Service_GMU_Source_ID
		    {
			    get
			    {
				    return this._Service_GMU_Source_ID;
			    }
			    set
			    {
				    if ((this._Service_GMU_Source_ID != value))
				    {
					    this._Service_GMU_Source_ID = value;
				    }
			    }
		    }
		
		
		    public System.Nullable<int> Service_GMU_Type_ID
		    {
			    get
			    {
				    return this._Service_GMU_Type_ID;
			    }
			    set
			    {
				    if ((this._Service_GMU_Type_ID != value))
				    {
					    this._Service_GMU_Type_ID = value;
				    }
			    }
		    }
		
		
		    public System.Nullable<bool> IsEscalated
		    {
			    get
			    {
				    return this._IsEscalated;
			    }
			    set
			    {
				    if ((this._IsEscalated != value))
				    {
					    this._IsEscalated = value;
				    }
			    }
		    }
	    }

        public partial class ServiceCurrentCallDetailsEntity
        {

            private string _Service_Received;

            private string _TimeOpened;

            private string _Staff_Name;

            private string _Site_Address;

            private string _Site_Code;

            private string _Machine_Name;

            private string _Machine_Type_ID;

            private string _Engineer_Name;

            private string _SLA_Contract_Description;

            private string _Call_Description;

            private string _Call_Status_Description;

            private string _Service_Job_Visit_No;

            private string _Sub_Company_Name;

            private string _Service_SMA;

            private string _Site_Postcode;

            private System.Nullable<int> _Service_Allocated_Job_No;

            private int _Service_ID;

            private System.Nullable<int> _Call_Status_ID;

            private System.Nullable<int> _CallOpenDays;

            private string _Service_Alert_Priority_Site;

            private string _Service_Alert_Priority_Machine;

            private string _Service_Additional_Work_Req;

            private System.Nullable<int> _Site_ID;

            private string _Bar_Position_Name;

            public ServiceCurrentCallDetailsEntity()
            {
            }

            public string Service_Job_Visit_No
            {
                get
                {
                    return this._Service_Job_Visit_No;
                }
                set
                {
                    if ((this._Service_Job_Visit_No != value))
                    {
                        this._Service_Job_Visit_No = value;
                    }
                }
            }

            public string Call_Status_Description
            {
                get
                {
                    return this._Call_Status_Description;
                }
                set
                {
                    if ((this._Call_Status_Description != value))
                    {
                        this._Call_Status_Description = value;
                    }
                }
            }


            public string Service_Received
            {
                get
                {
                    return this._Service_Received;
                }
                set
                {
                    if ((this._Service_Received != value))
                    {
                        this._Service_Received = value;
                    }
                }
            }


            public string Call_Description
            {
                get
                {
                    return this._Call_Description;
                }
                set
                {
                    if ((this._Call_Description != value))
                    {
                        this._Call_Description = value;
                    }
                }
            }

            public string Service_SMA
            {
                get
                {
                    return this._Service_SMA;
                }
                set
                {
                    if ((this._Service_SMA != value))
                    {
                        this._Service_SMA = value;
                    }
                }
            }

            public string TimeOpened
            {
                get
                {
                    return this._TimeOpened;
                }
                set
                {
                    if ((this._TimeOpened != value))
                    {
                        this._TimeOpened = value;
                    }
                }
            }

            public string Staff_Name
            {
                get
                {
                    return this._Staff_Name;
                }
                set
                {
                    if ((this._Staff_Name != value))
                    {
                        this._Staff_Name = value;
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

            public string Machine_Type_ID
            {
                get
                {
                    return this._Machine_Type_ID;
                }
                set
                {
                    if ((this._Machine_Type_ID != value))
                    {
                        this._Machine_Type_ID = value;
                    }
                }
            }

            public string Machine_Name
            {
                get
                {
                    return this._Machine_Name;
                }
                set
                {
                    if ((this._Machine_Name != value))
                    {
                        this._Machine_Name = value;
                    }
                }
            }

            public string Engineer_Name
            {
                get
                {
                    return this._Engineer_Name;
                }
                set
                {
                    if ((this._Engineer_Name != value))
                    {
                        this._Engineer_Name = value;
                    }
                }
            }

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
                        this._Sub_Company_Name = value;
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

            public string SLA_Contract_Description
            {
                get
                {
                    return this._SLA_Contract_Description;
                }
                set
                {
                    if ((this._SLA_Contract_Description != value))
                    {
                        this._SLA_Contract_Description = value;
                    }
                }
            }

            public System.Nullable<int> Service_Allocated_Job_No
            {
                get
                {
                    return this._Service_Allocated_Job_No;
                }
                set
                {
                    if ((this._Service_Allocated_Job_No != value))
                    {
                        this._Service_Allocated_Job_No = value;
                    }
                }
            }

            public int Service_ID
            {
                get
                {
                    return this._Service_ID;
                }
                set
                {
                    if ((this._Service_ID != value))
                    {
                        this._Service_ID = value;
                    }
                }
            }

            public System.Nullable<int> Call_Status_ID
            {
                get
                {
                    return this._Call_Status_ID;
                }
                set
                {
                    if ((this._Call_Status_ID != value))
                    {
                        this._Call_Status_ID = value;
                    }
                }
            }

            public System.Nullable<int> CallOpenDays
            {
                get
                {
                    return this._CallOpenDays;
                }
                set
                {
                    if ((this._CallOpenDays != value))
                    {
                        this._CallOpenDays = value;
                    }
                }
            }

            public string Service_Alert_Priority_Site
            {
                get
                {
                    return this._Service_Alert_Priority_Site;
                }
                set
                {
                    if ((this._Service_Alert_Priority_Site != value))
                    {
                        this._Service_Alert_Priority_Site = value;
                    }
                }
            }

            public string Service_Alert_Priority_Machine
            {
                get
                {
                    return this._Service_Alert_Priority_Machine;
                }
                set
                {
                    if ((this._Service_Alert_Priority_Machine != value))
                    {
                        this._Service_Alert_Priority_Machine = value;
                    }
                }
            }

            public string Service_Additional_Work_Req
            {
                get
                {
                    return this._Service_Additional_Work_Req;
                }
                set
                {
                    if ((this._Service_Additional_Work_Req != value))
                    {
                        this._Service_Additional_Work_Req = value;
                    }
                }
            }

            public string Bar_Position_Name
            {
                get
                {
                    return this._Bar_Position_Name;
                }
                set
                {
                    if ((this._Bar_Position_Name != value))
                    {
                        this._Bar_Position_Name = value;
                    }
                }
            }

            public System.Nullable<int> Site_ID
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


        }

        public partial class ServiceClosedCallDetailsEntity
           {

               private string _Service_Received;

               private string _Staff_Name_Received;

               private string _Staff_Name_Cleared;

               private string _Service_DownTime_HH_mm;

               private string _Service_Cleared;

               private string _Service_Job_Visit_No;

               private string _Site_Name_Address;

               private string _Site_Code;

               private string _Machine_Name_Stock_No;

               private string _Machine_Type_ID;

               private string _Engineer_Name;

               private string _SLA_Contract_Description;

               private string _Call_Description;

               private string _Call_Remedy_Description;

               private System.Nullable<int> _TimeOpened;

               private System.Nullable<int> _Service_DownTime;

               private System.Nullable<int> _Call_Remedy_ID;

               private System.Nullable<int> _Service_Allocated_Job_No;

               private int _Service_ID;

               private System.Nullable<int> _Call_Status_ID;

               private System.Nullable<int> _Zone_ID;

               private System.Nullable<int> _Site_ID;

               private System.Nullable<int> _Staff_ID;

               private System.Nullable<int> _Call_Group_ID;

               private System.Nullable<int> _Depot_ID;

               private string _Bar_Position_Name;
               private string _Site_Postcode;

               public ServiceClosedCallDetailsEntity()
               {
               }

               public string Service_Job_Visit_No
               {
                   get
                   {
                       return this._Service_Job_Visit_No;
                   }
                   set
                   {
                       if ((this._Service_Job_Visit_No != value))
                       {
                           this._Service_Job_Visit_No = value;
                       }
                   }
               }

               public string Service_Received
               {
                   get
                   {
                       return this._Service_Received;
                   }
                   set
                   {
                       if ((this._Service_Received != value))
                       {
                           this._Service_Received = value;
                       }
                   }
               }

               public string Staff_Name_Received
               {
                   get
                   {
                       return this._Staff_Name_Received;
                   }
                   set
                   {
                       if ((this._Staff_Name_Received != value))
                       {
                           this._Staff_Name_Received = value;
                       }
                   }
               }

               public string Staff_Name_Cleared
               {
                   get
                   {
                       return this._Staff_Name_Cleared;
                   }
                   set
                   {
                       if ((this._Staff_Name_Cleared != value))
                       {
                           this._Staff_Name_Cleared = value;
                       }
                   }
               }

               public string Service_DownTime_HH_mm
               {
                   get
                   {
                       return this._Service_DownTime_HH_mm;
                   }
                   set
                   {
                       if ((this._Service_DownTime_HH_mm != value))
                       {
                           this._Service_DownTime_HH_mm = value;
                       }
                   }
               }

               public string Service_Cleared
               {
                   get
                   {
                       return this._Service_Cleared;
                   }
                   set
                   {
                       if ((this._Service_Cleared != value))
                       {
                           this._Service_Cleared = value;
                       }
                   }
               }

               public string Site_Name_Address
               {
                   get
                   {
                       return this._Site_Name_Address;
                   }
                   set
                   {
                       if ((this._Site_Name_Address != value))
                       {
                           this._Site_Name_Address = value;
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

               public string Machine_Type_ID
               {
                   get
                   {
                       return this._Machine_Type_ID;
                   }
                   set
                   {
                       if ((this._Machine_Type_ID != value))
                       {
                           this._Machine_Type_ID = value;
                       }
                   }
               }

               public string Machine_Name_Stock_No
               {
                   get
                   {
                       return this._Machine_Name_Stock_No;
                   }
                   set
                   {
                       if ((this._Machine_Name_Stock_No != value))
                       {
                           this._Machine_Name_Stock_No = value;
                       }
                   }
               }

               public string Engineer_Name
               {
                   get
                   {
                       return this._Engineer_Name;
                   }
                   set
                   {
                       if ((this._Engineer_Name != value))
                       {
                           this._Engineer_Name = value;
                       }
                   }
               }

               public string SLA_Contract_Description
               {
                   get
                   {
                       return this._SLA_Contract_Description;
                   }
                   set
                   {
                       if ((this._SLA_Contract_Description != value))
                       {
                           this._SLA_Contract_Description = value;
                       }
                   }
               }

               public string Call_Description
               {
                   get
                   {
                       return this._Call_Description;
                   }
                   set
                   {
                       if ((this._Call_Description != value))
                       {
                           this._Call_Description = value;
                       }
                   }
               }

               public string Call_Remedy_Description
               {
                   get
                   {
                       return this._Call_Remedy_Description;
                   }
                   set
                   {
                       if ((this._Call_Remedy_Description != value))
                       {
                           this._Call_Remedy_Description = value;
                       }
                   }
               }

               public string Bar_Position_Name
               {
                   get
                   {
                       return this._Bar_Position_Name;
                   }
                   set
                   {
                       if ((this._Bar_Position_Name != value))
                       {
                           this._Bar_Position_Name = value;
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

               public System.Nullable<int> TimeOpened
               {
                   get
                   {
                       return this._TimeOpened;
                   }
                   set
                   {
                       if ((this._TimeOpened != value))
                       {
                           this._TimeOpened = value;
                       }
                   }
               }

               public System.Nullable<int> Service_DownTime
               {
                   get
                   {
                       return this._Service_DownTime;
                   }
                   set
                   {
                       if ((this._Service_DownTime != value))
                       {
                           this._Service_DownTime = value;
                       }
                   }
               }

               public System.Nullable<int> Call_Remedy_ID
               {
                   get
                   {
                       return this._Call_Remedy_ID;
                   }
                   set
                   {
                       if ((this._Call_Remedy_ID != value))
                       {
                           this._Call_Remedy_ID = value;
                       }
                   }
               }

               public System.Nullable<int> Service_Allocated_Job_No
               {
                   get
                   {
                       return this._Service_Allocated_Job_No;
                   }
                   set
                   {
                       if ((this._Service_Allocated_Job_No != value))
                       {
                           this._Service_Allocated_Job_No = value;
                       }
                   }
               }

               public int Service_ID
               {
                   get
                   {
                       return this._Service_ID;
                   }
                   set
                   {
                       if ((this._Service_ID != value))
                       {
                           this._Service_ID = value;
                       }
                   }
               }

               public System.Nullable<int> Call_Status_ID
               {
                   get
                   {
                       return this._Call_Status_ID;
                   }
                   set
                   {
                       if ((this._Call_Status_ID != value))
                       {
                           this._Call_Status_ID = value;
                       }
                   }
               }

               public System.Nullable<int> Zone_ID
               {
                   get
                   {
                       return this._Zone_ID;
                   }
                   set
                   {
                       if ((this._Zone_ID != value))
                       {
                           this._Zone_ID = value;
                       }
                   }
               }

               public System.Nullable<int> Site_ID
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

               public System.Nullable<int> Call_Group_ID
               {
                   get
                   {
                       return this._Call_Group_ID;
                   }
                   set
                   {
                       if ((this._Call_Group_ID != value))
                       {
                           this._Call_Group_ID = value;
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
           }

        public partial class SiteDetailEntity
        {

            private string _Standard_Opening_Hours_Description;

            private string _Site_Name;

            private string _Site_Code;

            private string _Site_Address_1;

            private string _Site_Address_2;

            private string _Site_Address_3;

            private string _Site_Address_4;

            private string _Site_Address_5;

            private string _Site_Postcode;

            private string _Site_Manager;

            private string _Site_Phone_No;

            private string _Depot_Name;

            private string _Service_Area_Name;

            private string _Sub_Company_Name;

            public SiteDetailEntity()
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
                        this._Sub_Company_Name = value;
                    }
                }
            }
        }

        public partial class ServiceEntity
        {
            private int _Service_ID;
            
            private string _Call_Fault_Additional_Notes;

            private System.Nullable<int> _Call_Fault_ID;

            private System.Nullable<int> _Call_Group_ID;

            private string _Call_Remedy_Additional_Description;

            private System.Nullable<int> _Call_Remedy_ID;

            private System.Nullable<int> _Call_Source_ID;

            private System.Nullable<int> _Call_Status_ID;

            private System.Nullable<int> _Installation_ID;

            private string _Machine_Type_ID;

            private string _Service_Acknowledged;

            private System.Nullable<bool> _Service_Additional_Work_Req;

            private System.Nullable<int> _Service_Allocated_Job_No;

            private string _Service_Arrived_At_Site;

            private string _Service_Cleared;

            private string _Service_Issued;

            private System.Nullable<int> _Service_Issued_To_Staff_ID;

            private string _Service_Received;

            private System.Nullable<int> _Service_Visit_No;

            private System.Nullable<int> _Site_ID;

            private System.Nullable<int> _SLA_Contract_ID;

            private System.Nullable<int> _Service_Received_Staff_ID;

            private System.Nullable<int> _Service_Issued_By_Staff_ID;

            private System.Nullable<int> _Service_Cleared_Staff_ID;

            public ServiceEntity()
            {
            }

            [Column(Storage = "Service_ID")]
            public int Service_ID
            {
                get
                {
                    return this._Service_ID;
                }
                set
                {
                    if ((this._Service_ID != value))
                    {
                        this._Service_ID = value;
                    }
                }
            }

            [Column(Storage="Call_Fault_Additional_Notes")]
            public string Call_Fault_Additional_Notes
            {
                get
                {
                    return this._Call_Fault_Additional_Notes;
                }
                set
                {
                    if ((this._Call_Fault_Additional_Notes != value))
                    {
                        this._Call_Fault_Additional_Notes = value;
                    }
                }
            }

             [Column(Storage = "Call_Fault_ID")]
            public System.Nullable<int> Call_Fault_ID
            {
                get
                {
                    return this._Call_Fault_ID;
                }
                set
                {
                    if ((this._Call_Fault_ID != value))
                    {
                        this._Call_Fault_ID = value;
                    }
                }
            }

             [Column(Storage = "Call_Group_ID")]
            public System.Nullable<int> Call_Group_ID
            {
                get
                {
                    return this._Call_Group_ID;
                }
                set
                {
                    if ((this._Call_Group_ID != value))
                    {
                        this._Call_Group_ID = value;
                    }
                }
            }

             [Column(Storage = "Call_Remedy_Additional_Description")]
            public string Call_Remedy_Additional_Description
            {
                get
                {
                    return this._Call_Remedy_Additional_Description;
                }
                set
                {
                    if ((this._Call_Remedy_Additional_Description != value))
                    {
                        this._Call_Remedy_Additional_Description = value;
                    }
                }
            }

             [Column(Storage = "Call_Remedy_ID")]
            public System.Nullable<int> Call_Remedy_ID
            {
                get
                {
                    return this._Call_Remedy_ID;
                }
                set
                {
                    if ((this._Call_Remedy_ID != value))
                    {
                        this._Call_Remedy_ID = value;
                    }
                }
            }

             [Column(Storage = "Call_Source_ID")]
            public System.Nullable<int> Call_Source_ID
            {
                get
                {
                    return this._Call_Source_ID;
                }
                set
                {
                    if ((this._Call_Source_ID != value))
                    {
                        this._Call_Source_ID = value;
                    }
                }
            }

             [Column(Storage = "Call_Status_ID")]
            public System.Nullable<int> Call_Status_ID
            {
                get
                {
                    return this._Call_Status_ID;
                }
                set
                {
                    if ((this._Call_Status_ID != value))
                    {
                        this._Call_Status_ID = value;
                    }
                }
            }

             [Column(Storage = "Installation_ID")]
            public System.Nullable<int> Installation_ID
            {
                get
                {
                    return this._Installation_ID;
                }
                set
                {
                    if ((this._Installation_ID != value))
                    {
                        this._Installation_ID = value;
                    }
                }
            }

             [Column(Storage = "Machine_Type_ID")]
            public string Machine_Type_ID
            {
                get
                {
                    return this._Machine_Type_ID;
                }
                set
                {
                    if ((this._Machine_Type_ID != value))
                    {
                        this._Machine_Type_ID = value;
                    }
                }
            }

             [Column(Storage = "Service_Acknowledged")]
            public string Service_Acknowledged
            {
                get
                {
                    return this._Service_Acknowledged;
                }
                set
                {
                    if ((this._Service_Acknowledged != value))
                    {
                        this._Service_Acknowledged = value;
                    }
                }
            }

             [Column(Storage = "Service_Additional_Work_Req")]
            public System.Nullable<bool> Service_Additional_Work_Req
            {
                get
                {
                    return this._Service_Additional_Work_Req;
                }
                set
                {
                    if ((this._Service_Additional_Work_Req != value))
                    {
                        this._Service_Additional_Work_Req = value;
                    }
                }
            }

             [Column(Storage = "Service_Allocated_Job_No")]
            public System.Nullable<int> Service_Allocated_Job_No
            {
                get
                {
                    return this._Service_Allocated_Job_No;
                }
                set
                {
                    if ((this._Service_Allocated_Job_No != value))
                    {
                        this._Service_Allocated_Job_No = value;
                    }
                }
            }

             [Column(Storage = "Service_Arrived_At_Site")]
            public string Service_Arrived_At_Site
            {
                get
                {
                    return this._Service_Arrived_At_Site;
                }
                set
                {
                    if ((this._Service_Arrived_At_Site != value))
                    {
                        this._Service_Arrived_At_Site = value;
                    }
                }
            }

             [Column(Storage = "Service_Cleared")]
            public string Service_Cleared
            {
                get
                {
                    return this._Service_Cleared;
                }
                set
                {
                    if ((this._Service_Cleared != value))
                    {
                        this._Service_Cleared = value;
                    }
                }
            }

             [Column(Storage = "Service_Issued")]
            public string Service_Issued
            {
                get
                {
                    return this._Service_Issued;
                }
                set
                {
                    if ((this._Service_Issued != value))
                    {
                        this._Service_Issued = value;
                    }
                }
            }

             [Column(Storage = "Service_Issued_To_Staff_ID")]
            public System.Nullable<int> Service_Issued_To_Staff_ID
            {
                get
                {
                    return this._Service_Issued_To_Staff_ID;
                }
                set
                {
                    if ((this._Service_Issued_To_Staff_ID != value))
                    {
                        this._Service_Issued_To_Staff_ID = value;
                    }
                }
            }

             [Column(Storage = "Service_Received")]
            public string Service_Received
            {
                get
                {
                    return this._Service_Received;
                }
                set
                {
                    if ((this._Service_Received != value))
                    {
                        this._Service_Received = value;
                    }
                }
            }

             [Column(Storage = "Service_Visit_No")]
            public System.Nullable<int> Service_Visit_No
            {
                get
                {
                    return this._Service_Visit_No;
                }
                set
                {
                    if ((this._Service_Visit_No != value))
                    {
                        this._Service_Visit_No = value;
                    }
                }
            }

             [Column(Storage = "Site_ID")]
            public System.Nullable<int> Site_ID
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

             [Column(Storage = "SLA_Contract_ID")]
            public System.Nullable<int> SLA_Contract_ID
            {
                get
                {
                    return this._SLA_Contract_ID;
                }
                set
                {
                    if ((this._SLA_Contract_ID != value))
                    {
                        this._SLA_Contract_ID = value;
                    }
                }
            }

             [Column(Storage = "Service_Received_Staff_ID")]
            public System.Nullable<int> Service_Received_Staff_ID
            {
                get
                {
                    return this._Service_Received_Staff_ID;
                }
                set
                {
                    if ((this._Service_Received_Staff_ID != value))
                    {
                        this._Service_Received_Staff_ID = value;
                    }
                }
            }

             [Column(Storage = "Service_Issued_By_Staff_ID")]
            public System.Nullable<int> Service_Issued_By_Staff_ID
            {
                get
                {
                    return this._Service_Issued_By_Staff_ID;
                }
                set
                {
                    if ((this._Service_Issued_By_Staff_ID != value))
                    {
                        this._Service_Issued_By_Staff_ID = value;
                    }
                }
            }

             [Column(Storage = "Service_Cleared_Staff_ID")]
            public System.Nullable<int> Service_Cleared_Staff_ID
            {
                get
                {
                    return this._Service_Cleared_Staff_ID;
                }
                set
                {
                    if ((this._Service_Cleared_Staff_ID != value))
                    {
                        this._Service_Cleared_Staff_ID = value;
                    }
                }
            }
        }

        public partial class ServiceNotesDisplayEntity
        {

            private string _Staff_Name;

            private string _Service_Notes_Notes;

            private string _Service_Notes_Date;

            private int _Service_Notes_ID;

            public ServiceNotesDisplayEntity()
            {
            }

            public string Staff_Name
            {
                get
                {
                    return this._Staff_Name;
                }
                set
                {
                    if ((this._Staff_Name != value))
                    {
                        this._Staff_Name = value;
                    }
                }
            }

            public string Service_Notes_Notes
            {
                get
                {
                    return this._Service_Notes_Notes;
                }
                set
                {
                    if ((this._Service_Notes_Notes != value))
                    {
                        this._Service_Notes_Notes = value;
                    }
                }
            }

            public string Service_Notes_Date
            {
                get
                {
                    return this._Service_Notes_Date;
                }
                set
                {
                    if ((this._Service_Notes_Date != value))
                    {
                        this._Service_Notes_Date = value;
                    }
                }
            }

            public int Service_Notes_ID
            {
                get
                {
                    return this._Service_Notes_ID;
                }
                set
                {
                    if ((this._Service_Notes_ID != value))
                    {
                        this._Service_Notes_ID = value;
                    }
                }
            }
        }

    }

