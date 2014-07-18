using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

public partial class UpdateDetails : System.Web.UI.Page
{
    int userId;
    string userName, formNameValue, formFieldValue;
    List<String> fieldNames;

    protected void Page_Load(object sender, EventArgs e)
    {
        Database.connectToDatabse();

        if (Request.QueryString["userName"] != null)
        {
            userName = Request.QueryString["userName"];
            name.Text = userName;
            if (Request.QueryString["formNameValue"] != null)
            {
                formNameValue = Request.QueryString["formNameValue"];
                FindFormFields(formNameValue);
                if (!IsPostBack)
                {
                    formName.SelectedValue = formNameValue;
                }
                if (Request.QueryString["formFieldValue"] != null)
                {
                    formFieldValue = Request.QueryString["formFieldValue"];
                    if (!IsPostBack) { fieldName.SelectedValue = formFieldValue; }
                    GetFormTable();
                    if (Request.QueryString["ChangeFieldName"] != null)
                    {
                        try
                        {
                            valuesBox.Attributes["style"] = "display:block;";
                            userId = UserFuncs.FindUserIdFromUserName(userName);
                            if (userId != -1)
                            {
                                int changeFieldItemNo = int.Parse(Request.QueryString["ChangeFieldName"]);
                                FindOldValue(changeFieldItemNo);
                            }
                            else
                            {
                                updateDetailsWarning.InnerText = "User not found";
                            }
                        }
                        catch (Exception ex)
                        {
                            updateDetailsWarning.InnerText = "Oops! Some error occured" + ex.Message;
                        }
                    }
                }
            }
        }
    }

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> GetCompletionList(string prefixText)
    {
        return UserFuncs.FindUserNameList(prefixText);
    }

    private void FindOldValue(int changeFieldItemNo)
    {
        if (userId != -1)
        {
            oldValue.Text = UserFuncs.FindValue(userId, formName.Text, fieldName.Text, changeFieldItemNo);
        }
    }

