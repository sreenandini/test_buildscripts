using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.EventsTransmitter
{
    internal class SenderFactory
    {
        public static ISender GetSender()
        {
            // if config http
            return new HttpSender();

        }
    }
}
