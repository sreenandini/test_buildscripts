using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataXChangeEndPointService.Data
{
    public class DBConstants
    {
        #region SP

        public const string SP_ESP_INSERTPCCARDRESPONSE = "esp_InsertPCCardResponse";
        public const string SP_USP_INSERTPCNOTIFICATIONRESPONSE = "usp_InsertPCNotificationResponse";
        public const string SP_RSP_GETINSTALLATIONNOBYBARPOSITION = "rsp_GetInstallationNoByBarPosition";
        public const string RSP_GET_PC_SERVERTRACKING = "rsp_Get_PC_ServerTracking";
        public const string USP_UPDATESENTFREEFORMTOCOMMSSTATUS = "usp_UpdateSentFreeFormToCommsStatus";

        #endregion //SP

        #region Parameters

        public const string CONST_PARAM_PC_ST_ID = "@PC_ST_ID";
        public const string CONST_PARAM_PC_CARD_NO = "@PC_CARDNO";
        public const string CONST_PARAM_PC_MESSAGE_TYPE = "@PC_MESSAGETYPE";
        public const string CONST_PARAM_PC_MESSAGE_CODE = "@PC_MESSAGECODE";
        public const string CONST_PARAM_PC_SLOTNO = "@PC_SLOTNO";
        public const string CONST_PARAM_PC_STAND = "@PC_STAND";
        public const string CONST_PARAM_PC_MESSAGE = "@PC_MESSAGE";
        public const string CONST_PARAM_PC_RECEIVED_DATE = "@PC_RECVDATE";
        public const string CONST_PARAM_PC_HANDLEPULLS = "@PC_HANDLEPULLS";
        public const string CONST_PARAM_PC_RATINGINTERVAL = "@PC_RATINGINTERVAL";
        public const string CONST_PARAM_PC_COMMS_DATA = "@PC_ST_CIR_COMMS_DATA";
        public const string CONST_PARAM_PC_DISPLAYMESSAGE = "@DisplayMessage";
        public const string CONST_PARAM_PC_LOCKTYPE = "@LockType";
        public const string CONST_PARAM_PC_ACKTYPE = "@AckType";
        public const string CONST_PARAM_PC_BREAKPERIOD = "@PC_BREAKPERIOD";
        public const string CONST_PARAM_PC_ENROLLED = "@PC_ENROLLED";
        public const string CONST_PARAM_BAR_POSITION_NAME = "@Bar_Position_Name";
        public const string CONST_PARAM_INSTALLATION_NO = "@Installation_No";
        public const string CONST_PARAM_SENT_TO_COMMS = "@SENT_TO_COMMS";
        public const string CONST_PARAM_MAX_ROWS = "@MaxRows";
        public const string CONST_PARAM_PC_ST_SENT_FF_TO_COMMS_STATUS = "@PC_ST_Sent_FF_To_Comms_Status";
        public const string CONST_PARAM_NOOFROWSTOPROCESS = "@NoOfRowsToProcess";

        #endregion //Parameters

        #region DM parameters
        public const string SP_USP_INSERTDMNOTIFICATIONRESPONSES = "usp_InsertDMNotificationResponses";
        public const string SP_RSP_GET_DMMESSAGES = "rsp_Get_DMMessages";
        public const string DSP_DELETEDIRECTMESSAGE = "dsp_DeleteDirectMessage";


        public const string CONST_PARAM_DM_ID = "@DM_ID";
        public const string CONST_PARAM_DM_CARD_NO = "@DM_Card_No";
        public const string CONST_PARAM_DM_SLOT_NO = "@DM_Slot_No";
        public const string CONST_PARAM_DM_STAND = "@DM_Stand";
        public const string CONST_PARAM_DM_FIRSTNAME = "@FirstName";
        public const string CONST_PARAM_DM_BIRTHDAY = "@Birthday";
        public const string CONST_PARAM_DM_ACTUALMESSAGE = "@DM_ActualMessage";
        public const string CONST_PARAM_DM_TYPE = "@DM_Type";
        public const string CONST_PARAM_DM_DISPLAYCONTROL = "@DM_DisplayControl";
        public const string CONST_PARAM_DM_CONDITIONALMASK = "@DM_ConditionalMask";
        public const string CONST_PARAM_DM_TOTALSEGMENTS = "@DM_TotalSegments";
        public const string CONST_PARAM_DM_DM_SEGMENTNUMBER = "@DM_SegmentNumber";
        public const string CONST_PARAM_DM_EPICONTROL1 = "@DM_EPIControl1";
        public const string CONST_PARAM_DM_EPICONTROL2 = "@DM_EPIControl2";
        public const string CONST_PARAM_DM_EPICONTROL3 = "@DM_EPIControl3";
        public const string CONST_PARAM_DM_EPICONTROL4 = "@DM_EPIControl4";
        public const string CONST_PARAM_CIR_SENT_TO_COMMS = "@DM_CIR_Sent_TO_Comms";
        public const string CONST_PARAM_CIR_COMMS_DATA = "@DM_CIR_COMMS_DATA";
        public const string CONST_PARAM_DM__SENT_FF_TO_COMMS = "@DM_Sent_FF_TO_Comms";

        #endregion //DM parameters
    }
}
