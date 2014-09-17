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
        [Function(Name = "dbo.esp_InsertCardLevelSettingsForGameCap")]
        public int InsertCardLevelSettingsForGameCap([Parameter(Name = "MaxNoofmachinestoCap", DbType = "Int")] System.Nullable<int> maxNoofmachinestoCap, [Parameter(Name = "CardLevel", DbType = "Int")] System.Nullable<int> cardLevel, [Parameter(Name = "MintstoCap", DbType = "VarChar(50)")] string mintstoCap, [Parameter(Name = "Site", DbType = "VarChar(20)")] string site, [Parameter(Name = "Staff_ID", DbType = "Int")] System.Nullable<int> staff_ID, [Parameter(Name = "Module_ID", DbType = "Int")] System.Nullable<int> module_ID, [Parameter(Name = "Module_Name", DbType = "VarChar(150)")] string module_Name, [Parameter(Name = "Screen_Name", DbType = "VarChar(150)")] string screen_Name)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), maxNoofmachinestoCap, cardLevel, mintstoCap, site, staff_ID, module_ID, module_Name, screen_Name);
            return ((int)(result.ReturnValue));
        }
        [Function(Name = "dbo.rsp_GetCardLevelSettings")]
        public ISingleResult<GetCardLevelSettingsResult> GetCardLevelSettings([Parameter(Name = "Site", DbType = "Int")] System.Nullable<int> site)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), site);
            return ((ISingleResult<GetCardLevelSettingsResult>)(result.ReturnValue));
        }
        [Function(Name = "dbo.esp_InsertGameCappingParameters")]
        public int InsertGameCappingParameters([Parameter(Name = "CapReleaseOnPlayerCardIn", DbType = "Bit")] System.Nullable<bool> capReleaseOnPlayerCardIn, [Parameter(Name = "ReserveGameForPlayer", DbType = "Bit")] System.Nullable<bool> reserveGameForPlayer, [Parameter(Name = "ReserveGameForEmployee", DbType = "Bit")] System.Nullable<bool> reserveGameForEmployee, [Parameter(Name = "MintsToExpire", DbType = "Int")] System.Nullable<int> mintsToExpire, [Parameter(Name = "Site", DbType = "Int")] System.Nullable<int> site, [Parameter(Name = "Staff_ID", DbType = "Int")] System.Nullable<int> staff_ID, [Parameter(Name = "Module_ID", DbType = "Int")] System.Nullable<int> module_ID, [Parameter(Name = "Module_Name", DbType = "VarChar(150)")] string module_Name, [Parameter(Name = "Screen_Name", DbType = "VarChar(150)")] string screen_Name)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), capReleaseOnPlayerCardIn, reserveGameForPlayer, reserveGameForEmployee, mintsToExpire, site, staff_ID, module_ID, module_Name, screen_Name);
            return ((int)(result.ReturnValue));
        }
        [Function(Name = "dbo.rsp_GetGameCappingParameters")]
        public ISingleResult<GetGameCappingParametersResult> GetGameCappingParameters([Parameter(Name = "Site", DbType = "Int")] System.Nullable<int> site)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), site);
            return ((ISingleResult<GetGameCappingParametersResult>)(result.ReturnValue));
        }        
        [Function(Name = "dbo.usp_DeleteGameCappingCardLevel")]
        public int DeleteGameCappingCardLevel([Parameter(Name = "CardLevel", DbType = "Int")] System.Nullable<int> cardLevel, [Parameter(Name = "Site", DbType = "VarChar(50)")] string site, [Parameter(Name = "Staff_ID", DbType = "Int")] System.Nullable<int> staff_ID, [Parameter(Name = "Module_ID", DbType = "Int")] System.Nullable<int> module_ID, [Parameter(Name = "Module_Name", DbType = "VarChar(150)")] string module_Name, [Parameter(Name = "Screen_Name", DbType = "VarChar(150)")] string screen_Name)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), cardLevel, site, staff_ID, module_ID, module_Name, screen_Name);
            return ((int)(result.ReturnValue));
        }
    }
    public partial class GetCardLevelSettingsResult
    {

        private int _CardLevel;

        private int _MaxNoofMachinestoCap;

        private string _MintstoCap;

        private int _SettingId;

        public GetCardLevelSettingsResult()
        {
        }

        [Column(Storage = "_CardLevel", DbType = "Int")]
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

        [Column(Storage = "_MaxNoofMachinestoCap", DbType = "Int NOT NULL")]
        public int MaxNoofMachinestoCap
        {
            get
            {
                return this._MaxNoofMachinestoCap;
            }
            set
            {
                if ((this._MaxNoofMachinestoCap != value))
                {
                    this._MaxNoofMachinestoCap = value;
                }
            }
        }

        [Column(Storage = "_MintstoCap", DbType = "VarChar(30)")]
        public string MintstoCap
        {
            get
            {
                return this._MintstoCap;
            }
            set
            {
                if ((this._MintstoCap != value))
                {
                    this._MintstoCap = value;
                }
            }
        }
        [Column(Storage = "_SettingId", DbType = "Int NOT NULL")]
        public int SettingId
        {
            get
            {
                return this._SettingId;
            }
            set
            {
                if ((this._SettingId != value))
                {
                    this._SettingId = value;
                }
            }
        }
    }
    public partial class GetGameCappingParametersResult
    {

        private int _GameCapID;

        private bool _CapReleaseOnPlayerCardIn;

        private bool _ReserveGameForPlayer;

        private bool _ReserveGameForEmployee;

        private int _MintsToExpire;

        private string _SITE;

        public GetGameCappingParametersResult()
        {
        }

        [Column(Storage = "_GameCapID", DbType = "Int NOT NULL")]
        public int GameCapID
        {
            get
            {
                return this._GameCapID;
            }
            set
            {
                if ((this._GameCapID != value))
                {
                    this._GameCapID = value;
                }
            }
        }

        [Column(Storage = "_CapReleaseOnPlayerCardIn", DbType = "Bit NOT NULL")]
        public bool CapReleaseOnPlayerCardIn
        {
            get
            {
                return this._CapReleaseOnPlayerCardIn;
            }
            set
            {
                if ((this._CapReleaseOnPlayerCardIn != value))
                {
                    this._CapReleaseOnPlayerCardIn = value;
                }
            }
        }

        [Column(Storage = "_ReserveGameForPlayer", DbType = "Bit NOT NULL")]
        public bool ReserveGameForPlayer
        {
            get
            {
                return this._ReserveGameForPlayer;
            }
            set
            {
                if ((this._ReserveGameForPlayer != value))
                {
                    this._ReserveGameForPlayer = value;
                }
            }
        }

        [Column(Storage = "_ReserveGameForEmployee", DbType = "Bit NOT NULL")]
        public bool ReserveGameForEmployee
        {
            get
            {
                return this._ReserveGameForEmployee;
            }
            set
            {
                if ((this._ReserveGameForEmployee != value))
                {
                    this._ReserveGameForEmployee = value;
                }
            }
        }

        [Column(Storage = "_MintsToExpire", DbType = "Int NOT NULL")]
        public int MintsToExpire
        {
            get
            {
                return this._MintsToExpire;
            }
            set
            {
                if ((this._MintsToExpire != value))
                {
                    this._MintsToExpire = value;
                }
            }
        }

        [Column(Storage = "_SITE", DbType = "VarChar(20) NOT NULL", CanBeNull = false)]
        public string SITE
        {
            get
            {
                return this._SITE;
            }
            set
            {
                if ((this._SITE != value))
                {
                    this._SITE = value;
                }
            }
        }
    }
}

