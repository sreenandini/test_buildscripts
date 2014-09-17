/// Source File Name	: ucBMCMeterAnalysis.cs
/// Description		    : Meter analysis user controls provides user to view meter data day wise 
///                     for selected criteria and also depicts the data in form of graph
/// Revision History
/// Author             Date              Description
/// ---------------------------------------------------
/// Madhusudhanan      12/5/08          created
/// Renjish N          19/5/08          Added code for Populating Graph.
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Windows.Forms;
using BMC.Business.Classes;
using BMC.Common.ConfigurationManagement;
using BMC.Common.LogManagement;
using BMC.Common.ExceptionManagement;
using System.Configuration;
using System.Collections;
using System.Threading;
using System.Windows.Forms.DataVisualization.Charting;
using BMC.Common;
using System.Diagnostics;

namespace BMCMeterAnalysis
{
    /// <summary>
    /// Usercontrol for meter analysis screen
    /// </summary>
    /// <Author>Madhusudhanan</Author>
    /// <DateCreated>14-5-2008</DateCreated>
    public partial class ucBMCMeterAnalysis : UserControl
    {
        #region "Variable Declaration"
        Int32 iSiteId = -1, iDistrictId = -1, iAreaId = -1, iRegionId = -1, iSubCompanyId = -1, iCompanyId = -1;
        //Arraylist to store the Graph Data.
        private static ArrayList objCollection = new ArrayList();
        //Datatable to store the retrieved data from DB.
        private DataTable dtMainGrid = new DataTable();
        private DataTable dtGraphTable = new DataTable();
        private const string strCompany = "Company";
        private const string strAvg = "Average";
        private bool blnbtnProcessClicked = false;
        private bool blnucBMCMeterAnalysisLoading = false;
        private static int iSelectedIndexMeterDetails = 1;
        private static string strcmbSelectedText = "";
        public static bool blnbtnZoomGraphVisible = false;
        private ListSortDirection direction = ListSortDirection.Descending;
        private string LastSort = string.Empty;
        //private string[] strMeterName = { "Handle", "Net win", "Games Played", "DOF", "Avg Bet", "Avg Daily Win", "Qty", "Avg Games Plyd", "Occupancy", "Theo Net Win", "Theo %", "Act %", "% Var" };
        private Hashtable hstMeterName = new Hashtable();
        private ToolTip ToolTip1 = null;
        string naText, allCapsText = string.Empty;
        string grpBySUBCOMPANY, grpByREGION, grpByAREA, grpByDISTRICT, grpBySITE, grpByZONE, grpByPOSITION, grpByCATEGORY, grpByTYPE, grpByOPERATOR, grpByDEPOT, grpByGAMETITLE, grpByGAMEASSET, grpByASSET = string.Empty;
        string hdrCompany, hdrSubCompany, hdrRegion, hdrArea, hdrDistrict, hdrSite, hdrZone, hdrType, hdrGameTitle, hdrInstallation, hdrCasinoWin =string.Empty;
        string hdrManufacturer, hdrCategory, hdrDepot, hdrOperator, hdrPosition, hdrAsset = string.Empty;
        string recCntTop20Perc, recCntBottom20Perc = string.Empty;
        string typeAvgDailyWin, typeCasinoWin, typeGamesBet, typeHandle = string.Empty;
        string mtrHandle, mtrNetwin, mtrGamesPlayed, mtrDOF, mtrAvgBet, mtrAvgDailyWin, mtrQty, mtrAvgGamesPlyd, mtrOccupancy, mtrTheoNetWin, mtrTheoPerc, mtrActPerc, mtrPercVar = string.Empty;
        private int _userID = 0;
        enum DatatoGet
        {
            Grid,
            Graph,
            Both
        }

        
        #endregion "Variable Declaration"

        #region "Constructor"
        /// <summary>
        /// Constructor.
        /// </summary>
        public ucBMCMeterAnalysis(int userID)
        {
            InitializeComponent();
            SetTagProperty();
            _userID = userID;
            ToolTip1 = new ToolTip();
            PaintGradient();

            allCapsText = this.GetResourceTextByKey("Key_ALLCaps");
            naText = this.GetResourceTextByKey("Key_NA");
            GetColumnHeaderText();
        }

        private void GetColumnHeaderText()
        { 
            hdrCompany = this.GetResourceTextByKey("Key_Company");
            hdrSubCompany = this.GetResourceTextByKey("Key_SubCompany");
            hdrRegion = this.GetResourceTextByKey("Key_Region");
            hdrArea = this.GetResourceTextByKey("Key_Area");
            hdrDistrict = this.GetResourceTextByKey("Key_District");
            hdrSite = this.GetResourceTextByKey("Key_Site");
            hdrZone = this.GetResourceTextByKey("Key_Zone");
            hdrType = this.GetResourceTextByKey("Key_Type");
            hdrGameTitle = this.GetResourceTextByKey("Key_GameTitle");
            hdrInstallation = this.GetResourceTextByKey("Key_Installation");
            hdrManufacturer = this.GetResourceTextByKey("Key_Manufacturer");
            hdrCategory = this.GetResourceTextByKey("Key_Category");
            hdrDepot = this.GetResourceTextByKey("Key_Depot");
            hdrOperator = this.GetResourceTextByKey("Key_Operator");
            hdrPosition = this.GetResourceTextByKey("Key_Position");
            hdrAsset = this.GetResourceTextByKey("Key_Asset");
            hdrCasinoWin = this.GetResourceTextByKey("Key_CasinoWinText");
        }

        private void PaintGradient()
        {

            System.Drawing.Drawing2D.LinearGradientBrush gradBrush;

            System.Drawing.Drawing2D.ColorBlend clrbld;
            clrbld = new System.Drawing.Drawing2D.ColorBlend(3);
            gradBrush = new System.Drawing.Drawing2D.LinearGradientBrush(new
                   Point(0, 0), new Point(this.Width, this.Height), Color.FromArgb(0, 100, 220), Color.White);
            //Color.FromArgb(143, 199, 255)
            Bitmap bmp = new Bitmap(this.Width, this.Height);

            Graphics g = Graphics.FromImage(bmp);

            g.FillRectangle(gradBrush, new Rectangle(0, 0, this.Width, this.Height));

            this.BackgroundImage = bmp;
            this.BackgroundImageLayout = ImageLayout.Stretch;


        }
        #endregion "Constructor"

        #region "on Control Load"
        /// <summary>
        /// control load event
        /// </summary>
        /// <Author>Madhusudhanan</Author>
        /// <DateCreated>13-5-08</DateCreated>
        /// <Parameters></Parameters>
        private void ucBMCMeterAnalysis_Load(object sender, EventArgs e)
        {
            this.ResolveResources();
         
            //Set the Loading status to True
            blnucBMCMeterAnalysisLoading = true;

            //Hide the graph till data is loaded.
            axMSChartMeterGraph.Visible = false;

            blnbtnZoomGraphVisible = false;
            dgMeterDetails.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
            try
            {
                dtpFrom.Value = DateTime.Now.AddDays(-1);
                dtpTo.Value = DateTime.Now;
                ConfigManager.SetConfigurationMode(ConfigManager.ConfigurationMode.AppConfig);
                //Fill the Combos.
                FillCombo(cmbCategory, MeterAnalysisBI.GetList("Category", 0), "Game_Category_ID", "Game_Category_Name");
                FillCombo(cmbOperator, MeterAnalysisBI.GetList("Operator", 0), "Operator_ID", "Operator_Name");
                FillCombo(cmbType, MeterAnalysisBI.GetList("Types", 0), "Machine_Type_ID", "Machine_Type_Code");
                FillCombo(cmbManufacturer, MeterAnalysisBI.GetList("Manufacturer", 0), "Manufacturer_ID", "Manufacturer_Name");

                LoadGroupBy();//cmbGroupBy
                LoadSortedDetails(); //cmbDataType,cmbRecordCount




                //  FillCombo(cmbGroupBy, ConfigManager.Read("GroupByItems").ToString().Split(','));
                // FillCombo(cmbRecordCount, ConfigManager.Read("RecordCountItems").ToString().Split(','));
                // FillCombo(cmbDatatype, ConfigManager.Read("DatatypeItems").ToString().Split(','));


                if (cmbOperator.SelectedIndex > 0)
                {
                    FillCombo(cmbDepot, MeterAnalysisBI.GetList("Depot", ToInteger(cmbOperator.SelectedValue)), "Depot_ID", "Depot_Name");
                }
                else
                {
                    FillCombo(cmbDepot, MeterAnalysisBI.GetList("Depot", 0), "Depot_ID", "Depot_Name");
                }
                if (cmbType.SelectedIndex > 0)
                {
                    FillCombo(cmbGame, MeterAnalysisBI.GetList("MachineClass", ToInteger(cmbType.SelectedValue)), "Machine_Class_ID", "Machine_Name");
                }
                else
                {
                    FillCombo(cmbGame, MeterAnalysisBI.GetList("MachineClass", 0), "Machine_Class_ID", "Machine_Name");
                }

                LoadCriteria();
                // mtrHandle, mtrNetwin, mtrGamesPlayed, mtrDOF, mtrAvgBet, mtrAvgDailyWin, mtrQty, mtrAvgGamesPlyd, mtrOccupancy, mtrTheoNetWin, mtrTheoPerc, mtrActPerc, mtrPercVar

                hstMeterName.Add(mtrHandle, "Handle");                  //hstMeterName.Add("Handle", "Handle");
                hstMeterName.Add(mtrNetwin, "Casino Win");              //hstMeterName.Add("Net win", "Casino Win");
                hstMeterName.Add(mtrGamesPlayed, "Games Played");       //hstMeterName.Add("Games Played", "Games Played");
                hstMeterName.Add(mtrDOF, "DOF");                        //hstMeterName.Add("DOF", "DOF");
                hstMeterName.Add(mtrAvgBet, "Avg Bet");                 //hstMeterName.Add("Avg Bet", "Avg Bet");
                hstMeterName.Add(mtrAvgDailyWin, "Avg Daily Win");      //hstMeterName.Add("Avg Daily Win", "Avg Daily Win");
                hstMeterName.Add(mtrQty, "Qty");                    //hstMeterName.Add("Qty", "Qty");
                hstMeterName.Add(mtrAvgGamesPlyd, "Avg Games");     //hstMeterName.Add("Avg Games Plyd", "Avg Games");
                hstMeterName.Add(mtrOccupancy, "Occupancy");        //hstMeterName.Add("Occupancy", "Occupancy");
                hstMeterName.Add(mtrTheoNetWin, "Theo_Net_Win");    //hstMeterName.Add("Theo Net Win", "Theo_Net_Win");
                hstMeterName.Add(mtrTheoPerc, "Theo %");            //hstMeterName.Add("Theo %", "Theo %");
                hstMeterName.Add(mtrActPerc, "Act %");              //hstMeterName.Add("Act %", "Act %");
                hstMeterName.Add(mtrPercVar, "% Var");              //hstMeterName.Add("% Var", "% Var");
                
                FillSitesViewer();
                //Set the Loading status to False
                blnucBMCMeterAnalysisLoading = false;
            }
            catch (Exception exUserControlLoad)
            { ExceptionManager.Publish(exUserControlLoad); }
            finally
            { System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default; }
        }

        public void LoadCriteria()
        {            
            mtrHandle = this.GetResourceTextByKey("Key_Handle");
            mtrNetwin = this.GetResourceTextByKey("Key_NetWin");
            mtrGamesPlayed = this.GetResourceTextByKey("Key_GamesPlayed");
            mtrDOF = this.GetResourceTextByKey("Key_DOF");
            mtrAvgBet = this.GetResourceTextByKey("Key_AvgBet");
            mtrAvgDailyWin = this.GetResourceTextByKey("Key_AvgDailyWin");
            mtrQty = this.GetResourceTextByKey("Key_Qty");
            mtrAvgGamesPlyd = this.GetResourceTextByKey("Key_AvgGamesPlayed");
            mtrOccupancy = this.GetResourceTextByKey("Key_Occupancy");
            mtrTheoNetWin = this.GetResourceTextByKey("Key_TheoNetWin");
            mtrTheoPerc = this.GetResourceTextByKey("Key_TheoPerc");
            mtrActPerc = this.GetResourceTextByKey("Key_ActPerc");
            mtrPercVar = this.GetResourceTextByKey("Key_VarPerc");

            List<string> lststrMeterName = new List<string>() {mtrHandle, mtrNetwin, mtrGamesPlayed, mtrDOF, mtrAvgBet, mtrAvgDailyWin, mtrQty, mtrAvgGamesPlyd, mtrOccupancy, mtrTheoNetWin, mtrTheoPerc, mtrActPerc, mtrPercVar};

            lststrMeterName.Sort();
            string[] strArrSortedstrMeterName = lststrMeterName.ToArray();

            FillCombo(cmbCriteria, strArrSortedstrMeterName);
        }


