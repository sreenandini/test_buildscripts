using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Messaging;
using System.Threading;
using BMC.EventsTransmitter.Utils;

namespace BMC.EventsTransmitter
{
    public class QueueManager : BMC.EventsTransmitter.IQueueManager
    {
        private string _QueuePath;
        private MessageQueue _Queue;
       // private bool IsReadingQueue;
        bool _IsQueueOpen;
        Log Logger = Log.GetInstance(); 
        private ManualResetEvent _mreRefresh = null;
        public  bool IsQueueOpen
        {
            get
            {
                return _IsQueueOpen;
            }
        }
        private QueueManager()
        {
            _mreRefresh = new ManualResetEvent(false);
        }
        public QueueManager(string QueuePath)
            : this()
        {
            _QueuePath = QueuePath;
        }
        public bool OpenQueue()
        {
            if (string.IsNullOrEmpty(_QueuePath))
                throw new Exception("QueueManager:Queue path Empty");
                _Queue = this.GetMessageQueue(_QueuePath,true) ;
                _IsQueueOpen = true;
            return _IsQueueOpen;
        }
        public void ReleaseLock()
        {
            _mreRefresh.Set();
        }
        public string ReadMessage()
        {
            string result = string.Empty;
            try
            {
                //if (IsReadingQueue==true)
                //{
                //    throw new Exception("QueueManager: Uncommited transaction found While reading Queue");
                //}
                if (!IsQueueOpen)
                {
                    throw new Exception("QueueManager:Queue not opened to read");
                }
                //IsReadingQueue = true;

                IAsyncResult async = _Queue.BeginPeek();
                while (true)
                {
                    if (async.AsyncWaitHandle.WaitOne(100))
                    {
                        break;
                    }
                    if (_mreRefresh.WaitOne(100))
                    {
                        _mreRefresh.Reset();
                        return result;
                    }
                }
                Message msg = _Queue.EndPeek(async);
                if (msg != null)
                {
                    result = msg.Body.ToString();
                }               
            }
            catch 
            {
                //close queue 
                this.CloseQueue();
                //IsReadingQueue = false;
                throw;
            }

            return result;
        }
        public void Commit()
        {
            //if (IsReadingQueue)
            //{
               _Queue.Receive(new TimeSpan(0, 0, 10));
              // IsReadingQueue = false;
            //}
        }
        public void RollBack()
        {
          //do nothing 
        }
        public void CloseQueue()
        {
            if (_Queue != null)
            {
                _Queue.Close();
                _Queue.Dispose();
                _IsQueueOpen = false;
            }
        }
        public void SendMessage(string Message,string Label )
        {
            if (!_IsQueueOpen)
            {
                throw new Exception("QueueManager:Queue not opened to write");
            }
            Message _Message = new Message();
            _Message.Formatter = new ActiveXMessageFormatter();
            _Message.Recoverable = true;
            _Message.Body = Message;
            _Message.Label = Label;
            _Queue.Send(_Message);
        }
        public  MessageQueue GetMessageQueue(string strQueuePath,  bool CreateIfNotExists)
        {
            MessageQueue _mQueue = null;
            try
            {
                if (MessageQueue.Exists(strQueuePath))
                {
                    //creates an instance MessageQueue 
                    _mQueue = new System.Messaging.MessageQueue(strQueuePath);
                }
                else
                {
                    if(CreateIfNotExists)
                    {
                    //creates a new private queue
                        _mQueue = MessageQueue.Create(strQueuePath);
                     _mQueue.SetPermissions("EveryOne", MessageQueueAccessRights.FullControl);
                     _mQueue.Formatter = new ActiveXMessageFormatter(); 
                    }
                }
                _mQueue.Formatter = new ActiveXMessageFormatter(); 
                return _mQueue;
            }
            catch (Exception Ex)
            {
                Logger.Error("QueueManager","GetMessageQueue(\"" + strQueuePath + "\")" , Ex);
                throw Ex;
            }
        }
    }
    public class WriteOnlyQueue
    {
        private QueueManager _QueueManager = null;
        public WriteOnlyQueue(string strQueuePath)
        {
            _QueueManager = new QueueManager(strQueuePath);
            _QueueManager.OpenQueue();
        }
        public void sendMessage(string XmlMessage)
        {
            _QueueManager.SendMessage(XmlMessage, "STMQueue");
        }
    }
}
