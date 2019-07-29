using Contratamos.Clases;
using Contratamos.Generales;
using Contratamos.Menu;
using Contratamos.Views;
using Plugin.DeviceInfo;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Services;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace Contratamos.ViewModel
{
    public class InicioViewModel :  ViewModelBase
    {
        #region Commands
        public INavigation Navigation { get; set; }
        public ICommand InicioCommand { get; set; }

        #endregion

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

        private MasterDetailPage MasterDetailPage;

        public InicioViewModel()
        {
            InicioCommand = new Command(Inicio);
            Titulo = modGeneral.NombreAplicacion;
            modGeneral.IDEstacion = string.Concat("ID Dispositivo: ", CrossDeviceInfo.Current.Id);
        }

        private async void Inicio()
        {
            try
            {
                Title = string.Empty;
                await Navigation.PushPopupAsync(new UserAnimationPage());
                MasterDetailPage = new MasterDetailPage
                {
                    Master = new MenuPage(),
                    Detail = new NavigationPage(new PagPrincipal()),
                };

                Application.Current.MainPage = MasterDetailPage;
                PopupNavigation.PopAsync();
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Contratámos - Inicio", "Ha ocurrido un problema al validar el usuario por favor verifíque que todo esté correcto.", "Ok");
            }
        }
    }
}
