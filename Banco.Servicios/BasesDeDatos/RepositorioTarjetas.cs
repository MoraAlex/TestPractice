using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Banco.Entidades;
using Banco.Servicios.ServiciosDeTerceros;

namespace Banco.Servicios
{
    public class RepositorioTarjetas
    {     

        public TarjetaDeCredito SeleccionarTarjeta(int idTarjeta)
        {
            TarjetaDeCredito result = null;

            using (SqlConnection connection = new SqlConnection(
               Constantes.SqlConnectionString))
            {
                SqlCommand command = new SqlCommand($"SELECT * FROM Tarjetas WHERE Id= {idTarjeta}", connection);
                command.Connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {   
                    var tipo = (int)reader[0];
                    switch (tipo)
                    {
                        case 1:
                            result = new TarjetaClasica();
                            break;
                        case 2:
                            result = new TarjetaOro();
                            break;
                        case 3:
                            result = new TarjetaPlatino();
                            break;
                    }
                }
            }
            return result;
        }        
    }
}
