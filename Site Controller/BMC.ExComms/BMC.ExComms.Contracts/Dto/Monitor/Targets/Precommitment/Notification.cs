using BMC.ExComms.Contracts.DTO.Freeform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Monitor
{
    #region Notification Message

    /// <summary>
    /// Host to GMU Notifications (or) GMU to Host Notifications Response
    /// </summary>
    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Monitor",
        Name = "MonTgt_B2B_PC_NotificationMessage")]
    [ExCommsMessageKnownType]
    public class MonTgt_B2B_PC_NotificationMessage
        : MonTgt_B2B, IMonTgt_B2B
    {

        #region Constructor

        public MonTgt_B2B_PC_NotificationMessage()
        {
        }

        #endregion Constructor

        #region Properties

        /// <summary>
        ///For this duration, message should be displayed to patron if not confirmed in between. During this display time, game will be temporarily disabled (in seconds)
        /// </summary>
        [DataMember]
        public short DisplayTime { get; set; }

        /// <summary>
        /// If display message is not confirmed, after every display interval, display message should be displayed for display time (in seconds)
        /// </summary>
        [DataMember]
        public short DisplayInterval { get; set; }

        /// <summary>
        /// Gets or sets the display message.
        /// </summary>
        public int DisplayMessageLength { get; set; }

        /// <summary>
        /// Gets or sets the display message.
        /// </summary>
        [DataMember]
        public string DisplayMessage { get; set; }

        #endregion //Properties
    }

    #endregion //Notification Message

    #region Approaching Notification Message

    /// <summary>
    /// This message is sent from the host to GMU when the patron reaches a pre-configured percentage of his limit value. 
    /// </summary>
    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Monitor",
        Name = "MonTgt_H2G_PC_ApproachingNotificationMessage")]
    [ExCommsMessageKnownType]
    public class MonTgt_H2G_PC_ApproachingNotificationMessage
        : MonTgt_B2B_PC_NotificationMessage, IMonTgt_H2G
    {

        #region Constructor

        public MonTgt_H2G_PC_ApproachingNotificationMessage()
        {
        }

        #endregion Constructor

        #region Properties

        /// <summary>
        /// Frequency of handle pulls
        /// </summary>
        [DataMember]
        public short HandlePulls { get; set; }

        /// <summary>
        /// Frequency of Rating Interval
        /// </summary>
        [DataMember]
        public short RatingInterval { get; set; }

        #endregion //Properties
    }

    #endregion //Approaching Notification Message

    #region Limit reached Notification Message

    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Monitor",
        Name = "MonTgt_H2G_PC_LimitReachedNotificationMessage")]
    [ExCommsMessageKnownType]
    public class MonTgt_H2G_PC_LimitReachedNotificationMessage
        : MonTgt_B2B_PC_NotificationMessage, IMonTgt_H2G
    {

        #region Constructor

        public MonTgt_H2G_PC_LimitReachedNotificationMessage()
        {
        }

        #endregion Constructor

        #region Properties

        /// <summary>
        /// Gets or sets the type of the lock.
        /// </summary>
        [DataMember]
        public FF_AppId_PreCommitment_LockType LockType { get; set; }

        #endregion //Properties
    }

    #endregion //Limit reached Notification Message

    #region Relaxed Limit Effective Notification Message

    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Monitor",
        Name = "MonTgt_H2G_PC_RelaxedLimitEffectiveNotificationMsg")]
    [ExCommsMessageKnownType]
    public class MonTgt_H2G_PC_RelaxedLimitEffectiveNotificationMsg
        : MonTgt_B2B_PC_NotificationMessage, IMonTgt_H2G
    {

        #region Constructor

        public MonTgt_H2G_PC_RelaxedLimitEffectiveNotificationMsg()
        {
        }

        #endregion Constructor

        #region Properties

        /// <summary>
        /// Indicating if rules based on Time spent on device per Day changed.  
        /// </summary>
        [DataMember]
        public bool IsDayTimeBasisChanged { get; set; }

        /// <summary>
        /// Indicating the time of new Target. HHMM (HH – Hours; MM – Minutes)
        /// </summary>
        [DataMember]
        public TimeSpan DayNewTargetTime { get; set; }

        /// <summary>
        /// Indicating the time of old Target. HHMM (HH – Hours; MM – Minutes)
        /// </summary>
        [DataMember]
        public TimeSpan DayOldTargetTime { get; set; }

        /// <summary>
        /// Indicating if rules are based on Gaming days per Week changed. 
        /// </summary>
        [DataMember]
        public bool IsWeekTimeBasisChanged { get; set; }

        /// <summary>
        /// Indicating the days set by patron in New Target (DD – Days)
        /// </summary>
        [DataMember]
        public byte WeekNewTargetTime { get; set; }

        /// <summary>
        /// Indicating the days set by patron in Old Target (DD – Days)
        /// </summary>
        [DataMember]
        public byte WeekOldTargetTime { get; set; }

        /// <summary>
        /// Indicating if rules are based on Gaming days per Month changed.  
        /// </summary>
        [DataMember]
        public bool IsMonthTimeBasisChanged { get; set; }

        /// <summary>
        /// Indicating the days set by patron in New Target (DD – Days)
        /// </summary>
        [DataMember]
        public byte MonthNewTargetTime { get; set; }

        /// <summary>
        /// Indicating the days set by patron in Old Target (DD – Days)
        /// </summary>
        [DataMember]
        public byte MonthOldTargetTime { get; set; }

        /// <summary>
        /// Indicating if rules are based on Net Loss per Day changed. 
        /// </summary>
        [DataMember]
        public bool IsDayLossBasisChanged { get; set; }

        /// <summary>
        /// Target loss limit set in New target (in cents)
        /// </summary>
        [DataMember]
        public double DayNewTargetLoss { get; set; }

        /// <summary>
        /// Target loss limit set in Old target (in cents)
        /// </summary>
        [DataMember]
        public double DayOldTargetLoss { get; set; }

        /// <summary>
        /// Indicating if rules are based on Net Loss per Week changed. 
        /// </summary>
        [DataMember]
        public bool IsWeekLossBasisChanged { get; set; }

        /// <summary>
        /// Target loss limit set in new target (in cents)
        /// </summary>
        [DataMember]
        public double WeekNewTargetLoss { get; set; }

        /// <summary>
        /// Target loss limit set in old target (in cents)
        /// </summary>
        [DataMember]
        public double WeekOldTargetLoss { get; set; }

        /// <summary>
        /// Indicating if rules are based on Net Loss per Month changed. 
        /// </summary>
        [DataMember]
        public bool IsMonthLossBasisChanged { get; set; }

        /// <summary>
        /// Target loss limit set in New target (in cents)
        /// </summary>
        [DataMember]
        public double MonthNewTargetLoss { get; set; }

        /// <summary>
        /// Target loss limit set in Old target (in cents)
        /// </summary>
        [DataMember]
        public double MonthOldTargetLoss { get; set; }

        /// <summary>
        /// Indicating if rules are based on Total wagers (Turnover) per Day changed. 
        /// </summary>
        [DataMember]
        public bool IsDayWagerBasisChanged { get; set; }

        /// <summary>
        /// Target wagers set in new target (in cents)
        /// </summary>
        [DataMember]
        public double DayNewTargetWager { get; set; }

        /// <summary>
        /// Target wagers set in old target (in cents)
        /// </summary>
        [DataMember]
        public double DayOldTargetWager { get; set; }

        /// <summary>
        /// Indicating if rules are based on Total wagers (Turnover) per Week changed. 
        /// </summary>
        [DataMember]
        public bool IsWeekWagerBasisChanged { get; set; }

        /// <summary>
        /// Target wagers set in New target (in cents)
        /// </summary>
        [DataMember]
        public double WeekNewTargetWager { get; set; }

        /// <summary>
        /// Target wagers set in Old target (in cents)
        /// </summary>
        [DataMember]
        public double WeekOldTargetWager { get; set; }

        /// <summary>
        /// Indicating if rules are based on Total wagers (Turnover) per Month changed. 
        /// </summary>
        [DataMember]
        public bool IsMonthWagerBasisChanged { get; set; }

        /// <summary>
        /// Target wagers set in New target (in cents)
        /// </summary>
        [DataMember]
        public double MonthNewTargetWager { get; set; }

        /// <summary>
        /// Target wagers in cents set in old target (in cents)
        /// </summary>
        [DataMember]
        public double MonthOldTargetWager { get; set; }

        /// <summary>
        /// Indicating if player has consecutive days changed. 
        /// </summary>
        [DataMember]
        public bool IsConsecutiveDaysBasis { get; set; }

        /// <summary>
        /// Indicating the number of target consecutive days in new target (DD – Days)
        /// </summary>
        [DataMember]
        public byte NewTargetConsecutiveDays { get; set; }

        /// <summary>
        /// Indicating the number of target consecutive days in old target (DD – Days)
        /// </summary>
        [DataMember]
        public byte OldTargetConsecutiveDays { get; set; }

        #endregion //Properties
    }

    #endregion //Relaxed Limit Effective Notification Message

    #region Notification Response

    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Monitor",
        Name = "MonTgt_G2H_PC_NotificationResponse")]
    [ExCommsMessageKnownType]
    public class MonTgt_G2H_PC_NotificationResponse
        : MonTgt_B2B_PC_Player_AccDetails, IMonTgt_G2H
    {
        #region Constructor

        public MonTgt_G2H_PC_NotificationResponse()
        {
        }

        #endregion Constructor

        #region Properties

        [DataMember]
        public FF_AppId_PreCommitmentAcknowledgementTypes AcknowledgementType { get; set; }

        #endregion //Properties
    }

    #endregion //Notification Response
}
