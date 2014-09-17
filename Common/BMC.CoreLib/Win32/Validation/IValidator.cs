using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.CoreLib.Win32.Validation
{
    public interface IValidator
    {
        string ErrorMessage { get; set; }
        bool Validate();
    }
}
