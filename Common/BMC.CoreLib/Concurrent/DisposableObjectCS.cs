using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.CoreLib.Concurrent
{
    /// <summary>
    /// Object base class with critical section support.
    /// </summary>
#if !SILVERLIGHT
    [Serializable]
#endif
    public class DisposableObjectCS
        : DisposableObjectNotify
    {
        protected CriticalSection _cs = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="DisposableObjectCS"/> class.
        /// </summary>
        public DisposableObjectCS()
        {
            _cs = new CriticalSection();
        }

        /// <summary>
        /// Occurs when [notify sync status].
        /// </summary>
        public event CriticalSectionStatusHandler NotifySyncStatus
        {
            add
            {
                _cs.NotifyStatus += value;
            }
            remove
            {
                _cs.NotifyStatus -= value;
            }
        }
    }
}
