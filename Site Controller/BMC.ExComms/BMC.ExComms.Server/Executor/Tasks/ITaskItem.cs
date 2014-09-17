using BMC.ExComms.Contracts.DTO;

namespace BMC.ExComms.Server.Executor
{
    using BMC.CoreLib.Concurrent;
    using BMC.ExComms.Contracts.DTO.Freeform;

    public delegate void ProcessTaskQueueItem<T>(T queueItem);

    public delegate void QueueProcessedHandler(TaskItemEventArgs e);

    public interface ITaskItem
    {
        event QueueProcessedHandler QueueProcessed;

        event ProcessTaskQueueItem<ICommsEntity> ProcessQueueItem;

        int TaskID { get; }

        bool IsAvailable { get; }

        int InnerQueueCount { get; }

        void AddDataItem(ICommsEntity val);

        void StartCurrentTask();

        void StopCurrentTask();
    }

    public struct TaskItemEventArgs
    {
        public int TaskID;
        public int InstallationNo;
        public int Q_Count;
        public ITaskItem TaskItem { get; set; }
        public ICommsEntity FFMessage { get; set; }
    }
}
