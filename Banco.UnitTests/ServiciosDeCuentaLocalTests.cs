using Banco.Entidades;
using Banco.Servicios;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banco.UnitTests
{
    [TestFixture]
    public class ServiciosDeCuentaLocalTests
    {
        #region Cambiar NIP
        [Test]
        public void CambiarNIP_ACuentaDeAhorroExterna_LanzaExcepcion()
        {
            //Arrange
            var cuentaBancaria = new CuentaDeAhorroExterna();
            var servicio = new ServiciosDeCuentaLocal();
            var nipInicial = 1234;
            var nipConf = 1234;
            //Act
            void metodoPrueba() => servicio.CambiarNIP(cuentaBancaria, nipInicial, nipConf);
            //Assert

            Assert.Throws<InvalidOperationException>(metodoPrueba);
        }

        [Test]
        public void CambiarNIP_NIPMenorACuatroDigitos_LanzaExcepcion()
        {
            //Arrange
            var cuenta = new CuentaDeAhorro();
            var servicio = new ServiciosDeCuentaLocal();
            var nipInicial = 696969696;
            var nipConf = 1234;

            //Act
            void prueba() => servicio.CambiarNIP(cuenta, nipInicial, nipConf);
            //Assert
            Assert.Throws<InvalidOperationException>(prueba);
        }

        [Test]
        public void CambiarNIP_NIPMayorACuatroDigitos_LanzaExcepcion()
        {
            //Arrange
            var cuenta = new CuentaDeAhorro();
            var servicio = new ServiciosDeCuentaLocal();
            var nipInicial = 1234;
            var nipConf = 123;

            //Act
            void prueba() => servicio.CambiarNIP(cuenta, nipInicial, nipConf);
            //Assert
            Assert.Throws<InvalidOperationException>(prueba);
        }

        [Test]
        public void CambiarNIP_NIPsDiferentes_LanzaExcepcion()
        {

        }

        [Test]
        public void CambiarNIP_NIPsvalidos_ActualizaCuentaConNuevoNIP()
        {

        }

        #endregion Cambiar NIP

        #region RetirarDeCajero

        [Test]
        public void RetirarDeCajero_OrigenDivisaMxn()
        {
            var moneda = new Moneda(105.3M, Divisa.EUR);
            var cuentaBancaria = new CuentaDeAhorro()
            {
                Balance = moneda
            };
            int NIP = 1234;
            decimal cantidad = 105.3M;
            void myFunc() => servicios.RetirarDeCajero(cuentaBancaria, NIP, cantidad);
            Assert.Throws<InvalidOperationException>(myFunc);
        }

        [Test]
        public void RetirarDeCajero_NipDiferente()
        {
            var cuentaBancaria = new CuentaDeAhorro()
            {
                NIP = 1234
            };
            var nipParameter = 1111;
            void myFunc() => servicios.RetirarDeCajero(cuentaBancaria, nipParameter, 105.3M);
            Assert.AreNotEqual(cuentaBancaria.NIP, nipParameter);
        }

        [Test]
        public void RetirarDeCajero_CuentaDeAhorroExterna()
        {
            decimal cantidad = 70M;
            var moneda = new Moneda(150M, Divisa.MXN);
            var cuentaBancaria = new CuentaDeAhorroExterna()
            {
                Balance = moneda,
                NIP = 1111
            };
            var monedaRestar = new Moneda(cuentaBancaria.Balance.Cantidad - cantidad - 30,
                Divisa.MXN);
            servicios.RetirarDeCajero(cuentaBancaria, 1111, cantidad);
            Assert.AreEqual(monedaRestar, cuentaBancaria.Balance);
        }

        [Test]
        public void RetirarDeCajero_TarjetaDeCredito()
        {
            decimal cantidad = 70M;
            decimal comision = cantidad * .06M;
            var moneda = new Moneda(150M, Divisa.MXN);
            var monedaResta = new Moneda(150 - cantidad - comision, Divisa.MXN);
            var cuentaBancaria = new TarjetaClasica()
            {
                NIP = 1111,
                Balance = moneda
            };
            servicios.RetirarDeCajero(cuentaBancaria, 1111, cantidad);
            Assert.AreEqual(monedaResta, cuentaBancaria.Balance);
        }

        ServiciosDeCuentaLocal servicios = new ServiciosDeCuentaLocal();


        #endregion RetirarDeCajero

        #region TransferirEntreCuentasPropias
        [Test]
        public void TransferirEntreCuentasPropias_DifDivisas()
        {
            CuentaDeAhorro cuentaOrigen = new CuentaDeAhorro()
            {
                Balance = new Moneda(100M, Divisa.MXN)
            };
            CuentaDeAhorro cuentaDestino = new CuentaDeAhorro()
            {
                Balance = new Moneda(100M, Divisa.USD)
            };
            void myFunc() => servicios.TransferirEntreCuentasPropias(cuentaOrigen, cuentaDestino,
                new Moneda(100, Divisa.MXN));
            Assert.Throws<InvalidOperationException>(myFunc);
        }

        [Test]
        public void TransferirEntreCuentasPropias_DifUsuarios()
        {
            CuentaDeAhorro cuentaOrigen = new CuentaDeAhorro()
            {
                Titular = new Usuario()
                {
                    Id = 1
                },
                Balance = new Moneda(100, Divisa.MXN)
            };

            CuentaDeAhorro cuentaDestino = new CuentaDeAhorro()
            {
                Titular = new Usuario()
                {
                    Id = 2
                },
                Balance = new Moneda(100, Divisa.MXN)
            };
            void myFunc() => servicios.TransferirEntreCuentasPropias(cuentaOrigen, cuentaDestino, new Moneda(100M, Divisa.MXN));
            Assert.Throws<InvalidOperationException>(myFunc);
        }




        #endregion TransferirEntreCuentasPropias

        #region PagarTarjeta
        [Test]
        public void PagarTarjeta_DifDiv()
        {
            CuentaDeAhorro cuentaOrigen = new CuentaDeAhorro()
            {
                Balance = new Moneda(100M, Divisa.USD)
            };
            TarjetaClasica tarjeta = new TarjetaClasica()
            {
                Balance = new Moneda(100M, Divisa.MXN)
            };
            Moneda cantidad = new Moneda(100M, Divisa.EUR);
            void myFunc() => servicios.PagarTarjeta(cuentaOrigen, tarjeta, cantidad);
            Assert.Throws<InvalidOperationException>(myFunc);
        }

        [Test]
        public void PagarTarjeta_DifId()
        {
            CuentaDeAhorro cuentaOrigen = new CuentaDeAhorro()
            {
                Titular = new Usuario()
                {
                    Id = 1
                }
            };
            TarjetaClasica tarjetaClasica = new TarjetaClasica()
            {
                Titular = new Usuario()
                {
                    Id = 2
                }
            };
            void myFunc() => servicios.PagarTarjeta(cuentaOrigen, tarjetaClasica, new Moneda(100M, Divisa.CHF));
        }


        #endregion PagarTarjeta
    }
}
