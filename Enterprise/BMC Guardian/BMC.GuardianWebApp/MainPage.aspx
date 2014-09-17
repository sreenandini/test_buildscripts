<%@ Page Language="C#" AutoEventWireup="true" Inherits="MainPage" Codebehind="MainPage.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="DetailControl" Src="~/DetailControl.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="en" lang="en">
<head runat="server">
    <title>Bally MultiConnect Guardian</title>
    <link rel="stylesheet" media="screen, projection" href="css/layout.css" />
    <style type="text/css">
        .style1
        {
            width: 63%;
        }
    </style>
    <script type="text/javascript">
        var childwindow;
        function ClosePopup() {
            if (childwindow != null)
                childwindow.close();            
        }
    </script>
</head>
<body onunload="ClosePopup()">
    <form id="Form1" runat="server">
    <asp:ScriptManager ID="scriptManager" runat="server" />
    <div id="wrapper">
        <div class="header">
            <table class="white_text" width="100%" height="30" cellpadding="0" cellspacing="0"
                border="0">
                <tr>
                    <td align="left">
                        <img src="images/logo.png" alt="Logo" />
                    </td>
                    <td width="25%" valign="middle" align="center">
                        Logged in as <b>
                            <%=UserName %>
                            | <a href="Login.aspx">Sign out</a></b>
                    </td>
                </tr>
            </table>
            <div class="mainbar">
                <table class="white_text" width="100%" height="26px" cellpadding="0" cellspacing="0"
                    border="0">
                    <tr>
                        <td align="left" style="padding-left: 10px; width: 75%;">
                            <asp:UpdatePanel ID="updatePanel1" runat="server">
                                <ContentTemplate>
                                &nbsp;Site Information:&nbsp;
                                     <%=Status %>
                                     </ContentTemplate>
                              </asp:UpdatePanel>
                        </td>
                        <td align="right">
                            <asp:Button Visible="true" ID="btnRefreshPage" runat="server" Text="Refresh" OnClick="btnRefreshPage_Click" />
                            <asp:Button Visible="false" ID="btnRequestNow" runat="server" Text="Request Status Now" OnClick="btnRequestNow_Click" />
                        </td>
                        <td style="width: 25%;" align="center">
                            <asp:DropDownList ID="DropDownListInterval" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownListInterval_SelectedIndexChanged">
                                <asp:ListItem Text="30 secs" Value="30000"></asp:ListItem>
                                <asp:ListItem Text="1 min" Value="60000"></asp:ListItem>
                                <asp:ListItem Text="5 mins" Value="300000"></asp:ListItem>
                                <asp:ListItem Text="15 mins" Value="900000"></asp:ListItem>
                                <asp:ListItem Text="30 mins" Value="1800000"></asp:ListItem>
                                <asp:ListItem Text="1 hour" Value="3600000"></asp:ListItem>
                            </asp:DropDownList>
                            <asp:CheckBox ID="CheckBoxAutoRefresh" runat="server" Text="Auto refresh" AutoPostBack="True"
                                OnCheckedChanged="CheckBoxAutoRefresh_CheckedChanged" />
                        </td>
                            
                        <td style="width: 18%;" align="center" valign="middle">
                            <asp:UpdateProgress ID="updateProgress" runat="server" AssociatedUpdatePanelID="updatePanel"
                                Visible="true">
                                <ProgressTemplate>
                                    <img src="Images/Running1.gif" alt="Loading" />
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                        </td>
                    
                    </tr>
                </table>
            </div>
        </div>
        <asp:UpdatePanel ID="updatePanel" runat="server">
            <ContentTemplate>
                <asp:Timer ID="SingleRunTimer" runat="server" Interval="1000" OnTick="SingleRunTimer_Tick"
                    Enabled="True">
                </asp:Timer>
                
                <asp:Timer ID="UpdateTimer" runat="server" Interval="30000" OnTick="UpdateTimer_Tick"
                    Enabled="False">
                </asp:Timer>
                <div id="MainDiv" class="main">
                    <!-- Left container -->
                    <div id="left_column">
                        <div class="left_title_bar">
                            <table width="99%" border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <b>Sites</b>
                                    </td>
                                    <td align="right">
                                        <asp:CheckBox ID="chkIsActive" Checked="true" runat="server" Text="Active Sites Only" />
                                    </td>
                                    <td align="right">
                                        <asp:DropDownList ID="DropDownListFilter" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownFilter_SelectedIndexChanged">
                                            <asp:ListItem>All</asp:ListItem>
                                            <asp:ListItem>Running</asp:ListItem>
                                            <asp:ListItem>Stopped</asp:ListItem>
                                            <asp:ListItem>Unknown</asp:ListItem>
                                            <asp:ListItem>Terminated</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td align="center">
                                        <asp:ImageButton ID="RefreshButton" runat="server" ImageUrl="Images/refresh.gif"
                                            alt="Refresh" OnClick="RefreshButton_Click" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="tree_panel">
                            <asp:TreeView ID="TreeViewSites" runat="server" BorderStyle="None" Width="272px"
                                CssClass="black_text" Font-Names="Arial" Font-Size="Medium" OnSelectedNodeChanged="TreeViewSites_SelectedNodeChanged">
                                <ParentNodeStyle Font-Bold="False" />
                                <HoverNodeStyle Font-Underline="True" ForeColor="#5555DD" />
                                <SelectedNodeStyle Font-Underline="True" ForeColor="#0033CC" HorizontalPadding="0px"
                                    VerticalPadding="0px" />
                                <NodeStyle Font-Names="Arial" Font-Size="9pt" ForeColor="Black" HorizontalPadding="5px"
                                    NodeSpacing="0px" VerticalPadding="0px" />
                            </asp:TreeView>
                            <div id="TreeViewMessage" align="center" runat="server" class="message" style="color: #FF0000;
                                display: none">
                                <p>
                                    No sites available.</p>
                            </div>
                        </div>
                    </div>
                    <div id="right_column">
                        <div id="MainView" runat="server" class="MainView" style="display: block;">
                            <div class="title_bar">
                                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td>
                                            Last Record Exported from Enterprise: 
                                            <asp:Label ID="lblLstRecExpFrmEnt" runat="server" Font-Bold="true"></asp:Label>
                                        </td>
                                        <td>
                                            Records to be Processed:
                                            <asp:Label ID="lblRecToPro" runat="server" Font-Bold="true"></asp:Label>
                                        </td>
                                        <td>
                                            Up and Running Sites:
                                            <asp:Label ID="lblUpnRunSite" runat="server" Font-Bold="true"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div class="title_bar">
                                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td>
                                            <b>Sites</b>
                                        </td>
                                        <td>
                                            <span class="filter">
                                                <asp:DropDownList ID="DropDownListFilterGrid" runat="server" AutoPostBack="True"
                                                    OnSelectedIndexChanged="DropDownFilter_SelectedIndexChanged">
                                                    <asp:ListItem>All</asp:ListItem>
                                                    <asp:ListItem>Running</asp:ListItem>
                                                    <asp:ListItem>Stopped</asp:ListItem>
                                                    <asp:ListItem>Unknown</asp:ListItem>
                                                    <asp:ListItem>Terminated</asp:ListItem>
                                                </asp:DropDownList>
                                            </span>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div class="datagridMain" style="height: 625px;">
                                <asp:GridView ID="GridViewSites" runat="server" Width="97%" AutoGenerateColumns="False"
                                    ForeColor="#000066" BackColor="White" BorderColor="White" BorderStyle="None"
                                    CaptionAlign="Left" CellPadding="0" CssClass="datagrid" PageSize="5" AllowSorting="True"
                                    OnRowDataBound="GridViewSites_RowDataBound">
                                    <RowStyle CssClass="row1" BorderColor="White" BackColor="White" />
                                    <Columns>
                                        <asp:TemplateField HeaderStyle-Width="20" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="StatusLinkButton" runat="server">
                                                    <asp:Image ID="ImageStatus" runat="server" ImageUrl="~/Images/Running.gif" /></asp:LinkButton></ItemTemplate>
                                            <HeaderStyle ForeColor="Black" CssClass="column_header" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Site Name">
                                            <ItemTemplate>
                                                <asp:LinkButton Text='<%# DataBinder.Eval(Container, "DataItem.SiteName") %>' ID="SiteNameLinkButton"
                                                    runat="server" CssClass="padding5px" ForeColor="Black" OnClick="SiteNameLinkButton_Clicked"></asp:LinkButton></ItemTemplate>
                                            <HeaderStyle ForeColor="Black" CssClass="column_header" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Services" HeaderText="Services">
                                            <ControlStyle CssClass="padding5px" />
                                            <HeaderStyle ForeColor="Black" CssClass="column_header" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="FIFO" HeaderText="FIFO">
                                            <ControlStyle CssClass="padding5px" />
                                            <HeaderStyle ForeColor="Black" CssClass="title_bar" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="LastUpdate" HeaderText="Last Update">
                                            <ControlStyle CssClass="padding5px" />
                                            <HeaderStyle ForeColor="Black" CssClass="column_header" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="LastRead" HeaderText="Last Drop">
                                            <ControlStyle CssClass="padding5px" />
                                            <HeaderStyle ForeColor="Black" CssClass="column_header" />
                                        </asp:BoundField>
                                    </Columns>
                                    <SelectedRowStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <HeaderStyle BackColor="White" BorderColor="#333333" Wrap="True" />
                                    <AlternatingRowStyle BackColor="#F1F1F1" />
                                </asp:GridView>
                                <div id="GridMessage" align="center" runat="server" class="message" style="display: none">
                                    <p>
                                        No sites available.</p>
                                </div>
                            </div>
                        </div>
                        <div id="DetailsView" runat="server" class="DetailsView" style="display: none;">
                            <uc1:DetailControl ID="detailControl" runat="server" Visible="true" />
                        </div>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="DropDownListInterval" EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="CheckBoxAutoRefresh" EventName="CheckedChanged" />
            </Triggers>
        </asp:UpdatePanel>
        <div class="copyrightstext" style="padding-left: 10px;">
            © <%=DateTime.Now.Year%> Bally Gaming, Inc. All rights reserved.
        </div>
    </div>
    </form>
</body>
</html>
