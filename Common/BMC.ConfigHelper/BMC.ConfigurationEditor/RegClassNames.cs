using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BMC.ConfigurationEditor
{
    public class RegClassName
    {
        public RegClassName(string className)
        {
            this.ClassName = className;
            this.Children = new List<RegClassName>();
        }

        public string ClassName { get; private set; }

        public List<RegClassName> Children { get; private set; }

        public string RefKeyName { get; set; }

        public TreeNode Node { get; set; }

        public override string ToString()
        {
            return this.ClassName;
        }
    }

    public class RegClassNames : SortedDictionary<string, RegClassName>
    {
        public RegClassNames()
            : base(StringComparer.InvariantCultureIgnoreCase) { }
    }
}
