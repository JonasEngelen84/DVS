using DVS.WPF.ViewModels;

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
                DetailedClothesListingItemViewModel? existingDclivm = _addEditEmployeeListingViewModel.GetClothesSizeFrom_employeeClothesSizes();

                if (existingDclivm != null)
                    existingDclivm.Quantity += 1;
                else
                    _addItemToEmployeeClothesList?.Invoke(CreateNewDetailedClothesitem(_addEditEmployeeListingViewModel));
            }
        }

        private static DetailedClothesListingItemViewModel CreateNewDetailedClothesitem(AddEditEmployeeListingViewModel _addEditEmployeeListingViewModel)
        {
            DetailedClothesListingItemViewModel newDclivm = new(_addEditEmployeeListingViewModel.SelectedDetailedClothesItem.Clothes,
                                                                _addEditEmployeeListingViewModel.SelectedDetailedClothesItem.ClothesSize)
            {
                Quantity = 1
            };

            return newDclivm;
        }
    }
}
