using Contratamos.Models;
using Contratamos.Servicios;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace Contratamos.ViewModel
{
    public class OfertasEmpleosViewModel : ViewModelBase
    {
        public INavigation Navigation { get; set; }

        private string _nombreAplicacion;
        public string NombreAplicacion
        {
            get { return _nombreAplicacion; }
            set { SetProperty(ref _nombreAplicacion, value); }
        }

        public List<Profesiones> ListaDeProfesiones { set; get; }

        private Profesiones _selectedProfesion;
        public Profesiones SelectedProfesion
        {
            get
            {
                return _selectedProfesion;
            }
            set
            {
                SetProperty(ref _selectedProfesion, value);
            }
        }
        private string _profesionText;
        public string ProfesionText
        {
            get
            {
                return _profesionText;
            }
            set
            {
                SetProperty(ref _profesionText, value);
            }
        }

        public OfertasEmpleosViewModel()
        {
            ListaDeProfesiones = ProfesionServices.ObtenerProfesiones().OrderBy(p => p.IdProfesion).ToList();
        }
    }
}
