using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BMC.EnterpriseClient.Helpers;
using BMC.EnterpriseBusiness.Entities;
using BMC.EnterpriseBusiness.Business;
using BMC.Common.LogManagement;
using BMC.Common.ExceptionManagement;
using BMC.Common;

namespace BMC.EnterpriseClient.Views
{
    public partial class frmAdminOpeningTimes : Form
    {
        SiteDetails sdobj = new SiteDetails();
        SiteOpeningHours OpenHr = new SiteOpeningHours();
        BMC.EnterpriseClient.Helpers.Datawatcher objDatawatcher = null;
        private string _sMode = "";

        public frmAdminOpeningTimes()
        {
            InitializeComponent();
            SetTagProperty();
        }

        private void SetTagProperty()
        {
            this.btnClose.Tag = "Key_CloseCaption";
            this.btnEdit.Tag = "Key_EditCaption";
            this.btnNew.Tag = "Key_NewCaption";
            this.Tag = "Key_StandardOpeningTimes";
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            EnableDisableControls("New");
        }

        private void EnableDisableControls(string Type)
        {
            switch (Type)
            {
                case "New":
                    lstOpeningTimes.Enabled = false;
                    ucOpeningHour.ClearGridViewData();
                    btnNew.Visible = false;
                    btn_Cancel.Visible = true;
                    btn_Update.Visible = true;
                    btnEdit.Visible = false;
                    ucOpeningHour.EnableDisableContols("New", false);
                    _sMode = "New";
                    break;
                case "Edit":
                    lstOpeningTimes.Enabled = false;
                    btnNew.Visible = false;
                    btnEdit.Visible = false;
                    btn_Cancel.Visible = true;
                    btn_Update.Visible = true;
                    ucOpeningHour.EnableDisableContols("Edit", true);
                    _sMode = "Edit";
                    break;
                case "Cancel":
                    lstOpeningTimes.Enabled = true;
                    btnEdit.Visible = true;
                    btnNew.Visible = true;
                    btn_Update.Visible = false;
                    btn_Cancel.Visible = false;
                    ucOpeningHour.EnableDisableContols("Cancel", false);
                    if (lstOpeningTimes.Items.Count > 0)
                    {
                        lstOpeningTimes_SelectedIndexChanged(lstOpeningTimes, null);
                    }
                    else
                    {
                        btnEdit.Visible = false;
                    }
                    break;
            }

        }

        private void frmAdminOpeningTimes_Load(object sender, EventArgs e)
        {
            this.ResolveResources();
            this.lstOpeningTimes.SelectedIndexChanged -= new System.EventHandler(this.lstOpeningTimes_SelectedIndexChanged);
            try
            {
                LoadData();
                this.lstOpeningTimes.SelectedIndexChanged += new System.EventHandler(this.lstOpeningTimes_SelectedIndexChanged);
                objDatawatcher = new Helpers.Datawatcher(this);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void LoadData()
        {
            List<AdminSiteEntity> StdOpnHrs = sdobj.GetStdOpeningHours();
            lstOpeningTimes.Refresh();
            if (StdOpnHrs != null)
            {
                lstOpeningTimes.DataSource = StdOpnHrs;
                lstOpeningTimes.DisplayMember = "Standard_Opening_Hours_Description";
                lstOpeningTimes.ValueMember = "Standard_Opening_Hours_ID";

                if (lstOpeningTimes.Items.Count > 0)
                {
                    btnEdit.Visible = true;
                    lstOpeningTimes_SelectedIndexChanged(lstOpeningTimes, null);
                }
                else
                {
                    btnEdit.Visible = false;
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                EnableDisableControls("Edit");
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void lstOpeningTimes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstOpeningTimes.SelectedIndex >= 0)
            {
                ucOpeningHour.LoadData(0, 0, Convert.ToInt64(lstOpeningTimes.SelectedValue), false, false);
            }
        }

        private void btn_Update_Click(object sender, EventArgs e)
        {
            try
            {
                if (_sMode == "New")
                {
                    long StdOpenHRID = 0;
                    string OpeningHours = string.Empty;
                    if (ucOpeningHour.ValidateControls(out OpeningHours))
                    {
                        if (OpeningHours.Trim() != null)
                        {
                            try
                            {
                                StdOpenHRID = sdobj.CheckInsertStdOpeningHrsDesc(OpeningHours.Trim());
                                if (StdOpenHRID == 0)
                                {
                                    Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_OPENHOURS_NAMEEXISTS"), this.Text);            // "That name already exists, please choose another");
                                    return;
                                }
                                else
                                {
                                    LoadData();
                                    EnableDisableControls("Cancel");
                                    lstOpeningTimes.SelectedValue = Convert.ToInt32(StdOpenHRID);
                                    Win32Extensions.ShowInfoMessageBox(this, "New OpenHours '" + OpeningHours + "' Created successfully.", this.Text);
                                }
                            }
                            catch (Exception ex)
                            {
                                Win32Extensions.ShowErrorMessageBox(this, "New OpenHours '" + OpeningHours + "' Creation Failed.", this.Text);
                                ExceptionManager.Publish(ex);
                            }
                        }
                    }
                }
                else if (_sMode == "Edit")
                {
                    int lastSelectedIndex = Convert.ToInt32(lstOpeningTimes.SelectedValue);
                    try
                    {
                        ucOpeningHour.SaveChanges();
                        EnableDisableControls("Cancel");
                        Win32Extensions.ShowInfoMessageBox(this, "OpenHours '" + lstOpeningTimes.Text + "' details updated successfully.", this.Text);
                    }
                    catch (Exception ex)
                    {
                        Win32Extensions.ShowErrorMessageBox(this, "OpenHours '" + lstOpeningTimes.Text + "' update failed.", this.Text);
                        ExceptionManager.Publish(ex);
                    }
                    finally
                    {
                        ucOpeningHour.EnableDisableContols("Cancel", false);
                        LoadData();
                        if (lstOpeningTimes.Items.Count > 0)
                        {
                            lstOpeningTimes.SelectedValue = lastSelectedIndex;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Win32Extensions.ShowErrorMessageBox(this, "Unable to update the OpenHour details.", this.Text);
                ExceptionManager.Publish(ex);
            }
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            EnableDisableControls("Cancel");
        }

    }
}
