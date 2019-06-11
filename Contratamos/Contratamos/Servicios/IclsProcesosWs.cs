using System;
using System.Collections.Generic;
using System.Data;

namespace Contratamos.Servicios
{
    public interface IclsProcesosWs
    {
        List<Models.Profesiones> CargarProfesiones();

        DataSet CargarOfertas();
    }
}
