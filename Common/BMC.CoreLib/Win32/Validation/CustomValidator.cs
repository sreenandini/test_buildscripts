using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows.Forms;

namespace BMC.CoreLib.Win32.Validation
{
    public class CustomValidator : BaseValidator
    {
        public CustomValidator()
            : base() { }

        public CustomValidator(IContainer container)
            : base(container) { }

        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Category("Appearance")]
        public bool HasErrors { get; set; }

        public override Control ControlToValidate
        {
            get
            {
                return base.ControlToValidate;
            }
            set
            {
                this.ClearValidation();
                base.ControlToValidate = value;
            }
        }

        protected override bool EnsureIsValid()
        {
            return !this.HasErrors;
        }

        public override void ClearValidation()
        {
            if (this.ControlToValidate != null)
            {
                this.ClearProvider();
            }
        }
    }
}
