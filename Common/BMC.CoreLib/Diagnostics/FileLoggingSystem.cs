/* ================================================================================= 
 * Purpose		:	File Logging System Class
 * File Name	:   FileLoggingSystem.cs
 * Author		:	A.Vinod Kumar
 * Created  	:	13/12/2010
 * ================================================================================= 
 * Revision History :
 * ================================================================================= 
 * 13/12/2010		A.Vinod Kumar    Initial Version
 * ===============================================================================*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using BMC.CoreLib.Concurrent;
using System.Threading;

namespace BMC.CoreLib.Diagnostics
{
    /// <summary>
    /// File Logging System Class
    /// </summary>
    [Serializable]
    public class FileLoggingSystem
        : LoggingSystemBase
    {
        /// <summary>
        /// Default Maximum File Size
        /// </summary>
        [NonSerialized]
        public const int DEFAULT_MAX_FILE_SIZE = (5 * 1024); // default 5 KB  

        /// <summary>
        /// Occurs when [file moved].
        /// </summary>
        public event FileMovedEventHandler FileMoved = null;

        [NonSerialized]
        private object _lockFile = new object();

        [NonSerialized]
        private int _isShutDown = 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileLoggingSystem"/> class.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="mode">The mode.</param>
        public FileLoggingSystem(string source, QueueProcessMode mode)
            : this(source, false, DEFAULT_MAX_FILE_SIZE, mode) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="FileLoggingSystem"/> class.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="maxFileSize">Size of the max file.</param>
        /// <param name="mode">The mode.</param>
        public FileLoggingSystem(string source, int maxFileSize, QueueProcessMode mode)
            : this(source, false, maxFileSize, mode) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="FileLoggingSystem"/> class.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="allowMultipleWriters">if set to <c>true</c> [allow multiple writers].</param>
        /// <param name="maxFileSize">Size of the max file.</param>
        /// <param name="mode">The mode.</param>
        public FileLoggingSystem(string source, bool allowMultipleWriters, int maxFileSize, QueueProcessMode mode)
            : base(source, mode,
            (mode == QueueProcessMode.Thread ? ExecutorServiceFactory.CreateExecutorService() : null))
        {
            if (Path.GetFileName(source).IsEmpty())
                throw new ArgumentNullException("File Name");

            if (mode == QueueProcessMode.Thread)
            {
                AppDomain.CurrentDomain.ProcessExit += CurrentDomain_ProcessExit;
                AppDomain.CurrentDomain.DomainUnload += CurrentDomain_ProcessExit;
                ShutdownHelper.ShutdownInitiated += CurrentDomain_ProcessExit;
            }

            this.MaxFileSize = maxFileSize;
            this.AllowMultipleWriters = allowMultipleWriters;
        }

        void CurrentDomain_ProcessExit(object sender, EventArgs e)
        {
            if (Interlocked.CompareExchange(ref _isShutDown, 1, 0) == 0)
            {
                this.ExecutorService.AwaitTermination(new TimeSpan(0, 2, 0));
            }
        }

        /// <summary>
        /// Processes the queue item.
        /// </summary>
        /// <param name="item">The item.</param>
        protected override void ProcessQueueItem(LogItem item)
        {
            // Ensure the file in safe range and write the contents
            lock (_lockFile)
            {
                // file in safe range
                try
                {
                    FileInfo fileInfo = new FileInfo(this.Source);
                    if (fileInfo.Exists)
                    {
                        if (fileInfo.Length >= this.MaxFileSize)
                        {
                            string newFileName = String.Format(@"{0}\{1} {2:dd-MMM-yyyy HH.mm.ss.fff}.txt",
                                fileInfo.DirectoryName, Path.GetFileNameWithoutExtension(fileInfo.FullName), DateTime.Now);

                            try
                            {
                                File.Move(fileInfo.FullName, newFileName);
                            }
                            finally
                            {
                                this.OnFileMoved(this.Source, newFileName);
                            }
                        }
                    }

                }
                catch (Exception ex)
                {
                    EventLogExceptionAdapter.WriteException(ex);
                }

                // actual writing
                try
                {
                    using (StreamWriter streamWriter = new StreamWriter(File.Open(this.Source, FileMode.Append, FileAccess.Write, FileShare.ReadWrite)))
                    {
                        try
                        {
                            streamWriter.WriteLine(item.Message);
                        }
                        catch (Exception ex)
                        {
                            EventLogExceptionAdapter.WriteException(ex);
                        }
                    }
                }
                catch (Exception ex)
                {
                    EventLogExceptionAdapter.WriteException(ex);
                }
            }
        }

        /// <summary>
        /// Gets or sets the size of the max file.
        /// </summary>
        /// <value>The size of the max file.</value>
        private int _maxFileSize = 0;

        public int MaxFileSize
        {
            get { return _maxFileSize; }
            set
            {
                if (value <= 0) value = DEFAULT_MAX_FILE_SIZE;
                _maxFileSize = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [allow multiple writers].
        /// </summary>
        /// <value>
        /// 	<c>true</c> if [allow multiple writers]; otherwise, <c>false</c>.
        /// </value>
        public bool AllowMultipleWriters { get; set; }

        /// <summary>
        /// Called when [file moved].
        /// </summary>
        /// <param name="sourceFile">The source file.</param>
        /// <param name="destinationFile">The destination file.</param>
        private void OnFileMoved(string sourceFile, string destinationFile)
        {
            if (this.FileMoved != null)
                this.FileMoved(sourceFile, destinationFile);
        }
    }
}
