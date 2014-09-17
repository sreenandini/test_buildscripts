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
        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.rsp_GetActiveUserName")]
        public ISingleResult<rsp_GetActiveUserNameResult> GetActiveUserName()
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
            return ((ISingleResult<rsp_GetActiveUserNameResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetUserDetails")]
        public ISingleResult<rsp_GetUserDetailsResult> GetUserDetails([Parameter(Name = "SecurityUserID", DbType = "Int")] System.Nullable<int> securityUserID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), securityUserID);
            return ((ISingleResult<rsp_GetUserDetailsResult>)(result.ReturnValue));
        }
        [Function(Name = "dbo.rsp_GetStaffDetails")]
        public ISingleResult<rsp_GetStaffDetailsResult> GetStaffDetails([Parameter(Name = "Staff_ID", DbType = "Int")] System.Nullable<int> staff_ID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), staff_ID);
            return ((ISingleResult<rsp_GetStaffDetailsResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetStaffName")]
        public ISingleResult<rsp_GetStaffNameResult> GetStaffName([Parameter(Name = "Staff_ID", DbType = "Int")] System.Nullable<int> staff_ID, [Parameter(Name = "Staff_IsARepresentative", DbType = "Bit")] System.Nullable<bool> staff_IsARepresentative, [Parameter(Name = "Staff_Terminated", DbType = "Bit")] System.Nullable<bool> staff_Terminated)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), staff_ID, staff_IsARepresentative, staff_Terminated);
            return ((ISingleResult<rsp_GetStaffNameResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_getUser_Access")]
        public ISingleResult<rsp_getUser_AccessResult> getUser_Access([Parameter(Name = "User_Group", DbType = "Int")] System.Nullable<int> user_Group)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), user_Group);
            return ((ISingleResult<rsp_getUser_AccessResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetUserGroupDetails")]
        public ISingleResult<rsp_GetUserGroupDetailsResult> GetUserGroupDetails()
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
            return ((ISingleResult<rsp_GetUserGroupDetailsResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetUserLanguagesDetails")]
        public ISingleResult<rsp_GetUserLanguagesDetailsResult> GetUserLanguagesDetails()
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
            return ((ISingleResult<rsp_GetUserLanguagesDetailsResult>)(result.ReturnValue));
        }


        [Function(Name = "dbo.usp_newUserGroup")]
        public int newUserGroup([Parameter(Name = "NewGroup", DbType = "NVarChar(100)")] string newGroup, [Parameter(Name = "Result", DbType = "Int")] ref System.Nullable<int> result)
        {
            IExecuteResult result1 = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), newGroup, result);
            result = ((System.Nullable<int>)(result1.GetParameterValue(1)));
            return ((int)(result1.ReturnValue));
        }

        [Function(Name = "dbo.usp_RevokeEmployeeCard")]
        public int RevokeEmployeeCard([Parameter(Name = "UserID", DbType = "Int")] System.Nullable<int> userID, [Parameter(Name = "EmpCardNumbers", DbType = "VarChar(200)")] string empCardNumbers, [Parameter(Name = "AuditUserId", DbType = "Int")] System.Nullable<int> auditUserId, [Parameter(Name = "AuditUserName", DbType = "VarChar(50)")] string auditUserName)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), userID, empCardNumbers,auditUserId,auditUserName);
            return ((int)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetActiveSiteDetailsforuser")]
        public ISingleResult<rsp_GetActiveSiteDetailsforuserResult> GetActiveSiteDetailsforuser([Parameter(Name = "SecurityUserID", DbType = "Int")] System.Nullable<int> securityUserID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), securityUserID);
            return ((ISingleResult<rsp_GetActiveSiteDetailsforuserResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.usp_UpdateUseronEmpCard")]
        public int UpdateUseronEmpCard([Parameter(Name = "UserID", DbType = "Int")] System.Nullable<int> userID, [Parameter(Name = "CardTrack", DbType = "Bit")] System.Nullable<bool> cardTrack, [Parameter(Name = "EmpCardNumbers", DbType = "VarChar(200)")] string empCardNumbers, [Parameter(Name = "IsUpdated", DbType = "Int")] ref System.Nullable<int> isUpdated)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), userID, cardTrack, empCardNumbers, isUpdated);
            isUpdated = ((System.Nullable<int>)(result.GetParameterValue(3)));
            return ((int)(result.ReturnValue));
        }
        [Function(Name = "dbo.rsp_GetStaffDepot")]
        public ISingleResult<rsp_GetStaffDepotResult> GetStaffDepot([Parameter(Name = "Staff_ID", DbType = "Int")] System.Nullable<int> staff_ID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), staff_ID);
            return ((ISingleResult<rsp_GetStaffDepotResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetEmployeeCardDetails")]
        public ISingleResult<rsp_GetEmployeeCardDetailsResult> GetEmployeeCardDetails([Parameter(Name = "CardNumber", DbType = "VarChar(20)")] string cardNumber)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), cardNumber);
            return ((ISingleResult<rsp_GetEmployeeCardDetailsResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetEmployeeCardInfo")]
        public ISingleResult<rsp_GetEmployeeCardDetailsResult> GetEmployeeCardInfo([Parameter(Name = "CardNumber", DbType = "VarChar(20)")] string cardNumber)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), cardNumber);
            return ((ISingleResult<rsp_GetEmployeeCardDetailsResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetCardLevelForEmployee")]
        public ISingleResult<rsp_GetEmployeeCardLevelResult> GetEmployeeCardLevel()
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
            return ((ISingleResult<rsp_GetEmployeeCardLevelResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.esp_InsertEmployeeCardDetails")]
        public int InsertEmployeeCardDetails([Parameter(Name = "EmpCardNumber", DbType = "VarChar(20)")] string empCardNumber,
            [Parameter(Name = "EmployeeName", DbType = "VarChar(50)")] string employeeName,
            [Parameter(Name = "UserID", DbType = "Int")] System.Nullable<int> userID,            
            [Parameter(Name = "IsActive", DbType = "Bit")] System.Nullable<bool> isActive,
            [Parameter(Name = "CreatedBy", DbType = "VarChar(50)")] string createdBy,            
            [Parameter(Name = "Result", DbType = "Int")] ref System.Nullable<int> result)
        {
            IExecuteResult result1 = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), empCardNumber,employeeName,userID,
                isActive, createdBy,result);
            result = ((System.Nullable<int>)(result1.GetParameterValue(6)));
            return ((int)(result1.ReturnValue));
        }

        [Function(Name = "dbo.esp_InsertEmployeeCardTypes")]
        public int InsertEmployeeCardTypes([Parameter(Name = "CardType", DbType = "VarChar(20)")] string cardType, [Parameter(Name = "Result", DbType = "Int")] ref System.Nullable<int> result)
        {
            IExecuteResult result1 = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), cardType, result);
            result = ((System.Nullable<int>)(result1.GetParameterValue(1)));
            return ((int)(result1.ReturnValue));
        }
        [Function(Name = "dbo.usp_userlockstatus")]
        public ISingleResult<usp_userlockstatusResult> UserLockStatus([Parameter(Name = "StaffID", DbType = "Int")] System.Nullable<int> staffID, [Parameter(DbType = "Int")] System.Nullable<int> isLocked)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), staffID, isLocked);
            return ((ISingleResult<usp_userlockstatusResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetServiceAreasDetails")]
        public ISingleResult<rsp_GetServiceAreasDetailsResult> GetServiceAreasDetails([Parameter(Name = "Depot_ID", DbType = "Int")] System.Nullable<int> depot_ID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), depot_ID);
            return ((ISingleResult<rsp_GetServiceAreasDetailsResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetOperatorandDepotDetails")]
        public ISingleResult<rsp_GetOperatorandDepotDetailsResult> GetOperatorandDepotDetails()
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
            return ((ISingleResult<rsp_GetOperatorandDepotDetailsResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.usp_UpdateUserDetails")]
        public int UpdateUserDetails([Parameter(Name = "WindowsUserName", DbType = "VarChar(200)")] string windowsUserName, [Parameter(Name = "Password", DbType = "VarChar(200)")] string password, [Parameter(Name = "UserName", DbType = "VarChar(200)")] string userName, [Parameter(Name = "LanguageID", DbType = "Int")] System.Nullable<int> languageID, [Parameter(Name = "CurrencyCulture", DbType = "Int")] System.Nullable<int> currencyCulture, [Parameter(Name = "DateCulture", DbType = "Int")] System.Nullable<int> dateCulture, [Parameter(Name = "SecurityUserID", DbType = "Int")] ref System.Nullable<int> securityUserID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), windowsUserName, password, userName, languageID, currencyCulture, dateCulture, securityUserID);
            securityUserID = ((System.Nullable<int>)(result.GetParameterValue(6)));
            return ((int)(result.ReturnValue));
        }

        [Function(Name = "dbo.usp_UpdateStaffDepot")]
        public int UpdateStaffDepot([Parameter(Name = "Staff_ID", DbType = "Int")] System.Nullable<int> staff_ID, [Parameter(Name = "Depot_ID", DbType = "Int")] System.Nullable<int> depot_ID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), staff_ID, depot_ID);
            return ((int)(result.ReturnValue));
        }

        [Function(Name = "dbo.usp_UpdateRoleAccess")]
        public int UpdateRoleAccess([Parameter(Name = "SecurityUserID", DbType = "Int")] System.Nullable<int> securityUserID, [Parameter(Name = "UserLevel", DbType = "VarChar(100)")] string userLevel)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), securityUserID, userLevel);
            return ((int)(result.ReturnValue));
        }

        [Function(Name = "dbo.usp_AddModifyStaffDetails")]
        public int AddModifyStaffDetails(
                    [Parameter(Name = "User_Group_ID", DbType = "Int")] System.Nullable<int> user_Group_ID,
                    [Parameter(Name = "Staff_First_Name", DbType = "VarChar(50)")] string staff_First_Name,
                    [Parameter(Name = "Staff_Last_Name", DbType = "VarChar(50)")] string staff_Last_Name,
                    [Parameter(Name = "Staff_Title", DbType = "VarChar(5)")] string staff_Title,
                    [Parameter(Name = "Staff_Address", DbType = "NVarChar(MAX)")] string staff_Address,
                    [Parameter(Name = "Staff_Postcode", DbType = "VarChar(10)")] string staff_Postcode,
                    [Parameter(Name = "Staff_Phone_No", DbType = "VarChar(15)")] string staff_Phone_No,
                    [Parameter(Name = "Staff_Extension_No", DbType = "VarChar(15)")] string staff_Extension_No,
                    [Parameter(Name = "Staff_Mobile_No", DbType = "VarChar(15)")] string staff_Mobile_No,
                    [Parameter(Name = "Staff_Job_Title", DbType = "VarChar(50)")] string staff_Job_Title,
                    [Parameter(Name = "Staff_Department", DbType = "VarChar(50)")] string staff_Department,
                    [Parameter(Name = "Staff_IsAnEngineer", DbType = "Bit")] System.Nullable<bool> staff_IsAnEngineer,
                    [Parameter(Name = "Staff_IsARepresentative", DbType = "Bit")] System.Nullable<bool> staff_IsARepresentative,
                    [Parameter(Name = "Staff_IsAStockController", DbType = "Bit")] System.Nullable<bool> staff_IsAStockController,
                    [Parameter(Name = "Staff_Start_Date", DbType = "VarChar(30)")] string staff_Start_Date,
                    [Parameter(Name = "Staff_End_Date", DbType = "VarChar(30)")] string staff_End_Date,
                    [Parameter(Name = "Staff_Username", DbType = "VarChar(50)")] string staff_Username,
                    [Parameter(Name = "Staff_Password", DbType = "VarChar(50)")] string staff_Password,
                    [Parameter(Name = "Depot_ID", DbType = "Int")] System.Nullable<int> depot_ID,
                    [Parameter(Name = "Service_Area_ID", DbType = "Int")] System.Nullable<int> service_Area_ID,
                    [Parameter(Name = "Supplier_ID", DbType = "Int")] System.Nullable<int> supplier_ID,
                    [Parameter(Name = "Staff_Personel_No", DbType = "VarChar(10)")] string staff_Personel_No,
                    [Parameter(Name = "Staff_Terminated", DbType = "Bit")] System.Nullable<bool> staff_Terminated,
                    [Parameter(Name = "Staff_Notes", DbType = "VarChar(255)")] string staff_Notes,
                    [Parameter(Name = "Email_Address", DbType = "VarChar(100)")] string email_Address,
                    [Parameter(Name = "UserTableID", DbType = "Int")] System.Nullable<int> userTableID,
                    [Parameter(Name = "Staff_ID", DbType = "Int")] ref System.Nullable<int> staff_ID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), user_Group_ID, staff_First_Name, staff_Last_Name, staff_Title, staff_Address, staff_Postcode, staff_Phone_No, staff_Extension_No, staff_Mobile_No, staff_Job_Title, staff_Department, staff_IsAnEngineer, staff_IsARepresentative, staff_IsAStockController, staff_Start_Date, staff_End_Date, staff_Username, staff_Password, depot_ID, service_Area_ID, supplier_ID, staff_Personel_No, staff_Terminated, staff_Notes, email_Address, userTableID, staff_ID);
            staff_ID = ((System.Nullable<int>)(result.GetParameterValue(26)));
            return ((int)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_CheckUserNameAlreadyExists")]
        public ISingleResult<rsp_CheckUserNameAlreadyExistsResult> CheckUserNameAlreadyExists([Parameter(Name = "StaffUser_Name", DbType = "VarChar(50)")] string staffUser_Name, [Parameter(Name = "Staff_Id", DbType = "Int")] System.Nullable<int> staff_Id)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), staffUser_Name, staff_Id);
            return ((ISingleResult<rsp_CheckUserNameAlreadyExistsResult>)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.usp_InsertEmpGMUMode")]
        public int InsertEmpGMUMode([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "EmpCardNumber", DbType = "VarChar(MAX)")] string empCardNumber, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "ExportXML", DbType = "VarChar(MAX)")] string exportXML, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "UserID", DbType = "Int")] System.Nullable<int> userID, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "UserName", DbType = "VarChar(15)")] string userName, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "ModuleID", DbType = "Int")] System.Nullable<int> moduleID, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "ModuleName", DbType = "VarChar(30)")] string moduleName, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Desc", DbType = "VarChar(250)")] string desc, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "EmpGroupID", DbType = "Int")] System.Nullable<int> empGroupID, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "CardLevel", DbType = "Int")] System.Nullable<int> cardLevel)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), empCardNumber, exportXML, userID, userName, moduleID, moduleName, desc, empGroupID,cardLevel);
            return ((int)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetEmployeeCardTypes")]
        public ISingleResult<rsp_GetEmployeeCardTypesResult> GetEmployeeCardTypes()
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
            return ((ISingleResult<rsp_GetEmployeeCardTypesResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetGMUModes")]
        public ISingleResult<rsp_GetGMUModesResult> GetGMUModes()
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
            return ((ISingleResult<rsp_GetGMUModesResult>)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.rsp_GetGMUEvents")]
        public ISingleResult<rsp_GetGMUEventsResult> GetGMUEvents()
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
            return ((ISingleResult<rsp_GetGMUEventsResult>)(result.ReturnValue));
        }
        [Function(Name = "dbo.rsp_GetEmpGMUModes")]
        public ISingleResult<rsp_GetEmpGMUModesResult> GetEmpGMUModes([Parameter(Name = "RoleID", DbType = "Int")] System.Nullable<int> roleID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), roleID);
            return ((ISingleResult<rsp_GetEmpGMUModesResult>)(result.ReturnValue));
        }
        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.rsp_GetEmpGMUEvents")]
        public ISingleResult<rsp_GetEmpGMUEventsResult> GetEmpGMUEvents([Parameter(Name = "RoleID", DbType = "Int")] System.Nullable<int> roleID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), roleID);
            return ((ISingleResult<rsp_GetEmpGMUEventsResult>)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.rsp_GetEventGroupTypes")]
        public ISingleResult<rsp_GetEventGroupTypesResult> GetEventGroupTypes()
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
            return ((ISingleResult<rsp_GetEventGroupTypesResult>)(result.ReturnValue));
        }
        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.usp_InsertEmpGMUEvent")]
        public int InsertEmpGMUEvent([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "EmpXML", DbType = "VarChar(MAX)")] string empXML, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "ExportXML", DbType = "VarChar(MAX)")] string exportXML, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "UserID", DbType = "Int")] System.Nullable<int> userID, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "UserName", DbType = "VarChar(15)")] string userName, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "ModuleID", DbType = "Int")] System.Nullable<int> moduleID, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "ModuleName", DbType = "VarChar(30)")] string moduleName, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Desc", DbType = "Int")] System.Nullable<int> desc, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "CardLevel", DbType = "Int")] System.Nullable<int> cardlevel)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), empXML, exportXML, userID, userName, moduleID, moduleName, desc,cardlevel);
            return ((int)(result.ReturnValue));
        }
        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.rsp_GetCardLevelBasedOnRole")]
        public ISingleResult<rsp_GetCardLevelBasedOnRoleResult> GetCardLevelBasedOnRole()
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
            return ((ISingleResult<rsp_GetCardLevelBasedOnRoleResult>)(result.ReturnValue));
        }
    }
    public partial class rsp_GetCardLevelBasedOnRoleResult
    {

        private int _SecurityRoleID;

        private string _RoleName;

        private System.Nullable<int> _EmpGMUModeGroup;

        private System.Nullable<int> _CardLevel;

        public rsp_GetCardLevelBasedOnRoleResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_SecurityRoleID", DbType = "Int NOT NULL")]
        public int SecurityRoleID
        {
            get
            {
                return this._SecurityRoleID;
            }
            set
            {
                if ((this._SecurityRoleID != value))
                {
                    this._SecurityRoleID = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_RoleName", DbType = "VarChar(100)")]
        public string RoleName
        {
            get
            {
                return this._RoleName;
            }
            set
            {
                if ((this._RoleName != value))
                {
                    this._RoleName = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_EmpGMUModeGroup", DbType = "Int")]
        public System.Nullable<int> EmpGMUModeGroup
        {
            get
            {
                return this._EmpGMUModeGroup;
            }
            set
            {
                if ((this._EmpGMUModeGroup != value))
                {
                    this._EmpGMUModeGroup = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_CardLevel", DbType = "Int")]
        public System.Nullable<int> CardLevel
        {
            get
            {
                return this._CardLevel;
            }
            set
            {
                if ((this._CardLevel != value))
                {
                    this._CardLevel = value;
                }
            }
        }
    }
    public partial class rsp_GetActiveUserNameResult
    {

        private string _UserName;

        private int _SecurityUserID;

        public rsp_GetActiveUserNameResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_UserName", DbType = "VarChar(200)")]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_SecurityUserID", DbType = "Int NOT NULL")]
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
    }
    public partial class rsp_GetEventGroupTypesResult
    {

        private int _GMUEventGroupID;

        private string _GMUEventGroupName;

        public rsp_GetEventGroupTypesResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_GMUEventGroupID", DbType = "Int NOT NULL")]
        public int GMUEventGroupID
        {
            get
            {
                return this._GMUEventGroupID;
            }
            set
            {
                if ((this._GMUEventGroupID != value))
                {
                    this._GMUEventGroupID = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_GMUEventGroupName", DbType = "VarChar(250) NOT NULL", CanBeNull = false)]
        public string GMUEventGroupName
        {
            get
            {
                return this._GMUEventGroupName;
            }
            set
            {
                if ((this._GMUEventGroupName != value))
                {
                    this._GMUEventGroupName = value;
                }
            }
        }
    }
    public partial class rsp_GetEmpGMUEventsResult
    {

        private int _EmpGMUEventId;

        private int _GMUEventId;

        private string _EmpCardNumber;

        private int _GMUEventGroupID;


        public rsp_GetEmpGMUEventsResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_EmpGMUEventId", DbType = "Int NOT NULL")]
        public int EmpGMUEventId
        {
            get
            {
                return this._EmpGMUEventId;
            }
            set
            {
                if ((this._EmpGMUEventId != value))
                {
                    this._EmpGMUEventId = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_GMUEventGroupID", DbType = "Int NOT NULL")]
        public int GMUEventGroupID
        {
            get
            {
                return this._GMUEventGroupID;
            }
            set
            {
                if ((this._GMUEventGroupID != value))
                {
                    this._GMUEventGroupID = value;
                }
            }
        }
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_GMUEventId", DbType = "Int NOT NULL")]
        public int GMUEventId
        {
            get
            {
                return this._GMUEventId;
            }
            set
            {
                if ((this._GMUEventId != value))
                {
                    this._GMUEventId = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_EmpCardNumber", DbType = "VarChar(20) NOT NULL", CanBeNull = false)]
        public string EmpCardNumber
        {
            get
            {
                return this._EmpCardNumber;
            }
            set
            {
                if ((this._EmpCardNumber != value))
                {
                    this._EmpCardNumber = value;
                }
            }
        }
    }
    public partial class rsp_GetUserDetailsResult
    {

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

        public rsp_GetUserDetailsResult()
        {
        }

        [Column(Storage = "_SecurityUserID", DbType = "Int NOT NULL")]
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

        [Column(Storage = "_WindowsUserName", DbType = "VarChar(200)")]
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

        [Column(Storage = "_UserName", DbType = "VarChar(200)")]
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

        [Column(Storage = "_Password", DbType = "VarChar(200)")]
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

        [Column(Storage = "_LanguageID", DbType = "Int")]
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

        [Column(Storage = "_CurrencyCulture", DbType = "Int")]
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

        [Column(Storage = "_DateCulture", DbType = "Int")]
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

        [Column(Storage = "_ChangePassword", DbType = "Bit")]
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

        [Column(Storage = "_CreatedDate", DbType = "DateTime")]
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

        [Column(Storage = "_PasswordChangeDate", DbType = "DateTime")]
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

        [Column(Storage = "_isReset", DbType = "Bit")]
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

        [Column(Storage = "_isLocked", DbType = "Bit NOT NULL")]
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

    public partial class rsp_GetStaffDetailsResult
    {

        private int _Staff_ID;

        private System.Nullable<int> _Operator_ID;

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

        private string _Staff_Username;

        private string _Staff_Password;

        private System.Nullable<int> _Depot_ID;

        private System.Nullable<int> _Service_Area_ID;

        private System.Nullable<int> _Supplier_ID;

        private string _Staff_Personel_No;

        private System.Nullable<bool> _Staff_Terminated;

        private string _Staff_Notes;

        private string _Email_Address;

        private System.Nullable<int> _UserTableID;

        public rsp_GetStaffDetailsResult()
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

    public partial class rsp_GetStaffNameResult
    {

        private int _Staff_ID;

        private string _Staff_First_Name;

        private string _Staff_Last_Name;

        private int _UserTableID;

        public rsp_GetStaffNameResult()
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


        [Column(Storage = "_UserTableID", DbType = "Int ")]
        public int UserTableID
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

    public partial class rsp_GetUserLanguagesDetailsResult
    {

        private int _LanguageID;

        private string _LanguageName;

        private string _CultureInfo;

        public rsp_GetUserLanguagesDetailsResult()
        {
        }

        [Column(Storage = "_LanguageID", DbType = "Int NOT NULL")]
        public int LanguageID
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

        [Column(Storage = "_LanguageName", DbType = "VarChar(100)")]
        public string LanguageName
        {
            get
            {
                return this._LanguageName;
            }
            set
            {
                if ((this._LanguageName != value))
                {
                    this._LanguageName = value;
                }
            }
        }

        [Column(Storage = "_CultureInfo", DbType = "VarChar(6)")]
        public string CultureInfo
        {
            get
            {
                return this._CultureInfo;
            }
            set
            {
                if ((this._CultureInfo != value))
                {
                    this._CultureInfo = value;
                }
            }
        }
    }

    public partial class rsp_GetStaffDepotResult
    {

        private System.Nullable<int> _Depot_ID;

        public rsp_GetStaffDepotResult()
        {
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

    public partial class rsp_GetEmployeeCardTypesResult
    {

        private int _GMUModeGroupID;

        private string _GMUModeGroupName;

        private string _GMUModeGroupDescription;

        public rsp_GetEmployeeCardTypesResult()
        {
        }

        [Column(Storage = "_GMUModeGroupID", DbType = "Int NOT NULL")]
        public int GMUModeGroupID
        {
            get
            {
                return this._GMUModeGroupID;
            }
            set
            {
                if ((this._GMUModeGroupID != value))
                {
                    this._GMUModeGroupID = value;
                }
            }
        }

        [Column(Storage = "_GMUModeGroupName", DbType = "VarChar(20)")]
        public string GMUModeGroupName
        {
            get
            {
                return this._GMUModeGroupName;
            }
            set
            {
                if ((this._GMUModeGroupName != value))
                {
                    this._GMUModeGroupName = value;
                }
            }
        }

        [Column(Storage = "_GMUModeGroupDescription", DbType = "VarChar(20)")]
        public string GMUModeGroupDescription
        {
            get
            {
                return this._GMUModeGroupDescription;
            }
            set
            {
                if ((this._GMUModeGroupDescription != value))
                {
                    this._GMUModeGroupDescription = value;
                }
            }
        }
    }

    public partial class rsp_GetEmployeeCardLevelResult
    {

        private int _CardLevel;

        public rsp_GetEmployeeCardLevelResult()
        {
        }

        [Column(Storage = "_CardLevel", DbType = "Int NOT NULL")]
        public int CardLevel
        {
            get
            {
                return this._CardLevel;
            }
            set
            {
                if ((this._CardLevel != value))
                {
                    this._CardLevel = value;
                }
            }
        }
    }

    public partial class rsp_GetEmployeeCardDetailsResult
    {

        private int _EmpID;

        private string _EmployeeCardNumber;

        private string _EmployeeName;

        private System.Nullable<bool> _IsActive;

        private System.Nullable<bool> _IsMasterCard;

        private string _CardType;

        private System.Nullable<int> _UserID;

        private string _Mapped;

        private System.Nullable<bool> _IsChecked;

        private string _SiteCode;

        private int? _CardLevel;

        public rsp_GetEmployeeCardDetailsResult()
        {
        }

        [Column(Storage = "_EmpID", DbType = "Int NOT NULL")]
        public int EmpID
        {
            get
            {
                return this._EmpID;
            }
            set
            {
                if ((this._EmpID != value))
                {
                    this._EmpID = value;
                }
            }
        }

        [Column(Storage = "_EmployeeCardNumber", DbType = "VarChar(20) NOT NULL", CanBeNull = false)]
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

        [Column(Storage = "_EmployeeName", DbType = "VarChar(50)")]
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

        [Column(Storage = "_IsActive", DbType = "Bit")]
        public System.Nullable<bool> IsActive
        {
            get
            {
                return this._IsActive;
            }
            set
            {
                if ((this._IsActive != value))
                {
                    this._IsActive = value;
                }
            }
        }

        [Column(Storage = "_IsMasterCard", DbType = "Bit")]
        public System.Nullable<bool> IsMasterCard
        {
            get
            {
                return this._IsMasterCard;
            }
            set
            {
                if ((this._IsMasterCard != value))
                {
                    this._IsMasterCard = value;
                }
            }
        }

        [Column(Storage = "_CardType", DbType = "VarChar(30)")]
        public string CardType
        {
            get
            {
                return this._CardType;
            }
            set
            {
                if ((this._CardType != value))
                {
                    this._CardType = value;
                }
            }
        }

        [Column(Storage = "_UserID", DbType = "Int")]
        public System.Nullable<int> UserID
        {
            get
            {
                return this._UserID;
            }
            set
            {
                if ((this._UserID != value))
                {
                    this._UserID = value;
                }
            }
        }

        [Column(Storage = "_Mapped", DbType = "VarChar(10) NOT NULL", CanBeNull = false)]
        public string Mapped
        {
            get
            {
                return this._Mapped;
            }
            set
            {
                if ((this._Mapped != value))
                {
                    this._Mapped = value;
                }
            }
        }

        [Column(Storage = "_IsChecked", DbType = "Bit")]
        public System.Nullable<bool> IsChecked
        {
            get
            {
                return this._IsChecked;
            }
            set
            {
                if ((this._IsChecked != value))
                {
                    this._IsChecked = value;
                }
            }
        }

        [Column(Storage = "_SiteCode", DbType = "varchar(50)")]
        public string SiteCode
        {
            get
            {
                return this._SiteCode;
            }
            set
            {
                if ((this._SiteCode != value))
                {
                    this._SiteCode = value;
                }
            }
        }
        [Column(Storage = "_CardLevel", DbType = "INT")]
        public System.Nullable<int> CardLevel
        {
            get
            {
                return this._CardLevel;
            }
            set
            {
                if ((this._CardLevel != value))
                {
                    this._CardLevel = value;
                }
            }
        }
    }
    public partial class rsp_GetUserGroupDetailsResult
    {

        private int _User_Group_ID;

        private string _User_Group_Name;

        private System.Nullable<int> _HQ_User_Access_ID;

        public rsp_GetUserGroupDetailsResult()
        {
        }

        [Column(Storage = "_User_Group_ID", DbType = "Int NOT NULL")]
        public int User_Group_ID
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

        [Column(Storage = "_User_Group_Name", DbType = "VarChar(50)")]
        public string User_Group_Name
        {
            get
            {
                return this._User_Group_Name;
            }
            set
            {
                if ((this._User_Group_Name != value))
                {
                    this._User_Group_Name = value;
                }
            }
        }

        [Column(Storage = "_HQ_User_Access_ID", DbType = "Int")]
        public System.Nullable<int> HQ_User_Access_ID
        {
            get
            {
                return this._HQ_User_Access_ID;
            }
            set
            {
                if ((this._HQ_User_Access_ID != value))
                {
                    this._HQ_User_Access_ID = value;
                }
            }
        }
    }

    public partial class usp_userlockstatusResult
    {

        private string _RESULT;

        public usp_userlockstatusResult()
        {
        }

        [Column(Storage = "_RESULT", DbType = "VarChar(7) NOT NULL", CanBeNull = false)]
        public string RESULT
        {
            get
            {
                return this._RESULT;
            }
            set
            {
                if ((this._RESULT != value))
                {
                    this._RESULT = value;
                }
            }
        }
    }

    public partial class rsp_GetOperatorandDepotDetailsResult
    {

        private int _Operator_ID;

        private string _Operator_Name;

        private System.Nullable<int> _Depot_ID;

        private string _Depot_Name;

        public rsp_GetOperatorandDepotDetailsResult()
        {
        }

        [Column(Storage = "_Operator_ID", DbType = "Int NOT NULL")]
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

        [Column(Storage = "_Operator_Name", DbType = "VarChar(50)")]
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

    public partial class rsp_GetServiceAreasDetailsResult
    {

        private int _Service_Area_ID;

        private string _Service_Area_Name;

        public rsp_GetServiceAreasDetailsResult()
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
    }
    public class rsp_GetEmpGMUModesResult
    {
        private string _EmpCardNumber;

        private string _GMUMode;

        private int _EmpGMUModeId;

        private int _GMUModeId;

        private int _GMUModeGroupID;

        private int? _EmpGMUModeGroup;

        public rsp_GetEmpGMUModesResult()
        {
        }

        [Column(Storage = "_GMUMode", DbType = "VarChar(20) NOT NULL", CanBeNull = false)]
        public string GMUMode
        {
            get
            {
                return this._GMUMode;
            }
            set
            {
                if ((this._GMUMode != value))
                {
                    this._GMUMode = value;
                }
            }
        }

        [Column(Storage = "_EmpGMUModeId", DbType = "Int NOT NULL")]
        public int EmpGMUModeId
        {
            get
            {
                return this._EmpGMUModeId;
            }
            set
            {
                if ((this._EmpGMUModeId != value))
                {
                    this._EmpGMUModeId = value;
                }
            }
        }

        [Column(Storage = "_GMUModeGroupID", DbType = "Int NOT NULL")]
        public int GMUModeGroupID
        {
            get
            {
                return this._GMUModeGroupID;
            }
            set
            {
                if ((this._GMUModeGroupID != value))
                {
                    this._GMUModeGroupID = value;
                }
            }
        }

        [Column(Storage = "_GMUModeId", DbType = "Int NOT NULL")]
        public int GMUModeId
        {
            get
            {
                return this._GMUModeId;
            }
            set
            {
                if ((this._GMUModeId != value))
                {
                    this._GMUModeId = value;
                }
            }
        }

        [Column(Storage = "_EmpCardNumber", DbType = "VarChar(50)")]
        public string EmpCardNumber
        {
            get
            {
                return this._EmpCardNumber;
            }
            set
            {
                if ((this._EmpCardNumber != value))
                {
                    this._EmpCardNumber = value;
                }
            }
        }
        [Column(Storage = "_EmpGMUModeGroup", DbType = "INT")]
        public int? EmpGMUModeGroup
        {
            get
            {
                return this._EmpGMUModeGroup;
            }
            set
            {
                if ((this._EmpGMUModeGroup != value))
                {
                    this._EmpGMUModeGroup = value;
                }
            }
        }
    }



    public partial class rsp_GetGMUModesResult
    {

        private string _GMUMode;

        private int _GMUModeID;

        private int _GMUModeGroupID;

        private string _GMUModedescription;

        private string _GMUModeGroupName;

        public rsp_GetGMUModesResult()
        {
        }

        [Column(Storage = "_GMUMode", DbType = "VarChar(20) NOT NULL", CanBeNull = false)]
        public string GMUMode
        {
            get
            {
                return this._GMUMode;
            }
            set
            {
                if ((this._GMUMode != value))
                {
                    this._GMUMode = value;
                }
            }
        }

        [Column(Storage = "_GMUModeID", DbType = "Int NOT NULL")]
        public int GMUModeID
        {
            get
            {
                return this._GMUModeID;
            }
            set
            {
                if ((this._GMUModeID != value))
                {
                    this._GMUModeID = value;
                }
            }
        }

        [Column(Storage = "_GMUModeGroupID", DbType = "Int NOT NULL")]
        public int GMUModeGroupID
        {
            get
            {
                return this._GMUModeGroupID;
            }
            set
            {
                if ((this._GMUModeGroupID != value))
                {
                    this._GMUModeGroupID = value;
                }
            }
        }

        [Column(Storage = "_GMUModeGroupName", DbType = "VarChar(15)")]
        public string GMUModeGroupName
        {
            get
            {
                return this._GMUModeGroupName;
            }
            set
            {
                if ((this._GMUModeGroupName != value))
                {
                    this._GMUModeGroupName = value;
                }
            }
        }
        [Column(Storage = "_GMUModedescription", DbType = "VarChar(250)")]
        public string GMUModedescription
        {
            get
            {
                return this._GMUModedescription;
            }
            set
            {
                if ((this._GMUModedescription != value))
                {
                    this._GMUModedescription = value;
                }
            }
        }
    }


    public partial class rsp_GetActiveSiteDetailsforuserResult
    {

        private int _Site_ID;

        private string _Site_Code;

        private string _Site_Name;

        private string _SC_ExchangeConnectionSting;

        private string _SC_TicketConnectionSting;

        public rsp_GetActiveSiteDetailsforuserResult()
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

        [Column(Storage = "_SC_ExchangeConnectionSting", DbType = "NVarChar(4000)")]
        public string SC_ExchangeConnectionSting
        {
            get
            {
                return this._SC_ExchangeConnectionSting;
            }
            set
            {
                if ((this._SC_ExchangeConnectionSting != value))
                {
                    this._SC_ExchangeConnectionSting = value;
                }
            }
        }

        [Column(Storage = "_SC_TicketConnectionSting", DbType = "NVarChar(4000)")]
        public string SC_TicketConnectionSting
        {
            get
            {
                return this._SC_TicketConnectionSting;
            }
            set
            {
                if ((this._SC_TicketConnectionSting != value))
                {
                    this._SC_TicketConnectionSting = value;
                }
            }
        }
    }

    public partial class rsp_CheckUserNameAlreadyExistsResult
    {

        private string _Staff_Username;

        public rsp_CheckUserNameAlreadyExistsResult()
        {
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
    }

    public partial class rsp_GetGMUEventsResult
    {

        private int _GMUEventID;

        private string _GMUEventName;

        private string _GMUEventDescription;

        private int _GMUEventGroupID;

        private int _Event_Fault_Type;

        private int _Event_Fault_Source;

        public rsp_GetGMUEventsResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_GMUEventID", DbType = "Int NOT NULL")]
        public int GMUEventID
        {
            get
            {
                return this._GMUEventID;
            }
            set
            {
                if ((this._GMUEventID != value))
                {
                    this._GMUEventID = value;
                }
            }
        }
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_GMUEventGroupID", DbType = "Int NOT NULL")]
        public int GMUEventGroupID
        {
            get
            {
                return this._GMUEventGroupID;
            }
            set
            {
                if ((this._GMUEventGroupID != value))
                {
                    this._GMUEventGroupID = value;
                }
            }
        }
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Event_Fault_Source", DbType = "Int NOT NULL")]
        public int Event_Fault_Source
        {
            get
            {
                return this._Event_Fault_Source;
            }
            set
            {
                if ((this._Event_Fault_Source != value))
                {
                    this._Event_Fault_Source = value;
                }
            }
        }
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Event_Fault_Type", DbType = "Int NOT NULL")]
        public int Event_Fault_Type
        {
            get
            {
                return this._Event_Fault_Type;
            }
            set
            {
                if ((this._Event_Fault_Type != value))
                {
                    this._Event_Fault_Type = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_GMUEventName", DbType = "VarChar(50)")]
        public string GMUEventName
        {
            get
            {
                return this._GMUEventName;
            }
            set
            {
                if ((this._GMUEventName != value))
                {
                    this._GMUEventName = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_GMUEventDescription", DbType = "VarChar(250)")]
        public string GMUEventDescription
        {
            get
            {
                return this._GMUEventDescription;
            }
            set
            {
                if ((this._GMUEventDescription != value))
                {
                    this._GMUEventDescription = value;
                }
            }
        }
    }

}
