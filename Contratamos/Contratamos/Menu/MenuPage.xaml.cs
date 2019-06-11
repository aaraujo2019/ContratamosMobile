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

            Content = new StackLayout
            {
                Padding = Device.Idiom == TargetIdiom.Tablet ? new Thickness(30, 50, 20, 20) : new Thickness(20, 50, 10, 10),
                Children = {
                    new MainLink("Inicio Sesión", 1),
                    new MainLink("Crear Usuario", 2)
                }
            };
            Title = "Menú Inicio";
            BackgroundColor = Color.Gray.WithLuminosity(0.9);
            Icon = Device.OS == TargetPlatform.iOS ? "menu.png" : null;
        }
	}
}