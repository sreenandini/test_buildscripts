/* ================================================================================= 
 * Purpose		:	Logger Extension Methods Class
 * File Name	:   LoggerExtensionMethods.cs
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
using System.Reflection;

namespace BMC.CoreLib.Diagnostics {
    public static class LoggerExtensionMethods {
        #region Methods

        #region Writes the log information (plain message)

        /// <summary>
        /// Writes the log info.
        /// </summary>
        /// <param name="owner">The owner.</param>
        /// <param name="message">The message.</param>
        public static void WriteLogInfo(this DisposableObjectLogger owner, string message) {
            if (owner.Logger == null) return;
            owner.Logger.WriteLogInfo(message);
        }

        /// <summary>
        /// Writes the log error.
        /// </summary>
        /// <param name="owner">The owner.</param>
        /// <param name="message">The message.</param>
        public static void WriteLogError(this DisposableObjectLogger owner, string message) {
            if (owner.Logger == null) return;
            owner.Logger.WriteLogError(message);
        }

        /// <summary>
        /// Writes the log warning.
        /// </summary>
        /// <param name="owner">The owner.</param>
        /// <param name="message">The message.</param>
        public static void WriteLogWarning(this DisposableObjectLogger owner, string message) {
            if (owner.Logger == null) return;
            owner.Logger.WriteLogWarning(message);
        }

        /// <summary>
        /// Writes the log exception.
        /// </summary>
        /// <param name="owner">The owner.</param>
        /// <param name="message">The message.</param>
        public static void WriteLogException(this DisposableObjectLogger owner, string message) {
            if (owner.Logger == null) return;
            owner.Logger.WriteLogException(message);
        }

        /// <summary>
        /// Writes the log exception.
        /// </summary>
        /// <param name="owner">The owner.</param>
        /// <param name="ex">The ex.</param>
        public static void WriteLogException(this DisposableObjectLogger owner, Exception ex) {
            if (owner.Logger == null) return;
            owner.Logger.WriteLogException(ex);
        }

        /// <summary>
        /// Writes the raw message.
        /// </summary>
        /// <param name="owner">The owner.</param>
        /// <param name="message">The message.</param>
        public static void WriteLogRawMessage(this DisposableObjectLogger owner, string message) {
            if (owner.Logger == null) return;
            owner.Logger.WriteLogRawMessage(message);
        }

        #endregion

        #region Writes the log information (plain message + Extra)

        /// <summary>
        /// Writes the log info.
        /// </summary>
        /// <param name="owner">The owner.</param>
        /// <param name="message">The message.</param>
        /// <param name="extra">The extra.</param>
        public static void WriteLogInfo(this DisposableObjectLogger owner, string message, object extra) {
            if (owner.Logger == null) return;
            owner.Logger.WriteLogInfo(message, extra);
        }

        /// <summary>
        /// Writes the log error.
        /// </summary>
        /// <param name="owner">The owner.</param>
        /// <param name="message">The message.</param>
        /// <param name="extra">The extra.</param>
        public static void WriteLogError(this DisposableObjectLogger owner, string message, object extra) {
            if (owner.Logger == null) return;
            owner.Logger.WriteLogError(message, extra);
        }

        /// <summary>
        /// Writes the log warning.
        /// </summary>
        /// <param name="owner">The owner.</param>
        /// <param name="message">The message.</param>
        /// <param name="extra">The extra.</param>
        public static void WriteLogWarning(this DisposableObjectLogger owner, string message, object extra) {
            if (owner.Logger == null) return;
            owner.Logger.WriteLogWarning(message, extra);
        }

        /// <summary>
        /// Writes the log exception.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="extra">The extra.</param>
        public static void WriteLogException(this DisposableObjectLogger owner, string message, object extra) {
            if (owner.Logger == null) return;
            owner.Logger.WriteLogException(message, extra);
        }

        /// <summary>
        /// Writes the log exception.
        /// </summary>
        /// <param name="owner">The owner.</param>
        /// <param name="ex">The ex.</param>
        /// <param name="extra">The extra.</param>
        public static void WriteLogException(this DisposableObjectLogger owner, Exception ex, object extra) {
            if (owner.Logger == null) return;
            owner.Logger.WriteLogException(ex, extra);
        }

        /// <summary>
        /// Writes the raw message.
        /// </summary>
        /// <param name="owner">The owner.</param>
        /// <param name="message">The message.</param>
        /// <param name="extra">The extra.</param>
        public static void WriteLogRawMessage(this DisposableObjectLogger owner, string message, object extra) {
            if (owner.Logger == null) return;
            owner.Logger.WriteLogRawMessage(message, extra);
        }

        #endregion

        #region Writes the log information (method base logging system)

        /// <summary>
        /// Writes the log info.
        /// </summary>
        /// <param name="owner">The owner.</param>
        /// <param name="methodBase">The method base.</param>
        /// <param name="message">The message.</param>
        public static void WriteLogInfo(this DisposableObjectLogger owner, MethodBase methodBase, string message) {
            if (owner.Logger == null) return;
            owner.Logger.WriteLogInfo(methodBase, message);
        }

        /// <summary>
        /// Writes the log error.
        /// </summary>
        /// <param name="owner">The owner.</param>
        /// <param name="methodBase">The method base.</param>
        /// <param name="message">The message.</param>
        public static void WriteLogError(this DisposableObjectLogger owner, MethodBase methodBase, string message) {
            if (owner.Logger == null) return;
            owner.Logger.WriteLogError(methodBase, message);
        }

        /// <summary>
        /// Writes the log warning.
        /// </summary>
        /// <param name="owner">The owner.</param>
        /// <param name="methodBase">The method base.</param>
        /// <param name="message">The message.</param>
        public static void WriteLogWarning(this DisposableObjectLogger owner, MethodBase methodBase, string message) {
            if (owner.Logger == null) return;
            owner.Logger.WriteLogWarning(methodBase, message);
        }

        /// <summary>
        /// Writes the log exception.
        /// </summary>
        /// <param name="owner">The owner.</param>
        /// <param name="methodBase">The method base.</param>
        /// <param name="message">The message.</param>
        public static void WriteLogException(this DisposableObjectLogger owner, MethodBase methodBase, string message) {
            if (owner.Logger == null) return;
            owner.Logger.WriteLogException(methodBase, message);
        }

        /// <summary>
        /// Writes the log exception.
        /// </summary>
        /// <param name="owner">The owner.</param>
        /// <param name="methodBase">The method base.</param>
        /// <param name="ex">The ex.</param>
        public static void WriteLogException(this DisposableObjectLogger owner, MethodBase methodBase, Exception ex) {
            if (owner.Logger == null) return;
            owner.Logger.WriteLogException(methodBase, ex);
        }

        #endregion

        #region Writes the log information (method base logging system + Extra)

        /// <summary>
        /// Writes the log info.
        /// </summary>
        /// <param name="owner">The owner.</param>
        /// <param name="methodBase">The method base.</param>
        /// <param name="message">The message.</param>
        /// <param name="extra">The extra.</param>
        public static void WriteLogInfo(this DisposableObjectLogger owner, MethodBase methodBase, string message, object extra) {
            if (owner.Logger == null) return;
            owner.Logger.WriteLogInfo(methodBase, message, extra);
        }

        /// <summary>
        /// Writes the log error.
        /// </summary>
        /// <param name="owner">The owner.</param>
        /// <param name="methodBase">The method base.</param>
        /// <param name="message">The message.</param>
        /// <param name="extra">The extra.</param>
        public static void WriteLogError(this DisposableObjectLogger owner, MethodBase methodBase, string message, object extra) {
            if (owner.Logger == null) return;
            owner.Logger.WriteLogError(methodBase, message, extra);
        }

        /// <summary>
        /// Writes the log warning.
        /// </summary>
        /// <param name="owner">The owner.</param>
        /// <param name="methodBase">The method base.</param>
        /// <param name="message">The message.</param>
        /// <param name="extra">The extra.</param>
        public static void WriteLogWarning(this DisposableObjectLogger owner, MethodBase methodBase, string message, object extra) {
            if (owner.Logger == null) return;
            owner.Logger.WriteLogWarning(methodBase, message, extra);
        }

        /// <summary>
        /// Writes the log exception.
        /// </summary>
        /// <param name="owner">The owner.</param>
        /// <param name="methodBase">The method base.</param>
        /// <param name="message">The message.</param>
        /// <param name="extra">The extra.</param>
        public static void WriteLogException(this DisposableObjectLogger owner, MethodBase methodBase, string message, object extra) {
            if (owner.Logger == null) return;
            owner.Logger.WriteLogException(methodBase, message, extra);
        }

        /// <summary>
        /// Writes the log exception.
        /// </summary>
        /// <param name="owner">The owner.</param>
        /// <param name="methodBase">The method base.</param>
        /// <param name="ex">The ex.</param>
        /// <param name="extra">The extra.</param>
        public static void WriteLogException(this DisposableObjectLogger owner, MethodBase methodBase, Exception ex, object extra) {
            if (owner.Logger == null) return;
            owner.Logger.WriteLogException(methodBase, ex, extra);
        }

        #endregion

        #region Writes the log information (module + procedure logging system)

        /// <summary>
        /// Writes the log info.
        /// </summary>
        /// <param name="owner">The owner.</param>
        /// <param name="moduleName">Name of the module.</param>
        /// <param name="procedureName">Name of the procedure.</param>
        /// <param name="message">The message.</param>
        public static void WriteLogInfo(this DisposableObjectLogger owner, string moduleName, string procedureName, string message) {
            if (owner.Logger == null) return;
            owner.Logger.WriteLogInfo(moduleName, procedureName, message);
        }

        /// <summary>
        /// Writes the log error.
        /// </summary>
        /// <param name="owner">The owner.</param>
        /// <param name="moduleName">Name of the module.</param>
        /// <param name="procedureName">Name of the procedure.</param>
        /// <param name="message">The message.</param>
        public static void WriteLogError(this DisposableObjectLogger owner, string moduleName, string procedureName, string message) {
            if (owner.Logger == null) return;
            owner.Logger.WriteLogError(moduleName, procedureName, message);
        }

        /// <summary>
        /// Writes the log warning.
        /// </summary>
        /// <param name="owner">The owner.</param>
        /// <param name="moduleName">Name of the module.</param>
        /// <param name="procedureName">Name of the procedure.</param>
        /// <param name="message">The message.</param>
        public static void WriteLogWarning(this DisposableObjectLogger owner, string moduleName, string procedureName, string message) {
            if (owner.Logger == null) return;
            owner.Logger.WriteLogWarning(moduleName, procedureName, message);
        }

        /// <summary>
        /// Writes the log exception.
        /// </summary>
        /// <param name="owner">The owner.</param>
        /// <param name="moduleName">Name of the module.</param>
        /// <param name="procedureName">Name of the procedure.</param>
        /// <param name="message">The message.</param>
        public static void WriteLogException(this DisposableObjectLogger owner, string moduleName, string procedureName, string message) {
            if (owner.Logger == null) return;
            owner.Logger.WriteLogException(moduleName, procedureName, message);
        }

        /// <summary>
        /// Writes the log exception.
        /// </summary>
        /// <param name="owner">The owner.</param>
        /// <param name="moduleName">Name of the module.</param>
        /// <param name="procedureName">Name of the procedure.</param>
        /// <param name="ex">The ex.</param>
        public static void WriteLogException(this DisposableObjectLogger owner, string moduleName, string procedureName, Exception ex) {
            if (owner.Logger == null) return;
            owner.Logger.WriteLogException(moduleName, procedureName, ex);
        }

        #endregion

        #region Writes the log information (module + procedure logging system + Extra)

        /// <summary>
        /// Writes the log info.
        /// </summary>
        /// <param name="owner">The owner.</param>
        /// <param name="moduleName">Name of the module.</param>
        /// <param name="procedureName">Name of the procedure.</param>
        /// <param name="message">The message.</param>
        /// <param name="extra">The extra.</param>
        public static void WriteLogInfo(this DisposableObjectLogger owner, string moduleName, string procedureName, string message, object extra) {
            if (owner.Logger == null) return;
            owner.Logger.WriteLogInfo(moduleName, procedureName, message, extra);
        }

        /// <summary>
        /// Writes the log error.
        /// </summary>
        /// <param name="owner">The owner.</param>
        /// <param name="moduleName">Name of the module.</param>
        /// <param name="procedureName">Name of the procedure.</param>
        /// <param name="message">The message.</param>
        /// <param name="extra">The extra.</param>
        public static void WriteLogError(this DisposableObjectLogger owner, string moduleName, string procedureName, string message, object extra) {
            if (owner.Logger == null) return;
            owner.Logger.WriteLogError(moduleName, procedureName, message, extra);
        }

        /// <summary>
        /// Writes the log warning.
        /// </summary>
        /// <param name="owner">The owner.</param>
        /// <param name="moduleName">Name of the module.</param>
        /// <param name="procedureName">Name of the procedure.</param>
        /// <param name="message">The message.</param>
        /// <param name="extra">The extra.</param>
        public static void WriteLogWarning(this DisposableObjectLogger owner, string moduleName, string procedureName, string message, object extra) {
            if (owner.Logger == null) return;
            owner.Logger.WriteLogWarning(moduleName, procedureName, message, extra);
        }

        /// <summary>
        /// Writes the log exception.
        /// </summary>
        /// <param name="owner">The owner.</param>
        /// <param name="moduleName">Name of the module.</param>
        /// <param name="procedureName">Name of the procedure.</param>
        /// <param name="message">The message.</param>
        /// <param name="extra">The extra.</param>
        public static void WriteLogException(this DisposableObjectLogger owner, string moduleName, string procedureName, string message, object extra) {
            if (owner.Logger == null) return;
            owner.Logger.WriteLogException(moduleName, procedureName, message, extra);
        }

        /// <summary>
        /// Writes the log exception.
        /// </summary>
        /// <param name="owner">The owner.</param>
        /// <param name="moduleName">Name of the module.</param>
        /// <param name="procedureName">Name of the procedure.</param>
        /// <param name="ex">The ex.</param>
        /// <param name="extra">The extra.</param>
        public static void WriteLogException(this DisposableObjectLogger owner, string moduleName, string procedureName, Exception ex, object extra) {
            if (owner.Logger == null) return;
            owner.Logger.WriteLogException(moduleName, procedureName, ex, extra);
        }

        #endregion

        #region Writes the log information (procedure + plain message)

        /// <summary>
        /// Writes the log info.
        /// </summary>
        /// <param name="owner">The owner.</param>
        /// <param name="procedureName">Name of the procedure.</param>
        /// <param name="message">The message.</param>
        public static void WriteLogInfo(this DisposableObjectLogger owner, string procedureName, string message) {
            if (owner.Logger == null) return;
            owner.Logger.WriteLogInfo(procedureName, message);
        }

        /// <summary>
        /// Writes the log error.
        /// </summary>
        /// <param name="owner">The owner.</param>
        /// <param name="procedureName">Name of the procedure.</param>
        /// <param name="message">The message.</param>
        public static void WriteLogError(this DisposableObjectLogger owner, string procedureName, string message) {
            if (owner.Logger == null) return;
            owner.Logger.WriteLogError(procedureName, message);
        }

        /// <summary>
        /// Writes the log warning.
        /// </summary>
        /// <param name="owner">The owner.</param>
        /// <param name="procedureName">Name of the procedure.</param>
        /// <param name="message">The message.</param>
        public static void WriteLogWarning(this DisposableObjectLogger owner, string procedureName, string message) {
            if (owner.Logger == null) return;
            owner.Logger.WriteLogWarning(procedureName, message);
        }

        /// <summary>
        /// Writes the log exception.
        /// </summary>
        /// <param name="owner">The owner.</param>
        /// <param name="procedureName">Name of the procedure.</param>
        /// <param name="message">The message.</param>
        public static void WriteLogException(this DisposableObjectLogger owner, string procedureName, string message) {
            if (owner.Logger == null) return;
            owner.Logger.WriteLogException(procedureName, message);
        }

        /// <summary>
        /// Writes the log exception.
        /// </summary>
        /// <param name="owner">The owner.</param>
        /// <param name="procedureName">Name of the procedure.</param>
        /// <param name="ex">The ex.</param>
        public static void WriteLogException(this DisposableObjectLogger owner, string procedureName, Exception ex) {
            if (owner.Logger == null) return;
            owner.Logger.WriteLogException(procedureName, ex);
        }

        #endregion

        #region Writes the log information (module procedure + plain message)

        /// <summary>
        /// Writes the log info.
        /// </summary>
        /// <param name="owner">The owner.</param>
        /// <param name="proc">Module procedure name.</param>
        /// <param name="message">The message.</param>
        public static void WriteLogInfo(this DisposableObjectLogger owner, ModuleProc proc, string message) {
            if (owner.Logger == null) return;
            owner.Logger.WriteLogInfo(proc, message);
        }

        /// <summary>
        /// Writes the log error.
        /// </summary>
        /// <param name="owner">The owner.</param>
        /// <param name="proc">Module procedure name.</param>
        /// <param name="message">The message.</param>
        public static void WriteLogError(this DisposableObjectLogger owner, ModuleProc proc, string message) {
            if (owner.Logger == null) return;
            owner.Logger.WriteLogError(proc, message);
        }

        /// <summary>
        /// Writes the log warning.
        /// </summary>
        /// <param name="owner">The owner.</param>
        /// <param name="proc">Module procedure name.</param>
        /// <param name="message">The message.</param>
        public static void WriteLogWarning(this DisposableObjectLogger owner, ModuleProc proc, string message) {
            if (owner.Logger == null) return;
            owner.Logger.WriteLogWarning(proc, message);
        }

        /// <summary>
        /// Writes the log exception.
        /// </summary>
        /// <param name="owner">The owner.</param>
        /// <param name="proc">Module procedure name.</param>
        /// <param name="message">The message.</param>
        public static void WriteLogException(this DisposableObjectLogger owner, ModuleProc proc, string message) {
            if (owner.Logger == null) return;
            owner.Logger.WriteLogException(proc, message);
        }

        /// <summary>
        /// Writes the log exception.
        /// </summary>
        /// <param name="owner">The owner.</param>
        /// <param name="proc">Module procedure name.</param>
        /// <param name="ex">The ex.</param>
        public static void WriteLogException(this DisposableObjectLogger owner, ModuleProc proc, Exception ex) {
            if (owner.Logger == null) return;
            owner.Logger.WriteLogException(proc, ex);
        }

        #endregion

        #endregion
    }
}
