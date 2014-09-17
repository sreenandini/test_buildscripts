using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BMC.Security.Manager;
using BMC.Security.Interfaces;
using BMC.Security.Entity;
using System.Data.SqlClient;
using BMC.DataAccess;
using BMC.Common.ConfigurationManagement;
using BMC.Common.LogManagement;
using BMC.Common.ExceptionManagement;
using Microsoft.Win32;
using BMC.EnterpriseReportsBusiness;
using BMC.EnterpriseReportsTransport;
using System.Configuration;
using BMC.Common;
using BMC.Reports;
using System.Globalization;
using BMC.ReportViewer;
using System.Text.RegularExpressions;
using BMC.CoreLib.Win32;


namespace BMC.EnterpriseReportsUI
{
    public partial class ReportsMain : Form
    {
        string ReportDateTimeFormat = string.Empty;
        string ReportDateFormat = string.Empty;

        clsSPParams lstParams = new clsSPParams();
        List<string> AlteredNodesName;
        List<string> AddedNodesName;
        bool isHrchyFilterInReports = false;
        string RoleName = "";
        int RoleID;
        string reportArg;
        int iTotalSiteCount;
        int _SiteID = 0;
        bool isGroupByZoneEnabled = false;
        ReportsBusiness oReportBusiness;
        Criteria oCriteria = null;
        Depreciation oDepreciation = null;
        IEnumerable<GetAllReportsToRolesAccess> reports = null;
        List<SqlParameter> SPParamlist = null;
        Label lblReportName = null;
        List<string> NonMgmtList = new List<string>() { "DEPRECIATIONREPORT" };
        int companyID = 0;
        int SubCompanyID = 0;
        int SRegion_id = 0;
        int area = 0;
        int iSecurityUserID = 0;

        //Commented by Manoj - Anu has tested & informed that SDSList & DateTimeList is not required - 2nd June 2014
        //If DateTime param is of type Varchar - We should not display Text box insteda we should display DateTime Picker
        //List<string> DateTimeList = new List<string>() { "MAJORPRIZESREPORT", "VOUCHERCOUPONLIABLITYREPORT", "REDEEMEDVOUCHERBYDEVICEREPORT", 
        //                                                 "EXPIREDVOUCHERCOUPONREPORT", "VOUCHERLISTINGREPORT", "JACKPOTSLIPSUMMARYREPORT", 
        //                                                 "MULTIGAMEMULTIDENOMREPORT", "COININBYPAYTABLEREPORT", "MULTIDENOMSLOTDETAILREPORT", 
        //                                                 "MULTIGAMESLOTDETAILREPORT", "MGMDDENOMPERFORMANCE", "GAMEPERFORMANCEREPORT", "GAMEREPORT","LOTTERYREVENUE_US",
        //                                                   "CROSSPROPERTYLIABILITYTRANSFERSUMMARYREPORT","CROSSPROPERTYLIABILITYTRANSFERDETAILSREPORT",
        //                                                  "CROSSPROPERTYTICKETANALYSISREPORT","EMPLOYEECARDSESSIONSREPORT","BMC_EFTQUESTIONABLETRANSACTIONS","BMC_EFTSLOTACTIVITY","BMC_EFTSLOTACTIVITYCUMULATIVE",
        //                                                 "CAPPEDGAMESUMMARYREPORT","CAPPEDGAMELISTREPORT","PROMOTIONALSUMMARYREPORT","PROMOTIONALVOUCHERLISTINGREPORT",
        //                                                 "CASHDISPENSERTRANSACTIONDETAILS", "CASHDISPENSERVARIANCEREPORT", "CASHDISPENSERDROPREPORT","PERIODENDLIQUIDATIONREVENUEREPORT","LIQUIDATIONEXPENSEREPORT","CASHDISPENSERCASSETTEACCOUNTINGDETAIL","CASHDISPENSERACCOUNTING"}; 	

        //List<string> SDSList = new List<string>() { "BMC_METERCOMPARISON", "BMC_SLOTPERFORMANCE", "BMC_SOFTCOUNTCOMPARISON", "BMC_DAILYEFTFUNDREVENUE",  
        //                                            "WINCOMPARISONREPORT","RPT_PERIODENDLIQUIDATIONREVENUE","LIQUIDATIONEXPENSEREPORT","MGMDBYGAMINGDEVICECABINETREPORT","MANUFACTURERPERFORMANCEREPORT","MGMDSUMMARYANALYSIS","TotalFundsIn","TotalFundsInDetails","CashDispenserInventoryStatusReport"};



        /// <summary>
        /// Tool Tip
        /// </summary>
        /// 
        private ToolTip ToolTipForReports = null;

        /// <summary>
        /// Tool Tip
        /// </summary>
        public ReportsMain()
        {
            RoleName = "Super User";
            InitializeComponent();
            reportArg = "No Reports";
            oReportBusiness = new ReportsBusiness();
            ToolTipForReports = new ToolTip();
            //lstReportParams = new ListReportparameters();
        }

        public ReportsMain(string roleName, string ReportArg)
        {
            try
            {
                InitializeComponent();
                RoleName = roleName;
                LogManager.WriteLog("Enterprise Reports *** Role Name : " + RoleName, LogManager.enumLogLevel.Info);
                reportArg = ReportArg;
                oReportBusiness = new ReportsBusiness();
                ToolTipForReports = new ToolTip();
                iSecurityUserID = Program.UserID;
            }
            catch (Exception ex)
            {

                ExceptionManager.Publish(ex);
            }
        }


        public string CurrencySymbolofCulture(string CurrencyCulture)
        {
            return string.IsNullOrEmpty(CurrencyCulture) ? string.Empty : new RegionInfo(CurrencyCulture).CurrencySymbol;
        }

