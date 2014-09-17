using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Windows.Forms;
using System.Threading;
using BMC.Common.ExceptionManagement;
using System.IO;
using BMC.Common.LogManagement;
using System.Globalization;

namespace BMC.Common.Utilities
{
    public class Common
    {

        #region Constant

        const int IntegerNull = int.MinValue;
        DateTime DateUnassigned = DateTime.Parse("12:00:00 AM");
        DateTime DateNull = DateTime.Parse("1/1/1800");
        DateTime DateMin = DateTime.Parse("1/1/1801");
        DateTime DateMax = DateTime.Parse("12/31/9999");
        DateTime DateTimeBase = DateTime.Parse("1/2/1801");
        const short ShortNull = short.MinValue;
        const decimal DecimalNull = decimal.MinValue;
        const double DoubleNull = double.MinValue;
        const long LongNull = long.MinValue;
        const string StringNull = "";       

        #endregion


        public static object GetNull(TypeCode DataTypeCode)
        {
            switch (DataTypeCode)
            {
                case TypeCode.Boolean:
                    return 0;
                case TypeCode.DateTime:
                    return DateTime.Parse("1/1/1800");
                case TypeCode.Decimal:
                    return DecimalNull;
                case TypeCode.Double:
                    return DoubleNull;
                case TypeCode.Int32:
                    return IntegerNull;
                case TypeCode.Int64:
                    return IntegerNull;
                case TypeCode.String:
                    return StringNull;
                default:
                    return null;
            }
        }

        public static T GetRowValue<T>(DataRow Row, string ColumnName)
        {
            Type TypeOfT;
            TypeOfT = typeof(T);
            TypeCode tpCode = Type.GetTypeCode(TypeOfT);
            switch (tpCode)
            {

                case TypeCode.DateTime:
                    if (Row[ColumnName].Equals(DBNull.Value))
                        return (T)(object)DateTime.Parse("1/1/1800");
                    else
                        return (T)Row[ColumnName];
                case TypeCode.Decimal:
                    if (Row[ColumnName].Equals(DBNull.Value))
                        return (T)(object)DecimalNull;
                    else
                        return (T)Row[ColumnName];
                case TypeCode.Double:
                    if (Row[ColumnName].Equals(DBNull.Value))
                        return (T)(object)DoubleNull;
                    else
                        return (T)Row[ColumnName];
                case TypeCode.Int32:
                    if (Row[ColumnName].Equals(DBNull.Value))
                        return (T)(object)int.Parse("0");
                    else
                        return (T)Row[ColumnName];

                case TypeCode.Int64:
                    if (Row[ColumnName].Equals(DBNull.Value))
                        return (T)(object)int.Parse("0");
                    else
                        return (T)Row[ColumnName];

                case TypeCode.String:
                    if (Row[ColumnName].Equals(DBNull.Value))
                        return (T)(object)string.Empty;
                    else
                        return (T)Row[ColumnName];

                default:
                    return (T)(object)null;
            }

        }

        public static string GetRowValueToTextBox<T>(DataRow Row, string ColumnName)
        {
            Type TypeOfT;
            TypeOfT = typeof(T);
            TypeCode tpCode = Type.GetTypeCode(TypeOfT);
            switch (tpCode)
            {

                case TypeCode.DateTime:
                    if (Row[ColumnName].Equals(DBNull.Value))
                        return DateTime.Parse("1/1/1800").ToString().Trim();
                    else
                        return Row[ColumnName].ToString().Trim();
                case TypeCode.Decimal:
                    if (Row[ColumnName].Equals(DBNull.Value))
                        return "0";
                    else
                        return Row[ColumnName].ToString().Trim();
                case TypeCode.Double:
                    if (Row[ColumnName].Equals(DBNull.Value))
                        return "0";
                    else
                        return Row[ColumnName].ToString().Trim();

                case TypeCode.Int32:
                    if (Row[ColumnName].Equals(DBNull.Value))
                        return "0";
                    else
                        return Row[ColumnName].ToString().Trim();

                case TypeCode.Int64:
                    if (Row[ColumnName].Equals(DBNull.Value))
                        return "0";
                    else
                        return Row[ColumnName].ToString().Trim();

                case TypeCode.String:
                    if (Row[ColumnName].Equals(DBNull.Value))
                        return StringNull.ToString().Trim();
                    else
                        return Row[ColumnName].ToString().Trim();

                default:
                    return (string)(object)null;
            }

        }

