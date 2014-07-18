<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="WebApplication2.Profile" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Email &ndash; OAP.v.02</title><link rel="stylesheet" href="Styles/pure-min.css"/>
        <link rel="stylesheet" href="Styles/email.css"/>
</head>
<body>
    <form id="form1" runat="server" class="pure-form">
    
    <div id="header_ribbon">
        <div style="float:right;right:10px;">
            <div id="headerRibbonUsernameDiv" class="header_ribbon_element" runat="server">
                <a id="headerRibbonUsername" href="#" style="color:White;" runat="server" onserverclick="HeaderRibbonUsername_Click">
                        Username
                </a>
                <div id="headerRibbonUsernameBox" runat="server" style="display:none;">
                    <asp:Image id="headerRibbonUsernameBoxImage" runat="server" Width= "100px" Height= "100px" ImageUrl="images/usericon.jpg"/>
                    <div style="float:right;display:inline-block;">
                        <ul style="margin-left:-20px;">
                            <li class="pure-menu-heading"><a href="#" style="color:rgb(64, 64, 64);" id="headerRibbonUsernameBoxName" runat="server">Aayushi Malviya</a></li>
                            <li style="padding-bottom:10px;"><a href="#" style="font-size:small;color:rgb(64, 64, 64);" id="headerRibbonUsernameBoxPost" runat="server">Sr. Software Developer</a></li>
                            <li><a class="button-secondary pure-button" href="#" style="background: #1b98f8;color:White;" id="headerRibbonUsernameBoxViewProfile" runat="server">View Profile</a></li>
                        </ul>
                        <div style="float:right;"><a runat="server" id="logoutButton" class="secondary-button pure-button" href="#" onserverclick="LogoutButton_Click">Logout</a></div>
                    </div>
                </div>
            </div>
            <div  class="header_ribbon_element">
                <a id="A2" href="#" style="color:White;" runat="server">Sites</a>
            </div>
            <div  class="header_ribbon_element">
                <a id="A3" href="calender.htm"  target="_blank" style="color:White;" runat="server">Calendar</a>
            </div>
            <div  class="header_ribbon_element">
                <a id="A4" href="#" style="color:White;" runat="server">People</a>
            </div>
            <br style="clear:right;"/>
        </div>
        <div class="app_name">
            OAP.v.02
        </div>
    </div>

    <div id="layout" class="content pure-g">

        <div id="nav" class="pure-u">

            <div class="nav-inner">
                    <asp:Button id="inbox_button" cssclass="primary-button pure-button" Text="Compose" runat="server"/>

                    <div class="pure-menu pure-menu-open">
                        <ul>
                            <li><a id="A5" href="#" runat="server">Inbox</a></li>
                            <li><a id="A6" href="#" runat="server">Important</a></li>
                            <li><a id="A7" href="#" runat="server">Sent</a></li>
                            <li><a id="A8" href="#" runat="server">Trash</a></li>
                            <li><a id="A9" href="#" runat="server">Leave Account</a></li>
                            <li><a id="A10" href="#" runat="server">LTC Record</a></li>
                            <li><a id="A11" href="#" runat="server">LTC Declaration</a></li>
                            <li><a id="A12" href="#" runat="server">Service Register</a></li>
                        </ul>
                    </div>
            </div>
        </div>

        <div id="navigation_result" class="navigation_result_wrapper">

            <div class="pure-menu pure-menu-open pure-menu-horizontal gmail-tabs" >
                <ul>
                    <li class="gmail-tabs-selected" id="profileTab" runat="server"><a href="#" runat="server" id="profile" onserverclick="Profile_Click">Profile</a></li>
                    <li id="leaveAccountTab" runat="server"><a href="#" runat="server" id="leaveAccount" onserverclick="LeaveAccount_Click">Leave Account</a></li>
                    <li id="ltcRecordTab" runat="server"><a href="#" runat="server" id="ltcRecord" onserverclick="LtcRecord_Click">LTC Record</a></li>
                    <li id="ltcDeclarationTab" runat="server"><a href="#" runat="server" id="ltcDeclaration" onserverclick="LtcDeclaration_Click">LTC Declaration</a></li>
                    <li id="serviceRegisterTab" runat="server"><a href="#" runat="server" id="serviceRegister" onserverclick="ServiceRegister_Click">Service Register</a></li>
                </ul>
            </div>
            <div class="main" style="margin-top:5px;" id="service_book_container">
                <asp:PlaceHolder runat="server" id="service_book_container_placeholder" />
            </div>

        </div>

        <br style="clear:left;" />
    </div>
    </form>
</body>
</html>

