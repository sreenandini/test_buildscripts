using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.ServiceProcess;
using BMC.CoreLib.Diagnostics;

namespace BMC.CoreLib.Services
{
    /// <summary>
    /// Windows Service Manager
    /// </summary>
    public class WindowsServiceManager : DisposableObject
    {
        private string _serviceName = string.Empty;
        private string _displayName = string.Empty;
        private string _binaryPath = string.Empty;
        private TextWriter _output = null;
        private string _operation = string.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceManager"/> class.
        /// </summary>
        /// <param name="serviceName">Name of the service.</param>
        /// <param name="displayName">The display name.</param>
        /// <param name="binaryPath">The binary path.</param>
        /// <param name="operation">The operation.</param>
        /// <param name="output">The output.</param>
        public WindowsServiceManager(string serviceName, string displayName,
            string binaryPath, string operation, TextWriter output)
        {
            _serviceName = serviceName;
            _displayName = displayName;
            _binaryPath = binaryPath;
            _operation = operation;
            _output = output;
        }

        /// <summary>
        /// Processes the specified start.
        /// </summary>
        /// <param name="start">The start.</param>
        /// <param name="args">The args.</param>
        /// <returns>True if succeeded; otherwise false.</returns>
        public bool Process(ServiceStart start, string[] args)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "Process");
            bool result = true;
            bool validArg = false;

            try
            {
                if (string.Compare(_operation, "/install", true) == 0)
                {
                    validArg = true;
                    Log.Info(PROC, "Argument : Service Installation");

                    ServiceControllerEx.InstallService(null, _serviceName, _displayName,
                        start, _binaryPath, null, string.Empty);

                    Log.Info(PROC, "Service installed successfully.");
                }
                else if (string.Compare(_operation, "/uninstall", true) == 0)
                {
                    validArg = true;
                    Log.Info(PROC, "Argument : Service Uninstallation");

                    ServiceControllerEx.UninstallService(null, _serviceName);

                    Log.Info(PROC, "Service uninstalled successfully.");
                }
                else if (string.Compare(_operation, "/start", true) == 0)
                {
                    validArg = true;
                    Log.Info(PROC, "Argument : Service Start");

                    ServiceController controller = new ServiceController(_serviceName, ".");
                    controller.Start(args);
                    controller.WaitForStatus(ServiceControllerStatus.Running, new TimeSpan(0, 0, 30));

                    Log.Info(PROC, "Service started successfully.");
                }
                else if (string.Compare(_operation, "/stop", true) == 0)
                {
                    validArg = true;
                    Log.Info(PROC, "Argument : Service Stop");

                    ServiceController controller = new ServiceController(_serviceName, ".");
                    controller.Stop();
                    controller.WaitForStatus(ServiceControllerStatus.Running, new TimeSpan(0, 0, 30));

                    Log.Info(PROC, "Service stopped successfully.");
                }
                else
                {
                    Log.Info(PROC, "Argument : Unrecognized input is given.");
                    result = false;
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
                result = false;
            }
            return (result || validArg);
        }
    }
}
