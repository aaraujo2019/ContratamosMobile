using Contratamos.Clases;
using Contratamos.Menu;
using Contratamos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Contratamos.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AplcacionesUsuarios : ContentPage
    {
        private StackLayout _Panel;
        private MasterDetailPage MasterDetailPage;
        private List<Aplicaciones> listaAplicaciones;
        private clsPrincipal clsPrincipal = new clsPrincipal();

        public AplcacionesUsuarios()
        {
            InitializeComponent();

            listaAplicaciones = clsPrincipal.CargarAplcaciones();
            ListarAplicaciones(listaAplicaciones);
        }

        private void MnuRegresar_Clicked(object sender, EventArgs e)
        {
            MasterDetailPage = null;
            MasterDetailPage = new MasterDetailPage
            {
                Master = new MenuPage(),
                Detail = new NavigationPage(new PagPrincipal()),
            };

            App.Current.MainPage = MasterDetailPage;
        }

        private void ListarAplicaciones(List<Aplicaciones> listaAplicaciones)
        {
            try
            {
                List<KeyValuePair<int, string>> listaElementos = new List<KeyValuePair<int, string>>();
                foreach (var item in listaAplicaciones)
                {
                    listaElementos.Add(new KeyValuePair<int, string>(item.IdUsuario, string.Concat(item.NombreUsuario, " - ", item.FechaAplicación)));
                }

                ListView listView = new ListView { ItemsSource = listaElementos };
                listView.ItemTemplate = new DataTemplate(() =>
                {
                    Label valor = new Label
                    {
                        FontSize = Device.Idiom == TargetIdiom.Tablet ? Device.GetNamedSize(NamedSize.Medium, typeof(Label)) : Device.GetNamedSize(NamedSize.Small, typeof(Label))
                    };
                    valor.SetBinding(Label.TextProperty, "Value");
                    valor.Margin = new Thickness(5, 10);

                    ViewCell mViewCell = new ViewCell();
                    View view = new StackLayout
                    {
                        Orientation = StackOrientation.Horizontal,
                        Children =
                        {
                            new StackLayout
                            {
                                Orientation = StackOrientation.Horizontal,
                                VerticalOptions = LayoutOptions.Center,
                                HorizontalOptions = LayoutOptions.Start,
                                Children = { valor }
                            }
                        }
                    };

                    mViewCell.View = view;
                    return mViewCell;
                });

                listView.ItemSelected += List_Itemselected;

                Label titulo = new Label
                {
                    Text = "Aplicaciones por usuario",
                    Margin = new Thickness(10, 15),
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.CenterAndExpand,
                    FontAttributes = FontAttributes.Bold,
                    FontSize = Device.Idiom == TargetIdiom.Tablet ? Device.GetNamedSize(NamedSize.Large, typeof(Label)) : Device.GetNamedSize(NamedSize.Small, typeof(Label))
                };

                _Panel = new StackLayout { Children = { titulo, listView } };
                pnlPlantillas.Children.Add(_Panel);
            }
            catch (Exception)
            {
                App.Current.MainPage.DisplayAlert("Contratámos", "Ha ocurrido un problema al momento de cargar los documentos.", "Ok");
            }
        }

        private void List_Itemselected(object sender, SelectedItemChangedEventArgs e)
        {
            ((ListView)sender).SelectedItem = null;

            if (((ListView)sender).SelectedItem != null)
            {
                var resp = (KeyValuePair<int, string>)e.SelectedItem;
                var idUsuario = resp.Key;
                var aplicaciones = listaAplicaciones.Where(o => o.IdUsuario == idUsuario).FirstOrDefault();

                MasterDetailPage = null;
                MasterDetailPage = new MasterDetailPage
                {
                    Master = new MenuPage(),
                    Detail = new NavigationPage(new vUsuarios(aplicaciones)),
                };

                App.Current.MainPage = MasterDetailPage;
            }
        }




    }
}