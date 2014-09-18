using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Data;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Linq.Expressions;
using System.ComponentModel;
using System;

namespace BMC.Transport
{
    public class PositionDetails
    {        
        public string Position { get; set; }
        public int InstallationNo { get; set; }
        public string BarPosNo { get; set; }
        public string AssetNo { get; set; }
        public string GMUNo { get; set; }
        public string SerialNo { get; set; }
        public string AltSerialNo { get; set; }
        public string MachineType { get; set; }
        public string Manufacturer { get; set; }
        public string GameCode { get; set; }
        public string GameCategory { get; set; }
        public string Game { get; set; }
        public int BarPosPort { get; set; }
        public int BarPosPoll { get; set; }
        public int BaseDenom { get; set; }
        public int CreditValue { get; set; }
        public float PercentagePayout { get; set; }
        public int Jackpot { get; set; }
        public string ActAssetNo { get; set; }
        public string ActSerialNo { get; set; }
        public int EnrolmentFlag { get; set; }
        public string CMPGameType { get; set; }
        public string OperatorName { get; set; }
        public int isMultiGame { get; set; }
		public bool GetGameDetails { get; set; }
        public bool IsDefaultAssetDetail { get; set; }
        public int OccupancyHour { get; set; }
        public string AssetDisplayName { get; set; }
        public string GameTypeCode { get; set; }

    }

    public class PositionCurrentStatusResult
    {

        private string _Bar_Pos_Name;

        private string _Machine_Name;

        private int _BAD_AAMS_Status;

        private int _BAD_Verification_Status;

        private int _Game_Verification;

        private int _Game_Enable_AAMS_Status;

        private int _Game_Install_AAMS_Status;

        private int _BAD_AAMS_EnableDisable;

        private int _BMC_Enterprise_Status;

        public PositionCurrentStatusResult()
        {
        }

        [Column(Storage = "_Bar_Pos_Name", DbType = "VarChar(50)")]
        public string Bar_Pos_Name
        {
            get
            {
                return this._Bar_Pos_Name;
            }
            set
            {
                if ((this._Bar_Pos_Name != value))
                {
                    this._Bar_Pos_Name = value;
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

        [Column(Storage = "_BAD_AAMS_Status", DbType = "Int NOT NULL")]
        public int BAD_AAMS_Status
        {
            get
            {
                return this._BAD_AAMS_Status;
            }
            set
            {
                if ((this._BAD_AAMS_Status != value))
                {
                    this._BAD_AAMS_Status = value;
                }
            }
        }

        [Column(Storage = "_BAD_Verification_Status", DbType = "Int NOT NULL")]
        public int BAD_Verification_Status
        {
            get
            {
                return this._BAD_Verification_Status;
            }
            set
            {
                if ((this._BAD_Verification_Status != value))
                {
                    this._BAD_Verification_Status = value;
                }
            }
        }

        [Column(Storage = "_Game_Verification", DbType = "Int NOT NULL")]
        public int Game_Verification
        {
            get
            {
                return this._Game_Verification;
            }
            set
            {
                if ((this._Game_Verification != value))
                {
                    this._Game_Verification = value;
                }
            }
        }

        [Column(Storage = "_Game_Enable_AAMS_Status", DbType = "Int NULL")]
        public int Game_Enable_AAMS_Status
        {
            get
            {
                return this._Game_Enable_AAMS_Status;
            }
            set
            {
                if ((this._Game_Enable_AAMS_Status != value))
                {
                    this._Game_Enable_AAMS_Status = value;
                }
            }
        }

        [Column(Storage = "_Game_Install_AAMS_Status", DbType = "Int NULL")]
        public int Game_Install_AAMS_Status
        {
            get
            {
                return this._Game_Install_AAMS_Status;
            }
            set
            {
                if ((this._Game_Install_AAMS_Status != value))
                {
                    this._Game_Install_AAMS_Status = value;
                }
            }
        }


        [Column(Storage = "_BAD_AAMS_EnableDisable", DbType = "Int NULL")]
        public int BAD_AAMS_EnableDisable
        {
            get
            {
                return this._BAD_AAMS_EnableDisable;
            }
            set
            {
                if ((this._BAD_AAMS_EnableDisable != value))
                {
                    this._BAD_AAMS_EnableDisable = value;
                }
            }
        }

        [Column(Storage = "_BMC_Enterprise_Status", DbType = "Int NULL")]
        public int BMC_Enterprise_Status
        {
            get
            {
                return this._BMC_Enterprise_Status;
            }
            set
            {
                if ((this._BMC_Enterprise_Status != value))
                {
                    this._BMC_Enterprise_Status = value;
                }
            }
        }

        
    }
}