using DVS.Models;
using DVS.ViewModels.ListViewItems;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Input;

namespace DVS.ViewModels
{
    public class EmployeesListingViewModel : ViewModelBase
    {
        private readonly ObservableCollection<EmployeeListingItemViewModel> _employeeListingItemCollection;
        public IEnumerable<EmployeeListingItemViewModel> EmployeeListingItemCollection => _employeeListingItemCollection;

        public EmployeesListingViewModel()
        {
            _employeeListingItemCollection = [];

            var employee1 = new EmployeeModel("1324", "Engelen", "Jonas", null);
            employee1.Clothes.Add(new ClothesModel("111", "Sommershirt", "Shirt", "XL", "Sommer", 12, null));
            employee1.Clothes.Add(new ClothesModel("112", "Sommershirt", "Shirt", "L", "Sommer", 8, null));
            employee1.Clothes.Add(new ClothesModel("113", "Sommershirt", "Shirt", "M", "Sommer", 10, null));

            var employee2 = new EmployeeModel("4567", "Yüksel", "Kemal", null);
            employee2.Clothes.Add(new ClothesModel("114", "Wintershirt", "Shirt", "XL", "Winter", 8, null));
            employee2.Clothes.Add(new ClothesModel("115", "Wintershirt", "Shirt", "L", "Winter", 5, null));
            employee2.Clothes.Add(new ClothesModel("111", "Sommershirt", "Shirt", "XL", "Sommer", 12, null));

            _employeeListingItemCollection.Add(new EmployeeListingItemViewModel(employee1));
            _employeeListingItemCollection.Add(new EmployeeListingItemViewModel(employee2));
            _employeeListingItemCollection.Add(new EmployeeListingItemViewModel(employee1));
            _employeeListingItemCollection.Add(new EmployeeListingItemViewModel(employee2));
            _employeeListingItemCollection.Add(new EmployeeListingItemViewModel(employee1));
            _employeeListingItemCollection.Add(new EmployeeListingItemViewModel(employee2));
            _employeeListingItemCollection.Add(new EmployeeListingItemViewModel(employee1));
            _employeeListingItemCollection.Add(new EmployeeListingItemViewModel(employee2));
            _employeeListingItemCollection.Add(new EmployeeListingItemViewModel(employee1));
            _employeeListingItemCollection.Add(new EmployeeListingItemViewModel(employee2));
            _employeeListingItemCollection.Add(new EmployeeListingItemViewModel(employee1));
            _employeeListingItemCollection.Add(new EmployeeListingItemViewModel(employee2));
            _employeeListingItemCollection.Add(new EmployeeListingItemViewModel(employee1));
            _employeeListingItemCollection.Add(new EmployeeListingItemViewModel(employee2));
            _employeeListingItemCollection.Add(new EmployeeListingItemViewModel(employee1));
            _employeeListingItemCollection.Add(new EmployeeListingItemViewModel(employee2));
            _employeeListingItemCollection.Add(new EmployeeListingItemViewModel(employee1));
            _employeeListingItemCollection.Add(new EmployeeListingItemViewModel(employee2));
            _employeeListingItemCollection.Add(new EmployeeListingItemViewModel(employee1));
            _employeeListingItemCollection.Add(new EmployeeListingItemViewModel(employee2));
            _employeeListingItemCollection.Add(new EmployeeListingItemViewModel(employee1));
            _employeeListingItemCollection.Add(new EmployeeListingItemViewModel(employee2));
            _employeeListingItemCollection.Add(new EmployeeListingItemViewModel(employee1));
            _employeeListingItemCollection.Add(new EmployeeListingItemViewModel(employee2));
            _employeeListingItemCollection.Add(new EmployeeListingItemViewModel(employee1));
            _employeeListingItemCollection.Add(new EmployeeListingItemViewModel(employee2));
        }

        
    }
}
