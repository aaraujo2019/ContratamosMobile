using Android;
using Android.App;
using Contratamos.Clases;
using Contratamos.Views;
using System;
using Xamarin.Forms;

namespace Contratamos.Menu
{
    public class MainLink : Checkbox
    {
        int OpcionSeleccionada = 0;

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
                case 2:
                    AlertDialog.Builder dialog = new AlertDialog.Builder(clsConfiguracion.mContext);
                    AlertDialog alert = dialog.Create();

                    alert.SetTitle("Contratámos");
                    alert.SetMessage("¿Desea cambiar el Modo de ejecución de la aplicación, está seguro.?");
                    alert.SetIcon(Resource.Drawable.IcDialogAlert);
                    alert.SetButton("OK", async (c, ev) =>
                    {
                        
                    });
                    alert.SetButton2("Cancelar", (s, ed) =>
                    {
                        return;
                    });
                    alert.Show();
                    break;
                case 1:

                        App.Current.MainPage = new NavigationPage(new Login());
                    break;
            }
        }
    }
}
