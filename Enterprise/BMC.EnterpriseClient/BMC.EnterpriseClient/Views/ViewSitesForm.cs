using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BMC.CoreLib.Diagnostics;
using BMC.CoreLib;
//using BMC.EnterpriseClient.Helpers;
using BMC.EnterpriseBusiness.Entities;
using BMC.EnterpriseBusiness.Business;
using BMC.CoreLib.Win32;
using System.Threading;
using BMC.SecurityVB;
using BMC.CoreLib.Registry;
using BMC.CoreLib.Concurrent;
using BMC.EnterpriseClient.Helpers;
using System.IO;
using BMC.EnterpriseClient.CashierTransactions;
using BMC.Common;

namespace BMC.EnterpriseClient.Views
{
    public partial class ViewSitesForm : FormBase, IViewSiteInfo
    {
        private ViewSitesBusiness _business = new ViewSitesBusiness();
        TreeNode _selectedNode = null;
        TreeNode _prvSelectedNode = new TreeNode();
        private string prev_scode = "";

        private bool _isSiteLoading = false;
        private IDictionary<string, int> _siteIndexes = new SortedDictionary<string, int>(StringComparer.InvariantCultureIgnoreCase);
        private BMC.EnterpriseClient.Helpers.AppSubTaskLink _linkCashierTransactions = null;
        private BMCSecurityCallMethod _bmcSecurityMethod = null;
        private bool _isSearchSitesLoading = false;

        private UxHeaderContent uxSites = null;
        private System.Windows.Forms.TreeView tvwSites;
        private UcVSSiteDetails _ucSite = null;
        private UcVSInstallations _ucInstallations = null;

        private UcVSDrop _ucDrop = null;
        private UcVSHourlyDetails _ucHourly = null;
        private UcVSAssets _ucAssets = null;

        private ViewSiteSearchEntity _searchByText = null;
        private ViewSiteSearchEntity _searchByForm = null;

        private IDictionary<KeyValuePair<int, int>, int> _selectedPeriodCounts = new Dictionary<KeyValuePair<int, int>, int>();
        private IDictionary<KeyValuePair<int, int>, CPeriodUnitsType> _selectedPeriodUnits = new Dictionary<KeyValuePair<int, int>, CPeriodUnitsType>();
        private string _formCaption = string.Empty;
        private TabPage _oldTabPage = null;
        private bool _isFormInitializing = true;
        private bool _isFormLoading = true;
        private class CompanyTag
        {
            public int CompanyId { get; set; }
        }

        private class SubCompanyTag
        {
            public int SubCompanyId { get; set; }
        }

        private class TreeDictionary : SortedDictionary<string, TreeNode>
        {
            public TreeDictionary()
                : base(StringComparer.InvariantCultureIgnoreCase) { }
        }

        public ViewSitesForm()
        {
            try
            {
                _isFormInitializing = true;
                this.InitSearchEntities();
                InitializeComponent();
                SetTagProperty();
                this.InitTreeView();
                this.Executor = ExecutorServiceFactory.CreateExecutorService();
                chkDisplay.Checked = true;
            }
            finally
            {
                _isFormInitializing = false;
            }
        }

        public void ShowHideExportbutton(bool show)
        {
            btnExport.Visible = show;
        }

        private void InitTreeView()
        {
            // 
            // tvwSites
            // 
            this.tvwSites = new TreeView();
            this.tvwSites.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvwSites.FullRowSelect = true;
            this.tvwSites.HideSelection = false;
            this.tvwSites.ImageIndex = 0;
            this.tvwSites.ImageList = this.imglstSmallIcons;
            this.tvwSites.Location = new System.Drawing.Point(0, 0);
            this.tvwSites.Margin = new System.Windows.Forms.Padding(0);
            this.tvwSites.Name = "tvwSites";
            this.tvwSites.SelectedImageIndex = 0;
            this.tvwSites.Size = new System.Drawing.Size(294, 465);
            this.tvwSites.TabIndex = 0;
            this.tvwSites.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvwSites_AfterSelect);

