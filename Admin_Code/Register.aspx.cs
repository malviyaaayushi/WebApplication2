using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication2.Admin_Code;
using System.Text.RegularExpressions;

public partial class Register : System.Web.UI.Page
{
    private Biodata biodata = new Biodata();
    private CertificateAttestation certificateAttestation = new CertificateAttestation();
    private LtcDeclaration ltcDeclaration = new LtcDeclaration();
    public int userID,uage,utype,uactorrank;
    public string udesgn, udeprt, uempNo, usex, uname, uuserName, upswd;
    public String PopupMsg;
    
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    private void InsertCredentials()
    {
        try
        {
            DataRow drow = Database.getRowFromTable("LoginCredentials");
            drow["name"] = name.Value;
            drow["userName"] = userName.Value;
            drow["pswd"] = RijndaelSimple.Encrypt(password.Value);
            drow["type"] = utype;
            drow["actorrank"] = uactorrank;
            drow["designation"] = udesgn;
            Database.updateTable(drow);


            Database.connectToDatabse();
            string query = "select * FROM LoginCredentials WHERE [userName]='" + userName.Value + "'";
            Database.executeQuery(query);
            userID = Database.getDataTable().Rows[0].Field<int>("userID");
            Database.disconnectToDatabase();

        }
        catch (Exception ex)
        {
            declarationWarningUp.InnerText = ex.Message; 
        }
     }

    private void InsertProfileInformation()
    {
        try
        {
            DataRow drow = Database.getRowFromTable("ProfileInformation");
            drow["designation"] = designation.Value;
            drow["department"] = department.Value;
            drow["userID"] = userID;
            drow["empID"] = employeeNo.Value;
            drow["Name"] = Regex.Replace(name.Value, "[^A-Za-z0-9$]", "");
            drow["Age"] = int.Parse(Regex.Replace(Age.Value, "[^A-Za-z0-9$]", ""));
            drow["Sex"] = Regex.Replace(Sex.Value, "[^A-Za-z0-9$]", "");
            // drow["DOB"] =
            Database.updateTable(drow);
        }
        catch (Exception ex)
        {
            declarationWarningUp.InnerText = ex.Message;
        }
    }

    private void InsertBiodata()
    {
        try
        {
            DataRow drow = Database.getRowFromTable("Biodata");
            drow["userID"] = userID;
            drow["name"] = name.Value;
            drow["fatherName"] = fathersname.Value; 
            drow["spouseName"] = spousename.Value;
            drow["nationality"] = nationality.Value; 
            drow["religion"] = religion.Value; 
            drow["scheduledCaste"] = scst.Value;
            drow["casteName"] = casteName.Value; 
            //drow["DOB"] = biodata.Dob;
            drow["qualificationWhenAppointed"] = appointed.Value; 
            drow["qualificationAfterwards"] = subsequent.Value;
            drow["heightCm"] = height.Value; 
            drow["identificationMarks"] = identification.Value;
            drow["permanentHomeAddress"] = address.Value;


            Database.updateTable(drow);

            declarationWarningUp.InnerText = certificateAttestation.MedicalTestBy + certificateAttestation.MedicalFileNo + userID + biodata.Name + biodata.Nationality;
           
        }
        catch (Exception ex)
        {
            declarationWarningUp.InnerText = ex.Message +biodata.Name ;//"Problem while registering user.3";
        }

    }

    private void InsertCertificateAttestation()
    {
        try
        {
            DataRow drow = Database.getRowFromTable("CertificationAttestation");

            drow["userID"] = userID;
            drow["medicalTestBy"] = medicalTestBy.Value;
            // drow["medicalTestDate"] = certificateAttestation.MedicalTestDate;
            drow["medicalFileNo"] = medicalTestSrNo.Value;
            drow["antecedentsFileNo"] = characterSrNo.Value;
            drow["biodataFileNo"] = biodataVerificationSrNo.Value;
            drow["RBScheme"] = schemeElected.Value;
            drow["RBFileNo"] = schemeFileNo.Value;
            //drow["RBDate"] = certificateAttestation.RbDate;
            drow["pranNo"] = pranNo.Value;
            drow["npsFileNo"] = npsFileNo.Value;
            Database.updateTable(drow);
        }
        catch (Exception ex)
        {
            declarationWarningUp.InnerText = ex.Message ;//"Problem while registering user.4";
        }
    }

