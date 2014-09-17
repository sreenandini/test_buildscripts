using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management;
using Microsoft.Win32;

namespace BMC.CoreLib.Registry
{
    public class RegistryWatcher : DisposableObject
    {
        private ManagementEventWatcher _watcher = null;
        private RegistryKey _keyToMonitor = null;

        public RegistryWatcher(RegistryKey regKey, string keyPath)
            : this(regKey, keyPath, new TimeSpan(0, 20, 0)) { }

        public RegistryWatcher(RegistryKey regKey, string keyPath, TimeSpan monitorInterval)
        {
            
            this.RegKey = regKey;
            this.KeyPath = keyPath;
            this.MonitorInterval = monitorInterval;
            _keyToMonitor = regKey.OpenSubKey(keyPath);
            this.Initialize();
        }

        private void Initialize()
        {
            if (_keyToMonitor != null)
            {
                string queryString = string.Format("SELECT * FROM RegistryKeyChangeEvent WHERE Hive = '{0}' AND KeyPath = '{1}'",
                    this.RegKey.Name, this.KeyPath);
                _watcher = new ManagementEventWatcher(new WqlEventQuery("RegistryKeyChangeEvent", queryString, this.MonitorInterval));
                _watcher.EventArrived += new EventArrivedEventHandler(OnRegistryWatcher_EventArrived);

                // specific for Windows XP
                _watcher.Scope.Path.NamespacePath = @"root\default";
            }
            else
            {
                string message = string.Format(@"The registry key {0}\{1} does not exist", this.RegKey.Name, this.KeyPath);
                throw new ArgumentException(message);
            }
        }

        void OnRegistryWatcher_EventArrived(object sender, EventArrivedEventArgs e)
        {
            if (this.RegistryKeyChanged != null)
            {
                this.RegistryKeyChanged(this, new RegistryKeyChangedEventArgs(e.NewEvent));
            }
        }

        public event EventHandler<RegistryKeyChangedEventArgs> RegistryKeyChanged;

        public RegistryKey RegKey { get; private set; }

        public string KeyPath { get; private set; }

        public TimeSpan MonitorInterval { get; private set; }

        protected override void DisposeManaged()
        {
            base.DisposeManaged();
            Extensions.DisposeObject(ref _keyToMonitor);
            Extensions.DisposeObject(ref _watcher);
        }
    }

    public class RegistryKeyChangedEventArgs : EventArgs
    {
        internal RegistryKeyChangedEventArgs(ManagementBaseObject arrivedEvent)
        {
            this.RegistryKey = arrivedEvent.Properties["Hive"].Value as string;
            this.KeyPath = arrivedEvent.Properties["KeyPath"].Value as string;
        }

        public string RegistryKey { get; private set; }
        public string KeyPath { get; private set; }
    }
}
