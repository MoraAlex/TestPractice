using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservaciones.Entidades
{
    public enum EstadoOcupacion
    {
        Vacio,
        Ocupado
    }

    public enum TipoHbitacion
    {
        Sencilla,
        Doble,
        Triple
    }

    public enum EstatusPago
    {
        Pagado,
        SinPagar
    }

    public enum Amenidad
    {
        Elevador = 1,
        Wifi = 2,
        AguaCaliente = 4,
        DesayunoIncluido = 8,
        Alberca = 16       
    }

    public enum Categoria
    {
        UnaEstrella,
        DosEstrellas,
        TresEstrellas,
        CuatroEstrellas,
        CincoEstrellas
    }
}
