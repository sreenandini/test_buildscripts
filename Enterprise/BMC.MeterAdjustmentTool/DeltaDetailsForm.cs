using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BMC.MeterAdjustmentTool.Helpers;
using System.Collections;
using BMC.Common.ExceptionManagement;
using Audit.Transport;
using Audit.BusinessClasses;
using BMC.Common;

namespace BMC.MeterAdjustmentTool
{
    public delegate void DeltaDetailsUpdateHandler(object sender, DeltaDetailsUpdateEventArgs e);
    public delegate void DeltaDetailsCloseHandler(object sender, DeltaDetailsUpdateEventArgs e);
    public delegate void DeltaDetailsFormClosedHandler(object sender, DeltaDetailsCloseEventArgs e);

    public partial class DeltaDetailsForm : DialogFormBase
    {
        #region CustomEvents

        public event DeltaDetailsUpdateHandler UpdateClick;
        public event DeltaDetailsCloseHandler CloseClick;

        #endregion

        public DataSet _dsDetails;
        public DataTable _dtDetail;
        public DataTable _dtRamReset;
        public DataTable _dtViewInfo;
        public Hashtable htChanges = new Hashtable();
        public Hashtable htCurrent = new Hashtable();
        public string HeaderText = string.Empty;
        public Boolean ShowRR = false;
        public Boolean ShowInfo = false;
        
        public int _installationNo {get; set;}
        public DateTime _readDatetime { get; set; }
        public DateTime _hourDatetime { get; set; }
        public string _headerText { get; set; }
        public string _hour { get; set; }
        public string _assetNo { get; set; }
        public DateTime _gamingDate { get; set; }
        public int _mgmdInstallationNo { get; set; }
        public DateTime _mgmdStartDateTime { get; set; }
        public DateTime _mgmdEndDateTime { get; set; }
        static string currencySymbol = CurrencyHelper.GetCurrencySymbol();

        public DeltaDetailsForm(DataSet ds, int installationNO, DateTime readDateTime, DateTime hourDateTime,string headerText, string hour, string assetNo, DateTime gamingDate, int mgmdInstallationNo, DateTime mgmdStartDatetime, DateTime mgmdEndDatetime)
        {
            _dsDetails = ds;
            InitializeComponent();
            dgvInfo.DataSource = null;
            _installationNo = installationNO;
            _readDatetime = readDateTime;
            _hourDatetime = hourDateTime;
            _headerText = headerText;
            _hour = hour;
            _assetNo = assetNo;
            _gamingDate = gamingDate;
            _mgmdInstallationNo = mgmdInstallationNo;
            _mgmdStartDateTime = mgmdStartDatetime;
            _mgmdEndDateTime = mgmdEndDatetime;

            // Set Tags for controls
            SetTagProperty();
        }

        private void SetTagProperty()
        {
            this.btnCancel.Tag = "Key_CancelCaption";
            this.btnUpdate.Tag = "Key_UpdateCaption";
            this.btnViewDetails.Tag = "Key_ViewInfo";
            this.lblCalcMeters.Tag = "Key_CalculatedMetersMandatory";
            this.Tag = "Key_DeltaAdjustment";
            this.btnViewRamReset.Tag = "Key_ViewRAMReset";
        }

