using Xamarin.Forms;

namespace Contratamos.Behaviors
{
    public class MaxLengthValidatorBehavior : Behavior<Entry>
    {
        public static readonly BindableProperty MaxLengthProperty = BindableProperty.Create("MaxLength", typeof(int), typeof(MaxLengthValidatorBehavior), 0);

        public int MaxLength
        {
            get { return (int)GetValue(MaxLengthProperty); }
            set { SetValue(MaxLengthProperty, value); }
        }

        protected override void OnAttachedTo(Entry entry)
        {
            entry.Unfocused += Unfocused;
            base.OnAttachedTo(entry);
        }

        // Devuelve una cadena de máximo una longitud permitida
        private void Unfocused(object sender, FocusEventArgs e)
        {
            Entry entry = (Entry)sender;

            if (entry.Text != null)
            {
                if (entry.Text.Length < MaxLength)
                    entry.Text = entry.Text.Substring(0, entry.Text.Length);
                else
                    entry.Text = entry.Text.Substring(0, MaxLength);
            }
            else entry.Text = "";
        }

        protected override void OnDetachingFrom(Entry entry)
        {
            entry.Unfocused -= Unfocused;
            base.OnDetachingFrom(entry);
        }
    }
}
