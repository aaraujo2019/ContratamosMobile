using Contratamos.Generales;
using Contratamos.Views;
using Contratamos.Servicios;
using Xamarin.Forms;

namespace Contratamos
{
    public partial class App : Application
    {
        public static clsUsuariosWs objWSUsuarios { get; set; }
        public static clsProcesosWs objWSProcesos { get; set; }

        public App()
        {
            InitializeComponent();
            modGeneral.NombreAplicacion = "CONTRATÁMOS";
            MainPage = new NavigationPage(new Inicio());
        }

        protected override void OnStart(){}
        protected override void OnSleep(){}
        protected override void OnResume(){}
    }
}
