// -----------------------------------------------------------------------
// <copyright file="ApplicationManager.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace BMC.CoreLib.WPF
{
#if NET4
    using BMC.CoreLib.IoC;
    using System.ComponentModel.Composition;
#endif
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Windows;
    using System.Threading;
    using System.Windows.Threading;
    using System.Reflection;
    using BMC.CoreLib.Concurrent;
    using BMC.CoreLib.Diagnostics;
    using System.ComponentModel;
    using System.Windows.Controls;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public static class ApplicationManager
    {
        private static Dispatcher _dispatcher = null;
        private static _ObjectWrapper _objectWrapper = null;
        private static IExecutorService _executorService = null;
        private static DependencyObject _rootVisual = null;
        private static SynchronizationContext _syncContext = null;

        private const string KEY_MAINCONTENTVISUAL = "MainContentVisual";

        static ApplicationManager()
        {
            Initialize();
        }

        private static void Initialize()
        {
#if !SILVERLIGHT
            _dispatcher = Dispatcher.FromThread(Thread.CurrentThread);
#else
            _rootVisual = rootVisual;
            _dispatcher = System.Windows.Deployment.Current.Dispatcher;
#endif

            _executorService = ExecutorServiceFactory.CreateExecutorService();
            CurrentApplication.Properties.Add("ExecutorService", _executorService);

#if !SILVERLIGHT
            CurrentApplication.DispatcherUnhandledException += CurrentApplication_DispatcherUnhandledException;
#else
            CurrentApplication.UnhandledException += CurrentApplication_UnhandledException;
#endif
            CurrentApplication.Exit += CurrentApplication_Exit;
        }

#if !SILVERLIGHT
        public static void Initialize(string uri)
#else
        public static void Initialize(UIElement rootVisual)
#endif
        {
            _objectWrapper = new _ObjectWrapper();

#if SILVERLIGHT
            _rootVisual = rootVisual;
            _dispatcher = System.Windows.Deployment.Current.Dispatcher;
#endif

            CurrentApplication.Startup += CurrentApplication_Startup;
#if !SILVERLIGHT
            CurrentApplication.StartupUri = new Uri(uri, UriKind.Relative);
#endif

#if NET4
            if (Application.Current != null)
            {
                MEFHelper.ComposeParts(Application.Current);
            }
#endif
        }

        static void CurrentApplication_Startup(object sender, StartupEventArgs e)
        {
#if !SILVERLIGHT
            _rootVisual = CurrentApplication.MainWindow;
#else
            CurrentApplication.RootVisual = _rootVisual as UIElement;        
#endif
        }

        private static void CurrentApplication_Exit(object sender, ExitEventArgs e)
        {
            _executorService.Shutdown();
        }

        public static bool IsInvokeRequired
        {
            get
            {
                Dispatcher dispatcher = _dispatcher;
                if (dispatcher == null) dispatcher = Application.Current.Dispatcher;
                return (_dispatcher != Dispatcher.FromThread(Thread.CurrentThread));
            }
        }

        public static DisposableObjectNotify StartupViewModel { get; set; }

        public static object ContentVisual
        {
            get
            {
                if (CurrentApplication.Properties.Contains(KEY_MAINCONTENTVISUAL))
                {
                    return CurrentApplication.Properties[KEY_MAINCONTENTVISUAL];
                }
                else
                {
                    return null;
                }
            }
            set
            {
                object oldValue = null;
                if (CurrentApplication.Properties.Contains(KEY_MAINCONTENTVISUAL))
                {
                    oldValue = CurrentApplication.Properties[KEY_MAINCONTENTVISUAL];
                    CurrentApplication.Properties[KEY_MAINCONTENTVISUAL] = value;
                }
                else
                {
                    CurrentApplication.Properties.Add(KEY_MAINCONTENTVISUAL, value);
                }

                if (oldValue != value &&
                    StartupViewModel != null)
                {
                    StartupViewModel.NotifyPropertyChanged("ContentVisual");
                }
            }
        }

#if !SILVERLIGHT
        private static void CurrentApplication_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            ModuleProc PROC = new ModuleProc("App", "Application_DispatcherUnhandledException");
            Log.Exception(PROC, e.Exception);
        }
#else
        private void CurrentApplication_UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            // If the app is running outside of the debugger then report the exception using
            // the browser's exception mechanism. On IE this will display it a yellow alert 
            // icon in the status bar and Firefox will display a script error.
            if (!System.Diagnostics.Debugger.IsAttached)
            {

                // NOTE: This will allow the application to continue running after an exception has been thrown
                // but not handled. 
                // For production applications this error handling should be replaced with something that will 
                // report the error to the website and stop the application.
                e.Handled = true;
                Deployment.Current.Dispatcher.BeginInvoke(delegate { ReportErrorToDOM(e); });
            }
        }

        private void ReportErrorToDOM(ApplicationUnhandledExceptionEventArgs e)
        {
            try
            {
                string errorMsg = e.ExceptionObject.Message + e.ExceptionObject.StackTrace;
                errorMsg = errorMsg.Replace('"', '\'').Replace("\r\n", @"\n");

                System.Windows.Browser.HtmlPage.Window.Eval("throw new Error(\"Unhandled Error in Silverlight Application " + errorMsg + "\");");
            }
            catch (Exception)
            {
            }
        }
