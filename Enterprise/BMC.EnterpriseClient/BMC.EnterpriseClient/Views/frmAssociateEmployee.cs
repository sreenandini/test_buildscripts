using BMC.Common.ExceptionManagement;
using BMC.EnterpriseBusiness.Business;
using BMC.EnterpriseBusiness.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BMC.EnterpriseClient.Helpers;
using BMC.Common;
using BMC.Security.Interfaces;
using BMC.Common.Utilities;

namespace BMC.EnterpriseClient.Views
{

    public partial class frmAssociateEmployee : Form
    {
        SaveChanges objDelSave;
        bool bFirst = true;
        bool bEventsFirst = true;
        string UserName = string.Empty;
        IRole Role = null;
        public frmAssociateEmployee(string sUserGroupName)
        {
            InitializeComponent();
            UserName = sUserGroupName;
            BMC.Security.Manager.RoleManager obj = new BMC.Security.Manager.RoleManager(DatabaseHelper.GetConnectionString());
            Role = obj.GetRoleByName(UserName);
            txtUserGroup.Text = Role.RoleName;
            this.ucAssociateEmployeeGMUModes1.RoleName = Role.RoleName;
            this.ucAssociateEmployeeGMUModes1.RoleID = Role.SecurityRoleID;
            this.ucAssoicateEmployeeEvents1.RoleName = Role.RoleName;
            this.ucAssoicateEmployeeEvents1.RoleID = Role.SecurityRoleID;
            SetPropertyTag();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            if (bFirst && this.ucAssociateEmployeeGMUModes1.objDelSave != null)
            {
                objDelSave += this.ucAssociateEmployeeGMUModes1.objDelSave;
                bFirst = false;
            }
            if (bEventsFirst && this.ucAssoicateEmployeeEvents1.objDelSave != null)
            {
                objDelSave += this.ucAssoicateEmployeeEvents1.objDelSave;
                bEventsFirst = false;
            }
            if (objDelSave != null)
            {
                if (objDelSave())
                    this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_DETAILS_UPDATE_SUCCESS"));
            }
            else
                this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_ENT_EMP_CARD_VALID_SELECT"));
        }
        public void SetPropertyTag()
        {
            this.tbpAssociateModes.Tag = "Key_AssociateModes";
            this.tbpAssociateEvents.Tag = "Key_AssociateEvents";
            this.btnClose.Tag = "Key_Close";
            this.btnSave.Tag = "Key_SaveCaption";
            this.lblUserGroup.Tag = "Key_Profiler_ExportType_USERROLE";
            this.lblCardLevel.Tag = "Key_CardLevel";
        }

        private void frmAssociateEmployee_Load(object sender, EventArgs e)
        {
            this.ResolveResources();
            FillEmpCardLevels();
        }

        private void FillEmpCardLevels()
        {
            try
            {
                // cmbCardLevel.SelectedIndexChanged -= cmbCardLevel_SelectedIndexChanged;
                List<EmployeeCardEntity> lstEmpCardLevels = null;
                EmployeeCardBiz objEmployeeCardBiz = new EmployeeCardBiz();
                List<CardLevel> oCardLevel = objEmployeeCardBiz.GetCardLevelBasedOnRole();
                lstEmpCardLevels = objEmployeeCardBiz.GetCardLevels();
                if (lstEmpCardLevels.Count == 0)
                {
                    cmbCardLevel.Items.Add(1);
                    cmbCardLevel.Text = "1";
                }
                else
                {
                    cmbCardLevel.DataSource = lstEmpCardLevels == null ? new List<EmployeeCardEntity>() : lstEmpCardLevels;
                    cmbCardLevel.DisplayMember = "EmpCardLevel";
                    cmbCardLevel.ValueMember = "EmpCardLevel";
                }
                if (oCardLevel == null)
                {
                    cmbCardLevel.SelectedIndex = 0;
                }
                else
                {
                    int? CardLevel = oCardLevel.Where(o => o.RoleID == Role.SecurityRoleID).Select(o => o.CardLevelID).FirstOrDefault();
                    cmbCardLevel.SelectedIndex = lstEmpCardLevels.FindIndex(o => o.EmpCardLevel == CardLevel);
                    //  cmbCardLevel.SelectedIndexChanged += cmbCardLevel_SelectedIndexChanged;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void cmbCardLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ucAssociateEmployeeGMUModes1.bFirst = true;
            this.ucAssociateEmployeeGMUModes1.CardLevel = (cmbCardLevel.SelectedIndex == 0) ? 1 : ((EmployeeCardEntity)(cmbCardLevel.SelectedItem)).EmpCardLevel;
            this.ucAssoicateEmployeeEvents1.CardLevel = (cmbCardLevel.SelectedIndex == 0) ? 1 : ((EmployeeCardEntity)(cmbCardLevel.SelectedItem)).EmpCardLevel;
        }
    }
}
