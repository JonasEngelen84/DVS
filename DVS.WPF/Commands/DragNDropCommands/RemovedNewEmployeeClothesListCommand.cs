using DVS.Domain.Models;
using DVS.WPF.ViewModels;

namespace DVS.WPF.Commands.DragNDropCommands
{
    public class RemovedNewEmployeeClothesListCommand(AddEditEmployeeListingViewModel addEditEmployeeListingViewModel,
                                                      Action<AvailableClothesSizeItem> removeItemFromEmployeeClothesList)
                                                      : CommandBase
    {
        private readonly AddEditEmployeeListingViewModel _addEditEmployeeListingViewModel = addEditEmployeeListingViewModel;

        public readonly Action<AvailableClothesSizeItem> _removeItemFromEmployeeClothesList = removeItemFromEmployeeClothesList;

        public override void Execute(object parameter)
        {
            if (_addEditEmployeeListingViewModel.SelectedAvailableClothesSizeItem.Quantity > 1)
                _addEditEmployeeListingViewModel.SelectedAvailableClothesSizeItem.Quantity -= 1;
            else if (_addEditEmployeeListingViewModel.SelectedAvailableClothesSizeItem.Quantity == 1)
                _removeItemFromEmployeeClothesList?.Invoke(_addEditEmployeeListingViewModel.SelectedAvailableClothesSizeItem);
        }
    }
}
