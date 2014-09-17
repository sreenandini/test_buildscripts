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
using BMC.Common;

namespace BMC.EnterpriseClient.Views
{
    public partial class frmGameTitle : GenericFormBase
    {

        #region Data Members

        GameLibraryBiz objGameLibraryBiz = new GameLibraryBiz();
        GetGameTitleDetailsByTitleId objGameTitleByTitle;
        private bool _isNew = false;
        private string sOldGameTitle = string.Empty;
        BMC.EnterpriseClient.Helpers.Datawatcher objDatawatcher = null;
        string defaultString = string.Empty;

        #endregion //Data Members

        #region Constructor

        public frmGameTitle()
        {
            InitializeComponent();
            SetPropertyTag();

            PopulateGameCategory();
            PopulateManufacturers();
            _isNew = true;
            objDatawatcher = new Helpers.Datawatcher(this);            
        }

        private void SetPropertyTag()
        {
            try
            {
                this.btnCancel.Tag = "Key_CancelCaption";
                this.lblGameCategory.Tag = "Key_GameCategoryColon";
                this.lblGameManufactrer.Tag = "Key_GameManufacturerColon";
                this.lblGameTitle.Tag = "Key_GameTitleColon";
                this.btnSave.Tag = "Key_SaveCaption";
                this.Tag = "Key_GameTitle";
                this.chkMultiGame.Tag = "Key_MultiGame";
                defaultString = this.GetResourceTextByKey("Key_PleaSelect");
            }
            catch (Exception ex)
            {                
                ExceptionManager.Publish(ex);
            }
           
        }

        public frmGameTitle(int iGameTitleId)
        {
            try
            {
                InitializeComponent();
                SetPropertyTag();

                objGameTitleByTitle = objGameLibraryBiz.GetGameTitleDetailsByTitle(iGameTitleId);
                if (objGameTitleByTitle == null)
                {
                    this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_GAMETITLE_NONAME"), this.Text);                                           
                    return;
                }

                sOldGameTitle = objGameTitleByTitle.Game_Title;
                txtGameTitle.Text = objGameTitleByTitle.Game_Title;
                chkMultiGame.Checked = true; // Convert.ToBoolean(objGameTitleByTitle.IsMultiGame);


                var EnrolledStatus = true;// Convert.ToBoolean(objGameTitleByTitle.EnrolledStatus);
                if (EnrolledStatus)
                {
                    EnableDisableControls(false);
                    this.ShowWarningMessageBox("Selected 'Game Title' is non editable since it is associated with 'In Use' Asset(s).");
                }
                objDatawatcher = new Helpers.Datawatcher(this);

                PopulateGameCategory();
                PopulateManufacturers();

                cmb_GameCategoryFilter.SelectedValue = objGameTitleByTitle.Game_Category_ID;
                cmb_ManufacturerFilter.SelectedValue = objGameTitleByTitle.Manufacturer_ID;
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void EnableDisableControls(bool p)
        {
            chkMultiGame.Enabled = txtGameTitle.Enabled = cmb_GameCategoryFilter.Enabled = cmb_ManufacturerFilter.Enabled = btnSave.Enabled = p;
        }

        #endregion //Constructor

        #region Events

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateControlValues()) return;
                
                if (_isNew)
                {
                    if (objGameLibraryBiz.VerifyGameTitleIsExists(txtGameTitle.Text.Trim()) > 0)
                    {
                        this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_GAMETITLE_NAMEEXISTS"), this.Text);                                                                   
                        return;
                    }

                    if (true)
                    {
                        objGameLibraryBiz.InsertNewAuditEntry(Audit.Transport.ModuleNameEnterprise.AUDIT_GAMELIBRARY, "Game Title Admin", "New Game Title Added", txtGameTitle.Text.Trim());
                        this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_GAMETILE_SUCCESS"), this.Text);                                                                  
                        this.DialogResult = DialogResult.OK;
                        objDatawatcher.DataModify = false;
                        this.Close();
                        return;
                    }
                }

                if (0 == 0)
                {
                    GetGameTitleDetailsByTitleId modifiedGameTitleDetails = new GetGameTitleDetailsByTitleId();
                    modifiedGameTitleDetails.Game_Category_ID = Convert.ToInt32(cmb_GameCategoryFilter.SelectedValue);
                    modifiedGameTitleDetails.Game_Title = txtGameTitle.Text.Trim();
                    modifiedGameTitleDetails.Manufacturer_ID = Convert.ToInt32(cmb_ManufacturerFilter.SelectedValue);
                    objGameLibraryBiz.AuditUpdatedGameTitleDetails(objGameTitleByTitle, modifiedGameTitleDetails);
                    this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_GAMETILE_SUCCESS"), this.Text);                                                               
                    this.DialogResult = DialogResult.OK;
                    objDatawatcher.DataModify = false;
                    this.Close();
                    return;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion //Events

        #region Methods

        private void PopulateGameCategory()
        {
            try
            {
                cmb_GameCategoryFilter.ValueMember = "Game_Category_ID";
                cmb_GameCategoryFilter.DisplayMember = "Game_Category_Name";
                List<GameCategory> lstCategory = objGameLibraryBiz.GetGameCategory(true, defaultString, defaultString);
                
                if (lstCategory != null && lstCategory.Count > 0)
                {
                    cmb_GameCategoryFilter.DataSource = lstCategory.Where(e=> e.Game_Category_ID!=1).ToList();
                    cmb_GameCategoryFilter.SelectedValue = objGameTitleByTitle.Game_Category_ID == 1 ? 0 : objGameTitleByTitle.Game_Category_ID;
                }
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

                List<Manufacturer> lstManufacturer = objGameLibraryBiz.GetManufacturers(true, defaultString, defaultString);
                cmb_ManufacturerFilter.DataSource = lstManufacturer;
                cmb_ManufacturerFilter.SelectedValue = objGameTitleByTitle.Manufacturer_ID;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private bool ValidateControlValues()
        {
            if (cmb_GameCategoryFilter.SelectedIndex <= 0)
            {
                this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_GAMETITLE_VALID_CATEGORY"), this.Text);                                                          
                return false;
            }

            if (string.IsNullOrEmpty(txtGameTitle.Text.Trim()))
            {
                this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_GAMETITLE_VALID_TITLE"), this.Text);                                                           
                txtGameTitle.Focus();                
                return false;
            }

            if (cmb_ManufacturerFilter.SelectedIndex <= 0)
            {
                this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_GAMETITLE_MANUFACTURER"), this.Text);                                                           
                return false;
            }

            return true;
        }

        #endregion //Methods

        private void frmGameTitle_Load(object sender, EventArgs e)
        {
            this.ResolveResources(); 
        }

    }
}
