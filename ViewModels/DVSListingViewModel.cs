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
        private DetailedClothesListingItemModel _selectedDetailedClothesItem;
        public DetailedClothesListingItemModel SelectedDetailedClothesItem
        {
            get
            {
                return _selectedDetailedClothesItem;
            }
            set
            {
                if (_selectedDetailedClothesItem != value)
                {
                    _selectedDetailedClothesItem = value;
                    OnPropertyChanged(nameof(SelectedDetailedClothesItem));
                }
            }
        }

        private DetailedEmployeeListingItemModel _selectedDetailedEmployeeItem;
        public DetailedEmployeeListingItemModel SelectedDetailedEmployeeItem
        {
            get
            {
                return _selectedDetailedEmployeeItem;
            }
            set
            {
                if (_selectedDetailedEmployeeItem != value)
                {
                    _selectedDetailedEmployeeItem = value;
                    OnPropertyChanged(nameof(SelectedDetailedEmployeeItem));
                }
            }
        }

        private DetailedClothesListingItemModel _incomingClothesListingItemModel;
        public DetailedClothesListingItemModel IncomingClothesListingItemModel
        {
            get
            {
                return _incomingClothesListingItemModel;
            }
            set
            {
                if(_incomingClothesListingItemModel != value)
                {
                    _incomingClothesListingItemModel = value;
                    OnPropertyChanged(nameof(IncomingClothesListingItemModel));
                }
            }
        }

        private DetailedClothesListingItemModel _removedClothesListingItemModel;
        public DetailedClothesListingItemModel RemovedClothesListingItemModel
        {
            get
            {
                return _removedClothesListingItemModel;
            }
            set
            {
                if (_removedClothesListingItemModel != value)
                {
                    _removedClothesListingItemModel = value;
                    OnPropertyChanged(nameof(RemovedClothesListingItemModel));
                }
            }
        }

        private readonly ObservableCollection<ClothesListingItemViewModel> _clothesListingItemCollection;
        public IEnumerable<ClothesListingItemViewModel> ClothesListingItemCollection => _clothesListingItemCollection;

        private readonly ObservableCollection<DetailedClothesListingItemModel> _detailedClothesListingItemCollection;
        public IEnumerable<DetailedClothesListingItemModel> DetailedClothesListingItemCollection => _detailedClothesListingItemCollection;

        private readonly ObservableCollection<EmployeeListingItemViewModel> _employeeListingItemCollection;
        public IEnumerable<EmployeeListingItemViewModel> EmployeeListingItemCollection => _employeeListingItemCollection;

        private readonly ObservableCollection<DetailedEmployeeListingItemModel> _detailedEmployeeListingItemCollection;
        public IEnumerable<DetailedEmployeeListingItemModel> DetailedEmployeeListingItemCollection => _detailedEmployeeListingItemCollection;
        
        private readonly ObservableCollection<DetailedClothesListingItemModel> _newEmployeeListingItemCollection;
        public IEnumerable<DetailedClothesListingItemModel> NewEmployeeListingItemCollection => _newEmployeeListingItemCollection;

        private readonly ClothesStore _clothesStore;
        private readonly EmployeeStore _employeeStore;

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


        public void AddClothesItemToNewEmployeeListingItemCollection(DetailedClothesListingItemModel item)
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
                DetailedClothesListingItemModel newItem = new(
                item.ID, item.Name, item.Category, item.Season, item.Size, 1);

                _newEmployeeListingItemCollection.Add(newItem);
            }
        }
        
        public void AddClothesItemToDetailedClothesListingItemCollection(DetailedClothesListingItemModel item)
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
                DetailedClothesListingItemModel newItem = new(
                item.ID, item.Name, item.Category, item.Season, item.Size, 1);

                _detailedClothesListingItemCollection.Add(newItem);
            }
        }

        public void RemoveClothesItemFromNewEmployeeListingItemCollection(DetailedClothesListingItemModel item)
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

        public void RemoveClothesItemFromDetailedClothesListingItemCollection(DetailedClothesListingItemModel item)
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
            CategoryModel Categorie = clothes.Categorie;
            SeasonModel Season = clothes.Season;

            if (clothes.Sizes.Count == 0)
            {
                _detailedClothesListingItemCollection.Add(new DetailedClothesListingItemModel(
                    ID, Name, Categorie, Season, null, null));
            }
            else
            {
                foreach (ClothesSizeModel size in clothes.Sizes)
                {
                    _detailedClothesListingItemCollection.Add(new DetailedClothesListingItemModel(
                        ID, Name, Categorie, Season, size.Size, size.Quantity));

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
                _detailedEmployeeListingItemCollection.Add(new DetailedEmployeeListingItemModel(
                    employeeID, employeeLastname, employeeFirstname, null, null, null, null));
            }
            else
            {
                foreach (DetailedClothesListingItemModel clothes in employee.Clothes)
                {
                    _detailedEmployeeListingItemCollection.Add(new DetailedEmployeeListingItemModel(
                        employeeID, employeeLastname, employeeFirstname, clothes.ID,
                        clothes.Name, clothes.Size, clothes.Quantity));
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
