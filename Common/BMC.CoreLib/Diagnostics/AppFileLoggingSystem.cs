using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace BMC.CoreLib.Diagnostics
{
    /// <summary>
    /// File Log Writer for the log file mentioned in app.config/web.config
    /// </summary>
    public class AppFileLoggingSystem : FileLoggingSystem
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AppFileLoggingSystem"/> class.
        /// </summary>
        /// <param name="next">The next log writer in the chain.</param>
        /// <param name="filter">The filter.</param>
        public AppFileLoggingSystem()
            : this(-1) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="AppFileLoggingSystem"/> class.
        /// </summary>
        /// <param name="maxLogSize">Size of the max log.</param>
        /// <param name="next">The next.</param>
        /// <param name="filter">The filter.</param>
        public AppFileLoggingSystem(int maxLogSize)
            : base(FileSource, maxLogSize, QueueProcessMode.Thread) { }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        private static string FileSource
        {
            get
            {
                try
                {
                    return ConfigurationManager.AppSettings["LogPath"].ToString();
                }
                catch { return string.Empty; }
            }
        }
    }
}
