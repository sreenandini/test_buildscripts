using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BMC.EnterpriseBusiness.Entities;
using BMC.EnterpriseBusiness.Business;
using BMC.Common.ExceptionManagement;
using System.Diagnostics;
using BMC.CoreLib.Win32;
using Stacker.Business;
using System.Text.RegularExpressions;
using BMC.EnterpriseDataAccess;
using BMC.Common.Utilities;
using Audit.Transport;
using BMC.Common;


namespace BMC.EnterpriseClient.Views
{
    public partial class UCGameCapping : UserControl, IAdminSite
    {
        #region User Defined Variables
        public GameCappingBiz ObjGameCappingBiz = null;
        public int iSiteID = 0;
        private List<int> lst_Changes = new List<int>();
        List<GetCardLevelSettings> lst_CardLevel = null;
        GetGameCappingParametersEntity objGameCapParam = null;
        DataGridViewRow row;
        DataGridViewCell cell;
        string ErrorFalls = "";
        List<int> lst_Delete = new List<int>();
        List<string> str_error = new List<string>();
        string sSite_Code = string.Empty;
        int iMaxMintsToCap = 0;
        bool bIsExpirePercentEnabled = false;
        string sError = string.Empty;
        #endregion User Defined Variables

        #region Constructor
        public UCGameCapping()
        {
            InitializeComponent();
            ((TextBox)nudMintsExpireGameCapping.Controls[1]).MaxLength = 3;
            // Set Tags for controls
            SetTagProperty();
        }

        private void SetTagProperty()
        {
            this.btnAdd.Tag = "Key_AddCaption";
            this.btnDelete.Tag = "Key_DeleteCaption";
            this.chkEmployeeReserve.Tag = "Key_AllowEmployeeToReserveGame";
            this.chkPlayerReserve.Tag = "Key_AllowPlayerToReserveGame";
            this.chkCapRelease.Tag = "Key_CapReleaseColon";
            this.grpCardLevelSetting.Tag = "Key_CardLevelSettings";
            this.grpSlotSetting.Tag = "Key_GameCappingSettings";
            this.lblMintsExpireGameCapping.Tag = "Key_MintsToExpire";
            this.btnSearch.Tag = "Key_SearchCaption";
            this.label1.Tag = "Key_SearchCardLevelColon";
        }
        #endregion Constructor

