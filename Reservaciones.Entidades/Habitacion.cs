using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservaciones.Entidades
{
    public class Habitacion
    {
        public int Id { get; set; }
        public Hotel Hotel { get; set; }

        public decimal TarifaBase { get; set; }
        public TipoHbitacion Tipo { get; set; }
        public EstadoOcupacion Ocupacion { get; set; }
    }
}
