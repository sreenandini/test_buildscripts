using System;
using System.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;
using BMC.Common.LogManagement;
using BMC.Guardian.Business;
using BMC.Guardian.DBHelper;
using BMC.Guardian.Transport;
using ImageControl = System.Web.UI.WebControls.Image;

public partial class MainPage : System.Web.UI.Page
{
    #region Fields

    private int _TimeInterval = 15;
    private int _MaxDaysForUpdates = 2;
    private int _MinDriveSpace = 1500;
    public string UserName = string.Empty;
    #endregion

    #region Construction

    public MainPage()
    {
        int configValue = 15;
        if (int.TryParse(ConfigurationManager.AppSettings["TimeInterval"], out configValue))
            _TimeInterval = configValue;

        configValue = 2;
        if (int.TryParse(ConfigurationManager.AppSettings["MaxDaysForUpdates"], out configValue))
            _MaxDaysForUpdates = configValue;

        configValue = 2;
        if (int.TryParse(ConfigurationManager.AppSettings["MinDriveSpace"], out configValue))
            _MinDriveSpace = configValue;
    }


    #endregion

    #region Private methods
    private void RefreshPageforAutoUpdate()
    {
        try
        {
            if (TreeViewSites.SelectedNode != null)
            {
                string selectedSite = TreeViewSites.SelectedNode.Text;
                string selectedSitePath = TreeViewSites.SelectedNode.ValuePath;
                PopulateSiteOverview();
                SelectSite(selectedSite);
            }
            else
            {
                PopulateSiteOverview();
            }
        }
        catch (Exception ex)
        {
            LogManager.WriteLog("RefreshPageforAutoUpdate::" + ex.Message, LogManager.enumLogLevel.Error);
        }
    }

    private void RefreshPage()
    {
        try
        {
            if (TreeViewSites.SelectedNode != null)
            {
                string selectedSite = TreeViewSites.SelectedNode.Text;
                string selectedSitePath = TreeViewSites.SelectedNode.ValuePath;
                DropDownListFilter.SelectedIndex = 0;
                DropDownListFilterGrid.SelectedIndex = 0;
                PopulateSiteOverview();
                SelectSite(selectedSite);
            }
            else
            {
                DropDownListFilter.SelectedIndex = 0;
                DropDownListFilterGrid.SelectedIndex = 0;
                PopulateSiteOverview();
                SelectSite("Sites");
                TreeViewSites.Nodes[0].Selected = true;
            }
        }
        catch (Exception ex)
        {
            LogManager.WriteLog("RefreshPage::" + ex.Message, LogManager.enumLogLevel.Error);
        }
    }

    private void PopulateSiteOverview()
    {
        GuardianUIDataSet.SiteOverviewDataTable siteOverviewDataTable = null;
        siteOverviewDataTable = CreateSiteOverviewList();
        DataView filteredDataView = FilterSites(siteOverviewDataTable, DropDownListFilter.SelectedIndex);

        PopulateSiteTreeView(filteredDataView);
        PopulateSiteGridView(filteredDataView);
    }

    private void SelectSite(string siteName)
    {
        if (siteName == "Sites")
        {
            MainView.Attributes["style"] = "display: block;";
            DetailsView.Attributes["style"] = "display: none;";
        }
        else
        {
            detailControl.PopulateSite(siteName);
            MainView.Attributes["style"] = "display: none;";
            DetailsView.Attributes["style"] = "display: block;";
        }
    }

    private void PopulateSiteTreeView(DataView dataView)
    {

        TreeViewSites.Nodes.Clear();

        if (dataView.Count <= 0)
        {
            GridMessage.Attributes["style"] = "display: block;";
            TreeViewMessage.Attributes["style"] = "display: block;";
        }
        else
        {
            TreeNode rootNode = new TreeNode("Sites");
            rootNode.Selected = true;
            foreach (DataRowView dataRowView in dataView)
            {
                GuardianUIDataSet.SiteOverviewRow siteOverviewRow = dataRowView.Row as GuardianUIDataSet.SiteOverviewRow;
                TreeNode treeNode = new TreeNode(siteOverviewRow.SiteName);
                if (siteOverviewRow.Status == 0)
                    treeNode.ImageUrl = "~/Images/Success.gif";
                else if (siteOverviewRow.Status == 1)
                    treeNode.ImageUrl = "~/Images/Failure.gif";
                else if (siteOverviewRow.Status == 2)
                    treeNode.ImageUrl = "~/Images/Alert.gif";
                else if (siteOverviewRow.Status == -1)
                    treeNode.ImageUrl = "~/Images/Terminated.gif";
                rootNode.ChildNodes.Add(treeNode);
            }

            TreeViewSites.Nodes.Add(rootNode);
            TreeViewSites.ExpandAll();
            GridMessage.Attributes["style"] = "display: none;";
            TreeViewMessage.Attributes["style"] = "display: none;";
        }
    }

