
using Contratamos.Generales;
using Contratamos.Models;
using Contratamos.ViewModel;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Contratamos.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OfertasEmpleos : ContentPage
    {
        private OfertasEmpleosViewModel ofertasEmpleosViewModel;
        public OfertasEmpleosViewModel OfertasEmpleosViewModel { get => ofertasEmpleosViewModel; set => ofertasEmpleosViewModel = value; }

        public OfertasEmpleos()
        {
            InitializeComponent();
            BindingContext = OfertasEmpleosViewModel = new OfertasEmpleosViewModel();
            OfertasEmpleosViewModel.Navigation = this.Navigation;

            fechaInicio.MinimumDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            fechaInicio.MaximumDate = new DateTime(DateTime.Now.Year + 20, 12, 31);
            fechaInicio.Date = DateTime.Now;

            fechaFin.MinimumDate = new DateTime(DateTime.Now.Year + 20, DateTime.Now.Month, 1);
            fechaFin.MaximumDate = new DateTime(DateTime.Now.Year + 20, 12, 31);
            fechaFin.Date = new DateTime(DateTime.Now.Year + 20, DateTime.Now.Month, DateTime.Now.Day);
        }

        private void btnGuardar_Clicked(object sender, EventArgs e)
        {
            Ofertas ofertaE = new Ofertas();
            if (txtTitulo.Text == string.Empty)
            {
                App.Current.MainPage.DisplayAlert("Contratámos", "Debe ingresar el tiutlo de la oferta.", "Ok");
                txtTitulo.Focus();
                return;
            }
            else ofertaE.Titulo = txtTitulo.Text;

            if (cmbProfesiones.SelectedIndex == -1)
            {
                App.Current.MainPage.DisplayAlert("Contratámos", "Debe seleccionar una profesión.", "Ok");
                cmbProfesiones.Focus();
                return;
            }
            else ofertaE.IdProfesion = cmbProfesiones.SelectedIndex;

            if (fechaInicio.Date < DateTime.Now)
            {
                App.Current.MainPage.DisplayAlert("Contratámos", "Debe seleccionar una fecha de inicio valida.", "Ok");
                fechaInicio.Focus();
                return;
            }
            else ofertaE.OfertaDesde = fechaInicio.Date;

            if (fechaFin.Date <= fechaInicio.Date)
            {
                App.Current.MainPage.DisplayAlert("Contratámos", "Debe seleccionar una fecha de fin valida.", "Ok");
                fechaFin.Focus();
                return;
            }
            else ofertaE.OfertaHasta = fechaFin.Date;

            if (txtSalario.Text == string.Empty)
            {
                App.Current.MainPage.DisplayAlert("Contratámos", "Debe ingresar el salario para la oferta.", "Ok");
                txtSalario.Focus();
                return;
            }
            else ofertaE.Salario = Convert.ToDouble(txtSalario.Text);

            if (txtDetalleOferta.Text == string.Empty)
            {
                App.Current.MainPage.DisplayAlert("Contratámos", "Debe ingresar alguna descripción para la oferta.", "Ok");
                txtDetalleOferta.Focus();
                return;
            }
            else ofertaE.DescripcionOferta = txtDetalleOferta.Text;

            ofertaE.IdUsuario = modGeneral.clsUsuario.IdUsuario;
            //ofertaE.


        }

        private void btnNuevo_Clicked(object sender, EventArgs e)
        {

        }

        private void Switch_Toggled(object sender, ToggledEventArgs e)
        {
            try
            {
                if (e.Value)
                {
                    lblEstado.Text = "Activo";
                }
                else lblEstado.Text = "Inactivo";
            }
            catch (Exception ex)
            {
                App.Current.MainPage.DisplayAlert("Contratámos", "Ha ocurrido un problema.", "Ok");
            }
        }
    }
}