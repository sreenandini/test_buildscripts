using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using BMC.Common.LogManagement;
using BMC.Common.ConfigurationManagement;
using BMC.Common.ExceptionManagement;
using BMC.DataAccess;
using System.Collections;
using System.Data.SqlClient;
using System.Globalization;
using BMC.EnterpriseReportsBusiness;
using BMC.EnterpriseReportsTransport;
using System.Data.Linq;
using System.Linq;
using BMC.Common.Utilities;
using System.Configuration;
using System.Xml.Serialization;
using BMC.Reports;
using BMC.Common;
using System.Text.RegularExpressions;

namespace BMC.EnterpriseReportsUI
{
    [Serializable]
    public partial class Criteria : Form
    {
        # region   Member variables
        ReportsBusiness oReportBusiness = null;
        string DateFormat = "yyyy-MM-dd HH:mm:ss";
        public GetAllReportsToRolesAccess ReportObject = null;
        public delegate void CriteriaHandler(object sender, FormClosedEventArgs e);
        public event CriteriaHandler delCriteriaClosed;
        
        # endregion   Member variables

        # region   Constructor

        public Criteria()
        {
            InitializeComponent();
            oReportBusiness = new ReportsBusiness();
        }

        public string XMLName { get; set; }
        public DataSet dtSet { get; set; }


        #endregion Constructor

        #region ButtonPosition

        private int newTop = 0;

