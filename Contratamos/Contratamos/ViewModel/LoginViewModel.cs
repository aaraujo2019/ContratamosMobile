using Contratamos.Generales;
using Contratamos.Models;
using Contratamos.Views;
using Plugin.DeviceInfo;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Services;
using System;
using System.Windows.Input;
using Xamarin.Forms;
using Contratamos.Clases;

namespace Contratamos.ViewModel
{
    public class LoginViewModel : ViewModelBase
    {
        #region Commands
        public INavigation Navigation { get; set; }
        public ICommand LoginCommand { get; set; }

        #endregion

        #region Properties
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

        private string _tituloMnu;
        public string TituloMenu
        {
            get { return _tituloMnu; }
            set { SetProperty(ref _tituloMnu, value); }
        }

        #endregion

        clsPrincipal clsPrincipal = new clsPrincipal();

        public LoginViewModel()
        {
            LoginCommand = new Command(Login);
            User.Usuario = null;
            User.Contraseña = null;

            Titulo = modGeneral.NombreAplicacion;            
            modGeneral.IDEstacion = string.Concat("ID Dispositivo: ", CrossDeviceInfo.Current.Id);
        }


        private async void Login()
        {
            Title = string.Empty;
            try
            {
                if (User.Usuario != null)
                {
                    if (User.Contraseña != null)
                    {
                        await Navigation.PushPopupAsync(new UserAnimationPage());
                        ValidarUsuario(User.Usuario, User.Contraseña);
                    }
                    else Message = "La contraseña es requerida";
                    PopupNavigation.PopAsync();
                }
                else Message = "El usuario es requerido";
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Contratámos", "Ha ocurrido un problema al validar el usuario por favor verifíque que todo esté correcto.", "Ok");
            }
        }

        private async void ValidarUsuario(string _usuario, string _pass)
        {
            string _Respuesta = string.Empty;
            var restorno = clsPrincipal.ValidarUsuario(_usuario, _pass);

            if (restorno != null)
            {
                modGeneral.usuario = _usuario;
                await Navigation.PushAsync(new PagPrincipal());
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Login", _Respuesta, "Ok");
            }
        }


    }
}
