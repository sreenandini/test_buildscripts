using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BMC.EnterpriseBusiness.Business;
using BMC.EnterpriseBusiness.Entities;
using BMC.EnterpriseClient.Views;
using BMC.EnterpriseClient.Helpers;
using BMC.Common.LogManagement;
using BMC.EnterpriseClient;
using BMC.Common.ExceptionManagement;
using BMC.Common;
namespace Details2
{
    public partial class Details2UC : UserControl, IAdminSite
    {
        SiteDetails sd = new SiteDetails();
        CommonUtility cuObj = new CommonUtility();
        frmAdminUtilities frmUtil = new frmAdminUtilities();
        //bool CustomerAccessDepotAll = false;
        int _SiteID = 0;
        string _Site_NTP_From = null;
        string _Site_NTP_To = null;
        bool NTPSetStatus = false;
        bool LoadDepot = false;
        int _RepresentativeId = 0;
        //AdminSiteEntity AdSiteEntity ;

        public Details2UC()
        {
            InitializeComponent();
            setTagProperty();
        }

        private void setTagProperty()
        {
            this.btnOpeningTimes.Tag = "Key_Custom";
            this.label2.Tag = "Key_FromColon";
            this.label12.Tag = "Key_InactivationDateColon";
            this.label11.Tag = "Key_MaxVLTColon";
            this.label1.Tag = "Key_NonTradingPeriodColon";
            this.label4.Tag = "Key_OperatorAreaColon";
            this.label5.Tag = "Key_OperatorServiceAreaColon";
            this.lblRepresentitive.Tag = "Key_RepresentativeColon";
            this.label8.Tag = "Key_ServiceAreaColon";
            this.label7.Tag = "Key_ServiceDepotColon";
            this.label6.Tag = "Key_ServiceOperatorColon";
            this.btnSetNTP.Tag = "Key_Set";
            this.chkIsSiteActive.Tag = "Key_SiteActive";
            this.label10.Tag = "Key_SiteConnectionIPAddress";
            this.label9.Tag = "Key_StandingOpeningHoursColon";
            this.label3.Tag = "Key_ToColon";

        }
        
        //public string NTPeriodFrom
        //{
        //    get { return txtNTPFrom.Text; }
        //    set { txtNTPFrom.Text = value; }
        //}
        //public string NTPeriodTo
        //{
        //    get { return txtNTPTo.Text; }
        //    set { txtNTPTo.Text = value; }
        //}
        //public string OperatorArea
        //{
        //    get { return txtSupplierArea.Text.ToString(); }
        //    set { txtSupplierArea.Text = value; }
        //}
        //public string OperatorServiceArea
        //{
        //    get { return txtSupplierServiceArea.Text.ToString(); }
        //    set { txtSupplierServiceArea.Text = value; }
        //}
        //public string ServiceOperator
        //{
        //    get { return CmbServiceSupplier.SelectedItem.ToString(); }
        //    set { CmbServiceSupplier.SelectedItem = value; }
        //}
        //public string ServiceDepot
        //{
        //    get { return CmbServiceDepot.SelectedItem.ToString(); }
        //    set { CmbServiceDepot.SelectedItem = value; }
        //}
        ////public List<AdminSiteEntity> ServiceDepotDS
        ////{            
        ////    set {CmbServiceDepot.DataSource = value;}
        ////}
        //public string ServiceArea
        //{
        //    get { return CmbServiceDepot.SelectedItem.ToString(); }
        //    set { CmbServiceArea.SelectedItem = value; }
        //}
        //public string StdOpeningHrs
        //{
        //    get { return lstOpeningHours.SelectedItem.ToString(); }
        //    set { lstOpeningHours.SelectedItem = value; }
        //}
        //public string SiteConnIP
        //{
        //    get { return txtSiteConnIP.Text.ToString(); }
        //    set { txtSiteConnIP.Text = value; }
        //}
        //public string MaxVlt
        //{
        //    get { return txtMaxVlt.Text.ToString(); }
        //    set { txtMaxVlt.Text = value; }
        //}
        //public DateTime InactivationDate
        //{
        //    get { return DtInactivatedDate.Value; }
        //    set { DtInactivatedDate.Value = value; }
        //}
        //public bool IsSiteActive
        //{
        //    get { return chkIsSiteActive.Checked; }
        //    set { chkIsSiteActive.Checked = value; }
        //}        
              
