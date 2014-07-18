using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;



public class ForeignService
{

    private DateTime from;
    private DateTime to;
    private string fEmployerDetails;
    private string fLPCPFpayable;
    private string fLPCPFreceived;
    private string fMRNo;
    private DateTime fMRDate;

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

    public string FEmployerDetails
    {
        get { return this.fEmployerDetails; }
        set { this.fEmployerDetails = value; }
    }

    public string FLPCPFpayable
    {
        get { return this.fLPCPFpayable; }
        set { this.fLPCPFpayable = value; }
    }

    public string FLPCPFreceived
    {
        get { return this.fLPCPFreceived; }
        set { this.fLPCPFreceived = value; }
    }

    public string FMRNo
    {
        get { return this.fMRNo; }
        set { this.fMRNo = value; }
    }

    public DateTime FMRDate
    {
        get { return this.fMRDate; }
        set { this.fMRDate = value; }
    }
}
