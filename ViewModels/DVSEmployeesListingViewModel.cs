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

        private readonly ObservableCollection<DetailedEmployeeListingItemModel> _detailedEmployeeListingItemCollection;
        public IEnumerable<DetailedEmployeeListingItemModel> DetailedEmployeeListingItemCollection => _detailedEmployeeListingItemCollection;

        public DVSEmployeesListingViewModel(EmployeeStore employeeStore)
        {
            _employeeStore = employeeStore;
            _employeeListingItemCollection = [];
            _detailedEmployeeListingItemCollection = [];

            EmployeeStore_EmployeesLoaded();
            _employeeStore.EmployeeAdded += EmployeeStore_EmployeeAdded;
            _employeeStore.EmployeesLoaded += EmployeeStore_EmployeesLoaded;
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
            _detailedEmployeeListingItemCollection.Clear();

            foreach (EmployeeModel employee in _employeeStore.Employees)
            {
                EmployeeStore_EmployeeAdded(employee);
            }
        }

        public void EmployeeStore_EmployeeAdded(EmployeeModel employee)
        {
            EmployeeListingItemViewModel item = new(employee);
            _employeeListingItemCollection.Add(item);

            //TODO: Employee_Added verbessern
            if (employee.Clothes.Count == 0)
            {
                string employeeID = employee.ID;
                string employeeLastname = employee.Lastname;
                string employeeFirstname = employee.Firstname;
                string? clothesID = null;
                string? clothesName = null;
                string? clothesSize = null;
                int? clothesQuantity = null;
                string? clothesComment = null;

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
                    string? clothesComment = clothes.Comment;

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
