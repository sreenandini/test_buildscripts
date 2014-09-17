using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BMC.EnterpriseBusiness.Business;
using BMC.EnterpriseBusiness.Entities;
using BMC.Common.LogManagement;
using System.Xml;
using System.Xml.Linq;
using System.Windows.Documents;
using BMC.Common.Utilities;
using BMC.Common.ExceptionManagement;
using BMC.CoreLib.Win32;
using Audit.Transport;
using BMC.Common;


namespace BMC.EnterpriseClient.Views
{
    public partial class frmDepot : BMC.EnterpriseClient.Helpers.BMCExtendedForm
    {
        #region Private Variables
        //
        private const string ServiceButtonCaption = "Service Areas";
        private const string DepotButtonCaption = "Depots";
        private const string SiteRepButtonCaption = "Site Rep's";
        private bool bServiceAreasChecked = false;
        bool CheckDepotEditPermission = AppGlobals.Current.HasUserAccess("HQ_Admin_Depot_Edit");
        int iUserID = 0;
        readonly int NEW_DEPOT = 99;
        int _result = 0;
        string strUserName = "";
        ToolTip ttSiteRep = new ToolTip();
        BMC.EnterpriseClient.Helpers.Datawatcher objDatawatcher = null;

        SiteDetails objdetails = new SiteDetails();
        List<DepotEntity> lstDepotDetails = null;
        List<ServiceAreaEntity> lstServiceArea = null;
        List<SiteRepresentativeEntity> lstSiteRep = null;
        private int? _OperatorID;
        private int? _DepotID;
        //
        #endregion Private Variables

        #region Constructor
        public frmDepot()
        {
            InitializeComponent();
            iUserID = AppGlobals.Current.UserId;
            strUserName = AppGlobals.Current.UserName;
            objDatawatcher = new Helpers.Datawatcher(this,
                 (w, f) =>
                 {
                     w.RemoveControlFromWatcher((f as frmDepot).cmbDepot);
                     w.RemoveControlFromWatcher((f as frmDepot).cmbServiceArea);
                     w.RemoveControlFromWatcher((f as frmDepot).cmbSupplier);
                     //w.RemoveControlFromWatcher((f as frmDepot).chkServiceDepot);
                 });
            SetTagProperty();//Externalization changes

        }
        private void SetTagProperty()
        {
            try
            {
                this.btnClose.Tag = "Key_CloseCaption";
                this.btnNew.Tag = "Key_NewCaption";
                this.btnUpdate.Tag = "Key_UpdateCaption";
                this.gpbServiceArea.Tag = "Key_ServiceArea";
                this.clmReps.Tag = "Key_SiteRepresentatives";
                this.chkServiceDepot.Tag = "Key_DepotIsServiceDepotMsg";
                this.Tag = "Key_Depot";

            }
            catch (Exception ex)
            {
            }
        }
        public frmDepot(int? OperatorID, int? DepotID)
            : this()
        {
            //InitializeComponent();
            _OperatorID = OperatorID;
            _DepotID = DepotID;

            //iUserID = AppGlobals.Current.UserId;
            //strUserName = AppGlobals.Current.UserName;
            //objDatawatcher = new Helpers.Datawatcher(this,
            //    (w,f) =>
            //    {
            //        w.RemoveControlFromWatcher((f as frmDepot).cmbDepot);
            //        w.RemoveControlFromWatcher((f as frmDepot).cmbServiceArea);
            //        w.RemoveControlFromWatcher((f as frmDepot).cmbSupplier);
            //        w.RemoveControlFromWatcher((f as frmDepot).chkServiceDepot);
            //
            //       
            //    });  

        }

        #endregion Constructor

        #region Data Load Methods

          //Load Operator in cmbSupplier
          private void LoadOperator()
          {
              try
              {
                  LogManager.WriteLog("Inside Load Operator", LogManager.enumLogLevel.Info);
                  if (AppEntryPoint.Current.CustomerAccessViewAllDepot == true || AppGlobals.Current.HasUserAccess("HQ_Customer_Access_View_Entire_Database"))
                  {
                      cmbSupplier.DataSource = null;
                      cmbSupplier.DisplayMember = "Operator_Name";
                      cmbSupplier.ValueMember = "Operator_ID";
                      List<OperatorEntity> lst_Operator = DepotBusiness.CreateInstance().Depot_LoadOperator();
                      cmbSupplier.DataSource = lst_Operator;
                      if (cmbSupplier.Items.Count > 0)
                      {
                          cmbSupplier.SelectedIndex = 0;
                          if (cmbDepot.Items.Count > 0)
                          {
                              cmbDepot.SelectedIndex = 0;
                              enabledisable(true);
                          }
                      }
                  }
                  else
                  {
                      cmbSupplier.DataSource = null;
                      cmbSupplier.DisplayMember = "Operator_Name";
                      cmbSupplier.ValueMember = "Operator_ID";
                      cmbSupplier.DataSource = objdetails.GetStaffCustomerAccessServiceOperator(AppEntryPoint.Current.StaffId);
                  }
              }
              catch (Exception ex)
              {
                  ExceptionManager.Publish(ex);
              }
          }

