using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;


public partial class AllUsers : System.Web.UI.Page
{
    DataRow row;
    HtmlGenericControl table, tbody, thead, tr,td1, td2, td3, td4, td5;

    protected void Page_Load(object sender, EventArgs e)
    {
        Database.connectToDatabse();
        string query = "SELECT * FROM ProfileInformation";
        Database.executeQuery(query);
        DataTable dtable = Database.getDataTable();
        Database.disconnectToDatabase();

        GenerateAllUserTable(dtable);
        uTable.Controls.Add(table);
    }

    protected void GenerateAllUserTable(DataTable dtable)
    {
        table = new HtmlGenericControl("table");
        table.Attributes["class"] = "pure-table-horizontal";
        table.Attributes["style"] = "width:80%;margin-top:60px;line-height:2";
        table.Attributes["id"] = "userTable";
        tbody = new HtmlGenericControl("tbody");
        thead = new HtmlGenericControl("thead");
        tr = new HtmlGenericControl("tr");
        td1 = new HtmlGenericControl("td");
        td2 = new HtmlGenericControl("td");
        td3 = new HtmlGenericControl("td");
        td4 = new HtmlGenericControl("td");
        td5 = new HtmlGenericControl("td");
        td1.InnerText = "User ID";
        td2.InnerText = "Employ ID";
        td3.InnerText = "Name";
        td4.InnerText = "Designation";
        td5.InnerText = "Department";
        tr.Controls.Add(td1);
        tr.Controls.Add(td2);
        tr.Controls.Add(td3);
        tr.Controls.Add(td4);
        tr.Controls.Add(td5);
        thead.Controls.Add(tr);
        tbody.Controls.Add(thead);

       
        for (int i = 0; i < dtable.Rows.Count; i++)
        {
            row = dtable.Rows[i];
            tr = new HtmlGenericControl("tr");
            td1 = new HtmlGenericControl("td");
            td2 = new HtmlGenericControl("td");
            td3 = new HtmlGenericControl("td");
            td4 = new HtmlGenericControl("td");
            td5 = new HtmlGenericControl("td");
            if (i % 2 == 0)
                tr.Attributes["class"] = "pure-table-even";
            else
                tr.Attributes["class"] = "pure-table-odd";

            td1.InnerText = dtable.Rows[i].Field<int>("userID").ToString();
            td2.InnerText = dtable.Rows[i].Field<string>("empID");
            td3.InnerText = dtable.Rows[i].Field<string>("Name");
            td4.InnerText = dtable.Rows[i].Field<string>("designation");
            td5.InnerText = dtable.Rows[i].Field<string>("department");
            tr.Controls.Add(td1);
            tr.Controls.Add(td2);
            tr.Controls.Add(td3);
            tr.Controls.Add(td4);
            tr.Controls.Add(td5);
            tbody.Controls.Add(tr);
        }
        table.Controls.Add(tbody);
    }

    protected void searchUser(object sender , EventArgs e)
    {
        string keyValue = Regex.Replace(SearchBar.Text , "[^A-Za-z0-9$]", "");
        string query = "SELECT * FROM ProfileInformation WHERE Name LIKE '%" + keyValue + "%'";
        Database.connectToDatabse();
        Database.executeQuery(query);

        DataTable dtable = Database.getDataTable();
        Database.disconnectToDatabase();

        GenerateAllUserTable(dtable);
        uTable.Controls.Clear();
        uTable.Controls.Add(table);
    }
}
            
