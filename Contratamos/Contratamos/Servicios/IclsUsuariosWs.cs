using Contratamos.Models;

namespace Contratamos.Servicios
{
    public interface IclsUsuariosWs
    {
        Usuarios Login(string Usuario, string Pass);
        void GuardarUsuario(Usuarios usuarios);
        void ActualizarUsuario(Usuarios usuarios);
    }
}
