using DVS.Domain.Models;
using DVS.WPF.ViewModels;

namespace DVS.WPF.Commands.DragNDropCommands
{
    public class ReceivedNewEmployeeClothesListCommand(
        AddEditEmployeeListingViewModel addEditEmployeeListingViewModel,
        Action<AvailableClothesSizeItem> addItemToEmployeeClothesList)
        : CommandBase
    {
        public override void Execute(object parameter)
        {
            if (addEditEmployeeListingViewModel.SelectedAvailableClothesSizeItem.Quantity > 0)
            {
                AvailableClothesSizeItem? existingAcsi = addEditEmployeeListingViewModel.GetClothesSizeFrom_employeeClothesSizes();

                if (existingAcsi != null)
                    existingAcsi.Quantity += 1;
                else
                    addItemToEmployeeClothesList?.Invoke(CreateNewAcsi(addEditEmployeeListingViewModel));
            }
        }

        private static AvailableClothesSizeItem CreateNewAcsi(AddEditEmployeeListingViewModel _addEditEmployeeListingViewModel)
        {
            return new AvailableClothesSizeItem(_addEditEmployeeListingViewModel.SelectedAvailableClothesSizeItem.ClothesSize)
            {
                Quantity = 1
            };
        }
    }
}
