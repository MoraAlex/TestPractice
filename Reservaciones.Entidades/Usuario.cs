using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservaciones.Entidades
{
    public class Usuario
    {
        public int Id { get; set; }
        public string NombreDeUsuario { get; set; }
        public string Password { get; set; }

        public string PrimerNombre { get; set; }
        public string SegundoNombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }

        public DateTime FechaNacimiento { get; set; }
        public string RFC { get; set; }
        public string Email { get; set; }

        public DateTime MiembroDesde { get; set; }
        public bool EsUsuarioPremium { get; set; }
    }
}
