using Contratamos.Models;
using System.Collections.Generic;
using System.Data;

namespace Contratamos.Servicios
{
    public class TipoUsuarioServices
    {
        public static List<TipoUsuario> ObtenerTipoUsuario()
        {
            return App.objWSProcesos.CargarTipoUsuario();
        }

        public static DataSet ConsultarTipoUsuarioId(int idTipoUsuario)
        {
            return App.objWSProcesos.ConsultarTipoUsuarioId(idTipoUsuario);
        }
    }
}
