using BMC.Common.ExceptionManagement;
using BMC.ExComms.Contracts.DTO.Freeform;
using BMC.ExComms.Contracts.DTO.Monitor;
using BMC.ExComms.DataLayer.MSSQL;
using BMC.ExMonitor.Server.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExMonitor.Server.Precommitment
{
    /// <summary>
    /// Precommitment Response message handler class
    /// </summary>
    public class PCResponseHandler
    {
        #region Data Members

        private static PCResponseHandler _pcResponseHandler = null;

        #endregion //Data Members

        #region Constructor

        public PCResponseHandler()
        {
        }

        #endregion //Constructor

        #region Properties

        public static PCResponseHandler PCResponseHandlerInstance
        {
            get
            {
                if (_pcResponseHandler == null)
                    _pcResponseHandler = new PCResponseHandler();

                return _pcResponseHandler;
            }
        }

        #endregion //Properties

        #region Public Memebers

        /// <summary>
        /// To insert Approach Limit Notificaation message into the DB
        /// </summary>
        /// <param name="strResponse"></param>
        /// <returns></returns>
        public bool InsertApproachLimitNotification(string strResponse)
        {
            try
            {
                return ExCommsDataContext.Current.InsertPCMessage(
                                                                GetCardNumber(strResponse), //Card Number
                                                                GetSlotNumber(strResponse), //Slot Number 
                                                                GetStand(strResponse), //Stand
                                                                Convert.ToInt32(GetHandlePullsForApproachLimit(strResponse)), //Handle Pulls
                                                                GetRatingIntervalForApproachLimit(strResponse), //Rating Interval
                                                                strResponse, // Message
                                                                null,
                                                                "A", //Ack Type
                                                                HandlerHelper.Current.BreakPeriodInterval, //Break Period
                                                                HandlerHelper.Current.IsPreCommitmentEnabled //PC Enrolled
                                                                );
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return false;
        }

        /// <summary>
        /// To insert Limit Reached Notificaation message into the DB
        /// </summary>
        /// <param name="strResponse"></param>
        /// <returns></returns>
        public bool InsertLimitReachedNotification(string strResponse)
        {
            try
            {
                return ExCommsDataContext.Current.InsertPCMessage(
                                                                GetCardNumber(strResponse), //Card Number
                                                                GetSlotNumber(strResponse), //Slot Number 
                                                                GetStand(strResponse), //Stand
                                                                null, //Handle Pulls
                                                                null, //Rating Interval
                                                                strResponse, // Message
                                                                GetLockTypeForLimitReached(strResponse), //LockType
                                                                "L", //Ack Type
                                                                HandlerHelper.Current.BreakPeriodInterval, //Break Period
                                                                HandlerHelper.Current.IsPreCommitmentEnabled  //PC Enrolled
                                                                );
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return false;
        }

        /// <summary>
        /// To insert Card in Response to the DB
        /// </summary>
        /// <param name="strResponse"></param>
        /// <returns></returns>
        public bool InsertCardInResponseFromPC(string strResponse)
        {
            try
            {
                return ExCommsDataContext.Current.InsertPCCardResponse(
                                                                GetCardNumber(strResponse), //Card Number
                                                                GetMessageType(strResponse), //Message Type
                                                                GetTransactionCode(strResponse), //Transaction Code
                                                                GetSlotNumber(strResponse), //Slot Number 
                                                                GetStand(strResponse), //Stand
                                                                strResponse, //Actual Response
                                                                DateTime.Now,
                                                                Convert.ToInt32(GetHandlePullsForCardIn(strResponse)), //Handle Pulls
                                                                GetRatingIntervalForCardIn(strResponse), //Rating Interval
                                                                HandlerHelper.Current.BreakPeriodInterval.ToString(),//strResponse.Substring(45, 3), //Break Period
                                                                GetPCEnrolled(strResponse) //PC Enrolled
                                                                );
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return false;
        }

        /// <summary>
        /// To insert Relaxed Limit Notificaation message into the DB
        /// </summary>
        /// <param name="strResponse"></param>
        /// <returns></returns>
        public bool InsertRelaxedLimitdNotification(string strResponse)
        {
            try
            {
                return ExCommsDataContext.Current.InsertPCMessage(
                                                                    GetCardNumber(strResponse), //Card Number
                                                                    GetSlotNumber(strResponse), //Slot Number 
                                                                    GetStand(strResponse), //Stand
                                                                    null, //Handle Pulls
                                                                    null, //Rating Interval
                                                                    strResponse, // Message
                                                                    null, //LockType
                                                                    "R", //Ack Type
                                                                    HandlerHelper.Current.BreakPeriodInterval, //Break Period
                                                                    true //PC Enrolled
                                                                    );
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return false;
        }

        /// <summary>
        /// To fetch un send records from DB, for send it to Comms
        /// </summary>
        /// <param name="iRecordsToProcess"></param>
        /// <returns></returns>
        public PC_ServerTrackingResult GetDataForSendToComms(ref int? iRecordsToProcess)
        {
            try
            {
                return ExCommsDataContext.Current.Get_PCMessages_SendToComms(HandlerHelper.Current.MaxRows, ref iRecordsToProcess);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return new PC_ServerTrackingResult();
            }
        }

        /// <summary>
        /// To update monitor entity sent status in DB
        /// </summary>
        /// <param name="iPC_ST_ID"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public bool UpdateSentFreeFormMsgToCommsStatus(int id, bool status)
        {
            try
            {
                return ExCommsDataContext.Current.UpdateSentMonitorMessagestatus(id, status);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return false;
        }

        #region Monitor Entity Creation

        /// <summary>
        /// To create Approach Limit Monitor message from response string
        /// </summary>
        /// <param name="strResponse"></param>
        /// <returns></returns>
        public MonTgt_H2G_PC_ApproachingNotificationMessage GetApproachLimitMessage(string strResponse)
        {
            try
            {
                MonTgt_H2G_PC_ApproachingNotificationMessage montgtApproachLimit = new MonTgt_H2G_PC_ApproachingNotificationMessage();

                if (HandlerHelper.Current.IsExtendedPlayer)
                {
                    montgtApproachLimit.DisplayTime = Convert.ToInt16(strResponse.Substring(50, 4));
                    montgtApproachLimit.DisplayInterval = Convert.ToInt16(strResponse.Substring(54, 4));

                    montgtApproachLimit.HandlePulls = Convert.ToInt16(strResponse.Substring(196, 4));
                    montgtApproachLimit.RatingInterval = Convert.ToInt16(strResponse.Substring(200, 4));
                }
                else
                {
                    montgtApproachLimit.DisplayTime = Convert.ToInt16(strResponse.Substring(48, 4));
                    montgtApproachLimit.DisplayInterval = Convert.ToInt16(strResponse.Substring(52, 4));

                    montgtApproachLimit.HandlePulls = Convert.ToInt16(strResponse.Substring(194, 4));
                    montgtApproachLimit.RatingInterval = Convert.ToInt16(strResponse.Substring(198, 4));
                }

                montgtApproachLimit.DisplayMessage = GetDisplayMessageForApproachLimit(strResponse);
                montgtApproachLimit.DisplayMessageLength = montgtApproachLimit.DisplayMessage.Length;
                return montgtApproachLimit;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return new MonTgt_H2G_PC_ApproachingNotificationMessage();
            }
        }

        /// <summary>
        /// To create Limit Reached Monitor message from response string
        /// </summary>
        /// <param name="strResponse"></param>
        /// <returns></returns>
        public MonTgt_H2G_PC_LimitReachedNotificationMessage GetLimitReachedMessage(string strResponse)
        {
            try
            {
                MonTgt_H2G_PC_LimitReachedNotificationMessage monTgtLimitReached = new MonTgt_H2G_PC_LimitReachedNotificationMessage();

                if (HandlerHelper.Current.IsExtendedPlayer)
                {
                    monTgtLimitReached.LockType = (FF_AppId_PreCommitment_LockType)Convert.ToInt32(strResponse.Substring(50, 1));
                    monTgtLimitReached.DisplayTime = Convert.ToInt16(strResponse.Substring(51, 4));
                    monTgtLimitReached.DisplayInterval = Convert.ToInt16(strResponse.Substring(55, 4));
                }
                else
                {
                    monTgtLimitReached.LockType = (FF_AppId_PreCommitment_LockType)Convert.ToInt32(strResponse.Substring(48, 1));
                    monTgtLimitReached.DisplayTime = Convert.ToInt16(strResponse.Substring(49, 4));
                    monTgtLimitReached.DisplayInterval = Convert.ToInt16(strResponse.Substring(53, 4));
                }

                monTgtLimitReached.DisplayMessage = GetDisplayMessageForLimitReached(strResponse);
                monTgtLimitReached.DisplayMessageLength = monTgtLimitReached.DisplayMessage.Length;
                return monTgtLimitReached;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return new MonTgt_H2G_PC_LimitReachedNotificationMessage();
            }
        }

        /// <summary>
        /// To create Relaxed Limit Monitor message from response string
        /// </summary>
        /// <param name="strResponse"></param>
        /// <returns></returns>
        public MonTgt_H2G_PC_RelaxedLimitEffectiveNotificationMsg GetRelaxedLimitFreeFormMessage(string strResponse)
        {
            try
            {
                MonTgt_H2G_PC_RelaxedLimitEffectiveNotificationMsg monTgtRelaxedLimit = new MonTgt_H2G_PC_RelaxedLimitEffectiveNotificationMsg();

                if (HandlerHelper.Current.IsExtendedPlayer)
                {
                    monTgtRelaxedLimit.IsDayTimeBasisChanged = Convert.ToBoolean(strResponse.Substring(49, 1) == "Y" ? 1 : 0);
                    monTgtRelaxedLimit.DayNewTargetTime = new TimeSpan(0, Convert.ToInt32(strResponse.Substring(50, 2)), Convert.ToInt32(strResponse.Substring(52, 2)));
                    monTgtRelaxedLimit.DayOldTargetTime = new TimeSpan(0, Convert.ToInt32(strResponse.Substring(54, 2)), 0, Convert.ToInt32(strResponse.Substring(56, 2)));

                    monTgtRelaxedLimit.IsWeekTimeBasisChanged = Convert.ToBoolean(strResponse.Substring(58, 1) == "Y" ? 1 : 0);
                    monTgtRelaxedLimit.WeekNewTargetTime = Convert.ToByte(strResponse.Substring(59, 2));
                    monTgtRelaxedLimit.WeekOldTargetTime = Convert.ToByte(strResponse.Substring(61, 2));

                    monTgtRelaxedLimit.IsMonthTimeBasisChanged = Convert.ToBoolean(strResponse.Substring(63, 1) == "Y" ? 1 : 0);
                    monTgtRelaxedLimit.MonthNewTargetTime = Convert.ToByte(strResponse.Substring(64, 2));
                    monTgtRelaxedLimit.MonthOldTargetTime = Convert.ToByte(strResponse.Substring(66, 2));

                    monTgtRelaxedLimit.IsDayLossBasisChanged = Convert.ToBoolean(strResponse.Substring(68, 1) == "Y" ? 1 : 0);
                    monTgtRelaxedLimit.DayNewTargetLoss = Convert.ToInt32(strResponse.Substring(69, 9));
                    monTgtRelaxedLimit.DayOldTargetLoss = Convert.ToInt32(strResponse.Substring(78, 9));

                    monTgtRelaxedLimit.IsWeekLossBasisChanged = Convert.ToBoolean(strResponse.Substring(87, 1) == "Y" ? 1 : 0);
                    monTgtRelaxedLimit.WeekNewTargetLoss = Convert.ToInt32(strResponse.Substring(88, 9));
                    monTgtRelaxedLimit.WeekOldTargetLoss = Convert.ToInt32(strResponse.Substring(97, 9));

                    monTgtRelaxedLimit.IsMonthLossBasisChanged = Convert.ToBoolean(strResponse.Substring(106, 1) == "Y" ? 1 : 0);
                    monTgtRelaxedLimit.MonthNewTargetLoss = Convert.ToInt32(strResponse.Substring(107, 9));
                    monTgtRelaxedLimit.MonthOldTargetLoss = Convert.ToInt32(strResponse.Substring(116, 9));

                    monTgtRelaxedLimit.IsDayWagerBasisChanged = Convert.ToBoolean(strResponse.Substring(125, 1) == "Y" ? 1 : 0);
                    monTgtRelaxedLimit.DayNewTargetWager = Convert.ToInt32(strResponse.Substring(126, 9));
                    monTgtRelaxedLimit.DayOldTargetWager = Convert.ToInt32(strResponse.Substring(135, 9));

                    monTgtRelaxedLimit.IsWeekWagerBasisChanged = Convert.ToBoolean(strResponse.Substring(144, 1) == "Y" ? 1 : 0);
                    monTgtRelaxedLimit.WeekNewTargetWager = Convert.ToInt32(strResponse.Substring(145, 9));
                    monTgtRelaxedLimit.WeekOldTargetWager = Convert.ToInt32(strResponse.Substring(154, 9));

                    monTgtRelaxedLimit.IsMonthWagerBasisChanged = Convert.ToBoolean(strResponse.Substring(163, 1) == "Y" ? 1 : 0);
                    monTgtRelaxedLimit.MonthNewTargetWager = Convert.ToInt32(strResponse.Substring(164, 9));
                    monTgtRelaxedLimit.MonthOldTargetWager = Convert.ToInt32(strResponse.Substring(182, 9));

                    monTgtRelaxedLimit.IsConsecutiveDaysBasis = Convert.ToBoolean(strResponse.Substring(183, 1) == "Y" ? 1 : 0);
                    monTgtRelaxedLimit.NewTargetConsecutiveDays = Convert.ToByte(strResponse.Substring(185, 2));
                    monTgtRelaxedLimit.OldTargetConsecutiveDays = Convert.ToByte(strResponse.Substring(188, 2));
                }
                else
                {
                    monTgtRelaxedLimit.IsDayTimeBasisChanged = Convert.ToBoolean(strResponse.Substring(48, 1) == "Y" ? 1 : 0);
                    monTgtRelaxedLimit.DayNewTargetTime = new TimeSpan(0, Convert.ToInt32(strResponse.Substring(49, 2)), Convert.ToInt32(strResponse.Substring(51, 4)));
                    monTgtRelaxedLimit.DayOldTargetTime = new TimeSpan(0, Convert.ToInt32(strResponse.Substring(53, 2)), Convert.ToInt32(strResponse.Substring(55, 2)));

                    monTgtRelaxedLimit.IsWeekTimeBasisChanged = Convert.ToBoolean(strResponse.Substring(57, 1) == "Y" ? 1 : 0);
                    monTgtRelaxedLimit.WeekNewTargetTime = Convert.ToByte(strResponse.Substring(58, 2));
                    monTgtRelaxedLimit.WeekOldTargetTime = Convert.ToByte(strResponse.Substring(60, 2));

                    monTgtRelaxedLimit.IsMonthTimeBasisChanged = Convert.ToBoolean(strResponse.Substring(62, 1) == "Y" ? 1 : 0);
                    monTgtRelaxedLimit.MonthNewTargetTime = Convert.ToByte(strResponse.Substring(63, 2));
                    monTgtRelaxedLimit.MonthOldTargetTime = Convert.ToByte(strResponse.Substring(65, 2));

                    monTgtRelaxedLimit.IsDayLossBasisChanged = Convert.ToBoolean(strResponse.Substring(67, 1) == "Y" ? 1 : 0);
                    monTgtRelaxedLimit.DayNewTargetLoss = Convert.ToInt32(strResponse.Substring(68, 9));
                    monTgtRelaxedLimit.DayOldTargetLoss = Convert.ToInt32(strResponse.Substring(77, 9));

                    monTgtRelaxedLimit.IsWeekLossBasisChanged = Convert.ToBoolean(strResponse.Substring(86, 1) == "Y" ? 1 : 0);
                    monTgtRelaxedLimit.WeekNewTargetLoss = Convert.ToInt32(strResponse.Substring(87, 9));
                    monTgtRelaxedLimit.WeekOldTargetLoss = Convert.ToInt32(strResponse.Substring(96, 9));

                    monTgtRelaxedLimit.IsMonthLossBasisChanged = Convert.ToBoolean(strResponse.Substring(105, 1) == "Y" ? 1 : 0);
                    monTgtRelaxedLimit.MonthNewTargetLoss = Convert.ToInt32(strResponse.Substring(106, 9));
                    monTgtRelaxedLimit.MonthOldTargetLoss = Convert.ToInt32(strResponse.Substring(115, 9));

                    monTgtRelaxedLimit.IsDayWagerBasisChanged = Convert.ToBoolean(strResponse.Substring(124, 1) == "Y" ? 1 : 0);
                    monTgtRelaxedLimit.DayNewTargetWager = Convert.ToInt32(strResponse.Substring(125, 9));
                    monTgtRelaxedLimit.DayOldTargetWager = Convert.ToInt32(strResponse.Substring(134, 9));

                    monTgtRelaxedLimit.IsWeekWagerBasisChanged = Convert.ToBoolean(strResponse.Substring(143, 1) == "Y" ? 1 : 0);
                    monTgtRelaxedLimit.WeekNewTargetWager = Convert.ToInt32(strResponse.Substring(144, 9));
                    monTgtRelaxedLimit.WeekOldTargetWager = Convert.ToInt32(strResponse.Substring(153, 9));

                    monTgtRelaxedLimit.IsMonthWagerBasisChanged = Convert.ToBoolean(strResponse.Substring(162, 1) == "Y" ? 1 : 0);
                    monTgtRelaxedLimit.MonthNewTargetWager = Convert.ToInt32(strResponse.Substring(163, 9));
                    monTgtRelaxedLimit.MonthOldTargetWager = Convert.ToInt32(strResponse.Substring(172, 9));

                    monTgtRelaxedLimit.IsConsecutiveDaysBasis = Convert.ToBoolean(strResponse.Substring(181, 1) == "Y" ? 1 : 0);
                    monTgtRelaxedLimit.NewTargetConsecutiveDays = Convert.ToByte(strResponse.Substring(182, 2));
                    monTgtRelaxedLimit.OldTargetConsecutiveDays = Convert.ToByte(strResponse.Substring(184, 2));
                }
                return monTgtRelaxedLimit;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return new MonTgt_H2G_PC_RelaxedLimitEffectiveNotificationMsg();
            }
        }

        /// <summary>
        /// To create Card In PC Response Monitor message from response string
        /// </summary>
        /// <param name="strResponse"></param>
        /// <returns></returns>
        public MonTgt_H2G_PC_StatusResponsePlayerCardIn GetCardInFreeFormMessage(string strResponse)
        {
            try
            {
                MonTgt_H2G_PC_StatusResponsePlayerCardIn montgtCardInResponse = new MonTgt_H2G_PC_StatusResponsePlayerCardIn();

                montgtCardInResponse.PCEnrolled = GetPCEnrolled(strResponse);
                montgtCardInResponse.HandlePulls = Convert.ToInt16(GetHandlePullsForCardIn(strResponse));
                montgtCardInResponse.RatingInterval = Convert.ToInt16(GetRatingIntervalForCardIn(strResponse));

                return montgtCardInResponse;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return new MonTgt_H2G_PC_StatusResponsePlayerCardIn();
            }
        }

        #endregion //Monitor Entity Creation

        #endregion //Public Memebers

        #region Private Members

        public string GetCardNumber(string strMsg)
        {
            return HandlerHelper.Current.IsExtendedPlayer ? strMsg.Substring(6, 10) : strMsg.Substring(4, 10);
        }

        public string GetMessageType(string strMsg)
        {
            return strMsg.Substring(0, 2);
        }

        public string GetTransactionCode(string strMsg)
        {
            return strMsg.Substring(2, 2);
        }

        public string GetSlotNumber(string strMsg)
        {
            return HandlerHelper.Current.IsExtendedPlayer ? strMsg.Substring(16, 10) : strMsg.Substring(14, 10);
        }

        public string GetStand(string strMsg)
        {
            return HandlerHelper.Current.IsExtendedPlayer ? strMsg.Substring(26, 10) : strMsg.Substring(24, 10);
        }

        private string GetHandlePullsForCardIn(string strMsg)
        {
            return HandlerHelper.Current.IsExtendedPlayer ? strMsg.Substring(37, 4) : strMsg.Substring(35, 4);
        }

        private string GetRatingIntervalForCardIn(string strMsg)
        {
            return HandlerHelper.Current.IsExtendedPlayer ? ConvertMinutesToSeconds(strMsg.Substring(41, 4)) : ConvertMinutesToSeconds(strMsg.Substring(39, 4));
        }

        private bool GetPCEnrolled(string strMsg)
        {
            return ((HandlerHelper.Current.IsExtendedPlayer ? strMsg.Substring(36, 1) : strMsg.Substring(34, 1)) == "Y" ? true : false);
        }

        private string ConvertMinutesToSeconds(string strRatingInsterval)
        {
            if (strRatingInsterval.Length < 4) return string.Empty;

            return (Convert.ToInt16(strRatingInsterval.Substring(0, 2)) * 60 + Convert.ToInt16(strRatingInsterval.Substring(2, 2))).ToString();
        }

        private string GetHandlePullsForApproachLimit(string strMsg)
        {
            return HandlerHelper.Current.IsExtendedPlayer ? strMsg.Substring(196, 4) : strMsg.Substring(194, 4);
        }

        private string GetRatingIntervalForApproachLimit(string strMsg)
        {
            return HandlerHelper.Current.IsExtendedPlayer ? strMsg.Substring(200, 4) : strMsg.Substring(198, 4);
        }

        private string GetDisplayMessageForApproachLimit(string strMsg)
        {
            return HandlerHelper.Current.IsExtendedPlayer ? strMsg.Substring(204) : strMsg.Substring(202);
        }

        private string GetDisplayMessageForLimitReached(string strMsg)
        {
            return HandlerHelper.Current.IsExtendedPlayer ? strMsg.Substring(197) : strMsg.Substring(195);
        }

        private char GetLockTypeForLimitReached(string strMsg)
        {
            return HandlerHelper.Current.IsExtendedPlayer ? Convert.ToChar(strMsg.Substring(50, 1)) : Convert.ToChar(strMsg.Substring(48, 1));
        }

        #endregion //Private Members
    }
}
