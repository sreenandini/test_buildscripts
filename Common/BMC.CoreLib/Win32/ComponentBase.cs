using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace BMC.CoreLib.Win32
{
    public class ComponentBase : Component
    {
        public ComponentBase() { }

        public ComponentBase(IContainer container)
            : this()
        {
            container.Add(this);
        }
    }
}
