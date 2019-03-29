using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservaciones.Entidades
{
    public class Reservacion
    {
        public Usuario Usuario { get; set; }
        public int Id { get; set; }

        public List<CostoHabitacionPorDia> HabitacionesReservadas { get; set; }
             
        public EstatusPago EstatusPago { get; set; }
        public DateTime FechaDeReserva { get; set; }

        public DateTime FechaDeEntrada { get; set; }
        public DateTime FechaDeSalida { get; set; }

        public decimal Total
        {
            get
            {
                return HabitacionesReservadas.Sum(h => h.Precio);
            }
        }
    }
}
