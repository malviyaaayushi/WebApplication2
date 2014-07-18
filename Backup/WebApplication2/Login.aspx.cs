using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication2
{
    public partial class Login : System.Web.UI.Page
    {
        private static string cookie_userName = "ffpdf.userId", cookie_pwd = "ffpdf.pwd";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.HttpMethod == "POST")
            {
                String userName = (Request.Form["UserName"]).Trim();
                String txtPassword = (Request.Form["Password"]).Trim();

                if (LoginValidate(userName, txtPassword))
                {
                    UserFuncs usrFuncs = new UserFuncs();
                    User user = usrFuncs.loginUser(userName, txtPassword);

                    if (user != null)
                    {
                        if (RememberMeChkBx.Checked)
                            RememberMe(userName, txtPassword);
                        else
                            RemoveRememberMe();
                        usrFuncs.giveAccessToUser(user);
                    }
                }
                else
                {
                    AUserSession.Current.Warning = "All fields are required";
                }
                
                LWarning.Text = AUserSession.Current.Warning;
            }
            else
            {
                if (FindRememberMe()) {
                    UserFuncs userFuncs = new UserFuncs();
                    HttpCookie c1 = Request.Cookies[cookie_userName];
                    HttpCookie c2 = Request.Cookies[cookie_pwd];
                    User user = userFuncs.loginUser(RijndaelSimple.Decrypt(c1.Value), RijndaelSimple.Decrypt(c2.Value));
                    if (user != null)
                    {
                        userFuncs.giveAccessToUser(user);
                    }
                }
            }
            if (IsPostBack) {
                if (AUserSession.Current.ThisUser != null)
                {
                    AUserSession.Current.removeAllWarnings();
                    Response.Redirect("Default.aspx");
                }
                return;
            }

        }

        protected bool LoginValidate(String userName, String password)
        {
            return !(string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password));
        }

        private void RememberMe(string email, string pwd)
        {
            AddRemoveCookies(email, pwd, true);
        }

        private void AddRemoveCookies(string userName, string pwd, bool bAdd)
        {
            HttpCookie c1 = bAdd ? new HttpCookie(cookie_userName, RijndaelSimple.Encrypt(userName)) : new HttpCookie(cookie_userName);
            HttpCookie c2 = bAdd ? new HttpCookie(cookie_pwd, RijndaelSimple.Encrypt(pwd)) : new HttpCookie(cookie_pwd);

            c1.Expires = c2.Expires = bAdd ? DateTime.Now.AddDays(2) : DateTime.Now.AddDays(-1d);
            Response.Cookies.Add(c1);
            Response.Cookies.Add(c2);
        }

        private void RemoveRememberMe()
        {
            AddRemoveCookies(null, null, false);
        }

        private bool FindRememberMe()
        {
            HttpCookie c1 = Request.Cookies[cookie_userName];
            HttpCookie c2 = Request.Cookies[cookie_pwd];

            RememberMeChkBx.Checked = (c1 != null && c2 != null);
            return RememberMeChkBx.Checked;
        }
    }
}