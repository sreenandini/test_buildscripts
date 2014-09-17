using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
    #region Notification Message

    public class FFTgt_B2B_PC_NotificationMessage
        : FFTgt_B2B_Precommitment_Data, IFFTgt_H2G
    {
        /// <summary>
        ///For this duration, message should be displayed to patron if not confirmed in between. During this display time, game will be temporarily disabled (in seconds)
        /// </summary>
        public short DisplayTime { get; set; }

        /// <summary>
        /// If display message is not confirmed, after every display interval, display message should be displayed for display time (in seconds)
        /// </summary>
        public short DisplayInterval { get; set; }

        /// <summary>
        /// Gets or sets the display message.
        /// </summary>
        public int DisplayMessageLength { get; set; }

        /// <summary>
        /// Gets or sets the display message.
        /// </summary>
        public string DisplayMessage { get; set; }
    }

    #endregion

    #region Approaching Notification Message

    /// <summary>
    /// This message is sent from the host to GMU when the patron reaches a pre-configured percentage of his limit value. 
    /// </summary>
    public class FFTgt_H2G_PC_ApproachingNotificationMessage
        : FFTgt_B2B_PC_NotificationMessage, IFFTgt_H2G
    {
        public override int EntityId
        {
            get
            {
                return (int)FF_AppId_PreCommitment_Action.ApproachingLimit;
            }
        }

        /// <summary>
        /// Frequency of handle pulls
        /// </summary>
        public short HandlePulls { get; set; }

        /// <summary>
        /// Frequency of handle pulls
        /// </summary>
        public short RatingInterval { get; set; }
    }

    #endregion

    #region Limit reached Notification Message
    /// <summary>
    /// This message is sent from the host to GMU when the patron reaches pre-commitment limits.
    /// </summary>
    public class FFTgt_H2G_PC_LimitReachedNotificationMessage
        : FFTgt_B2B_PC_NotificationMessage
    {
        public override int EntityId
        {
            get
            {
                return (int)FF_AppId_PreCommitment_Action.LimitReached;
            }
        }

        /// <summary>
        /// Gets or sets the type of the lock.
        /// </summary>
        public FF_AppId_PreCommitment_LockType LockType { get; set; }
    }

    #endregion

    #region Relaxed Limit Effective Notification Message
    /// <summary>
    /// This message is sent from the host to GMU indicating that patron’s new relaxed limits are effective and needs confirmation from patron, 
    /// if he wants to continue with new limits or Rollback to the old limits
    /// </summary>
    public class FFTgt_H2G_PC_RelaxedLimitEffectiveNotificationMsg
        : FFTgt_B2B_Precommitment_Data, IFFTgt_H2G
    {
        public override int EntityId
        {
            get
            {
                return (int)FF_AppId_PreCommitment_Action.Status;
            }
        }

        /// <summary>
        /// Indicating if rules based on Time spent on device per Day changed.  
        /// </summary>
        public bool IsDayTimeBasisChanged { get; set; }

        /// <summary>
        /// Indicating the time of new Target. HHMM (HH – Hours; MM – Minutes)
        /// </summary>
        public TimeSpan DayNewTargetTime { get; set; }

        /// <summary>
        /// Indicating the time of old Target. HHMM (HH – Hours; MM – Minutes)
        /// </summary>
        public TimeSpan DayOldTargetTime { get; set; }

        /// <summary>
        /// Indicating if rules are based on Gaming days per Week changed. 
        /// </summary>
        public bool IsWeekTimeBasisChanged { get; set; }

        /// <summary>
        /// Indicating the days set by patron in New Target (DD – Days)
        /// </summary>
        public byte WeekNewTargetTime { get; set; }

        /// <summary>
        /// Indicating the days set by patron in Old Target (DD – Days)
        /// </summary>
        public byte WeekOldTargetTime { get; set; }

        /// <summary>
        /// Indicating if rules are based on Gaming days per Month changed.  
        /// </summary>
        public bool IsMonthTimeBasisChanged { get; set; }

        /// <summary>
        /// Indicating the days set by patron in New Target (DD – Days)
        /// </summary>
        public byte MonthNewTargetTime { get; set; }

        /// <summary>
        /// Indicating the days set by patron in Old Target (DD – Days)
        /// </summary>
        public byte MonthOldTargetTime { get; set; }

        /// <summary>
        /// Indicating if rules are based on Net Loss per Day changed. 
        /// </summary>
        public bool IsDayLossBasisChanged { get; set; }

        /// <summary>
        /// Target loss limit set in New target (in cents)
        /// </summary>
        public double DayNewTargetLoss { get; set; }

        /// <summary>
        /// Target loss limit set in Old target (in cents)
        /// </summary>
        public double DayOldTargetLoss { get; set; }

        /// <summary>
        /// Indicating if rules are based on Net Loss per Week changed. 
        /// </summary>
        public bool IsWeekLossBasisChanged { get; set; }

        /// <summary>
        /// Target loss limit set in new target (in cents)
        /// </summary>
        public double WeekNewTargetLoss { get; set; }

        /// <summary>
        /// Target loss limit set in old target (in cents)
        /// </summary>
        public double WeekOldTargetLoss { get; set; }

        /// <summary>
        /// Indicating if rules are based on Net Loss per Month changed. 
        /// </summary>
        public bool IsMonthLossBasisChanged { get; set; }

        /// <summary>
        /// Target loss limit set in New target (in cents)
        /// </summary>
        public double MonthNewTargetLoss { get; set; }

        /// <summary>
        /// Target loss limit set in Old target (in cents)
        /// </summary>
        public double MonthOldTargetLoss { get; set; }

        /// <summary>
        /// Indicating if rules are based on Total wagers (Turnover) per Day changed. 
        /// </summary>
        public bool IsDayWagerBasisChanged { get; set; }

        /// <summary>
        /// Target wagers set in new target (in cents)
        /// </summary>
        public double DayNewTargetWager { get; set; }

        /// <summary>
        /// Target wagers set in old target (in cents)
        /// </summary>
        public double DayOldTargetWager { get; set; }

        /// <summary>
        /// Indicating if rules are based on Total wagers (Turnover) per Week changed. 
        /// </summary>
        public bool IsWeekWagerBasisChanged { get; set; }

        /// <summary>
        /// Target wagers set in New target (in cents)
        /// </summary>
        public double WeekNewTargetWager { get; set; }

        /// <summary>
        /// Target wagers set in Old target (in cents)
        /// </summary>
        public double WeekOldTargetWager { get; set; }

        /// <summary>
        /// Indicating if rules are based on Total wagers (Turnover) per Month changed. 
        /// </summary>
        public bool IsMonthWagerBasisChanged { get; set; }

        /// <summary>
        /// Target wagers set in New target (in cents)
        /// </summary>
        public double MonthNewTargetWager { get; set; }

        /// <summary>
        /// Target wagers in cents set in old target (in cents)
        /// </summary>
        public double MonthOldTargetWager { get; set; }

        /// <summary>
        /// Indicating if player has consecutive days changed. 
        /// </summary>
        public bool IsConsecutiveDaysBasis { get; set; }

        /// <summary>
        /// Indicating the number of target consecutive days in new target (DD – Days)
        /// </summary>
        public byte NewTargetConsecutiveDays { get; set; }

        /// <summary>
        /// Indicating the number of target consecutive days in old target (DD – Days)
        /// </summary>
        public byte OldTargetConsecutiveDays { get; set; }
    }

    #endregion

    #region Notification Response
    /// <summary>
    /// This message is sent by the GMU to host as a response to Approaching/Limit-Reached/Relaxed Limit Effective Notification Messages. 
    /// GMU can also send this message to the host when patron selects an option on break period notification for pre-commitment.
    /// </summary>
    public class FFTgt_G2H_PC_NotificationResponse 
        : FFTgt_B2B_PC_Player_AccDetails, IFFTgt_G2H
    {
        public override int EntityId
        {
            get
            {
                return (int)FF_AppId_PreCommitment_Action.NotificationResponseAck;
            }
        }

        public FF_AppId_PreCommitmentAcknowledgementTypes AcknowledgementType { get; set; }
    }
    #endregion
}
