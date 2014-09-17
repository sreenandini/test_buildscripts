using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BMC.EnterpriseClient.Helpers;
using BMC.CoreLib.Diagnostics;
using BMC.CoreLib;
using BMC.CoreLib.Concurrent;
using BMC.CoreLib.Win32;
using System.Threading;
using BMC.EnterpriseBusiness.Entities;
using BMC.EnterpriseBusiness.Business;
using BMC.Common.ExceptionManagement;
using BMC.Common;


namespace BMC.EnterpriseClient.Views
{
    public partial class UcVSDrop : UserControl, IUserControl2, IUserControl3
    {
        private IViewSiteDropViewer[] _reportViewers = null;
        private IDictionary<int, CPeriodUnitsType> _periodUnits = null;
        private IDictionary<int, CPeriodUnitsType> _periodDefaultUnits = null;
        private IDictionary<int, int> _periodDefaultCounts = null;

        private VSSiteTreeEntity _oldSite = null;
        private int _oldTabIndex = -1;
        private ContextMenuStrip _assignedMenu = null;
        //private ListViewColumnSorter _lvwColumnSorter = null;
        private ListViewCustomSorter _lvCustomSorter = null;
        bool bFirst = true;
        private enum DropIndexes
        {
            Position = 0,
            Batch = 1,
            Week = 2
        }
        private IList<string> _tabDescs = new List<string>()
        {
            "Position", "Batch", "Week"
        };

        public UcVSDrop(IViewSiteInfo viewSite)
        {
            this.ViewSite = viewSite;
            InitializeComponent();
            this.Initialize();
            ChangeContextMenuStripContents();
            //_lvwColumnSorter = new ListViewColumnSorter();
            //lvwDetails.ListViewItemSorter = _lvwColumnSorter;
            lvwDetails.ClipboardCopyMode = ListViewClipboardCopyMode.EnableWithHeaderText;
            lvwDetails.ClipboardCopyFormat = ListViewClipboardCopyFormat.Semicolon;
            _lvCustomSorter = new ListViewCustomSorter(lvwDetails, this.ViewSite as Form);
            this.cPeriodUnits.Items.AddRange(new object[] {
            this.GetResourceTextByKey("Key_Records"),
            this.GetResourceTextByKey("Key_Weeks"),
            this.GetResourceTextByKey("Key_Periods")});
            SetTagProperty();            
        }

        private void ChangeContextMenuStripContents()
        {
            try
            {
                ctxMenuColl.Items["ctxMenuItemCollDetails"].Text = this.GetResourceTextByKey("Key_UcVSDrop_Collection");
                ctxMenuColl.Items["ctxMenuItemCollRemarks"].Text = this.GetResourceTextByKey("Key_UcVSDrop_Remarks");
                ctxMenuBatch.Items["ctxMenuItemBatchMerge"].Text = this.GetResourceTextByKey("Key_UcVSDrop_MergeBatch");
                ctxMenuBatch.Items["ctxMenuItemBatchDelete"].Text = this.GetResourceTextByKey("Key_UcVSDrop_DeleteBatch");
                ctxMenuBatch.Items["deMergeBatchToolStripMenuItem"].Text = this.GetResourceTextByKey("Key_UcVSDrop_DeMerge");
            }
            catch (Exception ex )
            {
                ExceptionManager.Publish(ex);
            }
        }
        