          //load depot in cmbDepot based on the operator selected
          private void LoadDepot()
          {
              try
              {
                  if (AppEntryPoint.Current.CustomerAccessViewAllDepot == true || AppGlobals.Current.HasUserAccess("HQ_Customer_Access_View_Entire_Database"))
                  {
                      cmbDepot.DataSource = null;
                      cmbDepot.Items.Clear();
                      cmbDepot.DisplayMember = "Depot_Name";
                      cmbDepot.ValueMember = "Depot_ID";
                      cmbDepot.DataSource = DepotBusiness.CreateInstance().Depot_LoadDepot(Convert.ToInt32(cmbSupplier.SelectedValue));
                      if (lstDepotDetails != null && _DepotID.HasValue)
                      {
                          //int ind = Lstdepotdetails.FindIndex(obj => obj.Depot_ID == _DepotID);
                          //cmbDepot.SelectedIndex = ind;
                          this.cmbServiceArea.TextChanged += new System.EventHandler(this.cmbServiceArea_TextChanged);
                          cmbDepot.SelectedIndex = 0;
                          _DepotID = null;
                      }
                  }
                  else
                  {
                      cmbDepot.DataSource = null;
                      cmbDepot.Items.Clear();
                      cmbDepot.DisplayMember = "Depot_Name";
                      cmbDepot.ValueMember = "Depot_ID";
                      cmbDepot.DataSource = objdetails.GetCustomerAccessDepot(Convert.ToInt32(cmbSupplier.SelectedValue), AppEntryPoint.Current.StaffId);
                  }
              }
              catch (Exception ex)
              {
                  ExceptionManager.Publish(ex);
              }
          }

          //load depot details based on the depot selected
          private void LoadDepotDetails(int DepotID, int OperatorID)
          {
              try
              {
                  LogManager.WriteLog("LoadDepotDetails()_Loading Depot Details of DepotID & OperatorID :" + DepotID + OperatorID, LogManager.enumLogLevel.Info);
                  List<DepotEntity> ltDepot = DepotBusiness.CreateInstance().Depot_LoadDepotDetails(DepotID, OperatorID);
                  lstDepotDetails = ltDepot;
                  if (ltDepot.Count > 0)
                  {
                      DepotEntity depotdetails = ltDepot[0];
                      txtDepotName.Text = depotdetails.Depot_Name;
                      txtCadastralCode.Text = depotdetails.Depot_Cadastral_Code;
                      txtAddress.Text = depotdetails.Depot_Address;
                      txtPostCode.Text = depotdetails.Depot_Postcode;
                      txtArea.Text = Convert.ToString(depotdetails.Depot_Area);
                      txtStreetNo.Text = depotdetails.Depot_Street_Number;
                      txtPhoneNo.Text = depotdetails.Depot_Phone_Number;
                      txtContactName.Text = depotdetails.Depot_Contact_Name;
                      txtMuncipality.Text = depotdetails.Depot_Municipality;
                      txtDepotReference.Text = depotdetails.Depot_Reference;
                      chkServiceDepot.Checked = depotdetails.Depot_Service.Value;
                      txtProvince.Text = depotdetails.Depot_Province;
                      bServiceAreasChecked = Convert.ToBoolean(depotdetails.Depot_Service.Value);
                      if (!chkServiceDepot.Checked) cmbServiceArea.DataSource = null;
                      //cmbServiceArea.DisplayMember = "";
                      //cmbServiceArea.ValueMember = "";
                  }
              }
              catch (Exception ex)
              {
                  ExceptionManager.Publish(ex);
              }
          }

          private void LoadServiceArea(int depotid)
          {
              try
              {
                  cmbServiceArea.DataSource = null;
                  lstServiceArea = DepotBusiness.CreateInstance().Depot_LoadServiceArea(depotid);
                  cmbServiceArea.DataSource = lstServiceArea;
                  cmbServiceArea.DisplayMember = "Service_Area_Name";
                  cmbServiceArea.ValueMember = "Service_Area_ID";
              }
              catch (Exception ex)
              {
                  ExceptionManager.Publish(ex);
              }
          }

