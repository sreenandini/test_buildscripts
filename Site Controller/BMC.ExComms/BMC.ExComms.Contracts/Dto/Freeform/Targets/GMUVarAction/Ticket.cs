using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
    #region Ticket Number

    public abstract class FFTgt_B2B_GVA_TN_Base
        : FFTgt_B2B_GMUVarAction_Data
    {
        public override int EntityId
        {
            get
            {
                return (int)FF_AppId_GMUVarAction.TicketNumber;
            }
        }
    }

    /// <summary>
    /// GMU To Host Freeform for Ticket Number Request
    /// </summary>
    public class FFTgt_G2H_GVA_TN_Request
        : FFTgt_B2B_GVA_TN_Base, IFFTgt_G2H { }

    /// <summary>
    /// Host To GMU Freeform for Ticket Number Response
    /// </summary>
    public class FFTgt_H2G_GVA_TN_Response
        : FFTgt_B2B_GVA_TN_Base, IFFTgt_H2G
    {
        public int TicketNumber { get; set; }
    }

    /// <summary>
    /// GMU To Host Freeform for Ticket Number Status
    /// </summary>
    public class FFTgt_G2H_GVA_TN_Status
        : FFTgt_G2H_GMUVariableAction_Status, IFFTgt_G2H
    {
        public override int EntityId
        {
            get
            {
                return (int)FF_AppId_GMUVarAction.TicketNumber;
            }
        }
    }

    #endregion //Ticket Number

    #region Ticket Print Date

    public abstract class FFTgt_B2B_GVA_TPD_Base
        : FFTgt_B2B_GMUVarAction_Data
    {
        public override int EntityId
        {
            get
            {
                return (int)FF_AppId_GMUVarAction.TicketPrintDate;
            }
        }
    }

    /// <summary>
    /// GMU to Host Freeform for Ticket Print Date
    /// </summary>
    public class FFTgt_G2H_GVA_TPD_Request
        : FFTgt_B2B_GVA_TPD_Base, IFFTgt_G2H { }

    /// <summary>
    /// Host To GMU Freeform for Ticket Print Date Response
    /// </summary>
    public class FFTgt_H2G_GVA_TPD_Response
        : FFTgt_B2B_GVA_TPD_Base, IFFTgt_H2G
    {
        public DateTime Date { get; set; }
    }

    /// <summary>
    /// GMU To Host Freeform for Ticket Print Date Status
    /// </summary>
    public class FFTgt_G2H_GVA_TPD_Status
        : FFTgt_G2H_GMUVariableAction_Status, IFFTgt_G2H
    {
        public override int EntityId
        {
            get
            {
                return (int)FF_AppId_GMUVarAction.TicketPrintDate;
            }
        }
    }

    #endregion //Ticket Print Date

    #region Ticket Expiration Date

    public abstract class FFTgt_B2B_GVA_TED_Base
        : FFTgt_B2B_GMUVarAction_Data
    {
        public override int EntityId
        {
            get
            {
                return (int)FF_AppId_GMUVarAction.TicketExpirationDate;
            }
        }
    }

    /// <summary>
    /// GMU to Host Freeform for Ticket Expiration Date
    /// </summary>
    public class FFTgt_G2H_GVA_TED_Request
        : FFTgt_B2B_GVA_TED_Base, IFFTgt_G2H { }

    /// <summary>
    /// Host To GMU Freeform for Ticket Expiration Date Response
    /// </summary>
    public class FFTgt_H2G_GVA_TED_Response
        : FFTgt_B2B_GVA_TED_Base, IFFTgt_H2G
    {
        public DateTime Date { get; set; }
        public short ExipreDays { get; set; }
    }

    /// <summary>
    /// GMU To Host Freeform for Ticket Expiration Date Status
    /// </summary>
    public class FFTgt_G2H_GVA_TED_Status
        : FFTgt_G2H_GMUVariableAction_Status, IFFTgt_G2H
    {
        public override int EntityId
        {
            get
            {
                return (int)FF_AppId_GMUVarAction.TicketExpirationDate;
            }
        }
    }

    #endregion //Ticket Expiration Date

    #region Ticket Key

    public abstract class FFTgt_B2B_GVA_TK_Base
        : FFTgt_B2B_GMUVarAction_Data
    {
        public override int EntityId
        {
            get
            {
                return (int)FF_AppId_GMUVarAction.TicketKey;
            }
        }
    }

    /// <summary>
    /// GMU To Host Freeform for Ticket Key Request
    /// </summary>
    public class FFTgt_G2H_GVA_TK_Request
        : FFTgt_B2B_GVA_TK_Base, IFFTgt_G2H { }

    /// <summary>
    /// Host To GMU Freeform for Ticket Key Response
    /// </summary>
    public class FFTgt_H2G_GVA_TK_Response
        : FFTgt_B2B_GVA_TK_Base, IFFTgt_H2G
    {
        public string BarcodeKey { get; set; }
    }

    /// <summary>
    /// GMU To Host Freeform for Ticket Key Status
    /// </summary>
    public class FFTgt_G2H_GVA_TK_Status
        : FFTgt_G2H_GMUVariableAction_Status, IFFTgt_G2H
    {
        public override int EntityId
        {
            get
            {
                return (int)FF_AppId_GMUVarAction.TicketKey;
            }
        }
    }

    #endregion //Ticket Key

    #region Restricted Ticket Expiration Days

    public abstract class FFTgt_B2B_GVA_RTE_Days_Base
        : FFTgt_B2B_GMUVarAction_Data
    {
        public override int EntityId
        {
            get
            {
                return (int)FF_AppId_GMUVarAction.RestrictedTicketExpirationDays;
            }
        }
    }

    /// <summary>
    /// GMU to Host Freeform for Restricted Ticket Expiration Days
    /// </summary>
    public class FFTgt_G2H_GVA_RTE_Days_Request
        : FFTgt_B2B_GVA_RTE_Days_Base, IFFTgt_G2H { }

    /// <summary>
    /// Host To GMU Freeform for Restricted Ticket Expiration Days Response
    /// </summary>
    public class FFTgt_H2G_GVA_RTE_Days_Response
        : FFTgt_B2B_GVA_RTE_Days_Base, IFFTgt_H2G
    {
        public short ExipreDays { get; set; }
    }

    /// <summary>
    /// GMU To Host Freeform for Restricted Ticket Expiration Days Status
    /// </summary>
    public class FFTgt_G2H_GVA_RTE_Days_Status
        : FFTgt_G2H_GMUVariableAction_Status, IFFTgt_G2H
    {
        public override int EntityId
        {
            get
            {
                return (int)FF_AppId_GMUVarAction.RestrictedTicketExpirationDays;
            }
        }
    }

    #endregion //Restricted Ticket Expiration Days

    //#region Enhanced validation – Voucher

    //#region Enhanced validation – Voucher Profile

    //#region Enhanced validation – Voucher Profile Request

    ///// <summary>
    ///// GMU to Host Freeform for Enhanced validation – Voucher Profile Request
    ///// </summary>
    //public class FFTgt_G2H_VP_Request
    //    : FFTgt_B2B_GMUVarAction_Data, IFFTgt_G2H { }

    //#endregion //Enhanced validation – Voucher Profile Request

    //#region Enhanced validation – Voucher Profile - Response

    ///// <summary>
    ///// Host To GMU Freeform for Enhanced validation – Voucher Profile - Response
    ///// </summary>
    //public class FFTgt_H2G_VP_Response
    //    : FFTgt_B2B_GMUVarAction_Data, IFFTgt_H2G
    //{
    //    #region Private Data Member

    //    private int _maxValidationID;
    //    private int _minLevelValidationID;

    //    #endregion // Private Data Member

    //    #region Properties

    //    // Maximum Validation ID - Maximum Offline vouchers can be printed
    //    public int MaxValidationID
    //    {
    //        get
    //        {
    //            return this._maxValidationID;
    //        }
    //        set
    //        {
    //            if (this._maxValidationID == value) return;
    //            this._maxValidationID = value;
    //        }
    //    }

    //    // Min Level Validation ID
    //    public int MinLevelValidationID
    //    {
    //        get
    //        {
    //            return this._minLevelValidationID;
    //        }
    //        set
    //        {
    //            if (this._minLevelValidationID == value) return;
    //            this._minLevelValidationID = value;
    //        }
    //    }

    //    #endregion //Properties
    //}

    //#endregion //Enhanced validation – Voucher Profile - Response

    //#endregion Enhanced validation – Voucher Profile

    //#region Enhanced validation – Voucher Validation Data

    //#region Enhanced validation – Voucher Validation Data Request

    ///// <summary>
    ///// GMU to Host Freeform for Enhanced validation – Voucher Validation Data Request
    ///// </summary>
    //public class FFTgt_G2H_VV_Data_Request
    //    : FFTgt_B2B_GMUVarAction_Data, IFFTgt_G2H
    //{
    //    #region Private Data Member

    //    private int _validationRequestID;
    //    private int _numValidationID;

    //    #endregion // Private Data Member

    //    #region Properties

    //    // An ID for the Set of Machine/Sequence Numbers allocated by the host as requested. Default value is zero
    //    public int ValidationRequestID
    //    {
    //        get
    //        {
    //            return this._validationRequestID;
    //        }
    //        set
    //        {
    //            if (this._validationRequestID == value) return;
    //            this._validationRequestID = value;
    //        }
    //    }

    //    // Number of Sets of Machine/Sequence Numbers requested. Default value is 10
    //    public int NumValidationID
    //    {
    //        get
    //        {
    //            return this._numValidationID;
    //        }
    //        set
    //        {
    //            if (this._numValidationID == value) return;
    //            this._numValidationID = value;
    //        }
    //    }

    //    #endregion //Properties
    //}

    //#endregion //Enhanced validation – Voucher Validation Data Request

    //#region Enhanced validation – Voucher Validation Data - Response

    ///// <summary>
    ///// Host To GMU Freeform for Enhanced validation – Voucher Validation Data - Response
    ///// </summary>
    //public class FFTgt_H2G_VV_Data_Response
    //    : FFTgt_B2B_GMUVarAction_Data, IFFTgt_H2G
    //{
    //    #region Private Data Member

    //    private int _validationRequestID;
    //    private List<int> _validationIDs;

    //    #endregion // Private Data Member

    //    #region Properties

    //    // An ID for the Set of Machine/Sequence Numbers allocated by the host for that request. Default value is zero
    //    public int ValidationRequestID
    //    {
    //        get
    //        {
    //            return this._validationRequestID;
    //        }
    //        set
    //        {
    //            if (this._validationRequestID == value) return;
    //            this._validationRequestID = value;
    //        }
    //    }

    //    // A LIST of MachineID, SequenceNumber
    //    public List<int> ValidationIDs
    //    {
    //        get
    //        {
    //            return this._validationIDs;
    //        }
    //        set
    //        {
    //            this._validationIDs = value;
    //        }
    //    }

    //    #endregion //Properties
    //}

    //#endregion //Enhanced validation – Voucher Validation Data - Response

    //#endregion //Enhanced validation – Voucher Validation Data

    //#endregion //Enhanced validation – Voucher
}