        protected override void LoadChanges()
        {
            base.LoadChanges();           

            try
            {
                htChanges.Clear();
                htCurrent.Clear();
                dgvDetails.CellValueChanged += new DataGridViewCellEventHandler(dgvDetails_CellValueChanged);
                dgvDetails.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(dgvDetails_EditingControlShowing);
                dgvDetails.ColumnAdded += new DataGridViewColumnEventHandler(dgvDetails_ColumnAdded);
                _dtDetail = _dsDetails.Tables[MeterGlobals.ORIGINAL_TABLE];
                if (_dsDetails.Tables.Contains(MeterGlobals.RAMRESET_TABLE)) _dtRamReset = _dsDetails.Tables[MeterGlobals.RAMRESET_TABLE];
                if (_dsDetails.Tables.Contains(MeterGlobals.INFO_TABLE)) _dtViewInfo = _dsDetails.Tables[MeterGlobals.INFO_TABLE];

                dgvDetails.DataSource = _dtDetail;
                dgvDetails.Columns["Column"].Visible = false;

                foreach (DataGridViewColumn dc in dgvDetails.Columns)
                {
                    dc.ReadOnly = true;
                    dc.DefaultCellStyle.BackColor = System.Drawing.Color.LightGray;

                    // Get Header Texts from Resource
                    switch (dc.Name.ToUpper())
                    {
                        case "METER":
                            dc.Tag = "Key_Meter";
                            break;
                        case "START":
                            dc.Tag = "Key_Start";
                            break;
                        case "END":
                            dc.Tag = "Key_End";
                            break;
                        case "CURRENTDELTA":
                            dc.Tag = "Key_CurrentDelta";
                            break;
                        case "MODIFIEDVALUE":
                            dc.Tag = "Key_ModifiedValue";
                            break;
                    }
                }

                dgvDetails.Columns[dgvDetails.Columns.Count - 1].ReadOnly = false;
                dgvDetails.Columns[dgvDetails.Columns.Count - 1].DefaultCellStyle.BackColor = System.Drawing.Color.White;
                dgvRamResets.Visible = false;
                dgvInfo.Visible = false;
                pnlBottom.Visible = false;
                btnViewRamReset.Visible = (_dtRamReset != null);
                btnViewDetails.Visible = (_dtViewInfo != null);
                if (!btnViewRamReset.Visible) tblContent.ColumnStyles[1].Width = 0;
                if (!btnViewDetails.Visible) tblContent.ColumnStyles[2].Width = 0;
                dgvDetails.DataError += new DataGridViewDataErrorEventHandler(dgvDetails_DataError);

                // For externalization
                this.ResolveResources();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        public bool HasChanges { get; private set; }

        void dgvDetails_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_MAT_VALIDVALUE"));        //"Please enter a valid value!");
        }

        private void dgvDetails_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            float temp;

            try
            {
                if ((float.TryParse(dgvDetails.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString().Trim(), out temp)) ||
                    (dgvDetails.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "" ? 0
                    : (int)dgvDetails.Rows[e.RowIndex].Cells[e.ColumnIndex].Value) == 0)
                {
                    if (dgvDetails.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString().Trim() !=
                        dgvDetails.Rows[e.RowIndex].Cells["CurrentDelta"].Value.ToString().Trim())
                    {
                        if (!htChanges.Contains(dgvDetails.Rows[e.RowIndex].Cells["Column"].Value.ToString().Trim()))
                        {
                            htChanges.Add(dgvDetails.Rows[e.RowIndex].Cells["Column"].Value.ToString().Trim(),
                                (dgvDetails.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString().Trim() == "" ? "0"
                                : dgvDetails.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString().Trim()));
                            htCurrent.Add(dgvDetails.Rows[e.RowIndex].Cells["Column"].Value.ToString().Trim(),
                                dgvDetails.Rows[e.RowIndex].Cells["CurrentDelta"].Value.ToString().Trim());
                        }
                        else
                        {
                            htChanges[dgvDetails.Rows[e.RowIndex].Cells["Column"].Value.ToString().Trim()]
                                = dgvDetails.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString().Trim();
                            htCurrent[dgvDetails.Rows[e.RowIndex].Cells["Column"].Value.ToString().Trim()]
                                = dgvDetails.Rows[e.RowIndex].Cells["CurrentDelta"].Value.ToString().Trim();
                        }

                        dgvDetails.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = System.Drawing.Color.LightGreen;
                    }
                    else
                    {
                        if (htChanges.Contains(dgvDetails.Rows[e.RowIndex].Cells["Column"].Value.ToString().Trim()))
                        {
                            htChanges.Remove(dgvDetails.Rows[e.RowIndex].Cells["Column"].Value.ToString().Trim());
                            htCurrent.Remove(dgvDetails.Rows[e.RowIndex].Cells["Column"].Value.ToString().Trim());
                        }
                        dgvDetails.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = System.Drawing.Color.White;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void dgvDetails_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dgvDetails.CurrentCell.ColumnIndex == 5)
            {
                TextBox txtEdit = e.Control as TextBox;

                if (txtEdit.Text.Contains("."))
                {
                    txtEdit.MaxLength = 11;
                }
                else
                {
                    txtEdit.MaxLength = 9;
                }
                txtEdit.KeyPress += new KeyPressEventHandler(txtEdit_KeyPress);
            }
        }

        void txtEdit_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox txtEdit = sender as TextBox;

            if (txtEdit.Text.Contains("."))
            {
                txtEdit.MaxLength = 11;
            }
            else
            {
                txtEdit.MaxLength = 8;
            }
            if (_dtDetail.Columns[5].DataType.Name == "Double")
            {
                if ("-0123456789.\b".IndexOf(e.KeyChar) == -1)
                    e.Handled = true;
            }
            else if ("-0123456789\b".IndexOf(e.KeyChar) == -1)
                e.Handled = true;
        }

        private string GetMeterNameWithCurrency(string hdrKey)
        {
            string hdrNewKey = this.GetResourceTextByKey(hdrKey);
            if (hdrNewKey.Contains("#"))
            {
                hdrNewKey = hdrNewKey.Replace("#", currencySymbol);
            }
            return hdrNewKey;
        }

        private void dgvDetails_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && dgvDetails.Rows[e.RowIndex].Cells["Start"].Value.ToString() == "***")
            {
                DataTable meterInfo = new DataTable();
                bool isExists = false;
                
                meterInfo.Columns.Add(this.GetResourceTextByKey("Key_Meter"));

                meterInfo.Columns.Add(this.GetResourceTextByKey("Key_Start"));
                //UserInfo.Columns.Add("OnRamReset");
                //UserInfo.Columns.Add("AfterRamReset");
                meterInfo.Columns.Add(this.GetResourceTextByKey("Key_End"));
                meterInfo.Columns.Add(this.GetResourceTextByKey("Key_CurrentDelta"));

                //DataColumn dtcol = new DataColumn("Credits_Wagered");
                for (int i = 0; i < dgvDetails.Rows.Count; i++)
                {
                    if (dgvDetails.Rows[e.RowIndex].Cells["Meter"].Value.ToString() == GetMeterNameWithCurrency("Key_HrlyRdSch_AVG_BET"))            //  "AVERAGE BET"
                    {
                        isExists = true;
                        if (dgvDetails.Rows[i].Cells["Meter"].Value.ToString() == GetMeterNameWithCurrency("Key_HrlyRdSch_CREDITS_WAGERED"))            //  "CREDITS WAGERED"
                        {
                            DataRow drUserinfo = meterInfo.NewRow();
                            drUserinfo[0] = dgvDetails.Rows[i].Cells["Meter"].Value;
                            drUserinfo[1] = dgvDetails.Rows[i].Cells["Start"].Value;
                            //drUserinfo[2] = dgvDetails.Rows[i].Cells["OnRamReset"].Value;
                            //drUserinfo[3] = dgvDetails.Rows[i].Cells["AfterRamReset"].Value;
                            drUserinfo[2] = dgvDetails.Rows[i].Cells["End"].Value;
                            drUserinfo[3] = dgvDetails.Rows[i].Cells["CurrentDelta"].Value;
                            meterInfo.Rows.Add(drUserinfo);
                        }
                        if (dgvDetails.Rows[i].Cells["Meter"].Value.ToString() == GetMeterNameWithCurrency("Key_HrlyRdSch_GAMES_BET"))            //  "GAMES BET"
                        {
                            DataRow drUserinfo = meterInfo.NewRow();
                            drUserinfo[0] = dgvDetails.Rows[i].Cells["Meter"].Value;
                            drUserinfo[1] = dgvDetails.Rows[i].Cells["Start"].Value;
                            //drUserinfo[2] = dgvDetails.Rows[i].Cells["OnRamReset"].Value;
                            //drUserinfo[3] = dgvDetails.Rows[i].Cells["AfterRamReset"].Value;
                            drUserinfo[2] = dgvDetails.Rows[i].Cells["End"].Value;
                            drUserinfo[3] = dgvDetails.Rows[i].Cells["CurrentDelta"].Value;
                            meterInfo.Rows.Add(drUserinfo);
                        }
                    }
                    else if (dgvDetails.Rows[e.RowIndex].Cells["Meter"].Value.ToString() == GetMeterNameWithCurrency("Key_HrlyRdSch_CASINO_WIN"))           //  "CASINO WIN"
                    {
                        isExists = true;
                        if (dgvDetails.Rows[i].Cells["Meter"].Value.ToString() == GetMeterNameWithCurrency("Key_HrlyRdSch_CREDITS_WAGERED"))             //  "CREDITS WAGERED"
                        {
                            DataRow drUserinfo = meterInfo.NewRow();
                            drUserinfo[0] = dgvDetails.Rows[i].Cells["Meter"].Value;
                            drUserinfo[1] = dgvDetails.Rows[i].Cells["Start"].Value;
                            //drUserinfo[2] = dgvDetails.Rows[i].Cells["OnRamReset"].Value;
                            //drUserinfo[3] = dgvDetails.Rows[i].Cells["AfterRamReset"].Value;
                            drUserinfo[2] = dgvDetails.Rows[i].Cells["End"].Value;
                            drUserinfo[3] = dgvDetails.Rows[i].Cells["CurrentDelta"].Value;
                            meterInfo.Rows.Add(drUserinfo);
                        }
                        if (dgvDetails.Rows[i].Cells["Meter"].Value.ToString() == GetMeterNameWithCurrency("Key_HrlyRdSch_CREDITS_WON"))           //  "CREDITS WON"
                        {
                            DataRow drUserinfo = meterInfo.NewRow();
                            drUserinfo[0] = dgvDetails.Rows[i].Cells["Meter"].Value;
                            drUserinfo[1] = dgvDetails.Rows[i].Cells["Start"].Value;
                            //drUserinfo[2] = dgvDetails.Rows[i].Cells["OnRamReset"].Value;
                            //drUserinfo[3] = dgvDetails.Rows[i].Cells["AfterRamReset"].Value;
                            drUserinfo[2] = dgvDetails.Rows[i].Cells["End"].Value;
                            drUserinfo[3] = dgvDetails.Rows[i].Cells["CurrentDelta"].Value;
                            meterInfo.Rows.Add(drUserinfo);
                        }
                    }
                    else if (dgvDetails.Rows[e.RowIndex].Cells["Meter"].Value.ToString() == GetMeterNameWithCurrency("Key_HrlySch_OCCUPANCY"))        //  "OCCUPANCY"
                    {
                        isExists = true;
                        if (dgvDetails.Rows[i].Cells["Meter"].Value.ToString() == GetMeterNameWithCurrency("Key_HrlyRdSch_GAMES_BET"))              //  "GAMES BET"
                        {
                            DataRow drUserinfo = meterInfo.NewRow();
                            drUserinfo[0] = dgvDetails.Rows[i].Cells["Meter"].Value;
                            drUserinfo[1] = dgvDetails.Rows[i].Cells["Start"].Value;
                            //drUserinfo[2] = dgvDetails.Rows[i].Cells["OnRamReset"].Value;
                            //drUserinfo[3] = dgvDetails.Rows[i].Cells["AfterRamReset"].Value;
                            drUserinfo[2] = dgvDetails.Rows[i].Cells["End"].Value;
                            drUserinfo[3] = dgvDetails.Rows[i].Cells["CurrentDelta"].Value;
                            meterInfo.Rows.Add(drUserinfo);
                        }
                    }
                }

                if (isExists)
                {
                    new CalculatedMeterInfoForm(meterInfo).ShowDialogExAndDestroy(this);
                }
            }
        }

