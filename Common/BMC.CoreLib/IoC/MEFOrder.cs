// -----------------------------------------------------------------------
// <copyright file="IMEFOrder.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace BMC.CoreLib.IoC
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
#if NET4 && MEF
    using System.ComponentModel.Composition;
#endif

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public interface IMEFOrderMetadata
    {
        int Order { get; }
    }
}
