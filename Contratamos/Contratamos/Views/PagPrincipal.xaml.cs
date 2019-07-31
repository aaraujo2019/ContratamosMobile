using Android;
using Android.App;
using Contratamos.Clases;
using Contratamos.Generales;
using Contratamos.Menu;
using Contratamos.Models;
using Contratamos.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Contratamos.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PagPrincipal : ContentPage
    {
        private PagPrincipalViewModel pagPrincipalViewModel;
        private StackLayout _Panel;
        private List<Ofertas> listOfertas;
        private MasterDetailPage MasterDetailPage;
        private int OpcionBusqueda = 1;

        public PagPrincipal()
        {
            InitializeComponent();
            BindingContext = pagPrincipalViewModel = new PagPrincipalViewModel();
            pagPrincipalViewModel.Navigation = this.Navigation;
            pagPrincipalViewModel.NombreAplicacion = modGeneral.NombreAplicacion;
            pagPrincipalViewModel.IsVisiblePicker = false;
            pagPrincipalViewModel.IsVisibleTexto = true;

            DataSet ofertas = App.objWSProcesos.CargarOfertas();
            listOfertas = new List<Ofertas>();

            foreach (DataRow dr in ofertas.Tables[0].Rows)
            {
                listOfertas.Add(new Ofertas
                {
                    IdOferta = Convert.ToInt32(dr["IdOferta"]),
                    Titulo = dr["Titulo"].ToString(),
                    DescripcionOferta = dr["DescripcionOferta"].ToString(),
                    Salario = Convert.ToDouble(dr["Salario"]),
                    OfertaDesde = Convert.ToDateTime(dr["OfertaDesde"]),
                    OfertaHasta = Convert.ToDateTime(dr["OfertaHasta"]),
                    IdUsuario = Convert.ToInt32(dr["IdUsuario"]),
                    IdDispositivo = dr["IdDispositivo"].ToString(),
                    IdEstado = Convert.ToInt32(dr["IdEstado"]),
                    IdProfesion = Convert.ToInt32(dr["IdProfesion"])
                });
            }

            ListarOfertas(listOfertas);
        }

        protected override bool OnBackButtonPressed() => true;

        private void menuSalir_Clicked(object sender, EventArgs e)
        {
            AlertDialog.Builder dialog = new AlertDialog.Builder(clsConfiguracion.mContext);
            AlertDialog alert = dialog.Create();

            alert.SetTitle("Contratámos");
            alert.SetMessage("¿Desea cerrar la sesión?");
            alert.SetIcon(Resource.Drawable.IcDialogAlert);
            alert.SetButton("OK", async (c, ev) =>
            {
                App.Current.MainPage = new NavigationPage(new Inicio());
            });
            alert.SetButton2("Cancelar", (s, ed) =>
            {
                return;
            });

            alert.Show();
        }

        private void Switch_Toggled(object sender, ToggledEventArgs e)
        {
            try
            {
                if (pagPrincipalViewModel.IsVisibleTexto)
                {
                    pagPrincipalViewModel.IsVisibleTexto = false;
                    pagPrincipalViewModel.IsVisiblePicker = true;
                    OpcionBusqueda = 2;
                }
                else
                {
                    pagPrincipalViewModel.IsVisibleTexto = true;
                    pagPrincipalViewModel.IsVisiblePicker = false;
                    OpcionBusqueda = 1;
                }
            }
            catch (Exception)
            {
                App.Current.MainPage.DisplayAlert("Contratámos", "Ha ocurrido un problema.", "Ok");
            }
        }

        private void ListarOfertas(List<Ofertas> listaOferta)
        {
            try
            {
                List<KeyValuePair<int, string>> listaElementos = new List<KeyValuePair<int, string>>();
                foreach (var item in listaOferta)
                {
                    listaElementos.Add(new KeyValuePair<int, string>(item.IdOferta, item.Titulo));
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
                    Text = "Ofertas disponibles",
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
            if (modGeneral.clsUsuario != null)
            {
                ((ListView)sender).SelectedItem = null;

                if (((ListView)sender).SelectedItem != null)
                {
                    var resp = (KeyValuePair<int, string>)e.SelectedItem;
                    var idOferta = resp.Key;
                    var ofertaCompleta = listOfertas.Where(o => o.IdOferta == idOferta).FirstOrDefault();

                    MasterDetailPage = null;
                    MasterDetailPage = new MasterDetailPage
                    {
                        Master = new MenuPage(),
                        Detail = new NavigationPage(new OfertasEmpleos(ofertaCompleta)),
                    };

                    App.Current.MainPage = MasterDetailPage;
                }
            }
            else
            {
                App.Current.MainPage.DisplayAlert("Contratamos", "Debe inicar sesión para poder acceder a esta opción.", "Ok");
                MasterDetailPage = null;
                MasterDetailPage = new MasterDetailPage
                {
                    Master = new MenuPage(),
                    Detail = new NavigationPage(new Login()),
                };

                App.Current.MainPage = MasterDetailPage;
            }
        }

        private void BtnBuscar_Clicked(object sender, EventArgs e)
        {
            foreach (var item in pnlPlantillas.Children.ToList())
            {
                pnlPlantillas.Children.Remove(item);
            }
            
            string textoBusqueda = string.Empty;
            if (OpcionBusqueda == 1)
            {
                if (txtBusqueda.Text != null)
                    textoBusqueda = txtBusqueda.Text;
                else
                {
                    App.Current.MainPage.DisplayAlert("Contratamos", "Debe ingresar un criterio para la busqueda.", "Ok");
                    txtBusqueda.Focus();
                    return;
                }
            }
            else
            {
                if (pagPrincipalViewModel.SelectedProfesion != null)
                    textoBusqueda = pagPrincipalViewModel.SelectedProfesion.IdProfesion.ToString();
                else
                {
                    App.Current.MainPage.DisplayAlert("Contratamos", "Debe seleccionar una profesión.", "Ok");
                    return;
                }
            }

            DataSet ofertas = App.objWSProcesos.FiltrarOferta(OpcionBusqueda, textoBusqueda);
            listOfertas = new List<Ofertas>();

            foreach (DataRow dr in ofertas.Tables[0].Rows)
            {
                listOfertas.Add(new Ofertas
                {
                    IdOferta = Convert.ToInt32(dr["IdOferta"]),
                    Titulo = dr["Titulo"].ToString(),
                    DescripcionOferta = dr["DescripcionOferta"].ToString(),
                    Salario = Convert.ToDouble(dr["Salario"]),
                    OfertaDesde = Convert.ToDateTime(dr["OfertaDesde"]),
                    OfertaHasta = Convert.ToDateTime(dr["OfertaHasta"]),
                    IdUsuario = Convert.ToInt32(dr["IdUsuario"]),
                    IdDispositivo = dr["IdDispositivo"].ToString(),
                    IdEstado = Convert.ToInt32(dr["IdEstado"]),
                    IdProfesion = Convert.ToInt32(dr["IdProfesion"])
                });
            }

            ListarOfertas(listOfertas);
        }
    }
}