using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banco.Servicios.ServiciosDeTerceros
{
    public interface IServicioExternoSPEI
    {
        bool EnviarSpei(string banco, string CLABE, decimal cantidad);
    }
}
