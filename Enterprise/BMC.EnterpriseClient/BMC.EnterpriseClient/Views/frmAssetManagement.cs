using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BMC.EnterpriseBusiness.Business;
using BMC.Common.ExceptionManagement;
using BMC.Common.Utilities;
using BMC.EnterpriseBusiness.Entities;
using BMC.Common.LogManagement;
using BMC.CoreLib.Win32;
using System.IO;
using Audit.BusinessClasses;
using Audit.Transport;
using System.Diagnostics;
using Microsoft.Win32;
using BMC.Common;
namespace BMC.EnterpriseClient.Views
{
    enum StockStatus : int
    {
        STOCK_IN_STOCK = 0,
        STOCK_IN_USE = 1,
        STOCK_IN_STOCK_UNUSABLE = 2,
        STOCK_UNDER_REPAIR = 3,
        STOCK_ON_ORDER = 4,
        STOCK_DUE_OUT = 5,
        STOCK_SOLD = 6,
        STOCK_CONVERTED = 7,
        STOCK_TERMINATED = 13
    }

    public partial class frmAssetManagement : Form
    {
        #region Private Variables
        private bool bIsNGA;
        int? oldSupplier = -1;
        int? oldDepot = -1;
        int? oldType = -1;
        int? oldClass = -1;
        int? oldStatus = -1;
        int PayTableID = 0;
        long oldGame = -1;
        bool isFirst = false;
        int? oldStaff = -1;
        bool isTemplateSettingEnabled = false;
        bool IsNew = false;
        Dictionary<string, Action<List<GetMachineDetailsResult>>> dic_Invoke = new Dictionary<string, Action<List<GetMachineDetailsResult>>>();
        List<GetMachineTypeDetailsResult> lst_MCType = null;

        Action act_ref = null;
        Action<int> act_removeMC = null;
        string TemplateName = string.Empty;
        string FirstString = string.Empty;
        string AssetNumber = string.Empty;
        Button buttonCancel = new Button();
        private const string strUnAllocate = "--UNALLOCATED--";
        private const string ScreenName = "Asset Management => ";
        private  string strAny = BMC.Common.ResourceExtensions.GetResourceTextByKey(null, "Key_Any").ToUpper();// "--ANY--"; 
           
        private int _GameNameID = 0;
        #endregion


