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

            else if (parameter.Equals("AddEditEmployeeNewEmployeeClothesList"))
            {
                ClothesModel clothesToEdit = new(
                    _dVSListingViewModel.RemovedClothesListingItemModel.Clothes.GuidID,
                    _dVSListingViewModel.RemovedClothesListingItemModel.ID,
                    _dVSListingViewModel.RemovedClothesListingItemModel.Name,
                    _dVSListingViewModel.RemovedClothesListingItemModel.Clothes.Category,
                    _dVSListingViewModel.RemovedClothesListingItemModel.Clothes.Season,
                    _dVSListingViewModel.RemovedClothesListingItemModel.Comment);

                _dVSListingViewModel.RemovedClothesListingItemModel.ErrorMessage = null;

                if (_dVSListingViewModel.RemovedClothesListingItemModel.Quantity == 0)
                {
                    string messageBoxText = "Diese Bekleidung ist nicht verfügbar!";
                    string caption = "Bekleidung entfernen";
                    MessageBoxButton button = MessageBoxButton.OK;
                    MessageBoxImage icon = MessageBoxImage.Warning;
                    _ = MessageBox.Show(messageBoxText, caption, button, icon);
                }
                else if (_dVSListingViewModel.RemovedClothesListingItemModel.Quantity <= 3)
                {
                    string messageBoxText = $"ACHTUNG!\n\nVon dieser Bekleidung sind nur noch  {_dVSListingViewModel.RemovedClothesListingItemModel.Quantity-1}  Stück vorhanden!";
                    string caption = "Bekleidung entfernen";
                    MessageBoxButton button = MessageBoxButton.OK;
                    MessageBoxImage icon = MessageBoxImage.Warning;
                    _ = MessageBox.Show(messageBoxText, caption, button, icon);

                    clothesToEdit.Sizes.Add(new(_dVSListingViewModel.RemovedClothesListingItemModel.Size)
                    {
                        Quantity = _dVSListingViewModel.RemovedClothesListingItemModel.Quantity - 1,
                        IsSelected = true
                    });
                        
                }
                else
                {
                    clothesToEdit.Sizes.Add(new(_dVSListingViewModel.RemovedClothesListingItemModel.Size)
                    {
                        Quantity = _dVSListingViewModel.RemovedClothesListingItemModel.Quantity - 1,
                        IsSelected = true
                    });
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
