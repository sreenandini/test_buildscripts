using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.CoreLib.Win32
{
    public class ExpandOrCollapsedEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExpandOrCollapsedEventArgs"/> class.
        /// </summary>
        public ExpandOrCollapsedEventArgs()
        {
        }

        /// <summary>
        /// Gets or sets the new width.
        /// </summary>
        /// <value>The new width.</value>
        public int NewWidth { get; set; }
    }

    public delegate void ExpandOrCollapsedEventHandler(object sender, ExpandOrCollapsedEventArgs e);
}
