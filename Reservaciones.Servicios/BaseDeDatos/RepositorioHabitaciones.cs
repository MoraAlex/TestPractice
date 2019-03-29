using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reservaciones.Entidades;

namespace Reservaciones.Servicios.BaseDeDatos
{
    public class RepositorioHabitaciones
    {

        public List<Habitacion> SeleccionarhabitacionesDisponibles(DateTime entrada, DateTime salida)
        {
            List<Habitacion> result = new List<Habitacion>();

            using (SqlConnection connection = new SqlConnection(
               Constantes.SqlConnectionString))
            {
                SqlCommand command = new SqlCommand("SELECT  * FROM Habitaciones", connection);
                command.Connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var habitacion = new Habitacion();
                    habitacion.Id  = (int)reader[0];
                    habitacion.Tipo = (TipoHbitacion)reader[1];
                    //[...]
                    result.Add(habitacion);
                }
            }

            return result;
        }
    }
}
