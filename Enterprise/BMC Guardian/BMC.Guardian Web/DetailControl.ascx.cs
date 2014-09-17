using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Web.UI.WebControls;
using System.Xml;
using BMC.Common.ConfigurationManagement;
using BMC.Guardian.Business;
using BMC.Guardian.DBHelper;
using BMC.Guardian.Transport;
using ImageControl = System.Web.UI.WebControls.Image;
using BMC.Common.LogManagement;
using BMC.Guardian.Business.Proxy;
using BMC.Common.ExceptionManagement;



public partial class DetailControl : System.Web.UI.UserControl
{
    #region Fields

    private int _TimeInterval = 15;
    private int _MinDriveSpace = 1500;
    private int _MaxDaysForUpdates = 2;
    private bool _ForHistory;


    #endregion

    #region Construction

    public DetailControl()
    {
        int configValue = 0;

        if (int.TryParse(ConfigurationManager.AppSettings["TimeInterval"], out configValue))
            _TimeInterval = configValue;

        if (int.TryParse(ConfigurationManager.AppSettings["MaxDaysForUpdates"], out configValue))
            _MaxDaysForUpdates = configValue;

        if (int.TryParse(ConfigurationManager.AppSettings["MinDriveSpace"], out configValue))
            _MinDriveSpace = configValue;
    }

    #endregion

    #region Properties
    public IDictionary<string, LastProcessedDetails> LastProcessedDetails
    {
        get
        {
            return ViewState["LastProcessedDetails"] as IDictionary<string, LastProcessedDetails>;
        }
        set
        {
            ViewState["LastProcessedDetails"] = value;
        }
    }

    #endregion

    #region Public methods

    public void PopulateSite(string SiteName)
    {
        ClearControls();
        string siteStatus = GuardianDBHelper.GetSiteStatus(SiteName);
        LabelSite.Text = SiteName;
        Session["SiteName"] = SiteName;
        PoplutateSiteDetails(SiteName, siteStatus, false);
    }
    public void populateAsset(string Asset)
    {
        ClearControls();
        Session["Asset"] = Asset;
    }

