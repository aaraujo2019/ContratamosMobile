using Android;
using Android.App;
using Contratamos.Clases;
using Contratamos.Generales;
using Contratamos.Models;
using Contratamos.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Contratamos.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PagPrincipal : ContentPage
    {
        private PagPrincipalViewModel pagPrincipalViewModel;
        private WebView webView;
        private StackLayout _Panel;

        public PagPrincipal()
        {
            InitializeComponent();
            BindingContext = pagPrincipalViewModel = new PagPrincipalViewModel();
            pagPrincipalViewModel.Navigation = this.Navigation;
            pagPrincipalViewModel.NombreAplicacion = modGeneral.NombreAplicacion;
            pagPrincipalViewModel.IsVisiblePicker = false;
            pagPrincipalViewModel.IsVisibleTexto = true;

            DataSet ofertas = App.objWSProcesos.CargarOfertas();
            List<Ofertas> listOfertas = new List<Ofertas>();

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
                }
                else
                {
                    pagPrincipalViewModel.IsVisibleTexto = true;
                    pagPrincipalViewModel.IsVisiblePicker = false;
                }
            }
            catch (Exception ex)
            {
                App.Current.MainPage.DisplayAlert("Contratámos", "Ha ocurrido un problema.", "Ok");
            }
        }

        private void ListarOfertas(List<Ofertas> listaProfesiones)
        {
            try
            {
                List<KeyValuePair<int, string>> listaElementos = new List<KeyValuePair<int, string>>();
                foreach (var item in listaProfesiones)
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

                    MenuItem moreAction = new MenuItem { Text = "SELECCIONAR DOCUMENTO LOCAL", };
                    moreAction.Clicked += (sender, e) =>
                    {
                        MenuItem mi = ((MenuItem)sender);
                        OpenFolderDialogAsync();
                    };

                    ViewCell mViewCell = new ViewCell();
                    mViewCell.ContextActions.Add(moreAction);

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
            catch (Exception ex)
            {
                //clsConfiguracion.Main(string.Concat("Ha ocurrido un problema al momento de cargar los documentos.", ex.Message), "ListarDocumentos(); Anexos", modGeneral.usuario);
                App.Current.MainPage.DisplayAlert("Contratámos", "Ha ocurrido un problema al momento de cargar los documentos.", "Ok");
            }
        }

        private void List_Itemselected(object sender, SelectedItemChangedEventArgs e)
        {
            ((ListView)sender).SelectedItem = null;

            if (((ListView)sender).SelectedItem != null)
            {
                var resp = (KeyValuePair<int, string>)e.SelectedItem;
                //IdPantilla = resp.Key;
                //modGeneral.IDPlantillaActual = IdPantilla;

                //if (clsConfiguracion.ModoIntegracion == "SI")
                //    CargarPlantillaIntegracion();
                //else CargarPlantilla(IdPantilla);
            }
        }

        public async void OpenFolderDialogAsync()
        {
            SimpleFileDialog fileDialog = new SimpleFileDialog(clsConfiguracion.mContext, SimpleFileDialog.FileSelectionMode.FileOpen);
            string path = await fileDialog.GetFileOrDirectoryAsync(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath);

            if (!String.IsNullOrEmpty(path))
            {
                if (Path.GetExtension(path) != ".pdf")
                {
                    await App.Current.MainPage.DisplayAlert("Contratámos", "Debe seleccionar solo archivos .pdf", "Ok");
                    return;
                }

                //CargarArchivoSimple(path);
            }
        }


    }
}