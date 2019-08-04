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

        public int InsetarOfertaEmpleo(Ofertas ofertas)
        {
            try
            {
                return Conexion.BaseDatos.GuardarOfertaEmpleo(ofertas);
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public DataSet BuscarOfertasPorId(int idOferta)
        {
            try
            {
                return Conexion.BaseDatos.BuscarOfertasPorId(idOferta);
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public void ActualizarOferta(Ofertas ofertas)
        {
            try
            {
                Conexion.BaseDatos.ActualizarOferta(ofertas);
            }
            catch (System.Exception)
            {
                throw;
            }
        }


        public void GuardarAplicacion(int idOferta, int idUsuario, string IdDispositivo)
        {
            try
            {
                Conexion.BaseDatos.GuardarAplicacion(idOferta, idUsuario, IdDispositivo);
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public DataSet BuscarProfesionPorId(int idProfesion)
        {
            try
            {
                return Conexion.BaseDatos.BuscarProfesionPorId(idProfesion);
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public int GuardarProfesion(string Descripcion)
        {
            try
            {
                return Conexion.BaseDatos.GuardarProfesion(Descripcion);
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        public void ActualizarProfesion(Profesiones profesiones)
        {
            try
            {
                Conexion.BaseDatos.ActualizarProfesion(profesiones);
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        public DataSet FiltrarOferta(int opcion, string texto)
        {
            try
            {
                return Conexion.BaseDatos.FiltrarOferta(opcion, texto);
            }
            catch (System.Exception)
            {

                throw;
            }
        }


        public Usuarios ConsultarusuarioPorID(int pIdUsuario)
        {
            try
            {
                return Conexion.BaseDatos.ConsultarusuarioPorID(pIdUsuario);
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        public List<Aplicaciones> CargarAplcaciones()
        {
            try
            {
                return Conexion.BaseDatos.CargarAplcaciones();
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        public List<Ciudades> CargarCiudades()
        {
            try
            {
                return Conexion.BaseDatos.CargarCiudades();
            }
            catch (System.Exception)
            {

                throw;
            }
        }

    }
}