using DVS.Domain.Models;
using DVS.WPF.Stores;
using DVS.WPF.ViewModels.Forms;
using DVS.WPF.ViewModels.Views;
using System.Windows;

namespace DVS.WPF.Commands.AddEditClothesCommands
{
    public class EditClothesCommand(EditClothesViewModel editClothesViewModel,
        ClothesStore clothesStore, ModalNavigationStore modalNavigationStore) : AsyncCommandBase
    {
        private readonly EditClothesViewModel _editClothesViewModel = editClothesViewModel;
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
                AddEditClothesFormViewModel addEditClothesFormViewModel = _editClothesViewModel.AddEditClothesFormViewModel;

                addEditClothesFormViewModel.ErrorMessage = null;
                addEditClothesFormViewModel.IsSubmitting = true;

                // Alle ausgewählten Größen in eine ZwischenListe speichern.
                var selectedSizes = addEditClothesFormViewModel.AddEditListingViewModel.AvailableSizesUS.Any(size => size.IsSelected)
                    ? addEditClothesFormViewModel.AddEditListingViewModel.AvailableSizesUS.Where(size => size.IsSelected)
                    : addEditClothesFormViewModel.AddEditListingViewModel.AvailableSizesEU.Where(size => size.IsSelected);

                Clothes updatedClothes = new(addEditClothesFormViewModel.Clothes.GuidID,
                                             addEditClothesFormViewModel.ID,
                                             addEditClothesFormViewModel.Name,
                                             addEditClothesFormViewModel.Category,
                                             addEditClothesFormViewModel.Season,
                                             addEditClothesFormViewModel.Clothes.Comment);

                foreach (ClothesSize size in addEditClothesFormViewModel.Clothes.Sizes)
                {
                    size.Size.ClothesSizes.Remove(size);
                }

                //TODO: Kommentare von DetailedItems werden entfernt bei einem update
                foreach (SizeModel size in selectedSizes)
                {
                    size.ClothesSizes.Add(new ClothesSize(updatedClothes, size, size.Quantity));
                    updatedClothes.Sizes.Add(new ClothesSize(updatedClothes, size, size.Quantity));
                }

                addEditClothesFormViewModel.Clothes.Category?.Clothes.Remove(addEditClothesFormViewModel.Clothes);
                addEditClothesFormViewModel.Clothes.Season?.Clothes.Remove(addEditClothesFormViewModel.Clothes);
                updatedClothes.Category?.Clothes.Add(updatedClothes);
                updatedClothes.Season?.Clothes.Add(updatedClothes);

                try
                {
                    await _clothesStore.Update(updatedClothes);
                }
                catch (Exception)
                {
                    addEditClothesFormViewModel.ErrorMessage =
                        "Bearbeiten der Bekleidung ist fehlgeschlagen!\nBitte versuchen Sie es erneut.";
                }
                finally
                {
                    addEditClothesFormViewModel.IsSubmitting = false;
                    _modalNavigationStore.Close();
                }
            }
        }
    }
}
