using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class AUserSession
{
    private User thisUser;
    private String warning;
    private String currentBox;
    private String currentLook;

    // private constructor
    private AUserSession()
    {
        thisUser = null;
        warning = null;
        currentBox = "inbox";
        currentLook = "box";
    }

    // Gets the current session.
    public static AUserSession Current
    {
        get
        {
            AUserSession session =
                (AUserSession)HttpContext.Current.Session["__AUserSession__"];
            if (session == null)
            {
                session = new AUserSession();
                HttpContext.Current.Session["__AUserSession__"] = session;
            }
            return session;
        }
    }

    // **** add your session properties here, e.g like this:

    public User ThisUser
    {
        get { return this.thisUser; }
        set { this.thisUser = value; }
    }

    public string Warning
    {
        get { return this.warning; }
        set { this.warning = value; }
    }

    public string CurrentBox
    {
        get { return this.currentBox; }
        set { this.currentBox = value; }
    }

    public string CurrentLook
    {
        get { return this.currentLook; }
        set { this.currentLook = value; }
    }
    
    public void removeAllWarnings()
    {
        this.warning = null;
    }

    public void clearSession() {
        HttpContext.Current.Session.Clear();
    }

    public bool isUserLoggedIn() {
        return this.thisUser != null;
    }
}
