using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.CoreLib.Diagnostics;
using BMC.CoreLib;

namespace BMC.CoreLib.Diagnostics
{
    public class CustomLoggingSystem
        : LoggingSystemBase
    {
        /// <summary>
        /// Occurs when [process log item].
        /// </summary>
        public event ProcessLogItemHandler ProcessLogItem = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomLoggingSystem"/> class.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="moduleName">Name of the module.</param>
        public CustomLoggingSystem(QueueProcessMode mode)
            : base(string.Empty, mode) { }

        /// <summary>
        /// Processes the queue item.
        /// </summary>
        /// <param name="item">The item.</param>
        protected override void ProcessQueueItem(LogItem item)
        {
            object[] args = item.Extra as object[];
            if (this.ProcessLogItem != null)
                this.ProcessLogItem(item);
        }

        /// <summary>
        /// Displays the wait message.
        /// </summary>
        protected virtual void DisplayWaitMessage() { }
    }
}
