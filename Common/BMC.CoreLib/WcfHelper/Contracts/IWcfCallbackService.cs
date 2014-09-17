using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.CoreLib.WcfHelper.Contracts
{
    public interface IWcfCallbackService : IServiceContractBase
    {
        void Subscribe();

        void Unsubscribe();
    }

    public interface IWcfCallbackServiceClient : IServiceContractBase
    {
        bool SkipLogException { get; set; }
    }
}
