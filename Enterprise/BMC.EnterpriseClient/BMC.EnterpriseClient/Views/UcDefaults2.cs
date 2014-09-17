using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using BMC.CoreLib.Win32;
using System.Windows.Forms;
using BMC.EnterpriseBusiness.Entities;
using BMC.EnterpriseBusiness.Business;
using BMC.Common.ExceptionManagement;
using BMC.Common;
//using BMC.EnterpriseClient.Helpers;

namespace BMC.EnterpriseClient.Views
{
    public partial class UcDefaults2 : UserControl, IAdminSite
    {
        #region User defined Variables
        private int _Site_ID = 0;

        public int Site_ID
        {
            get { return _Site_ID; }
            set { _Site_ID = value; }
        }
        //AdminCompanyResult objAdminCompanyResult = null;
        int _TermsGroupId = 0;
        int _RepresentativeId = 0;
        #endregion

        #region Constructor
        public UcDefaults2()
        {
            InitializeComponent();
            // Set Tags for controls
            SetTagProperty();
            this.ResolveResources();
        }
        #endregion Constructor

        #region unused Events
        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
        #endregion Unused Events

        #region Events
        private void LstTP_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                List<DefaultEntity> objnew = Default2business.CreateInstance().GetTermsgroupidname();

                if ((cmpLstTP.SelectedItem as ComboBoxItem).Text == this.GetResourceTextByKey(1, "MSG_NONE"))
                {
                    txtTP.Text = this.GetResourceTextByKey(1, "MSG_NONE");
                }
                else
                    if (objnew.Count >= 1)
                    {
                        txtTP.Text = (cmpLstTP.SelectedItem as ComboBoxItem).Text;
                        if ((cmpLstTP.SelectedItem as ComboBoxItem).Text == this.GetResourceTextByKey("Key_Use_SubCompanyDefault"))
                        {
                            txtTP.Text = this.GetResourceTextByKey(1, "MSG_NONE");
                        }
                    }
                if ((cmpLstTP.SelectedItem as ComboBoxItem).Text == this.GetResourceTextByKey("Key_Use_SubCompanyDefault"))
                {
                    txtTP.Text = this.GetResourceTextByKey(1, "MSG_NONE");
                }


            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

        }
        #endregion Events

