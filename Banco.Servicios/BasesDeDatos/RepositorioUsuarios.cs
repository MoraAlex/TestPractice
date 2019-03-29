using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using
    Banco.Entidades;

namespace Banco.Servicios
{
    public class RepositorioUsuarios : IRepositorioUsuarios
    {
        public Usuario SeleccionarUsuario(string nombreDeUsuario)
        {
            Usuario result = null;

            using (SqlConnection connection = new SqlConnection(
               Constantes.SqlConnectionString))
            {
                SqlCommand command = new SqlCommand("SELECT TOP 1 * FROM Configuracion", connection);
                command.Connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    result = new Usuario();
                    result.Id = (int)reader[0];
                    result.NombreDeUsuario = reader[0].ToString();
                }
            }

            return result;
        }
    }
}