    protected void FindFormFields(string formNameValue)
    {
        if (!Page.IsPostBack)
        {
            fieldNames = new List<string>();
            switch (formNameValue)
            {
                case "Biodata": fieldNames.Add("name");
                    fieldNames.Add("fatherName");
                    fieldNames.Add("spouseName");
                    fieldNames.Add("nationality");
                    fieldNames.Add("religion");
                    fieldNames.Add("scheduledCaste");
                    fieldNames.Add("casteName");
                    fieldNames.Add("DOB");
                    fieldNames.Add("qualificationWhenAppointed");
                    fieldNames.Add("qualificationAfterwards");
                    fieldNames.Add("heightCm");
                    fieldNames.Add("identificationMarks");
                    fieldNames.Add("permanentHomeAddress");
                    break;
                case "CertificationAttestation": fieldNames.Add("medicalTestBy");
                    fieldNames.Add("medicalTestDate");
                    fieldNames.Add("medicalFileNo");
                    fieldNames.Add("antecedentsFileNo");
                    fieldNames.Add("biodataFileNo");
                    fieldNames.Add("RBScheme");
                    fieldNames.Add("RBFileNo");
                    fieldNames.Add("RBDate");
                    fieldNames.Add("pranNo");
                    fieldNames.Add("npsFileNo");
                    break;
                case "ForeignService": fieldNames.Add("fromPeriod");
                    fieldNames.Add("toPeriod");
                    fieldNames.Add("foreignEmployerDetails");
                    fieldNames.Add("LPCPFpayable");
                    fieldNames.Add("LPCPFreceived");
                    fieldNames.Add("MRNo");
                    fieldNames.Add("MRDate");
                    break;
                case "LeaveBalance": fieldNames.Add("casualLeaveBalance");
                    fieldNames.Add("clBalance");
                    fieldNames.Add("specialClBalance");
                    fieldNames.Add("specialLeaveBalance");
                    fieldNames.Add("halfPayLeaveBalance");
                    fieldNames.Add("commutedLeaveBalance");
                    fieldNames.Add("earnedLeaveBalance");
                    fieldNames.Add("extraOrdinaryLeaveBalance");
                    fieldNames.Add("maternityLeaveBalance");
                    fieldNames.Add("hospitalLeaveBalance");
                    fieldNames.Add("quarantineLeaveBalance");
                    fieldNames.Add("leaveNotLeaveBalance");
                    fieldNames.Add("sabbaticalLeaveBalance");
                    fieldNames.Add("vacationBalance");
                    break;
                case "LeaveDetails": fieldNames.Add("leaveStatus");
                    break;
                case "LeaveTravelConcession": fieldNames.Add("blockYear");
                    fieldNames.Add("fromDate");
                    fieldNames.Add("toDate");
                    fieldNames.Add("relativeName");
                    fieldNames.Add("relation");
                    fieldNames.Add("homeTown");
                    fieldNames.Add("otherPlaces");
                    fieldNames.Add("amountPaid");
                    fieldNames.Add("certifyingOfficer");
                    break;
                case "LtcDeclaration": fieldNames.Add("homeTown");
                    fieldNames.Add("taluka");
                    fieldNames.Add("district");
                    fieldNames.Add("state");
                    fieldNames.Add("nearestRlyStation");
                    fieldNames.Add("reasonOneForDeclaration");
                    fieldNames.Add("reasonTwoForDeclaration");
                    break;
                case "LtcDependents": fieldNames.Add("name");
                    fieldNames.Add("relationship");
                    fieldNames.Add("dob");
                    fieldNames.Add("employmentDetails");
                    fieldNames.Add("totalIncome");
                    break;
                case "PreviousQualifyingService": fieldNames.Add("[from]");
                    fieldNames.Add("[to]");
                    fieldNames.Add("postHeld");
                    fieldNames.Add("purpose");
                    break;
                case "ProfileInformation": fieldNames.Add("designation");
                    fieldNames.Add("department");
                    fieldNames.Add("empID");
                    fieldNames.Add("Age");
                    fieldNames.Add("Sex");
                    break;
                case "ServiceRegister": fieldNames.Add("postAndPayDescription");
                    fieldNames.Add("permanentOrTemporary");
                    fieldNames.Add("incumbent");
                    fieldNames.Add("postHeldPermanently");
                    fieldNames.Add("payInPermanentPost");
                    fieldNames.Add("officiatingPay");
                    fieldNames.Add("otherPay");
                    fieldNames.Add("fromPeriod");
                    fieldNames.Add("toPeriod");
                    fieldNames.Add("events1to8");
                    fieldNames.Add("leaveDescription");
                    fieldNames.Add("punishmentReference");
                    fieldNames.Add("remarks");
                    break;
            }
            fieldName.Items.Clear();
            int count = fieldNames.Count;
            ListItem option;
            for (int i = 0; i < count; i++)
            {
                option = new ListItem();
                option.Value = fieldNames[i];
                option.Text = fieldNames[i];
                fieldName.Items.Add(option);
            }
        }
    }

