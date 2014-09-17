// -----------------------------------------------------------------------
// <copyright file="RequiredFieldValidationRule.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace BMC.CoreLib.WPF.Validation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Windows.Controls;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class RequiredFieldValidationRule : ValidationRuleBase
    {
        public RequiredFieldValidationRule() { }

        #region ValidationMessage
        private string _validationMessage = default(string);

        public string ValidationMessage
        {
            get { return _validationMessage; }
            set
            {
                this.SetProperty<string>(ref _validationMessage, value, "ValidationMessage");
            }
        }
        #endregion

        //public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        //{
        //    string sValue = value.ToString();
        //    if (sValue.IsEmpty())
        //    {
        //        return new ValidationResult(false, this.ValidationMessage);
        //    }
        //    return new ValidationResult(true, null);
        //}

        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            string sValue = value.ToString();
            if (sValue.IsEmpty())
            {
                return new ValidationResult(false, this.ValidationMessage);
            }
            return new ValidationResult(true, null);
        }
    }
}
