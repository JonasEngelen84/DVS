using DVS.Domain.Models;
using DVS.WPF.Commands;
using DVS.WPF.Stores;
using DVS.WPF.ViewModels.Forms;
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

        public List<Clothes> EditedClothesSizesList { get; } = [];

        private readonly ClothesStore _clothesStore;

        public ICommand ClothesItemReceivedCommand { get; }
        public ICommand ClothesItemRemovedCommand { get; }


        public AddEditEmployeeListingViewModel(DVSListingViewModel dVSListingViewModel,
                                               ClothesStore clothesStore,
                                               Employee? employee)
        {
            _clothesStore = clothesStore;

            ClothesItemReceivedCommand = new ClothesItemReceivedCommand(SelectedDetailedClothesItem, AddItemToAvailableSizes, AddItemToEmployeeClothesList);
            ClothesItemRemovedCommand = new ClothesItemRemovedCommand(SelectedDetailedClothesItem, RemoveItemFromAvailableSizes, RemoveItemFromEmployeeClothesList);

            LoadAvailableSizes();
            LoadEmployeeClothes(employee);
        }


        private void LoadAvailableSizes()
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
                    foreach (ClothesSize size in clothes.Sizes)
                    {
                        _availableClothesSizes.Add(new DetailedClothesListingItemViewModel(clothes, size));
                    }
                }
            }
        }

        private void AddItemToAvailableSizes(Clothes clothes)
        {
            // Item in _availableClothesSizes updaten
            DetailedClothesListingItemViewModel? ItemToUpdate = _availableClothesSizes
                        .FirstOrDefault(y => y.Clothes.GuidID == clothes.GuidID
                        && y.Size == _selectedDetailedClothesItem.Size);

            ItemToUpdate.Update(clothes, ItemToUpdate.ClothesSize);

            // Geänderte Clothes-Instanz speichern/ersetzen.
            // Wird später an DVSListingViewModel übergeben für update DB und ClothesStore
            if (EditedClothesSizesList.Contains(clothes))
            {
                EditedClothesSizesList.Remove(clothes);
            }
            else
                EditedClothesSizesList.Add(clothes);
        }

        private void RemoveItemFromAvailableSizes(Clothes clothes)
        {
            // Item in _availableClothesSizes updaten
            DetailedClothesListingItemViewModel? ItemToUpdate = _availableClothesSizes
                        .FirstOrDefault(y => y.Clothes.GuidID == clothes.GuidID
                        && y.Size == _selectedDetailedClothesItem.Size);

            ItemToUpdate.Update(clothes, ItemToUpdate.ClothesSize);

            // Geänderte Clothes-Instanz speichern/ersetzen.
            // Wird später an DVSListingViewModel übergeben für update DB und ClothesStore
            if (EditedClothesSizesList.Contains(clothes))
            {
                EditedClothesSizesList.Remove(clothes);
            }
            else
                EditedClothesSizesList.Add(clothes);
        }


        private void LoadEmployeeClothes(Employee? employee)
        {
            _employeeClothesList.Clear();

            if (employee != null)
            {
                foreach (EmployeeClothesSize clothes in employee.Clothes)
                {
                    _employeeClothesList.Add(new DetailedClothesListingItemViewModel(clothes.ClothesSize.Clothes, clothes.ClothesSize));
                }
            }
        }

        private void AddItemToEmployeeClothesList(Clothes clothes)
        {
            // Item in _employeeClothesList updaten
            DetailedClothesListingItemViewModel? ItemToUpdate = _employeeClothesList
                        .FirstOrDefault(y => y.Clothes.GuidID == clothes.GuidID
                        && y.Size == _selectedDetailedClothesItem.Size);

            ItemToUpdate.Update(clothes, ItemToUpdate.ClothesSize);
        }

        private void RemoveItemFromEmployeeClothesList(Clothes clothes)
        {
            // Item in _employeeClothesList updaten
            DetailedClothesListingItemViewModel? ItemToUpdate = _employeeClothesList
                        .FirstOrDefault(y => y.Clothes.GuidID == clothes.GuidID
                        && y.Size == _selectedDetailedClothesItem.Size);

            ItemToUpdate.Update(clothes, ItemToUpdate.ClothesSize);
        }
    }
}
