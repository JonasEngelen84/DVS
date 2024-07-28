using DVS.Commands;
using DVS.Domain.Models;
using DVS.Stores;
using DVS.ViewModels.ListViewItems;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace DVS.ViewModels
{
    public class DVSListingViewModel : ViewModelBase
    {
        private readonly ObservableCollection<ClothesListingItemViewModel> _clothesListingItemCollection = [];
        public IEnumerable<ClothesListingItemViewModel> ClothesListingItemCollection => _clothesListingItemCollection;

        private readonly ObservableCollection<DetailedClothesListingItemViewModel> _detailedClothesListingItemCollection = [];
        public IEnumerable<DetailedClothesListingItemViewModel> DetailedClothesListingItemCollection => _detailedClothesListingItemCollection;

        private readonly ObservableCollection<EmployeeListingItemViewModel> _employeeListingItemCollection = [];
        public IEnumerable<EmployeeListingItemViewModel> EmployeeListingItemCollection => _employeeListingItemCollection;

        private readonly ObservableCollection<DetailedEmployeeListingItemViewModel> _detailedEmployeeListingItemCollection = [];
        public IEnumerable<DetailedEmployeeListingItemViewModel> DetailedEmployeeListingItemCollection => _detailedEmployeeListingItemCollection;
        
        private readonly ObservableCollection<DetailedClothesListingItemViewModel> _newEmployeeListingItemCollection = [];
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
                if(_incomingClothesListingItemModel != value)
                {
                    _incomingClothesListingItemModel = value;
                    OnPropertyChanged(nameof(IncomingClothesListingItemModel));
                }
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
                if (_removedClothesListingItemModel != value)
                {
                    _removedClothesListingItemModel = value;
                    OnPropertyChanged(nameof(RemovedClothesListingItemModel));
                }
            }
        }

        public DetailedClothesListingItemViewModel SelectedDetailedClothesItem
        {
            get => _detailedClothesListingItemCollection
                    .FirstOrDefault(y => y.Clothes.ID == _selectedDetailedClothesItemStore.SelectedDetailedClothesItem.ID);

            set
            {
                _selectedDetailedClothesItemStore.SelectedDetailedClothesItem = value;
                _selectedDetailedEmployeeClothesItemStore.SelectedDetailedEmployeeItem = null;
            }
        }
        
        public DetailedEmployeeListingItemViewModel SelectedDetailedEmployeeClothesItem
        {//TODO: ist beim löschen aller mitarbeiter bekleidungen auf null und wirft ex
            get => _detailedEmployeeListingItemCollection
                    .FirstOrDefault(y => y.Employee.ID == _selectedDetailedEmployeeClothesItemStore.SelectedDetailedEmployeeItem.ID);

            set
            {
                _selectedDetailedEmployeeClothesItemStore.SelectedDetailedEmployeeItem = value;
                _selectedDetailedClothesItemStore.SelectedDetailedClothesItem = null;
            }
        }

        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly ClothesStore _clothesStore;
        private readonly EmployeeStore _employeeStore;
        private readonly CategoryStore _categoryStore;
        private readonly SeasonStore _seasonStore;
        private readonly SelectedDetailedClothesItemStore _selectedDetailedClothesItemStore;
        private readonly SelectedDetailedEmployeeClothesItemStore _selectedDetailedEmployeeClothesItemStore;

        public ICommand ClothesItemReceivedCommand { get; }
        public ICommand ClothesItemRemovedCommand { get; }


        public DVSListingViewModel(ClothesStore clothesStore, EmployeeStore employeeStore,
                                   ModalNavigationStore modalNavigationStore, CategoryStore categoryStore, SeasonStore seasonStore,
                                   SelectedDetailedClothesItemStore selectedDetailedClothesItemStore,
                                   SelectedDetailedEmployeeClothesItemStore selectedDetailedEmployeeClothesItemStore)
        {
            _clothesStore = clothesStore;
            _employeeStore = employeeStore;
            _modalNavigationStore = modalNavigationStore;
            _categoryStore = categoryStore;
            _seasonStore = seasonStore;
            _selectedDetailedClothesItemStore = selectedDetailedClothesItemStore;
            _selectedDetailedEmployeeClothesItemStore = selectedDetailedEmployeeClothesItemStore;
            ClothesItemReceivedCommand = new ClothesItemReceivedCommand(this, _clothesStore);
            ClothesItemRemovedCommand = new ClothesItemRemovedCommand(this, _clothesStore);

            ClothesStore_ClothesLoaded();
            EmployeeStore_EmployeesLoaded();

            _clothesStore.ClothesLoaded += ClothesStore_ClothesLoaded;
            _clothesStore.ClothesAdded += ClothesStore_ClothesAdded;
            _clothesStore.ClothesUpdated += ClothesStore_ClothesUpdated;
            _clothesStore.ClothesDeleted += ClothesStore_ClothesDeleted;

            _employeeStore.EmployeesLoaded += EmployeeStore_EmployeesLoaded;
            _employeeStore.EmployeeAdded += EmployeeStore_EmployeeAdded;
            _employeeStore.EmployeeUpdated += EmployeeStore_EmployeeUpdated;
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

            DetailedClothesListingItemViewModel? existingItem = _newEmployeeListingItemCollection
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
                DetailedClothesListingItemViewModel newItem = new(clothes, IncomingClothesListingItemModel.Size);
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
                DetailedClothesListingItemViewModel? existingItem = _newEmployeeListingItemCollection
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
            //TODO: _newEmployeeListingItemCollection über anderen weg bei abbruch Add/Edit Employee löschen
            _newEmployeeListingItemCollection.Clear();

            foreach (ClothesModel clothes in _clothesStore.Clothes)
            {
                ClothesStore_ClothesAdded(clothes);
            }
        }

        private void ClothesStore_ClothesAdded(ClothesModel clothes)
        {
            _clothesListingItemCollection.Add(new ClothesListingItemViewModel(
                clothes, _modalNavigationStore, _categoryStore, _seasonStore, _clothesStore));

            if (clothes.Sizes.Count == 0)
            {
                DetailedClothesItemAdded(new DetailedClothesListingItemViewModel(clothes, null));
            }
            else
            {
                foreach (ClothesSizeModel size in clothes.Sizes)
                {
                    DetailedClothesItemAdded(new DetailedClothesListingItemViewModel(clothes, size.Size));
                }
            }
        }
         
        private void DetailedClothesItemAdded(DetailedClothesListingItemViewModel ItemToAdd)
        {
            _detailedClothesListingItemCollection.Add(ItemToAdd);
        }
         
        private void ClothesStore_ClothesUpdated(ClothesModel clothes)
        {
            // Finden des zu bearbeitenden ClothesItem mit der passenden ClothesGuidID
            ClothesListingItemViewModel? ItemToUpdate = _clothesListingItemCollection
                .FirstOrDefault(y => y.Clothes.GuidID == clothes.GuidID);

            ItemToUpdate?.Update(clothes);

            // Entfernen sämtlicher Kleidungsgrößen der zu bearbeitenden Bekleidung
            if (clothes.Sizes.Count == 0)
            {
                // Finden aller DetailedClothesItems mit der passenden ClothesGuidID
                List<DetailedClothesListingItemViewModel> itemsToDelete = _detailedClothesListingItemCollection
                .Where(y => y.Clothes.GuidID == clothes.GuidID)
                .ToList();

                foreach (DetailedClothesListingItemViewModel item in itemsToDelete)
                {
                    DetailedClothesItemDeleted(item);
                }

                DetailedClothesItemAdded(new DetailedClothesListingItemViewModel(clothes, null));
            }
            else
            {
                // Speichern aller Größen der zu bearbeitenden Bekleidung 
                var currentSizes = clothes.Sizes.Select(s => s.Size).ToHashSet();

                // Finden aller DetailedClothesItems mit der passenden ClothesID
                var detailedItems = _detailedClothesListingItemCollection
                    .Where(y => y.Clothes.GuidID == clothes.GuidID)
                    .ToList();

                // Entfernen sämtlicher DetailedClothesItems, deren Größe, die zu bearbeitende Bekleidung nicht mehr beinhaltet
                foreach (var item in detailedItems)
                {
                    if (!currentSizes.Contains(item.Size))
                    {
                        DetailedClothesItemDeleted(item);
                    }
                }

                // Prüfen ob die zu bearbeitende Bekleidung neue größen hinzubekommen hat? DetailedEmployeeClothesItems hinzufügen
                // ?? Aktualisieren der DetailedEmployeeClothesItems
                foreach (var size in clothes.Sizes)
                {
                    DetailedClothesListingItemViewModel? itemToUpdate = _detailedClothesListingItemCollection
                        .FirstOrDefault(y => y.Clothes.GuidID == clothes.GuidID && y.Size == size.Size);

                    if (itemToUpdate != null)
                    {
                        DetailedClothesItemUpdated(clothes, itemToUpdate);
                    }
                    else
                    {
                        DetailedClothesItemAdded(new DetailedClothesListingItemViewModel(clothes, size.Size));
                    }
                }
            }
        }
        
        private void DetailedClothesItemUpdated(ClothesModel clothes, DetailedClothesListingItemViewModel ItemToUpdate)
        {
            ItemToUpdate.Update(clothes);
        }

        private void ClothesStore_ClothesDeleted(Guid guidID)
        {
            ClothesListingItemViewModel? ItemToDelete = _clothesListingItemCollection
                .FirstOrDefault(y => y.Clothes.GuidID == guidID);

            _clothesListingItemCollection.Remove(ItemToDelete);

            List<DetailedClothesListingItemViewModel> ItemsToDelete = _detailedClothesListingItemCollection
                .Where(y => y.Clothes.GuidID == guidID)
                .ToList();

            foreach (DetailedClothesListingItemViewModel item in ItemsToDelete)
            {
                DetailedClothesItemDeleted(item);
            }
        }
        
        private void DetailedClothesItemDeleted(DetailedClothesListingItemViewModel itemToDelete)
        {
            _detailedClothesListingItemCollection.Remove(itemToDelete);
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
            _employeeListingItemCollection.Add(new EmployeeListingItemViewModel(
                employee, this, _modalNavigationStore, _employeeStore, _clothesStore));

            _newEmployeeListingItemCollection.Clear();

            if (employee.Clothes.Count == 0)
            {
                DetailedEmployeeItemAdded(new DetailedEmployeeListingItemViewModel(employee, null, null));
            }
            else
            {
                foreach (ClothesModel clothes in employee.Clothes)
                {
                    foreach (ClothesSizeModel size in clothes.Sizes)
                    {
                        DetailedEmployeeItemAdded(new DetailedEmployeeListingItemViewModel(employee, clothes.GuidID, size.Size));
                    }
                }
            }
        }
        
        private void DetailedEmployeeItemAdded(DetailedEmployeeListingItemViewModel itemToAdd)
        {
            _detailedEmployeeListingItemCollection.Add(itemToAdd);
        }

        private void EmployeeStore_EmployeeUpdated(EmployeeModel employee)
        {
            // Finden des zu bearbeitenden EmployeeItem mit der passenden EmployeeGuidID
            EmployeeListingItemViewModel? ItemToUpdate = _employeeListingItemCollection
                .FirstOrDefault(y => y.Employee.GuidID == employee.GuidID);

            ItemToUpdate?.Update(employee);
            _newEmployeeListingItemCollection.Clear();

            // Entfernen sämtlicher Bekleidung des zu bearbeitenden Mitarbeiters
            if (employee.Clothes.Count == 0)
            {
                // Finden aller DetailedEmployeeClothesItems mit der passenden EmployeeGuidID
                List<DetailedEmployeeListingItemViewModel> itemsToDelete = _detailedEmployeeListingItemCollection
                .Where(y => y.Employee.GuidID == employee.GuidID)
                .ToList();

                foreach (DetailedEmployeeListingItemViewModel item in itemsToDelete)
                {
                    DetailedEmployeeItemDeleted(item);
                }

                DetailedEmployeeItemAdded(new DetailedEmployeeListingItemViewModel(employee, null, null));
            }
            else
            {
                // Entfernen von DetailedEmployeeClothesItems, deren Bekleidungsgrößen nicht mehr im aktuellen EmployeeModel vorhanden sind
                foreach (ClothesModel clothes in employee.Clothes)
                {
                    // Erstellen einer Liste der aktuellen Bekleidungsgrößen des Mitarbeiter
                    var currentClothesSizes = employee.Clothes
                        .SelectMany(c => c.Sizes, (c, s) => new { c.GuidID, s.Size })
                        .ToHashSet();

                    // Finden der DetailedEmployeeClothesItems mit der passenden EmployeeGuidID
                    var detailedItems = _detailedEmployeeListingItemCollection
                        .Where(y => y.Employee.GuidID == employee.GuidID)
                        .ToList();

                    foreach (var item in detailedItems)
                    {
                        if (!currentClothesSizes.Any(cs => cs.GuidID == item.ClothesGuidID && cs.Size == item.Size))
                        {
                            DetailedEmployeeItemDeleted(item);
                        }
                    }
                }

                // Prüfen ob dere zu bearbeitende Mitarbeiter neue Bekleidung hinzubekommen hat? DetailedEmployeeClothesItems hinzufügen
                // ?? Aktualisieren der DetailedEmployeeClothesItems
                foreach (var clothes in employee.Clothes)
                {
                    foreach (var size in clothes.Sizes)
                    {
                        var itemToUpdate = _detailedEmployeeListingItemCollection
                            .FirstOrDefault(y => y.Employee.GuidID == employee.GuidID
                                                 && y.ClothesGuidID == clothes.GuidID
                                                 && y.Size == size.Size);

                        if (itemToUpdate != null)
                        {
                            DetailedEmployeeItemUpdated(employee, itemToUpdate);
                        }
                        else
                        {
                            DetailedEmployeeItemAdded(new DetailedEmployeeListingItemViewModel(employee, clothes.GuidID, size.Size));
                        }
                    }
                }
            }
        }
        
        private void DetailedEmployeeItemUpdated(EmployeeModel employee, DetailedEmployeeListingItemViewModel itemToUpdate)
        {
            itemToUpdate.Update(employee);
        }

        private void EmployeeStore_EmployeeDeleted(Guid guidID)
        {
            EmployeeListingItemViewModel? ItemToDelete = _employeeListingItemCollection
                .FirstOrDefault(y => y.Employee.GuidID == guidID);

            _employeeListingItemCollection.Remove(ItemToDelete);

            List<DetailedEmployeeListingItemViewModel> itemsToDelete = _detailedEmployeeListingItemCollection
                .Where(y => y.Employee.GuidID == guidID)
                .ToList();

            foreach (DetailedEmployeeListingItemViewModel item in itemsToDelete)
            {
                DetailedEmployeeItemDeleted(item);
            }
        }
        
        private void DetailedEmployeeItemDeleted(DetailedEmployeeListingItemViewModel itemToDelete)
        {
            _detailedEmployeeListingItemCollection.Remove(itemToDelete);
        }


        protected override void Dispose()
        {
            _clothesStore.ClothesLoaded -= ClothesStore_ClothesLoaded;
            _clothesStore.ClothesAdded -= ClothesStore_ClothesAdded;
            _clothesStore.ClothesUpdated -= ClothesStore_ClothesUpdated;
            _clothesStore.ClothesDeleted -= ClothesStore_ClothesDeleted;

            _employeeStore.EmployeesLoaded -= EmployeeStore_EmployeesLoaded;
            _employeeStore.EmployeeAdded -= EmployeeStore_EmployeeAdded;
            _employeeStore.EmployeeUpdated -= EmployeeStore_EmployeeUpdated;
            _employeeStore.EmployeeDeleted += EmployeeStore_EmployeeDeleted;

            base.Dispose();
        }
    }
}
