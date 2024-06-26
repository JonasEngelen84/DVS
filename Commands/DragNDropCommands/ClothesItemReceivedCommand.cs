using DVS.Components;
using DVS.ViewModels;

namespace DVS.Commands.DragNDropCommands
{
    public class ClothesItemReceivedCommand : CommandBase
    {
        private readonly DVSListingViewModel _dVSListingViewModel;

        public ClothesItemReceivedCommand(DVSListingViewModel dVSListingViewModel)
        {
            _dVSListingViewModel = dVSListingViewModel;
        }

        public override void Execute(object parameter)
        {
            if (parameter.Equals("AddEditEmployeClothesList"))
                _dVSListingViewModel.AddClothesItemToDetailedClothesListingItemCollection(_dVSListingViewModel.IncomingClothesListingItemModel);
            else if (parameter.Equals("AddEditEmployeeNewEmployeeClothesList"))
                _dVSListingViewModel.AddClothesItemToNewEmployeeListingItemCollection(_dVSListingViewModel.IncomingClothesListingItemModel);
        }
    }
}
