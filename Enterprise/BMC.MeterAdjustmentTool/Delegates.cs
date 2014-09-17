using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BMC.MeterAdjustmentTool.Exchange;

namespace BMC.MeterAdjustmentTool
{
    public class ProcessEventArgs : EventArgs
    {
        public ProcessEventArgs()
        {
            this.StartDate = DateTime.MinValue;
            this.EndDate = DateTime.MinValue;
        }

        public virtual DateTime StartDate { get; set; }

        public virtual DateTime EndDate { get; set; }

        public int? InstallationNo { get; set; }
    }

    public class GridRowSelectedEventArgs : EventArgs
    {
        public GridRowSelectedEventArgs() { }

        public DataRow SelectedRow { get; set; }
    }

    public class GridEditItemEventArgs : EventArgs
    {
        public GridEditItemEventArgs() { }

        //public int SelectedRowIndex { get; set; }

        public DataRow SelectedRow { get; set; }

        public int? InstallationNo { get; set; }
    }

    public delegate void ProcessClickedEventHandler(object sender, ProcessEventArgs e);
    public delegate void GridRowSelectedEventHandler(object sender, GridRowSelectedEventArgs e);
    public delegate void GridEditItemEventHandler(object sender, GridEditItemEventArgs e);

    public delegate IQueryExecutor<DataSet> CreateSelectQueryExecutorHandler();
    public delegate IQueryExecutor<bool> CreateUpdateQueryExecutorHandler();

    public delegate IQueryExecutor<DataSet> DeltaGridLoadGridHandler(ProcessEventArgs p);
    public delegate IQueryExecutor<DataSet> DeltaGridEditItemHandler(GridEditItemEventArgs p);
    public delegate IQueryExecutor<bool> DeltaGridUpdateItemHandler(GridEditItemEventArgs p);

    public delegate IDataInterface CreateDataInterfaceHandler();
    public delegate void FillDataForLoadGridEventHandler(ProcessEventArgs p, IQueryExecutor<DataSet> q);
    public delegate void FillDataForEditItemEventHandler(GridEditItemEventArgs p, IQueryExecutor<DataSet> q);
}
