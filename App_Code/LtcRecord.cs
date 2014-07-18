using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class LtcRecord
{
    private int blockYear;
    private DateTime fromDate;
    private DateTime toDate;
    private string relativeName;
    private string relation;
    private string homeTown;
    private string otherPlaces;
    private string amountPaid;
    private string certifyingOfficer;

    public int BlockYear
    {
        get { return this.blockYear; }
        set { this.blockYear = value; }
    }

    public DateTime FromDate
    {
        get { return this.fromDate; }
        set { this.fromDate = value; }
    }

    public DateTime ToDate
    {
        get { return this.toDate; }
        set { this.toDate = value; }
    }

    public string RelativeName
    {
        get { return this.relativeName; }
        set { this.relativeName = value; }
    }

    public string Relation
    {
        get { return this.relation; }
        set { this.relation = value; }
    }

    public string HomeTown
    {
        get { return this.homeTown; }
        set { this.homeTown = value; }
    }

    public string OtherPlaces
    {
        get { return this.otherPlaces; }
        set { this.otherPlaces = value; }
    }

    public string AmountPaid
    {
        get { return this.amountPaid; }
        set { this.amountPaid = value; }
    }

    public string CertifyingOfficer
    {
        get { return this.certifyingOfficer; }
        set { this.certifyingOfficer = value; }
    }
}