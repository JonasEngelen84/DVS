using DVS.Domain.Models;
using DVS.WPF.ViewModels;

namespace DVS.WPF.Commands.DragNDropCommands
{
    public class ReceivedNewEmployeeClothesListCommand(
        AddEditEmployeeListingViewModel addEditEmployeeListingViewModel,
        Action<EmployeeClothesSizeItem> addItemToEmployeeClothesList)
        : CommandBase
    {
        public override void Execute(object parameter)
        {
            if (addEditEmployeeListingViewModel.SelectedAvailableClothesSizeItem.Quantity > 0)
            {
                EmployeeClothesSizeItem? existingEcsi = addEditEmployeeListingViewModel.GetClothesSizeFrom_employeeClothesSizes();

                if (existingEcsi != null)
                    existingEcsi.Quantity += 1;
                else
                    addItemToEmployeeClothesList?.Invoke(CreateNewEcsi(addEditEmployeeListingViewModel));
            }
        }

        private static EmployeeClothesSizeItem CreateNewEcsi(AddEditEmployeeListingViewModel _addEditEmployeeListingViewModel)
        {
            return new EmployeeClothesSizeItem(_addEditEmployeeListingViewModel.SelectedAvailableClothesSizeItem.ClothesSize)
            {
                Quantity = 1
            };
        }
    }
}
