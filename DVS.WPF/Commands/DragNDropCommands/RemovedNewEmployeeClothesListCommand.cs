using DVS.WPF.ViewModels;
using DVS.WPF.ViewModels.ListingItems;

namespace DVS.WPF.Commands.DragNDropCommands
{
    public class RemovedNewEmployeeClothesListCommand(
        AddEditEmployeeListingViewModel addEditEmployeeListingViewModel,
        Action<EmployeeClothesSizeListingItemViewModel> removeItemFromEmployeeClothesList)
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
