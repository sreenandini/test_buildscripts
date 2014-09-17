using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.EnterpriseBusiness.Entities
{
    public partial class GetCardLevelSettings
    {

        private int _CardLevel;

        private int _MaxNoofMachinestoCap;

        private string _MintstoCap;

       private int _SettingID;

        public GetCardLevelSettings()
        {
        }
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
        public int SettingID
        {
            get
            {
                return this._SettingID;
            }
            set
            {
                if ((this._SettingID != value))
                {
                    this._SettingID = value;
                }
            }
        }
        
    }
     public partial class GetGameCappingParametersEntity
    {

        private int _GameCapID;

        private bool _CapReleaseOnPlayerCardIn;

        private bool _ReserveGameForPlayer;

        private bool _ReserveGameForEmployee;

        private int _MintsToExpire;

        private string _SITE;

        public GetGameCappingParametersEntity()
        {
        }

        
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

