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
using BMC.GuardianWebApp;
using System.Text.RegularExpressions;
using BMC.Common.ExceptionManagement;

public enum SiteStatusValues
{
    Terminated = -1,
    Running = 0,
    Stopped = 1,
    Unknown = 2
}
public partial class MainPage : System.Web.UI.Page
{
	#region Fields

	private int _TimeInterval = 15;
	private int _MaxDaysForUpdates = 2;
	private int _MinDriveSpace = 1500;    
	private int proxyTimeOut = 20;
	public string UserName = string.Empty;
	public string Status = string.Empty;
    private int _InActiveServerInterval = 10;
    private double _GraceTimeForCheckInterval = 3600;

	#endregion

	#region Construction

	public MainPage()
	{
        _TimeInterval = BMC.CoreLib.Extensions.GetAppSettingValueInt("TimeInterval", 15);
        _MaxDaysForUpdates = BMC.CoreLib.Extensions.GetAppSettingValueInt("MaxDaysForUpdates", 2);

        _MinDriveSpace = BMC.CoreLib.Extensions.GetAppSettingValueInt("MinDriveSpace", 1500);
        proxyTimeOut = BMC.CoreLib.Extensions.GetAppSettingValueInt("ProxyTimeOut", 10000);
        _InActiveServerInterval = BMC.CoreLib.Extensions.GetAppSettingValueInt("InActiveServerInterval", 10);

        _GraceTimeForCheckInterval = BMC.CoreLib.Extensions.GetAppSettingValueDouble("GraceTimeForCheckInterval", 3600);

        
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
                PopulateSiteOverview(_GraceTimeForCheckInterval);
				SelectSite(selectedSite);
			}
			else
			{
                PopulateSiteOverview(_GraceTimeForCheckInterval);
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
                PopulateSiteOverview(_GraceTimeForCheckInterval);
				SelectSite(selectedSite);
			}
			else
			{
				DropDownListFilter.SelectedIndex = 0;
				DropDownListFilterGrid.SelectedIndex = 0;
                PopulateSiteOverview(_GraceTimeForCheckInterval);
				SelectSite("Sites");
				TreeViewSites.Nodes[0].Selected = true;
			}
		}
		catch (Exception ex)
		{
			LogManager.WriteLog("RefreshPage::" + ex.Message, LogManager.enumLogLevel.Error);
		}
	}

    private void PopulateSiteOverview(double graceTimeForCheckInterval)
	{
		GuardianUIDataSet.SiteOverviewDataTable siteOverviewDataTable = null;
        siteOverviewDataTable = CreateSiteOverviewList(graceTimeForCheckInterval);
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
			(detailControl as DetailControl).PopulateSite(siteName);
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
				if (siteOverviewRow.Status == (int)SiteStatusValues.Running)
					treeNode.ImageUrl = "~/Images/Success.gif";
                else if (siteOverviewRow.Status == (int)SiteStatusValues.Stopped)
					treeNode.ImageUrl = "~/Images/Failure.gif";
                else if (siteOverviewRow.Status == (int)SiteStatusValues.Unknown)
					treeNode.ImageUrl = "~/Images/Alert.gif";
                else if (siteOverviewRow.Status == (int)SiteStatusValues.Terminated)
				{
					treeNode.ImageUrl = "~/Images/Terminated.gif";
					//treeNode.Value = "Terminated";
                    treeNode.Text = siteOverviewRow.SiteName + " [Terminated]";
				}
		//Show Tooltip for Treeview nodes
                treeNode.ToolTip = siteOverviewRow.SiteName;
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

    private GuardianUIDataSet.SiteOverviewDataTable CreateSiteOverviewList(double graceTimeForCheckInterval)
	{
		if (Session["UserName"] == null || string.IsNullOrEmpty(Session["UserName"].ToString()))
			Response.Redirect("Login.aspx", true);
		GuardianUIDataSet.SiteOverviewDataTable siteOverviewDataTable = new GuardianUIDataSet.SiteOverviewDataTable();
		try
		{
			string Region = "";
			string MissedHourlies = "";
			new Guardian_LINQBL().GetViewSiteStatusInfo("", Convert.ToString(Session["UserName"]), ref Region, ref MissedHourlies);
			FIFOStatus fIFOStatus = GuardianBL.GetFIFOStatus();
			lblLstRecExpFrmEnt.Text = fIFOStatus.LastRecord;
			lblRecToPro.Text = fIFOStatus.UnProcessed;
            GuardianDataSet.SiteDetailsDataTable siteDetailsDataTable = GuardianBL.GetSiteList(chkIsActive.Checked, Convert.ToInt32(Session["SecurityUserID"]), DropDownListFilter.SelectedIndex, _InActiveServerInterval);
			int upAndRunningSites = 0;
			Dictionary<string, LastProcessedDetails> dictionaryLastProcessedDetails = new Dictionary<string, LastProcessedDetails>();
			foreach (GuardianDataSet.SiteDetailsRow siteDetailsRow in siteDetailsDataTable)
			{
               GuardianUIDataSet.SiteOverviewRow siteOverviewRow = siteOverviewDataTable.NewSiteOverviewRow();
               try
               {
                    siteOverviewRow.Status = (int)SiteStatusValues.Running;
                    siteOverviewRow.SiteName = siteDetailsRow.Site_Name;
                    string siteStatus = siteDetailsRow.Site_Status;
                    siteOverviewRow.Services = GuardianBL.GetAllServiceStatus(siteOverviewRow.SiteName, siteStatus);
                    LastProcessedDetails lastProcessedDetails = GuardianBL.GetLastProcessedDetails(siteStatus);
                    SiteStatusDetails details = GuardianBL.GetSiteStatusDetails(lastProcessedDetails, Convert.ToString(Session["UserName"]), siteDetailsRow.HourlyNotRun, Region, proxyTimeOut, graceTimeForCheckInterval);
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
                        if (timeSpan.TotalHours >= _MaxDaysForUpdates * 24)
                            siteOverviewRow.Status = (int)SiteStatusValues.Unknown;
                        else if ((siteOverviewRow.Services.ToUpper() != "ALL SERVICES RUNNING"
                            && siteOverviewRow.LastRead == string.Empty && siteOverviewRow.LastUpdate == string.Empty)
                            || lastProcessedDetails.ExportRecordsToProcess != "0")
                            siteOverviewRow.Status = (int)SiteStatusValues.Stopped;
                        //else
                        //    upAndRunningSites++;
                        if (!details.Status) siteOverviewRow.Status = (int)SiteStatusValues.Stopped;
                        if (diskTable != null)
                        {
                            foreach (var item in diskTable)
                            {
                                if (int.TryParse(item.DriveSpace, out driveSpace))
                                {
                                    if (driveSpace < _MinDriveSpace)
                                    {
                                        siteOverviewRow.Status = (int)SiteStatusValues.Stopped;
                                        break;
                                    }
                                }
                            }
                        }
                        if (lastProcessedDetails.ExportRecordsToProcess == "0")
                            siteOverviewRow.FIFO = "Up to Date";
                        else
                        {
                            siteOverviewRow.FIFO = lastProcessedDetails.ExportRecordsToProcess;
                            siteOverviewRow.Status = (int)SiteStatusValues.Stopped;
                        }
                        siteOverviewRow.LastRead = lastProcessedDetails.LastDropCreated;
                        TimeSpan ts_subtract = DateTime.Now - Convert.ToDateTime(lastProcessedDetails.DateTime);
                        TimeSpan ts_interval = new TimeSpan(0, _InActiveServerInterval, 0);

                        if ((timeSpan.TotalDays < 73000 && timeSpan.TotalHours >= _MaxDaysForUpdates * 24)
                            || Convert.ToInt64(ts_subtract.TotalMinutes) > Convert.ToInt64(ts_interval.TotalMinutes))
                        {
                            siteOverviewRow.Services = string.Empty;
                            siteOverviewRow.FIFO = string.Empty;
                            siteOverviewRow.LastRead = string.Empty;
                            siteOverviewRow.LastUpdate = "Status Unknown";
                            siteOverviewRow.Status = (int)SiteStatusValues.Unknown;

                            if (ColumntoInt(siteDetailsRow["Site_Status_ID"]) == (int)SiteStatusValues.Terminated)
                            {
                                siteOverviewRow.Status = (int)SiteStatusValues.Terminated;
                            }

                        }
                        else
                            siteOverviewRow.LastUpdate = lastProcessedDetails.DateTime;
                    }
                    else
                    {
                        siteOverviewRow.Status = (int)SiteStatusValues.Unknown;
                    }

                    if (ColumntoInt(siteDetailsRow["Site_Status_ID"]) == (int)SiteStatusValues.Terminated)
                    {
                        siteOverviewRow.Status = (int)SiteStatusValues.Terminated;
                    }
                    else if (siteOverviewRow.Status == (int)SiteStatusValues.Running && ColumntoInt(siteDetailsRow["Site_Status_ID"]) == (int)SiteStatusValues.Stopped)
                    {
                        siteOverviewRow.Status = (int)SiteStatusValues.Stopped;
                    }

                    siteOverviewDataTable.AddSiteOverviewRow(siteOverviewRow);
                }
                catch (Exception ex)
                {
                    ExceptionManager.Publish(ex);
                    if (siteOverviewRow != null)
                        siteOverviewRow.Status = (int)SiteStatusValues.Unknown;
                }
			}
			(detailControl as DetailControl).LastProcessedDetails = dictionaryLastProcessedDetails;
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
		try
		{
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
            var query = from siteOverviewRow in siteOverviewDataTable where siteOverviewRow.Status == (int)SiteStatusValues.Running select siteOverviewRow;
			filteredDataView = query.AsDataView<GuardianUIDataSet.SiteOverviewRow>();
		}
		else if (siteStatus == 2)
		{
            var query = from siteOverviewRow in siteOverviewDataTable where siteOverviewRow.Status == (int)SiteStatusValues.Stopped select siteOverviewRow;
			filteredDataView = query.AsDataView<GuardianUIDataSet.SiteOverviewRow>();
		}
		else if (siteStatus == 3)
		{
            var query = from siteOverviewRow in siteOverviewDataTable where siteOverviewRow.Status == (int)SiteStatusValues.Unknown select siteOverviewRow;
			filteredDataView = query.AsDataView<GuardianUIDataSet.SiteOverviewRow>();
		}
		else if (siteStatus == 4)
		{
            var query = from siteOverviewRow in siteOverviewDataTable where siteOverviewRow.Status == (int)SiteStatusValues.Terminated select siteOverviewRow;
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
			Status = "Site details are loading...";
			UserName = Session["UserName"].ToString();
			SingleRunTimer.Enabled = true;
		}
		UpdateTimer.Enabled = CheckBoxAutoRefresh.Checked;
	}

	protected void SingleRunTimer_Tick(object sender, EventArgs e)
	{
		SingleRunTimer.Enabled = false;
		//UpdateTimer_Tick(sender, e);
        PopulateMainPage(_GraceTimeForCheckInterval);
		Status = string.Empty;
	}

    protected void PopulateMainPage(double graceTimeForCheckInterval)
    {
        try
        {
            PopulateSiteOverview(graceTimeForCheckInterval);
            if (Session["SelectedNode"] != null && TreeViewSites.Nodes.Count > 0)
            {
                for (int i = 0; i < TreeViewSites.Nodes[0].ChildNodes.Count; i++)
                {
                    if (Convert.ToString(TreeViewSites.Nodes[0].ChildNodes[i].Text + "").Contains(Convert.ToString(Session["SelectedNode"])))
                    {
                        SelectSite(Session["SelectedNode"].ToString());
                        TreeViewSites.Nodes[0].ChildNodes[i].Selected = true;
                        return;
                    }
                }
            }
            Session["SelectedNode"] = null;
            SelectSite("Sites");
        }
        catch (Exception ex)
        {
            ExceptionManager.Publish(ex);
            LogManager.WriteLog("UpdateTimer_Tick::" + ex.Message, LogManager.enumLogLevel.Error);
        }
    }
	protected void UpdateTimer_Tick(object sender, EventArgs e)
	{
		try
		{
			UpdateTimer.Enabled = false;
            		PopulateMainPage(_GraceTimeForCheckInterval);
		}
		catch (Exception ex)
		{
            ExceptionManager.Publish(ex);
			LogManager.WriteLog("UpdateTimer_Tick::" + ex.Message, LogManager.enumLogLevel.Error);
		}
		finally
		{
			UpdateTimer.Enabled = CheckBoxAutoRefresh.Checked;
		}
	}

	protected void SiteNameLinkButton_Clicked(object sender, EventArgs e)
	{
		LinkButton linkButton = sender as LinkButton;
		if (linkButton != null)
		{
			Session["SelectedNode"] = linkButton.Text;
            SelectSite(linkButton.Text);
            bool Isterminated = (TreeViewSites.SelectedNode != null && TreeViewSites.SelectedNode.Text.Contains("Terminated"));
			Session["isTerminated"] = Isterminated;
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
        string selectedNode = Regex.Replace(TreeViewSites.SelectedNode.Text, " \\[Terminated\\]", "");
        Session["SelectedNode"] = selectedNode;
        bool Isterminated = (TreeViewSites.SelectedNode != null && TreeViewSites.SelectedNode.Text.Contains("Terminated"));
		Session["isTerminated"] = Isterminated;
        if (selectedNode.ToUpper().Equals("SITES"))
            PopulateSiteOverview(_GraceTimeForCheckInterval);
        SelectSite(selectedNode);
	}

	protected void DropDownFilter_SelectedIndexChanged(object sender, EventArgs e)
	{
		DropDownList dropDownList = sender as DropDownList;
		DropDownListFilter.SelectedIndex = dropDownList.SelectedIndex;
		DropDownListFilterGrid.SelectedIndex = dropDownList.SelectedIndex;
		SelectSite("Sites");
        PopulateSiteOverview(_GraceTimeForCheckInterval);
	}

	protected void GridViewSites_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if ((e.Row != null) && (DataControlRowType.Header != e.Row.RowType) && (e.Row.DataItem != null))
		{
			GuardianUIDataSet.SiteOverviewRow siteOverviewRow = (e.Row.DataItem as DataRowView).Row as GuardianUIDataSet.SiteOverviewRow;
			ImageControl image = e.Row.Cells[0].Controls[1].Controls[0] as ImageControl;

            if (siteOverviewRow.Status == (int)SiteStatusValues.Terminated)
			{
				image.ImageUrl = "~/Images/Terminated.gif";
				e.Row.ForeColor = System.Drawing.Color.Black;
			}
            if (siteOverviewRow.Status == (int)SiteStatusValues.Running)
			{
				image.ImageUrl = "~/Images/Success.gif";
				e.Row.ForeColor = System.Drawing.Color.Black;
			}
            else if (siteOverviewRow.Status == (int)SiteStatusValues.Stopped)
			{
				image.ImageUrl = "~/Images/Failure.gif";
				e.Row.ForeColor = System.Drawing.Color.Red;
			}
            else if (siteOverviewRow.Status == (int)SiteStatusValues.Unknown)
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
            PopulateSiteOverview(_GraceTimeForCheckInterval);
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
            if (Session["UserName"] == null || string.IsNullOrEmpty(Session["UserName"].ToString()))
                Response.Redirect("Login.aspx", true);
			string SiteName = String.Empty;
			UserName = Convert.ToString(Session["UserName"]);
			if (Session["SiteName"] != null && !string.IsNullOrEmpty(Session["SiteName"].ToString()))
				SiteName = Session["SiteName"].ToString();
			if (!String.IsNullOrEmpty(SiteName))
			{
				GuardianBL.UpdateCurrentStatus(SiteName);
                if (MainView.Attributes["style"] == "display: none;")
                {
                    PopulateSiteOverview(_GraceTimeForCheckInterval);
                    SelectSite(SiteName);
                }
                else
                {
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