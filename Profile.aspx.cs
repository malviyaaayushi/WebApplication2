using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.Security;

public partial class Profile : System.Web.UI.Page
{
    public String PopupMsg;
    UserFuncs userFuncs;
    public ServiceBookFuncs serviceBookFuncs;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!AUserSession.Current.isUserLoggedIn())
        {
            LogoutButton_Click(sender, e);
        }
        setValues();
        userFuncs = new UserFuncs();
        serviceBookFuncs = new ServiceBookFuncs();
        serviceBookFuncs.fetchData();

        profile_nav_placeHolder.Controls.Clear();
        Biodata biodata = serviceBookFuncs.biodata;
        HtmlGenericControl table = GenerateBiodata(biodata);
        profile_nav_placeHolder.Controls.Add(table);
        Profile_Click(sender, e);
        AUserSession.Current.removeAllWarnings();
        PopupMsg = AUserSession.Current.Warning;
    }

    protected void Profile_Click(object sender, EventArgs e)
    {
        OpenTab(profileTab);
        service_book_container_placeholder.Controls.Clear();
        HtmlGenericControl title = new HtmlGenericControl("h1");
        title.InnerText = "Profile";
        service_book_container_placeholder.Controls.Add(title);
        ServiceBookFuncs sbf = new ServiceBookFuncs();
        Database.connectToDatabse();
        sbf.fetchForeignServices();
        sbf.fetchPreviousQualifyingService();
        sbf.fetchCertificateAttestation();
        Database.disconnectToDatabase();
        try
        {
            HtmlGenericControl div = GenerateProfile(sbf.previousQualifyingServiceList, sbf.foreignServiceList, sbf.certificateAttestation);
            service_book_container_placeholder.Controls.Add(div);
        }
        catch (Exception ex)
        {
            AUserSession.Current.Warning = ex.Message;
        }
    }


    protected void LeaveAccount_Click(object sender, EventArgs e)
    {
        OpenTab(leaveAccountTab);
        service_book_container_placeholder.Controls.Clear();
        HtmlGenericControl iframe = new HtmlGenericControl("iframe");
        iframe.Attributes["id"] = "leaveDetails_iframe";
        iframe.Attributes["class"] = "iframeCss";
        iframe.Attributes["style"] = "width:100%;";
        iframe.Attributes["src"] = "leaveAccount.aspx";
        service_book_container_placeholder.Controls.Add(iframe);
    }

    protected void LtcRecord_Click(object sender, EventArgs e)
    {
        service_book_container_placeholder.Controls.Clear();
        OpenTab(ltcRecordTab);
        HtmlGenericControl title = new HtmlGenericControl("h1");
        title.InnerText = "LTC RECORD";
        service_book_container_placeholder.Controls.Add(title);
        try
        {
            HtmlGenericControl table = GenerateLtcRecord(serviceBookFuncs.ltcRecordList, false, "");
            service_book_container_placeholder.Controls.Add(table);
        }
        catch (Exception ex)
        {
            HtmlGenericControl p = new HtmlGenericControl("p");
            p.InnerText = "No record found";
            service_book_container_placeholder.Controls.Add(p);
            AUserSession.Current.Warning = ex.Message;
        }
    }

    protected void LtcDeclaration_Click(object sender, EventArgs e)
    {
        service_book_container_placeholder.Controls.Clear();
        OpenTab(ltcDeclarationTab);
        try
        {
            HtmlGenericControl div = GenerateLtcDeclaration(serviceBookFuncs.ltcDeclaration);
            service_book_container_placeholder.Controls.Add(div);

            div = GenerateLtcDependents(serviceBookFuncs.ltcDependentsList, false, "");
            service_book_container_placeholder.Controls.Add(div);
            HtmlGenericControl p = new HtmlGenericControl("p");
            p.InnerText = "I hereby declare that the above-mentioned people are residing with and wholly dependent on me except the following:";
            service_book_container_placeholder.Controls.Add(p);
        }
        catch (Exception ex)
        {
            HtmlGenericControl p = new HtmlGenericControl("p");
            p.InnerText = "Details not found. Please contact the Administrator";
            service_book_container_placeholder.Controls.Add(p);
            AUserSession.Current.Warning = ex.Message;
        }
    }

    protected void ServiceRegister_Click(object sender, EventArgs e)
    {
        service_book_container_placeholder.Controls.Clear();
        OpenTab(serviceRegisterTab);

        HtmlGenericControl title = new HtmlGenericControl("h1");
        title.InnerText = "SERVICE REGISTER";
        service_book_container_placeholder.Controls.Add(title);
        try
        {
            HtmlGenericControl table = GenerateServiceRegister(serviceBookFuncs.serviceRegisterList, false, "");
            service_book_container_placeholder.Controls.Add(table);
        }
        catch (Exception ex)
        {
            AUserSession.Current.Warning = ex.Message;
        }
    }

    protected void OpenTab(HtmlGenericControl id)
    {
        // open this tab and close all others
        HtmlGenericControl[] tabs = { profileTab, leaveAccountTab, ltcRecordTab, ltcDeclarationTab, serviceRegisterTab };
        int size = tabs.Count();
        for (int i = 0; i < size; i++)
        {
            tabs[i].Attributes["class"] = "gmail-tabs-unselected";
        }
        id.Attributes["class"] = "gmail-tabs-selected";
    }

    protected HtmlGenericControl GenerateBiodata(Biodata biodata)
    {
        HtmlGenericControl table = new HtmlGenericControl("table");
        try
        {
            table.Attributes["id"] = "biodataTable";
            table.Attributes["class"] = "pure-table pure-table-horizontal";

            HtmlGenericControl tbody = new HtmlGenericControl("tbody");

            List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
            list.Add(new KeyValuePair<string, string>("Name", biodata.Name));
            list.Add(new KeyValuePair<string, string>("Father Name", biodata.FatherName));
            list.Add(new KeyValuePair<string, string>("Spouse Name", biodata.SpouseName));
            list.Add(new KeyValuePair<string, string>("Nationality", biodata.Nationality));
            list.Add(new KeyValuePair<string, string>("Religion", biodata.Religion));
            list.Add(new KeyValuePair<string, string>("Scheduled Caste", biodata.ScheduledCaste));
            list.Add(new KeyValuePair<string, string>("Caste Name", biodata.CasteName));
            list.Add(new KeyValuePair<string, string>("Date of Birth", biodata.Dob.ToString("dd MMMM yyyy")));
            list.Add(new KeyValuePair<string, string>("Qualification Afterwards", biodata.QualificationAfterwards));
            list.Add(new KeyValuePair<string, string>("Qualification When Appointed", biodata.QualificationWhenAppointed));
            list.Add(new KeyValuePair<string, string>("Height(cm)", biodata.HeightCm));
            list.Add(new KeyValuePair<string, string>("Identification Marks", biodata.IdentificationMarks));
            list.Add(new KeyValuePair<string, string>("Permanent Home Address", biodata.PermanentHomeAddress));


            for (int i = 0; i < list.Count; i++)
            {
                string key = list[i].Key;
                string value = list[i].Value;

                HtmlGenericControl tr = new HtmlGenericControl("tr");
                tr.Attributes["style"] = "font-size:small;";
                HtmlGenericControl td1 = new HtmlGenericControl("td");
                HtmlGenericControl td2 = new HtmlGenericControl("td");
                td1.InnerText = key;
                td2.InnerText = value;
                tr.Controls.Add(td1);
                tr.Controls.Add(td2);
                tbody.Controls.Add(tr);
            }
            table.Controls.Add(tbody);
        }
        catch (Exception ex)
        {
            AUserSession.Current.Warning = ex.Message;
        }
        return table;
    }

    private HtmlGenericControl GenerateProfile(List<PreviousQualifyingService> previousQualifyingServiceList, List<ForeignService> foreignServiceList, CertificateAttestation certificateAttestation)
    {
        HtmlGenericControl div = new HtmlGenericControl("div");

        HtmlGenericControl h1 = new HtmlGenericControl("h2");
        h1.InnerText = "Previos Qualifying Services";
        h1.Attributes["id"] = "h1";
        h1.Attributes["style"] = "float:left padding-left: 20px;";

        HtmlGenericControl h2 = new HtmlGenericControl("h2");
        h2.InnerText = "Foreign Services";
        h2.Attributes["id"] = "h2";
        h2.Attributes["style"] = "float:left padding-left: 20px;";

        HtmlGenericControl h3 = new HtmlGenericControl("h2");
        h3.InnerText = "Certificate and Attestation";
        h3.Attributes["id"] = "h3";
        h3.Attributes["style"] = "float:left padding-left: 20px;";

        div.Controls.Add(h1);
        div.Controls.Add(GenerateQualifyingServices(previousQualifyingServiceList, false, ""));
        div.Controls.Add(h2);
        div.Controls.Add(GenerateForeignServices(foreignServiceList, false, ""));
        div.Controls.Add(h3);
        div.Controls.Add(GenerateCertificationAttestation(certificateAttestation));
        return div;

    }

    public HtmlGenericControl GenerateQualifyingServices(List<PreviousQualifyingService> previousQualifyingServiceList, bool changeButton, string url)
    {
        HtmlGenericControl table1 = new HtmlGenericControl("table");
        table1.Attributes["id"] = "PreviosQualifyingServices";
        table1.Attributes["class"] = "pure-table";
        table1.Attributes["style"] = "font-size: 0.8em";
        HtmlGenericControl thead = new HtmlGenericControl("thead");
        HtmlGenericControl tr = new HtmlGenericControl("tr");
        HtmlGenericControl td = new HtmlGenericControl("td");
        if (changeButton)
        {
            tr.Controls.Add(td);
        }
        td.InnerText = "From";
        tr.Controls.Add(td);
        td = new HtmlGenericControl("td");
        td.InnerText = "To";
        tr.Controls.Add(td);
        td = new HtmlGenericControl("td");
        td.InnerText = "Post Held";
        tr.Controls.Add(td);
        td = new HtmlGenericControl("td");
        td.InnerText = "Purpose for which it qualifies";
        tr.Controls.Add(td);
        thead.Controls.Add(tr);
        table1.Controls.Add(thead);
        HtmlGenericControl tbody = new HtmlGenericControl("tbody");

        int size = previousQualifyingServiceList.Count;
        for (int i = 0; i < size; i++)
        {
            PreviousQualifyingService previousQualifyingService = previousQualifyingServiceList[i];
            tr = new HtmlGenericControl("tr");
            if (changeButton)
            {
                HtmlGenericControl td0 = new HtmlGenericControl("td");
                td0.InnerHtml = "<a class=\"pure-button pure-button-primary\" href=\"?" + url + "&ChangeFieldName=" + i + "\">Change</a>";
                tr.Controls.Add(td0);
            }
            td = new HtmlGenericControl("td");
            td.InnerText = previousQualifyingService.From.ToString("dd-MMM-yyyy");
            tr.Controls.Add(td);
            td = new HtmlGenericControl("td");
            td.InnerText = previousQualifyingService.To.ToString("dd-MMM-yyyy");
            tr.Controls.Add(td);
            td = new HtmlGenericControl("td");
            td.InnerText = previousQualifyingService.PostHeld;
            tr.Controls.Add(td);
            td = new HtmlGenericControl("td");
            td.InnerText = previousQualifyingService.Purpose;
            tr.Controls.Add(td);
            tbody.Controls.Add(tr);
        }
        table1.Controls.Add(tbody);
        return table1;
    }

    public HtmlGenericControl GenerateForeignServices(List<ForeignService> foreignServiceList, bool changeButton, string url)
    {

        // Second Table

        HtmlGenericControl table2 = new HtmlGenericControl("table");
        table2.Attributes["id"] = "ForeignServices";
        table2.Attributes["class"] = "pure-table";
        table2.Attributes["style"] = "font-size: 0.8em";
        HtmlGenericControl thead = new HtmlGenericControl("thead");
        HtmlGenericControl tr = new HtmlGenericControl("tr");
        HtmlGenericControl td = new HtmlGenericControl("td");
        HtmlGenericControl tbody = new HtmlGenericControl("tbody");
        if (changeButton)
        {
            tr.Controls.Add(td);
            td = new HtmlGenericControl("td");
        }
        td.InnerText = "From";
        tr.Controls.Add(td);
        td = new HtmlGenericControl("td");
        td.InnerText = "To";
        tr.Controls.Add(td);
        td = new HtmlGenericControl("td");
        td.InnerText = "Post held and name of Employer";
        tr.Controls.Add(td);
        td = new HtmlGenericControl("td");
        td.InnerText = "Leave and Pension / CPF Contribution payable";
        tr.Controls.Add(td);
        td = new HtmlGenericControl("td");
        td.InnerText = "Leave and Pension / CPF Contribution received";
        tr.Controls.Add(td);
        td = new HtmlGenericControl("td");
        td.InnerText = "M.R. No";
        tr.Controls.Add(td);
        td = new HtmlGenericControl("td");
        td.InnerText = "M.R. Date";
        tr.Controls.Add(td);
        thead.Controls.Add(tr);
        table2.Controls.Add(thead);
        tbody = new HtmlGenericControl("tbody");
        for (int i = 0; i < foreignServiceList.Count; i++)
        {
            ForeignService foreignService = foreignServiceList[i];
            tr = new HtmlGenericControl("tr");
            if (changeButton)
            {
                HtmlGenericControl td0 = new HtmlGenericControl("td");
                td0.InnerHtml = "<a class=\"pure-button pure-button-primary\" href=\"?" + url + "&ChangeFieldName=" + i + "\">Change</a>";
                tr.Controls.Add(td0);
            }

            td = new HtmlGenericControl("td");
            td.InnerText = foreignService.From.ToString("dd-MMM-yyyy");
            tr.Controls.Add(td);
            td = new HtmlGenericControl("td");
            td.InnerText = foreignService.To.ToString("dd-MMM-yyyy");
            tr.Controls.Add(td);
            td = new HtmlGenericControl("td");
            td.InnerText = foreignService.FEmployerDetails;
            tr.Controls.Add(td);
            td = new HtmlGenericControl("td");
            td.InnerText = foreignService.FLPCPFpayable;
            tr.Controls.Add(td);
            td = new HtmlGenericControl("td");
            td.InnerText = foreignService.FLPCPFreceived;
            tr.Controls.Add(td);
            td = new HtmlGenericControl("td");
            td.InnerText = foreignService.FMRNo;
            tr.Controls.Add(td);
            td = new HtmlGenericControl("td");
            td.InnerText = foreignService.FMRDate.ToString("dd-MMM-yyyy");
            tr.Controls.Add(td);
            tbody.Controls.Add(tr);
        }
        table2.Controls.Add(tbody);
        return table2;
    }

    protected HtmlGenericControl GenerateCertificationAttestation(CertificateAttestation certificateAttestation)
    {
        // Third Table
        HtmlGenericControl table3 = new HtmlGenericControl("table");
        table3.Attributes["id"] = "CertificateAttestation";
        table3.Attributes["class"] = "pure-table";
        table3.Attributes["style"] = "font-size: 0.8em;";

        HtmlGenericControl thead = new HtmlGenericControl("thead");
        HtmlGenericControl tr;
        HtmlGenericControl td = new HtmlGenericControl("td");
        HtmlGenericControl tbody = new HtmlGenericControl("tbody");
        tr = new HtmlGenericControl("tr");
        tr.Attributes["class"] = "pure-table-even";
        HtmlGenericControl td1 = new HtmlGenericControl("td");
        td1.InnerText = "Medical Examination";
        string s = "Medically Examined On " + certificateAttestation.MedicalTestDate.ToString() + " by " + certificateAttestation.MedicalTestBy +
                 ". Medical Certificate is in safe custody vide Sr. No " + certificateAttestation.MedicalFileNo.ToString();
        HtmlGenericControl td2 = new HtmlGenericControl("td");
        td2.InnerText = s;
        tr.Controls.Add(td1);
        tr.Controls.Add(td2);
        table3.Controls.Add(tr);


        tr = new HtmlGenericControl("tr");
        tr.Attributes["class"] = "pure-table-odd";
        td1 = new HtmlGenericControl("td");
        td2 = new HtmlGenericControl("td");
        td1.InnerText = "Character and Antecedents";
        s = "Characer and Antecedents have been verified and the verification report is kept in safe custody vide Sr.No " + certificateAttestation.AntecedentsFileNo;
        td2.InnerText = s;
        tr.Controls.Add(td1);
        tr.Controls.Add(td2);
        table3.Controls.Add(tr);


        tr = new HtmlGenericControl("tr");
        tr.Attributes["class"] = "pure-table-even";
        td1 = new HtmlGenericControl("td");
        td2 = new HtmlGenericControl("td");
        td1.InnerText = "Verification of Biodata";
        s = "Original documents for Biodata have been verified. Attested copies have been filed at Sr.No  " + certificateAttestation.BiodataFileNo;
        td2.InnerText = s;
        tr.Controls.Add(td1);
        tr.Controls.Add(td2);
        table3.Controls.Add(tr);

        tr = new HtmlGenericControl("tr");
        tr.Attributes["class"] = "pure-table-odd";
        td1 = new HtmlGenericControl("td");
        td1.Attributes["style"] = "font-size:1.5em;";
        td1.Attributes["colspan"] = "2";
        td1.InnerText = "Requirement Benefit Scheme";
        tr.Controls.Add(td1);
        table3.Controls.Add(tr);


        tr = new HtmlGenericControl("tr");
        tr.Attributes["class"] = "pure-table-even";
        td1 = new HtmlGenericControl("td");
        td2 = new HtmlGenericControl("td");
        td1.InnerText = "Scheme Elected";
        td2.InnerText = certificateAttestation.RbScheme;
        tr.Controls.Add(td1);
        tr.Controls.Add(td2);
        table3.Controls.Add(tr);


        tr = new HtmlGenericControl("tr");
        tr.Attributes["class"] = "pure-table-odd";
        td1 = new HtmlGenericControl("td");
        td2 = new HtmlGenericControl("td");
        td1.InnerText = "File No.";
        td2.InnerText = certificateAttestation.RbFileNo.ToString();
        tr.Controls.Add(td1);
        tr.Controls.Add(td2);
        table3.Controls.Add(tr);


        tr = new HtmlGenericControl("tr");
        tr.Attributes["class"] = "pure-table-even";
        td1 = new HtmlGenericControl("td");
        td2 = new HtmlGenericControl("td");
        td1.InnerText = "Date.";
        td2.InnerText = certificateAttestation.RbDate.ToString();
        tr.Controls.Add(td1);
        tr.Controls.Add(td2);
        table3.Controls.Add(tr);


        tr = new HtmlGenericControl("tr");
        tr.Attributes["class"] = "pure-table-odd";
        td1 = new HtmlGenericControl("td");
        td2 = new HtmlGenericControl("td");
        td1.InnerText = "PRAN No.";
        td2.InnerText = certificateAttestation.PranNo.ToString();
        tr.Controls.Add(td1);
        tr.Controls.Add(td2);
        table3.Controls.Add(tr);


        tr = new HtmlGenericControl("tr");
        tr.Attributes["class"] = "pure-table-even";
        td1 = new HtmlGenericControl("td");
        td2 = new HtmlGenericControl("td");
        td1.InnerText = "Nomination for NPS";
        td2.InnerText = certificateAttestation.NpsFileNo.ToString();
        tr.Controls.Add(td1);
        tr.Controls.Add(td2);
        table3.Controls.Add(tr);

        return table3;
    }

    public HtmlGenericControl GenerateServiceRegister(List<ServiceRegister> serviceRegisterList, bool changeButton, string url)
    {
        HtmlGenericControl table = new HtmlGenericControl("table");
        table.Attributes["class"] = "pure-table pure-table-bordered";
        table.Attributes["style"] = "overflow: scroll; overflow: auto;";

        HtmlGenericControl tbody = new HtmlGenericControl("tbody");

        HtmlGenericControl thead = new HtmlGenericControl("thead");

        HtmlGenericControl th1 = new HtmlGenericControl("tr");
        th1.Attributes["class"] = "pure-table-odd";
        th1.Attributes["style"] = "line-height:2;";
        if (changeButton)
        {
            HtmlGenericControl td10 = new HtmlGenericControl("th");
            th1.Controls.Add(td10);
        }
        HtmlGenericControl td11 = new HtmlGenericControl("th");
        td11.Attributes["style"] = "font-weight:normal;text-align:center;";
        td11.InnerText = "Description of Post and Pay";
        HtmlGenericControl td12 = new HtmlGenericControl("th");
        td12.Attributes["style"] = "font-weight:normal;text-align:center;";
        td12.InnerText = "Whether post is permanent or temporary";
        HtmlGenericControl td13 = new HtmlGenericControl("th");
        td13.Attributes["style"] = "font-weight:normal;text-align:center;";
        td13.InnerText = "Whether the incumbent is posted permanently or to officiate";
        HtmlGenericControl td14 = new HtmlGenericControl("th");
        td14.Attributes["style"] = "font-weight:normal;text-align:center;line-height: 180px;";
        td14.Attributes["colspan"] = "4";
        td14.InnerText = "If posted to officiate state";
        HtmlGenericControl td15 = new HtmlGenericControl("th");
        td15.Attributes["colspan"] = "2";
        td15.InnerText = "Period";
        td15.Attributes["style"] = "font-weight:normal;text-align:center;line-height: 180px;";
        HtmlGenericControl td16 = new HtmlGenericControl("th");
        td16.InnerText = "Events affecting columns 1 to 8";
        td16.Attributes["style"] = "font-weight:normal;text-align:center;";
        HtmlGenericControl td17 = new HtmlGenericControl("th");
        td17.InnerText = "Nature and duration of leave taken";
        td17.Attributes["style"] = "font-weight:normal;text-align:center;";
        HtmlGenericControl td18 = new HtmlGenericControl("th");
        td18.InnerText = "Reference to any recorded punishment, censure, reward, etc.";
        td18.Attributes["style"] = "font-weight:normal;text-align:center;";
        HtmlGenericControl td19 = new HtmlGenericControl("th");
        td19.InnerText = "Remarks";
        td19.Attributes["style"] = "font-weight:normal;text-align:center;line-height: 180px;";
        th1.Controls.Add(td11);
        th1.Controls.Add(td12);
        th1.Controls.Add(td13);
        th1.Controls.Add(td14);
        th1.Controls.Add(td15);
        th1.Controls.Add(td16);
        th1.Controls.Add(td17);
        th1.Controls.Add(td18);
        th1.Controls.Add(td19);
        thead.Controls.Add(th1);
        table.Controls.Add(thead);

        HtmlGenericControl th2 = new HtmlGenericControl("tr");
        th2.Attributes["class"] = "pure-table-odd";
        th2.Attributes["style"] = "line-height:1;";
        if (changeButton)
        {
            HtmlGenericControl td20 = new HtmlGenericControl("td");
            th2.Controls.Add(td20);
        }
        HtmlGenericControl td21 = new HtmlGenericControl("td");
        HtmlGenericControl td22 = new HtmlGenericControl("td");
        HtmlGenericControl td23 = new HtmlGenericControl("td");
        HtmlGenericControl td24 = new HtmlGenericControl("td");
        td24.InnerText = "Post held permanently";
        HtmlGenericControl td25 = new HtmlGenericControl("td");
        td25.InnerText = "Pay in the permanent post";
        HtmlGenericControl td26 = new HtmlGenericControl("td");
        td26.InnerText = "Officiating Pay";
        HtmlGenericControl td27 = new HtmlGenericControl("td");
        td27.InnerText = "Other emoluments falling under the term 'Pay'";
        HtmlGenericControl td28 = new HtmlGenericControl("td");
        td28.InnerText = "From";
        HtmlGenericControl td29 = new HtmlGenericControl("td");
        td29.InnerText = "To";
        HtmlGenericControl td30 = new HtmlGenericControl("td");
        HtmlGenericControl td31 = new HtmlGenericControl("td");
        HtmlGenericControl td32 = new HtmlGenericControl("td");
        HtmlGenericControl td33 = new HtmlGenericControl("td");
        th2.Controls.Add(td21);
        th2.Controls.Add(td22);
        th2.Controls.Add(td23);
        th2.Controls.Add(td24);
        th2.Controls.Add(td25);
        th2.Controls.Add(td26);
        th2.Controls.Add(td27);
        th2.Controls.Add(td28);
        th2.Controls.Add(td29);
        th2.Controls.Add(td30);
        th2.Controls.Add(td31);
        th2.Controls.Add(td32);
        th2.Controls.Add(td33);
        tbody.Controls.Add(th2);

        for (int i = 0; i < serviceRegisterList.Count(); i++)
        {
            HtmlGenericControl tr = new HtmlGenericControl("tr");
            tr.Attributes["class"] = (i % 2 == 0) ? "pure-table-even" : "pure-table-odd";

            if (changeButton)
            {
                HtmlGenericControl td0 = new HtmlGenericControl("td");
                td0.InnerHtml = "<a class=\"pure-button pure-button-primary\" href=\"?" + url + "&ChangeFieldName=" + i + "\">Change</a>";
                tr.Controls.Add(td0);
            }
            HtmlGenericControl td1 = new HtmlGenericControl("td");
            td1.InnerText = serviceRegisterList[i].PostAndPayDescription;
            HtmlGenericControl td2 = new HtmlGenericControl("td");
            td2.InnerText = serviceRegisterList[i].PermanentOrTemporary;
            HtmlGenericControl td3 = new HtmlGenericControl("td");
            td3.InnerText = serviceRegisterList[i].Incumbent;
            HtmlGenericControl td4 = new HtmlGenericControl("td");
            td4.InnerText = serviceRegisterList[i].PostHeldPermanently;
            HtmlGenericControl td5 = new HtmlGenericControl("td");
            td5.InnerText = serviceRegisterList[i].PayInPermanentPost.ToString();
            HtmlGenericControl td6 = new HtmlGenericControl("td");
            td6.InnerText = serviceRegisterList[i].OfficiatingPay.ToString();
            HtmlGenericControl td7 = new HtmlGenericControl("td");
            td7.InnerText = serviceRegisterList[i].OtherPay.ToString();
            HtmlGenericControl td8 = new HtmlGenericControl("td");
            td8.InnerText = serviceRegisterList[i].FromPeriod.ToString();
            HtmlGenericControl td9 = new HtmlGenericControl("td");
            td9.InnerText = serviceRegisterList[i].ToPeriod.ToString();
            HtmlGenericControl td10 = new HtmlGenericControl("td");
            td10.InnerText = serviceRegisterList[i].Events1to8;
            HtmlGenericControl td111 = new HtmlGenericControl("td");
            td111.InnerText = serviceRegisterList[i].LeaveDescription;
            HtmlGenericControl td112 = new HtmlGenericControl("td");
            td112.InnerText = serviceRegisterList[i].PunishmentReference;
            HtmlGenericControl td113 = new HtmlGenericControl("td");
            td113.InnerText = serviceRegisterList[i].Remarks;

            tr.Controls.Add(td1);
            tr.Controls.Add(td2);
            tr.Controls.Add(td3);
            tr.Controls.Add(td4);
            tr.Controls.Add(td5);
            tr.Controls.Add(td6);
            tr.Controls.Add(td7);
            tr.Controls.Add(td8);
            tr.Controls.Add(td9);
            tr.Controls.Add(td10);
            tr.Controls.Add(td111);
            tr.Controls.Add(td112);
            tr.Controls.Add(td113);

            tbody.Controls.Add(tr);
        }
        table.Controls.Add(tbody);
        return table;
    }

    public HtmlGenericControl GenerateLtcRecord(List<LtcRecord> ltcRecordList, bool changeButton, string url)
    {
        HtmlGenericControl table = new HtmlGenericControl("table");
        table.Attributes["class"] = "pure-table";

        HtmlGenericControl tbody = new HtmlGenericControl("tbody");

        int size = ltcRecordList.Count();

        HtmlGenericControl th1 = new HtmlGenericControl("tr");
        th1.Attributes["class"] = "pure-table-odd";
        th1.Attributes["style"] = "line-height:2;";
        HtmlGenericControl td11 = new HtmlGenericControl("td");
        HtmlGenericControl td12 = new HtmlGenericControl("td");
        td12.Attributes["colspan"] = "2"; td12.InnerText = "Leave dates";
        HtmlGenericControl td13 = new HtmlGenericControl("td");
        td13.InnerText = "Name of person for";
        HtmlGenericControl td14 = new HtmlGenericControl("td");
        td14.InnerText = "Relation of person for";
        HtmlGenericControl td15 = new HtmlGenericControl("td");
        HtmlGenericControl td16 = new HtmlGenericControl("td");
        HtmlGenericControl td17 = new HtmlGenericControl("td");
        HtmlGenericControl td18 = new HtmlGenericControl("td");
        th1.Controls.Add(td11);
        th1.Controls.Add(td12);
        th1.Controls.Add(td13);
        th1.Controls.Add(td14);
        th1.Controls.Add(td15);
        th1.Controls.Add(td16);
        th1.Controls.Add(td17);
        th1.Controls.Add(td18);
        tbody.Controls.Add(th1);

        HtmlGenericControl th2 = new HtmlGenericControl("tr");
        th2.Attributes["class"] = "pure-table-odd";
        th2.Attributes["style"] = "line-height:1;";
        HtmlGenericControl td21 = new HtmlGenericControl("td");
        td21.InnerText = "Block Year";
        HtmlGenericControl td22 = new HtmlGenericControl("td");
        td22.InnerText = "From";
        HtmlGenericControl td23 = new HtmlGenericControl("td");
        td23.InnerText = "To";
        HtmlGenericControl td24 = new HtmlGenericControl("td");
        td24.InnerText = "whom availed of";
        HtmlGenericControl td25 = new HtmlGenericControl("td");
        td25.InnerText = "whom availed of";
        HtmlGenericControl td26 = new HtmlGenericControl("td");
        td26.InnerText = "Home town";
        HtmlGenericControl td27 = new HtmlGenericControl("td");
        td27.InnerText = "Other places";
        HtmlGenericControl td28 = new HtmlGenericControl("td");
        td28.InnerText = "Amount Paid";
        HtmlGenericControl td29 = new HtmlGenericControl("td");
        td29.InnerText = "Certifying Officer";
        if (changeButton)
        {
            HtmlGenericControl td30 = new HtmlGenericControl("td");
            td30.InnerHtml = "";
            th2.Controls.Add(td30);
        }
        th2.Controls.Add(td21);
        th2.Controls.Add(td22);
        th2.Controls.Add(td23);
        th2.Controls.Add(td24);
        th2.Controls.Add(td25);
        th2.Controls.Add(td26);
        th2.Controls.Add(td27);
        th2.Controls.Add(td28);
        th2.Controls.Add(td29);


        tbody.Controls.Add(th2);

        for (int i = 0; i < size; i++)
        {
            HtmlGenericControl tr = new HtmlGenericControl("tr");
            tr.Attributes["class"] = (i % 2 == 0) ? "pure-table-even" : "pure-table-odd";
            HtmlGenericControl td1 = new HtmlGenericControl("td");
            td1.InnerText = ltcRecordList[i].BlockYear.ToString();
            HtmlGenericControl td2 = new HtmlGenericControl("td");
            td2.InnerText = ltcRecordList[i].FromDate.ToString();
            HtmlGenericControl td3 = new HtmlGenericControl("td");
            td3.InnerText = ltcRecordList[i].ToDate.ToString();
            HtmlGenericControl td4 = new HtmlGenericControl("td");
            td4.InnerText = ltcRecordList[i].RelativeName;
            HtmlGenericControl td5 = new HtmlGenericControl("td");
            td5.InnerText = ltcRecordList[i].Relation;
            HtmlGenericControl td6 = new HtmlGenericControl("td");
            td6.InnerText = ltcRecordList[i].HomeTown;
            HtmlGenericControl td7 = new HtmlGenericControl("td");
            td7.InnerText = ltcRecordList[i].OtherPlaces;
            HtmlGenericControl td8 = new HtmlGenericControl("td");
            td8.InnerText = ltcRecordList[i].AmountPaid.ToString();
            HtmlGenericControl td9 = new HtmlGenericControl("td");
            td9.InnerText = ltcRecordList[i].CertifyingOfficer;
            if (changeButton)
            {
                HtmlGenericControl td30 = new HtmlGenericControl("td");
                td30.InnerHtml = " <a class=\"pure-button pure-button-primary\" href=\"" + "?" + url + "&ChangeFieldName=" + i + "\">Change</a>";
                tr.Controls.Add(td30);
            }

            tr.Controls.Add(td1);
            tr.Controls.Add(td2);
            tr.Controls.Add(td3);
            tr.Controls.Add(td4);
            tr.Controls.Add(td5);
            tr.Controls.Add(td6);
            tr.Controls.Add(td7);
            tr.Controls.Add(td8);
            tr.Controls.Add(td9);

            tbody.Controls.Add(tr);
        }
        table.Controls.Add(tbody);
        return table;
    }

    private HtmlGenericControl GenerateLtcDeclaration(LtcDeclaration ltcDeclaration)
    {
        HtmlGenericControl div = new HtmlGenericControl("div");

        HtmlGenericControl head1 = new HtmlGenericControl("p");
        head1.InnerText = "Declared the following";

        HtmlGenericControl table = new HtmlGenericControl("table");
        table.Attributes["class"] = "pure-table pure-table-horizontal";

        HtmlGenericControl tbody = new HtmlGenericControl("tbody");

        HtmlGenericControl tr;
        HtmlGenericControl td1, td2;

        tr = new HtmlGenericControl("tr");
        tr.Attributes["class"] = "pure-table-odd";
        td1 = new HtmlGenericControl("td");
        td1.InnerText = "Hometown";
        td2 = new HtmlGenericControl("td");
        td2.InnerText = ltcDeclaration.HomeTown;
        tr.Controls.Add(td1);
        tr.Controls.Add(td2);
        tbody.Controls.Add(tr);

        tr = new HtmlGenericControl("tr");
        tr.Attributes["class"] = "pure-table-even";
        td1 = new HtmlGenericControl("td");
        td1.InnerText = "Taluka";
        td2 = new HtmlGenericControl("td");
        td2.InnerText = ltcDeclaration.Taluka;
        tr.Controls.Add(td1);
        tr.Controls.Add(td2);
        tbody.Controls.Add(tr);

        tr = new HtmlGenericControl("tr");
        tr.Attributes["class"] = "pure-table-odd";
        td1 = new HtmlGenericControl("td");
        td1.InnerText = "District";
        td2 = new HtmlGenericControl("td");
        td2.InnerText = ltcDeclaration.District;
        tr.Controls.Add(td1);
        tr.Controls.Add(td2);
        tbody.Controls.Add(tr);

        tr = new HtmlGenericControl("tr");
        tr.Attributes["class"] = "pure-table-even";
        td1 = new HtmlGenericControl("td");
        td1.InnerText = "State";
        td2 = new HtmlGenericControl("td");
        td2.InnerText = ltcDeclaration.State;
        tr.Controls.Add(td1);
        tr.Controls.Add(td2);
        tbody.Controls.Add(tr);

        tr = new HtmlGenericControl("tr");
        tr.Attributes["class"] = "pure-table-odd";
        td1 = new HtmlGenericControl("td");
        td1.InnerText = "Nearest Railway Station";
        td2 = new HtmlGenericControl("td");
        td2.InnerText = ltcDeclaration.NearestRlyStation;
        tr.Controls.Add(td1);
        tr.Controls.Add(td2);
        tbody.Controls.Add(tr);

        table.Controls.Add(tbody);
        div.Controls.Add(head1);
        div.Controls.Add(table);

        HtmlGenericControl b = new HtmlGenericControl("br");
        div.Controls.Add(b);
        div.Controls.Add(b);

        div.Controls.Add(b);
        div.Controls.Add(b);

        head1 = new HtmlGenericControl("p");
        head1.InnerText = "Reasons for declaration of " + ltcDeclaration.HomeTown + " are as under:";
        div.Controls.Add(head1);

        HtmlGenericControl div2 = new HtmlGenericControl("div");
        div2.Attributes["style"] = "margin-left:10px;";
        head1 = new HtmlGenericControl("p");
        head1.InnerText = ltcDeclaration.ReasonOneForDeclaration;
        div2.Controls.Add(head1);
        head1 = new HtmlGenericControl("p");
        head1.InnerText = ltcDeclaration.ReasonTwoForDeclaration;
        div2.Controls.Add(head1);
        div.Controls.Add(div2);

        div.Controls.Add(b);
        div.Controls.Add(b);
        return div;
    }

    public HtmlGenericControl GenerateLtcDependents(List<LtcDependents> ltcDependentsList, bool changeButton, string url)
    {
        int size = ltcDependentsList.Count;

        HtmlGenericControl table = new HtmlGenericControl("table");
        table.Attributes["class"] = "pure-table pure-table-horizontal";

        HtmlGenericControl tbody = new HtmlGenericControl("tbody");

        HtmlGenericControl tr = new HtmlGenericControl("tr");
        tr.Attributes["class"] = "pure-table-odd";
        HtmlGenericControl td1 = new HtmlGenericControl("td");
        td1.InnerText = "Sr. No.";
        HtmlGenericControl td2 = new HtmlGenericControl("td");
        td2.InnerText = "Name";
        HtmlGenericControl td3 = new HtmlGenericControl("td");
        td3.InnerText = "Relationship";
        HtmlGenericControl td4 = new HtmlGenericControl("td");
        td4.InnerText = "DOB";
        HtmlGenericControl td5 = new HtmlGenericControl("td");
        td5.InnerText = "Employment Details";
        HtmlGenericControl td6 = new HtmlGenericControl("td");
        td6.InnerText = "Income from all Sources";
        if (changeButton)
        {
            HtmlGenericControl td0 = new HtmlGenericControl("td");
            tr.Controls.Add(td0);
        }
        tr.Controls.Add(td1);
        tr.Controls.Add(td2);
        tr.Controls.Add(td3);
        tr.Controls.Add(td4);
        tr.Controls.Add(td5);
        tr.Controls.Add(td6);

        tbody.Controls.Add(tr);

        for (int i = 0; i < size; i++)
        {
            tr = new HtmlGenericControl("tr");
            tr.Attributes["class"] = "pure-table-even";
            td1 = new HtmlGenericControl("td");
            td1.InnerText = (i+1).ToString();
            td2 = new HtmlGenericControl("td");
            td2.InnerText = ltcDependentsList[i].Name;
            td3 = new HtmlGenericControl("td");
            td3.InnerText = ltcDependentsList[i].Relationship;
            td4 = new HtmlGenericControl("td");
            td4.InnerText = ltcDependentsList[i].Dob.ToString();
            td5 = new HtmlGenericControl("td");
            td5.InnerText = ltcDependentsList[i].EmploymentDetails;
            td6 = new HtmlGenericControl("td");
            td6.InnerText = ltcDependentsList[i].TotalIncome.ToString();
            if (changeButton)
            {
                HtmlGenericControl td30 = new HtmlGenericControl("td");
                td30.InnerHtml = " <a class=\"pure-button pure-button-primary\" href=\"" + "?" + url + "&ChangeFieldName=" + i + "\">Change</a>";
                tr.Controls.Add(td30);
            }
            tr.Controls.Add(td1);
            tr.Controls.Add(td2);
            tr.Controls.Add(td3);
            tr.Controls.Add(td4);
            tr.Controls.Add(td5);
            tr.Controls.Add(td6);

            tbody.Controls.Add(tr);
        }
        table.Controls.Add(tbody);
        return table;
    }

    protected void setValues()
    {
        headerRibbonUsername.InnerText = AUserSession.Current.ThisUser.UserName;
        //headerRibbonUsernameBoxName.InnerText = AUserSession.Current.ThisUser.Name;
        //headerRibbonUsernameBoxPost.InnerText = AUserSession.Current.ThisUser.Designation;
        //headerRibbonUsernameBoxImage.ImageUrl = "images/" + AUserSession.Current.ThisUser.UserName.ToLower() + ".jpg";
        profileNavImage.ImageUrl = "images/" + AUserSession.Current.ThisUser.UserName.ToLower() + ".jpg";

        profile_nav_placeHolder.Controls.Clear();
        try
        {
            HtmlGenericControl table = GenerateBiodata(serviceBookFuncs.biodata);
            profile_nav_placeHolder.Controls.Add(table);
        }
        catch (Exception ex)
        {
            HtmlGenericControl p = new HtmlGenericControl("p");
            p.InnerText = ex.Message;
            profile_nav_placeHolder.Controls.Add(p);
        }
    }

    protected void LogoutButton_Click(object sender, EventArgs e)
    {
        FormsAuthentication.SignOut();
        AUserSession.Current.clearSession();
        string cookie_userName = "ffpdf.userId", cookie_pwd = "ffpdf.pwd";
        HttpCookie c1 = new HttpCookie(cookie_userName);
        HttpCookie c2 = new HttpCookie(cookie_pwd);

        c1.Expires = c2.Expires = DateTime.Now.AddDays(-1d);
        Response.Cookies.Add(c1);
        Response.Cookies.Add(c2);

        Response.Redirect("Login.aspx");
    }

    protected void HeaderRibbonUsername_Click(object sender, EventArgs e)
    {
        if (headerRibbonUsernameBox.Attributes["style"] == "display:none;")
        {
            headerRibbonUsernameBox.Attributes["style"] = "display:block;";
            headerRibbonUsername.Attributes["style"] = "color:rgb(64, 64, 64);text:bold;";
            headerRibbonUsernameDiv.Attributes["style"] = "background:white;color:white;";
        }
        else
        {
            headerRibbonUsernameBox.Attributes["style"] = "display:none;";
            headerRibbonUsername.Attributes["style"] = "color:white;text:bold;";
            headerRibbonUsernameDiv.Attributes["style"] = "background:#1b98f8;color:white;";
        }
    }

}