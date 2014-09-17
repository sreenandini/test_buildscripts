using BMC.CoreLib;
using BMC.PlayerGateway.Gateway;
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
        public bool InsertDMNotificationResponses(DMMessages dmMessages)
        {
            try
            {
                using (ExCommsSQLDataAccess DataContext = this.GetDataContext())
                {
                    if (DataContext.usp_InsertDMNotificationResponses(
                                                                        dmMessages.CardNumber,
                                                                        dmMessages.SlotNumber,
                                                                        dmMessages.SlotNumber,
                                                                        dmMessages.FirstName,
                                                                        Convert.ToDateTime(dmMessages.Birthday),
                                                                        dmMessages.DisplayMessage,
                                                                        dmMessages.TransactionCode.ToString(),
                                                                        dmMessages.DisplayControl,
                                                                        dmMessages.ConditionalMask,
                                                                        dmMessages.TotalSegments,
                                                                        dmMessages.SegmentNumber,
                                                                        dmMessages.EPIControl1,
                                                                        dmMessages.EPIControl2,
                                                                        dmMessages.EPIControl3,
                                                                        dmMessages.EPIControl4
                                                                     ) > -1)
                        return true;
                }
            }
            catch (Exception ex)
            {
                Log.Exception(ex);
            }
            return false;
        }

        public bool DeleteDirectMessage(int id)
        {
            try
            {
                using (ExCommsSQLDataAccess DataContext = this.GetDataContext())
                {
                    if (DataContext.dsp_DeleteDirectMessage(id) > -1)
                        return true;
                }
            }
            catch (Exception ex)
            {
                Log.Exception(ex);
            }
            return false;
        }

        public DMMessagesResult Get_DMMessages_SendToComms(int maxRows, ref int? noOfRowsToProcess)
        {
            try
            {
                using (ExCommsSQLDataAccess DataContext = this.GetDataContext())
                {
                    return DataContext.rsp_Get_DMMessages(maxRows, ref noOfRowsToProcess).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                Log.Exception(ex);
                return new DMMessagesResult();
            }
        }
    }

    public partial class ExCommsSQLDataAccess : System.Data.Linq.DataContext
    {
        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.usp_InsertDMNotificationResponses")]
        public int usp_InsertDMNotificationResponses([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "DM_Card_No", DbType = "VarChar(10)")] string dM_Card_No, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "DM_Slot_No", DbType = "VarChar(10)")] string dM_Slot_No, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "DM_Stand", DbType = "VarChar(10)")] string dM_Stand, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "FirstName", DbType = "VarChar(50)")] string firstName, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Birthday", DbType = "DateTime")] System.Nullable<System.DateTime> birthday, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "DM_ActualMessage", DbType = "VarChar(MAX)")] string dM_ActualMessage, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "DM_Type", DbType = "VarChar(10)")] string dM_Type, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "DM_DisplayControl", DbType = "VarChar(10)")] string dM_DisplayControl, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "DM_ConditionalMask", DbType = "VarChar(20)")] string dM_ConditionalMask, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "DM_TotalSegments", DbType = "VarChar(20)")] string dM_TotalSegments, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "DM_SegmentNumber", DbType = "VarChar(20)")] string dM_SegmentNumber, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "DM_EPIControl1", DbType = "VarChar(20)")] string dM_EPIControl1, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "DM_EPIControl2", DbType = "VarChar(20)")] string dM_EPIControl2, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "DM_EPIControl3", DbType = "VarChar(20)")] string dM_EPIControl3, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "DM_EPIControl4", DbType = "VarChar(20)")] string dM_EPIControl4)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), dM_Card_No, dM_Slot_No, dM_Stand, firstName, birthday, dM_ActualMessage, dM_Type, dM_DisplayControl, dM_ConditionalMask, dM_TotalSegments, dM_SegmentNumber, dM_EPIControl1, dM_EPIControl2, dM_EPIControl3, dM_EPIControl4);
            return ((int)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.dsp_DeleteDirectMessage")]
        public int dsp_DeleteDirectMessage([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "DM_ID", DbType = "Int")] System.Nullable<int> dM_ID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), dM_ID);
            return ((int)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.rsp_Get_DMMessages")]
        public ISingleResult<DMMessagesResult> rsp_Get_DMMessages([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "MaxRows", DbType = "Int")] System.Nullable<int> maxRows, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "NoOfRowsToProcess", DbType = "Int")] ref System.Nullable<int> noOfRowsToProcess)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), maxRows, noOfRowsToProcess);
            noOfRowsToProcess = ((System.Nullable<int>)(result.GetParameterValue(1)));
            return ((ISingleResult<DMMessagesResult>)(result.ReturnValue));
        }
    }

    public partial class DMMessagesResult
    {

        private int _DM_ID;

        private string _CardNo;

        private string _SlotNo;

        private string _Stand;

        private string _FirstName;

        private System.Nullable<System.DateTime> _Birthday;

        private string _DM_ActualMessage;

        private string _DMType;

        private string _DisplayControl;

        private string _ConditionalMask;

        private string _TotalSegments;

        private string _SegmentNumber;

        private string _EPIControl1;

        private string _EPIControl2;

        private string _EPIControl3;

        private string _EPIControl4;

        private bool _SentToComms;

        private bool _SentFFToComms;

        private System.Data.Linq.Binary _CIRCommsData;

        private int _Installation_No;

        public DMMessagesResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_DM_ID", DbType = "Int NOT NULL")]
        public int DM_ID
        {
            get
            {
                return this._DM_ID;
            }
            set
            {
                if ((this._DM_ID != value))
                {
                    this._DM_ID = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_CardNo", DbType = "VarChar(20)")]
        public string CardNo
        {
            get
            {
                return this._CardNo;
            }
            set
            {
                if ((this._CardNo != value))
                {
                    this._CardNo = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_SlotNo", DbType = "VarChar(50)")]
        public string SlotNo
        {
            get
            {
                return this._SlotNo;
            }
            set
            {
                if ((this._SlotNo != value))
                {
                    this._SlotNo = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Stand", DbType = "VarChar(50)")]
        public string Stand
        {
            get
            {
                return this._Stand;
            }
            set
            {
                if ((this._Stand != value))
                {
                    this._Stand = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_FirstName", DbType = "VarChar(50)")]
        public string FirstName
        {
            get
            {
                return this._FirstName;
            }
            set
            {
                if ((this._FirstName != value))
                {
                    this._FirstName = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Birthday", DbType = "DateTime")]
        public System.Nullable<System.DateTime> Birthday
        {
            get
            {
                return this._Birthday;
            }
            set
            {
                if ((this._Birthday != value))
                {
                    this._Birthday = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_DM_ActualMessage", DbType = "VarChar(500)")]
        public string DM_ActualMessage
        {
            get
            {
                return this._DM_ActualMessage;
            }
            set
            {
                if ((this._DM_ActualMessage != value))
                {
                    this._DM_ActualMessage = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_DMType", DbType = "VarChar(20)")]
        public string DMType
        {
            get
            {
                return this._DMType;
            }
            set
            {
                if ((this._DMType != value))
                {
                    this._DMType = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_DisplayControl", DbType = "Char(3)")]
        public string DisplayControl
        {
            get
            {
                return this._DisplayControl;
            }
            set
            {
                if ((this._DisplayControl != value))
                {
                    this._DisplayControl = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ConditionalMask", DbType = "VarChar(20)")]
        public string ConditionalMask
        {
            get
            {
                return this._ConditionalMask;
            }
            set
            {
                if ((this._ConditionalMask != value))
                {
                    this._ConditionalMask = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_TotalSegments", DbType = "VarChar(20)")]
        public string TotalSegments
        {
            get
            {
                return this._TotalSegments;
            }
            set
            {
                if ((this._TotalSegments != value))
                {
                    this._TotalSegments = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_SegmentNumber", DbType = "VarChar(20)")]
        public string SegmentNumber
        {
            get
            {
                return this._SegmentNumber;
            }
            set
            {
                if ((this._SegmentNumber != value))
                {
                    this._SegmentNumber = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_EPIControl1", DbType = "VarChar(20)")]
        public string EPIControl1
        {
            get
            {
                return this._EPIControl1;
            }
            set
            {
                if ((this._EPIControl1 != value))
                {
                    this._EPIControl1 = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_EPIControl2", DbType = "VarChar(20)")]
        public string EPIControl2
        {
            get
            {
                return this._EPIControl2;
            }
            set
            {
                if ((this._EPIControl2 != value))
                {
                    this._EPIControl2 = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_EPIControl3", DbType = "VarChar(20)")]
        public string EPIControl3
        {
            get
            {
                return this._EPIControl3;
            }
            set
            {
                if ((this._EPIControl3 != value))
                {
                    this._EPIControl3 = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_EPIControl4", DbType = "VarChar(20)")]
        public string EPIControl4
        {
            get
            {
                return this._EPIControl4;
            }
            set
            {
                if ((this._EPIControl4 != value))
                {
                    this._EPIControl4 = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_SentToComms", DbType = "Bit NOT NULL")]
        public bool SentToComms
        {
            get
            {
                return this._SentToComms;
            }
            set
            {
                if ((this._SentToComms != value))
                {
                    this._SentToComms = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_SentFFToComms", DbType = "Bit NOT NULL")]
        public bool SentFFToComms
        {
            get
            {
                return this._SentFFToComms;
            }
            set
            {
                if ((this._SentFFToComms != value))
                {
                    this._SentFFToComms = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_CIRCommsData", DbType = "VarBinary(MAX)", CanBeNull = true)]
        public System.Data.Linq.Binary CIRCommsData
        {
            get
            {
                return this._CIRCommsData;
            }
            set
            {
                if ((this._CIRCommsData != value))
                {
                    this._CIRCommsData = value;
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
