using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;
using DAL;


namespace BLL
{
    public class IpsServiceBd
    {
        ConnectionManager connection;
        IpsRepositoryBD IpsRepositoryBD;

        public IpsServiceBd(string conexion)
        {
            connection = new ConnectionManager(conexion);
            IpsRepositoryBD = new IpsRepositoryBD(connection);
        }

        public ListaResouesta Lista()
        {
            ListaResouesta respuesta;
            try
            {
                connection.Open();
                respuesta = new ListaResouesta(IpsRepositoryBD.ListaIps());
                connection.Close();
                return respuesta;
            }
            catch (Exception e)
            {

                respuesta = new ListaResouesta($"Error: {e.Message}");
                return respuesta;
            }
        }

    }
    public class ListaResouesta
    {
        public List<string> Lista { get; set; }
        public string Mensaje { get; set; }
        public ListaResouesta(List<string> lista)
        {
            Lista = lista;
        }
        public ListaResouesta(string mensaje)
        {
            Mensaje = mensaje;
        }
    }
}
