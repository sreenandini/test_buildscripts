// -----------------------------------------------------------------------
// <copyright file="ValidationRuleBase.cs" company="">
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
    using System.ComponentModel;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public abstract class ValidationRuleBase : ValidationRule, INotifyPropertyChanged
    {
        public ValidationRuleBase() { }

        /// <summary>
        /// Sets the property.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="field">The field.</param>
        /// <param name="value">The value.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns>True if succeeded; otherwise false.</returns>
        public bool SetProperty<T>(ref T field, T value, string propertyName)
        {
            if (object.Equals(field, value)) return false;
            field = value;
            this.NotifyPropertyChanged(propertyName);
            return true;
        }

        #region INotifyPropertyChanged Members

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged = null;

        /// <summary>
        /// Notifies the property changed.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        public void NotifyPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            return null;
        }
    }
}
