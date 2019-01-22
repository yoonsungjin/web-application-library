using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DBLib.entities;
using DBLib.DAL;

namespace LibraryWebApp_SungjinYoon
{
    public partial class register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            EnumToListBox(typeof(UserCategory), lstUserCategory);
        }

        protected void btnSelDate_Click(object sender, EventArgs e)
        {
            Calendar1.Visible = true;

        }

        protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        {
            txtDateOfBirth.Text = (Calendar1.SelectedDate.ToShortDateString()).ToString();
            Calendar1.Visible = false;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string name = txtName.Text;
            string email = txtEmail.Text;
            string phoneNumber = txtPhoneNumber.Text;
            UserCategory uc = (UserCategory)lstUserCategory.SelectedIndex;  
            DateTime dateOfBirth = Convert.ToDateTime(txtDateOfBirth.Text);
            string password = txtPassword.Text;
            DateTime dateOfRegistration = DateTime.Today;
            MemberDAL memberDAL = new MemberDAL();
            Member m = new Member(name, email, phoneNumber, uc, dateOfBirth, password, dateOfRegistration);

            //
            if (CalAge(dateOfBirth) >= 15)
            {
                memberDAL.Add(m);
                lblStatus.Text = "Registration was successful";
            }
            else
            {
                lblStatus.Text = "Membership is allowed over 15 years old";
            }
        }

        //Display enum to list box
        static public void EnumToListBox(Type EnumType, ListControl TheListBox)
        {
            Array Values = System.Enum.GetValues(EnumType);

            foreach (int Value in Values)
            {
                string Display = Enum.GetName(EnumType, Value);
                ListItem Item = new ListItem(Display, Value.ToString());
                TheListBox.Items.Add(Item);
            }
        }

        //Calculate age = today - date of birth
        private int CalAge(DateTime dob)
        {
            DateTime Today = DateTime.Now;
            TimeSpan ts = Today - dob;
            
            int age = (ts.Days / 365);

            return age;
        }
    }
}