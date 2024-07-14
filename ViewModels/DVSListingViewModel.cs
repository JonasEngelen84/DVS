using DVS.Commands.DragNDropCommands;
using DVS.Models;
using DVS.Stores;
using DVS.ViewModels.ListViewItems;
using System.Collections.ObjectModel;
using System.Windows;
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

        private readonly ObservableCollection<ClothesListingItemViewModel> _clothesListingItemCollection = [];
        public IEnumerable<ClothesListingItemViewModel> ClothesListingItemCollection => _clothesListingItemCollection;

        private readonly ObservableCollection<DetailedClothesListingItemModel> _detailedClothesListingItemCollection = [];
        public IEnumerable<DetailedClothesListingItemModel> DetailedClothesListingItemCollection => _detailedClothesListingItemCollection;

        private readonly ObservableCollection<EmployeeListingItemViewModel> _employeeListingItemCollection = [];
        public IEnumerable<EmployeeListingItemViewModel> EmployeeListingItemCollection => _employeeListingItemCollection;

        private readonly ObservableCollection<DetailedEmployeeListingItemModel> _detailedEmployeeListingItemCollection = [];
        public IEnumerable<DetailedEmployeeListingItemModel> DetailedEmployeeListingItemCollection => _detailedEmployeeListingItemCollection;
        
        private readonly ObservableCollection<DetailedClothesListingItemModel> _newEmployeeListingItemCollection = [];
        public IEnumerable<DetailedClothesListingItemModel> NewEmployeeListingItemCollection => _newEmployeeListingItemCollection;

        private readonly ClothesStore _clothesStore;
        private readonly EmployeeStore _employeeStore;
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly CategoryStore _categoryStore;
        private readonly SeasonStore _seasonStore;

        public ICommand ClothesItemReceivedCommand { get; }
        public ICommand ClothesItemRemovedCommand { get; }


        public DVSListingViewModel(ClothesStore clothesStore, EmployeeStore employeeStore,
            ModalNavigationStore modalNavigationStore, CategoryStore categoryStore, SeasonStore seasonStore)
        {
            _clothesStore = clothesStore;
            _employeeStore = employeeStore;
            _modalNavigationStore = modalNavigationStore;
            _categoryStore = categoryStore;
            _seasonStore = seasonStore;

            ClothesItemReceivedCommand = new ClothesItemReceivedCommand(this);
            ClothesItemRemovedCommand = new ClothesItemRemovedCommand(this);

            ClothesStore_ClothesLoaded();
            EmployeeStore_EmployeesLoaded();

            _clothesStore.ClothesLoaded += ClothesStore_ClothesLoaded;
            _clothesStore.ClothesAdded += ClothesStore_ClothesAdded;
            _clothesStore.ClothesEdited += ClothesStore_ClothesEdited;

            _employeeStore.EmployeeAdded += EmployeeStore_EmployeeAdded;
            _employeeStore.EmployeesLoaded += EmployeeStore_EmployeesLoaded;
        }


        public async Task AddClothesItemToDetailedClothesListingItemCollection(DetailedClothesListingItemModel item)
        {
            if (item == null || _detailedClothesListingItemCollection.Contains(item))
            {
                return;
            }

            var existingItem = _detailedClothesListingItemCollection
                .FirstOrDefault(modelItem => modelItem.ID == item.ID && modelItem.Size == item.Size);

            if (existingItem != null)
            {
                var size = item.ClothesModel.Sizes.FirstOrDefault(modelItem => modelItem.Size == item.Size);
                size.Quantity++;

                await _clothesStore.Edit(item.ClothesModel);
            }
        }

        public async Task RemoveClothesItemFromDetailedClothesListingItemCollectionAsync(DetailedClothesListingItemModel item)
        {
            if (item == null || !_detailedClothesListingItemCollection.Contains(item))
            {
                return;
            }

            if (item.Quantity == 0)
            {
                string messageBoxText = "Dieses Bekleidung ist nicht verfügbar!";
                string caption = "Bekleidung entfernen";
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Warning;
                _ = MessageBox.Show(messageBoxText, caption, button, icon);
            }
            else if (item.Quantity <= 3)
            {
                string messageBoxText = $"ACHTUNG!\n\nVon dieser Bekleidung sind nur noch  {item.Quantity}  Stück vorhanden!";
                string caption = "Bekleidung entfernen";
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Warning;
                _ = MessageBox.Show(messageBoxText, caption, button, icon);

                var size = item.ClothesModel.Sizes.FirstOrDefault(modelItem => modelItem.Size == item.Size);
                size.Quantity--;

                await _clothesStore.Edit(item.ClothesModel);
            }
            else
            {
                var size = item.ClothesModel.Sizes.FirstOrDefault(modelItem => modelItem.Size == item.Size);
                size.Quantity--;

                await _clothesStore.Edit(item.ClothesModel);
            }

        }
        
        public void AddClothesItemToNewEmployeeListingItemCollection(DetailedClothesListingItemModel item)
        {
            if (item == null || _newEmployeeListingItemCollection.Contains(item))
            {
                return;
            }

            var existingItem = _newEmployeeListingItemCollection
                .FirstOrDefault(modelItem => modelItem.ID == item.ID && modelItem.Size == item.Size);

            if (existingItem == null)
            {
                ClothesModel clothes = new(Guid.NewGuid(), item.ClothesModel.ID, item.ClothesModel.Name,
                                           item.ClothesModel.Category, item.ClothesModel.Season, null)
                                       {
                                           Sizes = [new ClothesSizeModel(Guid.NewGuid(), item.Size) { Quantity = 1 }]
                                       };

                DetailedClothesListingItemModel newItem = new(clothes, item.Size);
                _newEmployeeListingItemCollection.Add(newItem);
            }
            else
            {
                var size = existingItem.ClothesModel.Sizes.FirstOrDefault(modelItem => modelItem.Size == item.Size);
                size.Quantity++;
            }
            
        }

        public void RemoveClothesItemFromNewEmployeeListingItemCollection(DetailedClothesListingItemModel item)
        {
            if (item == null || !_newEmployeeListingItemCollection.Contains(item))
            {
                return;
            }

            if (item.Quantity == 1)
                _newEmployeeListingItemCollection.Remove(item);
            else
            {
                var size = item.ClothesModel.Sizes.FirstOrDefault(modelItem => modelItem.Size == item.Size);
                size.Quantity--;
            }
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
            if (clothes.Sizes.Count == 0)
            {
                _detailedClothesListingItemCollection.Add(new DetailedClothesListingItemModel(clothes, null));
            }
            else
            {
                foreach (ClothesSizeModel size in clothes.Sizes)
                {
                    _detailedClothesListingItemCollection.Add(new DetailedClothesListingItemModel(clothes, size.Size));
                }
            }

            _clothesListingItemCollection.Add(new ClothesListingItemViewModel(clothes, _modalNavigationStore, _categoryStore, _seasonStore, _clothesStore));
        }

        private void ClothesStore_ClothesEdited(ClothesModel clothes)
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
                    employeeID, employeeLastname, employeeFirstname, null, null, null, null, null));
            }
            else
            {
                foreach (DetailedClothesListingItemModel clothes in employee.Clothes)
                {
                    _detailedEmployeeListingItemCollection.Add(new DetailedEmployeeListingItemModel(
                        employeeID, employeeLastname, employeeFirstname, clothes.ID,
                        clothes.Name, clothes.Size, clothes.Quantity, clothes.Comment));
                }
            }

            _employeeListingItemCollection.Add(new EmployeeListingItemViewModel(
                employee, this, _modalNavigationStore));

            _newEmployeeListingItemCollection.Clear();
        }

        private void EmployeeStore_EmployeeEdit(ClothesModel clothes)
        {

        }

        protected override void Dispose()
        {
            _clothesStore.ClothesLoaded -= ClothesStore_ClothesLoaded;
            _clothesStore.ClothesAdded -= ClothesStore_ClothesAdded;
            _clothesStore.ClothesEdited -= ClothesStore_ClothesEdited;

            _employeeStore.EmployeeAdded -= EmployeeStore_EmployeeAdded;
            _employeeStore.EmployeesLoaded -= EmployeeStore_EmployeesLoaded;

            base.Dispose();
        }
    }
}
