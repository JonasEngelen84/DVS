using DVS.Domain.Models;
using DVS.Domain.Services.Interfaces;
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
        private readonly IDirtyEntitySaver _dirtyEntitySaver;

        public DVSListingViewModel(
            ClothesStore clothesStore,
            EmployeeStore employeeStore,
            ModalNavigationStore modalNavigationStore,
            CategoryStore categoryStore,
            SeasonStore seasonStore,
            ClothesSizeStore clothesSizeStore,
            EmployeeClothesSizeStore employeeClothesSizesStore,
            SelectedClothesSizeStore selectedClothesSizeStore,
            SelectedEmployeeClothesSizeStore selectedEmployeeClothesSizeStore,
            IDirtyEntitySaver dirtyEntitySaver)
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
            _dirtyEntitySaver = dirtyEntitySaver;

            _clothesStore.ClothesAdded += ClothesStore_ClothesAdded;
            _clothesStore.ClothesUpdated += ClothesStore_ClothesUpdated;
            _clothesStore.ClothesDeleted += ClothesStore_ClothesDeleted;

            _employeeStore.EmployeeAdded += EmployeeStore_EmployeeAdded;
            _employeeStore.EmployeeUpdated += EmployeeStore_EmployeeUpdated;
            _employeeStore.EmployeeDeleted += EmployeeStore_EmployeeDeleted;

            _clothesSizeStore.ClothesSizeAdded += ClothesSizeStore_ClothesSizeAdded;
            _clothesSizeStore.ClothesSizeUpdated += ClothesSizeStore_ClothesSizeUpdated;
            _clothesSizeStore.ClothesSizeDeleted += ClothesSizeStore_ClothesSizeDeleted;

            _employeeClothesSizeStore.EmployeeClothesSizeAdded += EmployeeClothesSizeStore_EmployeeClothesSizeAdded;
            _employeeClothesSizeStore.EmployeeClothesSizeUpdated += EmployeeClothesSizeStore_EmployeeClothesSizeUpdated;
            _employeeClothesSizeStore.EmployeeClothesSizeDeleted += EmployeeClothesSizeStore_EmployeeClothesSizeDeleted;

            LoadClothes(dirtyEntitySaver);
            LoadEmployees();
        }

        private void LoadClothes(IDirtyEntitySaver dirtyEntitySaver)
        {
            _clothesCollection.Clear();
            _clothesSizeCollection.Clear();

            foreach (Clothes clothes in _clothesStore.Clothes)
            {
                ClothesStore_ClothesAdded(clothes, dirtyEntitySaver);
            }
        }
        private void ClothesStore_ClothesAdded(Clothes clothes, IDirtyEntitySaver dirtyEntitySaver)
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
                dirtyEntitySaver));

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
            ClothesListingItemViewModel clivmToUpdate = _clothesCollection.First(clivm => clivm.Clothes.Id == editedClothes.Id);
            clivmToUpdate.Update(editedClothes);
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
            EmployeeListingItemViewModel ElivmToUpdate = _employeeCollection
                .First(e => e.Employee.Id == editedEmployee.Id);

            ElivmToUpdate.Update(editedEmployee);
        }
        private void EmployeeStore_EmployeeDeleted(string EmployeeId)
        {
            EmployeeListingItemViewModel? ItemToDelete = _employeeCollection
                .FirstOrDefault(y => y.Employee.Id == EmployeeId);

            _employeeCollection.Remove(ItemToDelete);

            List<EmployeeClothesSize> itemsToDelete = _employeeClothesSizeCollection
                .Where(y => y.Employee.Id == EmployeeId)
                .ToList();

            foreach (EmployeeClothesSize ecs in itemsToDelete)
            {
                EmployeeClothesSizeStore_EmployeeClothesSizeDeleted(ecs);
            }
        }

        private void ClothesSizeStore_ClothesSizeAdded(ClothesSize newClothesSize)
        {
            _clothesSizeCollection.Add(newClothesSize);
        }
        private void ClothesSizeStore_ClothesSizeUpdated(ClothesSize editedClothesSize)
        {
            ClothesSize? existingClothesSize = _clothesSizeCollection
                .FirstOrDefault(ecs => ecs.Id == editedClothesSize.Id);

            if (existingClothesSize != null)
            {
                _clothesSizeCollection.Remove(existingClothesSize);
            }

            _clothesSizeCollection.Add(editedClothesSize);
        }
        private void ClothesSizeStore_ClothesSizeDeleted(ClothesSize ClothesSizeToDelete)
        {
            ClothesSize existingClothesSize = _clothesSizeCollection
                .First(cs => cs.Id == ClothesSizeToDelete.Id);

            if (existingClothesSize != null)
            {
                _clothesSizeCollection.Remove(existingClothesSize);
            }
        }
        
        private void EmployeeClothesSizeStore_EmployeeClothesSizeAdded(EmployeeClothesSize newEcs)
        {
            _employeeClothesSizeCollection.Add(newEcs);
        }
        private void EmployeeClothesSizeStore_EmployeeClothesSizeUpdated(EmployeeClothesSize editedEcs)
        {
            EmployeeClothesSize? existingEcs = _employeeClothesSizeCollection
                .FirstOrDefault(ecs => ecs.Id == editedEcs.Id);

            if (existingEcs != null)
            {
                _employeeClothesSizeCollection.Remove(existingEcs);
                _employeeClothesSizeCollection.Add(editedEcs);
            }
            else
                _employeeClothesSizeCollection.Add(editedEcs);
        }
        private void EmployeeClothesSizeStore_EmployeeClothesSizeDeleted(EmployeeClothesSize ecsToDelete)
        {
            EmployeeClothesSize existingEcs = _employeeClothesSizeCollection
                .First(ecs => ecs.Id == ecsToDelete.Id);

            _employeeClothesSizeCollection.Remove(existingEcs);
        }

        protected override void Dispose()
        {
            _clothesStore.ClothesAdded -= ClothesStore_ClothesAdded;
            _clothesStore.ClothesUpdated -= ClothesStore_ClothesUpdated;
            _clothesStore.ClothesDeleted -= ClothesStore_ClothesDeleted;

            _employeeStore.EmployeeAdded -= EmployeeStore_EmployeeAdded;
            _employeeStore.EmployeeUpdated -= EmployeeStore_EmployeeUpdated;
            _employeeStore.EmployeeDeleted -= EmployeeStore_EmployeeDeleted;

            _clothesSizeStore.ClothesSizeAdded -= ClothesSizeStore_ClothesSizeAdded;
            _clothesSizeStore.ClothesSizeUpdated -= ClothesSizeStore_ClothesSizeUpdated;
            _clothesSizeStore.ClothesSizeDeleted -= ClothesSizeStore_ClothesSizeDeleted;

            _employeeClothesSizeStore.EmployeeClothesSizeAdded -= EmployeeClothesSizeStore_EmployeeClothesSizeAdded;
            _employeeClothesSizeStore.EmployeeClothesSizeUpdated -= EmployeeClothesSizeStore_EmployeeClothesSizeUpdated;
            _employeeClothesSizeStore.EmployeeClothesSizeDeleted -= EmployeeClothesSizeStore_EmployeeClothesSizeDeleted;

            base.Dispose();
        }
    }
}
