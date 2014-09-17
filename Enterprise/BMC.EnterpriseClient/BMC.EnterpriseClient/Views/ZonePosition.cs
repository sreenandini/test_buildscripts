using Audit.Transport;
using BMC.Common;
using BMC.Common.ExceptionManagement;
using BMC.Common.LogManagement;
using BMC.CoreLib;
using BMC.CoreLib.Diagnostics;
using BMC.EnterpriseBusiness.Business;
using BMC.EnterpriseBusiness.Entities;
using BMC.EnterpriseClient.Helpers;
using BMC.SecurityVB;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;


namespace BMC.EnterpriseClient.Views
{
    public partial class ZonePosition : UserControl, IAdminSite, IControlActivator
    {
        #region Members
        public int _SiteId;
        public string _SiteName;
        public int LatestBarPosID;
        AdminSiteEntity _Entity;
        BZonePosition objBZonePos = new BZonePosition();
        CommonUtility objCU = new CommonUtility();
        ListViewItem lvBPItem = new ListViewItem();
        bool bViewRoutePermission = false;
        bool bViewRouteEditPermission = false;
        private string dateFormat = "dd MMM yyyy";
        int iUserID = 0;
        int? openHourID = null;
        string sUserName = string.Empty;
        bool bNew = false;
     
        #endregion Members

