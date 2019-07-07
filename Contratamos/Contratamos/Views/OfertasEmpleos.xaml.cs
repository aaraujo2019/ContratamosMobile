
using Contratamos.Clases;
using Contratamos.Generales;
using Contratamos.Models;
using Contratamos.Servicios;
using Contratamos.ViewModel;
using Plugin.DeviceInfo;
using System;
using System.Data;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Contratamos.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OfertasEmpleos : ContentPage
    {
        private OfertasEmpleosViewModel ofertasEmpleosViewModel;
        public OfertasEmpleosViewModel OfertasEmpleosViewModel { get => ofertasEmpleosViewModel; set => ofertasEmpleosViewModel = value; }
        clsPrincipal clsPrincipal = new clsPrincipal();
        Ofertas objOferta;

        public OfertasEmpleos()
        {
            InitializeComponent();
            BindingContext = OfertasEmpleosViewModel = new OfertasEmpleosViewModel();
            OfertasEmpleosViewModel.Navigation = this.Navigation;

            fechaInicio.MinimumDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            fechaInicio.MaximumDate = new DateTime(DateTime.Now.Year + 20, 12, 31);
            fechaInicio.Date = DateTime.Now;

            fechaFin.MinimumDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            fechaFin.MaximumDate = new DateTime(DateTime.Now.Year, 12, 31);
            fechaFin.Date = new DateTime(DateTime.Now.Year, 12, DateTime.Now.Day);
        }

        private void btnGuardar_Clicked(object sender, EventArgs e)
        {
            if (objOferta == null)
            {
                objOferta = new Ofertas();
                if (txtTitulo.Text == string.Empty)
                {
                    App.Current.MainPage.DisplayAlert("Contratámos", "Debe ingresar el tiutlo de la oferta.", "Ok");
                    txtTitulo.Focus();
                    return;
                }
                else objOferta.Titulo = txtTitulo.Text;

                if (cmbProfesiones.SelectedIndex == -1)
                {
                    App.Current.MainPage.DisplayAlert("Contratámos", "Debe seleccionar una profesión.", "Ok");
                    cmbProfesiones.Focus();
                    return;
                }
                else objOferta.IdProfesion = cmbProfesiones.SelectedIndex;

                if (Convert.ToDateTime(fechaInicio.Date.ToShortDateString()) < Convert.ToDateTime(DateTime.Now.ToShortDateString()))
                {
                    App.Current.MainPage.DisplayAlert("Contratámos", "Debe seleccionar una fecha de inicio valida.", "Ok");
                    return;
                }
                else objOferta.OfertaDesde = fechaInicio.Date;

                if (Convert.ToDateTime(fechaFin.Date.ToShortDateString()) <= Convert.ToDateTime(fechaInicio.Date.ToShortDateString()))
                {
                    App.Current.MainPage.DisplayAlert("Contratámos", "Debe seleccionar una fecha de fin valida.", "Ok");
                    return;
                }
                else objOferta.OfertaHasta = fechaFin.Date;

                if (txtSalario.Text == string.Empty)
                {
                    App.Current.MainPage.DisplayAlert("Contratámos", "Debe ingresar el salario para la oferta.", "Ok");
                    txtSalario.Focus();
                    return;
                }
                else objOferta.Salario = Convert.ToDouble(txtSalario.Text);

                if (txtDetalleOferta.Text == string.Empty)
                {
                    App.Current.MainPage.DisplayAlert("Contratámos", "Debe ingresar alguna descripción para la oferta.", "Ok");
                    txtDetalleOferta.Focus();
                    return;
                }
                else objOferta.DescripcionOferta = txtDetalleOferta.Text;

                objOferta.IdUsuario = modGeneral.clsUsuario.IdUsuario;
                objOferta.IdEstado = swEstadoOferta.IsToggled == true ? 1 : 2;
                objOferta.IdDispositivo = CrossDeviceInfo.Current.Id;

                var resultado = clsPrincipal.InsertarOferta(objOferta);
                App.Current.MainPage.DisplayAlert("Contratámos", string.Concat("La Oferta No. ", resultado, ", fue ingresada con exito. "), "Ok");
            }
            else
            {
                if (txtTitulo.Text == string.Empty)
                {
                    App.Current.MainPage.DisplayAlert("Contratámos", "Debe ingresar el tiutlo de la oferta.", "Ok");
                    txtTitulo.Focus();
                    return;
                }
                else objOferta.Titulo = txtTitulo.Text;

                if (cmbProfesiones.SelectedIndex == -1)
                {
                    App.Current.MainPage.DisplayAlert("Contratámos", "Debe seleccionar una profesión.", "Ok");
                    cmbProfesiones.Focus();
                    return;
                }
                else objOferta.IdProfesion = cmbProfesiones.SelectedIndex;

                if (Convert.ToDateTime(fechaInicio.Date.ToShortDateString()) < Convert.ToDateTime(DateTime.Now.ToShortDateString()))
                {
                    App.Current.MainPage.DisplayAlert("Contratámos", "Debe seleccionar una fecha de inicio valida.", "Ok");
                    return;
                }
                else objOferta.OfertaDesde = fechaInicio.Date;

                if (Convert.ToDateTime(fechaFin.Date.ToShortDateString()) <= Convert.ToDateTime(fechaInicio.Date.ToShortDateString()))
                {
                    App.Current.MainPage.DisplayAlert("Contratámos", "Debe seleccionar una fecha de fin valida.", "Ok");
                    return;
                }
                else objOferta.OfertaHasta = fechaFin.Date;

                if (txtSalario.Text == string.Empty)
                {
                    App.Current.MainPage.DisplayAlert("Contratámos", "Debe ingresar el salario para la oferta.", "Ok");
                    txtSalario.Focus();
                    return;
                }
                else objOferta.Salario = txtSalario.Text.Contains("$") ? objOferta.Salario : Convert.ToDouble(txtSalario.Text);

                if (txtDetalleOferta.Text == string.Empty)
                {
                    App.Current.MainPage.DisplayAlert("Contratámos", "Debe ingresar alguna descripción para la oferta.", "Ok");
                    txtDetalleOferta.Focus();
                    return;
                }
                else objOferta.DescripcionOferta = txtDetalleOferta.Text;

                objOferta.IdUsuario = modGeneral.clsUsuario.IdUsuario;
                objOferta.IdEstado = swEstadoOferta.IsToggled == true ? 1 : 2;
                objOferta.IdDispositivo = CrossDeviceInfo.Current.Id;

                clsPrincipal.ActualizarOferta(objOferta);
                App.Current.MainPage.DisplayAlert("Contratámos", "La Oferta fue actualizada con exito.", "Ok");
            }
            
            btnNuevo_Clicked(null, null);
        }

        private void btnNuevo_Clicked(object sender, EventArgs e)
        {
            txtTitulo.Text = string.Empty;
            cmbProfesiones.SelectedIndex = -1;
            fechaInicio.Date = DateTime.Now;
            fechaFin.Date = fechaInicio.Date;
            txtSalario.Text = string.Empty;
            txtDetalleOferta.Text = string.Empty;
            lblEstado.Text = "Inactivo";
            swEstadoOferta.IsToggled = false;
            txtDetalleOferta.Text = string.Empty;
            objOferta = null;
            txtBuscar.Text = string.Empty;
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
            catch (Exception)
            {
                App.Current.MainPage.DisplayAlert("Contratámos", "Ha ocurrido un problema.", "Ok");
            }
        }

        private void btnBuscar_Clicked(object sender, EventArgs e)
        {
            if (txtBuscar.Text != string.Empty)
            {
                DataSet oferta = clsPrincipal.BuscarOfertasPorId(Convert.ToInt32(txtBuscar.Text));

                foreach (DataRow dr in oferta.Tables[0].Rows)
                {
                    objOferta = new Ofertas();
                    objOferta.IdOferta = Convert.ToInt32(dr["IdOferta"]);
                    objOferta.Titulo = dr["Titulo"].ToString();
                    objOferta.DescripcionOferta = dr["DescripcionOferta"].ToString();
                    objOferta.Salario = Convert.ToDouble(dr["Salario"]);
                    objOferta.OfertaDesde = Convert.ToDateTime(dr["OfertaDesde"]);
                    objOferta.OfertaHasta = Convert.ToDateTime(dr["OfertaHasta"]);
                    objOferta.IdUsuario = Convert.ToInt32(dr["IdUsuario"]);
                    objOferta.IdDispositivo = dr["IdDispositivo"].ToString();
                    objOferta.IdEstado = Convert.ToInt32(dr["IdEstado"]);
                    objOferta.IdProfesion = Convert.ToInt32(dr["IdProfesion"]);
                    objOferta.IdEstado = Convert.ToInt32(dr["IdEstado"]);

                    txtTitulo.Text = objOferta.Titulo;
                    fechaInicio.Date = objOferta.OfertaDesde;
                    fechaFin.Date = objOferta.OfertaHasta;
                    txtSalario.Text = objOferta.Salario.ToString("C2");
                    cmbProfesiones.SelectedIndex = objOferta.IdProfesion;
                    txtDetalleOferta.Text = objOferta.DescripcionOferta;
                    swEstadoOferta.IsToggled = objOferta.IdEstado == 1 ? true : false;
                }
            }
        }
    }
}