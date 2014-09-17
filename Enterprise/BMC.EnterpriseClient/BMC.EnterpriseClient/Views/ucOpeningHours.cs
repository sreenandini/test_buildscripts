using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BMC.CoreLib.Win32;
using BMC.EnterpriseBusiness.Entities;
using BMC.EnterpriseBusiness.Business;
using BMC.Common.LogManagement;
using BMC.Common.ExceptionManagement;
using BMC.Common;


namespace BMC.EnterpriseClient.Views
{
    public partial class ucOpeningHours : UserControl
    {
        int[] RowOpened = new int[7];
        int iStartIdx = 0;
        int iEndIdx = 0;
        int SelCellCount = 0;
        int CurrentColumnIndex = 0;
        int FromDay = 0;
        int ToDay = 0;
        int SelStartDay = 0;
        int SelEndDay = 0;
        int CurrentRowIndex = 0;
        int _SiteID = 0;
        long _StdOpenHRID = 0;
        int _ZoneID = 0;
        bool LoadNew = true;
        int _iDgCellWidth = 9;
        int _iDgCellHeight = 40;
        SiteDetails sdobj = new SiteDetails();
        SiteOpeningHours OpenHr = new SiteOpeningHours();
        GetStdOpeningHrsDetailsResult StdOpenHREntity = new GetStdOpeningHrsDetailsResult();
        GetZoneOpeningHoursResult ZoneOpenHREntity = new GetZoneOpeningHoursResult();


        int[,] OpenedCells = new int[7, 96];
        public ucOpeningHours()
        {
            InitializeComponent();
            SetTagProperty();
        }

        public enum Days
        {
            Monday,
            Tuesday,
            Wednesday,
            Thursday,
            Friday,
            Saturday,
            Sunday,
        };

        public void SetTagProperty()
        {
            this.lblFromDayValue.Tag = "Key_lblFromDay";
            this.lblToDayValue.Tag = "Key_lblToDay";
            this.lbl_Midnight2.Tag = "Key_Midnight";
            this.lbl_Midnight.Tag = "Key_Midnight";
            this.lbl_Noon.Tag = "Key_Noon";
            this.btnOpen.Tag = "Key_OpenCaption1";
            this.Tag = "Key_OpeningHours";
            this.lblHours.Tag = "Key_StandardHoursColon";
            this.btnClosed.Tag = "Key_Closed";
            //this.btnCancel.Tag = "Key_CancelCaption";
            //this.btnOK.Tag = "Key_OKCaption";
            this.btnUseSiteTimes.Tag = "Key_UseSiteTimes";

        }

