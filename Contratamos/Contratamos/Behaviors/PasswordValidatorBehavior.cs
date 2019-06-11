using System.Text.RegularExpressions;
using Xamarin.Forms;


namespace Contratamos.Behaviors
{
    public class PasswordValidatorBehavior : Behavior<Entry>
    {
        const string passwordRegex = @"^(?=.*[A-Za-z])(?=.*\d)(?=.*[$@$!%*#?&])[A-Za-z\d$@$!%*#?&]{10,}$";

        protected override void OnAttachedTo(Entry entry)
        {
            entry.TextChanged += TextChanged;
            base.OnAttachedTo(entry);
        }

        //10 caracteres, con al menos: 1 digito, 1 minúscula, 1 mayúscula, 1 caracter especial
        void TextChanged(object sender, TextChangedEventArgs e)
        {
            bool valido = (Regex.IsMatch(e.NewTextValue, passwordRegex));
            ((Entry)sender).TextColor = valido ? Color.Green : Color.Red;
        }

        protected override void OnDetachingFrom(Entry entry)
        {
            entry.TextChanged -= TextChanged;
            base.OnDetachingFrom(entry);
        }
    }
}
