using DVS.Domain.Models;
using DVS.WPF.ViewModels;

namespace DVS.WPF.Commands.DragNDropCommands
{
    public class ReceivedNewEmployeeClothesListCommand(AddEditEmployeeListingViewModel addEditEmployeeListingViewModel,
                                                       Action<ClothesSizeListingItem> addItemToEmployeeClothesList)
                                                       : CommandBase
    {
        private readonly AddEditEmployeeListingViewModel _addEditEmployeeListingViewModel = addEditEmployeeListingViewModel;

        public readonly Action<ClothesSizeListingItem> _addItemToEmployeeClothesList = addItemToEmployeeClothesList;

        public override void Execute(object parameter)
        {
            if (_addEditEmployeeListingViewModel.SelectedDetailedClothesItem.Quantity > 0)
            {                
                ClothesSizeListingItem? existingDclivm = _addEditEmployeeListingViewModel.GetClothesSizeFrom_employeeClothesSizes();

                if (existingDclivm != null)
                    existingDclivm.Quantity += 1;
                else
                    _addItemToEmployeeClothesList?.Invoke(CreateNewDetailedClothesitem(_addEditEmployeeListingViewModel));
            }
        }

        private static ClothesSizeListingItem CreateNewDetailedClothesitem(AddEditEmployeeListingViewModel _addEditEmployeeListingViewModel)
        {
            ClothesSizeListingItem newDclivm = new(_addEditEmployeeListingViewModel.SelectedDetailedClothesItem.Clothes,
                                                                _addEditEmployeeListingViewModel.SelectedDetailedClothesItem.ClothesSize)
            {
                Quantity = 1
            };

            return newDclivm;
        }
    }
}