        private void PaintGradient()
        {
            System.Drawing.Drawing2D.LinearGradientBrush gradBrush;
            System.Drawing.Drawing2D.ColorBlend clrbld;
            clrbld = new System.Drawing.Drawing2D.ColorBlend(3);
            gradBrush = new System.Drawing.Drawing2D.LinearGradientBrush(new Point(0, 0), new Point(this.Width, this.Height), Color.FromArgb(170, 200, 255), Color.White);
            Bitmap bmp = new Bitmap(this.Width, this.Height);
            Graphics g = Graphics.FromImage(bmp);
            g.FillRectangle(gradBrush, new Rectangle(0, 0, this.Width, this.Height));
            this.BackgroundImage = bmp;
            this.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void LoadTreeViewForRole(int RoleID)
        {
            TreeNode RoleNode = new TreeNode();
            try
            {
                CreateNodesinTreeView(RoleID);
                foreach (TreeNode node in tvRoles.Nodes)
                {
                    if (node.Text == "SGVI Lottery Reports" && !(Convert.ToBoolean(oReportBusiness.GetSetting("SGVI_Enabled"))) &&
                        !(oReportBusiness.GetSetting("Client") == "SGVI"))
                        node.Text = "Lottery Reports";
                    else if (node.Text == "SGVI Lottery Reports" && (Convert.ToBoolean(oReportBusiness.GetSetting("SGVI_Enabled"))) &&
                        (oReportBusiness.GetSetting("Client") == "SGVI"))
                        node.Text = "SGVI Lottery Reports";

                    SetImageRecursive(node);
                }
                AlteredNodesName = new List<string>();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void SelectedIndexChanged(object sender, EventArgs e)
        {
            SetToolTip((Control)sender, 30);
        }

        /// <summary>
        /// Tool Tip 
        /// </summary>
        /// <param name="treeNode"></param>
        /// 
        private void SetToolTip(Control windowsControl, int length)
        {
            try
            {
                String caption = "" + windowsControl.Text;
                ToolTipForReports.SetToolTip(windowsControl, (caption.Trim().Length > length) ? caption : "");
                ToolTipForReports.AutoPopDelay = 2000;
                ToolTipForReports.InitialDelay = 0;
                ToolTipForReports.ReshowDelay = 100;
                ToolTipForReports.ShowAlways = true;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
        /// <summary>
        /// Tool Tip
        /// </summary>
        /// <param name="treeNode"></param>


        private void SetImageRecursive(TreeNode treeNode)
        {
            foreach (TreeNode tn in treeNode.Nodes)
            {
                if (tn.Nodes.Count == 0)
                    tn.ImageIndex = 1;
                else
                    tn.ImageIndex = 0;

                SetImageRecursive(tn);
            }
        }

        private void CreateNodesinTreeView(int RoleID)
        {
            List<TreeNode> parentNodeList = new List<TreeNode>();
            TreeNode tempNode = null;
            TreeNode tempParentNode = null;
            TreeNode[] arrTempNodes;
            string parentId = string.Empty;
            string reportId = string.Empty;
            string parentName = string.Empty;
            string reportName = string.Empty;

            tvRoles.Nodes.Clear();
            try
            {
                reports = oReportBusiness.GetAllReportsToRolesAccess(RoleID);
                foreach (GetAllReportsToRolesAccess rra in reports)
                {
                    tempParentNode = null;
                    parentId = rra.ParentID.ToString().Trim();
                    reportId = rra.ReportID.ToString().Trim();
                    parentName = rra.ParentName.ToString().Trim();
                    reportName = rra.ReportName.ToString().Trim();

                    if (rra.Level <= 0)
                        continue;

                    if (string.IsNullOrEmpty(parentName))
                    {
                        tempNode = tvRoles.Nodes.Add(reportId, this.GetResourceTextByKey("Key_DBV_"+ Regex.Replace(reportName.ToString().Trim(), "[^0-9a-zA-Z]+", "")));
                    }
                    else
                    {
                        arrTempNodes = tvRoles.Nodes.Find(parentId, true);
                        if (arrTempNodes.Length > 0)
                        {
                            tempParentNode = arrTempNodes[0];
                        }
                        else
                        {
                            tempParentNode = tvRoles.Nodes.Add(parentId, this.GetResourceTextByKey("Key_DBV_"+Regex.Replace(parentName.ToString().Trim(), "[^0-9a-zA-Z]+", "")));
                        }
                        tempNode = tempParentNode.Nodes.Add(reportId, this.GetResourceTextByKey("Key_DBV_" +Regex.Replace(reportName.ToString().Trim(), "[^0-9a-zA-Z]+", "")));
                    }
                    tempNode.Tag = rra;
                    tempNode.Name = reportName;
                }

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            tvRoles.ShowPlusMinus = true;
            
            
            /*tvRoles.Nodes.Clear();
            try
            {
                reports = oReportBusiness.GetAllReportsToRolesAccess(RoleID);

                foreach (GetAllReportsToRolesAccess rra in reports)
                {
                    if (rra.Level > 0)
                    {
                        if (rra.ReportArgName == Constants.AuditTrail)
                            continue;

                        if (string.IsNullOrEmpty(rra.ParentName))
                        {
                            tvRoles.Nodes.Add(rra.ReportID.ToString().Trim() + "~" + rra.ReportDescription.Trim(), rra.ReportName.ToString().Trim());
                        }
                        else
                        {
                            TreeNode[] oNode = (tvRoles.Nodes.Find(rra.ParentID.ToString() + "~" + rra.ParentName.ToString().Trim(), true));
                            if (oNode.Length > 0)
                                AddNode(rra.ParentID.ToString().Trim() + "~" + rra.ParentName.ToString().Trim(), rra.ReportID.ToString().Trim() + "~" + rra.ReportDescription.ToString().Trim(), rra.ReportName.ToString().Trim());
                            else
                            {
                                tvRoles.Nodes.Add(rra.ParentID.ToString().Trim() + "~" + rra.ParentName.ToString().Trim(), rra.ParentName.ToString().Trim());
                                AddNode(rra.ParentID.ToString().Trim() + "~" + rra.ParentName.ToString().Trim(), rra.ReportID.ToString().Trim() + "~" + rra.ReportDescription.ToString().Trim(), rra.ReportName.ToString().Trim());
                            }
                        }
                    }
                    else
                        ExceptionInfo = new List<string>(new string[] {rra.ReportID.ToString().Trim(),
                           rra.ReportArgName.ToString().Trim(),System.Convert.ToBoolean(rra.ReportStatus.ToString()) == true ? "1" : "0",
                            rra.MS_ProcedureUsed,rra.ReportName});

                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            //tvRoles.ExpandAll();
            
            tvRoles.ShowPlusMinus = true;
             * */
        }

        private void SetReportCriteria(TreeNode node)
        {
            try
            {
                    if (oCriteria != null)
                        oCriteria.Close();
                    AddCriteriaControls(node);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
        private void AddCriteriaControls(TreeNode rNode)
        {
            GetAllReportsToRolesAccess rra = (GetAllReportsToRolesAccess)rNode.Tag;
            int W = 220;
            int H = 25;
            int X = 160;
            int Y = 100;
            int lblX = 40;
            int lblY = 105;
            try
            {
                reportArg = rra.ReportArgName;
                this.pnlChild.Controls.Clear();
                this.pnlChild.BorderStyle = BorderStyle.FixedSingle;
                this.pnlChild.Width = 425;
                this.Width = 425 + 350;
                if (!NonMgmtList.Contains(rra.ReportArgName))
                {

                    oCriteria = new Criteria();
                    oCriteria.ReportObject = rra;
                    oCriteria.delCriteriaClosed += new Criteria.CriteriaHandler(delCriteriaClosed);
                    oCriteria.TopLevel = false;
                    oCriteria.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;   //Optional
                    oCriteria.TopMost = true;
                    oCriteria.Dock = DockStyle.Fill;
                    oCriteria.XMLName = Application.StartupPath + System.IO.Path.DirectorySeparatorChar + rra.XMLName;
                    SetPanelControls(rNode, false);
                    this.pnlChild.Controls.Add(oCriteria);
                    this.pnlChild.Width = oCriteria.Width + lblReportName.PreferredSize.Width / 3;
                    this.pnlChild.Height = oCriteria.Height; ;
                    oCriteria.Controls.Add(lblReportName);
                    SPParamlist = oReportBusiness.GetParameters(rra.MS_ProcedureUsed);
                    isGroupByZoneEnabled = (oReportBusiness.GetSetting("IsSuppressZoneEnabled").ToUpper() == "TRUE") ? true : false;
                    isHrchyFilterInReports = (oReportBusiness.GetSetting("HrchyFilterInReports", "True").ToUpper() == "TRUE") ? true : false;

                    if (SPParamlist.Count > 0)
                    {
                        ComboBox ctl = (ComboBox)CreateControl(DbType.Int32, X, Y, W, H, Constants.Company);
                        oCriteria.Controls.Add(ctl);
                        Label lblCompany = (Label)CreateControl(DbType.AnsiStringFixedLength, lblX, lblY, W, H, this.GetResourceTextByKey("Key_Company"));
                        oCriteria.Controls.Add(lblCompany);
                        List<Company> oCompany;
                        oCompany = oReportBusiness.CompanyDetails(iSecurityUserID);
                        FillCombo(ctl, oCompany, Constants.Company, Constants.cmbCompValue, Constants.cmbCompText);

                        Y = Y + 25;
                        lblY = lblY + 25;
                        foreach (SqlParameter sp in SPParamlist)
                        {

                            //No need to Create DateFormat Input parameter. This value will passed as common parameter
                            if (sp.ParameterName.Replace("@", "").ToUpper() == "DATEFORMAT")
                                continue;
                            //If userid is needed as filter add exception for your report here
                            if (sp.ParameterName.Replace("@", "").ToUpper() == Constants.UserID)
                                continue;
                            if (sp.ParameterName.Replace("@", "").ToUpper() == Constants.SiteIDList)
                                continue;

                            if (sp.ParameterName.Replace("@", "").ToUpper() != "COMPANY")
                            {
                                if (rra.ReportArgName.ToUpper().Equals("LIQUIDATION"))
                                {
                                    oCriteria.Controls.Add(CreateControl(DbType.Int32, X, Y, W, H, Constants.Subcompany));
                                    oCriteria.Controls.Add(CreateControl(DbType.AnsiStringFixedLength, lblX, lblY, W, H, this.GetResourceTextByKey("Key_Subcompany")));
                                    Y = Y + 25;
                                    lblY = lblY + 25;

                                    oCriteria.Controls.Add(CreateControl(DbType.Int32, X, Y, W, H, Constants.Region));
                                    oCriteria.Controls.Add(CreateControl(DbType.AnsiStringFixedLength, lblX, lblY, W, H, this.GetResourceTextByKey("Key_Region")));
                                    Y = Y + 25;
                                    lblY = lblY + 25;

                                    oCriteria.Controls.Add(CreateControl(DbType.Int32, X, Y, W, H, Constants.Area));
                                    oCriteria.Controls.Add(CreateControl(DbType.AnsiStringFixedLength, lblX, lblY, W, H, this.GetResourceTextByKey("Key_Area")));
                                    Y = Y + 25;
                                    lblY = lblY + 25;

                                    oCriteria.Controls.Add(CreateControl(DbType.Int32, X, Y, W, H, Constants.District));
                                    oCriteria.Controls.Add(CreateControl(DbType.AnsiStringFixedLength, lblX, lblY, W, H, this.GetResourceTextByKey("Key_District")));
                                    Y = Y + 25;
                                    lblY = lblY + 25;

                                    oCriteria.Controls.Add(CreateControl(DbType.Int32, X, Y, W, H, Constants.Site));
                                    oCriteria.Controls.Add(CreateControl(DbType.AnsiStringFixedLength, lblX, lblY, W, H, this.GetResourceTextByKey("Key_Site")));
                                    Y = Y + 25;
                                    lblY = lblY + 25;

                                    oCriteria.Controls.Add(CreateControl(DbType.Int32, X, Y, W, H, sp.ParameterName.Substring(1)));
                                    oCriteria.Controls.Add(CreateControl(DbType.AnsiStringFixedLength, lblX, lblY, W, H, sp.ParameterName.Substring(1)));
                                    Y = Y + 25;
                                    lblY = lblY + 25;
                                }
                                Control[] cmb = oCriteria.Controls.Find(sp.ParameterName.Replace("@", "").ToUpper(), true);
                                if (cmb.Length > 0)
                                    continue;
                                //Commented by Manoj - Anu has tested & informed that SDSList & DateTimeList is not required - 2nd June 2014
                                //if (sp.DbType == DbType.String && !SDSList.Contains(rra.ReportArgName.Trim().ToUpper()))
                                //    continue;
                                //if (DateTimeList.Contains(rra.ReportArgName.Trim().ToUpper()) && sp.DbType == DbType.DateTime)
                                //{
                                //    oCriteria.Controls.Add(CreateControl(DbType.DateTime2, X, Y, W, H, sp.ParameterName));
                                //    //Temporary Log
                                //    LogManager.WriteLog("DateTimeList is used by Report Argument:" + rra.ReportArgName, LogManager.enumLogLevel.Info);
                                //}


                                if (sp.DbType == DbType.Boolean)
                                {
                                    if (!rra.ReportArgName.ToUpper().Equals("DAILYACCOUNTINGSUMMARY"))
                                        oCriteria.Controls.Add(CreateControl(sp.DbType, X, Y, W, H, sp.ParameterName));
                                }
                                else
                                {
                                    Control[] ctrl = oCriteria.Controls.Find(sp.ParameterName.Replace("@", "").ToUpper(), true);

                                    if (ctrl.Length == 0)
                                    {
                                        if ((sp.DbType == DbType.DateTime) && (rra.IsTimeRequired == true))
                                        {
                                            oCriteria.Controls.Add(CreateControl(DbType.DateTime2, X, Y, W, H, sp.ParameterName));
                                        }
                                        else
                                        {
                                            oCriteria.Controls.Add(CreateControl(sp.DbType, X, Y, W, H, sp.ParameterName));
                                        }
                                    }

                                }

                                if (sp.DbType != DbType.Boolean)
                                {
                                    oCriteria.Controls.Add(CreateControl(DbType.AnsiStringFixedLength, lblX, lblY, W, H, sp.ParameterName));
                                }
                                Y = Y + 25;
                                lblY = lblY + 25;

                            }
                        }
                        if (rra.ReportArgName.ToUpper().Equals("JACKPOTSLIPSUMMARYREPORT"))
                        {
                            foreach (Control chkCtl in oCriteria.Controls)
                            {
                                if (chkCtl is CheckBox)
                                {
                                    (chkCtl as CheckBox).Checked = true;
                                }
                            }
                        }
                        else
                        {
                            if (rra.ReportArgName.ToUpper().Equals("MGMDBYGAMINGDEVICECABINETREPORT") || rra.ReportArgName.ToUpper().Equals("MGMDSUMMARYANALYSIS"))
                                foreach (Control chkCtl in oCriteria.Controls)
                                {
                                    if (chkCtl is CheckBox)
                                    {
                                        chkCtl.Top += 85;//add height of checked list box  
                                    }
                                }
                        }

                        if (rra.ReportArgName.ToUpper().Equals("VOUCHERLISTINGREPORT"))
                        {
                            //Control chkctl in oCriteria.Controls
                            Control chkctl = new Control();
                            if (chkctl is CheckBox)
                            {
                                (chkctl as CheckBox).Checked = true;

                            }
                        }
                        Control[] cmbCmp = oCriteria.Controls.Find(Constants.Company, true);
                        if (cmbCmp.Length > 0)
                        {
                            ComboBox parentctl = cmbCmp[0] as ComboBox;
                            List<Company> obCompany;
                            obCompany = oReportBusiness.CompanyDetails(iSecurityUserID);
                            FillCombo(parentctl, obCompany, Constants.Company, Constants.cmbCompValue, Constants.cmbCompText);
                        }

                        Control[] cmbRule = oCriteria.Controls.Find(Constants.RuleName, true);
                        if (cmbRule.Length > 0)
                        {
                            ComboBox cmbRule1 = cmbRule[0] as ComboBox;
                            List<SiteLicensing> obRule;
                            obRule = oReportBusiness.GetRuleName();
                            FillCombo(cmbRule1, obRule, Constants.RuleName, Constants.cmbRuleNameValue, Constants.cmbRuleNameText);
                        }

                        Control[] cmbStatus = oCriteria.Controls.Find("VoucherStatus", true);
                        if (cmbStatus.Length > 0)
                        {
                            ComboBox myCombo = cmbStatus[0] as ComboBox;
                            //Dont externalize the following Combo Box Items.
                            //myCombo.Items.Add(new ListItem(1, "ALL"));
                            List<ComboBoxItem> values = new List<ComboBoxItem>
                                {
                                    new ComboBoxItem { Text=this.GetResourceTextByKey("Key_RX_AllWithHyphens"), Value="All"},
                                    new ComboBoxItem { Value="ACTIVE" },
                                    new ComboBoxItem { Value="CANCELLED" },
                                    new ComboBoxItem { Value="EXPIRED" }
                                };

                            //myCombo.Items.Add(new ListItem(1, this.GetResourceTextByKey("Key_RX_AllWithHyphens")));
                            //myCombo.Items.Add(new ListItem(6, "ACTIVE"));
                            //myCombo.Items.Add(new ListItem(5, "CANCELLED"));
                            //myCombo.Items.Add(new ListItem(4, "EXPIRED"));

                            if (rra.ReportName == "Voucher Listing Report")
                                values.Add(new ComboBoxItem { Value = "PARTIALLYPAID" });
                                //myCombo.Items.Add(new ListItem(8, "PARTIALLYPAID"));

                            if (rra.ReportName != "Voucher/Coupon Liability Report")
                                values.Add(new ComboBoxItem { Value = "PAID" });
                                //myCombo.Items.Add(new ListItem(2, "PAID"));

                            values.Add(new ComboBoxItem { Value = "VOID" });
                            //myCombo.Items.Add(new ListItem(3, "VOID"));

                            if (!(rra.ReportName == "Voucher Listing Report" || rra.ReportName == "Voucher/Coupon Liability Report"))
                                values.Add(new ComboBoxItem { Value = "LIABILITYTRANSFER" });
                            //myCombo.Items.Add(new ListItem(7, "LIABILITYTRANSFER"));

                            values.ForEach(x => x.Text = string.IsNullOrEmpty(x.Text) ? this.GetResourceTextByKey("Key_ReportFilter_VoucherStatus_" + Regex.Replace(x.Value.ToString(), Constants.SpecialCharacterPattern, string.Empty)) : x.Text);
                            myCombo.DataSource = values;
                            myCombo.DisplayMember = "Text";
                            myCombo.ValueMember = "Value";
                            myCombo.SelectedIndex = 0;
                        }
                        //DeviceType
                        Control[] cmbDeviceType = oCriteria.Controls.Find("DeviceType", true);
                        if (cmbDeviceType.Length > 0)
                        {
                            //Dont externalize the following Combo Box Items.
                            ComboBox myCombo = cmbDeviceType[0] as ComboBox;
                            List<ComboBoxItem> values = new List<ComboBoxItem>
                                {
                                    new ComboBoxItem { Text=this.GetResourceTextByKey("Key_RX_AllWithHyphens"), Value="All"},
                                    new ComboBoxItem { Value="CASHDESK" },
                                    new ComboBoxItem { Value="SLOT" }
                                };

                            values.ForEach(x => x.Text = string.IsNullOrEmpty(x.Text) ? this.GetResourceTextByKey("Key_ReportFilter_DeviceType_" + Regex.Replace(x.Value.ToString(), Constants.SpecialCharacterPattern, string.Empty)) : x.Text);
                            myCombo.DataSource = values;
                            myCombo.DisplayMember = "Text";
                            myCombo.ValueMember = "Value";
                            //myCombo.Items.Add(new ListItem(1, "ALL"));
                            //myCombo.Items.Add(new ListItem(1, this.GetResourceTextByKey("Key_RX_AllWithHyphens")));
                            //myCombo.Items.Add(new ListItem(3, "CASHDESK"));
                            //myCombo.Items.Add(new ListItem(2, "SLOT"));
                            myCombo.SelectedIndex = 0;

                        }

                        Control[] cmbSlots = oCriteria.Controls.Find("Slot", true);
                        if (cmbSlots.Length > 0)
                        {
                            ComboBox myCombo = cmbSlots[0] as ComboBox;
                            ComboBox cmbComp = (ComboBox)this.Controls.Find(Constants.Company, true)[0];
                            ComboBox cmbSubComp = (ComboBox)this.Controls.Find(Constants.Subcompany, true)[0];
                            List<Slot> mySlots = null;
                            try
                            {
                                mySlots = oReportBusiness.GetSlots(0, ((Company)(cmbComp.SelectedItem)).Company_ID, ((SubCompany)(cmbSubComp.SelectedItem)).Sub_Company_ID);
                            }
                            catch (Exception Ex)
                            {
                                mySlots = oReportBusiness.GetSlots(0, 0, 0);
                                LogManager.WriteLog("Unable to load slots for criteria", LogManager.enumLogLevel.Error);
                                ExceptionManager.Publish(Ex);
                            }

                            FillCombo(myCombo, mySlots, "Slot", "Machine_Stock_No", "Machine_Stock_No");

                        }

                        Control[] cmbRptPeriod = oCriteria.Controls.Find("ReportPeriod", true);
                        if (cmbRptPeriod.Length > 0)
                        {
                            List<ComboBoxItem> lstComboBoxItem = new List<ComboBoxItem>();
                            //Dont externalize the following Combo Box Items.
                            ComboBox myCombo = cmbRptPeriod[0] as ComboBox;
                            lstComboBoxItem.Clear();
                            lstComboBoxItem.Add(new ComboBoxItem() { Text = this.GetResourceTextByKey("Key_ReportFilter_ReportPeriod_ALL").ToUpper(), Value = "ALL" });
                            lstComboBoxItem.Add(new ComboBoxItem() { Text = this.GetResourceTextByKey("Key_ReportFilter_ReportPeriod_DAY").ToUpper(), Value = "DAY" });
                            lstComboBoxItem.Add(new ComboBoxItem() { Text = this.GetResourceTextByKey("Key_ReportFilter_ReportPeriod_PTD").ToUpper(), Value = "PTD" });
                            lstComboBoxItem.Add(new ComboBoxItem() { Text = this.GetResourceTextByKey("Key_ReportFilter_ReportPeriod_MTD").ToUpper(), Value = "MTD" });
                            lstComboBoxItem.Add(new ComboBoxItem() { Text = this.GetResourceTextByKey("Key_ReportFilter_ReportPeriod_QTD").ToUpper(), Value = "QTD" });
                            lstComboBoxItem.Add(new ComboBoxItem() { Text = this.GetResourceTextByKey("Key_ReportFilter_ReportPeriod_YTD").ToUpper(), Value = "YTD" });
                            lstComboBoxItem.Add(new ComboBoxItem() { Text = this.GetResourceTextByKey("Key_ReportFilter_ReportPeriod_LTD").ToUpper(), Value = "LTD" });
                            myCombo.DataSource = lstComboBoxItem;
                            myCombo.ValueMember = "Value";
                            myCombo.DisplayMember = "Text";
                         
                            myCombo.SelectedIndex = 0;
                        }
                        Control[] cmbOrder = oCriteria.Controls.Find("OrderBy", true);
                        if (cmbOrder.Length > 0)
                        {
                            if (reportArg == "CappedGameSummaryReport")
                            {
                                ComboBox myCombo = cmbOrder[0] as ComboBox;
                                List<OrderBy> orderBy = new List<OrderBy>
                                {
                                    new OrderBy { OrderBy_ID = 1, OrderBy_Name = "Slot" },
                                    new OrderBy { OrderBy_ID = 2, OrderBy_Name = "Stand"}
                                };
                                orderBy.ForEach(x => x.Display_Name = this.GetResourceTextByKey("Key_ReportFilter_OrderBy_" + Regex.Replace(x.OrderBy_Name, Constants.SpecialCharacterPattern, string.Empty)));
                                myCombo.DataSource = orderBy;
                                myCombo.DisplayMember = "Display_Name";
                                myCombo.ValueMember = "OrderBy_ID";
                                myCombo.SelectedIndex = 0;
                            }
                            if (reportArg == "CappedGameListReport")
                            {
                                ComboBox myCombo = cmbOrder[0] as ComboBox;
                                List<OrderBy> orderBy = new List<OrderBy>
                                {
                                    new OrderBy { OrderBy_ID = 1, OrderBy_Name = "Slot" },
                                    new OrderBy { OrderBy_ID = 2, OrderBy_Name = "Stand"},
                                    new OrderBy { OrderBy_ID = 3, OrderBy_Name = "Reserved Date/Time"}
                                };
                                orderBy.ForEach(x => x.Display_Name = this.GetResourceTextByKey("Key_ReportFilter_OrderBy_" + Regex.Replace(x.OrderBy_Name, Constants.SpecialCharacterPattern, string.Empty)));
                                myCombo.DataSource = orderBy;
                                myCombo.DisplayMember = "Display_Name";
                                myCombo.ValueMember = "OrderBy_ID";
                                myCombo.SelectedIndex = 0;
                            }
                        }
                        Control[] cmbGrp = oCriteria.Controls.Find("GroupBY", true);
                        if (cmbGrp.Length > 0)
                        {
                            List<ComboBoxItem> lstComboBoxItem = new List<ComboBoxItem>();
                            if (reportArg == "MultiGameMultiDenomReport")
                            {
                                ComboBox myCombo = cmbGrp[0] as ComboBox;
                                lstComboBoxItem.Clear();
                                lstComboBoxItem.Add(new ComboBoxItem() { Text = this.GetResourceTextByKey("Key_ReportFilter_GroupBy_Game").ToUpper(), Value = "GAME" });
                                lstComboBoxItem.Add(new ComboBoxItem() { Text = this.GetResourceTextByKey("Key_ReportFilter_GroupBy_Denom").ToUpper(), Value = "DENOM" });
                                lstComboBoxItem.Add(new ComboBoxItem() { Text = this.GetResourceTextByKey("Key_ReportFilter_GroupBy_PAYTABLE").ToUpper(), Value = "PAYTABLE" });
                                myCombo.DataSource = lstComboBoxItem;
                                myCombo.ValueMember="Value";
                                myCombo.DisplayMember="Text";
                                

                                myCombo.SelectedIndex = 0;
                            }

                            if (reportArg == "MultiDenomSlotDetailReport" | reportArg == "CoinInbyPaytableReport")
                            {
                                ComboBox myCombo = cmbGrp[0] as ComboBox;
                                lstComboBoxItem.Clear();
                                lstComboBoxItem.Add(new ComboBoxItem() { Text = this.GetResourceTextByKey("Key_ReportFilter_GroupBy_Asset"), Value = "Asset" });
                                lstComboBoxItem.Add(new ComboBoxItem() { Text = this.GetResourceTextByKey("Key_ReportFilter_GroupBy_Denom").ToUpper(), Value = "Denom" });
                                myCombo.DataSource = lstComboBoxItem;
                                myCombo.ValueMember = "Value";
                                myCombo.DisplayMember = "Text";
                                
                                myCombo.SelectedIndex = 0;
                            }

                            if (reportArg == "MultiGameSlotDetailReport")
                            {
                                ComboBox myCombo = cmbGrp[0] as ComboBox;
                                lstComboBoxItem.Clear();
                                lstComboBoxItem.Add(new ComboBoxItem() { Text = this.GetResourceTextByKey("Key_ReportFilter_GroupBy_Asset"), Value = "Asset" });
                                lstComboBoxItem.Add(new ComboBoxItem() { Text = this.GetResourceTextByKey("Key_ReportFilter_GroupBy_Game").ToUpper(), Value = "Game" });
                                myCombo.DataSource = lstComboBoxItem;
                                myCombo.ValueMember = "Value";
                                myCombo.DisplayMember = "Text";
                               

                                myCombo.SelectedIndex = 0;
                            }
                        }
                    }

                    //Commented by Manoj - Anu has tested & informed that SDSList & DateTimeList is not required - 2nd June 2014
                    //if (SDSList.Contains(rra.ReportArgName))
                    //{ oCriteria.SDSReports = true; }
                    oCriteria.SetButtonsPosition();
                    oCriteria.Show();
                }
                else if (Constants.Depreciation.Contains(rra.ReportArgName))
                {
                    oDepreciation = new Depreciation();
                    this.pnlChild.Width = oDepreciation.Width;
                    this.pnlChild.Height = oDepreciation.Height;
                    oDepreciation.TopLevel = false;
                    oDepreciation.Dock = DockStyle.Fill;
                    this.pnlChild.Controls.Add(oDepreciation);
                    oDepreciation.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;   //Optional
                    oDepreciation.TopMost = true;
                    oDepreciation.Show();
                }
                this.Width = this.pnlChild.Width + this.gpTree.Width;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private Control CreateControl(DbType Value, int Left, int Top, int Width, int Height, string paramName)
        {

            Control oCtl = null;
            string resolvedText = string.Empty;

            try
            {
                if (!string.IsNullOrWhiteSpace(paramName))
                {
                    resolvedText = this.GetResourceTextByKey("Key_" + paramName.Replace("@", "").ToUpper());
                }

                switch (Value)
                {
                    case DbType.AnsiString:
                    case DbType.Int32:
                        {
                            oCtl = new ComboBox();

                            //Tool Tip
                            ((ComboBox)oCtl).SelectedIndexChanged += SelectedIndexChanged;
                            oCtl.MouseHover += SelectedIndexChanged;
                            //Tool TIP
                            oCtl.Width = Width;
                            oCtl.Height = Height;
                            Point p = new Point(Left, Top);
                            oCtl.Left = p.X;
                            oCtl.Top = p.Y;
                            oCtl.Name = paramName.Replace("@", "").ToUpper();

                            if (string.Compare(oCtl.Name, "StackerLevel", true) == 0)
                            {
                                ComboBox cbox = oCtl as ComboBox;
                                cbox.DropDownStyle = ComboBoxStyle.DropDownList;
                                cbox.Items.Add(this.GetResourceTextByKey("Key_RX_AllWithHyphens"));

                                for (int i = 1; i <= 100; i++)
                                {
                                    cbox.Items.Add(i);
                                }
                                cbox.SelectedIndex = 0;
                            }

                            if (string.Compare(oCtl.Name, Constants.CardStatus, true) == 0)
                            {
                                List<ComboBoxItem> lstComboBoxItem = new List<ComboBoxItem>();
                                ComboBox cbox = oCtl as ComboBox;
                                cbox.DropDownStyle = ComboBoxStyle.DropDownList;
                                lstComboBoxItem.Add(new ComboBoxItem() { Text = this.GetResourceTextByKey("Key_RX_AllWithHyphens").ToUpper(), Value = "--All--" });
                                lstComboBoxItem.Add(new ComboBoxItem() { Text = this.GetResourceTextByKey("Key_Active").ToUpper(), Value = "Active" });
                                lstComboBoxItem.Add(new ComboBoxItem() { Text = this.GetResourceTextByKey("Key_InActive").ToUpper(), Value = "InActive" });
                                //cbox.Items.Add(this.GetResourceTextByKey("Key_RX_AllWithHyphens"));
                                //cbox.Items.Add("Active");
                                //cbox.Items.Add("InActive");
                                cbox.DataSource = lstComboBoxItem;
                                cbox.DisplayMember = "Text";
                                cbox.ValueMember = "Value";
                                cbox.SelectedIndex = 0;
                            }

                            if (string.Compare(oCtl.Name, Constants.RuleName, true) == 0)
                            {
                                ComboBox cbox = oCtl as ComboBox;
                                cbox.DropDownStyle = ComboBoxStyle.DropDownList;
                                cbox.Items.Add(this.GetResourceTextByKey("Key_RX_AllWithHyphens"));
                                cbox.SelectedIndex = 0;
                            }

                            //(EP4 Change)
                            if (string.Compare(oCtl.Name, Constants.InventoryLevel, true) == 0)
                            {
                                ComboBox cbox = oCtl as ComboBox;
                                cbox.DropDownStyle = ComboBoxStyle.DropDownList;
                                cbox.Items.Add(this.GetResourceTextByKey("Key_RX_AllWithHyphens"));

                                for (int i = 1; i <= 100; i++)
                                {
                                    cbox.Items.Add(i);
                                }
                                cbox.SelectedIndex = 0;
                            }

                            if (string.Compare(oCtl.Name, Constants.VaultStatus, true) == 0)
                            {
                                ComboBox cbox = oCtl as ComboBox;
                                cbox.DropDownStyle = ComboBoxStyle.DropDownList;
                                List<ComboBoxItem> values = new List<ComboBoxItem>
                                {
                                    new ComboBoxItem { Text=this.GetResourceTextByKey("Key_RX_AllWithHyphens"), Value="--All--"},
                                    new ComboBoxItem { Value="Active" },
                                    new ComboBoxItem { Value="Assigned To Site" },
                                    new ComboBoxItem { Value="InActive" },
                                    new ComboBoxItem { Value="Sold" },
                                    new ComboBoxItem { Value="Terminated" }
                                };
                                
                                values.ForEach(x => x.Text = string.IsNullOrEmpty(x.Text) ? this.GetResourceTextByKey("Key_ReportFilter_VaultStatus_"+ Regex.Replace(x.Value.ToString(),Constants.SpecialCharacterPattern,string.Empty)) : x.Text);
                                cbox.DataSource = values;
                                cbox.DisplayMember = "Text";
                                cbox.ValueMember = "Value";
                                //cbox.Items.Add(this.GetResourceTextByKey("Key_RX_AllWithHyphens"));     //cbox.Items.Add("--All--");
                                //cbox.Items.Add("Active");                   //cbox.Items.Add(this.GetResourceTextByKey("Key_RX_Active"));             
                                //cbox.Items.Add("Assigned To Site");         //cbox.Items.Add(this.GetResourceTextByKey("Key_RX_AssignedToSite"));
                                //cbox.Items.Add("InActive");                 //cbox.Items.Add(this.GetResourceTextByKey("Key_RX_InActive"));
                                //cbox.Items.Add("Sold");                     //cbox.Items.Add(this.GetResourceTextByKey("Key_RX_Sold"));
                                //cbox.Items.Add("Terminated");               //cbox.Items.Add(this.GetResourceTextByKey("Key_RX_Terminated"));
                                cbox.SelectedIndex = 0;
                            }

                            if (string.Compare(oCtl.Name, Constants.TransactionType, true) == 0)
                            {
                                ComboBox cbox = oCtl as ComboBox;
                                cbox.DropDownStyle = ComboBoxStyle.DropDownList;
                                List<ComboBoxItem> values = new List<ComboBoxItem>
                                {
                                    new ComboBoxItem { Text=this.GetResourceTextByKey("Key_RX_AllWithHyphens"), Value="--All--"},
                                    new ComboBoxItem { Value="Event" },
                                    new ComboBoxItem { Value="Handpay" },
                                    new ComboBoxItem { Value="Jackpot" },
                                    new ComboBoxItem { Value="Mystery" },
                                    new ComboBoxItem { Value="Progressive" },
                                    new ComboBoxItem { Value="Voucher" }
                                };

                                values.ForEach(x => x.Text = string.IsNullOrEmpty(x.Text) ? this.GetResourceTextByKey("Key_ReportFilter_TransactionType_" + Regex.Replace(x.Value.ToString(), Constants.SpecialCharacterPattern, string.Empty)) : x.Text);
                                cbox.DataSource = values;
                                cbox.DisplayMember = "Text";
                                cbox.ValueMember = "Value";

                                //cbox.Items.Add(this.GetResourceTextByKey("Key_RX_AllWithHyphens")); //cbox.Items.Add("--All--");
                                //cbox.Items.Add("Event");        //cbox.Items.Add(this.GetResourceTextByKey("Key_RX_Event")); 
                                //cbox.Items.Add("Handpay");      //cbox.Items.Add(this.GetResourceTextByKey("Key_RX_Handpay")); 
                                //cbox.Items.Add("Jackpot");      //cbox.Items.Add(this.GetResourceTextByKey("Key_RX_Jackpot")); 
                                //cbox.Items.Add("Mystery");      //cbox.Items.Add(this.GetResourceTextByKey("Key_RX_Mystery")); 
                                //cbox.Items.Add("Progressive");  //cbox.Items.Add(this.GetResourceTextByKey("Key_RX_Progessive")); 
                                //cbox.Items.Add("Voucher");      //cbox.Items.Add(this.GetResourceTextByKey("Key_RX_Voucher")); 
                                cbox.SelectedIndex = 0;
                            }

                            if (string.Compare(oCtl.Name, Constants.InventoryType, true) == 0)
                            {
                                ComboBox cbox = oCtl as ComboBox;
                                cbox.DropDownStyle = ComboBoxStyle.DropDownList;

                                List<ComboBoxItem> values = new List<ComboBoxItem>
                                {
                                    new ComboBoxItem { Text=this.GetResourceTextByKey("Key_RX_AllWithHyphens"), Value="--All--"},
                                    new ComboBoxItem { Value="BMC" },
                                    new ComboBoxItem { Value="Vault" }
                                };

                                values.ForEach(x => x.Text = string.IsNullOrEmpty(x.Text) ? this.GetResourceTextByKey("Key_ReportFilter_InventoryType_" + Regex.Replace(x.Value.ToString(), Constants.SpecialCharacterPattern, string.Empty)) : x.Text);
                                cbox.DataSource = values;
                                cbox.DisplayMember = "Text";
                                cbox.ValueMember = "Value";
                                //cbox.Items.Add(this.GetResourceTextByKey("Key_RX_AllWithHyphens"));//cbox.Items.Add("--All--");
                                //cbox.Items.Add("BMC");      //cbox.Items.Add(this.GetResourceTextByKey("Key_RX_BMC"));
                                //cbox.Items.Add("Vault");    //cbox.Items.Add(this.GetResourceTextByKey("Key_RX_Vault"));
                                cbox.SelectedIndex = 0;
                            }
                            //(EP4 Change Ends)
                            ((ComboBox)oCtl).SelectedIndexChanged += new EventHandler(ReportsMain_SelectedIndexChanged);
                            break;
                        }
                    case DbType.DateTime:
                        {
                            oCtl = new DateTimePicker();

                            oCtl.Width = Width;
                            oCtl.Height = Height;
                            Point p = new Point(Left, Top);
                            oCtl.Left = p.X;
                            oCtl.Top = p.Y;
                            oCtl.Name = paramName.Replace("@", "").ToUpper();
                            if (oCtl.Name.ToUpper() == Constants.StartDate || oCtl.Name.ToUpper() == Constants.GamingDate)
                                ((DateTimePicker)oCtl).Value = DateTime.Now.AddDays(-1);
                            else
                                ((DateTimePicker)oCtl).Value = DateTime.Now;

                            ((DateTimePicker)oCtl).ValueChanged += new EventHandler(ReportsMain_ValueChanged);
                            break;
                        }

                    case DbType.DateTime2:
                        {
                            oCtl = new DateTimePicker();
                            ((DateTimePicker)oCtl).Format = DateTimePickerFormat.Custom;
                            ((DateTimePicker)oCtl).CustomFormat = "dd MMM yyyy HH:mm:ss";
                            oCtl.Width = Width;
                            oCtl.Height = Height;
                            Point p = new Point(Left, Top);
                            oCtl.Left = p.X;
                            oCtl.Top = p.Y;
                            oCtl.Name = paramName.Replace("@", "").ToUpper();
                            if (oCtl.Name.ToUpper() == Constants.StartDate || oCtl.Name.ToUpper() == Constants.GamingDate)
                            {
                                int LastDay = 0;
                                LastDay = DateTime.Now.Day - 1;
                                ((DateTimePicker)oCtl).Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, LastDay, 00, 00, 01);
                            }
                            else
                                ((DateTimePicker)oCtl).Value = DateTime.Now;

                            ((DateTimePicker)oCtl).ValueChanged += new EventHandler(ReportsMain_ValueChanged);
                            break;
                        }
                    case DbType.Object:
                        {
                            oCtl = new GroupBox();
                            oCtl.Width = Width;
                            oCtl.Height = Height;
                            Point p = new Point(Left, Top);
                            oCtl.Left = p.X;
                            oCtl.Top = p.Y;
                            oCtl.Name = paramName.Replace("@", "").ToUpper();
                            break;
                        }
                    case DbType.AnsiStringFixedLength:
                        {
                            oCtl = new Label();
                            oCtl.Height = Height;
                            ((Label)oCtl).AutoSize = true;
                            Point p = new Point(Left, Top);
                            if (paramName.EndsWith("Report"))
                            {
                                oCtl.Text = this.GetResourceTextByKey("Key_DBV_" + Regex.Replace(paramName.ToString().Trim(), "[^0-9a-zA-Z]+", ""));
                            }
                            else
                            {
                                oCtl.Text = paramName.Replace("@", "").ToString().ToUpper();
                            }
                            if (paramName == "@UserID")
                            {
                                oCtl.Text = paramName.Replace("@UserID", "USER NAME").ToString().ToUpper();
                            }
                            oCtl.Name = paramName.Replace("@", "").ToString().ToUpper();
                            oCtl.RightToLeft = RightToLeft.No;
                            oCtl.Left = p.X;
                            oCtl.Top = p.Y;

                            break;
                        }
                    case DbType.Boolean:
                        {
                            oCtl = new CheckBox();
                            if (paramName.Replace("@", "").ToString().ToUpper() == Constants.HideZeroVarianceCollections)
                                ((CheckBox)oCtl).Text = Constants.HideZeroVarianceCollectionslbl;
                            else
                                ((CheckBox)oCtl).Text = paramName.Replace("@", "").ToString().ToUpper();
                            oCtl.Name = paramName.Replace("@", "").ToString().ToUpper();
                            oCtl.Width = Width + paramName.Length; ;
                            oCtl.Height = Height;
                            if (oCtl.Text == "GROUPBYZONE")
                                ((CheckBox)oCtl).Checked = isGroupByZoneEnabled;
                            Point p = new Point(Left, Top);

                            oCtl.RightToLeft = RightToLeft.No;
                            oCtl.Left = p.X;
                            oCtl.Top = p.Y;
                            break;
                        }
                    case DbType.String:
                        {
                            oCtl = new CheckedListBox();
                            ((CheckedListBox)oCtl).Text = paramName.Replace("@", "").ToString().ToUpper();
                            oCtl.Name = paramName.Replace("@", "").ToString().ToUpper();
                            oCtl.Width = Width;
                            Point p = new Point(Left, Top);
                            oCtl.RightToLeft = RightToLeft.No;
                            oCtl.Left = p.X;
                            oCtl.Top = p.Y;
                            ((CheckedListBox)oCtl).AllowDrop = true;
                            ((CheckedListBox)oCtl).Sorted = true;
                            ((CheckedListBox)oCtl).CheckOnClick = true;
                            ((CheckedListBox)oCtl).Parent = this;
                            ((CheckedListBox)oCtl).Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
                            ((CheckedListBox)oCtl).BorderStyle = BorderStyle.FixedSingle;
                            ((CheckedListBox)oCtl).ScrollAlwaysVisible = true;
                            ((CheckedListBox)oCtl).ThreeDCheckBoxes = true;
                            ((CheckedListBox)oCtl).CheckOnClick = true;
                            ((CheckedListBox)oCtl).ItemCheck += new ItemCheckEventHandler(ReportsMain_Clb_ItemCheck);


                            break;
                        }
                    default: { break; }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            finally
            {
                try
                {
                    if (!string.IsNullOrWhiteSpace(resolvedText))
                    {
                        oCtl.Text = resolvedText;
                    }
                }
                catch { }
            }
            return oCtl;
        }

        private Control CreateRadioControl(int Left, int Top, int Width, int Height, string paramName)
        {

            RadioButton oCtl = new RadioButton();

            oCtl.Text = paramName.Replace("@", "").ToString().ToUpper();
            oCtl.Name = paramName.Replace("@", "").ToString().ToUpper();
            oCtl.Width = Width + paramName.Length;
            oCtl.Height = Height;
            Point p = new Point(Left, Top);
            oCtl.RightToLeft = RightToLeft.No;
            oCtl.Left = p.X;
            oCtl.Top = p.Y;
            oCtl.Checked = oCtl.Text.Equals(Constants.BMC);
            return oCtl;

        }

        private void ReportsMain_Clb_ItemCheck(object sender, ItemCheckEventArgs e)
        {

        }

        private void ReportsMain_ValueChanged(object sender, EventArgs e)
        {
            DateTimePicker dt = (DateTimePicker)sender;
            switch (dt.Name)
            {
                case Constants.StartDate:
                    {
                        Control[] dtEnd = oCriteria.Controls.Find(Constants.EndDate, true);
                        if (dtEnd.Length > 0)
                        {
                            DateTimePicker child = dtEnd[0] as DateTimePicker;
                            if (dt.Value > child.Value)
                            {
                                dt.MaxDate = child.Value;

                            }
                            else if (dt.Value > DateTime.Now)
                            {
                                dt.MaxDate = DateTime.Now;
                            }
                            else
                            {
                                dt.MaxDate = child.Value;
                                child.MinDate = dt.Value;
                            }
                        }
                        break;
                    }
                case Constants.EndDate:
                    {
                        Control[] dtStart = oCriteria.Controls.Find(Constants.StartDate, true);
                        if (dtStart.Length > 0)
                        {
                            DateTimePicker child = dtStart[0] as DateTimePicker;
                            if (dt.Value < child.Value)
                            {
                                dt.MinDate = child.Value;
                            }
                            else if (dt.Value > DateTime.Now)
                            {
                                dt.MaxDate = DateTime.Now;
                            }
                            else
                            {
                                dt.MinDate = child.Value;
                                child.MaxDate = dt.Value;
                            }
                        }
                        break;
                    }
                case Constants.GamingDate:
                case Constants.IssueDate:
                case Constants.ReportDate:
                    {
                        if (dt.Value > DateTime.Now)
                            dt.MaxDate = DateTime.Now;
                        break;
                    }
                default:
                    {

                        break;
                    }
            }


        }

        private void ReportsMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ComboBox ctrl = sender as ComboBox;
                ctrl.DropDownStyle = ComboBoxStyle.DropDownList;
                switch (ctrl.Name)
                {
                    case Constants.Company:
                        {
                            if (Constants.ExpiredCalendars.Contains(reportArg))
                                return;
                            companyID = ((Company)(ctrl.SelectedItem)).Company_ID;
                            Control[] cmb = oCriteria.Controls.Find(Constants.Subcompany, true);
                            List<SubCompany> oSubCompany;
                            if (cmb.Length > 0)
                            {
                                ComboBox child = cmb[0] as ComboBox;
                                oSubCompany = oReportBusiness.GetSubCompany(companyID, iSecurityUserID);
                                FillCombo(child, oSubCompany, Constants.Subcompany, Constants.cmbSubCmpValue, Constants.cmbSubCmpText, companyID != 0);
                            }

                            Control[] cmbMachineStockStatus = oCriteria.Controls.Find(Constants.StockStatus, true);
                            if (cmbMachineStockStatus.Length > 0)
                            {
                                DataTable ds = new DataTable("MachineStatus");
                                ds.Columns.Add("Status");
                                ds.Columns.Add("Value");
                                ds.Columns.Add("Display");
                                DataRow dr = ds.NewRow();
                                dr["Status"] = "All";
                                dr["Value"] = "-1";
                                dr["Display"] = this.GetResourceTextByKey("Key_All");
                                ds.Rows.Add(dr);
                                string[] str = ConfigurationManager.AppSettings["AssetDetails_MachineStatus"].ToString().Split(';');
                                foreach (string s in str)
                                {
                                    dr = ds.NewRow();
                                    string[] stritem = s.Split(',');
                                    dr["Status"] = stritem[1];
                                    dr["Value"] = stritem[0];
                                    dr["Display"] = this.GetResourceTextByKey("Key_" + stritem[1].Replace(" ",""));
                                    ds.Rows.Add(dr);
                                }
                                ComboBox child = cmbMachineStockStatus[0] as ComboBox;
                                child.DataSource = ds;
                                child.DisplayMember = "Display";
                                child.ValueMember = "Value";
                            }


                            Control[] cmbCard = oCriteria.Controls.Find(Constants.CardNumber, true);
                            List<CardNumberList> oCardNumberList;
                            if (cmbCard.Length > 0)
                            {
                                ComboBox child = cmbCard[0] as ComboBox;
                                oCardNumberList = oReportBusiness.GetCardNumberList();
                                FillCombo(child, oCardNumberList, Constants.CardNumber, Constants.cmbCardNumberValue, Constants.cmbCardNumberText);
                            }

                            Control[] cmbCardType = oCriteria.Controls.Find(Constants.CardType, true);
                            List<CardTypes> oCardTypesList;
                            if (cmbCardType.Length > 0)
                            {
                                ComboBox child = cmbCardType[0] as ComboBox;
                                oCardTypesList = oReportBusiness.GetCardTypes();
                                oCardTypesList.ForEach(x => x.EmpCardDisplay = this.GetResourceTextByKey("Key_ReportFilter_CardType_" + 
                                    Regex.Replace(x.EmpCardType,"[^a-zA-z]+",string.Empty)));
                                FillCombo(child, oCardTypesList, Constants.CardType, Constants.cmbCardTypeValue, Constants.cmbCardTypeText);
                            }


                            if (Constants.SiteAddress.Contains(reportArg))
                            {
                                Control[] cmbSupp = oCriteria.Controls.Find(Constants.Supplier, true);
                                if (cmbSupp.Length > 0)
                                {
                                    ComboBox child = cmbSupp[0] as ComboBox;
                                    List<Operator> oOperator;
                                    oOperator = oReportBusiness.GetSuppliers();
                                    FillCombo(child, oOperator, Constants.Supplier, Constants.cmbSupplierValue, Constants.cmbSupplierText);
                                }

                                Control[] cmbOrderBy = oCriteria.Controls.Find(Constants.OrderBy, true);
                                if (cmbOrderBy.Length > 0)
                                {
                                    ComboBox child = cmbOrderBy[0] as ComboBox;
                                    List<OrderBy> oOrderBy = oReportBusiness.GetOrderBy();
                                    //text externalization
                                    oOrderBy.ForEach(x => x.Display_Name = this.GetResourceTextByKey("Key_ReportFilter_OrderBy_" + Regex.Replace(x.OrderBy_Name,Constants.SpecialCharacterPattern, string.Empty)));
                                    List<OrderBy> sortedList = oOrderBy.OrderBy(i => i.OrderBy_Name).ToList<OrderBy>();

                                    FillCombo(child, sortedList, Constants.OrderBy, Constants.cmbOrderByValue, Constants.cmbOrderByText);
                                }
                            }
                            else if (Constants.Liquidation.Contains(reportArg))
                            {
                                Control[] cmbBatch = oCriteria.Controls.Find(Constants.Liquid, true);
                                if (cmbBatch.Length > 0)
                                {
                                    ComboBox child = cmbBatch[0] as ComboBox;
                                    List<BatchDetails> oBatch;
                                    oBatch = (List<BatchDetails>)oReportBusiness.GetBatchDetails("0");
                                    FillCombo(child, oBatch, Constants.Liquid, Constants.BatchID, Constants.BatchRef);
                                };
                            }
                            else
                            {
                                Control[] cmbCat = oCriteria.Controls.Find(Constants.Category, true);
                                if (cmbCat.Length > 0)
                                {
                                    ComboBox child = cmbCat[0] as ComboBox;
                                    List<Machine_Type> oCategory;
                                    oCategory = oReportBusiness.GetCategory();
                                    FillCombo(child, oCategory, Constants.Category, Constants.cmbCategoryValue, Constants.cmbCategoryText);
                                }
                            }
                            break;
                        }
                    case Constants.Subcompany:
                        {
                            SubCompanyID = ((SubCompany)(ctrl.SelectedItem)).Sub_Company_ID;

                            Control[] cmb = oCriteria.Controls.Find(Constants.Region, true);
                            if (cmb.Length > 0)
                            {
                                List<SubCompanyRegion> oSubCompanyRegion;
                                ComboBox child = cmb[0] as ComboBox;
                                if (((SubCompany)(ctrl.SelectedItem)).Sub_Company_Name == "--None--")
                                {
                                    oSubCompanyRegion = oReportBusiness.GetRegion(-1, -1);
                                    FillCombo(child, oSubCompanyRegion, Constants.Region, Constants.cmbRegionValue, Constants.cmbRegionText, SubCompanyID != 0);

                                }
                                else
                                {
                                    oSubCompanyRegion = oReportBusiness.GetRegion(((SubCompany)(ctrl.SelectedItem)).Sub_Company_ID, companyID);
                                    FillCombo(child, oSubCompanyRegion, Constants.Region, Constants.cmbRegionValue, Constants.cmbRegionText, SubCompanyID != 0);
                                }

                            }
                            Control[] cmbSite = oCriteria.Controls.Find(Constants.Site, true);
                            if (cmbSite.Length > 0)
                            {
                                List<Site> oSite;
                                ComboBox child = cmbSite[0] as ComboBox;
                                oSite = oReportBusiness.GetSites(companyID, ((SubCompany)(ctrl.SelectedItem)).Sub_Company_ID, 0, 0, Program.UserID);
                                if (!Constants.Liquidation.Contains(reportArg))
                                    FillCombo(child, oSite, Constants.Site, Constants.cmbSiteValue, Constants.cmbSiteText, SubCompanyID != 0);
                                else
                                    FillCombo(child, oSite, Constants.Site, Constants.cmbSite1Value, Constants.cmbSiteText, SubCompanyID != 0);
                            }

                            if (Constants.SiteAddress.Contains(reportArg))
                            {
                                Control[] cmbSupp = oCriteria.Controls.Find(Constants.Supplier, true);
                                if (cmbSupp.Length > 0)
                                {
                                    ComboBox child = cmbSupp[0] as ComboBox;
                                    List<Operator> oOperator;
                                    oOperator = oReportBusiness.GetSuppliers();
                                    FillCombo(child, oOperator, Constants.Supplier, Constants.cmbSupplierValue, Constants.cmbSupplierText);
                                }

                                Control[] cmbOrderBy = oCriteria.Controls.Find(Constants.OrderBy, true);
                                if (cmbOrderBy.Length > 0)
                                {
                                    ComboBox child = cmbOrderBy[0] as ComboBox;
                                    List<OrderBy> oOrderBy;
                                    oOrderBy = oReportBusiness.GetOrderBy();
                                    //text externalization
                                    oOrderBy.ForEach(x => x.Display_Name = this.GetResourceTextByKey("Key_ReportFilter_OrderBy_" + Regex.Replace(x.OrderBy_Name,Constants.SpecialCharacterPattern, string.Empty)));
                                    List<OrderBy> sortedList = oOrderBy.OrderBy(i => i.Display_Name).ToList<OrderBy>();
                                    FillCombo(child, sortedList, Constants.OrderBy, Constants.cmbOrderByValue, Constants.cmbOrderByText);
                                }
                            }


                            Control[] cmbweekly = oCriteria.Controls.Find(Constants.WeeklyLiquid, true);
                            List<PeriodEnd> oPeriodEnd;
                            if (cmbweekly.Length > 0)
                            {
                                ComboBox child = cmbweekly[0] as ComboBox;
                                oPeriodEnd = oReportBusiness.GetWeeklyLiquidationBatch(companyID, SubCompanyID);
                                FillCombo(child, oPeriodEnd, Constants.StatementNo, Constants.StatementNo, Constants.PeriodEndDetails);
                            }
                            break;
                        }
                    case Constants.Region:
                        {

                            SRegion_id = ((SubCompanyRegion)(ctrl.SelectedItem)).Sub_Company_Region_ID;
                            Control[] cmb = oCriteria.Controls.Find(Constants.Area, true);
                            if (cmb.Length > 0)
                            {
                                List<SubCompanyArea> oSubCompanyArea;
                                ComboBox child = cmb[0] as ComboBox;
                                if (((SubCompanyRegion)(ctrl.SelectedItem)).Sub_Company_Region_Name == this.GetResourceTextByKey("Key_RX_NoneWithHyphens"))
                                {
                                    oSubCompanyArea = oReportBusiness.GetArea(-1, -1, -1);
                                    FillCombo(child, oSubCompanyArea, Constants.Area, Constants.cmbAreaValue, Constants.cmbAreaText, SRegion_id != 0);

                                }
                                else
                                {

                                    oSubCompanyArea = oReportBusiness.GetArea(((SubCompanyRegion)(ctrl.SelectedItem)).Sub_Company_Region_ID, SubCompanyID, companyID);
                                    FillCombo(child, oSubCompanyArea, Constants.Area, Constants.cmbAreaValue, Constants.cmbAreaText, SRegion_id != 0);
                                }


                            }
                            break;
                        }
                    case Constants.Area:
                        {
                            Control[] cmb = oCriteria.Controls.Find(Constants.District, true);
                            if (cmb.Length > 0)
                            {
                                area = ((SubCompanyArea)(ctrl.SelectedItem)).Sub_Company_Area_ID;
                                if (((SubCompanyArea)(ctrl.SelectedItem)).Sub_Company_Area_Name == "--None--")
                                {

                                    List<SubCompanyDistrict> oSubCompanyDistrict;
                                    ComboBox child = cmb[0] as ComboBox;
                                    oSubCompanyDistrict = oReportBusiness.GetDistrict(-1, -1, -1, -1);
                                    FillCombo(child, oSubCompanyDistrict, Constants.District, Constants.cmbDistrictValue, Constants.cmbDistrictText, area != 0);
                                }
                                else
                                {
                                    List<SubCompanyDistrict> oSubCompanyDistrict;
                                    ComboBox child = cmb[0] as ComboBox;//region, area, subcompany_id, companyID
                                    oSubCompanyDistrict = oReportBusiness.GetDistrict(SRegion_id, area, SubCompanyID, companyID);
                                    FillCombo(child, oSubCompanyDistrict, Constants.District, Constants.cmbDistrictValue, Constants.cmbDistrictText, area != 0);
                                }
                            }
                            break;
                        }
                    case Constants.District:
                        {
                            Control[] cmb = oCriteria.Controls.Find(Constants.Site, true);
                            if (cmb.Length > 0)
                            {
                                int district = ((SubCompanyDistrict)(ctrl.SelectedItem)).Sub_Company_District_ID;
                                if (((SubCompanyDistrict)(ctrl.SelectedItem)).Sub_Company_District_Name == this.GetResourceTextByKey("Key_RX_NoneWithHyphens"))
                                {
                                    List<Site> ositeList;
                                    ComboBox child = cmb[0] as ComboBox;
                                    ositeList = oReportBusiness.GetSites(companyID, SubCompanyID, 0, 0, Program.UserID);
                                    FillCombo(child, ositeList, Constants.Site, Constants.cmbSite1Value, Constants.cmbDistrictText, district != 0);
                                }
                                else
                                {
                                    List<Site> ositeList;
                                    ComboBox child = cmb[0] as ComboBox;
                                    ositeList = oReportBusiness.GetSiteDetailsForDistict(companyID, SubCompanyID, SRegion_id, area, ((SubCompanyDistrict)(ctrl.SelectedItem)).Sub_Company_District_ID, Program.UserID);
                                    FillCombo(child, ositeList, Constants.Site, Constants.cmbSite1Value, Constants.cmbDistrictText, (district != 0 || (district == 0 && area == 0 && SRegion_id == 0) ? true : false));
                                }
                            }
                            break;
                        }
                    case Constants.SiteID:
                    case Constants.Site:
                        {
                            Control[] cmb = oCriteria.Controls.Find(Constants.Zone, true);
                            _SiteID = ((Site)(ctrl.SelectedItem)).Site_ID;
                            if (cmb.Length > 0)
                            {
                                List<Zone> oZone;
                                ComboBox child = cmb[0] as ComboBox;
                                Control[] cmbSubcompany = oCriteria.Controls.Find(Constants.Subcompany, true);
                                Control[] cmbCompany = oCriteria.Controls.Find(Constants.Company, true);
                                oZone = oReportBusiness.GetZones(((Site)(ctrl.SelectedItem)).Site_ID, ((SubCompany)(cmbSubcompany[0] as ComboBox).SelectedItem).Sub_Company_ID, ((Company)(cmbCompany[0] as ComboBox).SelectedItem).Company_ID);
                                FillCombo(child, oZone, Constants.Zone, Constants.cmbZoneValue, Constants.cmbZoneText, _SiteID != 0);
                            }
                            Control[] clb = oCriteria.Controls.Find(Constants.Period, true);
                            if (clb.Length > 0)
                            {
                                List<Period> oPeriod;

                                CheckedListBox child = clb[0] as CheckedListBox;
                                if (child.Items.Count <= 0)
                                {
                                    if (oCriteria.ReportObject.ReportArgName.ToUpper() == "MGMDBYGAMINGDEVICECABINETREPORT"
                                                || oCriteria.ReportObject.ReportArgName.ToUpper() == "MGMDSUMMARYANALYSIS"
                                                || oCriteria.ReportObject.ReportArgName.ToUpper() == "MANUFACTURERPERFORMANCEREPORT")
                                    {
                                        oPeriod = oReportBusiness.GetPeriod(true);
                                        oPeriod.ForEach(x => x.DisplayPeriodName = this.GetResourceTextByKey("Key_ReportFilter_Period_" + Regex.Replace(x.DisplayPeriodName, "[^a-zA-Z]+", string.Empty)));
                                    }
                                    else if (oCriteria.ReportObject.ReportArgName.ToUpper() == "WINCOMPARISONREPORT")
                                    {
                                        oPeriod = oReportBusiness.GetPeriod(3);
                                        oPeriod.ForEach(x => x.DisplayPeriodName = this.GetResourceTextByKey("Key_ReportFilter_Period_" + Regex.Replace(x.DisplayPeriodName, "[^a-zA-Z]+", string.Empty)));
                                    }
                                    else
                                    {
                                        oPeriod = oReportBusiness.GetPeriod();
                                        oPeriod.ForEach(x => x.DisplayPeriodName = this.GetResourceTextByKey("Key_ReportFilter_Period_" + Regex.Replace(x.DisplayPeriodName, "[^a-zA-Z]+", string.Empty)));
                                        if (reportArg == "BMC_DAILYEFTFUNDREVENUE")
                                        {
                                            oPeriod.RemoveAll(
                                                w =>

                                                (w.Period_Id == 2) || (w.Period_Id == 5)


                                            );
                                            var count = 0;
                                            oPeriod = oPeriod.Select(
                                                  w =>
                                                  new Period() { Period_Id = count++, PeriodName = w.PeriodName, DisplayPeriodName = w.DisplayPeriodName }
                                                  ).ToList();
                                        }

                                    }

                                    foreach (Period item in oPeriod)
                                    {

                                        child.Items.Insert(int.Parse(item.Period_Id.ToString()), item.DisplayPeriodName);
                                    }
                                    child.Height = child.Height + 25;
                                }
                            }

                            Control[] cmbSlots = oCriteria.Controls.Find("Slot", true);
                            if (cmbSlots.Length > 0)
                            {
                                ComboBox cmbComp = (ComboBox)oCriteria.Controls.Find(Constants.Company, true)[0];
                                ComboBox cmbSubComp = (ComboBox)oCriteria.Controls.Find(Constants.Subcompany, true)[0];
                                ComboBox myCombo = cmbSlots[0] as ComboBox;
                                List<Slot> mySlots = oReportBusiness.GetSlots(((Site)(ctrl.SelectedItem)).Site_ID, ((Company)(cmbComp.SelectedItem)).Company_ID, ((SubCompany)(cmbSubComp.SelectedItem)).Sub_Company_ID);
                                FillCombo(myCombo, mySlots, "Slot", "Machine_Stock_No", "Machine_Stock_No");

                            }

                            Control[] cmbLiquid = oCriteria.Controls.Find(Constants.Liquid, true);
                            if (cmbLiquid.Length > 0)
                            {
                                List<BatchDetails> oBatch;
                                ComboBox child = cmbLiquid[0] as ComboBox;
                                oBatch = oReportBusiness.GetBatchDetails(((Site)(ctrl.SelectedItem)).Site_Code);
                                FillCombo(child, oBatch, Constants.Liquid, Constants.BatchID, Constants.BatchRef);
                            }

                            Control[] cmbEmpID = oCriteria.Controls.Find("USERID", true);
                            if (cmbEmpID.Length > 0)
                            {
                                ComboBox parentctl = cmbEmpID[0] as ComboBox;
                                List<EmployeeID> ObEmpID;
                                ObEmpID = oReportBusiness.GetEmployeeID(((Site)(ctrl.SelectedItem)).Site_ID);
                                FillCombo(parentctl, ObEmpID, Constants.EmpID, Constants.cmbEmpIdValue, Constants.cmbEmpIdText);
                            }

                            Control[] cmbEmpCardID = oCriteria.Controls.Find("EMPCARDID", true);
                            if (cmbEmpCardID.Length > 0)
                            {
                                ComboBox parentctl = cmbEmpCardID[0] as ComboBox;
                                List<EmployeeCardID> obEmpCardID;
                                obEmpCardID = oReportBusiness.GetEmployeeCardID(Convert.ToInt32(((Site)(ctrl.SelectedItem)).Site_ID));
                                FillCombo(parentctl, obEmpCardID, Constants.EmpCardID, Constants.cmbEmpCardIdValue, Constants.cmbEmpCardIdText);
                            }

                            break;
                        }
                    case Constants.Supplier:
                        {
                            Control[] cmb = oCriteria.Controls.Find(Constants.Depot, true);
                            if (cmb.Length > 0)
                            {
                                List<Depot> oDepot;
                                ComboBox child = cmb[0] as ComboBox;
                                oDepot = oReportBusiness.GetDepot(((Operator)(ctrl.SelectedItem)).Operator_ID);
                                FillCombo(child, oDepot, Constants.Depot, Constants.cmbDepotValue, Constants.cmbDepotText);
                            }
                            break;
                        }
                    case Constants.OrderBy:
                        {
                            break;
                        }
                    case Constants.Period:
                        {
                            break;
                        }
                    case Constants.Category:
                        {
                            break;
                        }
                    case Constants.Zone:
                        {
                            break;
                        }
                    case Constants.EmpID:
                        {
                            Control[] cmb = oCriteria.Controls.Find(Constants.EmpCardID, true);
                            if (cmb.Length > 0)
                            {
                                List<EmployeeCardID> oEmployeeCardID;
                                ComboBox child = cmb[0] as ComboBox;
                                if (((EmployeeID)(ctrl.SelectedItem)).UserName == this.GetResourceTextByKey("Key_RX_AllWithHyphens"))
                                {
                                    oEmployeeCardID = oReportBusiness.GetEmployeeCardID(_SiteID, 0);
                                }


                                else
                                {
                                    oEmployeeCardID = oReportBusiness.GetEmployeeCardID(_SiteID,
                                            Convert.ToInt32(((EmployeeID)(ctrl.SelectedItem)).SecurityUserID));
                                }

                                FillCombo(child, oEmployeeCardID, Constants.EmpCardID, Constants.cmbEmpCardIdValue, Constants.cmbEmpCardIdText);
                            }
                            break;
                        }
                    case Constants.CardNumber:
                        {
                            Control[] cmb = oCriteria.Controls.Find(Constants.EmployeeName, true);
                            if (cmb.Length > 0)
                            {
                                List<EmployeeNameList> oEmployeeName;
                                ComboBox child = cmb[0] as ComboBox;

                                Control[] cmb1 = oCriteria.Controls.Find(Constants.CardStatus, true);
                                ComboBox cmbCardStatus = cmb1[0] as ComboBox;

                                Control[] cmb2 = oCriteria.Controls.Find(Constants.CardType, true);
                                ComboBox cmbCardType = cmb2[0] as ComboBox;

                                Control[] cmb3 = oCriteria.Controls.Find(Constants.EmployeeName, true);
                                ComboBox cmbEmployeeName = cmb3[0] as ComboBox;

                                if (((CardNumberList)(ctrl.SelectedItem)).EmployeeCardNumber == this.GetResourceTextByKey("Key_RX_AllWithHyphens"))
                                {
                                    oEmployeeName = oReportBusiness.GetEmployeeName(Convert.ToInt32(0));
                                    cmbCardStatus.Enabled = true;
                                    cmbCardType.Enabled = true;
                                    cmbEmployeeName.Enabled = true;
                                }
                                else
                                {
                                    oEmployeeName = oReportBusiness.GetEmployeeName(Convert.ToInt32(((CardNumberList)(ctrl.SelectedItem)).EmployeeCardNumber));
                                    cmbCardStatus.Enabled = false;
                                    cmbCardStatus.SelectedIndex = 0;
                                    cmbCardType.Enabled = false;
                                    cmbCardType.SelectedIndex = 0;
                                    cmbEmployeeName.Enabled = false;
                                }
                                FillCombo(child, oEmployeeName, Constants.EmployeeName, Constants.cmbEmployeeNameValue, Constants.cmbEmployeeNameText);
                            }
                            break;
                        }
                    default:
                        break;
                }
                if (reportArg == "EmployeeCardListReport" || reportArg == "AssetDetails")
                {
                    Control[] cmbComp = oCriteria.Controls.Find(Constants.Company, true);
                    ComboBox cmbCompany = cmbComp[0] as ComboBox;
                    cmbCompany.Enabled = false;
                }

            }
            catch (Exception ex)
            { ExceptionManager.Publish(ex); }

        }

        private void FillCombo<T>(ComboBox cb, List<T> olist, string type, string strComboValue, string strComboText)
        {
            FillCombo(cb, olist, type, strComboValue, strComboText, true);
        }

        private void FillCombo<T>(ComboBox cb, List<T> olist, string type, string strComboValue, string strComboText, bool isParentSelected)
        {
            try
            {
                if (olist.Count > 0)
                {
                    if (isHrchyFilterInReports && !isParentSelected) olist.Clear();
                    switch (type.ToUpper())
                    {
                        case Constants.Company:
                            {
                                List<Company> newlist = (List<Company>)(object)olist;
                                Company cmp = new Company();
                                cb.DataSource = newlist;
                                cb.DisplayMember = strComboText;
                                cb.ValueMember = strComboValue;
                                cb.DropDownStyle = ComboBoxStyle.DropDownList;
                                break;
                            }
                        case Constants.Subcompany:
                            {
                                List<SubCompany> newlist = (List<SubCompany>)(object)olist;
                                SubCompany cmp = new SubCompany();
                                cmp.Sub_Company_ID = 0;
                                cmp.Sub_Company_Name = this.GetResourceTextByKey("Key_RX_AllWithHyphens"); //Constants.ValueAny;
                                newlist.Insert(0, cmp);
                                cb.DataSource = newlist;
                                cb.DisplayMember = strComboText;
                                cb.ValueMember = strComboValue;
                                cb.DropDownStyle = ComboBoxStyle.DropDownList;
                                break;
                            }
                        case Constants.Region:
                            {
                                List<SubCompanyRegion> newlist = (List<SubCompanyRegion>)(object)olist;
                                SubCompanyRegion cmp = new SubCompanyRegion();
                                cmp.Sub_Company_Region_ID = 0;
                                cmp.Sub_Company_Region_Name = this.GetResourceTextByKey("Key_RX_AllWithHyphens"); //Constants.ValueAny;
                                newlist.Insert(0, cmp);
                                cb.DataSource = newlist;
                                cb.DisplayMember = strComboText;
                                cb.ValueMember = strComboValue;
                                break;
                            }
                        case Constants.Area:
                            {


                                List<SubCompanyArea> newlist = (List<SubCompanyArea>)(object)olist;
                                SubCompanyArea cmp = new SubCompanyArea();
                                cmp.Sub_Company_Area_ID = 0;
                                cmp.Sub_Company_Area_Name = this.GetResourceTextByKey("Key_RX_AllWithHyphens"); //Constants.ValueAny;
                                newlist.Insert(0, cmp);
                                cb.DataSource = newlist;
                                cb.DisplayMember = strComboText;
                                cb.ValueMember = strComboValue;
                                break;

                            }
                        case Constants.District:
                            {
                                List<SubCompanyDistrict> newlist = (List<SubCompanyDistrict>)(object)olist;
                                SubCompanyDistrict cmp = new SubCompanyDistrict();
                                cmp.Sub_Company_District_ID = 0;
                                cmp.Sub_Company_District_Name = this.GetResourceTextByKey("Key_RX_AllWithHyphens"); //Constants.ValueAny;
                                newlist.Insert(0, cmp);
                                cb.DataSource = newlist;
                                cb.DisplayMember = strComboText;
                                cb.ValueMember = strComboValue;
                                break;
                            }
                        case Constants.SiteID:
                        case Constants.Site:
                            {
                                List<Site> newlist = (List<Site>)(object)olist;
                                Site cmp = new Site();
                                cmp.Site_ID = 0;
                                cmp.Site_Name = this.GetResourceTextByKey("Key_RX_AllWithHyphens"); //Constants.ValueAny;
                                newlist.Insert(0, cmp);
                                cb.DataSource = newlist;
                                cb.DisplayMember = strComboText;
                                cb.ValueMember = strComboValue;
                                cb.DropDownStyle = ComboBoxStyle.DropDownList;
                                break;
                            }
                        case Constants.Category:
                            {
                                List<Machine_Type> newlist = (List<Machine_Type>)(object)olist;
                                Machine_Type cmp = new Machine_Type();
                                cmp.Machine_Type_ID = 0;
                                cmp.Machine_Type_Code = this.GetResourceTextByKey("Key_RX_AllWithHyphens"); //Constants.ValueAny;
                                newlist.Insert(0, cmp);
                                cb.DataSource = newlist;
                                cb.DisplayMember = strComboText;
                                cb.ValueMember = strComboValue;
                                break;
                            }
                        case Constants.Supplier:
                            {
                                List<Operator> newlist = (List<Operator>)(object)olist;
                                Operator cmp = new Operator();
                                cmp.Operator_ID = 0;
                                cmp.Operator_Name = this.GetResourceTextByKey("Key_RX_AllWithHyphens"); //Constants.ValueAny;
                                newlist.Insert(0, cmp);
                                cb.DataSource = newlist;
                                cb.DisplayMember = strComboText;
                                cb.ValueMember = strComboValue;
                                break;
                            }
                        case Constants.Depot:
                            {
                                List<Depot> newlist = (List<Depot>)(object)olist;
                                Depot cmp = new Depot();
                                cmp.Depot_ID = 0;
                                cmp.Depot_Name = this.GetResourceTextByKey("Key_RX_AllWithHyphens"); //Constants.ValueAny;
                                newlist.Insert(0, cmp);
                                cb.DataSource = newlist;
                                cb.DisplayMember = strComboText;
                                cb.ValueMember = strComboValue;
                                break;
                            }

                        case Constants.OrderBy:
                            {
                                List<OrderBy> newlist = (List<OrderBy>)(object)olist;
                                OrderBy cmp = new OrderBy();
                                cb.DataSource = newlist;
                                cb.DisplayMember = strComboText;
                                cb.ValueMember = strComboValue;
                                break;
                            }
                        case Constants.Zone:
                            {
                                List<Zone> newlist = (List<Zone>)(object)olist;
                                Zone cmp = new Zone();
                                cmp.Zone_ID = 0;
                                cmp.Zone_Name = this.GetResourceTextByKey("Key_RX_AllWithHyphens"); //Constants.ValueAny;
                                newlist.Insert(0, cmp);
                                cb.DataSource = newlist;
                                cb.DisplayMember = strComboText;
                                cb.ValueMember = strComboValue;
                                break;
                            }
                        case "SLOT":
                            {
                                List<Slot> localList = (List<Slot>)(object)olist;
                                Slot objSlot = new Slot();
                                objSlot.Machine_Stock_No = this.GetResourceTextByKey("Key_RX_AllWithHyphens");
                                localList.Insert(0, objSlot);
                                cb.DataSource = localList;
                                cb.DisplayMember = strComboText;
                                cb.ValueMember = strComboValue;
                                break;
                            }
                        case "STATEMENTNO":
                            {
                                List<PeriodEnd> localList = (List<PeriodEnd>)(object)olist;
                                PeriodEnd objPeriodEnd = new PeriodEnd();
                                objPeriodEnd.Period_End_ID = 0;
                                objPeriodEnd.Statement_No = 0;
                                objPeriodEnd.Period_End_Description = this.GetResourceTextByKey("Key_RX_AllWithHyphens"); //Constants.ValueAny;
                                localList.Insert(0, objPeriodEnd);
                                cb.DisplayMember = strComboText;
                                cb.ValueMember = "Statement_No";
                                cb.DataSource = localList;
                                break;
                            }
                        case "BATCH_NO":
                            {
                                List<BatchDetails> localList = (List<BatchDetails>)(object)olist;
                                BatchDetails objBatch = new BatchDetails();
                                objBatch.Batch_ID = 0;
                                objBatch.Batch_ref = this.GetResourceTextByKey("Key_RX_AllWithHyphens"); //Constants.ValueAny;
                                localList.Insert(0, objBatch);
                                cb.DataSource = localList;
                                cb.DisplayMember = strComboText;
                                cb.ValueMember = strComboValue;
                                break;
                            }
                        case Constants.EmpID:
                            {
                                List<EmployeeID> newlist = (List<EmployeeID>)(object)olist;
                                EmployeeID empid = new EmployeeID();
                                empid.UserName = this.GetResourceTextByKey("Key_RX_AllWithHyphens");
                                empid.SecurityUserID = 0;
                                newlist.Insert(0, empid);
                                cb.DataSource = newlist;
                                cb.DisplayMember = strComboText.ToString();
                                cb.ValueMember = strComboValue;
                                cb.DropDownStyle = ComboBoxStyle.DropDownList;

                                break;
                            }
                        case Constants.EmpCardID:
                            {
                                List<EmployeeCardID> newlist = (List<EmployeeCardID>)(object)olist;
                                EmployeeCardID empcardid = new EmployeeCardID();
                                empcardid.EmpCardID = this.GetResourceTextByKey("Key_RX_AllWithHyphens");
                                newlist.Insert(0, empcardid);
                                cb.DataSource = newlist;
                                cb.DisplayMember = strComboText.ToString();
                                cb.ValueMember = strComboValue;
                                cb.DropDownStyle = ComboBoxStyle.DropDownList;
                                break;
                            }
                        case Constants.CardNumber:
                            {
                                List<CardNumberList> newlist = (List<CardNumberList>)(object)olist;
                                CardNumberList CardList = new CardNumberList();
                                CardList.EmployeeCardNumber = this.GetResourceTextByKey("Key_RX_AllWithHyphens");
                                newlist.Insert(0, CardList);
                                cb.DataSource = newlist;
                                cb.DisplayMember = strComboText;
                                cb.ValueMember = strComboValue;
                                cb.DropDownStyle = ComboBoxStyle.DropDownList;
                                break;
                            }
                        case Constants.EmployeeName:
                            {
                                List<EmployeeNameList> newlist = (List<EmployeeNameList>)(object)olist;
                                EmployeeNameList EmpName = new EmployeeNameList();
                                EmpName.EmployeeName = this.GetResourceTextByKey("Key_RX_AllWithHyphens");
                                newlist.Insert(0, EmpName);
                                cb.DataSource = newlist;
                                cb.DisplayMember = strComboText;
                                cb.ValueMember = strComboValue;
                                cb.DropDownStyle = ComboBoxStyle.DropDownList;
                                break;
                            }
                        case Constants.CardType:
                            {
                                List<CardTypes> newlist = (List<CardTypes>)(object)olist;
                                CardTypes Card = new CardTypes();
                                Card.EmpCardDisplay = this.GetResourceTextByKey("Key_RX_AllWithHyphens");
                                newlist.Insert(0, Card);
                                cb.DataSource = newlist;
                                cb.DisplayMember = strComboText;
                                cb.ValueMember = strComboValue;
                                cb.DropDownStyle = ComboBoxStyle.DropDownList;
                                break;
                            }
                        case Constants.RuleName:
                            {
                                List<SiteLicensing> newlist = (List<SiteLicensing>)(object)olist;
                                SiteLicensing oRule = new SiteLicensing();
                                oRule.RuleName = this.GetResourceTextByKey("Key_RX_AllWithHyphens");
                                newlist.Insert(0, oRule);
                                cb.DataSource = newlist;
                                cb.DisplayMember = strComboText;
                                cb.ValueMember = strComboValue;
                                cb.DropDownStyle = ComboBoxStyle.DropDownList;
                                break;
                            }
                        default:
                            return;
                    }
                }
                else
                {
                    switch (type)
                    {
                        case Constants.Company:
                            {
                                List<Company> newlist = (List<Company>)(object)olist;
                                Company cmp = new Company();
                                cmp.Company_ID = 0;
                                cmp.Company_Name = this.GetResourceTextByKey("Key_RX_NoneWithHyphens");
                                newlist.Insert(0, cmp);
                                cb.DataSource = newlist;
                                cb.DisplayMember = strComboText;
                                cb.ValueMember = strComboValue;
                                break;
                            }
                        case Constants.Subcompany:
                            {
                                List<SubCompany> newlist = (List<SubCompany>)(object)olist;
                                SubCompany cmp = new SubCompany();
                                cmp.Sub_Company_ID = 0;
                                cmp.Sub_Company_Name = this.GetResourceTextByKey("Key_RX_NoneWithHyphens"); //Constants.ValueNone;
                                newlist.Insert(0, cmp);
                                cb.DataSource = newlist;
                                cb.DisplayMember = strComboText;
                                cb.ValueMember = strComboValue;
                                break;
                            }
                        case Constants.Region:
                            {
                                List<SubCompanyRegion> newlist = (List<SubCompanyRegion>)(object)olist;
                                SubCompanyRegion cmp = new SubCompanyRegion();
                                cmp.Sub_Company_Region_ID = 0;
                                cmp.Sub_Company_Region_Name = this.GetResourceTextByKey("Key_RX_NoneWithHyphens"); //Constants.ValueNone;
                                newlist.Insert(0, cmp);
                                cb.DataSource = newlist;
                                cb.DisplayMember = strComboText;
                                cb.ValueMember = strComboValue;
                                break;
                            }
                        case Constants.Area:
                            {
                                List<SubCompanyArea> newlist = (List<SubCompanyArea>)(object)olist;
                                SubCompanyArea cmp = new SubCompanyArea();
                                cmp.Sub_Company_Area_ID = 0;
                                cmp.Sub_Company_Area_Name = this.GetResourceTextByKey("Key_RX_NoneWithHyphens"); //Constants.ValueNone;
                                newlist.Insert(0, cmp);
                                cb.DataSource = newlist;
                                cb.DisplayMember = strComboText;
                                cb.ValueMember = strComboValue;
                                break;
                            }
                        case Constants.District:
                            {
                                List<SubCompanyDistrict> newlist = (List<SubCompanyDistrict>)(object)olist;
                                SubCompanyDistrict cmp = new SubCompanyDistrict();
                                cmp.Sub_Company_District_ID = 0;
                                cmp.Sub_Company_District_Name = this.GetResourceTextByKey("Key_RX_NoneWithHyphens"); //Constants.ValueNone;
                                newlist.Insert(0, cmp);
                                cb.DataSource = newlist;
                                cb.DisplayMember = strComboText;
                                cb.ValueMember = strComboValue;
                                break;
                            }
                        case Constants.SiteID:
                        case Constants.Site:
                            {
                                List<Site> newlist = (List<Site>)(object)olist;
                                Site cmp = new Site();
                                cmp.Site_ID = 0;
                                cmp.Site_Name = this.GetResourceTextByKey("Key_RX_NoneWithHyphens"); //Constants.ValueNone;
                                newlist.Insert(0, cmp);
                                cb.DataSource = newlist;
                                cb.DisplayMember = strComboText;
                                cb.ValueMember = strComboValue;
                                break;
                            }
                        case Constants.Category:
                            {
                                List<Machine_Type> newlist = (List<Machine_Type>)(object)olist;
                                Machine_Type cmp = new Machine_Type();
                                cmp.Machine_Type_ID = 0;
                                cmp.Machine_Type_Code = this.GetResourceTextByKey("Key_RX_NoneWithHyphens"); //Constants.ValueNone;
                                newlist.Insert(0, cmp);
                                cb.DataSource = newlist;
                                cb.DisplayMember = strComboText;
                                cb.ValueMember = strComboValue;
                                break;
                            }
                        case Constants.Supplier:
                            {
                                List<Operator> newlist = (List<Operator>)(object)olist;
                                Operator cmp = new Operator();
                                cmp.Operator_ID = 0;
                                cmp.Operator_Name = this.GetResourceTextByKey("Key_RX_NoneWithHyphens"); //Constants.ValueNone;
                                newlist.Insert(0, cmp);
                                cb.DataSource = newlist;
                                cb.DisplayMember = strComboText;
                                cb.ValueMember = strComboValue;
                                break;
                            }
                        case Constants.Depot:
                            {
                                List<Depot> newlist = (List<Depot>)(object)olist;
                                Depot cmp = new Depot();
                                cmp.Depot_ID = 0;
                                cmp.Depot_Name = this.GetResourceTextByKey("Key_RX_NoneWithHyphens"); //Constants.ValueNone;
                                newlist.Insert(0, cmp);
                                cb.DataSource = newlist;
                                cb.DisplayMember = strComboText;
                                cb.ValueMember = strComboValue;
                                break;
                            }
                        case Constants.Zone:
                            {
                                List<Zone> newlist = (List<Zone>)(object)olist;
                                Zone cmp = new Zone();
                                cmp.Zone_ID = 0;
                                cmp.Zone_Name = this.GetResourceTextByKey("Key_RX_NoneWithHyphens"); //Constants.ValueNone;
                                newlist.Insert(0, cmp);
                                cb.DataSource = newlist;
                                cb.DisplayMember = strComboText;
                                cb.ValueMember = strComboValue;
                                break;
                            }
                        case "Slot":
                            {
                                List<Slot> localList = (List<Slot>)(object)olist;
                                Slot objSlot = new Slot();
                                objSlot.Machine_Stock_No = this.GetResourceTextByKey("Key_RX_NoneWithHyphens"); //Constants.ValueNone;
                                localList.Insert(0, objSlot);
                                cb.DataSource = localList;
                                cb.DisplayMember = strComboText;
                                cb.ValueMember = strComboValue;
                                break;
                            }

                        case "STATEMENTNO":
                            {
                                List<PeriodEnd> localList = (List<PeriodEnd>)(object)olist;
                                PeriodEnd objPeriodEnd = new PeriodEnd();
                                objPeriodEnd.Period_End_ID = 0;
                                objPeriodEnd.Statement_No = 0;
                                objPeriodEnd.Period_End_Description = this.GetResourceTextByKey("Key_RX_AllWithHyphens"); //Constants.ValueAny;
                                cb.DisplayMember = strComboText;
                                cb.ValueMember = "Statement_No";
                                localList.Insert(0, objPeriodEnd);
                                cb.DataSource = localList;
                                break;
                            }
                        case "BATCHNO":
                            {
                                List<BatchDetails> localList = (List<BatchDetails>)(object)olist;
                                BatchDetails objBatch = new BatchDetails();
                                objBatch.Batch_ID = 0;
                                objBatch.Batch_ref = "";
                                localList.Insert(0, objBatch);
                                cb.DataSource = localList;
                                cb.DisplayMember = strComboText;
                                cb.ValueMember = strComboValue;
                                break;
                            }
                        case Constants.EmpID:
                            {
                                List<EmployeeID> newlist = (List<EmployeeID>)(object)olist;
                                EmployeeID empid = new EmployeeID();
                                empid.UserName = this.GetResourceTextByKey("Key_RX_AllWithHyphens");
                                empid.SecurityUserID = 0;
                                newlist.Insert(0, empid);
                                cb.DataSource = newlist;
                                cb.DisplayMember = strComboText.ToString();
                                cb.ValueMember = strComboValue;
                                cb.DropDownStyle = ComboBoxStyle.DropDownList;
                                break;
                            }
                        case Constants.EmpCardID:
                            {
                                List<EmployeeCardID> newlist = (List<EmployeeCardID>)(object)olist;
                                EmployeeCardID empcardid = new EmployeeCardID();
                                empcardid.EmpCardID = this.GetResourceTextByKey("Key_RX_AllWithHyphens");
                                newlist.Insert(0, empcardid);
                                cb.DataSource = newlist;
                                cb.DisplayMember = strComboText.ToString();
                                cb.ValueMember = strComboValue;
                                cb.DropDownStyle = ComboBoxStyle.DropDownList;
                                break;
                            }
                        case Constants.CardNumber:
                            {
                                List<CardNumberList> newlist = (List<CardNumberList>)(object)olist;
                                CardNumberList CardList = new CardNumberList();
                                CardList.EmployeeCardNumber = this.GetResourceTextByKey("Key_RX_AllWithHyphens");
                                newlist.Insert(0, CardList);
                                cb.DataSource = newlist;
                                cb.DisplayMember = strComboText;
                                cb.ValueMember = strComboValue;
                                cb.DropDownStyle = ComboBoxStyle.DropDownList;
                                break;
                            }
                        case Constants.EmployeeName:
                            {
                                List<EmployeeNameList> newlist = (List<EmployeeNameList>)(object)olist;
                                EmployeeNameList EmpName = new EmployeeNameList();
                                if (newlist.Count == 0)
                                    EmpName.EmployeeName = this.GetResourceTextByKey("Key_RX_NoneWithHyphens");
                                else
                                    EmpName.EmployeeName = this.GetResourceTextByKey("Key_RX_AllWithHyphens");
                                newlist.Insert(0, EmpName);
                                cb.DataSource = newlist;
                                cb.DisplayMember = strComboText;
                                cb.ValueMember = strComboValue;
                                cb.DropDownStyle = ComboBoxStyle.DropDownList;
                                break;
                            }
                        case Constants.RuleName:
                            {
                                List<SiteLicensing> newlist = (List<SiteLicensing>)(object)olist;
                                SiteLicensing oRule = new SiteLicensing();
                                oRule.RuleName = "--All--";
                                newlist.Insert(0, oRule);
                                cb.DataSource = newlist;
                                cb.DisplayMember = strComboText;
                                cb.ValueMember = strComboValue;
                                cb.DropDownStyle = ComboBoxStyle.DropDownList;
                                break;
                            }
                        default:
                            return;
                    }
                }
            }
            catch (Exception ex)
            { ExceptionManager.Publish(ex); }
        }

        private void delCriteriaClosed(object sender, FormClosedEventArgs e)
        {

            oCriteria = null;
        }

        private void SetPanelControls(TreeNode ReportNode, bool ParentNode)
        {
            string ReportName = string.Empty;
            GetAllReportsToRolesAccess objReport = (GetAllReportsToRolesAccess)ReportNode.Tag;
            if (ParentNode)
            {
                ReportName = ReportNode.Text;
            }
            else
            {
                ReportName = objReport.ReportName;
            }

            if (ReportName.ToUpper() == "SGVI LOTTERY REPORTS")
            {
                ReportName = "Lottery Reports";
            }

            Button btnOpen = null;
            Button btnExportToExcel = null;
            try
            {
                lblReportName = (Label)CreateControl(DbType.AnsiStringFixedLength, 25, 25, 500, 25, ReportName);
                FontFamily fm = new FontFamily("Arial");
                lblReportName.AutoSize = true;
                lblReportName.Font = new Font(fm, 14, FontStyle.Bold);
                lblReportName.Text = lblReportName.Text.ToUpper();
                lblReportName.Width = lblReportName.PreferredSize.Width;


                if ((pnlChild.Width / 2 - lblReportName.Width / 2) < 0)
                    lblReportName.Left = (lblReportName.Text.Length >= 30) ? 35 : ((pnlChild.Width / 2 - lblReportName.Width / 2) * -1);//for cross ticketing
                else
                    lblReportName.Left = 100;
                if (ParentNode == false)
                {
                    btnOpen = new Button();
                    btnOpen.Height = 35;
                    btnOpen.Width = 110;
                    btnOpen.Name = Constants.ButtonOpenName;
                    btnOpen.Tag = "Key_OpenReport";
                    btnOpen.Click += new EventHandler(btnOpen_Click);
                }

                //Included Export Button to Export report to Excel
                if ((ParentNode == false) && (oCriteria.ReportObject.ExportExcel == true))
                {
                    btnExportToExcel = new Button();
                    btnExportToExcel.Height = 35;
                    btnExportToExcel.Width = 110;
                    btnExportToExcel.Name = Constants.ButtonExportName;
                    btnExportToExcel.Tag = "Key_ExportExcel";
                    btnExportToExcel.Click += new EventHandler(btnExportToExcel_Click);
                }

                if (oCriteria == null)
                {
                    oCriteria = new Criteria();
                    oCriteria.delCriteriaClosed += new Criteria.CriteriaHandler(delCriteriaClosed);
                    oCriteria.TopLevel = false;
                    oCriteria.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                    oCriteria.TopMost = true;
                    oCriteria.Dock = DockStyle.Fill;
                }
                else
                {
                    oCriteria.OpenButton = btnOpen;
                    oCriteria.ExportButton = btnExportToExcel;
                    if (btnOpen != null)
                    {
                        btnOpen.Left = oCriteria.Width - 50;
                        btnOpen.Top = oCriteria.Height - 50;
                        oCriteria.Controls.Add(btnOpen);
                    }

                    if (btnExportToExcel != null)
                    {
                        btnExportToExcel.Left = oCriteria.Width + 70;
                        btnExportToExcel.Top = oCriteria.Height - 50;
                        oCriteria.Controls.Add(btnExportToExcel);
                    }
                }

                oCriteria.ReportObject = objReport;
                oCriteria.Controls.Add(lblReportName);
                oCriteria.SetButtonsPosition();
                this.pnlChild.Controls.Add(oCriteria);
                oCriteria.Show();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

        }

        private void btnExportToExcel_Click(object sender, EventArgs e)
        {
            try
            {
                ExportToExcel objExportToExcel = new ExportToExcel();
                oCriteria.LoadListparameters(lstParams);
                objExportToExcel.ExportDataToExcel(oCriteria.ReportObject.MS_ProcedureUsed, oCriteria.XMLName, lstParams, oCriteria.dtSet);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        public void LoadReport(GetAllReportsToRolesAccess ObjReport, clsSPParams lstParams, bool IsSDSReports)
        {
            RDLReportViewer.Instance.LoadReport(ObjReport.MS_ProcedureUsed, ObjReport.ReportName, ObjReport.ReportArgName, lstParams, IsSDSReports);
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                if (oCriteria != null)
                {
                    oCriteria.LoadListparameters(lstParams);
                    lstParams.ReportFilterDateFormat = (oCriteria.ReportObject.IsTimeRequired == true) ? ReportDateTimeFormat : ReportDateFormat;
                    LoadReport(oCriteria.ReportObject, lstParams, false);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            try
            {
                if (oCriteria != null)
                    oCriteria.cmdClose_Click(sender, e);
            }
            catch (Exception ex)
            { ExceptionManager.Publish(ex); }
        }

        private void tvRoles_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            try
            {
                if (e.Node.Nodes.Count > 0)
                {
                    this.pnlChild.Controls.Clear();
                    if (oCriteria != null)
                        oCriteria.Controls.Clear();
                    SetPanelControls(e.Node, true);
                    return;
                }
                {
                    SetReportCriteria(e.Node);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            finally
            {
                this.ResolveResources();
            }
        }

        private void tvRoles_AfterCheck(object sender, TreeViewEventArgs e)
        {
            try
            {

                TreeNode myNode = e.Node;

                if (myNode.Nodes.Count > 0)
                {
                    bool IsChecked = e.Node.Checked;

                    foreach (TreeNode item in myNode.Nodes)
                    {
                        item.Checked = IsChecked;
                    }

                    return;
                }
                if (AlteredNodesName.Count == 0 || AlteredNodesName.IndexOf(myNode.Name) < 0)
                {
                    AlteredNodesName.Add(myNode.Name);
                }
            }
            catch (Exception ex)
            { ExceptionManager.Publish(ex); }
        }

        private void ReportRoleAdmin_Load(object sender, EventArgs e)
        {
            try
            {
                ReportDateTimeFormat = oReportBusiness.GetSetting("ReportDateTimeFormat", "dd-MMM-yyyy HH:mm:ss");
                ReportDateFormat = oReportBusiness.GetSetting("ReportDateFormat", "dd-MMM-yyyy");
                lstParams.ReportPrintDateTimeFormat = oReportBusiness.GetSetting("ReportPrintDateTimeFormat", "dd-MMM-yyyy HH:mm:ss");
                lstParams.ReportDataDateAloneFormat = oReportBusiness.GetSetting("ReportDataDateAloneFormat", "dd-MMM-yyyy");
                lstParams.ReportDataDateNTimeFormat = oReportBusiness.GetSetting("ReportDataDateNTimeFormat", "dd-MMM-yyyy HH:mm:ss");

                List<IRole> Role = oReportBusiness.GetRoleByName(RoleName);
                if (Role != null)
                    if (Role.Count > 0)
                        RoleID = Role[0].SecurityRoleID;
                AlteredNodesName = new List<string>();
                AddedNodesName = new List<string>();
                tvRoles.ImageList = imageList1;
                LoadTreeViewForRole(RoleID);
                this.pnlChild.Controls.Clear(); ;
                if (oCriteria != null)
                    oCriteria.Controls.Clear();
                //SetPanelControls(Constants.MainHeader, true);
                iTotalSiteCount = oReportBusiness.GetALLSiteCount();
                //LoadResourceParameters(lstReportParams);
                //LoadCommonParams(lstReportParams);
                this.gpTree.Tag = "Key_ReportsMenu";
                this.ResolveResources();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

    }


}
