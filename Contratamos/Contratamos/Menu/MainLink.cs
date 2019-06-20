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
