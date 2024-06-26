using DVS.ViewModels;

namespace DVS.Commands.DragNDropCommands
{
    public class ClothesItemRemovedCommand(DVSListingViewModel dVSListingViewModel) : CommandBase
    {
        private readonly DVSListingViewModel _dVSListingViewModel = dVSListingViewModel;

        public override void Execute(object parameter)
        {
            if (parameter.Equals("AddEditEmployeAvailableClothesList"))
                _dVSListingViewModel.RemoveClothesItemFromNewEmployeeListingItemCollection(
                    _dVSListingViewModel.RemovedClothesListingItemModel);
            else if (parameter.Equals("AddEditEmployeeNewEmployeeClothesList"))
                _dVSListingViewModel.RemoveClothesItemFromDetailedClothesListingItemCollection(
                    _dVSListingViewModel.RemovedClothesListingItemModel);
        }
    }
}
