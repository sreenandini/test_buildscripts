<%@ Page Language="C#" AutoEventWireup="true" Inherits="StatusHistory" Codebehind="StatusHistory.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="DetailControl" Src="~/DetailControl.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>BMC Guardian - Site History</title>
    <link rel="stylesheet" media="screen, projection" href="css/layout.css" />
    <script language="javascript" type="text/javascript">
        function toggleDiv(divid) {
            if (document.getElementById(divid).style.display == 'none') {
                document.getElementById(divid).style.display = 'block';
            } else {
                document.getElementById(divid).style.display = 'none';
            }
        }
        function ShowDetailsDiv() 
        {
            
            document.getElementById("Summary").style.display == 'none';
            document.getElementById("Details").style.display == 'block';
            
        }
        function ShowSummaryDiv() {
            
            document.getElementById("Summary").style.display == 'block';
            document.getElementById("Details").style.display == 'none';
            
        }
    </script>
</head>

<body  style="margin-left:0px; margin-right:0px; text-align:center; width:100%; display:block;">
<center>
    <form id="form1" runat="server">
    <table border="0" cellpadding="0" cellspacing="0" class="title_bar">
    <tr>
	<td align="left">
	    <b>SiteName&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:&nbsp;</b>
	</td>
        <td align="left">
            <b><asp:Label ID="LabelSite" runat="server" Font-Bold="true"></asp:Label></b>
        </td>
	</tr>
	<tr>	
	<td align="left" width="85px">
        <b>Last Updated&nbsp;&nbsp;:</b>
	</td>
        <td align="left">
            <b><asp:Label ID="LabelLastUpdate" runat="server" Font-Bold="true"></asp:Label></b>
        </td>
	</tr>
    </table>
    
    <table>
    <tr><td></td></tr>
    <tr><td><br /></td></tr>
    <tr><td> <div  id="Summary" runat="server" >
        <table border="0" width="97%" class="title_bar">
            <tr>
                <td colspan="5">
                    <asp:GridView ID="GridViewSites" runat="server" Width="100%" AutoGenerateColumns="False"
                        Font-Size="Medium" ForeColor="#000066" BackColor="White" BorderColor="White" OnRowDataBound="GridViewSites_RowDataBound"
                        BorderStyle="None" CaptionAlign="Left" CellPadding="4" CssClass="datagrid" AllowPaging="false">
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
                            <asp:BoundField DataField="ServiceStatus" HeaderText="Services">
                                <ControlStyle CssClass="padding5px" />
                                <HeaderStyle ForeColor="Black" CssClass="column_header" />
                            </asp:BoundField>
                            <asp:BoundField DataField="FIFO" HeaderText="FIFO" ItemStyle-HorizontalAlign="Right">
                                <ControlStyle CssClass="padding5px" />
                                <HeaderStyle ForeColor="Black" CssClass="title_bar" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="TimeStamp">
                                <ItemTemplate>
                                    <asp:LinkButton Text='<%# DataBinder.Eval(Container, "DataItem.TimeStamp") %>' ToolTip='<%# DataBinder.Eval(Container, "DataItem.UpdateTimeStamp") %>'
                                        ID="TimeStampLinkButton" runat="server" CssClass="padding5px" ForeColor="Black"
                                        OnClick="TimeStampLinkButton_Clicked" OnClientClick="ShowDetailsDiv();"></asp:LinkButton>
                                </ItemTemplate>
                                <HeaderStyle ForeColor="Black" CssClass="column_header" />
                            </asp:TemplateField>
                        </Columns>
                        <SelectedRowStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <HeaderStyle BackColor="White" BorderColor="#333333" Wrap="True" />
                        <AlternatingRowStyle BackColor="#F1F1F1" />
                    </asp:GridView>
                    <div id="GridMessage" align="center" runat="server" class="message" style="display: none">
                        <p>
                            Status History not available.</p>
                    </div>
                </td>
            </tr>
            <tr >
                <td width="10%" align="center">
                    <asp:Button runat="server" Width="90%" ID="btnFirst" Text="<<" OnClick="btnFirst_Click" CssClass="btnstyle" />
                </td>
                <td width="10%" align="center">
                    <asp:Button runat="server" Width="90%" ID="btnPrev" Text="<" OnClick="btnPrev_Click" CssClass="btnstyle"/>
                </td>
                <td width="60%">
                </td>
                <td width="10%" align="center">
                    <asp:Button runat="server" Width="90%" ID="btnNext" Text=">" OnClick="btnNext_Click" CssClass="btnstyle"/>
                </td>
                <td width="10%" align="center">
                    <asp:Button runat="server" Width="90%" ID="btnLast" Text=">>" OnClick="btnLast_Click" CssClass="btnstyle"/>
                </td>
            </tr>
        </table>
    </div></td></tr>
   
    </table>
    
    
    <div id="Details" runat="server" style="display:none;" >
    <asp:ImageButton ID="Imageaa" runat="server" OnClick="lnkBack_Clicked" 
            OnClientClick="ShowSummaryDiv();" ImageUrl="~/Images/BackButton.png" 
            Height="60px" Width="60px" ToolTip="Click to go Back" />
    <uc1:DetailControl ID="detailControl" runat="server" Visible="true" />
    </div>
    </form>
    </center>
</body>
</html>
