// -----------------------------------------------------------------------
// <copyright file="GlobalSettings.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace BMC.EBSComms.Contracts.Configuration
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using BMC.CoreLib;
    using BMC.CoreLib.Concurrent;
    using BMC.CoreLib.Configuration;
    using System.Net;
    using BMC.CoreLib.Diagnostics;
    using BMC.EBSComms.Contracts.Configuration;
    using System.ComponentModel.Composition;
    using System.Data;
    using BMC.Common.Persistence;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    [Export(typeof(IEBSCommServerConfigStore))]
    internal class EBSCommServerConfigStore : EBSConfigStore, IEBSCommServerConfigStore
    {
        internal EBSCommServerConfigStore()
        {
            this.Initialize();
        }

        [ConfigAppSetting("LogRawMessages", typeof(bool))]
        public bool LogRawMessages { get; set; }

        // Registry Values
        [ConfigAppSetting("EBSCommServerHttpPort", typeof(int))]
        public int EBSCommServerHttpPort { get; set; }
    }
}