        #region IAdminSite Members
        public void LoadDetails(AdminSiteEntity entity)
        {
            try
            {
                _RepresentativeId = Convert.ToInt32(entity.Staff_ID);
                _TermsGroupId = Convert.ToInt32(entity.Terms_Group_ID);
                _Site_ID = entity.Site_ID;
                LoadDefaults();
                if (_Site_ID == 0)
                    tableLayoutPanel1.Enabled = false;
                else
                    tableLayoutPanel1.Enabled = AppGlobals.Current.HasUserAccess("HQ_Admin_Customers_Site_Edit");

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

        }
        #endregion IAdminSite Members

        #region User defined Functions
        public void LoadDefaults()
        {

            try
            {

                List<DefaultEntity> objD = Default2business.CreateInstance().DefaultTermsgroupDefaultsResult(_Site_ID);

                AdminSiteEntity Dentity = new AdminSiteEntity();

                foreach (var vdefault in objD)
                {
                    if (vdefault.Terms_Group_ID > 0)
                    {
                        if (vdefault.Terms_Group_Name == null)
                        {
                            txtTP.Text = this.GetResourceTextByKey(1, "MSG_NONE");
                            return;
                        }
                        txtTP.Text = vdefault.Terms_Group_Name;
                       // return;
                    }

                }


                List<DefaultEntity> objnew = Default2business.CreateInstance().GetTermsgroupidname();
                List<ComboBoxItem> lstTermsKey = new List<ComboBoxItem>();
                lstTermsKey.Add(
                    new ComboBoxItem()
                    {

                        Text = this.GetResourceTextByKey("Key_Use_SubCompanyDefault"),
                        Value = "-1"
                    }
                       );
                lstTermsKey.Add(
                    new ComboBoxItem()
                    {
                        Text =this.GetResourceTextByKey(1, "MSG_NONE"),
                        Value = "0"
                    }
                       );

                foreach (var v in objnew)
                {
                    lstTermsKey.Add(
                    new ComboBoxItem()
                    {
                        Text = v.Terms_Group_Name.ToString(),
                        Value = v.Terms_Group_ID.ToString()
                    });

                }
                cmpLstTP.DataSource = lstTermsKey;
                cmpLstTP.ValueMember = "Value";
                cmpLstTP.DisplayMember = "Text";
                cmpLstTP.SelectedIndex = 0;
                BMC.EnterpriseClient.Helpers.frmAdminUtilities frmUtobj = new BMC.EnterpriseClient.Helpers.frmAdminUtilities();
                CommonUtility cuobj = new CommonUtility();
                frmUtobj.setListBox(cmpLstTP, "", Convert.ToInt64(cuobj.VerifyValidNumberLong(_TermsGroupId.ToString())));
                if (_TermsGroupId == 0)
                {
                    cmpLstTP.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

            //
            //
            //
            try
            {

                List<DefaultEntity> objE = Default2business.CreateInstance().AcessKeyDefaultResult(_Site_ID);
                foreach (var vaccess in objE)
                {
                    if (vaccess.Access_Key_ID > 0)
                    {
                        if (vaccess.Access_Key_Name == null)
                        {
                            txtAKey.Text = this.GetResourceTextByKey(1, "MSG_NONE");
                            return;
                        }
                        txtAKey.Text = vaccess.Terms_Group_Name;
                        return;
                    }

                }

                List<DefaultEntity> objAccess = Default2business.CreateInstance().GetAccesskeyResult();
                txtAKey.Text = this.GetResourceTextByKey(1, "MSG_NONE");
                List<ComboBoxItem> lstAccessKey = new List<ComboBoxItem>();
                lstAccessKey.Add(
                    new ComboBoxItem()
                    {

                        Text = this.GetResourceTextByKey("Key_Use_SubCompanyDefault"),
                        Value = "-1"
                    }
                       );
                lstAccessKey.Add(
                    new ComboBoxItem()
                    {
                        Text = this.GetResourceTextByKey(1, "MSG_NONE"),
                        Value = "0"
                    }
                       );

                foreach (var v in objAccess)
                {
                    lstAccessKey.Add(
                    new ComboBoxItem()
                    {
                        Text = v.Access_Key_Name.ToString(),
                        Value = v.Access_Key_ID.ToString()
                    });

                }
                foreach (var v in objAccess)
                {
                    lstAccessKey.Add(
                    new ComboBoxItem()
                    {
                        Text = v.Access_Key_Manufacturer.ToString(),
                        Value = ""
                    });

                }
                foreach (var v in objAccess)
                {
                    lstAccessKey.Add(
                    new ComboBoxItem()
                    {
                        Text = v.Access_Key_Name.ToString(),
                        Value = ""
                    });

                }
                foreach (var v in objAccess)
                {
                    lstAccessKey.Add(
                    new ComboBoxItem()
                    {
                        Text = v.Access_Key_Manufacturer.ToString(),
                        Value = ""
                    });

                }

                cmbLstAKey.DataSource = lstAccessKey;
                cmbLstAKey.ValueMember = "Value";
                cmbLstAKey.DisplayMember = "Text";
                cmbLstAKey.SelectedIndex = 0;

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            ////

            try
            {

                List<DefaultEntity> objF = Default2business.CreateInstance().RepresentitiveDefaultResult(_Site_ID);

                foreach (var vRep in objF)
                {
                    if (vRep.Staff_ID > 0)
                    {
                        if (vRep.Staff_First_Name == null || vRep.Staff_Last_Name == null)
                        {
                            txtRep.Text = this.GetResourceTextByKey(1, "MSG_NONE");
                            return;
                        }
                        txtRep.Text = String.Concat(vRep.Staff_Last_Name.ToString() + ", " + vRep.Staff_First_Name.ToString());
                        return;
                    }
                }

                List<DefaultEntity> objRep = Default2business.CreateInstance().GetRepresentativecheck();

                List<ComboBoxItem> lstRepresentativecheck = new List<ComboBoxItem>();
                lstRepresentativecheck.Add(
                    new ComboBoxItem()
                    {

                        Text = this.GetResourceTextByKey("Key_Use_SubCompanyDefault"),
                        Value = "-1"
                    }
                       );
                lstRepresentativecheck.Add(
                    new ComboBoxItem()
                    {
                        Text = this.GetResourceTextByKey(1, "MSG_NONE"),
                        Value = "0"
                    }
                       );
                foreach (var v in objRep)
                {
                    if (_Site_ID == 0)
                    {
                    }
                    else
                    {
                        lstRepresentativecheck.Add(
                        new ComboBoxItem()
                        {

                            Text = String.Concat(v.Staff_Last_Name.ToString() + ", " + v.Staff_First_Name.ToString()),
                            Value = v.Staff_ID.ToString()
                        });
                    }

                }
                cmbLstRep.DataSource = lstRepresentativecheck;
                cmbLstRep.ValueMember = "Value";
                cmbLstRep.DisplayMember = "Text";
                cmbLstRep.SelectedIndex = 0;

                if (_Site_ID == 0)
                {
                }
                else
                {
                    BMC.EnterpriseClient.Helpers.frmAdminUtilities frmUtobjr = new BMC.EnterpriseClient.Helpers.frmAdminUtilities();
                    CommonUtility cuobjr = new CommonUtility();
                    frmUtobjr.setListBox(cmbLstRep, "", Convert.ToInt64(cuobjr.VerifyValidNumberLong(_RepresentativeId.ToString())));
                }
                if (_RepresentativeId == 0)
                {
                    cmbLstRep.SelectedIndex = 0;
                }
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

            //////////////////////
            try
            {
                //    //LOAD lstTPRecurseType value

                List<ComboBoxItem> lstTPRecurseType = new List<ComboBoxItem>();
                lstTPRecurseType.Add(
                    new ComboBoxItem()
                    {
                        Text = this.GetResourceTextByKey(1, "MSG_NONE"),
                        Value = "-1"
                    }
                       );
                lstTPRecurseType.Add(
                    new ComboBoxItem()
                    {
                        Text = this.GetResourceTextByKey("Key_ItemusingCascade"),
                        Value = "0"
                    });
                lstTPRecurseType.Add(
                    new ComboBoxItem()
                    {
                        Text = this.GetResourceTextByKey("Key_Allitems"),
                        Value = "1"
                    }
                       );
                cmbLstTPRecurseType.DataSource = lstTPRecurseType;
                cmbLstTPRecurseType.ValueMember = "value";
                cmbLstTPRecurseType.DisplayMember = "text";
                cmbLstTPRecurseType.SelectedIndex = -1;


                //LOAD lstTPRecurseType value

                List<ComboBoxItem> lstcmbLstAKeyRecurseType = new List<ComboBoxItem>();
                lstcmbLstAKeyRecurseType.Add(
                    new ComboBoxItem()
                    {
                        Text = this.GetResourceTextByKey(1, "MSG_NONE"),
                        Value = "-1"
                    }
                       );
                lstcmbLstAKeyRecurseType.Add(
                    new ComboBoxItem()
                    {
                        Text = this.GetResourceTextByKey("Key_ItemusingCascade"),
                        Value = "0"
                    });
                lstcmbLstAKeyRecurseType.Add(
                    new ComboBoxItem()
                    {
                        Text = this.GetResourceTextByKey("Key_Allitems"),
                        Value = "1"
                    }

                       );
                cmbLstAKeyRecurseType.DataSource = lstcmbLstAKeyRecurseType;
                cmbLstAKeyRecurseType.ValueMember = "value";
                cmbLstAKeyRecurseType.DisplayMember = "text";
                cmbLstAKeyRecurseType.SelectedIndex = -1;

                //LOAD lstTPRecurseType value

                List<ComboBoxItem> lstcmblstRepRecurseType = new List<ComboBoxItem>();
                lstcmblstRepRecurseType.Add(
                    new ComboBoxItem()
                    {
                        Text = this.GetResourceTextByKey(1, "MSG_NONE"),
                        Value = "-1"
                    }
                       );
                lstcmblstRepRecurseType.Add(
                    new ComboBoxItem()
                    {
                        Text = this.GetResourceTextByKey("Key_ItemusingCascade"),
                        Value = "0"
                    });
                lstcmblstRepRecurseType.Add(
                    new ComboBoxItem()
                    {
                        Text = this.GetResourceTextByKey("Key_Allitems"),
                        Value = "1"
                    }
                       );
                cmblstRepRecurseType.DataSource = lstcmblstRepRecurseType;
                cmblstRepRecurseType.ValueMember = "value";
                cmblstRepRecurseType.DisplayMember = "text";
                cmblstRepRecurseType.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void SetTagProperty()
        {
            try
            {
                this.lblAccessKey.Tag = "Key_AccessKeyColon";
                this.btntermsgroup.Tag = "Key_Apply";
                this.btnaccesskey.Tag = "Key_Apply";
                this.btnrepresntitive.Tag = "Key_Apply";
                this.lblCascadeType.Tag = "Key_CascadeTypeColon";
                this.lblItem.Tag = "Key_Item";
                this.lblRepresentitive.Tag = "Key_RepresentativeColon";
                this.lbltestgroup.Tag = "Key_TermsGroupColon";
                this.lblValue.Tag = "Key_Value";

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
        #endregion User defined Functions

        #region Events
        private void btntermsgroup_Click(object sender, EventArgs e)
        {
            try
            {
                if (_Site_ID == 0)
                {
                   // this.ShowInfoMessageBox("");
                    Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_UCDEFAULT_EMPTY"), this.ParentForm.Text);
                    return;
                }
                if (cmpLstTP.SelectedIndex == -1)
                {
                   // this.ShowInfoMessageBox("Please select a terms profile to allocate");
                    Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_UCDEFAULT_TERMSPROFILE"), this.ParentForm.Text);
                }
                if (cmbLstTPRecurseType.SelectedIndex == -1)
                {
                   // this.ShowInfoMessageBox("Please select a cascade type");
                    Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_UCDEFAULT_CASCADE"), this.ParentForm.Text);
                    return;
                }
                bool IsDefault = false;

               // if (this.ShowQuestionMessageBox("Are you sure? This may take some time...") == DialogResult.No)
                if (Win32Extensions.ShowQuestionMessageBox(this, this.GetResourceTextByKey(1, "MSG_UCDEFAULT_TIME"), this.ParentForm.Text) == DialogResult.No)
                {
                    return;
                }
                IsDefault = true;
                int Tappvalue = 0;
                List<DefaultEntity> objTapply = Default2business.CreateInstance().TermsgroupApply(_Site_ID);
                if (objTapply.Count > 0)
                {
                    foreach (var vtapp in objTapply)
                    {
                        if (vtapp.Terms_Group_ID == null)
                        {
                            Tappvalue = 0;
                        }
                        if (vtapp.Terms_Group_ID > 0)
                        {
                            Tappvalue = Convert.ToInt32(vtapp.Terms_Group_ID);
                        }
                        else
                        {
                            Tappvalue = Convert.ToInt32((cmpLstTP.SelectedItem as ComboBoxItem).Value);

                        }
                    }
                }
                else
                {
                    Tappvalue = Convert.ToInt32((cmpLstTP.SelectedItem as ComboBoxItem).Value);

                }
               // int cashcasetype = Convert.ToInt32((cmbLstAKeyRecurseType.SelectedItem as ComboBoxItem).Value);
                int cashcasetype = Convert.ToInt32(cmbLstAKeyRecurseType.SelectedValue);
                IsDefault = false;

                if (Default2business.CreateInstance().CascadeSiteUpdateTermsGroup(_Site_ID, Tappvalue, cashcasetype, IsDefault) == false)
                {
                    //this.ShowInfoMessageBox("Done");
                    Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_DONE"), this.ParentForm.Text);

                }
                else
                {
                    //this.ShowInfoMessageBox("Done");
                    Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_DONE"), this.ParentForm.Text);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
        #endregion Events

        #region IAdminSite Members

        public bool SaveDetails(AdminSiteEntity entity)
        {
            entity.Terms_Group_ID =Convert.ToInt32(cmpLstTP.SelectedValue);
            entity.Access_Key_ID = Convert.ToInt32(cmbLstAKey.SelectedValue);
            entity.RepresentitiveDefaultResultStaff_ID = Convert.ToInt32(cmbLstRep.SelectedValue);
            return true;
        }
        #endregion IAdminSite Members
        #region Events
        private void LstAKey_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ///We are disable the Acces Key on code So The function disabled \\\\\\\\\
                txtAKey.Text = this.GetResourceTextByKey(1, "MSG_NONE");
                //////////////////////////////
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
        private void lstRepRecurseType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void btnaccesskey_Click(object sender, EventArgs e)
        {
            try
            {

                if (_Site_ID == 0)
                {
                    //this.ShowInfoMessageBox("");
                    Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_UCDEFAULT_EMPTY"), this.ParentForm.Text);
                    return;
                }

                if (cmbLstAKey.SelectedIndex == -1)
                {
                   // this.ShowInfoMessageBox("Please select a default key to allocate");
                    Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_UCDEFAULT_DEFAULTKEY"), this.ParentForm.Text);
                }
                if (cmbLstAKeyRecurseType.SelectedIndex == -1)
                {
                   // this.ShowInfoMessageBox("Please select a cascade type");
                    Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_UCDEFAULT_CASCADE"), this.ParentForm.Text);
                    return;
                }

               // if (this.ShowQuestionMessageBox("Are you sure? This may take some time...") == DialogResult.No)
                if (Win32Extensions.ShowQuestionMessageBox(this, this.GetResourceTextByKey(1, "MSG_UCDEFAULT_TIME"), this.ParentForm.Text) == DialogResult.No)
                {
                    return;
                }


                int value = 0;
                List<DefaultEntity> objEntity5 = Default2business.CreateInstance().AccesskeyApply(_Site_ID);

                if (objEntity5.Count >= 1)
                {
                    int cashcasetype = 0;

                    bool IsDefault = true;

                    foreach (var v in objEntity5)
                    {
                        if (v.Access_Key_ID == null)
                        {

                        }
                        if (v.Access_Key_ID > 0)
                        {
                            value = Convert.ToInt32(v.Terms_Group_ID);
                        }
                        else
                        {
                            value = Convert.ToInt32((cmbLstAKey.SelectedItem as ComboBoxItem).Value);
                        }
                        cashcasetype = Convert.ToInt32((cmbLstAKeyRecurseType.SelectedItem as ComboBoxItem).Value);

                    }
                    IsDefault = false;

                    if (Default2business.CreateInstance().CascadeSiteUpdateAccessKey(_Site_ID, value, cashcasetype, IsDefault) == false)
                    {
                        //this.ShowInfoMessageBox("Done");
                        Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_DONE"), this.ParentForm.Text);
                    }

                    else
                    {
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

        }

        private void btnrepresntitive_Click(object sender, EventArgs e)
        {
            try
            {
                if (_Site_ID == 0)
                {
                   // this.ShowInfoMessageBox("");
                    Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_UCDEFAULT_EMPTY"), this.ParentForm.Text);
                    return;
                }



                if (cmbLstRep.SelectedIndex == -1)
                {
                   // this.ShowInfoMessageBox("Please select a default key to allocate");
                    Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_UCDEFAULT_DEFAULTKEY"), this.ParentForm.Text);
                }
                if (cmblstRepRecurseType.SelectedIndex == -1)
                {
                   // this.ShowInfoMessageBox("Please select a cascade type");
                    Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_UCDEFAULT_CASCADE"), this.ParentForm.Text);
                    return;
                }
              //  if (this.ShowQuestionMessageBox("Are you sure? This may take some time...") == DialogResult.No)
                if (Win32Extensions.ShowQuestionMessageBox(this, this.GetResourceTextByKey(1, "MSG_UCDEFAULT_TIME"), this.ParentForm.Text) == DialogResult.No)
                {
                    return;
                }
                int value = 0;
                int cashcasetype = 0;
                bool IsDefault;
                IsDefault = true;


                List<DefaultEntity> objEntity5 = Default2business.CreateInstance().RepresntitiveApply(_Site_ID);
                if (objEntity5.Count >= 1)
                {
                    foreach (var v in objEntity5)
                    {
                        if (v.Staff_ID == null)
                        {
                        }
                        if (v.Staff_ID > 0)
                        {
                            value = Convert.ToInt32(v.Terms_Group_ID);
                        }
                        else
                        {

                            value = Convert.ToInt32((cmbLstRep.SelectedItem as ComboBoxItem).Value);
                        }
                        cashcasetype = Convert.ToInt32((cmblstRepRecurseType.SelectedItem as ComboBoxItem).Value);
                    }

                    IsDefault = false;

                    if (Default2business.CreateInstance().CascadeSiteUpdateResntitive(_Site_ID, value, cashcasetype, IsDefault) == false)
                    {
                       // this.ShowInfoMessageBox("Done");
                        Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_DONE"), this.ParentForm.Text);
                    }
                    else
                    {
                       // this.ShowInfoMessageBox("Done");
                        Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_DONE"), this.ParentForm.Text);
                    }


                }
                IsDefault = false;

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void cmbLstRep_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                List<DefaultEntity> objRepchange = Default2business.CreateInstance().GetRepresentativecheck();
                if (objRepchange.Count == null)
                {
                    cmbLstRep.SelectedIndex = 0;
                }
                if (_Site_ID == 0)
                {
                    txtRep.Text = this.GetResourceTextByKey(1, "MSG_NONE");

                }

                if (objRepchange.Count > 1)
                {
                    if (cmbLstRep.SelectedIndex == -1)
                    {
                        cmbLstRep.SelectedIndex = 0;
                        txtRep.Text = this.GetResourceTextByKey(1, "MSG_NONE");


                    }

                    txtRep.Text = (cmbLstRep.SelectedItem as ComboBoxItem).Text;

                    if ((cmbLstRep.SelectedItem as ComboBoxItem).Text == this.GetResourceTextByKey("Key_Use_SubCompanyDefault"))
                    {
                        txtRep.Text = this.GetResourceTextByKey(1, "MSG_NONE");
                    }

                }
                else
                {
                    txtRep.Text = this.GetResourceTextByKey(1, "MSG_NONE");
                }


            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

        }
        #endregion Events

        #region Unused lable
        private void txtTP_TextChanged(object sender, EventArgs e)
        {

        }
        #endregion Unused lable
    }
}
