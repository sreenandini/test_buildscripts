using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using BMC.CoreLib.Diagnostics;
using System.Drawing.Design;
using System.Threading;

namespace BMC.CoreLib.Win32
{
    public partial class PagingDataGridView : UserControl
    {
        private Action _fillMessages = null;
        private int _pageIndex = 1;
        private int _pageTotal = 1;
        private int _totalRows = 0;

        private object _lockGrid = new object();
        private IAsyncProgress2 _asyncProgress = new SyncAsyncProgress();
        private IAsyncProgress2 _asyncActive = null;

        public PagingDataGridView()
        {
            InitializeComponent();
            _fillMessages = new Action(this.DataBind);
        }

        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Category("Data")]
        public ICollection GridDataSource { get; set; }

        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public int RowsPerPage { get; set; }

        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public bool ManualDataBinding { get; set; }

        //[Browsable(true)]
        //[EditorBrowsable(EditorBrowsableState.Always)]
        //public DataGridView InnerView
        //{
        //    get
        //    {                
        //        return this.dgvRows;
        //    }
        //}

        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        //[Editor("System.Windows.Forms.Design.DataGridViewColumnCollectionEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        ////[MergableProperty(false)]
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public DataGridViewColumnCollection Columns
        {
            get
            {
                return dgvRows.Columns;
            }
        }

        public void DataBind()
        {
            this.DataBind(_asyncProgress);
        }

