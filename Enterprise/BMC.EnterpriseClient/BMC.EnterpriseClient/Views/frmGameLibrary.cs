using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BMC.EnterpriseClient.Helpers;
using BMC.EnterpriseBusiness.Business;
using BMC.EnterpriseBusiness.Entities;
using BMC.Common.ExceptionManagement;
using System.Threading;
using BMC.CoreLib.Concurrent;
using BMC.CoreLib;
using BMC.Common;
namespace BMC.EnterpriseClient.Views
{
    public partial class frmGameLibrary : GenericFormBase
    {
        #region Data Members
        //private const string strAny = "ALL";
        GameLibraryBiz objGameLibraryBiz = new GameLibraryBiz();
        #endregion //Data Members

        int iGameTitleID = 0;
        int iGameManufacturer = 0;
        int iGameCategory = 0;
        ToolTip Ttip = new ToolTip();
        private bool _isLoading = true;
        int iGameID = 0;
        private string defaultALLString = string.Empty;

        private IExecutorService _exec = ExecutorServiceFactory.CreateExecutorService();
        #region Constructor

        public frmGameLibrary()
        {
            InitializeComponent();
            SetPropertyTag();
        }

        private void SetPropertyTag()
        {
            try
            {
                this.clmDenom.Tag = "Key_Denom";
                this.btn_EditPayTable.Tag = "Key_EditPayTable";
                this.gpFilterBy.Tag = "Key_FilterBy";
                this.lbl_GameCategoryFilter.Tag = "Key_GameCategoryColon";
                this.lbl_ManufacturerFilter.Tag = "Key_GameManufacturerColon";
                this.clmGameName.Tag = "Key_GameName";
                this.lblGameName.Tag = "Key_GameNameColon";
                this.grp_GameTitleDetails.Tag = "Key_GameTitleDetails";
                this.grp_GroupBy.Tag = "Key_GroupBy";
                this.label1.Tag = "Key_MCManufacturerColon";
                this.grp_gameDetails.Tag = "Key_MCProtocolGameDetails";
                this.grp_PayTableDetails.Tag = "Key_MCProtocolPayTableDetails";
                this.clmManId.Tag = "Key_MachineManufacturer";
                this.clmMachineName.Tag = "Key_MachineName";
                this.btn_MapGame.Tag = "Key_MapGame";
                this.lbl_GroupBy.Tag = "Key_OptionsColon";
                this.clmPayTableBet.Tag = "Key_PayTableBet";
                this.clmPayTabDesc.Tag = "Key_PayTableDescription";
                this.clmPayoutPercentage.Tag = "Key_PayoutPercent";
                this.clmTheoPayoutPercent.Tag = "Key_TheoPayoutPercHeader";
                this.btnLoad.Tag = "Key_Load";

                this.cmb_GroupBy.Items.Add(this.GetResourceTextByKey("Key_GameCategory"));
                this.cmb_GroupBy.Items.Add(this.GetResourceTextByKey("Key_Manufacturer"));
                this.editCategoryToolStripMenuItem.Text = this.GetResourceTextByKey("Key_EditCategory");
                this.editGameTitleToolStripMenuItem.Text = this.GetResourceTextByKey("Key_EditGameTitle");
                this.editManufacturerToolStripMenuItem.Text = this.GetResourceTextByKey("Key_EditManufacturer");
                this.mapGameToolStripMenuItem.Text = this.GetResourceTextByKey("Key_MapGame");
                this.newCategoryToolStripMenuItem.Text = this.GetResourceTextByKey("Key_NewCategory");
                this.newGameTitleToolStripMenuItem1.Text = this.GetResourceTextByKey("Key_NewGameTitle");
                this.newGameTitleToolStripMenuItem.Text = this.GetResourceTextByKey("Key_NewGameTitle");
                this.newManufacturerToolStripMenuItem.Text = this.GetResourceTextByKey("Key_NewManufacturer");
                defaultALLString = this.GetResourceTextByKey("Key_All").ToUpper();
            }
            catch (Exception ex)
            {                
                ExceptionManager.Publish(ex);
            }
        }


        #endregion //Constructor

        frmGamesMasterMap objGamesMasterMapForm = null;

        #region Override Methods

        protected override void LoadChanges()
        {
            base.LoadChanges();
        }

        #endregion //Override Methods

        #region Events
        
