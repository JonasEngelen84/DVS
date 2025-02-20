using DVS.Domain.Models;
using DVS.WPF.ViewModels;

namespace DVS.WPF.Commands.DragNDropCommands
{
    public class RemovedNewEmployeeClothesListCommand(
        AddEditEmployeeListingViewModel addEditEmployeeListingViewModel,
        Action<EmployeeClothesSizeItem> removeItemFromEmployeeClothesList)
        : CommandBase
    {
        public override void Execute(object parameter)
        {
            if (addEditEmployeeListingViewModel.SelectedEmployeeClothesSizeItem.Quantity > 1)
                addEditEmployeeListingViewModel.SelectedEmployeeClothesSizeItem.Quantity -= 1;
            else if (addEditEmployeeListingViewModel.SelectedEmployeeClothesSizeItem.Quantity == 1)
                removeItemFromEmployeeClothesList?.Invoke(addEditEmployeeListingViewModel.SelectedEmployeeClothesSizeItem);
        }
    }
}
