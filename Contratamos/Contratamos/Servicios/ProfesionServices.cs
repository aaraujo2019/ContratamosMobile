using Contratamos.Models;
using System.Collections.Generic;

namespace Contratamos.Servicios
{
    public class ProfesionServices
    {
        public static List<Profesiones> ObtenerProfesiones()
        {
             return App.objWSProcesos.CargarProfesiones();
        }
    }
}
