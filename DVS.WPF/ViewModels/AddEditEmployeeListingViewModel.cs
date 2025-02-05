using DVS.Domain.Models;
using DVS.WPF.Commands.DragNDropCommands;
using DVS.WPF.Stores;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace DVS.WPF.ViewModels
{
    public class AddEditEmployeeListingViewModel : ViewModelBase
    {
        private readonly ObservableCollection<DetailedClothesListingItemViewModel> _availableClothesSizes = [];
        public IEnumerable<DetailedClothesListingItemViewModel> AvailableClothesSizes => _availableClothesSizes;

        private readonly ObservableCollection<DetailedClothesListingItemViewModel> _employeeClothesList = [];
        public IEnumerable<DetailedClothesListingItemViewModel> EmployeeClothesList => _employeeClothesList;

        private List<Guid> EditedClothesList = [];

        private DetailedClothesListingItemViewModel _selectedDetailedClothesItem;
        public DetailedClothesListingItemViewModel SelectedDetailedClothesItem
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

            ClothesItemReceivedNewEmployeeClothesListCommand = new ClothesItemReceivedNewEmployeeClothesListCommand(this, AddItemToEmployeeClothesList);
            ClothesItemRemovedNewEmployeeClothesListCommand = new ClothesItemRemovedNewEmployeeClothesListCommand(this, RemoveItemFromEmployeeClothesList);
            ClothesItemReceivedAvailableClothesListCommand = new ClothesItemReceivedAvailableClothesListCommand(this, AddItemToAvailableSizes, AddEditedClothesList, RemoveEditedClothesList);
            ClothesItemRemovedAvailableClothesListCommand = new ClothesItemRemovedAvailableClothesListCommand(this, AddEditedClothesList, RemoveEditedClothesList);
        }


        public void ClearLists()
        {
            EditedClothesList.Clear();
            _employeeClothesList.Clear();
            _availableClothesSizes.Clear();
        }


        public void LoadAvailableSizes()
        {
            foreach (Clothes clothes in _clothesStore.Clothes)
            {
                if (clothes.Sizes.Count == 0)
                {
                    _availableClothesSizes.Add(new DetailedClothesListingItemViewModel(clothes, null));
                }
                else
                {
                    foreach (ClothesSize clothesSize in clothes.Sizes)
                    {
                        _availableClothesSizes.Add(new DetailedClothesListingItemViewModel(clothes, clothesSize));
                    }
                }
            }
        }

        public DetailedClothesListingItemViewModel GetClothesFrom_availableClothesSizes()
        {
            return _availableClothesSizes.FirstOrDefault(dclivm => dclivm.Clothes.GuidId == SelectedDetailedClothesItem.Clothes.GuidId);
        }
        
        public DetailedClothesListingItemViewModel GetClothesSizeFrom_availableClothesSizes()
        {
            return _availableClothesSizes.FirstOrDefault(dclivm => dclivm.ClothesSizeGuidId == SelectedDetailedClothesItem.ClothesSizeGuidId);
        }

        private void AddItemToAvailableSizes(DetailedClothesListingItemViewModel detailedClothesItem) => _availableClothesSizes.Add(detailedClothesItem);


        public void LoadEmployeeClothes(Employee employee)
        {
            foreach (EmployeeClothesSize clothes in employee.Clothes)
            {
                _employeeClothesList.Add(new DetailedClothesListingItemViewModel(clothes.ClothesSize.Clothes, clothes.ClothesSize)
                {
                    Quantity = clothes.Quantity
                });
            }
        }

        public DetailedClothesListingItemViewModel GetClothesSizeFrom_employeeClothesSizes()
        {
            return _employeeClothesList.FirstOrDefault(dclivm => dclivm.ClothesSizeGuidId == SelectedDetailedClothesItem.ClothesSizeGuidId);
        }

        private void AddItemToEmployeeClothesList(DetailedClothesListingItemViewModel detailedClothesItem) => _employeeClothesList.Add(detailedClothesItem);

        private void RemoveItemFromEmployeeClothesList(DetailedClothesListingItemViewModel detailedClothesItem)
        {
            DetailedClothesListingItemViewModel? itemToRemove = _employeeClothesList
                .FirstOrDefault(dclivm => dclivm.ClothesSizeGuidId == detailedClothesItem.ClothesSizeGuidId);

            if (itemToRemove != null)
            {
                _employeeClothesList.Remove(itemToRemove);
            }
        }


        public Guid? GetClothesSizeFrom_editedClothesList()
        {
            var found = EditedClothesList.FirstOrDefault(guid => guid == SelectedDetailedClothesItem.ClothesSizeGuidId);

            return found == default ? (Guid?)null : found;
        }

        public List<Guid> GetAllEditedClothes()
        {
            return EditedClothesList;
        }

        private void AddEditedClothesList(Guid ClothesSizeGuidId) => EditedClothesList.Add(ClothesSizeGuidId);

        private void RemoveEditedClothesList(Guid GuidIdToRemove)
        {
            EditedClothesList.Remove(GuidIdToRemove);
        }
    }
}
