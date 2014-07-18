using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.Security;

namespace WebApplication2
{
    public partial class Profile : System.Web.UI.Page
    {
        public String PopupMsg;
        private UserFuncs userFuncs;
        private ServiceBookFuncs serviceBookFuncs;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!AUserSession.Current.isUserLoggedIn()) {
                LogoutButton_Click(sender, e);
            }
            setValues();
            userFuncs = new UserFuncs();
            serviceBookFuncs = new ServiceBookFuncs();
            serviceBookFuncs.fetchData();
            PopupMsg = AUserSession.Current.Warning;
        }

        protected void Profile_Click(object sender, EventArgs e)
        {
            service_book_container_placeholder.Controls.Clear();
            Biodata biodata = new Biodata();
            OpenTab(profileTab);
        }

        protected void LtcRecord_Click(object sender, EventArgs e)
        {
            service_book_container_placeholder.Controls.Clear();
            OpenTab(ltcRecordTab);
            HtmlGenericControl title = new HtmlGenericControl("h1");
            title.InnerText = "LTC RECORD";
            service_book_container_placeholder.Controls.Add(title);
            List<LtcRecord> ltcRecordList = serviceBookFuncs.ltcRecord;
            try
            {
                HtmlGenericControl table = GenerateLtcRecord(ltcRecordList);
                service_book_container_placeholder.Controls.Add(table);
            }
            catch (Exception ex)
            {
                HtmlGenericControl p = new HtmlGenericControl("p");
                p.InnerText = ex.Message;// "No record found";
                service_book_container_placeholder.Controls.Add(p);
                AUserSession.Current.Warning = ex.Message;
            }
        }

        protected void LeaveAccount_Click(object sender, EventArgs e)
        {
            service_book_container_placeholder.Controls.Clear();
            Biodata biodata = new Biodata();

            OpenTab(leaveAccountTab);
        }

        protected void LtcDeclaration_Click(object sender, EventArgs e)
        {
            service_book_container_placeholder.Controls.Clear();
            Biodata biodata = new Biodata();

            OpenTab(ltcDeclarationTab);
        }

        protected void ServiceRegister_Click(object sender, EventArgs e)
        {
            service_book_container_placeholder.Controls.Clear();
            Biodata biodata = new Biodata();

            OpenTab(serviceRegisterTab);
        }

        protected void OpenTab(HtmlGenericControl id) {
            // open this tab and close all others
            HtmlGenericControl[] tabs = { profileTab, leaveAccountTab, ltcRecordTab, ltcDeclarationTab, serviceRegisterTab };
            int size = tabs.Count();
            for (int i = 0; i < size; i++) {
                tabs[i].Attributes["class"] = "gmail-tabs-unselected";
            }
            id.Attributes["class"] = "gmail-tabs-selected";
        }

        protected void GenerateBiodata(Biodata biodata)
        {
            HtmlGenericControl table = new HtmlGenericControl("table");
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
           
        }

        private HtmlGenericControl GenerateLtcRecord(List<LtcRecord> ltcRecordList)
        {
            HtmlGenericControl table = new HtmlGenericControl("table");
            table.Attributes["class"] = "pure-table pure-table-horizontal";

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

        protected void setValues() {
            headerRibbonUsername.InnerText = AUserSession.Current.ThisUser.UserName;
            headerRibbonUsernameBoxName.InnerText = AUserSession.Current.ThisUser.Name;
            headerRibbonUsernameBoxPost.InnerText = AUserSession.Current.ThisUser.Designation;
            headerRibbonUsernameBoxImage.ImageUrl = "images/" + AUserSession.Current.ThisUser.UserName.ToLower() + ".jpg";
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
}