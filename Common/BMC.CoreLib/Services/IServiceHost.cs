using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.CoreLib.Services
{
    /// <summary>
    /// Service Host
    /// </summary>
    public interface IServiceHost : IDisposable
    {
        /// <summary>
        /// Starts the customizaed Wcf Service Host.
        /// </summary>
        /// <returns>True if succeeded; otherwise false.</returns>
        bool Start();

        /// <summary>
        /// Stops the customizaed Wcf Service Host.
        /// </summary>
        /// <returns>True if succeeded; otherwise false.</returns>
        bool Stop();
    }
}
