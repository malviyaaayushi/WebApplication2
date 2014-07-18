<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="Profile" %>

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
<head id="Head1" runat="server">
    <title>Profile &ndash;</title><link rel="stylesheet" href="Styles/pure-min.css"/>
        <link rel="stylesheet" href="Styles/email.css"/>
</head>
<body>
    <form id="form1" runat="server" class="pure-form">
    
    <div id="header_ribbon" class="peterriver">
        <div style="float:right;right:10px;">
            <div id="headerRibbonUsernameDiv" class="header_ribbon_element" runat="server">
                <a id="headerRibbonUsername" href="#" style="color:White;" runat="server" onserverclick="HeaderRibbonUsername_Click">
                        Username
                </a>
                <div id="headerRibbonUsernameBox" runat="server" style="display:none;">
                    <asp:Image id="headerRibbonUsernameBoxImage" runat="server" Width= "100px" Height= "100px" ImageUrl="images/sayalee.jpg"/>
                    <div style="float:right;display:inline-block;">
                        <ul style="margin-left:-20px;">
                            <li class="pure-menu-heading"><a href="#" style="color:rgb(64, 64, 64);" id="headerRibbonUsernameBoxName" runat="server">Aayushi Malviya</a></li>
                            <li style="padding-bottom:10px;"><a href="#" style="font-size:small;color:rgb(64, 64, 64);" id="headerRibbonUsernameBoxPost" runat="server">Sr. Software Developer</a></li>
                            <li><a class="button-secondary pure-button" href="Default.aspx"  target="_blank" style="background: #1b98f8;color:White;" id="headerRibbonUsernameBoxViewProfile" runat="server">View Inbox</a></li>
                        </ul>
                        <div style="float:right;"><a runat="server" id="logoutButton" class="secondary-button pure-button" href="#" onserverclick="LogoutButton_Click">Logout</a></div>
                    </div>
                </div>
                <%--<iframe id="headerRibbonUsernameBox" runat="server" src="HeaderRibbonUsernameBox.aspx" style="display:none;border-color:White;width:100%" />--%>
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

    <div id="layout" class="content pure-g" style="padding:0;">

        <div id="nav-profile" class="pure-u">

            <div class="nav-inner">
                    <asp:Image id="profileNavImage" runat="server" Width= "250px" Height= "250px" style="float:left;" ImageUrl="images/usericon.jpg"/>
                    
                    <asp:PlaceHolder runat="server" id="profile_nav_placeHolder" />

            </div>
        </div>

        <div id="navigation_result" class="navigation_result_wrapper" style="width:65%;margin-left:18.5%;margin-top:3em;">

            <div class="pure-menu pure-menu-open pure-menu-horizontal gmail-tabs" >
                <ul>
                    <li class="gmail-tabs-selected" id="profileTab" runat="server"><a href="#" runat="server" id="profile" onserverclick="Profile_Click">Profile</a></li>
                    <li id="leaveAccountTab" runat="server"><a href="#" runat="server" id="leaveAccount" onserverclick="LeaveAccount_Click">Leave Account</a></li>
                    <li id="ltcRecordTab" runat="server"><a href="#" runat="server" id="ltcRecord" onserverclick="LtcRecord_Click">LTC Record</a></li>
                    <li id="ltcDeclarationTab" runat="server"><a href="#" runat="server" id="ltcDeclaration" onserverclick="LtcDeclaration_Click">LTC Declaration</a></li>
                    <li id="serviceRegisterTab" runat="server"><a href="#" runat="server" id="serviceRegister" onserverclick="ServiceRegister_Click">Service Register</a></li>
                </ul>
            </div>
            <div class="main" style="margin-top:5px;width:95.5%;overflow:auto;margin-left: 20px;" id="service_book_container">
                <asp:PlaceHolder runat="server" id="service_book_container_placeholder" />  
            </div>

        </div>

        <div id="Div1" class="navigation_result_wrapper" style="width:15%;margin-top:3em;margin-left: 1em;">

            <div class="main" style="margin-top:5px;" id="Div2">
                <div class="l-box-lrg pure-u-md-2-5" style="float:right;position:fixed;margin-right:10px;width: 14%;padding: 10px;background: white;">
                    <div class="pure-form">
                                <asp:Label ID="LWarning" runat="server" ForeColor="Red"></asp:Label><br/>
                                <h5>Change your password</h5>
                                <asp:Label ID="UserNameLabel" style="float:left;width:30%;font-size:x-small;" runat="server">New Password:</asp:Label>
                                <br />
                                <asp:TextBox ID="UserName" style="float:right;width:70%;font-size:x-small;" runat="server" CssClass="textEntry"></asp:TextBox>
                                <br />
                                <asp:Label ID="PasswordLabel" style="float:left;width:30%;font-size:x-small;" runat="server">Confirm Password:</asp:Label>
                                <br />
                                <asp:TextBox ID="Password" style="float:right;width:70%;font-size:x-small;" runat="server" CssClass="passwordEntry" TextMode="Password"></asp:TextBox>
                                <br />
                    
                        <asp:Button ID="LoginButton" runat="server" CssClass="pure-button-primary" style="padding:5px;width:100%;" CommandName="Login" Text="Submit" ValidationGroup="LoginUserValidationGroup"/>
                
                    </div>
                </div>
                
            </div>

        </div>



        <br style="clear:left;" />
    </div>
    </form>
</body>
</html>

