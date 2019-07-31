using Contratamos.Generales;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Contratamos.Menu
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MenuPage : ContentPage
	{
        public MenuPage ()
		{
			InitializeComponent ();
            string TituloMenu = string.Empty;

            if (modGeneral.clsUsuario != null)
                TituloMenu = string.Concat(modGeneral.clsUsuario.Nombre, " ", modGeneral.clsUsuario.Apellido);
            else TituloMenu = "Iniciar Sesión";

            Content = new StackLayout
            {
                Padding = Device.Idiom == TargetIdiom.Tablet ? new Thickness(30, 50, 20, 20) : new Thickness(30, 70, 20, 20),
                Children = {
                    new MainLink("Inicio", 3),
                    new MainLink("Aplicaciones", 8),
                    ((modGeneral.clsUsuario != null) && (modGeneral.clsUsuario.IdTipoUsuario == 2 || modGeneral.clsUsuario.IdTipoUsuario == 1)) ? 
                        new MainLink("Crear Oferta", 4):
                        new MainLink("Ver Oferta", 6),
                    new MainLink(TituloMenu, 1),
                    new MainLink("Crear Usuario", 2),
                    ((modGeneral.clsUsuario != null) && (modGeneral.clsUsuario.IdTipoUsuario == 1)) ?
                        new MainLink("Profesiones", 5) :
                        new MainLink("Salir", 7)
                }
            };
            Title = "Menú Inicio";
            BackgroundColor = Color.Gray.WithLuminosity(0.9);
            Icon = Device.OS == TargetPlatform.iOS ? "menu.png" : null;
        }
	}
}