using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;
using System.Data.Linq;
using System.Reflection;

namespace BMC.EnterpriseDataAccess
{
    public partial class EnterpriseDataContext
    {
        [Function(Name = "dbo.rsp_GetCallGroupDescription")]
        public ISingleResult<rsp_GetCallGroupDescription> LoadCallGroupDescription()
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
            return ((ISingleResult<rsp_GetCallGroupDescription>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetCallRemedyDescription")]
        public ISingleResult<rsp_GetCallRemedyDescription> LoadCallRemedyDescription()
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
            return ((ISingleResult<rsp_GetCallRemedyDescription>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetsiteNames")]
        public ISingleResult<rsp_GetsiteNamesResult> GetsiteNamesForservices([Parameter(Name = "Site_ID", DbType = "Int")] System.Nullable<int> site_ID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), site_ID);
            return ((ISingleResult<rsp_GetsiteNamesResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetDepotNamesForServiceCall")]
        public ISingleResult<rsp_GetDepotNamesResult> GetDepotNamesForservices()
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
            return ((ISingleResult<rsp_GetDepotNamesResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetEngineerNames")]
        public ISingleResult<rsp_GetEngineerNamesResult> GetEngineerNamesForservices([Parameter(Name = "Staff_ID", DbType = "Int")] System.Nullable<int> staff_ID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())),staff_ID);
            return ((ISingleResult<rsp_GetEngineerNamesResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetSubCompanyNames")]
        public ISingleResult<rsp_GetSubCompanyNamesResult> GetSubCompanyNamesForservices()
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
            return ((ISingleResult<rsp_GetSubCompanyNamesResult>)(result.ReturnValue));
        }
  
        [Function(Name = "dbo.rsp_GetServiceCurrentCallDetails")]
        public ISingleResult<rsp_GetServiceCurrentCallDetailsResult> GetServiceCurrentCallDetails(
            [Parameter(Name = "CallStatusID", DbType = "Int")] System.Nullable<int> callStatusID, 
            [Parameter(Name = "CallGroupID", DbType = "Int")] System.Nullable<int> callGroupID, 
            [Parameter(Name = "DepotIDList", DbType = "VarChar(MAX)")] string depotIDList, 
            [Parameter(Name = "StaffIDList", DbType = "VarChar(MAX)")] string staffIDList, 
            [Parameter(Name = "SiteIDList", DbType = "VarChar(MAX)")] string siteIDList,
            [Parameter(Name = "SubCompanyID", DbType = "Int")] System.Nullable<int> subCompanyID, 
            [Parameter(Name = "JobID", DbType = "Int")] System.Nullable<int> jobID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), callStatusID, callGroupID, depotIDList, staffIDList, siteIDList, subCompanyID, jobID);
            return ((ISingleResult<rsp_GetServiceCurrentCallDetailsResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetServiceClosedCallDetails")]
        public ISingleResult<rsp_GetServiceClosedCallDetailsResult> GetServiceClosedCallDetails([Parameter(Name =  "StartDate", DbType = "DateTime")] System.Nullable<System.DateTime> startDate, [Parameter(Name =  "EndDate", DbType = "DateTime")] System.Nullable<System.DateTime> endDate, [Parameter(Name =  "CallRemedyID", DbType = "Int")] System.Nullable<int> callRemedyID, [Parameter(Name =  "DepotIDList", DbType = "VarChar(MAX)")] string depotIDList, [Parameter(Name =  "StaffIDList", DbType = "VarChar(MAX)")] string staffIDList, [Parameter(Name =  "SiteIDList", DbType = "VarChar(MAX)")] string siteIDList, [Parameter(Name =  "SubCompanyID", DbType = "Int")] System.Nullable<int> subCompanyID, [Parameter(Name =  "JobID", DbType = "Int")] System.Nullable<int> jobID, [Parameter(Name =  "MachineStockNo", DbType = "VarChar(50)")] string machineStockNo)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), startDate, endDate, callRemedyID, depotIDList, staffIDList, siteIDList, subCompanyID, jobID, machineStockNo);
            return ((ISingleResult<rsp_GetServiceClosedCallDetailsResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetSiteDetailsForServiceCall")]
        public ISingleResult<rsp_GetSiteDetailsForServiceCallResult> GetSiteDetailsForServiceCall([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "SiteId", DbType = "Int")] System.Nullable<int> siteId)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), siteId);
            return ((ISingleResult<rsp_GetSiteDetailsForServiceCallResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetEngineerNamesBySiteID")]
        public ISingleResult<rsp_GetEngineerNamesBySiteIDResult> GetEngineerNamesBySiteID([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Site_ID", DbType = "Int")] System.Nullable<int> site_ID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), site_ID);
            return ((ISingleResult<rsp_GetEngineerNamesBySiteIDResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetMachineTypesForServiceCall")]
        public ISingleResult<rsp_GetMachineTypesForServiceCallResult> GetMachineTypesForServiceCall([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "IsNewCall", DbType = "Bit")] System.Nullable<bool> isNewCall)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), isNewCall);
            return ((ISingleResult<rsp_GetMachineTypesForServiceCallResult>)(result.ReturnValue));
        }

		[Function(Name = "dbo.rsp_GetServiceCurrentCallDetailsByServiceID")]
        public ISingleResult<rsp_GetServiceCurrentCallDetailsByServiceIDResult> GetServiceCurrentCallDetailsByServiceID([Parameter(Name = "Service_ID", DbType = "Int")] System.Nullable<int> service_ID, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "IsCallClosed", DbType = "Bit")] System.Nullable<bool> isCallClosed)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), service_ID, isCallClosed);
            return ((ISingleResult<rsp_GetServiceCurrentCallDetailsByServiceIDResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetServiceNotesByJobNo")]
        public ISingleResult<rsp_GetServiceNotesByJobNoResult> GetServiceNotesByJobNo([Parameter(Name = "Service_Allocated_Job_No", DbType = "Int")] System.Nullable<int> service_Allocated_Job_No)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), service_Allocated_Job_No);
            return ((ISingleResult<rsp_GetServiceNotesByJobNoResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetMachineNamesForServiceCall")]
        public ISingleResult<rsp_GetMachineNamesForServiceCallResult> GetMachineNamesForServiceCall([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "IsShowHistory", DbType = "Bit")] System.Nullable<bool> isShowHistory, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "SiteID", DbType = "Int")] System.Nullable<int> siteID, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "MachineTypeID", DbType = "Int")] System.Nullable<int> machineTypeID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), isShowHistory, siteID, machineTypeID);
            return ((ISingleResult<rsp_GetMachineNamesForServiceCallResult>)(result.ReturnValue));
        }

        //[Function(Name = "dbo.usp_InsertOrUpdateServiceCall")]
        //public int usp_InsertOrUpdateServiceCall(
        //            [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "ServiceId", DbType = "Int")] System.Nullable<int> serviceId,
        //            [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "SiteID", DbType = "Int")] System.Nullable<int> siteID,
        //            [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "CallSourceID", DbType = "Int")] System.Nullable<int> callSourceID,
        //            [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "CallFaultID", DbType = "Int")] System.Nullable<int> callFaultID,
        //            [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "CallGroupID", DbType = "Int")] System.Nullable<int> callGroupID,
        //            [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "CallRemedyID", DbType = "Int")] System.Nullable<int> callRemedyID,
        //            [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "MachineTypeID", DbType = "VarChar(50)")] string machineTypeID,
        //            [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "InstallationID", DbType = "Int")] System.Nullable<int> installationID,
        //            [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "ServiceVisitNo", DbType = "Int")] System.Nullable<int> serviceVisitNo,
        //            [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "ServiceReceivedStaffID", DbType = "Int")] System.Nullable<int> serviceReceivedStaffID,
        //            [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "ServiceIssuedToStaffID", DbType = "Int")] System.Nullable<int> serviceIssuedToStaffID,
        //            [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "ServiceIssuedByStaffID", DbType = "Int")] System.Nullable<int> serviceIssuedByStaffID,
        //            [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "CallStatusID", DbType = "Int")] System.Nullable<int> callStatusID,
        //            [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "ServiceIssued", DbType = "DateTime")] System.Nullable<System.DateTime> serviceIssued,
        //            [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "ServiceReceived", DbType = "DateTime")] System.Nullable<System.DateTime> serviceReceived,
        //            [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "CallFaultAdditionalNotes", DbType = "NVarChar(MAX)")] string callFaultAdditionalNotes,
        //            [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "CallRemedyAdditionalDescription", DbType = "NVarChar(MAX)")] string callRemedyAdditionalDescription,
        //            [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "ServiceAllocatedJobNo", DbType = "Int")] System.Nullable<int> serviceAllocatedJobNo,
        //            [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "IsCallClosed", DbType = "Bit")] System.Nullable<bool> isCallClosed,
        //            [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "ServiceArrivedAtSite", DbType = "DateTime")] System.Nullable<System.DateTime> serviceArrivedAtSite,
        //            [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "ServiceAcknowledged", DbType = "DateTime")] System.Nullable<System.DateTime> serviceAcknowledged,
        //                [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "ServiceCleared", DbType = "DateTime")] System.Nullable<System.DateTime> serviceCleared)
        //{
        //    IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), serviceId, siteID, callSourceID, callFaultID, callGroupID, callRemedyID, machineTypeID, installationID, serviceVisitNo, serviceReceivedStaffID, serviceIssuedToStaffID, serviceIssuedByStaffID, callStatusID, serviceIssued, serviceReceived, callFaultAdditionalNotes, callRemedyAdditionalDescription, serviceAllocatedJobNo, isCallClosed, serviceArrivedAtSite, serviceAcknowledged, serviceCleared);
        //    return ((int)(result.ReturnValue));
        //}

        [Function(Name = "dbo.usp_InsertOrUpdateServiceCall")]
        public int InsertOrUpdateServiceCall(
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "ServiceId", DbType = "Int")] System.Nullable<int> serviceId,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "SiteID", DbType = "Int")] System.Nullable<int> siteID,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "CallSourceID", DbType = "Int")] System.Nullable<int> callSourceID,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "CallFaultID", DbType = "Int")] System.Nullable<int> callFaultID,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "CallGroupID", DbType = "Int")] System.Nullable<int> callGroupID,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "CallRemedyID", DbType = "Int")] System.Nullable<int> callRemedyID,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "MachineTypeID", DbType = "VarChar(50)")] string machineTypeID,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "InstallationID", DbType = "Int")] System.Nullable<int> installationID,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "ServiceVisitNo", DbType = "Int")] System.Nullable<int> serviceVisitNo,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "ServiceReceivedStaffID", DbType = "Int")] System.Nullable<int> serviceReceivedStaffID,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "ServiceIssuedToStaffID", DbType = "Int")] System.Nullable<int> serviceIssuedToStaffID,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "ServiceIssuedByStaffID", DbType = "Int")] System.Nullable<int> serviceIssuedByStaffID,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "CallStatusID", DbType = "Int")] System.Nullable<int> callStatusID,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "CallFaultAdditionalNotes", DbType = "NVarChar(MAX)")] string callFaultAdditionalNotes,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "CallRemedyAdditionalDescription", DbType = "NVarChar(MAX)")] string callRemedyAdditionalDescription,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "ServiceAllocatedJobNo", DbType = "Int")] System.Nullable<int> serviceAllocatedJobNo,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "IsCallClosed", DbType = "Bit")] System.Nullable<bool> isCallClosed,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "ServiceReceived", DbType = "DateTime")] System.Nullable<System.DateTime> serviceReceived,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "ServiceIssued", DbType = "DateTime")] System.Nullable<System.DateTime> serviceIssued,                    
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "ServiceAcknowledged", DbType = "DateTime")] System.Nullable<System.DateTime> serviceAcknowledged,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "ServiceArrivedAtSite", DbType = "DateTime")] System.Nullable<System.DateTime> serviceArrivedAtSite,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "ServiceCleared", DbType = "DateTime")] System.Nullable<System.DateTime> serviceCleared,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "ServiceClearedStaffId", DbType = "Int")] System.Nullable<int> serviceClearedStaffId)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), serviceId, siteID, callSourceID, callFaultID, callGroupID, callRemedyID, machineTypeID, installationID, serviceVisitNo, serviceReceivedStaffID, serviceIssuedToStaffID, serviceIssuedByStaffID, callStatusID, callFaultAdditionalNotes, callRemedyAdditionalDescription, serviceAllocatedJobNo, isCallClosed, serviceReceived, serviceIssued, serviceAcknowledged, serviceArrivedAtSite, serviceCleared, serviceClearedStaffId);
            return ((int)(result.ReturnValue));
        }

        [Function(Name = "dbo.usp_InsertServiceNotesDetails")]
        public int InsertServiceNotes([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Service_ID", DbType = "Int")] System.Nullable<int> service_ID, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Staff_ID", DbType = "Int")] System.Nullable<int> staff_ID, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Engineer_ID", DbType = "Int")] System.Nullable<int> engineer_ID, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Subject", DbType = "VarChar(32)")] string subject, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Notes", DbType = "VarChar(255)")] string notes, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Service_Notes_Date", DbType = "DateTime")] System.Nullable<System.DateTime> service_Notes_Date, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Service_Notes_In_Out", DbType = "Int")] System.Nullable<int> service_Notes_In_Out, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Service_Closed_Id", DbType = "Int")] System.Nullable<int> service_Closed_Id)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), service_ID, staff_ID, engineer_ID, subject, notes, service_Notes_Date, service_Notes_In_Out, service_Closed_Id);
            return ((int)(result.ReturnValue));
        }

        public partial class rsp_GetStaffSCResult
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

            public rsp_GetStaffSCResult()
            {
            }

            [Column(Storage = "_Staff_ID", DbType = "Int NOT NULL")]
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

            [Column(Storage = "_Operator_ID", DbType = "Int")]
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
                        this._Computer_Build_ID = value;
                    }
                }
            }

            [Column(Storage = "_User_Group_ID", DbType = "Int")]
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

            [Column(Storage = "_Staff_First_Name", DbType = "VarChar(50)")]
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

            [Column(Storage = "_Staff_Last_Name", DbType = "VarChar(50)")]
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

            [Column(Storage = "_Staff_Title", DbType = "VarChar(5)")]
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

            [Column(Storage = "_Staff_Address", DbType = "NText", UpdateCheck = UpdateCheck.Never)]
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

            [Column(Storage = "_Staff_Postcode", DbType = "VarChar(10)")]
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

            [Column(Storage = "_Staff_Phone_No", DbType = "VarChar(15)")]
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

            [Column(Storage = "_Staff_Extension_No", DbType = "VarChar(15)")]
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

            [Column(Storage = "_Staff_Mobile_No", DbType = "VarChar(15)")]
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

            [Column(Storage = "_Staff_Job_Title", DbType = "VarChar(50)")]
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

            [Column(Storage = "_Staff_Department", DbType = "VarChar(50)")]
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

            [Column(Storage = "_Staff_IsACollector", DbType = "Bit")]
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

            [Column(Storage = "_Staff_IsAnEngineer", DbType = "Bit")]
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

            [Column(Storage = "_Staff_IsARepresentative", DbType = "Bit")]
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

            [Column(Storage = "_Staff_IsAStockController", DbType = "Bit")]
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

            [Column(Storage = "_Staff_Start_Date", DbType = "VarChar(30)")]
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

            [Column(Storage = "_Staff_End_Date", DbType = "VarChar(30)")]
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

            [Column(Storage = "_Staff_Modem_Phone_No", DbType = "VarChar(15)")]
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

            [Column(Storage = "_Staff_Remote_Inbox", DbType = "VarChar(100)")]
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

            [Column(Storage = "_Staff_Remote_Outbox", DbType = "VarChar(100)")]
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

            [Column(Storage = "_Staff_Username", DbType = "VarChar(50)")]
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

            [Column(Storage = "_Staff_Password", DbType = "VarChar(50)")]
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

            [Column(Storage = "_Staff_Analysis_D_R_1_Back_Neg", DbType = "Int")]
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

            [Column(Storage = "_Staff_Analysis_D_R_1_Front_Neg", DbType = "Int")]
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

            [Column(Storage = "_Staff_Analysis_D_R_1_Back_Pos", DbType = "Int")]
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

            [Column(Storage = "_Staff_Analysis_D_R_1_Front_Pos", DbType = "Int")]
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

            [Column(Storage = "_Staff_Analysis_D_R_2_Back_Neg", DbType = "Int")]
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

            [Column(Storage = "_Staff_Analysis_D_R_2_Front_Neg", DbType = "Int")]
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

            [Column(Storage = "_Staff_Analysis_D_R_2_Back_Pos", DbType = "Int")]
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

            [Column(Storage = "_Staff_Analysis_D_R_2_Front_Pos", DbType = "Int")]
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

            [Column(Storage = "_Staff_Analysis_D_R_3_Back_Neg", DbType = "Int")]
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

            [Column(Storage = "_Staff_Analysis_D_R_3_Front_Neg", DbType = "Int")]
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

            [Column(Storage = "_Staff_Analysis_D_R_3_Back_Pos", DbType = "Int")]
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

            [Column(Storage = "_Staff_Analysis_D_R_3_Front_Pos", DbType = "Int")]
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

            [Column(Storage = "_Staff_Analysis_D_R_4_Back_Neg", DbType = "Int")]
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

            [Column(Storage = "_Staff_Analysis_D_R_4_Front_Neg", DbType = "Int")]
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

            [Column(Storage = "_Staff_Analysis_D_R_4_Back_Pos", DbType = "Int")]
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

            [Column(Storage = "_Staff_Analysis_D_R_4_Front_Pos", DbType = "Int")]
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

            [Column(Storage = "_Staff_Analysis_D_R_5_Back_Neg", DbType = "Int")]
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

            [Column(Storage = "_Staff_Analysis_D_R_5_Front_Neg", DbType = "Int")]
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

            [Column(Storage = "_Staff_Analysis_D_R_5_Back_Pos", DbType = "Int")]
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

            [Column(Storage = "_Staff_Analysis_D_R_5_Front_Pos", DbType = "Int")]
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

            [Column(Storage = "_Staff_Tree_Checked_Back", DbType = "Int")]
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

            [Column(Storage = "_Staff_Tree_Checked_Front", DbType = "Int")]
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

            [Column(Storage = "_Staff_Tree_UnChecked_Back", DbType = "Int")]
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

            [Column(Storage = "_Staff_Tree_UnChecked_Front", DbType = "Int")]
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

            [Column(Storage = "_Staff_Tree_Mixed_Back", DbType = "Int")]
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

            [Column(Storage = "_Staff_Tree_Mixed_Front", DbType = "Int")]
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

            [Column(Storage = "_Staff_MAN_Number", DbType = "VarChar(50)")]
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
                        this._Depot_ID = value;
                    }
                }
            }

            [Column(Storage = "_Staff_GPS_Location", DbType = "VarChar(50)")]
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
                        this._Service_Area_ID = value;
                    }
                }
            }

            [Column(Storage = "_Supplier_ID", DbType = "Int")]
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

            [Column(Storage = "_Staff_Personel_No", DbType = "VarChar(10)")]
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

            [Column(Storage = "_Staff_Terminated", DbType = "Bit")]
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

            [Column(Storage = "_Staff_Collector_Current_Version", DbType = "VarChar(50)")]
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

            [Column(Storage = "_Staff_Collector_Uploaded_Version", DbType = "VarChar(50)")]
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

            [Column(Storage = "_Staff_Engineer_Current_Version", DbType = "VarChar(50)")]
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

            [Column(Storage = "_Staff_Engineer_Uploaded_Version", DbType = "VarChar(50)")]
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

            [Column(Storage = "_Staff_Notes", DbType = "VarChar(255)")]
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

            [Column(Storage = "_Staff_Last_Comms_Test_Sent", DbType = "VarChar(20)")]
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

            [Column(Storage = "_Staff_Last_Comms_Test_Received", DbType = "VarChar(20)")]
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

            [Column(Storage = "_Email_Address", DbType = "VarChar(100)")]
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

            [Column(Storage = "_UserTableID", DbType = "Int")]
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
        }

        public partial class rsp_GetDepotSCResult
        {

            private int _Service_Area_ID;

            private System.Nullable<int> _Depot_ID;

            private string _Service_Area_Name;

            private string _Service_Area_Description;

            private string _Service_Area_Notes;

            public rsp_GetDepotSCResult()
            {
            }

            [Column(Storage = "_Service_Area_ID", DbType = "Int NOT NULL")]
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
                        this._Depot_ID = value;
                    }
                }
            }

            [Column(Storage = "_Service_Area_Name", DbType = "VarChar(50)")]
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

            [Column(Storage = "_Service_Area_Description", DbType = "NText", UpdateCheck = UpdateCheck.Never)]
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

            [Column(Storage = "_Service_Area_Notes", DbType = "NText", UpdateCheck = UpdateCheck.Never)]
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

        public partial class rsp_GetSiteSCResult
        {

            private string _Site_Name;

            private int _Site_ID;

            private int _Sub_Company_ID;

            private string _Sub_Company_Name;

            public rsp_GetSiteSCResult()
            {
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
                        this._Site_Name = value;
                    }
                }
            }

            [Column(Storage = "_Site_ID", DbType = "Int NOT NULL")]
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

            [Column(Storage = "_Sub_Company_ID", DbType = "Int NOT NULL")]
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
                        this._Sub_Company_Name = value;
                    }
                }
            }
        }

        //rajkumar
        public partial class rsp_GetCallGroupDescription
        {

            private int _Call_Group_ID;

            private string _Call_Group_Description;

            public rsp_GetCallGroupDescription()
            {
            }

            [Column(Storage = "_Call_Group_ID", DbType = "Int NOT NULL")]
            public int Call_Group_ID
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

            [Column(Storage = "_Call_Group_Description", DbType = "VarChar(50)")]
            public string Call_Group_Description
            {
                get
                {
                    return this._Call_Group_Description;
                }
                set
                {
                    if ((this._Call_Group_Description != value))
                    {
                        this._Call_Group_Description = value;
                    }
                }
            }
        }

        public partial class rsp_LoadFaultDescriptionResult
        {

            private int _Call_Fault_ID;

            private string _Call_Fault_Description;

            public rsp_LoadFaultDescriptionResult()
            {
            }

            [Column(Storage = "_Call_Fault_ID", DbType = "Int NOT NULL")]
            public int Call_Fault_ID
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

            [Column(Storage = "_Call_Fault_Description", DbType = "VarChar(50)")]
            public string Call_Fault_Description
            {
                get
                {
                    return this._Call_Fault_Description;
                }
                set
                {
                    if ((this._Call_Fault_Description != value))
                    {
                        this._Call_Fault_Description = value;
                    }
                }
            }
        }

        public partial class rsp_GetCallRemedyDescription
        {

            private int _Call_Remedy_ID;

            private string _Call_Remedy_Description;

            public rsp_GetCallRemedyDescription()
            {
            }

            [Column(Storage = "_Call_Remedy_ID", DbType = "Int NOT NULL")]
            public int Call_Remedy_ID
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

            [Column(Storage = "_Call_Remedy_Description", DbType = "VarChar(50)")]
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
        }

        public partial class rsp_LoadCallSourceDescriptionResult
        {

            private int _Call_Source_ID;

            private string _Call_Source_Description;

            public rsp_LoadCallSourceDescriptionResult()
            {
            }

            [Column(Storage = "_Call_Source_ID", DbType = "Int NOT NULL")]
            public int Call_Source_ID
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

            [Column(Storage = "_Call_Source_Description", DbType = "VarChar(30)")]
            public string Call_Source_Description
            {
                get
                {
                    return this._Call_Source_Description;
                }
                set
                {
                    if ((this._Call_Source_Description != value))
                    {
                        this._Call_Source_Description = value;
                    }
                }
            }
        }

        public partial class rsp_GetSAdminMachineTypeResult
        {

            private string _Machine_Type_Code;

            private string _Machine_Type_Description;

            private System.Nullable<int> _Machine_Type_ID;

            public rsp_GetSAdminMachineTypeResult()
            {
            }

            [Column(Storage = "_Machine_Type_Code", DbType = "VarChar(50)")]
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

            [Column(Storage = "_Machine_Type_Description", DbType = "VarChar(50)")]
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

            [Column(Storage = "_Machine_Type_ID", DbType = "Int")]
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
    }
    public partial class rsp_GetServiceDetailsResult
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

        public rsp_GetServiceDetailsResult()
        {
        }

        [Column(Storage = "_Service_ID", DbType = "Int NOT NULL")]
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

        [Column(Storage = "_Call_Source_ID", DbType = "Int")]
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

        [Column(Storage = "_Call_Fault_ID", DbType = "Int")]
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

        [Column(Storage = "_Call_Status_ID", DbType = "Int")]
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

        [Column(Storage = "_Call_Remedy_ID", DbType = "Int")]
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

        [Column(Storage = "_Call_Remedy_Additional_Description", DbType = "NText", UpdateCheck = UpdateCheck.Never)]
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

        [Column(Storage = "_Site_ID", DbType = "Int")]
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

        [Column(Storage = "_Bar_Position_ID", DbType = "Int")]
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

        [Column(Storage = "_Zone_ID", DbType = "Int")]
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

        [Column(Storage = "_Machine_ID", DbType = "Int")]
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

        [Column(Storage = "_Machine_Type_ID", DbType = "VarChar(50)")]
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

        [Column(Storage = "_Installation_ID", DbType = "Int")]
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

        [Column(Storage = "_SLA_Contract_ID", DbType = "Int")]
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

        [Column(Storage = "_Service_DownTime", DbType = "Int")]
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

        [Column(Storage = "_Service_Received", DbType = "VarChar(20)")]
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

        [Column(Storage = "_Service_Received_Staff_ID", DbType = "Int")]
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

        [Column(Storage = "_Service_Issued", DbType = "VarChar(20)")]
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

        [Column(Storage = "_Service_Issued_By_Staff_ID", DbType = "Int")]
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

        [Column(Storage = "_Service_Issued_To_Staff_ID", DbType = "Int")]
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

        [Column(Storage = "_Service_Arrived_At_Site", DbType = "VarChar(20)")]
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

        [Column(Storage = "_Service_Cleared", DbType = "VarChar(20)")]
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

        [Column(Storage = "_Service_Cleared_Staff_ID", DbType = "Int")]
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

        [Column(Storage = "_Service_Closed_ID", DbType = "Int")]
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

        [Column(Storage = "_Service_Message_ID", DbType = "Int")]
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

        [Column(Storage = "_Service_Sequence_No", DbType = "Int")]
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

        [Column(Storage = "_Call_Group_ID", DbType = "Int")]
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

        [Column(Storage = "_Call_Fault_Additional_Notes", DbType = "NText", UpdateCheck = UpdateCheck.Never)]
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

        [Column(Storage = "_Service_Additional_Work_Req", DbType = "Bit")]
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

        [Column(Storage = "_Service_Alert_Priority_Site", DbType = "Bit")]
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

        [Column(Storage = "_Service_Alert_Priority_Machine", DbType = "Bit")]
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

        [Column(Storage = "_Service_Visit_No", DbType = "Int")]
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

        [Column(Storage = "_Service_Allocated_Job_No", DbType = "Int")]
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

        [Column(Storage = "_Service_Acknowledged", DbType = "VarChar(50)")]
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

        [Column(Storage = "_Service_GMU_Type_ID", DbType = "Int")]
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

        [Column(Storage = "_Service_GMU_Source_ID", DbType = "Int")]
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

        [Column(Storage = "_IsEscalated", DbType = "Bit")]
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

    public partial class rsp_GetSLA_ContractResult
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

        public rsp_GetSLA_ContractResult()
        {
        }

        [Column(Storage = "_SLA_Contract_ID", DbType = "Int NOT NULL")]
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

        [Column(Storage = "_SLA_Contract_Description", DbType = "VarChar(50)")]
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

        [Column(Storage = "_SLA_Contract_Response", DbType = "Int")]
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

        [Column(Storage = "_SLA_Contract_Site_Days", DbType = "Int")]
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

        [Column(Storage = "_SLA_Contract_Site_Calls", DbType = "Int")]
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

        [Column(Storage = "_SLA_Contract_Machine_Days", DbType = "Int")]
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

        [Column(Storage = "_SLA_Contract_Machine_Calls", DbType = "Int")]
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

        [Column(Storage = "_SLA_Contract_Order", DbType = "Int")]
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

        [Column(Storage = "_SLA_Contract_Default", DbType = "Bit")]
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

        [Column(Storage = "_SLA_ID", DbType = "Int")]
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

    public partial class rsp_GetsiteNamesResult
    {

        private int _Site_ID;

        private string _Site_Name;

        public rsp_GetsiteNamesResult()
        {
        }

        [Column(Storage = "_Site_ID", DbType = "Int NOT NULL")]
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
                    this._Site_Name = value;
                }
            }
        }
    }

    public partial class rsp_GetsiteserviceDetailResult
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

        public rsp_GetsiteserviceDetailResult()
        {
        }

        [Column(Storage = "_Standard_Opening_Hours_Description", DbType = "VarChar(50)")]
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

        [Column(Storage = "_Bar_Position_Name", DbType = "VarChar(50)")]
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

        [Column(Storage = "_Machine_Name", DbType = "VarChar(50)")]
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
                    this._Site_Name = value;
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
                    this._Site_Code = value;
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
                    this._Site_Address_1 = value;
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
                    this._Site_Address_2 = value;
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
                    this._Site_Address_3 = value;
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
                    this._Site_Address_4 = value;
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
                    this._Site_Address_5 = value;
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
                    this._Site_Postcode = value;
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
                    this._Site_Manager = value;
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
                    this._Site_Phone_No = value;
                }
            }
        }

        [Column(Storage = "_Machine_Stock_No", DbType = "VarChar(50)")]
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

        [Column(Storage = "_Installation_ID", DbType = "Int")]
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

        [Column(Storage = "_Machine_Name1", DbType = "VarChar(50)")]
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

        [Column(Storage = "_Bar_Position_Name1", DbType = "VarChar(50)")]
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

        [Column(Storage = "_Depot_Name", DbType = "VarChar(50)")]
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

        [Column(Storage = "_Service_Area_Name", DbType = "VarChar(50)")]
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
                    this._Sub_Company_Name = value;
                }
            }
        }
    }

    public partial class rsp_GetDepotNamesResult
    {

        private int _Depot_ID;

        private string _Depot_Name;

        public rsp_GetDepotNamesResult()
        {
        }

        [Column(Storage = "_Depot_ID", DbType = "Int NOT NULL")]
        public int Depot_ID
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

        [Column(Storage = "_Depot_Name", DbType = "VarChar(50)")]
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
    }

    public partial class rsp_GetEngineerNamesResult
    {

        private int _Staff_ID;

        private string _Staff_Name;

        public rsp_GetEngineerNamesResult()
        {
        }

        [Column(Storage = "_Staff_ID", DbType = "Int NOT NULL")]
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

        [Column(Storage = "_Staff_Name", DbType = "VarChar(100)")]
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
    }

    public partial class rsp_GetSubCompanyNamesResult
    {

        private int _Sub_Company_ID;

        private string _Sub_Company_Name;

        public rsp_GetSubCompanyNamesResult()
        {
        }

        [Column(Storage = "_Sub_Company_ID", DbType = "Int NOT NULL")]
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
                    this._Sub_Company_Name = value;
                }
            }
        }
    }

    public partial class rsp_GetMachineDetailScallResult
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

        private string _Machine_Name1;

        private string _Machine_Stock_No;

        private string _Machine_Manufacturers_Serial_No;

        private string _Bar_Position_Name1;

        private string _Depot_Name;

        private string _Service_Area_Name;

        private string _Sub_Company_Name;

        public rsp_GetMachineDetailScallResult()
        {
        }

        [Column(Storage = "_Bar_Position_Name", DbType = "VarChar(50)")]
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

        [Column(Storage = "_Machine_Name", DbType = "VarChar(50)")]
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
                    this._Site_Name = value;
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
                    this._Site_Code = value;
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
                    this._Site_Address_1 = value;
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
                    this._Site_Address_2 = value;
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
                    this._Site_Address_3 = value;
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
                    this._Site_Address_4 = value;
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
                    this._Site_Address_5 = value;
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
                    this._Site_Postcode = value;
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
                    this._Site_Manager = value;
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
                    this._Site_Phone_No = value;
                }
            }
        }

        [Column(Storage = "_Installation_ID", DbType = "Int")]
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

        [Column(Storage = "_Machine_Name1", DbType = "VarChar(50)")]
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

        [Column(Storage = "_Machine_Stock_No", DbType = "VarChar(50)")]
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

        [Column(Storage = "_Machine_Manufacturers_Serial_No", DbType = "VarChar(50)")]
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

        [Column(Storage = "_Bar_Position_Name1", DbType = "VarChar(50)")]
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

        [Column(Storage = "_Depot_Name", DbType = "VarChar(50)")]
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

        [Column(Storage = "_Service_Area_Name", DbType = "VarChar(50)")]
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
                    this._Sub_Company_Name = value;
                }
            }
        }
    }

    public partial class rsp_GetAllEngineerResult
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

        public rsp_GetAllEngineerResult()
        {
        }

        [Column(Storage = "_Staff_ID", DbType = "Int NOT NULL")]
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

        [Column(Storage = "_Operator_ID", DbType = "Int")]
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
                    this._Computer_Build_ID = value;
                }
            }
        }

        [Column(Storage = "_User_Group_ID", DbType = "Int")]
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

        [Column(Storage = "_Staff_First_Name", DbType = "VarChar(50)")]
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

        [Column(Storage = "_Staff_Last_Name", DbType = "VarChar(50)")]
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

        [Column(Storage = "_Staff_Title", DbType = "VarChar(5)")]
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

        [Column(Storage = "_Staff_Address", DbType = "NText", UpdateCheck = UpdateCheck.Never)]
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

        [Column(Storage = "_Staff_Postcode", DbType = "VarChar(10)")]
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

        [Column(Storage = "_Staff_Phone_No", DbType = "VarChar(15)")]
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

        [Column(Storage = "_Staff_Extension_No", DbType = "VarChar(15)")]
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

        [Column(Storage = "_Staff_Mobile_No", DbType = "VarChar(15)")]
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

        [Column(Storage = "_Staff_Job_Title", DbType = "VarChar(50)")]
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

        [Column(Storage = "_Staff_Department", DbType = "VarChar(50)")]
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

        [Column(Storage = "_Staff_IsACollector", DbType = "Bit")]
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

        [Column(Storage = "_Staff_IsAnEngineer", DbType = "Bit")]
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

        [Column(Storage = "_Staff_IsARepresentative", DbType = "Bit")]
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

        [Column(Storage = "_Staff_IsAStockController", DbType = "Bit")]
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

        [Column(Storage = "_Staff_Start_Date", DbType = "VarChar(30)")]
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

        [Column(Storage = "_Staff_End_Date", DbType = "VarChar(30)")]
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

        [Column(Storage = "_Staff_Modem_Phone_No", DbType = "VarChar(15)")]
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

        [Column(Storage = "_Staff_Remote_Inbox", DbType = "VarChar(100)")]
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

        [Column(Storage = "_Staff_Remote_Outbox", DbType = "VarChar(100)")]
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

        [Column(Storage = "_Staff_Username", DbType = "VarChar(50)")]
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

        [Column(Storage = "_Staff_Password", DbType = "VarChar(50)")]
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

        [Column(Storage = "_UserTableID", DbType = "Int")]
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

        [Column(Storage = "_Email_Address", DbType = "VarChar(100)")]
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

        [Column(Storage = "_Staff_Last_Comms_Test_Received", DbType = "VarChar(20)")]
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

        [Column(Storage = "_Staff_Last_Comms_Test_Sent", DbType = "VarChar(20)")]
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

        [Column(Storage = "_Staff_Notes", DbType = "VarChar(255)")]
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

        [Column(Storage = "_Staff_Engineer_Uploaded_Version", DbType = "VarChar(50)")]
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

        [Column(Storage = "_Staff_Engineer_Current_Version", DbType = "VarChar(50)")]
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

        [Column(Storage = "_Staff_Collector_Uploaded_Version", DbType = "VarChar(50)")]
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

        [Column(Storage = "_Staff_Collector_Current_Version", DbType = "VarChar(50)")]
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

        [Column(Storage = "_Staff_Terminated", DbType = "Bit")]
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

        [Column(Storage = "_Staff_Personel_No", DbType = "VarChar(10)")]
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

        [Column(Storage = "_Supplier_ID", DbType = "Int")]
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
                    this._Service_Area_ID = value;
                }
            }
        }

        [Column(Storage = "_Staff_GPS_Location", DbType = "VarChar(50)")]
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
                    this._Depot_ID = value;
                }
            }
        }

        [Column(Storage = "_Staff_MAN_Number", DbType = "VarChar(50)")]
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

        [Column(Storage = "_Staff_Tree_Mixed_Front", DbType = "Int")]
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

        [Column(Storage = "_Staff_Tree_Mixed_Back", DbType = "Int")]
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

        [Column(Storage = "_Staff_Tree_UnChecked_Front", DbType = "Int")]
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

        [Column(Storage = "_Staff_Tree_UnChecked_Back", DbType = "Int")]
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

        [Column(Storage = "_Staff_Tree_Checked_Front", DbType = "Int")]
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

        [Column(Storage = "_Staff_Tree_Checked_Back", DbType = "Int")]
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

        [Column(Storage = "_Staff_Analysis_D_R_5_Front_Pos", DbType = "Int")]
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

        [Column(Storage = "_Staff_Analysis_D_R_5_Back_Pos", DbType = "Int")]
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

        [Column(Storage = "_Staff_Analysis_D_R_5_Front_Neg", DbType = "Int")]
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

        [Column(Storage = "_Staff_Analysis_D_R_5_Back_Neg", DbType = "Int")]
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

        [Column(Storage = "_Staff_Analysis_D_R_4_Front_Pos", DbType = "Int")]
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

        [Column(Storage = "_Staff_Analysis_D_R_4_Back_Pos", DbType = "Int")]
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

        [Column(Storage = "_Staff_Analysis_D_R_4_Front_Neg", DbType = "Int")]
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

        [Column(Storage = "_Staff_Analysis_D_R_3_Front_Pos", DbType = "Int")]
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

        [Column(Storage = "_Staff_Analysis_D_R_4_Back_Neg", DbType = "Int")]
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

        [Column(Storage = "_Staff_Analysis_D_R_3_Back_Pos", DbType = "Int")]
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

        [Column(Storage = "_Staff_Analysis_D_R_3_Front_Neg", DbType = "Int")]
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

        [Column(Storage = "_Staff_Analysis_D_R_3_Back_Neg", DbType = "Int")]
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

        [Column(Storage = "_Staff_Analysis_D_R_2_Front_Pos", DbType = "Int")]
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

        [Column(Storage = "_Staff_Analysis_D_R_2_Back_Pos", DbType = "Int")]
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

        [Column(Storage = "_Staff_Analysis_D_R_2_Front_Neg", DbType = "Int")]
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

        [Column(Storage = "_Staff_Analysis_D_R_2_Back_Neg", DbType = "Int")]
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

        [Column(Storage = "_Staff_Analysis_D_R_1_Front_Pos", DbType = "Int")]
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

        [Column(Storage = "_Staff_Analysis_D_R_1_Back_Pos", DbType = "Int")]
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

        [Column(Storage = "_Staff_Analysis_D_R_1_Front_Neg", DbType = "Int")]
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

        [Column(Storage = "_Staff_Analysis_D_R_1_Back_Neg", DbType = "Int")]
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

    public partial class rsp_GetEngineerdetailSSResult
    {

        private int _Site_ID;

        private string _Staff_First_Name;

        private string _Staff_Last_Name;

        private System.Nullable<int> _Staff_ID;

        private System.Nullable<int> _Service_Area_ID;

        public rsp_GetEngineerdetailSSResult()
        {
        }

        [Column(Storage = "_Site_ID", DbType = "Int NOT NULL")]
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

        [Column(Storage = "_Staff_First_Name", DbType = "VarChar(50)")]
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

        [Column(Storage = "_Staff_Last_Name", DbType = "VarChar(50)")]
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
                    this._Staff_ID = value;
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
                    this._Service_Area_ID = value;
                }
            }
        }
    }

    public partial class rsp_SLA_ContractDetailasResult
    {

        private string _SLA_Contract_Description;

        private System.Nullable<int> _SLA_Contract_ID;

        public rsp_SLA_ContractDetailasResult()
        {
        }

        [Column(Storage = "_SLA_Contract_Description", DbType = "VarChar(50)")]
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

        [Column(Storage = "_SLA_Contract_ID", DbType = "Int")]
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

    public partial class rsp_ServiceCallFaultDetailsResult
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

        public rsp_ServiceCallFaultDetailsResult()
        {
        }

        [Column(Storage = "_Service_ID", DbType = "Int NOT NULL")]
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

        [Column(Storage = "_Call_Source_ID", DbType = "Int")]
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

        [Column(Storage = "_Call_Fault_ID", DbType = "Int")]
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

        [Column(Storage = "_Call_Status_ID", DbType = "Int")]
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

        [Column(Storage = "_Call_Remedy_ID", DbType = "Int")]
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

        [Column(Storage = "_Call_Remedy_Additional_Description", DbType = "NText", UpdateCheck = UpdateCheck.Never)]
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

        [Column(Storage = "_Site_ID", DbType = "Int")]
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

        [Column(Storage = "_Bar_Position_ID", DbType = "Int")]
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

        [Column(Storage = "_Zone_ID", DbType = "Int")]
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

        [Column(Storage = "_Machine_ID", DbType = "Int")]
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

        [Column(Storage = "_Machine_Type_ID", DbType = "VarChar(50)")]
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

        [Column(Storage = "_Installation_ID", DbType = "Int")]
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

        [Column(Storage = "_SLA_Contract_ID", DbType = "Int")]
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

        [Column(Storage = "_Service_DownTime", DbType = "Int")]
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

        [Column(Storage = "_Service_Received", DbType = "VarChar(20)")]
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

        [Column(Storage = "_Service_Received_Staff_ID", DbType = "Int")]
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

        [Column(Storage = "_Service_Issued", DbType = "VarChar(20)")]
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

        [Column(Storage = "_Service_Issued_By_Staff_ID", DbType = "Int")]
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

        [Column(Storage = "_Service_Issued_To_Staff_ID", DbType = "Int")]
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

        [Column(Storage = "_Service_Cleared", DbType = "VarChar(20)")]
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

        [Column(Storage = "_Service_Arrived_At_Site", DbType = "VarChar(20)")]
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

        [Column(Storage = "_Service_Cleared_Staff_ID", DbType = "Int")]
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

        [Column(Storage = "_Service_Closed_ID", DbType = "Int")]
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

        [Column(Storage = "_Service_Message_ID", DbType = "Int")]
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

        [Column(Storage = "_Service_Sequence_No", DbType = "Int")]
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

        [Column(Storage = "_Call_Group_ID", DbType = "Int")]
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

        [Column(Storage = "_Call_Fault_Additional_Notes", DbType = "NText", UpdateCheck = UpdateCheck.Never)]
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

        [Column(Storage = "_Service_Alert_Priority_Site", DbType = "Bit")]
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

        [Column(Storage = "_Service_Additional_Work_Req", DbType = "Bit")]
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

        [Column(Storage = "_Service_Alert_Priority_Machine", DbType = "Bit")]
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

        [Column(Storage = "_Service_Visit_No", DbType = "Int")]
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

        [Column(Storage = "_Service_Allocated_Job_No", DbType = "Int")]
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

        [Column(Storage = "_Service_Acknowledged", DbType = "VarChar(50)")]
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

        [Column(Storage = "_Service_GMU_Source_ID", DbType = "Int")]
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

        [Column(Storage = "_Service_GMU_Type_ID", DbType = "Int")]
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

        [Column(Storage = "_IsEscalated", DbType = "Bit")]
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

    public partial class rsp_GetServiceCurrentCallDetailsResult
    {

        private System.Nullable<int> _Service_Allocated_Job_No;

        private int _Service_ID;

        private System.Nullable<int> _Call_Status_ID;

        private string _Service_Received;

        private System.Nullable<int> _CallOpenDays;

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

        private string _Service_Alert_Priority_Site;

        private string _Service_Alert_Priority_Machine;

        private string _Service_Additional_Work_Req;

        private string _Site_Postcode;

        private System.Nullable<int> _Zone_ID;

        private string _Zone_Open_Monday;

        private string _Zone_Open_Tuesday;

        private string _Zone_Open_Wednesday;

        private string _Zone_Open_Thursday;

        private string _Zone_Open_Friday;

        private string _Zone_Open_Saturday;

        private string _Zone_Open_Sunday;

        private System.Nullable<int> _Site_ID;

        private string _Site_Name;

        private string _Site_Open_Monday;

        private string _Site_Open_Tuesday;

        private string _Site_Open_Wednesday;

        private string _Site_Open_Thursday;

        private string _Site_Open_Friday;

        private string _Site_Open_Saturday;

        private string _Site_Open_Sunday;

        private System.Nullable<int> _Staff_ID;

        private System.Nullable<int> _Site_Standard_Open_ID;

        private string _Site_Standard_Open_Monday;

        private string _Site_Standard_Open_Tuesday;

        private string _Site_Standard_Open_Wednesday;

        private string _Site_Standard_Open_Thursday;

        private string _Site_Standard_Open_Friday;

        private string _Site_Standard_Open_Saturday;

        private string _Site_Standard_Open_Sunday;

        private System.Nullable<int> _Zone_Standard_Open_ID;

        private string _Zone_Standard_Open_Monday;

        private string _Zone_Standard_Open_Tuesday;

        private string _Zone_Standard_Open_Wednesday;

        private string _Zone_Standard_Open_Thursday;

        private string _Zone_Standard_Open_Friday;

        private string _Zone_Standard_Open_Saturday;

        private string _Zone_Standard_Open_Sunday;

        private string _Bar_Position_Name;

        public rsp_GetServiceCurrentCallDetailsResult()
        {
        }

       [Column(Storage = "_Service_Allocated_Job_No", DbType = "Int")]
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

       [Column(Storage = "_Service_ID", DbType = "Int NOT NULL")]
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

       [Column(Storage = "_Call_Status_ID", DbType = "Int")]
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

       [Column(Storage = "_Service_Received", DbType = "VarChar(16)")]
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

       [Column(Storage = "_CallOpenDays", DbType = "Int")]
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

       [Column(Storage = "_TimeOpened", DbType = "VarChar(20) NOT NULL", CanBeNull = false)]
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

       [Column(Storage = "_Staff_Name", DbType = "VarChar(101)")]
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

       [Column(Storage = "_Site_Address", DbType = "VarChar(101)")]
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
                    this._Site_Code = value;
                }
            }
        }

       [Column(Storage = "_Machine_Name", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
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

       [Column(Storage = "_Machine_Type_ID", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
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

       [Column(Storage = "_Engineer_Name", DbType = "VarChar(101)")]
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

       [Column(Storage = "_SLA_Contract_Description", DbType = "VarChar(50)")]
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

       [Column(Storage = "_Call_Description", DbType = "VarChar(103)")]
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

       [Column(Storage = "_Call_Status_Description", DbType = "VarChar(10) NOT NULL", CanBeNull = false)]
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

       [Column(Storage = "_Service_Job_Visit_No", DbType = "VarChar(61)")]
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
                    this._Sub_Company_Name = value;
                }
            }
        }

       [Column(Storage = "_Service_SMA", DbType = "VarChar(3)")]
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

       [Column(Storage = "_Service_Alert_Priority_Site", DbType = "VarChar(1)")]
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

       [Column(Storage = "_Service_Alert_Priority_Machine", DbType = "VarChar(1)")]
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

       [Column(Storage = "_Service_Additional_Work_Req", DbType = "VarChar(1)")]
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

       [Column(Storage = "_Bar_Position_Name", DbType = "VarChar(50)")]
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
                   this._Site_Postcode = value;
               }
           }
       }

       [Column(Storage = "_Zone_ID", DbType = "Int")]
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

       [Column(Storage = "_Zone_Open_Monday", DbType = "VarChar(96)")]
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

       [Column(Storage = "_Zone_Open_Tuesday", DbType = "VarChar(96)")]
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

       [Column(Storage = "_Zone_Open_Wednesday", DbType = "VarChar(96)")]
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

       [Column(Storage = "_Zone_Open_Thursday", DbType = "VarChar(96)")]
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

       [Column(Storage = "_Zone_Open_Friday", DbType = "VarChar(96)")]
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

       [Column(Storage = "_Zone_Open_Saturday", DbType = "VarChar(96)")]
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

       [Column(Storage = "_Zone_Open_Sunday", DbType = "VarChar(96)")]
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

       [Column(Storage = "_Site_ID", DbType = "Int")]
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
                    this._Site_Name = value;
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
                    this._Site_Open_Monday = value;
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
                    this._Site_Open_Tuesday = value;
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
                    this._Site_Open_Wednesday = value;
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
                    this._Site_Open_Thursday = value;
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
                    this._Site_Open_Friday = value;
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
                    this._Site_Open_Saturday = value;
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
                    this._Site_Open_Sunday = value;
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
                    this._Staff_ID = value;
                }
            }
        }

       [Column(Storage = "_Site_Standard_Open_ID", DbType = "Int")]
        public System.Nullable<int> Site_Standard_Open_ID
        {
            get
            {
                return this._Site_Standard_Open_ID;
            }
            set
            {
                if ((this._Site_Standard_Open_ID != value))
                {
                    this._Site_Standard_Open_ID = value;
                }
            }
        }

       [Column(Storage = "_Site_Standard_Open_Monday", DbType = "VarChar(96)")]
        public string Site_Standard_Open_Monday
        {
            get
            {
                return this._Site_Standard_Open_Monday;
            }
            set
            {
                if ((this._Site_Standard_Open_Monday != value))
                {
                    this._Site_Standard_Open_Monday = value;
                }
            }
        }

       [Column(Storage = "_Site_Standard_Open_Tuesday", DbType = "VarChar(96)")]
        public string Site_Standard_Open_Tuesday
        {
            get
            {
                return this._Site_Standard_Open_Tuesday;
            }
            set
            {
                if ((this._Site_Standard_Open_Tuesday != value))
                {
                    this._Site_Standard_Open_Tuesday = value;
                }
            }
        }

       [Column(Storage = "_Site_Standard_Open_Wednesday", DbType = "VarChar(96)")]
        public string Site_Standard_Open_Wednesday
        {
            get
            {
                return this._Site_Standard_Open_Wednesday;
            }
            set
            {
                if ((this._Site_Standard_Open_Wednesday != value))
                {
                    this._Site_Standard_Open_Wednesday = value;
                }
            }
        }

       [Column(Storage = "_Site_Standard_Open_Thursday", DbType = "VarChar(96)")]
        public string Site_Standard_Open_Thursday
        {
            get
            {
                return this._Site_Standard_Open_Thursday;
            }
            set
            {
                if ((this._Site_Standard_Open_Thursday != value))
                {
                    this._Site_Standard_Open_Thursday = value;
                }
            }
        }

       [Column(Storage = "_Site_Standard_Open_Friday", DbType = "VarChar(96)")]
        public string Site_Standard_Open_Friday
        {
            get
            {
                return this._Site_Standard_Open_Friday;
            }
            set
            {
                if ((this._Site_Standard_Open_Friday != value))
                {
                    this._Site_Standard_Open_Friday = value;
                }
            }
        }

       [Column(Storage = "_Site_Standard_Open_Saturday", DbType = "VarChar(96)")]
        public string Site_Standard_Open_Saturday
        {
            get
            {
                return this._Site_Standard_Open_Saturday;
            }
            set
            {
                if ((this._Site_Standard_Open_Saturday != value))
                {
                    this._Site_Standard_Open_Saturday = value;
                }
            }
        }

       [Column(Storage = "_Site_Standard_Open_Sunday", DbType = "VarChar(96)")]
        public string Site_Standard_Open_Sunday
        {
            get
            {
                return this._Site_Standard_Open_Sunday;
            }
            set
            {
                if ((this._Site_Standard_Open_Sunday != value))
                {
                    this._Site_Standard_Open_Sunday = value;
                }
            }
        }

       [Column(Storage = "_Zone_Standard_Open_ID", DbType = "Int")]
        public System.Nullable<int> Zone_Standard_Open_ID
        {
            get
            {
                return this._Zone_Standard_Open_ID;
            }
            set
            {
                if ((this._Zone_Standard_Open_ID != value))
                {
                    this._Zone_Standard_Open_ID = value;
                }
            }
        }

       [Column(Storage = "_Zone_Standard_Open_Monday", DbType = "VarChar(96)")]
        public string Zone_Standard_Open_Monday
        {
            get
            {
                return this._Zone_Standard_Open_Monday;
            }
            set
            {
                if ((this._Zone_Standard_Open_Monday != value))
                {
                    this._Zone_Standard_Open_Monday = value;
                }
            }
        }

       [Column(Storage = "_Zone_Standard_Open_Tuesday", DbType = "VarChar(96)")]
        public string Zone_Standard_Open_Tuesday
        {
            get
            {
                return this._Zone_Standard_Open_Tuesday;
            }
            set
            {
                if ((this._Zone_Standard_Open_Tuesday != value))
                {
                    this._Zone_Standard_Open_Tuesday = value;
                }
            }
        }

       [Column(Storage = "_Zone_Standard_Open_Wednesday", DbType = "VarChar(96)")]
        public string Zone_Standard_Open_Wednesday
        {
            get
            {
                return this._Zone_Standard_Open_Wednesday;
            }
            set
            {
                if ((this._Zone_Standard_Open_Wednesday != value))
                {
                    this._Zone_Standard_Open_Wednesday = value;
                }
            }
        }

       [Column(Storage = "_Zone_Standard_Open_Thursday", DbType = "VarChar(96)")]
        public string Zone_Standard_Open_Thursday
        {
            get
            {
                return this._Zone_Standard_Open_Thursday;
            }
            set
            {
                if ((this._Zone_Standard_Open_Thursday != value))
                {
                    this._Zone_Standard_Open_Thursday = value;
                }
            }
        }

       [Column(Storage = "_Zone_Standard_Open_Friday", DbType = "VarChar(96)")]
        public string Zone_Standard_Open_Friday
        {
            get
            {
                return this._Zone_Standard_Open_Friday;
            }
            set
            {
                if ((this._Zone_Standard_Open_Friday != value))
                {
                    this._Zone_Standard_Open_Friday = value;
                }
            }
        }

        [Column(Storage = "_Zone_Standard_Open_Saturday", DbType = "VarChar(96)")]
        public string Zone_Standard_Open_Saturday
        {
            get
            {
                return this._Zone_Standard_Open_Saturday;
            }
            set
            {
                if ((this._Zone_Standard_Open_Saturday != value))
                {
                    this._Zone_Standard_Open_Saturday = value;
                }
            }
        }

        [Column(Storage = "_Zone_Standard_Open_Sunday", DbType = "VarChar(96)")]
        public string Zone_Standard_Open_Sunday
        {
            get
            {
                return this._Zone_Standard_Open_Sunday;
            }
            set
            {
                if ((this._Zone_Standard_Open_Sunday != value))
                {
                    this._Zone_Standard_Open_Sunday = value;
                }
            }
        }
    }

    public partial class rsp_GetServiceClosedCallDetailsResult
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

        public rsp_GetServiceClosedCallDetailsResult()
        {
        }

        [Column(Storage = "_Service_Received", DbType = "VarChar(16)")]
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

        [Column(Storage = "_Staff_Name_Received", DbType = "VarChar(102)")]
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

        [Column(Storage = "_Staff_Name_Cleared", DbType = "VarChar(102)")]
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

        [Column(Storage = "_Service_DownTime_HH_mm", DbType = "VarChar(5)")]
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

        [Column(Storage = "_Service_Cleared", DbType = "VarChar(16)")]
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

        [Column(Storage = "_Service_Job_Visit_No", DbType = "VarChar(61)")]
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

        [Column(Storage = "_Site_Name_Address", DbType = "VarChar(101)")]
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
                    this._Site_Code = value;
                }
            }
        }

        [Column(Storage = "_Machine_Name_Stock_No", DbType = "VarChar(103)")]
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

        [Column(Storage = "_Machine_Type_ID", DbType = "VarChar(50)")]
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

        [Column(Storage = "_Engineer_Name", DbType = "VarChar(101)")]
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

        [Column(Storage = "_SLA_Contract_Description", DbType = "VarChar(50)")]
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

        [Column(Storage = "_Call_Description", DbType = "VarChar(103)")]
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

        [Column(Storage = "_Call_Remedy_Description", DbType = "VarChar(MAX)")]
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

        [Column(Storage = "_Bar_Position_Name", DbType = "VarChar(50)")]
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
                    this._Site_Postcode = value;
                }
            }
        }

        [Column(Storage = "_TimeOpened", DbType = "Int")]
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

        [Column(Storage = "_Service_DownTime", DbType = "Int")]
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

        [Column(Storage = "_Call_Remedy_ID", DbType = "Int")]
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

        [Column(Storage = "_Service_Allocated_Job_No", DbType = "Int")]
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

        [Column(Storage = "_Service_ID", DbType = "Int NOT NULL")]
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

        [Column(Storage = "_Call_Status_ID", DbType = "Int")]
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

        [Column(Storage = "_Zone_ID", DbType = "Int")]
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

        [Column(Storage = "_Site_ID", DbType = "Int")]
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
                    this._Staff_ID = value;
                }
            }
        }

        [Column(Storage = "_Call_Group_ID", DbType = "Int")]
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
                    this._Depot_ID = value;
                }
            }
        }
    }

    public partial class rsp_GetSiteDetailsForServiceCallResult
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

        public rsp_GetSiteDetailsForServiceCallResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Standard_Opening_Hours_Description", DbType = "VarChar(50)")]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Site_Name", DbType = "VarChar(50)")]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Site_Code", DbType = "VarChar(50)")]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Site_Address_1", DbType = "VarChar(50)")]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Site_Address_2", DbType = "VarChar(50)")]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Site_Address_3", DbType = "VarChar(50)")]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Site_Address_4", DbType = "VarChar(50)")]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Site_Address_5", DbType = "VarChar(50)")]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Site_Postcode", DbType = "VarChar(15)")]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Site_Manager", DbType = "VarChar(50)")]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Site_Phone_No", DbType = "VarChar(15)")]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Depot_Name", DbType = "VarChar(50)")]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Service_Area_Name", DbType = "VarChar(50)")]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Sub_Company_Name", DbType = "VarChar(50)")]
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

    public partial class rsp_GetEngineerNamesBySiteIDResult
    {

        private System.Nullable<int> _Staff_ID;

        private string _Staff_Name;

        public rsp_GetEngineerNamesBySiteIDResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Staff_ID", DbType = "Int")]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Staff_Name", DbType = "VarChar(112)")]
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
    }

    public partial class rsp_GetMachineTypesForServiceCallResult
    {

        private string _Machine_Type_Code;

        private string _Machine_Type_Description;

        private System.Nullable<int> _Machine_Type_ID;

        public rsp_GetMachineTypesForServiceCallResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Machine_Type_Code", DbType = "VarChar(50)")]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Machine_Type_Description", DbType = "VarChar(50)")]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Machine_Type_ID", DbType = "Int")]
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

    public partial class rsp_GetServiceCurrentCallDetailsByServiceIDResult
    {

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

        public rsp_GetServiceCurrentCallDetailsByServiceIDResult()
        {
        }

        [Column(Storage = "_Call_Fault_Additional_Notes", DbType = "NText", UpdateCheck = UpdateCheck.Never)]
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

        [Column(Storage = "_Call_Fault_ID", DbType = "Int")]
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

        [Column(Storage = "_Call_Group_ID", DbType = "Int")]
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

        [Column(Storage = "_Call_Remedy_Additional_Description", DbType = "NText", UpdateCheck = UpdateCheck.Never)]
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

        [Column(Storage = "_Call_Remedy_ID", DbType = "Int")]
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

        [Column(Storage = "_Call_Source_ID", DbType = "Int")]
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

        [Column(Storage = "_Call_Status_ID", DbType = "Int")]
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

        [Column(Storage = "_Installation_ID", DbType = "Int")]
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

        [Column(Storage = "_Machine_Type_ID", DbType = "VarChar(50)")]
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

        [Column(Storage = "_Service_Acknowledged", DbType = "VarChar(50)")]
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

        [Column(Storage = "_Service_Additional_Work_Req", DbType = "Bit")]
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

        [Column(Storage = "_Service_Allocated_Job_No", DbType = "Int")]
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

        [Column(Storage = "_Service_Arrived_At_Site", DbType = "VarChar(20)")]
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

        [Column(Storage = "_Service_Cleared", DbType = "VarChar(20)")]
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

        [Column(Storage = "_Service_Issued", DbType = "VarChar(20)")]
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

        [Column(Storage = "_Service_Issued_To_Staff_ID", DbType = "Int")]
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

        [Column(Storage = "_Service_Received", DbType = "VarChar(20)")]
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

        [Column(Storage = "_Service_Visit_No", DbType = "Int")]
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

        [Column(Storage = "_Site_ID", DbType = "Int")]
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

        [Column(Storage = "_SLA_Contract_ID", DbType = "Int")]
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

        [Column(Storage = "_Service_Received_Staff_ID", DbType = "Int")]
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

        [Column(Storage = "_Service_Issued_By_Staff_ID", DbType = "Int")]
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


        [Column(Storage = "_Service_Cleared_Staff_ID", DbType = "Int")]
        public System.Nullable<int> Service_Cleared_Staff_ID
        {
            get
            {
                return this._Service_Cleared_Staff_ID;
            }
            set
            {
                if ((this._Service_Issued_To_Staff_ID != value))
                {
                    this._Service_Issued_To_Staff_ID = value;
                }
            }
        }
    }

    public partial class rsp_GetMachineNamesForServiceCallResult
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

        private string _Installation_Start_Date;

        public rsp_GetMachineNamesForServiceCallResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Bar_Position_Name", DbType = "VarChar(50)")]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Machine_Name", DbType = "VarChar(50)")]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Site_Name", DbType = "VarChar(50)")]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Site_Code", DbType = "VarChar(50)")]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Site_Address_1", DbType = "VarChar(50)")]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Site_Address_2", DbType = "VarChar(50)")]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Site_Address_3", DbType = "VarChar(50)")]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Site_Address_4", DbType = "VarChar(50)")]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Site_Address_5", DbType = "VarChar(50)")]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Site_Postcode", DbType = "VarChar(15)")]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Site_Manager", DbType = "VarChar(50)")]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Site_Phone_No", DbType = "VarChar(15)")]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Installation_ID", DbType = "Int")]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Machine_Stock_No", DbType = "VarChar(50)")]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Machine_Manufacturers_Serial_No", DbType = "VarChar(50)")]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Depot_Name", DbType = "VarChar(50)")]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Service_Area_Name", DbType = "VarChar(50)")]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Sub_Company_Name", DbType = "VarChar(50)")]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Installation_Start_Date", DbType = "VarChar(30)")]
        public string Installation_Start_Date
        {
            get
            {
                return this._Installation_Start_Date;
            }
            set
            {
                if ((this._Installation_Start_Date != value))
                {
                    this._Installation_Start_Date = value;
                }
            }
        }
    }

    public partial class rsp_GetServiceNotesByJobNoResult
    {

        private string _Staff_Name;

        private string _Service_Notes_Notes;

        private string _Service_Notes_Date;

        private int _Service_Notes_ID;

        public rsp_GetServiceNotesByJobNoResult()
        {
        }

        [Column(Storage = "_Staff_Name", DbType = "VarChar(101)")]
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

        [Column(Storage = "_Service_Notes_Notes", DbType = "VarChar(255)")]
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

        [Column(Storage = "_Service_Notes_Date", DbType = "VarChar(30)")]
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

        [Column(Storage = "_Service_Notes_ID", DbType = "Int NOT NULL")]
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
