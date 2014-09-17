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
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Data;
using System.Data.Common;
using System.Data.Linq;
using System.Net;
using System.Threading.Tasks;
using BMC.CoreLib.Diagnostics;
using System.Net.Sockets;
using System.Xml.Linq;
using BMC.CoreLib.Concurrent;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.Configuration;
using System.Globalization;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.Xml;
using System.Runtime.InteropServices;
using BMC.Common.LogManagement;
using System.Net.NetworkInformation;

namespace BMC.CoreLib
{
    /// <summary>
    /// Language Extension Methods
    /// </summary>
    public static partial class Extensions
    {
        #region Variables
        private static string STR_SEPARATOR = new String('*', 50);
        private static readonly bool _useTaskInsteadOfThread = false;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes the <see cref="Extensions"/> class.
        /// </summary>
        static Extensions()
        {
            AppTitle = "BMC Core Library";
            _useTaskInsteadOfThread = Extensions.GetAppSettingValueBool("UseTaskInsteadOfThread", false);
        }
        #endregion

        #region Encoding
#if !SILVERLIGHT
        private static Encoding _currentEncoding = Encoding.ASCII;
#else
        private static Encoding _currentEncoding = Encoding.Unicode;
#endif
        public static Encoding ASIIEncoding
        {
            get { return Encoding.ASCII; }
        }
        public static Encoding CurrentEncoding
        {
            get { return Extensions._currentEncoding; }
            set { Extensions._currentEncoding = value; }
        }
        #endregion

        #region Clone Methods
        /// <summary>
        /// Deep clone the given object.
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

        /// <summary>
        /// Deep clone the given object.
        /// </summary>
        /// <returns>Cloned Object.</returns>
        public static T DeepCloneXml<T>(this T source)
            where T : class, new()
        {
            using (Stream ms = new MemoryStream())
            {
                WriteXmlObject(source, ms);
                ms.Position = 0;
                return ReadXmlObject<T>(ms);
            }
        }

