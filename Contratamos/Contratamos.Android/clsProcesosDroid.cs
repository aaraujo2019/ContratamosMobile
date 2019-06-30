using Contratamos.Models;
using Contratamos.Servicios;
using System.Collections.Generic;
using System.Data;

namespace Contratamos.Droid
{
    public class clsProcesosDroid : IclsProcesosWs
    {
        Profesiones profesiones;
        public clsProcesosDroid()
        {
            profesiones = new Profesiones();
        }

        public List<Profesiones> CargarProfesiones()
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

        public List<TipoUsuario> CargarTipoUsuario()
        {
            try
            {
                return Conexion.BaseDatos.CargarTipoUsuario();
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

        public DataSet ConsultarTipoUsuarioId(int idTipoUsuario)
        {
            try
            {
                return Conexion.BaseDatos.ConsultarTipoUsuarioId(idTipoUsuario);
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public void InsetarOfertaEmpleo(Ofertas ofertas)
        {
            try
            {
                Conexion.BaseDatos.GuardarOfertaEmpleo(ofertas);
            }
            catch (System.Exception)
            {
                throw;
            }
        }

    }
}