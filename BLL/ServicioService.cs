using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Entity;
using System.IO;

namespace BLL
{
    public class ServicioService
    {
        ServicioRepository ServicioRepository;

        public ServicioService(Stream stream)
        {
            ServicioRepository = new ServicioRepository(stream);
        }

        public RespuestaConsulta Consultar()
        {
            RespuestaConsulta respuestaConsulta;
            try
            {
                List<Servicio> servicios = ServicioRepository.Consultar();
                respuestaConsulta = new RespuestaConsulta(servicios);
                return respuestaConsulta;
            }
            catch (Exception e)
            {
                respuestaConsulta = new RespuestaConsulta($"Error: {e.Message}");
                return respuestaConsulta;
            }
        }

        public RespuestaConsulta Validar( string combo)
        {
            RespuestaConsulta respuestaConsulta;
            try
            {
                List<Servicio> servicios1 = ServicioRepository.Validar(combo);
                respuestaConsulta = new RespuestaConsulta(servicios1);
                return respuestaConsulta;
            }
            catch (Exception e)
            {

                respuestaConsulta = new RespuestaConsulta($"Error: {e.Message}");
                return respuestaConsulta;
            }
        }

        public RespuestaConsulta ValidarValor(double Valor)
        {
            RespuestaConsulta respuestaConsulta;
            try
            {
                List<Servicio> servicios1 = ServicioRepository.ValidarValor(Valor);
                respuestaConsulta = new RespuestaConsulta(servicios1);
                return respuestaConsulta;
            }
            catch (Exception e)
            {

                respuestaConsulta = new RespuestaConsulta($"Error: {e.Message}");
                return respuestaConsulta;
            }
        }


        public RespuestaConsulta Noaptos(string combo, double Valor)
        {
            RespuestaConsulta respuestaConsulta;
            try
            {
                List<Servicio> servicios1 = ServicioRepository.NoAptos(combo, Valor);
                respuestaConsulta = new RespuestaConsulta(servicios1);
                return respuestaConsulta;
            }
            catch (Exception e)
            {

                respuestaConsulta = new RespuestaConsulta($"Error: {e.Message}");
                return respuestaConsulta;
            }
        }
        
    }

    public class RespuestaConsulta
    {
        public List<Servicio> Servicios{ get; set; }
        public string Mensaje { get; set; }
    public bool Error { get; set; }

        public RespuestaConsulta(List<Servicio> servicios)
        {
            Servicios = servicios;
            Error = false;
        }
        public RespuestaConsulta(string mensaje)
        {
            Mensaje = mensaje;
            Error = true;
        }
}
}
