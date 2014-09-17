namespace BMC.ExComms.Server.Executor
{
    using System.Collections.Concurrent;

    public class FreeForm_LocalQueue<T> 
        : ConcurrentQueue<T>
    {
        public string IP { get; set; }
    }

    public class FreeForm_FreeQueue<T> 
        : ConcurrentQueue<T> where T : ITaskItem
    {

    }

    public class FreeForm_ActiveQueue<T> 
        : ConcurrentQueue<T> where T : ITaskItem
    {

    }
}
