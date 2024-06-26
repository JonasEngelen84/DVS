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
            _dVSListingViewModel.RemoveClothesItem(_dVSListingViewModel.RemovedClothesListingItemModel);
        }
    }
}