#endif

        public static Application CurrentApplication
        {
            get { return Application.Current; }
        }

#if !SILVERLIGHT
#endif

        public static object FindResource(object resourceKey)
        {
#if !SILVERLIGHT
            return CurrentApplication.FindResource(resourceKey);
#else
            return CurrentApplication.Resources[resourceKey];
#endif
        }

        public static T FindResource<T>(object resourceKey)
            where T : class
        {
#if !SILVERLIGHT
            return CurrentApplication.FindResource(resourceKey) as T;
#else
            return CurrentApplication.Resources[resourceKey] as T;
#endif
        }

        public static SynchronizationContext SyncContext
        {
            get
            {
                if (_syncContext == null)
                {
                    if (SynchronizationContext.Current != null)
                    {
                        _syncContext = SynchronizationContext.Current;
                    }
                }
                return _syncContext;
            }
            set { _syncContext = value; }
        }

        public static void SyncSend(Action action)
        {
            SyncSend((o) =>
            {
                action();
            }, null);
        }

        public static void SyncSendSync(SendOrPostCallback d, object state)
        {
            if (IsInvokeRequired)
            {
                SyncSend(d, state);
            }
            else
            {
                d(state);
            }
        }

        public static void SyncSend(SendOrPostCallback d, object state)
        {
            if (SyncContext != null)
            {
                SyncContext.Send(d, state);
            }
            else
            {
#if !SILVERLIGHT
                _dispatcher.Invoke(d, state);
#else
                _dispatcher.BeginInvoke(d, state);
#endif
            }
        }

        public static void AsyncSend(SendOrPostCallback d, object state)
        {
            if (SyncContext != null)
            {
                SyncContext.Post(d, state);
            }
            else
            {
                _dispatcher.BeginInvoke(d, state);
            }
        }

        public static IExecutorService ExecutorService
        {
            get { return ApplicationManager._executorService; }
        }

        [CLSCompliant(false)]
        public class _ObjectWrapper
        {
            public _ObjectWrapper()
            {
#if NET4
                MEFHelper.ComposeParts(this);
#endif
            }

#if NET4
            [Import(typeof(IViewManager))]
#endif
            public IViewManager ViewManager { get; set; }

#if NET4
            [Import(typeof(IMessageBoxViewManager))]
#endif
            public IMessageBoxViewManager MessageBoxManager { get; set; }
        }

        public static IViewManager ViewManager
        {
            get
            {
                return _objectWrapper.ViewManager;
            }
        }

        public static IMessageBoxViewManager MessageBoxManager
        {
            get
            {
                return _objectWrapper.MessageBoxManager;
            }
        }

        public static void Shutdown()
        {
            Shutdown(0);
        }

        public static void Shutdown(int exitCode)
        {
#if !SILVERLIGHT
            CurrentApplication.Shutdown(exitCode);
#else
#endif
        }

        public static Window GetWindow(Control control)
        {
            ModuleProc PROC = new ModuleProc("ApplicationManager", "GetWindow");
            Window result = default(Window);

            try
            {
                if (control == null)
                {
                    result = CurrentApplication.MainWindow;
                }

                if (result == null)
                {
                    if (control != null)
                    {
                        result = Window.GetWindow(control);
                    }
                    if (result == null)
                    {
                        result = _rootVisual as Window;
                    }
                    if (result == null)
                    {
                        result = CurrentApplication.MainWindow;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }

            return result;
        }
    }
}
