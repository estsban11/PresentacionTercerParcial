using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;
using DAL;

namespace BLL
{
   public  class ArchivoLogService
    {
        ArchivoLogRepository ArchivoLog;

        public ArchivoLogService()
        {
            ArchivoLog = new ArchivoLogRepository();
        }

        public string Guardar(List<Servicio> servicios, double valor)
        {
            try
            {
                ArchivoLog.Guardar(servicios, valor);
                return "Todo correcto";
            }
            catch (Exception e)
            {

                return $"Error: {e.Message}";
            }
        }
    }
}