        public void LoadGroupBy()
        {
            //Group By
            //string[] strGroupByItems = ConfigManager.Read("GroupByItems").Split(',');
            //List<string> lststrGroupBy = new List<string>(strGroupByItems);

            grpBySUBCOMPANY = this.GetResourceTextByKey("Key_SubCompanyCaps");
            grpByREGION = this.GetResourceTextByKey("Key_RegionCaps");
            grpByAREA = this.GetResourceTextByKey("Key_AreaCaps");
            grpByDISTRICT = this.GetResourceTextByKey("Key_DistrictCaps");
            grpBySITE = this.GetResourceTextByKey("Key_SiteCaps");
            grpByZONE = this.GetResourceTextByKey("Key_ZoneCaps");
            grpByPOSITION = this.GetResourceTextByKey("Key_PositionCaps");
            grpByCATEGORY = this.GetResourceTextByKey("Key_CategoryCaps");
            grpByTYPE = this.GetResourceTextByKey("Key_TypeCaps");
            grpByOPERATOR = this.GetResourceTextByKey("Key_OperatorCaps");
            grpByDEPOT = this.GetResourceTextByKey("Key_DepotCaps");
            grpByGAMETITLE = this.GetResourceTextByKey("Key_GameTitleCaps");
            grpByGAMEASSET = this.GetResourceTextByKey("Key_GameAssetCaps");
            grpByASSET = this.GetResourceTextByKey("Key_AssetCaps");

            List<string> lststrGroupBy = new List<string>() { grpBySUBCOMPANY, grpByREGION, grpByAREA, grpByDISTRICT, grpBySITE, grpByZONE, 
                grpByPOSITION, grpByCATEGORY, grpByTYPE, grpByOPERATOR, grpByDEPOT, grpByGAMETITLE, grpByGAMEASSET, grpByASSET };

            lststrGroupBy.Sort();
            string[] strArrGroupByItems = lststrGroupBy.ToArray();
            FillCombo(cmbGroupBy, strArrGroupByItems);
        }

        public void LoadSortedDetails()
        {
             // Fill Data Type Combo
            typeAvgDailyWin = this.GetResourceTextByKey("Key_AvgDailyWin").ToUpper();
            typeCasinoWin =  this.GetResourceTextByKey("Key_CasinoWin").ToUpper();
            typeGamesBet = this.GetResourceTextByKey("Key_GamesBet").ToUpper();
            typeHandle = this.GetResourceTextByKey("Key_Handle").ToUpper() ;            
            List<string> lststrDataTypeItems = new List<string>() {typeAvgDailyWin,  typeCasinoWin, typeGamesBet, typeHandle };            
            lststrDataTypeItems.Sort();
            string[] strArrDataTypeItems = lststrDataTypeItems.ToArray();
            FillCombo(cmbDatatype, strArrDataTypeItems);           

            // Fill Record Count Combo
            recCntTop20Perc = this.GetResourceTextByKey("Key_Top20Perc");
            recCntBottom20Perc = this.GetResourceTextByKey("Key_Bottom20Perc");
            List<string> lststrRecordCountItems = new List<string>() { allCapsText, recCntTop20Perc, recCntBottom20Perc };
            lststrRecordCountItems.Sort();
            string[] strArrRecordCountItems = lststrRecordCountItems.ToArray();
            FillCombo(cmbRecordCount, strArrRecordCountItems);
        }
        #endregion

        #region "Fill combos Methods"
        /// <summary>
        /// Fill combo with data from the array
        /// </summary>
        /// <Author>Madhusudhanan</Author>
        /// <DateCreated>13-5-08</DateCreated>
        /// <Parameters>combobox and string array</Parameters>
        private void FillCombo(ComboBox cmbBox, string[] strValues)
        {
            try
            {
                cmbBox.Items.Clear();
                for (int iComboIndex = 0; iComboIndex < strValues.Length; iComboIndex++)
                {
                    cmbBox.Items.Add(strValues[iComboIndex].ToString());
                }
                cmbBox.SelectedIndex = 0;
            }
            catch (Exception exFillCombo)
            {
                LogManager.WriteLog("Error in Filling Combo" + "-Error Message-" + exFillCombo.Message, LogManager.enumLogLevel.Error);

            }
        }

        /// <summary>
        /// Fill combo with data from the datatable
        /// </summary>
        /// <Author>Madhusudhanan</Author>
        /// <DateCreated>13-5-08</DateCreated>
        /// <Parameters>combobox, datatable, value and display</Parameters>
        private void FillCombo(ComboBox cmbBox, DataTable dtSource, string strValueMember, string strDisplayMember)
        {
            // cmbBox.Items.Clear();
            cmbBox.DataSource = null;

            try
            {
                if (!string.IsNullOrEmpty(strValueMember))
                {
                    cmbBox.ValueMember = strValueMember;
                }
                cmbBox.DisplayMember = strDisplayMember;
                DataRow drRow = dtSource.NewRow();
                drRow[strDisplayMember] = this.GetResourceTextByKey("Key_AllCriteria");         // "--ALL--";
                drRow[strValueMember] = "0";
                dtSource.Rows.InsertAt(drRow, 0);
                cmbBox.DataSource = dtSource;
                cmbBox.SelectedIndex = 0;
            }
            catch (Exception exFillComboB)
            {
                LogManager.WriteLog("Error in Filling Combo" + "-Error Message-" + exFillComboB.Message, LogManager.enumLogLevel.Error);
            }
        }
        #endregion

        #region "Load treeview data"
        /// <summary>
        /// Gets data from DB and fills the site Treeview 
        /// with data heirarchically
        /// </summary>
        /// <Author>Madhusudhanan</Author>
        /// <DateCreated>13-5-08</DateCreated>
        /// <Parameters> </Parameters>
        /// <returns></returns>
        private void FillSitesViewer()
        {
            Int32 iOldSubComp = 0, iOldCompany = 0, iOldRegion = 0, iOldArea = 0, iOldSite = 0, iOldDistrict = 0;
            TreeNode tnAllCompNode = new TreeNode();
            tvSiteDetails.Nodes.Clear();
            try
            {
                tnAllCompNode = tvSiteDetails.Nodes.Add("ALL", this.GetResourceTextByKey("Key_AllCompanies"));                    // "All Companies");
                //tnAllCompNode.Expand();
                tvSiteDetails.SelectedNode = tnAllCompNode;
                MeterAnalysisTransport objTransport = new MeterAnalysisTransport();
                objTransport.UserID = _userID;
                if (!chkActiveSites.Checked)
                    objTransport.SiteStatusID = 1;
                SqlDataReader drSites = MeterAnalysisBI.GetSitesList(objTransport);
                if (drSites != null)
                {
                    while (drSites.Read())
                    {
                        //Add company Node
                        if (iOldCompany != ToInteger(drSites["Company_ID"]))
                        {
                            tvSiteDetails.Nodes["ALL"].Nodes.Add("CO,#" + drSites["Company_ID"].ToString(), drSites["Company_Name"].ToString());
                            tvSiteDetails.Nodes["ALL"].ExpandAll();
                            iOldCompany = ToInteger(drSites["Company_ID"]);
                        }
                        //Add subcompany if available in record
                        if (iOldSubComp != ToInteger(drSites["Sub_Company_ID"]))
                        {
                            //find the parent where this node needs to be added
                            TreeNode tnNode = tvSiteDetails.Nodes.Find("CO,#" + drSites["Company_ID"].ToString(), true)[0];
                            //if found add the node to parent
                            if (tnNode != null)
                            {
                                tnNode.Nodes.Add("SC,#" + drSites["Sub_Company_ID"].ToString(), drSites["Sub_Company_Name"].ToString());
                                tnNode.ExpandAll();
                                iOldSubComp = ToInteger(drSites["Sub_Company_ID"]);
                            }
                        }
                        //add rregion node if regiond ID is available in the result
                        if (iOldRegion != ToInteger(drSites["Sub_Company_Region_ID"]))
                        {
                            TreeNode[] tnNodeArray;
                            TreeNode tnNode = new TreeNode();
                            tnNodeArray = tvSiteDetails.Nodes.Find("SC,#" + drSites["Sub_Company_ID"].ToString(), true);
                            if (tnNodeArray != null && tnNodeArray.Length > 0) { tnNode = tnNodeArray[0]; }
                            if (!string.IsNullOrEmpty(tnNode.Text) && !string.IsNullOrEmpty(drSites["Sub_Company_Region_Name"].ToString()))
                            {
                                tnNode.Nodes.Add("RE," + drSites["Sub_Company_ID"].ToString() + "#" + drSites["Sub_Company_Region_ID"].ToString(), drSites["Sub_Company_Region_Name"].ToString());
                                tnNode.ExpandAll();
                                iOldRegion = ToInteger(drSites["Sub_Company_Region_ID"]);
                            }
                        }
                        //add area node if available
                        if (iOldArea != ToInteger(drSites["Sub_Company_area_ID"]))
                        {
                            TreeNode[] tnNodeArray;
                            TreeNode tnNode = new TreeNode();
                            tnNodeArray = tvSiteDetails.Nodes.Find("RE," + drSites["Sub_Company_ID"].ToString() + "#" + drSites["Sub_Company_Region_ID"].ToString(), true);
                            if (tnNodeArray != null && tnNodeArray.Length > 0) { tnNode = tnNodeArray[0]; }
                            if (!string.IsNullOrEmpty(tnNode.Text) && !string.IsNullOrEmpty(drSites["Sub_Company_Area_Name"].ToString()))
                            {
                                tnNode.Nodes.Add("AR," + drSites["Sub_Company_ID"].ToString() + "#" + drSites["Sub_Company_Region_ID"].ToString() + "#" + drSites["Sub_Company_area_ID"].ToString(), drSites["Sub_Company_Area_Name"].ToString());
                                tnNode.ExpandAll();
                                iOldArea = ToInteger(drSites["Sub_Company_area_ID"]);
                            }
                        }
                        //add district node if available
                        if (iOldDistrict != ToInteger(drSites["Sub_Company_District_ID"]))
                        {
                            TreeNode[] tnNodeArray;
                            TreeNode tnNode = new TreeNode();
                            tnNodeArray = tvSiteDetails.Nodes.Find("AR," + drSites["Sub_Company_ID"].ToString() + "#" + drSites["Sub_Company_Region_ID"].ToString() + "#" + drSites["Sub_Company_area_ID"].ToString(), true);
                            if (tnNodeArray != null && tnNodeArray.Length > 0) { tnNode = tnNodeArray[0]; }
                            if (!string.IsNullOrEmpty(tnNode.Text) && !string.IsNullOrEmpty(drSites["Sub_Company_District_Name"].ToString()))
                            {
                                tnNode.Nodes.Add("DI," + drSites["Sub_Company_ID"].ToString() + "#" + drSites["Sub_Company_Region_ID"].ToString() + "#" + drSites["Sub_Company_area_ID"].ToString() + "#" + drSites["Sub_Company_District_ID"].ToString(), drSites["Sub_Company_District_Name"].ToString());
                                tnNode.ExpandAll();
                                iOldDistrict = ToInteger(drSites["Sub_Company_District_ID"]);
                            }
                        }
                        //Add site to the respective nodes
                        if (iOldSite != ToInteger(drSites["Site_ID"]))
                        {
                            TreeNode tnNode;
                            if (!string.IsNullOrEmpty(drSites["Sub_Company_District_ID"].ToString()))
                            {
                                tnNode = tvSiteDetails.Nodes.Find("DI," + drSites["Sub_Company_ID"].ToString() + "#" + drSites["Sub_Company_Region_ID"].ToString() + "#" + drSites["Sub_Company_area_ID"].ToString() + "#" + drSites["Sub_Company_District_ID"].ToString(), true)[0];

                            }
                            else if (!string.IsNullOrEmpty(drSites["Sub_Company_Area_ID"].ToString()))
                            {
                                tnNode = tvSiteDetails.Nodes.Find("AR," + drSites["Sub_Company_ID"].ToString() + "#" + drSites["Sub_Company_Region_ID"].ToString() + "#" + drSites["Sub_Company_area_ID"].ToString(), true)[0];
                            }
                            else if (!string.IsNullOrEmpty(drSites["Sub_Company_Region_ID"].ToString()))
                            {
                                tnNode = tvSiteDetails.Nodes.Find("RE," + drSites["Sub_Company_ID"].ToString() + "#" + drSites["Sub_Company_Region_ID"].ToString(), true)[0];
                            }
                            else if (!string.IsNullOrEmpty(drSites["Sub_Company_ID"].ToString()))
                            {
                                tnNode = tvSiteDetails.Nodes.Find("SC,#" + drSites["Sub_Company_ID"].ToString(), true)[0];
                            }
                            else
                            {
                                tnNode = tvSiteDetails.Nodes.Find("CO,#" + drSites["Company_ID"].ToString(), true)[0];
                            }
                            if (tnNode != null)
                            {
                                //tnNode.Nodes.Add("SI" +drSites["Sub_Company_ID"].ToString() + "#" + drSites["Sub_Company_Region_ID"].ToString() + "#" + drSites["Sub_Company_area_ID"].ToString()+"#" + drSites["Sub_Company_District_ID"].ToString()+"#"+ drSites["Site_ID"].ToString(), drSites["Site_Name"].ToString() + "[" + drSites["Site_Code"].ToString()+"]");
                                tnNode.Nodes.Add("SI,#" + drSites["Site_ID"].ToString(), drSites["Site_Name"].ToString() + "[" + drSites["Site_Code"].ToString() + "]");
                                tnNode.ExpandAll();
                            }
                        }
                    }
                }
            }
            catch (Exception exFillSite)
            {
                LogManager.WriteLog("Error occured in loading the Org structure Tree." + "-Error Message-" + exFillSite.Message, LogManager.enumLogLevel.Error);
            }
        }
        #endregion

