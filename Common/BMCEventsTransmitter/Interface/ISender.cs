using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.EventsTransmitter
{
    internal interface ISender
    {
        bool IsConnected {get;}
        bool Initialize(string IPAddress, int Port);
        int Send(IMessageFormatter Formatter);
        bool CloseSender();
    }
}