        #region Events
        /// <summary>
        /// Add only CardLevel Value of row whose value has been changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvCardLevelSettings_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int iChangesValue = 0;
                bool bValid = false;
                bool isValid = false;
                int iValue = 0;
                string Cellindex = (e.RowIndex + "," + e.ColumnIndex);
                //Get the initial row values
                if (e.RowIndex == -1)
                    return;
                row = gvCardLevelSettings.Rows[e.RowIndex];
                cell = row.Cells[e.ColumnIndex];
                //save the changes made to the selected row
                if (int.TryParse(gvCardLevelSettings.Rows[e.RowIndex].Cells[0].Value.ToString(), out iChangesValue))
                {
                    if (iChangesValue >= 0 && !lst_Changes.Contains(iChangesValue))
                    {
                        lst_Changes.Add(iChangesValue);
                        lst_Delete.Remove(iChangesValue);
                    }
                }
                // set the flag for card level ( check if already exists -1, value cannot be null -2)
                int iCardLevel = Convert.ToInt32(gvCardLevelSettings.Rows[e.RowIndex].Cells[0].Value);
                if (iCardLevel == 0)
                    iValue = 2;
                else if (lst_CardLevel.FindAll(obj => obj.CardLevel == iCardLevel).Count > 1)
                    iValue = 1;
                switch (gvCardLevelSettings.Columns[e.ColumnIndex].Name)
                {
                    case "MintstoCap":
                        if (cell.Value == null)
                        {
                            cell.ErrorText = this.GetResourceTextByKey(1, "MSG_VALUE_EMPTY");         //"Value cannot be empty.";
                            cell.Style.BackColor = Color.Bisque;
                            btnAdd.Enabled = false;
                            return;
                        }
                        //Check if the mints to cap is valid 
                        int iValid = IsValidMintsToCap(cell.Value.ToString());
                        switch (iValid)
                        {
                            case 1:
                                cell.ErrorText = this.GetResourceTextByKey(1,"MSG_OPTIONALTIME");         // "Enter only 5 Optional Time.";
                                cell.Style.BackColor = Color.Bisque;
                                bValid = true;
                                break;
                            case 2:
                                cell.ErrorText = this.GetResourceTextByKey(1,"MSG_MINUTESTOCAPTIME");         // "Minutes to Cap Time should be greater than Expire Time";
                                cell.Style.BackColor = Color.Bisque;
                                bValid = true;
                                break;
                            case 3:
                                cell.ErrorText = this.GetResourceTextByKey(1,"MSG_NUMBERAFTERCOMMA");         // "Enter a number after Comma Separator.";
                                cell.Style.BackColor = Color.Bisque;
                                bValid = true;
                                break;
                            case 5:
                                cell.ErrorText = string.Format(this.GetResourceTextByKey(1,"MSG_MAXDIGITSFOROPTION"),iMaxMintsToCap);         // "Maximum of " + iMaxMintsToCap + " digits allowed for an Option.";
                                cell.Style.BackColor = Color.Bisque;
                                bValid = true;
                                break;
                            case 6:
                                cell.ErrorText = this.GetResourceTextByKey(1,"MSG_VALUE_ZERO");         // "Value cannot be Zero";
                                cell.Style.BackColor = Color.Bisque;
                                bValid = true;
                                break;
                            case 7:
                                cell.ErrorText = this.GetResourceTextByKey(1,"MSG_MAX_MINTSTOCAP");         // "Maximum allowed time for Minutes To Cap is '1092' minutes.";
                                cell.Style.BackColor = Color.Bisque;
                                bValid = true;
                                break;
                            case 0:
                                this.ShowErrorMessageBox(this.GetResourceTextByKey(1,"MSG_EXPIRETIME_EMPTY"),this.ParentForm.Text);         // "Expire Time cannot be empty.");
                                break;
                        }
                        if (iValid == 4)
                        {
                            if (str_error.Contains(Cellindex))
                                str_error.Remove(Cellindex);

                        }
                        else
                        {
                            if (ErrorFalls != Cellindex && !str_error.Contains(Cellindex))
                                str_error.Add(Cellindex);
                        }
                        break;
                    case "CardLevel":
                        switch (iValue)
                        {
                            case 1:
                                cell.ErrorText = this.GetResourceTextByKey(1,"MSG_CARDLEVEL_EXISTS");         //"Card Level already Exists";
                                cell.Style.BackColor = Color.Bisque;
                                bValid = true;
                                break;
                            case 2:
                                cell.ErrorText = this.GetResourceTextByKey(1,"MSG_CARDLEVEL_ZERO");         //"Card Level value cannot be Zero";
                                cell.Style.BackColor = Color.Bisque;
                                bValid = true;
                                break;
                        }
                        if (iValue == 0)
                        {

                            if (str_error.Contains(Cellindex))
                            {
                                str_error.Remove(Cellindex);
                            }
                            RemoveCardLevel();
                        }
                        else
                        {
                            if (ErrorFalls != Cellindex && !str_error.Contains(Cellindex))
                                str_error.Add(Cellindex);
                        }
                        break;
                    case "MaxNoofMachinestoCap":
                        if (cell.Value == null || cell.Value.ToString() == "0")
                        {
                            cell.ErrorText = this.GetResourceTextByKey(1, "MSG_VALUE_ZERO");         //"Value Cannot Be Zero";
                            cell.Style.BackColor = Color.Bisque;
                            bValid = true;
                            if (ErrorFalls != Cellindex && !str_error.Contains(Cellindex))
                                str_error.Add(Cellindex);
                            break;
                        }
                        else
                        {
                            if (str_error.Contains(Cellindex))
                                str_error.Remove(Cellindex);
                        }
                        break;
                }
                if (lst_CardLevel.Count != 0)
                {
                    foreach (GetCardLevelSettings ocard in lst_CardLevel)
                    {
                        isValid = ocard.CardLevel != 0 && ocard.MaxNoofMachinestoCap != 0 && ocard.MintstoCap != "0";
                    }
                }
                btnAdd.Enabled = (str_error.Count <= 0 && gvCardLevelSettings.RowCount < 99 && isValid);
                btnDelete.Enabled = (str_error.Count <= 0 && isValid);
                //if valid, proceed.
                if (!bValid)
                {
                    cell.ErrorText = "";
                    cell.Style.BackColor = SystemColors.Window;
                    ErrorFalls = "";
                }
                else
                {
                    ErrorFalls = e.RowIndex + "," + e.ColumnIndex;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        /// <summary>
        /// Validation to allow only numbers in GridView.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvCardLevelSettings_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            try
            {
                e.Control.KeyPress += new KeyPressEventHandler(tb_KeyPress);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        void tb_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                //check if the entered value is alphanumeric 
                if (gvCardLevelSettings.CurrentCell.ColumnIndex != 2)
                    e.Handled = (!char.IsControl(e.KeyChar)
                    && !char.IsDigit(e.KeyChar));
                else
                    e.Handled = (!char.IsControl(e.KeyChar)
                    && !char.IsDigit(e.KeyChar) && e.KeyChar != ',');
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
        /// <summary>
        /// To Delete Card Level
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (gvCardLevelSettings.SelectedRows.Count >= 1)
                {
                    if (this.ShowQuestionMessageBox(this.GetResourceTextByKey(1, "MSG_CARDLEVEL_DELETE"), this.ParentForm.Text) == DialogResult.Yes)    // "Do you want to Delete the CardLevel"
                    {
                        foreach (DataGridViewRow row in gvCardLevelSettings.SelectedRows)
                        {
                            lst_Delete.Add(Convert.ToInt32(row.Cells[0].Value));
                        }
                        if (lst_Delete.Count != 0)
                        {
                            foreach (int iCardLevel in lst_Delete)
                            {
                                lst_CardLevel.RemoveAll(obj => obj.CardLevel == iCardLevel);
                                lst_Changes.Remove(iCardLevel);
                            }
                            gvCardLevelSettings.DataSource = null;
                            if (lst_CardLevel == null || lst_CardLevel.Count == 0)
                            {
                                lst_CardLevel = new List<GetCardLevelSettings>();
                                lst_CardLevel.Add(new GetCardLevelSettings { CardLevel = 0, MaxNoofMachinestoCap = 0, MintstoCap = "0" });
                            }
                            gvCardLevelSettings.DataSource = lst_CardLevel;
                            //this.ShowMessageBox(this.GetResourceTextByKey(1, "MSG_SAVECHANGES"), this.ParentForm.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);     // Click Apply to save the changes.
                           
                            ResizeHeader();

                            btnAdd.Enabled=(lst_CardLevel.FindAll(obj => obj.CardLevel == 0).Count>=1)?false:true; 
                            if (gvCardLevelSettings.Rows.Count > 1)
                            {
                                foreach (DataGridViewRow row in gvCardLevelSettings.Rows)
                                {
                                    if (Convert.ToInt32(row.Cells[0].Value) == 0)
                                    {
                                        row.Cells[0].ReadOnly = false;
                                        row.Cells[0].Style.BackColor = Color.White;
                                    }
                                    else
                                    {
                                        gvCardLevelSettings.Columns[0].ReadOnly = true;
                                        gvCardLevelSettings.Columns[0].DefaultCellStyle.BackColor = Color.Silver;
                                    }
                                }
                            }
                            this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_SAVECHANGES"), this.ParentForm.Text);
                        }

                    }
                }
                else
                {
                    //this.ShowMessageBox(this.GetResourceTextByKey(1, "MSG_CARDLEVEL_SELECT"), MessageBoxButtons.OK, MessageBoxIcon.Information);     // "Please select Row header to Delete the CardLevel."
                    this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_CARDLEVEL_SELECT"),this.ParentForm.Text);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
        /// <summary>
        /// Add new card level to GridView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            //Add a new level to the Grid.
            try
            {
                lst_CardLevel.Add(new GetCardLevelSettings { CardLevel = 0, MaxNoofMachinestoCap = 0, MintstoCap = "0" });
                gvCardLevelSettings.DataSource = null;
                ////set the data source 
                gvCardLevelSettings.DataSource = lst_CardLevel;
                ResizeHeader();

                //Make the Last row as selected by default 
                int iRowIndex = lst_CardLevel.Count();
                if (iRowIndex != 0 && gvCardLevelSettings.DataSource != null)
                    gvCardLevelSettings.CurrentCell = gvCardLevelSettings.Rows[iRowIndex - 1].Cells[0];
                btnAdd.Enabled = false;
                btnDelete.Enabled = true;
                foreach (DataGridViewRow row in gvCardLevelSettings.Rows)
                {
                    gvCardLevelSettings.Columns[0].ReadOnly = true;
                    gvCardLevelSettings.Columns[0].DefaultCellStyle.BackColor = Color.Silver;
                }
                if (iRowIndex > 1)
                {
                    gvCardLevelSettings.Rows[iRowIndex - 1].Cells[0].ReadOnly = false;
                    gvCardLevelSettings.Rows[iRowIndex-1].Cells[0].Style.BackColor = Color.White;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        /// <summary>
        /// Searches the CardLevel From GridView.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtSearch.Text))
                {
                    bool bValid = false;
                    string sSearch = txtSearch.Text.Trim();
                    foreach (DataGridViewRow row in gvCardLevelSettings.Rows)
                    {
                        //search for the matching row    
                        gvCardLevelSettings.CurrentCell = gvCardLevelSettings[0, 0];
                        if (row.Cells[0].Value.ToString().Equals(sSearch))
                        {
                            //Found the row                         
                            gvCardLevelSettings.CurrentCell = row.Cells[0];
                            row.Selected = true;
                            bValid = true;
                            break;
                        }
                    }
                    if (!bValid)
                    {
                        //this.ShowMessageBox(string.Format(this.GetResourceTextByKey(1, "MSG_CARDLEVEL_NOTEXISTS"), sSearch), this.ParentForm.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);    //"Card Level " + sSearch + " does not Exist."
                        this.ShowInfoMessageBox(string.Format(this.GetResourceTextByKey(1, "MSG_CARDLEVEL_NOTEXISTS"), sSearch), this.ParentForm.Text);
                        return;
                    }
                }
                else
                {
                   // this.ShowMessageBox(this.GetResourceTextByKey(1, "MSG_SEARCHVALUE_EMPTY"), this.ParentForm.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);        // "Search value is empty"
                    this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_SEARCHVALUE_EMPTY"), this.ParentForm.Text);
                    return;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
        /// <summary>
        /// Handles DBNUll Exception
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvCardLevelSettings_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            try
            {
                return;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
        /// <summary>
        /// Allows only Numbers.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                e.Handled = (!char.IsControl(e.KeyChar)
                   && !char.IsDigit(e.KeyChar));
            }
            finally
            {
            }

        }
        /// <summary>
        /// Allow only Numbers.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nudMintsExpireGameCapping_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                e.Handled = !char.IsControl(e.KeyChar)
                  && !char.IsDigit(e.KeyChar);
            }
            finally
            {
            }

        }
        /// <summary>
        /// Validates Minutes To Cap
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nudMintsExpireGameCapping_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                ClearMinstoCapErrorMSg();
                
                if (ValidateMinstoCap())
                {
                    btnAdd.Enabled = (gvCardLevelSettings.RowCount < 99 && str_error.Count <= 0);
                    btnDelete.Enabled = str_error.Count <= 0;
                }
            }
            finally
            {

            }
        }
        #endregion Events

