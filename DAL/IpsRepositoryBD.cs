using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;
using System.Data.SqlClient;

namespace DAL
{
   public  class IpsRepositoryBD
    {
        List<string> lista = new List<string>();
        ConnectionManager connection;

        public IpsRepositoryBD(ConnectionManager connectionManager)
        {
            connection = connectionManager;
        }

        public List<string> ListaIps()
        {
            lista.Clear();
            SqlDataReader sqlDataReader;
            using(var command = connection.SqlConnection.CreateCommand())
            {
                command.CommandText = "Select * from IPS ";
                sqlDataReader = command.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    lista.Add(sqlDataReader[0].ToString());
                }
                sqlDataReader.Close();
            }
            return lista;
        }
    }
}
