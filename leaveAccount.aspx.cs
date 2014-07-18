using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;


public partial class leaveAccount : System.Web.UI.Page
{
    public String PopupMsg;
    public UserFuncs userFuncs;
    public ServiceBookFuncs serviceBookFuncs;
    public HtmlGenericControl leaveAccount_inner_div;


    protected void Page_Load(object sender, EventArgs e)
    {
        userFuncs = new UserFuncs();
        userFuncs.loadUserBox("outbox");
        serviceBookFuncs = new ServiceBookFuncs();
        serviceBookFuncs.fetchLeaveBalance();
        OpenSubTab(leaveBalanceTab);
        GenerateBalanceTable();
        leave_inner_placeholder.Controls.Add(leaveAccount_inner_div);
    }

    protected void LeaveBalance_Click(object sender, EventArgs e)
    {
        leave_inner_placeholder.Controls.Clear();
        OpenSubTab(leaveBalanceTab);

        leave_inner_placeholder.Controls.Add(leaveAccount_inner_div);

    }

    protected void GenerateBalanceTable()
    {
        leaveAccount_inner_div = new HtmlGenericControl("div");
        try
        {
            HtmlGenericControl table = new HtmlGenericControl("table");
            table.Attributes["id"] = "leaveBalanceTable";
            table.Attributes["class"] = "pure-table pure-table-horizontal";
            table.Attributes["style"] = "margin-top: 2%;margin-left: 2%;";

            HtmlGenericControl tbody = new HtmlGenericControl("tbody");
            HtmlGenericControl td;

            HtmlGenericControl tr = new HtmlGenericControl("tr");
            tr.Attributes["class"] = "pure-table-odd";

            for (int i = 0; i < serviceBookFuncs.balance.Count; i++)
            {
                td = new HtmlGenericControl("td");
                string key = serviceBookFuncs.balance[i].Key;
                td.InnerText = key;
                tr.Controls.Add(td);
            }
            tbody.Controls.Add(tr);
            tr = new HtmlGenericControl("tr");
            tr.Attributes["class"] = "pure-table-even";
            for (int i = 0; i < serviceBookFuncs.balance.Count; i++)
            {
                string value = serviceBookFuncs.balance[i].Value.ToString();

                td = new HtmlGenericControl("td");
                td.InnerText = value;
                tr.Controls.Add(td);
            }
            tbody.Controls.Add(tr);
            table.Controls.Add(tbody);
            leaveAccount_inner_div.Controls.Add(table);
            leave_inner_placeholder.Controls.Clear();
            leave_inner_placeholder.Controls.Add(leaveAccount_inner_div);
        }
        catch (Exception ex)
        {
            AUserSession.Current.Warning = ex.Message;
        }
    }

    protected void LeaveHistory_Click(object sender, EventArgs e)
    {
        OpenSubTab(leaveHistoryTab);
        leave_inner_placeholder.Controls.Clear();
        GenerateLeaveHistory();
        leave_inner_placeholder.Controls.Add(leaveAccount_inner_div);
    }

    protected void GenerateLeaveHistory()
    {
        leaveAccount_inner_div = new HtmlGenericControl("div");

        HtmlGenericControl table = new HtmlGenericControl("table");
        table.Attributes["id"] = "EarnedLeaveHistory";
        table.Attributes["class"] = "pure-table";
        table.Attributes["style"] = "margin-top: 2%;margin-left: 2%;";

        HtmlGenericControl thead = new HtmlGenericControl("thead");
        HtmlGenericControl tr = new HtmlGenericControl("tr");
        HtmlGenericControl td1 = new HtmlGenericControl("td");
        td1.InnerText = "From";
        HtmlGenericControl td2 = new HtmlGenericControl("td");
        td2.InnerText = "To";
        HtmlGenericControl td3 = new HtmlGenericControl("td");
        td3.InnerText = "Duration";
        HtmlGenericControl td4 = new HtmlGenericControl("td");
        td4.InnerText = "Recommending Authority";
        HtmlGenericControl td5 = new HtmlGenericControl("td");
        td5.InnerText = "Approving Authority";
        HtmlGenericControl td6 = new HtmlGenericControl("td");
        td6.InnerText = "Travel Concession Taken";
        tr.Controls.Add(td1);
        tr.Controls.Add(td2);
        tr.Controls.Add(td3);
        tr.Controls.Add(td4);
        tr.Controls.Add(td5);
        tr.Controls.Add(td6);
        thead.Controls.Add(tr);
        table.Controls.Add(thead);

        HtmlGenericControl tbody = new HtmlGenericControl("tbody");

        for (int i = 0; i < userFuncs.boxContent.Count && userFuncs.boxContent[i].ApplicationType == 0; i++)
        {
            if (userFuncs.boxContent[i].ApplicationType == 0)
            {
                tr = new HtmlGenericControl("tr");
                td1 = new HtmlGenericControl("td");
                td1.InnerText = userFuncs.boxContent[i].Date.ToString("dd-MMM-yyyy");
                td2 = new HtmlGenericControl("td");
                DateTime to = userFuncs.boxContent[i].Date;
                to.AddDays(userFuncs.boxContent[i].LeaveDuration);
                td2.InnerText = to.ToString("dd-MMM-yyyy");
                td3 = new HtmlGenericControl("td");
                td3.InnerText = userFuncs.boxContent[i].LeaveDuration.ToString();
                td4 = new HtmlGenericControl("td");
                td4.InnerText = userFuncs.boxContent[i].RecommAuthId.ToString();
                td5 = new HtmlGenericControl("td");
                td5.InnerText = userFuncs.boxContent[i].ApprovAuthId.ToString();
                td6 = new HtmlGenericControl("td");
                if (userFuncs.boxContent[i].AvailConcession == 0)
                    td6.InnerText = "No";
                else
                    td6.InnerText = "Yes";
                tr.Controls.Add(td1);
                tr.Controls.Add(td2);
                tr.Controls.Add(td3);
                tr.Controls.Add(td4);
                tr.Controls.Add(td5);
                tr.Controls.Add(td6);
                tbody.Controls.Add(tr);
            }
        }
        table.Controls.Add(tbody);
        leaveAccount_inner_div.Controls.Add(table);
        
    }

