using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BMC.CoreLib.Win32.Validation;

namespace BMC.EnterpriseClient.Helpers
{
    public static class ControlExtensions
    {
        public static bool RaiseInfoMessageAndReturn(this Control source, Form ownerForm, string message)
        {
            ownerForm.ShowInfoMessageBox(message);
            source.Focus();
            return false;
        }

        public static bool RaiseWarningMessageAndReturn(this Control source, Form ownerForm, string message)
        {
            ownerForm.ShowWarningMessageBox(message);
            source.Focus();
            return false;
        }

        public static bool RaiseErrorMessageAndReturn(this Control source, Form ownerForm, string message)
        {
            ownerForm.ShowErrorMessageBox(message);
            source.Focus();
            return false;
        }
        public static bool RaiseInfoMessageAndReturnV(this Control source, Form ownerForm, CustomValidator customValidator, ValidationSummary validationSummary, string message)
        {
            customValidator.ControlToValidate = source;
            customValidator.ErrorMessage = message;
            customValidator.HasErrors = true;
            validationSummary.Validate();
            source.Focus();
            return false;
        }
        public static bool RaiseWarningMessageAndReturnV(this Control source, Form ownerForm, CustomValidator customValidator, ValidationSummary validationSummary, string message)
        {
            customValidator.ControlToValidate = source;
            customValidator.ErrorMessage = message;
            customValidator.HasErrors = true;
            validationSummary.Validate();
            source.Focus();
            return false;
        }
        public static bool RaiseErrorMessageAndReturnV(this Control source, Form ownerForm, CustomValidator customValidator, ValidationSummary validationSummary, string message)
        {
            customValidator.ControlToValidate = source;
            customValidator.ErrorMessage = message;
            customValidator.HasErrors = true;
            validationSummary.Validate();
            source.Focus();
            return false;
        }
    }
}
