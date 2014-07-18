using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


public class LtcDeclaration
{
    private string homeTown;
    private string taluka;
    private string district;
    private string state;
    private string nearestRlyStation;
    private string reasonOneForDeclaration;
    private string reasonTwoForDeclaration;

    public string HomeTown
    {
        get { return this.homeTown; }
        set { this.homeTown = value; }
    }
    
     public string Taluka
    {
        get { return this.taluka; }
        set { this.taluka = value; }
    }

     public string District
    {
        get { return this.district; }
        set { this.district = value; }
    }

     public string State
    {
        get { return this.state; }
        set { this.state = value; }
    }

     public string NearestRlyStation
    {
        get { return this.nearestRlyStation; }
        set { this.nearestRlyStation = value; }
    }

     public string ReasonOneForDeclaration
    {
        get { return this.reasonOneForDeclaration; }
        set { this.reasonOneForDeclaration = value; }
    }
    
     public string ReasonTwoForDeclaration
    {
        get { return this.reasonTwoForDeclaration; }
        set { this.reasonTwoForDeclaration = value; }
    }
}
