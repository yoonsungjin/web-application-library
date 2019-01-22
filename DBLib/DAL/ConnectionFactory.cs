using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SqlClient;

namespace DBLib.DAL
{
    class ConnectionFactory
    {
        public SqlConnection GetConnection()
        {
            SqlConnection cnn = new SqlConnection(@"Data Source=DESKTOP-IFTOF8P\SQLSERVER;Initial Catalog=TestDB;Integrated Security=True");
            return cnn;
        }
    }
}
