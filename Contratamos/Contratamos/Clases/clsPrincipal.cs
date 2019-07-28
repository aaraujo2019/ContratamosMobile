using Contratamos.Models;
using System.Data;

namespace Contratamos.Clases
{
    public class clsPrincipal
    {
        public Usuarios ValidarUsuario(string Usuario, string Pass)
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
        
        public void GuardarAplicacion(int idOferta, int idUsuario, string IdDispositivo)
        {
            App.objWSProcesos.GuardarAplicacion(idOferta, idUsuario, IdDispositivo);
        }

        public DataSet BuscarProfesionPorId(int idProfesion)
        {
            return App.objWSProcesos.BuscarProfesionPorId(idProfesion);
        }

        public int GuardarProfesion(string Descripcion)
        {
            return App.objWSProcesos.GuardarProfesion(Descripcion);
        }

        public void ActualizarProfesion(Profesiones profesiones)
        {
            App.objWSProcesos.ActualizarProfesion(profesiones);
        }
    }
}
