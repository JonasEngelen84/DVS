using DVS.Domain.Models;
using DVS.WPF.ViewModels;

namespace DVS.WPF.Commands.DragNDropCommands
{
    public class RemovedNewEmployeeClothesListCommand(
        AddEditEmployeeListingViewModel addEditEmployeeListingViewModel,
        Action<AvailableClothesSizeItem> removeItemFromEmployeeClothesList)
        : CommandBase
    {
        public override void Execute(object parameter)
        {
            if (addEditEmployeeListingViewModel.SelectedAvailableClothesSizeItem.Quantity > 1)
                addEditEmployeeListingViewModel.SelectedAvailableClothesSizeItem.Quantity -= 1;
            else if (addEditEmployeeListingViewModel.SelectedAvailableClothesSizeItem.Quantity == 1)
                removeItemFromEmployeeClothesList?.Invoke(addEditEmployeeListingViewModel.SelectedAvailableClothesSizeItem);
        }
    }
}
