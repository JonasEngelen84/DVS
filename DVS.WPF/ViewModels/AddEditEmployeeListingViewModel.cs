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

        private readonly ObservableCollection<AvailableClothesSizeItem> _employeeClothesList = [];
        public IEnumerable<AvailableClothesSizeItem> EmployeeClothesList => _employeeClothesList;

        private readonly List<Guid> _editedClothesList = [];

        private AvailableClothesSizeItem _selectedClothesSizeItem;
        public AvailableClothesSizeItem SelectedClothesSizeItem
        {
            get
            {
                return _selectedClothesSizeItem;
            }
            set
            {
                if (_selectedClothesSizeItem != value)
                {
                    _selectedClothesSizeItem = value;
                }
            }
        }
        
        //private EmployeeClothesSizeItem _selectedEmployeeClothesSizeItem;
        //public EmployeeClothesSizeItem SelectedEmployeeClothesSizeItem
        //{
        //    get
        //    {
        //        return _selectedEmployeeClothesSizeItem;
        //    }
        //    set
        //    {
        //        if (_selectedEmployeeClothesSizeItem != value)
        //        {
        //            _selectedEmployeeClothesSizeItem = value;
        //        }
        //    }
        //}

        private readonly ClothesSizeStore _clothesSizeStore;
        //private readonly EmployeeClothesSizeStore _employeeClothesSizeStore;

        public ICommand ClothesItemReceivedNewEmployeeClothesListCommand { get; }
        public ICommand ClothesItemRemovedNewEmployeeClothesListCommand { get; }
        public ICommand ClothesItemReceivedAvailableClothesListCommand { get; }
        public ICommand ClothesItemRemovedAvailableClothesListCommand { get; }


        public AddEditEmployeeListingViewModel(ClothesSizeStore clothesSizeStore)
        {
            _clothesSizeStore = clothesSizeStore;
            //_employeeClothesSizeStore = employeeClothesSizeStore;

            ClothesItemReceivedNewEmployeeClothesListCommand = new ReceivedNewEmployeeClothesListCommand(this, AddItemToEmployeeClothesList);
            ClothesItemRemovedNewEmployeeClothesListCommand = new RemovedNewEmployeeClothesListCommand(this, RemoveItemFromEmployeeClothesList);
            ClothesItemReceivedAvailableClothesListCommand = new ReceivedAvailableClothesListCommand(this, AddItemToAvailableSizes, AddEditedClothesList, RemoveEditedClothesList);
            ClothesItemRemovedAvailableClothesListCommand = new RemovedAvailableClothesListCommand(this, AddEditedClothesList, RemoveEditedClothesList);
        }


        public void LoadAvailableSizes()
        {
            _availableClothesSizes.Clear();
            _editedClothesList.Clear();

            foreach (ClothesSize clothesSize in _clothesSizeStore.ClothesSizes)
            {
                _availableClothesSizes.Add(new AvailableClothesSizeItem(clothesSize));
            }
        }

        public AvailableClothesSizeItem GetClothesFrom_availableClothesSizes()
        {
            return _availableClothesSizes.FirstOrDefault(acsi => acsi.ClothesSize.Clothes.Id == SelectedClothesSizeItem.ClothesSize.Clothes.Id);
        }
        
        public AvailableClothesSizeItem GetClothesSizeFrom_availableClothesSizes()
        {
            return _availableClothesSizes.FirstOrDefault(acsi => acsi.ClothesSize.GuidId == SelectedClothesSizeItem.ClothesSize.GuidId);
        }

        private void AddItemToAvailableSizes(AvailableClothesSizeItem acsi) => _availableClothesSizes.Add(acsi);


        public void LoadEmployeeClothes(Employee? employee)
        {
            _employeeClothesList.Clear();

            if (employee != null)
            {
                foreach (EmployeeClothesSize ecs in employee.Clothes)
                {
                    _employeeClothesList.Add(new AvailableClothesSizeItem(ecs.ClothesSize));
                }
            }
        }

        public AvailableClothesSizeItem GetClothesSizeFrom_employeeClothesSizes()
        {
            return _employeeClothesList.FirstOrDefault(acsi => acsi.ClothesSize.GuidId == SelectedClothesSizeItem.ClothesSize.GuidId);
        }

        private void AddItemToEmployeeClothesList(AvailableClothesSizeItem acsi) => _employeeClothesList.Add(acsi);

        private void RemoveItemFromEmployeeClothesList(AvailableClothesSizeItem acsi)
        {
            AvailableClothesSizeItem? acsiToRemove = _employeeClothesList
                .FirstOrDefault(acsi => acsi.ClothesSize.GuidId == acsi.ClothesSize.GuidId);

            if (acsiToRemove != null)
            {
                _employeeClothesList.Remove(acsiToRemove);
            }
        }


        public Guid? GetClothesSizeFrom_editedClothesList()
        {
            var found = _editedClothesList.FirstOrDefault(guid => guid == SelectedClothesSizeItem.ClothesSize.GuidId);

            return found == default ? (Guid?)null : found;
        }

        public List<Guid> GetAllEditedClothes()
        {
            return _editedClothesList;
        }

        private void AddEditedClothesList(Guid ClothesSizeGuidId) => _editedClothesList.Add(ClothesSizeGuidId);

        private void RemoveEditedClothesList(Guid GuidIdToRemove)
        {
            _editedClothesList.Remove(GuidIdToRemove);
        }
    }
}
