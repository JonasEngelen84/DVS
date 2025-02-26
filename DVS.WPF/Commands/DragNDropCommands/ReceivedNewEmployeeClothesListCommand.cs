using DVS.WPF.ViewModels;
using DVS.WPF.ViewModels.ListingItems;

namespace DVS.WPF.Commands.DragNDropCommands
{
    public class ReceivedNewEmployeeClothesListCommand(
        AddEditEmployeeListingViewModel addEditEmployeeListingViewModel,
        Action<EmployeeClothesSizeListingItemViewModel> addItemToEmployeeClothesList)
        : CommandBase
    {
        public override void Execute(object parameter)
        {
            if (addEditEmployeeListingViewModel.SelectedAvailableClothesSizeItem.Quantity > 0)
            {
                EmployeeClothesSizeListingItemViewModel? existingEcsi = addEditEmployeeListingViewModel.GetClothesSizeFrom_employeeClothesSizes();

                if (existingEcsi != null) existingEcsi.Quantity += 1;
                else
                {
                   EmployeeClothesSizeListingItemViewModel newEcslivm = new(addEditEmployeeListingViewModel.SelectedAvailableClothesSizeItem.ClothesSize)
                    {
                        Quantity = 1
                    };

                    addItemToEmployeeClothesList?.Invoke(newEcslivm);
                }
            }
        }
    }
}
