using DBLib.DAL;
using DBLib.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LibraryWebApp_SungjinYoon.restricted
{
    public partial class dashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblStatus.Text = "";
                MemberDAL memberDAL = new MemberDAL();
                IssueDAL issueDAL = new IssueDAL();

                if (Session["Name"] != null)
                {
                    //Retrieve User ID from DB by User name from session 
                    int userID = memberDAL.GetUserIDByName(Session["Name"].ToString());
                    List<Issue> issuedBooks = issueDAL.ReadByUserID(userID);
                    GridView1.DataSource = issuedBooks;
                    GridView1.DataBind();
                }
                
            }

            
        }


        protected void btnReturn_Click1(object sender, EventArgs e)
        {
            lblStatus.Text = "";
            foreach (GridViewRow row in GridView1.Rows)
            {
                CheckBox cb = (CheckBox)row.FindControl("ckbSelBook");
                if (cb.Checked)
                {
                    IssueDAL issueDAL = new IssueDAL();
                    //update database when issued book is returned. 
                    issueDAL.UpdateBook(Convert.ToInt16(row.Cells[1].Text));
                    lblStatus.Text = "Returned Successfully";
                    
                }
               
            }
        }

        protected void lBtnIssue_Click1(object sender, EventArgs e)
        {
            Response.Redirect("issueBook.aspx");
        }
    }
}