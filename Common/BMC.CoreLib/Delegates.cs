using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.CoreLib.Diagnostics;

namespace BMC.CoreLib {
    /// <summary>
    /// Get Value Try Parse handler
    /// </summary>
    public delegate void GetValueTryParseHandler<T>(string text, out T value);

    /// <summary>
    /// FileMovedEventHandler
    /// </summary>
    public delegate void FileMovedEventHandler(string sourceFile, string destinationFile);

    /// <summary>
    /// MethodInvokedHandler
    /// </summary>
    public delegate void MethodInvokedHandler(Exception ex);

    /// <summary>
    /// ProcessLogItemHandler
    /// </summary>
    public delegate void ProcessQueueItemHandler<T>(T item);

    /// <summary>
    /// ProcessLogItemHandler
    /// </summary>
    public delegate void ProcessLogItemHandler(LogItem item);

    /// <summary>
    /// LogFormatMessageHandler
    /// </summary>
    public delegate void LogFormatMessageHandler(LogEntryType logType, string dateTime, string moduleName, string procedureName, string message);    

    /// <summary>
    /// Write to external logger
    /// </summary>
    public delegate void WriteToExternalLogHandler(string formattedMessage, LogEntryType type, object extra);
}