    private void InsertLtcDeclaration()
    {
        try
        {
            DataRow drow = Database.getRowFromTable("LtcDeclaration");

            drow["userID"] = userID;
            drow["homeTown"] = homeTown.Value;
            drow["taluka"] = taluka.Value;
            drow["district"] = district.Value;
            drow["state"] = state.Value;
            drow["nearestRlyStation"] = nearestRlyStation.Value;
            drow["reasonOneForDeclaration"] = reasonOneForDeclaration.Value;
            drow["reasonTwoForDeclaration"] =reasonTwoForDeclaration.Value;
            Database.updateTable(drow);
        }
        catch (Exception ex)
        {
            AUserSession.Current.Warning = ex.Message ;//"Problem while registering user.";
        }
            
    }

    private void InsertAllInfo()
    {
        try
        {
            InsertCredentials();
            InsertProfileInformation();
            InsertBiodata();
            InsertCertificateAttestation();
            InsertLtcDeclaration();
            ltcDeclarationDiv.Attributes["style"] = "display:none";
            PopupMsg = "User is registered successfully";
            Response.Redirect("Register.aspx");
        }
        catch (Exception ex)
        {
            PopupMsg = ex.Message;
        }
    }

    protected void Submit_Click(object sender, EventArgs e)
    {
        if (homeTown.Value.Trim() == "" || taluka.Value.Trim() == "" || district.Value.Trim() == "" || nearestRlyStation.Value.Trim() == "" || reasonOneForDeclaration.Value.Trim() == "" || reasonTwoForDeclaration.Value.Trim() == "")
        {
            declarationWarningUp.InnerText = "All fields are required.";
        }
        else
        {
            try
            {
                ltcDeclaration.HomeTown = homeTown.Value;
                ltcDeclaration.Taluka = taluka.Value;
                ltcDeclaration.District = district.Value;
                ltcDeclaration.State = state.Value;
                ltcDeclaration.NearestRlyStation = nearestRlyStation.Value;
                ltcDeclaration.ReasonOneForDeclaration = reasonOneForDeclaration.Value;
                ltcDeclaration.ReasonTwoForDeclaration = reasonTwoForDeclaration.Value;

                InsertAllInfo();
            }
            catch (Exception ex)
            {
                declarationWarningUp.InnerText = ex.Message + "p";
            }
        }
    }

    public void LoginNext_Click(object sender, EventArgs e)
    {
        uname = name.Value;
        uuserName = userName.Value;
        upswd = password.Value;
        string pswdAgain = password_again.Value;
        utype = int.Parse(type.Value);
        uactorrank = int.Parse(actorrank.Value);

        if (uname == "" || uuserName == "" || upswd == "" || pswdAgain == "")
        {
            loginWarningUp.InnerHtml = "All fields are required";
        }
        else
        {
            if (upswd == pswdAgain)
            {
                Database.connectToDatabse();
                string query = "select * FROM LoginCredentials WHERE [userName]='" + userName.Value + "'";
                Database.executeQuery(query);
                if (Database.getDataTable().Rows.Count == 0)
                {
                    loginCredentialsDiv.Attributes["style"] = "display:none";
                    profileInfoDiv.Attributes["style"] = "display:block;";
                    loginWarningUp.InnerText = "";
                }
                else
                {
                    loginWarningUp.InnerText = "This username already exists. Please choose diffrent username";
                }
                Database.disconnectToDatabase();
            }
            else
            {
                loginWarningUp.InnerText = "Passwords should match.";
            }
        }
    }

    protected void ProfileBack_Click(object sender, EventArgs e)
    {
        profileInfoDiv.Attributes["style"] = "display:none;";
        loginCredentialsDiv.Attributes["style"] = "display:block";
    }

