﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banco.Servicios.BasesDeDatos
{
    public interface IRepositorioConfiguraciones
    {
        int SeleccionarMaximoDeTarjetasPorUsuario();
    }
}