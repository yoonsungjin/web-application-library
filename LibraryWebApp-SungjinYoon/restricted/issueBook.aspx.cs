using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using DBLib.DAL;
using DBLib.entities;

namespace LibraryWebApp_SungjinYoon.restricted
{
    public partial class issueBook : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblStatus.Text = "";
            btnIssue.Visible = false;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtTitle.Text != "")
            {
                BookDAL bookDAL = new BookDAL();
                // retrieve books by book title from db
                List<Book> books = bookDAL.ReadByTitle("%" + txtTitle.Text + "%");
                GridView1.DataSource = books;
                GridView1.DataBind();
                btnIssue.Visible = true;
            }
            else
                lblStatus.Text = "Enter the book title!";
        }

        protected void btnIssue_Click(object sender, EventArgs e)
        {
            
            foreach (GridViewRow row in GridView1.Rows)
            {
                CheckBox cb = (CheckBox)row.FindControl("ckbSelBook");
                if (cb.Checked)
                {
                    IssueDAL issueDAL = new IssueDAL();
                    BookDAL bookDAL = new BookDAL();

                    //Calculate issued copy and retrieve number of copies 
                    int bookID = Convert.ToInt16(row.Cells[1].Text);
                    int issuedCopies = issueDAL.CountCopies(bookID);
                    int NumberOfCopies = bookDAL.GetNumberOfCopies(bookID);

                    if (issuedCopies < NumberOfCopies)
                    {
                        issueDAL.IssueBook(Convert.ToInt16(Session["UserID"]), Convert.ToInt16(row.Cells[1].Text));  //@TODO add userID
                        lblStatus.Text = "Book has been Issued Successfully";
                    }
                    else
                    {
                        lblStatus.Text = "The Book is not available at the moment";
                    }
                }
            }
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void lBtnDashboard_Click(object sender, EventArgs e)
        {
            Response.Redirect("Dashboard.aspx");
        }


    }
}