    protected void ProfileNext_Click(object sender, EventArgs e)
    {
        udesgn = designation.Value.Trim();
        udeprt = department.Value.Trim();
        uempNo = employeeNo.Value.Trim();
        if (Sex.Value == "0")
            usex = "M";
        else
            usex = "F";
        try
        {
            uage = int.Parse(Age.Value.Trim());

            if (udesgn == "" || udeprt == "" || uempNo == "")
                ProfileWarningUp.InnerText = "All fields are required";
            else
            {
                profileInfoDiv.Attributes["style"] = "display:none";
                biodataDiv.Attributes["style"] = "display:block;";
                ProfileWarningUp.InnerText = "";
            }
        }
        catch (Exception ex)
        {
            ProfileWarningUp.InnerText = "Enter proper values in Age field";
        }

    }

    protected void BiodataBack_Click(object sender, EventArgs e)
    {
        biodataDiv.Attributes["style"] = "display:none;";
        profileInfoDiv.Attributes["style"] = "display:block";
    }

    protected void BiodataNext_Click(object sender, EventArgs e)
    {
        if (fathersname.Value.Trim() == "" || spousename.Value.Trim() == "" || nationality.Value.Trim() == "" || scst.Value.Trim() == "" || casteName.Value.Trim() == "" || appointed.Value.Trim() == "" || subsequent.Value.Trim() == "" || identification.Value.Trim() == "" || address.Value.Trim() == "" || height.Value.Trim() == "")
        {
            biodataWarningUp.InnerText= "All fileds are required. ";
        }
        else
        {
            try
            {
                biodata.Name = name.Value;
                biodata.FatherName = fathersname.Value;
                biodata.SpouseName = spousename.Value;
                biodata.Nationality = nationality.Value;
                biodata.Religion = religion.Value;
                biodata.ScheduledCaste = scst.Value;
                biodata.CasteName = casteName.Value;
                //biodata.Dob = DateTime.Parse(DOB.ToString());
                biodata.QualificationWhenAppointed = appointed.Value;
                biodata.QualificationAfterwards = subsequent.Value;
                biodata.HeightCm = height.Value;
                biodata.IdentificationMarks = identification.Value;
                biodata.PermanentHomeAddress = address.Value;


                biodataDiv.Attributes["style"] = "display:none";
                certificateAttestationDiv.Attributes["style"] = "display:block;";
                biodataWarningUp.InnerText = "";
                
            }
            catch (Exception ex)
            {
                biodataWarningUp.InnerText = ex.Message + "*";
            }
            
        }
    }

    protected void CertificationBack_Click(object sender, EventArgs e)
    {
        certificateAttestationDiv.Attributes["style"] = "display:none;";
        biodataDiv.Attributes["style"] = "display:block";
    }

    protected void CertificationNext_Click(object sender, EventArgs e)
    {
        if (medicalTestBy.Value.Trim() == "" || medicalTestSrNo.Value.Trim() == "" || characterSrNo.Value.Trim() == "" || biodataVerificationSrNo.Value.Trim() == "" || schemeElected.Value.Trim() == "" || schemeFileNo.Value.Trim() == "" || pranNo.Value.Trim() == "" || npsFileNo.Value.Trim() == "")
        {
             certificationWarningUp.InnerText = "All fields are required.";
        }
        else
        {
            try
            {
                certificateAttestation.MedicalTestBy = medicalTestBy.Value;
                //certificateAttestation.MedicalTestDate = DateTime.Parse(medicalTestDate.ToString());
                certificateAttestation.MedicalFileNo = medicalTestSrNo.Value;
                certificateAttestation.AntecedentsFileNo = characterSrNo.Value;
                certificateAttestation.BiodataFileNo = biodataVerificationSrNo.Value;
                certificateAttestation.RbScheme = schemeElected.Value;
                certificateAttestation.RbFileNo = schemeFileNo.Value;
                //certificateAttestation.RbDate = DateTime.Parse(schemeDate.ToString());
                certificateAttestation.PranNo = pranNo.Value;
                certificateAttestation.NpsFileNo = npsFileNo.Value;

                certificateAttestationDiv.Attributes["style"] = "display:none";
                ltcDeclarationDiv.Attributes["style"] = "display:block;";
                //certificationWarningUp.InnerText = "";
            }
            catch (Exception ex)
            {
                certificationWarningUp.InnerText = ex.Message;
            }
        }
    }

    protected void LtcDeclarationBack_Click(object sender, EventArgs e)
    {
            ltcDeclarationDiv.Attributes["style"] = "display:none;";
            certificateAttestationDiv.Attributes["style"] = "display:block";
    }
}
