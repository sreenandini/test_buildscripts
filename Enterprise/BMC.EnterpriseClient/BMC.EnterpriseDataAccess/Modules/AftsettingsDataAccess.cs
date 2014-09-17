using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;
using System.Reflection;
using System.Data.Linq;

namespace BMC.EnterpriseDataAccess
{
    public partial class EnterpriseDataContext
    {       
    
        [Function(Name = "dbo.rsp_GetAFTSettingsDenom")]
        public ISingleResult<rsp_GetAFTSettingsDenomResult>GetAFTSettingsDenom([Parameter(Name = "SiteID", DbType = "Int")] System.Nullable<int> siteID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), siteID);
            return ((ISingleResult<rsp_GetAFTSettingsDenomResult>)(result.ReturnValue));
        }         


        public partial class rsp_GetAFTSettingsDenomResult
        {

            private System.Nullable<int> _Denom;

            public rsp_GetAFTSettingsDenomResult()
            {
            }

            [Column(Storage = "_Denom", DbType = "Int")]
            public System.Nullable<int> Denom
            {
                get
                {
                    return this._Denom;
                }
                set
                {
                    if ((this._Denom != value))
                    {
                        this._Denom = value;
                    }
                }
            }
        }
        /////////////////

        [Function(Name = "dbo.rsp_GetAFTSetting")]
        public ISingleResult<rsp_GetAFTSettingResult>GetAFTSetting([Parameter(Name = "SiteID", DbType = "Int")] System.Nullable<int> siteID, [Parameter(Name = "Denom", DbType = "Int")] System.Nullable<int> denom)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), siteID, denom);
            return ((ISingleResult<rsp_GetAFTSettingResult>)(result.ReturnValue));
        }
        public partial class rsp_GetAFTSettingResult
        {

            private int _AFTSetting_NO;

            private System.Nullable<int> _Denom;

            private string _SiteCode;

            private System.Nullable<bool> _AFTTransactionsAllowed;

            private System.Nullable<bool> _AllowCashableDeposits;

            private System.Nullable<bool> _AllowCashWithdrawal;

            private System.Nullable<bool> _AllowNonCashableDeposits;

            private System.Nullable<bool> _AllowOffers;

            private System.Nullable<bool> _AllowPartialTransfers;

            private System.Nullable<bool> _AllowPointsWithdrawal;

            private System.Nullable<bool> _AllowRedeemOffers;

            private System.Nullable<bool> _AutoDepositCashableCreditsonCardOut;

            private System.Nullable<bool> _AutoDepositNonCashableCreditsonCardOut;

            private System.Nullable<int> _EFTTimeoutValue;

            private System.Nullable<int> _MaxDepositAmount;

            private System.Nullable<int> _MaxWithDrawAmount;

            private System.Nullable<int> _Option1WithdrawalAmount;

            private System.Nullable<int> _Option2WithdrawalAmount;

            private System.Nullable<int> _Option3WithdrawalAmount;

            private System.Nullable<int> _Option4WithdrawalAmount;

            private System.Nullable<int> _Option5WithdrawalAmount;

            public rsp_GetAFTSettingResult()
            {
            }

            [Column(Storage = "_AFTSetting_NO", DbType = "Int NOT NULL")]
            public int AFTSetting_NO
            {
                get
                {
                    return this._AFTSetting_NO;
                }
                set
                {
                    if ((this._AFTSetting_NO != value))
                    {
                        this._AFTSetting_NO = value;
                    }
                }
            }

            [Column(Storage = "_Denom", DbType = "Int")]
            public System.Nullable<int> Denom
            {
                get
                {
                    return this._Denom;
                }
                set
                {
                    if ((this._Denom != value))
                    {
                        this._Denom = value;
                    }
                }
            }

            [Column(Storage = "_SiteCode", DbType = "VarChar(20)")]
            public string SiteCode
            {
                get
                {
                    return this._SiteCode;
                }
                set
                {
                    if ((this._SiteCode != value))
                    {
                        this._SiteCode = value;
                    }
                }
            }

            [Column(Storage = "_AFTTransactionsAllowed", DbType = "Bit")]
            public System.Nullable<bool> AFTTransactionsAllowed
            {
                get
                {
                    return this._AFTTransactionsAllowed;
                }
                set
                {
                    if ((this._AFTTransactionsAllowed != value))
                    {
                        this._AFTTransactionsAllowed = value;
                    }
                }
            }

            [Column(Storage = "_AllowCashableDeposits", DbType = "Bit")]
            public System.Nullable<bool> AllowCashableDeposits
            {
                get
                {
                    return this._AllowCashableDeposits;
                }
                set
                {
                    if ((this._AllowCashableDeposits != value))
                    {
                        this._AllowCashableDeposits = value;
                    }
                }
            }

            [Column(Storage = "_AllowCashWithdrawal", DbType = "Bit")]
            public System.Nullable<bool> AllowCashWithdrawal
            {
                get
                {
                    return this._AllowCashWithdrawal;
                }
                set
                {
                    if ((this._AllowCashWithdrawal != value))
                    {
                        this._AllowCashWithdrawal = value;
                    }
                }
            }

            [Column(Storage = "_AllowNonCashableDeposits", DbType = "Bit")]
            public System.Nullable<bool> AllowNonCashableDeposits
            {
                get
                {
                    return this._AllowNonCashableDeposits;
                }
                set
                {
                    if ((this._AllowNonCashableDeposits != value))
                    {
                        this._AllowNonCashableDeposits = value;
                    }
                }
            }

            [Column(Storage = "_AllowOffers", DbType = "Bit")]
            public System.Nullable<bool> AllowOffers
            {
                get
                {
                    return this._AllowOffers;
                }
                set
                {
                    if ((this._AllowOffers != value))
                    {
                        this._AllowOffers = value;
                    }
                }
            }

            [Column(Storage = "_AllowPartialTransfers", DbType = "Bit")]
            public System.Nullable<bool> AllowPartialTransfers
            {
                get
                {
                    return this._AllowPartialTransfers;
                }
                set
                {
                    if ((this._AllowPartialTransfers != value))
                    {
                        this._AllowPartialTransfers = value;
                    }
                }
            }

            [Column(Storage = "_AllowPointsWithdrawal", DbType = "Bit")]
            public System.Nullable<bool> AllowPointsWithdrawal
            {
                get
                {
                    return this._AllowPointsWithdrawal;
                }
                set
                {
                    if ((this._AllowPointsWithdrawal != value))
                    {
                        this._AllowPointsWithdrawal = value;
                    }
                }
            }

            [Column(Storage = "_AllowRedeemOffers", DbType = "Bit")]
            public System.Nullable<bool> AllowRedeemOffers
            {
                get
                {
                    return this._AllowRedeemOffers;
                }
                set
                {
                    if ((this._AllowRedeemOffers != value))
                    {
                        this._AllowRedeemOffers = value;
                    }
                }
            }

            [Column(Storage = "_AutoDepositCashableCreditsonCardOut", DbType = "Bit")]
            public System.Nullable<bool> AutoDepositCashableCreditsonCardOut
            {
                get
                {
                    return this._AutoDepositCashableCreditsonCardOut;
                }
                set
                {
                    if ((this._AutoDepositCashableCreditsonCardOut != value))
                    {
                        this._AutoDepositCashableCreditsonCardOut = value;
                    }
                }
            }

            [Column(Storage = "_AutoDepositNonCashableCreditsonCardOut", DbType = "Bit")]
            public System.Nullable<bool> AutoDepositNonCashableCreditsonCardOut
            {
                get
                {
                    return this._AutoDepositNonCashableCreditsonCardOut;
                }
                set
                {
                    if ((this._AutoDepositNonCashableCreditsonCardOut != value))
                    {
                        this._AutoDepositNonCashableCreditsonCardOut = value;
                    }
                }
            }

            [Column(Storage = "_EFTTimeoutValue", DbType = "Int")]
            public System.Nullable<int> EFTTimeoutValue
            {
                get
                {
                    return this._EFTTimeoutValue;
                }
                set
                {
                    if ((this._EFTTimeoutValue != value))
                    {
                        this._EFTTimeoutValue = value;
                    }
                }
            }

            [Column(Storage = "_MaxDepositAmount", DbType = "Int")]
            public System.Nullable<int> MaxDepositAmount
            {
                get
                {
                    return this._MaxDepositAmount;
                }
                set
                {
                    if ((this._MaxDepositAmount != value))
                    {
                        this._MaxDepositAmount = value;
                    }
                }
            }

            [Column(Storage = "_MaxWithDrawAmount", DbType = "Int")]
            public System.Nullable<int> MaxWithDrawAmount
            {
                get
                {
                    return this._MaxWithDrawAmount;
                }
                set
                {
                    if ((this._MaxWithDrawAmount != value))
                    {
                        this._MaxWithDrawAmount = value;
                    }
                }
            }

            [Column(Storage = "_Option1WithdrawalAmount", DbType = "Int")]
            public System.Nullable<int> Option1WithdrawalAmount
            {
                get
                {
                    return this._Option1WithdrawalAmount;
                }
                set
                {
                    if ((this._Option1WithdrawalAmount != value))
                    {
                        this._Option1WithdrawalAmount = value;
                    }
                }
            }

            [Column(Storage = "_Option2WithdrawalAmount", DbType = "Int")]
            public System.Nullable<int> Option2WithdrawalAmount
            {
                get
                {
                    return this._Option2WithdrawalAmount;
                }
                set
                {
                    if ((this._Option2WithdrawalAmount != value))
                    {
                        this._Option2WithdrawalAmount = value;
                    }
                }
            }

            [Column(Storage = "_Option3WithdrawalAmount", DbType = "Int")]
            public System.Nullable<int> Option3WithdrawalAmount
            {
                get
                {
                    return this._Option3WithdrawalAmount;
                }
                set
                {
                    if ((this._Option3WithdrawalAmount != value))
                    {
                        this._Option3WithdrawalAmount = value;
                    }
                }
            }

            [Column(Storage = "_Option4WithdrawalAmount", DbType = "Int")]
            public System.Nullable<int> Option4WithdrawalAmount
            {
                get
                {
                    return this._Option4WithdrawalAmount;
                }
                set
                {
                    if ((this._Option4WithdrawalAmount != value))
                    {
                        this._Option4WithdrawalAmount = value;
                    }
                }
            }

            [Column(Storage = "_Option5WithdrawalAmount", DbType = "Int")]
            public System.Nullable<int> Option5WithdrawalAmount
            {
                get
                {
                    return this._Option5WithdrawalAmount;
                }
                set
                {
                    if ((this._Option5WithdrawalAmount != value))
                    {
                        this._Option5WithdrawalAmount = value;
                    }
                }
            }
        }
        
        ///////////////////

		
		[Function(Name="dbo.esp_InsertAFTSettings")]
		public int InsertAFTSettings(
					[Parameter(Name="AFTTransactionsAllowed", DbType="Bit")] System.Nullable<bool> aFTTransactionsAllowed, 
					[Parameter(Name="AllowCashableDeposits", DbType="Bit")] System.Nullable<bool> allowCashableDeposits, 
					[Parameter(Name="AllowNonCashableDeposits", DbType="Bit")] System.Nullable<bool> allowNonCashableDeposits, 
					[Parameter(Name="AllowRedeemOffers", DbType="Bit")] System.Nullable<bool> allowRedeemOffers, 
					[Parameter(Name="AllowPointsWithdrawal", DbType="Bit")] System.Nullable<bool> allowPointsWithdrawal, 
					[Parameter(Name="AllowCashWithdrawal", DbType="Bit")] System.Nullable<bool> allowCashWithdrawal, 
					[Parameter(Name="AllowPartialTransfers", DbType="Bit")] System.Nullable<bool> allowPartialTransfers, 
					[Parameter(Name="AutoDepositNonCashableCreditsonCardOut", DbType="Bit")] System.Nullable<bool> autoDepositNonCashableCreditsonCardOut, 
					[Parameter(Name="AutoDepositCashableCreditsonCardOut", DbType="Bit")] System.Nullable<bool> autoDepositCashableCreditsonCardOut, 
					[Parameter(Name="AllowOffers", DbType="Bit")] System.Nullable<bool> allowOffers, 
					[Parameter(Name="EFTTimeoutValue", DbType="Int")] System.Nullable<int> eFTTimeoutValue, 
					[Parameter(Name="Option1WithdrawalAmount", DbType="Int")] System.Nullable<int> option1WithdrawalAmount, 
					[Parameter(Name="Option2WithdrawalAmount", DbType="Int")] System.Nullable<int> option2WithdrawalAmount, 
					[Parameter(Name="Option3WithdrawalAmount", DbType="Int")] System.Nullable<int> option3WithdrawalAmount, 
					[Parameter(Name="Option4WithdrawalAmount", DbType="Int")] System.Nullable<int> option4WithdrawalAmount, 
					[Parameter(Name="Option5WithdrawalAmount", DbType="Int")] System.Nullable<int> option5WithdrawalAmount, 
					[Parameter(Name="MaxDepositAmount", DbType="Int")] System.Nullable<int> maxDepositAmount, 
					[Parameter(Name="MaxWithdrawAmount", DbType="Int")] System.Nullable<int> maxWithdrawAmount, 
					[Parameter(Name="SiteID", DbType="Int")] System.Nullable<int> siteID, 
					[Parameter(Name="Denom", DbType="Int")] System.Nullable<int> denom)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), aFTTransactionsAllowed, allowCashableDeposits, allowNonCashableDeposits, allowRedeemOffers, allowPointsWithdrawal, allowCashWithdrawal, allowPartialTransfers, autoDepositNonCashableCreditsonCardOut, autoDepositCashableCreditsonCardOut, allowOffers, eFTTimeoutValue, option1WithdrawalAmount, option2WithdrawalAmount, option3WithdrawalAmount, option4WithdrawalAmount, option5WithdrawalAmount, maxDepositAmount, maxWithdrawAmount, siteID, denom);
			return ((int)(result.ReturnValue));
		}

        [Function(Name = "dbo.usp_deleteAFTSettings")]
        public int deleteAFTSettings([Parameter(Name = "SiteID", DbType = "Int")] System.Nullable<int> siteID, [Parameter(Name = "Val", DbType = "VarChar(1000)")] string val)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), siteID, val);
            return ((int)(result.ReturnValue));
        }
        //[Function(Name = "dbo.usp_deleteAFTSettings")]
        //public int deleteAFTSettings([Parameter(Name = "SiteID", DbType = "Int")] System.Nullable<int> siteID, [Parameter(Name = "DENOM", DbType = "Int")] System.Nullable<int> @val)
        //{
        //    IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), siteID, @val);
        //    return ((int)(result.ReturnValue));
        //}
	}
    }
	






