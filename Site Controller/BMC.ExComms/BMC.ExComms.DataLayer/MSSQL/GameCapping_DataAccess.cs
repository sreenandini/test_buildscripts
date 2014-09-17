using BMC.CoreLib;
using BMC.CoreLib.Diagnostics;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Reflection;
using System.Text;

namespace BMC.ExComms.DataLayer.MSSQL
{
    public partial class ExCommsDataContext
    {
        public GameCapInformation GetGameCappingInformation(int installationNo, string cardNo, bool isGameUnCapping)
        {
            try
            {
                using (ExCommsSQLDataAccess DataContext = this.GetDataContext())
                {
                    return DataContext.GetGameCappingInformation(installationNo, cardNo, isGameUnCapping).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                Log.Exception(ex);
                return null;
            }
        }

        public int StartGameCapSession(int installationNo, string slot, string stand, string reservedCardNo, string reservedForCardNo)
        {
            try
            {
                using (ExCommsSQLDataAccess DataContext = this.GetDataContext())
                {
                    return DataContext.StartGameCappingSession(installationNo, slot, stand, reservedCardNo, reservedForCardNo);
                }
            }
            catch (Exception ex)
            {
                Log.Exception(ex);
                return -1;
            }
        }

        public void AlertGameCappingSessionExpires(int installationNo)
        {
            try
            {
                using (ExCommsSQLDataAccess DataContext = this.GetDataContext())
                {
                    //DataContext.AlertSessionExpires(installationNo);
                }
            }
            catch (Exception ex)
            {
                Log.Exception(ex);
            }
        }

        public void EndGameCapSession(int installationNo, string releasedCardNo)
        {
            try
            {
                using (ExCommsSQLDataAccess DataContext = this.GetDataContext())
                {
                    DataContext.EndGameCappingSession(installationNo, 0, releasedCardNo, string.Empty);
                }
            }
            catch (Exception ex)
            {
                Log.Exception(ex);
            }
        }

        public void UpdateGameCappingDetails(string cardNo, System.Nullable<int> cardLevel, System.Nullable<bool> playerCard)
        {
            try
            {
                using (ExCommsSQLDataAccess DataContext = this.GetDataContext())
                {
                    DataContext.usp_GameCapInformation(cardNo, cardLevel, playerCard);
                }
            }
            catch (Exception ex)
            {
                Log.Exception(ex);
            }
        }
    }

    public partial class ExCommsSQLDataAccess : System.Data.Linq.DataContext
    {
        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.rsp_ValidateGameCapInformation")]
        public ISingleResult<GameCapInformation> GetGameCappingInformation([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Installation_No", DbType = "Int")] System.Nullable<int> installation_No, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "CardNo", DbType = "VarChar(30)")] string cardNo, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "IsGameUnCap", DbType = "Bit")] System.Nullable<bool> isGameUnCap)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), installation_No, cardNo, isGameUnCap);
            return ((ISingleResult<GameCapInformation>)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.rsp_ValidateGameCapInformation")]
        public ISingleResult<GameUnCapInformation> GetGameUnCappingInformation([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Installation_No", DbType = "Int")] System.Nullable<int> installation_No, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "CardNo", DbType = "VarChar(30)")] string cardNo, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "IsGameUnCap", DbType = "Bit")] System.Nullable<bool> isGameUnCap)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), installation_No, cardNo, isGameUnCap);
            return ((ISingleResult<GameUnCapInformation>)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.usp_TrackGameCapInformation")]
        public int StartGameCappingSession([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Installation_No", DbType = "Int")] System.Nullable<int> installation_No, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Slot", DbType = "VarChar(30)")] string slot, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Stand", DbType = "VarChar(30)")] string stand, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "ReservedCardNo", DbType = "VarChar(30)")] string reservedCardNo, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "ReservedForCardNo", DbType = "VarChar(30)")] string reservedForCardNo)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), installation_No, slot, stand, reservedCardNo, reservedForCardNo);
            return ((int)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.usp_UpdateGameCapAlert")]
        public int AlertGameCappingSessionExpires([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Installation_No", DbType = "Int")] System.Nullable<int> installation_No)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), installation_No);
            return ((int)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.usp_UpdateGameCapDetails")]
        public ISingleResult<GameCapInformation> EndGameCappingSession([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Installation_No", DbType = "Int")] System.Nullable<int> installation_No, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "GameCappingID", DbType = "Int")] System.Nullable<int> gameCappingID, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "CardNo", DbType = "VarChar(30)")] string cardNo, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "ReleasedBy", DbType = "VarChar(200)")] string releasedBy)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), installation_No, gameCappingID, cardNo, releasedBy);
            return ((ISingleResult<GameCapInformation>)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.usp_GameCapInformation")]
        public int usp_GameCapInformation([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "CardNo", DbType = "VarChar(20)")] string cardNo, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "CardLevel", DbType = "Int")] System.Nullable<int> cardLevel, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "PlayerCard", DbType = "Bit")] System.Nullable<bool> playerCard)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), cardNo, cardLevel, playerCard);
            return ((int)(result.ReturnValue));
        }
    }

    public partial class GameCapInformation
    {
        private string _Position;

        private string _Asset;

        private string _Message;

        private int _ReserveGameAsset;

        private int _MaxCapNotExceeded;

        private int _SelfReserve;

        private int _AllowReserve;

        private string _TimeOption;

        private int _AutoRelease;

        private int _ExpireMinstoAlert;

        public GameCapInformation()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Position", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Asset", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
        public string Asset
        {
            get
            {
                return this._Asset;
            }
            set
            {
                if ((this._Asset != value))
                {
                    this._Asset = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Message", DbType = "VarChar(15) NOT NULL", CanBeNull = false)]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ReserveGameAsset", DbType = "Int NOT NULL")]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_MaxCapNotExceeded", DbType = "Int NOT NULL")]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_SelfReserve", DbType = "Int NOT NULL")]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_AllowReserve", DbType = "Int NOT NULL")]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_TimeOption", DbType = "VarChar(1) NOT NULL", CanBeNull = false)]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_AutoRelease", DbType = "Int NOT NULL")]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ExpireMinstoAlert", DbType = "Int NOT NULL")]
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

    public partial class GameUnCapInformation
    {

        private string _Message;

        private int _ReserveGameAsset;

        private int _MaxCapNotExceeded;

        private int _SelfReserve;

        private int _AllowReserve;

        private string _TimeOption;

        private int _AutoRelease;

        private int _ExpireMinstoAlert;

        public GameUnCapInformation()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Message", DbType = "VarChar(15) NOT NULL", CanBeNull = false)]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ReserveGameAsset", DbType = "Int NOT NULL")]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_MaxCapNotExceeded", DbType = "Int NOT NULL")]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_SelfReserve", DbType = "Int NOT NULL")]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_AllowReserve", DbType = "Int NOT NULL")]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_TimeOption", DbType = "VarChar(1) NOT NULL", CanBeNull = false)]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_AutoRelease", DbType = "Int NOT NULL")]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ExpireMinstoAlert", DbType = "Int NOT NULL")]
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
