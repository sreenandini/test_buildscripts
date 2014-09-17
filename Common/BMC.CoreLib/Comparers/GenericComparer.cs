using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.CoreLib
{
    public class GenericComparer<T> : 
        DisposableObject, IComparer<T>
        where T : IComparable<T>
    {
        public GenericComparer() { }

        #region IComparer<T> Members

        public int Compare(T x, T y)
        {
            return ((IComparable<T>)x).CompareTo(y);            
        }

        #endregion
    }
}
