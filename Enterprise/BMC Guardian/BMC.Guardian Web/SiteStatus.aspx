<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SiteStatus.aspx.cs" Inherits="StatusHistory" %>

<%@ Register TagPrefix="uc1" TagName="DetailControl" Src="~/DetailControl.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>BMC Guardian - Site Status Details</title>
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
    <div class="title_bar">
        
        
       <div class="alignleft"><span>SiteName:</span>               
           </span><asp:Label Font-Size="10" ID="LabelSite" runat="server" Font-Bold="true"></asp:Label></span></div>
                    
       <div class="alignright">       
              <span>Last Updated Timestamp:</span> 
              <span><asp:Label ID="LabelLastUpdate" Font-Size="10" runat="server" Font-Bold="true"></asp:Label></span> </div>  
         
        
           
        <!--table width="100%" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    <h5>SiteName:</h5>
                </td>
                <td>
                    <asp:Label Font-Size="10" ID="Label1" runat="server" Font-Bold="true"></asp:Label>
                </td>
                <td align="right">
                
                    <h5>Last Updated Timestamp:</h5> 
                </td>
                <td>
                    <asp:Label ID="Label2" Font-Size="10" runat="server" Font-Bold="true"></asp:Label>
                </td>
            </tr>
        </table-->
        
        
    </div>
    <table>
    <tr><td></td></tr>
    <tr><td><br /></td></tr>
    <tr><td> <div  id="Summary" runat="server" >
        <table border="0" width="97%" class="title_bar">
            <tr>
                <td colspan="5">
                    <div class="datagrid">
                        <asp:GridView ID="GridViewStatus" runat="server" AllowSorting="True"  Font-Size="Medium"
                            AutoGenerateColumns="False" BackColor="White" BorderColor="White" 
                            BorderStyle="None" CaptionAlign="Left" CellPadding="5" CssClass="datagrid" 
                            ForeColor="#000066" OnRowDataBound="GridViewStatus_RowDataBound" PageSize="5">
                            <RowStyle BackColor="White" BorderColor="White" CssClass="row2" />
                            <SelectedRowStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            <HeaderStyle BackColor="#499FBF" BorderColor="#333333" />
                            <AlternatingRowStyle BackColor="#F1F1F1" />
                            <Columns>
                                <asp:TemplateField HeaderStyle-Width="20" HeaderText="Status" 
                                    ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Image ID="Image34" runat="server" ImageUrl="~/Images/Running.gif" />
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="column_header" ForeColor="Black" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="SiteDetails">
                                <ItemTemplate>
                                    <asp:LinkButton Text='<%# DataBinder.Eval(Container, "DataItem.SiteDetails") %>' ToolTip='<%# DataBinder.Eval(Container, "DataItem.SiteDetails") %>'
                                        ID="SiteDetails" runat="server" CssClass="padding5px" ForeColor="Black"
                                        OnClick="SiteDetails_Clicked" OnClientClick="ShowDetailsDiv();"></asp:LinkButton>
                                </ItemTemplate>
                                <HeaderStyle ForeColor="Black" CssClass="column_header" />
                            </asp:TemplateField>
                                <%--<asp:BoundField DataField="SiteDetails" HeaderText="Site Details" 
                                    ItemStyle-HorizontalAlign="Left">
                                    <HeaderStyle CssClass="column_header" ForeColor="Black" />
                                </asp:BoundField>--%>
                                <asp:BoundField DataField="Status" HeaderText="Status" 
                                    ItemStyle-HorizontalAlign="center">
                                    <HeaderStyle CssClass="column_header" ForeColor="Black" />
                                </asp:BoundField>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <div id="GridMessage" align="center" runat="server" class="message" style="display: none">
                        <p>
                            Site status not available.</p>
                    </div>
                </td>
            </tr>
            
        </table>
    </div></td></tr>
   
    </table>
    
    
    <div id="Details" runat="server" style="display:none;" >
    <asp:ImageButton ID="Imageaa" runat="server" OnClick="lnkBack_Clicked" 
            OnClientClick="ShowSummaryDiv();" ImageUrl="~/Images/BackButton.png" 
            Height="60px" Width="60px" ToolTip="Click to go Back" />
   <%-- <uc1:DetailControl ID="detailControl" runat="server" Visible="true" />--%>
   <div class="datagrid">
                        <asp:GridView ID="GridSiteStatus" runat="server" AllowSorting="True"  Font-Size="Medium"
                            AutoGenerateColumns="False" BackColor="White" BorderColor="White" 
                            BorderStyle="None" CaptionAlign="Left" CellPadding="5" CssClass="datagrid" 
                            ForeColor="#000066" OnRowDataBound="GridSiteStatus_RowDataBound" PageSize="5">
                            <RowStyle BackColor="White" BorderColor="White" CssClass="row2" />
                            <SelectedRowStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            <HeaderStyle BackColor="#499FBF" BorderColor="#333333" />
                            <AlternatingRowStyle BackColor="#F1F1F1" />
                            <Columns>
                                <asp:BoundField DataField="SiteDetails" HeaderText="Site Details" 
                                    ItemStyle-HorizontalAlign="Center">
                                    <HeaderStyle CssClass="column_header" ForeColor="Black" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Status" HeaderText="Status" 
                                    ItemStyle-HorizontalAlign="center">
                                    <HeaderStyle CssClass="column_header" ForeColor="Black" />
                                </asp:BoundField>
                            </Columns>
                        </asp:GridView>
                    </div>
   
    </div>
    </form>
    </center>
</body>
</html>
