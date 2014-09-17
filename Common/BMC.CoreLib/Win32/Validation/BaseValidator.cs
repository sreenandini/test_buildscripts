using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;
using BMC.CoreLib.Diagnostics;

namespace BMC.CoreLib.Win32.Validation
{
    public abstract class BaseValidator : ComponentBase, IValidator
    {
        private ErrorProvider _errorProvider = null;

        public BaseValidator()
            : base()
        {
            _errorProvider = new ErrorProvider();
        }

        public BaseValidator(IContainer container)
            : base(container)
        {
            _errorProvider = new ErrorProvider();
        }

        protected Control _controlToValidate = null;

        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Category("Validation")]
        [TypeConverter(typeof(ReferenceConverter))]
        public virtual Control ControlToValidate
        {
            get
            {
                return _controlToValidate;
            }
            set
            {
                if (_controlToValidate != null &&
                    !this.DesignMode)
                {
                    _controlToValidate.Validating -= this.OnControlToValidate_Validating;
                }

                _controlToValidate = value;
                if (_controlToValidate != null &&
                !this.DesignMode)
                {
                    _controlToValidate.Validating += new CancelEventHandler(this.OnControlToValidate_Validating);
                }
            }
        }

        void OnControlToValidate_Validating(object sender, CancelEventArgs e)
        {
            ModuleProc PROC = new ModuleProc("BaseValidator", "OnControlToValidate_Validating");

            try
            {
                this.Validate();
                if (!this.IsValid && this.CancelFocusChangeWhenInvalid)
                {
                    e.Cancel = true;
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Category("Appearance")]
        public Icon Icon
        {
            get { return _errorProvider.Icon; }
            set
            {
                _errorProvider.Icon = value;
            }
        }

        private string _errorMessage = string.Empty;

        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Category("Appearance")]
        public string ErrorMessage
        {
            get { return _errorMessage; }
            set { _errorMessage = value; }
        }

        private bool _isValid = false;

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool IsValid
        {
            get { return _isValid; }
        }

        private bool _cancelFocus = false;

        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Category("Appearance")]
        public bool CancelFocusChangeWhenInvalid
        {
            get { return _cancelFocus; }
            set { _cancelFocus = value; }
        }

        public virtual bool Validate()
        {
            ModuleProc PROC = new ModuleProc("BaseValidator", "Validate");

            try
            {
                _isValid = this.EnsureIsValid();

                if (_isValid)
                {
                    _errorProvider.SetError(this.ControlToValidate, string.Empty);
                }
                else
                {
                    _errorProvider.SetError(this.ControlToValidate, this.ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
            return _isValid;
        }

        protected abstract bool EnsureIsValid();

        protected void ClearProvider()
        {
            _errorProvider.SetError(this.ControlToValidate, string.Empty);
        }

        public virtual void ClearValidation() { }
    }
}
