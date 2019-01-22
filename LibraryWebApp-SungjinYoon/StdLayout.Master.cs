using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace LibraryWebApp_SungjinYoon
{
    public partial class StdLayout : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.IsAuthenticated)
            {
                Label1.Text = "Wecome " + Context.User.Identity.Name;
                LinkButton1.Visible = false;//!IsAuthenticated
                LinkButton2.Visible = true; //IsAuthenticated
            }
            else
            {
                Label1.Text = "Wecome Guest";
                LinkButton1.Visible = true;
                LinkButton2.Visible = false;
            }
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {

        }

        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            Response.Redirect("~/register.aspx");

        }
    }
}