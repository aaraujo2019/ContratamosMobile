using Contratamos.Models;
using Contratamos.Servicios;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace Contratamos.ViewModel
{
    public class PagPrincipalViewModel : ViewModelBase
    {
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

        private bool _isVisiblePicker;
        public bool IsVisiblePicker
        {
            get { return _isVisiblePicker; }
            set { SetProperty(ref _isVisiblePicker, value); }
        }

        private bool _isVisibleTexto;
        public bool IsVisibleTexto
        {
            get { return _isVisibleTexto; }
            set { SetProperty(ref _isVisibleTexto, value); }
        }

        public INavigation Navigation { get; set; }

        public PagPrincipalViewModel()
        {
            ListaDeProfesiones = ProfesionServices.ObtenerProfesiones().OrderBy(p => p.IdProfesion).ToList();    
        }
    }
}
