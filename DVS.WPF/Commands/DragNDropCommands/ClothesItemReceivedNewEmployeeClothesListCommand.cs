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
                DetailedClothesListingItemViewModel existingDclivm = _addEditEmployeeListingViewModel.EmployeeClothesList
                    .FirstOrDefault(dclivm => dclivm.ClothesSizeGuidId == _addEditEmployeeListingViewModel.SelectedDetailedClothesItem.ClothesSizeGuidId);

                DetailedClothesListingItemViewModel editedDclivm = CreateNewDetailedClothesitem(existingDclivm);

                AddOrUpdateDclivm(existingDclivm, editedDclivm);
            }
        }

        private DetailedClothesListingItemViewModel CreateNewDetailedClothesitem(DetailedClothesListingItemViewModel existingDclivm)
        {            
            ClothesSize editedClothesSize = new(_addEditEmployeeListingViewModel.SelectedDetailedClothesItem.ClothesSize.GuidId,
                                                _addEditEmployeeListingViewModel.SelectedDetailedClothesItem.Clothes,
                                                _addEditEmployeeListingViewModel.SelectedDetailedClothesItem.ClothesSize.Size,
                                                (int)_addEditEmployeeListingViewModel.SelectedDetailedClothesItem.Quantity - 1,
                                                _addEditEmployeeListingViewModel.SelectedDetailedClothesItem.ClothesSize.Comment);
            
            ClothesSize targetClothesSize = new(_addEditEmployeeListingViewModel.SelectedDetailedClothesItem.ClothesSize.GuidId,
                                                _addEditEmployeeListingViewModel.SelectedDetailedClothesItem.Clothes,
                                                _addEditEmployeeListingViewModel.SelectedDetailedClothesItem.ClothesSize.Size,
                                                existingDclivm?.Quantity + 1 ?? 1,
                                                _addEditEmployeeListingViewModel.SelectedDetailedClothesItem.ClothesSize.Comment);

            ClothesSize existingClothesSize = _addEditEmployeeListingViewModel.SelectedDetailedClothesItem.Clothes.Sizes.FirstOrDefault(cs => cs.GuidId == editedClothesSize.GuidId);

            _addEditEmployeeListingViewModel.SelectedDetailedClothesItem.Clothes.Sizes.Remove(existingClothesSize);
            _addEditEmployeeListingViewModel.SelectedDetailedClothesItem.Clothes.Sizes.Add(editedClothesSize);

            return new DetailedClothesListingItemViewModel(_addEditEmployeeListingViewModel.SelectedDetailedClothesItem.Clothes, targetClothesSize);
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
