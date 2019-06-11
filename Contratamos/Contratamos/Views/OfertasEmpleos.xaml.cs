
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Contratamos.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OfertasEmpleos : ContentPage
    {
        public OfertasEmpleos()
        {
            InitializeComponent();
            fechaInicio.MinimumDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            fechaInicio.MaximumDate = new DateTime(DateTime.Now.Year + 20, 12, 31);
            fechaInicio.Date = DateTime.Now;

            fechaFin.MinimumDate = new DateTime(DateTime.Now.Year + 20, DateTime.Now.Month, 1);
            fechaFin.MaximumDate = new DateTime(DateTime.Now.Year + 20, 12, 31);
            fechaFin.Date = new DateTime(DateTime.Now.Year + 20, DateTime.Now.Month, DateTime.Now.Day);
        }



    }
}