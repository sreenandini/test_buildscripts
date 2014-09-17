using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Data;
using System.Reflection;
using System.Linq.Expressions;
using System.ComponentModel;

namespace BMC.EnterpriseDataAccess
{
    public partial class EnterpriseDataContext
    {
        [Function(Name = "dbo.fn_IsOperatorExists", IsComposable = true)]
        public System.Nullable<bool> IsOperatorExists([Parameter(Name = "OperatorName", DbType = "VarChar(2000)")] string operatorName, [Parameter(Name = "OperatorID", DbType = "Int")] System.Nullable<int> operatorID)
        {
            return ((System.Nullable<bool>)(this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), operatorName, operatorID).ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetOperatorDetails")]
        public ISingleResult<rsp_GetOperatorDetailsResult> GetOperatorDetails([Parameter(Name = "Operator_ID", DbType = "Int")] System.Nullable<int> operator_ID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), operator_ID);
            return ((ISingleResult<rsp_GetOperatorDetailsResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.usp_DeleteOperatorDetails")]
        public int DeleteOperatorDetails([Parameter(Name = "OperatorID", DbType = "Int")] System.Nullable<int> operatorID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), operatorID);
            return ((int)(result.ReturnValue));
        }

        [Function(Name = "dbo.usp_UpdateOperatorDetails")]
        public int UpdateOperatorDetails(
                    [Parameter(Name = "Operator_ID", DbType = "Int")] System.Nullable<int> operator_ID,
                    [Parameter(Name = "Calendar_ID", DbType = "Int")] System.Nullable<int> calendar_ID,
                    [Parameter(Name = "Operator_Name", DbType = "VarChar(50)")] string operator_Name,
                    [Parameter(Name = "Operator_Address", DbType = "NText")] string operator_Address,
                    [Parameter(Name = "Operator_PostCode", DbType = "VarChar(50)")] string operator_PostCode,
                    [Parameter(Name = "Operator_Depot_Phone", DbType = "VarChar(15)")] string operator_Depot_Phone,
                    [Parameter(Name = "Operator_Fax", DbType = "VarChar(15)")] string operator_Fax,
                    [Parameter(Name = "Operator_EMail", DbType = "VarChar(100)")] string operator_EMail,
                    [Parameter(Name = "Operator_Contact", DbType = "VarChar(50)")] string operator_Contact,
                    [Parameter(Name = "Operator_Invoice_Address", DbType = "NText")] string operator_Invoice_Address,
                    [Parameter(Name = "Operator_Invoice_Postcode", DbType = "VarChar(50)")] string operator_Invoice_Postcode,
                    [Parameter(Name = "Operator_Invoice_Name", DbType = "VarChar(50)")] string operator_Invoice_Name,
                    [Parameter(Name = "Operator_Start_Date", DbType = "VarChar(30)")] string operator_Start_Date,
                    [Parameter(Name = "Operator_End_Date", DbType = "VarChar(30)")] string operator_End_Date,
                    [Parameter(Name = "Operator_AMEDIS_Code", DbType = "VarChar(4)")] string operator_AMEDIS_Code,
                    [Parameter(Name = "Operator_Logo_Reference", DbType = "VarChar(50)")] string operator_Logo_Reference,
                    [Parameter(Name = "Operator_Account_Name", DbType = "VarChar(32)")] string operator_Account_Name,
                    [Parameter(Name = "Operator_Sort_Code", DbType = "VarChar(8)")] string operator_Sort_Code,
                    [Parameter(Name = "Operator_Account_No", DbType = "VarChar(12)")] string operator_Account_No)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), operator_ID, calendar_ID, operator_Name, operator_Address, operator_PostCode, operator_Depot_Phone, operator_Fax, operator_EMail, operator_Contact, operator_Invoice_Address, operator_Invoice_Postcode, operator_Invoice_Name, operator_Start_Date, operator_End_Date, operator_AMEDIS_Code, operator_Logo_Reference, operator_Account_Name, operator_Sort_Code, operator_Account_No);
            return ((int)(result.ReturnValue));
        }
    }

    public partial class rsp_GetOperatorDetailsResult
    {

        private int _Operator_ID;

        private System.Nullable<int> _Calendar_ID;

        private string _Operator_Name;

        private string _Operator_Address;

        private string _Operator_PostCode;

        private string _Operator_Depot_Phone;

        private string _Operator_Fax;

        private string _Operator_EMail;

        private string _Operator_Contact;

        private string _Operator_Invoice_Address;

        private string _Operator_Invoice_Postcode;

        private string _Operator_Invoice_Name;

        private string _Operator_Start_Date;

        private string _Operator_End_Date;

        private string _Operator_AMEDIS_Code;

        private string _Operator_Logo_Reference;

        private string _Operator_Account_Name;

        private string _Operator_Sort_Code;

        private string _Operator_Account_No;

        public rsp_GetOperatorDetailsResult()
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
                    this._Calendar_ID = value;
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

        [Column(Storage = "_Operator_Address", DbType = "NText", UpdateCheck = UpdateCheck.Never)]
        public string Operator_Address
        {
            get
            {
                return this._Operator_Address;
            }
            set
            {
                if ((this._Operator_Address != value))
                {
                    this._Operator_Address = value;
                }
            }
        }

        [Column(Storage = "_Operator_PostCode", DbType = "VarChar(15)")]
        public string Operator_PostCode
        {
            get
            {
                return this._Operator_PostCode;
            }
            set
            {
                if ((this._Operator_PostCode != value))
                {
                    this._Operator_PostCode = value;
                }
            }
        }

        [Column(Storage = "_Operator_Depot_Phone", DbType = "VarChar(15)")]
        public string Operator_Depot_Phone
        {
            get
            {
                return this._Operator_Depot_Phone;
            }
            set
            {
                if ((this._Operator_Depot_Phone != value))
                {
                    this._Operator_Depot_Phone = value;
                }
            }
        }

        [Column(Storage = "_Operator_Fax", DbType = "VarChar(15)")]
        public string Operator_Fax
        {
            get
            {
                return this._Operator_Fax;
            }
            set
            {
                if ((this._Operator_Fax != value))
                {
                    this._Operator_Fax = value;
                }
            }
        }

        [Column(Storage = "_Operator_EMail", DbType = "VarChar(100)")]
        public string Operator_EMail
        {
            get
            {
                return this._Operator_EMail;
            }
            set
            {
                if ((this._Operator_EMail != value))
                {
                    this._Operator_EMail = value;
                }
            }
        }

        [Column(Storage = "_Operator_Contact", DbType = "VarChar(50)")]
        public string Operator_Contact
        {
            get
            {
                return this._Operator_Contact;
            }
            set
            {
                if ((this._Operator_Contact != value))
                {
                    this._Operator_Contact = value;
                }
            }
        }

        [Column(Storage = "_Operator_Invoice_Address", DbType = "NText", UpdateCheck = UpdateCheck.Never)]
        public string Operator_Invoice_Address
        {
            get
            {
                return this._Operator_Invoice_Address;
            }
            set
            {
                if ((this._Operator_Invoice_Address != value))
                {
                    this._Operator_Invoice_Address = value;
                }
            }
        }

        [Column(Storage = "_Operator_Invoice_Postcode", DbType = "VarChar(50)")]
        public string Operator_Invoice_Postcode
        {
            get
            {
                return this._Operator_Invoice_Postcode;
            }
            set
            {
                if ((this._Operator_Invoice_Postcode != value))
                {
                    this._Operator_Invoice_Postcode = value;
                }
            }
        }

        [Column(Storage = "_Operator_Invoice_Name", DbType = "VarChar(50)")]
        public string Operator_Invoice_Name
        {
            get
            {
                return this._Operator_Invoice_Name;
            }
            set
            {
                if ((this._Operator_Invoice_Name != value))
                {
                    this._Operator_Invoice_Name = value;
                }
            }
        }

        [Column(Storage = "_Operator_Start_Date", DbType = "VarChar(30)")]
        public string Operator_Start_Date
        {
            get
            {
                return this._Operator_Start_Date;
            }
            set
            {
                if ((this._Operator_Start_Date != value))
                {
                    this._Operator_Start_Date = value;
                }
            }
        }

        [Column(Storage = "_Operator_End_Date", DbType = "VarChar(30)")]
        public string Operator_End_Date
        {
            get
            {
                return this._Operator_End_Date;
            }
            set
            {
                if ((this._Operator_End_Date != value))
                {
                    this._Operator_End_Date = value;
                }
            }
        }

        [Column(Storage = "_Operator_AMEDIS_Code", DbType = "VarChar(4)")]
        public string Operator_AMEDIS_Code
        {
            get
            {
                return this._Operator_AMEDIS_Code;
            }
            set
            {
                if ((this._Operator_AMEDIS_Code != value))
                {
                    this._Operator_AMEDIS_Code = value;
                }
            }
        }

        [Column(Storage = "_Operator_Logo_Reference", DbType = "VarChar(50)")]
        public string Operator_Logo_Reference
        {
            get
            {
                return this._Operator_Logo_Reference;
            }
            set
            {
                if ((this._Operator_Logo_Reference != value))
                {
                    this._Operator_Logo_Reference = value;
                }
            }
        }

        [Column(Storage = "_Operator_Account_Name", DbType = "VarChar(32)")]
        public string Operator_Account_Name
        {
            get
            {
                return this._Operator_Account_Name;
            }
            set
            {
                if ((this._Operator_Account_Name != value))
                {
                    this._Operator_Account_Name = value;
                }
            }
        }

        [Column(Storage = "_Operator_Sort_Code", DbType = "VarChar(8)")]
        public string Operator_Sort_Code
        {
            get
            {
                return this._Operator_Sort_Code;
            }
            set
            {
                if ((this._Operator_Sort_Code != value))
                {
                    this._Operator_Sort_Code = value;
                }
            }
        }

        [Column(Storage = "_Operator_Account_No", DbType = "VarChar(12)")]
        public string Operator_Account_No
        {
            get
            {
                return this._Operator_Account_No;
            }
            set
            {
                if ((this._Operator_Account_No != value))
                {
                    this._Operator_Account_No = value;
                }
            }
        }
    }
}
