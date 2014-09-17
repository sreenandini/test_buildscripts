<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="DetailControl" Codebehind="DetailControl.ascx.cs" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<link rel="stylesheet" media="screen, projection" href="css/layout.css" />
<div>
    <table border="0" cellpadding="0" cellspacing="0" class="title_bar">
    <tr>
	<td  align="left">
	    <b>SiteName&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp:&nbsp;</b>
	</td>
	<td align="left">
	    <asp:Label ID="LabelSite" runat="server" Font-Bold="true"></asp:Label>
	</td>
   <td align="right" rowspan="2" style="vertical-align:central" >
	    <a href="#" target="StatusHisotry" runat="server" id="detailsref" onclick="childwindow = window.open('statusHistory.aspx', 
	'StatusHistory','width=700,height=800,toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=0,modal=yes');
    return false;"><b>History </b></a>
	</td>
	</tr>
	<tr>	
	<td align="left" width="85px" >
	    <b>Last Update&nbsp;&nbsp;:</b>
	</td>
	<td colspan="2" align="left">
	    <asp:Label ID="LabelLastUpdate" runat="server" Font-Bold="true"></asp:Label>
	</td>

    </tr>
</table>
    <br />
    <div runat="server" id="div_Info" >
    <div class="title_bar">
        <b>Services</b>
    </div>
    <br />
    <div class="datagrid">
        <asp:GridView ID="GridViewServices" runat="server" AutoGenerateColumns="False" ForeColor="#000066"
            BackColor="White" BorderColor="White" BorderStyle="None" CaptionAlign="Left"
            CellPadding="5" CssClass="datagrid" PageSize="5" AllowSorting="True" OnRowDataBound="GridViewServices_RowDataBound">
            <RowStyle CssClass="row2" BorderColor="White" BackColor="White" />
            <SelectedRowStyle HorizontalAlign="Center" VerticalAlign="Top" />
            <HeaderStyle BackColor="#499FBF" BorderColor="#333333" />
            <AlternatingRowStyle BackColor="#F1F1F1" />
            <Columns>
                <asp:TemplateField HeaderText="Status" HeaderStyle-Width="20" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/Running.gif" />
                    </ItemTemplate>
                    <HeaderStyle ForeColor="Black" CssClass="column_header" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:BoundField DataField="ServiceName" HeaderText="Service Name" ItemStyle-HorizontalAlign="Left">
                    <HeaderStyle ForeColor="Black" CssClass="column_header" />
                </asp:BoundField>
                <asp:BoundField DataField="ServiceStatus" HeaderText="Status" ItemStyle-HorizontalAlign="Left">
                    <HeaderStyle ForeColor="Black" CssClass="column_header" />
                </asp:BoundField>
                
                
            </Columns>
        </asp:GridView>
    </div>
    <%--<br />--%>
    <br />
    <div id="divSiteStatus" class="datagrid">
    <table id="tblStatus" width="100%" border="0" style="margin:[0px][0px][0px][0px]" runat=server>
    <tr>
    <td style="width:10%;text-align:center"><asp:Image ID="ImgSiteStatus" runat="server" ImageUrl="~/Images/Failure.gif" /></td>
    <td style="width:50%;text-align:left">Site Status</td><td style="width:40%;text-align:left"><a href="#" target="SiteStatus" runat="server" id="A1" onclick="childwindow = window.open('siteStatus.aspx', 
                'SiteStatus','width=700,height=300,toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=0,modal=yes');
            return false;"><b>View </b></a></td></tr></table>
    </div>
   <%-- <div class="title_bar">
        <b>Site Status </b> 
    </div>--%>
    
    <%--<br />--%>
    <br />
    <div class="title_bar">
        <table width="100%">
            <tr>
                <td width="85">
                    <b>Site info </b>
                </td>
                <td width="42%" style="font-weight:bold" ><asp:Literal  runat="server" ID="lt_GMUServer"></asp:Literal></td>
                <td width="44%" style="font-weight:bold" ><asp:Literal  runat="server" ID="lt_GMUMachine"></asp:Literal></td>
            </tr>
        </table>
    </div>
    <div class="datagrid">
        <asp:GridView ID="GridViewSiteInfo" runat="server" AutoGenerateColumns="False" ForeColor="#000066"
            BackColor="White" BorderColor="White" BorderStyle="None" CaptionAlign="Left"
            CssClass="datagrid" PageSize="5" AllowSorting="True" 
            OnRowDataBound="GridViewSiteInfo_RowDataBound" 
            onrowcommand="GridViewSiteInfo_RowCommand">
            <RowStyle CssClass="row2" BorderColor="White" BackColor="White" />
            <SelectedRowStyle HorizontalAlign="Center" VerticalAlign="Top" />
            <HeaderStyle BackColor="#499FBF" BorderColor="#333333" />
            <AlternatingRowStyle BackColor="#F1F1F1" />
            <Columns>
                <asp:BoundField DataField="Position" HeaderText="Position" ItemStyle-HorizontalAlign="Left"
                    ItemStyle-Width="20" HeaderStyle-Width="20">
                    <HeaderStyle ForeColor="Black" CssClass="column_header" />
                    <ItemStyle HorizontalAlign="Left" Width="20px"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="Asset" HeaderText="Asset" ItemStyle-HorizontalAlign="Left"
                    ItemStyle-Width="60" HeaderStyle-Width="60">
                    <HeaderStyle ForeColor="Black" CssClass="column_header" />
                    <ItemStyle HorizontalAlign="Left" Width="60px"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="GMUToServer" HeaderText="GMU->Server" ItemStyle-Width="90"
                    HeaderStyle-Width="90">
                    <HeaderStyle ForeColor="Black" CssClass="column_header" Wrap="false" />
                    <ItemStyle Width="90px"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="GMUToMachine" HeaderText="GMU->Machine" ItemStyle-Width="90"
                    HeaderStyle-Width="90">
                    <HeaderStyle ForeColor="Black" CssClass="column_header" Wrap="false" />
                    <ItemStyle Width="90px"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="GMUVersion" HeaderText="GMU_Version" ItemStyle-Width="90"
                    HeaderStyle-Width="90">
                    <HeaderStyle ForeColor="Black" CssClass="column_header" Wrap="false" />
                    <ItemStyle Width="90px"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="MachineStatus" HeaderText="Machine Status" ItemStyle-HorizontalAlign="Left"
                    ItemStyle-Width="60" HeaderStyle-Width="60">
                    <HeaderStyle ForeColor="Black" CssClass="column_header" />
                    <ItemStyle HorizontalAlign="Left" Width="60px"></ItemStyle>
                </asp:BoundField>
                <asp:ButtonField HeaderText="Reboot GMU" Text="Reboot" ButtonType="Button" HeaderStyle-ForeColor="Black"
                    HeaderStyle-CssClass="column_header" ItemStyle-Width="50" HeaderStyle-Width="50"
                    CommandName="Reboot">                   
                </asp:ButtonField>
                <asp:TemplateField HeaderText="Reboot Status"></asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    <br />
    <div class="title_bar">
        <b>Other Details</b>
    </div>
    <div class="content" style="height: 180px; margin-bottom: 1px;">
        <table align="center" width="100%" border="1" bordercolor="#CCCCCC" style="color: #000000;
            font-weight: normal; height: 100%; border-collapse: collapse" rules="rows" cellpadding="3">
            <tr class="row3">
                <td class="style5" align="left">
                    Last record exported:
                </td>
                <td class="fontbold" align="left">
                    <asp:Label ID="LabelFIFOLastRecordExported" runat="server"></asp:Label>
                </td>
            </tr>
            <tr class="row3">
                <td class="style5" align="left">
                    Records to process:
                </td>
                <td class="fontbold" align="left">
                    <asp:Label ID="LabelFIFORecordsToProcess" runat="server"></asp:Label>
                </td>
            </tr>
            <tr class="row3">
                <td class="style5" align="left">
                    Last drop created:
                </td>
                <td class="fontbold" align="left">
                    <asp:Label ID="LabelLastDropCreated" runat="server"></asp:Label>
                </td>
            </tr>
            <tr class="row3">
                <td class="style5" align="left">
                    Last read created:
                </td>
                <td class="fontbold" align="left">
                    <asp:Label ID="LabelLastReadCreated" runat="server"></asp:Label>
                </td>
            </tr>
            <tr class="row3">
                <td class="style5" align="left">
                    Last hourly created:
                </td>
                <td class="fontbold" align="left">
                    <asp:Label ID="LabelLastHourlyCreated" runat="server"></asp:Label>
                </td>
            </tr>
            <tr class="row3">
                <td class="style5" align="left">
                    Exchange DB Version:
                </td>
                <td class="fontbold" align="left">
                    <asp:Label ID="LabelExchangeDBVersion" runat="server"></asp:Label>
                </td>
            </tr>
            <%--<tr class="row3">
              <td class="style5" align="left">
                    Exchange patch version:
                </td>
                <td class="fontbold" align="left">
                    <asp:Label ID="LabelExchangePatchVersion" runat="server"></asp:Label>
                </td>
            </tr>--%>
        </table>
    </div>
    <br />
    <div class="title_bar">
        <b>Disk Space </b>
    </div>
    <div class="datagrid">
        <asp:GridView ID="GridViewDisk" runat="server" AutoGenerateColumns="False" ForeColor="#000066"
            BackColor="White" BorderColor="White" BorderStyle="None" CaptionAlign="Left"
            CellPadding="5" CssClass="datagrid" PageSize="5" AllowSorting="True" OnRowDataBound="GridViewDisk_RowDataBound">
            <RowStyle CssClass="row2" BorderColor="White" BackColor="White" />
            <SelectedRowStyle HorizontalAlign="Center" VerticalAlign="Top" />
            <HeaderStyle BackColor="#499FBF" BorderColor="#333333" />
            <AlternatingRowStyle BackColor="#F1F1F1" />
            <Columns>
                <asp:TemplateField HeaderText="Status" HeaderStyle-Width="20" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Image ID="Image34" runat="server" ImageUrl="~/Images/Running.gif" />
                    </ItemTemplate>
                    <HeaderStyle ForeColor="Black" CssClass="column_header" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:BoundField DataField="DriveName" HeaderText="Drive" ItemStyle-HorizontalAlign="Left">
                    <HeaderStyle ForeColor="Black" CssClass="column_header" />
                </asp:BoundField>
                <asp:BoundField DataField="DriveSpace" HeaderText="Space Available(in MB)" ItemStyle-HorizontalAlign="Right">
                    <HeaderStyle ForeColor="Black" CssClass="column_header" />
                </asp:BoundField>
            </Columns>
        </asp:GridView>
    </div>
    <br />
    <div class="title_bar">
        <b>System Startup/ShutDown Logs </b>
    </div>
    <div class="datagrid">
        <asp:GridView ID="SystemLogView" runat="server" AutoGenerateColumns="False" ForeColor="#000066"
            BackColor="White" BorderColor="White" BorderStyle="None" CaptionAlign="Left"
            CellPadding="5" CssClass="datagrid" PageSize="5" AllowSorting="True">
            <RowStyle CssClass="row2" BorderColor="White" BackColor="White" />
            <SelectedRowStyle HorizontalAlign="Center" VerticalAlign="Top" />
            <HeaderStyle BackColor="#499FBF" BorderColor="#333333" />
            <AlternatingRowStyle BackColor="#F1F1F1" />
            <Columns>
                <asp:BoundField DataField="Message" HeaderText="Message" ItemStyle-HorizontalAlign="Left">
                    <HeaderStyle ForeColor="Black" CssClass="column_header" />
                </asp:BoundField>
                <asp:BoundField DataField="TimeGenerated" HeaderText="Time" ItemStyle-HorizontalAlign="Left">
                    <HeaderStyle ForeColor="Black" CssClass="column_header" />
                </asp:BoundField>
            </Columns>
        </asp:GridView>
    </div>
    <br />
    <div class="title_bar">
        <b>General Information</b>
    </div>
    </div>
    <div class="generalinfo">
        <p id="GeneralInformation" class="padding5px" runat="server">
        </p>
    </div>
</div>
