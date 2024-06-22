using DVS.Models;
using DVS.Stores;
using System.Collections.ObjectModel;

namespace DVS.ViewModels
{
    public class DVSDetailedEmployeesListingViewModel : ViewModelBase
    {
        //private readonly SelectedClothesStore _selectedClothesStore;
        //private readonly SelectedEmployeeClothesStore _selectedEmployeeClothesStore;
        //private readonly ModalNavigationStore _modalNavigationStore;
        //private readonly DVSDetailedClothesListingViewModel _dVSDetailedClothesListingViewModel;
        private readonly EmployeeStore _employeeStore;

        private readonly ObservableCollection<DetailedEmployeeListingItemModel> _detailedEmployeeListingItemCollection;
        public IEnumerable<DetailedEmployeeListingItemModel> DetailedEmployeeListingItemCollection => _detailedEmployeeListingItemCollection;

        public DVSDetailedEmployeesListingViewModel(EmployeeStore employeeStore,
                                                    SelectedClothesStore selectedClothesStore,
                                                    SelectedEmployeeClothesStore selectedEmployeeClothesStore,
                                                    ModalNavigationStore modalNavigationStore)
        {
            //_selectedClothesStore = selectedClothesStore;
            //_selectedEmployeeClothesStore = selectedEmployeeClothesStore;
            //_modalNavigationStore = modalNavigationStore;
            _employeeStore = employeeStore;
            _detailedEmployeeListingItemCollection = [];
        
            EmployeeStore_EmployeesLoaded();
            _employeeStore.EmployeesLoaded += EmployeeStore_EmployeesLoaded;
            _employeeStore.EmployeeAdded += EmployeeStore_EmployeeAdded;
        }


        protected override void Dispose()
        {
            _employeeStore.EmployeesLoaded -= EmployeeStore_EmployeesLoaded;
            _employeeStore.EmployeeAdded -= EmployeeStore_EmployeeAdded;

            base.Dispose();
        }

        public void EmployeeStore_EmployeesLoaded()
        {
            _detailedEmployeeListingItemCollection.Clear();

            foreach (EmployeeModel employee in _employeeStore.Employees)
            {
                EmployeeStore_EmployeeAdded(employee);
            }
        }

        private void EmployeeStore_EmployeeAdded(EmployeeModel employee)
        {
            if (employee.Clothes.Count == 0)
            {
                string employeeID = employee.ID;
                string employeeLastname = employee.Lastname;
                string employeeFirstname = employee.Firstname;
                string clothesID = null;
                string clothesName = null;
                string clothesSize = null;
                int? clothesQuantity = null;
                string clothesComment = null;

                _detailedEmployeeListingItemCollection.Add(new DetailedEmployeeListingItemModel(employeeID,
                                                                                                employeeLastname,
                                                                                                employeeFirstname,
                                                                                                clothesID,
                                                                                                clothesName,
                                                                                                clothesSize,
                                                                                                clothesQuantity,
                                                                                                clothesComment));
            }
            else
            {
                foreach (DetailedClothesListingItemModel clothes in employee.Clothes)
                {
                    string employeeID = employee.ID;
                    string employeeLastname = employee.Lastname;
                    string employeeFirstname = employee.Firstname;
                    string clothesID = clothes.ID;
                    string clothesName = clothes.Name;
                    string clothesSize = clothes.Size;
                    int clothesQuantity = clothes.Quantity;
                    string clothesComment = clothes.Comment;

                    _detailedEmployeeListingItemCollection.Add(new DetailedEmployeeListingItemModel(employeeID,
                                                                                                    employeeLastname,
                                                                                                    employeeFirstname,
                                                                                                    clothesID,
                                                                                                    clothesName,
                                                                                                    clothesSize,
                                                                                                    clothesQuantity,
                                                                                                    clothesComment));
                }
                
            }
        }

        private void EmployeeStore_EmployeeEdit(ClothesModel clothes)
        {

        }
    }
}
