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

            ClothesItemReceivedCommand = new ClothesItemReceivedCommand(this, _clothesStore);
            ClothesItemRemovedCommand = new ClothesItemRemovedCommand(this, _clothesStore);

            ClothesStore_ClothesLoaded();
            EmployeeStore_EmployeesLoaded();

            _clothesStore.ClothesLoaded += ClothesStore_ClothesLoaded;
            _clothesStore.ClothesAdded += ClothesStore_ClothesAdded;
            _clothesStore.DetailedClothesItemAdded += ClothesStore_DetailedClothesItemAdded;
            _clothesStore.ClothesUpdated += ClothesStore_ClothesUpdated;
            _clothesStore.DetailedClothesItemUpdated += ClothesStore_DetailedClothesItemUpdated;
            _clothesStore.ClothesDeleted += ClothesStore_ClothesDeleted;

            _employeeStore.EmployeesLoaded += EmployeeStore_EmployeesLoaded;
            _employeeStore.EmployeeAdded += EmployeeStore_EmployeeAdded;
            _employeeStore.DetailedEmployeeItemAdded += EmployeeStore_DetailedEmployeeItemAdded;
            _employeeStore.EmployeeUpdated += EmployeeStore_EmployeeUpdated;
            _employeeStore.DetailedEmployeeItemUpdated += EmployeeStore_DetailedEmployeeItemUpdated;
            _employeeStore.EmployeeDeleted += EmployeeStore_EmployeeDeleted;
        }


        public void LoadNewEmployeeListingItemCollection(EmployeeModel employee)
        {
            foreach (ClothesModel clothes in employee.Clothes)
            {
                foreach (ClothesSizeModel size in clothes.Sizes)
                {
                    _newEmployeeListingItemCollection.Add(new(clothes, size.Size));

                }
            }
        }

        public void AddClothesItemToNewEmployeeListingItemCollection()
        {
            if (IncomingClothesListingItemModel == null || IncomingClothesListingItemModel.Quantity == 0)
            {
                return;
            }

            DetailedClothesListingItemModel? existingItem = _newEmployeeListingItemCollection
                .FirstOrDefault(modelItem => modelItem.ID == IncomingClothesListingItemModel.ID
                && modelItem.Size == IncomingClothesListingItemModel.Size);

            ClothesModel clothes = new(IncomingClothesListingItemModel.Clothes.GuidID,
                                       IncomingClothesListingItemModel.Clothes.ID,
                                       IncomingClothesListingItemModel.Clothes.Name,
                                       IncomingClothesListingItemModel.Clothes.Category,
                                       IncomingClothesListingItemModel.Clothes.Season,
                                       null);

            if (existingItem == null)
            {
                clothes.Sizes.Add(new (IncomingClothesListingItemModel.Size) { Quantity = 1 });
                DetailedClothesListingItemModel newItem = new(clothes, IncomingClothesListingItemModel.Size);
                _newEmployeeListingItemCollection.Add(newItem);
            }
            else
            {
                clothes.Sizes = existingItem.Clothes.Sizes;
                var size = existingItem.Clothes.Sizes.FirstOrDefault(modelItem => modelItem.Size == IncomingClothesListingItemModel.Size);
                size.Quantity++;
                existingItem.Update(clothes);
            }
        }

        public void RemoveClothesItemFromNewEmployeeListingItemCollection()
        {
            if (RemovedClothesListingItemModel == null || !_newEmployeeListingItemCollection.Contains(RemovedClothesListingItemModel))
            {
                return;
            }

            if (RemovedClothesListingItemModel.Quantity == 1)
                _newEmployeeListingItemCollection.Remove(RemovedClothesListingItemModel);
            else
            {
                DetailedClothesListingItemModel? existingItem = _newEmployeeListingItemCollection
                .FirstOrDefault(modelItem => modelItem.ID == IncomingClothesListingItemModel.ID
                && modelItem.Size == IncomingClothesListingItemModel.Size);

                ClothesModel clothes = new(IncomingClothesListingItemModel.Clothes.GuidID,
                                           IncomingClothesListingItemModel.Clothes.ID,
                                           IncomingClothesListingItemModel.Clothes.Name,
                                           IncomingClothesListingItemModel.Clothes.Category,
                                           IncomingClothesListingItemModel.Clothes.Season,
                                           null)
                {
                    Sizes = existingItem.Clothes.Sizes
                };

                var size = existingItem.Clothes.Sizes.FirstOrDefault(modelItem => modelItem.Size == IncomingClothesListingItemModel.Size);
                size.Quantity--;
                existingItem.Update(clothes);
            }
        }


        private void ClothesStore_ClothesLoaded()
        {
            _clothesListingItemCollection.Clear();
            _detailedClothesListingItemCollection.Clear();

            foreach (ClothesModel clothes in _clothesStore.Clothes)
            {
                ClothesStore_ClothesAdded(clothes);
                ClothesStore_DetailedClothesItemAdded(clothes);
            }
        }

        private void ClothesStore_ClothesAdded(ClothesModel clothes)
        {
            _clothesListingItemCollection.Add(new ClothesListingItemViewModel(
                clothes, _modalNavigationStore, _categoryStore, _seasonStore, _clothesStore));
        }
         
        private void ClothesStore_DetailedClothesItemAdded(ClothesModel clothes)
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
        }
         
        private void ClothesStore_ClothesUpdated(ClothesModel clothes)
        {
            ClothesListingItemViewModel? ItemToUpdate = _clothesListingItemCollection
                .FirstOrDefault(y => y.Clothes.GuidID == clothes.GuidID);

            ItemToUpdate.Update(clothes);
        }
        
        private void ClothesStore_DetailedClothesItemUpdated(ClothesModel clothes)
        {
            List<DetailedClothesListingItemModel> itemsToUpdate = _detailedClothesListingItemCollection
                .Where(y => y.Clothes.GuidID == clothes.GuidID).ToList();

            if (itemsToUpdate.Count != clothes.Sizes.Count)
            {
                //TODO: UpdateDetailedClothes verbessern
                DetailedClothesItemDeleted(itemsToUpdate);
                ClothesStore_DetailedClothesItemAdded(clothes);
            }
            else
            {
                foreach (DetailedClothesListingItemModel detailedClothesItem in itemsToUpdate)
                {
                    detailedClothesItem.Update(clothes);
                }
            }
        }

        private void ClothesStore_ClothesDeleted(Guid guidID)
        {
            ClothesListingItemViewModel? ItemToDelete = _clothesListingItemCollection
                .FirstOrDefault(y => y.Clothes.GuidID == guidID);
            _clothesListingItemCollection.Remove(ItemToDelete);

            List<DetailedClothesListingItemModel> itemsToDelete = _detailedClothesListingItemCollection
                .Where(y => y.Clothes.GuidID == guidID).ToList();
            DetailedClothesItemDeleted(itemsToDelete);
        }
        
        private void DetailedClothesItemDeleted(List<DetailedClothesListingItemModel> itemsToDelete)
        {
            foreach (DetailedClothesListingItemModel item in itemsToDelete)
            {
                _detailedClothesListingItemCollection.Remove(item);
            }
        }
        

        private void EmployeeStore_EmployeesLoaded()
        {
            _employeeListingItemCollection.Clear();
            _detailedEmployeeListingItemCollection.Clear();

            foreach (EmployeeModel employee in _employeeStore.Employees)
            {
                EmployeeStore_EmployeeAdded(employee);
            }
        }

        private void EmployeeStore_EmployeeAdded(EmployeeModel employee)
        {
            _employeeListingItemCollection.Add(new(employee, this, _modalNavigationStore, _employeeStore));
            _newEmployeeListingItemCollection.Clear();
        }
        
        private void EmployeeStore_DetailedEmployeeItemAdded(EmployeeModel employee)
        {
            if (employee.Clothes.Count == 0)
            {
                _detailedEmployeeListingItemCollection.Add(new(employee, null, null));
            }
            else
            {
                foreach (ClothesModel clothes in employee.Clothes)
                {
                    foreach (ClothesSizeModel size in clothes.Sizes)
                    {
                        _detailedEmployeeListingItemCollection.Add(new(employee, clothes.GuidID, size.Size));
                    }
                }
            }
        }

        private void EmployeeStore_EmployeeUpdated(EmployeeModel employee)
        {
            EmployeeListingItemViewModel? ItemToUpdate = _employeeListingItemCollection
                .FirstOrDefault(y => y.Employee.GuidID == employee.GuidID);

            ItemToUpdate.Update(employee);
            _newEmployeeListingItemCollection.Clear();
        }
        
        private void EmployeeStore_DetailedEmployeeItemUpdated(EmployeeModel employee)
        {
            List<DetailedEmployeeListingItemModel> itemsToUpdate = _detailedEmployeeListingItemCollection
                .Where(y => y.Employee.GuidID == employee.GuidID).ToList();

            if (itemsToUpdate.Count != employee.Clothes.Count)
            {
                //TODO: UpdateDetailedEmployee verbessern
                DetailedEmployeeItemDeleted(itemsToUpdate);
                EmployeeStore_DetailedEmployeeItemAdded(employee);
            }
            else
            {
                foreach (DetailedEmployeeListingItemModel detailedEmployeeItem in itemsToUpdate)
                {
                    detailedEmployeeItem.Update(employee);
                }
            }

            _newEmployeeListingItemCollection.Clear();
        }

        private void EmployeeStore_EmployeeDeleted(Guid guidID)
        {
            EmployeeListingItemViewModel? ItemToDelete = _employeeListingItemCollection
                .FirstOrDefault(y => y.Employee.GuidID == guidID);
            _employeeListingItemCollection.Remove(ItemToDelete);

            List<DetailedEmployeeListingItemModel> itemsToDelete = _detailedEmployeeListingItemCollection
                .Where(y => y.Employee.GuidID == guidID).ToList();
            DetailedEmployeeItemDeleted(itemsToDelete);
        }
        
        private void DetailedEmployeeItemDeleted(List<DetailedEmployeeListingItemModel> itemsToDelete)
        {
            foreach (DetailedEmployeeListingItemModel item in itemsToDelete)
            {
                _detailedEmployeeListingItemCollection.Remove(item);
            }
        }


        protected override void Dispose()
        {
            _clothesStore.ClothesLoaded -= ClothesStore_ClothesLoaded;
            _clothesStore.ClothesAdded -= ClothesStore_ClothesAdded;
            _clothesStore.DetailedClothesItemAdded -= ClothesStore_DetailedClothesItemAdded;
            _clothesStore.ClothesUpdated -= ClothesStore_ClothesUpdated;
            _clothesStore.DetailedClothesItemUpdated -= ClothesStore_DetailedClothesItemUpdated;
            _clothesStore.ClothesDeleted -= ClothesStore_ClothesDeleted;

            _employeeStore.EmployeesLoaded -= EmployeeStore_EmployeesLoaded;
            _employeeStore.EmployeeAdded -= EmployeeStore_EmployeeAdded;
            _employeeStore.DetailedEmployeeItemAdded -= EmployeeStore_DetailedEmployeeItemAdded;
            _employeeStore.EmployeeUpdated -= EmployeeStore_EmployeeUpdated;
            _employeeStore.DetailedEmployeeItemUpdated -= EmployeeStore_DetailedEmployeeItemUpdated;
            _employeeStore.EmployeeDeleted += EmployeeStore_EmployeeDeleted;

            base.Dispose();
        }
    }
}
