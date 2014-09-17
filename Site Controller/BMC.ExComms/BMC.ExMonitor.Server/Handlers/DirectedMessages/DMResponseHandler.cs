using BMC.Common.ExceptionManagement;
using BMC.ExComms.DataLayer.MSSQL;
using BMC.PlayerGateway.Gateway;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExMonitor.Server.Handlers.DirectedMessages
{
    /// <summary>
    /// Directed Messages Handler class
    /// </summary>
    public class DMResponseHandler
    {
        #region Data Members

        private static DMResponseHandler _dmResponseHandler = null;

        #endregion //Data Members

        #region Constructor

        public DMResponseHandler()
        {
        }

        #endregion //Constructor

        #region Properties

        public static DMResponseHandler DMResponseHandlerInstance
        {
            get
            {
                if (_dmResponseHandler == null)
                    _dmResponseHandler = new DMResponseHandler();

                return _dmResponseHandler;
            }
        }

        #endregion //Properties

        #region Public Memebers

        /// <summary>
        /// To insert DM message in to the DB
        /// </summary>
        /// <param name="strResponse"></param>
        /// <returns></returns>
        public bool InsertDMresponses(string strResponse)
        {
            try
            {
                DMMessages dMessages = FrameMessage(strResponse);
                return ExCommsDataContext.Current.InsertDMNotificationResponses(dMessages);
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
        public DMMessagesResult GetDataForSendToComms(ref int? iRecordsToProcess)
        {
            try
            {
                return ExCommsDataContext.Current.Get_DMMessages_SendToComms(HandlerHelper.Current.MaxRows, ref iRecordsToProcess);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return new DMMessagesResult();
            }
        }

        /// <summary>
        /// Delelte the messages from DB, which were sent to comms
        /// </summary>
        /// <param name="iDM_ID"></param>
        /// <returns></returns>
        public bool DeleteSentDMMessage(int iDM_ID)
        {
            try
            {
                ExCommsDataContext.Current.DeleteDirectMessage(iDM_ID);
                return true;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return false;
        }

        #endregion //Public Memebers

        #region Private Memebers

        /// <summary>
        /// To frame DM message entity
        /// </summary>
        /// <param name="strResponse"></param>
        /// <returns></returns>
        private DMMessages FrameMessage(string strResponse)
        {
            DMMessages messages = new DMMessages();
            messages.SlotNumber = GetSlotNumber(strResponse);
            messages.CardNumber = GetCardNumber(strResponse);
            messages.DisplayControl = GetDisplayControl(strResponse);
            messages.SegmentNumber = GetSegmentNumber(strResponse);
            messages.TotalSegments = GetTotalSegments(strResponse);
            messages.TransactionCode = Convert.ToInt32(GetTransactionCode(strResponse));
            messages.DisplayMessage = GetDisplayMessage(strResponse);
            messages.ConditionalMask = GetConditionalMask(strResponse);

            return messages;
        }

        private string GetSlotNumber(string strMsg)
        {
            return strMsg.Substring(4, 6);
        }

        private string GetCardNumber(string strMsg)
        {
            return strMsg.Substring(10, 10);
        }

        private string GetDisplayControl(string strMsg)
        {
            return strMsg.Substring(20, 3);
        }

        private string GetSegmentNumber(string strMsg)
        {
            return strMsg.Substring(41, 1);
        }

        private string GetTotalSegments(string strMsg)
        {
            return strMsg.Substring(42, 1);
        }

        private string GetTransactionCode(string strMsg)
        {
            return strMsg.Substring(2, 2);
        }

        private string GetDisplayMessage(string strMsg)
        {
            return strMsg.Substring(43);
        }

        private string GetConditionalMask(string strMsg)
        {
            return strMsg.Substring(23, 2);
        }

        #endregion //Private Memebers
    }
}
