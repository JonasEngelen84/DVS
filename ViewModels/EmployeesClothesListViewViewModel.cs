using DVS.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace DVS.ViewModels
{
    public class EmployeesClothesListViewViewModel : ViewModelBase
    {
        // Bereitstellung einer ObservableCollection der Mitarbeiter-Kleidung "_allEmployeeClothesCollection".
        private readonly ObservableCollection<EmployeeModel> _employeeClothesList;

        // Zur encapsulation (private) von "_allEmployeeClothesCollection" wird ein IEnumerable als pointer verwendet.
        public IEnumerable<EmployeeModel> EmployeeClothesList => _employeeClothesList;

        public EmployeesClothesListViewViewModel()
        {
            _employeeClothesList = [];

            HardCodedObjects();
        }

        private void HardCodedObjects()
        {
            _employeeClothesList.Add(new EmployeeModel("Jonas", "Engelen", 001, "Sommershirt", "XL", 4));
            _employeeClothesList.Add(new EmployeeModel("Jonas", "Engelen", 001, "Wintershirt", "M", 2));
            _employeeClothesList.Add(new EmployeeModel("Jonas", "Engelen", 001, "Sommerhose", "54", 3));
            _employeeClothesList.Add(new EmployeeModel("Jonas", "Engelen", 001, "Winterhose", "54", 2));
            _employeeClothesList.Add(new EmployeeModel("Jonas", "Engelen", 001, "Regenjacke", "L", 1));
            _employeeClothesList.Add(new EmployeeModel("Jonas", "Engelen", 001, "Fleecejacke", "S", 1));
            _employeeClothesList.Add(new EmployeeModel("Jonas", "Engelen", 001, "Winterjacke", "XL", 1));
            _employeeClothesList.Add(new EmployeeModel("Jonas", "Engelen", 001, "Sommerkappe", "", 1));
            _employeeClothesList.Add(new EmployeeModel("Jonas", "Engelen", 001, "Winterkappe", "", 1));

            _employeeClothesList.Add(new EmployeeModel("Markus", "Oettken", 123, "Sommershirt", "XL", 2));
            _employeeClothesList.Add(new EmployeeModel("Markus", "Oettken", 123, "Sommerhose", "56", 2));
            _employeeClothesList.Add(new EmployeeModel("Markus", "Oettken", 123, "Winterhose", "56", 2));
            _employeeClothesList.Add(new EmployeeModel("Markus", "Oettken", 123, "Regenjacke", "L", 1));
            _employeeClothesList.Add(new EmployeeModel("Markus", "Oettken", 123, "Winterjacke", "XL", 1));

            _employeeClothesList.Add(new EmployeeModel("Nadine", "Molik", 456, "Sommershirt", "XL", 1));
            _employeeClothesList.Add(new EmployeeModel("Nadine", "Molik", 456, "Wintershirt", "M", 1));
            _employeeClothesList.Add(new EmployeeModel("Nadine", "Molik", 456, "Sommerhose", "48", 1));
            _employeeClothesList.Add(new EmployeeModel("Nadine", "Molik", 456, "Winterhose", "48", 1));
            _employeeClothesList.Add(new EmployeeModel("Nadine", "Molik", 456, "Regenjacke", "L", 1));
            _employeeClothesList.Add(new EmployeeModel("Nadine", "Molik", 456, "Fleecejacke", "S", 1));
            _employeeClothesList.Add(new EmployeeModel("Nadine", "Molik", 456, "Winterjacke", "XL", 1));
            _employeeClothesList.Add(new EmployeeModel("Nadine", "Molik", 456, "Sommerkappe", "", 1));
            _employeeClothesList.Add(new EmployeeModel("Nadine", "Molik", 456, "Winterkappe", "", 1));
        }
    }
}
