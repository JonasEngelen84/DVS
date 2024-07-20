using DVS.Stores;
using DVS.ViewModels;

namespace DVS.Commands.DragNDropCommands
{
    public class ClothesItemReceivedCommand(
        DVSListingViewModel dVSListingViewModel, ClothesStore clothesStore) : CommandBase
    {
        private readonly DVSListingViewModel _dVSListingViewModel = dVSListingViewModel;

        public override void Execute(object parameter)
        {
            if (parameter.Equals("AddEditEmployeeNewEmployeeClothesList"))
                _dVSListingViewModel.AddClothesItemToNewEmployeeListingItemCollection(
                    _dVSListingViewModel.IncomingClothesListingItemModel);
            else
            {
                _dVSListingViewModel.AddClothesItemToDetailedClothesListingItemCollection(
                    _dVSListingViewModel.IncomingClothesListingItemModel);
            }
        }
    }
}
