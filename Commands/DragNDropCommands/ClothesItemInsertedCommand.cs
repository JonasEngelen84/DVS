using DVS.ViewModels;

namespace DVS.Commands.DragNDropCommands
{
    internal class ClothesItemInsertedCommand : CommandBase
    {
        private readonly DVSListingViewModel _dVSListingViewModel;

        public ClothesItemInsertedCommand(DVSListingViewModel dVSListingViewModel)
        {
            _dVSListingViewModel = dVSListingViewModel;
        }

        public override void Execute(object parameter)
        {
            //_dVSListingViewModel.InsertClothesItem(
            //    _dVSListingViewModel.InsertedClothesListingItemModel, _dVSListingViewModel.TargetClothesListingItemModel);
        }
    }
}
