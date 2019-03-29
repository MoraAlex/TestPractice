using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Banco.Servicios;
using Banco.Entidades;
using Banco.Servicios.BasesDeDatos;
using Banco.Servicios.ServiciosDeTerceros;

namespace Banco.UnitTests
{
    [TestFixture]
    public class ServiciosDeCuentaDependientesTests
    {
        #region Login

        [Test]
        public void Login_UsuarioNoExiste_RegresaFalso()
        {
            //Arrange
            var objetoFalso = new Mock<IRepositorioUsuarios>();
            var nombreUsuario = "jhon1";
            var pass = "1234";
            objetoFalso.Setup(m => m.SeleccionarUsuario(nombreUsuario))
                .Returns((Usuario)null);
            var servicio = new ServiciosDeCuentaDependientes(objetoFalso.Object);
            //Act
            var resultado = servicio.Login(nombreUsuario, pass);
            //Asert
            Assert.IsFalse(resultado);
        }

        [Test]
        public void Login_UsuarioInactivo_RegresaFalso()
        {
            //Arrange
            var objetoFalso = new Mock<IRepositorioUsuarios>();
            var nombreUsuario = "Jhon1";
            var pass = "1234";
            objetoFalso.Setup(m => m.SeleccionarUsuario(nombreUsuario))
                .Returns(new Usuario()
                {
                    NombreDeUsuario = nombreUsuario,
                    EstaActivo = false,
                });
            var servicio = new ServiciosDeCuentaDependientes(objetoFalso.Object);
            //Act

            //Assert
            Assert.IsFalse(servicio.Login(nombreUsuario, pass));
        }

        [Test]
        public void Login_UsuarioActivoYPasswordIncorrecto_RegresaFalso()
        {
            var objetoFalso = new Mock<IRepositorioUsuarios>();
            var nombreUsuario = "Pedro";
            var pass = "1234";
            objetoFalso.Setup(m => m.SeleccionarUsuario(nombreUsuario))
                .Returns(new Usuario()
                {
                    NombreDeUsuario = nombreUsuario,
                    Password = pass,
                    EstaActivo = true
                });
            var servicio = new ServiciosDeCuentaDependientes(objetoFalso.Object);
            Assert.IsFalse(servicio.Login(nombreUsuario, "1111"));
        }

        [Test]
        public void Login_UsuarioActivoYPasswordCorrecto_RegresaVerdadero()
        {
            var objetoFalso = new Mock<IRepositorioUsuarios>();
            var nombreUsuario = "Salchi Chon";
            var pass = "1234";
            objetoFalso.Setup(m => m.SeleccionarUsuario(nombreUsuario))
                .Returns(new Usuario()
                {
                    NombreDeUsuario = nombreUsuario,
                    Password = pass,
                    EstaActivo = true
                });
            var servicio = new ServiciosDeCuentaDependientes(objetoFalso.Object);
            Assert.IsTrue(servicio.Login(nombreUsuario, pass));
        }

        #endregion Login

        #region SolicitarTarjeta
        [Test]
        public void SolicitarTarjeta_YaTieneTarjeta()
        {
            TarjetaClasica tarjetaClasica = new TarjetaClasica();
            TarjetaOro tarjetaOro = new TarjetaOro();
            TarjetaPlatino tarjetaPlatino = new TarjetaPlatino();
            TarjetaPlatino tarjetaNueva = new TarjetaPlatino();
            ServiciosDeCuentaDependientes servicios = new ServiciosDeCuentaDependientes();
            Usuario usuario = new Usuario()
            {
                Cuentas = new ICuentaBancaria[] { tarjetaClasica, tarjetaOro, tarjetaPlatino }
            };

            Assert.IsFalse(servicios.SolicitarTarjeta(usuario, tarjetaNueva));
        }

        [Test]
        public void SolicitarTarjeta_maximoTarjetas()
        {
            var usuario = new Usuario()
            {
                Cuentas = new ICuentaBancaria[]
                {
                    new TarjetaClasica(),
                    new TarjetaPlatino()
                }
            };
            var tarjeta = new TarjetaOro();
            var objetoFalse = new Mock<IRepositorioConfiguraciones>();
            objetoFalse.Setup(m => m.SeleccionarMaximoDeTarjetasPorUsuario()).Returns(2);
            var servicios = new ServiciosDeCuentaDependientes(objetoFalse.Object);

            Assert.IsFalse(servicios.SolicitarTarjeta(usuario, tarjeta));
        }

