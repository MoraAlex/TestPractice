using Banco.Entidades;

namespace Banco.Servicios
{
    public interface IRepositorioUsuarios
    {
        Usuario SeleccionarUsuario(string nombreDeUsuario);
    }
}