          //load service area details based on the depot selected
          private void LoadDepotServiceAreaDetails(int depotId, int ServiceArea_ID)
          {
              try
              {
                  List<ServiceAreaEntity> lstServiceArea = DepotBusiness.CreateInstance().Depot_LoadServiceAreadetails(depotId, ServiceArea_ID);
                  if (lstServiceArea.Count > 0)
                  {
                      ServiceAreaEntity serviceareadetails = lstServiceArea[0];
                      txtDescription.Text = serviceareadetails.Service_Area_Description;
                      txtNotes.Text = serviceareadetails.Service_Area_Notes;
                  }
              }
              catch (Exception ex)
              {
                  ExceptionManager.Publish(ex);
              }
          }

          //loads sitereps details
          private void LoadDepotSiteRep()
          {
              try
              {
                  List<SiteRepresentativeEntity> ltsiterep = DepotBusiness.CreateInstance().Depot_LoadSiteRep(Convert.ToInt32(cmbDepot.SelectedValue));
                  if (ltsiterep.Count > 0)
                  {
                      lvwSiteReps.Items.Clear();
                      ltsiterep.ForEach(item =>
                      {
                          //lvReps.Items.Add(new RepresentativeSource { UserName = item.UserName, DepotID = item.Depot_ID, StaffID = item.Staff_ID, IsChecked = item.Depot_ID != null ? true : false }, item.Depot_ID != null ? true : false);
                          ListViewItem lvItem = new ListViewItem(item.UserName);
                          lvItem.Checked = item.Depot_ID != null ? true : false;
                          lvItem.Tag = item;
                          lvwSiteReps.Items.Add(lvItem);
                      });

                      //lvReps.DisplayMember = "UserName";
                      //lvReps.ValueMember = "DepotID";

                      lvwSiteReps.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
                  }

              }
              catch (Exception ex)
              {
                  ExceptionManager.Publish(ex);
              }
          }
          //
          class RepresentativeSource
          {
              public string UserName { get; set; }
              public int? DepotID { get; set; }
              public int StaffID { get; set; }
              public bool IsChecked { get; set; }
          }

          #endregion Data Load Methods

        #region Update Methods
          //
          private int UpdateDepotDetails(bool bNew)
          {
              try
              {
                  LogManager.WriteLog("UpdateDepotDetails()_ Updating Depot Details", LogManager.enumLogLevel.Info);
                  string sStaffIds = string.Empty;

                  try
                  {
                      for (int i = 0; i < lvwSiteReps.CheckedItems.Count; i++)
                      {
                          sStaffIds += "," + ((SiteRepresentativeEntity)lvwSiteReps.CheckedItems[i].Tag).Staff_ID.ToString();
                      }
                      if (!String.IsNullOrEmpty(sStaffIds))
                      {
                          sStaffIds = sStaffIds.Remove(0, 1);
                      }
                  }
                  catch (Exception Ex)
                  {
                      ExceptionManager.Publish(Ex);
                  }

                  int result = DepotBusiness.CreateInstance().DepotUpdate(txtDepotName.Text.Trim(),
                                                                          txtAddress.Text.Trim(), txtPostCode.Text.Trim(),
                                                                          Convert.ToInt32(cmbSupplier.SelectedValue),
                                                                          txtCadastralCode.Text.Trim(),
                                                                          txtArea.Text.Trim(),
                                                                          txtStreetNo.Text.Trim(),
                                                                          txtPhoneNo.Text.Trim(),
                                                                          txtProvince.Text.Trim(),
                                                                          txtContactName.Text.Trim(),
                                                                          txtMuncipality.Text.Trim(),
                                                                          txtDepotReference.Text.Trim(),
                                                                          chkServiceDepot.Checked,
                                                                          bNew ? 0 : Convert.ToInt32(cmbDepot.SelectedValue),
                                                                          cmbServiceArea.Text.Trim(),
                                                                          txtDescription.Text.Trim(),
                                                                          txtNotes.Text.Trim(),
                                                                          bNew ? 0 : Convert.ToInt32(cmbServiceArea.SelectedValue),
                                                                          sStaffIds);
                  return result;
              }
              catch (Exception ex)
              {
                  ExceptionManager.Publish(ex);
                  return -1;
              }
          }

          #endregion Update Methods

