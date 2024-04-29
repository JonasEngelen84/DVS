using DVS.Models;
using System.Collections.ObjectModel;

namespace DVS.ViewModels
{
    public class EmployeesClothesListViewViewModel : ViewModelBase
    {
        // Bereitstellung einer ObservableCollection der Mitarbeiter-Kleidung
        // (Kleidungsliste der Mitarbeiter aufgelöst für ListView).
        private readonly ObservableCollection<EmployeeClothesModel> _employeeClothesList;

        // Zur encapsulation (private) von "_employeeClothesList"
        // wird ein IEnumerable als pointer verwendet.
        public IEnumerable<EmployeeClothesModel> EmployeeClothesList => _employeeClothesList;

        public EmployeesClothesListViewViewModel()
        {
            _employeeClothesList = [];

            FillEL();
        }

        private void FillEL()
        {
            _employeeClothesList.Add(new EmployeeClothesModel(000, "Jonas", "Engelen", 111, "Sommershirt", "XL", 4));
            _employeeClothesList.Add(new EmployeeClothesModel(000, "Jonas", "Engelen", 114, "Wintershirt", "XL", 3));
            _employeeClothesList.Add(new EmployeeClothesModel(000, "Jonas", "Engelen", 311, "Winterhose", "58", 2));
            _employeeClothesList.Add(new EmployeeClothesModel(000, "Jonas", "Engelen", 411, "Regenjacke", "XL", 1));
            _employeeClothesList.Add(new EmployeeClothesModel(000, "Jonas", "Engelen", 511, "Fleecejacke", "XL", 1));
            _employeeClothesList.Add(new EmployeeClothesModel(000, "Jonas", "Engelen", 611, "Sommerkappe", "", 1));
            _employeeClothesList.Add(new EmployeeClothesModel(123, "Markus", "Oettken", 112, "Sommershirt", "L", 6));
            _employeeClothesList.Add(new EmployeeClothesModel(123, "Markus", "Oettken", 115, "Wintershirt", "L", 4));
            _employeeClothesList.Add(new EmployeeClothesModel(123, "Markus", "Oettken", 212, "Sommerhose", "55", 6));
            _employeeClothesList.Add(new EmployeeClothesModel(123, "Markus", "Oettken", 412, "Regenjacke", "L", 1));
            _employeeClothesList.Add(new EmployeeClothesModel(456, "Nadine", "Molik", 112, "Sommershirt", "L", 6));
            _employeeClothesList.Add(new EmployeeClothesModel(456, "Nadine", "Molik", 115, "Wintershirt", "L", 4));
            _employeeClothesList.Add(new EmployeeClothesModel(456, "Nadine", "Molik", 212, "Sommerhose", "55", 6));
            _employeeClothesList.Add(new EmployeeClothesModel(456, "Nadine", "Molik", 412, "Regenjacke", "L", 1));
            _employeeClothesList.Add(new EmployeeClothesModel(134, "Kemal", "Yüksel", 111, "Sommershirt", "XL", 4));
            _employeeClothesList.Add(new EmployeeClothesModel(134, "Kemal", "Yüksel", 114, "Wintershirt", "XL", 3));
            _employeeClothesList.Add(new EmployeeClothesModel(134, "Kemal", "Yüksel", 511, "Fleecejacke", "XL", 1));
            _employeeClothesList.Add(new EmployeeClothesModel(133, "Irfan", "Yüksel", 114, "Wintershirt", "XL", 3));
            _employeeClothesList.Add(new EmployeeClothesModel(133, "Irfan", "Yüksel", 311, "Winterhose", "58", 2));
            _employeeClothesList.Add(new EmployeeClothesModel(133, "Irfan", "Yüksel", 412, "Regenjacke", "L", 1));
        }
    }
}
