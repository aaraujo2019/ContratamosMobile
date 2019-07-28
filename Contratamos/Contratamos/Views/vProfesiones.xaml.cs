using Contratamos.Clases;
using Contratamos.Generales;
using Contratamos.Menu;
using Contratamos.Models;
using System;
using System.Data;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Contratamos.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class vProfesiones : ContentPage
    {
        Profesiones objProfesiones;
        clsPrincipal clsPrincipal = new clsPrincipal();
        private MasterDetailPage MasterDetailPage;
        int IdProfesion = 0;

        public vProfesiones()
        {
            InitializeComponent();
        }

        private void btnBuscar_Clicked(object sender, EventArgs e)
        {
            if (txtBuscar.Text != string.Empty)
            {
                DataSet profesion = clsPrincipal.BuscarProfesionPorId(Convert.ToInt32(txtBuscar.Text));

                foreach (DataRow dr in profesion.Tables[0].Rows)
                {
                    objProfesiones = new Profesiones();
                    objProfesiones.IdProfesion = Convert.ToInt32(dr["IdProfesion"]);
                    objProfesiones.Descripcion = dr["Descripcion"].ToString();

                    IdProfesion = objProfesiones.IdProfesion;
                    txtDescripcionProfesion.Text = objProfesiones.Descripcion;
                }
            }
        }

        private void btnGuardar_Clicked(object sender, EventArgs e)
        {
            if (objProfesiones == null)
            {
                objProfesiones = new Profesiones();
                if (txtDescripcionProfesion.Text == string.Empty)
                {
                    App.Current.MainPage.DisplayAlert("Contratámos", "Debe ingresar la descripción de la profesión.", "Ok");
                    txtDescripcionProfesion.Focus();
                    return;
                }
                else objProfesiones.Descripcion = txtDescripcionProfesion.Text;

                if (modGeneral.clsUsuario != null && (modGeneral.clsUsuario.IdTipoUsuario == 1))
                {
                    var resultado = clsPrincipal.GuardarProfesion(objProfesiones.Descripcion);
                    App.Current.MainPage.DisplayAlert("Contratámos", string.Concat("La Profesión No. ", resultado, ", fue ingresada con exito. "), "Ok");
                }
                else
                {
                    App.Current.MainPage.DisplayAlert("Contratámos", "Debe ser propietario para modificar la profesión", "Ok");
                }
            }
            else
            {
                if (txtDescripcionProfesion.Text == string.Empty)
                {
                    App.Current.MainPage.DisplayAlert("Contratámos", "Debe ingresar la descripción de la profesión.", "Ok");
                    txtDescripcionProfesion.Focus();
                    return;
                }

                clsPrincipal.ActualizarProfesion(objProfesiones);
                App.Current.MainPage.DisplayAlert("Contratámos", "La Profesión fue actualizada con exito.", "Ok");               
            }

            limpiarControles();
        }

        private void limpiarControles()
        {
            txtDescripcionProfesion.Text = string.Empty;
            txtBuscar.Text = string.Empty;
            IdProfesion = 0;
            objProfesiones = null;
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