        #region Constructor
        public frmAssetManagement()
        {
            InitializeComponent();
            act_ref = RefreshTreeView;
            act_removeMC = RemoveMachineFromDisplay;
            SetTagProperty();//Externalization changes
           
        }
        //Externalization changes
        private void SetTagProperty()
        {
            try
            {
                this.btnClose.Tag = "Key_CloseCaption";
                this.btnDisplay.Tag = "Key_DisplayCaption";
                this.btnImpAsset.Tag = "Key_ImportAssetdetailCaption";
                this.btnAvailabilitySelectNone.Tag = "Key_SelectNoneCaption";
                this.IDM_ADD_MODEL_NGA.Tag = "Key_AddCategory";
                this.IDM_ADD_TYPE.Tag = "Key_AddType";
                this.btnEditTypes.Tag = "Key_Admin_WOShortCut";
                this.btnEditManufacturer.Tag = "Key_Admin_WOShortCut";
                this.btnModelType.Tag = "Key_Admin_WOShortCut";
                this.btnGameCat.Tag = "Key_Admin_WOShortCut";
                this.btnEditSupplier.Tag = "Key_Admin_WOShortCut";
                this.btnEditDepot.Tag = "Key_Admin_WOShortCut";
                this.BtnEditUser.Tag = "Key_Admin_WOShortCut";                
                this.IDM_BUY_MACHINE.Tag = "Key_BuyMachine";
                this.label4.Tag = "Key_CabinetModelTypeColon";
                this.btnDepreciation.Tag = "Key_DepreciationCaption1";
                this.IDM_DELETE_TEMPLATE.Tag = "Key_DeleteTemplate";
                this.label7.Tag = "Key_DepotColon";
                this.chkDisplayInstallationDetails.Tag = "Key_DisplayInstallationDetails";
                this.IDM_EDIT_MODEL_NGA.Tag = "Key_EditCategory";
                this.IDM_EDIT_MACHINE.Tag = "Key_EditMachine";
                this.IDM_EDIT_TEMPLATE.Tag = "Key_EditTemplate";
                this.IDM_EDIT_TYPE.Tag = "Key_EditType";
                this.btnExAsset.Tag = "Key_ExportAssetdetails";
                this.grpFilter.Tag = "Key_lbl_Filter";
                this.label5.Tag = "Key_GameCategoryColon";
                this.lblGame.Tag = "Key_GameNameColon";
                this.label1.Tag = "Key_GameTitleAssetNoSerialNo";
                this.label10.Tag = "Key_GroupStockByColon";
                this.label9.Tag = "Key_MachineAvaliabilityColon";
                this.label2.Tag = "Key_MachineTypesColon";
                this.label3.Tag = "Key_ManufacturerColon";
                this.IDM_MODEL_ADMIN.Tag = "Key_ModelAdministrator";
                this.label6.Tag = "Key_OperatorColon";
                this.label8.Tag = "Key_RepresentativeColon";
                this.IDM_SAVE_TEMPLATE.Tag = "Key_SaveAsTemplate";
                this.btnAvailabilitySelectAll.Tag = "Key_SelectAllCaption";
                this.IDM_TERMINATE_MACHINE.Tag = "Key_TerminateMachine";
                this.IDM_VIEW_TERMINATE_MACHINE.Tag = "Key_ViewTerminationDetails";
                this.gpView.Tag = "Key_ViewingOptions";
                
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
        #endregion

        class MachineAvailability
        {
            public string Caption { get; set; }
            public int StockValue { get; set; }
        }

        #region Events
        private void toolStrip_buymachine_Click(object sender, EventArgs e)
        {
            bool IsNew = true;
            if (tvStock.SelectedNode != null)
            {
                //frm_buy
                //foreach (Form frm in this.OwnedForms)
                //{
                //    if (frm != null && frm.Name.Equals("frmBuy"))
                //    {
                //        Win32Extensions.ShowInfoMessageBox("The machine purchasing/editing screen is already active");
                //        return;
                //    }
                //}
                LogManager.WriteLog(ScreenName + @"BUY_MACHINE_Click", LogManager.enumLogLevel.Debug);
                int iMachineTypeID = 0;
                if (tvStock.SelectedNode.Parent != null)
                    iMachineTypeID = GetNodeID(tvStock.SelectedNode.Parent.Name);
                else
                    iMachineTypeID = GetNodeID(tvStock.SelectedNode.Name);
                frmBuy frm_buy = new frmBuy(GetNodeID(tvStock.SelectedNode.Name), bIsNGA, false, false, act_ref, act_removeMC, isTemplateSettingEnabled, iMachineTypeID, IsNew);
                frm_buy.Name = "frmBuy";
                // frm_buy.Owner = this;
                // frm_buy.Show();
                frm_buy.ShowDialogEx(this);

            }
        }

        /// <summary>
        /// Form Load 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Asset_Management_Load(object sender, EventArgs e)
        {
            try
            {
                bIsNGA = false;
                LogManager.WriteLog(ScreenName + "Load Asset Management", LogManager.enumLogLevel.Debug);
                LockControls();
                
                List<ComboBoxItem> lstGroupStock = new List<ComboBoxItem>();

                lstGroupStock.Add(
                    new ComboBoxItem()
                    {
                        Text = this.GetResourceTextByKey("Key_Availability"),
                        Value = "Availability"

                    });
                lstGroupStock.Add(
                    new ComboBoxItem()
                    {
                        Text = this.GetResourceTextByKey("Key_Depot"),
                        Value = "Depot"

                    });
                lstGroupStock.Add(
                    new ComboBoxItem()
                    {
                        Text = this.GetResourceTextByKey("Key_MachineType"),
                        Value = "Machine Type"

                    });
                lstGroupStock.Add(
                    new ComboBoxItem()
                    {
                        Text = this.GetResourceTextByKey("Key_Representative"),
                        Value = "Representative"
                    });

                cmbGroupStockBy.DataSource = null;
                cmbGroupStockBy.DataSource=lstGroupStock;
                cmbGroupStockBy.DisplayMember = "Text";
                cmbGroupStockBy.ValueMember = "Value";
                cmbGroupStockBy.SelectedValue = "Machine Type";
                
                //Load GameDetails
                cmbFilterbyGame.Items.Clear();
                List<rsp_GetGameNames> lst_Gamename = AssetManagementBiz.CreateInstance().LoadGameNames();
                lst_Gamename.Insert(0, new rsp_GetGameNames { MG_Game_ID = 0, MG_Game_Name = strAny });
                lst_Gamename.Insert(1, new rsp_GetGameNames { MG_Game_ID = 0, MG_Game_Name = "MULTI GAME" });
                cmbFilterbyGame.DataSource = lst_Gamename;
                cmbFilterbyGame.DisplayMember = "MG_Game_Name";
                cmbFilterbyGame.ValueMember = "MG_Game_ID";

                //Load GameDetails


                LoadMachineType();
                //Load Manufacturer Details

                LoadManufacturer();
                LoadModelType();
                LoadGameCategory();
                LoadSupplier();
                LoadStaffRepresentative();
                //Load Machine Details
                lstMachineAvailability.Items.Clear();
                lstMachineAvailability.DisplayMember = "Caption";
                lstMachineAvailability.ValueMember = "StockValue";
                lstMachineAvailability.Items.Add(new MachineAvailability { Caption = this.GetResourceTextByKey("Key_Availability"), StockValue = (int)StockStatus.STOCK_IN_STOCK }, true);
                lstMachineAvailability.Items.Add(new MachineAvailability { Caption = this.GetResourceTextByKey("Key_InUse"), StockValue = (int)StockStatus.STOCK_IN_USE }, true);
                lstMachineAvailability.Items.Add(new MachineAvailability { Caption = this.GetResourceTextByKey("Key_Terminated"), StockValue = (int)StockStatus.STOCK_TERMINATED }, true);

                AddInvocationlist();
                isTemplateSettingEnabled = IsTemplateSettingEnabled();

                if (Convert.ToBoolean(AdminBusiness.GetSetting("ImportExport_AssetFile", "False")))
                {
                    btnImpAsset.Visible = btnExAsset.Visible = true;
                    btnExAsset.Enabled = AppGlobals.Current.HasUserAccess("HQ_Stock_Asset_Export");
                    btnImpAsset.Enabled = AppGlobals.Current.HasUserAccess("HQ_Stock_Asset_Import");
                }

                DisableContextMenuItem(false);
                btnDisplay_Click(this, null);
                this.ResolveResources();//Externalization changes

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                Win32Extensions.ShowInfoMessageBox(this,this.GetResourceTextByKey("MSG_ASSETMANA_ASSET"), this.Text);
            }
        }

        private bool IsTemplateSettingEnabled()
        {
            try
            {
                bool isTemplateSettingEnabled = Convert.ToString(AdminBusiness.GetSetting("UseAssetTemplate", "false")).ToUpper().Trim().Equals("TRUE");
                if (!isTemplateSettingEnabled)
                    IDM_SAVE_TEMPLATE.Visible = isTemplateSettingEnabled;
                IDM_EDIT_TEMPLATE.Visible = isTemplateSettingEnabled;
                IDM_DELETE_TEMPLATE.Visible = isTemplateSettingEnabled;
                IDM_SAVE_TEMPLATE.Enabled = AppGlobals.Current.HasUserAccess("HQ_Template_Create");
                IDM_EDIT_TEMPLATE.Enabled = AppGlobals.Current.HasUserAccess("HQ_Template_Edit");
                IDM_DELETE_TEMPLATE.Enabled = AppGlobals.Current.HasUserAccess("HQ_Template_Delete");
                //IDM_SAVE_TEMPLATE.Enabled = false;
                //IDM_EDIT_TEMPLATE.Enabled = false;
                //IDM_DELETE_TEMPLATE.Enabled = false;
                //IDM_SAVE_TEMPLATE.Visible = false;
                //IDM_EDIT_TEMPLATE.Visible = false;
                //IDM_DELETE_TEMPLATE.Visible = false;
                return isTemplateSettingEnabled;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return false;
        }

        private void DisableContextMenuItem(bool IsVisible)
        {
            foreach (ToolStripMenuItem t_strip in ctMenuAssetTree.Items)
            {
                t_strip.Visible = IsVisible;
            }
        }
        /// <summary>
        /// Display machine details by stock view i.e.(M|C type,Avail,In Use)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDisplay_Click(object sender, EventArgs e)
        {
            try
            {

                tvStock.Nodes.Clear();
                int Machine_Type_ID = 0;
                int Manufacturer_ID = 0;
                int Operator_ID = 0;
                int Depot_ID = 0;
                int ModelTypeID = 0;
                int GameCategory_ID = 0;
                int RepresentativeID = 0;
                string _GameName = string.Empty;
                oldSupplier = -1;
                oldDepot = -1;
                oldType = -1;
                oldClass = -1;
                oldStatus = -1;
                oldStaff = -1;
                string strSearchCriteria = GetSearchString();
                string Machine_Status = "";
                Int32 _GameID = Convert.ToInt32(cmbFilterbyGame.SelectedValue);
                if (_GameID <= 0)
                {
                    _GameNameID = 0;
                }
                else
                {
                    _GameNameID = _GameID;
                }

                if (cmbFilterbyGame.SelectedIndex > 0)
                    _GameName = cmbFilterbyGame.Text;
                else
                    _GameName = "--ANY--";

                GetControlsValue(ref Machine_Type_ID, ref  Manufacturer_ID, ref Machine_Status, ref Operator_ID,
                                 ref Depot_ID, ref RepresentativeID, ref ModelTypeID, ref GameCategory_ID);

                LogManager.WriteLog(ScreenName + @"btnDisplay_Click:Get machine details ", LogManager.enumLogLevel.Debug);
                //Get Machine Details
                List<GetMachineDetailsResult> lst_MDetails = LoadMachineDetails(Machine_Type_ID, Operator_ID, Depot_ID, RepresentativeID, Manufacturer_ID, GameCategory_ID, ModelTypeID, Machine_Status, cmbGroupStockBy.SelectedValue.ToString(), strSearchCriteria, _GameNameID, _GameName);



                dic_Invoke[cmbGroupStockBy.SelectedValue.ToString()](lst_MDetails);

                tvStock.Visible = true;

                if (tvStock.Nodes.Count > 0)
                {
                    if (tvStock.Nodes.Count == 2)//to expand first level node
                    {
                        tvStock.Nodes[0].Expand();
                        tvStock.Nodes[1].Expand();
                    }
                    tvStock.SelectedNode = tvStock.Nodes[0];

                }

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                Win32Extensions.ShowInfoMessageBox(this,this.GetResourceTextByKey("MSG_ASSETMANA_MACHINE"), this.Text);
            }

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            try
            {
                LogManager.WriteLog(ScreenName + "btnClose_Click: Form Closing...", LogManager.enumLogLevel.Info);
                this.Close();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void tvStock_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button.Equals(MouseButtons.Right))
                {
                    TreeNode tn = tvStock.HitTest(e.X, e.Y).Node;
                    if (tn != null)
                    {
                        //FirstString = string.Empty;
                        //FirstString = tvStock.SelectedNode.Text.ToString(); // tn.Nodes[0].Tag.ToString();
                        //string[] words = FirstString.Split(' ');
                        // AssetNumber = string.Empty;

                        //for(int i=0;i<words;i++) 
                        //{
                        //   string first=words[0];
                        //}


                        tvStock.SelectedNode = tn;
                        string strNodeType = GetNodeType(tn.Name);
                        int nodeId = GetNodeID(tn.Name);
                        PopUpMenu(strNodeType, tn.Text.ToUpper().IndexOf("[NGA]") != -1 ? true : false, tn.Text);
                        switch (strNodeType)
                        {
                            case "MT": 
                                bIsNGA = Convert.ToBoolean(lst_MCType.Where(nga => nga.Machine_Type_ID == nodeId).Select(nga => nga.IsNonGamingAssetType).First());
                                IDM_DELETE_TEMPLATE.Visible = false;
                                IDM_EDIT_TEMPLATE.Visible = false;
                                IDM_SAVE_TEMPLATE.Visible = false;
                                break;
                            case "MC":
                                bIsNGA = true;
                                IDM_DELETE_TEMPLATE.Visible = false;
                                IDM_EDIT_TEMPLATE.Visible = false;
                                IDM_SAVE_TEMPLATE.Visible = false;

                                break;
                            case "MA":
                                bIsNGA = (tn.Text.ToUpper().IndexOf("[NGA]") != -1);
                                if (bIsNGA == false)
                                {
                                    if (!string.IsNullOrEmpty(Convert.ToString(tn.Tag)))
                                    {
                                        AssetNumber = Convert.ToString(tn.Tag);
                                        LogManager.WriteLog("Selected Asset No" + AssetNumber, LogManager.enumLogLevel.Info);
                                    }
                                    IsTemplateSettingEnabled();

                                }
                                else
                                {
                                    IDM_DELETE_TEMPLATE.Visible = false;
                                    IDM_EDIT_TEMPLATE.Visible = false;
                                    IDM_SAVE_TEMPLATE.Visible = false;
                                }
                                break;
                            case "MG":
                                IDM_DELETE_TEMPLATE.Visible = false;
                                IDM_EDIT_TEMPLATE.Visible = false;
                                IDM_SAVE_TEMPLATE.Visible = false;
                                break;
                        }
                        ctMenuAssetTree.Show(tvStock, e.Location);
                    }
                }
                //else if (e.Button.Equals(MouseButtons.Left))
                //{
                //    TreeNode tn = tvStock.SelectedNode;
                //    if (tn != null)
                //    {
                //        switch (GetNodeType(tn.Name))
                //        {
                //            case "MT":
                //                bIsNGA = false;
                //                break;
                //            case "MC":
                //                bIsNGA = true;
                //                break;
                //            case "MA":
                //                bIsNGA = (tn.Text.ToUpper().IndexOf("[NGA]") != -1);
                //                break;
                //        }
                //    }
                //}
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        /// <summary>
        /// calling buy machine form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IDM_EDIT_MACHINE_Click(object sender, EventArgs e)
        {
            try
            {
                if (tvStock.SelectedNode != null)
                {
                    //foreach (Form frm in this.OwnedForms)
                    //{
                    //    if (frm != null && frm.Name.Equals("frmBuy"))
                    //    {
                    //        Win32Extensions.ShowInfoMessageBox("The machine purchasing/editing screen is already active");
                    //        return;
                    //    }
                    //}

                    int MachineID = GetNodeID(tvStock.SelectedNode.Name);
                    int iMachineTypeID = 0;

                    if (bIsNGA)
                    {
                        if (tvStock.SelectedNode.Parent.Parent != null)
                            iMachineTypeID = GetNodeID(tvStock.SelectedNode.Parent.Parent.Name);
                    }
                    else
                    {
                        if (tvStock.SelectedNode.Parent != null)
                            iMachineTypeID = GetNodeID(tvStock.SelectedNode.Parent.Name);
                    }


                    //if (tvStock.SelectedNode.Parent.Parent != null)
                    //    iMachineTypeID = GetNodeID(tvStock.SelectedNode.Parent.Parent.Name);
                    //else if (tvStock.SelectedNode.Parent != null)
                    //    iMachineTypeID = GetNodeID(tvStock.SelectedNode.Parent.Name);

                    LogManager.WriteLog(ScreenName + @"EDIT_MACHINE_Click:Machine ID:" + MachineID + "; Asset Type: " + (bIsNGA ? "NonGaming" : "Gaming"), LogManager.enumLogLevel.Debug);
                    frmBuy frm_buy = new frmBuy(MachineID, bIsNGA, AssetManagementBiz.CreateInstance().CheckMachineInUse(MachineID), true, act_ref, act_removeMC, isTemplateSettingEnabled, iMachineTypeID, IsNew);
                    frm_buy.Name = "frmBuy";
                    frm_buy.Owner = this;
                    frm_buy.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

        }


        private void IDM_TERMINATE_MACHINE_Click(object sender, EventArgs e)
        {
            try
            {

                int MachineID = GetNodeID(tvStock.SelectedNode.Name);
                LogManager.WriteLog(ScreenName + @"IDM_TERMINATE_MACHINE_Click:Machine ID:" + MachineID + "; Asset Type: " + (bIsNGA ? "NonGaming" : "Gaming"), LogManager.enumLogLevel.Debug);
                frmTerminateMachine frm_terminate = new frmTerminateMachine();
                frm_terminate.StockNo = tvStock.SelectedNode.Text;
                frm_terminate.IsNGA = bIsNGA;
                frm_terminate.IsEdit = false;
                frm_terminate.Name = "frmTerminateMachine";
                frm_terminate.Owner = this;
                frm_terminate.ShowDialog();
                btnDisplay_Click(this, null);

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

        }

        private void btnAvailabilitySelectNone_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < lstMachineAvailability.Items.Count; i++)
            {
                lstMachineAvailability.SetItemChecked(i, false);
            }

        }

        private void btnAvailabilitySelectAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < lstMachineAvailability.Items.Count; i++)
            {
                lstMachineAvailability.SetItemChecked(i, true);
            }
        }

        private void IDM_ADD_MODEL_NGA_Click(object sender, EventArgs e)
        {
            try
            {
                int MachineTypeID = GetNodeID(tvStock.SelectedNode.Name);
                LogManager.WriteLog(ScreenName + "IDM_EDIT_MODEL_NGA_Click:MachineTypeID:" + MachineTypeID, LogManager.enumLogLevel.Info);
                frmMachineModelAdminNGA MCModelNGA = new frmMachineModelAdminNGA(act_ref);
                MCModelNGA.MachineClassID = 0;
                MCModelNGA.MachineTypeID = MachineTypeID;
                MCModelNGA.ShowDialog();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void IDM_EDIT_MODEL_NGA_Click(object sender, EventArgs e)
        {
            try
            {
                int MachineClassID = GetNodeID(tvStock.SelectedNode.Name);
                LogManager.WriteLog(ScreenName + "IDM_EDIT_MODEL_NGA_Click:MachineClassID:" + MachineClassID, LogManager.enumLogLevel.Info);
                frmMachineModelAdminNGA MCModelNGA = new frmMachineModelAdminNGA(act_ref);
                MCModelNGA.MachineClassID = MachineClassID;
                MCModelNGA.ShowDialog();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void IDM_EDIT_TYPE_Click(object sender, EventArgs e)
        {
            try
            {
                if ((tvStock.SelectedNode != null) && (GetNodeType(tvStock.SelectedNode.Name) == "MT"))
                {
                    int MachineTypeID = GetNodeID(tvStock.SelectedNode.Name);
                    LogManager.WriteLog(ScreenName + "IDM_EDIT_TYPE_Click:MachineTypeID:" + MachineTypeID, LogManager.enumLogLevel.Info);
                    frmAddMachineType frm = new frmAddMachineType();
                    frm.ShowMe(MachineTypeID);
                    frm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void IDM_ADD_TYPE_Click(object sender, EventArgs e)
        {
            try
            {
                if ((tvStock.SelectedNode != null) && (GetNodeType(tvStock.SelectedNode.Name) == "MT"))
                {
                    int MachineTypeID = GetNodeID(tvStock.SelectedNode.Name);
                    LogManager.WriteLog(ScreenName + "IDM_ADD_TYPE_Click:MachineTypeID:" + MachineTypeID, LogManager.enumLogLevel.Info);
                    frmAddMachineType frm = new frmAddMachineType();
                    frm.ShowMeNew(MachineTypeID);
                    frm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btnDepreciation_Click(object sender, EventArgs e)
        {
            try
            {
                LogManager.WriteLog(ScreenName + "btnDepreciation_Click:Depreciation form begin to show", LogManager.enumLogLevel.Info);
                frmDepreciation frm_dep = new frmDepreciation();
                frm_dep.ShowDialog();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btnEditTypes_Click(object sender, EventArgs e)
        {
            try
            {

                if (cmbMachineTypes.SelectedIndex > 0)
                {
                    int MachineTypeID = (int)cmbMachineTypes.SelectedValue;
                    LogManager.WriteLog(ScreenName + "btnEditTypes_Click:MachineTypeID:" + MachineTypeID, LogManager.enumLogLevel.Info);
                    frmAddMachineType frmMtype = new frmAddMachineType();
                    frmMtype.ShowMe(MachineTypeID);
                    frmMtype.ShowDialog();
                }
                else
                {
                    frmAddMachineType frmMtype = new frmAddMachineType();

                    frmMtype.ShowDialog();
                    LoadMachineType();

                }

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btnEditManufacturer_Click(object sender, EventArgs e)
        {
            try
            {

                if (cmbManufacturer.SelectedIndex > 0)
                {
                    int ManufacturerID = (int)cmbManufacturer.SelectedValue;
                    LogManager.WriteLog(ScreenName + "btnEditManufacturer_Click:ManufacturerID:" + ManufacturerID, LogManager.enumLogLevel.Info);
                    frmManufacturer frmMan = new frmManufacturer(ManufacturerID);
                    frmMan.ShowDialog();
                    LoadManufacturer();
                }
                else
                {
                    frmManufacturer frmMan = new frmManufacturer();
                    frmMan.ShowDialog();
                    LoadManufacturer();
                }

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

        }

        private void btnModelType_Click(object sender, EventArgs e)
        {
            try
            {
                LogManager.WriteLog(ScreenName + "btnModelType_Click:ModelType form begin to shown", LogManager.enumLogLevel.Info);
                frmModelType frmModel = new frmModelType();
                frmModel.ShowDialog();
                LoadModelType();

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btnGameCat_Click(object sender, EventArgs e)
        {
            try
            {

                if (cmbGameCategory.SelectedIndex > 1)
                {
                    int GameCategoryID = (int)cmbGameCategory.SelectedValue;
                    LogManager.WriteLog(ScreenName + "btnGameCat:GameCategoryID:" + GameCategoryID, LogManager.enumLogLevel.Info);

                    frmGameCategoryAdmin frmGameCat = new frmGameCategoryAdmin(GameCategoryID);
                    frmGameCat.ShowDialog();
                    LoadGameCategory();
                }
                else
                {
                    frmGameCategoryAdmin frmGameCat = new frmGameCategoryAdmin(0);
                    frmGameCat.ShowDialog();
                    LoadGameCategory();
                }

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void cmbSupplier_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbSupplier.SelectedIndex == -1)
                return;
            try
            {
                LoadDepot((int)cmbSupplier.SelectedValue);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

        }

        private void btnEditSupplier_Click(object sender, EventArgs e)
        {
            try
            {

                if (cmbSupplier.SelectedIndex > 0)
                {
                    int OperatorID = (int)cmbSupplier.SelectedValue;
                    LogManager.WriteLog(ScreenName + "btnEditSupplier_Click(:OperatorID:" + OperatorID, LogManager.enumLogLevel.Info);
                    frmOperator frm_op = new frmOperator(OperatorID);
                    frm_op.ShowDialog();
                    LoadSupplier();
                }
                else
                {
                    frmOperator frm_op = new frmOperator();
                    frm_op.ShowDialog();
                    LoadSupplier();
                }


            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

        }

        private void btnEditDepot_Click(object sender, EventArgs e)
        {

            try
            {
                int? OperatorID = null;
                int? DepotID = null;
                if (cmbDepot.SelectedIndex > 0)
                {
                    DepotID = (int)cmbDepot.SelectedValue;
                }
                if (cmbSupplier.SelectedIndex > 0)
                {
                    OperatorID = (int)cmbSupplier.SelectedValue;
                }
                LogManager.WriteLog(ScreenName + "btnEditDepot_Click(:OperatorID:" + (OperatorID ?? 0).ToString() + "DepotID:" + (DepotID ?? 0), LogManager.enumLogLevel.Info);
                frmDepot frm_depot = new frmDepot(OperatorID, DepotID);
                frm_depot.ShowDialog();
                LoadDepot(OperatorID ?? 0);



            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

        }

        private void BtnEditUser_Click(object sender, EventArgs e)
        {

            try
            {


                if (cmbRepresentative.SelectedIndex > 0)
                {
                    StaffNameResult lstStaff = cmbRepresentative.SelectedItem as StaffNameResult;
                    if (lstStaff != null)
                    {
                        LogManager.WriteLog(ScreenName + "BtnEditUser_Click(:Staff_Name:" + lstStaff.Staff_Name, LogManager.enumLogLevel.Info);
                        frmUserAdministration frm_rep = new frmUserAdministration(lstStaff.Staff_Representative_Name, lstStaff.Staff_ID, true);
                        frm_rep.ShowDialog();
                        LoadStaffRepresentative();
                    }
                }
                else
                {
                    frmUserAdministration frm_rep = new frmUserAdministration(AppGlobals.Current.LoggedinUser.UserName, AppGlobals.Current.LoggedinUser.SecurityUserID, true);
                    frm_rep.ShowDialog();
                    LoadStaffRepresentative();
                }

            }
            catch (Exception ex)
            {

                ExceptionManager.Publish(ex);
            }
        }

        private void tvStock_AfterExpand(object sender, TreeViewEventArgs e)
        {
            try
            {
                TreeNode tv_Pay = e.Node;
                if (tv_Pay != null && tv_Pay.Nodes.Count == 1 && tv_Pay.Nodes[0].Name == "PD")
                {
                    int MC_ID = (int)tv_Pay.Nodes[0].Tag;
                    string displayText = tv_Pay.Nodes[0].Text;
                    e.Node.Nodes.Remove(tv_Pay.Nodes[0]);
                    bool Availability = cmbGroupStockBy.SelectedValue.ToString().Equals("Availability");
                    GetPayTableDetails(MC_ID, e.Node, displayText, PayTableID, Availability);
                    PayTableID = 0;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

        }

        private void IDM_VIEW_TERMINATE_MACHINE_Click(object sender, EventArgs e)
        {
            try
            {

                int MachineID = GetNodeID(tvStock.SelectedNode.Name);
                LogManager.WriteLog(ScreenName + @"IDM_VIEW_TERMINATE_MACHINE_Click:Machine ID:" + MachineID + "; Asset Type: " + (bIsNGA ? "NonGaming" : "Gaming"), LogManager.enumLogLevel.Debug);
                frmTerminateMachine frm_terminate = new frmTerminateMachine();
                frm_terminate.StockNo = tvStock.SelectedNode.Text;
                frm_terminate.IsNGA = bIsNGA;
                frm_terminate.IsEdit = true;
                frm_terminate.Name = "frmTerminateMachine";
                frm_terminate.Owner = this;
                frm_terminate.ShowDialog();

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

        }

        private void IDM_SAVE_TEMPLATE_Click(object sender, EventArgs e)
        {
            try
            {
                TemplateName = Microsoft.VisualBasic.Interaction.InputBox(this.GetResourceTextByKey(1, "MSG_ENTER_NEW_TEMPLATE"), this.GetResourceTextByKey(1, "MSG_ASSET_TEMPLATE_TITLE"), "", (Screen.PrimaryScreen.WorkingArea.Width - 80) / 2, (Screen.PrimaryScreen.WorkingArea.Height - 80) / 2);

                if (TemplateName.Trim() == "")
                    buttonCancel.DialogResult = DialogResult.Cancel;
                else
                {
                    if (TemplateName.Length > 50)
                        this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_ASSETMANA_INVALIDTEMPNAME"), this.Text);

                    else if (TemplateName != null)
                    {
                        if (AssetManagementBiz.CreateInstance().IsTemplateNameExists(TemplateName) == 1)
                        {
                            this.ShowWarningMessageBox(this.GetResourceTextByKey(1, "MSG_ASSETMANA_NAMEEXISTS"), this.Text);
                            return;
                        }
                        AssetManagementBiz.CreateInstance().InsertAssetTemplate(AssetNumber, TemplateName);
                        this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_ASSETMANA_TEMPSUCCESS"), this.Text);
                        AddTemplateAudit();
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);

            }
        }

        private void IDM_EDIT_TEMPLATE_Click(object sender, EventArgs e)
        {
            try
            {
                frmBuy buy = new frmBuy();
                buy.ShowDialog();
                //int IsEdit = 1;
                //frmAssetTemplate objasset = new frmAssetTemplate(IsEdit, AssetNumber);
                //objasset.ShowDialog();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void IDM_DELETE_TEMPLATE_Click(object sender, EventArgs e)
        {
            try
            {
                int IsDelete = 0;
                frmAssetTemplate objasset = new frmAssetTemplate(IsDelete, AssetNumber);
                objasset.ShowDialog();

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void IDM_MODEL_ADMIN_Click(object sender, EventArgs e)
        {
            try
            {
                LogManager.WriteLog(ScreenName + "IDM_MODEL_ADMIN_Click:MachineModelAdmin form begin to shown", LogManager.enumLogLevel.Info);
                frmMachineModelAdmin frmModel = new frmMachineModelAdmin();
                frmModel.ShowMe(GetNodeID(tvStock.SelectedNode.Name));

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        /// <summary>
        /// Invoke Export Assest details binary
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExImpAsset_Click(object sender, EventArgs e)
        {
            try
            {
                //Check Excel Application installed or not-start
                //Type officeType = Type.GetTypeFromProgID("Excel.Application");
                //if (officeType == null)
                // {
                //     this.ShowWarningMessageBox("The Microsoft Office package is not currently installed");
                //     return;
                // }
                //Check Excel Application installed or not-End
                string strFileName = Path.GetFullPath(Path.Combine(Application.StartupPath,  "ExportImportAssetDetails.exe"));

                if (File.Exists(strFileName))
                {
                    //string strArguments = (AppGlobals.Current.HasUserAccess("HQ_Stock_Asset_Export") ? "Y" : "N")
                    //    + " N " + (AppGlobals.Current.UserId)
                    //    + " " + (AppGlobals.Current.UserName)
                    //     + " \"" + (BMC.Common.Security.CryptographyHelper.Decrypt(GetRegistryValue())) + "\"";

                    string strArguments =
                        "1 " + (AppGlobals.Current.UserId)
                        + " " + (AppGlobals.Current.UserName)
                         + " \"" + (BMC.Common.Security.CryptographyHelper.Decrypt(GetRegistryValue())) + "\"";


                    AppEntryPoint.Current.StartProcess(sender, null, strFileName, strArguments, true);
                }
                else
                    this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_ASSETMANA_EXPORT_NOT_INSTALL"), this.Text);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        /// <summary>
        /// Invoke Export Import Assest details binary
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void btnImpAsset_Click(object sender, EventArgs e)
        {
            try
            {
                //Check Excel Application installed or not-start
                //Type officeType = Type.GetTypeFromProgID("Excel.Application");
                //RegistryKey key = Registry.ClassesRoot;
                //RegistryKey excelKey = key.OpenSubKey("Excel.Application");
                //if (officeType == null)// || excelKey == null)
                //{
                //    this.ShowWarningMessageBox("The Microsoft Office package is not currently installed");
                //    return;
                //}
                //Check Excel Application installed or not-End
                string strFileName = Path.GetFullPath(Path.Combine(Application.StartupPath, "ExportImportAssetDetails.exe"));

                if (File.Exists(strFileName))
                {
                    //string strArguments = "N " + (AppGlobals.Current.HasUserAccess("HQ_Stock_Asset_Import") ? "Y" : "N")
                    //    + " " + (AppGlobals.Current.UserId)
                    //    + " " + (AppGlobals.Current.UserName)
                    //    +" \"" + (BMC.Common.Security.CryptographyHelper.Decrypt(GetRegistryValue())) + "\"";
                    string strArguments =
                        "2 " + (AppGlobals.Current.UserId)
                        + " " + (AppGlobals.Current.UserName)
                         + " \"" + (BMC.Common.Security.CryptographyHelper.Decrypt(GetRegistryValue())) + "\"";
                    AppEntryPoint.Current.StartProcess(sender, null, strFileName, strArguments, true);
                }
                else
                    this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_ASSETMANA_IMPORT_NOT_INSTALL"), this.Text);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        #endregion

        #region Private Methods

        void RefreshTreeView()
        {
            btnDisplay_Click(this, null);
        }

        string GetRegistryValue()
        {
            string retval = "";
            try
            {
                retval = BMCRegistryHelper.GetRegKeyValue("CashMasterHQ\\Settings", "SQLConnect", "").ToString();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return retval;
        }

        /// <summary>
        /// call Method at runtime by using action delegate
        /// </summary>
        private void AddInvocationlist()
        {
            dic_Invoke.Clear();
            dic_Invoke.Add("Depot", LoadDepot);
            dic_Invoke.Add("Availability", LoadAvailability);
            dic_Invoke.Add("Machine Type", LoadMachineType);
            dic_Invoke.Add("Game Category", LoadGameCategory);
            dic_Invoke.Add("Representative", LoadRepresentative);
        }
        /// <summary>
        /// Search Machine based on user query string (i.e '%W')
        /// </summary>
        /// <returns></returns>
        private string GetSearchString()
        {
            string qryMach = "";
            try
            {

                if (txtMachineName.Text != "")
                {
                    string myChar;
                    for (int i = 1; i <= txtMachineName.Text.Length; i++)
                    {
                        myChar = txtMachineName.Text.Substring((i - 1), 1);
                        switch (myChar)
                        {
                            case "\'":
                                qryMach = (qryMach + "[\'\']");
                                break;
                            case "-":
                                qryMach = (qryMach + "[-]");
                                break;
                            default:
                                qryMach = (qryMach + myChar);
                                break;
                        }
                    }
                    if (((qryMach.IndexOf("*", 0) + 1)
                                == 0)
                                && (((qryMach.IndexOf("?", 0) + 1)
                                == 0)
                                && (((qryMach.IndexOf("%", 0) + 1)
                                == 0)
                                && ((qryMach.IndexOf("_", 0) + 1)
                                == 0))))
                    {
                        qryMach = "*"
                                    + (qryMach + "*");
                    }
                    qryMach = qryMach.Replace("*", "%").Replace("?", "_");


                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                qryMach = "";
            }
            return qryMach;

        }



        private void LockControls()
        {
            try
            {
                btnDepreciation.Visible = AppGlobals.Current.HasUserAccess("HQ_Stock_Depreciation");

                btnEditTypes.Visible = AppGlobals.Current.HasUserAccess("hq_stock_machine_type");
                btnEditManufacturer.Visible = AppGlobals.Current.HasUserAccess("HQ_Stock_Manufacturer");
                btnEditSupplier.Visible = AppGlobals.Current.HasUserAccess("HQ_Stock_Supplier");
                //btnEditDepot.Visible = AppGlobals.Current.HasUserAccess("HQ_Admin_Depot_Edit");
                //btnEditComponent.Visible = AppGlobals.Current.HasUserAccess("HQ_Stock_Component");

                //IDM_FILE.Visible = true;
                IDM_ADD_TYPE.Visible = AppGlobals.Current.HasUserAccess("hq_stock_machine_type");
                IDM_EDIT_TYPE.Visible = AppGlobals.Current.HasUserAccess("hq_stock_machine_type");

                //NGA Changes
                IDM_ADD_MODEL_NGA.Visible = AppGlobals.Current.HasUserAccess("hq_stock_machine_model");
                IDM_EDIT_MODEL_NGA.Visible = AppGlobals.Current.HasUserAccess("hq_stock_machine_model");


                // Depot & Operator - Admin button visibility  based on user access- For CR#-171903 
                //
                // * User who is not having access to view Depot Data is able to view Depot in Asset Screen.
                //  * User who is not having access to view Operator Data is able to view Operator in Asset Screen.
                //  * 

                btnEditSupplier.Enabled = AppGlobals.Current.HasUserAccess("HQ_Admin_Operator");
                btnEditDepot.Enabled = AppGlobals.Current.HasUserAccess("HQ_Admin_Depot");
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void GetControlsValue(ref int Machine_Type_ID, ref int Manufacturer_ID, ref string Machine_Status,
                                    ref int Operator_ID, ref int Depot_ID, ref int RepresentativeID
                                    , ref int ModelTypeID, ref int GameCategory_ID)
        {
            try
            {
                if (cmbMachineTypes.SelectedIndex > 0)
                {
                    Machine_Type_ID = (int)cmbMachineTypes.SelectedValue;
                }
                if (cmbManufacturer.SelectedIndex > 0)
                {
                    Manufacturer_ID = (int)cmbManufacturer.SelectedValue;
                }
                Machine_Status = "";

                for (int i = 0; i <= (lstMachineAvailability.Items.Count - 1); i++)
                {
                    if (lstMachineAvailability.GetItemChecked(i))
                    {
                        MachineAvailability mc_Avail = lstMachineAvailability.Items[i] as MachineAvailability;
                        Machine_Status += mc_Avail.StockValue + ",";
                    }
                }
                if (Machine_Status.Length > 0)
                {
                    Machine_Status += (int)StockStatus.STOCK_SOLD + ",";
                }
                Machine_Status = Machine_Status.Remove(Machine_Status.Length - 1, 1);


                if (cmbSupplier.SelectedIndex > 0)
                {
                    Operator_ID = (int)cmbSupplier.SelectedValue;
                }
                if (cmbDepot.SelectedIndex > 0)
                {
                    Depot_ID = (int)cmbDepot.SelectedValue;
                }
                if (cmbRepresentative.SelectedIndex > 0)
                {
                    RepresentativeID = (int)cmbRepresentative.SelectedValue;
                }
                if (cmbModelType.SelectedIndex > 0)
                {
                    ModelTypeID = (int)cmbModelType.SelectedValue;
                }
                if (cmbGameCategory.SelectedIndex > 0)
                {
                    GameCategory_ID = (int)cmbGameCategory.SelectedValue;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
        /// <summary>
        /// Get Node Type MC-Machine class,MT-Machine Type...
        /// </summary>
        /// <param name="theKey"></param>
        /// <returns></returns>
        private string GetNodeType(string theKey)
        {
            int theComma = theKey.IndexOf(",", 0);
            string theResult;
            if (theComma >= 1)
            {
                theResult = theKey.Substring(0, theComma);
            }
            else
            {
                theResult = "";
            }
            return theResult;
        }

        private int GetNodeID(string theKey)
        {

            int retVal;
            string theResult = theKey.Substring(theKey.IndexOf(",") + 1);
            int theHash = theResult.IndexOf("#") + 1;
            if (theHash != 0)
            {
                theResult = theResult.Substring(0, theHash - 1);
            }
            int.TryParse(theResult, out retVal);
            return retVal;
        }

        /// <summary>
        /// enable or disable context menu
        /// </summary>
        /// <param name="type"></param>
        /// <param name="IsNGA"></param>
        /// <param name="Nodetext"></param>
        private void PopUpMenu(string type, bool IsNGA, string Nodetext)
        {
            try
            {
                IDM_ADD_MODEL_NGA.Visible = (IsNGA && type == "MT");
                IDM_BUY_MACHINE.Visible = ((!IsNGA && type == "MT") || (IsNGA && type == "MC"));
                IDM_BUY_MACHINE.Enabled = AppGlobals.Current.HasUserAccess("HQ_Stock_BuyMachine");
                IDM_MODEL_ADMIN.Visible = (!IsNGA && type == "MT");
                IDM_EDIT_MACHINE.Visible = (type == "MA" && (Nodetext.ToUpper().IndexOf("TERMINATED") < 0));
                IDM_TERMINATE_MACHINE.Visible = (type == "MA" && (Nodetext.ToUpper().IndexOf("IN STOCK") != -1));
                IDM_VIEW_TERMINATE_MACHINE.Visible = (type == "MA" && (Nodetext.ToUpper().IndexOf("TERMINAT") != -1));
                IDM_ADD_TYPE.Visible = (type == "MT");
                IDM_EDIT_TYPE.Visible = (type == "MT");
                IDM_EDIT_MODEL_NGA.Visible = (IsNGA && type == "MC");
                IDM_BUY_MACHINE.Enabled = AppGlobals.Current.HasUserAccess("HQ_Stock_BuyMachine");
                IDM_EDIT_MACHINE.Enabled = AppGlobals.Current.HasUserAccess("HQ_Stock_Edit");
                IDM_TERMINATE_MACHINE.Enabled = AppGlobals.Current.HasUserAccess("HQ_Stock_TerminateMachine");
                IDM_VIEW_TERMINATE_MACHINE.Enabled = IDM_TERMINATE_MACHINE.Enabled;
                IDM_SAVE_TEMPLATE.Visible = (Nodetext.ToUpper().IndexOf("TERMINATED") < 0);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        /// <summary>
        /// After Machine sold this method is called from BuyMachine used to remove M|C from treeview
        /// </summary>
        /// <param name="MachineID"></param>
        private void RemoveMachineFromDisplay(int MachineID)
        {
            try
            {
                TreeNode[] tn_Sold = tvStock.Nodes.Find("MA," + MachineID, true);
                if (tn_Sold != null && tn_Sold.Length > 0)
                {
                    tvStock.Nodes.Remove(tn_Sold[0]);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }



        /// <summary>
        /// Set display text by using machine_status_flag
        /// </summary>
        /// <param name="MC_StatusFlag"></param>
        /// <returns></returns>
        private string GetMCStatusDisplayText(int? MC_StatusFlag)
        {
            string displaytext = "";
            switch (MC_StatusFlag)
            {
                case (int)StockStatus.STOCK_IN_STOCK:
                    displaytext = "In Stock";
                    break;
                case (int)StockStatus.STOCK_IN_USE:
                    displaytext = "In Use";
                    break;
                case (int)StockStatus.STOCK_TERMINATED:
                    displaytext = "Terminated";
                    break;
            }
            return displaytext;
        }

        /// <summary>
        /// Set node color by using machine_status_flag
        /// </summary>
        /// <param name="MC_StatusFlag"></param>
        /// <param name="myNode"></param>
        private void SetNodeColor(int? MC_StatusFlag, TreeNode myNode)
        {
            switch (MC_StatusFlag)
            {
                case (int)StockStatus.STOCK_IN_USE:

                    myNode.ForeColor = Color.FromArgb(255, 0, 0);
                    break;

                case (int)StockStatus.STOCK_TERMINATED:
                    myNode.ForeColor = Color.FromArgb(255, 0, 255);
                    break;
            }
        }

        /// <summary>
        /// Add M/C class and M/C type for gaming and non-gaming asset
        /// </summary>
        /// <param name="MD"></param>
        /// <param name="M_TypeNode"></param>
        /// <param name="myNode"></param>
        /// <param name="MC_TypeNode"></param>
        /// <param name="displaytext"></param>
        private void AddMachineClassNode(GetMachineDetailsResult MD, TreeNode M_TypeNode, ref TreeNode myNode, TreeNode MC_TypeNode, string displaytext)
        {
            try
            {
                if (MD.IsNonGamingAssetType == 1)
                {

                    myNode = new TreeNode
                  {
                      Name = ("MA," + MD.Machine_ID),
                      Text = (MD.Machine_Stock_No + "   Serial:" + MD.Machine_Manufacturers_Serial_No + "    [" + displaytext + "] Manufacturer:" + MD.Manufacturer_Name + "  Depot: " + MD.Operator_Name + " - " + MD.Depot_Name)
                  };
                    MC_TypeNode.Nodes.Add(myNode);


                }
                else
                {
                    // AGS CHANGE
                    string AGSSerial = "";
                    double DerSerialNo;
                    double DerAssetNo;
                    double DerGMUNo;
                    if (MD.Machine_Manufacturers_Serial_No != null && MD.Machine_Manufacturers_Serial_No.Length > 13)
                    {
                        AGSSerial = "";
                        DerAssetNo = double.Parse(MD.Machine_Manufacturers_Serial_No.Substring(1, 8));
                        DerGMUNo = double.Parse(MD.Machine_Manufacturers_Serial_No.Substring(9, 5));
                        DerSerialNo = double.Parse(MD.Machine_Manufacturers_Serial_No.Substring(14, ((MD.Machine_Manufacturers_Serial_No.Length - 1) - 13)));
                        if (DerAssetNo > 0)
                        {
                            AGSSerial = DerAssetNo.ToString();
                        }
                        if (DerGMUNo > 0)
                        {
                            AGSSerial = AGSSerial + DerGMUNo.ToString();
                        }
                        if (DerSerialNo > 0)
                        {
                            AGSSerial = AGSSerial + DerSerialNo;
                        }
                    }
                    else
                    {
                        AGSSerial = MD.Machine_Manufacturers_Serial_No;
                    }
                    myNode = new TreeNode
                     {
                         Name = ("MA," + MD.Machine_ID),
                         Text = (MD.Machine_Stock_No + "   Serial:"
                                   + AGSSerial + "    [" + displaytext + "] Alt Serial:" + MD.Machine_Alternative_Serial_Numbers
                                   + "  Manufacturer:" + MD.Manufacturer_Name + "  Depot: " + MD.Operator_Name + " - " + MD.Depot_Name),
                         Tag = MD.Machine_Stock_No
                     };
                    M_TypeNode.Nodes.Add(myNode);

                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

        }
        #endregion

        #region TreeViewLoadMethods
        /// <summary>
        /// get Asset details
        /// </summary>
        /// <param name="Machine_Type_ID"></param>
        /// <param name="Operator_ID"></param>
        /// <param name="Depot_ID"></param>
        /// <param name="Manufacturer_ID"></param>
        /// <param name="Game_Category_ID"></param>
        /// <param name="ModelTypeID"></param>
        /// <param name="Machine_Status"></param>
        /// <param name="orderBy"></param>
        /// <param name="searchCriteria"></param>
        /// <returns></returns>

        /// <summary>
        /// Group by Depot
        /// </summary>
        /// <param name="lst_MDetails"></param>
        private void LoadDepot(List<GetMachineDetailsResult> lst_MDetails)
        {
            string displaytext = "";
            TreeNode tView = null;
            TreeNode myNode = new TreeNode();
            TreeNode M_TypeNode = new TreeNode();
            TreeNode MC_TypeNode = new TreeNode();
            try
            {
                lst_MDetails = lst_MDetails.FindAll(obj => obj.Operator_ID != null);
                LogManager.WriteLog(ScreenName + "Load machine details by depot wise", LogManager.enumLogLevel.Debug);
                TreeNode Depot_Node = new TreeNode();
                foreach (GetMachineDetailsResult MD in lst_MDetails)
                {

                    if (oldSupplier != MD.Operator_ID)
                    {

                        displaytext = (MD.Operator_Name == string.Empty) ? strUnAllocate : MD.Operator_Name;

                        tView = new TreeNode { Name = ("OP," + MD.Operator_ID), Text = displaytext };
                        tvStock.Nodes.Add(tView);
                        oldSupplier = MD.Operator_ID;
                        oldDepot = -1;
                    }
                    if (oldDepot != MD.Depot_ID)
                    {
                        displaytext = (MD.Depot_Name == string.Empty) ? strUnAllocate : MD.Depot_Name;
                        Depot_Node = new TreeNode { Name = "DE," + MD.Depot_ID + "#" + MD.Operator_ID, Text = displaytext };
                        tView.Nodes.Add(Depot_Node);
                        oldDepot = MD.Depot_ID;
                        oldType = -1;
                    }
                    if (oldType != MD.Machine_Type_ID && MD.Machine_Status_Flag != (int)StockStatus.STOCK_SOLD)
                    {

                        M_TypeNode = new TreeNode { Name = ("MT," + MD.Machine_Type_ID + "#" + MD.Operator_ID + "," + MD.Depot_ID), Text = MD.Machine_Type_Code };
                        Depot_Node.Nodes.Add(M_TypeNode);
                        oldType = MD.Machine_Type_ID;
                        oldClass = -1;
                    }
                    if ((oldClass != MD.Machine_Class_ID))
                    {

                        // NGA Changes
                        if (MD.IsNonGamingAssetType == 1)
                        {
                            MC_TypeNode = new TreeNode { Name = ("MC," + MD.Machine_Class_ID + "#" + MD.Operator_ID + "," + MD.Depot_ID), Text = (MD.Machine_Name + " [BC:" + MD.Machine_BACTA_Code + " - MC:" + MD.Machine_Class_Model_Code + "]      " + MD.Manufacturer_Name + "    [NGA]") };
                            M_TypeNode.Nodes.Add(MC_TypeNode);
                        }
                        else
                        {
                            // Commented for multigame changes
                            // Call tvStock.Nodes.Add("MT," & MyStock!Machine_Type_ID, tvwChild, "MC," & MyStock!Machine_class_id, MyStock!Machine_Name & " [BC:" & MyStock!Machine_BACTA_Code & " - MC:" & MyStock!Machine_Class_Model_Code & "]      " & MyStock!Manufacturer_Name)
                        }
                        oldClass = MD.Machine_Class_ID;

                    }
                    //Get display text i.e.Stock In Use
                    displaytext = GetMCStatusDisplayText(MD.Machine_Status_Flag);
                    // If MyStock!Machine_End_Date & "" = "" Then
                    if ((MD.Machine_Class_ID != null && MD.Machine_ID != 0) || (MD.Machine_ID == 0 && MD.Machine_Status_Flag == (int)StockStatus.STOCK_TERMINATED))
                    {
                        // NGA Changes
                        if (MD.Machine_Status_Flag != (int)StockStatus.STOCK_SOLD)
                        {
                            //Add Machine Class and AGS Serial
                            AddMachineClassNode(MD, M_TypeNode, ref myNode, MC_TypeNode, displaytext);
                        }
                        //set node color if stock is in use nodecolor is red
                        SetNodeColor(MD.Machine_Status_Flag, myNode);

                        //to display barposition no and installation details
                        DisplayInstallationDetails(MD, myNode);

                        // NGA Changes
                        if (MD.IsNonGamingAssetType == 1)
                        {
                            myNode.Text = myNode.Text + "    [NGA]";
                        }
                        else
                        {
                            //To Display paytable details
                            if (MD.PaytableFlag == 1)
                            {
                                if (chkDisplayInstallationDetails.Checked)
                                    myNode.Nodes.Add(new TreeNode { Name = "PD", Tag = MD.Machine_ID, Text = displaytext });
                                //GetPayTableDetails(MD, myNode, ref displaytext, ref PayTableID, false);
                            }
                        }

                    }
                    PayTableID = 0;

                }

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        /// <summary>
        /// Group by Stock (ie.InUSE)
        /// </summary>
        /// <param name="lst_MDetails"></param>
        private void LoadAvailability(List<GetMachineDetailsResult> lst_MDetails)
        {
            string displaytext = "";
            TreeNode tView = null;
            TreeNode myNode = new TreeNode();
            TreeNode M_TypeNode = new TreeNode();
            TreeNode MC_TypeNode = new TreeNode();
            try
            {
                lst_MDetails = lst_MDetails.FindAll(obj => obj.Operator_ID != null);
                LogManager.WriteLog(ScreenName + @"Load machine details by M\C Availability wise", LogManager.enumLogLevel.Debug);
                foreach (GetMachineDetailsResult MD in lst_MDetails)
                {
                    // Added Condition CR91132 Fix
                    if (MD.Machine_Status_Flag != null && MD.Machine_Stock_No != "")
                    {
                        if ((oldStatus != MD.Machine_Status_Flag))
                        {
                            if (MD.Machine_Status_Flag == (int)StockStatus.STOCK_SOLD)
                                continue;
                            //Get display text i.e.Stock In Use
                            displaytext = GetMCStatusDisplayText(MD.Machine_Status_Flag);

                            if (tView != null)
                            {
                                tvStock.Nodes.Add(tView);
                            }
                            tView = new TreeNode
                            {
                                Name = "MS," + MD.Machine_Status_Flag,
                                Text = displaytext

                            };

                            oldStatus = MD.Machine_Status_Flag;
                            oldType = -1;
                        }

                    }
                    if (oldType != MD.Machine_Type_ID && MD.Machine_Type_ID.ToString() != string.Empty)
                    {
                        M_TypeNode = new TreeNode
                            {
                                Name = "MT," + MD.Machine_Type_ID + "#" + MD.Machine_Status_Flag,
                                Text = MD.Machine_Type_Code
                            };
                        tView.Nodes.Add(M_TypeNode);
                        oldType = MD.Machine_Type_ID;
                        oldClass = -1;
                    }
                    if (oldClass != MD.Machine_Class_ID && MD.Machine_Class_ID.ToString() != string.Empty)
                    {
                        // NGA Changes
                        if (MD.IsNonGamingAssetType == 1)
                        {

                            MC_TypeNode = new TreeNode
                              {
                                  Name = "MC," + MD.Machine_Class_ID + "#" + MD.Machine_Status_Flag,
                                  Text = MD.Machine_Name + " [BC:" + MD.Machine_BACTA_Code + " - MC:" + MD.Machine_Class_Model_Code + "]      " + MD.Manufacturer_Name + "    [NGA]"
                              };
                            M_TypeNode.Nodes.Add(MC_TypeNode);
                        }
                        else
                        {
                            // Commented for multigame changes
                            // Call tvStock.Nodes.Add("MT," & MyStock!Machine_Type_ID, tvwChild, "MC," & MyStock!Machine_class_id, MyStock!Machine_Name & " [BC:" & MyStock!Machine_BACTA_Code & " - MC:" & MyStock!Machine_Class_Model_Code & "]      " & MyStock!Manufacturer_Name)
                        }
                        oldClass = MD.Machine_Class_ID;
                    }

                    if (MD.Machine_ID.ToString() != string.Empty && MD.Machine_Manufacturers_Serial_No != null)
                    {
                        if (MD.Machine_Status_Flag != (int)StockStatus.STOCK_SOLD)
                        {
                            //Add Machine Class and AGS Serial
                            AddMachineClassNode(MD, M_TypeNode, ref myNode, MC_TypeNode, displaytext);
                        }
                        //set node color if stock is in use nodecolor is red
                        SetNodeColor(MD.Machine_Status_Flag, myNode);

                        //to display barposition no and installation details
                        DisplayInstallationDetails(MD, myNode);
                        // NGA Changes
                        if (MD.IsNonGamingAssetType == 1)
                        {
                            myNode.Text = myNode.Text + "    [NGA]";
                        }
                        else
                        {

                            if (MD.PaytableFlag == 1)
                            {
                                //To Display paytable details
                                if (chkDisplayInstallationDetails.Checked)
                                    myNode.Nodes.Add(new TreeNode { Name = "PD", Tag = MD.Machine_ID, Text = displaytext });
                                //GetPayTableDetails(MD, myNode, ref displaytext, ref PayTableID, true);
                            }
                        }

                    }

                    PayTableID = 0;

                }
                tvStock.Nodes.Add(tView);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        /// <summary>
        /// Group by MachineType(i.e gaming or non-gaming)
        /// </summary>
        /// <param name="lst_MDetails"></param>
        private void LoadMachineType(List<GetMachineDetailsResult> lst_MDetails)
        {
            string displaytext = "";
            TreeNode tView = null;
            TreeNode myNode = new TreeNode();
            TreeNode M_TypeNode = new TreeNode();
            TreeNode MC_TypeNode = new TreeNode();
            try
            {
                LogManager.WriteLog(ScreenName + @"Load machine details by M\C Type wise", LogManager.enumLogLevel.Debug);
                foreach (GetMachineDetailsResult MD in lst_MDetails)
                {
                    // is machine from correct depot?

                    if (oldType != MD.Machine_Type_ID)
                    {
                        if (tView != null)
                        {
                            tvStock.Nodes.Add(tView);
                        }
                        // NGA Changes

                        M_TypeNode = new TreeNode
                        {
                            Name = "MT," + MD.Machine_Type_ID,
                            Text = MD.Machine_Type_Code + ((MD.IsNonGamingAssetType == 1) ? "    [NGA]" : "")
                        };

                        tView = M_TypeNode;
                        oldType = MD.Machine_Type_ID;
                        oldClass = -1;
                    }

                    if ((MD.Machine_Class_ID > 0) || (MD.Machine_ID == 0 && MD.Machine_Status_Flag == (int)StockStatus.STOCK_TERMINATED))
                    {

                        if (oldClass != MD.Machine_Class_ID)
                        {

                            // NGA Changes
                            if (MD.IsNonGamingAssetType == 1)
                            {
                                MC_TypeNode = new TreeNode
                            {
                                Name = "MC," + MD.Machine_Class_ID,
                                Text = MD.Machine_Name + " [BC:" + MD.Machine_BACTA_Code + " - MC:" + MD.Machine_Class_Model_Code + "]      " + MD.Manufacturer_Name + "    [NGA]"

                            };
                                M_TypeNode.Nodes.Add(MC_TypeNode);
                            }
                            else
                            {
                                // Commented for multigame changes
                                // Call tvStock.Nodes.Add("MT," & MyStock!Machine_Type_ID, tvwChild, "MC," & MyStock!Machine_class_id, MyStock!Machine_Name & " [BC:" & MyStock!Machine_BACTA_Code & " - MC:" & MyStock!Machine_Class_Model_Code & "]      " & MyStock!Manufacturer_Name)
                            }
                            oldClass = MD.Machine_Class_ID;
                        }
                        //Get display text i.e.Stock In Use
                        displaytext = GetMCStatusDisplayText(MD.Machine_Status_Flag);


                        if ((MD.Machine_ID > 0) || (MD.Machine_ID == 0 && MD.Machine_Status_Flag == (int)StockStatus.STOCK_TERMINATED))
                        {

                            //Add Machine Class and AGS Serial
                            if (MD.Machine_Status_Flag != (int)StockStatus.STOCK_SOLD)
                            {
                                AddMachineClassNode(MD, M_TypeNode, ref myNode, MC_TypeNode, displaytext);
                            }

                            //set node color if stock is in use nodecolor is red
                            SetNodeColor(MD.Machine_Status_Flag, myNode);

                            //to display barposition no and installation details
                            DisplayInstallationDetails(MD, myNode);

                            if (MD.IsNonGamingAssetType == 1)
                            {
                                myNode.Text = myNode.Text + "    [NGA]";
                            }
                            else
                            {
                                //To Display paytable details
                                if (MD.PaytableFlag == 1)
                                {
                                    if (chkDisplayInstallationDetails.Checked)
                                        myNode.Nodes.Add(new TreeNode { Name = "PD", Tag = MD.Machine_ID, Text = displaytext });
                                    // GetPayTableDetails(MD, myNode, ref displaytext, ref PayTableID, false);
                                }
                            }

                        }
                    }
                    PayTableID = 0;
                }
                if (tView != null)
                    tvStock.Nodes.Add(tView);
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        /// <summary>
        ///  Group by Game Category
        /// </summary>
        /// <param name="lst_MDetails"></param>
        private void LoadGameCategory(List<GetMachineDetailsResult> lst_MDetails)
        {
            string displaytext = "";
            TreeNode tView = null;
            TreeNode myNode = new TreeNode();
            TreeNode M_TypeNode = new TreeNode();
            TreeNode MC_TypeNode = new TreeNode();
            int Game_id = 0;
            try
            {
                oldType = -1;
                lst_MDetails = lst_MDetails.FindAll(obj => obj.Operator_ID != null);
                LogManager.WriteLog(ScreenName + @"Load machine details by game category wise", LogManager.enumLogLevel.Debug);
                List<FetchGameCategoryResult> lst_GameCategory = AssetManagementBiz.CreateInstance().FetchGameCategoryResult(0);
                if (!isFirst)
                {
                    tvStock.Nodes.Add(new TreeNode
                    {
                        Name = "CAT," + "0",
                        Text = displaytext + "[Unassigned]"
                    });
                    isFirst = true;
                }
                foreach (FetchGameCategoryResult GC in lst_GameCategory)
                {
                    Game_id = GC.Game_Category_ID;
                    if (Game_id != 0)
                    {
                        if (oldType != Game_id)
                        {
                            tView = new TreeNode
                            {
                                Name = ("CAT," + Game_id),
                                Text = GC.Game_Category_Name
                            };
                            oldType = GC.Game_Category_ID;
                            oldClass = -1;
                        }
                    }
                }

                oldType = -1;
                foreach (GetMachineDetailsResult MD in lst_MDetails)
                {

                    Game_id = MD.Game_Category_ID ?? 0;

                    if ((MD.Machine_Class_ID > 0) || (MD.Machine_ID == 0 && MD.Machine_Status_Flag == (int)StockStatus.STOCK_TERMINATED))
                    {
                        if (oldType != MD.Machine_Type_ID || oldGame != Game_id)
                        {
                            // NGA Changes
                            if (MD.IsNonGamingAssetType == 1)
                            {
                                // Call tvStock.Nodes.Add("CAT," & Game_id, tvwChild, "MT," & MyStock!Machine_Type_ID, MyStock!Machine_Type_Code & "" & "    [NGA]")
                                oldClass = -1;
                            }
                            else
                            {
                                // Commented for multigame changes
                                // Call tvStock.Nodes.Add(, , "MT," & MyStock!Machine_Type_ID, MyStock!Machine_Type_Code & "")
                                M_TypeNode = new TreeNode
                                {
                                    Name = "MT," + MD.Machine_Type_ID + Game_id,
                                    Text = MD.Machine_Type_Code
                                };
                                tView.Nodes.Add(M_TypeNode);
                            }
                            oldType = MD.Machine_Type_ID;
                            oldGame = Game_id;
                            oldClass = -1;
                        }
                        //Get display text i.e.Stock In Use
                        displaytext = GetMCStatusDisplayText(MD.Machine_Status_Flag);


                        if ((MD.Machine_ID > 0) || (MD.Machine_ID == 0 && MD.Machine_Status_Flag == (int)StockStatus.STOCK_TERMINATED))
                        {
                            if (MD.Machine_Status_Flag != (int)StockStatus.STOCK_SOLD)
                            {
                                //Add Machine Class and AGS Serial
                                AddMachineClassNode(MD, M_TypeNode, ref myNode, MC_TypeNode, displaytext);
                            }
                            //set node color if stock is in use nodecolor is red
                            SetNodeColor(MD.Machine_Status_Flag, myNode);

                            //to display barposition no and installation details
                            DisplayInstallationDetails(MD, myNode);

                            if (MD.IsNonGamingAssetType == 1)
                            {
                                myNode.Text = myNode.Text + "    [NGA]";
                            }
                            else
                            {
                                //To Display paytable details
                                if (MD.PaytableFlag == 1)
                                {
                                    if (chkDisplayInstallationDetails.Checked)
                                        myNode.Nodes.Add(new TreeNode { Name = "PD", Tag = MD.Machine_ID, Text = displaytext });
                                    //  GetPayTableDetails(MD, myNode, ref displaytext, ref PayTableID, false);
                                }

                            }
                        }
                    }
                }
                tvStock.Nodes.Add(tView);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        /// <summary>
        ///  Group by Staff
        /// </summary>
        /// <param name="lst_MDetails"></param>
        private void LoadRepresentative(List<GetMachineDetailsResult> lst_MDetails)
        {
            string displaytext = "";
            oldStaff = -1;
            TreeNode tView = null;
            TreeNode myNode = new TreeNode();
            TreeNode M_TypeNode = new TreeNode();
            TreeNode MC_TypeNode = new TreeNode();
            try
            {
                lst_MDetails = lst_MDetails.FindAll(obj => obj.Operator_ID != null);
                LogManager.WriteLog(ScreenName + @"Load machine details by staff wise", LogManager.enumLogLevel.Debug);
                foreach (GetMachineDetailsResult MD in lst_MDetails)
                {

                    if (oldStaff != MD.Staff_ID)
                    {
                        if (tView != null)
                        {
                            tvStock.Nodes.Add(tView);
                        }
                        if (MD.Staff_ID == 0)
                        {
                            tView = new TreeNode
                            {
                                Name = ("ST," + MD.Staff_ID),
                                Text = "Unallocated"
                            };
                        }
                        else
                        {

                            tView = new TreeNode
                            {
                                Name = "ST," + MD.Staff_ID,
                                Text = MD.Staff_Last_Name + "," + MD.Staff_First_Name
                            };
                        }
                        oldStaff = MD.Staff_ID;
                        oldType = -1;
                    }
                    if (oldType != MD.Machine_Type_ID && MD.Machine_Status_Flag != (int)StockStatus.STOCK_SOLD)
                    {

                        M_TypeNode = new TreeNode
                        {
                            Name = "MT," + MD.Machine_Type_ID + "#" + oldStaff,
                            Text = MD.Machine_Type_Code
                        };
                        tView.Nodes.Add(M_TypeNode);
                        oldType = MD.Machine_Type_ID;
                        oldClass = -1;
                    }
                    if ((MD.Machine_Class_ID > 0) || (MD.Machine_ID == 0 && MD.Machine_Status_Flag == (int)StockStatus.STOCK_TERMINATED))
                    {
                        if (oldClass != MD.Machine_Class_ID)
                        {
                            // NGA Changes
                            if (MD.IsNonGamingAssetType == 1)
                            {

                                MC_TypeNode = new TreeNode
                                {
                                    Name = "MC," + MD.Machine_Class_ID + "#" + oldStaff,
                                    Text = MD.Machine_Name + " [BC:" + MD.Machine_BACTA_Code + " - MC:" + MD.Machine_Class_Model_Code + "]      " +
                                    MD.Manufacturer_Name + "    [NGA]"
                                };
                                M_TypeNode.Nodes.Add(MC_TypeNode);
                            }
                            else
                            {
                                // Commented for multigame changes
                                // Call tvStock.Nodes.Add("MT," & MyStock!Machine_Type_ID, tvwChild, "MC," & MyStock!Machine_class_id, MyStock!Machine_Name & " [BC:" & MyStock!Machine_BACTA_Code & " - MC:" & MyStock!Machine_Class_Model_Code & "]      " & MyStock!Manufacturer_Name)
                            }
                            oldClass = MD.Machine_Class_ID;
                        }
                        //Get display text i.e.Stock In Use
                        displaytext = GetMCStatusDisplayText(MD.Machine_Status_Flag);

                        if ((MD.Machine_ID > 0) || (MD.Machine_ID == 0 && MD.Machine_Status_Flag == (int)StockStatus.STOCK_TERMINATED))
                        {
                            if (MD.Machine_Status_Flag != (int)StockStatus.STOCK_SOLD)
                            {
                                //Add Machine Class and AGS Serial
                                AddMachineClassNode(MD, M_TypeNode, ref myNode, MC_TypeNode, displaytext);
                            }

                            //set node color if stock is in use nodecolor is red
                            SetNodeColor(MD.Machine_Status_Flag, myNode);

                            //to display barposition no and installation details
                            DisplayInstallationDetails(MD, myNode);

                            if (MD.IsNonGamingAssetType == 1)
                            {
                                myNode.Text = myNode.Text + "    [NGA]";
                            }
                            else
                            {
                                //To Display paytable details
                                if (MD.PaytableFlag == 1)
                                {
                                    if (chkDisplayInstallationDetails.Checked)
                                        myNode.Nodes.Add(new TreeNode { Name = "PD", Tag = MD.Machine_ID, Text = displaytext });
                                    //GetPayTableDetails(MD, myNode, ref displaytext, ref PayTableID, false);
                                }
                            }
                        }

                    }
                }
                tvStock.Nodes.Add(tView);

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        #endregion

        #region DBMethods

        private List<GetMachineDetailsResult> LoadMachineDetails(int Machine_Type_ID, int Operator_ID, int Depot_ID, int Staff_ID, int Manufacturer_ID, int Game_Category_ID, int ModelTypeID, string Machine_Status, string orderBy, string searchCriteria, int searchgame, string _GameName)
        {
            List<GetMachineDetailsResult> lstUserLang = AssetManagementBiz.CreateInstance().GetMachineDetails(Machine_Type_ID, Operator_ID, Depot_ID, Staff_ID, Manufacturer_ID, Game_Category_ID, ModelTypeID, Machine_Status, orderBy, searchCriteria, searchgame, _GameName);
            return lstUserLang;
        }

        private void LoadMachineType()
        {
            try
            {
                lst_MCType = BuyMachineBiz.CreateInstance().GetMachineTypeDetails(null);
                if (lst_MCType != null)
                {
                    lst_MCType.Insert(0, new GetMachineTypeDetailsResult { Machine_Type_ID = -1, MCTypeDescription_NGA = strAny });
                }
                cmbMachineTypes.SelectedIndex = -1;
                cmbMachineTypes.DisplayMember = "MCTypeDescription_NGA";
                cmbMachineTypes.ValueMember = "Machine_Type_ID";
                cmbMachineTypes.DataSource = lst_MCType;


            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);

            }
        }
        /// <summary>
        /// Manufacturer Details
        /// </summary>
        private void LoadManufacturer()
        {
            try
            {

                cmbManufacturer.DisplayMember = "Manufacturer_Name";
                cmbManufacturer.ValueMember = "Manufacturer_ID";
                LogManager.WriteLog(ScreenName + "Load Manufacturer details", LogManager.enumLogLevel.Info);
                List<Manufacturer> lst_manf = BuyMachineBiz.CreateInstance().GetManufacturers();
                Manufacturer m_negative = lst_manf.Find(o => o.Manufacturer_ID == -1);
                if (m_negative != null)
                {
                    m_negative.Manufacturer_Name = strAny;
                }
                cmbManufacturer.DataSource = lst_manf;

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void LoadModelType()
        {
            try
            {
                LogManager.WriteLog(ScreenName + "Load Model Type details", LogManager.enumLogLevel.Info);
                List<GetModelTypeResult> lst_ModelType = AssetManagementBiz.CreateInstance().GetModelTypeDetails(null, null);
               foreach (var item in lst_ModelType)
			   {
        			if (item.MT_IsNGA)
            			item.MT_Model_Name = item.MT_Model_Name + "(Non Gaming)";
        			else
  				        item.MT_Model_Name = item.MT_Model_Name + "(Gaming)";
				}
               
                cmbModelType.DisplayMember = "MT_Model_Name";
                cmbModelType.ValueMember = "MT_ID";
                if (lst_ModelType != null)
                {
                    lst_ModelType.Insert(0, new GetModelTypeResult { MT_ID = -1, MT_Model_Name = strAny });
                }
                cmbModelType.DataSource = lst_ModelType;
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void LoadGameCategory()
        {
            try
            {
                LogManager.WriteLog(ScreenName + "Load game category details", LogManager.enumLogLevel.Info);
                cmbGameCategory.ValueMember = "Game_Category_ID";
                cmbGameCategory.DisplayMember = "Game_Category_Name";
                
               string defaultString = this.GetResourceTextByKey("Key_PleaSelect");
               List<GameCategory> lstCategory = GameLibraryBiz.CreateInstance().GetGameCategory(defaultString,defaultString);

                if (lstCategory != null)
                {
                    lstCategory.Insert(0, new GameCategory { Game_Category_ID = -1, Game_Category_Name = strAny });
                }
                cmbGameCategory.DataSource = lstCategory;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        /// <summary>
        /// Load Operator
        /// </summary>
        private void LoadSupplier()
        {
            try
            {
                LogManager.WriteLog(ScreenName + "Load operator details", LogManager.enumLogLevel.Info);
                List<OperatorEntity> lstOp = UserAdministrationBiz.CreateInstance().GetOperatorDetails();
                cmbSupplier.DisplayMember = "Operator_Name";
                cmbSupplier.ValueMember = "Operator_ID";
                cmbSupplier.DataSource = lstOp;

                if (lstOp.Count > 0)
                {
                    cmbSupplier.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

        }

        private void LoadDepot(int Operator_ID)
        {
            try
            {
                LogManager.WriteLog(ScreenName + "Load depot details", LogManager.enumLogLevel.Info);
                List<DepotEntity> lstDepot = UserAdministrationBiz.CreateInstance().GetDepotDetails(Operator_ID);
                cmbDepot.DisplayMember = "Depot_Name";
                cmbDepot.ValueMember = "Depot_ID";
                cmbDepot.DataSource = lstDepot;

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void LoadStaffRepresentative()
        {
            try
            {
                LogManager.WriteLog(ScreenName + "Load staff representative details", LogManager.enumLogLevel.Info);
                List<StaffNameResult> lstStaff = UserAdministrationBiz.CreateInstance().GetStaffName(null, true, false);


                cmbRepresentative.DisplayMember = "Staff_Representative_Name";
                cmbRepresentative.ValueMember = "Staff_ID";
                if (lstStaff != null)
                {
                    lstStaff.Insert(0, new StaffNameResult { Staff_ID = -1, Staff_Representative_Name = strAny });
                }
                cmbRepresentative.DataSource = lstStaff;

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        /// <summary>
        /// To display Installation details(i.e Bar Position) 
        /// </summary>
        /// <param name="MD"></param>
        /// <param name="myNode"></param>
        private void DisplayInstallationDetails(GetMachineDetailsResult MD, TreeNode myNode)
        {
            try
            {
                if (chkDisplayInstallationDetails.Checked)
                {
                    // NGA Changes
                    if (MD.IsNonGamingAssetType == 1 && MD.Machine_Status_Flag == 1)
                    {

                        myNode.Text = (myNode.Text + "    [" + MD.Site_Name + " (" + MD.Site_Code + ") ] Rep:"
                                        + MD.Staff_Last_Name + "," + MD.Staff_First_Name);
                        //}
                    }
                    else if (MD.Bar_Position_Name != null && MD.Machine_Status_Flag == 1)
                    {

                        myNode.Text = (myNode.Text + "    [" + MD.Site_Name + " (" + MD.Site_Code + ") Pos:" + MD.Bar_Position_Name + "] Rep:"
                                      + MD.Staff_Last_Name + "," + MD.Staff_First_Name);

                    }
                    else if (MD.IsNonGamingAssetType == 0)
                    {

                        myNode.Text = (myNode.Text + " [Not installed] Rep:" + MD.Staff_Last_Name + "," + MD.Staff_First_Name);
                    }


                }
                else
                {
                    myNode.Text = (myNode.Text + "  Rep: " + MD.Staff_Last_Name + "," + MD.Staff_First_Name);
                }
                if (!String.IsNullOrEmpty(MD.Machine_Category_Code))
                {
                    myNode.Text = myNode.Text + "  (" + MD.Machine_Category_Code + ")";
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

        }

        /// <summary>
        /// To Get Paytable details using MachineID and to root tree
        /// </summary>
        /// <param name="MD"></param>
        /// <param name="myNode"></param>
        /// <param name="displaytext"></param>
        /// <param name="PayTableID"></param>
        private void GetPayTableDetails(int Machine_ID, TreeNode myNode, string displaytext, int PayTableID, bool IsAvailabilty)
        {
            try
            {
                string oldDisplaytext = displaytext;
                List<GetPaytableDetailsForGameResult> lst_PaytableDetails = AssetManagementBiz.CreateInstance().GetPaytableDetailsForGameResult(Machine_ID);
                string GameAliasName = "";
                string strManufacturer = "";
                string disp = "";
                TreeNode MG_Node = new TreeNode();
                foreach (GetPaytableDetailsForGameResult GPaytable in lst_PaytableDetails)
                {
                    if (GameAliasName != GPaytable.AliasGameName || (strManufacturer != GPaytable.Manufacturer_Name))
                    {
                        disp = GPaytable.AliasGameName + "   ,   " + GPaytable.Manufacturer_Name;
                        GameAliasName = GPaytable.AliasGameName;
                        strManufacturer = GPaytable.Manufacturer_Name;
                        // GameName = myMachineD!Game_Name
                        MG_Node = new TreeNode
                        {
                            Name = "MG," + Machine_ID + GPaytable.Installation_No + GPaytable.AliasGameName + GPaytable.Manufacturer_Name,
                            Text = disp
                        };
                        myNode.Nodes.Add(MG_Node);
                    }
                    displaytext = (GPaytable.PaytableDescription == null || GPaytable.PaytableDescription == "") ? "[TBD]" : GPaytable.PaytableDescription
                        + "  ,  " + ((GPaytable.Payout == null || GPaytable.Payout.ToString() == "") ? "[N/a]" : GPaytable.Payout.ToString())
                        + "%  ,  " + ((GPaytable.TheoreticalPayout == 0 || GPaytable.TheoreticalPayout.ToString() == "") ? "[TBD]" : GPaytable.TheoreticalPayout.ToString())
                        + "%  ,  " + ((GPaytable.MaxBet == 0 || GPaytable.MaxBet.ToString() == "") ? "[N/a]" : GPaytable.MaxBet + " Credits");
                    PayTableID = GPaytable.PaytableID;


                    MG_Node.Nodes.Add(
                        new TreeNode
                        {
                            Name = ("MG," + GPaytable.Machine_ID + GPaytable.Installation_No + GPaytable.Game_Name + PayTableID),
                            Text = displaytext
                        });
                    if (IsAvailabilty && (displaytext != oldDisplaytext))
                    {
                        displaytext = oldDisplaytext;
                    }

                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

        }

        #endregion

        private void AddTemplateAudit()
        {
            try
            {
                AuditViewerBusiness business = new AuditViewerBusiness(DatabaseHelper.GetConnectionString());
                {
                    business.InsertAuditData(new Audit.Transport.Audit_History
                    {
                        EnterpriseModuleName = ModuleNameEnterprise.AssetTemplate,
                        Audit_Screen_Name = "Template Creation",
                        Audit_Desc = "Template Added-" + "Template Name: " + TemplateName + "",
                        AuditOperationType = OperationType.ADD,
                        Audit_User_ID = AppEntryPoint.Current.UserId,
                        Audit_User_Name = AppEntryPoint.Current.UserName
                    }, false);
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error While Adding Audit Log for ShareHolder Insert: Error Message:" + ex.Message, LogManager.enumLogLevel.Error);
            }
        }

        private void frmAssetManagement_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                Process[] pname = Process.GetProcessesByName("ExportImportAssetDetails");
                if (pname.Length > 0)
                {
                    pname[0].Kill();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F5)
            {
                btnDisplay_Click(this, null);
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void chkDisplayInstallationDetails_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void cmbGroupStockBy_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmbFilterbyGame_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void grpFilter_Enter(object sender, EventArgs e)
        {

        }
    }
}