        #region "Selected Value Changed Events for Combos"
        /// <summary>
        /// Operator combo selected index changed handler
        /// </summary>
        /// <Author>Madhusudhanan</Author>
        /// <DateCreated>14-5-08</DateCreated>
        /// <Parameters> </Parameters>
        /// <returns> </returns>
        private void cmbOperator_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbOperator.SelectedIndex > 0)
                {
                    FillCombo(cmbDepot, MeterAnalysisBI.GetList("Depot", ToInteger(cmbOperator.SelectedValue)), "Depot_ID", "Depot_Name");
                }
                else if (cmbOperator.SelectedIndex == 0)
                {
                    FillCombo(cmbDepot, MeterAnalysisBI.GetList("Depot", 0), "Depot_ID", "Depot_Name");
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        /// <summary>
        /// Type combo index changed handler
        /// </summary>
        /// <Author>Madhusudhanan</Author>
        /// <DateCreated>13-5-08</DateCreated>
        /// <Parameters> </Parameters>
        /// <returns> </returns>
        private void cmbType_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbType.SelectedIndex > 0)
                {
                    FillCombo(cmbGame, MeterAnalysisBI.GetList("MachineClass", ToInteger(cmbType.SelectedValue)), "Machine_Class_ID", "Machine_Name");
                }
                else if (cmbType.SelectedIndex == 0)
                {
                    FillCombo(cmbGame, MeterAnalysisBI.GetList("MachineClass", 0), "Machine_Class_ID", "Machine_Name");
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

        }
        #endregion

