using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BMC.CoreLib.Win32
{
    public delegate void GridViewRowSelectedHandler(GridViewRowSelectedEventArgs e);

    public delegate void GridViewCellClickedHandler(GridViewCellClickedEventArgs e);

    public delegate void GridViewAfterDataBoundHandler(GridViewAfterDataBoundEventArgs e);

    public delegate void GridViewManualRowBindHandler(ManualRowBindEventArgs e);

    public class GridViewEventArgs : EventArgs
    {
        public DataGridViewRow Row { get; internal set; }
        public int RowNumber { get; internal set; }
        public int GridRowNumber { get; internal set; }
        public object DataObject { get; internal set; }
    }

    public class ManualRowBindEventArgs : GridViewEventArgs { }

    public class GridViewRowSelectedEventArgs : GridViewEventArgs { }

    public class GridViewCellClickedEventArgs : GridViewEventArgs
    {
        public DataGridViewCell Cell { get; internal set; }
    }

    public class GridViewAfterDataBoundEventArgs : GridViewEventArgs
    {
        public int Start { get; internal set; }
        public int End { get; internal set; }
        public int Total { get; internal set; }
    }
}
