using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DBLib.entities;

namespace DBLib.DAL
{
    public class BookDAL
    {
        ConnectionFactory connectionFactory = new ConnectionFactory();

        public List<Book> ReadByTitle(string title)
        {
            List<Book> books = new List<Book>();

            using (SqlConnection cnn = connectionFactory.GetConnection())
            using (SqlCommand cmd = new SqlCommand("select * from Book where Title like @Title", cnn))
            {
                cnn.Open();
                cmd.Parameters.AddWithValue("@Title", title);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Book b = new Book(Convert.ToInt16(reader["BookID"]), reader["Title"].ToString(), Convert.ToInt16(reader["NumberOfCopies"]));
                    books.Add(b);
                }
            }
            return books;
        }

        public int GetNumberOfCopies(int bookID)
        {
            int count = 0;

            using (SqlConnection cnn = connectionFactory.GetConnection())

            using (SqlCommand cmd = new SqlCommand("select NumberOfCopies from Book where BookID=@bookID", cnn))
            {
                cnn.Open();
                cmd.Parameters.AddWithValue("@BookID", bookID);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    count = Convert.ToInt16(reader["NumberOfCopies"]);
                }
            }
            return count;
        }

        //      public void Add(Book b)
        //      {

        //          using (SqlConnection cnn = connectionFactory.GetConnection())

        //          using (SqlCommand cmd = new SqlCommand("insert into Book values (@Title, @NumberOfCopies)", cnn))
        //          {
        //              cnn.Open();
        //              cmd.Parameters.AddWithValue("@Title", b.Title);
        //              cmd.Parameters.AddWithValue("@NumberOfCopies", b.NumberOfCopies);

        ////              count = Convert.ToInt32(cmd.ExecuteScalar());   //returning the auto generated id(=count)

        //               cmd.ExecuteNonQuery();
        //          }
        //      }



    }
}
