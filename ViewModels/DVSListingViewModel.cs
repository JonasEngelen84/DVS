using DVS.Commands.DragNDropCommands;
using DVS.Models;
using DVS.Stores;
using DVS.ViewModels.ListViewItems;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace DVS.ViewModels
{
    public class DVSListingViewModel : ViewModelBase
    {
        private readonly ClothesStore _clothesStore;
        private readonly EmployeeStore _employeeStore;

        private readonly ObservableCollection<ClothesListingItemViewModel> _clothesListingItemCollection;
        public IEnumerable<ClothesListingItemViewModel> ClothesListingItemCollection => _clothesListingItemCollection;

        private readonly ObservableCollection<DetailedClothesListingItemViewModel> _detailedClothesListingItemCollection;
        public IEnumerable<DetailedClothesListingItemViewModel> DetailedClothesListingItemCollection => _detailedClothesListingItemCollection;

        private readonly ObservableCollection<EmployeeListingItemViewModel> _employeeListingItemCollection;
        public IEnumerable<EmployeeListingItemViewModel> EmployeeListingItemCollection => _employeeListingItemCollection;

        private readonly ObservableCollection<DetailedEmployeeListingItemViewModel> _detailedEmployeeListingItemCollection;
        public IEnumerable<DetailedEmployeeListingItemViewModel> DetailedEmployeeListingItemCollection => _detailedEmployeeListingItemCollection;
        
        private readonly ObservableCollection<DetailedClothesListingItemViewModel> _newEmployeeListingItemCollection;
        public IEnumerable<DetailedClothesListingItemViewModel> NewEmployeeListingItemCollection => _newEmployeeListingItemCollection;

        private DetailedClothesListingItemViewModel _incomingClothesListingItemModel;
        public DetailedClothesListingItemViewModel IncomingClothesListingItemModel
        {
            get
            {
                return _incomingClothesListingItemModel;
            }
            set
            {
                _incomingClothesListingItemModel = value;
                OnPropertyChanged(nameof(IncomingClothesListingItemModel));
            }
        }

        private DetailedClothesListingItemViewModel _removedClothesListingItemModel;
        public DetailedClothesListingItemViewModel RemovedClothesListingItemModel
        {
            get
            {
                return _removedClothesListingItemModel;
            }
            set
            {
                _removedClothesListingItemModel = value;
                OnPropertyChanged(nameof(RemovedClothesListingItemModel));
            }
        }

        public ICommand ClothesItemReceivedCommand { get; }
        public ICommand ClothesItemRemovedCommand { get; }


        public DVSListingViewModel(ClothesStore clothesStore, EmployeeStore employeeStore)
        {
            _clothesListingItemCollection = [];
            _detailedClothesListingItemCollection = [];
            _employeeListingItemCollection = [];
            _detailedEmployeeListingItemCollection = [];
            _newEmployeeListingItemCollection = [];

            _clothesStore = clothesStore;
            _employeeStore = employeeStore;

            ClothesItemReceivedCommand = new ClothesItemReceivedCommand(this);
            ClothesItemRemovedCommand = new ClothesItemRemovedCommand(this);

            ClothesStore_ClothesLoaded();
            EmployeeStore_EmployeesLoaded();

            _clothesStore.ClothesLoaded += ClothesStore_ClothesLoaded;
            _clothesStore.ClothesAdded += ClothesStore_ClothesAdded;

            _employeeStore.EmployeeAdded += EmployeeStore_EmployeeAdded;
            _employeeStore.EmployeesLoaded += EmployeeStore_EmployeesLoaded;
        }


        public void AddClothesItemToNewEmployeeListingItemCollection(DetailedClothesListingItemViewModel item)
        {
            if (item == null || _newEmployeeListingItemCollection.Contains(item))
            {
                return;
            }

            var existingItem = _newEmployeeListingItemCollection
                .FirstOrDefault(modelItem => modelItem.ID == item.ID && modelItem.Size == item.Size);

            if (existingItem != null)
            {
                existingItem.Quantity++;
            }
            else
            {
                DetailedClothesListingItemViewModel newItem = new(
                item.ID, item.Name, item.Categorie, item.Season, item.Size, 1, null);

                _newEmployeeListingItemCollection.Add(newItem);
            }
        }
        
        public void AddClothesItemToDetailedClothesListingItemCollection(DetailedClothesListingItemViewModel item)
        {
            if (item == null || _detailedClothesListingItemCollection.Contains(item))
            {
                return;
            }

            var existingItem = _detailedClothesListingItemCollection
                .FirstOrDefault(modelItem => modelItem.ID == item.ID && modelItem.Size == item.Size);

            if (existingItem != null)
            {
                existingItem.Quantity++;
            }
            else
            {
                DetailedClothesListingItemViewModel newItem = new(
                item.ID, item.Name, item.Categorie, item.Season, item.Size, 1, null);

                _detailedClothesListingItemCollection.Add(newItem);
            }
        }

        public void RemoveClothesItemFromNewEmployeeListingItemCollection(DetailedClothesListingItemViewModel item)
        {
            if (item == null || !_newEmployeeListingItemCollection.Contains(item))
            {
                return;
            }

            if (item.Quantity > 1)
                item.Quantity--;
            else
                _newEmployeeListingItemCollection.Remove(item);
        }

        public void RemoveClothesItemFromDetailedClothesListingItemCollection(DetailedClothesListingItemViewModel item)
        {
            if (item == null || !_detailedClothesListingItemCollection.Contains(item))
            {
                // Das Element ist nicht in der Liste, nichts tun
                return;
            }

            if (item.Quantity > 1)
                item.Quantity--;
            else
                _detailedClothesListingItemCollection.Remove(item);
        }

        public void ClothesStore_ClothesLoaded()
        {
            _clothesListingItemCollection.Clear();
            _detailedClothesListingItemCollection.Clear();

            foreach (ClothesModel clothes in _clothesStore.Clothes)
            {
                ClothesStore_ClothesAdded(clothes);
            }
        }

        private void ClothesStore_ClothesAdded(ClothesModel clothes)
        {
            string ID = clothes.ID;
            string Name = clothes.Name;
            string Categorie = clothes.Categorie;
            string Season = clothes.Season;

            if (clothes.Sizes.Count == 0)
            {
                _detailedClothesListingItemCollection.Add(new DetailedClothesListingItemViewModel(
                    ID, Name, Categorie, Season, null, null, null));
            }
            else
            {
                foreach (ClothesSizeModel size in clothes.Sizes)
                {
                    _detailedClothesListingItemCollection.Add(new DetailedClothesListingItemViewModel(
                        ID, Name, Categorie, Season, size.Size, size.Quantity, size.Comment));

                }
            }

            _clothesListingItemCollection.Add(new ClothesListingItemViewModel(clothes));
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
            string employeeID = employee.ID;
            string employeeLastname = employee.Lastname;
            string employeeFirstname = employee.Firstname;

            if (employee.Clothes.Count == 0)
            {
                _detailedEmployeeListingItemCollection.Add(new DetailedEmployeeListingItemViewModel(
                    employeeID, employeeLastname, employeeFirstname, null, null, null, null, null));
            }
            else
            {
                foreach (DetailedClothesListingItemViewModel clothes in employee.Clothes)
                {
                    _detailedEmployeeListingItemCollection.Add(new DetailedEmployeeListingItemViewModel(
                        employeeID, employeeLastname, employeeFirstname, clothes.ID,
                        clothes.Name, clothes.Size, clothes.Quantity, clothes.Comment));
                }
            }

            _employeeListingItemCollection.Add(new EmployeeListingItemViewModel(employee));
            _newEmployeeListingItemCollection.Clear();
        }

        private void EmployeeStore_EmployeeEdit(ClothesModel clothes)
        {

        }

        protected override void Dispose()
        {
            _clothesStore.ClothesLoaded -= ClothesStore_ClothesLoaded;
            _clothesStore.ClothesAdded -= ClothesStore_ClothesAdded;

            _employeeStore.EmployeeAdded -= EmployeeStore_EmployeeAdded;
            _employeeStore.EmployeesLoaded -= EmployeeStore_EmployeesLoaded;

            base.Dispose();
        }
    }
}
