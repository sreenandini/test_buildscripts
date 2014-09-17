/* ================================================================================= 
 * Purpose		:	Entry Point Class
 * File Name	:   EntryPoint.cs
 * Author		:	A.Vinod Kumar
 * Created  	:	22/12/2010
 * ================================================================================= 
 * Revision History :
 * ================================================================================= 
 * 22/12/2010		A.Vinod Kumar    Initial Version
 * ===============================================================================*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BMC.CoreLib.Diagnostics;

namespace BMC.CoreLib
{
    /// <summary>
    /// EntryPoint Class
    /// </summary>
    public abstract class EntryPoint
        : DisposableObjectNotify
    {
        /// <summary>
        /// Entry point
        /// </summary>
        private static EntryPoint _current = null;
        
        /// <summary>
        /// Collection of arguments.
        /// </summary>
        protected string[] _args = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="EntryPoint"/> class.
        /// </summary>
        /// <param name="args">The args.</param>
        protected EntryPoint(string[] args)
        {
            _current = this;

            if (args == null)
                _args = new string[0];
            else
                _args = args;
        }

        /// <summary>
        /// Runs this instance.
        /// </summary>
        public void Run()
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "Run");

            try
            {
                this.RunInternal();
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }            
        }

        /// <summary>
        /// Runs the internal.
        /// </summary>
        protected virtual void RunInternal()
        {
            if (_args.Length == 0)
                this.ExecuteWithoutCommandLine();
            else
                this.ExecuteWithCommandLine();
        }

        /// <summary>
        /// Executes the with command line.
        /// </summary>
        protected abstract void ExecuteWithCommandLine();

        /// <summary>
        /// Executes the without command line.
        /// </summary>
        protected abstract void ExecuteWithoutCommandLine();

        /// <summary>
        /// Applies the default view style.
        /// </summary>
        /// <param name="viewForm">The view form.</param>
        //public abstract void ApplyDefaultViewStyle(IViewBase viewForm);

        /// <summary>
        /// Overridable method which releases the managed resources.
        /// </summary>
        protected override void DisposeManaged()
        {
            base.DisposeManaged();
            _args = null;
        }

        #region Static Members
        public static EntryPoint Current
        {
            get
            {
                return _current;
            }
        }
        #endregion
    }
}
