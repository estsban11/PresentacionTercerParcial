using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;
using System.IO;

namespace DAL
{
    public class ServicioRepository
    {

       
        public Stream Stream { get; set; }
        public List<Servicio> servicios = new List<Servicio>();

        public ServicioRepository(Stream stream)
        {
            Stream = stream;
        }

        public List<Servicio> Consultar()
        {
            servicios.Clear();
            string linea = string.Empty;
            StreamReader lector = new StreamReader(Stream);
            while((linea = lector.ReadLine()) != null)
            {
                Servicio servicio = Mapear(linea);
                servicios.Add(servicio);
            }
            lector.Close();
            return servicios;
        }

        public Servicio Mapear(string linea)
        {
            Servicio servicio = new Servicio();
            string[] Datos = linea.Split(';');
            servicio.Id = Datos[0];
            servicio.Identificacion = Datos[1];
            servicio.Nombre = Datos[2];
            servicio.IdLaboratorio = Datos[3];
            servicio.Valor = Convert.ToDouble(Datos[4]);
            return servicio;
        }

        public List<Servicio> Validar( string combo)
        {
            return servicios.Where(s => s.Id == combo).ToList();
        }

        public List<Servicio> ValidarValor(double Valor)
        {
            return servicios.Where(s => s.Valor == Valor).ToList();
        }

        public List<Servicio> NoAptos(string combo, double Valor)
        {
            return servicios.Where(s => s.Id == combo && s.Valor != Valor).ToList();
        }

    }
}
