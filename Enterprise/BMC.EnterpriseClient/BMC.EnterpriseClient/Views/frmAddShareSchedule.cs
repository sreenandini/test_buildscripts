using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Audit.Transport;
using BMC.Common;
using BMC.Common.ExceptionManagement;
using BMC.Common.LogManagement;
using BMC.Common.Utilities;
using BMC.EnterpriseBusiness.Business;
using BMC.EnterpriseBusiness.Entities;
using BMC.EnterpriseClient.Helpers;
using BMC.EnterpriseClient.Helpers.ExtensionsMethods;
using BMC.CoreLib.Win32;

namespace BMC.EnterpriseClient.Views
{
    public partial class frmAddShareSchedule : Form
    {

        #region Variables
        int _shareScheduleId = 0;
        string _scheduleName = string.Empty;
        string _scheduleDescription = string.Empty;
        int _numberOfBands;
        string _bandCountType = string.Empty;
        ShareScheduleBusiness _objShareScheduleBusiness = new ShareScheduleBusiness();
        ShareScheduleEntity _objShareScheduleEntity;

        //audit entities
        ScheduleEntity scheduleSavedInfo;

        BandShareDataEntity bandShareDataSavedInfo;
        BandShareDateEntity bandShareDateSavedInfo;
        BandShareDetailsEntity bandShareDetailsSavedInfo;

        MachineClassDetailsEntity machineClassDetailsSavedInfo;

        List<ShareScheduleEntity> _lstShareBandDetails = null;
        List<ShareScheduleEntity> _lstMachineClass = null;
        ListViewCustomSorter _lvCustomSorter = null;
        bool _editType;

        #endregion

        #region Constructors
        public frmAddShareSchedule()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedDialog;            
            btnApplyBands.Enabled = false;
            btnApplyMachines.Enabled = false;
            btnAddGameTitle.Enabled = false;
            btnRemoveGameTitle.Enabled = false;
            btnMCBandFuturetoCurrent.Enabled = false;
            btnMCBandCurrenttoPast.Enabled = false;
            btnApplyDatesMachines.Enabled = false;
            lvBands.ClipboardCopyMode = ListViewClipboardCopyMode.EnableWithHeaderText;
            lvBands.ClipboardCopyFormat = ListViewClipboardCopyFormat.Semicolon;
            _lvCustomSorter = new ListViewCustomSorter(lvBands, this);
            lvMachines.ClipboardCopyMode = ListViewClipboardCopyMode.EnableWithHeaderText;
            lvMachines.ClipboardCopyFormat = ListViewClipboardCopyFormat.Semicolon;
            _lvCustomSorter = new ListViewCustomSorter(lvMachines, this);
            SetTagProperty();
        }

        public frmAddShareSchedule(bool editType, ShareScheduleEntity objEntity)
            : this()
        {
            _editType = editType;

            if (objEntity != null)
            {
                _objShareScheduleEntity = objEntity;
                this._shareScheduleId = objEntity.Share_Schedule_ID.Value;
                this._scheduleName = objEntity.Share_Schedule_Name;
                this._scheduleDescription = objEntity.Share_Schedule_Description;
                this._numberOfBands = objEntity.Share_Schedule_No_Bands.Value;
                this._bandCountType = objEntity.Share_Schedule_Bands_Name_Type;
            }
        }

        #endregion

        #region Private Methods

        private void SetTagProperty()
        {
            this.tpBands.Tag = "Key_Bands";
            this.tpMachines.Tag = "Key_Machines";
            //this.btnExit.Tag = "Key_ExitCaption1";
            this.chdrBandName.Tag = "Key_BandName";
            this.chdrOperatorPast.Tag = "Key_Operator";
            this.chdrSitePast.Tag = "Key_Site";
            this.chdrCompanyPast.Tag = "Key_Company";
            this.chdrBandPastDate.Tag = "Key_ChangeDate";
            this.chdrOperator.Tag = "Key_Operator";
            this.chdrSite.Tag = "Key_Site";
            this.chdrCompany.Tag = "Key_Company";
            this.chdrBandFutureDate.Tag = "Key_ChangeDate";
            this.chdrOperatorFuture.Tag = "Key_Operator";
            this.chdrSiteFuture.Tag = "Key_Site";
            this.chdrCompanyFuture.Tag = "Key_Company";
            this.tpSchedule.Tag = "Key_Schedule";
            this.btnCancelSchedule.Tag = "Key_Cancel";
            this.btnUpdateSchedule.Tag = "Key_UpdateCaption";
            this.btnEditSchedule.Tag = "Key_EditCaption";
            this.lblName.Tag = "Key_Name";
            this.lblDescription.Tag = "Key_Description";
            this.lblNoofBands.Tag = "Key_NoofBandsColon";
            this.lblBandCountType.Tag = "Key_BandCountTypeColon";
            this.rdbNumeric.Tag = "Key_Numeric";
            this.rdbAlpha.Tag = "Key_Alpha";
            this.lblSupplier.Tag = "Key_Supplier";
            this.lblSite.Tag = "Key_Site";
            this.lblCompany.Tag = "Key_Company";
            this.lblChange.Tag = "Key_ChangeDatesColon";
            this.lblMachineChangeDates.Tag = "Key_ChangeDatesColon";
            this.btnApplyBands.Tag = "Key_Apply";
            this.btnApplyDates.Tag = "Key_ApplyDates";
            this.btnBandsCurrentToPast.Tag = "Key_CurrenttoPast";
            this.btnBandFuturetoCurrent.Tag = "Key_FuturetoCurrent";
            this.chdrGameTitle.Tag = "Key_GameTitle";
            this.chdrPastBand.Tag = "Key_PastBand";
            this.chdrPastChangeDate.Tag = "Key_ChangeDate";
            this.chdrCurrentBand.Tag = "Key_CurrentBand";
            this.chdrFutureChangeDate.Tag = "Key_ChangeDate";
            this.chdrFutureBand.Tag = "Key_FutureBand";
            //this.lblBand.Tag = "Key_Band";
            //this.lblChangeMachine.Tag = "Key_Change";
            this.btnMCBandCurrenttoPast.Tag = "Key_CurrenttoPast";
            this.btnMCBandFuturetoCurrent.Tag = "Key_FuturetoCurrent";
            this.btnExport.Tag = "Key_Export";
            this.btnApplyMachines.Tag = "Key_Apply";
            this.btnApplyDatesMachines.Tag = "Key_ApplyDatesToAll";
            this.btnRemoveGameTitle.Tag = "Key_RemoveGameTitle";
            this.btnAddGameTitle.Tag = "Key_AddGameTitle";
            //this.grpBoxBandCountType.Tag = "Key_BandCountType";
            this.Tag = "Key_ShareSchedule";
            this.gbSchedule.Tag = "Key_Schedule";
            this.gbBandShares.Tag = "Key_Shares";
            this.gbBandDates.Tag = "Key_Dates";
            this.gbSearchGameTitle.Tag = "Key_SearchGameTitle";
            this.gbMachineBands.Tag = "Key_Bands";
            this.gbMachineDates.Tag = "Key_Dates";
            this.lblSearchGame.Tag = "Key_SearchGameTitleColon";
            this.lblMachineBands.Tag = "Key_Bands";
            this.lblMachineBandsPast.Tag = "Key_Past";
            this.lblMachineBandsCurrent.Tag = "Key_Current";
            this.lblMachineBandsFuture.Tag = "Key_Future";
            this.lblPastShares.Tag = "Key_PastSharesPercent";
            this.lblCurrentShares.Tag = "Key_CurrentSharesPercent";
            this.lblFutureShares.Tag = "Key_FutureSharesPercent";
            this.lblPastSharesGuaranteed.Tag = "Key_G";
            this.lblCurrentSharesGuaranteed.Tag = "Key_G";
            this.lblFutureSharesGuaranteed.Tag = "Key_G";
            this.btnBandsClose.Tag = "Key_CloseCaption";
            this.btnClose.Tag = "Key_CloseCaption";
            this.btnScheduleClose.Tag = "Key_CloseCaption";
            this.gbBandDetails.Tag = "Key_Details";
            this.gbMachineDetails.Tag = "Key_Details";

        }

