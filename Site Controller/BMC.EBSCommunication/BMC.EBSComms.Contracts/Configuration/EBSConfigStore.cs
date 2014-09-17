using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.CoreLib.Concurrent;
using BMC.CoreLib.Configuration;

namespace BMC.EBSComms.Contracts.Configuration
{
    public interface IEBSConfigStore : IConfigStore
    {
        IExecutorService Executor { get; set; }

        string LogPath { get; set; }

        bool LogIncomingMessages { get; set; }

        bool LogOutgoingMessages { get; set; }

        bool LogClients { get; set; }
    }
}
