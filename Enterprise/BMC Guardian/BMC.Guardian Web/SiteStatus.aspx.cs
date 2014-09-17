using System;
using System.Data;
using System.Web.UI.WebControls;
using BMC.Guardian.DBHelper;
using BMC.Guardian.Business;
using BMC.Guardian.Transport;
using BMC.Common.LogManagement;
using ImageControl = System.Web.UI.WebControls.Image;
using System.Globalization;
using BMC.Common.ConfigurationManagement;

enum RESULT
{
    YES = 1,
    NO = 0
}
public partial class StatusHistory : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string SiteName = "" ;
        if (!IsPostBack)
        {
            if (Session["UserName"] == null || string.IsNullOrEmpty(Session["UserName"].ToString()))
                Response.Redirect("Login.aspx", true);
            if (Session["SiteName"] != null && !string.IsNullOrEmpty( Session["SiteName"].ToString()))
                SiteName = Session["SiteName"].ToString();    
        }
        if (!string.IsNullOrEmpty(SiteName))
        {
            GridMessage.Visible = false;
            LabelSite.Text = SiteName;
            string siteStatus = GuardianDBHelper.GetSiteStatus(SiteName);
            SiteStatusDetails _siteStatus = GuardianBL.GetSiteStatusDetails(siteStatus, Convert.ToString(Session["UserName"]));
            Session["SiteStatusDetails"] = _siteStatus;
            GetSiteStatusTable(_siteStatus);
            LabelLastUpdate.Text = Convert.ToDateTime(_siteStatus.DateTime).ToString("G",new CultureInfo(_siteStatus.Region));
        }
    }

    protected void SiteDetails_Clicked(object sender, EventArgs e)
    {
        LinkButton linkButton = sender as LinkButton;
        if (linkButton != null)
        {
            if (Session["UserName"] == null || string.IsNullOrEmpty(Session["UserName"].ToString()))
                Response.Redirect("Login.aspx", true);
            SiteStatusDetails _siteStatus = Session["SiteStatusDetails"] as SiteStatusDetails;
            if (linkButton.Text.Equals(ConfigManager.Read("SiteDown")))
            {
                if (_siteStatus.IsSiteDown)
                    SetSiteDownInfo(_siteStatus);
                else
                    return;
            }
            else if (linkButton.Text.Equals(ConfigManager.Read("HourlyRun")))
                SetHourlyNotRunInfo(_siteStatus);
            else
                SetReadNotRunInfo(_siteStatus);

            Summary.Attributes["style"] = "display: none;";
            Details.Attributes["style"] = "display: block;";
        }
    }

    protected void GridViewStatus_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if ((e.Row != null) && (e.Row.RowType == DataControlRowType.DataRow ) && (e.Row.DataItem != null))
        {
            GuardianDataSet.SiteStatusRow siteStatusRow = ((DataRowView)e.Row.DataItem).Row as GuardianDataSet.SiteStatusRow;
            ImageControl image = e.Row.Cells[0].Controls[1] as ImageControl;
            if (Convert.ToString(siteStatusRow.SiteDetails).ToUpper().Equals(Convert.ToString(ConfigManager.Read("SiteDown")).ToUpper()))
            {
                if(Convert.ToString(siteStatusRow.Status).ToUpper() == "UP")
                    image.ImageUrl = "~/Images/Success.gif";
                else
                    image.ImageUrl = "~/Images/Failure.gif";
            }
            else
            {
                if (Convert.ToString(siteStatusRow.Status).ToUpper() == "YES" )
                    image.ImageUrl = "~/Images/Success.gif";
                else
                    image.ImageUrl = "~/Images/Failure.gif";
            }
        }
    }
    protected void lnkBack_Clicked(object sender, EventArgs e)
    {
        Summary.Attributes["style"] = "display: block;";
        Details.Attributes["style"] = "display: none;";
    }


    private void GetSiteStatusTable(SiteStatusDetails _siteStatus)
    {
        try
        {
            GuardianDataSet.SiteStatusDataTable siteStatusTable = new GuardianDataSet.SiteStatusDataTable();
            GuardianDataSet.SiteStatusRow row = (GuardianDataSet.SiteStatusRow)siteStatusTable.NewRow();
            row.SiteDetails = ConfigManager.Read("SiteDown");
            row.Status = (_siteStatus.IsSiteDown) ? "DOWN" : "UP"; 
            siteStatusTable.Rows.Add(row);
            row = (GuardianDataSet.SiteStatusRow)siteStatusTable.NewRow();
            row.SiteDetails = ConfigManager.Read("HourlyRun");
            row.Status = ((RESULT)Convert.ToInt32(_siteStatus.IsHourlyRun)).ToString();
            siteStatusTable.Rows.Add(row);
            row = (GuardianDataSet.SiteStatusRow)siteStatusTable.NewRow();
            row.SiteDetails = ConfigManager.Read("ReadRun");
            row.Status = ((RESULT)Convert.ToInt32(_siteStatus.IsReadRun)).ToString();
            siteStatusTable.Rows.Add(row);
            GridViewStatus.DataSource = siteStatusTable;
            GridViewStatus.DataBind();
        }
        catch (Exception ex)
        {
            LogManager.WriteLog("StatusHistory.GetHistoryTable::" + ex.Message, LogManager.enumLogLevel.Error);
        }
    }

    private void SetSiteDownInfo(SiteStatusDetails _siteStatus)
    {
        GuardianDataSet.SiteStatusDataTable siteStatusTable = new GuardianDataSet.SiteStatusDataTable();
        GuardianDataSet.SiteStatusRow row = (GuardianDataSet.SiteStatusRow)siteStatusTable.NewRow();
        row.SiteDetails = ConfigManager.Read("SiteDown");
        row.Status = (_siteStatus.IsSiteDown) ? "DOWN" : "UP";
        siteStatusTable.Rows.Add(row);
        row = (GuardianDataSet.SiteStatusRow)siteStatusTable.NewRow();
        row.SiteDetails = ConfigManager.Read("SiteDownTime");
        row.Status = DateTime.Now.ToString("G", new CultureInfo(_siteStatus.Region)); //DateTime.Now.ToString();
        siteStatusTable.Rows.Add(row);
        GridSiteStatus.DataSource = siteStatusTable;
        GridSiteStatus.DataBind();
    }
   

    private void SetHourlyNotRunInfo(SiteStatusDetails _siteStatus)
    {
        GuardianDataSet.SiteStatusDataTable siteStatusTable = new GuardianDataSet.SiteStatusDataTable();
        GuardianDataSet.SiteStatusRow row = (GuardianDataSet.SiteStatusRow)siteStatusTable.NewRow();
        row.SiteDetails = ConfigManager.Read("HourlyRun");
        row.Status = ((RESULT)Convert.ToInt32(_siteStatus.IsHourlyRun)).ToString(); 
        siteStatusTable.Rows.Add(row);
        row = (GuardianDataSet.SiteStatusRow)siteStatusTable.NewRow();
        row.SiteDetails = ConfigManager.Read("HourlyDateAndTime");
        row.Status = DateTime.Now.Date.AddHours(DateTime.Now.Hour).ToString("G", new CultureInfo(_siteStatus.Region));
        siteStatusTable.Rows.Add(row);
        row = (GuardianDataSet.SiteStatusRow)siteStatusTable.NewRow();
        row.SiteDetails = ConfigManager.Read("LastHourlyDateAndTime");
        row.Status = string.IsNullOrEmpty(_siteStatus.LastHourlyDate) ? "" : Convert.ToDateTime(_siteStatus.LastHourlyDate).ToString("d", new CultureInfo(_siteStatus.Region));// _siteStatus.LastHourCreated;
        siteStatusTable.Rows.Add(row);
        row = (GuardianDataSet.SiteStatusRow)siteStatusTable.NewRow();
        row.SiteDetails = ConfigManager.Read("LastHourRun");
        row.Status = _siteStatus.HourlyReadHour;
        siteStatusTable.Rows.Add(row);
        if (!string.IsNullOrEmpty(_siteStatus.MissedHourlies))
        {
            row = (GuardianDataSet.SiteStatusRow)siteStatusTable.NewRow();
            row.SiteDetails = ConfigManager.Read("MissedHours");
            row.Status = _siteStatus.MissedHourlies;
            siteStatusTable.Rows.Add(row);
        }
        GridSiteStatus.DataSource = siteStatusTable;
        GridSiteStatus.DataBind();
    }

    private void SetReadNotRunInfo(SiteStatusDetails _siteStatus)
    {
        GuardianDataSet.SiteStatusDataTable siteStatusTable = new GuardianDataSet.SiteStatusDataTable();
        GuardianDataSet.SiteStatusRow row = (GuardianDataSet.SiteStatusRow)siteStatusTable.NewRow();
        row.SiteDetails = ConfigManager.Read("ReadRun");
        row.Status = ((RESULT)Convert.ToInt32(_siteStatus.IsReadRun)).ToString("G", new CultureInfo(_siteStatus.Region)); 
        siteStatusTable.Rows.Add(row);
        if (!string.IsNullOrEmpty(_siteStatus.ReadTime) && _siteStatus.ReadTime.Contains(":"))
        {
            row = (GuardianDataSet.SiteStatusRow)siteStatusTable.NewRow();
            row.SiteDetails = ConfigManager.Read("ReadDateAndTime");
            row.Status = DateTime.Now.Date.AddHours(Convert.ToInt16(_siteStatus.ReadTime.Split(':')[0])).AddMinutes(Convert.ToInt16(_siteStatus.ReadTime.Split(':')[1])).ToString("G", new CultureInfo(_siteStatus.Region));
            siteStatusTable.Rows.Add(row);
        }
        row = (GuardianDataSet.SiteStatusRow)siteStatusTable.NewRow();
        row.SiteDetails = ConfigManager.Read("LastReadDateAndTime");
        row.Status = string.IsNullOrEmpty(_siteStatus.ReadDate) ? "" : Convert.ToDateTime(_siteStatus.ReadDate).ToString("d", new CultureInfo(_siteStatus.Region)); //_siteStatus.LastReadCreated;
        siteStatusTable.Rows.Add(row);
        GridSiteStatus.DataSource = siteStatusTable;
        GridSiteStatus.DataBind();
    }

    protected void GridSiteStatus_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    

}
