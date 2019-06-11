using System;

namespace Contratamos.Models
{
    public class Ofertas
    {
        public  int IdOferta { set; get; }
        public  string Titulo { set; get; }
        public  string DescripcionOferta { set; get; }
        public  double Salario { set; get; }
        public  DateTime OfertaDesde { set; get; }
        public  DateTime OfertaHasta { set; get; }
        public  int IdProfesion { set; get; }
        public  int IdUsuario { set; get; }
        public  int IdEstado { set; get; }
        public  string IdDispositivo { set; get; }
    }
}