        #region Validation Methods

          private bool bDepotAlreadyExists(bool NewDepot)
          {
              bool result;
              try
              {
                  result = DepotBusiness.CreateInstance().IsDepotExists(txtDepotName.Text, NewDepot ? 0 : Convert.ToInt32(cmbDepot.SelectedValue), Convert.ToInt32(cmbSupplier.SelectedValue));

              }
              catch (Exception ex)
              {
                  ExceptionManager.Publish(ex);
                  result = false;
              }
              return result;
          }

          private bool bServiceAreaAlreadyExists()
          {
              bool result;
              try
              {
                  LogManager.WriteLog("bServiceAreaAlreadyExists()_ Check Whether the Service Area Already exists", LogManager.enumLogLevel.Info);
                  result = DepotBusiness.CreateInstance().IsServiceAreaExists(cmbServiceArea.Text, Convert.ToInt32(cmbServiceArea.SelectedValue), Convert.ToInt32(cmbDepot.SelectedValue));
              }
              catch (Exception ex)
              {
                  ExceptionManager.Publish(ex);
                  result = false;
              }
              return result;
          }

          #endregion Validation Methods

        #region Miscellaneous Methods
          //
          private void InitializeScreen()
          {
              LogManager.WriteLog("Inside InitializeScreen", LogManager.enumLogLevel.Info);
              try
              {
                  this.cmbServiceArea.TextChanged -= new System.EventHandler(this.cmbServiceArea_TextChanged);
                  LoadOperator();
                  objDatawatcher.DataModify = false;
              }
              catch (Exception ex)
              {
                  ExceptionManager.Publish(ex);
              }
          }
          //
          private void ClearControlContent()
          {
              try
              {
                  LogManager.WriteLog("Inside ClearControlContent", LogManager.enumLogLevel.Info);
                  txtDepotName.Text = string.Empty;
                  txtAddress.Text = string.Empty;
                  txtPhoneNo.Text = string.Empty;
                  txtPostCode.Text = string.Empty;
                  txtContactName.Text = string.Empty;
                  txtDepotReference.Text = string.Empty;
                  chkServiceDepot.Checked = false;
                  txtStreetNo.Text = string.Empty;
                  txtProvince.Text = string.Empty;
                  txtMuncipality.Text = string.Empty;
                  txtCadastralCode.Text = string.Empty;
                  txtArea.Text = string.Empty;
                  txtDescription.Text = string.Empty;
                  txtNotes.Text = string.Empty;
                  cmbDepot.Text = string.Empty;
                  cmbServiceArea.Text = string.Empty;
                  lvwSiteReps.Items.Clear();
              }
              catch (Exception ex)
              {
                  ExceptionManager.Publish(ex);
              }
          }

          #endregion Miscellaneous Methods

        #region Events
        private void frmDepotNew_Load(object sender, EventArgs e)
        {
            try
            {
                InitializeScreen();
                if (!chkServiceDepot.Checked) gpbServiceArea.Enabled = false;
                if (cmbSupplier.Items.Count == 0)
                {
                    Win32Extensions.ShowMessageBox(this, "No Operator Exists to Create a new depot", "BMC Enterprise", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnNew.Enabled = false;
                    btnUpdate.Enabled = false;
                    return;
                }
                if (cmbDepot.Items.Count == 0)
                {
                    enabledisable(false);
                }
                objDatawatcher.DataModify = false;
                this.ResolveResources();//Externalization changes
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void cmbSupplier_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                if (cmbSupplier.SelectedIndex == -1)
                {
                    btnNew.Enabled = btnUpdate.Enabled = false;
                }
                LoadDepot();
                if (cmbDepot.Items.Count == 0)
                {
                    enabledisable(false);
                }
                btnNew.Enabled = btnUpdate.Enabled = CheckDepotEditPermission;
                objDatawatcher.DataModify = false;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void cmbDepot_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ClearControlContent();
                LoadDepotDetails((Convert.ToInt32(cmbDepot.SelectedValue)), (Convert.ToInt32(cmbSupplier.SelectedValue)));
                LoadServiceArea(Convert.ToInt32(cmbDepot.SelectedValue));
                if (cmbServiceArea.Items.Count > 0)
                {
                    LoadDepotServiceAreaDetails((Convert.ToInt32(cmbDepot.SelectedValue)), (Convert.ToInt32(cmbSupplier.SelectedValue)));
                }
                LoadDepotSiteRep();
                enabledisable(true);
                objDatawatcher.DataModify = false;
                LogManager.WriteLog("end of cmbDepsot_SelectedIndexChanged", LogManager.enumLogLevel.Info);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void txtArea_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = e.KeyChar.IsValidDigit();
        }

     

        private void lvReps_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            LogManager.WriteLog("Inside lvReps_ItemCheck", LogManager.enumLogLevel.Info);
            // (lvReps.Items[e.Index] as RepresentativeSource).IsChecked = Convert.ToBoolean(e.NewValue);
        }

        private void ValidText_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = e.KeyChar.IsValidText();
        }

