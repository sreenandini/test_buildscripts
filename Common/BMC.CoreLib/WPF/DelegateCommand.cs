// -----------------------------------------------------------------------
// <copyright file="DelegateCommand.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace BMC.CoreLib.WPF
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Windows.Input;
    using BMC.CoreLib.Diagnostics;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class DelegateCommand : DisposableObject, ICommand
    {
        protected Action<object> _execute = null;
        protected Func<object, bool> _canExecute = null;

        public DelegateCommand(Action<object> execute, Func<object, bool> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        #region ICommand Members

        public bool CanExecute(object parameter)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "Execute");

            try
            {
                if (_canExecute != null)
                    return _canExecute(parameter);
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }

            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "Execute");

            try
            {
                if (_execute != null)
                {
                    _execute(parameter);
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        #endregion
    }
}
