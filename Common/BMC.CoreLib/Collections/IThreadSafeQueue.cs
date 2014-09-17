using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.CoreLib.Collections
{
    public enum QueueItemPriority : short
    {
        Normal = 0,
        Low = 1,
        High = 2
    }

    /// <summary>
    /// Thread safe queue interface.
    /// </summary>
    /// <typeparam name="T">Type of the item.</typeparam>
    public interface IThreadSafeQueue<T> : IDisposable
    {
        int Capacity { get; }

        /// <summary>
        /// Adds an object to the end of the queue.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="priority">The priority.</param>
        /// <returns>True if succeeded; otherwise false.</returns>
        bool Enqueue(T item);

        /// <summary>
        /// Adds an object to the end of the queue.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="priority">The priority.</param>
        /// <returns>True if succeeded; otherwise false.</returns>
        bool Enqueue(T item, QueueItemPriority priority);

        /// <summary>
        /// Removes and returns the object at the beginning of the queue.
        /// </summary>
        /// <returns>
        /// The object that is removed from the beginning of the queue.
        /// </returns>
        T Dequeue();

        /// <summary>
        /// Gets a value indicating whether the queus has items.
        /// </summary>
        /// <value><c>true</c> if the queue has items; otherwise, <c>false</c>.</value>
        bool HasItems { get; }

        /// <summary>
        /// Gets the queue count.
        /// </summary>
        /// <value>The queue count.</value>
        int QueueCount { get; }
    }
}