        private void LoadListViewMachines(int shareScheduleId)
        {
            try
            {
                LogManager.WriteLog("Inside LoadListViewMachines...", LogManager.enumLogLevel.Info);
                lvMachines.Items.Clear();
                List<ShareScheduleEntity> lstMachineClass = _objShareScheduleBusiness.GetMachineClassShareBand(shareScheduleId);
                foreach (ShareScheduleEntity entity in lstMachineClass)
                {
                    ListViewItem item = new ListViewItem();
                    item.Tag = entity;
                    item.Text = entity.Machine_Name == null ? "--Default--" : entity.Machine_Name + ((entity.Machine_BACTA_Code != null) ? "[" + entity.Machine_BACTA_Code + "]" : string.Empty);
                    item.SubItems.Add(entity.PastBandName);
                    item.SubItems.Add(CheckEmpty(Convert.ToDateTime(entity.Machine_Class_Share_Past_Date).ToString("dd/MM/yyyy")));
                    item.SubItems.Add(entity.BandName);
                    item.SubItems.Add(CheckEmpty(Convert.ToDateTime(entity.Machine_Class_Share_Future_Date).ToString("dd/MM/yyyy")));
                    item.SubItems.Add(entity.FutureBandName);
                    lvMachines.Items.Add(item);
                }

                cmbMachineBandsPast.DataSource = _lstShareBandDetails.Select(e => new { e.Share_Band_ID, e.Share_Band_Name }).ToList();
                cmbMachineBandsPast.DisplayMember = "Share_Band_Name";
                cmbMachineBandsPast.ValueMember = "Share_Band_ID";
                cmbMachineBandsCurrent.DataSource = _lstShareBandDetails.Select(e => new { e.Share_Band_ID, e.Share_Band_Name }).ToList();
                cmbMachineBandsCurrent.DisplayMember = "Share_Band_Name";
                cmbMachineBandsCurrent.ValueMember = "Share_Band_ID";
                cmbMachineBandsFuture.DataSource = _lstShareBandDetails.Select(e => new { e.Share_Band_ID, e.Share_Band_Name }).ToList();
                cmbMachineBandsFuture.DisplayMember = "Share_Band_Name";
                cmbMachineBandsFuture.ValueMember = "Share_Band_ID";

                btnMCBandFuturetoCurrent.Enabled = lvMachines.Items.Count > 0;
                btnMCBandCurrenttoPast.Enabled = lvMachines.Items.Count > 0;
                btnApplyDatesMachines.Enabled = lvMachines.Items.Count > 0;
                btnApplyMachines.Enabled = false;
                btnRemoveGameTitle.Enabled = false;

                lvMachines.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

        }

        private void LoadListViewBands(int shareScheduleId)
        {
            try
            {
                LogManager.WriteLog("Inside LoadListViewBands...", LogManager.enumLogLevel.Info);
                lvBands.Items.Clear();
                _lstShareBandDetails = _objShareScheduleBusiness.GetShareBandDetails(shareScheduleId);
                foreach (ShareScheduleEntity entity in _lstShareBandDetails)
                {
                    ListViewItem item = new ListViewItem();
                    item.Tag = entity;
                    item.Text = entity.Share_Band_Name;

                    item.SubItems.Add(CheckEmpty(entity.Share_Band_Past_Supplier_Share.ToString()));
                    item.SubItems.Add(CheckEmpty(entity.Share_Band_Past_Site_Share.ToString()));
                    item.SubItems.Add(CheckEmpty((entity.Share_Band_Past_Company_Share.ToString())));
                    item.SubItems.Add(Convert.ToDateTime(entity.Share_Band_Past_End_Date).ToString("dd/MM/yyyy"));
                    item.SubItems.Add(CheckEmpty(entity.Share_Band_Supplier_Share.ToString()));
                    item.SubItems.Add(CheckEmpty(entity.Share_Band_Site_Share.ToString()));
                    item.SubItems.Add(CheckEmpty(entity.Share_Band_Company_Share.ToString()));
                    item.SubItems.Add(Convert.ToDateTime(entity.Share_Band_Future_Start_Date).ToString("dd/MM/yyyy"));
                    item.SubItems.Add(CheckEmpty(entity.Share_Band_Future_Supplier_Share.ToString()));
                    item.SubItems.Add(CheckEmpty(entity.Share_Band_Future_Site_Share.ToString()));
                    item.SubItems.Add(CheckEmpty(entity.Share_Band_Future_Company_Share.ToString()));

                    lvBands.Items.Add(item);
                }
                txtNoofBands.Text = lvBands.Items.Count == 0 ? "1" : lvBands.Items.Count.ToString();
                ListViewItem newitem = new ListViewItem();

                if (lvBands.Items.Count == 0)
                {
                    newitem.Text = (_bandCountType == "A") ? "A" : "1";
                    newitem.Tag = new ShareScheduleEntity() { Share_Schedule_ID = _shareScheduleId, Share_Schedule_Name = txtName.Text, Share_Band_ID = -1, Share_Band_Name = newitem.Text };
                }
                else
                {
                    newitem.Text = (_bandCountType == "A") ? char.ConvertFromUtf32((int)'A' + lvBands.Items.Count) : (1 + lvBands.Items.Count).ToString();
                    newitem.Tag = new ShareScheduleEntity() { Share_Schedule_ID = _shareScheduleId, Share_Schedule_Name = txtName.Text, Share_Band_ID = -1, Share_Band_Name = newitem.Text };
                }
                newitem.SubItems.Add("-");
                newitem.SubItems.Add("-");
                newitem.SubItems.Add("-");
                newitem.SubItems.Add("-");
                newitem.SubItems.Add("-");
                newitem.SubItems.Add("-");
                newitem.SubItems.Add("-");
                newitem.SubItems.Add("-");
                newitem.SubItems.Add("-");
                newitem.SubItems.Add("-");
                newitem.SubItems.Add("-");
                lvBands.Items.Add(newitem);

                txtPastSupplierShare.Text = String.Empty;
                txtPastSiteshare.Text = String.Empty;
                txtPastCompanyshare.Text = String.Empty;
                txtSupplierShare.Text = String.Empty;
                txtSiteshare.Text = String.Empty;
                txtCompanyshare.Text = String.Empty;
                txtFutureSupplierShare.Text = String.Empty;
                txtFutureSiteshare.Text = String.Empty;
                txtFutureCompanyshare.Text = String.Empty;
                chkCompanyShareGuaranteed.Checked = false;
                chkCompanyShareGuaranteedFuture.Checked = false;
                chkCompanyShareGuaranteedPast.Checked = false;
                chkSiteShareGuaranteedFuture.Checked = false;
                chkSiteShareGuaranteed.Checked = false;
                chkSiteShareGuaranteedPast.Checked = false;
                chkSupplierShareGuaranteed.Checked = false;
                chkSupplierShareGuaranteedFuture.Checked = false;
                chkSupplierShareGuaranteedPast.Checked = false;

                btnBandFuturetoCurrent.Enabled = lvBands.Items.Count > 1;
                btnBandsCurrentToPast.Enabled = lvBands.Items.Count > 1;
                btnApplyDates.Enabled = lvBands.Items.Count > 1;
                btnApplyBands.Enabled = false;

                lvBands.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private string CheckEmpty(string value)
        {
            try
            {
                LogManager.WriteLog("Inside CheckEmpty...", LogManager.enumLogLevel.Info);
                int myResult = 0;
                if (int.TryParse(value, out myResult))
                {
                    return myResult != 0 ? String.Format("{0:00.00}", myResult) : "-";
                }
                else if (value.ToString() == "")
                {
                    return "-";
                }
                else
                {
                    return value;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return "-";
            }
        }

        private bool VerifyValidNumber()
        {
            try
            {
                LogManager.WriteLog("Inside VerifyValidNumber...", LogManager.enumLogLevel.Info);
                if (((Convert.ToSingle(txtPastCompanyshare.Text) + Convert.ToSingle(txtPastSiteshare.Text) + Convert.ToSingle(txtPastSupplierShare.Text)) != 100) && ((Convert.ToSingle(txtPastCompanyshare.Text) + Convert.ToSingle(txtPastSiteshare.Text) + Convert.ToSingle(txtPastSupplierShare.Text) != 0)))
                {
                    BMC.CoreLib.Win32.Win32Extensions.ShowInfoMessageBox(null, this.GetResourceTextByKey(1, "MSG_PASTSHARE_TOTAL"));
                    return false;
                }
                else if (((Convert.ToSingle(txtCompanyshare.Text) + Convert.ToSingle(txtSiteshare.Text) + Convert.ToSingle(txtSupplierShare.Text)) != 100) && ((Convert.ToSingle(txtCompanyshare.Text) + Convert.ToSingle(txtSiteshare.Text) + Convert.ToSingle(txtSupplierShare.Text) != 0)))
                {
                    BMC.CoreLib.Win32.Win32Extensions.ShowInfoMessageBox(null, this.GetResourceTextByKey(1, "MSG_CURRENTSHARE_TOTAL"));
                    return false;
                }
                else if (((Convert.ToSingle(txtFutureCompanyshare.Text) + Convert.ToSingle(txtFutureSiteshare.Text) + Convert.ToSingle(txtFutureSupplierShare.Text)) != 100) && ((Convert.ToSingle(txtFutureCompanyshare.Text) + Convert.ToSingle(txtFutureSiteshare.Text) + Convert.ToSingle(txtFutureSupplierShare.Text) != 0)))
                {
                    BMC.CoreLib.Win32.Win32Extensions.ShowInfoMessageBox(null, this.GetResourceTextByKey(1, "MSG_FUTURESHARE_TOTAL"));
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                BMC.CoreLib.Win32.Win32Extensions.ShowInfoMessageBox(null, ex.Message);
                ExceptionManager.Publish(ex);
                return false;

            }

        }

        private void MakeControlsReadonly(bool value)
        {
            try
            {
                LogManager.WriteLog("Inside MakeControlsReadonly...", LogManager.enumLogLevel.Info);
                txtName.ReadOnly = value;
                txtDescription.ReadOnly = value;
                rdbAlpha.AutoCheck = !value;
                rdbNumeric.AutoCheck = !value;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
        #endregion

        #region Events
        private void frmAddShareSchedule_Load(object sender, EventArgs e)
        {
            try
            {
                LogManager.WriteLog("Inside frmAddShareSchedule_Load...", LogManager.enumLogLevel.Info);
                this.ResolveResources();

                btnCancelSchedule.Enabled = false;
                if (!_editType)
                {
                    txtNoofBands.Text = "0";
                    rdbAlpha.Checked = true;
                    btnEditSchedule.Enabled = false;
                    tpBands.Enabled = false;
                    tpMachines.Enabled = false;

                }
                else
                {
                    MakeControlsReadonly(true);
                    btnUpdateSchedule.Enabled = false;
                    tpBands.Enabled = true;
                    tpMachines.Enabled = true;
                    if (_objShareScheduleEntity != null)
                    {
                        txtDescription.Text = _scheduleDescription;
                        txtName.Text = _scheduleName;
                        txtNoofBands.Text = _numberOfBands.ToString();
                        if (_bandCountType == "A")
                            rdbAlpha.Checked = true;
                        else
                            rdbNumeric.Checked = true;
                        LoadListViewBands(_shareScheduleId);
                        LoadListViewMachines(_shareScheduleId);
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error While loading form Add Share Schedule  Error Message:" + ex.Message, LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);

            }
        }

        private void btnUpdateSchedule_Click(object sender, EventArgs e)
        {
            List<ShareScheduleEntity> lstShareSchedule = null;

            try
            {
                LogManager.WriteLog("Inside btnUpdateSchedule_Click...", LogManager.enumLogLevel.Info);
                _scheduleName = txtName.Text;
                _scheduleDescription = txtDescription.Text;
                _numberOfBands = txtNoofBands.Text == string.Empty ? 0 : int.Parse(txtNoofBands.Text);
                _bandCountType = rdbNumeric.Checked ? "N" : "A";

                if (_scheduleName == string.Empty)
                {
                    BMC.CoreLib.Win32.Win32Extensions.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_SCHEDULENAME_EMPTY"));
                    return;
                }
                lstShareSchedule = _objShareScheduleBusiness.GetShareScheduleDetails();
                if (_editType != true)
                {
                    foreach (ShareScheduleEntity entity in lstShareSchedule)
                    {
                        if (entity.Share_Schedule_Name == txtName.Text)
                        {
                            BMC.CoreLib.Win32.Win32Extensions.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_SCHEDULENAME_EXISTS"));
                            return;
                        }
                    }
                }
                else
                {
                    foreach (ShareScheduleEntity entity in lstShareSchedule)
                    {
                        if (entity.Share_Schedule_Name == txtName.Text && entity.Share_Schedule_ID != _shareScheduleId)
                        {
                            BMC.CoreLib.Win32.Win32Extensions.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_SCHEDULENAME_EXISTS"));
                            return;
                        }
                    }
                }
                int? shareScheduleIdNew = 0;
                _objShareScheduleBusiness.AddOrUpdateShareSchedule(_shareScheduleId, _scheduleName, _scheduleDescription, _numberOfBands, _bandCountType, ref shareScheduleIdNew);

                //Audit updated entries for schedule tab
                new Audit_History()
                    .AddEntry()
                    .SetModule(ModuleNameEnterprise.SGVIFinancial)
                    .SetScreen("Share|Schedule")
                    .SetOperationType(_shareScheduleId > 0 ? OperationType.MODIFY : OperationType.ADD)
                    .SetDescription(_shareScheduleId > 0 ? "Share Schedule '" + txtName.Text + "' modified ..[{0}]: {2} --> {1}" : "Share Schedule '" + txtName.Text + "' added newly ..[{0}]: {1}")
                    .InsertAuditEntries(
                            scheduleSavedInfo,
                            new ScheduleEntity
                            {
                                Share_Schedule_Name = _scheduleName,
                                Share_Schedule_Description = _scheduleDescription,
                                Share_Schedule_No_Bands = _numberOfBands,
                                Share_Schedule_Bands_Name_Type = _bandCountType
                            },
                            _shareScheduleId <= 0);


                _shareScheduleId = shareScheduleIdNew.Value;
                if (_shareScheduleId > 0 && _editType)
                {
                    BMC.CoreLib.Win32.Win32Extensions.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_SHARESCHEDULE_UPDATE_SUCCESS"));
                }
                else if (_shareScheduleId > 0)
                {
                    BMC.CoreLib.Win32.Win32Extensions.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_SHARESCHEDULE_ADD_SUCCESS"));
                }


                MakeControlsReadonly(true);
                _editType = true;
                btnEditSchedule.Enabled = true;
                btnUpdateSchedule.Enabled = false;
                btnCancelSchedule.Enabled = false;
                tpBands.Enabled = true;
                tpMachines.Enabled = true;
                LoadListViewBands(_shareScheduleId);

            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error While updating Share Schedule  Error Message:" + ex.Message, LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
            }

        }

        private void btnEditSchedule_Click(object sender, EventArgs e)
        {
            try
            {
                LogManager.WriteLog("Inside btnEditSchedule_Click...", LogManager.enumLogLevel.Info);
                if (_editType)
                {
                    MakeControlsReadonly(false);
                }
                btnCancelSchedule.Enabled = true;
                btnEditSchedule.Enabled = false;
                btnUpdateSchedule.Enabled = true;
                tpBands.Enabled = true;
                tpMachines.Enabled = true;

                scheduleSavedInfo = new ScheduleEntity
                {
                    Share_Schedule_Name = txtName.Text,
                    Share_Schedule_Description = txtDescription.Text,
                    Share_Schedule_No_Bands = Convert.ToInt32(txtNoofBands.Text),
                    Share_Schedule_Bands_Name_Type = rdbNumeric.Checked ? "N" : "A"
                };
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void lvBands_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                LogManager.WriteLog("Inside lvBands_MouseClick...", LogManager.enumLogLevel.Info);
                if (lvBands.SelectedItems[0].Tag != null)
                {
                    btnApplyBands.Enabled = true;
                    ShareScheduleEntity entity = (lvBands.SelectedItems[0].Tag) as ShareScheduleEntity;
                    if (entity.Share_Band_ID != -1)
                    {
                        txtPastSupplierShare.Text = entity.Share_Band_Past_Supplier_Share.Value.ToString("00.00");
                        txtPastSiteshare.Text = entity.Share_Band_Past_Site_Share.Value.ToString("00.00");
                        txtPastCompanyshare.Text = entity.Share_Band_Past_Company_Share.Value.ToString("00.00");
                        txtSupplierShare.Text = entity.Share_Band_Supplier_Share.Value.ToString("00.00");
                        txtSiteshare.Text = entity.Share_Band_Site_Share.Value.ToString("00.00");
                        txtCompanyshare.Text = entity.Share_Band_Company_Share.Value.ToString("00.00");
                        txtFutureSupplierShare.Text = entity.Share_Band_Future_Supplier_Share.Value.ToString("00.00");
                        txtFutureSiteshare.Text = entity.Share_Band_Future_Site_Share.Value.ToString("00.00");
                        txtFutureCompanyshare.Text = entity.Share_Band_Future_Company_Share.Value.ToString("00.00");

                        chkCompanyShareGuaranteed.Checked = entity.Share_Band_Company_Share_Guaranteed.Value;
                        chkCompanyShareGuaranteedFuture.Checked = entity.Share_Band_Future_Company_Share_Guaranteed.Value;
                        chkCompanyShareGuaranteedPast.Checked = entity.Share_Band_Past_Company_Share_Guaranteed.Value;
                        chkSiteShareGuaranteedFuture.Checked = entity.Share_Band_Future_Site_Share_Guaranteed.Value;
                        chkSiteShareGuaranteed.Checked = entity.Share_Band_Site_Share_Guaranteed.Value;
                        chkSiteShareGuaranteedPast.Checked = entity.Share_Band_Past_Site_Share_Guaranteed.Value;
                        chkSupplierShareGuaranteed.Checked = entity.Share_Band_Supplier_Share_Guaranteed.Value;
                        chkSupplierShareGuaranteedFuture.Checked = entity.Share_Band_Future_Supplier_Share_Guaranteed.Value;
                        chkSupplierShareGuaranteedPast.Checked = entity.Share_Band_Past_Supplier_Share_Guaranteed.Value;
                        dtpBandsPast.Text = entity.Share_Band_Past_End_Date;
                        dtpBandsFuture.Text = entity.Share_Band_Future_Start_Date;
                    }
                    else
                    {
                        chkCompanyShareGuaranteed.Checked = false;
                        chkCompanyShareGuaranteedFuture.Checked = false;
                        chkCompanyShareGuaranteedPast.Checked = false;
                        chkSiteShareGuaranteedFuture.Checked = false;
                        chkSiteShareGuaranteed.Checked = false;
                        chkSiteShareGuaranteedPast.Checked = false;
                        chkSupplierShareGuaranteed.Checked = false;
                        chkSupplierShareGuaranteedFuture.Checked = false;
                        chkSupplierShareGuaranteedPast.Checked = false;
                        txtPastSupplierShare.Text = String.Format("00.00");
                        txtPastSiteshare.Text = String.Format("00.00");
                        txtPastCompanyshare.Text = String.Format("00.00");
                        txtSupplierShare.Text = String.Format("00.00");
                        txtSiteshare.Text = String.Format("00.00");
                        txtCompanyshare.Text = String.Format("00.00");
                        txtFutureSupplierShare.Text = String.Format("00.00");
                        txtFutureSiteshare.Text = String.Format("00.00");
                        txtFutureCompanyshare.Text = String.Format("00.00");
                        dtpBandsPast.Text = DateTime.Now.ToShortDateString();
                        dtpBandsFuture.Text = DateTime.Now.AddDays(1).ToShortDateString();
                    }
                }

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }



        private void btnBandFuturetoCurrent_Click(object sender, EventArgs e)
        {
            try
            {
                LogManager.WriteLog("Inside btnBandFuturetoCurrent_Click...", LogManager.enumLogLevel.Info);
                if ((BMC.CoreLib.Win32.Win32Extensions.ShowQuestionMessageBox(null, this.GetResourceTextByKey(1, "MSG_MOVEFUTURE_INTO_CURRENT"))) == DialogResult.Yes)
                {
                    foreach (ListViewItem item in lvBands.Items)
                    {

                        ShareScheduleEntity objShareScheduleEntity = (ShareScheduleEntity)item.Tag;
                        if (objShareScheduleEntity.Share_Band_ID != -1)
                        {

                            //audit entity - before changes
                            bandShareDataSavedInfo = new BandShareDataEntity
                            {
                                Share_Band_Company_Share = objShareScheduleEntity.Share_Band_Company_Share,
                                Share_Band_Sec_Company_Share = objShareScheduleEntity.Share_Band_Sec_Company_Share,
                                Share_Band_Site_Share = objShareScheduleEntity.Share_Band_Site_Share,
                                Share_Band_Supplier_Rent = objShareScheduleEntity.Share_Band_Supplier_Rent,
                                Share_Band_Supplier_Rent_Guaranteed = objShareScheduleEntity.Share_Band_Supplier_Rent_Guaranteed,
                                Share_Band_Supplier_Share = objShareScheduleEntity.Share_Band_Supplier_Share
                            };


                            objShareScheduleEntity.Share_Band_Company_Share = objShareScheduleEntity.Share_Band_Future_Company_Share;
                            objShareScheduleEntity.Share_Band_Sec_Company_Share = objShareScheduleEntity.Share_Band_Future_Sec_Company_Share;
                            objShareScheduleEntity.Share_Band_Site_Share = objShareScheduleEntity.Share_Band_Future_Site_Share;
                            objShareScheduleEntity.Share_Band_Supplier_Rent = objShareScheduleEntity.Share_Band_Future_Supplier_Rent;
                            objShareScheduleEntity.Share_Band_Supplier_Rent_Guaranteed = objShareScheduleEntity.Share_Band_Future_Supplier_Rent_Guaranteed;
                            objShareScheduleEntity.Share_Band_Supplier_Share = objShareScheduleEntity.Share_Band_Future_Supplier_Share;

                            _objShareScheduleBusiness.AddOrUpdateShareBand(objShareScheduleEntity);

                            //audit changes
                            new Audit_History()
                                .AddEntry()
                                .SetOperationType(OperationType.MODIFY)
                                .SetScreen("Share|Bands")
                                .SetDescription("Share Schedule '" + txtName.Text + "' modified. Modified band '" + item.SubItems[0].Text + "' ..[{0}]: {2} --> {1}")
                                .SetModule(ModuleNameEnterprise.SGVIFinancial)
                                .InsertAuditEntries(
                                    bandShareDataSavedInfo,
                                    new BandShareDataEntity
                                    {
                                        Share_Band_Company_Share = objShareScheduleEntity.Share_Band_Company_Share,
                                        Share_Band_Sec_Company_Share = objShareScheduleEntity.Share_Band_Sec_Company_Share,
                                        Share_Band_Site_Share = objShareScheduleEntity.Share_Band_Site_Share,
                                        Share_Band_Supplier_Rent = objShareScheduleEntity.Share_Band_Supplier_Rent,
                                        Share_Band_Supplier_Rent_Guaranteed = objShareScheduleEntity.Share_Band_Supplier_Rent_Guaranteed,
                                        Share_Band_Supplier_Share = objShareScheduleEntity.Share_Band_Supplier_Share
                                    },
                                    false);


                        }

                    }
                    BMC.CoreLib.Win32.Win32Extensions.ShowInfoMessageBox(null, this.GetResourceTextByKey(1, "MSG_APPLY_SUCCESS"));
                    LoadListViewBands(_shareScheduleId);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                BMC.CoreLib.Win32.Win32Extensions.ShowInfoMessageBox(null, this.GetResourceTextByKey(1, "MSG_ERROR_SAVING"));
            }

        }

        private void btnBandsCurrentToPast_Click(object sender, EventArgs e)
        {
            try
            {
                LogManager.WriteLog("Inside btnBandsCurrentToPast_Click...", LogManager.enumLogLevel.Info);
                if ((BMC.CoreLib.Win32.Win32Extensions.ShowQuestionMessageBox(null, this.GetResourceTextByKey(1, "MSG_MOVECURRENT_INTO_PAST"))) == DialogResult.Yes)
                {
                    foreach (ListViewItem item in lvBands.Items)
                    {

                        ShareScheduleEntity objShareScheduleEntity = (ShareScheduleEntity)item.Tag;
                        if (objShareScheduleEntity.Share_Band_ID != -1)
                        {

                            //audit entity - before changes
                            bandShareDataSavedInfo = new BandShareDataEntity
                            {
                                Share_Band_Company_Share = objShareScheduleEntity.Share_Band_Past_Company_Share,
                                Share_Band_Sec_Company_Share = objShareScheduleEntity.Share_Band_Past_Sec_Company_Share,
                                Share_Band_Site_Share = objShareScheduleEntity.Share_Band_Past_Site_Share,
                                Share_Band_Supplier_Rent = objShareScheduleEntity.Share_Band_Past_Supplier_Rent,
                                Share_Band_Supplier_Rent_Guaranteed = objShareScheduleEntity.Share_Band_Past_Supplier_Rent_Guaranteed,
                                Share_Band_Supplier_Share = objShareScheduleEntity.Share_Band_Past_Supplier_Share
                            };

                            objShareScheduleEntity.Share_Band_Past_Company_Share = objShareScheduleEntity.Share_Band_Company_Share;
                            objShareScheduleEntity.Share_Band_Past_Sec_Company_Share = objShareScheduleEntity.Share_Band_Sec_Company_Share;
                            objShareScheduleEntity.Share_Band_Past_Site_Share = objShareScheduleEntity.Share_Band_Site_Share;
                            objShareScheduleEntity.Share_Band_Past_Supplier_Rent = objShareScheduleEntity.Share_Band_Supplier_Rent;
                            objShareScheduleEntity.Share_Band_Past_Supplier_Share = objShareScheduleEntity.Share_Band_Supplier_Share;

                            objShareScheduleEntity.Share_Band_Past_Supplier_Rent_Guaranteed = objShareScheduleEntity.Share_Band_Supplier_Rent_Guaranteed;
                            _objShareScheduleBusiness.AddOrUpdateShareBand(objShareScheduleEntity);

                            //audit changes
                            new Audit_History()
                                .AddEntry()
                                .SetOperationType(OperationType.MODIFY)
                                .SetScreen("Share|Bands")
                                .SetDescription("Share Schedule '" + txtName.Text + "' modified. Modified band '" + item.SubItems[0].Text + "' ..[{0}]: {2} --> {1}")
                                .SetModule(ModuleNameEnterprise.SGVIFinancial)
                                .InsertAuditEntries(
                                    bandShareDataSavedInfo,
                                    new BandShareDataEntity
                                    {
                                        Share_Band_Company_Share = objShareScheduleEntity.Share_Band_Company_Share,
                                        Share_Band_Sec_Company_Share = objShareScheduleEntity.Share_Band_Sec_Company_Share,
                                        Share_Band_Site_Share = objShareScheduleEntity.Share_Band_Site_Share,
                                        Share_Band_Supplier_Rent = objShareScheduleEntity.Share_Band_Supplier_Rent,
                                        Share_Band_Supplier_Rent_Guaranteed = objShareScheduleEntity.Share_Band_Supplier_Rent_Guaranteed,
                                        Share_Band_Supplier_Share = objShareScheduleEntity.Share_Band_Supplier_Share
                                    },
                                    false);

                        }
                    }
                    BMC.CoreLib.Win32.Win32Extensions.ShowInfoMessageBox(null, this.GetResourceTextByKey(1, "MSG_APPLY_SUCCESS"));
                    LoadListViewBands(_shareScheduleId);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                BMC.CoreLib.Win32.Win32Extensions.ShowInfoMessageBox(null, this.GetResourceTextByKey(1, "MSG_ERROR_SAVING"));
            }
        }

        private void btnApplyDates_Click(object sender, EventArgs e)
        {
            try
            {
                LogManager.WriteLog("Inside btnApplyDates_Click...", LogManager.enumLogLevel.Info);
                if ((dtpBandsFuture.Value - dtpBandsPast.Value).Days > 0)
                {
                    foreach (ListViewItem item in lvBands.Items)
                    {

                        ShareScheduleEntity objShareScheduleEntity = (ShareScheduleEntity)item.Tag;
                        if (objShareScheduleEntity.Share_Band_ID != -1)
                        {
                            //audit entity - before changes
                            bandShareDateSavedInfo = new BandShareDateEntity
                            {
                                Share_Band_Past_End_Date = objShareScheduleEntity.Share_Band_Past_End_Date,
                                Share_Band_Future_Start_Date = objShareScheduleEntity.Share_Band_Future_Start_Date
                            };

                            objShareScheduleEntity.Share_Band_Past_End_Date = Convert.ToDateTime(dtpBandsPast.Text).ToString("dd MMM yyyy");
                            objShareScheduleEntity.Share_Band_Future_Start_Date = Convert.ToDateTime(dtpBandsFuture.Text).ToString("dd MMM yyyy");
                            _objShareScheduleBusiness.AddOrUpdateShareBand(objShareScheduleEntity);

                            //audit changes
                            new Audit_History()
                                .AddEntry()
                                .SetOperationType(OperationType.MODIFY)
                                .SetScreen("Share|Bands")
                                .SetDescription("Share Schedule '" + txtName.Text + "' modified. Modified band '" + item.SubItems[0].Text + "'  ..[{0}]: {2} --> {1}")
                                .SetModule(ModuleNameEnterprise.SGVIFinancial)
                                .InsertAuditEntries(
                                    bandShareDateSavedInfo,
                                    new BandShareDateEntity
                                    {
                                        Share_Band_Past_End_Date = objShareScheduleEntity.Share_Band_Past_End_Date,
                                        Share_Band_Future_Start_Date = objShareScheduleEntity.Share_Band_Future_Start_Date
                                    },
                                    false);

                        }
                    }
                    BMC.CoreLib.Win32.Win32Extensions.ShowInfoMessageBox(null, this.GetResourceTextByKey(1, "MSG_APPLY_SUCCESS"));
                    LoadListViewBands(_shareScheduleId);
                }
                else
                {
                    BMC.CoreLib.Win32.Win32Extensions.ShowInfoMessageBox(null, this.GetResourceTextByKey(1, "MSG_INVALID_DATES"));
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                BMC.CoreLib.Win32.Win32Extensions.ShowInfoMessageBox(null, this.GetResourceTextByKey(1, "MSG_ERROR_SAVING"));
            }
        }

        private void btnApplyBands_Click(object sender, EventArgs e)
        {
            try
            {
                LogManager.WriteLog("Inside btnApplyBands_Click...", LogManager.enumLogLevel.Info);
                if (VerifyValidNumber())
                {
                    ShareScheduleEntity entity = (ShareScheduleEntity)lvBands.SelectedItems[0].Tag;

                    //audit entity - before changes
                    bandShareDetailsSavedInfo = new BandShareDetailsEntity
                    {
                        Share_Band_Past_Supplier_Share = entity.Share_Band_Past_Supplier_Share,
                        Share_Band_Past_Site_Share = entity.Share_Band_Past_Site_Share,
                        Share_Band_Past_Company_Share = entity.Share_Band_Past_Company_Share,
                        Share_Band_Supplier_Share = entity.Share_Band_Supplier_Share,
                        Share_Band_Site_Share = entity.Share_Band_Site_Share,
                        Share_Band_Company_Share = entity.Share_Band_Company_Share,
                        Share_Band_Future_Supplier_Share = entity.Share_Band_Future_Supplier_Share,
                        Share_Band_Future_Site_Share = entity.Share_Band_Future_Site_Share,
                        Share_Band_Future_Company_Share = entity.Share_Band_Future_Company_Share,
                        Share_Band_Company_Share_Guaranteed = entity.Share_Band_Company_Share_Guaranteed,
                        Share_Band_Future_Company_Share_Guaranteed = entity.Share_Band_Future_Company_Share_Guaranteed,
                        Share_Band_Past_Company_Share_Guaranteed = entity.Share_Band_Past_Company_Share_Guaranteed,
                        Share_Band_Future_Site_Share_Guaranteed = entity.Share_Band_Future_Site_Share_Guaranteed,
                        Share_Band_Site_Share_Guaranteed = entity.Share_Band_Site_Share_Guaranteed,
                        Share_Band_Past_Site_Share_Guaranteed = entity.Share_Band_Past_Site_Share_Guaranteed,
                        Share_Band_Supplier_Share_Guaranteed = entity.Share_Band_Supplier_Share_Guaranteed,
                        Share_Band_Future_Supplier_Share_Guaranteed = entity.Share_Band_Future_Supplier_Share_Guaranteed,
                        Share_Band_Past_Supplier_Share_Guaranteed = entity.Share_Band_Past_Supplier_Share_Guaranteed,
                        Share_Band_Past_End_Date = entity.Share_Band_Past_End_Date,
                        Share_Band_Future_Start_Date = entity.Share_Band_Future_Start_Date
                    };


                    entity.Share_Schedule_Name = txtName.Text;
                    entity.Share_Band_Past_Supplier_Share = Convert.ToSingle(txtPastSupplierShare.Text);
                    entity.Share_Band_Past_Site_Share = Convert.ToSingle(txtPastSiteshare.Text);
                    entity.Share_Band_Past_Company_Share = Convert.ToSingle(txtPastCompanyshare.Text);
                    entity.Share_Band_Supplier_Share = Convert.ToSingle(txtSupplierShare.Text);
                    entity.Share_Band_Site_Share = Convert.ToSingle(txtSiteshare.Text);
                    entity.Share_Band_Company_Share = Convert.ToSingle(txtCompanyshare.Text);
                    entity.Share_Band_Future_Supplier_Share = Convert.ToSingle(txtFutureSupplierShare.Text);
                    entity.Share_Band_Future_Site_Share = Convert.ToSingle(txtFutureSiteshare.Text);
                    entity.Share_Band_Future_Company_Share = Convert.ToSingle(txtFutureCompanyshare.Text);
                    entity.Share_Band_Company_Share_Guaranteed = chkCompanyShareGuaranteed.Checked;
                    entity.Share_Band_Future_Company_Share_Guaranteed = chkCompanyShareGuaranteedFuture.Checked;
                    entity.Share_Band_Past_Company_Share_Guaranteed = chkCompanyShareGuaranteedPast.Checked;
                    entity.Share_Band_Future_Site_Share_Guaranteed = chkSiteShareGuaranteedFuture.Checked;
                    entity.Share_Band_Site_Share_Guaranteed = chkSiteShareGuaranteed.Checked;
                    entity.Share_Band_Past_Site_Share_Guaranteed = chkSiteShareGuaranteedPast.Checked;
                    entity.Share_Band_Supplier_Share_Guaranteed = chkSupplierShareGuaranteed.Checked;
                    entity.Share_Band_Future_Supplier_Share_Guaranteed = chkSupplierShareGuaranteedFuture.Checked;
                    entity.Share_Band_Past_Supplier_Share_Guaranteed = chkSupplierShareGuaranteedPast.Checked;
                    entity.Share_Band_Past_End_Date = Convert.ToDateTime(dtpBandsPast.Text).ToString("dd MMM yyyy");
                    entity.Share_Band_Future_Start_Date = Convert.ToDateTime(dtpBandsFuture.Text).ToString("dd MMM yyyy");
                    _objShareScheduleBusiness.AddOrUpdateShareBand(entity);


                    //audit changes
                    new Audit_History()
                        .AddEntry()
                        .SetOperationType(entity.Share_Band_ID > -1 ? OperationType.MODIFY : OperationType.ADD)
                        .SetScreen("Share|Bands")
                        .SetDescription(entity.Share_Band_ID > -1 ? "Share Schedule '" + txtName.Text + "' modified. Modified band '" + lvBands.SelectedItems[0].SubItems[0].Text + "'  ..[{0}]: {2} --> {1}" : "Share Schedule '" + txtName.Text + "' modified. Added new band '" + lvBands.SelectedItems[0].SubItems[0].Text + "' ..[{0}]: {1}")
                        .SetModule(ModuleNameEnterprise.SGVIFinancial)
                        .InsertAuditEntries(
                            bandShareDetailsSavedInfo,
                            new BandShareDetailsEntity
                            {
                                Share_Band_Past_Supplier_Share = entity.Share_Band_Past_Supplier_Share,
                                Share_Band_Past_Site_Share = entity.Share_Band_Past_Site_Share,
                                Share_Band_Past_Company_Share = entity.Share_Band_Past_Company_Share,
                                Share_Band_Supplier_Share = entity.Share_Band_Supplier_Share,
                                Share_Band_Site_Share = entity.Share_Band_Site_Share,
                                Share_Band_Company_Share = entity.Share_Band_Company_Share,
                                Share_Band_Future_Supplier_Share = entity.Share_Band_Future_Supplier_Share,
                                Share_Band_Future_Site_Share = entity.Share_Band_Future_Site_Share,
                                Share_Band_Future_Company_Share = entity.Share_Band_Future_Company_Share,
                                Share_Band_Company_Share_Guaranteed = entity.Share_Band_Company_Share_Guaranteed,
                                Share_Band_Future_Company_Share_Guaranteed = entity.Share_Band_Future_Company_Share_Guaranteed,
                                Share_Band_Past_Company_Share_Guaranteed = entity.Share_Band_Past_Company_Share_Guaranteed,
                                Share_Band_Future_Site_Share_Guaranteed = entity.Share_Band_Future_Site_Share_Guaranteed,
                                Share_Band_Site_Share_Guaranteed = entity.Share_Band_Site_Share_Guaranteed,
                                Share_Band_Past_Site_Share_Guaranteed = entity.Share_Band_Past_Site_Share_Guaranteed,
                                Share_Band_Supplier_Share_Guaranteed = entity.Share_Band_Supplier_Share_Guaranteed,
                                Share_Band_Future_Supplier_Share_Guaranteed = entity.Share_Band_Future_Supplier_Share_Guaranteed,
                                Share_Band_Past_Supplier_Share_Guaranteed = entity.Share_Band_Past_Supplier_Share_Guaranteed,
                                Share_Band_Past_End_Date = entity.Share_Band_Past_End_Date,
                                Share_Band_Future_Start_Date = entity.Share_Band_Future_Start_Date
                            },
                            entity.Share_Band_ID <= -1);

                    BMC.CoreLib.Win32.Win32Extensions.ShowInfoMessageBox(null, this.GetResourceTextByKey(1, "MSG_APPLY_SUCCESS"));

                    btnApplyBands.Enabled = false;
                    btnRemoveGameTitle.Enabled = false;
                    LoadListViewBands(_shareScheduleId);
                    LoadListViewMachines(_shareScheduleId);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                BMC.CoreLib.Win32.Win32Extensions.ShowInfoMessageBox(null, this.GetResourceTextByKey(1, "MSG_ERROR_SAVING"));
            }

        }





        private void btnMCBandCurrenttoPast_Click(object sender, EventArgs e)
        {
            try
            {
                LogManager.WriteLog("Inside btnMCBandCurrenttoPast_Click...", LogManager.enumLogLevel.Info);
                if ((BMC.CoreLib.Win32.Win32Extensions.ShowQuestionMessageBox(null, this.GetResourceTextByKey(1, "MSG_MOVECURRENT_INTO_PAST"))) == DialogResult.Yes)
                {
                    foreach (ListViewItem item in lvMachines.Items)
                    {

                        ShareScheduleEntity objShareScheduleEntity = (ShareScheduleEntity)item.Tag;
                        if (objShareScheduleEntity.Machine_Class_Share_Band != -1 && objShareScheduleEntity.Machine_Class_ID != 0)
                        {
                            int? pastBandId = objShareScheduleEntity.Share_Band_ID_Past;
                            objShareScheduleEntity.Share_Band_ID_Past = objShareScheduleEntity.Share_Band_ID_Present;

                            _objShareScheduleBusiness.AddOrUpdateMachineClassShareBand(objShareScheduleEntity);

                            //audit changes
                            new Audit_History()
                                .AddEntry()
                                .SetField("Share_Band_ID_Past")
                                .SetOldValue(pastBandId.GetValueOrDefault().ToString())
                                .SetNewValue(objShareScheduleEntity.Share_Band_ID_Past.GetValueOrDefault().ToString())
                                .SetDescription("Share Schedule '" + txtName.Text + "' modified. Modified machine game '" + item.SubItems[0].Text + "' ..[Share_Band_ID_Past]: " + pastBandId.GetValueOrDefault().ToString() + " --> " + objShareScheduleEntity.Share_Band_ID_Past.GetValueOrDefault().ToString())
                                .SetScreen("Share|Machines")
                                .SetOperationType(OperationType.MODIFY)
                                .SetModule(ModuleNameEnterprise.SGVIFinancial)
                                .InsertEntry(pastBandId != objShareScheduleEntity.Share_Band_ID_Past);
                        }
                    }
                    BMC.CoreLib.Win32.Win32Extensions.ShowInfoMessageBox(null, this.GetResourceTextByKey(1, "MSG_APPLY_SUCCESS"));
                    LoadListViewMachines(_shareScheduleId);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                BMC.CoreLib.Win32.Win32Extensions.ShowInfoMessageBox(null, this.GetResourceTextByKey(1, "MSG_ERROR_SAVING"));
            }

        }

        private void btnMCBandFuturetoCurrent_Click(object sender, EventArgs e)
        {
            try
            {
                LogManager.WriteLog("Inside btnMCBandFuturetoCurrent_Click...", LogManager.enumLogLevel.Info);
                if ((BMC.CoreLib.Win32.Win32Extensions.ShowQuestionMessageBox(null, this.GetResourceTextByKey(1, "MSG_MOVEFUTURE_INTO_CURRENT"))) == DialogResult.Yes)
                {
                    foreach (ListViewItem item in lvMachines.Items)
                    {
                        ShareScheduleEntity objShareScheduleEntity = (ShareScheduleEntity)item.Tag;
                        if (objShareScheduleEntity.Machine_Class_Share_Band != -1 && objShareScheduleEntity.Machine_Class_ID != 0)
                        {
                            int? presentBandId = objShareScheduleEntity.Share_Band_ID_Present;

                            objShareScheduleEntity.Share_Band_ID_Present = objShareScheduleEntity.Share_Band_ID_Future;

                            _objShareScheduleBusiness.AddOrUpdateMachineClassShareBand(objShareScheduleEntity);

                            //audit changes
                            new Audit_History()
                                .AddEntry()
                                .SetField("Share_Band_ID_Present")
                                .SetOldValue(presentBandId.GetValueOrDefault().ToString())
                                .SetNewValue(objShareScheduleEntity.Share_Band_ID_Present.GetValueOrDefault().ToString())
                                .SetDescription("Share Schedule '" + txtName.Text + "' modified. Modified machine game '" + item.SubItems[0].Text + "' ..[Share_Band_ID_Present]: " + presentBandId.GetValueOrDefault().ToString() + " --> " + objShareScheduleEntity.Share_Band_ID_Present.GetValueOrDefault().ToString())
                                .SetScreen("Share|Machines")
                                .SetOperationType(OperationType.MODIFY)
                                .SetModule(ModuleNameEnterprise.SGVIFinancial)
                                .InsertEntry(presentBandId != objShareScheduleEntity.Share_Band_ID_Present);

                        }
                    }
                    BMC.CoreLib.Win32.Win32Extensions.ShowInfoMessageBox(null, this.GetResourceTextByKey(1, "MSG_APPLY_SUCCESS"));
                    LoadListViewMachines(_shareScheduleId);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                BMC.CoreLib.Win32.Win32Extensions.ShowInfoMessageBox(null, this.GetResourceTextByKey(1, "MSG_ERROR_SAVING"));
            }
        }


        private void btnApplyDatesMachines_Click(object sender, EventArgs e)
        {
            try
            {
                LogManager.WriteLog("Inside btnApplyDatesMachines_Click...", LogManager.enumLogLevel.Info);
                if ((DTPickerMachinesFuture.Value - DTPickerMachinesPast.Value).Days > 0)
                {
                    foreach (ListViewItem item in lvMachines.Items)
                    {

                        ShareScheduleEntity objShareScheduleEntity = (ShareScheduleEntity)item.Tag;
                        if (objShareScheduleEntity.Machine_Class_Share_Band != -1 && objShareScheduleEntity.Machine_Class_ID != 0)
                        {
                            string pastDate = objShareScheduleEntity.Machine_Class_Share_Past_Date, futureDate = objShareScheduleEntity.Machine_Class_Share_Future_Date;

                            objShareScheduleEntity.Machine_Class_Share_Past_Date = Convert.ToDateTime(DTPickerMachinesPast.Text).ToString("dd MMM yyyy");
                            objShareScheduleEntity.Machine_Class_Share_Future_Date = Convert.ToDateTime(DTPickerMachinesFuture.Text).ToString("dd MMM yyyy");
                            _objShareScheduleBusiness.AddOrUpdateMachineClassShareBand(objShareScheduleEntity);

                            //audit changes
                            new Audit_History()
                                .AddEntry()
                                .SetField("Machine_Class_Share_Past_Date")
                                .SetOldValue(pastDate)
                                .SetNewValue(objShareScheduleEntity.Machine_Class_Share_Past_Date)
                                .SetDescription("Share Schedule '" + txtName.Text + "' modified. Modified machine game '" + item.SubItems[0].Text + "' ..[Machine_Class_Share_Past_Date]: " + pastDate + " --> " + objShareScheduleEntity.Machine_Class_Share_Past_Date)
                                .SetScreen("Share|Machines")
                                .SetOperationType(OperationType.MODIFY)
                                .SetModule(ModuleNameEnterprise.SGVIFinancial)
                                .InsertEntry(pastDate != objShareScheduleEntity.Machine_Class_Share_Past_Date);

                            //audit changes
                            new Audit_History()
                                .AddEntry()
                                .SetField("Machine_Class_Share_Future_Date")
                                .SetOldValue(futureDate)
                                .SetNewValue(objShareScheduleEntity.Machine_Class_Share_Future_Date)
                                .SetDescription("Share Schedule '" + txtName.Text + "' modified. Modified machine game '" + item.SubItems[0].Text + "' ..[Machine_Class_Share_Future_Date]: " + futureDate + " --> " + objShareScheduleEntity.Machine_Class_Share_Future_Date)
                                .SetScreen("Share|Machines")
                                .SetOperationType(OperationType.MODIFY)
                                .SetModule(ModuleNameEnterprise.SGVIFinancial)
                                .InsertEntry(futureDate != objShareScheduleEntity.Machine_Class_Share_Future_Date);
                        }
                    }
                    BMC.CoreLib.Win32.Win32Extensions.ShowInfoMessageBox(null, this.GetResourceTextByKey(1, "MSG_APPLY_SUCCESS"));
                    LoadListViewMachines(_shareScheduleId);
                }
                else
                {
                    BMC.CoreLib.Win32.Win32Extensions.ShowInfoMessageBox(null, this.GetResourceTextByKey(1, "MSG_INVALID_DATES"));

                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                BMC.CoreLib.Win32.Win32Extensions.ShowInfoMessageBox(null, this.GetResourceTextByKey(1, "MSG_ERROR_SAVING"));
            }
        }

        private void btnApplyMachines_Click(object sender, EventArgs e)
        {
            try
            {
                LogManager.WriteLog("Inside btnApplyMachines_Click...", LogManager.enumLogLevel.Info);
                if ((DTPickerMachinesFuture.Value - DTPickerMachinesPast.Value).Days > 0)
                {
                    if (cmbMachineBandsPast.SelectedIndex == -1 || cmbMachineBandsCurrent.SelectedIndex == -1 || cmbMachineBandsFuture.SelectedIndex == -1)
                    {
                        BMC.CoreLib.Win32.Win32Extensions.ShowInfoMessageBox(null, this.GetResourceTextByKey(1, "MSG_INVALID_BANDS"));
                        return;
                    }

                    ShareScheduleEntity objShareScheduleEntity = (ShareScheduleEntity)lvMachines.SelectedItems[0].Tag;
                    if (objShareScheduleEntity != null)
                    {

                        //audit entity - before changes
                        machineClassDetailsSavedInfo = new MachineClassDetailsEntity
                        {
                            Machine_Class_Share_Past_Date = objShareScheduleEntity.Machine_Class_Share_Past_Date,
                            Machine_Class_Share_Future_Date = objShareScheduleEntity.Machine_Class_Share_Future_Date,
                            Share_Band_ID_Present = objShareScheduleEntity.Share_Band_ID_Present,
                            Share_Band_ID_Past = objShareScheduleEntity.Share_Band_ID_Past,
                            Share_Band_ID_Future = objShareScheduleEntity.Share_Band_ID_Future,
                        };

                        objShareScheduleEntity.Machine_Class_Share_Past_Date = Convert.ToDateTime(DTPickerMachinesPast.Text).ToString("dd MMM yyyy");
                        objShareScheduleEntity.Machine_Class_Share_Future_Date = Convert.ToDateTime(DTPickerMachinesFuture.Text).ToString("dd MMM yyyy");
                        objShareScheduleEntity.Share_Band_ID_Present = int.Parse(cmbMachineBandsCurrent.SelectedValue.ToString());
                        objShareScheduleEntity.Share_Band_ID_Past = int.Parse(cmbMachineBandsPast.SelectedValue.ToString());
                        objShareScheduleEntity.Share_Band_ID_Future = int.Parse(cmbMachineBandsFuture.SelectedValue.ToString());
                        _objShareScheduleBusiness.AddOrUpdateMachineClassShareBand(objShareScheduleEntity);

                        //audit changes
                        new Audit_History()
                            .AddEntry()
                            .SetDescription(objShareScheduleEntity.Machine_Class_Share_Band > 0 ? "Share Schedule '" + txtName.Text + "' modified. Modified machine game '" + lvMachines.SelectedItems[0].SubItems[0].Text + "' ..[{0}]: {2} --> {1}" : "Share Schedule '" + txtName.Text + "' modified. Added new machine game '" + lvMachines.SelectedItems[0].SubItems[0].Text + "'  ..[{0}]: {1}")
                            .SetScreen("Share|Machines")
                            .SetOperationType(objShareScheduleEntity.Machine_Class_Share_Band > 0 ? OperationType.MODIFY : OperationType.ADD)
                            .SetModule(ModuleNameEnterprise.SGVIFinancial)
                            .InsertAuditEntries(
                                machineClassDetailsSavedInfo,
                                new MachineClassDetailsEntity
                                {
                                    Machine_Class_Share_Past_Date = objShareScheduleEntity.Machine_Class_Share_Past_Date,
                                    Machine_Class_Share_Future_Date = objShareScheduleEntity.Machine_Class_Share_Future_Date,
                                    Share_Band_ID_Present = objShareScheduleEntity.Share_Band_ID_Present,
                                    Share_Band_ID_Past = objShareScheduleEntity.Share_Band_ID_Past,
                                    Share_Band_ID_Future = objShareScheduleEntity.Share_Band_ID_Future,

                                },
                                objShareScheduleEntity.Machine_Class_Share_Band <= 0);

                        BMC.CoreLib.Win32.Win32Extensions.ShowInfoMessageBox(null, this.GetResourceTextByKey(1, "MSG_APPLY_SUCCESS"));

                        DTPickerMachinesPast.Text = DateTime.Now.ToShortDateString();
                        DTPickerMachinesFuture.Text = DateTime.Now.AddDays(1).ToShortDateString();
                        txtAddMachineSearch.Text = string.Empty;
                        cmbMachineSearch.DataSource = null;
                        btnApplyMachines.Enabled = false;
                        btnRemoveGameTitle.Enabled = false;
                        LoadListViewMachines(_shareScheduleId);
                    }
                    else
                    {
                        BMC.CoreLib.Win32.Win32Extensions.ShowInfoMessageBox(null, this.GetResourceTextByKey(1, "MSG_SELECT_LISTVIEW"));
                    }
                }
                else
                {
                    BMC.CoreLib.Win32.Win32Extensions.ShowInfoMessageBox(null, this.GetResourceTextByKey(1, "MSG_INVALID_DATES"));
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                BMC.CoreLib.Win32.Win32Extensions.ShowInfoMessageBox(null, this.GetResourceTextByKey(1, "MSG_ERROR_SAVING"));
            }
        }



        private void btnAddGameTitle_Click(object sender, EventArgs e)
        {
            try
            {
                LogManager.WriteLog("Inside btnAddGameTitle_Click...", LogManager.enumLogLevel.Info);
                if (cmbMachineSearch.SelectedIndex > -1)
                {
                    foreach (ListViewItem item in lvMachines.Items)
                    {
                        string machineName = item.SubItems[0].Text;
                        machineName = machineName.Contains("[") ? machineName.Remove(machineName.LastIndexOf('[')) : machineName;
                        if (cmbMachineSearch.Text.Contains(machineName))
                        {
                            BMC.CoreLib.Win32.Win32Extensions.ShowInfoMessageBox(null, this.GetResourceTextByKey(1, "MSG_MACHINE_ALREADY"));
                            return;
                        }

                    }
                    int selectedMachineClassId = int.Parse(cmbMachineSearch.SelectedValue.ToString());
                    ListViewItem newitem = new ListViewItem();

                    var mcItem = (_lstMachineClass.Where(c => c.Machine_Class_ID == selectedMachineClassId).Select(lv => new { lv.Machine_BACTA_Code })).SingleOrDefault().Machine_BACTA_Code;
                    newitem.Text = mcItem != null ? mcItem.ToString() : cmbMachineSearch.Text;
                    newitem.Tag = new ShareScheduleEntity() { Machine_Class_Share_Band = -1, Machine_Class_ID = int.Parse(cmbMachineSearch.SelectedValue.ToString()) };
                    newitem.SubItems.Add("N/A");
                    newitem.SubItems.Add("N/A");
                    newitem.SubItems.Add("N/A");
                    newitem.SubItems.Add("N/A");
                    newitem.SubItems.Add("N/A");
                    lvMachines.Items.Add(newitem);
                    btnAddGameTitle.Enabled = false;
                    newitem.Selected = true;
                }
                else
                {
                    BMC.CoreLib.Win32.Win32Extensions.ShowInfoMessageBox(null, this.GetResourceTextByKey(1, "MSG_USE_SEARCH"));
                }

                lvMachines.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btnRemoveGameTitle_Click(object sender, EventArgs e)
        {
            try
            {
                LogManager.WriteLog("Inside btnRemoveGameTitle_Click...", LogManager.enumLogLevel.Info);
                if (lvMachines.SelectedItems[0].Tag != null)
                {
                    int machineClassShareBandId = ((ShareScheduleEntity)lvMachines.SelectedItems[0].Tag).Machine_Class_Share_Band;
                    if (BMC.CoreLib.Win32.Win32Extensions.ShowQuestionMessageBox(null, this.GetResourceTextByKey(1, "MSG_REMOVE_SELECTED_MACHINE") + lvMachines.SelectedItems[0].Text
                        + this.GetResourceTextByKey(1, "MSG_FROM_SCHEDULE")) == DialogResult.Yes

                        && machineClassShareBandId != -1)
                    {
                        _objShareScheduleBusiness.DeleteMachineClassShareBand(machineClassShareBandId);

                        //Audit changes
                        new Audit_History()
                        .AddEntry()
                        .SetModule(ModuleNameEnterprise.SGVIFinancial)
                        .SetScreen("Share|Machines")
                        .SetOldValue(machineClassShareBandId.ToString())
                        .SetOperationType(OperationType.DELETE)
                        .SetField("machine_class_share_band")
                        .SetDescription("Share Schedule '" + txtName.Text + "' modified. Removed game title '" + lvMachines.SelectedItems[0].SubItems[0].Text + "' ..[machine_class_share_band]")
                        .InsertEntry();

                        BMC.CoreLib.Win32.Win32Extensions.ShowInfoMessageBox(null, this.GetResourceTextByKey(1, "MSG_DELETE_SUCCESS"));

                    }
                    btnRemoveGameTitle.Enabled = false;
                    btnApplyMachines.Enabled = false;
                }
                LoadListViewMachines(_shareScheduleId);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

        }

        private void lvMachines_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                LogManager.WriteLog("Inside lvMachines_MouseClick...", LogManager.enumLogLevel.Info);
                btnApplyMachines.Enabled = true;
                btnRemoveGameTitle.Enabled = true;
                ShareScheduleEntity entity = (lvMachines.SelectedItems[0].Tag) as ShareScheduleEntity;
                if (entity.Machine_Class_Share_Band != -1)
                {
                    DTPickerMachinesPast.Text = entity.Machine_Class_Share_Past_Date.ToShortDateString();
                    DTPickerMachinesFuture.Text = entity.Machine_Class_Share_Future_Date.ToShortDateString();
                    cmbMachineBandsPast.Text = entity.PastBandName;
                    cmbMachineBandsCurrent.Text = entity.BandName;
                    cmbMachineBandsFuture.Text = entity.FutureBandName;
                }
                else
                {
                    DTPickerMachinesPast.Text = DateTime.Now.AddDays(7).ToShortDateString();
                    DTPickerMachinesFuture.Text = DateTime.Now.AddDays(7).ToShortDateString();
                    cmbMachineBandsPast.SelectedIndex = 0;
                    cmbMachineBandsCurrent.SelectedIndex = 0;
                    cmbMachineBandsFuture.SelectedIndex = 0;
                }


            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                LogManager.WriteLog("Inside btnExport_Click...", LogManager.enumLogLevel.Info);
                if (lvMachines.Items.Count > 0)
                {
                    SaveFileDialog saveFileDialog = new SaveFileDialog
                    {
                        Title = "Choose file to save to",
                        FileName = "MachineClass.csv",
                        Filter = "CSV (*.csv)|*.csv",
                        FilterIndex = 0,
                        InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
                    };


                    if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {

                        string[] headerValues = this.lvMachines.Columns
                                           .OfType<ColumnHeader>()
                                           .Select(header => header.Text.Trim())
                                           .ToArray();

                        string[][] columnValues = this.lvMachines.Items
                                    .OfType<ListViewItem>()
                                    .Select(lvi => lvi.SubItems
                                        .OfType<ListViewItem.ListViewSubItem>()
                                        .Select(si => si.Text).ToArray()).ToArray();
                        string table = string.Join(",", new string[] { "Machine allocation in Share schedule: ", this._scheduleName }) + Environment.NewLine;
                        table += string.Join(",", headerValues) + Environment.NewLine;
                        foreach (string[] columnValue in columnValues)
                        {
                            table += string.Join(",", columnValue) + Environment.NewLine;
                        }
                        table = table.TrimEnd('\r', '\n');
                        System.IO.File.WriteAllText(saveFileDialog.FileName, table);
                        BMC.CoreLib.Win32.Win32Extensions.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_DATA_SAVED") + saveFileDialog.FileName);
                    }

                }
                else
                {
                    BMC.CoreLib.Win32.Win32Extensions.ShowInfoMessageBox(null, this.GetResourceTextByKey(1, "MSG_NO_RECORDS"));
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btnCancelSchedule_Click(object sender, EventArgs e)
        {
            MakeControlsReadonly(true);
            btnCancelSchedule.Enabled = false;
            btnEditSchedule.Enabled = true;
            btnUpdateSchedule.Enabled = false;
        }


        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void txtAddMachineSearch_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                LogManager.WriteLog("Inside txtAddMachineSearch_KeyDown...", LogManager.enumLogLevel.Info);
                if (e.KeyCode == Keys.Enter)
                {
                    if (txtAddMachineSearch.Text != string.Empty)
                    {
                        _lstMachineClass = _objShareScheduleBusiness.GetMachineClass(txtAddMachineSearch.Text);
                        _lstMachineClass.Insert(0, new ShareScheduleEntity { Machine_Class_ID = 0, Machine_Name = "--Default--" });

                        cmbMachineSearch.DataSource = null;
                        cmbMachineSearch.DataSource = _lstMachineClass;
                        cmbMachineSearch.DisplayMember = "Machine_Name";
                        cmbMachineSearch.ValueMember = "Machine_Class_ID";
                        cmbMachineSearch.SelectedIndex = _lstMachineClass.Count > 1 ? 1 : 0;

                        if (_lstMachineClass.Count == 1)
                        {
                            BMC.CoreLib.Win32.Win32Extensions.ShowInfoMessageBox(null, this.GetResourceTextByKey(1, "MSG_GAME_TITLE_NOT_FOUND"));
                        }
                    }
                    else
                    {
                        cmbMachineSearch.DataSource = null;
                    }
                }

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

        }

        private void cmbMachineSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                LogManager.WriteLog("Inside cmbMachineSearch_SelectedIndexChanged...", LogManager.enumLogLevel.Info);
                if (cmbMachineSearch.SelectedIndex >= 0)
                    btnAddGameTitle.Enabled = true;
                else
                    btnAddGameTitle.Enabled = false;
                ToolTip toolTipMachineSearch = new ToolTip();

                toolTipMachineSearch.Show(cmbMachineSearch.SelectedText, this.cmbMachineSearch);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void lvBands_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                LogManager.WriteLog("Inside lvBands_SelectedIndexChanged...", LogManager.enumLogLevel.Info);
                btnApplyBands.Enabled = lvBands.SelectedItems.Count > 0;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

        }

        private void lvMachines_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                LogManager.WriteLog("Inside lvMachines_SelectedIndexChanged...", LogManager.enumLogLevel.Info);
                btnApplyMachines.Enabled = lvMachines.SelectedItems.Count > 0;
                btnRemoveGameTitle.Enabled = lvMachines.SelectedItems.Count > 0;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btnScheduleClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tcShareSchedule_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tcShareSchedule.SelectedIndex == 0)
                txtName.Focus();
            else if (tcShareSchedule.SelectedIndex == 1)
                txtPastSupplierShare.Focus();
            else if (tcShareSchedule.SelectedIndex == 2)
                txtAddMachineSearch.Focus();
        }

        #endregion

        
    }

    public class BandShareDetailsEntity
    {
        public float? Share_Band_Past_Supplier_Share { get; set; }
        public float? Share_Band_Past_Site_Share { get; set; }
        public float? Share_Band_Past_Company_Share { get; set; }
        public float? Share_Band_Supplier_Share { get; set; }
        public float? Share_Band_Site_Share { get; set; }
        public float? Share_Band_Company_Share { get; set; }
        public float? Share_Band_Future_Supplier_Share { get; set; }
        public float? Share_Band_Future_Site_Share { get; set; }
        public float? Share_Band_Future_Company_Share { get; set; }
        public bool? Share_Band_Company_Share_Guaranteed { get; set; }
        public bool? Share_Band_Future_Company_Share_Guaranteed { get; set; }
        public bool? Share_Band_Past_Company_Share_Guaranteed { get; set; }
        public bool? Share_Band_Future_Site_Share_Guaranteed { get; set; }
        public bool? Share_Band_Site_Share_Guaranteed { get; set; }
        public bool? Share_Band_Past_Site_Share_Guaranteed { get; set; }
        public bool? Share_Band_Supplier_Share_Guaranteed { get; set; }
        public bool? Share_Band_Future_Supplier_Share_Guaranteed { get; set; }
        public bool? Share_Band_Past_Supplier_Share_Guaranteed { get; set; }
        public string Share_Band_Past_End_Date { get; set; }
        public string Share_Band_Future_Start_Date { get; set; }
    }

    public class BandShareDateEntity
    {
        public string Share_Band_Past_End_Date { get; set; }
        public string Share_Band_Future_Start_Date { get; set; }
    }

    public class BandShareDataEntity
    {
        public float? Share_Band_Company_Share { get; set; }
        public float? Share_Band_Sec_Company_Share { get; set; }
        public float? Share_Band_Site_Share { get; set; }
        public float? Share_Band_Supplier_Rent { get; set; }
        public bool? Share_Band_Supplier_Rent_Guaranteed { get; set; }
        public float? Share_Band_Supplier_Share { get; set; }
    }

    public class MachineClassDetailsEntity
    {
        public string Machine_Class_Share_Past_Date { get; set; }
        public string Machine_Class_Share_Future_Date { get; set; }
        public int? Share_Band_ID_Present { get; set; }
        public int? Share_Band_ID_Past { get; set; }
        public int? Share_Band_ID_Future { get; set; }
    }

    public class ScheduleEntity
    {
        public string Share_Schedule_Name { get; set; }
        public string Share_Schedule_Description { get; set; }
        public int Share_Schedule_No_Bands { get; set; }
        public string Share_Schedule_Bands_Name_Type { get; set; }
    }
}
