using DVS.Models;
using DVS.ViewModels.ListViewItems;
using System.Collections.ObjectModel;

namespace DVS.ViewModels
{
    public class DVSEmployeesListingViewModel : ViewModelBase
    {
        private readonly ObservableCollection<EmployeeListingItemViewModel> _employeeListingItemCollection;
        public IEnumerable<EmployeeListingItemViewModel> EmployeeListingItemCollection => _employeeListingItemCollection;

        public DVSEmployeesListingViewModel()
        {
            _employeeListingItemCollection = [];

            Fill_employeeListingItemCollection();
            
        }










        public void Fill_employeeListingItemCollection()
        {
            var employee1 = new EmployeeModel("1324", "Engelen", "Jonas", "Vertrag läuft am 31.07.24 aus lbabla" +
                "lblb albl ablablbal balabla blablabalba lbalablab lablab alba lbalab lablabla blai!!!");
            employee1.Clothes.Add(new ClothesModel("111", "Sommershirt", "Shirt", "XL", "Sommer", 2, null));
            employee1.Clothes.Add(new ClothesModel("112", "Sommershirt", "Shirt", "L", "Sommer", 5, null));
            employee1.Clothes.Add(new ClothesModel("113", "Sommershirt", "Shirt", "M", "Sommer", 10, null));

            var employee2 = new EmployeeModel("4567", "Mustermann", "Max", null);
            employee2.Clothes.Add(new ClothesModel("114", "Wintershirt", "Shirt", "XL", "Winter", 8, null));
            employee2.Clothes.Add(new ClothesModel("115", "Wintershirt", "Shirt", "L", "Winter", 5, null));
            employee2.Clothes.Add(new ClothesModel("111", "Sommershirt", "Shirt", "XL", "Sommer", 12, null));
            employee2.Clothes.Add(new ClothesModel("114", "Wintershirt", "Shirt", "XL", "Winter", 8, null));
            employee2.Clothes.Add(new ClothesModel("115", "Wintershirt", "Shirt", "L", "Winter", 5, null));
            employee2.Clothes.Add(new ClothesModel("111", "Sommershirt", "Shirt", "XL", "Sommer", 12, null));

            var employee3 = new EmployeeModel("1596", "Musterfrau", "Mona", null);
            employee3.Clothes.Add(new ClothesModel("111", "Sommershirt", "Shirt", "S", "Sommer", 12, null));

            _employeeListingItemCollection.Add(new EmployeeListingItemViewModel(employee1));
            _employeeListingItemCollection.Add(new EmployeeListingItemViewModel(employee2));
            _employeeListingItemCollection.Add(new EmployeeListingItemViewModel(employee3));
        }
    }
}