        private void dgvDetails_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            // e.Column.CellTemplate.Style = Style 
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                string oldValue = string.Empty;
                string newValue = string.Empty;

                if (htChanges.Count > 0)
                {
                    if (this.ShowQuestionMessageBox((this.GetResourceTextByKey(1, "MSG_MAT_UPDATEMETER"))) == DialogResult.Yes)           //"Are you sure to update the meter Values?"
                    {
                        this.Cursor = Cursors.WaitCursor;
                        DeltaDetailsUpdateEventArgs Ua = new DeltaDetailsUpdateEventArgs(this, htChanges, htCurrent);
                        UpdateClick(sender, Ua);

                        dgvDetails.DataSource = _dtDetail;
                        dgvDetails.Columns["Column"].Visible = false;
                        foreach (DataGridViewColumn dc in dgvDetails.Columns)
                        {
                            dc.ReadOnly = true;
                            dc.DefaultCellStyle.BackColor = System.Drawing.Color.LightGray;

                        }
                        dgvDetails.Columns[dgvDetails.Columns.Count - 1].ReadOnly = false;
                        dgvDetails.Columns[dgvDetails.Columns.Count - 1].DefaultCellStyle.BackColor = System.Drawing.Color.White;

                        this.HasChanges = true;
                        this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_SAVE_SUCCESS")); 
                        foreach(var _key in htChanges.Keys)
                        {
                            string key = _key.ToString();
                            if (htCurrent.ContainsKey(key))                            
                                oldValue = htCurrent[key].ToString();                            
                            if (htChanges.ContainsKey(key))                            
                                newValue = htChanges[key].ToString();
                            
                           // InsertNewAuditEntry(ModuleNameEnterprise.MeterAdjustement, "Meter Adjustement Tool", key.ToString(), newValue, oldValue, MainForm._securityUserID, MainForm._userName );
                        }
                        
                        //   dgvDetails.Columns[2].DefaultCellStyle.BackColor = System.Drawing.Color.LightGray;
                        CloseClick(sender, Ua);
                        htChanges.Clear();

                        htCurrent.Clear();
                        this.Close();
                        this.Cursor = Cursors.Default;
                    }
                }
                else
                {
                    this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_MAT_NOCHANGES"));        //"No changes to update!");
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }


