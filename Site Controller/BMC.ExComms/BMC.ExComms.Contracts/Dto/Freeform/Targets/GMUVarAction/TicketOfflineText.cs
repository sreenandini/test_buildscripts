using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
    #region Maximum Offline Tickets Allowed

    public abstract class FFTgt_B2B_GVA_MOT_Allowed_Base
        : FFTgt_B2B_GMUVarAction_Data
    {
        public override int EntityId
        {
            get
            {
                return (int)FF_AppId_GMUVarAction.MaximumOfflineTicketsAllowed;
            }
        }
    }

    /// <summary>
    /// GMU to Host Freeform for Maximum Offline Tickets Allowed
    /// </summary>
    public class FFTgt_G2H_GVA_MOT_Allowed_Request
        : FFTgt_B2B_GVA_MOT_Allowed_Base, IFFTgt_G2H { }

    /// <summary>
    /// Host To GMU Freeform for Maximum Offline Tickets Allowed Response
    /// </summary>
    public class FFTgt_H2G_GVA_MOT_Allowed_Response
        : FFTgt_B2B_GVA_MOT_Allowed_Base, IFFTgt_H2G
    {
        public byte MaxOfflineTickets { get; set; }
    }

    #endregion //Maximum Offline Tickets Allowed

    #region Offline Ticket Text Line 1

    public abstract class FFTgt_B2B_GVA_OFFTKT_TxtLine1_Base
        : FFTgt_B2B_GMUVarAction_Data
    {
        public override int EntityId
        {
            get
            {
                return (int)FF_AppId_GMUVarAction.OfflineTicketTextLine1;
            }
        }
    }

    /// <summary>
    /// GMU to Host Freeform for Offline Ticket Text Line 1 Request
    /// </summary>
    public class FFTgt_G2H_GVA_OFFTKT_TxtLine1_Request
        : FFTgt_B2B_GVA_OFFTKT_TxtLine1_Base, IFFTgt_G2H { }

    /// <summary>
    /// Host To GMU Freeform for Offline Ticket Text Line 1 - Response
    /// </summary>
    public class FFTgt_H2G_GVA_OFFTKT_TxtLine1_Response
        : FFTgt_B2B_GVA_OFFTKT_TxtLine1_Base, IFFTgt_H2G
    {
        public string Line1Text { get; set; }
    }

    #endregion Offline Ticket Text Line 1

    #region Offline Ticket Text Line 2

    public abstract class FFTgt_B2B_GVA_OFFTKT_TxtLine2_Base
        : FFTgt_B2B_GMUVarAction_Data
    {
        public override int EntityId
        {
            get
            {
                return (int)FF_AppId_GMUVarAction.OfflineTicketTextLine2;
            }
        }
    }

    /// <summary>
    /// GMU to Host Freeform for Offline Ticket Text Line 2 Request
    /// </summary>
    public class FFTgt_G2H_GVA_OFFTKT_TxtLine2_Request
        : FFTgt_B2B_GVA_OFFTKT_TxtLine2_Base, IFFTgt_G2H { }

    /// <summary>
    /// Host To GMU Freeform for Offline Ticket Text Line 2 - Response
    /// </summary>
    public class FFTgt_H2G_GVA_OFFTKT_TxtLine2_Response
        : FFTgt_B2B_GVA_OFFTKT_TxtLine2_Base, IFFTgt_H2G
    {
        public string Line2Text { get; set; }
    }

    #endregion Offline Ticket Text Line 2
}
