
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

        public DataSet CargarOfertas()
        {
            return objProcesosWs.CargarOfertas();
        }

    }
}
