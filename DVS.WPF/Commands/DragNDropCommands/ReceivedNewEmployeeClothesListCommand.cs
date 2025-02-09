using DVS.Domain.Models;
using DVS.WPF.ViewModels;

namespace DVS.WPF.Commands.DragNDropCommands
{
    public class ReceivedNewEmployeeClothesListCommand(AddEditEmployeeListingViewModel addEditEmployeeListingViewModel,
                                                       Action<AvailableClothesSizeItem> addItemToEmployeeClothesList)
                                                       : CommandBase
    {
        private readonly AddEditEmployeeListingViewModel _addEditEmployeeListingViewModel = addEditEmployeeListingViewModel;

        public readonly Action<AvailableClothesSizeItem> _addItemToEmployeeClothesList = addItemToEmployeeClothesList;

        public override void Execute(object parameter)
        {
            if (_addEditEmployeeListingViewModel.SelectedClothesSizeItem.Quantity > 0)
            {
                AvailableClothesSizeItem? existingDclivm = _addEditEmployeeListingViewModel.GetClothesSizeFrom_employeeClothesSizes();

                if (existingDclivm != null)
                    existingDclivm.Quantity += 1;
                else
                    _addItemToEmployeeClothesList?.Invoke(CreateNewAcsi(_addEditEmployeeListingViewModel));
            }
        }

        private static AvailableClothesSizeItem CreateNewAcsi(AddEditEmployeeListingViewModel _addEditEmployeeListingViewModel)
        {
            return new AvailableClothesSizeItem(_addEditEmployeeListingViewModel.SelectedClothesSizeItem.ClothesSize)
            {
                Quantity = 1
            };
        }
    }
}
