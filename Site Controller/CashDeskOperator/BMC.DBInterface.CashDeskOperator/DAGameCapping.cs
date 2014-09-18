using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;
using System.Data.Linq;
using System.Reflection;
using BMC.DataAccess;

namespace BMC.DBInterface.CashDeskOperator
{
    [DatabaseAttribute(Name = "Exchange")]
    public partial class DAGameCapping : DataContext
    {
        static MappingSource mappingSource = new AttributeMappingSource();

        #region Extensibility Method Definitions
        partial void OnCreated();
        #endregion

        public DAGameCapping(string connection) :
            base(connection, mappingSource)
        {
            OnCreated();
            this.CommandTimeout = SqlHelper.LoadCommandTimeout();
        }


        [Function(Name = "dbo.rsp_GetGameCapDetails")]
        public ISingleResult<rsp_GetGameCapDetailsResult> GetGameCapDetails()
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
            return ((ISingleResult<rsp_GetGameCapDetailsResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.usp_UpdateGameCapDetails")]
        public ISingleResult<usp_UpdateGameCapDetailsResult> UpdateGameCapDetails([Parameter(Name = "Installation_No", DbType = "Int")] System.Nullable<int> installation_No, [Parameter(Name = "GameCappingID", DbType = "Int")] System.Nullable<int> gameCappingID, [Parameter(Name = "CardNo", DbType = "VarChar(30)")] string cardNo, [Parameter(Name = "ReleasedBy", DbType = "VarChar(200)")] string userName)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), installation_No, gameCappingID, cardNo, userName);
            return ((ISingleResult<usp_UpdateGameCapDetailsResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_ValidateGameCapInformation")]
        public ISingleResult<rsp_ValidateGameCapInformationResult> ValidateGameCapInformation([Parameter(Name = "Installation_No", DbType = "Int")] System.Nullable<int> installation_No, [Parameter(Name = "CardNo", DbType = "VarChar(30)")] string cardNo, [Parameter(Name = "IsGameUnCap", DbType = "Bit")] System.Nullable<bool> isGameUnCap)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), installation_No, cardNo, isGameUnCap);
            return ((ISingleResult<rsp_ValidateGameCapInformationResult>)(result.ReturnValue));
        }
    }

    public partial class rsp_GetGameCapDetailsResult
    {
        private int _GameCappingID;

        private int _InstallationNo;

        private string _Position;

        private string _ReservedBy;

        private string _ReservedFor;

        private System.Nullable<System.DateTime> _SessionStartTime;

        private long _ElapsedSec;

        private long _AlertUnCapSec;

        private bool _AlertCame;

        public rsp_GetGameCapDetailsResult()
        {
        }

        [Column(Storage = "_GameCappingID", DbType = "Int NOT NULL")]
        public int GameCappingID
        {
            get
            {
                return this._GameCappingID;
            }
            set
            {
                if ((this._GameCappingID != value))
                {
                    this._GameCappingID = value;
                }
            }
        }

        [Column(Storage = "_InstallationNo", DbType = "Int NOT NULL")]
        public int InstallationNo
        {
            get
            {
                return this._InstallationNo;
            }
            set
            {
                if ((this._InstallationNo != value))
                {
                    this._InstallationNo = value;
                }
            }
        }

        [Column(Storage = "_Position", DbType = "VarChar(30)")]
        public string Position
        {
            get
            {
                return this._Position;
            }
            set
            {
                if ((this._Position != value))
                {
                    this._Position = value;
                }
            }
        }

        [Column(Storage = "_ReservedBy", DbType = "VarChar(32)")]
        public string ReservedBy
        {
            get
            {
                return this._ReservedBy;
            }
            set
            {
                if ((this._ReservedBy != value))
                {
                    this._ReservedBy = value;
                }
            }
        }

        [Column(Storage = "_ReservedFor", DbType = "VarChar(32)")]
        public string ReservedFor
        {
            get
            {
                return this._ReservedFor;
            }
            set
            {
                if ((this._ReservedFor != value))
                {
                    this._ReservedFor = value;
                }
            }
        }

        [Column(Storage = "_SessionStartTime", DbType = "DateTime")]
        public System.Nullable<System.DateTime> SessionStartTime
        {
            get
            {
                return this._SessionStartTime;
            }
            set
            {
                if ((this._SessionStartTime != value))
                {
                    this._SessionStartTime = value;
                }
            }
        }

        [Column(Storage = "_ElapsedSec", DbType = "BigInt NOT NULL")]
        public long ElapsedSec
        {
            get
            {
                return this._ElapsedSec;
            }
            set
            {
                if ((this._ElapsedSec != value))
                {
                    this._ElapsedSec = value;
                }
            }
        }

        [Column(Storage = "_AlertUnCapSec", DbType = "BigInt NOT NULL")]
        public long AlertUnCapSec
        {
            get
            {
                return this._AlertUnCapSec;
            }
            set
            {
                if ((this._AlertUnCapSec != value))
                {
                    this._AlertUnCapSec = value;
                }
            }
        }

        [Column(Storage = "_AlertCame", DbType = "BIT NOT NULL")]
        public bool AlertCame
        {
            get
            {
                return this._AlertCame;
            }
            set
            {
                if ((this._AlertCame != value))
                {
                    this._AlertCame = value;
                }
            }
        }
    }

    public partial class usp_UpdateGameCapDetailsResult
    {

        private string _Message;

        private int _ReserveGameAsset;

        private int _MaxCapNotExceeded;

        private int _SelfReserve;

        private int _AllowReserve;

        private string _TimeOption;

        private int _AutoRelease;

        private int _ExpireMinstoAlert;

        public usp_UpdateGameCapDetailsResult()
        {
        }

        [Column(Storage = "_Message", DbType = "VarChar(50)")]        
        public string Message
        {
            get
            {
                return this._Message;
            }
            set
            {
                if ((this._Message != value))
                {
                    this._Message = value;
                }
            }
        }

        [Column(Storage = "_ReserveGameAsset", DbType = "Int NOT NULL")]
        public int ReserveGameAsset
        {
            get
            {
                return this._ReserveGameAsset;
            }
            set
            {
                if ((this._ReserveGameAsset != value))
                {
                    this._ReserveGameAsset = value;
                }
            }
        }

        [Column(Storage = "_MaxCapNotExceeded", DbType = "Int NOT NULL")]
        public int MaxCapNotExceeded
        {
            get
            {
                return this._MaxCapNotExceeded;
            }
            set
            {
                if ((this._MaxCapNotExceeded != value))
                {
                    this._MaxCapNotExceeded = value;
                }
            }
        }

        [Column(Storage = "_SelfReserve", DbType = "Int NOT NULL")]
        public int SelfReserve
        {
            get
            {
                return this._SelfReserve;
            }
            set
            {
                if ((this._SelfReserve != value))
                {
                    this._SelfReserve = value;
                }
            }
        }

        [Column(Storage = "_AllowReserve", DbType = "Int NOT NULL")]
        public int AllowReserve
        {
            get
            {
                return this._AllowReserve;
            }
            set
            {
                if ((this._AllowReserve != value))
                {
                    this._AllowReserve = value;
                }
            }
        }

        [Column(Storage = "_TimeOption", DbType = "VarChar(1) NOT NULL", CanBeNull = false)]
        public string TimeOption
        {
            get
            {
                return this._TimeOption;
            }
            set
            {
                if ((this._TimeOption != value))
                {
                    this._TimeOption = value;
                }
            }
        }

        [Column(Storage = "_AutoRelease", DbType = "Int NOT NULL", CanBeNull = false)]
        public int AutoRelease
        {
            get
            {
                return this._AutoRelease;
            }
            set
            {
                if ((this._AutoRelease != value))
                {
                    this._AutoRelease = value;
                }
            }
        }

        [Column(Storage = "_ExpireMinstoAlert", DbType = "Int NOT NULL", CanBeNull = false)]
        public int ExpireMinstoAlert
        {
            get
            {
                return this._ExpireMinstoAlert;
            }
            set
            {
                if ((this._ExpireMinstoAlert != value))
                {
                    this._ExpireMinstoAlert = value;
                }
            }
        }
    }

    public partial class rsp_ValidateGameCapInformationResult
    {

        private string _Message;

        private int _ReserveGameAsset;

        private int _MaxCapNotExceeded;

        private int _SelfReserve;

        private int _AllowReserve;

        private string _TimeOption;

        private int _AutoRelease;

        private int _ExpireMinstoAlert;

        public rsp_ValidateGameCapInformationResult()
        {
        }

        [Column(Storage = "_Message", DbType = "VarChar(4) NOT NULL", CanBeNull = false)]
        public string Message
        {
            get
            {
                return this._Message;
            }
            set
            {
                if ((this._Message != value))
                {
                    this._Message = value;
                }
            }
        }

        [Column(Storage = "_ReserveGameAsset", DbType = "Int NOT NULL")]
        public int ReserveGameAsset
        {
            get
            {
                return this._ReserveGameAsset;
            }
            set
            {
                if ((this._ReserveGameAsset != value))
                {
                    this._ReserveGameAsset = value;
                }
            }
        }

        [Column(Storage = "_MaxCapNotExceeded", DbType = "Int NOT NULL")]
        public int MaxCapNotExceeded
        {
            get
            {
                return this._MaxCapNotExceeded;
            }
            set
            {
                if ((this._MaxCapNotExceeded != value))
                {
                    this._MaxCapNotExceeded = value;
                }
            }
        }

        [Column(Storage = "_SelfReserve", DbType = "Int NOT NULL")]
        public int SelfReserve
        {
            get
            {
                return this._SelfReserve;
            }
            set
            {
                if ((this._SelfReserve != value))
                {
                    this._SelfReserve = value;
                }
            }
        }

        [Column(Storage = "_AllowReserve", DbType = "Int NOT NULL")]
        public int AllowReserve
        {
            get
            {
                return this._AllowReserve;
            }
            set
            {
                if ((this._AllowReserve != value))
                {
                    this._AllowReserve = value;
                }
            }
        }

        [Column(Storage = "_TimeOption", DbType = "VarChar(1) NOT NULL", CanBeNull = false)]
        public string TimeOption
        {
            get
            {
                return this._TimeOption;
            }
            set
            {
                if ((this._TimeOption != value))
                {
                    this._TimeOption = value;
                }
            }
        }

        [Column(Storage = "_AutoRelease", DbType = "Int NOT NULL")]
        public int AutoRelease
        {
            get
            {
                return this._AutoRelease;
            }
            set
            {
                if ((this._AutoRelease != value))
                {
                    this._AutoRelease = value;
                }
            }
        }

        [Column(Storage = "_ExpireMinstoAlert", DbType = "Int NOT NULL")]
        public int ExpireMinstoAlert
        {
            get
            {
                return this._ExpireMinstoAlert;
            }
            set
            {
                if ((this._ExpireMinstoAlert != value))
                {
                    this._ExpireMinstoAlert = value;
                }
            }
        }
    }
}
