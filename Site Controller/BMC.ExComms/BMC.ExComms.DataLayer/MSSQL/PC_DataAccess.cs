using BMC.CoreLib;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Reflection;
using System.Text;

namespace BMC.ExComms.DataLayer.MSSQL
{
    public partial class ExCommsDataContext
    {
        public MeterDeltaForPCSessionResult GetMeterDeltaForPCSession(int installationNo, string pCcardNo, string sessionType)
        {
            try
            {
                using (ExCommsSQLDataAccess DataContext = this.GetDataContext())
                {
                    return DataContext.rsp_GetMeterDeltaForPCSession(installationNo, pCcardNo, sessionType).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                Log.Exception(ex);
                return new MeterDeltaForPCSessionResult();
            }
        }

        public bool InsertPCMessage(string cardNo, string slot, string stand, int? handlepulls, string ratingInterval, string displayMessage, char? lockType, string ackType, int breakPeriodInterval, bool pcEnrolled)
        {
            try
            {
                using (ExCommsSQLDataAccess DataContext = this.GetDataContext())
                {
                    if (DataContext.usp_InsertPCNotificationResponse(cardNo, slot, stand, handlepulls, ratingInterval, displayMessage, lockType, ackType, breakPeriodInterval, pcEnrolled) > -1)
                        return true;
                }
            }
            catch (Exception ex)
            {
                Log.Exception(ex);
            }
            return false;
        }

        public bool InsertPCCardResponse(string cardNo, string messageType, string messageCode, string slotNo, string stand, string message, DateTime recvDate, int handlePulls, string ratingInterval, string breakPeriod, bool pcEnrolled)
        {
            try
            {
                using (ExCommsSQLDataAccess DataContext = this.GetDataContext())
                {
                    if (DataContext.esp_InsertPCCardResponse(cardNo, messageType, messageCode, slotNo, stand, message, recvDate, handlePulls, ratingInterval, breakPeriod, pcEnrolled) > -1)
                        return true;
                }
            }
            catch (Exception ex)
            {
                Log.Exception(ex);
            }
            return false;
        }

        public PC_ServerTrackingResult Get_PCMessages_SendToComms(int maxRows, ref int? noOfRowsToProcess)
        {
            try
            {
                using (ExCommsSQLDataAccess DataContext = this.GetDataContext())
                {
                    return DataContext.rsp_Get_PC_ServerTracking(maxRows, ref noOfRowsToProcess).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                Log.Exception(ex);
                return new PC_ServerTrackingResult();
            }
        }

        public bool UpdateSentMonitorMessagestatus(int id, bool status)
        {
            try
            {
                using (ExCommsSQLDataAccess DataContext = this.GetDataContext())
                {
                    if (DataContext.usp_UpdateSentFreeFormToCommsStatus(id, status) > -1)
                        return true;
                }
            }
            catch (Exception ex)
            {
                Log.Exception(ex);
            }
            return false;
        }
    }

    public partial class ExCommsSQLDataAccess : System.Data.Linq.DataContext
    {
        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.rsp_GetMeterDeltaForPCSession")]
        public ISingleResult<MeterDeltaForPCSessionResult> rsp_GetMeterDeltaForPCSession([global::System.Data.Linq.Mapping.ParameterAttribute(DbType = "Int")] System.Nullable<int> installationNo, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "PCcardNo", DbType = "VarChar(20)")] string pCcardNo, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType = "NChar(5)")] string sessionType)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), installationNo, pCcardNo, sessionType);
            return ((ISingleResult<MeterDeltaForPCSessionResult>)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.usp_InsertPCNotificationResponse")]
        public int usp_InsertPCNotificationResponse([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "CardNo", DbType = "VarChar(10)")] string cardNo, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Slot", DbType = "VarChar(10)")] string slot, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Stand", DbType = "VarChar(10)")] string stand, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Handlepulls", DbType = "Int")] System.Nullable<int> handlepulls, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "RatingInterval", DbType = "VarChar(5)")] string ratingInterval, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "DisplayMessage", DbType = "VarChar(MAX)")] string displayMessage, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "LockType", DbType = "Char(1)")] System.Nullable<char> lockType, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "AckType", DbType = "Char(2)")] string ackType, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "BreakPeriodInterval", DbType = "Int")] System.Nullable<int> breakPeriodInterval, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "IsPCEnrolled", DbType = "Bit")] System.Nullable<bool> isPCEnrolled)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), cardNo, slot, stand, handlepulls, ratingInterval, displayMessage, lockType, ackType, breakPeriodInterval, isPCEnrolled);
            return ((int)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.esp_InsertPCCardResponse")]
        public int esp_InsertPCCardResponse([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "PC_CARDNO", DbType = "VarChar(10)")] string pC_CARDNO, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "PC_MESSAGETYPE", DbType = "VarChar(2)")] string pC_MESSAGETYPE, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "PC_MESSAGECODE", DbType = "VarChar(2)")] string pC_MESSAGECODE, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "PC_SLOTNO", DbType = "VarChar(10)")] string pC_SLOTNO, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "PC_STAND", DbType = "VarChar(10)")] string pC_STAND, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "PC_MESSAGE", DbType = "VarChar(MAX)")] string pC_MESSAGE, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "PC_RECVDATE", DbType = "DateTime")] System.Nullable<System.DateTime> pC_RECVDATE, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "PC_HANDLEPULLS", DbType = "Int")] System.Nullable<int> pC_HANDLEPULLS, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "PC_RATINGINTERVAL", DbType = "VarChar(20)")] string pC_RATINGINTERVAL, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "PC_BREAKPERIOD", DbType = "VarChar(5)")] string pC_BREAKPERIOD, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "PC_ENROLLED", DbType = "Bit")] System.Nullable<bool> pC_ENROLLED)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), pC_CARDNO, pC_MESSAGETYPE, pC_MESSAGECODE, pC_SLOTNO, pC_STAND, pC_MESSAGE, pC_RECVDATE, pC_HANDLEPULLS, pC_RATINGINTERVAL, pC_BREAKPERIOD, pC_ENROLLED);
            return ((int)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.rsp_Get_PC_ServerTracking")]
        public ISingleResult<PC_ServerTrackingResult> rsp_Get_PC_ServerTracking([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "MaxRows", DbType = "Int")] System.Nullable<int> maxRows, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "NoOfRowsToProcess", DbType = "Int")] ref System.Nullable<int> noOfRowsToProcess)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), maxRows, noOfRowsToProcess);
            noOfRowsToProcess = ((System.Nullable<int>)(result.GetParameterValue(1)));
            return ((ISingleResult<PC_ServerTrackingResult>)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.usp_UpdateSentFreeFormToCommsStatus")]
        public int usp_UpdateSentFreeFormToCommsStatus([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "PC_ST_ID", DbType = "Int")] System.Nullable<int> pC_ST_ID, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "PC_ST_Sent_FF_To_Comms_Status", DbType = "Bit")] System.Nullable<bool> pC_ST_Sent_FF_To_Comms_Status)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), pC_ST_ID, pC_ST_Sent_FF_To_Comms_Status);
            return ((int)(result.ReturnValue));
        }
    }

    public partial class MeterDeltaForPCSessionResult
    {

        private System.Nullable<long> _GamesPlayed;

        private System.Nullable<long> _GamesWon;

        private System.Nullable<long> _GamesLost;

        private System.Nullable<long> _CoinsIn;

        private System.Nullable<long> _CoinsOut;

        private System.Nullable<System.DateTime> _SessionStartDate;

        public MeterDeltaForPCSessionResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_GamesPlayed", DbType = "BigInt")]
        public System.Nullable<long> GamesPlayed
        {
            get
            {
                return this._GamesPlayed;
            }
            set
            {
                if ((this._GamesPlayed != value))
                {
                    this._GamesPlayed = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_GamesWon", DbType = "BigInt")]
        public System.Nullable<long> GamesWon
        {
            get
            {
                return this._GamesWon;
            }
            set
            {
                if ((this._GamesWon != value))
                {
                    this._GamesWon = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_GamesLost", DbType = "BigInt")]
        public System.Nullable<long> GamesLost
        {
            get
            {
                return this._GamesLost;
            }
            set
            {
                if ((this._GamesLost != value))
                {
                    this._GamesLost = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_CoinsIn", DbType = "BigInt")]
        public System.Nullable<long> CoinsIn
        {
            get
            {
                return this._CoinsIn;
            }
            set
            {
                if ((this._CoinsIn != value))
                {
                    this._CoinsIn = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_CoinsOut", DbType = "BigInt")]
        public System.Nullable<long> CoinsOut
        {
            get
            {
                return this._CoinsOut;
            }
            set
            {
                if ((this._CoinsOut != value))
                {
                    this._CoinsOut = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_SessionStartDate", DbType = "DateTime")]
        public System.Nullable<System.DateTime> SessionStartDate
        {
            get
            {
                return this._SessionStartDate;
            }
            set
            {
                if ((this._SessionStartDate != value))
                {
                    this._SessionStartDate = value;
                }
            }
        }
    }

    public partial class PC_ServerTrackingResult
    {

        private int _PC_ST_ID;

        private string _PC_ST_Card_No;

        private string _PC_ST_Slot_No;

        private string _PC_ST_Stand;

        private int _PC_ST_Handle_Pulls;

        private string _PC_ST_Rating_Interval;

        private System.Guid _PC_ST_RequestID;

        private string _PC_ST_ActualMessage;

        private string _PC_ST_AcknowledgementType;

        private char _PC_ST_LockType;

        private int _PC_ST_BreakPeriodInterval;

        private bool _PC_ST_IsPCEnrolled;

        private System.DateTime _PC_ST_Current_Time;

        private System.Data.Linq.Binary _PC_ST_CIR_COMMS_DATA;

        private int _Installation_No;

        public PC_ServerTrackingResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_PC_ST_ID", DbType = "Int NOT NULL")]
        public int PC_ST_ID
        {
            get
            {
                return this._PC_ST_ID;
            }
            set
            {
                if ((this._PC_ST_ID != value))
                {
                    this._PC_ST_ID = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_PC_ST_Card_No", DbType = "VarChar(20) NOT NULL", CanBeNull = false)]
        public string PC_ST_Card_No
        {
            get
            {
                return this._PC_ST_Card_No;
            }
            set
            {
                if ((this._PC_ST_Card_No != value))
                {
                    this._PC_ST_Card_No = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_PC_ST_Slot_No", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
        public string PC_ST_Slot_No
        {
            get
            {
                return this._PC_ST_Slot_No;
            }
            set
            {
                if ((this._PC_ST_Slot_No != value))
                {
                    this._PC_ST_Slot_No = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_PC_ST_Stand", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
        public string PC_ST_Stand
        {
            get
            {
                return this._PC_ST_Stand;
            }
            set
            {
                if ((this._PC_ST_Stand != value))
                {
                    this._PC_ST_Stand = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_PC_ST_Handle_Pulls", DbType = "Int NOT NULL")]
        public int PC_ST_Handle_Pulls
        {
            get
            {
                return this._PC_ST_Handle_Pulls;
            }
            set
            {
                if ((this._PC_ST_Handle_Pulls != value))
                {
                    this._PC_ST_Handle_Pulls = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_PC_ST_Rating_Interval", DbType = "VarChar(10) NOT NULL", CanBeNull = false)]
        public string PC_ST_Rating_Interval
        {
            get
            {
                return this._PC_ST_Rating_Interval;
            }
            set
            {
                if ((this._PC_ST_Rating_Interval != value))
                {
                    this._PC_ST_Rating_Interval = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_PC_ST_RequestID", DbType = "UniqueIdentifier NOT NULL")]
        public System.Guid PC_ST_RequestID
        {
            get
            {
                return this._PC_ST_RequestID;
            }
            set
            {
                if ((this._PC_ST_RequestID != value))
                {
                    this._PC_ST_RequestID = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_PC_ST_ActualMessage", DbType = "VarChar(500) NOT NULL", CanBeNull = false)]
        public string PC_ST_ActualMessage
        {
            get
            {
                return this._PC_ST_ActualMessage;
            }
            set
            {
                if ((this._PC_ST_ActualMessage != value))
                {
                    this._PC_ST_ActualMessage = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_PC_ST_AcknowledgementType", DbType = "VarChar(20) NOT NULL", CanBeNull = false)]
        public string PC_ST_AcknowledgementType
        {
            get
            {
                return this._PC_ST_AcknowledgementType;
            }
            set
            {
                if ((this._PC_ST_AcknowledgementType != value))
                {
                    this._PC_ST_AcknowledgementType = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_PC_ST_LockType", DbType = "Char(1) NOT NULL")]
        public char PC_ST_LockType
        {
            get
            {
                return this._PC_ST_LockType;
            }
            set
            {
                if ((this._PC_ST_LockType != value))
                {
                    this._PC_ST_LockType = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_PC_ST_BreakPeriodInterval", DbType = "Int NOT NULL")]
        public int PC_ST_BreakPeriodInterval
        {
            get
            {
                return this._PC_ST_BreakPeriodInterval;
            }
            set
            {
                if ((this._PC_ST_BreakPeriodInterval != value))
                {
                    this._PC_ST_BreakPeriodInterval = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_PC_ST_IsPCEnrolled", DbType = "Bit NOT NULL")]
        public bool PC_ST_IsPCEnrolled
        {
            get
            {
                return this._PC_ST_IsPCEnrolled;
            }
            set
            {
                if ((this._PC_ST_IsPCEnrolled != value))
                {
                    this._PC_ST_IsPCEnrolled = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_PC_ST_Current_Time", DbType = "DateTime NOT NULL")]
        public System.DateTime PC_ST_Current_Time
        {
            get
            {
                return this._PC_ST_Current_Time;
            }
            set
            {
                if ((this._PC_ST_Current_Time != value))
                {
                    this._PC_ST_Current_Time = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_PC_ST_CIR_COMMS_DATA", DbType = "VarBinary(MAX)", CanBeNull = true)]
        public System.Data.Linq.Binary PC_ST_CIR_COMMS_DATA
        {
            get
            {
                return this._PC_ST_CIR_COMMS_DATA;
            }
            set
            {
                if ((this._PC_ST_CIR_COMMS_DATA != value))
                {
                    this._PC_ST_CIR_COMMS_DATA = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Installation_No", DbType = "Int NOT NULL")]
        public int Installation_No
        {
            get
            {
                return this._Installation_No;
            }
            set
            {
                if ((this._Installation_No != value))
                {
                    this._Installation_No = value;
                }
            }
        }
    }
}
