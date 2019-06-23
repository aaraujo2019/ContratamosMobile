using Contratamos.Models;

namespace Contratamos.Clases
{
    public class clsPrincipal
    {
        public Models.Usuarios ValidarUsuario(string Usuario, string Pass)
        {
            return App.objWSUsuarios.Login(Usuario, Pass);
        }

        public void GuardarUsuario(Usuarios usuario)
        {
            App.objWSUsuarios.GuardarUsuario(usuario);
        }

        public void ActualizarUsuario(Usuarios usuario)
        {
            App.objWSUsuarios.ActualizarUsuario(usuario);
        }

    }
}
