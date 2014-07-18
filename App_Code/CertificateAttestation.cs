using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class CertificateAttestation
{
    private string medicalTestBy;
    private DateTime medicalTestDate;
    private string medicalFileNo;
    private string antecedentsFileNo;
    private string biodataFileNo;
    private string rbScheme;
    private string rbFileNo;
    private DateTime rbDate;
    private string pranNo;
    private string npsFileNo;
    
    public String MedicalTestBy
    {
        get { return this.medicalTestBy; }
        set { this.medicalTestBy = value; }
    }

    public DateTime MedicalTestDate 
    {
        get { return this.medicalTestDate; }
        set { this.medicalTestDate = value; }
    }

    public string MedicalFileNo
    {
        get { return this.medicalFileNo; }
        set { this.medicalFileNo = value; }
    }

    public string AntecedentsFileNo
    {
        get { return this.antecedentsFileNo; }
        set { this.antecedentsFileNo = value; }
    }

    public string BiodataFileNo
    {
        get { return this.biodataFileNo; }
        set { this.biodataFileNo = value; }
    }

    public String RbScheme
    {
        get { return this.rbScheme; }
        set { this.rbScheme = value; }
    }

    public string RbFileNo
    {
        get { return this.rbFileNo; }
        set { this.rbFileNo = value; }
    }

    public DateTime RbDate
    {
        get { return this.rbDate; }
        set { this.rbDate = value; }
    }

    public string PranNo
    {
        get { return this.pranNo; }
        set { this.pranNo = value; }
    }

    public string  NpsFileNo
    {
        get { return this.npsFileNo; }
        set { this.npsFileNo = value; }
    }
}