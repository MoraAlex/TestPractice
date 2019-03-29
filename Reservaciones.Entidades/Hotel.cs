using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservaciones.Entidades
{
    public class Hotel 
    {
        public int HotelId { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
                
        public List<Habitacion> Habitaciones { get; set; }
        public Categoria Categoria { get; set; } 
        public List<Amenidad> Amenidades { get; set; }
    }
}