        #region Events
        public ZonePosition()
        {
            InitializeComponent();
            btnBulkCopyPositions.Visible = false;

            // Set Tags for controls
            SetTagProperty();
            this.ResolveResources();
        }
        private void SetTagProperty()
        {
            try
            {
                this.lblName.Tag = "Key_NameMandatory";
                this.btnZoneApply.Tag = "Key_Apply";
                this.btnBulkCopyPositions.Tag = "Key_BulkCopyPositions";
                this.btnZoneDelete.Tag = "Key_DeleteCaption";
                this.btnEditPosition.Tag = "Key_EditPosition";
                this.colGameTitle.Tag = "Key_GameTitle";
                this.lblInstallations.Tag = "Key_InstallationsColon";
                this.chkPromotionEnabled.Tag = "Key_IsPromotionEnabled";
                this.colLocation.Tag = "Key_Location";
                this.btnNewPosition.Tag = "Key_NewPosition";
                this.btnNewZone.Tag = "Key_NewZone";
                this.lblOpeningHours.Tag = "Key_OpeningHoursColon";
                this.colPos.Tag = "Key_Pos";
                this.colRouteName.Tag = "Key_RouteName";
                this.btnRoutePositions.Tag = "Key_RoutePositions";
                this.colType.Tag = "Key_Type";
                this.colZone.Tag = "Key_Zone";
                this.btnZoneTimes.Tag = "Key_ZoneTimes";
                this.lblZones.Tag = "Key_ZonesColon";

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
        private void btnNewPosition_Click(object sender, EventArgs e)
        {
            try
            {
                string BarPositionName;
                //Need to Check
                /////
                int SiteId = _SiteId;
                DateTime Bar_Position_Start_Date = DateAndTime.Now;
                int Depot_ID = 0;
                DateTime Bar_Position_Rent_Past_Date = DateAndTime.Now;
                DateTime Bar_Position_Share_Past_Date = DateAndTime.Now;
                DateTime Bar_Position_Licence_Past_Date = DateAndTime.Now;
                DateTime Bar_Position_Rent_Future_Date = DateAndTime.Now;
                DateTime Bar_Position_Share_Future_Date = DateAndTime.Now;
                DateTime Bar_Position_Licence_Future_Date = DateAndTime.Now;
                bool Bar_Position_Use_Terms = false;
                DateTime Bar_Position_Last_Collection_Date = DateAndTime.Now;
                string Bar_Position_Collection_Rent_Paid_Until = string.Empty;
                string Bar_Position_Price_Per_Play = string.Empty;
                string Bar_Position_Jackpot = string.Empty;
                string Bar_Position_Percentage_Payout = string.Empty;
                  // if (Win32Extensions.ShowQuestionMessageBox(this.GetResourceTextByKey(1, "MSG_UCDEFAULT_TIME")) == DialogResult.No)
                frmInputBox objName = new frmInputBox(this.GetResourceTextByKey(1, "MSG_ZONE_BARPOSNAME"), this.GetResourceTextByKey(1, "MSG_ZONE_NEWPOSITION"));
                //"Please Enter Bar Position Name.","Please enter the number of the new position:"
            
                objName.ShowDialog();
                BarPositionName = objName.TextValue;
                if (!string.IsNullOrEmpty(BarPositionName))
                {
                    if (!Regex.Match(BarPositionName, "^[0-9]+$").Success)
                    {
                       // Win32Extensions.ShowInfoMessageBox(this, "Found invalid characters, Please enter valid Bar Position name");
                        Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_ZONE_INVALIDCHARACTERS"),this.ParentForm.Text);
                        return;
                    }
                    if (CommonBiz.EnsureValidString(BarPositionName) != BarPositionName)
                    {
                        //Win32Extensions.ShowInfoMessageBox(this, "Invalid name entered");
                        Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_ZONE_INVALIDNAME"), this.ParentForm.Text);
                        return;

                    }
                    if (BarPositionName.Length > 3)
                    {
                      //  Win32Extensions.ShowInfoMessageBox(this, "Bar Position Name exceeded 3 Chars. Please enter valid Bar Position");
                        Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_ZONE_NAMEEXCEED"), this.ParentForm.Text);
                        return;
                    }
                    int? iNameExists = 0;
                    int IsBarPositionName = objBZonePos.BCheckNameExist(SiteId, BarPositionName, ref iNameExists);
                    if (iNameExists > 0)
                    {
                       // Win32Extensions.ShowInfoMessageBox(this, "A bar position with that name already exists.");
                        Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_ZONE_POSNAMEEXSITS"), this.ParentForm.Text);
                        return;
                    }
                    else
                    {
                        int? iBarPositionId = 0;
                        //DateTime _DtNow = DateTime.Now;
                        String dateTime = DateTime.Now.ToString(dateFormat);
                        int BarPositionId = objBZonePos.BInsertBarPosition(
                                                                        BarPositionName.PadLeft(3, '0'), //BarPositionName
                                                                        SiteId, //SiteId
                                                                        dateTime,//(string)ExtensionMethods.ToShortUTC(_DtNow), //Bar_Position_Start_Date
                                                                        Depot_ID, //Depot_ID //INC
                                                                        dateTime,//(string)ExtensionMethods.ToShortUTC(_DtNow),//Bar_Position_Rent_Past_Date
                                                                        dateTime,//(string)ExtensionMethods.ToShortUTC(_DtNow),//Bar_Position_Share_Past_Date
                                                                        dateTime,//(string)ExtensionMethods.ToShortUTC(_DtNow),//Bar_Position_Licence_Past_Date
                                                                        dateTime,//(string)ExtensionMethods.ToShortUTC(_DtNow),// Bar_Position_Rent_Future_Date
                                                                        dateTime,//(string)ExtensionMethods.ToShortUTC(_DtNow),//Bar_Position_Share_Future_Date
                                                                        dateTime,//(string)ExtensionMethods.ToShortUTC(_DtNow),//Bar_Position_Licence_Future_Date
                                                                        Bar_Position_Use_Terms,//Bar_Position_Use_Terms //INC
                                                                        dateTime,//(string)ExtensionMethods.ToShortUTC(_DtNow),//Bar_Position_Last_Collection_Date
                                                                        dateTime,//(string)ExtensionMethods.ToShortUTC(_DtNow), //Bar_Position_Collection_Rent_Paid_Until
                                                                        _Entity.Site_Price_Per_Play,// Bar_Position_Price_Per_Play
                                                                        true,// Bar_Position_Price_Per_Play_Default
                                                                        _Entity.Site_Jackpot,// Bar_Position_Jackpot
                                                                        true,
                                                                        _Entity.Site_Percentage_Payout,// Bar_Position_Percentage_Payout
                                                                        true,//Bar_Position_Percentage_Payout_Default
                                                                        _Entity.Access_Key_ID.GetValueOrDefault(),// Access_Key_ID
                                                                        true, //Access_Key_ID_Default
                                                                        _Entity.Terms_Group_ID.GetValueOrDefault(),// Terms_Group_ID
                                                                        true, // Terms_Group_ID_Default
                                                                        ref iBarPositionId
                                                                        );

                        lvBPItem = lvBarPos.Items.Add(iBarPositionId.ToString(), BarPositionName.ToString(), 0);
                        lvBPItem.SubItems.Add("");
                        //GetBarPositionID

                        objBZonePos.InsertNewAuditEntry(ModuleNameEnterprise.AUDIT_POSITION, "Site.Position", "Bar_Position_Name", BarPositionName.PadLeft(3, '0'), iUserID, sUserName, _SiteName);
                        List<BMC.EnterpriseDataAccess.EnterpriseDataContext.rsp_GetLatestBarPositionIDResult> BarPosition;
                        BarPosition = objBZonePos.GetBarPositionID().ToList();
                        foreach (var barpos in BarPosition)
                        {

                            LatestBarPosID = barpos.BarPositionID.GetValueOrDefault();
                            frmAdminBarPos objAdminBarPos = new frmAdminBarPos(barpos.BarPositionID.GetValueOrDefault(), _SiteId, BarPositionName);
                            objAdminBarPos.ShowDialog();
                        }
                        
                    }
                }
                LoadBarPosData(SiteId);
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("*** Zone/Position - New Zone Position ***", LogManager.enumLogLevel.Info);
                ExceptionManager.Publish(ex);
            }

        }
        private void btnEditPosition_Click(object sender, EventArgs e)
        {
            try
            {
                if (lvBarPos.SelectedItems.Count > 0)
                {
                    if (lvBarPos.SelectedItems[0].Name != null)
                    {
                        frmAdminBarPos objAdminBarPos = new frmAdminBarPos(Convert.ToInt32(lvBarPos.SelectedItems[0].Name), _SiteId, lvBarPos.SelectedItems[0].Text);
                        objAdminBarPos.ShowDialog();
                    }
                    LoadBarPosData(_SiteId);
                }
                btnEditPosition.Enabled = false;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
        private void btnBulkCopyPositions_Click(object sender, EventArgs e)
        {
            try
            {
                string FirstBarPos;
                string LastBarPos;

                if (lvBarPos.SelectedItems.Count > 0)
                {
                  //  if (this.ShowQuestionMessageBox("This will create any number of bar positions using the selected one as a template. Do you want to continue?") == DialogResult.Yes)
                    if (Win32Extensions.ShowQuestionMessageBox(this, this.GetResourceTextByKey(1, "MSG_ZONE_ANYNUMBERPOS"), this.ParentForm.Text) == DialogResult.Yes)
                    {
                        //frmInputBox objName = new frmInputBox("Please Enter Bar Position Name.", "Please enter the number of the first new position:");
                        frmInputBox objName = new frmInputBox(this.GetResourceTextByKey(1, "MSG_ZONE_BARPOSNAME"), this.GetResourceTextByKey(1, "MSG_ZONE_NEWPOSITION"));
                        objName.ShowDialog();
                        FirstBarPos = objName.TextValue;
                        if (FirstBarPos.Length > 3)
                        {
                           // Win32Extensions.ShowInfoMessageBox(this, "Bar Position Name exceeded 3 Chars. Please enter valid Bar Position");
                            Win32Extensions.ShowInfoMessageBox(this,this.GetResourceTextByKey(1, "MSG_ZONE_3CHAR"),this.ParentForm.Text);
                            return;
                        }
                        if (!string.IsNullOrEmpty(FirstBarPos))
                        {
                            if (Regex.Match(FirstBarPos, "^[0-9]+$").Success)
                            {
                               // frmInputBox objLastName = new frmInputBox("Please Enter Bar Position Name.", "Please enter the number of the Last new position:");
                                frmInputBox objLastName = new frmInputBox(this.GetResourceTextByKey(1, "MSG_ZONE_BARPOSNAME"), this.GetResourceTextByKey(1, "MSG_ZONE_LASTPOSITION"));                              
                                objLastName.ShowDialog();
                                LastBarPos = objLastName.TextValue;
                                if (LastBarPos.Length > 3)
                                {
                                   // Win32Extensions.ShowInfoMessageBox(this, "Bar Position Name exceeded 3 Chars. Please enter valid Bar Position");
                                    Win32Extensions.ShowInfoMessageBox(this,this.GetResourceTextByKey(1, "MSG_ZONE_3CHAR"),this.ParentForm.Text);
                                    return;
                                }
                                if (!string.IsNullOrEmpty(LastBarPos))
                                {
                                    if (Regex.Match(LastBarPos, "^[0-9]+$").Success)
                                    {
                                        if (Convert.ToInt64(LastBarPos) > Convert.ToInt64(FirstBarPos))
                                        {
                                            long x;

                                            Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_ZONE_BARPOSWARNING"), this.ParentForm.Text);

                                            for (x = Convert.ToInt64(FirstBarPos); x < Convert.ToInt64(LastBarPos); x++)
                                            {
                                                int? iNameExists = 0;
                                                int IsBarPositionName = objBZonePos.BCheckNameExist(_Entity.Site_ID, x.ToString("000"), ref iNameExists);
                                                if (iNameExists > 0)
                                                {
                                                    //Win32Extensions.ShowInfoMessageBox(this, "A bar position with that name: " + x + " already exists. so, it could not be created");--doubt
                                                    LogManager.WriteLog("Skipped BarPositions-->" + x.ToString("000"), LogManager.enumLogLevel.Info);
                                                    continue;
                                                }
                                            }
                                            List<BMC.EnterpriseDataAccess.EnterpriseDataContext.rsp_CreateBarPositionTemplateResult> InsertBulkCopy;
                                            InsertBulkCopy = objBZonePos.BBulkCopyPosition(Convert.ToInt32(lvBarPos.SelectedItems[0].Name), FirstBarPos.PadLeft(3, '0'), LastBarPos.PadLeft(3, '0')).ToList();
                                            //Refresh the BarPosition Data
                                            LoadBarPosData(_SiteId);
                                            //Insert into Export History
                                            int ExportHistory = objBZonePos.BExportHistory(_Entity.Site_ID.ToString(), "SITESETUP", _Entity.Site_ID);
                                            // mdlAuditing.AddMeAudit mdlAuditing.AUDIT_SITE, "Site.Position", "Bar_Position_Name", Format$(i, "000"), "Create Bulk Positions"
                                            objBZonePos.InsertNewAuditEntry(ModuleNameEnterprise.AUDIT_POSITION, "Site.BulkCopyPosition", "Bar_Position_Name", " Bar_Position " + FirstBarPos.PadLeft(3, '0') + " to " + LastBarPos.PadLeft(3, '0') + "", iUserID, sUserName, _SiteName);
                                        }
                                        else
                                        {
                                          //  Win32Extensions.ShowInfoMessageBox(this, "Invalid position numbers");
                                            Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_ZONE_INVALIDPOS"),this.ParentForm.Text);
                                            return;
                                        }
                                    }
                                    else
                                    {
                                        //Win32Extensions.ShowInfoMessageBox(this, "Found invalid characters, Please enter valid Bar Position name");
                                        Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_ZONE_INVALIDCHARACTERS"), this.ParentForm.Text);
                                        return;
                                    }
                                }
                            }
                            else
                            {
                               // Win32Extensions.ShowInfoMessageBox(this, "Found invalid characters, Please enter valid Bar Position name");
                                Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_ZONE_INVALIDCHARACTERS"), this.ParentForm.Text);
                                return;
                            }
                        }
                    }
                }
                else
                {
                 //   Win32Extensions.ShowInfoMessageBox(this, "Please select a Bar Position to use as a template");
                    Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_ZONE_SELECTPOS"), this.ParentForm.Text);
                    return;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Bulk Copy Bar Position : " + ex.Message, LogManager.enumLogLevel.Info);
                ExceptionManager.Publish(ex);
            }
        }
        private void btnNewZone_Click(object sender, EventArgs e)
        {
            try
            {
                //Insert New Zone
                txtZoneName.Text = "[" + this.GetResourceTextByKey("Key_NewZone").Replace("&","") + "]";
                btnZoneApply.Enabled = true;
                txtZoneName.Enabled = true;
                lstZoneOpeningHours.Enabled = true;
                bNew = true;
                txtZoneName.Focus();
                lstZoneOpeningHours.SelectedValue = -1;
               // this.ShowInfoMessageBox("Please Click Apply Button to Save Zone Details");
                Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_ZONE_APPLYBUTTON"), this.ParentForm.Text);
                btnNewZone.Enabled = false;
                btnZoneDelete.Enabled = btnZoneTimes.Enabled = false;


                string strOutput = string.Empty;
                CommonBiz.GetSiteSetting(_SiteId, "IsPromotionalTicketEnabled", ref strOutput);
                bool pstatus = false;
                chkPromotionEnabled.Checked = chkPromotionEnabled.Enabled = bool.TryParse(strOutput, out pstatus) ? pstatus : false;


            }
            catch (Exception ex)
            {
                btnNewZone.Enabled = true;
                ExceptionManager.Publish(ex);
            }
        }
        private void btnZoneApply_Click(object sender, EventArgs e)
        {
            string aZoneName = string.Empty;
            bool bNameExists = false;
            bool bPromotionEnabled = chkPromotionEnabled.Checked;
            try
            {
                if (txtZoneName.Text.Trim().Length > 0)
                {
                    string ZoneName = txtZoneName.Text.ToUpper().Trim();
                    int? ZoneID = 0;

                    List<ZoneEntity> dtZone = objBZonePos.GetZones(_SiteId);

                    if (lstZoneOpeningHours.SelectedItem != null)
                    {
                        openHourID = Convert.ToInt32(lstZoneOpeningHours.SelectedValue);
                    }

                    Int32 zoneID = bNew ? 0 : Convert.ToInt32(lstZones.SelectedValue);

                    bNameExists = dtZone != null && dtZone.Exists(x => x.Zone_Name.ToUpper().Trim().Equals(ZoneName) && x.Zone_ID != zoneID);

                    if (bNameExists)
                    {
                        // this.ShowInfoMessageBox("Zone name already exists.Please enter valid Zone Name");
                        Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_ZONE_ZONEEXISTS"), this.ParentForm.Text);
                        return;
                    }

                    if (bNew)
                    {
                        objBZonePos.BInsertZone(_Entity.Site_ID, openHourID, txtZoneName.Text, bPromotionEnabled, ref ZoneID);
                        if (ZoneID > 0)
                            objBZonePos.InsertNewAuditEntry(ModuleNameEnterprise.AUDIT_SITE, "Site.Zone", "Zone_Name", txtZoneName.Text, iUserID, sUserName, _SiteName);
                        else
                        {
                            //this.ShowInfoMessageBox("Unable to create new zone. Please try again later");
                            Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_ZONE_TRYAGAIN"), this.ParentForm.Text);
                            return;
                        }
                    }
                    else
                    {
                        aZoneName = ((ZoneEntity)lstZones.SelectedItem).Zone_Name;
                        objBZonePos.BInsertZoneName(txtZoneName.Text, Convert.ToInt32(lstZones.SelectedValue.ToString()), _Entity.Site_ID, bPromotionEnabled, openHourID);
                        int ExportHistory = objBZonePos.BExportHistory(this._SiteId.ToString(), "SITESETUP", this._SiteId);
                        if (aZoneName != txtZoneName.Text)
                            objBZonePos.Audit(ModuleNameEnterprise.AUDIT_SITE, "ZoneName ", "Site.Zone", aZoneName, "Zone_Name", aZoneName, txtZoneName.Text, iUserID, sUserName, _SiteName);
                    }
                    // this.ShowInfoMessageBox("Zone Details Updated Successfully");
                    Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_ZONE_DETAILSUPDATED"), this.ParentForm.Text);
                    LoadZoneList(_SiteId);
                    bNew = false;
                }
                else
                {
                    //  Win32Extensions.ShowInfoMessageBox(this, "Error updating zone details");
                    Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_ZONE_ERRORUPDATING"), this.ParentForm.Text);
                    bNew = false;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Zone-Apply", LogManager.enumLogLevel.Info);
                ExceptionManager.Publish(ex);
            }
            finally
            {
                btnNewZone.Enabled = true;
                bool isUnAssignedZone = (lstZones.Items.Count > 0) && AppGlobals.Current.HasUserAccess("HQ_Admin_Customers_Zone_Edit");
                btnZoneDelete.Enabled = btnZoneTimes.Enabled = isUnAssignedZone;
            }
        }
        private void btnZoneDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt32(lstZones.SelectedValue.ToString()) > 0)
                {
                   // if (this.ShowQuestionMessageBox("Are you sure you want to delete this zone?") == DialogResult.Yes)
                    if (Win32Extensions.ShowQuestionMessageBox(this, this.GetResourceTextByKey(1, "MSG_ZONE_DELETEZONE"), this.ParentForm.Text) == DialogResult.Yes)
                    {

                        int DeleteZone = objBZonePos.BDeleteZone(int.Parse(lstZones.SelectedValue.ToString()));
                        LoadZoneList(_SiteId);
                    }
                }
                else
                {
                   // Win32Extensions.ShowInfoMessageBox(this, "Error removing zone");
                    Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_ZONE_ERRORREMOVING"), this.ParentForm.Text);
                }
            }
            catch (Exception ex)
            {

                LogManager.WriteLog("Zone-Delete", LogManager.enumLogLevel.Info);
                ExceptionManager.Publish(ex);
            }
            finally
            {
            }

        }
        private void btnZoneTimes_Click(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt32(lstZones.SelectedValue.ToString()) >= 0)
                {
                    if (lstZoneOpeningHours.SelectedIndex == 0)
                    {

                        frmOpeningHours objOpeningHours = new frmOpeningHours(0, Convert.ToInt32(lstZones.SelectedValue.ToString()));
                        objOpeningHours.ShowDialogEx(this);

                    }
                    else
                    {
                       // Win32Extensions.ShowInfoMessageBox(this, "To manually alter the opening times you need to set the Opening Hours to ' --Custom-- '");
                        Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_ZONE_OPENINGHOURS"), this.ParentForm.Text);
                    }
                }
                else
                {
                    //Win32Extensions.ShowInfoMessageBox(this, "Please select zone from the list' --Custom-- '");
                    Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_ZONE_SELECTZONE"), this.ParentForm.Text);
                }
            }
            catch (Exception ex)
            {

                LogManager.WriteLog("Zone-Zone Times", LogManager.enumLogLevel.Info);
                ExceptionManager.Publish(ex);
            }
        }
        private void btnRoutePositions_Click(object sender, EventArgs e)
        {
            string strFileName = Application.StartupPath + @"\BMC.RouteManager.exe";
            try
            {
                if (File.Exists(strFileName))
                {
                    BMCSecurityCallMethod BMCSecurityMethod = new BMCSecurityCallMethod();
                    string delim = "[DELIM]";
                    string strArguements = BMCSecurityMethod.Encrypt(AppGlobals.Current.UserId.ToString()) + delim +
                                                    BMCSecurityMethod.Encrypt(AppGlobals.Current.UserName) + delim +
                                                    BMCSecurityMethod.Encrypt(_SiteId.ToString()) + delim +
                                                    BMCSecurityMethod.Encrypt(_SiteName) + delim + bViewRouteEditPermission;
                    AppEntryPoint.Current.StartProcess(sender, null, strFileName, strArguements, true);
                }
                else
                {
                   // Win32Extensions.ShowInfoMessageBox(this, "The RouteManager package is not currently installed");
                    Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_ZONE_ROUTEMANAGER"), this.ParentForm.Text);
                    return;
                }
            }
            catch (Exception ex)
            {

                ExceptionManager.Publish(ex);
            }
        }
        private void lstZones_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (lstZones.SelectedIndex < 0) return;
                bNew = false;
                if (lstZones.SelectedItem is ZoneEntity)
                {
                    bool isUnAssignedZone = ((ZoneEntity)lstZones.SelectedItem).AssignedZones == 0 && AppGlobals.Current.HasUserAccess("HQ_Admin_Customers_Zone_Edit");
                    btnZoneDelete.Enabled = isUnAssignedZone;
                    txtZoneName.Enabled = isUnAssignedZone;
                }
                if (lstZones.SelectedValue != null)
                {
                    txtZoneName.Text = lstZones.Text;
                    chkPromotionEnabled.Checked = ((ZoneEntity)lstZones.SelectedItem).PromotionEnabled;
                    lstZoneOpeningHours.SelectedValue = ((ZoneEntity)lstZones.SelectedItem).OpenHours;
                }
            }
            catch
            {
            }
        }
        private void lvBarPos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvBarPos.SelectedItems.Count > 0)
            {
                btnEditPosition.Enabled = AppGlobals.Current.HasUserAccess("HQ_Admin_Customers_Bar_Edit");
            }
        }
        #endregion Events

        #region Methods
        public void LoadOpeningHours()
        {

            try
            {
                lstZoneOpeningHours.DataSource = null;

                DataTable dtOpeningHours = objBZonePos.LoadOpeningHours();
                DataRow dr = dtOpeningHours.NewRow();
                dr["Standard_Opening_Hours_Description"] = this.GetResourceTextByKey("Key_CustomHyphen");
                dr["Standard_Opening_Hours_ID"] = 0;
                dtOpeningHours.Rows.InsertAt(dr, 0);
                if (dtOpeningHours.Rows.Count > 0)
                {
                    lstZoneOpeningHours.DataSource = dtOpeningHours;
                }
                lstZoneOpeningHours.DisplayMember = "Standard_Opening_Hours_Description";
                lstZoneOpeningHours.ValueMember = "Standard_Opening_Hours_ID";
                lstZoneOpeningHours.SelectedValue = -1;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

        }
        private void LoadZoneList(int SiteID)
        {
            try
            {

                List<ZoneEntity> dtZone = objBZonePos.GetZones(SiteID);
                if (dtZone.Count > 0)
                {
                    lstZones.DataSource = dtZone;
                    lstZones.DisplayMember = "Zone_Name";
                    lstZones.ValueMember = "Zone_ID";
                    lstZones.SelectedIndex = lstZones.Items.Count - 1;
                    txtZoneName.Text = lstZones.Text;
                    chkPromotionEnabled.Checked = ((ZoneEntity)lstZones.SelectedItem).PromotionEnabled;
                    lstZoneOpeningHours.SelectedValue = ((ZoneEntity)lstZones.SelectedItem).OpenHours;
                }
                else
                {
                    lstZones.DataSource = null;
                    txtZoneName.Text = string.Empty;
                    chkPromotionEnabled.Checked = false;
                    lstZoneOpeningHours.SelectedValue = -1;
                }

                bool isUnAssignedZone = (lstZones.Items.Count > 0) && AppGlobals.Current.HasUserAccess("HQ_Admin_Customers_Zone_Edit");

                btnZoneDelete.Enabled = btnZoneTimes.Enabled = isUnAssignedZone;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
        private void LoadBarPosData(int SiteID)
        {

            lvBarPos.Items.Clear();


            List<BMC.EnterpriseDataAccess.EnterpriseDataContext.rsp_GetBarPositionDetailsBySiteIDResult> lstBarPositionEntity;

            lstBarPositionEntity = objBZonePos.BGetBarPositionDetailsBySiteID(SiteID).ToList();

            int OldBarPositionID = 0;
            foreach (var BarPosition in lstBarPositionEntity)
            {
                if (OldBarPositionID != BarPosition.Bar_Position_ID)
                {

                    lvBPItem = lvBarPos.Items.Add(BarPosition.Bar_Position_ID.ToString(), BarPosition.Bar_Position_Name, 0);

                    lvBPItem.SubItems.Add(BarPosition.Bar_Position_Location);
                    if (objCU.VerifyValidNumberLong(BarPosition.Zone_ID.ToString()) > 0)
                    {

                        lvBPItem.SubItems.Add(BarPosition.Zone_Name);
                    }
                    else
                    {
                        lvBPItem.SubItems.Add("-");
                    }

                    if ((BarPosition.Installation_ID != null) && string.IsNullOrEmpty(BarPosition.Installation_End_Date))
                    {
                        lvBPItem.SubItems.Add(BarPosition.Machine_Type_Code);
                        lvBPItem.SubItems.Add(string.Concat(BarPosition.Machine_Name, "[", BarPosition.Machine_BACTA_Code, "]", BarPosition.Machine_Stock_No));
                    }
                    else
                    {

                        lvBPItem.SubItems.Add("-");
                        lvBPItem.SubItems.Add("-");
                    }

                    if ((BarPosition.Installation_ID != null) && !(string.IsNullOrEmpty(BarPosition.Route_Name)))
                    {
                        lvBPItem.SubItems.Add(BarPosition.Route_Name);
                    }
                    else
                    {
                        lvBPItem.SubItems.Add("-");
                    }
                    OldBarPositionID = BarPosition.Bar_Position_ID;
                }
            }
            if (lvBarPos.Items.Count > 0) btnBulkCopyPositions.Visible = true;
        }
        #endregion

        #region IAdminSite Members
        void IAdminSite.LoadDetails(AdminSiteEntity entity)
        {
            try
            {
                _Entity = entity;
                _SiteId = entity.Site_ID;
                _SiteName = entity.Site_Name;

                iUserID = AppGlobals.Current.UserId;
                sUserName = AppGlobals.Current.UserName;

                if (_SiteId != 0)
                {
                    EnableControls(AppGlobals.Current.HasUserAccess("HQ_Admin_Customers_Site_Edit"));
                }
                else
                {
                    EnableControls(false);
                }
                LogManager.WriteLog("Zone/Position - Starts Inside load Details", LogManager.enumLogLevel.Info);
                _SiteId = entity.Site_ID;
                LogManager.WriteLog("Zone/Position - Ends load Details", LogManager.enumLogLevel.Info);
                LoadBarPosData(entity.Site_ID);
                LoadOpeningHours();
                openHourID = Convert.ToInt32(lstZoneOpeningHours.SelectedValue);
                LoadZoneList(entity.Site_ID);

              

                string strOutput = string.Empty;
                CommonBiz.GetSiteSetting(_SiteId, "IsPromotionalTicketEnabled", ref strOutput);
                bool pstatus = false;

                chkPromotionEnabled.Checked = chkPromotionEnabled.Enabled = bool.TryParse(strOutput, out pstatus) ? pstatus: false;
                 
                

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        void EnableControls(bool isEnable)
        {
            grpButtons.Enabled = isEnable;
            grpInstallations.Enabled = isEnable;
            grpZones.Enabled = isEnable;
            grpZoneButtons.Enabled = isEnable;
            grpButtons.Enabled = isEnable;
            if (!AppGlobals.Current.HasUserAccess("HQ_Admin_Customers_Zone"))
            {
                grpZones.Enabled = false;
                grpZoneButtons.Enabled = false;
            }
            if (!AppGlobals.Current.HasUserAccess("HQ_Admin_Customers_Bar"))
            {
                grpButtons.Enabled = false;
                grpInstallations.Enabled = false;
            }
            if (!AppGlobals.Current.HasUserAccess("HQ_Admin_Customers_Bar"))
            {
                grpButtons.Enabled = false;
                grpInstallations.Enabled = false;
            }
            if (!AppGlobals.Current.HasUserAccess("HQ_Admin_Customers_Bar_BulkCopyPositions"))
            {
                btnBulkCopyPositions.Enabled = false;
            }
            if (!AppGlobals.Current.HasUserAccess("HQ_Admin_Customers_Bar_New"))
            {
                btnNewPosition.Enabled = false;
            }
            if (!AppGlobals.Current.HasUserAccess("HQ_Admin_Customers_Bar_Edit"))
            {
                btnEditPosition.Enabled = false;
            }
            if (!AppGlobals.Current.HasUserAccess("HQ_Admin_Customers_Zone_Edit"))
            {
                btnZoneApply.Enabled = false;
                btnZoneDelete.Enabled = false;
            }
            if (!AppGlobals.Current.HasUserAccess("HQ_Admin_Customers_Zone_New"))
            {
                btnNewZone.Enabled = false;
            }

            bViewRoutePermission = AppGlobals.Current.HasUserAccess("HQ_Admin_Routes");
            
            btnRoutePositions.Enabled = bViewRoutePermission;

            bViewRouteEditPermission = AppGlobals.Current.HasUserAccess("HQ_Admin_Routes_Edit");
        }
        bool IAdminSite.SaveDetails(AdminSiteEntity entity)
        {
            return true;
        }
        #endregion

        #region IControlActivator Members

        void IControlActivator.ActivateControl(object input)
        {
            ModuleProc PROC = new ModuleProc("ucAdminSite", "ActivateControl");

            try
            {
                OrganisationInput org = input as OrganisationInput;
                if (org != null)
                {
                    ListViewItem[] items = lvBarPos.Items.Find(org.BarPositionId.ToString(), true);
                    if (items != null && items.Length > 0)
                    {
                        items[0].Selected = true;
                        items[0].EnsureVisible();
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        #endregion

        private void lvBarPos_DoubleClick(object sender, EventArgs e)
        {
            if (btnEditPosition.Enabled)
            {
                btnEditPosition_Click(sender, e);
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void ZonePosition_Load(object sender, EventArgs e)
        {
            this.ResolveResources();
        }
    }
}
