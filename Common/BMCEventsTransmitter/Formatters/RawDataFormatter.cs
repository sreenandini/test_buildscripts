using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.EventsTransmitter
{
    class RawDataFormatter:IMessageFormatter
    {
        string _MessageStream=string.Empty;
        public RawDataFormatter(string Message)
        {
            _MessageStream = Message;
        }
        public byte[] GetBytes()
        {
            throw new NotImplementedException();
        }

        public byte[] GetBytes(byte bSequenceNumber)
        {
            throw new NotImplementedException();
        }

        public string MessageStream
        {
            get { return _MessageStream; }
        }


    }
}
