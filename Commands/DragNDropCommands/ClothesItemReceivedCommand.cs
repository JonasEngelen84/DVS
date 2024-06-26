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
            _dVSListingViewModel.AddClothesItem(_dVSListingViewModel.IncomingClothesListingItemModel);
        }
    }
}
