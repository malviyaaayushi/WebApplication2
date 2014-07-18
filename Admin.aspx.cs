using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Web.Security;

public partial class Admin : System.Web.UI.Page
{
    public String PopupMsg;
    public ServiceBookFuncs serviceBookFuncs;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!AUserSession.Current.isUserLoggedIn())
        {
            LogoutButton_Click(sender, e);
        }

        setValues();

        OpenTab(registerTab);
        AdminIframe.Attributes["src"] = "Admin_Code/Register.aspx";

        AUserSession.Current.removeAllWarnings();

        PopupMsg = AUserSession.Current.Warning;
    }

    protected void Register_Click(object sender, EventArgs e)
    {
        OpenTab(registerTab);
        AdminIframe.Attributes["src"] = "Admin_Code/Register.aspx";
    }

    protected void AllUsers_Click(object sender, EventArgs e)
    {
        OpenTab(allUsersTab);
        AdminIframe.Attributes["src"] = "Admin_Code/AllUsers.aspx";
    }

    protected void UpdateDetails_Click(object sender, EventArgs e)
    {
        OpenTab(updateDetailsTab);
        AdminIframe.Attributes["src"] = "Admin_Code/UpdateDetails.aspx";
    }

    protected void ServiceRegisterEntry_Click(object sender, EventArgs e)
    {
        OpenTab(serviceRegisterEntryTab);
        AdminIframe.Attributes["src"] = "Admin_Code/ServiceRegisterEntry.aspx";
    }

    protected void OpenTab(HtmlGenericControl id)
    {
        // open this tab and close all others
        HtmlGenericControl[] tabs = { registerTab, allUsersTab, updateDetailsTab, serviceRegisterEntryTab };
        int size = tabs.Count();
        for (int i = 0; i < size; i++)
        {
            tabs[i].Attributes["class"] = "gmail-tabs-unselected";
        }
        id.Attributes["class"] = "gmail-tabs-selected";
    }

    protected void setValues()
    {
        headerRibbonUsername.InnerText = AUserSession.Current.ThisUser.UserName;
        headerRibbonUsernameBoxName.InnerText = AUserSession.Current.ThisUser.Name;
        headerRibbonUsernameBoxPost.InnerText = AUserSession.Current.ThisUser.Designation;
        headerRibbonUsernameBoxImage.ImageUrl = "images/" + AUserSession.Current.ThisUser.UserName.ToLower() + ".jpg";
        profileNavImage.ImageUrl = "images/" + AUserSession.Current.ThisUser.UserName.ToLower() + ".jpg";

        admin_nav_placeHolder.Controls.Clear();
        try
        {
            ServiceBookFuncs serviceBookFuncs = new ServiceBookFuncs();
            Database.connectToDatabse();
            serviceBookFuncs.fetchBiodata();
            Database.disconnectToDatabase();
            HtmlGenericControl table = GenerateBiodata(serviceBookFuncs.biodata);
            admin_nav_placeHolder.Controls.Add(table);
        }
        catch (Exception ex)
        {
            HtmlGenericControl p = new HtmlGenericControl("p");
            p.InnerText = ex.Message;
            admin_nav_placeHolder.Controls.Add(p);
        }
    }

    protected HtmlGenericControl GenerateBiodata(Biodata biodata)
    {
        HtmlGenericControl table = new HtmlGenericControl("table");
        try
        {
            table.Attributes["id"] = "biodataTable";
            table.Attributes["class"] = "pure-table pure-table-horizontal";

            HtmlGenericControl tbody = new HtmlGenericControl("tbody");

            List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
            list.Add(new KeyValuePair<string, string>("Name", biodata.Name));
            list.Add(new KeyValuePair<string, string>("Father Name", biodata.FatherName));
            list.Add(new KeyValuePair<string, string>("Spouse Name", biodata.SpouseName));
            list.Add(new KeyValuePair<string, string>("Nationality", biodata.Nationality));
            list.Add(new KeyValuePair<string, string>("Religion", biodata.Religion));
            list.Add(new KeyValuePair<string, string>("Scheduled Caste", biodata.ScheduledCaste));
            list.Add(new KeyValuePair<string, string>("Caste Name", biodata.CasteName));
            list.Add(new KeyValuePair<string, string>("Date of Birth", biodata.Dob.ToString("dd MMMM yyyy")));
            list.Add(new KeyValuePair<string, string>("Qualification Afterwards", biodata.QualificationAfterwards));
            list.Add(new KeyValuePair<string, string>("Qualification When Appointed", biodata.QualificationWhenAppointed));
            list.Add(new KeyValuePair<string, string>("Height(cm)", biodata.HeightCm));
            list.Add(new KeyValuePair<string, string>("Identification Marks", biodata.IdentificationMarks));
            list.Add(new KeyValuePair<string, string>("Permanent Home Address", biodata.PermanentHomeAddress));


            for (int i = 0; i < list.Count; i++)
            {
                string key = list[i].Key;
                string value = list[i].Value;

                HtmlGenericControl tr = new HtmlGenericControl("tr");
                tr.Attributes["style"] = "font-size:small;";
                HtmlGenericControl td1 = new HtmlGenericControl("td");
                HtmlGenericControl td2 = new HtmlGenericControl("td");
                td1.InnerText = key;
                td2.InnerText = value;
                tr.Controls.Add(td1);
                tr.Controls.Add(td2);
                tbody.Controls.Add(tr);
            }
            table.Controls.Add(tbody);
        }
        catch (Exception ex)
        {
            AUserSession.Current.Warning = ex.Message;
        }
        return table;
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

    protected void broadCastButton_Click(object sender, EventArgs e)
    {
        try
        {
            DataRow drow = Database.getRowFromTable("AdminBroadcasts");
            drow["adminId"] = AUserSession.Current.ThisUser.UserId;
            drow["message"] = broadcastTextBox.Text;
            Database.updateTable(drow);
            broadCastWarning.InnerText = "";
        }
        catch (Exception ex)
        {
            broadCastWarning.InnerText = ex.Message;
        }
    }

}