using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BMC.EnterpriseBusiness.Entities;
using BMC.EnterpriseBusiness.Business;
using BMC.EnterpriseClient.Helpers;
using BMC.Common.LogManagement;
using BMC.Common.ExceptionManagement;
using BMC.Common;

namespace BMC.EnterpriseClient.Views
{
    public partial class frmAdminSiteClass : Form
    {
        private bool Classifmode = false;
        private bool Status = false;
        int SiteClassif_ID = 0;
        SiteDetails sdobj = new SiteDetails();
        //GetSiteClassifNameonID SiteClassif = new GetSiteClassifNameonID();
        AdminSiteClassEntity SCE = new AdminSiteClassEntity();
         BMC.EnterpriseClient.Helpers.Datawatcher objDatawatcher = null;

        public frmAdminSiteClass()
        {
            InitializeComponent();

            // Set Tags for controls
            SetTagProperty();
        }
        public frmAdminSiteClass(int SiteClassifID)
        {            
            SiteClassif_ID = SiteClassifID;
            InitializeComponent();

            // Set Tags for controls
            SetTagProperty();
        }

        private void SetTagProperty()
        {
            this.Tag = "Key_SiteClassification";
            BtnClose.Tag = "Key_Cancel";
            BtnEditOp.Tag = "Key_EditCaption";
            label1.Tag = "Key_NameColon";
            BtnUpdateOp.Tag = "Key_Update";
            BtnDeleteOp.Tag = "Key_DeleteCaption";
            BtnAddNewOp.Tag = "Key_AddCaption";
        }

