using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


public class Biodata
{
    private string name;
    private string fatherName;
    private string spouseName;
    private string nationality;
    private string religion;
    private string scheduledCaste;
    private string casteName;
    private DateTime dob;
    private string qualificationWhenAppointed;
    private string qualificationAfterwards;
    private string heightCm;
    private string identificationMarks;
    private string permanentHomeAddress;

    public string Name
    {
        get { return this.name; }
        set { this.name = value; }
    }

    public string FatherName
    {
        get { return this.fatherName; }
        set { this.fatherName = value; }
    }

    public string SpouseName
    {
        get { return this.spouseName; }
        set { this.spouseName = value; }
    }

    public string Nationality
    {
        get { return this.nationality; }
        set { this.nationality = value; }
    }
     
    public string Religion
    {
        get { return this.religion; }
        set { this.religion = value; }
    }

    public string ScheduledCaste
    {
        get { return this.scheduledCaste; }
        set { this.scheduledCaste = value; }
    }

    public string CasteName
    {
        get { return this.casteName; }
        set { this.casteName = value; }
    }
    
    public DateTime Dob
    {
        get { return this.dob; }
        set { this.dob = value; }
    }
    
    public string QualificationWhenAppointed
    {
        get { return this.qualificationWhenAppointed; }
        set { this.qualificationWhenAppointed = value; }
    }

    public string QualificationAfterwards
    {
        get { return this.qualificationAfterwards; }
        set { this.qualificationAfterwards = value; }
    }

    public string HeightCm
    {
        get { return this.heightCm; }
        set { this.heightCm = value; }
    }

    public string IdentificationMarks
    {
        get { return this.identificationMarks; }
        set { this.identificationMarks = value; }
    }
    
    public string PermanentHomeAddress
    {
        get { return this.permanentHomeAddress; }
        set { this.permanentHomeAddress = value; }
    }

}
