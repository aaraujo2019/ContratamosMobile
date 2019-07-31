using Contratamos.Clases;
using Contratamos.Generales;
using Contratamos.Menu;
using Contratamos.Models;
using Contratamos.Servicios;
using Contratamos.Views;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Services;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace Contratamos.ViewModel
{
    public class vUsuarioViewModel : ViewModelBase
    {
        public INavigation Navigation { get; set; }
        public ICommand GuardarCommand { get; set; }

        private clsPrincipal clsPrincipal = new clsPrincipal();
        private MasterDetailPage MasterDetailPage;

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

        private string _verificar;
        public string Verificar
        {
            get { return _verificar; }
            set { SetProperty(ref _verificar, value); }
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
            GuardarCommand = new Command(Guardar);

            if (modGeneral.clsUsuario != null)
            {
                User = modGeneral.clsUsuario;
            }

            ListaTipoUsuarios = TipoUsuarioServices.ObtenerTipoUsuario().OrderBy(p => p.IdTipoUsuario).ToList();
        }


        private async void Guardar()
        {
            if (User.Nombre == null)
            {
                App.Current.MainPage.DisplayAlert("Contratámos", "Debe ingresar el nombre del usuario para poder continuar.", "Ok");
                return;
            }

            if (User.Apellido == null)
            {
                App.Current.MainPage.DisplayAlert("Contratámos", "Debe ingresar el apellido del usuario para poder continuar.", "Ok");
                return;
            }

            if (User.Usuario == null)
            {
                App.Current.MainPage.DisplayAlert("Contratámos", "Debe ingresar un usuario para el inicio de sesión.", "Ok");
                return;
            }

            if (User.Contraseña == null)
            {
                App.Current.MainPage.DisplayAlert("Contratámos", "Debe ingresar una contraseña para poder continuar.", "Ok");
                return;
            }
            else if (User.Contraseña != Verificar)
            {
                App.Current.MainPage.DisplayAlert("Contratámos", "Las contraseñas deben coincidir", "Ok");
                return;
            }
            
            if (User.IdTipoUsuario == -1)
            {
                App.Current.MainPage.DisplayAlert("Contratámos", "Debe seleccionar el tipo de usuario para continuar.", "Ok");
                return;
            }
           

            if (User.Email != null)
            {
                if (!modGeneral.email_bien_escrito(User.Email))
                {
                    App.Current.MainPage.DisplayAlert("Contratámos", "El email está mal eswcrito, por favor verifíquelo.", "Ok");
                    return;
                }
            }


            //byte[] vacio = new byte[0];
            //if (User.ArchivoCv == vacio)
            //{
            //    App.Current.MainPage.DisplayAlert("Contratámos", "Debe cargar su hoja de vida para continuar.", "Ok");
            //    return;
            //}

            if (User.IdUsuario != 0)
            {
                clsPrincipal.ActualizarUsuario(User);
                App.Current.MainPage.DisplayAlert("Contratámos", "Usuario actualizado con exito.", "Ok");
            }
            else
            {
                clsPrincipal.GuardarUsuario(User);
                App.Current.MainPage.DisplayAlert("Contratámos", "El usuario se ha ingresado con exito.", "Ok");
            }

            await Navigation.PushPopupAsync(new UserAnimationPage());
            MasterDetailPage = new MasterDetailPage
            {
                Master = new MenuPage(),
                Detail = new NavigationPage(new PagPrincipal()),
            };

            Application.Current.MainPage = MasterDetailPage;
            PopupNavigation.PopAsync();
        }

    }
}
