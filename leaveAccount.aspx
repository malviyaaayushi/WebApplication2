<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="leaveAccount.aspx.cs" Inherits="leaveAccount" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="Styles/pure-min.css"/>
    <link rel="stylesheet" href="Styles/tabs"/>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div id="Dispalyalways">
			<div id="demo-horizontal-menu" class="pure-menu pure-menu-open pure-menu-horizontal gmail-tabs">
			    <ul id="std-menu-items">
			        <li style="width:33%;" id="leaveBalanceTab"  runat="server" ><a href="#" id="leaveBalance" runat="server" onserverclick="LeaveBalance_Click">Leave Balance</a></li>
			        <li style="width:33%;" id="leaveHistoryTab"  runat="server"><a href="#" id="leaveHistory" runat="server" onserverclick="LeaveHistory_Click">Earned Leave History</a></li>
			        <li style="width:33.5%;" id="halfPayHistoryTab" runat="server"><a href="#" id="halfPayHistory" runat="server" onserverclick="HalfPayHistory_Click">Half Play Leave History</a></li>
			     </ul>
		 	</div>
        </div>
        <asp:PlaceHolder ID="leave_inner_placeholder" runat="server"></asp:PlaceHolder>

    </div>
    </form>
</body>
</html>
