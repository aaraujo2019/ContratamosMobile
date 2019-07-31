
using Contratamos.Models;
using System.Collections.Generic;
using System.Data;

namespace Contratamos.Servicios
{
    public class clsProcesosWs
    {
        IclsProcesosWs objProcesosWs;

        public clsProcesosWs(IclsProcesosWs procesosWs)
        {
            objProcesosWs = procesosWs;
        }

        public List<Models.Profesiones> CargarProfesiones()
        {
            return objProcesosWs.CargarProfesiones();
        }

        public List<TipoUsuario> CargarTipoUsuario()
        {
            return objProcesosWs.CargarTipoUsuario();
        }

        public DataSet CargarOfertas()
        {
            return objProcesosWs.CargarOfertas();
        }
        
        public DataSet ConsultarTipoUsuarioId(int idTipoUsuario)
        {
            return objProcesosWs.ConsultarTipoUsuarioId(idTipoUsuario);
        }

        public int InsetarOfertaEmpleo(Ofertas ofertas)
        {
            return objProcesosWs.InsetarOfertaEmpleo(ofertas);
        }

        public DataSet BuscarOfertasPorId(int idOferta)
        {
            return objProcesosWs.BuscarOfertasPorId(idOferta);
        }

        public void ActualizarOferta(Ofertas ofertas)
        {
            objProcesosWs.ActualizarOferta(ofertas);
        }

        public void GuardarAplicacion(int idOferta, int idUsuario, string IdDispositivo)
        {
            objProcesosWs.GuardarAplicacion(idOferta, idUsuario, IdDispositivo);
        }

        public DataSet BuscarProfesionPorId(int idProfesion)
        {
            return objProcesosWs.BuscarProfesionPorId(idProfesion);
        }

        public int GuardarProfesion(string Descripcion)
        {
            return objProcesosWs.GuardarProfesion(Descripcion);
        }

        public void ActualizarProfesion(Profesiones profesiones)
        {
            objProcesosWs.ActualizarProfesion(profesiones);
        }

        public DataSet FiltrarOferta(int opcion, string texto)
        {
            return objProcesosWs.FiltrarOferta(opcion, texto);
        }

        public Usuarios ConsultarusuarioPorID(int pIdUsuario)
        {
            return objProcesosWs.ConsultarusuarioPorID(pIdUsuario);
        }

        public List<Aplicaciones> CargarAplcaciones()
        {
            return objProcesosWs.CargarAplcaciones();
        }
    }
}
