using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.CoreLib.Diagnostics {
    [Serializable]
    public class ConsoleLoggingSystem
        : LoggingSystemBase {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConsoleLoggingSystem"/> class.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="moduleName">Name of the module.</param>
        public ConsoleLoggingSystem()
            : this(QueueProcessMode.Synchronous) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConsoleLoggingSystem"/> class.
        /// </summary>
        /// <param name="displayWaitMessage">if set to <c>true</c> [display wait message].</param>
        public ConsoleLoggingSystem(QueueProcessMode mode)
            : base(typeof(Console).FullName, mode) { }

        /// <summary>
        /// Processes the queue item.
        /// </summary>
        /// <param name="item">The item.</param>
        protected override void ProcessQueueItem(LogItem item) {
            object[] args = item.Extra as object[];

            //string message = "Type : " + item.LogType.ToString() + ", Message : " + item.Message;
            string message = item.Message;
            if (args == null)
                Console.WriteLine(message);
            else
                Console.WriteLine(message, (object[])args);
        }

        /// <summary>
        /// Gets or sets a value indicating whether [need wait message].
        /// </summary>
        /// <value><c>true</c> if [need wait message]; otherwise, <c>false</c>.</value>
        public bool NeedWaitMessage { get; private set; }
    }
}
