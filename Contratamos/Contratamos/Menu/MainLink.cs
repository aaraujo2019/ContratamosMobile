using Contratamos.Generales;
using Contratamos.Views;
using System;
using Xamarin.Forms;

namespace Contratamos.Menu
{
    public class MainLink : Checkbox
    {
        private int OpcionSeleccionada = 0;
        private MasterDetailPage MasterDetailPage;

        public MainLink(string name, int opcion)
        {
            Text = name;
            Clicked += new EventHandler(OnClicked);
            OpcionSeleccionada = opcion;
        }

        public void OnClicked(object sender, EventArgs e)
        {
            Checked = !Checked;
            MetodoPrincipal(OpcionSeleccionada);
        }

        private async void MetodoPrincipal(int opcion)
        {
            switch (opcion)
            {
                case 5:

                    if (modGeneral.clsUsuario != null)
                    {
                        MasterDetailPage = null;
                        MasterDetailPage = new MasterDetailPage
                        {
                            Master = new MenuPage(),
                            Detail = new NavigationPage(new Profesiones()),
                        };

                        App.Current.MainPage = MasterDetailPage;
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("Contratámos", "Para poder acceder a esta opción de iniciar sesión primero.", "Ok");
                        MasterDetailPage = null;
                        MasterDetailPage = new MasterDetailPage
                        {
                            Master = new MenuPage(),
                            Detail = new NavigationPage(new Login()),
                        };

                        App.Current.MainPage = MasterDetailPage;
                    }
 
                    break;

                case 4:

                    if (modGeneral.clsUsuario != null)
                    {
                        MasterDetailPage = null;
                        MasterDetailPage = new MasterDetailPage
                        {
                            Master = new MenuPage(),
                            Detail = new NavigationPage(new OfertasEmpleos()),
                        };

                        App.Current.MainPage = MasterDetailPage;
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("Contratámos", "Para poder acceder a esta opción de iniciar sesión primero.", "Ok");
                        MasterDetailPage = null;
                        MasterDetailPage = new MasterDetailPage
                        {
                            Master = new MenuPage(),
                            Detail = new NavigationPage(new Login()),
                        };

                        App.Current.MainPage = MasterDetailPage;
                    }

                    break;

                case 3:

                    MasterDetailPage = null;
                    MasterDetailPage = new MasterDetailPage
                    {
                        Master = new MenuPage(),
                        Detail = new NavigationPage(new PagPrincipal()),
                    };

                    App.Current.MainPage = MasterDetailPage;
                    break;

                case 2:

                    MasterDetailPage = null;
                    MasterDetailPage = new MasterDetailPage
                    {
                        Master = new MenuPage(),
                        Detail = new NavigationPage(new vUsuarios()),
                    };

                    App.Current.MainPage = MasterDetailPage;
                    break;

                case 1:

                    if (modGeneral.clsUsuario != null)
                        App.Current.MainPage = new NavigationPage(new vUsuarios());
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("Contratámos", "Para poder acceder a esta opción de iniciar sesión primero.", "Ok");
                        MasterDetailPage = null;
                        MasterDetailPage = new MasterDetailPage
                        {
                            Master = new MenuPage(),
                            Detail = new NavigationPage(new Login()),
                        };

                        App.Current.MainPage = MasterDetailPage;
                    }

                    break;
            }
        }
    }
}
