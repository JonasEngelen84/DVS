using DVS.Domain.Models;
using DVS.WPF.ViewModels;

namespace DVS.WPF.Commands.DragNDropCommands
{
    public class ReceivedNewEmployeeClothesListCommand(AddEditEmployeeListingViewModel addEditEmployeeListingViewModel,
                                                       Action<ClothesSizeListingItemViewModel> addItemToEmployeeClothesList)
                                                       : CommandBase
    {
        private readonly AddEditEmployeeListingViewModel _addEditEmployeeListingViewModel = addEditEmployeeListingViewModel;

        public readonly Action<ClothesSizeListingItemViewModel> _addItemToEmployeeClothesList = addItemToEmployeeClothesList;

        public override void Execute(object parameter)
        {
            if (_addEditEmployeeListingViewModel.SelectedDetailedClothesItem.Quantity > 0)
            {                
                ClothesSizeListingItemViewModel? existingDclivm = _addEditEmployeeListingViewModel.GetClothesSizeFrom_employeeClothesSizes();

                if (existingDclivm != null)
                    existingDclivm.Quantity += 1;
                else
                    _addItemToEmployeeClothesList?.Invoke(CreateNewDetailedClothesitem(_addEditEmployeeListingViewModel));
            }
        }

        private static ClothesSizeListingItemViewModel CreateNewDetailedClothesitem(AddEditEmployeeListingViewModel _addEditEmployeeListingViewModel)
        {
            ClothesSizeListingItemViewModel newDclivm = new(_addEditEmployeeListingViewModel.SelectedDetailedClothesItem.Clothes,
                                                                _addEditEmployeeListingViewModel.SelectedDetailedClothesItem.ClothesSize)
            {
                Quantity = 1
            };

            return newDclivm;
        }
    }
}