    private void PopulateSiteGridView(DataView dataView)
    {
        GridViewSites.DataSource = dataView;
        GridViewSites.DataBind();
    }

    private GuardianUIDataSet.SiteOverviewDataTable CreateSiteOverviewList()
    {
        if (Session["UserName"] == null || string.IsNullOrEmpty(Session["UserName"].ToString()))
            Response.Redirect("Login.aspx", true);
        GuardianUIDataSet.SiteOverviewDataTable siteOverviewDataTable = new GuardianUIDataSet.SiteOverviewDataTable();
        try
        {
            FIFOStatus fIFOStatus = GuardianBL.GetFIFOStatus();
            lblLstRecExpFrmEnt.Text = fIFOStatus.LastRecord;
            lblRecToPro.Text = fIFOStatus.UnProcessed;
            GuardianDataSet.SiteDetailsDataTable siteDetailsDataTable = GuardianBL.GetSiteList(chkIsActive.Checked);
            int upAndRunningSites = 0;
            Dictionary<string, LastProcessedDetails> dictionaryLastProcessedDetails = new Dictionary<string, LastProcessedDetails>();
            foreach (GuardianDataSet.SiteDetailsRow siteDetailsRow in siteDetailsDataTable)
            {
                GuardianUIDataSet.SiteOverviewRow siteOverviewRow = siteOverviewDataTable.NewSiteOverviewRow();
                siteOverviewRow.Status = 0;
                siteOverviewRow.SiteName = siteDetailsRow.Site_Name;
                siteOverviewRow.Services = GuardianBL.GetAllServiceStatus(siteOverviewRow.SiteName);
                string siteStatus = GuardianDBHelper.GetSiteStatus(siteOverviewRow.SiteName);
                LastProcessedDetails lastProcessedDetails = GuardianBL.GetLastProcessedDetails(siteStatus);
                SiteStatusDetails details = GuardianBL.GetSiteStatusDetails(siteStatus, Convert.ToString(Session["UserName"]));
                dictionaryLastProcessedDetails[siteOverviewRow.SiteName] = lastProcessedDetails;
                GuardianDataSet.DiskDetailsDataTable diskTable;
                int driveSpace = 0;
                if (lastProcessedDetails != null)
                {
                    DateTime result = DateTime.MinValue;
                    TimeSpan timeSpan = TimeSpan.MinValue;
                    System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
                    doc.LoadXml(siteStatus);
                    diskTable = GuardianBL.GetDiskSpace(doc);
                    if (DateTime.TryParse(lastProcessedDetails.DateTime, out result))
                        timeSpan = DateTime.Now - result;
                    if (timeSpan.Hours >= _MaxDaysForUpdates * 24)
                        siteOverviewRow.Status = 2;
                    else if ((siteOverviewRow.Services.ToUpper() != "ALL SERVICES RUNNING" 
                        && siteOverviewRow.LastRead == string.Empty  && siteOverviewRow.LastUpdate == string.Empty)
                        || lastProcessedDetails.ExportRecordsToProcess != "0")
                        siteOverviewRow.Status = 1;
                    //else
                    //    upAndRunningSites++;
                    if (!details.Status) siteOverviewRow.Status = 1;
                    if (diskTable != null)
                    {
                        foreach (var item in diskTable)
                        {
                            if (int.TryParse(item.DriveSpace, out driveSpace))
                            {
                                if (driveSpace < _MinDriveSpace)
                                {
                                    siteOverviewRow.Status = 1;
                                    break;
                                }
                            }
                        }
                    }
                    if (lastProcessedDetails.ExportRecordsToProcess == "0")
                        siteOverviewRow.FIFO = "Up to Date";
                    else
                        siteOverviewRow.FIFO = lastProcessedDetails.ExportRecordsToProcess;
                    siteOverviewRow.LastRead = lastProcessedDetails.LastDropCreated;
                    if (timeSpan.Days < 73000 && timeSpan.Days > _MaxDaysForUpdates)
                    {
                        siteOverviewRow.Services = string.Empty;
                        siteOverviewRow.FIFO = string.Empty;
                        siteOverviewRow.LastRead = string.Empty;
                        siteOverviewRow.LastUpdate = "Status Unknown";
                        siteOverviewRow.Status = 2;

                        if (ColumntoInt(siteDetailsRow["Site_Status_ID"]) == -1)
                        {
                            siteOverviewRow.Status = -1;
                        }
                        else
                        {
                            siteOverviewRow.Status = 2;
                        }

                    }
                    else
                        siteOverviewRow.LastUpdate = lastProcessedDetails.DateTime;
                }
                else
                    siteOverviewRow.Status = 1;
                siteOverviewDataTable.AddSiteOverviewRow(siteOverviewRow);
            }
            detailControl.LastProcessedDetails = dictionaryLastProcessedDetails;
            upAndRunningSites = siteOverviewDataTable.Select("Status = 0").Length;
            lblUpnRunSite.Text = upAndRunningSites + " out of " + siteDetailsDataTable.Rows.Count;
        }
        catch (Exception ex)
        {
            LogManager.WriteLog("CreateSiteOverviewList::" + ex.Message, LogManager.enumLogLevel.Error);
        }
        return siteOverviewDataTable;
    }
    int ColumntoInt(object dtColumn)
    {
        try{
            return int.Parse(dtColumn.ToString());
        }
        catch
        {
            return 0;
        }
    }
    private DataView FilterSites(GuardianUIDataSet.SiteOverviewDataTable siteOverviewDataTable, int siteStatus)
    {
        DataView filteredDataView = null;
        if (siteStatus == 0)
            filteredDataView = siteOverviewDataTable.DefaultView;
        else if (siteStatus == 1)
        {
            var query = from siteOverviewRow in siteOverviewDataTable where siteOverviewRow.Status == 0 select siteOverviewRow;
            filteredDataView = query.AsDataView<GuardianUIDataSet.SiteOverviewRow>();
        }
        else if (siteStatus == 2)
        {
            var query = from siteOverviewRow in siteOverviewDataTable where siteOverviewRow.Status == 1 select siteOverviewRow;
            filteredDataView = query.AsDataView<GuardianUIDataSet.SiteOverviewRow>();
        }
        else if (siteStatus == 3)
        {
            var query = from siteOverviewRow in siteOverviewDataTable where siteOverviewRow.Status == 2 select siteOverviewRow;
            filteredDataView = query.AsDataView<GuardianUIDataSet.SiteOverviewRow>();
        }
        else if (siteStatus == 4)
        {
            var query = from siteOverviewRow in siteOverviewDataTable where siteOverviewRow.Status == -1 select siteOverviewRow;
            filteredDataView = query.AsDataView<GuardianUIDataSet.SiteOverviewRow>();
        }

        return filteredDataView;
    }

