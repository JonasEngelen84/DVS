using DVS.Domain.Models;
using DVS.WPF.Commands.DragNDropCommands;
using DVS.WPF.Stores;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace DVS.WPF.ViewModels
{
    public class AddEditEmployeeListingViewModel : ViewModelBase
    {
        private readonly ObservableCollection<AvailableClothesSizeItem> _availableClothesSizes = [];
        public IEnumerable<AvailableClothesSizeItem> AvailableClothesSizes => _availableClothesSizes;

        private readonly ObservableCollection<EmployeeClothesSizeItem> _employeeClothesList = [];
        public IEnumerable<EmployeeClothesSizeItem> EmployeeClothesList => _employeeClothesList;

        private readonly List<AvailableClothesSizeItem> _editedClothesSizesList = [];
        private readonly List<Clothes> _editedClothesList = [];

        private AvailableClothesSizeItem _selectedAvailableClothesSizeItem;
        public AvailableClothesSizeItem SelectedAvailableClothesSizeItem
        {
            get => _selectedAvailableClothesSizeItem;
            set
            {
                if (_selectedAvailableClothesSizeItem != value)
                {
                    _selectedAvailableClothesSizeItem = value;
                }
            }
        }
        
        private EmployeeClothesSizeItem _selectedEmployeeClothesSizeItem;
        public EmployeeClothesSizeItem SelectedEmployeeClothesSizeItem
        {
            get => _selectedEmployeeClothesSizeItem;
            set
            {
                if (_selectedEmployeeClothesSizeItem != value)
                {
                    _selectedEmployeeClothesSizeItem = value;
                }
            }
        }

        private readonly ClothesSizeStore _clothesSizeStore;

        public ICommand ClothesItemReceivedNewEmployeeClothesListCommand { get; }
        public ICommand ClothesItemRemovedNewEmployeeClothesListCommand { get; }
        public ICommand ClothesItemReceivedAvailableClothesListCommand { get; }
        public ICommand ClothesItemRemovedAvailableClothesListCommand { get; }


        public AddEditEmployeeListingViewModel(ClothesSizeStore clothesSizeStore)
        {
            _clothesSizeStore = clothesSizeStore;

            ClothesItemRemovedNewEmployeeClothesListCommand = new RemovedNewEmployeeClothesListCommand(this, RemoveItemFromEmployeeClothesList);
            ClothesItemReceivedNewEmployeeClothesListCommand = new ReceivedNewEmployeeClothesListCommand(this, AddItemToEmployeeClothesList);
            ClothesItemRemovedAvailableClothesListCommand = new RemovedAvailableClothesListCommand(
                this,
                AddEditedClothesSizesList,
                RemoveEditedClothesSizesList,
                AddEditedClothesList,
                RemoveEditedClothesList);
            ClothesItemReceivedAvailableClothesListCommand = new ReceivedAvailableClothesListCommand(
                this,
                AddItemToAvailableSizes,
                AddEditedClothesSizesList,
                RemoveEditedClothesSizesList,
                AddEditedClothesList,
                RemoveEditedClothesList);
        }


        public void LoadAvailableSizes()
        {
            _availableClothesSizes.Clear();
            _editedClothesSizesList.Clear();

            foreach (ClothesSize clothesSize in _clothesSizeStore.ClothesSizes)
            {
                _availableClothesSizes.Add(new AvailableClothesSizeItem(clothesSize)
                {
                    Quantity = clothesSize.Quantity
                });
            }
        }
        public AvailableClothesSizeItem GetAvailableClothesSizeItemFrom_availableClothesSizes()
        {
            return _availableClothesSizes
                .FirstOrDefault(acsi => acsi.ClothesId == SelectedAvailableClothesSizeItem.ClothesId &&
                acsi.ClothesSizeId == SelectedAvailableClothesSizeItem.ClothesSizeId);
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
        private void AddItemToAvailableSizes(AvailableClothesSizeItem acsi) => _availableClothesSizes.Add(acsi);

        public void LoadEmployeeClothes(Employee? employee)
        {
            _employeeClothesList.Clear();

            if (employee != null)
            {
                foreach (EmployeeClothesSize ecs in employee.Clothes)
                {
                    _employeeClothesList.Add(new EmployeeClothesSizeItem(ecs.ClothesSize) { Quantity = ecs.Quantity});
                }
            }
        }
        public EmployeeClothesSizeItem GetClothesSizeFrom_employeeClothesSizes()
        {
            return _employeeClothesList.FirstOrDefault(ecsi => ecsi.ClothesSize.GuidId == SelectedAvailableClothesSizeItem.ClothesSize.GuidId);
        }
        private void AddItemToEmployeeClothesList(EmployeeClothesSizeItem ecsi) => _employeeClothesList.Add(ecsi);
        private void RemoveItemFromEmployeeClothesList(EmployeeClothesSizeItem ecsi)
        {
            EmployeeClothesSizeItem ecsiToRemove = _employeeClothesList
                .First(ecsi => ecsi.ClothesSize.GuidId == ecsi.ClothesSize.GuidId);

            if (ecsiToRemove != null)
            {
                _employeeClothesList.Remove(ecsiToRemove);
            }
        }

        public AvailableClothesSizeItem? GetClothesSizeFrom_editedClothesSizesList()
        {
            var found = _editedClothesSizesList
                .FirstOrDefault(acsi => acsi.ClothesSizeId == SelectedAvailableClothesSizeItem.ClothesSizeId);

            return found == default ? (AvailableClothesSizeItem?)null : found;
        }
        public List<AvailableClothesSizeItem> GetAllEditedClothesSizes() { return _editedClothesSizesList; }
        private void AddEditedClothesSizesList(AvailableClothesSizeItem acsi) => _editedClothesSizesList.Add(acsi);
        private void RemoveEditedClothesSizesList(AvailableClothesSizeItem acsi) => _editedClothesSizesList.Remove(acsi);

        public Clothes? GetClothesFrom_editedClothesList()
        {
            var found = _editedClothesList
                .FirstOrDefault(c => c == SelectedAvailableClothesSizeItem.ClothesSize.Clothes);

            return found == default ? (Clothes?)null : found;
        }
        public List<Clothes> GetAllEditedClothes() { return _editedClothesList; }
        private void AddEditedClothesList(Clothes clothes) => _editedClothesList.Add(clothes);
        private void RemoveEditedClothesList(Clothes clothes) => _editedClothesList.Remove(clothes);
    }
}
