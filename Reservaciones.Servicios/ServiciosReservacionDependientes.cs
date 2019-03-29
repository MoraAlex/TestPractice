using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reservaciones.Entidades;
using Reservaciones.Servicios.BaseDeDatos;

namespace Reservaciones.Servicios
{
    public class ServiciosReservacionDependientes
    {
        public ServiciosReservacionDependientes()
        {

        }

        public List<CostoHabitacionPorDia> BuscarHabitaciones(DateTime entrada, DateTime salida, Usuario usuario)
        {
            var resultado = new List<CostoHabitacionPorDia>();
            if (entrada > salida)
            {
                throw new ArgumentException("Fecha de entrada es mayor a fecha de salida");
            }

            var habitacionesDisponibles = new RepositorioHabitaciones().SeleccionarhabitacionesDisponibles(entrada, salida);
            var reservaciones = new RepositorioReservaciones().BuscarReservacionesPorUsuario(usuario);

            //agregar habitaciones, que el usuario no haya reservado en esas fechas, a los resultados
            foreach (var habitacion in habitacionesDisponibles)
            {
                var reservasDeHabitacion = reservaciones.Where(r=> r.HabitacionesReservadas.Any(hr=>hr.Habitacion.Id == habitacion.Id));
                var reservasEnFechas = reservasDeHabitacion.Where(r=> (entrada >= r.FechaDeEntrada && entrada <= r.FechaDeSalida) || (salida >= r.FechaDeEntrada && salida <= r.FechaDeSalida));
                if (!reservasEnFechas.Any())
                {
                    var costo = new ServiciosReservacionLocales().GenerarPrecioDeHabitacionPorFechaYUsuario(habitacion, entrada, salida, usuario);
                    resultado.AddRange(costo);
                }
            }

            return resultado;
        }
    }
}
