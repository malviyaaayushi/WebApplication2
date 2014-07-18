using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.Security;

namespace WebApplication2
{
    public partial class Default : System.Web.UI.Page
    {
        public String PopupMsg;
        private UserFuncs userFuncs;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!AUserSession.Current.isUserLoggedIn()) {
                LogoutButton_Click(sender, e);
            }
            setValues();
            userFuncs = new UserFuncs();
            Load_Box_Content();
            if (Request.QueryString["appNo"] != null)
            {
                BoxItem_Click(int.Parse(Request.QueryString["appNo"]));
            }
            PopupMsg = AUserSession.Current.Warning;
        }

        protected void setValues() {
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

        protected void LeaveAccount_Click(object sender, EventArgs e)
        {
            Clear_Box_Structure();
        }

        protected void LtcRecord_Click(object sender, EventArgs e)
        {
            Clear_Box_Structure();
        }

        protected void LtcDeclaration_Click(object sender, EventArgs e)
        {
            Clear_Box_Structure();
        }

        protected void ServiceRegister_Click(object sender, EventArgs e)
        {
            Clear_Box_Structure();
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
            catch {
                HtmlGenericControl div = Generate_Empty_BoxList();
                boxListItem.Controls.Add(div);
            }
        }

        protected void Generate_Box_Structure() {
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

        protected void Clear_Box_Structure() {
            boxListItem.Controls.Clear();
            EmailContentPlaceHolder.Controls.Clear();
            navigationResultPlaceHolder.Controls.Clear();
        }

        protected HtmlGenericControl Generate_Empty_BoxList() {
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
            divSubject.InnerText = "Require leave for few days";// +app.ApplicationType.ToString() + " leave for few days";
            HtmlGenericControl divDesc = new HtmlGenericControl("h5");
            divDesc.Attributes.Add("class", "email-desc");
            divDesc.Attributes.Add("style", "font-weight:normal;");
            divDesc.InnerText = "Have to put some content here, dunno what tough, since it gives a nice look...";

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
            catch (Exception ex) {
                AUserSession.Current.Warning = ex.Message;
            }
        }

        protected HtmlGenericControl Get_Email_Body(Application boxItem)
        {
            HtmlGenericControl divBodyWrapper = new HtmlGenericControl("div");
            divBodyWrapper.Attributes.Add("class", "email-content-body");
            divBodyWrapper.InnerText = "Hahahaha. There's no text here.";
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
                HtmlGenericControl recommButton = new HtmlGenericControl("button");
                recommButton.Attributes.Add("class", "secondary-button pure-button");
                recommButton.InnerText = "Recommend";
                divInnerWrapper.Controls.Add(recommButton);
            }
            else if (((User)AUserSession.Current.ThisUser).UserId == app.ApprovAuthId)
            {
                HtmlGenericControl approvButton = new HtmlGenericControl("button");
                approvButton.Attributes.Add("class", "secondary-button pure-button");
                approvButton.InnerText = "Approve";
                divInnerWrapper.Controls.Add(approvButton);
            }
            else
            {
                HtmlGenericControl deleteButton = new HtmlGenericControl("button");
                deleteButton.Attributes.Add("class", "secondary-button pure-button");
                deleteButton.InnerText = "Cancel Leave";
                divInnerWrapper.Controls.Add(deleteButton);
                return divInnerWrapper;
            }

            HtmlGenericControl rejectButton = new HtmlGenericControl("button");
            rejectButton.Attributes.Add("class", "secondary-button pure-button");
            rejectButton.InnerText = "Reject";
            HtmlGenericControl forwardButton = new HtmlGenericControl("button");
            forwardButton.Attributes.Add("class", "secondary-button pure-button");
            forwardButton.InnerText = "Forward";
                        
            divInnerWrapper.Controls.Add(rejectButton);
            divInnerWrapper.Controls.Add(forwardButton);

            return divInnerWrapper;
        }
    }
}