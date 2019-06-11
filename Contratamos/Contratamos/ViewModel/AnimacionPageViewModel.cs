using Contratamos.Generales;
using Xamarin.Forms;

namespace Contratamos.ViewModel
{
    public class AnimacionPageViewModel : ViewModelBase
    {
        public INavigation Navigation { get; set; }

        private string _nombreAplicacion;
        public string NombreAplicacion
        {
            get { return _nombreAplicacion; }
            set { SetProperty(ref _nombreAplicacion, value); }
        }
        public AnimacionPageViewModel()
        {
            NombreAplicacion = modGeneral.NombreAplicacion;
        }
    }
}
