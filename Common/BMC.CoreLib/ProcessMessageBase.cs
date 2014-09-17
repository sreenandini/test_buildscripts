using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.CoreLib;
using BMC.CoreLib.Diagnostics;
using BMC.CoreLib.Concurrent;

namespace BMC.CoreLib
{
    /// <summary>
    /// Process the messages
    /// </summary>
    public delegate bool ProcessMessageHandler(object message);

    public interface IProcessMessage
        : IExecutorServiceBase, IDisposable
    {
        /// <summary>
        /// Occurs when [pre process message].
        /// </summary>
        event ProcessMessageHandler PreProcessMessage;

        /// <summary>
        /// Occurs when [post process message].
        /// </summary>
        event ProcessMessageHandler PostProcessMessage;

        /// <summary>
        /// Occurs when [post send message].
        /// </summary>
        event ProcessMessageHandler PostSendMessage;
    }

    /// <summary>
    /// Process message base
    /// </summary>
    /// <typeparam name="T">Type of the message.</typeparam>
    public abstract class ProcessMessageBase
        : ExecutorBase,
        IProcessMessage
    {
        protected ProcessMessageHandler _preProcessMessage = null;
        protected ProcessMessageHandler _postProcessMessage = null;
        protected ProcessMessageHandler _postSendMessage = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProcessMessageBase"/> class.
        /// </summary>
        /// <param name="executorService">The executor service.</param>
        protected ProcessMessageBase(IExecutorService executorService)
            : base(executorService) { }

        /// <summary>
        /// Occurs when [pre process message].
        /// </summary>
        public virtual event ProcessMessageHandler PreProcessMessage
        {
            add
            {
                _preProcessMessage += value;
            }
            remove
            {
                _preProcessMessage -= value;
            }
        }

        /// <summary>
        /// Occurs when [post process message].
        /// </summary>
        public virtual event ProcessMessageHandler PostProcessMessage
        {
            add
            {
                _postProcessMessage += value;
            }
            remove
            {
                _postProcessMessage -= value;
            }
        }

        /// <summary>
        /// Occurs when [post send message].
        /// </summary>
        public virtual event ProcessMessageHandler PostSendMessage
        {
            add
            {
                _postSendMessage += value;
            }
            remove
            {
                _postSendMessage -= value;
            }
        }

        /// <summary>
        /// Called when [pre process message].
        /// </summary>
        /// <param name="message">The message.</param>
        protected virtual bool OnPreProcessMessage(object message)
        {
            if (_preProcessMessage != null)
            {
                return _preProcessMessage(message);
            }
            return true;
        }

        /// <summary>
        /// Called when [post process message].
        /// </summary>
        /// <param name="message">The message.</param>
        protected virtual bool OnPostProcessMessage(object message)
        {
            if (_postProcessMessage != null)
            {
                return _postProcessMessage(message);
            }
            return true;
        }

        /// <summary>
        /// Called when [post send message].
        /// </summary>
        /// <param name="message">The message.</param>
        protected virtual bool OnPostSendMessage(object message)
        {
            if (_postSendMessage != null)
            {
                return _postSendMessage(message);
            }
            return true;
        }

        //public string LogHelperName { get; set; }

        //public void WriteVLog(ModuleProc PROC, string message, params object[] args)
        //{
        //    //LogManager.WriteVLog(PROC, "|==> " + this.LogHelperName + " : " + message, args);
        //}

        //public void WriteLog(ModuleProc PROC, string message)
        //{
        //    //LogManager.WriteVLog(PROC, "|==> " + this.LogHelperName + " : " + message);
        //}

        //public override void QueueWorkerItem(T item)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
