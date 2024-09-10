using DVS.Domain.Models;
using DVS.WPF.ViewModels;
using System.Collections.ObjectModel;

namespace DVS.WPF.Commands.DragNDropCommands
{
    public class ClothesItemReceivedNewEmployeeClothesListCommand(AddEditEmployeeListingViewModel addEditEmployeeListingViewModel,
                                                                  Action<DetailedClothesListingItemViewModel> addItemToEmployeeClothesList)
                                                                  : CommandBase
    {
        private readonly AddEditEmployeeListingViewModel _addEditEmployeeListingViewModel = addEditEmployeeListingViewModel;

        public readonly Action<DetailedClothesListingItemViewModel> _addItemToEmployeeClothesList = addItemToEmployeeClothesList;

        public override void Execute(object parameter)
        {
            if (_addEditEmployeeListingViewModel.SelectedDetailedClothesItem != null || _addEditEmployeeListingViewModel.SelectedDetailedClothesItem.Quantity > 0)
            {
                DetailedClothesListingItemViewModel existingItem = GetDetailedClothesItem(parameter);

                DetailedClothesListingItemViewModel editedItem = CreateNewDetailedClothesitem(existingItem);

                AddOrUpdateItem(existingItem, editedItem);
            }
        }

        private DetailedClothesListingItemViewModel GetDetailedClothesItem(object parameter)
        {
            DetailedClothesListingItemViewModel existingItem = _addEditEmployeeListingViewModel.EmployeeClothesList
                .FirstOrDefault(dclivm => dclivm.Clothes.GuidID == _addEditEmployeeListingViewModel.SelectedDetailedClothesItem.Clothes.GuidID
                    && dclivm.ClothesSizeGuidID == _addEditEmployeeListingViewModel.SelectedDetailedClothesItem.ClothesSizeGuidID);

            return existingItem;
        }

        private DetailedClothesListingItemViewModel CreateNewDetailedClothesitem(DetailedClothesListingItemViewModel existingItem)
        {
            ClothesSize editedItem = new(_addEditEmployeeListingViewModel.SelectedDetailedClothesItem.ClothesSize.GuidID,
                                         _addEditEmployeeListingViewModel.SelectedDetailedClothesItem.Clothes,
                                         _addEditEmployeeListingViewModel.SelectedDetailedClothesItem.ClothesSize.Size,
                                         _addEditEmployeeListingViewModel.SelectedDetailedClothesItem.ClothesSize.Quantity - 1,
                                         _addEditEmployeeListingViewModel.SelectedDetailedClothesItem.ClothesSize.Comment);

            Clothes editedClothes = new(_addEditEmployeeListingViewModel.SelectedDetailedClothesItem.Clothes.GuidID,
                                        _addEditEmployeeListingViewModel.SelectedDetailedClothesItem.Clothes.ID,
                                        _addEditEmployeeListingViewModel.SelectedDetailedClothesItem.Clothes.Name,
                                        _addEditEmployeeListingViewModel.SelectedDetailedClothesItem.Clothes.Category,
                                        _addEditEmployeeListingViewModel.SelectedDetailedClothesItem.Clothes.Season,
                                        _addEditEmployeeListingViewModel.SelectedDetailedClothesItem.Clothes.Comment)
            {
                Sizes = new ObservableCollection<ClothesSize>(_addEditEmployeeListingViewModel.SelectedDetailedClothesItem.Clothes.Sizes
                    .Select(s => new ClothesSize(s.GuidID, s.Clothes, s.Size, s.Quantity, s.Comment)))
            };

            editedClothes.Sizes.Remove(_addEditEmployeeListingViewModel.SelectedDetailedClothesItem.ClothesSize);
            editedClothes.Sizes.Add(editedItem);

            ClothesSize targetSize = new(_addEditEmployeeListingViewModel.SelectedDetailedClothesItem.ClothesSize.GuidID,
                                         _addEditEmployeeListingViewModel.SelectedDetailedClothesItem.Clothes,
                                         _addEditEmployeeListingViewModel.SelectedDetailedClothesItem.ClothesSize.Size,
                                         existingItem?.ClothesSize?.Quantity + 1 ?? 1,
                                         _addEditEmployeeListingViewModel.SelectedDetailedClothesItem.ClothesSize.Comment);

            return new DetailedClothesListingItemViewModel(editedClothes, targetSize);
        }

        private void AddOrUpdateItem(DetailedClothesListingItemViewModel existingItem, DetailedClothesListingItemViewModel editedItem)
        {
            if (existingItem != null)
                existingItem.Update(editedItem.Clothes, editedItem.ClothesSize);
            else
                _addItemToEmployeeClothesList?.Invoke(editedItem);
        }
    }
}
