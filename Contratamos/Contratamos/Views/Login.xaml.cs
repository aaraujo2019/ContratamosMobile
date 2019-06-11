using Android;
using Android.App;
using Contratamos.Clases;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Contratamos.ViewModel;

namespace Contratamos.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Login : ContentPage
	{
        private LoginViewModel loginViewModel;
        public LoginViewModel LoginViewModel { get => loginViewModel; set => loginViewModel = value; }

        public Login ()
		{
			InitializeComponent ();
            BindingContext = LoginViewModel = new LoginViewModel();
            LoginViewModel.Navigation = this.Navigation;
        }

        protected override bool OnBackButtonPressed() => true;

        private void mnuSalir_Clicked(object sender, EventArgs e)
        {
            AlertDialog.Builder dialog = new AlertDialog.Builder(clsConfiguracion.mContext);
            AlertDialog alert = dialog.Create();

            alert.SetTitle("BioFirma Mobile 1.0.0");
            alert.SetMessage("¿Esta seguro que quiere salir de BioFirma Mobile.?");
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