        /// <summary>
        /// Deep clone the given object.
        /// </summary>
        /// <returns>Cloned Object.</returns>
        public static T DeepCloneDataContract<T>(this T source)
            where T : class, new()
        {
            using (Stream ms = new MemoryStream())
            {
                WriteDataContractObject(source, ms);
                ms.Position = 0;
                return ReadDataContractObject<T>(ms);
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
        /// Gets the name of the assembly.
        /// </summary>
        /// <returns>Assembly name.</returns>
        public static AssemblyName GetAssemblyName(this Assembly assembly)
        {
#if !SILVERLIGHT
            return assembly.GetName();
#else
            return new AssemblyName(assembly.FullName);
#endif
        }
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

        public static void DisposeComObject(object comObject)
        {
            ModuleProc PROC = new ModuleProc("Extensions", "DisposeObjects");

            try
            {
                if (comObject == null) return;
                Marshal.FinalReleaseComObject(comObject);
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        public static void DisposeObjects<T>(this ICollection<T> collection)
            where T : IDisposableObject
        {
            ModuleProc PROC = new ModuleProc("Extensions", "DisposeObjects");

            try
            {
                foreach (var item in collection)
                {
                    if (item != null &&
                        !item.IsDisposed)
                    {
                        item.Dispose();
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        public static void DisposeObjects<K, V>(this IDictionary<K, V> collection)
            where V : IDisposableObject
        {
            ModuleProc PROC = new ModuleProc("Extensions", "DisposeObjects");

            try
            {
                DisposeObjects<V>(collection.Values);
                collection.Clear();
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
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
            return CreateThreadAndStart(start, string.Empty);
        }

        /// <summary>
        /// Creates the thread and starts it.
        /// </summary>
        /// <param name="start">The start.</param>
        /// <param name="threadName">Name of the thread.</param>
        /// <returns>Created thread.</returns>
        public static Thread CreateThreadAndStart(ThreadStart start, string threadName)
        {
            Thread th = CreateThread(start);
            if (!threadName.IsEmpty())
            {
                th.Name = threadName + th.ManagedThreadId.ToString();
            }
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
        /// <param name="parameter">The parameter.</param>
        /// <returns>Created thread.</returns>
        public static Thread CreateThreadAndStart(ParameterizedThreadStart start, object parameter)
        {
            return CreateThreadAndStart(start, parameter, string.Empty);
        }

        /// <summary>
        /// Creates the thread and starts it.
        /// </summary>
        /// <param name="start">The start.</param>
        /// <param name="parameter">The parameter.</param>
        /// <param name="threadName">Name of the thread.</param>
        /// <returns>Created thread.</returns>
        public static Thread CreateThreadAndStart(ParameterizedThreadStart start, object parameter, string threadName)
        {
            Thread th = CreateThread(start);
            if (!threadName.IsEmpty())
            {
                th.Name = threadName + th.ManagedThreadId.ToString();
            }
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

        public static Task CreateLongRunningTask(Action start)
        {
            return CreateLongRunningTask(start, null);
        }

        public static Task CreateLongRunningTask(Action start, CancellationToken? token)
        {
            Task th = null;
            th = token.HasValue ?
                new Task(start, token.SafeValue(), TaskCreationOptions.LongRunning) :
                new Task(start, TaskCreationOptions.LongRunning);
            th.Start();
            return th;
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
            return CurrentEncoding.GetString(bytes, index, count);
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
        /// Gets the unsigned int32.
        /// </summary>
        /// <param name="bytes">The bytes.</param>
        /// <param name="index">The index.</param>
        /// <param name="count">The count.</param>
        /// <returns>Converted unsigned int32 value.</returns>
        public static UInt32 GetUInt32(this byte[] bytes, int index, int count)
        {
            return TypeSystem.GetValueUInt(GetString(bytes, index, count));
        }
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
        /// Gets the unsigned int64.
        /// </summary>
        /// <param name="bytes">The bytes.</param>
        /// <param name="index">The index.</param>
        /// <param name="count">The count.</param>
        /// <returns>Converted unsigned int64 value.</returns>
        public static UInt64 GetUInt64(this byte[] bytes, int index, int count)
        {
            return TypeSystem.GetValueUInt64(GetString(bytes, index, count));
        }
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
        /// Gets the date time.
        /// </summary>
        /// <param name="bytes">The bytes.</param>
        /// <param name="index">The index.</param>
        /// <param name="count">The count.</param>
        /// <returns>Converted DateTime value.</returns>
        public static DateTime GetDateTimeWithoutSeprator(this byte[] bytes, int index, int count)
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
        /// <returns>Converted DateTime value.</returns>
        public static DateTime GetDateTime24(this string value)
        {
            return GetDateTime(value, "yyyyMMddHHmmss");
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
        /// Appends the format line.
        /// </summary>
        /// <param name="sb">The sb.</param>
        /// <param name="format">The format.</param>
        /// <param name="args">The arguments.</param>
        public static void AppendFormatLine(this StringBuilder sb, string format, params object[] args)
        {
            sb.AppendFormat(format, args);
            sb.Append(Environment.NewLine);
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
        /// Converts to hex.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="length">The length.</param>
        /// <returns>Hexadecimal string.</returns>
        public static string ConvertToHex(this string source, int length)
        {
            if (string.IsNullOrEmpty(source)) return string.Empty;
            int value = TypeSystem.GetValueInt(source);
            return value.ToString("X" + length.ToString());
        }

        /// <summary>
        /// Converts to int32.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>Int32 value.</returns>
        public static int ConvertToInt32(this string source)
        {
            return TypeSystem.GetValueInt(source);
        }

        /// <summary>
        /// Splits the and convert to int.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="length">The length.</param>
        /// <param name="output">The output.</param>
        /// <returns>True if successfully converted; otherwise false.</returns>
        public static string[] SplitString(this string source, char separator, int lengthExpected)
        {
            if (string.IsNullOrEmpty(source)) return null;
            string[] values = source.Split(new char[] { separator });

            if (values != null &&
                values.Length >= lengthExpected)
            {
                return values;
            }
            return null;
        }

        /// <summary>
        /// Splits the and convert to int.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="length">The length.</param>
        /// <param name="output">The output.</param>
        /// <returns>True if successfully converted; otherwise false.</returns>
        public static bool SplitAndConvertToInt(this string source, int start, int length, ref string[] values, ref int output)
        {
            return SplitAndConvertToNumeric<int>(source, start, length, ref values, ref output);
        }

        /// <summary>
        /// Splits the and convert to int64.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="length">The length.</param>
        /// <param name="output">The output.</param>
        /// <returns>True if successfully converted; otherwise false.</returns>
        public static bool SplitAndConvertToInt64(this string source, int start, int length, ref string[] values, ref long output)
        {
            return SplitAndConvertToNumeric<long>(source, start, length, ref values, ref output);
        }

        /// <summary>
        /// Splits the and convert to int.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="length">The length.</param>
        /// <param name="output">The output.</param>
        /// <returns>True if successfully converted; otherwise false.</returns>
        public static bool SplitAndConvertToNumeric<T>(this string source, int start, int length, ref string[] values, ref T output)
            where T : struct
        {
            output = default(T);
            object result = default(T);
            if (string.IsNullOrEmpty(source)) return false;
            values = source.Split(new char[] { ',' });
            bool validValue = false;

            if (values != null &&
                length <= values.Length)
            {
                validValue = true;

                StringBuilder sb = new StringBuilder();
                for (int i = start; i < (start + length); i++)
                {
                    sb.Append(values[i].ConvertToHex(2));

                }

                switch (typeof(T).FullName)
                {
                    case "System.Int64":
                        result = TypeSystem.GetValueInt64(sb.ToString());
                        break;

                    default:
                        result = TypeSystem.GetValueInt(sb.ToString());
                        break;
                }
            }
            output = (T)result;
            return validValue;
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

        public static string GetStartupDirectory()
        {
#if !SILVERLIGHT
            return Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
#else
            return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
#endif
        }

        public static string NormalizeDirectory(string source)
        {
            if (!source.IsEmpty())
            {
                char ch = source[source.Length - 1];
                if (ch != Path.DirectorySeparatorChar &&
                    ch != Path.AltDirectorySeparatorChar &&
                    ch != Path.VolumeSeparatorChar)
                {
                    return source + Path.DirectorySeparatorChar;
                }
            }
            return source;
        }

        public static string GetRelativePath(this string fullPath, string partialPath)
        {
            try
            {
                partialPath = NormalizeDirectory(partialPath);
                return Uri.UnescapeDataString(
                    new Uri(partialPath).MakeRelativeUri(
                        new Uri(fullPath)).ToString());
            }
            catch
            {
                return fullPath;
            }
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
                try
                {
#if !SILVERLIGHT
                    rawData = Encoding.Default.GetBytes(inner.StackTrace);
#else
                    rawData = Encoding.Unicode.GetBytes(inner.StackTrace);
#endif
                }
                catch { }

                if (action != null) action(inner);
                inner = inner.InnerException;
            }
        }

        public static string GetAllExceptions(this Exception ex)
        {
            ModuleProc PROC = new ModuleProc("", "GetAllExceptions");
            StringBuilder sb = new StringBuilder();

            try
            {
                ex.IterateException(e =>
                {
                    sb.AppendLine(e.Message);
                    if (e.StackTrace != null)
                    {
                        sb.AppendLine("StackTrace Information");
                        sb.AppendLine(new String('-', 50));
                        sb.AppendLine(ex.StackTrace);
                        sb.AppendLine(new String('-', 50));
                    }
                    sb.AppendLine();
                });
            }
            catch { }

            return sb.ToString();
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

        /// <summary>
        /// Writes the specified object in xml object.
        /// </summary>
        /// <typeparam name="T">Type of the object</typeparam>
        /// <param name="obj">Object to be serialized.</param>
        /// <param name="stream">Output stream in which the serialized object stored.</param>
        /// <returns>Serialized object</returns>
        public static T WriteXmlObject<T>(T obj, Stream stream)
            where T : class, new()
        {
            T obj2 = obj;
            try
            {
                XmlSerializer formatter = new XmlSerializer(typeof(T));
                formatter.Serialize(stream, obj);
            }
            catch
            {
                obj2 = default(T);
            }
            return obj2;
        }

        public static string WriteXmlObjectToString<T>(T obj, Encoding encoding, XmlSerializerNamespaces namespaces)
            where T : class, new()
        {
            string obj2 = string.Empty;
            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    XmlSerializer formatter = new XmlSerializer(typeof(T));
                    formatter.Serialize(ms, obj, namespaces);
                    ms.Position = 0;
                    byte[] buffer = new byte[ms.Length];
                    ms.Read(buffer, 0, buffer.Length);
                    obj2 = encoding.GetString(buffer);
                }
            }
            catch
            {
                obj2 = string.Empty;
            }
            return obj2;
        }

        /// <summary>
        /// Reads the object from xml stream.
        /// </summary>
        /// <typeparam name="T">Type of the object</typeparam>
        /// <param name="stream">Input stream in which the object to be deserialized.</param>
        /// <returns>Deserialized object</returns>
        public static T ReadXmlObject<T>(Stream stream)
            where T : class, new()
        {
            T obj = default(T);
            try
            {
                XmlSerializer formatter = new XmlSerializer(typeof(T));
                obj = formatter.Deserialize(stream) as T;
            }
            catch
            {
                obj = default(T);
            }
            return obj;
        }

        /// <summary>
        /// Reads the object from xml stream.
        /// </summary>
        /// <typeparam name="T">Type of the object</typeparam>
        /// <param name="stream">Input stream in which the object to be deserialized.</param>
        /// <returns>Deserialized object</returns>
        public static T ReadXmlObject<T>(string source, string defaultNamespace)
            where T : class, new()
        {
            T obj = default(T);
            try
            {
                StringReader sr = new StringReader(source);
                XmlTextReader reader = new XmlTextReader(sr);
                XmlSerializer serializer = new XmlSerializer(typeof(T), defaultNamespace);
                obj = serializer.Deserialize(reader) as T;
            }
            catch (Exception ex)
            {
                Log.Exception(ex);
                obj = default(T);
            }
            return obj;
        }

        /// <summary>
        /// Reads the object from xml stream.
        /// </summary>
        /// <typeparam name="T">Type of the object</typeparam>
        /// <param name="stream">Input stream in which the object to be deserialized.</param>
        /// <returns>Deserialized object</returns>
        public static T ReadXmlObjectWithExtra<T>(Stream stream, params Type[] extraTypes)
            where T : class, new()
        {
            T obj = default(T);
            try
            {
                XmlSerializer formatter = new XmlSerializer(typeof(T), (Type[])extraTypes);
                obj = formatter.Deserialize(stream) as T;
            }
            catch
            {
                obj = default(T);
            }
            return obj;
        }

        /// <summary>
        /// Reads the object from xml stream.
        /// </summary>
        /// <typeparam name="T">Type of the object</typeparam>
        /// <param name="stream">Input stream in which the object to be deserialized.</param>
        /// <returns>Deserialized object</returns>
        public static T ReadXmlObjectNoPI<T>(Stream stream)
            where T : class, new()
        {
            T obj = default(T);
            try
            {
                stream.Position = 0;
                XmlSerializer formatter = new XmlSerializer(typeof(T));
                XmlReaderSettings settings = new XmlReaderSettings()
                {
                    IgnoreProcessingInstructions = true
                };
                XmlReader reader = XmlReader.Create(stream, settings);
                obj = formatter.Deserialize(reader) as T;
            }
            catch
            {
                obj = default(T);
            }
            return obj;
        }

        /// Writes the specified object in xml object.
        /// </summary>
        /// <typeparam name="T">Type of the object</typeparam>
        /// <param name="obj">Object to be serialized.</param>
        /// <param name="stream">Output stream in which the serialized object stored.</param>
        /// <returns>Serialized object</returns>
        public static T WriteDataContractObject<T>(T obj, Stream stream)
            where T : class, new()
        {
            T obj2 = obj;
            try
            {
                DataContractSerializer formatter = new DataContractSerializer(typeof(T));
                formatter.WriteObject(stream, obj);
            }
            catch
            {
                obj2 = default(T);
            }
            return obj2;
        }

        /// Writes the specified object in xml object.
        /// </summary>
        /// <typeparam name="T">Type of the object</typeparam>
        /// <param name="obj">Object to be serialized.</param>
        /// <param name="stream">Output stream in which the serialized object stored.</param>
        /// <returns>Serialized object</returns>
        public static T WriteDataContractObject<T>(T obj, Stream stream, IEnumerable<Type> knownTypes)
        //where T : class, new()
        {
            T obj2 = obj;
            try
            {
                DataContractSerializer formatter = null;
                if (knownTypes == null)
                    formatter = new DataContractSerializer(typeof(T));
                else
                    formatter = new DataContractSerializer(typeof(T), knownTypes);
                formatter.WriteObject(stream, obj);
            }
            catch
            {
                obj2 = default(T);
            }
            return obj2;
        }

        /// <summary>
        /// Reads the object from xml stream.
        /// </summary>
        /// <typeparam name="T">Type of the object</typeparam>
        /// <param name="stream">Input stream in which the object to be deserialized.</param>
        /// <returns>Deserialized object</returns>
        public static T ReadDataContractObject<T>(Stream stream)
            where T : class, new()
        {
            T obj = default(T);
            try
            {
                DataContractSerializer formatter = new DataContractSerializer(typeof(T));
                obj = formatter.ReadObject(stream) as T;
            }
            catch
            {
                obj = default(T);
            }
            return obj;
        }

        /// <summary>
        /// Reads the object from xml stream.
        /// </summary>
        /// <typeparam name="T">Type of the object</typeparam>
        /// <param name="stream">Input stream in which the object to be deserialized.</param>
        /// <returns>Deserialized object</returns>
        public static T ReadDataContractObject<T>(Stream stream, IEnumerable<Type> knownTypes)
        //where T : class, new()
        {
            T obj = default(T);
            try
            {
                DataContractSerializer formatter = new DataContractSerializer(typeof(T), knownTypes);
                obj = (T)formatter.ReadObject(stream);
            }
            catch
            {
                obj = default(T);
            }
            return obj;
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

        /// <summary>
        /// Safes the value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The source.</param>
        /// <returns>
        /// 	<c>true</c> if the specified source is valid; otherwise, <c>false</c>.
        /// </returns>
        public static T SafeValue<T>(this Nullable<T> source)
        where T : struct
        {
            if (IsValid<T>(source)) return source.Value;
            return default(T);
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
        /// Gets the field value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="row">The row.</param>
        /// <param name="columnName">Name of the column.</param>
        /// <returns>Retrieved value or default value.</returns>
        public static T GetFieldValue<T>(this DataRow row, string columnName)
        {
            try
            {
                return row.Field<T>(columnName);
            }
            catch { return default(T); }
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

        #region Network Related functions

        public static NetworkInterface GetActiveNetworkInterface()
        {
            return GetActiveNetworkInterfaces().FirstOrDefault();
        }

        public static NetworkInterface[] GetActiveNetworkInterfaces()
        {
            return (from n in NetworkInterface.GetAllNetworkInterfaces()
                    where
                        (n.OperationalStatus == OperationalStatus.Up &&
                            (n.NetworkInterfaceType == NetworkInterfaceType.Ethernet ||
                            n.NetworkInterfaceType == NetworkInterfaceType.FastEthernetFx ||
                            n.NetworkInterfaceType == NetworkInterfaceType.FastEthernetT ||
                            n.NetworkInterfaceType == NetworkInterfaceType.Ethernet3Megabit ||
                            n.NetworkInterfaceType == NetworkInterfaceType.GigabitEthernet ||
                            n.NetworkInterfaceType == NetworkInterfaceType.Wireless80211))
                    select n).ToArray();
        }

        /// <summary>
        /// Gets the ip address.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns>Gets the one of the ipaddress from the current system.</returns>
        public static IPAddress GetIpAddress(int index)
        {
            string hostName = string.Empty;
            return GetIpAddress(index, out hostName);
        }

        /// <summary>
        /// Gets the ip address.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="hostName">Name of the host.</param>
        /// <returns>
        /// Gets the one of the ipaddress from the current system.
        /// </returns>
        public static IPAddress GetIpAddress(int index, out string hostName)
        {
            ModuleProc PROC = new ModuleProc("Extensions", "GetIpAddress");
            IPAddress result = null;
            hostName = string.Empty;

            try
            {
                try
                {
                    hostName = Dns.GetHostName();
                }
                catch (SocketException)
                {
                    hostName = IPAddress.Loopback.ToString();
                }

                IPHostEntry hostEntry = Dns.GetHostEntry(hostName);
                if (hostEntry != null &&
                    hostEntry.AddressList != null)
                {
                    if (index >= 0 && index < hostEntry.AddressList.Length)
                    {
                        result = hostEntry.AddressList[index];
                    }
                    else
                    {
                        // find the IPV4 Address
                        var ipv4 = (from ip in hostEntry.AddressList.OfType<IPAddress>()
                                    where ip.AddressFamily == AddressFamily.InterNetwork
                                    select ip).FirstOrDefault();
                        if (ipv4 != null)
                        {
                            result = ipv4;
                        }
                        else
                        {
                            result = hostEntry.AddressList[0];
                        }
                    }
                }
            }
            catch (Exception)
            {
                result = null;
            }

            return result;
        }

        public static string GetIpAddressString()
        {
            return GetIpAddressString(-1);
        }

        public static string GetIpAddressString(int index)
        {
            IPAddress ipAddr = Extensions.GetIpAddress(index);
            if (ipAddr != null)
            {
                return ipAddr.ToString();
            }
            return string.Empty;
        }

        public static IPAddress ToIPAddress(this string address)
        {
            IPAddress ipAddress = null;
            if (IPAddress.TryParse(address, out ipAddress)) return ipAddress;
            return IPAddress.None;
        }

        #endregion

        #region Object Methods

        /// <summary>
        /// To string safe implementation.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>String value.</returns>
        public static string ToStringSafe(this object source)
        {
            if (source is string)
            {
                if (string.IsNullOrEmpty((string)source))
                    return string.Empty;
                else
                    return ((string)source);
            }
            else if (source is DateTime)
            {
                return ((DateTime)source).ToString("dd/MM/yyyy HH:mm:ss.fff");
            }

            if (source == null) return string.Empty;
            return source.ToString();
        }

        public static string ToXmlString(this DateTime value, XmlDateTimeSerializationMode mode)
        {
            try
            {
                if (mode == XmlDateTimeSerializationMode.Local)
                {
                    return XmlConvert.ToString(value, "yyyy-MM-ddTHH:mm:ss.fffffffzzzzzz");
                }
                else
                {
                    // for mode DateTimeSerializationMode.Roundtrip and DateTimeSerializationMode.Default
                    return XmlConvert.ToString(value, XmlDateTimeSerializationMode.RoundtripKind);
                }
            }
            catch
            {
                try
                {
                    return XmlConvert.ToString(value, XmlDateTimeSerializationMode.RoundtripKind);
                }
                catch
                {
                    return XmlConvert.ToString(value, "yyyy-MM-dd HH:mm:ss.fff");
                }
            }
        }

        #endregion

        #region Xml Linq Functions
        /// <summary>
        /// Gets the text value.
        /// </summary>
        /// <param name="elem">The elem.</param>
        /// <returns>String value.</returns>
        public static string GetTextValue(this XElement elem)
        {
            if (elem == null) return string.Empty;
            return elem.Value.ToStringSafe();
        }

        /// <summary>
        /// Gets the element value.
        /// </summary>
        /// <param name="elem">The elem.</param>
        /// <param name="name">The name.</param>
        /// <returns>String value.</returns>
        public static string GetElementValue(this XElement elem, string name)
        {
            XElement child = GetElement(elem, name);
            if (child != null)
            {
                return child.Value;
            }
            return string.Empty;
        }

        /// <summary>
        /// Gets the attribute value.
        /// </summary>
        /// <param name="elem">The elem.</param>
        /// <param name="name">The name.</param>
        /// <returns>String value.</returns>
        public static string GetAttributeValue(this XElement elem, string name)
        {
            XAttribute child = GetAttribute(elem, name);
            if (child != null)
            {
                return child.Value;
            }
            return string.Empty;
        }

        /// <summary>
        /// Gets the attribute value.
        /// </summary>
        /// <param name="elem">The elem.</param>
        /// <param name="name">The name.</param>
        /// <returns>String value.</returns>
        public static string GetAttributeValue(this XElement elem, string name, string namespaceName)
        {
            XAttribute child = GetAttribute(elem, name, namespaceName);
            if (child != null)
            {
                return child.Value;
            }
            return string.Empty;
        }

        /// <summary>
        /// Gets the text value bool.
        /// </summary>
        /// <param name="elem">The elem.</param>
        /// <returns>Booleger value.</returns>
        public static bool GetTextValueBool(this XElement elem)
        {
            return TypeSystem.GetValueBool(GetTextValue(elem));
        }

        /// <summary>
        /// Gets the element value bool.
        /// </summary>
        /// <param name="elem">The elem.</param>
        /// <param name="name">The name.</param>
        /// <returns>Booleger value.</returns>
        public static bool GetElementValueBool(this XElement elem, string name)
        {
            return TypeSystem.GetValueBool(GetElementValue(elem, name));
        }

        /// <summary>
        /// Gets the attribute value bool.
        /// </summary>
        /// <param name="elem">The elem.</param>
        /// <param name="name">The name.</param>
        /// <returns>Booleger value.</returns>
        public static bool GetAttributeValueBool(this XElement elem, string name)
        {
            return TypeSystem.GetValueBool(GetAttributeValue(elem, name));
        }

        /// <summary>
        /// Gets the text value int.
        /// </summary>
        /// <param name="elem">The elem.</param>
        /// <returns>Integer value.</returns>
        public static int GetTextValueInt(this XElement elem)
        {
            return TypeSystem.GetValueInt(GetTextValue(elem));
        }

        /// <summary>
        /// Gets the element value int.
        /// </summary>
        /// <param name="elem">The elem.</param>
        /// <param name="name">The name.</param>
        /// <returns>Integer value.</returns>
        public static int GetElementValueInt(this XElement elem, string name)
        {
            return TypeSystem.GetValueInt(GetElementValue(elem, name));
        }

        /// <summary>
        /// Gets the attribute value int.
        /// </summary>
        /// <param name="elem">The elem.</param>
        /// <param name="name">The name.</param>
        /// <returns>Integer value.</returns>
        public static int GetAttributeValueInt(this XElement elem, string name)
        {
            return TypeSystem.GetValueInt(GetAttributeValue(elem, name));
        }

        /// <summary>
        /// Gets the text value int64.
        /// </summary>
        /// <param name="elem">The elem.</param>
        /// <returns>Integer value.</returns>
        public static long GetTextValueInt64(this XElement elem)
        {
            return GetTextValueInt64(elem, 0);
        }

        /// <summary>
        /// Gets the text value int64.
        /// </summary>
        /// <param name="elem">The elem.</param>
        /// <returns>Integer value.</returns>
        public static long GetTextValueInt64(this XElement elem, long defaultValue)
        {
            string textValue = GetTextValue(elem);
            if (textValue.IsEmpty()) return defaultValue;
            return TypeSystem.GetValueInt64(textValue);
        }

        /// <summary>
        /// Gets the element value int64.
        /// </summary>
        /// <param name="elem">The elem.</param>
        /// <param name="name">The name.</param>
        /// <returns>Integer value.</returns>
        public static long GetElementValueInt64(this XElement elem, string name)
        {
            return TypeSystem.GetValueInt64(GetElementValue(elem, name));
        }

        /// <summary>
        /// Gets the attribute value int64.
        /// </summary>
        /// <param name="elem">The elem.</param>
        /// <param name="name">The name.</param>
        /// <returns>Integer value.</returns>
        public static long GetAttributeValueInt64(this XElement elem, string name)
        {
            return TypeSystem.GetValueInt64(GetAttributeValue(elem, name));
        }

        /// <summary>
        /// Gets the attribute value double.
        /// </summary>
        /// <param name="elem">The elem.</param>
        /// <param name="name">The name.</param>
        /// <returns>Integer value.</returns>
        public static double GetAttributeValueDouble(this XElement elem, string name)
        {
            return TypeSystem.GetValueDouble(GetAttributeValue(elem, name));
        }

        /// <summary>
        /// Gets the element value double.
        /// </summary>
        /// <param name="elem">The elem.</param>
        /// <param name="name">The name.</param>
        /// <returns>Double value.</returns>
        public static double GetElementValueDouble(this XElement elem, string name)
        {
            return TypeSystem.GetValueDouble(GetElementValue(elem, name));
        }

        /// <summary>
        /// Gets the text value date time.
        /// </summary>
        /// <param name="elem">The elem.</param>
        /// <returns>Integer value.</returns>
        public static DateTime GetTextValueDateTime(this XElement elem)
        {
            return TypeSystem.GetValueDateTime(GetTextValue(elem));
        }

        /// <summary>
        /// Gets the element value date time.
        /// </summary>
        /// <param name="elem">The elem.</param>
        /// <param name="name">The name.</param>
        /// <returns>Datetime value.</returns>
        public static DateTime GetElementValueDateTime(this XElement elem, string name)
        {
            return TypeSystem.GetValueDateTime(GetElementValue(elem, name));
        }

        /// <summary>
        /// Gets the element value date time.
        /// </summary>
        /// <param name="elem">The elem.</param>
        /// <param name="name">The name.</param>
        /// <returns>Datetime value.</returns>
        public static DateTime GetElementValueDateTimeAny(this XElement elem, string name)
        {
            return TypeSystem.GetValueDateTimeAny(GetElementValue(elem, name));
        }

        /// <summary>
        /// Gets the attribute value date time.
        /// </summary>
        /// <param name="elem">The elem.</param>
        /// <param name="name">The name.</param>
        /// <returns>Datetime value.</returns>
        public static DateTime GetAttributeValueDateTime(this XElement elem, string name)
        {
            return TypeSystem.GetValueDateTime(GetAttributeValue(elem, name));
        }

        /// <summary>
        /// Gets the attribute value date time.
        /// </summary>
        /// <param name="elem">The elem.</param>
        /// <param name="name">The name.</param>
        /// <returns>Datetime value.</returns>
        public static DateTime GetAttributeValueDateTimeAny(this XElement elem, string name)
        {
            return TypeSystem.GetValueDateTimeAny(GetAttributeValue(elem, name));
        }

        /// <summary>
        /// Gets the attribute.
        /// </summary>
        /// <param name="elem">The elem.</param>
        /// <param name="name">The name.</param>
        /// <returns>Attribute for this element.</returns>
        public static XAttribute GetAttribute(this XElement elem, string name)
        {
            try
            {
                XAttribute child = elem.Attribute(XName.Get(name));
                if (child != null)
                {
                    return child;
                }
            }
            catch { }
            return null;
        }

        /// <summary>
        /// Gets the attribute.
        /// </summary>
        /// <param name="elem">The elem.</param>
        /// <param name="name">The name.</param>
        /// <returns>Attribute for this element.</returns>
        public static XAttribute GetAttribute(this XElement elem, string name, string namespaceName)
        {
            try
            {
                XAttribute child = elem.Attribute(XName.Get(name, namespaceName));
                if (child != null)
                {
                    return child;
                }
            }
            catch { }
            return null;
        }

        /// <summary>
        /// Gets the attributes.
        /// </summary>
        /// <param name="elem">The elem.</param>
        /// <param name="name">The name.</param>
        /// <returns>Elements for this element.</returns>
        public static IEnumerable<XAttribute> GetAttributes(this XElement elem, string name)
        {
            try
            {
                if (elem == null) return null;
                IEnumerable<XAttribute> children = elem.Attributes(XName.Get(name));
                if (children != null)
                {
                    return children;
                }
            }
            catch { }
            return null;
        }

        /// <summary>
        /// Gets the element.
        /// </summary>
        /// <param name="elem">The elem.</param>
        /// <param name="name">The name.</param>
        /// <returns>Child Element for this element.</returns>
        public static XElement GetElement(this XElement elem, string name)
        {
            try
            {
                XElement child = elem.Element(XName.Get(name));
                if (child != null)
                {
                    return child;
                }
            }
            catch { }
            return null;
        }

        /// <summary>
        /// Gets the elements.
        /// </summary>
        /// <param name="elem">The elem.</param>
        /// <param name="name">The name.</param>
        /// <returns>Elements for this element.</returns>
        public static IEnumerable<XElement> GetElements(this XElement elem, string name)
        {
            try
            {
                if (elem == null) return null;
                IEnumerable<XElement> children = elem.Elements(XName.Get(name));
                if (children != null)
                {
                    return children;
                }
            }
            catch { }
            return null;
        }
        #endregion

        #region Initialize Thread Functions
        /// <summary>
        /// Initializes the thread func.
        /// </summary>
        /// <param name="th">The th.</param>
        /// <param name="initializeStatus">The initialize status.</param>
        /// <param name="lockObject">The lock object.</param>
        /// <param name="threadFunc">The thread func.</param>
        public static void InitializeThreadFunc(ref Thread th, ref InitializeStatus initializeStatus,
            object lockObject, ThreadStart threadFunc, string threadName)
        {
            ModuleProc PROC = new ModuleProc("Extensions", "InitializeThreadFunc");

            try
            {
                if (initializeStatus == InitializeStatus.Uninitialized)
                {
                    lock (lockObject)
                    {
                        if (initializeStatus == InitializeStatus.Uninitialized)
                        {
                            initializeStatus = InitializeStatus.Initialized;
                            th = Extensions.CreateThreadAndStart(threadFunc, threadName);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Initializes the thread func.
        /// </summary>
        /// <param name="th">The th.</param>
        /// <param name="initializeStatus">The initialize status.</param>
        /// <param name="lockObject">The lock object.</param>
        /// <param name="threadFunc">The thread func.</param>
        public static void InitializeThreadFunc(ref Thread th, ref InitializeStatus initializeStatus,
            object lockObject, ParameterizedThreadStart threadFunc, string threadName)
        {
            ModuleProc PROC = new ModuleProc("Extensions", "InitializeThreadFunc");

            try
            {
                if (initializeStatus == InitializeStatus.Uninitialized)
                {
                    lock (lockObject)
                    {
                        if (initializeStatus == InitializeStatus.Uninitialized)
                        {
                            initializeStatus = InitializeStatus.Initialized;
                            th = Extensions.CreateThreadAndStart(threadFunc, threadName);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Initializes the thread func.
        /// </summary>
        /// <param name="th">The th.</param>
        /// <param name="lockObject">The lock object.</param>
        /// <param name="threadFunc">The thread func.</param>
        /// <param name="threadName">Name of the thread.</param>
        public static void InitializeThreadFunc(ref Thread th, object lockObject, Func<ThreadStart> threadFunc, string threadName)
        {
            ModuleProc PROC = new ModuleProc("Extensions", "InitializeThreadFunc");

            try
            {
                if (th == null)
                {
                    lock (lockObject)
                    {
                        if (th == null)
                        {
                            th = Extensions.CreateThreadAndStart(threadFunc(), threadName);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        /// <summary>
        /// Initializes the thread func.
        /// </summary>
        /// <param name="th">The th.</param>
        /// <param name="lockObject">The lock object.</param>
        /// <param name="threadFunc">The thread func.</param>
        /// <param name="threadName">Name of the thread.</param>
        public static void InitializeThreadFunc(ref Thread th, object lockObject, Func<ParameterizedThreadStart> threadFunc, string threadName)
        {
            ModuleProc PROC = new ModuleProc("Extensions", "InitializeThreadFunc");

            try
            {
                if (th == null)
                {
                    lock (lockObject)
                    {
                        if (th == null)
                        {
                            th = Extensions.CreateThreadAndStart(threadFunc(), threadName);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }
        #endregion

        #region Sequencial Loops
        public static void ForEachItem<T>(this IEnumerable<T> source, Action<T> doWork)
        {
            foreach (T item in source)
            {
                if (doWork != null)
                {
                    doWork(item);
                }
            }
        }
        #endregion

        #region Date/Time Methods

        /// <summary>
        /// Gets the universal date format.
        /// </summary>
        /// <param name="dateTime">The date time.</param>
        /// <returns></returns>
        public static string GetUTCDateString(this DateTime dateTime)
        {
            return String.Format(Thread.CurrentThread.CurrentUICulture, "{0:d}", dateTime);
        }

        /// <summary>
        /// Gets the universal date format.
        /// </summary>
        /// <param name="dateTime">The date time.</param>
        /// <returns></returns>
        public static string GetUTCDateString(this DateTime dateTime, string cultureName)
        {
            return GetUTCDateString(dateTime, new CultureInfo(cultureName));
        }

        /// <summary>
        /// Gets the universal date format.
        /// </summary>
        /// <param name="dateTime">The date time.</param>
        /// <returns></returns>
        public static string GetUTCDateString(this DateTime dateTime, CultureInfo cultureInfo)
        {
            return String.Format(cultureInfo, "{0:d}", dateTime);
        }

        public static string ToFullString(this DateTime dateTime)
        {
            return dateTime.ToString("dd/MM/yyyy HH:mm:ss.fff");
        }

        #endregion

        #region Config Settings
        public static string GetAppSettingValue(string keyName, string defaultValue)
        {
            try
            {
                if (ConfigurationManager.AppSettings.AllKeys.Contains(keyName))
                    return ConfigurationManager.AppSettings[keyName];
                else
                    return defaultValue;
            }
            catch { return defaultValue; }
        }

        public static int GetAppSettingValueInt(string keyName, int defaultValue)
        {
            return TypeSystem.GetValueInt(GetAppSettingValue(keyName, defaultValue.ToString()));
        }

        public static double GetAppSettingValueDouble(string keyName, double defaultValue)
        {
            return TypeSystem.GetValueDouble(GetAppSettingValue(keyName, defaultValue.ToString()));
        }

        public static bool GetAppSettingValueBool(string keyName, bool defaultValue)
        {
            return TypeSystem.GetValueBoolSimple(GetAppSettingValue(keyName, defaultValue.ToString()));
        }
        #endregion

        #region Screen Capture
        public static void SaveScreenshot(Rectangle bounds, Action<Bitmap> doWork)
        {
            ModuleProc PROC = new ModuleProc("Extensions", "SaveScreenshot");

            try
            {
                using (Bitmap bitmap = new Bitmap(bounds.Width, bounds.Height))
                {
                    using (Graphics g = Graphics.FromImage(bitmap))
                    {
                        g.CopyFromScreen(new Point(bounds.Left, bounds.Top), Point.Empty, bounds.Size);
                    }
                    doWork(bitmap);
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        public static void SaveScreenshot(Form ownerForm, Action<Bitmap> doWork)
        {
            ModuleProc PROC = new ModuleProc("Extensions", "SaveScreenshot");

            try
            {
                using (Bitmap bitmap = new Bitmap(ownerForm.Width, ownerForm.Height))
                {
                    ownerForm.DrawToBitmap(bitmap, new Rectangle(0, 0, bitmap.Width, bitmap.Height));
                    doWork(bitmap);
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        public static void SaveScreenshotToFile(Rectangle bounds, string fileName)
        {
            SaveScreenshot(bounds, (b) => { b.Save(fileName, ImageFormat.Png); });
        }

        public static void SaveScreenshotToStream(Rectangle bounds, Stream stream)
        {
            SaveScreenshot(bounds, (b) => { b.Save(stream, ImageFormat.Png); });
        }

        public static void SaveScreenshotToStream(Form ownerForm, Stream stream)
        {
            SaveScreenshot(ownerForm, (b) => { b.Save(stream, ImageFormat.Png); });
        }
        #endregion

        #region Numeric Byte methods

        /// <summary>
        /// Gets the int8.
        /// </summary>
        /// <param name="bytes">The bytes.</param>
        /// <param name="index">The index.</param>
        /// <param name="count">The count.</param>
        /// <returns>Byte value from the bytes.</returns>
        public static sbyte GetInt8(this byte[] bytes, int index, int count)
        {
            return TypeSystem.GetValueSByte(GetString(bytes, index, count));
        }

        /// <summary>
        /// Gets the unsigned int8.
        /// </summary>
        /// <param name="bytes">The bytes.</param>
        /// <param name="index">The index.</param>
        /// <param name="count">The count.</param>
        /// <returns>Byte value from the bytes.</returns>
        public static byte GetUInt8(this byte[] bytes, int index, int count)
        {
            return TypeSystem.GetValueByte(GetString(bytes, index, count));
        }

        /// <summary>
        /// Gets the int16.
        /// </summary>
        /// <param name="bytes">The bytes.</param>
        /// <param name="index">The index.</param>
        /// <param name="count">The count.</param>
        /// <returns>Short value from the bytes.</returns>
        public static short GetInt16(this byte[] bytes, int index, int count)
        {
            return TypeSystem.GetValueShort(GetString(bytes, index, count));
        }

        /// <summary>
        /// Gets the unsigned int16.
        /// </summary>
        /// <param name="bytes">The bytes.</param>
        /// <param name="index">The index.</param>
        /// <param name="count">The count.</param>
        /// <returns>Unsigned Short value from the bytes.</returns>
        public static ushort GetUInt16(this byte[] bytes, int index, int count)
        {
            return TypeSystem.GetValueUShort(GetString(bytes, index, count));
        }

        ///// <summary>
        ///// Gets the single.
        ///// </summary>
        ///// <param name="bytes">The bytes.</param>
        ///// <param name="index">The index.</param>
        ///// <param name="count">The count.</param>
        ///// <returns>Single value from the bytes.</returns>
        //public static float GetSingle(this byte[] bytes, int index, int count)
        //{
        //    return TypeSystem.GetValueSingle(GetString(bytes, index, count));
        //}

        /// <summary>
        /// Gets the double.
        /// </summary>
        /// <param name="bytes">The bytes.</param>
        /// <param name="index">The index.</param>
        /// <param name="count">The count.</param>
        /// <returns>Double value from the bytes.</returns>
        public static double GetDouble(this byte[] bytes, int index, int count)
        {
            return TypeSystem.GetValueDouble(GetString(bytes, index, count));
        }

        /// <summary>
        /// Gets the decimal.
        /// </summary>
        /// <param name="bytes">The bytes.</param>
        /// <param name="index">The index.</param>
        /// <param name="count">The count.</param>
        /// <returns>Decimal value from the bytes.</returns>
        public static decimal GetDecimal(this byte[] bytes, int index, int count)
        {
            return TypeSystem.GetValueDecimal(GetString(bytes, index, count));
        }

        /// <summary>
        /// Gets the bool.
        /// </summary>
        /// <param name="bytes">The bytes.</param>
        /// <param name="index">The index.</param>
        /// <param name="count">The count.</param>
        /// <returns>Bool value from the bytes.</returns>
        public static bool GetBool(this byte[] bytes, int index, int count)
        {
            string value = GetString(bytes, index, count);
            return TypeSystem.GetValueBoolSimple(value);
        }

        /// <summary>
        /// Gets the bool.
        /// </summary>
        /// <param name="bytes">The bytes.</param>
        /// <param name="index">The index.</param>
        /// <param name="count">The count.</param>
        /// <returns>Bool value from the bytes.</returns>
        public static bool GetBoolYesOrNo(this byte[] bytes, int index, int count)
        {
            string value = GetString(bytes, index, count);
            return (value == "Y");
        }

        /// <summary>
        /// Gets the bool.
        /// </summary>
        /// <param name="bytes">The bytes.</param>
        /// <param name="index">The index.</param>
        /// <param name="count">The count.</param>
        /// <returns>Bool value from the bytes.</returns>
        public static bool GetBool1Or0(this byte[] bytes, int index, int count)
        {
            string value = GetString(bytes, index, count);
            return (value == "1");
        }

        /// <summary>
        /// Gets the bool.
        /// </summary>
        /// <param name="bytes">The bytes.</param>
        /// <param name="index">The index.</param>
        /// <param name="count">The count.</param>
        /// <returns>Bool value from the bytes.</returns>
        public static bool GetBool2(this byte[] bytes, int index, int count)
        {
            return TypeSystem.GetValueBool(GetString(bytes, index, count));
        }

        /// <summary>
        /// Gets the date time.
        /// </summary>
        /// <param name="bytes">The bytes.</param>
        /// <param name="index">The index.</param>
        /// <param name="count">The count.</param>
        /// <returns>DateTime value from the bytes.</returns>
        public static DateTime GetDateTimeRaw(this byte[] bytes, int index, int count)
        {
            return TypeSystem.GetValueDateTime(GetString(bytes, index, count));
        }

        /// <summary>
        /// Gets the date time.
        /// </summary>
        /// <param name="bytes">The bytes.</param>
        /// <param name="index">The index.</param>
        /// <param name="count">The count.</param>
        /// <param name="format">The format.</param>
        /// <returns>DateTime value from the bytes.</returns>
        public static DateTime GetDateTime(this byte[] bytes, int index, int count, string format)
        {
            DateTime returnValue = DateTime.MinValue;

            try
            {
                switch (format)
                {
                    case "yyyyMMdd":
                    case "yyyyMMddHHmmss":
                        {
                            int year = bytes.GetInt32(index + 0, 4);
                            int month = bytes.GetInt32(index + 4, 2);
                            int day = bytes.GetInt32(index + 6, 2);
                            if (year <= 0) year = 1;
                            if (month <= 0) month = 1;
                            if (day <= 0) day = 1;

                            switch (format)
                            {
                                case "yyyyMMdd":
                                    returnValue = new DateTime(year, month, day);
                                    break;

                                case "yyyyMMddHHmmss":
                                    {
                                        returnValue = new DateTime(year, month, day,
                                                            bytes.GetInt32(index + 8, 2), bytes.GetInt32(index + 10, 2), bytes.GetInt32(index + 12, 2));
                                    }
                                    break;

                            }
                        }
                        break;
                    default:
                        returnValue = GetDateTimeWithoutSeprator(bytes, index, count);
                        break;
                }
            }
            catch (Exception ex)
            {
                returnValue = DateTime.MinValue;
            }
            return returnValue;
        }

        /// <summary>
        /// Gets the time span.
        /// </summary>
        /// <param name="bytes">The bytes.</param>
        /// <param name="index">The index.</param>
        /// <param name="count">The count.</param>
        /// <returns>Timespan value from the bytes.</returns>
        public static TimeSpan GetTimeSpan(this byte[] bytes, int index, int count)
        {
            return TypeSystem.GetValueTimeSpan(GetString(bytes, index, count));
        }

        /// <summary>
        /// Gets the time span.
        /// </summary>
        /// <param name="bytes">The bytes.</param>
        /// <param name="index">The index.</param>
        /// <param name="count">The count.</param>
        /// <param name="format">The format.</param>
        /// <returns>Timespan value from the bytes.</returns>
        public static TimeSpan GetTimeSpan(this byte[] bytes, int index, int count, string format)
        {
            TimeSpan returnValue = TimeSpan.MinValue;

            try
            {
                switch (format)
                {
                    case "HHmm":
                        returnValue = new TimeSpan(bytes.GetInt32(index + 0, 2), bytes.GetInt32(index + 2, 2), 0);
                        break;

                    case "HHmmss":
                        returnValue = new TimeSpan(bytes.GetInt32(index + 0, 2), bytes.GetInt32(index + 2, 2), bytes.GetInt32(index + 4, 2));
                        break;

                    default:
                        returnValue = GetTimeSpan(bytes, index, count);
                        break;
                }
            }
            catch (Exception ex)
            {
                returnValue = TimeSpan.MinValue;
            }
            return returnValue;
        }

        #endregion

        #region String Builder methods

        /// <summary>
        /// Appends the formatted string.
        /// </summary>
        /// <param name="sb">The string builder.</param>
        /// <param name="data">The data.</param>
        /// <param name="index">The index.</param>
        /// <param name="size">The size.</param>
        public static void AppendFormattedString(this StringBuilder sb, string data, int index, int size)
        {
            AppendFormattedString(sb, data, index, size, '0');
        }

        /// <summary>
        /// Appends the formatted string (pad right).
        /// </summary>
        /// <param name="sb">The string builder.</param>
        /// <param name="data">The data.</param>
        /// <param name="index">The index.</param>
        /// <param name="size">The size.</param>
        public static void AppendFormattedStringRight(this StringBuilder sb, string data, int index, int size)
        {
            AppendFormattedStringRight(sb, data, index, size, '0');
        }

        /// <summary>
        /// Appends the formatted date string.
        /// </summary>
        /// <param name="sb">The sb.</param>
        /// <param name="data">The data.</param>
        /// <param name="index">The index.</param>
        /// <param name="size">The size.</param>
        public static void AppendFormattedDateString(this StringBuilder sb, DateTime data, int index, int size)
        {
            AppendFormattedString(sb, data.ToString("yyyyMMdd"), index, size, '0');
        }

        /// <summary>
        /// Appends the formatted date string.
        /// </summary>
        /// <param name="sb">The sb.</param>
        /// <param name="data">The data.</param>
        /// <param name="index">The index.</param>
        /// <param name="size">The size.</param>
        public static void AppendFormattedDateString(this StringBuilder sb, DateTime data, int index, int size, string format)
        {
            AppendFormattedString(sb, data.ToString(format), index, size, '0');
        }

        /// <summary>
        /// Appends the formatted time string.
        /// </summary>
        /// <param name="sb">The sb.</param>
        /// <param name="data">The data.</param>
        /// <param name="index">The index.</param>
        /// <param name="size">The size.</param>
        public static void AppendFormattedTimeString(this StringBuilder sb, TimeSpan data, int index, int size)
        {
            AppendFormattedString(sb, string.Format("{0:D2}{1:D2}{2:D2}", data.Hours, data.Minutes, data.Seconds), index, size, '0');
        }

        /// <summary>
        /// Appends the formatted time string.
        /// </summary>
        /// <param name="sb">The sb.</param>
        /// <param name="data">The data.</param>
        /// <param name="index">The index.</param>
        /// <param name="size">The size.</param>
        public static void AppendFormattedTimeString(this StringBuilder sb, TimeSpan data, int index, int size, string format)
        {
            if (format == "HHmmss")
            {
                AppendFormattedString(sb, string.Format("{0:D2}{1:D2}{2:D2}", data.Hours, data.Minutes, data.Seconds), index, size, '0');
            }
            else
            {
                AppendFormattedString(sb, string.Format("{0:D2}{1:D2}", data.Hours, data.Minutes), index, size, '0');
            }
        }

        /// <summary>
        /// Appends the formatted string.
        /// </summary>
        /// <param name="sb">The string builder.</param>
        /// <param name="data">The data.</param>
        /// <param name="index">The index.</param>
        /// <param name="size">The size.</param>
        /// <param name="paddingChar">The padding char.</param>
        public static void AppendFormattedString(this StringBuilder sb, string data, int index, int size, char paddingChar)
        {
            if (data.IsEmpty()) data = "";
            sb.Append(data.PadLeft(size, paddingChar).Substring(0, size));
        }

        /// <summary>
        /// Appends the formatted string (pad right).
        /// </summary>
        /// <param name="sb">The string builder.</param>
        /// <param name="data">The data.</param>
        /// <param name="index">The index.</param>
        /// <param name="size">The size.</param>
        /// <param name="paddingChar">The padding char.</param>
        public static void AppendFormattedStringRight(this StringBuilder sb, string data, int index, int size, char paddingChar)
        {
            if (data.IsEmpty()) data = "";
            sb.Append(data.PadRight(size, paddingChar).Substring(0, size));
        }

        #endregion

        #region String Methods (Other)
        /// <summary>
        /// Gets the string left trim.
        /// </summary>
        /// <param name="bytes">The bytes.</param>
        /// <param name="index">The index.</param>
        /// <param name="count">The count.</param>
        /// <param name="paddingChar">The padding char.</param>
        /// <returns>String value from the bytes.</returns>
        public static string GetStringLeftTrim(this byte[] bytes, int index, int count, char paddingChar)
        {
            return GetString(bytes, index, count).TrimStart(new char[] { paddingChar });
        }

        /// <summary>
        /// Gets the string right trim.
        /// </summary>
        /// <param name="bytes">The bytes.</param>
        /// <param name="index">The index.</param>
        /// <param name="count">The count.</param>
        /// <param name="paddingChar">The padding char.</param>
        /// <returns>String value from the bytes.</returns>
        public static string GetStringRightTrim(this byte[] bytes, int index, int count, char paddingChar)
        {
            return GetString(bytes, index, count).TrimEnd(new char[] { paddingChar });
        }

        /// <summary>
        /// Gets the string left trim.
        /// </summary>
        /// <param name="bytes">The bytes.</param>
        /// <param name="index">The index.</param>
        /// <param name="count">The count.</param>
        /// <returns>String value from the bytes.</returns>
        public static string GetStringLeftTrim(this byte[] bytes, int index, int count)
        {
            return GetStringLeftTrim(bytes, index, count, '0');
        }

        /// <summary>
        /// Gets the string right trim.
        /// </summary>
        /// <param name="bytes">The bytes.</param>
        /// <param name="index">The index.</param>
        /// <param name="count">The count.</param>
        /// <returns>String value from the bytes.</returns>
        public static string GetStringRightTrim(this byte[] bytes, int index, int count)
        {
            return GetStringRightTrim(bytes, index, count, '0');
        }

        /// <summary>
        /// Gets the string left trim space.
        /// </summary>
        /// <param name="bytes">The bytes.</param>
        /// <param name="index">The index.</param>
        /// <param name="count">The count.</param>
        /// <returns>String value from the bytes.</returns>
        public static string GetStringLeftTrimSpace(this byte[] bytes, int index, int count)
        {
            return GetStringLeftTrim(bytes, index, count, ' ');
        }

        /// <summary>
        /// Gets the string right trim space.
        /// </summary>
        /// <param name="bytes">The bytes.</param>
        /// <param name="index">The index.</param>
        /// <param name="count">The count.</param>
        /// <returns>String value from the bytes.</returns>
        public static string GetStringRightTrimSpace(this byte[] bytes, int index, int count)
        {
            return GetStringRightTrim(bytes, index, count, ' ');
        }

        /// <summary>
        /// Gets the string right trim space.
        /// </summary>
        /// <param name="bytes">The bytes.</param>
        /// <param name="index">The index.</param>
        /// <param name="count">The count.</param>
        /// <returns>String value from the bytes.</returns>
        public static string GetStringRightTrimTilde(this byte[] bytes, int index, int count)
        {
            return GetStringRightTrim(bytes, index, count, '~');
        }
        #endregion

        #region Collection Methods

        public static int GetCollectionCount<T>(this System.Collections.IEnumerable enuerable)
        {
            if (enuerable is ICollection<T>)
            {
                return ((ICollection<T>)enuerable).Count;
            }
            else if (enuerable is System.Collections.ICollection)
            {
                return ((System.Collections.ICollection)enuerable).Count;
            }

            return 0;
        }

        public static int GetCollectionCount(this System.Collections.IEnumerable enuerable, Type genericType)
        {
            if (enuerable is System.Collections.ICollection)
            {
                return ((System.Collections.ICollection)enuerable).Count;
            }
            else
            {
                Type genericType2 = typeof(ICollection<>).MakeGenericType(genericType);
                if (genericType2.IsAssignableFrom(enuerable.GetType()))
                {

                }
            }

            return 0;
        }

        public static object[,] CreateArray(int rows, int columns, int lboundX, int lboundY)
        {
            int[] lowerBounds = new int[] { lboundX, lboundY };
            int[] lengths = new int[] { rows, columns };
            return (object[,])Array.CreateInstance(typeof(object), lengths, lowerBounds);
        }

        public static V AddOrGet<K, V>(this IDictionary<K, V> collection, K key, Func<V> onCreate)
        {
            ModuleProc PROC = new ModuleProc("Extensions", "AddOrGet");
            V result = default(V);

            try
            {
                if (!collection.ContainsKey(key))
                {
                    if (onCreate != null)
                    {
                        result = onCreate();
                        collection.Add(key, result);
                    }
                }
                else
                {
                    result = collection[key];
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }

            return result;
        }

        public static V GetIfExists<K, V>(this IDictionary<K, V> collection, K key)
        {
            ModuleProc PROC = new ModuleProc("Extensions", "AddOrGet");
            V result = default(V);

            try
            {
                if (collection.ContainsKey(key))
                {
                    result = collection[key];
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }

            return result;
        }

        public static V AddOrUpdate2<K, V>(this IDictionary<K, V> collection, K key, V value)
        {
            ModuleProc PROC = new ModuleProc("Extensions", "AddOrUpdate2");

            try
            {
                if (!collection.ContainsKey(key))
                {
                    collection.Add(key, value);
                }
                else
                {
                    collection[key] = value;
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }

            return value;
        }
        #endregion

        #region Hookup Unhandled Exceptions
        private static bool _HookupUnhandledExceptions
        {
            get { return GetAppSettingValueBool("HookupUnhandledExceptions", false); }
        }

        public static bool UseTaskInsteadOfThread
        {
            get { return _useTaskInsteadOfThread; }
        }

        public static void HookupUnhandledConsoleAppExceptions()
        {
            HookupUnhandledExceptions();
        }

        public static void HookupUnhandledWin32AppExceptions()
        {
            HookupUnhandledExceptions();
        }

        public static void HookupUnhandledWpfAppExceptions()
        {
            HookupUnhandledExceptions();
        }

        public static void HookupUnhandledExceptions()
        {
            if (_HookupUnhandledExceptions)
            {
                AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
                if (GetAppSettingValueBool("HookupFirstChanceExceptions", false))
                {
                    AppDomain.CurrentDomain.FirstChanceException += CurrentDomain_FirstChanceException;
                }
            }
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Log.Info("!!! CurrentDomain_UnhandledException (START)");
            Log.Info("!!! CLR is terminating : " + e.IsTerminating.ToString());
            Exception ex = e.ExceptionObject as Exception;
            if (ex != null)
            {
                Log.Exception(ex);
            }
            Log.Info("!!! CurrentDomain_UnhandledException (END)");
            Thread.Sleep(10000);
        }

        private static void CurrentDomain_FirstChanceException(object sender, System.Runtime.ExceptionServices.FirstChanceExceptionEventArgs e)
        {
            Log.Info("!!! CurrentDomain_FirstChanceException (START)");
            Log.Exception(e.Exception);
            Log.Info("!!! CurrentDomain_FirstChanceException (END)");
        }
        #endregion
    }

    #region Singleton Helper
    public abstract class SingletonHelperBase<T>
        where T : class
    {
        protected static object _currentLock = new object();
        protected static Lazy<T> _createFactory = null;
        protected static Func<T> _createFunc = null;

        public SingletonHelperBase(Lazy<T> createFactory)
        {
            _createFactory = createFactory;
        }

        public SingletonHelperBase(Func<T> createFunc)
        {
            _createFunc = createFunc;
        }

        public abstract T Current { get; }
    }

    /// <summary>
    /// Singleton Helper (Static)
    /// </summary>
    /// <typeparam name="T">Type of the object to be created</typeparam>
    public class SingletonHelper<T>
        : SingletonHelperBase<T>
        where T : class
    {
        private static T _current = default(T);

        public SingletonHelper(Lazy<T> createFactory)
            : base(createFactory) { }

        public SingletonHelper(Func<T> createFunc)
            : base(createFunc) { }

        public override T Current
        {
            get
            {
                if (_current == null)
                {
                    lock (_currentLock)
                    {
                        if (_current == null)
                        {
                            if (_createFactory != null)
                                _current = _createFactory.Value;
                            else if (_createFunc != null)
                                _current = _createFunc();
                        }
                    }
                }
                return _current;
            }
        }
    }

    /// <summary>
    /// Singleton Helper (Thread Static)
    /// </summary>
    /// <typeparam name="T">Type of the object to be created</typeparam>
    public class SingletonThreadHelper<T>
        : SingletonHelperBase<T>
        where T : class
    {
        [ThreadStatic]
        private static T _current;

        public SingletonThreadHelper(Lazy<T> createFactory)
            : base(createFactory) { }

        public SingletonThreadHelper(Func<T> createFunc)
            : base(createFunc) { }

        public override T Current
        {
            get
            {
                if (_current == null)
                {
                    lock (_currentLock)
                    {
                        if (_current == null)
                        {
                            if (_createFactory != null)
                                _current = _createFactory.Value;
                            else if (_createFunc != null)
                                _current = _createFunc();
                        }
                    }
                }
                return _current;
            }
        }
    }
    #endregion
}
