using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.Common.ExceptionManagement;
using BMC.Common.LogManagement;

namespace BMC.Business.NetworkService
{
    /// <summary>
    /// ObjectState
    /// </summary>
    public enum ObjectState
    {
        /// <summary>
        /// Activated
        /// </summary>
        Activated,
        /// <summary>
        /// Deactivated
        /// </summary>
        Deactivated
    }

    /// <summary>
    /// Object State Observer
    /// </summary>
    public abstract class ObjectStateObserver
    {
        /// <summary>
        /// State
        /// </summary>
        private ObjectState _state = ObjectState.Activated;

        /// <summary>
        /// Initializes a new instance of the <see cref="ObjectStateObserver"/> class.
        /// </summary>
        protected ObjectStateObserver() { }

        /// <summary>
        /// Gets a value indicating whether this instance is object inactive.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is object inactive; otherwise, <c>false</c>.
        /// </value>
        public bool IsObjectInactive
        {
            get { return (_state == ObjectState.Deactivated); }
        }

        /// <summary>
        /// Notifies the state.
        /// </summary>
        /// <param name="state">The state.</param>
        public virtual void NotifyState(ObjectState state)
        {
            _state = state;
        }
    }

    /// <summary>
    /// Object State Notifier
    /// </summary>
    public static class ObjectStateNotifier
    {
        private static IList<ObjectStateObserver> _observers = null;
        private static object _lockObject = new object();

        /// <summary>
        /// Initializes the <see cref="ObjectStateNotifier"/> class.
        /// </summary>
        static ObjectStateNotifier() { }

        /// <summary>
        /// Gets the observers.
        /// </summary>
        /// <returns></returns>
        private static IList<ObjectStateObserver> GetObservers()
        {
            if (_observers == null)
            {
                lock (_lockObject)
                {
                    if (_observers == null)
                    {
                        _observers = new List<ObjectStateObserver>();
                    }
                }
            }
            return _observers;
        }

        /// <summary>
        /// Adds the observer.
        /// </summary>
        /// <param name="observer">The observer.</param>
        public static void AddObserver(ObjectStateObserver observer)
        {
            try
            {
                GetObservers().Add(observer);
                LogManager.WriteLog("|##> " + observer.GetType().Name + " was successfully added to observer collection.",
                    LogManager.enumLogLevel.Info);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        /// <summary>
        /// Removes the observer.
        /// </summary>
        /// <param name="observer">The observer.</param>
        public static void RemoveObserver(ObjectStateObserver observer)
        {
            try
            {
                GetObservers().Remove(observer);
                LogManager.WriteLog("|##> " + observer.GetType().Name + " was successfully removed from observer collection.",
                    LogManager.enumLogLevel.Info);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        /// <summary>
        /// Notifies the observers.
        /// </summary>
        public static void NotifyObservers(ObjectState state)
        {
            try
            {
                LogManager.WriteLog("|##> Object State : " + state.ToString(),
                        LogManager.enumLogLevel.Info);

                foreach (ObjectStateObserver observer in GetObservers())
                {
                    try
                    {
                        observer.NotifyState(state);
                    }
                    catch (Exception ex)
                    {
                        ExceptionManager.Publish(ex);
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
    }
}
