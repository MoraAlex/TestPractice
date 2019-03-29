using Reservaciones.Entidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservaciones.Servicios.BaseDeDatos
{
    public class RepositorioReservaciones
    {
        public List<Reservacion> BuscarReservacionesPorUsuario(Usuario usuario)
        {
            List<Reservacion> result = new List<Reservacion>();

            using (SqlConnection connection = new SqlConnection(
               Constantes.SqlConnectionString))
            {
                SqlCommand command = new SqlCommand("SELECT  * FROM Reservaciones", connection);
                command.Connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var reservacion = new Reservacion();
                    reservacion.Id = (int)reader[0];
                    reservacion.FechaDeEntrada = (DateTime)reader[1];
                    //[...]
                    result.Add(reservacion);
                }
            }

            return result;
        }
    }
}
