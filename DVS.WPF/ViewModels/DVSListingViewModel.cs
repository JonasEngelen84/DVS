using DVS.Domain.Models;
using DVS.WPF.Commands;
using DVS.WPF.Stores;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace DVS.WPF.ViewModels
{
    public class DVSListingViewModel : ViewModelBase
    {
        private readonly ObservableCollection<ClothesListingItemViewModel> _clothesCollection = [];
        public IEnumerable<ClothesListingItemViewModel> ClothesCollection => _clothesCollection;

        private readonly ObservableCollection<ClothesSize> _clothesSizeCollection = [];
        public IEnumerable<ClothesSize> ClothesSizeCollection => _clothesSizeCollection;

        private readonly ObservableCollection<EmployeeListingItemViewModel> _employeeCollection = [];
        public IEnumerable<EmployeeListingItemViewModel> EmployeeCollection => _employeeCollection;

        private readonly ObservableCollection<EmployeeClothesSize> _employeeClothesSizeCollection = [];
        public IEnumerable<EmployeeClothesSize> EmployeeClothesSizeCollection => _employeeClothesSizeCollection;

        public ClothesSize SelectedClothesSize
        {
            set
            {
                _selectedClothesSizeStore.SelectedClothesSize = value;
                _selectedEmployeeClothesSizeStore.SelectedEmployeeClothesSize = null;
            }
        }
        
        public EmployeeClothesSize SelectedEmployeeClothesSize
        {
            set
            {
                _selectedEmployeeClothesSizeStore.SelectedEmployeeClothesSize = value;
                _selectedClothesSizeStore.SelectedClothesSize = null;
            }
        }

        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly SizeStore _sizeStore;
        private readonly ClothesStore _clothesStore;
        private readonly EmployeeStore _employeeStore;
        private readonly CategoryStore _categoryStore;
        private readonly SeasonStore _seasonStore;
        private readonly ClothesSizeStore _clothesSizeStore;
        private readonly EmployeeClothesSizeStore _employeeClothesSizesStore;
        private readonly SelectedClothesSizeStore _selectedClothesSizeStore;
        private readonly SelectedEmployeeClothesSizeStore _selectedEmployeeClothesSizeStore;
        private readonly AddEditEmployeeListingViewModel _addEditEmployeeListingViewModel;

        public ICommand LoadDataFromDbCommand { get; }

        public DVSListingViewModel(SizeStore sizeStore,
                                   ClothesStore clothesStore,
                                   EmployeeStore employeeStore,
                                   ModalNavigationStore modalNavigationStore,
                                   CategoryStore categoryStore,
                                   SeasonStore seasonStore,
                                   ClothesSizeStore clothesSizeStore,
                                   EmployeeClothesSizeStore employeeClothesSizesStore,
                                   SelectedClothesSizeStore selectedClothesSizeStore,
                                   SelectedEmployeeClothesSizeStore selectedEmployeeClothesSizeStore,
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
            _selectedClothesSizeStore = selectedClothesSizeStore;
            _selectedEmployeeClothesSizeStore = selectedEmployeeClothesSizeStore;
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
                                                        EmployeeClothesSizeStore employeeClothesSizesStore,
                                                        SelectedClothesSizeStore selectedDetailedClothesItemStore,
                                                        SelectedEmployeeClothesSizeStore selectedDetailedEmployeeClothesItemStore,
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
            _clothesCollection.Clear();
            _clothesSizeCollection.Clear();

            foreach (Clothes clothes in _clothesStore.Clothes)
            {
                ClothesStore_ClothesAdded(clothes);
            }
        }

        private void ClothesStore_ClothesAdded(Clothes clothes)
        {
            _clothesCollection.Add(new ClothesListingItemViewModel(
                clothes,
                _modalNavigationStore,
                _sizeStore,
                _categoryStore,
                _seasonStore,
                _clothesStore,
                _clothesSizeStore,
                _employeeClothesSizesStore,
                _employeeStore,
                this));

            if (clothes.Sizes.Count > 0)
            {
                foreach (ClothesSize clothesSize in clothes.Sizes)
                {
                    _clothesSizeCollection.Add(clothesSize);
                }
            }
        }
         
        private void ClothesStore_ClothesUpdated(Clothes updatedClothes)
        {
            // ClothesItem:
            // Finden des zu bearbeitenden ClothesItem mit der passenden ClothesGuidID
            ClothesListingItemViewModel? ItemToUpdate = _clothesCollection
                .FirstOrDefault(y => y.Clothes.Id == updatedClothes.Id);

            ItemToUpdate?.Update(updatedClothes);


            // ClothesSizeItem:
            // Speichern aller Größen der zu bearbeitenden Bekleidung 
            var currentClothesSizes = updatedClothes.Sizes.Select(s => s.Size).ToHashSet();

            // Finden aller ClothesSizes mit der passenden ClothesID
            var existingClothesSizes = _clothesSizeCollection
                .Where(y => y.Clothes.Id == updatedClothes.Id)
                .ToList();

            //if (currentClothesSizes.Count == 0)
            //{
            //    foreach (ClothesSize clothesSize in existingClothesSizes)
            //    {
            //        _clothesSizeCollection.Remove(clothesSize);
            //    }
            //    _clothesSizeCollection.Add(new DetailedClothesListingItemViewModel(updatedClothes, null));
            //}
            //else
            //{
            //    // Entfernen sämtlicher DetailedClothesItems, deren Größe, die zu bearbeitende Bekleidung nicht mehr beinhaltet
            //    foreach (DetailedClothesListingItemViewModel item in existingClothesSizes)
            //    {
            //        var matchingClothesSize = currentClothesSizes.FirstOrDefault(y => y.GuidId == item.ClothesSizeGuidId);

            //        if (matchingClothesSize == null)
            //        {
            //            _clothesSizeCollection.Remove(item);
            //        }
            //    }

            //    // Prüfen ob die zu bearbeitende Bekleidung neue größen hinzubekommen hat? DetailedEmployeeClothesItems hinzufügen
            //    // ?? Aktualisieren der DetailedEmployeeClothesItems
            //    foreach (ClothesSize size in updatedClothes.Sizes)
            //    {
            //        DetailedClothesListingItemViewModel? DetailedItemToUpdate = _clothesSizeCollection
            //            .FirstOrDefault(y => y.Clothes.GuidId == updatedClothes.GuidId && y.Size == size.Size.Size);

            //        if (DetailedItemToUpdate != null)
            //        {
            //            DetailedItemToUpdate.Update(updatedClothes, size);
            //        }
            //        else
            //        {
            //            _clothesSizeCollection.Add(new DetailedClothesListingItemViewModel(updatedClothes, size));
            //        }
            //    }
            //}
        }
        
        private void ClothesStore_ClothesDeleted(string ClothesId)
        {
            ClothesListingItemViewModel? ItemToDelete = _clothesCollection
                .FirstOrDefault(y => y.Clothes.Id == ClothesId);

            _clothesCollection.Remove(ItemToDelete);
        }
        

        private void EmployeeStore_EmployeesLoaded()
        {
            _employeeCollection.Clear();
            _employeeClothesSizeCollection.Clear();

            foreach (Employee employee in _employeeStore.Employees)
            {
                EmployeeStore_EmployeeAdded(employee);
            }
        }

        private void EmployeeStore_EmployeeAdded(Employee newEmployee)
        {
            _employeeCollection.Add(new EmployeeListingItemViewModel(newEmployee,
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

            if (newEmployee.Clothes.Count > 0)
            {
                foreach (EmployeeClothesSize ecs in newEmployee.Clothes)
                {
                    _employeeClothesSizeCollection.Add(ecs);
                }
            }
        }
        
        private void EmployeeStore_EmployeeUpdated(Employee updatedEmployee)
        {
            // Finden des zu bearbeitenden EmployeeItem mit der passenden EmployeeGuidID
            EmployeeListingItemViewModel? ItemToUpdate = _employeeCollection
                .FirstOrDefault(y => y.Employee.Id == updatedEmployee.Id);

            ItemToUpdate?.Update(updatedEmployee);

            // Entfernen sämtlicher Bekleidung des zu bearbeitenden Mitarbeiters
            if (updatedEmployee.Clothes.Count == 0)
            {
                // Finden aller EmployeeClothesSizes mit der passenden EmployeeGuidID
                List<EmployeeClothesSize> itemsToDelete = _employeeClothesSizeCollection
                .Where(y => y.Employee.Id == updatedEmployee.Id)
                .ToList();

                foreach (EmployeeClothesSize item in itemsToDelete)
                {
                    _employeeClothesSizeCollection.Remove(item);
                }

                //_employeeClothesSizeCollection.Add(new DetailedEmployeeListingItemViewModel(updatedEmployee, null));
            }
            else
            {
                // Entfernen von DetailedEmployeeClothesItems, deren Bekleidungsgrößen nicht mehr im aktuellen EmployeeModel vorhanden sind
                foreach (EmployeeClothesSize clothes in updatedEmployee.Clothes)
                {
                    // Erstellen einer Liste der aktuellen Bekleidungsgrößen des Mitarbeiter
                    var currentClothesSizes = updatedEmployee.Clothes
                        .Select(ecs => new { ecs.ClothesSize.Clothes.Id, ecs.ClothesSize.Size.Size })
                        .ToHashSet();

                    // Finden der DetailedEmployeeClothesItems mit der passenden EmployeeGuidID
                    var detailedItems = _employeeClothesSizeCollection
                        .Where(y => y.Employee.Id == updatedEmployee.Id)
                        .ToList();

                    //foreach (var item in detailedItems)
                    //{
                    //    if (!currentClothesSizes.Any(cs => cs.GuidId == item.ClothesGuidID && cs.Size == item.Size))
                    //    {
                    //        _employeeClothesSizeCollection.Remove(item);
                    //    }
                    //}
                }

                // Prüfen ob der zu bearbeitende Mitarbeiter neue Bekleidung hinzubekommen hat? DetailedEmployeeClothesItems hinzufügen
                // ?? Aktualisieren der DetailedEmployeeClothesItems
                //foreach (EmployeeClothesSize clothes in updatedEmployee.Clothes)
                //{
                //    var itemToUpdate = _employeeClothesSizeCollection
                //            .FirstOrDefault(y => y.Employee.GuidId == updatedEmployee.GuidId
                //                            && y.ClothesGuidID == clothes.ClothesSize.ClothesGuidId
                //                            && y.Size == clothes.ClothesSize.Size.Size);

                //    if (itemToUpdate != null)
                //    {
                //        itemToUpdate.Update(updatedEmployee, clothes);
                //    }
                //    else
                //    {
                //        _employeeClothesSizeCollection.Add(new DetailedEmployeeListingItemViewModel(updatedEmployee, clothes));
                //    }
                //}
            }
        }

        private void EmployeeStore_EmployeeDeleted(string EmployeeId)
        {
            EmployeeListingItemViewModel? ItemToDelete = _employeeCollection
                .FirstOrDefault(y => y.Employee.Id == EmployeeId);

            _employeeCollection.Remove(ItemToDelete);

            List<EmployeeClothesSize> itemsToDelete = _employeeClothesSizeCollection
                .Where(y => y.Employee.Id == EmployeeId)
                .ToList();

            foreach (EmployeeClothesSize item in itemsToDelete)
            {
                _employeeClothesSizeCollection.Remove(item);
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
