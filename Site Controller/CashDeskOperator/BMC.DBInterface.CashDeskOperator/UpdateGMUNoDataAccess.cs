using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;
using System.Data.Linq;
using BMC.DataAccess;
using System.Reflection;

namespace BMC.DBInterface.CashDeskOperator
{
    [DatabaseAttribute(Name = "Exchange")]
    public partial class UpdateGMUNoDataAccess : DataContext
    {
        static MappingSource mappingSource = new AttributeMappingSource();

        #region Extensibility Method Definitions
        partial void OnCreated();
        #endregion

        public UpdateGMUNoDataAccess(string connection) :
            base(connection, mappingSource)
        {
            OnCreated();
            this.CommandTimeout = SqlHelper.LoadCommandTimeout();
        }

        [Function(Name = "dbo.rsp_GetAGSdetails")]
        public ISingleResult<GetAGSdetailsResult> GetAGSdetails()
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
            return ((ISingleResult<GetAGSdetailsResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.FN_CheckAGSCombination", IsComposable = true)]
        public System.Nullable<bool> FN_CheckAGSCombination([Parameter(Name = "ActAssetNo", DbType = "VarChar(50)")] string actAssetNo, [Parameter(Name = "NewGMUNo", DbType = "VarChar(50)")] string newGMUNo, [Parameter(Name = "ActSerialNo", DbType = "VarChar(50)")] string actSerialNo)
        {
            return ((System.Nullable<bool>)(this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), actAssetNo, newGMUNo, actSerialNo).ReturnValue));
        }

        [Function(Name = "dbo.rsp_ExportAGSDetails")]
        public ISingleResult<rsp_ExportAGSDetailsResult> ExportAGSDetails([Parameter(Name = "MACHINE_No", DbType = "Int")] System.Nullable<int> mACHINE_No, [Parameter(Name = "NewGMUNo", DbType = "VarChar(50)")] string newGMUNo)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), mACHINE_No, newGMUNo);
            return ((ISingleResult<rsp_ExportAGSDetailsResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_UpdateGMUDetails")]
        public int UpdateGMUDetails([Parameter(Name = "MACHINE_No", DbType = "Int")] System.Nullable<int> mACHINE_No, [Parameter(Name = "NewGMUNo", DbType = "VarChar(50)")] string newGMUNo, [Parameter(Name = "Installation_No", DbType = "Int")] System.Nullable<int> installation_No)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), mACHINE_No, newGMUNo, installation_No);
            return ((int)(result.ReturnValue));
        }

        [Function(Name = "dbo.usp_UpdateGMUDownEvent")]
        public int UpdateGMUDownEvent([Parameter(Name = "Installation_No", DbType = "Int")] System.Nullable<int> installation_No)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), installation_No);
            return ((int)(result.ReturnValue));
        }

    }

    public partial class GetAGSdetailsResult
    {

        private string _AssetNo;

        private string _BarPositionNo;

        private System.Nullable<int> _Installation_No;

        private string _ActualAssetNo;

        private string _GMUNo;

        private string _SerialNo;

        private string _EditSave;

        private System.Nullable<int> _MachineID;

        public GetAGSdetailsResult()
        {
        }

        [Column(Storage = "_MachineID", DbType = "Int")]
        public System.Nullable<int> MachineID
        {
            get
            {
                return this._MachineID;
            }
            set
            {
                if ((this._MachineID != value))
                {
                    this._MachineID = value;
                }
            }
        }

        [Column(Storage = "_AssetNo", DbType = "VarChar(50)")]
        public string AssetNo
        {
            get
            {
                return this._AssetNo;
            }
            set
            {
                if ((this._AssetNo != value))
                {
                    this._AssetNo = value;
                }
            }
        }

        [Column(Storage = "_BarPositionNo", DbType = "VarChar(50)")]
        public string BarPositionNo
        {
            get
            {
                return this._BarPositionNo;
            }
            set
            {
                if ((this._BarPositionNo != value))
                {
                    this._BarPositionNo = value;
                }
            }
        }

        [Column(Storage = "_Installation_No", DbType = "Int")]
        public System.Nullable<int> Installation_No
        {
            get
            {
                return this._Installation_No;
            }
            set
            {
                if ((this._Installation_No != value))
                {
                    this._Installation_No = value;
                }
            }
        }

        [Column(Storage = "_ActualAssetNo", DbType = "VarChar(50)")]
        public string ActualAssetNo
        {
            get
            {
                return this._ActualAssetNo;
            }
            set
            {
                if ((this._ActualAssetNo != value))
                {
                    this._ActualAssetNo = value;
                }
            }
        }

        [Column(Storage = "_GMUNo", DbType = "VarChar(50)")]
        public string GMUNo
        {
            get
            {
                return this._GMUNo;
            }
            set
            {
                if ((this._GMUNo != value))
                {
                    this._GMUNo = value;
                }
            }
        }

        [Column(Storage = "_SerialNo", DbType = "VarChar(50)")]
        public string SerialNo
        {
            get
            {
                return this._SerialNo;
            }
            set
            {
                if ((this._SerialNo != value))
                {
                    this._SerialNo = value;
                }
            }
        }

        public string EditSave
        {
            get
            {

                return this._EditSave;

            }
            set
            {
                if ((this._EditSave != value))
                {
                    this._EditSave = value;
                }
            }
        }
    }

    public partial class rsp_ExportAGSDetailsResult
    {

        private string _XmlString;

        public rsp_ExportAGSDetailsResult()
        {
        }

        [Column(Storage = "_XmlString", DbType = "NVarChar(MAX)")]
        public string XmlString
        {
            get
            {
                return this._XmlString;
            }
            set
            {
                if ((this._XmlString != value))
                {
                    this._XmlString = value;
                }
            }
        }
    }



}
