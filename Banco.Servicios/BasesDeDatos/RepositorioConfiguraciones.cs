using Banco.Servicios.BasesDeDatos;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banco.Servicios
{
    public class RepositorioConfiguraciones : IRepositorioConfiguraciones
    {
        public int SeleccionarMaximoDeTarjetasPorUsuario()
        {
            var result = 0;

            using (SqlConnection connection = new SqlConnection(
               Constantes.SqlConnectionString))
            {
                SqlCommand command = new SqlCommand("SELECT TOP 1 * FROM Configuracion", connection);
                command.Connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    result = (int)reader[0];
                }
            }
            return result;
        }
    }
}
