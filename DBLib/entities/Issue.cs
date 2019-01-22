using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBLib.entities
{
    public class Issue
    {

        public int IssueID { get; set; }
        public int UserID { get; set; }
        public int BookID { get; set; }
        public string BookTitle { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime DueDate { get; set; }
        public double Fine { get; set; }



        public Issue(int userID, int bookID)
        {
            UserID = userID;
            BookID = bookID;
        }

        public Issue(int userID, int bookID, DateTime issueDate)
        {
            UserID = userID;
            BookID = bookID;
            IssueDate = issueDate;
        }

        public Issue(int issueID, int userId, int bookId, string bookTitle, DateTime issueDate)
        {
            IssueID = issueID;
            UserID = userId;
            BookID = bookId;
            BookTitle = bookTitle;
            IssueDate = issueDate;
            DueDate = IssueDate.AddDays(14);

            if(CalFine()>0)
                Fine = CalFine();
            

        }

        private double CalFine()
        {
            //Calculate a fine
            DateTime now = DateTime.Now;
            TimeSpan elapsed = now.Subtract(DueDate);
            double daysAgo = elapsed.TotalDays;

            return (int)daysAgo * 0.25;
        }
    }
}
