using DVS.Domain.Models;
using DVS.WPF.Commands;
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

        public DetailedClothesListingItemViewModel SelectedDetailedClothesItem
        {
            set
            {
                _selectedDetailedClothesItemStore.SelectedDetailedClothesItem = value;
                _selectedDetailedEmployeeClothesItemStore.SelectedDetailedEmployeeItem = null;
            }
        }
        
        public DetailedEmployeeListingItemViewModel SelectedDetailedEmployeeClothesItem
        {
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
        private readonly ClothesSizeStore _clothesSizeStore;
        private readonly EmployeeClothesSizesStore _employeeClothesSizesStore;
        private readonly SelectedDetailedClothesItemStore _selectedDetailedClothesItemStore;
        private readonly SelectedDetailedEmployeeClothesItemStore _selectedDetailedEmployeeClothesItemStore;
        private readonly AddEditEmployeeListingViewModel _addEditEmployeeListingViewModel;

        public ICommand LoadDataFromDbCommand { get; }

        public DVSListingViewModel(SizeStore sizeStore,
                                   ClothesStore clothesStore,
                                   EmployeeStore employeeStore,
                                   ModalNavigationStore modalNavigationStore,
                                   CategoryStore categoryStore,
                                   SeasonStore seasonStore,
                                   ClothesSizeStore clothesSizeStore,
                                   EmployeeClothesSizesStore employeeClothesSizesStore,
                                   SelectedDetailedClothesItemStore selectedDetailedClothesItemStore,
                                   SelectedDetailedEmployeeClothesItemStore selectedDetailedEmployeeClothesItemStore,
                                   AddEditEmployeeListingViewModel addEditEmployeeListingViewModel)
        {
            _sizeStore = sizeStore;
            _clothesStore = clothesStore;
            _employeeStore = employeeStore;
            _modalNavigationStore = modalNavigationStore;
            _categoryStore = categoryStore;
            _seasonStore = seasonStore;
            _clothesSizeStore = clothesSizeStore;
            _employeeClothesSizesStore = employeeClothesSizesStore;
            _selectedDetailedClothesItemStore = selectedDetailedClothesItemStore;
            _selectedDetailedEmployeeClothesItemStore = selectedDetailedEmployeeClothesItemStore;
            _addEditEmployeeListingViewModel = addEditEmployeeListingViewModel;

            LoadDataFromDbCommand = new LoadDataFromDbCommand(sizeStore,
                                                              categoryStore,
                                                              seasonStore,
                                                              clothesStore,
                                                              clothesSizeStore,
                                                              employeeStore,
                                                              employeeClothesSizesStore);

            _clothesStore.ClothesLoaded += ClothesStore_ClothesLoaded;
            _clothesStore.ClothesAdded += ClothesStore_ClothesAdded;
            _clothesStore.ClothesUpdated += ClothesStore_ClothesUpdated;
            _clothesStore.ClothesDeleted += ClothesStore_ClothesDeleted;

            _employeeStore.EmployeesLoaded += EmployeeStore_EmployeesLoaded;
            _employeeStore.EmployeeAdded += EmployeeStore_EmployeeAdded;
            _employeeStore.EmployeeUpdated += EmployeeStore_EmployeeUpdated;
            _employeeStore.EmployeeDeleted += EmployeeStore_EmployeeDeleted;
        }

        public static DVSListingViewModel LoadViewModel(SizeStore sizeStore,
                                                        ClothesStore clothesStore,
                                                        EmployeeStore employeeStore,
                                                        ModalNavigationStore modalNavigationStore,
                                                        CategoryStore categoryStore,
                                                        SeasonStore seasonStore,
                                                        ClothesSizeStore clothesSizeStore,
                                                        EmployeeClothesSizesStore employeeClothesSizesStore,
                                                        SelectedDetailedClothesItemStore selectedDetailedClothesItemStore,
                                                        SelectedDetailedEmployeeClothesItemStore selectedDetailedEmployeeClothesItemStore,
                                                        AddEditEmployeeListingViewModel addEditEmployeeListingViewModel)
        {
            DVSListingViewModel viewModel = new (sizeStore,
                                                 clothesStore,
                                                 employeeStore,
                                                 modalNavigationStore,
                                                 categoryStore,
                                                 seasonStore,
                                                 clothesSizeStore,
                                                 employeeClothesSizesStore,
                                                 selectedDetailedClothesItemStore,
                                                 selectedDetailedEmployeeClothesItemStore,
                                                 addEditEmployeeListingViewModel);

            viewModel.LoadDataFromDbCommand.Execute(null);

            return viewModel;
        }


        private void ClothesStore_ClothesLoaded()
        {
            _clothesListingItemCollection.Clear();
            _detailedClothesListingItemCollection.Clear();

            foreach (Clothes clothes in _clothesStore.Clothes)
            {
                ClothesStore_ClothesAdded(clothes);
            }
        }

        private void ClothesStore_ClothesAdded(Clothes clothes)
        {
            // Add ClothesListingItemViewModel
            _clothesListingItemCollection.Add(new ClothesListingItemViewModel(clothes,
                                                                              _modalNavigationStore,
                                                                              _sizeStore,
                                                                              _categoryStore,
                                                                              _seasonStore,
                                                                              _clothesStore,
                                                                              _clothesSizeStore,
                                                                              _employeeClothesSizesStore,
                                                                              _employeeStore,
                                                                              this));

            // Add DetailedClothesListingItemViewModel
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
         
        private void ClothesStore_ClothesUpdated(Clothes updatedClothes)
        {
            // ClothesItem:
            // Finden des zu bearbeitenden ClothesItem mit der passenden ClothesGuidID
            ClothesListingItemViewModel? ItemToUpdate = _clothesListingItemCollection
                .FirstOrDefault(y => y.Clothes.GuidID == updatedClothes.GuidID);

            ItemToUpdate?.Update(updatedClothes);


            // DetailedClothesItem:
            // Speichern aller Größen der zu bearbeitenden Bekleidung 
            var currentSizes = updatedClothes.Sizes.Select(s => s.Size).ToHashSet();

            // Finden aller DetailedClothesItems mit der passenden ClothesID
            var detailedItems = _detailedClothesListingItemCollection
                .Where(y => y.Clothes.GuidID == updatedClothes.GuidID)
                .ToList();

            if (currentSizes.Count == 0)
            {
                foreach (DetailedClothesListingItemViewModel item in detailedItems)
                {
                    _detailedClothesListingItemCollection.Remove(item);
                }
                _detailedClothesListingItemCollection.Add(new DetailedClothesListingItemViewModel(updatedClothes, null));
            }
            else
            {
                // Entfernen sämtlicher DetailedClothesItems, deren Größe, die zu bearbeitende Bekleidung nicht mehr beinhaltet
                foreach (DetailedClothesListingItemViewModel item in detailedItems)
                {
                    var matchingClothesSize = currentSizes.FirstOrDefault(y => y.GuidID == item.ClothesSizeGuidID);

                    if (matchingClothesSize == null)
                    {
                        _detailedClothesListingItemCollection.Remove(item);
                    }
                }

                // Prüfen ob die zu bearbeitende Bekleidung neue größen hinzubekommen hat? DetailedEmployeeClothesItems hinzufügen
                // ?? Aktualisieren der DetailedEmployeeClothesItems
                foreach (ClothesSize size in updatedClothes.Sizes)
                {
                    DetailedClothesListingItemViewModel? DetailedItemToUpdate = _detailedClothesListingItemCollection
                        .FirstOrDefault(y => y.Clothes.GuidID == updatedClothes.GuidID && y.Size == size.Size.Size);

                    if (DetailedItemToUpdate != null)
                    {
                        DetailedItemToUpdate.Update(updatedClothes, size);
                    }
                    else
                    {
                        _detailedClothesListingItemCollection.Add(new DetailedClothesListingItemViewModel(updatedClothes, size));
                    }
                }
            }
        }
        
        private void ClothesStore_ClothesDeleted(Guid ClothesGuidID)
        {
            ClothesListingItemViewModel? ItemToDelete = _clothesListingItemCollection
                .FirstOrDefault(y => y.Clothes.GuidID == ClothesGuidID);

            _clothesListingItemCollection.Remove(ItemToDelete);

            List<DetailedClothesListingItemViewModel> ItemsToDelete = _detailedClothesListingItemCollection
                .Where(y => y.Clothes.GuidID == ClothesGuidID)
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

        private void EmployeeStore_EmployeeAdded(Employee newEmployee)
        {
            _employeeListingItemCollection.Add(new EmployeeListingItemViewModel(newEmployee,
                                                                                this,
                                                                                _modalNavigationStore,
                                                                                _addEditEmployeeListingViewModel,
                                                                                _employeeStore,
                                                                                _clothesStore,
                                                                                _sizeStore,
                                                                                _categoryStore,
                                                                                _seasonStore,
                                                                                _clothesSizeStore,
                                                                                _employeeClothesSizesStore));

            if (newEmployee.Clothes.Count == 0)
            {
                _detailedEmployeeListingItemCollection.Add(new DetailedEmployeeListingItemViewModel(newEmployee, null));
            }
            else
            {
                foreach (EmployeeClothesSize clothes in newEmployee.Clothes)
                {
                    _detailedEmployeeListingItemCollection.Add(new DetailedEmployeeListingItemViewModel(newEmployee, clothes));
                }
            }
        }
        
        private void EmployeeStore_EmployeeUpdated(Employee updatedEmployee)
        {
            // Finden des zu bearbeitenden EmployeeItem mit der passenden EmployeeGuidID
            EmployeeListingItemViewModel? ItemToUpdate = _employeeListingItemCollection
                .FirstOrDefault(y => y.Employee.GuidID == updatedEmployee.GuidID);

            ItemToUpdate?.Update(updatedEmployee);

            // Entfernen sämtlicher Bekleidung des zu bearbeitenden Mitarbeiters
            if (updatedEmployee.Clothes.Count == 0)
            {
                // Finden aller DetailedEmployeeClothesItems mit der passenden EmployeeGuidID
                List<DetailedEmployeeListingItemViewModel> itemsToDelete = _detailedEmployeeListingItemCollection
                .Where(y => y.Employee.GuidID == updatedEmployee.GuidID)
                .ToList();

                foreach (DetailedEmployeeListingItemViewModel item in itemsToDelete)
                {
                    _detailedEmployeeListingItemCollection.Remove(item);
                }

                _detailedEmployeeListingItemCollection.Add(new DetailedEmployeeListingItemViewModel(updatedEmployee, null));
            }
            else
            {
                // Entfernen von DetailedEmployeeClothesItems, deren Bekleidungsgrößen nicht mehr im aktuellen EmployeeModel vorhanden sind
                foreach (EmployeeClothesSize clothes in updatedEmployee.Clothes)
                {
                    // Erstellen einer Liste der aktuellen Bekleidungsgrößen des Mitarbeiter
                    var currentClothesSizes = updatedEmployee.Clothes
                        .Select(ecs => new { ecs.ClothesSize.Clothes.GuidID, ecs.ClothesSize.Size.Size })
                        .ToHashSet();

                    // Finden der DetailedEmployeeClothesItems mit der passenden EmployeeGuidID
                    var detailedItems = _detailedEmployeeListingItemCollection
                        .Where(y => y.Employee.GuidID == updatedEmployee.GuidID)
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
                foreach (EmployeeClothesSize clothes in updatedEmployee.Clothes)
                {
                    var itemToUpdate = _detailedEmployeeListingItemCollection
                            .FirstOrDefault(y => y.Employee.GuidID == updatedEmployee.GuidID
                                            && y.ClothesGuidID == clothes.ClothesSize.ClothesGuidID
                                            && y.Size == clothes.ClothesSize.Size.Size);

                    if (itemToUpdate != null)
                    {
                        itemToUpdate.Update(updatedEmployee, clothes);
                    }
                    else
                    {
                        _detailedEmployeeListingItemCollection.Add(new DetailedEmployeeListingItemViewModel(updatedEmployee, clothes));
                    }
                }
            }
        }

        private void EmployeeStore_EmployeeDeleted(Guid EmployeeGuidID)
        {
            EmployeeListingItemViewModel? ItemToDelete = _employeeListingItemCollection
                .FirstOrDefault(y => y.Employee.GuidID == EmployeeGuidID);

            _employeeListingItemCollection.Remove(ItemToDelete);

            List<DetailedEmployeeListingItemViewModel> itemsToDelete = _detailedEmployeeListingItemCollection
                .Where(y => y.Employee.GuidID == EmployeeGuidID)
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
