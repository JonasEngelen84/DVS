using DVS.WPF.ViewModels;

namespace DVS.WPF.Commands.DragNDropCommands
{
    public class ClothesItemRemovedNewEmployeeClothesListCommand(AddEditEmployeeListingViewModel addEditEmployeeListingViewModel,
                                                                 Action<DetailedClothesListingItemViewModel> removeItemFromEmployeeClothesList)
                                                                 : CommandBase
    {
        private readonly AddEditEmployeeListingViewModel _addEditEmployeeListingViewModel = addEditEmployeeListingViewModel;

        public readonly Action<DetailedClothesListingItemViewModel> _removeItemFromEmployeeClothesList = removeItemFromEmployeeClothesList;

        public override void Execute(object parameter)
        {
            if (_addEditEmployeeListingViewModel.SelectedDetailedClothesItem.Quantity > 1)
                _addEditEmployeeListingViewModel.SelectedDetailedClothesItem.Quantity -= 1;
            else if (_addEditEmployeeListingViewModel.SelectedDetailedClothesItem.Quantity == 1)
                _removeItemFromEmployeeClothesList?.Invoke(_addEditEmployeeListingViewModel.SelectedDetailedClothesItem);
        }
    }
}