        [Test]
        public void SolicitarTarjeta_BuroOro()
        {
            var usuario = new Usuario()
            {
                Cuentas = new ICuentaBancaria[]
                {
                    new TarjetaClasica(),
                    new TarjetaPlatino()
                }
            };
            var RFC = "4F8";
            var tarjeta = new TarjetaOro();
            var objetoBuro = new Mock<IServicioExternoBuro>();
            objetoBuro.Setup(b => b.ConsultarBuro(RFC)).Returns(60);
            var objetoConf = new Mock<IRepositorioConfiguraciones>();
            objetoConf.Setup(c => c.SeleccionarMaximoDeTarjetasPorUsuario()).Returns(3);
            var servicios = new ServiciosDeCuentaDependientes(objetoBuro.Object, objetoConf.Object);
            Assert.IsFalse(servicios.SolicitarTarjeta(usuario, tarjeta));
        }

        [Test]
        public void SolicitarTarjeta_BuroPlata()
        {
            var usuario = new Usuario()
            {
                Cuentas = new ICuentaBancaria[]
                {
                    new TarjetaClasica(),
                    new TarjetaOro()
                }
            };
            var RFC = "4F8";
            var tarjeta = new TarjetaPlatino();
            var objetoBuro = new Mock<IServicioExternoBuro>();
            objetoBuro.Setup(b => b.ConsultarBuro(RFC)).Returns(80);
            var objetoConf = new Mock<IRepositorioConfiguraciones>();
            objetoConf.Setup(c => c.SeleccionarMaximoDeTarjetasPorUsuario()).Returns(3);
            var servicios = new ServiciosDeCuentaDependientes(objetoBuro.Object, objetoConf.Object);
            Assert.IsFalse(servicios.SolicitarTarjeta(usuario, tarjeta));
        }

        [Test]
        public void SolicitarTarjeta_BuroClasic()
        {
            var usuario = new Usuario()
            {
                Cuentas = new ICuentaBancaria[]
                {
                    new TarjetaOro(),
                    new TarjetaPlatino()
                }
            };
            var RFC = "4F8";
            var tarjeta = new TarjetaClasica();
            var objetoBuro = new Mock<IServicioExternoBuro>();
            objetoBuro.Setup(b => b.ConsultarBuro(RFC)).Returns(25);
            var objetoConf = new Mock<IRepositorioConfiguraciones>();
            objetoConf.Setup(c => c.SeleccionarMaximoDeTarjetasPorUsuario()).Returns(3);
            var servicios = new ServiciosDeCuentaDependientes(objetoBuro.Object, objetoConf.Object);
            Assert.IsFalse(servicios.SolicitarTarjeta(usuario, tarjeta));
        }


        #endregion SolicitarTarjeta

        #region TransferirATerceros
        [Test]
        public void TransferirATerceros_CuentaExt()
        {
            ICuentaBancaria origen = new CuentaDeAhorro();
            ICuentaBancaria destino = new CuentaDeAhorro();
            Moneda moneda = new Moneda(100, Divisa.MXN);
            ServiciosDeCuentaDependientes servicios = new ServiciosDeCuentaDependientes();
            void myFunc() => servicios.TransferirATerceros(origen, destino, moneda);
            Assert.Throws<InvalidOperationException>(myFunc);
        }

        [Test]
        public void TransferirATerceros_DifDiv()
        {
            ICuentaBancaria origen = new CuentaDeAhorro()
            {
                Balance = new Moneda(100, Divisa.EUR)
            };
            ICuentaBancaria destino = new CuentaDeAhorroExterna()
            {
                Balance = new Moneda(100, Divisa.MXN)
            };
            Moneda moneda = new Moneda(100, Divisa.MXN);
            ServiciosDeCuentaDependientes servicios = new ServiciosDeCuentaDependientes();
            void myFunc() => servicios.TransferirATerceros(origen, destino, moneda);
            Assert.Throws<InvalidOperationException>(myFunc);
        }

        [Test]
        public void TransferirATerceros_DifID()
        {
            ICuentaBancaria origen = new CuentaDeAhorro()
            {
                Titular = new Usuario()
                {
                    Id = 1
                },
                Balance = new Moneda(100, Divisa.USD)
            };
            ICuentaBancaria destino = new CuentaDeAhorroExterna()
            {
                Titular = new Usuario()
                {
                    Id = 2
                },
                Balance = new Moneda(100, Divisa.USD)
            };
            Moneda moneda = new Moneda(100, Divisa.USD);
            ServiciosDeCuentaDependientes servicios = new ServiciosDeCuentaDependientes();
            void myFunc() => servicios.TransferirATerceros(origen, destino, moneda);
            Assert.Throws<InvalidOperationException>(myFunc);
        }

        #endregion TransferirATerceros

        #region ConvertirMoneda


        #endregion ConvertirMoneda

        #region ConvertirPesosADolares


        #endregion ConvertirPesosADolares

        #region ConvertirPesosAEuros


        #endregion ConvertirPesosAEuros
    }
}
