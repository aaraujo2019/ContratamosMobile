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
                Padding = Device.Idiom == TargetIdiom.Tablet ? new Thickness(30, 50, 20, 20) : new Thickness(20, 50, 10, 10),
                Children = {
                    new MainLink("Inicio", 3),
                    new MainLink("Crear Oferta", 4),
                    new MainLink("Agregar Profesiones", 5),
                    new MainLink(TituloMenu, 1),
                    new MainLink("Crear Usuario", 2)
                }
            };
            Title = "Menú Inicio";
            BackgroundColor = Color.Gray.WithLuminosity(0.9);
            Icon = Device.OS == TargetPlatform.iOS ? "menu.png" : null;
        }
	}
}