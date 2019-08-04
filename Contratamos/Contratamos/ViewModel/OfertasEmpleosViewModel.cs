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
        public List<Ciudades> ListaCiudades { set; get; }

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

        private Ciudades _selectedCiudad;
        public Ciudades SelectedCiudad
        {
            get
            {
                return _selectedCiudad;
            }
            set
            {
                SetProperty(ref _selectedCiudad, value);
            }
        }
        private string _ciudadText;
        public string CiudadText
        {
            get
            {
                return _ciudadText;
            }
            set
            {
                SetProperty(ref _ciudadText, value);
            }
        }

        public OfertasEmpleosViewModel()
        {
            ListaDeProfesiones = ProfesionServices.ObtenerProfesiones().OrderBy(p => p.IdProfesion).ToList();
            ListaCiudades = clsCiudadesWs.CargarCiudades().OrderBy(c => c.NombreCiudad).ToList();
        }
    }
}
