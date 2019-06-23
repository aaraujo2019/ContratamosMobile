using Contratamos.Models;
using Contratamos.Servicios;
using System;

namespace Contratamos.Droid
{
    public class clsUsuariosDroid : IclsUsuariosWs
    {
        Usuarios usuarios;

        public clsUsuariosDroid()
        {
            usuarios = new Usuarios();
        }

        public Usuarios Login(string Usuario, string Pass)
        {
            try
            {
               return Conexion.BaseDatos.ValidarUsuarioConexionAcceso(Usuario, Pass);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void GuardarUsuario(Usuarios usuarios)
        {
            try
            {
                Conexion.BaseDatos.GuardarUsuario(usuarios);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void ActualizarUsuario(Usuarios usuarios)
        {
            try
            {
                Conexion.BaseDatos.ActualizarUsuario(usuarios);
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}