using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class ServiceRegisterEntry : System.Web.UI.Page
{
    string userName;

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> GetCompletionList(string prefixText)
    {
        return UserFuncs.FindUserNameList(prefixText);
    }

    protected void name_TextChanged(object sender, EventArgs e)
    {
        try
        {
            userName = name.Text;
            userName = userName.Split('(')[1];
            userName = userName.Split(')')[0];
            Response.Redirect("ServiceRegisterEntry.aspx?userName=" + userName);
        }
        catch (Exception ex)
        {
            serviceRegisterEntryWarning.InnerText = "Reset All fields";
        }
    }

    protected void updateButton_Click(object sender, EventArgs e)
    {
        try
        {
            userName = (Request.QueryString["userName"] == null) ? "" : Request.QueryString["userName"];
            int id = UserFuncs.FindUserIdFromUserName(userName);
            if (userName != null && id!=-1)
            {
                DataRow drow = Database.getRowFromTable("ServiceRegister");
                drow["userId"] = id;
                drow["postAndPayDescription"] = postAndPayDescription.Value;
                drow["permanentOrTemporary"] = permanentOrTemporary.Value;
                drow["incumbent"] = incumbent.Value;
                drow["postHeldPermanently"] = postHeldPermanently.Value;
                drow["payInPermanentPost"] = payInPermanentPost.Value;
                drow["officiatingPay"] = officiatingPay.Value;
                drow["otherPay"] = otherPay.Value;
                drow["fromPeriod"] = fromPeriod.Value;
                drow["toPeriod"] = toPeriod.Value;
                drow["events1to8"] = events1to8.Value;
                drow["leaveDescription"] = leaveDescription.Value;
                drow["punishmentReference"] = punishmentReference.Value;
                drow["remarks"] = remarks.Value;
                Database.updateTable(drow);
                serviceRegisterEntryWarning.InnerText = "";
            }
            
        }
        catch (Exception ex)
        {
            serviceRegisterEntryWarning.InnerText = ex.Message;
        }
    }
}