        public void InsertNewAuditEntry(ModuleNameEnterprise moduleName, string strScreenName, string strField, string strNewItem, string strOldItem, int iUserId, string strUsername)
        {
            try
            {
                //Calling Audit Method
                Audit_History AH = new Audit_History();
                //Populate required Values            
                AH.EnterpriseModuleName = moduleName;
                AH.Audit_Screen_Name = strScreenName;
                if (_headerText == this.GetResourceTextByKey("Key_SuspectedDailyReads"))   // "Suspected Daily Reads")
                {
                    AH.Audit_Desc = "Meter Adjustment done for [Read] : InstallationNo - [" + _installationNo + "], Read Date - [" + _readDatetime + "]";
                }
                else if (_headerText == this.GetResourceTextByKey("Key_SuspectedHourlyData"))   // "Suspected Hourly Data")
                {
                    AH.Audit_Desc = "Meter Adjustment done for [Hourly] : InstallationNo - [" + _installationNo + "], Date - [" + _hourDatetime.ToShortDateString() + "], Hour - [" + _hour + "]";
                }
                else if (_headerText == this.GetResourceTextByKey("Key_SuspectedCollectionDetails"))   // "Suspected Collection Details")
                {
                    AH.Audit_Desc = "Meter Adjustment done for [Collection] : InstallationNo - [" + _installationNo + "], Collection Date Time - [" + _gamingDate + "]";
                }
                else if (_headerText == this.GetResourceTextByKey("Key_SuspectedMGMDSessionDeltas"))   // "Suspected MGMD Session Deltas")
                {
                    AH.Audit_Desc = "Meter Adjustment done for [MGMD Session Delta] : InstallationNo - [" + _mgmdInstallationNo + "], MGMD Start Date Time - [" + _mgmdStartDateTime + "], MGMD End Date Time - [" + _mgmdEndDateTime +"]";
                }

                AH.AuditOperationType = OperationType.MODIFY;
                AH.Audit_Field = strField;
                AH.Audit_New_Vl = strNewItem;
                AH.Audit_Slot = string.Empty;
                AH.Audit_Old_Vl = strOldItem;

                AH.Audit_User_ID = iUserId;
                AH.Audit_User_Name = strUsername;

                AuditViewerBusiness AVB = new AuditViewerBusiness(Common.Utilities.DatabaseHelper.GetConnectionString());
                AVB.InsertAuditData(AH, true);

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btnViewRamReset_Click(object sender, EventArgs e)
        {
            try
            {
                if (false == ShowRR)
                {
                    if (_dtRamReset.Rows.Count > 0)
                    {
                        btnViewRamReset.Text = this.GetResourceTextByKey("Key_HideRAMReset");   // "Hide RAM Reset";
                        pnlBottom.Visible = true;
                        dgvRamResets.Visible = true;
                        btnViewDetails.Enabled = false;
                        dgvInfo.Visible = false;
                        dgvRamResets.DataSource = _dtRamReset;
                        ShowRR = true;
                    }
                    else
                    {
                        this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_MAT_NORAMRESET"));        //"No RAM reset events occured in this range");
                    }
                }
                else
                {
                    btnViewRamReset.Text = this.GetResourceTextByKey("Key_ViewRAMReset");     //"View RAM Reset";
                    pnlBottom.Visible = false;
                    dgvRamResets.Visible = false;
                    btnViewDetails.Enabled = true;
                    ShowRR = false;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btnViewDetails_Click(object sender, EventArgs e)
        {
            try
            {
                if (false == ShowInfo)
                {
                    if (_dtViewInfo.Rows.Count > 0)
                    {
                        btnViewDetails.Text = this.GetResourceTextByKey("Key_HideInfoCaption");     //"Hide Info";
                        pnlBottom.Visible = true;
                        dgvInfo.Visible = true;
                        dgvRamResets.Visible = false;
                        btnViewRamReset.Enabled = false;
                        dgvInfo.DataSource = _dtViewInfo;
                        ShowInfo = true;
                    }
                }
                else
                {
                    btnViewDetails.Text = this.GetResourceTextByKey("Key_ViewInfo");         //"View Info";
                    pnlBottom.Visible = false;
                    dgvInfo.Visible = false;
                    btnViewRamReset.Enabled = true;
                    ShowInfo = false;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
    }

    public class DeltaDetailsUpdateEventArgs : System.EventArgs
    {
        private Hashtable htChanges;
        private Hashtable htCurrent;

        public DeltaDetailsUpdateEventArgs(DeltaDetailsForm ownerForm, Hashtable Cha, Hashtable Curr)
        {
            this.OwnerForm = ownerForm;
            this.htChanges = Cha;
            this.htCurrent = Curr;
        }

        public Hashtable Changed()
        {
            return htChanges;
        }

        public Hashtable Current()
        {
            return htCurrent;
        }

        public DeltaDetailsForm OwnerForm { get; private set; }
    }

    public class DeltaDetailsCloseEventArgs : System.EventArgs
    {
        public DeltaDetailsCloseEventArgs(bool hasChanged)
        {
            this.HasChanged = hasChanged;
        }

        public bool HasChanged { get; private set; }

        public DataTable ViewInfo { get; set; }
    }
}
