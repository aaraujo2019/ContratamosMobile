
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

        public void InsetarOfertaEmpleo(Ofertas ofertas)
        {
            objProcesosWs.InsetarOfertaEmpleo(ofertas);
        }
    }
}
