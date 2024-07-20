using DVS.Models;
using DVS.Stores;
using DVS.ViewModels;
using System.Windows;

namespace DVS.Commands.DragNDropCommands
{
    public class ClothesItemRemovedCommand(
        DVSListingViewModel dVSListingViewModel, ClothesStore clothesStore) : AsyncCommandBase
    {
        private readonly DVSListingViewModel _dVSListingViewModel = dVSListingViewModel;
        private readonly ClothesStore _clothesStore = clothesStore;

        public override async Task ExecuteAsync(object parameter)
        {
            if (parameter.Equals("AddEditEmployeAvailableClothesList"))
                _dVSListingViewModel.RemoveClothesItemFromNewEmployeeListingItemCollection();
            else
            {
                ClothesModel clothesToEdit = new(
                    _dVSListingViewModel.RemovedClothesListingItemModel.Clothes.GuidID,
                    _dVSListingViewModel.RemovedClothesListingItemModel.ID,
                    _dVSListingViewModel.RemovedClothesListingItemModel.Name,
                    _dVSListingViewModel.RemovedClothesListingItemModel.Clothes.Category,
                    _dVSListingViewModel.RemovedClothesListingItemModel.Clothes.Season,
                    _dVSListingViewModel.RemovedClothesListingItemModel.Comment);
                clothesToEdit.Sizes = _dVSListingViewModel.RemovedClothesListingItemModel.Clothes.Sizes;

                _dVSListingViewModel.RemovedClothesListingItemModel.ErrorMessage = null;

                if (_dVSListingViewModel.RemovedClothesListingItemModel.Quantity == 0)
                {
                    string messageBoxText = "Diese Bekleidung ist nicht verfügbar!";
                    string caption = "Bekleidung entfernen";
                    MessageBoxButton button = MessageBoxButton.OK;
                    MessageBoxImage icon = MessageBoxImage.Warning;
                    _ = MessageBox.Show(messageBoxText, caption, button, icon);
                    return;
                }
                else if (_dVSListingViewModel.RemovedClothesListingItemModel.Quantity <= 3)
                {
                    string messageBoxText = $"ACHTUNG!\n\nVon dieser Bekleidung sind nur noch" +
                        $"  {_dVSListingViewModel.RemovedClothesListingItemModel.Quantity}  Stück vorhanden!";
                    string caption = "Bekleidung entfernen";
                    MessageBoxButton button = MessageBoxButton.OK;
                    MessageBoxImage icon = MessageBoxImage.Warning;
                    _ = MessageBox.Show(messageBoxText, caption, button, icon);

                    ClothesSizeModel sizeToEdit = clothesToEdit.Sizes.FirstOrDefault(
                        y => y.Size == _dVSListingViewModel.RemovedClothesListingItemModel.Size);
                    sizeToEdit.Quantity -= 1;

                }
                else
                {
                    ClothesSizeModel sizeToEdit = clothesToEdit.Sizes.FirstOrDefault(
                        y => y.Size == _dVSListingViewModel.RemovedClothesListingItemModel.Size);
                    sizeToEdit.Quantity -= 1;
                }
                    
                try
                {
                    await _clothesStore.Update(clothesToEdit);
                }
                catch (Exception)
                {
                    _dVSListingViewModel.RemovedClothesListingItemModel.ErrorMessage =
                        "Verschieben der Bekleidung ist fehlgeschlagen!\nBitte versuchen Sie es erneut.";
                }
            }
        }
    }
}