        private void GameLibraryForm_Load(object sender, EventArgs e)
        {
            try
            {
                cmb_GroupBy.SelectedIndex = 0;
                btn_ManufacturerFilter.Visible = AppGlobals.Current.HasUserAccess("HQ_Stock_Manufacturer");
                PopulateGameCategory();
                PopulateManufacturers();

                //Load GameDetails
                cmbGameName.Items.Clear();
                List<rsp_GetGameNames> lst_Gamename = AssetManagementBiz.CreateInstance().Load_GameLibraryGameNames();
                if (lst_Gamename != null)
                {
                    lst_Gamename.Insert(0, new rsp_GetGameNames { MG_Game_ID = 0, MG_Game_Name = BMC.Common.ResourceExtensions.GetResourceTextByKey(null, "Key_All").ToUpper() });
                }
                cmbGameName.DataSource = lst_Gamename;
                cmbGameName.DisplayMember = "MG_Game_Name";
                cmbGameName.ValueMember = "MG_Game_ID";
                //Load GameDetails
                this.ResolveResources();

            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
            finally
            {
                _isLoading = false;
                this.RefreshTreeViewInWorker();
            }
        }

        private void RefreshTreeViewInWorker()
        {
            ThreadPool.QueueUserWorkItem((o) =>
            {
                Thread.Sleep(100);
                BMC.CoreLib.Win32.Win32Extensions.CrossThreadInvoke(this, new Action(() =>
                {
                    this.RefreshTreeView();
                }));
            }, null);
        }

        private void cmb_GameCategoryFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshTreeView();
        }

        private void cmb_ManufacturerFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            int groupBy = cmb_GroupBy.SelectedIndex;

            
                if (groupBy == 1)
                    RefreshTreeView();
            
        }

