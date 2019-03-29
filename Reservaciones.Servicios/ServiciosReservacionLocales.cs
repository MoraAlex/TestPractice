using Reservaciones.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservaciones.Servicios
{
    public class ServiciosReservacionLocales
    {
        /// <summary>
        /// Recibe una lista de habitaciones; regresa las habitaciones que cumplen co n los filtros indicados como parametro
        /// </summary>
        /// <param name="habitaciones"></param>
        /// <param name="tipos"></param>
        /// <param name="amenidades"></param>
        /// <param name="categorias"></param>
        /// <returns></returns>
        public List<Habitacion> FiltrarBusquedaDeHabitaciones(List<Habitacion> habitaciones, List<TipoHbitacion> tipos = null, List<Amenidad> amenidades = null, List<Categoria> categorias = null)
        {
            var resultado = new List<Habitacion>();

            if(tipos != null)
            {
                resultado.AddRange(habitaciones.Where(h=> tipos.Contains(h.Tipo) ));
            }
            if(amenidades != null)
            {   
                var habitacionesFiltradas = habitaciones.Where(hab => hab.Hotel.Amenidades.All(a => amenidades.Contains(a)));
                resultado.AddRange(habitacionesFiltradas);
            }
            if(categorias != null)
            {
                var habitacionesFiltradas = habitaciones.Where(hab => categorias.Contains(hab.Hotel.Categoria));
                resultado.AddRange(habitacionesFiltradas);
            }

            return resultado.Distinct().ToList();
        }

        public CostoHabitacionPorDia GenerarPrecioDeHabitacionPorFechaYUsuario(Habitacion habitacion, DateTime fecha, Usuario usuario)
        {
            decimal porcentajeCosto = 1;

            //si recibimos un usuario y es un usuario premium, aplicar descuento de 10%
            if (usuario != null && usuario.EsUsuarioPremium)
            {  
                porcentajeCosto -= 0.10m;               
            }

            //si recibimos fecha, aplicar tarifas de fin de semana
            if(fecha != null)
            {
                if(fecha.DayOfWeek == DayOfWeek.Friday)
                {
                    porcentajeCosto += 0.05m;
                }
                else if(fecha.DayOfWeek == DayOfWeek.Saturday || fecha.DayOfWeek == DayOfWeek.Sunday)
                {
                    porcentajeCosto += 0.15m;
                }
            }

            var resultado = new CostoHabitacionPorDia(habitacion, habitacion.TarifaBase * porcentajeCosto);
            return resultado;
        }

        /// <summary>
        /// Regresa el precio de la ha bitacion por dia para cada dia del rango de fechas
        /// </summary>
        /// <param name="habitacion"></param>
        /// <param name="entrada"></param>
        /// <param name="salida"></param>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public List<CostoHabitacionPorDia> GenerarPrecioDeHabitacionPorFechaYUsuario(Habitacion habitacion, DateTime entrada, DateTime salida, Usuario usuario)
        {
            var resultado = new List<CostoHabitacionPorDia>();
            for (var fecha = entrada; fecha <= salida; fecha = fecha.AddDays(1))
            {
                resultado.Add(GenerarPrecioDeHabitacionPorFechaYUsuario(habitacion, fecha, usuario));
            }
            return resultado;
        }
    }
}