    #endregion

    #region Event handlers

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["UserName"] == null || string.IsNullOrEmpty(Session["UserName"].ToString()))
                Response.Redirect("Login.aspx", true);
            UserName = Session["UserName"].ToString();
            PopulateSiteOverview();
        }
    }

    protected void UpdateTimer_Tick(object sender, EventArgs e)
    {
        try
        {
            PopulateSiteOverview();
            if (Session["SelectedNode"] != null)
            {
                SelectSite(Session["SelectedNode"].ToString());
                if (Session["SelectedNode"].ToString().ToUpper() != "SITES" && Convert.ToInt32(Session["SelectedIndex"]) != -99)
                {
                    TreeViewSites.Nodes[0].ChildNodes[Convert.ToInt32(Session["SelectedIndex"])].Selected = true;
                }
            }
            else
                SelectSite("Sites");
        }
        catch (Exception ex)
        {
            LogManager.WriteLog("UpdateTimer_Tick::" + ex.Message, LogManager.enumLogLevel.Error);
        }
    }

    protected void SiteNameLinkButton_Clicked(object sender, EventArgs e)
    {
        LinkButton linkButton = sender as LinkButton;
        if (linkButton != null)
        {
            Session["SelectedNode"] = linkButton.Text;
            SelectSite(linkButton.Text);
            GridViewRow gridViewRow = linkButton.Parent.Parent as GridViewRow;
            if (gridViewRow != null)
            {
                Session["SelectedIndex"] = gridViewRow.RowIndex;
                TreeViewSites.Nodes[0].ChildNodes[gridViewRow.RowIndex].Selected = true;
            }
        }
    }

    protected void TreeViewSites_SelectedNodeChanged(object sender, EventArgs e)
    {
        Session["SelectedIndex"] = -99;
        for (int i = 0; i < TreeViewSites.Nodes[0].ChildNodes.Count; i++)
        {
            if (TreeViewSites.Nodes[0].ChildNodes[i].Selected)
            {
                Session["SelectedIndex"] = i;
                break;
            }
        }
        Session["SelectedNode"] = TreeViewSites.SelectedNode.Text;
        SelectSite(TreeViewSites.SelectedNode.Text);
    }

    protected void DropDownFilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList dropDownList = sender as DropDownList;
        DropDownListFilter.SelectedIndex = dropDownList.SelectedIndex;
        DropDownListFilterGrid.SelectedIndex = dropDownList.SelectedIndex;
        SelectSite("Sites");
        PopulateSiteOverview();
    }

    protected void GridViewSites_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if ((e.Row != null) && (DataControlRowType.Header != e.Row.RowType) && (e.Row.DataItem != null))
        {
            GuardianUIDataSet.SiteOverviewRow siteOverviewRow = (e.Row.DataItem as DataRowView).Row as GuardianUIDataSet.SiteOverviewRow;
            ImageControl image = e.Row.Cells[0].Controls[1].Controls[0] as ImageControl;

            if (siteOverviewRow.Status == -1)
            {
                image.ImageUrl = "~/Images/Terminated.gif";
                e.Row.ForeColor = System.Drawing.Color.Black;
            }
            if (siteOverviewRow.Status == 0)
            {
                image.ImageUrl = "~/Images/Success.gif";
                e.Row.ForeColor = System.Drawing.Color.Black;
            }
            else if (siteOverviewRow.Status == 1)
            {
                image.ImageUrl = "~/Images/Failure.gif";
                e.Row.ForeColor = System.Drawing.Color.Red;
            }
            else if (siteOverviewRow.Status == 2)
            {
                image.ImageUrl = "~/Images/Alert.gif";
                e.Row.ForeColor = System.Drawing.Color.Orange;
            }

        }
    }

    protected void RefreshButton_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        try
        {
            if (TreeViewSites.Nodes.Count > 0)
            {
                TreeViewSites.Nodes[0].Selected = true;
            }
            Session["SelectedIndex"] = -99;
            Session["SelectedNode"] = null;
            RefreshPage();
        }
        catch (Exception ex)
        {
            LogManager.WriteLog("RefreshButton_Click::" + ex.Message, LogManager.enumLogLevel.Error);
        }
    }

    protected void DropDownListInterval_SelectedIndexChanged(object sender, EventArgs e)
    {
        UpdateTimer.Interval = int.Parse(DropDownListInterval.SelectedValue);
    }

    protected void CheckBoxAutoRefresh_CheckedChanged(object sender, EventArgs e)
    {
        UpdateTimer.Enabled = CheckBoxAutoRefresh.Checked;
    }

    protected void btnRequestNow_Click(object sender, EventArgs e)
    {
        if (MainView.Attributes["style"] == "display: none;")
            PopulateSiteOverview();
        else
        {
            SelectSite("Sites");
            RefreshPage();
        }
    }

    protected void btnRefreshPage_Click(object sender, EventArgs e)
    {
        try
        {
            string SiteName = String.Empty;
            if (Session["SiteName"] != null && !string.IsNullOrEmpty(Session["SiteName"].ToString()))
                SiteName = Session["SiteName"].ToString();
            if (!String.IsNullOrEmpty(SiteName))
            {
                GuardianBL.UpdateCurrentStatus(SiteName);
                if (MainView.Attributes["style"] == "display: none;")
                {
                    SelectSite(SiteName);
                }
                else
                {
                    SelectSite("Sites");
                    RefreshPage();
                }
            }
        }
        catch (Exception ex)
        {
            LogManager.WriteLog("btnRefreshPage_Click::" + ex.Message, LogManager.enumLogLevel.Error);
        }
    }

    #endregion
}