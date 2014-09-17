using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading;
using BMC.CoreLib;
using BMC.CoreLib.Diagnostics;
using BMC.CoreLib.Concurrent;

namespace BMC.CoreLib.MSMQ
{
    /// <summary>
    /// Abstract Message Queue Processor
    /// </summary>
    /// <typeparam name="T">Type of the message.</typeparam>
    public abstract class MessageQueueProcessor
        : ProcessMessageBase,
        IMessageQueueProcessor
    {
        private MessageQueue _innerQueue = null;
        private string _servicePath = string.Empty;
        private bool _isTransactional = false;
        private IMessageFormatter _formatter = null;
        private object _lockObject = new object();
        private InitializeStatus _startAsyncStatus = InitializeStatus.Uninitialized;
        private IDictionary<string, IMessageFormatter> _messageFormatters = null;
        private int _queueTimeout = 10;

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageQueueProcessor"/> class.
        /// </summary>
        /// <param name="executorService">The executor service.</param>
        /// <param name="servicePath">The service path.</param>
        /// <param name="isTransactional">if set to <c>true</c> [is transactional].</param>
        /// <param name="formatter">The formatter.</param>
        protected MessageQueueProcessor(IExecutorService executorService, string servicePath,
            bool isTransactional, IMessageFormatter formatter, int queueTimeout) :
            this(executorService, servicePath, isTransactional, formatter, queueTimeout, null) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageQueueProcessor"/> class.
        /// </summary>
        /// <param name="executorService">The executor service.</param>
        /// <param name="servicePath">The service path.</param>
        /// <param name="isTransactional">if set to <c>true</c> [is transactional].</param>
        /// <param name="formatter">The formatter.</param>
        protected MessageQueueProcessor(IExecutorService executorService, string servicePath,
            bool isTransactional, IMessageFormatter formatter,
            int queueTimeout,
            IDictionary<string, IMessageFormatter> messageFormatters)
            : base(executorService)
        {
            _servicePath = servicePath;
            _isTransactional = isTransactional;
            _formatter = formatter;
            _messageFormatters = messageFormatters;
            _queueTimeout = (queueTimeout < 10 ? 10 : queueTimeout);
            this.Initialize();
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        private void Initialize()
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "Initialize");

            try
            {
                if (_servicePath.IsEmpty())
                {
                    Log.Info(PROC, "Message queue path was empty. Unable to proceeed further.");
                    return;
                }

                int timeout = Math.Max(5000, Extensions.GetAppSettingValueInt("MessageQueueFailedTimeout", 10000));
                bool isSuccess = false;
                while (!isSuccess)
                {
                    try
                    {
                        if (this.ExecutorService.IsShutdown) break;

                        if (MessageQueue.Exists(_servicePath))
                        {
                            _innerQueue = new MessageQueue(_servicePath, false, false, QueueAccessMode.SendAndReceive);
                        }
                        else
                        {
                            _innerQueue = MessageQueue.Create(_servicePath, _isTransactional);
                            _innerQueue.SetPermissions("EVERYONE", MessageQueueAccessRights.FullControl);
                        }

                        isSuccess = true;
                        Log.InfoV(PROC, "Message queue object for queue [{0}] was created successfully.", _servicePath);
                        //_innerQueue.Formatter = _formatter;
                    }
                    catch (Exception ex)
                    {
                        Log.Exception(PROC, ex);
                        _innerQueue = null;
                        Thread.Sleep(timeout); 
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
            finally
            {
                if (_innerQueue != null)
                {
                    if (!_innerQueue.CanRead)
                    {
                        Log.InfoV(PROC, "Message queue ({0}) was not allowed to read. Unable to proceeed further.", _servicePath);
                        Extensions.DisposeObject(ref _innerQueue);
                    }
                }
            }
        }

        /// <summary>
        /// Listens the asynchronously.
        /// </summary>
        public abstract void ListenAsync();

        /// <summary>
        /// Starts the asynchronous operation.
        /// </summary>
        /// <param name="threadName">Name of the thread.</param>
        protected void StartAsync(string threadName)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "StartAsync");

            try
            {
                if (_innerQueue != null)
                {
                    Thread th = null;
                    Extensions.InitializeThreadFunc(ref th, ref _startAsyncStatus, _lockObject,
                        new System.Threading.ThreadStart(this.ProcessMessagesAsync), threadName);
                }
                else
                {
                    Log.InfoV(PROC, "Unable to start the async thread, because unable to create the instance for Message queue ({0}).", _servicePath);
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        /// <summary>
        /// Gets the service path.
        /// </summary>
        /// <value>The service path.</value>
        public string ServicePath
        {
            get { return _servicePath; }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is transactional.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is transactional; otherwise, <c>false</c>.
        /// </value>
        public bool IsTransactional
        {
            get { return _isTransactional; }
        }

        /// <summary>
        /// Gets the message formatter.
        /// </summary>
        /// <value>The message formatter.</value>
        public IMessageFormatter MessageFormatter
        {
            get { return _formatter; }
        }

        #region Asynchronous Operations Members

        /// <summary>
        /// Posts the message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>True if succeeded; otherwise false.</returns>
        public bool PostMessage(object message)
        {
            return this.PostMessage(message, string.Empty);
        }

        /// <summary>
        /// Posts the message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>True if succeeded; otherwise false.</returns>
        public bool PostMessage(object message, string label)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "PostMessage");
            bool result = default(bool);

            try
            {
                if (message == null)
                {
                    Log.Info(PROC, "Message was empty.");
                    return false;
                }

                if (_innerQueue == null)
                {
                    Log.Info(PROC, "Message queue was null.");
                    return false;
                }

                if (!_innerQueue.CanWrite)
                {
                    Log.Info(PROC, "Message queue was not allowed to write.");
                    return false;
                }

                MessageQueueTransaction trans = null;
                try
                {
                    Message internalMessage = null;
                    if (message is Message)
                    {
                        internalMessage = (Message)message;
                    }
                    if (internalMessage == null)
                    {
                        internalMessage = new Message(message);
                        internalMessage.Formatter = this.GetFormatter(label);
                    }

                    // Transactional?
                    if (_isTransactional)
                    {
                        trans = new MessageQueueTransaction();
                        trans.Begin();

                        if (label.IsEmpty())
                            _innerQueue.Send(internalMessage, trans);
                        else
                            _innerQueue.Send(internalMessage, label, trans);
                    }
                    else
                    {
                        if (label.IsEmpty())
                            _innerQueue.Send(internalMessage);
                        else
                            _innerQueue.Send(internalMessage, label);
                    }

                    result = true;
                }
                catch (Exception ex)
                {
                    Log.Exception(PROC, ex);
                    result = false;
                }
                finally
                {
                    try
                    {
                        if (_isTransactional)
                        {
                            if (result) trans.Commit(); // Success
                            else trans.Abort(); // Failure
                        }
                    }
                    finally
                    {
                        if (result)
                        {
                            Log.Info(PROC, "Message was posted successfully to queue : " + this.ServicePath);

                            // Post send
                            try
                            {
                                this.OnPostSendMessage(message);
                            }
                            catch { /* No Interest to log the message */ }
                        }
                        else
                        {
                            Log.Info(PROC, "Unable to post the message to queue : " + this.ServicePath);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }

            return result;
        }

        private IMessageFormatter GetFormatter(string messageLabel)
        {
            IMessageFormatter formatter = null;

            // formatter
            if (!messageLabel.IsEmpty())
            {
                if (_messageFormatters != null &&
                    _messageFormatters.ContainsKey(messageLabel))
                {
                    formatter = _messageFormatters[messageLabel];
                }
            }
            if (formatter == null) formatter = _formatter;
            return formatter;
        }

        /// <summary>
        /// Gets the message from the queue.
        /// </summary>
        /// <returns>Message queue object.</returns>
        public object GetMessage()
        {
            bool isShutdown = false;
            return GetMessage(out isShutdown);
        }

        /// <summary>
        /// Gets the message from the queue.
        /// </summary>
        /// <returns>Message queue object.</returns>
        private object GetMessage(out bool isShutdown)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "DoAsync");
            object queueMsg = default(object);
            MessageQueueTransaction trans = null;
            Message msg = null;
            bool isSuccess = false;
            isShutdown = false;

            try
            {
                // Transactional?
                if (_isTransactional)
                {
                    trans = new MessageQueueTransaction();
                    trans.Begin();
                }

                // Asynchronously peek the message
                IAsyncResult arPeek = _innerQueue.BeginPeek();

                // any messages are there or shutdown called?
                if (this.ExecutorService.WaitForShutdown(arPeek.AsyncWaitHandle))
                {
                    isShutdown = true;
                    msg = null;
                    return default(object);
                }

                // Peeks the message
                msg = _innerQueue.EndPeek(arPeek);
                bool isNullMessage = false;
                if (msg != null)
                {
                    try
                    {
                        msg.Formatter = this.GetFormatter(msg.Label);
                        queueMsg = msg.Body;
                        isNullMessage = (queueMsg == null);
                    }
                    catch (Exception ex)
                    {
                        Log.Exception(PROC, ex);
                        queueMsg = default(object);
                    }
                }
                isSuccess = ((queueMsg != null) || isNullMessage);
                if (isNullMessage)
                {
                    Log.Info(PROC, "Invalid message body found. Removing from MSMQ.");
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
                isSuccess = false;
            }
            finally
            {
                try
                {
                    // if any message is there
                    if (!isShutdown)
                    {
                        // Actual message processing
                        if (queueMsg != null)
                        {
                            // Pre process
                            try
                            {
                                this.OnPreProcessMessage(queueMsg);
                            }
                            catch { /* No Interest to log the message */ }

                            // Process
                            try
                            {
                                isSuccess &= this.ProcessMessage(queueMsg);
                            }
                            catch (Exception ex)
                            {
                                Log.Exception(PROC, ex);
                            }
                        }

                        // Remove the message from the message queue
                        if (msg != null)
                        {
                            if (_isTransactional) _innerQueue.Receive(trans); // Transactional receive
                            else _innerQueue.Receive(); // Non Transactional receive
                        }
                    }

                    // either commit or rollback even if the shutdown called
                    if (_isTransactional)
                    {
                        if (isSuccess) trans.Commit(); // Success
                        else trans.Abort(); // Failure
                    }
                }
                catch (Exception ex)
                {
                    Log.Exception(PROC, ex);
                    trans.Abort();
                }
                finally
                {
                    if (queueMsg != null)
                    {
                        // Post Process
                        try
                        {
                            this.OnPostProcessMessage(queueMsg);
                        }
                        catch { /* No Interest to log the message */ }
                    }
                }
            }

            return queueMsg;
        }

        /// <summary>
        /// Getting the messages from the queue and process them.
        /// </summary>
        private void ProcessMessagesAsync()
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "ProcessMessagesAsync");
            _startAsyncStatus = InitializeStatus.Completed;
            bool isShutdown = false;
            this.ExecutorService.AddExecutor(this);

            try
            {
                // before process the messages
                try
                {
                    this.OnPreProcessMessagesAsync();
                }
                catch { }

                // actual message processing
                while (!this.ExecutorService.WaitForShutdown(_queueTimeout))
                {
                    GetMessage(out isShutdown);
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
            finally
            {
                // after process the messages                
                try
                {
                    this.OnPostProcessMessagesAsync();
                }
                catch { /* No Interest to log the message */ }

                this.Shutdown();
            }
        }

        /// <summary>
        /// Called before process message asynchronously
        /// </summary>
        protected virtual void OnPreProcessMessagesAsync() { }

        /// <summary>
        /// Called after process message asynchronously
        /// </summary>
        protected virtual void OnPostProcessMessagesAsync() { }

        /// <summary>
        /// Processes the message.
        /// </summary>
        /// <param name="message">The message.</param>
        public abstract bool ProcessMessage(object message);

        #endregion
    }
}
