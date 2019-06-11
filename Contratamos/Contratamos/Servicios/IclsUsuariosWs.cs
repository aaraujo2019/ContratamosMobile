using System;
using System.Collections.Generic;
using System.Text;

namespace Contratamos.Servicios
{
    public interface IclsUsuariosWs
    {
        Models.Usuarios Login(string Usuario, string Pass);
    }
}
