using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.CoreLib
{
    public interface IConfigValue<T, S>
    {
        T Value { get; set; }

        T RealValue { get; set; }

        string ToString();
    }

    public interface IConfigValue<T> : IConfigValue<T, T> { }
}
