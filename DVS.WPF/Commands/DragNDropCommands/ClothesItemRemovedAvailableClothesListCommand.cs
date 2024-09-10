using DVS.Domain.Models;
using DVS.WPF.ViewModels;
using System.Collections.ObjectModel;

namespace DVS.WPF.Commands.DragNDropCommands
{
    public class ClothesItemRemovedAvailableClothesListCommand(AddEditEmployeeListingViewModel addEditEmployeeListingViewModel,
                                                               Action<DetailedClothesListingItemViewModel> removeItemFromAvailableClothesList)
                                                               : CommandBase
    {
        private readonly AddEditEmployeeListingViewModel _addEditEmployeeListingViewModel = addEditEmployeeListingViewModel;

        public readonly Action<DetailedClothesListingItemViewModel> _removeItemFromAvailableClothesList = removeItemFromAvailableClothesList;

        public override void Execute(object parameter)
        {
            if (_addEditEmployeeListingViewModel.SelectedDetailedClothesItem != null || _addEditEmployeeListingViewModel.SelectedDetailedClothesItem.Quantity > 0)
            {
                DetailedClothesListingItemViewModel existingItem = GetDetailedClothesItem();

                DetailedClothesListingItemViewModel editedItem = CreateNewDetailedClothesitem(existingItem);

                RemoveOrUpdateItem(existingItem, editedItem);
            }
        }


        private DetailedClothesListingItemViewModel GetDetailedClothesItem()
        {
            DetailedClothesListingItemViewModel existingItem = _addEditEmployeeListingViewModel.AvailableClothesSizes
                .FirstOrDefault(dclivm => dclivm.Clothes.GuidID == _addEditEmployeeListingViewModel.SelectedDetailedClothesItem.Clothes.GuidID
                    && dclivm.ClothesSizeGuidID == _addEditEmployeeListingViewModel.SelectedDetailedClothesItem.ClothesSizeGuidID);

            return existingItem;
        }

        private DetailedClothesListingItemViewModel CreateNewDetailedClothesitem(DetailedClothesListingItemViewModel existingItem)
        {
            ClothesSize editedItem = new(_addEditEmployeeListingViewModel.SelectedDetailedClothesItem.ClothesSize.GuidID,
                                         _addEditEmployeeListingViewModel.SelectedDetailedClothesItem.Clothes,
                                         _addEditEmployeeListingViewModel.SelectedDetailedClothesItem.ClothesSize.Size,
                                         existingItem.ClothesSize.Quantity - 1,
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

            return new DetailedClothesListingItemViewModel(editedClothes, editedItem);
        }

        private void RemoveOrUpdateItem(DetailedClothesListingItemViewModel existingItem, DetailedClothesListingItemViewModel editedItem)
        {
            existingItem?.Update(editedItem.Clothes, editedItem.ClothesSize);


            var existingClothesItem = _addEditEmployeeListingViewModel.EditedClothesList
                    .FirstOrDefault(c => c.GuidID == editedItem.Clothes.GuidID)?
                    .Sizes.FirstOrDefault(cs => cs.GuidID == editedItem.ClothesSizeGuidID);

            int index = _addEditEmployeeListingViewModel.EditedClothesList.FindIndex(y => y.GuidID == existingClothesItem?.GuidID);

            if (index != -1)
                _addEditEmployeeListingViewModel.EditedClothesList[index] = editedItem.Clothes;
            else
                _addEditEmployeeListingViewModel.EditedClothesList.Add(editedItem.Clothes);
        }
    }
}
