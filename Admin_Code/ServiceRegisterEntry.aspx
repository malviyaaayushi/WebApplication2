<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ServiceRegisterEntry.aspx.cs" Inherits="ServiceRegisterEntry" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
    <form id="form1" runat="server" class="pure-form pure-form-aligned" style="margin-left:10em;margin-top:2em;">
        <p class="warning" id="serviceRegisterEntryWarning" runat="server"></p>
        <div class="pure-control-group">
			<label for="name">Name of User: </label>
            <ajaxToolkit:ToolkitScriptManager ID="ScriptManager1" runat="server"/>
            <asp:TextBox ID="name" runat="server" Width="400px" OnTextChanged="name_TextChanged"></asp:TextBox>
            <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="name" OnClientItemSelected="ItemSelected"
                CompletionListCssClass="autocomplete_completionListElement" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" CompletionListItemCssClass="autocomplete_listItem"
            MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="1" CompletionInterval="1000" ServiceMethod="GetCompletionList" >
            </ajaxToolkit:AutoCompleteExtender>
		</div>
		<h3>SERVICE REGISTER ENTRY</h3>
		<div class="pure-control-group">
			<label for="description">Description of Post and Pay </label>
			<input runat="server" type="text" id="postAndPayDescription" value="" name="postAndPayDescription" width="200px"/>
		</div>			
		<div class="pure-control-group">
			<label for="permanentOrTemporary">Whether Post is permanent or temporary</label>
			<input runat="server" type="text" name="permanentOrTemporary" id="permanentOrTemporary" autocomplete="off"/>
		</div>
		<div class="pure-control-group">
			<label for="incumbent">Whether the incumbent posted permanently or to officiate</label>
			<input runat="server" type="text" id="incumbent" name="incumbent"/>
		</div>
		<div class="pure-control-group">
			<label for="postHeldPermanently">Post held permanently </label>
			<input runat="server" type="text" name="postHeldPermanently" id="postHeldPermanently" autocomplete="off"/>
		</div>

		<div class="pure-control-group">
			<label for="payInPermanentPost">Pay in the permanent post</label>
			<input runat="server" type="text" name="payInPermanentPost" id="payInPermanentPost" autocomplete="off"/>
		</div>
		<div class="pure-control-group">
			<label for="officiatingPay">Officiating Pay </label>
			<input runat="server" type="text" name="officiatingPay" id="officiatingPay" autocomplete="off"/>
		</div>
		<div class="pure-control-group">
			<label for="otherPay"> Other emoluments falling under 'pay' </label>
			<input runat="server" type="text" name="otherPay" id="otherPay" autocomplete="off"/>
		</div>
		<div class="pure-control-group">
			<label for="fromPeriod">From </label>
			<input runat="server" type="text" name="fromPeriod" id="fromPeriod" autocomplete="off"/><span> (YYYY-MM-DD)</span>
		</div>
		<div class="pure-control-group">
			<label for="toPeriod"> To </label>
			<input runat="server" type="text" name="toPeriod" id="toPeriod" autocomplete="off"/><span> (YYYY-MM-DD)</span>
		</div>
		<div class="pure-control-group">
			<label for="events1to8"> Events affecting Columns 1 to 8</label>
			<input runat="server" type="text" name="events1to8" id="events1to8" autocomplete="off"/>
		</div>
		<div class="pure-control-group">
			<label for="leaveDescription">Nature and duration of leave taken </label>
			<input runat="server" type="text" name="leaveDescription" id="leaveDescription" autocomplete="off"/>
		</div>
		<div class="pure-control-group">
			<label for="punishmentReference"> Reference to any recorded punishment, censure, award etc. </label>
			<input runat="server" type="text" name="punishmentReference" id="punishmentReference" autocomplete="off"/>
		</div>
		<div class="pure-control-group">
			<label for="remarks"> Remarks </label>
			<input runat="server" type="text" name="remarks" id="remarks" autocomplete="off"/>
		</div>
		<input runat="server" type="hidden" name="token" value="c71040ae2b20c652e93ae7f7004c79c2"/>			
		<div class="pure-control">
			<asp:Button runat="server" cssClass="pure-button pure-button-primary" value="Update" width="100px" ID="updateButton" OnClick="updateButton_Click"/>
		</div>
    </form>
</body>
</html>