        private void cmbServiceArea_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbServiceArea.Items.Count == 0) return;
                else
                {
                    LoadDepotServiceAreaDetails(Convert.ToInt32(cmbDepot.SelectedValue), Convert.ToInt32(cmbServiceArea.SelectedValue));
                    this.cmbServiceArea.TextChanged += new System.EventHandler(this.cmbServiceArea_TextChanged);
                    objDatawatcher.DataModify = false;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnNew.Tag.ToString() == "Key_NewCaption")
                {
                    ClearControlContent();
                    btnNew.Text = this.GetResourceTextByKey("Key_CancelCaption");     // "C&ancel";
                    btnNew.Tag = "Key_CancelCaption";
                    enabledisable(true);
                    cmbDepot.Enabled = false;
                    cmbSupplier.Enabled = false;
                    txtDepotName.Focus();
                    LoadDepotSiteRep();
                    cmbDepot.DataSource = null;
                    _result = NEW_DEPOT;
                    List<SiteRepresentativeEntity> ltsiterep = DepotBusiness.CreateInstance().Depot_LoadSiteRep(0);
                }
                else
                {
                    btnNew.Text = this.GetResourceTextByKey("Key_NewCaption");  //"Ne&w";
                    btnNew.Tag = "Key_NewCaption";
                    ClearControlContent();
                    cmbSupplier.Enabled = true;
                    cmbDepot.Enabled = true;
                    cmbDepot.Focus();
                    LoadOperator();
                    if (cmbDepot.Items.Count > 0)
                    {
                        cmbDepot.SelectedIndex = 0;
                        enabledisable(false);
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                DepotEntity objUpdatedDepot = new DepotEntity();
                DepotBusiness objDepotbiz = new DepotBusiness();
                if (String.IsNullOrEmpty(txtDepotName.Text.Trim()))
                {
                    Win32Extensions.ShowMessageBox(this, "Please enter the Depot Name", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtDepotName.Focus();
                    return;
                }

                if (String.IsNullOrEmpty(txtAddress.Text.Trim()))
                {
                    Win32Extensions.ShowMessageBox(this, "Please enter the Address", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtAddress.Focus();
                    return;
                }

                if (String.IsNullOrEmpty(txtPostCode.Text.Trim()))
                {
                    Win32Extensions.ShowMessageBox(this, "Please enter the Post/Zip Code", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtPostCode.Focus();
                    return;
                }
                if (bDepotAlreadyExists(btnNew.Tag.ToString() == "Key_CancelCaption"))
                {
                    Win32Extensions.ShowMessageBox(this, "Depot Name" + " " + " ' " + txtDepotName.Text.Trim() + " ' " + " Already Exists-Please Choose a different Depot", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (cmbServiceArea.Text != null)
                {
                    if (bServiceAreaAlreadyExists())
                    {
                        Win32Extensions.ShowInfoMessageBox(this, string.Format(this.GetResourceTextByKey(1, "MSG_DEPOT_SERVICEEXISTS"), cmbServiceArea.Text), this.Text);
                        return;
                    }
                }

                if (btnNew.Tag.ToString() == "Key_CancelCaption")
                    _result = UpdateDepotDetails(true);
                else
                    _result = UpdateDepotDetails(false);

                if (_result == 0)
                {
                    //Auditing Depot Screen Operations
                    try
                    {
                        if (btnNew.Tag.ToString() == "Key_CancelCaption")
                        {

                            objDepotbiz.InsertNewAuditEntry(ModuleNameEnterprise.Depot, "Depot", "Depot_Name", txtDepotName.Text.Trim(), iUserID, strUserName);

                            if (txtCadastralCode.Text.Trim() != "")
                                objDepotbiz.InsertNewAuditEntry(ModuleNameEnterprise.Depot, "Depot", "Depot_Cadastral_Code", txtCadastralCode.Text, iUserID, strUserName);

                            if (txtAddress.Text.Trim() != "")
                                objDepotbiz.InsertNewAuditEntry(ModuleNameEnterprise.Depot, "Depot", "Depot_Address", txtAddress.Text, iUserID, strUserName);

                            if (txtPostCode.Text.Trim() != "")
                                objDepotbiz.InsertNewAuditEntry(ModuleNameEnterprise.Depot, "Depot", "Depot_Postcode", txtPostCode.Text, iUserID, strUserName);

                            if (txtArea.Text.Trim() != "")
                                objDepotbiz.InsertNewAuditEntry(ModuleNameEnterprise.Depot, "Depot", "Depot_Area", txtArea.Text, iUserID, strUserName);

                            if (txtStreetNo.Text.Trim() != "")
                                objDepotbiz.InsertNewAuditEntry(ModuleNameEnterprise.Depot, "Depot", "Depot_Street_Number", txtStreetNo.Text, iUserID, strUserName);

                            if (txtPhoneNo.Text.Trim() != "")
                                objDepotbiz.InsertNewAuditEntry(ModuleNameEnterprise.Depot, "Depot", "Depot_Phone_Number", txtPhoneNo.Text, iUserID, strUserName);

                            if (txtProvince.Text.Trim() != "")
                                objDepotbiz.InsertNewAuditEntry(ModuleNameEnterprise.Depot, "Depot", "Depot_Province", txtProvince.Text, iUserID, strUserName);

                            if (txtContactName.Text.Trim() != "")
                                objDepotbiz.InsertNewAuditEntry(ModuleNameEnterprise.Depot, "Depot", "Depot_Contact_Name", txtContactName.Text, iUserID, strUserName);

                            if (txtMuncipality.Text.Trim() != "")
                                objDepotbiz.InsertNewAuditEntry(ModuleNameEnterprise.Depot, "Depot", "Depot_Municipality", txtMuncipality.Text, iUserID, strUserName);

                            if (chkServiceDepot.Text.Trim() != "")
                                objDepotbiz.InsertNewAuditEntry(ModuleNameEnterprise.Depot, "Depot", "Depot_Service", chkServiceDepot.Text, iUserID, strUserName);

                            if (txtDepotReference.Text.Trim() != "")
                                objDepotbiz.InsertNewAuditEntry(ModuleNameEnterprise.Depot, "Depot", "Depot_Reference", txtDepotReference.Text, iUserID, strUserName);

                            if (chkServiceDepot.Checked)
                            {
                                if (cmbServiceArea.Text.Trim() != "")
                                    objDepotbiz.InsertNewAuditEntry(ModuleNameEnterprise.Depot, "Depot", "Service_Area_Name", cmbServiceArea.Text, iUserID, strUserName);
                                if (txtDescription.Text.Trim() != "")
                                    objDepotbiz.InsertNewAuditEntry(ModuleNameEnterprise.Depot, "Depot", "Service_Area_Description", cmbServiceArea.Text, iUserID, strUserName);
                                if (txtArea.Text.Trim() != "")
                                    objDepotbiz.InsertNewAuditEntry(ModuleNameEnterprise.Depot, "Depot", "Service_Area_Notes", cmbServiceArea.Text, iUserID, strUserName);
                            }
                        }
                        else
                        {
                            objUpdatedDepot.Depot_ID = Convert.ToInt32(cmbDepot.SelectedValue);
                            objUpdatedDepot.Depot_Name = txtDepotName.Text.Trim();
                            objUpdatedDepot.Depot_Cadastral_Code = txtCadastralCode.Text.Trim();
                            objUpdatedDepot.Depot_Address = txtAddress.Text.Trim();
                            objUpdatedDepot.Depot_Postcode = txtPostCode.Text.Trim();
                            objUpdatedDepot.Depot_Area = Convert.ToInt32(txtArea.Text.Trim());
                            objUpdatedDepot.Depot_Street_Number = txtStreetNo.Text.Trim();
                            objUpdatedDepot.Depot_Phone_Number = txtPhoneNo.Text.Trim();
                            objUpdatedDepot.Depot_Province = txtProvince.Text.Trim();
                            objUpdatedDepot.Depot_Contact_Name = txtContactName.Text.Trim();
                            objUpdatedDepot.Depot_Municipality = txtMuncipality.Text.Trim();
                            objUpdatedDepot.Depot_Reference = txtDepotReference.Text.Trim();
                            objUpdatedDepot.Depot_Service = chkServiceDepot.Checked;

                            AuditUpdatedDepotDetails(lstDepotDetails[0], objUpdatedDepot);

                            if (chkServiceDepot.Checked && cmbServiceArea.Text != "" && cmbServiceArea.SelectedIndex >= 0)
                            {
                                ServiceAreaEntity objUpdatedServiceArea = new ServiceAreaEntity();
                                objUpdatedServiceArea.Service_Area_ID = Convert.ToInt32(cmbServiceArea.SelectedValue);
                                objUpdatedServiceArea.Service_Area_Name = cmbServiceArea.Text.Trim();
                                objUpdatedServiceArea.Service_Area_Description = txtDescription.Text.Trim();
                                objUpdatedServiceArea.Service_Area_Notes = txtNotes.Text.Trim();
                                AuditUpdatedServiceDetails(lstServiceArea[0], objUpdatedServiceArea);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Win32Extensions.ShowWarningMessageBox(this, "Error While Auditing Depot Screen Operations");
                        ExceptionManager.Publish(ex);
                    }
                    Win32Extensions.ShowInfoMessageBox("Depot details updated successfully");
                    LoadDepot();
                    return;
                }

                if (_result == 1 || _result == 11)
                {
                    Win32Extensions.ShowErrorMessageBox("Error in updating Depot Details");
                    return;
                }
                if (_result == 2 || _result == 22)
                {
                    Win32Extensions.ShowErrorMessageBox("Error in updating Service Area Details");
                    return;
                }

                if (_result == 3)
                {
                    Win32Extensions.ShowErrorMessageBox("Error in updating Site Representative Details");
                    return;
                }

                if (_result == -1)
                {
                    Win32Extensions.ShowErrorMessageBox("Error in updating Depot Details");
                    return;
                }
            }
            catch (Exception ex)
            {
                Win32Extensions.ShowErrorMessageBox("Exception occured updating Depot Details");
                ExceptionManager.Publish(ex);
            }
            finally
            {
                if (_result != 99)
                {
                    //btnNew.Text = "&New";
                    btnNew.Tag = "Key_NewCaption";
                    cmbDepot.Enabled = cmbSupplier.Enabled = true;
                    LoadDepot();
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            LogManager.WriteLog("End of Depot Screen", LogManager.enumLogLevel.Info);
        }

        private void frmDepotNew_Activated(object sender, EventArgs e)
        {
            cmbSupplier.Focus();
        }

        private void cmbServiceArea_TextChanged(object sender, EventArgs e)
        {
            cmbServiceArea.DataSource = null;
            txtDescription.Text = txtNotes.Text = string.Empty;
        }

        #endregion Events  
     

        private void chkServiceDepot_CheckedChanged(object sender, EventArgs e)
        {
            if (chkServiceDepot.Checked)
            {
                gpbServiceArea.Enabled = true;
                LoadServiceArea(Convert.ToInt32(cmbDepot.SelectedValue));
                if (cmbServiceArea.Items.Count > 0)
                    cmbServiceArea.SelectedIndex = 0;
            }
            else
            {
                gpbServiceArea.Enabled = false;
                cmbServiceArea.DataSource = null;
            }
            objDatawatcher.DataModify = false;
        }

        public void enabledisable(bool result)
        {
            txtDepotName.Enabled = result;
            txtCadastralCode.Enabled = result;
            txtAddress.Enabled = result;
            txtAddress.Enabled = result;
            txtPostCode.Enabled = result;
            txtArea.Enabled = result;
            txtStreetNo.Enabled = result;
            txtPhoneNo.Enabled = result;
            txtProvince.Enabled = result;
            txtContactName.Enabled = result;
            txtMuncipality.Enabled = result;
            chkServiceDepot.Enabled = result;
            txtDepotReference.Enabled = result;
            grb_SiteRep.Enabled = result;
        }

        #region Auditing Depot
        public void AuditUpdatedDepotDetails(DepotEntity objOldData, DepotEntity objNewData)
        {
            try
            {
                if ((objOldData == null) || (objNewData == null)) return;
                DepotBusiness objDepotrbiz = new DepotBusiness();

                if (objOldData.Depot_Name.NullToString() != objNewData.Depot_Name)
                    objDepotrbiz.AuditModifiedDataForDepot(txtDepotName.Text.Trim(), "Depot_Name", objOldData.Depot_Name, objNewData.Depot_Name, iUserID, strUserName);

                if (objOldData.Depot_Cadastral_Code.NullToString() != objNewData.Depot_Cadastral_Code)
                    objDepotrbiz.AuditModifiedDataForDepot(txtCadastralCode.Text.Trim(), "Depot_Cadastral_Code", objOldData.Depot_Cadastral_Code, objNewData.Depot_Cadastral_Code, iUserID, strUserName);

                if (objOldData.Depot_Address.NullToString() != objNewData.Depot_Address)
                    objDepotrbiz.AuditModifiedDataForDepot(txtAddress.Text.Trim(), "Depot_Address", objOldData.Depot_Address, objNewData.Depot_Address, iUserID, strUserName);

                if (objOldData.Depot_Postcode.NullToString() != objNewData.Depot_Postcode)
                    objDepotrbiz.AuditModifiedDataForDepot(txtPostCode.Text.Trim(), "Depot_Postcode", objOldData.Depot_Postcode, objNewData.Depot_Postcode, iUserID, strUserName);

                if (objOldData.Depot_Area != objNewData.Depot_Area)
                    objDepotrbiz.AuditModifiedDataForDepot(txtArea.Text, "Depot_Area", objOldData.Depot_Area.ToString(), objNewData.Depot_Area.ToString(), iUserID, strUserName);

                if (objOldData.Depot_Street_Number.NullToString() != objNewData.Depot_Street_Number)
                    objDepotrbiz.AuditModifiedDataForDepot(txtStreetNo.Text.Trim(), "Depot_Street_Number", objOldData.Depot_Street_Number, objNewData.Depot_Street_Number, iUserID, strUserName);

                if (objOldData.Depot_Phone_Number.NullToString() != objNewData.Depot_Phone_Number)
                    objDepotrbiz.AuditModifiedDataForDepot(txtPhoneNo.Text.Trim(), "Depot_Phone_Number", objOldData.Depot_Phone_Number, objNewData.Depot_Phone_Number, iUserID, strUserName);

                if (objOldData.Depot_Province.NullToString() != objNewData.Depot_Province)
                    objDepotrbiz.AuditModifiedDataForDepot(txtProvince.Text.Trim(), "Depot_Province", objOldData.Depot_Province, objNewData.Depot_Province, iUserID, strUserName);

                if (objOldData.Depot_Contact_Name.NullToString() != objNewData.Depot_Contact_Name)
                    objDepotrbiz.AuditModifiedDataForDepot(txtContactName.Text.Trim(), "Depot_Contact_Name", objOldData.Depot_Contact_Name, objNewData.Depot_Contact_Name, iUserID, strUserName);

                if (objOldData.Depot_Municipality.NullToString() != objNewData.Depot_Municipality)
                    objDepotrbiz.AuditModifiedDataForDepot(txtMuncipality.Text.Trim(), "Depot_Municipality", objOldData.Depot_Municipality, objNewData.Depot_Municipality, iUserID, strUserName);

                if (objOldData.Depot_Reference != objNewData.Depot_Reference)
                    objDepotrbiz.AuditModifiedDataForDepot(txtDepotReference.Text.Trim(), "Depot_Reference", objOldData.Depot_Reference, objNewData.Depot_Reference, iUserID, strUserName);

                //if (objOldData.Depot_Service != objNewData.Depot_Service)
                //    objDepotrbiz.AuditModifiedDataForDepot(chkServiceDepot.Checked.ToString(), "Depot_Service", objOldData.Depot_Service.ToString(), objNewData.Depot_Service.ToString(), iUserID, strUserName);
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        public void AuditUpdatedServiceDetails(ServiceAreaEntity objOldData, ServiceAreaEntity objNewData)
        {
            try
            {
                if ((objOldData == null) || (objNewData == null)) return;
                DepotBusiness objDepotrbiz = new DepotBusiness();

                if (chkServiceDepot.Checked)
                {
                    if (objOldData.Service_Area_Name.NullToString() != objNewData.Service_Area_Name)
                        objDepotrbiz.AuditModifiedDataForDepot(cmbServiceArea.Text, "Service_Area_Name", objOldData.Service_Area_Name, objNewData.Service_Area_Name, iUserID, strUserName);

                    if (objOldData.Service_Area_Description.NullToString() != objNewData.Service_Area_Description)
                        objDepotrbiz.AuditModifiedDataForDepot(txtDescription.Text, "Service_Area_Description", objOldData.Service_Area_Description, objNewData.Service_Area_Description, iUserID, strUserName);

                    if (objOldData.Service_Area_Notes.NullToString() != objNewData.Service_Area_Notes)
                        objDepotrbiz.AuditModifiedDataForDepot(txtArea.Text, "Service_Area_Notes", objOldData.Service_Area_Notes, objNewData.Service_Area_Notes, iUserID, strUserName);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        #endregion
        




    }
}
