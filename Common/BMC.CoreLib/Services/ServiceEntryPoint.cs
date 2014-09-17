using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.IO;
using System.Threading;
using BMC.CoreLib.Diagnostics;
using BMC.CoreLib.Win32;
using System.Windows.Forms;
using BMC.CoreLib.Concurrent;
using BMC.ExOneService.Hosting;

namespace BMC.CoreLib.Services
{
    public abstract class ServiceEntryPoint : DisposableObject
    {
        private Mutex _lock = null;
        protected Guid _mutexGuid = Guid.Empty;
        protected string[] _args = null;
        protected IExecutorService _executorService = null;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// <param name="args">The args.</param>
        protected ServiceEntryPoint(string[] args)
            : this(Guid.Empty, args)
        {
            
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// <param name="mutexGuid">The mutex unique identifier.</param>
        /// <param name="args">The args.</param>
        protected ServiceEntryPoint(Guid mutexGuid, string[] args)
        {
            _mutexGuid = mutexGuid;
            _args = args;
            _executorService = ExecutorServiceFactory.CreateExecutorService();
        }

        public abstract void Run();

        protected void RunInternal(string serviceName, string displayName, string servicePath){
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "Process");
            bool isGUIMode = false;

            try
            {
                if (_args != null)
                {
                    if (_args.Length >= 1)
                    {
                        string cmdArg = _args[0];
                        if (System.String.Compare(cmdArg, "/debug", System.StringComparison.OrdinalIgnoreCase) == 0)
                        {
                            isGUIMode = true;
                        }
                        else
                        {
                            bool processed = false;

                            try
                            {
                                using (WindowsServiceManager serviceManager = new WindowsServiceManager(serviceName,
                                    displayName, servicePath, cmdArg, Console.Out))
                                {
                                    processed = serviceManager.Process(ServiceStart.SERVICE_AUTO_START, null);
                                }
                            }
                            catch (Exception ex)
                            {
                                Log.Exception(PROC, ex);
                            }

                            if (processed) return;
                        }
                    }
                }

                Show(isGUIMode);
            }
            catch (Exception ex)
            {
                //Debugger.Break();
                Log.Exception(PROC, ex);
            }
        }

        /// <summary>
        /// Shows the specified is GUI mode.
        /// </summary>
        /// <param name="isGUIMode">if set to <c>true</c> [is GUI mode].</param>
        private void Show(bool isGUIMode)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "Process");
            bool isFirstInstance = false;
            _lock = new Mutex(false, _mutexGuid.ToString(), out isFirstInstance);

            if (!isFirstInstance)
            {
                Log.Info(PROC, "Another instance was found.");
                int processId = Process.GetCurrentProcess().Id;
                Log.Info(PROC, string.Format("Current Process : [{0:D}]", processId));

                if (isGUIMode)
                {
                    if (Win32Extensions.ShowMessageBox(null, "Another instance is already running.\nPress [Yes] to kill that instance (or) [No] to exit.",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    {
                        Log.Info(PROC, string.Format("Current Process [{0:D}] was exited with Error Code (1).", processId));
                        Thread.Sleep(2000);
                        return;
                    }
                }

                Process[] existingProcesses = Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName);
                if (existingProcesses != null)
                {
                    foreach (Process existingProcess in existingProcesses)
                    {
                        try
                        {
                            if (existingProcess.Id != processId)
                            {
                                Log.Info(PROC, string.Format("Killing Process [{0:D}] ...", existingProcess.Id));
                                existingProcess.Kill();
                                Log.Info(PROC, string.Format("Process [{0:D}] was successfully killed", existingProcess.Id));
                            }
                        }
                        catch (Exception ex)
                        {
                            Log.Info(PROC, string.Format("Unable to kill Process [{0:D}]", existingProcess.Id));
                            Log.Exception(PROC, ex);
                        }
                    }
                }
            }

            Log.Info(PROC, string.Format("Application was opened in [{0}] mode.", (isGUIMode ? "GUI (Debug)" : "Windows Service")));

            if (isGUIMode)
            {
                ShowMainForm();
            }
            else
            {
                StartService();
            }
        }

        protected abstract IServiceHost CreateServiceHost();

        /// <summary>
        /// Shows the main form.
        /// </summary>
        protected virtual void ShowMainForm()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Form mainForm = new ServiceMainForm(_executorService, this.CreateServiceHost());
            this.CustomizeForm(mainForm);
            Application.Run(mainForm);
        }

        protected virtual void CustomizeForm(Form form) { }

        /// <summary>
        /// Starts the service.
        /// </summary>
        private void StartService()
        {
            ServiceBase[] ServicesToRun = this.GetServices();
            ServiceBase.Run(ServicesToRun);
        }

        protected abstract ServiceBase[] GetServices();
    }
}
