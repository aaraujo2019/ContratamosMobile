using Contratamos.Models;
using System;
using System.Collections.Generic;
using System.Data;

namespace Contratamos.Servicios
{
    public interface IclsProcesosWs
    {
        List<Profesiones> CargarProfesiones();
        List<TipoUsuario> CargarTipoUsuario();
        DataSet CargarOfertas();
        DataSet ConsultarTipoUsuarioId(int idTipoUsuario);
        void InsetarOfertaEmpleo(Ofertas ofertas);

    }
}
