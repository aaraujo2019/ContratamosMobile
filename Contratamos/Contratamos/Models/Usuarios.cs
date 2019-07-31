namespace Contratamos.Models
{
    public class Usuarios
    {
        public int IdUsuario { set; get; }
        public string Nombre { set; get; }
        public string Apellido { set; get; }
        public string Usuario { set; get; }
        public string Contraseña { set; get; }
        public string Email { set; get; }
        public int IdTipoUsuario { set; get; }
        public byte[] ArchivoCv { set; get; }
        public string Celular { set; get; }
    }
}
