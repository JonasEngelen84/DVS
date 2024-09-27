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
            if (_addEditEmployeeListingViewModel.SelectedDetailedClothesItem.Quantity > 0)
            {
                DetailedClothesListingItemViewModel existingDclivm = GetDetailedClothesItem();
                DetailedClothesListingItemViewModel editedDclivm = CreateNewDetailedClothesitem(existingDclivm);
                AddOrUpdateDclivm(existingDclivm, editedDclivm);
            }
        }

        private DetailedClothesListingItemViewModel GetDetailedClothesItem()
        {
            DetailedClothesListingItemViewModel existingItem = _addEditEmployeeListingViewModel.EmployeeClothesList
                .FirstOrDefault(dclivm => dclivm.ClothesSizeGuidID == _addEditEmployeeListingViewModel.SelectedDetailedClothesItem.ClothesSizeGuidID);

            return existingItem;
        }

        private DetailedClothesListingItemViewModel CreateNewDetailedClothesitem(DetailedClothesListingItemViewModel existingDclivm)
        {
            Clothes editedClothes = new(_addEditEmployeeListingViewModel.SelectedDetailedClothesItem.Clothes.GuidID,
                                        _addEditEmployeeListingViewModel.SelectedDetailedClothesItem.Clothes.ID,
                                        _addEditEmployeeListingViewModel.SelectedDetailedClothesItem.Clothes.Name,
                                        _addEditEmployeeListingViewModel.SelectedDetailedClothesItem.Clothes.Category,
                                        _addEditEmployeeListingViewModel.SelectedDetailedClothesItem.Clothes.Season,
                                        _addEditEmployeeListingViewModel.SelectedDetailedClothesItem.Clothes.Comment)
            {
                Sizes = new ObservableCollection<ClothesSize>(_addEditEmployeeListingViewModel.SelectedDetailedClothesItem.Clothes.Sizes)
            };

            ClothesSize editedDclivm = new(_addEditEmployeeListingViewModel.SelectedDetailedClothesItem.ClothesSize.GuidID,
                                           editedClothes,
                                           _addEditEmployeeListingViewModel.SelectedDetailedClothesItem.ClothesSize.Size,
                                           (int)_addEditEmployeeListingViewModel.SelectedDetailedClothesItem.Quantity - 1,
                                           _addEditEmployeeListingViewModel.SelectedDetailedClothesItem.ClothesSize.Comment);
            
            ClothesSize targetClothesSize = new(_addEditEmployeeListingViewModel.SelectedDetailedClothesItem.ClothesSize.GuidID,
                                                editedClothes,
                                                _addEditEmployeeListingViewModel.SelectedDetailedClothesItem.ClothesSize.Size,
                                                existingDclivm?.Quantity + 1 ?? 1,
                                                _addEditEmployeeListingViewModel.SelectedDetailedClothesItem.ClothesSize.Comment);

            ClothesSize existingItem = editedClothes.Sizes.FirstOrDefault(cs => cs.GuidID == editedDclivm.GuidID);

            editedClothes.Sizes.Remove(existingItem);
            editedClothes.Sizes.Add(editedDclivm);

            return new DetailedClothesListingItemViewModel(editedClothes, targetClothesSize);
        }

        private void AddOrUpdateDclivm(DetailedClothesListingItemViewModel existingDclivm, DetailedClothesListingItemViewModel editedDclivm)
        {
            if (existingDclivm != null)
                existingDclivm.Update(editedDclivm.Clothes, editedDclivm.ClothesSize);
            else
                _addItemToEmployeeClothesList?.Invoke(editedDclivm);
        }
    }
}
