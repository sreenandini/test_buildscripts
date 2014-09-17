using System;
namespace BMC.EventsTransmitter
{
    interface IQueueManager
    {
        void CloseQueue();
        void Commit();
        System.Messaging.MessageQueue GetMessageQueue(string strQueuePath, bool CreateIfNotExists);
        bool IsQueueOpen { get; }
        bool OpenQueue();
        string ReadMessage();
        void ReleaseLock();
        void RollBack();
        void SendMessage(string Message, string Label);
    }
}
