
namespace BMC.CashDeskOperator
{
    using System.Data.Linq;
    using System.Data.Linq.Mapping;
    using System.Reflection;
    using System.ComponentModel;
    using System;
    using BMCIPC;
    using System.Linq;
    using System.Collections.Generic;
    using System.Globalization;


    [DatabaseAttribute(Name = "Exchange")]
    public class InstallationDataContext : DataContext
    {
        public InstallationDataContext(string connectionString) :
            base(connectionString)
        {
        }
        

        [Function(Name = "dbo.rsp_GetInstallationDetailsForCDO")]
        public ISingleResult<GetInstallationDetailsForCDOResult> GetInstallationDetailsForCDO()
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
            return ((ISingleResult<GetInstallationDetailsForCDOResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetGamesInfo")]
        public ISingleResult<GetGamesInfoResult> GetGamesInfo([Parameter(Name = "Pos", DbType = "Varchar")]  string Pos)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), Pos);
            return ((ISingleResult<GetGamesInfoResult>)(result.ReturnValue));
        }

        [Function(Name="dbo.rsp_GetBarPositionDetailsForCashDeskoperator")]
		public ISingleResult<GetBarPositionDetailsForCashDeskoperatorResult> GetBarPositionDetailsForCashDeskoperator([Parameter(Name="SortBy", DbType="VarChar(20)")] string sortBy, [Parameter(Name="InstallNo", DbType="Int")] System.Nullable<int> installNo)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), sortBy, installNo);
            return ((ISingleResult<GetBarPositionDetailsForCashDeskoperatorResult>)(result.ReturnValue));
		}

        [Function(Name = "dbo.usp_InsertReinstateMachine")]
        public int InsertReinstateMachine([Parameter(Name = "Installation_No", DbType = "Int")] System.Nullable<int> installation_No, [Parameter(Name = "User_No", DbType = "Int")] System.Nullable<int> user_No, [Parameter(Name = "Treasury_Amount", DbType = "Float")] System.Nullable<double> treasury_Amount, [Parameter(Name = "Treasury_Reason", DbType = "VarChar(200)")] string treasury_Reason, [Parameter(Name = "Treasury_Type", DbType = "VarChar(50)")] string treasury_Type, [Parameter(Name = "Treasury_Float_Issued_By", DbType = "Int")] System.Nullable<int> treasury_Float_Issued_By, [Parameter(Name = "Installation_Float_Status", DbType = "Int")] System.Nullable<int> installation_Float_Status, [Parameter(Name = "Float_Issued_By", DbType = "VarChar(50)")] string float_Issued_By, [Parameter(Name = "Hopper", DbType = "Bit")] System.Nullable<bool> hopper)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), installation_No, user_No, treasury_Amount, treasury_Reason, treasury_Type, treasury_Float_Issued_By, installation_Float_Status, float_Issued_By, hopper);
            return ((int)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_isPositionLocked")]
        public ISingleResult<SProcResult>isPositionLocked([Parameter(Name = "Position", DbType = "VarChar(100)")] string position)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), position);
            return ((ISingleResult<SProcResult>)(result.ReturnValue)); 
        }
        
        [Function(Name = "dbo.rsp_GetGameDetailsForPos")]
        public ISingleResult<GetGameDetailsForPosResult> GetGameDetailsForPos([Parameter(Name = "Pos", DbType = "VarChar(10)")] string pos)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), pos);
            return ((ISingleResult<GetGameDetailsForPosResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetPaytableDetailsForGame")]
        public ISingleResult<GetPaytableDetailsForGameResult> GetPaytableDetailsForGame([Parameter(Name = "GameTitleId", DbType = "Int")] System.Nullable<int> gameTitleId, [Parameter(Name = "Pos", DbType = "VarChar(10)")] string pos)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), gameTitleId, pos);
            return ((ISingleResult<GetPaytableDetailsForGameResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetCoinType")]
        public ISingleResult<CoinTypeResult> GetCoinType([Parameter(Name = "CultureInfo", DbType = "VarChar(6)")] string cultureInfo)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), cultureInfo);
            return ((ISingleResult<CoinTypeResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetSlotStatus")]
        public ISingleResult<SlotStatusResult> GetSlotStatusFromDB([Parameter(Name = "SortBy", DbType = "VarChar(20)")] string sortBy, [Parameter(Name = "InstallNo", DbType = "Int")] System.Nullable<int> installNo)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), sortBy, installNo);
            return ((ISingleResult<SlotStatusResult>)(result.ReturnValue));
        }

        public List<FloorStatusData> GetSlotStatus(string sortBy, int installNo)
        {
            List<SlotStatusResult> actualResults = GetSlotStatusFromDB(sortBy, installNo).ToList();
            List<FloorStatusData> actualRetResults = (from Records in actualResults
                                                      select new FloorStatusData
                                                      {
                                                          Asset_No = Records.Asset_No,
                                                          Bar_Pos_Name = Records.Bar_Pos_Name,
                                                          Bar_Pos_No = Records.Bar_Pos_No,
                                                          FinalCollectionStatus = Records.FinalCollectionStatus,
                                                          FLOORLEFT = Records.FLOORLEFT,
                                                          FLOORTOP = Records.FLOORTOP,
                                                          Install_No = Records.Install_No,
                                                          IsCollectable = Records.IsCollectable,
                                                          Slot_Status = Records.Slot_Status,
                                                          StackerEventReceived = Records.StackerEventReceived,
                                                          UnClearedEvent = Records.UnClearedEvent,
                                                          BarPosName = Records.BarPosName,
                                                          Game_Name = Records.Game_Name
                                                      }).ToList<FloorStatusData>();
            return actualRetResults;

        }

        [Function(Name = "dbo.rsp_GetDisableMachine")]
        public ISingleResult<DisableMachineResult> GetDisableMachine([Parameter(Name = "Installation_No", DbType = "Int")] System.Nullable<int> installation_No)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), installation_No);
            return ((ISingleResult<DisableMachineResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetActiveInstallations")]
        public ISingleResult<ActiveMachine> GetActiveInstallations()
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
            return ((ISingleResult<ActiveMachine>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetCollectionReports")]
        public ISingleResult<CollectionReports> GetCollectionReports()
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
            return ((ISingleResult<CollectionReports>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetCollectionDetailReports")]
        public ISingleResult<CollectionReports> GetCollectionDetailReports()
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
            return ((ISingleResult<CollectionReports>)(result.ReturnValue));
        }
        //Allow Machine Removal With out Declaration
        [Function(Name = "dbo.usp_updateInstallationFloatStatus")]
        public int updateInstallationFloatStatus([Parameter(Name = "Installation_No", DbType = "Int")] System.Nullable<int> installation_No)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), installation_No);
            return ((int)(result.ReturnValue));
        }

        public class SProcResult
        {

            private string _RESULT;

            [Column(Storage = "_RESULT", DbType = "VarChar(8) NOT NULL", CanBeNull = false)]
            public string Result
            {
                get
                {
                    return _RESULT;
                }
                set
                {
                    if ((_RESULT != value))
                    {
                        _RESULT = value;
                    }
                }
            }
        }
       
    }

    public partial class GetBarPositionDetailsForCashDeskoperatorResult
	{
		
		private string _EPIDetails;
		
		private int _Bar_Pos_No;
		
		private string _Bar_Pos_Name;
		
		private System.Nullable<int> _BarPosName;
		
		private System.Nullable<int> _Installation_No;
		
		private int _Installation_Price_Of_Play;
		
		private float _Anticipated_Percentage_Payout;
		
		private int _Installation_MaxBet;
		
		private int _Installation_Token_Value;
		
		private int _FFInstallation_No;
		
		private int _HasPlayHappened;
		
		private int _IsHandPayUnProcessed;
		
		private int _IsCardedPlay;
		
		private int _ISVLTInstallationComplete;

        private int _ISGamesIntallationComplete;
		
		private int _ISMachineMaintenance;
		
		private int _IsCollectable;
		
		private string _Asset_No;
		
		private string _Game_Name;
		
		private System.Nullable<bool> _UnClearedEvent;
		
		private string _FLOORTOP;
		
		private string _FLOORLEFT;
		
		private System.Nullable<System.DateTime> _InstallationStartDate;
		
		private int _Installation_Float_Status;
		
		private int _Install_No;
		
		private string _SerialNumber;
		
		private string _Manufacturer;
		
		private string _Zone_Name;
		
		private string _ActAssetNo;
		
		private string _GMUNo;
		
		private string _ActSerialNo;
		
        private string _EnrolmentFlag;
		
		private string _Route;
		
		private System.Nullable<System.DateTime> _Start_Date;
		
		public GetBarPositionDetailsForCashDeskoperatorResult()
		{
		}
		
		[Column(Storage="_EPIDetails", DbType="VarChar(50)")]
		public string EPIDetails
		{
			get
			{
				return this._EPIDetails;
			}
			set
			{
				if ((this._EPIDetails != value))
				{
					this._EPIDetails = value;
				}
			}
		}
		
		[Column(Storage="_Bar_Pos_No", DbType="Int NOT NULL")]
		public int Bar_Pos_No
		{
			get
			{
				return this._Bar_Pos_No;
			}
			set
			{
				if ((this._Bar_Pos_No != value))
				{
					this._Bar_Pos_No = value;
				}
			}
		}
		
		[Column(Storage="_Bar_Pos_Name", DbType="VarChar(50)")]
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
		
		[Column(Storage="_BarPosName", DbType="Int")]
		public System.Nullable<int> BarPosName
		{
			get
			{
				return this._BarPosName;
			}
			set
			{
				if ((this._BarPosName != value))
				{
					this._BarPosName = value;
				}
			}
		}
		
		[Column(Storage="_Installation_No", DbType="Int")]
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
		
		[Column(Storage="_Installation_Price_Of_Play", DbType="Int NOT NULL")]
		public int Installation_Price_Of_Play
		{
			get
			{
				return this._Installation_Price_Of_Play;
			}
			set
			{
				if ((this._Installation_Price_Of_Play != value))
				{
					this._Installation_Price_Of_Play = value;
				}
			}
		}
		
		[Column(Storage="_Anticipated_Percentage_Payout", DbType="Real NOT NULL")]
		public float Anticipated_Percentage_Payout
		{
			get
			{
				return this._Anticipated_Percentage_Payout;
			}
			set
			{
				if ((this._Anticipated_Percentage_Payout != value))
				{
					this._Anticipated_Percentage_Payout = value;
				}
			}
		}
		
		[Column(Storage="_Installation_MaxBet", DbType="Int NOT NULL")]
		public int Installation_MaxBet
		{
			get
			{
				return this._Installation_MaxBet;
			}
			set
			{
				if ((this._Installation_MaxBet != value))
				{
					this._Installation_MaxBet = value;
				}
			}
		}
		
		[Column(Storage="_Installation_Token_Value", DbType="Int NOT NULL")]
		public int Installation_Token_Value
		{
			get
			{
				return this._Installation_Token_Value;
			}
			set
			{
				if ((this._Installation_Token_Value != value))
				{
					this._Installation_Token_Value = value;
				}
			}
		}
		
		[Column(Storage="_FFInstallation_No", DbType="Int NOT NULL")]
		public int FFInstallation_No
		{
			get
			{
				return this._FFInstallation_No;
			}
			set
			{
				if ((this._FFInstallation_No != value))
				{
					this._FFInstallation_No = value;
				}
			}
		}
		
		[Column(Storage="_HasPlayHappened", DbType="Int NOT NULL")]
		public int HasPlayHappened
		{
			get
			{
				return this._HasPlayHappened;
			}
			set
			{
				if ((this._HasPlayHappened != value))
				{
					this._HasPlayHappened = value;
				}
			}
		}
		
		[Column(Storage="_IsHandPayUnProcessed", DbType="Int NOT NULL")]
		public int IsHandPayUnProcessed
		{
			get
			{
				return this._IsHandPayUnProcessed;
			}
			set
			{
				if ((this._IsHandPayUnProcessed != value))
				{
					this._IsHandPayUnProcessed = value;
				}
			}
		}
		
		[Column(Storage="_IsCardedPlay", DbType="Int NOT NULL")]
		public int IsCardedPlay
		{
			get
			{
				return this._IsCardedPlay;
			}
			set
			{
				if ((this._IsCardedPlay != value))
				{
					this._IsCardedPlay = value;
				}
			}
		}
		
		[Column(Storage="_ISVLTInstallationComplete", DbType="Int NOT NULL")]
		public int ISVLTInstallationComplete
		{
			get
			{
				return this._ISVLTInstallationComplete;
			}
			set
			{
				if ((this._ISVLTInstallationComplete != value))
				{
					this._ISVLTInstallationComplete = value;
				}
			}
		}
		
		
        [Column(Storage = "_ISGamesIntallationComplete", DbType = "Int NOT NULL")]
        public int ISGamesIntallationComplete
        {
            get
            {
                return this._ISGamesIntallationComplete;
            }
            set
            {
                if ((this._ISGamesIntallationComplete != value))
                {
                    this._ISGamesIntallationComplete = value;
                }
            }
        }
		
		[Column(Storage="_ISMachineMaintenance", DbType="Int NOT NULL")]
		public int ISMachineMaintenance
		{
			get
			{
				return this._ISMachineMaintenance;
			}
			set
			{
				if ((this._ISMachineMaintenance != value))
				{
					this._ISMachineMaintenance = value;
				}
			}
		}
		
		[Column(Storage="_IsCollectable", DbType="Int NOT NULL")]
		public int IsCollectable
		{
			get
			{
				return this._IsCollectable;
			}
			set
			{
				if ((this._IsCollectable != value))
				{
					this._IsCollectable = value;
				}
			}
		}
		
		[Column(Storage="_Asset_No", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
		public string Asset_No
		{
			get
			{
				return this._Asset_No;
			}
			set
			{
				if ((this._Asset_No != value))
				{
					this._Asset_No = value;
				}
			}
		}
		
		[Column(Storage="_Game_Name", DbType="VarChar(50)")]
		public string Game_Name
		{
			get
			{
				return this._Game_Name;
			}
			set
			{
				if ((this._Game_Name != value))
				{
					this._Game_Name = value;
				}
			}
		}
		
		[Column(Storage="_UnClearedEvent", DbType="Bit")]
		public System.Nullable<bool> UnClearedEvent
		{
			get
			{
				return this._UnClearedEvent;
			}
			set
			{
				if ((this._UnClearedEvent != value))
				{
					this._UnClearedEvent = value;
				}
			}
		}
		
		[Column(Storage="_FLOORTOP", DbType="VarChar(20) NOT NULL", CanBeNull=false)]
		public string FLOORTOP
		{
			get
			{
				return this._FLOORTOP;
			}
			set
			{
				if ((this._FLOORTOP != value))
				{
					this._FLOORTOP = value;
				}
			}
		}
		
		[Column(Storage="_FLOORLEFT", DbType="VarChar(20) NOT NULL", CanBeNull=false)]
		public string FLOORLEFT
		{
			get
			{
				return this._FLOORLEFT;
			}
			set
			{
				if ((this._FLOORLEFT != value))
				{
					this._FLOORLEFT = value;
				}
			}
		}
		
		[Column(Storage="_InstallationStartDate", DbType="DateTime")]
		public System.Nullable<System.DateTime> InstallationStartDate
		{
			get
			{
				return this._InstallationStartDate;
			}
			set
			{
				if ((this._InstallationStartDate != value))
				{
					this._InstallationStartDate = value;
				}
			}
		}
		
		[Column(Storage="_Installation_Float_Status", DbType="Int NOT NULL")]
		public int Installation_Float_Status
		{
			get
			{
				return this._Installation_Float_Status;
			}
			set
			{
				if ((this._Installation_Float_Status != value))
				{
					this._Installation_Float_Status = value;
				}
			}
		}
		
		[Column(Storage="_Install_No", DbType="Int NOT NULL")]
		public int Install_No
		{
			get
			{
				return this._Install_No;
			}
			set
			{
				if ((this._Install_No != value))
				{
					this._Install_No = value;
				}
			}
		}
		
		[Column(Storage="_SerialNumber", DbType="VarChar(50)")]
		public string SerialNumber
		{
			get
			{
				return this._SerialNumber;
			}
			set
			{
				if ((this._SerialNumber != value))
				{
					this._SerialNumber = value;
				}
			}
		}
		
		[Column(Storage="_Manufacturer", DbType="VarChar(50)")]
		public string Manufacturer
		{
			get
			{
				return this._Manufacturer;
			}
			set
			{
				if ((this._Manufacturer != value))
				{
					this._Manufacturer = value;
				}
			}
		}
		
		[Column(Storage="_Zone_Name", DbType="VarChar(50)")]
		public string Zone_Name
		{
			get
			{
				return this._Zone_Name;
			}
			set
			{
				if ((this._Zone_Name != value))
				{
					this._Zone_Name = value;
				}
			}
		}
		[Column(Storage="_ActAssetNo", DbType="VarChar(50)")]
		public string ActAssetNo
		{
			get
			{
				return this._ActAssetNo;
			}
			set
			{
				if ((this._ActAssetNo != value))
				{
					this._ActAssetNo = value;
				}
			}
		}
		[Column(Storage="_GMUNo", DbType="VarChar(50)")]
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
		[Column(Storage="_ActSerialNo", DbType="VarChar(50)")]
		public string ActSerialNo
		{
			get
			{
				return this._ActSerialNo;
			}
			set
			{
				if ((this._ActSerialNo != value))
				{
					this._ActSerialNo = value;
				}
			}
		}
		
		[Column(Storage="_EnrolmentFlag", DbType="Int")]
		public string  EnrolmentFlag
		{
			get
			{
				return this._EnrolmentFlag;
			}
			set
			{
				if ((this._EnrolmentFlag != value))
				{
					this._EnrolmentFlag = value;
				}
			}
		}
		
		[Column(Storage="_Route", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
		public string Route
		{
			get
			{
				return this._Route;
			}
			set
			{
				if ((this._Route != value))
				{
					this._Route = value;
				}
			}
		}
		
		[Column(Storage="_Start_Date", DbType="DateTime")]
		public System.Nullable<System.DateTime> Start_Date
		{
			get
			{
				return this._Start_Date;
			}
			set
			{
				if ((this._Start_Date != value))
				{
					this._Start_Date = value;
				}
			}
		}
	}
    public class GetInstallationDetailsForCDOResult
    {

        private string _Bar_Pos_Name;

        private string _Stock_No;

        private string _Machine_Manufacturers_Serial_No;

        private string _BAD_AAMS_Code;

        private string _Manufacturer_Name;

        private string _StackerName;

        private System.Nullable<System.DateTime> _Start_Date;

        private System.Nullable<int> _Machine_Machine_Version;

        private System.Nullable<float> _HoldPercentage;

        private System.Nullable<float> _PercentagePayout;

        private System.Nullable<float> _BaseDenom;

        private System.Nullable<float> _CoinValue;

        private string _GMU;

        private string _Zone_Name;

        private System.Nullable<System.DateTime> _LastReadMeterFromGMU;
        
        private string _LastReadMeterFromGMUString;

        private string _GMU_No;

        private string _GMU_Version;

        public GetInstallationDetailsForCDOResult()
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

        [Column(Storage = "_Stock_No", DbType = "VarChar(50)")]
        public string Stock_No
        {
            get
            {
                return this._Stock_No;
            }
            set
            {
                if ((this._Stock_No != value))
                {
                    this._Stock_No = value;
                }
            }
        }

        [Column(Storage = "_Machine_Manufacturers_Serial_No", DbType = "VarChar(50)")]
        public string Machine_Manufacturers_Serial_No
        {
            get
            {
                return this._Machine_Manufacturers_Serial_No;
            }
            set
            {
                if ((this._Machine_Manufacturers_Serial_No != value))
                {
                    this._Machine_Manufacturers_Serial_No = value;
                }
            }
        }

        [Column(Storage = "_BAD_AAMS_Code", DbType = "VarChar(12)")]
        public string BAD_AAMS_Code
        {
            get
            {
                return this._BAD_AAMS_Code;
            }
            set
            {
                if ((this._BAD_AAMS_Code != value))
                {
                    this._BAD_AAMS_Code = value;
                }
            }
        }

        [Column(Storage = "_Manufacturer_Name", DbType = "VarChar(50)")]
        public string Manufacturer_Name
        {
            get
            {
                return this._Manufacturer_Name;
            }
            set
            {
                if ((this._Manufacturer_Name != value))
                {
                    this._Manufacturer_Name = value;
                }
            }
        }

        [Column(Storage = "_Start_Date", DbType = "DateTime")]
        public System.Nullable<System.DateTime> Start_Date
        {
            get
            {
                return this._Start_Date;
            }
            set
            {
                if ((this._Start_Date != value))
                {
                    this._Start_Date = value;
                }
            }
        }

        [Column(Storage = "_Machine_Machine_Version", DbType = "Int")]
        public System.Nullable<int> Machine_Machine_Version
        {
            get
            {
                return this._Machine_Machine_Version;
            }
            set
            {
                if ((this._Machine_Machine_Version != value))
                {
                    this._Machine_Machine_Version = value;
                }
            }
        }

        [Column(Storage = "_HoldPercentage", DbType = "decimal(8,2)")]
        public System.Nullable<float> HoldPercentage
        {
            get
            {
                return this._HoldPercentage;
            }
            set
            {
                if ((this._HoldPercentage != value))
                {
                    this._HoldPercentage = value;
                }
            }
        }

        [Column(Storage = "_PercentagePayout", DbType = "Real")]
        public System.Nullable<float> PercentagePayout
        {
            get
            {
                return this._PercentagePayout;
            }
            set
            {
                if ((this._PercentagePayout != value))
                {
                    this._PercentagePayout = value;
                }
            }
        }

        [Column(Storage = "_BaseDenom", DbType = "decimal(8,2)")]
        public System.Nullable<float> BaseDenom
        {
            get
            {
                return this._BaseDenom;
            }
            set
            {
                if ((this._BaseDenom != value))
                {
                    this._BaseDenom = value;
                }
            }
        }

        [Column(Storage = "_CoinValue", DbType = "decimal(8,2)")]
        public System.Nullable<float> CoinValue
        {
            get
            {
                return this._CoinValue;
            }
            set
            {
                if ((this._CoinValue != value))
                {
                    this._CoinValue = value;
                }
            }
        }

        [Column(Storage = "_GMU", DbType = "VarChar(20)")]
        public string GMU
        {
            get
            {
                return this._GMU;
            }
            set
            {
                if ((this._GMU != value))
                {
                    this._GMU = value;
                }
            }
        }

        [Column(Storage = "_Zone_Name", DbType = "VarChar(50)")]
        public string Zone_Name
        {
            get { return this._Zone_Name; }
            set
            {
                if (this._Zone_Name != value)
                {
                    this._Zone_Name = value;
                }
            }
        }

        [Column(Storage = "_LastReadMeterFromGMU", DbType = "DateTime")]
        public System.Nullable<System.DateTime> LastReadMeterFromGMU
        {
            get
            {
                return this._LastReadMeterFromGMU;
            }
            set
            {
                if ((this._LastReadMeterFromGMU != value))
                {
                    this._LastReadMeterFromGMU = value;
                }
            }
        }

        [Column(Storage = "_GMU_No", DbType = "VarChar(50)")]
        public string GMU_No
        {
            get
            {
                return this._GMU_No;
            }
            set
            {
                if ((this._GMU_No != value))
                {
                    this._GMU_No = value;
                }
            }
        }

        public string LastReadMeterFromGMUString
        {
            get
            {
                return this._LastReadMeterFromGMU.HasValue ? _LastReadMeterFromGMU.Value.ToString(CultureInfo.CurrentCulture) : "";
            }
            set
            {

                if ((this._LastReadMeterFromGMUString != value))
                {
                    this._LastReadMeterFromGMUString = value;

                }
            }
        }

        [Column(Storage = "_GMU_Version", DbType = "VarChar(20)")]
        public string GMU_Version
        {
            get
            {
                return this._GMU_Version;
            }
            set
            {
                if ((this._GMU_Version != value))
                {
                    this._GMU_Version = value;
                }
            }
        }

        [Column(Storage = "_StackerName", DbType = "VarChar(255)")]
        public string StackerName
        {
            get
            {
                return this._StackerName;
            }
            set
            {
                if ((this._StackerName != value))
                {
                    this._StackerName = value;
                }
            }
        }
    }

    public class GetGamesInfoResult
    {

        private string _Game_Title;

        private string _Game_Name;

        private string _Game_Part_Number;

        private string _Game_version;

        public GetGamesInfoResult()
        {
        }

        [Column(Storage = "_Game_Title", DbType = "VarChar(100)")]
        public string Game_Title
        {
            get
            {
                return this._Game_Title;
            }
            set
            {
                if ((this._Game_Title!= value))
                {
                    this._Game_Title= value;
                }
            }
        }

        [Column(Storage = "_Game_Name", DbType = "VarChar(100)")]
        public string Game_Name
        {
            get
            {
                return this._Game_Name;
            }
            set
            {
                if ((this._Game_Name != value))
                {
                    this._Game_Name = value;
                }
            }
        }

        [Column(Storage = "_Game_Part_Number", DbType = "VarChar(20)")]
        public string Game_Part_Number
        {
            get
            {
                return this._Game_Part_Number;
            }
            set
            {
                if ((this._Game_Part_Number != value))
                {
                    this._Game_Part_Number = value;
                }
            }
        }

        [Column(Storage = "_Game_version", DbType = "VarChar(10) NOT NULL", CanBeNull = false)]
        public string Game_version
        {
            get
            {
                return this._Game_version;
            }
            set
            {
                if ((this._Game_version != value))
                {
                    this._Game_version = value;
                }
            }
        }
    }


    public partial class GetGameDetailsForPosResult
    {

        	private System.Nullable<int> _Game_Title_Id;
		
		private string _Game_Title;
		
		private string _Game_Category_Name;
		
		private string _Manufacturer_Name;

        public GetGameDetailsForPosResult()
		{
		}
		
		[Column(Storage="_Game_Title_Id", DbType="Int")]
		public System.Nullable<int> Game_Title_Id
		{
			get
			{
				return this._Game_Title_Id;
			}
			set
			{
				if ((this._Game_Title_Id != value))
				{
					this._Game_Title_Id = value;
				}
			}
		}
		
		[Column(Storage="_Game_Title", DbType="VarChar(100)")]
		public string Game_Title
		{
			get
			{
				return this._Game_Title;
			}
			set
			{
				if ((this._Game_Title != value))
				{
					this._Game_Title = value;
				}
			}
		}
		
		[Column(Storage="_Game_Category_Name", DbType="VarChar(50)")]
		public string Game_Category_Name
		{
			get
			{
				return this._Game_Category_Name;
			}
			set
			{
				if ((this._Game_Category_Name != value))
				{
					this._Game_Category_Name = value;
				}
			}
		}
		
		[Column(Storage="_Manufacturer_Name", DbType="VarChar(50)")]
		public string Manufacturer_Name
		{
			get
			{
				return this._Manufacturer_Name;
			}
			set
			{
				if ((this._Manufacturer_Name != value))
				{
					this._Manufacturer_Name = value;
				}
			}
		}
    }

    public partial class GetPaytableDetailsForGameResult
    {

      	
		private string _PaytableDescription;
		
		private double _MaxBet;
		
		private double _PayoutPercent;

        public GetPaytableDetailsForGameResult()
		{
		}
		
		[Column(Storage="_PaytableDescription", DbType="VarChar(100) NOT NULL", CanBeNull=false)]
		public string PaytableDescription
		{
			get
			{
				return this._PaytableDescription;
			}
			set
			{
				if ((this._PaytableDescription != value))
				{
					this._PaytableDescription = value;
				}
			}
		}
		
		[Column(Storage="_MaxBet", DbType="Float NOT NULL")]
		public double MaxBet
		{
			get
			{
				return this._MaxBet;
			}
			set
			{
				if ((this._MaxBet != value))
				{
					this._MaxBet = value;
				}
			}
		}
		
		[Column(Storage="_PayoutPercent", DbType="Float NOT NULL")]
		public double PayoutPercent
		{
			get
			{
				return this._PayoutPercent;
			}
			set
			{
				if ((this._PayoutPercent != value))
				{
					this._PayoutPercent = value;
				}
			}
		}
    }

    public partial class CoinTypeResult
    {

        private int _CoinType;

        public CoinTypeResult()
        {
        }

        [Column(Storage = "_CoinType", DbType = "Int NOT NULL")]
        public int CoinType
        {
            get
            {
                return this._CoinType;
            }
            set
            {
                if ((this._CoinType != value))
                {
                    this._CoinType = value;
                }
            }
        }
    }

    public partial class SlotStatusResult
    {

        private int _Bar_Pos_No;

        private string _Bar_Pos_Name;

        private System.Nullable<int> _BarPosName;

        private string _Slot_Status;

        private int _IsCollectable;

        private string _Asset_No;

        private System.Nullable<bool> _UnClearedEvent;

        private System.Nullable<int> _FinalCollection_Status;

        private System.Nullable<bool> _StackerEventReceived;

        private string _FLOORTOP;

        private string _FLOORLEFT;

        private int _Install_No;

        private string _Game_Name = string.Empty;

        public SlotStatusResult()
        {
        }

        [Column(Storage = "_Bar_Pos_No", DbType = "Int NOT NULL")]
        public int Bar_Pos_No
        {
            get
            {
                return this._Bar_Pos_No;
            }
            set
            {
                if ((this._Bar_Pos_No != value))
                {
                    this._Bar_Pos_No = value;
                }
            }
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

        [Column(Storage = "_BarPosName", DbType = "Int")]
        public System.Nullable<int> BarPosName
        {
            get
            {
                return this._BarPosName;
            }
            set
            {
                if ((this._BarPosName != value))
                {
                    this._BarPosName = value;
                }
            }
        }

        [Column(Storage = "_Slot_Status", DbType = "VarChar(50)")]
        public string Slot_Status
        {
            get
            {
                return this._Slot_Status;
            }
            set
            {
                if ((this._Slot_Status != value))
                {
                    this._Slot_Status = value;
                }
            }
        }

        [Column(Storage = "_IsCollectable", DbType = "Int NOT NULL")]
        public int IsCollectable
        {
            get
            {
                return this._IsCollectable;
            }
            set
            {
                if ((this._IsCollectable != value))
                {
                    this._IsCollectable = value;
                }
            }
        }

        [Column(Storage = "_Asset_No", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
        public string Asset_No
        {
            get
            {
                return this._Asset_No;
            }
            set
            {
                if ((this._Asset_No != value))
                {
                    this._Asset_No = value;
                }
            }
        }

        [Column(Storage = "_UnClearedEvent", DbType = "Bit")]
        public System.Nullable<bool> UnClearedEvent
        {
            get
            {
                return this._UnClearedEvent;
            }
            set
            {
                if ((this._UnClearedEvent != value))
                {
                    this._UnClearedEvent = value;
                }
            }
        }

        [Column(Storage = "_StackerEventReceived", DbType = "bit")]
        public System.Nullable<bool> StackerEventReceived
        {
            get
            {
                return this._StackerEventReceived;
            }
            set
            {
                if ((this._StackerEventReceived != value))
                {
                    this._StackerEventReceived = value;
                }
            }

        }

        [Column(Storage = "_FinalCollection_Status", DbType = "TinyInt")]
        public System.Nullable<int> FinalCollectionStatus
        {
            get
            {
                return this._FinalCollection_Status;
            }
            set
            {
                if ((this._FinalCollection_Status != value))
                {
                    this._FinalCollection_Status = value;
                }
            }
        }

        [Column(Storage = "_FLOORTOP", DbType = "VarChar(20) NOT NULL", CanBeNull = false)]
        public string FLOORTOP
        {
            get
            {
                return this._FLOORTOP;
            }
            set
            {
                if ((this._FLOORTOP != value))
                {
                    this._FLOORTOP = value;
                }
            }
        }

        [Column(Storage = "_FLOORLEFT", DbType = "VarChar(20) NOT NULL", CanBeNull = false)]
        public string FLOORLEFT
        {
            get
            {
                return this._FLOORLEFT;
            }
            set
            {
                if ((this._FLOORLEFT != value))
                {
                    this._FLOORLEFT = value;
                }
            }
        }

        [Column(Storage = "_Install_No", DbType = "Int NOT NULL")]
        public int Install_No
        {
            get
            {
                return this._Install_No;
            }
            set
            {
                if ((this._Install_No != value))
                {
                    this._Install_No = value;
                }
            }
        }

        [Column(Storage = "_Game_Name", DbType = "VarChar(100) NULL", CanBeNull = true)]
        public string Game_Name
        {
            get
            {
                return this._Game_Name;
            }
            set
            {
                if ((this._Game_Name != value))
                {
                    this._Game_Name = value;
                }
            }
        }     
    }

    public partial class DisableMachineResult
    {

        private int _DisMachine;

        public DisableMachineResult()
		{
		}
		
		[Column(Storage="_DisMachine", DbType="Int NOT NULL")]
		public int DisMachine
		{
			get
			{
				return this._DisMachine;
			}
			set
			{
				if ((this._DisMachine != value))
				{
					this._DisMachine = value;
				}
			}
		}
    }

    public partial class ActiveMachine : INotifyPropertyChanged
    {

        private string _Name;

        private int _Installation_No;

        private string _Status;

        private int _IsChecked;

        public ActiveMachine()
        {
        }

        [Column(Storage = "_Name", DbType = "VarChar(500)")]
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
                    this._Name = value ?? "";
                }
            }
        }

        [Column(Storage = "_Installation_No", DbType = "Int NOT NULL")]
        public int Installation_No
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

        public string Status
        {
            get { return _Status; }
            set
            {
                _Status = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Status"));
                }
            }
        }
        public int IsChecked
        {
            get { return _IsChecked; }
            set
            {
                _IsChecked = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("IsChecked"));
                }
            }
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }


    public partial class CollectionReports : INotifyPropertyChanged
    {

        private string _Description;

        private string _ID;

        public CollectionReports()
        {
        }

        [Column(Storage = "_Description", DbType = "VarChar(500)")]
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
                    this._Description = value ?? "";
                }
            }
        }


        [Column(Storage = "_ID", DbType = "Int")]
        public string ID
        {
            get
            {
                return this._ID;
            }
            set
            {
                if ((this._ID != value))
                {
                    this._ID = value ?? "";
                }
            }
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}