        private void cmb_GroupBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshTreeView();
        }

        private void btn_ManufacturerFilter_Click(object sender, EventArgs e)
        {
            EditManufacturer(Convert.ToInt32(cmb_ManufacturerFilter.SelectedValue));
        }

        private void btn_GameCategoryFilter_Click(object sender, EventArgs e)
        {
            try
            {
                createNewCategory();
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        void child_FormClosed(object sender, FormClosedEventArgs e)
        {
            //when child form is closed, this code is executed
            this.Refresh();
        }

        private void btn_EditPayTable_Click(object sender, EventArgs e)
        {
            try
            {
                if ((lvPayTable.SelectedItems.Count <= 0) || (lvPayTable.SelectedItems[0] == null))
                {
                    this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_GAMELIBRARY_SELECTPAYTABLE"), this.Text);
                    return;
                }

                int iPayTable_Id = Convert.ToInt32(lvPayTable.SelectedItems[0].Tag);
                frmPayTableMaster objPayTableMasterForm = new frmPayTableMaster(iPayTable_Id);
                objPayTableMasterForm.FormClosed += new FormClosedEventHandler(child_FormClosed);
                objPayTableMasterForm.ShowDialog();

                if (objPayTableMasterForm.DialogResult != DialogResult.OK)
                    PopulateGamesandPaytableDetails();
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btn_MapGame_Click(object sender, EventArgs e)
        {
            MapGame();
        }

        private void trv_GameCategory_AfterSelect(object sender, TreeViewEventArgs e)
        {
          //  PopulateGamesandPaytableDetails();
        }

        private void lvGameDetails_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if ((lvGameDetails.SelectedItems.Count > 0) && (lvGameDetails.SelectedItems[0] != null))
                    DisplayPaytable(Convert.ToInt32(lvGameDetails.SelectedItems[0].Tag), lvGameDetails.SelectedItems[0].SubItems[2].Text);
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void newCategoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                createNewCategory();
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void newManufacturerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmManufacturer Manuform = new frmManufacturer();
                Manuform.FormClosed += new FormClosedEventHandler(child_FormClosed);
                Manuform.ShowDialog();

                if (Manuform.DialogResult != DialogResult.OK) return;
                PopulateManufacturers();
                RefreshTreeView();
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void editManufacturerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditManufacturer(Convert.ToInt32((trv_GameCategory.SelectedNode.Tag.ToString().Split(','))[1]));
        }

        private void newGameTitleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmGameTitle GameTitleform = new frmGameTitle();
                GameTitleform.FormClosed += new FormClosedEventHandler(child_FormClosed);
                GameTitleform.ShowDialog();

                if (GameTitleform.DialogResult != DialogResult.OK) return;
                RefreshTreeView();
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void editCategoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditCategory(Convert.ToInt32((trv_GameCategory.SelectedNode.Tag.ToString().Split(','))[1]));
        }

        private void editGameTitleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (trv_GameCategory.SelectedNode == null) return;

                frmGameTitle GameTitleform = new frmGameTitle(Convert.ToInt32((trv_GameCategory.SelectedNode.Tag.ToString().Split(','))[1]));
                GameTitleform.FormClosed += new FormClosedEventHandler(child_FormClosed);
                GameTitleform.ShowDialog();

                if (GameTitleform.DialogResult != DialogResult.OK) return;
                RefreshTreeView();
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void mapGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MapGame();
        }

        private void trv_GameCategory_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Right)
                {
                    ((TreeView)sender).SelectedNode = e.Node;
                }
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        #endregion //Events

        #region Methods

        private void RefreshTreeView()
        {
            this.RefreshTreeView(!_isLoading);
        }

        private void RefreshTreeView(bool reload)
        {
            TreeNode NodeSelect = null;
            try
            {
                lvGameDetails.Items.Clear();
                lvPayTable.Items.Clear();
                if (!reload) return;

                TreeNode tnRoot = new TreeNode();
                string gameCategory = cmb_GameCategoryFilter.SelectedIndex > 0 ? cmb_GameCategoryFilter.Text : "All";
                string gameName = cmbGameName.SelectedIndex > 0 ? cmbGameName.Text.Split(',')[0] : "All";
                string manufacturerName = cmb_ManufacturerFilter.SelectedIndex > 0 ? cmb_ManufacturerFilter.Text : "All"; ;

                string groupBy = cmb_GroupBy.Text;
                int gameID = ((gameName.ToUpper() == this.GetResourceTextByKey("Key_All").ToUpper()) ? 0 : Convert.ToInt32(cmbGameName.SelectedValue));
                int groupbyid = cmb_GroupBy.SelectedIndex;
                Form parentForm = this.ParentForm;
                BMC.CoreLib.Win32.Win32Extensions.ShowAsyncDialog(parentForm, this.GetResourceTextByKey("Key_LoadGame"), _exec,
                (o) =>
                {
                    o.CrossThreadInvoke(() =>
                    {
                        trv_GameCategory.Nodes.Clear();
                    });

                    Thread.Sleep(100);

                    BMC.CoreLib.Win32.IAsyncProgress2 o2 = o as BMC.CoreLib.Win32.IAsyncProgress2;
                    if(groupbyid==0)
                  //  if (groupBy == "Game Category")
                    {
                        //By Category lstCategory
                        List<GameCategory> lstCategory = objGameLibraryBiz.GetGameCategory(gameCategory, defaultALLString);
                        List<GameCategory> lstCategoryGL = objGameLibraryBiz.GetGameCategoryGL(gameCategory, gameName, defaultALLString);

                        string gameCatParam = (gameCategory.ToUpper() == this.GetResourceTextByKey("Key_All").ToUpper()) ? "All" : gameCategory;
                        string manufactNameParam = (manufacturerName.ToUpper() == this.GetResourceTextByKey("Key_All").ToUpper()) ? "All" : manufacturerName;

                        List<GamesByCategory> lstGamesbyCategory = objGameLibraryBiz.GetGamesByCategory(gameCatParam, manufactNameParam, gameID);
                        if (lstGamesbyCategory.Count == 1)
                        {
                            foreach (GamesByCategory obj in lstGamesbyCategory)
                                iGameID = obj.Game_Title_ID;
                        }
                        else
                            iGameID = 0;
                            //Convert.ToInt32(lstGamesbyCategory.Select(obj => obj.Game_Title_ID));
                        bool isManufacturerFilter = false;

                        o.CrossThreadInvoke(() =>
                        {
                            //Add root node                     
                            tnRoot = new TreeNode(this.GetResourceTextByKey("Key_All").ToUpper());
                            tnRoot.Tag = "C,0";
                            trv_GameCategory.Nodes.Add(tnRoot);
                            tnRoot.ContextMenuStrip = mnu_AllCategory;

                            if (cmb_ManufacturerFilter.Text != this.GetResourceTextByKey("Key_All").ToUpper())
                            {
                                isManufacturerFilter = true;
                            }
                        });

                        if (lstCategoryGL != null && lstCategoryGL.Count > 0)
                        {
                            o2.InitializeProgress(1, lstCategoryGL.Count);
                        }

                        int count = 1;
                        foreach (GameCategory objGameCategory in lstCategoryGL)
                        {
                            o2.UpdateStatusProgress(count++, "Loading : " + objGameCategory.Game_Category_Name);

                            TreeNode tnCategory = null;
                            bool bUnAssigned = false;
                            if (string.IsNullOrEmpty(objGameCategory.Game_Category_Name) || objGameCategory.Game_Category_Name == "Unassigned Category")
                            {
                                tnCategory = new TreeNode("Unassigned Category");
                                bUnAssigned = true;
                            }
                            else
                            {
                                tnCategory = new TreeNode(objGameCategory.Game_Category_Name);
                            }

                            tnCategory.Tag = "C," + objGameCategory.Game_Category_ID;
                            var lstSubNodes = (from GamesByCategory Title in lstGamesbyCategory
                                               where Title.Game_Category_Name == objGameCategory.Game_Category_Name
                                               orderby Title.Game_Title
                                               select Title).ToList<GamesByCategory>();


                            if ((lstSubNodes.Count <= 0) && (isManufacturerFilter))
                                continue;

                            o.CrossThreadInvoke(() =>
                            {
                                AddGameCategory(tnCategory, tnRoot, bUnAssigned);
                            });

                            if (lstSubNodes != null && lstSubNodes.Count > 0)
                            {
                                o2.InitializeProgress(1, lstSubNodes.Count);
                            }

                            int count2 = 1;
                            // if ((lstSubNodes.Count <= 0) && (cmb_ManufacturerFilter.Text != "ALL")) ;
                            foreach (GamesByCategory objGamesByCategory in lstSubNodes)
                            {
                                o2.UpdateStatusProgress(count2++, this.GetResourceTextByKey("Key_LoadingColon") + objGamesByCategory.Game_Title);

                                TreeNode tnGameTitle = null;
                                if (string.IsNullOrEmpty(objGamesByCategory.Manufacturer_Name))
                                    tnGameTitle = new TreeNode(objGamesByCategory.Game_Title);
                                else
                                    tnGameTitle = new TreeNode(objGamesByCategory.Game_Title + ", " + objGamesByCategory.Manufacturer_Name);

                                bUnAssigned = (objGamesByCategory.Game_Title == "Unassigned GameTitle") ? true : false;

                                tnGameTitle.Tag = "T," + objGamesByCategory.Game_Title_ID;

                                o.CrossThreadInvoke(() =>
                                {
                                    AddGameTitle(tnGameTitle, tnCategory, bUnAssigned);
                                });
                                if (NodeSelect == null)
                                {
                                    NodeSelect = tnGameTitle;
                                }
                                Thread.Sleep(1);
                            }

                            o.CrossThreadInvoke(() =>
                               {
                                   tnCategory.ExpandAll();
                               });

                            Thread.Sleep(1);
                        }

                        o.CrossThreadInvoke(() =>
                            {
                                tnRoot.ExpandAll();
                            });
                    }
                   // else if (groupBy == "Manufacturer")
                    else if (groupbyid == 1)
                    {
                        //By Manufacturer
                        List<Manufacturer> lstManufacturer = objGameLibraryBiz.GetManufacturers(manufacturerName, defaultALLString);

                        string gameCatParam = (gameCategory.ToUpper() == this.GetResourceTextByKey("Key_All").ToUpper()) ? "All" : gameCategory;
                        string manufactNameParam = (manufacturerName.ToUpper() == this.GetResourceTextByKey("Key_All").ToUpper()) ? "All" : manufacturerName;

                        List<GamesByManufacturer> gamesList = objGameLibraryBiz.GetGamesByManufacturer(gameCatParam, manufactNameParam, gameID);

                        bool isGameCategoryFilterText = false;

                        o.CrossThreadInvoke(() =>
                        {
                            //Add root node 
                            tnRoot = new TreeNode(this.GetResourceTextByKey("Key_All").ToUpper());
                            tnRoot.Tag = "C,0";
                            trv_GameCategory.Nodes.Add(tnRoot);
                            tnRoot.ContextMenuStrip = mnu_AllManufacturer;

                            if ((cmb_GameCategoryFilter.Text != this.GetResourceTextByKey("Key_All").ToUpper()))
                            {
                                isGameCategoryFilterText = true;
                            }
                        });

                        if (lstManufacturer != null && lstManufacturer.Count > 0)
                        {
                            o2.InitializeProgress(1, lstManufacturer.Count);
                        }

                        int count = 1;
                        foreach (Manufacturer objManufacturer in lstManufacturer)
                        {
                            o2.UpdateStatusProgress(count++, this.GetResourceTextByKey("Key_LoadingColon") + objManufacturer.Manufacturer_Name);

                            TreeNode tnCategory = null;
                            bool bUnAssigned = false;

                            //ADD GAME CATEGORY IN LEVEL 1
                            if (string.IsNullOrEmpty(objManufacturer.Manufacturer_Name))
                            {
                                tnCategory = new TreeNode("Unassigned Category");
                                bUnAssigned = true;
                            }
                            else
                                tnCategory = new TreeNode(objManufacturer.Manufacturer_Name);

                            tnCategory.Tag = "M," + objManufacturer.Manufacturer_ID;

                            var lstSubNodes = (from GamesByManufacturer Title in gamesList
                                               where Title.Manufacturer_Name == objManufacturer.Manufacturer_Name && Title.Game_Title_ID != null
                                               select Title).ToList<GamesByManufacturer>();

                            if ((lstSubNodes.Count <= 0) && (isGameCategoryFilterText)) continue;

                            o.CrossThreadInvoke(() =>
                               {
                                   AddGameManufacturer(tnCategory, tnRoot, bUnAssigned);

                               });

                            if (lstSubNodes != null && lstSubNodes.Count > 0)
                            {
                                o2.InitializeProgress(1, lstSubNodes.Count);
                            }

                            int count2 = 1;
                            foreach (GamesByManufacturer objGamesByManufacturer in lstSubNodes)
                            {
                                o2.UpdateStatusProgress(count2++, this.GetResourceTextByKey("Key_LoadingColon") + objGamesByManufacturer.Game_Title);

                                //ADD GAME TITLE IN LEVEL 2.
                                TreeNode tnGameTitle = null;

                                if (string.IsNullOrEmpty(objGamesByManufacturer.Game_Category_Name))
                                    tnGameTitle = new TreeNode(objGamesByManufacturer.Game_Title);
                                else
                                    tnGameTitle = new TreeNode(objGamesByManufacturer.Game_Title + ", " + objGamesByManufacturer.Game_Category_Name);

                                if (objGamesByManufacturer.Game_Title != "Unassigned GameTitle")
                                    bUnAssigned = false;

                                tnGameTitle.Tag = "T," + objGamesByManufacturer.Game_Title_ID.GetValueOrDefault();
                                o.CrossThreadInvoke(() =>
                                  {
                                      AddGameTitle(tnGameTitle, tnCategory, bUnAssigned);
                                  });
                                if (NodeSelect == null)
                                {
                                    NodeSelect = tnGameTitle;
                                }

                                Thread.Sleep(1);
                            }

                            Thread.Sleep(1);
                        }
                    }
                });

                //if (cmbGameName.Text == "ALL")
                //{
                //    trv_GameCategory.SelectedNode = tnRoot;
                //}
                //else
                //{
                //    trv_GameCategory.SelectedNode = NodeSelect;
                //}
                trv_GameCategory.ExpandAll();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void PopulateGameCategory()
        {
            try
            {
                cmb_GameCategoryFilter.ValueMember = "Game_Category_ID";
                cmb_GameCategoryFilter.DisplayMember = "Game_Category_Name";
                List<GameCategory> lstCategory = objGameLibraryBiz.GetGameCategoryByCategoryNameGL(true, defaultALLString, cmbGameName.Text, defaultALLString);
                cmb_GameCategoryFilter.DataSource = lstCategory;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void PopulateManufacturers()
        {
            try
            {
                cmb_ManufacturerFilter.ValueMember = "Manufacturer_ID";
                cmb_ManufacturerFilter.DisplayMember = "Manufacturer_Name";
                List<Manufacturer> lstManufacturer = objGameLibraryBiz.GetManufacturers(true, defaultALLString, defaultALLString);
                cmb_ManufacturerFilter.DataSource = lstManufacturer;

                /* To bind data for Machine manufacturer*/
                cboMachineManufaturer.ValueMember = "Manufacturer_ID";
                cboMachineManufaturer.DisplayMember = "Manufacturer_Name";
                List<Manufacturer> lstMacManufacturer = objGameLibraryBiz.GetManufacturers(true,defaultALLString,defaultALLString);
                cboMachineManufaturer.DataSource = lstMacManufacturer;
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void AddGameCategory(TreeNode Category, TreeNode RootNode, bool bUnAssigned)
        {
            try
            {
                if (!bUnAssigned)
                    Category.ContextMenuStrip = mnu_CategoryEdit;

                RootNode.Nodes.Add(Category);
                RootNode.ExpandAll();
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void AddGameManufacturer(TreeNode Category, TreeNode RootNode, bool bUnAssigned)
        {
            try
            {
                if (!bUnAssigned)
                    Category.ContextMenuStrip = mnu_EditManufacturer;

                RootNode.Nodes.Add(Category);
                RootNode.ExpandAll();
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void AddGameTitle(TreeNode GameTitle, TreeNode GameCategory, bool bUnAssigned)
        {
            try
            {
                if (!bUnAssigned)
                    GameTitle.ContextMenuStrip = mnu_EditGameTitle;

                GameCategory.Nodes.Add(GameTitle);
                GameCategory.ExpandAll();
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private List<MultiGameLibraryThemesDetails> DisplayGamesGrid(int iGameTitleId, int iGameCatID)
        {
            List<MultiGameLibraryThemesDetails> result = null;

            try
            {
                int iGameCategoryID = Convert.ToInt32(cmb_GameCategoryFilter.SelectedValue);
                int iManufacturerId = Convert.ToInt32(cmb_ManufacturerFilter.SelectedValue);
                int iMachineManufacturerId = Convert.ToInt32(cboMachineManufaturer.SelectedValue);

                string gameCategory = cmb_GameCategoryFilter.SelectedIndex > 0 ? cmb_GameCategoryFilter.Text : "All";
                string gameName = cmbGameName.SelectedIndex > 0 ? cmbGameName.Text.Split(',')[0] : "All";
                string manufacturerName = cmb_ManufacturerFilter.SelectedIndex > 0 ? cmb_ManufacturerFilter.Text : "All"; ;
                int gameID = ((cmbGameName.Text.ToUpper() == this.GetResourceTextByKey("Key_All").ToUpper()) ? 0 : Convert.ToInt32(cmbGameName.SelectedValue));
                lvGameDetails.Items.Clear();
                Form parentForm = this.ParentForm;

                BMC.CoreLib.Win32.Win32Extensions.ShowAsyncDialog(parentForm, this.GetResourceTextByKey("Key_LoadGameDetails"), _exec,
                  (o) =>
                  {
                      BMC.CoreLib.Win32.IAsyncProgress2 o2 = o as BMC.CoreLib.Win32.IAsyncProgress2;

                      var items = objGameLibraryBiz.GetMultiGameLibraryThemesDetails(iGameTitleId, iGameCatID, iManufacturerId, iGameCategoryID, iMachineManufacturerId, gameName);
                      if (items != null &&
                          items.Count > 0)
                      {
                          result = items;
                          o2.InitializeProgress(1, items.Count);
                          int index = 1;

                          List<GamesByCategory> lstGamesbyCategory = objGameLibraryBiz.GetGamesByCategory(gameCategory, manufacturerName, gameID);
                          List<GameCategory> lstCategoryGL = objGameLibraryBiz.GetGameCategoryGL(gameCategory, gameName, defaultALLString);
                          if (!(lstGamesbyCategory.Count == 0 || lstCategoryGL.Count == 0))
                          {
                              foreach (MultiGameLibraryThemesDetails objMultiGameLibraryThemesDetails in items)
                              {
                                  o2.UpdateStatusProgress(index++, this.GetResourceTextByKey("Key_LoadGameName")+ objMultiGameLibraryThemesDetails.GameName);

                                  ListViewItem objListViewItem = new ListViewItem(objMultiGameLibraryThemesDetails.GameName);


                                  objListViewItem.SubItems.Add(objMultiGameLibraryThemesDetails.Manufacturer);
                                  objListViewItem.SubItems.Add(objMultiGameLibraryThemesDetails.Alias_Machine_Name);
                                  objListViewItem.Tag = objMultiGameLibraryThemesDetails.MG_Game_ID;
                                  o.CrossThreadInvoke(() =>
                                       {
                                           lvGameDetails.Items.Add(objListViewItem);
                                       });

                                  Thread.Sleep(1);
                              }
                          }
                      }
                  });
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

            return result;
        }

        private void DisplayPaytable(int iGameId,string StockNo)
        {
            try
            {
                int iGameCategoryID = Convert.ToInt32(cmb_GameCategoryFilter.SelectedValue);
                int iManufacturerId = Convert.ToInt32(cmb_ManufacturerFilter.SelectedValue);
                lvPayTable.Items.Clear();

                Form parentForm = this.ParentForm;
                BMC.CoreLib.Win32.Win32Extensions.ShowAsyncDialog(parentForm, this.GetResourceTextByKey("Key_LoadPayDetails"), _exec,
                 (o) =>
                 {
                     BMC.CoreLib.Win32.IAsyncProgress2 o2 = o as BMC.CoreLib.Win32.IAsyncProgress2;

                     var items = objGameLibraryBiz.GetPayTable(iGameId, iManufacturerId, iGameCategoryID, StockNo);
                     if (items != null &&
                          items.Count > 0)
                     {
                         o2.InitializeProgress(1, items.Count);
                         int index = 1;
                         foreach (PayTable objPayTable in items)
                         {
                             o2.UpdateStatusProgress(index++, this.GetResourceTextByKey("Key_LoadPayName") + objPayTable.PT_Description);
                             ListViewItem objListViewItem = new ListViewItem();
                             objListViewItem.Text = objPayTable.PT_Description;
                             objListViewItem.Tag = objPayTable.Paytable_ID;
                             objListViewItem.SubItems.Add(objPayTable.Denom.ToString());
                             objListViewItem.SubItems.Add(objPayTable.Payout.ToString());
                             objListViewItem.SubItems.Add(objPayTable.MaxBet.ToString());
                             objListViewItem.SubItems.Add(objPayTable.TheoreticalPayout.ToString());
                             o.CrossThreadInvoke(() =>
                             {
                                 lvPayTable.Items.Add(objListViewItem);
                             });

                             Thread.Sleep(1);
                         }
                     }
                 });
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void DisplayPaytableForGameTitle(List<MultiGameLibraryThemesDetails> gamesResult, int iGameId)
        {
            try
            {
                int iGameCategoryID = Convert.ToInt32(cmb_GameCategoryFilter.SelectedValue);
                int iManufacturerId = Convert.ToInt32(cmb_ManufacturerFilter.SelectedValue);

                string gameCategory = cmb_GameCategoryFilter.SelectedIndex > 0 ? cmb_GameCategoryFilter.Text : "All";
                string gameName = cmbGameName.SelectedIndex > 0 ? cmbGameName.Text.Split(',')[0] : "All";
                string manufacturerName = cmb_ManufacturerFilter.SelectedIndex > 0 ? cmb_ManufacturerFilter.Text : "All"; ;

                int gameID = ((gameName.ToUpper() == this.GetResourceTextByKey("Key_All").ToUpper()) ? 0 : Convert.ToInt32(cmbGameName.SelectedValue));
                lvPayTable.Items.Clear();

                Form parentForm = this.ParentForm;

                BMC.CoreLib.Win32.Win32Extensions.ShowAsyncDialog(parentForm, this.GetResourceTextByKey("Key_LoadingPaytable"), _exec,
                  (o) =>
                  {
                      BMC.CoreLib.Win32.IAsyncProgress2 o2 = o as BMC.CoreLib.Win32.IAsyncProgress2;

                      List<PayTableForGameTitle> paytableItems = objGameLibraryBiz.GetPayTableForGameTitle(iGameId, iManufacturerId, iGameCategoryID);
                      List<PayTableForGameTitle> items = paytableItems;
                      if (gamesResult != null && gamesResult.Count > 0)
                      {
                          items = (from a in paytableItems
                                   where gamesResult.Exists(g => g.Alias_Machine_Name.ToString().IgnoreCaseCompare(a.Machine_Stock_No.ToString()))
                                   select a).ToList();
                      }

                      if (items != null &&
                          items.Count > 0)
                      {
                          o2.InitializeProgress(1, items.Count);
                          int index = 1;

                          List<GameCategory> lstCategoryGL = objGameLibraryBiz.GetGameCategoryGL(gameCategory, gameName, defaultALLString);
                          List<GamesByCategory> lstGamesbyCategory = objGameLibraryBiz.GetGamesByCategory(gameCategory, manufacturerName, gameID);

                          if (!(lstGamesbyCategory.Count == 0 || lstCategoryGL.Count == 0 || lvGameDetails.Items.Count == 0))
                          {
                              foreach (PayTableForGameTitle objPayTableForGameTitle in items)
                              {
                                  o2.UpdateStatusProgress(index++, this.GetResourceTextByKey("Key_LoadPayTable") + objPayTableForGameTitle.PT_Description);
                                  ListViewItem objListViewItem = new ListViewItem();

                                  objListViewItem.Text = objPayTableForGameTitle.PT_Description;
                                  objListViewItem.Tag = objPayTableForGameTitle.Paytable_ID;
                                  objListViewItem.SubItems.Add(objPayTableForGameTitle.Denom.ToString());
                                  objListViewItem.SubItems.Add(objPayTableForGameTitle.Payout.ToString());
                                  objListViewItem.SubItems.Add(objPayTableForGameTitle.MaxBet.ToString());
                                  objListViewItem.SubItems.Add(objPayTableForGameTitle.TheoreticalPayout.ToString());
                                  o.CrossThreadInvoke(() =>
                                     {
                                         lvPayTable.Items.Add(objListViewItem);
                                     });

                                  Thread.Sleep(1);
                              }
                          }
                      }
                  });
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void PopulateGamesandPaytableDetails()
        {
            try
            {
                int iKey = 0;
                TreeNode objTreeNode = trv_GameCategory.SelectedNode as TreeNode;
                if (objTreeNode == null)
                    iKey = iGameID;
                else
                    iKey = Convert.ToInt32((objTreeNode.Tag.ToString().Split(','))[1]);


                lvGameDetails.Items.Clear();
                lvPayTable.Items.Clear();
               
                List<MultiGameLibraryThemesDetails> gamesResult = null;
                if (objTreeNode != null)
                {
                    switch (objTreeNode.Level)
                    {
                        case 0:
                            gamesResult = DisplayGamesGrid(iKey, iKey);
                            DisplayPaytableForGameTitle(gamesResult, iKey);
                            break;

                        case 1:
                            if (objTreeNode.Text.Equals("[Unassigned]"))
                                DisplayGamesGrid(0, -1);
                            else
                                gamesResult = DisplayGamesGrid(0, iKey);
                            DisplayPaytableForGameTitle(gamesResult, 0);
                            break;

                        case 2:
                            gamesResult = DisplayGamesGrid(iKey, 0);
                            DisplayPaytableForGameTitle(gamesResult, iKey);
                            break;
                    }
                }
                else
                {
                    gamesResult = DisplayGamesGrid(iKey, 0);
                    DisplayPaytableForGameTitle(gamesResult, iKey);
                }
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void EditManufacturer(int iManufacturerId)
        {
            try
            {
                frmManufacturer Manuform = new frmManufacturer(iManufacturerId);
                Manuform.FormClosed += new FormClosedEventHandler(child_FormClosed);
                Manuform.ShowDialog();

                if (Manuform.DialogResult != DialogResult.OK) return;
                PopulateManufacturers();
                RefreshTreeView();
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void EditCategory(int iGameCategoryID)
        {
            try
            {
                frmGameCategoryAdmin GameCatform = new frmGameCategoryAdmin(iGameCategoryID);
                GameCatform.FormClosed += new FormClosedEventHandler(child_FormClosed);
                GameCatform.ShowDialog();

                if (GameCatform.DialogResult != DialogResult.OK) return;
                PopulateGameCategory();
                RefreshTreeView();
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void SetToolTip(Control windowsControl, int length)
        {
            String caption = "" + windowsControl.Text;
            Ttip.SetToolTip(windowsControl, (caption.Trim().Length > length) ? caption : "");
            Ttip.AutoPopDelay = 2000;
            Ttip.InitialDelay = 0;
            Ttip.ReshowDelay = 100;
            Ttip.ShowAlways = true;
        }
        private void MapGame()
        {
            try
            {

                if (lvGameDetails.Items.Count <= 0)
                {
                    this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_GAMELIBRARY_NOTFOUND"), this.Text);
                    return;
                }
                if (trv_GameCategory.SelectedNode == null)
                {
                    this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_GAMELIBRARY_SELECTGAMETITLE"), this.Text);
                    return;
                }
                if ((trv_GameCategory.SelectedNode.Tag.ToString().Split(','))[0] == "T")
                    iGameTitleID = Convert.ToInt32((trv_GameCategory.SelectedNode.Tag.ToString().Split(','))[1]);

                iGameManufacturer = Convert.ToInt32(cmb_ManufacturerFilter.SelectedValue);
                iGameCategory = Convert.ToInt32(cmb_GameCategoryFilter.SelectedValue);

                objGamesMasterMapForm = new frmGamesMasterMap(iGameTitleID, iGameManufacturer, iGameCategory);

                objGamesMasterMapForm.FormClosed += new FormClosedEventHandler(child_FormClosed);
                objGamesMasterMapForm.ShowDialog();

                iGameTitleID = iGameManufacturer = iGameCategory = 0;

                if (objGamesMasterMapForm.DialogResult != DialogResult.OK) return;
                PopulateGamesandPaytableDetails();
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void createNewCategory()
        {
            try
            {
                frmGameCategoryAdmin GameCatform = new frmGameCategoryAdmin();
                GameCatform.FormClosed += new FormClosedEventHandler(child_FormClosed);
                GameCatform.ShowDialog();

                if (GameCatform.DialogResult != DialogResult.OK) return;
                PopulateGameCategory();
                RefreshTreeView();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion //Methods

        private void cmb_GameCategoryFilter_MouseHover(object sender, EventArgs e)
        {
            SetToolTip((Control)sender, 2);
        }

        private void cmb_ManufacturerFilter_MouseHover(object sender, EventArgs e)
        {
            SetToolTip((Control)sender, 2);
        }

        private void cmb_GroupBy_MouseHover(object sender, EventArgs e)
        {
            SetToolTip((Control)sender, 2);
        }

        private void cmbGameName_SelectedIndexChanged(object sender, EventArgs e)
        {

            try
            {
                RefreshTreeView();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void cmbGameName_MouseHover(object sender, EventArgs e)
        {
            SetToolTip((Control)sender, 2);
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            PopulateGamesandPaytableDetails();
        }

        private void cmb_ManufacturerFilter_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            RefreshTreeView();
        }
    }
}
