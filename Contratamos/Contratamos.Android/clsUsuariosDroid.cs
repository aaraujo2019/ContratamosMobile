using Contratamos.Servicios;
using System;
using System.Collections.Generic;

namespace Contratamos.Droid
{
    public class clsUsuariosDroid : IclsUsuariosWs
    {
        Models.Usuarios usuarios;

        public clsUsuariosDroid()
        {
            usuarios = new Models.Usuarios();
        }

        public Models.Usuarios Login(string Usuario, string Pass)
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

    }
}