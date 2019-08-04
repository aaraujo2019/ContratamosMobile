using Contratamos.Models;
using System.Collections.Generic;

namespace Contratamos.Servicios
{
    public class clsCiudadesWs
    {
        public static List<Ciudades> CargarCiudades()
        {
            return App.objWSProcesos.CargarCiudades();
        }
    }
}