        public void GetOpenedHour(int Day, string OpenedDay)
        {
            int y = 0, Len = 0, TempLen = 0;
            string TempHr = null;
            int TempIndex = 0;
            TempHr = OpenedDay;
            try
            {
                if (OpenedDay != null)
                    Len = OpenedDay.Length;
                TempLen = Len;

                for (y = 0; y < 96; y++)
                {
                    if (Len > 0)
                    {
                        OpenedCells[Day, y] = Convert.ToInt32(TempHr.Substring(0, 1));

                        if (OpenedCells[Day, y] == 1)
                        {
                            TempIndex = y % 8;
                            if (TempIndex < 4)
                            {
                                dgOpeningHours.Rows[Day].Cells[y].Style.BackColor = Color.FromArgb(210, 0, 0);
                            }
                            else
                            {
                                dgOpeningHours.Rows[Day].Cells[y].Style.BackColor = System.Drawing.Color.Red;
                            }
                        }
                        TempLen = TempLen - 1;
                        if (TempLen > 1)
                            TempHr = TempHr.Substring(1);
                    }
                    else
                    {
                        OpenedCells[Day, y] = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        public void SaveChanges()
        {
            StringBuilder myOpeningHours;

            if (_SiteID > 0)
            {
                bool SiteStatus = false;
                bool ZoneStatus = false;
                try
                {
                    myOpeningHours = new StringBuilder();
                    myOpeningHours = CalcOpeningHours(0, myOpeningHours);
                    OpenHr.Site_Open_Monday = myOpeningHours.ToString();


                    myOpeningHours = new StringBuilder();
                    myOpeningHours = CalcOpeningHours(1, myOpeningHours);
                    OpenHr.Site_Open_Tuesday = myOpeningHours.ToString();


                    myOpeningHours = new StringBuilder();
                    myOpeningHours = CalcOpeningHours(2, myOpeningHours);
                    OpenHr.Site_Open_Wednesday = myOpeningHours.ToString();

                    myOpeningHours = new StringBuilder();
                    myOpeningHours = CalcOpeningHours(3, myOpeningHours);
                    OpenHr.Site_Open_Thursday = myOpeningHours.ToString();

                    myOpeningHours = new StringBuilder();
                    myOpeningHours = CalcOpeningHours(4, myOpeningHours);
                    OpenHr.Site_Open_Friday = myOpeningHours.ToString();

                    myOpeningHours = new StringBuilder();
                    myOpeningHours = CalcOpeningHours(5, myOpeningHours);
                    OpenHr.Site_Open_Saturday = myOpeningHours.ToString();

                    myOpeningHours = new StringBuilder();
                    myOpeningHours = CalcOpeningHours(6, myOpeningHours);
                    OpenHr.Site_Open_Sunday = myOpeningHours.ToString();

                    SiteStatus = sdobj.CheckandUpdateSiteOpeningHours(_SiteID, OpenHr);
                    if (SiteStatus == false)
                    {
                        Win32Extensions.ShowErrorMessageBox(this, this.GetResourceTextByKey(1, "MSG_ERROR_DB_ACCESS"), this.Text);
                    }
                }
                catch (Exception ex)
                {
                    ExceptionManager.Publish(ex);
                }
                if (SiteStatus == true)
                {
                    LogManager.WriteLog("---Site Opening Hours Updated Successfully---", LogManager.enumLogLevel.Info);
                    try
                    {
                        ZoneStatus = sdobj.CheckZoneinSite(_SiteID);
                    }
                    catch (Exception ex)
                    {
                        ExceptionManager.Publish(ex);
                    }

                    if (ZoneStatus)
                    {
                        if (Win32Extensions.ShowQuestionMessageBox(this, this.GetResourceTextByKey(1, "MSG_UPDATE_ZONES_WITH_THESE_TIMES"), this.Text) == DialogResult.Yes)
                        {
                            try
                            {
                                myOpeningHours = new StringBuilder();
                                myOpeningHours = CalcOpeningHours(0, myOpeningHours);
                                OpenHr.Zone_Open_Monday = myOpeningHours.ToString();

                                myOpeningHours = new StringBuilder();
                                myOpeningHours = CalcOpeningHours(1, myOpeningHours);
                                OpenHr.Zone_Open_Tuesday = myOpeningHours.ToString();

                                myOpeningHours = new StringBuilder();
                                myOpeningHours = CalcOpeningHours(2, myOpeningHours);
                                OpenHr.Zone_Open_Wednesday = myOpeningHours.ToString();

                                myOpeningHours = new StringBuilder();
                                myOpeningHours = CalcOpeningHours(3, myOpeningHours);
                                OpenHr.Zone_Open_Thursday = myOpeningHours.ToString();

                                myOpeningHours = new StringBuilder();
                                myOpeningHours = CalcOpeningHours(4, myOpeningHours);
                                OpenHr.Zone_Open_Friday = myOpeningHours.ToString();

                                myOpeningHours = new StringBuilder();
                                myOpeningHours = CalcOpeningHours(5, myOpeningHours);
                                OpenHr.Zone_Open_Saturday = myOpeningHours.ToString();

                                myOpeningHours = new StringBuilder();
                                myOpeningHours = CalcOpeningHours(6, myOpeningHours);
                                OpenHr.Zone_Open_Sunday = myOpeningHours.ToString();

                                sdobj.UpdateZoneOpeningHours(_SiteID, OpenHr);
                                //LOG - HERE -Opening HRs update successfully in Zone.
                            }
                            catch (Exception ex)
                            {
                                ExceptionManager.Publish(ex);
                            }
                        }
                    }
                }
                else if (SiteStatus == false)
                {
                    Win32Extensions.ShowErrorMessageBox(this, this.GetResourceTextByKey(1, "MSG_ERROR_DB_ACCESS"), this.Text);
                }
            }
            else if (_ZoneID > 0)
            {
                try
                {
                    myOpeningHours = new StringBuilder();
                    myOpeningHours = GetOpenedCells(0, myOpeningHours);
                    ZoneOpenHREntity.Zone_Open_Monday = myOpeningHours.ToString();

                    myOpeningHours = new StringBuilder();
                    myOpeningHours = GetOpenedCells(1, myOpeningHours);
                    ZoneOpenHREntity.Zone_Open_Tuesday = myOpeningHours.ToString();

                    myOpeningHours = new StringBuilder();
                    myOpeningHours = GetOpenedCells(2, myOpeningHours);
                    ZoneOpenHREntity.Zone_Open_Wednesday = myOpeningHours.ToString();

                    myOpeningHours = new StringBuilder();
                    myOpeningHours = GetOpenedCells(3, myOpeningHours);
                    ZoneOpenHREntity.Zone_Open_Thursday = myOpeningHours.ToString();

                    myOpeningHours = new StringBuilder();
                    myOpeningHours = GetOpenedCells(4, myOpeningHours);
                    ZoneOpenHREntity.Zone_Open_Friday = myOpeningHours.ToString();

                    myOpeningHours = new StringBuilder();
                    myOpeningHours = GetOpenedCells(5, myOpeningHours);
                    ZoneOpenHREntity.Zone_Open_Saturday = myOpeningHours.ToString();

                    myOpeningHours = new StringBuilder();
                    myOpeningHours = GetOpenedCells(6, myOpeningHours);
                    ZoneOpenHREntity.Zone_Open_Sunday = myOpeningHours.ToString();

                    if (!sdobj.CheckandUpdateZoneOpeningHours(_ZoneID, ZoneOpenHREntity))
                    {
                        Win32Extensions.ShowErrorMessageBox(this, this.GetResourceTextByKey(1, "MSG_ERROR_DB_ACCESS"), this.Text);
                    }
                }
                catch (Exception ex)
                {
                    ExceptionManager.Publish(ex);
                }
            }
            else if (_StdOpenHRID > 0)
            {
                try
                {
                    bool UpdateStatus = false;
                    int TotalSelectedCell = 0;

                    OpenHr.TotalHours = String.Empty;

                    myOpeningHours = new StringBuilder();
                    myOpeningHours = CalcOpeningHours(0, myOpeningHours);
                    StdOpenHREntity.Standard_Opening_Hours_Open_Monday = myOpeningHours.ToString();

                    myOpeningHours = new StringBuilder();
                    myOpeningHours = CalcOpeningHours(1, myOpeningHours);
                    StdOpenHREntity.Standard_Opening_Hours_Open_Tuesday = myOpeningHours.ToString();

                    myOpeningHours = new StringBuilder();
                    myOpeningHours = CalcOpeningHours(2, myOpeningHours);
                    StdOpenHREntity.Standard_Opening_Hours_Open_Wednesday = myOpeningHours.ToString();

                    myOpeningHours = new StringBuilder();
                    myOpeningHours = CalcOpeningHours(3, myOpeningHours);
                    StdOpenHREntity.Standard_Opening_Hours_Open_Thursday = myOpeningHours.ToString();

                    myOpeningHours = new StringBuilder();
                    myOpeningHours = CalcOpeningHours(4, myOpeningHours);
                    StdOpenHREntity.Standard_Opening_Hours_Open_Friday = myOpeningHours.ToString();

                    myOpeningHours = new StringBuilder();
                    myOpeningHours = CalcOpeningHours(5, myOpeningHours);
                    StdOpenHREntity.Standard_Opening_Hours_Open_Saturday = myOpeningHours.ToString();

                    myOpeningHours = new StringBuilder();
                    myOpeningHours = CalcOpeningHours(6, myOpeningHours);
                    StdOpenHREntity.Standard_Opening_Hours_Open_Sunday = myOpeningHours.ToString();

                    TotalSelectedCell = TotalColumSelected(OpenHr.TotalHours);
                    if (TotalSelectedCell > 0)
                    {
                        StdOpenHREntity.Standard_Opening_Hours_Total = (TotalSelectedCell * 15);
                    }
                    else
                    {
                        StdOpenHREntity.Standard_Opening_Hours_Total = 0;
                    }

                    UpdateStatus = sdobj.CheckandUpdateStdOpeningHrs(Convert.ToInt32(_StdOpenHRID), StdOpenHREntity);

                    if (UpdateStatus == false)
                    {
                        Win32Extensions.ShowErrorMessageBox(this, this.GetResourceTextByKey(1, "MSG_ERROR_DB_ACCESS"), this.Text);
                    }
                }
                catch (Exception ex)
                {
                    ExceptionManager.Publish(ex);
                }
            }
        }

        private StringBuilder CalcOpeningHours(int Rowindex, StringBuilder MyOpenHrs)
        {
            int i = 0;
            try
            {
                for (i = 0; i <= OpenedCells.GetUpperBound(1); i++)
                {
                    MyOpenHrs.Append(OpenedCells[Rowindex, i]);
                    OpenHr.TotalHours = OpenHr.TotalHours + OpenedCells[Rowindex, i].ToString();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return MyOpenHrs;
        }

        private StringBuilder GetOpenedCells(int Rowindex, StringBuilder MyOpenHrs)
        {
            try
            {
                for (int i = 0; i <= OpenedCells.GetUpperBound(1); i++)
                {
                    MyOpenHrs.Append(OpenedCells[Rowindex, i]);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return MyOpenHrs;
        }

        private int TotalColumSelected(string TotalHr)
        {
            int Length = 0, Count = 0;
            //string TempHr = null;
            string Tempval = null;

            if (TotalHr != null)
                Length = TotalHr.Length;
            try
            {
                while (Length > 0)
                {
                    Tempval = TotalHr.Substring(0, 1);
                    if (Tempval.Contains("1"))
                    {
                        Count = Count + 1;
                    }
                    TotalHr = TotalHr.Substring(1);
                    Length = Length - 1;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return Count;
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            try
            {
                int mySession = 1;
                if (dgOpeningHours.SelectedCells.Count == 0)
                {
                    return;
                }
                //dgOpeningHours.SelectedCells.c
                //dgOpeningHours.

                int Rowcount = dgOpeningHours.RowCount;
                int ColCount = dgOpeningHours.ColumnCount;
                int i = 0, j = 0;
                for (i = 0; i < Rowcount; i++)
                {
                    for (j = 0; j < ColCount; j++)
                    {
                        if (dgOpeningHours.Rows[i].Cells[j].Selected == true)
                        {
                            if (j % 8 < 4)
                            {
                                dgOpeningHours.Rows[i].Cells[j].Style.BackColor = Color.FromArgb(210, 0, 0);
                            }
                            else
                            {
                                dgOpeningHours.Rows[i].Cells[j].Style.BackColor = System.Drawing.Color.Red;
                            }
                            OpenedCells[i, j] = mySession;
                        }
                    }
                }
                dgOpeningHours.ClearSelection();
                lblFromDayValue.Text = "";
                lblToDayValue.Text = "";
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btnClosed_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgOpeningHours.SelectedCells.Count == 0)
                {
                    return;
                }

                int i = 0, j = 0;

                for (i = 0; i < dgOpeningHours.RowCount; i++)
                {
                    for (j = 0; j < dgOpeningHours.ColumnCount; j++)
                    {
                        if (dgOpeningHours.Rows[i].Cells[j].Selected == true)
                        {
                            if (j % 8 < 4)
                            {
                                dgOpeningHours.Rows[i].Cells[j].Style.BackColor = System.Drawing.Color.LightGray;
                            }
                            else
                            {
                                dgOpeningHours.Rows[i].Cells[j].Style.BackColor = System.Drawing.Color.White;
                            }
                            OpenedCells[i, j] = 0;
                        }
                    }
                }
                dgOpeningHours.ClearSelection();
                lblFromDayValue.Text = "";
                lblToDayValue.Text = "";
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void dgOpeningHours_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            dgOpeningHours.Rows[e.RowIndex].Selected = true;
            iStartIdx = 0;
            iEndIdx = 95;
        }

        private void dgOpeningHours_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (RowOpened[e.RowIndex] == 0)
            {
                btnOpen_Click(sender, e);
                RowOpened[e.RowIndex] = 1;
            }
            else if (RowOpened[e.RowIndex] == 1)
            {
                btnClosed_Click(sender, e);
                RowOpened[e.RowIndex] = 0;
            }
        }

        private void dgOpeningHours_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dgOpeningHours.SelectedCells.Count > 1) return;
            if (e.ColumnIndex < 0)
            {
                lbl_CurrentSelectionValue.Text = "";
                return;
            }
            TimeSpan ts1 = new TimeSpan(0, (e.ColumnIndex * 15), 0);
            TimeSpan ts2 = new TimeSpan(0, ((e.ColumnIndex + 1) * 15), 0);
            lbl_CurrentSelectionValue.Text = string.Format("{0:D2}:{1:D2}", ts1.Hours, ts1.Minutes) + "-> " + string.Format("{0:D2}:{1:D2}", ts2.Hours, ts2.Minutes);
        }

        private void dgOpeningHours_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (CurrentRowIndex < 0) return;
                SelCellCount = dgOpeningHours.SelectedCells.Count;
                if (SelCellCount == 1)
                {
                    SelStartDay = dgOpeningHours.SelectedCells[0].RowIndex;
                }

                if (dgOpeningHours.SelectedRows.Count == 1)
                {
                    SelStartDay = dgOpeningHours.SelectedCells[0].RowIndex;
                    SelEndDay = SelStartDay;
                }
                if (SelCellCount > 0)
                {
                    TimeSpan ts1;
                    TimeSpan ts2;

                    iEndIdx = CurrentColumnIndex;

                    if (dgOpeningHours.SelectedCells.Count == 1)
                    {
                        iStartIdx = iEndIdx;
                    }

                    if (dgOpeningHours.SelectedRows.Count > 1)
                    {
                        SelEndDay = CurrentRowIndex;

                        if (SelEndDay > SelStartDay)
                        {
                            FromDay = SelStartDay;
                            ToDay = SelEndDay;
                        }
                        else
                        {
                            FromDay = SelEndDay;
                            ToDay = SelStartDay;
                        }
                        iStartIdx = 0;
                        iEndIdx = 95;
                    }
                    else
                    {
                        SelEndDay = CurrentRowIndex;

                        if (SelEndDay < SelStartDay)
                        {
                            FromDay = SelEndDay;
                            ToDay = SelStartDay;
                        }
                        else
                        {
                            FromDay = SelStartDay;
                            ToDay = SelEndDay;
                        }

                        if (dgOpeningHours.SelectedRows.Count == 1)
                        {
                            if (dgOpeningHours.SelectedCells.Count == 96)
                            {
                                iStartIdx = 0;
                                iEndIdx = 95;
                            }
                        }
                    }
                    lblFromDayValue.Text = this.GetResourceTextByKey("Key_" + ((Days)FromDay).ToString());
                    lblToDayValue.Text = this.GetResourceTextByKey("Key_" + ((Days)ToDay).ToString());

                    if (iEndIdx > iStartIdx)
                    {
                        ts1 = new TimeSpan(0, (iStartIdx * 15), 0);
                        ts2 = new TimeSpan(0, ((iEndIdx + 1) * 15), 0);
                    }
                    else
                    {
                        ts1 = new TimeSpan(0, (iEndIdx * 15), 0);
                        ts2 = new TimeSpan(0, ((iStartIdx + 1) * 15), 0);
                    }

                    if (ts2.Days == 1)
                    {
                        lbl_CurrentSelectionValue.Text = string.Format("{0:D2}:{1:D2}", ts1.Hours, ts1.Minutes) + "-> " + string.Format("{0:D2}:{1:D2}", "24", ts2.Minutes);
                    }
                    else
                    {
                        lbl_CurrentSelectionValue.Text = string.Format("{0:D2}:{1:D2}", ts1.Hours, ts1.Minutes) + "-> " + string.Format("{0:D2}:{1:D2}", ts2.Hours, ts2.Minutes);
                    }

                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

        }

        private void dgOpeningHours_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            CurrentRowIndex = e.RowIndex;


        }

        private void dgOpeningHours_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            CurrentRowIndex = e.RowIndex;
            CurrentColumnIndex = e.ColumnIndex;

        }

        private void btnUseSiteTimes_Click(object sender, EventArgs e)
        {
            try
            {
                if (Win32Extensions.ShowQuestionMessageBox(this, "Do u want to apply Site timings to Zone?") == DialogResult.Yes)
                {
                    if (!sdobj.CheckandUpdateZoneOpeningSiteTime(_ZoneID))
                    {
                        Win32Extensions.ShowErrorMessageBox(this, this.GetResourceTextByKey(1, "MSG_ERROR_DB_ACCESS"), this.Text);
                    }
                    else
                    {
                        LoadNew = false;
                        ClearGridViewData();
                        if (_ZoneID > 0)
                        {
                            try
                            {
                                lblCaption.Text = this.GetResourceTextByKey("Key_Zone");          // "Zone";
                                btnUseSiteTimes.Visible = true;
                                GetZoneOpeningHoursResult ZnOpenHr = sdobj.ZoneOpenHours(_ZoneID);
                                if (ZnOpenHr != null)
                                {
                                    txt_OpeningHours.Text = ZnOpenHr.Zone_Name;
                                    GetOpenedHour(0, ZnOpenHr.Zone_Open_Monday);
                                    GetOpenedHour(1, ZnOpenHr.Zone_Open_Tuesday);
                                    GetOpenedHour(2, ZnOpenHr.Zone_Open_Wednesday);
                                    GetOpenedHour(3, ZnOpenHr.Zone_Open_Thursday);
                                    GetOpenedHour(4, ZnOpenHr.Zone_Open_Friday);
                                    GetOpenedHour(5, ZnOpenHr.Zone_Open_Saturday);
                                    GetOpenedHour(6, ZnOpenHr.Zone_Open_Sunday);
                                }
                            }
                            catch (Exception ex)
                            {
                                ExceptionManager.Publish(ex);
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

        private void btn_Cancel_Click(object sender, EventArgs e)
        {

        }

        private void ucOpeningHours_Load(object sender, EventArgs e)
        {
            this.dgOpeningHours.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgOpeningHours.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgOpeningHours.RowHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            this.dgOpeningHours.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            EnableDisableContols("Cancel", false);
            try
            {
                this.ResolveResources();
                btnClosed.Visible = AppGlobals.Current.HasUserAccess("HQ_Admin_OpenHours");
                btnOpen.Visible = AppGlobals.Current.HasUserAccess("HQ_Admin_OpenHours");

                string[] strDays = {   this.GetResourceTextByKey("Key_Monday"), 
                                       this.GetResourceTextByKey("Key_Tuesday"), 
                                       this.GetResourceTextByKey("Key_Wednesday"),
                                       this.GetResourceTextByKey("Key_Thursday"), 
                                       this.GetResourceTextByKey("Key_Friday"), 
                                       this.GetResourceTextByKey("Key_Saturday"),
                                       this.GetResourceTextByKey("Key_Sunday")
                                   };

                int i, j = 0;

                string strColName;
                if (LoadNew == true)
                {
                    for (i = 1; i < 97; i++)
                    {

                        strColName = "colHr" + i.ToString();
                        dgOpeningHours.Columns.Add(strColName, "");
                        dgOpeningHours.Columns[strColName].Width = _iDgCellWidth;

                        if (j < 4)
                        {
                            dgOpeningHours.Columns[strColName].CellTemplate.Style.BackColor = Color.LightGray;
                            j++;
                        }
                        else if (j >= 4)
                        {
                            dgOpeningHours.Columns[strColName].CellTemplate.Style.BackColor = Color.White;
                            j++;
                            if (j == 8) j = 0;
                        }
                    }
                    i = 0;
                    foreach (string item in strDays)
                    {
                        dgOpeningHours.Rows.Add("");
                        dgOpeningHours.Rows[i].Height = _iDgCellHeight;
                        dgOpeningHours.Rows[i].HeaderCell.Value = item;

                        i = i + 1;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        public void LoadData(int SiteID = 0, int ZoneID = 0, long StdOpenHRID = 0, bool fromZone = false, bool StdAdminOpenTimes = false)
        {
            _SiteID = SiteID;
            _ZoneID = ZoneID;
            _StdOpenHRID = StdOpenHRID;

            ClearGridViewData();

            try
            {
                dgOpeningHours.ClearSelection();

                if (SiteID > 0)
                {
                    try
                    {
                        lblCaption.Text = this.GetResourceTextByKey("Key_Site");              // "Site";
                        SiteOpeningHours OpenHr = sdobj.OpeningHours(_SiteID);
                        if (OpenHr != null)
                        {
                            txt_OpeningHours.Text = OpenHr.Site_Name.Trim();
                            GetOpenedHour(0, OpenHr.Site_Open_Monday);
                            GetOpenedHour(1, OpenHr.Site_Open_Tuesday);
                            GetOpenedHour(2, OpenHr.Site_Open_Wednesday);
                            GetOpenedHour(3, OpenHr.Site_Open_Thursday);
                            GetOpenedHour(4, OpenHr.Site_Open_Friday);
                            GetOpenedHour(5, OpenHr.Site_Open_Saturday);
                            GetOpenedHour(6, OpenHr.Site_Open_Sunday);
                        }
                    }
                    catch (Exception ex)
                    {
                        ExceptionManager.Publish(ex);
                    }

                }
                else if (ZoneID > 0)
                {
                    try
                    {
                        lblCaption.Text = this.GetResourceTextByKey("Key_Zone");          // "Zone";
                        btnUseSiteTimes.Visible = true;
                        GetZoneOpeningHoursResult ZnOpenHr = sdobj.ZoneOpenHours(_ZoneID);
                        if (ZnOpenHr != null)
                        {
                            txt_OpeningHours.Text = ZnOpenHr.Zone_Name;
                            GetOpenedHour(0, ZnOpenHr.Zone_Open_Monday);
                            GetOpenedHour(1, ZnOpenHr.Zone_Open_Tuesday);
                            GetOpenedHour(2, ZnOpenHr.Zone_Open_Wednesday);
                            GetOpenedHour(3, ZnOpenHr.Zone_Open_Thursday);
                            GetOpenedHour(4, ZnOpenHr.Zone_Open_Friday);
                            GetOpenedHour(5, ZnOpenHr.Zone_Open_Saturday);
                            GetOpenedHour(6, ZnOpenHr.Zone_Open_Sunday);
                        }
                    }
                    catch (Exception ex)
                    {
                        ExceptionManager.Publish(ex);
                    }
                }
                else if (StdOpenHRID > 0)
                {
                    try
                    {
                        lblCaption.Text = this.GetResourceTextByKey("Key_StandardOpeningTimesSet");        //"&Standard Opening Hours Set:";
                        btnUseSiteTimes.Visible = false;
                        GetStdOpeningHrsDetailsResult OpenHr = sdobj.GetStdOpeningHrsDetails(StdOpenHRID);
                        if (OpenHr != null)
                        {
                            txt_OpeningHours.Text = OpenHr.Standard_Opening_Hours_Description;
                            GetOpenedHour(0, OpenHr.Standard_Opening_Hours_Open_Monday);
                            GetOpenedHour(1, OpenHr.Standard_Opening_Hours_Open_Tuesday);
                            GetOpenedHour(2, OpenHr.Standard_Opening_Hours_Open_Wednesday);
                            GetOpenedHour(3, OpenHr.Standard_Opening_Hours_Open_Thursday);
                            GetOpenedHour(4, OpenHr.Standard_Opening_Hours_Open_Friday);
                            GetOpenedHour(5, OpenHr.Standard_Opening_Hours_Open_Saturday);
                            GetOpenedHour(6, OpenHr.Standard_Opening_Hours_Open_Sunday);
                        }


                    }
                    catch (Exception ex)
                    {
                        ExceptionManager.Publish(ex);
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        public void ClearGridViewData()
        {
            int i, j = 0;
            txt_OpeningHours.Text = string.Empty;
            for (i = 0; i < dgOpeningHours.RowCount; i++)
            {
                for (j = 0; j < dgOpeningHours.ColumnCount; j++)
                {
                    if (j % 8 < 4)
                    {
                        dgOpeningHours.Rows[i].Cells[j].Style.BackColor = System.Drawing.Color.LightGray;
                    }
                    else
                    {
                        dgOpeningHours.Rows[i].Cells[j].Style.BackColor = System.Drawing.Color.White;
                    }
                }
            }
            lblFromDayValue.Text = "";
            lblToDayValue.Text = "";
            lbl_CurrentSelectionValue.Text = "";
        }

        public void EnableDisableContols(string Type, bool Status)
        {
            switch (Type)
            {
                case "New":
                    txt_OpeningHours.ReadOnly = Status;
                    dgOpeningHours.Enabled = Status;
                    btnOpen.Enabled = Status;
                    btnClosed.Enabled = Status;
                    break;
                case "Edit":
                    dgOpeningHours.Enabled = Status;
                    btnOpen.Enabled = Status;
                    btnClosed.Enabled = Status;
                    break;
                case "Cancel":
                    txt_OpeningHours.ReadOnly = true;
                    txt_OpeningHours.Text = string.Empty;
                    dgOpeningHours.Enabled = false;
                    btnOpen.Enabled = false;
                    btnClosed.Enabled = false;
                    break;
            }
        }

        public bool ValidateControls(out string OpeningHours)
        {
            if (string.IsNullOrEmpty(txt_OpeningHours.Text.Trim()))
            {
                Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_OPENHOURS_NAME"), this.Text);            // "Please enter the name of the new opening hours set");
                txt_OpeningHours.Focus();
                OpeningHours = string.Empty;
                return false;
            }

            if (txt_OpeningHours.Text.Trim().Length > 50)
            {
                Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_NAME_EXCEEDLENGTH"), this.Text);            // "Name entered is more than 50 characters");
                txt_OpeningHours.Focus();
                OpeningHours = string.Empty;
                return false;
            }
            else
            {
                OpeningHours = txt_OpeningHours.Text;
                return true;
            }
        }

        private void btn_New_Click(object sender, EventArgs e)
        {

        }

        private void dgOpeningHours_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            try
            {
                if (e.RowIndex == -1 && e.ColumnIndex > -1)
                {
                    e.PaintBackground(e.CellBounds, false);
                    Rectangle r2 = e.CellBounds;
                    r2.Y += e.CellBounds.Height / 2;
                    r2.Height = e.CellBounds.Height / 2;
                    e.PaintContent(r2);
                    e.Handled = true;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void dgOpeningHours_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                string[] TimePeriod = { "|", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "|", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "|" };

                for (int j = 0; j < 96; )
                {
                    Rectangle r1 = this.dgOpeningHours.GetCellDisplayRectangle(j, -1, true); //get the column header cell
                    r1.X += 1;
                    r1.Y += 1;
                    r1.Width = r1.Width * 4 - 2;
                    r1.Height = r1.Height;
                    e.Graphics.FillRectangle(new SolidBrush(this.dgOpeningHours.ColumnHeadersDefaultCellStyle.BackColor), r1);
                    StringFormat format = new StringFormat();
                    format.Alignment = StringAlignment.Center;
                    format.LineAlignment = StringAlignment.Center;
                    e.Graphics.DrawString(TimePeriod[j / 4],
                    this.dgOpeningHours.ColumnHeadersDefaultCellStyle.Font,
                    new SolidBrush(this.dgOpeningHours.ColumnHeadersDefaultCellStyle.ForeColor),
                    r1,
                    format);
                    j += 4;
                }

                _iDgCellWidth = (dgOpeningHours.Size.Width - dgOpeningHours.Columns[0].Width) / 96;
                _iDgCellHeight = (dgOpeningHours.Size.Height - dgOpeningHours.Rows[0].Height) / 7;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void ucOpeningHours_Resize(object sender, EventArgs e)
        {
            //int count = dgOpeningHours.Columns.Count;
            //if (count > 0)
            //{
            //    int width = (int)Math.Ceiling((double)(dgOpeningHours.DisplayRectangle.Width) / (double)count);
            //    for (int i = 0; i < count; i++)
            //    {
            //        dgOpeningHours.Columns[i].Width = width;
            //    }
            //    dgOpeningHours.Invalidate(true);
            //}
        }
        }
    }
