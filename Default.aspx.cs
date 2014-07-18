using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.Security;
using System.Data;


public partial class Default : System.Web.UI.Page
{
    public string PopupMsg;
    private UserFuncs userFuncs;
    private TextBox commentbyrecommending;
    string comment;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!AUserSession.Current.isUserLoggedIn())
        {
            LogoutButton_Click(sender, e);
        }
        setValues();
        userFuncs = new UserFuncs();
        Load_Box_Content();
        if (Request.QueryString["appNo"] != null)
        {
            BoxItem_Click(int.Parse(Request.QueryString["appNo"]));
        }
        getNewsfeed();
        PopupMsg = AUserSession.Current.Warning;
    }

    private void getNewsfeed()
    {
        try
        {
            Database.connectToDatabse();
            string query = "SELECT * FROM AdminBroadcasts";
            Database.executeQuery(query);
            DataTable dtable = Database.getDataTable();
            int size = dtable.Rows.Count;
            for (int i = 0; i < 20 && i < size; i++)
            {
                DataRow row = dtable.Rows[i];
                HtmlGenericControl li = new HtmlGenericControl("li");
                li.Attributes["class"] = "newsfeedClassli";
                li.InnerText = row.Field<string>("message");
                newsfeed.Controls.Add(li);
            }
            Database.disconnectToDatabase();
        }
        catch (Exception ex)
        {
            AUserSession.Current.Warning = ex.Message;
        }
    }

    protected void ComposeButton_Click(object sender, EventArgs e)
    {
        if (newApplicationBox.Attributes["style"] == "display:none;")
        {
            newApplicationBox.Attributes["style"] = "display:block;";
        }
        else
        {
            newApplicationBox.Attributes["style"] = "display:none;";
        }
    }

    protected void setValues()
    {
        headerRibbonUsername.InnerText = AUserSession.Current.ThisUser.UserName;
        headerRibbonUsernameBoxName.InnerText = AUserSession.Current.ThisUser.Name;
        headerRibbonUsernameBoxPost.InnerText = AUserSession.Current.ThisUser.Designation;
        headerRibbonUsernameBoxImage.ImageUrl = "images/" + AUserSession.Current.ThisUser.UserName.ToLower() + ".jpg";
    }

    protected void LogoutButton_Click(object sender, EventArgs e)
    {
        FormsAuthentication.SignOut();
        AUserSession.Current.clearSession();
        string cookie_userName = "ffpdf.userId", cookie_pwd = "ffpdf.pwd";
        HttpCookie c1 = new HttpCookie(cookie_userName);
        HttpCookie c2 = new HttpCookie(cookie_pwd);

        c1.Expires = c2.Expires = DateTime.Now.AddDays(-1d);
        Response.Cookies.Add(c1);
        Response.Cookies.Add(c2);

        Response.Redirect("Login.aspx");
    }

    protected void HeaderRibbonUsername_Click(object sender, EventArgs e)
    {
        if (headerRibbonUsernameBox.Attributes["style"] == "display:none;")
        {
            headerRibbonUsernameBox.Attributes["style"] = "display:block;";
            headerRibbonUsername.Attributes["style"] = "color:rgb(64, 64, 64);text:bold;";
            headerRibbonUsernameDiv.Attributes["style"] = "background:white;color:white;";
        }
        else
        {
            headerRibbonUsernameBox.Attributes["style"] = "display:none;";
            headerRibbonUsername.Attributes["style"] = "color:white;text:bold;";
            headerRibbonUsernameDiv.Attributes["style"] = "background:#1b98f8;color:white;";
        }
    }

    protected void Inbox_Click(object sender, EventArgs e)
    {
        if (AUserSession.Current.CurrentBox != "inbox")
        {
            AUserSession.Current.CurrentBox = "inbox";
            Load_Box_Content();
        }
    }

    protected void Outbox_Click(object sender, EventArgs e)
    {
        if (AUserSession.Current.CurrentBox != "outbox")
        {
            AUserSession.Current.CurrentBox = "outbox";
            Load_Box_Content();
        }
    }

    protected void Important_Click(object sender, EventArgs e)
    {
        if (AUserSession.Current.CurrentBox != "important")
        {
            AUserSession.Current.CurrentBox = "important";
            Load_Box_Content();
        }
    }

    protected void Trash_Click(object sender, EventArgs e)
    {
        if (AUserSession.Current.CurrentBox != "trash")
        {
            AUserSession.Current.CurrentBox = "trash";
            Load_Box_Content();
        }
    }
    
    protected void Load_Box_Content()
    {
        try
        {
            if (!AUserSession.Current.CurrentLook.ToLower().Equals("box"))
            {
                AUserSession.Current.CurrentLook = "box";
                Generate_Box_Structure();
            }

            userFuncs.loadUserBox(AUserSession.Current.CurrentBox);

            boxListItem.Controls.Clear();
            List<Application> boxContent = userFuncs.boxContent;
            int i = 0;
            foreach (Application b in boxContent)
            {
                HtmlGenericControl div = Generate_Box_Item(b, i);
                boxListItem.Controls.Add(div);
                i++;
            }
        }
        catch
        {
            HtmlGenericControl div = Generate_Empty_BoxList();
            boxListItem.Controls.Add(div);
        }
    }

    protected void Generate_Box_Structure()
    {
        HtmlGenericControl divBoxList = new HtmlGenericControl("div");
        divBoxList.Attributes["ID"] = "boxList";
        divBoxList.Attributes["runat"] = "server";

        PlaceHolder boxListItemPlaceholder = new PlaceHolder();
        boxListItemPlaceholder.ID = "boxListItem";

        divBoxList.Controls.Clear();
        divBoxList.Controls.Add(boxListItemPlaceholder);

        HtmlGenericControl divMain = new HtmlGenericControl("div");
        divMain.Attributes["ID"] = "main";
        divMain.Attributes["runat"] = "server";

        PlaceHolder EmailContentPlaceHolder = new PlaceHolder();
        EmailContentPlaceHolder.ID = "EmailContentPlaceHolder";

        divMain.Controls.Clear();
        divMain.Controls.Add(EmailContentPlaceHolder);

        HtmlGenericControl lnBreak = new HtmlGenericControl("br");
        lnBreak.Attributes.Add("style", "clear:left;");

        navigationResultPlaceHolder.Controls.Clear();
        navigationResultPlaceHolder.Controls.Add(divBoxList);
        navigationResultPlaceHolder.Controls.Add(divMain);
        navigationResultPlaceHolder.Controls.Add(lnBreak);
    }

    protected void Clear_Box_Structure()
    {
        boxListItem.Controls.Clear();
        EmailContentPlaceHolder.Controls.Clear();
        navigationResultPlaceHolder.Controls.Clear();
    }

    protected HtmlGenericControl Generate_Empty_BoxList()
    {
        HtmlGenericControl divWrapper = new HtmlGenericControl("div");
        divWrapper.Attributes.Add("class", "email-item pure-g");
        HtmlGenericControl divInnerWrapper = new HtmlGenericControl("div");
        divInnerWrapper.Attributes.Add("class", "pure-u");

        HtmlGenericControl divSubject = new HtmlGenericControl("h5");
        divSubject.Attributes.Add("class", "email-subject");
        divSubject.Attributes.Add("style", "text-align:center;");
        divSubject.InnerText = "No Applications found";

        divInnerWrapper.Controls.Add(divSubject);
        divWrapper.Controls.Add(divInnerWrapper);

        return divWrapper;
    }

    protected HtmlGenericControl Generate_Box_Item(Application app, int index)
    {
        HtmlGenericControl divWrapper = new HtmlGenericControl("div");
        divWrapper.Attributes.Add("class", "email-item email-item-selected pure-g");

        HtmlGenericControl divInnerWrapper = new HtmlGenericControl("div");
        divInnerWrapper.Attributes.Add("class", "pure-u");
        divInnerWrapper.Attributes.Add("style", "width:100%;");

        HtmlAnchor senderName = new HtmlAnchor();
        senderName.InnerText = app.ApplicantName;
        String hrefArg = "?appNo=" + index.ToString();
        senderName.Attributes.Add("href", hrefArg);
        senderName.Attributes.Add("style", "color:black;");
        senderName.Attributes.Add("runat", "server");

        HtmlGenericControl divSenderName = new HtmlGenericControl("div");
        divSenderName.Attributes.Add("class", "email-name");
        divSenderName.Controls.Add(senderName);

        HtmlGenericControl divSendDate = new HtmlGenericControl("div");
        divSendDate.Attributes.Add("class", "mailDate");
        divSendDate.InnerText = app.Date.ToString("MMM dd") + " (" + app.Date.ToString("h:mm tt").ToLower() + ")";

        HtmlGenericControl divSubject = new HtmlGenericControl("h5");
        divSubject.Attributes.Add("class", "email-subject");
        divSubject.InnerText = "Require leave for " + app.LeaveDuration + " days";// +app.ApplicationType.ToString() + " leave for few days";
        HtmlGenericControl divDesc = new HtmlGenericControl("h5");
        divDesc.Attributes.Add("class", "email-desc");
        divDesc.Attributes.Add("style", "font-weight:normal;");
        if (app.Reason.Length > 80)
            divDesc.InnerText = app.Reason.Substring(0, 80) + "...";
        else
            divDesc.InnerText = app.Reason;

        divSenderName.Controls.Add(divSendDate);
        divInnerWrapper.Controls.Add(divSenderName);
        divInnerWrapper.Controls.Add(divSubject);
        divInnerWrapper.Controls.Add(divDesc);

        divWrapper.Controls.Add(divInnerWrapper);

        return divWrapper;
    }

    protected void BoxItem_Click(int index)
    {
        try
        {
            Application boxItem = (userFuncs.boxContent)[index];
            HtmlGenericControl email_header = Get_Email_Header(boxItem);
            HtmlGenericControl email_buttons = Get_Email_Buttons(boxItem);

            HtmlGenericControl divEmailContent = new HtmlGenericControl("div");
            divEmailContent.Attributes.Add("class", "email-content");

            HtmlGenericControl divHeaderWrapper = new HtmlGenericControl("div");
            divHeaderWrapper.Attributes.Add("class", "email-content-header pure-g");
            divHeaderWrapper.Controls.Add(email_header);
            divHeaderWrapper.Controls.Add(email_buttons);

            HtmlGenericControl emailBody = Get_Email_Body(boxItem);

            divEmailContent.Controls.Add(divHeaderWrapper);
            divEmailContent.Controls.Add(emailBody);

            EmailContentPlaceHolder.Controls.Clear();
            EmailContentPlaceHolder.Controls.Add(divEmailContent);
        }
        catch (Exception ex)
        {
            AUserSession.Current.Warning = ex.Message;
        }
    }

    protected void NewApplicationTypeList_Click(object sender, EventArgs e)
    {
        int val = newApplicationTypeList.SelectedIndex;
        switch (val)
        {
            case 0: Generate_Casual_Leave_Application(); break;
            case 1: Generate_Non_Casual_Leave_Application(); break;
        }
    }

    protected void Generate_Casual_Leave_Application()
    {
        HtmlGenericControl form = new HtmlGenericControl("div");
        form.Attributes["class"] = "pure-form pure-form-aligned";
        form.Attributes["style"] = "margin-left:10px";
        form.InnerHtml = "<div class=\"pure-control-group\"><label>Dates: </label><input id=\"dates\" runat=\"server\" type=\"text\" value=\"\"/></div>";
        form.InnerHtml += "<br/><div class=\"pure-control-group\"><label>Reasons for Casual Leave and grounds: </label><textarea runat=\"server\" id=\"rasonForLeave\" type=\"text\" value=\"\" maxlength=\"500\" rows=\"6\" cols=\"40\" placeholder=\"Do not use more than 100 words\"></textarea></div>";
        form.InnerHtml += "<br/><div class=\"pure-control-group\"><label>Recommending Authority: </label><input runat=\"server\" id=\"recommendingAuthority\" type=\"text\" value=\"\"/></div>";
        form.InnerHtml += "<br/><div class=\"pure-control-group\"><label>Approving Authority: </label><input runat=\"server\" id=\"approvingAuthority\" type=\"text\" value=\"\"/></div>";
        newApplicationFields.Controls.Add(form);
    }

    protected void Generate_Non_Casual_Leave_Application()
    {
        HtmlGenericControl form = new HtmlGenericControl("div");
        form.Attributes["class"] = "pure-form pure-form-aligned";
        form.Attributes["style"] = "margin-left:20px;";
        HtmlContainerControl d = new HtmlGenericControl("div");
        d.Attributes["class"] = "pure-control-group";
        Label l = new Label();
        l.Text = "Nature of Leave:";
        DropDownList leaveTypeList = new DropDownList();
        leaveTypeList.Attributes["id"] = "leaveTypeList";
        leaveTypeList.Attributes["runat"] = "server";
        leaveTypeList.AutoPostBack = true;
        ListItem l1 = new ListItem();
        l1.Text = "Earned Leave";
        l1.Value = "earnedLeaveBalance";
        leaveTypeList.Items.Add(l1);
        l1 = new ListItem();
        l1.Text = "Half Pay Leave";
        l1.Value = "halfPayLeaveBalance";
        leaveTypeList.Items.Add(l1);
        d.Controls.Add(l);
        d.Controls.Add(leaveTypeList);
        form.Controls.Add(d);
        d = new HtmlGenericControl("div");
        d.Attributes["class"] = "pure-control-group";
        d.InnerHtml = "<br/><label>Dates: </label><input id=\"dates\" style=\"margin-left:100px;\" runat=\"server\" type=\"text\" value=\"\"/>";
        form.Controls.Add(d);
        d = new HtmlGenericControl("div");
        d.Attributes["class"] = "pure-control-group";
        d.InnerHtml = "<br/><label style=\" width: 20%;\">Reasons for Leave: </label><textarea id=\"reasonForLeave\" style=\"margin-left:100px;\" runat=\"server\" type=\"text\" value=\"\" maxlength=\"500\" rows=\"6\" cols=\"30\" placeholder=\"Do not use more than 100 words\"></textarea>";
        form.Controls.Add(d);
        d = new HtmlGenericControl("div");
        d.Attributes["class"] = "pure-control-group";
        d.InnerHtml = "<br/><label style=\" width: 20%;\">Address while on leave: </label><textarea runat=\"server\" type=\"text\" style=\"margin-left:100px\" id=\"Address\" value=\"\" maxlength=\"500\" rows=\"3\" cols=\"30\" placeholder=\"Do not use more than 100 words\"></textarea>";
        form.Controls.Add(d);
        d = new HtmlGenericControl("div");
        d.Attributes["class"] = "pure-control-group";
        l = new Label();
        l.Text = "Do you want travel leave concession during the ensuring leave:";
        RadioButtonList radioButtonList = new RadioButtonList();
        RadioButton r1 = new RadioButton();
        r1.Text = "Yes";
        r1.Checked = true;
        r1.Attributes["runat"] = "server";
        RadioButton r2 = new RadioButton();
        r2.Text = "No";
        r2.Attributes["runat"] = "server";
        radioButtonList.Controls.Add(r1);
        radioButtonList.Controls.Add(r2);
        d.Controls.Add(l);
        d.Controls.Add(radioButtonList);
        form.Controls.Add(d);
        d = new HtmlGenericControl("div");
        d.Attributes["class"] = "pure-control-group";
        d.InnerHtml = "<br/><label>Recommending Authority: </label><input runat=\"server\" id=\"recommendingAuthority\" type=\"text\" value=\"\"/>";
        form.Controls.Add(d);
        d = new HtmlGenericControl("div");
        d.Attributes["class"] = "pure-control-group";
        d.InnerHtml = "<label>Approving Authority: </label><input runat=\"server\" id=\"approvingAuthority\" type=\"text\" value=\"\"/>";
        form.Controls.Add(d);
        d = new HtmlGenericControl("div");
        d.Attributes["class"] = "pure-control-group";
        d.InnerHtml = "<p> A. In the event of my resignation or voluntary retirement from the service. I undertake to refund:<br>" +
            "&nbsp;&nbsp;&nbsp;&nbsp;   1. The difference between the leave salary drawn during commuted leave and that admissible during half pay leave.<br>" +
            "<br>B.  Undertake to refund the leave salary drawn for the period of earned leave which would not have been admissible, had leave not been credited in advance in the event of my resignation, Voluntary retirement, dismissal or removal from service or removal from service or in the event of termination of my services.";
        form.Controls.Add(d);

        newApplicationFields.Controls.Add(form);
    }

    protected void SendApplicationButton_Click(object sender, EventArgs e)
    {

    }

    protected void DeleteDraftButton_Click(object sender, EventArgs e)
    {

    }

    protected void RecommendButton_Click(object sender, EventArgs e)
    {
        Database.connectToDatabse();
        string query = "UPDATE LeaveDetails SET leaveStatus='2', commentByRecommending='" + comment + "' WHERE applicationId='" + Request.QueryString["appNo"] + "'";
        Database.executeQuery(query);
        Database.disconnectToDatabase();
    }

    protected void ApproveButton_Click(object sender, EventArgs e)
    {
        Database.connectToDatabse();
        string query = "UPDATE LeaveDetails SET leaveStatus='3', commentByApproving='" + comment + "' WHERE applicationId='" + userFuncs.boxContent[int.Parse(Request.QueryString["appNo"])].ApplicationId + "'";
        Database.executeQuery(query);
        Database.disconnectToDatabase();
    }

    protected void CancelLeaveButton_Click(object sender, EventArgs e)
    {
        Database.connectToDatabse();
        string query = "UPDATE LeaveDetails SET leaveStatus='5' WHERE applicationId='" + Request.QueryString["appNo"] + "'";
        Database.executeQuery(query);
        Database.disconnectToDatabase();
    }

    protected void RejectButton_Click(object sender, EventArgs e)
    {
        Database.connectToDatabse();
        string query = "UPDATE LeaveDetails SET leaveStatus='4' WHERE applicationId='" + Request.QueryString["appNo"] + "'";
        Database.executeQuery(query);
        Database.disconnectToDatabase();
    }

    protected void ForwardButton_Click(object sender, EventArgs e)
    {

    }

    protected void setComment(object sender, EventArgs e)
    {
        if (IsPostBack)
        {
            comment = commentbyrecommending.Text;
        }
    }

    protected HtmlGenericControl Get_Email_Body(Application boxItem)
    {
        HtmlGenericControl divBodyWrapper = new HtmlGenericControl("div");
        divBodyWrapper.Attributes.Add("class", "email-content-body");

        HtmlGenericControl table = new HtmlGenericControl("table");
        table.Attributes["class"] = "pure-table pure-table-horizontal";
        HtmlGenericControl tbody = new HtmlGenericControl("tbody");

        HtmlGenericControl tr;
        HtmlGenericControl td1;
        HtmlGenericControl td2;

        tr = new HtmlGenericControl("tr");
        tr.Attributes["class"] = "pure-table-odd";
        td1 = new HtmlGenericControl("td");
        td2 = new HtmlGenericControl("td");
        td1.InnerText = "From";
        td2.InnerText = boxItem.FromDate.ToString();
        tr.Controls.Add(td1);
        tr.Controls.Add(td2);
        tbody.Controls.Add(tr);

        tr = new HtmlGenericControl("tr");
        tr.Attributes["class"] = "pure-table-even";
        td1 = new HtmlGenericControl("td");
        td2 = new HtmlGenericControl("td");
        td1.InnerText = "To";
        td2.InnerText = "find";
        tr.Controls.Add(td1);
        tr.Controls.Add(td2);
        tbody.Controls.Add(tr);

        tr = new HtmlGenericControl("tr");
        tr.Attributes["class"] = "pure-table-odd";
        td1 = new HtmlGenericControl("td");
        td2 = new HtmlGenericControl("td");
        td1.InnerText = "Reason";
        td2.InnerText = boxItem.Reason;
        tr.Controls.Add(td1);
        tr.Controls.Add(td2);
        tbody.Controls.Add(tr);

        tr = new HtmlGenericControl("tr");
        tr.Attributes["class"] = "pure-table-even";
        td1 = new HtmlGenericControl("td");
        td2 = new HtmlGenericControl("td");
        td1.InnerText = "Recommending Authority";
        td2.InnerText = UserFuncs.FindUserNameFromUserId(boxItem.RecommAuthId);
        tr.Controls.Add(td1);
        tr.Controls.Add(td2);
        tbody.Controls.Add(tr);

        tr = new HtmlGenericControl("tr");
        tr.Attributes["class"] = "pure-table-odd";
        td1 = new HtmlGenericControl("td");
        td2 = new HtmlGenericControl("td");
        td1.InnerText = "Approving Authority";
        td2.InnerText = UserFuncs.FindUserNameFromUserId(boxItem.ApprovAuthId);
        tr.Controls.Add(td1);
        tr.Controls.Add(td2);
        tbody.Controls.Add(tr);

        if (boxItem.ApplicationType != 0)
        {
            tr = new HtmlGenericControl("tr");
            tr.Attributes["class"] = "pure-table-even";
            td1 = new HtmlGenericControl("td");
            td2 = new HtmlGenericControl("td");
            td1.InnerText = "Concession Taken During Travel";
            if (boxItem.AvailConcession == 0)
                td2.InnerText = "No";
            else
                td2.InnerText = "Yes";
            tr.Controls.Add(td1);
            tr.Controls.Add(td2);
            tbody.Controls.Add(tr);
        }
        if (boxItem.ApplicationType != 0)
        {
            tr = new HtmlGenericControl("tr");
            tr.Attributes["class"] = "pure-table-odd";
            td1 = new HtmlGenericControl("td");
            td2 = new HtmlGenericControl("td");
            td1.InnerText = "Address During Leave";
            td2.InnerText = boxItem.LeaveAddress;
            tr.Controls.Add(td1);
            tr.Controls.Add(td2);
            tbody.Controls.Add(tr);
        }

        if ((boxItem.RecommAuthId == AUserSession.Current.ThisUser.UserId && boxItem.CommentByRecommending == "") || (boxItem.ApprovAuthId == AUserSession.Current.ThisUser.UserId && boxItem.CommentByApproving == ""))
        {
            tr = new HtmlGenericControl("tr");
            tr.Attributes["class"] = "pure-table-even";
            td1 = new HtmlGenericControl("td");
            td2 = new HtmlGenericControl("td");
            td1.InnerText = "Add comment";
            commentbyrecommending = new TextBox();
            commentbyrecommending.Attributes["runat"] = "server";
            commentbyrecommending.TextChanged += new EventHandler(setComment);
            commentbyrecommending.Rows = 3;
            commentbyrecommending.Columns = 40;
            commentbyrecommending.Attributes["placeholder"] = "Write a comment";
            td2.Controls.Add(commentbyrecommending);
            tr.Controls.Add(td1);
            tr.Controls.Add(td2);
            tbody.Controls.Add(tr);

        }

        if (boxItem.CommentByRecommending != "")
        {

            tr = new HtmlGenericControl("tr");
            tr.Attributes["class"] = "pure-table-odd";
            td1 = new HtmlGenericControl("td");
            td2 = new HtmlGenericControl("td");
            td1.InnerText = "Comment by Recommending Authority";
            td2.InnerText = boxItem.CommentByRecommending;
            tr.Controls.Add(td1);
            tr.Controls.Add(td2);
            tbody.Controls.Add(tr);
        }

        if (boxItem.CommentByApproving != "")
        {
            tr = new HtmlGenericControl("tr");
            tr.Attributes["class"] = "pure-table-even";
            td1 = new HtmlGenericControl("td");
            td2 = new HtmlGenericControl("td");
            td1.InnerText = "Comment by Approving Authority";
            td2.InnerText = boxItem.CommentByApproving;
            tr.Controls.Add(td1);
            tr.Controls.Add(td2);
            tbody.Controls.Add(tr);
        }

        if (boxItem.ApplicationType != 0)
        {
            tr = new HtmlGenericControl("tr");
            tr.Attributes["class"] = "pure-table-odd";
            td1 = new HtmlGenericControl("td");
            td2 = new HtmlGenericControl("td");
            td1.InnerText = "Medical Cerificate submitted";
            if (boxItem.MedicalCertificate == 0)
                td2.InnerText = "NO";
            else
                td2.InnerText = "Yes";
            tr.Controls.Add(td1);
            tr.Controls.Add(td2);
            tbody.Controls.Add(tr);
        }
        table.Controls.Add(tbody);
        divBodyWrapper.Controls.Add(table);
        return divBodyWrapper;

    }

    protected HtmlGenericControl Get_Email_Header(Application app)
    {
        HtmlGenericControl divInnerWrapper = new HtmlGenericControl("div");
        divInnerWrapper.Attributes.Add("class", "pure-u-1-2");

        HtmlGenericControl divSubject = new HtmlGenericControl("h1");
        divSubject.Attributes.Add("class", "email-content-title");
        divSubject.InnerText = "Require " + app.ApplicationType.ToString() + " leave";

        HtmlGenericControl divEmailContentSubtitle = new HtmlGenericControl("p");
        divEmailContentSubtitle.Attributes.Add("class", "email-content-subtitle");
        divEmailContentSubtitle.InnerHtml = "From <a>" + app.ApplicantName + "</a> at ";

        HtmlGenericControl divSendDate = new HtmlGenericControl("span");
        divSendDate.InnerText = app.Date.ToString("h:mm tt").ToLower() + ", " + app.Date.ToString("MMM dd") + "," + app.Date.ToString("yyyy");

        divInnerWrapper.Controls.Add(divSubject);
        divEmailContentSubtitle.Controls.Add(divSendDate);
        divInnerWrapper.Controls.Add(divEmailContentSubtitle);

        return divInnerWrapper;
    }

    protected HtmlGenericControl Get_Email_Buttons(Application app)
    {
        HtmlGenericControl divInnerWrapper = new HtmlGenericControl("div");
        divInnerWrapper.Attributes.Add("class", "email-content-controls pure-u-1-2");

        if (((User)AUserSession.Current.ThisUser).UserId == app.RecommAuthId)
        {
            Button recommButton = new Button();
            recommButton.CssClass = "secondary-button pure-button";
            recommButton.Text = "Recommend";
            recommButton.Click += new EventHandler(RecommendButton_Click);
            recommButton.Attributes["runat"] = "server";
            divInnerWrapper.Controls.Add(recommButton);
        }
        else if (((User)AUserSession.Current.ThisUser).UserId == app.ApprovAuthId)
        {
            Button approvButton = new Button();
            approvButton.CssClass = "secondary-button pure-button";
            approvButton.Text = "Approve";
            approvButton.Click += new EventHandler(ApproveButton_Click);
            approvButton.Attributes["runat"] = "server";
            divInnerWrapper.Controls.Add(approvButton);
        }
        else
        {
            Button deleteButton = new Button();
            deleteButton.CssClass = "secondary-button pure-button";
            deleteButton.Text = "Delete";
            deleteButton.Click += new EventHandler(CancelLeaveButton_Click);
            deleteButton.Attributes["runat"] = "server";
            divInnerWrapper.Controls.Add(deleteButton);
            return divInnerWrapper;
        }
        Button rejectButton = new Button();
        rejectButton.CssClass = "secondary-button pure-button";
        rejectButton.Text = "Reject";
        rejectButton.Click += new EventHandler(RejectButton_Click);
        rejectButton.Attributes["runat"] = "server";
        Button forwardButton = new Button();
        forwardButton.CssClass = "secondary-button pure-button";
        forwardButton.Text = "Forward";
        forwardButton.Click += new EventHandler(ForwardButton_Click);
        forwardButton.Attributes["runat"] = "server";

        divInnerWrapper.Controls.Add(rejectButton);
        divInnerWrapper.Controls.Add(forwardButton);

        return divInnerWrapper;
    }

    protected void newApplicationBoxHeaderButtonMinimize_Click(object sender, ImageClickEventArgs e)
    {
        newApplicationBox.Attributes["style"] = "display:none;";
    }

    protected void newApplicationBoxHeaderButtonExpand_Click(object sender, ImageClickEventArgs e)
    {
        newApplicationBox.Attributes["style"] = "display:block;";
    }

    protected void newApplicationBoxHeaderButtonClose_Click(object sender, ImageClickEventArgs e)
    {
        newApplicationBox.Attributes["style"] = "display:none;";
        //newApplicationBoxHeaderButtonExpand.Attributes["style"] = "display:none;";
    }

    protected void SearchImage_Click(object sender, ImageClickEventArgs e)
    {
        searchFunc();
    }

    protected void search_textbox_TextChanged(object sender, EventArgs e)
    {
        searchFunc();
    }

    private void searchFunc()
    {
        try
        {
            if (!AUserSession.Current.CurrentLook.ToLower().Equals("box"))
            {
                AUserSession.Current.CurrentLook = "box";
                Generate_Box_Structure();
            }

            userFuncs.loadSearchSpecificUserBox(AUserSession.Current.CurrentBox, search_textbox.Text);

            boxListItem.Controls.Clear();
            List<Application> boxContent = userFuncs.boxContent;
            int i = 0;
            foreach (Application b in boxContent)
            {
                HtmlGenericControl div = Generate_Box_Item(b, i);
                boxListItem.Controls.Add(div);
                i++;
            }
        }
        catch
        {
            HtmlGenericControl div = Generate_Empty_BoxList();
            boxListItem.Controls.Add(div);
        }
    }

}
