using DVS.Models;
using System.Collections.ObjectModel;

namespace DVS.ViewModels
{
    public class EmployeesClothesListViewViewModel : ViewModelBase
    {
        // Bereitstellung einer ObservableCollection der Mitarbeiter-Kleidung
        private readonly ObservableCollection<EmployeeModel> _employeeClothesList;

        // Zur encapsulation (private) von "_employeeClothesList" wird ein IEnumerable als pointer verwendet.
        public IEnumerable<EmployeeModel> EmployeeClothesList => _employeeClothesList;

        public EmployeesClothesListViewViewModel()
        {
            _employeeClothesList = [];

            FillEL();
        }

        private EmployeesClothesListViewViewModel _selectedEmployeeClothesItem;
        public EmployeesClothesListViewViewModel SelectedEmployeeClothesItem
        {
            get
            {
                return _selectedEmployeeClothesItem;
            }
            set
            {
                _selectedEmployeeClothesItem = value;
            }
        }

        private void FillEL()
        {
            _employeeClothesList.Add(new EmployeeModel(000, "Jonas", "Engelen"));
            _employeeClothesList.Add(new EmployeeModel(000, "Jonas", "Engelen"));
            _employeeClothesList.Add(new EmployeeModel(000, "Jonas", "Engelen"));
            _employeeClothesList.Add(new EmployeeModel(000, "Jonas", "Engelen"));
            _employeeClothesList.Add(new EmployeeModel(000, "Jonas", "Engelen"));
            _employeeClothesList.Add(new EmployeeModel(000, "Jonas", "Engelen"));
            _employeeClothesList.Add(new EmployeeModel(123, "Markus", "Oettken"));
            _employeeClothesList.Add(new EmployeeModel(123, "Markus", "Oettken"));
            _employeeClothesList.Add(new EmployeeModel(123, "Markus", "Oettken"));
            _employeeClothesList.Add(new EmployeeModel(123, "Markus", "Oettken"));
            _employeeClothesList.Add(new EmployeeModel(456, "Nadine", "Molik"));
            _employeeClothesList.Add(new EmployeeModel(456, "Nadine", "Molik"));
            _employeeClothesList.Add(new EmployeeModel(456, "Nadine", "Molik"));
            _employeeClothesList.Add(new EmployeeModel(456, "Nadine", "Molik"));
            _employeeClothesList.Add(new EmployeeModel(134, "Kemal", "Yüksel"));
            _employeeClothesList.Add(new EmployeeModel(134, "Kemal", "Yüksel"));
            _employeeClothesList.Add(new EmployeeModel(134, "Kemal", "Yüksel"));
            _employeeClothesList.Add(new EmployeeModel(133, "Irfan", "Yüksel"));
            _employeeClothesList.Add(new EmployeeModel(133, "Irfan", "Yüksel"));
            _employeeClothesList.Add(new EmployeeModel(133, "Irfan", "Yüksel"));
        }
    }
}
