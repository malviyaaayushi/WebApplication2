<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AllUsers.aspx.cs" Inherits="AllUsers" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="../Styles/pure-min.css"/>
</head>
<body>
    <form id="form1" class="pure-form" runat="server">
    <div id="AllUser" runat="server" style="width:100%;padding-left:30px;padding-top:30px">
        <div id="searchBox">
        <asp:TextBox ID="SearchBar" runat="server" ToolTip="Search" placeholder="Search with name here.."  onTextChanged="searchUser" style="width:40%;height:30px;float:left;" />                
        </div>
        <div id="uTable" runat="server"></div>
    </div>
    </form>
</body>
</html>
