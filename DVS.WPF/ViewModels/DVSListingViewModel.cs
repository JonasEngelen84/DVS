using DVS.WPF.Commands;
using DVS.Domain.Models;
using DVS.WPF.Stores;
using DVS.WPF.ViewModels.ListViewItems;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace DVS.WPF.ViewModels
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
                    .FirstOrDefault(y => y.Clothes.GuidID == _selectedDetailedClothesItemStore.SelectedDetailedClothesItem.Clothes.GuidID);

            set
            {
                _selectedDetailedClothesItemStore.SelectedDetailedClothesItem = value;
                _selectedDetailedEmployeeClothesItemStore.SelectedDetailedEmployeeItem = null;
            }
        }
        
        public DetailedEmployeeListingItemViewModel SelectedDetailedEmployeeClothesItem
        {//TODO: ist beim löschen aller mitarbeiter bekleidungen auf null und wirft ex
            get => _detailedEmployeeListingItemCollection
                    .FirstOrDefault(y => y.Employee.GuidID == _selectedDetailedEmployeeClothesItemStore.SelectedDetailedEmployeeItem.Employee.GuidID);

            set
            {
                _selectedDetailedEmployeeClothesItemStore.SelectedDetailedEmployeeItem = value;
                _selectedDetailedClothesItemStore.SelectedDetailedClothesItem = null;
            }
        }

        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly SizeStore _sizeStore;
        private readonly ClothesStore _clothesStore;
        private readonly EmployeeStore _employeeStore;
        private readonly CategoryStore _categoryStore;
        private readonly SeasonStore _seasonStore;
        private readonly SelectedDetailedClothesItemStore _selectedDetailedClothesItemStore;
        private readonly SelectedDetailedEmployeeClothesItemStore _selectedDetailedEmployeeClothesItemStore;

        public ICommand ClothesItemReceivedCommand { get; }
        public ICommand ClothesItemRemovedCommand { get; }


        public DVSListingViewModel(SizeStore sizeStore,
                                   ClothesStore clothesStore,
                                   EmployeeStore employeeStore,
                                   ModalNavigationStore modalNavigationStore,
                                   CategoryStore categoryStore,
                                   SeasonStore seasonStore,
                                   SelectedDetailedClothesItemStore selectedDetailedClothesItemStore,
                                   SelectedDetailedEmployeeClothesItemStore selectedDetailedEmployeeClothesItemStore)
        {
            _sizeStore = sizeStore;
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


        public void LoadNewEmployeeListingItemCollection(Employee employee)
        {
            foreach (EmployeeClothesSize clothes in employee.EmployeeClothes)
            {
                _newEmployeeListingItemCollection.Add(
                    new DetailedClothesListingItemViewModel(clothes.ClothesSize.Clothes, clothes.ClothesSize));
            }
        }

        public void AddClothesItemToNewEmployeeListingItemCollection()
        {
            if (IncomingClothesListingItemModel == null || IncomingClothesListingItemModel.Quantity == 0)
            {
                return;
            }

            DetailedClothesListingItemViewModel? existingItem = _newEmployeeListingItemCollection
                .FirstOrDefault(y => y.Clothes.GuidID == IncomingClothesListingItemModel.Clothes.GuidID
                && y.Size == IncomingClothesListingItemModel.Size);

            Clothes clothes = new(IncomingClothesListingItemModel.Clothes.GuidID,
                                  IncomingClothesListingItemModel.Clothes.ID,
                                  IncomingClothesListingItemModel.Clothes.Name,
                                  IncomingClothesListingItemModel.Clothes.Category,
                                  IncomingClothesListingItemModel.Clothes.Season,
                                  null);

            if (existingItem == null)
            {
                clothes.Sizes.Add(new(Guid.NewGuid(), clothes, IncomingClothesListingItemModel.Clothes.Sizes
                    .FirstOrDefault(y => y.GuidID == IncomingClothesListingItemModel.ClothesSizeGuidID).Size, 1));

                DetailedClothesListingItemViewModel newItem = new(clothes, IncomingClothesListingItemModel.ClothesSize);
                _newEmployeeListingItemCollection.Add(newItem);
            }
            else
            {
                clothes.Sizes = existingItem.Clothes.Sizes;
                var size = existingItem.Clothes.Sizes.FirstOrDefault(y => y.Size.Size == IncomingClothesListingItemModel.Size);
                size.Quantity++;
                existingItem.Update(clothes, IncomingClothesListingItemModel.ClothesSize);
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

                Clothes clothes = new(IncomingClothesListingItemModel.Clothes.GuidID,
                                      IncomingClothesListingItemModel.Clothes.ID,
                                      IncomingClothesListingItemModel.Clothes.Name,
                                      IncomingClothesListingItemModel.Clothes.Category,
                                      IncomingClothesListingItemModel.Clothes.Season,
                                      null)
                {
                    Sizes = existingItem.Clothes.Sizes
                };

                var size = existingItem.Clothes.Sizes.FirstOrDefault(modelItem => modelItem.Size.Size == IncomingClothesListingItemModel.Size);
                size.Quantity--;
                existingItem.Update(clothes, IncomingClothesListingItemModel.ClothesSize);
            }
        }


        private void ClothesStore_ClothesLoaded()
        {
            _clothesListingItemCollection.Clear();
            _detailedClothesListingItemCollection.Clear();
            //TODO: _newEmployeeListingItemCollection über anderen weg bei abbruch Add/Edit Employee löschen
            _newEmployeeListingItemCollection.Clear();

            foreach (Clothes clothes in _clothesStore.Clothes)
            {
                ClothesStore_ClothesAdded(clothes);
            }
        }

        private void ClothesStore_ClothesAdded(Clothes clothes)
        {
            _clothesListingItemCollection.Add(new ClothesListingItemViewModel(
                clothes, _modalNavigationStore, _sizeStore, _categoryStore, _seasonStore, _clothesStore));

            if (clothes.Sizes.Count == 0)
            {
                _detailedClothesListingItemCollection.Add(new DetailedClothesListingItemViewModel(clothes, null));
            }
            else
            {
                foreach (ClothesSize size in clothes.Sizes)
                {
                    _detailedClothesListingItemCollection.Add(new DetailedClothesListingItemViewModel(clothes, size));
                }
            }
        }
         
        private void ClothesStore_ClothesUpdated(Clothes clothes)
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
                    _detailedClothesListingItemCollection.Remove(item);
                }

                _detailedClothesListingItemCollection.Add(new DetailedClothesListingItemViewModel(clothes, null));
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
                foreach (DetailedClothesListingItemViewModel item in detailedItems)
                {
                    if (!currentSizes.Contains(item.Clothes.Sizes.FirstOrDefault(y => y.GuidID == item.ClothesSizeGuidID).Size))
                    {
                        _detailedClothesListingItemCollection.Remove(item);
                    }
                }

                // Prüfen ob die zu bearbeitende Bekleidung neue größen hinzubekommen hat? DetailedEmployeeClothesItems hinzufügen
                // ?? Aktualisieren der DetailedEmployeeClothesItems
                foreach (var size in clothes.Sizes)
                {
                    DetailedClothesListingItemViewModel? DetailedItemToUpdate = _detailedClothesListingItemCollection
                        .FirstOrDefault(y => y.Clothes.GuidID == clothes.GuidID && y.Size == size.Size.Size);

                    if (DetailedItemToUpdate != null)
                    {
                        DetailedItemToUpdate.Update(clothes, size);
                    }
                    else
                    {
                        _detailedClothesListingItemCollection.Add(new DetailedClothesListingItemViewModel(clothes, size));
                    }
                }
            }
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
                _detailedClothesListingItemCollection.Remove(item);
            }
        }
        

        private void EmployeeStore_EmployeesLoaded()
        {
            _employeeListingItemCollection.Clear();
            _detailedEmployeeListingItemCollection.Clear();

            foreach (Employee employee in _employeeStore.Employees)
            {
                EmployeeStore_EmployeeAdded(employee);
            }
        }

        private void EmployeeStore_EmployeeAdded(Employee employee)
        {
            _employeeListingItemCollection.Add(new EmployeeListingItemViewModel(
                employee, this, _modalNavigationStore, _employeeStore, _clothesStore));

            _newEmployeeListingItemCollection.Clear();

            if (employee.EmployeeClothes.Count == 0)
            {
                _detailedEmployeeListingItemCollection.Add(new DetailedEmployeeListingItemViewModel(employee, null));
            }
            else
            {
                foreach (EmployeeClothesSize clothes in employee.EmployeeClothes)
                {
                    _detailedEmployeeListingItemCollection.Add(new DetailedEmployeeListingItemViewModel(employee, clothes));
                }
            }
        }
        
        private void EmployeeStore_EmployeeUpdated(Employee employee)
        {
            // Finden des zu bearbeitenden EmployeeItem mit der passenden EmployeeGuidID
            EmployeeListingItemViewModel? ItemToUpdate = _employeeListingItemCollection
                .FirstOrDefault(y => y.Employee.GuidID == employee.GuidID);

            ItemToUpdate?.Update(employee);
            _newEmployeeListingItemCollection.Clear();

            // Entfernen sämtlicher Bekleidung des zu bearbeitenden Mitarbeiters
            if (employee.EmployeeClothes.Count == 0)
            {
                // Finden aller DetailedEmployeeClothesItems mit der passenden EmployeeGuidID
                List<DetailedEmployeeListingItemViewModel> itemsToDelete = _detailedEmployeeListingItemCollection
                .Where(y => y.Employee.GuidID == employee.GuidID)
                .ToList();

                foreach (DetailedEmployeeListingItemViewModel item in itemsToDelete)
                {
                    _detailedEmployeeListingItemCollection.Remove(item);
                }

                _detailedEmployeeListingItemCollection.Add(new DetailedEmployeeListingItemViewModel(employee, null));
            }
            else
            {
                // Entfernen von DetailedEmployeeClothesItems, deren Bekleidungsgrößen nicht mehr im aktuellen EmployeeModel vorhanden sind
                foreach (EmployeeClothesSize clothes in employee.EmployeeClothes)
                {
                    // Erstellen einer Liste der aktuellen Bekleidungsgrößen des Mitarbeiter
                    var currentClothesSizes = employee.EmployeeClothes
                        .Select(ecs => new { ecs.ClothesSize.Clothes.GuidID, ecs.ClothesSize.Size.Size })
                        .ToHashSet();

                    // Finden der DetailedEmployeeClothesItems mit der passenden EmployeeGuidID
                    var detailedItems = _detailedEmployeeListingItemCollection
                        .Where(y => y.Employee.GuidID == employee.GuidID)
                        .ToList();

                    foreach (var item in detailedItems)
                    {
                        if (!currentClothesSizes.Any(cs => cs.GuidID == item.ClothesGuidID && cs.Size == item.Size))
                        {
                            _detailedEmployeeListingItemCollection.Remove(item);
                        }
                    }
                }

                // Prüfen ob der zu bearbeitende Mitarbeiter neue Bekleidung hinzubekommen hat? DetailedEmployeeClothesItems hinzufügen
                // ?? Aktualisieren der DetailedEmployeeClothesItems
                foreach (EmployeeClothesSize clothes in employee.EmployeeClothes)
                {
                    var itemToUpdate = _detailedEmployeeListingItemCollection
                            .FirstOrDefault(y => y.Employee.GuidID == employee.GuidID
                                                 && y.ClothesGuidID == clothes.ClothesSize.ClothesGuidID
                                                 && y.Size == clothes.ClothesSize.Size.Size);

                    if (itemToUpdate != null)
                    {
                        itemToUpdate.Update(employee, clothes);
                    }
                    else
                    {
                        _detailedEmployeeListingItemCollection.Add(new DetailedEmployeeListingItemViewModel(employee, clothes));
                    }
                }
            }
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
                _detailedEmployeeListingItemCollection.Remove(item);
            }
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
