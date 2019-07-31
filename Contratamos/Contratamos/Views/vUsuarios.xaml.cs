using Contratamos.Clases;
using Contratamos.Generales;
using Contratamos.Menu;
using Contratamos.Models;
using Contratamos.ViewModel;
using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Contratamos.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class vUsuarios : ContentPage
    {
        private vUsuarioViewModel vusuarioViewModel;
        public vUsuarioViewModel vUsuarioViewModel { get => vusuarioViewModel; set => vusuarioViewModel = value; }
        public modGeneral modGeneral = new modGeneral();
        private MasterDetailPage MasterDetailPage;
        private clsPrincipal clsPrincipal = new clsPrincipal();

        public vUsuarios()
        {
            InitializeComponent();
            BindingContext = vUsuarioViewModel = new vUsuarioViewModel();
            vUsuarioViewModel.Navigation = this.Navigation;

            if (modGeneral.clsUsuario != null)
            {
                cmbTipoUsuario.SelectedIndex = modGeneral.clsUsuario.IdTipoUsuario;
                vUsuarioViewModel.Verificar = modGeneral.clsUsuario.Contraseña;
            }
        }

        public vUsuarios(Aplicaciones aplcaciones)
        {
            InitializeComponent();

            Usuarios usuarioSlec = clsPrincipal.ConsultarusuarioPorID(aplcaciones.IdUsuario);
           
            BindingContext = vUsuarioViewModel = new vUsuarioViewModel();
            vUsuarioViewModel.Navigation = this.Navigation;

            vUsuarioViewModel.User = usuarioSlec;

            if (vUsuarioViewModel.User != null)
            {
                cmbTipoUsuario.SelectedIndex = vUsuarioViewModel.User.IdTipoUsuario;
                vUsuarioViewModel.Verificar = vUsuarioViewModel.User.Contraseña;
            }

            if (modGeneral.clsUsuario != null)
            {
                if (modGeneral.clsUsuario.IdUsuario == vUsuarioViewModel.User.IdUsuario)
                    HabilitarControles();
                else InhabilitarControles();
            }
            else InhabilitarControles();
        }

        public async void OpenFolderDialogAsync()
        {
            SimpleFileDialog fileDialog = new SimpleFileDialog(clsConfiguracion.mContext, SimpleFileDialog.FileSelectionMode.FileOpen);
            string path = await fileDialog.GetFileOrDirectoryAsync(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath);

            if (!String.IsNullOrEmpty(path))
            {
                if (Path.GetExtension(path) != ".pdf")
                {
                    App.Current.MainPage.DisplayAlert("Contratamos", "Debe seleccionar solo archivos .pdf", "Ok");
                    return;
                }

                CargarArchivoSimple(path);
            }
        }

        private void CargarArchivoSimple(string Archivo)
        {
            try
            {
                if (Device.RuntimePlatform == Device.Android)
                {
                    byte[] pdfBytes = File.ReadAllBytes(Archivo);
                    string pdfBase64 = Convert.ToBase64String(pdfBytes);
                    vUsuarioViewModel.User.ArchivoCv = modGeneral.ConvertirDocBinary(Archivo);
                    txtRuta.Text = Archivo;
                }
            }
            catch (Exception ex)
            {
                clsConfiguracion.Main(string.Concat("Ha ocurrido un problema al cargar el documento. ", ex.Message), "CargarArchivo(); Anexos", modGeneral.usuario);
                App.Current.MainPage.DisplayAlert("Contratámos", "Ha ocurrido un problema al cargar el documento.", "Ok");
            }
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            txtNombre.Text = string.Empty;
            txtApellido.Text = string.Empty;
            txtContrasena.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtRuta.Text = string.Empty;
            txtUsuario.Text = string.Empty;
            cmbTipoUsuario.SelectedIndex = -1;
            txtCeluar.Text = string.Empty;
        }

        private void InhabilitarControles()
        {
            txtNombre.IsEnabled = false;
            txtApellido.IsEnabled = false;
            txtContrasena.IsEnabled = false;
            txtEmail.IsEnabled = false;
            txtRuta.IsEnabled = false;
            txtUsuario.IsEnabled = false;
            cmbTipoUsuario.IsEnabled = false;
            txtCeluar.IsEnabled = false;
        }

        private void HabilitarControles()
        {
            txtNombre.IsEnabled = true;
            txtApellido.IsEnabled = true;
            txtContrasena.IsEnabled = true;
            txtEmail.IsEnabled = true;
            txtRuta.IsEnabled = true;
            txtUsuario.IsEnabled = true;
            cmbTipoUsuario.IsEnabled = true;
            txtCeluar.IsEnabled = true;
            txtObservaciones.IsEnabled = true;
            btnGuardar.IsEnabled = true;
        }

        private void BtnBuscar_Clicked(object sender, EventArgs e)
        {
            OpenFolderDialogAsync();
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
    }
}