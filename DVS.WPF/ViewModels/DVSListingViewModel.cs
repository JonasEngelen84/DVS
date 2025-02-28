using DVS.Domain.Models;
using DVS.WPF.Stores;
using DVS.WPF.ViewModels.ListingItems;
using System.Collections.ObjectModel;

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
        private readonly ClothesStore _clothesStore;
        private readonly EmployeeStore _employeeStore;
        private readonly CategoryStore _categoryStore;
        private readonly SeasonStore _seasonStore;
        private readonly ClothesSizeStore _clothesSizeStore;
        private readonly EmployeeClothesSizeStore _employeeClothesSizeStore;
        private readonly SelectedClothesSizeStore _selectedClothesSizeStore;
        private readonly SelectedEmployeeClothesSizeStore _selectedEmployeeClothesSizeStore;

        public DVSListingViewModel(
            ClothesStore clothesStore,
            EmployeeStore employeeStore,
            ModalNavigationStore modalNavigationStore,
            CategoryStore categoryStore,
            SeasonStore seasonStore,
            ClothesSizeStore clothesSizeStore,
            EmployeeClothesSizeStore employeeClothesSizesStore,
            SelectedClothesSizeStore selectedClothesSizeStore,
            SelectedEmployeeClothesSizeStore selectedEmployeeClothesSizeStore)
        {
            _clothesStore = clothesStore;
            _employeeStore = employeeStore;
            _modalNavigationStore = modalNavigationStore;
            _categoryStore = categoryStore;
            _seasonStore = seasonStore;
            _clothesSizeStore = clothesSizeStore;
            _employeeClothesSizeStore = employeeClothesSizesStore;
            _selectedClothesSizeStore = selectedClothesSizeStore;
            _selectedEmployeeClothesSizeStore = selectedEmployeeClothesSizeStore;

            _clothesStore.ClothesAdded += ClothesStore_ClothesAdded;
            _clothesStore.ClothesUpdated += ClothesStore_ClothesUpdated;
            _clothesStore.ClothesDeleted += ClothesStore_ClothesDeleted;
            _employeeStore.EmployeeAdded += EmployeeStore_EmployeeAdded;
            _employeeStore.EmployeeUpdated += EmployeeStore_EmployeeUpdated;
            _employeeStore.EmployeeDeleted += EmployeeStore_EmployeeDeleted;
            _clothesSizeStore.ClothesSizeUpdated += ClothesSizeStore_ClothesSizeUpdated;
            _employeeClothesSizeStore.EmployeeClothesSizeUpdated += EmployeeClothesSizeStore_EmployeeClothesSizeUpdate;

            LoadClothes();
            LoadEmployees();
        }

        private void LoadClothes()
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
                _categoryStore,
                _seasonStore,
                _clothesStore,
                _clothesSizeStore,
                _employeeClothesSizeStore,
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
        private void ClothesStore_ClothesUpdated(Clothes editedClothes)
        {
            // ClothesItem:
            ClothesListingItemViewModel clivmToUpdate = _clothesCollection.First(clivm => clivm.Clothes.Id == editedClothes.Id);
            clivmToUpdate.Update(editedClothes);


            // ClothesSizeItem:
            List<string> currentClothesSizes = editedClothes.Sizes.Select(s => s.Size).ToList();

            // Finden aller ClothesSizes mit der passenden ClothesID
            List<ClothesSize> existingClothesSizes = _clothesSizeCollection .Where(y => y.Clothes.Id == editedClothes.Id) .ToList();
        }        
        private void ClothesStore_ClothesDeleted(string ClothesId)
        {
            ClothesListingItemViewModel? ItemToDelete = _clothesCollection
                .FirstOrDefault(y => y.Clothes.Id == ClothesId);

            _clothesCollection.Remove(ItemToDelete);
        }        

        private void LoadEmployees()
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
            _employeeCollection.Add(new EmployeeListingItemViewModel(
                newEmployee,
                this,
                _modalNavigationStore,
                _employeeStore,
                _clothesStore,
                _categoryStore,
                _seasonStore,
                _clothesSizeStore,
                _employeeClothesSizeStore));

            if (newEmployee.Clothes.Count > 0)
            {
                foreach (EmployeeClothesSize ecs in newEmployee.Clothes)
                {
                    _employeeClothesSizeCollection.Add(ecs);
                }
            }
        }        
        private void EmployeeStore_EmployeeUpdated(Employee editedEmployee)
        {
            // Finden des zu bearbeitenden EmployeeItem mit der passenden EmployeeGuidID
            EmployeeListingItemViewModel? ItemToUpdate = _employeeCollection
                .FirstOrDefault(y => y.Employee.Id == editedEmployee.Id);

            ItemToUpdate?.Update(editedEmployee);

            // Entfernen sämtlicher Bekleidung des zu bearbeitenden Mitarbeiters
            if (editedEmployee.Clothes.Count == 0)
            {
                // Finden aller EmployeeClothesSizes mit der passenden EmployeeGuidID
                List<EmployeeClothesSize> itemsToDelete = _employeeClothesSizeCollection
                .Where(y => y.Employee.Id == editedEmployee.Id)
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
                foreach (EmployeeClothesSize clothes in editedEmployee.Clothes)
                {
                    // Erstellen einer Liste der aktuellen Bekleidungsgrößen des Mitarbeiter
                    var currentClothesSizes = editedEmployee.Clothes
                        .Select(ecs => new { ecs.ClothesSize.Clothes.Id, ecs.ClothesSize.Size })
                        .ToHashSet();

                    // Finden der DetailedEmployeeClothesItems mit der passenden EmployeeGuidID
                    var detailedItems = _employeeClothesSizeCollection
                        .Where(y => y.Employee.Id == editedEmployee.Id)
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

        private void ClothesSizeStore_ClothesSizeUpdated(ClothesSize editedClothesSize)
        {
            ClothesSize existingClothesSize = _clothesSizeCollection.First(cs => cs.GuidId == editedClothesSize.GuidId);

            if (existingClothesSize != null)
            {
                _clothesSizeCollection.Remove(existingClothesSize);
                _clothesSizeCollection.Add(editedClothesSize);
            }
        }
        
        private void EmployeeClothesSizeStore_EmployeeClothesSizeUpdate(EmployeeClothesSize editedEcs)
        {
            EmployeeClothesSize existingEcs = _employeeClothesSizeCollection
                .First(ecs => ecs.GuidId == editedEcs.GuidId);

            if (existingEcs != null)
            {
                _employeeClothesSizeCollection.Remove(existingEcs);
                _employeeClothesSizeCollection.Add(editedEcs);
                                
                //_employeeCollection.Remove(existingEcs.Employee);
                //_employeeCollection.Add(editedEcs);
            }            

            EmployeeListingItemViewModel elivmToUpdate = _employeeCollection
                .First(elivm => elivm.Employee.Id == existingEcs.Employee.Id);

            elivmToUpdate?.Update(editedEcs.Employee);
        }

        protected override void Dispose()
        {
            _clothesStore.ClothesAdded -= ClothesStore_ClothesAdded;
            _clothesStore.ClothesUpdated -= ClothesStore_ClothesUpdated;
            _clothesStore.ClothesDeleted -= ClothesStore_ClothesDeleted;
            _clothesSizeStore.ClothesSizeUpdated -= ClothesSizeStore_ClothesSizeUpdated;
            _employeeStore.EmployeeAdded -= EmployeeStore_EmployeeAdded;
            _employeeStore.EmployeeUpdated -= EmployeeStore_EmployeeUpdated;
            _employeeStore.EmployeeDeleted += EmployeeStore_EmployeeDeleted;
            _employeeClothesSizeStore.EmployeeClothesSizeUpdated += EmployeeClothesSizeStore_EmployeeClothesSizeUpdate;

            base.Dispose();
        }
    }
}
