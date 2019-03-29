using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reservaciones.Entidades;
using Reservaciones.Servicios;
using NUnit.Framework;

namespace Reservaciones.UnitTests
{
    [TestFixture]
    public class ServiciosReservacionLocalesTests
    {
        [Test]
        public void FiltrarBusquedaDeHabitaciones_Tipos()
        {
            List<Habitacion> habitaciones = new List<Habitacion>
            {
                new Habitacion()
                {
                    Tipo = TipoHbitacion.Doble
                },
                new Habitacion()
                {
                    Tipo = TipoHbitacion.Sencilla
                },
                new Habitacion()
                {
                    Tipo = TipoHbitacion.Doble
                },
                new Habitacion()
                {
                    Tipo = TipoHbitacion.Triple
                },
                new Habitacion()
                {
                    Tipo = TipoHbitacion.Triple
                },
                new Habitacion()
                {
                    Tipo = TipoHbitacion.Doble
                }
            };
            List<TipoHbitacion> tipoHbitaciones = new List<TipoHbitacion>
            {
                TipoHbitacion.Doble,
                TipoHbitacion.Sencilla,
                TipoHbitacion.Triple
            };
            ServiciosReservacionLocales servicios = new ServiciosReservacionLocales();
            CollectionAssert.AreEqual(habitaciones, 
                servicios.FiltrarBusquedaDeHabitaciones(habitaciones, tipoHbitaciones, null, null));
        }

        [Test]
        public void FiltrarBusquedaDeHabitaciones_Amenidades()
        {
            List<Habitacion> habitaciones = new List<Habitacion>
            {

                new Habitacion()
                {
                    Tipo = TipoHbitacion.Doble
                },
                new Habitacion()
                {
                    Tipo = TipoHbitacion.Sencilla
                },
                new Habitacion()
                {
                    Tipo = TipoHbitacion.Doble
                },
                new Habitacion()
                {
                    Tipo = TipoHbitacion.Triple
                },
                new Habitacion()
                {
                    Tipo = TipoHbitacion.Triple
                },
                new Habitacion()
                {
                    Tipo = TipoHbitacion.Doble
                }
            };
            List<TipoHbitacion> tipoHbitaciones = new List<TipoHbitacion>
            {
                TipoHbitacion.Doble,
                TipoHbitacion.Sencilla,
                TipoHbitacion.Triple
            };
            List<Amenidad> amenidades = new List<Amenidad>
            {
                Amenidad.AguaCaliente
            };
            ServiciosReservacionLocales servicios = new ServiciosReservacionLocales();
            CollectionAssert.AreEqual(habitaciones,
                servicios.FiltrarBusquedaDeHabitaciones(habitaciones, tipoHbitaciones, amenidades, null));
        }
    }
}
