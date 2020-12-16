using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;
using DAL;

namespace BLL
{
    public class ServicioServiceBD
    {
        private readonly ConnectionManager connection;
        private readonly ServicioRepositoryBD servicioRepositoryBD;

        public ServicioServiceBD(string cadena)
        {
            connection = new ConnectionManager(cadena);
            servicioRepositoryBD = new ServicioRepositoryBD(connection);
        }

        public double Valor(string id)
        {
            try
            {
                connection.Open();
                double valor = servicioRepositoryBD.ObtenerValor(id);
                connection.Close();
                return valor;
            }
            catch (Exception e)
            {

                return 0;
            }
        }

        public string Guardar(List<Servicio> servicios)
        {
            try
            {
                connection.Open();
                servicioRepositoryBD.Guardar(servicios);
                connection.Close();
                return "Se guardo exitosamente";
            } 
            catch (Exception e)
            {

                return $"Error: {e.Message}";
            }
        }

        public RespuestaConsulta Consultar()
        {
            RespuestaConsulta respuesta;
            try
            {
                connection.Open();
                respuesta = new RespuestaConsulta(servicioRepositoryBD.ConsultarServicios());
                connection.Close();
                return respuesta;
            }
            catch (Exception e)
            { 
                respuesta = new RespuestaConsulta($"Error: {e.Message}");
                return respuesta;
            }
            finally { connection.Close(); }
        }
    }

    public class RespuestaServivios
    {
        public List<Servicio> Servicios { get; set; }
        public string Mensaje { get; set; }

        public RespuestaServivios(List<Servicio> servicios)
        {
            Servicios = servicios;
        }
        public RespuestaServivios(string mensaje)
        {
            Mensaje = mensaje;
        }
    }
}
