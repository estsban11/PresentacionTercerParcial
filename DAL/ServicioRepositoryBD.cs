using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;
using System.Data.SqlClient;

namespace DAL
{
    public class ServicioRepositoryBD
    {
        ConnectionManager ConnectionManager;
        List<Laboratorio> laboratorios = new List<Laboratorio>();
        List<Servicio> Servicios = new List<Servicio>();
        public ServicioRepositoryBD(ConnectionManager connection)
        {
            ConnectionManager = connection;
        }

        public void Guardar(List<Servicio> servicios)
        {
            foreach (var item in servicios)
            {
                using(var command = ConnectionManager.SqlConnection.CreateCommand())
                {
                    command.CommandText = "Registrar_servicio";
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue(@"idIps", item.Id);
                    command.Parameters.AddWithValue(@"identificacion", item.Identificacion);
                    command.Parameters.AddWithValue(@"nombre", item.Nombre);
                    command.Parameters.AddWithValue(@"idLaboratorio", item.IdLaboratorio);
                    command.Parameters.AddWithValue(@"valor", item.Valor);
                    command.ExecuteNonQuery();
                }
            }
        }

        public List<Servicio> ConsultarServicios()
        {
            Servicios.Clear();
            SqlDataReader sqlDataReader;
            using (var command = ConnectionManager.SqlConnection.CreateCommand())
            {
                command.CommandText = "Select * from Servicio";
                sqlDataReader = command.ExecuteReader();
                if (sqlDataReader.HasRows)
                {
                    while (sqlDataReader.Read())
                    {
                        if (!sqlDataReader.HasRows) return null;
                       Servicios.Add(MapearServicio(sqlDataReader));
                    }
                    sqlDataReader.Close();
                }
            }
            return Servicios;
        }

        public Servicio MapearServicio(SqlDataReader sqlDataReader)
        {
            Servicio servicio = new Servicio();
            servicio.Id = ((object)sqlDataReader[@"IdIps"]).ToString();
            servicio.Identificacion = ((object)sqlDataReader[@"identificacion"]).ToString();
            servicio.Nombre = ((object)sqlDataReader[@"nombre"]).ToString();
            servicio.IdLaboratorio = ((object)sqlDataReader[@"idLaboratorio"]).ToString();
            servicio.Valor = Convert.ToDouble( ((object)sqlDataReader[@"valor"]).ToString());
            return servicio;
        }

        public List<Laboratorio> Consultar()
        {
            laboratorios.Clear();
            SqlDataReader sqlDataReader;
            using(var command = ConnectionManager.SqlConnection.CreateCommand())
            {
                command.CommandText = "Select * from Laboratorios";
                sqlDataReader = command.ExecuteReader();
                if (sqlDataReader.HasRows)
                {
                    while (sqlDataReader.Read())
                    {
                        if (!sqlDataReader.HasRows) return null;
                        laboratorios.Add(Mapear(sqlDataReader));
                    }
                    sqlDataReader.Close();
                }
            }
            return laboratorios;
        }

        public Laboratorio Mapear(SqlDataReader sqlDataReader)
        {
            Laboratorio laboratorio = new Laboratorio();
            laboratorio.IdLaboratorio = ((object)sqlDataReader[@"IdLaboratorio"]).ToString();
            laboratorio.Nombre = ((object)sqlDataReader[@"Nombre"]).ToString();
            laboratorio.Valor =Convert.ToDouble( ((object)sqlDataReader[@"Valor"]).ToString());
            return laboratorio;
        }

        public double ObtenerValor(string id)
        {
            List<Laboratorio> laboratorios = Consultar();
            return laboratorios.Where(l => l.IdLaboratorio == id).Select(l => l.Valor).FirstOrDefault();
        }
    }
}
