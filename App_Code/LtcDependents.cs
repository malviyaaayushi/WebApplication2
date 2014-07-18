using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class LtcDependents
{
    private string name;
    private string relationship;
    private DateTime dob;
    private string employmentDetails;
    private double totalIncome;

    public string Name
    {
        get { return this.name; }
        set { this.name = value; }
    }

     public string Relationship
    {
        get { return this.relationship; }
        set { this.relationship = value; }
    }

     public DateTime Dob
    {
        get { return this.dob; }
        set { this.dob = value; }
    }

     public string EmploymentDetails
    {
        get { return this.employmentDetails; }
        set { this.employmentDetails = value; }
    }

     public double TotalIncome
    {
        get { return this.totalIncome; }
        set { this.totalIncome = value; }
    }

    
}
