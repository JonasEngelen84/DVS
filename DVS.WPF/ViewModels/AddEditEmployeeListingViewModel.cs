using DVS.Domain.Models;
using DVS.WPF.Commands.DragNDropCommands;
using DVS.WPF.Stores;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace DVS.WPF.ViewModels
{
    public class AddEditEmployeeListingViewModel : ViewModelBase
    {
        private readonly ObservableCollection<ClothesSizeListingItemViewModel> _availableClothesSizes = [];
        public IEnumerable<ClothesSizeListingItemViewModel> AvailableClothesSizes => _availableClothesSizes;

        private readonly ObservableCollection<ClothesSizeListingItemViewModel> _employeeClothesList = [];
        public IEnumerable<ClothesSizeListingItemViewModel> EmployeeClothesList => _employeeClothesList;

        private readonly List<Guid> _editedClothesList = [];

        private ClothesSizeListingItemViewModel _selectedDetailedClothesItem;
        public ClothesSizeListingItemViewModel SelectedDetailedClothesItem
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
                }
            }
        }

        private readonly ClothesStore _clothesStore;

        public ICommand ClothesItemReceivedNewEmployeeClothesListCommand { get; }
        public ICommand ClothesItemRemovedNewEmployeeClothesListCommand { get; }
        public ICommand ClothesItemReceivedAvailableClothesListCommand { get; }
        public ICommand ClothesItemRemovedAvailableClothesListCommand { get; }


        public AddEditEmployeeListingViewModel(ClothesStore clothesStore)
        {
            _clothesStore = clothesStore;

            ClothesItemReceivedNewEmployeeClothesListCommand = new ReceivedNewEmployeeClothesListCommand(this, AddItemToEmployeeClothesList);
            ClothesItemRemovedNewEmployeeClothesListCommand = new RemovedNewEmployeeClothesListCommand(this, RemoveItemFromEmployeeClothesList);
            ClothesItemReceivedAvailableClothesListCommand = new ReceivedAvailableClothesListCommand(this, AddItemToAvailableSizes, AddEditedClothesList, RemoveEditedClothesList);
            ClothesItemRemovedAvailableClothesListCommand = new RemovedAvailableClothesListCommand(this, AddEditedClothesList, RemoveEditedClothesList);
        }


        public void ClearLists()
        {
            _editedClothesList.Clear();
            _employeeClothesList.Clear();
            _availableClothesSizes.Clear();
        }


        public void LoadAvailableSizes()
        {
            foreach (Clothes clothes in _clothesStore.Clothes)
            {
                if (clothes.Sizes.Count == 0)
                {
                    _availableClothesSizes.Add(new ClothesSizeListingItemViewModel(clothes, null));
                }
                else
                {
                    foreach (ClothesSize clothesSize in clothes.Sizes)
                    {
                        _availableClothesSizes.Add(new ClothesSizeListingItemViewModel(clothes, clothesSize));
                    }
                }
            }
        }

        public ClothesSizeListingItemViewModel GetClothesFrom_availableClothesSizes()
        {
            return _availableClothesSizes.FirstOrDefault(dclivm => dclivm.Clothes.GuidId == SelectedDetailedClothesItem.Clothes.GuidId);
        }
        
        public ClothesSizeListingItemViewModel GetClothesSizeFrom_availableClothesSizes()
        {
            return _availableClothesSizes.FirstOrDefault(dclivm => dclivm.ClothesSizeGuidId == SelectedDetailedClothesItem.ClothesSizeGuidId);
        }

        private void AddItemToAvailableSizes(ClothesSizeListingItemViewModel detailedClothesItem) => _availableClothesSizes.Add(detailedClothesItem);


        public void LoadEmployeeClothes(Employee employee)
        {
            foreach (EmployeeClothesSize clothes in employee.Clothes)
            {
                _employeeClothesList.Add(new ClothesSizeListingItemViewModel(clothes.ClothesSize.Clothes, clothes.ClothesSize)
                {
                    Quantity = clothes.Quantity
                });
            }
        }

        public ClothesSizeListingItemViewModel GetClothesSizeFrom_employeeClothesSizes()
        {
            return _employeeClothesList.FirstOrDefault(dclivm => dclivm.ClothesSizeGuidId == SelectedDetailedClothesItem.ClothesSizeGuidId);
        }

        private void AddItemToEmployeeClothesList(ClothesSizeListingItemViewModel detailedClothesItem) => _employeeClothesList.Add(detailedClothesItem);

        private void RemoveItemFromEmployeeClothesList(ClothesSizeListingItemViewModel detailedClothesItem)
        {
            ClothesSizeListingItemViewModel? itemToRemove = _employeeClothesList
                .FirstOrDefault(dclivm => dclivm.ClothesSizeGuidId == detailedClothesItem.ClothesSizeGuidId);

            if (itemToRemove != null)
            {
                _employeeClothesList.Remove(itemToRemove);
            }
        }


        public Guid? GetClothesSizeFrom_editedClothesList()
        {
            var found = _editedClothesList.FirstOrDefault(guid => guid == SelectedDetailedClothesItem.ClothesSizeGuidId);

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
