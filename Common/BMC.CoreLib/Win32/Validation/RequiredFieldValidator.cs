using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace BMC.CoreLib.Win32.Validation
{
    public class RequiredFieldValidator : BaseValidator
    {
        public RequiredFieldValidator()
            : base() { }

        public RequiredFieldValidator(IContainer container)
            : base(container) { }

        protected override bool EnsureIsValid()
        {
            if (this.ControlToValidate.Text.IsEmpty()) return false;
            else return !this.ControlToValidate.Text.Trim().IsEmpty();
        }
    }    
}
