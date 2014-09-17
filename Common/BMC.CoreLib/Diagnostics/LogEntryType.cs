/* ================================================================================= 
 * Purpose		:	Log Entry Type
 * File Name	:   LogEntryType.cs
 * Author		:	A.Vinod Kumar
 * Created  	:	10/12/2010
 * ================================================================================= 
 * Revision History :
 * ================================================================================= 
 * 10/12/2010		A.Vinod Kumar    Initial Version
 * ===============================================================================*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.CoreLib.Diagnostics
{
    // Summary:
    //     Specifies the event type of an event log entry.
    public enum LogEntryType
    {
        All = 0,
        // Summary:
        //     An error event. This indicates a significant problem the user should know
        //     about; usually a loss of functionality or data.
        Error = 1,
        //
        // Summary:
        //     A warning event. This indicates a problem that is not immediately significant,
        //     but that may signify conditions that could cause future problems.
        Warning = 2,
        //
        // Summary:
        //     An information event. This indicates a significant, successful operation.
        Information = 4,
        //
        // Summary:
        //     An exception audit event. This indicates a exception occured.
        Exception = 8,
        //
        // Summary:
        //     An raw message. This indicates message without any formatting.
        RawMessage = 16,
        Debug = 32,
        Description = 64
    }
}
