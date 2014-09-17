// -----------------------------------------------------------------------
// <copyright file="FileModificationWatcher.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace BMC.CoreLib.Configuration
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.IO;
    using BMC.CoreLib.Diagnostics;

    public delegate void FileModifiedHandler(FileModificationWatcher watcher);

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class FileModificationWatcher : DisposableObjectNotify
    {
        private FileSystemWatcher _fileMonitor = null;
        private object _lock = new object();

        private DateTime _lastModified = DateTime.MinValue;

        public FileModificationWatcher(string filePath)
        {
            this.FilePath = filePath;
        }

        public string FilePath { get; private set; }

        public void StartMonitoring()
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "StartMonitoring");

            try
            {
                if (_fileMonitor == null)
                {
                    lock (_lock)
                    {
                        if (_fileMonitor == null)
                        {
                            _fileMonitor = new FileSystemWatcher();
                            _fileMonitor.Path = Path.GetDirectoryName(this.FilePath);
                            _fileMonitor.Filter = Path.GetFileName(this.FilePath);
                            _fileMonitor.EnableRaisingEvents = true;
                            _fileMonitor.NotifyFilter = NotifyFilters.LastWrite;
                            _fileMonitor.Changed += new FileSystemEventHandler(OnFileMonitor_Changed);
                            //this.OnFileModified();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        public void StopMonitoring()
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "StopMonitoring");

            try
            {
                if (_fileMonitor != null)
                {
                    lock (_lock)
                    {
                        if (_fileMonitor != null)
                        {
                            _fileMonitor.EnableRaisingEvents = false;
                            _fileMonitor.Changed -= (OnFileMonitor_Changed);
                            _fileMonitor.Dispose();
                            _fileMonitor = null;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        void OnFileMonitor_Changed(object sender, FileSystemEventArgs e)
        {
            FileSystemWatcher fsw = sender as FileSystemWatcher;
            if (fsw == null) return;

            try
            {
                fsw.EnableRaisingEvents = false;
                this.OnFileModified(e);
            }
            finally
            {
                fsw.EnableRaisingEvents = true;
            }
        }

        public event FileModifiedHandler FileModified = null;

        public WatcherChangeTypes ChangeType { get; set; }

        private void OnFileModified(FileSystemEventArgs e)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "Initialize");

            try
            {
                DateTime lastModified = File.GetLastWriteTime(this.FilePath);
                if (_lastModified == DateTime.MinValue ||
                    ((lastModified > _lastModified) &&
                     (lastModified.Subtract(_lastModified).TotalMilliseconds > 900)))
                {
                    _lastModified = lastModified;
                    string message = string.Format("{0} was modified at {1}", this.FilePath, lastModified.ToString("dd/MM/yyyy HH:mm:ss.fff"));
                    Log.Info(message);
                }
                else
                {
                    return;
                }
            }
            catch { }

            try
            {
                this.ChangeType = e.ChangeType;
                if (this.FileModified != null)
                {
                    this.FileModified(this);
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        protected override void DisposeManaged()
        {
            base.DisposeManaged();
            this.StopMonitoring();
        }
    }
}