    public void PoplutateSiteDetails(string siteName, string siteStatus, bool IsHistory)
    {
        if (Session["UserName"] == null || string.IsNullOrEmpty(Session["UserName"].ToString()))
            Response.Redirect("Login.aspx", true);
        _ForHistory = IsHistory;
        LabelSite.Text = siteName;
        if (_ForHistory)
        {
            detailsref.Visible = false;
        }
        if (!string.IsNullOrEmpty(siteStatus))
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(siteStatus);
            LastProcessedDetails Details = GuardianBL.GetLastProcessedDetails(siteStatus);
            tblStatus.Visible = !_ForHistory;
            if (!_ForHistory)
            {
                SiteStatusDetails _siteStatus = GuardianBL.GetSiteStatusDetails(siteStatus, Convert.ToString(Session["UserName"]));
                ImgSiteStatus.ImageUrl = _siteStatus.Status ? "~/Images/Success.gif" : "~/Images/Failure.gif";
            }
            
            TimeSpan timeSpanDiff = TimeSpan.Zero;

            if (string.IsNullOrEmpty(Details.DateTime))
            {
                GeneralInformation.InnerText = "Not able to get status from DB. Make sure the Webservice and BMC Guardian Service is up and running Exchange.";

            }
            else
                timeSpanDiff = SetLastUpdate(siteName, Details.DateTime);
            PopulateOtherDetails(siteName, Details);

            GuardianDataSet.ServiceDetailsDataTable serviceDetailsDataTable = GuardianBL.GetServiceStatusasTable(siteStatus);
            if (serviceDetailsDataTable.Rows.Count == 0)
            {
                GeneralInformation.InnerText = "Not able to get status from DB. Make sure the Webservice and BMC Guardian Service is up and running Exchange.";
            }
            else
            {
                PopulateServiceDetails(serviceDetailsDataTable);
                PopulateDiskDetails(xmlDocument, serviceDetailsDataTable, timeSpanDiff);
                PopulateSiteInterrogationDetails(xmlDocument);
                //PopulateSiteVLTStatusDetails(xmlDocument);
                PopulateSystemLogsTable(xmlDocument);
            }
        }
    }

    #endregion

    #region Private methods

    private void ClearControls()
    {
        GridViewDisk.DataSource = null;
        GridViewServices.DataSource = null;
        GridViewSiteInfo.DataSource = null;
        SystemLogView.DataSource = null;
        GridViewDisk.DataBind();
        GridViewServices.DataBind();
        GridViewSiteInfo.DataBind();
        SystemLogView.DataBind();
        LabelSite.Text = string.Empty;
        LabelLastUpdate.Text = string.Empty;
        LabelLastDropCreated.Text = string.Empty;
        LabelLastReadCreated.Text = string.Empty;
        LabelLastHourlyCreated.Text = string.Empty;
        LabelExchangeDBVersion.Text = string.Empty;
        GeneralInformation.InnerText = string.Empty;
        //LabelExchangePatchVersion.Text = string.Empty;
        LabelFIFORecordsToProcess.Text = string.Empty;
        LabelFIFOLastRecordExported.Text = string.Empty;
    }

    private TimeSpan SetLastUpdate(string siteName, string dateTime)
    {

        LabelLastUpdate.Text = dateTime;
        if (_ForHistory)
        {
            return TimeSpan.Zero;
        }
        TimeSpan timeSpanDiff = DateTime.Now - Convert.ToDateTime(dateTime);
        if ((timeSpanDiff.Days * 1440) + (timeSpanDiff.Hours * 60) + timeSpanDiff.Minutes > _TimeInterval)
            LabelLastUpdate.ForeColor = Color.Orange;
        else
            LabelLastUpdate.ForeColor = Color.Black;

        return timeSpanDiff;
    }

    private void PopulateHourlyStatisticsDetails(XmlDocument xmlDocument)
    {
        GuardianDataSet.HourlyStatisticsDataTable hourlyStatisticsDataTable = GuardianBL.GetHourlyStatistics(xmlDocument);
        if ((hourlyStatisticsDataTable != null) && (hourlyStatisticsDataTable.Rows.Count > 0))
        {

        }
    }

    private void PopulateSiteInterrogationDetails(XmlDocument xmlDocument)
    {
        string shtml = "<b>{0}:&nbsp;&nbsp;&nbsp;{1}/<span style='color:red'>{2}</span></b>";
        GuardianDataSet.SiteInterrogationDataTable siteInterrogationDataTable = GuardianBL.GetSiteInterrogationDetails(xmlDocument);
        GridViewSiteInfo.DataSource = siteInterrogationDataTable;
        GridViewSiteInfo.DataBind();
        try
        {
            if (siteInterrogationDataTable != null)
            {
                if (siteInterrogationDataTable.Rows.Count > 0)
                {
                    DataRow[] odr = siteInterrogationDataTable.Select("GMUToServer='TRUE'");
                    lt_GMUServer.Text = string.Format(shtml, "GMU->Server", odr.Length.ToString(), (siteInterrogationDataTable.Rows.Count - odr.Length).ToString());
                    DataRow[] odr1 = siteInterrogationDataTable.Select("GMUToServer='TRUE' AND GMUToMachine<>0");
                    lt_GMUMachine.Text = string.Format(shtml, "GMU->Machine", odr1.Length.ToString(), (siteInterrogationDataTable.Rows.Count - odr1.Length).ToString());
                }
            }
        }
        catch (Exception Ex)
        {
            LogManager.WriteLog("DetailsControl.PopulateSiteInterrogationDetailsError" + Ex.Message, LogManager.enumLogLevel.Error);

        }
    }

    //private void PopulateSiteVLTStatusDetails(XmlDocument xmlDocument)
    //{
    //    GuardianDataSet.SiteVLTStatusDataTable siteVLTStatusDataTable = GuardianBL.GetSiteVLTStatusDetails(xmlDocument);
    //    gridVLT.DataSource = siteVLTStatusDataTable;
    //    gridVLT.DataBind();
    //}

    private void PopulateSystemLogsTable(XmlDocument xmlDocument)
    {
        GuardianDataSet.SystemLogsDataTable SystemLogsDataTable = GuardianBL.GetSystemLogs(xmlDocument);
        SystemLogView.DataSource = SystemLogsDataTable;
        SystemLogView.DataBind();
    }
    public bool RebootFlag
    {
        get;
        set;
    }

    private void PopulateServiceDetails(GuardianDataSet.ServiceDetailsDataTable serviceDetailsDataTable)
    {
        GridViewServices.DataSource = serviceDetailsDataTable;
        GridViewServices.DataBind();
        //Added to hide Reboot button in history screen.
        if (RebootFlag == true)
        {
            GridViewSiteInfo.Columns[5].Visible = false;
            GridViewSiteInfo.Columns[6].Visible = false;
        }

    }

    private void PopulateOtherDetails(string siteName, LastProcessedDetails details)
    {
        LabelLastReadCreated.Text = details.LastReadCreated;
        LabelLastHourlyCreated.Text = details.LastHourCreated;
        LabelLastDropCreated.Text = details.LastDropCreated;
        LabelFIFOLastRecordExported.Text = details.LastRecordExported;
        LabelFIFORecordsToProcess.Text = details.ExportRecordsToProcess;
        LabelExchangeDBVersion.Text = details.DBVersion;
        // LabelExchangePatchVersion.Text = details.BMCVersion;

        if (details.ExportRecordsToProcess == "0")
            LabelFIFORecordsToProcess.ForeColor = Color.Black;
        else
            LabelFIFORecordsToProcess.ForeColor = Color.Red;
    }

    private void PopulateDiskDetails(XmlDocument xmlDocument, GuardianDataSet.ServiceDetailsDataTable serviceDetailsDataTable, TimeSpan timeSpanDiff)
    {
        GuardianDataSet.DiskDetailsDataTable diskDetailsDataTable = GuardianBL.GetDiskSpace(xmlDocument);
        if (timeSpanDiff.Days > _MaxDaysForUpdates)
        {
            GeneralInformation.InnerText = "Status unknown at Present. No updates for long time.";
            GridViewDisk.ForeColor = Color.Orange;
            GridViewServices.ForeColor = Color.Orange;
            foreach (GuardianDataSet.ServiceDetailsRow serviceDetailsRow in serviceDetailsDataTable)
                serviceDetailsRow.ServiceStatus = "Unknown";
            GridViewServices.DataSource = serviceDetailsDataTable;
            GridViewServices.DataBind();
            foreach (GuardianDataSet.DiskDetailsRow item in diskDetailsDataTable)
            {
                item.DriveSpace = "Unknown";
            }
        }

        GridViewDisk.DataSource = diskDetailsDataTable;
        GridViewDisk.DataBind();
    }

    #endregion

    #region Event handlers

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void GridViewDisk_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if ((e.Row != null) && (DataControlRowType.Header != e.Row.RowType) && (e.Row.DataItem != null))
        {
            GuardianDataSet.DiskDetailsRow diskDetailsRow = (GuardianDataSet.DiskDetailsRow)(e.Row.DataItem as DataRowView).Row;
            ImageControl image = e.Row.Cells[0].Controls[1] as ImageControl;
            int driveSpace = 0;
            if (int.TryParse(diskDetailsRow.DriveSpace, out driveSpace))
            {
                if (driveSpace < _MinDriveSpace)
                {
                    image.ImageUrl = "~/Images/Failure.gif";
                    e.Row.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    image.ImageUrl = "~/Images/Success.gif";
                    e.Row.ForeColor = System.Drawing.Color.Black;
                }
            }
            else
            {
                image.ImageUrl = "~/Images/Alert.gif";
                e.Row.ForeColor = System.Drawing.Color.Orange;
            }
        }
    }

    protected void GridViewServices_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if ((e.Row != null) && (DataControlRowType.Header != e.Row.RowType) && (e.Row.DataItem != null))
        {
            GuardianDataSet.ServiceDetailsRow serviceDetailsRow = (e.Row.DataItem as DataRowView).Row as GuardianDataSet.ServiceDetailsRow;
            ImageControl image = e.Row.Cells[0].Controls[1] as ImageControl;

            if (serviceDetailsRow.ServiceStatus.ToUpper() == "RUNNING")
            {
                image.ImageUrl = "~/Images/Success.gif";
                e.Row.ForeColor = System.Drawing.Color.Black;
            }
            else if (serviceDetailsRow.ServiceStatus.ToUpper() == "UNKNOWN")
            {
                image.ImageUrl = "~/Images/Alert.gif";
                e.Row.ForeColor = System.Drawing.Color.Orange;
            }
            else
            {
                image.ImageUrl = "~/Images/Failure.gif";
                e.Row.ForeColor = System.Drawing.Color.Red;
            }
        }
    }

    protected void GridViewSiteInfo_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if ((e.Row != null) && (DataControlRowType.Header != e.Row.RowType) && (e.Row.DataItem != null))
        {
            GuardianDataSet.SiteInterrogationRow siteInterrogationRow = (e.Row.DataItem as DataRowView).Row as GuardianDataSet.SiteInterrogationRow;
            if (siteInterrogationRow.GMUToServer == bool.FalseString)
            {
                e.Row.Cells[2].ForeColor = Color.Red;
                e.Row.Cells[3].Text = "N/A";
                e.Row.Cells[3].ForeColor = Color.Red;
            }
            else
            {
                if (siteInterrogationRow.GMUToMachine == "0")
                {
                    e.Row.Cells[3].Text = "Error";
                    e.Row.Cells[3].ForeColor = Color.Red;
                }
                else
                    e.Row.Cells[3].Text = "Ok";
            }
        }
    }

    protected void GridVLTStatus_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if ((e.Row != null) && (DataControlRowType.Header == e.Row.RowType))
        {
            e.Row.Cells[0].Text = "Position";
            e.Row.Cells[1].Text = "VLT AAMS";
            e.Row.Cells[2].Text = "VLT<br/>Verification";
            e.Row.Cells[3].Text = "Game Install<br/>AAMS Status";
            e.Row.Cells[4].Text = "Game<br/>Verification";
            e.Row.Cells[5].Text = "Game Enable<br/>AAMS Status";
            e.Row.Cells[6].Text = "AAMS<br/>Enable Disable";
            e.Row.Cells[7].Text = "BMC Enterprise<br/>Status";
        }

        if ((e.Row != null) && (DataControlRowType.Header != e.Row.RowType) && (e.Row.DataItem != null))
        {
            GuardianDataSet.SiteVLTStatusRow siteVLTStatusRow = (e.Row.DataItem as DataRowView).Row as GuardianDataSet.SiteVLTStatusRow;
            ImageControl image1 = e.Row.Cells[1].Controls[1] as ImageControl;
            ImageControl image2 = e.Row.Cells[2].Controls[1] as ImageControl;
            ImageControl image3 = e.Row.Cells[3].Controls[1] as ImageControl;
            ImageControl image4 = e.Row.Cells[4].Controls[1] as ImageControl;
            ImageControl image5 = e.Row.Cells[5].Controls[1] as ImageControl;
            ImageControl image6 = e.Row.Cells[6].Controls[1] as ImageControl;
            ImageControl image7 = e.Row.Cells[7].Controls[1] as ImageControl;

            if (siteVLTStatusRow.BAD_AAMS_Status == "1")
            {
                image1.ImageUrl = "~/Images/Success.gif";
                e.Row.ForeColor = System.Drawing.Color.Black;
            }
            else
            {
                image1.ImageUrl = "~/Images/Failure.gif";
                e.Row.ForeColor = System.Drawing.Color.Black;
            }

            if (siteVLTStatusRow.BAD_Verification_Status == "1")
            {
                image2.ImageUrl = "~/Images/Success.gif";
                e.Row.ForeColor = System.Drawing.Color.Black;
            }
            else
            {
                image2.ImageUrl = "~/Images/Failure.gif";
                e.Row.ForeColor = System.Drawing.Color.Black;
            }

            if (siteVLTStatusRow.Game_AAMS_Status == "1")
            {
                image3.ImageUrl = "~/Images/Success.gif";
                e.Row.ForeColor = System.Drawing.Color.Black;
            }
            else
            {
                image3.ImageUrl = "~/Images/Failure.gif";
                e.Row.ForeColor = System.Drawing.Color.Black;
            }

            if (siteVLTStatusRow.Game_Verification == "1")
            {
                image4.ImageUrl = "~/Images/Success.gif";
                e.Row.ForeColor = System.Drawing.Color.Black;
            }
            else
            {
                image4.ImageUrl = "~/Images/Failure.gif";
                e.Row.ForeColor = System.Drawing.Color.Black;
            }

            if (siteVLTStatusRow.Game_Enable_AAMS_Status == "1")
            {
                image5.ImageUrl = "~/Images/Success.gif";
                e.Row.ForeColor = System.Drawing.Color.Black;
            }
            else
            {
                image5.ImageUrl = "~/Images/Failure.gif";
                e.Row.ForeColor = System.Drawing.Color.Black;
            }

            if (siteVLTStatusRow.BAD_AAMS_EnableDisable == "1")
            {
                image6.ImageUrl = "~/Images/Success.gif";
                e.Row.ForeColor = System.Drawing.Color.Black;
            }
            else if (siteVLTStatusRow.BAD_AAMS_EnableDisable == "2")
            {
                image6.ImageUrl = "~/Images/Failure.gif";
                e.Row.ForeColor = System.Drawing.Color.Black;
            }
            else if (siteVLTStatusRow.BAD_AAMS_EnableDisable == "0")
            {
                image6.Visible = false;
                e.Row.ForeColor = System.Drawing.Color.Black;
                e.Row.Cells[6].Text = "N/A";
            }
            else
            {
                image6.Visible = false;
                e.Row.ForeColor = System.Drawing.Color.Black;
                e.Row.Cells[6].Text = "";
            }

            if (siteVLTStatusRow.BMC_Enterprise_Status == "1")
            {
                image7.ImageUrl = "~/Images/Success.gif";
                e.Row.ForeColor = System.Drawing.Color.Black;
            }
            else if (siteVLTStatusRow.BMC_Enterprise_Status == "0")
            {
                image7.ImageUrl = "~/Images/Failure.gif";
                e.Row.ForeColor = System.Drawing.Color.Black;
            }
            else
            {
                image7.Visible = false;
                e.Row.ForeColor = System.Drawing.Color.Black;
                e.Row.Cells[7].Text = "";
            }
        }


    }

    #endregion

    #region Reboot
    protected void GridViewSiteInfo_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string IPAddress = string.Empty;
        string siteCode = GuardianDBHelper.GetSiteCode(Session["SiteName"].ToString());
        int rowindex = Convert.ToInt32(e.CommandArgument);
        string Asset = GridViewSiteInfo.Rows[rowindex].Cells[1].Text;
        int installationNo = GuardianDBHelper.GetInstallationNo(Asset); //GuardianDBHelper.GetInstallationNo(Session["Asset"]);
        Proxy boot = null;
        string result = string.Empty;
        try
        {
            boot = new Proxy(siteCode, false);
            string retVal = boot.RebootGMU(installationNo);
            LogManager.WriteLog(retVal, LogManager.enumLogLevel.Info);
            string[] spl = retVal.Split(',');
            for (int i = 0; i < 2; i++)
            {
                result = spl[0];
                IPAddress = spl[1];
            }
            if (result.ToUpper()=="TRUE")
            {
                GridViewSiteInfo.Rows[rowindex].Cells[6].Text = "Reboot Successful for IP:" + IPAddress;
                LogManager.WriteLog("Reboot successful for IP:" + IPAddress, LogManager.enumLogLevel.Info);
            }

            else
            {
                GridViewSiteInfo.Rows[rowindex].Cells[6].Text = "Reboot Failed for IP:" + IPAddress;
                LogManager.WriteLog("Reboot Failed." + IPAddress, LogManager.enumLogLevel.Info);
            }
        }

        catch (Exception ex)
        {

            ExceptionManager.Publish(ex);
            GridViewSiteInfo.Rows[rowindex].Cells[6].Text = "Reboot Failed for IP:" + IPAddress;
            LogManager.WriteLog("Reboot Failed." + IPAddress, LogManager.enumLogLevel.Info);
        }
        finally
        {
            if (boot != null)
            {
                boot.Dispose();
            }
        }
    }
    #endregion Reboot

}



