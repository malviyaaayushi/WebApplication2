using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


public class User
{
    private string userName;
    private int userId;
    private int actorRank;
    private int type;
    private String name;
    private String designation;
    
    public string UserName
    {
        get { return this.userName; }
        set { this.userName = value; }
    }

    public int UserId
    {
        get { return this.userId; }
        set { this.userId = value; }
    }

    public int ActorRank
    {
        get { return this.actorRank; }
        set { this.actorRank = value; }
    }

    public int Type
    {
        get { return this.type; }
        set { this.type = value; }
    }

    public String Designation
    {
        get { return this.designation; }
        set { this.designation = value; }
    }

    public String Name
    {
        get { return this.name; }
        set { this.name = value; }
    }

}
