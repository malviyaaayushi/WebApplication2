using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class ServiceRegister
{
    private String postAndPayDescription;
    private String permanentOrTemporary;
    private String incumbent;
    private String postHeldPermanently;
    private String payInPermanentPost;
    private String officiatingPay;
    private String otherPay;
    private DateTime fromPeriod;
    private DateTime toPeriod;
    private String events1to8;
    private String leaveDescription;
    private String punishmentReference;
    private String remarks;

    public String PostAndPayDescription
    {
        get { return this.postAndPayDescription; }
        set { this.postAndPayDescription = value; }
    }

    public String PermanentOrTemporary
    {
        get { return this.permanentOrTemporary; }
        set { this.permanentOrTemporary = value; }
    }

    public String Incumbent
    {
        get { return this.incumbent; }
        set { this.incumbent = value; }
    }

    public String PostHeldPermanently
    {
        get { return this.postHeldPermanently; }
        set { this.postHeldPermanently = value; }
    }

    public String PayInPermanentPost 
    {
        get { return this.payInPermanentPost; }
        set { this.payInPermanentPost = value; }
    }

    public String OfficiatingPay
    {
        get { return this.officiatingPay; }
        set { this.officiatingPay = value; }
    }

    public String OtherPay
    {
        get { return this.otherPay; }
        set { this.otherPay = value; }
    }

    public DateTime FromPeriod
    {
        get { return this.fromPeriod; }
        set { this.fromPeriod = value; }
    }

    public DateTime ToPeriod
    {
        get { return this.toPeriod; }
        set { this.toPeriod = value; }
    }

    public String Events1to8
    {
        get { return this.events1to8; }
        set { this.events1to8 = value; }
    }

    public String LeaveDescription
    {
        get { return this.leaveDescription; }
        set { this.leaveDescription = value; }
    }

    public String PunishmentReference
    {
        get { return this.punishmentReference; }
        set { this.punishmentReference = value; }
    }

    public String Remarks
    {
        get { return this.remarks; }
        set { this.remarks = value; }
    }
}
