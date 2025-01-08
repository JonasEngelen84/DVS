using DVS.Domain.Models;
using DVS.WPF.ViewModels;
using System.Collections.ObjectModel;

namespace DVS.WPF.Commands.DragNDropCommands
{
    class ClothesItemReceivedAvailableClothesListCommand(AddEditEmployeeListingViewModel addEditEmployeeListingViewModel,
                                                         Action<DetailedClothesListingItemViewModel> addItemToAvailableClothesList)
                                                         : CommandBase
    {
        private readonly AddEditEmployeeListingViewModel _addEditEmployeeListingViewModel = addEditEmployeeListingViewModel;

        public readonly Action<DetailedClothesListingItemViewModel> _addItemToAvailableClothesList = addItemToAvailableClothesList;

        public override void Execute(object parameter)
        {
            if (_addEditEmployeeListingViewModel.SelectedDetailedClothesItem.Quantity > 0)
            {
                DetailedClothesListingItemViewModel existingDclivm = GetDetailedClothesItem();
                DetailedClothesListingItemViewModel editedDclivm = CreateNewDetailedClothesitem(existingDclivm);
                AddOrUpdateItem(existingDclivm, editedDclivm);
            }
        }

        private DetailedClothesListingItemViewModel GetDetailedClothesItem()
        {
            DetailedClothesListingItemViewModel existingItem = _addEditEmployeeListingViewModel.AvailableClothesSizes
                .FirstOrDefault(dclivm => dclivm.Clothes.GuidId == _addEditEmployeeListingViewModel.SelectedDetailedClothesItem.Clothes.GuidId
                    && dclivm.ClothesSizeGuidId == _addEditEmployeeListingViewModel.SelectedDetailedClothesItem.ClothesSizeGuidId);

            return existingItem;
        }

        private DetailedClothesListingItemViewModel CreateNewDetailedClothesitem(DetailedClothesListingItemViewModel existingDclivm)
        {
            ClothesSize editedDclivm = new(_addEditEmployeeListingViewModel.SelectedDetailedClothesItem.ClothesSize.GuidId,
                                           _addEditEmployeeListingViewModel.SelectedDetailedClothesItem.Clothes,
                                           _addEditEmployeeListingViewModel.SelectedDetailedClothesItem.ClothesSize.Size,
                                           existingDclivm?.ClothesSize?.Quantity + 1 ?? 1,
                                           _addEditEmployeeListingViewModel.SelectedDetailedClothesItem.ClothesSize.Comment);

            Clothes editedClothes = new(_addEditEmployeeListingViewModel.SelectedDetailedClothesItem.Clothes.GuidId,
                                        _addEditEmployeeListingViewModel.SelectedDetailedClothesItem.Clothes.Id,
                                        _addEditEmployeeListingViewModel.SelectedDetailedClothesItem.Clothes.Name,
                                        _addEditEmployeeListingViewModel.SelectedDetailedClothesItem.Clothes.Category,
                                        _addEditEmployeeListingViewModel.SelectedDetailedClothesItem.Clothes.Season,
                                        _addEditEmployeeListingViewModel.SelectedDetailedClothesItem.Clothes.Comment)
            {
                Sizes = new ObservableCollection<ClothesSize>(_addEditEmployeeListingViewModel.SelectedDetailedClothesItem.Clothes.Sizes)
            };

            ClothesSize existingItem = editedClothes.Sizes.FirstOrDefault(cs => cs.GuidId == editedDclivm.GuidId);

            editedClothes.Sizes.Remove(existingItem);
            editedClothes.Sizes.Add(editedDclivm);

            return new DetailedClothesListingItemViewModel(editedClothes, editedDclivm);
        }

        private void AddOrUpdateItem(DetailedClothesListingItemViewModel existingItem, DetailedClothesListingItemViewModel editedItem)
        {
            if (existingItem != null)
                existingItem.Update(editedItem.Clothes, editedItem.ClothesSize);
            else
                _addItemToAvailableClothesList?.Invoke(editedItem);


            var existingClothesItem = _addEditEmployeeListingViewModel.EditedClothesList
                    .FirstOrDefault(c => c.GuidId == editedItem.Clothes.GuidId)?
                    .Sizes.FirstOrDefault(cs => cs.GuidId == editedItem.ClothesSizeGuidId);

            int index = _addEditEmployeeListingViewModel.EditedClothesList.FindIndex(y => y.GuidId == existingClothesItem?.GuidId);

            if (index != -1)
                _addEditEmployeeListingViewModel.EditedClothesList[index] = editedItem.Clothes;
            else
                _addEditEmployeeListingViewModel.EditedClothesList.Add(editedItem.Clothes);
        }
    }
}
