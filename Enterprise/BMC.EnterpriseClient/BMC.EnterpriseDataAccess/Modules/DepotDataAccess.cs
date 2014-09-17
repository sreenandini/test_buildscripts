using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Data;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Linq.Expressions;
using System.ComponentModel;
using System;

namespace BMC.EnterpriseDataAccess
{
    public partial class EnterpriseDataContext
    {
        [Function(Name = "dbo.fn_IsDepotExists", IsComposable = true)]
        public System.Nullable<bool> IsDepotExists([Parameter(Name = "DepotName", DbType = "VarChar(2000)")] string depotName, [Parameter(Name = "DepotID", DbType = "Int")] System.Nullable<int> depotID, [Parameter(Name = "Operator_ID", DbType = "Int")] System.Nullable<int> operator_ID)
        {
            return ((System.Nullable<bool>)(this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), depotName, depotID, operator_ID).ReturnValue));
        }

        [Function(Name = "dbo.fn_IsDepotServiceAreaExists", IsComposable = true)]
        public System.Nullable<bool> IsDepotServiceAreaExists([Parameter(Name = "ServiceArea", DbType = "VarChar(2000)")] string serviceArea, [Parameter(Name = "ServiceAreaID", DbType = "Int")] System.Nullable<int> serviceAreaID, [Parameter(Name = "DepotID", DbType = "Int")] System.Nullable<int> depotID)
        {
            return ((System.Nullable<bool>)(this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), serviceArea, serviceAreaID, depotID).ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetDepotOperator")]
        public ISingleResult<rsp_GetDepotOperatorResult> GetDepotOperator()
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
            return ((ISingleResult<rsp_GetDepotOperatorResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetDepot")]
        public ISingleResult<rsp_GetDepotResult> GetDepot([Parameter(Name = "Operator_ID", DbType = "Int")] System.Nullable<int> operator_ID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), operator_ID);
            return ((ISingleResult<rsp_GetDepotResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetDepotDetails")]
        public ISingleResult<rsp_GetDepotDetailsResult> GetDepotDetails([Parameter(Name = "Depot_ID", DbType = "Int")] System.Nullable<int> depot_ID, [Parameter(Name = "Operator_ID", DbType = "Int")] System.Nullable<int> operator_ID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), depot_ID, operator_ID);
            return ((ISingleResult<rsp_GetDepotDetailsResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetDepotServiceArea")]
        public ISingleResult<rsp_GetDepotServiceAreaResult> GetDepotServiceArea([Parameter(Name = "Depot_ID", DbType = "Int")] System.Nullable<int> depot_ID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), depot_ID);
            return ((ISingleResult<rsp_GetDepotServiceAreaResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetDepotServiceAreaDetails")]
        public ISingleResult<rsp_GetDepotServiceAreaDetailsResult> GetDepotServiceAreaDetails([Parameter(Name = "Depotid", DbType = "Int")] System.Nullable<int> depotid, [Parameter(Name = "Service_Area_ID", DbType = "Int")] System.Nullable<int> service_Area_ID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), depotid, service_Area_ID);
            return ((ISingleResult<rsp_GetDepotServiceAreaDetailsResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetDepotSiteRep")]
        public ISingleResult<rsp_GetDepotSiteRepResult> GetDepotSiteRep([Parameter(DbType = "Int")] System.Nullable<int> iDepotid)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), iDepotid);
            return ((ISingleResult<rsp_GetDepotSiteRepResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.usp_UpdateDepotDetails")]
        public int UpdateDepotDetails(
                    [Parameter(Name = "Depot_Name", DbType = "VarChar(50)")] string depot_Name,
                    [Parameter(Name = "Depot_Address", DbType = "NText")] string depot_Address,
                    [Parameter(Name = "Depot_Postcode", DbType = "VarChar(10)")] string depot_Postcode,
                    [Parameter(Name = "Depot_Contact_Name", DbType = "VarChar(50)")] string depot_Contact_Name,
                    [Parameter(Name = "Supplier_ID", DbType = "Int")] System.Nullable<int> supplier_ID,
                    [Parameter(Name = "Depot_Reference", DbType = "VarChar(20)")] string depot_Reference,
                    [Parameter(Name = "Depot_Service", DbType = "Bit")] System.Nullable<bool> depot_Service,
                    [Parameter(Name = "Depot_Phone_Number", DbType = "VarChar(50)")] string depot_Phone_Number,
                    [Parameter(Name = "Depot_Street_Number", DbType = "VarChar(15)")] string depot_Street_Number,
                    [Parameter(Name = "Depot_Province", DbType = "VarChar(15)")] string depot_Province,
                    [Parameter(Name = "Depot_Municipality", DbType = "VarChar(40)")] string depot_Municipality,
                    [Parameter(Name = "Depot_Cadastral_Code", DbType = "VarChar(15)")] string depot_Cadastral_Code,
                    [Parameter(Name = "Depot_Area", DbType = "Int")] System.Nullable<int> depot_Area,
                    [Parameter(Name = "DepotID", DbType = "Int")] System.Nullable<int> depotID,
                    [Parameter(Name = "Service_Area_Name", DbType = "VarChar(16)")] string service_Area_Name,
                    [Parameter(Name = "Service_Area_Description", DbType = "ntext")] string service_Area_Description,
                    [Parameter(Name = "Service_Area_Notes", DbType = "ntext")] string service_Area_Notes,
                    [Parameter(Name = "Service_Area_ID", DbType = "Int")] System.Nullable<int> service_Area_ID,
                    [Parameter(Name = "StaffIDs", DbType = "VarChar(MAX)")] string staffIDs)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), depot_Name, depot_Address, depot_Postcode, depot_Contact_Name, supplier_ID, depot_Reference, depot_Service, depot_Phone_Number, depot_Street_Number, depot_Province, depot_Municipality, depot_Cadastral_Code, depot_Area, depotID, service_Area_Name, service_Area_Description, service_Area_Notes, service_Area_ID, staffIDs);
            return ((int)(result.ReturnValue));
        }
    }

    public partial class rsp_GetDepotOperatorResult
    {

        private int _Operator_ID;

        private string _Operator_Name;

        public rsp_GetDepotOperatorResult()
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
    }

    public partial class rsp_GetDepotResult
    {

        private int _Depot_ID;

        private string _Depot_Name;

        public rsp_GetDepotResult()
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

    public partial class rsp_GetDepotDetailsResult
    {

        private int _Depot_ID;

        private string _Depot_Name;

        private string _Depot_Address;

        private string _Depot_Postcode;

        private string _Depot_Contact_Name;

        private string _Depot_AMEDIS_Depot_Code;

        private System.Nullable<int> _Supplier_ID;

        private string _Depot_Reference;

        private System.Nullable<bool> _Depot_Service;

        private string _Depot_Financial_Code;

        private string _Depot_Account_Name;

        private string _Depot_Sort_Code;

        private string _Depot_Account_No;

        private string _Depot_Phone_Number;

        private string _Depot_Street_Number;

        private string _Depot_Province;

        private string _Depot_Municipality;

        private string _Depot_Cadastral_Code;

        private System.Nullable<int> _Depot_Area;

        private System.Nullable<int> _Depot_Location_Type;

        private System.Nullable<int> _Depot_Toponym;

        private System.Nullable<int> _Depot_Code;

        private System.Nullable<int> _Depot_Closed;

        private System.Nullable<System.DateTime> _Depot_ActivationDate;

        private System.Nullable<System.DateTime> _Depot_DeletionDate;

        private System.Nullable<System.DateTime> _Depot_LastUpdateDate;

        private System.Nullable<int> _Depot_Status;

        public rsp_GetDepotDetailsResult()
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

        [Column(Storage = "_Depot_Address", DbType = "NText", UpdateCheck = UpdateCheck.Never)]
        public string Depot_Address
        {
            get
            {
                return this._Depot_Address;
            }
            set
            {
                if ((this._Depot_Address != value))
                {
                    this._Depot_Address = value;
                }
            }
        }

        [Column(Storage = "_Depot_Postcode", DbType = "VarChar(10)")]
        public string Depot_Postcode
        {
            get
            {
                return this._Depot_Postcode;
            }
            set
            {
                if ((this._Depot_Postcode != value))
                {
                    this._Depot_Postcode = value;
                }
            }
        }

        [Column(Storage = "_Depot_Contact_Name", DbType = "VarChar(50)")]
        public string Depot_Contact_Name
        {
            get
            {
                return this._Depot_Contact_Name;
            }
            set
            {
                if ((this._Depot_Contact_Name != value))
                {
                    this._Depot_Contact_Name = value;
                }
            }
        }

        [Column(Storage = "_Depot_AMEDIS_Depot_Code", DbType = "VarChar(4)")]
        public string Depot_AMEDIS_Depot_Code
        {
            get
            {
                return this._Depot_AMEDIS_Depot_Code;
            }
            set
            {
                if ((this._Depot_AMEDIS_Depot_Code != value))
                {
                    this._Depot_AMEDIS_Depot_Code = value;
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

        [Column(Storage = "_Depot_Reference", DbType = "VarChar(20)")]
        public string Depot_Reference
        {
            get
            {
                return this._Depot_Reference;
            }
            set
            {
                if ((this._Depot_Reference != value))
                {
                    this._Depot_Reference = value;
                }
            }
        }

        [Column(Storage = "_Depot_Service", DbType = "Bit")]
        public System.Nullable<bool> Depot_Service
        {
            get
            {
                return this._Depot_Service;
            }
            set
            {
                if ((this._Depot_Service != value))
                {
                    this._Depot_Service = value;
                }
            }
        }

        [Column(Storage = "_Depot_Financial_Code", DbType = "VarChar(20)")]
        public string Depot_Financial_Code
        {
            get
            {
                return this._Depot_Financial_Code;
            }
            set
            {
                if ((this._Depot_Financial_Code != value))
                {
                    this._Depot_Financial_Code = value;
                }
            }
        }

        [Column(Storage = "_Depot_Account_Name", DbType = "VarChar(32)")]
        public string Depot_Account_Name
        {
            get
            {
                return this._Depot_Account_Name;
            }
            set
            {
                if ((this._Depot_Account_Name != value))
                {
                    this._Depot_Account_Name = value;
                }
            }
        }

        [Column(Storage = "_Depot_Sort_Code", DbType = "VarChar(8)")]
        public string Depot_Sort_Code
        {
            get
            {
                return this._Depot_Sort_Code;
            }
            set
            {
                if ((this._Depot_Sort_Code != value))
                {
                    this._Depot_Sort_Code = value;
                }
            }
        }

        [Column(Storage = "_Depot_Account_No", DbType = "VarChar(12)")]
        public string Depot_Account_No
        {
            get
            {
                return this._Depot_Account_No;
            }
            set
            {
                if ((this._Depot_Account_No != value))
                {
                    this._Depot_Account_No = value;
                }
            }
        }

        [Column(Storage = "_Depot_Phone_Number", DbType = "VarChar(50)")]
        public string Depot_Phone_Number
        {
            get
            {
                return this._Depot_Phone_Number;
            }
            set
            {
                if ((this._Depot_Phone_Number != value))
                {
                    this._Depot_Phone_Number = value;
                }
            }
        }

        [Column(Storage = "_Depot_Street_Number", DbType = "VarChar(15)")]
        public string Depot_Street_Number
        {
            get
            {
                return this._Depot_Street_Number;
            }
            set
            {
                if ((this._Depot_Street_Number != value))
                {
                    this._Depot_Street_Number = value;
                }
            }
        }

        [Column(Storage = "_Depot_Province", DbType = "VarChar(15)")]
        public string Depot_Province
        {
            get
            {
                return this._Depot_Province;
            }
            set
            {
                if ((this._Depot_Province != value))
                {
                    this._Depot_Province = value;
                }
            }
        }

        [Column(Storage = "_Depot_Municipality", DbType = "VarChar(40)")]
        public string Depot_Municipality
        {
            get
            {
                return this._Depot_Municipality;
            }
            set
            {
                if ((this._Depot_Municipality != value))
                {
                    this._Depot_Municipality = value;
                }
            }
        }

        [Column(Storage = "_Depot_Cadastral_Code", DbType = "VarChar(15)")]
        public string Depot_Cadastral_Code
        {
            get
            {
                return this._Depot_Cadastral_Code;
            }
            set
            {
                if ((this._Depot_Cadastral_Code != value))
                {
                    this._Depot_Cadastral_Code = value;
                }
            }
        }

        [Column(Storage = "_Depot_Area", DbType = "Int")]
        public System.Nullable<int> Depot_Area
        {
            get
            {
                return this._Depot_Area;
            }
            set
            {
                if ((this._Depot_Area != value))
                {
                    this._Depot_Area = value;
                }
            }
        }

        [Column(Storage = "_Depot_Location_Type", DbType = "Int")]
        public System.Nullable<int> Depot_Location_Type
        {
            get
            {
                return this._Depot_Location_Type;
            }
            set
            {
                if ((this._Depot_Location_Type != value))
                {
                    this._Depot_Location_Type = value;
                }
            }
        }

        [Column(Storage = "_Depot_Toponym", DbType = "Int")]
        public System.Nullable<int> Depot_Toponym
        {
            get
            {
                return this._Depot_Toponym;
            }
            set
            {
                if ((this._Depot_Toponym != value))
                {
                    this._Depot_Toponym = value;
                }
            }
        }

        [Column(Storage = "_Depot_Code", DbType = "Int")]
        public System.Nullable<int> Depot_Code
        {
            get
            {
                return this._Depot_Code;
            }
            set
            {
                if ((this._Depot_Code != value))
                {
                    this._Depot_Code = value;
                }
            }
        }

        [Column(Storage = "_Depot_Closed", DbType = "Int")]
        public System.Nullable<int> Depot_Closed
        {
            get
            {
                return this._Depot_Closed;
            }
            set
            {
                if ((this._Depot_Closed != value))
                {
                    this._Depot_Closed = value;
                }
            }
        }

        [Column(Storage = "_Depot_ActivationDate", DbType = "DateTime")]
        public System.Nullable<System.DateTime> Depot_ActivationDate
        {
            get
            {
                return this._Depot_ActivationDate;
            }
            set
            {
                if ((this._Depot_ActivationDate != value))
                {
                    this._Depot_ActivationDate = value;
                }
            }
        }

        [Column(Storage = "_Depot_DeletionDate", DbType = "DateTime")]
        public System.Nullable<System.DateTime> Depot_DeletionDate
        {
            get
            {
                return this._Depot_DeletionDate;
            }
            set
            {
                if ((this._Depot_DeletionDate != value))
                {
                    this._Depot_DeletionDate = value;
                }
            }
        }

        [Column(Storage = "_Depot_LastUpdateDate", DbType = "DateTime")]
        public System.Nullable<System.DateTime> Depot_LastUpdateDate
        {
            get
            {
                return this._Depot_LastUpdateDate;
            }
            set
            {
                if ((this._Depot_LastUpdateDate != value))
                {
                    this._Depot_LastUpdateDate = value;
                }
            }
        }

        [Column(Storage = "_Depot_Status", DbType = "Int")]
        public System.Nullable<int> Depot_Status
        {
            get
            {
                return this._Depot_Status;
            }
            set
            {
                if ((this._Depot_Status != value))
                {
                    this._Depot_Status = value;
                }
            }
        }
    }

    public partial class rsp_GetDepotServiceAreaResult
    {

        private int _Service_Area_ID;

        private System.Nullable<int> _Depot_ID;

        private string _Service_Area_Name;

        private string _Service_Area_Description;

        private string _Service_Area_Notes;

        public rsp_GetDepotServiceAreaResult()
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

    public partial class rsp_GetDepotServiceAreaDetailsResult
    {

        private int _Service_Area_ID;

        private System.Nullable<int> _Depot_ID;

        private string _Service_Area_Name;

        private string _Service_Area_Description;

        private string _Service_Area_Notes;

        public rsp_GetDepotServiceAreaDetailsResult()
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

    public partial class rsp_GetDepotSiteRepResult
    {

        private int _Staff_ID;

        private string _Name;

        private System.Nullable<int> _Depot_ID;

        public rsp_GetDepotSiteRepResult()
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

        [Column(Storage = "_Name", DbType = "VarChar(101)")]
        public string Name
        {
            get
            {
                return this._Name;
            }
            set
            {
                if ((this._Name != value))
                {
                    this._Name = value;
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

    
}
