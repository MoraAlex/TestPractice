using Banco.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banco.Servicios
{
    public static class FuncionesComunes
    {
        public static void RestarCantidad(ICuentaBancaria destino, Moneda cantidad)
        {
            if (destino.Balance.Divisa != cantidad.Divisa)
            {
                throw new InvalidOperationException("Divisas diferentes");
            }
            destino.Balance.Restar(cantidad);
        }

    }
}