    protected void HalfPayHistory_Click(object sender, EventArgs e)
    {
        leave_inner_placeholder.Controls.Clear();
        OpenSubTab(halfPayHistoryTab);
        GenerateHalfPayHistory();
        leave_inner_placeholder.Controls.Add(leaveAccount_inner_div);
    }

    protected void GenerateHalfPayHistory()
    {
        leaveAccount_inner_div = new HtmlGenericControl("div");

        HtmlGenericControl h1 = new HtmlGenericControl("h2");
        h1.InnerText = "Half Pay Leaves Taken";
        h1.Attributes["style"] = "float:left padding-left: 20px;";
        HtmlGenericControl table1 = new HtmlGenericControl("table");
        table1.Attributes["id"] = "HalfPayLeaveHistory";
        table1.Attributes["class"] = "pure-table";
        table1.Attributes["style"] = "font-size: 0.8em";

        HtmlGenericControl thead = new HtmlGenericControl("thead");
        HtmlGenericControl tr = new HtmlGenericControl("tr");
        HtmlGenericControl td1 = new HtmlGenericControl("td");
        td1.InnerText = "From";
        HtmlGenericControl td2 = new HtmlGenericControl("td");
        td2.InnerText = "To";
        HtmlGenericControl td3 = new HtmlGenericControl("td");
        td3.InnerText = "Duration";
        HtmlGenericControl td4 = new HtmlGenericControl("td");
        td4.InnerText = "Recommending Authority";
        HtmlGenericControl td5 = new HtmlGenericControl("td");
        td5.InnerText = "Approving Authority";
        HtmlGenericControl td6 = new HtmlGenericControl("td");
        td6.InnerText = "Travel Concession Taken";
        HtmlGenericControl td7 = new HtmlGenericControl("td");
        td7.InnerText = "Commuted";
        tr.Controls.Add(td1);
        tr.Controls.Add(td2);
        tr.Controls.Add(td3);
        tr.Controls.Add(td4);
        tr.Controls.Add(td5);
        tr.Controls.Add(td6);
        tr.Controls.Add(td7);
        thead.Controls.Add(tr);
        table1.Controls.Add(thead);

        HtmlGenericControl tbody = new HtmlGenericControl("tbody");

        for (int i = 0; i < userFuncs.boxContent.Count; i++) //applicationType of half pay leaves
        {
            if (userFuncs.boxContent[i].ApplicationType == 0)
            {

                tr = new HtmlGenericControl("tr");
                td1 = new HtmlGenericControl("td");
                td1.InnerText = userFuncs.boxContent[i].Date.ToString("dd-MMM-yyyy");
                td2 = new HtmlGenericControl("td");
                DateTime to = userFuncs.boxContent[i].Date;
                to.AddDays(userFuncs.boxContent[i].LeaveDuration);
                td2.InnerText = to.ToString("dd-MMM-yyyy");
                td3 = new HtmlGenericControl("td");
                td3.InnerText = userFuncs.boxContent[i].LeaveDuration.ToString();
                td4 = new HtmlGenericControl("td");
                td4.InnerText = userFuncs.boxContent[i].RecommAuthId.ToString();
                td5 = new HtmlGenericControl("td");
                td5.InnerText = userFuncs.boxContent[i].ApprovAuthId.ToString();
                td6 = new HtmlGenericControl("td");
                if (userFuncs.boxContent[i].AvailConcession == 0)
                    td6.InnerText = "No";
                else
                    td6.InnerText = "Yes";
                td7 = new HtmlGenericControl("td");
                if (userFuncs.boxContent[i].Commuted == 0)
                    td7.InnerText = "No";
                else
                    td7.InnerText = "Yes";
                tr.Controls.Add(td1);
                tr.Controls.Add(td2);
                tr.Controls.Add(td3);
                tr.Controls.Add(td4);
                tr.Controls.Add(td5);
                tr.Controls.Add(td6);
                tr.Controls.Add(td7);
                tbody.Controls.Add(tr);
            }
        }
        table1.Controls.Add(tbody);

        // Second Table
        HtmlGenericControl h2 = new HtmlGenericControl("h2");
        h2.InnerText = "Leave Not Due Leaves Taken";
        h2.Attributes["style"] = "float:left; padding-left:20px;";

        HtmlGenericControl table2 = new HtmlGenericControl("table");
        table2.Attributes["id"] = "LeaveNotDueLeaveHistory";
        table2.Attributes["class"] = "pure-table";
        table2.Attributes["style"] = "width:100%;font-size: 0.8em";
        thead = new HtmlGenericControl("thead");
        tr = new HtmlGenericControl("tr");
        td1 = new HtmlGenericControl("td");
        td1.InnerText = "From";
        td2 = new HtmlGenericControl("td");
        td2.InnerText = "To";
        td3 = new HtmlGenericControl("td");
        td3.InnerText = "Duration";
        td4 = new HtmlGenericControl("td");
        td4.InnerText = "Recommending Authority";
        td5 = new HtmlGenericControl("td");
        td5.InnerText = "Approving Authority";
        td6 = new HtmlGenericControl("td");
        td6.InnerText = "Travel Concession Taken";
        tr.Controls.Add(td1);
        tr.Controls.Add(td2);
        tr.Controls.Add(td3);
        tr.Controls.Add(td4);
        tr.Controls.Add(td5);
        tr.Controls.Add(td6);
        thead.Controls.Add(tr);
        table2.Controls.Add(thead);

        tbody = new HtmlGenericControl("tbody");

        for (int i = 0; i < userFuncs.boxContent.Count; i++) //applicationType of leave Not Due leaves
        {
            if (userFuncs.boxContent[i].ApplicationType == 0)
            {
                tr = new HtmlGenericControl("tr");
                td1 = new HtmlGenericControl("td");
                td1.InnerText = userFuncs.boxContent[i].Date.ToString("dd-MMM-yyyy");
                td2 = new HtmlGenericControl("td");
                DateTime to = userFuncs.boxContent[i].Date;
                to.AddDays(userFuncs.boxContent[i].LeaveDuration);
                td2.InnerText = to.ToString("dd-MMM-yyyy");
                td3 = new HtmlGenericControl("td");
                td3.InnerText = userFuncs.boxContent[i].LeaveDuration.ToString();
                td4 = new HtmlGenericControl("td");
                td4.InnerText = userFuncs.boxContent[i].RecommAuthId.ToString();
                td5 = new HtmlGenericControl("td");
                td5.InnerText = userFuncs.boxContent[i].ApprovAuthId.ToString();
                td6 = new HtmlGenericControl("td");
                if (userFuncs.boxContent[i].AvailConcession == 0)
                    td6.InnerText = "No";
                else
                    td6.InnerText = "Yes";
                tr.Controls.Add(td1);
                tr.Controls.Add(td2);
                tr.Controls.Add(td3);
                tr.Controls.Add(td4);
                tr.Controls.Add(td5);
                tr.Controls.Add(td6);
                tbody.Controls.Add(tr);
            }
        }
        table2.Controls.Add(tbody);
        leaveAccount_inner_div.Controls.Add(h1);
        leaveAccount_inner_div.Controls.Add(table1);
        leaveAccount_inner_div.Controls.Add(h2);
        leaveAccount_inner_div.Controls.Add(table2);
        
    }
    protected void OpenSubTab(HtmlGenericControl id)
    {
        // open this tab and close all others
        HtmlGenericControl[] tabs = { leaveBalanceTab, leaveHistoryTab, halfPayHistoryTab };
        int size = tabs.Count();
        for (int i = 0; i < size;i++)
        {
            tabs[i].Attributes["class"] = "gmail-tabs-unselected";
        }
        id.Attributes["class"] = "gmail-tabs-selected";
    }
}
