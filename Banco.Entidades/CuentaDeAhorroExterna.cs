using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banco.Entidades
{
    public class CuentaDeAhorroExterna : CuentaDeAhorro
    {
        public string Banco { get; set; }
    }
}
