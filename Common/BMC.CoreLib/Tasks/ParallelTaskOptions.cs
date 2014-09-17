using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.CoreLib.Tasks
{
    public class ParallelTaskOptions : DisposableObject
    {
        public ParallelTaskOptions() { }

        public bool IsCancelled { get; set; }
    }

    public enum ParallelTaskResultStatus
    {
        None = 0,
        Created = 1,
        Completed = 2,
        Canceled = 3
    }

    public class ParallelTaskResult : DisposableObject
    {
        private List<Exception> _exceptions = null;

        public ParallelTaskResult()
            : this(ParallelTaskResultStatus.None) { }

        public ParallelTaskResult(ParallelTaskResultStatus status)
        {
            _exceptions = new List<Exception>();
            this.Status = status;
        }

        public ParallelTaskResultStatus Status { get; set; }

        public List<Exception> Exceptions
        {
            get { return _exceptions; }
        }
    }
}
