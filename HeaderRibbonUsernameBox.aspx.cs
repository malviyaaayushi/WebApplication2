using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;



public partial class HeaderRibbonUsernameBox : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //headerRibbonUsername.InnerText = AUserSession.Current.ThisUser.UserName;
        headerRibbonUsernameBoxName.InnerText = AUserSession.Current.ThisUser.Name;
        headerRibbonUsernameBoxPost.InnerText = AUserSession.Current.ThisUser.Designation;
        headerRibbonUsernameBoxImage.ImageUrl = "images/" + AUserSession.Current.ThisUser.UserName.ToLower() + ".jpg";
        //profileNavImage.ImageUrl = "images/" + AUserSession.Current.ThisUser.UserName.ToLower() + ".jpg";
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


}
