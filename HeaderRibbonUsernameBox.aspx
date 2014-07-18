<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HeaderRibbonUsernameBox.aspx.cs" Inherits="HeaderRibbonUsernameBox" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title><link rel="stylesheet" href="Styles/pure-min.css"/>
    <link rel="stylesheet" href="Styles/email.css"/>
</head>
<body>
    <form id="form1" runat="server">
    <div id="headerRibbonUsernameBox" runat="server" >
        <asp:Image id="headerRibbonUsernameBoxImage" runat="server" Width= "100px" Height= "100px"/>
        <div style="float:right;display:inline-block;">
            <ul style="margin-left:-20px;">
                <li class="pure-menu-heading"><a href="#" style="color:rgb(64, 64, 64);" id="headerRibbonUsernameBoxName" runat="server">Aayushi Malviya</a></li>
                <li style="padding-bottom:10px;"><a href="#" style="font-size:small;color:rgb(64, 64, 64);" id="headerRibbonUsernameBoxPost" runat="server">Sr. Software Developer</a></li>
                <li><a class="button-secondary pure-button" href="Profile.aspx"  target="_blank" style="background: #1b98f8;color:White;" id="headerRibbonUsernameBoxViewProfile" runat="server">View Profile</a></li>
            </ul>
        </div>
        <br style="clear:both;"/>
            <div style="float:right;"><a runat="server" id="logoutButton" class="secondary-button pure-button" href="#" onserverclick="LogoutButton_Click">Logout</a></div>
            <div style="float:left;"><a runat="server" id="adminButton" class="secondary-button pure-button" href="Admin.aspx"  target="_blank">Admin</a></div>
    </div>
    </form>
</body>
</html>
