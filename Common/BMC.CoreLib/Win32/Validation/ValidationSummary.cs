using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using BMC.CoreLib.Diagnostics;
using System.Windows.Forms;
using System.Drawing;

namespace BMC.CoreLib.Win32.Validation
{
    public class ValidationSummary : ControlBase, IValidator
    {
        private string[] _actualErrorMessage = null;
        protected TextFormatFlags _formatFlags = TextFormatFlags.Left |
            TextFormatFlags.Top |
            TextFormatFlags.GlyphOverhangPadding |
            TextFormatFlags.WordBreak;

        public ValidationSummary()
        {
            this.ForeColor = Color.Red;
        }

        public ValidationSummary(IContainer container)
            : base(container) { }

        private bool _showMessageBox = false;

        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Category("Appearance")]
        public bool ShowMessageBox
        {
            get { return _showMessageBox; }
            set { _showMessageBox = value; }
        }

        private bool _exitOnFirstFailure = false;

        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Category("Appearance")]
        public bool ExitOnFirstFailure
        {
            get { return _exitOnFirstFailure; }
            set { _exitOnFirstFailure = value; }
        }

        private string _errorMessage = string.Empty;

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string ErrorMessage
        {
            get { return _errorMessage; }
            set { _errorMessage = value; }
        }

        #region IValidator Members

        public bool Validate()
        {
            ModuleProc PROC = new ModuleProc("ValidationSummary", "Validate");
            bool result = false;

            try
            {
                _actualErrorMessage = null;
                IContainer container = this.Container;
                IValidator[] validators = (from i in container.Components.OfType<IValidator>()
                                           where Object.ReferenceEquals(this, i) == false
                                           select i).ToArray();
                if (validators != null)
                {
                    bool compResult = true;
                    IList<string> errorMessages = new List<string>();
                    Control firstComponent = null;

                    foreach (IValidator validator in validators)
                    {
                        bool isValid = validator.Validate();
                        if (!isValid)
                        {
                            errorMessages.Add(validator.ErrorMessage);
                            if (firstComponent == null)
                            {
                                BaseValidator validator2 = validator as BaseValidator;
                                if (validator2 != null)
                                {
                                    firstComponent = validator2.ControlToValidate;
                                }
                            }
                        }
                        compResult &= isValid;

                        if (!isValid && _exitOnFirstFailure) break;
                    }

                    if (!compResult)
                    {
                        string[] errorMessagesArray = errorMessages.ToArray();
                        if (this.ShowMessageBox)
                        {
                            string fullMessage = string.Join(Environment.NewLine, errorMessagesArray);
                            this.ShowErrorMessageBox(fullMessage);
                        }
                        else
                        {
                            _actualErrorMessage = errorMessagesArray;
                            this.Invalidate();
                        }
                        if (firstComponent != null)
                        {
                            firstComponent.Focus();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }

            return result;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (!this.ShowMessageBox &&
                _actualErrorMessage != null)
            {
                Rectangle rc = this.ClientRectangle;
                Padding pad = this.Padding;
                Rectangle rc2 = new Rectangle(rc.Left + pad.Left, rc.Top + pad.Top,
                    (rc.Right - pad.Right - pad.Left), (rc.Bottom - pad.Bottom - pad.Top));
                Rectangle rcText = rc2;

                for (int i = 0; i < _actualErrorMessage.Length; i++)
                {
                    string message = "* " + _actualErrorMessage[i];
                    TextRenderer.DrawText(e.Graphics, message, this.Font,
                    rcText, this.ForeColor, _formatFlags);
                    rcText.Y += rc.Height + pad.Bottom;
                }
            }
        }

        #endregion
    }
}
