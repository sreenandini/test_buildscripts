using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.EnterpriseBusiness.Entities
{
    public partial  class AftsettingsEntity
    {
        private System.Nullable<int> _Denom;
       
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

        private int _AFTSetting_NO;

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

    public class CompareAFTEntity
    {
        public string Name { get; set; }
        public ValueType Value { get; set; }
    }
}
