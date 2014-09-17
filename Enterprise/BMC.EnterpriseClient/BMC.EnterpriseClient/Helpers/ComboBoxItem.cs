using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.CoreLib;
using System.Windows.Forms;

namespace BMC.EnterpriseClient.Helpers
{
    public class ComboBoxItem<T> : DisposableObject
    {
        public ComboBoxItem()
            : this(string.Empty) { }

        public ComboBoxItem(string text)
            : this(default(T), text) { }

        public ComboBoxItem(T value, string text)
        {
            this.Text = text;
            this.Value = value;
        }

        public string Text { get; set; }

        public T Value { get; set; }
    }

    public class ComboBoxItem : ComboBoxItem<object>
    {
        public ComboBoxItem()
            : this(string.Empty) { }

        public ComboBoxItem(string text)
            : this(null, text) { }

        public ComboBoxItem(object value, string text)
            : base(value, text) { }
    }
}
