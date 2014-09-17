// -----------------------------------------------------------------------
// <copyright file="TypeComparer.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace BMC.CoreLib.Comparers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class TypeComparer : DisposableObject, IComparer<Type>
    {
        public TypeComparer() { }

        #region IComparer<Type> Members

        public int Compare(Type x, Type y)
        {
            return string.Compare(x.FullName, y.FullName, true);// (x.FullName.IgnoreCaseCompare(y.FullName) ? 0 : 1);
        }

        #endregion
    }
}
