using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using BMC.Common.ExceptionManagement;
using DataXChangeEndPointService.Data;
using FreeForm;
using BMC.PlayerGateway.Gateway;
using BMC.Common.LogManagement;
using System.Configuration;

namespace DataXChangeEndPointService.Business
{
    public class ResponseBusiness
    {
        private static ResponseBusiness _ResponseBusiness = null;
        private ResponseDataAccess objResponseDataAccess = new ResponseDataAccess();

        private ResponseBusiness() 
        {
        }

        public static ResponseBusiness ResponseBusinessInstance
        {
            get
            {
                if (_ResponseBusiness == null)
                    _ResponseBusiness = new ResponseBusiness();

                return _ResponseBusiness;
            }
        }

        public bool InsertApproachLimitNotification(string strResponse)
        {
            try
            {
                objResponseDataAccess.InsertPCMessages(
                                                                GetCardNumber(strResponse), //Card Number
                                                                GetSlotNumber(strResponse), //Slot Number 
                                                                GetStand(strResponse), //Stand
                                                                Convert.ToInt32(GetHandlePullsForApproachLimit(strResponse)), //Handle Pulls
                                                                GetRatingIntervalForApproachLimit(strResponse), //Rating Interval
                                                                strResponse, // Message
                                                                null,
                                                                "A", //Ack Type
                                                                ResponseDataAccess.BreakPeriodInterval.ToString(), //Break Period
                                                                true //PC Enrolled
                                                                );
                return true;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return false;
        }

        public bool InsertLimitReachedNotification(string strResponse)
        {
            try
            {
                objResponseDataAccess.InsertPCMessages(
                                                                GetCardNumber(strResponse), //Card Number
                                                                GetSlotNumber(strResponse), //Slot Number 
                                                                GetStand(strResponse), //Stand
                                                                null, //Handle Pulls
                                                                null, //Rating Interval
                                                                strResponse, // Message
                                                                GetLockTypeForLimitReached(strResponse), //LockType
                                                                "L", //Ack Type
                                                                ResponseDataAccess.BreakPeriodInterval.ToString(), //Break Period
                                                                true //PC Enrolled
                                                                );
                return true;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return false;
        }

        public bool InsertCardInResponseFromPC(string strResponse)
        {
            try
            {
                objResponseDataAccess.InsertCardInResponseFromPC(
                                                                GetCardNumber(strResponse), //Card Number
                                                                GetMessageType(strResponse), //Message Type
                                                                GetTransactionCode(strResponse), //Transaction Code
                                                                GetSlotNumber(strResponse), //Slot Number 
                                                                GetStand(strResponse), //Stand
                                                                strResponse, //Actual Response
                                                                DateTime.Now,
                                                                Convert.ToInt32(GetHandlePullsForCardIn(strResponse)), //Handle Pulls
                                                                GetRatingIntervalForCardIn(strResponse), //Rating Interval
                                                                ResponseDataAccess.BreakPeriodInterval.ToString(),//strResponse.Substring(45, 3), //Break Period
                                                                GetPCEnrolled(strResponse) //PC Enrolled
                                                                );
                return true;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return false;
        }

        public bool InsertRelaxedLimitdNotification(string strResponse)
        {
            try
            {
                objResponseDataAccess.InsertPCMessages(
                                                                    GetCardNumber(strResponse), //Card Number
                                                                    GetSlotNumber(strResponse), //Slot Number 
                                                                    GetStand(strResponse), //Stand
                                                                    null, //Handle Pulls
                                                                    null, //Rating Interval
                                                                    strResponse, // Message
                                                                    null, //LockType
                                                                    "R", //Ack Type
                                                                    ResponseDataAccess.BreakPeriodInterval.ToString(), //Break Period
                                                                    true //PC Enrolled
                                                                    );
                return true;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return false;
        }

        public DataSet GetDataForSendToComms(ref int iRecordsToProcess)
        {
            return objResponseDataAccess.GetDataForSendToComms(MessageHandler.iMaxRows, ref iRecordsToProcess);
        }

        public bool UpdateSentFreeFormMsgToCommsStatus(int iPC_ST_ID, bool bStatus)
        {
            try
            {
                objResponseDataAccess.UpdateSentFreeFormMsgToCommsStatus(iPC_ST_ID, bStatus);
                return true;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return false;
        }

        public int GetInstallationByBarPosition(string strResponse)
        {
            return ResponseDataAccess.IsExtendedPlayer ? objResponseDataAccess.GetInstallationByBarPosition(strResponse.Substring(26, 10))
                                                    : objResponseDataAccess.GetInstallationByBarPosition(strResponse.Substring(24, 10));
        }

        public byte[] GetApproachLimitFreeFormMessage(string strResponse)
        {
            try
            {
                FreeFormMsg objFreeFormMsg = new FreeFormMsg();
                PCApproachNotification objPCApproachNotification = new PCApproachNotification();

                objPCApproachNotification.MessageType = GetMessageType(strResponse);
                objPCApproachNotification.TransactionCode = GetTransactionCode(strResponse);
                objPCApproachNotification.CardLength = ResponseDataAccess.IsExtendedPlayer ? Convert.ToInt32(strResponse.Substring(4, 2)) : 0;
                objPCApproachNotification.CardNo = GetCardNumber(strResponse);
                objPCApproachNotification.SlotNo = GetSlotNumber(strResponse);
                objPCApproachNotification.Stand = GetStand(strResponse);

                if (ResponseDataAccess.IsExtendedPlayer)
                {
                    objPCApproachNotification.EventDate = DateTime.ParseExact(strResponse.Substring(36, 8), "yyyyMMdd", null);
                    string Time = strResponse.Substring(44, 6);
                    objPCApproachNotification.EventTime = new TimeSpan(Convert.ToInt32(Time.Substring(0, 2)), Convert.ToInt32(Time.Substring(2, 2)), 0);

                    objPCApproachNotification.DisplayTime = strResponse.Substring(50, 4);

                    objPCApproachNotification.DisplayInterval = (Convert.ToInt32(strResponse.Substring(54, 2)) * 60 + Convert.ToInt32(strResponse.Substring(56, 2))).ToString();

                    objPCApproachNotification.IsDayTimeBasis = Convert.ToBoolean(strResponse.Substring(58, 1) == "Y" ? 1 : 0);
                    objPCApproachNotification.DayTargetTime = strResponse.Substring(59, 4);
                    objPCApproachNotification.CurrentDayTimeValue = strResponse.Substring(63, 4);

                    objPCApproachNotification.IsWeekTimeBasis = Convert.ToBoolean(strResponse.Substring(67, 1) == "Y" ? 1 : 0);
                    objPCApproachNotification.WeekTargetTime = strResponse.Substring(68, 2);
                    objPCApproachNotification.CurrentWeekTimeValue = strResponse.Substring(70, 2);

                    objPCApproachNotification.IsMonthTimeBasis = Convert.ToBoolean(strResponse.Substring(72, 1) == "Y" ? 1 : 0);
                    objPCApproachNotification.MonthTargetTime = strResponse.Substring(73, 2);
                    objPCApproachNotification.CurrentMonthTimeValue = strResponse.Substring(75, 2);

                    objPCApproachNotification.IsDayLossBasis = Convert.ToBoolean(strResponse.Substring(77, 1) == "Y" ? 1 : 0);
                    objPCApproachNotification.DayTargetLoss = Convert.ToInt32(strResponse.Substring(78, 9));
                    objPCApproachNotification.CurrentDayLossValue = Convert.ToInt32(strResponse.Substring(87, 9));

                    objPCApproachNotification.IsWeekLossBasis = Convert.ToBoolean(strResponse.Substring(96, 1) == "Y" ? 1 : 0);
                    objPCApproachNotification.WeekTargetLoss = Convert.ToInt32(strResponse.Substring(97, 9));
                    objPCApproachNotification.CurrentWeekLossValue = Convert.ToInt32(strResponse.Substring(106, 9));

                    objPCApproachNotification.IsMonthLossBasis = Convert.ToBoolean(strResponse.Substring(115, 1) == "Y" ? 1 : 0);
                    objPCApproachNotification.MonthTargetLoss = Convert.ToInt32(strResponse.Substring(116, 9));
                    objPCApproachNotification.CurrentMonthLossValue = Convert.ToInt32(strResponse.Substring(125, 9));

                    objPCApproachNotification.IsDayWagerBasis = Convert.ToBoolean(strResponse.Substring(134, 1) == "Y" ? 1 : 0);
                    objPCApproachNotification.DayTargetWagers = Convert.ToInt32(strResponse.Substring(135, 9));
                    objPCApproachNotification.CurrentDayWagerValue = Convert.ToInt32(strResponse.Substring(144, 9));

                    objPCApproachNotification.IsWeekWagerBasis = Convert.ToBoolean(strResponse.Substring(153, 1) == "Y" ? 1 : 0);
                    objPCApproachNotification.WeekTargetWagers = Convert.ToInt32(strResponse.Substring(154, 9));
                    objPCApproachNotification.CurrentWeekWagerValue = Convert.ToInt32(strResponse.Substring(163, 9));

                    objPCApproachNotification.IsMonthWagerBasis = Convert.ToBoolean(strResponse.Substring(172, 1) == "Y" ? 1 : 0);
                    objPCApproachNotification.MonthTargetWagers = Convert.ToInt32(strResponse.Substring(173, 9));
                    objPCApproachNotification.CurrentMonthWagerValue = Convert.ToInt32(strResponse.Substring(182, 9));

                    objPCApproachNotification.IsConsecutiveDaysBasis = Convert.ToBoolean(strResponse.Substring(191, 1) == "Y" ? 1 : 0);
                    objPCApproachNotification.TargetConsecutiveDays = Convert.ToInt32(strResponse.Substring(192, 2));
                    objPCApproachNotification.CurrentConsecutiveDays = Convert.ToInt32(strResponse.Substring(194, 2));

                    objPCApproachNotification.HandlePulls = strResponse.Substring(196, 4);
                    objPCApproachNotification.RatingInterval = (Convert.ToInt32(strResponse.Substring(200, 2)) * 60 + Convert.ToInt32(strResponse.Substring(202, 2))).ToString();
                    objPCApproachNotification.DisplayMessage = strResponse.Substring(204).Trim();
                }
                else
                {
                    objPCApproachNotification.EventDate = DateTime.ParseExact(strResponse.Substring(34, 8), "yyyyMMdd", null);
                    string Time = strResponse.Substring(42, 6);

                    objPCApproachNotification.EventTime = new TimeSpan(Convert.ToInt32(Time.Substring(0, 2)), Convert.ToInt32(Time.Substring(2, 2)), 0);

                    objPCApproachNotification.DisplayTime = (Convert.ToInt32(strResponse.Substring(48, 2)) * 60 + Convert.ToInt32(strResponse.Substring(50, 2))).ToString();

                    objPCApproachNotification.DisplayInterval = (Convert.ToInt32(strResponse.Substring(52, 2)) * 60 + Convert.ToInt32(strResponse.Substring(54, 2))).ToString();



                    objPCApproachNotification.IsDayTimeBasis = Convert.ToBoolean(strResponse.Substring(56, 1) == "Y" ? 1 : 0);
                    objPCApproachNotification.DayTargetTime = strResponse.Substring(57, 4);
                    objPCApproachNotification.CurrentDayTimeValue = strResponse.Substring(61, 4);

                    objPCApproachNotification.IsWeekTimeBasis = Convert.ToBoolean(strResponse.Substring(65, 1) == "Y" ? 1 : 0);
                    objPCApproachNotification.WeekTargetTime = strResponse.Substring(66, 2);
                    objPCApproachNotification.CurrentWeekTimeValue = strResponse.Substring(68, 2);

                    objPCApproachNotification.IsMonthTimeBasis = Convert.ToBoolean(strResponse.Substring(70, 1) == "Y" ? 1 : 0);
                    objPCApproachNotification.MonthTargetTime = strResponse.Substring(71, 2);
                    objPCApproachNotification.CurrentMonthTimeValue = strResponse.Substring(73, 2);

                    objPCApproachNotification.IsDayLossBasis = Convert.ToBoolean(strResponse.Substring(75, 1) == "Y" ? 1 : 0);
                    objPCApproachNotification.DayTargetLoss = Convert.ToInt32(strResponse.Substring(76, 9));
                    objPCApproachNotification.CurrentDayLossValue = Convert.ToInt32(strResponse.Substring(85, 9));

                    objPCApproachNotification.IsWeekLossBasis = Convert.ToBoolean(strResponse.Substring(94, 1) == "Y" ? 1 : 0);
                    objPCApproachNotification.WeekTargetLoss = Convert.ToInt32(strResponse.Substring(95, 9));
                    objPCApproachNotification.CurrentWeekLossValue = Convert.ToInt32(strResponse.Substring(104, 9));

                    objPCApproachNotification.IsMonthLossBasis = Convert.ToBoolean(strResponse.Substring(113, 1) == "Y" ? 1 : 0);
                    objPCApproachNotification.MonthTargetLoss = Convert.ToInt32(strResponse.Substring(114, 9));
                    objPCApproachNotification.CurrentMonthLossValue = Convert.ToInt32(strResponse.Substring(123, 9));

                    objPCApproachNotification.IsDayWagerBasis = Convert.ToBoolean(strResponse.Substring(132, 1) == "Y" ? 1 : 0);
                    objPCApproachNotification.DayTargetWagers = Convert.ToInt32(strResponse.Substring(134, 9));
                    objPCApproachNotification.CurrentDayWagerValue = Convert.ToInt32(strResponse.Substring(142, 9));

                    objPCApproachNotification.IsWeekWagerBasis = Convert.ToBoolean(strResponse.Substring(151, 1) == "Y" ? 1 : 0);
                    objPCApproachNotification.WeekTargetWagers = Convert.ToInt32(strResponse.Substring(152, 9));
                    objPCApproachNotification.CurrentWeekWagerValue = Convert.ToInt32(strResponse.Substring(161, 9));

                    objPCApproachNotification.IsMonthWagerBasis = Convert.ToBoolean(strResponse.Substring(170, 1) == "Y" ? 1 : 0);
                    objPCApproachNotification.MonthTargetWagers = Convert.ToInt32(strResponse.Substring(171, 9));
                    objPCApproachNotification.CurrentMonthWagerValue = Convert.ToInt32(strResponse.Substring(180, 9));

                    objPCApproachNotification.IsConsecutiveDaysBasis = Convert.ToBoolean(strResponse.Substring(189, 1) == "Y" ? 1 : 0);
                    objPCApproachNotification.TargetConsecutiveDays = Convert.ToInt32(strResponse.Substring(190, 2));
                    objPCApproachNotification.CurrentConsecutiveDays = Convert.ToInt32(strResponse.Substring(192, 2));

                    objPCApproachNotification.HandlePulls = strResponse.Substring(194, 4);
                    objPCApproachNotification.RatingInterval = (Convert.ToInt32(strResponse.Substring(198, 2)) * 60 + Convert.ToInt32(strResponse.Substring(200, 2))).ToString();
                    objPCApproachNotification.DisplayMessage = strResponse.Substring(202).Trim();
                }

                return objFreeFormMsg.GetApproachingLimitResponse(objPCApproachNotification);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return new byte[] { };
            }
        }

        public byte[] GetLimitReachedFreeFormMessage(string strResponse)
        {
            try
            {
                FreeFormMsg objFreeFormMsg = new FreeFormMsg();
                PCLimitReachedNotification objPCLimitReachedNotification = new PCLimitReachedNotification();

                objPCLimitReachedNotification.MessageType = GetMessageType(strResponse);
                objPCLimitReachedNotification.TransactionCode = GetTransactionCode(strResponse);
                objPCLimitReachedNotification.CardLength = ResponseDataAccess.IsExtendedPlayer ? Convert.ToInt32(strResponse.Substring(4, 2)) : 0;
                objPCLimitReachedNotification.CardNo = GetCardNumber(strResponse);
                objPCLimitReachedNotification.SlotNo = GetSlotNumber(strResponse);
                objPCLimitReachedNotification.Stand = GetStand(strResponse);

                if (ResponseDataAccess.IsExtendedPlayer)
                {
                    objPCLimitReachedNotification.LockType = strResponse.Substring(50, 1);
                    objPCLimitReachedNotification.DisplayTime = (Convert.ToInt32(strResponse.Substring(51, 2)) * 60 + Convert.ToInt32(strResponse.Substring(53, 2))).ToString();
                    objPCLimitReachedNotification.DisplayInterval = (Convert.ToInt32(strResponse.Substring(55, 2)) * 60 + Convert.ToInt32(strResponse.Substring(55, 2))).ToString();

                    objPCLimitReachedNotification.IsDayTimeBasis = Convert.ToBoolean(strResponse.Substring(59, 1) == "Y" ? 1 : 0);
                    objPCLimitReachedNotification.DayTargetTime = strResponse.Substring(60, 4);
                    objPCLimitReachedNotification.CurrentDayTimeValue = strResponse.Substring(64, 4);

                    objPCLimitReachedNotification.IsWeekTimeBasis = Convert.ToBoolean(strResponse.Substring(68, 1) == "Y" ? 1 : 0);
                    objPCLimitReachedNotification.WeekTargetTime = strResponse.Substring(69, 2);
                    objPCLimitReachedNotification.CurrentWeekTimeValue = strResponse.Substring(71, 2);

                    objPCLimitReachedNotification.IsMonthTimeBasis = Convert.ToBoolean(strResponse.Substring(73, 1) == "Y" ? 1 : 0);
                    objPCLimitReachedNotification.MonthTargetTime = strResponse.Substring(74, 2);
                    objPCLimitReachedNotification.CurrentMonthTimeValue = strResponse.Substring(76, 2);

                    objPCLimitReachedNotification.IsDayLossBasis = Convert.ToBoolean(strResponse.Substring(78, 1) == "Y" ? 1 : 0);
                    objPCLimitReachedNotification.DayTargetLoss = Convert.ToInt32(strResponse.Substring(79, 9));
                    objPCLimitReachedNotification.CurrentDayLossValue = Convert.ToInt32(strResponse.Substring(88, 9));

                    objPCLimitReachedNotification.IsWeekLossBasis = Convert.ToBoolean(strResponse.Substring(97, 1) == "Y" ? 1 : 0);
                    objPCLimitReachedNotification.WeekTargetLoss = Convert.ToInt32(strResponse.Substring(98, 9));
                    objPCLimitReachedNotification.CurrentWeekLossValue = Convert.ToInt32(strResponse.Substring(107, 9));

                    objPCLimitReachedNotification.IsMonthLossBasis = Convert.ToBoolean(strResponse.Substring(116, 1) == "Y" ? 1 : 0);
                    objPCLimitReachedNotification.MonthTargetLoss = Convert.ToInt32(strResponse.Substring(117, 9));
                    objPCLimitReachedNotification.CurrentMonthLossValue = Convert.ToInt32(strResponse.Substring(126, 9));

                    objPCLimitReachedNotification.IsDayWagerBasis = Convert.ToBoolean(strResponse.Substring(135, 1) == "Y" ? 1 : 0);
                    objPCLimitReachedNotification.DayTargetWagers = Convert.ToInt32(strResponse.Substring(136, 9));
                    objPCLimitReachedNotification.CurrentDayWagerValue = Convert.ToInt32(strResponse.Substring(145, 9));

                    objPCLimitReachedNotification.IsWeekWagerBasis = Convert.ToBoolean(strResponse.Substring(154, 1) == "Y" ? 1 : 0);
                    objPCLimitReachedNotification.WeekTargetWagers = Convert.ToInt32(strResponse.Substring(155, 9));
                    objPCLimitReachedNotification.CurrentWeekWagerValue = Convert.ToInt32(strResponse.Substring(164, 9));

                    objPCLimitReachedNotification.IsMonthWagerBasis = Convert.ToBoolean(strResponse.Substring(173, 1) == "Y" ? 1 : 0);
                    objPCLimitReachedNotification.MonthTargetWagers = Convert.ToInt32(strResponse.Substring(175, 9));
                    objPCLimitReachedNotification.CurrentMonthWagerValue = Convert.ToInt32(strResponse.Substring(183, 9));

                    objPCLimitReachedNotification.IsConsecutiveDaysBasis = Convert.ToBoolean(strResponse.Substring(192, 1) == "Y" ? 1 : 0);
                    objPCLimitReachedNotification.TargetConsecutiveDays = Convert.ToInt32(strResponse.Substring(193, 2));
                    objPCLimitReachedNotification.CurrentConsecutiveDays = Convert.ToInt32(strResponse.Substring(195, 2));
                    objPCLimitReachedNotification.DisplayMessage = strResponse.Substring(197).Trim();
                }
                else
                {
                    objPCLimitReachedNotification.LockType = strResponse.Substring(48, 1);
                    objPCLimitReachedNotification.DisplayTime = strResponse.Substring(49, 4);
                    objPCLimitReachedNotification.DisplayInterval = strResponse.Substring(53, 4);

                    objPCLimitReachedNotification.IsDayTimeBasis = Convert.ToBoolean(strResponse.Substring(57, 1) == "Y" ? 1 : 0); ;
                    objPCLimitReachedNotification.DayTargetTime = strResponse.Substring(58, 4);
                    objPCLimitReachedNotification.CurrentDayTimeValue = strResponse.Substring(62, 4);

                    objPCLimitReachedNotification.IsWeekTimeBasis = Convert.ToBoolean(strResponse.Substring(66, 1) == "Y" ? 1 : 0);
                    objPCLimitReachedNotification.WeekTargetTime = strResponse.Substring(67, 2);
                    objPCLimitReachedNotification.CurrentWeekTimeValue = strResponse.Substring(69, 2);

                    objPCLimitReachedNotification.IsMonthTimeBasis = Convert.ToBoolean(strResponse.Substring(71, 1) == "Y" ? 1 : 0);
                    objPCLimitReachedNotification.MonthTargetTime = strResponse.Substring(72, 2);
                    objPCLimitReachedNotification.CurrentMonthTimeValue = strResponse.Substring(74, 2);

                    objPCLimitReachedNotification.IsDayLossBasis = Convert.ToBoolean(strResponse.Substring(76, 1) == "Y" ? 1 : 0);
                    objPCLimitReachedNotification.DayTargetLoss = Convert.ToInt32(strResponse.Substring(77, 9));
                    objPCLimitReachedNotification.CurrentDayLossValue = Convert.ToInt32(strResponse.Substring(86, 9) == "Y" ? 1 : 0);

                    objPCLimitReachedNotification.IsWeekLossBasis = Convert.ToBoolean(strResponse.Substring(95, 1) == "Y" ? 1 : 0);
                    objPCLimitReachedNotification.WeekTargetLoss = Convert.ToInt32(strResponse.Substring(96, 9));
                    objPCLimitReachedNotification.CurrentWeekLossValue = Convert.ToInt32(strResponse.Substring(105, 9));

                    objPCLimitReachedNotification.IsMonthLossBasis = Convert.ToBoolean(strResponse.Substring(114, 1) == "Y" ? 1 : 0);
                    objPCLimitReachedNotification.MonthTargetLoss = Convert.ToInt32(strResponse.Substring(115, 9));
                    objPCLimitReachedNotification.CurrentMonthLossValue = Convert.ToInt32(strResponse.Substring(124, 9));

                    objPCLimitReachedNotification.IsDayWagerBasis = Convert.ToBoolean(strResponse.Substring(133, 1) == "Y" ? 1 : 0);
                    objPCLimitReachedNotification.DayTargetWagers = Convert.ToInt32(strResponse.Substring(134, 9) == "Y" ? 1 : 0);
                    objPCLimitReachedNotification.CurrentDayWagerValue = Convert.ToInt32(strResponse.Substring(143, 9) == "Y" ? 1 : 0);

                    objPCLimitReachedNotification.IsWeekWagerBasis = Convert.ToBoolean(strResponse.Substring(152, 1) == "Y" ? 1 : 0);
                    objPCLimitReachedNotification.WeekTargetWagers = Convert.ToInt32(strResponse.Substring(153, 9));
                    objPCLimitReachedNotification.CurrentWeekWagerValue = Convert.ToInt32(strResponse.Substring(162, 9));

                    objPCLimitReachedNotification.IsMonthWagerBasis = Convert.ToBoolean(strResponse.Substring(171, 1) == "Y" ? 1 : 0);
                    objPCLimitReachedNotification.MonthTargetWagers = Convert.ToInt32(strResponse.Substring(173, 9));
                    objPCLimitReachedNotification.CurrentMonthWagerValue = Convert.ToInt32(strResponse.Substring(181, 9));

                    objPCLimitReachedNotification.IsConsecutiveDaysBasis = Convert.ToBoolean(strResponse.Substring(190, 1) == "Y" ? 1 : 0);
                    objPCLimitReachedNotification.TargetConsecutiveDays = Convert.ToInt32(strResponse.Substring(191, 2));
                    objPCLimitReachedNotification.CurrentConsecutiveDays = Convert.ToInt32(strResponse.Substring(193, 2));
                    objPCLimitReachedNotification.DisplayMessage = strResponse.Substring(195).Trim();
                }

                return objFreeFormMsg.GetLimitReachedResponse(objPCLimitReachedNotification);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return new byte[] { };
            }
        }

        public byte[] GetRelaxedLimitFreeFormMessage(string strResponse)
        {
            try
            {
                FreeFormMsg objFreeFormMsg = new FreeFormMsg();
                PCRelaxedLimitNotification objPCRelaxedLimitNotification = new PCRelaxedLimitNotification();

                objPCRelaxedLimitNotification.MessageType = GetMessageType(strResponse);
                objPCRelaxedLimitNotification.TransactionCode = GetTransactionCode(strResponse);
                objPCRelaxedLimitNotification.CardLength = ResponseDataAccess.IsExtendedPlayer ? Convert.ToInt32(strResponse.Substring(4, 2)) : 0;
                objPCRelaxedLimitNotification.CardNo = GetCardNumber(strResponse);
                objPCRelaxedLimitNotification.SlotNo = GetSlotNumber(strResponse);
                objPCRelaxedLimitNotification.Stand = GetStand(strResponse);

                if (ResponseDataAccess.IsExtendedPlayer)
                {
                    objPCRelaxedLimitNotification.IsDayTimeBasisChanged = Convert.ToBoolean(strResponse.Substring(49, 1) == "Y" ? 1 : 0);
                    objPCRelaxedLimitNotification.DayNewTargetTime = strResponse.Substring(50, 4);
                    objPCRelaxedLimitNotification.DayOldTargetTime = strResponse.Substring(54, 4);

                    objPCRelaxedLimitNotification.IsWeekTimeBasisChanged = Convert.ToBoolean(strResponse.Substring(58, 1) == "Y" ? 1 : 0);
                    objPCRelaxedLimitNotification.WeekNewTargetTime = strResponse.Substring(59, 2);
                    objPCRelaxedLimitNotification.WeekOldTargetTime = strResponse.Substring(61, 2);

                    objPCRelaxedLimitNotification.IsMonthTimeBasisChanged = Convert.ToBoolean(strResponse.Substring(63, 1) == "Y" ? 1 : 0);
                    objPCRelaxedLimitNotification.MonthNewTargetTime = strResponse.Substring(64, 2);
                    objPCRelaxedLimitNotification.MonthOldTargetTime = strResponse.Substring(66, 2);

                    objPCRelaxedLimitNotification.IsDayLossBasisChanged = Convert.ToBoolean(strResponse.Substring(68, 1) == "Y" ? 1 : 0);
                    objPCRelaxedLimitNotification.DayNewTargetLoss = Convert.ToInt32(strResponse.Substring(69, 9));
                    objPCRelaxedLimitNotification.DayOldTargetLoss = Convert.ToInt32(strResponse.Substring(78, 9));

                    objPCRelaxedLimitNotification.IsWeekLossBasisChanged = Convert.ToBoolean(strResponse.Substring(87, 1) == "Y" ? 1 : 0);
                    objPCRelaxedLimitNotification.WeekNewTargetLoss = Convert.ToInt32(strResponse.Substring(88, 9));
                    objPCRelaxedLimitNotification.WeekOldTargetLoss = Convert.ToInt32(strResponse.Substring(97, 9));

                    objPCRelaxedLimitNotification.IsMonthLossBasisChanged = Convert.ToBoolean(strResponse.Substring(106, 1) == "Y" ? 1 : 0);
                    objPCRelaxedLimitNotification.MonthNewTargetLoss = Convert.ToInt32(strResponse.Substring(107, 9));
                    objPCRelaxedLimitNotification.MonthOldTargetLoss = Convert.ToInt32(strResponse.Substring(116, 9));

                    objPCRelaxedLimitNotification.IsDayWagerBasisChanged = Convert.ToBoolean(strResponse.Substring(125, 1) == "Y" ? 1 : 0);
                    objPCRelaxedLimitNotification.DayNewTargetWager = Convert.ToInt32(strResponse.Substring(126, 9));
                    objPCRelaxedLimitNotification.DayOldTargetWager = Convert.ToInt32(strResponse.Substring(135, 9));

                    objPCRelaxedLimitNotification.IsWeekWagerBasisChanged = Convert.ToBoolean(strResponse.Substring(144, 1) == "Y" ? 1 : 0);
                    objPCRelaxedLimitNotification.WeekNewTargetWager = Convert.ToInt32(strResponse.Substring(145, 9));
                    objPCRelaxedLimitNotification.WeekOldTargetWager = Convert.ToInt32(strResponse.Substring(154, 9));

                    objPCRelaxedLimitNotification.IsMonthWagerBasisChanged = Convert.ToBoolean(strResponse.Substring(163, 1) == "Y" ? 1 : 0);
                    objPCRelaxedLimitNotification.MonthNewTargetWager = Convert.ToInt32(strResponse.Substring(164, 9));
                    objPCRelaxedLimitNotification.MonthOldTargetWager = Convert.ToInt32(strResponse.Substring(182, 9));

                    objPCRelaxedLimitNotification.IsConsecutiveDaysChanged = Convert.ToBoolean(strResponse.Substring(183, 1) == "Y" ? 1 : 0);
                    objPCRelaxedLimitNotification.NewConsecutiveDays = Convert.ToInt32(strResponse.Substring(185, 2));
                    objPCRelaxedLimitNotification.OldConsecutiveDays = Convert.ToInt32(strResponse.Substring(188, 2));
                }
                else
                {
                    objPCRelaxedLimitNotification.IsDayTimeBasisChanged = Convert.ToBoolean(strResponse.Substring(48, 1) == "Y" ? 1 : 0);
                    objPCRelaxedLimitNotification.DayNewTargetTime = strResponse.Substring(49, 4);
                    objPCRelaxedLimitNotification.DayOldTargetTime = strResponse.Substring(53, 4);

                    objPCRelaxedLimitNotification.IsWeekTimeBasisChanged = Convert.ToBoolean(strResponse.Substring(57, 1) == "Y" ? 1 : 0);
                    objPCRelaxedLimitNotification.WeekNewTargetTime = strResponse.Substring(58, 2);
                    objPCRelaxedLimitNotification.WeekOldTargetTime = strResponse.Substring(60, 2);

                    objPCRelaxedLimitNotification.IsMonthTimeBasisChanged = Convert.ToBoolean(strResponse.Substring(62, 1) == "Y" ? 1 : 0);
                    objPCRelaxedLimitNotification.MonthNewTargetTime = strResponse.Substring(63, 2);
                    objPCRelaxedLimitNotification.MonthOldTargetTime = strResponse.Substring(65, 2);

                    objPCRelaxedLimitNotification.IsDayLossBasisChanged = Convert.ToBoolean(strResponse.Substring(67, 1) == "Y" ? 1 : 0);
                    objPCRelaxedLimitNotification.DayNewTargetLoss = Convert.ToInt32(strResponse.Substring(68, 9));
                    objPCRelaxedLimitNotification.DayOldTargetLoss = Convert.ToInt32(strResponse.Substring(77, 9));

                    objPCRelaxedLimitNotification.IsWeekLossBasisChanged = Convert.ToBoolean(strResponse.Substring(86, 1) == "Y" ? 1 : 0);
                    objPCRelaxedLimitNotification.WeekNewTargetLoss = Convert.ToInt32(strResponse.Substring(87, 9));
                    objPCRelaxedLimitNotification.WeekOldTargetLoss = Convert.ToInt32(strResponse.Substring(96, 9));

                    objPCRelaxedLimitNotification.IsMonthLossBasisChanged = Convert.ToBoolean(strResponse.Substring(105, 1) == "Y" ? 1 : 0);
                    objPCRelaxedLimitNotification.MonthNewTargetLoss = Convert.ToInt32(strResponse.Substring(106, 9));
                    objPCRelaxedLimitNotification.MonthOldTargetLoss = Convert.ToInt32(strResponse.Substring(115, 9));

                    objPCRelaxedLimitNotification.IsDayWagerBasisChanged = Convert.ToBoolean(strResponse.Substring(124, 1) == "Y" ? 1 : 0);
                    objPCRelaxedLimitNotification.DayNewTargetWager = Convert.ToInt32(strResponse.Substring(125, 9));
                    objPCRelaxedLimitNotification.DayOldTargetWager = Convert.ToInt32(strResponse.Substring(134, 9));

                    objPCRelaxedLimitNotification.IsWeekWagerBasisChanged = Convert.ToBoolean(strResponse.Substring(143, 1) == "Y" ? 1 : 0);
                    objPCRelaxedLimitNotification.WeekNewTargetWager = Convert.ToInt32(strResponse.Substring(144, 9));
                    objPCRelaxedLimitNotification.WeekOldTargetWager = Convert.ToInt32(strResponse.Substring(153, 9));

                    objPCRelaxedLimitNotification.IsMonthWagerBasisChanged = Convert.ToBoolean(strResponse.Substring(162, 1) == "Y" ? 1 : 0);
                    objPCRelaxedLimitNotification.MonthNewTargetWager = Convert.ToInt32(strResponse.Substring(163, 9));
                    objPCRelaxedLimitNotification.MonthOldTargetWager = Convert.ToInt32(strResponse.Substring(172, 9));

                    objPCRelaxedLimitNotification.IsConsecutiveDaysChanged = Convert.ToBoolean(strResponse.Substring(181, 1) == "Y" ? 1 : 0);
                    objPCRelaxedLimitNotification.NewConsecutiveDays = Convert.ToInt32(strResponse.Substring(182, 2));
                    objPCRelaxedLimitNotification.OldConsecutiveDays = Convert.ToInt32(strResponse.Substring(184, 2));
                }
                return objFreeFormMsg.GetRelaxedLimitResponse(objPCRelaxedLimitNotification);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return new byte[] { };
            }
        }

        public byte[] GetCardInFreeFormMessage(string strResponse)
        {
            try
            {
                FreeFormMsg objFreeFormMsg = new FreeFormMsg();
                PCCardInResponse objPCCardInResponse = new PCCardInResponse();

                objPCCardInResponse.MessageType = GetMessageType(strResponse);
                objPCCardInResponse.TransactionCode = GetTransactionCode(strResponse);
                objPCCardInResponse.CardLength = ResponseDataAccess.IsExtendedPlayer ? Convert.ToInt32(strResponse.Substring(4, 2)) : 0;
                objPCCardInResponse.CardNo = GetCardNumber(strResponse);
                objPCCardInResponse.SlotNo = GetSlotNumber(strResponse);
                objPCCardInResponse.Stand = GetStand(strResponse);

                objPCCardInResponse.PCEnrolled = GetPCEnrolled(strResponse);
                objPCCardInResponse.HandlePulls = GetHandlePullsForCardIn(strResponse);
                objPCCardInResponse.RatingInterval = GetRatingIntervalForCardIn(strResponse);
                objPCCardInResponse.BreakPeriod = ResponseDataAccess.BreakPeriodInterval;

                return objFreeFormMsg.GetCardInResponse(objPCCardInResponse, ResponseDataAccess.BreakPeriodMessage, ResponseDataAccess.BreakPeriodDisplayTime);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return new byte[] {};
            }
        }

        #region Private Members

        public string GetCardNumber(string strMsg)
        {
            return ResponseDataAccess.IsExtendedPlayer ? strMsg.Substring(6, 10) : strMsg.Substring(4, 10);
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
            return ResponseDataAccess.IsExtendedPlayer ? strMsg.Substring(16, 10) : strMsg.Substring(14, 10);
        }

        public string GetStand(string strMsg)
        {
            return ResponseDataAccess.IsExtendedPlayer ? strMsg.Substring(26, 10) : strMsg.Substring(24, 10);
        }

        private string GetHandlePullsForCardIn(string strMsg)
        {
            return ResponseDataAccess.IsExtendedPlayer ? strMsg.Substring(37, 4) : strMsg.Substring(35, 4);
        }

        private string GetRatingIntervalForCardIn(string strMsg)
        {
            return ResponseDataAccess.IsExtendedPlayer ? ConvertMinutesToSeconds(strMsg.Substring(41, 4)) : ConvertMinutesToSeconds(strMsg.Substring(39, 4));
        }

        private bool GetPCEnrolled(string strMsg)
        {
            return ((ResponseDataAccess.IsExtendedPlayer ? strMsg.Substring(36, 1) : strMsg.Substring(34, 1)) == "Y" ? true : false);
        }

        private string ConvertMinutesToSeconds(string strRatingInsterval)
        {
            if (strRatingInsterval.Length < 4) return string.Empty;

            return (Convert.ToInt32(strRatingInsterval.Substring(0, 2)) * 60 + Convert.ToInt32(strRatingInsterval.Substring(2, 2))).ToString();
        }

        private string GetHandlePullsForApproachLimit(string strMsg)
        {
            return ResponseDataAccess.IsExtendedPlayer ? strMsg.Substring(196, 4) : strMsg.Substring(194, 4);
        }

        private string GetRatingIntervalForApproachLimit(string strMsg)
        {
            return ResponseDataAccess.IsExtendedPlayer ? strMsg.Substring(200, 4) : strMsg.Substring(198, 4);
        }

        private string GetDisplayMessageForApproachLimit(string strMsg)
        {
            return ResponseDataAccess.IsExtendedPlayer ? strMsg.Substring(204) : strMsg.Substring(202);
        }

        private string GetDisplayMessageForLimitReached(string strMsg)
        {
            return ResponseDataAccess.IsExtendedPlayer ? strMsg.Substring(197) : strMsg.Substring(195);
        }

        private char GetLockTypeForLimitReached(string strMsg)
        {
            return ResponseDataAccess.IsExtendedPlayer ? Convert.ToChar(strMsg.Substring(50, 1)) : Convert.ToChar(strMsg.Substring(48, 1));
        }

        #endregion //Private Members
    }
}
