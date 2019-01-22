using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Web.Security;
using DBLib.DAL;

namespace LibraryWebApp_SungjinYoon
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            
            MemberDAL memberDAL = new MemberDAL();
            //authenticate user using db by name and password
            if (memberDAL.Authenticate(txtUserName.Text, txtPassword.Text))
            {
                // store user name and userID in session variable
                Session["Name"] = Convert.ToString(txtUserName.Text);
                Session["UserID"] = Convert.ToString(memberDAL.GetUserIDByName(txtUserName.Text));  
                FormsAuthentication.RedirectFromLoginPage(txtUserName.Text, CheckBox1.Checked);
            }
            else
            {
                lblStatus.Text = "Login Failed. Try Again!";
                txtUserName.Text = "";
                txtPassword.Text = "";

            }
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            Response.Redirect("Register.aspx");
        }
    }
}