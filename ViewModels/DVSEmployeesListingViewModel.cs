using DVS.Models;
using DVS.Stores;
using DVS.ViewModels.ListViewItems;
using System.Collections.ObjectModel;

namespace DVS.ViewModels
{
    public class DVSEmployeesListingViewModel : ViewModelBase
    {
        private readonly EmployeeStore _employeeStore;

        private readonly ObservableCollection<EmployeeListingItemViewModel> _employeeListingItemCollection;
        public IEnumerable<EmployeeListingItemViewModel> EmployeeListingItemCollection => _employeeListingItemCollection;


        public DVSEmployeesListingViewModel(EmployeeStore employeeStore)
        {
            _employeeStore = employeeStore;
            _employeeListingItemCollection = [];

            EmployeeStore_EmployeesLoaded();
        }


        protected override void Dispose()
        {
            _employeeStore.EmployeeAdded -= EmployeeStore_EmployeeAdded;
            _employeeStore.EmployeesLoaded -= EmployeeStore_EmployeesLoaded;

            base.Dispose();
        }

        public void EmployeeStore_EmployeesLoaded()
        {
            _employeeListingItemCollection.Clear();

            foreach (EmployeeModel employee in _employeeStore.Employees)
            {
                EmployeeStore_EmployeeAdded(employee);
            }
        }

        public void EmployeeStore_EmployeeAdded(EmployeeModel employee)
        {
            EmployeeListingItemViewModel item = new(employee);
            _employeeListingItemCollection.Add(item);
        }

        private void ClotheStore_ClothesEdit(ClothesModel clothes)
        {

        }
    }
}
