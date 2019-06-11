using System;
using Xamarin.Forms;

namespace Contratamos.Behaviors
{
    public class FechaValidatorBehavior : Behavior<DatePicker>
    {
        protected override void OnAttachedTo(DatePicker dp)
        {
            dp.DateSelected += DateSelected;
            base.OnAttachedTo(dp);
        }

        // Valida una fecha menor al año actual en 100 años máximo
        private void DateSelected(object sender, DateChangedEventArgs e)
        {
            int resultado = DateTime.Now.Year - e.NewDate.Year;
            bool valido = (resultado <= 100 && resultado >= 0);
            ((DatePicker)sender).BackgroundColor = valido ? Color.Green : Color.Red;
        }

        protected override void OnDetachingFrom(DatePicker dp)
        {
            dp.DateSelected -= DateSelected;
            base.OnDetachingFrom(dp);
        }
    }
}
