<%@ Page Language="C#" AutoEventWireup="true" Inherits="Login"  Codebehind="Login.aspx.cs" %>

<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="en" lang="en">
<head>
    <title>Bally BMC Guardian</title>
    <link rel="stylesheet" media="screen, projection" href="css/layout.css" />
</head>
<body>
    <form id="LoginForm" runat="server">
    <table height="97%" cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
            <td>
                <table cellspacing="0" cellpadding="0" border="0" background="images/loginbg.jpg"
                    style="background-repeat: no-repeat;" width="864" height="430" align="center">
                    <tr>
                        <td>
                            <div style="padding-left: 545px; padding-top: 120px;">
                                <table cellspacing="0" cellpadding="0" width="30%" border="0">
                                    <tr>
                                        <td class="white_text" valign="top" align="left" width="100">
                                            UserName:
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="left" height="30">
                                            <asp:TextBox ID="UserNameTextBox" runat="server" class="formfield" tabindex="1" 
                                                Width="240px"/>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="white_text" valign="top" align="left">
                                            Password:
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="left" height="30">
                                            <asp:TextBox ID="PasswordTextBox" runat="server" class="formfield" tabindex="2" 
                                                TextMode="Password" Width="240px" />
                                        </td>
                                    </tr>
                                    <tr style="height: 40px">
                                        <td>
                                            <asp:ImageButton ID="LoginButton" runat="server" 
                                                ImageUrl="images/login_btn.jpg" onclick="LoginButton_Click" />
                                        </td>
                                    </tr>
                                    <tr><td class="white_text"><%=Message%> </td></tr>
                                </table>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td class="copyrightstext">
                                        <div style="padding-top: 30px;">
                                            © <%=DateTime.Now.Year%> Bally Gaming, Inc. All rights reserved.</div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
    </div>
</body>
</html>
