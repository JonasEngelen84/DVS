using DVS.Models;
using DVS.Stores;
using DVS.ViewModels.ListViewItems;
using System.Collections.ObjectModel;

namespace DVS.ViewModels
{
    public class DVSListingViewModel : ViewModelBase
    {
        private readonly ClothesStore _clothesStore;
        private readonly EmployeeStore _employeeStore;

        private readonly ObservableCollection<ClothesListingItemViewModel> _clothesListingItemCollection;
        public IEnumerable<ClothesListingItemViewModel> ClothesListingItemCollection => _clothesListingItemCollection;

        private readonly ObservableCollection<DetailedClothesListingItemModel> _detailedClothesListingItemCollection;
        public IEnumerable<DetailedClothesListingItemModel> DetailedClothesListingItemCollection => _detailedClothesListingItemCollection;

        private readonly ObservableCollection<EmployeeListingItemViewModel> _employeeListingItemCollection;
        public IEnumerable<EmployeeListingItemViewModel> EmployeeListingItemCollection => _employeeListingItemCollection;

        private readonly ObservableCollection<DetailedEmployeeListingItemModel> _detailedEmployeeListingItemCollection;
        public IEnumerable<DetailedEmployeeListingItemModel> DetailedEmployeeListingItemCollection => _detailedEmployeeListingItemCollection;


        public DVSListingViewModel(ClothesStore clothesStore, EmployeeStore employeeStore)
        {
            _clothesStore = clothesStore;
            _employeeStore = employeeStore;

            _clothesListingItemCollection = [];
            _detailedClothesListingItemCollection = [];
            _employeeListingItemCollection = [];
            _detailedEmployeeListingItemCollection = [];

            ClothesStore_ClothesLoaded();
            EmployeeStore_EmployeesLoaded();

            _clothesStore.ClothesLoaded += ClothesStore_ClothesLoaded;
            _clothesStore.ClothesAdded += ClothesStore_ClothesAdded;

            _employeeStore.EmployeeAdded += EmployeeStore_EmployeeAdded;
            _employeeStore.EmployeesLoaded += EmployeeStore_EmployeesLoaded;
        }


        protected override void Dispose()
        {
            _clothesStore.ClothesLoaded -= ClothesStore_ClothesLoaded;
            _clothesStore.ClothesAdded -= ClothesStore_ClothesAdded;

            _employeeStore.EmployeeAdded -= EmployeeStore_EmployeeAdded;
            _employeeStore.EmployeesLoaded -= EmployeeStore_EmployeesLoaded;

            base.Dispose();
        }

        public void ClothesStore_ClothesLoaded()
        {
            _clothesListingItemCollection.Clear();
            _detailedClothesListingItemCollection.Clear();
            _employeeListingItemCollection.Clear();
            _detailedEmployeeListingItemCollection.Clear();

            foreach (ClothesModel clothes in _clothesStore.Clothes)
            {
                ClothesStore_ClothesAdded(clothes);
            }
            
            foreach (EmployeeModel employee in _employeeStore.Employees)
            {
                EmployeeStore_EmployeeAdded(employee);
            }
        }

        private void ClothesStore_ClothesAdded(ClothesModel clothes)
        {
            ClothesListingItemViewModel item = new(clothes);
            _clothesListingItemCollection.Add(item);

            foreach (ClothesSizeModel size in clothes.Sizes)
            {
                _detailedClothesListingItemCollection.Add(new DetailedClothesListingItemModel(clothes.ID,
                                                                                              clothes.Name,
                                                                                              clothes.Categorie,
                                                                                              clothes.Season,
                                                                                              size.Size,
                                                                                              size.Quantity,
                                                                                              clothes.Comment));
            }
        }

        private void ClotheStore_ClothesEdit(ClothesModel clothes)
        {

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