        #region "Tree view node click "
        /// <summary>
        /// treeview node click handler
        /// </summary>
        /// <Author>Madhusudhanan</Author>
        /// <DateCreated>13-5-08</DateCreated>
        /// <Parameters> </Parameters>
        /// <returns></returns>
        private void tvSiteDetails_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
            string strNodeKey = string.Empty;
            try
            {
                strNodeKey = e.Node.Name.Substring(0, 2);
                Int32 iRegID = 0;
                if (cmbGroupBy.SelectedItem != null)
                {
                    strcmbSelectedText = cmbGroupBy.SelectedItem.ToString();
                }
                LoadGroupBy();
                // FillCombo(cmbGroupBy, ConfigurationManager.AppSettings["GroupByItems"].ToString().Split(','));
                Int32 iLastIndex = e.Node.Name.LastIndexOf("#") + 1;
                FindRegion(e.Node, ref iRegID);

                if (iLastIndex > 0)
                {
                    Int32 iNodeId = ToInteger(e.Node.Name.Substring(iLastIndex));
                    //get the Node id and change options in Group by combo
                    //set values of IDs at higher level to 0 and lower level to -1
                    switch (strNodeKey)
                    {
                        case "SI":
                            iSiteId = iNodeId;
                            cmbGroupBy.Items.Remove(this.GetResourceTextByKey("Key_DistrictCaps"));     // "DISTRICT");
                            cmbGroupBy.Items.Remove(this.GetResourceTextByKey("Key_AreaCaps"));     //"AREA");
                            cmbGroupBy.Items.Remove(this.GetResourceTextByKey("Key_RegionCaps"));     //"REGION");
                            cmbGroupBy.Items.Remove(this.GetResourceTextByKey("Key_CompanyCaps"));     //"COMPANY");
                            cmbGroupBy.Items.Remove(this.GetResourceTextByKey("Key_SubCompanyCaps"));     //"SUB COMPANY");
                            iCompanyId = 0;
                            iSubCompanyId = 0;
                            iRegionId = 0;
                            iAreaId = 0;
                            iDistrictId = 0;
                            break;
                        case "DI":
                            iDistrictId = iNodeId;
                            cmbGroupBy.Items.Remove(this.GetResourceTextByKey("Key_AreaCaps"));     //"AREA");
                            cmbGroupBy.Items.Remove(this.GetResourceTextByKey("Key_RegionCaps"));     //"REGION");
                            cmbGroupBy.Items.Remove(this.GetResourceTextByKey("Key_CompanyCaps"));     //"COMPANY");
                            cmbGroupBy.Items.Remove(this.GetResourceTextByKey("Key_SubCompanyCaps"));     //"SUB COMPANY");
                            iCompanyId = 0;
                            iSubCompanyId = 0;
                            iRegionId = 0;
                            iAreaId = 0;
                            iSiteId = -1;
                            break;
                        case "AR":
                            iAreaId = iNodeId;
                            cmbGroupBy.Items.Remove(this.GetResourceTextByKey("Key_RegionCaps"));     //"REGION");
                            cmbGroupBy.Items.Remove(this.GetResourceTextByKey("Key_CompanyCaps"));     //"COMPANY");
                            cmbGroupBy.Items.Remove(this.GetResourceTextByKey("Key_SubCompanyCaps"));     //"SUB COMPANY");
                            iCompanyId = 0;
                            iSubCompanyId = 0;
                            iRegionId = 0;
                            iDistrictId = -1;
                            iSiteId = -1;
                            break;
                        case "RE":
                            iRegionId = iNodeId;
                            cmbGroupBy.Items.Remove(this.GetResourceTextByKey("Key_CompanyCaps"));     //"COMPANY");
                            cmbGroupBy.Items.Remove(this.GetResourceTextByKey("Key_SubCompanyCaps"));     //"SUB COMPANY");
                            iCompanyId = 0;
                            iSubCompanyId = 0;
                            iAreaId = -1;
                            iDistrictId = -1;
                            iSiteId = -1;
                            break;
                        case "SC":
                            iSubCompanyId = iNodeId;
                            cmbGroupBy.Items.Remove(this.GetResourceTextByKey("Key_CompanyCaps"));     //"COMPANY");
                            iCompanyId = 0;
                            iRegionId = -1;
                            iAreaId = -1;
                            iDistrictId = -1;
                            iSiteId = -1;
                            break;
                        case "CO":
                            iCompanyId = iNodeId;
                            iSubCompanyId = -1;
                            iRegionId = -1;
                            iAreaId = -1;
                            iDistrictId = -1;
                            iSiteId = -1;
                            break;
                        default:
                            iCompanyId = -1;
                            iSubCompanyId = -1;
                            iRegionId = -1;
                            iAreaId = -1;
                            iDistrictId = -1;
                            iSiteId = -1;
                            break;
                    }
                    MeterAnalysisTransport ObjMATransport = new MeterAnalysisTransport();
                    ObjMATransport.SiteID = iSiteId;
                    ObjMATransport.DistrictID = iDistrictId;
                    ObjMATransport.AreaID = iAreaId;
                    ObjMATransport.RegionID = iRegID;
                    ObjMATransport.SubCompanyID = iSubCompanyId;
                    ObjMATransport.CompanyID = iCompanyId;
                    ObjMATransport.SiteStatusID = 1;
                    ObjMATransport.UserID = _userID;
                    DataTable dtSiteDetails = new DataTable();
                    dtSiteDetails = MeterAnalysisBI.GetSiteDetails(ObjMATransport);
                    if (dtSiteDetails.Rows.Count > 0)
                    {
                        //set the details of the selected node into the respective textboxes
                        SetTextBoxText(txtCompany, iCompanyId, dtSiteDetails.Rows[0]["Company_Name"].ToString());
                        SetTextBoxText(txtSubCompany, iSubCompanyId, dtSiteDetails.Rows[0]["Sub_Company_Name"].ToString());
                        SetTextBoxText(txtRegion, iRegID, dtSiteDetails.Rows[0]["Sub_Company_Region_Name"].ToString());
                        SetTextBoxText(txtArea, iAreaId, dtSiteDetails.Rows[0]["Sub_Company_Area_Name"].ToString());
                        SetTextBoxText(txtDistrict, iDistrictId, dtSiteDetails.Rows[0]["Sub_Company_District_Name"].ToString());
                        SetTextBoxText(txtSite, iSiteId, dtSiteDetails.Rows[0]["Site_Name"].ToString());
                    }
                    else
                    {
                        txtCompany.Text = naText;       // "n/a";
                        txtSubCompany.Text = naText;    // "n/a";
                        txtRegion.Text = naText;        // "n/a";
                        txtArea.Text = naText;          // "n/a";
                        txtDistrict.Text = naText;      // "n/a";
                        txtSite.Text = naText;          // "n/a";
                    }
                }
                else
                {
                    txtCompany.Text = allCapsText;       // "ALL";
                    txtSubCompany.Text = allCapsText;    // "ALL";
                    txtRegion.Text = allCapsText;        // "ALL";
                    txtArea.Text = allCapsText;          // "ALL";
                    txtDistrict.Text = allCapsText;      // "ALL";
                    txtSite.Text = allCapsText;          // "ALL";
                    iCompanyId = -1;
                    iSubCompanyId = -1;
                    iRegionId = -1;
                    iAreaId = -1;
                    iDistrictId = -1;
                    iSiteId = -1;
                }
                tvSiteDetails.SelectedNode = e.Node;

                //Code to persist with the selected Group By option.
                //Set bool value to false.
                bool boolGroupBySelected = false;
                if (strcmbSelectedText.Trim() != "")
                {
                    try
                    {
                        //Loop thru to cjeck if this option exists on the item list of the combo.
                        foreach (object objItem in cmbGroupBy.Items)
                        {
                            if (objItem.ToString().Trim() == strcmbSelectedText.Trim())
                            {
                                boolGroupBySelected = true;
                                break;
                            }
                        }
                    }
                    catch { }//Do nothing.
                }
                if (boolGroupBySelected)
                {
                    cmbGroupBy.SelectedItem = strcmbSelectedText.Trim();
                }
                else
                {
                    cmbGroupBy.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            { ExceptionManager.Publish(ex); }
            finally
            { System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default; }
        }
        #endregion

        #region "Other private functions"
        /// <summary>
        /// Converts an object to integer
        /// </summary>
        /// <Author>Madhusudhanan</Author>
        /// <DateCreated>13-5-08</DateCreated>
        /// <Parameters> </Parameters>
        /// <returns>integer value</returns>
        private Int32 ToInteger(object ObjValue)
        {
            try
            {
                return Convert.ToInt32(ObjValue);
            }
            catch
            { return 0; }
        }
        /// <summary>
        /// Set the text of textbox
        /// </summary>
        /// <Author>Madhusudhanan</Author>
        /// <DateCreated>13-5-08</DateCreated>
        /// <Parameters> Textbox, selected node id, and the text to be set</Parameters>
        /// <returns></returns>
        private void SetTextBoxText(TextBox txtBox, Int32 iNodeID, string strText)
        {
            try
            {
                if (iNodeID != -1)
                {
                    if (!string.IsNullOrEmpty(strText))
                    {
                        txtBox.Text = strText;
                    }
                    else
                    {
                        txtBox.Text = naText;       // "n/a";
                    }
                }
                else
                {
                    txtBox.Text = allCapsText;       // "ALL";
                }
            }
            catch
            { txtBox.Text = naText;      // "n/a"; 
            }
        }

        /// <summary>
        /// Gets the value of integer variable
        /// if inout is -1 returns 0 else the same value
        /// </summary>
        /// <Author>Madhusudhanan</Author>
        /// <DateCreated>13-5-08</DateCreated>
        /// <Parameters>integer variable </Parameters>
        /// <returns>if inout is -1 returns 0 else the same value</returns>
        private Int32 GetValue(Int32 iValue)
        {
            try
            {
                //if value is not -1 then return original value
                if (iValue != -1)
                    return iValue;
                else
                    return 0;
            }
            catch
            { return 0; }
        }

        /// <summary>
        /// Returns combo value
        /// </summary>
        /// <Author>Madhusudhanan</Author>
        /// <DateCreated>13-5-08</DateCreated>
        /// <Parameters> </Parameters>
        /// <returns>returns selected value if selected, else 0 </returns>
        private Int32 GetComboValue(ComboBox cmbBox)
        {
            try
            {
                if (cmbBox.SelectedIndex > 0)
                    return ToInteger(cmbBox.SelectedValue);
                else
                    return 0;
            }
            catch
            { return 0; }
        }
        /// <summary>
        /// Sets the header text of a column in the grid
        /// and set the visibility as false if text is empty 
        /// </summary>
        /// <Author>Madhusudhanan</Author>
        /// <DateCreated>20-5-08</DateCreated>
        /// <Parameters>Column  name and header text</Parameters>
        /// <returns></returns>
        private void SetColumnHeaderText(string strColumnName, string strHeaderText)
        {
            bool blnHasText = false;
            DataGridViewColumn dgcColumn;
            try
            {
                dgcColumn = dgMeterDetails.Columns[strColumnName];
                if (dgcColumn != null)
                {
                    //chk each row whether the text is available
                    foreach (DataGridViewRow dgrRow in dgMeterDetails.Rows)
                    {
                        if (dgrRow.Cells[strColumnName].Value != null)
                        {
                            //if text found break the loop
                            if (!string.IsNullOrEmpty(dgrRow.Cells[strColumnName].Value.ToString().Trim()))
                            {
                                blnHasText = true;
                                break;
                            }
                        }
                    }
                    //if text not found set visibilty as false
                    if (!blnHasText)
                    {
                        dgcColumn.Visible = false;
                    }
                    //else set visibilty and header text
                    else
                    {
                        dgcColumn.Visible = true;
                        dgcColumn.HeaderText = strHeaderText;
                        dgcColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
                    }
                }
                //dgcColumn.Visible = true;
                //dgcColumn.HeaderText = strHeaderText;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error in Setting Column Header Text for " + strColumnName + "-Error Message-" + ex.Message, LogManager.enumLogLevel.Error);

            }
        }
        //Overloaded method for setting column properties
        private void SetColumnHeaderText(string strColumnName, string strHeaderText, DataGridViewContentAlignment Alignment, string strFormat)
        {
            DataGridViewColumn dgcColumn;
            try
            {
                dgcColumn = dgMeterDetails.Columns[strColumnName];
                dgcColumn.Visible = true;
                dgcColumn.HeaderText = strHeaderText;
                dgcColumn.DefaultCellStyle.Alignment = Alignment;
                if (!string.IsNullOrEmpty(strFormat))
                {
                    dgcColumn.DefaultCellStyle.Format = strFormat;
                }
                dgcColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error in Setting Column Header Text" + "-Error Message-" + ex.Message, LogManager.enumLogLevel.Error);

            }
        }

        #endregion

        #region "Load main grid"
        /// <summary>
        /// Gets data for the Main grid and graph
        /// </summary>
        /// <Author>Madhusudhanan</Author>
        /// <DateCreated>13-5-08</DateCreated>
        /// <Parameters> True for filling datagrid, false for filling graph </Parameters>
        /// <returns></returns>
        private DataTable LoadMainGrid(DatatoGet datatoget)
        {
            String strFromDate = "";
            String strToDate = "";
            try
            {
                cmbCriteria.Enabled = cmbRecordCount.SelectedItem.ToString() != allCapsText;      // "ALL";
                MeterAnalysisTransport objMATransport = new MeterAnalysisTransport();
                strFromDate = dtpFrom.Value.ToShortDateString();
                strToDate = dtpTo.Value.ToShortDateString();
                //set  properties to transport object
                objMATransport.StartDate = Convert.ToDateTime(strFromDate + " 00:00:00 AM");
                objMATransport.EndDate = Convert.ToDateTime(strToDate + " 00:00:00 PM");
                objMATransport.CompanyID = GetValue(iCompanyId);
                objMATransport.SubCompanyID = GetValue(iSubCompanyId);
                objMATransport.SiteID = GetValue(iSiteId);
                objMATransport.OperatorID = GetComboValue(cmbOperator);
                objMATransport.DepotID = GetComboValue(cmbDepot);
                objMATransport.TypeID = GetComboValue(cmbType);
                objMATransport.CategoryID = GetComboValue(cmbCategory);
                objMATransport.ClassID = GetComboValue(cmbGame);
                objMATransport.RegionID = GetValue(iRegionId);
                objMATransport.AreaID = GetValue(iAreaId);
                objMATransport.DistrictID = GetValue(iDistrictId);
                objMATransport.ManufacturerID = GetComboValue(cmbManufacturer);
                objMATransport.PeriodID = 0;
                objMATransport.Active = chkActive.Checked ? "yes" : "";
                objMATransport.IsOnlyActiveSites = chkActiveSites.Checked ? "yes" : "no";

                //send order by clause based on selected value
               
                // grpBySUBCOMPANY, grpByREGION, grpByAREA, grpByDISTRICT, grpBySITE, grpByZONE, 
                // grpByPOSITION, grpByCATEGORY, grpByTYPE, grpByOPERATOR, grpByDEPOT, grpByGAMETITLE, grpByGAMEASSET, grpByASSET

                    if(cmbGroupBy.SelectedItem.ToString() == grpBySUBCOMPANY)  // "SUB COMPANY":
                        objMATransport.GroupByClause = "Sub_Company_Name"; 
                    else if(cmbGroupBy.SelectedItem.ToString() == grpByREGION)
                        objMATransport.GroupByClause = "Region_ID"; 
                    else if(cmbGroupBy.SelectedItem.ToString() == grpByAREA)   //  "AREA":
                        objMATransport.GroupByClause = "Area_ID"; 
                    else if(cmbGroupBy.SelectedItem.ToString() == grpByDISTRICT)   //  "DISTRICT":
                        objMATransport.GroupByClause = "District_ID"; 
                    else if(cmbGroupBy.SelectedItem.ToString() == grpBySITE)   //  "SITE":
                        objMATransport.GroupByClause = "Site_Name"; 
                    else if(cmbGroupBy.SelectedItem.ToString() == grpByZONE)   //  "ZONE":
                        objMATransport.GroupByClause = "Zone_Name"; 
                    else if(cmbGroupBy.SelectedItem.ToString() == grpByPOSITION)   //  "POSITION":
                        objMATransport.GroupByClause = "Bar_Position_Name"; 
                    else if(cmbGroupBy.SelectedItem.ToString() == grpByCATEGORY)   //  "CATEGORY":
                        objMATransport.GroupByClause = "Category_Code"; 
                    else if(cmbGroupBy.SelectedItem.ToString() == grpByTYPE)   //  "TYPE":
                        objMATransport.GroupByClause = "Machine_Type_Code"; 
                    else if(cmbGroupBy.SelectedItem.ToString() == grpByOPERATOR)   //  "OPERATOR":
                        objMATransport.GroupByClause = "Operator_Name"; 
                    else if(cmbGroupBy.SelectedItem.ToString() == grpByDEPOT)   //  "DEPOT":
                        objMATransport.GroupByClause = "Depot_Name"; 
                    else if(cmbGroupBy.SelectedItem.ToString() == grpByGAMETITLE)   //  "GAMETITLE":
                        objMATransport.GroupByClause = "Machine_Name"; 
                    else if(cmbGroupBy.SelectedItem.ToString() == grpByGAMEASSET)   //  "GAMEASSET":
                        objMATransport.GroupByClause = "Installation_ID"; 
                    else if(cmbGroupBy.SelectedItem.ToString() == grpByASSET)   //  "ASSET":
                        objMATransport.GroupByClause = "Machine_Stock_No"; 
                    else
                        objMATransport.GroupByClause = ""; 
                
                //if used for filling grid
                //get records based on selected value
                //switch (cmbRecordCount.SelectedItem.ToString())
                //{
                    if(cmbRecordCount.SelectedItem.ToString() == recCntTop20Perc)   // "TOP 20%":
                    {
                        objMATransport.NoOfRecords = "Top20";
                        //objMATransport.Criteria = hstMeterName[cmbCriteria.Text].ToString(); 
                        //LoadCriteria();
                        objMATransport.Criteria = hstMeterName[cmbCriteria.Text].ToString();
                    }
                    else  if(cmbRecordCount.SelectedItem.ToString() == recCntBottom20Perc)   //"BOTTOM 20%":
                    {
                        objMATransport.NoOfRecords = "Bottom20";
                        //LoadCriteria();
                        objMATransport.Criteria = hstMeterName[cmbCriteria.Text].ToString();
                        //objMATransport.Criteria = hstMeterName[cmbCriteria.Text].ToString(); 
                    }
                    else
                        objMATransport.NoOfRecords = string.Empty;                      
                  //}
                if (rdnHold.Checked)
                {
                    if (objMATransport.Criteria == "Theo %") objMATransport.Criteria = "Hold_Perc";
                    if (objMATransport.Criteria == "Act %") objMATransport.Criteria = "Hold_Act_Perc";
                    if (objMATransport.Criteria == "% Var") objMATransport.Criteria = "Hold_Perc_Var";
                }
                else
                {
                    if (objMATransport.Criteria == "Theo %") objMATransport.Criteria = "Payout_Perc";
                    if (objMATransport.Criteria == "Act %") objMATransport.Criteria = "Payout_Act_Perc";
                    if (objMATransport.Criteria == "% Var") objMATransport.Criteria = "Payout_Perc_Var";
                }

                // TODO: Check why this is used??
                switch (cmbCriteria.SelectedItem.ToString())
                {
                    case "Handle":
                        objMATransport.Criteria = "Handle"; break;
                    case " Net Win":
                        objMATransport.Criteria = "Net Win"; break;
                    case " Games Played":
                        objMATransport.Criteria = "Games Played"; break;
                    case " DOF":
                        objMATransport.Criteria = "DOF"; break;
                    case " Avg Bet":
                        objMATransport.Criteria = "Avg Bet"; break;
                    case " Avg Daily Win":
                        objMATransport.Criteria = "Avg Daily Win"; break;
                    case " Qty":
                        objMATransport.Criteria = "Qty"; break;
                    case " Avg Games Played":
                        objMATransport.Criteria = "Avg Games Plyd"; break;
                    case " Occupancy":
                        objMATransport.Criteria = "Occupancy"; break;
                    case " Theo%":
                        objMATransport.Criteria = "Theo%"; break;
                    case " Act%":
                        objMATransport.Criteria = "Act%"; break;
                    case " %Var":
                        objMATransport.Criteria = "%Var"; break;
                    case " Theo Net Win":
                        objMATransport.Criteria = "Theo Net Win"; break;
                }

                objMATransport.SearchMCID = 0;
                objMATransport.SearchInstallationID = 0;
                objMATransport.SearchAsset = "";
                objMATransport.UserID = _userID;
                if (rdnWeek.Checked)
                { objMATransport.PeriodID = 2; }
                else if (rdnPeriod.Checked)
                { objMATransport.PeriodID = 3; }
                else
                { objMATransport.PeriodID = 1; }




                if (datatoget == DatatoGet.Grid)
                {
                    GetDataForGrid((object)objMATransport);
                }
                else if (datatoget == DatatoGet.Graph)
                {
                    GetDataForGraph((object)objMATransport);
                }
                else
                {
                    Thread t1 = new Thread(new ParameterizedThreadStart(GetDataForGrid));
                    t1.Start((object)objMATransport);
                    Thread t2 = new Thread(new ParameterizedThreadStart(GetDataForGraph));
                    t2.Start((object)objMATransport);
                    t1.Join();
                    t2.Join();
                }
                //old code b4 threading..so still using to return empty data table
                return new DataTable();
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error in loading main grid" + "-Error Message-" + ex.Message, LogManager.enumLogLevel.Error);
                throw ex;
            }
        }

        private void GetDataForGrid(object myObject)
        {
            dtMainGrid = MeterAnalysisBI.GetMainGridDetails((MeterAnalysisTransport)myObject, true);
        }

        private void GetDataForGraph(object myObject)
        {
            dtGraphTable = MeterAnalysisBI.GetGraphDetails((MeterAnalysisTransport)myObject);
        }


        /// <summary>
        /// Method to Load Grid.
        /// </summary>
        /// <Author>Madhusudhanan</Author>
        /// <DateCreated>13-5-08</DateCreated>
        /// <Parameters> </Parameters>
        /// <returns></returns>
        private void LoadGrid()
        {
            try
            {
                statusBar2.Text = "";

                //Check if Todate is grater than FromDate.
                if (dtpFrom.Value > dtpTo.Value)
                {
                    MessageBox.Show(this.GetResourceTextByKey(1, "MSG_STARTDT_LESS_ENDDT"), this.GetResourceTextByKey(1, "MSG_APP_TITLE"));
                    dgMeterDetails.DataSource = null;
                    dgMeterDetails.Rows.Clear();
                    axMSChartMeterGraph.Visible = false;

                    blnbtnZoomGraphVisible = false;
                    return;
                }
                else
                {
                    if ((12 * (dtpTo.Value.Year - dtpFrom.Value.Year)) + (dtpTo.Value.Month - dtpFrom.Value.Month) > ToInteger(ConfigurationManager.AppSettings["MaxMonths"]))
                    {
                        MessageBox.Show(this.GetResourceTextByKey(1, "MSG_SEARCHRANGE_EXCEED"), this.GetResourceTextByKey(1, "MSG_APP_TITLE"));
                        return;
                    }
                    blnbtnProcessClicked = true;
                    System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
                    //For filling main grid
                    dtMainGrid = new DataTable();
                    LoadMainGrid(DatatoGet.Both);

                    if (dtMainGrid.Rows.Count > 0)
                    {
                        DataGridViewCellStyle dgStyle = new DataGridViewCellStyle();
                        dgStyle.ForeColor = Color.DarkBlue;
                        dgMeterDetails.Rows.Clear();
                        dgMeterDetails.Columns.Clear();
                        //Set the data to the Grid by adding columns and rows manually.
                        foreach (DataColumn column in dtMainGrid.Columns)
                            dgMeterDetails.Columns.Add(column.ColumnName, column.ColumnName);
                        foreach (DataRow row in dtMainGrid.Rows)
                            dgMeterDetails.Rows.Add(row.ItemArray);
                        dgMeterDetails.Rows[0].DefaultCellStyle = dgStyle;
                        for (Int32 iColIndex = 0; iColIndex < dgMeterDetails.Columns.Count; iColIndex++)
                        {
                            dgMeterDetails.Columns[iColIndex].Visible = false;
                        }

                        // grpBySUBCOMPANY, grpByREGION, grpByAREA, grpByDISTRICT, grpBySITE, grpByZONE, 
                        // grpByPOSITION, grpByCATEGORY, grpByTYPE, grpByOPERATOR, grpByDEPOT, grpByGAMETITLE, grpByGAMEASSET, grpByASSET

                        //set visibilty and header text for columns based on selected group by
                        //switch (cmbGroupBy.SelectedItem.ToString())
                        //{
                            if(cmbGroupBy.SelectedItem.ToString() == grpBySUBCOMPANY) // "SUB COMPANY":
                            {
                                SetColumnHeaderText("Sub Company", hdrSubCompany);     //  "Sub Company");
                                SetColumnHeaderText("Company", hdrCompany);          //"Company");
                            }
                            else if(cmbGroupBy.SelectedItem.ToString() == grpByREGION) // "REGION":
                            {
                                SetColumnHeaderText("Region", hdrRegion);               //  "Region");
                                SetColumnHeaderText("Sub Company", hdrSubCompany);     //"Sub Company");
                                SetColumnHeaderText("Company", hdrCompany);          //"Company");
                            }
                            else if(cmbGroupBy.SelectedItem.ToString() == grpByAREA) // "AREA":
                            {
                                SetColumnHeaderText("Area", hdrArea);                   //"Area");
                                SetColumnHeaderText("Sub Company", hdrSubCompany);     //"Sub Company");
                                SetColumnHeaderText("Company", hdrCompany);          //"Company");
                                SetColumnHeaderText("Region", hdrRegion);          // "Region");
                                }
                            else if(cmbGroupBy.SelectedItem.ToString() == grpByDISTRICT) // "DISTRICT":
                            {
                                SetColumnHeaderText("District", hdrDistrict);         // "District");
                                SetColumnHeaderText("Sub Company", hdrSubCompany);     //"Sub Company");
                                SetColumnHeaderText("Company", hdrCompany);          //"Company");
                                SetColumnHeaderText("Region", hdrRegion);          // "Region");
                                SetColumnHeaderText("Area", hdrArea);           // "Area");
                            }
                            else if(cmbGroupBy.SelectedItem.ToString() == grpBySITE) // "SITE":
                            {
                                SetColumnHeaderText("Site", hdrSite);               //"Site");
                                SetColumnHeaderText("Sub Company", hdrSubCompany);     //"Sub Company");
                                SetColumnHeaderText("Company", hdrCompany);          //"Company");
                                SetColumnHeaderText("Region", hdrRegion);          //"Region");
                                SetColumnHeaderText("Area", hdrArea);           //"Area");
                                SetColumnHeaderText("District", hdrDistrict);         //"District");
                            }
                            else if(cmbGroupBy.SelectedItem.ToString() == grpByZONE) // "ZONE":
                            {
                                SetColumnHeaderText("Zone", hdrZone);               // "Zone");
                                SetColumnHeaderText("Company", hdrCompany);          //"Company");
                                SetColumnHeaderText("Sub Company", hdrSubCompany);     //"Sub Company");
                                SetColumnHeaderText("Region", hdrRegion);           //"Region");
                                SetColumnHeaderText("Area", hdrArea);               //"Area");
                                SetColumnHeaderText("District", hdrDistrict);         // "District");
                                SetColumnHeaderText("Site", hdrSite);               // "Site");
                                }
                            else if(cmbGroupBy.SelectedItem.ToString() == grpByPOSITION ) // "POSITION":
                            {
                                SetColumnHeaderText("Position", hdrPosition);            //"Position");
                                SetColumnHeaderText("Company", hdrCompany);             //"Company");
                                SetColumnHeaderText("Sub Company", hdrSubCompany);     //"Sub Company");
                                SetColumnHeaderText("Region", hdrRegion);               //"Region");
                                SetColumnHeaderText("Area", hdrArea);                   //"Area");
                                SetColumnHeaderText("District", hdrDistrict);          //"District");
                                SetColumnHeaderText("Site", hdrSite);               //"Site");
                                SetColumnHeaderText("Zone", hdrZone);               //"Zone");
                                SetColumnHeaderText("Type", hdrType);               // "Type");
                                SetColumnHeaderText("Game Title", hdrGameTitle);     // "Game Title");
                            }
                            else if(cmbGroupBy.SelectedItem.ToString() == grpByCATEGORY) // "CATEGORY":
                            {
                                SetColumnHeaderText("Category", hdrCategory);       // "Category");
                                SetColumnHeaderText("Type", hdrType);               //"Type");
                            }
                            else if(cmbGroupBy.SelectedItem.ToString() == grpByTYPE) // "TYPE":
                            {
                                SetColumnHeaderText("Type", hdrType);               //"Type");
                            }
                            else if(cmbGroupBy.SelectedItem.ToString() == grpByOPERATOR) // "OPERATOR":
                            {
                                SetColumnHeaderText("Operator", hdrOperator);       //  "Operator");
                             }
                            else if(cmbGroupBy.SelectedItem.ToString() == grpByDEPOT) // "DEPOT":
                            {
                                SetColumnHeaderText("Depot", hdrDepot);             // "Depot");
                                SetColumnHeaderText("Operator", hdrOperator);       // "Operator");
                            }
                            else if(cmbGroupBy.SelectedItem.ToString() == grpByGAMETITLE) // "GAMETITLE":
                            {
                                SetColumnHeaderText("Game Title", hdrGameTitle);     //"Game Title");
                                SetColumnHeaderText("Position", hdrPosition);        //"Position");
                                SetColumnHeaderText("Site", hdrSite);               // "Site");
                                SetColumnHeaderText("Company", hdrCompany);         // "Company");
                                SetColumnHeaderText("Sub Company", hdrSubCompany);  // "Sub Company");
                                SetColumnHeaderText("Manufacturer", hdrManufacturer); // "Manufacturer ");
                                SetColumnHeaderText("Category", hdrCategory);       //"Category");   
                                SetColumnHeaderText("Type", hdrType);               //"Type");
                            }
                            else if(cmbGroupBy.SelectedItem.ToString() == grpByGAMEASSET) // "GAMEASSET":
                            {
                                SetColumnHeaderText("Asset", hdrAsset);               //"Asset");
                                SetColumnHeaderText("Game Title", hdrGameTitle);     //"Game Title");
                                SetColumnHeaderText("Position", hdrPosition);        //"Position");
                                SetColumnHeaderText("Site", hdrSite);               //"Site");
                                SetColumnHeaderText("Sub Company", hdrSubCompany);  //"Sub Company");
                                SetColumnHeaderText("Company", hdrCompany);         //"Company");
                                SetColumnHeaderText("Installation", hdrInstallation);   //"Installation");
                                SetColumnHeaderText("Manufacturer_Name", hdrManufacturer); // "Manufacturer ");
                                SetColumnHeaderText("Category_Code", hdrCategory);    //"Category");
                                SetColumnHeaderText("Type", hdrType);                 //"Type");
                            }
                            else if(cmbGroupBy.SelectedItem.ToString() == grpByASSET) // "ASSET":
                            {
                                SetColumnHeaderText("Asset", hdrAsset);               // "Asset");
                                SetColumnHeaderText("Game Title", hdrGameTitle);     //"Game Title");
                                SetColumnHeaderText("Position", hdrPosition);        //"Position");
                                SetColumnHeaderText("Site", hdrSite);               //"Site");
                                SetColumnHeaderText("Sub Company", hdrSubCompany);  //"Sub Company");
                                SetColumnHeaderText("Company", hdrCompany);         //"Company");
                                SetColumnHeaderText("Manufacturer", hdrManufacturer); // "Manufacturer ");
                                SetColumnHeaderText("Category", hdrCategory);       //"Category");
                                SetColumnHeaderText("Type", hdrType);               //"Type");
                           }
                            //default:                                
                        //}

                        // mtrHandle, mtrNetwin, mtrGamesPlayed, mtrDOF, mtrAvgBet, mtrAvgDailyWin, mtrQty, mtrAvgGamesPlyd, mtrOccupancy, mtrTheoNetWin, mtrTheoPerc, mtrActPerc, mtrPercVar

                        SetColumnHeaderText("Qty", mtrQty, DataGridViewContentAlignment.MiddleRight, "");
                        SetColumnHeaderText("Handle", mtrHandle, DataGridViewContentAlignment.MiddleRight, "##,##0.00");

                        SetColumnHeaderText("Casino Win", hdrCasinoWin, DataGridViewContentAlignment.MiddleRight, "##,##0.00");
                        SetColumnHeaderText("Avg Daily Win", mtrAvgDailyWin, DataGridViewContentAlignment.MiddleRight, "##,##0.00");
                        SetColumnHeaderText("Avg Bet", mtrAvgBet, DataGridViewContentAlignment.MiddleRight, "N");
                        SetColumnHeaderText("DOF", mtrDOF, DataGridViewContentAlignment.MiddleRight, "##,##0");
                        SetColumnHeaderText("Avg Games", mtrAvgGamesPlyd, DataGridViewContentAlignment.MiddleRight, "##,##0");
                        if (ConfigManager.Read("HideOccupancy").ToUpper() != "YES")
                        {
                            SetColumnHeaderText("Occupancy", mtrOccupancy, DataGridViewContentAlignment.MiddleRight, "##,##0.00");
                        }


                        SetColumnHeaderText("Games Played", mtrGamesPlayed, DataGridViewContentAlignment.MiddleRight, "##,##0");
                        SetColumnHeaderText("Theo_Net_Win", mtrTheoNetWin, DataGridViewContentAlignment.MiddleRight, "##,##0");
                        if (rdnHold.Checked)
                        {
                            SetColumnHeaderText("Hold_Perc", mtrTheoPerc, DataGridViewContentAlignment.MiddleRight, "##,##0.00");
                            SetColumnHeaderText("Hold_Act_Perc", mtrActPerc, DataGridViewContentAlignment.MiddleRight, "##,##0.00");
                            SetColumnHeaderText("Hold_Perc_Var", mtrPercVar, DataGridViewContentAlignment.MiddleRight, "##,##0.00");
                        }
                        else
                        {
                            SetColumnHeaderText("Payout_Perc", mtrTheoPerc, DataGridViewContentAlignment.MiddleRight, "##,##0.00");
                            SetColumnHeaderText("Payout_Act_Perc", mtrActPerc, DataGridViewContentAlignment.MiddleRight, "##,##0.00");
                            SetColumnHeaderText("Payout_Perc_Var", mtrPercVar, DataGridViewContentAlignment.MiddleRight, "##,##0.00");
                        }

                        dgMeterDetails.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        if(dgMeterDetails.Rows.Count >=2)
                            dgMeterDetails.Rows[1].Selected = true;
                        //Set the selectedIndex to 1.
                        iSelectedIndexMeterDetails = 1;
                        statusBar2.Text = string.Format(this.GetResourceTextByKey(1, "MSG_RECORDSFOUND"), (dtMainGrid.Rows.Count - 1).ToString());        // +" Record(s) found.";
                    }
                    else
                    {
                        MessageBox.Show(this.GetResourceTextByKey(1, "MSG_CDM_NO_RECORDS_FOR_SELECT"), this.GetResourceTextByKey(1, "MSG_APP_TITLE"));
                        statusBar2.Text = this.GetResourceTextByKey(1, "MSG_CDM_NO_RECORDS_FOR_SELECT");
                        dgMeterDetails.DataSource = null;
                        dgMeterDetails.Rows.Clear();
                        axMSChartMeterGraph.Visible = false;

                        blnbtnZoomGraphVisible = false;
                        return;
                    }
                    blnbtnProcessClicked = false;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error in LoadGrid" + "-Error Message-" + ex.Message, LogManager.enumLogLevel.Error);
                throw ex;
            }
        }
        #endregion
        # region "Graph Data"
        /// <summary>
        /// Method to Load Grid.
        /// </summary>
        /// <Author>Renjish N </Author>
        /// <DateCreated>16-May-2008</DateCreated>
        /// <Parameters> </Parameters>
        /// <returns></returns>
        private void LoadGraphData()
        {
            try
            {
                if (dtpFrom.Value > dtpTo.Value)
                {
                    dgMeterDetails.DataSource = null;
                    dgMeterDetails.Rows.Clear();
                    axMSChartMeterGraph.Visible = false;

                    blnbtnZoomGraphVisible = false;
                    statusBar2.Text = "";
                    return;
                }
                else if ((12 * (dtpTo.Value.Year - dtpFrom.Value.Year)) + (dtpTo.Value.Month - dtpFrom.Value.Month) > ToInteger(ConfigurationManager.AppSettings["MaxMonths"]))
                {
                    dgMeterDetails.DataSource = null;
                    dgMeterDetails.Rows.Clear();
                    axMSChartMeterGraph.Visible = false;

                    blnbtnZoomGraphVisible = false;
                    statusBar2.Text = "";
                    return;
                }
                //Load Graph only if some record is selected in Grid.
                if (iSelectedIndexMeterDetails > 0)
                {
                    //Declare variables.
                    DataTable objGraphDataTable = new DataTable();
                    DataTable objGraphDataTableDataView = dtMainGrid;
                    MeterAnalysisChartData objMeterAnalysisChartData = new MeterAnalysisChartData();
                    float fGraphValue = 0;
                    //Graph data count.
                    int iCount = 0;

                    //Clear the arraylist data.
                    objCollection.Clear();

                    //Retrieve data from DB.
                    objGraphDataTableDataView = dtGraphTable;// LoadMainGrid(false);
                    if (objGraphDataTableDataView.Rows.Count > 0)
                    {
                        //Sort data based on Period Date.
                        objGraphDataTableDataView.DefaultView.Sort = "Read_Date";
                        objGraphDataTable = objGraphDataTableDataView.DefaultView.ToTable();
                    }
                    //Load data to the chart.
                    if (objGraphDataTable.Rows.Count > 0)
                    {
                        //Iterate through each row and assign data to the arraylist.
                        foreach (DataRow objGraphDataRow in objGraphDataTable.Rows)
                        {
                            //Add data to arraylist and return the same.
                            objMeterAnalysisChartData = AddItemToList(objGraphDataRow["Read_Date"].ToString());

                            //Check for selected Data Type and assign the value.
                            //switch (cmbDatatype.Text)
                            //{
                                if(cmbDatatype.Text == typeAvgDailyWin)      // "AVG DAILY WIN":
                                    fGraphValue = (float)Convert.ToInt32(objGraphDataRow["AVGDAILYWIN"]); 
                                else if(cmbDatatype.Text == typeHandle ) // "HANDLE":
                                    fGraphValue = (float)Convert.ToInt64(objGraphDataRow["Bet"]); 
                                 else if(cmbDatatype.Text ==  typeCasinoWin ) // "CASINO WIN":
                                    fGraphValue = (float)Convert.ToInt64(objGraphDataRow["CasinoWin"]); 
                                 else if(cmbDatatype.Text == typeGamesBet ) // "GAMES BET":
                                    fGraphValue = (float)Convert.ToInt64(objGraphDataRow["GamesBet"]); 
                                else
                                    fGraphValue = (float)Convert.ToInt32(objGraphDataRow["AVGDAILYWIN"]); 
                            //}
                            
                            // grpBySUBCOMPANY, grpByREGION, grpByAREA, grpByDISTRICT, grpBySITE, grpByZONE, 
                            // grpByPOSITION, grpByCATEGORY, grpByTYPE, grpByOPERATOR, grpByDEPOT, grpByGAMETITLE, grpByGAMEASSET, grpByASSET

                            //switch (cmbGroupBy.Text)
                            //{
                                if(cmbGroupBy.Text == grpByGAMETITLE ) // "GAMETITLE":
                                {
                                    //Check for the selected row in grid and add machine data for matching records.
                                    if (dtMainGrid.Rows[iSelectedIndexMeterDetails]["Game Title"].ToString() == objGraphDataRow["Game Title"].ToString())
                                        objMeterAnalysisChartData.MachineValue = objMeterAnalysisChartData.MachineValue + fGraphValue;
                                    }
                                else if(cmbGroupBy.Text == grpByGAMEASSET) // "GAMEASSET":
                                {                                    
                                     //Check for the selected row in grid and add machine data for matching records.
                                    if ((int)dtMainGrid.Rows[iSelectedIndexMeterDetails]["Installation"] == (int)objGraphDataRow["Installation"])
                                        objMeterAnalysisChartData.MachineValue = objMeterAnalysisChartData.MachineValue + fGraphValue;
                                 }
                                else if(cmbGroupBy.Text == grpByASSET) // "ASSET":
                                    {
                                     //Check for the selected row in grid and add machine data for matching records.
                                    if (dtMainGrid.Rows[iSelectedIndexMeterDetails]["Asset"].ToString() == objGraphDataRow["Asset"].ToString())
                                        objMeterAnalysisChartData.MachineValue = objMeterAnalysisChartData.MachineValue + fGraphValue;
                                    }
                                else if(cmbGroupBy.Text == grpBySUBCOMPANY) // "SUB COMPANY":
                                    {
                                     //Check for the selected row in grid and add machine data for matching records.
                                    if (dtMainGrid.Rows[iSelectedIndexMeterDetails]["Sub Company"].ToString() == objGraphDataRow["Sub Company"].ToString())
                                        objMeterAnalysisChartData.MachineValue = objMeterAnalysisChartData.MachineValue + fGraphValue;
                                    }
                                else if(cmbGroupBy.Text == grpByREGION) // "REGION":
                                    {
                                     //Check for the selected row in grid and add machine data for matching records.
                                    if (dtMainGrid.Rows[iSelectedIndexMeterDetails]["Region"].ToString() == objGraphDataRow["Region"].ToString())
                                        objMeterAnalysisChartData.MachineValue = objMeterAnalysisChartData.MachineValue + fGraphValue;
                                    }
                                else if(cmbGroupBy.Text == grpByAREA) // "AREA":
                                    {
                                     //Check for the selected row in grid and add machine data for matching records.
                                    if (dtMainGrid.Rows[iSelectedIndexMeterDetails]["Area"].ToString() == objGraphDataRow["Area"].ToString())
                                        objMeterAnalysisChartData.MachineValue = objMeterAnalysisChartData.MachineValue + fGraphValue;
                                    }
                                else if(cmbGroupBy.Text == grpByDISTRICT) // "DISTRICT":
                                    {
                                     //Check for the selected row in grid and add machine data for matching records.
                                    if (dtMainGrid.Rows[iSelectedIndexMeterDetails]["District"].ToString() == objGraphDataRow["District"].ToString())
                                        objMeterAnalysisChartData.MachineValue = objMeterAnalysisChartData.MachineValue + fGraphValue;
                                    }
                                else if(cmbGroupBy.Text == grpByZONE) // "ZONE":
                                    {
                                     //Check for the selected row in grid and add machine data for matching records.
                                    if (dtMainGrid.Rows[iSelectedIndexMeterDetails]["Zone"].ToString() == objGraphDataRow["Zone"].ToString())
                                        if (dtMainGrid.Rows[iSelectedIndexMeterDetails]["Site"].ToString() == objGraphDataRow["Site"].ToString())
                                            objMeterAnalysisChartData.MachineValue = objMeterAnalysisChartData.MachineValue + fGraphValue;
                                    }
                                else if(cmbGroupBy.Text == grpByPOSITION) // "POSITION":
                                    {
                                     //Check for the selected row in grid and add machine data for matching records.
                                    if (dtMainGrid.Rows[iSelectedIndexMeterDetails]["Position"].ToString() == objGraphDataRow["Position"].ToString())
                                        if (dtMainGrid.Rows[iSelectedIndexMeterDetails]["Site"].ToString() == objGraphDataRow["Site"].ToString())
                                            objMeterAnalysisChartData.MachineValue = objMeterAnalysisChartData.MachineValue + fGraphValue;
                                    }
                                else if(cmbGroupBy.Text == grpByOPERATOR) // "OPERATOR":
                                    {
                                     //Check for the selected row in grid and add machine data for matching records.
                                    if (dtMainGrid.Rows[iSelectedIndexMeterDetails]["Operator"].ToString() == objGraphDataRow["Operator"].ToString())
                                        objMeterAnalysisChartData.MachineValue = objMeterAnalysisChartData.MachineValue + fGraphValue;
                                    }
                                else if(cmbGroupBy.Text == grpByDEPOT) // "DEPOT":
                                    {
                                     //Check for the selected row in grid and add machine data for matching records.
                                    if (dtMainGrid.Rows[iSelectedIndexMeterDetails]["Depot"].ToString() == objGraphDataRow["Depot"].ToString())
                                        objMeterAnalysisChartData.MachineValue = objMeterAnalysisChartData.MachineValue + fGraphValue;
                                    }
                                else if(cmbGroupBy.Text ==  grpBySITE) // "SITE":
                                    {
                                     //Check for the selected row in grid and add machine data for matching records.
                                    if (dtMainGrid.Rows[iSelectedIndexMeterDetails]["Site"].ToString() == objGraphDataRow["Site"].ToString())
                                        objMeterAnalysisChartData.MachineValue = objMeterAnalysisChartData.MachineValue + fGraphValue;
                                    }
                                else if(cmbGroupBy.Text == grpByTYPE) // "TYPE":
                                    {
                                     //Check for the selected row in grid and add machine data for matching records.
                                    if (dtMainGrid.Rows[iSelectedIndexMeterDetails]["Type"].ToString() == objGraphDataRow["Type"].ToString())
                                        objMeterAnalysisChartData.MachineValue = objMeterAnalysisChartData.MachineValue + fGraphValue;
                                    }
                                else if(cmbGroupBy.Text == grpByCATEGORY) // "CATEGORY":
                                {
                                    //Check for the selected row in grid and add machine data for matching records.
                                    if (dtMainGrid.Rows[iSelectedIndexMeterDetails]["Category"].ToString() == objGraphDataRow["Category"].ToString())
                                        objMeterAnalysisChartData.MachineValue = objMeterAnalysisChartData.MachineValue + fGraphValue;
                               }
                            //}
                            //Add Group data for all records.
                            objMeterAnalysisChartData.GroupValue = objMeterAnalysisChartData.GroupValue + fGraphValue;
                            objMeterAnalysisChartData.GroupQuantity = objMeterAnalysisChartData.GroupQuantity + 1;
                        }
                        //Clear the null values from the collection.
                        objCollection.TrimToSize();

                        //Object to store graph data.
                        object[,] objGraphData = new object[2, objCollection.Count];

                        //Iterate through the arraylist and calculate the average for Machine & Group data.
                        AddSeries();
                        //axMSChartMeterGraph.Series[0].Points.Clear();
                        //axMSChartMeterGraph.Series[1].Points.Clear();

                        axMSChartMeterGraph.Series[strCompany].Points.Clear();

                        foreach (MeterAnalysisChartData objMACData in objCollection)
                        {
                            objGraphData[0, iCount] = objMACData.MachineValue;
                            axMSChartMeterGraph.Series[strCompany].Points.AddY(objGraphData[0, iCount]);
                            if (objMACData.GroupQuantity > 0)
                            {
                                objGraphData[1, iCount] = Math.Round(Convert.ToDecimal((objMACData.GroupValue / objMACData.GroupQuantity)), 2);
                                axMSChartMeterGraph.Series[strAvg].Points.AddY(objGraphData[1, iCount]);
                                //if (cmbDatatype.Text == "AVG DAILY WIN")
                                //    objGraphData[1, iCount] = objMACData.GroupValue / objMACData.GroupQuantity;
                                //else
                                //    objGraphData[1, iCount] = objMACData.GroupValue;
                            }
                            else
                            {
                                objGraphData[1, iCount] = 0;
                                axMSChartMeterGraph.Series[strAvg].Points.AddY(0);
                            }
                            iCount++;
                        }

                        //Set the label text of the legend for the Graph.
                        string strLegendLabelSelected = "";
                        string strLegendLabelAll = "";

                        // grpBySUBCOMPANY, grpByREGION, grpByAREA, grpByDISTRICT, grpBySITE, grpByZONE, 
                        // grpByPOSITION, grpByCATEGORY, grpByTYPE, grpByOPERATOR, grpByDEPOT, grpByGAMETITLE, grpByGAMEASSET, grpByASSET
                        
                        //Check for selected Group Type.
                        //switch (cmbGroupBy.Text)
                        //{
                            if(cmbGroupBy.Text == grpByGAMETITLE)   // "GAMETITLE":
                            {
                                strLegendLabelSelected = hdrGameTitle;    // "Game Title";    
                                strLegendLabelAll = this.GetResourceTextByKey("Key_GameTitles1");       //"Game Titles";    
                            }
                           else if(cmbGroupBy.Text == grpByGAMEASSET)   //"GAMEASSET":
                            {
                                strLegendLabelSelected = this.GetResourceTextByKey("Key_GameAsset");  //"Game Asset";    
                                strLegendLabelAll = this.GetResourceTextByKey("Key_GameAssets");  //"Game Assets";       
                           }
                           else if(cmbGroupBy.Text == grpByASSET)   // "ASSET":
                            {
                                strLegendLabelSelected = hdrAsset;  //"Asset"; 
                                strLegendLabelAll = this.GetResourceTextByKey("Key_Assets");  //"Assets";     
                           }
                           else if(cmbGroupBy.Text == grpBySUBCOMPANY)   // "SUB COMPANY":
                            {
                                strLegendLabelSelected = hdrSubCompany;  //"Sub Company";   
                                strLegendLabelAll = this.GetResourceTextByKey("Key_SubCompanies");  //"Sub Companies";   
                            }
                           else if(cmbGroupBy.Text == grpByREGION)   // "REGION":
                            {
                                strLegendLabelSelected = hdrRegion;  //"Region";       
                                strLegendLabelAll = this.GetResourceTextByKey("Key_Regions");  //"Regions";  
                           }
                           else if(cmbGroupBy.Text == grpByAREA)   // "AREA":
                            {
                                strLegendLabelSelected = hdrArea;  //"Area";    
                                strLegendLabelAll = this.GetResourceTextByKey("Key_Areas");  //"Areas";     
                            }
                           else if(cmbGroupBy.Text == grpByDISTRICT)   // "DISTRICT":
                            {
                                strLegendLabelSelected = hdrDistrict;  //"District";      
                                strLegendLabelAll = this.GetResourceTextByKey("Key_Districts");  //"Districts";  
                           }
                           else if(cmbGroupBy.Text == grpByZONE)   // "ZONE":
                            {
                                strLegendLabelSelected = hdrZone;  //"Zone";   
                                strLegendLabelAll = this.GetResourceTextByKey("Key_Zones");  //"Zones";       
                           }
                           else if(cmbGroupBy.Text == grpByPOSITION)   // "POSITION":
                            {
                                strLegendLabelSelected = hdrPosition;  //"Position";  
                                strLegendLabelAll = this.GetResourceTextByKey("Key_Positions");  //"Positions";    
                           }
                           else if(cmbGroupBy.Text == grpByOPERATOR)   // "OPERATOR":
                            {
                                strLegendLabelSelected = hdrOperator;  //"Operator";   
                                strLegendLabelAll = this.GetResourceTextByKey("Key_Operators");  //"Operators";     
                           }
                           else if(cmbGroupBy.Text == grpByDEPOT)   // "DEPOT":
                            {
                                strLegendLabelSelected = hdrDepot;  //"Depot";      
                                strLegendLabelAll = this.GetResourceTextByKey("Key_Depots");  //"Depots";   
                           }
                           else if(cmbGroupBy.Text == grpBySITE)   // "SITE":
                            {
                                strLegendLabelSelected = hdrSite; //"Site";    
                                strLegendLabelAll = this.GetResourceTextByKey("Key_Sites");  //"Sites";     
                           }
                           else if(cmbGroupBy.Text ==  grpByCATEGORY)   // "CATEGORY":
                            {
                                strLegendLabelSelected = hdrCategory; //"Category";    
                                strLegendLabelAll = this.GetResourceTextByKey("Key_Categories");  //"Categories";  
                           }
                            else if (cmbGroupBy.Text == grpByTYPE)   // "TYPE":
                            {
                                strLegendLabelSelected = hdrType;  //"Type"; 
                                strLegendLabelAll = this.GetResourceTextByKey("Key_Types");     //"Types";   
                           }
                        //}
                        //Chart Graph.
                        //Set the required properties for the chart.

                        if (objGraphData.Length > 0)
                        {
                            axMSChartMeterGraph.Visible = true;
                            if (!(objGraphData.Length > 2))
                            {
                                for (int i = 0; i <= 1; i++)
                                {
                                    axMSChartMeterGraph.Series[i].MarkerStyle = (i == 1) ? MarkerStyle.Diamond : MarkerStyle.Cross;
                                    axMSChartMeterGraph.Series[i].MarkerSize = 15;
                                    axMSChartMeterGraph.Series[i].MarkerColor = (i == 1) ? Color.Yellow : Color.DarkRed;
                                    axMSChartMeterGraph.Series[i].MarkerBorderColor = Color.Green;
                                    axMSChartMeterGraph.Series[i].MarkerBorderWidth = 1;
                                }

                            }
                            blnbtnZoomGraphVisible = true;
                            axMSChartMeterGraph.Series[strCompany].ChartType = SeriesChartType.Line;
                            axMSChartMeterGraph.Series[strAvg].ChartType = SeriesChartType.Line;
                            axMSChartMeterGraph.Series[strCompany].IsValueShownAsLabel = true;
                            axMSChartMeterGraph.Series[strAvg].IsValueShownAsLabel = true;
                            axMSChartMeterGraph.Series[strCompany].LegendText = this.GetResourceTextByKey("Key_Selected") + " " + strLegendLabelSelected;        // "Selected "
                            axMSChartMeterGraph.Series[strAvg].LegendText = this.GetResourceTextByKey("Key_AvgOfAll") + " " + strLegendLabelAll;      // "Avg of all " 
                            //Set the legend details.
                            axMSChartMeterGraph.ChartAreas["Default"].AxisY2.Enabled = System.Windows.Forms.DataVisualization.Charting.AxisEnabled.True;
                            double iColumnCount = 0.5;
                            axMSChartMeterGraph.ChartAreas["Default"].AxisX.CustomLabels.Clear();
                            //Assign the Column label for the Graph.
                            foreach (MeterAnalysisChartData objMACData in objCollection)
                            {
                                axMSChartMeterGraph.ChartAreas["Default"].AxisX.CustomLabels.Add(iColumnCount, 1 + iColumnCount, Convert.ToDateTime(objMACData.ChartDataLabel).ToString("dd MMM yy"));
                                iColumnCount += 1;
                            }

                        }
                        #region DeadCode
                        //if (objGraphData.Length > 2)
                        //{
                        //    //Make the Graph visible.
                        //    axMSChartMeterGraph.Visible = true;

                        //    blnbtnZoomGraphVisible = true;
                        //    //Set the required properties for the chart.
                        //    axMSChartMeterGraph.chartType = MSChart20Lib.VtChChartType.VtChChartType2dLine;
                        //    axMSChartMeterGraph.Stacking = false;
                        //    axMSChartMeterGraph.ColumnCount = (short)objGraphData.Length;
                        //    axMSChartMeterGraph.RowCount = 2;
                        //    //Assign the data.
                        //    axMSChartMeterGraph.ChartData = objGraphData;

                        //    //Set the legend details.
                        //    axMSChartMeterGraph.DataGrid.set_RowLabel(1, 1, "Selected " + strLegendLabelSelected);
                        //    axMSChartMeterGraph.DataGrid.set_RowLabel(2, 1, "Avg of all " + strLegendLabelAll);
                        //    axMSChartMeterGraph.Legend.Location.LocationType = MSChart20Lib.VtChLocationType.VtChLocationTypeBottom;
                        //    short iColumnCount = 1;
                        //    //Assign the Column label for the Graph.
                        //    foreach (MeterAnalysisChartData objMACData in objCollection)
                        //    {
                        //        axMSChartMeterGraph.DataGrid.set_ColumnLabel(iColumnCount, 1, Convert.ToDateTime(objMACData.ChartDataLabel).ToString("dd MMM yy"));
                        //        iColumnCount++;
                        //    }
                        //}
                        //else
                        //{
                        //    axMSChartMeterGraph.Visible = false;
                        //    axMSChart1.Visible = true;
                        //    blnbtnZoomGraphVisible = false;
                        //    //Set the required properties for the chart.
                        //    axMSChart1.chartType = MSChart20Lib.VtChChartType.VtChChartType2dLine;
                        //    axMSChart1.Stacking = false;
                        //    axMSChart1.ColumnCount = (short)objGraphData.Length;
                        //    axMSChart1.RowCount = 2;
                        //    //Assign the data.
                        //    axMSChart1.ChartData = objGraphData;

                        //    //Set the legend details.
                        //    axMSChart1.DataGrid.set_RowLabel(1, 1, "Selected " + strLegendLabelSelected);
                        //    axMSChart1.DataGrid.set_RowLabel(2, 1, "Avg of All " + strLegendLabelAll);
                        //    axMSChart1.Legend.Location.LocationType = MSChart20Lib.VtChLocationType.VtChLocationTypeBottom;

                        //    short iColumnCount = 1;
                        //    //Assign the Column label for the Graph.
                        //    foreach (MeterAnalysisChartData objMACData in objCollection)
                        //    {
                        //        if (!string.IsNullOrEmpty(objMACData.ChartDataLabel))
                        //        {
                        //            axMSChart1.DataGrid.set_ColumnLabel(iColumnCount, 1, Convert.ToDateTime(objMACData.ChartDataLabel).ToString("dd MMM yy"));
                        //            iColumnCount++;    
                        //        }
                        //    }
                        //} 
                        #endregion
                        //Persist with graph data.
                        MeterAnalysisChartData.objCollection = objCollection;
                        MeterAnalysisChartData.objGraphData = objGraphData;
                        MeterAnalysisChartData.strLegendLabelAll = strLegendLabelAll;
                        MeterAnalysisChartData.strLegendLabelSelected = strLegendLabelSelected;

                    }
                }

            }
            catch (Exception exLoadGraphData)
            {
                LogManager.WriteLog("Error in loading Graph." + "-Error Message-" + exLoadGraphData.Message, LogManager.enumLogLevel.Error);
                throw exLoadGraphData;
            }
        }

        private void AddSeries()
        {
            try
            {
                axMSChartMeterGraph.Series.Clear();
                Series sc_company = new Series();
                Series sc_Avg = new Series();

                sc_company.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(26)))), ((int)(((byte)(59)))), ((int)(((byte)(105)))));
                sc_company.BorderWidth = 3;
                sc_company.ChartArea = "Default";
                sc_company.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
                sc_company.Color = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(65)))), ((int)(((byte)(140)))), ((int)(((byte)(240)))));
                sc_company.Legend = "Default";
                sc_company.MarkerSize = 8;
                sc_company.MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Circle;
                sc_company.Name = strCompany;
                sc_company.ShadowColor = System.Drawing.Color.Black;
                sc_company.ShadowOffset = 2;
                sc_company.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;
                sc_company.YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;
                sc_Avg.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(26)))), ((int)(((byte)(59)))), ((int)(((byte)(105)))));
                sc_Avg.BorderWidth = 3;
                sc_Avg.ChartArea = "Default";
                sc_Avg.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
                sc_Avg.Color = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(224)))), ((int)(((byte)(64)))), ((int)(((byte)(10)))));
                sc_Avg.Legend = "Default";
                sc_Avg.MarkerSize = 9;
                sc_Avg.MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Diamond;
                sc_Avg.Name = strAvg;
                sc_Avg.ShadowColor = System.Drawing.Color.Black;
                sc_Avg.ShadowOffset = 2;
                sc_Avg.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;
                sc_Avg.YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;
                axMSChartMeterGraph.Series.Add(sc_company);
                axMSChartMeterGraph.Series.Add(sc_Avg);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
        /// <summary>
        /// Add MeterAnalysisChartData object to the Arraylist collection.
        /// </summary>
        /// <Author>Renjish N</Author>
        /// <DateCreated>16-May-2008</DateCreated>
        /// <Parameters> 
        /// <param name="sText"></param>
        /// </Parameters>
        /// <returns>Object of type MeterAnalysisChartData</returns>
        /// Method Revision History
        ///
        /// Author             Date              Description
        /// ---------------------------------------------------
        /// Renjish N          16-May-2008       Intial Version 
        private MeterAnalysisChartData AddItemToList(string sText)
        {
            int iDataIndex = 0;
            try
            {
                //Declare variables.
                MeterAnalysisChartData objMeterAnalysisChartData = new MeterAnalysisChartData();
                int iCollectionIndex = 0;
                bool bDataExists = false;
                if (!string.IsNullOrEmpty(sText))
                {
                    //Remove time part of the datetime string.
                    sText = sText.Substring(0, sText.IndexOf(" "));

                    if (objCollection.Count > 0)
                    {
                        //Iterate through collection if data already exists.
                        foreach (MeterAnalysisChartData objMACData in objCollection)
                        {
                            if (objMACData.ChartDataLabel == sText)
                            {
                                bDataExists = true;
                                break;
                            }
                            else
                                bDataExists = false;
                            iCollectionIndex++;
                        }
                    }
                }
                //If data does not exist add new.
                if (!bDataExists && !string.IsNullOrEmpty(sText))
                {
                    objMeterAnalysisChartData.ChartDataLabel = sText;
                    objCollection.Add(objMeterAnalysisChartData);
                    iDataIndex = objCollection.Count - 1;
                }
                else
                    iDataIndex = iCollectionIndex;
            }
            catch (Exception exAddItemToList)
            {
                LogManager.WriteLog("Error in Adding Item to Arraylist." + "-Error Message-" + exAddItemToList.Message, LogManager.enumLogLevel.Error);
                throw exAddItemToList;
            }

            if (objCollection.Count > 0)
                return (MeterAnalysisChartData)objCollection[iDataIndex];
            else
                return new MeterAnalysisChartData();

        }

        /// <summary>
        /// Reload the graph every time a selected row is changed in the Grid.
        /// </summary>
        /// <Author>Renjish N</Author>
        /// <DateCreated>16-May-2008</DateCreated>
        /// <Parameters> 
        /// <param name="sender"></param>
        /// <param name="e"></param> 
        /// </Parameters>
        /// <returns>Nothing</returns>
        ///
        /// Method Revision History
        ///
        /// Author             Date              Description
        /// ---------------------------------------------------
        /// Renjish N          16-May-2008       Intial Version        
        private void dgMeterDetails_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
                if (e.RowIndex == 0)
                {
                    if (dgMeterDetails.Rows.Count > iSelectedIndexMeterDetails)
                    {
                        dgMeterDetails.Rows[iSelectedIndexMeterDetails].Selected = true;
                    }
                }
                else
                {
                    //Set the selected row value.
                    iSelectedIndexMeterDetails = e.RowIndex;
                    if (!blnbtnProcessClicked)
                    {
                        //Reload Graph with the selected data.
                        LoadGraphData();
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            finally
            {
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
            }
        }

        /// <summary>
        /// Reload the graph every time a selected value is changed in the Data Type Combo Box.
        /// </summary>
        /// <Author>Renjish N</Author>
        /// <DateCreated>26-May-2008</DateCreated>
        /// <Parameters> 
        /// <param name="sender"></param>
        /// <param name="e"></param> 
        /// </Parameters>
        /// <returns>Nothing</returns>
        ///
        /// Method Revision History
        ///
        /// Author             Date              Description
        /// ---------------------------------------------------
        /// Renjish N          26-May-2008       Intial Version 
        private void cmbDatatype_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
                if (!blnucBMCMeterAnalysisLoading)
                {

                    //Reload Graph.
                    LoadGraphData();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            finally
            {
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
            }
        }

        /// <summary>
        /// Reload the graph every time a selected value is changed in the radio button list.
        /// </summary>
        /// <Author>Renjish N</Author>
        /// <DateCreated>27-May-2008</DateCreated>
        /// <Parameters> 
        /// <param name="sender"></param>
        /// <param name="e"></param> 
        /// </Parameters>
        /// <returns>Nothing</returns>
        ///
        /// Method Revision History
        ///
        /// Author             Date              Description
        /// ---------------------------------------------------
        /// Renjish N          27-May-2008       Intial Version 
        private void rdnDay_CheckedChanged(object sender, EventArgs e)
        {
            try
            {

                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
                if (!blnucBMCMeterAnalysisLoading && rdnDay.Checked)
                {
                    LoadMainGrid(DatatoGet.Graph);
                    //Reload Graph.
                    LoadGraphData();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            finally
            {

                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
            }
        }

        /// <summary>
        /// Reload the graph every time a selected value is changed in the radio button list.
        /// </summary>
        /// <Author>Renjish N</Author>
        /// <DateCreated>27-May-2008</DateCreated>
        /// <Parameters> 
        /// <param name="sender"></param>
        /// <param name="e"></param> 
        /// </Parameters>
        /// <returns>Nothing</returns>
        ///
        /// Method Revision History
        ///
        /// Author             Date              Description
        /// ---------------------------------------------------
        /// Renjish N          27-May-2008       Intial Version 
        private void rdnWeek_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
                if (!blnucBMCMeterAnalysisLoading && rdnWeek.Checked)
                {
                    LoadMainGrid(DatatoGet.Graph);
                    //Reload Graph.
                    LoadGraphData();
                }
            }
            catch (Exception ex)
            { ExceptionManager.Publish(ex); }
            finally
            { System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default; }
        }

        /// <summary>
        /// Reload the graph every time a selected value is changed in the radio button list.
        /// </summary>
        /// <Author>Renjish N</Author>
        /// <DateCreated>27-May-2008</DateCreated>
        /// <Parameters> 
        /// <param name="sender"></param>
        /// <param name="e"></param> 
        /// </Parameters>
        /// <returns>Nothing</returns>
        ///
        /// Method Revision History
        ///
        /// Author             Date              Description
        /// ---------------------------------------------------
        /// Renjish N          27-May-2008       Intial Version 
        private void rdnPeriod_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
                if (!blnucBMCMeterAnalysisLoading && rdnPeriod.Checked)
                {
                    LoadMainGrid(DatatoGet.Graph);
                    //Reload Graph.
                    LoadGraphData();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            finally
            {
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
            }
        }
        #endregion "Graph Data"

        #region "Button click Event handlers"
        /// <summary>
        /// Loads the graph data
        /// </summary>
        /// <Author>Madhusudhanan</Author>
        /// <DateCreated>13-5-08</DateCreated>
        /// <Parameters></Parameters>
        /// <returns></returns>
        private void btnProcess_Click(object sender, EventArgs e)
        {
            try
            {

                //Load the Grid.
                LoadGrid();
                //Make the Graph visible.
                axMSChartMeterGraph.Visible = false;

                blnbtnZoomGraphVisible = false;


                if (dgMeterDetails.Rows.Count > 0)
                {
                    //Load the Graph.
                    LoadGraphData();
                }


            }
            catch (Exception ex)
            { ExceptionManager.Publish(ex); }
            finally
            { System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default; }
        }

        /// <summary>
        /// Export button click event handler
        /// </summary>
        /// <Author>Madhusudhanan</Author>
        /// <DateCreated>13-5-08</DateCreated>
        /// <Parameters> </Parameters>
        /// <returns></returns>
        private void btnExport_Click(object sender, EventArgs e)
        {
            string strFileName = string.Empty;
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
            DataSet dsExportData = new DataSet();
            DataTable dtExportData = new DataTable();
            try
            {
                if (dtpFrom.Value > dtpTo.Value)
                    return;
                else if ((12 * (dtpTo.Value.Year - dtpFrom.Value.Year)) + (dtpTo.Value.Month - dtpFrom.Value.Month) > ToInteger(ConfigurationManager.AppSettings["MaxMonths"]))
                    return;
                //dtExportData.DataSet = dsExportData.Tables.Add("ExportTable");
                LoadMainGrid(DatatoGet.Grid);
                dtExportData = dtMainGrid;
                if (dtExportData.Rows.Count != 0)
                {
                    DataExportUtility objExportUtility = new DataExportUtility();
                    FileDialog fdSaveAs = new SaveFileDialog();
                    //get the path to be saved from the user
                    fdSaveAs.Filter = "Excel Worksheets|*.xls";
                    if (fdSaveAs.ShowDialog() == DialogResult.OK)
                    {
                        strFileName = fdSaveAs.FileName;
                    }
                    if (strFileName != "")
                    {
                        foreach (DataColumn col in dtExportData.Columns)
                        {
                            col.ColumnName = col.ColumnName.Replace("_", " ");
                        }
                        objExportUtility.ExportDataToExcel(dtExportData.DataSet, ExportStyle.SheetWise, strFileName);
                    }
                }
                else
                {
                    MessageBox.Show(this.GetResourceTextByKey(1, "MSG_NO_RECORDS_TO_EXPORT"), this.GetResourceTextByKey(1, "MSG_APP_TITLE"));
                }

            }
            catch (Exception ex)
            { ExceptionManager.Publish(ex); }
            finally
            { System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default; }
        }

        /// <summary>
        /// Print button click handler
        /// </summary>
        /// <Author>Madhusudhanan</Author>
        /// <DateCreated>13-5-08</DateCreated>
        /// <Parameters> </Parameters>
        /// <returns></returns>
        private void btnPrint_Click(object sender, EventArgs e)
        {
        }
        /// <summary>
        /// Show or Hide tree button click handler
        /// </summary>
        /// <Author>Madhusudhanan</Author>
        /// <DateCreated>31-5-08</DateCreated>
        /// <Parameters> </Parameters>
        /// <returns></returns>
        private void btnShowTree_Click(object sender, EventArgs e)
        {
            try
            {
                if (label10.Visible)
                {
                    panel1.Location = panel2.Location;
                    panel1.Width = panel2.Width + panel1.Width + 8;
                    label10.Visible = false;
                    panel2.Visible = false;
                    btnShowTree.Text = this.GetResourceTextByKey("Key_TreeRightArrow");    // ">>TREE";
                    //btnShowTree.Location = new Point(btnShowTree.Location.X - 75, btnShowTree.Location.Y);
                    //lblCompany.Location = new Point(lblCompany.Location.X - 50, lblCompany.Location.Y);
                    //label2.Location = new Point(label2.Location.X - 15, label2.Location.Y);
                    //label3.Location = new Point(label3.Location.X, label3.Location.Y);
                    //label4.Location = new Point(label4.Location.X + 25, label4.Location.Y);
                    //label5.Location = new Point(label5.Location.X + 50, label5.Location.Y);
                    //label6.Location = new Point(label5.Location.X + 75, label5.Location.Y);
                    //txtCompany.Location = new Point(txtCompany.Location.X - 50, txtCompany.Location.Y);
                    //txtSubCompany.Location = new Point(txtSubCompany.Location.X - 25, txtSubCompany.Location.Y);
                    //txtRegion.Location = new Point(txtRegion.Location.X, txtRegion.Location.Y);
                    //txtArea.Location = new Point(txtArea.Location.X + 25, txtArea.Location.Y);
                    //txtDistrict.Location = new Point(txtDistrict.Location.X + 50, txtDistrict.Location.Y);
                    //txtSite.Location = new Point(txtSite.Location.X + 75, txtSite.Location.Y);
                }
                else
                {
                    panel1.Location = new Point(panel2.Location.X + panel2.Width + 8, panel2.Location.Y);
                    panel1.Width = panel1.Width - panel2.Width - 8;
                    label10.Visible = true;
                    panel2.Visible = true;
                    btnShowTree.Text = this.GetResourceTextByKey("Key_TreeLeftArrow");    //"<<TREE";
                    //btnShowTree.Location = new Point(btnShowTree.Location.X + 75, btnShowTree.Location.Y);
                    //lblCompany.Location = new Point(lblCompany.Location.X + 50, lblCompany.Location.Y);
                    //label2.Location = new Point(label2.Location.X + 15, label2.Location.Y);
                    //label3.Location = new Point(label3.Location.X, label3.Location.Y);
                    //label4.Location = new Point(label4.Location.X - 25, label4.Location.Y);
                    //label5.Location = new Point(label5.Location.X - 50, label5.Location.Y);
                    //label6.Location = new Point(label5.Location.X - 75, label5.Location.Y);
                    //txtCompany.Location = new Point(txtCompany.Location.X + 50, txtCompany.Location.Y);
                    //txtSubCompany.Location = new Point(txtSubCompany.Location.X + 25, txtSubCompany.Location.Y);
                    //txtRegion.Location = new Point(txtRegion.Location.X, txtRegion.Location.Y);
                    //txtArea.Location = new Point(txtArea.Location.X - 25, txtArea.Location.Y);
                    //txtDistrict.Location = new Point(txtDistrict.Location.X - 50, txtDistrict.Location.Y);
                    //txtSite.Location = new Point(txtSite.Location.X - 75, txtSite.Location.Y);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            finally
            {
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
            }
        }
        #endregion

        /// <summary>
        /// Assigns the Resource Key names to the controls--Created by kishore sivagnanam
        /// </summary>
        public void SetTagProperty()
        {            
            this.chkActive.Tag = "Key_ActiveAssets";
            this.chkActiveSites.Tag = "Key_ActiveSites";
            this.txtSite.Tag = "Key_ALLCaps";
            this.txtDistrict.Tag = "Key_ALLCaps";
            this.txtArea.Tag = "Key_ALLCaps";
            this.txtRegion.Tag = "Key_ALLCaps";
            this.txtSubCompany.Tag = "Key_ALLCaps";
            this.txtCompany.Tag = "Key_ALLCaps";
            this.label3.Tag = "Key_AreaColon";
            this.rdnDay.Tag = "Key_ByDay";
            this.rdnPeriod.Tag = "Key_ByPeriod";
            this.rdnWeek.Tag = "Key_ByWeek";
            this.label6.Tag = "Key_CategoryColon";
            this.lblCompany.Tag = "Key_CompanyColon";
            this.lblCriteria.Tag = "Key_CriteriaColon";
            this.label9.Tag = "Key_DataTypeColon";
            this.label13.Tag = "Key_DepotColon";
            this.label4.Tag = "Key_DistrictColon";
            this.btnExport.Tag = "Key_Export";
            this.label12.Tag = "Key_FromColon";
            this.label7.Tag = "Key_GameColon";
            this.lblGroupBy.Tag = "Key_GroupByColon";
            this.rdnHold.Tag = "Key_HoldPercent";
            this.label8.Tag = "Key_ManufacturerColon";
            this.label14.Tag = "Key_OperatorColon";
            this.label10.Tag = "Key_OrgHierarchicalView";
            this.rdnPayout.Tag = "Key_PayoutPercent";
            this.btnPrint.Tag = "Key_Print";
            this.btnProcess.Tag = "Key_Process";
            this.lblDataType.Tag = "Key_RecordsColon";
            this.label2.Tag = "Key_RegionColon";
            this.label5.Tag = "Key_SiteColon";
            this.label1.Tag = "Key_SubCompanyColon";
            this.label11.Tag = "Key_ToColon";
            this.lblType.Tag = "Key_TypeColon";
            this.btnShowTree.Tag = "Key_TreeLeftArrow";

        }

        private void dgMeterDetails_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {

            if (e.RowIndex == -1 && dgMeterDetails.Rows.Count > 2)
            {
                object[] values = new object[dgMeterDetails.Rows[0].Cells.Count];
                try
                {
                    for (int i = 0; i < dgMeterDetails.Rows[0].Cells.Count; i++)
                    {
                        if (dgMeterDetails.Rows[0].Cells[i] != null)
                            values[i] = dgMeterDetails.Rows[0].Cells[i].Value;
                    }
                    dgMeterDetails.Rows.RemoveAt(0);
                    if (LastSort == dgMeterDetails.Columns[e.ColumnIndex].HeaderText)
                    {
                        if (direction == ListSortDirection.Descending)
                        {
                            dgMeterDetails.Sort(dgMeterDetails.Columns[e.ColumnIndex], ListSortDirection.Ascending);
                            direction = ListSortDirection.Ascending;
                        }
                        else
                        {
                            dgMeterDetails.Sort(dgMeterDetails.Columns[e.ColumnIndex], ListSortDirection.Descending);
                            direction = ListSortDirection.Descending;
                        }
                    }
                    else
                    {
                        LastSort = dgMeterDetails.Columns[e.ColumnIndex].HeaderText;
                        dgMeterDetails.Sort(dgMeterDetails.Columns[e.ColumnIndex], ListSortDirection.Ascending);
                        direction = ListSortDirection.Ascending;
                    }
                }
                catch (Exception ex)
                {
                    LogManager.WriteLog(ex.Message, LogManager.enumLogLevel.Error);
                }

                dgMeterDetails.Rows.Insert(0, values);



            }
        }

        private void chkActiveSites_CheckedChanged(object sender, EventArgs e)
        {
            FillSitesViewer();
        }

        /// <summary>
        /// Set tooltip to the Windows Controls 
        /// </summary>
        /// <param name="control"></param>
        /// <param name="length"></param>
        private void SetToolTip(Control control, int length)
        {
            String caption = "" + control.Text;
            ToolTip1.SetToolTip(control, (caption.Trim().Length > length) ? caption : "");
            ToolTip1.AutoPopDelay = 2000;
            ToolTip1.InitialDelay = 0;
            ToolTip1.ReshowDelay = 100;
            ToolTip1.ShowAlways = true;
        }

        private void txtSite_MouseHover(object sender, EventArgs e)
        {
            SetToolTip((TextBox)sender, 15);
        }

        private void cmbRecordCount_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbCriteria.Enabled = cmbRecordCount.SelectedItem.ToString() != allCapsText;      //"ALL";

        }
        //Restricted future date selection.
        private void dtpFrom_ValueChanged(object sender, EventArgs e)
        {
            if (dtpFrom.Value.Date > DateTime.Now.Date || dtpFrom.Value.Date > dtpTo.Value.Date)
                dtpFrom.Value = dtpTo.Value;
        }
        private void dtpTo_ValueChanged(object sender, EventArgs e)
        {
            if (dtpTo.Value.Date > DateTime.Now.Date || dtpFrom.Value.Date > dtpTo.Value.Date)
                dtpTo.Value = dtpFrom.Value;
        }

        void FindRegion(TreeNode TreeRegion, ref int retval)
        {
            if (TreeRegion != null && TreeRegion.Parent != null)
            {
                if (!TreeRegion.Parent.Name.StartsWith("RE"))
                    FindRegion(TreeRegion.Parent, ref retval);
                else
                    retval = Convert.ToInt32("0" + TreeRegion.Parent.Name.Split('#')[1]);
            }
        }

        private void dgMeterDetails_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            dgMeterDetails.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void dgMeterDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dgMeterDetails.SelectionMode = DataGridViewSelectionMode.CellSelect;
        }


        private void dgMeterDetails_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            dgMeterDetails.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void dgMeterDetails_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            dgMeterDetails.SelectionMode = DataGridViewSelectionMode.CellSelect;
        }

      
    }
}