        public static DataTable GetDataTableFromReader(System.Data.SqlClient.SqlDataReader DataReader)
        {
            DataTable newTable = new DataTable();
            DataColumn Col;
            DataRow Row;
            for (int i = 0; i < DataReader.FieldCount - 1; i++)
            {
                Col = new DataColumn();
                Col.ColumnName = DataReader.GetName(i);
                Col.DataType = DataReader.GetFieldType(i);

                newTable.Columns.Add(Col);
            }

            while (DataReader.Read())
            {
                Row = newTable.NewRow();
                for (int i = 0; i < DataReader.FieldCount - 1; i++)
                {
                    Row[i] = DataReader[i];
                }
                newTable.Rows.Add(Row);
            }

            return newTable;
        }

        public static string GetMachineName()
        {
            return System.Environment.MachineName;
        }

        /// <summary>
        /// get universal date
        /// </summary>
        /// <param name="strDate"></param>
        /// <returns></returns>
        public static string GetUniversalDate(DateTime strDate)
        {
            return strDate.ToString("dd MMM yyyy");
        }

        /// <summary>
        /// get universal date
        /// </summary>
        /// <param name="strDate"></param>
        /// <returns></returns>
        public static string GetUniversalDateTime(DateTime strDate)
        {
            return strDate.ToString("dd MMM yyyy HH:MM:SS");
        }

        /// <summary>
        /// This method is used to get the short date format from the User Language setting in Enterprise application.
        /// </summary>
        /// <returns> short date format as a string.</returns>
        public static string GetDateFormatByUserSetting()
        {
            if (!string.IsNullOrEmpty(Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern))
            {
                return Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern;
            }
            return new CultureInfo("en-US").DateTimeFormat.ShortDatePattern;
        }

    }

    public class Handlers
    {
        #region Char Key Handlers
        public static bool IsInteger(char e)
        {
            if (e == (char)46) // This is the character Code for Decimal. 
                return true;
            if (Char.IsNumber(e) || (e == (char)Keys.Back) || e == (char)Keys.Delete || e == (char)Keys.Enter || e == (char)Keys.Tab)
                return false;
            else
                return true;
        }
        public static bool IsAlpha(char e)
        {
            if (Char.IsLetter(e) || e == (char)Keys.Back || e == (char)Keys.Delete || e == (char)Keys.Enter || e == (char)Keys.Tab)
                return false;
            else
                return true;
        }
        public static bool IsAlphaNumeric(char e)
        {
            if (Char.IsLetterOrDigit(e) || e == (char)Keys.Back || e == (char)Keys.Delete || e == (char)Keys.Enter || e == (char)Keys.Tab)
                return false;
            else
                return true;
        }
        public static bool IsInteger(char e, string AllowedCharacters)
        {
            char[] ch = AllowedCharacters.ToCharArray();

            foreach (char C in ch)
            {
                if (e == C)
                {
                    return false;
                }

            }
            if (e == (char)46)
                return true;
            if (Char.IsNumber(e) || (e == (char)Keys.Back) || e == (char)Keys.Delete || e == (char)Keys.Enter || e == (char)Keys.Tab)
                return false;
            else
                return true;
        }
        #endregion

    }
    public enum UserQueueItemPriority : short
    {
        Normal = 0,
        Low = 1,
        High = 2
    }

    public class UserModeQueue<T>
    {
        private LinkedList<T> _queue = null;

        private object _lockFullEvent = new object();
        private object _lockEmptyEvent = new object();
        private int _fullWaiters = 0;
        private int _emptyWaiters = 0;
        private int _count = 0;
        private bool _isLogging = false;
        private ManualResetEventSlim _mre = null;

        public UserModeQueue(ManualResetEventSlim mre, int capacity)
            : this(mre, capacity, -1, false) { }

        public UserModeQueue(ManualResetEventSlim mre, int capacity, int queueTimeout)
            : this(mre, capacity, queueTimeout, false) { }

        public UserModeQueue(ManualResetEventSlim mre, int capacity, bool isLogging)
            : this(mre, capacity, -1, isLogging) { }

        public UserModeQueue(ManualResetEventSlim mre, int capacity, int queueTimeout, bool isLogging)
        {
            _mre = mre;
            this.Capacity = capacity;
            _queue = new LinkedList<T>();
            _isLogging = isLogging;
            this.QueueTimeout = (queueTimeout < 0 ? -1 : queueTimeout);
        }

        public int QueueTimeout { get; set; }

        #region IQueueHandler<T> Members

        public int Capacity { get; private set; }

        public bool CanDequeue()
        {
            return false;
        }

        public bool Enqueue(T item)
        {
            return this.Enqueue(item, UserQueueItemPriority.Normal);
        }