            this.uxSites = new UxHeaderContent();
            this.uxSites.ChildContainer.Controls.Add(this.tvwSites);
            this.uxSites.ChildContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uxSites.ChildContainer.Location = new System.Drawing.Point(0, 26);
            this.uxSites.ChildContainer.Name = "Child";
            this.uxSites.ChildContainer.Size = new System.Drawing.Size(294, 465);
            this.uxSites.ChildContainer.TabIndex = 2;
            this.uxSites.ContentPadding = new System.Windows.Forms.Padding(0);
            this.uxSites.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uxSites.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(149)))), ((int)(((byte)(192)))));
            this.uxSites.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uxSites.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.uxSites.HeaderText = this.GetResourceTextByKey("Key_AvailableSites");
            this.uxSites.IsClosable = false;
            this.uxSites.Location = new System.Drawing.Point(3, 3);
            this.uxSites.Name = "uxSites";
            this.uxSites.PinVisible = false;
            this.uxSites.Size = new System.Drawing.Size(294, 491);
            this.uxSites.StartColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(64)))), ((int)(((byte)(114)))));
            this.uxSites.TabIndex = 0;
            this.tblSites.Controls.Add(this.uxSites, 0, 0);
        }

        public void SetTagProperty()
        {
            this.tbpInstallations.Tag = "Key_CurrentInstallations";
            this.tbpSiteDetails.Tag = "Key_SiteDetails";
            this.tbpAssets.Tag = "Key_Assets";
            this.tbpHourly.Tag = "Key_Hourly";
            this.tbpDrop.Tag = "Key_Drop";
            this.btnCashierTransactions.Tag = "Key_CashierHistory";
            this.btnClose.Tag = "Key_Close";
            this.btnExport.Tag = "Key_Export";
            this.btnFilter.Tag = "Key_Filter";
            this.btnRefresh.Tag = "Key_Load";
            this.btnTree.Tag = "Key_Tree";
            this.chkDisplay.Tag = "Key_Display";
            this.chkDisplayActiveSites.Tag = "Key_DisplayActiveSitesOnly";

            this.ctxMenuItemSite.Text = this.GetResourceTextByKey("Key_SiteAdministration");
            this.ctxMenuItemCompany.Text = this.GetResourceTextByKey("Key_CompanyAdministration");
            this.ctxMenuItemSubCompany.Text = this.GetResourceTextByKey("Key_SubCompanyAdministration");
            this.Tag = "Key_ViewSites";            
           
        }
        private void AssignContextMenu(TreeNode curNode)
        {
            ModuleProc PROC = new ModuleProc("", "tvwSites_MouseUp");

            try
            {
                if (curNode != null)
                {
                    curNode.ContextMenuStrip = null;
                    if (curNode.Tag is VSSiteTreeEntity)
                    {
                        if (AppGlobals.Current.HasUserAccess("HQ_Admin_Customers_Site"))
                        {
                            curNode.ContextMenuStrip = ctxMenuSite;
                        }
                    }
                    else if (curNode.Tag is CompanyTag)
                    {
                        if (AppGlobals.Current.HasUserAccess("HQ_Admin_Customers_Company"))
                        {
                            curNode.ContextMenuStrip = ctxMenuCompany;
                        }
                    }
                    else if (curNode.Tag is SubCompanyTag)
                    {
                        if (AppGlobals.Current.HasUserAccess("HQ_Admin_Customers_Sub"))
                        {
                            curNode.ContextMenuStrip = ctxMenuSubCompany;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        private void InitSearchEntities()
        {
            _searchByText = new ViewSiteSearchEntity()
                {
                    UserId = AppEntryPoint.Current.StaffId// AppGlobals.Current.UserId
                };
            _searchByForm = new ViewSiteSearchEntity()
           {
               UserId = AppEntryPoint.Current.StaffId,//AppGlobals.Current.UserId,
               ExcludeVacant = false
           };
        }

        protected override void LoadChanges()
        {
            this.SuspendLayout();
            try
            {
                base.LoadChanges();
                _formCaption = this.Text;

                _oldTabPage = tabDetails2.TabPages[0];
                _getSelectedSite = new Func<VSSiteTreeEntity>(this.GetSelectedSitePrivate);
                _bmcSecurityMethod = new BMCSecurityCallMethod();
                this.AdminUtilities = new BMC.EnterpriseClient.Helpers.frmAdminUtilities();
                ViewSiteHelper.FillSearchCombo(cboSiteSearch, -1, "ViewSiteForm_SearchText");

                tblSites.Visible = false;
                cboSites.DisplayMember = "DisplayName";
                cboSites.ValueMember = "Site_ID";
                _linkCashierTransactions = new BMC.EnterpriseClient.Helpers.AppSubTaskLink(btnCashierTransactions,
                    (g) =>
                    {
                        return g.HasUserAccess("HQ_CashierTransactions");
                    }
                    , "BMCCashierTransactions.exe", this.GetEncryptedUserInfo);
                btnCashierTransactions.Enabled = AppGlobals.Current.HasUserAccess("HQ_CashierTransactions");
                //   _linkCashierTransactions.IsExternalFileExists;                                     
                this.CreateChildControls();
                this.ShowHideSiteTree();
                cboSiteSearch.Text=string.Empty;
            }
            finally
            {
                this.ResumeLayout(true);
                ThreadPool.QueueUserWorkItem((o) =>
                {
                    Thread.Sleep(100);
                    this.CrossThreadInvoke(new Action(() =>
                    {
                        this.LoadSiteTree(_searchByText);
                    }));
                });
            }
        }

        private void CreateChildControls()
        {
            ModuleProc PROC = new ModuleProc("ViewSitesForm", "CreateChildControls");

            try
            {
                tbpSiteDetails.Controls.Add((_ucSite = new UcVSSiteDetails(this) { Dock = DockStyle.Fill }));
                tbpInstallations.Controls.Add((_ucInstallations = new UcVSInstallations(this) { Dock = DockStyle.Fill }));

                tbpDrop.Controls.Add((_ucDrop = new UcVSDrop(this) { Dock = DockStyle.Fill }));
                tbpHourly.Controls.Add((_ucHourly = new UcVSHourlyDetails(this) { Dock = DockStyle.Fill }));
                tbpAssets.Controls.Add((_ucAssets = new UcVSAssets(this) { Dock = DockStyle.Fill }));
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        private string[] GetEncryptedUserInfo()
        {
            return new string[] 
            {
                _bmcSecurityMethod.Encrypt(this.SelectedSite.Site_ID.ToString()) + "[DELIM]" +
                _bmcSecurityMethod.Encrypt(AppGlobals.Current.LoggedinUser.SecurityUserID.ToString())
            };
        }

        private Func<VSSiteTreeEntity> _getSelectedSite = null;

        private VSSiteTreeEntity GetSelectedSitePrivate()
        {
            ModuleProc PROC = new ModuleProc("ViewSitesForm", "GetSelectedSiteIdPrivate");

            try
            {
                if (cboSites.SelectedItem != null)
                {
                    VSSiteTreeEntity entity = cboSites.SelectedItem as VSSiteTreeEntity;
                    if (entity != null)
                    {
                        return entity;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
            return null;
        }

        public VSSiteTreeEntity SelectedSite
        {
            get
            {
                return (VSSiteTreeEntity)BMC.CoreLib.Win32.Win32Extensions.CrossThreadInvokeFunc(this.ParentForm, _getSelectedSite);
            }
        }

        public VSInstallationEntity SelectedInstallation
        {
            get
            {
                if (_ucInstallations != null)
                {
                    return _ucInstallations.SelectedInstallation;
                }
                return null;
            }
        }

        public BMC.EnterpriseClient.Helpers.frmAdminUtilities AdminUtilities { get; private set; }

        public ViewSitesBusiness Business { get { return _business; } }

        public string FormCaption { get; set; }

        private void ClearUserControls(params UserControl[] ucs)
        {
            foreach (var uc in ucs)
            {
                this.ClearUserControl(uc);
            }
        }

        private void ClearUserControl(UserControl uc)
        {
            IUserControl2 iuc = uc as IUserControl2;
            if (iuc != null) iuc.ClearControl();
        }

        private void ClearActiveControl()
        {
            ModuleProc PROC = new ModuleProc("", "ClearActiveControl");

            try
            {
                TabPage tbp = tabDetails2.SelectedTab;
                if (tbp != null && tbp.Controls.Count > 0)
                {
                    IUserControl2 uc2 = tbp.Controls[0] as IUserControl2;
                    if (uc2 != null)
                    {
                        uc2.ClearControl();
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        private void LoadSiteTree(ViewSiteSearchEntity search)
        {
            ModuleProc PROC = new ModuleProc("ViewSitesForm", "LoadSiteTree");
            TreeDictionary dicCompanies = new TreeDictionary();
            TreeDictionary dicSubCompanies = new TreeDictionary();
            tvwSites.Nodes.Clear();
            cboSites.Items.Clear();
            _isSiteLoading = true;

            try
            {
                VSSiteTreesEntity entity = null;
                _selectedNode = null;

                this.ClearUserControls(new UserControl[] 
                {
                    _ucSite,
                    _ucInstallations,
                    _ucDrop,
                    _ucHourly,
                    _ucAssets
                });

                // fetching details
                BMC.CoreLib.Win32.Win32Extensions.ShowAsyncDialog(this, this.GetResourceTextByKey(1, "MSG_VWS_FET"), this.Executor,
                    (o) =>
                    {
                        o.CloseOnComplete = true;
                        entity = _business.GetSiteTree(search);
                    });

                // loading details
                if (entity != null && entity.Count != 0)
                {
                    BMC.CoreLib.Win32.Win32Extensions.ShowAsyncDialog(this, this.GetResourceTextByKey(1, "MSG_VWS_LOAD"), this.Executor, 1, entity.Count,
                        //BMC.CoreLib.Win32.Win32Extensions.ShowAsyncDialogContinuous(this, "Sites - [Loading details...]", this.Executor, 
                        (o) =>
                        {
                            IAsyncProgress2 o2 = o as IAsyncProgress2;
                            o.CloseOnComplete = true;

                            int count = entity.Count;
                            for (int i = 0; i < entity.Count; i++)
                            {
                                VSSiteTreeEntity det = entity[i];
                                o2.UpdateStatusProgress(i, string.Format(this.GetResourceTextByKey(1, "MSG_VWS_LOAD_SITE"), (i + 1), count.ToString()));

                                TreeNode nodeCompany = null;
                                TreeNode nodeSubCompany = null;
                                TreeNode nodeSite = null;
                                string keyCompany = det.CompanyKey;
                                string keySubCompany = det.SubCompanyKey;

                                // company
                                if (dicCompanies.ContainsKey(keyCompany))
                                {
                                    nodeCompany = dicCompanies[keyCompany];
                                }
                                else
                                {
                                    nodeCompany = new TreeNode(det.Company_Name, 0, 0);
                                    nodeCompany.Tag = new CompanyTag() { CompanyId = det.Company_ID };
                                    dicCompanies.Add(keyCompany, nodeCompany);
                                    o.CrossThreadInvoke(() =>
                                    {
                                        tvwSites.Nodes.Add(nodeCompany);
                                    });
                                }

                                // sub company
                                if (dicSubCompanies.ContainsKey(keySubCompany))
                                {
                                    nodeSubCompany = dicSubCompanies[keySubCompany];
                                }
                                else
                                {
                                    nodeSubCompany = new TreeNode(det.Sub_Company_Name, 1, 1);
                                    nodeSubCompany.Tag = new SubCompanyTag() { SubCompanyId = det.Sub_Company_ID };
                                    dicSubCompanies.Add(keySubCompany, nodeSubCompany);
                                    o.CrossThreadInvoke(() =>
                                    {
                                        nodeCompany.Nodes.Add(nodeSubCompany);
                                    });
                                }

                                // site
                                nodeSite = new TreeNode(det.DisplayName, 2, 2);
                                int index = -1;
                                o.CrossThreadInvoke(() =>
                                {
                                    index = cboSites.Items.Add(det);
                                });

                                det.LinkIndex = index;
                                nodeSite.Tag = det;
                                if (_selectedNode == null)
                                {
                                    _selectedNode = nodeSite;
                                }
                                o.CrossThreadInvoke(() =>
                                {
                                    nodeSite.Name = det.Site_Code;
                                    nodeSubCompany.Nodes.Add(nodeSite);
                                    nodeCompany.ExpandAll();
                                });

                                o.CrossThreadInvoke(() =>
                                {

                                });
                                Thread.Sleep(1);
                            }

                            o.CrossThreadInvoke(() =>
                            {
                                //lvwHourly.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
                            });
                        });
                    label1.Tag = "Key_SpaceSingle";
                    label1.ForeColor = Color.Empty;
                    //  btnFilter.Enabled = (tvwSites.Nodes.Count == 0) ? false : true;
                }
                else
                {
                    label1.Tag = "Key_No_Data";
                    label1.ForeColor = Color.Red;
                }
                //{
                //    btnFilter.Enabled = (tvwSites.Nodes.Count == 0) ? false : true;
                //    if (!_isLoaded)
                //    {
                //        BMC.CoreLib.Win32.Win32Extensions.ShowMessageBox(this, "No Data to Display", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //        search.ActiveSitesOnly = false; search.PayoutPercentageFrom = null; search.PayoutPercentageTo = null; search.CompanyID = null; search.Depot_ID = null;
                //        search.BarPositionId = null; search.HasAddress = false; search.HasBarPositionId = false; search.HasSiteRef = false; search.HasSupplier = false;
                //        search.Operator_ID = null; search.ModelSearch = null; search.ExcludeVacant = false; search.Sub_CompanyID = null; search.Sub_Company_Region_ID = null;
                //        search.Sub_Company_District_ID = null; search.Sub_Company_Area_ID = null; search.SiteRepId = null; search.Site_ZonaRice = null; search.SearchText = "";
                //        search.Machine_Type_ID = null; search.Manufacturer_ID = null;
                //        tvwSites.AfterSelect -= tvwSites_AfterSelect;
                //        //this.LoadSiteTree(search);
                //        tvwSites.AfterSelect += tvwSites_AfterSelect;
                //        if (_prvSelectedNode != null)
                //        {
                //            if (_prvSelectedNode.Tag != null)
                //            {
                //                VSSiteTreeEntity site_ent = _prvSelectedNode.Tag as VSSiteTreeEntity;
                //                _prvSelectedNode.Name = site_ent.Site_Code;
                //                prev_scode = site_ent.Site_Code;
                //            }
                //        }
                //        search.isFilter = false;
                //        cboSiteSearch.SelectedIndex = 0;
                //    }
                //    _isLoaded = false;
                //}
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
            finally
            {
                if (_selectedNode != null)
                {
                    if (prev_scode != "")
                    {
                        TreeNode[] tn_select = tvwSites.Nodes.Find(prev_scode, true);
                        if (tn_select != null && tn_select.Count() > 0)
                        {
                            // tvwSites.SelectedNode = tn_select[0];
                            tvwSites_AfterSelect(this, new TreeViewEventArgs(tvwSites.SelectedNode));
                            prev_scode = "";
                        }
                    }
                    else
                    {
                        // tvwSites.SelectedNode = _selectedNode;
                    }
                }
                _isSiteLoading = false;
                this.LoadSite(cboSites.SelectedItem as VSSiteTreeEntity);
            }
        }

        private void LoadSite(VSSiteTreeEntity entity)
        {
            ModuleProc PROC = new ModuleProc("ViewSitesForm", "LoadSite");
            if (_isSiteLoading) return;

            try
            {
                if (_ucSite != null)
                {
                    this.LoadUserControl(_ucSite);
                }
                if (_ucInstallations != null)
                {
                    this.LoadUserControl(_ucInstallations);
                    this.ClearActiveControl();
                    _ucInstallations.SelectItem();
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
            finally
            {
                this.EnableDisableNavigationButtons();
                //this.Text = string.Empty;
                if (!this.FormCaption.IsEmpty())
                    this.Text = this.FormCaption;
                else
                    this.Text = _formCaption;
            }
        }

        public void Reload()
        {
            if (_isFormLoading)
            {
                this.ActivateTabChild();
                _isFormLoading = false;
            }

            this.SelectTabChild(tabDetails2);
        }

        public IExecutorService Executor { get; private set; }

        public IViewSiteReportViewer Viewer { get; set; }

        private void LoadUserControls(params UserControl[] controls)
        {
            foreach (UserControl control in controls)
            {
                IUserControl uc = control as IUserControl;
                if (uc != null)
                {
                    this.LoadUserControl(uc);
                }
            }
        }

        private void LoadUserControl(IUserControl uc)
        {
            if (uc != null)
            {
                if (!uc.IsControlInitialized) uc.IsControlInitialized = true;
                uc.LoadControl();
            }
        }

        private void SelectTabChild(TabControl tab)
        {
            ModuleProc PROC = new ModuleProc("ViewSitesForm", "SelectTabChild");
            if (this.SelectedSite == null)
                return;
            try
            {
                TabPage tbp = tab.SelectedTab;
                if (tbp != null && tbp.Controls.Count > 0)
                {
                    this.LoadUserControl(tbp.Controls[0] as IUserControl);
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        private void tabDetails2_SelectedIndexChanged(object sender, EventArgs e)
        {
            ModuleProc PROC = new ModuleProc("", "tabDetails2_SelectedIndexChanged");

            try
            {
                // Deactivated
                TabControl tab = tabDetails2;
                TabPage tbp = tab.SelectedTab;
                if (_oldTabPage != null &&
                    _oldTabPage != tbp)
                {
                    if (_oldTabPage.Controls.Count > 0)
                    {
                        IUserControl3 uc3 = _oldTabPage.Controls[0] as IUserControl3;
                        if (uc3 != null) uc3.DeActivated();
                    }
                }

                _oldTabPage = tbp;
                if (!chkDisplay.Checked)
                {
                    UnloadDetails();
                }
                else
                {
                    this.ShowHideExportbutton(true);

                    // Activate
                    this.ActivateTabChild();

                    // Clear
                    this.ClearActiveControl();

                    this.Viewer = null;
                    // Load
                    //this.SelectTabChild(tab);
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        private void ActivateTabChild()
        {
            ModuleProc PROC = new ModuleProc("", "ActivateTabChild");

            try
            {
                if (_oldTabPage != null &&
                    _oldTabPage.Controls.Count > 0)
                {
                    IUserControl3 uc3 = _oldTabPage.Controls[0] as IUserControl3;
                    if (uc3 != null) uc3.Activated();
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        private void tvwSites_AfterSelect(object sender, TreeViewEventArgs e)
        {
            ModuleProc PROC = new ModuleProc("ViewSitesForm", "tvwSites_AfterSelect");

            try
            {
                TreeNode node = e.Node;
                VSSiteTreeEntity entity = node.Tag as VSSiteTreeEntity;
                if (entity != null)
                {
                    int index = entity.LinkIndex;
                    cboSites.SelectedIndex = index;
                }
                this.AssignContextMenu(node);
                _ucHourly.FillFilterByValues();
                UnloadDetails();
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        private void btnTree_Click(object sender, EventArgs e)
        {
            this.ShowHideSiteTree();
        }

        public void ShowHideSiteTree()
        {
            tblSites.Visible = !tblSites.Visible;
            tblContent.ColumnStyles[0].Width = (tblSites.Visible ? 300 : 0);
            btnTree.ImageKey = (tblSites.Visible ? "MovePrev" : "MoveNext");
        }

        private void cboSites_SelectedIndexChanged(object sender, EventArgs e)
        {
            ModuleProc PROC = new ModuleProc("ViewSitesForm", "tvwSites_AfterSelect");

            try
            {
                VSSiteTreeEntity entity = cboSites.SelectedItem as VSSiteTreeEntity;
                if (entity != null)
                {
                    this.LoadSite(entity);
                    _ucHourly.FillFilterByValues();
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        private void EnableDisableNavigationButtons()
        {
            ModuleProc PROC = new ModuleProc("ViewSitesForm", "EnableDisableNavigationButtons");

            try
            {
                int count = cboSites.Items.Count;
                int index = cboSites.SelectedIndex;
                btnMoveFirst.Enabled = (index > 0 && index < count);
                btnMovePrev.Enabled = (index > 0 && index < count);
                btnMoveNext.Enabled = (index >= 0 && index < count - 1);
                btnMoveLast.Enabled = (index >= 0 && index < count - 1);
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        private void btnMoveFirst_Click(object sender, EventArgs e)
        {
            if (cboSites.Items.Count > 0) cboSites.SelectedIndex = 0;
        }

        private void btnMovePrev_Click(object sender, EventArgs e)
        {
            if ((cboSites.SelectedIndex - 1) >= 0)
            {
                cboSites.SelectedIndex = cboSites.SelectedIndex - 1;
            }
        }

        private void btnMoveNext_Click(object sender, EventArgs e)
        {
            if ((cboSites.SelectedIndex + 1) < cboSites.Items.Count)
            {
                cboSites.SelectedIndex = cboSites.SelectedIndex + 1;
            }
        }

        private void btnMoveLast_Click(object sender, EventArgs e)
        {
            if (cboSites.Items.Count > 0) cboSites.SelectedIndex = cboSites.Items.Count - 1;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            if (tvwSites.SelectedNode != null)
            {
                this.ClearActiveControl();
                this.Reload();
                //this.SelectTabChild(tabDetails2);
                chkDisplay.Checked = true;                
            }
            else
            {
                BMC.CoreLib.Win32.Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_SITE_SELECT"), this._formCaption);
            }
        }

        private void btnCashierTransactions_Click(object sender, EventArgs e)
        {

            if (tvwSites.SelectedNode == null)
            {
                BMC.CoreLib.Win32.Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1,"MSG_ViewSites_CashierTransaction"), this.Text);
            }
            else
            {
                int _UserID = AppGlobals.Current.LoggedinUser.SecurityUserID;

                frmCashDeskManager objCashDeskManager = new frmCashDeskManager(this.SelectedSite.Site_ID, _UserID);
                objCashDeskManager.StartPosition = FormStartPosition.CenterScreen;
                objCashDeskManager.ShowDialog(this);

            }
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            if (BMC.CoreLib.Win32.Win32Extensions.ShowDialogExAndDestroy(new ViewSitesSearchForm(_searchByForm), this, null, null))
            {
                _prvSelectedNode = tvwSites.SelectedNode;
                this.LoadSiteTree(_searchByForm);
            }
        }

        private void cboSiteSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboSiteSearch.Tag != null) return;
            this.SearchSite(cboSiteSearch.SelectedIndex, string.Empty);
        }

        private void cboSiteSearch_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!ViewSiteHelper.SaveSearchCombo(cboSiteSearch, "ViewSiteForm_SearchText"))
                {
                    this.SearchSite(-1, string.Empty);
                }
            }
        }

        private void SearchSite(int index, string value)
        {
            ModuleProc PROC = new ModuleProc("ViewSitesForm", "SearchSite");
            if (_isSearchSitesLoading) return;

            _isSearchSitesLoading = true;
            try
            {
                string searchText = value;
                if (index != -1)
                {
                    searchText = cboSiteSearch.Items[index].ToString();
                }
                _searchByForm.SearchText = searchText;
                this.LoadSiteTree(_searchByForm);
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
            finally
            {
                _isSearchSitesLoading = false;
            }
        }

        private void chkDisplayActiveSites_CheckedChanged(object sender, EventArgs e)
        {
            _searchByText.ActiveSitesOnly = chkDisplayActiveSites.Checked;
            _searchByForm.ActiveSitesOnly = chkDisplayActiveSites.Checked;
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            ModuleProc PROC = new ModuleProc("", "btnExport_Click");

            try
            {

                if (tabDetails2.SelectedTab == tbpHourly)
                {
                    _ucHourly.ExportReport();
                }

                else if (this.Viewer != null)
                {
                    this.Viewer.ExportReport();
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        private void chkDisplay_CheckedChanged(object sender, EventArgs e)
        {
            if (_isFormInitializing) return;

            if (!chkDisplay.Checked)
            {
                UnloadDetails();
                tabDetails2.Enabled = false;
            }
            else
            {
                this.Reload();
                tabDetails2.Enabled = true;
            }
        }

        private void UnloadDetails()
        {
            _ucDrop.ClearItems();
            _ucHourly.ClearItems();
            if (chkDisplay.Checked == false)
            {
                _ucAssets.ClearItems();
            }
        }

        private void ctxMenuItemCompany_Click(object sender, EventArgs e)
        {
            if (tvwSites.SelectedNode != null &&
                tvwSites.SelectedNode.Tag != null &&
                tvwSites.SelectedNode.Tag is CompanyTag)
            {
                this.InvokeOrganisationForm(OrganisationInputType.Company, ((CompanyTag)tvwSites.SelectedNode.Tag).CompanyId);
            }
        }

        private void ctxMenuItemSubCompany_Click(object sender, EventArgs e)
        {
            if (tvwSites.SelectedNode != null &&
                tvwSites.SelectedNode.Tag != null &&
                tvwSites.SelectedNode.Tag is SubCompanyTag)
            {
                this.InvokeOrganisationForm(OrganisationInputType.SubCompany, ((SubCompanyTag)tvwSites.SelectedNode.Tag).SubCompanyId);
            }
        }

        private void ctxMenuItemSite_Click(object sender, EventArgs e)
        {
            if (tvwSites.SelectedNode != null &&
                tvwSites.SelectedNode.Tag != null &&
                tvwSites.SelectedNode.Tag is VSSiteTreeEntity)
            {
                this.InvokeOrganisationForm(OrganisationInputType.Site, 0);
            }
        }

        public int GetSelectedPeriodCount(int tab, int subtab, int defaultValue)
        {
            KeyValuePair<int, int> pair = new KeyValuePair<int, int>(tab, subtab);
            if (_selectedPeriodCounts.ContainsKey(pair))
                return _selectedPeriodCounts[pair];
            return defaultValue;
        }

        public void SetSelectedPeriodCount(int tab, int subtab, int value)
        {
            KeyValuePair<int, int> pair = new KeyValuePair<int, int>(tab, subtab);
            if (_selectedPeriodCounts.ContainsKey(pair))
                _selectedPeriodCounts[pair] = value;
            else
                _selectedPeriodCounts.Add(pair, value);
        }

        public CPeriodUnitsType GetSelectedPeriodUnit(int tab, int subtab, CPeriodUnitsType defaultValue)
        {
            KeyValuePair<int, int> pair = new KeyValuePair<int, int>(tab, subtab);
            if (_selectedPeriodUnits.ContainsKey(pair))
                return _selectedPeriodUnits[pair];
            return defaultValue;
        }

        public void SetSelectedPeriodUnit(int tab, int subtab, CPeriodUnitsType value)
        {
            KeyValuePair<int, int> pair = new KeyValuePair<int, int>(tab, subtab);
            if (_selectedPeriodUnits.ContainsKey(pair))
                _selectedPeriodUnits[pair] = value;
            else
                _selectedPeriodUnits.Add(pair, value);
        }

        public void InvokeOrganisationForm(OrganisationInputType inputType, int value)
        {
            ModuleProc PROC = new ModuleProc("", "InvokeOrganisationForm");

            try
            {
                OrganisationInput input = new OrganisationInput()
                {
                    InputType = inputType,
                };

                switch (inputType)
                {
                    case OrganisationInputType.Position:
                        if (this.SelectedInstallation != null)
                        {
                            input.SiteId = this.SelectedSite.Site_ID;
                            input.InstallationId = this.SelectedInstallation.Installation_ID.SafeValue();
                            input.BarPositionId = this.SelectedInstallation.Bar_Position_ID;
                        }
                        break;

                    case OrganisationInputType.Site:
                        if (this.SelectedSite != null)
                        {
                            input.SiteId = this.SelectedSite.Site_ID;
                        }
                        break;

                    case OrganisationInputType.SubCompany:
                        input.SubCompanyId = value;
                        break;

                    case OrganisationInputType.Company:
                        input.CompanyId = value;
                        break;

                    default:
                        break;
                }
                if (this.SelectedInstallation != null)
                {
                    AppGlobals.Current.ActiveForm.InvokeInlineForm("tbrItemOrganisation", input);
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        private void ViewSitesForm_Load(object sender, EventArgs e)
        {
            this.ResolveResources();
        }
    }
    public interface IViewSiteInfo
    {
        VSSiteTreeEntity SelectedSite { get; }
        VSInstallationEntity SelectedInstallation { get; }
        BMC.EnterpriseClient.Helpers.frmAdminUtilities AdminUtilities { get; }
        string FormCaption { get; set; }
        void Reload();
        IExecutorService Executor { get; }
        IViewSiteReportViewer Viewer { get; set; }
        ViewSitesBusiness Business { get; }

        int GetSelectedPeriodCount(int tab, int subtab, int defaultValue);
        void SetSelectedPeriodCount(int tab, int subtab, int value);

        CPeriodUnitsType GetSelectedPeriodUnit(int tab, int subtab, CPeriodUnitsType defaultValue);
        void SetSelectedPeriodUnit(int tab, int subtab, CPeriodUnitsType value);

        void InvokeOrganisationForm(OrganisationInputType inputType, int value);
        void ShowHideExportbutton(bool show);
    }
}
