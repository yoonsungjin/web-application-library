using DBLib.entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DBLib.DAL
{
    public class MemberDAL
    {
        ConnectionFactory connectionFactory = new ConnectionFactory();

        public bool Authenticate(string name, string password)
        {
            bool Authenticated = false;
            string savedHash, savedSalt;
            using (SqlConnection cnn = connectionFactory.GetConnection())
            using (SqlCommand cmd = new SqlCommand("select * from Member where Name=@Name", cnn))
            {
                cnn.Open();
                cmd.Parameters.AddWithValue("@Name", name);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    savedHash = reader["Password"].ToString();
                    savedSalt = reader["Salt"].ToString();
                    byte[] bSalt = Convert.FromBase64String(savedSalt);
                    byte[] bPass = Encoding.UTF8.GetBytes(password);

                    byte[] bComb = new byte[bSalt.Length + bPass.Length];
                    bSalt.CopyTo(bComb, 0);
                    bPass.CopyTo(bComb, bSalt.Length);

                    SHA512 sHA512 = SHA512.Create();
                    byte[] bHash = sHA512.ComputeHash(bComb);

                    String hashPassword = Convert.ToBase64String(bHash);

                    if (hashPassword == savedHash)
                        Authenticated = true;


                }
            }
            return Authenticated;
        }


        public int Add(Member m)
        {
            int count = 0;

            RNGCryptoServiceProvider rNG = new RNGCryptoServiceProvider();
            byte[] bSalt = new byte[16];
            rNG.GetBytes(bSalt);

            byte[] bPass = Encoding.UTF8.GetBytes(m.Password);

            byte[] bComb = new byte[bSalt.Length + bPass.Length];
            bSalt.CopyTo(bComb, 0);
            bPass.CopyTo(bComb, bSalt.Length);

            SHA512 sHA512 = SHA512.Create();
            byte[] bHash = sHA512.ComputeHash(bComb);

            String hashPassword = Convert.ToBase64String(bHash);
            String salt = Convert.ToBase64String(bSalt);



            using (SqlConnection cnn = connectionFactory.GetConnection())


            using (SqlCommand cmd = new SqlCommand("insert into Member values (@Name, @Email, @PhoneNumber, @UserCategory, @DateOfBirth, @Password, @DateOfRegistration, @Salt)", cnn))
            {
                cnn.Open();
                cmd.Parameters.AddWithValue("@Name", m.Name);
                cmd.Parameters.AddWithValue("@Email", m.email);
                cmd.Parameters.AddWithValue("@PhoneNumber", m.PhoneNumber);
                cmd.Parameters.AddWithValue("@UserCategory", Convert.ToDecimal(m.userCategory));
                cmd.Parameters.AddWithValue("@DateOfBirth", m.DateOfBirth);
                cmd.Parameters.AddWithValue("@Password", hashPassword);
                cmd.Parameters.AddWithValue("@DateOfRegistration", m.DateOfRegistration);
                cmd.Parameters.AddWithValue("@Salt", salt);

                count = Convert.ToInt32(cmd.ExecuteScalar());   //returning the auto generated id(=count)

                // cmd.ExecuteNonQuery();
            }
            return count;
        }

        public int GetUserIDByName(string name)
        {
            int userID = 0;

            using (SqlConnection cnn = connectionFactory.GetConnection())

            using (SqlCommand cmd = new SqlCommand("select * from Member where Name=@Name", cnn))
            {
                cnn.Open();
                cmd.Parameters.AddWithValue("@Name", name);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    userID = Convert.ToInt32(reader["UserID"]);


                }
            }
            return userID;
        }

        //public Student ReadByNo(int studentNo)
        //{
        //    Student s = null;

        //    using (SqlConnection cnn = connectionFactory.GetConnection())

        //    using (SqlCommand cmd = new SqlCommand("select * from student where StudentNo=@StudentNo", cnn))
        //    {
        //        cnn.Open();
        //        cmd.Parameters.AddWithValue("@StudentNo", studentNo);
        //        SqlDataReader reader = cmd.ExecuteReader();
        //        if (reader.Read())
        //        {
        //            s = new Student(Convert.ToInt32(reader["studentno"]),
        //            reader["name"].ToString(),
        //            reader["address"].ToString());


        //        }
        //    }
        //    return s;
        //}


        //public List<Student> ReadAll()
        //{
        //    List<Student> students = new List<Student>();
        //    using (SqlConnection cnn = connectionFactory.GetConnection())

        //    using (SqlCommand cmd = new SqlCommand("select * from student", cnn))
        //    {
        //        cnn.Open();
        //        SqlDataReader reader = cmd.ExecuteReader();
        //        if (reader.Read())
        //        {
        //            Student s = new Student(Convert.ToInt32(reader["studentno"]),
        //                reader["name"].ToString(),
        //                reader["address"].ToString());
        //            students.Add(s);

        //        }
        //    }
        //    return students;
        //}


    }

}
