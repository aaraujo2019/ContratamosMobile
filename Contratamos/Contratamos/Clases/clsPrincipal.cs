using System.Collections.Generic;

namespace Contratamos.Clases
{
    public class clsPrincipal
    {
        public Models.Usuarios ValidarUsuario(string Usuario, string Pass)
        {
            return App.objWSUsuarios.Login(Usuario, Pass);
        }

    }
}
