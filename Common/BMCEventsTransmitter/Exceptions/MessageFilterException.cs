using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.EventsTransmitter
{
    internal class MessageFilteredException : Exception
    {
        public string FilteredMessageID { get; private set; }
        public MessageFilteredException(string MessageID)
            : base(String.Format("[MessageFilteredException] Message Exception Code [{0}]", MessageID))
        {
            FilteredMessageID = MessageID;
        }

    }
}
