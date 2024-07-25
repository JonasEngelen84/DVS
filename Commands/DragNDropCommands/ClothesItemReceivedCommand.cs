using DVS.Models;
using DVS.Stores;
using DVS.ViewModels;

namespace DVS.Commands.DragNDropCommands
{
    public class ClothesItemReceivedCommand(
        DVSListingViewModel dVSListingViewModel, ClothesStore clothesStore) : AsyncCommandBase
    {
        private readonly DVSListingViewModel _dVSListingViewModel = dVSListingViewModel;
        private readonly ClothesStore _clothesStore = clothesStore;

        public override async Task ExecuteAsync(object parameter)
        {
            if (parameter.Equals("AddEditEmployeeNewEmployeeClothesList"))
                _dVSListingViewModel.AddClothesItemToNewEmployeeListingItemCollection();
            else
            {
                DetailedClothesListingItemModel? existingItem = _dVSListingViewModel.DetailedClothesListingItemCollection
                .FirstOrDefault(modelItem => modelItem.ID == _dVSListingViewModel.IncomingClothesListingItemModel.ID
                && modelItem.Size == _dVSListingViewModel.IncomingClothesListingItemModel.Size);

                ClothesModel clothes = new(_dVSListingViewModel.IncomingClothesListingItemModel.Clothes.GuidID,
                                           _dVSListingViewModel.IncomingClothesListingItemModel.Clothes.ID,
                                           _dVSListingViewModel.IncomingClothesListingItemModel.Clothes.Name,
                                           _dVSListingViewModel.IncomingClothesListingItemModel.Clothes.Category,
                                           _dVSListingViewModel.IncomingClothesListingItemModel.Clothes.Season,
                                           null);

                _dVSListingViewModel.RemovedClothesListingItemModel.ErrorMessage = null;

                if (existingItem == null)
                {
                    _dVSListingViewModel.RemovedClothesListingItemModel.ErrorMessage =
                        "Verschieben der Bekleidung ist fehlgeschlagen!\nBitte versuchen Sie es erneut.";
                }
                else
                {
                    clothes.Sizes = existingItem.Clothes.Sizes;
                    var size = existingItem.Clothes.Sizes.FirstOrDefault(modelItem => modelItem.Size == _dVSListingViewModel.IncomingClothesListingItemModel.Size);
                    size.Quantity++;

                    try
                    {
                        await _clothesStore.DragNDropUpdate(clothes);
                    }
                    catch (Exception)
                    {
                        dVSListingViewModel.RemovedClothesListingItemModel.ErrorMessage =
                            "Verschieben der Bekleidung ist fehlgeschlagen!\nBitte versuchen Sie es erneut.";
                    }
                }
            }
        }
    }
}