        public void DataBind(IAsyncProgress2 o)
        {
            ModuleProc PROC = new ModuleProc("", "DataBind");
            _asyncActive = o;

            try
            {
                _pageIndex = 0;
                _pageTotal = 0;
                _totalRows = 0;

                o.CrossThreadInvoke(() =>
                {
                    dgvRows.ClearSelection();
                    dgvRows.Rows.Clear();
                    cboPageNos.Items.Clear();

                    cboPageNos.Enabled = false;
                    cboPageNos.SelectedIndex = -1;
                    tblButtons.Visible = false;
                    this.EnableDisablePageButtons();
                });
                this.HasMessages = false;

                if (this.GridDataSource != null
                    && this.GridDataSource.Count > 0)
                {
                    o.CrossThreadInvoke(() =>
                    {
                        tblButtons.Visible = true;
                    });

                    this.HasMessages = true;
                    _totalRows = this.GridDataSource.Count;
                    _pageTotal = (_totalRows / this.RowsPerPage) +
                        (((_totalRows % this.RowsPerPage) > 0) ? 1 : 0);

                    for (int i = 1; i <= _pageTotal; i++)
                    {
                        o.CrossThreadInvoke(() =>
                        {
                            cboPageNos.Items.Add(i);
                        });
                        Thread.Sleep(1);
                    }
                    o.CrossThreadInvoke(() =>
                    {
                        cboPageNos.Enabled = true;
                    });

                    o.CrossThreadInvoke(() =>
                    {
                        if (cboPageNos.Items.Count > 0)
                            cboPageNos.SelectedIndex = 0;
                        else
                            cboPageNos.SelectedIndex = -1;
                    });
                }
                else
                {
                    o.CrossThreadInvoke(() =>
                    {
                        cboPageNos.Items.Clear();
                        lblPageInfo.Text = string.Empty;
                        cboPageNos.SelectedIndex = -1;
                    });
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        private void EnableDisablePageButtons()
        {
            cboPageNos.Enabled = (this.GridDataSource != null && this.GridDataSource.Count > 0);

            btnMoveFirst.Enabled = (_pageIndex > 1 && _pageIndex <= _pageTotal);
            btnMovePrev.Enabled = (_pageIndex > 1 && _pageIndex <= _pageTotal);

            btnMoveLast.Enabled = (_pageIndex >= 1 && _pageIndex < _pageTotal);
            btnMoveNext.Enabled = (_pageIndex >= 1 && _pageIndex < _pageTotal);
        }

        public void ClearRows()
        {
            dgvRows.Rows.Clear();
        }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool HasMessages { get; private set; }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public DataGridView GridView { get { return dgvRows; } }

        private void cboPageNos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboPageNos.SelectedIndex != -1)
            {
                int pageIndex = (int)cboPageNos.SelectedItem;
                this.DataBind(pageIndex);
            }
            this.EnableDisablePageButtons();
        }

        private void DataBind(int pageIndex)
        {
            ModuleProc PROC = new ModuleProc("", "Method");
            GridViewAfterDataBoundEventArgs args = new GridViewAfterDataBoundEventArgs();

            lock (_lockGrid)
            {
                try
                {
                    dgvRows.ClearSelection();
                    dgvRows.Rows.Clear();

                    _pageIndex = pageIndex;
                    int start = (_pageIndex - 1) * this.RowsPerPage;
                    int end = (_pageIndex * this.RowsPerPage) - 1;
                    if (end > (_totalRows - 1)) end = _totalRows - 1;
                    int records = (end - start + 1);
                    int availRecords = Math.Min(records, this.RowsPerPage);
                    args.Start = start + 1;
                    args.End = start + availRecords;
                    args.Total = _totalRows;
                    lblPageInfo.Text = string.Format("{0:D} - {1:D} ({2:D})", args.Start, args.End, args.Total);

                    object[] query = this.GridDataSource
                                                    .AsQueryable()
                                                    .OfType<Object>()
                                                    .Skip(start)
                                                    .Take(availRecords).ToArray();
                    if (this.ManualDataBinding &&
                        query != null &&
                        query.Length > 0)
                    {
                        if (_asyncActive != null)
                        {
                            try
                            {
                                _asyncActive.InitializeProgress(1, query.Length);
                            }
                            catch { }
                        }

                        int i = 1;
                        foreach (var item in query)
                        {
                            if (_asyncActive != null)
                            {
                                try
                                {
                                    if (_asyncActive.ExecutorService != null &&
                                        _asyncActive.ExecutorService.IsShutdown) break;
                                }
                                catch { }
                            }

                            int rowIndex = dgvRows.Rows.Add();
                            DataGridViewRow row = dgvRows.Rows[rowIndex];
                            row.Tag = item;

                            if (_asyncActive != null)
                            {
                                try
                                {
                                    _asyncActive.UpdateStatusProgress(i, string.Empty);
                                }
                                catch { }
                            }

                            ManualRowBindEventArgs e = new ManualRowBindEventArgs()
                            {
                                Row = row,
                                RowNumber = (start + rowIndex + 1),
                                GridRowNumber = (rowIndex + 1),
                                DataObject = item
                            };
                            this.OnManualRowBind(e);
                            e = null;
                            i++;
                        }
                    }
                    else
                    {
                        dgvRows.DataSource = query;
                    }
                }
                catch (Exception ex)
                {
                    Log.Exception(PROC, ex);                    
                }
            }

            try
            {

                if (dgvRows.Rows.Count > 0)
                    dgvRows.Rows[0].Selected = true;
                this.dgvRows_SelectionChanged(dgvRows, EventArgs.Empty);

                this.OnAfterDataBound(args);
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
            finally
            {
                _asyncActive = null;
            }
        }

        private void dgvRows_SelectionChanged(object sender, EventArgs e)
        {
            ModuleProc PROC = new ModuleProc("", "dgvRows_SelectionChanged");

            lock (_lockGrid)
            {
                try
                {
                    if (dgvRows.SelectedRows.Count > 0)
                    {
                        DataGridViewRow row = dgvRows.SelectedRows[0];
                        GridViewRowSelectedEventArgs args = new GridViewRowSelectedEventArgs()
                        {
                            Row = row,
                            DataObject = row.DataBoundItem
                        };
                        this.RowSelected(args);
                    }
                }
                catch (Exception ex)
                {
                    Log.Exception(PROC, ex);
                }
            }
        }

        private void dgvRows_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            ModuleProc PROC = new ModuleProc("", "dgvRows_CellClick");

            try
            {
                DataGridViewRow row = dgvRows.Rows[e.RowIndex];
                DataGridViewCell cell = row.Cells[e.ColumnIndex];
                GridViewCellClickedEventArgs args = new GridViewCellClickedEventArgs()
                {
                    Row = row,
                    Cell = cell
                };
                if (this.ManualDataBinding)
                {
                    args.DataObject = row.Tag;
                }
                else
                {
                    args.DataObject = row.DataBoundItem;
                }
                this.OnCellClicked(args);
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        public event GridViewRowSelectedHandler RowSelected = null;

        private void OnRowSelected(GridViewRowSelectedEventArgs e)
        {
            ModuleProc PROC = new ModuleProc("", "OnRowSelected");

            try
            {
                if (this.RowSelected != null)
                {
                    this.RowSelected(e);
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        public event GridViewAfterDataBoundHandler AfterDataBound = null;

        private void OnAfterDataBound(GridViewAfterDataBoundEventArgs e)
        {
            ModuleProc PROC = new ModuleProc("", "OnAfterDataBound");

            try
            {
                if (this.AfterDataBound != null)
                {
                    this.AfterDataBound(e);
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        public event GridViewManualRowBindHandler ManualRowBind = null;

        private void OnManualRowBind(ManualRowBindEventArgs e)
        {
            ModuleProc PROC = new ModuleProc("", "OnManualRowBind");

            try
            {
                if (this.ManualRowBind != null)
                {
                    this.ManualRowBind(e);
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        public event GridViewCellClickedHandler CellClicked = null;

        private void OnCellClicked(GridViewCellClickedEventArgs e)
        {
            ModuleProc PROC = new ModuleProc("", "OnManualRowBind");

            try
            {
                if (this.CellClicked != null)
                {
                    this.CellClicked(e);
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        private void btnMoveFirst_Click(object sender, EventArgs e)
        {
            cboPageNos.SelectedIndex = 0;
        }

        private void btnMovePrev_Click(object sender, EventArgs e)
        {
            cboPageNos.SelectedIndex -= 1;
        }

        private void btnMoveNext_Click(object sender, EventArgs e)
        {
            cboPageNos.SelectedIndex += 1;
        }

        private void btnMoveLast_Click(object sender, EventArgs e)
        {
            cboPageNos.SelectedIndex = (_pageTotal - 1);
        }
    }
}
