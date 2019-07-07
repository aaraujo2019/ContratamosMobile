using Contratamos.Models;
using System.Data;

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

        public int InsertarOferta(Ofertas oferta)
        {
           return App.objWSProcesos.InsetarOfertaEmpleo(oferta);
        }

        public DataSet BuscarOfertasPorId(int idOferta)
        {
            return App.objWSProcesos.BuscarOfertasPorId(idOferta);
        }

        public void ActualizarOferta(Ofertas ofertas)
        {
            App.objWSProcesos.ActualizarOferta(ofertas);
        }

    }
}
