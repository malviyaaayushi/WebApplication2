using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


public class Application
{
    private int applicationId;
    private int applicantId;
    private String applicantName;
    private int leaveStatus;
    private DateTime date;
    private DateTime fromDate;
    private int leaveDuration;
    private String reason;
    private int recommAuthId;
    private int approvAuthId;
    private int applicationType;
    private int availConcession;
    private String leaveAddress;
    private String commentByApproving;
    private String commentByRecommending;
    private int rejectedBy;
    private int read;
    private int medicalCertificate;
    private int commuted;

    public int ApplicationId
    {
        get { return this.applicationId; }
        set { this.applicationId = value; }
    }

    public int ApplicantId
    {
        get { return this.applicantId; }
        set { this.applicantId = value; }
    }

    public String ApplicantName
    {
        get { return this.applicantName; }
        set { this.applicantName = value; }
    }

    private int LeaveStatus
    {
        get { return this.leaveStatus; }
        set { this.leaveStatus = value; }
    }

    public DateTime Date
    {
        get { return this.date; }
        set { this.date = value; }
    }

    public DateTime FromDate
    {
        get { return this.fromDate; }
        set { this.fromDate = value; }
    }

    private int LeaveDuration
    {
        get { return this.leaveDuration; }
        set { this.leaveDuration = value; }
    }

    private String Reason
    {
        get { return this.reason; }
        set { this.reason = value; }
    }

    public int RecommAuthId
    {
        get { return this.recommAuthId; }
        set { this.recommAuthId = value; }
    }

    public int ApprovAuthId
    {
        get { return this.approvAuthId; }
        set { this.approvAuthId = value; }
    }

    public int ApplicationType
    {
        get { return this.applicationType; }
        set { this.applicationType = value; }
    }

    private int AvailConcession
    {
        get { return this.availConcession; }
        set { this.availConcession = value; }
    }

    private String LeaveAddress
    {
        get { return this.leaveAddress; }
        set { this.leaveAddress = value; }
    }
    
    private String CommentByApproving
    {
        get { return this.commentByApproving; }
        set { this.commentByApproving = value; }
    }

    private String CommentByRecommending
    {
        get { return this.commentByRecommending; }
        set { this.commentByRecommending = value; }
    }

    private int RejectedBy
    {
        get { return this.rejectedBy; }
        set { this.rejectedBy = value; }
    }

    private int Read
    {
        get { return this.read; }
        set { this.read = value; }
    }

    private int MedicalCertificate
    {
        get { return this.medicalCertificate; }
        set { this.medicalCertificate = value; }
    }

    private int Commuted
    {
        get { return this.commuted; }
        set { this.commuted = value; }
    }

}