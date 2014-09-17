/* ================================================================================= 
 * Purpose		:	Language Extension Methods
 * File Name	:   Extensions.cs
 * Author		:	A.Vinod Kumar
 * Created  	:	02/12/2010
 * ================================================================================= 
 * Revision History :
 * ================================================================================= 
 * 02/12/2010		A.Vinod Kumar    Initial Version
 * ===============================================================================*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using WIN = Microsoft.Win32;
using System.Windows.Forms;
using System.Windows;
using System.Data;
using System.Data.Common;
using System.Data.Linq;
using System.Net;
using System.Net.Sockets;
using BMC.Common;

namespace BMC.MeterAdjustmentTool
{
    /// <summary>
    /// Language Extension Methods
    /// </summary>
    public static class Extensions
    {
        #region Variables
        private static string STR_SEPARATOR = new String('*', 50);
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes the <see cref="Extensions"/> class.
        /// </summary>
        static Extensions()
        {
            AppTitle = ResourceExtensions.GetResourceTextByKey(null, "Key_BMC_Meter_Adjustment_Tool_MessageBox_Header");
        }
        #endregion

        #region Clone Methods
        /// <summary>
        /// Deeps the clone.
        /// </summary>
        /// <returns>Cloned Object.</returns>
        public static T DeepClone<T>(this T source)
            where T : class, new()
        {
            using (Stream ms = new MemoryStream())
            {
                WriteBinaryObject(source, ms);
                ms.Position = 0;
                return ReadBinaryObject<T>(ms);
            }
        }
        #endregion

        #region Global Methods
        /// <summary>
        /// Gets or sets the app title.
        /// </summary>
        /// <value>The app title.</value>
        public static string AppTitle { get; set; }

        /// <summary>
        /// Gets the app version.
        /// </summary>
        /// <param name="needRevision">if set to <c>true</c> [need revision].</param>
        /// <returns>Assembly version.</returns>
        public static string GetAppVersion(bool needRevision)
        {
            AssemblyName asmName = Assembly.GetCallingAssembly().GetName();
            Version ver = asmName.Version;

            if (needRevision)
                return string.Format("{0:D}.{1:D}.{2:D}}", ver.Major, ver.Minor, ver.Revision);
            else
                return string.Format("{0:D}.{1:D}", ver.Major, ver.Minor);
        }
        #endregion

        #region Thread Functions
        /// <summary>
        /// Creates the thread.
        /// </summary>
        /// <param name="start">The start.</param>
        /// <returns>Created thread.</returns>
        public static Thread CreateThread(ThreadStart start)
        {
            return CreateThread(start, ThreadPriority.Normal);
        }

        /// <summary>
        /// Creates the thread.
        /// </summary>
        /// <param name="start">The start.</param>
        /// <param name="priority">The priority.</param>
        /// <returns>Created thread.</returns>
        public static Thread CreateThread(ThreadStart start, ThreadPriority priority)
        {
            Thread th = new Thread(start);
            th.IsBackground = true;
            th.Priority = priority;
            return th;
        }

        /// <summary>
        /// Creates the thread and starts it.
        /// </summary>
        /// <param name="start">The start.</param>
        /// <returns>Created thread.</returns>
        public static Thread CreateThreadAndStart(ThreadStart start)
        {
            Thread th = CreateThread(start);
            th.Start();
            return th;
        }

        /// <summary>
        /// Creates the thread.
        /// </summary>
        /// <param name="start">The start.</param>
        /// <returns>Created thread.</returns>
        public static Thread CreateThread(ParameterizedThreadStart start)
        {
            return CreateThread(start, ThreadPriority.Normal);
        }

        /// <summary>
        /// Creates the thread.
        /// </summary>
        /// <param name="start">The start.</param>
        /// <param name="priority">The priority.</param>
        /// <returns>Created thread.</returns>
        public static Thread CreateThread(ParameterizedThreadStart start, ThreadPriority priority)
        {
            Thread th = new Thread(start);
            th.IsBackground = true;
            th.Priority = priority;
            return th;
        }

        /// <summary>
        /// Creates the thread and starts it.
        /// </summary>
        /// <param name="start">The start.</param>
        /// <returns>Created thread.</returns>
        public static Thread CreateThreadAndStart(ParameterizedThreadStart start, object parameter)
        {
            Thread th = CreateThread(start);
            th.Start(parameter);
            return th;
        }

        /// <summary>
        /// Waits for thread to finish.
        /// </summary>
        /// <param name="workerThread">The worker thread.</param>
        public static void WaitForThreadFinish(this Thread workerThread)
        {
            WaitForThreadFinish(workerThread, null);
        }

        /// <summary>
        /// Waits for thread to finish.
        /// </summary>
        /// <param name="workerThread">The worker thread.</param>
        /// <param name="afterFinish">The after finish.</param>
        public static void WaitForThreadFinish(this Thread workerThread, Action afterFinish)
        {
            if (workerThread == null) return;

            if (workerThread.IsAlive)
            {
                workerThread.Abort();
                while (!workerThread.IsAlive) ;
                if (afterFinish != null) afterFinish();
            }
        }

        #endregion

        #region String Related Functions

        /// <summary>
        /// Determines whether the specified source is empty.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>
        /// True if string is empty; otherwise false.
        /// </returns>
        public static bool IsEmpty(this string source)
        {
            return string.IsNullOrEmpty(source);
        }

        /// <summary>
        /// Ignores the case compare.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="comapare">The comapare.</param>
        /// <returns>True if succeeded; otherwise false.</returns>
        public static bool IgnoreCaseCompare(this string source, string comapare)
        {
            return string.Compare(source, comapare, true) == 0;
        }

        /// <summary>
        /// Prepares the app file path.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>Application file path.</returns>
        public static string PrepareAppFilePath(string fileName)
        {
            string directoryName = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            return Path.Combine(directoryName, fileName);
        }

        /// <summary>
        /// Combines the strings.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="separator">The separator.</param>
        /// <returns>Combined string</returns>
        public static string CombineStrings(this string[] source, string separator)
        {
            StringBuilder sb = new StringBuilder();
            foreach (string item in source)
            {
                if (sb.Length > 0) sb.Append(separator);
                sb.Append(item);
            }
            return sb.ToString();
        }

        /// <summary>
        /// Gets the string.
        /// </summary>
        /// <param name="bytes">The bytes.</param>
        /// <param name="index">The index.</param>
        /// <param name="count">The count.</param>
        /// <returns>Converted string.</returns>
        public static string GetString(this byte[] bytes, int index, int count)
        {
            return Encoding.ASCII.GetString(bytes, index, count);
        }

        /// <summary>
        /// Gets the single.
        /// </summary>
        /// <param name="bytes">The bytes.</param>
        /// <param name="index">The index.</param>
        /// <param name="count">The count.</param>
        /// <returns>Converted single value.</returns>
        public static Single GetSingle(this byte[] bytes, int index, int count)
        {
            return GetSingle(GetString(bytes, index, count));
        }

        /// <summary>
        /// Gets the int32.
        /// </summary>
        /// <param name="bytes">The bytes.</param>
        /// <param name="index">The index.</param>
        /// <param name="count">The count.</param>
        /// <returns>Converted int32 value.</returns>
        public static Int32 GetInt32(this byte[] bytes, int index, int count)
        {
            return GetInt32(GetString(bytes, index, count));
        }

        /// <summary>
        /// Gets the int64.
        /// </summary>
        /// <param name="bytes">The bytes.</param>
        /// <param name="index">The index.</param>
        /// <param name="count">The count.</param>
        /// <returns>Converted int64 value.</returns>
        public static Int64 GetInt64(this byte[] bytes, int index, int count)
        {
            return GetInt64(GetString(bytes, index, count));
        }

        /// <summary>
        /// Gets the date time.
        /// </summary>
        /// <param name="bytes">The bytes.</param>
        /// <param name="index">The index.</param>
        /// <param name="count">The count.</param>
        /// <returns>Converted DateTime value.</returns>
        public static DateTime GetDateTime(this byte[] bytes, int index, int count)
        {
            if (count != 14) return DateTime.MinValue;

            try
            {
                DateTime dt = new DateTime(GetInt32(bytes, index, 4),
                                            GetInt32(bytes, index + 4, 2),
                                            GetInt32(bytes, index + 6, 2),
                                            GetInt32(bytes, index + 8, 2),
                                            GetInt32(bytes, index + 10, 2),
                                            GetInt32(bytes, index + 12, 2));
                return dt;
            }
            catch { return DateTime.MinValue; }
        }

        /// <summary>
        /// Gets the single.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>Converted single value.</returns>
        public static Single GetSingle(this string value)
        {
            if (string.IsNullOrEmpty(value)) return default(Single);
            Single value2 = default(Single);
            if (Single.TryParse(value, out value2)) return value2;
            return default(Single);
        }

        /// <summary>
        /// Gets the int32.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>Converted int32 value.</returns>
        public static Int32 GetInt32(this string value)
        {
            if (string.IsNullOrEmpty(value)) return default(Int32);
            Int32 value2 = default(Int32);
            if (Int32.TryParse(value, out value2)) return value2;
            return default(Int32);
        }

        /// <summary>
        /// Gets the int64.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>Converted int64 value.</returns>
        public static Int64 GetInt64(this string value)
        {
            if (string.IsNullOrEmpty(value)) return default(Int64);
            Int64 value2 = default(Int64);
            if (Int64.TryParse(value, out value2)) return value2;
            return default(Int64);
        }

        /// <summary>
        /// Gets the date time.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>Converted DateTime value.</returns>
        public static DateTime GetDateTime(this string value)
        {
            return GetDateTime(value, "yyyyMMddhhmmss");
        }

        /// <summary>
        /// Gets the date time.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="format">The format.</param>
        /// <returns>Converted DateTime value.</returns>
        public static DateTime GetDateTime(this string value, string format)
        {
            DateTime dt = DateTime.MinValue;
            if (DateTime.TryParseExact(value, format, null, System.Globalization.DateTimeStyles.None, out dt))
            {
                return dt;
            }
            return DateTime.MinValue;
        }

        public static DateTime ParseDateTime(this string value)
        {
            DateTime dt = DateTime.MinValue;
            if (DateTime.TryParse(value, out dt))
                return dt;
            return DateTime.MinValue;
        }

        /// <summary>
        /// Appends the string value.
        /// </summary>
        /// <param name="sb">The sb.</param>
        /// <param name="value">The value.</param>
        /// <param name="func">The func.</param>
        public static void AppendStringValue(this StringBuilder sb, string value, Func<string, string> func)
        {
            string value2 = value ?? "";
            sb.Append(func(value2));
        }

        /// <summary>
        /// Pads the left and sub string.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="paddingChar">The padding char.</param>
        /// <param name="totalWidth">The total width.</param>
        /// <returns>Padded and sub string.</returns>
        public static string PadLeftAndSubString(this string source, char paddingChar, int totalWidth)
        {
            return source.PadLeft(totalWidth, paddingChar).Substring(0, totalWidth);
        }

        /// <summary>
        /// Appends the file name suffix.
        /// </summary>
        /// <param name="fileFullPath">The file full path.</param>
        /// <param name="suffix">The suffix.</param>
        /// <param name="directory">The directory.</param>
        /// <returns>Full path of the file.</returns>
        public static string AppendFileNameSuffix(this string fileFullPath, string suffix, string directory)
        {
            FileInfo filePath = new FileInfo(fileFullPath);
            string ext = filePath.Extension;
            string nameOnly = Path.GetFileNameWithoutExtension(filePath.Name);
            string newFileName = Path.ChangeExtension(nameOnly + "_" +
                Path.GetFileNameWithoutExtension(suffix), ext);
            return Path.Combine(directory, newFileName);
        }

        #endregion

        #region Exception Extension Methods

        /// <summary>
        /// Iterates the exception.
        /// </summary>
        /// <param name="ex">The ex.</param>
        /// <param name="action">The action.</param>
        public static void IterateException(this Exception ex, Action<Exception> action)
        {
            Exception inner = ex;
            while (inner != null)
            {
                byte[] rawData = null;
                try { rawData = Encoding.Default.GetBytes(inner.StackTrace); }
                catch { }

                if (action != null) action(inner);
                inner = inner.InnerException;
            }
        }

        #endregion

        #region Serialization Methods

        /// <summary>
        /// Writes the specified object in binary object.
        /// </summary>
        /// <typeparam name="T">Type of the object</typeparam>
        /// <param name="obj">Object to be serialized.</param>
        /// <param name="stream">Output stream in which the serialized object stored.</param>
        /// <returns>Serialized object</returns>
        public static T WriteBinaryObject<T>(T obj, Stream stream)
            where T : class, new()
        {
            T obj2 = obj;
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, obj);
            }
            catch
            {
                obj2 = default(T);
            }
            return obj2;
        }

        /// <summary>
        /// Reads the object from binary stream.
        /// </summary>
        /// <typeparam name="T">Type of the object</typeparam>
        /// <param name="stream">Input stream in which the object to be deserialized.</param>
        /// <returns>Deserialized object</returns>
        public static T ReadBinaryObject<T>(Stream stream)
            where T : class, new()
        {
            T obj = default(T);
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                obj = formatter.Deserialize(stream) as T;
            }
            catch
            {
                obj = default(T);
            }
            return obj;
        }

        #endregion

        #region Folder Dialogs

        /// <summary>
        /// Shows the folder dialog.
        /// </summary>
        /// <param name="selectedObject">The selected object.</param>
        /// <param name="description">The description.</param>
        /// <returns>Selected folder.</returns>
        public static string ShowFolderDialog(string selectedObject, string description)
        {
            if (!Directory.Exists(selectedObject))
                selectedObject = string.Empty;

            FolderBrowserDialog oddCommand = new FolderBrowserDialog();
            oddCommand.Description = description;
            oddCommand.RootFolder = Environment.SpecialFolder.DesktopDirectory;
            oddCommand.SelectedPath = selectedObject;
            if (oddCommand.ShowDialog() == DialogResult.OK)
                return oddCommand.SelectedPath;
            return string.Empty;
        }

        #endregion

        #region Nullable Fuctions
        /// <summary>
        /// Determines whether the specified source is valid.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The source.</param>
        /// <returns>
        /// 	<c>true</c> if the specified source is valid; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsValid<T>(this Nullable<T> source)
        where T : struct
        {
            return (source != null && source.HasValue);
        }
        #endregion

        #region Util Functions

        #endregion

        #region System.Data functions
        /// <summary>
        /// Determines whether [is valid data set] [the specified source].
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>
        /// 	<c>true</c> if [is valid data set] [the specified source]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsValidDataSet(this DataSet source)
        {
            return (source != null && source.Tables.Count > 0);
        }

        /// <summary>
        /// Determines whether [is valid data table] [the specified source].
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>
        /// 	<c>true</c> if [is valid data table] [the specified source]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsValidDataTable(this DataTable source)
        {
            return (source != null && source.Rows.Count > 0);
        }

        /// <summary>
        /// Gets the data table.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="tableIndex">Index of the table.</param>
        /// <returns>Selected data table.</returns>
        public static DataTable GetDataTable(this DataSet source, int tableIndex)
        {
            DataTable dt = null;
            if (IsValidDataSet(source))
            {
                DataTableCollection tables = source.Tables;

                if (tableIndex >= 0 && tableIndex < tables.Count)
                {
                    dt = source.Tables[tableIndex];
                }
            }
            return dt;
        }

        /// <summary>
        /// Gets the data row.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="tableIndex">Index of the table.</param>
        /// <param name="rowIndex">Index of the row.</param>
        /// <returns>Selected data row.</returns>
        public static DataRow GetDataRow(this DataSet source, int tableIndex, int rowIndex)
        {
            DataTable dt = GetDataTable(source, tableIndex);
            return GetDataRow(dt, rowIndex);
        }

        /// <summary>
        /// Gets the data row.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="rowIndex">Index of the row.</param>
        /// <returns>Selected data row.</returns>
        public static DataRow GetDataRow(this DataTable source, int rowIndex)
        {
            DataRow dr = null;
            DataTable dt = source;

            if (IsValidDataTable(dt))
            {
                DataRowCollection rows = dt.Rows;
                if (rowIndex >= 0 && rowIndex < rows.Count)
                {
                    dr = rows[rowIndex];
                }
            }

            return dr;
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="row">The row.</param>
        /// <param name="columnName">Name of the column.</param>
        /// <returns>Retrieved value or default value.</returns>
        public static T GetValue<T>(this DataRow row, string columnName)
        {
            return GetValue<T>(row, columnName, default(T));
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="row">The row.</param>
        /// <param name="columnName">Name of the column.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>Retrieved value or default value.</returns>
        public static T GetValue<T>(this DataRow row, string columnName, T defaultValue)
        {
            if (row == null) return defaultValue;
            if (row[columnName] == DBNull.Value) return defaultValue;
            return (T)row[columnName];
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parameters">The parameters.</param>
        /// <param name="paramIndex">Index of the param.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>Retrieved value or default value.</returns>
        public static T GetValue<T>(this DbParameter[] parameters, int paramIndex, T defaultValue)
        {
            if (parameters == null) return defaultValue;
            if (!(paramIndex >= 0 && paramIndex < parameters.Length)) return defaultValue;
            return DBConvert.ChangeType<T>(parameters[paramIndex].Value);
        }

        /// <summary>
        /// Gets the db valid or null value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value.</param>
        /// <returns>valid or DB NULL value.</returns>
        public static object GetDbValidOrNullValue<T>(T value)
            where T : class
        {
            if (typeof(T) == typeof(string))
            {
                string sValue = Convert.ToString(value);
                if (sValue.IsEmpty()) return DBNull.Value;
                return sValue;
            }
            else
            {
                if (value == null) return DBNull.Value;
                return value;
            }
        }

        #endregion

        #region Dispose Methods

        /// <summary>
        /// Disposes the object.
        /// </summary>
        /// <param name="source">The source.</param>
        public static void DisposeObject<T>(ref T source)
            where T : IDisposable
        {
            try
            {
                source.Dispose();
            }
            catch { }
            finally
            {
                source = default(T);
            }
        }

        #endregion

        #region Date/Time Methods

        public static string ToString_ddMMMyyyy(this DateTime date)
        {
            return date.ToString("dd MMM yyyy");
        }

        public static DateTime GetValidDateTime(this DateTime source)
        {
            if (source == DateTime.MinValue) return DateTime.Now;
            return source;
        }

        public static DateTime GetDayStart(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, 0, 0, 0);
        }

        public static DateTime GetDayEnd(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, 23, 59, 59);
        }

        #endregion
    }
}
