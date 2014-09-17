using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
    public abstract class FFTgt_B2B_GVA_EnablePrint_RT_Base
        : FFTgt_B2B_GMUVarAction_Data
    {
        public override int EntityId
        {
            get
            {
                return (int)FF_AppId_GMUVarAction.EnablePrintingOfRestrictedTickets;
            }
        }
    }

    /// <summary>
    /// GMU to Host Freeform for Enable Printing of Restricted Tickets Request
    /// </summary>
    public class FFTgt_G2H_GVA_EnablePrint_RT_Request
        : FFTgt_B2B_GVA_EnablePrint_RT_Base, IFFTgt_G2H { }

    /// <summary>
    /// Host To GMU Freeform for Enable Printing of Restricted Tickets Response
    /// </summary>
    public class FFTgt_H2G_GVA_EnablePrint_RT_Response
        : FFTgt_B2B_GVA_EnablePrint_RT_Base, IFFTgt_H2G
    {
        // Enable Restricted Tickets
        // 0 -> Do not allow printing of restricted tickets, 1 -> Allow printing of restricted tickets.
        public FF_AppId_PrintRestrictedTicket EnableRestrictedTickets { get; set; }
    }

    /// <summary>
    /// GMU To Host Freeform for Enable Printing of Restricted Tickets Status
    /// </summary>
    public class FFTgt_G2H_GVA_EnablePrint_RT_Status
        : FFTgt_G2H_GMUVariableAction_Status, IFFTgt_G2H
    {
        public override int EntityId
        {
            get
            {
                return (int)FF_AppId_GMUVarAction.EnablePrintingOfRestrictedTickets;
            }
        }
    }
}
