using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Text.RegularExpressions;
using BMC.CoreLib.Diagnostics;

namespace BMC.CoreLib.Win32.Validation
{
    public class RegExFieldValidator : BaseValidator
    {
        private Regex _regex = null;
        public string _expression = string.Empty;

        public RegExFieldValidator()
            : base() { }

        public RegExFieldValidator(IContainer container)
            : base(container) { }

        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Category("Validation")]
        public string Expression
        {
            get
            {
                return _expression;
            }
            set
            {
                if (value != _expression)
                {
                    this.InitExpression(value);
                    _expression = value;
                }
            }
        }

        protected virtual void InitExpression(string expression)
        {
            ModuleProc PROC = new ModuleProc("RegExFieldValidator", "InitExpression");

            try
            {
                if (_regex != null)
                {
                    _regex = null;
                }
                _regex = new Regex(expression);
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        protected override bool EnsureIsValid()
        {
            Match m = _regex.Match(this.ControlToValidate.Text);
            if (m == null) return false;
            else return !this.ControlToValidate.Text.Trim().IsEmpty();
        }
    }
}
