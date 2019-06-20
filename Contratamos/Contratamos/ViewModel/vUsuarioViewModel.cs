using Contratamos.Generales;
using Contratamos.Models;
using Contratamos.Servicios;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace Contratamos.ViewModel
{
    public class vUsuarioViewModel : ViewModelBase
    {
        public INavigation Navigation { get; set; }

        private Usuarios _user = new Usuarios();
        public Usuarios User
        {
            get { return _user; }
            set { SetProperty(ref _user, value); }
        }

        private string _message;
        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }

        private string _titulo;
        public string Titulo
        {
            get { return _titulo; }
            set { SetProperty(ref _titulo, value); }
        }

        public List<TipoUsuario> ListaTipoUsuarios { set; get; }

        private TipoUsuario _selectedTipoUsuario;
        public TipoUsuario SelectedTipoUsuario
        {
            get
            {
                return _selectedTipoUsuario;
            }
            set
            {
                SetProperty(ref _selectedTipoUsuario, value);
            }
        }
        private string _tipoUsuarioText;
        public string TipoUsuarioText
        {
            get
            {
                return _tipoUsuarioText;
            }
            set
            {
                SetProperty(ref _tipoUsuarioText, value);
            }
        }

        public vUsuarioViewModel()
        {
            Titulo = modGeneral.NombreAplicacion;

            if (modGeneral.clsUsuario != null)
                User = modGeneral.clsUsuario;
            
            ListaTipoUsuarios = TipoUsuarioServices.ObtenerTipoUsuario().OrderBy(p => p.IdTipoUsuario).ToList();
        }
    }
}
