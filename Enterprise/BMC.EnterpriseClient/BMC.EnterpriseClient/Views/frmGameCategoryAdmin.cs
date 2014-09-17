using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using BMC.EnterpriseBusiness.Business;
using BMC.EnterpriseBusiness.Entities;
using BMC.Common.ExceptionManagement;
using Audit.Transport;
using BMC.CoreLib.Win32;
using BMC.Common;
namespace BMC.EnterpriseClient.Views
{
    partial class frmGameCategoryAdmin : Form
    {
        #region Data Members

        private int _Game_Category_ID;
        private string _Game_Category_Name;
        private GameLibraryBiz objGameLibraryBiz = new GameLibraryBiz();
        BMC.EnterpriseClient.Helpers.Datawatcher objDatawatcher = null;
        string _msgBoxTitle = string.Empty;

        #endregion //Data Members

        #region Properties

        public int Game_Category_ID
        {
            get
            {
                return this._Game_Category_ID;
            }
            set
            {
                if (this._Game_Category_ID != value)
                {
                    this._Game_Category_ID = value;
                }
            }
        }

        public string Game_Category_Name
        {
            get
            {
                return this._Game_Category_Name;
            }
            set
            {
                if (this._Game_Category_Name != value)
                {
                    this._Game_Category_Name = value;
                }
            }
        }

        #endregion //Properties

        #region Constructor

        public frmGameCategoryAdmin()
        {
            InitializeComponent();
            LoadForm();
            SetPropertyTag();            
        }

        private void SetPropertyTag()
        {
            try
            {
                this.btnCancel.Tag = "Key_CancelCaption";
                this.btnSave.Tag = "Key_SaveCaption";
                this.label1.Tag = "Key_NameColon";
                this.Tag = "Key_GameCategoryAdmin";
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        public frmGameCategoryAdmin(int _Game_Category_ID)
        {
            InitializeComponent();
            SetPropertyTag();
            Game_Category_ID = _Game_Category_ID;
            LoadForm();           
        }

        #endregion //Constructor

        #region Events

        public void LoadForm()
        {
            try
            {
                if (Game_Category_ID != 0)
                {
                    Game_Category_Name = objGameLibraryBiz.GetGameCategoryByGameID(Game_Category_ID);
                    if (Game_Category_Name != "")
                    {
                        txtName.Text = Game_Category_Name;
                    }
                }
                //if (Game_Category_ID == 0)
                //{
                //}
                //else
                //{
                //    Game_Category_Name = objGameLibraryBiz.GetGameCategoryByGameID(Game_Category_ID);
                //    if (Game_Category_Name != "")
                //    {
                //        txtName.Text = Game_Category_Name;
                //    }
                //}
                objDatawatcher = new Helpers.Datawatcher(this);
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btnEditSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtName.Text.Trim() == "")
                {
                    this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_GAMECATEGORY_VALID_NAME"), this.Text);
                }
                else
                {
                    Game_Category_Name = txtName.Text;
                    int insert_update_result = objGameLibraryBiz.SaveGameCategory(Game_Category_ID, Game_Category_Name);
                    if (insert_update_result == -1)
                    {
                        this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_GAMECATEGORY_UNIQUECATEGORY"), this.Text);
                        return;
                    }
                    else
                    {
                        objGameLibraryBiz.InsertNewAuditEntry(ModuleNameEnterprise.AUDIT_MANUFACTURER, "Game Category Admin", "Game Category Added", Game_Category_Name);
                        this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_GAMECATEGORY_SUCCESS"), this.Text);
                        this.DialogResult = DialogResult.OK;
                        objDatawatcher.DataModify = false;
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        #endregion //Events

        private void frmGameCategoryAdmin_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void frmGameCategoryAdmin_Load(object sender, EventArgs e)
        {
            this.ResolveResources();   
        }
    }
}
