using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Monitor
{
    #region Pre-commitment Status Request

    /// <summary>
    /// GMU to Host monitor target for Pre-commitment Status Request
    /// </summary>
    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Monitor",
        Name = "MonTgt_G2H_PC_StatusRequest")]
    [ExCommsMessageKnownType]
    public class MonTgt_G2H_PC_StatusRequest
        : MonTgt_B2B_PC_Player_AccDetails, IMonTgt_G2H
    {

        #region Constructor

        public MonTgt_G2H_PC_StatusRequest()
        {
        }

        #endregion //Constructor

        #region Properties

        [DataMember]
        public string PlayerPIN { get; set; }

        #endregion //Properties
    }

    #endregion //Pre-commitment Status Request

    #region Pre-commitment Status Response

    /// <summary>
    /// Host to GMU monitor for Pre-commitment Status Response
    /// </summary>
    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Monitor",
        Name = "MonTgt_H2G_PC_StatusResponse")]
    [ExCommsMessageKnownType]
    public class MonTgt_H2G_PC_StatusResponse
        : MonTgt_B2B_Precommitment_Data, IMonTgt_H2G
    {

        #region Constructor

        public MonTgt_H2G_PC_StatusResponse()
        {
        }

        #endregion //Constructor

        #region Properties

        /// <summary>
        /// PC Status
        /// </summary>
        [DataMember]
        public string Status { get; set; }

        /// <summary>
        /// Whether Enrolled for PC
        /// </summary>
        [DataMember]
        public bool IsPCEnrolled { get; set; }

        /// <summary>
        /// Whether Day Time Basis Enabled
        /// </summary>
        [DataMember]
        public bool IsDayTimeBasis { get; set; }

        /// <summary>
        /// Day Configured Target Time
        /// </summary>
        [DataMember]
        public TimeSpan DayTargetTime { get; set; }

        /// <summary>
        /// Day Current Target Time
        /// </summary>
        [DataMember]
        public TimeSpan DayCurrentTargetTime { get; set; }

        /// <summary>
        /// Whether Week Time Basis Enabled
        /// </summary>
        [DataMember]
        public bool IsWeekTimeBasis { get; set; }

        /// <summary>
        /// Week Configured Target Time
        /// </summary>
        [DataMember]
        public byte WeekTargetTime { get; set; }

        /// <summary>
        /// Week Current Target Time
        /// </summary>
        [DataMember]
        public byte WeekCurrentTargetTime { get; set; }

        /// <summary>
        /// Whether Month Time Basis Enabled
        /// </summary>
        [DataMember]
        public bool IsMonthTimeBasis { get; set; }

        /// <summary>
        /// Month Configured Target Time
        /// </summary>
        [DataMember]
        public byte MonthTargetTime { get; set; }

        /// <summary>
        /// Month Current Target Time
        /// </summary>
        [DataMember]
        public byte MonthCurrentTargetTime { get; set; }

        /// <summary>
        /// Whether Day Loss Basis Enabled
        /// </summary>
        [DataMember]
        public bool IsDayLossBasis { get; set; }

        /// <summary>
        /// Day Configured Target Time
        /// </summary>
        [DataMember]
        public double DayTargetLoss { get; set; }

        /// <summary>
        /// Month Current Target Time
        /// </summary>
        [DataMember]
        public double DayCurrentTargetLoss { get; set; }

        /// <summary>
        /// Whether Week Loss Basis Enabled
        /// </summary>
        [DataMember]
        public bool IsWeekLossBasis { get; set; }

        /// <summary>
        /// Week Configured Target Loss
        /// </summary>
        [DataMember]
        public double WeekTargetLoss { get; set; }

        /// <summary>
        /// Week Current Target Loss
        /// </summary>
        [DataMember]
        public double WeekCurrentTargetLoss { get; set; }

        /// <summary>
        /// Whether Month Loss Basis Enabled
        /// </summary>
        [DataMember]
        public bool IsMonthLossBasis { get; set; }

        /// <summary>
        /// Month Configured Target Loss
        /// </summary>
        [DataMember]
        public double MonthTargetLoss { get; set; }

        /// <summary>
        /// Month Current Target Loss
        /// </summary>
        [DataMember]
        public double MonthCurrentTargetLoss { get; set; }

        /// <summary>
        /// Whether Day Wager Basis Enabled
        /// </summary>
        [DataMember]
        public bool IsDayWagerBasis { get; set; }

        /// <summary>
        /// Day Configured Wager
        /// </summary>
        [DataMember]
        public double DayTargetWager { get; set; }

        /// <summary>
        /// Day Current Target Wager
        /// </summary>
        [DataMember]
        public double DayCurrentTargetWager { get; set; }

        /// <summary>
        /// Whether Week Wager Basis Enabled
        /// </summary>
        [DataMember]
        public bool IsWeekWagerBasis { get; set; }

        /// <summary>
        /// Week Configured Wager
        /// </summary>
        [DataMember]
        public double WeekTargetWager { get; set; }

        /// <summary>
        /// Week Current Target Wager
        /// </summary>
        [DataMember]
        public double WeekCurrentTargetWager { get; set; }

        /// <summary>
        /// Whether Month Wager Basis Enabled
        /// </summary>
        [DataMember]
        public bool IsMonthWagerBasis { get; set; }

        /// <summary>
        /// Month Configured Wager
        /// </summary>
        public double MonthTargetWager { get; set; }

        /// <summary>
        /// Month Current Target Wager
        /// </summary>
        [DataMember]
        public double MonthCurrentTargetWager { get; set; }

        /// <summary>
        /// Whether Consecutive Days Enabled
        /// </summary>
        [DataMember]
        public bool IsConsecutiveDaysBasis { get; set; }

        /// <summary>
        /// Target Consecutive Days
        /// </summary>
        [DataMember]
        public int TargetConsecutiveDays { get; set; }

        /// <summary>
        /// Current Consecutive Days
        /// </summary>
        [DataMember]
        public int CurrentConsecutiveDays { get; set; }

        /// <summary>
        /// Display message length
        /// </summary>
        [DataMember]
        public int DisplayMessageLength { get; set; }

        /// <summary>
        /// Display message
        /// </summary>
        [DataMember]
        public string DisplayMessage { get; set; }

        #endregion //Properties
    }

    #endregion //Pre-commitment Status Response

    #region Pre-commitment Status Response Player Card-in

    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Monitor",
        Name = "MonTgt_H2G_PC_StatusResponsePlayerCardIn")]
    [ExCommsMessageKnownType]
    public class MonTgt_H2G_PC_StatusResponsePlayerCardIn
        : MonTgt_B2B_Precommitment_Data, IMonTgt_H2G
    {

        #region Constructor

        public MonTgt_H2G_PC_StatusResponsePlayerCardIn()
        {
        }

        #endregion //Constructor

        #region Properties

        /// <summary>
        /// Indicating if player is enrolled or not for Pre commitment. 
        /// </summary>
        [DataMember]
        public bool PCEnrolled { get; set; }

        /// <summary>
        /// Frequency of handle pulls
        /// </summary>
        [DataMember]
        public short HandlePulls { get; set; }

        /// <summary>
        /// Frequency of interval ratings (in seconds)
        /// </summary>
        [DataMember]
        public short RatingInterval { get; set; }

        #endregion //Properties
    }

    #endregion //Pre-commitment Status Response Player Card-in
}
