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

            LoadEmployeeListingItemCollection();
            
        }










        public void LoadEmployeeListingItemCollection()
        {
            var employee1 = new EmployeeModel("1324", "Engelen", "Jonas", "Vertrag läuft am 31.07.24 aus lbabla" +
                "lblb albl ablablbal balabla blablabalba lbalablab lablab alba lbalab lablabla bla!!!");
            //employee1.Clothes.Add(new EmployeeClothesModel(new ClothesModel("111", "Sommershirt", "Shirt", "Sommer", null), 2));

            var employee2 = new EmployeeModel("4567", "Mustermann", "Max", null);
            //employee2.Clothes.Add(new EmployeeClothesModel(new ClothesModel("111", "Sommershirt", "Shirt", "Sommer", null), 2));

            var employee3 = new EmployeeModel("1596", "Musterfrau", "Mona", null);
            //employee3.Clothes.Add(new EmployeeClothesModel(new ClothesModel("111", "Sommershirt", "Shirt", "Sommer", null), 2));

            _employeeListingItemCollection.Add(new EmployeeListingItemViewModel(employee1));
            _employeeListingItemCollection.Add(new EmployeeListingItemViewModel(employee2));
            _employeeListingItemCollection.Add(new EmployeeListingItemViewModel(employee3));
        }
    }
}