        public bool Enqueue(T item, UserQueueItemPriority priority)
        {
            bool result = default(bool);

            lock (_queue)
            {
                try
                {
                    // if full, wait until some items consumed
                    while (_queue.Count == this.Capacity)
                    {
                        if (_mre.Wait(this.QueueTimeout)) break;
                        _fullWaiters++;

                        try
                        {
                            lock (_lockFullEvent)
                            {
                                Monitor.Exit(_queue);
                                Monitor.Wait(_lockFullEvent);
                                Monitor.Enter(_queue);
                            }
                        }
                        finally
                        {
                            _fullWaiters--;
                        }
                    }
                }
                catch (Exception ex)
                {
                    if (!_isLogging) ExceptionManager.Publish(ex);
                }
                finally
                {
                    if (!_mre.IsSet)
                    {
                        if (priority == UserQueueItemPriority.High)
                        {
                            _queue.AddFirst(item);
                        }
                        else
                        {
                            _queue.AddLast(item);
                        }

                        result = true;
                        _count = _queue.Count;
                    }
                }
            }

            // wake the waiting consumers
            if (_emptyWaiters > 0)
            {
                lock (_lockEmptyEvent)
                {
                    Monitor.Pulse(_lockEmptyEvent);
                }
            }

            return result;
        }

        public T Dequeue()
        {
            T result = default(T);

            lock (_queue)
            {
                try
                {
                    // if empty, wait for something to add
                    while (_queue.Count == 0)
                    {
                        if (_mre.Wait(this.QueueTimeout)) break;
                        _emptyWaiters++;

                        try
                        {
                            lock (_lockEmptyEvent)
                            {
                                Monitor.Exit(_queue);
                                Monitor.Wait(_lockEmptyEvent);
                                Monitor.Enter(_queue);
                            }
                        }
                        finally
                        {
                            _emptyWaiters--;
                        }
                    }
                }
                catch (Exception ex)
                {
                    if (!_isLogging) ExceptionManager.Publish(ex);
                }
                finally
                {
                    if (!_mre.IsSet)
                    {
                        // always remove from first
                        if (_queue.First != null)
                        {
                            result = _queue.First.Value;
                            _queue.RemoveFirst();
                        }
                    }
                    _count = _queue.Count;
                }
            }

            // wake the producers who are waiting to produce
            if (_fullWaiters > 0)
            {
                lock (_lockFullEvent)
                {
                    Monitor.Pulse(_lockFullEvent);
                }
            }

            return result;
        }

        public bool HasItems
        {
            get
            {
                return (_count > 0);
            }
        }

        public int QueueCount
        {
            get
            {
                return _count;
            }
        }

        #endregion

        public void Shutdown()
        {
            // wake the waiting consumers
            if (_emptyWaiters > 0)
            {
                lock (_lockEmptyEvent)
                {
                    Monitor.Pulse(_lockEmptyEvent);
                }
            }

            // wake the producers who are waiting to produce
            if (_fullWaiters > 0)
            {
                lock (_lockFullEvent)
                {
                    Monitor.Pulse(_lockFullEvent);
                }
            }
        }
    }

    public delegate void FileModifiedHandler(FileModificationWatcher watcher);

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class FileModificationWatcher : IDisposable
    {
        private FileSystemWatcher _fileMonitor = null;
        private object _lock = new object();

        [NonSerialized]
        protected bool _disposed = false;
        private DateTime _lastModified = DateTime.MinValue;

        public FileModificationWatcher(string filePath)
        {
            this.FilePath = filePath;
        }

        public string FilePath { get; private set; }

        public void StartMonitoring()
        {
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
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        public void StopMonitoring()
        {
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
                ExceptionManager.Publish(ex);
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
            try
            {
                DateTime lastModified = File.GetLastWriteTime(this.FilePath);
                if (_lastModified == DateTime.MinValue ||
                    ((lastModified > _lastModified) &&
                     (lastModified.Subtract(_lastModified).TotalMilliseconds > 900)))
                {
                    _lastModified = lastModified;
                    string message = string.Format("{0} was modified at {1}", this.FilePath, lastModified.ToString("dd/MM/yyyy HH:mm:ss.fff"));
                    LogManager.WriteLog(message, LogManager.enumLogLevel.Info);
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
                ExceptionManager.Publish(ex);
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        private void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                // managed dispose
                if (disposing)
                {
                    this.DisposeManaged();
                }

                // unmanaged dispose
                this.DisposeUnmanaged();
                _disposed = true;
            }
        }

        /// <summary>
        /// Overridable method which releases the managed resources.
        /// </summary>
        protected virtual void DisposeManaged()
        {
            this.StopMonitoring();
        }

        /// <summary>
        /// Overridable method which releases the unmanaged resources.
        /// </summary>
        protected virtual void DisposeUnmanaged() { }
    }

    public static class ShutdownHelper
    {
        public static void Shutdown()
        {
            try
            {
                if (ShutdownInitiated != null)
                {
                    ShutdownInitiated(null, EventArgs.Empty);
                }
            }
            catch (Exception ex)
            {
                EventLogExceptionAdapter.WriteException(ex);
            }
        }

        public static event EventHandler ShutdownInitiated = null;
    }
}
