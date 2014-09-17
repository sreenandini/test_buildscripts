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
using Audit.Transport;
using BMC.Common;

namespace BMC.EnterpriseClient.Views
{
    public partial class frmGamesMasterMap : GenericFormBase
    {
        #region Data Members

        GameLibraryBiz objGameLibraryBiz = new GameLibraryBiz();
        DialogResult _dialogResult = DialogResult.Cancel;     
        #endregion //Data Members


        List<GameDetails> lstGameDetails = null;

        #region Property
        public int Manufacturer { get; set; }
        public int Category { get; set; }
        public int GameTitleID { get; set; }
        #endregion Property

        #region Constructor

        public frmGamesMasterMap()
        {
            InitializeComponent();
            SetPropertyTag();
        }

        private void SetPropertyTag()
        {
            try
            {
                this.btnClear.Tag = "Key_ClearCaption";
                this.btnSearch.Tag = "Key_SearchCaption";
                this.btnRemove.Tag = "Key_LessThanTriple";
                this.btnAssign.Tag = "Key_GreaterThanTriple";
                this.clmMapInfoAssetNo.Tag = "Key_AssetNo";
                this.clmUnMapInfoAssetNo.Tag = "Key_AssetNo";
                this.lblGameCategory.Tag = "Key_GameCategoryColon";
                this.lblGameManufacturer.Tag = "Key_GameManufacturerColon";
                this.clmGameName.Tag = "Key_GameName";
                this.clmUnMapGameName.Tag = "Key_GameName";
                this.clmMapInfoGamePartNo.Tag = "Key_GamePartNumber";
                this.clmUnMapInfoGamePartNo.Tag = "Key_GamePartNumber";
                this.lblGameTitle.Tag = "Key_GameTitleColon";
                this.clmMapInfoIsGameActive.Tag = "Key_IsGameActive";
                this.clmUnMapInfoIsGameActive.Tag = "Key_IsGameActive";
                this.clmMapInfoMacSerialNo.Tag = "Key_MCSerialNo";
                this.clmUMapInfoMacSerialNo.Tag = "Key_MCSerialNo";
                this.clmManufacture.Tag = "Key_MachineManufacturer";
                this.clmManufactur.Tag = "Key_MachineManufacturer";
                this.clmMachineName.Tag = "Key_MachineName";
                this.clmMachine_Name.Tag = "Key_MachineName";
                this.grpMapUnMapGames.Tag = "Key_MappedMcProtocolGameInfromation";
                this.grpMappedGameInfo.Tag = "Key_MappedMcProtocolGameInfromation";
                this.lblGameName.Tag = "Key_MappedMcProtocolGames";
                this.clmMapInfoPositionNo.Tag = "Key_PositionNo";
                this.clmUnMapInfoPositionNo.Tag = "Key_PositionNo";
                this.txtSrcUnMapGame.Tag = "Key_SearchUnmappedMcProtocolGames";
                this.clmMapInfoSiteName.Tag = "Key_SiteName";
                this.clmUnMapInfoSiteName.Tag = "Key_SiteName";
                this.grpUnmappedInfo.Tag = "Key_UnMappedMcProtocolGameInfromation";
                this.lblUnmapped.Tag = "Key_UnmappedMcProtocolGames";
                this.Tag = "Key_GameMasterMapping";
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

        }

        public frmGamesMasterMap(int iGameTitleID, int iManufacturer, int iCategory):this()
        {
            GameTitleID = iGameTitleID;
            Manufacturer = iManufacturer;
            Category = iCategory;
        }

        #endregion //Constructor

        #region Events

        private void lvUnmapped_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                lvMappedGameInfo.Items.Clear();
                lvUnmappedInfo.Items.Clear();

                if ((lvUnmapped.SelectedItems == null) || (lvUnmapped.SelectedItems.Count <= 0) || (lvUnmapped.SelectedItems[0] == null)) return;
                GetAssetDetailsForGame(Convert.ToInt32(lvUnmapped.SelectedItems[0].Tag), false);
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void lvMapped_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                lvMappedGameInfo.Items.Clear();
                lvUnmappedInfo.Items.Clear();

                if ((lvMapped.SelectedItems == null) || (lvMapped.SelectedItems.Count <= 0) || (lvMapped.SelectedItems[0] == null)) return;
                GetAssetDetailsForGame(Convert.ToInt32(lvMapped.SelectedItems[0].Tag), true);
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btnAssign_Click(object sender, EventArgs e)
        {
            try
            {
                string sID = "0";

                if (cmbGameTitle.SelectedIndex == -1)
                {
                    this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_GAMEMASTERMAP_SELECT_GAME"), this.Text);
                    cmbGameTitle.Focus();
                    return;
                }

                foreach (ListViewItem objListViewItem in lvUnmapped.SelectedItems)
                {
                    sID += "," + objListViewItem.Tag.ToString();
                }

                AssignGames(sID, false);
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            try
            {
                string sID = "0";

                foreach (ListViewItem objListViewItem in lvMapped.SelectedItems)
                {
                    sID += "," + objListViewItem.Tag.ToString();
                }

                AssignGames(sID, true);
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void frmGamesMasterMap_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                this.DialogResult = _dialogResult;
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void frmGamesMasterMap_Load(object sender, EventArgs e)
        {
            LoadGames();

            if (cmbGameTitle.SelectedIndex > -1)
                PopulateMappings();

            PopulateSlotGames(false);

            this.ResolveResources();
        }

        private void cmbGameTitle_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbGameTitle.SelectedIndex > -1)
            {
                txtGameManufacturer.Text = ((GameDetails)cmbGameTitle.SelectedItem).ManufacturerName;
                txtGameCategory.Text = ((GameDetails)cmbGameTitle.SelectedItem).CategoryName;
            }
            else
                txtGameManufacturer.Text = txtGameCategory.Text = "";

            try
            {
                PopulateMappings();
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtSrcUnMapGame.Text.Trim() != "" && txtSrcUnMapGame.Text != "Search Unmapped M/c Protocol Games")
            {
                PopulateSlotGames(true);
            }
        }

        private void txtSrcUnMapGame_Enter(object sender, EventArgs e)
        {
            if (txtSrcUnMapGame.Text.Length == 0 || txtSrcUnMapGame.Text == "Search Unmapped M/c Protocol Games")
            {
                txtSrcUnMapGame.Text = "";
            }

            txtSrcUnMapGame.ForeColor = Color.Black;
        }

        private void txtSrcUnMapGame_Leave(object sender, EventArgs e)
        {
            if (txtSrcUnMapGame.Text.Length == 0)
            {
                txtSrcUnMapGame.Text = "Search Unmapped M/c Protocol Games";
                txtSrcUnMapGame.ForeColor = Color.DimGray;
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            PopulateSlotGames(false);
            txtSrcUnMapGame.Text = "Search Unmapped M/c Protocol Games";
            txtSrcUnMapGame.ForeColor = Color.DimGray;
        }

        private void txtSrcUnMapGame_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                btnSearch_Click(sender, e);
            }
        }

        #endregion //Events

        #region Methods

        private void LoadGames()
        {
            try
            {
                lstGameDetails = objGameLibraryBiz.GetGameDetails(Manufacturer, Category);

                if (lstGameDetails.Count > 0)
                {
                    cmbGameTitle.DisplayMember = "GameTitle";
                    cmbGameTitle.ValueMember = "GameTitleId";
                    cmbGameTitle.DataSource = lstGameDetails;

                    if (lstGameDetails.Count == 1 || GameTitleID == 0)
                        cmbGameTitle.SelectedIndex = 0;
                    else
                        cmbGameTitle.SelectedValue = GameTitleID;
                }
                else
                {
                    this.ShowWarningMessageBox(this.GetResourceTextByKey(1, "MSG_GAMEMASTERMAP_NOGAME"), this.Text);                   
                    this.DialogResult = DialogResult.Cancel;
                    this.Close();
                }

            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void PopulateSlotGames(bool bFilter)
        {
            bool bMatch = !bFilter;

            try
            {
                lvUnmapped.Items.Clear();

                foreach (MultiGameLibraryThemesDetails objMultiGameLibraryThemesDetails in objGameLibraryBiz.GetMultiGameLibraryThemesDetails(1, 0, 0, 0, 0, "ALL"))
                {
                    if (bFilter && !String.IsNullOrEmpty(txtSrcUnMapGame.Text.Trim()))
                    {
                        if (!objMultiGameLibraryThemesDetails.GameName.ToUpper().Contains(txtSrcUnMapGame.Text.ToUpper()))
                        {
                            bMatch = false;
                        }
                        else
                        {
                            bMatch = true;
                        }
                    }
                    else
                    {
                        bMatch = true;
                    }

                    if (bMatch)
                    {
                        ListViewItem objListViewItem = new ListViewItem(objMultiGameLibraryThemesDetails.GameName);
                        objListViewItem.SubItems.Add(objMultiGameLibraryThemesDetails.Manufacturer);
                        objListViewItem.SubItems.Add(objMultiGameLibraryThemesDetails.Alias_Machine_Name);
                        objListViewItem.Tag = objMultiGameLibraryThemesDetails.MG_Game_ID;
                        lvUnmapped.Items.Add(objListViewItem);
                    }
                }
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void PopulateMappings()
        {
            try
            {
                if ((cmbGameTitle.SelectedValue == null) || (cmbGameTitle.SelectedValue == "0")) return;

                lvMapped.Items.Clear();

                foreach (MultiGameLibraryThemesDetails objMultiGameLibraryThemesDetails in objGameLibraryBiz.GetMultiGameLibraryThemesDetails(Convert.ToInt32(cmbGameTitle.SelectedValue), 0, 0, 0, 0, "ALL"))
                {
                    ListViewItem objListViewItem = new ListViewItem(objMultiGameLibraryThemesDetails.GameName);
                    objListViewItem.SubItems.Add(objMultiGameLibraryThemesDetails.Manufacturer);
                    objListViewItem.SubItems.Add(objMultiGameLibraryThemesDetails.Alias_Machine_Name);
                    objListViewItem.Tag = objMultiGameLibraryThemesDetails.MG_Game_ID;
                    lvMapped.Items.Add(objListViewItem);
                }
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void GetAssetDetailsForGame(int iGameId, bool bIsMapped)
        {
            try
            {
                List<AssetDetailsForGame> lstAssetDetailsForGame = objGameLibraryBiz.GetAssetDetailsForGame(iGameId);
                if (bIsMapped)
                    FillMappedGameInfo(lstAssetDetailsForGame);
                else
                    FillUnMappedGameInfo(lstAssetDetailsForGame);
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void FillMappedGameInfo(List<AssetDetailsForGame> lstAssetDetailsForGame)
        {
            try
            {
                foreach (AssetDetailsForGame objGameDetails in lstAssetDetailsForGame)
                {
                    ListViewItem objListViewItem = new ListViewItem();
                    objListViewItem.Text = objGameDetails.Site_Name;
                    objListViewItem.SubItems.Add(objGameDetails.Bar_Position_Name);
                    objListViewItem.SubItems.Add(objGameDetails.Machine_Stock_No);
                    objListViewItem.SubItems.Add(objGameDetails.Machine_Manufacturers_Serial_No);
                    objListViewItem.SubItems.Add(objGameDetails.Game_Part_Number);
                    objListViewItem.SubItems.Add(Convert.ToString(objGameDetails.IsGameActive));
                    lvMappedGameInfo.Items.Add(objListViewItem);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void FillUnMappedGameInfo(List<AssetDetailsForGame> lstAssetDetailsForGame)
        {
            try
            {
                foreach (AssetDetailsForGame objGameDetails in lstAssetDetailsForGame)
                {
                    ListViewItem objListViewItem = new ListViewItem();
                    objListViewItem.Text = objGameDetails.Site_Name;
                    objListViewItem.SubItems.Add(objGameDetails.Bar_Position_Name);
                    objListViewItem.SubItems.Add(objGameDetails.Machine_Stock_No);
                    objListViewItem.SubItems.Add(objGameDetails.Machine_Manufacturers_Serial_No);
                    objListViewItem.SubItems.Add(objGameDetails.Game_Part_Number);
                    objListViewItem.SubItems.Add(Convert.ToString(objGameDetails.IsGameActive));
                    lvUnmappedInfo.Items.Add(objListViewItem);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void AssignGames(string SelectedGameIDs, bool bRemove)
        {
            int iGameCount;

            FormedGame objFormedGame = null;

            if (SelectedGameIDs.Length < 2)
            {
                this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_GAMEMASTERMAP_SELECTITEM"), this.Text);
                return;
            }

            try
            {
                objFormedGame = objGameLibraryBiz.IsFormedGame(SelectedGameIDs);

                objFormedGame.IsMultiGame = 1; //No more framed games, so treat all game as unique

                if (objFormedGame.IsMultiGame == 0)
                {
                    iGameCount = objFormedGame.Game_IDs.Split(',').Count() - 2;

                    if (iGameCount > 0)
                    {
                        if (this.ShowQuestionMessageBox(
                            string.Format(this.GetResourceTextByKey(1, "MSG_GAMESMASTER_TOTAL"), iGameCount.ToString()) +
                            (bRemove ? this.GetResourceTextByKey(1, "MSG_UNMAPALL") : this.GetResourceTextByKey(1, "MSG_MAPALL")), this.Text) == DialogResult.No)
                            return;
                    }
                }
                else
                {
                    objFormedGame.Game_IDs = SelectedGameIDs;
                }

                if (objGameLibraryBiz.MapGames(bRemove ? "0" : (cmbGameTitle.SelectedValue.ToString()), objFormedGame.Game_IDs) == 0)
                {
                    objGameLibraryBiz.UpdateMachineClass(objFormedGame.Machine_Class_IDs, (int)cmbGameTitle.SelectedValue, (bRemove ? 1 : 0));
                    objGameLibraryBiz.InsertNewAuditEntry(ModuleNameEnterprise.AUDIT_GAMELIBRARY, "Game Libary Mapping", (bRemove ? "Unmapping" : "Mapping") + " Game", "Game Ids " + objFormedGame.Game_IDs.Remove(0, 2) + " " + (bRemove ? "unmapped from" : "mapped to") + " Game Title :" + cmbGameTitle.Text);
                    _dialogResult = DialogResult.OK;
                }

                PopulateMappings();
                PopulateSlotGames((txtSrcUnMapGame.Text.Length == 0 || txtSrcUnMapGame.Text == "Search Unmapped M/c Protocol Games" || bRemove) ? false : true);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
        #endregion //Methods
    }
}