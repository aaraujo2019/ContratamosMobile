using Contratamos.ViewModel;
using Rg.Plugins.Popup.Pages;

namespace Contratamos.Views
{
	public partial class UserAnimationPage : PopupPage
    {
        private AnimacionPageViewModel animacionPageViewModel;
        public UserAnimationPage ()
		{
			InitializeComponent ();
            BindingContext = animacionPageViewModel = new AnimacionPageViewModel();
            animacionPageViewModel.Navigation = this.Navigation;
        }
	}
}