    protected void GetFormTable()
    {
        string url = Request.QueryString.ToString();
        if (Request.QueryString["ChangeFieldName"] != null)
        {
            url = url.Replace("&ChangeFieldName=" + Request.QueryString["ChangeFieldName"], "");
        }
        updateDetailsTablePlaceholder.Controls.Clear();
        if (formNameValue.Equals("ServiceRegister"))
        {
            ServiceBookFuncs serviceBookFuncs = new ServiceBookFuncs();
            serviceBookFuncs.fetchServiceRegister();
            Profile profile = new Profile();
            HtmlGenericControl table = profile.GenerateServiceRegister(serviceBookFuncs.serviceRegisterList, true, url);
            HtmlGenericControl div = new HtmlGenericControl("div");
            div.Attributes["style"] = "overflow: scroll; overflow:auto;";
            div.Controls.Add(table);
            updateDetailsTablePlaceholder.Controls.Add(div);
        }
        else if (formName.Text.Equals("LeaveTravelConcession"))
        {
            ServiceBookFuncs serviceBookFuncs = new ServiceBookFuncs();
            Profile profile = new Profile();
            serviceBookFuncs.fetchLTCRecord();
            HtmlGenericControl table = profile.GenerateLtcRecord(serviceBookFuncs.ltcRecordList, true, url);
            updateDetailsTablePlaceholder.Controls.Add(table);
        }
        else if (formName.Text.Equals("LtcDependents"))
        {
            ServiceBookFuncs serviceBookFuncs = new ServiceBookFuncs();
            Profile profile = new Profile();
            serviceBookFuncs.fetchLtcDependents();
            HtmlGenericControl table = profile.GenerateLtcDependents(serviceBookFuncs.ltcDependentsList, true, url);
            updateDetailsTablePlaceholder.Controls.Add(table);
        }
        else if (formName.Text.Equals("ForeignService"))
        {
            ServiceBookFuncs serviceBookFuncs = new ServiceBookFuncs();
            Profile profile = new Profile();
            serviceBookFuncs.fetchForeignServices();
            HtmlGenericControl table = profile.GenerateForeignServices(serviceBookFuncs.foreignServiceList,true,url);
            updateDetailsTablePlaceholder.Controls.Add(table);
        }
        else if (formName.Text.Equals("PreviousQualifyingService"))
        {
            ServiceBookFuncs serviceBookFuncs = new ServiceBookFuncs();
            Profile profile = new Profile();
            serviceBookFuncs.fetchPreviousQualifyingService();
            HtmlGenericControl table = profile.GenerateQualifyingServices(serviceBookFuncs.previousQualifyingServiceList, true, url);
            updateDetailsTablePlaceholder.Controls.Add(table);
        }
    }

    protected void UpdateDetailsButton_Click(object sender, EventArgs e)
    {
        Database.connectToDatabse();
        Database.executeQuery("UPDATE " + formNameValue + " SET " + formFieldValue + "= '" + newValue.Text + "' WHERE " +formFieldValue + " = '" + oldValue.Text +"'");
        updateDetailsTablePlaceholder.Controls.Clear();
        GetFormTable();
        updateDetailsWarningDown.InnerText = "Value is successfully updated";
        Database.disconnectToDatabase();
    }

    protected void fieldName_SelectedIndexChanged(object sender, EventArgs e)
    {
        if ((Request.QueryString["userName"] != null) && (Request.QueryString["formNameValue"] != null))
        {
            string url = "UpdateDetails.aspx?";
            if (Request.QueryString["formFieldValue"] == null)
            {
                url += Request.QueryString.ToString() + "&formFieldValue=" + fieldName.SelectedValue;
            }
            else
            {
                url += "userName=" + Request.QueryString["userName"] + "&formNameValue=" + Request.QueryString["formNameValue"] + "&formFieldValue=" + fieldName.SelectedValue;
                
            }
            if (!(formNameValue == "LeaveTravelConcession" || formNameValue == "LtcDependents" || formNameValue == "PreviousQualifyingService" || formNameValue == "ServiceRegister" || formNameValue == "ForeignService"))
            {
                url += "&ChangeFieldName=0";
            }
            Response.Redirect(url);
        }
    }

    protected void formName_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Request.QueryString["userName"] != null)
        {
            if (Request.QueryString["formNameValue"] == null)
            {
                Response.Redirect("UpdateDetails.aspx?" + Request.QueryString.ToString() + "&formNameValue=" + formName.SelectedValue);
            }
            else
            {
                Response.Redirect("UpdateDetails.aspx?userName=" + Request.QueryString["userName"] + "&formNameValue=" + formName.SelectedValue);
            }
        }
    }

    protected void name_TextChanged(object sender, EventArgs e)
    {
        try
        {
            userName = name.Text;
            userName = userName.Split('(')[1];
            userName = userName.Split(')')[0];
            Response.Redirect("UpdateDetails.aspx?userName=" + userName);
        }
        catch (Exception ex)
        {
            updateDetailsWarning.InnerText = "Reset All fields";
        }
    }

    protected void clearDetailsButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("UpdateDetails.aspx");
    }
}