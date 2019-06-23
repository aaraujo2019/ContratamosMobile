
using Contratamos.Models;

namespace Contratamos.Servicios
{
    public class clsUsuariosWs
    {
        IclsUsuariosWs objUsuario;
        
        public clsUsuariosWs(IclsUsuariosWs usuariosWs)
        {
            objUsuario = usuariosWs;
        }

        public Usuarios Login(string Usuario, string Pass)
        {
            return objUsuario.Login(Usuario, Pass);
        }

        public void GuardarUsuario(Usuarios usuarios)
        {
            objUsuario.GuardarUsuario(usuarios);
        }

        public void ActualizarUsuario(Usuarios usuarios)
        {
            objUsuario.ActualizarUsuario(usuarios);
        }

    }
}
