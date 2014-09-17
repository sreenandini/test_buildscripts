using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BMC.EnterpriseBusiness.Entities;
using BMC.Common.LogManagement;
using BMC.Common.ExceptionManagement;
using BMC.EnterpriseBusiness.Business;
using BMC.Common;
namespace BMC.EnterpriseClient.Views
{
    public partial class Areas : UserControl, IAdminSite
    {
        #region Local Declartion

        #region Objects
        private SiteAreaBiz _objSiteAreaBiz = null;
        AdminSubCompany _AdminSubCompany = null;
        List<SubCompayRegions> _lstSubCompayRegions;
        ListViewItem[] lvItem;
        AdminSiteEntity _AdminSite = null;
        #endregion Objects

        #endregion Local Declartion

        #region Drived Methods
        //Methods of IAdminSite Interface
        public void LoadDetails(AdminSiteEntity entity)
        {
            try
            {
                LogManager.WriteLog("Inside Site -> Areas load Details", LogManager.enumLogLevel.Info);
                _AdminSite = entity;
                if (entity.Site_ID !=0 && !AppGlobals.Current.HasUserAccess("HQ_Admin_Customers_Site_Edit"))
                {
                    grpAreas.Enabled = false;
                    grpDistricts.Enabled = false;
                    grpRegions.Enabled = false;
                }
                LoadSubCompanyRegion();            
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        public bool SaveDetails(AdminSiteEntity entity)
        {
            try
            {
                LogManager.WriteLog("Inside Site -> Areas SaveDetails", LogManager.enumLogLevel.Info);
                entity.Sub_Company_Region_ID =lvRegions.Items.Count == 0 ? 0 : Convert.ToInt32(lvRegions.SelectedItems[0].Tag);
                entity.Sub_Company_Area_ID = lvAreas.Items.Count == 0 ? 0 : Convert.ToInt32(lvAreas.SelectedItems[0].Tag);
                entity.Sub_Company_District_ID = lvDistricts.Items.Count == 0 ? 0 : Convert.ToInt32(lvDistricts.SelectedItems[0].Tag);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return false;
            }

            return true;
        }
        #endregion Drived Methods

        #region Events
        
        public Areas()
        {
            InitializeComponent();
            setTagProperty();
            _objSiteAreaBiz = new SiteAreaBiz();
            _AdminSubCompany = new AdminSubCompany();
            _lstSubCompayRegions = null;
            lvItem = null;
        }

        private void setTagProperty()
        {
            this.lblAreas.Tag = "Key_Areas";
            this.lblRegionDescription.Tag = "Key_DescriptionColon";
            this.lblAreaDescription.Tag = "Key_DescriptionColon";
            this.lblDistrictDescription.Tag = "Key_DescriptionColon";
            this.lblDistricts.Tag = "Key_Districts";
            this.lblRegionName.Tag = "Key_NameColon";
            this.lblAreaName.Tag = "Key_NameColon";
            this.lblDistrictName.Tag = "Key_NameColon";
            this.label1.Tag = "Key_Regions";
            this.lblRegionStaff.Tag = "Key_StaffColon";
            this.lblAreaStaff.Tag = "Key_StaffColon";
            this.lblDistrictStaff.Tag = "Key_StaffColon";
        }

        private void Areas_Load(object sender, EventArgs e)
        {
            this.ResolveResources();
        }

        private void lvRegions_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (lvRegions.SelectedItems.Count <= 0) return;
                
                ListViewItem item = (ListViewItem)lvRegions.SelectedItems[0];
                
                if (item != null)
                {
                    txtRegionName.Text = item.SubItems[0].Text;
                    txtRegionStaff.Text = item.SubItems[1].Text;
                    txtRegionDescription.Text = item.SubItems[2].Text;
                    LoadAreaDetails();                    
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void lvAreas_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (lvAreas.SelectedItems.Count <= 0) return;
                ListViewItem item = (ListViewItem)lvAreas.SelectedItems[0];
                if (item != null)
                {
                    txtAreaName.Text = item.SubItems[0].Text;
                    txtAreaStaff.Text = item.SubItems[1].Text;
                    txtAreaDescription.Text = item.SubItems[2].Text;
                    LoadDistrictDetails();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void lvDistricts_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (lvDistricts.SelectedItems.Count <= 0) return;
                ListViewItem item = (ListViewItem)lvDistricts.SelectedItems[0];
                if (item != null)
                {
                    txtDistrictName.Text = item.SubItems[0].Text;
                    txtDistrictStaff.Text = item.SubItems[1].Text;
                    txtDistrictDescription.Text = item.SubItems[2].Text;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        #endregion Events

        #region User Methods
        private void LoadSubCompanyRegion()
        {
            try
            {
                int iSelectedIndex = 0;
                lvRegions.Items.Clear();
                _lstSubCompayRegions = _objSiteAreaBiz.GetSubCompayRegions(_AdminSite.Site_ID);
                lvItem = new ListViewItem[_lstSubCompayRegions.Count + 1];
                int iLVIndex = 0;

                lvItem[iLVIndex] = new ListViewItem(new string[] {
                       this.GetResourceTextByKey("Key_NoneHyphen"),
                        this.GetResourceTextByKey("Key_Hyphen"),
                        this.GetResourceTextByKey("Key_Hyphen")
                    });
                
                lvItem[iLVIndex++].Tag = "0";                

                foreach (var item in _lstSubCompayRegions)
                {
                    
                    lvItem[iLVIndex] = new ListViewItem(new string[] {
                        item.Sub_Company_Region_Name,
                        (Convert.ToInt32(item.Staff_ID) > 0) ? item.Staff_Last_Name + ", " + item.Staff_First_Name : "-",
                        item.Sub_Company_Region_Description
                    });

                    if (_AdminSite.Sub_Company_Region_ID == item.Sub_Company_Region_ID)
                        iSelectedIndex = iLVIndex;

                    lvItem[iLVIndex++].Tag = item.Sub_Company_Region_ID;
                }

                lvRegions.Items.AddRange(lvItem);
                lvRegions.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);

                lvRegions.Items[iSelectedIndex].Selected = true;

                if (iSelectedIndex == 0)
                {
                    txtRegionName.Text = string.Empty;
                    txtRegionStaff.Text = string.Empty;
                    txtRegionDescription.Text = string.Empty;
                }
                else
                {
                    txtRegionName.Text = lvRegions.SelectedItems[0].SubItems[0].Text;
                    txtRegionStaff.Text = lvRegions.SelectedItems[0].SubItems[1].Text;
                    txtRegionDescription.Text = lvRegions.SelectedItems[0].SubItems[2].Text;
                }

            }
            catch (Exception ex)
            {
                lvItem = new ListViewItem[1];
                int iLVIndex = 0;

                lvItem[iLVIndex] = new ListViewItem(new string[] {
                        this.GetResourceTextByKey("Key_NoneHyphen"),
                        this.GetResourceTextByKey("Key_Hyphen"),
                        this.GetResourceTextByKey("Key_Hyphen")
                    });

                lvItem[iLVIndex].Tag = "0";
                lvRegions.Items.AddRange(lvItem);
                lvRegions.Items[iLVIndex].Selected = true;
                ExceptionManager.Publish(ex);
            }
        }

        private void LoadAreaDetails()
        {
            try
            {
                int iSelectedIndex = 0;
                int iLVIndex = 0;
                List<SubCompanyAreaEntity> AreaEntities = _AdminSubCompany.GetSubCompanyAreaDetails(Convert.ToInt32(lvRegions.SelectedItems[0].Tag));
                
                lvAreas.Items.Clear();
                
                lvItem = new ListViewItem[AreaEntities.Count + 1];
                
                lvItem[iLVIndex] = new ListViewItem(new string[] {
                       this.GetResourceTextByKey("Key_NoneHyphen"),
                       this.GetResourceTextByKey("Key_Hyphen"),
                       this.GetResourceTextByKey("Key_Hyphen")
                    });

                lvItem[iLVIndex++].Tag = "0";

                foreach (var item in AreaEntities)
                {

                    lvItem[iLVIndex] = new ListViewItem(new string[] {
                        item.Sub_Company_Area_Name,
                        (Convert.ToInt32(item.Staff_ID) > 0) ? item.Staff_Last_Name + ", " + item.Staff_First_Name : "-",
                        item.Sub_Company_Area_Description
                    });

                    if (_AdminSite.Sub_Company_Area_ID == item.Sub_Company_Area_ID)
                        iSelectedIndex = iLVIndex;

                    lvItem[iLVIndex++].Tag = item.Sub_Company_Area_ID;
                }

                lvAreas.Items.AddRange(lvItem);
                lvAreas.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);

                lvAreas.Items[iSelectedIndex].Selected = true;
                
                if (iSelectedIndex == 0)
                {
                    txtAreaName.Text = string.Empty;
                    txtAreaStaff.Text = string.Empty;
                    txtAreaDescription.Text = string.Empty;
                }
                else
                {
                    txtAreaName.Text = lvAreas.SelectedItems[0].SubItems[0].Text;
                    txtAreaStaff.Text = lvAreas.SelectedItems[0].SubItems[1].Text;
                    txtAreaDescription.Text = lvAreas.SelectedItems[0].SubItems[2].Text;
                    
                }

            }
            catch (Exception ex)
            {
                lvItem = new ListViewItem[1];
                int iLVIndex = 0;

                lvItem[iLVIndex] = new ListViewItem(new string[] {
                        this.GetResourceTextByKey("Key_NoneHyphen"),
                       this.GetResourceTextByKey("Key_Hyphen"),
                       this.GetResourceTextByKey("Key_Hyphen")
                    });

                lvItem[iLVIndex].Tag = "0";
                lvAreas.Items.AddRange(lvItem);
                lvAreas.Items[iLVIndex].Selected = true;
                ExceptionManager.Publish(ex);
            }            
        }

        private void LoadDistrictDetails()
        {
            try
            {
                int iSelectedIndex = 0;
                int iLVIndex = 0;
                
                List<SubCompanyDistrictEntity> DistrictEntities = _AdminSubCompany.GetSubCompanyDistrictDetails(Convert.ToInt32(lvAreas.SelectedItems[0].Tag));

                lvDistricts.Items.Clear();

                lvItem = new ListViewItem[DistrictEntities.Count + 1];

                lvItem[iLVIndex] = new ListViewItem(new string[] {
                        this.GetResourceTextByKey("Key_NoneHyphen"),
                        this.GetResourceTextByKey("Key_Hyphen"),
                        this.GetResourceTextByKey("Key_Hyphen")
                    });

                lvItem[iLVIndex++].Tag = "0";

                foreach (var item in DistrictEntities)
                {

                    lvItem[iLVIndex] = new ListViewItem(new string[] {
                        item.Sub_Company_District_Name,
                        (Convert.ToInt32(item.Staff_ID) > 0) ? item.Staff_Last_Name + ", " + item.Staff_First_Name : "-",
                        item.Sub_Company_District_Description
                    });

                    if (_AdminSite.Sub_Company_District_ID == item.Sub_Company_District_ID)
                        iSelectedIndex = iLVIndex;

                    lvItem[iLVIndex++].Tag = item.Sub_Company_District_ID;
                }

                lvDistricts.Items.AddRange(lvItem);
                lvDistricts.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);

                lvDistricts.Items[iSelectedIndex].Selected = true;

                if (iSelectedIndex == 0)
                {
                    txtDistrictName.Text = string.Empty;
                    txtDistrictStaff.Text = string.Empty;
                    txtDistrictDescription.Text = string.Empty;
                }
                else
                {
                    txtDistrictName.Text = lvDistricts.SelectedItems[0].SubItems[0].Text;
                    txtDistrictStaff.Text = lvDistricts.SelectedItems[0].SubItems[1].Text;
                    txtDistrictDescription.Text = lvDistricts.SelectedItems[0].SubItems[2].Text;
                }

            }
            catch (Exception ex)
            {
                lvItem = new ListViewItem[1];
                int iLVIndex = 0;

                lvItem[iLVIndex] = new ListViewItem(new string[] {
                        this.GetResourceTextByKey("Key_NoneHyphen"),
                        this.GetResourceTextByKey("Key_Hyphen"),
                        this.GetResourceTextByKey("Key_Hyphen")
                    });

                lvItem[iLVIndex].Tag = "0";
                lvDistricts.Items.AddRange(lvItem);
                lvDistricts.Items[iLVIndex].Selected = true;
                ExceptionManager.Publish(ex);
            }            
        }
        #endregion User Methods

    }
}