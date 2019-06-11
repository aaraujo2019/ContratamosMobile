
namespace Contratamos.Servicios
{
    public class clsUsuariosWs
    {
        IclsUsuariosWs objUsuario;
        
        public clsUsuariosWs(IclsUsuariosWs usuariosWs)
        {
            objUsuario = usuariosWs;
        }

        public Models.Usuarios Login(string Usuario, string Pass)
        {
            return objUsuario.Login(Usuario, Pass);
        }

    }
}
