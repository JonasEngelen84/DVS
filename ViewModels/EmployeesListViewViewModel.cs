using DVS.Models;
using DVS.Stores;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace DVS.ViewModels
{
    public class EmployeesListViewViewModel : ViewModelBase
    {
        // Bereitstellung einer ObservableCollection der Mitarbeiter-Kleidung "_allEmployeeClothesCollection".
        private readonly ObservableCollection<EmployeeModel> _employeeList;

        // Zur encapsulation (private) von "_allEmployeeClothesCollection" wird ein IEnumerable als pointer verwendet.
        public IEnumerable<EmployeeModel> EmployeeList => _employeeList;

        public EmployeesListViewViewModel(SelectedClothesStore _selectedClothesStore)
        {
            _employeeList = [];

            HardCodedObjects();
        }

        private void HardCodedObjects()
        {
            _employeeList.Add(new EmployeeModel("Jonas", "Engelen", 001));
            _employeeList[0].Clothes[0, 0] = ClothesListViewViewModel.ClothesCollection
            //_employeeList.Add(new EmployeeModel("Shirt", "Wintershirt", "M", "Winter", 2, 666, "Jonas", "Engelen"));
            //_employeeList.Add(new EmployeeModel("Hose", "Sommerhose", "54", "Sommer", 3, 666, "Jonas", "Engelen"));
            //_employeeList.Add(new EmployeeModel("Hose", "Winterhose", "54", "Winter", 2, 666, "Jonas", "Engelen"));
            //_employeeList.Add(new EmployeeModel("Jacke", "Regenjacke", "L", "", 1, 666, "Jonas", "Engelen"));
            //_employeeList.Add(new EmployeeModel("Jacke", "Fleecejacke", "S", "", 1, 666, "Jonas", "Engelen"));

            _employeeList.Add(new EmployeeModel("Markus", "Oettken", 002));
            //_employeeList.Add(new EmployeeModel("Hose", "Sommerhose", "56", "Sommer", 2, 123, "Markus", "Oettken"));
            //_employeeList.Add(new EmployeeModel("Hose", "Winterhose", "56", "Winter", 2, 123, "Markus", "Oettken"));
            //_employeeList.Add(new EmployeeModel("Jacke", "Regenjacke", "L", "", 1, 123, "Markus", "Oettken"));
            //_employeeList.Add(new EmployeeModel("Jacke", "Winterjacke", "XL", "Winter", 1, 123, "Markus", "Oettken"));

            _employeeList.Add(new EmployeeModel("Nadine", "Molik", 003));
            //_employeeList.Add(new EmployeeModel("Shirt", "Wintershirt", "M", "Winter", 1, 456, "Nadine", "Molik"));
            //_employeeList.Add(new EmployeeModel("Hose", "Sommerhose", "48", "Sommer", 1, 456, "Nadine", "Molik"));
            //_employeeList.Add(new EmployeeModel("Hose", "Winterhose", "48", "Winter", 1, 456, "Nadine", "Molik"));
            //_employeeList.Add(new EmployeeModel("Jacke", "Regenjacke", "L", "", 1, 456, "Nadine", "Molik"));
        }
    }
}
