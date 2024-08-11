using DVS.Domain.Models;
using DVS.WPF.Stores;
using DVS.WPF.ViewModels.Forms;
using DVS.WPF.ViewModels.Views;
using System.Windows;

namespace DVS.WPF.Commands.AddEditClothesCommands
{
    public class UpdateClothesCommand(UpdateClothesViewModel updateClothesViewModel,
        ClothesStore clothesStore, ModalNavigationStore modalNavigationStore) : AsyncCommandBase
    {
        private readonly UpdateClothesViewModel _updateClothesViewModel = updateClothesViewModel;
        private readonly ClothesStore _clothesStore = clothesStore;
        private readonly ModalNavigationStore _modalNavigationStore = modalNavigationStore;

        public override async Task ExecuteAsync(object parameter)
        {
            string messageBoxText = "Bekleidung bearbeiten?";
            string caption = "Bekleidung bearbeiten";
            MessageBoxButton button = MessageBoxButton.YesNo;
            MessageBoxImage icon = MessageBoxImage.Warning;
            MessageBoxResult dialog = MessageBox.Show(messageBoxText, caption, button, icon);

            if (dialog == MessageBoxResult.Yes)
            {
                AddEditClothesFormViewModel updateClothesFormViewModel = _updateClothesViewModel.AddEditClothesFormViewModel;

                updateClothesFormViewModel.ErrorMessage = null;
                updateClothesFormViewModel.IsSubmitting = true;

                // Alle ausgewählten Größen in eine ZwischenListe speichern.
                var selectedSizes = updateClothesFormViewModel.AddEditListingViewModel.AvailableSizesUS.Any(size => size.IsSelected)
                    ? updateClothesFormViewModel.AddEditListingViewModel.AvailableSizesUS.Where(size => size.IsSelected)
                    : updateClothesFormViewModel.AddEditListingViewModel.AvailableSizesEU.Where(size => size.IsSelected);

                Clothes updatedClothes = new(updateClothesFormViewModel.Clothes.GuidID,
                                             updateClothesFormViewModel.ID,
                                             updateClothesFormViewModel.Name,
                                             updateClothesFormViewModel.Category,
                                             updateClothesFormViewModel.Season,
                                             updateClothesFormViewModel.Clothes.Comment);

                foreach (ClothesSize size in updateClothesFormViewModel.Clothes.Sizes)
                {
                    size.Size.ClothesSizes.Remove(size);
                }

                //TODO: Kommentare von DetailedItems werden entfernt bei einem update
                foreach (SizeModel size in selectedSizes)
                {
                    size.ClothesSizes.Add(new ClothesSize(Guid.NewGuid(), updatedClothes, size, size.Quantity));
                    updatedClothes.Sizes.Add(new ClothesSize(Guid.NewGuid(), updatedClothes, size, size.Quantity));
                }

                updatedClothes.Category?.Clothes.Remove(updateClothesFormViewModel.Clothes);
                updatedClothes.Category?.Clothes.Add(updatedClothes);
                updatedClothes.Season?.Clothes.Remove(updateClothesFormViewModel.Clothes);
                updatedClothes.Season?.Clothes.Add(updatedClothes);

                try
                {
                    await _clothesStore.Update(updatedClothes);
                }
                catch (Exception)
                {
                    updateClothesFormViewModel.ErrorMessage =
                        "Bearbeiten der Bekleidung ist fehlgeschlagen!\nBitte versuchen Sie es erneut.";
                }
                finally
                {
                    updateClothesFormViewModel.IsSubmitting = false;
                    _modalNavigationStore.Close();
                }
            }
        }
    }
}