        #region UserDefined Function

        /// <summary>
        /// Load Settings details into UI.
        /// </summary>
        /// <param name="entity"></param>
        public void LoadDetails(AdminSiteEntity entity)
        {
            if (entity.Site_ID < 1)
            {
                tblMainFrame.Enabled = false;
                return;
            }
            try
            {
                ObjGameCappingBiz = new GameCappingBiz();

                try
                {
                    iMaxMintsToCap = Convert.ToInt32(BMC.Common.ConfigurationManagement.ConfigManager.Read("MinutesToCap"));
                    iMaxMintsToCap = iMaxMintsToCap > 4 ? 4 : iMaxMintsToCap;
                }
                catch (Exception)
                {
                    iMaxMintsToCap = 4;
                }
                if (nudMintsExpireGameCapping.Value > 240)
                    //this.ShowMessageBox(this.GetResourceTextByKey(1, "MSG_MAXEXPIRETIME"), this.ParentForm.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);              // "Maximum value for Minutes to expire is 240"
                    this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_MAXEXPIRETIME"), this.ParentForm.Text);
                lst_Changes.Clear();
                iSiteID = entity.Site_ID;
                sSite_Code = entity.Site_Code;

                gvCardLevelSettings.DataSource = null;
                objGameCapParam = ObjGameCappingBiz.GetGameMappingParameters(iSiteID);
                bIsExpirePercentEnabled = (BMC.Common.ConfigurationManagement.ConfigManager.Read("IsExpirePercentEnabled").ToUpper() == "TRUE");

                lblMintsExpireGameCapping.Visible = bIsExpirePercentEnabled;

                nudMintsExpireGameCapping.Visible = bIsExpirePercentEnabled;

                //bind the Game capping parameters 
                if (objGameCapParam != null)
                {
                    chkCapRelease.Checked = objGameCapParam.CapReleaseOnPlayerCardIn;
                    chkEmployeeReserve.Checked = objGameCapParam.ReserveGameForEmployee;
                    chkPlayerReserve.Checked = objGameCapParam.ReserveGameForPlayer;
                    nudMintsExpireGameCapping.Text = objGameCapParam.MintsToExpire.ToString();
                }
                else
                {
                    objGameCapParam = new GetGameCappingParametersEntity();
                    nudMintsExpireGameCapping.Value = 0;
                }
                //get card level settings
                lst_CardLevel = ObjGameCappingBiz.GetCardLevelSettings(iSiteID);
                if (lst_CardLevel == null)
                {
                    lst_CardLevel = new List<GetCardLevelSettings>();
                    lst_CardLevel.Add(new GetCardLevelSettings { CardLevel = 0, MaxNoofMachinestoCap = 0, MintstoCap = "0" });
                }
                //set the data source for the grid
                gvCardLevelSettings.DataSource = lst_CardLevel;
                //gvCardLevelSettings
                ResizeHeader();
                btnAdd.Enabled = gvCardLevelSettings.RowCount <= 99 && (!(lst_CardLevel.FindAll(obj => obj.CardLevel == 0).Count == 1 && lst_CardLevel.Count == 1)); ;
                btnDelete.Enabled = (!(lst_CardLevel.FindAll(obj => obj.CardLevel == 0).Count == 1 && lst_CardLevel.Count == 1));
                if (gvCardLevelSettings.Rows.Count > 1)
                {
                    foreach (DataGridViewRow row in gvCardLevelSettings.Rows)
                    {
                        gvCardLevelSettings.Columns[0].ReadOnly = true;
                        gvCardLevelSettings.Columns[0].DefaultCellStyle.BackColor = Color.Silver;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            finally
            {
                if (iSiteID != 0 && !AppGlobals.Current.HasUserAccess("HQ_Admin_Customers_Site_Edit"))
                {
                    grpSlotSetting.Enabled = false;
                    grpCardLevelSetting.Enabled = false;
                    btnAdd.Enabled = false;
                    btnDelete.Enabled = false;
                }
            }
        }
        /// <summary>
        /// Saves all the Game Capping Settings into GameCapping table.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool SaveDetails(AdminSiteEntity entity)
        {
            if (!tblMainFrame.Enabled)
            {
                return true;
            }
            try
            {
                bool bExport = false;
                if (ValidateGameCapSettings(entity))
                {
                    //Saves GameCapping Settings Details                                 
                    if (objGameCapParam.CapReleaseOnPlayerCardIn != chkCapRelease.Checked || objGameCapParam.ReserveGameForPlayer != chkPlayerReserve.Checked ||
                        objGameCapParam.ReserveGameForEmployee != chkEmployeeReserve.Checked ||
                        objGameCapParam.MintsToExpire != Convert.ToInt32(nudMintsExpireGameCapping.Text))
                    {
                        ObjGameCappingBiz.InsertGameCappingParameters(chkCapRelease.Checked, chkPlayerReserve.Checked,
                                    chkEmployeeReserve.Checked, Convert.ToInt32(nudMintsExpireGameCapping.Text), entity.Site_ID, AppGlobals.Current.UserId, Convert.ToInt32(ModuleNameEnterprise.AUDIT_GAMECAPPING), ModuleNameEnterprise.AUDIT_GAMECAPPING.ToString(), "Game Capping");
                        bExport = true;
                    }

                    //Saves Game Card Level Settings 
                    if (lst_Changes.Count != 0)
                    {
                        foreach (int CardLevelId in lst_Changes)
                        {
                            GetCardLevelSettings oCard = lst_CardLevel.Find(obj => obj.CardLevel == CardLevelId);
                            if (oCard == null) continue;
                            ObjGameCappingBiz.InsertCardLevelSettings(Convert.ToInt32(oCard.MaxNoofMachinestoCap.ToString().Trim()), Convert.ToInt32(oCard.CardLevel.ToString().Trim()), oCard.MintstoCap.Trim(), entity.Site_ID.ToString(), AppGlobals.Current.UserId, Convert.ToInt32(ModuleNameEnterprise.AUDIT_GAMECAPPING), ModuleNameEnterprise.AUDIT_GAMECAPPING.ToString(), "Game Capping");
                        }
                        bExport = true;
                    }

                    if (lst_Delete.Count != 0)
                    {
                        foreach (int CardLevel in lst_Delete)
                        {
                            if (CardLevel == 0) continue;
                            ObjGameCappingBiz.DeleteGameCappingCardLevel(CardLevel, sSite_Code, AppGlobals.Current.UserId, Convert.ToInt32(ModuleNameEnterprise.AUDIT_GAMECAPPING), ModuleNameEnterprise.AUDIT_GAMECAPPING.ToString(), "Game Capping");
                        }
                        bExport = true;
                        lst_Delete.Clear();
                    }

                    if (bExport)
                        ObjGameCappingBiz.ExportHistory(entity.Site_ID.ToString(), "GAMECAPPING", entity.Site_ID);

                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return false;
            }
            //return true;
        }
        /// <summary>
        /// Validation for Minutes To Cap Game
        /// </summary>
        /// <param name="MintsToCap"></param>
        /// <returns></returns>
        public int IsValidMintsToCap(string MintsToCap)
        {
            try
            {
                string sAppendValue = string.Empty;
                int iSplitCount = MintsToCap.Split(',').Count();
                StringBuilder oString = new StringBuilder();

                for (int i = iSplitCount; i < 5; i++)
                {
                    sAppendValue = oString.Append(",1").ToString();
                }

                MintsToCap = string.Concat(MintsToCap, sAppendValue);

                //Validate the 5 time options
                Regex rgxTimePattern = new Regex(@"^(\d(?:\d(?:\d(?:\d)?)?)?)\,(\d(?:\d(?:\d(?:\d)?)?)?)\,(\d(?:\d(?:\d(?:\d)?)?)?)\,(\d(?:\d(?:\d(?:\d)?)?)?)\,(\d(?:\d(?:\d(?:\d)?)?)?)$", RegexOptions.IgnoreCase);
                Match mtcTimeValues = rgxTimePattern.Match(MintsToCap);

                string[] sValidateMints = MintsToCap.Split(',');

                if ((sValidateMints[0].Length > iMaxMintsToCap) || (sValidateMints[1].Length > iMaxMintsToCap) || (sValidateMints[2].Length > iMaxMintsToCap) || (sValidateMints[3].Length > iMaxMintsToCap) || (sValidateMints[4].Length > iMaxMintsToCap))
                    return 5;

                if (nudMintsExpireGameCapping.Text == "")
                {
                    return 0;//(false)
                }

                if (sValidateMints[0] == "" || sValidateMints[1] == "" || sValidateMints[2] == "" || sValidateMints[3] == "" || sValidateMints[4] == "")
                    return 3;

                if (Convert.ToInt32(sValidateMints[0]) == 0 || Convert.ToInt32(sValidateMints[1]) == 0
                      || Convert.ToInt32(sValidateMints[2]) == 0 || Convert.ToInt32(sValidateMints[3]) == 0
                      || Convert.ToInt32(sValidateMints[4]) == 0 )
                {
                    return 6;//Value Should not be Zero.
                }

                if (mtcTimeValues.Groups.Count == 1)
                {
                    return 1;//only 5 Optional Time Allowed.(false)
                }

                int iMintsToExpire = Convert.ToInt32(nudMintsExpireGameCapping.Text);

                if (nudMintsExpireGameCapping.Visible)
                {
                    if (!(Convert.ToInt32(sValidateMints[0]) >= iMintsToExpire && (Convert.ToInt32(sValidateMints[1]) >= iMintsToExpire || Convert.ToInt32(sValidateMints[1]) == 0)
                       && (Convert.ToInt32(sValidateMints[2]) >= iMintsToExpire || Convert.ToInt32(sValidateMints[2]) == 0) && (Convert.ToInt32(sValidateMints[3]) >= iMintsToExpire || Convert.ToInt32(sValidateMints[3]) == 0)
                       && (Convert.ToInt32(sValidateMints[4]) >= iMintsToExpire )))
                    {
                        return 2;//Mints To Expire > Expiry Time(false)
                    }
                }

                //validation for maximum mins can be set.
                if (Convert.ToInt32(sValidateMints[0]) > 1092 || Convert.ToInt32(sValidateMints[1]) > 1092
                     || Convert.ToInt32(sValidateMints[2]) > 1092 || Convert.ToInt32(sValidateMints[3]) > 1092
                     || Convert.ToInt32(sValidateMints[4]) > 1092 )
                    return 7;

                return 4;//true
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return 3;//Comma Should Follow With number.(false)
            }
        }
        /// <summary>
        /// Clears the Error if values are swapped.
        /// </summary>
        private void RemoveCardLevel()
        {
            try
            {
                for (int i = 0; i < str_error.Count; i++)
                {

                    string[] strCell = str_error[i].Split(',');
                    if (strCell[1] == "0")
                    {
                        int column = Convert.ToInt32(strCell[1]);
                        int row = Convert.ToInt32(strCell[0]);
                        int Cardlevel = Convert.ToInt32(gvCardLevelSettings[column, row].Value);
                        int oCard = lst_CardLevel.FindAll(obj => obj.CardLevel == Cardlevel).Count;
                        if (oCard == 1)
                        {
                            gvCardLevelSettings[column, row].ErrorText = "";
                            gvCardLevelSettings[column, row].Style.BackColor = SystemColors.Window;
                            if (str_error.Contains(str_error[i]))
                            {
                                str_error.Remove(str_error[i]);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        /// <summary>
        /// Grid View Size and Header Formatting.
        /// </summary>
        public void ResizeHeader()
        {
            try
            {
                gvCardLevelSettings.Columns[0].Width = 100;
                gvCardLevelSettings.Columns[1].Width = 212;
                gvCardLevelSettings.Columns[2].Width = 212;
                gvCardLevelSettings.Columns[3].Visible = false;
                DataGridViewTextBoxColumn oRestrictLength = (DataGridViewTextBoxColumn)gvCardLevelSettings.Columns[0];
                DataGridViewTextBoxColumn oRestrictLength1 = (DataGridViewTextBoxColumn)gvCardLevelSettings.Columns[1];
                DataGridViewTextBoxColumn oRestrictLength2 = (DataGridViewTextBoxColumn)gvCardLevelSettings.Columns[2];
                //oRestrictLength.HeaderText = this.GetResourceTextByKey("Key_CardLevel");         // "Card Level";
                //oRestrictLength1.HeaderText = this.GetResourceTextByKey("Key_MaxMintsToCap");    //"Max No of Machines To Cap";
                //oRestrictLength2.HeaderText = this.GetResourceTextByKey("Key_MaxMintsToCapWithCommas");    //"Max Minutes To Cap(Separated By Commas)";

                oRestrictLength.Tag = "Key_CardLevel";         // "Card Level";
                oRestrictLength1.Tag = "Key_MaxMintsToCap";    //"Max No of Machines To Cap";
                oRestrictLength2.Tag = "Key_MaxMintsToCapWithCommas";    //"Max Minutes To Cap(Separated By Commas)";
                gvCardLevelSettings.ResolveResources();

                oRestrictLength.MaxInputLength = 2;
                oRestrictLength1.MaxInputLength = 3;
                oRestrictLength2.MaxInputLength = 24;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
        /// <summary>
        /// Validates the grid for error.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        private bool ValidateGameCapSettings(AdminSiteEntity entity)
        {
            bool bResult = true;

            try
            {
                if (entity.Site_ID > 0)
                {
                    //Is Mins to exipre is empty
                    if (nudMintsExpireGameCapping.Visible)
                    {
                        if (string.IsNullOrEmpty(nudMintsExpireGameCapping.Text.Trim()))
                        {
                            //this.ShowMessageBox(this.GetResourceTextByKey(1, "MSG_EXPIRETIME_EMPTY"), this.ParentForm.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);             // "Expire Time cannot be empty"
                            this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_EXPIRETIME_EMPTY"), this.ParentForm.Text);
                            bResult = false;
                        }
                        if (nudMintsExpireGameCapping.Value > 240)
                        {
                            //this.ShowMessageBox(this.GetResourceTextByKey(1, "MSG_MAXEXPIRETIME"), this.ParentForm.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);      // "Maximum value for Minutes to expire is 240"
                            this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_MAXEXPIRETIME"), this.ParentForm.Text);
                            bResult = false;
                        }
                    }                   


                    if (bResult)
                    {
                        if (str_error.Count > 0)
                        {
                            string errormsg = "";
                            if (str_error.Count > 1)
                            {
                                errormsg = this.GetResourceTextByKey(1, "MSG_CLEARERRORS");                    // "Please Clear the Errors and Save the Details.";
                            }
                            else
                            {
                                string[] strCell = str_error[0].Split(',');
                                errormsg = gvCardLevelSettings[Convert.ToInt32(strCell[1]), Convert.ToInt32(strCell[0])].ErrorText;
                            }

                            //this.ShowMessageBox(errormsg, this.ParentForm.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            this.ShowInfoMessageBox(errormsg, this.ParentForm.Text);
                            bResult = false;
                        }
                    }

                    //Validate Mins to expire and Cap Mins are Ok
                    if (bResult)
                    {
                        if (!ValidateMinstoCap())
                        {
                            this.ShowErrorMessageBox(this.GetResourceTextByKey(1, "MSG_CAPTIME_MINUTES"), this.ParentForm.Text);          //"Enter a valid minutes to cap time.");
                            bResult = false;
                        }
                    }

                    if (bResult)
                    {
                        ClearMinstoCapErrorMSg();

                        foreach (GetCardLevelSettings CardLevelInfo in lst_CardLevel)
                        {
                            if (lst_Changes.Count < 1)
                                break;

                            if (CardLevelInfo.CardLevel == 0)
                            {
                               // this.ShowMessageBox(this.GetResourceTextByKey(1, "MSG_CARDLEVEL"), this.ParentForm.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);        // "Enter a valid cardLevel"
                                this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_CARDLEVEL"), this.ParentForm.Text);
                                bResult = false;
                                break;
                            }

                            if (CardLevelInfo.MaxNoofMachinestoCap == 0)
                            {
                                //this.ShowMessageBox(this.GetResourceTextByKey(1, "MSG_MAXCAPCOUNT"), this.ParentForm.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);          // "Enter a max machine cap count."
                                this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_MAXCAPCOUNT"), this.ParentForm.Text);
                                bResult = false;
                                break;
                            }

                            if (IsValidMintsToCap(CardLevelInfo.MintstoCap) != 4)
                            {
                                //this.ShowMessageBox(this.GetResourceTextByKey(1, "MSG_CAPTIME_MINUTES"), this.ParentForm.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);       // "Enter a valid minutes to cap time"
                                this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_CAPTIME_MINUTES"), this.ParentForm.Text);
                                bResult = false;
                                break;
                            }
                        }
                    }

                   
                }
            }
            catch
            {
                bResult = false;
            }

            return bResult;
        }
        /// <summary>
        /// Clears the Minutes to cap error.
        /// </summary>
        private void ClearMinstoCapErrorMSg()
        {
            try
            {
                foreach (DataGridViewRow row in gvCardLevelSettings.Rows)
                {
                    string Cellindex = (row.Cells[2].RowIndex + "," + row.Cells[2].ColumnIndex);

                    if (str_error.Contains(Cellindex))
                        str_error.Remove(Cellindex);

                    if (row.Cells[2].ErrorText == this.GetResourceTextByKey(1, "MSG_MINUTESTOCAPTIME"))     //"Minutes to Cap Time should be greater than Expire Time")
                    {
                        row.Cells[2].ErrorText = "";
                    }

                    row.Cells[2].Style.BackColor = row.Cells[2].Style.BackColor != SystemColors.Window ? SystemColors.Window : row.Cells[2].Style.BackColor;

                }
            }
            finally
            {

            }

        }
        /// <summary>
        /// Clear minutes to cap error in Expire time value changed event.
        /// </summary>
        /// <returns></returns>
        private bool ValidateMinstoCap()
        {
            try
            {
                if (lst_CardLevel == null)
                    return true;
                if (lst_CardLevel[0].CardLevel == 0 && lst_CardLevel[0].MaxNoofMachinestoCap == 0 && lst_CardLevel[0].MintstoCap == "0" && lst_CardLevel.Count == 1)
                    return true;

                bool bResult = true;
                char[] delimter = { ',' };

                List<GetCardLevelSettings> olst = lst_CardLevel.FindAll(o => o.MintstoCap == null);
                if (bResult)
                {
                    if (olst.Count <= 0)
                    {
                        List<GetCardLevelSettings> lst_temp = lst_CardLevel.FindAll(obj =>
                                obj.MintstoCap.Split(delimter).Any(o => Convert.ToInt32(o) < nudMintsExpireGameCapping.Value));

                        foreach (GetCardLevelSettings gc in lst_temp)
                        {
                            int index = lst_CardLevel.FindIndex(obj => obj == gc);
                            if (index != -1 && lst_CardLevel[index].CardLevel != 0 && lst_CardLevel[index].MaxNoofMachinestoCap != 0 && lst_CardLevel[index].MintstoCap != "0")
                            {
                                DataGridViewCell cell = gvCardLevelSettings.Rows[index].Cells[2];
                                cell.ErrorText = this.GetResourceTextByKey(1, "MSG_MINUTESTOCAPTIME");       // "Minutes to Cap Time should be greater than Expire Time";
                                cell.Style.BackColor = Color.Bisque;
                                btnAdd.Enabled = false;
                                btnDelete.Enabled = false;
                            }
                            bResult = false;
                        }
                    }
                    else
                        bResult = false;
                }

                return bResult;
            }
            finally
            {

            }
        }
        #endregion UserDefined Function

        private void UCGameCapping_Load(object sender, EventArgs e)
        {
            // For externalization
            this.ResolveResources();
        }
    }
}