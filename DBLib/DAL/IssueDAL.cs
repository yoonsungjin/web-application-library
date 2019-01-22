using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DBLib.entities;

namespace DBLib.DAL
{
    public class IssueDAL
    {
        ConnectionFactory connectionFactory = new ConnectionFactory();


        public void UpdateBook(int issueID)
        {

            using (SqlConnection cnn = connectionFactory.GetConnection())

            using (SqlCommand cmd = new SqlCommand("update Issue set DateOfReturn = @DateOfReturn where ID = @ID", cnn))
            {
                cnn.Open();
                cmd.Parameters.AddWithValue("@DateOfReturn", DateTime.Today);
                cmd.Parameters.AddWithValue("@ID", issueID);
                
                //              count = Convert.ToInt32(cmd.ExecuteScalar());   //returning the auto generated id(=count)

                cmd.ExecuteNonQuery();
            }
        }

        public void IssueBook(int userID, int bookID)
        {

            using (SqlConnection cnn = connectionFactory.GetConnection())

            using (SqlCommand cmd = new SqlCommand("insert into Issue(UserID, BookID, DateOfIssue) values (@UserID, @BookID, @DateOfIssue)", cnn))
            {
                cnn.Open();
                cmd.Parameters.AddWithValue("@UserID", userID);
                cmd.Parameters.AddWithValue("@BookID", bookID);
                cmd.Parameters.AddWithValue("@DateOfIssue", DateTime.Today);

                //              count = Convert.ToInt32(cmd.ExecuteScalar());   //returning the auto generated id(=count)

                cmd.ExecuteNonQuery();
            }
        }
        public int CountCopies(int bookID)
        {
            int count=0;

            using (SqlConnection cnn = connectionFactory.GetConnection())

            using (SqlCommand cmd = new SqlCommand("select count(*) as count from Issue where BookID=@bookID and DateOfReturn IS NULL", cnn))
            {
                cnn.Open();
                cmd.Parameters.AddWithValue("@BookID", bookID);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    count = Convert.ToInt16(reader["count"]);
                }
            }
            return count;
        }

        public List<Book> GetBookByUserID(int userID)
        {
            List<Book> books = new List<Book>();

            using (SqlConnection cnn = connectionFactory.GetConnection())

            using (SqlCommand cmd = new SqlCommand("select * from Issue i join Book b on i.BookID=b.BookID where userID=@userID", cnn))
            {
                cnn.Open();
                cmd.Parameters.AddWithValue("@userID", userID);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                      Book b = new Book(Convert.ToInt16(reader["BookID"]),
                        reader["Title"].ToString(),
                        Convert.ToInt16(reader["NumberOfCopies"]));
                    books.Add(b);
                }
            }
            return books;
        }

        public List<Issue> ReadByUserID(int userID)
        {
            List<Issue> issues = new List<Issue>();

            using (SqlConnection cnn = connectionFactory.GetConnection())
            using (SqlCommand cmd = new SqlCommand("select Issue.ID, Book.BookID, Book.Title, Issue.DateOfIssue, Issue.DateOfReturn " +
                                                   "from Book, Issue where Book.BookID = Issue.BookID and UserID=@UserID and Issue.DateOfReturn IS NULL", cnn))
            {
                cnn.Open();
                cmd.Parameters.AddWithValue("@UserID", userID);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Issue i = new Issue(Convert.ToInt16(reader["ID"]), userID, Convert.ToInt16(reader["BookID"]), reader["Title"].ToString(), 
                        Convert.ToDateTime(reader["DateOfIssue"]));
                    //Issue i = new Issue(userID, Convert.ToInt16(reader["BookID"]), reader["Title"].ToString(),
                    //    Convert.ToDateTime(reader["DateOfIssue"]), Convert.ToDateTime(reader["DateOfReturn"]));

                    issues.Add(i);
                }
            }
            return issues;
        }

        //public List<Issue> ReadByUserID(int userID)
        //{
        //    List<Issue> issues = new List<Issue>();

        //    using (SqlConnection cnn = connectionFactory.GetConnection())
        //    using (SqlCommand cmd = new SqlCommand("select * from Issue where UserID=@UserID", cnn))
        //    {
        //        cnn.Open();
        //        cmd.Parameters.AddWithValue("@UserID", userID);
        //        SqlDataReader reader = cmd.ExecuteReader();
        //        while (reader.Read())
        //        {
        //            Issue i = new Issue(Convert.ToInt16(reader["UserID"]), Convert.ToInt16(reader["BookID"]), Convert.ToDateTime(reader["IssueDate"]));
        //            issues.Add(i);
        //        }
        //    }
        //    return issues;
        //}

    }
}
