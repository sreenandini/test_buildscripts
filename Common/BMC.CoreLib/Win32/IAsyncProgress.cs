using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.CoreLib.Concurrent;

namespace BMC.CoreLib.Win32
{
    public interface IAsyncProgress
    {
        /// <summary>
        /// Gets the executor service.
        /// </summary>
        /// <value>The executor service.</value>
        IExecutorService ExecutorService { get; }

        /// <summary>
        /// Updates the status.
        /// </summary>
        /// <param name="statusText">The status text.</param>
        void UpdateStatus(string statusText);

        /// <summary>
        /// Crosses the thread invoke.
        /// </summary>
        /// <param name="method">The method.</param>
        /// <param name="args">The args.</param>
        void CrossThreadInvoke(Delegate method, params object[] args);

        /// <summary>
        /// Crosses the thread invoke.
        /// </summary>
        /// <param name="action">The action.</param>
        void CrossThreadInvoke(Action action);

        /// <summary>
        /// Gets or sets a value indicating whether [close on complete].
        /// </summary>
        /// <value><c>true</c> if [close on complete]; otherwise, <c>false</c>.</value>
        bool CloseOnComplete { get; set; }

        /// <summary>
        /// Occurs when [cancel click].
        /// </summary>
        event EventHandler CancelClick;
    }

    public interface IAsyncProgress2 : IAsyncProgress
    {
        /// <summary>
        /// Initializes the progress.
        /// </summary>
        /// <param name="minimum">The minimum.</param>
        /// <param name="maximum">The maximum.</param>
        void InitializeProgress(int minimum, int maximum);

        /// <summary>
        /// Updates the status.
        /// </summary>
        /// <param name="statusText">The status text.</param>
        void UpdateStatusObject(object status);

        /// <summary>
        /// Updates the status progress.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="statusText">The status text.</param>
        void UpdateStatusProgress(int value, string statusText);

        /// <summary>
        /// Gets or sets the color of the console.
        /// </summary>
        /// <value>The color of the console.</value>
        ConsoleColor? ConsoleColor { get; set; }
    }

    public sealed class SyncAsyncProgress : IAsyncProgress2
    {
        public void InitializeProgress(int minimum, int maximum)
        {
            
        }

        public void UpdateStatusObject(object status)
        {
            
        }

        public void UpdateStatusProgress(int value, string statusText)
        {
           
        }

        public ConsoleColor? ConsoleColor
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public IExecutorService ExecutorService
        {
            get { throw new NotImplementedException(); }
        }

        public void UpdateStatus(string statusText)
        {
            
        }

        public void CrossThreadInvoke(Delegate method, params object[] args)
        {
            
        }

        public void CrossThreadInvoke(Action action)
        {
            action();
        }

        public bool CloseOnComplete
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public event EventHandler CancelClick;
    }


    /// <summary>
    /// UpdateAsyncStatusHandler
    /// </summary>
    public delegate void UpdateAsyncStatusHandler(string statusText);

    /// <summary>
    /// UpdateAsyncStatusHandler
    /// </summary>
    public delegate void UpdateAsyncStatusProgressHandler(int value, string statusText);

    /// <summary>
    /// UpdateAsyncStatus2Handler
    /// </summary>
    public delegate void UpdateAsyncStatusObjectHandler(object status);
}
