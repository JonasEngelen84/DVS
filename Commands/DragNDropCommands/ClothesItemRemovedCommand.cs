using DVS.ViewModels;

namespace DVS.Commands.DragNDropCommands
{
    public class ClothesItemRemovedCommand: CommandBase
    {
        private readonly DVSListingViewModel _dVSListingViewModel;

        public ClothesItemRemovedCommand(DVSListingViewModel dVSListingViewModel)
        {
            _dVSListingViewModel = dVSListingViewModel;
        }

        public override void Execute(object parameter)
        {
            if (parameter.Equals("AddEditEmployeClothesList"))
                _dVSListingViewModel.RemoveClothesItemFromNewEmployeeListingItemCollection(_dVSListingViewModel.RemovedClothesListingItemModel);
            else if (parameter.Equals("AddEditEmployeeNewEmployeeClothesList"))
                _dVSListingViewModel.RemoveClothesItemFromDetailedClothesListingItemCollection(_dVSListingViewModel.RemovedClothesListingItemModel);
        }
    }
}
