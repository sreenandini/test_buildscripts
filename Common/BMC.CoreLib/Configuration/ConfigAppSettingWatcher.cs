using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.IO;
using BMC.CoreLib.Diagnostics;

namespace BMC.CoreLib.Configuration
{
    public class ConfigAppSettingWatcher : DisposableObject
    {
        private System.Configuration.Configuration _config = null;
        private FileSystemWatcher _fileMonitor = null;

        private IDictionary<string, Action<string>> _userActions = null;
        private IDictionary<string, string> _settingsCache = null;
        private object _lock = new object();

        public ConfigAppSettingWatcher()
        {
            _userActions = new SortedDictionary<string, Action<string>>(System.StringComparer.InvariantCultureIgnoreCase);
            _settingsCache = new SortedDictionary<string, string>(System.StringComparer.InvariantCultureIgnoreCase);
            this.ReloadConfig();
        }

        private void PopulateCache(bool raiseAction)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "PopulateCache");
            if (_config == null) return;

            try
            {
                foreach (KeyValueConfigurationElement item in _config.AppSettings.Settings)
                {
                    bool valueChanged = false;
                    string key = item.Key;
                    string newValue = item.Value;

                    if (!_settingsCache.ContainsKey(key))
                    {
                        _settingsCache.Add(key, newValue);
                        valueChanged = true;
                    }
                    else
                    {
                        string oldValue = _settingsCache[key];
                        _settingsCache[key] = newValue;
                        valueChanged = (!oldValue.IgnoreCaseCompare(newValue));
                    }

                    if (raiseAction && valueChanged)
                    {
                        if (_userActions.ContainsKey(key))
                        {
                            _userActions[key](newValue);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        public void Start()
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "Start");

            try
            {
                this.PopulateCache(true);

                if (_fileMonitor == null)
                {
                    lock (_lock)
                    {
                        if (_fileMonitor == null)
                        {
                            _fileMonitor = new FileSystemWatcher();
                            _fileMonitor.Path = Path.GetDirectoryName(_config.FilePath);
                            _fileMonitor.Filter = Path.GetFileName(_config.FilePath);
                            _fileMonitor.EnableRaisingEvents = true;
                            _fileMonitor.NotifyFilter = NotifyFilters.LastWrite;
                            _fileMonitor.Changed += new FileSystemEventHandler(OnFileMonitor_Changed);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        public void Stop()
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "Stop");

            try
            {
                this.PopulateCache(true);

                if (_fileMonitor != null)
                {
                    lock (_lock)
                    {
                        if (_fileMonitor != null)
                        {
                            _fileMonitor.Changed -= (OnFileMonitor_Changed);                            
                            _fileMonitor.Dispose();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        protected override void DisposeUnmanaged()
        {
            base.DisposeUnmanaged();
            this.Stop();
        }

        private void ReloadConfig()
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "ReloadConfig");
            try
            {
                if (_config != null)
                {
                    _config = null;
                }
                _config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
                _config = null;
            }
        }

        void OnFileMonitor_Changed(object sender, FileSystemEventArgs e)
        {
            this.ReloadConfig();
            this.PopulateCache(true);
        }

        public ConfigAppSettingWatcher Register(string settingName, Action<string> action)
        {
            if (!_userActions.ContainsKey(settingName))
            {
                _userActions.Add(settingName, action);
            }
            else
            {
                _userActions[settingName] = action;
            }
            return this;
        }

        protected override void DisposeManaged()
        {
            base.DisposeManaged();
            if (_fileMonitor != null)
            {
                _fileMonitor.Changed -= OnFileMonitor_Changed;
                _fileMonitor.Dispose();
            }
        }
    }
}
