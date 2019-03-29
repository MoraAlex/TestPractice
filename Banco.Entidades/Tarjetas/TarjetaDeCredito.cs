using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banco.Entidades
{
    public abstract class TarjetaDeCredito : ICuentaBancaria
    {
        public int NumeroDeCuenta { get; set; }
        public Usuario Titular { get; set; }
        public Moneda Balance { get; set; }
        public DateTime FechaDeCreacion { get; set; }
        public bool EstaActiva { get; set; }
        public int NIP { get; set; }

        public DateTime FechaDeCorte { get; set; }
        public decimal TasaDeInteres { get; set; }
    }
}
