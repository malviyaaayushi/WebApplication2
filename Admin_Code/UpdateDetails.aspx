<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UpdateDetails.aspx.cs" Inherits="UpdateDetails" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link rel="stylesheet" href="../Styles/pure-min.css"/>
    <link rel="stylesheet" href="../Styles/AutoSuggest.css"/>
    <script type="text/javascript">
        function ItemSelected(sender, args) {
            __doPostBack(sender.get_element().name, "");
        }
    </script>
</head>
<body>
    <form id="form1" runat="server" class="pure-form pure-form-aligned" style="margin-left:10px;margin-top:2em;">
         <p runat="server" id="updateDetailsWarning" class="warning" />
        <div class="pure-control-group">
		    <label for="username">Username: </label>
            <ajaxToolkit:ToolkitScriptManager ID="ScriptManager1" runat="server"/>
            <asp:TextBox ID="name" runat="server" Width="400px" OnTextChanged="name_TextChanged"></asp:TextBox>
            <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="name" OnClientItemSelected="ItemSelected"
                CompletionListCssClass="autocomplete_completionListElement" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" CompletionListItemCssClass="autocomplete_listItem"
            MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="1" CompletionInterval="1000" ServiceMethod="GetCompletionList" >
            </ajaxToolkit:AutoCompleteExtender>
            <asp:Button runat="server" ID="clearDetailsButton" OnClick="clearDetailsButton_Click" Text="Clear Fields" style="float:margin-right:20px;" CssClass="pure-button pure-button-primary"/>
	    </div>

	    <div class="pure-control-group">
		    <label for="formName">Form to be changed: </label>
            <asp:DropDownList runat="server" type="text" id="formName" OnSelectedIndexChanged="formName_SelectedIndexChanged" AutoPostBack="true">
                <asp:ListItem value="Biodata">Biodata</asp:ListItem>
                <asp:ListItem value="CertificationAttestation">Certification and Attestation</asp:ListItem>
                <asp:ListItem value="ForeignService">Foreign Service</asp:ListItem>
                <asp:ListItem value="LeaveBalance">Leave Balance</asp:ListItem>
                <asp:ListItem value="LeaveDetails">Leave Details</asp:ListItem>
                <asp:ListItem value="LeaveTravelConcession">Leave Travel Concession</asp:ListItem>
                <asp:ListItem value="LoginCredentials">Login Credentials</asp:ListItem>
                <asp:ListItem value="LtcDeclaration">LTC Declaration</asp:ListItem>
                <asp:ListItem value="LtcDependents">LTC Dependents</asp:ListItem>
                <asp:ListItem value="PreviousQualifyingService">Previous Qualifying Service</asp:ListItem>
                <asp:ListItem value="ProfileInformation">Profile Information</asp:ListItem>
                <asp:ListItem value="ServiceRegister">Service Register</asp:ListItem>
            </asp:DropDownList>
	    </div>

	    <div class="pure-control-group">
		    <label for="fieldName">Field to be changed: </label>
            <asp:DropDownList runat="server" ID="fieldName" AutoPostBack="true" OnSelectedIndexChanged="fieldName_SelectedIndexChanged">
            </asp:DropDownList>
	    </div>

        <asp:PlaceHolder runat="server" ID="updateDetailsTablePlaceholder" />
        <br/>

        <div id="valuesBox" runat="server" style="display:none;">
	        <div class="pure-control-group">
		        <label for="oldValue">Existing value: </label>
                <asp:TextBox ID="oldValue" runat="server" readonly="true"></asp:TextBox>
	        </div>

	        <div class="pure-control-group">
		        <label for="newValue">New value: </label>
                <asp:TextBox ID="newValue" runat="server"></asp:TextBox>
	        </div>

	        <div class="pure-control">
                <asp:Button runat="server" ID="updateDetailsButton"  OnClick="UpdateDetailsButton_Click" Text="Update" CssClass="pure-button pure-button-primary"/>
                <p runat="server" id="updateDetailsWarningDown" style="color:green" />
            </div>
        </div>	
    </form>
</body>
</html>
