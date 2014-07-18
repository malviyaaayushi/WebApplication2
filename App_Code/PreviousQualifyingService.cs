using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class PreviousQualifyingService
{
    private DateTime from;
    private DateTime to;
    private string postHeld;
    private string purpose;

     public DateTime From
    {
        get { return this.from; }
        set { this.from = value; }
    }

     public DateTime To
    {
        get { return this.to; }
        set { this.to = value; }
    }

     public string PostHeld
    {
        get { return this.postHeld; }
        set { this.postHeld = value; }
    }

     public string Purpose
    {
        get { return this.purpose; }
        set { this.purpose = value; }
    }

}
