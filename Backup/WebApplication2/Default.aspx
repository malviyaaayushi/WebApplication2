<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebApplication2.Default" %>
        
<% if (PopupMsg!=null)
   { %>
<script type="text/JavaScript">

    function ShowMessage() {
        alert("<%=PopupMsg %>");
    }
    window.onload = ShowMessage; 

</script>
<% } %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Email &ndash; OAP.v.02</title><link rel="stylesheet" href="http://yui.yahooapis.com/pure/0.5.0/pure-min.css"/>
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
                    <asp:Image id="headerRibbonUsernameBoxImage" runat="server" Width= "100px" Height= "100px"/>
                    <div style="float:right;display:inline-block;">
                        <ul style="margin-left:-20px;">
                            <li class="pure-menu-heading"><a href="#" style="color:rgb(64, 64, 64);" id="headerRibbonUsernameBoxName" runat="server">Aayushi Malviya</a></li>
                            <li style="padding-bottom:10px;"><a href="#" style="font-size:small;color:rgb(64, 64, 64);" id="headerRibbonUsernameBoxPost" runat="server">Sr. Software Developer</a></li>
                            <li><a class="button-secondary pure-button" href="Profile.aspx"  target="_blank" style="background: #1b98f8;color:White;" id="headerRibbonUsernameBoxViewProfile" runat="server">View Profile</a></li>
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

    <div id="toolbar">
        <div id="search_box">
                <asp:TextBox runat="server" ID="search_textbox" ToolTip="Search" style="height: 30px;width: 92%;float: left;clear: both;display: inline-block;margin: 0;" />
                <asp:ImageButton id="SearchImage" runat="server"
                   AlternateText="Search"
                   Width= "30px"
                   Height= "30px"
                   ImageUrl="images/search_icon.png"/>
        </div>
    </div>

    <div id="layout" class="content pure-g">

        <div id="nav" class="pure-u">
            <a href="#" class="nav-menu-button">Menu</a>

            <div class="nav-inner">
                    <asp:Button id="inbox_button" cssclass="primary-button pure-button" Text="Compose" runat="server"/>

                    <div class="pure-menu pure-menu-open">
                        <ul>
                            <li><a id="inbox" href="#" runat="server" onserverclick="Inbox_Click">Inbox</a></li>
                            <li><a id="important" href="#" runat="server" onserverclick="Important_Click">Important</a></li>
                            <li><a id="outbox" href="#" runat="server" onserverclick="Outbox_Click">Sent</a></li>
                            <li><a id="trash" href="#" runat="server" onserverclick="Trash_Click">Trash</a></li>
                            <li><a id="leaveAccount" href="#" runat="server" onserverclick="LeaveAccount_Click">Leave Account</a></li>
                            <li><a id="ltcRecord" href="#" runat="server" onserverclick="LtcRecord_Click">LTC Record</a></li>
                            <li><a id="ltcDeclaration" href="#" runat="server" onserverclick="LtcDeclaration_Click">LTC Declaration</a></li>
                            <li><a id="serviceRegister" href="#" runat="server" onserverclick="ServiceRegister_Click">Service Register</a></li>
                        </ul>
                    </div>
            </div>
        </div>

        <div id="navigation_result" class="navigation_result_wrapper" runat="server">
            <asp:PlaceHolder runat="server" id="navigationResultPlaceHolder" />
            <div ID="boxList" runat="server">
                <asp:PlaceHolder runat="server" id="boxListItem" />
                    <%--<div class="email-item email-item-selected pure-g">
                        <div class="pure-u">
                            <div class="email-name">Aayushi Malviya<div class="mailDate">June 17(3:56pm)</div></div>
                            <h5 class="email-subject">Application for MSA</h5>
                            <div class="email-desc">
                                Hey, I would like to apply for the GSA program 2014-15. Here are my detai...
                            </div>
                        </div>
                    </div>--%>
                    <%--<ul class="pure-paginator">
                        <li><a class="pure-button prev" href="#" style="height:1em;">&#171;</a></li>
                        <li><a class="pure-button pure-button-active" href="#" style="height:1em;">1</a></li>
                        <li><a class="pure-button" href="#" style="height:1em;">2</a></li>
                        <li><a class="pure-button" href="#" style="height:1em;">3</a></li>
                        <li><a class="pure-button" href="#" style="height:1em;">4</a></li>
                        <li><a class="pure-button" href="#" style="height:1em;">5</a></li>
                        <li><a class="pure-button" href="#" style="height:1em;">6</a></li>
                        <li><a class="pure-button next" href="#" style="height:1em;">&#187;</a></li>
                    </ul>--%>
            </div>

            <div id="main" class="pure-u-1">
                <asp:PlaceHolder runat="server" id="EmailContentPlaceHolder">
                <div class="email-content">
                    <div class="email-content-header pure-g">
                        <div class="pure-u-1-2">
                            <h1 class="email-content-title">Application for GSA</h1>
                            <p class="email-content-subtitle">
                                From <a>Aayushi Malviya</a> at <span>3:56pm, June 17, 2014</span>
                            </p>
                        </div>

                        <div class="email-content-controls pure-u-1-2">
                            <button class="secondary-button pure-button">Recommend</button>
                            <button class="secondary-button pure-button">Reject</button>
                            <button class="secondary-button pure-button">Forward</button>
                        </div>
                    </div>

                    <div class="email-content-body">
                        <p>
                            Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.
                        </p>
                        <p>
                            Duis aute irure dolor in reprehenderit in voluptate velit essecillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.
                        </p>
                        <p>
                            Aliquam ac feugiat dolor. Proin mattis massa sit amet enim iaculis tincidunt. Mauris tempor mi vitae sem aliquet pharetra. Fusce in dui purus, nec malesuada mauris. Curabitur ornare arcu quis mi blandit laoreet. Vivamus imperdiet fermentum mauris, ac posuere urna tempor at. Duis pellentesque justo ac sapien aliquet egestas. Morbi enim mi, porta eget ullamcorper at, pharetra id lorem.
                        </p>
                        <p>
                            Donec sagittis dolor ut quam pharetra pretium varius in nibh. Suspendisse potenti. Donec imperdiet, velit vel adipiscing bibendum, leo eros tristique augue, eu rutrum lacus sapien vel quam. Nam orci arcu, luctus quis vestibulum ut, ullamcorper ut enim. Morbi semper erat quis orci aliquet condimentum. Nam interdum mauris sed massa dignissim rhoncus.
                        </p>
                        <p>
                            Regards,<br/>
                            Tilo
                        </p>
                    </div>
                </div>
                </asp:PlaceHolder>
            </div>

            <br style="clear:left;"/>
        </div>

        <br style="clear:left;" />
    </div>
    </form>
</body>
</html>
