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

        public List<DetailedClothesListingItemViewModel> EditedClothesList { get; } = [];

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
            ClothesItemReceivedAvailableClothesListCommand = new ClothesItemReceivedAvailableClothesListCommand(this, AddItemToAvailableSizes);
            ClothesItemRemovedAvailableClothesListCommand = new ClothesItemRemovedAvailableClothesListCommand(this);
        }


        public void ClearEditedClothesList()
        {
            EditedClothesList.Clear();
        }


        public void LoadAvailableSizes()
        {
            _availableClothesSizes.Clear();

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

        private void AddItemToAvailableSizes(DetailedClothesListingItemViewModel detailedClothesItem) => _availableClothesSizes.Add(detailedClothesItem);


        public void LoadEmployeeClothes(Employee employee)
        {
            _employeeClothesList.Clear();

            foreach (EmployeeClothesSize clothes in employee.Clothes)
            {
                _employeeClothesList.Add(new DetailedClothesListingItemViewModel(clothes.ClothesSize.Clothes, clothes.ClothesSize));
            }
        }

        private void AddItemToEmployeeClothesList(DetailedClothesListingItemViewModel detailedClothesItem) => _employeeClothesList.Add(detailedClothesItem);

        private void RemoveItemFromEmployeeClothesList(DetailedClothesListingItemViewModel detailedClothesItem)
        {
            var itemToRemove = _employeeClothesList.FirstOrDefault(dclivm => dclivm.ClothesSizeGuidId == detailedClothesItem.ClothesSizeGuidId);

            if (itemToRemove != null)
            {
                _employeeClothesList.Remove(itemToRemove);
            }
        }
    }
}
