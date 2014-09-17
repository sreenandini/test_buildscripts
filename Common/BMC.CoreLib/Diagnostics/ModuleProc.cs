using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;

namespace BMC.CoreLib.Diagnostics
{
    /// <summary>
    /// Module Procedure Class
    /// </summary>
    public class ModuleProc : DisposableObject
    {
        private static readonly bool _canLogMethod = false;

        /// <summary>
        /// Initializes the <see cref="ModuleProc"/> class.
        /// </summary>
        static ModuleProc()
        {
            _canLogMethod = Extensions.GetAppSettingValueBool("LOG_METHOD_SIGNATURE", false);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ModuleProc"/> class.
        /// </summary>
        /// <param name="procedure">The procedure.</param>
        public ModuleProc(string procedure)
        {
            this.Procedure = procedure;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ModuleProc"/> class.
        /// </summary>
        /// <param name="module">The module.</param>
        /// <param name="procedure">The procedure.</param>
        public ModuleProc(string module, string procedure)
        {
            this.Module = module;
            this.Procedure = procedure;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ModuleProc" /> class.
        /// </summary>
        /// <param name="methodBase">The method base.</param>
        public ModuleProc(MethodBase methodBase)
        {
            this.Module = methodBase.DeclaringType.Name;
            this.Procedure = methodBase.Name;
        }

        /// <summary>
        /// Gets or sets the module.
        /// </summary>
        /// <value>The module.</value>
        public string Module { get; set; }

        /// <summary>
        /// Gets or sets the procedure.
        /// </summary>
        /// <value>The procedure.</value>
        public string Procedure { get; set; }

        /// <summary>
        /// Toes the string.
        /// </summary>
        /// <param name="sb">The sb.</param>
        protected override void ToString(StringBuilder sb)
        {
            base.ToString(sb);
            if (!this.Module.IsEmpty()) sb.Append(this.Module);
            if (sb.Length > 0) sb.Append("::");
            if (!this.Procedure.IsEmpty()) sb.Append(this.Procedure);
        }

        /// <summary>
        /// Combines the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>Combined string.</returns>
        public string Combine(string message)
        {
            return Combine(this.Module, this.Procedure, message);
        }

        /// <summary>
        /// Combines the specified message.
        /// </summary>
        /// <param name="procedure">The procedure.</param>
        /// <param name="message">The message.</param>
        /// <returns>Combined string.</returns>
        public static string Combine(string procedure, string message)
        {
            return Combine(string.Empty, procedure, message);
        }

        /// <summary>
        /// Combines the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>Combined string.</returns>
        public static string Combine(string module, string procedure, string message)
        {
            if (_canLogMethod &&
                (!module.IsEmpty() || !procedure.IsEmpty()))
            {
                return (!module.IsEmpty() ? "[" + module + "]" : "") +
                                       (!module.IsEmpty() ?
                                       ((!procedure.IsEmpty() ? "." : "") + 
                                        "[" + procedure + ":" + Thread.CurrentThread.ManagedThreadId + "] -> ") : "") +
                                       message;   
            }
            else
            {
                return message;                         
            }
        }
    }
}
