using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
    #region Pre-commitment Status Request

    /// <summary>
    /// GMU to Host Freeform for Pre-commitment Status Request
    /// </summary>
    public class FFTgt_G2H_PC_StatusRequest
        : FFTgt_B2B_PC_Player_AccDetails, IFFTgt_G2H
    {
        #region Private Data Members

        private string _playerPIN;

        #endregion //Private Data Members

        #region Properties

        /// <summary>
        /// Player PIN
        /// </summary>
        public string PlayerPIN
        {
            get
            {
                return this._playerPIN;
            }
            set
            {
                if (this._playerPIN == value) return;
                this._playerPIN = value;
            }
        }

        #endregion //Properties

        public override int EntityId
        {
            get
            {
                return (int)FF_AppId_PreCommitment_Action.Status;
            }
        }
    }

    #endregion //Pre-commitment Status Request

    #region Pre-commitment Status Response

    /// <summary>
    /// Host to GMU Freeform for Pre-commitment Status Response
    /// </summary>
    public class FFTgt_H2G_PC_StatusResponse
        : FFTgt_B2B_Precommitment_Data, IFFTgt_H2G
    {
        #region Private Data Members

        private string _status;
        private bool _isPCEnrolled;

        private bool _isDayTimeBasis;
        private TimeSpan _dayTargetTime;
        private TimeSpan _dayCurrentTargetTime;

        private bool _isWeekTimeBasis;
        private byte _weekTargetTime;
        private byte _weekCurrentTargetTime;

        private bool _isMonthTimeBasis;
        private byte _monthTargetTime;
        private byte _monthCurrentTargetTime;

        private bool _isDayLossBasis;
        private double _dayTargetLoss;
        private double _dayCurrentTargetLoss;

        private bool _isWeekLossBasis;
        private double _weekTargetLoss;
        private double _weekCurrentTargetLoss;

        private bool _isMonthLossBasis;
        private double _monthTargetLoss;
        private double _monthCurrentTargetLoss;

        private bool _isDayWagerBasis;
        private double _dayTargetWager;
        private double _dayCurrentTargetWager;

        private bool _isWeekWagerBasis;
        private double _weekTargetWager;
        private double _weekCurrentTargetWager;

        private bool _isMonthWagerBasis;
        private double _monthTargetWager;
        private double _monthCurrentTargetWager;

        private bool _isConsecutiveDaysBasis;
        private int _targetConsecutiveDays;
        private int _currentConsecutiveDays;

        #endregion //Private Data Members

        #region Properties

        /// <summary>
        /// Status
        /// </summary>
        public string Status
        {
            get
            {
                return this._status;
            }
            set
            {
                if (this._status == value) return;
                this._status = value;
            }
        }

        /// <summary>
        /// Whether Enrolled for PC
        /// </summary>
        public bool IsPCEnrolled
        {
            get
            {
                return this._isPCEnrolled;
            }
            set
            {
                if (this._isPCEnrolled == value) return;
                this._isPCEnrolled = value;
            }
        }

        /// <summary>
        /// Whether Day Time Basis Enabled
        /// </summary>
        public bool IsDayTimeBasis
        {
            get
            {
                return this._isDayTimeBasis;
            }
            set
            {
                if (this._isDayTimeBasis == value) return;
                this._isDayTimeBasis = value;
            }
        }

        /// <summary>
        /// Day Configured Target Time
        /// </summary>
        public TimeSpan DayTargetTime
        {
            get
            {
                return this._dayTargetTime;
            }
            set
            {
                this._dayTargetTime = value;
            }
        }

        /// <summary>
        /// Day Current Target Time
        /// </summary>
        public TimeSpan DayCurrentTargetTime
        {
            get
            {
                return this._dayCurrentTargetTime;
            }
            set
            {
                this._dayCurrentTargetTime = value;
            }
        }

        /// <summary>
        /// Whether Week Time Basis Enabled
        /// </summary>
        public bool IsWeekTimeBasis
        {
            get
            {
                return this._isWeekTimeBasis;
            }
            set
            {
                if (this._isWeekTimeBasis == value) return;
                this._isWeekTimeBasis = value;
            }
        }

        /// <summary>
        /// Week Configured Target Time
        /// </summary>
        public byte WeekTargetTime
        {
            get
            {
                return this._weekTargetTime;
            }
            set
            {
                this._weekTargetTime = value;
            }
        }

        /// <summary>
        /// Week Current Target Time
        /// </summary>
        public byte WeekCurrentTargetTime
        {
            get
            {
                return this._weekCurrentTargetTime;
            }
            set
            {
                this._weekCurrentTargetTime = value;
            }
        }

        /// <summary>
        /// Whether Month Time Basis Enabled
        /// </summary>
        public bool IsMonthTimeBasis
        {
            get
            {
                return this._isMonthTimeBasis;
            }
            set
            {
                if (this._isMonthTimeBasis == value) return;
                this._isMonthTimeBasis = value;
            }
        }

        /// <summary>
        /// Month Configured Target Time
        /// </summary>
        public byte MonthTargetTime
        {
            get
            {
                return this._monthTargetTime;
            }
            set
            {
                this._monthTargetTime = value;
            }
        }

        /// <summary>
        /// Month Current Target Time
        /// </summary>
        public byte MonthCurrentTargetTime
        {
            get
            {
                return this._monthCurrentTargetTime;
            }
            set
            {
                this._monthCurrentTargetTime = value;
            }
        }

        /// <summary>
        /// Whether Day Loss Basis Enabled
        /// </summary>
        public bool IsDayLossBasis
        {
            get
            {
                return this._isDayLossBasis;
            }
            set
            {
                if (this._isDayLossBasis == value) return;
                this._isDayLossBasis = value;
            }
        }

        /// <summary>
        /// Day Configured Target Time
        /// </summary>
        public double DayTargetLoss
        {
            get
            {
                return this._dayTargetLoss;
            }
            set
            {
                if (this._dayTargetLoss == value) return;
                this._dayTargetLoss = value;
            }
        }

        /// <summary>
        /// Month Current Target Time
        /// </summary>
        public double DayCurrentTargetLoss
        {
            get
            {
                return this._dayCurrentTargetLoss;
            }
            set
            {
                if (this._dayCurrentTargetLoss == value) return;
                this._dayCurrentTargetLoss = value;
            }
        }

        /// <summary>
        /// Whether Week Loss Basis Enabled
        /// </summary>
        public bool IsWeekLossBasis
        {
            get
            {
                return this._isWeekLossBasis;
            }
            set
            {
                if (this._isWeekLossBasis == value) return;
                this._isWeekLossBasis = value;
            }
        }

        /// <summary>
        /// Week Configured Target Loss
        /// </summary>
        public double WeekTargetLoss
        {
            get
            {
                return this._weekTargetLoss;
            }
            set
            {
                if (this._weekTargetLoss == value) return;
                this._weekTargetLoss = value;
            }
        }

        /// <summary>
        /// Week Current Target Loss
        /// </summary>
        public double WeekCurrentTargetLoss
        {
            get
            {
                return this._weekCurrentTargetLoss;
            }
            set
            {
                if (this._weekCurrentTargetLoss == value) return;
                this._weekCurrentTargetLoss = value;
            }
        }

        /// <summary>
        /// Whether Month Loss Basis Enabled
        /// </summary>
        public bool IsMonthLossBasis
        {
            get
            {
                return this._isMonthLossBasis;
            }
            set
            {
                if (this._isMonthLossBasis == value) return;
                this._isMonthLossBasis = value;
            }
        }

        /// <summary>
        /// Month Configured Target Loss
        /// </summary>
        public double MonthTargetLoss
        {
            get
            {
                return this._monthTargetLoss;
            }
            set
            {
                if (this._monthTargetLoss == value) return;
                this._monthTargetLoss = value;
            }
        }

        /// <summary>
        /// Month Current Target Loss
        /// </summary>
        public double MonthCurrentTargetLoss
        {
            get
            {
                return this._monthCurrentTargetLoss;
            }
            set
            {
                if (this._monthCurrentTargetLoss == value) return;
                this._monthCurrentTargetLoss = value;
            }
        }

        /// <summary>
        /// Whether Day Wager Basis Enabled
        /// </summary>
        public bool IsDayWagerBasis
        {
            get
            {
                return this._isDayWagerBasis;
            }
            set
            {
                if (this._isDayWagerBasis == value) return;
                this._isDayWagerBasis = value;
            }
        }

        /// <summary>
        /// Day Configured Wager
        /// </summary>
        public double DayTargetWager
        {
            get
            {
                return this._dayTargetWager;
            }
            set
            {
                if (this._dayTargetWager == value) return;
                this._dayTargetWager = value;
            }
        }

        /// <summary>
        /// Day Current Target Wager
        /// </summary>
        public double DayCurrentTargetWager
        {
            get
            {
                return this._dayCurrentTargetWager;
            }
            set
            {
                if (this._dayCurrentTargetWager == value) return;
                this._dayCurrentTargetWager = value;
            }
        }

        /// <summary>
        /// Whether Week Wager Basis Enabled
        /// </summary>
        public bool IsWeekWagerBasis
        {
            get
            {
                return this._isWeekWagerBasis;
            }
            set
            {
                if (this._isWeekWagerBasis == value) return;
                this._isWeekWagerBasis = value;
            }
        }

        /// <summary>
        /// Week Configured Wager
        /// </summary>
        public double WeekTargetWager
        {
            get
            {
                return this._weekTargetWager;
            }
            set
            {
                if (this._weekTargetWager == value) return;
                this._weekTargetWager = value;
            }
        }

        /// <summary>
        /// Week Current Target Wager
        /// </summary>
        public double WeekCurrentTargetWager
        {
            get
            {
                return this._weekCurrentTargetWager;
            }
            set
            {
                if (this._weekCurrentTargetWager == value) return;
                this._weekCurrentTargetWager = value;
            }
        }

        /// <summary>
        /// Whether Month Wager Basis Enabled
        /// </summary>
        public bool IsMonthWagerBasis
        {
            get
            {
                return this._isMonthWagerBasis;
            }
            set
            {
                if (this._isMonthWagerBasis == value) return;
                this._isMonthWagerBasis = value;
            }
        }

        /// <summary>
        /// Month Configured Wager
        /// </summary>
        public double MonthTargetWager
        {
            get
            {
                return this._monthTargetWager;
            }
            set
            {
                if (this._monthTargetWager == value) return;
                this._monthTargetWager = value;
            }
        }

        /// <summary>
        /// Month Current Target Wager
        /// </summary>
        public double MonthCurrentTargetWager
        {
            get
            {
                return this._monthCurrentTargetWager;
            }
            set
            {
                if (this._monthCurrentTargetWager == value) return;
                this._monthCurrentTargetWager = value;
            }
        }

        /// <summary>
        /// Whether Consecutive Days Enabled
        /// </summary>
        public bool IsConsecutiveDaysBasis
        {
            get
            {
                return this._isConsecutiveDaysBasis;
            }
            set
            {
                if (this._isConsecutiveDaysBasis == value) return;
                this._isConsecutiveDaysBasis = value;
            }
        }

        /// <summary>
        /// Target Consecutive Days
        /// </summary>
        public int TargetConsecutiveDays
        {
            get
            {
                return this._targetConsecutiveDays;
            }
            set
            {
                if (this._targetConsecutiveDays == value) return;
                this._targetConsecutiveDays = value;
            }
        }

        /// <summary>
        /// Current Consecutive Days
        /// </summary>
        public int CurrentConsecutiveDays
        {
            get
            {
                return this._currentConsecutiveDays;
            }
            set
            {
                if (this._currentConsecutiveDays == value) return;
                this._currentConsecutiveDays = value;
            }
        }

        public int DisplayMessageLength { get; set; }
        public string DisplayMessage { get; set; }

        #endregion //Properties

        public override int EntityId
        {
            get
            {
                return (int)FF_AppId_PreCommitment_Action.Status;
            }
        }
    }

    #endregion //Pre-commitment Status Response

    #region Pre-commitment Status Response Player Card-in

    /// <summary>
    /// This message is from the host to GMU after a Player Card-in event notification. 
    /// </summary>
    public class FFTgt_H2G_PC_StatusResponsePlayerCardIn
        : FFTgt_B2B_Precommitment_Data, IFFTgt_H2G
    {
        /// <summary>
        /// Indicating if player is enrolled or not for Pre commitment. 
        /// </summary>
        public bool PCEnrolled { get; set; }

        /// <summary>
        /// Frequency of handle pulls
        /// </summary>
        public short HandlePulls { get; set; }

        /// <summary>
        /// Frequency of interval ratings (in seconds)
        /// </summary>
        public short RatingInterval { get; set; }

        public override int EntityId
        {
            get
            {
                return (int)FF_AppId_PreCommitment_Action.CardIn;
            }
        }
    }

    #endregion


   


    

}
