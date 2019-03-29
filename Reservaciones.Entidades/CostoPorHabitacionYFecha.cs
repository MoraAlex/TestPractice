using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservaciones.Entidades
{
    public class CostoHabitacionPorDia
    {
        public CostoHabitacionPorDia(Habitacion habitacion, decimal costo)
        {
            Habitacion = habitacion;
            Precio = costo;
        }

        public Habitacion Habitacion { get; set; }

        public DateTime Fecha { get; set; }
        public decimal Precio { get; set; }
    }
}
