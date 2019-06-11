using System;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace Contratamos.Behaviors
{
    public class NumeroValidatorBehavior : Behavior<Entry>
    {
        const string digitosRegEx = @"^[0-9]+$";

        protected override void OnAttachedTo(Entry entry)
        {
            entry.TextChanged += TextChanged;
            base.OnAttachedTo(entry);
        }

        // Solo dígitos
        void TextChanged(object sender, TextChangedEventArgs e)
        {
            bool valido = (Regex.IsMatch(e.NewTextValue, digitosRegEx, RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250)));
            ((Entry)sender).TextColor = valido ? Color.Green : Color.Red;
            ((Entry)sender).FontAttributes = FontAttributes.Bold;
        }

        protected override void OnDetachingFrom(Entry entry)
        {
            entry.TextChanged -= TextChanged;
            base.OnDetachingFrom(entry);
        }
    }
}
