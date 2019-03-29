using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Banco.Entidades;
using Banco.Servicios.ServiciosDeTerceros;

namespace Banco.Servicios
{
    public class ServiciosDeCuentaLocal
    {
        public ServiciosDeCuentaLocal()
        {

        }

        public void CambiarNIP(ICuentaBancaria cuenta, int nip, int verificarNip)
        {
            //cuenta de otro banco, operacion invalida
            if (cuenta is CuentaDeAhorroExterna)
            {
                throw new InvalidOperationException("No se puede cambiar NIP de otro banco");
            }

            //NIP debe ser de 4 digitos
            //Puede verse el error?
            if(nip < 999 || verificarNip < 999)
            {
                throw new InvalidOperationException("NIP debe ser de 4 digitos");
            }
            if (nip > 9999 || verificarNip > 999)
            {
                throw new InvalidOperationException("NIP debe ser de 4 digitos");
            }

            //verificar que ambos nips son iguales
            if(nip != verificarNip)
            {
                throw new InvalidOperationException("NIPs distintos.");
            }

            cuenta.NIP = nip;
        }

        public void RetirarDeCajero(ICuentaBancaria origen, int NIP, decimal cantidad)
        {
            decimal comision = 0;

            //el banco solo tiene cajeros en pesos
            if (origen.Balance.Divisa != Divisa.MXN)
            {
                throw new InvalidOperationException("Solo MXN permitidos.");
            }

            //validar NIP
            if (origen.NIP != NIP)
            {
                throw new InvalidOperationException("NIP incorrecto.");
            }

            //cuenta de otro banco, cobrar $30 de comision
            if(origen is CuentaDeAhorroExterna)
            {
                comision = 30;
            }

            //si es tarjeta de credito, cobrar 6% de comision
            if (origen is TarjetaDeCredito)
            {
                comision = cantidad * 0.06m;
            }

            var total = new Moneda(cantidad + comision, Divisa.MXN);
            origen.Balance = (Moneda)origen.Balance.Restar(total);
        }     

        public void TransferirEntreCuentasPropias(ICuentaBancaria origen, ICuentaBancaria destino, Moneda cantidad)
        {
            if(origen.Balance.Divisa != destino.Balance.Divisa)
            {
                throw new InvalidOperationException("Divisas diferentes");
            }
            if(origen.Titular.Id != destino.Titular.Id)
            {
                throw new InvalidOperationException("Usuarios diferentes");
            }

            origen.Balance.Restar(cantidad);
            destino.Balance.Agregar(cantidad);
        }

        public void PagarTarjeta(ICuentaBancaria cuentaOrigen, TarjetaDeCredito tarjeta, Moneda cantidad)
        {
            if (cuentaOrigen.Balance.Divisa != tarjeta.Balance.Divisa)
            {
                throw new InvalidOperationException("Divisas diferentes");
            }
            if (cuentaOrigen.Titular.Id != tarjeta.Titular.Id)
            {
                throw new InvalidOperationException("Las cuentas de transferencia son de usuarios diferentes");
            }

            //generar comision de 5% si se paga desde otra TDC
            if( !(cuentaOrigen is CuentaDeAhorro) )
            {
                FuncionesComunes.RestarCantidad(cuentaOrigen, new Moneda(cantidad.Cantidad * (decimal)0.05, cantidad.Divisa));
            }

            //generar comision de 10% si se paga despues de fecha de corte
            var hoy = DateTime.Now;
            if (new DateTime(hoy.Year, hoy.Month, tarjeta.FechaDeCorte.Day) < DateTime.Now)
            {
                FuncionesComunes.RestarCantidad(cuentaOrigen, new Moneda(cantidad.Cantidad * 0.10m, cantidad.Divisa));
            }

            cuentaOrigen.Balance.Restar(cantidad);
            tarjeta.Balance.Agregar(cantidad);
        }       
        
    }
}
