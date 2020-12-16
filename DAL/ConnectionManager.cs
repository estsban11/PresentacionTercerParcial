using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace DAL
{
   public class ConnectionManager
    {

        public SqlConnection SqlConnection;

        public ConnectionManager(string connection)
        {
            SqlConnection = new SqlConnection(connection);
        }

        public void Open()
        {
            SqlConnection.Open();
        }

        public void Close()
        {
            SqlConnection.Close();
        }
    }
}
