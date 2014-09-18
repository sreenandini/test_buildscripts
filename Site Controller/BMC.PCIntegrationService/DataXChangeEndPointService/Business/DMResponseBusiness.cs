using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataXChangeEndPointService.Data;
using BMC.PlayerGateway.Gateway;
using BMC.Common.ExceptionManagement;
using System.Data;

namespace DataXChangeEndPointService.Business
{
    public class DMResponseBusiness
    {
        private static DMResponseBusiness _ResponseBusiness = null;
        private DMResponseDataAccess objResponseDataAccess = new DMResponseDataAccess();

        public DMResponseBusiness()
        {
        }

        public static DMResponseBusiness ResponseBusinessInstance
        {
            get
            {
                if (_ResponseBusiness == null)
                    _ResponseBusiness = new DMResponseBusiness();

                return _ResponseBusiness;
            }
        }

        public bool InsertDMresponses(string strResponse)
        {
            try
            {
                DMMessages dMessages =  FrameMessage(strResponse);
                return objResponseDataAccess.InsertDMMessages(dMessages);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return false;
        }

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

        private string GetMessageType(string strMsg)
        {
            return strMsg.Substring(0, 2);
        }

        private string GetTransactionCode(string strMsg)
        {
            return strMsg.Substring(2, 2);
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
        private string GetConditionalMask(string strMsg)
        {
            return strMsg.Substring(23, 2);
        }
        private string GetEPIControl1(string strMsg)
        {
            return strMsg.Substring(25, 4);
        }
        private string GetEPIControl2(string strMsg)
        {
            return strMsg.Substring(29, 4);
        }
        private string GetEPIControl3(string strMsg)
        {
            return strMsg.Substring(33, 4);
        }
        private string GetEPIControl4(string strMsg)
        {
            return strMsg.Substring(37, 4);
        }
        private string GetSegmentNumber(string strMsg)
        {
            return strMsg.Substring(41, 1);
        }
        private string GetTotalSegments(string strMsg)
        {
            return strMsg.Substring(42, 1);
        }
        private string GetDisplayMessage(string strMsg)
        {
            return strMsg.Substring(43);
        }

        public bool DeleteSentDMMessage(int iDM_ID)
        {
            try
            {
                objResponseDataAccess.DeleteSentDMMessage(iDM_ID);
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
            return objResponseDataAccess.GetDataForSendToComms(DMMessageHandler.iMaxRows, ref iRecordsToProcess);
        }
    }
}
