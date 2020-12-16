using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;
using System.IO;

namespace DAL
{
   public class ArchivoLogRepository
    {
        private string Ruta = @"Log.txt";

        public void Guardar(List<Servicio> servicios, double valor)
        {
            FileStream file = new FileStream(Ruta, FileMode.Append);
            StreamWriter escritor = new StreamWriter(file);

            foreach (var item in servicios)
            {
                escritor.WriteLine($"{item.Id};{item.Identificacion};{item.Nombre};{item.IdLaboratorio};{item.Valor};El valor pactado es {valor}");
            }
            escritor.Close();
            file.Close();
        }
    }
}