        private void SetTagProperty()
        {
            try
            {
                this.tbpBatch.Tag = "Key_Batch";
                this.ctxMenuItemBatchDelete.Tag = "Key_DeleteCollectionBatch";
                this.deMergeBatchToolStripMenuItem.Tag = "Key_DeMergeBatch";
                this.ctxMenuItemBatchMerge.Tag = "Key_MergeAnotherBatch";
                this.tbpPosition.Tag = "Key_Position";
                this.ctxMenuItemCollRemarks.Tag = "Key_Remarks";
                this.ctxMenuItemCollDetails.Tag = "Key_ViewCollectionDetails";
                this.tbpWeek.Tag = "Key_Week";
                this.cPeriodCount.Tag = "Key_AllCriteria";
                this.Tag = "Key_ViewSites";
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
        }

        private void Initialize()
        {
            if (BMC.CoreLib.Win32.Win32Extensions.IsInDesignMode()) return;
            ModuleProc PROC = new ModuleProc("", "Initialize");

            try
            {
                _reportViewers = new IViewSiteDropViewer[] 
                {
                    new ViewSiteCollectionDetails(),
                    new ViewSiteCollectionBatch(),
                    new ViewSiteCollectionWeek(),
                };
                _periodUnits = new SortedDictionary<int, CPeriodUnitsType>()
                {
                    { 0, (CPeriodUnitsType.Records | CPeriodUnitsType.Weeks | CPeriodUnitsType.Periods) },
                    { 1, (CPeriodUnitsType.Periods | CPeriodUnitsType.Weeks) },
                    { 2, (CPeriodUnitsType.Weeks) },
                };
                _periodDefaultUnits = new SortedDictionary<int, CPeriodUnitsType>()
                {
                    { 0, (CPeriodUnitsType.Records) },
                    { 1, (CPeriodUnitsType.Weeks) },
                    { 2, (CPeriodUnitsType.Weeks) },
                };
                _periodDefaultCounts = new SortedDictionary<int, int>()
                {
                    { 0, 12 },
                    { 1, 12 },
                    { 2, 12 },
                };
                foreach (var reportViewer in _reportViewers)
                {
                    reportViewer.View = lvwDetails;
                }
                this.Activated();
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        private void tabDrop_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Activated();
            lvwDetails.Items.Clear();
            this.ViewSite.Viewer = null;
            // this.LoadControl();
        }

        private void tabDrop_Deselected(object sender, TabControlEventArgs e)
        {
            this.DeActivated();
        }

        public IViewSiteInfo ViewSite { get; set; }

        #region IUserControl Members

        public bool IsControlInitialized { get; set; }

        public void LoadControl()
        {
            ModuleProc PROC = new ModuleProc("UcHourlyDetails", "LoadControl");
            if (!this.IsControlInitialized || BMC.CoreLib.Win32.Win32Extensions.IsInDesignMode()) return;

            try
            {
                this.AssignContextMenu();
                int dataCount = 0;
                object entity = null;
                IList<ListViewItem> items = new List<ListViewItem>();

                int selectedIndex = tabDrop.SelectedIndex;
                CPeriodUnitsType unit = cPeriodUnits.SelectedUnit;
                int count = cPeriodCount.SelectedCount;
                IViewSiteDropViewer viewer = _reportViewers[selectedIndex];
                string tabDesc = _tabDescs[selectedIndex];

                // skip the refresh (No need for refreshing the data based upon the installation)
                if (viewer.SkipRefresh)
                {
                    if (!(this.ViewSite.SelectedSite != _oldSite ||
                        selectedIndex != _oldTabIndex))
                    {
                        return;
                    }
                }

                Form parentForm = this.ParentForm;
                IExecutorService exec = ExecutorServiceFactory.CreateExecutorService();

                this.ViewSite.Viewer = viewer;
                lvwDetails.Items.Clear();

                try
                {
                    lvwDetails.SuspendLayout();

                    // fetching details
                    BMC.CoreLib.Win32.Win32Extensions.ShowAsyncDialog(parentForm, string.Format(this.GetResourceTextByKey(1, "MSG_DROP_FETCH"), tabDesc), exec,
                        (o) =>
                        {
                            o.CloseOnComplete = true;
                            o.CrossThreadInvoke(() =>
                            {
                                viewer.InitReport();
                            });

                            viewer.BarPositionId = 0;
                            viewer.BarPosition = string.Empty;
                            viewer.SiteId = 0;
                            viewer.Company = string.Empty;
                            viewer.SubCompany = string.Empty;
                            viewer.SiteName = string.Empty;

                            if (this.ViewSite.SelectedInstallation != null)
                            {
                                viewer.BarPositionId = this.ViewSite.SelectedInstallation.Bar_Position_ID;
                                viewer.BarPosition = this.ViewSite.SelectedInstallation.Bar_Position_Name;
                            }
                            if (this.ViewSite.SelectedSite != null)
                            {
                                viewer.SiteId = this.ViewSite.SelectedSite.Site_ID;
                                viewer.Company = this.ViewSite.SelectedSite.Company_Name;
                                viewer.SubCompany = this.ViewSite.SelectedSite.Sub_Company_Name;
                                viewer.SiteName = this.ViewSite.SelectedSite.Site_Name;
                            }
                            viewer.Records = (unit == CPeriodUnitsType.Records ? count : -1);
                            viewer.Weeks = (unit == CPeriodUnitsType.Weeks ? count : -1);
                            viewer.Periods = (unit == CPeriodUnitsType.Periods ? count : -1);
                            entity = viewer.FetchData(o, out dataCount);

                            // loading details
                            if (entity != null && (dataCount > 0))
                            {
                                IAsyncProgress2 o2 = o as IAsyncProgress2;
                                o.CloseOnComplete = true;
                                o2.InitializeProgress(1, dataCount);

                                int count2 = dataCount;
                                for (int i = 0; i < dataCount; i++)
                                {
                                    int proIdx = (i + 1);
                                    int proPerc = (int)(((float)proIdx / (float)count2) * 100.0);
                                    o2.UpdateStatusProgress(i, string.Format(this.GetResourceTextByKey(1, "MSG_DROP_LOAD"), tabDesc, proIdx, count2.ToString(), proPerc));
                                    //o2.UpdateStatusProgress(i, "Loading Drop " + tabDesc + " Details . . . : " + 
                                    //    proIdx + " of " + count2.ToString() +
                                    //    " (" + proPerc + "%)");

                                    o.CrossThreadInvoke(() =>
                                    {
                                        ListViewItem item = viewer.FillReport(entity, i);
                                        if (item != null)
                                        {
                                            items.Add(item);
                                        }
                                    });
                                    Thread.Sleep(1);
                                }
                            }
                        });
                   
                    this.ResolveResources();
                }
                finally
                {
                    lvwDetails.Items.AddRange(items.ToArray());
                    lvwDetails.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
                    lvwDetails.ResumeLayout(true);
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
            finally
            {
                _oldSite = this.ViewSite.SelectedSite;
                _oldTabIndex = tabDrop.SelectedIndex;
            }
        }

        public bool SaveControl()
        {
            throw new NotImplementedException();
        }

        #endregion

        private void AssignContextMenu()
        {
            ModuleProc PROC = new ModuleProc("", "lvwDetails_MouseUp");

            try
            {
                DropIndexes index = (DropIndexes)tabDrop.SelectedIndex;
                lvwDetails.ContextMenuStrip = null;
                _assignedMenu = null;

                switch (index)
                {
                    case DropIndexes.Position:
                        _assignedMenu = ctxMenuColl;
                        break;
                    case DropIndexes.Batch:
                        _assignedMenu = ctxMenuBatch;
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        private void lvwDetails_DoubleClick(object sender, EventArgs e)
        {
            ModuleProc PROC = new ModuleProc("", "Method");

            try
            {
                if (lvwDetails.SelectedItems.Count > 0)
                {
                    IViewSiteDropViewer viewer = _reportViewers[tabDrop.SelectedIndex];
                    viewer.ShowDialog(lvwDetails.SelectedItems[0].Tag);
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        public void ClearItems()
        {
            lvwDetails.Items.Clear();
        }

        #region IUserControl2 Members

        public void ClearControl()
        {
            ModuleProc PROC = new ModuleProc("", "ClearControl");

            try
            {
                _oldTabIndex = -1;
                lvwDetails.Items.Clear();
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        #endregion

        private void lvwDetails_SelectedIndexChanged(object sender, EventArgs e)
        {
            ModuleProc PROC = new ModuleProc("", "lvwDetails_SelectedIndexChanged");

            try
            {
                lvwDetails.ContextMenuStrip = null;
                if (lvwDetails.SelectedItems != null &&
                    lvwDetails.SelectedItems.Count > 0)
                {
                    lvwDetails.ContextMenuStrip = _assignedMenu;
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }
        private void ctxMenuItemCollRemarks_Click(object sender, EventArgs e)
        {
            ModuleProc PROC = new ModuleProc("", "Collection_remarks");
            try
            {
                VSDropPositionEntity entity = ((VSDropPositionEntity)lvwDetails.SelectedItems[0].Tag);
                if (entity == null) return;
                int collection_id = entity.Collection_ID;
                string Collection_remarks = entity.Remarks;
                if (BMC.CoreLib.Win32.Win32Extensions.ShowDialogExAndDestroy(new frmCollectionRemarks(collection_id, Collection_remarks), this, null, null))
                {
                    this.LoadControl();
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }
        private void ctxMenuItemBatchDelete_Click(object sender, EventArgs e)
        {
            ModuleProc PROC = new ModuleProc("", "ctxMenuItemBatchDelete_Click");

            try
            {
                if (lvwDetails.SelectedItems != null &&
                    lvwDetails.SelectedItems.Count > 0)
                {
                    VSDropBatch2Entity entity = lvwDetails.SelectedItems[0].Tag as VSDropBatch2Entity;
                    if (entity != null)
                    {
                        if (BMC.CoreLib.Win32.Win32Extensions.ShowQuestionMessageBox(this, this.GetResourceTextByKey(1, "MSG_VSDROP_DEL_BATCH"), this.Text) == DialogResult.Yes)
                        {
                            bool result = this.ViewSite.Business.DeleteCollectionBatch(entity.Batch_ID.SafeValue());
                            if (result)
                            {
                                BMC.CoreLib.Win32.Win32Extensions.ShowInfoMessageBox(this,string.Format(this.GetResourceTextByKey(1, "MSG_VSDROP_DEL_BATCH_SUCCESS"),lvwDetails.SelectedItems[0].Text), this.Text);
                                this.ViewSite.Reload();
                            }
                            else
                                BMC.CoreLib.Win32.Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_VSDROP_DEL_BATCH_FAILURE"), this.Text);
                        }
                    }
                }
                
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }



        private void lvwDetails_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            /*
            if (e.Column == _lvwColumnSorter.SortColumn)
            {
                // Reverse the current sort direction for this column.
                if (_lvwColumnSorter.Order == System.Windows.Forms.SortOrder.Ascending)
                {
                    _lvwColumnSorter.Order = System.Windows.Forms.SortOrder.Descending;
                }
                else
                {
                    _lvwColumnSorter.Order = System.Windows.Forms.SortOrder.Ascending;
                }
            }
            else
            {
                // Set the column number that is to be sorted; default to ascending.
                _lvwColumnSorter.SortColumn = e.Column;
                _lvwColumnSorter.Order = System.Windows.Forms.SortOrder.Ascending;
            }
            ListView lst_view = sender as ListView;
            lst_view.Sort();
            */

        }

        private void ctxMenuItemCollDetails_Click(object sender, EventArgs e)
        {
            ModuleProc PROC = new ModuleProc("", "Method");

            try
            {
                if (lvwDetails.SelectedItems.Count > 0)
                {
                    IViewSiteDropViewer viewer = _reportViewers[tabDrop.SelectedIndex];
                    viewer.ShowDialog(lvwDetails.SelectedItems[0].Tag);
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        private void ctxMenuItemCollDetails_Click_1(object sender, EventArgs e)
        {

            ModuleProc PROC = new ModuleProc("", "Method");

            try
            {
                if (lvwDetails.SelectedItems.Count > 0)
                {
                    IViewSiteDropViewer viewer = _reportViewers[tabDrop.SelectedIndex];
                    viewer.ShowDialog(lvwDetails.SelectedItems[0].Tag);
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        public void Activated()
        {
            ModuleProc PROC = new ModuleProc("", "Activated");

            try
            {
                int index = tabDrop.SelectedIndex;
                cPeriodUnits.Fill(_periodUnits[index]);
                cPeriodUnits.SelectedUnit = this.ViewSite.GetSelectedPeriodUnit(ViewSiteHelper.TAB_DROP, tabDrop.SelectedIndex, _periodDefaultUnits[index]);
                cPeriodCount.SelectedCount = this.ViewSite.GetSelectedPeriodCount(ViewSiteHelper.TAB_DROP, tabDrop.SelectedIndex, _periodDefaultCounts[index]);
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        public void DeActivated()
        {
            ModuleProc PROC = new ModuleProc("", "Deactivated");

            try
            {
                this.ViewSite.SetSelectedPeriodCount(ViewSiteHelper.TAB_DROP, tabDrop.SelectedIndex, cPeriodCount.SelectedCount);
                this.ViewSite.SetSelectedPeriodUnit(ViewSiteHelper.TAB_DROP, tabDrop.SelectedIndex, cPeriodUnits.SelectedUnit);
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }
      
        private void ctxMenuItemBatchMerge_Click(object sender, EventArgs e)
        {
            ModuleProc PROC = new ModuleProc("", "BatchMerge");
            try
            {
                frmInputBox mergebox = new frmInputBox(string.Format(this.GetResourceTextByKey(1, "MSG_DROP_MERGEBATCH"), Environment.NewLine), string.Format(this.GetResourceTextByKey(1, "MSG_DROP_MERGEBATCH"), Environment.NewLine), "", true);                
                mergebox.ShowDialog();
                VSDropBatch2Entity entity = ((VSDropBatch2Entity)lvwDetails.SelectedItems[0].Tag);
                IViewSiteDropViewer viewer = _reportViewers[1];
                this.ViewSite.Viewer = viewer;
                int SiteId = viewer.SiteId;
                int MergeBatchvalue = Convert.ToInt32(entity.Batch_ID);               
                int DeletedBatchNo = 0;
                int.TryParse(mergebox.TextValue.Trim(), out DeletedBatchNo);
                if (MergeBatchvalue == DeletedBatchNo || DeletedBatchNo == 0)
                {
                    if (DeletedBatchNo == 0)
                    {
                        BMC.CoreLib.Win32.Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_DROP_NOT_VALID"), this.Text);
                    }
                    else
                    {
                        BMC.CoreLib.Win32.Win32Extensions.ShowInfoMessageBox(this, string.Format(this.GetResourceTextByKey(1, "MSG_DROP_NOT_SAME"),MergeBatchvalue, DeletedBatchNo), this.Text);
                    }
                    return;
                }
                VSMergeBatchBusiness obj = new VSMergeBatchBusiness();
                if(obj.IsBatchAvailableCheck(DeletedBatchNo, SiteId))
                {
                    obj.CreateMergeBatchCheck(DeletedBatchNo, MergeBatchvalue);
                    obj.MergeBatchCheck(DeletedBatchNo, MergeBatchvalue);
                    obj.ProcessNegativeNetcheck(DeletedBatchNo > MergeBatchvalue ? MergeBatchvalue : DeletedBatchNo, SiteId, true);
                    obj.DeleteBatchCheck(DeletedBatchNo);
                    obj.AuditMergeBatch(MergeBatchvalue, DeletedBatchNo, SiteId);
                    _oldTabIndex = -1;
                    this.ViewSite.Reload();
                    BMC.CoreLib.Win32.Win32Extensions.ShowInfoMessageBox(this, string.Format(this.GetResourceTextByKey(1, "MSG_MERGE_BATCH_SUCCESS"), DeletedBatchNo, MergeBatchvalue), this.Text); 
                }
                else
                {
                    BMC.CoreLib.Win32.Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_DROP_MISSING_BATCH"), this.Text);

                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        private void deMergeBatchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ModuleProc PROC = new ModuleProc("", "deMergeBatch");
            try
            {
                frmInputBox mergebox = new frmInputBox(string.Format(this.GetResourceTextByKey(1,"MSG_DROP_DEMERGEBATCH"), Environment.NewLine), string.Format(this.GetResourceTextByKey(1,"MSG_DROP_DEMERGEBATCH"), Environment.NewLine));
                mergebox.ShowDialog();
                VSDropBatch2Entity entity = ((VSDropBatch2Entity)lvwDetails.SelectedItems[0].Tag);
                IViewSiteDropViewer viewer = _reportViewers[1];
                this.ViewSite.Viewer = viewer;
                int SiteId = viewer.SiteId;
                int MergeBatchvalue = Convert.ToInt32(entity.Batch_ID);
                int DeletedBatchNo = Convert.ToInt32(mergebox.TextValue);
                if (MergeBatchvalue == DeletedBatchNo)
                {
                    BMC.CoreLib.Win32.Win32Extensions.ShowInfoMessageBox(this, string.Format(this.GetResourceTextByKey(1, "MSG_DROP_NOT_SAME"),MergeBatchvalue, DeletedBatchNo), this.Text);
                }
                VSMergeBatchBusiness obj = new VSMergeBatchBusiness();
                obj.IDM_DEMERGE_COL_BATCH(DeletedBatchNo, MergeBatchvalue, SiteId);

                _oldTabIndex = -1;
                this.ViewSite.Reload();
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }

        }

        private void UcVSDrop_Load(object sender, EventArgs e)
        {
            this.ResolveResources();
        }

    }
}
