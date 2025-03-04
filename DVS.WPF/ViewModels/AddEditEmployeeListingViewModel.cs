using DVS.Domain.Models;
using DVS.WPF.Commands.DragNDropCommands;
using DVS.WPF.Stores;
using DVS.WPF.ViewModels.ListingItems;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace DVS.WPF.ViewModels
{
    public class AddEditEmployeeListingViewModel : ViewModelBase
    {
        private readonly ObservableCollection<AvailableClothesSizeItem> _availableClothesSizes = [];
        public IEnumerable<AvailableClothesSizeItem> AvailableClothesSizes => _availableClothesSizes;

        private readonly ObservableCollection<EmployeeClothesSizeListingItemViewModel> _employeeClothesList = [];
        public IEnumerable<EmployeeClothesSizeListingItemViewModel> EmployeeClothesList => _employeeClothesList;

        private readonly List<AvailableClothesSizeItem> _clothesSizesToEdit = [];
        private readonly List<Clothes> _clothesToEdit = [];

        private AvailableClothesSizeItem _selectedAvailableClothesSizeItem;
        public AvailableClothesSizeItem SelectedAvailableClothesSizeItem
        {
            get => _selectedAvailableClothesSizeItem;
            set
            {
                if (_selectedAvailableClothesSizeItem != value)
                {
                    _selectedAvailableClothesSizeItem = value;
                    _selectedEmployeeClothesSizeItem = null;
                }
            }
        }

        private EmployeeClothesSizeListingItemViewModel _selectedEmployeeClothesSizeItem;
        public EmployeeClothesSizeListingItemViewModel SelectedEmployeeClothesSizeItem
        {
            get => _selectedEmployeeClothesSizeItem;
            set
            {
                if (_selectedEmployeeClothesSizeItem != value)
                {
                    _selectedEmployeeClothesSizeItem = value;
                    _selectedAvailableClothesSizeItem = null;
                }
            }
        }

        private readonly ClothesSizeStore _clothesSizeStore;

        public ICommand ClothesItemReceivedNewEmployeeClothesListCommand { get; }
        public ICommand ClothesItemRemovedNewEmployeeClothesListCommand { get; }
        public ICommand ClothesItemReceivedAvailableClothesListCommand { get; }
        public ICommand ClothesItemRemovedAvailableClothesListCommand { get; }


        public AddEditEmployeeListingViewModel(Employee? employee, ClothesSizeStore clothesSizeStore)
        {
            _clothesSizeStore = clothesSizeStore;

            ClothesItemRemovedNewEmployeeClothesListCommand = new RemovedNewEmployeeClothesListCommand(this, RemoveItemFromEmployeeClothesList);
            ClothesItemReceivedNewEmployeeClothesListCommand = new ReceivedNewEmployeeClothesListCommand(this, AddItemToEmployeeClothesList);
            ClothesItemRemovedAvailableClothesListCommand = new RemovedAvailableClothesListCommand(this, AddClothesSizesToEdit, AddClothesToEdit);
            ClothesItemReceivedAvailableClothesListCommand = new ReceivedAvailableClothesListCommand(this, AddClothesSizesToEdit, AddClothesToEdit);

            LoadAvailableSizes();
            LoadEmployeeClothes(employee);
        }


        public void LoadAvailableSizes()
        {
            _availableClothesSizes.Clear();
            _clothesSizesToEdit.Clear();

            foreach (ClothesSize clothesSize in _clothesSizeStore.ClothesSizes)
            {
                _availableClothesSizes.Add(new AvailableClothesSizeItem(clothesSize)
                {
                    Quantity = clothesSize.Quantity
                });
            }
        }
        public AvailableClothesSizeItem? GetAvailableClothesSizeItemFrom_availableClothesSizes(Guid guidId)
        {
            return _availableClothesSizes.FirstOrDefault(acsi => acsi.ClothesSizeId == guidId);
        }
        public Clothes GetClothesFrom_availableClothesSizes()
        {
            return _availableClothesSizes
                .FirstOrDefault(acsi => acsi.ClothesId == SelectedAvailableClothesSizeItem.ClothesId)
                .ClothesSize.Clothes;
        }
        public ClothesSize GetClothesSizeFrom_availableClothesSizes()
        {
            return _availableClothesSizes
                .FirstOrDefault(acsi => acsi.ClothesSizeId == SelectedAvailableClothesSizeItem.ClothesSizeId)
                .ClothesSize;
        }
        
        public void LoadEmployeeClothes(Employee? employee)
        {
            _employeeClothesList.Clear();

            if (employee != null)
            {
                foreach (EmployeeClothesSize ecs in employee.Clothes)
                {
                    _employeeClothesList.Add(new EmployeeClothesSizeListingItemViewModel(ecs.ClothesSize, ecs.GuidId)
                    {
                        Quantity = ecs.Quantity
                    });
                }
            }
        }
        public EmployeeClothesSizeListingItemViewModel GetClothesSizeFrom_employeeClothesSizes()
        {
            return _employeeClothesList.FirstOrDefault(ecsi => ecsi.ClothesSize.GuidId == SelectedAvailableClothesSizeItem.ClothesSize.GuidId);
        }
        private void AddItemToEmployeeClothesList(EmployeeClothesSizeListingItemViewModel ecslivm) => _employeeClothesList.Add(ecslivm);
        private void RemoveItemFromEmployeeClothesList(EmployeeClothesSizeListingItemViewModel ecslivm) => _employeeClothesList.Remove(ecslivm);

        public AvailableClothesSizeItem? GetClothesSizeFrom_clothesSizesToEdit(Guid guidId)
        {
            var found = _clothesSizesToEdit.FirstOrDefault(acsi => acsi.ClothesSizeId == guidId);
            return found == default ? (AvailableClothesSizeItem?)null : found;
        }
        public List<AvailableClothesSizeItem> GetAllClothesSizesToEdit() { return _clothesSizesToEdit; }
        private void AddClothesSizesToEdit(AvailableClothesSizeItem acsi) => _clothesSizesToEdit.Add(acsi);
        
        public Clothes? GetClothesFrom_clothesToEdit(string Id)
        {
            var found = _clothesToEdit.FirstOrDefault(c => c.Id == Id);
            return found == default ? (Clothes?)null : found;
        }
        public List<Clothes> GetAllClothesToEdit() { return _clothesToEdit; }
        private void AddClothesToEdit(Clothes clothes) => _clothesToEdit.Add(clothes);
    }
}