        public Control OpenButton { get; set; }
        public Control ExportButton { get; set; }
        
        
        public void SetButtonsPosition()
        {
            try
            {
                int newLeft = 0;
                newTop = 0;
                foreach (Control currentControl in this.Controls)
                {
                    if (currentControl == null ||
                    currentControl == this.OpenButton ||
                    currentControl == this.ExportButton) continue;
                    Rectangle objRectangle = currentControl.Bounds;
                    if ((objRectangle.Y + objRectangle.Height) > newTop)
                    {
                        newTop = objRectangle.Y + objRectangle.Height;
                    }
                    if (objRectangle.X > newLeft)
                    {
                        newLeft = objRectangle.X;
                    }
                }

                if (newTop <= 100) newTop = 100;
                newTop += 10;
                if (this.OpenButton != null)
                {
                    this.OpenButton.Location = new Point(newLeft, newTop);
                    newLeft = this.OpenButton.Left + this.OpenButton.Width;
                }

                if (this.ExportButton != null)
                {
                    this.ExportButton.Location = new Point(newLeft + 10, newTop);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        #endregion

        #region Events

        private void frmCriteria_Load(object sender, EventArgs e)
        {

        }

        public void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }



        private void Criteria_FormClosed(object sender, FormClosedEventArgs e)
        {
            delCriteriaClosed(this, e);
        }

        public void LoadListparameters(clsSPParams lstParams)
        {
            lstParams.UserId = Program.UserID;
            lstParams.SiteIDList = string.Empty;
            Control.ControlCollection coll = this.Controls;
            foreach (Control c in coll)
            {
                if (c != null)
                {
                    switch (c.GetType().Name.ToUpper())
                    {
                        case "BUTTON": { break; }
                        case "LABEL": { break; }
                        case "RADIOBUTTON":
                            {
                                RadioButton rbtn = c as RadioButton;
                                switch (rbtn.Name.ToUpper())
                                {

                                    case Constants.BMC:
                                        lstParams.BMC = rbtn.Checked;
                                        break;
                                    case Constants.Promotional:
                                        lstParams.Promotional = rbtn.Checked;
                                        break;
                                    case Constants.Both:
                                        lstParams.Both = rbtn.Checked;
                                        break;
                                }
                                break;
                            }

                        case "CHECKBOX":
                            {
                                CheckBox chk = c as CheckBox;
                                switch (c.Name.ToUpper())
                                {
                                    case Constants.HideZeroVarianceCollections:
                                        {
                                            lstParams.HideZeroVarianceCollections = (chk.Checked) ? true : false;
                                            break;
                                        }
                                    case Constants.IncludeNonCashable:
                                        {
                                            lstParams.IncludeNonCashable = (chk.Checked) ? true : false;
                                            break;
                                        }
                                    case Constants.UsePhysicalWin:
                                        {
                                            lstParams.UsePhysicalWin = (chk.Checked) ? true : false;
                                            break;
                                        }
                                    case Constants.UseHandpay:
                                        {
                                            lstParams.ShowHandpay = (chk.Checked) ? true : false;
                                            break;
                                        }
                                    case Constants.UseJackpot:
                                        {
                                            lstParams.ShowJackpot = (chk.Checked) ? true : false;
                                            break;
                                        }
                                    //(Ep4 Changes)                                       
                                    case Constants.IncludeZero:
                                        {
                                            lstParams.IncludeZero = (chk.Checked) ? true : false;
                                            break;
                                        }
                                    case Constants.UseGroupByZone:
                                        {
                                            lstParams.GroupByZone = chk.Checked;
                                            break;
                                        }
                                    case Constants.IsGamingDayBasedReport:
                                        {
                                            lstParams.IsGamingDayBasedReport = (chk.Checked) ? true : false;
                                            break;
                                        }
                                }
                                break;
                            }
                        case "DATETIMEPICKER":
                            {
                                DateTimePicker dtp = c as DateTimePicker;
                                switch (c.Name.ToUpper())
                                {
                                    case Constants.StartDate:
                                        {
                                            lstParams.StartDate = dtp.Value.ToString(DateFormat);
                                            lstParams.EndDateLong = dtp.Value.ToLongDateString();
                                            lstParams.EndDateShort = dtp.Value.ToShortDateString();
                                            lstParams.StartDateString = dtp.Value.ToShortDateString();
                                            lstParams.dtStartDate = dtp.Value;
                                            break;
                                        }
                                    case Constants.TO:
                                        {
                                            lstParams.To = Convert.ToDateTime(dtp.Value.ToString(DateFormat));
                                            lstParams.StartDate = dtp.Value.ToString(DateFormat);
                                            lstParams.EndDateLong = dtp.Value.ToString(DateFormat);
                                            lstParams.EndDateShort = dtp.Value.ToString(DateFormat);

                                            break;
                                        }
                                    case Constants.EndDate:
                                        {
                                            lstParams.EndDate = dtp.Value.ToString(DateFormat);
                                            lstParams.EndDateString = dtp.Value.ToShortDateString();
                                            lstParams.dtEndDate = dtp.Value;
                                            break;
                                        }
                                    case Constants.GamingDate:
                                        {
                                            lstParams.GamingDate = dtp.Value.ToString(DateFormat);
                                            break;
                                        }
                                    case Constants.IssueDate:
                                        {
                                            lstParams.IssueDate = dtp.Value;
                                            break;
                                        }
                                    case Constants.ReportDate:
                                        {
                                            lstParams.ReportDate = dtp.Value;
                                            break;
                                        }


                                }
                                break;
                            }
                        case "COMBOBOX":
                            {
                                ComboBox cmb = c as ComboBox;

                                switch (c.Name.ToUpper())
                                {
                                    case Constants.StackerLevel:
                                        {
                                            int temp = Int32.MinValue;
                                            if (cmb.SelectedItem == null)
                                                break;
                                            Int32.TryParse(cmb.Text, out temp);
                                            lstParams.StackerLevel = temp;
                                            break;
                                        }
                                    case Constants.InventoryLevel:
                                        {
                                            int temp = Int32.MinValue;
                                            if (cmb.SelectedItem == null)
                                                break;
                                            Int32.TryParse(cmb.Text, out temp);
                                            lstParams.InventoryLevel = temp;
                                            break;
                                        }
                                    case Constants.Company:
                                        {
                                            if (cmb.SelectedItem == null)
                                                break;
                                            lstParams.CompanyName = ((Company)(cmb.SelectedItem)).Company_Name;
                                            lstParams.Company = ((Company)(cmb.SelectedItem)).Company_ID;
                                            break;
                                        }
                                    case Constants.Subcompany:
                                        {
                                            if (cmb.SelectedItem == null)
                                                break;
                                            lstParams.SubCompanyName = ((SubCompany)(cmb.SelectedItem)).Sub_Company_Name;
                                            lstParams.SubCompany = ((SubCompany)(cmb.SelectedItem)).Sub_Company_ID;
                                            break;
                                        }
                                    case Constants.Region:
                                        {
                                            if (cmb.SelectedItem == null)
                                                break;
                                            lstParams.RegionName = ((SubCompanyRegion)(cmb.SelectedItem)).Sub_Company_Region_Name;
                                            lstParams.Region = ((SubCompanyRegion)(cmb.SelectedItem)).Sub_Company_Region_ID;
                                            break;
                                        }
                                    case Constants.Area:
                                        {
                                            if (cmb.SelectedItem == null)
                                                break;
                                            lstParams.AreaName = ((SubCompanyArea)(cmb.SelectedItem)).Sub_Company_Area_Name;
                                            lstParams.Area = ((SubCompanyArea)(cmb.SelectedItem)).Sub_Company_Area_ID;
                                            break;
                                        }
                                    case Constants.District:
                                        {
                                            if (cmb.SelectedItem == null)
                                                break;
                                            lstParams.DistrictName = ((SubCompanyDistrict)(cmb.SelectedItem)).Sub_Company_District_Name;
                                            lstParams.District = ((SubCompanyDistrict)(cmb.SelectedItem)).Sub_Company_District_ID;
                                            break;
                                        }
                                    case Constants.SiteID:
                                    case Constants.Site:
                                        {
                                            string strSiteList = string.Empty;

                                            if (cmb.SelectedItem == null)
                                                break;

                                            
                                            if ((((Site)(cmb.SelectedItem)).Site_Name.ToLower() == this.GetResourceTextByKey("Key_RX_AnyWithHyphens").ToLower()) || (((Site)(cmb.SelectedItem)).Site_Name.ToLower() == this.GetResourceTextByKey("Key_RX_AllWithHyphens").ToLower()))
                                            {
                                                List<Site> ositeList;
                                                ositeList = oReportBusiness.GetSites(lstParams.Company, lstParams.SubCompany, 0, 0, Program.UserID);
                                                ((List<Site>)ositeList).ForEach((x) => { if (x.Site_ID != 0) { strSiteList += ((strSiteList.Trim() == string.Empty) ? "" : ",") + x.Site_ID.ToString(); } });
                                                lstParams.SiteName = ((Site)(cmb.SelectedItem)).Site_Name;            // "--All--";
                                                lstParams.Site = 0;
                                                lstParams.SiteID = 0;
                                                lstParams.SiteIDList = strSiteList;                                                        
                                            }
                                            else if ((((Site)(cmb.SelectedItem)).Site_Name.ToLower() == this.GetResourceTextByKey("Key_RX_NoneWithHyphens").ToLower()))
                                            {
                                                lstParams.SiteName = ((Site)(cmb.SelectedItem)).Site_Name;            // "No Sites";
                                                lstParams.Site = 0;
                                                lstParams.SiteID = 0;
                                                lstParams.SiteIDList = "0";
                                                string ReportArg = ((GetAllReportsToRolesAccess)ReportObject).ReportArgName;
                                                if (ReportArg == "CappedGameSummaryReprot" || ReportArg == "CappedGameListReport")
                                                {
                                                    lstParams.Site = -1;
                                                    lstParams.SiteID = -1;
                                                    lstParams.SiteIDList = "-1";
                                                }
                                            }
                                            else
                                            {
                                                lstParams.SiteName = ((Site)(cmb.SelectedItem)).Site_Name;
                                                lstParams.Site = ((Site)(cmb.SelectedItem)).Site_ID;
                                                lstParams.SiteID = ((Site)(cmb.SelectedItem)).Site_ID;
                                                lstParams.SiteIDList = Convert.ToString(lstParams.SiteID);
                                            }
                                            break;
                                        }
                                    case Constants.Category:
                                        {
                                            if (cmb.SelectedItem == null)
                                                break;
                                            lstParams.CategoryName = ((Machine_Type)(cmb.SelectedItem)).Machine_Type_Code;
                                            lstParams.Category = ((Machine_Type)(cmb.SelectedItem)).Machine_Type_ID;
                                            break;
                                        }
                                    case Constants.Supplier:
                                        {
                                            if (cmb.SelectedItem == null)
                                                break;
                                            lstParams.SupplierName = ((Operator)(cmb.SelectedItem)).Operator_Name;
                                            lstParams.Supplier = ((Operator)(cmb.SelectedItem)).Operator_ID;
                                            break;
                                        }
                                    case Constants.Depot:
                                        {
                                            if (cmb.SelectedItem == null)
                                                break;
                                            lstParams.DepotName = ((Depot)(cmb.SelectedItem)).Depot_Name;
                                            lstParams.Depot = ((Depot)(cmb.SelectedItem)).Depot_ID;
                                            break;
                                        }

                                    case Constants.OrderBy:
                                        {
                                            if (cmb.SelectedItem == null)
                                                break;
                                            lstParams.OrderByName = ((OrderBy)(cmb.SelectedItem)).OrderBy_Name;
                                            lstParams.OrderBy = ((OrderBy)(cmb.SelectedItem)).OrderBy_ID;
                                            break;
                                        }
                                    case Constants.Zone:
                                        {
                                            if (cmb.SelectedItem == null)
                                                break;

                                            if (((Zone)(cmb.SelectedItem)).Zone_Name.ToLower() == this.GetResourceTextByKey("Key_RX_AnyWithHyphens").ToLower() || ((Zone)(cmb.SelectedItem)).Zone_Name.ToLower() == this.GetResourceTextByKey("Key_RX_AllWithHyphens").ToLower())
                                            {
                                                lstParams.ZoneName = this.GetResourceTextByKey("Key_RX_AllWithHyphens");
                                                lstParams.Zone = 0;
                                            }
                                            else
                                            {
                                                lstParams.ZoneName = ((Zone)(cmb.SelectedItem)).Zone_Name;
                                                lstParams.Zone = ((Zone)(cmb.SelectedItem)).Zone_ID;
                                            }
                                            break;
                                        }

                                    case Constants.VoucherStatus:
                                        {
                                            if (cmb.SelectedItem == null)
                                                break;
                                            //if (cmb.SelectedIndex <= 0)                                            
                                            //    lstParams.VoucherStatus = "All";                                            
                                            //else
                                                lstParams.VoucherStatus = cmb.SelectedValue.ToString();
                                            lstParams.VoucherStatusName = cmb.SelectedIndex > 0 ? cmb.SelectedValue.ToString() : cmb.Text;
                                            break;
                                        }
                                    case Constants.DeviceType:
                                        {
                                            if (cmb.SelectedItem == null)
                                                break;
                                            //if (cmb.SelectedIndex <= 0)
                                            //    lstParams.DeviceType = "All";
                                            //else
                                            lstParams.DeviceType = cmb.SelectedValue.ToString();
                                            lstParams.DeviceTypeName = cmb.SelectedIndex > 0 ? cmb.SelectedValue.ToString() : cmb.Text;
                                            break;
                                        }
                                    case Constants.Slot:
                                        {
                                            if (cmb.SelectedItem == null)
                                                break;
                                            if (cmb.SelectedIndex <= 0)
                                                lstParams.Slot = "All"; //Dont pass Externalized value here
                                            else
                                            lstParams.Slot = ((Slot)cmb.SelectedItem).Machine_Stock_No;
                                            lstParams.SlotName = cmb.SelectedValue.ToString();
                                            break;
                                        }
                                    case Constants.ReportPeriod:
                                        {
                                            if (cmb.SelectedItem == null)
                                                break;
                                            if (cmb.SelectedIndex <= 0)
                                                lstParams.ReportPeriod = "All"; //Dont pass Externalized value here
                                            else
                                                lstParams.ReportPeriod = cmb.SelectedValue.ToString();
                                            lstParams.ReportPeriodName = cmb.SelectedValue.ToString();
                                            break;
                                        }
                                    case Constants.GroupBy:
                                        {
                                            if (cmb.SelectedItem == null)
                                                break;
                                            lstParams.GroupBY = cmb.SelectedValue.ToString();
                                            break;
                                        }
                                    case Constants.WeeklyLiquid:
                                        {
                                            if (cmb.SelectedItem == null)
                                                break;
                                            lstParams.iStatement_No = ((PeriodEnd)(cmb.SelectedItem)).Statement_No.Value;
                                            break;
                                        }
                                    case Constants.Liquid:
                                        {
                                            if (cmb.SelectedItem == null)
                                                break;
                                            lstParams.Batch_No = ((BatchDetails)(cmb.SelectedItem)).Batch_ID.ToString();
                                            break;
                                        }
                                    case Constants.EmpID:
                                        {
                                            if (cmb.SelectedItem == null)
                                                break;
                                            lstParams.EmpID = Convert.ToString(((EmployeeID)(cmb.SelectedItem)).SecurityUserID);
                                            break;
                                        }
                                    case Constants.EmpCardID:
                                        {
                                            if (cmb.SelectedItem == null)
                                                break;
                                            if (cmb.SelectedIndex <= 0)
                                                lstParams.EmpCardID = "--All--"; //Dont pass Externalized value here
                                            else
                                            lstParams.EmpCardID = ((EmployeeCardID)(cmb.SelectedItem)).EmpCardID;
                                            break;
                                        }
                                    case Constants.CardNumber:
                                        {
                                            if (cmb.SelectedItem == null)
                                                break;
                                            lstParams.CardNumber = ((CardNumberList)(cmb.SelectedItem)).EmployeeCardNumber;
                                            if (lstParams.CardNumber.ToLower() == this.GetResourceTextByKey("Key_RX_AllWithHyphens").ToLower())
                                                lstParams.CardNumber = "0";
                                            break;
                                        }
                                    case Constants.EmployeeName:
                                        {
                                            if (cmb.SelectedItem == null)
                                                break;
                                            if (cmb.SelectedIndex <= 0)
                                                lstParams.EmployeeName = "--All--";
                                            else
                                                lstParams.EmployeeName = ((EmployeeNameList)(cmb.SelectedItem)).EmployeeName;
                                            break;
                                        }
                                    case Constants.CardType:
                                        {
                                            if (cmb.SelectedItem == null)
                                                break;
                                            if (cmb.SelectedIndex <= 0)
                                                lstParams.CardType = "--All--";
                                            else
                                                lstParams.CardType = ((CardTypes)(cmb.SelectedItem)).EmpCardType;
                                            break;
                                        }
                                    case Constants.RuleName:
                                        {
                                            if (cmb.SelectedItem == null)
                                                break;
                                            if (cmb.SelectedIndex <= 0)
                                                lstParams.RuleName = "--All--";
                                            else
                                                lstParams.RuleName = ((SiteLicensing)(cmb.SelectedItem)).RuleName;
                                            break;
                                        }

                                    case Constants.CardStatus:
                                        {
                                            if (cmb.SelectedItem == null)
                                                break;
                                            if (cmb.SelectedIndex <= 0)
                                                lstParams.CardStatus = "--All--";
                                            else 
                                                lstParams.CardStatus = Convert.ToString(cmb.SelectedValue);
                                            break;
                                        }
                                    case Constants.StockStatus:
                                        {
                                            if (cmb.SelectedItem == null)
                                                break;
                                            lstParams.StockStatus = Convert.ToInt32(cmb.SelectedValue);
                                            lstParams.StockStatusName = (cmb.SelectedItem as DataRowView)["Status"].ToString();
                                            break;
                                        }
                                    //(EP4 Changes)
                                    case Constants.VaultStatus:
                                        {
                                            if (cmb.SelectedItem == null)
                                                break;

                                            //if (cmb.SelectedIndex<=0)
                                            //    lstParams.VaultStatus = "--All--"; //Dont pass Externalized value here
                                            //else
                                                lstParams.VaultStatus = Convert.ToString(cmb.SelectedValue);
                                            break;
                                        }
                                    case Constants.TransactionType:
                                        {
                                            if (cmb.SelectedItem == null)
                                                break;
                                            //if (cmb.SelectedIndex <= 0)
                                            //    lstParams.TransactionType = "--All--"; //Dont pass Externalized value here
                                            //else
                                                lstParams.TransactionType = Convert.ToString(cmb.SelectedValue);
                                            break;
                                        }
                                    case Constants.InventoryType:
                                        {
                                            if (cmb.SelectedItem == null)
                                                break;
                                            //if (cmb.SelectedIndex <= 0)
                                            //    lstParams.InventoryType = "--All--"; //Dont pass Externalized value here
                                            //else
                                                lstParams.InventoryType = Convert.ToString(cmb.SelectedValue);
                                            break;
                                        }
                                    default:
                                        break;
                                }
                                break;

                            }
                        case "CHECKEDLISTBOX":
                            {
                                CheckedListBox clb = c as CheckedListBox;
                                string Periodlist = string.Empty;
                                List<Period> oPeriod;
                                oPeriod = oReportBusiness.GetPeriod();
                                oPeriod.ForEach(x => x.DisplayPeriodName = this.GetResourceTextByKey("Key_ReportFilter_Period_" + Regex.Replace(x.DisplayPeriodName, "[^a-zA-Z]+", string.Empty)));
                                if (clb.CheckedItems.Count > 0)
                                {
                                    for (int i = 0; i < clb.CheckedItems.Count; i++)
                                    {
                                        foreach (Period item in oPeriod)
                                        {
                                            if (item.DisplayPeriodName.ToLower() == clb.CheckedItems[i].ToString().ToLower())
                                            {
                                                if (i > 0)
                                                    Periodlist = Periodlist + "," + item.PeriodName;
                                                else
                                                    Periodlist = item.PeriodName;
                                                break;
                                            }
                                        }
                                    }
                                    lstParams.Period = Periodlist;
                                }
                                else
                                    lstParams.Period = "DAY";

                                break;
                            }

                    }
                }
            }
        }

        #endregion Events


    }
}
