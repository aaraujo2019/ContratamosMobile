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
        int InsetarOfertaEmpleo(Ofertas ofertas);
        DataSet BuscarOfertasPorId(int idOferta);
        void ActualizarOferta(Ofertas ofertas);
        void GuardarAplicacion(int idOferta, int idUsuario, string IdDispositivo);
        DataSet BuscarProfesionPorId(int idProfesion);
        int GuardarProfesion(string Descripcion);
        void ActualizarProfesion(Profesiones profesiones);
    }
}
