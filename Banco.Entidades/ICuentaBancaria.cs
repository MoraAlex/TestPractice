using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banco.Entidades
{
    public interface ICuentaBancaria
    {
        int NumeroDeCuenta { get; set; }
        Usuario Titular { get; set; }
        Moneda Balance { get; set; }  
            
        DateTime FechaDeCreacion { get; set; }
        bool EstaActiva { get; set; }
        int NIP { get; set; }
    }
}
