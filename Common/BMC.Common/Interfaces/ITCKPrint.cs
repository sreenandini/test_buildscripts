using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TCKPrint
{

    public struct TicketInfo
    {
        public string XMLLayout;
        public string XMLData;
    }

    public class PrinterStatus
    {
        public bool Valid
        {
            get;
            set;
        }
        public int StatusInt;

        public string StatusString;
    }

    public interface ITCKPrint
    {
        PrinterStatus Pinfo
        {
            get;
            set;
        }
        /// <summary>
        ///  Checks for Out Of Paper
        /// </summary>
        bool OutOfPaper
        {
            get;

        }
        /// <summary>
        /// Connect to ITHCA 850 Printer
        /// </summary>
        /// <param name="ErrorMsg"></param>
        /// <returns></returns>
        bool Connect(ref string ErrorMsg);

        /// <summary>
        /// Print a new ticket
        /// </summary>
        /// <param name="eTicket">pass TicketInfo struct</param>
        /// <returns></returns>
        bool PrintTicket(TicketInfo eTicket);
    }
}
