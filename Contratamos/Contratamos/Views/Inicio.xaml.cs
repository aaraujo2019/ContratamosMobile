using Android;
using Android.App;
using Contratamos.Clases;
using Contratamos.ViewModel;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Contratamos.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Inicio : ContentPage
	{
        private InicioViewModel inicioViewModel;
        public InicioViewModel InicioViewModel { get => inicioViewModel; set => inicioViewModel = value; }

        public Inicio ()
		{
			InitializeComponent ();
            BindingContext = InicioViewModel = new InicioViewModel();
            InicioViewModel.Navigation = this.Navigation;
		}

        protected override bool OnBackButtonPressed() => true;

        private void mnuSalir_Clicked(object sender, EventArgs e)
        {
            AlertDialog.Builder dialog = new AlertDialog.Builder(clsConfiguracion.mContext);
            AlertDialog alert = dialog.Create();

            alert.SetTitle("Contratámos");
            alert.SetMessage("¿Esta seguro que quiere salir.?");
            alert.SetIcon(Resource.Drawable.IcDialogAlert);
            alert.SetButton("OK", (c, ev) =>
            {
                Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
            });
            alert.SetButton2("Cancelar", (s, ed) =>
            {
                return;
            });
            alert.Show();
        }
    }
}