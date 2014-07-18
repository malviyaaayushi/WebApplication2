<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Default" %>
        
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
    <title>Administration Portal | IIT Indore </title><link rel="stylesheet" href="Styles/pure-min.css"/>
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
                        <%
                            //if (AUserSession.Current.ThisUser.Type == 1) { 
                        %>
                                <div style="float:left;"><a runat="server" id="adminButton" class="secondary-button pure-button" href="Admin.aspx"  target="_blank">Admin</a></div>
                        <%
                            //}
                        %>
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
                <asp:TextBox runat="server" ID="search_textbox" ToolTip="Search" OnTextChanged="search_textbox_TextChanged" style="height: 30px;width: 92%;float: left;clear: both;display: inline-block;margin: 0;" />
                <asp:ImageButton id="SearchImage" runat="server" OnClick="SearchImage_Click"
                   AlternateText="Search"
                   Width= "30px"
                   Height= "30px"
                   ImageUrl="images/search_icon.png"/>
        </div>
    </div>

    <div id="layout" class="content pure-g">

        <div id="nav" class="pure-u">

            <div class="nav-inner">
                    <asp:Button id="composeButton" cssclass="primary-button pure-button" Text="Compose" runat="server" OnClick="ComposeButton_Click"/>
                
                    <div class="pure-menu pure-menu-open">
                        <ul>
                            <li><a id="inbox" href="#" runat="server" onserverclick="Inbox_Click">Inbox</a></li>
                            <li><a id="important" href="#" runat="server" onserverclick="Important_Click">Important</a></li>
                            <li><a id="outbox" href="#" runat="server" onserverclick="Outbox_Click">Sent</a></li>
                            <li><a id="trash" href="#" runat="server" onserverclick="Trash_Click">Trash</a></li>
                        </ul>
                    </div>
                    <div class="pure-menu pure-menu-open">
                        <ul runat="server" id="newsfeed" class="newsfeedClass">
                            <li class="newsfeedClassli"></li>
                            <li class="newsfeedClassli">NEWSFEED</li>
                        </ul>
                    </div>
            </div>
        </div>

        <div id="navigation_result" class="navigation_result_wrapper" runat="server">
            <asp:PlaceHolder runat="server" id="navigationResultPlaceHolder" />
            <div ID="boxList" runat="server">
                <asp:PlaceHolder runat="server" id="boxListItem" />
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
                    <p style="width:700px;text-align:center;margin-top:50px;">Click to view application</p>
                </div>

                
                </asp:PlaceHolder>
            </div>
            <div runat="server" id="newApplicationBox" class="newApplicationBoxClass cloud" style="display:none;">
                <div runat="server" id="newApplicationBoxHeader" class="newApplicationBoxHeaderClass">
                    <div id="newApplicationBoxHeaderApplicationName" class="newApplicationBoxHeaderApplicationNameClass">
                        New Application
                    </div>
                    <div id="newApplicationBoxHeaderButtons" class="newApplicationBoxHeaderButtonsClass">
                        <asp:ImageButton runat="server" ID="newApplicationBoxHeaderButtonMinimize" OnClick="newApplicationBoxHeaderButtonMinimize_Click" ImageUrl="images/minus.png" Width="27px" Height="27px" />
                        <%-- <asp:ImageButton runat="server" ID="newApplicationBoxHeaderButtonExpand" OnClick="newApplicationBoxHeaderButtonExpand_Click"  /> --%>
                        <asp:ImageButton runat="server" ID="newApplicationBoxHeaderButtonClose" OnClick="newApplicationBoxHeaderButtonClose_Click" ImageUrl="images/close.png" Width="27px" Height="27px"/>
                    </div>
                </div>
                <div runat="server" id="newApplicationBoxBody" class="newApplicationBoxBodyClass border">
                    <div style="padding:5px;border-bottom: 1px solid #cfcfcf;">
                        <asp:Label runat="server" id="newApplicationTypeLabel" style="margin-left:10px;">Application</asp:Label>
                        <asp:DropDownList runat="server" id="newApplicationTypeList" type="text" AutoPostBack="true" OnSelectedIndexChanged="NewApplicationTypeList_Click">
                            <asp:ListItem Text="Casual Leave Application" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Non-Casual Leave Application" Value="2"></asp:ListItem>
                        </asp:DropDownList>
                        
                    </div>
                    <asp:PlaceHolder runat="server" ID="newApplicationFields"/>
                </div>
                <div runat="server" id="newApplicationBoxFooterButtons" class="newApplicationBoxFooterButtonsClass silverbk border">
                    <asp:Button runat="server" ID="sendApplicationButton" style="margin:0 0 5px 0;" cssClass="primary-button pure-button"  Text="Send" OnClick="SendApplicationButton_Click"/>
                    <%--<asp:ImageButton runat="server" ID="deleteDraftButton"  OnClick="DeleteDraftButton_Click"/>--%>
                </div>
            </div>

            <br style="clear:left;"/>
        </div>

        <br style="clear:left;" />
    </div>
    </form>
</body>
</html>