        private void frmAdminSiteClass_Load(object sender, EventArgs e)
        {
            //             'this sets up the opertators information

           // LvSiteClass.Items.Clear();

            // For externalization
            this.ResolveResources();

            try
            {
                LvSiteClass.DataBindings.Clear();
                LvSiteClass.Refresh();
                txtName.Text = "";
                List<AdminSiteEntity> ClassResult = sdobj.GetClassificationinfo();
                if (ClassResult.Count > 0)
                {
                    LvSiteClass.DataSource = ClassResult;
                    LvSiteClass.DisplayMember = "Site_Classification_Name";
                    LvSiteClass.ValueMember = "Site_Classification_ID";
                    LvSiteClass.SelectedIndex = 0;
                    Status = true;
                    LvSiteClass_SelectedIndexChanged(sender, e);
                    if (SiteClassif_ID != 0)
                    {
                        Showme(SiteClassif_ID);
                        //this.Show();
                    }
                }
                else
                {
                    LvSiteClass.DataSource = null;
                    Win32Extensions.ShowInfoMessageBox(this,this.GetResourceTextByKey(1, "MSG_ADD_SITECLASS"), this.Text);            // "Please Add A New Site Classification");
                }

                txtName.Enabled = true;
                grpName.Enabled = false;

                tblLowwerButtons.ColumnStyles[0].Width = 120;
                BtnAddNewOp.Visible = true;

                if (LvSiteClass.Items.Count > 0)
                {
                    tblLowwerButtons.ColumnStyles[2].Width = 120;
                    tblLowwerButtons.ColumnStyles[3].Width = 120;
                    BtnEditOp.Visible = true;
                    BtnDeleteOp.Visible = true;
                }
                else
                {
                    tblLowwerButtons.ColumnStyles[2].Width = 0;
                    tblLowwerButtons.ColumnStyles[3].Width = 0;
                    BtnEditOp.Visible = false;
                    BtnDeleteOp.Visible = false;
                }

                tblLowwerButtons.ColumnStyles[4].Width = 0;
                tblLowwerButtons.ColumnStyles[1].Width = 0;
                BtnClose.Visible = false;
                BtnUpdateOp.Visible = false;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            objDatawatcher = new Helpers.Datawatcher(this);
        }
        public void Showme(int SiteClassifID)
        {            
            if (LvSiteClass.SelectedIndex >= 0)
            {
                try
                {
                    int i = -1;
                    int TempSiteClassifID = 0;
                    //for( i = 0; i =LvSiteClass.Items.Count - 1 
                    do
                    {
                        i = i + 1;
                        LvSiteClass.SelectedIndex = i;
                        TempSiteClassifID = Convert.ToInt32(LvSiteClass.SelectedValue);
                    }
                    while (SiteClassifID != TempSiteClassifID && i != LvSiteClass.Items.Count - 1);
                    //this.Show();
                }
                catch (Exception ex)
                {
                    ExceptionManager.Publish(ex);
                }
            }
        }
        private void LvSiteClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (LvSiteClass.Items.Count > 0)
                {
                    if (LvSiteClass.SelectedIndex > -1 && Status == true)
                    {
                        txtName.DataBindings.Clear();
                        SCE.SiteClassID = Convert.ToInt32(LvSiteClass.SelectedValue);
                        List<AdminSiteEntity> ClassResutID = sdobj.GetSiteClassificationName(SCE.SiteClassID);
                        if(ClassResutID != null)                        
                        txtName.DataBindings.Add("Text", ClassResutID, "Site_Classification_Name");
                    }
                }
                else
                {
                   // MessageBox.Show("Please Add A New Site Classification");
                    Win32Extensions.ShowInfoMessageBox(this,this.GetResourceTextByKey(1, "MSG_ADD_SITECLASS"), this.Text);            //("Please Add A New Site Classification");
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void BtnAddNewOp_Click(object sender, EventArgs e)
        {
            try
            {
                txtName.Text = "";
                Classifmode = true;
                
                tblLowwerButtons.ColumnStyles[0].Width = 0;
                BtnAddNewOp.Visible = false;

                tblLowwerButtons.ColumnStyles[4].Width = 120;
                BtnClose.Visible = true;

                tblLowwerButtons.ColumnStyles[3].Width = 0;
                BtnDeleteOp.Visible = false;

                tblLowwerButtons.ColumnStyles[2].Width = 0;
                BtnEditOp.Visible = false;

                tblLowwerButtons.ColumnStyles[1].Width = AppGlobals.Current.HasUserAccess("HQ_Admin_Operator_Edit") ? 120 : 0;
                BtnUpdateOp.Visible = AppGlobals.Current.HasUserAccess("HQ_Admin_Operator_Edit");

                LvSiteClass.Enabled = true;
                grpName.Enabled = true;

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            frmAdminSiteClass_Load(sender, e); 
        }

        private void BtnUpdateOp_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtName.Text == "")
                {
                    Win32Extensions.ShowInfoMessageBox(this,this.GetResourceTextByKey(1, "MSG_VALIDNAME"), this.Text);            //"Please enter a valid Name");
                    return;
                }
                if (Classifmode == true)
                {
                    SCE.SiteClassID = 0;
                    SCE.SiteClassName = txtName.Text;
                    sdobj.InsertUpdateSiteClassification(SCE);
                    //LvSiteClass.Items.Add(txtName.Text);
                    List<AdminSiteEntity> ClassResult = sdobj.GetClassificationinfo();
                    if (ClassResult.Count > 0)
                    {
                        LvSiteClass.DataSource = ClassResult;
                        LvSiteClass.DisplayMember = "Site_Classification_Name";
                        LvSiteClass.ValueMember = "Site_Classification_ID";
                    }
                }
                else if (LvSiteClass.SelectedIndex > -1)
                {
                    SCE.SiteClassID = Convert.ToInt32(LvSiteClass.SelectedValue);
                    SCE.SiteClassName = txtName.Text;
                    sdobj.InsertUpdateSiteClassification(SCE);
                    List<AdminSiteEntity> ClassResult = sdobj.GetClassificationinfo();
                    if (ClassResult.Count > 0)
                    {
                        LvSiteClass.DataSource = ClassResult;
                        LvSiteClass.DisplayMember = "Site_Classification_Name";
                        LvSiteClass.ValueMember = "Site_Classification_ID";
                        Showme(SCE.SiteClassID);
                    }
                }

                tblLowwerButtons.ColumnStyles[0].Width = 120;
                BtnAddNewOp.Visible = true;

                tblLowwerButtons.ColumnStyles[4].Width = 0;
                BtnClose.Visible = false;

                tblLowwerButtons.ColumnStyles[3].Width = 120;
                BtnDeleteOp.Visible = true;

                tblLowwerButtons.ColumnStyles[2].Width = 120;
                BtnEditOp.Visible = true;

                tblLowwerButtons.ColumnStyles[1].Width = 0;
                BtnUpdateOp.Visible = false;

                LvSiteClass.Enabled = true;
                grpName.Enabled = false;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void BtnEditOp_Click(object sender, EventArgs e)
        {
            Classifmode = false;

            tblLowwerButtons.ColumnStyles[0].Width = 0;
            BtnAddNewOp.Visible = false;

            tblLowwerButtons.ColumnStyles[4].Width = 120;
            BtnClose.Visible = true;

            tblLowwerButtons.ColumnStyles[3].Width = 0;
            BtnDeleteOp.Visible = false;

            tblLowwerButtons.ColumnStyles[2].Width = 0;
            BtnEditOp.Visible = false;

            tblLowwerButtons.ColumnStyles[1].Width = 120;
            BtnUpdateOp.Visible = true;

            LvSiteClass.Enabled = false;

            grpName.Enabled = true;
        }

        private void BtnDeleteOp_Click(object sender, EventArgs e)
        {
            int DeleteStatus = 0;
            try
            {
                if (Win32Extensions.ShowQuestionMessageBox(this, string.Format(this.GetResourceTextByKey(1, "MSG_DELETE_SITECLASS"), txtName.Text), this.Text) == System.Windows.Forms.DialogResult.Yes)               // "Do You Wish To Delete " + txtName.Text
                {
                    SCE.SiteClassID = Convert.ToInt32(LvSiteClass.SelectedValue);
                    DeleteStatus = sdobj.DeleteSiteClassifonSite(SCE.SiteClassID);

                    if (DeleteStatus != 1)
                    {
                        //Win32Extensions.ShowInfoMessageBox("The Site Classification" + txtName.Text + " is currently in use on sites.\r\n" + "Please de-allocate them before deleting this classification");
                        Win32Extensions.ShowInfoMessageBox(this,string.Format(this.GetResourceTextByKey(1, "MSG_SITECLASS_USED"), txtName.Text), this.Text); 
                    }
                    else
                    {
                        frmAdminSiteClass_Load(sender, e);                                               
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            //frmAdminSiteClass_Load(sender, e);                        
        }
    }
}