        //public void ServiceOperatorfromDetail1(int OprID)
        //{
        //    CmbServiceDepot.Refresh();
        //    List<AdminSiteEntity> objDepot = sd.GetDepot(OprID);
        //    CmbServiceDepot.DataSource = objDepot;
        //    CmbServiceDepot.DisplayMember = "Depot_Name";
        //    CmbServiceDepot.ValueMember = "Depot_ID";
        //    CmbServiceDepot.SelectedIndex = -1;
        //}

        public void ServiceOperatorfromDetail1(List<AdminSiteEntity> objCol)
        {
            try
            {
                CmbServiceDepot.Refresh();
                //List<AdminSiteEntity> objDepot = sd.GetDepot(OprID);
                CmbServiceDepot.DataSource = objCol;
                CmbServiceDepot.DisplayMember = "Depot_Name";
                CmbServiceDepot.ValueMember = "Depot_ID";
                CmbServiceDepot.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
        private void Details2UC_Load(object sender, EventArgs e)
        {
            this.ResolveResources();
            DtInactivatedDate.CustomFormat = BMC.Common.Utilities.Common.GetDateFormatByUserSetting();
            dTP_NTPFrom.CustomFormat = BMC.Common.Utilities.Common.GetDateFormatByUserSetting();
            dtp_NTPTo.CustomFormat = BMC.Common.Utilities.Common.GetDateFormatByUserSetting();
        }

        #region IAdminSite Members

        public void LoadDetails(AdminSiteEntity entity)
        {
            try
            {
                _RepresentativeId = Convert.ToInt32(entity.Staff_ID);
                _SiteID = entity.Site_ID;
                OnLoadSiteDetail2();                
                if (_SiteID == 0)
                {
                    tableLayoutPanel1.Enabled = false;
                }
                else
                {
                    tableLayoutPanel1.Enabled = true;
                }
                if (entity.Site_Status_ID == 0)
                {
                    chkIsSiteActive.Checked = true;
                    DtInactivatedDate.Value = Convert.ToDateTime(frmUtil.GetRegionalDate(DateTime.Now.ToString())); //Common Function to be added for RegionalDate
                    DtInactivatedDate.Enabled = false;
                    entity.Site_Inactive_Date = null;
                }
                else
                {
                    chkIsSiteActive.Checked = false;
                    DtInactivatedDate.Enabled = true;
                    if (entity.Site_Inactive_Date != null || entity.Site_Inactive_Date.ToString() != "")
                    {
                        DtInactivatedDate.Value = Convert.ToDateTime(frmUtil.GetRegionalDate(entity.Site_Inactive_Date.ToString())); //Common Function to be added for RegionalDate
                        entity.Site_Inactive_Date = Convert.ToDateTime(frmUtil.GetRegionalDate(entity.Site_Inactive_Date.ToString()));
                    }
                }
                if (entity.Site_Connection_IPAddress == null || entity.Site_Connection_IPAddress == "")
                {
                    txtSiteConnIP.Text = "";
                }
                else
                {
                    txtSiteConnIP.Text = entity.Site_Connection_IPAddress;
                }
                if (entity.Site_MaxNumber_VLT == 0)
                {
                    txtMaxVlt.Text = "0";
                }
                else
                {
                    txtMaxVlt.Text = entity.Site_MaxNumber_VLT.ToString();
                }
                txtSupplierArea.Text = entity.Site_Supplier_Area + "";
                txtSupplierServiceArea.Text = entity.Site_Supplier_Service_Area + "";

                Int64 OpeningHrID;

                OpeningHrID = cuObj.VerifyValidNumberLong(Convert.ToString(entity.Standard_Opening_Hours_ID));
                if (OpeningHrID > 0)
                {
                    frmUtil.setListBox(lstOpeningHours, "", OpeningHrID);
                }

                if (!String.IsNullOrEmpty(entity.Site_Non_Trading_Period_From))
                {
                    dTP_NTPFrom.Checked = true;
                    dTP_NTPFrom.Value = Convert.ToDateTime(frmUtil.GetRegionalDate(entity.Site_Non_Trading_Period_From));
                }
                else
                {
                    dTP_NTPFrom.Checked = false;
                }
                if (!String.IsNullOrEmpty(entity.Site_Non_Trading_Period_To))
                {
                     dtp_NTPTo.Checked = true;
                    dtp_NTPTo.Value = Convert.ToDateTime(frmUtil.GetRegionalDate(Convert.ToString(entity.Site_Non_Trading_Period_To)));
                }
                else
                {
                    dtp_NTPTo.Checked = false;
                }
                if (cuObj.VerifyValidNumberLong(Convert.ToString(entity.Service_Supplier_ID)) > 0)
                {
                    frmUtil.setListBox(CmbServiceSupplier, "", Convert.ToInt64(entity.Service_Supplier_ID));
                }
                else if (cuObj.VerifyValidNumberLong(Convert.ToString(entity.Supplier_ID)) > 0)
                {
                    frmUtil.setListBox(CmbServiceSupplier, "", Convert.ToInt64(entity.Supplier_ID));
                }

                if (cuObj.VerifyValidNumberLong(Convert.ToString(entity.Service_Depot_ID)) > 0)
                {
                    frmUtil.setListBox(CmbServiceDepot, "", Convert.ToInt64(entity.Service_Depot_ID));
                }
                else if (cuObj.VerifyValidNumberLong(Convert.ToString(entity.Depot_ID)) > 0)
                {
                    frmUtil.setListBox(CmbServiceDepot, "", Convert.ToInt64(entity.Depot_ID));
                }

                CmbServiceDepot.SelectedIndex = CmbServiceSupplier.SelectedIndex < 0 ? -1 : CmbServiceDepot.SelectedIndex < 0 ? 0 : CmbServiceDepot.SelectedIndex;

                if (cuObj.VerifyValidNumberLong(Convert.ToString(entity.Service_Area_ID)) > 0)
                {
                    frmUtil.setListBox(CmbServiceArea, "", Convert.ToInt64(entity.Service_Area_ID));
                }
                if (_RepresentativeId != 0)
                {
                    frmUtil.setListBox(cmbLstRep, "", _RepresentativeId);
                }
                entity.Service_Supplier_ID = (entity.Service_Supplier_ID == -1 || entity.Service_Supplier_ID == null) ? 0 : entity.Service_Supplier_ID;
                entity.Supplier_ID = (entity.Supplier_ID == -1 || entity.Supplier_ID == null) ? 0 : entity.Supplier_ID;
                entity.Service_Depot_ID = (entity.Service_Depot_ID == -1 || entity.Service_Depot_ID == null) ? 0 : entity.Service_Depot_ID;
                entity.Service_Area_ID = (entity.Service_Area_ID == -1 || entity.Service_Area_ID == null) ? 0 : entity.Service_Area_ID;
                entity.Sub_Company_Area_ID = (entity.Sub_Company_Area_ID == null) ? 0 : entity.Sub_Company_Area_ID;
                entity.Sub_Company_District_ID = (entity.Sub_Company_District_ID == null) ? 0 : entity.Sub_Company_District_ID;
                entity.Sub_Company_Region_ID = (entity.Sub_Company_Region_ID == null) ? 0 : entity.Sub_Company_Region_ID;
                entity.Staff_ID = (entity.Staff_ID == null) ? 0 : entity.Staff_ID;
                entity.Site_Closed = (entity.Site_Closed == null) ? 0 : entity.Site_Closed;
                entity.Site_MaxNumber_VLT = (entity.Site_MaxNumber_VLT == null) ? 0 : entity.Site_MaxNumber_VLT;
                entity.Standard_Opening_Hours_ID = (entity.Standard_Opening_Hours_ID == null) ? 0 : entity.Standard_Opening_Hours_ID;

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            finally
            {
                if (_SiteID != 0 && !AppGlobals.Current.HasUserAccess("HQ_Admin_Customers_Site_Edit"))
                {
                    tableLayoutPanel1.Enabled = false;
                }
            }
        }

        public void OnLoadSiteDetail2()
        {            
            try
            {
                CmbServiceSupplier.Refresh();
                if (AppEntryPoint.Current.CustomerAccessViewAllDepot == true || AppGlobals.Current.HasUserAccess("HQ_Customer_Access_View_Entire_Database") == true)
                {                
                    List<AdminSiteEntity> SvcOp = sd.GetServiceOperator();
                    if (SvcOp != null)
                    {
                        CmbServiceSupplier.DataSource = SvcOp;
                        CmbServiceSupplier.DisplayMember = "Operator_Name";
                        CmbServiceSupplier.ValueMember = "Operator_ID";
                        CmbServiceSupplier.SelectedIndex = -1;
                        LoadDepot = true;

                    }
                    CmbServiceSupplier.SelectedIndex = -1;
                }
                else if(AppEntryPoint.Current.StaffId != 0)
                {                  
                    List<AdminSiteEntity> SvcOp = sd.GetStaffCustomerAccessServiceOperator(AppEntryPoint.Current.StaffId);
                    if (SvcOp != null)
                    {
                        CmbServiceSupplier.DataSource = SvcOp;
                        CmbServiceSupplier.DisplayMember = "Operator_Name";
                        CmbServiceSupplier.ValueMember = "Operator_ID";
                        CmbServiceSupplier.SelectedIndex = -1;
                        LoadDepot = true;

                    }
                    CmbServiceSupplier.SelectedIndex = -1;
                }
                // Call QFillCombo(CmbServiceSupplier, "SELECT Operator_Name, Operator_ID FROM Operator ORDER BY Operator_Name ASC", "Operator_Name", "Operator_ID")

                List<AdminSiteEntity> StdOpnHrs = sd.GetStdOpeningHours();
                lstOpeningHours.Refresh();
                AdminSiteEntity OpnHrsList = new AdminSiteEntity();
                OpnHrsList.Standard_Opening_Hours_Description = this.GetResourceTextByKey("Key_CustomHyphen");
                if (OpnHrsList != null)
                {
                    StdOpnHrs.Insert(0, OpnHrsList);
                    lstOpeningHours.DataSource = StdOpnHrs;
                    lstOpeningHours.DisplayMember = "Standard_Opening_Hours_Description";
                    lstOpeningHours.ValueMember = "Standard_Opening_Hours_ID";
                }
                lstOpeningHours.SelectedIndex = 0;

                // Call QFillCombo(lstOpeningHours, "SELECT Standard_Opening_Hours_ID, Standard_Opening_Hours_Description FROM Standard_Opening_Hours ORDER BY Standard_Opening_Hours_Description", "Standard_Opening_Hours_Description", "Standard_Opening_Hours_ID", False, False)
                // Call lstOpeningHours.AddItem(" --Custom-- ", 0)
                // lstOpeningHours.ItemData(lstOpeningHours.NewIndex) = 0

                cmbLstRep.Refresh();
                if (_SiteID == 0)
                {
                    cmbLstRep.Text = this.GetResourceTextByKey("Key_NoneHyphen");                    
                }
                else
                {
                    List<GetRepresentativeonSiteResult> RepList = sd.GetRepresentativeonSite(_SiteID);
                    GetRepresentativeonSiteResult InitialVal = new GetRepresentativeonSiteResult();
                    InitialVal.Staff_Full_Name = this.GetResourceTextByKey("Key_NoneHyphen");
                    InitialVal.Staff_ID = 0;
                    if (RepList != null)
                    {
                        RepList.Insert(0, InitialVal);
                        cmbLstRep.DataSource = RepList;
                        cmbLstRep.DisplayMember = "Staff_Full_Name";
                        cmbLstRep.ValueMember = "Staff_ID";
                        cmbLstRep.SelectedIndex = 0;
                    }
                    else
                    {
                        cmbLstRep.Text = this.GetResourceTextByKey("Key_NoneHyphen"); 
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
               

        #endregion

        #region IAdminSite Members

        
        public bool SaveDetails(AdminSiteEntity entity)
        {
            try
            {
                if (!ValidateNonTradingPeriod(entity))
                {
                    return false;
                }
                entity.Site_Non_Trading_Period_From = _Site_NTP_From;
                entity.Site_Non_Trading_Period_To = _Site_NTP_To;
                entity.Site_Connection_IPAddress = txtSiteConnIP.Text.Trim();
                try
                {
                    entity.Site_MaxNumber_VLT = Convert.ToInt32(txtMaxVlt.Text.Trim());
                }
                catch (Exception)
                {
                    entity.Site_MaxNumber_VLT = 0;
                }
                entity.Standard_Opening_Hours_ID = frmUtil.GetItemValue(lstOpeningHours);
                entity.Site_Supplier_Area = txtSupplierArea.Text.Trim();
                entity.Site_Supplier_Service_Area = txtSupplierServiceArea.Text.Trim();
                entity.Service_Area_ID = frmUtil.GetItemValue(CmbServiceArea);
                entity.Service_Depot_ID = frmUtil.GetItemValue(CmbServiceDepot);
                entity.Service_Supplier_ID = frmUtil.GetItemValue(CmbServiceSupplier);

                if (entity.Site_Closed == 1)
                {
                    entity.Site_Status_ID = 1;
                    entity.Site_Inactive_Date = DateTime.Now;
                }
                else if (chkIsSiteActive.Checked)
                {
                    entity.Site_Status_ID = 0;
                    entity.Site_Inactive_Date = null;
                }
                else
                {
                    entity.Site_Status_ID = 1;
                    entity.Site_Inactive_Date = Convert.ToDateTime(frmUtil.GetUniversalDate(DtInactivatedDate.Value));
                }

                entity.Staff_ID = frmUtil.GetItemValue(cmbLstRep);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return false;
            }
            return true; 
        }

        #endregion

        

        private void btnSetNTP_Click(object sender, EventArgs e)
        {
            ValidateNonTradingPeriod(null);
        }

        private bool ValidateNonTradingPeriod(AdminSiteEntity entity)
        {
            bool resultValue = false;
            if (_SiteID != 0)
            {
                if (!dTP_NTPFrom.Checked && !dtp_NTPTo.Checked)
                {
                    _Site_NTP_From = null; //frmUtil.GetUniversalDate(dTP_NTPFrom.Value);
                    _Site_NTP_To = null;// frmUtil.GetUniversalDate(dtp_NTPTo.Value);
                    try
                    {
                        sd.InsertNTPonSite(_SiteID, _Site_NTP_From, _Site_NTP_To);
                        NTPSetStatus = true;
                    }
                    catch (Exception ex)
                    {
                        ExceptionManager.Publish(ex);
                    }
                    resultValue = true;
                }
                else if (dTP_NTPFrom.Checked && dtp_NTPTo.Checked)
                {
                    if (dTP_NTPFrom.Value.CompareTo(dtp_NTPTo.Value) <= 0)
                       
                    {
                        
                            //DialogResult result2 = MessageBox.Show("Would you like to create a zero collection for this period for each machine?", "NTPeriod", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                            if (entity != null && entity.Site_Non_Trading_Period_From == Convert.ToString(frmUtil.GetUniversalDate(dTP_NTPFrom.Value)) && entity.Site_Non_Trading_Period_To == Convert.ToString(frmUtil.GetUniversalDate(dtp_NTPTo.Value)))
                                return true;
                            DialogResult result = Win32Extensions.ShowQuestionMessageBoxCancel(this, this.GetResourceTextByKey(1, "MSG_ENT_UC_DETAILS2_ZERO_COLLECTION"), this.ParentForm.Text);

                            if (result == DialogResult.Yes)
                            {
                                try
                                {
                                    _Site_NTP_From = Convert.ToString(frmUtil.GetUniversalDate(dTP_NTPFrom.Value));
                                    _Site_NTP_To = Convert.ToString(frmUtil.GetUniversalDate(dtp_NTPTo.Value));
                                    sd.InsertNTPonSite(_SiteID, _Site_NTP_From, _Site_NTP_To);
                                    NTPSetStatus = true;

                                    if (result == DialogResult.Yes)
                                    {
                                        int CollDays = 0;
                                        string Remarks = null;
                                        CollDays = Convert.ToInt32((Convert.ToDateTime(_Site_NTP_To) - Convert.ToDateTime(_Site_NTP_From)).TotalDays);
                                        Remarks = "Zero collection for non trading period: " + _Site_NTP_From + " to " + _Site_NTP_To;
                                        sd.NTPZeroCollection(_SiteID, _Site_NTP_From, _Site_NTP_To, CollDays, "00:00", "00:00", 'N', Remarks);
                                        resultValue = true;
                                    }
                                }
                                catch (Exception ex)
                                {
                                    ExceptionManager.Publish(ex);
                                }
                            }
                            else if (result == DialogResult.No || result == DialogResult.Cancel)
                            {
                                dTP_NTPFrom.Checked = false;
                                dtp_NTPTo.Checked = false;
                                dTP_NTPFrom.Focus();
                            }
                    }
                    else 
                    {

                        if (dtp_NTPTo.Value.Date < dTP_NTPFrom.Value.Date)
                        {
                            Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_ENT_UC_DETAILS2_DATE_VALIDATION"), this.ParentForm.Text); 
                            dTP_NTPFrom.Checked = false;
                            dtp_NTPTo.Checked = false;
                            dtp_NTPTo.Focus();
                        }
                        else
                        {
                            Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_ENT_UC_DETAILS2_DATE_VALIDATION1"), this.ParentForm.Text);
                        dTP_NTPFrom.Checked = false;
                        dtp_NTPTo.Checked = false;
                        dTP_NTPFrom.Focus();
                        }

                    }
            }
            else
            {
                Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_ENT_UC_DETAILS2_NON_TRADING"), this.ParentForm.Text);
                    dTP_NTPFrom.Checked = false;
                    dtp_NTPTo.Checked = false;
                    dTP_NTPFrom.Focus();
                }
            }
            else
            {
                resultValue = true;
            }
            return resultValue;
        }

        private void btnOpeningTimes_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstOpeningHours.SelectedIndex == 0)
                {
                    if (_SiteID != 0)
                    {
                        frmOpeningHours OpeningHrs = new frmOpeningHours(_SiteID, 0);
                        OpeningHrs.ShowDialogEx(this);
                    }
                    else
                    {
                        Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_ENT_UC_DETAILS2_SET_TIME"), this.ParentForm.Text);
                    }
                }
                else
                {
                    Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_ENT_UC_DETAILS2_EDIT_TIME"), this.ParentForm.Text);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void CmbServiceSupplier_SelectedIndexChanged(object sender, EventArgs e)
        {        
            try
            {
                if (Win32Extensions.IsInDesignMode()) return;
              //  CmbServiceDepot.Refresh();
               // CmbServiceDepot.DataBindings.Clear();
                CmbServiceSupplier.Refresh();
                if (CmbServiceSupplier.SelectedIndex != -1 && LoadDepot == true)
                {
                    //Call QFillCombo(CmbServiceDepot, "SELECT Depot_Name, Depot_ID FROM Depot WHERE Supplier_ID = " & GetItemData(CmbServiceSupplier) & " ORDER BY Depot_Name ASC", "Depot_Name", "Depot_ID", , True)
                    if(AppEntryPoint.Current.CustomerAccessViewAllDepot == true || AppGlobals.Current.HasUserAccess("HQ_Customer_Access_View_Entire_Database") == true)
                    {
                        List<AdminSiteEntity> objDepot = sd.GetServiceDepot(((AdminSiteEntity)CmbServiceSupplier.SelectedItem).Operator_ID);
                        AdminSiteEntity SvcDepotList = new AdminSiteEntity();
                        SvcDepotList.Depot_Name = this.GetResourceTextByKey("Key_NoneHyphen");
                        if (objDepot != null)
                        {
                            objDepot.Insert(0, SvcDepotList);
                            CmbServiceDepot.DataSource = objDepot;
                            CmbServiceDepot.DisplayMember = "Depot_Name";
                            CmbServiceDepot.ValueMember = "Depot_ID";
                            CmbServiceDepot.SelectedIndex = 0;
                        }
                        //else
                        //{
                        //    CmbServiceDepot.DataBindings.Clear();
                        //    CmbServiceDepot.Refresh();
                        //    CmbServiceDepot.Items.Add(" --None-- ");
                        //    CmbServiceDepot.SelectedIndex = 0;
                        //}
                    }
                    else if (AppEntryPoint.Current.StaffId != 0 && (((AdminSiteEntity)CmbServiceSupplier.SelectedItem).Operator_ID != 0))
                    {
                        List<AdminSiteEntity> objDepot = sd.GetCustomerAccessDepot(((AdminSiteEntity)CmbServiceSupplier.SelectedItem).Operator_ID, AppEntryPoint.Current.StaffId);
                        AdminSiteEntity SvcDepotList = new AdminSiteEntity();
                        SvcDepotList.Depot_Name = this.GetResourceTextByKey("Key_NoneHyphen");
                        if (objDepot != null)
                        {
                            objDepot.Insert(0, SvcDepotList);
                            CmbServiceDepot.DataSource = objDepot;
                            CmbServiceDepot.DisplayMember = "Depot_Name";
                            CmbServiceDepot.ValueMember = "Depot_ID";
                            CmbServiceDepot.SelectedIndex = 0;
                        }
                        else
                        {
                            CmbServiceDepot.Items.Add(this.GetResourceTextByKey("Key_NoneHyphen"));
                            CmbServiceDepot.SelectedIndex = 0;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void CmbServiceDepot_SelectedIndexChanged(object sender, EventArgs e)
        {
            CmbServiceArea.DataSource = null;            
            CmbServiceArea.Items.Clear();
            CmbServiceArea.Refresh();            
            try
            {
                if (CmbServiceDepot.SelectedIndex > -1)
                {
                    if (CmbServiceDepot.SelectedIndex >= 0)
                    {
                        List<AdminSiteEntity> objServArea = null;
                        if(CmbServiceDepot.SelectedValue != null && CmbServiceDepot.SelectedValue is AdminSiteEntity)
                            objServArea = sd.GetServiceArea_Depot(Convert.ToInt32(((AdminSiteEntity)CmbServiceDepot.SelectedValue).Depot_ID));
                        else if(CmbServiceDepot.SelectedValue != null && CmbServiceDepot.SelectedValue is Int32)
                            objServArea = sd.GetServiceArea_Depot(Convert.ToInt32(CmbServiceDepot.SelectedValue));
                        if (objServArea != null)
                        {
                            AdminSiteEntity SvcArea = new AdminSiteEntity();
                            SvcArea.Service_Area_Name = this.GetResourceTextByKey("Key_NoneHyphen");
                            objServArea.Insert(0, SvcArea);
                            CmbServiceArea.DataSource = objServArea;
                            CmbServiceArea.DisplayMember = "Service_Area_Name";
                            CmbServiceArea.ValueMember = "Service_Area_ID";
                            CmbServiceArea.SelectedIndex = 0;
                        }
                        else
                        {
                            CmbServiceArea.Items.Add(this.GetResourceTextByKey("Key_NoneHyphen"));
                            CmbServiceArea.SelectedIndex = 0;
                        }
                    }
                    else
                    {
                        CmbServiceArea.Items.Add(this.GetResourceTextByKey("Key_NoneHyphen"));
                        CmbServiceArea.SelectedIndex = 0;
                    }

                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

        }

        private void chkIsSiteActive_CheckedChanged(object sender, EventArgs e)
        {
            if (chkIsSiteActive.Checked)
            {
                DtInactivatedDate.Enabled = false;
            }
            else
            {
                DtInactivatedDate.Enabled = true;
            }
        }

        private void txtMaxVlt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void txtSiteConnIP_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }
    }
}
