using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BMC.Common.LogManagement;
using BMC.EnterpriseBusiness.Business;
using BMC.EnterpriseBusiness.Entities;
using System.IO;
using BMC.CoreLib.Win32;
using BMC.Common.ExceptionManagement;
using BMC.Common;


namespace BMC.EnterpriseClient.Views
{

    public delegate void ControlLoadDelegate(int ID);

    public partial class ucowner : UserControl, IAdminSite
    {
        private bool _bSubCompanySelected = false;
        int _site_ID = 0;
        int staffId = 0;
        int _CompID = 0;
        int _SubcompanyId = 0;
        int _previousSubcompanyId = 0;
        int _NextSubcompanyId = 0;

        public ucowner()
        {
            InitializeComponent();

            // Set Tags for controls
            SetTagProperty();
        }

        private void SetTagProperty()
        {
            this.lblcompany.Tag  = "Key_CompanyColon";
            this.lblnextcod.Tag  = "Key_NextSubCompanyChangeoverDateColon";
            this.lblnextsubcompany.Tag  = "Key_NextSubCompanyColon";
            this.lblpcod.Tag  = "Key_PreviousSubCompanyChangeoverDate";
            this.lblpsubcompany.Tag  = "Key_PreviousSubCompanyColon";
            this.lblsubcompany.Tag  = "Key_SubCompanyColon";
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ucowner_Load(object sender, EventArgs e)
        {
            // For externalization
            this.ResolveResources();
            dtpicknsucod.CustomFormat = BMC.Common.Utilities.Common.GetDateFormatByUserSetting();
            dtpickpsucod.CustomFormat = BMC.Common.Utilities.Common.GetDateFormatByUserSetting();
        }
        /// <summary>
        /// Load Details
        /// </summary>
        /// <param name="entity"></param>
        public void LoadDetails(AdminSiteEntity entity)
        {

            try
            {
                _site_ID = Convert.ToInt32(entity.Site_ID);
                if (_site_ID == 0)
                {
                    tblowner.Enabled = false;
                }
                else
                {
                    tblowner.Enabled = true;
                }
                _CompID = Convert.ToInt32(entity.Company_ID);
                _SubcompanyId = Convert.ToInt32(entity.Sub_Company_ID);
                _NextSubcompanyId = Convert.ToInt32(entity.Next_Sub_Company_ID);
                _previousSubcompanyId = Convert.ToInt32(entity.Site_Previous_Sub_Company_ID);

                if (entity.Previous_Sub_Company_Change_Date == "")
                {
                    dtpickpsucod.Enabled = false;
                }
                if (entity.Previous_Sub_Company_Change_Date == null)
                {
                    dtpickpsucod.Enabled = false;
                    entity.Previous_Sub_Company_Change_Date = dtpickpsucod.Value.Date.ToString(); ;
                }
                else
                {
                    dtpickpsucod.Enabled = true;
                    if (entity.Previous_Sub_Company_Change_Date == null)
                    {
                    }
                    if (entity.Previous_Sub_Company_Change_Date == "")
                    {
                    }
                    else
                    {
                        dtpicknsucod.Enabled = true;
                        dtpickpsucod.Value = Convert.ToDateTime(entity.Previous_Sub_Company_Change_Date).Date;
                    }
                }

                if (entity.Next_Sub_Company_Change_Date == "")
                {
                    dtpicknsucod.Enabled = false;
                }
                if (entity.Next_Sub_Company_Change_Date == null)
                {
                    dtpicknsucod.Enabled = false;
                    entity.Next_Sub_Company_Change_Date = dtpicknsucod.Value.Date.ToString();
                }

                else
                {
                    if (entity.Next_Sub_Company_Change_Date == null)
                    {

                    }
                    if (entity.Next_Sub_Company_Change_Date == "")
                    {

                    }

                    else
                    {

                        dtpicknsucod.Enabled = true;
                        dtpicknsucod.Value = Convert.ToDateTime(entity.Next_Sub_Company_Change_Date).Date;
                    }
                }
                LogManager.WriteLog("End load Details", LogManager.enumLogLevel.Info);

                staffId = Convert.ToInt32(entity.Staff_ID);
                Loadcompany();
                previousnextSubcompany();
                LogManager.WriteLog("Inside load Details", LogManager.enumLogLevel.Info);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            finally
            {
                if (_site_ID != 0 && !AppGlobals.Current.HasUserAccess("HQ_Admin_Customers_Site_Edit"))
                {
                    tblowner.Enabled = false;
                }
            }
        }
        // <summary>
        /// Load company value
        /// </summary>
        private void Loadcompany()
        {
            try
            {

                LogManager.WriteLog("Inside owner Company", LogManager.enumLogLevel.Info);

                if (AppEntryPoint.Current.CustomerAccessViewAllCompanies == true || AppGlobals.Current.HasUserAccess("HQ_Customer_Access_View_Entire_Database") == true)
                {

                    List<ownerEntity> objEntity = ownerBusiness.CreateInstance().owner_Loadcompany();
                    cmbCompany.DataSource = objEntity;
                    cmbCompany.DisplayMember = "company_Name";
                    cmbCompany.ValueMember = "Company_ID";

                }
                else
                {


                    List<ownerEntity> objEntityowner = ownerBusiness.CreateInstance().GetCustomerAccesstoCompany(staffId);
                    cmbCompany.DataSource = objEntityowner;
                    cmbCompany.DisplayMember = "company_Name";
                    cmbCompany.ValueMember = "Company_ID";
                }
                BMC.EnterpriseClient.Helpers.frmAdminUtilities frmUtobj = new BMC.EnterpriseClient.Helpers.frmAdminUtilities();
                CommonUtility cuobj = new CommonUtility();
                frmUtobj.setListBox(cmbCompany, "", Convert.ToInt64(cuobj.VerifyValidNumberLong(_CompID.ToString())));
                subcompany(0);
                LogManager.WriteLog("End of owner Company", LogManager.enumLogLevel.Info);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
        /// <summary>
        /// Load subcompany  value from Company ID
        /// </summary>
        /// <param name="subvalue"></param>
        private void subcompany(int _companyID)
        {
            try
            {
                LogManager.WriteLog("Inside subcompany", LogManager.enumLogLevel.Info);
                _bSubCompanySelected = false;
                _companyID = (cmbCompany.SelectedItem != null) ? ((ownerEntity)cmbCompany.SelectedItem).Company_ID : 0;
                if (AppEntryPoint.Current.CustomerAccessViewAllCompanies == true || AppGlobals.Current.HasUserAccess("HQ_Customer_Access_View_Entire_Database") == true)
                {
                    List<ownerEntity> objEntity = ownerBusiness.CreateInstance().owner_subcompany(_companyID);
                    cmbSubcompany.DataSource = objEntity;
                    cmbSubcompany.DisplayMember = "company_subcompany";
                    cmbSubcompany.ValueMember = "Sub_Company_ID";
                    cmbSubcompany.SelectedIndex = -1;
                }
                else
                {
                    List<ownerEntity> objEntitysubcompany = ownerBusiness.CreateInstance().GetCustomerAccessSubCompany(_companyID, staffId);
                    cmbSubcompany.DataSource = objEntitysubcompany;
                    cmbSubcompany.DisplayMember = "Sub_Company_Name";
                    cmbSubcompany.ValueMember = "Sub_Company_ID";
                    cmbSubcompany.SelectedIndex = -1;
                }
                              
                
                BMC.EnterpriseClient.Helpers.frmAdminUtilities frmUtobj = new BMC.EnterpriseClient.Helpers.frmAdminUtilities();
                CommonUtility cuobj = new CommonUtility();
                frmUtobj.setListBox(cmbSubcompany, "", Convert.ToInt64(cuobj.VerifyValidNumberLong(_SubcompanyId.ToString())));
                _bSubCompanySelected = true;
                LogManager.WriteLog("End of subcompany", LogManager.enumLogLevel.Info);       
            }
            catch (Exception ex)
            {
               ExceptionManager.Publish(ex);
            }
        }
        
        private void previousnextSubcompany()
        {

            try
            {
                int _companyID = 0;//Convert.ToInt32(cmbCompany.SelectedValue)

                if (AppEntryPoint.Current.CustomerAccessViewAllCompanies == true || AppGlobals.Current.HasUserAccess("HQ_Customer_Access_View_Entire_Database") == true)
                {
                    List<ownerEntity> objEntity2 = ownerBusiness.CreateInstance().owner_psubcompany(_companyID);
                    cmbpreviousSubcompany.DataSource = objEntity2;
                    cmbpreviousSubcompany.DisplayMember = "company_subcompany";
                    cmbpreviousSubcompany.ValueMember = "Sub_Company_ID";
                    cmbpreviousSubcompany.SelectedIndex = -1;
                }
                else
                {
                    List<ownerEntity> objEntitysubcompany = ownerBusiness.CreateInstance().GetCustomerAccessSubCompany(_companyID, staffId);
                    cmbpreviousSubcompany.DataSource = objEntitysubcompany;
                    cmbpreviousSubcompany.DisplayMember = "company_subcompany";
                     cmbpreviousSubcompany.ValueMember = "Sub_Company_ID";
                    cmbpreviousSubcompany.SelectedIndex = -1;
                }
                BMC.EnterpriseClient.Helpers.frmAdminUtilities frmUtobj1 = new BMC.EnterpriseClient.Helpers.frmAdminUtilities();
                CommonUtility cuobj1 = new CommonUtility();
                frmUtobj1.setListBox(cmbpreviousSubcompany, "", Convert.ToInt64(cuobj1.VerifyValidNumberLong(_previousSubcompanyId.ToString())));

                if (AppEntryPoint.Current.CustomerAccessViewAllCompanies == true || AppGlobals.Current.HasUserAccess("HQ_Customer_Access_View_Entire_Database") == true)
                {
                    List<ownerEntity> objEntity3 = ownerBusiness.CreateInstance().owner_nsubcompany(_companyID);
                    cmbNextsubcompany.DataSource = objEntity3;
                    cmbNextsubcompany.DisplayMember = "company_subcompany";
                    cmbNextsubcompany.ValueMember = "Sub_Company_ID";
                    cmbNextsubcompany.SelectedIndex = -1;
                }
                else
                {
                    List<ownerEntity> objEntitysubcompany = ownerBusiness.CreateInstance().GetCustomerAccessSubCompany(_companyID, staffId);
                    cmbNextsubcompany.DataSource = objEntitysubcompany;
                    cmbNextsubcompany.DisplayMember = "company_subcompany";
                    cmbNextsubcompany.ValueMember = "Sub_Company_ID";
                    cmbNextsubcompany.SelectedIndex = -1;
                }
                BMC.EnterpriseClient.Helpers.frmAdminUtilities frmUtobj = new BMC.EnterpriseClient.Helpers.frmAdminUtilities();
                CommonUtility cuobj = new CommonUtility();
                frmUtobj.setListBox(cmbNextsubcompany, "", Convert.ToInt64(cuobj.VerifyValidNumberLong(_NextSubcompanyId.ToString())));
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);

            }
        }
    
        private void cmbpreviousSubcompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbpreviousSubcompany.SelectedIndex >= 0)
                {
                    dtpickpsucod.Enabled = true;

                }
                else
                {
                    dtpickpsucod.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
        private void cmbCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                cmbNextsubcompany.SelectedIndex = -1;
                cmbpreviousSubcompany.SelectedIndex = -1;
                if (cmbCompany.SelectedIndex >= 0)
                subcompany(0);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void cmbNextsubcompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbNextsubcompany.SelectedIndex >= 0)
                {
                    dtpicknsucod.Enabled = true;
                }
                else
                {
                    dtpicknsucod.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
              
        }
        /// <summary>
        /// save details to Entity
        /// </summary>
        /// <param name="entity"></param>

        public bool SaveDetails(AdminSiteEntity entity)
        {
            try
            {
                LogManager.WriteLog("Inside SaveDetails", LogManager.enumLogLevel.Info);


                if (cmbCompany.SelectedIndex >= 0)
                {
                    entity.Company_ID = Convert.ToInt32(cmbCompany.SelectedValue);
                    if (cmbSubcompany.Text == "")
                    {
                        this.ShowErrorMessageBox(this.GetResourceTextByKey(1, "MSG_SL_SELECT_SUBCOMPANY"), this.ParentForm.Text);   //Please Select a SubCompany
                        return false;
                    }
                }
                if (cmbSubcompany.SelectedIndex >= 0)
                    entity.Sub_Company_ID = Convert.ToInt32(cmbSubcompany.SelectedValue);
                if (entity.Previous_Sub_Company_Change_Date == null && entity.Previous_Sub_Company_Change_Date == "")
                {
                    return false;
                }
                else
                {
                    entity.Previous_Sub_Company_Change_Date = Convert.ToString(dtpickpsucod.Value.Date);
                }
                if (entity.Next_Sub_Company_Change_Date == null && entity.Next_Sub_Company_Change_Date == "")
                {
                    return false;
                }
                else
                {
                    entity.Next_Sub_Company_Change_Date = Convert.ToString(dtpicknsucod.Value.Date);
                }
                if (cmbpreviousSubcompany.SelectedIndex >= 0)
                    entity.Site_Previous_Sub_Company_ID = Convert.ToInt32(cmbpreviousSubcompany.SelectedValue);
                if(cmbNextsubcompany.SelectedIndex>= 0)
                    entity.Next_Sub_Company_ID = Convert.ToInt32(cmbNextsubcompany.SelectedValue);
                LogManager.WriteLog("End SaveDetails", LogManager.enumLogLevel.Info);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return false;
            }
            return true;
        }

        private void tblowner_Paint(object sender, PaintEventArgs e)
        {

        }

    }
}
