using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BMC.CoreLib.Diagnostics;
using BMC.CoreLib;
using BMC.EnterpriseClient.Helpers;
using BMC.CoreLib.Concurrent;
using BMC.CoreLib.Win32;
using System.Threading;
using BMC.EnterpriseBusiness.Business;
using BMC.EnterpriseDataAccess;
using BMC.EnterpriseBusiness.Entities;
using BMC.Common.ExceptionManagement;
using BMC.Common.LogManagement;
using BMC.Common;

namespace BMC.EnterpriseClient.Views
{
    public partial class UcVSAssets : UserControl, IUserControl2
    {
        private IViewSiteAssetViewer[] _reportViewers = null;
        private BuyMachineBiz objMachine = null;
        private AssetReportBiz objAssetReport = null;
        private frmSiteMachineControl _frmMachineControl = null;

        private const int CONTROL_TAB = 2;
        private const int GameReport_Tab = 3;
        private ListViewCustomSorter _lvCustomSorter = null;

        public UcVSAssets(IViewSiteInfo viewSite)
        {
            this.ViewSite = viewSite;
            InitializeComponent();
            this.Initialize();
            // Set Tags for controls
            SetTagProperty();
        }
        private void SetTagProperty()
        {
            try
            {
                this.tbpHistory.Tag = "Key_AssetHistory";
                this.tbpReport.Tag = "Key_AssetReport";
                this.lblCategory.Tag = "Key_CategoryColon";
                this.tbpControl.Tag = "Key_Control";
                this.lblFrom.Tag = "Key_FromColon";
                this.tbpGameReports.Tag = "Key_GameReports";
                this.lblTo.Tag = "Key_ToColon";
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
        private void Initialize()
        {
            if (BMC.CoreLib.Win32.Win32Extensions.IsInDesignMode()) return;
            ModuleProc PROC = new ModuleProc("", "Initialize");
            _lvCustomSorter = new ListViewCustomSorter(lvwDetails, this.ViewSite as Form);
            //Load all the categories initially
            this.LoadCategories();

            try
            {
                lvwDetails.ClipboardCopyMode = ListViewClipboardCopyMode.EnableWithHeaderText;
                lvwDetails.ClipboardCopyFormat = ListViewClipboardCopyFormat.Semicolon;
                dtpFromDate.Value = DateTime.Now.Date;
                Form parentForm = this.ViewSite as Form;
                if (parentForm != null)
                {
                    parentForm.FormClosing += new FormClosingEventHandler(ParentForm_FormClosing);
                }
                _reportViewers = new IViewSiteAssetViewer[] 
                {
                    new ViewSiteAssetReport(),
                    new ViewSiteAssetHistory(),
                    null,
                    new ViewSiteAssetGameReports(),
                };
                foreach (var reportViewer in _reportViewers)
                {
                    if (reportViewer != null)
                    {
                        reportViewer.View = lvwDetails;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        void ParentForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            ModuleProc PROC = new ModuleProc("", "ParentForm_FormClosing");

            try
            {
                if (_frmMachineControl != null)
                {
                    _frmMachineControl.Close();
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        public IViewSiteInfo ViewSite { get; set; }
        private void tabAsset_SelectedIndexChanged(object sender, EventArgs e)
        {
            ModuleProc PROC = new ModuleProc("", "tabAsset_SelectedIndexChanged");
            try
            {
                lvwDetails.Items.Clear();
                int index = tabAsset.SelectedIndex;
                tblContainer.RowStyles[1].Height = ((index == 0 || index == 3) ? 35 : 0);
                lvwDetails.Visible = (index != CONTROL_TAB);
                this.ViewSite.ShowHideExportbutton(index != CONTROL_TAB);
                this.ViewSite.Viewer = null;
                // control
                if (index == CONTROL_TAB)
                {
                    if (!AppGlobals.Current.HasUserAccess("hq_stock_machine_control"))
                    {
                        //BMC.CoreLib.Win32.Win32Extensions.ShowWarningMessageBox(null, "User is not having Permission to View Control Details");
                        BMC.CoreLib.Win32.Win32Extensions.ShowWarningMessageBox(null, this.GetResourceTextByKey(1, "MSG_SITEDETAILS_USERPERMISSION"), this.ParentForm.Text);
                        return;
                    }
                    // already loaded
                    if (_frmMachineControl != null)
                    {
                        _frmMachineControl.Activate();
                    }
                    else if (this.ViewSite.SelectedSite != null &&
                        this.ViewSite.SelectedSite.Site_ID != 0)
                    {
                        _frmMachineControl = new frmSiteMachineControl(this.ViewSite);
                        _frmMachineControl.ShowInTaskbar = false;
                        _frmMachineControl.FormClosing += new FormClosingEventHandler(OnfrmMachineControl_FormClosing);
                        if (DialogResult.Cancel == _frmMachineControl.ShowDialog())
                        {
                            this.tabAsset.SelectedTab = this.tabAsset.TabPages[0];
                        }
                    }
                }
                else
                {
                    if (index == GameReport_Tab)
                    {
                        cboCategory.Visible = false;
                        lblCategory.Visible = false;
                    }
                    else
                    {
                        cboCategory.Visible = true;
                        lblCategory.Visible = true;
                    }

                    // this.LoadControl();
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        void OnfrmMachineControl_FormClosing(object sender, FormClosingEventArgs e)
        {
            _frmMachineControl = null;
        }


        #region IUserControl Members

        public bool IsControlInitialized { get; set; }

        public void LoadControl()
        {
            ModuleProc PROC = new ModuleProc("UcVSAssets", "LoadControl");
            if (!this.IsControlInitialized || BMC.CoreLib.Win32.Win32Extensions.IsInDesignMode()) return;

            try
            {
                Form parentForm = this.ParentForm;
                IExecutorService exec = ExecutorServiceFactory.CreateExecutorService();
                IViewSiteAssetViewer viewer = _reportViewers[tabAsset.SelectedIndex];
                this.ViewSite.Viewer = viewer;
                int dataCount = 0;
                object entity = null;
                lvwDetails.Items.Clear();
                this.ViewSite.ShowHideExportbutton(tabAsset.SelectedIndex != CONTROL_TAB);

                #region Start & End Date validation
                if (dtpFromDate.Value > dtpToDate.Value)
                {
                    // BMC.CoreLib.Win32.Win32Extensions.ShowWarningMessageBox(null, "Start date & time cannot be greater than end date & time");
                    BMC.CoreLib.Win32.Win32Extensions.ShowWarningMessageBox(null, this.GetResourceTextByKey(1, "MSG_SITEDETAILS_STARTDATE"), this.ParentForm.Text);
                    return;
                }

                if (dtpFromDate.Value > DateTime.Now)
                {
                    //BMC.CoreLib.Win32.Win32Extensions.ShowWarningMessageBox(null, "Start date & time cannot be greater than current date & time");
                    BMC.CoreLib.Win32.Win32Extensions.ShowWarningMessageBox(null, this.GetResourceTextByKey(1, "MSG_SITEDETAILS_CURRENTDATE"), this.ParentForm.Text);
                    return;
                }

                if (dtpToDate.Value > DateTime.Now)
                {
                    // BMC.CoreLib.Win32.Win32Extensions.ShowWarningMessageBox(null, "End date & time cannot be greater than current date & time");
                    BMC.CoreLib.Win32.Win32Extensions.ShowWarningMessageBox(null, this.GetResourceTextByKey(1, "MSG_SITEDETAILS_ENDDATE"), this.ParentForm.Text);
                    return;
                }
                #endregion Start & End Date validation

                // fetching details
                BMC.CoreLib.Win32.Win32Extensions.ShowAsyncDialog(parentForm, this.GetResourceTextByKey(1,"MSG_VWA_FETCHASSET"), exec,
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
                        viewer.FromDate = dtpFromDate.Value;
                        viewer.ToDate = dtpToDate.Value;

                        o.CrossThreadInvoke(() =>
                        {
                            viewer.CategoryId = (cboCategory.SelectedItem as GetMachineTypeDetailsResult).Machine_Type_ID;
                            viewer.Category = cboCategory.Text;
                        });
                        entity = viewer.FetchData(out dataCount);
                    });

                // loading details
                try
                {
                    if (viewer.HasFetchTimeoutError)
                    {
                        // BMC.CoreLib.Win32.Win32Extensions.ShowWarningMessageBox(this.ViewSite as Form, "Unable to load the data due to Huge data available.");
                        BMC.CoreLib.Win32.Win32Extensions.ShowWarningMessageBox(this.ViewSite as Form, this.GetResourceTextByKey(1, "MSG_SITEDETAILS_LOADDATA"), this.ParentForm.Text);
                    }
                    else
                    {
                        if (entity != null && (dataCount > 0))
                        {
                            IList<ListViewItem> items = new List<ListViewItem>();

                            try
                            {
                                lvwDetails.SuspendLayout();

                                //BMC.CoreLib.Win32.Win32Extensions.ShowAsyncDialog(parentForm, "Drop - [Loading details...]", exec, 1, dataCount,
                                BMC.CoreLib.Win32.Win32Extensions.ShowAsyncDialog(parentForm, this.GetResourceTextByKey(1,"MSG_VWA_LOADINGASSEt"), exec,
                                    (o) =>
                                    {
                                        IAsyncProgress2 o2 = o as IAsyncProgress2;
                                        o.CloseOnComplete = true;

                                        int count = dataCount;
                                        for (int i = 0; i < dataCount; i++)
                                        {
                                            int proIdx = (i + 1);
                                            int proPerc = (int)(((float)proIdx / (float)count) * 100.0);
                                            o2.UpdateStatusProgress(i, string.Format(this.GetResourceTextByKey(1,"MSG_VWA_Load"), proIdx, count.ToString(), proPerc));
                                            //o2.UpdateStatusProgress(i, "Loading Asset Details . . . : " +
                                            //    proIdx + " of " + count.ToString() +
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
                                    });
                            }
                            finally
                            {
                                lvwDetails.Items.AddRange(items.ToArray());
                            }
                        }
                    }
                    this.ResolveResources();
                }
                finally
                {
                    lvwDetails.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
                    lvwDetails.ResumeLayout(true);
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
        public bool SaveControl()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region "New methods"

        /// <summary>
        /// Load all the available categories from DB.
        /// <param>No</param>
        /// <value>loads the categories in combobox</value>
        /// </summary>
        private void LoadCategories()
        {
            try
            {
                //Initialise the business object.
                objMachine = BuyMachineBiz.CreateInstance();
                List<GetMachineTypeDetailsResult> lstMachineType = null;
                try
                {
                    lstMachineType = objMachine.GetMachineTypeDetails(null);
                }
                catch { }
                if (lstMachineType == null)
                {
                    lstMachineType = new List<GetMachineTypeDetailsResult>();
                }
                lstMachineType.Insert(0, new GetMachineTypeDetailsResult()
                {
                    Machine_Type_Code = this.GetResourceTextByKey("Key_AllCriteria"),
                    Machine_Type_ID = -1
                });

                //Set the datasource
                cboCategory.DropDownStyle = ComboBoxStyle.DropDownList;
                var lst = lstMachineType.Where((x) => x.IsNonGamingAssetType != 1).Select((x) => x).ToList();
                cboCategory.DataSource = lst;
                cboCategory.DisplayMember = "Machine_Type_Code";
                cboCategory.ValueMember = "Machine_Type_ID";
                if (cboCategory.Items.Count > 0)
                {
                    cboCategory.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Could not load the categories...", LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
            }
        }

        #endregion

        private void cboCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            //LoadControl();
            lvwDetails.Items.Clear();
        }

        #region IUserControl2 Members

        public void ClearControl()
        {
            ModuleProc PROC = new ModuleProc("", "ClearControl");

            try
            {
                lvwDetails.Items.Clear();
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        #endregion

        private void UcVSAssets_Load(object sender, EventArgs e)
        {
            try
            {
                this.ResolveResources();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
    }
}
