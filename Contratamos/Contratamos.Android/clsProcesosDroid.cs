using Contratamos.Servicios;
using System.Collections.Generic;
using System.Data;

namespace Contratamos.Droid
{
    public class clsProcesosDroid : IclsProcesosWs
    {
        Models.Profesiones profesiones;
        public clsProcesosDroid()
        {
            profesiones = new Models.Profesiones();
        }

        public List<Models.Profesiones> CargarProfesiones()
        {
            try
            {
                return Conexion.BaseDatos.CargarProfesiones();
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public DataSet CargarOfertas()
        {
            try
            {
                return Conexion.BaseDatos.CargarOfertas();
            }
            catch (System.Exception)
            {
                throw;
            }
        }

    }
}