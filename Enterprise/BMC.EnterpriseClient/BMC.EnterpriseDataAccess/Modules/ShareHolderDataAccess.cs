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
        [Function(Name = "dbo.rsp_GetShareHolder")]
        public ISingleResult<rsp_GetShareHolderResult> rsp_GetShareHolder()
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
            return ((ISingleResult<rsp_GetShareHolderResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.usp_DeleteShareHolder")]
        public int usp_DeleteShareHolder([Parameter(Name = "ShareHolderId", DbType = "Int")] System.Nullable<int> shareHolderId, [Parameter(Name = "Status", DbType = "Int")] ref System.Nullable<int> status)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), shareHolderId, status);
            status = ((System.Nullable<int>)(result.GetParameterValue(1)));
            return ((int)(result.ReturnValue));
        }

        [Function(Name = "dbo.usp_UpdateShareHolderDetails")]
        public int usp_UpdateShareHolderDetails([Parameter(Name = "ShareHolderId", DbType = "Int")] System.Nullable<int> shareHolderId, [Parameter(Name = "ShareHolderName", DbType = "VarChar(50)")] string shareHolderName, [Parameter(Name = "ShareHolderDescription", DbType = "VarChar(255)")] string shareHolderDescription)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), shareHolderId, shareHolderName, shareHolderDescription);
            return ((int)(result.ReturnValue));
        }

        [Function(Name = "dbo.usp_InsertShareHolderDetails")]
        public int usp_InsertShareHolderDetails([Parameter(Name = "ShareHolderName", DbType = "VarChar(50)")] string shareHolderName, [Parameter(Name = "ShareHolderDescription", DbType = "VarChar(255)")] string shareHolderDescription)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), shareHolderName, shareHolderDescription);
            return ((int)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_CheckShareHolderNameExists")]
        public int rsp_CheckShareHolderNameExists([Parameter(Name = "ShareHolderName", DbType = "VarChar(50)")] string shareHolderName, [Parameter(Name = "ShareHolderId", DbType = "Int")] System.Nullable<int> shareHolderId, [Parameter(Name = "NameCount", DbType = "Int")] ref System.Nullable<int> nameCount)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), shareHolderName, shareHolderId, nameCount);
            nameCount = ((System.Nullable<int>)(result.GetParameterValue(2)));
            return ((int)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_CheckShareholder_ExistsIn_PSG_Or_ESG")]
        public int rsp_CheckShareholder_ExistsIn_PSG_Or_ESG([Parameter(Name = "ShareholderId", DbType = "Int")] int shareholderId, [Parameter(Name = "IsExists", DbType = "Bit")] ref System.Nullable<bool> bIsExists)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), shareholderId, bIsExists);
            bIsExists = ((System.Nullable<bool>)(result.GetParameterValue(1))).GetValueOrDefault();
            return ((int)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetCommonProfitShareGroups")]
        public ISingleResult<rsp_GetCommonProfitShareGroupsResult> rsp_GetCommonProfitShareGroups([Parameter(Name = "CommonProfitShareType", DbType = "SmallInt")] System.Nullable<short> commonProfitShareType)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), commonProfitShareType);
            return ((ISingleResult<rsp_GetCommonProfitShareGroupsResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetCommonProfitShares")]
        public ISingleResult<rsp_GetCommonProfitSharesResult> rsp_GetCommonProfitShares([Parameter(Name = "CommonProfitShareType", DbType = "SmallInt")] System.Nullable<short> commonProfitShareType, [Parameter(Name = "ParentGroupId", DbType = "Int")] System.Nullable<int> parentGroupId)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), commonProfitShareType, parentGroupId);
            return ((ISingleResult<rsp_GetCommonProfitSharesResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetCommonShareHolders")]
        public ISingleResult<rsp_GetCommonShareHoldersResult> rsp_GetCommonShareHolders([Parameter(Name = "CommonProfitShareType", DbType = "SmallInt")] System.Nullable<short> commonProfitShareType, [Parameter(Name = "ShareGroupId", DbType = "Int")] System.Nullable<int> shareGroupId, [Parameter(Name = "ShareId", DbType = "Int")] System.Nullable<int> shareId)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), commonProfitShareType, shareGroupId, shareId);
            return ((ISingleResult<rsp_GetCommonShareHoldersResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetCommonProfitSharePercentage")]
        public ISingleResult<rsp_GetCommonProfitSharePercentageResult> rsp_GetCommonProfitSharePercentage([Parameter(Name = "CommonProfitShareType", DbType = "SmallInt")] System.Nullable<short> commonProfitShareType, [Parameter(Name = "ParentGroupId", DbType = "Int")] System.Nullable<int> parentGroupId, [Parameter(Name = "ShareId", DbType = "Int")] System.Nullable<int> shareId)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), commonProfitShareType, parentGroupId, shareId);
            return ((ISingleResult<rsp_GetCommonProfitSharePercentageResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.usp_DeleteExpenseShareGroup")]
        public int usp_DeleteExpenseShareGroup([Parameter(Name = "ExpenseShareGroupId", DbType = "Int")] System.Nullable<int> expenseShareGroupId, [Parameter(Name = "Status", DbType = "Int")] ref System.Nullable<int> status)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), expenseShareGroupId, status);
            status = ((System.Nullable<int>)(result.GetParameterValue(1)));
            return ((int)(result.ReturnValue));
        }

        [Function(Name = "dbo.usp_DeleteProfitShareGroup")]
        public int usp_DeleteProfitShareGroup([Parameter(Name = "ProfitShareGroupId", DbType = "Int")] System.Nullable<int> profitShareGroupId, [Parameter(Name = "Status", DbType = "Int")] ref System.Nullable<int> status)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), profitShareGroupId, status);
            status = ((System.Nullable<int>)(result.GetParameterValue(1)));
            return ((int)(result.ReturnValue));
        }

        [Function(Name = "dbo.usp_DeleteExpenseShare")]
        public int usp_DeleteExpenseShare([Parameter(Name = "ExpenseShareGroupId", DbType = "Int")] System.Nullable<int> expenseShareGroupId, [Parameter(Name = "ExpenseShareId", DbType = "Int")] System.Nullable<int> expenseShareId, [Parameter(Name = "Status", DbType = "Int")] ref System.Nullable<int> status)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), expenseShareGroupId, expenseShareId, status);
            status = ((System.Nullable<int>)(result.GetParameterValue(2)));
            return ((int)(result.ReturnValue));
        }

        [Function(Name = "dbo.usp_DeleteProfitShare")]
        public int usp_DeleteProfitShare([Parameter(Name = "ProfitShareGroupId", DbType = "Int")] System.Nullable<int> profitShareGroupId, [Parameter(Name = "ProfitShareId", DbType = "Int")] System.Nullable<int> profitShareId, [Parameter(Name = "Status", DbType = "Int")] ref System.Nullable<int> status)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), profitShareGroupId, profitShareId, status);
            status = ((System.Nullable<int>)(result.GetParameterValue(2)));
            return ((int)(result.ReturnValue));
        }
        [Function(Name = "dbo.usp_UpdateExpenseShareGroupDetails")]
        public int usp_UpdateExpenseShareGroupDetails([Parameter(Name = "ExpenseShareGroupId", DbType = "Int")] System.Nullable<int> expenseShareGroupId, [Parameter(Name = "ExpenseShareGroupName", DbType = "VarChar(50)")] string expenseShareGroupName, [Parameter(Name = "ExpenseSharePercentage", DbType = "Float")] System.Nullable<double> expenseSharePercentage, [Parameter(Name = "ExpenseShareGroupDescription", DbType = "VarChar(255)")] string expenseShareGroupDescription, [Parameter(Name = "ExpenseShareGroupIdOut", DbType = "Int")] ref System.Nullable<int> expenseShareGroupIdOut)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), expenseShareGroupId, expenseShareGroupName, expenseSharePercentage, expenseShareGroupDescription, expenseShareGroupIdOut);
            expenseShareGroupIdOut = ((System.Nullable<int>)(result.GetParameterValue(4)));
            return ((int)(result.ReturnValue));
        }

        [Function(Name = "dbo.usp_UpdateProfitShareGroupDetails")]
        public int usp_UpdateProfitShareGroupDetails([Parameter(Name = "ProfitShareGroupId", DbType = "Int")] System.Nullable<int> profitShareGroupId, [Parameter(Name = "ProfitShareGroupName", DbType = "VarChar(50)")] string profitShareGroupName, [Parameter(Name = "ProfitSharePercentage", DbType = "Float")] System.Nullable<double> profitSharePercentage, [Parameter(Name = "ProfitShareGroupDescription", DbType = "VarChar(255)")] string profitShareGroupDescription, [Parameter(Name = "ProfitShareGroupIdOut", DbType = "Int")] ref System.Nullable<int> profitShareGroupIdOut)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), profitShareGroupId, profitShareGroupName, profitSharePercentage, profitShareGroupDescription, profitShareGroupIdOut);
            profitShareGroupIdOut = ((System.Nullable<int>)(result.GetParameterValue(4)));
            return ((int)(result.ReturnValue));
        }

        [Function(Name = "dbo.usp_UpdateExpenseShareDetails")]
        public int usp_UpdateExpenseShareDetails([Parameter(Name = "ShareHolderId", DbType = "Int")] System.Nullable<int> shareHolderId, [Parameter(Name = "ExpenseShareGroupId", DbType = "Int")] System.Nullable<int> expenseShareGroupId, [Parameter(Name = "ExpenseShareId", DbType = "Int")] System.Nullable<int> expenseShareId, [Parameter(Name = "ExpenseSharePercentage", DbType = "Float")] System.Nullable<double> expenseSharePercentage, [Parameter(Name = "ExpenseShareDescription", DbType = "VarChar(255)")] string expenseShareDescription)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), shareHolderId, expenseShareGroupId, expenseShareId, expenseSharePercentage, expenseShareDescription);
            return ((int)(result.ReturnValue));
        }

        [Function(Name = "dbo.usp_UpdateProfitShareDetails")]
        public int usp_UpdateProfitShareDetails([Parameter(Name = "ShareHolderId", DbType = "Int")] System.Nullable<int> shareHolderId, [Parameter(Name = "ProfitShareGroupId", DbType = "Int")] System.Nullable<int> profitShareGroupId, [Parameter(Name = "ProfitShareId", DbType = "Int")] System.Nullable<int> profitShareId, [Parameter(Name = "ProfitSharePercentage", DbType = "Float")] System.Nullable<double> profitSharePercentage, [Parameter(Name = "ProfitShareDescription", DbType = "VarChar(255)")] string profitShareDescription)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), shareHolderId, profitShareGroupId, profitShareId, profitSharePercentage, profitShareDescription);
            return ((int)(result.ReturnValue));
        }
        [Function(Name = "dbo.rsp_CheckExpenseShareGroupNameExists")]
        public int rsp_CheckExpenseShareGroupNameExists([Parameter(Name = "ExpenseShareGroupName", DbType = "VarChar(50)")] string expenseShareGroupName, [Parameter(Name = "ExpenseShareGroupId", DbType = "Int")] System.Nullable<int> expenseShareGroupId, [Parameter(Name = "NameCount", DbType = "Int")] ref System.Nullable<int> nameCount)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), expenseShareGroupName, expenseShareGroupId, nameCount);
            nameCount = ((System.Nullable<int>)(result.GetParameterValue(2)));
            return ((int)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_CheckProfitShareGroupNameExists")]
        public int rsp_CheckProfitShareGroupNameExists([Parameter(Name = "ProfitShareGroupName", DbType = "VarChar(50)")] string profitShareGroupName, [Parameter(Name = "ProfitShareGroupId", DbType = "Int")] System.Nullable<int> profitShareGroupId, [Parameter(Name = "NameCount", DbType = "Int")] ref System.Nullable<int> nameCount)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), profitShareGroupName, profitShareGroupId, nameCount);
            nameCount = ((System.Nullable<int>)(result.GetParameterValue(2)));
            return ((int)(result.ReturnValue));
        }
    }

    public partial class rsp_GetShareHolderResult
    {

        private int _ShareHolderId;

        private string _ShareHolderName;

        private string _ShareHolderDescription;

        private System.Nullable<System.DateTime> _DateCreated;

        private System.Nullable<System.DateTime> _DateModified;

        private bool _SysDelete;

        public rsp_GetShareHolderResult()
        {
        }

        [Column(Storage = "_ShareHolderId", DbType = "Int NOT NULL")]
        public int ShareHolderId
        {
            get
            {
                return this._ShareHolderId;
            }
            set
            {
                if ((this._ShareHolderId != value))
                {
                    this._ShareHolderId = value;
                }
            }
        }

        [Column(Storage = "_ShareHolderName", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
        public string ShareHolderName
        {
            get
            {
                return this._ShareHolderName;
            }
            set
            {
                if ((this._ShareHolderName != value))
                {
                    this._ShareHolderName = value;
                }
            }
        }

        [Column(Storage = "_ShareHolderDescription", DbType = "VarChar(255)")]
        public string ShareHolderDescription
        {
            get
            {
                return this._ShareHolderDescription;
            }
            set
            {
                if ((this._ShareHolderDescription != value))
                {
                    this._ShareHolderDescription = value;
                }
            }
        }

        [Column(Storage = "_DateCreated", DbType = "DateTime")]
        public System.Nullable<System.DateTime> DateCreated
        {
            get
            {
                return this._DateCreated;
            }
            set
            {
                if ((this._DateCreated != value))
                {
                    this._DateCreated = value;
                }
            }
        }

        [Column(Storage = "_DateModified", DbType = "DateTime")]
        public System.Nullable<System.DateTime> DateModified
        {
            get
            {
                return this._DateModified;
            }
            set
            {
                if ((this._DateModified != value))
                {
                    this._DateModified = value;
                }
            }
        }

        [Column(Storage = "_SysDelete", DbType = "Bit NOT NULL")]
        public bool SysDelete
        {
            get
            {
                return this._SysDelete;
            }
            set
            {
                if ((this._SysDelete != value))
                {
                    this._SysDelete = value;
                }
            }
        }
    }

    public partial class rsp_GetCommonProfitShareGroupsResult
    {

        private int _Id;

        private string _Name;

        private double _Percentage;

        private string _Description;

        public rsp_GetCommonProfitShareGroupsResult()
        {
        }

        [Column(Storage = "_Id", DbType = "Int NOT NULL")]
        public int Id
        {
            get
            {
                return this._Id;
            }
            set
            {
                if ((this._Id != value))
                {
                    this._Id = value;
                }
            }
        }

        [Column(Storage = "_Name", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
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

        [Column(Storage = "_Percentage", DbType = "Float NOT NULL")]
        public double Percentage
        {
            get
            {
                return this._Percentage;
            }
            set
            {
                if ((this._Percentage != value))
                {
                    this._Percentage = value;
                }
            }
        }

        [Column(Storage = "_Description", DbType = "VarChar(255)")]
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
                    this._Description = value;
                }
            }
        }
    }

    public partial class rsp_GetCommonProfitSharesResult
    {

        private int _Id;

        private double _Percentage;

        private string _Description;

        private int _ShareHolderId;

        private string _ShareHolderName;

        public rsp_GetCommonProfitSharesResult()
        {
        }

        [Column(Storage = "_Id", DbType = "Int NOT NULL")]
        public int Id
        {
            get
            {
                return this._Id;
            }
            set
            {
                if ((this._Id != value))
                {
                    this._Id = value;
                }
            }
        }

        [Column(Storage = "_Percentage", DbType = "Float NOT NULL")]
        public double Percentage
        {
            get
            {
                return this._Percentage;
            }
            set
            {
                if ((this._Percentage != value))
                {
                    this._Percentage = value;
                }
            }
        }

        [Column(Storage = "_Description", DbType = "VarChar(255)")]
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
                    this._Description = value;
                }
            }
        }

        [Column(Storage = "_ShareHolderId", DbType = "Int NOT NULL")]
        public int ShareHolderId
        {
            get
            {
                return this._ShareHolderId;
            }
            set
            {
                if ((this._ShareHolderId != value))
                {
                    this._ShareHolderId = value;
                }
            }
        }

        [Column(Storage = "_ShareHolderName", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
        public string ShareHolderName
        {
            get
            {
                return this._ShareHolderName;
            }
            set
            {
                if ((this._ShareHolderName != value))
                {
                    this._ShareHolderName = value;
                }
            }
        }
    }
    public partial class rsp_GetCommonShareHoldersResult
    {

        private int _Id;

        private string _Name;

        public rsp_GetCommonShareHoldersResult()
        {
        }

        [Column(Storage = "_Id", DbType = "Int NOT NULL")]
        public int Id
        {
            get
            {
                return this._Id;
            }
            set
            {
                if ((this._Id != value))
                {
                    this._Id = value;
                }
            }
        }

        [Column(Storage = "_Name", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
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
    }

    public partial class rsp_GetCommonProfitSharePercentageResult
    {

        private System.Nullable<double> _TotalPercentage;

        public rsp_GetCommonProfitSharePercentageResult()
        {
        }

        [Column(Storage = "_TotalPercentage", DbType = "Float")]
        public System.Nullable<double> TotalPercentage
        {
            get
            {
                return this._TotalPercentage;
            }
            set
            {
                if ((this._TotalPercentage != value))
                {
                    this._TotalPercentage = value;
                }
            }
        }
    }
}