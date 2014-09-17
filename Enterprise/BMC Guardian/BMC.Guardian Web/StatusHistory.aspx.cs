using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using BMC.Guardian.DBHelper;
using BMC.Guardian.Business;
using BMC.Guardian.Transport;
using BMC.Common.LogManagement;
using BMC.Common.ConfigurationManagement;
using ImageControl = System.Web.UI.WebControls.Image;

public partial class StatusHistory : System.Web.UI.Page
{
    int CurrentPage = 1;
    int NoofPages = 1;
    int pageSize = 25;
    protected void Page_Load(object sender, EventArgs e)
    {
        string SiteName = "" ;
        try
        {
            pageSize = Convert.ToInt32(ConfigManager.Read("RecordsToDsiplay"));
        }
        catch  {}
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
            string SiteCode = GuardianDBHelper.GetSiteCode(SiteName);
            LabelSite.Text = SiteName;
            string siteStatus = GuardianDBHelper.GetSiteStatus(SiteName);
            GuardianDataSet.StatusHistoryDataTable history = GuardianBL.GetStatusHistory(SiteCode);
            if (history.Count ==0)
            {
                GridViewSites.DataSource = null;
                GridMessage.Visible = true;
                EnablePagingButtons(false, false, false, false);
                return;
            }
            LastProcessedDetails details = GuardianBL.GetLastProcessedDetails(siteStatus);
            LabelLastUpdate.Text = details.DateTime;
            if(history.Rows.Count % pageSize == 0)
                NoofPages = Convert.ToInt32( history.Rows.Count / pageSize);
            else
                NoofPages = Convert.ToInt32(history.Rows.Count / pageSize)+1;
            Session["NoofPages"] = NoofPages;
            Session["HistoryTable"] = history;
            GridViewSites.PageSize = pageSize;
            GetHistoryTable(0, pageSize);
            CurrentPage = 1;
            Session["CurrentPage"] = CurrentPage;
            
            EnablePagingButtons(false, false, true, true);
            if (history.Count <= pageSize)
            {
                EnablePagingButtons(false, false, false, false);
            }
        }
    }

    protected void TimeStampLinkButton_Clicked(object sender, EventArgs e)
    {
        LinkButton linkButton = sender as LinkButton;
        
        if (linkButton != null)
        {          
            string timestamp = linkButton.ToolTip;
            DateTime dtTimeStamp = DateTime.MinValue;
            DateTime.TryParse(timestamp, out dtTimeStamp);

            string Status = GuardianBL.GetStatusForGivenTime(Session["SiteName"].ToString(), dtTimeStamp);
            detailControl.RebootFlag = true;
            detailControl.PoplutateSiteDetails(Session["SiteName"].ToString(), Status, true);            
            Summary.Attributes["style"] = "display: none;";
            Details.Attributes["style"] = "display: block;";
        }
    }

    protected void GridViewSites_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if ((e.Row != null) && (e.Row.RowType == DataControlRowType.DataRow ) && (e.Row.DataItem != null))
        {
            GuardianDataSet.StatusHistoryRow StatusHistoryRow = e.Row.DataItem as GuardianDataSet.StatusHistoryRow;
            ImageControl image = e.Row.Cells[0].Controls[1] as ImageControl;
            if (StatusHistoryRow.ServiceStatus.ToUpper() == "ALL SERVICES RUNNING" && StatusHistoryRow.Fifo == "0")
                image.ImageUrl = "~/Images/Success.gif";
            else
                image.ImageUrl = "~/Images/Failure.gif";
        }
    }
    protected void lnkBack_Clicked(object sender, EventArgs e)
    {
        Summary.Attributes["style"] = "display: block;";
        Details.Attributes["style"] = "display: none;";
        
    }
    private void GetHistoryTable(int SkipRecords, int takeRecords)
    {
        try
        {
            GuardianDataSet.StatusHistoryDataTable history = (GuardianDataSet.StatusHistoryDataTable)Session["HistoryTable"];
            var HistoryTable = (from h in history
                                select h).Skip(SkipRecords).Take(takeRecords);
            GridViewSites.DataSource= HistoryTable;
            GridViewSites.DataBind();
        }
        catch (Exception ex)
        {
            LogManager.WriteLog("StatusHistory.GetHistoryTable::" + ex.Message, LogManager.enumLogLevel.Error);
        }
    }

    #region PagingMethods
    private void EnablePagingButtons(bool First, bool Prev, bool Next, bool Last)
    {
        btnFirst.Enabled = First;
        btnPrev.Enabled = Prev;
        btnNext.Enabled = Next;
        btnLast.Enabled = Last;
    }

    protected void btnFirst_Click(object sender, EventArgs e)
    {
        GetHistoryTable(0, pageSize);
        CurrentPage = 1;
        EnablePagingButtons(false, false, true, true);
        Session["CurrentPage"] = CurrentPage;

    }
    protected void btnPrev_Click(object sender, EventArgs e)
    {
        CurrentPage = Convert.ToInt32(Session["CurrentPage"]);
        CurrentPage--;

        GetHistoryTable(((CurrentPage - 1) * pageSize), pageSize);
        if (CurrentPage == 1)
        {
            EnablePagingButtons(false, false, true, true);
        }
        else
            EnablePagingButtons(true, true, true, true);
        Session["CurrentPage"] = CurrentPage;
    }

    protected void btnNext_Click(object sender, EventArgs e)
    {
        NoofPages = Convert.ToInt32(Session["NoofPages"]);
        CurrentPage = Convert.ToInt32(Session["CurrentPage"]);
        GetHistoryTable((CurrentPage * pageSize), pageSize);
        CurrentPage++;
        if (CurrentPage == NoofPages)
        {
            EnablePagingButtons(true, true, false, false);
            CurrentPage = NoofPages;
        }
        else
            EnablePagingButtons(true, true, true, true);
        Session["CurrentPage"] = CurrentPage;
    }

    protected void btnLast_Click(object sender, EventArgs e)
    {
        NoofPages = Convert.ToInt32(Session["NoofPages"]);
        GetHistoryTable(((NoofPages - 1) * pageSize), pageSize);
        CurrentPage = NoofPages;
        EnablePagingButtons(true, true, false, false);
        Session["CurrentPage"] = CurrentPage;
    }
    #endregion "PagingMethods"
}
